using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Xml;
using System.Reflection;

namespace RFIDFactory
{
    public class RFIDFactory
    {
        private static string RFIDType;
        private static string RFIDDllPath;
        private static string RFIDInstance;

        private static string _FileName = @"Konfigurace\RFIDFactory.xml";

        public static IRFIDProvider.IRFIDProvider Init()
        {
            try
            {
                LoadConfiguration();

                Assembly ass = Assembly.LoadFrom(RFIDDllPath);

                Type[] typy = ass.GetTypes();
                foreach (Type t in typy)
                {
                    Type[] ifaces = t.GetInterfaces();
                    foreach (Type iface in ifaces)
                    {
                        if (iface == typeof(IRFIDProvider.IRFIDProvider))
                            return (IRFIDProvider.IRFIDProvider)ass.CreateInstance(t.FullName);
                    }
                }

                throw new Exception("RFID interface not found ...");
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }

        public static string GetScannerTypeName()
        {
            try
            {

                string FilePath;

                FilePath = (new Uri(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase), _FileName))).LocalPath;
                string pom;

                using (XmlReader reader = XmlReader.Create(FilePath))
                {
                    while (reader.Read())
                    {
                        // Only detect start elements.
                        if (reader.IsStartElement())
                        {
                            // Get element name and switch on it.
                            switch (reader.Name)
                            {
                                case "Scanner":
                                    pom = reader["Type"];
                                    if (pom != null)
                                        return pom;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }

                return "None";
                //    Scanner.EnableAllBarcodes();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return "None";
                //Fask.MST_W.Forms.MsgBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, Fask.MST_W.Forms.MsgBoxIcon.Warning);
                //Log.Write(ex.Message, "Aktivace scanneru");
            }
        }

        private static void LoadConfiguration()
        {
            string FilePath;
            string AssemblyDirectoryPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            FilePath = (new Uri(Path.Combine(AssemblyDirectoryPath, _FileName))).LocalPath;
            string pom;

            using (XmlReader reader = XmlReader.Create(FilePath))
            {
                while (reader.Read())
                {
                    // Only detect start elements.
                    if (reader.IsStartElement())
                    {
                        // Get element name and switch on it.
                        switch (reader.Name)
                        {
                            case "Scanner":
                                pom = reader["Path"];
                                if (pom != null)
                                    RFIDDllPath = (new Uri(Path.Combine(AssemblyDirectoryPath, pom))).LocalPath;
                                pom = reader["Type"];
                                if (pom != null)
                                    RFIDType = pom;
                                pom = reader["Instance"];
                                if (pom != null)
                                    RFIDInstance = pom;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }
    }
}
