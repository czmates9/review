using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fask.Server.Interfaces.Classes;
using System.Data;

namespace Fask.Server.Interfaces.Vyroba
{
    public interface IVyroba_00 : IWebModule
    {

        // ************** Metody pro pripravu transakce generovani dat **************** //
        /// <summary>
        /// Generuje data predlohy
        /// </summary>
        /// <param name="objednavka"></param>
        /// <returns></returns>
        StatusInfo Vyroba_GenerateDavka(Objednavka objednavka, Sklad sklad);


    }
}
