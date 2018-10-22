using NUnit.Framework;
using FluentAssertions;

using Thuria.Calot.TestUtilities;
using Thuria.Thark.DataModel.Attributes;

namespace Thuria.Thark.DataModel.Tests.Attributes
{
  [TestFixture]
  public class TestThuriaColumnAttribute
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var tableAttribute = new ThuriaColumnAttribute("TestColumn");
      //---------------Test Result -----------------------
      tableAttribute.Should().NotBeNull();
    }

    [TestCase("columnName")]
    public void Constructor_GivenNullParameter_ShouldThrowException(string parameterName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      ConstructorTestHelper.ValidateArgumentNullExceptionIfParameterIsNull<ThuriaColumnAttribute>(parameterName);
      //---------------Test Result -----------------------
    }

    [TestCase("columnName", "ColumnName")]
    [TestCase("columnAlias", "Alias")]
    [TestCase("isPrimaryKey", "IsPrimaryKey")]
    [TestCase("isInsertColumn", "IsInsertColumn")]
    [TestCase("isUpdateColumn", "IsUpdateColumn")]
    public void Constructor_GivenParameterValue_ShouldSetPropertyValue(string parameterName, string propertyName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      ConstructorTestHelper.ValidatePropertySetWithParameter<ThuriaColumnAttribute>(parameterName, propertyName);
      //---------------Test Result -----------------------
    }

    [TestCase("ColumnName")]
    [TestCase("Alias")]
    [TestCase("IsPrimaryKey")]
    [TestCase("IsInsertColumn")]
    [TestCase("IsUpdateColumn")]
    [TestCase("ColumnName")]
    public void Properties_GivenValue_ShouldSetPropertyValue(string propertyName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      PropertyTestHelper.ValidateGetAndSet<ThuriaColumnAttribute>(propertyName);
      //---------------Test Result -----------------------
    }

    [TestCase("TestProperty")]
    [TestCase("SomeProperty")]
    [TestCase(null)]
    public void SetPropertyName_ShouldSetPropertyNameProperty(string propertyName)
    {
      //---------------Set up test pack-------------------
      var columnAttribute = new ThuriaColumnAttribute("SomeColumn");
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var thuriaColumnAttribute = columnAttribute.SetPropertyName(propertyName);
      //---------------Test Result -----------------------
      thuriaColumnAttribute.Should().NotBeNull();
      thuriaColumnAttribute.Should().Be(columnAttribute);
      thuriaColumnAttribute.PropertyName.Should().Be(propertyName);

      columnAttribute.PropertyName.Should().Be(propertyName);
    }
  }
}
