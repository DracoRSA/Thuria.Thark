using Thuria.Thark.Core.Statement;
using Thuria.Thark.Core.Statement.Models;

namespace Thuria.Thark.StatementBuilder.Models
{
  /// <summary>
  /// Condition Data Model
  /// </summary>
  public class ConditionModel : BaseModel, IConditionModel
  {
    private readonly string _sourceTableName;
    private readonly string _sourceColumnName;
    private readonly EqualityOperators _equalityOperator;
    private readonly object _conditionValue;

    /// <summary>
    /// Condition Data Model constructor
    /// </summary>
    /// <param name="sourceTableName">Source Table Name</param>
    /// <param name="sourceColumnName">Source Column Name</param>
    /// <param name="equalityOperator">Equality Operator</param>
    /// <param name="conditionValue">Condition Value</param>
    public ConditionModel(string sourceTableName, string sourceColumnName, EqualityOperators equalityOperator, object conditionValue)
    {
      _sourceTableName  = sourceTableName;
      _sourceColumnName = sourceColumnName;
      _equalityOperator = equalityOperator;
      _conditionValue   = conditionValue;
    }

    /// <inheritdoc />
    public string Quote => _conditionValue.StatementQuote();

    /// <summary>
    /// Create a string representation of the model
    /// </summary>
    /// <returns>A string representation of the model</returns>
    public override string ToString()
    {
      var conditionColumn = new ColumnModel(_sourceTableName, _sourceColumnName)
        {
          DatabaseProvider = DatabaseProvider
        };

      return $"{conditionColumn} {DataMap.EqualityOperatorMap[_equalityOperator]} {Quote}{_conditionValue.StatementValue() ?? "NULL"}{Quote}";
    }

    /// <inheritdoc />
    public override bool Equals(object compareObject)
    {
      var otherField = compareObject as ConditionModel;
      if (otherField == null) { return false; }

      return ToString() == otherField.ToString();
    }
  }
}