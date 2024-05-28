using System.Threading.Tasks;
using System;
using System.Linq;

namespace PupsCoreLib.Components;

public class PupsEvent<T> where T : EventArgs
{
  public delegate void PupsEventHandler(object s, T e);
  private event PupsEventHandler _event;
  public Task<int> AddListener(PupsEventHandler handler)
  {
    _event += handler;
    return Task.FromResult(_event.GetInvocationList().Length - 1);
  }
  public Task RemoveListener(PupsEventHandler handler)
  {
    _event -= handler;
    return Task.CompletedTask;
  }
  public Task RemoveListener(int index)
  {
    index = Math.Clamp(index, 0, _event.GetInvocationList().Length - 1);
    _event -= _event.GetInvocationList()[index] as PupsEventHandler;
    return Task.CompletedTask;
  }
  public Task RemoveAllListeners()
  {
    _event.GetInvocationList().ToList().ForEach(x => _event -= x as PupsEventHandler);
    return Task.CompletedTask;
  }
  public Task Invoke(object s, T e)
  {
    _event?.Invoke(s, e);
    return Task.CompletedTask;
  }
}