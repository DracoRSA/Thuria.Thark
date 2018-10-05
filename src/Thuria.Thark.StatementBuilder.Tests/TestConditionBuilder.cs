using NUnit.Framework;

using Thuria.Thark.Core.Statement;
using Thuria.Thark.Core.Statement.Builders;
using Thuria.Thark.StatementBuilder.Builders;

namespace Thuria.Thark.StatementBuilder.Tests
{
  [TestFixture]
  public class TestConditionBuilder
  {
    [Test]
    public void Create_ShouldReturnIConditionBuilder()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var builder = ConditionBuilder.Create();
      //---------------Test Result -----------------------
      Assert.IsNotNull(builder);
      Assert.IsInstanceOf<IConditionBuilder>(builder);
    }

    [TestCase(DatabaseProviderType.SqlServer, " [TestTable1].[TestColumn1] = [TestTable2].[TestColumn2] ")]
    [TestCase(DatabaseProviderType.Postgres, " [TestTable1].[TestColumn1] = [TestTable2].[TestColumn2] ")]
    [TestCase(DatabaseProviderType.Firebird, " \"TestTable1\".\"TestColumn1\" = \"TestTable2\".\"TestColumn2\" ")]
    public void Build_GivenDatabaseProvider_ShouldReturnExpectedStatement(DatabaseProviderType databaseProviderType, string expectedStatement)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var sqlStatement = ConditionBuilder.Create().WithDatabaseProvider(databaseProviderType).WithCondition("TestTable1", "TestColumn1", EqualityOperators.Equals, "TestTable2", "TestColumn2").Build();
      //---------------Test Result -----------------------
      StringAssert.AreEqualIgnoringCase(expectedStatement, sqlStatement);
    }

    [TestCase("SourceTable", "SourceColumn", EqualityOperators.Equals, "CompareTable", "CompareColumn", " [SourceTable].[SourceColumn] = [CompareTable].[CompareColumn] ")]
    [TestCase("SourceTable", "SourceColumn", EqualityOperators.Like, "CompareTable", "CompareColumn", " [SourceTable].[SourceColumn] LIKE [CompareTable].[CompareColumn] ")]
    [TestCase("SourceTable", "SourceColumn", EqualityOperators.GreaterThan, "CompareTable", "CompareColumn", " [SourceTable].[SourceColumn] > [CompareTable].[CompareColumn] ")]
    [TestCase("SourceTable", "SourceColumn", EqualityOperators.GreaterThanOrEqualTo, "CompareTable", "CompareColumn", " [SourceTable].[SourceColumn] >= [CompareTable].[CompareColumn] ")]
    [TestCase("SourceTable", "SourceColumn", EqualityOperators.LessThan, "CompareTable", "CompareColumn", " [SourceTable].[SourceColumn] < [CompareTable].[CompareColumn] ")]
    [TestCase("SourceTable", "SourceColumn", EqualityOperators.LessThanOrEqualTo, "CompareTable", "CompareColumn", " [SourceTable].[SourceColumn] <= [CompareTable].[CompareColumn] ")]
    public void Build_WithCondition_TableColumnCondition_ShouldReturnExpectedStatement(string sourceTable, string sourceColumn, 
                                                                                       EqualityOperators equalityOperator,
                                                                                       string compareTable, string compareColumn, string expectedStatement)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var sqlStatement = ConditionBuilder.Create().WithDatabaseProvider(DatabaseProviderType.SqlServer)
                                                  .WithCondition(sourceTable, sourceColumn, equalityOperator, compareTable, compareColumn)
                                                  .Build();
      //---------------Test Result -----------------------
      StringAssert.AreEqualIgnoringCase(expectedStatement, sqlStatement);
    }

    [TestCase("SourceTable", "SourceColumn", EqualityOperators.Equals, "TestValue", " [SourceTable].[SourceColumn] = 'TestValue' ")]
    [TestCase("SourceTable", "SourceColumn", EqualityOperators.Like, "TestValue", " [SourceTable].[SourceColumn] LIKE 'TestValue' ")]
    [TestCase("SourceTable", "SourceColumn", EqualityOperators.GreaterThan, "TestValue", " [SourceTable].[SourceColumn] > 'TestValue' ")]
    [TestCase("SourceTable", "SourceColumn", EqualityOperators.GreaterThanOrEqualTo, "TestValue", " [SourceTable].[SourceColumn] >= 'TestValue' ")]
    [TestCase("SourceTable", "SourceColumn", EqualityOperators.LessThan, "TestValue", " [SourceTable].[SourceColumn] < 'TestValue' ")]
    [TestCase("SourceTable", "SourceColumn", EqualityOperators.LessThanOrEqualTo, "TestValue", " [SourceTable].[SourceColumn] <= 'TestValue' ")]
    public void Build_WithCondition_ValueCondition_ShouldReturnExpectedStatement(string sourceTable, string sourceColumn,
                                                                                 EqualityOperators equalityOperator,
                                                                                 object compareValue, string expectedStatement)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var sqlStatement = ConditionBuilder.Create().WithDatabaseProvider(DatabaseProviderType.SqlServer)
                                                  .WithCondition(sourceTable, sourceColumn, equalityOperator, compareValue)
                                                  .Build();
      //---------------Test Result -----------------------
      StringAssert.AreEqualIgnoringCase(expectedStatement, sqlStatement);
    }

    [Test]
    public void Build_WithAnd_ShouldReturnExpectedStatement()
    {
      //---------------Set up test pack-------------------
      var expectedStatement = " [SourceTable1].[SourceColumn1] = 'TestValue1' AND [SourceTable2].[SourceColumn2] = 'TestValue2' ";
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var sqlStatement = ConditionBuilder.Create()
                                         .WithDatabaseProvider(DatabaseProviderType.SqlServer)
                                         .WithCondition("SourceTable1", "SourceColumn1", EqualityOperators.Equals, "TestValue1")
                                         .WithAnd()
                                         .WithCondition("SourceTable2", "SourceColumn2", EqualityOperators.Equals, "TestValue2")
                                         .Build();
      //---------------Test Result -----------------------
      StringAssert.AreEqualIgnoringCase(expectedStatement, sqlStatement);
    }

    [Test]
    public void Build_WithOr_ShouldReturnExpectedStatement()
    {
      //---------------Set up test pack-------------------
      var expectedStatement = " [SourceTable1].[SourceColumn1] = 'TestValue1' OR [SourceTable2].[SourceColumn2] = 'TestValue2' ";
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var sqlStatement = ConditionBuilder.Create()
                                         .WithDatabaseProvider(DatabaseProviderType.SqlServer)
                                         .WithCondition("SourceTable1", "SourceColumn1", EqualityOperators.Equals, "TestValue1")
                                         .WithOr()
                                         .WithCondition("SourceTable2", "SourceColumn2", EqualityOperators.Equals, "TestValue2")
                                         .Build();
      //---------------Test Result -----------------------
      StringAssert.AreEqualIgnoringCase(expectedStatement, sqlStatement);
    }

    [Test]
    public void Build_WithSection_ShouldReturnExpectedStatement()
    {
      //---------------Set up test pack-------------------
      var expectedStatement = " [SourceTable1].[SourceColumn1] = 'TestValue1' AND ( [SourceTable2].[SourceColumn2] = 'TestValue2' OR [SourceTable3].[SourceColumn3] = 'TestValue3' ) ";
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var sqlStatement = ConditionBuilder.Create()
                                         .WithDatabaseProvider(DatabaseProviderType.SqlServer)
                                         .WithCondition("SourceTable1", "SourceColumn1", EqualityOperators.Equals, "TestValue1")
                                         .WithAnd()
                                         .WithStartSection()
                                         .WithCondition("SourceTable2", "SourceColumn2", EqualityOperators.Equals, "TestValue2")
                                         .WithOr()
                                         .WithCondition("SourceTable3", "SourceColumn3", EqualityOperators.Equals, "TestValue3")
                                         .WithEndSection()
                                         .Build();
      //---------------Test Result -----------------------
      StringAssert.AreEqualIgnoringCase(expectedStatement, sqlStatement);
    }
  }
}
