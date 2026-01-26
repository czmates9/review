using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.StavSkladu
{
    public interface IStavSkladu2_GetStavSkladu : IStavSkladu2
    {
        /// <summary>
        /// Vraci vsechny zaznamy stavu skladu.
        /// </summary>
        /// <returns>Dataset s kompletne nactenym stavem skladu.</returns>
        void GetStavSkladu(ref Fask.Interfaces.DataSets.StavSkladu StavSkladu);

    }
}
