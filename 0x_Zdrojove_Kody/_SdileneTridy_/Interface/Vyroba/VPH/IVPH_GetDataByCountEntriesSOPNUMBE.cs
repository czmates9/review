using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.VPH
{
    public interface IVPH_GetDataByCountEntriesSOPNUMBE : IVPH
    {
        Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHDataTable GetDataByCountEntriesSOPNUMBE(int CountEntries, string SOPNUMBE);
    }
}
