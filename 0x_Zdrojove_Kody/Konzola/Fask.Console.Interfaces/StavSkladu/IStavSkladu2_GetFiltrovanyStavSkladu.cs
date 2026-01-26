using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.StavSkladu
{
    public interface IStavSkladu2_GetFiltrovanyStavSkladu : IStavSkladu2
    {
        /// <summary>
        /// Vrací záznamy podle zadaného filtru.
        /// </summary>
        /// <param name="StavSkladuFiltr">Filtr záznamů.</param>
        /// <returns>Vyfiltrovaný stav skladu.</returns>
        void GetFiltrovanyStavSkladu(Fask.Interfaces.Filtry.StavSkladuListFiltr StavSkladuFiltr, ref Fask.Interfaces.DataSets.StavSkladu StavSkladu);

    }
}
