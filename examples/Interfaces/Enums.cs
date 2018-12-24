namespace MGS.Casino.Veyron.DataAccessInterfaces
{
  /// <summary>
  /// Parameter Direction
  /// </summary>
  public enum DataParameterDirection
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

  /// <summary>
  /// Transaction Isolation Enumeration
  /// </summary>
  public enum TransactionIsolation
  {
    /// <summary>
    /// 
    /// </summary>
    Chaos,

    /// <summary>
    /// 
    /// </summary>
    ReadCommitted,

    /// <summary>
    /// 
    /// </summary>
    ReadUncommitted,

    /// <summary>
    /// 
    /// </summary>
    RepeatableRead,

    /// <summary>
    /// 
    /// </summary>
    Serializable,

    /// <summary>
    /// 
    /// </summary>
    Snapshot,

    /// <summary>
    /// 
    /// </summary>
    Unspecified
  }
}
