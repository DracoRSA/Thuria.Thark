using Thuria.Thark.Core.Statement;

namespace Thuria.Thark.StatementBuilder.Tests
{
  public static class BuilderTestData
  {
    public static object[] SimpleSelectTestCases =
    {
      new object[]
      {
        DatabaseProviderType.SqlServer,
        "TestTable",
        "SELECT * FROM [TestTable]"
      },
      new object[]
      {
        DatabaseProviderType.Postgres,
        "TestTable",
        "SELECT * FROM [TestTable]"
      },
      new object[]
      {
        DatabaseProviderType.Firebird,
        "TestTable",
        "SELECT * FROM \"TestTable\""
      },
    };

    public static object[] SelectTestCasesWithColumns =
    {
      new object[]
      {
        DatabaseProviderType.SqlServer,
        "TestTable",
        new[]
        {
          new[]
          {
            string.Empty,
            "TestColumn1",
            string.Empty
          },
          new[]
          {
            string.Empty,
            "TestColumn2",
            string.Empty
          }
        },
        new object[][]
        {
        },
        "SELECT [TestColumn1], [TestColumn2] FROM [TestTable]",
      },
      new object[]
      {
        DatabaseProviderType.Postgres,
        "TestTable",
        new[]
        {
          new[]
          {
            string.Empty,
            "TestColumn1",
            string.Empty
          },
          new[]
          {
            string.Empty,
            "TestColumn2",
            string.Empty
          }
        },
        new object[][]
        {
        },
        "SELECT [TestColumn1], [TestColumn2] FROM [TestTable]"
      },
      new object[]
      {
        DatabaseProviderType.Firebird,
        "TestTable",
        new[]
        {
          new[]
          {
            string.Empty,
            "TestColumn1",
            string.Empty
          },
          new[]
          {
            string.Empty,
            "TestColumn2",
            string.Empty
          }
        },
        new object[][]
        {
        },
        "SELECT \"TestColumn1\", \"TestColumn2\" FROM \"TestTable\""
      },
      new object[]
      {
        DatabaseProviderType.SqlServer,
        "TestTable",
        new[]
        {
          new[]
          {
            "TestTable",
            "TestColumn1",
            string.Empty
          },
          new[]
          {
            "TestTable",
            "TestColumn2",
            string.Empty
          }
        },
        new object[][]
        {
        },
        "SELECT [TestTable].[TestColumn1], [TestTable].[TestColumn2] FROM [TestTable]"
      },
      new object[]
      {
        DatabaseProviderType.Postgres,
        "TestTable",
        new[]
        {
          new[]
          {
            "TestTable",
            "TestColumn1",
            string.Empty
          },
          new[]
          {
            "TestTable",
            "TestColumn2",
            string.Empty
          }
        },
        new object[][]
        {
        },
        "SELECT [TestTable].[TestColumn1], [TestTable].[TestColumn2] FROM [TestTable]"
      },
      new object[]
      {
        DatabaseProviderType.Firebird,
        "TestTable",
        new[]
        {
          new[]
          {
            "TestTable",
            "TestColumn1",
            string.Empty
          },
          new[]
          {
            "TestTable",
            "TestColumn2",
            string.Empty
          }
        },
        new object[][]
        {
        },
        "SELECT \"TestTable\".\"TestColumn1\", \"TestTable\".\"TestColumn2\" FROM \"TestTable\""
      },
      new object[]
      {
        DatabaseProviderType.SqlServer,
        "TestTable",
        new[]
        {
          new[]
          {
            "TestTable",
            "TestColumn1",
            "TestAlias1"
          },
          new[]
          {
            "TestTable",
            "TestColumn2",
            "TestAlias2"
          }
        },
        new object[][]
        {
        },
        "SELECT [TestTable].[TestColumn1] AS [TestAlias1], [TestTable].[TestColumn2] AS [TestAlias2] FROM [TestTable]"
      },
      new object[]
      {
        DatabaseProviderType.Postgres,
        "TestTable",
        new[]
        {
          new[]
          {
            "TestTable",
            "TestColumn1",
            "TestAlias1"
          },
          new[]
          {
            "TestTable",
            "TestColumn2",
            "TestAlias2"
          }
        },
        new object[][]
        {
        },
        "SELECT [TestTable].[TestColumn1] AS [TestAlias1], [TestTable].[TestColumn2] AS [TestAlias2] FROM [TestTable]"
      },
      new object[]
      {
        DatabaseProviderType.Firebird,
        "TestTable",
        new[]
        {
          new[]
          {
            "TestTable",
            "TestColumn1",
            "TestAlias1"
          },
          new[]
          {
            "TestTable",
            "TestColumn2",
            "TestAlias2"
          }
        },
        new object[][]
        {
        },
        "SELECT \"TestTable\".\"TestColumn1\" AS \"TestAlias1\", \"TestTable\".\"TestColumn2\" AS \"TestAlias2\" FROM \"TestTable\""
      },
      new object[]
      {
        DatabaseProviderType.SqlServer,
        "TestTable",
        new[]
        {
          new[]
          {
            "TestTable",
            "TestColumn1",
            "TestAlias1"
          },
          new[]
          {
            "TestTable",
            "TestColumn2",
            "TestAlias2"
          }
        },
        new[]
        {
          new object[]
          {
            "TestTable",
            "TestCondition",
            EqualityOperators.Equals,
            "TestValue"
          }
        },
        "SELECT [TestTable].[TestColumn1] AS [TestAlias1], [TestTable].[TestColumn2] AS [TestAlias2] FROM [TestTable] WHERE [TestTable].[TestCondition] = 'TestValue'"
      },
      new object[]
      {
        DatabaseProviderType.Postgres,
        "TestTable",
        new[]
        {
          new[]
          {
            "TestTable",
            "TestColumn1",
            "TestAlias1"
          },
          new[]
          {
            "TestTable",
            "TestColumn2",
            "TestAlias2"
          }
        },
        new[]
        {
          new object[]
          {
            "TestTable",
            "TestCondition",
            EqualityOperators.Equals,
            "TestValue"
          }
        },
        "SELECT [TestTable].[TestColumn1] AS [TestAlias1], [TestTable].[TestColumn2] AS [TestAlias2] FROM [TestTable] WHERE [TestTable].[TestCondition] = 'TestValue'"
      },
      new object[]
      {
        DatabaseProviderType.Firebird,
        "TestTable",
        new[]
        {
          new[]
          {
            "TestTable",
            "TestColumn1",
            "TestAlias1"
          },
          new[]
          {
            "TestTable",
            "TestColumn2",
            "TestAlias2"
          }
        },
        new[]
        {
          new object[]
          {
            "TestTable",
            "TestCondition",
            EqualityOperators.Equals,
            "TestValue"
          }
        },
        "SELECT \"TestTable\".\"TestColumn1\" AS \"TestAlias1\", \"TestTable\".\"TestColumn2\" AS \"TestAlias2\" FROM \"TestTable\" WHERE \"TestTable\".\"TestCondition\" = 'TestValue'"
      },
    };
  }
}