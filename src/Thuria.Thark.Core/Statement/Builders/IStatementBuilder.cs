using System.Collections.Generic;

using Thuria.Thark.Core.Statement.Providers;

namespace Thuria.Thark.Core.Statement.Builders
{
  public interface IStatementBuilder
  {
    IDatabaseProvider DatabaseProvider { get; }
    List<string> Errors { get; }

    IStatementBuilder WithDatabaseProvider(DatabaseProviderType databaseProvider);
    void DatabaseProviderChanged();
    string Build();
  }
}