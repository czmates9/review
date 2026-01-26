using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.Strediska
{
    public interface IStrediska2_InsertStredisko : IStrediska2
    {
        /// <summary>
        /// Vloží do DB nový záznam strediska.
        /// </summary>
        /// <param name="zdrojRow">Stredisko, které se má vložit.</param>
        /// <returns>True - vše v pořádku, False - chyba</returns>
        bool InsertStredisko(Fask.Interfaces.DataSets.Strediska.CZMST091Row strediskoRow);
    }
}
