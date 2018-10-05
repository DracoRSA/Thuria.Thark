using System;
using Thuria.Thark.Core.Statement;

namespace Thuria.Thark.StatementBuilder.Models
{
  public class RelationshipModel : BaseModel
  {
    private readonly RelationshipType _relationshipType;
    private readonly string _keyTableName;
    private readonly string _keyColumnName;
    private readonly EqualityOperators _equalityOperator;
    private readonly string _foreignKeyTableName;
    private readonly string _foreignKeyColumnName;

    public RelationshipModel(RelationshipType relationshipType, string keyTableName, string keyColumnName,
                             EqualityOperators equalityOperator, string foreignKeyTableName, string foreignKeyColumnName)
    {
      _relationshipType     = relationshipType;
      _keyTableName         = keyTableName;
      _keyColumnName        = keyColumnName;
      _equalityOperator     = equalityOperator;
      _foreignKeyTableName  = foreignKeyTableName;
      _foreignKeyColumnName = foreignKeyColumnName;
    }

    public override string ToString()
    {
      var relationshipCondition = new ColumnConditionModel(_keyTableName, _keyColumnName, _equalityOperator, _foreignKeyTableName, _foreignKeyColumnName)
        {
          DatabaseProvider = DatabaseProvider
        };

      switch (_relationshipType)
      {
        case RelationshipType.OneToOne:
          return $"LEFT JOIN [{_foreignKeyTableName}] ON {relationshipCondition}";

        default:
          throw new NotSupportedException($"{_relationshipType} not currently supported");
      }
    }

    public override bool Equals(object compareObject)
    {
      var otherField = compareObject as RelationshipModel;
      if (otherField == null) { return false; }

      return ToString() == otherField.ToString();
    }
  }
}
