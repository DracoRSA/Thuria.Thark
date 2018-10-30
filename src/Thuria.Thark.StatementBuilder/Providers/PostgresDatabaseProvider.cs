using Thuria.Thark.Core.Statement;
using Thuria.Thark.Core.Statement.Providers;

namespace Thuria.Thark.StatementBuilder.Providers
{
  /// <summary>
  /// Postgres Database Provider
  /// </summary>
  public class PostgresDatabaseProvider : IDatabaseProvider
  {
    /// <inheritdoc />
    public DatabaseProviderType DatabaseProviderType { get; } = DatabaseProviderType.Postgres;

    /// <inheritdoc />
    public string StatementOpenQuote { get; } = "[";

    /// <inheritdoc />
    public string StatementCloseQuote { get; } = "]";
  }
}