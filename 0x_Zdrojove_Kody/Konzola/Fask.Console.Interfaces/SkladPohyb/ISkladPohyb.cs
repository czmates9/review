using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Console.Interfaces.SkladPohyb
{
    /// <summary>
    /// Pohyby ve skladu. Jako tabulku vyuziva pohled s pevne danou strukturou. 
    /// Umoznuje zobrazit data soucasne pro vydej, prijem, ...
    /// </summary>
    public interface ISkladPohyb : IVyrobaKonzola
    {
        /// <summary>
        /// Connection string pro pripojeni k DB.
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// Vrací záznamy podle zadaného filtru.
        /// </summary>
        /// <param name="ds">Filtr záznamů.</param>
        /// <returns>Vyfiltrované pohyby.</returns>
        Fask.Console.Interfaces.DataSets.SkladPohyb GetFiltrovanySkladLokace(Fask.Console.Interfaces.Classes.SkladPohybListFiltr filtr, string tableName);

        /// <summary>
        /// Vrací veškeré typy pohybu (distinct TYPE)
        /// </summary>
        /// <returns></returns>
        Fask.Console.Interfaces.DataSets.SkladPohyb.CZMST_Sklad_PohybDataTable GetTypPohybu(string tableName);
    }
}
