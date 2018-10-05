using System;
using System.Text;

using Thuria.Thark.Core.Statement.Models;

namespace Thuria.Thark.StatementBuilder.Models
{
  public class ColumnModel : BaseModel, IColumnModel
  {
    // ReSharper disable once UnusedMember.Global
    public ColumnModel(string columnName)
      : this(string.Empty, columnName, string.Empty)
    {
    }

    public ColumnModel(string tableName, string columnName)
      : this(tableName, columnName, string.Empty)
    {
    }

    public ColumnModel(string tableName, string columnName, string fieldAlias)
    {
      if (string.IsNullOrWhiteSpace(columnName)) { throw new ArgumentNullException(nameof(columnName)); }

      TableName  = tableName;
      ColumnName = columnName;
      Alias      = fieldAlias;
    }

    public string TableName { get; protected set; }
    public string ColumnName { get; protected set; }
    public string Alias { get; protected set; }

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

    public override bool Equals(object compareObject)
    {
      var otherField = compareObject as ColumnModel;
      if (otherField == null) { return false; }

      return ToString() == otherField.ToString();
    }
  }
}