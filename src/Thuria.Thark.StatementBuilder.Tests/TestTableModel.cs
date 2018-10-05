using NUnit.Framework;

using Thuria.Calot.TestUtilities;
using Thuria.Thark.StatementBuilder.Models;
using Thuria.Thark.StatementBuilder.Providers;

namespace Thuria.Thark.StatementBuilder.Tests
{
  [TestFixture]
  public class TestTableModel
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var selectColumn = new TableModel("tableName", "columnAlias");
      //---------------Test Result -----------------------
      Assert.IsNotNull(selectColumn);
    }

    [TestCase("tableName")]
    public void Constructor_GivenNullParameter_ShouldThrowException(string parameterName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      ConstructorTestHelper.ValidateArgumentNullExceptionIfParameterIsNull<TableModel>(parameterName);
      //---------------Test Result -----------------------
    }

    [Test]
    public void Constructor_GivenTableName_ShouldSetColumnProperty()
    {
      //---------------Set up test pack-------------------
      var tableName = "tableName";
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var tableModel = new TableModel(tableName);
      //---------------Test Result -----------------------
      Assert.AreEqual(tableName, tableModel.TableName);
    }

    [Test]
    public void Constructor_GivenTableName_And_Alias_ShouldSetAllProperties()
    {
      //---------------Set up test pack-------------------
      var tableName  = "tableName";
      var tableAlias = "tableAlias";
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var tableModel = new TableModel(tableName, tableAlias);
      //---------------Test Result -----------------------
      Assert.AreEqual(tableName, tableModel.TableName);
      Assert.AreEqual(tableAlias, tableModel.Alias);
    }

    [Test]
    public void Properties_GivenDatabaseProvider_ShouldSetProperty()
    {
      //---------------Set up test pack-------------------
      var sqlProvider = new SqlServerDatabaseProvider();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var tableModel = new TableModel("tableName")
        {
          DatabaseProvider = sqlProvider
        };
      //---------------Test Result -----------------------
      Assert.AreEqual(sqlProvider, tableModel.DatabaseProvider);
    }

    [TestCase("TestTable", "", "[TestTable]")]
    [TestCase("TestTable", "TestAlias", "[TestTable] AS [TestAlias]")]
    public void ToString_GivenTableModelValues_ShouldReturnExpectedStatement(string tableName, string tableAlias, string expectedStatement)
    {
      //---------------Set up test pack-------------------
      var tableModel = new TableModel(tableName, tableAlias)
        {
          DatabaseProvider = new SqlServerDatabaseProvider()
        };
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var selectStatement = tableModel.ToString();
      //---------------Test Result -----------------------
      Assert.AreEqual(expectedStatement, selectStatement);
    }

    [TestCase("TestTable", "")]
    [TestCase("TestTable", "")]
    public void Equals_GivenSameTableData_ShouldReturnTrue(string tableName, string tableAlias)
    {
      //---------------Set up test pack-------------------
      var tableModel = new TableModel(tableName, tableAlias)
        {
          DatabaseProvider = new SqlServerDatabaseProvider()
        };
      var compareModel = new TableModel(tableName, tableAlias)
        {
          DatabaseProvider = new SqlServerDatabaseProvider()
        };
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var equalResult = tableModel.Equals(compareModel);
      //---------------Test Result -----------------------
      Assert.IsTrue(equalResult);
    }

    [TestCase("TestTable", "")]
    [TestCase("TestTable", "")]
    public void Equals_GivenDifferentTableData_ShouldReturnFalse(string tableName, string tableAlias)
    {
      //---------------Set up test pack-------------------
      var tableModel = new TableModel(tableName, tableAlias)
        {
          DatabaseProvider = new SqlServerDatabaseProvider()
        };
      var compareModel = new TableModel("differentTable")
        {
          DatabaseProvider = new SqlServerDatabaseProvider()
        };
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var equalResult = tableModel.Equals(compareModel);
      //---------------Test Result -----------------------
      Assert.IsFalse(equalResult);
    }

    public void Equals_GivenNullTableData_ShouldReturnFalse(string tableName, string tableAlias)
    {
      //---------------Set up test pack-------------------
      var tableModel = new TableModel(tableName, tableAlias)
        {
          DatabaseProvider = new SqlServerDatabaseProvider()
        };
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var equalResult = tableModel.Equals(null);
      //---------------Test Result -----------------------
      Assert.IsFalse(equalResult);
    }

    [Test]
    public void GetHashCode_ShouldReturnStringHashCode()
    {
      //---------------Set up test pack-------------------
      var tableModel = new TableModel("TestTable", "testAlias")
        {
          DatabaseProvider = new SqlServerDatabaseProvider()
        };
      var hashCode = 0;
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      Assert.DoesNotThrow(() => hashCode = tableModel.GetHashCode());
      //---------------Test Result -----------------------
      Assert.IsNotNull(hashCode);
    }
  }
}
