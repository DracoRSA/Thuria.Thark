using System;
using System.Reflection;
using System.Collections.Generic;
using Dapper;
using MGS.Casino.Veyron.DataAccessInterfaces;
using MGS.Casino.Veyron.DataAccessInterfaces.Metadata;

namespace MGS.Casino.Veyron.DataAccess
{
  /// A Dapper mapping provider implementation of the <see cref="IDatamapProvider"/>.
  public class DapperDatamapProvider : IDatamapProvider
  {
    /// <inheritdoc />
    public void CreateDatamap(Type dataModelType)
    {
      var dbColumnMap   = new Dictionary<string, string>();
      var allProperties = dataModelType.GetProperties();

      foreach (var currentProperty in allProperties)
      {
        var dbColumnAttribute = currentProperty.GetCustomAttribute<VeyronDbColumnAttribute>();
        if (dbColumnAttribute == null)
        {
          continue;
        }

        dbColumnMap.Add(dbColumnAttribute.DbColumnName, currentProperty.Name);
      }

      var columnMapper = new Func<Type, string, PropertyInfo>
        (
          (type, columnName) => type.GetProperty(dbColumnMap.ContainsKey(columnName) ? dbColumnMap[columnName] : columnName)
        );
      var columnTypeMap = new CustomPropertyTypeMap(dataModelType, (type, columnName) => columnMapper(type, columnName));

      SqlMapper.SetTypeMap(dataModelType, columnTypeMap);
    }
  }
}
