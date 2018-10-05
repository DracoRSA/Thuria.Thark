using System;

namespace Thuria.Thark.StatementBuilder
{
  public class StatementBuilderException : Exception
  {
    public StatementBuilderException(string message) 
      : base(message)
    {
    }
  }
}