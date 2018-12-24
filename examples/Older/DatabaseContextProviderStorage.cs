using System.Threading;
using MGS.Casino.Veyron.DataAccessInterfaces;

namespace MGS.Casino.Veyron.DataAccess
{
  /// <inheritdoc />
  public class DatabaseContextProviderStorage : IDatabaseContextProviderStorage
  {
    private readonly AsyncLocal<IDatabaseContextProvider> _dbContextProvider = new AsyncLocal<IDatabaseContextProvider>();
    private readonly ReaderWriterLockSlim _readerWriterLock;

    /// <summary>
    /// Constructor
    /// </summary>
    public DatabaseContextProviderStorage()
    {
      _readerWriterLock = new ReaderWriterLockSlim();
    }

    /// <inheritdoc />
    public bool HasConnectionProvider
    {
      get
      {
        try
        {
          _readerWriterLock.EnterReadLock();
          return _dbContextProvider.Value != null;
        }
        finally
        {
          _readerWriterLock.ExitReadLock();
        }
      }
    }

    /// <inheritdoc />
    public IDatabaseContextProvider DatabaseContextProvider => _dbContextProvider.Value;

    /// <inheritdoc />
    public void Register(IDatabaseContextProvider databaseContextProvider)
    {
      try
      {
        _readerWriterLock.EnterWriteLock();
        _dbContextProvider.Value = databaseContextProvider;
      }
      finally
      {
        _readerWriterLock.ExitWriteLock();
      }
    }

    /// <inheritdoc />
    public void UnRegister()
    {
      try
      {
        _readerWriterLock.EnterWriteLock();
        _dbContextProvider.Value = null;
      }
      finally
      {
        _readerWriterLock.ExitWriteLock();
      }
    }
  }
}
