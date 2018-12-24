using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Dapper;
using MGS.Casino.Veyron.DataAccessInterfaces;

namespace MGS.Casino.Veyron.DataAccess
{
  /// <inheritdoc />
  public class DapperDatabaseContextInputParametersProvider : IDatabaseContextInputParametersProvider
  {
    private readonly object _syncObject = new object();

    /// <inheritdoc />
    public object ProcessParameters(IList<DataAccessParameter> inputParameters, bool processInParallel = false)
    {
      var allInputParameters = new DynamicParameters();
      if (inputParameters != null && inputParameters.Any())
      {
        Parallel.ForEach(inputParameters, 
                         new ParallelOptions { MaxDegreeOfParallelism = processInParallel ? 10 : 1 },
                         currentParameter => ProcessParameter(currentParameter, allInputParameters));
      }

      return allInputParameters;
    }

    private void ProcessParameter(DataAccessParameter inputParameter, DynamicParameters allInputParameters)
    {
      lock(_syncObject)
      { 
        allInputParameters.Add(inputParameter.Name, inputParameter.ParameterType, 
                               parameterValue: inputParameter.Value, parameterDirection: inputParameter.Direction);
      }
    }
  }
}
