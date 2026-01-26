using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;



namespace Konzola
{
    public class NapovedaClass
    {
        public static bool NapovedacTor(string FileName, out Dictionary<string, string> list)
        {
            // Get the path of the settings file.
            try
            {
                string m_NapovedaPath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),"Napoveda", FileName);
                list = new Dictionary<string, string>();

                if (File.Exists(m_NapovedaPath))
                {
                    XmlDocument xdoc = new XmlDocument();
                    xdoc.Load(m_NapovedaPath);
                    XmlElement root = xdoc.DocumentElement;
                    foreach (XmlNode node in root.SelectNodes("/Napoveda/add"))
                    {
                        list.Add(node.Attributes["key"].Value, node.Attributes["value"].Value);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }


    }
}
