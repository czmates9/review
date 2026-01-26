using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fask.Interfaces.Classes;

namespace Fask.Interfaces.Inventura
{
    public interface IInventura2_SloucitInventury : IInventura2
    {
        // ************** Metody pro pripravu predlohy **************** //
        /// <summary>
        /// Sloučí inventury do jedne
        /// </summary>
        /// <param name="CountEntries">seznam davek invnetur ke slouceni</param>
        /// <returns></returns>
        StatusInfo SloucitInventury(List<string> CountEntries, string sloucenaI);
    }
}
