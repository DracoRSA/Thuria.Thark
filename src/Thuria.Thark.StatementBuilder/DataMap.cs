using System.Collections.Generic;
using Thuria.Thark.Core.Statement;

namespace Thuria.Thark.StatementBuilder
{
  public static class DataMap
  {
    public static Dictionary<BooleanOperator, string> BooleanOperatorMap = new Dictionary<BooleanOperator, string>
      {
        { BooleanOperator.And, " AND " },
        { BooleanOperator.Or, " OR " }
      };

    public static Dictionary<EqualityOperators, string> EqualityOperatorMap = new Dictionary<EqualityOperators, string>
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