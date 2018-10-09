using System;
using System.Data;

namespace Thuria.Thark.Core.DataAccess
{
  /// <summary>
  /// Database Context
  /// </summary>
  public interface IDatabaseContext : IDisposable
  {
    /// <summary>
    /// Database Connection
    /// </summary>
    IDbConnection DbConnection { get; }

    /// <summary>
    /// Database  Transaction Manager
    /// </summary>
    IDatabaseTransactionProvider DatabaseTransactionProvider { get; }

    /// <summary>
    /// Command Timeout
    /// </summary>
    int CommandTimeout { get; set; }

    /// <summary>
    /// Execute SQL Statement
    /// </summary>
    /// <param name="statementToExecute">Sql Statement to execute</param>
    /// <param name="sqlParameters">SQL Parameters</param>
    /// <returns>An integer specifying the number of rows affected</returns>
    int ExecuteSqlStatement(string statementToExecute, dynamic sqlParameters = null);

    /// <summary>
    /// Complete the transaction
    /// </summary>
    void Complete();
  }
}