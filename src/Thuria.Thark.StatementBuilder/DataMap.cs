using System.Collections.Generic;
using Thuria.Thark.Core.Statement;

namespace Thuria.Thark.StatementBuilder
{
  /// <summary>
  /// Data Mapping functionality
  /// </summary>
  public static class DataMap
  {
    /// <summary>
    /// Boolean Operator Mapping
    /// </summary>
    public static readonly Dictionary<BooleanOperator, string> BooleanOperatorMap = new Dictionary<BooleanOperator, string>
      {
        { BooleanOperator.And, " AND " },
        { BooleanOperator.Or, " OR " }
      };

    /// <summary>
    /// Equality Operators Mapping
    /// </summary>
    public static readonly Dictionary<EqualityOperators, string> EqualityOperatorMap = new Dictionary<EqualityOperators, string>
      {
        { EqualityOperators.Equals, "=" },
        { EqualityOperators.NotEquals, "!=" },
        { EqualityOperators.GreaterThan, ">" },
        { EqualityOperators.GreaterThanOrEqualTo, ">=" },
        { EqualityOperators.LessThan, "<" },
        { EqualityOperators.LessThanOrEqualTo, "<=" },
        { EqualityOperators.Like, "LIKE" },
        { EqualityOperators.Contains, "" },
        { EqualityOperators.StartsWith, "" },
        { EqualityOperators.EndsWith, "" },
      };
  }
}