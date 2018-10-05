using System;
using System.Text;

using Thuria.Thark.Core.Statement.Models;

namespace Thuria.Thark.StatementBuilder.Models
{
  public class TableModel : BaseModel, ITableModel
  {
    public TableModel(string tableName, string tableAlias = null)
    {
      if (string.IsNullOrWhiteSpace(tableName)) { throw new ArgumentNullException(nameof(tableName)); }

      TableName = tableName;
      Alias     = tableAlias;
    }

    public string TableName { get; private set; }

    public string Alias { get; private set; }

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

    public override bool Equals(object compareObject)
    {
      var otherField = compareObject as TableModel;
      if (otherField == null) { return false; }

      return ToString() == otherField.ToString();
    }
  }
}