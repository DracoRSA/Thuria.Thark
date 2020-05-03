using System;

using NSubstitute;
using NUnit.Framework;
using FluentAssertions;

using Thuria.Thark.Core.DataAccess;
using Thuria.Thark.DataAccess.Context;
using Thuria.Thark.DataAccess.Builders;

namespace Thuria.Thark.DataAccess.Tests
{
  [TestFixture]
  public class TestDatabaseContextBuilder
  {
    private readonly string _connectionString = "Data Source=localhost;Initial Catalog=Test;Persist Security Info=True;User Id=user;Password=somePassword;Connect Timeout=300;";

    [Test]
    public void Create_ShouldReturnInstanceOfDatabaseBuilder()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var databaseBuilder = DatabaseContextBuilder.Create;
      //---------------Test Result -----------------------
      databaseBuilder.Should().NotBeNull();
      databaseBuilder.Should().BeOfType<DatabaseContextBuilder>();
    }

    [Test]
    public void Build_GivenNoConnectionString_ShouldFailToBuild()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var exception = Assert.Throws<ArgumentException>(() => DatabaseContextBuilder.Create
                                                                                   .WithDatabaseConnectionProvider(Substitute.For<IDatabaseConnectionProvider>())
                                                                                   .Build());
      //---------------Test Result -----------------------
      exception.Message.Should().Contain("Database Connection string must be specified");
      exception.ParamName.Should().Be("_connectionString");
    }

    [Test]
    public void Build_GivenNoDatabaseConnectionProvider_ShouldFailToBuild()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var exception = Assert.Throws<ArgumentException>(() => DatabaseContextBuilder.Create
                                                                                   .WithConnectionString(_connectionString)
                                                                                   .Build());
      //---------------Test Result -----------------------
      exception.Message.Should().Contain("Connection Provider must be specified");
      exception.ParamName.Should().Be("_databaseConnectionProvider");
    }
    
    [Test]
    public void Build_WithConnectionString_ShouldBuildConnectionWithConnectionString()
    {
      //---------------Set up test pack-------------------
      var dbConnectionProvider = Substitute.For<IDatabaseConnectionProvider>();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var _ = DatabaseContextBuilder.Create
                                    .WithConnectionString(_connectionString)
                                    .WithDatabaseConnectionProvider(dbConnectionProvider)
                                    .Build();
      //---------------Test Result -----------------------
      dbConnectionProvider.Received(1).GetConnection(_connectionString);
    }
    
    [Test]
    public void Build_WithTimeout_ShouldBuildContextWithCommandTimeout()
    {
      //---------------Set up test pack-------------------
      var dbConnectionProvider = Substitute.For<IDatabaseConnectionProvider>();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var dbContext = DatabaseContextBuilder.Create
                                            .WithConnectionString(_connectionString)
                                            .WithDatabaseConnectionProvider(dbConnectionProvider)
                                            .WithCommandTimeout(99)
                                            .Build();
      //---------------Test Result -----------------------
      dbContext.CommandTimeout.Should().Be(99);
    }
    
    [Test]
    public void Build_AsNotReadonly_ShouldReturnReadWriteDatabaseContext()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      using (var databaseContext = DatabaseContextBuilder.Create
                                                         .WithConnectionString(_connectionString)
                                                         .WithDatabaseConnectionProvider(Substitute.For<IDatabaseConnectionProvider>())
                                                         .Build())
      {
        //---------------Test Result -----------------------
        databaseContext.Should().NotBeNull();
        databaseContext.Should().BeOfType<ReadWriteDatabaseContext>();
      }
    }
    
    [Test]
    public void Build_AsReadonly_ShouldReturnReadonlyDatabaseContext()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      using (var databaseContext = DatabaseContextBuilder.Create
                                                         .AsReadonly()
                                                         .WithConnectionString(_connectionString)
                                                         .WithDatabaseConnectionProvider(Substitute.For<IDatabaseConnectionProvider>())
                                                         .Build())
      {
        //---------------Test Result -----------------------
        databaseContext.Should().NotBeNull();
        databaseContext.Should().BeOfType<ReadonlyDatabaseContext>();
      }
    }
    
    [Test]
    public void BuildReadonly_ShouldBuildReadonlyDatabaseContext()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      using (var databaseContext = DatabaseContextBuilder.Create
                                                         .WithConnectionString(_connectionString)
                                                         .WithDatabaseConnectionProvider(Substitute.For<IDatabaseConnectionProvider>())
                                                         .BuildReadonly())
      {
        //---------------Test Result -----------------------
        databaseContext.Should().NotBeNull();
        databaseContext.Should().BeOfType<ReadonlyDatabaseContext>();
      }
    }
    
    [Test]
    public void BuildReadWrite_ShouldBuildReadWriteDatabaseContext()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      using (var databaseContext = DatabaseContextBuilder.Create
                                                         .WithConnectionString(_connectionString)
                                                         .WithDatabaseConnectionProvider(Substitute.For<IDatabaseConnectionProvider>())
                                                         .BuildReadWrite())
      {
        //---------------Test Result -----------------------
        databaseContext.Should().NotBeNull();
        databaseContext.Should().BeOfType<ReadWriteDatabaseContext>();
      }
    }
  }
}
