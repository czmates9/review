using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Interfaces.Vyroba.API
{
    public interface IVyroba_Vaha : IMES
    {

        void Vaha_zapis(Fask.WEBAPI.API_BusinessObjects.FASK_Events FE_data);

        


        ///// <summary>
        ///// Aktualizuje stavy z ADAMa
        ///// </summary>
        ///// <param name="dsvyroba">Databaze MachineStateSet</param>
        //void SetMachineStateSet(MachineStateSet dsmachine);
    }
}
