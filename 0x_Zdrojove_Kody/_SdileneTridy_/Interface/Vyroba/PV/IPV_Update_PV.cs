using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.PV
{
    public interface IPV_Update_PV : IPV
    {

        bool Update(Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVPRow dt);
    }
}
