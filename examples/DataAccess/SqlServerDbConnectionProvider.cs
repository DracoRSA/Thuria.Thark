using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using MGS.Casino.Veyron.DataAccessInterfaces;

namespace MGS.Casino.Veyron.DataAccess
{
  /// <summary>
  /// SQL Server DbConnection Provider
  /// </summary>
  public class SqlServerDbConnectionProvider : IDbConnectionProvider
  {
    /// <inheritdoc />
    public async Task<IVeyronDbConnection> CreateDbConnectionAsync(string connectionString)
    {
      if (string.IsNullOrWhiteSpace(connectionString))
      {
        throw new ArgumentNullException(nameof(connectionString));
      }

      var sqlConnection = new SqlConnection(connectionString);
      await sqlConnection.OpenAsync().ConfigureAwait(false);

      return new VeyronSqlConnection(sqlConnection);
    }
  }
}
