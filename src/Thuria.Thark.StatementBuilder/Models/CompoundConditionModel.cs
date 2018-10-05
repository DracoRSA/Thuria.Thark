using System;

using Thuria.Thark.Core.Statement;
using Thuria.Thark.Core.Statement.Models;

namespace Thuria.Thark.StatementBuilder.Models
{
  public class CompoundConditionModel : BaseModel, ICompoundConditionModel
  {
    public CompoundConditionModel(IConditionModel leftCondition, BooleanOperator booleanOperator, IConditionModel rightCondition)
    {
      LeftCondition   = leftCondition ?? throw new ArgumentNullException(nameof(leftCondition));
      BooleanOperator = booleanOperator;
      RightCondition  = rightCondition ?? throw new ArgumentNullException(nameof(rightCondition));
    }

    public IConditionModel LeftCondition { get; private set; }
    public IConditionModel RightCondition { get; private set; }
    public BooleanOperator BooleanOperator { get; private set; }

    public override string ToString()
    {
      return $"{LeftCondition}{DataMap.BooleanOperatorMap[BooleanOperator]}{RightCondition}";
    }

    public override bool Equals(object compareObject)
    {
      var otherField = compareObject as CompoundConditionModel;
      if (otherField == null) { return false; }

      return ToString() == otherField.ToString();
    }
  }
}
