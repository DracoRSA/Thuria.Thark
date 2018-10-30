namespace Thuria.Thark.Core.Statement.Models
{
  /// <summary>
  /// Condition Data Model
  /// </summary>
  public interface IConditionModel : IBaseModel
  {
    /// <summary>
    /// Quote Character to use
    /// </summary>
    string Quote { get; }
  }
}