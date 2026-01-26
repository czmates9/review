using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Fask.RestSharp.API;
using RestSharp;
using Fask.Logging;
using System.Net;
using Fask.Interfaces.DataSets;
using static Fask.ModuleSql_API.Classes.Comunication;
using Fask.WEBAPI;

namespace Fask.ModuleSql_API
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

            //API
            //return Vyroba_VMachinesOperations.GetDataByMachineIDoperationID(ConnectionString, MachinesID, OperationsID);

            Fask.WEBAPI.API_BusinessObjects.BO_VMachinesOperations bo = new Fask.WEBAPI.API_BusinessObjects.BO_VMachinesOperations();
            Vyroba.VMachinesOperationsDataTable dt = new Vyroba.VMachinesOperationsDataTable();
            //int result = -1;
            // ds.VMachinesOperations.Clear();

            try
            {
                #region skladani URL
                HttpValueCollection URI_param_tmp = new HttpValueCollection();
                string tmp = null;

                if(MachinesID != null)
                URI_param_tmp.Add("MachinesID", MachinesID);

                if (OperationsID != null)
                    URI_param_tmp.Add("OperationsID", OperationsID);


                tmp += "?";
                tmp += URI_param_tmp.ToString();
                #endregion

                string param = "Konzola_VMachinesOperations_GetDataByMachineIDoperationID" + tmp;
                string JSON = "";
                IRestResponse restResponse;

                if (!Communicate(Terminal_ID, REST_Type.GET, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    bo = Newtonsoft.Json.JsonConvert.DeserializeObject<Fask.WEBAPI.API_BusinessObjects.BO_VMachinesOperations>(restResponse.Content);

                    //TODO MaR naplnit dt z bo
                    BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_VMachinesOperations, Fask.WEBAPI.API_BusinessObjects.BO_VMachinesOperations_row, Vyroba.VMachinesOperationsDataTable, Vyroba.VMachinesOperationsRow> y = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_VMachinesOperations, Fask.WEBAPI.API_BusinessObjects.BO_VMachinesOperations_row, Vyroba.VMachinesOperationsDataTable, Vyroba.VMachinesOperationsRow>();
                    dt = y.GetDTFromBO(bo);


                    return dt;
                }

                return null;
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                return null;
            }
        }

        #endregion
    }
}
