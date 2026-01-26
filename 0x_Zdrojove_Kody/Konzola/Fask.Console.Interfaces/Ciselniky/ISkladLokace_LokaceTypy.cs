using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Console.Interfaces.Ciselniky
{
    public interface ISkladLokace_LokaceTypy : IVyrobaKonzola
    {
        /// <summary>
        /// Connection string pro pripojeni k DB.
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// Vraci typy lokaci.
        /// </summary>
        /// <returns></returns>
        Fask.Console.Interfaces.DataSets.SkladLokace GetSkladLokace_LokaceTypy();

        /// <summary>
        /// Vraci typ lokace podle id.
        /// </summary>
        /// <param name="type">Typ lokace.</param>
        /// <returns>Pokud nalezeno, vraci zaznam, jinak null</returns>
        Fask.Console.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_LokaceTypyRow GetSkladLokace_LokaceTypyByType(string type);

        /// <summary>
        /// Smaže typ lokace.
        /// </summary>
        /// <param name="skl_id">Typ lokace.</param>
        /// <returns></returns>
        bool DeleteSkladLokace_LokaceTypy(string type);

        /// <summary>
        /// Aktualizuje typ lokace.
        /// </summary>
        /// <param name="row">Lokace, ktera se bude aktualizovat</param>
        /// <returns></returns>
        bool UpdateSkladLokace_LokaceTypy(Console.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_LokaceTypyRow row);

        /// <summary>
        /// Vloží do DB nový záznam typu lokace.
        /// </summary>
        /// <param name="row">Lokace, která se má vložit.</param>
        /// <returns>True - vše v pořádku, False - chyba</returns>
        bool InsertSkladLokace_LokaceTypy(Console.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_LokaceTypyRow row);

        /// <summary>
        /// Vraci filtrovane typy lokaci.
        /// </summary>
        /// <param name="filtr">filtr zaznamu</param>
        /// <returns></returns>
        Fask.Console.Interfaces.DataSets.SkladLokace GetFiltrovaneSkladLokace_LokaceTypy(Fask.Console.Interfaces.Classes.LokaceTypyListFiltr filtr);
    }
}
