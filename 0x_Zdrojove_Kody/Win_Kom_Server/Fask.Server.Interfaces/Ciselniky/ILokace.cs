using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fask.Server.Interfaces.Classes;

namespace Fask.Server.Interfaces.Ciselniky
{
	/// <summary>
	/// Interface pro Lokace
	/// </summary>
    public interface ILokace
    {
		/// <summary>
		/// Metoda pro přípravu dat pro Souboru Lokace pro terminal 
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <param name="sklad">Sklad</param>
		/// <returns>Lokace, dataset naplnen daty</returns>
        Fask.DataSets.Lokace KatalogLokace(Terminal terminal, Sklad sklad);

		/// <summary>
		/// Metoda pro export Lokací z IS do SQL DB
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <param name="sklad">Sklad</param>
		/// <param name="so">reference na StatusObjekt</param>
		/// <returns>Objekt StatusInfo který nese info o stavu</returns>
		StatusInfo KatalogLokaceExport(Terminal terminal, Sklad sklad, ref StatusObject so);
	}
}
