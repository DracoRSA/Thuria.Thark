using System;
using MGS.Casino.Veyron.DataAccessInterfaces;

namespace MGS.Casino.Veyron.DataAccess
{
  /// <summary>
  /// Database Access Thread Data Model
  /// </summary>
  public class DatabaseContextProviderDataModel : IDatabaseContextProviderDataModel
  {
    /// <summary>
    /// Create Database Access Thread Data Model
    /// </summary>
    /// <param name="connectionString">Connection String</param>
    /// <param name="dbConnection"></param>
    public DatabaseContextProviderDataModel(string connectionString, IVeyronDbConnection dbConnection)
    {
      ConnectionString   = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
      DbConnection       = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
    }

    /// <inheritdoc />
    public string ConnectionString { get; }

    /// <inheritdoc />
    public IVeyronDbConnection DbConnection { get; }
  }
}
