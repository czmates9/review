using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Prijem
{
   public interface IPrijem2_UpdatePE : IPrijem2
    {
        /// <summary>
        /// vloži řadek...
        /// </summary>
        /// <param name="zdrojRow"> row </param>
        /// <returns></returns>
        bool UpdatePE(Fask.Interfaces.DataSets.Prijem.CZMST_PERow PERow);

        /// <summary>
        /// update řadky...
        /// </summary>
        /// <param name="zdrojRow"> dataTable </param>
        /// <returns></returns>
        int UpdatePE(Fask.Interfaces.DataSets.Prijem.CZMST_PEDataTable PE_dt);
    }
}
