using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.SkladLokace
{
   public interface ISkladLokace2_GetFiltrovanySkladLokaceStav : ISkladLokace2
    {

        /// <summary>
        /// Vrací záznamy stavu skladu lokacniho mechanismu podle zadaného filtru.
        /// </summary>
        /// <param name="ds">Filtr záznamů.</param>
        /// <returns>Vyfiltrované záznamy.</returns>
        Fask.Interfaces.DataSets.SkladLokace GetFiltrovanySkladLokaceStav(Fask.Interfaces.Filtry.SkladLokaceStavListFiltr filtr);

    }
}
