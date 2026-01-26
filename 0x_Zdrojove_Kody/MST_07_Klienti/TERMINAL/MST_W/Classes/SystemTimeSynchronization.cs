using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Fask.MST_W.ServerAccess;

namespace Fask.MST_W.Classes
{
    internal class SystemTimeSynchronization
    {
        /// <summary>
        /// Synchronizuje cas s casovym serverem dle konfigurace
        /// </summary>
        /// <exception>Muze vyvolat vyjimku</exception>
        public bool Synchronize()
        {
            /* 
             * 1) nacteni konfigura
             * 2) natazeni casu ze vzdaleneho casoveho serveru
             * 3) nastaveni aktualniho casu
             */

            DateTime dtServer = GetDateTimeFromServer();
            DateTime dtTerminal = DateTime.Now;
            TimeSpan tsRozdil = dtServer - dtTerminal;
            if (Math.Abs(tsRozdil.TotalMinutes) > 1) // udela synchronizaci, pouze pokud je rozdil casu vetsi jak 1 minuta...
            {
                return this.SetDateTime(dtServer);
            }
            else
            {
                return true;
            }
        }

        public DateTime GetDateTimeFromServer()
        {
            return GetDateTimeFromWebService();
        }

        private DateTime GetDateTimeFromWebService()
        {
            DateTime dtserver = DateTime.Now;

            _WebRefernces_Globals.SystemTimeServiceSession systime = new Fask.MST_W._WebRefernces_Globals.SystemTimeServiceSession();
            systime.Url = MST_Global.ServerAddress + "SystemTime.asmx";
            systime.Timeout = 15000;
            systime.UpdateWebServiceCredentials();

            dtserver = systime.GetSystemTime();

            return dtserver;
        }

        /// <summary>
        /// Nastavi znovu aktualni cas
        /// </summary>
        public bool SetDateTime(DateTime dt)
        {
            try
            {
                Fask.SystemTime.SystemDateTime.SetDeviceTime(dt);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
