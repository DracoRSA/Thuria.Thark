using System;
using System.Collections.Concurrent;
using MGS.Casino.Veyron.DataAccessInterfaces;

namespace MGS.Casino.Veyron.DataAccess
{
  /// <summary>
  /// Database Context Provider
  /// </summary>
  public class DatabaseContextProvider : IDatabaseContextProvider
  {
    private readonly ConcurrentDictionary<string, IDatabaseContextProviderDataModel> _registeredContexts;

    /// <summary>
    /// Create Database Context Provider
    /// </summary>
    public DatabaseContextProvider()
    {
      _registeredContexts = new ConcurrentDictionary<string, IDatabaseContextProviderDataModel>();
    }

    /// <inheritdoc />
    public void Register(string connectionString, IVeyronDbConnection dbConnection)
    {
      if (Get(connectionString) != null)
      {
        return;
      }

      var dataModel = new DatabaseContextProviderDataModel(connectionString, dbConnection);
      _registeredContexts.TryAdd(connectionString, dataModel);
    }

    /// <inheritdoc />
    public void UnRegister(string connectionString)
    {
      var dataModel = Get(connectionString);
      if (dataModel == null)
      {
        return;
      }
      
      if (dataModel.DbConnection.Transaction?.Connection != null)
      {
        return;
      }

      _registeredContexts.TryRemove(connectionString, out dataModel);
      dataModel.DbConnection.Close();
    }

    /// <inheritdoc />
    public IDatabaseContextProviderDataModel Get(string connectionString)
    {
      if (string.IsNullOrWhiteSpace(connectionString))
      {
        throw new ArgumentNullException(nameof(connectionString));
      }

      return _registeredContexts.ContainsKey(connectionString)
                          ? _registeredContexts[connectionString]
                          : null;
    }
  }
}
