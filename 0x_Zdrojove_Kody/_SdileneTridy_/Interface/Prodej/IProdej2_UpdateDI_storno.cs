using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Prodej
{
    public interface IProdej2_UpdateDI_storno : IProdej2
    {
        /// <summary>
        /// vloži řadek...
        /// </summary>
        /// <param name="zdrojRows"> rows </param>
        /// <returns></returns>
        int UpdateDI_storno(Fask.Interfaces.DataSets.Prodej.CZMST_DIDataTable dt);
    }
}
