using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FASK.Vaha.RAVAS.Constants
{
    public static class Common
    {

        #region Pomocne konstanty

        /// <summary>
        /// Carriage return
        /// "Návrat vozíku"
        /// posune kurzor na začatek řadku
        /// </summary>
        public const string FASK_CR = "\r";


        #endregion

        #region Prikazy pro RAVAS vahu

        #region Prikazy SET a RESET

        /// Odpoved na tyto přikazy je:
        /// 
        /// Pokud přikaz prošel v pořadku tak:
        /// OK<CR>
        /// 
        /// Pokud nastala chyba na vahe tak:
        /// ERR<CR>
        /// 


        /// <summary>
        /// Set zero value
        /// </summary>
        public const string COMMAND_SZ = "SZ";

        /// <summary>
        /// Reset zero value
        /// </summary>
        public const string COMMAND_RZ = "RZ";

        /// <summary>
        /// Set preset tare value
        /// SP<value>
        /// 
        /// Pokud váha pracuje v rozsazích s číslem za desetinnou čárkou, měla by být 
        /// odpovídajícím způsobem uvedena přednastavená hodnota táry. 
        /// Pokud váha pracuje v rozsahu rovném nebo vyšším než 1 kg/lb, pak by měla být hodnota 
        /// zadána s desetinnou čárkou na konci hodnoty. 
        /// 
        /// Např.rozsahy 0,1/0,2/0,5 >> SP0001,5<CR>, rozsahy 1/2/5/10/20/50 >> SP00150.<CR>
        /// 
        /// 
        /// </summary>
        public const string COMMAND_SP = "SP{0}";

        /// <summary>
        /// Reset preset tare
        /// </summary>
        public const string COMMAND_RP = "RP";

        /// <summary>
        /// Reset tare
        /// </summary>
        public const string COMMAND_RT = "RT";

        /// <summary>
        /// Set tare
        /// </summary>
        public const string COMMAND_ST = "ST";

        /// <summary>
        /// Set tare (also with a previous tare)
        /// 
        /// Toto je speciální příkaz tare, který se používá hlavně s aplikacemi pro výběr objednávek. 
        /// Zruší předchozí tare a nastaví novou hodnotu tare, která zahrnuje starou hodnotu tare a přidanou čistou hmotnost. 
        /// Pokud se hmotnost do 5 sekund nedostane stabilní, bude vygenerována chyba.
        /// 
        /// </summary>
        public const string COMMAND_SR = "SR";

        #endregion


        /// <summary>
        /// Send gross mode (continuously)
        /// </summary>
        public const string COMMAND_SG = "SG";

        /// <summary>
        /// Send net mode (continuously)
        /// </summary>
        public const string COMMAND_SN = "SN";

        /// <summary>
        /// Send weights mode (continuously)
        /// 
        /// Pokud je aktivní chybový stav (např. přetížení nebo nedostatečné zatížení), měl by být po vyřešení 
        /// chybového stavu obnoven SW příkaz.
        /// 
        /// </summary>
        public const string COMMAND_SW = "SW";

        /// <summary>
        /// Send angle positions X and Y (continuously)
        /// </summary>
        public const string COMMAND_SA = "SA";

        /// <summary>
        /// Similar to SW but including errors
        /// </summary>
        public const string COMMAND_SL = "SL";

        /// <summary>
        /// Get preset tare
        /// </summary>
        public const string COMMAND_GP = "GP";

        /// <summary>
        /// Get tare
        /// </summary>
        public const string COMMAND_GT = "GT";

        /// <summary>
        /// Get gross
        /// </summary>
        public const string COMMAND_GG = "GG";

        /// <summary>
        /// Get net
        /// </summary>
        public const string COMMAND_GN = "GN";

        /// <summary>
        /// Get net, gross, status and checksum
        /// </summary>
        public const string COMMAND_GW = "GW";

        /// <summary>
        /// Get angle positions X and Y
        /// </summary>
        public const string COMMAND_GA = "GA";

        /// <summary>
        /// Read out of last 50 messages
        /// </summary>
        public const string COMMAND_GE = "GE";

        /// <summary>
        /// Read out of general info and parameters
        /// </summary>
        public const string COMMAND_GI = "GI";

        /// <summary>
        /// Read out of status and calibration
        /// </summary>
        public const string COMMAND_GS = "GS";

        /// <summary>
        /// Read out total log file
        /// </summary>
        public const string COMMAND_GL = "GL";

        /// <summary>
        /// Reset the ERRORs database (passcode required)
        /// </summary>
        public const string COMMAND_RE = "RE";

        /// <summary>
        /// Get net, wait for no motion
        /// 
        /// V případě, že váha není stabilní do 5 sekund, bude místo hmotnosti vrácena chyba. 
        /// Ujistěte se, že je váha stabilní a odešlete příkaz znovu. 
        /// Hmotnost se automaticky přičte k součtu indikátoru a pokud je připojena tiskárna, 
        /// automaticky odešle příkaz k tisku. 
        /// Pokud se součet nepoužije, bude automaticky resetován, jakmile celková hmotnost 
        /// dosáhne hodnoty 99999 nebo pořadové číslo dosáhne 99, podle toho, co nastane dříve.
        /// 
        /// </summary>
        public const string COMMAND_MN = "MN";

        /// <summary>
        /// Get gross, wait for no motion
        /// 
        /// V případě, že váha není stabilní do 5 sekund, bude místo hmotnosti vrácena chyba. 
        /// Ujistěte se, že je váha stabilní a odešlete příkaz znovu. 
        /// Hmotnost se automaticky přičte k součtu indikátoru a pokud je připojena tiskárna, 
        /// automaticky odešle příkaz k tisku. 
        /// Pokud se součet nepoužije, bude automaticky resetován, jakmile celková hmotnost 
        /// dosáhne hodnoty 99999 nebo pořadové číslo dosáhne 99, podle toho, co nastane dříve.
        /// 
        /// </summary>
        public const string COMMAND_MG = "MG";

        /// <summary>
        /// Send and Reset Subtotal,
        /// </summary>
        public const string COMMAND_RS = "RS";

        /// <summary>
        /// Get net and alibi nr., wait for no motion
        /// 
        /// Pokud alibi číslo dosáhne hodnoty 9999, začne znovu 0001 a přepíše první datovou sadu podle FIFO.
        /// 
        /// V případě, že váha není stabilní do 5 sekund, bude místo hmotnosti vrácena chyba. 
        /// Ujistěte se, že je váha stabilní a odešlete příkaz znovu. 
        /// Hmotnost se automaticky přičte k součtu indikátoru a pokud je připojena tiskárna, 
        /// automaticky odešle příkaz k tisku. 
        /// Pokud se součet nepoužije, bude automaticky resetován, jakmile celková hmotnost 
        /// dosáhne hodnoty 99999 nebo pořadové číslo dosáhne 99, podle toho, co nastane dříve.
        /// 
        /// </summary>
        public const string COMMAND_AN = "AN";

        /// <summary>
        /// Get gross and alibi nr., wait for no motion
        /// 
        /// Pokud alibi číslo dosáhne hodnoty 9999, začne znovu 0001 a přepíše první datovou sadu podle FIFO.
        /// 
        /// V případě, že váha není stabilní do 5 sekund, bude místo hmotnosti vrácena chyba. 
        /// Ujistěte se, že je váha stabilní a odešlete příkaz znovu. 
        /// Hmotnost se automaticky přičte k součtu indikátoru a pokud je připojena tiskárna, 
        /// automaticky odešle příkaz k tisku. 
        /// Pokud se součet nepoužije, bude automaticky resetován, jakmile celková hmotnost 
        /// dosáhne hodnoty 99999 nebo pořadové číslo dosáhne 99, podle toho, co nastane dříve.
        /// 
        /// </summary>
        public const string COMMAND_AG = "AG";

        #endregion

    }
}
