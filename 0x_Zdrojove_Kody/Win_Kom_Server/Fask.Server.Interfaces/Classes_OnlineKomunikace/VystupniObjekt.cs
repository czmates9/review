using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Server.Interfaces.Classes_OnlineKomunikace
{
	/// <summary>
	/// Jedná se  třídu která nese vyhodnocené data o z online komunikace
	/// </summary>
	public class VystupniObjekt
	{

		public decimal MnozstviDodavatelePozadovano;
		public decimal MnozstviDodavateleDodano;
		public decimal MnozstviDodavateleDodat;
		public decimal MnozstviOdberateliPozadovano;
		public decimal MnozstviOdberatelumDodano;
		public decimal MnozstviOdberatelumDodat;
		public decimal Vysledek;

		/// <summary>
		/// Konstruktor
		/// </summary>
		public VystupniObjekt()
		{
 
		}
	}
}
