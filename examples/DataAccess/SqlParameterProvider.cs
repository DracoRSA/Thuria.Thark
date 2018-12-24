using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using MGS.Casino.Veyron.DataAccessInterfaces;

namespace MGS.Casino.Veyron.DataAccess
{
  /// <inheritdoc />
  public class SqlParameterProvider : IDbConnectionParameterProvider
  {
    private readonly Dictionary<Type, Func<string, object, ParameterDirection, SqlParameter>> _parameterProcessor =
      new Dictionary<Type, Func<string, object, ParameterDirection, SqlParameter>>
        {
          {
            typeof(string), (parameterName, parameterValue, parameterDirection) => new SqlParameter(parameterName, SqlDbType.VarChar, -1)
              {
                Value = parameterValue,
                Direction = parameterDirection
              }
          },
          {
            typeof(int), (parameterName, parameterValue, parameterDirection) => new SqlParameter(parameterName, SqlDbType.Int, 4)
              {
                Value = parameterValue,
                Direction = parameterDirection
              }
          },
          {
            typeof(long), (parameterName, parameterValue, parameterDirection) => new SqlParameter(parameterName, SqlDbType.BigInt, 8)
              {
                Value = parameterValue,
                Direction = parameterDirection
              }
          },
          {
            typeof(double), (parameterName, parameterValue, parameterDirection) => new SqlParameter(parameterName, SqlDbType.Float, 8)
              {
                Value = parameterValue,
                Direction = parameterDirection
              }
          },
          {
            typeof(byte), (parameterName, parameterValue, parameterDirection) => new SqlParameter(parameterName, SqlDbType.TinyInt, 1)
              {
                Value = parameterValue,
                Direction = parameterDirection
              }
          },
          {
            typeof(short), (parameterName, parameterValue, parameterDirection) => new SqlParameter(parameterName, SqlDbType.SmallInt, 2)
              {
                Value = parameterValue,
                Direction = parameterDirection
              }
          },
          {
            typeof(byte[]), (parameterName, parameterValue, parameterDirection) => new SqlParameter(parameterName, SqlDbType.VarBinary, -1)
              {
                Value = parameterValue,
                Direction = parameterDirection
              }
          },
          {
            typeof(Guid), (parameterName, parameterValue, parameterDirection) => new SqlParameter(parameterName, SqlDbType.UniqueIdentifier, 16)
              {
                Value = parameterValue,
                Direction = parameterDirection
              }
          },
          {
            typeof(Guid?), (parameterName, parameterValue, parameterDirection) => new SqlParameter(parameterName, SqlDbType.UniqueIdentifier, 16)
              {
                Value = parameterValue,
                Direction = parameterDirection
              }
          },
          {
            typeof(bool), (parameterName, parameterValue, parameterDirection) => new SqlParameter(parameterName, SqlDbType.Bit, 1)
              {
                Value = parameterValue,
                Direction = parameterDirection
              }
          },
          {
            typeof(DateTime), (parameterName, parameterValue, parameterDirection) => new SqlParameter(parameterName, SqlDbType.DateTime)
              {
                Value = parameterValue,
                Direction = parameterDirection
              }
          },
        };

    /// <inheritdoc />
    public IDbDataParameter Convert(DataAccessParameter inputParameter)
    {
      if (!_parameterProcessor.ContainsKey(inputParameter.ParameterType))
      {
        throw new Exception($"No SQL Parameter Processor found for {inputParameter.Name}-{inputParameter.ParameterType}");
      }

      switch (inputParameter.Direction)
      {
        case DataParameterDirection.Input:
          return _parameterProcessor[inputParameter.ParameterType](inputParameter.Name, inputParameter.Value, ParameterDirection.Input);
        case DataParameterDirection.Output:
          return _parameterProcessor[inputParameter.ParameterType](inputParameter.Name, inputParameter.Value, ParameterDirection.Output);
        case DataParameterDirection.InputOutput:
          return _parameterProcessor[inputParameter.ParameterType](inputParameter.Name, inputParameter.Value, ParameterDirection.InputOutput);
        default:
          throw new Exception($"No SQL Parameter Processor found for {inputParameter.Name}-{inputParameter.ParameterType}");
      }
    }
  }
}