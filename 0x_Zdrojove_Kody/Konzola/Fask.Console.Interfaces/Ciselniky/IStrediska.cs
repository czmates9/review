using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Console.Interfaces.Ciselniky
{
    public interface IStrediska : IVyrobaKonzola
    {
        /// <summary>
        /// Connection string pro pripojeni k DB.
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// Vraci seznam Stredisek.
        /// </summary>
        /// <returns></returns>
        Fask.Console.Interfaces.DataSets.Strediska GetStrediska();

        /// <summary>
        /// Vraci stredisko podle ID.
        /// </summary>
        /// <param name="id">ID strediska.</param>
        /// <returns>Pokud nalezeno, vraci zaznam, jinak null</returns>
        Fask.Console.Interfaces.DataSets.Strediska.CZMST091Row GetStrediskoByID(string id);

        /// <summary>
        /// Smaže stredisko.
        /// </summary>
        /// <param name="id">ID strediska.</param>
        /// <returns></returns>
        bool DeleteStredisko(string id);

        /// <summary>
        /// Aktualizuje informace o stredisku.
        /// </summary>
        /// <param name="zdrojRow">Stredisko, ktere se bude aktualizovat</param>
        /// <returns></returns>
        bool UpdateStredisko(Console.Interfaces.DataSets.Strediska.CZMST091Row strediskoRow);

        /// <summary>
        /// Vloží do DB nový záznam strediska.
        /// </summary>
        /// <param name="zdrojRow">Stredisko, které se má vložit.</param>
        /// <returns>True - vše v pořádku, False - chyba</returns>
        bool InsertStredisko(Console.Interfaces.DataSets.Strediska.CZMST091Row strediskoRow);
    }
}
