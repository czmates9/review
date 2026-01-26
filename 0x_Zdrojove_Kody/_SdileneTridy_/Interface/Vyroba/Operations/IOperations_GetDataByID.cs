using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.Operations
{
    public interface IOperations_GetDataByID : IOperations
    {
        Fask.Interfaces.DataSets.Vyroba.OperationsDataTable Operations_GetDataByID(string ID);
    }
}
