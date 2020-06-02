using System;
using Thuria.Thark.Core.DataAccess;

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
    /// <param name="contextAction">Context Action</param>
    /// <param name="isRequired">Required indicator (Default True)</param>
    public ThuriaConditionColumnAttribute(DbContextAction contextAction, bool isRequired = true)
    {
      ContextAction = contextAction;
      IsRequired  = isRequired;
    }

    /// <summary>
    /// Context Action
    /// </summary>
    public DbContextAction ContextAction { get; set; }

    /// <summary>
    /// Required Indicator
    /// </summary>
    public bool IsRequired { get; set; }
  }
}
