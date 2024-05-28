using System;

namespace PupsCoreLib.Services.LogService;

public struct LogMessage
{
  public LogMessage(string message, LogStatusType logStatus = LogStatusType.Info)
  {
    Message = message;
    LogStatus = logStatus;
  }
  public string Message { get; set; } = string.Empty;
  public LogStatusType LogStatus { get; set; } = LogStatusType.Info;
  public DateTime Time { get; set; } = DateTime.Now;
}
