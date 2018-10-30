namespace Thuria.Thark.Core.Statement.Models
{
  /// <summary>
  /// Compound Condition Data Model
  /// </summary>
  public interface ICompoundConditionModel : IBaseModel
  {
    /// <summary>
    /// Left Condition
    /// </summary>
    IConditionModel LeftCondition { get; }

    /// <summary>
    /// Right Condition
    /// </summary>
    IConditionModel RightCondition { get; }

    /// <summary>
    /// Boolean Operator
    /// </summary>
    BooleanOperator BooleanOperator { get; }
  }
}
