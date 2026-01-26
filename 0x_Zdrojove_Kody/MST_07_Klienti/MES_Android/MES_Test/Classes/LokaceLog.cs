using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MES_Android.Classes
{
    static class LokaceLog
    {
        public static void writeBody(LokaceService.LokacePohyb pohybRow, string UserID)
        {
            Fask.Logging.LogLevel lvl = Fask.Logging.LogLevel.Location;

            Fask.Logging.ExceptionHandler2.Handle(lvl, "Guid winformat:" + pohybRow.guid);
            Fask.Logging.ExceptionHandler2.Handle(lvl, "Terminal,user:" + pohybRow.TermID + "," + UserID);
            Fask.Logging.ExceptionHandler2.Handle(lvl, "Type of record(saved to database):" + pohybRow.POHYB_TYPE.ToString());
            Fask.Logging.ExceptionHandler2.Handle(lvl, "location src,skl_id_src,location dst,skl_id_dst,material,serltnum,quantity: " + pohybRow.LOCNCODE_SRC + "," + pohybRow.SKL_ID_SRC + "," + pohybRow.LOCNCODE_DST + "," + pohybRow.SKL_ID_DST + "," + pohybRow.ITEMNMBR + "," + pohybRow.SERLTNUM + "," + pohybRow.QTYSHPPD);
            //Fask.Logging.ExceptionHandler2.Handle(lvl, "SkladSrcRegal,LokaceSrc,material,quantity:" + tableRow.SKL_ID_SRC.Trim() + "," + tableRow.LOCNCODE_SRC.Trim() + "," + tableRow.ITEMNMBR + "," + tableRow.QTYSHPPD);
        }
    }
}