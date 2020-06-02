using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Thuria.Thark.Core.Models;

namespace Thuria.Thark.Core.DataAccess
{
  /// <summary>
  /// Database Context
  /// </summary>
  public interface IDatabaseContext : IDisposable
  {
    /// <summary>
    /// Command Timeout
    /// </summary>
    int CommandTimeout { get; set; }

    /// <summary>
    /// Database Context Name
    /// </summary>
    string DbContextName { get; set; }

    /// <summary>
    /// Execute an Action (Async)
    /// </summary>
    /// <typeparam name="T">Data Type of Data Model</typeparam>
    /// <param name="dbContextAction">Database Context Action to execute</param>
    /// <param name="dataModel">Data Model to be used for the Action</param>
    /// <param name="sqlCommandText">Sql Command Text</param>
    /// <param name="dataParameters">Data Parameters</param>
    /// <returns>
    /// A DB Context Action Result object
    /// </returns>
    Task<IDbContextActionResult<T>> ExecuteActionAsync<T>(DbContextAction dbContextAction, 
                                                          T dataModel = default(T), 
                                                          string sqlCommandText = null,
                                                          IEnumerable<IDataAccessParameter> dataParameters = null) 
      where T : class; 

    /// <summary>
    /// Complete the transaction
    /// </summary>
    void Complete();
  }
}