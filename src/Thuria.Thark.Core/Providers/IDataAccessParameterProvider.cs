using System.Data;
using Thuria.Thark.Core.Models;

namespace Thuria.Thark.Core.Providers
{
  /// <summary>
  /// Data Access Parameter Provider
  /// </summary>
  public interface IDataAccessParameterProvider
  {
    /// <summary>
    /// Convert the Data Access Parameter to a Db Data Parameter
    /// </summary>
    /// <param name="inputParameter">Input Parameter</param>
    /// <returns>A Db Data Parameter</returns>
    IDbDataParameter Convert(IDataAccessParameter inputParameter);
  }
}
