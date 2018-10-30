using Thuria.Thark.Core.Statement;
using Thuria.Thark.Core.Statement.Providers;

namespace Thuria.Thark.StatementBuilder.Providers
{
  /// <summary>
  /// Firebird Database Provider
  /// </summary>
  public class FirebirdDatabaseProvider : IDatabaseProvider
  {
    /// <inheritdoc />
    public DatabaseProviderType DatabaseProviderType { get; } = DatabaseProviderType.Firebird;

    /// <inheritdoc />
    public string StatementOpenQuote { get; } = "\"";

    /// <inheritdoc />
    public string StatementCloseQuote { get; } = "\"";
  }
}