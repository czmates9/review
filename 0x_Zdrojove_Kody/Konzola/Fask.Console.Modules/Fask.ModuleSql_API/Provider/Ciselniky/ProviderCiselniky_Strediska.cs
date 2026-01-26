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
using Fask.Interfaces.Filtry;

namespace Fask.ModuleSql_API
{
    public partial class Provider :

        Fask.Interfaces.Ciselniky.Strediska.IStrediska2,
        Fask.Interfaces.Ciselniky.Strediska.IStrediska2_GetFiltrovaneStrediska

    {

        #region old
        //public Odberatele GetFiltrovaneOdberatele(AdresarListFiltr filter)
        //{
        //    Fask.Interfaces.DataSets.Odberatele ds = new Fask.Interfaces.DataSets.Odberatele();
        //    Fask.WEBAPI.API_BusinessObjects.BO_CZMST090 bo = new Fask.WEBAPI.API_BusinessObjects.BO_CZMST090();
        //    Odberatele.CZMST090DataTable dt = new Odberatele.CZMST090DataTable();
        //    ds.CZMST090.Clear();

        //    try
        //    {
        //        IRestResponse restResponse;
        //        string param = "Konzola_GetFiltrovaneZbozi";
        //        string JSON = "";

        //        JSON = Classes.JSON_Class.Serialize_JSON(filter);

        //        if (!Communicate(Terminal_ID, REST_Type.POST, out restResponse, param, JSON))
        //        {
        //            ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
        //        }

        //        if (restResponse.StatusCode == HttpStatusCode.OK)
        //        {
        //            bo = Newtonsoft.Json.JsonConvert.DeserializeObject<Fask.WEBAPI.API_BusinessObjects.BO_CZMST090>(restResponse.Content);


        //            BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZMST090, Fask.WEBAPI.API_BusinessObjects.BO_CZMST090_row, Odberatele.CZMST090DataTable, Odberatele.CZMST090Row> y = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZMST090, Fask.WEBAPI.API_BusinessObjects.BO_CZMST090_row, Odberatele.CZMST090DataTable, Odberatele.CZMST090Row>();
        //            dt = y.GetDTFromBO(bo);

        //            foreach (var item in dt)
        //            {
        //                ds.CZMST090.ImportRow(item);
        //            }

        //            ds.CZMST090.AcceptChanges();

        //            ////FZ_data = ds.DataSetToBO();
        //            ////ds = FZ_data.BOToDataSet();
        //            //foreach (var item in FZ_data.BOToDataSet())
        //            //{
        //            //    ds.CZMST090.ImportRow(item);
        //            //}

        //            return ds;
        //        }

        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionHandler2.Handle(ex);
        //        return null;
        //    }
        //} 
        #endregion
        public Strediska GetFiltrovaneStrediska(StrediskaListFiltr filtr)
        {
            Fask.Interfaces.DataSets.Strediska ds = new Fask.Interfaces.DataSets.Strediska();
            Fask.WEBAPI.API_BusinessObjects.BO_CZMST091 bo = new Fask.WEBAPI.API_BusinessObjects.BO_CZMST091();
            Strediska.CZMST091DataTable dt = new Strediska.CZMST091DataTable();
            ds.CZMST091.Clear();

            try
            {
                IRestResponse restResponse;
                string param = "Konzola_GetFiltrovaneStrediska";
                string JSON = "";

                JSON = Classes.JSON_Class.Serialize_JSON(filtr);

                if (!Communicate(Terminal_ID, REST_Type.POST, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    bo = Newtonsoft.Json.JsonConvert.DeserializeObject<Fask.WEBAPI.API_BusinessObjects.BO_CZMST091>(restResponse.Content);


                    BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZMST091, Fask.WEBAPI.API_BusinessObjects.BO_CZMST091_row, Strediska.CZMST091DataTable, Strediska.CZMST091Row> y = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZMST091, Fask.WEBAPI.API_BusinessObjects.BO_CZMST091_row, Strediska.CZMST091DataTable, Strediska.CZMST091Row>();
                    dt = y.GetDTFromBO(bo);

                    foreach (var item in dt)
                    {
                        ds.CZMST091.ImportRow(item);
                    }

                    ds.CZMST091.AcceptChanges();

                    ////FZ_data = ds.DataSetToBO();
                    ////ds = FZ_data.BOToDataSet();
                    //foreach (var item in FZ_data.BOToDataSet())
                    //{
                    //    ds.CZMST090.ImportRow(item);
                    //}

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
