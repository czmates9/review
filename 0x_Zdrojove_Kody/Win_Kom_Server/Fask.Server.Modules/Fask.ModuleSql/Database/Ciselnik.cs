using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.ModuleSql.Database
{
    public class Ciselnik
    {
        /// <summary>
        /// Jedná se o univerzalní metodu pro naplneni datatable z databaze
        /// </summary>
        /// <param name="CS">ConnectionString</param>
        /// <param name="script">Vykonaný script</param>
        /// <param name="dt">tabulka pro naplneni</param>
        public void Fill_Universal(string CS, string script, DataTable dt)
        {
            SqlConnection conn = null;
            SqlCommand comm = null;

            using (conn = new SqlConnection(CS))
            {
                using (comm = conn.CreateCommand())
                {
                    comm.CommandText = script;
                    comm.CommandType = CommandType.Text;
                    using (var ada = new SqlDataAdapter())
                    {
                        ada.SelectCommand = comm;
                        ada.Fill(dt);

                    }
                }
            }
        }
    }
}
