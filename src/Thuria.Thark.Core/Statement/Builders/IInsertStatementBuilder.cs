namespace Thuria.Thark.Core.Statement.Builders
{
  public interface IInsertStatementBuilder : IStatementBuilder
  {
    new IInsertStatementBuilder WithDatabaseProvider(DatabaseProviderType databaseProvider);
    IInsertStatementBuilder WithTable(string tableName);
    IInsertStatementBuilder WithIdColumn(string columnName);
    IInsertStatementBuilder WithColumn(string columnName, object columnValue);
  }
}
