using Fask.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Server.Interfaces.Vyroba.API
{
    interface IVyroba_Mer_Zarizeni : IMES
    {

        Fask.Interfaces.DataSets.Vyroba.MachinesDefinitionMeasurementDataTable Konfigurace_Mericich_Zarizeni_00(string IP);

        ///// <summary>
        ///// Aktualizuje stavy z ADAMa
        ///// </summary>
        ///// <param name="dsvyroba">Databaze MachineStateSet</param>
        //void SetMachineStateSet(MachineStateSet dsmachine);
    }
}
