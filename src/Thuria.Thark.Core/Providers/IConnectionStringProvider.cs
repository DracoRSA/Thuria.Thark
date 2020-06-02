namespace Thuria.Thark.Core.Providers
{
  /// <summary>
  /// Connection String Provider
  /// </summary>
  public interface IConnectionStringProvider
  {
    /// <summary>
    /// Get the Connection String
    /// </summary>
    /// <param name="dbContext">Context Name (Default - Thark)</param>
    /// <returns>Connection String</returns>
    string GetConnectionString(string dbContext = "Thark");
  }
}
