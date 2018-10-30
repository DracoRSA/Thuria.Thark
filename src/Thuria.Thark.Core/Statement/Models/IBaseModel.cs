using Thuria.Thark.Core.Statement.Providers;

namespace Thuria.Thark.Core.Statement.Models
{
  /// <summary>
  /// Base Data Model
  /// </summary>
  public interface IBaseModel
  {
    /// <summary>
    /// Database Provider Type
    /// </summary>
    IDatabaseProvider DatabaseProvider { get; set; }

    /// <summary>
    /// Create a string representation of the model
    /// </summary>
    /// <returns>A string representation of the model</returns>
    string ToString();

    /// <summary>
    /// Compare the current object to an object
    /// </summary>
    /// <param name="compareObject">Object to compare to</param>
    /// <returns>A boolean indicating whether the objects are equal</returns>
    bool Equals(object compareObject);

    /// <summary>
    /// Create a Hash Code of an object
    /// </summary>
    /// <param name="obj">Object to create a hash of</param>
    /// <returns>A hash representation of the object</returns>
    int GetHashCode(object obj);
  }
}