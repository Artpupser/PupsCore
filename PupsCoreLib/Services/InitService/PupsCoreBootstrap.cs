using System.Threading.Tasks;
using PupsCore.Services.CatchService;
using PupsCore.Services.IOService;
using PupsCore.Services.LogService;

namespace PupsCore.Services.InitService;
public static class PupsCoreBootstrap
{
  public static bool AlreadyInited { get; private set; } = false;
  public static Task InitPupsCoreBootstrap()
  {
    _ = new LogManager();
    _ = new FileManager();
    _ = new CatchManager();
    _ = LogManager.Instance.PushLog("PupsCore initialized!", LogStatusType.Ok);
    AlreadyInited = true;
    return Task.CompletedTask;
  }
}