using NUnit.Framework;
using FluentAssertions;

using Microsoft.Data.Sqlite;

using Thuria.Calot.TestUtilities;

namespace Thuria.Thark.DataAccess.Sqlite.Tests
{
  [TestFixture]
  public class TestSqliteDbConnectionProvider
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var provider = new SqliteDbConnectionProvider();
      //---------------Test Result -----------------------
      provider.Should().NotBeNull();
    }

    [TestCase("GetConnection", "connectionString")]
    public void Method_GivenNullParameter_ShouldThrowException(string methodName, string parameterName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      MethodTestHelper.ValidateArgumentNullExceptionIfParameterIsNull<SqliteDbConnectionProvider>(methodName, parameterName);
      //---------------Test Result -----------------------
    }

    [Test]
    public void GetConnection_ShouldReturnSqlConnection()
    {
      //---------------Set up test pack-------------------
      var connectionString = new SqliteConnectionStringBuilder()
                               {
                                 Mode = SqliteOpenMode.Memory
                               }.ToString();

      var provider = new SqliteDbConnectionProvider();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var dbConnection = provider.GetConnection(connectionString);
      //---------------Test Result -----------------------
      dbConnection.Should().BeOfType<SqliteConnection>();
    }
  }
}
