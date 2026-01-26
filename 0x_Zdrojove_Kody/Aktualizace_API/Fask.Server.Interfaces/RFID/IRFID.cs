using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Server.Interfaces.RFID
{
    public interface IRFID
    {
        List<int> RFID_GetNextSerial(string itemnmbr, string skl_id, int countSerials);
        bool RFID_Assign(Fask.DataSets.RFID rfid);
        bool RFID_Assign_From_Vydej(Fask.DataSets.Vydej vydej_rfid);
        bool RFID_Assign_From_Prodej(Fask.DataSets.ProdejData prodej_rfid);
    }
}
