using System;
using System.Data;
using System.Linq;
using System.Collections.ObjectModel;

namespace MGS.Casino.Veyron.DataAccess.Tests
{
  internal class TestDataParameterCollection : Collection<IDataParameter>, IDataParameterCollection
  {
    public bool Contains(string parameterName)
    {
      return Items.Any(parameter => parameter != null && parameter.ParameterName == parameterName);
    }

    public int IndexOf(string parameterName)
    {
      var indexItem = GetParameter(parameterName);
      return indexItem == null ? -1 : Items.IndexOf(indexItem);
    }

    public void RemoveAt(string parameterName)
    {
      var indexItem = GetParameter(parameterName);
      if (indexItem == null)
      {
        return;
      }

      Items.RemoveAt(Items.IndexOf(indexItem));
    }

    public object this[string parameterName]
    {
      get => GetParameter(parameterName);
      set
      {
        var indexItem = Items.FirstOrDefault(parameter => parameter.ParameterName == parameterName);
        if (indexItem == null)
        {
          throw new Exception($"Parameter [{parameterName}] not found");
        }

        Items[Items.IndexOf(indexItem)] = (IDataParameter)value;
      }
    }

    private IDataParameter GetParameter(string parameterName)
    {
      return Items.FirstOrDefault(parameter => parameter != null && parameter.ParameterName == parameterName);
    }
  }
}
