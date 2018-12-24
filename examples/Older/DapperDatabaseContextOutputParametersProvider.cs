using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Concurrent;
using MGS.Casino.Veyron.DataAccessInterfaces;

namespace MGS.Casino.Veyron.DataAccess
{
  /// <inheritdoc />
  public class DapperDatabaseContextOutputParametersProvider : IDatabaseContextOutputParametersProvider
  {
    /// <inheritdoc />
    public Dictionary<string, object> ProcessParameters(List<DataAccessParameter> inputParameters, dynamic outputParameters, bool processInParallel = false)
    {
      var outputValues = new ConcurrentDictionary<string, object>();

      if (inputParameters != null && inputParameters.Any())
      {
        Parallel.ForEach(inputParameters,
                         new ParallelOptions { MaxDegreeOfParallelism = processInParallel ? 10 : 1 },
                         currentParameter => ProcessParameter(outputValues, currentParameter, outputParameters));
      }

      return outputValues.ToDictionary(kvp => kvp.Key,
                                       kvp => kvp.Value);
    }

    private void ProcessParameter(ConcurrentDictionary<string, object> outputValues, DataAccessParameter inputParameter, dynamic outputParameters)
    {
      if (inputParameter.Direction != DataParameterDirection.Output)
      {
        return;
      }

      var parameterValue = outputParameters.Get<dynamic>(inputParameter.Name);
      if (parameterValue == null && inputParameter.IsMandatory)
      {
        throw new Exception($"Output Parameter {inputParameter.Name} expected to have a value");
      }

      if (parameterValue == null)
      {
        parameterValue = inputParameter.DefaultValue;
      }

      outputValues.TryAdd(inputParameter.Name, parameterValue);
    }
  }
}
