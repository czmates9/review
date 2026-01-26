using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;


using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Collections.Specialized;

namespace MES_Android.Config
{
    public static class Settings
    {

        private static NameValueCollection m_settings;
        public static bool StatusPath;
        //hodnota pro povoleni odesilani trace + logu - pouziva se v MainActivity.cs
        public static bool LogUpload;
        //int jak casto se budou uploadovat logy
        public static int LogUploadTimer;

        static Settings()
        {


        }

        public static void Initialize()
        {
            LogUploadTimer = 300000;
            LogUpload = true;
            StatusPath = false;

            m_settings = new NameValueCollection();

            if (File.Exists(Classes.DataInfo_Static.SettingsXML))
            {
                StatusPath = true;
                System.Xml.XmlDocument xdoc = new System.Xml.XmlDocument();
                xdoc.Load(Classes.DataInfo_Static.SettingsXML);
                XmlElement root = xdoc.DocumentElement;
                foreach (XmlNode node in root.SelectNodes("/configuration/appSettings/add"))
                {
                    m_settings.Add(node.Attributes["key"].Value, node.Attributes["value"].Value);
                }
                StatusPath = true;
            }
            else
            {
                if (!Directory.Exists(Classes.DataInfo_Static.PathDir))
                    Directory.CreateDirectory(Classes.DataInfo_Static.PathDir);
                Settings.Create();
                Settings.Update();
                StatusPath = true;
            }

            var x = Config.Settings.ServerAccessCertificateTrust; // Tohle je z duvodu načtení certifikatu pro komunikaci


        }


        #region Tady jednotlive ukladane prvky


        public static string Adresa
        {
            get
            {
                if (Config.Settings.isHTTPS)
                    return string.Format("https://{0}/", Config.Settings.Adresa_API);
                else
                    return string.Format("http://{0}/", Config.Settings.Adresa_API);
            }
        }

        /// <summary>
        /// Adresa
        /// </summary>
        public static string Adresa_API
        {
            get { return GetValue("Adresa_API", @"192.168.1.69/MST_Win_Kom_Server_7_Dasenka"); } //192.168.1.69/MST_Win_Kom_Server_7_Dasenka //192.168.1.121:8120
            set { SetValue("Adresa_API", value); }
        }

        

        public static bool isHTTPS
        {
            get { return bool.Parse(GetValue("isHTTPS", false.ToString())); }
            set { SetValue("isHTTPS", value.ToString()); }
        }

        /// <summary>
        /// Adresa
        /// </summary>
        public static int TimeOut
        {
            get { return int.Parse(GetValue("TimeOut", @"6000000")); }
            set { SetValue("TimeOut", value.ToString()); }
        }

        /// <summary>
        /// TerminalID
        /// </summary>
        public static byte TerminalID
        {
            get { return byte.Parse(GetValue("TerminalID", 25.ToString())); }
            set { SetValue("TerminalID", value.ToString()); }
        }

        #region skener radio button

        public static bool ONOFF_ZEBRA
        {
            get { return bool.Parse(GetValue("ONOFF_ZEBRA", false.ToString())); }
            set { SetValue("ONOFF_ZEBRA", value.ToString()); }
        }

        public static bool ONOFF_ZXing
        {
            get { return bool.Parse(GetValue("ONOFF_ZXing", true.ToString())); }
            set { SetValue("ONOFF_ZXing", value.ToString()); }
        }

        #endregion

        #region Komunikace HTTPs 

        public enum ServerAccessType
        {
            Anonymous,
            Credentials
        }

        public static ServerAccessType ServerAccess
        {
            get { return (ServerAccessType)Enum.Parse(typeof(ServerAccessType), GetValue("ServerAccess", ServerAccessType.Anonymous.ToString()), true); }
            set { SetValue("ServerAccess", value.ToString()); }
        }

        public static string ServerAccessUsername
        {
            get { return GetValue("serverAccessUsername", string.Empty); }
            set { SetValue("serverAccessUsername", value); }
        }

        public static string ServerAccessPassword
        {
            get { return GetValue("serverAccessPassword", string.Empty); }
            set { SetValue("serverAccessPassword", value); }
        }

        public static string ServerAccessDomain
        {
            get { return GetValue("serverAccessDomain", string.Empty); }
            set { SetValue("serverAccessDomain", value); }
        }

        public static bool ServerAccessPreauthenticate
        {
            get { return bool.Parse(GetValue("serverAccessPreauthenticate", false.ToString())); }
            set { SetValue("serverAccessPreauthenticate", value.ToString()); }
        }

        public static bool ServerAccessAllowRedirection
        {
            get { return bool.Parse(GetValue("serverAccessAllowRedirection", false.ToString())); }
            set { SetValue("serverAccessAllowRedirection", value.ToString()); }
        }

        public static bool ServerAccessAllowDecompression
        {
            get { return bool.Parse(GetValue("serverAccessAllowDecompression", false.ToString())); }
            set { SetValue("serverAccessAllowDecompression", value.ToString()); }
        }

        #region Certifikaty

        public enum ServerAccessCertificatesTrustType
        {
            OnlyInstalled,
            TrustAll,
            TrustQuery
        }


        public static ServerAccessCertificatesTrustType ServerAccessCertificateTrust
        {
            get 
            {
                var tmp = (ServerAccessCertificatesTrustType)Enum.Parse(typeof(ServerAccessCertificatesTrustType), GetValue("ServerAccessCertificatesTrust", ServerAccessCertificatesTrustType.TrustAll.ToString()), true);

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
                SetValue("ServerAccessCertificatesTrust", value.ToString());
                var _serverAccessCertificateTrust = value;
                
                switch (_serverAccessCertificateTrust)
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

        /// <summary>
        /// Obecny format desetinnych cisel, ktere se budou zobrazovat v aplikaci
        /// </summary>
        public static string UIFormatDesCisel
        {
            get { return GetValue("UIFormatDesCisel", "N"); }
            set { SetValue("UIFormatDesCisel", value); }
        }

        #endregion

        #region API

        public static string API_konstant
        {
            get { return GetValue("API_konstant", "api"); }
            set { SetValue("API_konstant", value); }
        }

        public static string API_Autorizace
        {
            get { return GetValue("API_Autorizace", "MDox"); }
            set { SetValue("API_Autorizace", value); }
        }

        #endregion

        public static void SetValue(string key, string val)
        {
            try
            {
                m_settings.Set(key, val);
            }
            catch
            {
                m_settings.Add(key, val);
            }
        }

        public static string GetValue(string key, string defalutValue)
        {
            try
            {


                string val = m_settings[key];
                if (val == null)
                    SetValue(key, defalutValue);

                return (val != null ? val : defalutValue);
            }
            catch
            {
                SetValue(key, defalutValue);
                return defalutValue;
            }
        }

        public static void Update()
        {
            FileStream fs = new FileStream(Classes.DataInfo_Static.SettingsXML, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read);
            XmlTextWriter tw = new XmlTextWriter(fs, System.Text.UTF8Encoding.UTF8);
            tw.Formatting = Formatting.Indented;
            tw.WriteStartDocument();
            tw.WriteStartElement("configuration");
            tw.WriteStartElement("appSettings");

            for (int i = 0; i < m_settings.Count; ++i)
            {
                tw.WriteStartElement("add");
                tw.WriteStartAttribute("key", string.Empty);
                tw.WriteRaw(m_settings.GetKey(i));
                tw.WriteEndAttribute();

                tw.WriteStartAttribute("value", string.Empty);
                tw.WriteRaw(m_settings.Get(i));
                tw.WriteEndAttribute();
                tw.WriteEndElement();
            }

            tw.WriteEndElement();
            tw.WriteEndElement();

            tw.Flush();
            tw.Close();
            tw.Dispose();
            tw = null;
        }

        private static void Create()
        {
            try
            {

                string s = string.Empty;
                bool b = false;

                byte TID = TerminalID;
                int TO = TimeOut;

                b = isHTTPS;
                s = Adresa_API;
                s = API_Autorizace;
                s = API_konstant;

                s = UIFormatDesCisel;

                ServerAccessCertificatesTrustType SACT = ServerAccessCertificateTrust;
                b = ServerAccessAllowDecompression;
                b = ServerAccessAllowRedirection;
                b = ServerAccessPreauthenticate;
                s = ServerAccessDomain;
                s = ServerAccessPassword;
                s = ServerAccessUsername;
                b = ServerAccessAllowDecompression;
                ServerAccessType SA = ServerAccess;

                b = ONOFF_ZXing;
                b = ONOFF_ZEBRA;

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }

        }
    }
}