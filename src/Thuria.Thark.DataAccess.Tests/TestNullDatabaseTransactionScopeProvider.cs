using System.Transactions;

using NUnit.Framework;
using FluentAssertions;

using Thuria.Thark.Core.DataAccess;
using Thuria.Thark.DataAccess.Providers;

namespace Thuria.Thark.DataAccess.Tests
{
  [TestFixture]
  public class TestNullDatabaseTransactionScopeProvider
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var scopeManager = new NullDatabaseTransactionScopeProvider();
      //---------------Test Result -----------------------
      scopeManager.Should().NotBeNull();
    }

    [Test]
    public void Start_ShouldDoNotThrowException()
    {
      //---------------Set up test pack-------------------
      using (var scopeManager = new NullDatabaseTransactionScopeProvider())
      {
        //---------------Assert Precondition----------------
        //---------------Execute Test ----------------------
        Assert.DoesNotThrow(() => scopeManager.Start(TransactionScopeOption.Required, TransactionIsolation.ReadCommitted, 30));
        //---------------Test Result -----------------------
      }
    }

    [Test]
    public void Complete_ShouldDoNotThrowException()
    {
      //---------------Set up test pack-------------------
      using (var scopeManager = new NullDatabaseTransactionScopeProvider())
      {
        //---------------Assert Precondition----------------
        //---------------Execute Test ----------------------
        Assert.DoesNotThrow(() => scopeManager.Complete());
        //---------------Test Result -----------------------
      }
    }

    [Test]
    public void Abort_ShouldDoNotThrowException()
    {
      //---------------Set up test pack-------------------
      using (var scopeManager = new NullDatabaseTransactionScopeProvider())
      {
        //---------------Assert Precondition----------------
        //---------------Execute Test ----------------------
        Assert.DoesNotThrow(() => scopeManager.Abort());
        //---------------Test Result -----------------------
      }
    }
  }
}
