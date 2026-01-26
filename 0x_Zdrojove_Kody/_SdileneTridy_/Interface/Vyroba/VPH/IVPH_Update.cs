using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.VPH
{
    public interface IVPH_Update : IVPH
    {
        int Update(Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHDataTable dt);
    }
}
