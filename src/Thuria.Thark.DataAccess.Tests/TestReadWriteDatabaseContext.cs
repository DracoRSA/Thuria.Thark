using System.Data;
using Dapper;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Thuria.Calot.TestUtilities;
using Thuria.Thark.Core.DataAccess;
using Thuria.Thark.DataAccess.Context;

namespace Thuria.Thark.DataAccess.Tests
{
  [TestFixture]
  public class TestReadWriteDatabaseContext
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      var dbConnection               = Substitute.For<IDbConnection>();
      var databaseTransactionManager = Substitute.For<IDatabaseTransactionProvider>();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var databaseContext = new ReadWriteDatabaseContext(dbConnection, databaseTransactionManager);
      //---------------Test Result -----------------------
      databaseContext.Should().NotBeNull();
    }

    [TestCase("dbConnection")]
    [TestCase("databaseTransactionProvider")]
    public void Constructor_GivenNullParameter_ShouldThrowException(string parameterName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      ConstructorTestHelper.ValidateArgumentNullExceptionIfParameterIsNull<ReadWriteDatabaseContext>(parameterName);
      //---------------Test Result -----------------------
    }

    [Test]
    public void Insert_ShouldExecuteSqlStatement()
    {
      //---------------Set up test pack-------------------
      var sqlStatement    = "INSERT INTO [Table1] (Id, Name) VALUES (1, 'Test')";
      var dbConnection    = Substitute.For<IDbConnection>();
      var databaseContext = CreateDatabaseContext(dbConnection);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      databaseContext.Insert<object>(sqlStatement);
      //---------------Test Result -----------------------
      dbConnection.Received(1).Execute(sqlStatement);
    }

    [Test]
    public void Update_ShouldExecuteSqlStatement()
    {
      //---------------Set up test pack-------------------
      var sqlStatement    = "UPDATE [Table1] SET Name = 'Test' WHERE Id = 1";
      var dbConnection    = Substitute.For<IDbConnection>();
      var databaseContext = CreateDatabaseContext(dbConnection);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      databaseContext.Update<object>(sqlStatement);
      //---------------Test Result -----------------------
      dbConnection.Received(1).Execute(sqlStatement);
    }

    private IReadWriteDatabaseContext CreateDatabaseContext(IDbConnection dbConnection = null, IDatabaseTransactionProvider transactionProvider = null)
    {
      var connection = dbConnection ?? Substitute.For<IDbConnection>();
      var manager    = transactionProvider ?? Substitute.For<IDatabaseTransactionProvider>();

      return new ReadWriteDatabaseContext(connection, manager);
    }
  }
}
