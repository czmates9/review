using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.VPH
{
    public interface IVPH_Update_Row : IVPH
    {
        int Update_Row(Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHRow Row);
    }
}
