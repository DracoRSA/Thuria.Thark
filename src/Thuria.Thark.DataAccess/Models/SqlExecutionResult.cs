using System.Collections.Generic;
using Thuria.Thark.Core.DataAccess;

namespace Thuria.Thark.DataAccess.Models
{
  /// <summary>
  /// SQL Execution Result
  /// </summary>
  public class SqlExecutionResult
  {
    /// <summary>
    /// Sql Execution Result Constructor
    /// </summary>
    /// <param name="actionResult">SQL Execution Action Result</param>
    /// <param name="rowsAffected">Rows Affected (Optional - Default 0)</param>
    /// <param name="resultData">Execution Result Data (Optional - Default null)</param>
    /// <param name="errorMessage">Execution Error Message (Optional - Default null)</param>
    public SqlExecutionResult(SqlExecutionActionResult actionResult, int rowsAffected = 0, IEnumerable<object> resultData = null, string errorMessage = null)
    {
      ActionResult = actionResult;
      RowsAffected = rowsAffected;
      ResultData   = resultData;
      ErrorMessage = errorMessage;
    }

    /// <summary>
    /// Execution Success indicator
    /// </summary>
    public SqlExecutionActionResult ActionResult { get; }

    /// <summary>
    /// Rows Affected by the Action (Optional - Default 0)
    /// </summary>
    public int RowsAffected { get; }

    /// <summary>
    /// Execution result Data (Optional - Default null)
    /// </summary>
    public IEnumerable<object> ResultData { get; }

    /// <summary>
    /// Result Error message
    /// </summary>
    public string ErrorMessage { get; }
  }
}
