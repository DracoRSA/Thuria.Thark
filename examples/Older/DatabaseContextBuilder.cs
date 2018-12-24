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
    private readonly IDatabaseContextProvider _databaseContextProvider;
    private readonly IDatabaseContextProviderStorage _databaseContextProviderStorage;
    private readonly IDatabaseContextInputParametersProvider _inputParametersProvider;
    private readonly IDatabaseContextOutputParametersProvider _outputParametersProvider;
    private readonly IDbConnectionParameterProvider _dbConnectionParameterProvider;

    private string _connectionString;
    private int _commandTimeout = 30;
    private bool _startTransaction;
    private IsolationLevel _isolationLevel;

    /// <summary>
    /// Create a new Database Context Builder
    /// </summary>
    /// <param name="dbConnectionProvider">DB Connection Provider</param>
    /// <param name="databaseContextProvider"></param>
    /// <param name="databaseContextProviderStorage"></param>
    /// <param name="inputParametersProvider">Input Parameters Provider</param>
    /// <param name="outputParametersProvider">Output Parameters Provider</param>
    /// <param name="dbConnectionParameterProvider">DbConnection Parameter Provider</param>
    public DatabaseContextBuilder(
      IDbConnectionProvider dbConnectionProvider,
      IDatabaseContextProvider databaseContextProvider,
      IDatabaseContextProviderStorage databaseContextProviderStorage,
      IDatabaseContextInputParametersProvider inputParametersProvider,
      IDatabaseContextOutputParametersProvider outputParametersProvider,
      IDbConnectionParameterProvider dbConnectionParameterProvider)
    {
      _dbConnectionProvider = dbConnectionProvider ?? throw new ArgumentNullException(nameof(dbConnectionProvider));
      _databaseContextProvider = databaseContextProvider ?? throw new ArgumentNullException(nameof(databaseContextProvider));
      _databaseContextProviderStorage = databaseContextProviderStorage ?? throw new ArgumentNullException(nameof(databaseContextProviderStorage));
      _inputParametersProvider = inputParametersProvider ?? throw new ArgumentNullException(nameof(inputParametersProvider));
      _outputParametersProvider = outputParametersProvider ?? throw new ArgumentNullException(nameof(outputParametersProvider));
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

      IDatabaseContext databaseContext;
      databaseContext = new SqlDatabaseContext(_connectionString, dbConnection, _dbConnectionParameterProvider)
        {
          CommandTimeout = _commandTimeout
        };

      if (_startTransaction)
      {
        databaseContext.DbConnection.BeginTransaction(_isolationLevel);
      }

      return databaseContext;
    }

    /// <inheritdoc />
    public async Task<IDatabaseContext> BuildEnlistedAsync()
    {
      ValidateBuilder();

      if (!_databaseContextProviderStorage.HasConnectionProvider)
      {
        _databaseContextProviderStorage.Register(_databaseContextProvider);
      }

      var dataModel = _databaseContextProviderStorage.DatabaseContextProvider.Get(_connectionString);
      if (dataModel == null)
      {
        var dbConnection = await _dbConnectionProvider.CreateDbConnectionAsync(_connectionString).ConfigureAwait(false);
        _databaseContextProviderStorage.DatabaseContextProvider.Register(_connectionString, dbConnection);

        dataModel = _databaseContextProviderStorage.DatabaseContextProvider.Get(_connectionString);
      }


      IDatabaseContext databaseContext;
      if (_useSqlCommand)
      {
        databaseContext = new SqlDatabaseContext(dataModel.ConnectionString, dataModel.DbConnection, _dbConnectionParameterProvider)
          {
            CommandTimeout = _commandTimeout
          };
      }
      else
      {
        databaseContext = new DapperDatabaseContext(dataModel.ConnectionString, dataModel.DbConnection, 
                                                    _databaseContextProvider, _inputParametersProvider, _outputParametersProvider)
          {
            CommandTimeout = _commandTimeout
          };
      }

      if (_startTransaction && (databaseContext.DbConnection.Transaction == null || databaseContext.DbConnection?.Connection == null))
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