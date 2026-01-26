using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.Pracovnici
{
    public interface IPracovnici2_InsertPracovnici : IPracovnici2
    {
        /// <summary>
        /// Vloží do DB nový záznam Pracovnici.
        /// </summary>
        /// <param name="zdrojRow">Pracovnici, který se má vložit.</param>
        /// <returns>True - vše v pořádku, False - chyba</returns>
        bool InsertPracovnici(Fask.Interfaces.DataSets.Pracovnici.CZMST096Row PracovniciRow);
    }
}
