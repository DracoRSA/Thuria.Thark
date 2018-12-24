using System;
using System.Data;
using FluentAssertions;
using MGS.Casino.Veyron.DataAccessInterfaces;
using MGS.Casino.Veyron.TestUtilities;
using NUnit.Framework;

namespace MGS.Casino.Veyron.DataAccess.Tests
{
  [TestFixture]
  public class TestSqlParameterProvider
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var parameterProvider = new SqlParameterProvider();
      //---------------Test Result -----------------------
      parameterProvider.Should().NotBeNull();
    }

    [Test]
    public void Convert_GivenUnsupportedType_ShouldThrowException()
    {
      //---------------Set up test pack-------------------
      var parameterProvider   = new SqlParameterProvider();
      var dataAccessParameter = new DataAccessParameter("test", typeof(uint), 1);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var exception = Assert.Throws<Exception>(() => parameterProvider.Convert(dataAccessParameter));
      //---------------Test Result -----------------------
      exception.Message.Should().Contain($"No SQL Parameter Processor found for {dataAccessParameter.Name}-{dataAccessParameter.ParameterType}");
    }

    [TestCase(typeof(string), DataParameterDirection.Input, ParameterDirection.Input, DbType.AnsiString)]
    [TestCase(typeof(int), DataParameterDirection.Input, ParameterDirection.Input, DbType.Int32)]
    [TestCase(typeof(long), DataParameterDirection.Input, ParameterDirection.Input, DbType.Int64)]
    [TestCase(typeof(double), DataParameterDirection.Input, ParameterDirection.Input, DbType.Double)]
    [TestCase(typeof(byte), DataParameterDirection.Input, ParameterDirection.Input, DbType.Byte)]
    [TestCase(typeof(short), DataParameterDirection.Input, ParameterDirection.Input, DbType.Int16)]
    [TestCase(typeof(byte[]), DataParameterDirection.Input, ParameterDirection.Input, DbType.Binary)]
    [TestCase(typeof(Guid), DataParameterDirection.Input, ParameterDirection.Input, DbType.Guid)]
    [TestCase(typeof(Guid?), DataParameterDirection.Input, ParameterDirection.Input, DbType.Guid)]
    [TestCase(typeof(bool), DataParameterDirection.Input, ParameterDirection.Input, DbType.Boolean)]
    [TestCase(typeof(DateTime), DataParameterDirection.Input, ParameterDirection.Input, DbType.DateTime)]
    [TestCase(typeof(string), DataParameterDirection.Output, ParameterDirection.Output, DbType.AnsiString)]
    [TestCase(typeof(int), DataParameterDirection.Output, ParameterDirection.Output, DbType.Int32)]
    [TestCase(typeof(long), DataParameterDirection.Output, ParameterDirection.Output, DbType.Int64)]
    [TestCase(typeof(double), DataParameterDirection.Output, ParameterDirection.Output, DbType.Double)]
    [TestCase(typeof(byte), DataParameterDirection.Output, ParameterDirection.Output, DbType.Byte)]
    [TestCase(typeof(short), DataParameterDirection.Output, ParameterDirection.Output, DbType.Int16)]
    [TestCase(typeof(byte[]), DataParameterDirection.Output, ParameterDirection.Output, DbType.Binary)]
    [TestCase(typeof(Guid), DataParameterDirection.Output, ParameterDirection.Output, DbType.Guid)]
    [TestCase(typeof(Guid?), DataParameterDirection.Output, ParameterDirection.Output, DbType.Guid)]
    [TestCase(typeof(bool), DataParameterDirection.Output, ParameterDirection.Output, DbType.Boolean)]
    [TestCase(typeof(DateTime), DataParameterDirection.Output, ParameterDirection.Output, DbType.DateTime)]
    [TestCase(typeof(string), DataParameterDirection.Input, ParameterDirection.Input, DbType.AnsiString)]
    [TestCase(typeof(int), DataParameterDirection.InputOutput, ParameterDirection.InputOutput, DbType.Int32)]
    [TestCase(typeof(long), DataParameterDirection.InputOutput, ParameterDirection.InputOutput, DbType.Int64)]
    [TestCase(typeof(double), DataParameterDirection.InputOutput, ParameterDirection.InputOutput, DbType.Double)]
    [TestCase(typeof(byte), DataParameterDirection.InputOutput, ParameterDirection.InputOutput, DbType.Byte)]
    [TestCase(typeof(short), DataParameterDirection.InputOutput, ParameterDirection.InputOutput, DbType.Int16)]
    [TestCase(typeof(byte[]), DataParameterDirection.InputOutput, ParameterDirection.InputOutput, DbType.Binary)]
    [TestCase(typeof(Guid), DataParameterDirection.InputOutput, ParameterDirection.InputOutput, DbType.Guid)]
    [TestCase(typeof(Guid?), DataParameterDirection.InputOutput, ParameterDirection.InputOutput, DbType.Guid)]
    [TestCase(typeof(bool), DataParameterDirection.InputOutput, ParameterDirection.InputOutput, DbType.Boolean)]
    [TestCase(typeof(DateTime), DataParameterDirection.InputOutput, ParameterDirection.InputOutput, DbType.DateTime)]
    public void Convert_GivenParameter_ShouldReturnExpectedDbParameter(Type parameterType, DataParameterDirection parameterDirection,
                                                                       ParameterDirection expectedParameterDirection, DbType expectedDbType)
    {
      //---------------Set up test pack-------------------
      var parameterName       = "TestParameter";
      var parameterValue      = RandomValueGenerator.CreateRandomValue(parameterType);
      var dataAccessParameter = new DataAccessParameter(parameterName, parameterType, parameterValue, parameterDirection);
      var parameterProvider   = new SqlParameterProvider();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var dbDataParameter = parameterProvider.Convert(dataAccessParameter);
      //---------------Test Result -----------------------
      dbDataParameter.ParameterName.Should().Be(parameterName);
      dbDataParameter.Direction.Should().Be(expectedParameterDirection);
      dbDataParameter.DbType.Should().Be(expectedDbType);
    }
  }
}
