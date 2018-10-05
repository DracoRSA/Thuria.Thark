using Thuria.Thark.Core.Statement;
using Thuria.Thark.Core.Statement.Providers;

namespace Thuria.Thark.StatementBuilder.Providers
{
  public class PostgresDatabaseProvider : IDatabaseProvider
  {
    public DatabaseProviderType DatabaseProviderType { get; } = DatabaseProviderType.Postgres;
    public string StatementOpenQuote { get; } = "[";
    public string StatementCloseQuote { get; } = "]";
  }
}