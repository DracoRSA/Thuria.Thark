using System;

using FluentAssertions;
using Moq;
using NSubstitute;
using NUnit.Framework;

using Thuria.Thark.Core.Statement;
using Thuria.Thark.Core.Statement.Builders;
using Thuria.Thark.Core.Statement.Models;
using Thuria.Thark.StatementBuilder.Builders;

namespace Thuria.Thark.StatementBuilder.Tests
{
  [TestFixture]
  public class TestUpdateStatementBuilder
  {
    [Test]
    public void Create_ShouldReturnIUpdateStatementBuilder()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var builder = UpdateStatementBuilder.Create();
      //---------------Test Result -----------------------
      Assert.IsNotNull(builder);
      Assert.IsInstanceOf<IUpdateStatementBuilder>(builder);
    }

    [TestCase(DatabaseProviderType.Firebird, DatabaseProviderType.SqlServer)]
    [TestCase(DatabaseProviderType.SqlServer, DatabaseProviderType.Firebird)]
    [TestCase(DatabaseProviderType.SqlServer, DatabaseProviderType.Postgres)]
    public void WithDatabaseProvider_ShouldChangeDatabaseProvider(DatabaseProviderType defaultProviderType, DatabaseProviderType databaseProviderType)
    {
      //---------------Set up test pack-------------------
      var builder = FakeUpdateStatementBuilder.Create(defaultProviderType) as FakeUpdateStatementBuilder;
      //---------------Assert Precondition----------------
      builder.Should().NotBeNull();
      //---------------Execute Test ----------------------
      builder?.WithDatabaseProvider(databaseProviderType);
      //---------------Test Result -----------------------
      Assert.IsTrue(builder?.HasDatabaseProviderChanged);
    }

    [Test]
    public void Build_GivenNoUpdateTable_ShouldThrowException()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var exception = Assert.Throws<StatementBuilderException>(() => UpdateStatementBuilder.Create().Build());
      //---------------Test Result -----------------------
      StringAssert.Contains("UPDATE Statement Validation errors occurred", exception.Message);
      StringAssert.Contains("Table Name must be specified to create an UPDATE Statement", exception.Message);
    }

    [Test]
    public void Build_GivenNoColumn_ShouldThrowException()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var exception = Assert.Throws<StatementBuilderException>(() => UpdateStatementBuilder.Create().WithTable("TestTable").Build());
      //---------------Test Result -----------------------
      StringAssert.Contains("UPDATE Statement Validation errors occurred", exception.Message);
      StringAssert.Contains("At least one column must be specified to create an UPDATE Statement", exception.Message);
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
      var exception = Assert.Throws<ArgumentNullException>(() => UpdateStatementBuilder.Create()
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
      UpdateStatementBuilder.Create()
                            .WithDatabaseProvider(DatabaseProviderType.Postgres)
                            .WithTable("TestTable")
                            .WithColumn("Id", "Test")
                            .WithWhereCondition(testCondition)
                            .Build();
      //---------------Test Result -----------------------
      testCondition.DatabaseProvider.DatabaseProviderType.Should().Be(DatabaseProviderType.Postgres);
    }

    [Test]
    public void Build_GivenValidValues_ShouldReturnExpectedStatement()
    {
      //---------------Set up test pack-------------------
      var recordId          = Guid.NewGuid();
      var recordDescription = "TestDescription";
      var expectedStatement = $"UPDATE [TestTable] SET [Description] = '{recordDescription}',[IsActive] = 1 WHERE [TestTable].[Id] = '{recordId}'";
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var sqlStatement = UpdateStatementBuilder.Create()
                              .WithTable("TestTable")
                              .WithColumn("Description", recordDescription)
                              .WithColumn("IsActive", true)
                              .WithWhereCondition("TestTable", "Id", EqualityOperators.Equals, recordId)
                              .Build();
      //---------------Test Result -----------------------
      Assert.AreEqual(expectedStatement, sqlStatement);
    }

    private class FakeUpdateStatementBuilder : UpdateStatementBuilder
    {
      private FakeUpdateStatementBuilder()
      {
      }

      public bool HasDatabaseProviderChanged { get; private set; }

      public static IUpdateStatementBuilder Create(DatabaseProviderType providerType)
      {
        var builder = new FakeUpdateStatementBuilder();
        builder.UpdateDatabaseProvider(providerType);
        builder.HasDatabaseProviderChanged = false;

        return builder;
      }

      public override void DatabaseProviderChanged()
      {
        HasDatabaseProviderChanged = true;
        base.DatabaseProviderChanged();
      }
    }
  }
}
