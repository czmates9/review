using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.Strediska
{
    public interface IStrediska2_UpdateStredisko : IStrediska2
    {
        /// <summary>
        /// Aktualizuje informace o stredisku.
        /// </summary>
        /// <param name="zdrojRow">Stredisko, ktere se bude aktualizovat</param>
        /// <returns></returns>
        bool UpdateStredisko(Fask.Interfaces.DataSets.Strediska.CZMST091Row strediskoRow);
    }
}
