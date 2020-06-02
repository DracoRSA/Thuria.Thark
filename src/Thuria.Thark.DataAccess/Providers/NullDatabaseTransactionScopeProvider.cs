using System.Transactions;

using Thuria.Thark.Core.Providers;
using Thuria.Thark.Core.DataAccess;

namespace Thuria.Thark.DataAccess.Providers
{
  /// <inheritdoc />
  public class NullDatabaseTransactionScopeProvider : IDatabaseTransactionProvider
  {
    /// <inheritdoc />
    public void Dispose()
    {
      // Do Nothing
    }

    /// <inheritdoc />
    public void Start(TransactionScopeOption transactionScopeOption, TransactionIsolation transactionIsolation, int transactionTimespan)
    {
      // Do Nothing
    }

    /// <inheritdoc />
    public void Complete()
    {
      // Do Nothing
    }

    /// <inheritdoc />
    public void Abort()
    {
      // Do nothing
    }
  }
}
