using NUnit.Framework;

using Thuria.Zitidar.Extensions;
using Thuria.Thark.Core.Statement;
using Thuria.Thark.StatementBuilder.Providers;

namespace Thuria.Thark.StatementBuilder.Tests
{
  [TestFixture]
  public class TestPostgresDatabaseProvider
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var provider = new PostgresDatabaseProvider();
      //---------------Test Result -----------------------
      Assert.IsNotNull(provider);
    }

    [TestCase("DatabaseProviderType", DatabaseProviderType.Postgres)]
    [TestCase("StatementOpenQuote", "[")]
    [TestCase("StatementCloseQuote", "]")]
    public void Properties_ShouldReturnExpectedValue(string propertyName, object expectedValue)
    {
      //---------------Set up test pack-------------------
      var provider = new PostgresDatabaseProvider();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var propertyValue = provider.GetPropertyValue(propertyName);
      //---------------Test Result -----------------------
      Assert.AreEqual(expectedValue, propertyValue);
    }
  }
}