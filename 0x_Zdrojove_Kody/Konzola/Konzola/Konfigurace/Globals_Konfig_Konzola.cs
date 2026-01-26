using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konzola.Konfigurace
{

    public class Globals_Konfig_Konzola
    {
        private static string _configFilePath = "MST_Konfig_Konzola.xml";


        /// <summary>
        /// Jmeno konfiguracniho souboru
        /// </summary>
        public static string ConfigFilePath { get { return _configFilePath; } set { _configFilePath = value; } }


        public static Konf Konfigurace;


        public static string LoadConfiguration()
        {
            return LoadConfiguration(Globals_Konfig_Konzola.ConfigFilePath);
        }


        public static string SaveConfiguration()
        {
            return SaveConfiguration(Globals_Konfig_Konzola.ConfigFilePath);
        }

        private static string LoadConfiguration(string FileName)
        {
            try
            {

                string FilePath = GetPath(FileName);


                if (!File.Exists(FilePath))
                {
                    //Soubor neexistuje, tak vytvořit..
                    Globals_Konfig_Konzola.CreateFile(FilePath);
                }


                if (Konfigurace == null)
                    Konfigurace = new Konf();

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

                Globals_Konfig_Konzola.LoadConfiguration();

                return "OK";
            }
            catch (Exception ex)
            { return ex.Message; }

        }

        private static void CreateFile(string FilePath)
        {

            try
            {

                Konf ds = new Konf();

                ds.Ukolovani.AddUkolovaniRow(
                    false,
                    false,
                    true,
                    true,
                    true
                    );

                ds.System.AddSystemRow(
                    byte.Parse(Properties.Resources.TerminalID),
                    false,
                    false,
                    "159",
                    false,
                    true,
                    @"Data Source=192.168.1.121\SQLEXPRESS;Initial Catalog=Agro_fask;User ID=LOGIN;Password=HESLO",
                    true
                    );

                ds.Provider.AddProviderRow(
                    "Fask.ModulePohodaXML.dll",
                    "Fask.BarCodeGraphics.ZPL_Zxing.dll",
                    "Fask.Module.Print.GS1.dll"
                    );

                ds.Planovani.AddPlanovaniRow(
                    true,
                    true,
                    true,
                    true,
                    string.Empty,
                    string.Empty,
                    true,
                    true,
                    false
                    );

                ds.VNC.AddVNCRow("Format16bppRgb555");

                ds.Vyroba.AddVyrobaRow(
                    true,
                    true,
                    "Vazba_P_PS",
                    "VyrobaPaletovylistek.zpl",
                    true,
                    false
                    );

                ds.Vyroba_Ciselniky.AddVyroba_CiselnikyRow(
                    true,
                    true,
                    true,
                    true,
                    true,
                    true,
                    true
                    );

                ds.Vyroba_Rozbory.AddVyroba_RozboryRow(
                    true,
                    true,
                    true,
                    true,
                    true,
                    true,
                    true,
                    true,
                    string.Empty,
                    false
                    );

                ds.Vyroba_Transakce.AddVyroba_TransakceRow(
                    true,
                    true,
                    true,
                    true,
                    true,
                    false
                    );

                ds.IT_Cast.AddIT_CastRow(
                    true,
                    true,
                    true,
                    true,
                    true,
                    true,
                    true,
                    true,
                    true
                    );

                ds.StavSkladu.AddStavSkladuRow(
                    true,
                    true,
                    true
                    );

                ds.Sklady.AddSkladyRow(
                    true,
                    true,
                    true,
                    true,
                    "fask_view_pohyby_aktualni",
                    "fask_view_pohyby_archivni",
                    "fask_view_pohyby_aktualni_a_archivni"
                    );

                ds.Sklady_Ciselniky.AddSklady_CiselnikyRow(
                    true,
                    true,
                    true,
                    true,
                    true,
                    true,
                    true,
                    true,
                    true
                    );

                ds.Sklady_Transakce.AddSklady_TransakceRow(
                    true,
                    true,
                    true,
                    true,
                    true,
                    true,
                    true,
                    true,
                    true,
                    true,
                    true,
                    true,
                    true,
                    true,
                    true,
                    true,
                    true,
                    true,
                    true,
                    true,
                    true,
                    true,
                    true
                    );

                ds.Sklady_Rozbory.AddSklady_RozboryRow(
                    true,
                    true,
                    true,
                    true,
                    true,
                    true,
                    true,
                    string.Empty
                    );

                ds.Sklady_Importni_mustek.AddSklady_Importni_mustekRow(
                 true
                 );

                ds.Sklady_RFID.AddSklady_RFIDRow(
                    "FF",
                    "COM4",
                    "_57600bps",
                    false
                    );

                ds.Servis.AddServisRow(
                    true,
                    true,
                    true,
                    true,
                    true,
                    true,
                    true,
                    true,
                    true,
                    @"C:\FASK\ImagesDataFileDirectory",
                    true
                    );

                ds.Servis_Vazby.AddServis_VazbyRow(
                    true,
                    true,
                    true,
                    true,
                    true
                    );

                ds.Export.AddExportRow(
                    false,
                    "d.M.yyyy H:m.s",
                    @"C:\",
                    "Export.xlsx",
                    "Export.csv",
                    "Export.xml"
                    );

                ds.Tisk.AddTiskRow(
                    false,
                    false,
                    @"C:\",
                    @"ZP",
                    @"RD",
                    @"ZP",
                    @"ZP",
                    @"RD",
                    @"",
                    @""
                    );

                ds.Vyroba_Ostatni.AddVyroba_OstatniRow(
                    1000,
                    0,
                    1000,
                    "Report_VyrobnyPrikaz.rdlc",
                    "Report_PlanovaniVyroby.rdlc",
                    "FASK",
                    "Za humnama",
                    "666 00",
                    "Brno City",
                    300,
                    300,
                    3
                    );

                ds.Sklady_Ostatni.AddSklady_OstatniRow(
                    Fask.Interfaces.Classes.ZOBRAZENI_DAT.Aktualni.ToString(),
                    true,
                    false,
                    300,
                    3
                    );


                ds.Sklady_Vydej.AddSklady_VydejRow(
                    true,
                    "Vydej_PrikazVychystani_I_Tec.rdlc",
                    "I-Tec",
                    "Kalvodova 2/1087",
                    "709 00",
                    "Ostrava 9"
                    );

                ds.Ostatni.AddOstatniRow(
                    "COM",
                    "10; 10",
                    40,
                    "N"
                    );

                ds.Sklady_Inventura.AddSklady_InventuraRow(
                    true,
                    string.Empty,
                    true,
                    @"C:\"
                    );

                ds.Servis_Ostatni.AddServis_OstatniRow(
                    300,
                    300,
                    300,
                    300
                    );

                ds.Planovani_VV_Params.AddPlanovani_VV_ParamsRow(",","ks,pár");

                ds.Sklady_Inventura_Tisk.AddSklady_Inventura_TiskRow(
                    "Report_InventuraCompare.rdlc",
                    "FASK",
                    "Za humnama",
                    "666 00",
                    "Brno City"
                    );

                ds.Sklady_Transakce_VolnyPohyb_Nasnimane.AddSklady_Transakce_VolnyPohyb_NasnimaneRow(
                    "StwPh",
                    " $SERLTNUM$;{TAB};$REZ_1$;{TAB}",
                    ";",
                    200
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


        #region Parametry s parsovanim


        public static Fask.Vyroba_P.Scanner.ScannerTypes ScannerType
        {
            get
            {
                string scan =  Konfigurace.Ostatni[0].ScannerType;
                
                if (string.IsNullOrEmpty(scan))
                    return Fask.Vyroba_P.Scanner.ScannerTypes.None;
                else
                    return (Fask.Vyroba_P.Scanner.ScannerTypes)Enum.Parse(typeof(Fask.Vyroba_P.Scanner.ScannerTypes), scan, true);

            }
            set
            {
                Konfigurace.Ostatni[0].ScannerType = value.ToString();
                SaveConfiguration();
            }
        }


        public static Fask.Interfaces.Classes.ZOBRAZENI_DAT FormSkladPohybListZobrazeniDat
        {
            get 
            {
                string fs = Konfigurace.Sklady_Ostatni[0].FormSkladPohybListZobrazeniDat;

                if (string.IsNullOrEmpty(fs))
                    return Fask.Interfaces.Classes.ZOBRAZENI_DAT.Aktualni;
                else
                    return (Fask.Interfaces.Classes.ZOBRAZENI_DAT)Enum.Parse(typeof(Fask.Interfaces.Classes.ZOBRAZENI_DAT), fs, true); 
            }
            set 
            {
                Konfigurace.Sklady_Ostatni[0].FormSkladPohybListZobrazeniDat = value.ToString();
                SaveConfiguration();
            }
        }

        public static Point ApplicationPosition
        {
            get
            {
                try
                {

                    string point = Konfigurace.Ostatni[0].ApplicationPosition;

                    if (string.IsNullOrEmpty(point))
                        return new Point(0, 0);
                    else
                        return (Point)(new PointConverter()).ConvertFromString(point);

                }
                catch (System.Exception )
                {
                    return new Point(0, 0);
                }
            }
            set
            {
                
                Konfigurace.Ostatni[0].ApplicationPosition = (new PointConverter()).ConvertToString(value);
                SaveConfiguration();
            }
        }

        #endregion


    }
}
