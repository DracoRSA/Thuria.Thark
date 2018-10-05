using System.Text;
using System.Linq;
using System.Collections.Generic;

using Thuria.Thark.Core.Statement;
using Thuria.Thark.Core.Statement.Builders;
using Thuria.Thark.Core.Statement.Providers;
using Thuria.Thark.StatementBuilder.Providers;

namespace Thuria.Thark.StatementBuilder.Builders
{
  public abstract class StatementBuilderBase : IStatementBuilder
  {
    public IDatabaseProvider DatabaseProvider { get; private set; } = new SqlServerDatabaseProvider();

    public List<string> Errors { get; } = new List<string>();

    public IStatementBuilder WithDatabaseProvider(DatabaseProviderType databaseProviderType)
    {
      UpdateDatabaseProvider(databaseProviderType);
      return this;
    }

    public abstract string Build();

    public abstract void DatabaseProviderChanged();

    protected void UpdateDatabaseProvider(DatabaseProviderType databaseProviderType)
    {
      if (databaseProviderType == DatabaseProvider.DatabaseProviderType) { return; }

      switch (databaseProviderType)
      {
        case DatabaseProviderType.SqlServer:
          DatabaseProvider = new SqlServerDatabaseProvider();
          break;

        case DatabaseProviderType.Postgres:
          DatabaseProvider = new PostgresDatabaseProvider();
          break;

        case DatabaseProviderType.Firebird:
          DatabaseProvider = new FirebirdDatabaseProvider();
          break;

        default:
          throw new StatementBuilderException($"Unsupported Database Provider provided [{databaseProviderType}]");
      }

      DatabaseProviderChanged();
    }

    protected string GetErrorMessage()
    {
      var errorMessage = new StringBuilder();

      foreach (var currentError in Errors)
      {
        errorMessage.AppendLine(currentError);
      }

      return errorMessage.ToString();
    }

    protected virtual bool ValidateStatementRequirement()
    {
      Errors.Clear();
      if (DatabaseProvider == null) { Errors.Add("Database Provider must be specified"); }

      return !Errors.Any();
    }
  }
}