using System;
using System.Linq;
using Dapper;
using NUnit.Framework;
using FluentAssertions;
using MGS.Casino.Veyron.DataAccessInterfaces;

namespace MGS.Casino.Veyron.DataAccess.Tests
{
  [TestFixture]
  public class TestDynamicParametersExtensions
  {
    [TestCase("testParameter", typeof(int))]
    [TestCase("testParameter", typeof(uint), 76)]
    [TestCase("testParameter", typeof(int), 99)]
    [TestCase("testParameter", typeof(DateTime), null, DataParameterDirection.Output)]
    [TestCase("testParameter", typeof(string), "testValue", DataParameterDirection.InputOutput)]
    public void Add_GivenParameterDetails_ShouldCreateDynamicParameterWithExpectedValues(string parameterName, Type parameterType, object parameterValue = null,
                                                                                         DataParameterDirection parameterDirection = DataParameterDirection.Input, 
                                                                                         int? parameterSize = null)
    {
      //---------------Set up test pack-------------------
      var dynamicParameters = new DynamicParameters();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      dynamicParameters.Add(parameterName, parameterType, parameterValue, parameterDirection, parameterSize);
      //---------------Test Result -----------------------
      dynamicParameters.ParameterNames.Contains(parameterName).Should().BeTrue();
      ((SqlMapper.IParameterLookup)dynamicParameters)[parameterName].Should().Be(parameterValue);
    }
  }
}
