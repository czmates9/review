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

        Fask.Interfaces.Ciselniky.SkladLokace_Mapa.ISkladLokace_Mapa2,
        Fask.Interfaces.Ciselniky.SkladLokace_Mapa.ISkladLokace_Mapa2_GetFiltrovaneSkladLokace_Mapa,

        Fask.Interfaces.Ciselniky.SkladLokace_LVS.ISkladLokace_LVS2,
        Fask.Interfaces.Ciselniky.SkladLokace_LVS.ISkladLokace_LVS2_GetFiltrovanySkladLokace_LokaceVariantySortiment,

        Fask.Interfaces.Ciselniky.SkladLokace_LokaceTypy.ISkladLokace_LokaceTypy2,
        Fask.Interfaces.Ciselniky.SkladLokace_LokaceTypy.ISkladLokace_LokaceTypy2_GetFiltrovaneSkladLokace_LokaceTypy

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
        public SkladLokace GetFiltrovaneSkladLokace_LokaceTypy(LokaceTypyListFiltr filtr)
        {
            Fask.Interfaces.DataSets.SkladLokace ds = new Fask.Interfaces.DataSets.SkladLokace();
            Fask.WEBAPI.API_BusinessObjects.BO_CZMST_SkladLokace_LokaceTypy bo = new Fask.WEBAPI.API_BusinessObjects.BO_CZMST_SkladLokace_LokaceTypy();
            SkladLokace.CZMST_SkladLokace_LokaceTypyDataTable dt = new SkladLokace.CZMST_SkladLokace_LokaceTypyDataTable();
            ds.CZMST_SkladLokace_LokaceTypy.Clear();

            try
            {
                IRestResponse restResponse;
                string param = "Konzola_GetFiltrovaneSkladLokace_LokaceTypy";
                string JSON = "";

                JSON = Classes.JSON_Class.Serialize_JSON(filtr);

                if (!Communicate(Terminal_ID, REST_Type.POST, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    bo = Newtonsoft.Json.JsonConvert.DeserializeObject<Fask.WEBAPI.API_BusinessObjects.BO_CZMST_SkladLokace_LokaceTypy>(restResponse.Content);


                    BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZMST_SkladLokace_LokaceTypy, Fask.WEBAPI.API_BusinessObjects.BO_CZMST_SkladLokace_LokaceTypy_row, SkladLokace.CZMST_SkladLokace_LokaceTypyDataTable, SkladLokace.CZMST_SkladLokace_LokaceTypyRow> y = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZMST_SkladLokace_LokaceTypy, Fask.WEBAPI.API_BusinessObjects.BO_CZMST_SkladLokace_LokaceTypy_row, SkladLokace.CZMST_SkladLokace_LokaceTypyDataTable, SkladLokace.CZMST_SkladLokace_LokaceTypyRow>();
                    dt = y.GetDTFromBO(bo);

                    foreach (var item in dt)
                    {
                        ds.CZMST_SkladLokace_LokaceTypy.ImportRow(item);
                    }

                    ds.CZMST_SkladLokace_LokaceTypy.AcceptChanges();

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

        public SkladLokace GetFiltrovaneSkladLokace_Mapa(SkladLokaceMapaListFiltr filtr)
        {
            Fask.Interfaces.DataSets.SkladLokace ds = new Fask.Interfaces.DataSets.SkladLokace();
            Fask.WEBAPI.API_BusinessObjects.BO_CZMST_SkladLokace_Mapa bo = new Fask.WEBAPI.API_BusinessObjects.BO_CZMST_SkladLokace_Mapa();
            SkladLokace.CZMST_SkladLokace_MapaDataTable dt = new SkladLokace.CZMST_SkladLokace_MapaDataTable();
            ds.CZMST_SkladLokace_LokaceTypy.Clear();

            try
            {
                IRestResponse restResponse;
                string param = "Konzola_GetFiltrovaneSkladLokace_Mapa";
                string JSON = "";

                JSON = Classes.JSON_Class.Serialize_JSON(filtr);

                if (!Communicate(Terminal_ID, REST_Type.POST, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    bo = Newtonsoft.Json.JsonConvert.DeserializeObject<Fask.WEBAPI.API_BusinessObjects.BO_CZMST_SkladLokace_Mapa>(restResponse.Content);


                    BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZMST_SkladLokace_Mapa, Fask.WEBAPI.API_BusinessObjects.BO_CZMST_SkladLokace_Mapa_row, SkladLokace.CZMST_SkladLokace_MapaDataTable, SkladLokace.CZMST_SkladLokace_MapaRow> y = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZMST_SkladLokace_Mapa, Fask.WEBAPI.API_BusinessObjects.BO_CZMST_SkladLokace_Mapa_row, SkladLokace.CZMST_SkladLokace_MapaDataTable, SkladLokace.CZMST_SkladLokace_MapaRow>();
                    dt = y.GetDTFromBO(bo);

                    foreach (var item in dt)
                    {
                        ds.CZMST_SkladLokace_Mapa.ImportRow(item);
                    }

                    ds.CZMST_SkladLokace_Mapa.AcceptChanges();

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

        public SkladLokace GetFiltrovanySkladLokace_LokaceVariantySortiment(LokaceVariantySortimentListFiltr filtr)
        {
            Fask.Interfaces.DataSets.SkladLokace ds = new Fask.Interfaces.DataSets.SkladLokace();
            Fask.WEBAPI.API_BusinessObjects.BO_CZMST_SkladLokace_LokaceVariantySortiment bo = new Fask.WEBAPI.API_BusinessObjects.BO_CZMST_SkladLokace_LokaceVariantySortiment();
            SkladLokace.CZMST_SkladLokace_LokaceVariantySortimentDataTable dt = new SkladLokace.CZMST_SkladLokace_LokaceVariantySortimentDataTable();
            ds.CZMST_SkladLokace_LokaceTypy.Clear();

            try
            {
                IRestResponse restResponse;
                string param = "Konzola_GetFiltrovanySkladLokace_LokaceVariantySortiment";
                string JSON = "";

                JSON = Classes.JSON_Class.Serialize_JSON(filtr);

                if (!Communicate(Terminal_ID, REST_Type.POST, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    bo = Newtonsoft.Json.JsonConvert.DeserializeObject<Fask.WEBAPI.API_BusinessObjects.BO_CZMST_SkladLokace_LokaceVariantySortiment>(restResponse.Content);


                    BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZMST_SkladLokace_LokaceVariantySortiment, Fask.WEBAPI.API_BusinessObjects.BO_CZMST_SkladLokace_LokaceVariantySortiment_row, SkladLokace.CZMST_SkladLokace_LokaceVariantySortimentDataTable, SkladLokace.CZMST_SkladLokace_LokaceVariantySortimentRow> y = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZMST_SkladLokace_LokaceVariantySortiment, Fask.WEBAPI.API_BusinessObjects.BO_CZMST_SkladLokace_LokaceVariantySortiment_row, SkladLokace.CZMST_SkladLokace_LokaceVariantySortimentDataTable, SkladLokace.CZMST_SkladLokace_LokaceVariantySortimentRow>();
                    dt = y.GetDTFromBO(bo);

                    foreach (var item in dt)
                    {
                        ds.CZMST_SkladLokace_LokaceVariantySortiment.ImportRow(item);
                    }

                    ds.CZMST_SkladLokace_LokaceVariantySortiment.AcceptChanges();

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
