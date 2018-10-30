using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Thuria.Thark.Core.Statement;
using Thuria.Thark.Core.Statement.Models;
using Thuria.Thark.Core.Statement.Builders;
using Thuria.Thark.StatementBuilder.Models;

namespace Thuria.Thark.StatementBuilder.Builders
{
  /// <summary>
  /// Select Statement Builder
  /// </summary>
  public class SelectStatementBuilder : StatementBuilderBase, ISelectStatementBuilder
  {
    private readonly List<ITableModel> _selectTables        = new List<ITableModel>();
    private readonly List<IColumnModel> _selectColumns      = new List<IColumnModel>();
    private readonly List<IConditionModel> _whereConditions = new List<IConditionModel>();
    private readonly List<string> _rawWhereConditions       = new List<string>();

    /// <summary>
    /// Select Statement Builder constructor
    /// </summary>
    protected SelectStatementBuilder()
    {
    }

    /// <summary>
    /// Create a new instance of the SelectStatementBuilder
    /// </summary>
    /// <returns>A new instance of the SelectStatementBuilder</returns>
    // ReSharper disable once UnusedMember.Global
    public static ISelectStatementBuilder Create => new SelectStatementBuilder();

    /// <inheritdoc />
    public new ISelectStatementBuilder WithDatabaseProvider(DatabaseProviderType databaseProvider)
    {
      UpdateDatabaseProvider(databaseProvider);
      return this;
    }

    /// <inheritdoc />
    public ISelectStatementBuilder WithTable(string tableName, string tableAlias = null)
    {
      return WithTable(new TableModel(tableName, tableAlias));
    }

    /// <inheritdoc />
    public ISelectStatementBuilder WithTable(ITableModel tableModel)
    {
      if (_selectTables.Any((model) => model.TableName == tableModel.TableName))
      {
        return this;
      }

      tableModel.DatabaseProvider = DatabaseProvider;
      _selectTables.Add(tableModel);
      return this;
    }

    /// <inheritdoc />
    public ISelectStatementBuilder WithColumn(string statementColumn, string columnAlias = null)
    {
      return WithColumn(new ColumnModel(string.Empty, statementColumn, columnAlias));
    }

    /// <inheritdoc />
    public ISelectStatementBuilder WithColumn(IColumnModel statementColumn)
    {
      if (statementColumn == null) { throw new ArgumentNullException(nameof(statementColumn)); }

      statementColumn.DatabaseProvider = DatabaseProvider;
      _selectColumns.Add(statementColumn);
      return this;
    }

    /// <inheritdoc />
    public ISelectStatementBuilder WithWhereCondition(string sourceTable, string sourceColumnName, EqualityOperators equalityOperator, object conditionValue = null)
    {
      return WithWhereCondition(new ConditionModel(sourceTable, sourceColumnName, equalityOperator, conditionValue));
    }

    /// <inheritdoc />
    public ISelectStatementBuilder WithWhereCondition(IConditionModel whereCondition)
    {
      if (whereCondition == null) { throw new ArgumentNullException(nameof(whereCondition)); }

      whereCondition.DatabaseProvider = DatabaseProvider;
      _whereConditions.Add(whereCondition);
      return this;
    }

    /// <inheritdoc />
    public ISelectStatementBuilder WithWhereCondition(string rawWhereCondition)
    {
      if (string.IsNullOrWhiteSpace(rawWhereCondition)) { throw new ArgumentNullException(nameof(rawWhereCondition)); }

      _rawWhereConditions.Add(rawWhereCondition);
      return this;
    }

    /// <inheritdoc />
    public override string Build()
    {
      if (!ValidateStatementRequirement())
      {
        throw new StatementBuilderException($"Select Statement Validation errors occurred\n{GetErrorMessage()}");
      }

      var selectStatement = new StringBuilder();
      selectStatement.Append("SELECT ");
      selectStatement.Append(AddColumnsToSelectStatement());
      selectStatement.Append(" FROM ");
      selectStatement.Append(AddTableToSelectStatement());
      selectStatement.Append(AddWhereConditionsToSelectStatement());

      return selectStatement.ToString();
    }

    /// <inheritdoc />
    public override void DatabaseProviderChanged()
    {
      foreach (var selectTable in _selectTables)
      {
        selectTable.DatabaseProvider = DatabaseProvider;
      }

      foreach (var currentField in _selectColumns)
      {
        currentField.DatabaseProvider = DatabaseProvider;
      }

      foreach (var whereCondition in _whereConditions)
      {
        whereCondition.DatabaseProvider = DatabaseProvider;
      }
    }

    /// <inheritdoc />
    protected override bool ValidateStatementRequirement()
    {
      base.ValidateStatementRequirement();

      if (!_selectTables.Any()) { Errors.Add("At least one table name must be provided"); }

      return !Errors.Any();
    }

    private string AddColumnsToSelectStatement()
    {

      if (!_selectColumns.Any()) { return "*"; }

      var returnValue = new StringBuilder();
      var columnCount = 0;
      foreach (var currentColumn in _selectColumns)
      {
        if (columnCount > 0)
        {
          returnValue.Append(", ");
        }

        returnValue.Append(currentColumn.ToString());
        columnCount++;
      }

      return returnValue.ToString();
    }

    private string AddTableToSelectStatement()
    {
      var allTables = _selectTables.Select(model => model.ToString()).ToArray();
      return string.Join(",", allTables);
    }

    private string AddWhereConditionsToSelectStatement()
    {
      if (!_whereConditions.Any() && !_rawWhereConditions.Any()) { return string.Empty; }

      var returnValue = new StringBuilder();
      returnValue.Append(" WHERE ");

      var columnCount = 0;
      foreach (var currentCondition in _whereConditions)
      {
        if (columnCount > 0)
        {
          throw new StatementBuilderException("Multiple where clauses not supported yet");
        }

        returnValue.Append(currentCondition.ToString());
        columnCount++;
      }

      var rawConditionCount = 0;
      foreach (var currentCondition in _rawWhereConditions)
      {
        if (rawConditionCount > 0)
        {
          throw new StatementBuilderException("Multiple where clauses not supported yet");
        }

        returnValue.Append(currentCondition);
        rawConditionCount++;
      }

      return returnValue.ToString();
    }
  }
}
