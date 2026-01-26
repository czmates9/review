using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vydej
{
    public interface IVydej2_GetFiltrovaneHlavicky
    {
        /// <summary>
        /// Vraci hlavicky podle zadaneho filtru
        /// </summary>
        /// <param name="filtr"></param>
        /// <returns></returns>
        Fask.Interfaces.DataSets.Vydej GetFiltrovaneHlavicky(Fask.Interfaces.Filtry.VydejHlavickyFiltr filtr);

    }
}
