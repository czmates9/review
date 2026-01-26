using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Interfaces.Inventura
{
    public interface IInventura2_GetCompare_I123
    {


        /// <summary>
        /// Vrací záznamy stavu CZMST_I123 podle zadaného filtru.
        /// </summary>
        /// <param name="ds">Filtr záznamů.</param>
        /// <returns>Vyfiltrované záznamy.</returns>
        Fask.Interfaces.DataSets.Inventura_Compare GetCompare_I123(Fask.Interfaces.Filtry.InventuraCompare filtr);

    }
}
