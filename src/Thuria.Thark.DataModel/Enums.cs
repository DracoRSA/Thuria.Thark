namespace Thuria.Thark.DataModel
{
  /// <summary>
  /// Thark Action enumeration
  /// </summary>
  public enum TharkAction
  {
    /// <summary>
    /// No Action
    /// </summary>
    None,

    /// <summary>
    /// Retrieve Data Action
    /// </summary>
    Retrieve,

    /// <summary>
    /// Insert Data Action
    /// </summary>
    Insert,

    /// <summary>
    /// Update Data Action
    /// </summary>
    Update,

    /// <summary>
    /// Delete Data Action
    /// </summary>
    Delete
  }

  /// <summary>
  /// Relationship Type
  /// </summary>
  public enum TharkRelationshipType
  {
    /// <summary>
    /// One to One
    /// </summary>
    OneToOne,

    /// <summary>
    /// One To Many
    /// </summary>
    OneToMany
  }
}
