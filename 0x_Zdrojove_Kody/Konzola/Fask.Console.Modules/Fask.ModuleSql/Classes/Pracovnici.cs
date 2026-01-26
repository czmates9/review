using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Fask.ModuleSql
{

   public class Pracovnici
    {
       public static bool DeleteAll(string connectionString)
	{
		                   System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(connectionString);
                connection.Open();

                SQL_Datasets.PracovniciTableAdapters.CZMST096TableAdapter ta_096 = new SQL_Datasets.PracovniciTableAdapters.CZMST096TableAdapter();
                ta_096.Connection = connection;
                trans = connection.BeginTransaction(IsolationLevel.Serializable);
                ta_096.MyTransaction = trans;

                ta_096.DeleteAll();

                if (trans != null)
                    trans.Commit();

                return true;
            }
            catch
            {
                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch { }
                throw;
            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            } 
	}
    }
}
