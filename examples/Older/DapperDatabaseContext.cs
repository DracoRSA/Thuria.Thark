using System;
using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using Dapper;
using MGS.Casino.Veyron.DataAccessInterfaces;

namespace MGS.Casino.Veyron.DataAccess
{
  /// <inheritdoc />
  public class DapperDatabaseContext : IDatabaseContext
  {
    private bool _isDisposing;
    private readonly IDatabaseContextProvider _databaseContextProvider;
    private readonly IDatabaseContextInputParametersProvider _inputParametersProvider;
    private readonly IDatabaseContextOutputParametersProvider _outputParametersProvider;

    /// <summary>
    /// Create new Database Context
    /// </summary>
    /// <param name="connectionString">Connection String</param>
    /// <param name="dbConnection">Veyron Db Connection</param>
    /// <param name="databaseContextProvider"></param>
    /// <param name="inputParametersProvider">Input Parameters Provider</param>
    /// <param name="outputParametersProvider">Output Parameters Provider</param>
    public DapperDatabaseContext(string connectionString, IVeyronDbConnection dbConnection, IDatabaseContextProvider databaseContextProvider, 
                                 IDatabaseContextInputParametersProvider inputParametersProvider, IDatabaseContextOutputParametersProvider outputParametersProvider)
    {
      ConnectionString          = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
      DbConnection              = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
      _databaseContextProvider  = databaseContextProvider;
      _inputParametersProvider  = inputParametersProvider ?? throw new ArgumentNullException(nameof(inputParametersProvider));
      _outputParametersProvider = outputParametersProvider ?? throw new ArgumentNullException(nameof(outputParametersProvider));
    }

    /// <summary>
    /// Dispose Database Context
    /// </summary>
    public void Dispose()
    {
      if (_isDisposing)
      {
        return;
      }

      _isDisposing = true;

      if (_databaseContextProvider == null)
      {
        DbConnection?.Close();
        DbConnection?.Dispose();
        DbConnection = null;
      }

      _databaseContextProvider?.UnRegister(ConnectionString);
    }

    /// <inheritdoc />
    public string ConnectionString { get; }

    /// <inheritdoc />
    public IVeyronDbConnection DbConnection { get; private set; }

    /// <inheritdoc />
    public int CommandTimeout { get; set; } = 30;

    /// <inheritdoc />
    public async Task<Dictionary<string, object>> ExecuteStoredProcedureAsync(string storedProcedureName, List<DataAccessParameter> storedProcedureParameters)
    {
      // Process the input parameters
      var procParameters = (DynamicParameters) _inputParametersProvider.ProcessParameters(storedProcedureParameters, true);

      // Execute the stored procedure
      await DbConnection.QueryAsync(storedProcedureName, procParameters, commandType: CommandType.StoredProcedure, 
                                    commandTimeout: CommandTimeout, transaction: DbConnection.Transaction)
                        .ConfigureAwait(false);

      // Process the Output Values
      return _outputParametersProvider.ProcessParameters(storedProcedureParameters, procParameters, true);
    }

    /// <inheritdoc />
    public async Task<(IEnumerable<T>, Dictionary<string, object>)> ExecuteStoredProcedureAsync<T>(string storedProcedureName, List<DataAccessParameter> storedProcedureParameters)
      where T : class
    {
      // Process the input parameters
      var procParameters = (DynamicParameters) _inputParametersProvider.ProcessParameters(storedProcedureParameters, true);

      // Execute the stored procedure
      var queryResult = await DbConnection.QueryAsync<T>(storedProcedureName, procParameters, commandType: CommandType.StoredProcedure,
                                                         commandTimeout: CommandTimeout, transaction: DbConnection.Transaction)
                                          .ConfigureAwait(false);

      // Process the Output Values
      var outputValues = _outputParametersProvider.ProcessParameters(storedProcedureParameters, procParameters, true);

      // Return the mapped objects and the possible output values
      return (queryResult.AsList(), outputValues);
    }

    /// <inheritdoc />
    public async Task<IDataReader> ExecuteReaderAsync(string storedProcedureName, List<DataAccessParameter> storedProcedureParameters)
    {
      var (dataReader, _) = await ExecuteAsync(storedProcedureName, storedProcedureParameters).ConfigureAwait(false);
      return dataReader;
    }

    /// <inheritdoc />
    public async Task<(IDataReader, Dictionary<string, object>)> ExecuteAsync(string storedProcedureName, List<DataAccessParameter> storedProcedureParameters) 
    {
      // Process the input parameters
      var procParameters = (DynamicParameters)_inputParametersProvider.ProcessParameters(storedProcedureParameters, true);

      // Execute the stored procedure
      var queryResult = await DbConnection.ExecuteReaderAsync(storedProcedureName, procParameters, commandType: CommandType.StoredProcedure,
                                                              commandTimeout: CommandTimeout, transaction: DbConnection.Transaction)
                                          .ConfigureAwait(false);

      // Process the Output Values
      var outputValues = _outputParametersProvider.ProcessParameters(storedProcedureParameters, procParameters, true);

      // Return the mapped objects and the possible output values
      return (queryResult, outputValues);
    }

    /// <inheritdoc />
    public void Complete()
    {
      DbConnection.Transaction?.Commit();
    }
  }
}
