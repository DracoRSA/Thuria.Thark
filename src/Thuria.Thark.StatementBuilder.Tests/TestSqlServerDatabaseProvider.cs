using NUnit.Framework;

using Thuria.Zitidar.Extensions;
using Thuria.Thark.Core.Statement;
using Thuria.Thark.StatementBuilder.Providers;

namespace Thuria.Thark.StatementBuilder.Tests
{
  [TestFixture]
  public class TestSqlServerDatabaseProvider
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var provider = new SqlServerDatabaseProvider();
      //---------------Test Result -----------------------
      Assert.IsNotNull(provider);
    }

    [TestCase("DatabaseProviderType", DatabaseProviderType.SqlServer)]
    [TestCase("StatementOpenQuote", "[")]
    [TestCase("StatementCloseQuote", "]")]
    public void Properties_ShouldReturnExpectedValue(string propertyName, object expectedValue)
    {
      //---------------Set up test pack-------------------
      var provider = new SqlServerDatabaseProvider();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var propertyValue = provider.GetPropertyValue(propertyName);
      //---------------Test Result -----------------------
      Assert.AreEqual(expectedValue, propertyValue);
    }
  }
}
