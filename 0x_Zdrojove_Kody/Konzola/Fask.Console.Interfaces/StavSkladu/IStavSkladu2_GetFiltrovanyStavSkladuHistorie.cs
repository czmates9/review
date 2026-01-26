using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.StavSkladu
{
    public interface IStavSkladu2_GetFiltrovanyStavSkladuHistorie : IStavSkladu2
    {
        /// <summary>
        /// Vrací záznamy historie pohybů podle zadaného filtru.
        /// </summary>
        /// <param name="StavSkladuFiltr">Filtr záznamů.</param>
        /// <param name="StavSkladu">Vyfiltrovaná historie pohybů.</param>
        void GetFiltrovanyStavSkladuHistorie(Fask.Interfaces.Filtry.StavSkladuHistorieFiltr StavSkladuFiltr, ref Fask.Interfaces.DataSets.StavSkladu StavSkladu);

    }
}
