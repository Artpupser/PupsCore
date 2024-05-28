using System.Threading.Tasks;
using PupsCoreLib.Services.CatchService;
using PupsCoreLib.Services.FileService;
using PupsCoreLib.Services.LogService;

namespace PupsCoreLib.Services.InitService;
public static class PupsCoreBootstrap
{
  public static Task InitPupsCoreBootstrap(PupsCoreBootstrapEnum pupsCoreBootstrapEnum)
  {
    if (pupsCoreBootstrapEnum.HasFlag(PupsCoreBootstrapEnum.LogManager))
    {
      _ = new LogManager();
      _ = LogManager.Instance.PushLog("LogManager initialized!", LogStatusType.Info);
    }
    if (pupsCoreBootstrapEnum.HasFlag(PupsCoreBootstrapEnum.FileManager))
    {
      _ = new FileManager();
      _ = LogManager.Instance.PushLog("FileManager initialized!", LogStatusType.Info);
    }
    if (pupsCoreBootstrapEnum.HasFlag(PupsCoreBootstrapEnum.CatchManager))
    {
      _ = new CatchManager();
      _ = LogManager.Instance.PushLog("CatchManager initialized!", LogStatusType.Info);
    }
    _ = LogManager.Instance.PushLog("PupsCore initialized!", LogStatusType.Ok);
    return Task.CompletedTask;
  }
}