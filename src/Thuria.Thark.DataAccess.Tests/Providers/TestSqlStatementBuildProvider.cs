using System;
using NSubstitute;
using NUnit.Framework;
using FluentAssertions;

using Thuria.Calot.TestUtilities;
using Thuria.Thark.Core.Providers;
using Thuria.Thark.DataAccess.Providers;
using Thuria.Thark.Core.Statement.Builders;
using Thuria.Thark.StatementBuilder.Builders;

namespace Thuria.Thark.DataAccess.Tests.Providers
{
  [TestFixture]
  public class TestSqlStatementBuildProvider
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      var selectStatementBuilder = Substitute.For<ISelectStatementBuilder>();
      var insertStatementBuilder = Substitute.For<IInsertStatementBuilder>();
      var updateStatementBuilder = Substitute.For<IUpdateStatementBuilder>();
      var conditionBuilder       = Substitute.For<IConditionBuilder>();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var buildProvider = new SqlStatementBuildProvider(selectStatementBuilder, insertStatementBuilder, updateStatementBuilder, conditionBuilder);
      //---------------Test Result -----------------------
      buildProvider.Should().NotBeNull();
    }

    [TestCase("selectStatementBuilder")]
    [TestCase("insertStatementBuilder")]
    [TestCase("updateStatementBuilder")]
    [TestCase("conditionBuilder")]
    public void Constructor_GivenNullParameter_ShouldThrowArgumentNullException(string parameterName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      ConstructorTestHelper.ValidateArgumentNullExceptionIfParameterIsNull<SqlStatementBuildProvider>(parameterName);
      //---------------Test Result -----------------------
    }

    [Test]
    public void BuildSelectStatement_GivenNullDataModel_ShouldThrowArgumentNullException()
    {
      //---------------Set up test pack-------------------
      FakeTestDataModel dataModel = null;
      var buildProvider           = CreateProvider();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      // ReSharper disable once ExpressionIsAlwaysNull
      var exception = Assert.Throws<ArgumentNullException>(() => buildProvider.BuildSelectStatement(dataModel));
      //---------------Test Result -----------------------
      exception.ParamName.Should().Be("dataModel");
    }

    [Test]
    public void BuildSelectStatement_GivenDataModel_ShouldBuildExpectedStatement()
    {
      //---------------Set up test pack-------------------
      FakeTestDataModel dataModel = new FakeTestDataModel
                                      {
                                        Id = Guid.NewGuid()
                                      };
      var expectedStatement = $"SELECT [FakeTest].[Id], [FakeTest].[Name], [FakeTest].[Date], [FakeTest].[SomeFieldAlias] AS [SomeField] " +
                              $"FROM [FakeTest] " +
                              $"WHERE  [FakeTest].[Id] = '{dataModel.Id}'";
      var buildProvider     = CreateProvider();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      // ReSharper disable once ExpressionIsAlwaysNull
      var selectStatement = buildProvider.BuildSelectStatement(dataModel);
      //---------------Test Result -----------------------
      selectStatement.Trim().Should().Be(expectedStatement);
    }

    [Test]
    public void BuildInsertStatement_GivenNullDataModel_ShouldThrowArgumentNullException()
    {
      //---------------Set up test pack-------------------
      FakeTestDataModel dataModel = null;
      var buildProvider           = CreateProvider();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      // ReSharper disable once ExpressionIsAlwaysNull
      var exception = Assert.Throws<ArgumentNullException>(() => buildProvider.BuildInsertStatement(dataModel));
      //---------------Test Result -----------------------
      exception.ParamName.Should().Be("dataModel");
    }

    [Test]
    public void BuildInsertStatement_GivenDataModel_ShouldBuildExpectedStatement()
    {
      //---------------Set up test pack-------------------
      FakeTestDataModel dataModel = new FakeTestDataModel
                                      {
                                        Id        = Guid.NewGuid(),
                                        Name      = RandomValueGenerator.CreateRandomString(10, 20),
                                        Date      = RandomValueGenerator.CreateRandomDate(),
                                        SomeField = RandomValueGenerator.CreateRandomString(15, 30)
                                      };
      var expectedStatement = "INSERT INTO [FakeTest] ([Id],[Name],[Date],[SomeFieldAlias]) " +
                              $"VALUES ('{dataModel.Id}','{dataModel.Name}','{dataModel.Date}','{dataModel.SomeField}')";
      var buildProvider = CreateProvider();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      // ReSharper disable once ExpressionIsAlwaysNull
      var insertStatement = buildProvider.BuildInsertStatement(dataModel);
      //---------------Test Result -----------------------
      insertStatement.Trim().Should().Be(expectedStatement);
    }

    [Test]
    public void BuildUpdateStatement_GivenNullDataModel_ShouldThrowArgumentNullException()
    {
      //---------------Set up test pack-------------------
      FakeTestDataModel dataModel = null;
      var buildProvider           = CreateProvider();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      // ReSharper disable once ExpressionIsAlwaysNull
      var exception = Assert.Throws<ArgumentNullException>(() => buildProvider.BuildUpdateStatement(dataModel));
      //---------------Test Result -----------------------
      exception.ParamName.Should().Be("dataModel");
    }

    [Test]
    public void BuildUpdateStatement_GivenDataModel_ShouldBuildExpectedStatement()
    {
      //---------------Set up test pack-------------------
      FakeTestDataModel dataModel = new FakeTestDataModel
                                      {
                                        Id = Guid.NewGuid(),
                                        Name = RandomValueGenerator.CreateRandomString(10, 20),
                                        Date = RandomValueGenerator.CreateRandomDate(),
                                        SomeField = RandomValueGenerator.CreateRandomString(15, 30)
                                      };
      var expectedStatement = "UPDATE [FakeTest] " +
                              $"SET [Name] = '{dataModel.Name}',[Date] = '{dataModel.Date}',[SomeFieldAlias] = '{dataModel.SomeField}' " +
                              $"WHERE  [FakeTest].[Id] = '{dataModel.Id}'";
      var buildProvider = CreateProvider();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      // ReSharper disable once ExpressionIsAlwaysNull
      var updateStatement = buildProvider.BuildUpdateStatement(dataModel);
      //---------------Test Result -----------------------
      updateStatement.Trim().Should().Be(expectedStatement);
    }

    private IStatementBuildProvider CreateProvider()
    {
      var selectStatementBuilder = new SelectStatementBuilder();
      var insertStatementBuilder = new InsertStatementBuilder();
      var updateStatementBuilder = new UpdateStatementBuilder();
      var conditionBuilder       = new ConditionBuilder();

      return new SqlStatementBuildProvider(selectStatementBuilder, 
                                           insertStatementBuilder, 
                                           updateStatementBuilder, 
                                           conditionBuilder);
    }
  }
}
