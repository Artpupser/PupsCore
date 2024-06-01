using System;
using System.Linq;
using System.Reflection;

namespace PupsCore.Services.InitService;

public class BootstrapManager
{
  public static bool AlreadyInited { get; private set; } = false;
  static BootstrapManager()
  {
    if (!AlreadyInited)
    {
      var types = from t in Assembly.GetExecutingAssembly().GetTypes()
                  where t.GetInterfaces().Contains(typeof(IBootstrap)) && t.GetConstructor(Type.EmptyTypes) != null
                  select Activator.CreateInstance(t) as IBootstrap;
      types.OrderBy(t => t.GetType().GetCustomAttribute<BootstrapPropsAttribute>().Priority).ToList().ForEach(t => t.Init());
    }
    AlreadyInited = true;
  }

}
