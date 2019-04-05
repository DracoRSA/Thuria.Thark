using System.Text;
using System.Linq;
using System.Collections.Generic;

using Thuria.Thark.Core.Statement;
using Thuria.Thark.Core.Statement.Builders;
using Thuria.Thark.Core.Statement.Providers;
using Thuria.Thark.StatementBuilder.Providers;

namespace Thuria.Thark.StatementBuilder.Builders
{
  /// <summary>
  /// Statement Builder Base
  /// </summary>
  public abstract class StatementBuilderBase : IStatementBuilder
  {
    /// <inheritdoc />
    public IDatabaseProvider DatabaseProvider { get; private set; } = new SqlServerDatabaseProvider();

    /// <inheritdoc />
    public List<string> Errors { get; } = new List<string>();

    /// <inheritdoc />
    public IStatementBuilder WithDatabaseProvider(DatabaseProviderType databaseProviderType)
    {
      UpdateDatabaseProvider(databaseProviderType);
      return this;
    }

    /// <inheritdoc />
    public abstract void Clear();

    /// <inheritdoc />
    public abstract string Build();

    /// <inheritdoc />
    public abstract void DatabaseProviderChanged();

    /// <summary>
    /// Update the Database Provider to use
    /// </summary>
    /// <param name="databaseProviderType">Database Provider Type</param>
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

    /// <summary>
    /// Return a string representing the current Error(s)
    /// </summary>
    /// <returns>String representing the current Error(s)</returns>
    protected string GetErrorMessage()
    {
      var errorMessage = new StringBuilder();

      foreach (var currentError in Errors)
      {
        errorMessage.AppendLine(currentError);
      }

      return errorMessage.ToString();
    }

    /// <summary>
    /// Validate the Statement Requirements
    /// </summary>
    /// <returns>A boolean indicating whether the validation succeeded or not</returns>
    protected virtual bool ValidateStatementRequirement()
    {
      Errors.Clear();
      if (DatabaseProvider == null)
      {
        Errors.Add("Database Provider must be specified");
      }

      return !Errors.Any();
    }
  }
}