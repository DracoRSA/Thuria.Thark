using Thuria.Thark.Core.Statement.Models;
using Thuria.Thark.Core.Statement.Providers;

namespace Thuria.Thark.StatementBuilder.Models
{
  /// <summary>
  /// Base Data Model
  /// </summary>
  public abstract class BaseModel : IBaseModel
  {
    /// <summary>
    /// Database Provider
    /// </summary>
    public IDatabaseProvider DatabaseProvider { get; set; }

    /// <summary>
    /// Equality implementation
    /// </summary>
    /// <param name="compareObject">Object to compare the current object to</param>
    /// <returns>A boolean indicating equality</returns>
    public new abstract bool Equals(object compareObject);

    /// <summary>
    /// Create a Hash code of the given object
    /// </summary>
    /// <param name="obj">Object to Hash</param>
    /// <returns>A Has representation of the given object</returns>
    public int GetHashCode(object obj)
    {
      return ToString().GetHashCode();
    }
  }
}