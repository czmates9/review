using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Prijem
{
    public interface IPrijem2_GetFiltrovaneDavkyPE : IPrijem2
    {
        /// <summary>
        /// Vraci CZMST_PI podle zadaneho filtru
        /// </summary>
        /// <param name="filtr"></param>
        /// <returns></returns>
        Fask.Interfaces.DataSets.Prijem GetFiltrovaneDavkyPE(Fask.Interfaces.Filtry.PrijemDavkyFiltr filtr);

    }
}
