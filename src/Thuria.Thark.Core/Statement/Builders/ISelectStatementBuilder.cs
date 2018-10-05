using Thuria.Thark.Core.Statement.Models;

namespace Thuria.Thark.Core.Statement.Builders
{
  public interface ISelectStatementBuilder : IStatementBuilder
  {
    new ISelectStatementBuilder WithDatabaseProvider(DatabaseProviderType databaseProvider);
    ISelectStatementBuilder WithTable(string tableName, string tableAlias = null);
    ISelectStatementBuilder WithTable(ITableModel tableModel);
    ISelectStatementBuilder WithColumn(string statementColumn, string columnAlias = null);
    ISelectStatementBuilder WithColumn(IColumnModel statementColumn);
    ISelectStatementBuilder WithWhereCondition(string sourceTable, string sourceColumnName, EqualityOperators equalityOperator, object conditionValue = null);
    ISelectStatementBuilder WithWhereCondition(IConditionModel whereCondition);
    ISelectStatementBuilder WithWhereCondition(string rawWhereCondition);
  }
}
