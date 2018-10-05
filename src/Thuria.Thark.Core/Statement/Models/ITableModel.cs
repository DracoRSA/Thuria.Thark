namespace Thuria.Thark.Core.Statement.Models
{
  public interface ITableModel : IBaseModel
  {
    string TableName { get; }
    string Alias { get; }
  }
}