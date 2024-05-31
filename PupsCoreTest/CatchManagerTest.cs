using NUnit.Framework;
using PupsConsoleTest;
using PupsCore.Components;
using PupsCore.Services.CatchService;
using PupsCore.Services.InitService;
using PupsCore.Statics;

namespace PupsCoreTest;

[TestFixture]
public class CatchManagerTest : AnyTestAbstract
{
  [Test]
  public void FakeExcpetionTest()
  {
    CatchManager.Instance.onCatchAnyException.AddListener((s, e) =>
    {
      Assert.That(e.Exception.Message, Is.EqualTo("Fake Exception Test"));
      Assert.That(e.Exception.ExceptionType, Is.EqualTo(PupsExceptionType.Warning));
    });
    try
    {
      throw new System.Exception("Fake Exception Test");
    }
    catch (System.Exception)
    {

    }
  }
  [Test]
  public void FakePupsExcpetionTest()
  {
    CatchManager.Instance.onCatchAnyException.AddListener((s, e) =>
    {
      Assert.That(e.Exception.Message, Is.EqualTo("Fake PupsException Test"));
      Assert.That(e.Exception.ExceptionType, Is.EqualTo(PupsExceptionType.Error));
    });
    try
    {
      throw new PupsException("Fake PupsException Test", PupsExceptionType.Error);
    }
    catch (PupsException)
    {

    }
  }
}