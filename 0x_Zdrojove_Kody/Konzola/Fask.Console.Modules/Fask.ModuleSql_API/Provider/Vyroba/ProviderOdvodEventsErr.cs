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
using Fask.WEBAPI;

namespace Fask.ModuleSql_API
{
    public partial class Provider :
        Fask.Interfaces.Vyroba.Odvod_EventsErr.IOdvod_EventsErr,
        Fask.Interfaces.Vyroba.Odvod_EventsErr.IOdvod_EventsErr_GetFiltrovanyOdvodEvents
    {
        #region IOdvod_EventsErr


        #region IOdvod_EventsErr_GetFiltrovanyOdvodEvents Members

        public Fask.Interfaces.DataSets.Vyroba EventsErr_GetFiltrovanyOdvodEvents(Fask.Interfaces.Filtry.Odvod_EventsErrListFiltr filtr)
        {
            Fask.WEBAPI.API_BusinessObjects.BO_FASK_EventsErr bo = new Fask.WEBAPI.API_BusinessObjects.BO_FASK_EventsErr();
            Vyroba.FASK_EventsErrDataTable dt = new Vyroba.FASK_EventsErrDataTable();
            //int result = -1;
           
            try
            {
                IRestResponse restResponse;
                string param = "Konzola_EventsErr_GetFiltrovanyOdvodEvents";
                string JSON = "";

                JSON = JSON_Class.Serialize_JSON(filtr);

                if (!Communicate(Terminal_ID, REST_Type.POST, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    bo = Newtonsoft.Json.JsonConvert.DeserializeObject<Fask.WEBAPI.API_BusinessObjects.BO_FASK_EventsErr>(restResponse.Content);

                    //TODO MaR naplnit dt z bo
                    BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_FASK_EventsErr, Fask.WEBAPI.API_BusinessObjects.BO_FASK_EventsErr_row, Vyroba.FASK_EventsErrDataTable, Vyroba.FASK_EventsErrRow> y = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_FASK_EventsErr, Fask.WEBAPI.API_BusinessObjects.BO_FASK_EventsErr_row, Vyroba.FASK_EventsErrDataTable, Vyroba.FASK_EventsErrRow>();
                    dt = y.GetDTFromBO(bo);

                    Vyroba ds = new Vyroba();
                    ds.FASK_EventsErr.Clear();

                    foreach (var item in dt)
                    {
                        ds.FASK_EventsErr.ImportRow(item);
                    }

                    ds.FASK_EventsErr.AcceptChanges();

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

     

        #endregion


    }
}
