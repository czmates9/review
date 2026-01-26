using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fask.Server.Interfaces.Classes;

namespace Fask.Server.Interfaces.Expedice
{
    /// <summary>
    /// Interface pro Expedici.
    /// </summary>
    public interface IExpedice
    {

        // ************** Metody pro pripravu transakce generovani dat **************** //
        /// <summary>
        /// Generuje data predlohy
        /// </summary>
        /// <param name="objednavka"></param>
        /// <returns></returns>
        StatusInfo Expedice_GenerateDavka(Objednavka objednavka, Sklad sklad);


        /// <summary>
        /// Metoda pro Tisk z Baleni
        /// </summary>
        /// <param name="guidHlavickaBaleni">Guid hlavičky</param>
        /// <param name="NMBRPAL">číslo palety</param>
        /// <returns>Data pro tisk</returns>
        Fask.Server.Interfaces.DataSets.ExpediceBaleni.CZMST_Expedice_Baleni_Polozky_TISKDataTable Expedice_Baleni_Tisk(Guid guidHlavickaBaleni, string NMBRPAL);

		/// <summary>
		/// Metoda pro přidání hlavičky do Baleni 
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <param name="user">Uživatel</param>
		/// <param name="sklad">Sklad</param>
		/// <param name="hlavicka">Položky pro pridani</param>
		/// <returns>StatusInfo - objekt ktery nese info o stavu</returns>
        StatusInfo Expedice_Baleni_Hlavicka_Add(Terminal terminal, User user, Sklad sklad, Fask.Server.Interfaces.DataSets.ExpediceBaleniHlavicky hlavicka);

		/// <summary>
		/// Metoda která smaže zaznamy balení
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <param name="user">Uživatel</param>
		/// <param name="sklad">Sklad</param>
		/// <param name="hlavickaID">GUID Hlavičky</param>
		/// <returns>StatusInfo - objekt ktery nese info o stavu</returns>
        StatusInfo Expedice_Baleni_Hlavicka_Del(Terminal terminal, User user, Sklad sklad, Guid hlavickaID);

		/// <summary>
		/// Metoda která vratí nalezene hlavčky pro Baleni
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <param name="user">Uzivatel</param>
		/// <param name="sklad">Sklad</param>
		/// <returns>Dataset ExpediceBaleniHlavicky, naplnen informacema</returns>
        Fask.Server.Interfaces.DataSets.ExpediceBaleniHlavicky Expedice_Baleni_GetHlavicky(Terminal terminal, User user, Sklad sklad);

		/// <summary>
		/// Metoda která vráti seznam položek pro Baleni
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <param name="user">uživatel</param>
		/// <param name="sklad">Sklad</param>
		/// <param name="hlavickaID">Guid Hlavičky</param>
		/// <returns>Dataset ExpediceBaleni, naplnena balikama</returns>
        Fask.Server.Interfaces.DataSets.ExpediceBaleni Expedice_Baleni_GetPolozky(Terminal terminal, User user, Sklad sklad, Guid hlavickaID);

		/// <summary>
		/// Metoda kterí vrací seznam položek z balení
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <param name="user">uživatel</param>
		/// <param name="skl_id">ID Skladu</param>
		/// <param name="barcode">č.kod</param>
		/// <param name="itemnmbr"> ID položky</param>
		/// <param name="serltnum">seriove čislo/ šarže</param>
		/// <returns>Dataset ExpediceBaleni, naplneni položkama</returns>
        Fask.Server.Interfaces.DataSets.ExpediceBaleni Expedice_Baleni_Polozka_Get(Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.User user, string skl_id, string barcode, string itemnmbr, string serltnum);

		/// <summary>
		/// Metoda pro zpracovaní davky Baleni 
		/// </summary>
		/// <param name="hlavicka">GUID Hlavičky</param>
		/// <param name="user">uživatel</param>
		/// <param name="terminal">Terminal</param>
		/// <param name="sklad">Sklad</param>
		/// <param name="expedicedata">Data pro zpracování</param>
		/// <param name="processExpediceState">Přiznak, co se ma udelat: Uvolnit, UvolnitAZpracovat, Zpracovat</param>
		/// <returns>StatusObjekt, nese informace o stavu</returns>
        StatusObject Expedice_Baleni_Process( Guid hlavicka, User user, Terminal terminal, Sklad sklad, Fask.Server.Interfaces.DataSets.ExpediceBaleni expedicedata, ProcessState processExpediceState);

		/// <summary>
		/// Společna metoda pro volani po zpracovani davky
		/// </summary>
		/// <param name="hlavicka">Guid hlavičky</param>
		/// <returns>True- OK, False- chyba</returns>
		bool Expedice_Baleni_AfterProcessedAction(Guid hlavicka);

		/// <summary>
		/// Metoda na Storno davky Baleni
		/// </summary>
		/// <param name="hlavickaID">Guid Hlavičky</param>
		/// <param name="terminal">Terminal</param>
		/// <param name="user">uživatel</param>
		/// <param name="password">Heslo</param>
		/// <returns>StatusInfo - objekt ktery nese info o stavu</returns>
        StatusInfo Expedice_Baleni_Storno_Hlavicka(Guid hlavickaID, Terminal terminal, User user, string password);

		/// <summary>
		/// Metoda pro pridani položky Baliku do davky
		/// </summary>
		/// <param name="hlavicka">Guid hlavičky</param>
		/// <param name="terminal">Terminal</param>
		/// <param name="user">uživatel</param>
		/// <param name="sklad">sklad</param>
		/// <param name="expediceRows">Položky baliku na pridani</param>
		/// <returns>StatusInfo - objekt ktery nese info o stavu</returns>
        StatusInfo Expedice_Baleni_Polozka_Add(Guid hlavickaID, Terminal terminal, User user, Fask.Server.Interfaces.Classes.Sklad sklad, Fask.Server.Interfaces.DataSets.ExpediceBaleni expediceRows);

		/// <summary>
		/// Metoda pro smazani položky z davky baleni
		/// </summary>
		/// <param name="hlavickaID">Guid hlavičky</param>
		/// <param name="terminal">Terminal</param>
		/// <param name="user">uživatel</param>
		/// <param name="polozkaID">Guid položky</param>
		/// <returns>StatusInfo - objekt ktery nese info o stavu</returns>
		StatusInfo Expedice_Baleni_Polozka_Del(Guid hlavickaID, Terminal terminal, User user, Guid polozkaID);

		/// <summary>
		/// Metoda pro generovani SSCC kodu
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <param name="user">uživatel</param>
		/// <returns>Objekt SSCC s vygenerovanym kodem SSCC</returns>
        Fask.Server.Interfaces.BarCodes.SSCC Expedice_SSCC_Generovat(Terminal terminal, User user);

		/// <summary>
		/// Metoda pro najdeni konkretneho SSCC kodu u položek
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <param name="user">uživatel</param>
		/// <param name="skl_id">Sklad</param>
		/// <param name="hlavickaID">Guid hlavičky</param>
		/// <param name="code">číslo palety</param>
		/// <returns>Objekt SSCC s sscc</returns>
        Fask.Server.Interfaces.BarCodes.SSCC Expedice_SSCC_Get(Terminal terminal, User user, string skl_id, Guid hlavickaID, string code);

		/// <summary>
		/// Metoda která vytvori hlavicku predlohy podle zaslanych dat.
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <param name="user">Uživatel</param>
		/// <param name="sklad">Sklad</param>
		/// <param name="hlavicka">polozky pro pridani</param>
		/// <returns>StatusInfo - objekt ktery nese info o stavu</returns>
        StatusInfo Expedice_Hlavicka_Add(Terminal terminal, User user, Sklad sklad, Fask.Server.Interfaces.DataSets.ExpediceHlavicky hlavicka);

		/// <summary>
		/// Metoda pro smazani radku v hlavičke expedice včetne dat v predloze
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <param name="user">uživatel</param>
		/// <param name="sklad">Sklad</param>
		/// <param name="hlavickaID">Guid Hlavičky</param>
		/// <returns>StatusInfo - objekt ktery nese info o stavu</returns>
        StatusInfo Expedice_Hlavicka_Del(Terminal terminal, User user, Sklad sklad, Guid hlavickaID);

		/// <summary>
		/// Metoda ktera vraci hlavičky z expedice
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <param name="user">Uživatel</param>
		/// <param name="sklad">Sklad</param>
		/// <returns>Dataset ExpediceHlavicky, naplnen hlavičkama</returns>
        Fask.Server.Interfaces.DataSets.ExpediceHlavicky Expedice_GetHlavicky(Terminal terminal, User user, Sklad sklad);

		/// <summary>
		/// Metoda která vrací palety na expedici
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <param name="user">Uživatel</param>
		/// <param name="sklad">SKlad</param>
		/// <param name="hlavickaID">Guid hlavičky</param>
		/// <returns>Dataset Expedice naplnen paletama</returns>
        Fask.Server.Interfaces.DataSets.Expedice Expedice_GetPalety(Terminal terminal, User user, Sklad sklad, Guid hlavickaID);

		/// <summary>
		/// Metoda která vraci palety podle čísla palety
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <param name="user">Uživatel</param>
		/// <param name="skl_id">Sklad</param>
		/// <param name="nmbrpal">číslo palety</param>
		/// <returns>Expedice, naplnene položkama pro paletu </returns>
        Fask.Server.Interfaces.DataSets.Expedice Expedice_Paleta_Get(Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.User user, string skl_id, string nmbrpal);

		/// <summary>
		/// Metoda pro dotaženi položek z expedice
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <param name="user">uživatel</param>
		/// <param name="sklad">Sklad</param>
		/// <param name="hlavickaID">Guid Hlavičky</param>
		/// <returns>Expedice, dotažena data</returns>
        Fask.Server.Interfaces.DataSets.Expedice Expedice_GetPolozky(Terminal terminal, User user, Sklad sklad, Guid hlavickaID);

		/// <summary>
		/// Metoda pro zpracovaní davky expedce na serveru
		/// </summary>
		/// <param name="hlavicka">Guid hlavičky</param>
		/// <param name="user">uživatel</param>
		/// <param name="terminal">Terminal</param>
		/// <param name="sklad">Sklad</param>
		/// <param name="processExpediceState">příznak co se ma stat: Uvolnit, ZpracovatAPokracovat, Zpracovat</param>
		/// <returns>StatusObject - nese informace o stavu</returns>
        StatusObject Expedice_Process( Guid hlavicka, User user, Terminal terminal, Sklad sklad, ProcessState processExpediceState);

		/// <summary>
		/// MEtoda se volá pro zpracovani davky Společna metoda
		/// </summary>
		/// <param name="hlavicka">Guid hlavičky</param>
		/// <returns>True - OK, False - chyba</returns>
		bool Expedice_AfterProcessedAction(Guid hlavicka);

		/// <summary>
		/// Metoda pro pridany položky na paletu
		/// </summary>
		/// <param name="hlavicka">Guid hlavičky</param>
		/// <param name="terminal">Terminal</param>
		/// <param name="user">uživatel</param>
		/// <param name="sklad">Sklad</param>
		/// <param name="nmbrpal">číslo palety</param>
		/// <returns>StatusInfo - objekt ktery nese info o stavu</returns>
        StatusInfo Expedice_Polozka_Add(Guid hlavicka, Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.User user, Fask.Server.Interfaces.Classes.Sklad sklad, string nmbrpal);

		/// <summary>
		/// Metoda pro smazani položky z palety
		/// </summary>
		/// <param name="hlavicka">Guid hlavičky</param>
		/// <param name="terminal">ID Terminalu</param>
		/// <param name="user">ID uživatele</param>
		/// <param name="nmbrpal">číslo palety</param>
		/// <returns>StatusInfo, objekt nese informace o stavu</returns>
        StatusInfo Expedice_Polozka_Del(Guid hlavicka, Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.User user, string nmbrpal);
    }
}
