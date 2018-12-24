using System;
using System.Data;
using System.Threading.Tasks;
using MGS.Casino.Veyron.DataAccessInterfaces;

namespace MGS.Casino.Veyron.DataAccess
{
  /// <inheritdoc />
  public class DatabaseContextBuilder : IDatabaseContextBuilder
  {
    private readonly IDbConnectionProvider _dbConnectionProvider;
    private readonly IDbConnectionParameterProvider _dbConnectionParameterProvider;

    private string _connectionString;
    private int _commandTimeout = 30;
    private bool _startTransaction;
    private IsolationLevel _isolationLevel;

    /// <summary>
    /// Create a new Database Context Builder
    /// </summary>
    /// <param name="dbConnectionProvider">DB Connection Provider</param>
    /// <param name="dbConnectionParameterProvider">DbConnection Parameter Provider</param>
    public DatabaseContextBuilder(
      IDbConnectionProvider dbConnectionProvider,
      IDbConnectionParameterProvider dbConnectionParameterProvider)
    {
      _dbConnectionProvider = dbConnectionProvider ?? throw new ArgumentNullException(nameof(dbConnectionProvider));
      _dbConnectionParameterProvider = dbConnectionParameterProvider ?? throw new ArgumentNullException(nameof(dbConnectionParameterProvider));
    }

    /// <inheritdoc />
    public IDatabaseContextBuilder WithConnectionString(string connectionString)
    {
      _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
      return this;
    }

    /// <inheritdoc />
    public IDatabaseContextBuilder WithTransaction(IsolationLevel isolationLevel)
    {
      _startTransaction = true;
      _isolationLevel = isolationLevel;
      return this;
    }

    /// <inheritdoc />
    public IDatabaseContextBuilder WithCommandTimeout(int commandTimeout)
    {
      _commandTimeout = commandTimeout;
      return this;
    }

    /// <inheritdoc />
    public async Task<IDatabaseContext> BuildAsync()
    {
      ValidateBuilder();

      var dbConnection = await _dbConnectionProvider.CreateDbConnectionAsync(_connectionString).ConfigureAwait(false);

      IDatabaseContext databaseContext = new SqlDatabaseContext(_connectionString, dbConnection, _dbConnectionParameterProvider)
        {
          CommandTimeout = _commandTimeout
        };

      if (_startTransaction)
      {
        databaseContext.DbConnection.BeginTransaction(_isolationLevel);
      }

      return databaseContext;
    }

    private void ValidateBuilder()
    {
      if (string.IsNullOrWhiteSpace(_connectionString))
      {
        throw new Exception("No Database Connection String specified");
      }
    }
  }
}