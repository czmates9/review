using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Server.Interfaces.Inventura1
{
	/// <summary>
	/// Interface pro Inventuru pro Android terminal
	/// </summary>
    public interface IInventura1Terminal : IMST
    {
		/// <summary>
		/// Metoda která pripravy soubor na serveru pro Terminal
		/// </summary>
		/// <param name="countentries">číslo dávky</param>
		/// <param name="idterminal">ID Terminalu</param>
		/// <param name="inventura">data do souborzu</param>
		/// <returns>True-OK, False-chyba</returns>
        bool Inventura_FillInventuraDB(int countentries, byte idterminal, Fask.DataSets.Inventura1 inventura);


		/// <summary>
		/// Metoda pro zpracovaní dat na serveru z prijateho souboru
		/// </summary>
		/// <param name="countentries">číslo dávky</param>
		/// <param name="idterminal">ID Terminalu</param>
		/// <returns> Dataset - Inventura1, naplnen datama  </returns>
        Fask.DataSets.Inventura1 Inventura_GetInventuraDB(int countentries, byte idterminal);
   
    }
}
