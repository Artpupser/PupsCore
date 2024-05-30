using PupsCore.Services.InitService;
using static PupsCore.Statics.PupsCoreStatic;

namespace PupsCoreApp;

internal class Program
{
  private static async Task Main() => await new Program().MainAsync();
  private async Task MainAsync()
  {
    await PupsCoreBootstrap.InitPupsCoreBootstrap();
    Console.WriteLine("Hello world!");
    Console.WriteLine("Input any text to end...");
    Console.ReadLine();
    await EndApplication(() =>
    {
      Console.WriteLine("Goodbye!");
    });
  }
}