using Thuria.Thark.Core.Statement;
using Thuria.Thark.Core.Statement.Models;

namespace Thuria.Thark.StatementBuilder.Models
{
  /// <summary>
  /// Column Conditiona Model
  /// </summary>
  public class ColumnConditionModel : BaseModel, IConditionModel
  {
    private readonly string _sourceTableName;
    private readonly string _sourceColumnName;
    private readonly EqualityOperators _equalityOperator;
    private readonly string _compareTableName;
    private readonly string _compareColumnName;

    /// <summary>
    /// Column Condition Data Model constructor
    /// </summary>
    /// <param name="sourceTableName">Source Table Name</param>
    /// <param name="sourceColumnName">Source Table Column</param>
    /// <param name="equalityOperator">Equality Operator to be applied</param>
    /// <param name="compareTableName">Comparison Table Name</param>
    /// <param name="compareColumnName">Comparison Column Name</param>
    public ColumnConditionModel(string sourceTableName, string sourceColumnName, EqualityOperators equalityOperator, string compareTableName, string compareColumnName)
    {
      _sourceTableName = sourceTableName;
      _sourceColumnName = sourceColumnName;
      _equalityOperator = equalityOperator;
      _compareTableName = compareTableName;
      _compareColumnName = compareColumnName;
    }

    /// <inheritdoc />
    public string Quote { get; } = "";

    /// <inheritdoc />
    public override string ToString()
    {
      var conditionColumn = new ColumnModel(_sourceTableName, _sourceColumnName)
        {
          DatabaseProvider = DatabaseProvider
        };

      var compareColumn = new ColumnModel(_compareTableName, _compareColumnName)
        {
          DatabaseProvider = DatabaseProvider
        };

      return $"{conditionColumn} {DataMap.EqualityOperatorMap[_equalityOperator]} {compareColumn}";
    }

    /// <inheritdoc />
    public override bool Equals(object compareObject)
    {
      var otherField = compareObject as ColumnConditionModel;
      if (otherField == null) { return false; }

      return ToString() == otherField.ToString();
    }
  }
}
