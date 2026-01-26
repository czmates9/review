using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.IO;

namespace Fask.MST_W
{
    public class Cycle
    {
        public string CycleName = null;
        public short CisloCyklu;
        public override string ToString()
        {
            return string.Format("Èíslo : {0} Popis : {1}", CisloCyklu.ToString(), CycleName);
        }
    }
    public class TypZavoz
    {
        public string TyzZavozName;
        public override string ToString()
        {
            return string.Format("Název závozu : {0}", TyzZavozName);
        }
    }
    public class MST_Global
    {
        //Globalni objekty
        public static Config.PriorityColors PriorityColors = new Fask.MST_W.Config.PriorityColors();
        public static void PriorityColorsLoad()
        {
            try
            {
                if (!File.Exists(Main.ConfigPriorityColors))
                    return;

                PriorityColors.ReadXml(Main.ConfigPriorityColors, System.Data.XmlReadMode.IgnoreSchema);
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
        }
        public static void PriorityColorsSave()
        {
            try
            {
                PriorityColors.WriteXml(Main.ConfigPriorityColors, System.Data.XmlWriteMode.IgnoreSchema);
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
        }

        // Nazvy nekterych prvku
        public static string MNName = Properties.Resources.MNName; //"Modelové èíslo";
        public static string MNCode = Properties.Resources.MNCode; //"MN";
        public static string SNName = Properties.Resources.SNName;
        public static string SNCode = Properties.Resources.SNCode;
        public static string SWName = Properties.Resources.SWName;
        public static string SWCode = Properties.Resources.SWCode;
        public static string DVName = Properties.Resources.DVName;
        public static string DVCode = Properties.Resources.DVCode;
        public static string PON_NAME = Properties.Resources.PON_Name;
        public static string LC_NAME = Properties.Resources.LCName;
        public static string REZ1_PRIJ_NAME = Properties.Resources.REZ1_Prijem_Name;
        public static string REZ2_PRIJ_NAME = Properties.Resources.REZ2_Prijem_Name;

        public static string REZ1_VYDE_NAME = Properties.Resources.REZ1_Vydej_Name;
        public static string REZ2_VYDE_NAME = Properties.Resources.REZ2_Vydej_Name;

        public static string REZ1_PROD_NAME = Properties.Resources.REZ1_Prod_Name;
        public static string REZ2_PROD_NAME = Properties.Resources.REZ2_Prod_Name;
        public static string REZ3_PROD_NAME = Properties.Resources.REZ3_Prod_Name;
        public static string REZ4_PROD_NAME = Properties.Resources.REZ4_Prod_Name;

        public static string ScannerTypeName = "None";
        /// <summary>
        /// Promenne pro praci s dynamickym linkovanim modulu (Lorecnc,...)
        /// </summary>
        #region dynamic loading modules

        //seznam dynamickych modulu
        public static List<Classes.AssemblyInfo> assemblies = new List<Classes.AssemblyInfo>();

        //seznam cest k modulum
        //public static List<string> dllPath = new List<string>();
        //seznam nazvu buttonu k jednotlivym modulum
        //public static List<string> buttonName = new List<string>();
        //interface pro praci se scannerem v modulech
        //public static Fask.ScannerProvider.IScannerProvider scanner = null;
        #endregion

        public static FormBorderStyle FormBorderStyleGlobal
        {
            get { return Settings.UIHideWindowText ? FormBorderStyle.None : FormBorderStyle.FixedSingle; }
        }

        private static bool _sqlCe = true;
        /// <summary>
        /// Je pouzita verianta s databazi SQL CE
        /// </summary>
        public static bool SqlCe
        {
            get { return _sqlCe; }
            set { _sqlCe = value; }
        }

        private static bool _lokalizacePovolit;
        public static bool LokalizacePovolit
        {
            get { return _lokalizacePovolit; }
            set { _lokalizacePovolit = value; }
        }

        private static bool _LokalizaceVlastniPovolit;
        public static bool LokalizaceVlastniPovolit
        {
            get { return _LokalizaceVlastniPovolit; }
            set { _LokalizaceVlastniPovolit = value; }
        }

        //private static Localization.LocalizationSupport.LocalType _lokalizaceZvolena = Fask.MST_W.Localization.LocalizationSupport.LocalType.cs;
        //public static Localization.LocalizationSupport.LocalType LokalizaceZvolena
        //{
        //    get { return _lokalizaceZvolena; }
        //    set { _lokalizaceZvolena = value; }
        //}

        private static Fask.Localization.LocalizationSupport.LocalType _lokalizaceZvolena = Fask.Localization.LocalizationSupport.LocalType.cs;
        public static Fask.Localization.LocalizationSupport.LocalType LokalizaceZvolena
        {
            get { return _lokalizaceZvolena; }
            set { _lokalizaceZvolena = value; }
        }

        //private static string _lokalizaceZvolena;
        //public static string LokalizaceZvolena
        //{
        //    get { return _lokalizaceZvolena; }
        //    set { _lokalizaceZvolena = value; }
        //}

        private static string _userPwd = string.Empty;
        public static string UserPwd
        {
            get { return _userPwd; }
            set { _userPwd = value; }
        }

        private static int _userID = 0;
        public static int UserID
        {
            get { return _userID; }
            set { _userID = value; }
        }

        private static string _userLoginName = "";
        public static string UserLoginName
        {
            get { return _userLoginName; }
            set { _userLoginName = value; }
        }

        private static byte _terminalID = 0;
        /// <summary>
        /// ID terminalu
        /// </summary>
        public static byte TerminalID
        {
            get { return _terminalID; }
            set { _terminalID = value; }
        }

        /// <summary>
        /// Servis
        /// </summary>
        private static bool _ServisEnable = false;
        public static bool ServisEnable
        {
            get { return _ServisEnable; }
            set { _ServisEnable = value; }
        }

        private static bool _ServisShowInMST = true;
        public static bool ServisShowInMST
        {
            get { return _ServisShowInMST; }
            set { _ServisShowInMST = value; }
        }

        //private static bool _HuskyVratkaShowInMST = true;
        //public static bool HuskyVratkaShowInMST
        //{
        //    get { return _HuskyVratkaShowInMST; }
        //    set { _HuskyVratkaShowInMST = value; }
        //}

        private static int _ServisTimeoutOnline = 10000;
        public static int ServisTimeoutOnline
        {
            get { return _ServisTimeoutOnline; }
            set { _ServisTimeoutOnline = value; }
        }

        private static bool _ServisDavkaOtevritIhnedPoStazeni = false;
        public static bool ServisDavkaOtevritIhnedPoStazeni
        {
            get { return _ServisDavkaOtevritIhnedPoStazeni; }
            set { _ServisDavkaOtevritIhnedPoStazeni = value; }
        }

        private static bool _ServisFiltrZobrazeniPovolit = false;
        public static bool ServisFiltrZobrazeniPovolit
        {
            get { return _ServisFiltrZobrazeniPovolit; }
            set { _ServisFiltrZobrazeniPovolit = value; }
        }

        private static bool _ServisAutomatickaZmenaStavuPovolit = false;
        /// <summary>
        /// Pokud je zmena stavu a navazujici stav je pouze jeden, dojde k automatickemu zvoleni.
        /// </summary>
        public static bool ServisAutomatickaZmenaStavuPovolit
        {
            get { return _ServisAutomatickaZmenaStavuPovolit; }
            set { _ServisAutomatickaZmenaStavuPovolit = value; }
        }

        private static bool _ServisPozadovatPotvrzeniZmenyStavu = false;
        /// <summary>
        /// Urceni, zdali se bude zobrazovat dialog pro potvrzeni zmeny stavu
        /// </summary>
        public static bool ServisPozadovatPotvrzeniZmenyStavu
        {
            get { return _ServisPozadovatPotvrzeniZmenyStavu; }
            set { _ServisPozadovatPotvrzeniZmenyStavu = value; }
        }

        private static bool _ServisPozadovatPotvrzeniZmenyCinnosti = false;
        /// <summary>
        /// Urceni, zdali se bude zobrazovat dialog pro potvrzeni zmeny cinnosti
        /// </summary>
        public static bool ServisPozadovatPotvrzeniZmenyCinnosti
        {
            get { return _ServisPozadovatPotvrzeniZmenyCinnosti; }
            set { _ServisPozadovatPotvrzeniZmenyCinnosti = value; }
        }

        private static bool _ServisSynchronizaceZdrojePoVyberuPovolit = true;
        /// <summary>
        /// Povoleni synchronizace zdroje ihned po vyberu zdroje.
        /// </summary>
        public static bool ServisSynchronizaceZdrojePoVyberuPovolit
        {
            get { return _ServisSynchronizaceZdrojePoVyberuPovolit; }
            set { _ServisSynchronizaceZdrojePoVyberuPovolit = value; }
        }

        private static bool _ServisPrehratZvukPoVyberuMoznosti = false;
        /// <summary>
        /// Pøehrát zvuk pøi potvrzení voleb
        /// </summary>
        public static bool ServisPrehratZvukPoVyberuMoznosti
        {
            get { return _ServisPrehratZvukPoVyberuMoznosti; }
            set { _ServisPrehratZvukPoVyberuMoznosti = value; }
        }

        private static bool _ServisStatusBarZobrazitNazevStavu = true;
        /// <summary>
        /// Zobrazit název stavu ve status baru.
        /// </summary>
        public static bool ServisStatusBarZobrazitNazevStavu
        {
            get { return _ServisStatusBarZobrazitNazevStavu; }
            set { _ServisStatusBarZobrazitNazevStavu = value; }
        }

        private static bool _ServisStatusBarZobrazitIdZdroje = true;
        /// <summary>
        /// Zobrazit id zdroje ve status baru.
        /// </summary>
        public static bool ServisStatusBarZobrazitIdZdroje
        {
            get { return _ServisStatusBarZobrazitIdZdroje; }
            set { _ServisStatusBarZobrazitIdZdroje = value; }
        }

        private static bool _ServisStatusBarZobrazitOznaceniZdroje = true;
        /// <summary>
        /// Zobrazit oznaceni zdroje ve status baru.
        /// </summary>
        public static bool ServisStatusBarZobrazitOznaceniZdroje 
        {
            get { return _ServisStatusBarZobrazitOznaceniZdroje; }
            set { _ServisStatusBarZobrazitOznaceniZdroje = value; }
        }

        private static bool _ServisZobrazitInformaciOUkonceniStavu= false;
        /// <summary>
        /// Zobrazit informaci o ukonceni stavu v pripade, ze je zaskrtnuto v konfiguraci: "Po ukonèení stavu návrat do seznamu".
        /// </summary>
        public static bool ServisZobrazitInformaciOUkonceniStavu
        {
            get { return _ServisZobrazitInformaciOUkonceniStavu; }
            set { _ServisZobrazitInformaciOUkonceniStavu = value; }
        }

        private static bool _ServisStatusBarZobrazitTypZdroje = true;
        /// <summary>
        /// Zobrazit typ zdroje ve status baru.
        /// </summary>
        public static bool ServisStatusBarZobrazitTypZdroje
        {
            get { return _ServisStatusBarZobrazitTypZdroje; }
            set { _ServisStatusBarZobrazitTypZdroje = value; }
        }

        private static int _ServisAutoUpdateInterval = 1000;
        public static int ServisAutoUpdateInterval
        {
            get { return _ServisAutoUpdateInterval; }
            set { _ServisAutoUpdateInterval = value; }
        }

        private static bool _ServisAllowCamera = true;
        [Obsolete("Od providera na foceni se nepouziva")]
        public static bool ServisAllowCamera
        {
            get { return _ServisAllowCamera; }
            set { _ServisAllowCamera = value; }
        }

        private static bool _ServisDavkoveZpracovani = false;
        public static bool ServisDavkoveZpracovani
        {
            get { return _ServisDavkoveZpracovani; }
            set { _ServisDavkoveZpracovani = value; }
        }

        private static bool _ServisVyberOdberatelPovolit = false;
        public static bool ServisVyberOdberatelPovolit 
        {
            get { return _ServisVyberOdberatelPovolit; }
            set { _ServisVyberOdberatelPovolit = value; }
        }

        private static bool _ServisVyberOkruhPovolit = false;
        public static bool ServisVyberOkruhPovolit
        {
            get { return _ServisVyberOkruhPovolit; }
            set { _ServisVyberOkruhPovolit = value; }
        }

        private static bool _ServisUkonceniStavuNavrat = true;
        public static bool ServisUkonceniStavuNavrat
        {
            get { return _ServisUkonceniStavuNavrat; }
            set { _ServisUkonceniStavuNavrat = value; }
        }

        private static int _ServisTimeoutSynchronize = 20000;
        public static int ServisTimeoutSynchronize
        {
            get { return _ServisTimeoutSynchronize; }
            set { _ServisTimeoutSynchronize = value; }
        }

        private static bool _ServisOdeslaniDatNaPozadiPovolit = true;
        public static bool ServisOdeslaniDatNaPozadiPovolit 
        {
            get { return _ServisOdeslaniDatNaPozadiPovolit; }
            set { _ServisOdeslaniDatNaPozadiPovolit = value; }
        }

        private static bool _ServisOdeslaniDatPoUkonceniZadavaniPovolit = true;
        public static bool ServisOdeslaniDatPoUkonceniZadavaniPovolit
        {
            get { return _ServisOdeslaniDatPoUkonceniZadavaniPovolit; }
            set { _ServisOdeslaniDatPoUkonceniZadavaniPovolit = value; }
        }

        #region TaD Dialogy Servis

        private static bool _Servis_DialogOpusteniModulu = true;
        public static bool Servis_DialogOpusteniModulu
        {
            get { return _Servis_DialogOpusteniModulu; }
            set { _Servis_DialogOpusteniModulu = value; }
        }

        #endregion

        // Tasks ...
        private static bool _TasksEnable = true;
        public static bool TasksEnable
        {
            get { return _TasksEnable; }
            set { _TasksEnable = value; }
        }

        private static bool _TasksShowInMST = false;
        public static bool TasksShowInMST
        {
            get { return _TasksShowInMST; }
            set { _TasksShowInMST = value; }
        }

        private static int _TasksTimeout = 10000;
        public static int TasksTimeout
        {
            get { return _TasksTimeout; }
            set { _TasksTimeout = value; }
        }

        private static int _TasksSynchronizationInterval = 60000;
        public static int TasksSynchronizationInterval
        {
            get { return _TasksSynchronizationInterval; }
            set { _TasksSynchronizationInterval = value; }
        }

        private static int _TasksNotifyDialogShowSeconds = 20;
        public static int TasksNotifyDialogShowSeconds
        {
            get { return _TasksNotifyDialogShowSeconds; }
            set { _TasksNotifyDialogShowSeconds = value; }
        }


        // Events ...
        private static bool _EventsEnable = true;
        public static bool EventsEnable
        {
            get { return _EventsEnable; }
            set { _EventsEnable = value; }
        }

        private static bool _EventsShowInMST = false;
        public static bool EventsShowInMST
        {
            get { return _EventsShowInMST; }
            set { _EventsShowInMST = value; }
        }

        private static bool _EventsOnline = true;
        public static bool EventsOnline
        {
            get { return _EventsOnline; }
            set { _EventsOnline = value; }
        }

        private static int _EventsOnlineTimeout = 10000;
        public static int EventsOnlineTimeout
        {
            get { return _EventsOnlineTimeout; }
            set { _EventsOnlineTimeout = value; }
        }

        private static bool _EventsOnlineConfirm = true;
        public static bool EventsOnlineConfirm
        {
            get { return _EventsOnlineConfirm; }
            set { _EventsOnlineConfirm = value; }
        }

        private static int _EventsSynchronizationInterval = 30000;
        public static int EventsSynchronizationInterval
        {
            get { return _EventsSynchronizationInterval; }
            set { _EventsSynchronizationInterval = value; }
        }


        private static bool _OnlineObjednavkaDetailPovolit = true;
        public static bool OnlineObjednavkaDetailPovolit
        {
            get { return _OnlineObjednavkaDetailPovolit; }
            set { _OnlineObjednavkaDetailPovolit = value; }
        }

        private static bool _OnlinePolozkaKusuPovolit = true;
        public static bool OnlinePolozkaKusuPovolit
        {
            get { return _OnlinePolozkaKusuPovolit; }
            set { _OnlinePolozkaKusuPovolit = value; }
        }

        private static bool _OnlinePolozkaKusuNaSkladePovolit = true;
        public static bool OnlinePolozkaKusuNaSkladePovolit
        {
            get { return _OnlinePolozkaKusuNaSkladePovolit; }
            set { _OnlinePolozkaKusuNaSkladePovolit = value; }
        }

        private static bool _OnlinePolozkaDetailPovolit = true;
        public static bool OnlinePolozkaDetailPovolit
        {
            get { return _OnlinePolozkaDetailPovolit; }
            set { _OnlinePolozkaDetailPovolit = value; }
        }

        //nazvy sluzeb pro server
        public static string ServerAddressSqliteDBs
        {
            get { return ServerAddress + @"SQLiteDBs/"; }
        }

        private static string _serverAddress = "192.168.1.121:80";
        public static string ServerAddress
        {
			get
			{
				if (MST_Global.isHTTPS)
					return string.Format("https://{0}/", _serverAddress);
				else
					return string.Format("http://{0}/", _serverAddress);
			}
        }

		public static string Adresa_API
		{
			get { return _serverAddress; }
			set { _serverAddress = value; }
		}


		private static string _autorizace_API = "MDox";
		public static string Autorizace_API
		{
			get { return _autorizace_API; }
			set { _autorizace_API = value; }
		}

		private static string _api_konstant = "api";
		public static string API_konstant
		{
			get { return _api_konstant; }
			set { _api_konstant = value; }
		}

		private static bool _isHTTPS = false;
		public static bool isHTTPS
		{
			get { return _isHTTPS; }
			set { _isHTTPS = value; }
		}


		private static int _serviceTimeOut = 15000;
		/// <summary>
		/// Timeout pristupu ke sluzbe
		/// </summary>
		public static int ServiceTimeOut
		{
			get { return _serviceTimeOut; }
			set { _serviceTimeOut = value; }
		}
        /*
        private static string _PrintServerTemplateNamePrijemPredloha = string.Empty;
        public static string PrintServerTemplateNamePrijemPredloha
        {
            get { return _PrintServerTemplateNamePrijemPredloha; }
            set { _PrintServerTemplateNamePrijemPredloha = value; }
        }

        private static string _PrintServerTemplateNamePrijemNasnimane = string.Empty;
        public static string PrintServerTemplateNamePrijemNasnimane
        {
            get { return _PrintServerTemplateNamePrijemNasnimane; }
            set { _PrintServerTemplateNamePrijemNasnimane = value; }
        }

        private static string _PrintServerTemplateNameVydejPredloha = string.Empty;
        public static string PrintServerTemplateNameVydejPredloha
        {
            get { return _PrintServerTemplateNameVydejPredloha; }
            set { _PrintServerTemplateNameVydejPredloha = value; }
        }

        private static string _PrintServerTemplateNameVydejNasnimane = string.Empty;
        public static string PrintServerTemplateNameVydejNasnimane
        {
            get { return _PrintServerTemplateNameVydejNasnimane; }
            set { _PrintServerTemplateNameVydejNasnimane = value; }
        }

        private static string _PrintServerTemplateNameVydejPalListek = string.Empty;
        public static string PrintServerTemplateNameVydejPalListek
        {
            get { return _PrintServerTemplateNameVydejPalListek; }
            set { _PrintServerTemplateNameVydejPalListek = value; }
        }

        private static string _PrintServerTemplateNameProdejPredloha = string.Empty;
        public static string PrintServerTemplateNameProdejPredloha
        {
            get { return _PrintServerTemplateNameProdejPredloha; }
            set { _PrintServerTemplateNameProdejPredloha = value; }
        }

        private static string _PrintServerTemplateNameProdejNasnimane = string.Empty;
        public static string PrintServerTemplateNameProdejNasnimane
        {
            get { return _PrintServerTemplateNameProdejNasnimane; }
            set { _PrintServerTemplateNameProdejNasnimane = value; }
        }

        private static string _PrintServerTemplateNameInventuraPredloha = string.Empty;
        public static string PrintServerTemplateNameInventuraPredloha
        {
            get { return _PrintServerTemplateNameInventuraPredloha; }
            set { _PrintServerTemplateNameInventuraPredloha = value; }
        }

        private static string _PrintServerTemplateNameInventuraNasnimane = string.Empty;
        public static string PrintServerTemplateNameInventuraNasnimane
        {
            get { return _PrintServerTemplateNameInventuraNasnimane; }
            set { _PrintServerTemplateNameInventuraNasnimane = value; }
        }
        */


        public enum ServerAccessType
        {
            Anonymous,
            Credentials
        }
        private static ServerAccessType _serverAccess = ServerAccessType.Anonymous;
        public static ServerAccessType ServerAccess
        {
            get { return _serverAccess; }
            set { _serverAccess = value; }
        }

        private static string _serverAccessUsername = string.Empty;
        public static string ServerAccessUsername
        {
            get { return _serverAccessUsername; }
            set { _serverAccessUsername = value; }
        }

        private static string _serverAccessPassword = string.Empty;
        public static string ServerAccessPassword
        {
            get { return _serverAccessPassword; }
            set { _serverAccessPassword = value; }
        }

        private static string _serverAccessDomain = string.Empty;
        public static string ServerAccessDomain
        {
            get { return _serverAccessDomain; }
            set { _serverAccessDomain = value; }
        }

        private static bool _serverAccessPreauthenticate = false;
        public static bool ServerAccessPreauthenticate
        {
            get { return _serverAccessPreauthenticate; }
            set { _serverAccessPreauthenticate = value; }
        }

        private static bool _serverAccessAllowRedirection = false;
        public static bool ServerAccessAllowRedirection
        {
            get { return _serverAccessAllowRedirection; }
            set { _serverAccessAllowRedirection = value; }
        }

        private static bool _serverAccessAllowDecompression = false;
        public static bool ServerAccessAllowDecompression
        {
            get { return _serverAccessAllowDecompression; }
            set { _serverAccessAllowDecompression = value; }
        }

        private static int _LogUploadInterval = 100;
        public static int LogUploadInterval
        {
            get { return _LogUploadInterval; }
            set { _LogUploadInterval = value; }
        }

        public enum ServerAccessCertificatesTrustType
        {
            OnlyInstalled,
            TrustAll,
            TrustQuery
        }

        private static ServerAccessCertificatesTrustType _serverAccessCertificateTrust = ServerAccessCertificatesTrustType.OnlyInstalled;
		public static ServerAccessCertificatesTrustType ServerAccessCertificateTrust
		{
			get { return _serverAccessCertificateTrust; }
			set
			{
				_serverAccessCertificateTrust = value;
				// info : 26.3.2020 JiS : presunuto z MainMSTw na toto misto, kdyz se nastavuje neco, tak hned tady ... jine misto nedava smysl ... 
				switch (_serverAccessCertificateTrust)
				{
					case MST_Global.ServerAccessCertificatesTrustType.TrustAll:
						System.Net.ServicePointManager.CertificatePolicy = new ServerAccess.CertificatesPolicy.TrustAllCertificatePolicy();
						break;
					case MST_Global.ServerAccessCertificatesTrustType.TrustQuery:
						System.Net.ServicePointManager.CertificatePolicy = new ServerAccess.CertificatesPolicy.QueryTrustCertificatePolicy();
						break;
					case MST_Global.ServerAccessCertificatesTrustType.OnlyInstalled:
					default:
						System.Net.ServicePointManager.CertificatePolicy = null;    // TODO : ?? je toto spravne ??  => otestovat .. .!!!
						break;
				}

			}
		}
        

        //private static Scanner.ScannerTypes _ScannerType = Fask.MST_W..Scanner.ScannerTypes.Symbol_MC3000;
        //public static Scanner.ScannerTypes ScannerType
        //{
        //    get { return _ScannerType; }
        //    set { _ScannerType = value; }
        //}

        private static Scanner.ScannerRFIDTypes _RFIDScannerType = Fask.MST_W.Scanner.ScannerRFIDTypes.None;
        public static Scanner.ScannerRFIDTypes RFIDScannerType
        {
            get { return _RFIDScannerType; }
            set { _RFIDScannerType = value; }
        }


        /// <summary>
        /// Velkost Pamete 
        /// </summary>
        internal static string _MemorySize = "5";
        public static string MemorySize
        {
            get { return _MemorySize; }
            set { _MemorySize = value; }
        }

        /// <summary>
        /// Velkost Pamete 
        /// </summary>
        internal static bool _MemoryChecked = false;
        public static bool MemoryChecked
        {
            get { return _MemoryChecked;  }
            set { _MemoryChecked = value; }
        }
        

        internal static string _Storage = string.Empty;
        public static string Storage
        {
            get
            {
                if (_Storage == string.Empty)
                    return Main.DataDir;
                else
                    return _Storage;
            }
            set
            {
                try
                {
                    //string fullpath = System.IO.Path.GetFullPath(value);
                    //if (!System.IO.Directory.Exists(fullpath))
                    //    System.IO.Directory.CreateDirectory(fullpath);
                    //_Storage = fullpath;
                    //Main.WrkDir = _Storage + @"\";

                    _Storage = value;
                }
                catch (Exception ex)
                {
                    Logging.Log.Write(ex.Message, "Storage Set");
                }
            }
        }

        private static bool _ShowPanelButtons;
        public static bool ShowPanelButtons
        {
            get { return _ShowPanelButtons; }
            set { _ShowPanelButtons = value; }
        }


        private static bool _inventura1 = false;
        public static bool Inventura1
        {
            get { return _inventura1; }
            set { _inventura1 = value; }
        }

        private static bool _inventura2 = false;
        public static bool Inventura2
        {
            get { return _inventura2; }
            set { _inventura2 = value; }
        }

        private static bool _vydej = false;
        public static bool Vydej
        {
            get { return _vydej; }
            set { _vydej = value; }
        }

        //private static bool _huskyVratka = false;
        //public static bool HuskyVratka
        //{
        //    get { return _huskyVratka; }
        //    set { _huskyVratka = value; }
        //}

        private static bool _prodej = false;

        public static bool Prodej
        {
            get { return _prodej; }
            set { _prodej = value; }
        }

        private static bool _prijem = false;

        public static bool Prijem
        {
            get { return _prijem; }
            set { _prijem = value; }
        }

        private static bool _expedice = false;

        public static bool Expedice
        {
            get { return _expedice; }
            set { _expedice = value; }
        }

        //===========================================
        //pridano pro povoleni rfid
        private static bool _RFIDPovolitUHF = false;
        public static bool RFIDPovolitUHF
        {
            get { return _RFIDPovolitUHF; }
            set { _RFIDPovolitUHF = value; }
        }

        private static bool _RFIDukladatNacitatText = false;
        public static bool RFIDUkladatNacitatText
        {
            get { return _RFIDukladatNacitatText; }
            set { _RFIDukladatNacitatText = value; }
        }

        private static int _RFIDPowerLevel = 200;
        public static int RFIDPowerLevel
        {
            get { return _RFIDPowerLevel; }
            set { _RFIDPowerLevel = value; }
        }

        /// <summary>
        /// Pamete ktere se vycitavaju s Tagu
        /// </summary>
        private static bool _RFID_memoryEPC = true;
        public static bool RFID_memoryEPC
        {
            get { return _RFID_memoryEPC; }
            set { _RFID_memoryEPC = value; }
        }

        private static bool _RFID_memoryUSER = true;
        public static bool RFID_memoryUSER
        {
            get { return _RFID_memoryUSER; }
            set { _RFID_memoryUSER = value; }
        }

        private static bool _RFID_memoryRESERVED = true;
        public static bool RFID_memoryRESERVED
        {
            get { return _RFID_memoryRESERVED; }
            set { _RFID_memoryRESERVED = value; }
        }

        private static bool _RFID_memoryTID = true;
        public static bool RFID_memoryTID
        {
            get { return _RFID_memoryTID; }
            set { _RFID_memoryTID = value; }
        }


        //===========================================

        //Sound scanner po nacteni

        private static bool _OnScannerSound_Inventura1_sqlc = false;
        public static bool OnScannerSound_Inventura1_sqlc
        {
            get { return _OnScannerSound_Inventura1_sqlc; }
            set { _OnScannerSound_Inventura1_sqlc = value; }
        }

        private static bool _OnScannerSound_Inventura2 = false;
        public static bool OnScannerSound_Inventura2
        {
            get { return _OnScannerSound_Inventura2; }
            set { _OnScannerSound_Inventura2 = value; }
        }

        private static bool _OnScannerSound_Expedice = false;
        public static bool OnScannerSound_Expedice
        {
            get { return _OnScannerSound_Expedice; }
            set { _OnScannerSound_Expedice = value; }
        }

        private static bool _OnScannerSound_Online = false;
        public static bool OnScannerSound_Online
        {
            get { return _OnScannerSound_Online; }
            set { _OnScannerSound_Online = value; }
        }


        private static bool _OnScannerSound_Prijem_4 = false;
        public static bool OnScannerSound_Prijem_4
        {
            get { return _OnScannerSound_Prijem_4; }
            set { _OnScannerSound_Prijem_4 = value; }
        }


        private static bool _OnScannerSound_Prodej_3 = false;
        public static bool OnScannerSound_Prodej_3
        {
            get { return _OnScannerSound_Prodej_3; }
            set { _OnScannerSound_Prodej_3 = value; }
        }


        private static bool _OnScannerSound_Vydej_3 = false;
        public static bool OnScannerSound_Vydej_3
        {
            get { return _OnScannerSound_Vydej_3; }
            set { _OnScannerSound_Vydej_3 = value; }
        }


        private static bool _OnScannerSound_ServisModul = false;
        public static bool OnScannerSound_ServisModul
        {
            get { return _OnScannerSound_ServisModul; }
            set { _OnScannerSound_ServisModul = value; }
        }


        private static bool _OnScannerSound_Forms = true;
        public static bool OnScannerSound_Forms
        {
            get { return _OnScannerSound_Forms; }
            set { _OnScannerSound_Forms = value; }
        }
        //===========================================

        //mena modulov
        private static string _inventura1Name = "Inventura1";
        private static string _inventura2Name = "Inventura2";
        private static string _vydejName = "Výdej";
        private static string _prodejName = "Prodej";
        private static string _prijemName = "Pøíjem";
        private static string _expediceName = "Expedice";
        //private static string _huskyVratkaName = "Vratka z komise";
        //vlastnosti mien
        public static string Inventura1Name
        {
            get { return _inventura1Name; }
            set { _inventura1Name = value; }
        }
        public static string Inventura2Name
        {
            get { return _inventura2Name; }
            set { _inventura2Name = value; }
        }
        public static string VydejName
        {
            get { return _vydejName; }
            set { _vydejName = value; }
        }
        public static string ProdejName
        {
            get { return _prodejName; }
            set { _prodejName = value; }
        }
        public static string PrijemName
        {
            get { return _prijemName; }
            set { _prijemName = value; }
        }
        public static string ExpediceName
        {
            get { return _expediceName; }
            set { _expediceName = value; }
        }
        //public static string HuskyVratkaName
        //{
        //    get { return _huskyVratkaName; }
        //    set { _huskyVratkaName = value; }
        //}

        public const string MODEL_MEMORY = "MEMORY";
        public const string MODEL_CODEBOOK = "CODEBOOK";

        private static string _prijemModel = MODEL_MEMORY;
        public static string PrijemModel
        {
            get { return _prijemModel; }
            set { _prijemModel = value; }
        }

        private static bool _Inventura2ShowInMST = false;
        public static bool Inventura2ShowInMST
        {
            get { return _Inventura2ShowInMST; }
            set { _Inventura2ShowInMST = value; }
        }

        private static bool _Inventura2DotazKancl = true;
        public static bool Inventura2DotazKancl
        {
            get { return _Inventura2DotazKancl; }
            set { _Inventura2DotazKancl = value; }
        }

        private static bool _Inventura2DotazOsoba = false;
        public static bool Inventura2DotazOsoba
        {
            get { return _Inventura2DotazOsoba; }
            set { _Inventura2DotazOsoba = value; }
        }

        private static bool _Inventura2DotazStredisko = false;
        public static bool Inventura2DotazStredisko
        {
            get { return _Inventura2DotazStredisko; }
            set { _Inventura2DotazStredisko = value; }
        }

        private static bool _Inventura2DotazLokace = false;
        public static bool Inventura2DotazLokace
        {
            get { return _Inventura2DotazLokace; }
            set { _Inventura2DotazLokace = value; }
        }


        private static int _Inventura2ChunkTimeout = 10000;
        public static int Inventura2ChunkTimeout
        {
            get { return _Inventura2ChunkTimeout; }
            set { _Inventura2ChunkTimeout = value; }
        }

        #region TaD Dialogy Inventura2

        private static bool _Inventura2_DialogOpusteniModulu = true;
        public static bool Inventura2_DialogOpusteniModulu
        {
            get { return _Inventura2_DialogOpusteniModulu; }
            set { _Inventura2_DialogOpusteniModulu = value; }
        }

        #endregion


        private static string _Inventura1REZ1Nazev = "Rezerva 1";
        public static string Inventura1REZ1Nazev
        {
            get { return _Inventura1REZ1Nazev; }
            set { _Inventura1REZ1Nazev = value; }
        }

        private static bool _Inventura1REZ1IsNumber = true;
        public static bool Inventura1REZ1IsNumber
        {
            get { return _Inventura1REZ1IsNumber; }
            set { _Inventura1REZ1IsNumber = value; }
        }

        private static bool _Inventura1REZ1Mandatory = true;
        public static bool Inventura1REZ1Mandatory
        {
            get { return _Inventura1REZ1Mandatory; }
            set { _Inventura1REZ1Mandatory = value; }
        }

        private static string _Inventura1REZ2Nazev = "Rezerva 2";
        public static string Inventura1REZ2Nazev
        {
            get { return _Inventura1REZ2Nazev; }
            set { _Inventura1REZ2Nazev = value; }
        }

        private static bool _Inventura1REZ2IsNumber = true;
        public static bool Inventura1REZ2IsNumber
        {
            get { return _Inventura1REZ2IsNumber; }
            set { _Inventura1REZ2IsNumber = value; }
        }

        private static bool _Inventura1REZ2Mandatory = true;
        public static bool Inventura1REZ2Mandatory
        {
            get { return _Inventura1REZ2Mandatory; }
            set { _Inventura1REZ2Mandatory = value; }
        }

        private static int _Inventura1ChunkTimeout = 10000;
        public static int Inventura1ChunkTimeout
        {
            get { return _Inventura1ChunkTimeout; }
            set { _Inventura1ChunkTimeout = value; }
        }

        private static int _Inventura1OnlineTimeout = 10000;
        public static int Inventura1OnlineTimeout
        {
            get { return _Inventura1OnlineTimeout; }
            set { _Inventura1OnlineTimeout = value; }
        }

        private static bool _Inventura1OnlineKontrola = false;
        public static bool Inventura1OnlineKontrola
        {
            get { return _Inventura1OnlineKontrola; }
            set { _Inventura1OnlineKontrola = value; }
        }

        private static bool _Inventura1PolozkaNasnimatPouzeJednou = false;
        public static bool Inventura1PolozkaNasnimatPouzeJednou
        {
            get { return _Inventura1PolozkaNasnimatPouzeJednou; }
            set { _Inventura1PolozkaNasnimatPouzeJednou = value; }
        }

        private static bool _Inventura1PouzitCiselnikSkladu = false;
        public static bool Inventura1PouzitCiselnikSkladu
        {
            get { return _Inventura1PouzitCiselnikSkladu; }
            set { _Inventura1PouzitCiselnikSkladu = value; }
        }

        private static bool _Inventura1PolozkyVyberJenScannerem = false;
        public static bool Inventura1PolozkyVyberJenScannerem
        {
            get { return _Inventura1PolozkyVyberJenScannerem; }
            set { _Inventura1PolozkyVyberJenScannerem = value; }
        }

		private static bool _Inventura1ParsovaniCarovehoKoduPovolit = false;
		public static bool Inventura1ParsovaniCarovehoKoduPovolit
		{
			get { return _Inventura1ParsovaniCarovehoKoduPovolit; }
			set { _Inventura1ParsovaniCarovehoKoduPovolit = value; }
		}

		private static bool _Inventura1KontrolaSarzeUParsovanehoCarovehoKoduPovolit = false;
		public static bool Inventura1KontrolaSarzeUParsovanehoCarovehoKoduPovolit
		{
			get { return _Inventura1KontrolaSarzeUParsovanehoCarovehoKoduPovolit; }
			set { _Inventura1KontrolaSarzeUParsovanehoCarovehoKoduPovolit = value; }
		}

		private static bool _Inventura1ZobrazitDialogZadaniMnozstviParsovanehoKodu = false;
				public static bool Inventura1ZobrazitDialogZadaniMnozstviParsovanehoKodu
		{
			get { return _Inventura1ZobrazitDialogZadaniMnozstviParsovanehoKodu; }
			set { _Inventura1ZobrazitDialogZadaniMnozstviParsovanehoKodu = value; }
		}


        #region TaD Dialogy Inventura1

        private static bool _Inventura1_DialogOpusteniModulu = true;
        public static bool Inventura1_DialogOpusteniModulu
        {
            get { return _Inventura1_DialogOpusteniModulu; }
            set { _Inventura1_DialogOpusteniModulu = value; }
        }

        #endregion

        private static bool _Inventura1ShowInMST = false;
        public static bool Inventura1ShowInMST
        {
            get { return _Inventura1ShowInMST; }
            set { _Inventura1ShowInMST = value; }
        }

        private static bool _vydejLocationQuestion = false;
        public static bool VydejLocationQuestion
        {
            get { return _vydejLocationQuestion; }
            set { _vydejLocationQuestion = value; }
        }

        private static bool _vydejGenerovaniPrikazuPozadovatSklad = false;
        public static bool VydejGenerovaniPrikazuZadatSklad
        {
            get { return _vydejGenerovaniPrikazuPozadovatSklad; }
            set { _vydejGenerovaniPrikazuPozadovatSklad = value; }
        }

        private static bool _vydejItemTypeQuestion = false;

        public static bool VydejItemTypeQuestion
        {
            get { return _vydejItemTypeQuestion; }
            set { _vydejItemTypeQuestion = value; }
        }

        private static bool _vydejSkladPouzit = false;
        public static bool VydejSkladPouzit
        {
            get { return _vydejSkladPouzit; }
            set { _vydejSkladPouzit = value; }
        }

        private static string _vydejSkladID = string.Empty;
        /// <summary>
        /// ID vychoziho skladu vydeje
        /// </summary>
        public static string VydejSkladID
        {
            get { return _vydejSkladID; }
            set { _vydejSkladID = value; }
        }

        private static bool _vydejParsovaniCarovehoKoduPovolit = false;
        public static bool VydejParsovaniCarovehoKoduPovolit
        {
            get { return _vydejParsovaniCarovehoKoduPovolit; }
            set { _vydejParsovaniCarovehoKoduPovolit = value; }
        }

        private static bool _vydej_Items_Online = true;
        public static bool Vydej_Items_Online
        {
            get { return _vydej_Items_Online; }
            set { _vydej_Items_Online = value; }
        }

		private static bool _vydej_FIFOFEFO_Online = true;
				public static bool Vydej_FIFOFEFO_Online
        {
			get { return _vydej_FIFOFEFO_Online; }
			set { _vydej_FIFOFEFO_Online = value; }
        }

		private static bool _vydej_ExpiraceCheck_Online = true;
		public static bool Vydej_ExpiraceCheck_Online
        {
			get { return _vydej_ExpiraceCheck_Online; }
			set { _vydej_ExpiraceCheck_Online = value; }
        }

		private static bool _vydej_TypSPrelokovanim = false;
		public static bool Vydej_TypSPrelokovanim
        {
			get { return _vydej_TypSPrelokovanim; }
			set { _vydej_TypSPrelokovanim = value; }
        }



        private static bool _vydej_HromadneSN = false;
        public static bool Vydej_HromadneSN
        {
            get { return _vydej_HromadneSN; }
            set { _vydej_HromadneSN = value; }
        }

        private static int _vydej_HromadneSN_N = 0;
        public static int Vydej_HromadneSN_N
        {
            get { return _vydej_HromadneSN_N; }
            set { _vydej_HromadneSN_N = value; }
        }

		private static bool _Vydej_MnozstviAutoJedna = false;
		public static bool Vydej_MnozstviAutoJedna
		{
			get { return _Vydej_MnozstviAutoJedna; }
			set { _Vydej_MnozstviAutoJedna = value; }
		}

        private static bool _vydejPrevzitIDSkladuZCiselnikuSkladu = false;
        public static bool VydejPrevzitIDSkladuZCiselnikuSkladu
        {
            get { return _vydejPrevzitIDSkladuZCiselnikuSkladu; }
            set { _vydejPrevzitIDSkladuZCiselnikuSkladu = value; }
        }

        private static bool _vydejFiltrCiselnikSkladuOnlyOne = false;
        public static bool VydejFiltrCiselnikSkladuOnlyOne
        {
            get { return _vydejFiltrCiselnikSkladuOnlyOne; }
            set { _vydejFiltrCiselnikSkladuOnlyOne = value; }
        }

        private static bool _vydejTypyPoctyPalet = false;
        public static bool VydejTypyPoctyPalet
        {
            get { return _vydejTypyPoctyPalet; }
            set { _vydejTypyPoctyPalet = value; }
        }

        private static bool _VydejVyberTiskarnyDokladu = false;
        public static bool VydejVyberTiskarnyDokladu
        {
            get { return _VydejVyberTiskarnyDokladu; }
            set { _VydejVyberTiskarnyDokladu = value; }
        }

        private static bool _VydejVyberPracovnika = false;
        public static bool VydejVyberPracovnika
        {
            get { return _VydejVyberPracovnika; }
            set { _VydejVyberPracovnika = value; }
        }

        private static bool _vydejPovolitZmenuOdberatele = false;
        public static bool VydejPovolitZmenuOdberatele
        {
            get { return _vydejPovolitZmenuOdberatele; }
            set { _vydejPovolitZmenuOdberatele = value; }
        }

        private static bool _vydejObjednavkaDetail = false;
        public static bool VydejObjednavkaDetail
        {
            get { return _vydejObjednavkaDetail; }
            set { _vydejObjednavkaDetail = value; }
        }

        private static bool _vydejPocetKusuNaSkladeOnline = false;
        public static bool VydejPocetKusuNaSkladeOnline
        {
            get { return _vydejPocetKusuNaSkladeOnline; }
            set { _vydejPocetKusuNaSkladeOnline = value; }
        }

        private static bool _vydejPocetKusuOnline = false;
        public static bool VydejPocetKusuOnline
        {
            get { return _vydejPocetKusuOnline; }
            set { _vydejPocetKusuOnline = value; }
        }

        private static bool _vydejGenerovatNenalezenouVydejku = false;
        public static bool VydejGenerovatNenalezenouVydejku
        {
            get { return _vydejGenerovatNenalezenouVydejku; }
            set { _vydejGenerovatNenalezenouVydejku = value; }
        }

        private static bool _vydejPolozkaDetailOnline = false;
        public static bool VydejPolozkaDetailOnline
        {
            get { return _vydejPolozkaDetailOnline; }
            set { _vydejPolozkaDetailOnline = value; }
        }

        private static bool _vydejGenerovatDataPrikazuOnline = false;
        public static bool VydejGenerovatDataPrikazuOnline
        {
            get { return _vydejGenerovatDataPrikazuOnline; }
            set { _vydejGenerovatDataPrikazuOnline = value; }
        }

        private static string _vydejAktualniCisloPalety = "";
        public static string VydejAktualniCisloPalety
        {
            get { return _vydejAktualniCisloPalety; }
            set { _vydejAktualniCisloPalety = value; }
        }

        private static bool _vydejTypOznaceniPalety = true;
        public static bool VydejTypOznaceniPalety
        {
            get { return _vydejTypOznaceniPalety; }
            set { _vydejTypOznaceniPalety = value; }
        }
        private static bool _vydejPovolitPreplneniPolozky = false;
        public static bool vydejPovolitPreplneniPolozky
        {
            get { return _vydejPovolitPreplneniPolozky; }
            set { _vydejPovolitPreplneniPolozky = value; }
        }

        private static bool _vydejZadaniLocncodePredSN = true;
        public static bool vydejZadaniLocncodePredSN
        {
            get { return _vydejZadaniLocncodePredSN; }
            set { _vydejZadaniLocncodePredSN = value; }
        }

        private static bool _VydejLocationPouzitCiselnik = false;
        public static bool VydejLocationPouzitCiselnik
        {
            get { return _VydejLocationPouzitCiselnik; }
            set { _VydejLocationPouzitCiselnik = value; }
        }
        private static bool _VydejLocationFiltrovatData = false;
        public static bool VydejLocationFiltrovatData
        {
            get { return _VydejLocationFiltrovatData; }
            set { _VydejLocationFiltrovatData = value; }
        }

        private static bool _VydejLocationAllowAutocommit = false;
        public static bool VydejLocationAllowAutocommit
        {
            get { return _VydejLocationAllowAutocommit; }
            set { _VydejLocationAllowAutocommit = value; }
        }

        private static bool _VydejPokracovatNaJinemTerminalu = false;
        public static bool VydejPokracovatNaJinemTerminalu
        {
            get { return _VydejPokracovatNaJinemTerminalu; }
            set { _VydejPokracovatNaJinemTerminalu = value; }
        }

        private static bool _VydejHledaniCkAutoVyberPrvniNeuplne = true;
        public static bool VydejHledaniCkAutoVyberPrvniNeuplne
        {
            get { return _VydejHledaniCkAutoVyberPrvniNeuplne; }
            set { _VydejHledaniCkAutoVyberPrvniNeuplne = value; }
        }

        // Vydej REZ_2
        private static string _VydejRozsireni1Rezerva2Nazev = "REZ_2";
        public static string VydejRozsireni1Rezerva2Nazev
        {
            get { return _VydejRozsireni1Rezerva2Nazev; }
            set { _VydejRozsireni1Rezerva2Nazev = value; }
        }
        private static string _VydejRozsireni1Rezerva2Default = string.Empty;
        public static string VydejRozsireni1Rezerva2Default
        {
            get { return _VydejRozsireni1Rezerva2Default; }
            set { _VydejRozsireni1Rezerva2Default = value; }
        }
        private static bool _VydejRozsireni1Rezerva2Zadavat = false;
        public static bool VydejRozsireni1Rezerva2Zadavat
        {
            get { return _VydejRozsireni1Rezerva2Zadavat; }
            set { _VydejRozsireni1Rezerva2Zadavat = value; }
        }

        private static bool _VydejEtiketaTiskPoVlozeniDotaz = false;
        public static bool VydejEtiketaTiskPoVlozeniDotaz
        {
            get { return _VydejEtiketaTiskPoVlozeniDotaz; }
            set { _VydejEtiketaTiskPoVlozeniDotaz = value; }
        }

        private static bool _VydejPolozkyVyberJenScannerem = false;
        public static bool VydejPolozkyVyberJenScannerem
        {
            get { return _VydejPolozkyVyberJenScannerem; }
            set { _VydejPolozkyVyberJenScannerem = value; }
        }

        private static bool _VydejDavkyVyberJenScannerem = false;
        public static bool VydejDavkyVyberJenScannerem
        {
            get { return _VydejDavkyVyberJenScannerem; }
            set { _VydejDavkyVyberJenScannerem = value; }
        }

        private static bool _VydejPovolitKontrolaSNPredloha = true;
        public static bool VydejPovolitKontrolaSNPredloha
        {
            get { return _VydejPovolitKontrolaSNPredloha; }
            set { _VydejPovolitKontrolaSNPredloha = value; }
        }

        private static bool _VydejPovolitOcipovani = false;
        public static bool VydejPovolitOcipovani
        {
            get { return _VydejPovolitOcipovani; }
            set { _VydejPovolitOcipovani = value; }
        }

		private static bool _vydej_HromadneBaliky = false;
		public static bool Vydej_HromadneBaliky
		{
			get { return _vydej_HromadneBaliky; }
			set { _vydej_HromadneBaliky = value; }
		}

		private static bool _vydej_PtatSeNaPamatovani = false;
		public static bool Vydej_PtatSeNaPamatovani
		{
			get { return _vydej_PtatSeNaPamatovani; }
			set { _vydej_PtatSeNaPamatovani = value; }
		}

        #region TaD Dialogy Vydej

        private static bool _VydejDialogUspesnehoOdeslaniDavky = false;
                public static bool VydejDialogUspesnehoOdeslaniDavky
        {
            get { return _VydejDialogUspesnehoOdeslaniDavky; }
            set { _VydejDialogUspesnehoOdeslaniDavky = value; }
        }

        private static bool _VydejDialogOpusteniModulu = true;
        public static bool VydejDialogOpusteniModulu
        {
            get { return _VydejDialogOpusteniModulu; }
            set { _VydejDialogOpusteniModulu = value; }
        }

        private static bool _VydejDialogOpusteniVydejky = true;
                public static bool VydejDialogOpusteniVydejky
        {
            get { return _VydejDialogOpusteniVydejky; }
            set { _VydejDialogOpusteniVydejky = value; }
        }

        private static decimal _vydejTimeDialogInterval = 3;
        public static decimal VydejTimeDialogInterval
        {
            get { return _vydejTimeDialogInterval; }
            set { _vydejTimeDialogInterval = value; }
        }

        private static bool _vydejTimeDialogNasnimana = true;
        public static bool VydejTimeDialogNasnimana
        {
            get { return _vydejTimeDialogNasnimana; }
            set { _vydejTimeDialogNasnimana = value; }
        }

        private static bool _vydejDialogDavkaNenalezenaVygenerovat = true;
        public static bool VydejDialogDavkaNenalezenaVygenerovat
        {
            get { return _vydejDialogDavkaNenalezenaVygenerovat; }
            set { _vydejDialogDavkaNenalezenaVygenerovat = value; }
        }

        private static bool _vydejDialogNaDiskuNejsouDavkyStahnout = true;
        public static bool VydejDialogNaDiskuNejsouDavkyStahnout
        {
            get { return _vydejDialogNaDiskuNejsouDavkyStahnout; }
            set { _vydejDialogNaDiskuNejsouDavkyStahnout = value; }
        }

        #endregion 



                #region Vydej REZ1 a REZ2

                private static bool _VydejRez2Cislo = true;
        public static bool VydejRez2Cislo
        {
            get { return _VydejRez2Cislo; }
            set { _VydejRez2Cislo = value; }
        }

        private static bool _VydejRez1Cislo = true;
        public static bool VydejRez1Cislo
        {
            get { return _VydejRez1Cislo; }
            set { _VydejRez1Cislo = value; }
        }

        private static bool _VydejRez2Povinne = true;
        public static bool VydejRez2Povinne
        {
            get { return _VydejRez2Povinne; }
            set { _VydejRez2Povinne = value; }
        }

        private static bool _VydejRez1Povinne = true;
        public static bool VydejRez1Povinne
        {
            get { return _VydejRez1Povinne; }
            set { _VydejRez1Povinne = value; }
        }

        private static bool _VydejRez2Pamatovat = true;
        public static bool VydejRez2Pamatovat
        {
            get { return _VydejRez2Pamatovat; }
            set { _VydejRez2Pamatovat = value; }
        }

        private static bool _VydejRez1Pamatovat = true;
        public static bool VydejRez1Pamatovat
        {
            get { return _VydejRez1Pamatovat; }
            set { _VydejRez1Pamatovat = value; }
        }

        #endregion

        private static bool _inventura1ZadaniLocncodePredSN = true;
        public static bool inventura1ZadaniLocncodePredSN
        {
            get { return _inventura1ZadaniLocncodePredSN; }
            set { _inventura1ZadaniLocncodePredSN = value; }
        }

        private static bool _inventura1ZadaniLocncodeJednou = false;
        public static bool inventura1ZadaniLocncodeJednou
        {
            get { return _inventura1ZadaniLocncodeJednou; }
            set { _inventura1ZadaniLocncodeJednou = value; }
        }

        private static bool _inventura1ZadaniLocncodePamatovatPosledni = true;
        public static bool inventura1ZadaniLocncodePamatovatPosledni
        {
            get { return _inventura1ZadaniLocncodePamatovatPosledni; }
            set { _inventura1ZadaniLocncodePamatovatPosledni = value; }
        }


        private static bool _prijemTimeDialog = true;
        public static bool PrijemTimeDialog
        {
            get { return _prijemTimeDialog; }
            set { _prijemTimeDialog = value; }
        }

        private static decimal _prijemTimeDialogInterval = 3;
        public static decimal PrijemTimeDialogInterval
        {
            get { return _prijemTimeDialogInterval; }
            set { _prijemTimeDialogInterval = value; }
        }

        private static bool _povolitAktualizacePristupu = true;
        public static bool PovolitAktualizacePristupu
        {
            get { return _povolitAktualizacePristupu; }
            set { _povolitAktualizacePristupu = value; }
        }

        private static bool _povolitPrintServer = true;
        public static bool PovolitPrintServer
        {
            get { return _povolitPrintServer; }
            set { _povolitPrintServer = value; }
        }


        private static int _vydejMaxPocetDavek = 0;
        /// <summary>
        /// Maximalni pocet davek, ktere lze do terminalu stahnout. 0=neomezene
        /// </summary>
        public static int VydejMaxPocetDavek
        {
            get { return _vydejMaxPocetDavek; }
            set { _vydejMaxPocetDavek = value; }
        }

        private static Keys _DataGridScrollUp = Keys.D2;
        /// <summary>
        /// Klavesa na kterou se scrolluje v DG nahoru
        /// </summary>
        public static Keys DataGridScrollUp
        {
            get { return _DataGridScrollUp; }
            set { _DataGridScrollUp = value; }
        }

        private static Keys _DataGridScrollDown = Keys.D2;
        /// <summary>
        /// Klavesa na kterou se scrolluje v DG dolu
        /// </summary>
        public static Keys DataGridScrollDown
        {
            get { return _DataGridScrollDown; }
            set { _DataGridScrollDown = value; }
        }

        private static int _vydejPocetDavekZobraz = 3;
        /// <summary>
        /// Maximalni pocet davek, ktere lze do terminalu stahnout. 0=neomezene
        /// </summary>
        public static int VydejPocetDavekZobraz
        {
            get { return _vydejPocetDavekZobraz; }
            set { _vydejPocetDavekZobraz = value; }
        }


        private static int _vydejSequence = 1;
        /// <summary>
        /// Sequence SSCC kodu 
        /// </summary>
        public static int VydejSequence
        {
            get { return _vydejSequence; }
            set { _vydejSequence = value; }
        }

        //private static int _vydejDelkaKoduPalety = 20;
        ///// <summary>
        ///// Delka SSCC kodu palety (default: 20 znaku)
        ///// </summary>
        //public static int VydejDelkaKoduPalety
        //{
        //    get { return _vydejDelkaKoduPalety; }
        //    set { _vydejDelkaKoduPalety = value; }
        //}

        //private static string _vydejPrefix = "123456789";
        ///// <summary>
        ///// Prefix SSCC kodu vcetne AI identifikatoru (napr: 003859400500)
        ///// </summary>
        //public static string VydejPrefix
        //{
        //    get { return _vydejPrefix; }
        //    set { _vydejPrefix = value; }
        //}

        private static bool _docasnePovolitPreplneni = false;
        /// <summary>
        /// Docasne povoli preplneni polozky
        /// </summary>
        public static bool DocasnePovolitPreplneni
        {
            get { return _docasnePovolitPreplneni; }
            set { _docasnePovolitPreplneni = value; }
        }

        private static bool _vydejRezimCelePalety = false;
        /// <summary>
        /// dopsat...
        /// </summary>
        public static bool VydejRezimCelePalety
        {
            get { return _vydejRezimCelePalety; }
            set { _vydejRezimCelePalety = value; }
        }

        private static bool _vydejSubezimSberDat = true;
        /// <summary>
        /// dopsat...
        /// </summary>
        public static bool VydejSubezimSberDat
        {
            get { return _vydejSubezimSberDat; }
            set { _vydejSubezimSberDat = value; }
        }

        private static bool _vydejDoplnitPrefix = false;
        /// <summary>
        /// Docasne povoli preplnenii polozky
        /// </summary>
        public static bool VydejDoplnitPrefix
        {
            get { return _vydejDoplnitPrefix; }
            set { _vydejDoplnitPrefix = value; }
        }

        private static bool _vydejStahnoutNejvyssiPrioritu = true;
        /// <summary>
        /// Nepovoli stahnout vydejku s nizsi prioritou, pokud existuje nejaka s vyssi
        /// </summary>
        public static bool VydejStahnoutNejvyssiPrioritu
        {
            get { return _vydejStahnoutNejvyssiPrioritu; }
            set { _vydejStahnoutNejvyssiPrioritu = value; }
        }


        private static bool _vydejHromadneVyplneni = true;
        public static bool VydejHromadneVyplneni
        {
            get { return _vydejHromadneVyplneni; }
            set { _vydejHromadneVyplneni = value; }
        }

        private static bool _vydejSlucovaniDavek = true;
        public static bool VydejSlucovaniDavek
        {
            get { return _vydejSlucovaniDavek; }
            set { _vydejSlucovaniDavek = value; }
        }

        //Pridani nastaveni stahovani ciselniku zbozi.
        private static bool _vydejZboziPouzitCiselnik = false;
        public static bool VydejZboziPouzitCiselnik
        {
            get { return _vydejZboziPouzitCiselnik; }
            set { _vydejZboziPouzitCiselnik = value; }
        }

        private static bool _vydejPovolitRazeniVydejek = false;
        public static bool VydejPovolitRazeniVydejek
        {
            get { return _vydejPovolitRazeniVydejek; }
            set { _vydejPovolitRazeniVydejek = value; }
        }

        private static bool _VydejZboziVyberJenScannerem = false;
        public static bool VydejZboziVyberJenScannerem
        {
            get { return _VydejZboziVyberJenScannerem; }
            set { _VydejZboziVyberJenScannerem = value; }
        }

        private static string _VydejZboziVyhledaniDleSloupce = "CZ_CARKOD,VNDITNUM";
        public static string VydejZboziVyhledaniDleSloupce
        {
            get { return _VydejZboziVyhledaniDleSloupce; }
            set { _VydejZboziVyhledaniDleSloupce = value; }
        }

		private static Fask.MST_W.Vydej_3.Varianta_TiskSoupis _VydejTiskVariantaSoupis = Fask.MST_W.Vydej_3.Varianta_TiskSoupis.I_Tec;
		public static Fask.MST_W.Vydej_3.Varianta_TiskSoupis VydejTiskVariantaSoupis
		{
			get { return _VydejTiskVariantaSoupis; }
			set { _VydejTiskVariantaSoupis = value; }
		}

		#region Dialogy tisk

		private static Fask.MST_W.Vydej_3.Potvrzeni_AnoNe _Vydej_PokracovatJinyTerm_AnoNe = Fask.MST_W.Vydej_3.Potvrzeni_AnoNe.Ano;
		public static Fask.MST_W.Vydej_3.Potvrzeni_AnoNe Vydej_PokracovatJinyTerm_AnoNe
		{
			get { return _Vydej_PokracovatJinyTerm_AnoNe; }
			set { _Vydej_PokracovatJinyTerm_AnoNe = value; }
		}

		private static bool _VydejDialogTiskSoupis = true;
		public static bool VydejDialogTiskSoupis
		{
			get { return _VydejDialogTiskSoupis; }
			set { _VydejDialogTiskSoupis = value; }
		}

		private static bool _VydejDialogPokracovatNaJinemTerm = true;
		public static bool VydejDialogPokracovatNaJinemTerm
		{
			get { return _VydejDialogPokracovatNaJinemTerm; }
			set { _VydejDialogPokracovatNaJinemTerm = value; }
		}

		private static bool _VydejTiskSoupisMN_Auto1 = true;
		public static bool VydejTiskSoupisMN_Auto1
		{
			get { return _VydejTiskSoupisMN_Auto1; }
			set { _VydejTiskSoupisMN_Auto1 = value; }
		}
		

		#endregion

		#region TaD F10 Vydej možnosti

		private static bool _F10_ZobrazitAlternativyLokaci = false;
		public static bool F10_ZobrazitAlternativyLokaci
		{
			get { return _F10_ZobrazitAlternativyLokaci; }
			set { _F10_ZobrazitAlternativyLokaci = value; }
		}

		private static bool _F10_TiskPalListku = true;
		public static bool F10_TiskPalListku
		{
			get { return _F10_TiskPalListku; }
			set { _F10_TiskPalListku = value; }
		}


		#endregion

        // expedice
        private static bool _ExpediceShowInMST = false;
        public static bool ExpediceShowInMST
        {
            get { return _ExpediceShowInMST; }
            set { _ExpediceShowInMST = value; }
        }

		//Automatika
		private static bool _Automatika_FirtsRun = false;
		public static bool Automatika_FirtsRun
		{
			get { return _Automatika_FirtsRun; }
			set { _Automatika_FirtsRun = value; }
		}

        public static string LoadElement(XmlDocument xmldoc, string uzelname, string defaultvalue)
        {
            string uzelvalue = defaultvalue;
            XmlElement confignode = xmldoc.SelectSingleNode(uzelname) as XmlElement;
            if (confignode != null)
            {
                try { uzelvalue = confignode.InnerText; }
                catch { }
            }

            return uzelvalue;
        }

        public static bool Load(string filename)
        {
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(filename);
                XmlElement confignode = null;

                //Terminal
                _DataGridScrollUp = (Keys)Enum.Parse(typeof(Keys), LoadElement(xmldoc, "/Config/Terminal/ScrollDataGridUp", _DataGridScrollUp.ToString()), true);
                _DataGridScrollDown = (Keys)Enum.Parse(typeof(Keys), LoadElement(xmldoc, "/Config/Terminal/ScrollDataGridDown", _DataGridScrollDown.ToString()), true);
                _sqlCe = bool.Parse(LoadElement(xmldoc, "/Config/Terminal/SqlCe", _sqlCe.ToString()));
                _terminalID = byte.Parse(LoadElement(xmldoc, "/Config/Terminal/ID", _terminalID.ToString()));

				_api_konstant = LoadElement(xmldoc, "/Config/Terminal/api_konstant", _api_konstant);
				_serverAddress = LoadElement(xmldoc, "/Config/Terminal/Address", _serverAddress);
				_isHTTPS = bool.Parse(LoadElement(xmldoc, "/Config/Terminal/isHTTPS", _isHTTPS.ToString()));
				_autorizace_API = LoadElement(xmldoc, "/Config/Terminal/autorizace_API", _autorizace_API);
                _serviceTimeOut = int.Parse(LoadElement(xmldoc, "/Config/Terminal/ServiceTimeOut", _serviceTimeOut.ToString()));
                
				
				_serverAccess = (ServerAccessType)Enum.Parse(typeof(ServerAccessType), LoadElement(xmldoc, "/Config/Terminal/ServerAccess", _serverAccess.ToString()), true);
                _serverAccessUsername = LoadElement(xmldoc, "/Config/Terminal/ServerAccessUsername", _serverAccessUsername);
                _povolitAktualizacePristupu = bool.Parse(LoadElement(xmldoc, "/Config/Terminal/PovolitAktualizacePristupu", _povolitAktualizacePristupu.ToString()));

                _lokalizacePovolit = bool.Parse(LoadElement(xmldoc, "/Config/Terminal/LokalizacePovolit", _lokalizacePovolit.ToString()));
                _LokalizaceVlastniPovolit = bool.Parse(LoadElement(xmldoc, "/Config/Terminal/LokalizaceVlastniPovolit", _LokalizaceVlastniPovolit.ToString()));

                //try
                //{
                //    _lokalizaceZvolena = (Localization.LocalizationSupport.LocalType)Enum.Parse(typeof(Localization.LocalizationSupport.LocalType), LoadElement(xmldoc, "/Config/Terminal/LokalizaceZvolena", _lokalizaceZvolena.ToString()), true);
                //}
                //catch
                //{
                //    _lokalizaceZvolena = Fask.MST_W.Localization.LocalizationSupport.LocalType.cs;
                //}
                try
                {
                    _lokalizaceZvolena = (Fask.Localization.LocalizationSupport.LocalType)Enum.Parse(typeof(Fask.Localization.LocalizationSupport.LocalType), LoadElement(xmldoc, "/Config/Terminal/LokalizaceZvolena", _lokalizaceZvolena.ToString()), true);
                }
                catch
                {
                    _lokalizaceZvolena = Fask.Localization.LocalizationSupport.LocalType.cs;
                }
                
                // Print server
                /*
                _PrintServerTemplateNamePrijemPredloha = LoadElement(xmldoc, "/Config/Terminal/PrintServerTemplateNamePrijemPredloha", _PrintServerTemplateNamePrijemPredloha);
                _PrintServerTemplateNamePrijemNasnimane = LoadElement(xmldoc, "/Config/Terminal/PrintServerTemplateNamePrijemNasnimane", _PrintServerTemplateNamePrijemNasnimane);
                _PrintServerTemplateNameVydejPredloha = LoadElement(xmldoc, "/Config/Terminal/PrintServerTemplateNameVydejPredloha", _PrintServerTemplateNameVydejPredloha);
                _PrintServerTemplateNameVydejNasnimane = LoadElement(xmldoc, "/Config/Terminal/PrintServerTemplateNameVydejNasnimane", _PrintServerTemplateNameVydejNasnimane);
                _PrintServerTemplateNameProdejPredloha = LoadElement(xmldoc, "/Config/Terminal/PrintServerTemplateNameProdejPredloha", _PrintServerTemplateNameProdejPredloha);
                _PrintServerTemplateNameProdejNasnimane = LoadElement(xmldoc, "/Config/Terminal/PrintServerTemplateNameProdejNasnimane", _PrintServerTemplateNameProdejNasnimane);
                _PrintServerTemplateNameInventuraPredloha = LoadElement(xmldoc, "/Config/Terminal/PrintServerTemplateNameInventuraPredloha", _PrintServerTemplateNameInventuraPredloha);
                _PrintServerTemplateNameInventuraNasnimane = LoadElement(xmldoc, "/Config/Terminal/PrintServerTemplateNameInventuraNasnimane", _PrintServerTemplateNameInventuraNasnimane);
                _PrintServerTemplateNameVydejPalListek = LoadElement(xmldoc, "/Config/Terminal/PrintServerTemplateNameVydejPalListek", _PrintServerTemplateNameVydejPalListek);
                */
                _povolitPrintServer = bool.Parse(LoadElement(xmldoc, "/Config/Terminal/PrintServer", _povolitPrintServer.ToString()));

                // TODO : sifrovani hesel ...
                _serverAccessPassword = LoadElement(xmldoc, "/Config/Terminal/ServerAccessPassword", _serverAccessPassword);
                _serverAccessDomain = LoadElement(xmldoc, "/Config/Terminal/ServerAccessDomain", _serverAccessDomain);
                _serverAccessPreauthenticate = bool.Parse(LoadElement(xmldoc, "/Config/Terminal/ServerAccessPreauthenticate", _serverAccessPreauthenticate.ToString()));
                _serverAccessAllowRedirection = bool.Parse(LoadElement(xmldoc, "/Config/Terminal/ServerAccessAllowRedirection", _serverAccessAllowRedirection.ToString()));
                _serverAccessAllowDecompression = bool.Parse(LoadElement(xmldoc, "/Config/Terminal/ServerAccessAllowDecompression", _serverAccessAllowDecompression.ToString()));
                //_serverAccessCertificateTrust = (ServerAccessCertificatesTrustType)Enum.Parse(typeof(ServerAccessCertificatesTrustType), LoadElement(xmldoc, "/Config/Terminal/ServerAccessCertificatesTrust", _serverAccessCertificateTrust.ToString()), true);
				ServerAccessCertificateTrust = (ServerAccessCertificatesTrustType)Enum.Parse(typeof(ServerAccessCertificatesTrustType), LoadElement(xmldoc, "/Config/Terminal/ServerAccessCertificatesTrust", _serverAccessCertificateTrust.ToString()), true);
                //_ScannerType = (Scanner.ScannerTypes)Enum.Parse(typeof(Scanner.ScannerTypes), LoadElement(xmldoc, "/Config/Terminal/ScannerType", _ScannerType.ToString()), true);
                _Storage = LoadElement(xmldoc, "/Config/Terminal/Storage", _Storage);
                _MemorySize = LoadElement(xmldoc, "/Config/Terminal/MemorySize", _MemorySize);
                _MemoryChecked = bool.Parse(LoadElement(xmldoc, "/Config/Terminal/MemoryChecked", _MemoryChecked.ToString()));
                LogUploadInterval = int.Parse(LoadElement(xmldoc, "/Config/Terminal/LogUploadInterval", _LogUploadInterval.ToString()));
                Fask.Logging.Log.Enable = bool.Parse(LoadElement(xmldoc, "/Config/Terminal/Log", Fask.Logging.Log.Enable.ToString()));
                Fask.Logging.Log.EnableAdvanced = bool.Parse(LoadElement(xmldoc, "/Config/Terminal/LogAdvanced", Fask.Logging.Log.EnableAdvanced.ToString()));
                Fask.Logging.Trace2.Enable = bool.Parse(LoadElement(xmldoc, "/Config/Terminal/Trace", Fask.Logging.Trace2.Enable.ToString()));
                Fask.Logging.Trace2.Separator = LoadElement(xmldoc, "/Config/Terminal/Trace_Separator", Fask.Logging.Trace2.Separator);
                _ShowPanelButtons = bool.Parse(LoadElement(xmldoc, "/Config/Terminal/ShowPanelButtons", _ShowPanelButtons.ToString()));

                //===========================================
                //RFID terminal
                _RFIDScannerType = (Scanner.ScannerRFIDTypes)Enum.Parse(typeof(Scanner.ScannerRFIDTypes), LoadElement(xmldoc, "/Config/Terminal/RFIDScannerType", _RFIDScannerType.ToString()), true);
                _RFIDPovolitUHF = bool.Parse(LoadElement(xmldoc, "/Config/Terminal/RFIDAllowUHF", _RFIDPovolitUHF.ToString()));
                _RFIDukladatNacitatText = bool.Parse(LoadElement(xmldoc, "/Config/Terminal/RFIDLoadAsciiTextData", _RFIDukladatNacitatText.ToString()));
                _RFIDPowerLevel = int.Parse(LoadElement(xmldoc, "/Config/Terminal/RFIDPowerLevel", _RFIDPowerLevel.ToString()));

                _RFID_memoryEPC = bool.Parse(LoadElement(xmldoc, "/Config/Terminal/RFID_memoryEPC", _RFID_memoryEPC.ToString()));
                _RFID_memoryUSER = bool.Parse(LoadElement(xmldoc, "/Config/Terminal/RFID_memoryUSER", _RFID_memoryUSER.ToString()));
                _RFID_memoryRESERVED = bool.Parse(LoadElement(xmldoc, "/Config/Terminal/RFID_memoryRESERVED", _RFID_memoryRESERVED.ToString()));
                _RFID_memoryTID = bool.Parse(LoadElement(xmldoc, "/Config/Terminal/RFID_memoryTID", _RFID_memoryTID.ToString()));


                //===========================================
//scannerready sound

                _OnScannerSound_Inventura1_sqlc = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Inventura1/OnScannerSound_Inventura1_sqlc", _OnScannerSound_Inventura1_sqlc.ToString()));
                _OnScannerSound_Inventura2 = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Inventura2/OnScannerSound_Inventura2", _OnScannerSound_Inventura2.ToString()));
                _OnScannerSound_Expedice = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Expedice/OnScannerSound_Expedice", _OnScannerSound_Expedice.ToString()));
                _OnScannerSound_Online = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Prijem/OnScannerSound_Online", _OnScannerSound_Online.ToString()));
                _OnScannerSound_Prijem_4 = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Prijem/OnScannerSound_Prijem_4", _OnScannerSound_Prijem_4.ToString()));
                _OnScannerSound_Prodej_3 = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Prodej/OnScannerSound_Prodej_3", _OnScannerSound_Prodej_3.ToString()));
                _OnScannerSound_Vydej_3 = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/OnScannerSound_Vydej_3", _OnScannerSound_Vydej_3.ToString()));
                _OnScannerSound_ServisModul = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Servis/OnScannerSound_ServisModul", _OnScannerSound_ServisModul.ToString()));

                //===========================================

                //Servis modul
                _ServisEnable = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Servis/Allow", _ServisEnable.ToString()));
                _ServisShowInMST = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Servis/ShowInMST", _ServisShowInMST.ToString()));
                _ServisTimeoutOnline = int.Parse(LoadElement(xmldoc, "/Config/Modules/Servis/TimeoutOnline", _ServisTimeoutOnline.ToString()));
                _ServisTimeoutSynchronize = int.Parse(LoadElement(xmldoc, "/Config/Modules/Servis/TimeoutSynchronize", _ServisTimeoutSynchronize.ToString()));
                _ServisAutoUpdateInterval = int.Parse(LoadElement(xmldoc, "/Config/Modules/Servis/AutoUpdateInterval", _ServisAutoUpdateInterval.ToString()));                
                _ServisAllowCamera = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Servis/AllowCamera", _ServisAllowCamera.ToString()));
                _ServisDavkoveZpracovani = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Servis/DavkoveZpracovani", _ServisDavkoveZpracovani.ToString()));
                _ServisVyberOdberatelPovolit = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Servis/VyberOdberatelPovolit", _ServisVyberOdberatelPovolit.ToString()));
                _ServisVyberOkruhPovolit= bool.Parse(LoadElement(xmldoc, "/Config/Modules/Servis/VyberOkruhPovolit", _ServisVyberOkruhPovolit.ToString()));
                _ServisUkonceniStavuNavrat = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Servis/UkonceniStavuNavrat", _ServisUkonceniStavuNavrat.ToString()));
                _ServisPozadovatPotvrzeniZmenyStavu = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Servis/ServisPozadovatPotvrzeniZmenyStavu", _ServisPozadovatPotvrzeniZmenyStavu.ToString()));
                _ServisDavkaOtevritIhnedPoStazeni = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Servis/DavkaOtevritIhnedPoStazeni", _ServisDavkaOtevritIhnedPoStazeni.ToString()));
                _ServisFiltrZobrazeniPovolit = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Servis/FiltrZobrazeniPovolit", _ServisFiltrZobrazeniPovolit.ToString()));
                _ServisAutomatickaZmenaStavuPovolit = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Servis/AutomatickaZmenaStavuPovolit", _ServisAutomatickaZmenaStavuPovolit.ToString()));
                _ServisPozadovatPotvrzeniZmenyCinnosti = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Servis/ServisPozadovatPotvrzeniZmenyCinnosti", _ServisPozadovatPotvrzeniZmenyCinnosti.ToString()));
                _ServisSynchronizaceZdrojePoVyberuPovolit = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Servis/SynchronizaceZdrojePoVyberuPovolit", _ServisSynchronizaceZdrojePoVyberuPovolit.ToString()));
                _ServisPrehratZvukPoVyberuMoznosti = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Servis/ServisPrehratZvukPoVyberuMoznosti", _ServisPrehratZvukPoVyberuMoznosti.ToString()));
                _ServisStatusBarZobrazitNazevStavu = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Servis/ServisStatusBarZobrazitNazevStavu", _ServisStatusBarZobrazitNazevStavu.ToString()));
                _ServisStatusBarZobrazitIdZdroje = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Servis/ServisStatusBarZobrazitIdZdroje", _ServisStatusBarZobrazitIdZdroje.ToString()));
                _ServisStatusBarZobrazitTypZdroje = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Servis/ServisStatusBarZobrazitTypZdroje", _ServisStatusBarZobrazitTypZdroje.ToString()));
                _ServisStatusBarZobrazitOznaceniZdroje = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Servis/ServisStatusBarZobrazitOznaceniZdroje", _ServisStatusBarZobrazitOznaceniZdroje.ToString()));
                _ServisZobrazitInformaciOUkonceniStavu = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Servis/ServisZobrazitInformaciOUkonceniStavu", _ServisZobrazitInformaciOUkonceniStavu.ToString()));
                _ServisOdeslaniDatNaPozadiPovolit = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Servis/PovolitOdeslaniDatNaPozadi", _ServisOdeslaniDatNaPozadiPovolit.ToString()));
                _ServisOdeslaniDatPoUkonceniZadavaniPovolit = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Servis/PovolitOdeslaniDatPoUkonceniZadavani", _ServisOdeslaniDatPoUkonceniZadavaniPovolit.ToString()));
                #region TaD Dialogy Servis
                _Servis_DialogOpusteniModulu = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Servis/Servis_DialogOpusteniModulu", _Servis_DialogOpusteniModulu.ToString()));
                #endregion
                //Udalosti (Events)
                _EventsEnable = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Events/Allow", _EventsEnable.ToString()));
                _EventsShowInMST = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Events/ShowInMST", _EventsShowInMST.ToString()));
                _EventsOnline = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Events/Online", _EventsOnline.ToString()));
                _EventsOnlineTimeout = int.Parse(LoadElement(xmldoc, "/Config/Modules/Events/OnlineTimeout", _EventsOnlineTimeout.ToString()));
                _EventsOnlineConfirm = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Events/OnlineConfirm", _EventsOnlineConfirm.ToString()));
                _EventsSynchronizationInterval = int.Parse(LoadElement(xmldoc, "/Config/Modules/Events/SynchronizationInterval", _EventsSynchronizationInterval.ToString()));

                //Ukoly
                _TasksEnable = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Tasks/Allow", _TasksEnable.ToString()));
                _TasksShowInMST = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Tasks/ShowInMST", _TasksShowInMST.ToString()));
                _TasksTimeout = int.Parse(LoadElement(xmldoc, "/Config/Modules/Tasks/Timeout", _TasksTimeout.ToString()));
                _TasksSynchronizationInterval = int.Parse(LoadElement(xmldoc, "/Config/Modules/Tasks/SynchronizationInterval", _TasksSynchronizationInterval.ToString()));
                _TasksNotifyDialogShowSeconds = int.Parse(LoadElement(xmldoc, "/Config/Modules/Tasks/NotifyDialogShowSeconds", TasksNotifyDialogShowSeconds.ToString()));

                // Expedice
                _expedice = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Expedice/Allow", _expedice.ToString()));
                _expediceName = LoadElement(xmldoc, "/Config/Modules/Expedice/NameModul", _expediceName);
                _ExpediceShowInMST = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Expedice/ShowInMST", _ExpediceShowInMST.ToString()));

                //Inventura1
                _inventura1 = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Inventura1/Allow", _inventura1.ToString()));
                _inventura1Name = LoadElement(xmldoc, "/Config/Modules/Inventura1/NameModul", _inventura1Name);
                _Inventura1ShowInMST = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Inventura1/ShowInMST", _Inventura1ShowInMST.ToString()));
                _Inventura1PolozkaNasnimatPouzeJednou = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Inventura1/PolozkaNasnimatPouzeJednou", _Inventura1PolozkaNasnimatPouzeJednou.ToString()));
                _Inventura1OnlineKontrola = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Inventura1/OnlineKontrola", _Inventura1OnlineKontrola.ToString()));
                _inventura1ZadaniLocncodePredSN = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Inventura1/ZadaniLocncodePredSN", _inventura1ZadaniLocncodePredSN.ToString()));
                _inventura1ZadaniLocncodeJednou = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Inventura1/ZadaniLocncodeJednou", _inventura1ZadaniLocncodeJednou.ToString()));
                _inventura1ZadaniLocncodePamatovatPosledni = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Inventura1/ZadaniLocncodePamatovatPosledni", _inventura1ZadaniLocncodePamatovatPosledni.ToString()));
                _Inventura1OnlineTimeout = int.Parse(LoadElement(xmldoc, "/Config/Modules/Inventura1/OnlineTimeout", _Inventura1OnlineTimeout.ToString()));
                _Inventura1ChunkTimeout = int.Parse(LoadElement(xmldoc, "/Config/Modules/Inventura1/ChunkTimeout", _Inventura1ChunkTimeout.ToString()));
                _Inventura1PouzitCiselnikSkladu = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Inventura1/PouzitCiselnikSkladu", _Inventura1PouzitCiselnikSkladu.ToString()));
                _Inventura1PolozkyVyberJenScannerem = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Inventura1/PolozkyVyberJenScannerem", _Inventura1PolozkyVyberJenScannerem.ToString()));
				_Inventura1ParsovaniCarovehoKoduPovolit = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Inventura1/ParsovaniCarovehoKoduPovolit", _Inventura1ParsovaniCarovehoKoduPovolit.ToString()));
				_Inventura1KontrolaSarzeUParsovanehoCarovehoKoduPovolit = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Inventura1/Inventura1KontrolaSarzeUParsovanehoCarovehoKoduPovolit", _Inventura1KontrolaSarzeUParsovanehoCarovehoKoduPovolit.ToString()));
                _Inventura1REZ1Nazev = LoadElement(xmldoc, "/Config/Modules/Inventura1/REZ1Nazev", _Inventura1REZ1Nazev);
                _Inventura1REZ1IsNumber = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Inventura1/REZ1IsNumber", _Inventura1REZ1IsNumber.ToString()));
                _Inventura1REZ1Mandatory = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Inventura1/REZ1Mandatory", _Inventura1REZ1Mandatory.ToString()));
                _Inventura1REZ2Nazev = LoadElement(xmldoc, "/Config/Modules/Inventura1/REZ2Nazev", _Inventura1REZ2Nazev);
                _Inventura1REZ2IsNumber = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Inventura1/REZ2IsNumber", _Inventura1REZ2IsNumber.ToString()));
                _Inventura1REZ2Mandatory = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Inventura1/REZ2Mandatory", _Inventura1REZ2Mandatory.ToString()));

				_Inventura1ZobrazitDialogZadaniMnozstviParsovanehoKodu = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Inventura1/ZobrazitDialogZadaniMnozstviParsovanehoKodu", _Inventura1ZobrazitDialogZadaniMnozstviParsovanehoKodu.ToString()));

                #region TaD Dialogy Inventura1
                _Inventura1_DialogOpusteniModulu = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Inventura1/Inventura1_DialogOpusteniModulu", _Inventura1_DialogOpusteniModulu.ToString()));
                #endregion

                //Inventura2
                _inventura2 = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Inventura2/Allow", _inventura2.ToString()));
                _inventura2Name = LoadElement(xmldoc, "/Config/Modules/Inventura2/NameModul", _inventura2Name);
                _Inventura2ShowInMST = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Inventura2/ShowInMST", _Inventura2ShowInMST.ToString()));
                _Inventura2DotazKancl = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Inventura2/DotazKancl", _Inventura2DotazKancl.ToString()));
                _Inventura2DotazLokace = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Inventura2/DotazLokace", _Inventura2DotazLokace.ToString()));
                _Inventura2DotazOsoba = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Inventura2/DotazOsoba", _Inventura2DotazOsoba.ToString()));
                _Inventura2DotazStredisko = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Inventura2/DotazStredisko", _Inventura2DotazStredisko.ToString()));
                #region TaD Dialogy Inventura2
                _Inventura2_DialogOpusteniModulu = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Inventura2/Inventura2_DialogOpusteniModulu", _Inventura2_DialogOpusteniModulu.ToString()));
                #endregion

                //Prodej
                _prodej = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Prodej/Allow", _prodej.ToString()));
                _prodejName = LoadElement(xmldoc, "/Config/Modules/Prodej/NameModul", _prodejName);

                //Vydej
                _vydej = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/Allow", _vydej.ToString()));
                _vydejName = LoadElement(xmldoc, "/Config/Modules/Vydej/NameModul", _vydejName);
                _vydejLocationQuestion = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/LocationQuestion", _vydejLocationQuestion.ToString()));
                _vydejItemTypeQuestion = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/ItemTypeQuestion", _vydejItemTypeQuestion.ToString()));
                _vydejGenerovaniPrikazuPozadovatSklad = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/GenerovaniPrikazuPozadovatSklad", _vydejGenerovaniPrikazuPozadovatSklad.ToString()));
                _vydejTypyPoctyPalet = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/TypyPoctyPalet", _vydejTypyPoctyPalet.ToString()));
                _vydejTypOznaceniPalety = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/TypOznaceniPalety", _vydejTypOznaceniPalety.ToString()));
                _vydejPovolitPreplneniPolozky = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/PovolitPreplneniPolozky", _vydejPovolitPreplneniPolozky.ToString()));
                _vydejZadaniLocncodePredSN = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/ZadaniLocncodePredSN", _vydejZadaniLocncodePredSN.ToString()));
                _vydejRezimCelePalety = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/RezimCelePalety", _vydejRezimCelePalety.ToString()));
                _vydejDoplnitPrefix = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/DoplnitPrefix", _vydejDoplnitPrefix.ToString()));
                _vydejMaxPocetDavek = int.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/MaxPocetDavek", _vydejMaxPocetDavek.ToString()));
                _vydejSequence = int.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/Sequence", _vydejSequence.ToString()));

                _vydejPocetDavekZobraz = int.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/PocetDavekZobraz", _vydejPocetDavekZobraz.ToString()));
                _VydejLocationPouzitCiselnik = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/LocationPouzitCiselnik", _VydejLocationPouzitCiselnik.ToString()));
                _VydejLocationFiltrovatData = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/LocationFiltrovatData", _VydejLocationFiltrovatData.ToString()));
                _VydejLocationAllowAutocommit = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/LocationAllowAutocommit", _VydejLocationAllowAutocommit.ToString()));
                _VydejPokracovatNaJinemTerminalu = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/PokracovatNaJinemTerminalu", _VydejPokracovatNaJinemTerminalu.ToString()));
                _VydejHledaniCkAutoVyberPrvniNeuplne = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/HledaniCkAutoVyberPrvniNeuplne", _VydejHledaniCkAutoVyberPrvniNeuplne.ToString()));
                _VydejRozsireni1Rezerva2Zadavat = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/Rozsireni1Rezerva2Zadavat", _VydejRozsireni1Rezerva2Zadavat.ToString()));
                _VydejRozsireni1Rezerva2Nazev = LoadElement(xmldoc, "/Config/Modules/Vydej/Rozsireni1Rezerva2Nazev", _VydejRozsireni1Rezerva2Nazev);
                _VydejRozsireni1Rezerva2Default = LoadElement(xmldoc, "/Config/Modules/Vydej/Rozsireni1Rezerva2Default", _VydejRozsireni1Rezerva2Default);
                _vydejZboziPouzitCiselnik = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/ZboziPouzitCiselnik", _vydejZboziPouzitCiselnik.ToString()));
                _vydejPovolitRazeniVydejek = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/PovolitRazeniVydejek", _vydejPovolitRazeniVydejek.ToString()));
                _vydejStahnoutNejvyssiPrioritu = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/StahnoutNejvyssiPrioritu", _vydejStahnoutNejvyssiPrioritu.ToString()));
                _vydejSlucovaniDavek = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/PovolitSlucovaniDavek", _vydejSlucovaniDavek.ToString()));
                _vydejHromadneVyplneni = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/PovolitHromadneVyplnovani", _vydejHromadneVyplneni.ToString()));

                _vydejPovolitZmenuOdberatele = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/PovolitZmenuOdberatele", _vydejPovolitZmenuOdberatele.ToString()));
                _vydejPocetKusuNaSkladeOnline = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/PocetKusuNaSklade", _vydejPocetKusuNaSkladeOnline.ToString()));
                _vydejPocetKusuOnline = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/PocetKusu", _vydejPocetKusuOnline.ToString()));
                _vydejGenerovatNenalezenouVydejku = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/GenerovatNenalezenouVydejku", _vydejGenerovatNenalezenouVydejku.ToString()));
                _vydejPolozkaDetailOnline = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/PolozkaDetail", _vydejPolozkaDetailOnline.ToString()));
                _vydejObjednavkaDetail = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/ObjednavkaDetail", _vydejObjednavkaDetail.ToString()));
                _vydejGenerovatDataPrikazuOnline = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/GenerovatDataPrikazuOnline", _vydejGenerovatDataPrikazuOnline.ToString()));
                _VydejEtiketaTiskPoVlozeniDotaz = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/EtiketaTiskPoVlozeniDotaz", _VydejEtiketaTiskPoVlozeniDotaz.ToString()));
                _VydejPolozkyVyberJenScannerem = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/PolozkyVyberJenScannerem", _VydejPolozkyVyberJenScannerem.ToString()));
                _VydejZboziVyberJenScannerem = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/ZboziVyberJenScannerem", _VydejZboziVyberJenScannerem.ToString()));
                _VydejZboziVyhledaniDleSloupce = LoadElement(xmldoc, "/Config/Modules/Vydej/ZboziVyhledaniDleSloupce", _VydejZboziVyhledaniDleSloupce.ToString());
                _VydejVyberTiskarnyDokladu = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/VyberTiskarnyDokladu", _VydejVyberTiskarnyDokladu.ToString()));
                _VydejVyberPracovnika = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/VyberPracovnika", _VydejVyberPracovnika.ToString()));
                _VydejDavkyVyberJenScannerem = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/DavkyVyberJenScannerem", _VydejDavkyVyberJenScannerem.ToString()));
                _VydejPovolitKontrolaSNPredloha = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/PovolitKontrolaSNPredloha", _VydejPovolitKontrolaSNPredloha.ToString()));
                _VydejPovolitOcipovani = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/PovolitOcipovani", _VydejPovolitOcipovani.ToString()));
                _vydejParsovaniCarovehoKoduPovolit = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/ParsovaniCarovehoKoduPovolit", _vydejParsovaniCarovehoKoduPovolit.ToString()));

                #region TaD Dialogy Vydej
                _VydejDialogUspesnehoOdeslaniDavky = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/VydejDialogUspesnehoOdeslaniDavky", _VydejDialogUspesnehoOdeslaniDavky.ToString()));
                _VydejDialogOpusteniModulu = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/VydejDialogOpusteniModulu", _VydejDialogOpusteniModulu.ToString()));
                _VydejDialogOpusteniVydejky = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/VydejDialogOpusteniVydejky", _VydejDialogOpusteniVydejky.ToString()));

                _vydejTimeDialogInterval = decimal.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/VydejTimeDialogInterval", _vydejTimeDialogInterval.ToString()));
                _vydejTimeDialogNasnimana = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/VydejTimeDialogNasnimana", _vydejTimeDialogNasnimana.ToString()));

                _vydejDialogDavkaNenalezenaVygenerovat = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/VydejDialogDavkaNenalezenaVygenerovat", _vydejDialogDavkaNenalezenaVygenerovat.ToString()));
                _vydejDialogNaDiskuNejsouDavkyStahnout = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/VydejDialogNaDiskuNejsouDavkyStahnout", _vydejDialogNaDiskuNejsouDavkyStahnout.ToString()));

                #endregion

                _vydej_HromadneSN = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/HromadneSN", _vydej_HromadneSN.ToString()));
                _vydej_HromadneSN_N = int.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/HromadneSN_N", _vydej_HromadneSN_N.ToString()));

				_vydej_HromadneBaliky = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/HromadneBaliky", _vydej_HromadneBaliky.ToString()));
				_vydej_PtatSeNaPamatovani = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/PtatSeNaPamatovaniRozmeru", _vydej_PtatSeNaPamatovani.ToString()));


                // Vydej online
                _vydej_Items_Online = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/Items_Online", _vydej_Items_Online.ToString()));
				_vydej_FIFOFEFO_Online = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/FIFOFEFO_Online", _vydej_FIFOFEFO_Online.ToString()));
				_vydej_ExpiraceCheck_Online = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/ExpiraceCheck_Online", _vydej_ExpiraceCheck_Online.ToString()));

				_vydej_TypSPrelokovanim = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/TypSPrelokovanim", _vydej_TypSPrelokovanim.ToString()));

				_Vydej_MnozstviAutoJedna = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/MnozstviAutoJedna", _Vydej_MnozstviAutoJedna.ToString()));                
                
                // Vydej sklady
                _vydejSkladPouzit = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/SkladPouzit", _vydejSkladPouzit.ToString()));
                _vydejSkladID = LoadElement(xmldoc, "/Config/Modules/Vydej/SkladID", _vydejSkladID);
                _vydejPrevzitIDSkladuZCiselnikuSkladu = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/PrevzitIDSkladuZCiselnikuSkladu", _vydejPrevzitIDSkladuZCiselnikuSkladu.ToString()));
                _vydejFiltrCiselnikSkladuOnlyOne = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/FiltrCiselnikSkladuOnlyOne", _vydejFiltrCiselnikSkladuOnlyOne.ToString()));
				
				_VydejTiskVariantaSoupis = (Fask.MST_W.Vydej_3.Varianta_TiskSoupis)Enum.Parse(typeof(Fask.MST_W.Vydej_3.Varianta_TiskSoupis), LoadElement(xmldoc, "/Config/Modules/Vydej/VydejTiskVariantaSoupis", _VydejTiskVariantaSoupis.ToString()), true);


				_Vydej_PokracovatJinyTerm_AnoNe = (Fask.MST_W.Vydej_3.Potvrzeni_AnoNe)Enum.Parse(typeof(Fask.MST_W.Vydej_3.Potvrzeni_AnoNe), LoadElement(xmldoc, "/Config/Modules/Vydej/PokracovatJinyTerm_AnoNe", _Vydej_PokracovatJinyTerm_AnoNe.ToString()), true);
				_VydejDialogPokracovatNaJinemTerm = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/DialogPokracovatNaJinemTerm", _VydejDialogPokracovatNaJinemTerm.ToString()));
				_VydejDialogTiskSoupis = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/DialogTiskSoupis", _VydejDialogTiskSoupis.ToString()));
				_VydejTiskSoupisMN_Auto1 = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Vydej/TiskSoupisMN_Auto1", _VydejTiskSoupisMN_Auto1.ToString()));
				

				#region F10 možnosti

				_F10_TiskPalListku = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Vydej/F10_TiskPalListku", _F10_TiskPalListku.ToString()));
				_F10_ZobrazitAlternativyLokaci = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Vydej/F10_ZobrazitAlternativyLokaci", _F10_ZobrazitAlternativyLokaci.ToString()));


				#endregion

                //Prijem
                _prijem = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Prijem/Allow", _prijem.ToString()));
                _prijemName = LoadElement(xmldoc, "/Config/Modules/Prijem/NameModul", _prijemName);
                _prijemModel = LoadElement(xmldoc, "/Config/Modules/Prijem/Model", _prijemModel);
                _prijemTimeDialog = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Prijem/PrijemTimeDialog", _prijemTimeDialog.ToString()));
                _prijemTimeDialogInterval = int.Parse(LoadElement(xmldoc, "/Config/Modules/Prijem/PrijemTimeDialogInterval", _prijemTimeDialogInterval.ToString()));

                // RFID Husky
                //_huskyVratkaName = LoadElement(xmldoc, "/Config/Modules/HuskyVratka/NameModul", _huskyVratkaName);
                //_huskyVratka = bool.Parse(LoadElement(xmldoc, "/Config/Modules/HuskyVratka/Allow", _huskyVratka.ToString()));
                //_HuskyVratkaShowInMST = bool.Parse(LoadElement(xmldoc, "/Config/Modules/HuskyVratka/ShowInMST", _HuskyVratkaShowInMST.ToString()));
                
                _OnlineObjednavkaDetailPovolit = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Prijem/ObjednavkaDetail", _OnlineObjednavkaDetailPovolit.ToString()));
                _OnlinePolozkaDetailPovolit = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Prijem/PolozkaDetail", _OnlinePolozkaDetailPovolit.ToString()));
                _OnlinePolozkaKusuNaSkladePovolit = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Prijem/PolozkaKusuNaSklade", _OnlinePolozkaKusuNaSkladePovolit.ToString()));
                _OnlinePolozkaKusuPovolit = bool.Parse(LoadElement(xmldoc, "/Config/Modules/Prijem/PolozkaKusuPovolit", _OnlinePolozkaKusuPovolit.ToString()));

                confignode = null;
                confignode = xmldoc.SelectSingleNode("/Config/Strings") as XmlElement;
                if (confignode != null)
                {
                    XmlNode xmlnode;

                    xmlnode = null;
                    xmlnode = confignode.SelectSingleNode("./EAN_L");
                    if (xmlnode != null && xmlnode.InnerText.Length > 0) MNName = xmlnode.InnerText;

                    xmlnode = null;
                    xmlnode = confignode.SelectSingleNode("./EAN_S");
                    if (xmlnode != null && xmlnode.InnerText.Length > 0) MNCode = xmlnode.InnerText;

                    xmlnode = null;
                    xmlnode = confignode.SelectSingleNode("./SN_L");
                    if (xmlnode != null && xmlnode.InnerText.Length > 0) SNName = xmlnode.InnerText;

                    xmlnode = null;
                    xmlnode = confignode.SelectSingleNode("./SN_S");
                    if (xmlnode != null && xmlnode.InnerText.Length > 0) SNCode = xmlnode.InnerText;

                    xmlnode = null;
                    xmlnode = confignode.SelectSingleNode("./SW_L");
                    if (xmlnode != null && xmlnode.InnerText.Length > 0) SWName = xmlnode.InnerText;

                    xmlnode = null;
                    xmlnode = confignode.SelectSingleNode("./SW_S");
                    if (xmlnode != null && xmlnode.InnerText.Length > 0) SWCode = xmlnode.InnerText;

                    xmlnode = null;
                    xmlnode = confignode.SelectSingleNode("./DV_L");
                    if (xmlnode != null && xmlnode.InnerText.Length > 0) DVName = xmlnode.InnerText;

                    xmlnode = null;
                    xmlnode = confignode.SelectSingleNode("./DV_S");
                    if (xmlnode != null && xmlnode.InnerText.Length > 0) DVCode = xmlnode.InnerText;

                    xmlnode = null;
                    xmlnode = confignode.SelectSingleNode("./PON_NAME");
                    if (xmlnode != null && xmlnode.InnerText.Length > 0) PON_NAME = xmlnode.InnerText;


                    xmlnode = null;
                    xmlnode = confignode.SelectSingleNode("./LC_NAME");
                    if (xmlnode != null && xmlnode.InnerText.Length > 0) LC_NAME = xmlnode.InnerText;

                    xmlnode = null;
                    xmlnode = confignode.SelectSingleNode("./REZ1_PRIJ_NAME");
                    if (xmlnode != null && xmlnode.InnerText.Length > 0) REZ1_PRIJ_NAME = xmlnode.InnerText;

                    xmlnode = null;
                    xmlnode = confignode.SelectSingleNode("./REZ2_PRIJ_NAME");
                    if (xmlnode != null && xmlnode.InnerText.Length > 0) REZ2_PRIJ_NAME = xmlnode.InnerText;

                    xmlnode = null;
                    xmlnode = confignode.SelectSingleNode("./REZ1_VYDE_NAME");
                    if (xmlnode != null && xmlnode.InnerText.Length > 0) REZ1_VYDE_NAME = xmlnode.InnerText;

                    xmlnode = null;
                    xmlnode = confignode.SelectSingleNode("./REZ2_VYDE_NAME");
                    if (xmlnode != null && xmlnode.InnerText.Length > 0) REZ2_VYDE_NAME = xmlnode.InnerText;



                    xmlnode = null;
                    xmlnode = confignode.SelectSingleNode("./REZ1_PROD_NAME");
                    if (xmlnode != null && xmlnode.InnerText.Length > 0) REZ1_PROD_NAME = xmlnode.InnerText;

                    xmlnode = null;
                    xmlnode = confignode.SelectSingleNode("./REZ2_PROD_NAME");
                    if (xmlnode != null && xmlnode.InnerText.Length > 0) REZ2_PROD_NAME = xmlnode.InnerText;

                    xmlnode = null;
                    xmlnode = confignode.SelectSingleNode("./REZ3_PROD_NAME");
                    if (xmlnode != null && xmlnode.InnerText.Length > 0) REZ3_PROD_NAME = xmlnode.InnerText;

                    xmlnode = null;
                    xmlnode = confignode.SelectSingleNode("./REZ4_PROD_NAME");
                    if (xmlnode != null && xmlnode.InnerText.Length > 0) REZ4_PROD_NAME = xmlnode.InnerText;


                }

                return true;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                return false;
            }
        }

        public static void SaveElement(XmlDocument xmldoc, string vetevuzelname, string uzelvalue)
        {
            int index = vetevuzelname.LastIndexOf("/");
            string vetevname = vetevuzelname.Substring(0, index);
            string uzelname = vetevuzelname.Substring(index + 1);
            SaveElement(xmldoc, vetevname, uzelname, uzelvalue);
        }

        /// <summary>
        /// ulozi do xml hodnotu 
        /// </summary>
        /// <param name="xmldoc">xmldocument do ktereho se bude ukladat</param>
        /// <param name="vetevname">xpath cesta k vetvi (/Config/Modules/Inventura1/)</param>
        /// <param name="nodename">nazev uzlu ktery nese hodnotu (Allow)</param>
        /// <param name="nodevalue">textova hodnota v uzlu (true)</param>
        public static void SaveElement(XmlDocument xmldoc, string vetevname, string uzelname, string uzelvalue)
        {
            XmlElement vetevnode = xmldoc.SelectSingleNode(vetevname) as XmlElement;
            if (vetevnode == null) // TODO : pokud vetev neexistuje, tak doplnit ...
            {
                string[] uzly = vetevname.Split(new char[] { '/' });
                if (uzly.Length <= 3) //nevlozi pokud jsou jen 2 uzly (/Config/Terminal)
                    return;           //musi byt nejmene 3 aby nedoslo k chybe na urovni rozdeleneho configu (/Config/Modules/<Modul>) ...

                int index = vetevname.LastIndexOf("/");
                string vetevhlavni = vetevname.Substring(0, index);
                string uzelhlavni = vetevname.Substring(index + 1);

                XmlElement vetevhlavninode = xmldoc.SelectSingleNode(vetevhlavni) as XmlElement;
                if (vetevhlavninode == null) // vubec neexistuje sekce => chyba ...
                    return;

                XmlElement uzelhlavninode = xmldoc.CreateElement(uzelhlavni);
                vetevhlavninode.AppendChild(uzelhlavninode);

                vetevnode = uzelhlavninode;

            }

            XmlElement uzelnode = vetevnode.SelectSingleNode(uzelname) as XmlElement;
            if (uzelnode == null)
            {
                uzelnode = xmldoc.CreateElement(uzelname);
                vetevnode.AppendChild(uzelnode);
            }

            try { uzelnode.InnerText = uzelvalue; }
            catch { }
        }

        public static bool Save(string filename)
        {
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(filename);

                //Terminal
                SaveElement(xmldoc, "/Config/Terminal/ScrollDataGridUp", _DataGridScrollUp.ToString());
                SaveElement(xmldoc, "/Config/Terminal/ScrollDataGridDown", _DataGridScrollDown.ToString());
                SaveElement(xmldoc, "/Config/Terminal/SqlCe", _sqlCe.ToString());
                SaveElement(xmldoc, "/Config/Terminal/ID", _terminalID.ToString());

				SaveElement(xmldoc, "/Config/Terminal/api_konstant", _api_konstant);
				SaveElement(xmldoc, "/Config/Terminal/Address", _serverAddress);
				SaveElement(xmldoc, "/Config/Terminal/isHTTPS", _isHTTPS.ToString());
				SaveElement(xmldoc, "/Config/Terminal/autorizace_API", _autorizace_API);
                SaveElement(xmldoc, "/Config/Terminal/ServiceTimeOut", _serviceTimeOut.ToString());

                SaveElement(xmldoc, "/Config/Terminal/ServerAccess", _serverAccess.ToString());
                SaveElement(xmldoc, "/Config/Terminal/ServerAccessUsername", _serverAccessUsername);
                SaveElement(xmldoc, "/Config/Terminal/ServerAccessPassword", _serverAccessPassword);
                SaveElement(xmldoc, "/Config/Terminal/ServerAccessDomain", _serverAccessDomain);
                SaveElement(xmldoc, "/Config/Terminal/ServerAccessPreauthenticate", _serverAccessPreauthenticate.ToString());
                SaveElement(xmldoc, "/Config/Terminal/ServerAccessAllowRedirection", _serverAccessAllowRedirection.ToString());
                SaveElement(xmldoc, "/Config/Terminal/ServerAccessAllowDecompression", _serverAccessAllowDecompression.ToString());
                SaveElement(xmldoc, "/Config/Terminal/ServerAccessCertificatesTrust", _serverAccessCertificateTrust.ToString());
                //SaveElement(xmldoc, "/Config/Terminal/ScannerType", _ScannerType.ToString());
                SaveElement(xmldoc, "/Config/Terminal/Storage", _Storage);
                SaveElement(xmldoc, "/Config/Terminal/MemorySize", _MemorySize);
                SaveElement(xmldoc, "/Config/Terminal/MemoryChecked", _MemoryChecked.ToString());
                SaveElement(xmldoc, "/Config/Terminal/LogUploadInterval", _LogUploadInterval.ToString());
                SaveElement(xmldoc, "/Config/Terminal/Log", Fask.Logging.Log.Enable.ToString());
                SaveElement(xmldoc, "/Config/Terminal/LogAdvanced", Fask.Logging.Log.EnableAdvanced.ToString());
                SaveElement(xmldoc, "/Config/Terminal/Trace", Fask.Logging.Trace2.Enable.ToString());
                SaveElement(xmldoc, "/Config/Terminal/Trace_Separator", Fask.Logging.Trace2.Separator);
                SaveElement(xmldoc, "/Config/Terminal/ShowPanelButtons", _ShowPanelButtons.ToString());
                SaveElement(xmldoc, "/Config/Terminal/PovolitAktualizacePristupu", _povolitAktualizacePristupu.ToString());
                SaveElement(xmldoc, "/Config/Terminal/LokalizacePovolit", _lokalizacePovolit.ToString());
                SaveElement(xmldoc, "/Config/Terminal/LokalizaceVlastniPovolit", _LokalizaceVlastniPovolit.ToString());
                SaveElement(xmldoc, "/Config/Terminal/LokalizaceZvolena", _lokalizaceZvolena.ToString());
                
                //===========================================
                //Print server
                /*
                SaveElement(xmldoc, "/Config/Terminal/PrintServerTemplateNamePrijemPredloha", _PrintServerTemplateNamePrijemPredloha);
                SaveElement(xmldoc, "/Config/Terminal/PrintServerTemplateNamePrijemNasnimane", _PrintServerTemplateNamePrijemNasnimane);
                SaveElement(xmldoc, "/Config/Terminal/PrintServerTemplateNameProdejPredloha", _PrintServerTemplateNameProdejPredloha);
                SaveElement(xmldoc, "/Config/Terminal/PrintServerTemplateNameProdejNasnimane", _PrintServerTemplateNameProdejNasnimane);
                SaveElement(xmldoc, "/Config/Terminal/PrintServerTemplateNameInventuraPredloha", _PrintServerTemplateNameInventuraPredloha);
                SaveElement(xmldoc, "/Config/Terminal/PrintServerTemplateNameInventuraNasnimane", _PrintServerTemplateNameInventuraNasnimane);
                SaveElement(xmldoc, "/Config/Terminal/PrintServerTemplateNameVydejPredloha", _PrintServerTemplateNameVydejPredloha);
                SaveElement(xmldoc, "/Config/Terminal/PrintServerTemplateNameVydejNasnimane", _PrintServerTemplateNameVydejNasnimane);
                SaveElement(xmldoc, "/Config/Terminal/PrintServerTemplateNameVydejPalListek", _PrintServerTemplateNameVydejPalListek);
                */
                SaveElement(xmldoc, "/Config/Terminal/PrintServer", _povolitPrintServer.ToString());

                //===========================================
                //RFID terminal
                SaveElement(xmldoc, "/Config/Terminal/RFIDScannerType", _RFIDScannerType.ToString());
                SaveElement(xmldoc, "/Config/Terminal/RFIDAllowUHF", _RFIDPovolitUHF.ToString());
                SaveElement(xmldoc, "/Config/Terminal/RFIDLoadAsciiTextData", _RFIDukladatNacitatText.ToString());
                SaveElement(xmldoc, "/Config/Terminal/RFIDPowerLevel", _RFIDPowerLevel.ToString());

                SaveElement(xmldoc, "/Config/Terminal/RFID_memoryEPC", _RFID_memoryEPC.ToString());
                SaveElement(xmldoc, "/Config/Terminal/RFID_memoryUSER", _RFID_memoryUSER.ToString());
                SaveElement(xmldoc, "/Config/Terminal/RFID_memoryRESERVED", _RFID_memoryRESERVED.ToString());
                SaveElement(xmldoc, "/Config/Terminal/RFID_memoryTID", _RFID_memoryTID.ToString());
                //===========================================

                //Servis
                SaveElement(xmldoc, "/Config/Modules/Servis/Allow", _ServisEnable.ToString());
                SaveElement(xmldoc, "/Config/Modules/Servis/ShowInMST", _ServisShowInMST.ToString());
                SaveElement(xmldoc, "/Config/Modules/Servis/TimeoutOnline", _ServisTimeoutOnline.ToString());
                SaveElement(xmldoc, "/Config/Modules/Servis/TimeoutSynchronize", _ServisTimeoutSynchronize.ToString());
                SaveElement(xmldoc, "/Config/Modules/Servis/AutoUpdateInterval", _ServisAutoUpdateInterval.ToString());
                SaveElement(xmldoc, "/Config/Modules/Servis/AllowCamera", _ServisAllowCamera.ToString());
                SaveElement(xmldoc, "/Config/Modules/Servis/DavkoveZpracovani", _ServisDavkoveZpracovani.ToString());
                SaveElement(xmldoc, "/Config/Modules/Servis/VyberOdberatelPovolit", _ServisVyberOdberatelPovolit.ToString());
                SaveElement(xmldoc, "/Config/Modules/Servis/VyberOkruhPovolit", _ServisVyberOkruhPovolit.ToString());
                SaveElement(xmldoc, "/Config/Modules/Servis/UkonceniStavuNavrat", _ServisUkonceniStavuNavrat.ToString());
                SaveElement(xmldoc, "/Config/Modules/Servis/ServisPozadovatPotvrzeniZmenyStavu", _ServisPozadovatPotvrzeniZmenyStavu.ToString());
                SaveElement(xmldoc, "/Config/Modules/Servis/DavkaOtevritIhnedPoStazeni", _ServisDavkaOtevritIhnedPoStazeni.ToString());
                SaveElement(xmldoc, "/Config/Modules/Servis/FiltrZobrazeniPovolit", _ServisFiltrZobrazeniPovolit.ToString());
                SaveElement(xmldoc, "/Config/Modules/Servis/AutomatickaZmenaStavuPovolit", _ServisAutomatickaZmenaStavuPovolit.ToString());
                SaveElement(xmldoc, "/Config/Modules/Servis/ServisPozadovatPotvrzeniZmenyCinnosti", _ServisPozadovatPotvrzeniZmenyCinnosti.ToString());
                SaveElement(xmldoc, "/Config/Modules/Servis/SynchronizaceZdrojePoVyberuPovolit", _ServisSynchronizaceZdrojePoVyberuPovolit.ToString());
                SaveElement(xmldoc, "/Config/Modules/Servis/ServisPrehratZvukPoVyberuMoznosti", _ServisPrehratZvukPoVyberuMoznosti.ToString());
                SaveElement(xmldoc, "/Config/Modules/Servis/ServisStatusBarZobrazitNazevStavu", _ServisStatusBarZobrazitNazevStavu.ToString());
                SaveElement(xmldoc, "/Config/Modules/Servis/ServisStatusBarZobrazitIdZdroje", _ServisStatusBarZobrazitIdZdroje.ToString());
                SaveElement(xmldoc, "/Config/Modules/Servis/ServisStatusBarZobrazitTypZdroje", _ServisStatusBarZobrazitTypZdroje.ToString());
                SaveElement(xmldoc, "/Config/Modules/Servis/ServisStatusBarZobrazitOznaceniZdroje", _ServisStatusBarZobrazitOznaceniZdroje.ToString());
                SaveElement(xmldoc, "/Config/Modules/Servis/ServisZobrazitInformaciOUkonceniStavu", _ServisZobrazitInformaciOUkonceniStavu.ToString());
                SaveElement(xmldoc, "/Config/Modules/Servis/PovolitOdeslaniDatNaPozadi", _ServisOdeslaniDatNaPozadiPovolit.ToString());
                SaveElement(xmldoc, "/Config/Modules/Servis/PovolitOdeslaniDatPoUkonceniZadavani", _ServisOdeslaniDatPoUkonceniZadavaniPovolit.ToString());
                SaveElement(xmldoc, "/Config/Modules/Servis/OnScannerSound_ServisModul", _OnScannerSound_ServisModul.ToString());
                #region TaD Dialogy Servis
                SaveElement(xmldoc, "/Config/Modules/Servis/Servis_DialogOpusteniModulu", _Servis_DialogOpusteniModulu.ToString());
                #endregion
                //===========================================
                //Udalosti (Events)
                SaveElement(xmldoc, "/Config/Modules/Events/Allow", _EventsEnable.ToString());
                SaveElement(xmldoc, "/Config/Modules/Events/ShowInMST", _EventsShowInMST.ToString());
                SaveElement(xmldoc, "/Config/Modules/Events/Online", _EventsOnline.ToString());
                SaveElement(xmldoc, "/Config/Modules/Events/OnlineTimeout", _EventsOnlineTimeout.ToString());
                SaveElement(xmldoc, "/Config/Modules/Events/OnlineConfirm", _EventsOnlineConfirm.ToString());
                SaveElement(xmldoc, "/Config/Modules/Events/SynchronizationInterval", _EventsSynchronizationInterval.ToString());
                //===========================================
                //Ukoly (Tasks)
                SaveElement(xmldoc, "/Config/Modules/Tasks/Allow", _TasksEnable.ToString());
                SaveElement(xmldoc, "/Config/Modules/Tasks/ShowInMST", _TasksShowInMST.ToString());
                SaveElement(xmldoc, "/Config/Modules/Tasks/Timeout", _TasksTimeout.ToString());
                SaveElement(xmldoc, "/Config/Modules/Tasks/SynchronizationInterval", _TasksSynchronizationInterval.ToString());
                SaveElement(xmldoc, "/Config/Modules/Tasks/NotifyDialogShowSeconds", _TasksNotifyDialogShowSeconds.ToString());

                //Inventura1
                SaveElement(xmldoc, "/Config/Modules/Inventura1/Allow", _inventura1.ToString());
                SaveElement(xmldoc, "/Config/Modules/Inventura1/NameModul", _inventura1Name);
                SaveElement(xmldoc, "/Config/Modules/Inventura1/ShowInMST", _Inventura1ShowInMST.ToString());
                SaveElement(xmldoc, "/Config/Modules/Inventura1/PolozkaNasnimatPouzeJednou", _Inventura1PolozkaNasnimatPouzeJednou.ToString());
                SaveElement(xmldoc, "/Config/Modules/Inventura1/OnlineKontrola", _Inventura1OnlineKontrola.ToString());
                SaveElement(xmldoc, "/Config/Modules/Inventura1/ZadaniLocncodePredSN", _inventura1ZadaniLocncodePredSN.ToString());
                SaveElement(xmldoc, "/Config/Modules/Inventura1/ZadaniLocncodeJednou", _inventura1ZadaniLocncodeJednou.ToString());
                SaveElement(xmldoc, "/Config/Modules/Inventura1/ZadaniLocncodePamatovatPosledni", _inventura1ZadaniLocncodePamatovatPosledni.ToString());
                SaveElement(xmldoc, "/Config/Modules/Inventura1/OnlineTimeout", _Inventura1OnlineTimeout.ToString());
                SaveElement(xmldoc, "/Config/Modules/Inventura1/ChunkTimeout", _Inventura1ChunkTimeout.ToString());
                SaveElement(xmldoc, "/Config/Modules/Inventura1/PouzitCiselnikSkladu", _Inventura1PouzitCiselnikSkladu.ToString());
                SaveElement(xmldoc, "/Config/Modules/Inventura1/PolozkyVyberJenScannerem", _Inventura1PolozkyVyberJenScannerem.ToString());
                SaveElement(xmldoc, "/Config/Modules/Inventura1/OnScannerSound_Inventura1_sqlc", _OnScannerSound_Inventura1_sqlc.ToString());
				SaveElement(xmldoc, "/Config/Modules/Inventura1/ParsovaniCarovehoKoduPovolit", _Inventura1ParsovaniCarovehoKoduPovolit.ToString());
				SaveElement(xmldoc, "/Config/Modules/Inventura1/Inventura1KontrolaSarzeUParsovanehoCarovehoKoduPovolit", _Inventura1KontrolaSarzeUParsovanehoCarovehoKoduPovolit.ToString());

				SaveElement(xmldoc, "/Config/Modules/Inventura1/ZobrazitDialogZadaniMnozstviParsovanehoKodu", _Inventura1ZobrazitDialogZadaniMnozstviParsovanehoKodu.ToString());
				

                #region TaD Dialogy Inventura1
                SaveElement(xmldoc, "/Config/Modules/Inventura1/Inventura1_DialogOpusteniModulu", _Inventura1_DialogOpusteniModulu.ToString());
                #endregion

                // Expedice
                SaveElement(xmldoc, "/Config/Modules/Expedice/Allow", _expedice.ToString());
                SaveElement(xmldoc, "/Config/Modules/Expedice/NameModul", _expediceName);
                SaveElement(xmldoc, "/Config/Modules/Expedice/ShowInMST", _ExpediceShowInMST.ToString());
                SaveElement(xmldoc, "/Config/Modules/Expedice/OnScannerSound_Expedice", _OnScannerSound_Expedice.ToString());

                //Inventura2
                SaveElement(xmldoc, "/Config/Modules/Inventura2/Allow", _inventura2.ToString());
                SaveElement(xmldoc, "/Config/Modules/Inventura2/NameModul", _inventura2Name);
                SaveElement(xmldoc, "/Config/Modules/Inventura2/ShowInMST", _Inventura2ShowInMST.ToString());
                SaveElement(xmldoc, "/Config/Modules/Inventura2/DotazKancl", _Inventura2DotazKancl.ToString());
                SaveElement(xmldoc, "/Config/Modules/Inventura2/DotazLokace", _Inventura2DotazLokace.ToString());
                SaveElement(xmldoc, "/Config/Modules/Inventura2/DotazOsoba", _Inventura2DotazOsoba.ToString());
                SaveElement(xmldoc, "/Config/Modules/Inventura2/DotazStredisko", _Inventura2DotazStredisko.ToString());
                SaveElement(xmldoc, "/Config/Modules/Inventura2/OnScannerSound_Inventura2", _OnScannerSound_Inventura2.ToString());
                
                #region TaD Dialogy Inventura2

                SaveElement(xmldoc, "/Config/Modules/Inventura2/Inventura2_DialogOpusteniModulu", _Inventura2_DialogOpusteniModulu.ToString());
                
                #endregion

                //Prodej
                SaveElement(xmldoc, "/Config/Modules/Prodej/Allow", _prodej.ToString());
                SaveElement(xmldoc, "/Config/Modules/Prodej/NameModul", _prodejName);
                SaveElement(xmldoc, "/Config/Modules/Prodej/OnScannerSound_Prodej_3", _OnScannerSound_Prodej_3.ToString());

                //Vydej
                SaveElement(xmldoc, "/Config/Modules/Vydej/Allow", _vydej.ToString());
                SaveElement(xmldoc, "/Config/Modules/Vydej/NameModul", _vydejName);
                SaveElement(xmldoc, "/Config/Modules/Vydej/LocationQuestion", _vydejLocationQuestion.ToString());
                SaveElement(xmldoc, "/Config/Modules/Vydej/ItemTypeQuestion", _vydejItemTypeQuestion.ToString());
                SaveElement(xmldoc, "/Config/Modules/Vydej/GenerovaniPrikazuPozadovatSklad", _vydejGenerovaniPrikazuPozadovatSklad.ToString());
                SaveElement(xmldoc, "/Config/Modules/Vydej/TypOznaceniPalety", _vydejTypOznaceniPalety.ToString());
                SaveElement(xmldoc, "/Config/Modules/Vydej/TypyPoctyPalet", _vydejTypyPoctyPalet.ToString());
                SaveElement(xmldoc, "/Config/Modules/Vydej/PovolitPreplneniPolozky", _vydejPovolitPreplneniPolozky.ToString());
                SaveElement(xmldoc, "/Config/Modules/Vydej/ZadaniLocncodePredSN", _vydejZadaniLocncodePredSN.ToString());
                SaveElement(xmldoc, "/Config/Modules/Vydej/RezimCelePalety", _vydejRezimCelePalety.ToString());
                SaveElement(xmldoc, "/Config/Modules/Vydej/DoplnitPrefix", _vydejDoplnitPrefix.ToString());
                SaveElement(xmldoc, "/Config/Modules/Vydej/Sequence", _vydejSequence.ToString());

                SaveElement(xmldoc, "/Config/Modules/Vydej/MaxPocetDavek", _vydejMaxPocetDavek.ToString());
                SaveElement(xmldoc, "/Config/Modules/Vydej/PocetDavekZobraz", _vydejPocetDavekZobraz.ToString());
                SaveElement(xmldoc, "/Config/Modules/Vydej/LocationPouzitCiselnik", _VydejLocationPouzitCiselnik.ToString());
                SaveElement(xmldoc, "/Config/Modules/Vydej/LocationFiltrovatData", _VydejLocationFiltrovatData.ToString());
                SaveElement(xmldoc, "/Config/Modules/Vydej/LocationAllowAutocommit", _VydejLocationAllowAutocommit.ToString());
                SaveElement(xmldoc, "/Config/Modules/Vydej/PokracovatNaJinemTerminalu", _VydejPokracovatNaJinemTerminalu.ToString());
                SaveElement(xmldoc, "/Config/Modules/Vydej/HledaniCkAutoVyberPrvniNeuplne", _VydejHledaniCkAutoVyberPrvniNeuplne.ToString());
                SaveElement(xmldoc, "/Config/Modules/Vydej/Rozsireni1Rezerva2Zadavat", _VydejRozsireni1Rezerva2Zadavat.ToString());
                SaveElement(xmldoc, "/Config/Modules/Vydej/Rozsireni1Rezerva2Nazev", _VydejRozsireni1Rezerva2Nazev);
                SaveElement(xmldoc, "/Config/Modules/Vydej/Rozsireni1Rezerva2Default", _VydejRozsireni1Rezerva2Default);
                SaveElement(xmldoc, "/Config/Modules/Vydej/ZboziPouzitCiselnik", _vydejZboziPouzitCiselnik.ToString());
                SaveElement(xmldoc, "/Config/Modules/Vydej/PovolitRazeniVydejek", _vydejPovolitRazeniVydejek.ToString());
                SaveElement(xmldoc, "/Config/Modules/Vydej/StahnoutNejvyssiPrioritu", _vydejStahnoutNejvyssiPrioritu.ToString());
                SaveElement(xmldoc, "/Config/Modules/Vydej/PovolitSlucovaniDavek", _vydejSlucovaniDavek.ToString());
                SaveElement(xmldoc, "/Config/Modules/Vydej/PovolitHromadneVyplnovani", _vydejHromadneVyplneni.ToString());

                SaveElement(xmldoc, "/Config/Modules/Vydej/PovolitZmenuOdberatele", _vydejPovolitZmenuOdberatele.ToString());
                SaveElement(xmldoc, "/Config/Modules/Vydej/PocetKusuNaSklade", _vydejPocetKusuNaSkladeOnline.ToString());
                SaveElement(xmldoc, "/Config/Modules/Vydej/PocetKusu", _vydejPocetKusuOnline.ToString());
                SaveElement(xmldoc, "/Config/Modules/Vydej/GenerovatNenalezenouVydejku", _vydejGenerovatNenalezenouVydejku.ToString());
                SaveElement(xmldoc, "/Config/Modules/Vydej/PolozkaDetail", _vydejPolozkaDetailOnline.ToString());
                SaveElement(xmldoc, "/Config/Modules/Vydej/ObjednavkaDetail", _vydejObjednavkaDetail.ToString());
                SaveElement(xmldoc, "/Config/Modules/Vydej/GenerovatDataPrikazuOnline", _vydejGenerovatDataPrikazuOnline.ToString());
                SaveElement(xmldoc, "/Config/Modules/Vydej/EtiketaTiskPoVlozeniDotaz", _VydejEtiketaTiskPoVlozeniDotaz.ToString());
                SaveElement(xmldoc, "/Config/Modules/Vydej/PolozkyVyberJenScannerem", _VydejPolozkyVyberJenScannerem.ToString());
                SaveElement(xmldoc, "/Config/Modules/Vydej/ZboziVyberJenScannerem", _VydejZboziVyberJenScannerem.ToString());
                SaveElement(xmldoc, "/Config/Modules/Vydej/ZboziVyhledaniDleSloupce", _VydejZboziVyhledaniDleSloupce.ToString());
                SaveElement(xmldoc, "/Config/Modules/Vydej/VyberTiskarnyDokladu", _VydejVyberTiskarnyDokladu.ToString());
                SaveElement(xmldoc, "/Config/Modules/Vydej/VyberPracovnika", _VydejVyberPracovnika.ToString());
                SaveElement(xmldoc, "/Config/Modules/Vydej/DavkyVyberJenScannerem", _VydejDavkyVyberJenScannerem.ToString());
                SaveElement(xmldoc, "/Config/Modules/Vydej/PovolitKontrolaSNPredloha", _VydejPovolitKontrolaSNPredloha.ToString());
                SaveElement(xmldoc, "/Config/Modules/Vydej/PovolitOcipovani", _VydejPovolitOcipovani.ToString());
                SaveElement(xmldoc, "/Config/Modules/Vydej/ParsovaniCarovehoKoduPovolit", _vydejParsovaniCarovehoKoduPovolit.ToString());
                #region TaD Dialogy Vydej
                SaveElement(xmldoc, "/Config/Modules/Vydej/VydejDialogUspesnehoOdeslaniDavky", _VydejDialogUspesnehoOdeslaniDavky.ToString());
                SaveElement(xmldoc, "/Config/Modules/Vydej/VydejDialogOpusteniModulu", _VydejDialogOpusteniModulu.ToString());
                SaveElement(xmldoc, "/Config/Modules/Vydej/VydejDialogOpusteniVydejky", _VydejDialogOpusteniVydejky.ToString());

                SaveElement(xmldoc, "/Config/Modules/Vydej/VydejTimeDialogInterval", _vydejTimeDialogInterval.ToString());
                SaveElement(xmldoc, "/Config/Modules/Vydej/VydejTimeDialogNasnimana", _vydejTimeDialogNasnimana.ToString());

                SaveElement(xmldoc, "/Config/Modules/Vydej/VydejDialogDavkaNenalezenaVygenerovat", _vydejDialogDavkaNenalezenaVygenerovat.ToString());
                SaveElement(xmldoc, "/Config/Modules/Vydej/VydejDialogNaDiskuNejsouDavkyStahnout", _vydejDialogNaDiskuNejsouDavkyStahnout.ToString());
                
                #endregion

                SaveElement(xmldoc, "/Config/Modules/Vydej/HromadneSN", _vydej_HromadneSN.ToString());
                SaveElement(xmldoc, "/Config/Modules/Vydej/HromadneSN_N", _vydej_HromadneSN_N.ToString());

				SaveElement(xmldoc, "/Config/Modules/Vydej/HromadneBaliky", _vydej_HromadneBaliky.ToString());
				SaveElement(xmldoc, "/Config/Modules/Vydej/PtatSeNaPamatovaniRozmeru", _vydej_PtatSeNaPamatovani.ToString());


                // Vydej online
                SaveElement(xmldoc, "/Config/Modules/Vydej/Items_Online", _vydej_Items_Online.ToString());
				SaveElement(xmldoc, "/Config/Modules/Vydej/FIFOFEFO_Online", _vydej_FIFOFEFO_Online.ToString());
				SaveElement(xmldoc, "/Config/Modules/Vydej/ExpiraceCheck_Online", _vydej_ExpiraceCheck_Online.ToString());
				SaveElement(xmldoc, "/Config/Modules/Vydej/TypSPrelokovanim", _vydej_TypSPrelokovanim.ToString());

				SaveElement(xmldoc, "/Config/Modules/Vydej/MnozstviAutoJedna", _Vydej_MnozstviAutoJedna.ToString());

				#region F10 monosti

				SaveElement(xmldoc, "/Config/Modules/Vydej/F10_TiskPalListku", _F10_TiskPalListku.ToString());
				SaveElement(xmldoc, "/Config/Modules/Vydej/F10_ZobrazitAlternativyLokaci", _F10_ZobrazitAlternativyLokaci.ToString());
				#endregion

                // Vydej sklady
                SaveElement(xmldoc, "/Config/Modules/Vydej/SkladPouzit", _vydejSkladPouzit.ToString());
                SaveElement(xmldoc, "/Config/Modules/Vydej/SkladID", _vydejSkladID);
                SaveElement(xmldoc, "/Config/Modules/Vydej/PrevzitIDSkladuZCiselnikuSkladu", _vydejPrevzitIDSkladuZCiselnikuSkladu.ToString());
                SaveElement(xmldoc, "/Config/Modules/Vydej/FiltrCiselnikSkladuOnlyOne", _vydejFiltrCiselnikSkladuOnlyOne.ToString());

				SaveElement(xmldoc, "/Config/Modules/Vydej/VydejTiskVariantaSoupis", _VydejTiskVariantaSoupis.ToString());

				SaveElement(xmldoc, "/Config/Modules/Vydej/PokracovatJinyTerm_AnoNe", _Vydej_PokracovatJinyTerm_AnoNe.ToString());
				SaveElement(xmldoc, "/Config/Modules/Vydej/DialogPokracovatNaJinemTerm", _VydejDialogPokracovatNaJinemTerm.ToString());
				SaveElement(xmldoc, "/Config/Modules/Vydej/DialogTiskSoupis", _VydejDialogTiskSoupis.ToString());
				SaveElement(xmldoc, "/Config/Modules/Vydej/TiskSoupisMN_Auto1", _VydejTiskSoupisMN_Auto1.ToString());

				SaveElement(xmldoc, "/Config/Modules/Vydej/OnScannerSound_Vydej_3", _OnScannerSound_Vydej_3.ToString());
                //Prijem
                SaveElement(xmldoc, "/Config/Modules/Prijem/Allow", _prijem.ToString());
                SaveElement(xmldoc, "/Config/Modules/Prijem/NameModul", _prijemName);
                SaveElement(xmldoc, "/Config/Modules/Prijem/Model", _prijemModel);
                SaveElement(xmldoc, "/Config/Modules/Prijem/PrijemTimeDialog", _prijemTimeDialog.ToString());
                SaveElement(xmldoc, "/Config/Modules/Prijem/PrijemTimeDialogInterval", _prijemTimeDialogInterval.ToString());

                // RFID Husky
                //SaveElement(xmldoc, "/Config/Modules/HuskyVratka/Allow", _huskyVratka.ToString());
                //SaveElement(xmldoc, "/Config/Modules/HuskyVratka/NameModul", _huskyVratkaName);
                //SaveElement(xmldoc, "/Config/Modules/HuskyVratka/ShowInMST", _HuskyVratkaShowInMST.ToString());

                SaveElement(xmldoc, "/Config/Modules/Prijem/ObjednavkaDetail", _OnlineObjednavkaDetailPovolit.ToString());
                SaveElement(xmldoc, "/Config/Modules/Prijem/PolozkaDetail", _OnlinePolozkaDetailPovolit.ToString());
                SaveElement(xmldoc, "/Config/Modules/Prijem/PolozkaKusuNaSklade", _OnlinePolozkaKusuNaSkladePovolit.ToString());
                SaveElement(xmldoc, "/Config/Modules/Prijem/PolozkaKusuPovolit", _OnlinePolozkaKusuPovolit.ToString());
                SaveElement(xmldoc, "/Config/Modules/Prijem/OnScannerSound_Online", _OnScannerSound_Online.ToString());
                SaveElement(xmldoc, "/Config/Modules/Prijem/OnScannerSound_Prijem_4", _OnScannerSound_Prijem_4.ToString());

                xmldoc.Save(filename);

                return true;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message, "MST_Global.Save");
                return false;
            }
        }
    }
}
