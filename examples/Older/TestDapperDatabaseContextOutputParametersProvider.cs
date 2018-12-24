using NUnit.Framework;
using FluentAssertions;

namespace MGS.Casino.Veyron.DataAccess.Tests
{
  [TestFixture]
  public class TestDapperDatabaseContextOutputParametersProvider
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var outputParametersProvider = new DapperDatabaseContextOutputParametersProvider();
      //---------------Test Result -----------------------
      outputParametersProvider.Should().NotBeNull();
    }
  }
}
