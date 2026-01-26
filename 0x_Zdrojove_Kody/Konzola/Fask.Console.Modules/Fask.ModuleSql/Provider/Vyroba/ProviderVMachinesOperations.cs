using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Fask.ModuleSql.Database;

namespace Fask.ModuleSql
{
    public partial class Provider  :
        Fask.Interfaces.Vyroba.VMachinesOperations.IVMachinesOperations,
        Fask.Interfaces.Vyroba.VMachinesOperations.IVMachinesOperations_GetDataByMachineIDoperationID

    {

        #region IVMachinesOperations_GetDataByMachineIDoperationID Members

        public Fask.Interfaces.DataSets.Vyroba.VMachinesOperationsDataTable VMachinesOperations_GetDataByMachineIDoperationID(string MachinesID, string OperationsID)
        {
            #region old sql
            //Fask.Interfaces.DataSets.Vyroba.VMachinesOperationsDataTable dt = new Fask.Interfaces.DataSets.Vyroba.VMachinesOperationsDataTable();

            //var ta = new Fask.ModuleSql.SQL_Datasets.VyrobaDataSetTableAdapters.VMachinesOperationsTableAdapter();
            //ta.Connection = new SqlConnection(ConnectionString);

            //var tmp = ta.GetDataByMachineIDoperationID(MachinesID, OperationsID);


            //foreach (var item in tmp)
            //{
            //    dt.ImportRow(item);
            //}


            //return dt; 
            #endregion
            Vyroba_VMachinesOperations obj = new Vyroba_VMachinesOperations();

            return obj.GetDataByMachineIDoperationID(ConnectionString, MachinesID, OperationsID);
        }

        #endregion
    }
}
