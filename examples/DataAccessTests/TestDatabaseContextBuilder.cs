using System;
using System.Data;
using Moq;
using NUnit.Framework;
using FluentAssertions;
using MGS.Casino.Veyron.TestUtilities;
using MGS.Casino.Veyron.DataAccessInterfaces;

namespace MGS.Casino.Veyron.DataAccess.Tests
{
  [TestFixture]
  public class TestDatabaseContextBuilder
  {
    [Test]
    public void Create_ShouldCreateIDatabaseContextBuilder()
    {
      //---------------Set up test pack-------------------
      var connectionProvider             = new Mock<IDbConnectionProvider>();
      var dbConnectionParametersProvider = new Mock<IDbConnectionParameterProvider>();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var dbBuilder = new DatabaseContextBuilder(connectionProvider.Object, dbConnectionParametersProvider.Object);
      //---------------Test Result -----------------------
      dbBuilder.Should().NotBeNull();
      dbBuilder.Should().BeAssignableTo<IDatabaseContextBuilder>();
    }
    
    [TestCase("dbConnectionProvider")]
    public void Constructor_GivenNullParameter_ShouldThrowException(string parameterName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      ConstructorTestHelper.ValidateArgumentNullExceptionIfParameterIsNull<DatabaseContextBuilder>(parameterName);
      //---------------Test Result -----------------------
    }
    
    [Test]
    public void WithConnectionString_GivenEmptyString_ShouldThrowException()
    {
      //---------------Set up test pack-------------------
      var databaseContextBuilder = CreateDatabaseContextBuilder();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var exception = Assert.Throws<ArgumentNullException>(() => databaseContextBuilder.WithConnectionString(null));
      //---------------Test Result -----------------------
      exception.ParamName.Should().Be("connectionString");
    }
    
    [Test]
    public void WithConnectionString_GivenValidConnectionString_ShouldUseConnectionStringWhenCreatingDbConnection()
    {
      //---------------Set up test pack-------------------
      var connectionString       = "testConnectionString";
      var dbConnectionProvider   = CreateDBConnectionProvider(connectionString);
      var databaseContextBuilder = CreateDatabaseContextBuilder(dbConnectionProvider: dbConnectionProvider.Object);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      Assert.DoesNotThrowAsync(() => databaseContextBuilder.WithConnectionString(connectionString).BuildAsync());
      //---------------Test Result -----------------------
      dbConnectionProvider.Verify(provider => provider.CreateDbConnectionAsync(connectionString), Times.Once);
    }
    
    [Test]
    public void WithConnectionString_ShouldReturnInstanceOfBuilder()
    {
      //---------------Set up test pack-------------------
      var databaseContextBuilder = CreateDatabaseContextBuilder();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var returnBuilder = databaseContextBuilder.WithConnectionString("TestConnectionString");
      //---------------Test Result -----------------------
      returnBuilder.Should().NotBeNull();
      returnBuilder.Should().Be(databaseContextBuilder);
      returnBuilder.Should().BeOfType<DatabaseContextBuilder>();
    }
    
    [Test]
    public void WithTransaction_ShouldUseCreateTransactionWhenBuild()
    {
      //---------------Set up test pack-------------------
      var connectionString = "connectionString";
      var isolationLevel   = IsolationLevel.ReadCommitted;

      var dbConnection         = new Mock<IVeyronDbConnection>();
      var dbConnectionProvider = new Mock<IDbConnectionProvider>();
      dbConnectionProvider.Setup(provider => provider.CreateDbConnectionAsync(connectionString))
                          .ReturnsAsync(dbConnection.Object);

      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var databaseContext = new DatabaseContextBuilder(dbConnectionProvider.Object, new SqlParameterProvider())
                                        .WithConnectionString(connectionString)
                                        .WithTransaction(isolationLevel)
                                        .BuildAsync().Result;
      //---------------Test Result -----------------------
      databaseContext.Should().NotBeNull();
      dbConnection.Verify(connection => connection.BeginTransaction(isolationLevel), Times.Once);
    }
    
    [Test]
    public void WithTransactionManager_ShouldReturnInstanceOfBuilder()
    {
      //---------------Set up test pack-------------------
      var dbConnectionProvider = new Mock<IDbConnectionProvider>();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var contextBuilder = new DatabaseContextBuilder(dbConnectionProvider.Object, new SqlParameterProvider())
                                        .WithTransaction(IsolationLevel.ReadCommitted);
      //---------------Test Result -----------------------
      contextBuilder.Should().NotBeNull();
      contextBuilder.Should().BeOfType<DatabaseContextBuilder>();
    }
    
    [Test]
    public void WithCommandTimeout_GivenTimeoutValue_ShouldUseTimeoutWhenBuild()
    {
      //---------------Set up test pack-------------------
      var commandTimeout       = 60;
      var connectionString     = "connectionString";
      var dbConnectionProvider = CreateDBConnectionProvider(connectionString);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var databaseContext = new DatabaseContextBuilder(dbConnectionProvider.Object, new SqlParameterProvider())
                                        .WithConnectionString(connectionString)
                                        .WithCommandTimeout(commandTimeout)
                                        .BuildAsync().Result;
      //---------------Test Result -----------------------
      databaseContext.Should().NotBeNull();
      databaseContext.CommandTimeout.Should().Be(commandTimeout);
    }
    
    [Test]
    public void WithCommandTimeout_ShouldReturnInstanceOfBuilder()
    {
      //---------------Set up test pack-------------------
      var dbConnectionProvider = new Mock<IDbConnectionProvider>();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var contextBuilder = new DatabaseContextBuilder(dbConnectionProvider.Object, new SqlParameterProvider())
                                    .WithCommandTimeout(60);
      //---------------Test Result -----------------------
      contextBuilder.Should().NotBeNull();
      contextBuilder.Should().BeOfType<DatabaseContextBuilder>();
    }
    
    [Test]
    public void Build_GivenNoConnectionString_ShouldThrowException()
    {
      //---------------Set up test pack-------------------
      var dbContextBuilder = CreateDatabaseContextBuilder();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var exception = Assert.ThrowsAsync<Exception>(() => dbContextBuilder.BuildAsync());
      //---------------Test Result -----------------------
      exception.Message.Should().Contain("No Database Connection String specified");
    }
    
    private Mock<IDbConnectionProvider> CreateDBConnectionProvider(string connectionString, IVeyronDbConnection dbConnection = null)
    {
      var dbConnectionProvider = new Mock<IDbConnectionProvider>();
      if (dbConnection == null)
      {
        dbConnection = CreateMockDbConnection().Object;
      }
    
      dbConnectionProvider.Setup(provider => provider.CreateDbConnectionAsync(connectionString))
                          .ReturnsAsync(dbConnection);
    
      return dbConnectionProvider;
    }
    
    private Mock<IVeyronDbConnection> CreateMockDbConnection()
    {
      var mockDbConnection = new Mock<IVeyronDbConnection>();
      mockDbConnection.Setup(connection => connection.BeginTransaction(It.IsAny<IsolationLevel>()))
                      .Returns(new Mock<IDbTransaction>().Object);
      return mockDbConnection;
    }
    
    private IDatabaseContextBuilder CreateDatabaseContextBuilder(IDbConnectionProvider dbConnectionProvider = null)
    {
      var connectionProvider = dbConnectionProvider ?? new Mock<IDbConnectionProvider>().Object;
      return new DatabaseContextBuilder(connectionProvider, new SqlParameterProvider());
    }
  }
}
