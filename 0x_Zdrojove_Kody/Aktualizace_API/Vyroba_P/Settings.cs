using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Collections.Specialized;
using Microsoft.Win32;
using System.Globalization;
using System.Drawing;

namespace Fask.Aktualizace_API
{
    public class Settings
    {

        #region Eventy ID

        //private string UEventSmenaLogin_INFO = "1";
        //private string UEventSmenaLogout_INFO = "2";
        //private string UEventPracovnikPrihlaseni_INFO = "3";
        //private string UEventPracovnikOdhlaseni_INFO = "4";
        //private string UEventZruseniOdvod_INFO = "6";
        //private string UEventZruseniPriprava_INFO = "7";

        #endregion

        private static NameValueCollection m_settings;
        private static string m_settingsPath;


        #region Adresa

        public static string WebServiceAddressVyroba
        {
            get
            {
                if (Settings.isHTTPS)
                    return string.Format("https://{0}/", Settings.Adresa_API);
                else
                    return string.Format("http://{0}/", Settings.Adresa_API);
            }
        }

        /// <summary>
        /// Pøipojovaci øetìzec do databáze
        /// </summary>
        public static string Connection_DB
        {
            get { return GetValue("Connection_DB", string.Empty); }
            set { SetValue("Connection_DB", value); }
        }

     


        /// <summary>
        /// Adresa web serveru, ktery terminal vyuziva
        /// </summary>
        public static string Adresa_API
        {
            get { return GetValue("Adresa_API", Properties.Resources.WebServiceAddressVyroba); }
            set { SetValue("Adresa_API", value); }
        }

        public static string Autorizace_API
        {
            get { return GetValue("Autorizace_API", "MDox"); }
            set { SetValue("Autorizace_API", value); }
        }

        public static string API_konstant
        {
            get { return GetValue("API_konstant", "api"); }
            set { SetValue("API_konstant", value); }
        }

        public static bool isHTTPS
        {
            get { return bool.Parse(GetValue("isHTTPS", false.ToString())); }
            set { SetValue("isHTTPS", value.ToString()); }
        }

        /// <summary>
        /// Web service timeout
        /// </summary>
        public static int TimeOut
        {
            get { return int.Parse(GetValue("TimeOut", Properties.Resources.WebServiceTimeOut)); }
            set { SetValue("TimeOut", value.ToString()); }
        }

        #endregion


        public static int FormMainTimeFormatWidth
        {
            get { return int.Parse(GetValue("FormMainTimeFormatWidth", 100.ToString())); }
            set { SetValue("FormMainTimeFormatWidth", value.ToString()); }
        }
        
        public static string FormMainTimeFormat
        {
            get { return GetValue("FormMainTimeFormat", "HH:mm:ss"); }
            set { SetValue("FormMainTimeFormat", value); }
        }
        

        public static Point ApplicationPosition
        {
            get {
                try
                {
                    return (Point)(new PointConverter()).ConvertFromString(GetValue("ApplicationPosition", "0; 0"));
                }
                catch (Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle("Settings", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                    return new Point(0, 0);
                }
            }
            set
            {
                try
                {
                    SetValue("ApplicationPosition", (new PointConverter()).ConvertToString(value));
                }
                catch (Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle("Settings", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                }
            }
        }

        /// <summary>
        /// ID prednastaveneho stroje
        /// </summary>
        //public static int? MachineID
        //{
        //    get
        //    {
        //        int machineid;
        //        if (!int.TryParse(GetValue("MachineID", string.Empty), out machineid))
        //            return null;
        //        else
        //            return machineid;
        //    }
        //    set
        //    {
        //        if (!value.HasValue)
        //            SetValue("MachineID", string.Empty);
        //        else
        //            SetValue("MachineID", value.Value.ToString());
        //    }
        //}


        /// <summary>
        /// Pokud je vypnuto, zadne korekce neni mozne vkladat, ani nejsou na tomto stroji zadne automaticky vkladane...
        /// </summary>
        public static bool Production_Material_Dopocist
        {
            get { return bool.Parse(GetValue("Production_Material_Dopocist", true.ToString())); }
            set { SetValue("Production_Material_Dopocist", value.ToString()); }
        }

        public static bool Production_Material_AUTO_vyber
        {
            get { return bool.Parse(GetValue("Production_Material_AUTO_vyber", true.ToString())); }
            set { SetValue("Production_Material_AUTO_vyber", value.ToString()); }
        }


        /// <summary>
        /// KioskMod volba
        /// </summary>
        public static bool chB_system_kiskmod
        {
            get { return bool.Parse(GetValue("chB_system_kiskmod", true.ToString())); }
            set { SetValue("chB_system_kiskmod", value.ToString()); }
        }

        /// <summary>
        /// Nastavuje zda se je potreba heslo pro ukonceni aplikace
        /// </summary>
        /// <remarks></remarks>
        public static bool LogOut_bezHesla
        {
            get { return bool.Parse(GetValue("LogOut_bezHesla", true.ToString())); }
            set { SetValue("LogOut_bezHesla", value.ToString()); }
        }

        public static string MachineID
        {
            get
            {
                return GetValue("MachineID", string.Empty);
            }
            set
            {
                SetValue("MachineID", value);
            }
        }

        /// <summary>
        /// Pokud je vypnuto, zadne korekce neni mozne vkladat, ani nejsou na tomto stroji zadne automaticky vkladane...
        /// </summary>
        public static bool CorrectsEnable
        {
            get { return bool.Parse(GetValue("CorrectsEnable", true.ToString())); }
            set { SetValue("CorrectsEnable", value.ToString()); }
        }

        /// <summary>
        /// Zpusob zadavani korekci
        /// 0 = jednorazove (zacatek i konec)
        /// 1 = postupne (zacatek, konec)
        /// </summary>
        public static string CorrectionType
        {
            get { return GetValue("CorrectionType", "0"); } 
            set { SetValue("CorrectionType", value); }
        }


        public static bool CorrectsCheckMinimumEnable
        {
            get { return bool.Parse(GetValue("CorrectsCheckMinimumEnable", false.ToString())); }
            set { SetValue("CorrectsCheckMinimumEnable", value.ToString()); }
        }

        public static bool CorrectsCheckPercentEnable
        {
            get { return bool.Parse(GetValue("CorrectsCheckPercentEnable", false.ToString())); }
            set { SetValue("CorrectsCheckPercentEnable", value.ToString()); }
        }

        /// <summary>
        /// Kontrolovat korekce pri vyrobe
        /// </summary>
        public static bool CorrectsCheckEnable
        {
            get { return bool.Parse(GetValue("CorrectsCheckEnable", false.ToString())); }
            set { SetValue("CorrectsCheckEnable", value.ToString()); }
        }
        /// <summary>
        /// Procentuelni rozdil korekce pri vyrobe ...
        /// </summary>
        public static decimal CorrectsCheckPercentValue
        {
            get { return decimal.Parse(GetValue("CorrectsCheckPercentValue", "1")); }
            set { SetValue("CorrectsCheckPercentValue", value.ToString()); }
        }

        public static TimeSpan CorrectsCheckMinimumValue
        {
            get { return TimeSpan.Parse(GetValue("CorrectsCheckMinimumValue", (new TimeSpan(0, 10, 0)).ToString())); }
            set { SetValue("CorrectsCheckMinimumValue", value.ToString()); }
        }


        public static bool CorrectsUsporaZadaniEnable
        {
            get { return bool.Parse(GetValue("CorrectsUsporaZadaniEnable", false.ToString())); }
            set { SetValue("CorrectsUsporaZadaniEnable", value.ToString()); }
        }

        public static string OdvadeniType
        {
            get { return GetValue("OdvadeniType", "Z"); }
            set { SetValue("OdvadeniType", value); }
        }

        public static string APIAktualizacePath_staraVerze
        {
            get { return GetValue("APIAktualizacePath_staraVerze", @"C:\FASK\"); }
            set { SetValue("APIAktualizacePath_staraVerze", value); }
        }

        public static string APIAktualizacePath_novaVerze
        {
            get { return GetValue("APIAktualizacePath_novaVerze", @"C:\FASK\"); }
            set { SetValue("APIAktualizacePath_novaVerze", value); }
        }

        public static string APIAktualizacePath_zalohaVerze
        {
            get { return GetValue("APIAktualizacePath_zalohaVerze", @"C:\FASK\"); }
            set { SetValue("APIAktualizacePath_zalohaVerze", value); }
        }

        public static string APIAktualizacePath_obnovaVerze
        {
            get { return GetValue("APIAktualizacePath_obnovaVerze", @"C:\FASK\"); }
            set { SetValue("APIAktualizacePath_obnovaVerze", value); }
        }


        public static bool OdvadeniZobrazovatCasy
        {
            get { return bool.Parse(GetValue("OdvadeniZobrazovatCasy", "true")); }
            set { SetValue("OdvadeniZobrazovatCasy", value.ToString()); }
        }

        public static bool OdvadeniPolozkaJizBylaOdvedena
        {
            get { return bool.Parse(GetValue("OdvadeniPolozkaJizBylaOdvedena", "true")); }
            set { SetValue("OdvadeniPolozkaJizBylaOdvedena", value.ToString()); }
        }

        public static bool OdvadeniZadanVetsiPocetKOdvedeniDotaz
        {
            get { return bool.Parse(GetValue("OdvadeniZadanVetsiPocetKOdvedeniDotaz", "true")); }
            set { SetValue("OdvadeniZadanVetsiPocetKOdvedeniDotaz", value.ToString()); }
        }        

        public static bool ZobrazovatChybySynchronizaceDatabaze
        {
            get { return bool.Parse(GetValue("ZobrazovatChybySynchronizaceDatabaze", "True")); }
            set { SetValue("ZobrazovatChybySynchronizaceDatabaze", value.ToString()); }
        }

        public static bool ZahajeniZakazkyDotaz
        {
            get { return bool.Parse(GetValue("ZahajeniZakazkyDotaz", "true")); }
            set { SetValue("ZahajeniZakazkyDotaz", value.ToString()); }
        }

        public static bool NecinnostTrvalaDeleNezDotaz
        {
            get { return bool.Parse(GetValue("NecinnostTrvalaDeleNezDotaz", "true")); }
            set { SetValue("NecinnostTrvalaDeleNezDotaz", value.ToString()); }
        }

        public static bool PovolitUkonceniZJinehoStroje
        {
            get { return bool.Parse(GetValue("PovolitUkonceniZJinehoStroje", "false")); }
            set { SetValue("PovolitUkonceniZJinehoStroje", value.ToString()); }
        }

        public static bool PovolitOdvadeniMnozstviNula
        {
            get { return bool.Parse(GetValue("PovolitOdvadeniMnozstviNula", "false")); }
            set { SetValue("PovolitOdvadeniMnozstviNula", value.ToString()); }
        }

        public static int FormPotvrzeniHeight
        {
            get { return int.Parse(GetValue("FormPotvrzeniHeight", "360")); }
            set { SetValue("FormPotvrzeniHeight", value.ToString()); }
        }

        public static int FormPotvrzeniWidth
        {
            get { return int.Parse(GetValue("FormPotvrzeniWidth", "265")); }
            set { SetValue("FormPotvrzeniWidth", value.ToString()); }
        }

        //public static Size FormOperacePotvrzeniSize
        //{
        //    get { return (Size)(new SizeConverter()).ConvertFromString(GetValue("FormOperacePotvrzeniSize", "300; 200")); }
        //    set { SetValue("FormOperacePotvrzeniSize", (new SizeConverter()).ConvertToString(value.ToString())); }
        //}

        
        public static bool UdalostiPovolitZmenuCasuPrihlaseniPracovnika
        {
            get { return bool.Parse(GetValue("UdalostiPovolitZmenuCasuPrihlaseniPracovnika", "true")); }
            set { SetValue("UdalostiPovolitZmenuCasuPrihlaseniPracovnika", value.ToString()); }
        }

        public static bool UdalostiZobrazitCasPrihlaseniPracovnika
        {
            get { return bool.Parse(GetValue("UdalostiZobrazitCasPrihlaseniPracovnika", "true")); }
            set { SetValue("UdalostiZobrazitCasPrihlaseniPracovnika", value.ToString()); }
        }

        public static bool OdvadeniPozadovatZadaniStroje
        {
            get { return bool.Parse(GetValue("OdvadeniPozadovatZadaniStroje", "true")); }
            set { SetValue("OdvadeniPozadovatZadaniStroje", value.ToString()); }
        }
        
        public static bool OdvadeniPozadovatHesloUzivatele
        {
            get { return bool.Parse(GetValue("OdvadeniPozadovatHesloUzivatele", "false")); }
            set { SetValue("OdvadeniPozadovatHesloUzivatele", value.ToString()); }
        }

        public static bool OdvadeniOdhlasitUzivatelePriZahajeniPrestavky
        {
            get { return bool.Parse(GetValue("OdhlasitUzivatelePriZahajeniPrestavky", "false")); }
            set { SetValue("OdhlasitUzivatelePriZahajeniPrestavky", value.ToString()); }
        }
        public static bool OdvadeniSouhrnPoStartzakazky
        {
            get { return bool.Parse(GetValue("SouhrnPoStartzakazky", "false")); }
            set { SetValue("SouhrnPoStartzakazky", value.ToString()); }
        }

        public static bool OdvadeniKontrolaSarze
        {
            get { return bool.Parse(GetValue("KontrolaSarze", "false")); }
            set { SetValue("KontrolaSarze", value.ToString()); }
        }


        public static bool OdvadeniOdhlasitUzivatelePriUkonceniPrestavky
        {
            get { return bool.Parse(GetValue("OdhlasitUzivatelePriUkonceniPrestavky", "false")); }
            set { SetValue("OdhlasitUzivatelePriUkonceniPrestavky", value.ToString()); }
        }

        public static bool OdvadeniOdhlasitUzivatelePriOdchoduZPracoviste
        {
            get { return bool.Parse(GetValue("OdhlasitUzivatelePriOdchoduZPracoviste", "false")); }
            set { SetValue("OdhlasitUzivatelePriOdchoduZPracoviste", value.ToString()); }
        }

        public static int FormBlokaceSplitterDistance1
        {
            get { return int.Parse(GetValue("FormBlokaceSplitterDistance1", "400")); }
            set { SetValue("FormBlokaceSplitterDistance1", value.ToString()); }
        }
        
        public static int FormVyberPrikazSplitterDistance1
        {
            get { return int.Parse(GetValue("FormVyberPrikazSplitterDistance1", "500")); }
            set { SetValue("FormVyberPrikazSplitterDistance1", value.ToString()); }
        }

        public static int FormVyberPrikazOperaceSplitterDistance2
        {
            get { return int.Parse(GetValue("FormVyberPrikazOperaceSplitterDistance2", "150")); }
            set { SetValue("FormVyberPrikazOperaceSplitterDistance2", value.ToString()); }
        }

        public static int FormVyberPrikazOperaceSplitterDistance1
        {
            get { return int.Parse(GetValue("FormVyberPrikazOperaceSplitterDistance1", "500")); }
            set { SetValue("FormVyberPrikazOperaceSplitterDistance1", value.ToString()); }
        }
        
        public static string ConfigTiskSablony
        {
            get { return GetValue("ConfigTiskSablony", @".\Tisk\TiskSablony.xml"); }
            set { SetValue("ConfigTiskSablony", value); }
        }

        public static bool VyberZakazkyPoPrihlaseni
        {
            get { return bool.Parse(GetValue("VyberZakazkyPoPrihlaseni", false.ToString())); }
            set { SetValue("VyberZakazkyPoPrihlaseni", value.ToString()); }
        }

        /// <summary>
        /// true = nastaveni Labara (Odvadeni typ Start/Stop/Stop,...), jinak Esa (Start/Stop/Start/Stop)
        /// </summary>
        public static bool OdvadeniSledovatCastecneOdvody
        {
            get { return bool.Parse(GetValue("OdvadeniSledovatCastecneOdvody", "true")); }
            set { SetValue("OdvadeniSledovatCastecneOdvody", value.ToString()); }
        }


        public static bool OdvadeniUkonceniStartStopPovolitVlozeniStart
        {
            get { return bool.Parse(GetValue("OdvadeniUkonceniStartStopPovolitVlozeniStart", "false")); }
            set { SetValue("OdvadeniUkonceniStartStopPovolitVlozeniStart", value.ToString()); }
        }

        public static bool OvladaniNumerickouKlavesnici
        {
            get { return bool.Parse(GetValue("OvladaniNumerickouKlavesnici", false.ToString())); }
            set { SetValue("OvladaniNumerickouKlavesnici", value.ToString()); }
        }

        public static bool PredvyplnitZbyvajiciMnozstvi
        {
            get { return bool.Parse(GetValue("PredvyplnitZbyvajiciMnozstvi", true.ToString())); }
            set { SetValue("PredvyplnitZbyvajiciMnozstvi", value.ToString()); }
        }

        /// <summary>
        /// Nastavuje zda se vyrobni prikazy zpracovavaji davkove nebo online...
        /// </summary>
        /// <remarks>V pripade davkoveho zpracovani je vyrobni prikaz blokovan terminalem a jiny terminal jej nemuze zpracovat</remarks>
        public static bool DavkoveZpracovani
        {
            get { return bool.Parse(GetValue("DavkoveZpracovani", true.ToString())); }
            set { SetValue("DavkoveZpracovani", value.ToString()); }
        }

        /// <summary>
        /// Pokud je davkove zpracovani, tak je mozne povolit blokace prikazu
        /// </summary>
        public static bool DavkoveZpracovaniBlokace
        {
            get { return bool.Parse(GetValue("DavkoveZpracovaniBlokace", false.ToString())); }
            set { SetValue("DavkoveZpracovaniBlokace", value.ToString()); }
        }
        
        /// <summary>
        /// Pouzity typ scanneru pro carove kody
        /// </summary>
        public static Fask.Aktualizace_API.Scanner.ScannerTypes ScannerType
        {
            get { return (Fask.Aktualizace_API.Scanner.ScannerTypes)Enum.Parse(typeof(Fask.Aktualizace_API.Scanner.ScannerTypes), GetValue("ScannerType", Fask.Aktualizace_API.Scanner.ScannerTypes.None.ToString()), true); }
            set { SetValue("ScannerType", value.ToString()); }
        }

        /// <summary>
        /// Vyska radku v okne Udalosti
        /// </summary>
        public static int UdalostiStatusRowHeight
        {
            get { return int.Parse(GetValue("UdalostiStatusRowHeight", Properties.Resources.UdalostiStatusRowHeight)); }
            set { SetValue("UdalostiStatusRowHeight", value.ToString()); }
        }

        /// <summary>
        /// Sirka sloupce id udalosti v okne Udalosti
        /// </summary>
        public static int UdalostiStatusIDWidth
        {
            get { return int.Parse(GetValue("UdalostiStatusIDWidth", Properties.Resources.UdalostiStatusIDWidth)); }
            set { SetValue("UdalostiStatusIDWidth", value.ToString()); }
        }

        /// <summary>
        /// Sirka sloupce popisu udalosti v okne Udalosti
        /// </summary>
        public static int UdalostiStatusDescWidth
        {
            get { return int.Parse(GetValue("UdalostiStatusDescWidth", Properties.Resources.UdalostiStatusDescWidth)); }
            set { SetValue("UdalostiStatusDescWidth", value.ToString()); }
        }

        /// <summary>
        /// Pocet radku v table layout panelu
        /// </summary>
        public static int ModulPocetRadku
        {
            get { return int.Parse(GetValue("ModulPocetRadku", "2")); }
            set { SetValue("ModulPocetRadku", value.ToString()); }
        }

        /// <summary>
        /// Pocet sloupcu v table layout panelu
        /// </summary>
        public static int ModulPocetSloupcu
        {
            get { return int.Parse(GetValue("ModulPocetSloupcu", "2")); }
            set { SetValue("ModulPocetSloupcu", value.ToString()); }
        }

        /// <summary>
        /// povolení využití modulu odvádìní
        /// </summary>
        public static bool ModulKonzola
        {
            get { return bool.Parse(GetValue("ModulPovolitOdvadeni", "true")); }
            set { SetValue("ModulPovolitOdvadeni", value.ToString()); }
        }

        /// <summary>
        /// Poradi sloupce, kde bude umisten modul.
        /// </summary>
        public static int ModulKonzolaColumn
        {
            get { return int.Parse(GetValue("ModulOdvadeniColumn", "0")); }
            set { SetValue("ModulOdvadeniColumn", value.ToString()); }
        }

        /// <summary>
        /// Rozsireni do strany, kde bude umisten modul.
        /// </summary>
        public static int ModulKonzolaColumnSpan
        {
            get { return int.Parse(GetValue("ModulOdvadeniColumnSpan", "1")); }
            set { SetValue("ModulOdvadeniColumnSpan", value.ToString()); }
        }

        /// <summary>
        /// Poradi sloupce, kde bude umisten modul.
        /// </summary>
        public static int ModulKonzolaRow
        {
            get { return int.Parse(GetValue("ModulOdvadeniRow", "0")); }
            set { SetValue("ModulOdvadeniRow", value.ToString()); }
        }

        /// <summary>
        /// Rozsireni do strany, kde bude umisten modul.
        /// </summary>
        public static int ModulKonzolaRowSpan
        {
            get { return int.Parse(GetValue("ModulOdvadeniRowSpan", "1")); }
            set { SetValue("ModulOdvadeniRowSpan", value.ToString()); }
        }

        /// <summary>
        /// povolení využití modulu dotisk
        /// </summary>
        public static bool ModulPovolitDotisk
        {
            get { return bool.Parse(GetValue("ModulPovolitDotisk", "false")); }
            set { SetValue("ModulPovolitDotisk", value.ToString()); }
        }
        /// <summary>
        /// Poradi sloupce, kde bude umisten modul.
        /// </summary>
        public static int ModulDotiskColumn
        {
            get { return int.Parse(GetValue("ModulDotiskColumn", "0")); }
            set { SetValue("ModulDotiskColumn", value.ToString()); }
        }

        /// <summary>
        /// Rozsireni do strany, kde bude umisten modul.
        /// </summary>
        public static int ModulDotiskColumnSpan
        {
            get { return int.Parse(GetValue("ModulDotiskColumnSpan", "1")); }
            set { SetValue("ModulDotiskColumnSpan", value.ToString()); }
        }

        /// <summary>
        /// Poradi sloupce, kde bude umisten modul.
        /// </summary>
        public static int ModulDotiskRow
        {
            get { return int.Parse(GetValue("ModulDotiskRow", "0")); }
            set { SetValue("ModulDotiskRow", value.ToString()); }
        }

        /// <summary>
        /// Rozsireni do strany, kde bude umisten modul.
        /// </summary>
        public static int ModulDotiskRowSpan
        {
            get { return int.Parse(GetValue("ModulDotiskRowSpan", "1")); }
            set { SetValue("ModulDotiskRowSpan", value.ToString()); }
        }

        /// <summary>
        /// povolení využití modulu korekce
        /// </summary>
        public static bool ModulPovolitKorekce
        {
            get { return bool.Parse(GetValue("ModulPovolitKorekce", "true")); }
            set { SetValue("ModulPovolitKorekce", value.ToString()); }
        }

        /// <summary>
        /// Poradi sloupce, kde bude umisten modul.
        /// </summary>
        public static int ModulKorekceColumn
        {
            get { return int.Parse(GetValue("ModulKorekceColumn", "0")); }
            set { SetValue("ModulKorekceColumn", value.ToString()); }
        }

        /// <summary>
        /// Rozsireni do strany, kde bude umisten modul.
        /// </summary>
        public static int ModulKorekceColumnSpan
        {
            get { return int.Parse(GetValue("ModulKorekceColumnSpan", "1")); }
            set { SetValue("ModulKorekceColumnSpan", value.ToString()); }
        }

        /// <summary>
        /// Poradi sloupce, kde bude umisten modul.
        /// </summary>
        public static int ModulKorekceRow
        {
            get { return int.Parse(GetValue("ModulKorekceRow", "1")); }
            set { SetValue("ModulKorekceRow", value.ToString()); }
        }

        /// <summary>
        /// Rozsireni do strany, kde bude umisten modul.
        /// </summary>
        public static int ModulKorekceRowSpan
        {
            get { return int.Parse(GetValue("ModulKorekceRowSpan", "1")); }
            set { SetValue("ModulKorekceRowSpan", value.ToString()); }
        }

        /// <summary>
        /// povolení využití modulu udalosti
        /// </summary>
        public static bool ModulPovolitUdalosti
        {
            get { return bool.Parse(GetValue("ModulPovolitUdalosti", "true")); }
            set { SetValue("ModulPovolitUdalosti", value.ToString()); }
        }

        /// <summary>
        /// Poradi sloupce, kde bude umisten modul.
        /// </summary>
        public static int ModulUdalostiColumn
        {
            get { return int.Parse(GetValue("ModulUdalostiColumn", "1")); }
            set { SetValue("ModulUdalostiColumn", value.ToString()); }
        }

        /// <summary>
        /// Rozsireni do strany, kde bude umisten modul.
        /// </summary>
        public static int ModulUdalostiColumnSpan
        {
            get { return int.Parse(GetValue("ModulUdalostiColumnSpan", "1")); }
            set { SetValue("ModulUdalostiColumnSpan", value.ToString()); }
        }

        /// <summary>
        /// Poradi sloupce, kde bude umisten modul.
        /// </summary>
        public static int ModulUdalostiRow
        {
            get { return int.Parse(GetValue("ModulUdalostiRow", "0")); }
            set { SetValue("ModulUdalostiRow", value.ToString()); }
        }

        /// <summary>
        /// Rozsireni do strany, kde bude umisten modul.
        /// </summary>
        public static int ModulUdalostiRowSpan
        {
            get { return int.Parse(GetValue("ModulUdalostiRowSpan", "2")); }
            set { SetValue("ModulUdalostiRowSpan", value.ToString()); }
        }

        /// <summary>
        /// povolení využití modulu prehled odvodu
        /// </summary>
        public static bool ModulPovolitPrehledOdvodu
        {
            get { return bool.Parse(GetValue("ModulPovolitPrehledOdvodu", "false")); }
            set { SetValue("ModulPovolitPrehledOdvodu", value.ToString()); }
        }

        /// <summary>
        /// Velikost pisma.
        /// </summary>
        public static float ModulPrehledOdvoduFontSize
        {
            get { return float.Parse(GetValue("ModulPrehledOdvoduFontSize", "7,25")); }
            set { SetValue("ModulPrehledOdvoduFontSize", value.ToString()); }
        }

        /// <summary>
        /// Velikost pisma radku.
        /// </summary>
        public static float ModulPrehledOdvoduFontRowSize
        {
            get { return float.Parse(GetValue("ModulPrehledOdvoduFontRowSize", "7,25")); }
            set { SetValue("ModulPrehledOdvoduFontRowSize", value.ToString()); }
        }

        /// <summary>
        /// Poradi sloupce, kde bude umisten modul.
        /// </summary>
        public static int ModulPrehledOdvoduColumn
        {
            get { return int.Parse(GetValue("ModulPrehledOdvoduColumn", "1")); }
            set { SetValue("ModulPrehledOdvoduColumn", value.ToString()); }
        }

        /// <summary>
        /// Rozsireni do strany, kde bude umisten modul.
        /// </summary>
        public static int ModulPrehledOdvoduColumnSpan
        {
            get { return int.Parse(GetValue("ModulPrehledOdvoduColumnSpan", "1")); }
            set { SetValue("ModulPrehledOdvoduColumnSpan", value.ToString()); }
        }

        /// <summary>
        /// Poradi sloupce, kde bude umisten modul.
        /// </summary>
        public static int ModulPrehledOdvoduRow
        {
            get { return int.Parse(GetValue("ModulPrehledOdvoduRow", "1")); }
            set { SetValue("ModulPrehledOdvoduRow", value.ToString()); }
        }

        /// <summary>
        /// Rozsireni do strany, kde bude umisten modul.
        /// </summary>
        public static int ModulPrehledOdvoduRowSpan
        {
            get { return int.Parse(GetValue("ModulPrehledOdvoduRowSpan", "1")); }
            set { SetValue("ModulPrehledOdvoduRowSpan", value.ToString()); }
        }

        /// <summary>
        /// Casovy interval resetovani filtru u prehledu odvadeni
        /// </summary>
        public static int ModulPrehledOdvoduFilterResetInterval
        {
            get { return int.Parse(GetValue("ModulPrehledOdvoduFilterResetInterval", Properties.Resources.TimerDownloadInterval)); }
            set { SetValue("ModulPrehledOdvoduFilterResetInterval", value.ToString()); }
        }

        /// <summary>
        /// Casovy interval automaticke aktualizace prehledu odvadeni
        /// </summary>
        public static int ModulPrehledOdvoduAutoUpdateInterval
        {
            get { return int.Parse(GetValue("ModulPrehledOdvoduAutoUpdateInterval", Properties.Resources.TimerDownloadInterval)); }
            set { SetValue("ModulPrehledOdvoduAutoUpdateInterval", value.ToString()); }
        }

        /// <summary>
        /// Maximalni pocet dnu, ktery se bude uchovavat v historii.
        /// </summary>
        public static int ModulPrehledOdvoduMaxDaysInHistory
        {
            get { return int.Parse(GetValue("ModulPrehledOdvoduMaxDaysInHistory", "7")); }
            set { SetValue("ModulPrehledOdvoduMaxDaysInHistory", value.ToString()); }
        }

        /// <summary>
        /// Stahovat data ze serveru.
        /// </summary>
        public static bool ModulPrehledOdvoduPouzitDataZeServeru
        {
            get { return bool.Parse(GetValue("ModulPrehledOdvoduPouzitDataZeServeru", "false")); }
            set { SetValue("ModulPrehledOdvoduPouzitDataZeServeru", value.ToString()); }
        }

        /// <summary>
        /// Celkový poèet záznamù, který se má zobrazit
        /// </summary>
        public static int ModulPrehledPocetHodinHistorie
        {
            get { return int.Parse(GetValue("ModulPrehledPocetHodinHistorie", "8")); }
            set { SetValue("ModulPrehledPocetHodinHistorie", value.ToString()); }
        }

        /// <summary>
        /// Celkový poèet záznamù, který se má stáhnout ze serveru
        /// </summary>
        public static int ModulPrehledPocetHodinHistorieServer
        {
            get { return int.Parse(GetValue("ModulPrehledPocetHodinHistorieServer", "768")); } //32dni * 24hod = 768 hod.
            set { SetValue("ModulPrehledPocetHodinHistorieServer", value.ToString()); }
        }


        /// <summary>
        /// povolení využití modulu prehled odvodu
        /// </summary>
        public static bool ModulPovolitNedokonceneZakazky
        {
            get { return bool.Parse(GetValue("ModulPovolitNedokonceneZakazky", "false")); }
            set { SetValue("ModulPovolitNedokonceneZakazky", value.ToString()); }
        }

        /// <summary>
        /// Velikost pisma.
        /// </summary>
        public static float ModulNedokonceneZakazkyFontSize
        {
            get { return float.Parse(GetValue("ModulNedokonceneZakazkyFontSize", "7,25")); }
            set { SetValue("ModulNedokonceneZakazkyFontSize", value.ToString()); }
        }

        /// <summary>
        /// Velikost pisma radku.
        /// </summary>
        public static float ModulNedokonceneZakazkyFontRowSize
        {
            get { return float.Parse(GetValue("ModulNedokonceneZakazkyFontRowSize", "7,25")); }
            set { SetValue("ModulNedokonceneZakazkyFontRowSize", value.ToString()); }
        }

        /// <summary>
        /// Stahovat data ze serveru.
        /// </summary>
        public static bool ModulNedokonceneZakazkyPouzitDataZeServeru
        {
            get { return bool.Parse(GetValue("ModulNedokonceneZakazkyPouzitDataZeServeru", "true")); }
            set { SetValue("ModulNedokonceneZakazkyPouzitDataZeServeru", value.ToString()); }
        }


        public static bool ModulNedokonceneZakazkyPouzitFiltrNaUzivatele
        {
            get { return bool.Parse(GetValue("ModulNedokonceneZakazkyPouzitFiltrNaUzivatele", "false")); }
            set { SetValue("ModulNedokonceneZakazkyPouzitFiltrNaUzivatele", value.ToString()); }
        }

        public static bool ModulNedokonceneZakazkyPouzitFiltrNaStroj
        {
            get { return bool.Parse(GetValue("ModulNedokonceneZakazkyPouzitFiltrNaStroj", "false")); }
            set { SetValue("ModulNedokonceneZakazkyPouzitFiltrNaStroj", value.ToString()); }
        }

        /// <summary>
        /// Poradi sloupce, kde bude umisten modul.
        /// </summary>
        public static int ModulNedokonceneZakazkyColumn
        {
            get { return int.Parse(GetValue("ModulNedokonceneZakazkyColumn", "1")); }
            set { SetValue("ModulNedokonceneZakazkyColumn", value.ToString()); }
        }

        /// <summary>
        /// Rozsireni do strany, kde bude umisten modul.
        /// </summary>
        public static int ModulNedokonceneZakazkyColumnSpan
        {
            get { return int.Parse(GetValue("ModulNedokonceneZakazkyColumnSpan", "1")); }
            set { SetValue("ModulNedokonceneZakazkyColumnSpan", value.ToString()); }
        }

        /// <summary>
        /// Poradi sloupce, kde bude umisten modul.
        /// </summary>
        public static int ModulNedokonceneZakazkyRow
        {
            get { return int.Parse(GetValue("ModulNedokonceneZakazkyRow", "1")); }
            set { SetValue("ModulNedokonceneZakazkyRow", value.ToString()); }
        }

        /// <summary>
        /// Rozsireni do strany, kde bude umisten modul.
        /// </summary>
        public static int ModulNedokonceneZakazkyRowSpan
        {
            get { return int.Parse(GetValue("ModulNedokonceneZakazkyRowSpan", "1")); }
            set { SetValue("ModulNedokonceneZakazkyRowSpan", value.ToString()); }
        }

        /// <summary>
        /// Casovy interval resetovani filtru u prehledu odvadeni
        /// </summary>
        public static int ModulNedokonceneZakazkyFilterResetInterval
        {
            get { return int.Parse(GetValue("ModulNedokonceneZakazkyFilterResetInterval", Properties.Resources.TimerDownloadInterval)); }
            set { SetValue("ModulNedokonceneZakazkyFilterResetInterval", value.ToString()); }
        }

        /// <summary>
        /// Casovy interval automaticke aktualizace prehledu odvadeni
        /// </summary>
        public static int ModulNedokonceneZakazkyAutoUpdateInterval
        {
            get { return int.Parse(GetValue("ModulNedokonceneZakazkyAutoUpdateInterval", Properties.Resources.TimerDownloadInterval)); }
            set { SetValue("ModulNedokonceneZakazkyAutoUpdateInterval", value.ToString()); }
        }

        // TODO

        /// <summary>
        /// ID udalosti odhlaseni pracovnika
        /// </summary>
        public static string UEventTiskEtikety
        {
            get { return GetValue("UEventTiskEtikety", Properties.Resources.UEventTiskEtikety); }
            set { SetValue("UEventTiskEtikety", value); }
        }

        /// <summary>
        /// Povoluje zadani ID pracovnika pri spusteni aplikace ... 
        /// </summary>
        public static bool UEventPracovnikLoginEnabled
        {
            get { return bool.Parse(GetValue("UEventPracovnikLoginEnabled", Properties.Resources.UEventPracovnikLoginEnabled)); }
            set { SetValue("UEventPracovnikLoginEnabled", value.ToString()); }
        }

        /// <summary>
        /// ID udalosti odhlaseni pracovnika
        /// </summary>
        public static string UEventPracovnikPrihlaseni
        {
            get { return GetValue("UEventPracovnikPrihlaseni", Properties.Resources.UEventPracovnikPrihlaseni); }
            set { SetValue("UEventPracovnikPrihlaseni", value); }
        }

        /// <summary>
        /// Zobrazit prehled casu pri odhlaseni pracovnika
        /// </summary>
        public static bool UEventPracovnikOdhlaseniShowReport
        {
            get { return bool.Parse(GetValue("UEventPracovnikOdhlaseniShowReport", false.ToString())); }
            set { SetValue("UEventPracovnikOdhlaseniShowReport", value.ToString()); }
        }

        /// <summary>
        /// ID udalosti prihlaseni pracovnika
        /// </summary>
        public static string UEventPracovnikOdhlaseni
        {
            get { return GetValue("UEventPracovnikOdhlaseni", Properties.Resources.UEventPracovnikOdhlaseni); }
            set { SetValue("UEventPracovnikOdhlaseni", value); }
        }

        /// <summary>
        /// ID udalosti zruseni odvod
        /// </summary>
        public static string UEventZruseniOdvod
        {
            get { return GetValue("UEventZruseniOdvod", Properties.Resources.UEventPracovnikOdhlaseni); }
            set { SetValue("UEventZruseniOdvod", value); }
        }

        /// <summary>
        /// ID udalosti zruseni Priprava
        /// </summary>
        public static string UEventZruseniPriprava
        {
            get { return GetValue("UEventZruseniPriprava", Properties.Resources.UEventPracovnikOdhlaseni); }
            set { SetValue("UEventZruseniPriprava", value); }
        }
        

        /// <summary>
        /// Povoleni prihlasovani smeny
        /// </summary>
        /// <remarks>pokud neni povoleno prihlasovani smeny, pak se vynechava logovaci obrazovka</remarks>
        public static bool UEventSmenaEnabled
        {
            get { return bool.Parse(GetValue("UEventSmenaEnabled", Properties.Resources.UEventSmenaEnabled)); }
            set { SetValue("UEventSmenaEnabled", value.ToString()); }
        }

        /// <summary>
        /// ID udalosti prihlaseni smeny
        /// </summary>
        public static string UEventSmenaLogin
        {
            get { return GetValue("UEventSmenaLogin", Properties.Resources.UEventSmenaLogin); }
            set { SetValue("UEventSmenaLogin", value); }
        }

        /// <summary>
        /// ID udalosti odhlaseni smeny
        /// </summary>
        public static string UEventSmenaLogout
        {
            get { return GetValue("UEventSmenaLogout", Properties.Resources.UEventSmenaLogout); }
            set { SetValue("UEventSmenaLogout", value); }
        }
        
        /// <summary>
        /// Casovy interval nahravani odvedene vyroby
        /// </summary>
        public static int TimerUploadInterval
        {
            get { return int.Parse(GetValue("TimerUploadInterval", Properties.Resources.TimerUploadInterval)); }
            set { SetValue("TimerUploadInterval", value.ToString()); }
        }
      

        /// <summary>
        /// Casovy interval stahovani predlohy vyroby
        /// </summary>
        public static int TimerDownloadInterval
        {
            get { return int.Parse(GetValue("TimerDownloadInterval", Properties.Resources.TimerDownloadInterval)); }
            set { SetValue("TimerDownloadInterval", value.ToString()); }
        }

        /// <summary>
        /// Logovat tisk...Zmeneno...
        /// </summary>
        public static bool Loging
        {
            get { return bool.Parse(GetValue("Loging", Properties.Resources.Loging)); }
            set { SetValue("Loging", value.ToString()); }
        }                                                

        /// <summary>
        /// ID terminalu
        /// </summary>
        /// <remarks>Toto id musi byt jedinecne pro terminal</remarks>
        public static byte TerminalID
        {
            get { return byte.Parse(GetValue("TerminalID", Properties.Resources.TerminalID)); }
            set { SetValue("TerminalID", value.ToString()); }
        }                                                    

        /// <summary>
        /// Heslo pro odhlášení smìny
        /// </summary>
        public static string UEventSmenaLogoutPasswordConfig
        {
            get { return GetValue("UEventSmenaLogoutPasswordConfig", string.Empty); }
            set { SetValue("UEventSmenaLogoutPasswordConfig", value); }
        }




        /// <summary>
        /// Datum a cas posledniho uspesne synchronizace(stazeni) predlohy vyroby
        /// </summary>
        public static DateTime LastDownload
        {
            get
            {
                try { return DateTime.Parse(GetValue("LastDownload", DateTime.MinValue.ToString(NumberFormatInfo.InvariantInfo)), NumberFormatInfo.InvariantInfo); }
                catch { return DateTime.MinValue; }
            }
            set { SetValue("LastDownload", value.ToString(NumberFormatInfo.InvariantInfo)); }
        }


        /// <summary>
        /// Datum a cas posledniho odvodu
        /// </summary>
        public static DateTime LastProductionDateTime
        {
            get
            {
                try
                {
                    return DateTime.Parse(GetValue("LastProductionDateTime", DateTime.Now.ToString(NumberFormatInfo.InvariantInfo)));
                }
                catch { return DateTime.Now; }
            }
            set { SetValue("LastProductionDateTime", value.ToString(NumberFormatInfo.InvariantInfo)); }
        }


        public static TimeSpan Production_UserMaxTimeSpanNoAction
        {
            get { return TimeSpan.Parse(GetValue("Production_UserMaxTimeSpanNoAction", (new TimeSpan(0, 30, 0).ToString()))); }
            set { SetValue("Production_UserMaxTimeSpanNoAction", value.ToString()); }
        }

        #region Materialy

        /// <summary>
        /// Vychozi zdrojova lokace materialu vyrobku
        /// </summary>
        public static string Production_Material_Source_LOCNCODE
        {
            get { return GetValue("Production_Material_Source_LOCNCODE", string.Empty); }
            set { SetValue("Production_Material_Source_LOCNCODE", value.Trim()); }
        }

        /// <summary>
        /// Zadavat zdrojovou lokaci skladu materialu
        /// </summary>
        public static bool Production_Material_Source_LOCNCODE_Enter
        {
            get { return bool.Parse(GetValue("Production_Material_Source_LOCNCODE_Enter", false.ToString())); }
            set { SetValue("Production_Material_Source_LOCNCODE_Enter", value.ToString()); }
        }

        /// <summary>
        /// Prednastaveny zdrojovy sklad materialu vyrobku
        /// </summary>
        public static string Production_Material_Source_SKLID
        {
            get { return GetValue("Production_Material_Source_SKLID", string.Empty); }
            set { SetValue("Production_Material_Source_SKLID", value.Trim()); }
        }

        /// <summary>
        /// Vyzaduje zadani zdrojoveho skladu materialu vstupujiciho do vyrobku
        /// </summary>
        public static bool Production_Material_Source_SKLID_Enter
        {
            get { return bool.Parse(GetValue("Production_Material_Source_SKLID_Enter", false.ToString())); }
            set { SetValue("Production_Material_Source_SKLID_Enter", value.ToString()); }
        }

        /// <summary>
        /// Vyzaduje zadani rozpadu vstupniho materialu vyrobku
        /// </summary>
        public static bool Production_Material_Enter
        {
            get { return bool.Parse(GetValue("Production_Material_Enter", false.ToString())); }
            set { SetValue("Production_Material_Enter", value.ToString()); }
        }

        /// <summary>
        /// Zadani materialu vyrobku v formu Operace Potvrzeni
        /// </summary>
        public static bool Production_Material_OperacePotvrzeniButton
        {
            get { return bool.Parse(GetValue("Production_Material_OperacePotvrzeniButton", false.ToString())); }
            set { SetValue("Production_Material_OperacePotvrzeniButton", value.ToString()); }
        }

        /// <summary>
        /// Zadani materialu vyrobku pred vyrobou
        /// </summary>
        public static bool Production_Material_PredVyrobou
        {
            get { return bool.Parse(GetValue("Production_Material_PredVyrobou", false.ToString())); }
            set { SetValue("Production_Material_PredVyrobou", value.ToString()); }
        }

        /// <summary>
        /// Zadani materialu vyrobku po vyrobe
        /// </summary>
        public static bool Production_Material_PoVyrobe
        {
            get { return bool.Parse(GetValue("Production_Material_PoVyrobe", false.ToString())); }
            set { SetValue("Production_Material_PoVyrobe", value.ToString()); }
        }

        /// <summary>
        /// Nastaveni hodnotu sarze
        /// </summary>
        public static bool Production_Material_Doplnit_Sarze
        {
            get { return bool.Parse(GetValue("Production_Material_Doplnit_Sarze", false.ToString())); }
            set { SetValue("Production_Material_Doplnit_Sarze", value.ToString()); }
        }

        /// <summary>
        /// Nastaveni hodnotu sarze
        /// </summary>
        public static string Production_Material_Hodnota_Sarze
        {
            get { return GetValue("Production_Material_Hodnota_Sarze", string.Empty); }
            set { SetValue("Production_Material_Hodnota_Sarze", value.Trim()); }
        }

        /// <summary>
        /// Nastaveni predvybraneho ciloveho skladu vyrobku
        /// </summary>
        public static string Production_Destination_SKLID
        {
            get { return GetValue("Production_Destination_SKLID", string.Empty); }
            set { SetValue("Production_Destination_SKLID", value.Trim()); }
        }

        /// <summary>
        /// Vyzaduje zadani ciloveho skladu vyrobku
        /// </summary>
        public static bool Production_Destination_SKLID_Enter
        {
            get { return bool.Parse(GetValue("Production_Destination_SKLID_Enter", false.ToString())); }
            set { SetValue("Production_Destination_SKLID_Enter", value.ToString()); }
        }

        /// <summary>
        /// Vychozi cilova lokace vyrobku
        /// </summary>
        public static string Production_Destination_LOCNCODE
        {
            get { return GetValue("Production_Destination_LOCNCODE", string.Empty); }
            set { SetValue("Production_Destination_LOCNCODE", value.Trim()); }
        }

        /// <summary>
        /// Zadavat cilovou lokaci skladu vyrobku
        /// </summary>
        public static bool Production_Destination_LOCNCODE_Enter
        {
            get { return bool.Parse(GetValue("Production_Destination_LOCNCODE_Enter", false.ToString())); }
            set { SetValue("Production_Destination_LOCNCODE_Enter", value.ToString()); }
        }

        /// <summary>
        /// Preskocit okno potvrzeni operace
        /// </summary>
        public static bool OdvadeniPotvrzeniOperace
        {
            get { return bool.Parse(GetValue("Odvadeni_PotvrzeniOperace", false.ToString())); }
            set { SetValue("Odvadeni_PotvrzeniOperace", value.ToString()); }
        }

        /// <summary>
        /// Preskocit okno Prehled (Form OdvadeniPrehled)
        /// </summary>
        public static bool OdvadeniPrehled
        {
            get { return bool.Parse(GetValue("Odvadeni_Prehled", false.ToString())); }
            set { SetValue("Odvadeni_Prehled", value.ToString()); }
        }

        /// <summary>
        ///  Povolit zruseni operace
        /// </summary>
        public static bool ZruseniOperace
        {
            get { return bool.Parse(GetValue("Odvadeni_ZruseniOperace", false.ToString())); }
            set { SetValue("Odvadeni_ZruseniOperace", value.ToString()); }
        }

        /// <summary>
        ///  Povolit zruseni operace
        /// </summary>
        public static bool ZruseniPouzeVS
        {
            get { return bool.Parse(GetValue("Odvadeni_ZruseniPouzeVS", false.ToString())); }
            set { SetValue("Odvadeni_ZruseniPouzeVS", value.ToString()); }
        }

        

        #endregion

        /// <summary>
        /// ID uzivatele posledniho odvodu vyroby
        /// </summary>
        public static string LastProductionUserID
        {
            get { return (GetValue("LastProductionUserID", string.Empty)); }
            set { SetValue("LastProductionUserID", value.ToString()); }
        }

        /// <summary>
        /// ID prestavky
        /// </summary>
        public static int PrestavkaID
        {
            get { return int.Parse(GetValue("PrestavkaID", (-1).ToString())); }
            set { SetValue("PrestavkaID", value.ToString()); }
        }

        /// <summary>
        /// Doba od posledniho odvodu uzivatele, po ktere je znovu uzivatel vyzvan k prihlaseni
        /// </summary>
        public static TimeSpan LoginUserTimeOut
        {
            get { return TimeSpan.Parse(GetValue("LoginUserTimeOut", Properties.Resources.LoginUserTimeOut)); }
            set { SetValue("LoginUserTimeOut", value.ToString()); }
        }

        /// <summary>
        /// Po Stop Odvod Ihned Zahajeni Vyroby
        /// </summary>
        public static bool StopPripravaStartVyrobaIhned
        {
            get { return bool.Parse(GetValue("StopPripravaStartVyrobaIhned", true.ToString())); }
            set { SetValue("StopPripravaStartVyrobaIhned", value.ToString()); }
        }


        public static int ProductionOnlineTimeout
        {
            get { return int.Parse(GetValue("ProductionOnlineTimeout", (10000).ToString())); }
            set { SetValue("ProductionOnlineTimeout", value.ToString()); }
        }

        /// <summary>
        /// Heslo umožòující pøístup do Systém - konfigurace. 
        /// </summary>
        public static string PasswordConfig
        {
            get { return (GetValue("PasswordConfig", Properties.Resources.PasswordConfig)); }
            set { SetValue("PasswordConfig", value.ToString()); }
        }

        /// <summary>
        /// povolení využití modulu úkolování
        /// </summary>
        public static bool ModulPovolitUkolovani
        {
            get { return bool.Parse(GetValue("ModulPovolitUkolovani", "true")); }
            set { SetValue("ModulPovolitUkolovani", value.ToString()); }
        }

        /// <summary>
        /// Poradi sloupce, kde bude umisten modul.
        /// </summary>
        public static int ModulUkolovaniColumn
        {
            get { return int.Parse(GetValue("ModulUkolovaniColumn", "0")); }
            set { SetValue("ModulUkolovaniColumn", value.ToString()); }
        }

        /// <summary>
        /// Rozsireni do strany, kde bude umisten modul.
        /// </summary>
        public static int ModulUkolovaniColumnSpan
        {
            get { return int.Parse(GetValue("ModulUkolovaniColumnSpan", "1")); }
            set { SetValue("ModulUkolovaniColumnSpan", value.ToString()); }
        }

        /// <summary>
        /// Poradi sloupce, kde bude umisten modul.
        /// </summary>
        public static int ModulUkolovaniRow
        {
            get { return int.Parse(GetValue("ModulUkolovaniRow", "0")); }
            set { SetValue("ModulUkolovaniRow", value.ToString()); }
        }

        /// <summary>
        /// Rozsireni do strany, kde bude umisten modul.
        /// </summary>
        public static int ModulUkolovaniRowSpan
        {
            get { return int.Parse(GetValue("ModulUkolovaniRowSpan", "1")); }
            set { SetValue("ModulUkolovaniRowSpan", value.ToString()); }
        }

        public static bool TasksEnable
        {
            get { return bool.Parse(GetValue("TaskEnable", "true")); }
            set { SetValue("TasksEnable", value.ToString()); }
        }

        public static int TasksNotifyDialogShowSeconds
        {
            get { return int.Parse(GetValue("TasksNotifyDialogShowSeconds", "20")); }
            set { SetValue("TasksNotifyDialogShowSeconds", value.ToString()); }
        }

        /// <summary>
        /// Predmet emailu odesilaneho na sms
        /// </summary>
        public static string Email_Predmet_sms
        {
            get { return (GetValue("Email_Predmet_sms", string.Empty)); }
            set { SetValue("Email_Predmet_sms", value.ToString()); }
        }

        /// <summary>
        /// Predmet emailu odesilaneho na email
        /// </summary>
        public static string Email_Predmet_email
        {
            get { return (GetValue("Email_Predmet_email", string.Empty)); }
            set { SetValue("Email_Predmet_email", value.ToString()); }
        }

        #region Tisk VP
        public static bool TiskPalety
        {
            get { return bool.Parse(GetValue("TiskPalety", "false")); }
            set { SetValue("TiskPalety", value.ToString()); }
        }

        public static bool TiskPotvrzeni
        {
            get { return bool.Parse(GetValue("TiskPotvrzeni", "false")); }
            set { SetValue("TiskPotvrzeni", value.ToString()); }
        }

        public static bool TiskMnozstviJednaAutomaticky
        {
            get { return bool.Parse(GetValue("TiskMnozstviJednaAutomaticky", "false")); }
            set { SetValue("TiskMnozstviJednaAutomaticky", value.ToString()); }
        }

        public static string TiskNazevSablony
        {
            get { return (GetValue("TiskNazevSablony", string.Empty)); }
            set { SetValue("TiskNazevSablony", value.ToString()); }
        }

        public static string TiskNazevTiskarny
        {
            get { return (GetValue("TiskNazevTiskarny", string.Empty)); }
            set { SetValue("TiskNazevTiskarny", value.ToString()); }
        }

        public static string TiskMnozstviPredvyplnit
        {
            get { return (GetValue("TiskMnozstviPredvyplnit", string.Empty)); }
            set { SetValue("TiskMnozstviPredvyplnit", value.ToString()); }
        }

        public static bool Tisk_OneWayPrint
        {
            get { return bool.Parse(GetValue("Tisk_OneWayPrint", "false")); }
            set { SetValue("Tisk_OneWayPrint", value.ToString()); }
        }


        public static string Tisk_PopisPoctuTisku
        {
            get { return (GetValue("Tisk_PopisPoctuTisku", string.Empty)); }
            set { SetValue("Tisk_PopisPoctuTisku", value.ToString()); }
        }

        public static bool SSCC_Generovani_auto
        {
            get { return bool.Parse(GetValue("SSCC_Generovani_auto", "false")); }
            set { SetValue("SSCC_Generovani_auto", value.ToString()); }
        }

        #endregion

        public static bool Odvadeni_OvereniUzivateleBezHesla
        {
            get { return bool.Parse(GetValue("Odvadeni_OvereniUzivateleBezHesla", "false")); }
            set { SetValue("Odvadeni_OvereniUzivateleBezHesla", value.ToString()); }
        }

        #region Certifikaty pro HTTPs


        public static Fask.Aktualizace_API.Globals.ServerAccessCertificatesTrustType ServerAccessCertificateTrust
        {
            get
            {
                string cert = string.IsNullOrEmpty(GetValue("ServerAccessCertificateTrust", Fask.Aktualizace_API.Globals.ServerAccessCertificatesTrustType.TrustAll.ToString())) ? Fask.Aktualizace_API.Globals.ServerAccessCertificatesTrustType.OnlyInstalled.ToString() : GetValue("ServerAccessCertificateTrust", Fask.Aktualizace_API.Globals.ServerAccessCertificatesTrustType.TrustAll.ToString());
                return (Fask.Aktualizace_API.Globals.ServerAccessCertificatesTrustType)Enum.Parse(typeof(Fask.Aktualizace_API.Globals.ServerAccessCertificatesTrustType), cert, true);
            }
            set { SetValue("ServerAccessCertificateTrust", value.ToString()); }
        }


        #region Parametry

        public enum ServerAccessType
        {
            Anonymous,
            Credentials
        }

        public static ServerAccessType ServerAccess
        {
            get { return (ServerAccessType)Enum.Parse(typeof(ServerAccessType), GetValue("ServerAccess", ServerAccessType.Anonymous.ToString()), true); }
            set { SetValue("ServerAccess", value.ToString()); }
        }

        public static string ServerAccessUsername

        {
            get { return GetValue("ServerAccessUsername", string.Empty); }
            set { SetValue("ServerAccessUsername", value.ToString()); }
        }

        public static string ServerAccessPassword
        {
            get { return GetValue("ServerAccessPassword", string.Empty); }
            set { SetValue("ServerAccessPassword", value.ToString()); }
        }

        public static string ServerAccessDomain
        {
            get { return GetValue("ServerAccessDomain", string.Empty); }
            set { SetValue("ServerAccessDomain", value.ToString()); }
        }

        public static bool ServerAccessPreauthenticate
        {
            get { return bool.Parse(GetValue("ServerAccessPreauthenticate", false.ToString())); }
            set { SetValue("ServerAccessPreauthenticate", value.ToString()); }
        }

        public static bool ServerAccessAllowRedirection
        {
            get { return bool.Parse(GetValue("ServerAccessAllowRedirection", false.ToString())); }
            set { SetValue("ServerAccessAllowRedirection", value.ToString()); }
        }

        public static bool ServerAccessAllowDecompression
        {
            get { return bool.Parse(GetValue("ServerAccessAllowDecompression", false.ToString())); }
            set { SetValue("ServerAccessAllowDecompression", value.ToString()); }
        }

        #endregion

        #endregion

        #region REZ hodnoty lokalizace

        public static string Odvadeni_REZ_1_Lokalizace
        {
            get { return GetValue("Odvadeni_REZ_1_Lokalizace", "REZ1"); }
            set { SetValue("Odvadeni_REZ_1_Lokalizace", value); }
        }

        public static string Odvadeni_REZ_2_Lokalizace
        {
            get { return GetValue("Odvadeni_REZ_2_Lokalizace", "REZ1"); }
            set { SetValue("Odvadeni_REZ_2_Lokalizace", value); }
        }

        public static string Odvadeni_REZ_3_Lokalizace
        {
            get { return GetValue("Odvadeni_REZ_3_Lokalizace", "REZ1"); }
            set { SetValue("Odvadeni_REZ_3_Lokalizace", value); }
        }

        public static string Odvadeni_REZ_4_Lokalizace
        {
            get { return GetValue("Odvadeni_REZ_4_Lokalizace", "REZ1"); }
            set { SetValue("Odvadeni_REZ_4_Lokalizace", value); }
        }

        public static string Odvadeni_REZ_5_Lokalizace
        {
            get { return GetValue("Odvadeni_REZ_5_Lokalizace", "REZ1"); }
            set { SetValue("Odvadeni_REZ_5_Lokalizace", value); }
        }

        #endregion

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
    }
}
