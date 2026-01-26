using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Console.Interfaces.Ciselniky
{
    public interface ISkladLokace_Mapa : IVyrobaKonzola
    {
        /// <summary>
        /// Connection string pro pripojeni k DB.
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// Vraci mapu skladu.
        /// </summary>
        /// <returns></returns>
        Fask.Console.Interfaces.DataSets.SkladLokace GetSkladLokace_Mapa();

        /// <summary>
        /// Vraci lokaci podle skl_id a locncode.
        /// </summary>
        /// <param name="skl_id">ID skladu.</param>
        /// <param name="locncode">ID lokace.</param>
        /// <returns>Pokud nalezeno, vraci zaznam, jinak null</returns>
        Fask.Console.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_MapaRow GetSkladLokace_MapaBySklIDAndLocncode(string skl_id, string locncode);

        /// <summary>
        /// Vraci filtrovane varianty sortimentu.
        /// </summary>
        /// <param name="filtr">filtr zaznamu</param>
        /// <returns></returns>
        Fask.Console.Interfaces.DataSets.SkladLokace GetFiltrovaneSkladLokace_Mapa(Fask.Console.Interfaces.Classes.SkladLokaceMapaListFiltr filtr);

        /// <summary>
        /// Smaže lokaci.
        /// </summary>
        /// <param name="skl_id">ID skladu.</param>
        /// <param name="locncode">ID lokace.</param>
        /// <returns></returns>
        bool DeleteSkladLokace_Mapa(string skl_id, string locncode);

        /// <summary>
        /// Aktualizuje informace o lokaci.
        /// </summary>
        /// <param name="row">Lokace, ktera se bude aktualizovat</param>
        /// <returns></returns>
        bool UpdateSkladLokace_Mapa(Console.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_MapaRow row);

        /// <summary>
        /// Vloží do DB nový záznam lokace.
        /// </summary>
        /// <param name="row">Lokace, která se má vložit.</param>
        /// <returns>True - vše v pořádku, False - chyba</returns>
        bool InsertSkladLokace_Mapa(Console.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_MapaRow row);
    }
}
