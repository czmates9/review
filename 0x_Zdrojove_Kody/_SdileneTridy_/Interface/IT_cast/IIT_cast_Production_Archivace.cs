using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.IT_cast
{
    public interface IIT_cast_Production_Archivace : IIT_cast
    {
        /// <summary>
        /// jedná se o metodu která zavolá proceduru pro Export z FASK_Events do Productions
        /// </summary>
        /// <param name="OD">je typi DATETIME? a OD ktereho datumu se to ma dotahnotu</param>
        /// <param name="DO">je typi DATETIME? a DO ktereho datumu se to ma dotahnotu</param>
        int Production_ArchivaceProcedura(DateTime? OD, DateTime? DO);
        int Production_ArchivaceProcedura_guid(Guid guid);
    }
}
