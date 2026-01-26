using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.Odberatele
{
    public interface IOdberatele2_InsertOdberatel : IOdberatele2
    {
        /// <summary>
        /// Vloží do DB nový záznam odberatele.
        /// </summary>
        /// <param name="zdrojRow">Odberatel, který se má vložit.</param>
        /// <returns>True - vše v pořádku, False - chyba</returns>
        bool InsertOdberatel(Fask.Interfaces.DataSets.Odberatele.CZMST090Row odberatelRow);
    }
}
