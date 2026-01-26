using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konzola.Konfigurace_Tisky
{

    public class Globals_Konfig_Tisk
    {
        private static string _configFilePath = "MST_Konfig_Tisk.xml";


        /// <summary>
        /// Jmeno konfiguracniho souboru
        /// </summary>
        public static string ConfigFilePath { get { return _configFilePath; } set { _configFilePath = value; } }


        public static Konf_T Konfigurace;


        public static string LoadConfiguration()
        {
            return LoadConfiguration(Globals_Konfig_Tisk.ConfigFilePath);
        }


        public static string SaveConfiguration()
        {
            return SaveConfiguration(Globals_Konfig_Tisk.ConfigFilePath);
        }

        private static string LoadConfiguration(string FileName)
        {
            try
            {

                string FilePath = GetPath(FileName);


                if (!File.Exists(FilePath))
                {
                    //Soubor neexistuje, tak vytvořit..
                    Globals_Konfig_Tisk.CreateFile(FilePath);
                }


                if (Konfigurace == null)
                    Konfigurace = new Konf_T();

                Konfigurace.Clear();
                Konfigurace.ReadXml(FilePath);

                //Verifikace, zda je tam iba jeden řadek v každe tabulce, inak logovat

                return "OK";
            }
            catch (Exception ex)
            { return ex.Message; }
        }

        private static string SaveConfiguration(string FileName)
        {
            try
            {

                string FilePath = GetPath(FileName);

                //if (File.Exists(FilePath))
                //    File.Delete(FilePath);
               
                Konfigurace.WriteXml(FilePath);

                Globals_Konfig_Tisk.LoadConfiguration();

                return "OK";
            }
            catch (Exception ex)
            { return ex.Message; }

        }

        private static void CreateFile(string FilePath)
        {

            try
            {

                Konf_T ds = new Konf_T();

                ds.Params.AddParamsRow(
                    "Zebra",
                    "Nazev Tiskarny na serveru",
                    false
                    );

                ds.AcceptChanges();

                ds.WriteXml(FilePath);

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private static string GetPath(string FileName)
        {
            string FilePath = string.Empty;

            try
            {
                FilePath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), @"Konfigurace\" + FileName);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }

            return FilePath;
        }
    }
}
