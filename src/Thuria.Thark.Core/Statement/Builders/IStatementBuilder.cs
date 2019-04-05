using System.Collections.Generic;

using Thuria.Thark.Core.Statement.Providers;

namespace Thuria.Thark.Core.Statement.Builders
{
  /// <summary>
  /// Statement Builder
  /// </summary>
  public interface IStatementBuilder
  {
    /// <summary>
    /// Database Provider Type
    /// </summary>
    IDatabaseProvider DatabaseProvider { get; }

    /// <summary>
    /// Error(s) that occurred during execution
    /// </summary>
    List<string> Errors { get; }

    /// <summary>
    /// Specify the Database Provider to use
    /// </summary>
    /// <param name="databaseProvider">Database Provider Type</param>
    /// <returns>An instance of the Statement Builder</returns>
    IStatementBuilder WithDatabaseProvider(DatabaseProviderType databaseProvider);

    /// <summary>
    /// Database Provider Changed delegate
    /// </summary>
    void DatabaseProviderChanged();

    /// <summary>
    /// Clear the Builder in preparation for building a new Statement
    /// </summary>
    void Clear();

    /// <summary>
    /// Build the Statement
    /// </summary>
    /// <returns>A string representing the Statement</returns>
    string Build();
  }
}