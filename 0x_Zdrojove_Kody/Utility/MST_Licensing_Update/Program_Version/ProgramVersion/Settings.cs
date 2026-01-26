using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using System.IO;
//using System.Xml;
using System.Collections.Specialized;
using Microsoft.Win32;

namespace ProgramVersion
{
   public class Settings
    {

        private static NameValueCollection m_settings;
        private static string m_settingsPath;



        #region Ctecka

        public static string LicenseCteckaPassword
        {
            get { return GetValue("LicenseCteckaPassword", "fask!pro159"); }
            set { SetValue("LicenseCteckaPassword", value); }
        }

        public static string LicenseCteckaNameFile
        {
            get { return GetValue("LicenseCteckaNameFile", "mst_w.ini"); }
            set { SetValue("LicenseCteckaNameFile", value); }
        }

        public static string LicenseCteckaPriponaFile
        {
            get { return GetValue("LicenseCteckaPriponaFile", "ini files (*.ini)|*.ini"); }
            set { SetValue("LicenseCteckaPriponaFile", value); }
        }

        public static DateTime LicenseCteckaExpiredDate
        {
            get { return DateTime.ParseExact(GetValue("LicenseCteckaExpiredDate", "01.01.2100"), "dd.MM.yyyy", null); }
            set { SetValue("LicenseCteckaExpiredDate", value.ToString("dd.MM.yyyy")); }
        } 
        #endregion

        public static string LicenseKonzolaPassword
        {
            get { return GetValue("LicenseKonzolaPassword", "fask!pro159"); }
            set { SetValue("LicenseKonzolaPassword", value); }
        }

        public static string LicenseKonzolaNameFile
        {
            get { return GetValue("LicenseKonzolaNameFile", "mst_w.ini"); }
            set { SetValue("LicenseKonzolaNameFile", value); }
        }

        public static string LicenseKonzolaPriponaFile
        {
            get { return GetValue("LicenseKonzolaPriponaFile", "ini files (*.ini)|*.ini"); }
            set { SetValue("LicenseKonzolaPriponaFile", value); }
        }

        public static DateTime LicenseKonzolaExpiredDate
        {
            get { return DateTime.ParseExact(GetValue("LicenseKonzolaExpiredDate", "01.01.2100"), "dd.MM.yyyy", null); }
            set { SetValue("LicenseKonzolaExpiredDate", value.ToString("dd.MM.yyyy")); }
        }

        static Settings()
       {
           m_settingsPath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "Settings.xml");
           
           
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
            tw.Indentation = 4;

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
    }
}
