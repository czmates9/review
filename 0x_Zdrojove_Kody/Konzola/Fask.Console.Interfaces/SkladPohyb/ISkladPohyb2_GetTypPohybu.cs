using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.SkladPohyb
{
    public interface ISkladPohyb2_GetTypPohybu : ISkladPohyb2
    {
        /// <summary>
        /// Vrací veškeré typy pohybu (distinct TYPE)
        /// </summary>
        /// <returns></returns>
        Fask.Interfaces.DataSets.SkladPohyb.CZMST_Sklad_PohybDataTable GetTypPohybu(string tableName);
   

    }
}
