using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Data;

namespace Fask.MST_W.Prodej
{
	public class Globals
	{
		private static bool _PaletyPovolit = false;
		public static bool PaletyPovolit
		{
			get { return _PaletyPovolit; }
			set { _PaletyPovolit = value; }
		}

		private static string adminPwdKey = "pwdAdmin951";
		private static string _hesloEditaceNaplnenaDavka = string.Empty;
		public static string HesloEditaceNaplnenaDavka
		{
			get
			{
				if (string.IsNullOrEmpty(_hesloEditaceNaplnenaDavka))
					return "159";
				return Fask.Encryption.RijndaelWrapper.Decrypt(_hesloEditaceNaplnenaDavka, adminPwdKey);
			}
			set { _hesloEditaceNaplnenaDavka = Fask.Encryption.RijndaelWrapper.Encrypt(value.ToString(), adminPwdKey); }
		}

		private static bool _tiskSoupisuPriUzavreniDavky = false;
		/// <summary>
		/// Zobrazi dotaz na tisk soupisu pri uzavreni davky.
		/// </summary>
		public static bool TiskSoupisuPriUzavreniDavky
		{
			get { return _tiskSoupisuPriUzavreniDavky; }
			set { _tiskSoupisuPriUzavreniDavky = value; }
		}

		private static bool _tiskEtiketyPoPridaniZbozi = false;
		/// <summary>
		/// Zobrazi dotaz na tisk etikety ihned po pridani zbozi.
		/// </summary>
		public static bool TiskEtiketyPoPridaniZbozi
		{
			get { return _tiskEtiketyPoPridaniZbozi; }
			set { _tiskEtiketyPoPridaniZbozi = value; }
		}

		private static bool _EtiketaTisk_PrebiratMnozstvi = false;
		public static bool EtiketaTisk_PrebiratMnozstvi
		{
			get { return _EtiketaTisk_PrebiratMnozstvi; }
			set { _EtiketaTisk_PrebiratMnozstvi = value; }
		}

		


		private static bool _povolitZadaniMnozstviScannerem = true;
		/// <summary>
		/// Povoli zadani mnozstvi pouze scannerem.
		/// </summary>
		public static bool PovolitZadaniMnozstviScannerem
		{
			get { return _povolitZadaniMnozstviScannerem; }
			set { _povolitZadaniMnozstviScannerem = value; }
		}

		private static bool _povolitExportCiselniku = true;
		/// <summary>
		/// Zobrazi dialog dotazu, zdali se ma exportovat ciselnik pred aktualizaci.
		/// </summary>
		public static bool PovolitExportCiselniku
		{
			get { return _povolitExportCiselniku; }
			set { _povolitExportCiselniku = value; }
		}

		#region TaD Prodej Dialogy

		private static bool _ProdejDialogUspesnehoOdeslaniDavky = true;
		public static bool ProdejDialogUspesnehoOdeslaniDavky
		{
			get { return _ProdejDialogUspesnehoOdeslaniDavky; }
			set { _ProdejDialogUspesnehoOdeslaniDavky = value; }
		}


		/// <summary>
		/// Preskoceni dialogu o ukonceni zpracovane davky
		/// </summary>
		private static bool _ProdejDialogUkonceniZpracobaniDavky = true;
		public static bool ProdejDialogUkonceniZpracobaniDavky
		{
			get { return _ProdejDialogUkonceniZpracobaniDavky; }
			set { _ProdejDialogUkonceniZpracobaniDavky = value; }
		}

		/// <summary>
		/// Preskoceni dialogu o smazani davky
		/// </summary>
		private static bool _ProdejDialogSmazaniDavky = true;
		public static bool ProdejDialogSmazaniDavky
		{
			get { return _ProdejDialogSmazaniDavky; }
			set { _ProdejDialogSmazaniDavky = value; }
		}


		/// <summary>
		/// Dialog ktery zobrazi nasnimanou lokaci
		/// </summary>
		private static bool _ProdejDialogNasnimanaLokace = true;
		public static bool ProdejDialogNasnimanaLokace
		{
			get { return _ProdejDialogNasnimanaLokace; }
			set { _ProdejDialogNasnimanaLokace = value; }
		}


		/// <summary>
		/// Preskoceni dialogu o opusteni modulu prodej
		/// </summary>
		private static bool _ProdejDialogOpusteniModulu = true;
		public static bool ProdejDialogOpusteniModulu
		{
			get { return _ProdejDialogOpusteniModulu; }
			set { _ProdejDialogOpusteniModulu = value; }
		}

		/// <summary>
		/// èi se má zapnout logika rozhodovani zda tisknout s cenou anebo bez ceny
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


		private static bool _DialogTisk = true;
		public static bool DialogTisk
		{
			get { return _DialogTisk; }
			set { _DialogTisk = value; }
		}

		

		#endregion

		#region TaD F10 možnosti

		private static bool _F10_OnlinePocetKusuSklad = true;
		public static bool F10_OnlinePocetKusuSklad
		{
			get { return _F10_OnlinePocetKusuSklad; }
			set { _F10_OnlinePocetKusuSklad = value; }
		}

		private static bool _F10_TiskEtiketa = true;
		public static bool F10_TiskEtiketa
		{
			get { return _F10_TiskEtiketa; }
			set { _F10_TiskEtiketa = value; }
		}


		private static bool _F10_ZobrazitAlternativyLokaci = true;
				public static bool F10_ZobrazitAlternativyLokaci
		{
			get { return _F10_ZobrazitAlternativyLokaci; }
			set { _F10_ZobrazitAlternativyLokaci = value; }
		}

		
		
		#endregion

		private static bool _PovolitParsovaniMnozstvi = true;
		public static bool PovolitParsovaniMnozstvi
		{
			get { return _PovolitParsovaniMnozstvi; }
			set { _PovolitParsovaniMnozstvi = value; }
		}

		private static bool _PovolitParsovaniSarze = true;
				public static bool PovolitParsovaniSarze
		{
			get { return _PovolitParsovaniSarze; }
			set { _PovolitParsovaniSarze = value; }
		}




		private static bool _pozadovatHesloProOtevreniRozpracovaneDavky = false;
		/// <summary>
		/// Povoli otevrit jiz naplnenou davku.
		/// </summary>
		public static bool PozadovatHesloProOtevreniRozpracovaneDavky
		{
			get { return _pozadovatHesloProOtevreniRozpracovaneDavky; }
			set { _pozadovatHesloProOtevreniRozpracovaneDavky = value; }
		}

		private static bool _PovolitNovouPolozku = false;
		/// <summary>
		/// Povoli vlozeni nove polozky, pokud nebyla nalezena
		/// </summary>
		public static bool PovolitNovouPolozku
		{
			get { return _PovolitNovouPolozku; }
			set { _PovolitNovouPolozku = value; }
		}

		private static bool _dotazPridatNovaPolozka = true;
		/// <summary>
		/// Povoli vlozeni nove polozky, pokud nebyla nalezena
		/// </summary>
		public static bool DotazPridatNovaPolozka
		{
			get { return _dotazPridatNovaPolozka; }
			set { _dotazPridatNovaPolozka = value; }
		}

		private static bool _FindDefaultMJ = false;
		public static bool FindDefaultMJ
		{
			get { return _FindDefaultMJ; }
			set { _FindDefaultMJ = value; }
		}
		// ovìøovat pohyb
		private static bool _overovatPohyb = false;
		public static bool OverovatPohyb
		{
			get { return _overovatPohyb; }
			set { _overovatPohyb = value; }
		}

		private static bool _overovatZdrojovouLokaci = false;
		public static bool OverovatZdrojovouLokaci
		{
			get { return _overovatZdrojovouLokaci; }
			set { _overovatZdrojovouLokaci = value; }
		}

		private static bool _overovatCilovouLokaci = false;
		public static bool OverovatCilovouLokaci
		{
			get { return _overovatCilovouLokaci; }
			set { _overovatCilovouLokaci = value; }
		}

		private static bool _doporuceneCiloveLokace = false;
		public static bool DoporuceneCiloveLokace
		{
			get { return _doporuceneCiloveLokace; }
			set { _doporuceneCiloveLokace = value; }
		}

		private static bool _doporucenePalety = false;
		public static bool DoporucenePalety
		{
			get { return _doporucenePalety; }
			set { _doporucenePalety = value; }
		}

		private static bool _povolitTiskEtikety = false;
		/// <summary>
		/// Povolit tisk etikety
		/// </summary>
		public static bool PovolitTiskEtikety
		{
			get { return _povolitTiskEtikety; }
			set { _povolitTiskEtikety = value; }
		}

		private static string _predvyplneneMnozstviEtikety = string.Empty;
		/// <summary>
		/// Predvyplnene mnozstvi etikety
		/// </summary>
		public static string PredvyplneneMnozstviEtikety
		{
			get { return _predvyplneneMnozstviEtikety; }
			set { _predvyplneneMnozstviEtikety = value; }
		}

		private static string _predvyplneneMnozstviSoupisu = string.Empty;
		/// <summary>
		/// Predvyplnene mnozstvi soupisu
		/// </summary>
		public static string PredvyplneneMnozstviSoupisu
		{
			get { return _predvyplneneMnozstviSoupisu; }
			set { _predvyplneneMnozstviSoupisu = value; }
		}

		private static bool _povolitTiskSoupisu = false;
		/// <summary>
		/// Povolit tisk soupisu
		/// </summary>
		public static bool PovolitTiskSoupisu
		{
			get { return _povolitTiskSoupisu; }
			set { _povolitTiskSoupisu = value; }
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

		/// <summary>
		/// Povolení pøevodu mezi sklady.
		/// </summary>
		private static bool _povolitPrevodMeziSklady = false;
		public static bool PovolitPrevodMeziSklady
		{
			get { return _povolitPrevodMeziSklady; }
			set { _povolitPrevodMeziSklady = value; }
		}

		/// <summary>
		/// Povolení prace s cenami (Helios, ...).
		/// </summary>
		private static bool _povolitCeny = true;
		public static bool PovolitCeny
		{
			get { return _povolitCeny; }
			set { _povolitCeny = value; }
		}

		//MnozstviREZ1Vypln
		private static bool _MnozstviREZ1Vypln = false;
		public static bool MnozstviREZ1Vypln
		{
			get { return _MnozstviREZ1Vypln; }
			set { _MnozstviREZ1Vypln = value; }
		}

		private static bool _Mnozstvi1Auto = false;
		public static bool Mnozstvi1Auto
		{
			get { return _Mnozstvi1Auto; }
			set { _Mnozstvi1Auto = value; }
		}

		private static bool _ZobrazovatReport = false;
		public static bool ZobrazovatReport
		{
			get { return _ZobrazovatReport; }
			set { _ZobrazovatReport = value; }
		}

		

		private static bool _FiltrDodavatele = false;
		public static bool FiltrDodavatele
		{
			get { return _FiltrDodavatele; }
			set { _FiltrDodavatele = value; }
		}

		private static bool _FiltrCiselnikSkladuOnlyOne = false;
		public static bool FiltrCiselnikSkladuOnlyOne
		{
			get { return _FiltrCiselnikSkladuOnlyOne; }
			set { _FiltrCiselnikSkladuOnlyOne = value; }
		}

		private static bool _OnlinePocetKusuSklad = true;
		public static bool OnlinePocetKusuSklad
		{
			get { return _OnlinePocetKusuSklad; }
			set { _OnlinePocetKusuSklad = value; }
		}

		private static bool _OnlinePocetKusu = true;
		public static bool OnlinePocetKusu
		{
			get { return _OnlinePocetKusu; }
			set { _OnlinePocetKusu = value; }
		}

		private static bool _OnlineDetailPolozka = true;
		public static bool OnlineDetailPolozka
		{
			get { return _OnlineDetailPolozka; }
			set { _OnlineDetailPolozka = value; }
		}

		private static bool _FiltrCiselnikSkladu = false;
		public static bool FiltrCiselnikSkladu
		{
			get { return _FiltrCiselnikSkladu; }
			set { _FiltrCiselnikSkladu = value; }
		}

		private static bool _PouzitSklady = false;
		public static bool PouzitSklady
		{
			get { return _PouzitSklady; }
			set { _PouzitSklady = value; }
		}

		private static bool _ZobrazovatListPolozek = true;
		public static bool ZobrazovatListPolozek
		{
			get { return _ZobrazovatListPolozek; }
			set { _ZobrazovatListPolozek = value; }
		}

		private static bool _FiltrCiselnikMen = true;
		public static bool FiltrCiselnikMen
		{
			get { return _FiltrCiselnikMen; }
			set { _FiltrCiselnikMen = value; }
		}

		private static bool _Rez4Cislo = true;
		public static bool Rez4Cislo
		{
			get { return _Rez4Cislo; }
			set { _Rez4Cislo = value; }
		}

		private static bool _Rez3Cislo = true;
		public static bool Rez3Cislo
		{
			get { return _Rez3Cislo; }
			set { _Rez3Cislo = value; }
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

		private static bool _Rez4Povinne = true;
		public static bool Rez4Povinne
		{
			get { return _Rez4Povinne; }
			set { _Rez4Povinne = value; }
		}

		private static bool _Rez3Povinne = true;
		public static bool Rez3Povinne
		{
			get { return _Rez3Povinne; }
			set { _Rez3Povinne = value; }
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

		private static bool _Rez4Pamatovat = true;
		public static bool Rez4Pamatovat
		{
			get { return _Rez4Pamatovat; }
			set { _Rez4Pamatovat = value; }
		}

		private static bool _Rez3Pamatovat = true;
		public static bool Rez3Pamatovat
		{
			get { return _Rez3Pamatovat; }
			set { _Rez3Pamatovat = value; }
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

		private static bool _StrediskoJednoNaDavku = false;
		public static bool StrediskoJednoNaDavku
		{
			get { return _StrediskoJednoNaDavku; }
			set { _StrediskoJednoNaDavku = value; }
		}

		private static bool _StrediskoKPolozce = true;
		public static bool StrediskoKPolozce
		{
			get { return _StrediskoKPolozce; }
			set { _StrediskoKPolozce = value; }
		}

		private static bool _StrediskoText = true;
		public static bool StrediskoText
		{
			get { return _StrediskoText; }
			set { _StrediskoText = value; }
		}

		private static bool _PracovniciJedenNaDavku = false;
		public static bool PracovniciJedenNaDavku
		{
			get { return _PracovniciJedenNaDavku; }
			set { _PracovniciJedenNaDavku = value; }
		}

		private static bool _PracovniciKPolozce = true;
		public static bool PracovniciKPolozce
		{
			get { return _PracovniciKPolozce; }
			set { _PracovniciKPolozce = value; }
		}

		private static bool _PracovniciText = true;
		public static bool PracovniciText
		{
			get { return _PracovniciText; }
			set { _PracovniciText = value; }
		}

		private static bool _PracovniciVyberJenScannerem = true;
		public static bool PracovniciVyberJenScannerem
		{
			get { return _PracovniciVyberJenScannerem; }
			set { _PracovniciVyberJenScannerem = value; }
		}

		private static bool _PolozkyVyberJenScannerem = true;
		public static bool PolozkyVyberJenScannerem
		{
			get { return _PolozkyVyberJenScannerem; }
			set { _PolozkyVyberJenScannerem = value; }
		}

		private static bool _NacistNazevSkladuPriVyhledavani = true;
		public static bool NacistNazevSkladuPriVyhledavani
		{
			get { return _NacistNazevSkladuPriVyhledavani; }
			set { _NacistNazevSkladuPriVyhledavani = value; }
		}

		private static bool _PolozkyVyhledatPomociSarze = false;
		public static bool PolozkyVyhledatPomociSarze
		{
			get { return _PolozkyVyhledatPomociSarze; }
			set { _PolozkyVyhledatPomociSarze = value; }
		}

		private static bool _AktualizaceZboziPredVyberemDavky = false;
		public static bool AktualizaceZboziPredVyberemDavky
		{
			get { return _AktualizaceZboziPredVyberemDavky; }
			set { _AktualizaceZboziPredVyberemDavky = value; }
		}

		private static bool _KontrolaStavuSkladu = true;
		public static bool KontrolaStavuSkladu
		{
			get { return _KontrolaStavuSkladu; }
			set { _KontrolaStavuSkladu = value; }
		}

		private static bool _prodejZadaniLocncodePredSN = true;
		public static bool prodejZadaniLocncodePredSN
		{
			get { return _prodejZadaniLocncodePredSN; }
			set { _prodejZadaniLocncodePredSN = value; }
		}

		private static bool _prodejPovolitZadaniLocncode = true;
		public static bool prodejPovolitZadaniLocncode
		{
			get { return _prodejPovolitZadaniLocncode; }
			set { _prodejPovolitZadaniLocncode = value; }
		}

		private static bool _OneOdberatelAutoSelect = true;
		public static bool OneOdberatelAutoSelect
		{
			get { return _OneOdberatelAutoSelect; }
			set { _OneOdberatelAutoSelect = value; }
		}

		private static int _NextNumber;
		public static int NextNumber
		{
			get { return _NextNumber; }
			set { _NextNumber = value; }
		}

		private static int _Prefix;
		public static int Prefix
		{
			get { return _Prefix; }
			set { _Prefix = value; }
		}

		private static int _GridViewRowCount = 8;
		public static int GridViewRowCount
		{
			get { return _GridViewRowCount; }
			set { _GridViewRowCount = value; }
		}

		private static bool _PriceIsWithTax = true;
		public static bool PriceIsWithTax
		{
			get { return _PriceIsWithTax; }
			set { _PriceIsWithTax = value; }
		}

		private static bool _PriceIsWithTaxEnable = true;
		public static bool PriceIsWithTaxEnable
		{
			get { return _PriceIsWithTaxEnable; }
			set { _PriceIsWithTaxEnable = value; }
		}

		private static bool _Price0IsWithTax = true;
		public static bool Price0IsWithTax
		{
			get { return _Price0IsWithTax; }
			set { _Price0IsWithTax = value; }
		}

		private static bool _Price1IsWithTax = true;
		public static bool Price1IsWithTax
		{
			get { return _Price1IsWithTax; }
			set { _Price1IsWithTax = value; }
		}

		private static bool _Price2IsWithTax = true;
		public static bool Price2IsWithTax
		{
			get { return _Price2IsWithTax; }
			set { _Price2IsWithTax = value; }
		}

		private static bool _Price3IsWithTax = true;
		public static bool Price3IsWithTax
		{
			get { return _Price3IsWithTax; }
			set { _Price3IsWithTax = value; }
		}

		private static bool _Price4IsWithTax = true;
		public static bool Price4IsWithTax
		{
			get { return _Price4IsWithTax; }
			set { _Price4IsWithTax = value; }
		}

		private static bool _Price5IsWithTax = true;
		public static bool Price5IsWithTax
		{
			get { return _Price5IsWithTax; }
			set { _Price5IsWithTax = value; }
		}

		/// <summary>
		/// Kontrola existence nactene polozky v DI pomoci CzCarKod
		/// </summary>
		private static bool _ExistenceNasnimanePolozky = true;
		public static bool ExistenceNasnimanePolozky
		{
			get { return _ExistenceNasnimanePolozky; }
			set { _ExistenceNasnimanePolozky = value; }
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

		private static bool _RangeEnable;
		public static bool RangeEnable
		{
			get { return _RangeEnable; }
			set { _RangeEnable = value; }
		}

		private static bool _TypDokladu;
		/// <summary>
		/// Urcuje, zda ma modul pracovat s ciselnikem typu dokladu
		/// </summary>
		public static bool TypDokladu
		{
			get { return _TypDokladu; }
			set { _TypDokladu = value; }
		}

		private static bool _RucniVolbaOdberatele;
		/// <summary>
		/// Urcuje, zda je moznost vybirat rucne odberatele, pomoci klavesy Enter
		/// </summary>
		public static bool RucniVolbaOdberatele
		{
			get { return _RucniVolbaOdberatele; }
			set { _RucniVolbaOdberatele = value; }
		}

		private static bool _Strediska;
		/// <summary>
		/// Urcuje zda ma modul pro polozku vyzadovat i zadani typu strediska
		/// </summary>
		public static bool Strediska
		{
			get { return _Strediska; }
			set { _Strediska = value; }
		}

		private static bool _Pracovnici;
		/// <summary>
		/// Urcuje zda ma modul pro polozku vyzadovat i zadani pracovnika
		/// </summary>
		public static bool Pracovnici
		{
			get { return _Pracovnici; }
			set { _Pracovnici = value; }
		}

		private static string _SkladID;
		/// <summary>
		/// Ciselny identifikator id skladu
		/// </summary>
		public static string SkladID
		{
			get { return _SkladID; }
			set { _SkladID = value; }
		}


		private static bool _DisponibilityCheck;
		/// <summary>
		/// Urcuje, zda se ma v prodeji provest test na disponibilitu vybraneho zbozi
		/// Klicem je cislo polozky + sklad na kterem je polozka vedena ( + odberatel ???)
		/// </summary>
		public static bool DisponibilityCheck
		{
			get { return _DisponibilityCheck; }
			set { _DisponibilityCheck = value; }
		}

		private static bool _DisponibilityHlaska;
		public static bool DisponibilityHlaska
		{
			get { return _DisponibilityHlaska; }
			set { _DisponibilityHlaska = value; }
		}

		private static bool _DisponibilityZvuk;
		public static bool DisponibilityZvuk
		{
			get { return _DisponibilityZvuk; }
			set { _DisponibilityZvuk = value; }
		}

		private static bool _Odberatel;
		/// <summary>
		/// Bude se dotahovat odberatel ?
		/// </summary>
		public static bool Odberatel
		{
			get { return _Odberatel; }
			set { _Odberatel = value; }
		}

		private static bool _OneOdberatelOnly;
		/// <summary>
		/// Pro davku bude povolen pouze jeden odberatel
		/// </summary>
		public static bool OneOdberatelOnly
		{
			get { return _OneOdberatelOnly; }
			set { _OneOdberatelOnly = value; }
		}


		private static bool _vnditnum2serltnum;
		/// <summary>
		/// Automaticky do serioveho cisla vkladat hodnotu z vnditnum
		/// </summary>
		public static bool Vnditnum2Serltnum
		{
			get { return _vnditnum2serltnum; }
			set { _vnditnum2serltnum = value; }
		}



		public static bool PriceXIsWithTax(string typOdberatele)
		{
			string otyp = typOdberatele.Trim();
			if (otyp == "0") return Price0IsWithTax;
			else if (otyp == "1") return Price1IsWithTax;
			else if (otyp == "2") return Price2IsWithTax;
			else if (otyp == "3") return Price3IsWithTax;
			else if (otyp == "4") return Price4IsWithTax;
			else if (otyp == "5") return Price5IsWithTax;
			else return Price0IsWithTax;
		}

		public static byte PriceX(string typOdberatele)
		{
			string otyp = typOdberatele.Trim();
			if (otyp == "0") return 0;
			else if (otyp == "1") return 1;
			else if (otyp == "2") return 2;
			else if (otyp == "3") return 3;
			else if (otyp == "4") return 4;
			else if (otyp == "5") return 5;
			else return 0;
		}


		public static void zjisti_cenu(
			Fask.SQLiteDBs.DataSets.Zbozi.CZMST095Row zbozi,
			Fask.SQLiteDBs.DataSets.Odberatele.CZMST090Row odberatel,
			Fask.SQLiteDBs.DataSets.Meny.CZMST097Row mena,
			//ref decimal cenasdani, 
			//ref decimal cenabezdane, 
			//ref decimal cenadan, 
			//ref bool jecenasdani, 
			//ref byte cenovahladina
			Classes.Price price
			)
		{
            // pokud je odberatel null, tak nelze zjistit cenu ... 
            if (odberatel == null)
                return;

            //using (Fask.SQLiteDBs.DataSets.ZboziTableAdapters.CZMST095MTableAdapter ta_zM = new Fask.SQLiteDBs.DataSets.ZboziTableAdapters.CZMST095MTableAdapter())
            //{
                //ta_zM.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + Main.CiselnikZboziDB);

                try
                {
                    // zjisti cenu
                    decimal cena = 0;
                    decimal? cenaM = null;

                    try { cena = (decimal)zbozi["PRICE" + odberatel.odb_typ.Trim()]; }
                    catch { }
                    //zjisti pouzitou cenovou hladinu
                    price.cenovaHladina = PriceX(odberatel.odb_typ);
                    //zjisti zda je cena uvedena s dani nebo bez dane
                    price.jeCenaSDani = PriceXIsWithTax(odberatel.odb_typ);
                    if (zbozi.IsMENA_IDNull())
                        price.mena = null;
                    else
                        price.mena = zbozi.MENA_ID.Trim();

                    if (mena == null && odberatel == null)
                        price.menaM = null;

                    if (odberatel != null && !odberatel.Ismena_IDNull() && !String.IsNullOrEmpty(odberatel.mena_ID.Trim()))
                        price.menaM = odberatel.mena_ID.Trim();

                    if (mena != null && !String.IsNullOrEmpty(mena.mena_ID.Trim()))
                        price.menaM = mena.mena_ID.Trim();

                    if (price.menaM != null)
                    {
                        //cenaM = ta_zM.GetPrice(zbozi.ITEMNMBR.Trim(), price.menaM, price.cenovaHladina);

						//TaD 18.3.2020 pøedelano

						using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Zbozi z = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Zbozi(Main.CiselnikZboziDB))
						{
							cenaM = z.GetPrice(zbozi.ITEMNMBR.Trim(), price.menaM, price.cenovaHladina);
							
						}


                        // TODO : Pokud neni nalezena cena zbozi, tak prepocitat kurzem? respektive co udelat???
                        // momentalne se neulozi cenaM, respektive bude prazdne ... 
                    }

                    if (!price.jeCenaSDani)
                    {// v predloze je cena bez dane
                        //cenabezdane = cena;
                        //cenasdani = cena * (1 + _zbozi.TAXRATE / 100);
                        //cenadan = cenasdani - cenabezdane;
                        price.cenaBezDane = cena;
                        price.cenaDan = Math.Round(cena * (Math.Round(zbozi.TAXRATE / 100, 4)), 2);
                        price.cenaSDani = cena + price.cenaDan;

                        if (cenaM.HasValue)
                        {
                            price.cenaBezDaneM = cenaM;
                            price.cenaDanM = Math.Round(cenaM.Value * (Math.Round(zbozi.TAXRATE / 100, 4)), 2);
                            price.cenaSDaniM = cenaM + price.cenaDanM;
                        }
                    }
                    else
                    {// v predloze je cena i s dani
                        //cenasdani = cena;
                        //cenabezdane = cena / (1 + _zbozi.TAXRATE / 100);
                        //cenadan = cenasdani - cenabezdane;
                        price.cenaSDani = cena;
                        price.cenaDan = Math.Round(cena * Math.Round((zbozi.TAXRATE / (100 + zbozi.TAXRATE)), 4), 2);
                        price.cenaBezDane = cena - price.cenaDan;

                        if (cenaM.HasValue)
                        {
                            price.cenaSDaniM = cenaM;
                            price.cenaDanM = Math.Round(cenaM.Value * Math.Round((zbozi.TAXRATE / (100 + zbozi.TAXRATE)), 4), 2);
                            price.cenaBezDaneM = cenaM - price.cenaDanM;
                        }
                    }

                }
                catch (Exception ex)
                {
                    Logging.Log.Write(ex.Message + "\n" + ex.StackTrace, "Prodej.Globals.zjisti_cenu");
                }
            //}
		}

		public static void nastav_cenu(
			Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIRow di,
			//decimal cenasdani, 
			//decimal cenabezdane, 
			//decimal cenadan, 
			//bool jecenasdani, 
			//byte cenovahladina
			Classes.Price price
			)
		{
			// TODO : upravit nastaveni ceny pro cizi Meny ... czmst095M

			di.PRICEX = price.cenovaHladina;
			di.TAXAMPIE = price.cenaDan;

			if (price.mena != null)
				di.mena_ID = price.mena;
			else
				di.Setmena_IDNull();

			if (price.menaM != null)
				di.mena_IDM = price.menaM;
			else
				di.Setmena_IDMNull();

			if (price.cenaDanM.HasValue)
				di.TAXAMPIEM = price.cenaDanM.Value;
			else
				di.SetTAXAMPIEMNull();

			if (Globals.PriceIsWithTaxEnable)
			{
				if (!Globals.PriceIsWithTax)
				{// ve vystupu je cena bez dane
					di.AMOUNPIE = price.cenaBezDane;
					if (price.cenaBezDaneM.HasValue)
						di.AMOUNPIEM = price.cenaBezDaneM.Value;
					else
						di.SetAMOUNPIEMNull();
					di.WITHTAX = 0;
				}
				else
				{// ve vystupu je cena i s dani
					di.AMOUNPIE = price.cenaSDani;
					if (price.cenaSDaniM.HasValue)
						di.AMOUNPIEM = price.cenaSDaniM.Value;
					else
						di.SetAMOUNPIEMNull();
					di.WITHTAX = 1;
				}
			}
			else
			{
				if (price.jeCenaSDani)
				{
					di.AMOUNPIE = price.cenaSDani;
					if (price.cenaSDaniM.HasValue)
						di.AMOUNPIEM = price.cenaSDaniM.Value;
					else
						di.SetAMOUNPIEMNull();
					di.WITHTAX = 1;
				}
				else
				{
					di.AMOUNPIE = price.cenaBezDane;
					if (price.cenaBezDaneM.HasValue)
						di.AMOUNPIEM = price.cenaBezDaneM.Value;
					else
						di.SetAMOUNPIEMNull();
					di.WITHTAX = 0;
				}
			}
		}


		public static bool Load(string filename)
		{
			try
			{
				XmlDocument xmldoc = new XmlDocument();
				xmldoc.Load(filename);
				XmlElement prodejnode = null;
				prodejnode = null;

				_StrediskoText = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/StrediskoText", _StrediskoText.ToString()));
				_pozadovatHesloProOtevreniRozpracovaneDavky = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/PozadovatHesloProOtevreniRozpracovaneDavky", _pozadovatHesloProOtevreniRozpracovaneDavky.ToString()));
				_povolitZadaniMnozstviScannerem = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/PovolitZadaniMnozstviScannerem", _povolitZadaniMnozstviScannerem.ToString()));
				_hesloEditaceNaplnenaDavka = MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/HesloEditaceNaplnenaDavka", _hesloEditaceNaplnenaDavka.ToString());
				_tiskSoupisuPriUzavreniDavky = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/TiskSoupisuPriUzavreniDavky", _tiskSoupisuPriUzavreniDavky.ToString()));
				_tiskEtiketyPoPridaniZbozi = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/TiskEtiketyPoPridaniZbozi", _tiskEtiketyPoPridaniZbozi.ToString()));

				_EtiketaTisk_PrebiratMnozstvi = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/EtiketaTisk_PrebiratMnozstvi", _EtiketaTisk_PrebiratMnozstvi.ToString()));

				

				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/StrediskoKPolozce") as XmlElement;
				if (prodejnode != null)
				{
					try { _StrediskoKPolozce = bool.Parse(prodejnode.InnerText); }
					catch { }
				}

				_StrediskoJednoNaDavku = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/StrediskoJednoNaDavku", _StrediskoJednoNaDavku.ToString()));


				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/KontrolaStavuSkladu") as XmlElement;
				if (prodejnode != null)
				{
					try { _KontrolaStavuSkladu = bool.Parse(prodejnode.InnerText); }
					catch { }
				}

				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/PriceIsWithTax") as XmlElement;
				if (prodejnode != null)
				{
					try { _PriceIsWithTax = bool.Parse(prodejnode.InnerText); }
					catch { }
				}
				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/PriceIsWithTaxEnable") as XmlElement;
				if (prodejnode != null)
				{
					try { _PriceIsWithTaxEnable = bool.Parse(prodejnode.InnerText); }
					catch { }
				}
				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/Price0IsWithTax") as XmlElement;
				if (prodejnode != null)
				{
					try { _Price0IsWithTax = bool.Parse(prodejnode.InnerText); }
					catch { }
				}
				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/Price1IsWithTax") as XmlElement;
				if (prodejnode != null)
				{
					try { _Price1IsWithTax = bool.Parse(prodejnode.InnerText); }
					catch { }
				}
				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/Price2IsWithTax") as XmlElement;
				if (prodejnode != null)
				{
					try { _Price2IsWithTax = bool.Parse(prodejnode.InnerText); }
					catch { }
				}
				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/Price3IsWithTax") as XmlElement;
				if (prodejnode != null)
				{
					try { _Price3IsWithTax = bool.Parse(prodejnode.InnerText); }
					catch { }
				}
				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/Price4IsWithTax") as XmlElement;
				if (prodejnode != null)
				{
					try { _Price4IsWithTax = bool.Parse(prodejnode.InnerText); }
					catch { }
				}
				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/Price5IsWithTax") as XmlElement;
				if (prodejnode != null)
				{
					try { _Price5IsWithTax = bool.Parse(prodejnode.InnerText); }
					catch { }
				}

				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/OnlinePocetKusuSklad") as XmlElement;
				if (prodejnode != null)
				{
					try { _OnlinePocetKusuSklad = bool.Parse(prodejnode.InnerText); }
					catch { }
				}

				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/OnlinePocetKusu") as XmlElement;
				if (prodejnode != null)
				{
					try { _OnlinePocetKusu = bool.Parse(prodejnode.InnerText); }
					catch { }
				}

				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/GridViewRowCount") as XmlElement;
				if (prodejnode != null)
				{
					try { GridViewRowCount = int.Parse(prodejnode.InnerText); }
					catch { }
				}

				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/OnlineDetailPolozka") as XmlElement;
				if (prodejnode != null)
				{
					try { _OnlineDetailPolozka = bool.Parse(prodejnode.InnerText); }
					catch { }
				}

				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/OneOdberatelAutoSelect") as XmlElement;
				if (prodejnode != null)
				{
					try { _OneOdberatelAutoSelect = bool.Parse(prodejnode.InnerText); }
					catch { }
				}

				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/ZadaniLocncodePredSN") as XmlElement;
				if (prodejnode != null)
				{
					try { _prodejZadaniLocncodePredSN = bool.Parse(prodejnode.InnerText); }
					catch { }
				}

				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/PovolitZadaniLocncode") as XmlElement;
				if (prodejnode != null)
				{
					try { _prodejPovolitZadaniLocncode = bool.Parse(prodejnode.InnerText); }
					catch { }
				}

				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/RangeEnable") as XmlElement;
				if (prodejnode != null)
				{
					try { _RangeEnable = bool.Parse(prodejnode.InnerText); }
					catch { }
				}

				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/Range") as XmlElement;
				if (prodejnode != null)
				{
					try { NextNumber = Convert.ToInt32(prodejnode.Attributes["Next"].Value); }
					catch { }
				}

				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/Range") as XmlElement;
				if (prodejnode != null)
				{
					try { Prefix = Convert.ToInt32(prodejnode.Attributes["Prefix"].Value); }
					catch { }
				}

				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/TypDokladu") as XmlElement;
				if (prodejnode != null)
				{
					try { _TypDokladu = bool.Parse(prodejnode.InnerText); }
					catch { }
				}

				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/RucniVolbaOdberatele") as XmlElement;
				if (prodejnode != null)
				{
					try { _RucniVolbaOdberatele = bool.Parse(prodejnode.InnerText); }
					catch { }
				}

				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/Strediska") as XmlElement;
				if (prodejnode != null)
				{
					try { _Strediska = bool.Parse(prodejnode.InnerText); }
					catch { }
				}


				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/SkladID") as XmlElement;
				if (prodejnode != null)
				{
					try { _SkladID = prodejnode.InnerText; }
					catch { }
				}

				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/DisponibilityCheck") as XmlElement;
				if (prodejnode != null)
				{
					try { _DisponibilityCheck = bool.Parse(prodejnode.InnerText); }
					catch { }
				}

				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/DisponibilityHlaska") as XmlElement;
				if (prodejnode != null)
				{
					try { _DisponibilityHlaska = bool.Parse(prodejnode.InnerText); }
					catch { }
				}

				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/DisponibilityZvuk") as XmlElement;
				if (prodejnode != null)
				{
					try { _DisponibilityZvuk = bool.Parse(prodejnode.InnerText); }
					catch { }
				}

				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/Odberatel") as XmlElement;
				if (prodejnode != null)
				{
					try { _Odberatel = bool.Parse(prodejnode.InnerText); }
					catch { }
				}

				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/Vnditnum2Serltnum") as XmlElement;
				if (prodejnode != null)
				{
					try { _vnditnum2serltnum = bool.Parse(prodejnode.InnerText); }
					catch { }
				}

				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/OneOdberatelOnly") as XmlElement;
				if (prodejnode != null)
				{
					try { _OneOdberatelOnly = bool.Parse(prodejnode.InnerText); }
					catch { }
				}

				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/Rez1Cislo") as XmlElement;
				if (prodejnode != null)
				{
					try { _Rez1Cislo = bool.Parse(prodejnode.InnerText); }
					catch { }
				}

				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/Rez2Cislo") as XmlElement;
				if (prodejnode != null)
				{
					try { _Rez2Cislo = bool.Parse(prodejnode.InnerText); }
					catch { }
				}

				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/Rez3Cislo") as XmlElement;
				if (prodejnode != null)
				{
					try { _Rez3Cislo = bool.Parse(prodejnode.InnerText); }
					catch { }
				}

				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/Rez4Cislo") as XmlElement;
				if (prodejnode != null)
				{
					try { _Rez4Cislo = bool.Parse(prodejnode.InnerText); }
					catch { }
				}

				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/Rez1Povinne") as XmlElement;
				if (prodejnode != null)
				{
					try { _Rez1Povinne = bool.Parse(prodejnode.InnerText); }
					catch { }
				}

				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/Rez2Povinne") as XmlElement;
				if (prodejnode != null)
				{
					try { _Rez2Povinne = bool.Parse(prodejnode.InnerText); }
					catch { }
				}

				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/Rez3Povinne") as XmlElement;
				if (prodejnode != null)
				{
					try { _Rez3Povinne = bool.Parse(prodejnode.InnerText); }
					catch { }
				}

				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/Rez4Povinne") as XmlElement;
				if (prodejnode != null)
				{
					try { _Rez4Povinne = bool.Parse(prodejnode.InnerText); }
					catch { }
				}

				_Rez1Pamatovat = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/Rez1Pamatovat", _Rez1Pamatovat.ToString()));
				_Rez2Pamatovat = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/Rez2Pamatovat", _Rez2Pamatovat.ToString()));
				_Rez3Pamatovat = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/Rez3Pamatovat", _Rez3Pamatovat.ToString()));
				_Rez4Pamatovat = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/Rez4Pamatovat", _Rez4Pamatovat.ToString()));

				_FiltrCiselnikSkladu = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/FiltrCiselnikSkladu", _FiltrCiselnikSkladu.ToString()));
				_PouzitSklady = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/PouzitSklady", _PouzitSklady.ToString()));

				_FiltrCiselnikSkladuOnlyOne = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/FiltrCiselnikSkladuOnlyOne", _FiltrCiselnikSkladuOnlyOne.ToString()));

				_ZobrazovatListPolozek = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/ZobrazovatListPolozek", _ZobrazovatListPolozek.ToString()));
				_Mnozstvi1Auto = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/Mnozstvi1Auto", _Mnozstvi1Auto.ToString()));
				_ZobrazovatReport = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/ZobrazovatReport", _ZobrazovatReport.ToString()));
				_MnozstviREZ1Vypln = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/MnozstviREZ1Vypln", _MnozstviREZ1Vypln.ToString()));

				_FindDefaultMJ = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/FindDefaultMJ", _FindDefaultMJ.ToString()));

				_Pracovnici = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/Pracovnici", _Pracovnici.ToString()));
				_PracovniciJedenNaDavku = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/PracovniciJedenNaDavku", _PracovniciJedenNaDavku.ToString()));
				_PracovniciKPolozce = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/PracovniciKPolozce", _PracovniciKPolozce.ToString()));
				_PracovniciText = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/PracovniciText", _PracovniciText.ToString()));
				_PracovniciVyberJenScannerem = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/PracovniciVyberJenScannerem", _PracovniciVyberJenScannerem.ToString()));
				_PolozkyVyberJenScannerem = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/PolozkyVyberJenScannerem", _PolozkyVyberJenScannerem.ToString()));
				_PolozkyVyhledatPomociSarze = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/PolozkyVyhledatPomociSarze", _PolozkyVyhledatPomociSarze.ToString()));
				_AktualizaceZboziPredVyberemDavky = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/AktualizaceZboziPredVyberemDavky", _AktualizaceZboziPredVyberemDavky.ToString()));

				_PovolitNovouPolozku = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/PovolitNovouPolozku", _PovolitNovouPolozku.ToString()));
				_PaletyPovolit = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/PaletyPovolit", _PaletyPovolit.ToString()));
				_dotazPridatNovaPolozka = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/DotazPridatNovaPolozka", _dotazPridatNovaPolozka.ToString()));
				_FiltrDodavatele = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/FiltrDodavatele", _FiltrDodavatele.ToString()));

				#region TaD Prodej Dialogy

				_ProdejDialogUspesnehoOdeslaniDavky = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/ProdejDialogUspesnehoOdeslaniDavky", _ProdejDialogUspesnehoOdeslaniDavky.ToString()));
				_ProdejDialogUkonceniZpracobaniDavky = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/ProdejDialogUkonceniZpracobaniDavky", _ProdejDialogUkonceniZpracobaniDavky.ToString()));
				_ProdejDialogSmazaniDavky = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/ProdejDialogSmazaniDavky", _ProdejDialogSmazaniDavky.ToString()));
				_ProdejDialogOpusteniModulu = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/ProdejDialogOpusteniModulu", _ProdejDialogOpusteniModulu.ToString()));
				_ProdejDialogNasnimanaLokace = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/ProdejDialogNasnimanaLokace", _ProdejDialogNasnimanaLokace.ToString()));
				_EtiketaTiskDotazSCenou = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/EtiketaTiskDotazSCenou", _EtiketaTiskDotazSCenou.ToString()));

				_EtiketaTiskDotazSCenou_Cena = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/EtiketaTiskDotazSCenou_Cena", _EtiketaTiskDotazSCenou_Cena.ToString()));
				_EtiketaTiskDotazSCenou_ZobrazDialog = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/EtiketaTiskDotazSCenou_ZobrazDialog", _EtiketaTiskDotazSCenou_ZobrazDialog.ToString()));

				_DialogTisk = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/DialogTisk", _DialogTisk.ToString()));
				#endregion

				#region F10 možnosti

				_F10_OnlinePocetKusuSklad = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/F10_OnlinePocetKusuSklad", _F10_OnlinePocetKusuSklad.ToString()));
				_F10_ZobrazitAlternativyLokaci = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/F10_ZobrazitAlternativyLokaci", _F10_ZobrazitAlternativyLokaci.ToString()));
				_F10_TiskEtiketa = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/F10_TiskEtiketa", _F10_TiskEtiketa.ToString()));

				
				#endregion

				_PovolitParsovaniMnozstvi = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/PovolitParsovaniMnozstvi", _PovolitParsovaniMnozstvi.ToString()));
				_PovolitParsovaniSarze = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/PovolitParsovaniSarze", _PovolitParsovaniSarze.ToString()));

				_ExistenceNasnimanePolozky = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/ExistenceNasnimanePolozky", _ExistenceNasnimanePolozky.ToString()));


				_overovatPohyb = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/OverovatPohyb", _overovatPohyb.ToString()));
				_overovatZdrojovouLokaci = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/OverovatZdrojovouLokaci", _overovatZdrojovouLokaci.ToString()));
				_overovatCilovouLokaci = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/OverovatCilovouLokaci", _overovatCilovouLokaci.ToString()));
				_doporuceneCiloveLokace = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/DoporuceneCiloveLokace", _doporuceneCiloveLokace.ToString()));
				_povolitTiskEtikety = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/PovolitTisk", _povolitTiskEtikety.ToString()));
				_doporucenePalety = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/DoporucenePalety", _doporucenePalety.ToString()));
				_predvyplneneMnozstviEtikety = MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/PredvyplneneMnozstviTiskuEtikety", _predvyplneneMnozstviEtikety.ToString());
				_predvyplneneMnozstviSoupisu = MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/PredvyplneneMnozstviTiskuSoupisu", _predvyplneneMnozstviSoupisu.ToString());
				_povolitTiskSoupisu = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/PovolitTiskSoupisu", _povolitTiskSoupisu.ToString()));
				_povolitPrevodMeziSklady = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/PovolitPrevodMeziSklady", _povolitPrevodMeziSklady.ToString()));
				_zobrazitDialogZadaniMnozstviParsovanehoKodu = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/ZobrazitDialogZadaniMnozstviParsovanehoKodu", _zobrazitDialogZadaniMnozstviParsovanehoKodu.ToString()));
				_povolitExportCiselniku = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/PovolitExportCiselniku", _povolitExportCiselniku.ToString()));
				_nacistSkladIDOnline = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/NacistSkladIDOnline", _nacistSkladIDOnline.ToString()));
				_povolitCeny = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/PovolitCeny", _povolitCeny.ToString()));
				_NacistNazevSkladuPriVyhledavani = bool.Parse(MST_Global.LoadElement(xmldoc, "/Config/Modules/Prodej/NacistNazevSkladuPriVyhledavani", _NacistNazevSkladuPriVyhledavani.ToString()));

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
				XmlElement prodejnode = null;
				prodejnode = null;

				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/StrediskoText", _StrediskoText.ToString());
				//_povolitEditaceNaplnenaDavka
				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/PozadovatHesloProOtevreniRozpracovaneDavky", _pozadovatHesloProOtevreniRozpracovaneDavky.ToString());

				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/PovolitZadaniMnozstviScannerem", _povolitZadaniMnozstviScannerem.ToString());
				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/HesloEditaceNaplnenaDavka", _hesloEditaceNaplnenaDavka);
				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/TiskSoupisuPriUzavreniDavky", _tiskSoupisuPriUzavreniDavky.ToString());
				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/TiskEtiketyPoPridaniZbozi", _tiskEtiketyPoPridaniZbozi.ToString());
				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/EtiketaTisk_PrebiratMnozstvi", _EtiketaTisk_PrebiratMnozstvi.ToString());
				

				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/StrediskoKPolozce") as XmlElement;
				if (prodejnode != null)
				{
					try { prodejnode.InnerText = _StrediskoKPolozce.ToString(); }
					catch { }
				}

				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/StrediskoJednoNaDavku", _StrediskoJednoNaDavku.ToString());

				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/KontrolaStavuSkladu") as XmlElement;
				if (prodejnode != null)
				{
					try { prodejnode.InnerText = _KontrolaStavuSkladu.ToString(); }
					catch { }
				}

				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/PriceIsWithTax") as XmlElement;
				if (prodejnode != null)
				{
					try { prodejnode.InnerText = _PriceIsWithTax.ToString(); }
					catch { }
				}
				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/PriceIsWithTaxEnable") as XmlElement;
				if (prodejnode != null)
				{
					try { prodejnode.InnerText = _PriceIsWithTaxEnable.ToString(); }
					catch { }
				}
				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/Price0IsWithTax") as XmlElement;
				if (prodejnode != null)
				{
					try { prodejnode.InnerText = _Price0IsWithTax.ToString(); }
					catch { }
				}
				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/Price1IsWithTax") as XmlElement;
				if (prodejnode != null)
				{
					try { prodejnode.InnerText = _Price1IsWithTax.ToString(); }
					catch { }
				}
				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/Price2IsWithTax") as XmlElement;
				if (prodejnode != null)
				{
					try { prodejnode.InnerText = _Price2IsWithTax.ToString(); }
					catch { }
				}

				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/OnlineDetailPolozka") as XmlElement;
				if (prodejnode != null)
				{
					try { prodejnode.InnerText = _OnlineDetailPolozka.ToString(); }
					catch { }
				}

				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/OnlinePocetKusu") as XmlElement;
				if (prodejnode != null)
				{
					try { prodejnode.InnerText = _OnlinePocetKusu.ToString(); }
					catch { }
				}

				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/OnlinePocetKusuSklad") as XmlElement;
				if (prodejnode != null)
				{
					try { prodejnode.InnerText = _OnlinePocetKusuSklad.ToString(); }
					catch { }
				}

				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/Price3IsWithTax") as XmlElement;
				if (prodejnode != null)
				{
					try { prodejnode.InnerText = _Price3IsWithTax.ToString(); }
					catch { }
				}
				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/Price4IsWithTax") as XmlElement;
				if (prodejnode != null)
				{
					try { prodejnode.InnerText = _Price4IsWithTax.ToString(); }
					catch { }
				}
				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/Price5IsWithTax") as XmlElement;
				if (prodejnode != null)
				{
					try { prodejnode.InnerText = _Price5IsWithTax.ToString(); }
					catch { }
				}
				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/OneOdberatelAutoSelect") as XmlElement;
				if (prodejnode != null)
				{
					try { prodejnode.InnerText = _OneOdberatelAutoSelect.ToString(); }
					catch { }
				}

				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/ZadaniLocncodePredSN") as XmlElement;
				if (prodejnode != null)
				{
					try
					{
						prodejnode.InnerText = _prodejZadaniLocncodePredSN.ToString();
					}
					catch { }
				}

				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/PovolitZadaniLocncode") as XmlElement;
				if (prodejnode != null)
				{
					try
					{
						prodejnode.InnerText = _prodejPovolitZadaniLocncode.ToString();
					}
					catch { }
				}

				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/RangeEnable") as XmlElement;
				if (prodejnode != null)
				{
					try { prodejnode.InnerText = _RangeEnable.ToString(); }
					catch { }
				}

				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/Range") as XmlElement;
				if (prodejnode != null)
				{
					try { prodejnode.Attributes["Next"].Value = NextNumber.ToString(); }
					catch { }
				}

				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/Range") as XmlElement;
				if (prodejnode != null)
				{
					try { prodejnode.Attributes["Prefix"].Value = Prefix.ToString(); }
					catch { }
				}

				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/TypDokladu") as XmlElement;
				if (prodejnode != null)
				{
					try { prodejnode.InnerText = _TypDokladu.ToString(); }
					catch { }
				}

				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/RucniVolbaOdberatele") as XmlElement;
				if (prodejnode != null)
				{
					try { prodejnode.InnerText = _RucniVolbaOdberatele.ToString(); }
					catch { }
				}

				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/Strediska") as XmlElement;
				if (prodejnode != null)
				{
					try { prodejnode.InnerText = _Strediska.ToString(); }
					catch { }
				}

				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/SkladID") as XmlElement;
				if (prodejnode != null)
				{
					try { prodejnode.InnerText = _SkladID; }
					catch { }
				}

				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/DisponibilityCheck") as XmlElement;
				if (prodejnode != null)
				{
					try { prodejnode.InnerText = _DisponibilityCheck.ToString(); }
					catch { }
				}

				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/DisponibilityHlaska") as XmlElement;
				if (prodejnode != null)
				{
					try { prodejnode.InnerText = _DisponibilityHlaska.ToString(); }
					catch { }
				}

				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/DisponibilityZvuk") as XmlElement;
				if (prodejnode != null)
				{
					try { prodejnode.InnerText = _DisponibilityCheck.ToString(); }
					catch { }
				}


				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/Odberatel") as XmlElement;
				if (prodejnode != null)
				{
					try { prodejnode.InnerText = _Odberatel.ToString(); }
					catch { }
				}

				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/Vnditnum2Serltnum") as XmlElement;
				if (prodejnode != null)
				{
					try { prodejnode.InnerText = _vnditnum2serltnum.ToString(); }
					catch { }
				}

				prodejnode = null;
				prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/OneOdberatelOnly") as XmlElement;
				if (prodejnode != null)
				{
					try { prodejnode.InnerText = _OneOdberatelOnly.ToString(); }
					catch { }
				}

				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/Rez1Cislo", _Rez1Cislo.ToString());
				//prodejnode = null;
				//prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/Rez1Cislo") as XmlElement;
				//if (prodejnode != null)
				//{
				//    try { prodejnode.InnerText = _Rez1Cislo.ToString(); }
				//    catch { }
				//}

				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/Rez2Cislo", _Rez2Cislo.ToString());
				//prodejnode = null;
				//prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/Rez2Cislo") as XmlElement;
				//if (prodejnode != null)
				//{
				//    try { prodejnode.InnerText = _Rez2Cislo.ToString(); }
				//    catch { }
				//}

				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/Rez3Cislo", _Rez3Cislo.ToString());
				//prodejnode = null;
				//prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/Rez3Cislo") as XmlElement;
				//if (prodejnode != null)
				//{
				//    try { prodejnode.InnerText = _Rez3Cislo.ToString(); }
				//    catch { }
				//}

				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/Rez4Cislo", _Rez4Cislo.ToString());
				//prodejnode = null;
				//prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/Rez4Cislo") as XmlElement;
				//if (prodejnode != null)
				//{
				//    try { prodejnode.InnerText = _Rez4Cislo.ToString(); }
				//    catch { }
				//}

				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/Rez1Povinne", _Rez1Povinne.ToString());
				//prodejnode = null;
				//prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/Rez1Povinne") as XmlElement;
				//if (prodejnode != null)
				//{
				//    try { prodejnode.InnerText = _Rez1Povinne.ToString(); }
				//    catch { }
				//}

				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/Rez2Povinne", _Rez2Povinne.ToString());
				//prodejnode = null;
				//prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/Rez2Povinne") as XmlElement;
				//if (prodejnode != null)
				//{
				//    try { prodejnode.InnerText = _Rez2Povinne.ToString(); }
				//    catch { }
				//}

				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/Rez3Povinne", _Rez3Povinne.ToString());
				//prodejnode = null;
				//prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/Rez3Povinne") as XmlElement;
				//if (prodejnode != null)
				//{
				//    try { prodejnode.InnerText = _Rez3Povinne.ToString(); }
				//    catch { }
				//}



				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/GridViewRowCount", GridViewRowCount.ToString());
				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/Rez4Povinne", _Rez4Povinne.ToString());
				//prodejnode = null;
				//prodejnode = xmldoc.SelectSingleNode("/Config/Modules/Prodej/Rez4Povinne") as XmlElement;
				//if (prodejnode != null)
				//{
				//    try { prodejnode.InnerText = _Rez4Povinne.ToString(); }
				//    catch { }
				//}

				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/Rez1Pamatovat", _Rez1Pamatovat.ToString());
				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/Rez2Pamatovat", _Rez2Pamatovat.ToString());
				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/Rez3Pamatovat", _Rez3Pamatovat.ToString());
				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/Rez4Pamatovat", _Rez4Pamatovat.ToString());

				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/FiltrCiselnikSkladu", _FiltrCiselnikSkladu.ToString());
				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/PouzitSklady", _PouzitSklady.ToString());

				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/FiltrCiselnikSkladuOnlyOne", _FiltrCiselnikSkladuOnlyOne.ToString());

				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/ZobrazovatListPolozek", _ZobrazovatListPolozek.ToString());


				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/Mnozstvi1Auto", _Mnozstvi1Auto.ToString());
				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/ZobrazovatReport", _ZobrazovatReport.ToString());
				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/MnozstviREZ1Vypln", _MnozstviREZ1Vypln.ToString());

				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/FindDefaultMJ", _FindDefaultMJ.ToString());

				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/Pracovnici", _Pracovnici.ToString());
				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/PracovniciJedenNaDavku", _PracovniciJedenNaDavku.ToString());
				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/PracovniciKPolozce", _PracovniciKPolozce.ToString());
				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/PracovniciText", _PracovniciText.ToString());
				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/PracovniciVyberJenScannerem", _PracovniciVyberJenScannerem.ToString());
				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/PolozkyVyberJenScannerem", _PolozkyVyberJenScannerem.ToString());
				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/PolozkyVyhledatPomociSarze", _PolozkyVyhledatPomociSarze.ToString());
				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/AktualizaceZboziPredVyberemDavky", _AktualizaceZboziPredVyberemDavky.ToString());

				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/PovolitNovouPolozku", _PovolitNovouPolozku.ToString());

				#region TaD Prodej Dialogy

				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/ProdejDialogOpusteniModulu", _ProdejDialogOpusteniModulu.ToString());
				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/ProdejDialogSmazaniDavky", _ProdejDialogSmazaniDavky.ToString());
				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/ProdejDialogNasnimanaLokace", _ProdejDialogNasnimanaLokace.ToString());
				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/ProdejDialogUkonceniZpracobaniDavky", _ProdejDialogUkonceniZpracobaniDavky.ToString());
				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/ProdejDialogUspesnehoOdeslaniDavky", _ProdejDialogUspesnehoOdeslaniDavky.ToString());
				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/EtiketaTiskDotazSCenou", _EtiketaTiskDotazSCenou.ToString());

				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/EtiketaTiskDotazSCenou_Cena", _EtiketaTiskDotazSCenou_Cena.ToString());
				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/EtiketaTiskDotazSCenou_ZobrazDialog", _EtiketaTiskDotazSCenou_ZobrazDialog.ToString());

				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/DialogTisk", _DialogTisk.ToString());

				#endregion

				#region F10 monosti

				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/F10_OnlinePocetKusuSklad", _F10_OnlinePocetKusuSklad.ToString());
				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/F10_ZobrazitAlternativyLokaci", _F10_ZobrazitAlternativyLokaci.ToString());
				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/F10_TiskEtiketa", _F10_TiskEtiketa.ToString());

				
				#endregion

				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/PovolitParsovaniMnozstvi", _PovolitParsovaniMnozstvi.ToString());
				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/PovolitParsovaniSarze", _PovolitParsovaniSarze.ToString());

				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/ExistenceNasnimanePolozky", _ExistenceNasnimanePolozky.ToString());

				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/DotazPridatNovaPolozka", _dotazPridatNovaPolozka.ToString());
				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/PaletyPovolit", _PaletyPovolit.ToString());

				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/FiltrDodavatele", _FiltrDodavatele.ToString());

				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/OverovatPohyb", _overovatPohyb.ToString());
				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/OverovatZdrojovouLokaci", _overovatZdrojovouLokaci.ToString());
				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/OverovatCilovouLokaci", _overovatCilovouLokaci.ToString());
				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/DoporuceneCiloveLokace", _doporuceneCiloveLokace.ToString());
				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/DoporucenePalety", _doporucenePalety.ToString());
				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/PovolitTisk", _povolitTiskEtikety.ToString());
				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/PredvyplneneMnozstviTiskuEtikety", _predvyplneneMnozstviEtikety.ToString());
				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/PredvyplneneMnozstviTiskuSoupisu", _predvyplneneMnozstviSoupisu.ToString());
				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/PovolitTiskSoupisu", _povolitTiskSoupisu.ToString());
				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/PovolitPrevodMeziSklady", _povolitPrevodMeziSklady.ToString());
				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/ZobrazitDialogZadaniMnozstviParsovanehoKodu", _zobrazitDialogZadaniMnozstviParsovanehoKodu.ToString());
				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/PovolitExportCiselniku", _povolitExportCiselniku.ToString());
				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/NacistSkladIDOnline", _nacistSkladIDOnline.ToString());
				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/PovolitCeny", _povolitCeny.ToString());
				MST_Global.SaveElement(xmldoc, "/Config/Modules/Prodej/NacistNazevSkladuPriVyhledavani", _NacistNazevSkladuPriVyhledavani.ToString());

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
