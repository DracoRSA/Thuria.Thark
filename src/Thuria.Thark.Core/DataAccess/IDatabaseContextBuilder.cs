using Thuria.Thark.Core.Providers;

namespace Thuria.Thark.Core.DataAccess
{
  /// <summary>
  /// Database Builder
  /// </summary>
  public interface IDatabaseContextBuilder
  {
    /// <summary>
    /// Specify the Connection String Provider to use
    /// </summary>
    /// <param name="connectionStringProvider"></param>
    /// <returns></returns>
    IDatabaseContextBuilder WithConnectionStringProvider(IConnectionStringProvider connectionStringProvider);

    /// <summary>
    /// Specify the Database Provider to use
    /// </summary>
    /// <param name="databaseConnectionProvider">Database Connection Provider</param>
    /// <returns>An instance of the Database Builder</returns>
    IDatabaseContextBuilder WithDatabaseConnectionProvider(IDatabaseConnectionProvider databaseConnectionProvider);

    /// <summary>
    /// Specify the Database Transaction Provider to use
    /// </summary>
    /// <param name="databaseTransactionProvider">Database Transaction Provider</param>
    /// <returns>An instance of the Database Builder</returns>
    IDatabaseContextBuilder WithDatabaseTransactionProvider(IDatabaseTransactionProvider databaseTransactionProvider);

    /// <summary>
    /// Specify the Statement Build Provider to use
    /// </summary>
    /// <param name="statementBuildProvider">Statement Build Provider</param>
    /// <returns>An instance of the Database Builder</returns>
    IDatabaseContextBuilder WithStatementBuildProvider(IStatementBuildProvider statementBuildProvider);

    /// <summary>
    /// Specify the Data Model Populate Provider to use
    /// </summary>
    /// <param name="dataModelPopulateProvider">Data Model Populate Provider</param>
    /// <returns>An instance of the Database Builder</returns>
    IDatabaseContextBuilder WithDataModelPopulateProvider(IDataModelPopulateProvider dataModelPopulateProvider);

    /// <summary>
    /// Specify the Command Timeout
    /// </summary>
    /// <param name="newCommandTimeout">Command Timeout</param>
    /// <returns>An instance of the Database Builder</returns>
    IDatabaseContextBuilder WithCommandTimeout(int newCommandTimeout);

    /// <summary>
    /// Specify the Database Context Name
    /// </summary>
    /// <param name="dbContextName">Database Context Name</param>
    /// <returns>An instance of the Database Builder</returns>
    IDatabaseContextBuilder WithDatabaseContextName(string dbContextName);

    /// <summary>
    /// Build
    /// </summary>
    /// <returns>An instance of the newly created Database Context</returns>
    IDatabaseContext Build();
  }
}
