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
}
