using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fask.Interfaces.Classes;

namespace Fask.Interfaces.Inventura
{
    public interface IInventura2_GenerateInventura : IInventura2
    {
        // ************** Metody pro pripravu predlohy **************** //
        /// <summary>
        /// Generuje data predlohy inventury
        /// </summary>
        /// <param name="objednavka"></param>
        /// <returns></returns>
        StatusInfo GenerateInventura();

    }
}
