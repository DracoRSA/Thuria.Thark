using Thuria.Thark.Core.Statement.Models;

namespace Thuria.Thark.Core.Statement.Builders
{
  /// <summary>
  /// Update Statement Builder
  /// </summary>
  public interface IUpdateStatementBuilder : IStatementBuilder
  {
    /// <summary>
    /// Specify the Database Provider to use
    /// </summary>
    /// <param name="databaseProviderType">Database Provider Type</param>
    /// <returns>An instance of the Update Statement Builder</returns>
    new IUpdateStatementBuilder WithDatabaseProvider(DatabaseProviderType databaseProviderType);

    /// <summary>
    /// Specify the Table to use
    /// </summary>
    /// <param name="tableName">Table Name</param>
    /// <returns>An instance of the Update Statement Builder</returns>
    IUpdateStatementBuilder WithTable(string tableName);

    /// <summary>
    /// Specify a Column to use
    /// </summary>
    /// <param name="columnName">Column Name</param>
    /// <param name="columnValue">Column Value</param>
    /// <returns>An instance of the Update Statement Builder</returns>
    IUpdateStatementBuilder WithColumn(string columnName, object columnValue);

    /// <summary>
    /// Specify a Where Condition to use
    /// </summary>
    /// <param name="sourceTable">Source Table</param>
    /// <param name="sourceColumnName">Source Column Name</param>
    /// <param name="equalityOperator">Equality Operator</param>
    /// <param name="conditionValue">Condition Value</param>
    /// <returns>An instance of the Update Statement Builder</returns>
    IUpdateStatementBuilder WithWhereCondition(string sourceTable, string sourceColumnName, EqualityOperators equalityOperator, object conditionValue = null);

    /// <summary>
    /// Specify a Where Condition to use
    /// </summary>
    /// <param name="whereCondition">Condition Data Model</param>
    /// <returns>An instance of the Update Statement Builder</returns>
    IUpdateStatementBuilder WithWhereCondition(IConditionModel whereCondition);

    /// <summary>
    /// Specify a Where Condition to use
    /// </summary>
    /// <param name="rawWhereCondition">Raw Condition SQL Statement</param>
    /// <returns>An instance of the Update Statement Builder</returns>
    IUpdateStatementBuilder WithWhereCondition(string rawWhereCondition);
  }
}
