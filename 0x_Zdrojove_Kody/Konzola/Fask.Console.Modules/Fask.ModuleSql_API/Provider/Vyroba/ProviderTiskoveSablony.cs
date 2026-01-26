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
        Fask.Interfaces.Vyroba.Odvod_TiskoveSablony.IOdvod_TiskoveSablony,
         Fask.Interfaces.Vyroba.Odvod_TiskoveSablony.IOdvod_TiskoveSablony_GetFiltrovanyTiskoveSablony
    {

        #region IOdvod_TiskoveSablony


        #region IOdvod_TiskoveSablony_GetFiltrovanyTiskoveSablony Members

        public Interfaces.DataSets.Vyroba TiskoveSablony_GetFiltrovanyOdvodTiskoveSablony(Fask.Interfaces.Filtry.Odvod_TiskoveSablonyFiltr filtr)
        {
            Fask.WEBAPI.API_BusinessObjects.BO_FASK_FORMULARE bo = new Fask.WEBAPI.API_BusinessObjects.BO_FASK_FORMULARE();
            Vyroba.FASK_FORMULAREDataTable dt = new Vyroba.FASK_FORMULAREDataTable();
            //int result = -1;

            try
            {
                IRestResponse restResponse;
                //string param = "Konzola_MachineStateSet_GetFiltrovanyOdvodMachineStateSet";
                string param = "Konzola_TiskoveSablony_GetFiltrovanyOdvodTiskoveSablony";
                string JSON = "";

                JSON = JSON_Class.Serialize_JSON(filtr);

                if (!Communicate(Terminal_ID, REST_Type.POST, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    bo = Newtonsoft.Json.JsonConvert.DeserializeObject<Fask.WEBAPI.API_BusinessObjects.BO_FASK_FORMULARE>(restResponse.Content);

                    //TODO MaR naplnit dt z bo
                    BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_FASK_FORMULARE, Fask.WEBAPI.API_BusinessObjects.BO_FASK_FORMULARE_row, Vyroba.FASK_FORMULAREDataTable, Vyroba.FASK_FORMULARERow> y = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_FASK_FORMULARE, Fask.WEBAPI.API_BusinessObjects.BO_FASK_FORMULARE_row, Vyroba.FASK_FORMULAREDataTable, Vyroba.FASK_FORMULARERow>();
                    dt = y.GetDTFromBO(bo);

                    Vyroba ds = new Vyroba();
                    ds.FASK_FORMULARE.Clear();

                    foreach (var item in dt)
                    {
                        ds.FASK_FORMULARE.ImportRow(item);
                    }

                    ds.FASK_FORMULARE.AcceptChanges();

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

        public string TiskoveSablony_GetFiltrovanyOdvodTiskoveSablony_Path(Odvod_TiskoveSablonyFiltr filtr)
        {
            throw new NotImplementedException();
        }

        #endregion



        #endregion
    }
}
