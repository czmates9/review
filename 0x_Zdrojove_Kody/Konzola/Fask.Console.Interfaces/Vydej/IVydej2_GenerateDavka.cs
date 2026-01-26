using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fask.Interfaces.Classes;

namespace Fask.Interfaces.Vydej
{
    public interface IVydej2_GenerateDavka
    {
        // ************** Metody pro pripravu predlohy **************** //
        /// <summary>
        /// Generuje data predlohy
        /// </summary>
        /// <param name="objednavka"></param>
        /// <returns></returns>
        StatusInfo Vydej_GenerateDavka(Objednavka objednavka, Sklad sklad,int? CountEntries);
    }
}
