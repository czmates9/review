using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Tisky
{
    public interface ITisky2_MetodaEtiketa_GS1 : ITisky2
    {
        DataSets.Vyroba.CZPRO_VPPDataTable TiskMetodaEtiketa_GS1(ref DataSets.Vyroba.CZPRO_VPPDataTable data);
    }
}

