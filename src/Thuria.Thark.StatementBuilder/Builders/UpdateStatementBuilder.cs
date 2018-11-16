using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

using Thuria.Thark.Core.Statement;
using Thuria.Thark.Core.Statement.Models;
using Thuria.Thark.Core.Statement.Builders;
using Thuria.Thark.StatementBuilder.Models;

namespace Thuria.Thark.StatementBuilder.Builders
{
  /// <summary>
  /// Update Statement Builder
  /// </summary>
  public class UpdateStatementBuilder : StatementBuilderBase, IUpdateStatementBuilder
  {
    private string _updateTableName;
    private readonly Dictionary<IColumnModel, object> _updateColumns = new Dictionary<IColumnModel, object>();
    private readonly List<IConditionModel> _whereConditions          = new List<IConditionModel>();
    private readonly List<string> _rawWhereConditions                = new List<string>();

    /// <summary>
    /// Create a new instance of the UpdateStatementBuilder
    /// </summary>
    /// <returns>A new instance of the UpdateStatementBuilder</returns>
    public static IUpdateStatementBuilder Create => new UpdateStatementBuilder();

    /// <inheritdoc />
    public new IUpdateStatementBuilder WithDatabaseProvider(DatabaseProviderType databaseProvider)
    {
      UpdateDatabaseProvider(databaseProvider);
      return this;
    }

    /// <inheritdoc />
    public IUpdateStatementBuilder WithTable(string tableName)
    {
      _updateTableName = tableName;
      return this;
    }

    /// <inheritdoc />
    public IUpdateStatementBuilder WithColumn(string columnName, object columnValue)
    {
      var columnModel = new ColumnModel(columnName)
        {
          DatabaseProvider = DatabaseProvider
        };
      _updateColumns.Add(columnModel, columnValue);
      return this;
    }

    /// <inheritdoc />
    public IUpdateStatementBuilder WithWhereCondition(string sourceTable, string sourceColumnName, EqualityOperators equalityOperator, object conditionValue = null)
    {
      return WithWhereCondition(new ConditionModel(sourceTable, sourceColumnName, equalityOperator, conditionValue));
    }

    /// <inheritdoc />
    public IUpdateStatementBuilder WithWhereCondition(IConditionModel whereCondition)
    {
      if (whereCondition == null) { throw new ArgumentNullException(nameof(whereCondition)); }

      whereCondition.DatabaseProvider = DatabaseProvider;
      _whereConditions.Add(whereCondition);
      return this;
    }

    /// <inheritdoc />
    public IUpdateStatementBuilder WithWhereCondition(string rawWhereCondition)
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
        throw new StatementBuilderException($"UPDATE Statement Validation errors occurred\n{GetErrorMessage()}");
      }

      var updateStatement = new StringBuilder();

      updateStatement.Append($"UPDATE {DatabaseProvider.StatementOpenQuote}{_updateTableName}{DatabaseProvider.StatementCloseQuote} SET ");
      var columnCount = 0;
      foreach (var currentColumn in _updateColumns)
      {
        if (columnCount > 0)
        {
          updateStatement.Append(",");
        }

        var statementQuote = currentColumn.Value.StatementQuote();

        updateStatement.Append($"{currentColumn.Key.ToString()} = {statementQuote}{currentColumn.Value.StatementValue()}{statementQuote}");
        columnCount++;
      }

      updateStatement.Append(AddWhereConditionsToSelectStatement());

      Clear();

      return updateStatement.ToString();
    }

    /// <inheritdoc />
    public override void DatabaseProviderChanged()
    {
      foreach (var currentField in _updateColumns)
      {
        currentField.Key.DatabaseProvider = DatabaseProvider;
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

      if (string.IsNullOrWhiteSpace(_updateTableName))
      {
        Errors.Add("Table Name must be specified to create an UPDATE Statement");
      }

      if (!_updateColumns.Any())
      {
        Errors.Add("At least one column must be specified to create an UPDATE Statement");
      }

      return !Errors.Any();
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

    private void Clear()
    {
      _updateTableName = string.Empty;
      _updateColumns.Clear();
      _whereConditions.Clear();
      _rawWhereConditions.Clear();
    }
  }
}
