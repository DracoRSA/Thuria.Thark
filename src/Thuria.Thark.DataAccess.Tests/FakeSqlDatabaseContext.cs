using System.Data;

using Thuria.Thark.Core.Providers;
using Thuria.Thark.DataAccess.Context;

namespace Thuria.Thark.DataAccess.Tests
{
  public class FakeSqlDatabaseContext : SqlDatabaseContext
  {
    public FakeSqlDatabaseContext(IConnectionStringProvider connectionStringProvider, 
                                  IDatabaseConnectionProvider databaseConnectionProvider, 
                                  IDatabaseTransactionProvider databaseTransactionProvider,
                                  IStatementBuildProvider sqlStatementBuildProvider,
                                  IDataModelPopulateProvider dataModelPopulateProvider) 
      : base(connectionStringProvider, databaseConnectionProvider, databaseTransactionProvider, sqlStatementBuildProvider, dataModelPopulateProvider)
    {
    }

    public IDbConnection InternalDbConnection => base.DbConnection;
  }
}