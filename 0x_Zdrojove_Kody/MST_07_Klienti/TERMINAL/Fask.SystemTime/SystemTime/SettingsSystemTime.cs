using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Collections.Specialized;

namespace Fask.SystemTime
{
    public class SettingsSystemTime
    {
        private static NameValueCollection m_settings;
        private static string m_settingsPath;


        public static string Connection
        {
            get { return GetValue("Connection", string.Empty); }
            set { SetValue("Connection", value); }
        }


        public static SystemTime.ConfigurationSource Source
        {
            get { return (SystemTime.ConfigurationSource)Enum.Parse(typeof(SystemTime.ConfigurationSource), GetValue("Source", SystemTime.ConfigurationSource.None.ToString()), true); }
            set { SetValue("Source", value.ToString()); }
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
                return (val != null ? val : defalutValue);
            }
            catch 
            {
                return defalutValue;
            }
        }

        static SettingsSystemTime()
        {
            // Get the path of the settings file.

            m_settings = new NameValueCollection();
            m_settingsPath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase), "SettingsSystemTime.xml");

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
