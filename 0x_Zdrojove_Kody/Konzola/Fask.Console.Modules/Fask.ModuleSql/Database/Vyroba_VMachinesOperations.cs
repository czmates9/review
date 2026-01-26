using Fask.Interfaces.DataSets;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.ModuleSql.Database
{
    public class Vyroba_VMachinesOperations
    {        


        public Vyroba.VMachinesOperationsDataTable GetDataByMachineIDoperationID(string CS, string MachinesID, string OperationsID)
        {

            System.Data.SqlClient.SqlConnection conn = null;
            System.Data.SqlClient.SqlCommand comm = null;
            Vyroba.VMachinesOperationsDataTable dataTable = new Vyroba.VMachinesOperationsDataTable();

            using (conn = new SqlConnection(CS))
            {
                using (comm = conn.CreateCommand())
                {
                    //comm.CommandText = "Select * from " + Tables.TABLE_VMachinesOperations;
                    comm.CommandText = "Select * FROM " + Fask.SQL.Constants.Common.TABLE_VMachinesOperations +
                        " WHERE ( machineid = @machineid ) " +
                        "and ( operationid = @operationid )";


                    comm.Parameters.Add(new SqlParameter()
                    { ParameterName = "@machineid", DbType = DbType.String, SourceColumn = "machineid", Value = MachinesID == null ? throw  new Exception("MachinesID is null!") : MachinesID });
                    comm.Parameters.Add(new SqlParameter()
                    { ParameterName = "@operationid", DbType = DbType.String, SourceColumn = "operationid", Value = OperationsID == null ? throw new Exception("OperationsID is null!") : OperationsID });

                    comm.CommandType = CommandType.Text;
                    using (var ada = new SqlDataAdapter())
                    {
                        ada.SelectCommand = comm;
                        ada.Fill(dataTable);
                    }
                }
            }
            return dataTable;
        }



    }
}
