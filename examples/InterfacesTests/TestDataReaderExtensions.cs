using System;
using System.Data;
using Moq;
using NUnit.Framework;
using FluentAssertions;

namespace MGS.Casino.Veyron.DataAccessInterfaces.Tests
{
  [TestFixture]
  public class TestDataReaderExtensions
  {
    [Test]
    public void GetValue_GivenDataReaderIsNull_ShouldReturnNull()
    {
      //---------------Set up test pack-------------------
      object returnValue = null;
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var exception = Assert.Throws<ArgumentNullException>(() => returnValue = ((IDataReader)null).GetValue("TestColumn"));
      //---------------Test Result -----------------------
      exception.ParamName.Should().Be("dataReader");
    }

    [Test]
    public void GetValue_GiveColumnExistsAndColumnValueIsNull_ShouldReturnNull()
    {
      //---------------Set up test pack-------------------
      var columnName = "TestColumn";
      var dataReader = new Mock<IDataReader>();
      dataReader.Setup(reader => reader[columnName]).Returns(DBNull.Value);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var columnValue = dataReader.Object.GetValue(columnName);
      //---------------Test Result -----------------------
      columnValue.Should().BeNull();
    }

    [Test]
    public void GetValue_GiveColumnExistsAndColumnValueIsNullAndIsMandatory_ShouldThrowException()
    {
      //---------------Set up test pack-------------------
      var columnName = "TestColumn";
      var dataReader = new Mock<IDataReader>();
      dataReader.Setup(reader => reader[columnName]).Returns(DBNull.Value);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var exception = Assert.Throws<Exception>(() => dataReader.Object.GetValue(columnName, isMandatory: true));
      //---------------Test Result -----------------------
      exception.Message.Should().Be($"Mandatory value for column {columnName} is NULL.");
    }

    [TestCase("DisplayName", "Test Name")]
    [TestCase("Id", 12345)]
    public void GetValue_GivenColumnExistsAndHasValue_ShouldReturnColumnValue(string columnName, object expectedColumnValue)
    {
      //---------------Set up test pack-------------------
      var dataReader = new Mock<IDataReader>();
      dataReader.Setup(reader => reader[columnName]).Returns(expectedColumnValue);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var columnValue = dataReader.Object.GetValue(columnName);
      //---------------Test Result -----------------------
      columnValue.Should().Be(expectedColumnValue);
    }

    [Test]
    public void GetValue_Generic_GivenDataReaderIsNull_ShouldThrowException()
    {
      //---------------Set up test pack-------------------
      object returnValue = null; 
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var exception = Assert.Throws<ArgumentNullException>(() => returnValue = ((IDataReader)null).GetValue<string>("TestColumn"));
      //---------------Test Result -----------------------
      exception.ParamName.Should().Be("dataReader");
    }
    
    [Test]
    public void GetValue_Generic_GiveColumnExistsAndColumnValueIsNull_ShouldReturnNull()
    {
      //---------------Set up test pack-------------------
      var columnName = "TestColumn";
      var dataReader = new Mock<IDataReader>();
      dataReader.Setup(reader => reader[columnName]).Returns(DBNull.Value);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var columnValue = dataReader.Object.GetValue<string>(columnName);
      //---------------Test Result -----------------------
      columnValue.Should().BeNull();
    }
    
    [Test]
    public void GetValue_Generic_GiveColumnExistsAndColumnValueIsNullAndIsMandatory_ShouldThrowException()
    {
      //---------------Set up test pack-------------------
      var columnName = "TestColumn";
      var dataReader = new Mock<IDataReader>();
      dataReader.Setup(reader => reader[columnName]).Returns(DBNull.Value);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var exception = Assert.Throws<Exception>(() => dataReader.Object.GetValue<string>(columnName, isMandatory: true));
      //---------------Test Result -----------------------
      exception.Message.Should().Be($"Mandatory value for column {columnName} is NULL.");
    }
    
    [Test]
    public void GetValue_Generic_GivenColumnExistsAndHasValue_ShouldReturnColumnValue()
    {
      //---------------Set up test pack-------------------
      var columnName = "DisplayName";
      var expectedColumnValue = "Test Name";
      var dataReader = new Mock<IDataReader>();
      dataReader.Setup(reader => reader[columnName]).Returns(expectedColumnValue);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var columnValue = dataReader.Object.GetValue<string>(columnName);
      //---------------Test Result -----------------------
      columnValue.Should().Be(expectedColumnValue);
    }
  }
}
