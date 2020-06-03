using System;
using System.Data;
using System.Linq;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;

using Thuria.Thark.Core.Models;
using Thuria.Thark.Core.Providers;
using Thuria.Thark.Core.DataAccess;
using Thuria.Thark.DataAccess.Models;

namespace Thuria.Thark.DataAccess.Context
{
  /// <summary>
  /// SQL Database Context
  /// </summary>
  public class SqlDatabaseContext : DatabaseContextBase
  {
    private readonly IStatementBuildProvider _sqlStatementBuildProvider;
    private readonly IDataModelPopulateProvider _dataModelPopulateProvider;

    /// <summary>
    /// Sql Database Context
    /// </summary>
    /// <param name="connectionStringProvider">Connection String Provider</param>
    /// <param name="databaseConnectionProvider">Database Connection Provider</param>
    /// <param name="databaseTransactionProvider">Database Transaction Provider</param>
    /// <param name="sqlStatementBuildProvider">SQL Statement Build Provider</param>
    /// <param name="dataModelPopulateProvider">Data Model Populate Provider</param>
    public SqlDatabaseContext(IConnectionStringProvider connectionStringProvider,
                              IDatabaseConnectionProvider databaseConnectionProvider,
                              IDatabaseTransactionProvider databaseTransactionProvider,
                              IStatementBuildProvider sqlStatementBuildProvider,
                              IDataModelPopulateProvider dataModelPopulateProvider)
      : base(connectionStringProvider, databaseConnectionProvider, databaseTransactionProvider)
    {
      _sqlStatementBuildProvider = sqlStatementBuildProvider ?? throw new ArgumentNullException(nameof(sqlStatementBuildProvider));
      _dataModelPopulateProvider = dataModelPopulateProvider ?? throw new ArgumentNullException(nameof(dataModelPopulateProvider));
    }

    /// <inheritdoc />
    public override async Task<IDbContextActionResult<T>> ExecuteActionAsync<T>(DbContextAction dbContextAction,
                                                                                T dataModel = default(T),
                                                                                string sqlCommandText = null,
                                                                                IEnumerable<IDataAccessParameter> dataParameters = null)
    {
      var actionResult = new DbContextActionResult<T>();
      var mapData      = true;

      try
      {
        var dbCommand            = DbConnection.CreateCommand();
        dbCommand.CommandTimeout = CommandTimeout;
        dbCommand.CommandType    = CommandType.Text;

        switch (dbContextAction)
        {
          case DbContextAction.Retrieve:
            dbCommand.CommandText = _sqlStatementBuildProvider.BuildSelectStatement(dataModel);
            break;

          case DbContextAction.Create:
            dbCommand.CommandText = _sqlStatementBuildProvider.BuildInsertStatement(dataModel);
            mapData               = false;
            break;

          case DbContextAction.StoredProcedure:
            dbCommand.CommandType = CommandType.StoredProcedure;
            break;

          default:
            throw new InvalidEnumArgumentException(nameof(dbContextAction), (int) dbContextAction, typeof(DbContextActionResult));
        }

        DbConnection.Open();

        var resultData = new List<T>();
        if (mapData)
        {
          var dataReader = dbCommand.ExecuteReader();
          while (dataReader.Read())
          {
            var rowData = Enumerable.Range(0, dataReader.FieldCount)
                                    .ToDictionary(i => dataReader.GetName(i), i => dataReader.GetValue(i));

            resultData.Add((T) await _dataModelPopulateProvider.PopulateAsync(typeof(T), rowData, dbContextAction));
          }

          dataReader.Close();
        }
        else
        {
          dbCommand.ExecuteNonQuery();
        }

        DbConnection.Close();
        actionResult.SetSuccessResult(resultData);
      }
      catch (Exception runtimeException)
      {
        actionResult.SetExceptionResult(runtimeException);
      }

      return actionResult;
    }
  }
}