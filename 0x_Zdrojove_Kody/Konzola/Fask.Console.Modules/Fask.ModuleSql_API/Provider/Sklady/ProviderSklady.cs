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
using Fask.Interfaces.Classes;
using Fask.Interfaces.Filtry;

namespace Fask.ModuleSql_API
{
    public partial class Provider :
        Fask.Interfaces.Prodej.IProdej2,
        Fask.Interfaces.Prodej.IProdej2_ImportDavka,
        Fask.Interfaces.Prodej.IProdej2_Prodej_GetFiltrovaneDavky

    {
        public StatusInfo ImportDavka(Objednavka objednavka, Sklad sklad, bool Grupuj)
        {
            throw new NotImplementedException();
        }

        public Prodej Prodej_GetFiltrovaneDavky(ProdejFiltr filtr)
        {
            Fask.WEBAPI.API_BusinessObjects.BO_CZMST_DI bo = new Fask.WEBAPI.API_BusinessObjects.BO_CZMST_DI();
            
            Prodej.CZMST_DIDataTable dt = new Prodej.CZMST_DIDataTable();
            //int result = -1;

            try
            {
                IRestResponse restResponse;
                string param = "Konzola_Prodej_GetFiltrovaneDavky";
                string JSON = "";

                JSON = JSON_Class.Serialize_JSON(filtr);

                if (!Communicate(Terminal_ID, REST_Type.POST, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    bo = Newtonsoft.Json.JsonConvert.DeserializeObject<Fask.WEBAPI.API_BusinessObjects.BO_CZMST_DI>(restResponse.Content);

                    //TODO MaR naplnit dt z bo
                    BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZMST_DI, Fask.WEBAPI.API_BusinessObjects.BO_CZMST_DI_row, Prodej.CZMST_DIDataTable, Prodej.CZMST_DIRow> y = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZMST_DI, Fask.WEBAPI.API_BusinessObjects.BO_CZMST_DI_row, Prodej.CZMST_DIDataTable, Prodej.CZMST_DIRow>();
                     dt = y.GetDTFromBO(bo);

                    Prodej ds = new Prodej();
                    ds.CZMST_DI.Clear();

                    foreach (var item in dt)
                    {
                        ds.CZMST_DI.ImportRow(item);
                    }

                    ds.CZMST_DI.AcceptChanges();

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
    }
}
