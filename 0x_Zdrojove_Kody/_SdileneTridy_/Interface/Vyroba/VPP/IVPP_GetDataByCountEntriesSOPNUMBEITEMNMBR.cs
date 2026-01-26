using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.VPP
{
    public interface IVPP_GetDataByCountEntriesSOPNUMBEITEMNMBR : IVPP
    {
        Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable GetDataByCountEntriesSOPNUMBEITEMNMBR(int CountEntries, string SOPNUMBE, string ITEMNMBR);
    }
}
