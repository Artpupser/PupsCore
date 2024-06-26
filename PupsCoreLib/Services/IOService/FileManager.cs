using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using PupsCore.Components;
using PupsCore.Services.LogService;

namespace PupsCore.Services.IOService;

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
    _ = LogManager.Instance.PushLog("FileManager initialized!", LogStatusType.Info);
  }
  public Task<string> BuildPath(PupsDirectoryEnum pupsDirectoryEnum, string fullName = "") => Task.FromResult($"{_directoryPaths[pupsDirectoryEnum]}{fullName}");
  #region FileOperations
  #region Other
  public Task DeleteFile(string path)
  {
    File.Delete(path);
    logManager.PushLog($"File deleted: [{path}]", LogStatusType.Info);
    return Task.CompletedTask;
  }
  public Task DeleteDir(string path)
  {
    Directory.Delete(path);
    logManager.PushLog($"Directory deleted: [{path}]", LogStatusType.Info);
    return Task.CompletedTask;
  }
  public Task<bool> IsExitis(string path) => Task.FromResult(File.Exists(path) || Directory.Exists(path));
  public Task CreateFile(string path)
  {
    using var stream = File.Create(path);
    logManager.PushLog($"File created: [{path}]", LogStatusType.Info);
    return Task.CompletedTask;
  }
  #endregion
  #region Json
  public async Task<PupsTryResult<T>> TryGetFileJson<T>(string path)
  {
    try
    {
      return new PupsTryResult<T>(true, await GetFileJson<T>(path));
    }
    catch (Exception e)
    {
      return new PupsTryResult<T>(false, default, (PupsException)e);
    }
  }
  public async Task<PupsTryResult> TrySetFileJson<T>(string path, T content)
  {
    try
    {
      await SetFileJson(path, content);
      return new PupsTryResult(true);
    }
    catch (Exception e)
    {
      return new PupsTryResult(false, (PupsException)e);
    }
  }
  public async Task<T> GetFileJson<T>(string path) => JsonConvert.DeserializeObject<T>(await File.ReadAllTextAsync(path));
  public async Task SetFileJson<T>(string path, T content) => await File.WriteAllTextAsync(path, JsonConvert.SerializeObject(content));
  #endregion
  #region Bson
  public async Task<PupsTryResult<T>> TryGetFileBson<T>(string path)
  {
    try
    {
      return new PupsTryResult<T>(true, await GetFileBson<T>(path));
    }
    catch (Exception e)
    {
      return new PupsTryResult<T>(false, default, (PupsException)e);
    }
  }
  public async Task<PupsTryResult> TrySetFileBson<T>(string path, T content)
  {
    try
    {
      await SetFileBson(path, content);
      return new PupsTryResult(true);
    }
    catch (Exception e)
    {
      return new PupsTryResult(false, (PupsException)e);
    }
  }
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
  public async Task<PupsTryResult<string>> TryGetFileStr(string path)
  {
    try
    {
      return new PupsTryResult<string>(true, await GetFileStr(path));
    }
    catch (Exception e)
    {
      return new PupsTryResult<string>(false, "", (PupsException)e);
    }
  }
  public async Task<PupsTryResult> TryGetFileStr(string path, string content)
  {
    try
    {
      await SetFileStr(path, content);
      return new PupsTryResult(true);
    }
    catch (Exception e)
    {
      return new PupsTryResult(false, (PupsException)e);
    }
  }
  public async Task<string> GetFileStr(string path) => await File.ReadAllTextAsync(path);
  public async Task SetFileStr(string path, string content) => await File.WriteAllTextAsync(path, content);
  #endregion
  #endregion
  private async Task Init()
  {
    foreach (var path in _directoryPaths.Values)
    {
      if (Directory.Exists(path))
      {
        await logManager.PushLog($"Directory {path} already exists!", LogStatusType.Info);
        continue;
      }
      Directory.CreateDirectory(path);
      await logManager.PushLog($"Directory {path} created!", LogStatusType.Info);
    }
    await logManager.CreateFileLog();
  }
}