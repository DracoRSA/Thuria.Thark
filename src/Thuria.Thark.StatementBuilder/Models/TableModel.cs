using System;
using System.Text;

using Thuria.Thark.Core.Statement.Models;

namespace Thuria.Thark.StatementBuilder.Models
{
  /// <summary>
  /// Table Data Model
  /// </summary>
  public class TableModel : BaseModel, ITableModel
  {
    /// <summary>
    /// Table Data Model constructor
    /// </summary>
    /// <param name="tableName">Table Name</param>
    /// <param name="tableAlias">Table Alias</param>
    public TableModel(string tableName, string tableAlias = null)
    {
      if (string.IsNullOrWhiteSpace(tableName)) { throw new ArgumentNullException(nameof(tableName)); }

      TableName = tableName;
      Alias     = tableAlias;
    }

    /// <inheritdoc />
    public string TableName { get; }

    /// <inheritdoc />
    public string Alias { get; }

    /// <summary>
    /// Create a string representation of the model
    /// </summary>
    /// <returns>A string representation of the model</returns>
    public override string ToString()
    {
      if (DatabaseProvider == null) { throw new StatementBuilderException("Database Provider must be specified"); }

      var returnValue = new StringBuilder();
      returnValue.Append($"{DatabaseProvider.StatementOpenQuote}{TableName}{DatabaseProvider.StatementCloseQuote}");

      if (!string.IsNullOrWhiteSpace(Alias))
      {
        returnValue.Append($" AS {DatabaseProvider.StatementOpenQuote}{Alias}{DatabaseProvider.StatementCloseQuote}");
      }

      return returnValue.ToString();
    }

    /// <inheritdoc />
    public override bool Equals(object compareObject)
    {
      var otherField = compareObject as TableModel;
      if (otherField == null) { return false; }

      return ToString() == otherField.ToString();
    }
  }
}