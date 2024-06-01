using System.Runtime.CompilerServices;
using NUnit.Framework;
using PupsCore.Services.InitService;

namespace PupsConsoleTest;

public abstract class AnyTestAbstract
{
  [SetUp]
  public void SetUp()
  {
    RuntimeHelpers.RunClassConstructor(typeof(BootstrapManager).TypeHandle);
  }
}