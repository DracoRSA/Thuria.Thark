using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;
using FluentAssertions;
using Dapper;
using MGS.Casino.Veyron.DataAccessInterfaces;

namespace MGS.Casino.Veyron.DataAccess.Tests
{
  [TestFixture]
  [Parallelizable]
  public class TestDapperDatabaseContextInputParametersProvider
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var inputParametersProvider = new DapperDatabaseContextInputParametersProvider();
      //---------------Test Result -----------------------
      inputParametersProvider.Should().NotBeNull();
    }

    [Test]
    public void ProcessParametersAsync_GivenNoParameters_ShouldReturnEmptyDynamicParameters()
    {
      //---------------Set up test pack-------------------
      var inputParametersProvider       = new DapperDatabaseContextInputParametersProvider();
      DynamicParameters inputParameters = null;
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      Assert.DoesNotThrow(() => inputParameters = (DynamicParameters) inputParametersProvider.ProcessParameters(null));
      //---------------Test Result -----------------------
      inputParameters.Should().NotBeNull();
      inputParameters.Should().BeOfType<DynamicParameters>();
    }

    [Test]
    public void ProcessParametersAsync_GivenParameters_ShouldReturnDynamicParametersWithParametersDefined()
    {
      //---------------Set up test pack-------------------
      var inputParametersProvider = new DapperDatabaseContextInputParametersProvider();
      var inputParameters         = new List<DataAccessParameter>
        {
          new DataAccessParameter("TestParameter1", typeof(int), 1234),
          new DataAccessParameter("TestParameter2", typeof(bool), false),
          new DataAccessParameter("TestParameter3", typeof(int), 56789),
          new DataAccessParameter("TestParameter4", typeof(int), 3897),
        };
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var processedParameters = (DynamicParameters) inputParametersProvider.ProcessParameters(inputParameters);
      //---------------Test Result -----------------------
      processedParameters.ParameterNames.Count().Should().Be(inputParameters.Count);
    }

    [Test]
    public void ProcessParametersAsync_GivenParameters_AndProcessParallel_ShouldReturnDynamicParametersWithParametersDefined()
    {
      //---------------Set up test pack-------------------
      var inputParametersProvider = new DapperDatabaseContextInputParametersProvider();
      var inputParameters         = new List<DataAccessParameter>
        {
          new DataAccessParameter("TestParameter1", typeof(int), 1234),
          new DataAccessParameter("TestParameter2", typeof(bool), false),
          new DataAccessParameter("TestParameter3", typeof(int), 56789),
          new DataAccessParameter("TestParameter4", typeof(int), 3897),
        };
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var processedParameters = (DynamicParameters) inputParametersProvider.ProcessParameters(inputParameters, true);
      //---------------Test Result -----------------------
      processedParameters.ParameterNames.Count().Should().Be(inputParameters.Count);
    }
  }
}
