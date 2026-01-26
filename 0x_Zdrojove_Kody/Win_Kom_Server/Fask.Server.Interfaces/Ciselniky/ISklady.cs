using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fask.Server.Interfaces.Classes;

namespace Fask.Server.Interfaces.Ciselniky
{
	/// <summary>
	/// Interface pro generovaní a přípravu Sklady
	/// </summary>
    public interface ISklady
    {
		/// <summary>
		/// Metoda pro přípravu dat pro Souboru Sklady pro terminal
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <returns>Dataset Sklady naplnen datama</returns>
        Fask.Interfaces.DataSets.Sklady KatalogSklady(Terminal terminal);

		/// <summary>
		/// Metoda pro export Sklady z IS do SQL DB
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <param name="so">reference na StatusObjekt</param>
		/// <returns>Objekt StatusInfo který nese info o stavu</returns>
        StatusInfo KatalogSkladyExport(Terminal terminal, ref StatusObject so);
    }
}
