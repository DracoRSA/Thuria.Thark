using Thuria.Thark.Core.Statement.Models;
using Thuria.Thark.Core.Statement.Providers;

namespace Thuria.Thark.StatementBuilder.Models
{
  public abstract class BaseModel : IBaseModel
  {
    public IDatabaseProvider DatabaseProvider { get; set; }

    public new abstract bool Equals(object compareObject);

    public int GetHashCode(object obj)
    {
      return ToString().GetHashCode();
    }
  }
}