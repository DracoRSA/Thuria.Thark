using System;

namespace Thuria.Thark.DataModel.Attributes
{
  /// <summary>
  /// Thuria Relationship Attribute
  /// </summary>
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
  public class ThuriaRelationshipAttribute : Attribute
  {
    /// <summary>
    /// Thuria Relationship Attribute constructor
    /// </summary>
    /// <param name="relationshipName">Relationship Name</param>
    /// <param name="relationshipType">Relationship Type</param>
    /// <param name="keyFieldName">Key Field Name</param>
    /// <param name="foreignKeyFieldName">Foreign Key Field Name</param>
    /// <param name="loadExplicitly">Load Explicitly indicator</param>
    public ThuriaRelationshipAttribute(string relationshipName, TharkRelationshipType relationshipType, string keyFieldName, string foreignKeyFieldName, bool loadExplicitly = true)
    {
      if (string.IsNullOrWhiteSpace(relationshipName)) { throw new ArgumentNullException(nameof(relationshipName)); }
      if (string.IsNullOrWhiteSpace(keyFieldName)) { throw new ArgumentNullException(nameof(keyFieldName)); }
      if (string.IsNullOrWhiteSpace(foreignKeyFieldName)) { throw new ArgumentNullException(nameof(foreignKeyFieldName)); }

      RelationshipName    = relationshipName;
      RelationshipType    = relationshipType;
      KeyFieldName        = keyFieldName;
      ForeignKeyFieldName = foreignKeyFieldName;
      LoadExplicitly      = loadExplicitly;
    }

    /// <summary>
    /// Relationship Name
    /// </summary>
    public string RelationshipName { get; set; }

    /// <summary>
    /// Relationship Type
    /// </summary>
    public TharkRelationshipType RelationshipType { get; set; }

    /// <summary>
    /// Key Field Name
    /// </summary>
    public string KeyFieldName { get; set; }

    /// <summary>
    /// Foreign Key Field Name
    /// </summary>
    public string ForeignKeyFieldName { get; set; }

    /// <summary>
    /// Load Explicitly indicator
    /// </summary>
    public bool LoadExplicitly { get; set; }

    /// <summary>
    /// Property Name
    /// </summary>
    public string PropertyName { get; set; }
  }
}
