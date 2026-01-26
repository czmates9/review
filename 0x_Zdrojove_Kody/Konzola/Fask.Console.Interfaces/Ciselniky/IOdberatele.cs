using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Console.Interfaces.Ciselniky
{
    public interface IOdberatele : IVyrobaKonzola
    {
        /// <summary>
        /// Connection string pro pripojeni k DB.
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// Vraci seznam odberatelu.
        /// </summary>
        /// <returns></returns>
        Fask.Console.Interfaces.DataSets.Odberatele GetOdberatele();

        /// <summary>
        /// Vraci odberatele podle ID.
        /// </summary>
        /// <param name="id">ID odberatele.</param>
        /// <returns>Pokud nalezeno, vraci zaznam, jinak null</returns>
        Fask.Console.Interfaces.DataSets.Odberatele.CZMST090Row GetOdberatelByID(string id);

        /// <summary>
        /// Smaže odberatele.
        /// </summary>
        /// <param name="id">ID odberatele.</param>
        /// <returns></returns>
        bool DeleteOdberatel(string id);

        /// <summary>
        /// Aktualizuje informace o odberateli.
        /// </summary>
        /// <param name="zdrojRow">Odberatel, ktery se bude aktualizovat</param>
        /// <returns></returns>
        bool UpdateOdberatel(Console.Interfaces.DataSets.Odberatele.CZMST090Row odberatelRow);

        /// <summary>
        /// Vloží do DB nový záznam odberatele.
        /// </summary>
        /// <param name="zdrojRow">Odberatel, který se má vložit.</param>
        /// <returns>True - vše v pořádku, False - chyba</returns>
        bool InsertOdberatel(Console.Interfaces.DataSets.Odberatele.CZMST090Row odberatelRow);
    }
}
