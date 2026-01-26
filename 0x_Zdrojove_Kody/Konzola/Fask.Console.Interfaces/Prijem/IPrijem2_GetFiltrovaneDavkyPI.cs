using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Prijem
{
    public interface IPrijem2_GetFiltrovaneDavkyPI : IPrijem2
    {
        /// <summary>
        /// Vraci CZMST_PI podle zadaneho filtru
        /// </summary>
        /// <param name="filtr"></param>
        /// <returns></returns>
        Fask.Interfaces.DataSets.Prijem GetFiltrovaneDavkyPI(Fask.Interfaces.Filtry.PrijemNasnimaneFiltr filtr);

    }
}
