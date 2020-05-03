using NSubstitute;
using NUnit.Framework;

using Thuria.Calot.TestUtilities;
using Thuria.Thark.Core.Statement;
using Thuria.Thark.Core.Statement.Models;
using Thuria.Thark.StatementBuilder.Models;
using Thuria.Thark.StatementBuilder.Providers;

namespace Thuria.Thark.StatementBuilder.Tests
{
  [TestFixture]
  public class TestCompoundConditionModel
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      var leftCondition  = Substitute.For<IConditionModel>();
      var rightCondition = Substitute.For<IConditionModel>();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var model = new CompoundConditionModel(leftCondition, BooleanOperator.And, rightCondition);
      //---------------Test Result -----------------------
      Assert.IsNotNull(model);
    }

    [TestCase("leftCondition")]
    [TestCase("rightCondition")]
    public void Constructor_GivenNullParameter_ShouldThrowException(string parameterName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      ConstructorTestHelper.ValidateArgumentNullExceptionIfParameterIsNull<CompoundConditionModel>(parameterName);
      //---------------Test Result -----------------------
    }

    [TestCase("LeftTestTable", "LeftTestColumn", EqualityOperators.Equals, "LeftTestValue", 
              BooleanOperator.And, "RightTestTable", "RightTestColumn", 
              EqualityOperators.Like, "RightTestValue", "[LeftTestTable].[LeftTestColumn] = 'LeftTestValue' AND [RightTestTable].[RightTestColumn] LIKE 'RightTestValue'")]
    [TestCase("LeftTestTable", "LeftTestColumn", EqualityOperators.GreaterThan, "LeftTestValue",
              BooleanOperator.Or, "RightTestTable", "RightTestColumn", EqualityOperators.GreaterThanOrEqualTo, "RightTestValue",
              "[LeftTestTable].[LeftTestColumn] > 'LeftTestValue' OR [RightTestTable].[RightTestColumn] >= 'RightTestValue'")]
    public void ToString_GivenConditionValues_ShouldReturnExpectedStatement(string leftConditionTableName, string leftConditionColumnName, EqualityOperators leftConditionOperator, object leftConditionValue,
                                                                            BooleanOperator booleanOperator,
                                                                            string rightConditionTableName, string rightConditionColumnName, EqualityOperators rightConditionOperator, object rightConditionValue,
                                                                            string expectedStatement)
    {
      //---------------Set up test pack-------------------
      var leftCondition = new ConditionModel(leftConditionTableName, leftConditionColumnName, leftConditionOperator, leftConditionValue)
        {
          DatabaseProvider = new SqlServerDatabaseProvider()
        };
      var rightCondition = new ConditionModel(rightConditionTableName, rightConditionColumnName, rightConditionOperator, rightConditionValue)
        {
          DatabaseProvider = new SqlServerDatabaseProvider()
        };
      var compoundCondition = new CompoundConditionModel(leftCondition, booleanOperator, rightCondition)
        {
          DatabaseProvider = new SqlServerDatabaseProvider()
        };
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var selectStatement = compoundCondition.ToString();
      //---------------Test Result -----------------------
      Assert.AreEqual(expectedStatement, selectStatement);
    }

    [TestCase("LeftTestTable", "LeftTestColumn", EqualityOperators.Equals, "LeftTestValue", BooleanOperator.And, "RightTestTable", "RightTestColumn", EqualityOperators.Like, "RightTestValue")]
    [TestCase("LeftTestTable", "LeftTestColumn", EqualityOperators.Equals, "LeftTestValue", BooleanOperator.Or, "RightTestTable", "RightTestColumn", EqualityOperators.Like, "RightTestValue")]
    public void Equals_GivenConditionValues_ShouldReturnTrue(string leftConditionTableName, string leftConditionColumnName, EqualityOperators leftConditionOperator, object leftConditionValue,
                                                             BooleanOperator booleanOperator,
                                                             string rightConditionTableName, string rightConditionColumnName, EqualityOperators rightConditionOperator, object rightConditionValue)
    {
      //---------------Set up test pack-------------------
      var leftCondition = new ConditionModel(leftConditionTableName, leftConditionColumnName, leftConditionOperator, leftConditionValue)
        {
          DatabaseProvider = new SqlServerDatabaseProvider()
        };
      var rightCondition = new ConditionModel(rightConditionTableName, rightConditionColumnName, rightConditionOperator, rightConditionValue)
        {
          DatabaseProvider = new SqlServerDatabaseProvider()
        };
      var conditionModel = new CompoundConditionModel(leftCondition, booleanOperator, rightCondition)
        {
          DatabaseProvider = new SqlServerDatabaseProvider()
        };
      var compareCondition = new CompoundConditionModel(leftCondition, booleanOperator, rightCondition)
        {
          DatabaseProvider = new SqlServerDatabaseProvider()
        };
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var equalResult = conditionModel.Equals(compareCondition);
      //---------------Test Result -----------------------
      Assert.IsTrue(equalResult);
    }

    [TestCase("LeftTestTable", "LeftTestColumn", EqualityOperators.Equals, "LeftTestValue", BooleanOperator.Or, "RightTestTable", "RightTestColumn", EqualityOperators.Like, "RightTestValue")]
    public void Equals_GivenDifferentConditionValues_ShouldReturnFalse(string leftConditionTableName, string leftConditionColumnName, EqualityOperators leftConditionOperator, object leftConditionValue,
                                                                       BooleanOperator booleanOperator,
                                                                       string rightConditionTableName, string rightConditionColumnName, EqualityOperators rightConditionOperator, object rightConditionValue)
    {
      //---------------Set up test pack-------------------
      var leftCondition = new ConditionModel(leftConditionTableName, leftConditionColumnName, leftConditionOperator, leftConditionValue)
        {
          DatabaseProvider = new SqlServerDatabaseProvider()
        };
      var rightCondition = new ConditionModel(rightConditionTableName, rightConditionColumnName, rightConditionOperator, rightConditionValue)
        {
          DatabaseProvider = new SqlServerDatabaseProvider()
        };
      var conditionModel = new CompoundConditionModel(leftCondition, booleanOperator, rightCondition)
        {
          DatabaseProvider = new SqlServerDatabaseProvider()
        };

      var differentLeftCondition = new ConditionModel("", "DifferentColumn1", EqualityOperators.GreaterThan, "Unknown1")
        {
          DatabaseProvider = new SqlServerDatabaseProvider()
        };
      var differentRightCondition = new ConditionModel("", "DifferentColumn2", EqualityOperators.LessThan, "Unknown2")
        {
          DatabaseProvider = new SqlServerDatabaseProvider()
        };
      var compareCondition = new CompoundConditionModel(differentLeftCondition, BooleanOperator.Or, differentRightCondition)
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
      var leftCondition = new ConditionModel("Table1", "DifferentColumn1", EqualityOperators.GreaterThan, "Unknown1")
        {
          DatabaseProvider = new SqlServerDatabaseProvider()
        };
      var rightCondition = new ConditionModel("Table2", "DifferentColumn2", EqualityOperators.LessThan, "Unknown2")
        {
          DatabaseProvider = new SqlServerDatabaseProvider()
        };
      var condition = new CompoundConditionModel(leftCondition, BooleanOperator.And, rightCondition)
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
