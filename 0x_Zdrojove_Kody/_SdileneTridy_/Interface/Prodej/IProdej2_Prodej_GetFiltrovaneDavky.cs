using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fask.Interfaces.Filtry;

namespace Fask.Interfaces.Prodej
{
    public interface IProdej2_Prodej_GetFiltrovaneDavky : IProdej2
    {
        /// <summary>
        /// Vraci CZMST_SE podle zadaneho filtru
        /// </summary>
        /// <param name="filtr"></param>
        /// <returns></returns>
        Fask.Interfaces.DataSets.Prodej Prodej_GetFiltrovaneDavky(ProdejFiltr filtr);

    }
}
