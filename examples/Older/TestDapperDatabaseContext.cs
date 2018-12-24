using System.Data;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using FluentAssertions;
using Dapper;
using MGS.Casino.Veyron.TestUtilities;
using MGS.Casino.Veyron.DataAccessInterfaces;
using NSubstitute;
using System.Collections.Generic;

namespace MGS.Casino.Veyron.DataAccess.Tests
{
  [TestFixture]
  public class TestDapperDatabaseContext
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      var dbConnection             = new Mock<IVeyronDbConnection>();
      var dbContextProvider        = new Mock<IDatabaseContextProvider>();
      var inputParametersProvider  = new Mock<IDatabaseContextInputParametersProvider>();
      var outputParametersProvider = new Mock<IDatabaseContextOutputParametersProvider>();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var dbContext = new DapperDatabaseContext("connectionString", dbConnection.Object, dbContextProvider.Object, 
                                                inputParametersProvider.Object, outputParametersProvider.Object);
      //---------------Test Result -----------------------
      dbContext.Should().NotBeNull();
    }
    
    [TestCase("connectionString")]
    [TestCase("dbConnection")]
    [TestCase("inputParametersProvider")]
    [TestCase("outputParametersProvider")]
    public void Constructor_GivenNullParameter_ShouldThrowException(string parameterName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      ConstructorTestHelper.ValidateArgumentNullExceptionIfParameterIsNull<DapperDatabaseContext>(parameterName);
      //---------------Test Result -----------------------
    }
    
    [TestCase("connectionString", "ConnectionString")]
    [TestCase("dbConnection", "DbConnection")]
    public void Constructor_GivenParameter_ShouldSetExpectedProperty(string parameterName, string propertyName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      ConstructorTestHelper.ValidatePropertySetWithParameter<DapperDatabaseContext>(parameterName, propertyName);
      //---------------Test Result -----------------------
    }
    
    [TestCase("CommandTimeout")]
    public void Properties_GivenValue_ShouldSetPropertyValue(string propertyName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      PropertyTestHelper.ValidateGetAndSet<DapperDatabaseContext>(propertyName);
      //---------------Test Result -----------------------
    }
    
    [Test]
    public void Dispose_ShouldCallUnRegisterOnIDatabaseContextProvider()
    {
      //---------------Set up test pack-------------------
      var dbConnection      = new Mock<IVeyronDbConnection>();
      var dbContextProvider = new Mock<IDatabaseContextProvider>();
      var connectionString  = "connectionString";
      var dbContext         = new DapperDatabaseContext(connectionString, dbConnection.Object, dbContextProvider.Object, 
                                                  new DapperDatabaseContextInputParametersProvider(), new DapperDatabaseContextOutputParametersProvider());
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      Assert.DoesNotThrow(() => dbContext.Dispose());
      //---------------Test Result -----------------------
      dbContextProvider.Verify(provider => provider.UnRegister(connectionString), Times.Once);
    }

    private IDatabaseContext CreateDatabaseContext(string connectionString = "connectionString",  
                                                   IVeyronDbConnection veyronDbConnection = null, 
                                                   IDatabaseContextProvider databaseContextProvider = null,
                                                   int commandTimeout = 30)
    {
      var dbConnection      =  veyronDbConnection ?? new Mock<IVeyronDbConnection>().Object;
      var dbContextProvider = databaseContextProvider ?? new DatabaseContextProvider();

      return new DapperDatabaseContext(connectionString, dbConnection, dbContextProvider, 
                                       new DapperDatabaseContextInputParametersProvider(), 
                                       new DapperDatabaseContextOutputParametersProvider())
        {
          CommandTimeout = commandTimeout
        };
    }

    private Mock<IVeyronDbConnection> CreateMockDbConnection(string connectionString)
    {
      var dbConnection = new Mock<IDbConnection>();
      var dbCommand = new Mock<IDbCommand>();

      dbCommand.Setup(command => command.Connection).Returns(dbConnection.Object);

      dbConnection.Setup(connection => connection.ConnectionString).Returns(connectionString);
      dbConnection.Setup(connection => connection.CreateCommand()).Returns(dbCommand.Object);

      var veyronDbConnection = new Mock<IVeyronDbConnection>();
      veyronDbConnection.Setup(connection => connection.Connection).Returns(dbConnection.Object);

      return veyronDbConnection;
    }
  }
}
