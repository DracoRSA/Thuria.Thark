using System;
using System.Data;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using FluentAssertions;
using MGS.Casino.Veyron.TestUtilities;
using MGS.Casino.Veyron.DataAccessInterfaces;

namespace MGS.Casino.Veyron.DataAccess.Tests
{
  [TestFixture]
  public class TestDatabaseContextProvider
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var provider = new DatabaseContextProvider();
      //---------------Test Result -----------------------
      provider.Should().NotBeNull();
    }

    [TestCase("Register", "connectionString")]
    [TestCase("Register", "dbConnection")]
    [TestCase("UnRegister", "connectionString")]
    [TestCase("Get", "connectionString")]
    public void Methods_GivenNullParameter_ShouldThrowException(string methodName, string parameterName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      MethodTestHelper.ValidateArgumentNullExceptionIfParameterIsNull<DatabaseContextProvider>(methodName, parameterName);
      //---------------Test Result -----------------------
    }

    [Test]
    public void Get_GivenConnectionStringNotRegistered_ShouldReturnNull()
    {
      //---------------Set up test pack-------------------
      var provider = new DatabaseContextProvider();
      provider.Register("testConnection", new Mock<IVeyronDbConnection>().Object);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var dataModel = provider.Get("connectionString");
      //---------------Test Result -----------------------
      dataModel.Should().BeNull();
    }

    [Test]
    public void Get_GivenConnectionStringIsRegistered_ShouldReturnRegisteredData()
    {
      //---------------Set up test pack-------------------
      var connectionString = "connectionString";
      var provider         = new DatabaseContextProvider();
      var dbConnection     = new Mock<IVeyronDbConnection>();
      provider.Register(connectionString, dbConnection.Object);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var dataModel = provider.Get(connectionString);
      //---------------Test Result -----------------------
      dataModel.Should().NotBeNull();
      dataModel.ConnectionString.Should().Be(connectionString);
      dataModel.DbConnection.Should().Be(dbConnection.Object);
    }

    [Test]
    public void Register_GivenConnectionStringNotRegistered_ShouldRegister()
    {
      //---------------Set up test pack-------------------
      var connectionString = "connectionString";
      var provider         = new DatabaseContextProvider();
      var dbConnection     = new Mock<IVeyronDbConnection>();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      provider.Register(connectionString, dbConnection.Object);
      //---------------Test Result -----------------------
      var dataModel = provider.Get(connectionString);
      dataModel.Should().NotBeNull();
      dataModel.ConnectionString.Should().Be(connectionString);
      dataModel.DbConnection.Should().Be(dbConnection.Object);
    }

    [Test]
    public void Register_GivenConnectionStringAlreadyRegistered_ShouldNotRegister()
    {
      //---------------Set up test pack-------------------
      var connectionString = "connectionString";
      var provider         = new DatabaseContextProvider();
      var dbConnection     = new Mock<IVeyronDbConnection>();
      //---------------Assert Precondition----------------
      provider.Register(connectionString, dbConnection.Object);
      //---------------Execute Test ----------------------
      provider.Register(connectionString, dbConnection.Object);
      //---------------Test Result -----------------------
      var dataModel = provider.Get(connectionString);
      dataModel.Should().NotBeNull();
      dataModel.ConnectionString.Should().Be(connectionString);
      dataModel.DbConnection.Should().Be(dbConnection.Object);
    }

    [Ignore("REASON")] //TODO Johan Dercksen 20 Sep 2018: Ignored Test - REASON
    [Test]
    public void Register_GivenMultipleThreads_ShouldRegisterOneForEachThread()
    {
      //---------------Set up test pack-------------------
      var connectionString = "connectionString";
      var provider         = new DatabaseContextProvider();

      var executionAction = new Action(() => 
        {
          var dbConnection = new Mock<IVeyronDbConnection>();
          provider.Register(connectionString, dbConnection.Object);

          var dataModel = provider.Get(connectionString);
          dataModel.Should().NotBeNull();
          dataModel.ConnectionString.Should().Be(connectionString);
          dataModel.DbConnection.Should().Be(dbConnection.Object);
        });
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      Task.WaitAll(new[] { Task.Factory.StartNew(executionAction), Task.Factory.StartNew(executionAction) }, 5000);
      //---------------Test Result -----------------------
    }

    [Test]
    public void Unregister_GivenConnectionStringIsRegistered_ShouldCloseDbConnectionAndUnregister()
    {
      //---------------Set up test pack-------------------
      var connectionString = "connectionString";
      var provider         = new DatabaseContextProvider();
      var dbConnection     = new Mock<IVeyronDbConnection>();
      provider.Register(connectionString, dbConnection.Object);
      //---------------Assert Precondition----------------
      var dataModel1 = provider.Get(connectionString);
      dataModel1.Should().NotBeNull();
      //---------------Execute Test ----------------------
      provider.UnRegister(connectionString);
      //---------------Test Result -----------------------
      var dataModel2 = provider.Get(connectionString);
      dataModel2.Should().BeNull();
      dbConnection.Verify(connection => connection.Close(), Times.Once);
    }

    [Test]
    public void Unregister_GivenConnectionStringNotRegistered_ShouldDoNothing()
    {
      //---------------Set up test pack-------------------
      var connectionString = "connectionString";
      var provider         = new DatabaseContextProvider();
      var dbConnection     = new Mock<IVeyronDbConnection>();
      //---------------Assert Precondition----------------
      var dataModel1 = provider.Get(connectionString);
      dataModel1.Should().BeNull();
      //---------------Execute Test ----------------------
      provider.UnRegister(connectionString);
      //---------------Test Result -----------------------
      var dataModel2 = provider.Get(connectionString);
      dataModel2.Should().BeNull();
      dbConnection.Verify(connection => connection.Close(), Times.Never);
    }

    [Test]
    public void Unregister_GivenConnectionStringIsRegisteredAndTransactionIsActive_ShouldNotCloseDbConnectionAndNotUnregister()
    {
      //---------------Set up test pack-------------------
      var connectionString = "connectionString";
      var dbConnection     = new Mock<IVeyronDbConnection>();
      var dbTransaction    = new Mock<IDbTransaction>();
      dbTransaction.Setup(transaction => transaction.Connection).Returns(dbConnection.Object);
      dbConnection.Setup(connection => connection.Transaction).Returns(dbTransaction.Object);

      var provider = new DatabaseContextProvider();
      provider.Register(connectionString, dbConnection.Object);
      //---------------Assert Precondition----------------
      var dataModel1 = provider.Get(connectionString);
      dataModel1.Should().NotBeNull();
      //---------------Execute Test ----------------------
      provider.UnRegister(connectionString);
      //---------------Test Result -----------------------
      var dataModel2 = provider.Get(connectionString);
      dataModel2.Should().NotBeNull();
      dbConnection.Verify(connection => connection.Close(), Times.Never);
    }
  }
}
