using System.Data;
using NUnit.Framework;
using FluentAssertions;
using MGS.Casino.Veyron.TestUtilities;

namespace MGS.Casino.Veyron.DataAccess.Tests
{
  [TestFixture]
  public class TestDbTypeMapMetadata
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var dataModel = new DbTypeMapMetadata(DbType.Date, null);
      //---------------Test Result -----------------------
      dataModel.Should().NotBeNull();
    }
    
    [TestCase("dbType", "DbType")]
    [TestCase("size", "Size")]
    public void Constructor_GivenParameterValue_ShouldSetProperty(string parameterName, string propertyName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      ConstructorTestHelper.ValidatePropertySetWithParameter<DbTypeMapMetadata>(parameterName, propertyName);
      //---------------Test Result -----------------------
    }
  }
}
