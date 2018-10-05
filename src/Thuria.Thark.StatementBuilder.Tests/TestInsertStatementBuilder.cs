using System;

using NUnit.Framework;

using Thuria.Thark.Core.Statement;
using Thuria.Thark.Core.Statement.Builders;
using Thuria.Thark.StatementBuilder.Builders;

namespace Thuria.Thark.StatementBuilder.Tests
{
  [TestFixture]
  public class TestInsertStatementBuilder
  {
    [Test]
    public void Create_ShouldReturnIInsertStatementBuilder()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var builder = InsertStatementBuilder.Create();
      //---------------Test Result -----------------------
      Assert.IsNotNull(builder);
      Assert.IsInstanceOf<IInsertStatementBuilder>(builder);
    }

    [TestCase(DatabaseProviderType.Firebird, DatabaseProviderType.SqlServer)]
    [TestCase(DatabaseProviderType.SqlServer, DatabaseProviderType.Firebird)]
    [TestCase(DatabaseProviderType.SqlServer, DatabaseProviderType.Postgres)]
    public void WithDatabaseProvider_ShouldChangeDatabaseProvider(DatabaseProviderType defaultProviderType, DatabaseProviderType databaseProviderType)
    {
      //---------------Set up test pack-------------------
      var builder = FakeInsertStatementBuilder.Create(defaultProviderType) as FakeInsertStatementBuilder;
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      builder.WithDatabaseProvider(databaseProviderType);
      //---------------Test Result -----------------------
      Assert.IsTrue(builder.HasDatabaseProviderChanged);
    }

    [Test]
    public void Build_GivenNoInsertTable_ShouldThrowException()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var exception = Assert.Throws<StatementBuilderException>(() => InsertStatementBuilder.Create().Build());
      //---------------Test Result -----------------------
      StringAssert.Contains("INSERT Statement Validation errors occurred", exception.Message);
    }

    [Test]
    public void Build_GivenNoColumn_ShouldThrowException()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var exception = Assert.Throws<StatementBuilderException>(() => InsertStatementBuilder.Create().WithTable("TestTable").Build());
      //---------------Test Result -----------------------
      StringAssert.Contains("INSERT Statement Validation errors occurred", exception.Message);
    }

    [Test]
    public void Build_GivenValidValues_ShouldReturnExpectedStatement()
    {
      //---------------Set up test pack-------------------
      var recordId = Guid.NewGuid();
      var expectedStatement = $"INSERT INTO [TestTable] ([Id],[Description],[IsActive]) VALUES ('{recordId}','TestDescription',1)";
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var sqlStatement = InsertStatementBuilder.Create().WithTable("TestTable").WithColumn("Id", recordId).WithColumn("Description", "TestDescription").WithColumn("IsActive", true).Build();
      //---------------Test Result -----------------------
      Assert.AreEqual(expectedStatement, sqlStatement);
    }

    private class FakeInsertStatementBuilder : InsertStatementBuilder
    {
      private FakeInsertStatementBuilder() : base()
      {
      }

      public bool HasDatabaseProviderChanged { get; set; }

      public static IInsertStatementBuilder Create(DatabaseProviderType providerType)
      {
        var builder = new FakeInsertStatementBuilder();
        builder.UpdateDatabaseProvider(providerType);
        builder.HasDatabaseProviderChanged = false;

        return builder;
      }

      public override void DatabaseProviderChanged()
      {
        this.HasDatabaseProviderChanged = true;
        base.DatabaseProviderChanged();
      }
    }
  }
}
