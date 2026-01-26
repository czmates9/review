using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fask.Server.Interfaces.Classes;

namespace Fask.Server.Interfaces.Servis
{
    public interface IServis
    {
        // Ciselniky a stavy

        /// <summary>
        /// Pripravuje ciselnik zdroju pro terminal
        /// </summary>
        /// <param name="terminal">Cislo terminalu</param>
        /// <param name="so">Stav pripravy ciselniku</param>
        /// <returns>Servis dataset</returns>
        Fask.DataSets.Servis Servis_Prepare_Ciselniky(Terminal terminal, ref StatusObject so);

        /// <summary>
        /// Pripravuje ciselnik definic moznych stavu (Stavy, Cinnosti, ...)
        /// </summary>
        /// <param name="terminal"></param>
        /// <param name="so"></param>
        /// <returns>Servis dataset</returns>
        Fask.DataSets.Servis Servis_Prepare_Stavy(Terminal terminal, ref StatusObject so);

        /// <summary>
        /// Pripravuje ciselnik zvolene dynamicke tabulky.
        /// </summary>
        /// <param name="terminal"></param>
        /// <param name="so"></param>
        /// <param name="tableName">Nazev tabulky</param>
        /// <returns>Servis dataset</returns>
        Fask.DataSets.Servis Servis_Prepare_Dynamic_Table(Terminal terminal, ref StatusObject so, string tableName);

        // Zpracovani dat 

        /// <summary>
        /// Provede aktualizaci stavu zdroje v databazi
        /// </summary>
        /// <param name="zdroj">Aktualizovany zdroj</param>
        /// <returns>vraci stav aktualizace a zpet posledni platne informace o zdroji.</returns>
        StatusInfo Servis_ProcessState(ref Zdroj zdroj);

        /// <summary>
        /// Vraci data historie zvoleneho zdroje
        /// </summary>
        /// <param name="terminal">terminal</param>
        /// <param name="user">uzivatel</param>
        /// <param name="zdroj">zdroj historie</param>
        /// <param name="pocetZaznamu">pocet zaznamu historie</param>
        /// <param name="history">Data historie zdroje</param>
        /// <returns>Servis dataset</returns>
        Fask.DataSets.Servis Servis_ZdrojHistory(Terminal terminal, User user, Zdroj zdroj, int pocetZaznamu);

        /// <summary>
        /// Nahraje nove zaznamy do DB
        /// </summary>
        /// <param name="dtZdrojStav">Data z tabulky ZdrojPohyb</param>
        void UpdateZdrojPohyb(Fask.DataSets.Servis.CZMST_Servis_ZdrojPohybDataTable dtZdrojStav);

        /// <summary>
        /// Vygenerovani nove davky (CZMST_Servis_Predloha)
        /// </summary>
        /// <param name="document_number">ID dokladu</param>
        /// <param name="odb_id">ID odberatele</param>
        /// <param name="okruhid">ID okruhu</param>
        /// <returns></returns>
        Fask.Server.Interfaces.Classes.StatusInfo Servis_GenerateDavka(User user, string document_number, string odb_id, string okruhid);

        /// <summary>
        /// Potvrzeni stazeni davky (nastaveni parametru Rozpracovano v rozmezi 1-99 podle ID terminalu)
        /// </summary>
        /// <param name="davka">Cislo davky.</param>
        /// <param name="terminal">ID terminalu.</param>
        /// <returns></returns>
        bool Servis_GetDavkaReceived(Davka davka, Terminal terminal);


        /// <summary>
        /// Vraci vsechny vygenerovane davky.
        /// </summary>
        /// <param name="terminal">ID terminalu.</param>
        /// <returns></returns>
        Fask.Server.Interfaces.DataSets.ServisDavky Servis_GetDavky(Terminal terminal);

        /// <summary>
        /// Vraci data davky.
        /// </summary>
        /// <param name="davka">Cislo davky.</param>
        /// <param name="terminal">ID terminalu.</param>
        /// <returns></returns>
        Fask.DataSets.Servis Servis_GetDavka(Fask.Server.Interfaces.Classes.Davka davka, Fask.Server.Interfaces.Classes.Terminal terminal);

        // ************** Metody pro zpracovani dat ************ //
        /// <summary>
        /// Zpracovani davky a nastaveni 'Rozpracovano' na hodnotu 100+.
        /// </summary>
        /// <param name="davka">Cislo davky.</param>
        /// <param name="terminal">ID terminalu.</param>
        /// <param name="servisdata">Data pro vlozeni.</param>
        /// <param name="processServisState">Status (vratit, zpracovat)</param>
        /// <returns></returns>
        StatusObject Servis_Process(Davka davka, Terminal terminal, Fask.DataSets.Servis servisdata, ProcessState processServisState);

        /// <summary>
        /// Volani procedury po zpracovani dat
        /// </summary>
        /// <param name="davka">Cislo davky.</param>
        /// <returns></returns>
        bool Servis_AfterProcessedAction(Davka davka);

        /// <summary>
        /// Stornovani nezpracovane davky (nastaveni Rozpracovano na 100+)
        /// </summary>
        /// <param name="davka">Cislo davky.</param>
        /// <param name="user">ID uzivatele.</param>
        /// <param name="terminal">ID terminalu.</param>
        /// <param name="password">Heslo pro stornovani.</param>
        /// <returns></returns>
        StatusObject Servis_Storno_Davka(Davka davka, User user, Terminal terminal, string password);
    }
}
