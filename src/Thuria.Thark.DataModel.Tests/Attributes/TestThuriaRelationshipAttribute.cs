using NUnit.Framework;
using FluentAssertions;

using Thuria.Calot.TestUtilities;
using Thuria.Thark.DataModel.Attributes;

namespace Thuria.Thark.DataModel.Tests.Attributes
{
  [TestFixture]
  public class TestThuriaRelationshipAttribute
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var tableAttribute = new ThuriaRelationshipAttribute("TestRelationship", TharkRelationshipType.OneToOne, "KeyFieldName", "ForeignKeyFieldName");
      //---------------Test Result -----------------------
      tableAttribute.Should().NotBeNull();
    }

    [TestCase("relationshipName")]
    [TestCase("keyFieldName")]
    [TestCase("foreignKeyFieldName")]
    public void Constructor_GivenNullParameter_ShouldThrowException(string parameterName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      ConstructorTestHelper.ValidateArgumentNullExceptionIfParameterIsNull<ThuriaRelationshipAttribute>(parameterName);
      //---------------Test Result -----------------------
    }

    [TestCase("relationshipName", "RelationshipName")]
    [TestCase("relationshipType", "RelationshipType")]
    [TestCase("keyFieldName", "KeyFieldName")]
    [TestCase("foreignKeyFieldName", "ForeignKeyFieldName")]
    [TestCase("loadExplicitly", "LoadExplicitly")]
    public void Constructor_GivenParameterValue_ShouldSetPropertyValue(string parameterName, string propertyName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      ConstructorTestHelper.ValidatePropertySetWithParameter<ThuriaRelationshipAttribute>(parameterName, propertyName);
      //---------------Test Result -----------------------
    }

    [TestCase("RelationshipName")]
    [TestCase("RelationshipType")]
    [TestCase("KeyFieldName")]
    [TestCase("ForeignKeyFieldName")]
    [TestCase("LoadExplicitly")]
    [TestCase("PropertyName")]
    public void Properties_GivenValue_ShouldSetPropertyValue(string propertyName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      PropertyTestHelper.ValidateGetAndSet<ThuriaRelationshipAttribute>(propertyName);
      //---------------Test Result -----------------------
    }
  }
}
