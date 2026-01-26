using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Fask.Server.Interfaces.Lokace
{
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
    /// Rozhrani pro vsechny typy databazi.
    /// </summary>
    public interface ILokace
    {
        /// <summary>
        /// Metoda pro pridani zaznamu do lokacniho mechanismu (vcetne pohybu).
        /// </summary>
        /// <param name="record">Trida reprezentujici zaznam=jeden radek tabulky.</param>
        Fask.Server.Interfaces.Classes.StatusLokace Lokace_AddRecord(LokacePohyb record);

        /// <summary>
        /// Metoda pro odstraneni zaznamu (vlozi se opacny pohyb/y).
        /// </summary>
        /// <param name="Guid">Guid zaznamu</param>
        /// <param name="ModulName">Modul o ktery se jedna (Prijem/Vydej)</param>
		/// <returns>Trida reprezentujici zaznam=jeden radek tabulky</returns>
        Fask.Server.Interfaces.Classes.StatusLokace Lokace_DeleteRecord(Guid Guid, ModulName ModulName);

        /// <summary>
        /// Defregmentace (presun ze zdrojove lokace/skladu na cilovou lokaci/sklad)
        /// </summary>
        /// <param name="Record">Zaznam, ktereho se defregmentace tyka.</param>
        Fask.Server.Interfaces.Classes.StatusLokace Lokace_MoveItem(LokacePohyb Record);

		/// <summary>
		/// Zobrazi mnozstvi materialu na jednotlivych lokaci
		/// </summary>
		/// <param name="itemnmbr">ID materialu.</param>
		/// <returns>Odpovidajici zaznamy</returns>
        Fask.Server.Interfaces.DataSets.Location Lokace_ShowMaterial(string itemnmbr);

        /// <summary>
        /// Zobrazi mnozstvi materialu na jednotlivych lokaci
        /// </summary>
        /// <param name="itemnmbr">ID materialu.</param>
        /// <param name="serltnum">Sarze.</param>
        /// <param name="skl_id">ID skladu.</param>
        /// <param name="NumberOfRecords">Pocet zaznamu.</param>
        /// <returns>Odpovidajici zaznamy</returns>
        Fask.Server.Interfaces.DataSets.Location Lokace_ShowMaterial(string itemnmbr, string serltnum, string skl_id, uint? NumberOfRecords, bool ShowEmpty);

        /// <summary>
        /// Zobrazi mnozstvi materialu v regalu.
        /// </summary>
        /// <param name="itemnmbr">Oznaceni materialu.</param>
        /// <param name="serltnum">Sarze.</param>
        /// <param name="locncode">Lokace.</param>
        /// <param name="skl_id">ID skladu.</param>
        /// <returns>Odpovidajici zaznamy.</returns>
        Fask.Server.Interfaces.DataSets.Location Lokace_GeMaterial(string itemnmbr, string serltnum, string locncode, string skl_id);

        /// <summary>
        /// Zobrazi material serazeny podle expirace od nejstarsiho (tabulka get_os_ms).
        /// </summary>
        /// <param name="itemnmbr">Oznaceni materialu.</param>
        /// <param name="skl_id">ID skladu.</param>
        /// <returns>Odpovidajici zaznamy.</returns>
        Fask.Server.Interfaces.DataSets.Location Lokace_ShowOldestMaterial(string itemnmbr, string skl_id);

        /// <summary>
        /// Zobrazi material serazeny podle expirace od nejstarsiho (tabulka get_os_ms).
        /// </summary>
        /// <param name="itemnmbr">Oznaceni materialu.</param>
        /// <param name="skl_id">ID skladu.</param>
        /// <param name="NumberOfRecords">Pocet zaznamu.</param>
        /// <returns>Odpovidajici zaznamy.</returns>
        Fask.Server.Interfaces.DataSets.Location Lokace_ShowOldestMaterial(string itemnmbr, string skl_id, uint NumberOfRecords);

        /// <summary>
        /// Funkce pro zjisteni zda je material na dane lokaci - pro defregmentaci.
        /// </summary>
        /// <param name="itemnmbr">Oznaceni materialu</param>
        /// <param name="locncode">ID Lokace.</param>
        /// <param name="skl_id">ID skladu.</param>
        /// <returns>Priznak existence.</returns>
        bool Lokace_CheckMaterialInLocation(string itemnmbr, string locncode, string skl_id);

        /// <summary>
        /// Funkce pro online kontrolu lokace.
        /// </summary>
        /// <param name="Regal">Oznaceni regalu</param>
        /// <returns>Priznak existence</returns>
        Fask.Server.Interfaces.Classes.StatusOverLokace Lokace_OverLokace(string skl_id, string locncode);

        /// <summary>
        /// Funkce pro online kontrolu lokace.
        /// </summary>
        /// <param name="itemnmbr">Oznaceni materialu.</param>
        /// <param name="serltnum">Sarze.</param>
        /// <param name="locncode">ID lokace.</param>
        /// <param name="skl_id">ID skladu.</param>
        /// <param name="qtyshppd">Mnozstvi.</param>
        /// <param name="doc_id">Typ dokladu.</param>
        /// <param name="locationType">Typ lokace (zdrojova, cilova)</param>
        /// <param name="recordType">Typ zaznamu (urcuje se z czmst092 - cfg_lok_mech_pohyb_type)</param>
		/// <returns>StatusOverLokace - nese informace o stavu</returns>
        Fask.Server.Interfaces.Classes.StatusOverLokace Lokace_OverLokace(string itemnmbr, string serltnum, string locncode, string skl_id, decimal qtyshppd, string doc_id, Fask.Server.Interfaces.Classes.TYPLokace locationType, Fask.Server.Interfaces.Lokace.TypeOfRecord recordType);

        /// <summary>
        /// Funkce pro navrat prijmovych lokaci podle ID skladu
        /// </summary>
        /// <param name="skl_id">ID skladu</param>
        /// <returns>Prijmove lokace</returns>
        Fask.Server.Interfaces.DataSets.Location Lokace_ShowReceiveLocations(string skl_id);

        /// <summary>
        /// Zjisteni, zdali varianta lokace existuje.
        /// </summary>
        /// <param name="itemnmbr">Polozka ID</param>
        /// <param name="skl_id">Sklad ID</param>
        /// <param name="locncode">Lokace</param>
        /// <returns>True-OK, False-Chyba</returns>
        bool Lokace_VariantySortimentExists(string itemnmbr, string skl_id, string locncode);

        /// <summary>
        /// Zjisteni, zdali varianta lokace existuje.
        /// </summary>
        /// <param name="itemnmbr">Polozka ID</param>
        /// <param name="skl_id">Sklad ID</param>
        /// <param name="locncode">Lokace</param>
		/// <returns>True-OK, False-Chyba</returns>
        bool Lokace_VariantySortimentExistsDefault(string itemnmbr, string skl_id, string locncode);

        /// <summary>
        /// Nastaveni varianty lokace.
        /// </summary>
        /// <param name="itemnmbr">Polozka ID</param>
        /// <param name="skl_id">Sklad ID</param>
        /// <param name="locncode">Lokace</param>
        /// <param name="type">Typ</param>
		/// <returns>True-OK, False-Chyba</returns>
        bool Lokace_VariantySortimentNastav(byte idterminal, int userid, string itemnmbr, string skl_id, string locncode, string type);

        /// <summary>
        /// Ziskani informaci varianty lokaci pro polozku skladu
        /// </summary>
        /// <param name="itemnmbr">Polozka ID</param>
        /// <param name="skl_id">Sklad ID</param>
		/// <returns>Location- dotažene lokace</returns>
        DataSets.Location Lokace_VariantySortimentGet(string itemnmbr, string skl_id);

		///// <summary>
		///// Zobrazi mnozstvi materialu ve skladu.
		///// </summary>
		///// <param name="itemnmbr">Oznaceni materialu.</param>
		///// <param name="skl_id">ID skladu.</param>
		///// <param name="NumberOfRecords">Pocet zaznamu.</param>
		///// <returns>Odpovidajici zaznamy.</returns>
		////Fask.Server.Interfaces.DataSets.Location Lokace_ShowMaterial(string itemnmbr, string skl_id, uint NumberOfRecords);

    }
}
