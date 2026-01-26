using Fask.Interfaces.DataSets;
using Fask.Logging;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Fask.Extension;
using Fask.WEBAPI;
using static Fask.ModuleSql_API.Classes.Comunication;

namespace Fask.ModuleSql_API
{
    public partial class Provider :
                Fask.Interfaces.Ciselniky.Sklady.ISklady2,
                Fask.Interfaces.Ciselniky.Sklady.ISklady2_GetSklady,
                Fask.Interfaces.Ciselniky.Sklady.ISklady2_Vyroba_Fill,
                Fask.Interfaces.Ciselniky.Lokace.ILokace2,
                Fask.Interfaces.Ciselniky.Lokace.ILokace2_Fill

    {
        

        public Sklady GetSklady()
        {
            Sklady ds = null;
            Fask.WEBAPI.API_BusinessObjects.BO_CZMST093 FZ_data = new Fask.WEBAPI.API_BusinessObjects.BO_CZMST093();
            try
            {
                IRestResponse restResponse;
                string param = "Konzola_GetSklady";
                string JSON = "";

                if (!Communicate(Terminal_ID, REST_Type.GET, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    FZ_data = Newtonsoft.Json.JsonConvert.DeserializeObject<Fask.WEBAPI.API_BusinessObjects.BO_CZMST093>(restResponse.Content);

                    //FZ_data = ds.DataSetToBO();
                    ds = FZ_data.BOToDataSet();

                    return ds;
                }

                return null;
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                return null;
            }
        }

        public void Sklady_Vyroba_Fill(Fask.Interfaces.DataSets.Vyroba ds)
        {

            #region old
            // Database.Ciselnik cis = new Database.Ciselnik();
            //cis.Fill_Universal(ConnectionString,
            //    "SELECT * FROM " + Tables.TABLE_CZMST093,
            //    ds.CZMST093
            //    ); 
            #endregion


            Fask.WEBAPI.API_BusinessObjects.BO_CZMST093 bo = new Fask.WEBAPI.API_BusinessObjects.BO_CZMST093();
            Vyroba.CZMST093DataTable dt = new Vyroba.CZMST093DataTable();
            //int result = -1;
            ds.CZMST093.Clear();

            try
            {
               
                string param = "Konzola_Sklady_Vyroba_Fill";
                string JSON = "";
                IRestResponse restResponse;

                if (!Communicate(Terminal_ID, REST_Type.GET, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    bo = Newtonsoft.Json.JsonConvert.DeserializeObject<Fask.WEBAPI.API_BusinessObjects.BO_CZMST093>(restResponse.Content);

                    //TODO MaR naplnit dt z bo
                    BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZMST093, Fask.WEBAPI.API_BusinessObjects.BO_CZMST093_row, Vyroba.CZMST093DataTable, Vyroba.CZMST093Row> y = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZMST093, Fask.WEBAPI.API_BusinessObjects.BO_CZMST093_row, Vyroba.CZMST093DataTable, Vyroba.CZMST093Row>();
                    dt = y.GetDTFromBO(bo);

                    foreach (var item in dt)
                    {
                        ds.CZMST093.ImportRow(item);
                    }

                    ds.CZMST093.AcceptChanges();

                    //return -1;
                }

               // return -1;
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
               // return -1;
            }
        }

        /// <summary>
        /// ILokace2
        /// </summary>
        /// <param name="ds"></param>
        public void Fill(Vyroba ds)
        {
            Fask.WEBAPI.API_BusinessObjects.BO_CZMST094 bo = new Fask.WEBAPI.API_BusinessObjects.BO_CZMST094();
            Vyroba.CZMST094DataTable dt = new Vyroba.CZMST094DataTable();
            //int result = -1;
            ds.CZMST094.Clear();

            try
            {

                string param = "Konzola_Lokace_Fill";
                string JSON = "";
                IRestResponse restResponse;

                if (!Communicate(Terminal_ID, REST_Type.GET, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    bo = Newtonsoft.Json.JsonConvert.DeserializeObject<Fask.WEBAPI.API_BusinessObjects.BO_CZMST094>(restResponse.Content);

                    //TODO MaR naplnit dt z bo
                    BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZMST094, Fask.WEBAPI.API_BusinessObjects.BO_CZMST094_row, Vyroba.CZMST094DataTable, Vyroba.CZMST094Row> y = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZMST094, Fask.WEBAPI.API_BusinessObjects.BO_CZMST094_row, Vyroba.CZMST094DataTable, Vyroba.CZMST094Row>();
                    dt = y.GetDTFromBO(bo);

                    foreach (var item in dt)
                    {
                        ds.CZMST094.ImportRow(item);
                    }

                    ds.CZMST094.AcceptChanges();

                    //return -1;
                }

                // return -1;
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                // return -1;
            }
        }

    }
}
