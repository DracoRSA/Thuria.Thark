using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Thuria.Thark.Core.DataAccess;

namespace Thuria.Thark.Core.Providers
{
  /// <summary>
  /// Data Model Populate Provider
  /// </summary>
  public interface IDataModelPopulateProvider
  {
    /// <summary>
    /// Create and Populate the Data Model from the Source data (Async)
    /// </summary>
    /// <param name="dataModelType">Data Model Type</param>
    /// <param name="sourceData"></param>
    /// <param name="contextAction">Context Action</param>
    /// <returns>A newly populated Data Model</returns>
    Task<object> PopulateAsync(Type dataModelType, IDictionary<string, object> sourceData, DbContextAction contextAction);

    /// <summary>
    /// Create and Populate the Data Model from the Source data (Async)
    /// </summary>
    /// <param name="sourceData"></param>
    /// <param name="contextAction">Context Action</param>
    /// <returns>A newly populated Data Model</returns>
    Task<T> PopulateAsync<T>(IDictionary<string, object> sourceData, DbContextAction contextAction) where T : class;
  }
}
