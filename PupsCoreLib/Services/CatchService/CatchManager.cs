using System;
using System.Threading.Tasks;
using PupsCore.Components;
using PupsCore.Services.LogService;

namespace PupsCore.Services.CatchService;

public class CatchManager
{
  private static CatchManager instance;
  public static CatchManager Instance { get => instance; private set => instance = value; }
  private static LogManager logManager { get => LogManager.Instance; }
  public PupsEvent<CatchEventArgs> onCatchAnyException = new();
  public CatchManager()
  {
    if (Instance != null)
      throw new PupsException("CatchManager is already initialized! [Singleton pattern]", PupsExceptionType.Error);
    Instance = this;
    _ = Init();
  }
  private Task Init()
  {
    AppDomain.CurrentDomain.UnhandledException += UnhandledException;
    return Task.CompletedTask;
  }

  private void UnhandledException(object sender, UnhandledExceptionEventArgs e)
  {
    var basicException = e.ExceptionObject as Exception;
    var pupsException = e.ExceptionObject as PupsException ?? new("", PupsExceptionType.Warning);
    var message = $"isTerminating: [{e.IsTerminating}], {basicException.Source} >\n{basicException.StackTrace},\n\tMessage: [{pupsException.Message}]";
    switch (pupsException.ExceptionType)
    {
      case PupsExceptionType.Warning:
        _ = logManager.PushLog(message, LogStatusType.Warning);
        break;
      case PupsExceptionType.Error:
        _ = logManager.PushLog(message, LogStatusType.Error);
        break;
      case PupsExceptionType.Fatal:
        _ = logManager.PushLog(message, LogStatusType.Fatal);
        break;
    }
    onCatchAnyException.Invoke(this, new CatchEventArgs(pupsException));
    _ = logManager.PushLog("<< Program end working... >>", LogStatusType.Info);
    _ = logManager.SaveFileLog();
  }
}