using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.VPH

{
    public interface IVPH_GetPotrebaMaterialu : IVPH
    {
        Fask.Interfaces.DataSets.Vyroba GetPotrebaMaterialu(Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHDataTable dt_VPH, Fask.Interfaces.Filtry.PotrebaMaterialu_Filtr filtr, string USERID);
    }
}
