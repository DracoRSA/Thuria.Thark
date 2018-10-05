using Thuria.Thark.Core.Statement.Models;

namespace Thuria.Thark.Core.Statement.Builders
{
  public interface IUpdateStatementBuilder : IStatementBuilder
  {
    new IUpdateStatementBuilder WithDatabaseProvider(DatabaseProviderType databaseProvider);
    IUpdateStatementBuilder WithTable(string tableName);
    IUpdateStatementBuilder WithColumn(string columnName, object columnValue);
    IUpdateStatementBuilder WithWhereCondition(string sourceTable, string sourceColumnName, EqualityOperators equalityOperator, object conditionValue = null);
    IUpdateStatementBuilder WithWhereCondition(IConditionModel whereCondition);
    IUpdateStatementBuilder WithWhereCondition(string rawWhereCondition);
  }
}
