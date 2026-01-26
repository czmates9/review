using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.Odberatele
{
    public interface IOdberatele2_UpdateOdberatel : IOdberatele2
    {
        /// <summary>
        /// Aktualizuje informace o odberateli.
        /// </summary>
        /// <param name="zdrojRow">Odberatel, ktery se bude aktualizovat</param>
        /// <returns></returns>
        bool UpdateOdberatel(Fask.Interfaces.DataSets.Odberatele.CZMST090Row odberatelRow);

    }
}
