using System;
using System.Collections.Generic;
using System.Data;
using Ingres.Client;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Module.Ingres.SAD.Database
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
            IngresConnection conn = null;
            IngresCommand comm = null;

            using (conn = new IngresConnection(CS))
            {
                using (comm = conn.CreateCommand())
                {
                    comm.CommandText = script;
                    comm.CommandType = CommandType.Text;
                    using (var ada = new IngresDataAdapter())
                    {
                        ada.SelectCommand = comm;
                        ada.Fill(dt);

                    }
                }
            }
        }
    }
}
