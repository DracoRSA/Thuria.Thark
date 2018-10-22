using NUnit.Framework;
using FluentAssertions;

using Thuria.Calot.TestUtilities;
using Thuria.Thark.DataModel.Models;
using Thuria.Thark.DataModel.Attributes;

namespace Thuria.Thark.DataModel.Tests.Models
{
  [TestFixture]
  public class TestThuriaDataModelRelationshipMetadata
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      var dataModel             = new ThuriaTestDataModel();
      var relationshipAttribute = new ThuriaRelationshipAttribute("ForeignTest", TharkRelationshipType.OneToOne, "Id", "ThuriaTestId");
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var relationshipMetadata = new ThuriaDataModelRelationshipMetadata("Id", dataModel, relationshipAttribute);
      //---------------Test Result -----------------------
      relationshipMetadata.Should().NotBeNull();
    }

    [TestCase("parentPropertyName", "ParentPropertyName")]
    [TestCase("dataModel", "DataModel")]
    [TestCase("relationship", "Relationship")]
    public void Constructor_GivenParameterValue_ShouldSetPropertyValue(string parameterName, string propertyName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      ConstructorTestHelper.ValidatePropertySetWithParameter<ThuriaDataModelRelationshipMetadata>(parameterName, propertyName);
      //---------------Test Result -----------------------
    }

    [TestCase("KeyValue")]
    public void Properties_GivenValue_ShouldSetPropertyValue(string propertyName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      PropertyTestHelper.ValidateGetAndSet<ThuriaDataModelRelationshipMetadata>(propertyName);
      //---------------Test Result -----------------------
    }
  }
}
