using System;

namespace MGS.Casino.Veyron.DataAccessInterfaces
{
  /// <summary>
  /// Data mapping provider interface
  /// </summary>
  public interface IDatamapProvider
  {
    /// <summary>
    /// Create a Data Map for the given Data Model
    /// </summary>
    /// <param name="dataModelType"></param>
    void CreateDatamap(Type dataModelType);
  }
}
