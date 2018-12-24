using System;
using System.Collections.Generic;
using System.Linq;

using StructureMap;
using StructureMap.Graph;
using StructureMap.Graph.Scanning;

using MGS.Casino.Veyron.DataAccess;
using MGS.Casino.Veyron.DataAccessInterfaces;

namespace MGS.Casino.Veyron.ServiceHost.Conventions
{
  public class DatamapProviderConvention : IRegistrationConvention
  {
    public void ScanTypes(TypeSet types, Registry registry)
    {
      var datamapProvider  = new DapperDatamapProvider();
      var veyronDataModels = from currentType in types.AllTypes().Where(type => !type.IsAbstract)
                             let veyronDataModelInterface = currentType.GetInterfaces().FirstOrDefault(type => type.Name == typeof(IVeyronDataModel).Name)
                             where veyronDataModelInterface != null
                             select currentType;

      foreach (var currentDataModelType in veyronDataModels)
      {
        datamapProvider.CreateDatamap(currentDataModelType);
      }
    }
  }
}
