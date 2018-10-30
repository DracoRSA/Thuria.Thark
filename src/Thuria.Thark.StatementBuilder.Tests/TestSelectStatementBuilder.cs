using System;

using NSubstitute;
using NUnit.Framework;
using FluentAssertions;

using Thuria.Thark.Core.Statement;
using Thuria.Thark.Core.Statement.Models;
using Thuria.Thark.StatementBuilder.Models;
using Thuria.Thark.Core.Statement.Builders;
using Thuria.Thark.StatementBuilder.Builders;

namespace Thuria.Thark.StatementBuilder.Tests
{
  [TestFixture]
  public class TestSelectStatementBuilder
  {
    [Test]
    public void Build_GivenNoTable_ShouldReturnEmptyStatement_And_ErrorInErrorList()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var exception = Assert.Throws<StatementBuilderException>(() => SelectStatementBuilder.Create.Build());
      //---------------Test Result -----------------------
      exception.Message.Should().Contain("At least one table name must be provided");
    }

    [TestCaseSource(typeof(BuilderTestData), "SimpleSelectTestCases")]
    public void Build_GivenTableButNoColumns_ShouldReturnExpectedStatement(DatabaseProviderType providerType, string tableName, string expectedStatement)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var selectStatement = SelectStatementBuilder.Create.WithDatabaseProvider(providerType).WithTable(tableName).Build();
      //---------------Test Result -----------------------
      Assert.AreEqual(expectedStatement, selectStatement);
    }

    [TestCaseSource(typeof(BuilderTestData), "SelectTestCasesWithColumns")]
    public void Build_GivenTableAndColumns_ShouldReturnExpectedStatement(DatabaseProviderType providerType, string tableName, 
                                                                         string[][] tableColumns, object[][] whereConditions, string expectedStatement)
    {
      //---------------Set up test pack-------------------
      var builder = SelectStatementBuilder.Create.WithDatabaseProvider(providerType).WithTable(tableName);

      foreach (var currentColumn in tableColumns)
      {
        if (string.IsNullOrWhiteSpace(currentColumn[0]))
        {
          builder.WithColumn(currentColumn[1], currentColumn[2]);
        }
        else
        {
          var selectColumn = new ColumnModel(currentColumn[0], currentColumn[1], currentColumn[2]);
          builder.WithColumn(selectColumn);
        }
      }

      foreach (var currentCondition in whereConditions)
      {
        builder.WithWhereCondition(currentCondition[0].ToString(), currentCondition[1].ToString(), (EqualityOperators)currentCondition[2], currentCondition[3]);
      }
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var selectStatement = builder.Build();
      //---------------Test Result -----------------------
      Assert.AreEqual(expectedStatement, selectStatement);
    }

    [TestCase(DatabaseProviderType.Firebird, DatabaseProviderType.SqlServer)]
    [TestCase(DatabaseProviderType.SqlServer, DatabaseProviderType.Firebird)]
    [TestCase(DatabaseProviderType.SqlServer, DatabaseProviderType.Postgres)]
    public void WithDatabaseProvider_ShouldChangeDatabaseProvider(DatabaseProviderType defaultProviderType, DatabaseProviderType databaseProviderType)
    {
      //---------------Set up test pack-------------------
      var builder = (FakeSelectStatementBuilder)FakeSelectStatementBuilder.Create;
      builder.Should().NotBeNull();

      builder.WithDatabaseProvider(defaultProviderType);
      builder.HasDatabaseProviderChanged = false;
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      // ReSharper disable once PossibleNullReferenceException
      builder.WithDatabaseProvider(databaseProviderType);
      //---------------Test Result -----------------------
      Assert.IsTrue(builder.HasDatabaseProviderChanged);
    }

    [Test]
    public void WithTable_GivenUniqueTableName_ShouldReturnStatementWithTable()
    {
      //---------------Set up test pack-------------------
      var testTableName     = "TestTable";
      var expectedStatement = $"SELECT * FROM [{testTableName}]";
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var selectStatement = SelectStatementBuilder.Create.WithTable(testTableName).Build();
      //---------------Test Result -----------------------
      Assert.AreEqual(expectedStatement, selectStatement);
    }

    [Test]
    public void WithTable_GivenSameTable_ShouldReturnStatementWithTableNotDuplicated()
    {
      //---------------Set up test pack-------------------
      var testTableName     = "TestTable";
      var expectedStatement = $"SELECT * FROM [{testTableName}]";
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var selectStatement = SelectStatementBuilder.Create.WithTable(testTableName).WithTable(testTableName).Build();
      //---------------Test Result -----------------------
      Assert.AreEqual(expectedStatement, selectStatement);
    }

    [Test]
    public void WithTable_GivenValidTable_ShouldSetDatabaseProviderOnTableModel()
    {
      //---------------Set up test pack-------------------
      var tableModel = Substitute.For<ITableModel>();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      SelectStatementBuilder.Create.WithDatabaseProvider(DatabaseProviderType.Postgres).WithTable(tableModel).Build();
      //---------------Test Result -----------------------
      tableModel.DatabaseProvider.DatabaseProviderType.Should().Be(DatabaseProviderType.Postgres);
    }

    [Test]
    public void WithColumn_GivenNullColumnName_ShouldThrowException()
    {
      //---------------Set up test pack-------------------
      var testTableName = "TestTable";
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var exception = Assert.Throws<ArgumentNullException>(() => SelectStatementBuilder.Create.WithTable(testTableName).WithColumn(string.Empty).Build());
      //---------------Test Result -----------------------
      Assert.AreEqual("columnName", exception.ParamName);
    }

    [Test]
    public void WithColumn_GivenNullColumn_ShouldThrowException()
    {
      //---------------Set up test pack-------------------
      var testTableName = "TestTable";
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var exception = Assert.Throws<ArgumentNullException>(() => SelectStatementBuilder.Create.WithTable(testTableName).WithColumn(null).Build());
      //---------------Test Result -----------------------
      Assert.AreEqual("statementColumn", exception.ParamName);
    }

    [Test]
    public void WithColumn_GivenValidColumn_ShouldSetDatabaseProviderOnTableModel()
    {
      //---------------Set up test pack-------------------
      var testColumn = Substitute.For<IColumnModel>();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      SelectStatementBuilder.Create
                            .WithDatabaseProvider(DatabaseProviderType.Postgres)
                            .WithTable("TestTable")
                            .WithColumn(testColumn)
                            .Build();
      //---------------Test Result -----------------------
      testColumn.DatabaseProvider.DatabaseProviderType.Should().Be(DatabaseProviderType.Postgres);
    }

    [Test]
    public void WithWhereCondition_GivenNullCondition_ShouldThrowException()
    {
      //---------------Set up test pack-------------------
      var testTableName              = "TestTable";
      IConditionModel whereCondition = null;
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      // ReSharper disable once ExpressionIsAlwaysNull
      var exception = Assert.Throws<ArgumentNullException>(() => SelectStatementBuilder.Create
                                                                                       .WithTable(testTableName)
                                                                                       .WithWhereCondition(whereCondition)
                                                                                       .Build());
      //---------------Test Result -----------------------
      Assert.AreEqual("whereCondition", exception.ParamName);
    }

    [Test]
    public void WithWhereCondition_GivenValidCondition_ShouldSetDatabaseProviderOnTableModel()
    {
      //---------------Set up test pack-------------------
      var testCondition = Substitute.For<IConditionModel>();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      SelectStatementBuilder.Create
                            .WithDatabaseProvider(DatabaseProviderType.Postgres)
                            .WithTable("TestTable")
                            .WithWhereCondition(testCondition)
                            .Build();
      //---------------Test Result -----------------------
      testCondition.DatabaseProvider.DatabaseProviderType.Should().Be(DatabaseProviderType.Postgres);
    }

    private class FakeSelectStatementBuilder : SelectStatementBuilder
    {
      private FakeSelectStatementBuilder()
      {
      }

      public bool HasDatabaseProviderChanged { get; set; }

      public new static ISelectStatementBuilder Create => new FakeSelectStatementBuilder { HasDatabaseProviderChanged = false };

      public override void DatabaseProviderChanged()
      {
        HasDatabaseProviderChanged = true;
        base.DatabaseProviderChanged();
      }
    }
  }
}
