using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Collections.Specialized;
using Microsoft.Win32;
using System.Globalization;

namespace Fask.Vyroba_W
{
    public class Settings
    {
        private static NameValueCollection m_settings;
        private static string m_settingsPath;


		/// <summary>
		/// Adresa web serveru, ktery terminal vyuziva
		/// </summary>
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

        /// <summary>
        /// Umoznuje online volani funkci vyroby
        /// 17.1.2018 JiS pro MTJ a firmu Kruzik -> online funkce MTJ x fask ...
        /// </summary>
        public static bool Vyroba_Online
        {
            get { return bool.Parse(GetValue("Vyroba_Online", false.ToString())); }
            set { SetValue("Vyroba_Online", value.ToString()); }
        }

        /// <summary>
        /// Vklada novy start 
        /// </summary>
        public static bool OdvadeniSledovatCastecneOdvody
        {
            get { return bool.Parse(GetValue("OdvadeniSledovatCastecneOdvody", false.ToString())); }
            set { SetValue("OdvadeniSledovatCastecneOdvody", value.ToString()); }
        }

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
        /// Zadani materialu vyrobku po vyrobe
        /// </summary>
        public static bool Odvadeni_CasNecinnosti
        {
            get { return bool.Parse(GetValue("Odvadeni_CasNecinnosti", false.ToString())); }
            set { SetValue("Odvadeni_CasNecinnosti", value.ToString()); }
        }

        /// <summary>
        /// Zadani materialu vyrobku po vyrobe
        /// </summary>
        public static bool Odvadeni_production_onlyPositive
        {
            get { return bool.Parse(GetValue("Odvadeni_production_onlyPositive", true.ToString())); }
            set { SetValue("Odvadeni_production_onlyPositive", value.ToString()); }
        }

        /// <summary>
        /// Potlaceni hlašky ktera upozornuje na vetsi mnozstvi
        /// </summary>
        public static bool Odvadeni_production_NeupozornovatNaVetsiPocet
        {
            get { return bool.Parse(GetValue("Odvadeni_production_NeupozornovatNaVetsiPocet", true.ToString())); }
            set { SetValue("Odvadeni_production_NeupozornovatNaVetsiPocet", value.ToString()); }
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
        /// ID stroje nastaveneho stroje ...
        /// </summary>
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

        public static bool VyberZakazkyPoPrihlaseni
        {
            get { return bool.Parse(GetValue("VyberZakazkyPoPrihlaseni", false.ToString())); }
            set { SetValue("VyberZakazkyPoPrihlaseni", value.ToString()); }
        }

		public static bool PrepnutiZakazky
        {
			get { return bool.Parse(GetValue("PrepnutiZakazky", false.ToString())); }
			set { SetValue("PrepnutiZakazky", value.ToString()); }
        }

		

        /// <summary>
        /// Pouzity typ scanneru pro carove kody
        /// </summary>
        public static Fask.MST_W.Scanner.ScannerTypes ScannerType
        {
            get { return (Fask.MST_W.Scanner.ScannerTypes)Enum.Parse(typeof(Fask.MST_W.Scanner.ScannerTypes), GetValue("ScannerType", Fask.MST_W.Scanner.ScannerTypes.Unitech_HT660.ToString()), true); }
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
        /// ID udalosti prihlaseni pracovnika
        /// </summary>
        public static string UEventPracovnikOdhlaseni
        {
            get { return GetValue("UEventPracovnikOdhlaseni", Properties.Resources.UEventPracovnikOdhlaseni); }
            set { SetValue("UEventPracovnikOdhlaseni", value); }
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
        /// Logovat chyby do souboru
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
        /// Adresa web serveru, ktery terminal vyuziva k tisku
        /// </summary>
        public static string TiskWebServiceAddress
        {
            get { return GetValue("TiskWebServiceAddress", Properties.Resources.TiskWebServiceAddress); }
            set { SetValue("TiskWebServiceAddress", value); }
        }

        /// <summary>
        /// Timeout pro webovy tisk
        /// </summary>
        public static int TiskWebServiceTimeOut
        {
            get { return int.Parse(GetValue("TiskWebServiceTimeOut", Properties.Resources.TiskWebServiceTimeOut)); }
            set { SetValue("TiskWebServiceTimeOut", value.ToString()); }
        }

        /// <summary>
        /// Jednosmerny nebo obousmerny tisk 
        /// <remarks>Jednosmerny neceka na odpoved ze serveru -> OneWay</remarks>
        /// </summary>
        public static bool TiskOneWayPrint
        {
            get { return bool.Parse(GetValue("TiskOneWayPrint", Properties.Resources.TiskOneWayPrint)); }
            set { SetValue("TiskOneWayPrint", value.ToString()); }
        }

        /// <summary>
        /// nazev tiskarny pro tisk baleni
        /// </summary>
        public static string TiskBaleniTiskarna
        {            
            get { return GetValue("TiskBaleniTiskarna", Properties.Resources.TiskBaleniTiskarna); }
            set { SetValue("TiskBaleniTiskarna", value); }
        }

        /// <summary>
        /// nazev sablony pro tisk baleni
        /// </summary>
        public static string TiskBaleniSablona
        {
            get { return GetValue("TiskBaleniSablona", Properties.Resources.TiskBaleniSablona); }
            set { SetValue("TiskBaleniSablona", value); }
        }


        public static bool TiskPotvrzovaniPoctuVytisku
        {
            get { return bool.Parse(GetValue("TiskPotvrzovaniPoctuVytisku", Properties.Resources.TiskPotvrzovaniPoctuVytisku)); }
            set { SetValue("TiskPotvrzovaniPoctuVytisku", value.ToString()); }
        }


        public static string TiskPocetVytisku
        {
            get { return GetValue("TiskPocetVytisku", Properties.Resources.TiskPocetVytisku); }
            set { SetValue("TiskPocetVytisku", value); }
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
            get { return TimeSpan.Parse(GetValue("Production_UserMaxTimeSpanNoAction", Properties.Resources.Production_UserMaxTimeSpanNoAction)); }
            set { SetValue("Production_UserMaxTimeSpanNoAction", value.ToString()); }
        }

        public static string Production_MachineID_Preset
        {
            get { return GetValue("Production_MachineID_Preset", string.Empty); }
            set { SetValue("Production_MachineID_Preset", value); }
        }
               

        /// <summary>
        /// ID uzivatele posledniho odvodu vyroby
        /// </summary>
        public static string LastProductionUserID
        {
            get { return (GetValue("LastProductionUserID", string.Empty)); }
            set { SetValue("LastProductionUserID", value.ToString()); }
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
        /// Po Start Vyroba ihned Stop Vyroby
        /// </summary>
        public static bool StopVyrobaPoStartVyrobaIhned
        {
            get { return bool.Parse(GetValue("StopVyrobaPoStartVyrobaIhned", false.ToString())); }
            set { SetValue("StopVyrobaPoStartVyrobaIhned", value.ToString()); }
        }

        /// <summary>
        /// Po Stop Odvod Ihned Zahajeni Vyroby
        /// </summary>
        public static bool StopPripravaStartVyrobaIhned
        {
            get { return bool.Parse(GetValue("StopPripravaStartVyrobaIhned", Properties.Resources.StopPripravaStartVyrobaIhned)); }
            set { SetValue("StopPripravaStartVyrobaIhned", value.ToString()); }
        }


        public static int ProductionOnlineTimeout
        {
            get { return int.Parse(GetValue("ProductionOnlineTimeout", Properties.Resources.ProductionOnlineTimeout)); }
            set { SetValue("ProductionOnlineTimeout", value.ToString()); }
        }


		#region TaD Parsovani kodu

		public static Parsing.Config Parsing_Config
		{
			get
			{
				if (!PovolParsovani)
					return null;

				Parsing.Config config = new Fask.Parsing.Config(
					PovolParsovani_WeightCode,
					PovolParsovani_WeightCode_12
					);
				return config;
			}
		}


		public static bool PovolParsovani_WeightCode
		{
			get { return bool.Parse(GetValue("PovolParsovani_WeightCode", false.ToString())); }
			set { SetValue("PovolParsovani_WeightCode", value.ToString()); }
		}

		public static bool PovolParsovani_WeightCode_12
		{
			get { return bool.Parse(GetValue("PovolParsovani_WeightCode_12", false.ToString())); }
			set { SetValue("PovolParsovani_WeightCode_12", value.ToString()); }
		}

		public static bool PovolParsovani
		{
			get { return bool.Parse(GetValue("PovolParsovani", false.ToString())); }
			set { SetValue("PovolParsovani", value.ToString()); }
		}
	
		#endregion

		#region Certifikaty pro HTTPs


		public static Fask.Vyroba_W.Globals.ServerAccessCertificatesTrustType ServerAccessCertificateTrust
		{
			get 
			{
				string cert = string.IsNullOrEmpty(GetValue("ServerAccessCertificateTrust", Fask.Vyroba_W.Globals.ServerAccessCertificatesTrustType.TrustAll.ToString())) ? Fask.Vyroba_W.Globals.ServerAccessCertificatesTrustType.OnlyInstalled.ToString() : GetValue("ServerAccessCertificateTrust", Fask.Vyroba_W.Globals.ServerAccessCertificatesTrustType.TrustAll.ToString());
				return (Fask.Vyroba_W.Globals.ServerAccessCertificatesTrustType)Enum.Parse(typeof(Fask.Vyroba_W.Globals.ServerAccessCertificatesTrustType), cert, true); 
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
            get { return (ServerAccessType)Enum.Parse(typeof(ServerAccessType), GetValue("ServerAccess",ServerAccessType.Anonymous.ToString()), true); }
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
            get { return GetValue("ServerAccessDomain",string.Empty); }
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

		#region šarže

		public static bool Production_Sarze_SN_Enable
		{
			get { return bool.Parse(GetValue("Production_Sarze_SN_Enable", false.ToString())); }
			set { SetValue("Production_Sarze_SN_Enable", value.ToString()); }
		}
		
		#endregion

		#region Tisk


		public static bool Production_Tisk_Etiketa_Enable
		{
			get { return bool.Parse(GetValue("Production_Tisk_Etiketa_Enable", false.ToString())); }
			set { SetValue("Production_Tisk_Etiketa_Enable", value.ToString()); }
		}

		#endregion

		#region MyRegion

		public static bool UIHideTaskBar
		{
			get { return bool.Parse(GetValue("UIHideTaskBar", false.ToString())); }
			set { SetValue("UIHideTaskBar", value.ToString()); }
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

            m_settingsPath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase), "Settings.xml");

            if (!File.Exists(m_settingsPath))
                throw new FileNotFoundException(
                                  m_settingsPath + " could not be found.");

            System.Xml.XmlDocument xdoc = new XmlDocument();
            xdoc.Load(m_settingsPath);
            XmlElement root = xdoc.DocumentElement;
            m_settings = new NameValueCollection();
            foreach (XmlNode node in root.SelectNodes("/configuration/appSettings/add"))
            {
                m_settings.Add(node.Attributes["key"].Value, node.Attributes["value"].Value);
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
