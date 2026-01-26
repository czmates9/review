using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Prijem
{
    public interface IPrijem2_UpdatePI_storno : IPrijem2
    {
        /// <summary>
        /// vloži řadek...
        /// </summary>
        /// <param name="zdrojRows"> rows </param>
        /// <returns></returns>
        int UpdatePI_storno(Fask.Interfaces.DataSets.Prijem.CZMST_PIDataTable dt);
    }
}
