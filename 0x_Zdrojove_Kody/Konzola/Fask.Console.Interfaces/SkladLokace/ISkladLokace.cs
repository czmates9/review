using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Console.Interfaces.SkladLokace
{
    /// <summary>
    /// Určení, jaký typ lokace se ověřuje
    /// 'S' - zdrojova, zdali je mozne z lokace vzit
    /// 'D' - cilova, zdali je mozne na lokaci umistit
    /// </summary>
    public enum TYPLokace
    {
        SOURCE = 'S',
        DEST = 'D'
    }

    public enum STATUSOverLokace
    {
        OK = 0,
        WARNING = 1,
        ERROR = 2
    }

    public class StatusOverLokace
    {
        public STATUSOverLokace State;              // stav (0 - vse v poradku, 1 - poruseno doporucene poradi, mozno pokracovat, 2 - chyba, neni mozne pokracovat)
        public string Message;            // zpráva, která se zobrazí při chybě
    }

    /// <summary>
    /// Typ, podle ktreho se rozhoduje, co se bude ukladat do tabulky pohybu.
    /// </summary>
    public enum TypeOfRecord
    {
        /// <summary>
        /// Empty
        /// </summary>
        E/*mpty*/,
        /// <summary>
        /// Prijem
        /// </summary>
        P/*rijem*/,
        /// <summary>
        /// Vydej
        /// </summary>
        V/*ydej*/,
        /// <summary>
        /// Inventura
        /// </summary>
        I/*nventura*/,
        /// <summary>
        /// Defregmentace
        /// </summary>
        D/*efregmentace*/
    };

    /// <summary>
    /// Oznaceni modulu, ktery funkci vola.
    /// </summary>
    public enum ModulName { PRIJEM, DEFREGMENTACE, VYDEJ };

    /// <summary>
    /// Interface lokacniho mechanismu.
    /// </summary>
    public interface ISkladLokace : IVyrobaKonzola
    {
        /// <summary>
        /// Connection string pro pripojeni k DB.
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// Vrací záznamy stavu skladu lokacniho mechanismu podle zadaného filtru.
        /// </summary>
        /// <param name="ds">Filtr záznamů.</param>
        /// <returns>Vyfiltrované záznamy.</returns>
        Fask.Console.Interfaces.DataSets.SkladLokace GetFiltrovanySkladLokaceStav(Fask.Console.Interfaces.Classes.SkladLokaceStavListFiltr filtr);

        /// <summary>
        /// Provede prijem/vydej/presun podle typu pohybu.
        /// </summary>
        /// <param name="Record">Zaznam, ktereho se defregmentace tyka.</param>
        bool InsertStavPohyb(LokacePohyb Record);
    }
}
