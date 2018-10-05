using NUnit.Framework;

using Thuria.Thark.Core.Statement;
using Thuria.Thark.StatementBuilder.Models;
using Thuria.Thark.StatementBuilder.Providers;

namespace Thuria.Thark.StatementBuilder.Tests
{
  [TestFixture]
  public class TestColumnConditionModel
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var selectColumn = new ColumnConditionModel("SourceTable", "SourceColumn", EqualityOperators.Equals, "CompareTable", "CompareColumn");
      //---------------Test Result -----------------------
      Assert.IsNotNull(selectColumn);
    }

    [TestCase("SourceTable", "SourceColumn", EqualityOperators.Equals, "CompareTable", "CompareColumn", "[SourceTable].[SourceColumn] = [CompareTable].[CompareColumn]")]
    [TestCase("", "SourceColumn", EqualityOperators.Equals, "CompareTable", "CompareColumn", "[SourceColumn] = [CompareTable].[CompareColumn]")]
    [TestCase("", "SourceColumn", EqualityOperators.Equals, "", "CompareColumn", "[SourceColumn] = [CompareColumn]")]
    [TestCase("SourceTable", "SourceColumn", EqualityOperators.Equals, "", "CompareColumn", "[SourceTable].[SourceColumn] = [CompareColumn]")]
    [TestCase("SourceTable", "SourceColumn", EqualityOperators.Like, "CompareTable", "CompareColumn", "[SourceTable].[SourceColumn] LIKE [CompareTable].[CompareColumn]")]
    [TestCase("SourceTable", "SourceColumn", EqualityOperators.GreaterThan, "CompareTable", "CompareColumn", "[SourceTable].[SourceColumn] > [CompareTable].[CompareColumn]")]
    [TestCase("SourceTable", "SourceColumn", EqualityOperators.GreaterThanOrEqualTo, "CompareTable", "CompareColumn", "[SourceTable].[SourceColumn] >= [CompareTable].[CompareColumn]")]
    [TestCase("SourceTable", "SourceColumn", EqualityOperators.LessThan, "CompareTable", "CompareColumn", "[SourceTable].[SourceColumn] < [CompareTable].[CompareColumn]")]
    [TestCase("SourceTable", "SourceColumn", EqualityOperators.LessThanOrEqualTo, "CompareTable", "CompareColumn", "[SourceTable].[SourceColumn] <= [CompareTable].[CompareColumn]")]
    public void ToString_GivenConditionValues_ShouldReturnExpectedStatement(string sourceTable, string sourceColumn, 
                                                                            EqualityOperators equalityOperator, 
                                                                            string compareTable, string compareColumn, string expectedStatement)
    {
      //---------------Set up test pack-------------------
      var conditionColumn = new ColumnConditionModel(sourceTable, sourceColumn, equalityOperator, compareTable, compareColumn)
        {
          DatabaseProvider = new SqlServerDatabaseProvider()
        };
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var selectStatement = conditionColumn.ToString();
      //---------------Test Result -----------------------
      Assert.AreEqual(expectedStatement, selectStatement);
    }

    [TestCase("SourceTable", "SourceColumn", EqualityOperators.Equals, "CompareTable", "CompareColumn")]
    public void Equals_GivenSameConditionData_ShouldReturnTrue(string sourceTable, string sourceColumn, 
                                                               EqualityOperators equalityOperator, 
                                                               string compareTable, string compareColumn)
    {
      //---------------Set up test pack-------------------
      var conditionModel = new ColumnConditionModel(sourceTable, sourceColumn, equalityOperator, compareTable, compareColumn)
        {
          DatabaseProvider = new SqlServerDatabaseProvider()
        };
      var compareCondition = new ColumnConditionModel(sourceTable, sourceColumn, equalityOperator, compareTable, compareColumn)
        {
          DatabaseProvider = new SqlServerDatabaseProvider()
        };
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var equalResult = conditionModel.Equals(compareCondition);
      //---------------Test Result -----------------------
      Assert.IsTrue(equalResult);
    }

    [TestCase("SourceTable", "SourceColumn", EqualityOperators.Equals, "CompareTable", "CompareColumn")]
    [TestCase("", "SourceColumn", EqualityOperators.Equals, "CompareTable", "CompareColumn")]
    [TestCase("", "SourceColumn", EqualityOperators.Equals, "", "CompareColumn")]
    [TestCase("SourceTable", "SourceColumn", EqualityOperators.Equals, "", "CompareColumn")]
    public void Equals_GivenDifferentConditionData_ShouldReturnTrue(string sourceTable, string sourceColumn, 
                                                                    EqualityOperators equalityOperator, 
                                                                    string compareTable, string compareColumn)
    {
      //---------------Set up test pack-------------------
      var conditionModel = new ColumnConditionModel(sourceTable, sourceColumn, equalityOperator, compareTable, compareColumn)
        {
          DatabaseProvider = new SqlServerDatabaseProvider()
        };
      var compareCondition = new ColumnConditionModel("unknownTable", "illegalColumn", EqualityOperators.StartsWith, "UnknownCompareTable", "UnknownCompareColumn")
        {
          DatabaseProvider = new SqlServerDatabaseProvider()
        };
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var equalResult = conditionModel.Equals(compareCondition);
      //---------------Test Result -----------------------
      Assert.IsFalse(equalResult);
    }
  }
}