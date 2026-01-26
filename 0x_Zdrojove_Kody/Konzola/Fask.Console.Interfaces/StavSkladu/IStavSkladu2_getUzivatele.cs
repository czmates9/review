using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.StavSkladu
{
    public interface IStavSkladu2_getUzivatele : IStavSkladu2
    {

        /// <summary>
        /// Vraci vsechny zaznamy uzivatelu z tabulky czmstpwd.
        /// </summary>
        /// <returns>Dataset se vsemi uzivateli.</returns>
        void getUzivatele(ref Fask.Interfaces.DataSets.StavSkladu StavSkladu);

    }
}
