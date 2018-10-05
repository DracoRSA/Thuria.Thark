namespace Thuria.Thark.Core.Statement.Models
{
  public interface ICompoundConditionModel : IBaseModel
  {
    IConditionModel LeftCondition { get; }
    IConditionModel RightCondition { get; }
    BooleanOperator BooleanOperator { get; }
  }
}
