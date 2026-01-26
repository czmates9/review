using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Fask.MST_W.Classes
{
    static class LokaceLog
    {
        public static void writeBody(Fask.MST_W.LokaceService.LokacePohyb pohybRow)
        {
            string context = "LocationLog";

            Logging.Log.WriteAdvanced("Guid winformat:" + pohybRow.guid, context);
            Logging.Log.WriteAdvanced("Terminal,user:" + pohybRow.TermID + "," + Settings.UserLogin, context);
            Logging.Log.WriteAdvanced("Type of record(saved to database):" + pohybRow.POHYB_TYPE.ToString(), context);
            Logging.Log.WriteAdvanced("location src,skl_id_src,location dst,skl_id_dst,material,serltnum,quantity: " + pohybRow.LOCNCODE_SRC + "," + pohybRow.SKL_ID_SRC + "," + pohybRow.LOCNCODE_DST + "," + pohybRow.SKL_ID_DST + "," + pohybRow.ITEMNMBR + "," + pohybRow.SERLTNUM + "," + pohybRow.QTYSHPPD, context);
            //Logging.Log.WriteAdvanced("SkladSrcRegal,LokaceSrc,material,quantity:" + tableRow.SKL_ID_SRC.Trim() + "," + tableRow.LOCNCODE_SRC.Trim() + "," + tableRow.ITEMNMBR + "," + tableRow.QTYSHPPD, context);
        }
    }
}
