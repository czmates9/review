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
using Fask.Interfaces.Vyroba.Corrects;
using Fask.WEBAPI;
using static Fask.ModuleSql_API.Classes.Comunication;

namespace Fask.ModuleSql_API
{
    public partial class Provider :
        Fask.Interfaces.Vyroba.Corrects.ICorrects,
        Fask.Interfaces.Vyroba.Corrects.ICorrects_GetDataByID
    {

        #region ICorrects_GetDataByID Members

        Fask.Interfaces.DataSets.Vyroba.CorrectsDataTable ICorrects_GetDataByID.Corrects_GetDataByID(int ID)
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

            //API
            //return Vyroba_Corrects.Corrects_GetDataByID(ConnectionString, ID);

            Fask.WEBAPI.API_BusinessObjects.BO_Corrects bo = new Fask.WEBAPI.API_BusinessObjects.BO_Corrects();
            Vyroba.CorrectsDataTable dt = new Vyroba.CorrectsDataTable();
            //int result = -1;
            // ds.Corrects.Clear();

            try
            {
                #region skladani URL
                HttpValueCollection URI_param_tmp = new HttpValueCollection();
                string tmp = null;

                URI_param_tmp.Add("ID", ID.ToString());


                tmp += "?";
                tmp += URI_param_tmp.ToString();
                #endregion

                string param = "Konzola_Corrects_GetDataByID" + tmp;
                string JSON = "";
                IRestResponse restResponse;

                if (!Communicate(Terminal_ID, REST_Type.GET, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    bo = Newtonsoft.Json.JsonConvert.DeserializeObject<Fask.WEBAPI.API_BusinessObjects.BO_Corrects>(restResponse.Content);

                    //TODO MaR naplnit dt z bo
                    BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Corrects, Fask.WEBAPI.API_BusinessObjects.BO_Corrects_row, Vyroba.CorrectsDataTable, Vyroba.CorrectsRow> y = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Corrects, Fask.WEBAPI.API_BusinessObjects.BO_Corrects_row, Vyroba.CorrectsDataTable, Vyroba.CorrectsRow>();
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
