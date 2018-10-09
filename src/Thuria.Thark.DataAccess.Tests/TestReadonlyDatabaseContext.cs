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
  public class TestReadonlyDatabaseContext
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      var dbConnection       = Substitute.For<IDbConnection>();
      var transactionManager = Substitute.For<IDatabaseTransactionProvider>();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var databaseContext = new ReadonlyDatabaseContext(dbConnection, transactionManager);
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
      ConstructorTestHelper.ValidateArgumentNullExceptionIfParameterIsNull<ReadonlyDatabaseContext>(parameterName);
      //---------------Test Result -----------------------
    }

    [Test]
    public void Dispose_ShouldDisposeAllObjects()
    {
      //---------------Set up test pack-------------------
      var dbConnection       = Substitute.For<IDbConnection>();
      var transactionManager = Substitute.For<IDatabaseTransactionProvider>();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      using (new ReadonlyDatabaseContext(dbConnection, transactionManager))
      {
      }
      //---------------Test Result -----------------------
      dbConnection.Received(1).Dispose();
      transactionManager.Received(1).Dispose();
    }

    [Test]
    public void Select_GivenSqlStatement_ShouldCallQueryWithStatement()
    {
      //---------------Set up test pack-------------------
      var sqlStatement    = "SELECT * FROM Test";
      var dbConnection    = Substitute.For<IDbConnection>();
      var databaseContext = CreateDatabaseContext(dbConnection);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      databaseContext.Select<FakeTestDataModel>(sqlStatement);
      //---------------Test Result -----------------------
      dbConnection.Received(1).Query<FakeTestDataModel>(sqlStatement);
    }

    [Test]
    public void SelectOne_GivenSqlStatement_ShouldCallQueryWithStatement()
    {
      //---------------Set up test pack-------------------
      var sqlStatement    = "SELECT * FROM Test";
      var dbConnection    = Substitute.For<IDbConnection>();
      var databaseContext = CreateDatabaseContext(dbConnection);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      databaseContext.SelectOne<FakeTestDataModel>(sqlStatement);
      //---------------Test Result -----------------------
      dbConnection.Received(1).Query<FakeTestDataModel>(sqlStatement);
    }

    private IReadonlyDatabaseContext CreateDatabaseContext(IDbConnection dbConnection = null, IDatabaseTransactionProvider databaseTransactionProvider = null)
    {
      var connection         = dbConnection ?? Substitute.For<IDbConnection>();
      var transactionManager = databaseTransactionProvider ?? Substitute.For<IDatabaseTransactionProvider>();

      return new ReadonlyDatabaseContext(connection, transactionManager);
    }
  }
}