using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.PV
{
    public interface IPV_Edit_RefPVH_in_PVP : IPV
    {

        bool Edit_RefPVH_in_PVP(int DEXROWID_PVP, int? RefPVH);
    }
}
