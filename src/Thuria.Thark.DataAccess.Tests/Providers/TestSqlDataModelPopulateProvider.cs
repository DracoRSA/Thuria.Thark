using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using FluentAssertions;
using Thuria.Calot.TestUtilities;
using Thuria.Thark.Core.DataAccess;
using Thuria.Thark.DataAccess.Providers;

namespace Thuria.Thark.DataAccess.Tests.Providers
{
  [TestFixture]
  public class TestSqlDataModelPopulateProvider
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var dataModelPopulateProvider = new SqlDataModelPopulateProvider();
      //---------------Test Result -----------------------
      dataModelPopulateProvider.Should().NotBeNull();
    }

    [Test]
    public async Task PopulateAsync_GivenEmptySourceDataData_ShouldCreateEmptyDataModel()
    {
      //---------------Set up test pack-------------------
      var sourceData                = new Dictionary<string, object>();
      var dataModelPopulateProvider = new SqlDataModelPopulateProvider();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var dataModel = (FakeTestDataModel) await dataModelPopulateProvider.PopulateAsync(typeof(FakeTestDataModel), sourceData, DbContextAction.Retrieve);
      //---------------Test Result -----------------------
      dataModel.Should().NotBeNull();
      dataModel.Should().BeOfType<FakeTestDataModel>();
      dataModel.Id.Should().BeEmpty();
      dataModel.Name.Should().BeNullOrWhiteSpace();
    }

    [Test]
    public async Task PopulateAsync_GivenData_ShouldCreateAndPopulateDataModelAsExpected()
    {
      //---------------Set up test pack-------------------
      var sourceData = new Dictionary<string, object>
                         {
                           { "Id", Guid.NewGuid() },
                           { "Name", RandomValueGenerator.CreateRandomString(10, 30) },
                           { "Date", RandomValueGenerator.CreateRandomDate() },
                           { "SomeFieldAlias", RandomValueGenerator.CreateRandomString(30, 50) }
                         };
      var dataModelPopulateProvider = new SqlDataModelPopulateProvider();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var dataModel = (FakeTestDataModel) await dataModelPopulateProvider.PopulateAsync(typeof(FakeTestDataModel), sourceData, DbContextAction.Retrieve);
      //---------------Test Result -----------------------
      dataModel.Should().NotBeNull();
      dataModel.Id.Should().Be((Guid) sourceData["Id"]);
      dataModel.Name.Should().Be((string) sourceData["Name"]);
      dataModel.Date.Should().Be((DateTime) sourceData["Date"]);
      dataModel.SomeField.Should().Be((string) sourceData["SomeFieldAlias"]);
    }

    [Test]
    public async Task PopulateAsync_GivenGenericType_ShouldCreateAndPopulateDataModelAsExpected()
    {
      //---------------Set up test pack-------------------
      var sourceData = new Dictionary<string, object>
                         {
                           { "Id", Guid.NewGuid() },
                           { "Name", RandomValueGenerator.CreateRandomString(10, 30) },
                           { "Date", RandomValueGenerator.CreateRandomDate() },
                           { "SomeFieldAlias", RandomValueGenerator.CreateRandomString(30, 50) }
                         };
      var dataModelPopulateProvider = new SqlDataModelPopulateProvider();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var dataModel = await dataModelPopulateProvider.PopulateAsync<FakeTestDataModel>(sourceData, DbContextAction.Retrieve);
      //---------------Test Result -----------------------
      dataModel.Should().NotBeNull();
      dataModel.Id.Should().Be((Guid) sourceData["Id"]);
      dataModel.Name.Should().Be((string) sourceData["Name"]);
      dataModel.Date.Should().Be((DateTime) sourceData["Date"]);
      dataModel.SomeField.Should().Be((string)sourceData["SomeFieldAlias"]);
    }

    [Test]
    public async Task PopulateAsync_GivenSourceColumnWithNoProperty_ShouldNotThrowException()
    {
      //---------------Set up test pack-------------------
      var sourceData = new Dictionary<string, object>
                         {
                           { "Id", Guid.NewGuid() },
                           { "UnknownColumn", RandomValueGenerator.CreateRandomString(20, 30) },
                         };
      var dataModelPopulateProvider = new SqlDataModelPopulateProvider();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var dataModel = await dataModelPopulateProvider.PopulateAsync<FakeTestDataModel>(sourceData, DbContextAction.Retrieve);
      //---------------Test Result -----------------------
      dataModel.Should().NotBeNull();
      dataModel.Id.Should().Be((Guid) sourceData["Id"]);
    }
  }
}
