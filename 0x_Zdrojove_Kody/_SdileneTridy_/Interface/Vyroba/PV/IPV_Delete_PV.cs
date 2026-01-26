using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.PV
{
    public interface IPV_Delete_PV : IPV
    {

        bool Delete(Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVPRow dt, decimal JizZaplanovano);
    }
}
