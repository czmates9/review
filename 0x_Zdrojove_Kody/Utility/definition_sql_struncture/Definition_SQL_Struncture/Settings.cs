using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Collections.Specialized;
using Microsoft.Win32;
using System.Globalization;
using System.Drawing;

namespace Definition_SQL_Struncture
{
    public class Settings
    {
        private static NameValueCollection m_settings;
        private static string m_settingsPath;

        public static string LastPath
        {
            get { return GetValue("LastPath", System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)); }
            set { SetValue("LastPath", value.ToString()); }

        }

        public static string ConnectionString
        {
            get { return GetValue("ConnectionString", string.Empty); }
            set { SetValue("ConnectionString", value.ToString()); }

        }

        


        #region Metody pro Settings

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
