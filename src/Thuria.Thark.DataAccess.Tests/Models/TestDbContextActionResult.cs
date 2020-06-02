using System;
using System.Collections.Generic;
using NUnit.Framework;
using FluentAssertions;
using Thuria.Calot.TestUtilities;
using Thuria.Thark.Core.DataAccess;
using Thuria.Thark.DataAccess.Models;

namespace Thuria.Thark.DataAccess.Tests.Models
{
  [TestFixture]
  public class TestDbContextActionResult
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var actionResult = new DbContextActionResult<object>();
      //---------------Test Result -----------------------
      actionResult.Should().NotBeNull();
    }

    [TestCase("SetWarningResult", "warningMessage")]
    [TestCase("SetErrorResult", "errorMessage")]
    [TestCase("SetExceptionResult", "runtimeException")]
    public void Method_GivenNullParameter_ShouldThrowArgumentNullException(string methodName, string parameterName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      MethodTestHelper.ValidateArgumentNullExceptionIfParameterIsNull<DbContextActionResult<object>>(methodName, parameterName);
      //---------------Test Result -----------------------
    }

    [Test]
    public void SetSuccessResult_ShouldSetExpectedProperties()
    {
      //---------------Set up test pack-------------------
      var actionResults = new List<object>
                            {
                              new object(), new object()
                            };

      var actionResult = new DbContextActionResult<object>();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      actionResult.SetSuccessResult(actionResults);
      //---------------Test Result -----------------------
      actionResult.ActionResult.Should().Be(DbContextActionResult.Success);
      actionResult.Results.Should().BeEquivalentTo(actionResults);
      actionResult.ActionMessage.Should().BeNullOrWhiteSpace();
      actionResult.Exception.Should().BeNull();
    }

    [Test]
    public void SetWarningResult_ShouldSetExpectedProperties()
    {
      //---------------Set up test pack-------------------
      var warningMessage = RandomValueGenerator.CreateRandomString(20, 40);
      var actionResult   = new DbContextActionResult<object>();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      actionResult.SetWarningResult(warningMessage);
      //---------------Test Result -----------------------
      actionResult.ActionResult.Should().Be(DbContextActionResult.Warning);
      actionResult.Results.Should().BeNull();
      actionResult.ActionMessage.Trim().Should().Be($"Warning: {warningMessage}");
      actionResult.Exception.Should().BeNull();
    }

    [Test]
    public void SetErrorResult_ShouldSetExpectedProperties()
    {
      //---------------Set up test pack-------------------
      var errorMessage = RandomValueGenerator.CreateRandomString(20, 40);
      var actionResult = new DbContextActionResult<object>();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      actionResult.SetErrorResult(errorMessage);
      //---------------Test Result -----------------------
      actionResult.ActionResult.Should().Be(DbContextActionResult.Error);
      actionResult.Results.Should().BeNull();
      actionResult.ActionMessage.Trim().Should().Be($"Error: {errorMessage}");
      actionResult.Exception.Should().BeNull();
    }

    [Test]
    public void SetExceptionResult_ShouldSetExpectedProperties()
    {
      //---------------Set up test pack-------------------
      var exceptionMessage = RandomValueGenerator.CreateRandomString(20, 40);
      var exception        = new Exception(exceptionMessage);
      var actionResult     = new DbContextActionResult<object>();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      actionResult.SetExceptionResult(exception);
      //---------------Test Result -----------------------
      actionResult.ActionResult.Should().Be(DbContextActionResult.Exception);
      actionResult.Results.Should().BeNull();
      actionResult.ActionMessage.Trim().Should().Be($"Exception: {exception}");
      actionResult.Exception.Should().Be(exception);
    }
  }
}
