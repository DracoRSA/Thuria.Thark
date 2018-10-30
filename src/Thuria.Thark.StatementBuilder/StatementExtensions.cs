using System;

namespace Thuria.Thark.StatementBuilder
{
  /// <summary>
  /// Statement Extensions
  /// </summary>
  public static class StatementExtensions
  {
    /// <summary>
    /// Determine the Quote character to use for a specific data valeu
    /// </summary>
    /// <param name="dataValue">Data Value</param>
    /// <returns>A empty string or the relevant Quote</returns>
    public static string StatementQuote(this object dataValue)
    {
      if (dataValue == null) { return string.Empty; }

      switch (dataValue.GetType().Name)
      {
        case "Guid":
        case "String":
        case "DateTime":
          return "'";

        default:
          return string.Empty;
      }
    }

    /// <summary>
    /// Process the Statement Value
    /// </summary>
    /// <param name="dataValue">Data Value</param>
    /// <returns>The Statement Value</returns>
    public static object StatementValue(this object dataValue)
    {
      if (dataValue == null) { return "NULL"; }

      switch (dataValue.GetType().Name)
      {
        case "Boolean":
          return Convert.ToInt16(dataValue);

        default:
          return dataValue;
      }
    }
  }
}
