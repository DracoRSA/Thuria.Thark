using System;
using System.Data;

namespace MGS.Casino.Veyron.DataAccessInterfaces
{
  /// <summary>
  /// IDataReader Extension methods
  /// </summary>
  public static class DataReaderExtensions
  {
    /// <summary>
    /// Retrieve the value from a Data Reader
    /// </summary>
    /// <param name="dataReader">Data Reader</param>
    /// <param name="columnName">Column Name</param>
    /// <param name="isMandatory">Mandatory indicator</param>
    /// <param name="defaultValue">Default Value</param>
    /// <returns>Either the default value or the current record's column value</returns>
    public static object GetValue(this IDataReader dataReader, string columnName, bool isMandatory = false, object defaultValue = null)
    {
      if (dataReader == null)
      {
        throw new ArgumentNullException(nameof(dataReader));
      }

      if (dataReader[columnName] != DBNull.Value)
      {
        return dataReader[columnName];
      }

      if (isMandatory)
      {
        throw new Exception($"Mandatory value for column {columnName} is NULL.");
      }

      return defaultValue;
    }

    /// <summary>
    /// Retrieve the value from a Data Reader
    /// </summary>
    /// <typeparam name="T">Value Type</typeparam>
    /// <param name="dataReader">Data Reader</param>
    /// <param name="columnName">Column Name</param>
    /// <param name="isMandatory">Mandatory indicator</param>
    /// <param name="defaultValue">Default Value</param>
    /// <returns>Either the default value or the current record's column value</returns>
    public static T GetValue<T>(this IDataReader dataReader, string columnName, bool isMandatory = false, T defaultValue = default(T))
    {
      if (dataReader == null)
      {
        throw new ArgumentNullException(nameof(dataReader));
      }

      if (dataReader[columnName] != DBNull.Value)
      {
        return (T) Convert.ChangeType(dataReader[columnName], typeof(T));
      }

      if (isMandatory)
      {
        throw new Exception($"Mandatory value for column {(object)columnName} is NULL.");
      }

      return defaultValue;
    }
  }
}
