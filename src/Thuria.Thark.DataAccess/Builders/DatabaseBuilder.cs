using System;
using System.Data;

using Thuria.Thark.Core.Statement;
using Thuria.Thark.Core.DataAccess;

namespace Thuria.Thark.DataAccess.Builders
{
  public class DatabaseBuilder : IDatabaseBuilder
  {
    private string connectionString;
    private DatabaseProviderType databaseProviderType;
    private bool isReadonly;

    public static IDatabaseBuilder Create()
    {
      return new DatabaseBuilder();
    }

    public IDatabaseBuilder WithDatabaseProviderType(DatabaseProviderType databaseProviderType)
    {
      this.databaseProviderType = databaseProviderType;
      return this;
    }

    public IDatabaseBuilder WithConnectionString(string connectionString)
    {
      this.connectionString = connectionString;
      return this;
    }

    public IDatabaseBuilder AsReadonly()
    {
      this.isReadonly = true;
      return this;
    }

    public IDatabaseContext Build()
    {
      this.ValidateBuilder();
      var databaseConnection = this.CreateDatabaseConnection();

      return this.isReadonly
                  ? new ReadonlyDatabaseContext(databaseConnection, new NullDatabaseTransactionScopeManager())
                  : new ReadWriteDatabaseContext(databaseConnection, new NullDatabaseTransactionScopeManager());
    }

    public IReadonlyDatabaseContext BuildReadonly()
    {
      this.ValidateBuilder();
      var databaseConnection = this.CreateDatabaseConnection();

      return new ReadonlyDatabaseContext(databaseConnection, new NullDatabaseTransactionScopeManager());
    }

    public IReadWriteDatabaseContext BuildReadWrite()
    {
      this.ValidateBuilder();
      var databaseConnection = this.CreateDatabaseConnection();

      return new ReadWriteDatabaseContext(databaseConnection, new NullDatabaseTransactionScopeManager());
    }

    private void ValidateBuilder()
    {
      if (string.IsNullOrWhiteSpace(this.connectionString))
      {
        throw new Exception("Database Connection String is empty");
      }
    }

    private IDbConnection CreateDatabaseConnection()
    {
      switch (this.databaseProviderType)
      {
        case DatabaseProviderType.SqlServer:
          return new SqlConnection(this.connectionString);

        default:
          throw new Exception($"Database Provider [{this.databaseProviderType}] is not currently supported");
      }
    }
  }
}
