using System.Linq;
using System.Data;
using System.Collections.Generic;

using Dapper;

using Thuria.Thark.Core.DataAccess;

namespace Thuria.Thark.DataAccess.Context
{
  /// <summary>
  /// Readonly Database Context
  /// </summary>
  public class ReadonlyDatabaseContext : DatabaseContext, IReadonlyDatabaseContext
  {
    /// <summary>
    /// Construct a Readonly Database Context
    /// </summary>
    /// <param name="dbConnection">Db Connection</param>
    /// <param name="databaseTransactionProvider">Database Transaction Manager</param>
    public ReadonlyDatabaseContext(IDbConnection dbConnection, IDatabaseTransactionProvider databaseTransactionProvider)
      : base(dbConnection, databaseTransactionProvider)
    {
    }

    /// <inheritdoc />
    public IEnumerable<T> Select<T>(string sqlStatement)
    {
      return DbConnection.Query<T>(sqlStatement);
    }

    /// <inheritdoc />
    public T SelectOne<T>(string sqlStatement)
    {
      return DbConnection.Query<T>(sqlStatement).FirstOrDefault();
    }
  }
}
