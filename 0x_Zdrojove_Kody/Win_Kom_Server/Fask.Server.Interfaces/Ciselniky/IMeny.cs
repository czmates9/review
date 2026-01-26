using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fask.Server.Interfaces.Classes;

namespace Fask.Server.Interfaces.Ciselniky
{
	/// <summary>
	/// Interface pro generovaní a připravu Měn
	/// </summary>
    public interface IMeny
    {
		/// <summary>
		/// Metoda pro přípravu dat pro Souboru Měny pro terminal
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <returns>Dataset Meny naplnen datama</returns>
        Fask.DataSets.Meny KatalogMen(Terminal terminal);

		/// <summary>
		/// Metoda pro export Měny z IS do SQL DB
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <param name="so">reference na StatusObjekt</param>
		/// <returns>Objekt StatusInfo který nese info o stavu</returns>
        StatusInfo KatalogMenExport(Terminal terminal, ref StatusObject so);
    }
}
