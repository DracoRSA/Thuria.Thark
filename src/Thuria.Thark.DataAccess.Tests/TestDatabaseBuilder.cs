using System;
using System.Data.SqlClient;

using NUnit.Framework;
using FluentAssertions;

using Thuria.Thark.Core.Statement;
using Thuria.Thark.DataAccess.Builders;
using Thuria.Thark.DataAccess.Context;

namespace Thuria.Thark.DataAccess.Tests
{
  [TestFixture]
  public class TestDatabaseBuilder
  {
    private readonly string _connectionString = "Data Source=localhost;Initial Catalog=Test;Persist Security Info=True;User Id=user;Password=somePassword;Connect Timeout=300;";

    [Test]
    public void Create_ShouldReturnInstanceOfDatabaseBuilder()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var databaseBuilder = DatabaseBuilder.Create;
      //---------------Test Result -----------------------
      databaseBuilder.Should().NotBeNull();
      databaseBuilder.Should().BeOfType<DatabaseBuilder>();
    }

    [Test]
    public void Build_GivenNoConnectionString_ShouldFailToBuild()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var exception = Assert.Throws<Exception>(() => DatabaseBuilder.Create.Build());
      //---------------Test Result -----------------------
      StringAssert.Contains("Database Connection String is empty", exception.Message);
    }
    
    [Test]
    public void Build_WithConnectionString_GivenConnectionString_ShouldBuildConnectionWithConnectionString()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var databaseContext = DatabaseBuilder.Create
                                           .WithConnectionString(_connectionString)
                                           .BuildReadonly();
      //---------------Test Result -----------------------
      databaseContext.DbConnection.ConnectionString.Should().Be(_connectionString);
    }
    
    [TestCase(DatabaseProviderType.SqlServer, typeof(SqlConnection))]
    //        [TestCase(DatabaseProviderType.Postgres, typeof(SqlConnection))]
    //        [TestCase(DatabaseProviderType.Firebird, typeof(SqlConnection))]
    public void Build_WithDatabaseProviderType_ShouldReturnExpectedProviderType(DatabaseProviderType databaseProviderType, Type expectedType)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var databaseContext = DatabaseBuilder.Create
                                           .WithDatabaseProviderType(databaseProviderType)
                                           .WithConnectionString(_connectionString)
                                           .AsReadonly()
                                           .Build();
      //---------------Test Result -----------------------
      databaseContext.Should().NotBeNull();
      databaseContext.DbConnection.Should().NotBeNull();
      databaseContext.DbConnection.Should().BeOfType(expectedType);
    }

    [Test]
    public void Build_AsReadonly_ShouldReturnReadonlyDatabaseContext()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      using (var databaseContext = DatabaseBuilder.Create
                                                  .AsReadonly()
                                                  .WithConnectionString(_connectionString)
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
      using (var databaseContext = DatabaseBuilder.Create
                                                  .WithConnectionString(_connectionString)
                                                  .BuildReadonly())
      {
        //---------------Test Result -----------------------
        databaseContext.Should().NotBeNull();
        databaseContext.Should().BeOfType<ReadonlyDatabaseContext>();
      }
    }
  }
}
