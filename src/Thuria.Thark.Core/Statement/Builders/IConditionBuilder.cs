namespace Thuria.Thark.Core.Statement.Builders
{
  /// <summary>
  /// Condition Builder
  /// </summary>
  public interface IConditionBuilder
  {
    /// <summary>
    /// Specify the Database Provider to use
    /// </summary>
    /// <param name="databaseProviderType">Database Provider Type</param>
    /// <returns>An instance of the Condition Builder</returns>
    IConditionBuilder WithDatabaseProvider(DatabaseProviderType databaseProviderType);

    /// <summary>
    /// Specify a Condition to use
    /// </summary>
    /// <param name="sourceTable">Source Table</param>
    /// <param name="sourceColumn">Source Column</param>
    /// <param name="equalityOperator">Equality operator</param>
    /// <param name="conditionValue">Condition Value</param>
    /// <returns>An instance of the Condition Builder</returns>
    IConditionBuilder WithCondition(string sourceTable, string sourceColumn, EqualityOperators equalityOperator, object conditionValue);

    /// <summary>
    /// Specify a Condition to use
    /// </summary>
    /// <param name="leftConditionTable">Left Condition Table Name</param>
    /// <param name="leftConditionColumn">Left Condition Column Name</param>
    /// <param name="equalityOperator">Equality operator</param>
    /// <param name="rightConditionTable">Right Condition Table Name</param>
    /// <param name="rightConditionColumn">Right Condition Column Name</param>
    /// <returns>An instance of the Condition Builder</returns>
    IConditionBuilder WithCondition(string leftConditionTable, string leftConditionColumn, EqualityOperators equalityOperator, string rightConditionTable, string rightConditionColumn);

    /// <summary>
    /// Specify a Start Section to use
    /// </summary>
    /// <returns>An instance of the Condition Builder</returns>
    IConditionBuilder WithStartSection();

    /// <summary>
    /// Specify an End Section to use
    /// </summary>
    /// <returns>An instance of the Condition Builder</returns>
    IConditionBuilder WithEndSection();

    /// <summary>
    /// Specify an And condition
    /// </summary>
    /// <returns>An instance of the Condition Builder</returns>
    IConditionBuilder WithAnd();

    /// <summary>
    /// Specify an Or condition
    /// </summary>
    /// <returns>An instance of the Condition Builder</returns>
    IConditionBuilder WithOr();

    /// <summary>
    /// Clear the Builder in preparation for building a new Condition
    /// </summary>
    void Clear();

    /// <summary>
    /// Build the required Condition
    /// </summary>
    /// <returns>A string representing the condition</returns>
    string Build();
  }
}
