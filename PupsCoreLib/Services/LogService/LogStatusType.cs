namespace PupsCoreLib.Services.LogService;

public enum LogStatusType : byte
{
  Ok = 0,
  Info = 1,
  Trace = 3,
  Debug = 2,
  Warning = 4,
  Error = 5,
  Fatal = 6,
}
