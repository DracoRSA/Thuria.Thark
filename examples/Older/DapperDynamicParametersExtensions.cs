using System;
using System.Data;
using System.Collections.Generic;
using Dapper;
using MGS.Casino.Veyron.DataAccessInterfaces;

namespace MGS.Casino.Veyron.DataAccess
{
  /// <summary>
  /// Dapper Dynamic Parameters extension methods
  /// </summary>
  public static class DapperDynamicParametersExtensions
  {
    private static readonly Dictionary<Type, DbTypeMapMetadata> DbTypeMap = new Dictionary<Type, DbTypeMapMetadata>
    {
      { typeof (byte), new DbTypeMapMetadata(DbType.Byte, null) },
      { typeof (sbyte), new DbTypeMapMetadata(DbType.Byte, null) },
      { typeof (short), new DbTypeMapMetadata(DbType.Int16, 2) },
      { typeof (ushort), new DbTypeMapMetadata(DbType.Int16, 2) },
      { typeof (int), new DbTypeMapMetadata(DbType.Int32, null) },
      { typeof (uint), new DbTypeMapMetadata(DbType.Int32, null) },
      { typeof (long), new DbTypeMapMetadata(DbType.Int64, null) },
      { typeof (ulong), new DbTypeMapMetadata(DbType.Int64, null) },
      { typeof (float), new DbTypeMapMetadata(DbType.Double, null) },
      { typeof (double), new DbTypeMapMetadata(DbType.Double, null) },
      { typeof (decimal), new DbTypeMapMetadata(DbType.Decimal, null) },
      { typeof (bool), new DbTypeMapMetadata(DbType.Boolean, null) },
      { typeof (string), new DbTypeMapMetadata(DbType.String, 8192) },
      { typeof (char), new DbTypeMapMetadata(DbType.AnsiStringFixedLength, null) },
      { typeof (Guid), new DbTypeMapMetadata(DbType.Guid, null) },
      { typeof (DateTime), new DbTypeMapMetadata(DbType.DateTime, null) },
      { typeof (DateTimeOffset), new DbTypeMapMetadata(DbType.DateTimeOffset, null) },
      { typeof (byte[]), new DbTypeMapMetadata(DbType.Binary, -1) },
      { typeof (byte?), new DbTypeMapMetadata(DbType.Byte, null) },
      { typeof (sbyte?), new DbTypeMapMetadata(DbType.Byte, null) },
      { typeof (short?), new DbTypeMapMetadata(DbType.Int16, 2) },
      { typeof (ushort?), new DbTypeMapMetadata(DbType.Int16, 2) },
      { typeof (int?), new DbTypeMapMetadata(DbType.Int32, null) },
      { typeof (uint?), new DbTypeMapMetadata(DbType.Int32, null) },
      { typeof (long?), new DbTypeMapMetadata(DbType.Int64, null) },
      { typeof (ulong?), new DbTypeMapMetadata(DbType.Int64, null) },
      { typeof (float?), new DbTypeMapMetadata(DbType.Double, null) },
      { typeof (double?), new DbTypeMapMetadata(DbType.Double, null) },
      { typeof (decimal?), new DbTypeMapMetadata(DbType.Decimal, null) },
      { typeof (bool?), new DbTypeMapMetadata(DbType.Boolean, null) },
      { typeof (char?), new DbTypeMapMetadata(DbType.AnsiStringFixedLength, null) },
      { typeof (Guid?), new DbTypeMapMetadata(DbType.Guid, null) },
      { typeof (DateTime?), new DbTypeMapMetadata(DbType.DateTime, null) },
      { typeof (DateTimeOffset?), new DbTypeMapMetadata(DbType.DateTimeOffset, null) }
    };

    /// <summary>
    /// Add Dynamic Parameter
    /// </summary>
    /// <param name="dynamicParameter">Dynamic Parameter</param>
    /// <param name="parameterName">Parameter Name</param>
    /// <param name="parameterType">Parameter Type</param>
    /// <param name="parameterValue">Parameter Value (Optional / Default value = null)</param>
    /// <param name="parameterDirection">Parameter Direction (Optional / Default value = Input)</param>
    /// <param name="parameterSize">Size of Parameter (Optional / Default depends on data type)</param>
    public static void Add(this DynamicParameters dynamicParameter, string parameterName, Type parameterType, object parameterValue = null, 
                           DataParameterDirection parameterDirection = DataParameterDirection.Input, int? parameterSize = null)
    {
      if (dynamicParameter == null)
      {
        throw new Exception("Dynamic Parameter is null");
      }

      var convertedValue = parameterValue;
      if (parameterValue != null && parameterType == typeof(uint))
      {
        convertedValue = Convert.ToInt32(parameterValue);
      }

      var convertedDirection = ParameterDirection.Input;
      switch (parameterDirection)
      {
        case DataParameterDirection.Input:
          convertedDirection = ParameterDirection.Input;
          break;

        case DataParameterDirection.Output:
          convertedDirection = ParameterDirection.Output;
          break;

        case DataParameterDirection.InputOutput:
          convertedDirection = ParameterDirection.InputOutput;
          break;
      }

      dynamicParameter.Add(parameterName, convertedValue, dbType: DbTypeMap[parameterType].DbType, 
                           direction: convertedDirection, size: parameterSize ?? DbTypeMap[parameterType].Size);
    }
  }
}
