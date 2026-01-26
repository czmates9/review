using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Server.Interfaces.Classes_OnlineKomunikace
{
	/// <summary>
	/// Jedná se  třídu která nese informace pro vyhodnoceni dat na serveru
	/// </summary>
	public class VstupniObjekt
	{

		public string DOC_ID;
		public string DOC_ID2;
		public string SKL_ID;



		public string ITEMNMBR;
		public decimal MnozstviZadane;
		public decimal MnozstviNasnimane;

		/// <summary>
		/// Konstruktor
		/// </summary>
		public VstupniObjekt()
		{
 
		}

	}
}
