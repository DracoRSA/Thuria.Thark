using System;
using Thuria.Zitidar.Extensions;

using Thuria.Thark.DataModel;
using Thuria.Thark.Core.Providers;
using Thuria.Thark.Core.Statement;
using Thuria.Thark.Core.DataAccess;
using Thuria.Thark.Core.Statement.Builders;
using Thuria.Thark.StatementBuilder.Models;

namespace Thuria.Thark.DataAccess.Providers
{
  /// <summary>
  /// SQL Statement Build Provider
  /// </summary>
  public class SqlStatementBuildProvider : IStatementBuildProvider
  {
    private readonly object _selectBuilderLock = new object();
    private readonly object _insertBuilderLock = new object();
    private readonly object _updateBuilderLock = new object();
    private readonly object _conditionBuilderLock = new object();

    private ISelectStatementBuilder _selectStatementBuilder;
    private IInsertStatementBuilder _insertStatementBuilder;
    private IUpdateStatementBuilder _updateStatementBuilder;
    private IConditionBuilder _conditionBuilder;

    /// <summary>
    /// SQL Statement Build Provider Constructor
    /// </summary>
    /// <param name="selectStatementBuilder">Select Statement Builder</param>
    /// <param name="insertStatementBuilder">Insert Statement Builder</param>
    /// <param name="updateStatementBuilder">Update Statement Builder</param>
    /// <param name="conditionBuilder">Condition Builder</param>
    public SqlStatementBuildProvider(ISelectStatementBuilder selectStatementBuilder,
                                     IInsertStatementBuilder insertStatementBuilder,
                                     IUpdateStatementBuilder updateStatementBuilder,
                                     IConditionBuilder conditionBuilder)
    {
      _selectStatementBuilder = selectStatementBuilder ?? throw new ArgumentNullException(nameof(selectStatementBuilder));
      _insertStatementBuilder = insertStatementBuilder ?? throw new ArgumentNullException(nameof(insertStatementBuilder));
      _updateStatementBuilder = updateStatementBuilder ?? throw new ArgumentNullException(nameof(updateStatementBuilder));
      _conditionBuilder       = conditionBuilder ?? throw new ArgumentNullException(nameof(conditionBuilder));
    }

    /// <inheritdoc />
    public string BuildSelectStatement<T>(T dataModel) where T : class
    {
      if (dataModel == null)
      {
        throw new ArgumentNullException(nameof(dataModel));
      }

      var dataModelTable   = dataModel.GetThuriaDataModelTableName();
      var dataModelColumns = dataModel.GetThuriaDataModelColumns(DbContextAction.Retrieve);
      var whereCondition   = GetWhereConditionsForDataModel(DbContextAction.Retrieve, dataModel);

      string sqlStatement;

      lock (_selectBuilderLock)
      {
        _selectStatementBuilder.Clear();
        _selectStatementBuilder = _selectStatementBuilder.WithTable(dataModelTable);

        foreach (var currentColumn in dataModelColumns)
        {
          var columnModel = new ColumnModel(dataModelTable, currentColumn.ColumnName, currentColumn.Alias);
          _selectStatementBuilder.WithColumn(columnModel);
        }

        if (!string.IsNullOrWhiteSpace(whereCondition))
        {
          _selectStatementBuilder.WithWhereCondition(whereCondition);
        }

        sqlStatement = _selectStatementBuilder.Build();
      }

      return sqlStatement;
    }

    /// <inheritdoc />
    public string BuildInsertStatement<T>(T dataModel) where T : class
    {
      if (dataModel == null)
      {
        throw new ArgumentNullException(nameof(dataModel));
      }

      var dataModelTable   = dataModel.GetThuriaDataModelTableName();
      var dataModelColumns = dataModel.GetThuriaDataModelColumns(DbContextAction.Create);

      string sqlStatement;

      lock (_insertBuilderLock)
      {
        _insertStatementBuilder.Clear();
        _insertStatementBuilder = _insertStatementBuilder.WithTable(dataModelTable);

        foreach (var currentColumn in dataModelColumns)
        {
          var propertyValue = dataModel.GetPropertyValue(currentColumn.PropertyName);
          if (propertyValue == null || propertyValue.Equals(propertyValue.GetType().GetDefaultData()))
          {
            continue;
          }

          _insertStatementBuilder.WithColumn(currentColumn.ColumnName, propertyValue);
        }

        sqlStatement = _insertStatementBuilder.Build();
      }

      return sqlStatement;
    }

    /// <inheritdoc />
    public string BuildUpdateStatement<T>(T dataModel) where T : class
    {
      if (dataModel == null)
      {
        throw new ArgumentNullException(nameof(dataModel));
      }

      var dataModelTable   = dataModel.GetThuriaDataModelTableName();
      var dataModelColumns = dataModel.GetThuriaDataModelColumns(DbContextAction.Update);
      var whereCondition   = GetWhereConditionsForDataModel(DbContextAction.Update, dataModel);

      string sqlStatement;

      lock (_updateBuilderLock)
      {
        _updateStatementBuilder.Clear();
        _updateStatementBuilder = _updateStatementBuilder.WithTable(dataModelTable);

        foreach (var currentColumn in dataModelColumns)
        {
          var propertyValue = dataModel.GetPropertyValue(currentColumn.PropertyName);
          if (propertyValue == null || propertyValue.Equals(propertyValue.GetType().GetDefaultData()))
          {
            continue;
          }

          _updateStatementBuilder.WithColumn(currentColumn.ColumnName, propertyValue);
        }

        if (!string.IsNullOrWhiteSpace(whereCondition))
        {
          _updateStatementBuilder.WithWhereCondition(whereCondition);
        }

        sqlStatement = _updateStatementBuilder.Build();
      }

      return sqlStatement;
    }

    private string GetWhereConditionsForDataModel(DbContextAction contextAction, object dataModel)
    {
      var conditionCount      = 0;
      var tableName           = dataModel.GetThuriaDataModelTableName();
      var dataModelConditions = dataModel.GetThuriaDataModelConditions(contextAction);

      string whereCondition;

      lock (_conditionBuilderLock)
      {
        _conditionBuilder.Clear();
        foreach (var currentCondition in dataModelConditions)
        {
          var propertyValue = currentCondition.Value;
          if (propertyValue == null || propertyValue.Equals(propertyValue.GetType().GetDefaultData()))
          {
            continue;
          }

          if (conditionCount > 0)
          {
            _conditionBuilder = _conditionBuilder.WithAnd();
          }

          _conditionBuilder = _conditionBuilder.WithCondition(tableName, currentCondition.ColumnName, EqualityOperators.Equals, propertyValue);
          conditionCount++;
        }

        whereCondition = _conditionBuilder.Build();
      }

      return whereCondition;
    }
  }
}
