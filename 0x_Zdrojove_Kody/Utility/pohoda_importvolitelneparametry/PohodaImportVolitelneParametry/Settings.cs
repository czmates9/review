using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PohodaImportVolitelneParametry
{
    public class Settings
    {
        private static NameValueCollection m_settings;
        private static string m_settingsPath;

        #region Parametry

        #region Skladane Paraetry co nejdu v XML

        public static string PathToINIFile
        {
            get { return Path.Combine(DirPath, NameINIFile); }
        }

        public static string PathToInputDirectory
        {
            get { return Path.Combine(DirPath, "Export"); }
        }

        public static string DirPath
        {
            get { return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location); }
        }

        #endregion

        #region Connection string čast

        //Provider=SQLNCLI10;
        //Data Source = 192.168.1.114\sql_2014;
        //Initial Catalog = StwPh_04535510_2017;
        //User ID = sa; 
        //Password=sasa

        public static string ConnectionStringPohoda
        {
            get { return string.Format("Provider={0};Data Source={1};Initial Catalog={2};User ID={3};Password={4}", Settings.Provider, Settings.Data_Source,Settings.Catalog, Settings.SQL_Login, Settings.SQL_Heslo); }
        }

        public static string Catalog
        {
            get { return GetValue("Catalog", string.Empty); }
            set { SetValue("Catalog", value); }
        }

        public static string Provider
        {
            get { return GetValue("Provider", string.Empty); }
            set { SetValue("Provider", value); }
        }

        public static string Data_Source
        {
            get { return GetValue("Data_Source", string.Empty); }
            set { SetValue("Data_Source", value); }
        }

        public static string SQL_Heslo
        {
            //get { return GetValue("SQL_Heslo", string.Empty); }
            //set { SetValue("SQL_Heslo", value); }

            get
            {
                string default_encrypted_konfiguraceheslo = Fask.Encryption.RijndaelWrapper.Encrypt(Encoding.ASCII.GetString(new byte[] { 49, 53, 57 }), Globals.encryptPassword);
                string encrypt_cs = GetValue("SQL_Heslo", default_encrypted_konfiguraceheslo);

                try
                {
                    return Fask.Encryption.RijndaelWrapper.Decrypt(encrypt_cs, Globals.encryptPassword);
                }
                catch (System.Security.Cryptography.CryptographicException ex)
                {
                    return null;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            set
            {
                string encrypt_cs = Fask.Encryption.RijndaelWrapper.Encrypt(value, Globals.encryptPassword);
                SetValue("SQL_Heslo", encrypt_cs);
            }
        }

        public static string SQL_Login
        {
            get { return GetValue("SQL_Login", string.Empty); }
            set { SetValue("SQL_Login", value); }
        }

        #endregion

        public static string Pohoda_Login
        {
            get { return GetValue("Pohoda_Login", string.Empty); }
            set { SetValue("Pohoda_Login", value); }
        }

        public static string Pohoda_Heslo
        {
            //get { return GetValue("Pohoda_Heslo", string.Empty); }
            //set { SetValue("Pohoda_Heslo", value); }
            get
            {
                string default_encrypted_konfiguraceheslo = Fask.Encryption.RijndaelWrapper.Encrypt(Encoding.ASCII.GetString(new byte[] { 49, 53, 57 }), Globals.encryptPassword);
                string encrypt_cs = GetValue("Pohoda_Heslo", default_encrypted_konfiguraceheslo);

                try
                {
                    return Fask.Encryption.RijndaelWrapper.Decrypt(encrypt_cs, Globals.encryptPassword);
                }
                catch (System.Security.Cryptography.CryptographicException ex)
                {
                    return null;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            set
            {
                string encrypt_cs = Fask.Encryption.RijndaelWrapper.Encrypt(value, Globals.encryptPassword);
                SetValue("Pohoda_Heslo", encrypt_cs);
            }
        }

        public static string NameINIFile
        {
            get { return GetValue("NameINIFile", string.Empty); }
            set { SetValue("NameINIFile", value); }
        }

        public static string ICO
        {
            get { return GetValue("ICO", string.Empty); }
            set { SetValue("ICO", value); }
        }

        public static string PathToPohodaEXE
        {
            get { return GetValue("PathToPohodaEXE", string.Empty); }
            set { SetValue("PathToPohodaEXE", value); }
        }

        public static bool Process_UseShellExecute
        {
            get { return bool.Parse(GetValue("Process_UseShellExecute", false.ToString())); }
            set { SetValue("Process_UseShellExecute", value.ToString()); }
        }

        #region Mapovani cesty

        public static string Communicator_Drive_Mapping_Letter
        {
            get { return GetValue("Communicator_Drive_Mapping_Letter", string.Empty); }
            set { SetValue("Communicator_Drive_Mapping_Letter", value); }
        }
        public static string Comunicator_Drive_Mapping_UNCPath
        {
            get { return GetValue("Comunicator_Drive_Mapping_UNCPath", string.Empty); }
            set { SetValue("Comunicator_Drive_Mapping_UNCPath", value); }
        }
        public static string Comunicator_Drive_Mapping_Domain
        {
            get { return GetValue("Comunicator_Drive_Mapping_Domain", string.Empty); }
            set { SetValue("Comunicator_Drive_Mapping_Domain", value); }
        }
        public static string Comunicator_Drive_Mapping_User
        {
            get { return GetValue("Comunicator_Drive_Mapping_User", string.Empty); }
            set { SetValue("Comunicator_Drive_Mapping_User", value); }
        }
        public static string Comunicator_Drive_Mapping_Password
        {
            get
            {
                string default_encrypted_konfiguraceheslo = Fask.Encryption.RijndaelWrapper.Encrypt(Encoding.ASCII.GetString(new byte[] { 49, 53, 57 }), Globals.encryptPassword);
                string encrypt_cs = GetValue("Comunicator_Drive_Mapping_Password", default_encrypted_konfiguraceheslo);

                try
                {
                    return Fask.Encryption.RijndaelWrapper.Decrypt(encrypt_cs, Globals.encryptPassword);
                }
                catch (System.Security.Cryptography.CryptographicException ex)
                {
                    return null;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            set
            {
                string encrypt_cs = Fask.Encryption.RijndaelWrapper.Encrypt(value, Globals.encryptPassword);
                SetValue("Comunicator_Drive_Mapping_Password", encrypt_cs);
            }
        }

        #endregion

        #endregion

        #region Metody pro praci

        static Settings()
        {
            // Get the path of the settings file.
            m_settingsPath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Settings.xml");
            m_settings = new NameValueCollection();

            if (File.Exists(m_settingsPath))
            {
                System.Xml.XmlDocument xdoc = new XmlDocument();
                xdoc.Load(m_settingsPath);
                XmlElement root = xdoc.DocumentElement;
                foreach (XmlNode node in root.SelectNodes("/configuration/appSettings/add"))
                {
                    m_settings.Add(node.Attributes["key"].Value, node.Attributes["value"].Value);
                }
            }
            else
            {
                using (FileStream fs = System.IO.File.Create(m_settingsPath))
                {

                }

                Settings.NameINIFile = "xml_imp.ini";
                Settings.Catalog = "StwPh_12345678_2019";
                Settings.ICO = "12345678";
                Settings.Data_Source = @"192.168.1.121\SQLEXPRESS";
                Settings.Pohoda_Login = "Admin";
                Settings.SQL_Login = "sa";
                Settings.PathToPohodaEXE = @"\\192.168.1.121\pohoda_e1\Pohoda.exe";
                Settings.Provider = "SQLNCLI11";

                Settings.SQL_Heslo = string.Empty;
                Settings.Pohoda_Heslo = string.Empty;
                Settings.Comunicator_Drive_Mapping_Password = string.Empty;



                Settings.Update();

            }
        }

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
            XmlTextWriter tw = new XmlTextWriter(
                m_settingsPath,
                System.Text.UTF8Encoding.UTF8
                );
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

            tw.Close();
        }

        #endregion


    }
}
