using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using PupsCore.Services.InitService;
using static PupsCore.Statics.PupsCoreStatic;

namespace PupsCoreApp;

internal class Program
{
  public Program()
  {
    RuntimeHelpers.RunClassConstructor(typeof(BootstrapManager).TypeHandle);
  }
  private static async Task Main() => await new Program().MainAsync();
  private async Task MainAsync()
  {
    Console.WriteLine("Hello world!");
    Console.WriteLine("Input any text to end...");
    Console.ReadLine();
    await EndApplication(() =>
    {
      Console.WriteLine("Goodbye!");
    });
  }
}