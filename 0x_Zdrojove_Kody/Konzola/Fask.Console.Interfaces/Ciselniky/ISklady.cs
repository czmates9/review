using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Console.Interfaces.Ciselniky
{
    public interface ISklady : IVyrobaKonzola
    {
        /// <summary>
        /// Connection string pro pripojeni k DB.
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// Vraci seznam skladu.
        /// </summary>
        /// <returns></returns>
        Fask.Console.Interfaces.DataSets.Sklady GetSklady();

        /// <summary>
        /// Vraci sklad podle ID.
        /// </summary>
        /// <param name="id">ID skladu.</param>
        /// <returns>Pokud nalezeno, vraci zaznam, jinak null</returns>
        Fask.Console.Interfaces.DataSets.Sklady.CZMST093Row GetSkladByID(string id);

        /// <summary>
        /// Vraci filtrovane sklady.
        /// </summary>
        /// <param name="filtr">filtr zaznamu</param>
        /// <returns></returns>
        Console.Interfaces.DataSets.Sklady GetFiltrovaneSklady(Fask.Console.Interfaces.Classes.SkladyListFiltr filtr);

        /// <summary>
        /// Smaže sklad.
        /// </summary>
        /// <param name="id">ID skladu.</param>
        /// <returns></returns>
        bool DeleteSklad(string id);

        /// <summary>
        /// Aktualizuje informace o skladu.
        /// </summary>
        /// <param name="zdrojRow">Sklad, ktery se bude aktualizovat</param>
        /// <returns></returns>
        bool UpdateSklad(Console.Interfaces.DataSets.Sklady.CZMST093Row skladRow);

        /// <summary>
        /// Vloží do DB nový záznam skladu.
        /// </summary>
        /// <param name="zdrojRow">Sklad, které se má vložit.</param>
        /// <returns>True - vše v pořádku, False - chyba</returns>
        bool InsertSklad(Console.Interfaces.DataSets.Sklady.CZMST093Row skladRow);
    }
}
