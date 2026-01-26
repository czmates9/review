using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Fask.ModulePohodaXML.Provider
{
    public partial class Provider   :
        Fask.Interfaces.Vyroba.Corrects.ICorrects,
        Fask.Interfaces.Vyroba.Corrects.ICorrects_GetDataByID
    {

      
        #region ICorrects_GetDataByID Members

        Fask.Interfaces.DataSets.Vyroba.CorrectsDataTable Fask.Interfaces.Vyroba.Corrects.ICorrects_GetDataByID.Corrects_GetDataByID(int ID)
        {
            //Globals_V1.LoadConfiguration();
            //Fask.Interfaces.DataSets.Vyroba.CorrectsDataTable dt = new Fask.Interfaces.DataSets.Vyroba.CorrectsDataTable();

            //var ta = new Pohoda_DataSets.VyrobaDataSetTableAdapters.CorrectsTableAdapter();
            //ta.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

            //var tmp = ta.GetDataByID(ID);


            //foreach (var item in tmp)
            //{
            //    dt.ImportRow(item);
            //}


            //return dt;

            Globals_V1.LoadConfiguration();
            return Database.Vyroba_Corrects.Corrects_GetDataByID(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB, ID);
        }

        #endregion




    }
}
