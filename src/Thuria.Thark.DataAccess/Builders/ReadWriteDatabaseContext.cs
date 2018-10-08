using System;
using System.Data;

using Thuria.Thark.Core.DataAccess;

namespace Thuria.Thark.DataAccess.Builders
{
  public class ReadWriteDatabaseContext : IDatabaseContext
  {
    public ReadWriteDatabaseContext(IDbConnection databaseConnection, NullDatabaseTransactionScopeManager nullDatabaseTransactionScopeManager)
    {
      throw new NotImplementedException();
    }
  }
}