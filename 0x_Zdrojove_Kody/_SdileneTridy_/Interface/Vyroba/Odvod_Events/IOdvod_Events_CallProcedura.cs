using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.Odvod_Events
{
    public interface IOdvod_Events_CallProcedura : IOdvod_Events
    {
        /// <summary>
        /// jedná se o metodu která zavolá proceduru pro Export z FASK_Events do Productions
        /// </summary>
        /// <param name="OD">je typi DATETIME? a OD ktereho datumu se to ma dotahnotu</param>
        /// <param name="DO">je typi DATETIME? a DO ktereho datumu se to ma dotahnotu</param>
        string CallProcedura(DateTime? OD, DateTime? DO, string Material, out int? CountEntries);
    }
}
