﻿using PupsCoreLib.Services.InitService;
using PupsCoreLib.Services.LogService;

namespace PupsCoreApp;

internal class Program
{
  private static async Task Main() => await new Program().MainAsync();
  private async Task MainAsync()
  {
    await PupsCoreBootstrap.InitPupsCoreBootstrap(PupsCoreBootstrapEnum.All);
    Console.WriteLine("Hello world!");
    Console.WriteLine("Input any text to end...");
    Console.ReadLine();
    await EndApplication();
  }
  private async Task EndApplication()
  {
    await LogManager.Instance.PushLog("PupsCoreApp ending :)", LogStatusType.Ok);
  }
}