using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Fask.Logging;
using RestSharp;
using System.Net;
using Fask.Interfaces.DataSets;
using Fask.RestSharp.API;
using static Fask.ModuleSql_API.Classes.Comunication;
using Fask.WEBAPI;

namespace Fask.ModuleSql_API
{
    public partial class Provider :
        Fask.Interfaces.Vyroba.Operations.IOperations,
        Fask.Interfaces.Vyroba.Operations.IOperations_GetDataByID,
        Fask.Interfaces.Vyroba.Operations.IOperations_Fill
    {

        #region IOperations_GetDataByID Members

        Fask.Interfaces.DataSets.Vyroba.OperationsDataTable Fask.Interfaces.Vyroba.Operations.IOperations_GetDataByID.Operations_GetDataByID(string ID)
        {
            Fask.WEBAPI.API_BusinessObjects.BO_Operations bo = new Fask.WEBAPI.API_BusinessObjects.BO_Operations();
            Vyroba.OperationsDataTable dt = new Vyroba.OperationsDataTable();
            //int result = -1;
            // ds.Operations.Clear();

            try
            {
                #region skladani URL
                HttpValueCollection URI_param_tmp = new HttpValueCollection();
                string tmp = null;

                URI_param_tmp.Add("ID", ID);


                tmp += "?";
                tmp += URI_param_tmp.ToString();
                #endregion

                string param = "Konzola_Operations_GetDataByID" + tmp;
                string JSON = "";
                IRestResponse restResponse;

                if (!Communicate(Terminal_ID, REST_Type.GET, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    bo = Newtonsoft.Json.JsonConvert.DeserializeObject<Fask.WEBAPI.API_BusinessObjects.BO_Operations>(restResponse.Content);

                    //TODO MaR naplnit dt z bo
                    BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Operations, Fask.WEBAPI.API_BusinessObjects.BO_Operations_row, Vyroba.OperationsDataTable, Vyroba.OperationsRow> y = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Operations, Fask.WEBAPI.API_BusinessObjects.BO_Operations_row, Vyroba.OperationsDataTable, Vyroba.OperationsRow>();
                    dt = y.GetDTFromBO(bo);

                    //foreach (var item in dt)
                    //{
                    //    ds.Operations.ImportRow(item);
                    //}

                    //ds.Operations.AcceptChanges();

                    return dt;
                }

                return null;
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                return null;
            }

            #region old sql
            ////Fask.Interfaces.DataSets.Vyroba.OperationsDataTable dt = new Fask.Interfaces.DataSets.Vyroba.OperationsDataTable();

            ////var ta = new Fask.ModuleSql.SQL_Datasets.VyrobaDataSetTableAdapters.OperationsTableAdapter();
            ////ta.Connection = new SqlConnection(ConnectionString);

            ////var tmp = ta.GetDataByID(ID);


            ////foreach (var item in tmp)
            ////{
            ////    dt.ImportRow(item);
            ////}


            ////return dt;

            //return Vyroba_Operations.Operations_GetDataByID(ConnectionString, ID); 
            #endregion
        }

        #endregion

        #region IOperations_Fill Members

        void Fask.Interfaces.Vyroba.Operations.IOperations_Fill.Operations_Fill(Fask.Interfaces.DataSets.Vyroba ds)
        {
            Fask.WEBAPI.API_BusinessObjects.BO_Operations bo = new Fask.WEBAPI.API_BusinessObjects.BO_Operations();
            Vyroba.OperationsDataTable dt = new Vyroba.OperationsDataTable();
            //int result = -1;
            ds.Operations.Clear();

            try
            {

                string param = "Konzola_Operations_Fill";
                string JSON = "";
                IRestResponse restResponse;

                if (!Communicate(Terminal_ID, REST_Type.GET, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    bo = Newtonsoft.Json.JsonConvert.DeserializeObject<Fask.WEBAPI.API_BusinessObjects.BO_Operations>(restResponse.Content);

                    //TODO MaR naplnit dt z bo
                    BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Operations, Fask.WEBAPI.API_BusinessObjects.BO_Operations_row, Vyroba.OperationsDataTable, Vyroba.OperationsRow> y = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Operations, Fask.WEBAPI.API_BusinessObjects.BO_Operations_row, Vyroba.OperationsDataTable, Vyroba.OperationsRow>();
                    dt = y.GetDTFromBO(bo);

                    foreach (var item in dt)
                    {
                        ds.Operations.ImportRow(item);
                    }

                    ds.Operations.AcceptChanges();

                    // return -1;
                }

                // return -1;
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                // return -1;
            }

            #region old sql
            ////SQL_Datasets.VyrobaDataSet tmp = new SQL_Datasets.VyrobaDataSet();

            ////var ta = new Fask.ModuleSql.SQL_Datasets.VyrobaDataSetTableAdapters.OperationsTableAdapter();
            ////ta.Connection = new SqlConnection(ConnectionString);

            ////ta.Fill(tmp.Operations);

            ////foreach (var item in tmp.Operations)
            ////{
            ////    ds.Operations.ImportRow(item);
            ////}

            //Vyroba_Operations.Operations_Fill(ConnectionString, ds); 
            #endregion
        }

        #endregion
    }
}
