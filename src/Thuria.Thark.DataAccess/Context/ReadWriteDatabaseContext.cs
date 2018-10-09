using System.Data;

using Dapper;

using Thuria.Thark.Core.DataAccess;

namespace Thuria.Thark.DataAccess.Context
{
  /// <summary>
  /// Read / Write Database Context
  /// </summary>
  public class ReadWriteDatabaseContext : ReadonlyDatabaseContext, IReadWriteDatabaseContext
  {
    /// <summary>
    /// Construct Read / Write Database Context
    /// </summary>
    /// <param name="dbConnection">Db Connection</param>
    /// <param name="databaseTransactionProvider">Database Transaction Manager</param>
    public ReadWriteDatabaseContext(IDbConnection dbConnection, IDatabaseTransactionProvider databaseTransactionProvider)
      : base(dbConnection, databaseTransactionProvider)
    {
    }

    /// <inheritdoc />
    public object Insert<T>(string sqlStatement)
    {
      return DbConnection.QuerySingleOrDefault<object>(sqlStatement);
    }

    /// <inheritdoc />
    public int Update<T>(string sqlStatement)
    {
      return ExecuteSqlStatement(sqlStatement);
    }

    /// <inheritdoc />
    public void Delete<T>(string sqlStatement)
    {
      throw new System.NotImplementedException();
    }
  }
}
