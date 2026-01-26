using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Inventura
{
    public interface IInventura2_GetFiltrovanaPredloha : IInventura2
    {

        /// <summary>
        /// Vraci data predlohy vcetne poctu nasnimanych polozek a pripadneho rozdilu ve vystupnich datech (czmst_i4)
        /// </summary>
        /// <param name="filtr">Zvoleny filtr.</param>
        /// <returns></returns>
        Fask.Interfaces.DataSets.Inventura GetFiltrovanaPredloha(Fask.Interfaces.Filtry.InventuraPredlohaListFiltr filtr);

    }

}
