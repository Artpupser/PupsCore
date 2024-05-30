using System;
using System.Threading.Tasks;
using PupsCore.Services.LogService;

namespace PupsCore.Statics;

public class PupsCoreStatic
{
  private readonly static Random random = new();
  public static int RandomInt(int min, int max) => random.Next(min, max + 1);
  public static async Task EndApplication(Action lastAction = null)
  {
    lastAction?.Invoke();
    await LogManager.Instance.PushLog("PupsCoreApp ending :)", LogStatusType.Ok);
    await LogManager.Instance.SaveFileLog();
    Environment.Exit(0);
  }
}