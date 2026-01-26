using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.PV
{
    public interface IPV_Edit_QTY_PV : IPV
    {

        bool Edit_QTY(Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVPRow row, 
            decimal? QTY, 
            bool Flag,
            string VP_PRPS_SOPNUMBE
            );
    }
}
