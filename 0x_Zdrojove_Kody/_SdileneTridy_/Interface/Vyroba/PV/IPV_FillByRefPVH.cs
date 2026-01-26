using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.PV
{
    public interface IPV_FillByRefPVH : IPV
    {

        bool FillByRefPVH(Fask.Interfaces.DataSets.Vyroba_Planovani ds, int Ref_PVH);
    }
}
