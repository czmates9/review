using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Interfaces.Vyroba.VPH
{
    public interface IVPH_GetFiltrovanyVPHList : IVPH
    {
        Fask.Interfaces.DataSets.Vyroba GetFiltrovanyVPHList(Fask.Interfaces.Filtry.Vyroba_VPH_Filtr filtr);
    }
}
