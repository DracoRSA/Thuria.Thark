using System.Collections.Generic;
using NUnit.Framework;
using FluentAssertions;

namespace MGS.Casino.Veyron.DataAccessInterfaces.Tests
{
  [TestFixture]
  public class TestDataResultExtensions
  {
    [Test]
    public void Get_GivenNullExecutionResult_ShouldReturnDefaultValueForType()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var dataResult = ((Dictionary<string, object>)null).Get<int>("test");
      //---------------Test Result -----------------------
      dataResult.Should().Be(default(int));
    }

    [Test]
    public void Get_GivenEmptyExecutionResult_ShouldReturnDefaultValueForType()
    {
      //---------------Set up test pack-------------------
      var executionResult = new Dictionary<string, object>();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var dataResult = executionResult.Get<int>("testField");
      //---------------Test Result -----------------------
      dataResult.Should().Be(default(int));
    }

    [TestCase("testField", 10)]
    [TestCase("testField2", 12)]
    [TestCase("testField3", 15)]
    public void Get_GivenFieldExistsInResult_ShouldReturnExpectedValue(string fieldName, object fieldValue)
    {
      //---------------Set up test pack-------------------
      var executionResult = new Dictionary<string, object>
        {
          { fieldName, fieldValue }
        };
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var dataResult = executionResult.Get<int>(fieldName);
      //---------------Test Result -----------------------
      dataResult.Should().Be((int) fieldValue);
    }
  }
}
