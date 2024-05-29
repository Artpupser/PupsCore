using System;

namespace PupsCore.Components;

public class PupsException : Exception
{
  private PupsExceptionType _exceptionType;
  public PupsExceptionType ExceptionType { get => _exceptionType; private set => _exceptionType = value; }

  public PupsException(string message, PupsExceptionType exceptionType) : base(message)
  {
    ExceptionType = exceptionType;
  }
  public PupsException(string message) : base(message)
  {
    ExceptionType = PupsExceptionType.Warning;
  }
}