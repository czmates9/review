using Fask.Server.Interfaces.DataSets;
using System;
using System.Collections.Generic;
using System.Data;
using Ingres.Client;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Module.Ingres.SAD.Database
{
    public class Vyroba_VMachinesOperations
    {        


        public static Fask.Interfaces.DataSets.Vyroba.VMachinesOperationsDataTable GetDataByMachineIDoperationID(string CS, string MachinesID, string OperationsID)
        {

            IngresConnection conn = null;
            IngresCommand comm = null;
            Fask.Interfaces.DataSets.Vyroba.VMachinesOperationsDataTable dataTable = new Fask.Interfaces.DataSets.Vyroba.VMachinesOperationsDataTable();

            using (conn = new IngresConnection(CS))
            {
                using (comm = conn.CreateCommand())
                {
                    //comm.CommandText = "Select * from " + Tables.TABLE_VMachinesOperations;
                    comm.CommandText = "Select * FROM " + Fask.SQL.Constants.Common.TABLE_VMachinesOperations +
                        " WHERE ( machineid = @machineid ) " +
                        "and ( operationid = @operationid )";


                    comm.Parameters.Add(new IngresParameter()
                    { ParameterName = "@machineid", DbType = DbType.String, SourceColumn = "machineid", Value = MachinesID == null ? throw  new Exception("MachinesID is null!") : MachinesID });
                    comm.Parameters.Add(new IngresParameter()
                    { ParameterName = "@operationid", DbType = DbType.String, SourceColumn = "operationid", Value = OperationsID == null ? throw new Exception("OperationsID is null!") : OperationsID });

                    comm.CommandType = CommandType.Text;
                    using (var ada = new IngresDataAdapter())
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
