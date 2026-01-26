using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fask.Server.Interfaces.Classes;

namespace Fask.Server.Interfaces.Ciselniky
{
	/// <summary>
	/// Interface pro přípravu Typy dokladu
	/// </summary>
    public interface ITypDokladu
    {
		/// <summary>
		/// Metoda pro přípravu dat pro Souboru Typy Dokladu pro terminal
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <param name="sklad">Sklad</param>
		/// <returns>Dataset TypDokladu naplnen datama </returns>
        Fask.DataSets.TypDokladu KatalogTypDokladu(Terminal terminal, Sklad sklad);

		/// <summary>
		/// Metoda pro export TypDokladu z IS do SQL DB
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <param name="sklad">Sklad</param>
		/// <param name="so">reference na StatusObjekt</param>
		/// <returns>Objekt StatusInfo který nese info o stavu</returns>
		StatusInfo KatalogTypDokladuExport(Terminal terminal, Sklad sklad, ref StatusObject so);


	}
}
