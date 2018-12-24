using System.Threading.Tasks;

namespace MGS.Casino.Veyron.DataAccessInterfaces
{
  /// <summary>
  /// DbConnection Provider
  /// </summary>
  public interface IDbConnectionProvider
  {
    /// <summary>
    /// Create DbConnection
    /// </summary>
    /// <param name="connectionString">Connection String</param>
    /// <returns>DbConnection</returns>
    Task<IVeyronDbConnection> CreateDbConnectionAsync(string connectionString);
  }
}