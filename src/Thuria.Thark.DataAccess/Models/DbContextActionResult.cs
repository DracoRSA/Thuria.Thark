using System;
using System.Text;
using System.Collections.Generic;

using Thuria.Thark.Core.Models;
using Thuria.Thark.Core.DataAccess;

namespace Thuria.Thark.DataAccess.Models
{
  /// <summary>
  /// Database Context Action Result
  /// </summary>
  public class DbContextActionResult<T> : IDbContextActionResult<T> where T : class
  {
    private readonly StringBuilder _actionMessages = new StringBuilder();

    /// <inheritdoc />
    public DbContextActionResult ActionResult { get; private set; }

    /// <inheritdoc />
    public IEnumerable<T> Results { get; private set; }

    /// <inheritdoc />
    public string ActionMessage => _actionMessages.ToString();

    /// <inheritdoc />
    public Exception Exception { get; private set; }

    /// <summary>
    /// Set a successful result
    /// </summary>
    /// <param name="actionResults">The Action Results to attach</param>
    public void SetSuccessResult(IEnumerable<T> actionResults = null)
    {
      ActionResult = DbContextActionResult.Success;
      Results      = actionResults;
      Exception    = null;

      _actionMessages.Clear();
    }

    /// <summary>
    /// Set a Warning Result
    /// </summary>
    /// <param name="warningMessage">Warning Message</param>
    public void SetWarningResult(string warningMessage)
    {
      if (string.IsNullOrWhiteSpace(warningMessage))
      {
        throw new ArgumentNullException(nameof(warningMessage));
      }

      ActionResult = DbContextActionResult.Warning;
      Results      = null;
      Exception    = null;

      warningMessage = warningMessage.Replace("Warning: ", "");
      _actionMessages.AppendLine($"Warning: {warningMessage}");
    }

    /// <summary>
    /// Set an Error Result
    /// </summary>
    /// <param name="errorMessage">Error Message</param>
    public void SetErrorResult(string errorMessage)
    {
      if (string.IsNullOrWhiteSpace(errorMessage))
      {
        throw new ArgumentNullException(nameof(errorMessage));
      }

      ActionResult = DbContextActionResult.Error;
      Results      = null;
      Exception    = null;

      errorMessage = errorMessage.Replace("Error: ", "");
      _actionMessages.AppendLine($"Error: {errorMessage}");
    }

    /// <summary>
    /// Set an Exception Result
    /// </summary>
    /// <param name="runtimeException">Runtime Exception</param>
    public void SetExceptionResult(Exception runtimeException)
    {
      if (runtimeException == null)
      {
        throw new ArgumentNullException(nameof(runtimeException));
      }

      ActionResult = DbContextActionResult.Exception;
      Results      = null;
      Exception    = runtimeException;

      _actionMessages.AppendLine($"Exception: {runtimeException}");
    }
  }
}
