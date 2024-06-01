using System;

namespace PupsCore.Services.InitService;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class BootstrapPropsAttribute : Attribute
{
  public uint Priority { get; private set; }
  public BootstrapPropsAttribute(uint priority) =>
    Priority = priority;
}