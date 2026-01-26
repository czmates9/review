using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Server.Interfaces.Servis
{
	/// <summary>
	/// Třída která nese 
	/// </summary>
    public class Zdroj
    {
		/// <summary>
		/// ID Zdroje
		/// </summary>
        public string IDZdroj;

		/// <summary>
		/// ID Stavu
		/// </summary>
        public string IDStav;

		/// <summary>
		/// ID činosti
		/// </summary>
        public string IDCinnost;

		/// <summary>
		/// čas zmeny
		/// </summary>
        public DateTime Modified;

		/// <summary>
		/// ID Terminalu
		/// </summary>
        public byte IDTerminal;

		/// <summary>
		/// ID uživatele
		/// </summary>
        public int IDUser;

		/// <summary>
		/// GUID- identifikator
		/// </summary>
        public Guid GUID;

		/// <summary>
		/// činnost hodnota
		/// </summary>
        public string CinnostValue;

		/// <summary>
		/// činnost Typ
		/// </summary>
        public string CinnostType;
		
		/// <summary>
		/// číslo dávky
		/// </summary>
        public int? CountEntries;

		/// <summary>
		/// ID Oběratele
		/// </summary>
        public string ODB_ID;

		/// <summary>
		/// ID Okruhu
		/// </summary>
        public string OkruhID;

		/// <summary>
		/// Označení činnosti
		/// </summary>
        public string CinnostOznaceni;

		/// <summary>
		/// Souřadnice X
		/// </summary>
        public double? GPS_X;

		/// <summary>
		/// Souřadnice Y
		/// </summary>
        public double? GPS_Y;

		/// <summary>
		/// Souřadnice Z
		/// </summary>
        public int? GPS_Z;
    }
}
