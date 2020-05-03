namespace Thuria.Thark.Core.Statement
{
  /// <summary>
  /// Database Provider Type enumeration
  /// </summary>
  public enum DatabaseProviderType
  {
    /// <summary>
    /// Sql Server
    /// </summary>
    SqlServer,

    /// <summary>
    /// Sql Lite
    /// </summary>
    Sqlite,

    /// <summary>
    /// Postgres
    /// </summary>
    Postgres,

    /// <summary>
    /// Firebird
    /// </summary>
    Firebird
  }

  /// <summary>
  /// Equality Operator enumeration
  /// </summary>
  public enum EqualityOperators
  {
    /// <summary>
    /// Equal
    /// </summary>
    Equals,

    /// <summary>
    /// Not Equal
    /// </summary>
    NotEquals,

    /// <summary>
    /// Greater Than
    /// </summary>
    GreaterThan,

    /// <summary>
    /// Greater Than or Equal To
    /// </summary>
    GreaterThanOrEqualTo,

    /// <summary>
    /// Less Than
    /// </summary>
    LessThan,

    /// <summary>
    /// Less Than or Equal To
    /// </summary>
    LessThanOrEqualTo,

    /// <summary>
    /// Like
    /// </summary>
    Like,

    /// <summary>
    /// Contains
    /// </summary>
    Contains,

    /// <summary>
    /// Starts with
    /// </summary>
    StartsWith,

    /// <summary>
    /// Ends with
    /// </summary>
    EndsWith
  }

  /// <summary>
  /// Boolean Operator enumeration
  /// </summary>
  public enum BooleanOperator
  {
    /// <summary>
    /// And
    /// </summary>
    And,

    /// <summary>
    /// Or
    /// </summary>
    Or
  }

  /// <summary>
  /// Relationship Type enumerator
  /// </summary>
  public enum RelationshipType
  {
    /// <summary>
    /// One to One Relationship
    /// </summary>
    OneToOne,

    /// <summary>
    /// One to Many Relationship
    /// </summary>
    OneToMany
  }
}
