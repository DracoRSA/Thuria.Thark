using System.Data;
using Thuria.Thark.Core.Statement;

namespace Thuria.Thark.Core.DataAccess
{
  /// <summary>
  /// Database Connection Provider
  /// </summary>
  public interface IDatabaseConnectionProvider
  {
    /// <summary>
    /// Database Provider Type
    /// </summary>
    DatabaseProviderType ProviderType { get; }

    /// <summary>
    /// Retrieve a connection with the specified connection string
    /// </summary>
    /// <param name="connectionString">Connection String</param>
    /// <returns>A newly created DB Connection</returns>
    IDbConnection GetConnection(string connectionString);
  }
}
