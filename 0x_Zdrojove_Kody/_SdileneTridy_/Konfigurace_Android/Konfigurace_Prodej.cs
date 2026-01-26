using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MES_Android
{
    public class Konfigurace_Prodej
    {

        private bool _polozkyVyberJenScannerem = false;
        [Popis("Výběr položky jen scannerem ")]
        public bool PolozkyVyberJenScannerem { get => _polozkyVyberJenScannerem; set => _polozkyVyberJenScannerem = value; }

        private bool _ZadaniLocncodePredSN = true;
        [Popis("Zadání lokace před SN ")]
        public bool ZadaniLocncodePredSN { get => _ZadaniLocncodePredSN; set => _ZadaniLocncodePredSN = value; }

        private bool _existenceNasnimanePolozky = true;
        [Popis("Kontrola položka již nasnímana ")]
        public bool ExistenceNasnimanePolozky { get => _existenceNasnimanePolozky; set => _existenceNasnimanePolozky = value; }

        private bool _nacistSkladIDOnline = false;
        [Popis("Načíst ID skladu online ")]
        public bool NacistSkladIDOnline { get => _nacistSkladIDOnline; set => _nacistSkladIDOnline = value; }

        private bool _zobrazitDialogZadaniMnozstviParsovanehoKodu = true;
        [Popis("Zobrazit dialog množství u parsovaného kódu ")]
        public bool ZobrazitDialogZadaniMnozstviParsovanehoKodu { get => _zobrazitDialogZadaniMnozstviParsovanehoKodu; set => _zobrazitDialogZadaniMnozstviParsovanehoKodu = value; }

        private bool _povolitZadaniMnozstviScannerem = true;
        [Popis("Povolit zadání množství scannerem ")]
        public bool PovolitZadaniMnozstviScannerem { get => _povolitZadaniMnozstviScannerem; set => _povolitZadaniMnozstviScannerem = value; }

        private bool _mnozstvi1Auto = false;
        [Popis("Množství 1 automaticky ")]
        public bool Mnozstvi1Auto { get => _mnozstvi1Auto; set => _mnozstvi1Auto = value; }
        
        private bool _mnozstviREZ1Vypln = false;
        [Popis("Množství nastav ze zboží REZ1 ")]
        public bool MnozstviREZ1Vypln { get => _mnozstviREZ1Vypln; set => _mnozstviREZ1Vypln = value; }

        private bool _kontrolaStavuSkladu = true;
        [Popis("Kontrola stavu skladu ")]
        public bool KontrolaStavuSkladu { get => _kontrolaStavuSkladu; set => _kontrolaStavuSkladu = value; }

        private bool _disponibilityZvuk = true;
        [Popis("Povolit zvuk ")]
        public bool DisponibilityZvuk { get => _disponibilityZvuk; set => _disponibilityZvuk = value; }

        private string _SoundDisponibility = "brownuv_sum.wav";
        [Popis("Disponibilita zvuk:")]
        public string SoundDisponibility { get => _SoundDisponibility; set => _SoundDisponibility = value; }

        private bool _disponibilityHlaska = true;
        [Popis("Disponibilita hláška ")]
        public bool DisponibilityHlaska { get => _disponibilityHlaska; set => _disponibilityHlaska = value; }

        private bool _strediskoKPolozce = true;
        [Popis("K položce ")]
        public bool StrediskoKPolozce { get => _strediskoKPolozce; set => _strediskoKPolozce = value; }

        private bool _strediskoText = true;
        [Popis("Holý text ")]
        public bool StrediskoText { get => _strediskoText; set => _strediskoText = value; }

        private bool _pracovniciKPolozce = true;
        [Popis("K položce ")]
        public bool PracovniciKPolozce { get => _pracovniciKPolozce; set => _pracovniciKPolozce = value; }

        private bool _pracovniciText = true;
        [Popis("Holý text ")]
        public bool PracovniciText { get => _pracovniciText; set => _pracovniciText = value; }

        private bool _overovatPohyb = false;
        [Popis("Ověřovat pohyb ")]
        public bool OverovatPohyb { get => _overovatPohyb; set => _overovatPohyb = value; }

        private bool _povolitPrintServer = true;
        [Popis("Povolit tiskový server ")]
        public bool PovolitPrintServer { get => _povolitPrintServer; set => _povolitPrintServer = value; }

        private bool _dialogTisk = true;
        [Popis("Dialog Tisk ")]
        public bool DialogTisk { get => _dialogTisk; set => _dialogTisk = value; }

        private bool _etiketaTiskDotazSCenou = false;
        [Popis("Tisky alternativy ")]
        public bool EtiketaTiskDotazSCenou { get => _etiketaTiskDotazSCenou; set => _etiketaTiskDotazSCenou = value; }

        private bool _etiketaTiskDotazSCenou_Cena = false;
        [Popis(" Alternativa ")]
        public bool EtiketaTiskDotazSCenou_Cena { get => _etiketaTiskDotazSCenou_Cena; set => _etiketaTiskDotazSCenou_Cena = value; }

        private bool _etiketaTiskDotazSCenou_ZobrazDialog = false;
        [Popis("Zobrazit dialog ")]
        public bool EtiketaTiskDotazSCenou_ZobrazDialog { get => _etiketaTiskDotazSCenou_ZobrazDialog; set => _etiketaTiskDotazSCenou_ZobrazDialog = value; }

        private bool _etiketaTisk_PrebiratMnozstvi = false;
        [Popis("Tisk etiketa, přebírat zadané množství ")]
        public bool EtiketaTisk_PrebiratMnozstvi { get => _etiketaTisk_PrebiratMnozstvi; set => _etiketaTisk_PrebiratMnozstvi = value; }

        private string _predvyplneneMnozstviEtikety = string.Empty;
        [Popis("Předvyp. množství ")]
        public string PredvyplneneMnozstviEtikety { get => _predvyplneneMnozstviEtikety; set => _predvyplneneMnozstviEtikety = value; }

        private bool _polozkyVyhledatPomociSarze = false;
        [Popis("Vyhledat položku pomocí šarže ")]
        public bool PolozkyVyhledatPomociSarze { get => _polozkyVyhledatPomociSarze; set => _polozkyVyhledatPomociSarze = value; }

        private int _gridViewRowCount = 10;
        [Popis("Stránka - zobrazit řádků číselníků ")]
        public int GridViewRowCount { get => _gridViewRowCount; set => _gridViewRowCount = value; }

        private bool _filtrCiselnikSkladu = true;
        [Popis("Použít filtr dle číselníku skladů ")]
        public bool FiltrCiselnikSkladu { get => _filtrCiselnikSkladu; set => _filtrCiselnikSkladu = value; }

        private bool _filtrDodavatele = false;
        [Popis("Použít filtr dle dodavatele ")]
        public bool FiltrDodavatele { get => _filtrDodavatele; set => _filtrDodavatele = value; }

        private bool _aktualizaceZboziPredVyberemDavky = false;
        [Popis("Dotaz aktualizace zboží před výběrem dávky ")]
        public bool AktualizaceZboziPredVyberemDavky { get => _aktualizaceZboziPredVyberemDavky; set => _aktualizaceZboziPredVyberemDavky = value; }

        private string _skladID = "";
        [Popis("ID Skladu ")]
        public string SkladID { get => _skladID; set => _skladID = value; }

        private bool _filtrCiselnikSkladuOnlyOne;
        [Popis("Pouze jeden sklad na dávku ")]
        public bool FiltrCiselnikSkladuOnlyOne { get => _filtrCiselnikSkladuOnlyOne; set => _filtrCiselnikSkladuOnlyOne = value; }

        private bool _odberatel;
        [Popis("Povolit odběratele ")]
        public bool Odberatel { get => _odberatel; set => _odberatel = value; }

        private string _SoundExpedice = "Expedice.wav";
        [Popis("Expedice zvuk ")]
        public string SoundExpedice { get => _SoundExpedice; set => _SoundExpedice = value; }

        private string _SoundSkladExpedice = "Rozdelit.wav";
        [Popis("Sklad/Expedice zvuk ")]
        public string SoundSkladExpedice { get => _SoundSkladExpedice; set => _SoundSkladExpedice = value; }

        private string _SoundSklad = "Sklad.wav";
        [Popis("Sklad zvuk ")]
        public string SoundSklad { get => _SoundSklad; set => _SoundSklad = value; }

        private bool _zobrazovatReport;
        [Popis("Zobrazovat report ")]
        public bool ZobrazovatReport { get => _zobrazovatReport; set => _zobrazovatReport = value; }

        private string _SoundUspesneVlozeni = "UspesneNacteni.wav";
        [Popis("Úspěšné vložení zvuk:")]
        public string SoundUspesneVlozeni { get => _SoundUspesneVlozeni; set => _SoundUspesneVlozeni = value; }

        private bool _povolitNovouPolozku = false;
        [Popis("Povolit přidání nové položky ")]
        public bool PovolitNovouPolozku { get => _povolitNovouPolozku; set => _povolitNovouPolozku = value; }

        private bool _dialogNasnimanaLokace = true;
        [Popis("Zobrazit dialog nasnímané lokace ")]
        public bool DialogNasnimanaLokace { get => _dialogNasnimanaLokace; set => _dialogNasnimanaLokace = value; }

        private bool _DialogUspesnehoOdeslaniDavky = true;
        [Popis("Zobrazit dialog úspěšného odeslání ")]
        public bool DialogUspesnehoOdeslaniDavky { get => _DialogUspesnehoOdeslaniDavky; set => _DialogUspesnehoOdeslaniDavky = value; }

        private bool _rangeEnable = true;
        [Popis("Generovat nové číslo dávky ")]
        public bool RangeEnable { get => _rangeEnable; set => _rangeEnable = value; }

        private Konfigurace_Prodej_REZ _REZ1 = new Konfigurace_Prodej_REZ();
        [Popis("Dop.Info REZ1 ")]
        public Konfigurace_Prodej_REZ REZ1 { get => _REZ1; set => _REZ1 = value; }

        private Konfigurace_Prodej_REZ _REZ2 = new Konfigurace_Prodej_REZ();
        [Popis("Dop.Info REZ2")]
        public Konfigurace_Prodej_REZ REZ2 { get => _REZ2; set => _REZ2 = value; }

        private Konfigurace_Prodej_REZ _REZ3 = new Konfigurace_Prodej_REZ();
        [Popis("Dop.Info REZ3")]
        public Konfigurace_Prodej_REZ REZ3 { get => _REZ3; set => _REZ3 = value; }

        private Konfigurace_Prodej_REZ _REZ4 = new Konfigurace_Prodej_REZ();
        [Popis("Dop.Info REZ4")]
        public Konfigurace_Prodej_REZ REZ4 { get => _REZ4; set => _REZ4 = value; }

        private Konfigurace_Prodej_Price _cena = new Konfigurace_Prodej_Price();
        [Popis("Ceny ")]
        public Konfigurace_Prodej_Price Cena { get => _cena; set => _cena = value; }

        private int _SSCC_Sequence = 1;
        [Popis("ID SSCC Sequence ")]
        public int SSCC_Sequence { get => _SSCC_Sequence; set => _SSCC_Sequence = value; }

    }
}