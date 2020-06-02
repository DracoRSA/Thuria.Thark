using System.Threading.Tasks;
using System.Collections.Generic;

using Thuria.Thark.Core.Models;
using Thuria.Thark.Core.Statement;
using Thuria.Thark.Core.DataAccess;

namespace Thuria.Thark.Core.Providers
{
  /// <summary>
  /// Sql Statement Execution Provider
  /// </summary>
  public interface IStatementExecutionProvider
  {
    /// <summary>
    /// Execute the Sql Action with the specified SQL Query (Async)
    /// </summary>
    /// <param name="executionAction">SQL Execution Action</param>
    /// <param name="sqlQuery">SQL Query to execute</param>
    /// <param name="actionParameters">Action Parameters (Optional)</param>
    /// <param name="dbContext">Database Context (Optional - Default Thark)</param>
    /// <param name="databaseProviderType">Database Provider Type (Default - SQL Server)</param>
    /// <returns>SQL Execution Result</returns>
    Task<ISqlExecutionResult> ExecuteAsync(SqlExecutionAction executionAction, 
                                           string sqlQuery,
                                           IList<IDataAccessParameter> actionParameters = null,
                                           string dbContext = "Thark",
                                           DatabaseProviderType databaseProviderType = DatabaseProviderType.SqlServer);
  }
}