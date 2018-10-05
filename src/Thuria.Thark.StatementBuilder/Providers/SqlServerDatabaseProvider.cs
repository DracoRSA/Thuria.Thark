using Thuria.Thark.Core.Statement;
using Thuria.Thark.Core.Statement.Providers;

namespace Thuria.Thark.StatementBuilder.Providers
{
  public class SqlServerDatabaseProvider : IDatabaseProvider
  {
    public DatabaseProviderType DatabaseProviderType { get; } = DatabaseProviderType.SqlServer;
    public string StatementOpenQuote { get; } = "[";
    public string StatementCloseQuote { get; } = "]";
  }
}