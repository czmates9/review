using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Interfaces.Vyroba.API
{
   
    public interface IVyrobaPrint : IMES
    {

        Fask.WEBAPI.API_BusinessObjects.FASK_Events Vyroba_data_faskevents(Fask.WEBAPI.API_BusinessObjects.Filtr_FASK_Events filtr, string ID);

        Fask.WEBAPI.API_BusinessObjects.FASK_Events Vyroba_data_faskevents(string ID);



        ///// <summary>
        ///// Aktualizuje stavy z ADAMa
        ///// </summary>
        ///// <param name="dsvyroba">Databaze MachineStateSet</param>
        //void SetMachineStateSet(MachineStateSet dsmachine);
    }
}
