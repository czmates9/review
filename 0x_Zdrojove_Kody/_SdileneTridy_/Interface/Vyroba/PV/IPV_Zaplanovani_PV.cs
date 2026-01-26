using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.PV
{
    public interface IPV_Zaplanovani_PV : IPV
    {

        bool Zaplanovani_PV(Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVPRow row, int DexRowID_PVH, decimal JizZaplanovano);
    }
}
