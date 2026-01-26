using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vydej
{
    public interface IVydej2_GetFiltrovaneDavkySI : IVydej2
    {
        /// <summary>
        /// Vraci CZMST_SI podle zadaneho filtru
        /// </summary>
        /// <param name="filtr"></param>
        /// <returns></returns>
        Fask.Interfaces.DataSets.Vydej GetFiltrovaneDavkySI(Fask.Interfaces.Filtry.VydejNasnimaneFiltr filtr);

    }

}
