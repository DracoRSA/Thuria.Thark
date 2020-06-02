using System;

using NSubstitute;
using NUnit.Framework;
using FluentAssertions;

using Thuria.Calot.TestUtilities;
using Thuria.Thark.Core.Providers;
using Thuria.Thark.Core.DataAccess;
using Thuria.Thark.DataAccess.Builders;

namespace Thuria.Thark.DataAccess.Tests.Builders
{
  [TestFixture]
  public class TestSqlDatabaseContextBuilder
  {
    [Test]
    public void Create_ShouldReturnInstanceOfDatabaseBuilder()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var databaseBuilder = SqlDatabaseContextBuilder.Create;
      //---------------Test Result -----------------------
      databaseBuilder.Should().NotBeNull();
      databaseBuilder.Should().BeAssignableTo<IDatabaseContextBuilder>();
      databaseBuilder.Should().BeOfType<SqlDatabaseContextBuilder>();
    }

    [Test]
    public void WithConnectionStringProvider_GivenNullProvider_ShouldThrowArgumentNullException()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var exception = Assert.Throws<ArgumentNullException>(() => SqlDatabaseContextBuilder.Create.WithConnectionStringProvider(null));
      //---------------Test Result -----------------------
      exception.Should().NotBeNull();
      exception.ParamName.Should().Be("connectionStringProvider");
    }

    [Test]
    public void WithConnectionStringProvider_GivenProvider_ShouldBuildContextWithProvider()
    {
      //---------------Set up test pack-------------------
      var connectionStringProvider = Substitute.For<IConnectionStringProvider>();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var databaseContextBuilder = FakeSqlDatabaseContextBuilder.Create
                                                                .WithConnectionStringProvider(connectionStringProvider);
      //---------------Test Result -----------------------
      databaseContextBuilder.Should().NotBeNull();
      ((FakeSqlDatabaseContextBuilder) databaseContextBuilder).InternalConnectionStringProvider.Should().Be(connectionStringProvider);
    }

    [Test]
    public void WithDatabaseConnectionProvider_GivenNullProvider_ShouldThrowArgumentNullException()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var exception = Assert.Throws<ArgumentNullException>(() => SqlDatabaseContextBuilder.Create.WithDatabaseConnectionProvider(null));
      //---------------Test Result -----------------------
      exception.Should().NotBeNull();
      exception.ParamName.Should().Be("databaseConnectionProvider");
    }

    [Test]
    public void WithDatabaseConnectionProvider_GivenProvider_ShouldBuildContextWithProvider()
    {
      //---------------Set up test pack-------------------
      var databaseConnectionProvider = Substitute.For<IDatabaseConnectionProvider>();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var databaseContextBuilder = FakeSqlDatabaseContextBuilder.Create
                                                                .WithDatabaseConnectionProvider(databaseConnectionProvider);
      //---------------Test Result -----------------------
      databaseContextBuilder.Should().NotBeNull();
      ((FakeSqlDatabaseContextBuilder) databaseContextBuilder).InternalDatabaseConnectionProvider.Should().Be(databaseConnectionProvider);
    }

    [Test]
    public void WithDatabaseTransactionProvider_GivenNullProvider_ShouldThrowArgumentNullException()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var exception = Assert.Throws<ArgumentNullException>(() => SqlDatabaseContextBuilder.Create.WithDatabaseTransactionProvider(null));
      //---------------Test Result -----------------------
      exception.Should().NotBeNull();
      exception.ParamName.Should().Be("databaseTransactionProvider");
    }

    [Test]
    public void WithDatabaseTransactionProvider_GivenProvider_ShouldBuildContextWithProvider()
    {
      //---------------Set up test pack-------------------
      var databaseTransactionProvider = Substitute.For<IDatabaseTransactionProvider>();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var databaseContextBuilder = FakeSqlDatabaseContextBuilder.Create
                                                                .WithDatabaseTransactionProvider(databaseTransactionProvider);
      //---------------Test Result -----------------------
      databaseContextBuilder.Should().NotBeNull();
      ((FakeSqlDatabaseContextBuilder) databaseContextBuilder).InternalDatabaseTransactionProvider.Should().Be(databaseTransactionProvider);
    }

    [Test]
    public void WithStatementBuildProvider_GivenNullProvider_ShouldThrowArgumentNullException()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var exception = Assert.Throws<ArgumentNullException>(() => SqlDatabaseContextBuilder.Create.WithStatementBuildProvider(null));
      //---------------Test Result -----------------------
      exception.Should().NotBeNull();
      exception.ParamName.Should().Be("statementBuildProvider");
    }

    [Test]
    public void WithStatementBuildProvider_GivenProvider_ShouldBuildContextWithProvider()
    {
      //---------------Set up test pack-------------------
      var statementBuildProvider = Substitute.For<IStatementBuildProvider>();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var databaseContextBuilder = FakeSqlDatabaseContextBuilder.Create
                                                                .WithStatementBuildProvider(statementBuildProvider);
      //---------------Test Result -----------------------
      databaseContextBuilder.Should().NotBeNull();
      ((FakeSqlDatabaseContextBuilder) databaseContextBuilder).InternalStatementBuildProvider.Should().Be(statementBuildProvider);
    }

    [Test]
    public void WithDataModelPopulateProvider_GivenNullProvider_ShouldThrowArgumentNullException()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var exception = Assert.Throws<ArgumentNullException>(() => SqlDatabaseContextBuilder.Create.WithDataModelPopulateProvider(null));
      //---------------Test Result -----------------------
      exception.Should().NotBeNull();
      exception.ParamName.Should().Be("dataModelPopulateProvider");
    }

    [Test]
    public void WithDataModelPopulateProvider_GivenProvider_ShouldBuildContextWithProvider()
    {
      //---------------Set up test pack-------------------
      var dataModelPopulateProvider = Substitute.For<IDataModelPopulateProvider>();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var databaseContextBuilder = FakeSqlDatabaseContextBuilder.Create
                                                                .WithDataModelPopulateProvider(dataModelPopulateProvider);
      //---------------Test Result -----------------------
      databaseContextBuilder.Should().NotBeNull();
      ((FakeSqlDatabaseContextBuilder) databaseContextBuilder).InternalDataModelPopulateProvider.Should().Be(dataModelPopulateProvider);
    }

    [Test]
    public void WithCommandTimeout_GivenProvider_ShouldBuildContextWithProvider()
    {
      //---------------Set up test pack-------------------
      var commandTimeout = RandomValueGenerator.CreateRandomInt(100, 1000);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var databaseContext = SqlDatabaseContextBuilder.Create
                                                     .WithConnectionStringProvider(Substitute.For<IConnectionStringProvider>())
                                                     .WithDatabaseConnectionProvider(Substitute.For<IDatabaseConnectionProvider>())
                                                     .WithStatementBuildProvider(Substitute.For<IStatementBuildProvider>())
                                                     .WithDataModelPopulateProvider(Substitute.For<IDataModelPopulateProvider>())
                                                     .WithCommandTimeout(commandTimeout)
                                                     .Build();
      //---------------Test Result -----------------------
      databaseContext.Should().NotBeNull();
      databaseContext.CommandTimeout.Should().Be(commandTimeout);
    }

    [Test]
    public void WithDatabaseContextName_GivenName_ShouldBuildContextWithDatabaseContextName()
    {
      //---------------Set up test pack-------------------
      var dbContextName = RandomValueGenerator.CreateRandomString(10, 30);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var databaseContext = SqlDatabaseContextBuilder.Create
                                                     .WithConnectionStringProvider(Substitute.For<IConnectionStringProvider>())
                                                     .WithDatabaseConnectionProvider(Substitute.For<IDatabaseConnectionProvider>())
                                                     .WithStatementBuildProvider(Substitute.For<IStatementBuildProvider>())
                                                     .WithDataModelPopulateProvider(Substitute.For<IDataModelPopulateProvider>())
                                                     .WithDatabaseContextName(dbContextName)
                                                     .Build();
      //---------------Test Result -----------------------
      databaseContext.Should().NotBeNull();
      databaseContext.DbContextName.Should().Be(dbContextName);
    }

    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    public void WithDatabaseContextName_GivenNullOrEmpty_ShouldThrowArgumentNullException(string dbContextName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var exception = Assert.Throws<ArgumentNullException>(() => SqlDatabaseContextBuilder.Create
                                                                                          .WithConnectionStringProvider(Substitute.For<IConnectionStringProvider>())
                                                                                          .WithDatabaseConnectionProvider(Substitute.For<IDatabaseConnectionProvider>())
                                                                                          .WithStatementBuildProvider(Substitute.For<IStatementBuildProvider>())
                                                                                          .WithDataModelPopulateProvider(Substitute.For<IDataModelPopulateProvider>())
                                                                                          .WithDatabaseContextName(dbContextName)
                                                                                          .Build());
      //---------------Test Result -----------------------
      exception.ParamName.Should().Be("dbContextName");
    }

    [Test]
    public void Build_GivenNoConnectionStringProvider_ShouldFailToBuild()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var exception = Assert.Throws<ArgumentException>(() => SqlDatabaseContextBuilder.Create
                                                                                      .WithDatabaseConnectionProvider(Substitute.For<IDatabaseConnectionProvider>())
                                                                                      .WithStatementBuildProvider(Substitute.For<IStatementBuildProvider>())
                                                                                      .WithDataModelPopulateProvider(Substitute.For<IDataModelPopulateProvider>())
                                                                                      .Build());
      //---------------Test Result -----------------------
      exception.Message.Should().Contain("Connection String Provider must be specified");
      exception.ParamName.Should().Be("ConnectionStringProvider");
    }
    
    [Test]
    public void Build_GivenNoDatabaseConnectionProvider_ShouldFailToBuild()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var exception = Assert.Throws<ArgumentException>(() => SqlDatabaseContextBuilder.Create
                                                                                      .WithConnectionStringProvider(Substitute.For<IConnectionStringProvider>())
                                                                                      .WithStatementBuildProvider(Substitute.For<IStatementBuildProvider>())
                                                                                      .WithDataModelPopulateProvider(Substitute.For<IDataModelPopulateProvider>())
                                                                                      .Build());
      //---------------Test Result -----------------------
      exception.Message.Should().Contain("Connection Provider must be specified");
      exception.ParamName.Should().Be("DatabaseConnectionProvider");
    }
    
    [Test]
    public void Build_GivenNoStatementBuildProvider_ShouldFailToBuild()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var exception = Assert.Throws<ArgumentException>(() => SqlDatabaseContextBuilder.Create
                                                                                      .WithConnectionStringProvider(Substitute.For<IConnectionStringProvider>())
                                                                                      .WithDatabaseConnectionProvider(Substitute.For<IDatabaseConnectionProvider>())
                                                                                      .WithDataModelPopulateProvider(Substitute.For<IDataModelPopulateProvider>())
                                                                                      .Build());
      //---------------Test Result -----------------------
      exception.Message.Should().Contain("Statement Build Provider must be specified");
      exception.ParamName.Should().Be("StatementBuildProvider");
    }
    
    [Test]
    public void Build_GivenNoDataModelPopulateProvider_ShouldFailToBuild()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var exception = Assert.Throws<ArgumentException>(() => SqlDatabaseContextBuilder.Create
                                                                                      .WithConnectionStringProvider(Substitute.For<IConnectionStringProvider>())
                                                                                      .WithDatabaseConnectionProvider(Substitute.For<IDatabaseConnectionProvider>())
                                                                                      .WithStatementBuildProvider(Substitute.For<IStatementBuildProvider>())
                                                                                      .Build());
      //---------------Test Result -----------------------
      exception.Message.Should().Contain("Data Model Populate Provider must be specified");
      exception.ParamName.Should().Be("DataModelPopulateProvider");
    }
    
    [Test]
    public void Build_WithConnectionStringProvider_ShouldBuildConnectionWithConnectionString()
    {
      //---------------Set up test pack-------------------
      var connectionString         = RandomValueGenerator.CreateRandomString(20, 30);
      var connectionStringProvider = Substitute.For<IConnectionStringProvider>();
      connectionStringProvider.GetConnectionString(Arg.Any<string>())
                              .Returns(connectionString);

      var dbConnectionProvider = Substitute.For<IDatabaseConnectionProvider>();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var databaseContext = SqlDatabaseContextBuilder.Create
                                                     .WithConnectionStringProvider(connectionStringProvider)
                                                     .WithDatabaseConnectionProvider(dbConnectionProvider)
                                                     .WithStatementBuildProvider(Substitute.For<IStatementBuildProvider>())
                                                     .WithDataModelPopulateProvider(Substitute.For<IDataModelPopulateProvider>())
                                                     .Build();
      //---------------Test Result -----------------------
      databaseContext.ExecuteActionAsync(DbContextAction.Retrieve, new object());
      dbConnectionProvider.Received(1).GetConnection(Arg.Any<string>());
    }
  }
}
