using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.Zbozi
{
    public interface IZbozi2_InsertZbozi : IZbozi2
    {
        /// <summary>
        /// Vloží do DB nový záznam zbozi.
        /// </summary>
        /// <param name="zdrojRow">Zbozi, které se má vložit.</param>
        /// <returns>True - vše v pořádku, False - chyba</returns>
        bool InsertZbozi(Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_KONZOLARow zboziRow);
    }
}
