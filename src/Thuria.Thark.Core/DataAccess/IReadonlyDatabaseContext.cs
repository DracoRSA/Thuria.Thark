using System.Collections.Generic;

namespace Thuria.Thark.Core.DataAccess
{
  /// <summary>
  /// readonly Database Context
  /// </summary>
  public interface IReadonlyDatabaseContext : IDatabaseContext
  {
    /// <summary>
    /// Retrieve a list from the database and return a list of mapped objects
    /// </summary>
    /// <typeparam name="T">Object Type</typeparam>
    /// <param name="sqlStatement">SQL Statement</param>
    /// <returns>A list of mapped objects</returns>
    IEnumerable<T> Select<T>(string sqlStatement);

    /// <summary>
    /// Return a single mapped object of the specified type
    /// </summary>
    /// <typeparam name="T">Object Type</typeparam>
    /// <param name="sqlStatement">SQL Statement</param>
    /// <returns>Single mapped object of the specified type</returns>
    T SelectOne<T>(string sqlStatement);
  }
}