using NUnit.Framework;
using PupsCore.Services.InitService;

namespace PupsConsoleTest;

public abstract class AnyTestAbstract
{
  [SetUp]
  public void Setup()
  {
    if (PupsCoreBootstrap.AlreadyInited == false)
      PupsCoreBootstrap.InitPupsCoreBootstrap();
  }
}