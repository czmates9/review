using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.Machines
{
    public interface IMachines_IudCommand : IMachines
    {

        /// <summary>
        /// Smaže stroj.
        /// </summary>
        /// <param name="MachinesRow">Stroj, který se má vložit.</param>
        /// <returns>True - vše v pořádku, False - chyba</returns>
        bool DeleteMachines(Fask.Interfaces.DataSets.Vyroba.MachinesRow MachinesRow);

        /// <summary>
        /// Vloží do DB nový záznam stroje.
        /// </summary>
        /// <param name="MachinesRow">Stroj, který se má vložit.</param>
        /// <returns>True - vše v pořádku, False - chyba</returns>
        bool InsertMachines(Fask.Interfaces.DataSets.Vyroba.MachinesRow MachinesRow);


        /// <summary>
        /// Aktualizuje informace o stroji.
        /// </summary>
        /// <param name="MachinesRow">Stroj, ktery se bude aktualizovat</param>
        /// <returns></returns>
        bool UpdateMachines(Fask.Interfaces.DataSets.Vyroba.MachinesRow MachinesRow);

    }
}

