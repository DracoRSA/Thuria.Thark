using System;

using Thuria.Thark.Core.DataAccess;
using Thuria.Thark.DataAccess.Context;
using Thuria.Thark.DataAccess.Providers;

namespace Thuria.Thark.DataAccess.Builders
{
  /// <inheritdoc />
  public class DatabaseContextBuilder : IDatabaseContextBuilder
  {
    private string _connectionString;
    private IDatabaseConnectionProvider _databaseConnectionProvider;
    private bool _isReadonly;
    private int _commandTimeout = 30;

    /// <summary>
    /// Create a new Database Builder object
    /// </summary>
    public static IDatabaseContextBuilder Create => new DatabaseContextBuilder();

    /// <inheritdoc />
    public IDatabaseContextBuilder WithDatabaseConnectionProvider(IDatabaseConnectionProvider databaseConnectionProvider)
    {
      if (databaseConnectionProvider == null) { throw new ArgumentNullException(nameof(databaseConnectionProvider)); }

      _databaseConnectionProvider = databaseConnectionProvider;
      return this;
    }

    /// <inheritdoc />
    public IDatabaseContextBuilder WithCommandTimeout(int newCommandTimeout)
    {
      _commandTimeout = newCommandTimeout;
      return this;
    }

    /// <inheritdoc />
    public IDatabaseContextBuilder WithConnectionString(string connectionString)
    {
      _connectionString = connectionString;
      return this;
    }

    /// <inheritdoc />
    public IDatabaseContextBuilder AsReadonly()
    {
      _isReadonly = true;
      return this;
    }

    /// <inheritdoc />
    public IDatabaseContext Build()
    {
      ValidateBuilder();
      var databaseConnection = _databaseConnectionProvider.GetConnection(_connectionString);

      return _isReadonly
                  ? new ReadonlyDatabaseContext(databaseConnection, new NullDatabaseTransactionScopeProvider()) { CommandTimeout = _commandTimeout } 
                  : new ReadWriteDatabaseContext(databaseConnection, new NullDatabaseTransactionScopeProvider()) { CommandTimeout = _commandTimeout };
    }

    /// <inheritdoc />
    public IReadonlyDatabaseContext BuildReadonly()
    {
      ValidateBuilder();
      var databaseConnection = _databaseConnectionProvider.GetConnection(_connectionString);

      return new ReadonlyDatabaseContext(databaseConnection, new NullDatabaseTransactionScopeProvider());
    }

    /// <inheritdoc />
    public IReadWriteDatabaseContext BuildReadWrite()
    {
      ValidateBuilder();
      var databaseConnection = _databaseConnectionProvider.GetConnection(_connectionString);

      return new ReadWriteDatabaseContext(databaseConnection, new NullDatabaseTransactionScopeProvider());
    }

    private void ValidateBuilder()
    {
      if (string.IsNullOrWhiteSpace(_connectionString))
      {
        throw new ArgumentException("Database Connection string must be specified", nameof(_connectionString));
      }

      if (_databaseConnectionProvider == null)
      {
        throw new ArgumentException("Connection Provider must be specified", nameof(_databaseConnectionProvider));
      }
    }
  }
}
