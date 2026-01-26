using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Fask.Server.Interfaces.Classes_Vyroba;
using Fask.Server.Interfaces.DataSets;

namespace Fask.Server.Interfaces.Vyroba
{
    public interface IProduction
    {
        /// <summary>
        /// Kontrolni funkce pro odvadeni vzhledem ke stroji. true - uzivatel muze soucasne odvadet na vice strojich, false - nemuze
        /// </summary>
        bool AllowUserProductionOnMoreMachines { get; set; }

        /// <summary>
        /// Aktualizuje hlavicky, polozky a ciselniky Vyroby
        /// </summary>
        /// <param name="dsvyroba">Databaze vyroby</param>
        void GetTables(Fask.Interfaces.DataSets.Vyroba dsvyroba);

        /// <summary>
        /// Aktualizuje pouze ciselniky Vyroby
        /// </summary>
        /// <param name="dsvyroba">Databaze vyroby</param>
        void GetCiselniky(Fask.Interfaces.DataSets.Vyroba dsvyroba);

        /// <summary>
        /// Aktualizuje hlavicky vyroby, ktere jsou volne ke stazeni terminalem
        /// </summary>
        /// <param name="terminalID">ID terminalu, ktery zada o hlavicky</param>
        /// <returns>Tabulka hlavicek vyrobnich prikazu</returns>
        Fask.Interfaces.DataSets.Vyroba GetHlavicky(byte terminalID);

        /// <summary>
        /// Vraci polozky vyrobniho prikazu a tento vyrobni prikaz blokuje pro tento terminal
        /// </summary>
        /// <param name="terminalID">Terminal ID, ktery vyrobni prikaz zpracovava a blokuje pro sebe</param>
        /// <returns>Tabulka s polozkami vyrobniho prikazu</returns>
        Fask.Interfaces.DataSets.Vyroba GetPolozky(VyrobniPrikazHlavicka hlavicka);

        /// <summary>
        /// Funknce zablokuje hlavicku a polozky vyrobniho prikazu pro terminal
        /// </summary>
        /// <param name="terminalID">ID Terminalu, ktery polozky blokuje(zpracovava)</param>
        /// <param name="pozadavekBlokace">Typ pozadavku blokace zaznamu</param>
        /// <returns>True=zablokovano, False=nelze zablokovat</returns>
        /// <exception cref="">Vyjimka v pripade chyby</exception>
        bool VyrobniPrikazBlokace(byte terminalID, VyrobniPrikazHlavicka vyrobniprikaz, BlokaceTyp pozadavekBlokace);

        /// <summary>
        /// Vraci zpet terminalu poslednich n zaznamu pro uzivatele z produkcnich dat.
        /// </summary>
        /// <param name="UserID">ID uzivatele/obsluhy</param>
        /// <param name="nLastActions">pocet zaznamu, ktere se maji vratit</param>
        /// <returns>Vraci naplnenou tabulku Production zaznamy [razeni dateeve desc a 1.je korekce casu]</returns>
        Fask.Interfaces.DataSets.Vyroba ProductionLastAction(string UserID, string MachineID, bool? Corrections, int nLastActions);

        /// <summary>
        /// Vraci zpet terminalu poslednich n zaznamu z produkcnich dat.
        /// </summary>
        /// <param name="UserID">ID uzivatele/obsluhy</param>
        /// <param name="MachineID">ID stroje</param>
        /// <param name="nLastActions">pocet zaznamu</param>
        /// <returns>Vraci naplnenou tabulku Production zaznamy [razeni dateeve desc]</returns>
        [Obsolete("Nahrazeno metodou [ProductionHistoryFilter]", false)]
        Fask.Interfaces.DataSets.Vyroba ProductionHistory(string UserID, string MachineID, string sopnumbe, DateTime? dateFrom, DateTime? dateTo, int nLastActions);

        /// <summary>
        /// Vraci zpet terminalu zaznamy dle nastavenych filtru. Pokud nejsou filtry nastaveny, pak se vraci zaznamy zpet o pocet pozadovanych hodin od ted, dle parametru nLastHours
        /// </summary>
        /// <param name="filtersHistory">Filtry historie</param>
        /// <param name="nLastHours">Pocet hodin zpet, ktere budou vraceny terminalu</param>
        /// <returns>Plni datatable VyrobaDataSet.Production pozadovanymi daty(muze jich byt opravdu hodne ... )</returns>
        Fask.Interfaces.DataSets.Vyroba ProductionHistoryFilter(FiltersHistory filtersHistory, int nLastHours);

        /// <summary>
        /// Vraci zpet terminalu zaznamy dle nastavenych filtru. Pokud nejsou filtry nastaveny, pak se vraci zaznamy zpet o pocet pozadovanych hodin od ted, dle parametru nLastHours
        /// </summary>
        /// <param name="filtersHistory">Filtry historie</param>
        /// <param name="nLastHours">Pocet hodin zpet, ktere budou vraceny terminalu</param>
        /// <returns>Plni datatable VyrobaDataSet.Production pozadovanymi daty(muze jich byt opravdu hodne ... )</returns>
        Fask.Interfaces.DataSets.Vyroba_StatistikaOdvadeni Production_Filter(FiltersHistory filtersHistory);


        /// <summary>
        /// Returns last opened Production for user and machine
        /// </summary>
        /// <param name="userID">User</param>
        /// <param name="MachineID">Machine</param>
        /// <returns>Dataset Production.DataServices.VyrobaDataSet.ProductionDataTable filled with last opened production for user and machine</returns>
        Fask.Interfaces.DataSets.Vyroba OpenedProduction(string userID, string MachineID);

        /// <summary>
        /// Returns all opened Productions.
        /// </summary>
        /// <returns></returns>
        Fask.Interfaces.DataSets.Vyroba AllOpenedProductions();

        /// <summary>
        /// Returns last opened Corrections for user and machine
        /// </summary>
        /// <param name="userID">User</param>
        /// <param name="MachineID">Machine</param>
        /// <returns>Dataset Production.DataServices.VyrobaDataSet.ProductionDataTable filled with last opened corrections for user and machine</returns>
        Fask.Interfaces.DataSets.Vyroba OpenedCorrection(string userID, string MachineID);


        Fask.Interfaces.DataSets.Vyroba Soubeh(Guid soubehGUID);

        /// <summary>
        /// Vraci datum a cas posledniho zapsani udalosti
        /// </summary>
        /// <param name="UserID">ID uzivatele/obsluhy pro akci z production</param>
        /// <returns></returns>
        DateTime? UserLastAction(string UserID);

        /// <summary>
        /// Aktualizuje do databaze vsechny tabulky vyroby
        /// </summary>
        /// <param name="dsvyroba">Databaze k aktualizaci</param>
        void UpdateTables(Fask.Interfaces.DataSets.Vyroba dsvyroba);

        /// <summary>
        /// Nahraje nove zaznamy do DB
        /// </summary>
        /// <param name="dtProduction">Vyrobni data</param>
        void UpdateProduction(Fask.Interfaces.DataSets.Vyroba.ProductionDataTable dtProduction);

        /// <summary>
        /// Nahraje nove zaznamy do DB
        /// </summary>
        /// <param name="dtProduction_Sources"></param>
        void UpdateProduction_Sources(Fask.Interfaces.DataSets.Vyroba.Production_SourcesDataTable dtProduction_Sources);

        /// <summary>
        /// Nahraje nove zaznamy do DB
        /// </summary>
        /// <param name="dtUserEvents">Udalosti obsluhy</param>
        void UpdateUserEvents(Fask.Interfaces.DataSets.Vyroba.UserEventsDataTable dtUserEvents);

        /// <summary>
        /// Nahraje nove zaznamy do DB
        /// </summary>
        /// <param name="dtProduction_SN">Vyrobni data</param>
        void UpdateProduction_SN(Fask.Interfaces.DataSets.Vyroba.Production_SNDataTable dtProduction_SN);

        //Report_UserDay Get_Report_UserDay(string UserID, DateTime? datetimeBetweenLogInOut);
        Report_UserDay Get_Report_UserDay(string UserID, DateTime datetimeLogin, DateTime datetimeLastOperation);

        //15.1.2019 - funkionalita online pro reseni MTJ pro fy Kruzik
        /// <summary>
        /// Vraci data s hlavickou operace a radkem vyrobniho prikazu ...
        /// </summary>
        /// <param name="operaceID">zadany carovy kod operace</param>
        /// <param name="terminalID">cislo terminalu, ktery akci vyvolava</param>
        /// <returns>Data hlavicky prikazu a radku operace</returns>
        Fask.Interfaces.DataSets.Vyroba Vyroba_Online_CheckOperation(string operaceID, string terminalID, out ProductionState productionStateEnabled);

        //15.1.2019 - funkionalita online pro reseni MTJ pro fy Kruzik
        /// <summary>
        /// Vraci data s hlavickou operace a radkem vyrobniho prikazu ...
        /// </summary>
        /// <param name="operaceID">zadany carovy kod operace</param>
        /// <param name="terminalID">cislo terminalu, ktery akci vyvolava</param>
        /// <returns>Data hlavicky prikazu a radku operace</returns>
        bool Vyroba_Online_WriteOperation(ProductionObject productionObject, out string message);

        /// <summary>
        /// Metoda pro vzgenerovani šarže
        /// </summary>
        /// <param name="smenaID">ID Směny</param>
        /// <param name="userID">ID uživatele</param>
        /// <param name="linkaID">ID Linky</param>
        /// <returns></returns>
        string ReturnSarze(string smenaID, string userID, string linkaID, decimal qty, string ITEMNMBR);
    }
}
