using System.Data;
using System.Threading.Tasks;

namespace MGS.Casino.Veyron.DataAccessInterfaces
{
  /// <summary>
  /// Database Context Builder
  /// </summary>
  public interface IDatabaseContextBuilder
  {
    /// <summary>
    /// Specify Connection String to be used
    /// </summary>
    /// <param name="connectionString"></param>
    /// <returns>The current instance of the builder</returns>
    IDatabaseContextBuilder WithConnectionString(string connectionString);

    /// <summary>
    /// Start a transaction with the specified Isolation Level
    /// </summary>
    /// <param name="isolationLevel">Transaction Isolation Level</param>
    /// <returns>The current instance of the builder</returns>
    IDatabaseContextBuilder WithTransaction(IsolationLevel isolationLevel);

    /// <summary>
    /// Command Timeout in Seconds
    /// </summary>
    /// <param name="commandTimeout">Command Timeout</param>
    /// <returns>The current instance of the builder</returns>
    IDatabaseContextBuilder WithCommandTimeout(int commandTimeout);

    /// <summary>
    /// Build the Database Context
    /// </summary>
    /// <returns>A new / existing Database Context</returns>
    Task<IDatabaseContext> BuildAsync();
  }
}
