
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Interfaces.Vyroba.API
{
    public interface IVyroba_GetMachineStateSetEventsRow : IMES
    {
        Fask.WEBAPI.API_BusinessObjects.MachineStateSet GetMachineStateSetEventsRow(Fask.WEBAPI.API_BusinessObjects.Filtr_MachineStateSet filtr);
    }
}
