using NUnit.Framework;
using FluentAssertions;
using Thuria.Calot.TestUtilities;

using Thuria.Thark.Core.DataAccess;
using Thuria.Thark.DataAccess.Models;

namespace Thuria.Thark.DataAccess.Tests.Models
{
  [TestFixture]
  public class TestSqlExecutionResult
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var executionResult = new SqlExecutionResult(SqlExecutionActionResult.Success);
      //---------------Test Result -----------------------
      executionResult.Should().NotBeNull();
    }

    [TestCase("actionResult", "ActionResult")]
    [TestCase("rowsAffected", "RowsAffected")]
    [TestCase("resultData", "ResultData")]
    [TestCase("errorMessage", "ErrorMessage")]
    public void Constructor_GivenParameterValue_ShouldSetPropertyValue(string parameterName, string propertyName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      ConstructorTestHelper.ValidatePropertySetWithParameter<SqlExecutionResult>(parameterName, propertyName);
      //---------------Test Result -----------------------
    }
  }
}
