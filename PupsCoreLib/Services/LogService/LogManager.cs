using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PupsCoreLib.Components;
using PupsCoreLib.Services.FileService;

namespace PupsCoreLib.Services.LogService;

public class LogManager
{
  private static LogManager instance;
  public static LogManager Instance { get => instance; private set => instance = value; }
  private static FileManager fileManager { get => FileManager.Instance; }
  private string _logPath = string.Empty;
  private bool _logFileCreated = false;
  private StringBuilder _logBuilder;
  private string _postfixFileName { get => "log"; }
  private ushort _autoSaveFileValue = 25;
  private ushort _maxCountLogFiles = 10;
  private ushort _countLogToAppendFile = 0;
  public LogManager()
  {
    if (Instance != null)
      throw new PupsException("LogManager is already initialized! [Singleton pattern]", PupsExceptionType.Error);
    Instance = this;
    _ = Init();
  }
  private Task Init()
  {
    _logBuilder = new();
    _logBuilder.Append($"<< Log created: [{DateTime.Now}], App path: [{Assembly.GetExecutingAssembly().Location}], Core: [PupsCore] >>");
    AppDomain.CurrentDomain.ProcessExit += (sender, e) => _ = SaveFileLog();
    return Task.CompletedTask;
  }
  public async Task PushLog(string message, LogStatusType logStatus = LogStatusType.Info)
  {
    _logBuilder.Append(await BuildLog(message, logStatus));
    _countLogToAppendFile++;
    if (_autoSaveFileValue == _countLogToAppendFile)
      await SaveFileLog();
  }
  public async Task PushLog(LogMessage logMessage)
  {
    _logBuilder.Append(await BuildLog(logMessage));
    _countLogToAppendFile++;
    if (_autoSaveFileValue == _countLogToAppendFile)
      await SaveFileLog();
  }
  public async Task SaveFileLog()
  {
    if (!_logFileCreated)
      await CreateFileLog();
    _countLogToAppendFile = 0;
    await File.AppendAllTextAsync(_logPath, _logBuilder.ToString());
    _logBuilder.Clear();
  }
  public async Task CreateFileLog()
  {
    if (fileManager == null)
      throw new PupsException("LogManager does not working because FileManager not initialized!", PupsExceptionType.Error);
    await CheckCountLogFiles();
    var fileNameLog = $"{DateTime.Now:[MM-d-yyyy] h-mm-ss}.{_postfixFileName}";
    _logPath = await fileManager.BuildPath(PupsDirectoryEnum.Logs, fileNameLog);
    await fileManager.CreateFile(_logPath);
    _logFileCreated = true;
  }
  public async Task CheckCountLogFiles()
  {
    var dirInfo = new DirectoryInfo(await fileManager.BuildPath(PupsDirectoryEnum.Logs));
    var logsFiles = dirInfo.GetFiles($"*{_postfixFileName}", SearchOption.TopDirectoryOnly);
    var firstCreatedFile = logsFiles.OrderBy(x => x.CreationTime).FirstOrDefault();
    if (logsFiles.Length >= _maxCountLogFiles)
      await fileManager.DeleteFile(firstCreatedFile.FullName);
  }
  private Task<string> BuildLog(string message, LogStatusType logStatus) =>
    Task.FromResult($"\n[{DateTime.Now}] [{logStatus}] -> {message}");
  private Task<string> BuildLog(LogMessage logMessage) =>
    Task.FromResult($"\n[{logMessage.Time}] [{logMessage.LogStatus}] -> {logMessage}");
}
