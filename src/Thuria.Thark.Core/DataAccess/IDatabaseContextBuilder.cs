namespace Thuria.Thark.Core.DataAccess
{
  /// <summary>
  /// Database Builder
  /// </summary>
  public interface IDatabaseContextBuilder
  {
    /// <summary>
    /// Specify the Database Provider to use
    /// </summary>
    /// <param name="databaseConnectionProvider">Database Connection Provider</param>
    /// <returns>An instance of the Database Builder</returns>
    IDatabaseContextBuilder WithDatabaseConnectionProvider(IDatabaseConnectionProvider databaseConnectionProvider);

    /// <summary>
    /// Specify the Command Timeout
    /// </summary>
    /// <param name="newCommandTimeout">Command Timeout</param>
    /// <returns>An instance of the Database Builder</returns>
    IDatabaseContextBuilder WithCommandTimeout(int newCommandTimeout);

    /// <summary>
    /// Specify the Connection String
    /// </summary>
    /// <param name="connectionString">Connection String</param>
    /// <returns>An instance of the Database Builder</returns>
    IDatabaseContextBuilder WithConnectionString(string connectionString);

    /// <summary>
    /// Specify to build a Readonly Database Context
    /// </summary>
    /// <returns></returns>
    IDatabaseContextBuilder AsReadonly();

    /// <summary>
    /// Build
    /// </summary>
    /// <returns>An instance of the newly created Database Context</returns>
    IDatabaseContext Build();

    /// <summary>
    /// Build a Readonly Database Context
    /// </summary>
    /// <returns>An instance of a Readonly Database Context</returns>
    IReadonlyDatabaseContext BuildReadonly();

    /// <summary>
    /// Build a Read Write Database Context
    /// </summary>
    /// <returns>An instance of a read/write Database Context</returns>
    IReadWriteDatabaseContext BuildReadWrite();
  }
}