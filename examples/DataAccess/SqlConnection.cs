using System;
using System.Data;
using MGS.Casino.Veyron.DataAccessInterfaces;

namespace MGS.Casino.Veyron.DataAccess
{
  /// <summary>
  /// Veyron SQL Connection
  /// </summary>
  public class VeyronSqlConnection : IVeyronDbConnection
  {
    /// <summary>
    /// Create Veyron DB Connection
    /// </summary>
    /// <param name="dbConnection">DB Connection</param>
    public VeyronSqlConnection(IDbConnection dbConnection)
    {
      Connection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
    }

    /// <inheritdoc />
    public IDbConnection Connection { get; }

    /// <inheritdoc />
    public IDbTransaction Transaction { get; private set; }

    /// <inheritdoc />
    public string ConnectionString
    {
      get => Connection.ConnectionString;
      set => Connection.ConnectionString = value;
    }

    /// <inheritdoc />
    public int ConnectionTimeout => Connection.ConnectionTimeout;

    /// <inheritdoc />
    public ConnectionState State => Connection.State;

    /// <inheritdoc />
    public string Database => Connection.Database;

    /// <inheritdoc />
    public IDbTransaction BeginTransaction(IsolationLevel isolationLevel)
    {
      Transaction = Connection.BeginTransaction(isolationLevel);
      return Transaction;
    }

    /// <inheritdoc />
    public IDbTransaction BeginTransaction()
    {
      Transaction = Connection.BeginTransaction();
      return Transaction;
    }

    /// <inheritdoc />
    public void ChangeDatabase(string databaseName)
    {
      Connection.ChangeDatabase(databaseName);
    }

    /// <inheritdoc />
    public void Close()
    {
      Connection.Close();
    }

    /// <inheritdoc />
    public IDbCommand CreateCommand()
    {
      return Connection.CreateCommand();
    }

    /// <inheritdoc />
    public void Open()
    {
      if (Connection.State != ConnectionState.Open)
      {
        Connection.Open();
      }
    }

    /// <inheritdoc />
    public void Dispose()
    {
      Connection.Dispose();
    }
  }
}