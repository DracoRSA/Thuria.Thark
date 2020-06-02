using System;
using System.Data;
using System.Data.SqlClient;

using Thuria.Thark.Core.Providers;
using Thuria.Thark.Core.Statement;

namespace Thuria.Thark.DataAccess.SqlServer
{
  /// <summary>
  /// SQL Server Database Connection Provider
  /// </summary>
  public class SqlServerDbConnectionProvider : IDatabaseConnectionProvider
  {
    /// <inheritdoc />
    public DatabaseProviderType ProviderType { get; } = DatabaseProviderType.SqlServer;

    /// <inheritdoc />
    public IDbConnection GetConnection(string connectionString)
    {
      if (string.IsNullOrWhiteSpace(connectionString))
      {
        throw new ArgumentNullException(nameof(connectionString));
      }

      return new SqlConnection(connectionString);
    }
  }
}
