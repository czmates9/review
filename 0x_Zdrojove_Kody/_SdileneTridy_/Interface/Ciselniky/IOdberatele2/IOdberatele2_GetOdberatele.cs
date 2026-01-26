using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.Odberatele
{
    public interface IOdberatele2_GetOdberatele : IOdberatele2
    {
        /// <summary>
        /// Vraci seznam odberatelu.
        /// </summary>
        /// <returns></returns>
        Fask.Interfaces.DataSets.Odberatele GetOdberatele();
    }
}
