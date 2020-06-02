using System;
using System.Linq;
using System.Collections.Generic;

using NUnit.Framework;
using FluentAssertions;

using Thuria.Calot.TestUtilities;
using Thuria.Thark.Core.DataAccess;

namespace Thuria.Thark.DataModel.Tests
{
  [TestFixture]
  public class TestThuriaDataModelExtensions
  {
    [Test]
    public void GetThuriaDataModelTableName_GivenDataModelWithTableAttributeSet_ShouldReturnNameSpecifiedInAttribute()
    {
      //---------------Set up test pack-------------------
      var dataModel = new ThuriaTestDataModel();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var tableName = dataModel.GetThuriaDataModelTableName();
      //---------------Test Result -----------------------
      tableName.Should().Be("ThuriaTestOne");
    }

    [Test]
    public void GetThuriaDataModelTableName_GivenDataModelWithTableAttributeNotSet_ShouldReturnClassName()
    {
      //---------------Set up test pack-------------------
      var dataModel = new ThuriaPocoDataModel();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var tableName = dataModel.GetThuriaDataModelTableName();
      //---------------Test Result -----------------------
      tableName.Should().Be("ThuriaPoco");
    }

    [TestCase("Id", true, "Id", null, false, false)]
    [TestCase("Name", true, "DisplayName", "Name")]
    [TestCase("Description", false)]
    [TestCase("ModifiedDate", true, "Modified")]
    [TestCase("IsActive", true, "IsActive", null, false, true)]
    public void GetThuriaDataModelColumnName_GivenDataModel_ShouldReturnExpectedAttribute(string propertyName, bool isAttributeSet,
                                                                                          string columnName = null, string columnAlias = null, 
                                                                                          bool isInsertColumn = true, bool isUpdateColumn = true)
    {
      //---------------Set up test pack-------------------
      var dataModel = new ThuriaTestDataModel();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var columnAttribute = dataModel.GetThuriaDataModelColumnName(propertyName);
      //---------------Test Result -----------------------
      columnAttribute.Should().NotBeNull();

      if (isAttributeSet)
      {
        columnAttribute.ColumnName.Should().Be(columnName);
        columnAttribute.Alias.Should().Be(columnAlias);
        columnAttribute.IsInsertColumn.Should().Be(isInsertColumn);
        columnAttribute.IsUpdateColumn.Should().Be(isUpdateColumn);
      }
      else
      {
        columnAttribute.ColumnName.Should().Be(propertyName);
        columnAttribute.Alias.Should().BeNullOrEmpty();
        columnAttribute.IsInsertColumn.Should().BeTrue();
        columnAttribute.IsUpdateColumn.Should().BeTrue();
      }
    }

    [Test]
    public void GetThuriaDataModelColumnName_GivenPropertyDoesNotExist_ShouldThrowException()
    {
      //---------------Set up test pack-------------------
      var propertyName = "InvalidProperty";
      var dataModel    = new ThuriaTestDataModel();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var exception = Assert.Throws<Exception>(() => dataModel.GetThuriaDataModelColumnName(propertyName));
      //---------------Test Result -----------------------
      exception.Message.Should().Be($"Property {propertyName} does not exist on {dataModel.GetType().FullName}");
    }

    [TestCase(DbContextAction.Retrieve, "Id", "Id")]
    [TestCase(DbContextAction.Retrieve, "DisplayName", "Name")]
    [TestCase(DbContextAction.Retrieve, "Description", "Description")]
    [TestCase(DbContextAction.Retrieve, "Modified", "ModifiedDate")]
    [TestCase(DbContextAction.Retrieve, "IsActive", "IsActive")]
    [TestCase(DbContextAction.Create, "Id", "Id", false)]
    [TestCase(DbContextAction.Create, "DisplayName", "Name")]
    [TestCase(DbContextAction.Create, "Description", "Description")]
    [TestCase(DbContextAction.Create, "Modified", "ModifiedDate")]
    [TestCase(DbContextAction.Create, "IsActive", "IsActive", false)]
    [TestCase(DbContextAction.Update, "Id", "Id", false)]
    [TestCase(DbContextAction.Update, "DisplayName", "Name")]
    [TestCase(DbContextAction.Update, "Description", "Description")]
    [TestCase(DbContextAction.Update, "Modified", "ModifiedDate")]
    [TestCase(DbContextAction.Update, "IsActive", "IsActive")]
    public void GetThuriaDataModelColumns_ShouldReturnTheExpectedColumnAttributes(DbContextAction DbContextAction, string columnName, string propertyName, bool isExpectedInList = true)
    {
      //---------------Set up test pack-------------------
      var dataModel = new ThuriaTestDataModel();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var dataModelColumns = dataModel.GetThuriaDataModelColumns(DbContextAction);
      //---------------Test Result -----------------------
      var columnAttributes = dataModelColumns.ToList();

      columnAttributes.Should().NotBeNull();

      if (isExpectedInList)
      {
        columnAttributes.Should().Contain(attribute => attribute.ColumnName == columnName);
        columnAttributes.Should().Contain(attribute => attribute.PropertyName == propertyName);
      }
      else
      {
        columnAttributes.Should().NotContain(attribute => attribute.ColumnName == columnName);
        columnAttributes.Should().NotContain(attribute => attribute.PropertyName == propertyName);
      }
    }

    [Test]
    public void GetThuriaDataModelPrimaryKey_GivenDataModelWithPrimaryKey_ShouldReturnExpectedColumnAttribute()
    {
      //---------------Set up test pack-------------------
      var dataModel = new ThuriaTestDataModel();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var primaryKey = dataModel.GetThuriaDataModelPrimaryKey();
      //---------------Test Result -----------------------
      primaryKey.Should().NotBeNull();
      primaryKey.ColumnName.Should().Be("Id");
      primaryKey.PropertyName.Should().Be("Id");
    }

    [Test]
    public void GetThuriaDataModelPrimaryKey_GivenDataModelWithNoPrimaryKey_ShouldReturnNull()
    {
      //---------------Set up test pack-------------------
      var dataModel = new ThuriaPocoDataModel();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var primaryKey = dataModel.GetThuriaDataModelPrimaryKey();
      //---------------Test Result -----------------------
      primaryKey.Should().BeNull();
    }

    [TestCase(DbContextAction.Retrieve, 5)]
    [TestCase(DbContextAction.Create, 2)]
    [TestCase(DbContextAction.Update, 3)]
    [TestCase(DbContextAction.Delete, 5)]
    public void GetThuriaDataModelConditions_GivenDataModelWithConditions_ShouldReturnAllExpectedConditions(DbContextAction DbContextAction, int noOfExpectedConditions)
    {
      //---------------Set up test pack-------------------
      var dataModel = new ThuriaTestDataModel
        {
          Id           = Guid.NewGuid(),
          Name         = RandomValueGenerator.CreateRandomString(),
          Description  = RandomValueGenerator.CreateRandomString(),
          ModifiedDate = RandomValueGenerator.CreateRandomDate(),
          IsActive     = true
        };
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var allConditions = dataModel.GetThuriaDataModelConditions(DbContextAction);
      //---------------Test Result -----------------------
      var dataModelConditions = allConditions.ToList();

      dataModelConditions.Should().NotBeNull();
      dataModelConditions.Count.Should().Be(noOfExpectedConditions);
    }

    [TestCase(DbContextAction.Create)]
    [TestCase(DbContextAction.Update)]
    public void GetThuriaDataModelConditions_GivenDataModelWithNoConditions_ShouldReturnEmptyConditionList(DbContextAction DbContextAction)
    {
      //---------------Set up test pack-------------------
      var dataModel = new ThuriaPocoDataModel
        {
          Id           = Guid.NewGuid(),
          Name         = RandomValueGenerator.CreateRandomString(),
          Description  = RandomValueGenerator.CreateRandomString(),
          ModifiedDate = RandomValueGenerator.CreateRandomDate(),
          IsActive     = true
        };
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var allConditions = dataModel.GetThuriaDataModelConditions(DbContextAction);
      //---------------Test Result -----------------------
      var dataModelConditions = allConditions.ToList();

      dataModelConditions.Should().NotBeNull();
      dataModelConditions.Count.Should().Be(0);
    }

    [Test]
    public void GetThuriaDataModelRelationships_GivenDataModelWithNoRelationships_ShouldReturnEmptyList()
    {
      //---------------Set up test pack-------------------
      var dataModel = new ThuriaPocoDataModel();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var modelRelationships = dataModel.GetThuriaDataModelRelationships();
      //---------------Test Result -----------------------
      var thuriaRelationshipAttributes = modelRelationships.ToList();
      thuriaRelationshipAttributes.Should().NotBeNull();
      thuriaRelationshipAttributes.Count().Should().Be(0);
    }

    [Test]
    public void GetThuriaDataModelRelationships_GivenDataModelWithRelationships_ShouldReturnListOfRelationships()
    {
      //---------------Set up test pack-------------------
      var dataModel = new ThuriaTestDataModel();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var modelRelationships = dataModel.GetThuriaDataModelRelationships();
      //---------------Test Result -----------------------
      var thuriaRelationshipAttributes = modelRelationships.ToList();
      thuriaRelationshipAttributes.Should().NotBeNull();
      thuriaRelationshipAttributes.Count.Should().Be(2);
    }

    [Test]
    public void GetThuriaPopulatedRelationshipMetadata_GivenDataModelWithNoRelationships_ShouldReturnEmptyList()
    {
      //---------------Set up test pack-------------------
      var dataModel = new ThuriaPocoDataModel();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var modelRelationships = dataModel.GetThuriaPopulatedRelationshipMetadata();
      //---------------Test Result -----------------------
      var thuriaRelationshipAttributes = modelRelationships.ToList();
      thuriaRelationshipAttributes.Should().NotBeNull();
      thuriaRelationshipAttributes.Count().Should().Be(0);
    }

    [Test]
    public void GetThuriaPopulatedRelationshipMetadata_GivenDataModelWithRelationships_ShouldReturnListOfRelationships()
    {
      //---------------Set up test pack-------------------
      var dataModel = new ThuriaTestDataModel
        {
          Id                   = Guid.NewGuid(),
          Name                 = RandomValueGenerator.CreateRandomString(),
          Description          = RandomValueGenerator.CreateRandomString(),
          ModifiedDate         = RandomValueGenerator.CreateRandomDate(),
          IsActive             = true,
          ForeignTestDataModel = new ThuriaForeignTestDataModel(),
          AllForeignTests      = new List<ThuriaForeignTestDataModel> { new ThuriaForeignTestDataModel() }
        };
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var modelRelationships = dataModel.GetThuriaPopulatedRelationshipMetadata();
      //---------------Test Result -----------------------
      var thuriaRelationshipAttributes = modelRelationships.ToList();
      thuriaRelationshipAttributes.Should().NotBeNull();
      thuriaRelationshipAttributes.Count.Should().Be(2);
    }
  }
}
