namespace Thuria.Thark.Core.DataAccess
{
  /// <summary>
  /// Read / Write Database Context
  /// </summary>
  public interface IReadWriteDatabaseContext : IReadonlyDatabaseContext
  {
    /// <summary>
    /// Insert an object into the database
    /// </summary>
    /// <typeparam name="T">Object Type</typeparam>
    /// <param name="sqlStatement">SQL Statement</param>
    /// <returns>Newly created object</returns>
    object Insert<T>(string sqlStatement);

    /// <summary>
    /// Update an existing object in the database
    /// </summary>
    /// <typeparam name="T">Object Type</typeparam>
    /// <param name="sqlStatement">SQL Statement</param>
    /// <returns>Number of rows affected</returns>
    int Update<T>(string sqlStatement);

    /// <summary>
    /// Delete an existing object in the database
    /// </summary>
    /// <typeparam name="T">Object Type</typeparam>
    /// <param name="sqlStatement">SQL Statement</param>
    void Delete<T>(string sqlStatement);
  }
}