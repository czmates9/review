using System;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Data;
using Fask.Logging;

namespace FASK.SledovaniVyroby.Module.Vyroba_SV.Vozikova_matice
{

    class VM_logika
    {


        public VM_logika()
        {
            string filename = Path.GetDirectoryName(Application.ExecutablePath);
            configfilename = Path.Combine(filename, cfilename);
          

            //nastaveni konfiguracniho souboru matic
            //Pokud nebyly nacteny zadne parametry, vlozim vychzoi (?)
            if (!ExistiFile())
            {
                config.Matice_stavy.AddMatice_stavyRow(true,false,false,false, "1");
                config.Matice_stavy.AddMatice_stavyRow(true, true, false, false, "2");
                config.Matice_stavy.AddMatice_stavyRow(true, true, true, false, "3");
                config.Matice_stavy.AddMatice_stavyRow(true, true, true, true, "4");
                config.Matice_stavy.AddMatice_stavyRow(false, false, true, false, "5");
                config.Matice_stavy.AddMatice_stavyRow(false, false, true, true, "6");
                //Ulozeni
                Save();
            }
            //Nacteni
            Load();

        }

        public static VM_oznaceni ParseEnum<VM_oznaceni>(string value)
        {
            return (VM_oznaceni)Enum.Parse(typeof(VM_oznaceni), value, true);
        }


        //zjisteni polohy voziku na zaklade sepnuti relatek L1-L4
        public ICommDatabase.DSVyroba.FASK_MachinesRow GetOznaceniVM(bool L1, bool L2, bool L3, bool L4)
        {
            try
            {
                //pokud bude kombinace L1-L4 dostanu oznaceni stavu voziku
                var pom_1 = config.Matice_stavy.Where(x =>  x.L1 == L1 & x.L2 == L2 & x.L3 == L3 & x.L4 == L4 ).ToList();

                if (pom_1.Count == 1)
                {

                    //VM_oznaceni MyStatus = (VM_oznaceni)Enum.Parse(typeof(VM_oznaceni), pom_1.First().Oznaceni, true);
                    var id = pom_1.First().ID_FASK_Machines;

                    ICommDatabase.DSVyroba data = Database.Classes.Vyroba_Local.getMachines(Configuration.AgroSledovaniVozikuConfig.config.CS[0].DB_Local);

                    var xx = data.FASK_Machines.Where(x => x.id.Trim() == id.Trim()).ToList();

                    if (xx.Count == 1)
                    {
                        return xx.First();
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                    return null;


            }
            catch(Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                return null;
            }

            
        }


        #region sprava souboru

        /// <summary>
        /// Nazvev konfiguracniho souboru
        /// </summary>
        private const string cfilename = "vyroba_agro_SV_config_matice.xml";

        /// <summary>
        /// Absolutni nazev konfiguracniho souboru
        /// </summary>
        private static string configfilename;
        public static string ConfigFileName
        {
            get { return configfilename; }
        }

        /// <summary>
        /// Tabulka s konfiguraci modulu
        /// </summary>
        private ConfigMatice config = new ConfigMatice();



        public bool ExistiFile()
        {
            if (File.Exists(configfilename))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Nacteni konfiguracniho xml souboru do vnitrnich tabulek
        /// </summary>
        public void Load()
        {
            try { config.ReadXml(ConfigFileName); }
            catch { /*Napr soubor jeste neexistuje*/ }
        }

        /// <summary>
        /// Ulozeni konfiguracniho xml souboru z vnitrnich tabulek
        /// </summary>
        public void Save()
        {
            config.WriteXml(ConfigFileName);
        }
        #endregion


    }


}
