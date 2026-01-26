using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fask.MST_W_Server.Classes
{
    public static class Stats
    {
        private static int count = 0;
        private static DateTime lastSend = DateTime.Now;
        private static TimeSpan saveInterval = new TimeSpan(12, 0, 0);

        public static void addRequest()
        {
            count++;

            if (DateTime.Now - lastSend > saveInterval)
                lastDataSend(); 
        }

        public static void setRequestTime(int hours)
        {
            saveInterval = new TimeSpan(hours, 0, 0);
        }
     
        private static void lastDataSend()
        {
            //Odeslani emailu
			Fask.Logging.ExceptionHandler2.Handle_Email("Počet dotazů na server za posledních " + saveInterval.Hours + " hodin: " + count);

            lastSend = DateTime.Now;
            count = 0;
        }

    }
}
