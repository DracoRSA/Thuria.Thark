using System;

namespace Thuria.Thark.DataModel.Attributes
{
  /// <summary>
  /// Thuria Column Attribute
  /// </summary>
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
  public class ThuriaColumnAttribute : Attribute
  {
    /// <summary>
    /// Thuria Colum Attribute Constructor
    /// </summary>
    /// <param name="columnName">Column Name in Database to map the property to</param>
    /// <param name="columnAlias">Column Alias (Optional)</param>
    /// <param name="isPrimaryKey">Primary Key indicator</param>
    /// <param name="isInsertColumn">Insert Column indicator (Default False)</param>
    /// <param name="isUpdateColumn">Update Column (Default False)</param>
    public ThuriaColumnAttribute(string columnName, string columnAlias = null, bool isPrimaryKey = false, bool isInsertColumn = true, bool isUpdateColumn = true)
    {
      if (string.IsNullOrWhiteSpace(columnName)) { throw new ArgumentNullException(nameof(columnName)); }

      ColumnName     = columnName;
      Alias          = columnAlias;
      IsPrimaryKey   = isPrimaryKey;
      IsInsertColumn = isInsertColumn;
      IsUpdateColumn = isUpdateColumn;
    }

    /// <summary>
    /// Database Column Name
    /// </summary>
    public string ColumnName { get; set; }

    /// <summary>
    /// Column Alias
    /// </summary>
    public string Alias { get; set; }

    /// <summary>
    /// Primary Key indicator
    /// </summary>
    public bool IsPrimaryKey { get; set; }

    /// <summary>
    /// Insert Column indicator
    /// </summary>
    public bool IsInsertColumn { get; set; }

    /// <summary>
    /// Update Column indicator
    /// </summary>
    public bool IsUpdateColumn { get; set; }

    /// <summary>
    /// Property Name
    /// </summary>
    public string PropertyName { get; private set; }

    /// <summary>
    /// Set Property Name
    /// </summary>
    /// <param name="propertyName">Property Name</param>
    /// <returns>The current attribute</returns>
    public ThuriaColumnAttribute SetPropertyName(string propertyName)
    {
      PropertyName = propertyName;
      return this;
    }
  }
}
