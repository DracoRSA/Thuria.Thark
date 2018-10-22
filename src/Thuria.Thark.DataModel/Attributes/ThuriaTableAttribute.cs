using System;

namespace Thuria.Thark.DataModel.Attributes
{
  /// <summary>
  /// Thuria Table Attribute
  /// </summary>
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
  public class ThuriaTableAttribute : Attribute
  {
    /// <summary>
    /// Attribute Constructor
    /// </summary>
    /// <param name="tableName">Table Name</param>
    public ThuriaTableAttribute(string tableName)
    {
      if (string.IsNullOrWhiteSpace(tableName)) { throw new ArgumentNullException(nameof(tableName)); }
      TableName = tableName;
    }

    /// <summary>
    /// Table Name to map data from
    /// </summary>
    public string TableName { get; private set; }
  }
}