using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fask.Server.Interfaces.Classes;

namespace Fask.Server.Interfaces.Ciselniky
{
	/// <summary>
	/// Interface pro generovaní a přípravu Zboží
	/// </summary>
    public interface IZbozi
    {
		/// <summary>
		/// Metoda pro přípravu dat pro Souboru Zboží pro terminal
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <param name="sklad">Sklad</param>
		/// <param name="so">reference na StatusObjekt</param>
		/// <returns>Dataset Zbozi naplnen datama</returns>
		Fask.Interfaces.DataSets.Zbozi KatalogZbozi(Terminal terminal, Sklad sklad, ref StatusObject so);

		/// <summary>
		/// Metoda pro export Zásob z IS do SQL DB
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <param name="sklad">Sklad</param>
		/// <param name="so">reference na StatusObjekt</param>
		/// <returns>Objekt StatusInfo který nese info o stavu</returns>
        StatusInfo KatalogZboziExport(Terminal terminal, Sklad sklad, ref StatusObject so);
    }
}
