using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Thuria.Thark.DataModel;
using Thuria.Zitidar.Extensions;
using Thuria.Thark.Core.Providers;
using Thuria.Thark.Core.DataAccess;

namespace Thuria.Thark.DataAccess.Providers
{
  /// <summary>
  /// Populate Data Model Provider
  /// </summary>
  public class SqlDataModelPopulateProvider : IDataModelPopulateProvider
  {
    private readonly object _populateLock = new object();

    /// <inheritdoc />
    public async Task<object> PopulateAsync(Type dataModelType, IDictionary<string, object> sourceData, DbContextAction contextAction)
    {
      var dataModel = Activator.CreateInstance(dataModelType);

      return sourceData.Any()
               ? await PopulateDataModelAsync(dataModel, sourceData, contextAction)
               : dataModel;
    }

    /// <inheritdoc />
    public async Task<T> PopulateAsync<T>(IDictionary<string, object> sourceData, DbContextAction contextAction) where T : class
    {
      var dataModel = Activator.CreateInstance<T>();

      return sourceData.Any()
               ? (T) await PopulateDataModelAsync(dataModel, sourceData, contextAction)
               : dataModel;
    }

    private Task<object> PopulateDataModelAsync(object dataModel, IDictionary<string, object> sourceData, DbContextAction contextAction)
    {
      var taskCompletionSource = new TaskCompletionSource<object>();

      try
      {
        var allProperties    = dataModel.GetType().GetProperties();
        var dataModelColumns = dataModel.GetThuriaDataModelColumns(contextAction);

        Parallel.ForEach(dataModelColumns, column =>
                                             {
                                               var propertyInfo = allProperties.FirstOrDefault(info => info.Name == column.PropertyName);
                                               if (!sourceData.ContainsKey(column.ColumnName) || propertyInfo == null)
                                               {
                                                 return;
                                               }
        
                                               lock (_populateLock)
                                               {
                                                 dataModel.SetPropertyValue(column.Alias ?? column.PropertyName, sourceData[column.ColumnName], true);
                                               }
                                             });

        taskCompletionSource.SetResult(dataModel);
      }
      catch (Exception runtimeException)
      {
        taskCompletionSource.SetException(runtimeException);
      }

      return taskCompletionSource.Task;
    }
  }
}
