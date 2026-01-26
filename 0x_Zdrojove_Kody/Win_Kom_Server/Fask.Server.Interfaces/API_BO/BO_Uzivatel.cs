using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.WEBAPI.API_BusinessObjects
{
    class BO_Uzivatel
    {
    }

	public class UzivatelJSON
	{
		public UzivatelJSON_row[] rows { get; set; }
	}

	public class UzivatelJSON_row
	{
		public System.Data.DataRowState RowState { get; set; }

		public int id { get; set; }
		public string loginid { get; set; }
		public int countentries { get; set; }
		public byte idterminal { get; set; }
		public int userID { get; set; }
		public string login { get; set; }
		public string dstFile { get; set; }

	}
}
