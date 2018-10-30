namespace Thuria.Thark.Core.Statement.Models
{
  /// <summary>
  /// Column Data Model
  /// </summary>
  public interface IColumnModel : IBaseModel
  {
    /// <summary>
    /// Table Name
    /// </summary>
    string TableName { get; }

    /// <summary>
    /// Column Name
    /// </summary>
    string ColumnName { get; }

    /// <summary>
    /// Column Alias
    /// </summary>
    string Alias { get; }
  }
}