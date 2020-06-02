using System;
using Thuria.Thark.Core.DataAccess;

namespace Thuria.Thark.Core.Models
{
  /// <summary>
  /// Data Access Parameter
  /// </summary>
  public interface IDataAccessParameter
  {
    /// <summary>
    /// Parameter Name
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Parameter Type
    /// </summary>
    Type ParameterType { get; }

    /// <summary>
    /// Parameter Object Type
    /// (Used when ParameterType == Type)
    /// </summary>
    string ObjectType { get; }

    /// <summary>
    /// Parameter Value
    /// </summary>
    object Value { get; }

    /// <summary>
    /// Parameter Direction
    /// </summary>
    DataAccessParameterDirection Direction { get; }

    /// <summary>
    /// Parameter Mandatory indicator
    /// </summary>
    bool IsMandatory { get; }

    /// <summary>
    /// Parameter Default Value
    /// </summary>
    object DefaultValue { get; }
  }
}
