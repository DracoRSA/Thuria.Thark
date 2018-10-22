using NUnit.Framework;
using FluentAssertions;
using Thuria.Calot.TestUtilities;
using Thuria.Thark.DataModel.Models;

namespace Thuria.Thark.DataModel.Tests.Models
{
  [TestFixture]
  public class TestThuriaDataModelConditionMetadata
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var conditionMetadata = new ThuriaDataModelConditionMetadata("Id", true, null);
      //---------------Test Result -----------------------
      conditionMetadata.Should().NotBeNull();
    }

    [TestCase("columnName", "ColumnName")]
    [TestCase("isRequired", "IsRequired")]
    [TestCase("value", "Value")]
    public void Constructor_GivenParameter_ShouldSetPropertyValue(string parameterName, string propertyName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      ConstructorTestHelper.ValidatePropertySetWithParameter<ThuriaDataModelConditionMetadata>(parameterName, propertyName);
      //---------------Test Result -----------------------
    }
  }
}
