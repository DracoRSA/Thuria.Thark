using NUnit.Framework;
using FluentAssertions;

namespace MGS.Casino.Veyron.DataAccess.Tests
{
  [TestFixture]
  public class TestDatabaseContextProviderStorage
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var providerStorage = new DatabaseContextProviderStorage();
      //---------------Test Result -----------------------
      providerStorage.Should().NotBeNull();
    }

    [Test]
    public void HasConnectionProvider_GivenNoProviderRegistered_ShouldReturnFalse()
    {
      //---------------Set up test pack-------------------
      var providerStorage = new DatabaseContextProviderStorage();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var hasConnectionProvider = providerStorage.HasConnectionProvider;
      //---------------Test Result -----------------------
      hasConnectionProvider.Should().BeFalse();
    }

    [Test]
    public void HasConnectionProvider_GivenProviderRegistered_ShouldReturnTrue()
    {
      //---------------Set up test pack-------------------
      var providerStorage = new DatabaseContextProviderStorage();
      providerStorage.Register(new DatabaseContextProvider());
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var hasConnectionProvider = providerStorage.HasConnectionProvider;
      //---------------Test Result -----------------------
      hasConnectionProvider.Should().BeTrue();
    }

    [Test]
    public void DatabaseContextProvider_GivenNoProviderRegistered_ShouldReturnNull()
    {
      //---------------Set up test pack-------------------
      var providerStorage = new DatabaseContextProviderStorage();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var contextProvider = providerStorage.DatabaseContextProvider;
      //---------------Test Result -----------------------
      contextProvider.Should().BeNull();
    }

    [Test]
    public void DatabaseContextProvider_GivenProviderRegistered_ShouldReturnRegisteredProvider()
    {
      //---------------Set up test pack-------------------
      var databaseContextProvider = new DatabaseContextProvider();
      var providerStorage         = new DatabaseContextProviderStorage();
      providerStorage.Register(databaseContextProvider);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var contextProvider = providerStorage.DatabaseContextProvider;
      //---------------Test Result -----------------------
      contextProvider.Should().NotBeNull();
      contextProvider.Should().Be(databaseContextProvider);
    }

    [Test]
    public void Register_ShouldRegisterDatabaseContextProvider()
    {
      //---------------Set up test pack-------------------
      var databaseContextProvider = new DatabaseContextProvider();
      var providerStorage         = new DatabaseContextProviderStorage();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      providerStorage.Register(databaseContextProvider);
      //---------------Test Result -----------------------
      providerStorage.HasConnectionProvider.Should().BeTrue();
      providerStorage.DatabaseContextProvider.Should().NotBeNull();
      providerStorage.DatabaseContextProvider.Should().Be(databaseContextProvider);
    }

    [Test]
    public void UnRegister_ShouldUnRegisterDatabaseContextProvider()
    {
      //---------------Set up test pack-------------------
      var databaseContextProvider = new DatabaseContextProvider();
      var providerStorage         = new DatabaseContextProviderStorage();
      providerStorage.Register(databaseContextProvider);
      //---------------Assert Precondition----------------
      providerStorage.DatabaseContextProvider.Should().NotBeNull();
      //---------------Execute Test ----------------------
      providerStorage.UnRegister();
      //---------------Test Result -----------------------
      providerStorage.HasConnectionProvider.Should().BeFalse();
      providerStorage.DatabaseContextProvider.Should().BeNull();
    }
  }
}
