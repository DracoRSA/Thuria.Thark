using System;
using System.Collections.Generic;

using Thuria.Thark.Core.DataAccess;

namespace Thuria.Thark.Core.Models
{
  /// <summary>
  /// Database Context Action Res8lt
  /// </summary>
  public interface IDbContextActionResult<T> where T : class
  {
    /// <summary>
    /// Action Result
    /// </summary>
    DbContextActionResult ActionResult { get; }

    /// <summary>
    /// Result from performing the Database Context Action (Optional)
    /// </summary>
    IEnumerable<T> Results { get; }

    /// <summary>
    /// Action Message (Warning, Error)
    /// </summary>
    string ActionMessage { get; }

    /// <summary>
    /// Exception that occurred while performing the Database Context Action (Optional)
    /// </summary>
    Exception Exception { get; }
  }
}
