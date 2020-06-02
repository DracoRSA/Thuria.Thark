using System;
using System.Data;

using Microsoft.Data.Sqlite;

using Thuria.Thark.Core.Statement;
using Thuria.Thark.Core.Providers;

namespace Thuria.Thark.DataAccess.Sqlite
{
  /// <summary>
  /// Sql Lite Database Connection Provider
  /// </summary>
  public class SqliteDbConnectionProvider : IDatabaseConnectionProvider
  {
    /// <inheritdoc />
    public DatabaseProviderType ProviderType { get; } = DatabaseProviderType.Sqlite;

    /// <inheritdoc />
    public IDbConnection GetConnection(string connectionString)
    {
      if (string.IsNullOrWhiteSpace(connectionString))
      {
        throw new ArgumentNullException(nameof(connectionString));
      }

      return new SqliteConnection(connectionString);
    }
  }
}
