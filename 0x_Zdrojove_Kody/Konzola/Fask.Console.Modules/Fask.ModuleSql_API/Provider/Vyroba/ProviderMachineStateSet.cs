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
using Fask.Interfaces.Filtry;

namespace Fask.ModuleSql_API
{
    public partial class Provider :
        Fask.Interfaces.Vyroba.Odvod_MachineStateSet.IOdvod_MachineStateSet,
         Fask.Interfaces.Vyroba.Odvod_MachineStateSet.IOdvod_MachineStateSet_GetFiltrovanyMachineStateSets,
        Interfaces.Vyroba.Odvod_MachineStateSet.IOdvod_MachineStateSet_GetEnum_description
    {
        public List<string> LoadStrojDescriptionFromDatabase()
        {
            List<string> descriptions = new List<string>();

            try
            {
                IRestResponse restResponse;
                string param = "Konzola_MachineStateSet_GetEnum_description";
                string JSON = "";

                // JSON = JSON_Class.Serialize_JSON(filtr);

                if (!Communicate(Terminal_ID, REST_Type.GET, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    descriptions = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(restResponse.Content);



                    return descriptions;

                }

                return null;
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                return null;
            }
        }

        public List<string> LoadStrojNameFromDatabase()
        {
           
            List<string> descriptions = new List<string>();

            try
            {
                IRestResponse restResponse;
                string param = "Konzola_MachineStateSet_GetEnum_description";
                string JSON = "";

               // JSON = JSON_Class.Serialize_JSON(filtr);

                if (!Communicate(Terminal_ID, REST_Type.GET, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    descriptions = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(restResponse.Content);

                  

                    return descriptions;

                }

                return null;
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                return null;
            }

        }

        #region IOdvod_MachineStateSet


        #region IOdvod_MachineStateSet_GetFiltrovanyMachineStateSet Members

        public Interfaces.DataSets.Vyroba MachineStateSet_GetFiltrovanyOdvodMachineStateSet(Fask.Interfaces.Filtry.Odvod_MachineStateSetListFiltr filtr)
        {
            Fask.WEBAPI.API_BusinessObjects.BO_MachineStateSet bo = new Fask.WEBAPI.API_BusinessObjects.BO_MachineStateSet();
            Vyroba.MachineStateSetHistoryDataTable dt = new Vyroba.MachineStateSetHistoryDataTable();
            //int result = -1;

            try
            {
                IRestResponse restResponse;
                string param = "Konzola_MachineStateSet_GetFiltrovanyOdvodMachineStateSet";
                string JSON = "";

                JSON = JSON_Class.Serialize_JSON(filtr);

                if (!Communicate(Terminal_ID, REST_Type.POST, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    bo = Newtonsoft.Json.JsonConvert.DeserializeObject<Fask.WEBAPI.API_BusinessObjects.BO_MachineStateSet>(restResponse.Content);

                    //TODO MaR naplnit dt z bo
                    BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_MachineStateSet, Fask.WEBAPI.API_BusinessObjects.BO_MachineStateSet_row, Vyroba.MachineStateSetHistoryDataTable, Vyroba.MachineStateSetHistoryRow> y = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_MachineStateSet, Fask.WEBAPI.API_BusinessObjects.BO_MachineStateSet_row, Vyroba.MachineStateSetHistoryDataTable, Vyroba.MachineStateSetHistoryRow>();
                    dt = y.GetDTFromBO(bo);

                    Vyroba ds = new Vyroba();
                    ds.MachineStateSetHistory.Clear();

                    foreach (var item in dt)
                    {
                        ds.MachineStateSetHistory.ImportRow(item);
                    }

                    ds.MachineStateSetHistory.AcceptChanges();

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

        public Vyroba MachineStateSet_GetFiltrovanyOdvodMachineStateSet_Analyza_Odvodu(Odvod_MachineStateSetListFiltr filtr)
        {
            throw new NotImplementedException();
        }

        #endregion



        #endregion
    }
}
