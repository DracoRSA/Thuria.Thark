using System.Data;

namespace MGS.Casino.Veyron.DataAccess
{
  /// <summary>
  /// Db Type Map Metadata
  /// </summary>
  public class DbTypeMapMetadata
  {
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="dbType"></param>
    /// <param name="size"></param>
    public DbTypeMapMetadata(DbType dbType, int? size)
    {
      DbType = dbType;
      Size   = size;
    }

    /// <summary>
    /// Db Type
    /// </summary>
    public DbType DbType { get; }

    /// <summary>
    /// Size
    /// </summary>
    public int? Size { get; }
  }
}
