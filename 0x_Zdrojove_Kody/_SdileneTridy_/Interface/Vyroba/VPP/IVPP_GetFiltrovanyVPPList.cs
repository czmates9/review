using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Interfaces.Vyroba.VPP
{
    public interface IVPP_GetFiltrovanyVPPList : IVPP
    {
        Fask.Interfaces.DataSets.Vyroba GetFiltrovanyVPPList(Fask.Interfaces.Filtry.Vyroba_VPP_Filtr filtr);
    }
}
