namespace MGS.Casino.Veyron.DataAccessInterfaces
{
  /// <summary>
  /// Database Context Provider Storage
  /// </summary>
  public interface IDatabaseContextProviderStorage
  {
    /// <summary>
    /// Connection Provider exists indicator
    /// </summary>
    bool HasConnectionProvider { get; }

    /// <summary>
    /// Current Database Context Provider
    /// </summary>
    IDatabaseContextProvider DatabaseContextProvider { get; }

    /// <summary>
    /// Register a Database Context Provider
    /// </summary>
    /// <param name="databaseContextProvider">New Database Context Provider</param>
    void Register(IDatabaseContextProvider databaseContextProvider);

    /// <summary>
    /// Un-Register an existing context provider
    /// </summary>
    void UnRegister();
  }
}
