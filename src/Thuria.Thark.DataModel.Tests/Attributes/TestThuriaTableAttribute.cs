using NUnit.Framework;
using FluentAssertions;

using Thuria.Calot.TestUtilities;
using Thuria.Thark.DataModel.Attributes;

namespace Thuria.Thark.DataModel.Tests.Attributes
{
  [TestFixture]
  public class TestThuriaTableAttribute
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var tableAttribute = new ThuriaTableAttribute("TestTable");
      //---------------Test Result -----------------------
      tableAttribute.Should().NotBeNull();
    }

    [TestCase("tableName")]
    public void Constructor_GivenNullParameter_ShouldThrowException(string parameterName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      ConstructorTestHelper.ValidateArgumentNullExceptionIfParameterIsNull<ThuriaTableAttribute>(parameterName);
      //---------------Test Result -----------------------
    }

    [TestCase("tableName", "TableName")]
    public void Constructor_GivenParameterValue_ShouldSetPropertyValue(string parameterName, string propertyName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      ConstructorTestHelper.ValidatePropertySetWithParameter<ThuriaTableAttribute>(parameterName, propertyName);
      //---------------Test Result -----------------------
    }
  }
}
