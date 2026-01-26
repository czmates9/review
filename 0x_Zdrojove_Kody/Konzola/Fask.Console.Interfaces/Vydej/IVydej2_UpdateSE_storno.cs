using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vydej
{
    public interface IVydej2_UpdateSE_storno : IVydej2
    {
        /// <summary>
        /// vloži řadek...
        /// </summary>
        /// <param name="zdrojRows"> rows </param>
        /// <returns></returns>
        int UpdateSE_storno(Fask.Interfaces.DataSets.Vydej.CZMST_SEDataTable dt);
    }
}
