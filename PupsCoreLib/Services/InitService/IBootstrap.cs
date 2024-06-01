using System.Threading.Tasks;

namespace PupsCore.Services.InitService;


public interface IBootstrap
{
  public abstract Task Init();
}
