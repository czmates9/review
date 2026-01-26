using System;
using System.Collections.Generic;
using System.Text;

namespace Fask.SystemTime
{
    public class Synchronization : IMethods, IConfiguration
    {

        #region IMethods Members


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
            DateTime dtServer = DateTime.Now;
            switch (this.Source)
            {
                case ConfigurationSource.Database:
                    dtServer = GetDateTimeFromDatabase(this.Connection);
                    break;
                case ConfigurationSource.WebService:
                    dtServer = GetDateTimeFromWebSystemTimeService(this.Connection);
                    break;
                case ConfigurationSource.NNTP:
                    throw new Exception("Not implemented");
                    break;
                case ConfigurationSource.None:
                default:
                    dtServer = DateTime.Now;
                    break;
            }
            return dtServer;
        }

        private DateTime GetDateTimeFromDatabase(string connectionstring)
        {
            DateTime dtserver = DateTime.Now;
            System.Data.SqlClient.SqlCommand sqlcomm = null;
            try
            {
                sqlcomm = new System.Data.SqlClient.SqlCommand(
                "Select GETDATE()",
                new System.Data.SqlClient.SqlConnection(connectionstring)
                );

                sqlcomm.Connection.Open();

                object o = sqlcomm.ExecuteScalar();
                dtserver = Convert.ToDateTime(o);

            }
            finally
            {
                if (sqlcomm != null && sqlcomm.Connection.State == System.Data.ConnectionState.Open)
                    sqlcomm.Connection.Close();
            }
            return dtserver;
        }

        private DateTime GetDateTimeFromWebSystemTimeService(string connectionstring)
        {
            DateTime dtserver = DateTime.Now;
         
            SystemTimeService.SystemTime systime = new Fask.SystemTime.SystemTimeService.SystemTime();
            systime.Url = connectionstring;

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
                SystemDateTime.SetDeviceTime(dt);
                return true;
            }
            catch 
            {
                return false;
            }
        }

        //public void LoadConfig()
        //{
        //}

        public void SaveConfig()
        {
            SettingsSystemTime.Update();
        }

        #endregion

        #region IConfiguration Members

        private ConfigurationSource _Source = SettingsSystemTime.Source;
        public ConfigurationSource Source
        {
            get
            {
                return _Source;
            }
            set
            {
                _Source = value;
            }
        }

        private string _Connection = SettingsSystemTime.Connection;
        public string Connection
        {
            get
            {
                return _Connection;
            }
            set
            {
                _Connection = value;
            }
        }

        #endregion
    }
}
