using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Collections.Specialized;
using Microsoft.Win32;

namespace Fask.MST_W
{
    public class Settings
    {
        private static NameValueCollection m_settings;
        private static string m_settingsPath;

        #region ************ Parsovani kodu ***************

        public static Parsing.Config Parsing_Config
        {
            get
            {
                if (!Parsing_Enabled)
                    return null;

                Parsing.Config config = new Fask.Parsing.Config(
                    Parsing_WeightCode_12,
                    Parsing_WeightCode,
                    Parsing_SABNeznamyKod,
                    Parsing_BarcodeSlashSarze,
                    Parsing_FenixBarcodeObal,
                    Parsing_HIBC,
                    Parsing_GS1,
					Parsing_SAB_AustralianNorm,
					Parsing_SAB_GS1_Zavorky,
					Parsing_SAB_GS1_BALTON,
					Parsing_InnovaMedical_GS1_Datum,
					Parsing_SAB_GS1_BELDICO
                    );
                return config;
            }
        }

        public static bool Parsing_Enabled
        {
            get { return bool.Parse(GetValue("Parsing_Enabled", false.ToString())); }
            set { SetValue("Parsing_Enabled", value.ToString()); }
        }

        /// <summary>
        /// WeightCode_12
        /// </summary>
        /// <remarks>Reseno pro fy Vlach</remarks>
        public static bool Parsing_WeightCode_12
        {
            get { return bool.Parse(GetValue("Parsing_WeightCode_12", false.ToString())); }
            set { SetValue("Parsing_WeightCode_12", value.ToString()); }
        }

        /// <summary>
        /// WeightCode
        /// </summary>
        /// <remarks>Reseno pro fy Steinex</remarks>
        public static bool Parsing_WeightCode
        {
            get { return bool.Parse(GetValue("Parsing_WeightCode", false.ToString())); }
            set { SetValue("Parsing_WeightCode", value.ToString()); }
        }

        /// <summary>
        /// SABNeznamyKod
        /// </summary>
        /// <remarks>Reseno pro fy SAB - dodavatel allwin (USA)</remarks>
        public static bool Parsing_SABNeznamyKod
        {
            get { return bool.Parse(GetValue("Parsing_SABNeznamyKod", false.ToString())); }
            set { SetValue("Parsing_SABNeznamyKod", value.ToString()); }
        }

		/// <summary>
		/// SAB_AustralianNorm
		/// </summary>
		/// <remarks>Reseno pro fy SAB - dodavatel austefix (Australie) </remarks>
		public static bool Parsing_SAB_AustralianNorm
		{
			get { return bool.Parse(GetValue("Parsing_SAB_AustralianNorm", false.ToString())); }
			set { SetValue("Parsing_SAB_AustralianNorm", value.ToString()); }
		}

		/// <summary>
		/// SAB BS1 Prasarna GS1
		/// </summary>
		/// <remarks>Reseno pro fy SAB - dodavatel austefix (Australie) </remarks>
		public static bool Parsing_SAB_GS1_Zavorky
		{
			get { return bool.Parse(GetValue("Parsing_SAB_GS1_Zavorky", false.ToString())); }
			set { SetValue("Parsing_SAB_GS1_Zavorky", value.ToString()); }
		}

		/// <summary>
		/// SAB_ GS1 Balton
		/// </summary>
		/// <remarks>Reseno pro fy SAB - BALTON </remarks>
		public static bool Parsing_SAB_GS1_BALTON
		{
			get { return bool.Parse(GetValue("Parsing_SAB_GS1_BALTON", false.ToString())); }
			set { SetValue("Parsing_SAB_GS1_BALTON", value.ToString()); }
		}

		/// <summary>
		/// SAB_ GS1 Beldico
		/// </summary>
		/// <remarks>Reseno pro fy SAB - Beldico </remarks>
		public static bool Parsing_SAB_GS1_BELDICO
		{
			get { return bool.Parse(GetValue("Parsing_SAB_GS1_BELDICO", false.ToString())); }
			set { SetValue("Parsing_SAB_GS1_BELDICO", value.ToString()); }
		}
		

        /// <summary>
        /// BarcodeSlashSarze
        /// </summary>
        /// <remarks>Reseno pro fy Labara</remarks>
        public static bool Parsing_BarcodeSlashSarze
        {
            get { return bool.Parse(GetValue("Parsing_BarcodeSlashSarze", false.ToString())); }
            set { SetValue("Parsing_BarcodeSlashSarze", value.ToString()); }
        }

        /// <summary>
        /// FenixBarcodeObal
        /// </summary>
        /// <remarks>Reseno pro fy FENIX</remarks>
        public static bool Parsing_FenixBarcodeObal
        {
            get { return bool.Parse(GetValue("Parsing_FenixBarcodeObal", false.ToString())); }
            set { SetValue("Parsing_FenixBarcodeObal", value.ToString()); }
        }

        /// <summary>
        /// FenixHIBC
        /// </summary>
        /// <remarks>Reseno pro fy FENIX</remarks>
        public static bool Parsing_HIBC
        {
            get { return bool.Parse(GetValue("Parsing_HIBC", false.ToString())); }
            set { SetValue("Parsing_HIBC", value.ToString()); }
        }

        /// <summary>
        /// FenixGS1
        /// </summary>
        /// <remarks>Reseno pro fy FENIX</remarks>
        public static bool Parsing_GS1
        {
            get { return bool.Parse(GetValue("Parsing_GS1", false.ToString())); }
            set { SetValue("Parsing_GS1", value.ToString()); }
        }

		/// <summary>
		/// InnovaMedical_GS1_Datum
		/// </summary>
		/// <remarks>Reseno pro fy InnovaMedical</remarks>
		public static bool Parsing_InnovaMedical_GS1_Datum
		{
			get { return bool.Parse(GetValue("Parsing_InnovaMedical_GS1_Datum", false.ToString())); }
			set { SetValue("Parsing_InnovaMedical_GS1_Datum", value.ToString()); }
		}

        #endregion


        public static Components.KeyboardManager.KeyboardType KeyboardType
        {
            get
            {
                try
                {
                    return (Components.KeyboardManager.KeyboardType)Enum.Parse(typeof(Components.KeyboardManager.KeyboardType), GetValue("KeyboardType", Components.KeyboardManager.KeyboardType.Unknown.ToString()), true);
                }
                catch //(Exception ex)
                {
                    return Fask.MST_W.Components.KeyboardManager.KeyboardType.Unknown;
                }
            }
            set { SetValue("KeyboardType", value.ToString()); }
        }

        /// <summary>
        /// Zpusob vyhledavani polozky pri vyberu.
        /// </summary>
        public static Prodej_3.ProdejVyberMaterialuList.HledaniEnum ProdejVyberMaterialuListTypHledani
        {
            get
            {
                try
                {
                    return (Prodej_3.ProdejVyberMaterialuList.HledaniEnum)Enum.Parse(typeof(Prodej_3.ProdejVyberMaterialuList.HledaniEnum), GetValue("ProdejVyberMaterialuListTypHledani", Prodej_3.ProdejVyberMaterialuList.HledaniEnum.SERLTNUM.ToString()), true);
                }
                catch //(Exception ex)
                {
                    return Prodej_3.ProdejVyberMaterialuList.HledaniEnum.SERLTNUM;
                }
            }
            set { SetValue("ProdejVyberMaterialuListTypHledani", value.ToString()); }
        }

        private static string adminPwdKey = "pwdAdmin951";
        public static string AdminPwd
        {
            get { 
                string pwd = GetValue("AdminPwd", null);
                if (string.IsNullOrEmpty(pwd))
                    return "159";
                else
                    return Fask.Encryption.RijndaelWrapper.Decrypt(pwd, adminPwdKey); 
            }
            set { SetValue("AdminPwd", Fask.Encryption.RijndaelWrapper.Encrypt(value.ToString(), adminPwdKey)); }
        }

        public static int VlozTypyPaletFormDefault
        {
            get { return int.Parse(GetValue("VlozTypyPaletFormDefault", "0")); }
            set { SetValue("VlozTypyPaletFormDefault", value.ToString()); }
        }


        public static string PrijemPrijmovaLokaceDefault
        {
            get { return GetValue("PrijemPrijmovaLokaceDefault", string.Empty); }
            set { SetValue("PrijemPrijmovaLokaceDefault", value.ToString()); }
        }

        public static string PrijemSoundSklad
        {
            get { return GetValue("PrijemSoundSklad", string.Empty); }
            set { SetValue("PrijemSoundSklad", value.ToString()); }
        }

        public static string PrijemSoundExpedice
        {
            get { return GetValue("PrijemSoundExpedice", string.Empty); }
            set { SetValue("PrijemSoundExpedice", value.ToString()); }
        }

        public static string PrijemSoundSkladExpedice
        {
            get { return GetValue("PrijemSoundSkladExpedice", string.Empty); }
            set { SetValue("PrijemSoundSkladExpedice", value.ToString()); }
        }

		public static string PrijemSoundUspesneVlozeni
		{
			get { return GetValue("PrijemSoundUspesneVlozeni", "UspesneNacteni.wav"); }
			set { SetValue("PrijemSoundUspesneVlozeni", value.ToString()); }
		}

        public static bool PrijemNerealizovanePrijekyDVNZ
        {
            get { return bool.Parse(GetValue("PrijemNerealizovanePrijekyDVNZ", Boolean.TrueString)); }
            set { SetValue("PrijemNerealizovanePrijekyDVNZ", value.ToString()); }
        }

        public static bool PrijemZalokovaniListFiltrVse
        {
            get { return bool.Parse(GetValue("PrijemZalokovaniListFiltrVse", "false")); }
            set { SetValue("PrijemZalokovaniListFiltrVse", value.ToString()); }
        }


        public static bool ServisListZdrojeStavFiltrVse
        {
            get { return bool.Parse(GetValue("ServisListZdrojeStavFiltrVse", "false")); }
            set { SetValue("ServisListZdrojeStavFiltrVse", value.ToString()); }
        }

        public static Ukolovani_1.UkolovaniMain.FiltrEnum UkolovaniFiltr
        {
            get { return (Ukolovani_1.UkolovaniMain.FiltrEnum)Ukolovani_1.UkolovaniMain.FiltrEnum.Parse(typeof(Ukolovani_1.UkolovaniMain.FiltrEnum), GetValue("UkolovaniFiltr", Ukolovani_1.UkolovaniMain.FiltrEnum.Aktivni.ToString()), true); }
            set { SetValue("UkolovaniFiltr", value.ToString()); }
        }

        public static int VyberPracovnikaRowsCount
        {
            get { return int.Parse(GetValue("VyberPracovnikaRowsCount", "30")); }
            set { SetValue("VyberPracovnikaRowsCount", value.ToString()); }
        }          


        public static string VyberPracovnikaLastSort
        {
            get { return GetValue("VyberPracovnikaLastSort", string.Empty); }
            set { SetValue("VyberPracovnikaLastSort", value); }
        }        

        public static MST_W.Vydej_3.Nastaveni.ListPolozekStatusBar.VydejStatusBarMnozstvi VydejStatusBarPole1Value
        {
            get { return (MST_W.Vydej_3.Nastaveni.ListPolozekStatusBar.VydejStatusBarMnozstvi)Enum.Parse(typeof(MST_W.Vydej_3.Nastaveni.ListPolozekStatusBar.VydejStatusBarMnozstvi), GetValue("VydejStatusBarPole1Value", MST_W.Vydej_3.Nastaveni.ListPolozekStatusBar.VydejStatusBarMnozstvi.Prazdne.ToString()), true); }
            set { SetValue("VydejStatusBarPole1Value", value.ToString()); }
        }
        public static MST_W.Vydej_3.Nastaveni.ListPolozekStatusBar.VydejStatusBarMnozstvi VydejStatusBarPole2Value
        {
            get { return (MST_W.Vydej_3.Nastaveni.ListPolozekStatusBar.VydejStatusBarMnozstvi)Enum.Parse(typeof(MST_W.Vydej_3.Nastaveni.ListPolozekStatusBar.VydejStatusBarMnozstvi), GetValue("VydejStatusBarPole2Value", MST_W.Vydej_3.Nastaveni.ListPolozekStatusBar.VydejStatusBarMnozstvi.Prazdne.ToString()), true); }
            set { SetValue("VydejStatusBarPole2Value", value.ToString()); }
        }
        public static MST_W.Vydej_3.Nastaveni.ListPolozekStatusBar.VydejStatusBarMnozstvi VydejStatusBarPole3Value
        {
            get { return (MST_W.Vydej_3.Nastaveni.ListPolozekStatusBar.VydejStatusBarMnozstvi)Enum.Parse(typeof(MST_W.Vydej_3.Nastaveni.ListPolozekStatusBar.VydejStatusBarMnozstvi), GetValue("VydejStatusBarPole3Value", MST_W.Vydej_3.Nastaveni.ListPolozekStatusBar.VydejStatusBarMnozstvi.Prazdne.ToString()), true); }
            set { SetValue("VydejStatusBarPole3Value", value.ToString()); }
        }

        public static MST_W.Vydej_3.Nastaveni.ListPolozekStatusBar.VydejStatusBarMnozstvi VydejStatusBarPole4Value

        {
            get { return (MST_W.Vydej_3.Nastaveni.ListPolozekStatusBar.VydejStatusBarMnozstvi)Enum.Parse(typeof(MST_W.Vydej_3.Nastaveni.ListPolozekStatusBar.VydejStatusBarMnozstvi), GetValue("VydejStatusBarPole4Value", MST_W.Vydej_3.Nastaveni.ListPolozekStatusBar.VydejStatusBarMnozstvi.Prazdne.ToString()), true); }
            set { SetValue("VydejStatusBarPole4Value", value.ToString()); }
        }

      public static Prijem_4.PrijemList.AktivniFiltrZobrazeni PrijemZobrazeniFiltrVseNeuplne2
        {
            get { return (Prijem_4.PrijemList.AktivniFiltrZobrazeni)Prijem_4.PrijemList.AktivniFiltrZobrazeni.Parse(typeof(Prijem_4.PrijemList.AktivniFiltrZobrazeni), GetValue("PrijemZobrazeniAktivniFiltr", Prijem_4.PrijemList.AktivniFiltrZobrazeni.Vse.ToString()), true); }
            set { SetValue("PrijemZobrazeniAktivniFiltr", value.ToString()); }
        }

        public static string VydejStatusBarPole1Prefix
        {
            get { return GetValue("VydejStatusBarPole1Prefix", string.Empty); }
            set { SetValue("VydejStatusBarPole1Prefix", value); }
        }


        public static string VydejStatusBarPole2Prefix
        {
            get { return GetValue("VydejStatusBarPole2Prefix", string.Empty); }
            set { SetValue("VydejStatusBarPole2Prefix", value); }
        }


        public static string VydejStatusBarPole3Prefix
        {
            get { return GetValue("VydejStatusBarPole3Prefix", string.Empty); }
            set { SetValue("VydejStatusBarPole3Prefix", value); }
        }


        public static string VydejStatusBarPole4Prefix
        {
            get { return GetValue("VydejStatusBarPole4Prefix", string.Empty); }
            set { SetValue("VydejStatusBarPole4Prefix", value); }
        }

        public static string VydejStatusBarPole1Postfix
        {
            get { return GetValue("VydejStatusBarPole1Postfix", string.Empty); }
            set { SetValue("VydejStatusBarPole1Postfix", value); }
        }

        public static string VydejStatusBarPole2Postfix
        {
            get { return GetValue("VydejStatusBarPole2Postfix", string.Empty); }
            set { SetValue("VydejStatusBarPole2Postfix", value); }
        }


        public static string VydejStatusBarPole3Postfix
        {
            get { return GetValue("VydejStatusBarPole3Postfix", string.Empty); }
            set { SetValue("VydejStatusBarPole3Postfix", value); }
        }


        public static string VydejStatusBarPole4Postfix
        {
            get { return GetValue("VydejStatusBarPole4Postfix", string.Empty); }
            set { SetValue("VydejStatusBarPole4Postfix", value); }
        }

        public static bool VydejStatusBarPole1Allow
        {
            get { return bool.Parse(GetValue("VydejStatusBarPole1Allow", true.ToString())); }
            set { SetValue("VydejStatusBarPole1Allow", value.ToString()); }
        }

        public static bool VydejStatusBarPole2Allow
        {
            get { return bool.Parse(GetValue("VydejStatusBarPole2Allow", true.ToString())); }
            set { SetValue("VydejStatusBarPole2Allow", value.ToString()); }
        }

        public static bool VydejStatusBarPole3Allow
        {
            get { return bool.Parse(GetValue("VydejStatusBarPole3Allow", true.ToString())); }
            set { SetValue("VydejStatusBarPole3Allow", value.ToString()); }
        }

        public static bool VydejStatusBarPole4Allow
        {
            get { return bool.Parse(GetValue("VydejStatusBarPole4Allow", true.ToString())); }
            set { SetValue("VydejStatusBarPole4Allow", value.ToString()); }
        }

        public static string Online_BYZNYS_ZboziHodnotaLast
        {
            get { return GetValue("Online_BYZNYS_ZboziHodnotaLast", string.Empty); }
            set { SetValue("Online_BYZNYS_ZboziHodnotaLast", value); }
        }

        public static string Online_BYZNYS_ZboziVolbaLast
        {
            get { return GetValue("Online_BYZNYS_ZboziVolbaLast", string.Empty); }
            set { SetValue("Online_BYZNYS_ZboziVolbaLast", value); }
        }

        
        public static string Online_BYZNYS_PartnerHodnotaLast
        {
            get { return GetValue("Online_BYZNYS_PartnerHodnotaLast", string.Empty); }
            set { SetValue("Online_BYZNYS_PartnerHodnotaLast", value); }
        }       

        public static string Online_BYZNYS_PartnerVolbaLast
        {
            get { return GetValue("Online_BYZNYS_PartnerVolbaLast", string.Empty); }
            set { SetValue("Online_BYZNYS_PartnerVolbaLast", value); }
        }
        
        public static int Online_BYZNYS_CommandTimeout
        {
            get { return int.Parse(GetValue("Online_BYZNYS_CommandTimeout", "10")); }
            set { SetValue("Online_BYZNYS_CommandTimeout", value.ToString()); }
        }


        public static bool Online_BYZNYS_VyberPartneraKlicDefault_Povolit
        {
            get { return bool.Parse(GetValue("Online_BYZNYS_VyberPartneraKlicDefault_Povolit", false.ToString())); }
            set { SetValue("Online_BYZNYS_VyberPartneraKlicDefault_Povolit", value.ToString()); }
        }

        public static int Online_BYZNYS_VyberPartneraKlicDefault
        {
            get { return int.Parse(GetValue("Online_BYZNYS_VyberPartneraKlicDefault", 0.ToString())); }
            set { SetValue("Online_BYZNYS_VyberPartneraKlicDefault", value.ToString()); }
        }
                
        private const string Online_BYZNYS_ConncetionString_Security_Password = "OPkjhdU&%30j387&Y";
        public static string Online_BYZNYS_ConnectionString
        {
            get
            {
                string connectinstring = GetValue("Online_BYZNYS_ConnectionString", @"Data Source=FASKCZ-CV002\SQLEXPRESS;Initial Catalog=BYZNYS_BWSCANNER;user id=sa;password=sa");
                try { connectinstring = Fask.Encryption.RijndaelWrapper.Decrypt(connectinstring, Online_BYZNYS_ConncetionString_Security_Password); }
                catch { }
                return connectinstring;
            }
            set
            {
                SetValue("Online_BYZNYS_ConnectionString", Fask.Encryption.RijndaelWrapper.Encrypt(value, Online_BYZNYS_ConncetionString_Security_Password));
            }
        }

        public static bool Online_BYZNYS
        {
            get { return bool.Parse(GetValue("Online_BYZNYS", false.ToString())); }
            set { SetValue("Online_BYZNYS", value.ToString()); }
        }
                
        public static bool SystemTimeUpdate
        {
            get { return bool.Parse(GetValue("SystemTimeUpdate", true.ToString())); }
            set { SetValue("SystemTimeUpdate", value.ToString()); }
        }
                
        public static string UserLogin
        {
            get { return GetValue("UserLogin", string.Empty); }
            set { SetValue("UserLogin", value); }
        }
        
        /// <summary>
        /// Obecny format desetinnych cisel, ktere se budou zobrazovat v aplikaci
        /// </summary>
        public static string UIFormatDesCisel
        {
            get { return GetValue("UIFormatDesCisel", "N"); }
            set { SetValue("UIFormatDesCisel", value); }
        }

        public static bool UIMultistartTest
        {
            get { return bool.Parse(GetValue("UIMultistartTest", true.ToString())); }
            set { SetValue("UIMultistartTest", value.ToString()); }
        }

        public static bool UIHideWindowText
        {
            get { return bool.Parse(GetValue("UIHideWindowText", false.ToString())); }
            set { SetValue("UIHideWindowText", value.ToString()); }
        }

        public static bool UIHideTaskBar
        {
            get { return bool.Parse(GetValue("UIHideTaskBar", false.ToString())); }
            set { SetValue("UIHideTaskBar", value.ToString()); }
        }

        /// <summary>
        /// Velikost fontu pro grid
        /// </summary>
        public static int UIGridFont
        {
            // TODO : doplnit do konfigurace aplikace ... viz. rf-scanner
            get { return int.Parse(GetValue("UIGridFont", "10")); }
            set { SetValue("UIGridFont", value.ToString()); }
        }

		#region UI automatika


		public static bool uia_enable
		{
			get { return bool.Parse(GetValue("uia_enable", false.ToString())); }
			set { SetValue("uia_enable", value.ToString()); }
		}

		public static bool uia_Expedice
		{
			get { return bool.Parse(GetValue("uia_Expedice", false.ToString())); }
			set { SetValue("uia_Expedice", value.ToString()); }
		}
		public static bool uia_inventura1
		{
			get { return bool.Parse(GetValue("uia_inventura1", false.ToString())); }
			set { SetValue("uia_inventura1", value.ToString()); }
		}
		public static bool uia_inventura2
		{
			get { return bool.Parse(GetValue("uia_inventura2", false.ToString())); }
			set { SetValue("uia_inventura2", value.ToString()); }
		}
		public static bool uia_prijem
		{
			get { return bool.Parse(GetValue("uia_prijem", false.ToString())); }
			set { SetValue("uia_prijem", value.ToString()); }
		}


		public static bool uia_Servis
		{
			get { return bool.Parse(GetValue("uia_Servis", false.ToString())); }
			set { SetValue("uia_Servis", value.ToString()); }
		}

		public static bool uia_prodej
		{
			get { return bool.Parse(GetValue("uia_prodej", false.ToString())); }
			set { SetValue("uia_prodej", value.ToString()); }
		}
		public static bool uia_udalosti
		{
			get { return bool.Parse(GetValue("uia_udalosti", false.ToString())); }
			set { SetValue("uia_udalosti", value.ToString()); }
		}
		public static bool uia_ukoly
		{
			get { return bool.Parse(GetValue("uia_ukoly", false.ToString())); }
			set { SetValue("uia_ukoly", value.ToString()); }
		}
		public static bool uia_vydej
		{
			get { return bool.Parse(GetValue("uia_vydej", false.ToString())); }
			set { SetValue("uia_vydej", value.ToString()); }
		}

		public static bool uia_prodej_VybratTD
		{
			get { return bool.Parse(GetValue("uia_prodej_VybratTD", false.ToString())); }
			set { SetValue("uia_prodej_VybratTD", value.ToString()); }
		}

		public static bool uia_prodej_VybratJeden
		{
			get { return bool.Parse(GetValue("uia_prodej_VybratJeden", false.ToString())); }
			set { SetValue("uia_prodej_VybratJeden", value.ToString()); }
		}

		

		public static bool uia_prodej_novaDavka
		{
			get { return bool.Parse(GetValue("uia_prodej_novaDavka", false.ToString())); }
			set { SetValue("uia_prodej_novaDavka", value.ToString()); }
		}
		public static string uia_prodej_docid2TD
		{
			get { return GetValue("uia_prodej_docid2TD", false.ToString()); }
			set { SetValue("uia_prodej_docid2TD", value.ToString()); }
		}
		public static string uia_prodej_docidTD
		{
			get { return GetValue("uia_prodej_docidTD", false.ToString()); }
			set { SetValue("uia_prodej_docidTD", value.ToString()); }
		}


		#endregion


        
        public static string PoradiSloupcuVydej3ListPolozek3
        {
            get { return GetValue("PoradiSloupcuVydej3ListPolozek3", string.Empty); }
            set { SetValue("PoradiSloupcuVydej3ListPolozek3", value); }
        }
        
        public static int FormLokaceVyberIDWidth
        {
            get { return int.Parse(GetValue("FormLokaceVyberIDWidth", "70")); }
            set { SetValue("FormLokaceVyberIDWidth", value.ToString()); }
        }

        public static int FormLokaceVyberNazevWidth
        {
            get { return int.Parse(GetValue("FormLokaceVyberNazevWidth", "100")); }
            set { SetValue("FormLokaceVyberNazevWidth", value.ToString()); }
        }

        public static int FormLokaceVyberBarcodeWidth
        {
            get { return int.Parse(GetValue("FormLokaceVyberBarcodeWidth", "60")); }
            set { SetValue("FormLokaceVyberBarcodeWidth", value.ToString()); }
        }

        public static int FormLokaceVyberTypWidth
        {
            get { return int.Parse(GetValue("FormLokaceVyberTypWidth", "30")); }
            set { SetValue("FormLokaceVyberTypWidth", value.ToString()); }
        }

        public static int FormLokaceVyberDexrowidWidth
        {
            get { return int.Parse(GetValue("FormLokaceVyberDexrowidWidth", "30")); }
            set { SetValue("FormLokaceVyberDexrowidWidth", value.ToString()); }
        }

        public static int FormLokaceVyberRowHeigth
        {
            get { return int.Parse(GetValue("FormLokaceVyberRowHeigth", "25")); }
            set { SetValue("FormLokaceVyberRowHeigth", value.ToString()); }
        }
        
        public static string PoradiSloupcuFormLokaceVyber
        {
            get { return GetValue("PoradiSloupcuFormLokaceVyber", string.Empty); }
            set { SetValue("PoradiSloupcuFormLokaceVyber", value); }
        }

        public static string PoradiSloupcuInventura1ListPolozky
        {
            get { return GetValue("PoradiSloupcuInventura1ListPolozky", string.Empty); }
            set { SetValue("PoradiSloupcuInventura1ListPolozky", value); }
        }
        
        public static int Inventura1NasnimanoRowHeigth
        {
            get { return int.Parse(GetValue("Inventura1NasnimanoRowHeigth", "25")); }
            set { SetValue("Inventura1NasnimanoRowHeigth", value.ToString()); }
        }
        
        public static int Inventura1NasnimaneSkladIDWidth
        {
            get { return int.Parse(GetValue("Inventura1NasnimaneSkladIDWidth", "40")); }
            set { SetValue("Inventura1NasnimaneSkladIDWidth", value.ToString()); }
        }

        public static int Inventura1NasnimaneSkladDescWidth
        {
            get { return int.Parse(GetValue("Inventura1NasnimaneSkladDescWidth", "40")); }
            set { SetValue("Inventura1NasnimaneSkladDescWidth", value.ToString()); }
        }

        public static int Inventura1NasnimaneITEMDESCWidth
        {
            get { return int.Parse(GetValue("Inventura1NasnimaneITEMDESCWidth", "40")); }
            set { SetValue("Inventura1NasnimaneITEMDESCWidth", value.ToString()); }
        }

        public static int Inventura1NasnimaneGUIDWidth
        {
            get { return int.Parse(GetValue("Inventura1NasnimaneGUIDWidth", "40")); }
            set { SetValue("Inventura1NasnimaneGUIDWidth", value.ToString()); }
        }

        public static int Inventura1NasnimaneDEXROWIDWidth
        {
            get { return int.Parse(GetValue("Inventura1NasnimaneDEXROWIDWidth", "40")); }
            set { SetValue("Inventura1NasnimaneDEXROWIDWidth", value.ToString()); }
        }

        public static int Inventura1NasnimaneUSERIDWidth
        {
            get { return int.Parse(GetValue("Inventura1NasnimaneUSERIDWidth", "40")); }
            set { SetValue("Inventura1NasnimaneUSERIDWidth", value.ToString()); }
        }

        public static int Inventura1NasnimaneTIMEDONEWidth
        {
            get { return int.Parse(GetValue("Inventura1NasnimaneTIMEDONEWidth", "40")); }
            set { SetValue("Inventura1NasnimaneTIMEDONEWidth", value.ToString()); }
        }
        
        public static int Inventura1NasnimaneDATEDONEWidth
        {
            get { return int.Parse(GetValue("Inventura1NasnimaneDATEDONEWidth", "40")); }
            set { SetValue("Inventura1NasnimaneDATEDONEWidth", value.ToString()); }
        }
        
        public static int Inventura1NasnimaneSERLNMBRWidth
        {
            get { return int.Parse(GetValue("Inventura1NasnimaneSERLNMBRWidth", "40")); }
            set { SetValue("Inventura1NasnimaneSERLNMBRWidth", value.ToString()); }
        }
        
        public static int Inventura1NasnimaneQTYPACKWidth
        {
            get { return int.Parse(GetValue("Inventura1NasnimaneQTYPACKWidth", "40")); }
            set { SetValue("Inventura1NasnimaneQTYPACKWidth", value.ToString()); }
        }
        
        public static int Inventura1NasnimaneQUANTITYWidth
        {
            get { return int.Parse(GetValue("Inventura1NasnimaneQUANTITYWidth", "40")); }
            set { SetValue("Inventura1NasnimaneQUANTITYWidth", value.ToString()); }
        }

        public static int Inventura1NasnimaneQUANTITYMJWidth
        {
            get { return int.Parse(GetValue("Inventura1NasnimaneQUANTITYMJWidth", "40")); }
            set { SetValue("Inventura1NasnimaneQUANTITYMJWidth", value.ToString()); }
        }

        public static int Inventura1NasnimaneMJWidth
        {
            get { return int.Parse(GetValue("Inventura1NasnimaneMJWidth", "40")); }
            set { SetValue("Inventura1NasnimaneMJWidth", value.ToString()); }
        }

        public static int Inventura1NasnimaneExpiraceWidth
        {
            get { return int.Parse(GetValue("Inventura1NasnimaneExpiraceWidth", "40")); }
            set { SetValue("Inventura1NasnimaneExpiraceWidth", value.ToString()); }
        }

        public static int Inventura1NasnimaneVNDITNUMWidth
        {
            get { return int.Parse(GetValue("Inventura1NasnimaneVNDITNUMWidth", "40")); }
            set { SetValue("Inventura1NasnimaneVNDITNUMWidth", value.ToString()); }
        }


        public static int Inventura1NasnimaneLOCNCODEWidth
        {
            get { return int.Parse(GetValue("Inventura1NasnimaneLOCNCODEWidth", "40")); }
            set { SetValue("Inventura1NasnimaneLOCNCODEWidth", value.ToString()); }
        }
        
        public static int Inventura1NasnimaneCZCARKODWidth
        {
            get { return int.Parse(GetValue("Inventura1NasnimaneCZCARKODWidth", "40")); }
            set { SetValue("Inventura1NasnimaneCZCARKODWidth", value.ToString()); }
        }
        
        public static int Inventura1NasnimaneITEMNMBRWidth
        {
            get { return int.Parse(GetValue("Inventura1NasnimaneITEMNMBRWidth", "40")); }
            set { SetValue("Inventura1NasnimaneITEMNMBRWidth", value.ToString()); }
        }


        public static int Inventura1NasnimaneCOUNTENTRIESWidth
        {
            get { return int.Parse(GetValue("Inventura1NasnimaneCOUNTENTRIESWidth", "40")); }
            set { SetValue("Inventura1NasnimaneCOUNTENTRIESWidth", value.ToString()); }
        }


        public static string PoradiSloupcuInventura1Nasnimane
        {
            get { return GetValue("PoradiSloupcuInventura1Nasnimane", string.Empty); }
            set { SetValue("PoradiSloupcuInventura1Nasnimane", value); }
        }


        public static Inventura1_sqlce.Nasnimane2.SetrizeniType Inventura1NasnimaneSetriditV
        {
            get { return (Inventura1_sqlce.Nasnimane2.SetrizeniType)Enum.Parse(typeof(Inventura1_sqlce.Nasnimane2.SetrizeniType), GetValue("Inventura1NasnimaneSetriditV", Inventura1_sqlce.Nasnimane2.SetrizeniType.ASC.ToString()), true); }
            set { SetValue("Inventura1NasnimaneSetriditV", value.ToString()); }
        }	

        public static Inventura1_sqlce.Nasnimane2.SetrizeniType Inventura1NasnimaneSetriditH
        {
            get { return (Inventura1_sqlce.Nasnimane2.SetrizeniType)Enum.Parse(typeof(Inventura1_sqlce.Nasnimane2.SetrizeniType), GetValue("Inventura1NasnimaneSetriditH", Inventura1_sqlce.Nasnimane2.SetrizeniType.ASC.ToString()), true); }
            set { SetValue("Inventura1NasnimaneSetriditH", value.ToString()); }
        }	

        public static Inventura1_sqlce.Nasnimane2.RazeniType Inventura1NasnimaneRazeniV
        {
            get { return (Inventura1_sqlce.Nasnimane2.RazeniType)Enum.Parse(typeof(Inventura1_sqlce.Nasnimane2.RazeniType), GetValue("Inventura1NasnimaneRazeniV", Inventura1_sqlce.Nasnimane2.RazeniType.None.ToString()), true); }
            set { SetValue("Inventura1NasnimaneRazeniV", value.ToString()); }
        }	

        public static Inventura1_sqlce.Nasnimane2.RazeniType Inventura1NasnimaneRazeniH
        {
            get { return (Inventura1_sqlce.Nasnimane2.RazeniType)Enum.Parse(typeof(Inventura1_sqlce.Nasnimane2.RazeniType), GetValue("Inventura1NasnimaneRazeniH", Inventura1_sqlce.Nasnimane2.RazeniType.None.ToString()), true); }
            set { SetValue("Inventura1NasnimaneRazeniH", value.ToString()); }
        }


        public static string PrijemRez1LastValue
        {
            get { return GetValue("PrijemRez1LastValue", string.Empty); }
            set { SetValue("PrijemRez1LastValue", value); }
        }
        
        public static string PrijemRez2LastValue
        {
            get { return GetValue("PrijemRez2LastValue", string.Empty); }
            set { SetValue("PrijemRez2LastValue", value); }
        }

        public static string VydejRez1LastValue
        {
            get { return GetValue("VydejRez1LastValue", string.Empty); }
            set { SetValue("VydejRez1LastValue", value); }
        }

        public static string VydejRez2LastValue
        {
            get { return GetValue("VydejRez2LastValue", string.Empty); }
            set { SetValue("VydejRez2LastValue", value); }
        }


        public static bool PrijemZobrazeniFiltrVseNeuplne
        {
            get { return bool.Parse(GetValue("PrijemZobrazeniFiltrVseNeuplne", false.ToString())); }
            set { SetValue("PrijemZobrazeniFiltrVseNeuplne", value.ToString()); }
        }

        //Inventura2


        public static int Inventura2ZmenaStrediskoNazevWidth
        {
            get { return int.Parse(GetValue("Inventura2ZmenaStrediskoNazevWidth", "100")); }
            set { SetValue("Inventura2ZmenaStrediskoNazevWidth", value.ToString()); }
        }

        public static int Inventura2ZmenaStrediskoStrediskoWidth
        {
            get { return int.Parse(GetValue("Inventura2ZmenaStrediskoStrediskoWidth", "30")); }
            set { SetValue("Inventura2ZmenaStrediskoStrediskoWidth", value.ToString()); }
        }

        public static int Inventura2ZmenaOsobyJmenoWidth
        {
            get { return int.Parse(GetValue("Inventura2ZmenaOsobyJmenoWidth", "50")); }
            set { SetValue("Inventura2ZmenaOsobyJmenoWidth", value.ToString()); }
        }

        public static int Inventura2ZmenaOsobyPrijmeniWidth
        {
            get { return int.Parse(GetValue("Inventura2ZmenaOsobyPrijmeniWidth", "70")); }
            set { SetValue("Inventura2ZmenaOsobyPrijmeniWidth", value.ToString()); }
        }

        public static int Inventura2ZmenaOsobyTitulWidth
        {
            get { return int.Parse(GetValue("Inventura2ZmenaOsobyTitulWidth", "40")); }
            set { SetValue("Inventura2ZmenaOsobyTitulWidth", value.ToString()); }
        }
        
        public static int Inventura2ZmenaOsobyOsobazodpWidth
        {
            get { return int.Parse(GetValue("Inventura2ZmenaOsobyOsobazodpWidth", "40")); }
            set { SetValue("Inventura2ZmenaOsobyOsobazodpWidth", value.ToString()); }
        }
        
        public static int Inventura2ZmenaKanclTextWidth
        {
            get { return int.Parse(GetValue("Inventura2ZmenaKanclTextWidth", "100")); }
            set { SetValue("Inventura2ZmenaKanclTextWidth", value.ToString()); }
        }
        public static int Inventura2ZmenaKanclNazevWidth
        {
            get { return int.Parse(GetValue("Inventura2ZmenaKanclNazevWidth", "100")); }
            set { SetValue("Inventura2ZmenaKanclNazevWidth", value.ToString()); }
        }

        public static int Inventura2ZmenaKanclEANWidth
        {
            get { return int.Parse(GetValue("Inventura2ZmenaKanclEANWidth", "100")); }
            set { SetValue("Inventura2ZmenaKanclEANWidth", value.ToString()); }
        }

        public static int Inventura2ZmenaKanclStreWidth
        {
            get { return int.Parse(GetValue("Inventura2ZmenaKanclStreWidth", "40")); }
            set { SetValue("Inventura2ZmenaKanclStreWidth", value.ToString()); }
        }

        public static int Inventura2ZmenaKanclKanclWidth
        {
            get { return int.Parse(GetValue("Inventura2ZmenaKanclKanclWidth", "40")); }
            set { SetValue("Inventura2ZmenaKanclKanclWidth", value.ToString()); }
        }
        
        public static int Inventura2ZmenaLokaceNazevWidth
        {
            get { return int.Parse(GetValue("Inventura2ZmenaLokaceNazevWidth", "100")); }
            set { SetValue("Inventura2ZmenaLokaceNazevWidth", value.ToString()); }
        }

        public static int Inventura2ZmenaLokaceEanlWidth
        {
            get { return int.Parse(GetValue("Inventura2ZmenaLokaceEanlWidth", "60")); }
            set { SetValue("Inventura2ZmenaLokaceEanlWidth", value.ToString()); }
        }

        public static int Inventura2ZmenaLokaceLokace2Width
        {
            get { return int.Parse(GetValue("Inventura2ZmenaLokaceLokace2Width", "60")); }
            set { SetValue("Inventura2ZmenaLokaceLokace2Width", value.ToString()); }
        }

        public static int Inventura2ZmenaLokaceLokace1Width
        {
            get { return int.Parse(GetValue("Inventura2ZmenaLokaceLokace1Width", "60")); }
            set { SetValue("Inventura2ZmenaLokaceLokace1Width", value.ToString()); }
        }

        public static int Inventura2RowHeigth
        {
            get { return int.Parse(GetValue("Inventura2RowHeigth", "25")); }
            set { SetValue("Inventura2RowHeigth", value.ToString()); }
        }
        
        public static string PoradiSloupcuInventura2ListPolozky
        {
            get { return GetValue("PoradiSloupcuInventura2ListPolozky", string.Empty); }
            set { SetValue("PoradiSloupcuInventura2ListPolozky", value); }
        }


        public static Inventura2.Nasnimane.SetrizeniType Inventura2NSetriditV
        {
            get { return (Inventura2.Nasnimane.SetrizeniType)Enum.Parse(typeof(Inventura2.Nasnimane.SetrizeniType), GetValue("Inventura2NSetriditV", Inventura2.Nasnimane.SetrizeniType.ASC.ToString()), true); }
            set { SetValue("Inventura2NSetriditV", value.ToString()); }
        }

        public static Inventura2.Nasnimane.SetrizeniType Inventura2NSetriditH
        {
            get { return (Inventura2.Nasnimane.SetrizeniType)Enum.Parse(typeof(Inventura2.Nasnimane.SetrizeniType), GetValue("Inventura2NSetriditH", Inventura2.Nasnimane.SetrizeniType.ASC.ToString()), true); }
            set { SetValue("Inventura2NSetriditH", value.ToString()); }
        }
        
        public static Inventura2.Nasnimane.RazeniType Inventura2NRazeniV
        {
            get { return (Inventura2.Nasnimane.RazeniType)Enum.Parse(typeof(Inventura2.Nasnimane.RazeniType), GetValue("Inventura2NRazeniV", Inventura2.Nasnimane.RazeniType.None.ToString()), true); }
            set { SetValue("Inventura2NRazeniV", value.ToString()); }
        }

        public static Inventura2.Nasnimane.RazeniType Inventura2NRazeniH
        {
            get { return (Inventura2.Nasnimane.RazeniType)Enum.Parse(typeof(Inventura2.Nasnimane.RazeniType), GetValue("Inventura2NRazeniH", Inventura2.Nasnimane.RazeniType.None.ToString()), true); }
            set { SetValue("Inventura2NRazeniH", value.ToString()); }
        }

        
        public static int Inventura2NRowHeigth
        {
            get { return int.Parse(GetValue("Inventura2NRowHeigth", "25")); }
            set { SetValue("Inventura2NRowHeigth", value.ToString()); }
        }

        public static string PoradiSloupcuInventura2NListPolozky
        {
            get { return GetValue("PoradiSloupcuInventura2NListPolozky", string.Empty); }
            set { SetValue("PoradiSloupcuInventura2NListPolozky", value); }
        }

        public static int Inventura2NID_MAJETEKWidth
        {
            get { return int.Parse(GetValue("Inventura2NID_MAJETEKWidth", "30")); }
            set { SetValue("Inventura2NID_MAJETEKWidth", value.ToString()); }
        }

        public static int Inventura2NCasZprWidth
        {
            get { return int.Parse(GetValue("Inventura2NCasZprWidth", "40")); }
            set { SetValue("Inventura2NCasZprWidth", value.ToString()); }
        }

        public static int Inventura2NOsZprWidth
        {
            get { return int.Parse(GetValue("Inventura2NOsZprWidth", "30")); }
            set { SetValue("Inventura2NOsZprWidth", value.ToString()); }
        }

        public static int Inventura2NKlicLokWidth
        {
            get { return int.Parse(GetValue("Inventura2NKlicLokWidth", "30")); }
            set { SetValue("Inventura2NKlicLokWidth", value.ToString()); }
        }

        public static int Inventura2NKusuWidth
        {
            get { return int.Parse(GetValue("Inventura2NKusuWidth", "40")); }
            set { SetValue("Inventura2NKusuWidth", value.ToString()); }
        }

        public static int Inventura2NEanWidth
        {
            get { return int.Parse(GetValue("Inventura2NEanWidth", "50")); }
            set { SetValue("Inventura2NEanWidth", value.ToString()); }
        }

        public static int Inventura2NKancelarNazevWidth
        {
            get { return int.Parse(GetValue("Inventura2NKancelarNazevWidth", "60")); }
            set { SetValue("Inventura2NKancelarNazevWidth", value.ToString()); }
        }

        public static int Inventura2NKancelarWidth
        {
            get { return int.Parse(GetValue("Inventura2NKancelarWidth", "30")); }
            set { SetValue("Inventura2NKancelarWidth", value.ToString()); }
        }

        public static int Inventura2NLokaceNazevWidth
        {
            get { return int.Parse(GetValue("Inventura2NLokaceNazevWidth", "60")); }
            set { SetValue("Inventura2NLokaceNazevWidth", value.ToString()); }
        }
        
        public static int Inventura2NLokace2Width
        {
            get { return int.Parse(GetValue("Inventura2NLokace2Width", "50")); }
            set { SetValue("Inventura2NLokace2Width", value.ToString()); }
        }

        public static int Inventura2NLokace1Width
        {
            get { return int.Parse(GetValue("Inventura2NLokace1Width", "50")); }
            set { SetValue("Inventura2NLokace1Width", value.ToString()); }
        }

        public static int Inventura2NOsobaNazevWidth
        {
            get { return int.Parse(GetValue("Inventura2NOsobaNazevWidth", "60")); }
            set { SetValue("Inventura2NOsobaNazevWidth", value.ToString()); }
        }

        public static int Inventura2NOsobaWidth
        {
            get { return int.Parse(GetValue("Inventura2NOsobaWidth", "30")); }
            set { SetValue("Inventura2NOsobaWidth", value.ToString()); }
        }

        public static int Inventura2NStredNazevWidth
        {
            get { return int.Parse(GetValue("Inventura2NStredNazevWidth", "60")); }
            set { SetValue("Inventura2NStredNazevWidth", value.ToString()); }
        }

        public static int Inventura2NStredWidth
        {
            get { return int.Parse(GetValue("Inventura2NStredWidth", "30")); }
            set { SetValue("Inventura2NStredWidth", value.ToString()); }
        }

        public static int Inventura2NKategorieWidth
        {
            get { return int.Parse(GetValue("Inventura2NKategorieWidth", "30")); }
            set { SetValue("Inventura2NKategorieWidth", value.ToString()); }
        }

        public static int Inventura2NIDWidth
        {
            get { return int.Parse(GetValue("Inventura2NIDWidth", "30")); }
            set { SetValue("Inventura2NIDWidth", value.ToString()); }
        }

        public static int Inventura2NNAZEVWidth
        {
            get { return int.Parse(GetValue("Inventura2NNAZEVWidth", "100")); }
            set { SetValue("Inventura2NNAZEVWidth", value.ToString()); }
        }
        
        public static int Inventura2NI_CISLOWidth
        {
            get { return int.Parse(GetValue("Inventura2NI_CISLOWidth", "30")); }
            set { SetValue("Inventura2NI_CISLOWidth", value.ToString()); }
        }
        
        public static int Inventura2ZbyvaWidth
        {
            get { return int.Parse(GetValue("Inventura2ZbyvaWidth", "40")); }
            set { SetValue("Inventura2ZbyvaWidth", value.ToString()); }
        }

        public static int Inventura2NactenoWidth
        {
            get { return int.Parse(GetValue("Inventura2NactenoWidth", "40")); }
            set { SetValue("Inventura2NactenoWidth", value.ToString()); }
        }

        public static int Inventura2KlicLokWidth
        {
            get { return int.Parse(GetValue("Inventura2KlicLokWidth", "30")); }
            set { SetValue("Inventura2KlicLokWidth", value.ToString()); }
        }

        public static int Inventura2KusuWidth
        {
            get { return int.Parse(GetValue("Inventura2KusuWidth", "30")); }
            set { SetValue("Inventura2KusuWidth", value.ToString()); }
        }
        
        public static int Inventura2EanWidth
        {
            get { return int.Parse(GetValue("Inventura2EanWidth", "40")); }
            set { SetValue("Inventura2EanWidth", value.ToString()); }
        }
        
        public static int Inventura2KancelarNazevWidth
        {
            get { return int.Parse(GetValue("Inventura2KancelarNazevWidth", "50")); }
            set { SetValue("Inventura2KancelarNazevWidth", value.ToString()); }
        }

        public static int Inventura2KancelarPopisWidth
        {
            get { return int.Parse(GetValue("Inventura2KancelarPopisWidth", "50")); }
            set { SetValue("Inventura2KancelarPopisWidth", value.ToString()); }
        }

        public static int Inventura2KancelarWidth
        {
            get { return int.Parse(GetValue("Inventura2KancelarWidth", "30")); }
            set { SetValue("Inventura2KancelarWidth", value.ToString()); }
        }

        public static int Inventura2LokaceNazevWidth
        {
            get { return int.Parse(GetValue("Inventura2LokaceNazevWidth", "50")); }
            set { SetValue("Inventura2LokaceNazevWidth", value.ToString()); }
        }

        public static int Inventura2Lokace2Width
        {
            get { return int.Parse(GetValue("Inventura2Lokace2Width", "50")); }
            set { SetValue("Inventura2Lokace2Width", value.ToString()); }
        }

        public static int Inventura2Lokace1Width
        {
            get { return int.Parse(GetValue("Inventura2Lokace1Width", "50")); }
            set { SetValue("Inventura2Lokace1Width", value.ToString()); }
        }
        
        public static int Inventura2OsobaNazevWidth
        {
            get { return int.Parse(GetValue("Inventura2OsobaNazevWidth", "100")); }
            set { SetValue("Inventura2OsobaNazevWidth", value.ToString()); }
        }
        
        public static int Inventura2OsobaWidth
        {
            get { return int.Parse(GetValue("Inventura2OsobaWidth", "30")); }
            set { SetValue("Inventura2OsobaWidth", value.ToString()); }
        }

        public static int Inventura2StredNazevWidth
        {
            get { return int.Parse(GetValue("Inventura2StredNazevWidth", "100")); }
            set { SetValue("Inventura2StredNazevWidth", value.ToString()); }
        }
        
        public static int Inventura2StredWidth
        {
            get { return int.Parse(GetValue("Inventura2StredWidth", "30")); }
            set { SetValue("Inventura2StredWidth", value.ToString()); }
        }

        public static int Inventura2KategorieWidth
        {
            get { return int.Parse(GetValue("Inventura2KategorieWidth", "30")); }
            set { SetValue("Inventura2KategorieWidth", value.ToString()); }
        }

        public static int Inventura2IDWidth
        {
            get { return int.Parse(GetValue("Inventura2IDWidth", "30")); }
            set { SetValue("Inventura2IDWidth", value.ToString()); }
        }
        
        public static int Inventura2NAZEVWidth
        {
            get { return int.Parse(GetValue("Inventura2NAZEVWidth", "100")); }
            set { SetValue("Inventura2NAZEVWidth", value.ToString()); }
        }
        
        public static int Inventura2I_CISLOWidth
        {
            get { return int.Parse(GetValue("Inventura2I_CISLOWidth", "45")); }
            set { SetValue("Inventura2I_CISLOWidth", value.ToString()); }
        }
                
        public static int Inventura1RowHeigth
        {
            get { return int.Parse(GetValue("Inventura1RowHeigth", "25")); }
            set { SetValue("Inventura1RowHeigth", value.ToString()); }
        }

        public static string Inventura1_HledatLastText
        {
            get { return GetValue("Inventura1_HledatLastText", string.Empty); }
            set { SetValue("Inventura1_HledatLastText", value); }
        }

        public static bool Inventura1_HledatFulltext
        {
            get { return bool.Parse(GetValue("Inventura1_HledatFulltext", false.ToString())); }
            set { SetValue("Inventura1_HledatFulltext", value.ToString()); }
        }
                
        public static int Inventura1CZ_CarKodWidth
        {
            get { return int.Parse(GetValue("Inventura1CZ_CarKodWidth", "45")); }
            set { SetValue("Inventura1CZ_CarKodWidth", value.ToString()); }
        }

        public static int Inventura1CZ_SERNUM_FINDWidth
        {
            get { return int.Parse(GetValue("Inventura1CZ_SERNUM_FINDWidth", "45")); }
            set { SetValue("Inventura1CZ_SERNUM_FINDWidth", value.ToString()); }
        }
        
        public static int Inventura1CZ_SERNUM_TRACKWidth
        {
            get { return int.Parse(GetValue("Inventura1CZ_SERNUM_TRACKWidth", "45")); }
            set { SetValue("Inventura1CZ_SERNUM_TRACKWidth", value.ToString()); }
        }

        public static int Inventura1SkladIDWidth
        {
            get { return int.Parse(GetValue("Inventura1SkladIDWidth", "45")); }
            set { SetValue("Inventura1SkladIDWidth", value.ToString()); }
        }

        public static int Inventura1SkladWidth
        {
            get { return int.Parse(GetValue("Inventura1SkladWidth", "45")); }
            set { SetValue("Inventura1SkladWidth", value.ToString()); }
        }
               
        public static int Inventura1LOCNCODEWidth
        {
            get { return int.Parse(GetValue("Inventura1LOCNCODEWidth", "45")); }
            set { SetValue("Inventura1LOCNCODEWidth", value.ToString()); }
        }

        public static int Inventura1NASNIMANOWidth
        {
            get { return int.Parse(GetValue("Inventura1NASNIMANOWidth", "45")); }
            set { SetValue("Inventura1NASNIMANOWidth", value.ToString()); }
        }

        public static int Inventura1QUANTITYWidth
        {
            get { return int.Parse(GetValue("Inventura1QUANTITYWidth", "45")); }
            set { SetValue("Inventura1QUANTITYWidth", value.ToString()); }
        }
        
        public static int Inventura1ITEMDESCWidth
        {
            get { return int.Parse(GetValue("Inventura1ITEMDESCWidth", "45")); }
            set { SetValue("Inventura1ITEMDESCWidth", value.ToString()); }
        }

        public static int Inventura1ITEMCODECWidth
        {
            get { return int.Parse(GetValue("Inventura1ITEMCODECWidth", "45")); }
            set { SetValue("Inventura1ITEMCODECWidth", value.ToString()); }
        }
        
        public static int Inventura1ITEMNMBRWidth
        {
            get { return int.Parse(GetValue("Inventura1ITEMNMBRWidth", "45")); }
            set { SetValue("Inventura1ITEMNMBRWidth", value.ToString()); }
        }

        public static List<string> RemovedRFIDCodes { get; set; }

        
        //Vydej

        /// <summary>
        /// Filtr na zobrazeni jen nenulovych pozic
        /// </summary>
        public static bool VydejVyberMaterialuFiltrMnozstviNula
        {
            get { return bool.Parse(GetValue("VydejVyberMaterialuFiltrMnozstviNula", false.ToString())); }
            set { SetValue("VydejVyberMaterialuFiltrMnozstviNula", value.ToString()); }
        }

        /// <summary>
        /// Zpusob vyhledavani polozky pri vyberu.
        /// </summary>
        public static Vydej_3.VydejVyberMaterialuList.HledaniEnum VydejVyberMaterialuListTypHledani
        {
            get
            {
                try
                {
                    return (Vydej_3.VydejVyberMaterialuList.HledaniEnum)Enum.Parse(typeof(Vydej_3.VydejVyberMaterialuList.HledaniEnum), GetValue("VydejVyberMaterialuListTypHledani", Vydej_3.VydejVyberMaterialuList.HledaniEnum.SERLTNUM.ToString()), true);
                }
                catch //(Exception ex)
                {
                    return Vydej_3.VydejVyberMaterialuList.HledaniEnum.SERLTNUM;
                }
            }
            set { SetValue("VydejVyberMaterialuListTypHledani", value.ToString()); }
        }


        // 22.8.2016 JiS - zmena filtru pro zobrazeni - VICHR
        //public static bool VydejZobrazeniFiltrVseNeuplne
        //{
        //    get { return bool.Parse(GetValue("VydejZobrazeniFiltrVseNeuplne", false.ToString())); }
        //    set { SetValue("VydejZobrazeniFiltrVseNeuplne", value.ToString()); }
        //}
        public static Vydej_3.ListPolozek3.FiltrZobrazeniPolozek VydejZobrazeniFiltrVseNeuplne
        {
            get
            {
                try
                {
                    return (Vydej_3.ListPolozek3.FiltrZobrazeniPolozek)Enum.Parse(typeof(Vydej_3.ListPolozek3.FiltrZobrazeniPolozek), GetValue("VydejZobrazeniFiltrVseNeuplne", Vydej_3.ListPolozek3.FiltrZobrazeniPolozek.Vse.ToString()), true);
                }
                catch
                {
                    return Vydej_3.ListPolozek3.FiltrZobrazeniPolozek.Vse;
                }
            }
            set { SetValue("VydejZobrazeniFiltrVseNeuplne", value.ToString()); }
        }


        public static int VydejSeznamPaletPolozkaNazevColumnWidth
        {
            get { return int.Parse(GetValue("VydejSeznamPaletPolozkaNazevColumnWidth", Properties.Resources.VydejSeznamPaletPolozkaNazevColumnWidth)); }
            set { SetValue("VydejSeznamPaletPolozkaNazevColumnWidth", value.ToString()); }
        }


        public static int VydejSeznamPaletPolozkaIDWidth
        {
            get { return int.Parse(GetValue("VydejSeznamPaletPolozkaIDWidth", Properties.Resources.VydejSeznamPaletPolozkaIDWidth)); }
            set { SetValue("VydejSeznamPaletPolozkaIDWidth", value.ToString()); }
        }


        public static int VydejSeznamPaletPolozkaCisloWidth
        {
            get { return int.Parse(GetValue("VydejSeznamPaletPolozkaCisloWidth", Properties.Resources.VydejSeznamPaletPolozkaCisloWidth)); }
            set { SetValue("VydejSeznamPaletPolozkaCisloWidth", value.ToString()); }
        }


        public static int VydejSeznamPaletPolozkaPocetWidth
        {
            get { return int.Parse(GetValue("VydejSeznamPaletPolozkaPocetWidth", Properties.Resources.VydejSeznamPaletPolozkaPocetWidth)); }
            set { SetValue("VydejSeznamPaletPolozkaPocetWidth", value.ToString()); }
        }

        public static int VydejPaletListFormNasnimanoWidth
        {
            get { return int.Parse(GetValue("VydejPaletListFormNasnimanoWidth", Properties.Resources.VydejPaletListFormNasnimanoWidth)); }
            set { SetValue("VydejPaletListFormNasnimanoWidth", value.ToString()); }
        }


        public static int VydejPaletListFormPocetSkupBal
        {
            get { return int.Parse(GetValue("VydejPaletListFormPocetSkupBal", Properties.Resources.VydejPaletListFormPocetSkupBal)); }
            set { SetValue("VydejPaletListFormPocetSkupBal", value.ToString()); }
        }


        public static int VydejPaletListFomrmNadSkupbal
        {
            get { return int.Parse(GetValue("VydejPaletListFomrmNadSkupbal", Properties.Resources.VydejPaletListFomrmNadSkupbal)); }
            set { SetValue("VydejPaletListFomrmNadSkupbal", value.ToString()); }
        }



        public static int VydejPaletListFormKsSkupBal
        {
            get { return int.Parse(GetValue("VydejPaletListFormKsSkupBal", Properties.Resources.VydejPaletListFormKsSkupBal)); }
            set { SetValue("VydejPaletListFormKsSkupBal", value.ToString()); }
        }


        public static int VydejPaletListFormIDWidth
        {
            get { return int.Parse(GetValue("VydejPaletListFormIDWidth", Properties.Resources.VydejPaletListFormIDWidth)); }
            set { SetValue("VydejPaletListFormIDWidth", value.ToString()); }
        }


        public static int VydejPaletListFormSarzeWidth
        {
            get { return int.Parse(GetValue("VydejPaletListFormSarzeWidth", Properties.Resources.VydejPaletListFormSarzeWidth)); }
            set { SetValue("VydejPaletListFormSarzeWidth", value.ToString()); }
        }


        public static int VydejPaletListFormRowHeight
        {
            get { return int.Parse(GetValue("VydejPaletListFormRowHeight", Properties.Resources.VydejPaletListFormRowHeight)); }
            set { SetValue("VydejPaletListFormRowHeight", value.ToString()); }
        }

        public static int VydejSeznamPaletPolozkaRowHeight
        {
            get { return int.Parse(GetValue("VydejSeznamPaletPolozkaRowHeight", Properties.Resources.VydejSeznamPaletPolozkaRowHeight)); }
            set { SetValue("VydejSeznamPaletPolozkaRowHeight", value.ToString()); }
        }

        public static int VydejPaletySirkaRadku
        {
            get { return int.Parse(GetValue("VydejPaletySirkaRadku", Properties.Resources.VydejPaletySirkaRadku)); }
            set { SetValue("VydejPaletySirkaRadku", value.ToString()); }
        }


        public static int VydejPaletySirkaRadku2
        {
            get { return int.Parse(GetValue("VydejPaletySirkaRadku2", Properties.Resources.VydejPaletySirkaRadku2)); }
            set { SetValue("VydejPaletySirkaRadku2", value.ToString()); }
        }


        public static int VydejTypPalety
        {
            get { return int.Parse(GetValue("VydejTypPalety", Properties.Resources.VydejTypPalety)); }
            set { SetValue("VydejTypPalety", value.ToString()); }
        }
        public static int VydejCelkemPocetSkupBal
        {
            get { return int.Parse(GetValue("VydejCelkemPocetSkupBal", Properties.Resources.VydejCelkemPocetSkupBal)); }
            set { SetValue("VydejCelkemPocetSkupBal", value.ToString()); }
        }
        public static int VydejKusuSamostatne
        {
            get { return int.Parse(GetValue("VydejKusuSamostatnet", Properties.Resources.VydejKusuSamostatne)); }
            set { SetValue("VydejKusuSamostatne", value.ToString()); }
        }
        public static int VydejPocetPalet
        {
            get { return int.Parse(GetValue("VydejPocetPalet", Properties.Resources.VydejPocetPalet)); }
            set { SetValue("VydejPocetPalett", value.ToString()); }
        }
        public static int VydejProcZPalety
        {
            get { return int.Parse(GetValue("VydejProcZPalety", Properties.Resources.VydejProcZPalety)); }
            set { SetValue("VydejProcZPaletyt", value.ToString()); }
        }
        public static int VydejPocetKusuPaleta
        {
            get { return int.Parse(GetValue("VydejPocetKusuPaleta", Properties.Resources.VydejPocetKusuPaleta)); }
            set { SetValue("VydejPocetKusuPaletat", value.ToString()); }
        }

        public static int VydejPocetKusuSkupBal
        {
            get { return int.Parse(GetValue("VydejPocetKusuSkupBal", Properties.Resources.VydejPocetKusuSkupBal)); }
            set { SetValue("VydejPocetKusuSkupBal", value.ToString()); }
        }

        public static int VydejID
        {
            get { return int.Parse(GetValue("VydejID", Properties.Resources.VydejID)); }
            set { SetValue("VydejID", value.ToString()); }
        }

        public static int VydejPaletyNazevPolozky
        {
            get { return int.Parse(GetValue("VydejPaletyNazevPolozky", Properties.Resources.VydejPaletyNazevPolozky)); }
            set { SetValue("VydejPaletyNazevPolozky", value.ToString()); }
        }

        public static int VydejPaletyPocetKs
        {
            get { return int.Parse(GetValue("VydejPaletyPocetKs", Properties.Resources.VydejPaletyPocetKs)); }
            set { SetValue("VydejPaletyPocetKs", value.ToString()); }
        }

        public static int VydejPaletyTypPalety
        {
            get { return int.Parse(GetValue("VydejPaletyTypPalety", Properties.Resources.VydejPaletyTypPalety)); }
            set { SetValue("VydejPaletyTypPaletys", value.ToString()); }
        }

        public static int VydejPaletyCisloPalListku
        {
            get { return int.Parse(GetValue("VydejPaletyCisloPalListku", Properties.Resources.VydejPaletyCisloPalListku)); }
            set { SetValue("VydejPaletyCisloPalListku", value.ToString()); }
        }

        public static int VydejPaletyCisloPalListkuN
        {
            get { return int.Parse(GetValue("VydejPaletyCisloPalListkuN", Properties.Resources.VydejPaletyCisloPalListku)); }
            set { SetValue("VydejPaletyCisloPalListkuN", value.ToString()); }
        }

        public static int VydejPaletyNazevPolozky2
        {
            get { return int.Parse(GetValue("VydejPaletyNazevPolozky2", Properties.Resources.VydejPaletyNazevPolozky2)); }
            set { SetValue("VydejPaletyNazevPolozky2", value.ToString()); }
        }

        public static int VydejPaletyPocetKs2
        {
            get { return int.Parse(GetValue("VydejPaletyPocetKs2", Properties.Resources.VydejPaletyPocetKs2)); }
            set { SetValue("VydejPaletyPocetKs2", value.ToString()); }
        }

        public static int VydejPaletyTypPalety2
        {
            get { return int.Parse(GetValue("VydejPaletyTypPalety2", Properties.Resources.VydejPaletyTypPalety2)); }
            set { SetValue("VydejPaletyTypPaletys2", value.ToString()); }
        }

        public static int VydejPaletyCisloPalListku2
        {
            get { return int.Parse(GetValue("VydejPaletyCisloPalListku2", Properties.Resources.VydejPaletyCisloPalListku2)); }
            set { SetValue("VydejPaletyCisloPalListku2", value.ToString()); }
        }

        public static int VydejPaletyCisloPalListku2N
        {
            get { return int.Parse(GetValue("VydejPaletyCisloPalListku2N", Properties.Resources.VydejPaletyCisloPalListku2)); }
            set { SetValue("VydejPaletyCisloPalListku2N", value.ToString()); }
        }

        public static int VydejTypPalety2
        {
            get { return int.Parse(GetValue("VydejTypPalety2", Properties.Resources.VydejTypPalety2)); }
            set { SetValue("VydejTypPalety2", value.ToString()); }
        }
        public static int VydejCelkemPocetSkupBal2
        {
            get { return int.Parse(GetValue("VydejCelkemPocetSkupBal2", Properties.Resources.VydejCelkemPocetSkupBal2)); }
            set { SetValue("VydejCelkemPocetSkupBal2", value.ToString()); }
        }
        public static int VydejKusuSamostatne2
        {
            get { return int.Parse(GetValue("VydejKusuSamostatnet2", Properties.Resources.VydejKusuSamostatne2)); }
            set { SetValue("VydejKusuSamostatne2", value.ToString()); }
        }
        public static int VydejPocetPalet2
        {
            get { return int.Parse(GetValue("VydejPocetPalet2", Properties.Resources.VydejPocetPalet2)); }
            set { SetValue("VydejPocetPalett2", value.ToString()); }
        }
        public static int VydejProcZPalety2
        {
            get { return int.Parse(GetValue("VydejProcZPalety2", Properties.Resources.VydejProcZPalety2)); }
            set { SetValue("VydejProcZPaletyt2", value.ToString()); }
        }
        public static int VydejPocetKusuPaleta2
        {
            get { return int.Parse(GetValue("VydejPocetKusuPaleta2", Properties.Resources.VydejPocetKusuPaleta2)); }
            set { SetValue("VydejPocetKusuPaletat2", value.ToString()); }
        }

        public static int VydejPocetKusuSkupBal2
        {
            get { return int.Parse(GetValue("VydejPocetKusuSkupBal2", Properties.Resources.VydejPocetKusuSkupBal2)); }
            set { SetValue("VydejPocetKusuSkupBal2", value.ToString()); }
        }

        public static int VydejID2
        {
            get { return int.Parse(GetValue("VydejID2", Properties.Resources.VydejID2)); }
            set { SetValue("VydejID2", value.ToString()); }
        }

        public static int VydejNazevWidth2
        {
            get { return int.Parse(GetValue("VydejNazevWidth2", Properties.Resources.VydejNazevWidth2)); }
            set { SetValue("VydejNazevWidth2", value.ToString()); }
        }

        public static int VydejZbyvaWidth2
        {
            get { return int.Parse(GetValue("VydejZbyvaWidth2", Properties.Resources.VydejZbyvaWidth2)); }
            set { SetValue("VydejZbyvaWidth2", value.ToString()); }
        }

        public static int VydejMnozstviWidth2
        {
            get { return int.Parse(GetValue("VydejMnozstviWidth2", Properties.Resources.VydejMnozstviWidth2)); }
            set { SetValue("VydejMnozstviWidth2", value.ToString()); }
        }

        public static int VydejRowHeight2
        {
            get { return int.Parse(GetValue("VydejRowHeight2", Properties.Resources.VydejRowHeight2)); }
            set { SetValue("VydejRowHeight2", value.ToString()); }
        }

            
	



        public static int VydejListVydejekDefaultRowHeight
        {
            get { return int.Parse(GetValue("VydejListVydejekDefaultRowHeight", Properties.Resources.VydejListVydejekDefaultRowHeight)); }
            set { SetValue("VydejListVydejekDefaultRowHeight", value.ToString()); }
        }

        public static int VydejListVydejekDavkaWidth
        {
            get { return int.Parse(GetValue("VydejListVydejekDavkaWidth", Properties.Resources.VydejListVydejekDavkaWidth)); }
            set { SetValue("VydejListVydejekDavkaWidth", value.ToString()); }
        }
               
        public static int VydejListVydejekSopnumbeWidth
        {
            get { return int.Parse(GetValue("VydejListVydejekSopnumbeWidth", Properties.Resources.VydejListVydejekSopnumbeWidth)); }
            set { SetValue("VydejListVydejekSopnumbeWidth", value.ToString()); }
        }                

        public static int VydejListVydejekInfo1Width
        {
            get { return int.Parse(GetValue("VydejListVydejekInfo1Width", Properties.Resources.VydejListVydejekInfo1Width)); }
            set { SetValue("VydejListVydejekInfo1Width", value.ToString()); }
        }

        public static int VydejListVydejekCntItemsWidth
        {
            get { return int.Parse(GetValue("VydejListVydejekCntItemsWidth", Properties.Resources.VydejListVydejekCntItemsWidth)); }
            set { SetValue("VydejListVydejekCntItemsWidth", value.ToString()); }
        }                  

        public static int VydejListVydejekSumItemsWidth
        {
            get { return int.Parse(GetValue("VydejListVydejekSumItemsWidth", Properties.Resources.VydejListVydejekSumItemsWidth)); }
            set { SetValue("VydejListVydejekSumItemsWidth", value.ToString()); }
        }


        //public static bool ProdejCarovyKodFiltr
        //{
        //    get { return bool.Parse(GetValue("ProdejCarovyKodFiltr", false.ToString())); }
        //    set { SetValue("ProdejCarovyKodFiltr", value.ToString()); }
        //}

		public static string ProdejSoundDisponibility
		{
            get
            {
                string prodejSoundDisponibility = GetValue("ProdejSoundDisponibility", string.Empty);
                ProdejSoundDisponibility = prodejSoundDisponibility; //aby se to ulozilo, pokud to jeste neni v konfiguraci
                return prodejSoundDisponibility;
            }
			set { SetValue("ProdejSoundDisponibility", value.ToString()); }
		}


        public static string ProdejRez1LastValue
        {
            get { return GetValue("ProdejRez1LastValue", string.Empty); }
            set { SetValue("ProdejRez1LastValue", value); }
        }

        public static string ProdejRez2LastValue
        {
            get { return GetValue("ProdejRez2LastValue", string.Empty); }
            set { SetValue("ProdejRez2LastValue", value); }
        }

        public static string ProdejRez3LastValue
        {
            get { return GetValue("ProdejRez3LastValue", string.Empty); }
            set { SetValue("ProdejRez3LastValue", value); }
        }

        public static string ProdejRez4LastValue
        {
            get { return GetValue("ProdejRez4LastValue", string.Empty); }
            set { SetValue("ProdejRez4LastValue", value); }
        }


        public static string ProdejListLastSort
        {
            get { return GetValue("ProdejListLastSort", string.Empty); }
            set { SetValue("ProdejListLastSort", value); }
        }

        public static string ProdejVyberPracovnikaLastSort
        {
            get { return GetValue("ProdejVyberPracovnikaLastSort", string.Empty); }
            set { SetValue("ProdejVyberPracovnikaLastSort", value); }
        }

        public static string ProdejVyberStrediskaLastSort
        {
            get { return GetValue("ProdejVyberStrediskaLastSort", string.Empty); }
            set { SetValue("ProdejVyberStrediskaLastSort", value); }
        }


        public static string ProdejVyberMenyLastSort
        {
            get { return GetValue("ProdejVyberMenyLastSort", string.Empty); }
            set { SetValue("ProdejVyberMenyLastSort", value); }
        }

        public static int ProdejVyberStrediskaDocIdWidth
        {
            get { return int.Parse(GetValue("ProdejVyberStrediskaDocIdWidth", Properties.Resources.ProdejVyberStrediskaDocIdWidth)); }
            set { SetValue("ProdejVyberStrediskaDocIdWidth", value.ToString()); }
        }

        public static int ProdejVyberStrediskaDocDescWidth
        {
            get { return int.Parse(GetValue("ProdejVyberStrediskaDocDescWidth", Properties.Resources.ProdejVyberStrediskaDocDescWidth)); }
            set { SetValue("ProdejVyberStrediskaDocDescWidth", value.ToString()); }
        }

        public static int ProdejVyberStrediskaDocTypWidth
        {
            get { return int.Parse(GetValue("ProdejVyberStrediskaDocTypWidth", Properties.Resources.ProdejVyberStrediskaDocTypWidth)); }
            set { SetValue("ProdejVyberStrediskaDocTypWidth", value.ToString()); }
        }

        public static int ProdejVyberStrediskaDocCarKodWidth
        {
            get { return int.Parse(GetValue("ProdejVyberStrediskaDocCarKodWidth", Properties.Resources.ProdejVyberStrediskaDocCarKodWidth)); }
            set { SetValue("ProdejVyberStrediskaDocCarKodWidth", value.ToString()); }
        }


        public static int ProdejVyberTypDokladuDocCarKodWidth
        {
            get { return int.Parse(GetValue("ProdejVyberTypDokladuDocCarKodWidth", Properties.Resources.ProdejVyberTypDokladuDocCarKodWidth)); }
            set { SetValue("ProdejVyberTypDokladuDocCarKodWidth", value.ToString()); }
        }

        public static int ProdejVyberTypDokladuDocTypWidth
        {
            get { return int.Parse(GetValue("ProdejVyberTypDokladuDocTypWidth", Properties.Resources.ProdejVyberTypDokladuDocTypWidth)); }
            set { SetValue("ProdejVyberTypDokladuDocTypWidth", value.ToString()); }
        }

        public static int ProdejVyberTypDokladuDocDescWidth
        {
            get { return int.Parse(GetValue("ProdejVyberTypDokladuDocDescWidth", Properties.Resources.ProdejVyberTypDokladuDocDescWidth)); }
            set { SetValue("ProdejVyberTypDokladuDocDescWidth", value.ToString()); }
        }

        public static int ProdejVyberTypDokladuDocIdWidth
        {
            get { return int.Parse(GetValue("ProdejVyberTypDokladuDocIdWidth", Properties.Resources.ProdejVyberTypDokladuDocIdWidth)); }
            set { SetValue("ProdejVyberTypDokladuDocIdWidth", value.ToString()); }
        }


        public static string ProdejVyberOdberateleLastSort
        {
            get { return GetValue("ProdejVyberOdberateleLastSort", string.Empty); }
            set { SetValue("ProdejVyberOdberateleLastSort", value); }
        }

        public static int ProdejVyberOdberateleOdbCarKodWidth
        {
            get { return int.Parse(GetValue("ProdejVyberOdberateleOdbCarKodWidth", Properties.Resources.ProdejVyberOdberateleOdbCarKodWidth)); }
            set { SetValue("ProdejVyberOdberateleOdbCarKodWidth", value.ToString()); }
        }

        public static int ProdejVyberOdberateleMenaWidth
        {
            get { return int.Parse(GetValue("ProdejVyberOdberateleMenaWidth", Properties.Resources.ProdejVyberOdberateleOdbCarKodWidth)); }
            set { SetValue("ProdejVyberOdberateleMenaWidth", value.ToString()); }
        }


        public static int ProdejVyberOdberateleOdbTypWidth
        {
            get { return int.Parse(GetValue("ProdejVyberOdberateleOdbTypWidth", Properties.Resources.ProdejVyberOdberateleOdbTypWidth)); }
            set { SetValue("ProdejVyberOdberateleOdbTypWidth", value.ToString()); }
        }

        public static int ProdejVyberOdberateleOdbDescWidth
        {
            get { return int.Parse(GetValue("ProdejVyberOdberateleOdbDescWidth", Properties.Resources.ProdejVyberOdberateleOdbDescWidth)); }
            set { SetValue("ProdejVyberOdberateleOdbDescWidth", value.ToString()); }
        }

        public static int ProdejVyberOdberateleOdbIdWidth
        {
            get { return int.Parse(GetValue("ProdejVyberOdberateleOdbIdWidth", Properties.Resources.ProdejVyberOdberateleOdbIdWidth)); }
            set { SetValue("ProdejVyberOdberateleOdbIdWidth", value.ToString()); }
        }

        public static int ProdejVyberDavkyCisloDavkyWidth
        {
            get { return int.Parse(GetValue("ProdejVyberDavkyCisloDavkyWidth", Properties.Resources.ProdejVyberDavkyCisloDavkyWidth)); }
            set { SetValue("ProdejVyberDavkyCisloDavkyWidth", value.ToString()); }
        }

        public static int ProdejVyberDavkyPolozekWidth
        {
            get { return int.Parse(GetValue("ProdejVyberDavkyPolozekWidth", Properties.Resources.ProdejVyberDavkyPolozekWidth)); }
            set { SetValue("ProdejVyberDavkyPolozekWidth", value.ToString()); }
        }

        public static int ProdejVyberDavkyOdberatelWidth
        {
            get { return int.Parse(GetValue("ProdejVyberDavkyOdberatelWidth", Properties.Resources.ProdejVyberDavkyOdberatelWidth)); }
            set { SetValue("ProdejVyberDavkyOdberatelWidth", value.ToString()); }
        }

        public static int ProdejVyberDavkyDokladWidth
        {
            get { return int.Parse(GetValue("ProdejVyberDavkyDokladWidth", "50")); }
            set { SetValue("ProdejVyberDavkyDokladWidth", value.ToString()); }
        }

        public static int ProdejVyberDavkyStrediskoWidth
        {
            get { return int.Parse(GetValue("ProdejVyberDavkyStrediskoWidth", "50")); }
            set { SetValue("ProdejVyberDavkyStrediskoWidth", value.ToString()); }
        }

        public static int ProdejVyberDavkySkladWidth
        {
            get { return int.Parse(GetValue("ProdejVyberDavkySkladWidth", "50")); }
            set { SetValue("ProdejVyberDavkySkladWidth", value.ToString()); }
        }

        public static int ProdejVyberDavkyRowHeight
        {
            get { return int.Parse(GetValue("ProdejVyberDavkyRowHeight", "23")); }
            set { SetValue("ProdejVyberDavkyRowHeight", value.ToString()); }
        }

		public static bool ProdejVyberOdberatele_Odberatel
        {
			get { return bool.Parse(GetValue("ProdejVyberOdberatele_Odberatel", true.ToString())); }
			set { SetValue("ProdejVyberOdberatele_Odberatel", value.ToString()); }
        }

		
        
                
        public static int VydejNazevWidth
        {
            get { return int.Parse(GetValue("VydejNazevWidth", Properties.Resources.VydejNazevWidth)); }
            set { SetValue("VydejNazevWidth", value.ToString()); }
        }

        public static int VydejZbyvaWidth
        {
            get { return int.Parse(GetValue("VydejZbyvaWidth", Properties.Resources.VydejZbyvaWidth)); }
            set { SetValue("VydejZbyvaWidth", value.ToString()); }
        }

        public static int VydejMnozstviWidth
        {
            get { return int.Parse(GetValue("VydejMnozstviWidth", Properties.Resources.VydejMnozstviWidth)); }
            set { SetValue("VydejMnozstviWidth", value.ToString()); }
        }


        public static int VydejCZCarKodWidth
        {
            get { return int.Parse(GetValue("VydejCZCarKodWidth", "50")); }
            set { SetValue("VydejCZCarKodWidth", value.ToString()); }
        }


        public static int VydejVNDITNUMWidth
        {
            get { return int.Parse(GetValue("VydejVNDITNUMWidth", "50")); }
            set { SetValue("VydejVNDITNUMWidth", value.ToString()); }
        }


        public static int VydejLokaceWidth
        {
            get { return int.Parse(GetValue("VydejLokaceWidth", "50")); }
            set { SetValue("VydejLokaceWidth", value.ToString()); }
        }
        
        public static int VydejRowHeight
        {
            get { return int.Parse(GetValue("VydejRowHeight", Properties.Resources.VydejRowHeight)); }
            set { SetValue("VydejRowHeight", value.ToString()); }
        }

        public static int VydejPaletListFormNazevWidth
        {
            get { return int.Parse(GetValue("VydejPaletListFormNazevWidth", Properties.Resources.VydejPaletListFormNazevWidth)); }
            set { SetValue("VydejPaletListFormNazevWidth", value.ToString()); }
        }

        public static int OnlineItemnumberPageNumber
        {
            get { return int.Parse(GetValue("OnlineItemnumberPageNumber", Properties.Resources.OnlineItemnumberPageNumber)); }
            set { SetValue("OnlineItemnumberPageNumber", value.ToString()); }
        }

        #region TaD Tisk Baleni soupis

        public static int Vydej_Baleni_Tisk_Font_Size_Width
        {
            get { return int.Parse(GetValue("Vydej_Baleni_Tisk_Font_Size_Width", "24")); }
            set { SetValue("Vydej_Baleni_Tisk_Font_Size_Width", value.ToString()); }
        }

        public static int Vydej_Baleni_Tisk_Font_Size_Height
        {
            get { return int.Parse(GetValue("Vydej_Baleni_Tisk_Font_Size_Height", "25")); }
            set { SetValue("Vydej_Baleni_Tisk_Font_Size_Height", value.ToString()); }
        }

        public static int Vydej_Baleni_Tisk_Font_Size_SirkaStitku
        {
            get { return int.Parse(GetValue("Vydej_Baleni_Tisk_Font_Size_SirkaStitku", "562")); }
            set { SetValue("Vydej_Baleni_Tisk_Font_Size_SirkaStitku", value.ToString()); }
        }
        

        public static int Vydej_Baleni_Tisk_Font_Size_Medzera
        {
            get { return int.Parse(GetValue("Vydej_Baleni_Tisk_Font_Size_Medzera", "8")); }
            set { SetValue("Vydej_Baleni_Tisk_Font_Size_Medzera", value.ToString()); }
        }

        public static int Vydej_Baleni_Tisk_Font_Size_ZnakuNaRadek
        {
            get { return int.Parse(GetValue("Vydej_Baleni_Tisk_Font_Size_ZnakuNaRadek", "91")); }
            set { SetValue("Vydej_Baleni_Tisk_Font_Size_ZnakuNaRadek", value.ToString()); }
        }

		public static bool Vydej_Baleni_Tisk_Enable
        {
			get { return bool.Parse(GetValue("Vydej_Baleni_Tisk_Enable", true.ToString())); }
			set { SetValue("Vydej_Baleni_Tisk_Enable", value.ToString()); }
        }

		public static bool Vydej_Soupis_Tisk_Enable
		{
			get { return bool.Parse(GetValue("Vydej_Soupis_Tisk_Enable", true.ToString())); }
			set { SetValue("Vydej_Soupis_Tisk_Enable", value.ToString()); }
		}

        public static string Vydej_dimensionSirka
        {
            get { return GetValue("Vydej_dimensionSirka", "0"); }
            set { SetValue("Vydej_dimensionSirka", value); }
        }

        public static string Vydej_dimensionVyska
        {
            get { return GetValue("Vydej_dimensionVyska", "0"); }
            set { SetValue("Vydej_dimensionVyska", value); }
        }

        public static string Vydej_dimensionHloubka
        {
            get { return GetValue("Vydej_dimensionHloubka", "0"); }
            set { SetValue("Vydej_dimensionHloubka", value); }
        }

        public static string Vydej_gross_weight
        {
            get { return GetValue("Vydej_gross_weight", "0"); }
            set { SetValue("Vydej_gross_weight", value); }
        }


		#region Scanner kontinualne

		//public static bool scanner_SoftTrigger
		//{
		//    get { return bool.Parse(GetValue("scanner_SoftTrigger", false.ToString())); }
		//    set { SetValue("scanner_SoftTrigger", value.ToString()); }
		//}


		//public static int scanner_last_successbeeptime
		//{
		//    get { return int.Parse(GetValue("scanner_last_successbeeptime", "0")); }
		//    set { SetValue("scanner_last_successbeeptime", value.ToString()); }
		//}

		/////
		//public static Fask.ScannerProvider.AIMTYPE scanner_last_aimtype
		//{
		//    get { return GetAimtype(GetValue("scanner_last_aimtype", "0")); }
		//    set { SetValue("scanner_last_aimtype", SetAimtype(value)); }
		//}


		//private static Fask.ScannerProvider.AIMTYPE GetAimtype(string value)
		//{
		//    switch (value)
		//    {
		//        case "5":
		//            return Fask.ScannerProvider.AIMTYPE.CONTINUOUS_READ;
		//        case "3":
		//            return Fask.ScannerProvider.AIMTYPE.PRESS_AND_RELEASE;
		//        case "1":
		//            return Fask.ScannerProvider.AIMTYPE.TIMED_HOLD;
		//        case "2":
		//            return Fask.ScannerProvider.AIMTYPE.TIMED_RELEASE;
		//        case "0":
		//            return Fask.ScannerProvider.AIMTYPE.TRIGGER;
		//        case "-1":
		//        default:
		//            return Fask.ScannerProvider.AIMTYPE.UNKNOWN;
		//    }
		//}

		//private static string SetAimtype(Fask.ScannerProvider.AIMTYPE value)
		//{
		//    switch (value)
		//    {
		//        case Fask.ScannerProvider.AIMTYPE.CONTINUOUS_READ:
		//            return "5";
		//        case Fask.ScannerProvider.AIMTYPE.PRESS_AND_RELEASE:
		//            return "3";
		//        case Fask.ScannerProvider.AIMTYPE.TIMED_HOLD:
		//            return "1";
		//        case Fask.ScannerProvider.AIMTYPE.TIMED_RELEASE:
		//            return "2";
		//        case Fask.ScannerProvider.AIMTYPE.TRIGGER:
		//            return "0";
		//        case Fask.ScannerProvider.AIMTYPE.UNKNOWN:
		//        default:
		//            return "-1";
		//    }
		//}
	#endregion
        
        
        


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
                return (val != null ? val : defalutValue);
            }
            catch 
            {
                return defalutValue;
            }
        }

        static Settings()
        {
            // Get the path of the settings file.

            try
            {
                m_settingsPath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase), "Settings.xml");
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
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                System.Windows.Forms.MessageBox.Show(
                    ex.Message + 
                    (ex.InnerException == null ? string.Empty : ("\n" +ex.InnerException.Message)),
                    "Settings load", 
                    System.Windows.Forms.MessageBoxButtons.OK, 
                    System.Windows.Forms.MessageBoxIcon.Exclamation, 
                    System.Windows.Forms.MessageBoxDefaultButton.Button1);                
            }
        }

        public static void Update()
        {
            XmlTextWriter tw = new XmlTextWriter(
                m_settingsPath,
                System.Text.UTF8Encoding.UTF8
                );

            tw.Formatting = Formatting.Indented;
            tw.Indentation = 4;

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
