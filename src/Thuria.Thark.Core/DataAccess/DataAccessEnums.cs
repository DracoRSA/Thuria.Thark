namespace Thuria.Thark.Core.DataAccess
{
  /// <summary>
  /// Transaction Isolation option enumeration
  /// </summary>
  public enum TransactionIsolation
  {
    /// <summary>
    /// Chaos 
    /// </summary>
    Chaos,

    /// <summary>
    /// Read Committed
    /// </summary>
    ReadCommitted,

    /// <summary>
    /// Read Uncommitted
    /// </summary>
    ReadUncommitted,

    /// <summary>
    /// Repeatable Read
    /// </summary>
    RepeatableRead,

    /// <summary>
    /// Serializable
    /// </summary>
    Serializable,

    /// <summary>
    /// Snapshot
    /// </summary>
    Snapshot,

    /// <summary>
    /// Unspecified
    /// </summary>
    Unspecified
  }

  /// <summary>
  /// Database Context Action enumeration
  /// </summary>
  public enum DbContextAction
  {
    /// <summary>
    /// Take no Action
    /// </summary>
    None,

    /// <summary>
    /// Retrieve Data
    /// </summary>
    Retrieve,

    /// <summary>
    /// Create a new item
    /// </summary>
    Create,

    /// <summary>
    /// Update an existing item
    /// </summary>
    Update,

    /// <summary>
    /// Delete an existing item
    /// </summary>
    Delete,

    /// <summary>
    /// Execute SQL Statement
    /// </summary>
    SqlStatement,

    /// <summary>
    /// Execute a Stored Procedure
    /// </summary>
    StoredProcedure
  }

  /// <summary>
  /// Database Context Action Result enumeration
  /// </summary>
  public enum DbContextActionResult
  {
    /// <summary>
    /// Successful Database ContextAction
    /// </summary>
    Success,

    /// <summary>
    /// Error Occurred while performing the Database Context Action
    /// </summary>
    Error,

    /// <summary>
    /// Warning(s) occurred while performing the Database Context Action
    /// </summary>
    Warning,

    /// <summary>
    /// Exception occurred while performing the Database Context Action
    /// </summary>
    Exception
  }

  /// <summary>
  /// SQL Execution Action
  /// </summary>
  public enum SqlExecutionAction
  {
    /// <summary>
    /// Unknown Action
    /// </summary>
    Unknown,

    /// <summary>
    /// Retrieve Data
    /// </summary>
    Retrieve,

    /// <summary>
    /// Insert Data
    /// </summary>
    Insert,
  }

  /// <summary>
  /// Sql Execution Action Result
  /// </summary>
  public enum SqlExecutionActionResult
  {
    /// <summary>
    /// Action was Successful
    /// </summary>
    Success,

    /// <summary>
    /// Action has warnings
    /// </summary>
    Warning,

    /// <summary>
    /// Error occurred
    /// </summary>
    Error
  }

  /// <summary>
  /// Data Access Parameter Direction enumeration
  /// </summary>
  public enum DataAccessParameterDirection
  {
    /// <summary>
    /// Input Parameter
    /// </summary>
    Input,

    /// <summary>
    /// Output Parameter
    /// </summary>
    Output,

    /// <summary>
    /// Input and Output Parameter
    /// </summary>
    InputOutput
  }
}
