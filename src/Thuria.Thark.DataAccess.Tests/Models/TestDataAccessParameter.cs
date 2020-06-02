using NUnit.Framework;
using FluentAssertions;
using Thuria.Calot.TestUtilities;
using Thuria.Thark.DataAccess.Models;

namespace Thuria.Thark.DataAccess.Tests.Models
{
  [TestFixture]
  public class TestDataAccessParameter
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var dataAccessParameter = new DataAccessParameter("test", typeof(string));
      //---------------Test Result -----------------------
      dataAccessParameter.Should().NotBeNull();
    }

    [TestCase("parameterName")]
    public void Constructor_GivenNullParameter_ShouldThrowArgumentNullException(string parameterName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      ConstructorTestHelper.ValidateArgumentNullExceptionIfParameterIsNull<DataAccessParameter>(parameterName);
      //---------------Test Result -----------------------
    }

    [TestCase("parameterName", "Name")]
    [TestCase("parameterType", "ParameterType")]
    [TestCase("objectType", "ObjectType")]
    [TestCase("parameterValue", "Value")]
    [TestCase("direction", "Direction")]
    [TestCase("isMandatory", "IsMandatory")]
    [TestCase("defaultValue", "DefaultValue")]
    public void Constructor_GivenParameterValue_ShouldSetPropertyValue(string parameterName, string propertyName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      ConstructorTestHelper.ValidatePropertySetWithParameter<DataAccessParameter>(parameterName, propertyName);
      //---------------Test Result -----------------------
    }
  }
}
