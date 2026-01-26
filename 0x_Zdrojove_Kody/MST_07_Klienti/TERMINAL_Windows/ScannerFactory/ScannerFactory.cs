using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Reflection;

namespace FASK.MST_WINDOWS.ScannerFactory
{
    public class ScannerFactory
    {
        private static string scannerType;
        private static string scannerDllPath;
        private static string scannerInstance;

        public static FASK.MST_WINDOWS.IScannerProvider.IScannerProvider Init()
        {
            try
            {
                LoadConfiguration();

                Assembly ass = Assembly.LoadFrom(scannerDllPath);

                Type[] typy = ass.GetTypes();
                foreach (Type t in typy)
                {
                    Type[] ifaces = t.GetInterfaces();
                    foreach (Type iface in ifaces)
                    {
                        if (iface == typeof(FASK.MST_WINDOWS.IScannerProvider.IScannerProvider))
                            return (FASK.MST_WINDOWS.IScannerProvider.IScannerProvider)ass.CreateInstance(t.FullName);
                    }
                }

                throw new Exception("Scanner interface not found ...");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetScannerTypeName()
        {
            try
            {

                string FilePath;

                FilePath = (new Uri(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase), "ScannerFactory.xml"))).LocalPath;
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
                return "None";
                //Fask.MST_W.Forms.MsgBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, Fask.MST_W.Forms.MsgBoxIcon.Warning);
                //Log.Write(ex.Message, "Aktivace scanneru");
            }
        }

        private static void LoadConfiguration()
        {
            string FilePath;
            string AssemblyDirectoryPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            FilePath = (new Uri(Path.Combine(AssemblyDirectoryPath, "ScannerFactory.xml"))).LocalPath;
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
                                    scannerDllPath = (new Uri(Path.Combine(AssemblyDirectoryPath, pom))).LocalPath;
                                pom = reader["Type"];
                                if (pom != null)
                                    scannerType = pom;
                                pom = reader["Instance"];
                                if (pom != null)
                                    scannerInstance = pom;
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
