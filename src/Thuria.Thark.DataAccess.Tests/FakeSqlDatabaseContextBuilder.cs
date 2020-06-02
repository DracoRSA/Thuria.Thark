using Thuria.Thark.Core.Providers;
using Thuria.Thark.Core.DataAccess;
using Thuria.Thark.DataAccess.Builders;

namespace Thuria.Thark.DataAccess.Tests
{
  public class FakeSqlDatabaseContextBuilder : SqlDatabaseContextBuilder
  {
    public new static IDatabaseContextBuilder Create => new FakeSqlDatabaseContextBuilder();

    public IConnectionStringProvider InternalConnectionStringProvider => base.ConnectionStringProvider;
    public IDatabaseConnectionProvider InternalDatabaseConnectionProvider => base.DatabaseConnectionProvider;
    public IDatabaseTransactionProvider InternalDatabaseTransactionProvider => base.DatabaseTransactionProvider;
    public IStatementBuildProvider InternalStatementBuildProvider => base.StatementBuildProvider;
    public IDataModelPopulateProvider InternalDataModelPopulateProvider => base.DataModelPopulateProvider;
  }
}
