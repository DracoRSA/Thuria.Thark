using Thuria.Thark.Core.Statement;
using Thuria.Thark.Core.Statement.Providers;

namespace Thuria.Thark.StatementBuilder.Providers
{
  /// <summary>
  /// SQL Server Database Provider
  /// </summary>
  public class SqlServerDatabaseProvider : IDatabaseProvider
  {
    /// <inheritdoc />
    public DatabaseProviderType DatabaseProviderType { get; } = DatabaseProviderType.SqlServer;

    /// <inheritdoc />
    public string StatementOpenQuote { get; } = "[";

    /// <inheritdoc />
    public string StatementCloseQuote { get; } = "]";
  }
}