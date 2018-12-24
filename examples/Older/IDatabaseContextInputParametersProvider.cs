using System.Collections.Generic;

namespace MGS.Casino.Veyron.DataAccessInterfaces
{
  /// <summary>
  /// Database Context Input Parameters Provider
  /// </summary>
  public interface IDatabaseContextInputParametersProvider
  {
    /// <summary>
    /// Process the list of parameters
    /// </summary>
    /// <param name="inputParameters">Input Parameters</param>
    /// <param name="processInParallel">Process In Parallel indicator</param>
    /// <returns>An object representing the processed parameters</returns>
    dynamic ProcessParameters(IList<DataAccessParameter> inputParameters, bool processInParallel = false);
  }
}