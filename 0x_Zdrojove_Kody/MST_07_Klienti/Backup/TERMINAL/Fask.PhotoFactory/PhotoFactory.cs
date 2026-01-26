using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;
using System.Xml;

namespace Fask.PhotoFactory
{
    public class PhotoFactory
    {
        private static string photoType;
        private static string photoDllPath;

        /// <summary>
        /// Inicialize providera pro foceni.
        /// </summary>
        /// <returns>Zvoleny provider (pokud neni nalezen, pouzije se PhotoProviderBase</returns>
        public static Fask.PhotoProvider.IPhotoProvider Init()
        {
            try
            {
                LoadConfiguration();

                Assembly ass;
                Type[] typy;
                if (!string.IsNullOrEmpty(photoDllPath))
                {
                    ass = Assembly.LoadFrom(photoDllPath);

                    typy = ass.GetTypes();
                    foreach (Type t in typy)
                    {
                        Type[] ifaces = t.GetInterfaces();
                        foreach (Type iface in ifaces)
                        {
                            if (iface == typeof(Fask.PhotoProvider.IPhotoProvider))
                                return (Fask.PhotoProvider.IPhotoProvider)ass.CreateInstance(t.FullName);
                        }
                    }
                }

                // najiti base provideru (defaultniho)
                // TODO: Zalogovat nenastaveneho providera??
                string AssemblyDirectoryPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
                string dllpath = (new Uri(Path.Combine(AssemblyDirectoryPath, "Fask.PhotoProviderBase.dll"))).LocalPath;
                photoType = "Base";
                ass = Assembly.LoadFrom(dllpath);
                typy = ass.GetTypes();
                foreach (Type t in typy)
                {
                    Type[] ifaces = t.GetInterfaces();
                    foreach (Type iface in ifaces)
                    {
                        if (iface == typeof(Fask.PhotoProvider.IPhotoProvider))
                            return (Fask.PhotoProvider.IPhotoProvider)ass.CreateInstance(t.FullName);
                    }
                }

                throw new Exception("Photo interface not found ...");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public static string GetPhotoTypeName()
        //{
        //    try
        //    {

        //        string FilePath;

        //        FilePath = (new Uri(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase), "ScannerFactory.xml"))).LocalPath;
        //        string pom;

        //        using (XmlReader reader = XmlReader.Create(FilePath))
        //        {
        //            while (reader.Read())
        //            {
        //                // Only detect start elements.
        //                if (reader.IsStartElement())
        //                {
        //                    // Get element name and switch on it.
        //                    switch (reader.Name)
        //                    {
        //                        case "Photo":
        //                            pom = reader["Type"];
        //                            if (pom != null)
        //                                return pom;
        //                            break;
        //                        default:
        //                            break;
        //                    }
        //                }
        //            }
        //        }

        //        return "None";
        //    }
        //    catch
        //    {
        //        return "None";
        //    }
        //}

        private static void LoadConfiguration()
        {
            string FilePath;
            string AssemblyDirectoryPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            FilePath = (new Uri(Path.Combine(AssemblyDirectoryPath, "PhotoFactory.xml"))).LocalPath;
            string pom;

            // soubor neexistuje, nastavi se defaultni provider
            if (!File.Exists(FilePath))
                return;

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
                            case "Photo":
                                pom = reader["Path"];
                                if (pom != null)
                                    photoDllPath = (new Uri(Path.Combine(AssemblyDirectoryPath, pom))).LocalPath;
                                pom = reader["Type"];
                                if (pom != null)
                                    photoType = pom;
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

