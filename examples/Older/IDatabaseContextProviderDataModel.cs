namespace MGS.Casino.Veyron.DataAccessInterfaces
{
  /// <summary>
  /// Database Context Provider Data Model
  /// </summary>
  public interface IDatabaseContextProviderDataModel
  {
    /// <summary>
    /// Connection String
    /// </summary>
    string ConnectionString { get; }

    /// <summary>
    /// Database Connection
    /// </summary>
    IVeyronDbConnection DbConnection { get; }
  }
}