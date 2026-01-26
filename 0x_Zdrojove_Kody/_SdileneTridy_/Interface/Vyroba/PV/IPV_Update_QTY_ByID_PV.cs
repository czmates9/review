using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.PV
{
    public interface IPV_Update_QTY_ByID_PV : IPV
    {

        bool Update_QTY_ByID_PV(int ID, decimal? VP_PRPS_QTY, bool VP_PRPS, string VP_PRPS_SOPNUMBE);
    }
}
