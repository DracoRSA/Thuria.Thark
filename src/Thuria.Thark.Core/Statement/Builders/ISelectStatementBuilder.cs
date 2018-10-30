using Thuria.Thark.Core.Statement.Models;

namespace Thuria.Thark.Core.Statement.Builders
{
  /// <summary>
  /// Select Statement Builder
  /// </summary>
  public interface ISelectStatementBuilder : IStatementBuilder
  {
    /// <summary>
    /// Specify the Database Provider to use
    /// </summary>
    /// <param name="databaseProviderType">Database Provider Type</param>
    /// <returns>An instance of the Select Statement Builder</returns>
    new ISelectStatementBuilder WithDatabaseProvider(DatabaseProviderType databaseProviderType);

    /// <summary>
    /// Specify the Table to use
    /// </summary>
    /// <param name="tableName">Table Name</param>
    /// <param name="tableAlias">Table Alias (Optional)</param>
    /// <returns>An instance of the Select Statement Builder</returns>
    ISelectStatementBuilder WithTable(string tableName, string tableAlias = null);

    /// <summary>
    /// Specify the Table to use
    /// </summary>
    /// <param name="tableModel">Table Data Model</param>
    /// <returns>An instance of the Select Statement Builder</returns>
    ISelectStatementBuilder WithTable(ITableModel tableModel);

    /// <summary>
    /// Specify a Column to use
    /// </summary>
    /// <param name="statementColumn">Column Name</param>
    /// <param name="columnAlias">Column Alias</param>
    /// <returns>An instance of the Select Statement Builder</returns>
    ISelectStatementBuilder WithColumn(string statementColumn, string columnAlias = null);

    /// <summary>
    /// Specify a Column to use
    /// </summary>
    /// <param name="statementColumn">Column Data Model</param>
    /// <returns>An instance of the Select Statement Builder</returns>
    ISelectStatementBuilder WithColumn(IColumnModel statementColumn);

    /// <summary>
    /// Specify a Where Condition to use
    /// </summary>
    /// <param name="sourceTable">Source Table</param>
    /// <param name="sourceColumnName">Source Column Name</param>
    /// <param name="equalityOperator">Equality Operator</param>
    /// <param name="conditionValue">Condition Value</param>
    /// <returns>An instance of the Select Statement Builder</returns>
    ISelectStatementBuilder WithWhereCondition(string sourceTable, string sourceColumnName, EqualityOperators equalityOperator, object conditionValue = null);

    /// <summary>
    /// Specify a Where Condition to use
    /// </summary>
    /// <param name="whereCondition">Condition Data Model</param>
    /// <returns>An instance of the Select Statement Builder</returns>
    ISelectStatementBuilder WithWhereCondition(IConditionModel whereCondition);

    /// <summary>
    /// Specify a Where Condition to use
    /// </summary>
    /// <param name="rawWhereCondition">Raw Condition SQL Statement</param>
    /// <returns>An instance of the Select Statement Builder</returns>
    ISelectStatementBuilder WithWhereCondition(string rawWhereCondition);
  }
}
