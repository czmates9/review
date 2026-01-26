using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Fask.ModuleSql.Database;

namespace Fask.ModuleSql
{
    public partial class Provider :
        Fask.Interfaces.Vyroba.Operations.IOperations,
        Fask.Interfaces.Vyroba.Operations.IOperations_GetDataByID,
        Fask.Interfaces.Vyroba.Operations.IOperations_Fill
    {

        #region IOperations_GetDataByID Members

        public Fask.Interfaces.DataSets.Vyroba.OperationsDataTable Operations_GetDataByID(string ID)
        {
            //Fask.Interfaces.DataSets.Vyroba.OperationsDataTable dt = new Fask.Interfaces.DataSets.Vyroba.OperationsDataTable();

            //var ta = new Fask.ModuleSql.SQL_Datasets.VyrobaDataSetTableAdapters.OperationsTableAdapter();
            //ta.Connection = new SqlConnection(ConnectionString);

            //var tmp = ta.GetDataByID(ID);


            //foreach (var item in tmp)
            //{
            //    dt.ImportRow(item);
            //}


            //return dt;

            return Vyroba_Operations.Operations_GetDataByID(ConnectionString, ID);
        }

        #endregion

        #region IOperations_Fill Members

       public void Operations_Fill(Fask.Interfaces.DataSets.Vyroba ds)
        {
            //SQL_Datasets.VyrobaDataSet tmp = new SQL_Datasets.VyrobaDataSet();

            //var ta = new Fask.ModuleSql.SQL_Datasets.VyrobaDataSetTableAdapters.OperationsTableAdapter();
            //ta.Connection = new SqlConnection(ConnectionString);

            //ta.Fill(tmp.Operations);

            //foreach (var item in tmp.Operations)
            //{
            //    ds.Operations.ImportRow(item);
            //}

            Vyroba_Operations.Operations_Fill(ConnectionString, ds);
        }

        #endregion
    }
}
