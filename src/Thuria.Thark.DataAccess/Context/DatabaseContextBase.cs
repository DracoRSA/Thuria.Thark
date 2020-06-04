using System;
using System.Data;
using System.Collections.Generic;

using System.Threading.Tasks;
using Thuria.Thark.Core.Models;
using Thuria.Thark.Core.Providers;
using Thuria.Thark.Core.DataAccess;

namespace Thuria.Thark.DataAccess.Context
{
  /// <summary>
  /// Database Context Base functionality
  /// </summary>
  public abstract class DatabaseContextBase : IDatabaseContext
  {
    private IDbConnection _dbConnection;
    private readonly IConnectionStringProvider _connectionStringProvider;
    private readonly IDatabaseConnectionProvider _databaseConnectionProvider;
    private bool _isDisposing;

    /// <summary>
    /// SQL Database Context constructor
    /// </summary>
    /// <param name="connectionStringProvider">Connection String Provider</param>
    /// <param name="databaseConnectionProvider">Database Connection Provider</param>
    /// <param name="databaseTransactionProvider">Database Transaction Provider</param>
    protected DatabaseContextBase(IConnectionStringProvider connectionStringProvider, 
                                  IDatabaseConnectionProvider databaseConnectionProvider, 
                                  IDatabaseTransactionProvider databaseTransactionProvider)
    {
      _connectionStringProvider   = connectionStringProvider ?? throw new ArgumentNullException(nameof(connectionStringProvider));
      _databaseConnectionProvider = databaseConnectionProvider ?? throw new ArgumentNullException(nameof(databaseConnectionProvider));
      DatabaseTransactionProvider = databaseTransactionProvider ?? throw new ArgumentNullException(nameof(databaseTransactionProvider));
    }

    /// <summary>
    /// Object Dispose
    /// </summary>
    public void Dispose()
    {
      if (_isDisposing) { return; }
      _isDisposing = true;

      _dbConnection?.Close();
      _dbConnection?.Dispose();
      _dbConnection = null;

      DatabaseTransactionProvider?.Complete();
      DatabaseTransactionProvider?.Dispose();
      DatabaseTransactionProvider = null;
    }

    /// <inheritdoc />
    public int CommandTimeout { get; set; } = 30;

    /// <inheritdoc />
    public string DbContextName { get; set; } = "Thark";

    /// <summary>
    ///  Database Connection
    /// </summary>
    protected IDbConnection DbConnection 
    {
      get
      {
        if (_dbConnection != null)
        {
          return _dbConnection;
        }

        var connectionString = _connectionStringProvider.GetConnectionString(DbContextName);
        _dbConnection        = _databaseConnectionProvider.GetConnection(connectionString);
        return _dbConnection;
      }
    }
    
    /// <summary>
    /// Database Transaction Provider
    /// </summary>
    protected IDatabaseTransactionProvider DatabaseTransactionProvider { get; private set; }

    /// <inheritdoc />
    public Task<bool> OpenAsync()
    {
      var taskCompletionSource = new TaskCompletionSource<bool>();
      var openResult           = true;

      try
      {
        if (DbConnection?.State == ConnectionState.Closed)
        {
          DbConnection.Open();
        }
        else
        {
          openResult = false;
        }

        taskCompletionSource.SetResult(openResult);
      }
      catch (Exception runtimeException)
      {
        taskCompletionSource.SetException(runtimeException);
      }

      return taskCompletionSource.Task;
    }

    /// <inheritdoc />
    public Task<bool> CloseAsync()
    {
      var taskCompletionSource = new TaskCompletionSource<bool>();
      var openResult = true;

      try
      {
        if (DbConnection?.State == ConnectionState.Open)
        {
          DbConnection.Close();
        }
        else
        {
          openResult = false;
        }

        taskCompletionSource.SetResult(openResult);
      }
      catch (Exception runtimeException)
      {
        taskCompletionSource.SetException(runtimeException);
      }

      return taskCompletionSource.Task;
    }

    /// <inheritdoc />
    public abstract Task<IDbContextActionResult<T>> ExecuteActionAsync<T>(DbContextAction dbContextAction,
                                                                          T dataModel = default(T),
                                                                          string sqlCommandText = null,
                                                                          IEnumerable<IDataAccessParameter> dataParameters = null)
      where T : class;

    /// <inheritdoc />
    public void Complete()
    {
      DatabaseTransactionProvider?.Complete();
    }
  }
}
