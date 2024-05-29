using System;
using System.Threading.Tasks;

namespace PupsCore.Components;

// -- NOT STABLE / NOT TESTED
public struct PupsTryResult<T>
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