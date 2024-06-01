using System.Threading.Tasks;
using PupsCore.Services.CatchService;
using PupsCore.Services.IOService;
using PupsCore.Services.LogService;
using PupsCore.Services.InitService;

namespace PupsCore.Services.InitService;

[BootstrapProps(uint.MaxValue)]
public class PupsCoreBootstrap : IBootstrap
{
  public Task Init()
  {
    _ = new LogManager();
    _ = new FileManager();
    _ = new CatchManager();
    _ = LogManager.Instance.PushLog("PupsCore initialized!", LogStatusType.Ok);
    return Task.CompletedTask;
  }
}