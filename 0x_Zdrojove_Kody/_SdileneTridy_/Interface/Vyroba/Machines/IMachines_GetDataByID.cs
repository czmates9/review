using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.Machines
{
    public interface IMachines_GetDataByID : IMachines
    {
        Fask.Interfaces.DataSets.Vyroba.MachinesDataTable Machines_GetDataByID(string ID);
    }
}
