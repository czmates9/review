using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Fask.MST_W.Prijem_4
{
    /// <summary>
    /// Konfigurace modulu prijem
    /// </summary>
    public class Globals
    {

        /*
        /// <summary>
        /// Provadet kontrolu, zda je nasnimane cislo kratsi nez
        /// </summary>
        public static bool CONFIG_KONT_DELKA = false;

        /// <summary>
        /// Provadet kontrolu dokoncenosti
        /// </summary>
        public static bool CONFIG_KONT_DOKONCENOSTI = false;

        /// <summary>
        /// false: po zadani MN bude v cyklu snimat SN, dokud to uziv. neprerusi
        ///  true: po sejmuti SN se vrati zpet na zadavani MN
        /// </summary>
        public static bool CONFIG_ZADAT_MN_POKAZDE = false;

        /// <summary>
        /// false: nepta se
        ///  true: pokud neni jeste nasminano poz. mnozstvi, pak se zepta, jestli chce opravdu skoncit
        /// </summary>
        public static bool CONFIG_KONT_UPL_POL = false;

        /// <summary>
        /// false: neptat se
        ///  true: je-li jiz nasnimano pozadovane mnozstvi, pak se zepta, jestli chce snimat dal
        /// </summary>
        public static bool CONFIG_PTATSE_NEANO = false;

        /// <summary>
        /// false: pokracovat bez dohledavani(po vyberu modeloveho cisla)
        ///  true: dohledat pocet a ptat se, zda pokracovat, pokud je toto MN hotovo(po vyberu modeloveho cisla)
        /// </summary>
        public static bool CONFIG_POKRDOHLED = false;

        /// <summary>
        /// 0: listovani seznamem modelovych cisel
        /// 1: snimani MN
        /// </summary>
        public static bool CONFIG_LISTSNIM = false;

        /// <summary>
        /// 0: disable nasledujici volbu(1b)
        /// 1: enable nasledujici volbu(1b)
        /// </summary>
        public static bool ENABLE_LISTSNIM = false;


        // parametry urcujici, ktere hodnoty se budou snimat
        /// <summary>
        /// 0: Snima udaj do PONUMBER
        /// 1: Snima udaj do VNDITNUM(!!!NE PONUMBER!!!)
        /// </summary>
        public static bool CONFIG_SNIM_PONUMBER = false;
        public static bool CONFIG_SNIM_REZ2 =false;
        public static bool CONFIG_SNIM_LOCNCODE = false;
        
        // parametry, ktere budou ridit snimani, v pripade, ze neni predloha
        public static bool ENABLE_SNIMATZADAT_SN =false;
        /// <summary>
        /// Je-li bez predlohy, pak se ridi timto parametrem
        /// </summary>
        public static bool CONFIG_SNIMATZADAT_SN = false;
        /// <summary>
        /// Snimat sw (je-li bez predlohy)
        /// </summary>
        public static bool CONFIG_SNIMAT_POL1 = false;
        /// <summary>
        /// Snimat dv (je-li bez predlohy)
        /// </summary>
        public static bool CONFIG_SNIMAT_POL2 = false;
        /// <summary>
        /// Snimat rez1 (s i bez predlohy)
        /// </summary>
        public static bool CONFIG_SNIMAT_POL3 = false;

        /// <summary>
        /// 0: je-li v db uvedena nulova delka kodu, pak delku nekontroluj
        /// 1: je-li v db uvedena nulova delka kodu, pak si zapamatuj posledni delku posledniho nasnimaneho kodu a tu kontroluj
        /// </summary>
        public static bool CONFIG_KONT_NUL_DELKA = false;

        /// <summary>
        /// 0: pouzit puvodni prim. klic (ponumber, ord)
        /// 1: pouzit alter. prim. klic pri nevyplnenem ponumber a ord (itemnmbr)
        /// </summary>
        public static bool CONFIG_PRIM_KEY1 = false;

        /// <summary>
        /// 0: nepovolit tvorbu nove karty
        /// 1: povolit
        /// </summary>
        public static bool CONFIG_NOVA_KARTA = false;

        /// <summary>
        /// 0: nepovolit duplicitu seriovych cisel (sarzi)
        /// 1: povolit
        /// </summary>
        public static bool CONFIG_DUPLIC_SN = false;
        */



        private static bool _overfillitem = true;
        private static int _DelkaKontrolySN = 5;
        private static DateTime _Datum = DateTime.Now;
        private static int _PosledniGenSN = 0;
        private static int _PocetCislicVlastniSN = 5;
        private static int _MaxDelkaZakazSN = 50; // TOTO je ZLE, prebirat z Fask.Columns
        private static int _MaxPocetZakazSN = 5;

        private static int _ZobrazitZaznamu = 10;
        public static int ZobrazitZaznamu
        {
            get { return _ZobrazitZaznamu; }
            set { _ZobrazitZaznamu = value; }
        }
        private static bool _Rez2Cislo = true;
        public static bool Rez2Cislo
        {
            get { return _Rez2Cislo; }
            set { _Rez2Cislo = value; }
        }

        private static bool _Rez1Cislo = true;
        public static bool Rez1Cislo
        {
            get { return _Rez1Cislo; }
            set { _Rez1Cislo = value; }
        }

        private static bool _Rez2Povinne = true;
        public static bool Rez2Povinne
        {
            get { return _Rez2Povinne; }
            set { _Rez2Povinne = value; }
        }

        private static bool _Rez1Povinne = true;
        public static bool Rez1Povinne
        {
            get { return _Rez1Povinne; }
            set { _Rez1Povinne = value; }
        }

        private static bool _Rez2Pamatovat = true;
        public static bool Rez2Pamatovat
        {
            get { return _Rez2Pamatovat; }
            set { _Rez2Pamatovat = value; }
        }

        private static bool _Rez1Pamatovat = true;
        public static bool Rez1Pamatovat
        {
            get { return _Rez1Pamatovat; }
            set { _Rez1Pamatovat = value; }
        }


        /// <summary>
        /// Povoli/Zakaze preplneni polozky
        /// </summary>
        public static bool OverFillItem
        {
            get { return _overfillitem; }
            set { _overfillitem = value; }
        }

        /// <summary>
        /// Pocet znaku ktere se kontroluji u zadaneho SN
        /// </summary>
        public static int DelkaKontrolySN
        {
            get { return _DelkaKontrolySN; }
            set { _DelkaKontrolySN = value; }
        }
        /// <summary>
        /// Nastaveni Data a casu
        /// </summary>
        public static DateTime Datum
        {
            get { return _Datum; }
            set { _Datum = value; }
        }
        /// <summary>
        /// Posledni generovane SN
        /// </summary>
        public static int PosledniGenSN
        {
            get { return _PosledniGenSN; }
            set { _PosledniGenSN = value; }
        }
        /// <summary>
        /// Nastavi pocet platnych cislic pri generovani vlastniho SN
        /// </summary>
        public static int PocetCislicVlastniSN
        {
            get { return _PocetCislicVlastniSN; }
            set { _PocetCislicVlastniSN = value; }
        }

        private static bool _zobrazitDialogZadaniMnozstviParsovanehoKodu = true;
        /// <summary>
        /// Povoleni zobrazeni dialogu pro potvrzeni mnozstvi v pripade sejmuti napr. vahoveho kodu.
        /// </summary>
        public static bool ZobrazitDialogZadaniMnozstviParsovanehoKodu
        {
            get { return _zobrazitDialogZadaniMnozstviParsovanehoKodu; }
            set { _zobrazitDialogZadaniMnozstviParsovanehoKodu = value; }
        }

        /// <summary>
        /// Maximalni delka zadaneho zakaznickeho SN
        /// </summary>
        public static int MaxDelkaZakazSN
        {
            get { return _MaxDelkaZakazSN; }
            set { _MaxDelkaZakazSN = value; }
        }
        /// <summary>
        /// Maximalni pocet zakaznickych SN (pocet platnych cislic)
        /// </summary>
        public static int MaxPocetZakazSN
        {
            get { return _MaxPocetZakazSN; }
            set { _MaxPocetZakazSN = value; }
        }

        private static bool _nacistSkladIDOnline = false;
        /// <summary>
        /// Nacist ID skladu online (ANC).
        /// </summary>
        public static bool NacistSkladIDOnline
        {
            get { return _nacistSkladIDOnline; }
            set { _nacistSkladIDOnline = value; }
        }

        private static bool _ZadaniLocncodePredSN = true;
        public static bool ZadaniLocncodePredSN
        {
            get { return _ZadaniLocncodePredSN; }
            set { _ZadaniLocncodePredSN = value; }
        }

        private static bool _HledaniCkAutoVyberPrvniNeuplne = true;
        public static bool HledaniCkAutoVyberPrvniNeuplne
        {
            get { return _HledaniCkAutoVyberPrvniNeuplne; }
            set { _HledaniCkAutoVyberPrvniNeuplne = value; }
        }

        private static bool _EtiketaTiskPoVlozeniDotaz = false;
        public static bool EtiketaTiskPoVlozeniDotaz
        {
            get { return _EtiketaTiskPoVlozeniDotaz; }
            set { _EtiketaTiskPoVlozeniDotaz = value; }
        }


        private static bool _EtiketyTiskPredOdeslanimDotaz = false;
        public static bool EtiketyTiskPredOdeslanimDotaz
        {
            get { return _EtiketyTiskPredOdeslanimDotaz; }
            set { _EtiketyTiskPredOdeslanimDotaz = value; }
        }

        private static bool _EtiketyTiskPoOtevreniDotaz = false;
        public static bool EtiketyTiskPoOtevreniDotaz
        {
            get { return _EtiketyTiskPoOtevreniDotaz; }
            set { _EtiketyTiskPoOtevreniDotaz = value; }
        }

		private static bool _EtiketaTisk_PrebiratMnozstvi = false;
		public static bool EtiketaTisk_PrebiratMnozstvi
		{
			get { return _EtiketaTisk_PrebiratMnozstvi; }
			set { _EtiketaTisk_PrebiratMnozstvi = value; }
		}



        private static bool _PolozkyVyberJenScannerem = false;
        public static bool PolozkyVyberJenScannerem
        {
            get { return _PolozkyVyberJenScannerem; }
            set { _PolozkyVyberJenScannerem = value; }
        }

        private static bool _SlucovaniDavek = false;
        public static bool SlucovaniDavek
        {
            get { return _SlucovaniDavek; }
            set { _SlucovaniDavek = value; }
        }

        private static bool _LokaceNaDavkuPovolit = false;
        public static bool LokaceNaDavkuPovolit
        {
            get { return _LokaceNaDavkuPovolit; }
            set { _LokaceNaDavkuPovolit = value; }
        }

        private static bool _OnlinePohyby = false;
        public static bool OnlinePohyby
        {
            get { return _OnlinePohyby; }
            set { _OnlinePohyby = value; }
        }

        private static bool _ViceTerminaly = false;
        public static bool ViceTerminaly
        {
            get { return _ViceTerminaly; }
            set { _ViceTerminaly = value; }
        }

        private static bool _ZmenaDataDokladu = false;
        public static bool ZmenaDataDokladu
        {
            get { return _ZmenaDataDokladu; }
            set { _ZmenaDataDokladu = value; }
        }

        private static bool _GenerovaniPrikazuPozadovatSklad = false;
        public static bool GenerovaniPrikazuZadatSklad
        {
            get { return _GenerovaniPrikazuPozadovatSklad; }
            set { _GenerovaniPrikazuPozadovatSklad = value; }
        }

        private static bool _ParsovaniCarovehoKoduPovolit = false;
        public static bool ParsovaniCarovehoKoduPovolit
        {
            get { return _ParsovaniCarovehoKoduPovolit; }
            set { _ParsovaniCarovehoKoduPovolit = value; }
        }

        private static bool _GenerovaniSarze = false;
        public static bool GenerovaniSarze
        {
            get { return _GenerovaniSarze; }
            set { _GenerovaniSarze = value; }
        }

        private static bool _OverovatLokaci = false;
        public static bool OverovatLokaci
        {
            get { return _OverovatLokaci; }
            set { _OverovatLokaci = value; }
        }

        private static bool _DoporuceneLokace = false;
        public static bool DoporuceneLokace
        {
            get { return _DoporuceneLokace; }
            set { _DoporuceneLokace = value; }
        }

        private static bool _DoplneniVychoziLokacePovolit = false;
        /// <summary>
        /// Povoleni doplneni vychozi lokace.
        /// </summary>
        public static bool DoplneniVychoziLokacePovolit
        {
            get { return _DoplneniVychoziLokacePovolit; }
            set { _DoplneniVychoziLokacePovolit = value; }
        }

        private static bool _NezrealizovanePrijemky = false;
        public static bool NezrealizovanePrijemky
        {
            get { return _NezrealizovanePrijemky; }
            set { _NezrealizovanePrijemky = value; }
        }

        /// <summary>
        /// možnost nataženi neszealizovanych prijemek bud všechny anebo selekce pomoci èarovych kodu položek uprava pro Hanibal
        /// </summary>
        private static bool _NerealizovanePrijekyCarKody = false;
        public static bool NerealizovanePrijekyCarKody
        {
            get { return _NerealizovanePrijekyCarKody; }
            set { _NerealizovanePrijekyCarKody = value; }
        }



        private static bool _KontrolovatSNsPredlohou = true;
        public static bool KontrolovatSNsPredlohou
        {
            get { return _KontrolovatSNsPredlohou; }
            set { _KontrolovatSNsPredlohou = value; }
        }

        private static bool _ZobrazitDialogZadaniSN = true;
        public static bool ZobrazitDialogZadaniSN
        {
            get { return _ZobrazitDialogZadaniSN; }
            set { _ZobrazitDialogZadaniSN = value; }
        }

        private static bool _PovolitPrazdnouHodnotuSN = false;
        public static bool PovolitPrazdnouHodnotuSN
        {
            get { return _PovolitPrazdnouHodnotuSN; }
            set { _PovolitPrazdnouHodnotuSN = value; }
        }

        private static bool _PovolitFoceniPriPridaniPolozky = false;
        public static bool PovolitFoceniPriPridaniPolozky
        {
            get { return _PovolitFoceniPriPridaniPolozky; }
            set { _PovolitFoceniPriPridaniPolozky = value; }
        }

        private static bool _DavkaOtevritIhnedPoStazeni = false;
        public static bool DavkaOtevritIhnedPoStazeni
        {
            get { return _DavkaOtevritIhnedPoStazeni; }
            set { _DavkaOtevritIhnedPoStazeni = value; }
        }

        private static bool _PovolitZalokovani = false;
        public static bool PovolitZalokovani
        {
            get { return _PovolitZalokovani; }
            set { _PovolitZalokovani = value; }
        }

        #region TaD Dialog Prijem

        private static bool _PrijemDialogUspesnehoOdeslaniDavky = false;
        public static bool PrijemDialogUspesnehoOdeslaniDavky
        {
            get { return _PrijemDialogUspesnehoOdeslaniDavky; }
            set { _PrijemDialogUspesnehoOdeslaniDavky = value; }
        }


        /// <summary>
        /// Preskoceni dialogu o opusteni modulu
        /// </summary>
        private static bool _PrijemDialogOpusteniModulu = true;
        public static bool PrijemDialogOpusteniModulu
        {
            get { return _PrijemDialogOpusteniModulu; }
            set { _PrijemDialogOpusteniModulu = value; }
        }

        /// <summary>
        /// Preskoceni dialogu o opusteni modulu
        /// </summary>
        private static bool _PrijemDialogOpusteniPrijemky = true;
        public static bool PrijemDialogOpusteniPrijemky
        {
            get { return _PrijemDialogOpusteniPrijemky; }
            set { _PrijemDialogOpusteniPrijemky = value; }
        }

        /// <summary>
        /// Preskoceni dialogu o opusteni modulu
        /// </summary>
        private static bool _PrijemDialogOpusteniZalokovani = true;
        public static bool PrijemDialogOpusteniZalokovani
        {
            get { return _PrijemDialogOpusteniZalokovani; }
            set { _PrijemDialogOpusteniZalokovani = value; }
        }

		/// <summary>
		/// Dialog pro dotaz zda tisknout etiketu s cenou nebo bez HANIBAL
		/// </summary>
		private static bool _EtiketaTiskDotazSCenou = false;
		public static bool EtiketaTiskDotazSCenou
		{
			get { return _EtiketaTiskDotazSCenou; }
			set { _EtiketaTiskDotazSCenou = value; }
		}

		/// <summary>
		/// Promenna ktera bude nest logiku zda se ma tisknout s cenou anebo bez
		/// </summary>
		private static bool _EtiketaTiskDotazSCenou_Cena = false;
		public static bool EtiketaTiskDotazSCenou_Cena
		{
			get { return _EtiketaTiskDotazSCenou_Cena; }
			set { _EtiketaTiskDotazSCenou_Cena = value; }
		}

		/// <summary>
		/// Promenna ktera urèuje zda se ma zobrazit dialog anebo brat brednastavenou hodnnotu
		/// </summary>
		private static bool _EtiketaTiskDotazSCenou_ZobrazDialog = false;
		public static bool EtiketaTiskDotazSCenou_ZobrazDialog
		{
			get { return _EtiketaTiskDotazSCenou_ZobrazDialog; }
			set { _EtiketaTiskDotazSCenou_ZobrazDialog = value; }
		}



        #endregion

        private static bool _PovolitZmenuRezimuZadaniLokace = false;
        public static bool PovolitZmenuRezimuZadaniLokace
        {
            get { return _PovolitZmenuRezimuZadaniLokace; }
            set { _PovolitZmenuRezimuZadaniLokace = value; }
        }

        private static bool _ZobrazovatReport = false;
        public static bool ZobrazovatReport
        {
            get { return _ZobrazovatReport; }
            set { _ZobrazovatReport = value; }
        }

        private static bool _MnozstviAutoJedna = false;
        public static bool MnozstviAutoJedna
        {
            get { return _MnozstviAutoJedna; }
            set { _MnozstviAutoJedna = value; }
        }

        private static bool _RozhodovatSkladExpedice = false;
        public static bool RozhodovatSkladExpedice
        {
            get { return _RozhodovatSkladExpedice; }
            set { _RozhodovatSkladExpedice = value; }
        }

        private static bool _DialogTisk = false;
        public static bool DialogTisk
        {
            get { return _DialogTisk; }
            set { _DialogTisk = value; }
        }



        private static bool _ZalokovaniPozadovatZadaniMnozstvi = false;
        public static bool ZalokovaniPozadovatZadaniMnozstvi
        {
            get { return _ZalokovaniPozadovatZadaniMnozstvi; }
            set { _ZalokovaniPozadovatZadaniMnozstvi = value; }
        }

        private static bool _GenerovatNenalezenouPrijemku = false;
        public static bool GenerovatNenalezenouPrijemku
        {
            get { return _GenerovatNenalezenouPrijemku; }
            set { _GenerovatNenalezenouPrijemku = value; }
        }

        private static bool _SkladPouzit = false;
        public static bool SkladPouzit
        {
            get { return _SkladPouzit; }
            set { _SkladPouzit = value; }
        }

        private static string _SkladID = string.Empty;
        public static string SkladID
        {
            get { return _SkladID; }
            set { _SkladID = value; }
        }

        private static bool _PrevzitIDSkladuZCiselnikuSkladu = false;
        public static bool PrevzitIDSkladuZCiselnikuSkladu
        {
            get { return _PrevzitIDSkladuZCiselnikuSkladu; }
            set { _PrevzitIDSkladuZCiselnikuSkladu = value; }
        }

        private static bool _FiltrCiselnikSkladuOnlyOne = false;
        public static bool FiltrCiselnikSkladuOnlyOne
        {
            get { return _FiltrCiselnikSkladuOnlyOne; }
            set { _FiltrCiselnikSkladuOnlyOne = value; }
        }

        private static bool _KontrolaVyplneniLokaciPredOdeslanim = false;
        public static bool KontrolaVyplneniLokaciPredOdeslanim
        {
            get { return _KontrolaVyplneniLokaciPredOdeslanim; }
            set { _KontrolaVyplneniLokaciPredOdeslanim = value; }
        }

        private static bool _OnlineHmotnost = false;
        public static bool OnlineHmotnost
        {
            get { return _OnlineHmotnost; }
            set { _OnlineHmotnost = value; }
        }

		private static int _sequenceSSCC = 1;
		/// <summary>
		/// Sequence SSCC kodu 
		/// </summary>
		public static int SequenceSSCC
		{
			get { return _sequenceSSCC; }
			set { _sequenceSSCC = value; }
		}

		private static bool _typOznaceniPalety = false;
		/// <summary>
		/// Povoleni palet 
		/// </summary>
		public static bool TypOznaceniPalety
		{
			get { return _typOznaceniPalety; }
			set { _typOznaceniPalety = value; }
		}

		private static bool _typyPoctyPalet = false;
		/// <summary>
		/// Povoleni palet 
		/// </summary>
		public static bool TypyPoctyPalet
		{
			get { return _typyPoctyPalet; }
			set { _typyPoctyPalet = value; }
		}


        public static bool Load(string filename)
        {
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(filename);
                XmlElement confignode = null;

                confignode = xmldoc.SelectSingleNode("/Config/Modules/Prijem/OverFillItem") as XmlElement;
                if (confignode != null)
                {
                    try { _overfillitem = bool.Parse(confignode.InnerText); }
                    catch { }
                }
                confignode = null;

                confignode = xmldoc.SelectSingleNode("/Config/Modules/Prijem/DelkaKontrolySN") as XmlElement;
                if (confignode != null)
                {
                    try { _DelkaKontrolySN = Convert.ToInt32(confignode.InnerText); }
                    catch { }
                }
                confignode = null;

                confignode = xmldoc.SelectSingleNode("/Config/Modules/Prijem/Datum") as XmlElement;
                if (confignode != null)
                {
                    try { _Datum = Convert.ToDateTime(confignode.InnerText, System.Globalization.DateTimeFormatInfo.InvariantInfo); }
                    catch { }
                }
                confignode = null;

                confignode = xmldoc.SelectSingleNode("/Config/Modules/Prijem/PocetCislicVlastniSN") as XmlElement;
                if (confignode != null)
                {
                    try { _PocetCislicVlastniSN = Convert.ToInt32(confignode.InnerText); }
                    catch { }
                }
                confignode = null;


                confignode = xmldoc.SelectSingleNode("/Config/Modules/Prijem/ZadaniLocncodePredSN") as XmlElement;
                if (confignode != null)
                {
                    try { _ZadaniLocncodePredSN = bool.Parse(confignode.InnerText); }
                    catch { }
                }
                confignode = null;


                confignode = xmldoc.SelectSingleNode("/Config/Modules/Prijem/PosledniGenSN") as XmlElement;
                if (confignode != null)
                {
                    try { _PosledniGenSN = Convert.ToInt32(confignode.InnerText); }
                    catch { }
                }
                confignode = null;

                confignode = xmldoc.SelectSingleNode("/Config/Modules/Prijem/MaxDelkaZakazSN") as XmlElement;
                if (confignode != null)
                {
                    try { _MaxDelkaZakazSN = Convert.ToInt32(confignode.InnerText); }
                    catch { }
                }
                confignode = null;

                confignode = xmldoc.SelectSingleNode("/Config/Modules/Prijem/MaxPocetZakazSN") as XmlElement;
                if (confignode != null)
                {
                    try { _MaxPocetZakazSN = Convert.ToInt32(confignode.InnerText); }
                    catch { }
                }
                confignode = null;

                confignode = xmldoc.SelectSingleNode("/Config/Modules/Prijem/Rez1Cislo") as XmlElement;
                if (confignode != null)
                {
                    try { _Rez1Cislo = bool.Parse(confignode.InnerText); }
                    catch { }
                }
                confignode = null;

                confignode = xmldoc.SelectSingleNode("/Config/Modules/Prijem/Rez2Cislo") as XmlElement;
                if (confignode != null)
                {
                    try { _Rez2Cislo = bool.Parse(confignode.InnerText); }
                    catch { }
                }
                confignode = null;

                confignode = xmldoc.SelectSingleNode("/Config/Modules/Prijem/Rez1Povinne") as XmlElement;
                if (confignode != null)
                {
                    try { _Rez1Povinne = bool.Parse(confignode.InnerText); }
                    catch { }
                }
                confignode = null;

                confignode = xmldoc.SelectSingleNode("/Config/Modules/Prijem/Rez2Povinne") as XmlElement;
                if (confignode != null)
                {
                    try { _Rez2Povinne = bool.Parse(confignode.InnerText); }
                    catch { }
                }
                confignode = null;

                _Rez1Pamatovat = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prijem/Rez1Pamatovat", _Rez1Pamatovat.ToString()));
                _Rez2Pamatovat = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prijem/Rez2Pamatovat", _Rez2Pamatovat.ToString()));
                _ZobrazitZaznamu = int.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prijem/ZobrazitZaznamu", _ZobrazitZaznamu.ToString()));

                _HledaniCkAutoVyberPrvniNeuplne = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prijem/HledaniCkAutoVyberPrvniNeuplne", _HledaniCkAutoVyberPrvniNeuplne.ToString()));
                _EtiketaTiskPoVlozeniDotaz = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prijem/EtiketaTiskPoVlozeniDotaz", _EtiketaTiskPoVlozeniDotaz.ToString()));
                _EtiketyTiskPredOdeslanimDotaz = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prijem/EtiketyTiskPredOdeslanimDotaz", _EtiketyTiskPredOdeslanimDotaz.ToString()));
                _EtiketyTiskPoOtevreniDotaz = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prijem/EtiketyTiskPoOtevreniDotaz", _EtiketyTiskPoOtevreniDotaz.ToString()));
				_EtiketaTisk_PrebiratMnozstvi = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prijem/EtiketaTisk_PrebiratMnozstvi", _EtiketaTisk_PrebiratMnozstvi.ToString()));

				_DialogTisk = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prijem/EtiketaTisk_DialogTisk", _DialogTisk.ToString()));


                _PolozkyVyberJenScannerem = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prijem/PolozkyVyberJenScannerem", _PolozkyVyberJenScannerem.ToString()));
                _SlucovaniDavek = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prijem/SlucovaniDavek", _SlucovaniDavek.ToString()));
                _OnlinePohyby = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prijem/OnlinePohyby", _OnlinePohyby.ToString()));
                _ViceTerminaly = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prijem/ViceTerminaly", _ViceTerminaly.ToString()));
                _ZmenaDataDokladu = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prijem/ZmenaDataDokladu", _ZmenaDataDokladu.ToString()));
                _GenerovaniPrikazuPozadovatSklad = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prijem/GenerovaniPrikazuPozadovatSklad", _GenerovaniPrikazuPozadovatSklad.ToString()));
                _GenerovaniSarze = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prijem/GenerovaniSarze", _GenerovaniSarze.ToString()));
                _OverovatLokaci = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prijem/OverovatLokaci", _OverovatLokaci.ToString()));
                _PovolitFoceniPriPridaniPolozky = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prijem/PovolitFoceniPriPridaniPolozky", _PovolitFoceniPriPridaniPolozky.ToString()));
                _DavkaOtevritIhnedPoStazeni = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prijem/DavkaOtevritIhnedPoStazeni", _DavkaOtevritIhnedPoStazeni.ToString()));
                _PovolitZalokovani = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prijem/PovolitZalokovani", _PovolitZalokovani.ToString()));
                _ZalokovaniPozadovatZadaniMnozstvi = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prijem/ZalokovaniPozadovatZadaniMnozstvi", _ZalokovaniPozadovatZadaniMnozstvi.ToString()));
                _DoporuceneLokace = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prijem/DoporuceneLokace", _DoporuceneLokace.ToString()));
                _KontrolovatSNsPredlohou = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prijem/KontrolovatSNsPredlohou", _KontrolovatSNsPredlohou.ToString()));
                _ZobrazitDialogZadaniSN = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prijem/ZobrazitDialogZadaniSN", _ZobrazitDialogZadaniSN.ToString()));
                _PovolitPrazdnouHodnotuSN = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prijem/PovolitPrazdnouHodnotuSN", _PovolitPrazdnouHodnotuSN.ToString()));
                _GenerovatNenalezenouPrijemku = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prijem/GenerovatNenalezenouPrijemku", _GenerovatNenalezenouPrijemku.ToString()));
                _KontrolaVyplneniLokaciPredOdeslanim = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prijem/KontrolaVyplneniLokaciPredOdeslanim", _KontrolaVyplneniLokaciPredOdeslanim.ToString()));
                _NezrealizovanePrijemky = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prijem/NezrealizovanePrijemky", _NezrealizovanePrijemky.ToString()));
                _NerealizovanePrijekyCarKody = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prijem/NerealizovanePrijekyCarKody", _NerealizovanePrijekyCarKody.ToString()));

                // Prijem sklady
                _SkladPouzit = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prijem/SkladPouzit", _SkladPouzit.ToString()));
                _SkladID = MST_Global.LoadElement(xmldoc, "/Config/Modules/Prijem/SkladID", _SkladID);
                _PrevzitIDSkladuZCiselnikuSkladu = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prijem/PrevzitIDSkladuZCiselnikuSkladu", _PrevzitIDSkladuZCiselnikuSkladu.ToString()));
                _FiltrCiselnikSkladuOnlyOne = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prijem/FiltrCiselnikSkladuOnlyOne", _FiltrCiselnikSkladuOnlyOne.ToString()));
                _DoplneniVychoziLokacePovolit = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prijem/DoplneniVychoziLokacePovolit", _DoplneniVychoziLokacePovolit.ToString()));
                _ParsovaniCarovehoKoduPovolit = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prijem/ParsovaniCarovehoKoduPovolit", _ParsovaniCarovehoKoduPovolit.ToString()));
                _LokaceNaDavkuPovolit = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prijem/PovolitLokaceNaDavku", _LokaceNaDavkuPovolit.ToString()));
                _zobrazitDialogZadaniMnozstviParsovanehoKodu = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prijem/ZobrazitDialogZadaniMnozstviParsovanehoKodu", _zobrazitDialogZadaniMnozstviParsovanehoKodu.ToString()));
                _OnlineHmotnost = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prijem/OnlineHmotnost", _OnlineHmotnost.ToString()));
                _nacistSkladIDOnline = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prijem/NacistSkladIDOnline", _nacistSkladIDOnline.ToString()));
                _PovolitZmenuRezimuZadaniLokace = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prijem/PovolitZmenuRezimuZadaniLokace", _PovolitZmenuRezimuZadaniLokace.ToString()));

                #region TaD Dialogy

                _PrijemDialogUspesnehoOdeslaniDavky = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prijem/PrijemDialogUspesnehoOdeslaniDavky", _PrijemDialogUspesnehoOdeslaniDavky.ToString()));
                _PrijemDialogOpusteniModulu = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prijem/PrijemDialogOpusteniModulu", _PrijemDialogOpusteniModulu.ToString()));
                _PrijemDialogOpusteniPrijemky = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prijem/PrijemDialogOpusteniPrijemky", _PrijemDialogOpusteniPrijemky.ToString()));
                _PrijemDialogOpusteniZalokovani = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prijem/PrijemDialogOpusteniZalokovani", _PrijemDialogOpusteniZalokovani.ToString()));
				_EtiketaTiskDotazSCenou = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prijem/EtiketaTiskDotazSCenou", _EtiketaTiskDotazSCenou.ToString()));

				_EtiketaTiskDotazSCenou_Cena = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prijem/EtiketaTiskDotazSCenou_Cena", _EtiketaTiskDotazSCenou_Cena.ToString()));
				_EtiketaTiskDotazSCenou_ZobrazDialog = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prijem/EtiketaTiskDotazSCenou_ZobrazDialog", _EtiketaTiskDotazSCenou_ZobrazDialog.ToString()));
                
                #endregion

                _ZobrazovatReport = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prijem/ZobrazovatReport", _ZobrazovatReport.ToString()));
                _MnozstviAutoJedna = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prijem/MnozstviAutoJedna", _MnozstviAutoJedna.ToString()));

                _RozhodovatSkladExpedice = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prijem/RozhodovatSkladExpedice", _RozhodovatSkladExpedice.ToString()));

				_sequenceSSCC = int.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prijem/SequenceSSCC", _sequenceSSCC.ToString()));
				_typOznaceniPalety = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prijem/TypOznaceniPalety", _typOznaceniPalety.ToString()));
				_typyPoctyPalet = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prijem/TypyPoctyPalet", _typyPoctyPalet.ToString()));

                return true;
            }
            catch
            {
                return false;
            }

        }

        public static bool Save(string filename)
        {
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(filename);
                XmlElement confignode = null;

                confignode = xmldoc.SelectSingleNode("/Config/Modules/Prijem/OverFillItem") as XmlElement;
                if (confignode != null)
                {
                    try { confignode.InnerText = _overfillitem.ToString(); }
                    catch { }
                }
                confignode = null;
                confignode = xmldoc.SelectSingleNode("/Config/Modules/Prijem/PocetCislicVlastniSN") as XmlElement;

                if (confignode != null)
                {
                    try { confignode.InnerText = _PocetCislicVlastniSN.ToString(); }
                    catch { }
                }
                confignode = null;

                confignode = xmldoc.SelectSingleNode("/Config/Modules/Prijem/DelkaKontrolySN") as XmlElement;

                if (confignode != null)
                {
                    try { confignode.InnerText = _DelkaKontrolySN.ToString(); }
                    catch { }
                }
                confignode = null;

                confignode = xmldoc.SelectSingleNode("/Config/Modules/Prijem/ZadaniLocncodePredSN") as XmlElement;
                if (confignode != null)
                {
                    try
                    {
                        confignode.InnerText = _ZadaniLocncodePredSN.ToString();
                    }
                    catch { }
                }
                confignode = null;

                confignode = xmldoc.SelectSingleNode("/Config/Modules/Prijem/Datum") as XmlElement;
                if (confignode != null)
                {
                    try { confignode.InnerText = _Datum.ToString(System.Globalization.DateTimeFormatInfo.InvariantInfo); }
                    catch { }
                }
                confignode = null;

                confignode = xmldoc.SelectSingleNode("/Config/Modules/Prijem/PosledniGenSN") as XmlElement;
                if (confignode != null)
                {
                    try { confignode.InnerText = _PosledniGenSN.ToString(); }
                    catch { }
                }
                confignode = null;

                confignode = xmldoc.SelectSingleNode("/Config/Modules/Prijem/MaxDelkaZakazSN") as XmlElement;
                if (confignode != null)
                {
                    try { confignode.InnerText = _MaxDelkaZakazSN.ToString(); }
                    catch { }
                }
                confignode = null;

                confignode = xmldoc.SelectSingleNode("/Config/Modules/Prijem/MaxPocetZakazSN") as XmlElement;
                if (confignode != null)
                {
                    try { confignode.InnerText = _MaxPocetZakazSN.ToString(); }
                    catch { }
                }
                confignode = null;

                MST_Global.SaveElement(xmldoc, "/Config/Modules/Prijem/Rez1Cislo", _Rez1Cislo.ToString());
                //confignode = xmldoc.SelectSingleNode("/Config/Modules/Prijem/Rez1Cislo") as XmlElement;
                //if (confignode != null)
                //{
                //    try { confignode.InnerText = _Rez1Cislo.ToString(); }
                //    catch { }
                //}
                //confignode = null;

                MST_Global.SaveElement(xmldoc, "/Config/Modules/Prijem/Rez2Cislo", _Rez2Cislo.ToString());
                //confignode = xmldoc.SelectSingleNode("/Config/Modules/Prijem/Rez2Cislo") as XmlElement;
                //if (confignode != null)
                //{
                //    try { confignode.InnerText = _Rez2Cislo.ToString(); }
                //    catch { }
                //}
                //confignode = null;

                MST_Global.SaveElement(xmldoc, "/Config/Modules/Prijem/Rez1Povinne", _Rez1Povinne.ToString());
                //confignode = xmldoc.SelectSingleNode("/Config/Modules/Prijem/Rez1Povinne") as XmlElement;
                //if (confignode != null)
                //{
                //    try { confignode.InnerText = _Rez1Povinne.ToString(); }
                //    catch { }
                //}
                //confignode = null;

                MST_Global.SaveElement(xmldoc, "/Config/Modules/Prijem/Rez2Povinne", _Rez2Povinne.ToString());
                //confignode = xmldoc.SelectSingleNode("/Config/Modules/Prijem/Rez2Povinne") as XmlElement;
                //if (confignode != null)
                //{
                //    try { confignode.InnerText = _Rez2Povinne.ToString(); }
                //    catch { }
                //}
                //confignode = null;


                MST_Global.SaveElement(xmldoc, "/Config/Modules/Prijem/Rez1Pamatovat", _Rez1Pamatovat.ToString());
                MST_Global.SaveElement(xmldoc, "/Config/Modules/Prijem/Rez2Pamatovat", _Rez2Pamatovat.ToString());
                MST_Global.SaveElement(xmldoc, "/Config/Modules/Prijem/ZobrazitZaznamu", _ZobrazitZaznamu.ToString());

                MST_Global.SaveElement(xmldoc, "/Config/Modules/Prijem/HledaniCkAutoVyberPrvniNeuplne", _HledaniCkAutoVyberPrvniNeuplne.ToString());
                MST_Global.SaveElement(xmldoc, "/Config/Modules/Prijem/EtiketaTiskPoVlozeniDotaz", _EtiketaTiskPoVlozeniDotaz.ToString());
                MST_Global.SaveElement(xmldoc, "/Config/Modules/Prijem/EtiketyTiskPredOdeslanimDotaz", _EtiketyTiskPredOdeslanimDotaz.ToString());
                MST_Global.SaveElement(xmldoc, "/Config/Modules/Prijem/EtiketyTiskPoOtevreniDotaz", _EtiketyTiskPoOtevreniDotaz.ToString());
				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prijem/EtiketaTisk_PrebiratMnozstvi", _EtiketaTisk_PrebiratMnozstvi.ToString());
				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prijem/EtiketaTisk_DialogTisk", _DialogTisk.ToString());



                MST_Global.SaveElement(xmldoc, "/Config/Modules/Prijem/PolozkyVyberJenScannerem", _PolozkyVyberJenScannerem.ToString());
                MST_Global.SaveElement(xmldoc, "/Config/Modules/Prijem/SlucovaniDavek", _SlucovaniDavek.ToString());
                MST_Global.SaveElement(xmldoc, "/Config/Modules/Prijem/OnlinePohyby", _OnlinePohyby.ToString());
                MST_Global.SaveElement(xmldoc, "/Config/Modules/Prijem/ViceTerminaly", _ViceTerminaly.ToString());
                MST_Global.SaveElement(xmldoc, "/Config/Modules/Prijem/ZmenaDataDokladu", _ZmenaDataDokladu.ToString());
                MST_Global.SaveElement(xmldoc, "/Config/Modules/Prijem/GenerovaniPrikazuPozadovatSklad", _GenerovaniPrikazuPozadovatSklad.ToString());
                MST_Global.SaveElement(xmldoc, "/Config/Modules/Prijem/GenerovaniSarze", _GenerovaniSarze.ToString().ToString());
                MST_Global.SaveElement(xmldoc, "/Config/Modules/Prijem/OverovatLokaci", _OverovatLokaci.ToString().ToString());
                MST_Global.SaveElement(xmldoc, "/Config/Modules/Prijem/PovolitFoceniPriPridaniPolozky", _PovolitFoceniPriPridaniPolozky.ToString());
                MST_Global.SaveElement(xmldoc, "/Config/Modules/Prijem/DavkaOtevritIhnedPoStazeni", _DavkaOtevritIhnedPoStazeni.ToString());
                MST_Global.SaveElement(xmldoc, "/Config/Modules/Prijem/PovolitZalokovani", _PovolitZalokovani.ToString().ToString());
                MST_Global.SaveElement(xmldoc, "/Config/Modules/Prijem/ZalokovaniPozadovatZadaniMnozstvi", _ZalokovaniPozadovatZadaniMnozstvi.ToString().ToString());
                MST_Global.SaveElement(xmldoc, "/Config/Modules/Prijem/DoporuceneLokace", _DoporuceneLokace.ToString().ToString());
                MST_Global.SaveElement(xmldoc, "/Config/Modules/Prijem/KontrolovatSNsPredlohou", _KontrolovatSNsPredlohou.ToString().ToString());
                MST_Global.SaveElement(xmldoc, "/Config/Modules/Prijem/ZobrazitDialogZadaniSN", _ZobrazitDialogZadaniSN.ToString().ToString());
                MST_Global.SaveElement(xmldoc, "/Config/Modules/Prijem/PovolitPrazdnouHodnotuSN", _PovolitPrazdnouHodnotuSN.ToString().ToString());
                MST_Global.SaveElement(xmldoc, "/Config/Modules/Prijem/GenerovatNenalezenouPrijemku", _GenerovatNenalezenouPrijemku.ToString());
                MST_Global.SaveElement(xmldoc, "/Config/Modules/Prijem/KontrolaVyplneniLokaciPredOdeslanim", _KontrolaVyplneniLokaciPredOdeslanim.ToString());
                MST_Global.SaveElement(xmldoc, "/Config/Modules/Prijem/NezrealizovanePrijemky", _NezrealizovanePrijemky.ToString());
                MST_Global.SaveElement(xmldoc, "/Config/Modules/Prijem/OnlineHmotnost", _OnlineHmotnost.ToString());
                MST_Global.SaveElement(xmldoc, "/Config/Modules/Prijem/NerealizovanePrijekyCarKody", _NerealizovanePrijekyCarKody.ToString());
                //Prijem sklady
                MST_Global.SaveElement(xmldoc, "/Config/Modules/Prijem/SkladPouzit", _SkladPouzit.ToString());
                MST_Global.SaveElement(xmldoc, "/Config/Modules/Prijem/SkladID", _SkladID);
                MST_Global.SaveElement(xmldoc, "/Config/Modules/Prijem/PrevzitIDSkladuZCiselnikuSkladu", _PrevzitIDSkladuZCiselnikuSkladu.ToString());
                MST_Global.SaveElement(xmldoc, "/Config/Modules/Prijem/FiltrCiselnikSkladuOnlyOne", _FiltrCiselnikSkladuOnlyOne.ToString());
                MST_Global.SaveElement(xmldoc, "/Config/Modules/Prijem/DoplneniVychoziLokacePovolit", _DoplneniVychoziLokacePovolit.ToString());
                MST_Global.SaveElement(xmldoc, "/Config/Modules/Prijem/ParsovaniCarovehoKoduPovolit", _ParsovaniCarovehoKoduPovolit.ToString());
                MST_Global.SaveElement(xmldoc, "/Config/Modules/Prijem/PovolitLokaceNaDavku", _LokaceNaDavkuPovolit.ToString());
                MST_Global.SaveElement(xmldoc, "/Config/Modules/Prijem/ZobrazitDialogZadaniMnozstviParsovanehoKodu", _zobrazitDialogZadaniMnozstviParsovanehoKodu.ToString());
                MST_Global.SaveElement(xmldoc, "/Config/Modules/Prijem/NacistSkladIDOnline", _nacistSkladIDOnline.ToString());
                MST_Global.SaveElement(xmldoc, "/Config/Modules/Prijem/PovolitZmenuRezimuZadaniLokace", _PovolitZmenuRezimuZadaniLokace.ToString());
                
                #region TaD Dialogy Prijem
                MST_Global.SaveElement(xmldoc, "/Config/Modules/Prijem/PrijemDialogUspesnehoOdeslaniDavky", _PrijemDialogUspesnehoOdeslaniDavky.ToString());                
                MST_Global.SaveElement(xmldoc, "/Config/Modules/Prijem/PrijemDialogOpusteniModulu", _PrijemDialogOpusteniModulu.ToString());
                MST_Global.SaveElement(xmldoc, "/Config/Modules/Prijem/PrijemDialogOpusteniPrijemky", _PrijemDialogOpusteniPrijemky.ToString());
                MST_Global.SaveElement(xmldoc, "/Config/Modules/Prijem/PrijemDialogOpusteniZalokovani", _PrijemDialogOpusteniZalokovani.ToString());
				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prijem/EtiketaTiskDotazSCenou", _EtiketaTiskDotazSCenou.ToString());

				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prijem/EtiketaTiskDotazSCenou_Cena", _EtiketaTiskDotazSCenou_Cena.ToString());
				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prijem/EtiketaTiskDotazSCenou_ZobrazDialog", _EtiketaTiskDotazSCenou_ZobrazDialog.ToString());
                #endregion

                MST_Global.SaveElement(xmldoc, "/Config/Modules/Prijem/ZobrazovatReport", _ZobrazovatReport.ToString());
                MST_Global.SaveElement(xmldoc, "/Config/Modules/Prijem/MnozstviAutoJedna", _MnozstviAutoJedna.ToString());
                MST_Global.SaveElement(xmldoc, "/Config/Modules/Prijem/RozhodovatSkladExpedice", _RozhodovatSkladExpedice.ToString());
				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prijem/SequenceSSCC", _sequenceSSCC.ToString());
				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prijem/TypOznaceniPalety", _typOznaceniPalety.ToString());
				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prijem/TypyPoctyPalet", _typyPoctyPalet.ToString());
				

				

                xmldoc.Save(filename);

                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
