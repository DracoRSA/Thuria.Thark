using System;
using System.Data;

using Dapper;

using Thuria.Thark.Core.DataAccess;

namespace Thuria.Thark.DataAccess.Context
{
  /// <inheritdoc />
  public abstract class DatabaseContext : IDatabaseContext
  {
    private bool _isDisposing;

    /// <summary>
    /// Construct a new Database Context
    /// </summary>
    /// <param name="dbConnection">DB Connection</param>
    /// <param name="databaseTransactionProvider">Database Transaction Manager</param>
    protected DatabaseContext(IDbConnection dbConnection, IDatabaseTransactionProvider databaseTransactionProvider)
    {
      DbConnection               = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
      DatabaseTransactionProvider = databaseTransactionProvider ?? throw new ArgumentNullException(nameof(databaseTransactionProvider));
    }

    /// <summary>
    /// Dispose the Database Context
    /// </summary>
    public void Dispose()
    {
      if (_isDisposing) { return; }
      _isDisposing = true;

      if (DbConnection != null)
      {
        DbConnection.Close();
        DbConnection.Dispose();
        DbConnection = null;
      }

      if (DatabaseTransactionProvider != null)
      {
        DatabaseTransactionProvider.Dispose();
        DatabaseTransactionProvider = null;
      }
    }

    /// <inheritdoc />
    public IDbConnection DbConnection { get; private set; }

    /// <inheritdoc />
    public IDatabaseTransactionProvider DatabaseTransactionProvider { get; private set; }

    /// <inheritdoc />
    public int CommandTimeout { get; set; } = 30;

    /// <inheritdoc />
    public virtual int ExecuteSqlStatement(string statementToExecute, dynamic sqlParameters = null)
    {
      return DbConnection.Execute(statementToExecute, (object)sqlParameters, commandTimeout: CommandTimeout);
    }

    /// <inheritdoc />
    public void Complete()
    {
      DatabaseTransactionProvider?.Complete();
    }
  }
}
