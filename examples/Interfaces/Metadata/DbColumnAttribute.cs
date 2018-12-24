using System;

namespace MGS.Casino.Veyron.DataAccessInterfaces.Metadata
{
  /// <summary>
  /// Veyron DB Column Attribute
  /// </summary>
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
  public class VeyronDbColumnAttribute : Attribute
  {
    /// <summary>
    /// Create new Db Column Attribute
    /// </summary>
    /// <param name="dbColumnName"></param>
    public VeyronDbColumnAttribute(string dbColumnName)
    {
      DbColumnName = dbColumnName ?? throw new ArgumentNullException(nameof(dbColumnName));
    }

    /// <summary>
    /// Db Column Name of property
    /// </summary>
    public string DbColumnName { get; }
  }
}
