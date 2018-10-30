namespace Thuria.Thark.Core.Statement.Models
{
  /// <summary>
  /// Table Data Model
  /// </summary>
  public interface ITableModel : IBaseModel
  {
    /// <summary>
    /// Table Name
    /// </summary>
    string TableName { get; }

    /// <summary>
    /// Table Alias
    /// </summary>
    string Alias { get; }
  }
}