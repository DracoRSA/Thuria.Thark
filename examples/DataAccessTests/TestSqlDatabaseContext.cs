using System;
using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NSubstitute;
using NUnit.Framework;
using FluentAssertions;
using MGS.Casino.Veyron.TestUtilities;
using MGS.Casino.Veyron.DataAccessInterfaces;
using MGS.Casino.Veyron.Extensions;

namespace MGS.Casino.Veyron.DataAccess.Tests
{
  [TestFixture]
  public class TestSqlDatabaseContext
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      var dbConnection      = new Mock<IVeyronDbConnection>();
      var parameterProvider = new Mock<IDbConnectionParameterProvider>();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var databaseContext = new SqlDatabaseContext("connectionString", dbConnection.Object, parameterProvider.Object);
      //---------------Test Result -----------------------
      databaseContext.Should().NotBeNull();
    }

    [TestCase("connectionString")]
    [TestCase("dbConnection")]
    [TestCase("dbConnectionParameterProvider")]
    public void Constructor_GivenNullParameter_ShouldThrowException(string parameterName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      ConstructorTestHelper.ValidateArgumentNullExceptionIfParameterIsNull<SqlDatabaseContext>(parameterName);
      //---------------Test Result -----------------------
    }

    [TestCase("connectionString", "ConnectionString")]
    [TestCase("dbConnection", "DbConnection")]
    public void Constructor_GivenParameterValues_ShouldSetProperties(string parameterName, string propertyName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      ConstructorTestHelper.ValidatePropertySetWithParameter<SqlDatabaseContext>(parameterName, propertyName);
      //---------------Test Result -----------------------
    }

    [TestCase("CommandTimeout")]
    public void Properties_GivenValue_ShouldSetPropertyValue(string propertyName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      PropertyTestHelper.ValidateGetAndSet<SqlDatabaseContext>(propertyName);
      //---------------Test Result -----------------------
    }

    [Test]
    public void Dispose_ShouldCallCloseOnDbConnection()
    {
      //---------------Set up test pack-------------------
      var dbConnection                  = new Mock<IVeyronDbConnection>();
      var dbConnectionParameterProvider = new Mock<IDbConnectionParameterProvider>();
      var connectionString              = "connectionString";
      var dbContext                     = new SqlDatabaseContext(connectionString, dbConnection.Object, dbConnectionParameterProvider.Object);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      Assert.DoesNotThrow(() => dbContext.Dispose());
      //---------------Test Result -----------------------
      dbConnection.Verify(connection => connection.Close());
    }

    [Test]
    public async Task ExecuteStoredProcedureAsync_GivenValidParameters_ShouldCreateExpectedDbCommand()
    {
      //---------------Set up test pack-------------------
      var storedProcName = "TestStoredProc";
      var commandTimeout = 199;
      var dbDataReader   = Substitute.For<IDataReader>();
      var dbCommand      = Substitute.For<IDbCommand>();
      dbCommand.ExecuteReader().Returns(dbDataReader);

      var veyronDbConnection = Substitute.For<IVeyronDbConnection>();
      veyronDbConnection.CreateCommand().Returns(dbCommand);

      using (var dbContext = CreateDatabaseContext(veyronDbConnection: veyronDbConnection, commandTimeout: commandTimeout))
      {
        //---------------Assert Precondition----------------
        //---------------Execute Test ----------------------
        await dbContext.ExecuteStoredProcedureAsync(storedProcName, new List<DataAccessParameter>());
        //---------------Test Result -----------------------
        dbCommand.Received().CommandType    = CommandType.StoredProcedure;
        dbCommand.Received().CommandText    = storedProcName;
        dbCommand.Received().CommandTimeout = commandTimeout;
      }
    }

    [Test]
    public async Task ExecuteAsync_GivenValidParameters_ShouldCreateExpectedDbCommand()
    {
      //---------------Set up test pack-------------------
      var storedProcName = "TestStoredProc";
      var commandTimeout = 199;
      var dbDataReader   = Substitute.For<IDataReader>();
      var dbCommand      = Substitute.For<IDbCommand>();
      dbCommand.ExecuteReader().Returns(dbDataReader);

      var veyronDbConnection = Substitute.For<IVeyronDbConnection>();
      veyronDbConnection.CreateCommand().Returns(dbCommand);

      using (var dbContext = CreateDatabaseContext(veyronDbConnection: veyronDbConnection, commandTimeout: commandTimeout))
      {
        //---------------Assert Precondition----------------
        //---------------Execute Test ----------------------
        var (dataReader, outputResults) = await dbContext.ExecuteAsync(storedProcName, new List<DataAccessParameter>());
        //---------------Test Result -----------------------
        dbCommand.Received().CommandType    = CommandType.StoredProcedure;
        dbCommand.Received().CommandText    = storedProcName;
        dbCommand.Received().CommandTimeout = commandTimeout;
      }
    }

    [Test]
    public async Task ExecuteReaderAsync_GivenValidParameters_ShouldCreateExpectedDbCommand()
    {
      //---------------Set up test pack-------------------
      var storedProcName = "TestStoredProc";
      var commandTimeout = 199;
      var dbDataReader   = Substitute.For<IDataReader>();
      var dbCommand      = Substitute.For<IDbCommand>();
      dbCommand.ExecuteReader().Returns(dbDataReader);

      var veyronDbConnection = Substitute.For<IVeyronDbConnection>();
      veyronDbConnection.CreateCommand().Returns(dbCommand);

      using (var dbContext = CreateDatabaseContext(veyronDbConnection: veyronDbConnection, commandTimeout: commandTimeout))
      {
        //---------------Assert Precondition----------------
        //---------------Execute Test ----------------------
        var dataReader = await dbContext.ExecuteReaderAsync(storedProcName, new List<DataAccessParameter>());
        //---------------Test Result -----------------------
        dbCommand.Received().CommandType    = CommandType.StoredProcedure;
        dbCommand.Received().CommandText    = storedProcName;
        dbCommand.Received().CommandTimeout = commandTimeout;
      }
    }

    [Test]
    public async Task ExecuteStoredProcedureAsync_Generic_GivenValidParameters_ShouldCreateExpectedDbCommand()
    {
      //---------------Set up test pack-------------------
      var storedProcName = "TestStoredProc";
      var commandTimeout = 199;
      var dbDataReader   = Substitute.For<IDataReader>();
      var dbCommand      = Substitute.For<IDbCommand>();
      dbCommand.ExecuteReader().Returns(dbDataReader);

      var veyronDbConnection = Substitute.For<IVeyronDbConnection>();
      veyronDbConnection.CreateCommand().Returns(dbCommand);

      using (var dbContext = CreateDatabaseContext(veyronDbConnection: veyronDbConnection, commandTimeout: commandTimeout))
      {
        //---------------Assert Precondition----------------
        //---------------Execute Test ----------------------
        await dbContext.ExecuteStoredProcedureAsync<TestFakeDataModel>(storedProcName, new List<DataAccessParameter>());
        //---------------Test Result -----------------------
        dbCommand.Received().CommandType    = CommandType.StoredProcedure;
        dbCommand.Received().CommandText    = storedProcName;
        dbCommand.Received().CommandTimeout = commandTimeout;
      }
    }

    [Test]
    public async Task ExecuteAsync_GivenValidParameters_ShouldReturnDataReaderAndNotCloseDataReader()
    {
      //---------------Set up test pack-------------------
      var storedProcName = "TestStoredProc";
      var dbDataReader   = Substitute.For<IDataReader>();
      var dbCommand      = Substitute.For<IDbCommand>();
      dbCommand.ExecuteReader().Returns(dbDataReader);

      var veyronDbConnection = Substitute.For<IVeyronDbConnection>();
      veyronDbConnection.CreateCommand().Returns(dbCommand);

      using (var dbContext = CreateDatabaseContext(veyronDbConnection: veyronDbConnection))
      {
        //---------------Assert Precondition----------------
        //---------------Execute Test ----------------------
        var (dataReader, outputResults) = await dbContext.ExecuteAsync(storedProcName, new List<DataAccessParameter>());
        //---------------Test Result -----------------------
        dataReader.Should().Be(dbDataReader);
        dbDataReader.DidNotReceive().Close();
      }
    }

    [Test]
    public async Task ExecuteReaderAsync_GivenValidParameters_ShouldReturnDataReaderAndNotCloseDataReader()
    {
      //---------------Set up test pack-------------------
      var storedProcName = "TestStoredProc";
      var dbDataReader   = Substitute.For<IDataReader>();
      var dbCommand      = Substitute.For<IDbCommand>();
      dbCommand.ExecuteReader().Returns(dbDataReader);

      var veyronDbConnection = Substitute.For<IVeyronDbConnection>();
      veyronDbConnection.CreateCommand().Returns(dbCommand);

      using (var dbContext = CreateDatabaseContext(veyronDbConnection: veyronDbConnection))
      {
        //---------------Assert Precondition----------------
        //---------------Execute Test ----------------------
        var dataReader = await dbContext.ExecuteReaderAsync(storedProcName, new List<DataAccessParameter>());
        //---------------Test Result -----------------------
        dataReader.Should().Be(dbDataReader);
        dbDataReader.DidNotReceive().Close();
      }
    }

    [Test]
    public async Task ExecuteStoredProcedureAsync_GivenValidParameters_ShouldReturnOutputValuesAndCloseDataReader()
    {
      //---------------Set up test pack-------------------
      var storedProcName            = "TestStoredProc";
      var storedProcedureParameters = new List<DataAccessParameter>
        {
          new DataAccessParameter("Parameter1", typeof(int), RandomValueGenerator.CreateRandomLong(1, 15)),
          new DataAccessParameter("Parameter2", typeof(DateTime), RandomValueGenerator.CreateRandomDate()),
          new DataAccessParameter("Parameter3", typeof(long), RandomValueGenerator.CreateRandomLong(), DataParameterDirection.Output),
        };

      var sqlParameterProvider    = new SqlParameterProvider();
      var dbDataReader            = Substitute.For<IDataReader>();
      var dataParameterCollection = new TestDataParameterCollection();
      var dbCommand               = Substitute.For<IDbCommand>();
      dbCommand.Parameters.Returns(dataParameterCollection);
      dbCommand.ExecuteReader().Returns(dbDataReader)
               .AndDoes(
                 info =>
                   {
                     dataParameterCollection.Add(sqlParameterProvider.Convert(storedProcedureParameters[2]));
                   });

      var veyronDbConnection = Substitute.For<IVeyronDbConnection>();
      veyronDbConnection.CreateCommand().Returns(dbCommand);

      using (var dbContext = CreateDatabaseContext(veyronDbConnection: veyronDbConnection))
      {
        //---------------Assert Precondition----------------
        //---------------Execute Test ----------------------
        var outputResults = await dbContext.ExecuteStoredProcedureAsync(storedProcName, storedProcedureParameters);
        //---------------Test Result -----------------------
        dbDataReader.Received().Close();
        outputResults.Should().NotBeNull();
        outputResults.Count.Should().Be(1);
        outputResults?.ContainsKey("Parameter3");
      }
    }

    [Test]
    public async Task ExecuteStoredProcedureAsync_Generic_GivenValidParameters_ShouldReturnOutputValuesAndCloseDataReader()
    {
      //---------------Set up test pack-------------------
      var storedProcName            = "TestStoredProc";
      var storedProcedureParameters = new List<DataAccessParameter>
        {
          new DataAccessParameter("Parameter1", typeof(int), RandomValueGenerator.CreateRandomLong(1, 15)),
          new DataAccessParameter("Parameter2", typeof(DateTime), RandomValueGenerator.CreateRandomDate()),
          new DataAccessParameter("Parameter3", typeof(long), RandomValueGenerator.CreateRandomLong(), DataParameterDirection.Output),
        };

      var sqlParameterProvider    = new SqlParameterProvider();
      var dbDataReader            = Substitute.For<IDataReader>();
      var dataParameterCollection = new TestDataParameterCollection();
      var dbCommand               = Substitute.For<IDbCommand>();
      dbCommand.Parameters.Returns(dataParameterCollection);
      dbCommand.ExecuteReader().Returns(dbDataReader)
               .AndDoes(
                 info =>
                   {
                     dataParameterCollection.Add(sqlParameterProvider.Convert(storedProcedureParameters[2]));
                   });

      var veyronDbConnection = Substitute.For<IVeyronDbConnection>();
      veyronDbConnection.CreateCommand().Returns(dbCommand);

      using (var dbContext = CreateDatabaseContext(veyronDbConnection: veyronDbConnection))
      {
        //---------------Assert Precondition----------------
        //---------------Execute Test ----------------------
        var (_, outputResults) = await dbContext.ExecuteStoredProcedureAsync<TestFakeDataModel>(storedProcName, storedProcedureParameters);
        //---------------Test Result -----------------------
        dbDataReader.Received().Close();
        outputResults.Should().NotBeNull();
        outputResults.Count.Should().Be(1);
        outputResults?.ContainsKey("Parameter3");
      }
    }

    [Test]
    public async Task ExecuteAsync_GivenInputParameters_ShouldCallStoredProcedureWithParameters()
    {
      //---------------Set up test pack-------------------
      var storedProcName          = "TestStoredProc";
      var dbDataReader            = Substitute.For<IDataReader>();
      var dataParameterCollection = Substitute.For<IDataParameterCollection>();
      var dbCommand               = Substitute.For<IDbCommand>();
      dbCommand.ExecuteReader().Returns(dbDataReader);
      dbCommand.Parameters.Returns(dataParameterCollection);

      var veyronDbConnection = Substitute.For<IVeyronDbConnection>();
      veyronDbConnection.CreateCommand().Returns(dbCommand);

      var storedProcedureParameters = new List<DataAccessParameter>
        {
          new DataAccessParameter("Parameter1", typeof(int), RandomValueGenerator.CreateRandomLong(1, 15), DataParameterDirection.Input),
          new DataAccessParameter("Parameter2", typeof(DateTime), RandomValueGenerator.CreateRandomDate(), DataParameterDirection.Input),
        };

      using (var dbContext = CreateDatabaseContext(veyronDbConnection: veyronDbConnection))
      {
        //---------------Assert Precondition----------------
        //---------------Execute Test ----------------------
        await dbContext.ExecuteAsync(storedProcName, storedProcedureParameters);
        //---------------Test Result -----------------------
        dataParameterCollection.Received(storedProcedureParameters.Count).Add(Arg.Any<object>());
      }
    }

    [Test]
    public async Task ExecuteReaderAsync_GivenInputParameters_ShouldCallStoredProcedureWithParameters()
    {
      //---------------Set up test pack-------------------
      var storedProcName          = "TestStoredProc";
      var dbDataReader            = Substitute.For<IDataReader>();
      var dataParameterCollection = Substitute.For<IDataParameterCollection>();
      var dbCommand               = Substitute.For<IDbCommand>();
      dbCommand.ExecuteReader().Returns(dbDataReader);
      dbCommand.Parameters.Returns(dataParameterCollection);

      var veyronDbConnection = Substitute.For<IVeyronDbConnection>();
      veyronDbConnection.CreateCommand().Returns(dbCommand);

      var storedProcedureParameters = new List<DataAccessParameter>
        {
          new DataAccessParameter("Parameter1", typeof(int), RandomValueGenerator.CreateRandomLong(1, 15), DataParameterDirection.Input),
          new DataAccessParameter("Parameter2", typeof(DateTime), RandomValueGenerator.CreateRandomDate(), DataParameterDirection.Input),
        };

      using (var dbContext = CreateDatabaseContext(veyronDbConnection: veyronDbConnection))
      {
        //---------------Assert Precondition----------------
        //---------------Execute Test ----------------------
        await dbContext.ExecuteReaderAsync(storedProcName, storedProcedureParameters);
        //---------------Test Result -----------------------
        dataParameterCollection.Received(storedProcedureParameters.Count).Add(Arg.Any<object>());
      }
    }

    [Test]
    public async Task ExecuteStoredProcedureAsync_GivenInputParameters_ShouldCallStoredProcedureWithParameters()
    {
      //---------------Set up test pack-------------------
      var storedProcName          = "TestStoredProc";
      var dbDataReader            = Substitute.For<IDataReader>();
      var dataParameterCollection = Substitute.For<IDataParameterCollection>();
      var dbCommand               = Substitute.For<IDbCommand>();
      dbCommand.ExecuteReader().Returns(dbDataReader);
      dbCommand.Parameters.Returns(dataParameterCollection);

      var veyronDbConnection = Substitute.For<IVeyronDbConnection>();
      veyronDbConnection.CreateCommand().Returns(dbCommand);

      var storedProcedureParameters = new List<DataAccessParameter>
        {
          new DataAccessParameter("Parameter1", typeof(int), RandomValueGenerator.CreateRandomLong(1, 15), DataParameterDirection.Input),
          new DataAccessParameter("Parameter2", typeof(DateTime), RandomValueGenerator.CreateRandomDate(), DataParameterDirection.Input),
        };

      using (var dbContext = CreateDatabaseContext(veyronDbConnection: veyronDbConnection))
      {
        //---------------Assert Precondition----------------
        //---------------Execute Test ----------------------
        await dbContext.ExecuteStoredProcedureAsync(storedProcName, storedProcedureParameters);
        //---------------Test Result -----------------------
        dataParameterCollection.Received(storedProcedureParameters.Count).Add(Arg.Any<object>());
      }
    }

    [Test]
    public async Task ExecuteStoredProcedureAsync_Generic_GivenInputParameters_ShouldCallStoredProcedureWithParameters()
    {
      //---------------Set up test pack-------------------
      var storedProcName          = "TestStoredProc";
      var dbDataReader            = Substitute.For<IDataReader>();
      var dataParameterCollection = Substitute.For<IDataParameterCollection>();
      var dbCommand               = Substitute.For<IDbCommand>();
      dbCommand.ExecuteReader().Returns(dbDataReader);
      dbCommand.Parameters.Returns(dataParameterCollection);

      var veyronDbConnection = Substitute.For<IVeyronDbConnection>();
      veyronDbConnection.CreateCommand().Returns(dbCommand);

      var storedProcedureParameters = new List<DataAccessParameter>
        {
          new DataAccessParameter("Parameter1", typeof(int), RandomValueGenerator.CreateRandomLong(1, 15), DataParameterDirection.Input),
          new DataAccessParameter("Parameter2", typeof(DateTime), RandomValueGenerator.CreateRandomDate(), DataParameterDirection.Input),
        };

      using (var dbContext = CreateDatabaseContext(veyronDbConnection: veyronDbConnection))
      {
        //---------------Assert Precondition----------------
        //---------------Execute Test ----------------------
        await dbContext.ExecuteStoredProcedureAsync<TestFakeDataModel>(storedProcName, storedProcedureParameters);
        //---------------Test Result -----------------------
        dataParameterCollection.Received(storedProcedureParameters.Count).Add(Arg.Any<object>());
      }
    }

    [Test]
    public async Task ExecuteAsync_GivenInputAndOutputParameters_ShouldReturnOutputParameters()
    {
      //---------------Set up test pack-------------------
      var sqlParameterProvider      = new SqlParameterProvider();
      var storedProcName            = "TestStoredProc";
      var storedProcedureParameters = new List<DataAccessParameter>
        {
          new DataAccessParameter("Parameter1", typeof(int), RandomValueGenerator.CreateRandomLong(1, 15)),
          new DataAccessParameter("Parameter2", typeof(DateTime), RandomValueGenerator.CreateRandomDate()),
          new DataAccessParameter("Parameter3", typeof(long), RandomValueGenerator.CreateRandomLong(), DataParameterDirection.Output),
        };

      var dbDataReader            = Substitute.For<IDataReader>();
      var dataParameterCollection = new TestDataParameterCollection();
      var dbCommand               = Substitute.For<IDbCommand>();
      dbCommand.Parameters.Returns(dataParameterCollection);
      dbCommand.ExecuteReader().Returns(dbDataReader)
               .AndDoes(
                 info =>
                   {
                     dataParameterCollection.Add(sqlParameterProvider.Convert(storedProcedureParameters[2]));
                   });

      var veyronDbConnection = Substitute.For<IVeyronDbConnection>();
      veyronDbConnection.CreateCommand().Returns(dbCommand);

      using (var dbContext = CreateDatabaseContext(veyronDbConnection: veyronDbConnection))
      {
        //---------------Assert Precondition----------------
        //---------------Execute Test ----------------------
        var (_, outputValues) = await dbContext.ExecuteAsync(storedProcName, storedProcedureParameters);
        //---------------Test Result -----------------------
        outputValues.Count.Should().Be(1);
      }
    }

    [Test]
    public async Task ExecuteStoredProcedureAsync_GivenInputAndOutputParameters_ShouldReturnOutputParameters()
    {
      //---------------Set up test pack-------------------
      var sqlParameterProvider      = new SqlParameterProvider();
      var storedProcName            = "TestStoredProc";
      var storedProcedureParameters = new List<DataAccessParameter>
        {
          new DataAccessParameter("Parameter1", typeof(int), RandomValueGenerator.CreateRandomLong(1, 15)),
          new DataAccessParameter("Parameter2", typeof(DateTime), RandomValueGenerator.CreateRandomDate()),
          new DataAccessParameter("Parameter3", typeof(long), RandomValueGenerator.CreateRandomLong(), DataParameterDirection.Output),
        };

      var dbDataReader            = Substitute.For<IDataReader>();
      var dataParameterCollection = new TestDataParameterCollection();
      var dbCommand               = Substitute.For<IDbCommand>();
      dbCommand.Parameters.Returns(dataParameterCollection);
      dbCommand.ExecuteReader().Returns(dbDataReader)
               .AndDoes(
                 info =>
                   {
                     dataParameterCollection.Add(sqlParameterProvider.Convert(storedProcedureParameters[2]));
                   });

      var veyronDbConnection = Substitute.For<IVeyronDbConnection>();
      veyronDbConnection.CreateCommand().Returns(dbCommand);

      using (var dbContext = CreateDatabaseContext(veyronDbConnection: veyronDbConnection))
      {
        //---------------Assert Precondition----------------
        //---------------Execute Test ----------------------
        var outputValues = await dbContext.ExecuteStoredProcedureAsync(storedProcName, storedProcedureParameters);
        //---------------Test Result -----------------------
        outputValues.Count.Should().Be(1);
        outputValues.ContainsKey("Parameter3");
      }
    }

    [Test]
    public async Task ExecuteStoredProcedureAsync_Generic_GivenInputAndOutputParameters_ShouldReturnOutputParameters()
    {
      //---------------Set up test pack-------------------
      var sqlParameterProvider      = new SqlParameterProvider();
      var storedProcName            = "TestStoredProc";
      var storedProcedureParameters = new List<DataAccessParameter>
        {
          new DataAccessParameter("Parameter1", typeof(int), RandomValueGenerator.CreateRandomLong(1, 15)),
          new DataAccessParameter("Parameter2", typeof(DateTime), RandomValueGenerator.CreateRandomDate()),
          new DataAccessParameter("Parameter3", typeof(long), RandomValueGenerator.CreateRandomLong(), DataParameterDirection.Output),
        };

      var dbDataReader            = Substitute.For<IDataReader>();
      var dataParameterCollection = new TestDataParameterCollection();
      var dbCommand               = Substitute.For<IDbCommand>();
      dbCommand.Parameters.Returns(dataParameterCollection);
      dbCommand.ExecuteReader().Returns(dbDataReader)
               .AndDoes(
                 info =>
                   {
                     dataParameterCollection.Add(sqlParameterProvider.Convert(storedProcedureParameters[2]));
                   });

      var veyronDbConnection = Substitute.For<IVeyronDbConnection>();
      veyronDbConnection.CreateCommand().Returns(dbCommand);

      using (var dbContext = CreateDatabaseContext(veyronDbConnection: veyronDbConnection))
      {
        //---------------Assert Precondition----------------
        //---------------Execute Test ----------------------
        var (_, outputValues) = await dbContext.ExecuteStoredProcedureAsync<TestFakeDataModel>(storedProcName, storedProcedureParameters);
        //---------------Test Result -----------------------
        outputValues.Count.Should().Be(1);
        outputValues.ContainsKey("Parameter3");
      }
    }

    [Test]
    public void ExecuteAsync_GivenOutputParameterIsMandatoryAndNoValueSupplied_ShouldThrowException()
    {
      //---------------Set up test pack-------------------
      var sqlParameterProvider      = new SqlParameterProvider();
      var storedProcName            = "TestStoredProc";
      var storedProcedureParameters = new List<DataAccessParameter>
        {
          new DataAccessParameter("Parameter1", typeof(int), RandomValueGenerator.CreateRandomLong(1, 15)),
          new DataAccessParameter("Parameter2", typeof(DateTime), RandomValueGenerator.CreateRandomDate()),
          new DataAccessParameter("Parameter3", typeof(string), parameterDirection: DataParameterDirection.Output, isMandatory: true)
        };

      var dbDataReader            = Substitute.For<IDataReader>();
      var dataParameterCollection = new TestDataParameterCollection();
      var dbCommand               = Substitute.For<IDbCommand>();
      dbCommand.Parameters.Returns(dataParameterCollection);
      dbCommand.ExecuteReader().Returns(dbDataReader)
               .AndDoes(
                 info =>
                   {
                     dataParameterCollection.Add(sqlParameterProvider.Convert(storedProcedureParameters[2]));
                   });

      var veyronDbConnection = Substitute.For<IVeyronDbConnection>();
      veyronDbConnection.CreateCommand().Returns(dbCommand);

      using (var dbContext = CreateDatabaseContext(veyronDbConnection: veyronDbConnection))
      {
        //---------------Assert Precondition----------------
        //---------------Execute Test ----------------------
        var exception = Assert.ThrowsAsync<AggregateException>(() => dbContext.ExecuteAsync(storedProcName, storedProcedureParameters));
        //---------------Test Result -----------------------
        exception.InnerException.Should().NotBeNull();
        exception.InnerException?.Message.Should().Contain($"Mandatory return value expected for {storedProcedureParameters[2].Name} but no value supplied");
      }
    }

    [Test]
    public void ExecuteStoredProcedureAsync_GivenOutputParameterIsMandatoryAndNoValueSupplied_ShouldThrowException()
    {
      //---------------Set up test pack-------------------
      var sqlParameterProvider      = new SqlParameterProvider();
      var storedProcName            = "TestStoredProc";
      var storedProcedureParameters = new List<DataAccessParameter>
        {
          new DataAccessParameter("Parameter1", typeof(int), RandomValueGenerator.CreateRandomLong(1, 15)),
          new DataAccessParameter("Parameter2", typeof(DateTime), RandomValueGenerator.CreateRandomDate()),
          new DataAccessParameter("Parameter3", typeof(string), parameterDirection: DataParameterDirection.Output, isMandatory: true)
        };

      var dbDataReader            = Substitute.For<IDataReader>();
      var dataParameterCollection = new TestDataParameterCollection();
      var dbCommand               = Substitute.For<IDbCommand>();
      dbCommand.Parameters.Returns(dataParameterCollection);
      dbCommand.ExecuteReader().Returns(dbDataReader)
               .AndDoes(
                 info =>
                   {
                     dataParameterCollection.Add(sqlParameterProvider.Convert(storedProcedureParameters[2]));
                   });

      var veyronDbConnection = Substitute.For<IVeyronDbConnection>();
      veyronDbConnection.CreateCommand().Returns(dbCommand);

      using (var dbContext = CreateDatabaseContext(veyronDbConnection: veyronDbConnection))
      {
        //---------------Assert Precondition----------------
        //---------------Execute Test ----------------------
        var exception = Assert.ThrowsAsync<AggregateException>(() => dbContext.ExecuteStoredProcedureAsync(storedProcName, storedProcedureParameters));
        //---------------Test Result -----------------------
        exception.InnerException.Should().NotBeNull();
        exception.InnerException?.Message.Should().Contain($"Mandatory return value expected for {storedProcedureParameters[2].Name} but no value supplied");
      }
    }

    [Test]
    public void ExecuteStoredProcedureAsync_Generic_GivenOutputParameterIsMandatoryAndNoValueSupplied_ShouldThrowException()
    {
      //---------------Set up test pack-------------------
      var sqlParameterProvider      = new SqlParameterProvider();
      var storedProcName            = "TestStoredProc";
      var storedProcedureParameters = new List<DataAccessParameter>
        {
          new DataAccessParameter("Parameter1", typeof(int), RandomValueGenerator.CreateRandomLong(1, 15)),
          new DataAccessParameter("Parameter2", typeof(DateTime), RandomValueGenerator.CreateRandomDate()),
          new DataAccessParameter("Parameter3", typeof(string), parameterDirection: DataParameterDirection.Output, isMandatory: true)
        };

      var dbDataReader            = Substitute.For<IDataReader>();
      var dataParameterCollection = new TestDataParameterCollection();
      var dbCommand               = Substitute.For<IDbCommand>();
      dbCommand.Parameters.Returns(dataParameterCollection);
      dbCommand.ExecuteReader().Returns(dbDataReader)
               .AndDoes(
                 info =>
                   {
                     dataParameterCollection.Add(sqlParameterProvider.Convert(storedProcedureParameters[2]));
                   });

      var veyronDbConnection = Substitute.For<IVeyronDbConnection>();
      veyronDbConnection.CreateCommand().Returns(dbCommand);

      using (var dbContext = CreateDatabaseContext(veyronDbConnection: veyronDbConnection))
      {
        //---------------Assert Precondition----------------
        //---------------Execute Test ----------------------
        var exception = Assert.ThrowsAsync<AggregateException>(() => dbContext.ExecuteStoredProcedureAsync<TestFakeDataModel>(storedProcName, storedProcedureParameters));
        //---------------Test Result -----------------------
        exception.InnerException.Should().NotBeNull();
        exception.InnerException?.Message.Should().Contain($"Mandatory return value expected for {storedProcedureParameters[2].Name} but no value supplied");
      }
    }

    [Test]
    public async Task ExecuteAsync_GivenOutputParameterIsNotMandatoryAndDefaultValueSupplied_ShouldReturnDefaultValueForParameter()
    {
      //---------------Set up test pack-------------------
      var sqlParameterProvider      = new SqlParameterProvider();
      var storedProcName            = "TestStoredProc";
      var storedProcedureParameters = new List<DataAccessParameter>
        {
          new DataAccessParameter("Parameter1", typeof(int), RandomValueGenerator.CreateRandomLong(1, 15)),
          new DataAccessParameter("Parameter2", typeof(DateTime), RandomValueGenerator.CreateRandomDate()),
          new DataAccessParameter("Parameter3", typeof(string), parameterDirection: DataParameterDirection.Output, defaultValue: RandomValueGenerator.CreateRandomString(1, 10))
        };

      var dbDataReader            = Substitute.For<IDataReader>();
      var dataParameterCollection = new TestDataParameterCollection();
      var dbCommand               = Substitute.For<IDbCommand>();
      dbCommand.Parameters.Returns(dataParameterCollection);
      dbCommand.ExecuteReader().Returns(dbDataReader)
               .AndDoes(
                 info =>
                   {
                     dataParameterCollection.Add(sqlParameterProvider.Convert(storedProcedureParameters[2]));
                   });

      var veyronDbConnection = Substitute.For<IVeyronDbConnection>();
      veyronDbConnection.CreateCommand().Returns(dbCommand);

      using (var dbContext = CreateDatabaseContext(veyronDbConnection: veyronDbConnection))
      {
        //---------------Assert Precondition----------------
        //---------------Execute Test ----------------------
        var (_, outputValues) = await dbContext.ExecuteAsync(storedProcName, storedProcedureParameters);
        //---------------Test Result -----------------------
        outputValues.ContainsKey("Parameter3").Should().BeTrue();
        outputValues["Parameter3"].Should().Be(storedProcedureParameters[2].DefaultValue);
      }
    }

    [Test]
    public async Task ExecuteStoredProcedureAsync_GivenOutputParameterIsNotMandatoryAndDefaultValueSupplied_ShouldReturnDefaultValueForParameter()
    {
      //---------------Set up test pack-------------------
      var sqlParameterProvider      = new SqlParameterProvider();
      var storedProcName            = "TestStoredProc";
      var storedProcedureParameters = new List<DataAccessParameter>
        {
          new DataAccessParameter("Parameter1", typeof(int), RandomValueGenerator.CreateRandomLong(1, 15)),
          new DataAccessParameter("Parameter2", typeof(DateTime), RandomValueGenerator.CreateRandomDate()),
          new DataAccessParameter("Parameter3", typeof(string), parameterDirection: DataParameterDirection.Output, defaultValue: RandomValueGenerator.CreateRandomString(1, 10))
        };

      var dbDataReader            = Substitute.For<IDataReader>();
      var dataParameterCollection = new TestDataParameterCollection();
      var dbCommand               = Substitute.For<IDbCommand>();
      dbCommand.Parameters.Returns(dataParameterCollection);
      dbCommand.ExecuteReader().Returns(dbDataReader)
               .AndDoes(
                 info =>
                   {
                     dataParameterCollection.Add(sqlParameterProvider.Convert(storedProcedureParameters[2]));
                   });

      var veyronDbConnection = Substitute.For<IVeyronDbConnection>();
      veyronDbConnection.CreateCommand().Returns(dbCommand);

      using (var dbContext = CreateDatabaseContext(veyronDbConnection: veyronDbConnection))
      {
        //---------------Assert Precondition----------------
        //---------------Execute Test ----------------------
        var outputValues = await dbContext.ExecuteStoredProcedureAsync(storedProcName, storedProcedureParameters);
        //---------------Test Result -----------------------
        outputValues.ContainsKey("Parameter3").Should().BeTrue();
        outputValues["Parameter3"].Should().Be(storedProcedureParameters[2].DefaultValue);
      }
    }

    [Test]
    public async Task ExecuteStoredProcedureAsync_Generic_GivenOutputParameterIsNotMandatoryAndDefaultValueSupplied_ShouldReturnDefaultValueForParameter()
    {
      //---------------Set up test pack-------------------
      var sqlParameterProvider      = new SqlParameterProvider();
      var storedProcName            = "TestStoredProc";
      var storedProcedureParameters = new List<DataAccessParameter>
        {
          new DataAccessParameter("Parameter1", typeof(int), RandomValueGenerator.CreateRandomLong(1, 15)),
          new DataAccessParameter("Parameter2", typeof(DateTime), RandomValueGenerator.CreateRandomDate()),
          new DataAccessParameter("Parameter3", typeof(string), parameterDirection: DataParameterDirection.Output, defaultValue: RandomValueGenerator.CreateRandomString(1, 10))
        };

      var dbDataReader            = Substitute.For<IDataReader>();
      var dataParameterCollection = new TestDataParameterCollection();
      var dbCommand               = Substitute.For<IDbCommand>();
      dbCommand.Parameters.Returns(dataParameterCollection);
      dbCommand.ExecuteReader().Returns(dbDataReader)
               .AndDoes(
                 info =>
                   {
                     dataParameterCollection.Add(sqlParameterProvider.Convert(storedProcedureParameters[2]));
                   });

      var veyronDbConnection = Substitute.For<IVeyronDbConnection>();
      veyronDbConnection.CreateCommand().Returns(dbCommand);

      using (var dbContext = CreateDatabaseContext(veyronDbConnection: veyronDbConnection))
      {
        //---------------Assert Precondition----------------
        //---------------Execute Test ----------------------
        var (_, outputValues) = await dbContext.ExecuteStoredProcedureAsync<TestFakeDataModel>(storedProcName, storedProcedureParameters);
        //---------------Test Result -----------------------
        outputValues.ContainsKey("Parameter3").Should().BeTrue();
        outputValues["Parameter3"].Should().Be(storedProcedureParameters[2].DefaultValue);
      }
    }

    [Test]
    public async Task ExecuteStoredProcedureAsync_Generic_ShouldReturnListOfMappedDataModels()
    {
      //---------------Set up test pack-------------------
      var sqlParameterProvider      = new SqlParameterProvider();
      var storedProcName            = "TestStoredProc";
      var storedProcedureParameters = new List<DataAccessParameter>
        {
          new DataAccessParameter("Parameter1", typeof(int), RandomValueGenerator.CreateRandomLong(1, 15)),
          new DataAccessParameter("Parameter2", typeof(DateTime), RandomValueGenerator.CreateRandomDate()),
          new DataAccessParameter("Parameter3", typeof(string), parameterDirection: DataParameterDirection.Output, defaultValue: RandomValueGenerator.CreateRandomString(1, 10))
        };

      var dataModels = new List<TestFakeDataModel>
        {
          new TestFakeDataModel
            {
              Id = RandomValueGenerator.CreateRandomInt(1, 5), Description = RandomValueGenerator.CreateRandomString(1, 10), ModuleId = RandomValueGenerator.CreateRandomUInt(1, 10)
            },
          new TestFakeDataModel
            {
              Id = RandomValueGenerator.CreateRandomInt(5, 10), Description = RandomValueGenerator.CreateRandomString(1, 10), ModuleId = RandomValueGenerator.CreateRandomUInt(10, 20)
            },
        };

      var dbDataReader = Substitute.For<IDataReader>();
      var readCount = -1;
      dbDataReader.Read().Returns(info =>
        {
          readCount++;
          return readCount < dataModels.Count;
        });
      dbDataReader[Arg.Any<string>()].Returns(info => dataModels[readCount].GetPropertyValue(info.Arg<string>())); 

      var dataParameterCollection = new TestDataParameterCollection();
      var dbCommand               = Substitute.For<IDbCommand>();
      dbCommand.Parameters.Returns(dataParameterCollection);
      dbCommand.ExecuteReader().Returns(dbDataReader)
               .AndDoes(
                 info =>
                   {
                     dataParameterCollection.Add(sqlParameterProvider.Convert(storedProcedureParameters[2]));
                   });

      var veyronDbConnection = Substitute.For<IVeyronDbConnection>();
      veyronDbConnection.CreateCommand().Returns(dbCommand);

      using (var dbContext = CreateDatabaseContext(veyronDbConnection: veyronDbConnection))
      {
        //---------------Assert Precondition----------------
        //---------------Execute Test ----------------------
        var (returnedDataModels, outputValues) = await dbContext.ExecuteStoredProcedureAsync<TestFakeDataModel>(storedProcName, storedProcedureParameters);
        //---------------Test Result -----------------------
        var allDataModels = returnedDataModels as TestFakeDataModel[] ?? returnedDataModels.ToArray();
        allDataModels.Count().Should().Be(dataModels.Count);

        for (int loopCount = 0; loopCount < dataModels.Count; loopCount++)
        {
          allDataModels[loopCount].Id.Should().Be(dataModels[loopCount].Id);
          allDataModels[loopCount].Description.Should().Be(dataModels[loopCount].Description);
          allDataModels[loopCount].ModuleId.Should().Be(dataModels[loopCount].ModuleId);
        }

        outputValues.ContainsKey("Parameter3").Should().BeTrue();
        outputValues["Parameter3"].Should().Be(storedProcedureParameters[2].DefaultValue);
      }
    }

    [Test]
    public void Complete_GivenNoTransaction_ShouldNotThrowException()
    {
      //---------------Set up test pack-------------------
      using (var dbContext = CreateDatabaseContext())
      {
        //---------------Assert Precondition----------------
        dbContext.DbConnection.Transaction.Should().BeNull();
        //---------------Execute Test ----------------------
        Assert.DoesNotThrow(() => dbContext.Complete());
        //---------------Test Result -----------------------
      }
    }

    [Test]
    public void Complete_GivenTransaction_ShouldCommitTransaction()
    {
      //---------------Set up test pack-------------------
      var dbTransaction      = Substitute.For<IDbTransaction>();
      var veyronDbConnection = Substitute.For<IVeyronDbConnection>();
      veyronDbConnection.Transaction.Returns(dbTransaction);

      using (var dbContext = CreateDatabaseContext(veyronDbConnection: veyronDbConnection))
      {
        //---------------Assert Precondition----------------
        //---------------Execute Test ----------------------
        dbContext.Complete();
        //---------------Test Result -----------------------
        dbTransaction.Received(1).Commit();
      }
    }

    private SqlDatabaseContext CreateDatabaseContext(IVeyronDbConnection veyronDbConnection = null, int commandTimeout = 30)
    {
      var dbConnection                  = veyronDbConnection ?? new Mock<IVeyronDbConnection>().Object;
      var dbConnectionParameterProvider = new Mock<IDbConnectionParameterProvider>();
      var connectionString              = "connectionString";

      return new SqlDatabaseContext(connectionString, dbConnection, dbConnectionParameterProvider.Object)
        {
          CommandTimeout = commandTimeout
        };
    }
  }
}
