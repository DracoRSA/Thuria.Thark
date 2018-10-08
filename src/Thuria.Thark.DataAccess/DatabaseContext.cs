using System;
using System.Data;

using Thuria.Thark.Core.DataAccess;

namespace Thuria.Thark.DataAccess
{
  /// <inheritdoc />
  public abstract class DatabaseContext : IDatabaseContext
  {
    private bool _isDisposing;

    /// <summary>
    /// Construct a new Database Context
    /// </summary>
    /// <param name="dbConnection">DB Connection</param>
    /// <param name="databaseTransactionManager">Database Transaction Manager</param>
    protected DatabaseContext(IDbConnection dbConnection, IDatabaseTransactionManager databaseTransactionManager)
    {
      DbConnection               = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
      DatabaseTransactionManager = databaseTransactionManager ?? throw new ArgumentNullException(nameof(databaseTransactionManager));
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

      if (DatabaseTransactionManager != null)
      {
        DatabaseTransactionManager.Dispose();
        DatabaseTransactionManager = null;
      }
    }

    /// <inheritdoc />
    public IDbConnection DbConnection { get; private set; }
    /// <inheritdoc />
    public IDatabaseTransactionManager DatabaseTransactionManager { get; private set; }
    /// <inheritdoc />
    public int CommandTimeout { get; set; } = 30;

    /// <inheritdoc />
    public virtual int ExecuteSqlStatement(string statementToExecute, dynamic sqlParameters = null)
    {
      return DbConnection.Execute(statementToExecute, (object)sqlParameters, commandTimeout: this.CommandTimeout);
    }

    /// <inheritdoc />
    public void Complete()
    {
      DatabaseTransactionManager?.Complete();
    }
  }
}
