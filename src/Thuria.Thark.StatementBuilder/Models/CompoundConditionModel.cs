using System;

using Thuria.Thark.Core.Statement;
using Thuria.Thark.Core.Statement.Models;

namespace Thuria.Thark.StatementBuilder.Models
{
  /// <summary>
  /// Compound Condition Data Model
  /// </summary>
  public class CompoundConditionModel : BaseModel, ICompoundConditionModel
  {
    /// <summary>
    /// Compound Condition Data Model constructor
    /// </summary>
    /// <param name="leftCondition">Left Condition</param>
    /// <param name="booleanOperator">Boolean Operator</param>
    /// <param name="rightCondition">Right Condition</param>
    public CompoundConditionModel(IConditionModel leftCondition, BooleanOperator booleanOperator, IConditionModel rightCondition)
    {
      LeftCondition   = leftCondition ?? throw new ArgumentNullException(nameof(leftCondition));
      BooleanOperator = booleanOperator;
      RightCondition  = rightCondition ?? throw new ArgumentNullException(nameof(rightCondition));
    }

    /// <inheritdoc />
    public IConditionModel LeftCondition { get; private set; }

    /// <inheritdoc />
    public IConditionModel RightCondition { get; private set; }

    /// <inheritdoc />
    public BooleanOperator BooleanOperator { get; private set; }

    /// <summary>
    /// Create a string representation of the Compound Condition
    /// </summary>
    /// <returns>A string representation of the Compound Condition</returns>
    public override string ToString()
    {
      return $"{LeftCondition}{DataMap.BooleanOperatorMap[BooleanOperator]}{RightCondition}";
    }

    /// <inheritdoc />
    public override bool Equals(object compareObject)
    {
      var otherField = compareObject as CompoundConditionModel;
      if (otherField == null) { return false; }

      return ToString() == otherField.ToString();
    }
  }
}
