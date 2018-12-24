using System;
using System.Collections.Generic;

namespace MGS.Casino.Veyron.DataAccessInterfaces
{
  /// <summary>
  /// Data Result Extensions
  /// </summary>
  public static class DataResultExtensions
  {
    /// <summary>
    /// Get Value from Execution Result
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="executionResult">Execution Result dictionary</param>
    /// <param name="fieldName">Field Name</param>
    /// <returns></returns>
    public static T Get<T>(this Dictionary<string, object> executionResult, string fieldName)
    {
      if (executionResult == null || !executionResult.ContainsKey(fieldName) || executionResult[fieldName] == null)
      {
        return default(T);
      }

      return (T) Convert.ChangeType(executionResult[fieldName], typeof(T));
    }
  }
}
