using Thuria.Thark.Core.Statement;
using Thuria.Thark.Core.Statement.Models;
using Thuria.Thark.StatementBuilder.Builders;

namespace Thuria.Thark.StatementBuilder.Models
{
  public class ConditionModel : BaseModel, IConditionModel
  {
    private readonly string _sourceTableName;
    private readonly string _sourceColumnName;
    private readonly EqualityOperators _equalityOperator;
    private readonly object _conditionValue;

    public ConditionModel(string sourceTableName, string sourceColumnName, EqualityOperators equalityOperator, object conditionValue)
    {
      _sourceTableName  = sourceTableName;
      _sourceColumnName = sourceColumnName;
      _equalityOperator = equalityOperator;
      _conditionValue   = conditionValue;
    }

    public string Quote => _conditionValue.StatementQuote();

    public override string ToString()
    {
      var conditionColumn = new ColumnModel(_sourceTableName, _sourceColumnName)
        {
          DatabaseProvider = DatabaseProvider
        };

      return $"{conditionColumn} {DataMap.EqualityOperatorMap[_equalityOperator]} {Quote}{_conditionValue ?? "NULL"}{Quote}";
    }

    public override bool Equals(object compareObject)
    {
      var otherField = compareObject as ConditionModel;
      if (otherField == null) { return false; }

      return ToString() == otherField.ToString();
    }
  }
}