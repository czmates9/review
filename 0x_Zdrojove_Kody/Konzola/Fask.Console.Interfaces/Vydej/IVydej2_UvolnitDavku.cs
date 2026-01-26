using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fask.Interfaces.Classes;

namespace Fask.Interfaces.Vydej
{
    public interface IVydej2_UvolnitDavku : IVydej2
    {

        /// <summary>
        /// Uvolneni davky podle 
        /// </summary>
        /// <param name="objednavka">ID >> SOPNUMBER a CisloDavky >> CountEntries </param>
        /// <returns></returns>
        bool Vydej_UvolnitDavku(int CountEntries);
    }
}
