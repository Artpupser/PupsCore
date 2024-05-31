namespace PupsCore.Services.LogService;

public enum LogStatusType : byte
{
  Ok = 0,
  Info = 1,
  Debug = 2,
  Trace = 3,
  Warning = 4,
  Error = 5,
  Fatal = 6,
}