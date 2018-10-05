namespace Thuria.Thark.Core.Statement.Builders
{
  public interface IConditionBuilder
  {
    IConditionBuilder WithDatabaseProvider(DatabaseProviderType databaseProviderType);
    IConditionBuilder WithCondition(string sourceTable, string sourceColumn, EqualityOperators equalityOperator, object conditionValue);
    IConditionBuilder WithCondition(string leftConditionTable, string leftConditionColumn, EqualityOperators equalityOperator, string rightConditionTable, string rightConditionColumn);
    IConditionBuilder WithStartSection();
    IConditionBuilder WithEndSection();
    IConditionBuilder WithAnd();
    IConditionBuilder WithOr();
    string Build();
  }
}
