using NUnit.Framework;

using Thuria.Calot.TestUtilities;
using Thuria.Thark.StatementBuilder.Models;
using Thuria.Thark.StatementBuilder.Providers;

namespace Thuria.Thark.StatementBuilder.Tests
{
  [TestFixture]
  public class TestSelectColumnModel
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var selectColumn = new ColumnModel("tableName", "columnName", "columnAlias");
      //---------------Test Result -----------------------
      Assert.IsNotNull(selectColumn);
    }

    [TestCase("columnName")]
    public void Constructor_GivenNullParameter_ShouldThrowException(string parameterName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      ConstructorTestHelper.ValidateArgumentNullExceptionIfParameterIsNull<ColumnModel>(parameterName);
      //---------------Test Result -----------------------
    }

    [Test]
    public void Constructor_GivenColumnName_ShouldSetColumnProperty()
    {
      //---------------Set up test pack-------------------
      var columnName = "columnName";
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var selectColumn = new ColumnModel(columnName);
      //---------------Test Result -----------------------
      Assert.AreEqual(columnName, selectColumn.ColumnName);
    }

    [Test]
    public void Constructor_GivenTable_And_ColumnName_ShouldSetAllProperties()
    {
      //---------------Set up test pack-------------------
      var tableName  = "tableName";
      var columnName = "columnName";
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var selectColumn = new ColumnModel(tableName, columnName);
      //---------------Test Result -----------------------
      Assert.AreEqual(tableName, selectColumn.TableName);
      Assert.AreEqual(columnName, selectColumn.ColumnName);
    }

    [Test]
    public void Constructor_GivenTableName_And_ColumnName_And_Alias_ShouldSetAllProperties()
    {
      //---------------Set up test pack-------------------
      var tableName   = "tableName";
      var columnName  = "columnName";
      var columnAlias = "columnAlias";
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var selectColumn = new ColumnModel(tableName, columnName, columnAlias);
      //---------------Test Result -----------------------
      Assert.AreEqual(tableName, selectColumn.TableName);
      Assert.AreEqual(columnName, selectColumn.ColumnName);
      Assert.AreEqual(columnAlias, selectColumn.Alias);
    }

    [Test]
    public void Properties_GivenDatabaseProvider_ShouldSetProperty()
    {
      //---------------Set up test pack-------------------
      var sqlProvider = new SqlServerDatabaseProvider();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var selectColumn = new ColumnModel("testColumn")
        {
          DatabaseProvider = sqlProvider
        };
      //---------------Test Result -----------------------
      Assert.AreEqual(sqlProvider, selectColumn.DatabaseProvider);
    }

    [TestCase("", "testColumn", "", "[testColumn]")]
    [TestCase("testTable", "testColumn", "", "[testTable].[testColumn]")]
    [TestCase("testTable", "testColumn", "testAlias", "[testTable].[testColumn] AS [testAlias]")]
    public void ToString_GivenColumnValues_ShouldReturnExpectedStatement(string tableName, string columnName, string columnAlias, string expectedStatement)
    {
      //---------------Set up test pack-------------------
      var selectColumn = new ColumnModel(tableName, columnName, columnAlias)
        {
          DatabaseProvider = new SqlServerDatabaseProvider()
        };
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var selectStatement = selectColumn.ToString();
      //---------------Test Result -----------------------
      Assert.AreEqual(expectedStatement, selectStatement);
    }

    [TestCase("", "testColumn", "")]
    [TestCase("testTable", "testColumn", "")]
    [TestCase("testTable", "testColumn", "testAlias")]
    public void Equals_GivenSameColumnData_ShouldReturnTrue(string tableName, string columnName, string columnAlias)
    {
      //---------------Set up test pack-------------------
      var selectColumn = new ColumnModel(tableName, columnName, columnAlias)
        {
          DatabaseProvider = new SqlServerDatabaseProvider()
        };
      var compareColumn = new ColumnModel(tableName, columnName, columnAlias)
        {
          DatabaseProvider = new SqlServerDatabaseProvider()
        };
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var equalResult = selectColumn.Equals(compareColumn);
      //---------------Test Result -----------------------
      Assert.IsTrue(equalResult);
    }

    [TestCase("", "testColumn", "")]
    [TestCase("testTable", "testColumn", "")]
    [TestCase("testTable", "testColumn", "testAlias")]
    public void Equals_GivenDifferentColumnData_ShouldReturnFalse(string tableName, string columnName, string columnAlias)
    {
      //---------------Set up test pack-------------------
      var selectColumn = new ColumnModel(tableName, columnName, columnAlias)
        {
          DatabaseProvider = new SqlServerDatabaseProvider()
        };
      var compareColumn = new ColumnModel(string.Empty, "differentColumn", string.Empty)
        {
          DatabaseProvider = new SqlServerDatabaseProvider()
        };
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var equalResult = selectColumn.Equals(compareColumn);
      //---------------Test Result -----------------------
      Assert.IsFalse(equalResult);
    }

    [Test]
    public void GetHashCode_ShouldReturnStringHashCode()
    {
      //---------------Set up test pack-------------------
      var selectColumn = new ColumnModel("testTable", "testColumn", "testAlias")
        {
          DatabaseProvider = new SqlServerDatabaseProvider()
        };
      var hashCode = 0;
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      Assert.DoesNotThrow(() => hashCode = selectColumn.GetHashCode());
      //---------------Test Result -----------------------
      Assert.IsNotNull(hashCode);
    }
  }
}
