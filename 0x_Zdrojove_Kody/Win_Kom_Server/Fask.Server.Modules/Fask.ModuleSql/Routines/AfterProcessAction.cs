using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Fask.ModuleSql.Routines
{
	/// <summary>
	/// Třída která vykonáva neco po nečem... dle vtupnych parametru
	/// </summary>
    class AfterProcessAction
    {
		/// <summary>
		/// Metoda která provede akci PO
		/// </summary>
		/// <param name="connstring">ConnectionString</param>
		/// <param name="table">Nazev tabulky</param>
		/// <param name="countentries">číslo dávky</param>
		/// <param name="aDP_Action">nazev procedury</param>
		/// <param name="aDP_Action_P1">Nazev prvniho parametru(Tabulky)</param>
		/// <param name="aDP_Action_P2">Nazev prvniho parametru(čísla dávky)</param>
		/// <param name="AfterDataProcessed_Action_CommandTimeout"></param>
        public static void Execute(string connstring, string table, int countentries, string aDP_Action, string aDP_Action_P1, string aDP_Action_P2, int AfterDataProcessed_Action_CommandTimeout)
        {
            System.Data.SqlClient.SqlConnection adpaconnection = null;

            try 
            {
                adpaconnection = new System.Data.SqlClient.SqlConnection(connstring);
                System.Data.SqlClient.SqlCommand adpacommand = new System.Data.SqlClient.SqlCommand(aDP_Action);
                adpacommand.CommandType = CommandType.StoredProcedure;

                adpacommand.CommandTimeout = AfterDataProcessed_Action_CommandTimeout;

                adpacommand.Parameters.Add(new System.Data.SqlClient.SqlParameter(aDP_Action_P1, SqlDbType.NVarChar, 20));
                adpacommand.Parameters.Add(new System.Data.SqlClient.SqlParameter(aDP_Action_P2, SqlDbType.NVarChar, 20));

                ((IDataParameter)adpacommand.Parameters[aDP_Action_P1]).Value = table;
                ((IDataParameter)adpacommand.Parameters[aDP_Action_P2]).Value = countentries.ToString();

                adpacommand.Connection = adpaconnection;

                adpaconnection.Open();
                adpacommand.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (adpaconnection != null && (adpaconnection.State & ConnectionState.Open) == ConnectionState.Open)
                    adpaconnection.Close();
            }
        }
    }
}
