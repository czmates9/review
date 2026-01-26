using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.VPP
{
    public interface IVPP_GetDataByCountEntriesSopnumbeItemnmbrBarcodeP : IVPP
    {

        Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable GetDataByCountEntriesSopnumbeItemnmbrBarcodeP(int CountEntries, string SOPNUMBE, string ITEMNMBR, string BarcodeP);
    }
}
