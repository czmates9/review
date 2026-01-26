using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FASK.SledovaniVyroby.Module.Vyroba_Agro_Modbus.Constants
{
    public static class Common
    {

        public const string STAV_odecet = "odecet";
        public const string STAV_odecetKusy = "odecist kusy";
        public const string STAV_prihlaseniSmeny = "prihlaseni smeny";
        public const string STAV_odhlaseni = "odhlaseni";
        public const string STAV_zmenaProhaz = "zmena prohaz";
        //TODO MaR 11.7 2023 pridany konstanty
        public const string STAV_zmenaProhazON = "prohaz zapnuto";
        public const string STAV_zmenaProhazOFF = "prohaz vypnuto";

        public const string STAV_Vyrobek_zmenaVyrobku = "Vyrobek:zmena vyrobku manual";
        public const string STAV_Vyrobek_zmenaVyrobkuSCAN = "Vyrobek:zmena vyrobku scan";
        public const string STAV_ulozeniPoOdectu = "ulozeni po odectu";
        public const string STAV_ulozeniPoOdectuEAN = "ulozeni po odectu EAN";
        public const string STAV_zmenaSarze = "zmena sarze";
        public const string STAV_automatickeUlozeni = "automaticke ulozeni";
        public const string STAV_automatickeUlozeniPaleta = "automaticke ulozeni paleta";
        public const string STAV_konecAplikace = "konec aplikace";


        public const string STAV_rucni = "rucni";
        public const string STAV_ScanReadOK = "Scan Read ok";
        public const string STAV_ScanReadNavic = "Scan Read navic";
        public const string STAV_ScanNOReadOK = "scan NoRead ok";
        public const string STAV_ScanNOReadNavic = "Scan NoRead navic";
        public const string STAV_CidloNavic = "Cidlo navic";
        public const string STAV_CidloOK = "Cidlo ok";
        public const string STAV_sensorOdvod = "sensorOdvod";
        public const string STAV_sensorPaleta = "sensorPaleta";

        public const string STAV_posledniPaleta1 = "posledni paleta 1";
        public const string STAV_posledniPaleta2 = "posledni paleta 2";
        public const string STAV_posledniPaleta3 = "posledni paleta 3";
        public const string STAV_odhlaseni_PosledniPaleta = "odhlaseni posledni paleta";

        public const string STAV_posledniPaleta_ZZ = "zaverecny zaznam";

        public const string STAV_SSK = "System kontroly kodu 3";
        public const string STAV_SSK_NORead = "System kontroly kodu 3 NORead";
        public const string STAV_PP_predcasne_spusteni = "PP predcasne spusteni";

        public const string STAV_PO_predcasny_odjezd = "PO predcasny odjezd";

        public const string STAV_RezimServisStart = "Rezim Servis Start";
        public const string STAV_RezimServisStop = "Rezim Servis Stop";


        //Constants.Common.STAV_STAV_sensorOdvod


        // Stavy palet
        public const string STAV_UplnaPaleta = "UP";
        //public const string STAV_NEUplnaPaleta = "NP";
        public const string STAV_ZaverecnyZaznam = "ZZ";

    }
}
