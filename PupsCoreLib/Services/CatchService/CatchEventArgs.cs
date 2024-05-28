using System;
using PupsCoreLib.Components;

namespace PupsCoreLib.Services.CatchService;

public class CatchEventArgs : EventArgs
{
  private PupsException _exception;
  public PupsException Exception { get => _exception; set => _exception = value; }
  public CatchEventArgs(PupsException exception)
  {
    Exception = exception;
  }

}
