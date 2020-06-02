namespace Thuria.Thark.Core.Providers
{
  /// <summary>
  /// SQL Statement Build Provider
  /// </summary>
  public interface IStatementBuildProvider
  {
    /// <summary>
    /// Build a Select Statement based on the supplied Data Model
    /// </summary>
    /// <typeparam name="T">Data Model Type</typeparam>
    /// <param name="dataModel">Data Model</param>
    /// <returns>A string representing a Select Statement</returns>
    string BuildSelectStatement<T>(T dataModel) where T : class;

    /// <summary>
    /// Build a Insert Statement based on the supplied Data Model
    /// </summary>
    /// <typeparam name="T">Data Model Type</typeparam>
    /// <param name="dataModel">Data Model</param>
    /// <returns>A string representing a Insert Statement</returns>
    string BuildInsertStatement<T>(T dataModel) where T : class;

    /// <summary>
    /// Build Update Statement
    /// </summary>
    /// <typeparam name="T">Data Model Type</typeparam>
    /// <param name="dataModel">Data Model</param>
    /// <returns>A string representing a Update Statement</returns>
    string BuildUpdateStatement<T>(T dataModel) where T : class;
  }
}
