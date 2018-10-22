using System;

namespace Thuria.Thark.DataModel.Models
{
  /// <summary>
  /// Thuria Data Model Condition Metadata
  /// </summary>
  public class ThuriaDataModelConditionMetadata
  {
    /// <summary>
    /// Thuria Data Model Condition Metadata constructor
    /// </summary>
    /// <param name="columnName">Column Name</param>
    /// <param name="isRequired">Required indicator</param>
    /// <param name="value">Condition Value</param>
    public ThuriaDataModelConditionMetadata(string columnName, bool isRequired, object value)
    {
      ColumnName = columnName ?? throw new ArgumentNullException(nameof(columnName));
      IsRequired = isRequired;
      Value      = value;
    }

    /// <summary>
    /// Column Name
    /// </summary>
    public string ColumnName { get; }

    /// <summary>
    /// Required indicator
    /// </summary>
    public bool IsRequired { get; }

    /// <summary>
    /// Condition Value
    /// </summary>
    public object Value { get; }
  }
}
