using System;
using Thuria.Thark.DataModel.Attributes;

namespace Thuria.Thark.DataModel.Models
{
  /// <summary>
  /// Thuria Data Model Relationship Metadata
  /// </summary>
  public class ThuriaDataModelRelationshipMetadata
  {
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="parentPropertyName">Parent Property Name</param>
    /// <param name="dataModel">Data Model</param>
    /// <param name="relationship">Relationship Attribute</param>
    public ThuriaDataModelRelationshipMetadata(string parentPropertyName, object dataModel, ThuriaRelationshipAttribute relationship)
    {
      ParentPropertyName = parentPropertyName ?? throw new ArgumentNullException(nameof(parentPropertyName));
      DataModel          = dataModel ?? throw new ArgumentNullException(nameof(dataModel));
      Relationship       = relationship ?? throw new ArgumentNullException(nameof(relationship));
    }

    /// <summary>
    /// Parent Property Name
    /// </summary>
    public string ParentPropertyName { get; }

    /// <summary>
    /// Data Model
    /// </summary>
    public object DataModel { get; }

    /// <summary>
    /// Relationship Attribute
    /// </summary>
    public ThuriaRelationshipAttribute Relationship { get; }

    /// <summary>
    /// Key Value
    /// </summary>
    public object KeyValue { get; set; }
  }
}