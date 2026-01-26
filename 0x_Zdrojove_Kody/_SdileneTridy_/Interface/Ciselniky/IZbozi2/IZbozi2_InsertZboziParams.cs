using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.Zbozi
{
    public interface IZbozi2_InsertZboziParams : IZbozi2
    {
        /// <summary>
        /// Vloží do DB nový záznam zbozi.
        /// </summary>
        /// <param name="zdrojRow">Zbozi, které se má vložit.</param>
        /// <returns>True - vše v pořádku, False - chyba</returns>
        bool InsertZboziParams(Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_PARAMETRY_KONZOLARow zboziRow);
    }
}
