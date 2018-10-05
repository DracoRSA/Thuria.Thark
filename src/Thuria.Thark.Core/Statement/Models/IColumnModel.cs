namespace Thuria.Thark.Core.Statement.Models
{
  public interface IColumnModel : IBaseModel
  {
    string TableName { get; }
    string ColumnName { get; }
    string Alias { get; }
  }
}