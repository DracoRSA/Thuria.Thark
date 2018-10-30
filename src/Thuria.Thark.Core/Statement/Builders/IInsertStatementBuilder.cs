namespace Thuria.Thark.Core.Statement.Builders
{
  /// <summary>
  /// Insert Statement Builder
  /// </summary>
  public interface IInsertStatementBuilder : IStatementBuilder
  {
    /// <summary>
    /// Specify the Database Provider to use
    /// </summary>
    /// <param name="databaseProviderType">Database Provider Type</param>
    /// <returns>An instance of the Insert Statement Builder</returns>
    new IInsertStatementBuilder WithDatabaseProvider(DatabaseProviderType databaseProviderType);

    /// <summary>
    /// SpecifY the Table to use
    /// </summary>
    /// <param name="tableName">Table Name</param>
    /// <returns>An instance of the Insert Statement Builder</returns>
    IInsertStatementBuilder WithTable(string tableName);

    /// <summary>
    /// Specify the ID Column to use
    /// </summary>
    /// <param name="columnName">Column Name</param>
    /// <returns>An instance of the Insert Statement Builder</returns>
    IInsertStatementBuilder WithIdColumn(string columnName);

    /// <summary>
    /// Specify the Column top use
    /// </summary>
    /// <param name="columnName">Column Name</param>
    /// <param name="columnValue">Column Value</param>
    /// <returns>An instance of the Insert Statement Builder</returns>
    IInsertStatementBuilder WithColumn(string columnName, object columnValue);
  }
}
