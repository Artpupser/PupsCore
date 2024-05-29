namespace PupsCore.Components;

public struct PupsTryResult : IPupsTryResult
{
  private bool _success = false;
  private PupsException _exception = null;
  public PupsTryResult(bool success, PupsException exception = null)
  {
    Success = success;
    Exception = exception;
  }
  public bool Success { get => _success; set => _success = value; }
  public PupsException Exception { get => _exception; set => _exception = value; }
}

public struct PupsTryResult<T> : IPupsTryResult
{
  private bool _success = false;
  private PupsException _exception = null;
  private T _content = default;

  public PupsTryResult(bool success, T content, PupsException exception = null)
  {
    Success = success;
    Exception = exception;
    Content = content;
  }
  public bool Success { get => _success; set => _success = value; }
  public PupsException Exception { get => _exception; set => _exception = value; }
  public T Content { get => _content; set => _content = value; }
}
