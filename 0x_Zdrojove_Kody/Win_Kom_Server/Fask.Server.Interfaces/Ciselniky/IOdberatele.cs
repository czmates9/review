using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fask.Interfaces.DataSets;
using Fask.Server.Interfaces.Classes;

namespace Fask.Server.Interfaces.Ciselniky
{
	/// <summary>
	/// Interface pro generovaní a přípravu Odběratele
	/// </summary>
    public interface IOdberatele
    {
		/// <summary>
		/// Metoda pro přípravu dat pro Souboru Odběratele pro terminal
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <param name="sklad">Sklad</param>
		/// <returns>Dataset Odberatele naplnen datama</returns>
        Odberatele KatalogOdberatele(Fask.Server.Interfaces.Classes.Terminal terminal, Sklad sklad);


		/// <summary>
		/// Metoda pro export Odběratele z IS do SQL DB
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <param name="so">reference na StatusObjekt</param>
		/// <returns>Objekt StatusInfo který nese info o stavu</returns>
        StatusInfo KatalogOdberateleExport(Fask.Server.Interfaces.Classes.Terminal terminal, ref StatusObject so);
    }
}
