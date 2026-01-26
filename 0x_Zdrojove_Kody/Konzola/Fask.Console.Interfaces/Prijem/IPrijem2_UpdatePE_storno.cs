using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Prijem
{
    public interface IPrijem2_UpdatePE_storno : IPrijem2
    {
        /// <summary>
        /// vloži řadek...
        /// </summary>
        /// <param name="zdrojRows"> rows </param>
        /// <returns></returns>
        int UpdatePE_storno(Fask.Interfaces.DataSets.Prijem.CZMST_PEDataTable dt);
    }
}
