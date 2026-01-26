using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Console.Interfaces.Ciselniky
{
    public interface IZbozi : IVyrobaKonzola
    {
        /// <summary>
        /// Connection string pro pripojeni k DB.
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// Vraci seznam zbozi.
        /// </summary>
        /// <returns></returns>
        Fask.Console.Interfaces.DataSets.Zbozi GetZbozi();

        /// <summary>
        /// Vraci zbozi podle ID.
        /// </summary>
        /// <param name="id">ID zbozi.</param>
        /// <returns>Pokud nalezeno, vraci zaznam, jinak null</returns>
        Fask.Console.Interfaces.DataSets.Zbozi.CZMST095Row GetZboziByID(string id);

        /// <summary>
        /// Smaže zbozi.
        /// </summary>
        /// <param name="id">ID zbozi.</param>
        /// <returns></returns>
        bool DeleteZbozi(int id);

        /// <summary>
        /// Aktualizuje informace o zbozi.
        /// </summary>
        /// <param name="zdrojRow">Zbozi, ktere se bude aktualizovat</param>
        /// <returns></returns>
        bool UpdateZbozi(Console.Interfaces.DataSets.Zbozi.CZMST095Row zboziRow);

        /// <summary>
        /// Vloží do DB nový záznam zbozi.
        /// </summary>
        /// <param name="zdrojRow">Zbozi, které se má vložit.</param>
        /// <returns>True - vše v pořádku, False - chyba</returns>
        bool InsertZbozi(Console.Interfaces.DataSets.Zbozi.CZMST095Row zboziRow);

        /// <summary>
        /// Vraci filtrovane varianty sortimentu.
        /// </summary>
        /// <param name="filtr">filtr zaznamu</param>
        /// <returns></returns>
        Console.Interfaces.DataSets.Zbozi GetFiltrovaneZbozi(Fask.Console.Interfaces.Classes.ZboziListFiltr filtr);
    }
}
