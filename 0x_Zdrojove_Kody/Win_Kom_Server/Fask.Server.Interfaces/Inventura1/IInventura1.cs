using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fask.Server.Interfaces.Classes;
using System.Data;

namespace Fask.Server.Interfaces.Inventura1
{
	/// <summary>
	/// Interface pro Inventuru 
	/// </summary>
    public interface IInventura1
    {
        
		/// <summary>
		/// Metoda která vrací hlavičky inventury
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <param name="sklad">Sklad</param>
		/// <returns>Dataset Inventury1 naplnen datma</returns>
        Fask.DataSets.Inventury1 Inventura_GetInventury(Terminal terminal, Sklad sklad);

		/// <summary>
		/// Metoda pro dotažení dat inventury pro Terminal
		/// </summary>
		/// <param name="davka">Dávka</param>
		/// <param name="terminal">Terminal</param>
		/// <returns>Dataset Inventura1 s naplnenima datama</returns>
        Fask.DataSets.Inventura1 Inventura_GetInventura(Davka davka, Terminal terminal);

		/// <summary>
		/// Metoda pro označení stažené inventury
		/// </summary>
		/// <param name="davka">Davka</param>
		/// <param name="terminal">Terminal</param>
		/// <returns>True-OK, False- Chyba</returns>
        bool Inventura_GetInventuraReceived(Davka davka, Terminal terminal);

		/// <summary>
		/// Metoda pro zpracovaní dat na serveru do SQL a IS
		/// </summary>
		/// <param name="davka">Dávka</param>
		/// <param name="terminal">Terminal</param>
		/// <param name="inventuradata">Data pro zpracovaní</param>
		/// <param name="processInventuraState">příznak, co se ma s datama udelat</param>
		/// <returns>StatusObject - Nese informace o stavu</returns>
        StatusObject Inventura_Process( Davka davka, Terminal terminal, Fask.DataSets.Inventura1 inventuradata, ProcessState processInventuraState);

		/// <summary>
		/// Metoda která se volá po zprocesovaní dat
		/// </summary>
		/// <param name="davka">Dávka</param>
		/// <returns>True-OK, False- chyba</returns>
        bool Inventura_AfterProcessedAction(Davka davka);

		/// <summary>
		/// Metoda pro online kontrolu položky a označeni
		/// </summary>
		/// <param name="davka">Dávka</param>
		/// <param name="terminal">Terminal</param>
		/// <param name="itemnmbr">ID položky</param>
		/// <param name="o_terminalid">reference na O_TID</param>
		/// <returns>True-OK, False-chyba</returns>
        bool Inventura_OnlineUnCheckState(Davka davka, Terminal terminal, string itemnmbr, out byte o_terminalid);

		/// <summary>
		/// Metoda pro online kontrolu položky a odznačeni
		/// </summary>
		/// <param name="countentries">číslo dávky</param>
		/// <param name="terminal">Terminal</param>
		/// <param name="itemnmbr">ID položky</param>
		/// <param name="o_terminalid">reference na O_TID</param>
		/// <returns> True-OK, False-chzba </returns>
        bool Inventura_OnlineCheckState(Davka countentries, Terminal terminal, string itemnmbr, out byte o_terminalid);

    }
}
