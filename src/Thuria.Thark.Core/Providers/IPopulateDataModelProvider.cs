using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Thuria.Thark.Core.Providers
{
  /// <summary>
  /// Populate Data Model Provider
  /// </summary>
  public interface IPopulateDataModelProvider
  {
    /// <summary>
    /// Create and Populate the Data Model from the Source data (Async)
    /// </summary>
    /// <param name="dataModelType">Data Model Type</param>
    /// <param name="sourceData"></param>
    /// <returns></returns>
    Task<object> PopulateAsync(Type dataModelType, IDictionary<string, object> sourceData);
  }
}
