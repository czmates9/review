using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using FASK.MST_WINDOWS.Logging;
using FASK.MST_WINDOWS.Main.Configuration;

namespace FASK.MST_WINDOWS.Main.Configuration
{
   public class Config
    {
       private const string cfilename = @"Configuration\GlobalConfig.xml";

        
        private static string configfilename;
        public static string ConfigFileName
        {
            get { return configfilename; }
        }

        public static GlobalConfig config = new GlobalConfig();

        static Config()
        {
            string filename = Path.GetDirectoryName(Application.ExecutablePath);
            configfilename = Path.Combine(filename, cfilename);
        }

        /// <summary>
        /// Nacteni globalni konfigurace do tabulky
        /// </summary>
        public static void Load()
        {
            try
            {
                config.ReadXml(ConfigFileName);
                //1.moznost pro ziskani informaci
                //2.moznost = save global configuration v gui - viz frmGlobalConfiguration.cs
                //Hned po nacteni se pokusim predat logging modulu informace - machineID, localSqlConnString
                //LogConfig.MachineID = Configuration.Config.config.Main[0].MachineID;
                //LogConfig.MachineType = Configuration.Config.config.Main[0].MachineType;
                //LogConfig.Koeficient = decimal.Parse(Configuration.Config.config.Main[0].Koeficient);
                //LogConfig.SqlConnectionStringLocal = Configuration.Config.config.Main[0].SqlConnectionStringLocal;
                //LogConfig.SqlConnectionStringGlobal = Configuration.Config.config.Main[0].SqlConnectionStringRemote;
                Main_KomServer = config.Main[0].KomServer;
                Main_TerminalID = config.Main[0].TerminalID;
                Main_OverVuciAD = config.Main[0].OverVuciAD;
                Main_DomenaProAD = config.Main[0].DomenaProAD;

                RFID_Address = config.RFID[0].Address;
                RFID_Baudrate = config.RFID[0].Baudrate;
                RFID_ComPort = config.RFID[0].ComPort;

                LoginMetod_Description = config.LoginMetod[0].Description;
                LoginMetod_Login = config.LoginMetod[0].Login;
                LoginMetod_Method = config.LoginMetod[0].Method;
                LoginMetod_Password = config.LoginMetod[0].Password;

                ServerAccess =(ServerAccessType)Enum.Parse(typeof(ServerAccessType), config.Komunikace_HTTPs[0].ServerAccess, true);
                ServerAccessAllowDecompression = config.Komunikace_HTTPs[0].ServerAccessAllowDecompression;
                ServerAccessAllowRedirection = config.Komunikace_HTTPs[0].ServerAccessAllowRedirection;
                ServerAccessDomain = config.Komunikace_HTTPs[0].ServerAccessDomain;
                ServerAccessPassword = config.Komunikace_HTTPs[0].ServerAccessPassword;
                ServerAccessPreauthenticate = config.Komunikace_HTTPs[0].ServerAccessPreauthenticate;
                ServerAccessUsername = config.Komunikace_HTTPs[0].ServerAccessUsername;

                ServerAccessCertificateTrust = (ServerAccessCertificatesTrustType)Enum.Parse(typeof(ServerAccessCertificatesTrustType), config.Komunikace_HTTPs[0].ServerAccessCertificateTrust, true);
                
            }
            catch (Exception ex)
            {
                ErrorLog.Log.Write(ex.Message);
            }
        }

        /// <summary>
        /// Ulozeni globalni konfigurace
        /// </summary>
        public static void Save()
        {
            config.WriteXml(ConfigFileName);
        }

        /// <summary>
        /// Tvorba nazvu modulu pro zobrazeni
        /// </summary>
        /// <param name="id">Id modulu</param>
        /// <param name="name">Jmeno modulu</param>
        /// <returns>Nazev modulu</returns>
        public static string createModuleListString(string id, string name)
        {
            return id + " : " + name;
        }

        #region Promenne naèitane

        public const string modulenonestring = " < none > ";

        /// <summary>
        /// ID uzivatele
        /// </summary>
        public static int? LoginID { get; set; }

        /// <summary>
        /// ID Masiny
        /// </summary>
        public static string Main_TerminalID { get; set; }

        /// <summary>
        /// Jmeno uzivatele
        /// </summary>
        public static string LoginName { get; set; }


        /// <summary>
        /// adresa komunikacniho serveru
        /// </summary>
        public static string Main_KomServer { get; set; }

        /// <summary>
        /// Overovat vèi AD
        /// 
        /// </summary>
        public static bool Main_OverVuciAD { get; set; }

       /// <summary>
       /// Domena pro AD
       /// </summary>
        public static string Main_DomenaProAD { get; set; }


        /// <summary>
        /// RFID adresa ked ich je vic
        /// </summary>
        public static string RFID_Address { get; set; }

        /// <summary>
        /// Rychlost pro RFID
        /// </summary>
        public static string RFID_Baudrate { get; set; }

        /// <summary>
        /// COM port pro RFID
        /// </summary>
        public static string RFID_ComPort { get; set; }

        /// <summary>
        /// Poznamka pro metodu prihlaseni
        /// </summary>
        public static string LoginMetod_Description { get; set; }


        public static string LoginMetod_Login { get; set; }
        public static string LoginMetod_Method { get; set; }
        public static string LoginMetod_Password { get; set; }

        #endregion


        #region Komunikace HTTPs 

        public enum ServerAccessType
        {
            Anonymous,
            Credentials
        }

        private static string _serverAccess = "Anonymous";
        public static ServerAccessType ServerAccess
        {
            get { return (ServerAccessType)Enum.Parse(typeof(ServerAccessType), _serverAccess, true); }
            set { _serverAccess = value.ToString(); }
        }

        private static string _serverAccessUsername = string.Empty;
        public static string ServerAccessUsername
        {
            get { return _serverAccessUsername; }
            set { _serverAccessUsername = value; }
        }


        private static string _serverAccessPassword = string.Empty;
        public static string ServerAccessPassword
        {
            get { return _serverAccessPassword; }
            set { _serverAccessPassword = value; }
        }

        private static string _serverAccessDomain = string.Empty;
        public static string ServerAccessDomain
        {
            get { return _serverAccessDomain; }
            set { _serverAccessDomain = value; }
        }

        private static bool _serverAccessPreauthenticate = false;
        public static bool ServerAccessPreauthenticate
        {
            get { return _serverAccessPreauthenticate; }
            set { _serverAccessPreauthenticate = value; }
        }

        private static bool _serverAccessAllowRedirection = false;
        public static bool ServerAccessAllowRedirection
        {
            get { return _serverAccessAllowRedirection; }
            set { _serverAccessAllowRedirection = value; }
        }

        private static bool _serverAccessAllowDecompression = false;
        public static bool ServerAccessAllowDecompression
        {
            get { return _serverAccessAllowDecompression; }
            set { _serverAccessAllowDecompression = value; }
        }

        #region Certifikaty

        public enum ServerAccessCertificatesTrustType
        {
            OnlyInstalled,
            TrustAll,
            TrustQuery
        }


        private static string _serverAccessCertificateTrust = "TrustAll";
        public static ServerAccessCertificatesTrustType ServerAccessCertificateTrust
        {
            get
            {
                var tmp = (ServerAccessCertificatesTrustType)Enum.Parse(typeof(ServerAccessCertificatesTrustType), _serverAccessCertificateTrust, true);

                switch (tmp)
                {
                    case ServerAccessCertificatesTrustType.TrustAll:
                        System.Net.ServicePointManager.CertificatePolicy = new ServerAccess.CertificatesPolicy.TrustAllCertificatePolicy();
                        break;
                    case ServerAccessCertificatesTrustType.TrustQuery:
                        System.Net.ServicePointManager.CertificatePolicy = new ServerAccess.CertificatesPolicy.QueryTrustCertificatePolicy();
                        break;
                    case ServerAccessCertificatesTrustType.OnlyInstalled:
                    default:
                        System.Net.ServicePointManager.CertificatePolicy = null;
                        break;
                }

                return tmp;
            }
            set
            {
                //SetValue("ServerAccessCertificatesTrust", value.ToString());
                _serverAccessCertificateTrust = value.ToString();
                var tmp = (ServerAccessCertificatesTrustType)Enum.Parse(typeof(ServerAccessCertificatesTrustType), _serverAccessCertificateTrust, true);

                switch (tmp)
                {
                    case ServerAccessCertificatesTrustType.TrustAll:
                        System.Net.ServicePointManager.CertificatePolicy = new ServerAccess.CertificatesPolicy.TrustAllCertificatePolicy();
                        break;
                    case ServerAccessCertificatesTrustType.TrustQuery:
                        System.Net.ServicePointManager.CertificatePolicy = new ServerAccess.CertificatesPolicy.QueryTrustCertificatePolicy();
                        break;
                    case ServerAccessCertificatesTrustType.OnlyInstalled:
                    default:
                        System.Net.ServicePointManager.CertificatePolicy = null;
                        break;
                }

            }
        }

        #endregion

        #endregion


    }
}
