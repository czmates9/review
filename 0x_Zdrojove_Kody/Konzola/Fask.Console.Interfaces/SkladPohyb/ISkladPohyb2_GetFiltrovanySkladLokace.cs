using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.SkladPohyb
{
    public interface ISkladPohyb2_GetFiltrovanySkladLokace : ISkladPohyb2
    {
        /// <summary>
        /// Vrací záznamy podle zadaného filtru.
        /// </summary>
        /// <param name="ds">Filtr záznamů.</param>
        /// <returns>Vyfiltrované pohyby.</returns>
        Fask.Interfaces.DataSets.SkladPohyb GetFiltrovanySkladLokace(Fask.Interfaces.Filtry.SkladPohybListFiltr filtr, string tableName);

    }
}
