using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using PupsCoreLib.Components;
using PupsCoreLib.Services.LogService;

namespace PupsCoreLib.Services.FileService;

public class FileManager
{
  private static FileManager instance;
  public static FileManager Instance { get => instance; private set => instance = value; }
  private readonly static string rootProjectPath = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/";
  private static LogManager logManager { get => LogManager.Instance; }
  private readonly Dictionary<PupsDirectoryEnum, string> _directoryPaths = new()
  {
    [PupsDirectoryEnum.RootProject] = rootProjectPath,
    [PupsDirectoryEnum.Logs] = $"{rootProjectPath}Logs/",
    [PupsDirectoryEnum.Data] = $"{rootProjectPath}Data/",
    [PupsDirectoryEnum.Config] = $"{rootProjectPath}Config/",
  };
  public FileManager()
  {
    if (Instance != null)
      throw new PupsException("FileManager is already initialized! [Singleton pattern]", PupsExceptionType.Error);
    Instance = this;
    _ = Init();
  }
  public Task<string> BuildPath(PupsDirectoryEnum pupsDirectoryEnum, string fullName = "") =>
    Task.FromResult($"{_directoryPaths[pupsDirectoryEnum]}{fullName}");
  #region FileOperations
  #region Other
  public Task DeleteFile(string path)
  {
    File.Delete(path);
    return Task.CompletedTask;
  }
  public Task DeleteDir(string path)
  {
    Directory.Delete(path);
    return Task.CompletedTask;
  }
  public Task<bool> IsExitis(string path) => Task.FromResult(File.Exists(path) || Directory.Exists(path));
  public Task CreateFile(string path)
  {
    using var stream = File.Create(path);
    return Task.CompletedTask;
  }
  #endregion
  #region Json
  public async Task<T> GetFileJson<T>(string path) => JsonConvert.DeserializeObject<T>(await File.ReadAllTextAsync(path));
  public async Task SetFileJson<T>(string path, T content) => await File.WriteAllTextAsync(path, JsonConvert.SerializeObject(content));
  #endregion
  #region Bson
  public async Task<T> GetFileBson<T>(string path)
  {
    var data = Convert.FromBase64String(await File.ReadAllTextAsync(path));
    using var ms = new MemoryStream(data);
    using var reader = new BsonDataReader(ms);
    return new JsonSerializer().Deserialize<T>(reader);
  }
  public async Task SetFileBson<T>(string path, T content)
  {
    using var ms = new MemoryStream();
    using var writer = new BsonDataWriter(ms);
    new JsonSerializer().Serialize(writer, content);
    await File.WriteAllTextAsync(path, Convert.ToBase64String(ms.ToArray()));
  }
  #endregion
  #region String
  public async Task<string> GetFileStr(string path) => await File.ReadAllTextAsync(path);
  public async Task SetFileStr(string path, string content) => await File.WriteAllTextAsync(path, content);
  #endregion
  #endregion
  private async Task Init()
  {
    foreach (var path in _directoryPaths.Values)
    {
      if (!Directory.Exists(path))
      {
        Directory.CreateDirectory(path);
        await logManager.PushLog($"Directory {path} created!", LogStatusType.Info);
        continue;
      }
      await logManager.PushLog($"Directory {path} already exists!", LogStatusType.Info);
    }
    await logManager.CreateFileLog();
  }
}