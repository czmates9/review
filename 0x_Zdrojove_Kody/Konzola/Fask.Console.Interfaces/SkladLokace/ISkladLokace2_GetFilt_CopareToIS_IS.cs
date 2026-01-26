using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.SkladLokace
{
   public interface ISkladLokace2_GetFilt_CopareToIS_IS : ISkladLokace2
    {

        /// <summary>
        /// Vrací záznamy stavu skladu lokacniho mechanismu podle zadaného filtru.
        /// </summary>
        /// <param name="ds">Filtr záznamů.</param>
        /// <returns>Vyfiltrované záznamy.</returns>
        Fask.Interfaces.DataSets.SkladLokace_CompareToIS GetFilt_CopareToIS_IS(Fask.Interfaces.Filtry.SkladLokaceCompareToISFiltr filtr);

    }
}
