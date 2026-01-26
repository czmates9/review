using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.VMachinesOperations
{
    public interface IVMachinesOperations_GetDataByMachineIDoperationID : IVMachinesOperations
    {
        Fask.Interfaces.DataSets.Vyroba.VMachinesOperationsDataTable VMachinesOperations_GetDataByMachineIDoperationID(string MachinesID, string OperationsID);
    }
}
