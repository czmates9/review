using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Interfaces.Vyroba.API
{
    public interface IVyroba_PostMSSEventRow : IMES
    {
        Fask.WEBAPI.API_BusinessObjects.MachineStateSet PostMachineStateSetEventsRow(Fask.WEBAPI.API_BusinessObjects.MachineStateSet MSS_objekt);

        /// <summary>
        /// Aktualizuje stavy z ADAMa
        /// </summary>
        /// <param name="dsvyroba">Databaze MachineStateSet</param>
        void SetMachineStateSet(Fask.WEBAPI.API_BusinessObjects.MachineStateSet dsmachine);
    }
}
