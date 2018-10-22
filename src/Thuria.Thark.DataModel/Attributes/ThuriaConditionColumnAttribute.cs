using System;

namespace Thuria.Thark.DataModel.Attributes
{
  /// <summary>
  /// Thuria Condition Column Attribute
  /// </summary>
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
  public class ThuriaConditionColumnAttribute : Attribute
  {
    /// <summary>
    /// Thuria Condition Column Attribute constructor
    /// </summary>
    /// <param name="tharkAction">Thark Action</param>
    /// <param name="isRequired">Required indicator (Default True)</param>
    public ThuriaConditionColumnAttribute(TharkAction tharkAction, bool isRequired = true)
    {
      TharkAction = tharkAction;
      IsRequired  = isRequired;
    }

    /// <summary>
    /// Thark Action
    /// </summary>
    public TharkAction TharkAction { get; set; }

    /// <summary>
    /// Required Indicator
    /// </summary>
    public bool IsRequired { get; set; }
  }
}
