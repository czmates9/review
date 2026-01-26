using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Server.Interfaces.API_BO
{
    class BO_Prijem
    {

	}

	public class Prijem
	{
		public Prijem_row[] rows { get; set; }
	}

	public class Prijem_row
	{
		public System.Data.DataRowState RowState { get; set; }

		public int countentries { get; set; }
		public byte idterminal { get; set; }
		public string itemtype { get; set; }
		public string state { get; set; }
		public string skl_id { get; set; }
		public string sopnumbe { get; set; }

	}
}
