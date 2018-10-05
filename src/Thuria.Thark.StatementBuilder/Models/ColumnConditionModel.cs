using Thuria.Thark.Core.Statement;
using Thuria.Thark.Core.Statement.Models;

namespace Thuria.Thark.StatementBuilder.Models
{
  public class ColumnConditionModel : BaseModel, IConditionModel
  {
    private readonly string _sourceTableName;
    private readonly string _sourceColumnName;
    private readonly EqualityOperators _equalityOperator;
    private readonly string _compareTableName;
    private readonly string _compareColumnName;

    public ColumnConditionModel(string sourceTableName, string sourceColumnName, EqualityOperators equalityOperator, string compareTableName, string compareColumnName)
    {
      _sourceTableName = sourceTableName;
      _sourceColumnName = sourceColumnName;
      _equalityOperator = equalityOperator;
      _compareTableName = compareTableName;
      _compareColumnName = compareColumnName;
    }

    public string Quote { get; private set; } = "";

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

    public override bool Equals(object compareObject)
    {
      var otherField = compareObject as ColumnConditionModel;
      if (otherField == null) { return false; }

      return ToString() == otherField.ToString();
    }
  }
}
