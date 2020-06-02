using System.Collections.Generic;
using Thuria.Thark.Core.DataAccess;

namespace Thuria.Thark.Core.Models
{
  /// <summary>
  /// Sql Execution Result
  /// </summary>
  public interface ISqlExecutionResult
  {
    /// <summary>
    /// Execution Success indicator
    /// </summary>
    SqlExecutionActionResult ActionResult { get; }

    /// <summary>
    /// Rows Affected by the Action (Optional - Default 0)
    /// </summary>
    int RowsAffected { get; }

    /// <summary>
    /// Execution result Data (Optional - Default null)
    /// </summary>
    IEnumerable<object> ResultData { get; }

    /// <summary>
    /// Result Error message
    /// </summary>
    string ErrorMessage { get; }
  }
}
