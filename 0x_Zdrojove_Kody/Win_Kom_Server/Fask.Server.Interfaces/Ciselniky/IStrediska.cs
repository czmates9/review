using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fask.Server.Interfaces.Classes;

namespace Fask.Server.Interfaces.Ciselniky
{
	/// <summary>
	/// Interface pro generovaní a přípravu Střediska
	/// </summary>
    public interface IStrediska
    {
		/// <summary>
		/// Metoda pro přípravu dat pro Souboru Střediska pro terminal
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <param name="sklad">Sklad</param>
		/// <returns>Dataset Strediska naplnen datama</returns>
		Fask.Interfaces.DataSets.Strediska KatalogStrediska(Terminal terminal, Sklad sklad);

		/// <summary>
		/// Metoda pro export Střediska z IS do SQL DB
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <param name="so">reference na StatusObjekt</param>
		/// <returns>Objekt StatusInfo který nese info o stavu</returns>
        StatusInfo KatalogStrediskaExport(Terminal terminal, ref StatusObject so);
    }
}
