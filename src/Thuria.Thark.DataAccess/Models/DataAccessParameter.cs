using System;

using Thuria.Thark.Core.Models;
using Thuria.Thark.Core.DataAccess;

namespace Thuria.Thark.DataAccess.Models
{
  /// <summary>
  /// Data Access Parameter
  /// </summary>
  public class DataAccessParameter : IDataAccessParameter
  {
    /// <summary>
    /// Data Access Parameter constructor
    /// </summary>
    /// <param name="parameterName"></param>
    /// <param name="parameterType"></param>
    /// <param name="parameterValue"></param>
    /// <param name="objectType">Object Type (If ParameterType == Type)</param>
    /// <param name="direction"></param>
    /// <param name="isMandatory"></param>
    /// <param name="defaultValue"></param>
    public DataAccessParameter(string parameterName, 
                               Type parameterType, 
                               object parameterValue = null, 
                               string objectType = null,
                               DataAccessParameterDirection direction = DataAccessParameterDirection.Input, 
                               bool isMandatory = false, 
                               object defaultValue = null)
    {
      Name          = parameterName ?? throw new ArgumentNullException(nameof(parameterName));
      ParameterType = parameterType;
      Value         = parameterValue;
      ObjectType    = objectType;
      Direction     = direction;
      IsMandatory   = isMandatory;
      DefaultValue  = defaultValue;
    }

    /// <inheritdoc />
    public string Name { get; }

    /// <inheritdoc />
    public Type ParameterType { get; }

    /// <inheritdoc />
    public string ObjectType { get; }

    /// <inheritdoc />
    public object Value { get; }

    /// <inheritdoc />
    public DataAccessParameterDirection Direction { get; }

    /// <inheritdoc />
    public bool IsMandatory { get; }

    /// <inheritdoc />
    public object DefaultValue { get; }
  }
}
