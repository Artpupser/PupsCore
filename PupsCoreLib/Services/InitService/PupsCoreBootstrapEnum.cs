using System;

namespace PupsCoreLib.Services.InitService;

[Flags]
public enum PupsCoreBootstrapEnum
{
  None = 0,
  LogManager = 1 << 0,
  FileManager = 1 << 1,
  CatchManager = 1 << 2,
  All = LogManager | FileManager | CatchManager
}
