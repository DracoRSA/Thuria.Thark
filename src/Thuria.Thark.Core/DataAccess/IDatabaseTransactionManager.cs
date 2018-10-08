using System;
using System.Transactions;

namespace Thuria.Thark.Core.DataAccess
{
  /// <summary>
  /// Database Transaction Manager
  /// </summary>
  public interface IDatabaseTransactionManager : IDisposable
  {
    /// <summary>
    /// Start the Transaction
    /// </summary>
    /// <param name="transactionScopeOption">Transaction Scope Option</param>
    /// <param name="transactionIsolation">Transaction Isolation</param>
    /// <param name="transactionTimespan">Transaction Timespan</param>
    void Start(TransactionScopeOption transactionScopeOption, TransactionIsolation transactionIsolation, int transactionTimespan);

    /// <summary>
    /// Complete the Transaction
    /// </summary>
    void Complete();

    /// <summary>
    /// Abort the Transaction
    /// </summary>
    void Abort();
  }
}