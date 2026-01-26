using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Module.ABRA.CarpServise.Database
{
    class Inventura
    {
		public static int CZMSTI1H_MAX_CountEntries()
		{
			System.Data.SqlClient.SqlCommand sqlcommand = null;
			try
			{
				sqlcommand = new System.Data.SqlClient.SqlCommand();
				sqlcommand.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
				sqlcommand.CommandType = System.Data.CommandType.Text;
				sqlcommand.CommandText = "SELECT MAX( CountEntries ) FROM CZMST_I1H";
				sqlcommand.Connection.Open();
				object o = sqlcommand.ExecuteScalar();


				try
				{
					int countentries = Convert.ToInt32(o);
					return countentries + 1; // +1 je z duvodu že tohle ID se už použije pro další dávku
				}
				catch (Exception e)
				{
					Logging.ExceptionHandler2.Handle(e);
					return 1; //pokud nenalezeno ... ???
				}
			}
			finally
			{
				if ((sqlcommand.Connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
					sqlcommand.Connection.Close();
			}
		}

		public static int CZMSTI1H_CountEntries(string Desc)
		{
			System.Data.SqlClient.SqlCommand sqlcommand = null;
			try
			{
				sqlcommand = new System.Data.SqlClient.SqlCommand();
				sqlcommand.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
				sqlcommand.CommandType = System.Data.CommandType.Text;
				sqlcommand.CommandText = "SELECT MAX( CountEntries ) FROM CZMST_I1H WHERE Description ='" + Desc.Trim() +"'";
				sqlcommand.Connection.Open();
				object o = sqlcommand.ExecuteScalar();


				try
				{
					int countentries = Convert.ToInt32(o);
					return countentries;
				}
				catch (Exception e)
				{
					Logging.ExceptionHandler2.Handle(e);
					return 0; //pokud nenalezeno ... ???
				}
			}
			finally
			{
				if ((sqlcommand.Connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
					sqlcommand.Connection.Close();
			}
		}

	}
}
