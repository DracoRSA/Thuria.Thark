using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Concurrent;
using MGS.Casino.Veyron.DataAccessInterfaces;
using MGS.Casino.Veyron.DataAccessInterfaces.Metadata;

namespace MGS.Casino.Veyron.DataAccess
{
  /// <inheritdoc />
  public class SqlDatabaseContext : IDatabaseContext
  {
    private bool _isDisposing;
    private readonly int _maxDegreesOfParallelism = 10;
    private readonly IDbConnectionParameterProvider _dbConnectionParameterProvider;

    /// <summary>
    /// SQL Database Context constructor
    /// </summary>
    /// <param name="connectionString"></param>
    /// <param name="dbConnection"></param>
    /// <param name="dbConnectionParameterProvider"></param>
    public SqlDatabaseContext(string connectionString, IVeyronDbConnection dbConnection, IDbConnectionParameterProvider dbConnectionParameterProvider)
    {
      ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
      DbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
      _dbConnectionParameterProvider = dbConnectionParameterProvider ?? throw new ArgumentNullException(nameof(dbConnectionParameterProvider));
    }

    /// <summary>
    /// Dispose of the current object and any dependencies
    /// </summary>
    public void Dispose()
    {
      if (_isDisposing)
      {
        return;
      }

      _isDisposing = true;

      DbConnection?.Close();
      DbConnection?.Dispose();
      DbConnection = null;
    }

    /// <inheritdoc />
    public string ConnectionString { get; }

    /// <inheritdoc />
    public IVeyronDbConnection DbConnection { get; private set; }

    /// <inheritdoc />
    public int CommandTimeout { get; set; }

    /// <inheritdoc />
    public async Task<Dictionary<string, object>> ExecuteStoredProcedureAsync(string storedProcedureName, List<DataAccessParameter> storedProcedureParameters)
    {
      // Execute the stored procedure
      var (_, outputParameters) = await ExecuteSqlCommand(storedProcedureName, storedProcedureParameters, true);
      return outputParameters;
    }

    /// <inheritdoc />
    public async Task<(IEnumerable<T>, Dictionary<string, object>)> ExecuteStoredProcedureAsync<T>(string storedProcedureName, List<DataAccessParameter> storedProcedureParameters) where T : class
    {
      // Execute the stored procedure
      var (dataReader, outputParameters) = await ExecuteSqlCommand(storedProcedureName, storedProcedureParameters).ConfigureAwait(false);

      // Process and Set the return result
      var allDataModels = new List<T>();
      while (dataReader.Read())
      {
        var dataModel = (T)MapDataModel(typeof(T), dataReader);
        allDataModels.Add(dataModel);
      }

      dataReader.Close();

      return (allDataModels, outputParameters);
    }

    /// <inheritdoc />
    public async Task<IDataReader> ExecuteReaderAsync(string storedProcedureName, List<DataAccessParameter> storedProcedureParameters)
    {
      var (dataReader, _) = await ExecuteSqlCommand(storedProcedureName, storedProcedureParameters).ConfigureAwait(false);
      return dataReader;
    }

    /// <inheritdoc />
    public async Task<(IDataReader, Dictionary<string, object>)> ExecuteAsync(string storedProcedureName, List<DataAccessParameter> storedProcedureParameters)
    {
      return await ExecuteSqlCommand(storedProcedureName, storedProcedureParameters).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public void Complete()
    {
      DbConnection.Transaction?.Commit();
    }

    private Task<(IDataReader, Dictionary<string, object>)> ExecuteSqlCommand(string storedProcedureName, List<DataAccessParameter> storedProcedureParameters, bool closeReader = false)
    {
      var taskCompletionSource = new TaskCompletionSource<(IDataReader, Dictionary<string, object>)>();

      try
      {
        // Create the Stored Procedure Command
        var sqlCommand = CreateStoredProcedureSqlCommand(storedProcedureName);

        // Process the Input Parameters
        if (storedProcedureParameters != null && storedProcedureParameters.Any())
        {
          ProcessInputParameters(storedProcedureParameters, sqlCommand);
        }

        // Execute the stored procedure
        var dataReader = sqlCommand.ExecuteReader();

        // Process the Output Parameters
        var outputParameters = ProcessOutputParameters(storedProcedureParameters, sqlCommand.Parameters);

        // Close the reader if required
        if (closeReader)
        {
          dataReader.Close();
        }

        // Set the result
        taskCompletionSource.SetResult((dataReader, outputParameters));
      }
      catch (Exception runtimeException)
      {
        taskCompletionSource.SetException(runtimeException);
      }

      return taskCompletionSource.Task;
    }

    private void ProcessInputParameters(List<DataAccessParameter> storedProcedureParameters, IDbCommand sqlCommand)
    {
      var syncObject = new object();
      Parallel.ForEach(
        storedProcedureParameters,
        new ParallelOptions
          {
            MaxDegreeOfParallelism = _maxDegreesOfParallelism
          },
        (currentInputParameter) =>
          {
            var dbDataParameter = _dbConnectionParameterProvider.Convert(currentInputParameter);
            lock (syncObject)
            {
              sqlCommand.Parameters.Add(dbDataParameter);
            }
          });
    }

    private IDbCommand CreateStoredProcedureSqlCommand(string storedProcedureName)
    {
      var dbCommand = DbConnection.CreateCommand();
      dbCommand.CommandType = CommandType.StoredProcedure;
      dbCommand.CommandText = storedProcedureName;
      dbCommand.CommandTimeout = CommandTimeout;
      dbCommand.Transaction = DbConnection.Transaction;

      return dbCommand;
    }

    private Dictionary<string, object> ProcessOutputParameters(List<DataAccessParameter> inputParameters, IDataParameterCollection outputSqlParameters)
    {
      var outputParameters = new ConcurrentDictionary<string, object>();

      Parallel.ForEach(
        inputParameters,
        new ParallelOptions
          {
            MaxDegreeOfParallelism = _maxDegreesOfParallelism
          },
        (currentParameter) =>
          {
            if (currentParameter.Direction != DataParameterDirection.Output && currentParameter.Direction != DataParameterDirection.InputOutput)
            {
              return;
            }

            object parameterValue = null;
            if (outputSqlParameters.Contains(currentParameter.Name))
            {
              var sqlParameter = (SqlParameter)outputSqlParameters[currentParameter.Name];
              if (sqlParameter.Value != null && sqlParameter.Value != DBNull.Value)
              {
                parameterValue = sqlParameter.Value;
              }
              else
              {
                if (currentParameter.IsMandatory)
                {
                  throw new Exception($"Mandatory return value expected for {currentParameter.Name} but no value supplied");
                }

                parameterValue = currentParameter.DefaultValue;
              }
            }

            outputParameters.TryAdd(currentParameter.Name, parameterValue);
          });

      return outputParameters.ToDictionary(pair => pair.Key, pair => pair.Value);
    }

    private object MapDataModel(Type dataModelType, IDataReader dataReader)
    {
      var dataModel = Activator.CreateInstance(dataModelType);
      var allProperties = dataModel.GetType().GetProperties();

      foreach (var currentProperty in allProperties)
      {
        var dbColumnAttribute = currentProperty.GetCustomAttribute<VeyronDbColumnAttribute>();
        var dbColumnName = dbColumnAttribute == null ? currentProperty.Name : dbColumnAttribute.DbColumnName;
        var dbColumnValue = dataReader.GetValue(dbColumnName);

        currentProperty.SetValue(dataModel, dbColumnValue);
      }

      return dataModel;
    }
  }
}