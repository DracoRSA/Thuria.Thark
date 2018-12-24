using System;
using Dapper;
using NUnit.Framework;
using FluentAssertions;
using MGS.Casino.Veyron.DataAccessInterfaces.Metadata;

namespace MGS.Casino.Veyron.DataAccess.Tests
{
  [TestFixture]
  public class TestDapperDatamapProvider
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var datamapProvider = new DapperDatamapProvider();
      //---------------Test Result -----------------------
      datamapProvider.Should().NotBeNull();
    }

    [Test]
    public void CreateDatamap_GivenTypeWithMapProperties_ShouldCreateDapperDatamap()
    {
      //---------------Set up test pack-------------------
      var datamapProvider = new DapperDatamapProvider();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      datamapProvider.CreateDatamap(typeof(FakeDapperMap));
      //---------------Test Result -----------------------
      var typeMap = SqlMapper.GetTypeMap(typeof(FakeDapperMap));
      typeMap.Should().NotBeNull();
    }

    private class FakeDapperMap
    {
      public Guid Id { get; set; }

      [VeyronDbColumn("DisplayName")]
      public string Name { get; set; }
    }
  }
}
