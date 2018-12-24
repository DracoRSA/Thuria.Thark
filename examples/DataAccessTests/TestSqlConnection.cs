using System.Data;
using Moq;
using NUnit.Framework;
using FluentAssertions;
using MGS.Casino.Veyron.TestUtilities;

namespace MGS.Casino.Veyron.DataAccess.Tests
{
  [TestFixture]
  public class TestVeyronSqlConnection
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      var dbConnection = new Mock<IDbConnection>();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var sqlConnection = new VeyronSqlConnection(dbConnection.Object);
      //---------------Test Result -----------------------
      sqlConnection.Should().NotBeNull();
    }

    [TestCase("dbConnection")]
    public void Constructor_GivenNullParameter_ShouldThrowException(string parameterName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      ConstructorTestHelper.ValidateArgumentNullExceptionIfParameterIsNull<VeyronSqlConnection>(parameterName);
      //---------------Test Result -----------------------
    }

    [TestCase("dbConnection", "Connection")]
    public void Constructor_ShouldSetPropertiesWithParameterValue(string parameterName, string propertyName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      ConstructorTestHelper.ValidatePropertySetWithParameter<VeyronSqlConnection>(parameterName, propertyName);
      //---------------Test Result -----------------------
    }

    [Test]
    public void BeginTransaction_GivenNoIsolationLevel_ShouldCreateTransactionAndSetTransactionProperty()
    {
      //---------------Set up test pack-------------------
      var dbTransaction = new Mock<IDbTransaction>();
      var dbConnection  = new Mock<IDbConnection>();
      dbConnection.Setup(connection => connection.BeginTransaction()).Returns(dbTransaction.Object);
      var sqlConnection = new VeyronSqlConnection(dbConnection.Object);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var returnedTransaction = sqlConnection.BeginTransaction();
      //---------------Test Result -----------------------
      sqlConnection.Transaction.Should().Be(dbTransaction.Object);
      returnedTransaction.Should().Be(dbTransaction.Object);
      dbConnection.Verify(connection => connection.BeginTransaction(), Times.Once);
    }

    [TestCase(IsolationLevel.ReadCommitted)]
    [TestCase(IsolationLevel.Chaos)]
    [TestCase(IsolationLevel.ReadUncommitted)]
    public void BeginTransaction_GivenIsolationLevel_ShouldCreateTransactionAndSetTransactionProperty(IsolationLevel isolationLevel)
    {
      //---------------Set up test pack-------------------
      var dbTransaction = new Mock<IDbTransaction>();
      var dbConnection  = new Mock<IDbConnection>();
      dbConnection.Setup(connection => connection.BeginTransaction(It.IsAny<IsolationLevel>())).Returns(dbTransaction.Object);
      var sqlConnection = new VeyronSqlConnection(dbConnection.Object);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var returnedTransaction = sqlConnection.BeginTransaction(isolationLevel);
      //---------------Test Result -----------------------
      sqlConnection.Transaction.Should().Be(dbTransaction.Object);
      returnedTransaction.Should().Be(dbTransaction.Object);
      dbConnection.Verify(connection => connection.BeginTransaction(isolationLevel), Times.Once);
    }

    [Test]
    public void ConnectionString_Get_ShouldReturnDbConnectionConnectionString()
    {
      //---------------Set up test pack-------------------
      var connectionString = "connectionString";
      var dbConnection     = new Mock<IDbConnection>();
      dbConnection.Setup(connection => connection.ConnectionString).Returns(connectionString);

      var sqlConnection = new VeyronSqlConnection(dbConnection.Object);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var result = sqlConnection.ConnectionString;
      //---------------Test Result -----------------------
      result.Should().Be(connectionString);
    }

    [Test]
    public void ConnectionString_Set_ShouldSetDbConnectionConnectionString()
    {
      //---------------Set up test pack-------------------
      var connectionString = "connectionString";
      var dbConnection     = new Mock<IDbConnection>();
      var sqlConnection    = new VeyronSqlConnection(dbConnection.Object);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      sqlConnection.ConnectionString = connectionString;
      //---------------Test Result -----------------------
      dbConnection.VerifySet(connection => connection.ConnectionString = connectionString, Times.Once);
    }

    [Test]
    public void ConnectionTimeout_ShouldReturnDbConnectionConnectionTimeout()
    {
      //---------------Set up test pack-------------------
      var connectionTimeout = 99;
      var dbConnection      = new Mock<IDbConnection>();
      dbConnection.Setup(connection => connection.ConnectionTimeout).Returns(connectionTimeout);

      var sqlConnection = new VeyronSqlConnection(dbConnection.Object);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var result = sqlConnection.ConnectionTimeout;
      //---------------Test Result -----------------------
      result.Should().Be(connectionTimeout);
    }

    [Test]
    public void State_ShouldReturnDbConnectionState()
    {
      //---------------Set up test pack-------------------
      var connectionState = ConnectionState.Executing;
      var dbConnection    = new Mock<IDbConnection>();
      dbConnection.Setup(connection => connection.State).Returns(connectionState);

      var sqlConnection = new VeyronSqlConnection(dbConnection.Object);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var result = sqlConnection.State;
      //---------------Test Result -----------------------
      result.Should().Be(connectionState);
    }

    [Test]
    public void Database_ShouldReturnDbConnectionDatabase()
    {
      //---------------Set up test pack-------------------
      var connectionDatabase = "casino";
      var dbConnection       = new Mock<IDbConnection>();
      dbConnection.Setup(connection => connection.Database).Returns(connectionDatabase);

      var sqlConnection = new VeyronSqlConnection(dbConnection.Object);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var result = sqlConnection.Database;
      //---------------Test Result -----------------------
      result.Should().Be(connectionDatabase);
    }

    [Test]
    public void ChangeDatabase_ShouldCallChangeDatabaseOnDbConnection()
    {
      //---------------Set up test pack-------------------
      var databaseName  = "testDB";
      var dbConnection  = new Mock<IDbConnection>();
      var sqlConnection = new VeyronSqlConnection(dbConnection.Object);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      sqlConnection.ChangeDatabase(databaseName);
      //---------------Test Result -----------------------
      dbConnection.Verify(connection => connection.ChangeDatabase(databaseName), Times.Once);
    }

    [Test]
    public void Open_ShouldCallOpenOnDbConnection()
    {
      //---------------Set up test pack-------------------
      var dbConnection  = new Mock<IDbConnection>();
      var sqlConnection = new VeyronSqlConnection(dbConnection.Object);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      sqlConnection.Open();
      //---------------Test Result -----------------------
      dbConnection.Verify(connection => connection.Open(), Times.Once);
    }

    [Test]
    public void Close_ShouldCallCloseOnDbConnection()
    {
      //---------------Set up test pack-------------------
      var dbConnection  = new Mock<IDbConnection>();
      var sqlConnection = new VeyronSqlConnection(dbConnection.Object);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      sqlConnection.Close();
      //---------------Test Result -----------------------
      dbConnection.Verify(connection => connection.Close(), Times.Once);
    }

    [Test]
    public void Dispose_ShouldCallDisposeOnDbConnection()
    {
      //---------------Set up test pack-------------------
      var dbConnection  = new Mock<IDbConnection>();
      var sqlConnection = new VeyronSqlConnection(dbConnection.Object);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      sqlConnection.Dispose();
      //---------------Test Result -----------------------
      dbConnection.Verify(connection => connection.Dispose(), Times.Once);
    }

    [Test]
    public void CreateCommand_ShouldCallCreateCommandOnDbConnectionAndReturnExpectedCommand()
    {
      //---------------Set up test pack-------------------
      var dbCommand    = new Mock<IDbCommand>();
      var dbConnection = new Mock<IDbConnection>();
      dbConnection.Setup(connection => connection.CreateCommand()).Returns(dbCommand.Object);

      var sqlConnection = new VeyronSqlConnection(dbConnection.Object);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var newCommand = sqlConnection.CreateCommand();
      //---------------Test Result -----------------------
      dbConnection.Verify(connection => connection.CreateCommand(), Times.Once);
      newCommand.Should().Be(dbCommand.Object);
    }
  }
}
