using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Interfaces.Inventura
{
    public interface IInventura2_GetCompare_I4
    {
        /// <summary>
        /// Vrací záznamy stavu CZMST_I4 podle zadaného filtru.
        /// </summary>
        /// <param name="ds">Filtr záznamů.</param>
        /// <returns>Vyfiltrované záznamy.</returns>
        Fask.Interfaces.DataSets.Inventura_Compare GetCompare_I4(Fask.Interfaces.Filtry.InventuraCompare filtr);

    }
}
