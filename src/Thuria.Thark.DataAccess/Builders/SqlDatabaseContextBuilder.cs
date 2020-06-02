using System;

using Thuria.Thark.Core.Providers;
using Thuria.Thark.Core.DataAccess;
using Thuria.Thark.DataAccess.Context;
using Thuria.Thark.DataAccess.Providers;

namespace Thuria.Thark.DataAccess.Builders
{
  /// <summary>
  /// SQL Database Context Builder
  /// </summary>
  public class SqlDatabaseContextBuilder : IDatabaseContextBuilder
  {
    private string _dbContextName = "Thark";
    private int _commandTimeout = 30;

    /// <summary>
    /// Create a new Database Builder object
    /// </summary>
    public static IDatabaseContextBuilder Create => new SqlDatabaseContextBuilder();

    /// <summary>
    /// Connection String Provider
    /// </summary>
    protected IConnectionStringProvider ConnectionStringProvider { get; private set; }

    /// <summary>
    /// Database ConnectionProvider
    /// </summary>
    protected IDatabaseConnectionProvider DatabaseConnectionProvider { get; private set; }

    /// <summary>
    /// Database Transaction Provider
    /// </summary>
    protected IDatabaseTransactionProvider DatabaseTransactionProvider { get; set; }

    /// <summary>
    /// Statement Build Provider
    /// </summary>
    protected IStatementBuildProvider StatementBuildProvider { get; set; }

    /// <summary>
    /// Data Model Populate Provider
    /// </summary>
    protected IDataModelPopulateProvider DataModelPopulateProvider { get; set; }

    /// <inheritdoc />
    public IDatabaseContextBuilder WithConnectionStringProvider(IConnectionStringProvider connectionStringProvider)
    {
      if (connectionStringProvider == null)
      {
        throw new ArgumentNullException(nameof(connectionStringProvider));
      }

      ConnectionStringProvider = connectionStringProvider;
      return this;
    }

    /// <inheritdoc />
    public IDatabaseContextBuilder WithDatabaseConnectionProvider(IDatabaseConnectionProvider databaseConnectionProvider)
    {
      if (databaseConnectionProvider == null)
      {
        throw new ArgumentNullException(nameof(databaseConnectionProvider));
      }

      DatabaseConnectionProvider = databaseConnectionProvider;
      return this;
    }

    /// <inheritdoc />
    public IDatabaseContextBuilder WithDatabaseTransactionProvider(IDatabaseTransactionProvider databaseTransactionProvider)
    {
      if (databaseTransactionProvider == null)
      {
        throw new ArgumentNullException(nameof(databaseTransactionProvider));
      }

      DatabaseTransactionProvider = databaseTransactionProvider;
      return this;
    }

    /// <inheritdoc />
    public IDatabaseContextBuilder WithStatementBuildProvider(IStatementBuildProvider statementBuildProvider)
    {
      if (statementBuildProvider == null)
      {
        throw new ArgumentNullException(nameof(statementBuildProvider));
      }

      StatementBuildProvider = statementBuildProvider;
      return this;
    }

    /// <inheritdoc />
    public IDatabaseContextBuilder WithDataModelPopulateProvider(IDataModelPopulateProvider dataModelPopulateProvider)
    {
      if (dataModelPopulateProvider == null)
      {
        throw new ArgumentNullException(nameof(dataModelPopulateProvider));
      }

      DataModelPopulateProvider = dataModelPopulateProvider;
      return this;
    }

    /// <inheritdoc />
    public IDatabaseContextBuilder WithCommandTimeout(int newCommandTimeout)
    {
      _commandTimeout = newCommandTimeout;
      return this;
    }

    /// <inheritdoc />
    public IDatabaseContextBuilder WithDatabaseContextName(string dbContextName)
    {
      if (string.IsNullOrWhiteSpace(dbContextName))
      {
        throw new ArgumentNullException(nameof(dbContextName));
      }

      _dbContextName = dbContextName;
      return this;
    }

    /// <inheritdoc />
    public IDatabaseContext Build()
    {
      ValidateBuilder();

      if (DatabaseTransactionProvider == null)
      {
        DatabaseTransactionProvider = new NullDatabaseTransactionScopeProvider();
      }

      return new SqlDatabaseContext(ConnectionStringProvider, 
                                    DatabaseConnectionProvider, 
                                    DatabaseTransactionProvider, 
                                    StatementBuildProvider, 
                                    DataModelPopulateProvider)
               {
                 CommandTimeout = _commandTimeout,
                 DbContextName  = _dbContextName
               };
    }

    private void ValidateBuilder()
    {
      if (ConnectionStringProvider == null)
      {
        throw new ArgumentException("Connection String Provider must be specified", nameof(ConnectionStringProvider));
      }

      if (DatabaseConnectionProvider == null)
      {
        throw new ArgumentException("Database Connection Provider must be specified", nameof(DatabaseConnectionProvider));
      }

      if (StatementBuildProvider == null)
      {
        throw new ArgumentException("Statement Build Provider must be specified", nameof(StatementBuildProvider));
      }

      if (DataModelPopulateProvider == null)
      {
        throw new ArgumentException("Data Model Populate Provider must be specified", nameof(DataModelPopulateProvider));
      }
    }
  }
}
