using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.Odberatele
{
    public interface IOdberatele2_GetFiltrovaneOdberatele : IOdberatele2
    {
        Fask.Interfaces.DataSets.Odberatele GetFiltrovaneOdberatele(Filtry.AdresarListFiltr filter);
    }
}
