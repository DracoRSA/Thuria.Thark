using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using NSubstitute;
using NUnit.Framework;
using FluentAssertions;

using Thuria.Zitidar.Extensions;
using Thuria.Calot.TestUtilities;
using Thuria.Thark.Core.Providers;
using Thuria.Thark.Core.DataAccess;
using Thuria.Thark.DataAccess.Context;
using Thuria.Thark.DataAccess.Providers;

namespace Thuria.Thark.DataAccess.Tests.Context
{
  [TestFixture]
  public class TestSqlDatabaseContext
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      var connectionStringProvider    = Substitute.For<IConnectionStringProvider>();
      var databaseConnectionProvider  = Substitute.For<IDatabaseConnectionProvider>();
      var databaseTransactionProvider = Substitute.For<IDatabaseTransactionProvider>();
      var sqlStatementBuildProvider   = Substitute.For<IStatementBuildProvider>();
      var dataModelPopulateProvider   = Substitute.For<IDataModelPopulateProvider>();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var databaseContext = new SqlDatabaseContext(connectionStringProvider, 
                                                   databaseConnectionProvider, 
                                                   databaseTransactionProvider, 
                                                   sqlStatementBuildProvider, 
                                                   dataModelPopulateProvider);
      //---------------Test Result -----------------------
      databaseContext.Should().NotBeNull();
    }

    [TestCase("connectionStringProvider")]
    [TestCase("databaseConnectionProvider")]
    [TestCase("databaseTransactionProvider")]
    [TestCase("sqlStatementBuildProvider")]
    public void Constructor_GivenNullParameter_ShouldThrowArgumentNullException(string parameterName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      ConstructorTestHelper.ValidateArgumentNullExceptionIfParameterIsNull<SqlDatabaseContext>(parameterName);
      //---------------Test Result -----------------------
    }

    [Test]
    public void Dispose_ShouldCompleteAndDisposeDbDatabaseTransactionProvider()
    {
      //---------------Set up test pack-------------------
      var databaseTransactionProvider = Substitute.For<IDatabaseTransactionProvider>();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      using (var _ = CreateDatabaseContext(databaseTransactionProvider: databaseTransactionProvider))
      {
      }
      //---------------Test Result -----------------------
      databaseTransactionProvider.Received(1).Complete();
      databaseTransactionProvider.Received(1).Dispose();
    }

    [TestCase("CommandTimeout")]
    [TestCase("DbContextName")]
    public void Properties_GivenValue_ShouldSetParameterValue(string propertyName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      PropertyTestHelper.ValidateGetAndSet<SqlDatabaseContext>(propertyName);
      //---------------Test Result -----------------------
    }

    [TestCase("CommandTimeout", 30)]
    [TestCase("DbContextName", "Thark")]
    public void Property_GivenDefaultInstantiation_ShouldBeSetToDefaultExpectedValues(string propertyName, object expectedValue)
    {
      //---------------Set up test pack-------------------
      var databaseContext = CreateDatabaseContext();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var propertyValue = databaseContext.GetPropertyValue(propertyName);
      //---------------Test Result -----------------------
      propertyValue.Should().Be(expectedValue);
    }

    [Test]
    public void DbConnection_GivenFirstUse_ShouldGetConnectionString()
    {
      //---------------Set up test pack-------------------
      var connectionStringProvider = Substitute.For<IConnectionStringProvider>();

      using (var databaseContext = (FakeSqlDatabaseContext) CreateDatabaseContext(connectionStringProvider, useFalseContext: true))
      {
        //---------------Assert Precondition----------------
        //---------------Execute Test ----------------------
        var dbConnection = databaseContext.InternalDbConnection;
        //---------------Test Result -----------------------
        dbConnection.Should().NotBeNull();
        connectionStringProvider.Received(1).GetConnectionString(Arg.Any<string>());
      }
    }

    [Test]
    public void DbConnection_GivenAfterFirstUse_ShouldNotGetConnectionString()
    {
      //---------------Set up test pack-------------------
      var connectionStringProvider = Substitute.For<IConnectionStringProvider>();

      using (var databaseContext = (FakeSqlDatabaseContext) CreateDatabaseContext(connectionStringProvider, useFalseContext: true))
      {
        var _ = databaseContext.InternalDbConnection;
        //---------------Assert Precondition----------------
        //---------------Execute Test ----------------------
        var dbConnection = databaseContext.InternalDbConnection;
        //---------------Test Result -----------------------
        dbConnection.Should().NotBeNull();
        connectionStringProvider.Received(1).GetConnectionString(Arg.Any<string>());
      }
    }

    [Test]
    public async Task OpenAsync_GivenDbConnectionClosed_ShouldCallOpenOnDbConnection()
    {
      //---------------Set up test pack-------------------
      var dbConnection               = Substitute.For<IDbConnection>();
      var databaseConnectionProvider = Substitute.For<IDatabaseConnectionProvider>();
      databaseConnectionProvider.GetConnection(Arg.Any<string>()).Returns(dbConnection);

      var dbContext = CreateDatabaseContext(databaseConnectionProvider: databaseConnectionProvider);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      await dbContext.OpenAsync();
      //---------------Test Result -----------------------
      dbConnection.Received(1).Open();;
    }

    [Test]
    public async Task OpenAsync_GivenDbConnectionOpen_ShouldNotCallOpenOnDbConnection()
    {
      //---------------Set up test pack-------------------
      var dbConnection = Substitute.For<IDbConnection>();
      dbConnection.State.Returns(ConnectionState.Open);

      var databaseConnectionProvider = Substitute.For<IDatabaseConnectionProvider>();
      databaseConnectionProvider.GetConnection(Arg.Any<string>()).Returns(dbConnection);

      var dbContext = CreateDatabaseContext(databaseConnectionProvider: databaseConnectionProvider);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      await dbContext.OpenAsync();
      //---------------Test Result -----------------------
      dbConnection.DidNotReceive().Open();;
    }

    [Test]
    public async Task CloseAsync_GivenDbConnectionClosed_ShouldNotCallCloseOnDbConnection()
    {
      //---------------Set up test pack-------------------
      var dbConnection = Substitute.For<IDbConnection>();
      dbConnection.State.Returns(ConnectionState.Closed);

      var databaseConnectionProvider = Substitute.For<IDatabaseConnectionProvider>();
      databaseConnectionProvider.GetConnection(Arg.Any<string>()).Returns(dbConnection);

      var dbContext = CreateDatabaseContext(databaseConnectionProvider: databaseConnectionProvider);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      await dbContext.CloseAsync();
      //---------------Test Result -----------------------
      dbConnection.DidNotReceive().Close();
    }

    [Test]
    public async Task CloseAsync_GivenDbConnectionOpen_ShouldCallCloseOnDbConnection()
    {
      //---------------Set up test pack-------------------
      var dbConnection = Substitute.For<IDbConnection>();
      dbConnection.State.Returns(ConnectionState.Open);

      var databaseConnectionProvider = Substitute.For<IDatabaseConnectionProvider>();
      databaseConnectionProvider.GetConnection(Arg.Any<string>()).Returns(dbConnection);

      var dbContext = CreateDatabaseContext(databaseConnectionProvider: databaseConnectionProvider);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      await dbContext.CloseAsync();
      //---------------Test Result -----------------------
      dbConnection.Received(1).Close();
    }

    [TestCase(DbContextAction.Retrieve, CommandType.Text)]
    [TestCase(DbContextAction.Create, CommandType.Text)]
    [TestCase(DbContextAction.Update, CommandType.Text)]
    [TestCase(DbContextAction.Delete, CommandType.Text)]
    [TestCase(DbContextAction.SqlStatement, CommandType.Text)]
    [TestCase(DbContextAction.StoredProcedure, CommandType.StoredProcedure)]
    public async Task ExecuteActionAsync_GivenContextAction_ShouldSetCommandTextAsExpected(DbContextAction contextAction, CommandType expectedCommandType)
    {
      //---------------Set up test pack-------------------
      var dbConnection = Substitute.For<IDbConnection>();
      var dbCommand    = Substitute.For<IDbCommand>();
      dbConnection.CreateCommand().Returns(dbCommand);

      var databaseConnectionProvider = Substitute.For<IDatabaseConnectionProvider>();
      databaseConnectionProvider.GetConnection(Arg.Any<string>()).Returns(dbConnection);

      var dbContext = CreateDatabaseContext(databaseConnectionProvider: databaseConnectionProvider);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      await dbContext.ExecuteActionAsync(contextAction, new FakeTestDataModel());
      //---------------Test Result -----------------------
      dbCommand.Received(1).CommandType = expectedCommandType;
    }

    [Test]
    public async Task ExecuteActionAsync_GivenRetrieveAction_ShouldCallBuildSelectStatement()
    {
      //---------------Set up test pack-------------------
      var dataModel              = new FakeTestDataModel();
      var statementBuildProvider = Substitute.For<IStatementBuildProvider>();
      var dbContext              = CreateDatabaseContext(sqlStatementBuildProvider: statementBuildProvider);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      await dbContext.ExecuteActionAsync(DbContextAction.Retrieve, dataModel);
      //---------------Test Result -----------------------
      statementBuildProvider.Received(1).BuildSelectStatement(dataModel);
    }

    [Test]
    public async Task ExecuteActionAsync_GivenRetrieveAction_ShouldCallExecuteReader()
    {
      //---------------Set up test pack-------------------
      var dataModel = new FakeTestDataModel { Id = Guid.NewGuid() };

      var dbCommand    = Substitute.For<IDbCommand>();
      var dbConnection = Substitute.For<IDbConnection>();
      dbConnection.CreateCommand().Returns(dbCommand);

      var databaseConnectionProvider = Substitute.For<IDatabaseConnectionProvider>();
      databaseConnectionProvider.GetConnection(Arg.Any<string>()).Returns(dbConnection);

      var dbContext = CreateDatabaseContext(databaseConnectionProvider: databaseConnectionProvider);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var actionResult = await dbContext.ExecuteActionAsync(DbContextAction.Retrieve, dataModel);
      //---------------Test Result -----------------------
      dbCommand.Received(1).ExecuteReader();
    }

    [Test]
    public async Task ExecuteActionAsync_GivenRetrieveAction_And_NoData_ShouldMapReturnedEmptyList()
    {
      //---------------Set up test pack-------------------
      var dataModel      = new FakeTestDataModel { Id = Guid.NewGuid() };
      var dataReaderData = new List<FakeTestDataModel>();

      var fakeDataReader = TestHelper.CreateSubstituteDataReader(dataReaderData);
      var dbCommand      = Substitute.For<IDbCommand>();
      dbCommand.ExecuteReader().Returns(fakeDataReader);

      var dbConnection = Substitute.For<IDbConnection>();
      dbConnection.CreateCommand().Returns(dbCommand);

      var databaseConnectionProvider = Substitute.For<IDatabaseConnectionProvider>();
      databaseConnectionProvider.GetConnection(Arg.Any<string>()).Returns(dbConnection);

      var dbContext = CreateDatabaseContext(databaseConnectionProvider: databaseConnectionProvider);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var actionResult = await dbContext.ExecuteActionAsync(DbContextAction.Retrieve, dataModel);
      //---------------Test Result -----------------------
      actionResult.ActionResult.Should().Be(DbContextActionResult.Success);
      actionResult.Results.Any().Should().BeFalse();
    }

    [Test]
    public async Task ExecuteActionAsync_GivenRetrieveAction_And_DataFound_ShouldMapReturnedData()
    {
      //---------------Set up test pack-------------------
      var dataModel      = new FakeTestDataModel { Id = Guid.NewGuid() };
      var dataReaderData = new List<FakeTestDataModel>
                             {
                               CreateRandomTestDataModel(), 
                               CreateRandomTestDataModel(dataModel.Id), 
                               CreateRandomTestDataModel()
                             };

      var fakeDataReader = TestHelper.CreateSubstituteDataReader(dataReaderData);
      var dbCommand      = Substitute.For<IDbCommand>();
      dbCommand.ExecuteReader().Returns(fakeDataReader);

      var dbConnection = Substitute.For<IDbConnection>();
      dbConnection.CreateCommand().Returns(dbCommand);

      var databaseConnectionProvider = Substitute.For<IDatabaseConnectionProvider>();
      databaseConnectionProvider.GetConnection(Arg.Any<string>()).Returns(dbConnection);

      var dbContext = CreateDatabaseContext(databaseConnectionProvider: databaseConnectionProvider);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var actionResult = await dbContext.ExecuteActionAsync(DbContextAction.Retrieve, dataModel);
      //---------------Test Result -----------------------
      actionResult.ActionResult.Should().Be(DbContextActionResult.Success);
      actionResult.Results.Any().Should().BeTrue();
      actionResult.Results.Should().Contain(model => model.Id.Equals(dataModel.Id));
    }

    [Test]
    public async Task ExecuteActionAsync_GivenCreateAction_ShouldCallBuildInsertStatement()
    {
      //---------------Set up test pack-------------------
      var dataModel = new FakeTestDataModel();
      var statementBuildProvider = Substitute.For<IStatementBuildProvider>();
      var dbContext = CreateDatabaseContext(sqlStatementBuildProvider: statementBuildProvider);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      await dbContext.ExecuteActionAsync(DbContextAction.Create, dataModel);
      //---------------Test Result -----------------------
      statementBuildProvider.Received(1).BuildInsertStatement(dataModel);
    }

    [Test]
    public async Task ExecuteActionAsync_GivenCreateAction_ShouldCallExecuteNonQuery()
    {
      //---------------Set up test pack-------------------
      var dataModel = new FakeTestDataModel { Id = Guid.NewGuid() };

      var dbCommand    = Substitute.For<IDbCommand>();
      var dbConnection = Substitute.For<IDbConnection>();
      dbConnection.CreateCommand().Returns(dbCommand);

      var databaseConnectionProvider = Substitute.For<IDatabaseConnectionProvider>();
      databaseConnectionProvider.GetConnection(Arg.Any<string>()).Returns(dbConnection);

      var dbContext = CreateDatabaseContext(databaseConnectionProvider: databaseConnectionProvider);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var _ = await dbContext.ExecuteActionAsync(DbContextAction.Create, dataModel);
      //---------------Test Result -----------------------
      dbCommand.Received(1).ExecuteNonQuery();
    }

    [Test]
    public async Task ExecuteActionAsync_GivenClosedConnection_ShouldCallOpenAndCloseOnDbConnection()
    {
      //---------------Set up test pack-------------------
      var dataModel = new FakeTestDataModel { Id = Guid.NewGuid() };

      var dbConnectionState = ConnectionState.Closed;
      var dbConnection = Substitute.For<IDbConnection>();
      dbConnection.State.Returns(info => dbConnectionState);
      dbConnection.When(connection => connection.Open())
                  .Do(info => dbConnectionState = ConnectionState.Open);

      var databaseConnectionProvider = Substitute.For<IDatabaseConnectionProvider>();
      databaseConnectionProvider.GetConnection(Arg.Any<string>()).Returns(dbConnection);

      var dbContext = CreateDatabaseContext(databaseConnectionProvider: databaseConnectionProvider);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var _ = await dbContext.ExecuteActionAsync(DbContextAction.Create, dataModel);
      //---------------Test Result -----------------------
      dbConnection.Received(1).Open();
      dbConnection.Received(1).Close();
    }

    [Test]
    public async Task ExecuteActionAsync_GivenOpenConnection_ShouldNotCallCloseOnDbConnection()
    {
      //---------------Set up test pack-------------------
      var dataModel = new FakeTestDataModel { Id = Guid.NewGuid() };

      var dbCommand    = Substitute.For<IDbCommand>();
      var dbConnection = Substitute.For<IDbConnection>();
      dbConnection.State.Returns(ConnectionState.Open);
      dbConnection.CreateCommand().Returns(dbCommand);

      var databaseConnectionProvider = Substitute.For<IDatabaseConnectionProvider>();
      databaseConnectionProvider.GetConnection(Arg.Any<string>()).Returns(dbConnection);

      var dbContext = CreateDatabaseContext(databaseConnectionProvider: databaseConnectionProvider);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var _ = await dbContext.ExecuteActionAsync(DbContextAction.Create, dataModel);
      //---------------Test Result -----------------------
      dbConnection.DidNotReceive().Close();
    }

    [Test]
    public void Complete_ShouldCallCompleteOnDatabaseTransactionProvider()
    {
      //---------------Set up test pack-------------------
      var databaseTransactionProvider = Substitute.For<IDatabaseTransactionProvider>();
      var dbContext                   = CreateDatabaseContext(databaseTransactionProvider: databaseTransactionProvider);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      dbContext.Complete();
      //---------------Test Result -----------------------
      databaseTransactionProvider.Received(1).Complete();
    }

    private IDatabaseContext CreateDatabaseContext(IConnectionStringProvider connectionStringProvider = null,
                                                   IDatabaseConnectionProvider databaseConnectionProvider = null,
                                                   IDatabaseTransactionProvider databaseTransactionProvider = null,
                                                   IStatementBuildProvider sqlStatementBuildProvider = null,
                                                   IDataModelPopulateProvider dataModelPopulateProvider = null,
                                                   bool useFalseContext = false)
    {
      connectionStringProvider    ??= Substitute.For<IConnectionStringProvider>();
      databaseConnectionProvider  ??= Substitute.For<IDatabaseConnectionProvider>();
      databaseTransactionProvider ??= Substitute.For<IDatabaseTransactionProvider>();
      sqlStatementBuildProvider   ??= Substitute.For<IStatementBuildProvider>();
      dataModelPopulateProvider   ??= new SqlDataModelPopulateProvider();

      return useFalseContext
               ? new FakeSqlDatabaseContext(connectionStringProvider, databaseConnectionProvider, databaseTransactionProvider, sqlStatementBuildProvider, dataModelPopulateProvider)
               : new SqlDatabaseContext(connectionStringProvider, databaseConnectionProvider, databaseTransactionProvider, sqlStatementBuildProvider, dataModelPopulateProvider);
    }

    private FakeTestDataModel CreateRandomTestDataModel(Guid dataModelId = default)
    {
      return new FakeTestDataModel
               {
                 Id        = dataModelId == Guid.Empty ? Guid.NewGuid() : dataModelId,
                 Name      = RandomValueGenerator.CreateRandomString(20, 40),
                 Date      = RandomValueGenerator.CreateRandomDate(),
                 SomeField = RandomValueGenerator.CreateRandomString(30, 60)
               };
    }
  }
}
