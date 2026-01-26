using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.PV
{
    public interface IPV_FillPVH : IPV
    {

        bool FillPVH(Fask.Interfaces.DataSets.Vyroba_Planovani ds);
    }
}
