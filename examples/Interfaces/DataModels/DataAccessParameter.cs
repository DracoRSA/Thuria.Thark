using System;

// ReSharper disable once CheckNamespace
namespace MGS.Casino.Veyron.DataAccessInterfaces
{
  /// <summary>
  /// Data Access Parameter Data Model
  /// </summary>
  public class DataAccessParameter
  {
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="parameterName"></param>
    /// <param name="parameterType"></param>
    /// <param name="parameterValue"></param>
    /// <param name="parameterDirection"></param>
    /// <param name="isMandatory"></param>
    /// <param name="defaultValue"></param>
    public DataAccessParameter(string parameterName, Type parameterType, object parameterValue = null, 
                               DataParameterDirection parameterDirection = DataParameterDirection.Input,
                               bool isMandatory = false, object defaultValue = null)
    {
      Name          = parameterName ?? throw new ArgumentNullException(nameof(parameterName));
      ParameterType = parameterType;
      Value         = parameterValue;
      Direction     = parameterDirection;
      IsMandatory   = isMandatory;
      DefaultValue  = defaultValue;
    }

    /// <summary>
    /// Parameter Name
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Parameter Type
    /// </summary>
    public Type ParameterType { get; }

    /// <summary>
    /// Parameter Value
    /// </summary>
    public object Value { get; }

    /// <summary>
    /// Parameter Direction
    /// </summary>
    public DataParameterDirection Direction { get; }

    /// <summary>
    /// Manadatory indicator
    /// </summary>
    public bool IsMandatory { get; }

    /// <summary>
    /// Default Parameter Value
    /// </summary>
    public object DefaultValue { get; }
  }
}
