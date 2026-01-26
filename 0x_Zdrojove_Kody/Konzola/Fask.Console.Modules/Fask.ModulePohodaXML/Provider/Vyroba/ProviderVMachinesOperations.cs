using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Fask.ModulePohodaXML.Provider
{
    public partial class Provider  :
        Fask.Interfaces.Vyroba.VMachinesOperations.IVMachinesOperations,
        Fask.Interfaces.Vyroba.VMachinesOperations.IVMachinesOperations_GetDataByMachineIDoperationID

    {

        #region IVMachinesOperations_GetDataByMachineIDoperationID Members

        public Fask.Interfaces.DataSets.Vyroba.VMachinesOperationsDataTable VMachinesOperations_GetDataByMachineIDoperationID(string MachinesID, string OperationsID)
        {
            //Globals_V1.LoadConfiguration();
            //Fask.Interfaces.DataSets.Vyroba.VMachinesOperationsDataTable dt = new Fask.Interfaces.DataSets.Vyroba.VMachinesOperationsDataTable();

            //var ta = new Pohoda_DataSets.VyrobaDataSetTableAdapters.VMachinesOperationsTableAdapter();
            //ta.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

            //var tmp = ta.GetDataByMachineIDoperationID(MachinesID, OperationsID);


            //foreach (var item in tmp)
            //{
            //    dt.ImportRow(item);
            //}


            //return dt;

            try
            {
                Globals_V1.LoadConfiguration();
                Fask.Interfaces.DataSets.Vyroba.VMachinesOperationsDataTable dt = new Fask.Interfaces.DataSets.Vyroba.VMachinesOperationsDataTable();

                using (var con = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                {
                    using (var ada = new System.Data.SqlClient.SqlDataAdapter())
                    {
                        using (var com = con.CreateCommand())
                        {
                            var select = @"SELECT * FROM " + Fask.SQL.Constants.Common.TABLE_VMachinesOperations +
                                " (machineid = '" + MachinesID + "') AND (operationid = '" + OperationsID + "') ";

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
    }
}
