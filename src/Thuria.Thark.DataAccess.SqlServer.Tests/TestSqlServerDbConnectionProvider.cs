using System.Data.SqlClient;

using NUnit.Framework;
using FluentAssertions;

using Thuria.Calot.TestUtilities;

namespace Thuria.Thark.DataAccess.SqlServer.Tests
{
  [TestFixture]
  public class TestSqlServerDbConnectionProvider
  {
    private readonly string _connectionString = "Data Source=localhost;Initial Catalog=Test;Persist Security Info=True;User Id=user;Password=somePassword;Connect Timeout=300;";

    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var provider = new SqlServerDbConnectionProvider();
      //---------------Test Result -----------------------
      provider.Should().NotBeNull();
    }

    [TestCase("GetConnection", "connectionString")]
    public void Method_GivenNullParameter_ShouldThrowException(string methodName, string parameterName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      MethodTestHelper.ValidateArgumentNullExceptionIfParameterIsNull<SqlServerDbConnectionProvider>(methodName, parameterName);
      //---------------Test Result -----------------------
    }

    [Test]
    public void GetConnection_ShouldReturnSqlConnection()
    {
      //---------------Set up test pack-------------------
      var provider = new SqlServerDbConnectionProvider();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var dbConnection = provider.GetConnection(_connectionString);
      //---------------Test Result -----------------------
      dbConnection.Should().BeOfType<SqlConnection>();
    }
  }
}
