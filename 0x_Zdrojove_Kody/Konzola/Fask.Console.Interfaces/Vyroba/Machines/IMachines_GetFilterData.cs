using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.Machines
{
    public interface IMachines_GetFilterData : IMachines
    {

        Fask.Interfaces.DataSets.Vyroba Machines_GetFilterData(Fask.Interfaces.Filtry.Odvod_MachineStateSetListFiltr filtr);
    }
}



