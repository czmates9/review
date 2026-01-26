using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Servis
{
    public interface IServis : IMES
    {
        /// <summary>
        /// Connection string pro pripojeni k DB.
        /// </summary>
        string ConnectionString { get; set; }
        /**************** ZDROJE ****************/
        /// <summary>
        /// Vraci seznam zdroju.
        /// </summary>
        /// <returns></returns>
        Fask.Interfaces.DataSets.Servis GetZdroje();

        /// <summary>
        /// Vraci zdroj podle ID.
        /// </summary>
        /// <param name="id">ID zdroje.</param>
        /// <returns>Pokud nalezeno, vraci zaznam, jinak null</returns>
        Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojRow GetZdrojByID(string id);

        /// <summary>
        /// Vraci filtrovane zdroje.
        /// </summary>
        /// <param name="id">ID zdroje.</param>
        /// <returns>Pokud nalezeno, vraci zaznam, jinak null</returns>
        Fask.Interfaces.DataSets.Servis GetFiltrovaneZdroje(Fask.Interfaces.Filtry.ZdrojeListFiltr filtr);

        /// <summary>
        /// Smaže zdroj a jeho navaznosti (tabulka ZdrojStav).
        /// </summary>
        /// <param name="id">ID zdroje.</param>
        /// <returns></returns>
        bool DeleteZdroj(string id);

        /// <summary>
        /// Provede duplikaci zdroje dle zadaneho id
        /// </summary>
        /// <param name="id">id duplikovaneho zdroje</param>
        /// <param name="newid">id noveho zdroje</param>
        /// <returns>Pokud projde tak true, jinak false</returns>
        bool DuplicateZdroj(Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojRow zdrojRow, string newid);

        /// <summary>
        /// Aktualizuje informace o zdroji.
        /// </summary>
        /// <param name="zdrojRow">Zdroj, ktery se bude aktualizovat</param>
        /// <returns></returns>
        bool UpdateZdroj(Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojRow zdrojRow);

        /// <summary>
        /// Vloží do DB nový záznam zdroje.
        /// </summary>
        /// <param name="zdrojRow">Zdroj, který se má vložit.</param>
        /// <returns>True - vše v pořádku, False - chyba</returns>
        bool InsertZdroj(Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojRow zdrojRow);

        /**************** CINNOSTI ****************/
        /// <summary>
        /// Vraci seznam cinnosti.
        /// </summary>
        /// <returns></returns>
        Fask.Interfaces.DataSets.Servis GetCinnosti();

        /// <summary>
        /// Vraci cinnost podle ID.
        /// </summary>
        /// <param name="id">ID cinnosti.</param>
        /// <returns>Pokud nalezeno, vraci zaznam, jinak null</returns>
        Fask.Interfaces.DataSets.Servis.CZMST_Servis_CinnostRow GetCinnostByID(string id);

        /// <summary>
        /// Smaže cinnost a jeho navaznosti (tabulka CinnostNext).
        /// </summary>
        /// <param name="id">ID zdroje.</param>
        /// <returns></returns>
        bool DeleteCinnost(string id);

        /// <summary>
        /// Aktualizuje informace o cinnosti.
        /// </summary>
        /// <param name="zdrojRow">Cinnost, ktera se bude aktualizovat</param>
        /// <returns></returns>
        bool UpdateCinnost(Fask.Interfaces.DataSets.Servis.CZMST_Servis_CinnostRow cinnostRow);

        /// <summary>
        /// Vloží do DB nový záznam cinnosti.
        /// </summary>
        /// <param name="zdrojRow">Cinnost, který se má vložit.</param>
        /// <returns>True - vše v pořádku, False - chyba</returns>
        bool InsertCinnost(Fask.Interfaces.DataSets.Servis.CZMST_Servis_CinnostRow cinnostRow);

        /**************** STAVY ****************/
        /// <summary>
        /// Vraci seznam stavu.
        /// </summary>
        /// <returns></returns>
        Fask.Interfaces.DataSets.Servis GetStavy();

        /// <summary>
        /// Vraci stav podle ID.
        /// </summary>
        /// <param name="id">ID stavu.</param>
        /// <returns>Pokud nalezeno, vraci zaznam, jinak null</returns>
        Fask.Interfaces.DataSets.Servis.CZMST_Servis_StavRow GetStavByID(string id);

        /// <summary>
        /// Smaže stav a jeho navaznosti  z tabulky StavNext (maze podle ID a IDNext).
        /// </summary>
        /// <param name="id">ID stavu.</param>
        /// <returns></returns>
        bool DeleteStav(string id);

        /// <summary>
        /// Aktualizuje informace o stavu.
        /// </summary>
        /// <param name="zdrojRow">Stav, ktery se bude aktualizovat</param>
        /// <returns></returns>
        bool UpdateStav(Fask.Interfaces.DataSets.Servis.CZMST_Servis_StavRow stavRow);

        /// <summary>
        /// Vloží do DB nový záznam stavu.
        /// </summary>
        /// <param name="zdrojRow">Stav, který se má vložit.</param>
        /// <returns>True - vše v pořádku, False - chyba</returns>
        bool InsertStav(Fask.Interfaces.DataSets.Servis.CZMST_Servis_StavRow stavRow);

        /**************** STAVY NEXT ****************/
        /// <summary>
        /// Vloží do DB nový záznam StavNext.
        /// </summary>
        /// <param name="id">ID stavu.</param>
        /// <returns></returns>
        bool InsertStavNext(string stavID, string stavNextID);

        /// <summary>
        /// Vloží do DB záznamy StavNext z datasetu v transakci a soucasne overi, zdali jiz nejsou ulozeny.
        /// </summary>
        /// <param name="id">ID stavu.</param>
        /// <returns></returns>
        bool InsertStavNext(string stavID, Fask.Interfaces.DataSets.Servis dsStavyNext);

        /// <summary>
        /// Smaže StavNext.
        /// </summary>
        /// <param name="id">ID stavu.</param>
        /// <returns></returns>
        bool DeleteStavNext(string stavID, string stavNextID);

        /// <summary>
        /// Vraci seznam vsech navazujicich stavu podle puvodniho stavu
        /// </summary>
        /// <param name="stavID">Stav, ze ktereho se zjistuji navaznosti.</param>
        /// <returns>Seznam navazujicich stavu.</returns>
        Fask.Interfaces.DataSets.Servis GetStavyNextByStavID(string stavID);

        /**************** ČINNOSTI NEXT ****************/
        /// <summary>
        /// Vloží do DB nový záznam CinnostNext.
        /// </summary>
        /// <param name="id">ID činnosti.</param>
        /// <param name="IDValue">ID value.</param>
        /// <returns></returns>
        bool InsertCinnostNext(string cinnostID, string cinnostNextID, string IDValue);

        /// <summary>
        /// Vloží do DB záznamy CinnostNext z datasetu v transakci a soucasne overi, zdali jiz nejsou ulozeny.
        /// </summary>
        /// <param name="id">ID cinnosti.</param>
        /// <param name="IDValue">ID value.</param>
        /// <returns></returns>
        bool InsertCinnostNext(string cinnostID, string IDValue, Fask.Interfaces.DataSets.Servis dsCinnostiNext);

        /// <summary>
        /// Smaže CinnostNext.
        /// </summary>
        /// <param name="id">ID cinnosti.</param>
        /// <returns></returns>
        bool DeleteCinnostNext(string cinnostID, string cinnostNextID);

        /// <summary>
        /// Vraci vybranou CinnostNext.
        /// </summary>
        /// <param name="cinnostID">CinnostID.</param>
        /// <param name="cinnostNextID">CinnostNextID</param>
        /// <returns></returns>
        Fask.Interfaces.DataSets.Servis.CZMST_Servis_CinnostNextRow GetCinnostNextByID(string cinnostID, string cinnostNextID);

        /// <summary>
        /// Vraci seznam vsech navazujicich cinnosti podle puvodniho cinnosti
        /// </summary>
        /// <param name="cinnostID">Cinnost, ze ktere se zjistuji navaznosti.</param>
        /// <returns>Seznam navazujicich stavu.</returns>
        Fask.Interfaces.DataSets.Servis GetCinnostiNextByCinnostID(string cinnostID);

        /**************** ZDROJE STAVY ****************/
        /// <summary>
        /// Vraci seznam stavu zdroju.
        /// </summary>
        /// <returns></returns>
        Fask.Interfaces.DataSets.Servis GetZdrojeStavy();

        /// <summary>
        /// Vraci stav zdroje podle id zdroje.
        /// </summary>
        /// <param name="id">ID zdroje.</param>
        /// <returns>Pokud nalezeno, vraci zaznam, jinak null</returns>
        Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojStavRow GetZdrojStavByZdrojID(string id);

        /// <summary>
        /// Smaže zdroj a jeho navaznosti.
        /// </summary>
        /// <param name="id">ID zdroje.</param>
        /// <returns></returns>
        bool DeleteZdrojStavByZdrojID(string id);

        /// <summary>
        /// Aktualizuje informace o stavu zdroje.
        /// </summary>
        /// <param name="zdrojRow">ZdrojStav, ktery se bude aktualizovat</param>
        /// <returns></returns>
        bool UpdateZdrojStav(Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojStavRow zdrojStavRow);

        /// <summary>
        /// Vloží do DB nový záznam stavu zdroje.
        /// </summary>
        /// <param name="zdrojStavRow">ZdrojStav, který se má vložit.</param>
        /// <returns>True - vše v pořádku, False - chyba</returns>
        bool InsertZdrojStav(Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojStavRow zdrojStavRow);

        /**************** ZDROJ POHYB ****************/
        /// <summary>
        /// Vrací záznamy historie pohybů podle zadaného filtru.
        /// </summary>
        /// <param name="ZdrojPohybFiltr">Filtr záznamů.</param>
        Fask.Interfaces.DataSets.Servis GetFiltrovanyZdrojPohyb(Fask.Interfaces.Filtry.ZdrojePohybListFiltr ZdrojPohybFiltr);


        /**************** Report Sestava ****************/
        /// <summary>
        /// Vrací záznamy historie pohybů podle zadaného filtru.
        /// </summary>
        /// <param name="ZdrojPohybFiltr">Filtr záznamů.</param>
        System.Data.DataSet GetFiltrovanyZdrojPohyb(Fask.Interfaces.Filtry.ReportSestavaFiltr reportsestavaFiltr);

        /// <summary>
        /// Nastavuje priznak exportovano na zdrojich s uvedenym GUID.
        /// </summary>
        /// <param name="guidsExported">Exportovane radky pohybu</param>
        /// <param name="casExportu">cas exportu, null=neexportovano bude v db NULL</param>
        /// <returns></returns>
        //bool UpdateZdrojPohybExported(List<Guid> guidsExported, DateTime? casExportu);
        bool UpdateZdrojPohyb(Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojPohybDataTable zdrojPohybTable);


        /**************** Okruh ****************/
        /// <summary>
        /// Vraci seznam okruhu.
        /// </summary>
        /// <returns></returns>
        Fask.Interfaces.DataSets.Servis GetOkruhy();

        /// <summary>
        /// Vraci okruh podle id.
        /// </summary>
        /// <param name="id">ID okruhu.</param>
        /// <returns>Pokud nalezeno, vraci zaznam, jinak null</returns>
        Fask.Interfaces.DataSets.Servis.CZMST_Servis_OkruhRow GetOkruhByID(string id);

        /// Vraci okruh podle id.
        /// </summary>
        /// <param name="id">ID okruhu.</param>
        /// <returns>Pokud nalezeno, vraci zaznam, jinak null</returns>
        Fask.Interfaces.DataSets.Servis.CZMST_Servis_OkruhDataTable GetOkruhByOBDID(string id);

        /// <summary>
        /// Vraci okruh podle ZdrojSeznamID.
        /// </summary>
        /// <param name="id">Sloupec ZdrojSeznamID.</param>
        /// <returns>Pokud nalezeno, vraci zaznam, jinak null</returns>
        Fask.Interfaces.DataSets.Servis.CZMST_Servis_OkruhRow GetOkruhByZdrojSeznamID(string id);

        /// <summary>
        /// Smaže okruh a jeho navaznosti.
        /// </summary>
        /// <param name="okruhID">ID okruhu.</param>
        /// <param name="seznamID">ID seznamu.</param>
        /// <returns></returns>
        bool DeleteOkruhByID(string okruhID, string seznamID);

        /// <summary>
        /// Aktualizuje informace o okruhu.
        /// </summary>
        /// <param name="okruhRow">Okruh, ktery se bude aktualizovat</param>
        /// <returns></returns>
        bool UpdateOkruh(Fask.Interfaces.DataSets.Servis.CZMST_Servis_OkruhRow okruhRow);

        /// <summary>
        /// Vloží do DB nový záznam okruhu.
        /// </summary>
        /// <param name="okruhRow">Okruh, který se má vložit.</param>
        /// <returns>True - vše v pořádku, False - chyba</returns>
        bool InsertOkruh(Fask.Interfaces.DataSets.Servis.CZMST_Servis_OkruhRow okruhRow);

        /**************** Seznam zdroju ****************/
        /// <summary>
        /// Vraci kompletni zdroju.
        /// </summary>
        /// <returns></returns>
        Fask.Interfaces.DataSets.Servis GetZdrojSeznam();

        /// <summary>
        /// Vraci ZdrojSeznam podle id.
        /// </summary>
        /// <param name="seznamID">ID seznamu.</param>
        /// <returns>Pokud nalezeno, vraci zaznam, jinak null</returns>
        Fask.Interfaces.DataSets.Servis GetZdrojSeznamByID(string seznamID);

        /// <summary>
        /// Vraci ZdrojSeznam podle id seznamu a id zdroje.
        /// </summary>
        /// <param name="seznamID">ID seznamu.</param>
        /// <param name="zdrojID">ID zdroje.</param>
        /// <returns>Pokud nalezeno, vraci zaznam, jinak null</returns>
        Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojSeznamRow GetZdrojSeznamByIDAndZdrojID(string seznamID, string zdrojID);

        /// <summary>
        /// Smaže seznam a jeho navaznosti.
        /// </summary>
        /// <param name="seznamID">ID seznamu.</param>
        /// <returns></returns>
        bool DeleteZdrojSeznamByID(string seznamID);

        /// <summary>
        /// Smaže zdroj ve vybranem seznamu.
        /// </summary>
        /// <param name="seznamID">ID seznamu.</param>
        /// <param name="zdrojID">ID zdroje.</param>
        /// <returns></returns>
        bool DeleteZdrojSeznamByIDAndZdrojID(string seznamID, string zdrojID);

        /// <summary>
        /// Aktualizuje informace o seznamu.
        /// </summary>
        /// <param name="seznamRow">Seznam, ktery se bude aktualizovat</param>
        /// <returns></returns>
        bool UpdateZdrojSeznam(Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojSeznamRow seznamRow);

        /// <summary>
        /// Vloží do DB nový záznam seznamu.
        /// </summary>
        /// <param name="seznamRow">Seznam, který se má vložit.</param>
        /// <returns>True - vše v pořádku, False - chyba</returns>
        bool InsertZdrojSeznam(Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojSeznamRow seznamRow);

        /// <summary>
        /// Vloží do DB záznamy ZdrojSeznam z datasetu v transakci a soucasne overi, zdali jiz nejsou ulozeny.
        /// </summary>
        /// <param name="seznamID">ID okruhu.</param>
        /// <param name="dsZdrojSeznam">Data pro vlozeni.</param>
        /// <returns></returns>
        bool InsertZdrojSeznam(string seznamID, Fask.Interfaces.DataSets.Servis dsZdrojSeznam);

        /**************** Definice dynamicke tabulky ****************/
        /// <summary>
        /// Vraci seznam dynamickych tabulek.
        /// </summary>
        /// <returns></returns>
        Fask.Interfaces.DataSets.Servis GetDynamicTableDefinition();

        /// <summary>
        /// Vraci informace o dynamicke tabulce podle typeName.
        /// </summary>
        /// <param name="typeName">typeName tabulky (z cinnosti).</param>
        /// <returns>Pokud nalezeno, vraci zaznam, jinak null</returns>
        Fask.Interfaces.DataSets.Servis.CZMST_Servis_Dynamic_Table_DefinitionRow GetDynamicTableDefinitionByTypeName(string typeName);

        /// <summary>
        /// Smaže zaznam z definic dynamickych tabulek a soucasne smaze (drop) dynamickou tabulku.
        /// </summary>
        /// <param name="fullName">Cely nazev tabulky.</param>
        /// <param name="typeName">Zkratka tabulky (v cinnosti).</param>
        /// <returns></returns>
        bool DeleteDynamicTableDefinitionByID(string fullName, string typeName);

        /// <summary>
        /// Vloží do DB nový záznam definice dynamicke tabulky a soucasne vytvori danou tabulku..
        /// </summary>
        /// <param name="dynTableRow">Definice dynamicke tabulky.</param>
        /// <returns>True - vše v pořádku, False - chyba</returns>
        bool InsertDynamicTableDefinition(Fask.Interfaces.DataSets.Servis.CZMST_Servis_Dynamic_Table_DefinitionRow dynTableRow);

        /**************** Definice dynamicke tabulky ****************/
        /// <summary>
        /// Vraci vsechna data z dynamicke tabulky.
        /// </summary>
        /// <param name="tablename">nazev tabulky.</param>
        /// <returns></returns>
        Fask.Interfaces.DataSets.Servis GetDynamicTable(string tablename);

        /// <summary>
        /// Vraci informace o dynamicke tabulce podle typeName.
        /// </summary>
        /// <param name="tablename">typeName tabulky (z cinnosti).</param>
        /// <param name="id">ID, podle ktereho se hleda.</param>
        /// <returns>Pokud nalezeno, vraci zaznam, jinak null</returns>
        Fask.Interfaces.DataSets.Servis.CZMST_Servis_Dynamic_TableRow GetDynamicTableByID(string tablename, string id);

        /// <summary>
        /// Smaže zaznam z definic dynamickych tabulek a soucasne smaze (drop) dynamickou tabulku.
        /// </summary>
        /// <param name="dynTableRow">Udaj o dynamicke tabulce (nazev tabulky).</param>
        /// <param name="id">ID zaznamu v tabulce.</param>
        /// <returns></returns>
        bool DeleteDynamicTableByID(Fask.Interfaces.DataSets.Servis.CZMST_Servis_Dynamic_Table_DefinitionRow dynTableRow, string id);

        /// <summary>
        /// Vloží do DB nový záznam do tabulky.
        /// </summary>
        /// <param name="dynTableRow">Informace o dynamicke tabulce.</param>
        /// <param name="tableRow">Data, ktera se vkladaji.</param>
        /// <returns>True - vše v pořádku, False - chyba</returns>
        bool InsertDynamicTable(Fask.Interfaces.DataSets.Servis.CZMST_Servis_Dynamic_Table_DefinitionRow dynTableRow, Fask.Interfaces.DataSets.Servis.CZMST_Servis_Dynamic_TableRow tableRow);

        /// <summary>
        /// Aktualizuje záznam v dynamicke tabulce.
        /// </summary>
        /// <param name="dynTableRow">Informace o dynamicke tabulce.</param>
        /// <param name="tableRow">Data, ktera se vkladaji.</param>
        /// <returns>True - vše v pořádku, False - chyba</returns>
        bool UpdateDynamicTable(Fask.Interfaces.DataSets.Servis.CZMST_Servis_Dynamic_Table_DefinitionRow dynTableRow, Fask.Interfaces.DataSets.Servis.CZMST_Servis_Dynamic_TableRow tableRow);
    }
}
