using NUnit.Framework;

using Thuria.Thark.Core.Statement;
using Thuria.Thark.StatementBuilder.Models;
using Thuria.Thark.StatementBuilder.Providers;

namespace Thuria.Thark.StatementBuilder.Tests
{
  [TestFixture]
  public class TestConditionModel
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var selectColumn = new ConditionModel("tableName", "columnName", EqualityOperators.Equals, "testValue");
      //---------------Test Result -----------------------
      Assert.IsNotNull(selectColumn);
    }

    [TestCase("testTable", "testColumn", EqualityOperators.Equals, "testValue", "[testTable].[testColumn] = 'testValue'")]
    [TestCase("", "testColumn", EqualityOperators.Equals, "testValue", "[testColumn] = 'testValue'")]
    [TestCase("testTable", "testColumn", EqualityOperators.NotEquals, "testValue", "[testTable].[testColumn] != 'testValue'")]
    [TestCase("testTable", "testColumn", EqualityOperators.GreaterThan, "testValue", "[testTable].[testColumn] > 'testValue'")]
    [TestCase("testTable", "testColumn", EqualityOperators.GreaterThanOrEqualTo, "testValue", "[testTable].[testColumn] >= 'testValue'")]
    [TestCase("testTable", "testColumn", EqualityOperators.LessThan, "testValue", "[testTable].[testColumn] < 'testValue'")]
    [TestCase("testTable", "testColumn", EqualityOperators.LessThanOrEqualTo, "testValue", "[testTable].[testColumn] <= 'testValue'")]
    [TestCase("testTable", "testColumn", EqualityOperators.Like, "testValue", "[testTable].[testColumn] LIKE 'testValue'")]
    public void ToString_GivenConditionValues_ShouldReturnExpectedStatement(string tableName, string columnName, EqualityOperators equalityOperator, object conditionValue, string expectedStatement)
    {
      //---------------Set up test pack-------------------
      var conditionColumn = new ConditionModel(tableName, columnName, equalityOperator, conditionValue)
        {
          DatabaseProvider = new SqlServerDatabaseProvider()
        };
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var selectStatement = conditionColumn.ToString();
      //---------------Test Result -----------------------
      Assert.AreEqual(expectedStatement, selectStatement);
    }

    [TestCase("", "testColumn", EqualityOperators.Equals, "conditionValue")]
    public void Equals_GivenSameConditionData_ShouldReturnTrue(string tableName, string columnName, EqualityOperators equalityOperator, object conditionValue)
    {
      //---------------Set up test pack-------------------
      var conditionModel = new ConditionModel(tableName, columnName, equalityOperator, conditionValue)
        {
          DatabaseProvider = new SqlServerDatabaseProvider()
        };
      var compareCondition = new ConditionModel(tableName, columnName, equalityOperator, conditionValue)
        {
          DatabaseProvider = new SqlServerDatabaseProvider()
        };
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var equalResult = conditionModel.Equals(compareCondition);
      //---------------Test Result -----------------------
      Assert.IsTrue(equalResult);
    }

    [TestCase("", "testColumn", EqualityOperators.Equals, "conditionValue")]
    public void Equals_GivenDifferentConditionData_ShouldReturnTrue(string tableName, string columnName, EqualityOperators equalityOperator, object conditionValue)
    {
      //---------------Set up test pack-------------------
      var conditionModel = new ConditionModel(tableName, columnName, equalityOperator, conditionValue)
        {
          DatabaseProvider = new SqlServerDatabaseProvider()
        };
      var compareCondition = new ConditionModel("unknownTable", "illegalColumn", EqualityOperators.StartsWith, "unkownValue")
        {
          DatabaseProvider = new SqlServerDatabaseProvider()
        };
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var equalResult = conditionModel.Equals(compareCondition);
      //---------------Test Result -----------------------
      Assert.IsFalse(equalResult);
    }

    [Test]
    public void GetHashCode_ShouldReturnStringHashCode()
    {
      //---------------Set up test pack-------------------
      var condition = new ConditionModel("testTable", "testColumn", EqualityOperators.Equals, "conditionValue")
        {
          DatabaseProvider = new SqlServerDatabaseProvider()
        };
      var hashCode = 0;
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      Assert.DoesNotThrow(() => hashCode = condition.GetHashCode());
      //---------------Test Result -----------------------
      Assert.IsNotNull(hashCode);
    }
  }
}
