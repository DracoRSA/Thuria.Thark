using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

using Thuria.Zitidar.Extensions;
using Thuria.Thark.DataModel.Models;
using Thuria.Thark.DataModel.Attributes;

namespace Thuria.Thark.DataModel
{
  /// <summary>
  /// Thuria DataModel Extension Methods
  /// </summary>
  public static class ThuriaDataModelExtensions
  {
    /// <summary>
    /// Retrieve the Thuria Database Table Name of the provided Data Model
    /// </summary>
    /// <param name="dataModel">Data Model</param>
    /// <returns>
    /// This will return one of the following:
    ///   * If the Thuria Table Attribute is set on the data model, the name specified in the attribute or
    ///   * The name of the data model class
    /// </returns>
    public static string GetThuriaDataModelTableName(this object dataModel)
    {
      var dataModelType        = dataModel.GetType();
      var thuriaTableAttribute = dataModelType.GetCustomAttribute<ThuriaTableAttribute>();

      return thuriaTableAttribute == null ? dataModelType.Name.Replace("DataModel", "") : thuriaTableAttribute.TableName;
    }

    /// <summary>
    /// Retrieve the Thuria Database Column metadata if the given Data Model's specified property
    /// </summary>
    /// <param name="dataModel">Data Model</param>
    /// <param name="propertyName">Property Name</param>
    /// <returns>The Thuria Column Attribute associated with the specified property</returns>
    public static ThuriaColumnAttribute GetThuriaDataModelColumnName(this object dataModel, string propertyName)
    {
      var propertyInfo = dataModel.GetType().GetProperty(propertyName);
      if (propertyInfo == null)
      {
        throw new Exception($"Property {propertyName} does not exist on {dataModel.GetType().FullName}");
      }

      var thuriaColumnAttribute = propertyInfo.GetCustomAttribute<ThuriaColumnAttribute>();
    
      return thuriaColumnAttribute ?? new ThuriaColumnAttribute(propertyName);
    }
    
    /// <summary>
    /// Retrieve the Given Data Models Columns based on the Action to be performed
    /// </summary>
    /// <param name="dataModel">Data Model</param>
    /// <param name="tharkAction">Thark Action</param>
    /// <returns>A list of Column Attributes for the specified Thark Action</returns>
    public static IEnumerable<ThuriaColumnAttribute> GetThuriaDataModelColumns(this object dataModel, TharkAction tharkAction)
    {
      var dataModelType = dataModel.GetType();
      var allProperties = dataModelType.GetProperties();
    
      return (from currentProperty in allProperties
              let thuriaIgnoreAttribute = currentProperty.GetCustomAttribute<ThuriaIgnoreAttribute>()
              where thuriaIgnoreAttribute == null
              let thuriaRelationshipAttribute = currentProperty.GetCustomAttribute<ThuriaRelationshipAttribute>()
              where thuriaRelationshipAttribute == null
              // let thuriaConditionAttribute = currentProperty.GetCustomAttributes<ThuriaConditionColumnAttribute>().FirstOrDefault(attribute => attribute.TharkAction == tharkAction)
              // where thuriaConditionAttribute == null
              let thuriaColumnAttribute = currentProperty.GetCustomAttribute<ThuriaColumnAttribute>()
              select thuriaColumnAttribute?.SetPropertyName(currentProperty.Name) ?? new ThuriaColumnAttribute(currentProperty.Name).SetPropertyName(currentProperty.Name))
              .Where(thuriaColumnAttribute =>
                {
                  switch (tharkAction)
                  {
                    case TharkAction.Insert:
                      return thuriaColumnAttribute.IsInsertColumn;
                    case TharkAction.Update:
                      return thuriaColumnAttribute.IsUpdateColumn;
                    default:
                      return true;
                  }
                });
    }

    /// <summary>
    /// Retrieve the Primary Key Column on a given Data Model
    /// </summary>
    /// <param name="dataModel">Data Model</param>
    /// <returns>
    /// A null if no Primary key has been specified or a ThuriaColumnAttribute representing the Primary Key of the Data Model
    /// </returns>
    public static ThuriaColumnAttribute GetThuriaDataModelPrimaryKey(this object dataModel)
    {
      var dataModelType = dataModel.GetType();
      var allProperties = dataModelType.GetProperties();
    
      foreach (var currentProperty in allProperties)
      {
        if (currentProperty.GetCustomAttribute<ThuriaIgnoreAttribute>() != null) { continue; }
    
        var columnAttribute = currentProperty.GetCustomAttributes<ThuriaColumnAttribute>()
                                                      .FirstOrDefault(attribute => attribute.IsPrimaryKey);
        if (columnAttribute == null) { continue; }

        return columnAttribute.SetPropertyName(currentProperty.Name);
      }
    
      return null;
    }
    

    /// <summary>
    /// Retrieve the Conditions for a given Data Model for a specific Thark Action
    /// </summary>
    /// <param name="dataModel">Data Model</param>
    /// <param name="tharkAction">Thark Action</param>
    /// <returns></returns>
    public static IEnumerable<ThuriaDataModelConditionMetadata> GetThuriaDataModelConditions(this object dataModel, TharkAction tharkAction)
    {
      var allConditions = new List<ThuriaDataModelConditionMetadata>();
      var dataModelType = dataModel.GetType();
      var allProperties = dataModelType.GetProperties();
    
      foreach (var currentProperty in allProperties)
      {
        if (currentProperty.GetCustomAttribute<ThuriaIgnoreAttribute>() != null) { continue; }
    
        var conditionColumnAttribute = currentProperty.GetCustomAttributes<ThuriaConditionColumnAttribute>()
                                                      .FirstOrDefault(attribute => attribute.TharkAction == tharkAction);
        if (conditionColumnAttribute == null) { continue; }
    
        var propertyValue     = dataModel.GetPropertyValue(currentProperty.Name);
        var columnName        = dataModel.GetThuriaDataModelColumnName(currentProperty.Name);
        var conditionMetadata = new ThuriaDataModelConditionMetadata(columnName.ColumnName, conditionColumnAttribute.IsRequired, propertyValue);
    
        allConditions.Add(conditionMetadata);
      }
    
      return allConditions;
    }

    /// <summary>
    /// Retrieve the Relationships associated with the given Data Model
    /// </summary>
    /// <param name="dataModel">Data Model</param>
    /// <returns>A list of relationships (if any) associated with the Data Model</returns>
    public static IEnumerable<ThuriaRelationshipAttribute> GetThuriaDataModelRelationships(this object dataModel)
    {
      var dataModelType    = dataModel.GetType();
      var allProperties    = dataModelType.GetProperties();
      var returnAttributes = new List<ThuriaRelationshipAttribute>();
    
      foreach (var currentProperty in allProperties)
      {
        if (currentProperty.GetCustomAttribute<ThuriaIgnoreAttribute>() != null) { continue; }
    
        var relationshipAttributes = currentProperty.GetCustomAttributes<ThuriaRelationshipAttribute>();
        foreach (var currentRelationshipAttribute in relationshipAttributes)
        {
          currentRelationshipAttribute.PropertyName = currentProperty.Name;
          returnAttributes.Add(currentRelationshipAttribute);
        }
      }
    
      return returnAttributes;
    }
    
    /// <summary>
    /// Retrieve the Thuria Populated Relationship from a given Data Model
    /// </summary>
    /// <param name="dataModel">Data Model</param>
    /// <returns>A list of the Relationships Metadata</returns>
    public static IEnumerable<ThuriaDataModelRelationshipMetadata> GetThuriaPopulatedRelationshipMetadata(this object dataModel)
    {
      var dataModelType = dataModel.GetType();
      var allProperties = dataModelType.GetProperties();
    
      var allRelationships = new List<ThuriaDataModelRelationshipMetadata>();
      foreach (var currentProperty in allProperties)
      {
        if (currentProperty.GetCustomAttribute<ThuriaIgnoreAttribute>() != null) { continue; }
    
        var currentRelationship = currentProperty.GetCustomAttribute<ThuriaRelationshipAttribute>();
        if (currentRelationship == null) { continue; }
    
        var propertyValue = dataModel.GetPropertyValue(currentProperty.Name);
        if (propertyValue == null) { continue; }
    
        var relationshipMetadata = new ThuriaDataModelRelationshipMetadata(currentProperty.Name, propertyValue, currentRelationship);
        allRelationships.Add(relationshipMetadata);
      }
    
      return allRelationships;
    }
  }
}
