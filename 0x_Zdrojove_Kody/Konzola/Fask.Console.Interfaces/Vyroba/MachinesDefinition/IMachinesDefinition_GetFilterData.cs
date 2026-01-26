using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.MachinesDefinition
{
    public interface IMachinesDefinition_GetFilterData : IMachinesDefinition
    {

        Fask.Interfaces.DataSets.Vyroba MachinesDefinition_GetFilterData(Fask.Interfaces.Filtry.CiselnikModuly_MachinesDefinitionFiltr filtr);
    }
}
