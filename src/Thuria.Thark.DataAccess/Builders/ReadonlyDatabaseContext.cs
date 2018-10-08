using System.Linq;
using System.Data;
using System.Collections.Generic;

using Dapper;

using Thuria.Thark.Core.DataAccess;

namespace Thuria.Thark.DataAccess.Builders
{
  public class ReadonlyDatabaseContext : DatabaseContext, IReadonlyDatabaseContext
  {
    public ReadonlyDatabaseContext(IDbConnection dbConnection, IDatabaseTransactionManager databaseTransactionManager)
      : base(dbConnection, databaseTransactionManager)
    {
    }

    public IEnumerable<T> Select<T>(string sqlStatement)
    {
      return this.DbConnection.Query<T>(sqlStatement);
    }

    public T SelectOne<T>(string sqlStatement)
    {
      return this.DbConnection.Query<T>(sqlStatement).FirstOrDefault();
    }
  }
}
