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
  /// Insert Statement Builder
  /// </summary>
  public class InsertStatementBuilder : StatementBuilderBase, IInsertStatementBuilder
  {
    private string _insertTableName;
    private IColumnModel _idColumn;
    private readonly Dictionary<IColumnModel, object> _insertColumns = new Dictionary<IColumnModel, object>();

    /// <summary>
    /// Create a new instance of the Insert Statement Builder
    /// </summary>
    // ReSharper disable once UnusedMember.Global
    public static IInsertStatementBuilder Create => new InsertStatementBuilder();

    /// <inheritdoc />
    public new IInsertStatementBuilder WithDatabaseProvider(DatabaseProviderType databaseProvider)
    {
      UpdateDatabaseProvider(databaseProvider);
      return this;
    }

    /// <inheritdoc />
    public IInsertStatementBuilder WithTable(string tableName)
    {
      _insertTableName = tableName;
      return this;
    }

    /// <inheritdoc />
    public IInsertStatementBuilder WithIdColumn(string columnName)
    {
      var columnModel = new ColumnModel(columnName)
        {
          DatabaseProvider = DatabaseProvider
        };
      _idColumn = columnModel;
      return this;
    }

    /// <inheritdoc />
    public IInsertStatementBuilder WithColumn(string columnName, object columnValue)
    {
      var columnModel = new ColumnModel(columnName)
        {
          DatabaseProvider = DatabaseProvider
        };
      _insertColumns.Add(columnModel, columnValue);
      return this;
    }


    /// <inheritdoc />
    public override void Clear()
    {
      _insertTableName = string.Empty;
      _idColumn = null;
      _insertColumns.Clear();
    }

    /// <inheritdoc />
    public override string Build()
    {
      if (!ValidateStatementRequirement())
      {
        throw new StatementBuilderException($"INSERT Statement Validation errors occurred\n{GetErrorMessage()}");
      }

      var insertStatement = new StringBuilder();

      insertStatement.Append($"INSERT INTO {DatabaseProvider.StatementOpenQuote}{_insertTableName}{DatabaseProvider.StatementCloseQuote} (");
      var columnCount = 0;
      foreach (var currentColumn in _insertColumns)
      {
        if (columnCount > 0)
        {
          insertStatement.Append(",");
        }

        insertStatement.Append(currentColumn.Key.ToString());
        columnCount++;
      }

      insertStatement.Append(") ");

      if (_idColumn != null)
      {
        insertStatement.Append($"OUTPUT INSERTED.[{_idColumn.ColumnName}] ");
      }

      insertStatement.Append("VALUES (");

      columnCount = 0;
      foreach (var currentColumn in _insertColumns)
      {
        if (columnCount > 0)
        {
          insertStatement.Append(",");
        }

        var statementQuote = currentColumn.Value.StatementQuote();
        insertStatement.Append($"{statementQuote}{currentColumn.Value.StatementValue()}{statementQuote}");
        columnCount++;
      }

      insertStatement.Append(")");

      Clear();

      return insertStatement.ToString();
    }

    /// <inheritdoc />
    public override void DatabaseProviderChanged()
    {
      foreach (var currentColumn in _insertColumns)
      {
        currentColumn.Key.DatabaseProvider = DatabaseProvider;
      }
    }

    /// <inheritdoc />
    protected override bool ValidateStatementRequirement()
    {
      base.ValidateStatementRequirement();

      if (string.IsNullOrWhiteSpace(_insertTableName))
      {
        Errors.Add("Table Name must be specified to create an INSERT Statement");
      }

      if (!_insertColumns.Any())
      {
        Errors.Add("At least one column must be specified to create an INSERT Statement");
      }

      return !Errors.Any();
    }
  }
}
