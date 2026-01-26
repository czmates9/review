using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fask.POHODA.Disponibility;

namespace Fask.Interfaces.Vyroba.PV
{
    public interface IPV_Navrh_PARAMS_Disponibilita : IPV
    {
        Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVP_DispoDataTable PARAMS_Disponibilita(Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVPDataTable dt);
    }
}
