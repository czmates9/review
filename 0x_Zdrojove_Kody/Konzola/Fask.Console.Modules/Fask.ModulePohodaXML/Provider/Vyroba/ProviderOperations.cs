using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Fask.ModulePohodaXML.Provider
{
    public partial class Provider :
        Fask.Interfaces.Vyroba.Operations.IOperations,
        Fask.Interfaces.Vyroba.Operations.IOperations_GetDataByID,
        Fask.Interfaces.Vyroba.Operations.IOperations_Fill
    {

        #region IOperations_GetDataByID Members

        Fask.Interfaces.DataSets.Vyroba.OperationsDataTable Fask.Interfaces.Vyroba.Operations.IOperations_GetDataByID.Operations_GetDataByID(string ID)
        {
            //Globals_V1.LoadConfiguration();
            //Fask.Interfaces.DataSets.Vyroba.OperationsDataTable dt = new Fask.Interfaces.DataSets.Vyroba.OperationsDataTable();

            //var ta = new Pohoda_DataSets.VyrobaDataSetTableAdapters.OperationsTableAdapter();
            //ta.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

            //var tmp = ta.GetDataByID(ID);


            //foreach (var item in tmp)
            //{
            //    dt.ImportRow(item);
            //}


            //return dt;

            try
            {
                Globals_V1.LoadConfiguration();
                Fask.Interfaces.DataSets.Vyroba.OperationsDataTable dt = new Fask.Interfaces.DataSets.Vyroba.OperationsDataTable();

                using (var con = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                {
                    using (var ada = new System.Data.SqlClient.SqlDataAdapter())
                    {
                        using (var com = con.CreateCommand())
                        {
                            var select = @"SELECT * FROM " + Fask.SQL.Constants.Common.TABLE_Operations+
                                " WHERE id = '" + ID + "'";

                            ada.SelectCommand = com;
                            ada.SelectCommand.CommandText = select;
                            ada.SelectCommand.Connection = con;
                            ada.SelectCommand.CommandType = CommandType.Text;

                            int returnValue;
                            returnValue = ada.Fill(dt);
                        }
                    }
                }

                return dt;
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return null;
            }

        }

        #endregion

        #region IOperations_Fill Members

        void Fask.Interfaces.Vyroba.Operations.IOperations_Fill.Operations_Fill(Fask.Interfaces.DataSets.Vyroba ds)
        {
            //Globals_V1.LoadConfiguration();
            //ds.Operations.Clear();
            //Pohoda_DataSets.VyrobaDataSet tmp = new Pohoda_DataSets.VyrobaDataSet();

            //var ta = new Pohoda_DataSets.VyrobaDataSetTableAdapters.OperationsTableAdapter();
            //ta.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

            //ta.Fill(tmp.Operations);

            //foreach (var item in tmp.Operations)
            //{
            //    ds.Operations.ImportRow(item);
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
                            var select = @"SELECT * FROM " + Fask.SQL.Constants.Common.TABLE_Operations;

                            ada.SelectCommand = com;
                            ada.SelectCommand.CommandText = select;
                            ada.SelectCommand.Connection = con;
                            ada.SelectCommand.CommandType = CommandType.Text;

                            int returnValue;
                            returnValue = ada.Fill(ds.Operations);
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
    }
}
