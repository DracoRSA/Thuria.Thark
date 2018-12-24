using System.Data;

namespace MGS.Casino.Veyron.DataAccessInterfaces
{
  /// <summary>
  /// DB Connection Parameter Provider
  /// </summary>
  public interface IDbConnectionParameterProvider
  {
    /// <summary>
    /// Convert Data Access Parameter to DbDataParameter
    /// </summary>
    /// <param name="inputParameter">Data Access Parameter</param>
    /// <returns>DbDataParameter</returns>
    IDbDataParameter Convert(DataAccessParameter inputParameter);
  }
}