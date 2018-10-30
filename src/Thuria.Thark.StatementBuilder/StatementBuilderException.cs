using System;

namespace Thuria.Thark.StatementBuilder
{
  /// <summary>
  /// Statement Builder Exception
  /// </summary>
  public class StatementBuilderException : Exception
  {
    /// <summary>
    /// Statement Builder Exception constructor
    /// </summary>
    public StatementBuilderException(string message) 
      : base(message)
    {
    }
  }
}