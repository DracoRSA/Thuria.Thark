using System;
using System.Text;

using Thuria.Thark.Core.Statement.Models;

namespace Thuria.Thark.StatementBuilder.Models
{
  /// <summary>
  /// Column Data Model
  /// </summary>
  public class ColumnModel : BaseModel, IColumnModel
  {
    /// <summary>
    /// Column Data Model constructor
    /// </summary>
    /// <param name="columnName">Column Name</param>
    // ReSharper disable once UnusedMember.Global
    public ColumnModel(string columnName)
      : this(string.Empty, columnName, string.Empty)
    {
    }

    /// <summary>
    /// Column Data Model constructor
    /// </summary>
    /// <param name="tableName">Table Name</param>
    /// <param name="columnName">Column Name</param>
    public ColumnModel(string tableName, string columnName)
      : this(tableName, columnName, string.Empty)
    {
    }

    /// <summary>
    /// Column Data Model constructor
    /// </summary>
    /// <param name="tableName">Table Name</param>
    /// <param name="columnName">Column Name</param>
    /// <param name="fieldAlias">Field Alias</param>
    public ColumnModel(string tableName, string columnName, string fieldAlias)
    {
      if (string.IsNullOrWhiteSpace(columnName)) { throw new ArgumentNullException(nameof(columnName)); }

      TableName  = tableName;
      ColumnName = columnName;
      Alias      = fieldAlias;
    }

    /// <inheritdoc />
    public string TableName { get; protected set; }

    /// <inheritdoc />
    public string ColumnName { get; protected set; }

    /// <inheritdoc />
    public string Alias { get; protected set; }

    /// <summary>
    /// Create a string representation of the Column
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
      var returnValue = new StringBuilder();

      if (!string.IsNullOrWhiteSpace(TableName))
      {
        returnValue.Append($"{DatabaseProvider.StatementOpenQuote}{TableName}{DatabaseProvider.StatementCloseQuote}.");
      }

      returnValue.Append($"{DatabaseProvider.StatementOpenQuote}{ColumnName}{DatabaseProvider.StatementCloseQuote}");

      if (!string.IsNullOrWhiteSpace(Alias))
      {
        returnValue.Append($" AS {DatabaseProvider.StatementOpenQuote}{Alias}{DatabaseProvider.StatementCloseQuote}");
      }

      return returnValue.ToString();
    }

    /// <inheritdoc />
    public override bool Equals(object compareObject)
    {
      var otherField = compareObject as ColumnModel;
      if (otherField == null) { return false; }

      return ToString() == otherField.ToString();
    }
  }
}