
using System;
namespace FASK.SledovaniVyroby.Module.Rezacka
{
    /// <summary>
    /// Jedna trida reprezentujici operaci
    /// </summary>
    public class Operation
    {
        /// <summary>
        /// Konstruktor - nastaveni string hodnot na prazdny retezec
        /// </summary>
        public Operation(bool free)
        {
            IDO = string.Empty;
            NAZEV = string.Empty;
            CK = string.Empty;
            NEXTOPERATIONS = string.Empty;
            SPHLAVICKA = string.Empty;
            SPINFO = string.Empty;
            SPZAKAZKA = string.Empty;
            VOLNA = free;
            SPMATERIAL = string.Empty;
        }

        /// <summary>
        /// Konstruktor - kopie parametru
        /// </summary>
        public Operation(Operation op)
        {
            IDO = op.IDO;
            NAZEV = op.NAZEV;
            CK = op.CK;
            SCAN1 = op.SCAN1;
            SCAN2 = op.SCAN2;
            SCAN3 = op.SCAN3;
            SENSOR = op.SENSOR;
            VOLNA = op.VOLNA;
            START = op.START;
            KONEC = op.KONEC;
            NEXTOPERATIONS = op.NEXTOPERATIONS;
            SPINFO = op.SPINFO;
            SPHLAVICKA = op.SPHLAVICKA;
            SCANZAKAZKA = op.SCANZAKAZKA;
            SPZAKAZKA = op.SPZAKAZKA;
            KONTROLAMAT = op.KONTROLAMAT;
            SPMATERIAL = op.SPMATERIAL;
            LOGIN = op.LOGIN;
        }

        /// <summary>
        /// K hodnote ve sloupci SENSOR tabulky FASK_Operations vraci odpovidajici popis
        /// </summary>
        /// <param name="sensorValue">Hodnota sloupce</param>
        /// <returns>Popis</returns>
        public static string sensorDescription(byte sensorValue)
        {
            //Vystup
            string desc = string.Empty;

            //0 je prazdny reteze - vracim
            if (sensorValue == 0) return string.Empty;
            //1 je nepracovat
            if ((sensorValue & 1) > 0) return "Nepracovat";
            //Jinak projizdim zbyle moznosti
            if ((sensorValue & 2) > 0) desc += (desc == string.Empty ? string.Empty : ", ") + "Číst hodotu před zahájení operace";
            if ((sensorValue & 4) > 0) desc += (desc == string.Empty ? string.Empty : ", ") + "Číst hodotu po zahájení operace";
            //if ((sensorValue & 8) > 0) desc += (desc == string.Empty ? string.Empty : ", ") + "Načíst hodotu před ukončením operace";
            //if ((sensorValue & 16) > 0) desc += (desc == string.Empty ? string.Empty : ", ") + "Načíst hodotu po ukončení operace";

            //Pokud jeste zde je prazdny string, nic nevyhovovalo
            if (desc == string.Empty) throw new ApplicationException("Neočekávaná hodnota sloupce SENSOR v tabulce FASK_Operations");
            //Jinak Ok
            return desc;
        }

        //Odpovida jednotlivym sloupcum v operations db
        /// <summary>
        /// ID operace
        /// </summary>
        internal string IDO { get; set; }
        /// <summary>
        /// Nazev operace
        /// </summary>
        internal string NAZEV { get; set; }
        /// <summary>
        /// Carovy kod operace
        /// </summary>
        internal string CK { get; set; }
        /// <summary>
        /// Scanovani 1
        /// </summary>
        internal byte SCAN1 { get; set; }
        /// <summary>
        /// Scanovani 2
        /// </summary>
        internal byte SCAN2 { get; set; }
        /// <summary>
        /// Scanovani 3
        /// </summary>
        internal byte SCAN3 { get; set; }
        /// <summary>
        /// Hodnota sensoru
        /// 0. vychozi stav pro prazdny strin - INIT
        /// 1. Nepracovan
        /// 2. Pracovat pred zahajenim (scanovanim)
        /// 4. Pracovat po zahajeni (scanovani)
        /// </summary>
        internal byte SENSOR { get; set; }
        /// <summary>
        /// Priznak, ze operace je volna
        /// </summary>
        internal bool VOLNA { get; set; }
        /// <summary>
        /// Priznak, ze operace je startovni
        /// </summary>
        internal bool START { get; set; }
        /// <summary>
        /// Priznak, ze operace je koncova
        /// </summary>
        internal bool KONEC { get; set; }
        //Odpovida nasledujicim operacim v next_operations v db
        /// <summary>
        /// Mozne nasledujici operace
        /// </summary>
        internal string NEXTOPERATIONS { get; set; }
        /// <summary>
        /// Nazev volane procedury k dotazeni hlavicky k operaci - prazdny nazev => nebude se dotahovat
        /// </summary>
        internal string SPHLAVICKA { get; set; }
        /// <summary>
        /// Nazev volane procedury k dotazeni informaci k operaci - prazdny nazev => nebude se dotahovat
        /// </summary>
        internal string SPINFO { get; set; }
        /// <summary>
        /// Scanovani cisla zakazky
        /// </summary>
        internal byte SCANZAKAZKA { get; set; }
        /// <summary>
        /// Nazev volane procedury k zjisteni informaci o zakazky - prazdny nazev => chyba pokud je priznak
        /// </summary>
        internal string SPZAKAZKA { get; set; }
        /// <summary>
        /// Priznak, zda se ma kontrolovat material
        /// </summary>
        internal bool KONTROLAMAT { get; set; }
        /// <summary>
        /// Nazev volane procedury k zjisteni informaci o materialu - prazdny nazev => chyba pokud je priznak
        /// </summary>
        internal string SPMATERIAL { get; set; }
        /// <summary>
        /// Priznak, zda se ma prehlasovat uzivatel
        /// </summary>
        internal bool LOGIN { get; set; }
    }
}
