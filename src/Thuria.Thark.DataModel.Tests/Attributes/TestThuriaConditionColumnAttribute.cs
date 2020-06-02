using NUnit.Framework;
using FluentAssertions;
using Thuria.Calot.TestUtilities;

using Thuria.Thark.Core.DataAccess;
using Thuria.Thark.DataModel.Attributes;

namespace Thuria.Thark.DataModel.Tests.Attributes
{
  [TestFixture]
  public class TestThuriaConditionColumnAttribute
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var tableAttribute = new ThuriaConditionColumnAttribute(DbContextAction.None);
      //---------------Test Result -----------------------
      tableAttribute.Should().NotBeNull();
    }

    [TestCase("contextAction", "ContextAction")]
    [TestCase("isRequired", "IsRequired")]
    public void Constructor_GivenParameterValue_ShouldSetPropertyValue(string parameterName, string propertyName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      ConstructorTestHelper.ValidatePropertySetWithParameter<ThuriaConditionColumnAttribute>(parameterName, propertyName);
      //---------------Test Result -----------------------
    }
    
    [TestCase("ContextAction")]
    [TestCase("IsRequired")]
    public void Properties_GivenValue_ShouldSetPropertyValue(string propertyName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      PropertyTestHelper.ValidateGetAndSet<ThuriaConditionColumnAttribute>(propertyName);
      //---------------Test Result -----------------------
    }
  }
}
