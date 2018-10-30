namespace Thuria.Thark.Core.Statement.Providers
{
  /// <summary>
  /// Database Provider
  /// </summary>
  public interface IDatabaseProvider
  {
    /// <summary>
    /// Database Provider Type
    /// </summary>
    DatabaseProviderType DatabaseProviderType { get; }

    /// <summary>
    /// Statement Open Quote
    /// </summary>
    string StatementOpenQuote { get; }

    /// <summary>
    /// Statement Close Quote
    /// </summary>
    string StatementCloseQuote { get; }
  }
}
