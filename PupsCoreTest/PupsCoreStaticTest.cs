using NUnit.Framework;
using PupsConsoleTest;
using PupsCore.Services.InitService;
using PupsCore.Statics;

namespace PupsCoreTest;

[TestFixture]
public class PupsCoreStaticTest : AnyTestAbstract
{
  [Test]
  public void RandomIntTest()
  {
    var first = PupsCoreStatic.RandomInt(1, 10);
    var second = PupsCoreStatic.RandomInt(1, 10);
    Assert.That(first, Is.Not.EqualTo(second));
  }
}