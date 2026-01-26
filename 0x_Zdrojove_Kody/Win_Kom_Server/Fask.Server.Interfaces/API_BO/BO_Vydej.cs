using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Server.Interfaces.API_BO
{
    class BO_Vydej
    {
    }

	public class Vydejky
	{
		public Vydejky_row[] rows { get; set; }
	}

	public class Vydejky_row
	{
		public System.Data.DataRowState RowState { get; set; }


		public int userid { get; set; }
		public byte idterminal { get; set; }
		public string itemtype { get; set; }
		public string prefixskladu { get; set; }

	}

	public class Ciselniky
	{
		public Ciselniky_row[] rows { get; set; }
	}

	public class Ciselniky_row
	{
		public System.Data.DataRowState RowState { get; set; }


		public int userid { get; set; }
		public byte idterminal { get; set; }
		public string itemtype { get; set; }
		public string prefixskladu { get; set; }

	}



	public class Vydejka
	{
		public Vydejka_row[] rows { get; set; }
	}

	public class Vydejka_row
	{
		public System.Data.DataRowState RowState { get; set; }

		public int countentries { get; set; }
		public byte idterminal { get; set; }
		public string itemtype { get; set; }
		public string state { get; set; }
		public string skl_id { get; set; }
		public string sopnumbe { get; set; }
		public string itemnmbr { get; set; }
		public string serltnum { get; set; }
		public DateTime? expirace { get; set; }
		//string itemnmbr, string sklad_id, string serltnum, DateTime? expirace

	}


	public class Polozka
	{
		public Polozka_row[] rows { get; set; }
	}

	public class Polozka_row
	{
		public System.Data.DataRowState RowState { get; set; }


		public string idZbozi { get; set; }
		public string idSklad { get; set; }
		public string itemnmbr { get; set; }
		public string skl_id { get; set; }
		public string serltnum { get; set; }
		public string doc_id { get; set; }
		public string locncode { get; set; }

		//string itemnmbr, string skl_id, string serltnum, string doc_id, string locncode

	}

	public class Ostatni
	{
		public Ostatni_row[] rows { get; set; }
	}

	public class Ostatni_row
	{
		public System.Data.DataRowState RowState { get; set; }

		public int countentries { get; set; }
		public byte idterminal { get; set; }
		public string itemtype { get; set; }
		public string state { get; set; }
		public string skl_id { get; set; }
		public string sopnumbe { get; set; }
		public string itemnmbr { get; set; }
		public string serltnum { get; set; }
		public DateTime? expirace { get; set; }
		//string itemnmbr, string sklad_id, string serltnum, DateTime? expirace

	}

	public class Inventura
	{
		public Inventura_row[] rows { get; set; }
	}

	public class Inventura_row
	{
		public System.Data.DataRowState RowState { get; set; }

		public int countentries { get; set; }
		public byte idterminal { get; set; }
		public string itemtype { get; set; }
		public string state { get; set; }
		public string skl_id { get; set; }
		public string sopnumbe { get; set; }
		public string itemnmbr { get; set; }
		public string serltnum { get; set; }
		public DateTime? expirace { get; set; }
		//string itemnmbr, string sklad_id, string serltnum, DateTime? expirace

	}

	public class Vyroba
	{
		public Vyroba_row[] rows { get; set; }
	}

	public class Vyroba_row
	{
		public System.Data.DataRowState RowState { get; set; }

		public int countentries { get; set; }
		public byte idterminal { get; set; }
		public string itemtype { get; set; }
		public string state { get; set; }
		public string skl_id { get; set; }
		public string sopnumbe { get; set; }
		public string itemnmbr { get; set; }
		public string serltnum { get; set; }
		public DateTime? expirace { get; set; }
		//string itemnmbr, string sklad_id, string serltnum, DateTime? expirace

	}

	public class Expedice
	{
		public Expedice_row[] rows { get; set; }
	}

	public class Expedice_row
	{
		public System.Data.DataRowState RowState { get; set; }

		public int countentries { get; set; }
		public byte idterminal { get; set; }
		public string itemtype { get; set; }
		public string state { get; set; }
		public string skl_id { get; set; }
		public string sopnumbe { get; set; }
		public string itemnmbr { get; set; }
		public string serltnum { get; set; }
		public DateTime? expirace { get; set; }
		//string itemnmbr, string sklad_id, string serltnum, DateTime? expirace

	}
}
