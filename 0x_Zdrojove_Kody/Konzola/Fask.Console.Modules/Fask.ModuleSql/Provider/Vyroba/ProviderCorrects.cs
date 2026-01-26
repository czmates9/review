using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Fask.ModuleSql.Database;

namespace Fask.ModuleSql
{
    public partial class Provider   :
        Fask.Interfaces.Vyroba.Corrects.ICorrects,
        Fask.Interfaces.Vyroba.Corrects.ICorrects_GetDataByID
    {

      
        #region ICorrects_GetDataByID Members

       public Fask.Interfaces.DataSets.Vyroba.CorrectsDataTable Corrects_GetDataByID(int ID)
        {
            #region old sql
            //Fask.Interfaces.DataSets.Vyroba.CorrectsDataTable dt = new Fask.Interfaces.DataSets.Vyroba.CorrectsDataTable();

            //var ta = new Fask.ModuleSql.SQL_Datasets.VyrobaDataSetTableAdapters.CorrectsTableAdapter();
            //ta.Connection = new SqlConnection(ConnectionString);

            //var tmp = ta.GetDataByID(ID);


            //foreach (var item in tmp)
            //{
            //    dt.ImportRow(item);
            //}


            //return dt; 
            #endregion

            return Database.Vyroba_Corrects.Corrects_GetDataByID(ConnectionString, ID);

        }

        #endregion




    }
}
