using Thuria.Thark.Core.Statement.Providers;

namespace Thuria.Thark.Core.Statement.Models
{
  public interface IBaseModel
  {
    IDatabaseProvider DatabaseProvider { get; set; }

    string ToString();
    bool Equals(object compareObject);
    int GetHashCode(object obj);
  }
}