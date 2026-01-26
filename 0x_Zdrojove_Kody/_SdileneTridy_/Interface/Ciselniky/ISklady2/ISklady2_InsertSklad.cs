using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.Sklady
{
    public interface ISklady2_InsertSklad : ISklady2
    {
        /// <summary>
        /// Vloží do DB nový záznam skladu.
        /// </summary>
        /// <param name="zdrojRow">Sklad, které se má vložit.</param>
        /// <returns>True - vše v pořádku, False - chyba</returns>
        bool InsertSklad(Fask.Interfaces.DataSets.Sklady.CZMST093Row skladRow);
    }
}
