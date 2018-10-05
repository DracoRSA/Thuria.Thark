using System;

namespace Thuria.Thark.StatementBuilder
{
  public static class StatementExtensions
  {
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
