using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.Sklady
{
    public interface ISklady2_UpdateSklad : ISklady2
    {
        /// <summary>
        /// Aktualizuje informace o skladu.
        /// </summary>
        /// <param name="zdrojRow">Sklad, ktery se bude aktualizovat</param>
        /// <returns></returns>
        bool UpdateSklad(Fask.Interfaces.DataSets.Sklady.CZMST093Row skladRow);

    }
}
