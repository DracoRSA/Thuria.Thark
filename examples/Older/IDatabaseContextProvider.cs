namespace MGS.Casino.Veyron.DataAccessInterfaces
{
  /// <summary>
  /// Database Context Provider
  /// </summary>
  public interface IDatabaseContextProvider
  {
    /// <summary>
    /// Register a new Database Context
    /// </summary>
    /// <param name="connectionString">DB Connection String</param>
    /// <param name="dbConnection">DB Connection</param>
    void Register(string connectionString, IVeyronDbConnection dbConnection);

    /// <summary>
    /// UnRegister and existing Database Context
    /// </summary>
    /// <param name="connectionString">Connection String</param>
    void UnRegister(string connectionString);

    /// <summary>
    /// Retrieve an existing Database Context Data Model
    /// </summary>
    /// <param name="connectionString"></param>
    /// <returns>The existing data model or null if nothing is registered</returns>
    IDatabaseContextProviderDataModel Get(string connectionString);
  }
}
