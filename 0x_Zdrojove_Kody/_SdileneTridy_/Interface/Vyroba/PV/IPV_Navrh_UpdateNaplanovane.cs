using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Interfaces.Vyroba.PV
{
    public interface IPV_Navrh_UpdateNaplanovane : IPV
    {
        void UpdateNaplanovane(Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVPDataTable dt);
    }
}
