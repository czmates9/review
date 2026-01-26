using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fask.Server.Interfaces.Classes;

namespace Fask.Server.Interfaces.Expedice
{
    /// <summary>
    /// Rozhrani pro vsechny typy databazi.
    /// </summary>
    public interface IExpedice
    {
        /// <summary>
        /// Vytvori hlavicku predlohy podle zaslanych dat.
        /// </summary>
        /// <param name="terminal">Terminal</param>
        /// <param name="user">Uzivatel</param>
        /// <param name="hlavicka">Hlavicka, ktera se ma vytvorit</param>
        /// <returns></returns>
        StatusInfo Expedice_Baleni_Hlavicka_Add(Terminal terminal, User user, Sklad sklad, Fask.Server.Interfaces.DataSets.ExpediceBaleniHlavicky hlavicka);

        /// <summary>
        /// Odstrani hlavicku, vcetne vsech polozek predlohy.
        /// </summary>
        /// <param name="terminal">Terminal</param>
        /// <param name="user">Uzivatel</param>
        /// <param name="hlavicka">Hlavicka</param>
        /// <returns></returns>
        StatusInfo Expedice_Baleni_Hlavicka_Del(Terminal terminal, User user, Sklad sklad, Guid hlavickaID);

        // ************** Metody pro zisakni predlohy **************** //
        /// <summary>
        /// Vraci seznam hlavicek pro sklad a uzivatele/terminal??.
        /// </summary>
        /// <param name="terminal">Terminal</param>
        /// <param name="user">Uzivatel</param>
        /// <param name="sklad">Sklad</param>
        /// <returns></returns>
        Fask.Server.Interfaces.DataSets.ExpediceBaleniHlavicky Expedice_Baleni_GetHlavicky(Terminal terminal, User user, Sklad sklad);

        /// <summary>
        /// Vraci seznam veskerych polozek uvnitr davky (hlavickyID)
        /// </summary>
        /// <param name="terminal"></param>
        /// <param name="user"></param>
        /// <param name="sklad"></param>
        /// <param name="hlavickaID"></param>
        /// <returns></returns>
        Fask.Server.Interfaces.DataSets.ExpediceBaleni Expedice_Baleni_GetPolozky(Terminal terminal, User user, Sklad sklad, Guid hlavickaID);

        /// <summary>
        /// Vraci informace o polozce podle caroveho kodu.
        /// </summary>
        /// <param name="terminal"></param>
        /// <param name="user"></param>
        /// <param name="skl_id"></param>
        /// <param name="barcode"></param>
        /// <param name="itemnmbr"></param>
        /// <param name="serltnum"></param>
        /// <returns></returns>
        Fask.Server.Interfaces.DataSets.ExpediceBaleni Expedice_Baleni_Polozka_Get(Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.User user, string skl_id, string barcode, string itemnmbr, string serltnum);

        // ************** Metody pro zpracovani dat ************ //
        StatusObject Expedice_Baleni_Process(
            Guid hlavicka, User user, Terminal terminal, Sklad sklad,
            Fask.Server.Interfaces.DataSets.ExpediceBaleni expedicedata,
            ProcessState processExpediceState);
        bool Expedice_Baleni_AfterProcessedAction(Guid hlavicka);

        // ************** Doplnujici metody ********* //
        /// <summary>
        /// Provede se stornovani nasnimanych dat a nastaveni Rozpracovano na hodnotu 4
        /// </summary>
        /// <param name="davka">ID davky</param>
        /// <param name="terminal">Terminal</param>
        /// <param name="password">Heslo pro stornovani dat</param>
        /// <returns></returns>
        StatusInfo Expedice_Baleni_Storno_Hlavicka(Guid hlavickaID, Terminal terminal, User user, string password);

        // ************** Online zapisy stavu prijmu ************* //
        StatusInfo Expedice_Baleni_Polozka_Add(Guid hlavickaID, Terminal terminal, User user, Fask.Server.Interfaces.Classes.Sklad sklad, Fask.Server.Interfaces.DataSets.ExpediceBaleni expediceRows);
        StatusInfo Expedice_Baleni_Polozka_Del(Guid hlavickaID, Terminal terminal, User user, Guid polozkaID);

        // ************** Online generovani SSCC ************* //
        /// <summary>
        /// Vraci nove vygenerovany SSCC kod
        /// </summary>
        /// <param name="terminal"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        Fask.Server.Interfaces.BarCodes.SSCC Expedice_SSCC_Generovat(Terminal terminal, User user);

        /// <summary>
        /// Vyhledani SSCC kodu podle zadane hodnoty
        /// </summary>
        /// <param name="terminal"></param>
        /// <param name="user"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        Fask.Server.Interfaces.BarCodes.SSCC Expedice_SSCC_Get(Terminal terminal, User user, string skl_id, Guid hlavickaID, string code);

        /******************************************************/
        /// <summary>
        /// Vytvori hlavicku predlohy podle zaslanych dat.
        /// </summary>
        /// <param name="terminal">Terminal</param>
        /// <param name="user">Uzivatel</param>
        /// <param name="hlavicka">Hlavicka, ktera se ma vytvorit</param>
        /// <returns></returns>
        StatusInfo Expedice_Hlavicka_Add(Terminal terminal, User user, Sklad sklad, Fask.Server.Interfaces.DataSets.ExpediceHlavicky hlavicka);

        /// <summary>
        /// Odstrani hlavicku, vcetne vsech polozek predlohy.
        /// </summary>
        /// <param name="terminal">Terminal</param>
        /// <param name="user">Uzivatel</param>
        /// <param name="hlavicka">Hlavicka</param>
        /// <returns></returns>
        StatusInfo Expedice_Hlavicka_Del(Terminal terminal, User user, Sklad sklad, Guid hlavickaID);

        // ************** Metody pro zisakni predlohy **************** //
        /// <summary>
        /// Vraci seznam hlavicek pro sklad a uzivatele/terminal??.
        /// </summary>
        /// <param name="terminal">Terminal</param>
        /// <param name="user">Uzivatel</param>
        /// <param name="sklad">Sklad</param>
        /// <returns></returns>
        Fask.Server.Interfaces.DataSets.ExpediceHlavicky Expedice_GetHlavicky(Terminal terminal, User user, Sklad sklad);

        Fask.Server.Interfaces.DataSets.Expedice Expedice_GetPalety(Terminal terminal, User user, Sklad sklad, Guid hlavickaID);

        Fask.Server.Interfaces.DataSets.Expedice Expedice_Paleta_Get(Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.User user, string skl_id, string nmbrpal);

        /// <summary>
        /// Vraci seznam veskerych polozek uvnitr davky (hlavickyID)
        /// </summary>
        /// <param name="terminal"></param>
        /// <param name="user"></param>
        /// <param name="sklad"></param>
        /// <param name="hlavickaID"></param>
        /// <returns></returns>
        Fask.Server.Interfaces.DataSets.Expedice Expedice_GetPolozky(Terminal terminal, User user, Sklad sklad, Guid hlavickaID);

        //// ************** Metody pro zpracovani dat ************ //
        StatusObject Expedice_Process(
            Guid hlavicka, User user, Terminal terminal, Sklad sklad,
            ProcessState processExpediceState);
        bool Expedice_AfterProcessedAction(Guid hlavicka);

        //// ************** Doplnujici metody ********* //
        ///// <summary>
        ///// Provede se stornovani nasnimanych dat a nastaveni Rozpracovano na hodnotu 4
        ///// </summary>
        ///// <param name="davka">ID davky</param>
        ///// <param name="terminal">Terminal</param>
        ///// <param name="password">Heslo pro stornovani dat</param>
        ///// <returns></returns>
        //StatusInfo Expedice_Storno_Hlavicka(Guid hlavickaID, Terminal terminal, User user, string password);

        //// ************** Online zapisy stavu prijmu ************* //
        StatusInfo Expedice_Polozka_Add(Guid hlavicka, Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.User user, Fask.Server.Interfaces.Classes.Sklad sklad, string nmbrpal);
        StatusInfo Expedice_Polozka_Del(Guid hlavicka, Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.User user, string nmbrpal);
    }
}
