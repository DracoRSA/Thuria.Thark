namespace Thuria.Thark.Core.Statement
{
  public enum DatabaseProviderType
  {
    SqlServer,
    Postgres,
    Firebird
  }

  public enum EqualityOperators
  {
    Equals,
    NotEquals,
    GreaterThan,
    GreaterThanOrEqualTo,
    LessThan,
    LessThanOrEqualTo,
    Like,
    Contains,
    StartsWith,
    EndsWith
  }

  public enum BooleanOperator
  {
    And,
    Or
  }
  public enum RelationshipType
  {
    OneToOne,
    OneToMany
  }
}
