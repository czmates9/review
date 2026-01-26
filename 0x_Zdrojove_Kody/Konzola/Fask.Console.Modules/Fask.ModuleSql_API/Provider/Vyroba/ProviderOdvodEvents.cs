using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Fask.Interfaces.DataSets;
using RestSharp;
using Fask.Logging;
using System.Net;
using Fask.Extension;
using Fask.RestSharp.API;
using Fask.ModuleSql_API.Classes;
using static Fask.ModuleSql_API.Classes.Comunication;

namespace Fask.ModuleSql_API
{
    public partial class Provider :
        Fask.Interfaces.Vyroba.Odvod_Events.IOdvod_Events,
        Fask.Interfaces.Vyroba.Odvod_Events.IOdvod_Events_GetFiltrovanyOdvodEvents,
        Fask.Interfaces.Vyroba.Odvod_Events.IOdvod_Events_CallProcedura,
        Fask.Interfaces.Vyroba.Odvod_Events.IOdvod_Events_GetMaterials,
        Fask.Interfaces.Vyroba.Odvod_Events.IOdvod_Events_StornoEvent,
        Fask.Interfaces.Vyroba.Odvod_Events.IOdvod_Events_StornoEvent_OnlineCheck
    {

        public string ConnectionString = null; //19.5.2022 MaR smazat po dokonceni

        #region IOdvod_Events


        #region IOdvod_Events_GetFiltrovanyOdvodEvents Members

        public Vyroba GetFiltrovanyOdvodEvents(Fask.Interfaces.Filtry.Odvod_EventsListFiltr filtr)
        {
            Vyroba ds = new Vyroba();
            Fask.WEBAPI.API_BusinessObjects.FASK_Events FZ_data = new Fask.WEBAPI.API_BusinessObjects.FASK_Events();
            try
            {
                IRestResponse restResponse;
                string param = "Konzola_GetFiltrovanyOdvodEvents";
                string JSON = "";

                JSON = JSON_Class.Serialize_JSON(filtr);

                if (!Communicate(Terminal_ID, REST_Type.POST, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    FZ_data = Newtonsoft.Json.JsonConvert.DeserializeObject<Fask.WEBAPI.API_BusinessObjects.FASK_Events>(restResponse.Content);

                    //FZ_data = ds.DataSetToBO();
                    //ds = FZ_data.BOToDataSet();
                    foreach (var item in FZ_data.BOToDataSet())
                    {
                        ds.FASK_Events.ImportRow(item);
                    }

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

        #endregion 

        #region IOdvod_Events_CallProcedura Members

        public string CallProcedura(DateTime? OD, DateTime? DO, string Material, out int? CountEntries)
        {

            string result = string.Empty;
            CountEntries = null;
            Fask.WEBAPI.API_BusinessObjects.BO_CallProcedura bo = new Fask.WEBAPI.API_BusinessObjects.BO_CallProcedura();
            try
            {
                #region skladani URL
                HttpValueCollection URI_param_tmp = new HttpValueCollection();
                string tmp = null;

                if (OD.HasValue)
                    URI_param_tmp.Add("OD", OD.Value.ToString(System.Globalization.DateTimeFormatInfo.InvariantInfo));

                if (DO.HasValue)
                    URI_param_tmp.Add("DO", DO.Value.ToString(System.Globalization.DateTimeFormatInfo.InvariantInfo));

                if (!string.IsNullOrEmpty(Material))
                    URI_param_tmp.Add("Material", Material);

                tmp += "?";
                tmp += URI_param_tmp.ToString();
                #endregion

                string param = "Konzola_CallProcedura" + tmp;
                string JSON = "";
                IRestResponse restResponse;
                //JSON = JSON_Class.Serialize_JSON(filtr);

                if (!Communicate(Terminal_ID, REST_Type.GET, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    bo = Newtonsoft.Json.JsonConvert.DeserializeObject<Fask.WEBAPI.API_BusinessObjects.BO_CallProcedura>(restResponse.Content);
                    CountEntries = bo.CountEntries;
                    result = bo.result;

                    return result;
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

        #region IOdvod_Events_GetMaterials Members

        public DataTable GetMaterials()
        {

            DataTable dt = new DataTable();
            Fask.WEBAPI.API_BusinessObjects.BO_Material FZ_data = new Fask.WEBAPI.API_BusinessObjects.BO_Material();
            try
            {
                IRestResponse restResponse;
                string param = "Konzola_GetMaterials";
                string JSON = "";

                //JSON = JSON_Class.Serialize_JSON(filtr);

                if (!Communicate(Terminal_ID, REST_Type.GET, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    FZ_data = Newtonsoft.Json.JsonConvert.DeserializeObject<Fask.WEBAPI.API_BusinessObjects.BO_Material>(restResponse.Content);

                    dt.Columns.Add("material");
                    //FZ_data = ds.DataSetToBO();
                    //ds = FZ_data.BOToDataSet();
                    foreach (var item in FZ_data.material)
                    {
                        var row = dt.NewRow();
                        row["material"] = item;
                        dt.Rows.Add(row);
                    }

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

        #region IOdvod_Events_StornoEvent Members

        public bool StornoEvent(Vyroba.FASK_EventsRow row)
        {
            Vyroba ds = new Vyroba();
            Fask.WEBAPI.API_BusinessObjects.FASK_Events FZ_data = new Fask.WEBAPI.API_BusinessObjects.FASK_Events();
            bool result = false;

            try
            {
                row.AcceptChanges();
                ds.FASK_Events.ImportRow(row);
                FZ_data = ds.FASK_Events.DataSetToBO();


                IRestResponse restResponse;
                string param = "Konzola_StornoEvent";
                string JSON = "";

                JSON = JSON_Class.Serialize_JSON(FZ_data);

                if (!Communicate(Terminal_ID, REST_Type.POST, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<bool>(restResponse.Content);

                    
                    return result;
                }

                return false;
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                return false;
            }

        }

        public int? StornoEvent_OnlineCheck(Guid G)
        {

            int? result = null;

            try
            {
                IRestResponse restResponse;
                string param = "Konzola_StornoEvent_OnlineCheck";
                string JSON = "";

                JSON = JSON_Class.Serialize_JSON(G);

                if (!Communicate(Terminal_ID, REST_Type.POST, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<int?>(restResponse.Content);
                    return result;
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

        #endregion


    }
}
