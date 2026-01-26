using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.SkladLokace
{
    public interface ISkladLokace2_InsertStavPohyb : ISkladLokace2
    {

        /// <summary>
        /// Provede prijem/vydej/presun podle typu pohybu.
        /// </summary>
        /// <param name="Record">Zaznam, ktereho se defregmentace tyka.</param>
        bool InsertStavPohyb(LokacePohyb Record);
    }
}
