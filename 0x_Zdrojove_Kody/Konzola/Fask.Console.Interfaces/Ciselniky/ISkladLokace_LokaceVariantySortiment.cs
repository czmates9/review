using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Console.Interfaces.Ciselniky
{
    public interface ISkladLokace_LokaceVariantySortiment : IVyrobaKonzola
    {
        /// <summary>
        /// Connection string pro pripojeni k DB.
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// Vraci varianty zbozi.
        /// </summary>
        /// <returns></returns>
        Fask.Console.Interfaces.DataSets.SkladLokace GetSkladLokace_LokaceVariantySortiment();

        /// <summary>
        /// Vraci varianty zbozi id.
        /// </summary>
        /// <param name="skl_id">ID skladu.</param>
        /// <param name="locncode">ID lokace.</param>
        /// <param name="itemnmbr">ID polozky.</param>
        /// <returns>Pokud nalezeno, vraci zaznam, jinak null</returns>
        Fask.Console.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_LokaceVariantySortimentRow GetSkladLokace_LokaceVariantySortimentBySklidLocncodeItemnmbr(string skl_id, string locncode, string itemnmbr);

        /// <summary>
        /// Smaže variantu zbozi.
        /// </summary>
        /// <param name="skl_id">ID skladu.</param>
        /// <param name="locncode">ID lokace.</param>
        /// <param name="itemnmbr">ID polozky.</param>
        /// <returns></returns>
        bool DeleteSkladLokace_LokaceVariantySortiment(string skl_id, string locncode, string itemnmbr);

        /// <summary>
        /// Aktualizuje variantu zbozi.
        /// </summary>
        /// <param name="row">varianta, ktera se bude aktualizovat</param>
        /// <returns></returns>
        bool UpdateSkladLokace_LokaceVariantySortiment(Console.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_LokaceVariantySortimentRow row, string locncodeold);

        /// <summary>
        /// Vloží do DB nový záznam varianty zbozi.
        /// </summary>
        /// <param name="row">varianta zbozi, která se má vložit.</param>
        /// <returns>True - vše v pořádku, False - chyba</returns>
        bool InsertSkladLokace_LokaceVariantySortiment(Console.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_LokaceVariantySortimentRow row);

        /// <summary>
        /// Vloží do DB nový záznam varianty zbozi.
        /// </summary>
        /// <param name="TermID">ID Terminalu</param>
        /// <param name="row">varianta zbozi, která se má vložit.</param>
        /// <param name="type">Typ lokace</param>
        /// <returns>True - vše v pořádku, False - chyba</returns>
        Fask.Console.Interfaces.Classes.INVENTURA_PLNENI_VARIANT_STATUS InsertSkladLokace_LokaceVariantySortiment(byte TermID, Console.Interfaces.DataSets.Inventura.CZMST_I4Row row, string type);

        /// <summary>
        /// Vraci filtrovane varianty sortimentu.
        /// </summary>
        /// <param name="filtr">filtr zaznamu</param>
        /// <returns></returns>
        Console.Interfaces.DataSets.SkladLokace GetFiltrovanySkladLokace_LokaceVariantySortiment(Fask.Console.Interfaces.Classes.FormLokaceVariantySortimentListFiltr filtr);
    }
}
