using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.VPP
{
    public interface IVPP_FillByCountEntriesAndSOPNUMBE : IVPP
    {
        int FillByCountEntriesAndSOPNUMBE(Fask.Interfaces.DataSets.Vyroba ds, string TypeORDERBY ,int CountEntries, string SOPNUMBE);
    }
}
