using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.MachinesDefinition
{
    public interface IMachinesDefinition_IudCommand : IMachinesDefinition
    {

        /// <summary>
        /// Smaže modul.
        /// </summary>
        /// <param name="MachinesDefinitionRow">Modul, který se má vložit.</param>
        /// <returns>True - vše v pořádku, False - chyba</returns>
        bool DeleteMachinesDefinition(Fask.Interfaces.DataSets.Vyroba.MachinesDefinitionRow MachinesDefinitionRow);

        /// <summary>
        /// Vloží do DB nový záznam modulu.
        /// </summary>
        /// <param name="MachinesDefinitionRow">Stroj, který se má vložit.</param>
        /// <returns>True - vše v pořádku, False - chyba</returns>
        bool InsertMachinesDefinition(Fask.Interfaces.DataSets.Vyroba.MachinesDefinitionRow MachinesDefinitionRow);


        /// <summary>
        /// Aktualizuje informace o modulu.
        /// </summary>
        /// <param name="MachinesDefinitionRow">Modul, ktery se bude aktualizovat</param>
        /// <returns></returns>
        bool UpdateMachinesDefinition(Fask.Interfaces.DataSets.Vyroba.MachinesDefinitionRow MachinesDefinitionRow);
    }
}
