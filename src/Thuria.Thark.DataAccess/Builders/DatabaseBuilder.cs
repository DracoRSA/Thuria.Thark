using System;
using System.Data;
using System.Data.SqlClient;

using Thuria.Thark.Core.Statement;
using Thuria.Thark.Core.DataAccess;
using Thuria.Thark.DataAccess.Context;
using Thuria.Thark.DataAccess.Providers;

namespace Thuria.Thark.DataAccess.Builders
{
  /// <inheritdoc />
  public class DatabaseBuilder : IDatabaseBuilder
  {
    private string _connectionString;
    private DatabaseProviderType _databaseProviderType;
    private bool _isReadonly;
    private int _commandTimeout = 30;

    /// <summary>
    /// Private Constructor for Database Builder
    /// </summary>
    private DatabaseBuilder()
    {
    }

    /// <summary>
    /// Create a new Database Builder object
    /// </summary>
    public static IDatabaseBuilder Create => new DatabaseBuilder();

    /// <inheritdoc />
    public IDatabaseBuilder WithDatabaseProviderType(DatabaseProviderType databaseProviderType)
    {
      _databaseProviderType = databaseProviderType;
      return this;
    }

    /// <inheritdoc />
    public IDatabaseBuilder WithCommandTimeout(int newCommandTimeout)
    {
      _commandTimeout = newCommandTimeout;
      return this;
    }

    /// <inheritdoc />
    public IDatabaseBuilder WithConnectionString(string connectionString)
    {
      _connectionString = connectionString;
      return this;
    }

    /// <inheritdoc />
    public IDatabaseBuilder AsReadonly()
    {
      _isReadonly = true;
      return this;
    }

    /// <inheritdoc />
    public IDatabaseContext Build()
    {
      ValidateBuilder();
      var databaseConnection = CreateDatabaseConnection();

      return _isReadonly
                  ? new ReadonlyDatabaseContext(databaseConnection, new NullDatabaseTransactionScopeProvider()) { CommandTimeout = _commandTimeout } 
                  : new ReadWriteDatabaseContext(databaseConnection, new NullDatabaseTransactionScopeProvider()) { CommandTimeout = _commandTimeout };
    }

    /// <inheritdoc />
    public IReadonlyDatabaseContext BuildReadonly()
    {
      ValidateBuilder();
      var databaseConnection = CreateDatabaseConnection();

      return new ReadonlyDatabaseContext(databaseConnection, new NullDatabaseTransactionScopeProvider());
    }

    /// <inheritdoc />
    public IReadWriteDatabaseContext BuildReadWrite()
    {
      ValidateBuilder();
      var databaseConnection = CreateDatabaseConnection();

      return new ReadWriteDatabaseContext(databaseConnection, new NullDatabaseTransactionScopeProvider());
    }

    private void ValidateBuilder()
    {
      if (string.IsNullOrWhiteSpace(_connectionString))
      {
        throw new Exception("Database Connection String is empty");
      }
    }

    private IDbConnection CreateDatabaseConnection()
    {
      switch (_databaseProviderType)
      {
        case DatabaseProviderType.SqlServer:
          return new SqlConnection(_connectionString);

        default:
          throw new Exception($"Database Provider [{_databaseProviderType}] is not currently supported");
      }
    }
  }
}
