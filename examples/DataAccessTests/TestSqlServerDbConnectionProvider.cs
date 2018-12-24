using System;
using NUnit.Framework;
using FluentAssertions;

namespace MGS.Casino.Veyron.DataAccess.Tests
{
  [TestFixture]
  public class TestSqlServerDbConnectionProvider
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var dbConnectionProvider = new SqlServerDbConnectionProvider();
      //---------------Test Result -----------------------
      dbConnectionProvider.Should().NotBeNull();
    }

    [Test]
    public void CreateDbConnection_GivenEmptyConnectionString_ShouldThrowException()
    {
      //---------------Set up test pack-------------------
      var dbConnectionProvider = new SqlServerDbConnectionProvider();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var exception = Assert.ThrowsAsync<ArgumentNullException>(() => dbConnectionProvider.CreateDbConnectionAsync(null));
      //---------------Test Result -----------------------
      exception.ParamName.Should().Be("connectionString");
    }
  }
}
