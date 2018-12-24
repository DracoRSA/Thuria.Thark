using System;
using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MGS.Casino.Veyron.DataAccessInterfaces
{
  /// <summary>
  /// Database Context Contract
  /// </summary>
  public interface IDatabaseContext : IDisposable
  {
    /// <summary>
    /// Connection String
    /// </summary>
    string ConnectionString { get; }

    /// <summary>
    /// DbConnection
    /// </summary>
    IVeyronDbConnection DbConnection { get; }

    /// <summary>
    /// Command Timeout
    /// </summary>
    int CommandTimeout { get; set; }

    /// <summary>
    /// Execute Store Procedure
    /// </summary>
    /// <param name="storedProcedureName">Stored Procedure Name</param>
    /// <param name="storedProcedureParameters">Stored Procedure Input Parameters</param>
    /// <returns>A Dictionary containing the key value pairs of all values marked as output values</returns>
    Task<Dictionary<string, object>> ExecuteStoredProcedureAsync(string storedProcedureName, List<DataAccessParameter> storedProcedureParameters);

    /// <summary>
    /// Execute Store Procedure
    /// </summary>
    /// <param name="storedProcedureName">Stored Procedure Name</param>
    /// <param name="storedProcedureParameters">Stored Procedure Input Parameters</param>
    /// <returns>An enumerable list of objects containing the result</returns>
    Task<(IEnumerable<T>, Dictionary<string, object>)> ExecuteStoredProcedureAsync<T>(string storedProcedureName, List<DataAccessParameter> storedProcedureParameters)
      where T : class;

    /// <summary>
    /// Execute Store Procedure
    /// </summary>
    /// <param name="storedProcedureName">Stored Procedure Name</param>
    /// <param name="storedProcedureParameters">Stored Procedure Input Parameters</param>
    /// <returns>A Data Reader containing the result</returns>
    Task<IDataReader> ExecuteReaderAsync(string storedProcedureName, List<DataAccessParameter> storedProcedureParameters);

    /// <summary>
    /// Execute Store Procedure
    /// </summary>
    /// <param name="storedProcedureName">Stored Procedure Name</param>
    /// <param name="storedProcedureParameters">Stored Procedure Input Parameters</param>
    /// <returns>An enumerable list of objects containing the result</returns>
    Task<(IDataReader, Dictionary<string, object>)> ExecuteAsync(string storedProcedureName, List<DataAccessParameter> storedProcedureParameters);

    /// <summary>
    /// Complete the pending transaction
    /// </summary>
    void Complete();
  }
}
