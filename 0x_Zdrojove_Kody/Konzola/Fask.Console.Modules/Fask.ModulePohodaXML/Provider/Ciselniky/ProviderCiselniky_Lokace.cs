using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Fask.ModulePohodaXML.Provider
{
    public partial class Provider :
        Fask.Interfaces.Ciselniky.Lokace.ILokace2,
        Fask.Interfaces.Ciselniky.Lokace.ILokace2_Fill
    {

        #region ILokace2_Fill Members

        #region ILokace2_Fill Members

        public void Fill(Fask.Interfaces.DataSets.Vyroba ds)
        {
            //Globals_V1.LoadConfiguration();
            //Pohoda_DataSets.VyrobaDataSet.CZMST094DataTable dt = new Pohoda_DataSets.VyrobaDataSet.CZMST094DataTable();

            //Pohoda_DataSets.VyrobaDataSetTableAdapters.CZMST094TableAdapter ta = new Pohoda_DataSets.VyrobaDataSetTableAdapters.CZMST094TableAdapter();
            //ta.Connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

            //ta.Fill(dt);

            //foreach (var item in dt)
            //{
            //    ds.CZMST094.ImportRow(item);
            //}

            try
            {
                Globals_V1.LoadConfiguration();

                using (var con = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                {
                    using (var ada = new System.Data.SqlClient.SqlDataAdapter())
                    {
                        using (var com = con.CreateCommand())
                        {

                            var select = @"SELECT * FROM " + Fask.SQL.Constants.Common.TABLE_CZMST094;

                            ada.SelectCommand = com;
                            ada.SelectCommand.CommandText = select;
                            ada.SelectCommand.Connection = con;
                            ada.SelectCommand.CommandType = CommandType.Text;

                            int returnValue;
                            returnValue = ada.Fill(ds.CZMST094);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return;
            }
        }

        #endregion 


        #endregion
    }
}
