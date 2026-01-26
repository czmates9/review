using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Interfaces.Vyroba.PV
{
    public interface IPV_Navrh_GetRow_Parametry : IPV
    {
        Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_PLANOVANI_PARAMSRow GetRow_Parametry(Guid G);
    }
}
