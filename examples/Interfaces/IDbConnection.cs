using System.Data;

namespace MGS.Casino.Veyron.DataAccessInterfaces
{
  /// <summary>
  /// Veyron DB Connection
  /// </summary>
  public interface IVeyronDbConnection : IDbConnection
  {
    /// <summary>
    /// Underlying Db Connection
    /// </summary>
    IDbConnection Connection { get; }

    /// <summary>
    /// Underlying DB Transaction
    /// </summary>
    IDbTransaction Transaction { get; }
  }
}
