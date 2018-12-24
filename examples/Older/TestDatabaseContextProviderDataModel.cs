using Moq;
using NUnit.Framework;
using FluentAssertions;
using MGS.Casino.Veyron.TestUtilities;
using MGS.Casino.Veyron.DataAccessInterfaces;

namespace MGS.Casino.Veyron.DataAccess.Tests
{
  [TestFixture]
  public class TestDatabaseContextProviderDataModel
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      var dbConnection = new Mock<IVeyronDbConnection>();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var dataModel = new DatabaseContextProviderDataModel("connectionString", dbConnection.Object);
      //---------------Test Result -----------------------
      dataModel.Should().NotBeNull();
    }
    
    [TestCase("connectionString")]
    [TestCase("dbConnection")]
    public void Constructor_GivenNullParameter_ShouldThrowException(string parameterName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      ConstructorTestHelper.ValidateArgumentNullExceptionIfParameterIsNull<DatabaseContextProviderDataModel>(parameterName);
      //---------------Test Result -----------------------
    }
    
    [TestCase("connectionString", "ConnectionString")]
    [TestCase("dbConnection", "DbConnection")]
    public void Constructor_GivenParameterValue_ShouldSetProperty(string parameterName, string propertyName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      ConstructorTestHelper.ValidatePropertySetWithParameter<DatabaseContextProviderDataModel>(parameterName, propertyName);
      //---------------Test Result -----------------------
    }
  }
}
