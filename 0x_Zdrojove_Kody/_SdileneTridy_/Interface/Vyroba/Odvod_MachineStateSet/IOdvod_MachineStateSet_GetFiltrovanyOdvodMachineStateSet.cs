using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.Odvod_MachineStateSet
{
    public interface IOdvod_MachineStateSet_GetFiltrovanyMachineStateSets : IOdvod_MachineStateSet
    {
        Fask.Interfaces.DataSets.Vyroba MachineStateSet_GetFiltrovanyOdvodMachineStateSet(Fask.Interfaces.Filtry.Odvod_MachineStateSetListFiltr filtr);

        Fask.Interfaces.DataSets.Vyroba MachineStateSet_GetFiltrovanyOdvodMachineStateSet_Analyza_Odvodu(Fask.Interfaces.Filtry.Odvod_MachineStateSetListFiltr filtr);
    }
}
