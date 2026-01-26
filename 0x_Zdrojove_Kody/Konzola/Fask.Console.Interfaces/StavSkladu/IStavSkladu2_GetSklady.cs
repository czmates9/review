using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.StavSkladu
{
    public interface IStavSkladu2_GetSklady : IStavSkladu2
    {

        /// <summary>
        /// Vraci vsechny sklady z tabulky czmst093.
        /// </summary>
        /// <returns>Dataset se vsemi sklady.</returns>
        void GetSklady(ref Fask.Interfaces.DataSets.StavSkladu StavSkladu);
    }
}
