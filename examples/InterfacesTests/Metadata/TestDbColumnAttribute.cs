using NUnit.Framework;
using FluentAssertions;
using MGS.Casino.Veyron.TestUtilities;
using MGS.Casino.Veyron.DataAccessInterfaces.Metadata;

namespace MGS.Casino.Veyron.DataAccessInterfaces.Tests.Metadata
{
  [TestFixture]
  public class TestVeyronDbColumnAttribute
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var dbColumnAttribute = new VeyronDbColumnAttribute("test");
      //---------------Test Result -----------------------
      dbColumnAttribute.Should().NotBeNull();
    }

    [TestCase("dbColumnName")]
    public void Constructor_GivenNullParameter_ShouldThrowException(string parameterName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      ConstructorTestHelper.ValidateArgumentNullExceptionIfParameterIsNull<VeyronDbColumnAttribute>(parameterName);
      //---------------Test Result -----------------------
    }

    [TestCase("dbColumnName", "DbColumnName")]
    public void Constructor_GivenParameter_ShouldSetPropertyValue(string parameterName, string propertyName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      ConstructorTestHelper.ValidatePropertySetWithParameter<VeyronDbColumnAttribute>(parameterName, propertyName);
      //---------------Test Result -----------------------
    }
  }
}
