using NUnit.Framework;
using FluentAssertions;
using MGS.Casino.Veyron.TestUtilities;

namespace MGS.Casino.Veyron.DataAccessInterfaces.Tests.DataModels
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
    public void Constructor_GivenNullParameter_ShouldThrowException(string parameterName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      ConstructorTestHelper.ValidateArgumentNullExceptionIfParameterIsNull<DataAccessParameter>(parameterName);
      //---------------Test Result -----------------------
    }

    [TestCase("parameterName", "Name")]
    [TestCase("parameterType", "ParameterType")]
    [TestCase("parameterValue", "Value")]
    [TestCase("parameterDirection", "Direction")]
    [TestCase("isMandatory", "IsMandatory")]
    [TestCase("defaultValue", "DefaultValue")]
    public void Constructor_GivenParameter_ShouldSetPropertyValue(string parameterName, string propertyName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      ConstructorTestHelper.ValidatePropertySetWithParameter<DataAccessParameter>(parameterName, propertyName);
      //---------------Test Result -----------------------
    }
  }
}
