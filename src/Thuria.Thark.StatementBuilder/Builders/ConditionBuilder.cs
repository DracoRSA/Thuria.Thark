using System.Text;
using System.Linq;
using System.Collections.Generic;

using Thuria.Thark.Core.Statement;
using Thuria.Thark.Core.Statement.Models;
using Thuria.Thark.StatementBuilder.Models;
using Thuria.Thark.Core.Statement.Builders;
using Thuria.Thark.Core.Statement.Providers;
using Thuria.Thark.StatementBuilder.Providers;

namespace Thuria.Thark.StatementBuilder.Builders
{
  /// <summary>
  /// Condition Builder
  /// </summary>
  public class ConditionBuilder : IConditionBuilder
  {
    private int _conditionNumber;
    private IDatabaseProvider _databaseProvider;
    private readonly IDictionary<int, object> _buildConditions;

    private ConditionBuilder()
    {
      _conditionNumber  = 0;
      _databaseProvider = new SqlServerDatabaseProvider();
      _buildConditions  = new Dictionary<int, object>();
    }

    /// <summary>
    /// Create a Condition Builder
    /// </summary>
    /// <returns>An instance of a Condition Builder</returns>
    public static IConditionBuilder Create => new ConditionBuilder();

    /// <inheritdoc />
    public IConditionBuilder WithDatabaseProvider(DatabaseProviderType databaseProviderType)
    {
      switch (databaseProviderType)
      {
        case DatabaseProviderType.SqlServer:
          _databaseProvider = new SqlServerDatabaseProvider();
          break;

        case DatabaseProviderType.Postgres:
          _databaseProvider = new PostgresDatabaseProvider();
          break;

        case DatabaseProviderType.Firebird:
          _databaseProvider = new FirebirdDatabaseProvider();
          break;
      }

      return this;
    }

    /// <inheritdoc />
    public IConditionBuilder WithCondition(string sourceTable, string sourceColumn, EqualityOperators equalityOperator, object conditionValue)
    {
      var conditionModel = new ConditionModel(sourceTable, sourceColumn, equalityOperator, conditionValue);
      _buildConditions.Add(_conditionNumber++, conditionModel);

      return this;
    }

    /// <inheritdoc />
    public IConditionBuilder WithCondition(string leftConditionTable, string leftConditionColumn, EqualityOperators equalityOperator,
                                           string rightConditionTable, string rightConditionColumn)
    {
      var columnConditionModel = new ColumnConditionModel(leftConditionTable, leftConditionColumn, equalityOperator, rightConditionTable, rightConditionColumn);
      _buildConditions.Add(_conditionNumber++, columnConditionModel);

      return this;
    }

    /// <inheritdoc />
    public IConditionBuilder WithStartSection()
    {
      _buildConditions.Add(_conditionNumber++, " (");
      return this;
    }

    /// <inheritdoc />
    public IConditionBuilder WithEndSection()
    {
      _buildConditions.Add(_conditionNumber++, ") ");
      return this;
    }

    /// <inheritdoc />
    public IConditionBuilder WithAnd()
    {
      _buildConditions.Add(_conditionNumber++, BooleanOperator.And);
      return this;
    }

    /// <inheritdoc />
    public IConditionBuilder WithOr()
    {
      _buildConditions.Add(_conditionNumber++, BooleanOperator.Or);
      return this;
    }

    /// <inheritdoc />
    public string Build()
    {
      var returnCondition = new StringBuilder();

      foreach (var currentCondition in _buildConditions.OrderBy(pair => pair.Key))
      {
        if (currentCondition.Value is IConditionModel)
        {
          var condition              = (IConditionModel)currentCondition.Value;
          condition.DatabaseProvider = _databaseProvider;

          returnCondition.Append($" {condition} ");
        }

        if (currentCondition.Value is string)
        {
          returnCondition.Append($"{(string)currentCondition.Value}");
        }

        if (currentCondition.Value is BooleanOperator)
        {
          returnCondition.Append($"{((BooleanOperator)currentCondition.Value).ToString().ToUpper()}");
        }
      }

      return returnCondition.ToString();
    }
  }
}
