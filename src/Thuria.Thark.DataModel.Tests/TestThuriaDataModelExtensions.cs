using System;
using System.Linq;
using System.Collections.Generic;

using NUnit.Framework;
using FluentAssertions;

using Thuria.Calot.TestUtilities;

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

    [TestCase(TharkAction.Retrieve, "Id", "Id")]
    [TestCase(TharkAction.Retrieve, "DisplayName", "Name")]
    [TestCase(TharkAction.Retrieve, "Description", "Description")]
    [TestCase(TharkAction.Retrieve, "Modified", "ModifiedDate")]
    [TestCase(TharkAction.Retrieve, "IsActive", "IsActive")]
    [TestCase(TharkAction.Insert, "Id", "Id", false)]
    [TestCase(TharkAction.Insert, "DisplayName", "Name")]
    [TestCase(TharkAction.Insert, "Description", "Description")]
    [TestCase(TharkAction.Insert, "Modified", "ModifiedDate")]
    [TestCase(TharkAction.Insert, "IsActive", "IsActive", false)]
    [TestCase(TharkAction.Update, "Id", "Id", false)]
    [TestCase(TharkAction.Update, "DisplayName", "Name")]
    [TestCase(TharkAction.Update, "Description", "Description")]
    [TestCase(TharkAction.Update, "Modified", "ModifiedDate")]
    [TestCase(TharkAction.Update, "IsActive", "IsActive")]
    public void GetThuriaDataModelColumns_ShouldReturnTheExpectedColumnAttributes(TharkAction tharkAction, string columnName, string propertyName, bool isExpectedInList = true)
    {
      //---------------Set up test pack-------------------
      var dataModel = new ThuriaTestDataModel();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var dataModelColumns = dataModel.GetThuriaDataModelColumns(tharkAction);
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

    [TestCase(TharkAction.Retrieve, 0)]
    [TestCase(TharkAction.Insert, 2)]
    [TestCase(TharkAction.Update, 3)]
    [TestCase(TharkAction.Delete, 0)]
    public void GetThuriaDataModelConditions_GivenDataModelWithConditions_AndInsertAction_ShouldReturnAllExpectedConditions(TharkAction tharkAction, int noOfExpectedConditions)
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
      var allConditions = dataModel.GetThuriaDataModelConditions(tharkAction);
      //---------------Test Result -----------------------
      var dataModelConditions = allConditions.ToList();

      dataModelConditions.Should().NotBeNull();
      dataModelConditions.Count.Should().Be(noOfExpectedConditions);
    }

    [TestCase(TharkAction.Insert)]
    [TestCase(TharkAction.Update)]
    public void GetThuriaDataModelConditions_GivenDataModelWithNoConditions_ShouldReturnEmptyConditionList(TharkAction tharkAction)
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
      var allConditions = dataModel.GetThuriaDataModelConditions(tharkAction);
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
