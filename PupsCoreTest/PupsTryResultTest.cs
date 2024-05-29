using System.Threading.Tasks;
using NUnit.Framework;
using PupsConsoleTest;
using PupsCore.Components;
using PupsCore.Services.InitService;
using PupsCore.Statics;

namespace PupsCoreTest;

[TestFixture]
public class PupsTryResultTest : AnyTestAbstract
{

  [Test]
  public void TryResultErrorTest()
  {
    var errorResult = SpecialErrorTest<object>().Result;
    Assert.That(errorResult.Success, Is.False);
  }
  [Test]
  public void TryResultSuccessTest()
  {
    var successResult = SpecialSuccessTest<object>().Result;
    Assert.That(successResult.Success, Is.True);
  }
  public Task<PupsTryResult<T>> SpecialErrorTest<T>()
  {
    try
    {
      throw new PupsException("Special Error", PupsExceptionType.Error);
    }
    catch (PupsException pe)
    {
      return Task.FromResult(new PupsTryResult<T>(false, default, pe));
    }
  }
  public Task<PupsTryResult<T>> SpecialSuccessTest<T>()
  {
    try
    {
      return Task.FromResult(new PupsTryResult<T>(true, default, null));
    }
    catch (PupsException pe)
    {
      return Task.FromResult(new PupsTryResult<T>(false, default, pe));
    }
  }
}