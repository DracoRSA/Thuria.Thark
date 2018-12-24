using System.Collections.Generic;

namespace MGS.Casino.Veyron.DataAccessInterfaces
{
  /// <summary>
  /// Database Context Output Parameters Provider
  /// </summary>
  public interface IDatabaseContextOutputParametersProvider
  {
    /// <summary>
    /// Process the list of parameters
    /// </summary>
    /// <param name="inputParameters">Original Input Parameters</param>
    /// <param name="outputParameters">Output Parameters</param>
    /// <param name="processInParallel">Process In Parallel indicator</param>
    /// <returns>An object representing the processed parameters</returns>
    Dictionary<string, object> ProcessParameters(List<DataAccessParameter> inputParameters, dynamic outputParameters, bool processInParallel = false);
  }
}