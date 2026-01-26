using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.Pracovnici
{
    public interface IPracovnici2_UpdatePracovnici : IPracovnici2
    {
        /// <summary>
        /// Aktualizuje informace o Pracovnikovy.
        /// </summary>
        /// <param name="zdrojRow">Pracovnici, ktery se bude aktualizovat</param>
        /// <returns></returns>
        bool UpdatePracovnici(Fask.Interfaces.DataSets.Pracovnici.CZMST096Row PracovniciRow);

    }
}
