using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Console.Interfaces.Ciselniky
{
    public interface IPracovnici : IVyrobaKonzola
    {
        /// <summary>
        /// Connection string pro pripojeni k DB.
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// Vraci seznam Pracovniku.
        /// </summary>
        /// <returns></returns>
        Fask.Console.Interfaces.DataSets.Pracovnici GetPracovnici();

        /// <summary>
        /// Vraci pracovnika podle ID.
        /// </summary>
        /// <param name="id">ID pracovnika.</param>
        /// <returns>Pokud nalezeno, vraci zaznam, jinak null</returns>
        Fask.Console.Interfaces.DataSets.Pracovnici.CZMST096Row GetPracovnikByID(string id);

        /// <summary>
        /// Vraci filtrovane pracovniky.
        /// </summary>
        /// <param name="filtr">filtr zaznamu</param>
        /// <returns></returns>
        Console.Interfaces.DataSets.Pracovnici GetFiltrovanePracovniky(Fask.Console.Interfaces.Classes.PracovniciListFiltr filtr);

        /// <summary>
        /// Smaže Pracovnika.
        /// </summary>
        /// <param name="id">ID pracovnika.</param>
        /// <returns></returns>
        bool DeletePracovnici(string id);

        /// <summary>
        /// Aktualizuje informace o Pracovnikovy.
        /// </summary>
        /// <param name="zdrojRow">Pracovnici, ktery se bude aktualizovat</param>
        /// <returns></returns>
        bool UpdatePracovnici(Console.Interfaces.DataSets.Pracovnici.CZMST096Row PracovniciRow);

        /// <summary>
        /// Vloží do DB nový záznam Pracovnici.
        /// </summary>
        /// <param name="zdrojRow">Pracovnici, který se má vložit.</param>
        /// <returns>True - vše v pořádku, False - chyba</returns>
        bool InsertPracovnici(Console.Interfaces.DataSets.Pracovnici.CZMST096Row PracovniciRow);
    }
}
