using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fask.Server.Interfaces.Classes;
using System.Data;

namespace Fask.Server.Interfaces.Inventura2
{
    public interface IInventura2
    {
		/// <summary>
		/// Metoda která vraci seznam dostupnych inventurnich predloh
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <returns>Dataset - Inventury2, naplnen datama pro inventuru</returns>
		Fask.DataSets.Inventury2 Inventura2_GetInventury(Terminal terminal);

		/// <summary>
		/// Metoda pro dotaženi inventury z DB pro pripravu souboru
		/// </summary>
		/// <param name="davka">Davka</param>
		/// <param name="terminal">Terminal</param>
		/// <returns>Dataset Inventura2 - naplnen datama</returns>
        Fask.DataSets.Inventura2 Inventura2_GetInventura(Davka davka, Terminal terminal);

		/// <summary>
		/// Metoda blokuje data po prenosu do terminalu, je poslednim krokem pri prenosu dat
		/// </summary>
		/// <param name="davka">Davka</param>
		/// <param name="terminal">Terminal</param>
		/// <returns>True-OK, False-Chyba</returns>
        bool Inventura2_GetInventuraReceived(Davka davka, Terminal terminal);

		/// <summary>
		/// Metoda která zpracuje prenesena data nebo uvolni davku...
		/// </summary>
		/// <param name="davka">Davka</param>
		/// <param name="terminal">Terminal</param>
		/// <param name="inventuradata">Data pro zpracovaní</param>
		/// <param name="processInventuraState">Zpracovat/Uvolnit</param>
		/// <returns>StatusObject - Nese informace o stavu</returns>
        StatusObject Inventura2_Process(Davka davka, Terminal terminal, Fask.DataSets.Inventura2 inventuradata, ProcessState processInventuraState);

		/// <summary>
		/// Metoda která se volá po zpracovaní dat
		/// </summary>
		/// <param name="davka">Davka</param>
		/// <returns>True-OK, False-Chyba</returns>
        bool Inventura2_AfterProcessedAction(Davka davka);

    }
}
