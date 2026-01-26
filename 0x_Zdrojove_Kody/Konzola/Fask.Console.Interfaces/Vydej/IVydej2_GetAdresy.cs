using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vydej
{
    public interface IVydej2_GetAdresaOdberatel : IVydej2
    {
        /// <summary>
        /// Vraci adresy dotažene pro objednavku
        /// </summary>
        /// <returns></returns>
        Fask.Interfaces.DataSets.Vydej GetAdresaOdberatel(string SOPNUMBE);
    }
}
