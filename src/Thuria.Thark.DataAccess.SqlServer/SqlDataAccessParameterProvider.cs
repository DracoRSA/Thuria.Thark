using System;
using System.Data;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Collections.Generic;

using Thuria.Thark.Core.Models;
using Thuria.Thark.Core.Providers;
using Thuria.Thark.Core.DataAccess;

namespace Thuria.Thark.DataAccess.SqlServer
{
  /// <summary>
  /// Sql Data Access Parameter Provider
  /// </summary>
  public class SqlDataAccessParameterProvider : IDataAccessParameterProvider
  {
    private readonly Dictionary<Type, Func<string, object, ParameterDirection, SqlParameter>> _parameterProcessors
      = new Dictionary<Type, Func<string, object, ParameterDirection, SqlParameter>>
          {
            {
              typeof(string),
              (parameterName, parameterValue, parameterDirection) => new SqlParameter(parameterName, SqlDbType.VarChar, -1)
                                                                       {
                                                                         Value     = parameterValue,
                                                                         Direction = parameterDirection
                                                                       }
            },
            {
              typeof(int),
              (parameterName, parameterValue, parameterDirection) => new SqlParameter(parameterName, SqlDbType.Int, 4)
                                                                       {
                                                                         Value     = parameterValue,
                                                                         Direction = parameterDirection
                                                                       }
            },
            {
              typeof(long),
              (parameterName, parameterValue, parameterDirection) => new SqlParameter(parameterName, SqlDbType.BigInt, 8)
                                                                       {
                                                                         Value     = parameterValue,
                                                                         Direction = parameterDirection
                                                                       }
            },
            {
              typeof(double),
              (parameterName, parameterValue, parameterDirection) => new SqlParameter(parameterName, SqlDbType.Float, 8)
                                                                       {
                                                                         Value     = parameterValue,
                                                                         Direction = parameterDirection
                                                                       }
            },
            {
              typeof(byte),
              (parameterName, parameterValue, parameterDirection) => new SqlParameter(parameterName, SqlDbType.TinyInt, 1)
                                                                       {
                                                                         Value = parameterValue,
                                                                         Direction = parameterDirection
                                                                       }
            },
            {
              typeof(short),
              (parameterName, parameterValue, parameterDirection) => new SqlParameter(parameterName, SqlDbType.SmallInt, 2)
                                                                       {
                                                                         Value     = parameterValue,
                                                                         Direction = parameterDirection
                                                                       }
            },
            {
              typeof(byte[]),
              (parameterName, parameterValue, parameterDirection) => new SqlParameter(parameterName, SqlDbType.VarBinary, -1)
                                                                       {
                                                                         Value     = parameterValue,
                                                                         Direction = parameterDirection
                                                                       }
            },
            {
              typeof(Guid),
              (parameterName, parameterValue, parameterDirection) => new SqlParameter(parameterName, SqlDbType.UniqueIdentifier, 16)
                                                                       {
                                                                         Value     = parameterValue,
                                                                         Direction = parameterDirection
                                                                       }
            },
            {
              typeof(Guid?),
              (parameterName, parameterValue, parameterDirection) => new SqlParameter(parameterName, SqlDbType.UniqueIdentifier, 16)
                                                                       {
                                                                         Value     = parameterValue,
                                                                         Direction = parameterDirection
                                                                       }
            },
            {
              typeof(bool),
              (parameterName, parameterValue, parameterDirection) => new SqlParameter(parameterName, SqlDbType.Bit, 1)
                                                                       {
                                                                         Value = parameterValue,
                                                                         Direction = parameterDirection
                                                                       }
            },
            {
              typeof(DateTime),
              (parameterName, parameterValue, parameterDirection) => new SqlParameter(parameterName, SqlDbType.DateTime)
                                                                       {
                                                                         Value     = parameterValue,
                                                                         Direction = parameterDirection
                                                                       }
            },
          };

    /// <inheritdoc />
    public IDbDataParameter Convert(IDataAccessParameter inputParameter)
    {
      if (inputParameter == null) { throw new ArgumentNullException(nameof(inputParameter)); }

      if (!_parameterProcessors.ContainsKey(inputParameter.ParameterType) && inputParameter.ParameterType != typeof(Type))
      {
        throw new ArgumentException($"No Sql Parameter Processor found for {inputParameter.Name}-{inputParameter.ParameterType.Name}");
      }

      switch (inputParameter.Direction)
      {
        case DataAccessParameterDirection.Input:
          if (inputParameter.ParameterType == typeof(Type))
          {
            return new SqlParameter(inputParameter.Name, SqlDbType.Structured)
                     {
                       TypeName  = inputParameter.ObjectType,
                       Value     = inputParameter.Value,
                       Direction = ParameterDirection.Input
                     };
          }
          else
          {
            return _parameterProcessors[inputParameter.ParameterType](inputParameter.Name, inputParameter.Value, ParameterDirection.Input);
          }

        default:
          throw new InvalidEnumArgumentException(nameof(inputParameter.Direction), (int)inputParameter.Direction, typeof(DataAccessParameterDirection));
      }
    }
  }
}
