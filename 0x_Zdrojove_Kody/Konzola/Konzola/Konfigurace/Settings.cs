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

namespace Konzola
{
    public class Settings
    { 

        /// <summary>
        /// Pouzity typ scanneru pro carove kody
        /// </summary>
        public static Fask.Vyroba_P.Scanner.ScannerTypes ScannerType
        {
            get { return (Fask.Vyroba_P.Scanner.ScannerTypes)Enum.Parse(typeof(Fask.Vyroba_P.Scanner.ScannerTypes), GetValue("ScannerType", Fask.Vyroba_P.Scanner.ScannerTypes.None.ToString()), true); }
            set { SetValue("ScannerType", value.ToString()); }
        }


        public static Point ApplicationPosition
        {
            get
            {
                try
                {
                    return (Point)(new PointConverter()).ConvertFromString(GetValue("ApplicationPosition", "10; 10"));
                }
                catch (Exception)
                {
                    return new Point(0, 0);
                }
            }
            set
            {
                try
                {
                    SetValue("ApplicationPosition", (new PointConverter()).ConvertToString(value));
                }
                catch (Exception)
                {
                }
            }
        }

        public static bool SkladyTransakceVydejPredlohaKontrolaPreplneni
        {
            get { return bool.Parse(GetValue("SkladyTransakceVydejPredlohaKontrolaPreplneni", true.ToString())); }
            set { SetValue("SkladyTransakceVydejPredlohaKontrolaPreplneni", value.ToString()); }
        }
      
        public static bool SkladyTransakceInventuraPredlohaAlternaceCarKody 
        {
            get { return bool.Parse(GetValue("SkladyTransakceInventuraPredlohaAlternaceCarKody", true.ToString())); }
            set { SetValue("SkladyTransakceInventuraPredlohaAlternaceCarKody", value.ToString()); }
        }

        public static string SkladyTransakceInventuraINVPredloha_Filter
        {
            get { return GetValue("SkladyTransakceInventuraINVPredloha_Filter", string.Empty); }
            set { SetValue("SkladyTransakceInventuraINVPredloha_Filter", value.ToString()); }
        }

        public static bool FormSkladStavLokaceListVypocetStavuDlePolozky
        {
            get { return bool.Parse(GetValue("FormSkladStavLokaceListVypocetStavuDlePolozky", true.ToString())); }
            set { SetValue("FormSkladStavLokaceListVypocetStavuDlePolozky", value.ToString()); }
        }

        public static bool FormInventuraPredlohaListVypocetStavuDlePolozky
        {
            get { return bool.Parse(GetValue("FormInventuraPredlohaListVypocetStavuDlePolozky", true.ToString())); }
            set { SetValue("FormInventuraPredlohaListVypocetStavuDlePolozky", value.ToString()); }
        }

        public static bool SkladyCiselnikPracovnici_synchronizaceAD
        {
            get { return bool.Parse(GetValue("SkladyCiselnikPracovnici_synchronizaceAD", true.ToString())); }
            set { SetValue("SkladyCiselnikPracovnici_synchronizaceAD", value.ToString()); }
        }

        public static string VydejDavkyTemplate
        {
            get { return GetValue("VydejDavkyTemplate", "Test.rdlc"); }
            set { SetValue("VydejDavkyTemplate", value); }
        }

        public static string Vydej_Param_Dodavatel_Firma
        {
            get { return GetValue("Vydej_Param_Dodavatel_Firma", "-"); }
            set { SetValue("Vydej_Param_Dodavatel_Firma", value); }
        }

        public static string Vydej_Param_Dodavatel_Adresa
        {
            get { return GetValue("Vydej_Param_Dodavatel_Adresa", "-"); }
            set { SetValue("Vydej_Param_Dodavatel_Adresa", value); }
        }

        public static string Vydej_Param_Dodavatel_PSC
        {
            get { return GetValue("Vydej_Param_Dodavatel_PSC", "-"); }
            set { SetValue("Vydej_Param_Dodavatel_PSC", value); }
        }

        public static string Vydej_Param_Dodavatel_Obec
        {
            get { return GetValue("Vydej_Param_Dodavatel_Obec", "-"); }
            set { SetValue("Vydej_Param_Dodavatel_Obec", value); }
        }

        public static bool Prodej_ImportDokladuGrupuj
        {
            get { return bool.Parse(GetValue("Prodej_ImportDokladuGrupuj", false.ToString())); }
            set { SetValue("Prodej_ImportDokladuGrupuj", value.ToString()); }
        }


        public static string ExportyExcelFileName
        {
            get { return GetValue("ExportyExcelFileName", "Export.xlsx"); }
            set { SetValue("ExportyExcelFileName", value.ToString()); }
        }

        public static string ExportyCSVFileName
        {
            get { return GetValue("ExportyCSVFileName", "Export.csv"); }
            set { SetValue("ExportyCSVFileName", value.ToString()); }
        }

        public static string ExportyXMLFileName
        {
            get { return GetValue("ExportyXMLFileName", "Export.xml"); }
            set { SetValue("ExportyXMLFileName", value.ToString()); }
        }

        public static string ImportPath
        {
            get { return GetValue("ImportPath", @"C:\"); }
            set { SetValue("ImportPath", value.ToString()); }
        }
  
        public static int ServisFormVazbyOkruhZdrojSeznamListSplitterDistance
        {
            get { return int.Parse(GetValue("ServisFormVazbyOkruhZdrojSeznamListSplitterDistance", "300")); }
            set { SetValue("ServisFormVazbyOkruhZdrojSeznamListSplitterDistance", value.ToString()); }
        }

        public static int ServisFormVazbyStavStavNextListSplitterDistance
        {
            get { return int.Parse(GetValue("ServisFormVazbyStavStavNextListSplitterDistance", "300")); }
            set { SetValue("ServisFormVazbyStavStavNextListSplitterDistance", value.ToString()); }
        }

        public static int ServisFormVazbyDynTabDefDynTabListSplitterDistance
        {
            get { return int.Parse(GetValue("ServisFormVazbyDynTabDefDynTabListSplitterDistance", "300")); }
            set { SetValue("ServisFormVazbyDynTabDefDynTabListSplitterDistance", value.ToString()); }
        }

        public static int ServisFormVazbyCinnostCinnostNextListSplitterDistance
        {
            get { return int.Parse(GetValue("ServisFormVazbyCinnostCinnostNextListSplitterDistance", "300")); }
            set { SetValue("ServisFormVazbyCinnostCinnostNextListSplitterDistance", value.ToString()); }
        }

        public static int SkladyFormSkladStavLokaceListSplitterDistance
        {
            get { return int.Parse(GetValue("SkladyFormSkladStavLokaceListSplitterDistance", "300")); }
            set { SetValue("SkladyFormSkladStavLokaceListSplitterDistance", value.ToString()); }
        }

        public static int ProgressIndicatorSize
        {
            get { return int.Parse(GetValue("ProgressIndicatorSize", "40")); }
            set { SetValue("ProgressIndicatorSize", value.ToString()); }
        }

        public static int FormUkolyUzivListSplitterDistance
        {
            get { return int.Parse(GetValue("FormUkolyUzivListSplitterDistance", "300")); }
            set { SetValue("FormUkolyUzivListSplitterDistance", value.ToString()); }
        }

        public static string UIFormatDesCisel
        {
            get { return GetValue("UIFormatDesCisel", "N"); }
            set { SetValue("UIFormatDesCisel", value); }
        }

        public static int VazbyMaterialySplitPoloha
        {
            get { return int.Parse(GetValue("VazbyMaterialySplitPoloha", "300")); }
            set { SetValue("VazbyMaterialySplitPoloha", value.ToString()); }
        }

        public static int Vazby_P_PS_SplitPoloha
        {
            get { return int.Parse(GetValue("Vazby_P_PS_SplitPoloha", "300")); }
            set { SetValue("Vazby_P_PS_SplitPoloha", value.ToString()); }
        }

        public static int FormLokaceMapaGenerovaniLokaci_N
        {
            get { return int.Parse(GetValue("FormLokaceMapaGenerovaniLokaci_N", "3")); }
            set { SetValue("FormLokaceMapaGenerovaniLokaci_N", value.ToString()); }
        }

        public static void SetValue(string key, string val)
        {

        }

        public static string GetValue(string key, string defalutValue)
        {
            try
            {
                return  defalutValue;
            }
            catch
            {
                SetValue(key, defalutValue);
                return defalutValue;
            }
        }

        
    }
}
