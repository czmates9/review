using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Fask.DataSets;
using Fask.Server.Interfaces.Extension;
using Fask.Extension;
using Fask.RestSharp.API;
using System.Data;
using Fask.Server.Interfaces.DataSets;
using Fask.WEBAPI;

namespace Fask.MST_W_Server.Controllers
{
    public class VyrobaController : ApiController
    {
		Fask.Interfaces.IMES provider = null;

		private void Init()
		{
			try
			{
				Konfigurace.Classes.Globals_Konfig_WebConfig.LoadConfiguration();
				string providerAssemblyPath = Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Providers[0].Provider_Vyroba;
				string providerAssemblyPathGlobal = Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Providers[0].Provider;
				if (String.IsNullOrEmpty(providerAssemblyPath))
					providerAssemblyPath = providerAssemblyPathGlobal;

				if (!String.IsNullOrEmpty(providerAssemblyPath))
				{
					if (provider == null) //inicializace se provede pouze pokud nebyla provedena ... 
					{
						//System.Web.Services.WebService ws = new System.Web.Services.WebService();

						//Assembly providerAssemlby = Assembly.LoadFrom(ws.Server.MapPath(providerAssemblyPath));
						string s = @"~/" + providerAssemblyPath;
						var mappedPath = System.Web.Hosting.HostingEnvironment.MapPath(s);
						Assembly providerAssemlby = Assembly.LoadFrom(mappedPath);

						Type[] types = providerAssemlby.GetTypes();
						foreach (Type t in types)
						{
							try
							{
								if (typeof(Fask.Interfaces.IMES).IsAssignableFrom(t))
								{
									provider = (Fask.Interfaces.IMES)providerAssemlby.CreateInstance(t.FullName);
									if (provider != null)
										break;
								}
							}
							catch (Exception ex)
							{
								Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
							}
						}
					}
				}
			}

			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
			}
		}

        #region Ciselniky

        /// <summary>
        /// Poslani spravy z clienta že vše proběhlo v pořádku
        /// </summary>
        /// <param name="filtr"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Konzola_GetFiltrovaneZbozi")]
        public HttpResponseMessage Konzola_GetFiltrovaneZbozi([FromBody] Fask.Interfaces.Filtry.ZboziListFiltr filtr)
        {
            Init();
            Fask.Interfaces.DataSets.Zbozi ds = new Fask.Interfaces.DataSets.Zbozi();
            Fask.WEBAPI.API_BusinessObjects.FASK_ZASOBY_ALL listObjektu_FZ = new Fask.WEBAPI.API_BusinessObjects.FASK_ZASOBY_ALL();

            if (provider != null && provider is Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_GetFiltrovaneZbozi)
            {
                try
                {

                    ds = ((Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_GetFiltrovaneZbozi)provider).GetFiltrovaneZbozi(filtr);

                    //logika DS na BO
                    // ds.copytoBO
                    listObjektu_FZ = ds.FASK_ZASOBY_ALL_KONZOLA.DataSetToBO();

                    //navrat BO
                    return Request.CreateResponse<Fask.WEBAPI.API_BusinessObjects.FASK_ZASOBY_ALL>(HttpStatusCode.OK, listObjektu_FZ);

                }
                catch (Exception ex)
                {
                    return Request.CreateResponse<Exception>(HttpStatusCode.InternalServerError, ex);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        /// <summary>
        /// Vraci seznamFask.Interfaces.DataSets.Zbozi.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Konzola_GetZbozi")]
        public HttpResponseMessage Konzola_GetZbozi()
        {
            Init();
            Fask.Interfaces.DataSets.Zbozi ds = new Fask.Interfaces.DataSets.Zbozi();
            Fask.WEBAPI.API_BusinessObjects.FASK_ZASOBY_ALL listObjektu_FZ = new Fask.WEBAPI.API_BusinessObjects.FASK_ZASOBY_ALL();

            if (provider != null && provider is Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_GetZbozi)
            {
                try
                {

                    ds = ((Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_GetZbozi)provider).GetZbozi();

                    //logika DS na BO
                    // ds.copytoBO
                    listObjektu_FZ = ds.FASK_ZASOBY_ALL_KONZOLA.DataSetToBO();

                    //navrat BO
                    return Request.CreateResponse<Fask.WEBAPI.API_BusinessObjects.FASK_ZASOBY_ALL>(HttpStatusCode.OK, listObjektu_FZ);

                }
                catch (Exception ex)
                {
                    return Request.CreateResponse<Exception>(HttpStatusCode.InternalServerError, ex);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        /// <summary>
        /// SmazeFask.Interfaces.DataSets.Zbozi.
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/Konzola_DeleteZbozi")]
        public HttpResponseMessage Konzola_DeleteZbozi()
        {
            Init();
            bool stav = false;

            if (provider != null && provider is Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_DeleteZbozi)
            {
                try
                {
                    #region parametry z URL

                    int id_ZBOZI;
                    int? ID_Params;
                    // Zdrojove URL, resp. parametry z něho
                    string URL_param = Request.RequestUri.Query;

                    //list parametru
                    var SeznamParametru = HttpUtility.ParseQueryString(URL_param);

                    var id_ZBOZItmp = SeznamParametru.Get("id_ZBOZI");
                    id_ZBOZI = int.Parse(id_ZBOZItmp);

                    var ID_Paramstmp = SeznamParametru.Get("ID_Params");
                    ID_Params = ID_Paramstmp == null ? null : (int?)decimal.Parse(ID_Paramstmp);
                    #endregion


                    stav = ((Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_DeleteZbozi)provider).DeleteZbozi(id_ZBOZI, ID_Params);

                    return Request.CreateResponse<bool>(HttpStatusCode.OK, stav);

                }
                catch (Exception ex)
                {
                    return Request.CreateResponse<Exception>(HttpStatusCode.InternalServerError, ex);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        /// <summary>
        /// Metoda která vratí jeden řadek zboži z tabulky FASK_ZBOZI left join FASK_ZBOZI_PARAMETRY
        /// </summary>
        /// <param name="id">ID Zboží</param>
        /// <returns>DataRow jeden řadek zboží</returns>
        [HttpGet]
        [Route("api/Konzola_GetZboziByID/{id}")]
        public HttpResponseMessage Konzola_GetZboziByID(string id)
        {
            Init();
            Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_ALL_KONZOLARow ds_row = null;
            Fask.Interfaces.DataSets.Zbozi ds = new Fask.Interfaces.DataSets.Zbozi();

            Fask.WEBAPI.API_BusinessObjects.FASK_ZASOBY_ALL listObjektu_FZ = new Fask.WEBAPI.API_BusinessObjects.FASK_ZASOBY_ALL();

            if (provider != null && provider is Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_GetZboziByID)
            {
                try
                {

                    ds_row = ((Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_GetZboziByID)provider).GetZboziByID(id);

                    if (ds_row != null)
                        ds.FASK_ZASOBY_ALL_KONZOLA.AddFASK_ZASOBY_ALL_KONZOLARow(ds_row);
                    //logika DS na BO
                    // ds.copytoBO
                    listObjektu_FZ = ds.FASK_ZASOBY_ALL_KONZOLA.DataSetToBO();

                    //navrat BO
                    return Request.CreateResponse<Fask.WEBAPI.API_BusinessObjects.FASK_ZASOBY_ALL>(HttpStatusCode.OK, listObjektu_FZ);

                }
                catch (Exception ex)
                {
                    return Request.CreateResponse<Exception>(HttpStatusCode.InternalServerError, ex);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        /// <summary>
        /// Metoda pro Insert zboží do FASK_ZASOBY
        /// </summary>
        /// <param name="zboziRow">řadek co se vloží</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Konzola_InsertZbozi")]
        public HttpResponseMessage Konzola_InsertZbozi([FromBody] Fask.WEBAPI.API_BusinessObjects.FASK_ZASOBY FZ_data)
        {
            Init();
            bool stav = false;
            Fask.Interfaces.DataSets.Zbozi ds = new Fask.Interfaces.DataSets.Zbozi();

            if (provider != null && provider is Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_InsertZbozi)
            {
                try
                {
                    //ds = FZ_data.BOToDataSet();
                    foreach (var item in FZ_data.BOToDataSet())
                    {
                        ds.FASK_ZASOBY_KONZOLA.ImportRow(item);
                    }

                    stav = ((Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_InsertZbozi)provider).InsertZbozi(ds.FASK_ZASOBY_KONZOLA.First());

                    return Request.CreateResponse<bool>(HttpStatusCode.OK, stav);

                }
                catch (Exception ex)
                {
                    return Request.CreateResponse<Exception>(HttpStatusCode.InternalServerError, ex);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        /// <summary>
        /// Metoda pro update pouze FASK_ZBOZI
        /// </summary>
        /// <param name="zboziRow">Radek pro Update</param>
        /// <returns>True-OK, False- chyba</returns>
        [HttpPost]
        [Route("api/Konzola_UpdateZbozi")]
        public HttpResponseMessage Konzola_UpdateZbozi([FromBody] Fask.WEBAPI.API_BusinessObjects.FASK_ZASOBY_ALL FZ_data)
        {
            Init();
            bool stav = false;
           Fask.Interfaces.DataSets.Zbozi ds = new Fask.Interfaces.DataSets.Zbozi();

            if (provider != null && provider is Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_UpdateZbozi)
            {
                try
                {
                    //ds = FZ_data.BOToDataSet();
                    foreach (var item in FZ_data.BOToDataSet())
                    {
                        ds.FASK_ZASOBY_ALL_KONZOLA.ImportRow(item);
                    }

                    stav = ((Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_UpdateZbozi)provider).UpdateZbozi(ds.FASK_ZASOBY_ALL_KONZOLA.First());

                    return Request.CreateResponse<bool>(HttpStatusCode.OK, stav);

                }
                catch (Exception ex)
                {
                    return Request.CreateResponse<Exception>(HttpStatusCode.InternalServerError, ex);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        /// <summary>
        /// Metoda pro inser do FASK_ZASOBY_PARAMETRY
        /// </summary>
        /// <param name="zboziRow"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Konzola_InsertZboziParams")]
        public HttpResponseMessage Konzola_InsertZboziParams([FromBody] Fask.WEBAPI.API_BusinessObjects.FASK_ZASOBY_PARAMETRY FZ_data)
        {
            Init();
            bool stav = false;
           Fask.Interfaces.DataSets.Zbozi ds = new Fask.Interfaces.DataSets.Zbozi();

            if (provider != null && provider is Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_InsertZboziParams)
            {
                try
                {
                    //ds = FZ_data.BOToDataSet();
                    foreach (var item in FZ_data.BOToDataSet())
                    {
                        ds.FASK_ZASOBY_PARAMETRY_KONZOLA.ImportRow(item);
                    }

                    stav = ((Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_InsertZboziParams)provider).InsertZboziParams(ds.FASK_ZASOBY_PARAMETRY_KONZOLA.First());

                    return Request.CreateResponse<bool>(HttpStatusCode.OK, stav);

                }
                catch (Exception ex)
                {
                    return Request.CreateResponse<Exception>(HttpStatusCode.InternalServerError, ex);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        /// <summary>
        /// Metoda pro Inser/Update Parametru...
        /// </summary>
        /// <param name="zboziRow"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Konzola_UpdateZboziParams")]
        public HttpResponseMessage Konzola_UpdateZboziParams([FromBody] Fask.WEBAPI.API_BusinessObjects.FASK_ZASOBY_ALL FZ_data)
        {
            Init();
            bool stav = false;
           Fask.Interfaces.DataSets.Zbozi ds = new Fask.Interfaces.DataSets.Zbozi();

            if (provider != null && provider is Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_UpdateZboziParams)
            {
                try
                {
                    //ds = FZ_data.BOToDataSet();
                    foreach (var item in FZ_data.BOToDataSet())
                    {
                        ds.FASK_ZASOBY_ALL_KONZOLA.ImportRow(item);
                    }

                    stav = ((Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_UpdateZboziParams)provider).UpdateZboziParams(ds.FASK_ZASOBY_ALL_KONZOLA.First());

                    return Request.CreateResponse<bool>(HttpStatusCode.OK, stav);

                }
                catch (Exception ex)
                {
                    return Request.CreateResponse<Exception>(HttpStatusCode.InternalServerError, ex);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }


        [HttpPost]
        [Route("api/Konzola_UpdateParametry")]
        public HttpResponseMessage Konzola_UpdateParametry([FromBody] Fask.WEBAPI.API_BusinessObjects.FASK_ZASOBY_ALL FZ_data)
        {
            Init();
            bool stav = false;
           Fask.Interfaces.DataSets.Zbozi ds = new Fask.Interfaces.DataSets.Zbozi();

            if (provider != null && provider is Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_UpdateParametry)
            {
                try
                {
                    //ds = FZ_data.BOToDataSet();
                    foreach (var item in FZ_data.BOToDataSet())
                    {
                        ds.FASK_ZASOBY_ALL_KONZOLA.ImportRow(item);
                    }

                    stav = ((Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_UpdateParametry)provider).UpdateParametry(ds.FASK_ZASOBY_ALL_KONZOLA.First());

                    return Request.CreateResponse<bool>(HttpStatusCode.OK, stav);

                }
                catch (Exception ex)
                {
                    return Request.CreateResponse<Exception>(HttpStatusCode.InternalServerError, ex);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        [Route("api/Konzola_GetParametrybyID/{ITEMNMBR}")]
        public HttpResponseMessage Konzola_GetParametrybyID(string ITEMNMBR)
        {
            Init();
            bool stav = false;

            if (provider != null && provider is Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_GetParametrybyID)
            {
                try
                {

                    stav = ((Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_GetParametrybyID)provider).GetParametrybyID(ITEMNMBR);

                    return Request.CreateResponse<bool>(HttpStatusCode.OK, stav);

                }
                catch (Exception ex)
                {
                    return Request.CreateResponse<Exception>(HttpStatusCode.InternalServerError, ex);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        [Route("api/Konzola_ImportZbozi")]
        public HttpResponseMessage Konzola_ImportZbozi()
        {
            Init();
            string stav = string.Empty;

            if (provider != null && provider is Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_ImportZbozi)
            {
                try
                {

                    stav = ((Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_ImportZbozi)provider).ImportZbozi();

                    return Request.CreateResponse<string>(HttpStatusCode.OK, stav);

                }
                catch (Exception ex)
                {
                    return Request.CreateResponse<Exception>(HttpStatusCode.InternalServerError, ex);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        [Route("api/Konzola_ExportKatalogZasoby_Procedura")]
        public HttpResponseMessage Konzola_ExportKatalogZasoby_Procedura()
        {
            Init();
            string stav = string.Empty;

            if (provider != null && provider is Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_ExportKatalogZasoby_Procedura)
            {
                try
                {

                    stav = ((Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_ExportKatalogZasoby_Procedura)provider).ExportKatalogZasoby_Procedura();

                    return Request.CreateResponse<string>(HttpStatusCode.OK, stav);

                }
                catch (Exception ex)
                {
                    return Request.CreateResponse<Exception>(HttpStatusCode.InternalServerError, ex);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        [Route("api/Konzola_GetSklady")]
        public HttpResponseMessage Konzola_GetSklady()
        {
            Init();
            string stav = string.Empty;
            Fask.Interfaces.DataSets.Sklady ds = new Fask.Interfaces.DataSets.Sklady();
            Fask.WEBAPI.API_BusinessObjects.BO_CZMST093 FZ_data = new Fask.WEBAPI.API_BusinessObjects.BO_CZMST093();


            if (provider != null && provider is Fask.Interfaces.Ciselniky.Sklady.ISklady2_GetSklady)
            {
                try
                {

                    ds = ((Fask.Interfaces.Ciselniky.Sklady.ISklady2_GetSklady)provider).GetSklady();
                    FZ_data = ds.DataSetToBO();

                    return Request.CreateResponse<Fask.WEBAPI.API_BusinessObjects.BO_CZMST093>(HttpStatusCode.OK, FZ_data);

                }
                catch (Exception ex)
                {
                    return Request.CreateResponse<Exception>(HttpStatusCode.InternalServerError, ex);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        [Route("api/Konzola_Sklady_Vyroba_Fill")]
        public HttpResponseMessage Konzola_Sklady_Vyroba_Fill()
        {
            Init();
            try
            {
                Fask.WEBAPI.API_BusinessObjects.BO_CZMST093 bo = new Fask.WEBAPI.API_BusinessObjects.BO_CZMST093();
                Fask.Interfaces.DataSets.Vyroba.CZMST093DataTable dt = new Fask.Interfaces.DataSets.Vyroba.CZMST093DataTable();
                Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();

                if (provider != null && provider is Fask.Interfaces.Ciselniky.Sklady.ISklady2_Vyroba_Fill)
                {
                    try
                    {
                        ((Fask.Interfaces.Ciselniky.Sklady.ISklady2_Vyroba_Fill)provider).Sklady_Vyroba_Fill(ds);

                        BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZMST093, Fask.WEBAPI.API_BusinessObjects.BO_CZMST093_row, Fask.Interfaces.DataSets.Vyroba.CZMST093DataTable, Fask.Interfaces.DataSets.Vyroba.CZMST093Row> x = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZMST093, Fask.WEBAPI.API_BusinessObjects.BO_CZMST093_row, Fask.Interfaces.DataSets.Vyroba.CZMST093DataTable, Fask.Interfaces.DataSets.Vyroba.CZMST093Row>();

                        bo = x.GetBOFromDT(ds.CZMST093);


                        return Request.CreateResponse<Fask.WEBAPI.API_BusinessObjects.BO_CZMST093>(HttpStatusCode.OK, bo);
                    }
                    catch (Exception ex)
                    {
                        //return Request.CreateResponse<string>(HttpStatusCode.BadRequest, ex.Message + ex.StackTrace);
                        return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
                    }

                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
            }

        }

        [HttpGet]
        [Route("api/Konzola_Lokace_Fill")]
        public HttpResponseMessage Konzola_Lokace_Fill()
        {
            Init();
            try
            {
                Fask.WEBAPI.API_BusinessObjects.BO_CZMST094 bo = new Fask.WEBAPI.API_BusinessObjects.BO_CZMST094();
                Fask.Interfaces.DataSets.Vyroba.CZMST094DataTable dt = new Fask.Interfaces.DataSets.Vyroba.CZMST094DataTable();
                Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();

                if (provider != null && provider is Fask.Interfaces.Ciselniky.Lokace.ILokace2_Fill)
                {
                    try
                    {
                        ((Fask.Interfaces.Ciselniky.Lokace.ILokace2_Fill)provider).Fill(ds);

                        BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZMST094, Fask.WEBAPI.API_BusinessObjects.BO_CZMST094_row, Fask.Interfaces.DataSets.Vyroba.CZMST094DataTable, Fask.Interfaces.DataSets.Vyroba.CZMST094Row> x = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZMST094, Fask.WEBAPI.API_BusinessObjects.BO_CZMST094_row, Fask.Interfaces.DataSets.Vyroba.CZMST094DataTable, Fask.Interfaces.DataSets.Vyroba.CZMST094Row>();

                        bo = x.GetBOFromDT(ds.CZMST094);


                        return Request.CreateResponse<Fask.WEBAPI.API_BusinessObjects.BO_CZMST094>(HttpStatusCode.OK, bo);
                    }
                    catch (Exception ex)
                    {
                        //return Request.CreateResponse<string>(HttpStatusCode.BadRequest, ex.Message + ex.StackTrace);
                        return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
                    }

                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
            }

        }

        /// <summary>
        /// SmazeFask.Interfaces.DataSets.Zbozi.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Konzola_TEST")]
        public HttpResponseMessage Konzola_TEST()
        {
            #region ukazka kodu na klient
            int neco = 0;
            HttpValueCollection x = new HttpValueCollection();
            x.Add("A", "daaaaaasds");
            x.Add("B", neco.ToString());
            x.Add("C", "dsggggds");
            x.Add("D", "dshjhjjjjds");
            x.Add("E", "dsdrrrrrrs");
            var xxx = x.ToString();
            #endregion

            string ID = null;
            int? cislo = null;
            decimal? QTY = null;
            string nazev = null;

            // Zdrojove URL, resp. parametry z něho
            string URL_param = Request.RequestUri.Query;

            //list parametru
            var SeznamParametru = HttpUtility.ParseQueryString(URL_param);

            ID = SeznamParametru.Get("ID");
            nazev = SeznamParametru.Get("nazev");

            var cislotmp = SeznamParametru.Get("cislo");
            cislo = cislotmp == null ? null : (int?)int.Parse(cislotmp);

            var QTYtmp = SeznamParametru.Get("QTY");
            QTY = QTYtmp == null ? null : (decimal?)decimal.Parse(cislotmp);



            return Request.CreateResponse<bool>(HttpStatusCode.OK, true);


        }
        #endregion

        #region Rozbory

        #region prehled odvadeni stroju

        
        [HttpPost]
        [Route("api/Konzola_GetFiltrovanyOdvodEvents")]
        public HttpResponseMessage Konzola_GetFiltrovanyOdvodEvents([FromBody] Fask.Interfaces.Filtry.Odvod_EventsListFiltr filtr)
        {
            Init();
            Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();
            Fask.WEBAPI.API_BusinessObjects.FASK_Events listObjektu_FZ = new Fask.WEBAPI.API_BusinessObjects.FASK_Events();

            if (provider != null && provider is Fask.Interfaces.Vyroba.Odvod_Events.IOdvod_Events_GetFiltrovanyOdvodEvents)
            {
                try
                {

                    ds = ((Fask.Interfaces.Vyroba.Odvod_Events.IOdvod_Events_GetFiltrovanyOdvodEvents)provider).GetFiltrovanyOdvodEvents(filtr);

                    //logika DS na BO
                    // ds.copytoBO
                    listObjektu_FZ = ds.FASK_Events.DataSetToBO();

                    //navrat BO
                    return Request.CreateResponse<Fask.WEBAPI.API_BusinessObjects.FASK_Events>(HttpStatusCode.OK, listObjektu_FZ);

                }
                catch (Exception ex)
                {
                    return Request.CreateResponse<Exception>(HttpStatusCode.InternalServerError, ex);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        [Route("api/Konzola_CallProcedura")]
        public HttpResponseMessage CallProcedura()
        {
            Init();


            try
            {
                #region parametry z URL
                DateTime? OD = null;
                DateTime? DO = null;
                string Material = null;
                int? CountEntries = null;
                Fask.WEBAPI.API_BusinessObjects.BO_CallProcedura bo = new Fask.WEBAPI.API_BusinessObjects.BO_CallProcedura();


                // Zdrojove URL, resp. parametry z něho
                string URL_param = Request.RequestUri.Query;

                //list parametru
                var SeznamParametru = HttpUtility.ParseQueryString(URL_param);

                var ODtmp = SeznamParametru.Get("OD");
                OD = DateTime.Parse(ODtmp, System.Globalization.DateTimeFormatInfo.InvariantInfo);

                var DOtmp = SeznamParametru.Get("DO");
                DO = DateTime.Parse(DOtmp, System.Globalization.DateTimeFormatInfo.InvariantInfo);

                var Materialtmp = SeznamParametru.Get("Material");
                Material = Materialtmp.ToString();
                #endregion

                if (provider != null && provider is Fask.Interfaces.Vyroba.Odvod_Events.IOdvod_Events_CallProcedura)
                {
                    try
                    {
                        bo.result = ((Fask.Interfaces.Vyroba.Odvod_Events.IOdvod_Events_CallProcedura)provider).CallProcedura(OD, DO, Material, out CountEntries);
                        bo.CountEntries = CountEntries;
                        return Request.CreateResponse<Fask.WEBAPI.API_BusinessObjects.BO_CallProcedura>(HttpStatusCode.OK, bo);
                    }
                    catch (Exception ex)
                    {
                        //return Request.CreateResponse<string>(HttpStatusCode.BadRequest, ex.Message + ex.StackTrace);
                        return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
                    }

                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
            }

        }

        [HttpGet]
        [Route("api/Konzola_GetMaterials")]
        public HttpResponseMessage GetMaterials()
        {
            Init();
            try
            {
                DataTable dt = new DataTable();
                Fask.WEBAPI.API_BusinessObjects.BO_Material FZ_data = new Fask.WEBAPI.API_BusinessObjects.BO_Material();
                List<string> list = new List<string>();


                if (provider != null && provider is Fask.Interfaces.Vyroba.Odvod_Events.IOdvod_Events_GetMaterials)
                {
                    try
                    {
                        dt = ((Fask.Interfaces.Vyroba.Odvod_Events.IOdvod_Events_GetMaterials)provider).GetMaterials();

                        if ((dt != null) && (dt.Rows.Count > 0))
                        {
                            foreach (DataRow item in dt.Rows)
                            {
                                list.Add(item["material"].ToString());
                            }
                            FZ_data.material = list.ToArray();
                        }
                        return Request.CreateResponse<Fask.WEBAPI.API_BusinessObjects.BO_Material>(HttpStatusCode.OK, FZ_data);
                    }
                    catch (Exception ex)
                    {
                        //return Request.CreateResponse<string>(HttpStatusCode.BadRequest, ex.Message + ex.StackTrace);
                        return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
                    }

                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
            }

        }

        [HttpPost]
        [Route("api/Konzola_StornoEvent")]
        public HttpResponseMessage Konzola_StornoEvent([FromBody] Fask.WEBAPI.API_BusinessObjects.FASK_Events FZ_data)
        {
            Init();
            Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();
            Fask.WEBAPI.API_BusinessObjects.FASK_Events listObjektu_FZ = new Fask.WEBAPI.API_BusinessObjects.FASK_Events();
            bool result = false;

            if (provider != null && provider is Fask.Interfaces.Vyroba.Odvod_Events.IOdvod_Events_StornoEvent)
            {
                try
                {
                    foreach (var item in FZ_data.BOToDataSet())
                    {
                        ds.FASK_Events.ImportRow(item);
                    }

                    if(ds.FASK_Events.Count == 1)
                    {
                        result = ((Fask.Interfaces.Vyroba.Odvod_Events.IOdvod_Events_StornoEvent)provider).StornoEvent(ds.FASK_Events.First());
                        return Request.CreateResponse<bool>(HttpStatusCode.OK, result);
                    }
                    else
                    {
                        return Request.CreateResponse<bool>(HttpStatusCode.NotFound, false);
                    }

                }
                catch (Exception ex)
                {
                    return Request.CreateResponse<Exception>(HttpStatusCode.InternalServerError, ex);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        [Route("api/Konzola_StornoEvent_OnlineCheck")]
        public HttpResponseMessage Konzola_StornoEvent_OnlineCheck([FromBody] Guid G)
        {
            Init();
            int? result = null;

            if (provider != null && provider is Fask.Interfaces.Vyroba.Odvod_Events.IOdvod_Events_StornoEvent_OnlineCheck)
            {
                try
                {
                        result = ((Fask.Interfaces.Vyroba.Odvod_Events.IOdvod_Events_StornoEvent_OnlineCheck)provider).StornoEvent_OnlineCheck(G);
                        return Request.CreateResponse<int?>(HttpStatusCode.OK, result);
                  

                }
                catch (Exception ex)
                {
                    return Request.CreateResponse<Exception>(HttpStatusCode.InternalServerError, ex);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        #endregion

        #region prehled odvadeni stroju chyby


        [HttpPost]
        [Route("api/Konzola_EventsErr_GetFiltrovanyOdvodEvents")]
        public HttpResponseMessage Konzola_EventsErr_GetFiltrovanyOdvodEvents([FromBody] Fask.Interfaces.Filtry.Odvod_EventsErrListFiltr filtr)
        {
            Fask.WEBAPI.API_BusinessObjects.BO_FASK_EventsErr bo = new Fask.WEBAPI.API_BusinessObjects.BO_FASK_EventsErr();
            Fask.Interfaces.DataSets.Vyroba.FASK_EventsErrDataTable dt = new Fask.Interfaces.DataSets.Vyroba.FASK_EventsErrDataTable();
            Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();

            Init();

            if (provider != null && provider is Fask.Interfaces.Vyroba.Odvod_EventsErr.IOdvod_EventsErr_GetFiltrovanyOdvodEvents)
            {
                try
                {
                    ds = ((Fask.Interfaces.Vyroba.Odvod_EventsErr.IOdvod_EventsErr_GetFiltrovanyOdvodEvents)provider).EventsErr_GetFiltrovanyOdvodEvents(filtr);

                    BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_FASK_EventsErr, Fask.WEBAPI.API_BusinessObjects.BO_FASK_EventsErr_row, Fask.Interfaces.DataSets.Vyroba.FASK_EventsErrDataTable, Fask.Interfaces.DataSets.Vyroba.FASK_EventsErrRow> x = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_FASK_EventsErr, Fask.WEBAPI.API_BusinessObjects.BO_FASK_EventsErr_row, Fask.Interfaces.DataSets.Vyroba.FASK_EventsErrDataTable, Fask.Interfaces.DataSets.Vyroba.FASK_EventsErrRow>();

                    bo = x.GetBOFromDT(ds.FASK_EventsErr);


                    return Request.CreateResponse<Fask.WEBAPI.API_BusinessObjects.BO_FASK_EventsErr>(HttpStatusCode.OK, bo);


                }
                catch (Exception ex)
                {
                    return Request.CreateResponse<Exception>(HttpStatusCode.InternalServerError, ex);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }






        #endregion

        #region prehled odvadeni stroju stavy


        [HttpPost]
        [Route("api/Konzola_MachineStateSet_GetFiltrovanyOdvodMachineStateSet")]
        public HttpResponseMessage Konzola_MachineStateSet_GetFiltrovanyOdvodMachineStateSet([FromBody] Fask.Interfaces.Filtry.Odvod_MachineStateSetListFiltr filtr)
        {
            Fask.WEBAPI.API_BusinessObjects.BO_MachineStateSet bo = new Fask.WEBAPI.API_BusinessObjects.BO_MachineStateSet();
            Fask.Interfaces.DataSets.Vyroba.MachineStateSetDataTable dt = new Fask.Interfaces.DataSets.Vyroba.MachineStateSetDataTable();
            Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();

            Init();

            if (provider != null && provider is Fask.Interfaces.Vyroba.Odvod_MachineStateSet.IOdvod_MachineStateSet_GetFiltrovanyMachineStateSets)
            {
                try
                {
                    ds = ((Fask.Interfaces.Vyroba.Odvod_MachineStateSet.IOdvod_MachineStateSet_GetFiltrovanyMachineStateSets)provider).MachineStateSet_GetFiltrovanyOdvodMachineStateSet(filtr);

                    BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_MachineStateSet, Fask.WEBAPI.API_BusinessObjects.BO_MachineStateSet_row, Fask.Interfaces.DataSets.Vyroba.MachineStateSetDataTable, Fask.Interfaces.DataSets.Vyroba.MachineStateSetRow> x = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_MachineStateSet, Fask.WEBAPI.API_BusinessObjects.BO_MachineStateSet_row, Fask.Interfaces.DataSets.Vyroba.MachineStateSetDataTable, Fask.Interfaces.DataSets.Vyroba.MachineStateSetRow>();

                    bo = x.GetBOFromDT(ds.MachineStateSet);


                    return Request.CreateResponse<Fask.WEBAPI.API_BusinessObjects.BO_MachineStateSet>(HttpStatusCode.OK, bo);


                }
                catch (Exception ex)
                {
                    return Request.CreateResponse<Exception>(HttpStatusCode.InternalServerError, ex);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }



        [HttpGet]
        [Route("api/Konzola_MachineStateSet_GetEnum_description")]
        public HttpResponseMessage Konzola_MachineStateSet_GetEnum_description()
        {
            List<string> descriptions = new List<string>();

            Init();

            if (provider != null && provider is Fask.Interfaces.Vyroba.Odvod_MachineStateSet.IOdvod_MachineStateSet_GetEnum_description)
            {
                try
                {
                    descriptions = ((Fask.Interfaces.Vyroba.Odvod_MachineStateSet.IOdvod_MachineStateSet_GetEnum_description)provider).LoadStrojDescriptionFromDatabase();

                  


                    return Request.CreateResponse<List<string>>(HttpStatusCode.OK, descriptions);


                }
                catch (Exception ex)
                {
                    return Request.CreateResponse<Exception>(HttpStatusCode.InternalServerError, ex);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }




        #endregion

        #region prehled vyrobky

        #region Groups
        [HttpGet]
        [Route("api/Konzola_Groups_Fill")]
        public HttpResponseMessage Konzola_Groups_Fill()
        {
            Init();
            try
            {
                Fask.WEBAPI.API_BusinessObjects.BO_Groups bo = new Fask.WEBAPI.API_BusinessObjects.BO_Groups();
                Fask.Interfaces.DataSets.Vyroba.GroupsDataTable dt = new Fask.Interfaces.DataSets.Vyroba.GroupsDataTable();
                Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();



                if (provider != null && provider is Fask.Interfaces.Vyroba.Groups.IGroups_FillGroups)
                {
                    try
                    {
                        ((Fask.Interfaces.Vyroba.Groups.IGroups_FillGroups)provider).Groups_Fill(ds);

                        BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Groups, Fask.WEBAPI.API_BusinessObjects.BO_Groups_row, Fask.Interfaces.DataSets.Vyroba.GroupsDataTable, Fask.Interfaces.DataSets.Vyroba.GroupsRow> x = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Groups, Fask.WEBAPI.API_BusinessObjects.BO_Groups_row, Fask.Interfaces.DataSets.Vyroba.GroupsDataTable, Fask.Interfaces.DataSets.Vyroba.GroupsRow>();

                        bo = x.GetBOFromDT(ds.Groups);


                        return Request.CreateResponse<Fask.WEBAPI.API_BusinessObjects.BO_Groups>(HttpStatusCode.OK, bo);
                    }
                    catch (Exception ex)
                    {
                        //return Request.CreateResponse<string>(HttpStatusCode.BadRequest, ex.Message + ex.StackTrace);
                        return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
                    }

                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
            }

        }

        [HttpGet]
        [Route("api/Konzola_Groups_GetDataByID")]
        public HttpResponseMessage Konzola_Groups_GetDataByID()
        {
            Init();
            try
            {
                Fask.WEBAPI.API_BusinessObjects.BO_Groups bo = new Fask.WEBAPI.API_BusinessObjects.BO_Groups();
                Fask.Interfaces.DataSets.Vyroba.GroupsDataTable dt = new Fask.Interfaces.DataSets.Vyroba.GroupsDataTable();

                #region parametry z URL
                string ID = null;

                // Zdrojove URL, resp. parametry z něho
                string URL_param = Request.RequestUri.Query;

                //list parametru
                var SeznamParametru = HttpUtility.ParseQueryString(URL_param);

                var IDtmp = SeznamParametru.Get("ID");
                ID = IDtmp.ToString();


                #endregion

                if (provider != null && provider is Fask.Interfaces.Vyroba.Groups.IGroups_GetDataByID)
                {
                    try
                    {
                        dt = ((Fask.Interfaces.Vyroba.Groups.IGroups_GetDataByID)provider).Groups_GetDataByID(ID);

                        BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Groups, Fask.WEBAPI.API_BusinessObjects.BO_Groups_row, Fask.Interfaces.DataSets.Vyroba.GroupsDataTable, Fask.Interfaces.DataSets.Vyroba.GroupsRow> x = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Groups, Fask.WEBAPI.API_BusinessObjects.BO_Groups_row, Fask.Interfaces.DataSets.Vyroba.GroupsDataTable, Fask.Interfaces.DataSets.Vyroba.GroupsRow>();
                        bo = x.GetBOFromDT(dt);


                        return Request.CreateResponse<Fask.WEBAPI.API_BusinessObjects.BO_Groups>(HttpStatusCode.OK, bo);
                    }
                    catch (Exception ex)
                    {
                        //return Request.CreateResponse<string>(HttpStatusCode.BadRequest, ex.Message + ex.StackTrace);
                        return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
                    }

                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
            }

        } 
        #endregion

        #region Operations

        [HttpGet]
        [Route("api/Konzola_Operations_Fill")]
        public HttpResponseMessage Konzola_Operations_Fill()
        {
            Init();
            try
            {
                Fask.WEBAPI.API_BusinessObjects.BO_Operations bo = new Fask.WEBAPI.API_BusinessObjects.BO_Operations();
                Fask.Interfaces.DataSets.Vyroba.OperationsDataTable dt = new Fask.Interfaces.DataSets.Vyroba.OperationsDataTable();
                Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();



                if (provider != null && provider is Fask.Interfaces.Vyroba.Operations.IOperations_Fill)
                {
                    try
                    {
                        ((Fask.Interfaces.Vyroba.Operations.IOperations_Fill)provider).Operations_Fill(ds);

                        BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Operations, Fask.WEBAPI.API_BusinessObjects.BO_Operations_row, Fask.Interfaces.DataSets.Vyroba.OperationsDataTable, Fask.Interfaces.DataSets.Vyroba.OperationsRow> x = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Operations, Fask.WEBAPI.API_BusinessObjects.BO_Operations_row, Fask.Interfaces.DataSets.Vyroba.OperationsDataTable, Fask.Interfaces.DataSets.Vyroba.OperationsRow>();

                        bo = x.GetBOFromDT(ds.Operations);


                        return Request.CreateResponse<Fask.WEBAPI.API_BusinessObjects.BO_Operations>(HttpStatusCode.OK, bo);
                    }
                    catch (Exception ex)
                    {
                        //return Request.CreateResponse<string>(HttpStatusCode.BadRequest, ex.Message + ex.StackTrace);
                        return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
                    }

                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
            }

        }

        [HttpGet]
        [Route("api/Konzola_Operations_GetDataByID")]
        public HttpResponseMessage Konzola_Operations_GetDataByID()
        {
            Init();
            try
            {
                Fask.WEBAPI.API_BusinessObjects.BO_Operations bo = new Fask.WEBAPI.API_BusinessObjects.BO_Operations();
                Fask.Interfaces.DataSets.Vyroba.OperationsDataTable dt = new Fask.Interfaces.DataSets.Vyroba.OperationsDataTable();

                #region parametry z URL
                string ID = null;

                // Zdrojove URL, resp. parametry z něho
                string URL_param = Request.RequestUri.Query;

                //list parametru
                var SeznamParametru = HttpUtility.ParseQueryString(URL_param);

                var IDtmp = SeznamParametru.Get("ID");
                ID = IDtmp.ToString();


                #endregion

                if (provider != null && provider is Fask.Interfaces.Vyroba.Operations.IOperations_GetDataByID)
                {
                    try
                    {
                        dt = ((Fask.Interfaces.Vyroba.Operations.IOperations_GetDataByID)provider).Operations_GetDataByID(ID);

                        BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Operations, Fask.WEBAPI.API_BusinessObjects.BO_Operations_row, Fask.Interfaces.DataSets.Vyroba.OperationsDataTable, Fask.Interfaces.DataSets.Vyroba.OperationsRow> x = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Operations, Fask.WEBAPI.API_BusinessObjects.BO_Operations_row, Fask.Interfaces.DataSets.Vyroba.OperationsDataTable, Fask.Interfaces.DataSets.Vyroba.OperationsRow>();
                        bo = x.GetBOFromDT(dt);


                        return Request.CreateResponse<Fask.WEBAPI.API_BusinessObjects.BO_Operations>(HttpStatusCode.OK, bo);
                    }
                    catch (Exception ex)
                    {
                        //return Request.CreateResponse<string>(HttpStatusCode.BadRequest, ex.Message + ex.StackTrace);
                        return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
                    }

                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
            }

        } 
        #endregion

        #region Machines

        [HttpGet]
        [Route("api/Konzola_Machines_Fill")]
        public HttpResponseMessage Konzola_Machines_Fill()
        {
            Init();
            try
            {
                Fask.WEBAPI.API_BusinessObjects.BO_Machines bo = new Fask.WEBAPI.API_BusinessObjects.BO_Machines();
                Fask.Interfaces.DataSets.Vyroba.MachinesDataTable dt = new Fask.Interfaces.DataSets.Vyroba.MachinesDataTable();
                Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();



                if (provider != null && provider is Fask.Interfaces.Vyroba.Machines.IMachines_Fill)
                {
                    try
                    {
                        ((Fask.Interfaces.Vyroba.Machines.IMachines_Fill)provider).Machines_Fill(ds);

                        BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Machines, Fask.WEBAPI.API_BusinessObjects.BO_Machines_row, Fask.Interfaces.DataSets.Vyroba.MachinesDataTable, Fask.Interfaces.DataSets.Vyroba.MachinesRow> x = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Machines, Fask.WEBAPI.API_BusinessObjects.BO_Machines_row, Fask.Interfaces.DataSets.Vyroba.MachinesDataTable, Fask.Interfaces.DataSets.Vyroba.MachinesRow>();

                        bo = x.GetBOFromDT(ds.Machines);


                        return Request.CreateResponse<Fask.WEBAPI.API_BusinessObjects.BO_Machines>(HttpStatusCode.OK, bo);
                    }
                    catch (Exception ex)
                    {
                        //return Request.CreateResponse<string>(HttpStatusCode.BadRequest, ex.Message + ex.StackTrace);
                        return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
                    }

                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
            }

        }

        [HttpGet]
        [Route("api/Konzola_Machines_GetDataByID")]
        public HttpResponseMessage Konzola_Machines_GetDataByID()
        {
            Init();
            try
            {
                Fask.WEBAPI.API_BusinessObjects.BO_Machines bo = new Fask.WEBAPI.API_BusinessObjects.BO_Machines();
                Fask.Interfaces.DataSets.Vyroba.MachinesDataTable dt = new Fask.Interfaces.DataSets.Vyroba.MachinesDataTable();

                #region parametry z URL
                string ID = null;

                // Zdrojove URL, resp. parametry z něho
                string URL_param = Request.RequestUri.Query;

                //list parametru
                var SeznamParametru = HttpUtility.ParseQueryString(URL_param);

                var IDtmp = SeznamParametru.Get("ID");
                ID = IDtmp.ToString();


                #endregion

                if (provider != null && provider is Fask.Interfaces.Vyroba.Machines.IMachines_GetDataByID)
                {
                    try
                    {
                        dt = ((Fask.Interfaces.Vyroba.Machines.IMachines_GetDataByID)provider).Machines_GetDataByID(ID);

                        BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Machines, Fask.WEBAPI.API_BusinessObjects.BO_Machines_row, Fask.Interfaces.DataSets.Vyroba.MachinesDataTable, Fask.Interfaces.DataSets.Vyroba.MachinesRow> x = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Machines, Fask.WEBAPI.API_BusinessObjects.BO_Machines_row, Fask.Interfaces.DataSets.Vyroba.MachinesDataTable, Fask.Interfaces.DataSets.Vyroba.MachinesRow>();
                        bo = x.GetBOFromDT(dt);


                        return Request.CreateResponse<Fask.WEBAPI.API_BusinessObjects.BO_Machines>(HttpStatusCode.OK, bo);
                    }
                    catch (Exception ex)
                    {
                        //return Request.CreateResponse<string>(HttpStatusCode.BadRequest, ex.Message + ex.StackTrace);
                        return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
                    }

                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
            }

        }
        #endregion

        #region Production

        [HttpPost]
        [Route("api/Konzola_Production_GetFiltrovanyProductionList")]
        public HttpResponseMessage Konzola_Production_GetFiltrovanyProductionList([FromBody] Fask.Interfaces.Filtry.ProductionListFiltr filtr)
        {
            Init();
            try
            {
                Fask.WEBAPI.API_BusinessObjects.BO_Production bo = new Fask.WEBAPI.API_BusinessObjects.BO_Production();
                Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable dt = new Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable();
                Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();



                if (provider != null && provider is Fask.Interfaces.Vyroba.Production.IProduction_GetFiltrovanyProductionList)
                {
                    try
                    {
                     ds = ((Fask.Interfaces.Vyroba.Production.IProduction_GetFiltrovanyProductionList)provider).Production_GetFiltrovanyProductionList(filtr);

                        BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Production, Fask.WEBAPI.API_BusinessObjects.BO_Production_row, Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable, Fask.Interfaces.DataSets.Vyroba.Production_KonzolaRow> x = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Production, Fask.WEBAPI.API_BusinessObjects.BO_Production_row, Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable, Fask.Interfaces.DataSets.Vyroba.Production_KonzolaRow>();

                        bo = x.GetBOFromDT(ds.Production_Konzola);


                        return Request.CreateResponse<Fask.WEBAPI.API_BusinessObjects.BO_Production>(HttpStatusCode.OK, bo);
                    }
                    catch (Exception ex)
                    {
                        //return Request.CreateResponse<string>(HttpStatusCode.BadRequest, ex.Message + ex.StackTrace);
                        return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
                    }

                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
            }

        }

        [HttpGet]
        [Route("api/Konzola_Production_FillByCORRGUID")]
        public HttpResponseMessage Konzola_Production_FillByCORRGUID()
        {
            Init();
            try
            {
                Fask.WEBAPI.API_BusinessObjects.BO_Production bo = new Fask.WEBAPI.API_BusinessObjects.BO_Production();
                Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable dt = new Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable();
                Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();

                #region parametry z URL
                Guid CORRGUID;

                // Zdrojove URL, resp. parametry z něho
                string URL_param = Request.RequestUri.Query;

                //list parametru
                var SeznamParametru = HttpUtility.ParseQueryString(URL_param);

                var CORRGUIDtmp = SeznamParametru.Get("CORRGUID");
                CORRGUID = Guid.Parse(CORRGUIDtmp);


                #endregion


                if (provider != null && provider is Fask.Interfaces.Vyroba.Production.IProduction_FillByCORRGUID)
                {
                    try
                    {
                        ((Fask.Interfaces.Vyroba.Production.IProduction_FillByCORRGUID)provider).Production_FillByCORRGUID(ds, CORRGUID);

                        BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Production, Fask.WEBAPI.API_BusinessObjects.BO_Production_row, Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable, Fask.Interfaces.DataSets.Vyroba.Production_KonzolaRow> x = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Production, Fask.WEBAPI.API_BusinessObjects.BO_Production_row, Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable, Fask.Interfaces.DataSets.Vyroba.Production_KonzolaRow>();

                        bo = x.GetBOFromDT(ds.Production_Konzola);


                        return Request.CreateResponse<Fask.WEBAPI.API_BusinessObjects.BO_Production>(HttpStatusCode.OK, bo);
                    }
                    catch (Exception ex)
                    {
                        //return Request.CreateResponse<string>(HttpStatusCode.BadRequest, ex.Message + ex.StackTrace);
                        return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
                    }

                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
            }

        }

        [HttpGet]
        [Route("api/Konzola_Production_FillBySOUBEHGUID")]
        public HttpResponseMessage Konzola_Production_FillBySOUBEHGUID()
        {
            Init();
            try
            {
                Fask.WEBAPI.API_BusinessObjects.BO_Production bo = new Fask.WEBAPI.API_BusinessObjects.BO_Production();
                Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable dt = new Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable();
                Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();

                #region parametry z URL
                Guid SOUBEHGUID;

                // Zdrojove URL, resp. parametry z něho
                string URL_param = Request.RequestUri.Query;

                //list parametru
                var SeznamParametru = HttpUtility.ParseQueryString(URL_param);

                var SOUBEHGUIDtmp = SeznamParametru.Get("SOUBEHGUID");
                SOUBEHGUID = Guid.Parse(SOUBEHGUIDtmp);


                #endregion


                if (provider != null && provider is Fask.Interfaces.Vyroba.Production.IProduction_FillBySOUBEHGUID)
                {
                    try
                    {
                        ((Fask.Interfaces.Vyroba.Production.IProduction_FillBySOUBEHGUID)provider).Production_FillBySOUBEHGUID(ds, SOUBEHGUID);

                        BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Production, Fask.WEBAPI.API_BusinessObjects.BO_Production_row, Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable, Fask.Interfaces.DataSets.Vyroba.Production_KonzolaRow> x = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Production, Fask.WEBAPI.API_BusinessObjects.BO_Production_row, Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable, Fask.Interfaces.DataSets.Vyroba.Production_KonzolaRow>();

                        bo = x.GetBOFromDT(ds.Production_Konzola);


                        return Request.CreateResponse<Fask.WEBAPI.API_BusinessObjects.BO_Production>(HttpStatusCode.OK, bo);
                    }
                    catch (Exception ex)
                    {
                        //return Request.CreateResponse<string>(HttpStatusCode.BadRequest, ex.Message + ex.StackTrace);
                        return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
                    }

                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
            }

        }

        [HttpGet]
        [Route("api/Konzola_Production_GetDataByCORRGUID")]
        public HttpResponseMessage Konzola_Production_GetDataByCORRGUID()
        {
            Init();
            try
            {
                Fask.WEBAPI.API_BusinessObjects.BO_Production bo = new Fask.WEBAPI.API_BusinessObjects.BO_Production();
                Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable dt = new Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable();
                Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();

                #region parametry z URL
                Guid CORRGUID;

                // Zdrojove URL, resp. parametry z něho
                string URL_param = Request.RequestUri.Query;

                //list parametru
                var SeznamParametru = HttpUtility.ParseQueryString(URL_param);

                var CORRGUIDtmp = SeznamParametru.Get("CORRGUID");
                CORRGUID = Guid.Parse(CORRGUIDtmp);


                #endregion


                if (provider != null && provider is Fask.Interfaces.Vyroba.Production.IProduction_GetDataByCORRGUID)
                {
                    try
                    {
                       dt = ((Fask.Interfaces.Vyroba.Production.IProduction_GetDataByCORRGUID)provider).Production_GetDataByCORRGUID(CORRGUID);

                        BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Production, Fask.WEBAPI.API_BusinessObjects.BO_Production_row, Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable, Fask.Interfaces.DataSets.Vyroba.Production_KonzolaRow> x = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Production, Fask.WEBAPI.API_BusinessObjects.BO_Production_row, Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable, Fask.Interfaces.DataSets.Vyroba.Production_KonzolaRow>();

                        bo = x.GetBOFromDT(dt);


                        return Request.CreateResponse<Fask.WEBAPI.API_BusinessObjects.BO_Production>(HttpStatusCode.OK, bo);
                    }
                    catch (Exception ex)
                    {
                        //return Request.CreateResponse<string>(HttpStatusCode.BadRequest, ex.Message + ex.StackTrace);
                        return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
                    }

                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
            }

        }

        [HttpGet]
        [Route("api/Konzola_Production_GetDataBySOUBEHGUID")]
        public HttpResponseMessage Konzola_Production_GetDataBySOUBEHGUID()
        {
            Init();
            try
            {
                Fask.WEBAPI.API_BusinessObjects.BO_Production bo = new Fask.WEBAPI.API_BusinessObjects.BO_Production();
                Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable dt = new Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable();
                Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();

                #region parametry z URL
                Guid SOUBEHGUID;

                // Zdrojove URL, resp. parametry z něho
                string URL_param = Request.RequestUri.Query;

                //list parametru
                var SeznamParametru = HttpUtility.ParseQueryString(URL_param);

                var SOUBEHGUIDtmp = SeznamParametru.Get("SOUBEHGUID");
                SOUBEHGUID = Guid.Parse(SOUBEHGUIDtmp);


                #endregion


                if (provider != null && provider is Fask.Interfaces.Vyroba.Production.IProduction_GetDataBySOUBEHGUID)
                {
                    try
                    {
                        dt = ((Fask.Interfaces.Vyroba.Production.IProduction_GetDataBySOUBEHGUID)provider).Production_GetDataBySOUBEHGUID(SOUBEHGUID);

                        BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Production, Fask.WEBAPI.API_BusinessObjects.BO_Production_row, Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable, Fask.Interfaces.DataSets.Vyroba.Production_KonzolaRow> x = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Production, Fask.WEBAPI.API_BusinessObjects.BO_Production_row, Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable, Fask.Interfaces.DataSets.Vyroba.Production_KonzolaRow>();

                        bo = x.GetBOFromDT(dt);


                        return Request.CreateResponse<Fask.WEBAPI.API_BusinessObjects.BO_Production>(HttpStatusCode.OK, bo);
                    }
                    catch (Exception ex)
                    {
                        //return Request.CreateResponse<string>(HttpStatusCode.BadRequest, ex.Message + ex.StackTrace);
                        return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
                    }

                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
            }

        }

        [HttpPost]
        [Route("api/Konzola_Production_Update")]
        public HttpResponseMessage Konzola_Production_Update([FromBody] Fask.WEBAPI.API_BusinessObjects.BO_Production FZ_data)
        {
            Init();

            bool result = false;
            Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable dt = new Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable();

            if (provider != null && provider is Fask.Interfaces.Vyroba.Production.IProduction_Update)
            {
                try
                {
                    BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Production, Fask.WEBAPI.API_BusinessObjects.BO_Production_row, Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable, Fask.Interfaces.DataSets.Vyroba.Production_KonzolaRow> y = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Production, Fask.WEBAPI.API_BusinessObjects.BO_Production_row, Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable, Fask.Interfaces.DataSets.Vyroba.Production_KonzolaRow>();
                    dt = y.GetDTFromBO(FZ_data);

                   
                        ((Fask.Interfaces.Vyroba.Production.IProduction_Update)provider).Production_Update(dt);
                        result = true;
                    
                    

                    return Request.CreateResponse<bool>(HttpStatusCode.OK, result);
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse<Exception>(HttpStatusCode.InternalServerError, ex);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        [Route("api/Konzola_Production_GetFiltrovanyProductionVazby")]
        public HttpResponseMessage Konzola_Production_GetFiltrovanyProductionVazby([FromBody] Fask.Interfaces.Filtry.Vazby_P_PS_Filtr filtr)
        {
            Init();
            try
            {
                Fask.WEBAPI.API_BusinessObjects.BO_Production bo = new Fask.WEBAPI.API_BusinessObjects.BO_Production();
                Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable dt = new Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable();
                Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();



                if (provider != null && provider is Fask.Interfaces.Vyroba.Production.IProduction_GetFiltrovanyProductionVazby)
                {
                    try
                    {
                        ds = ((Fask.Interfaces.Vyroba.Production.IProduction_GetFiltrovanyProductionVazby)provider).Production_GetFiltrovanyProductionVazby(filtr);

                        BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Production, Fask.WEBAPI.API_BusinessObjects.BO_Production_row, Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable, Fask.Interfaces.DataSets.Vyroba.Production_KonzolaRow> x = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Production, Fask.WEBAPI.API_BusinessObjects.BO_Production_row, Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable, Fask.Interfaces.DataSets.Vyroba.Production_KonzolaRow>();

                        bo = x.GetBOFromDT(ds.Production_Konzola);


                        return Request.CreateResponse<Fask.WEBAPI.API_BusinessObjects.BO_Production>(HttpStatusCode.OK, bo);
                    }
                    catch (Exception ex)
                    {
                        //return Request.CreateResponse<string>(HttpStatusCode.BadRequest, ex.Message + ex.StackTrace);
                        return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
                    }

                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
            }

        }

        #endregion

        #region Corrects
        [HttpGet]
        [Route("api/Konzola_Corrects_GetDataByID")]
        public HttpResponseMessage Konzola_Corrects_GetDataByID()
        {
            Init();
            try
            {
                Fask.WEBAPI.API_BusinessObjects.BO_Corrects bo = new Fask.WEBAPI.API_BusinessObjects.BO_Corrects();
                Fask.Interfaces.DataSets.Vyroba.CorrectsDataTable dt = new Fask.Interfaces.DataSets.Vyroba.CorrectsDataTable();
                Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();

                #region parametry z URL
                int ID;

                // Zdrojove URL, resp. parametry z něho
                string URL_param = Request.RequestUri.Query;

                //list parametru
                var SeznamParametru = HttpUtility.ParseQueryString(URL_param);

                var IDtmp = SeznamParametru.Get("ID");
                ID = Int32.Parse(IDtmp);


                #endregion


                if (provider != null && provider is Fask.Interfaces.Vyroba.Corrects.ICorrects_GetDataByID)
                {
                    try
                    {
                        dt = ((Fask.Interfaces.Vyroba.Corrects.ICorrects_GetDataByID)provider).Corrects_GetDataByID(ID);

                        BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Corrects, Fask.WEBAPI.API_BusinessObjects.BO_Corrects_row, Fask.Interfaces.DataSets.Vyroba.CorrectsDataTable, Fask.Interfaces.DataSets.Vyroba.CorrectsRow> x = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Corrects, Fask.WEBAPI.API_BusinessObjects.BO_Corrects_row, Fask.Interfaces.DataSets.Vyroba.CorrectsDataTable, Fask.Interfaces.DataSets.Vyroba.CorrectsRow>();

                        bo = x.GetBOFromDT(dt);


                        return Request.CreateResponse<Fask.WEBAPI.API_BusinessObjects.BO_Corrects>(HttpStatusCode.OK, bo);
                    }
                    catch (Exception ex)
                    {
                        //return Request.CreateResponse<string>(HttpStatusCode.BadRequest, ex.Message + ex.StackTrace);
                        return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
                    }

                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
            }

        }
        #endregion

        #region VMachinesOperations
        [HttpGet]
        [Route("api/Konzola_VMachinesOperations_GetDataByMachineIDoperationID")]
        public HttpResponseMessage Konzola_VMachinesOperations_GetDataByMachineIDoperationID()
        {
            Init();
            try
            {
                Fask.WEBAPI.API_BusinessObjects.BO_VMachinesOperations bo = new Fask.WEBAPI.API_BusinessObjects.BO_VMachinesOperations();
                Fask.Interfaces.DataSets.Vyroba.VMachinesOperationsDataTable dt = new Fask.Interfaces.DataSets.Vyroba.VMachinesOperationsDataTable();
                Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();

                #region parametry z URL
                string MachinesID;
                string OperationsID;

                // Zdrojove URL, resp. parametry z něho
                string URL_param = Request.RequestUri.Query;

                //list parametru
                var SeznamParametru = HttpUtility.ParseQueryString(URL_param);

                var MachinesIDtmp = SeznamParametru.Get("MachinesID");
                MachinesID = MachinesIDtmp.ToString();

                var OperationsIDtmp = SeznamParametru.Get("OperationsID");
                OperationsID = OperationsIDtmp.ToString();


                #endregion


                if (provider != null && provider is Fask.Interfaces.Vyroba.VMachinesOperations.IVMachinesOperations_GetDataByMachineIDoperationID)
                {
                    try
                    {
                        dt = ((Fask.Interfaces.Vyroba.VMachinesOperations.IVMachinesOperations_GetDataByMachineIDoperationID)provider).VMachinesOperations_GetDataByMachineIDoperationID(MachinesID, OperationsID);

                        BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_VMachinesOperations, Fask.WEBAPI.API_BusinessObjects.BO_VMachinesOperations_row, Fask.Interfaces.DataSets.Vyroba.VMachinesOperationsDataTable, Fask.Interfaces.DataSets.Vyroba.VMachinesOperationsRow> x = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_VMachinesOperations, Fask.WEBAPI.API_BusinessObjects.BO_VMachinesOperations_row, Fask.Interfaces.DataSets.Vyroba.VMachinesOperationsDataTable, Fask.Interfaces.DataSets.Vyroba.VMachinesOperationsRow>();

                        bo = x.GetBOFromDT(dt);


                        return Request.CreateResponse<Fask.WEBAPI.API_BusinessObjects.BO_VMachinesOperations>(HttpStatusCode.OK, bo);
                    }
                    catch (Exception ex)
                    {
                        //return Request.CreateResponse<string>(HttpStatusCode.BadRequest, ex.Message + ex.StackTrace);
                        return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
                    }

                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
            }

        }
        #endregion

        #region tiskove sablony

        //todo dodelat MaR
        [HttpPost]
        [Route("api/Konzola_TiskoveSablony_GetFiltrovanyOdvodTiskoveSablony")]
        public HttpResponseMessage Konzola_TiskoveSablony_GetFiltrovanyOdvodTiskoveSablony([FromBody] Fask.Interfaces.Filtry.Odvod_TiskoveSablonyFiltr filtr)
        {
            Fask.WEBAPI.API_BusinessObjects.BO_FASK_FORMULARE bo = new Fask.WEBAPI.API_BusinessObjects.BO_FASK_FORMULARE();
            Fask.Interfaces.DataSets.Vyroba.FASK_FORMULAREDataTable dt = new Fask.Interfaces.DataSets.Vyroba.FASK_FORMULAREDataTable();
            Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();

            Init();

            if (provider != null && provider is Fask.Interfaces.Vyroba.Odvod_TiskoveSablony.IOdvod_TiskoveSablony_GetFiltrovanyTiskoveSablony)
            {
                try
                {
                    ds = ((Fask.Interfaces.Vyroba.Odvod_TiskoveSablony.IOdvod_TiskoveSablony_GetFiltrovanyTiskoveSablony)provider).TiskoveSablony_GetFiltrovanyOdvodTiskoveSablony(filtr);

                    BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_FASK_FORMULARE, Fask.WEBAPI.API_BusinessObjects.BO_FASK_FORMULARE_row, Fask.Interfaces.DataSets.Vyroba.FASK_FORMULAREDataTable, Fask.Interfaces.DataSets.Vyroba.FASK_FORMULARERow> x = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_FASK_FORMULARE, Fask.WEBAPI.API_BusinessObjects.BO_FASK_FORMULARE_row, Fask.Interfaces.DataSets.Vyroba.FASK_FORMULAREDataTable, Fask.Interfaces.DataSets.Vyroba.FASK_FORMULARERow>();

                    bo = x.GetBOFromDT(ds.FASK_FORMULARE);


                    return Request.CreateResponse<Fask.WEBAPI.API_BusinessObjects.BO_FASK_FORMULARE>(HttpStatusCode.OK, bo);


                }
                catch (Exception ex)
                {
                    return Request.CreateResponse<Exception>(HttpStatusCode.InternalServerError, ex);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        [Route("api/Konzola_Tisk_TiskovaSablonaEdit_DB")]
        public HttpResponseMessage Konzola_Tisk_TiskovaSablonaEdit_DB([FromBody] Fask.WEBAPI.API_BusinessObjects.BO_FASK_FORMULARE FZ_data)
        {
            Init();

            int result;

                if (provider != null && provider is Fask.Server.Interfaces.Tisky.ITisk_TiskovaSablona)
                {
                try
                {

                    BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_FASK_FORMULARE, Fask.WEBAPI.API_BusinessObjects.BO_FASK_FORMULARE_row, Fask.Interfaces.DataSets.Vyroba.FASK_FORMULAREDataTable, Fask.Interfaces.DataSets.Vyroba.FASK_FORMULARERow> y = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_FASK_FORMULARE, Fask.WEBAPI.API_BusinessObjects.BO_FASK_FORMULARE_row, Fask.Interfaces.DataSets.Vyroba.FASK_FORMULAREDataTable, Fask.Interfaces.DataSets.Vyroba.FASK_FORMULARERow>();
                  
                    var dt = y.GetDTFromBO(FZ_data);

                    Fask.Interfaces.DataSets.Vyroba.FASK_FORMULARERow row = dt.First();

                    row.AcceptChanges();
                    row.SetModified();

                    result = ((Fask.Server.Interfaces.Tisky.ITisk_TiskovaSablona)provider).TiskovaSablonaEdit_DB(row);
                    return Request.CreateResponse<int>(HttpStatusCode.OK, result);


                }
                catch (Exception ex)
                {
                    return Request.CreateResponse<Exception>(HttpStatusCode.InternalServerError, ex);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }


        [HttpPost]
        [Route("api/Konzola_Tisk_TiskovaSablonaInsert_DB")]
        public HttpResponseMessage Konzola_Tisk_TiskovaSablonaInsert_DB([FromBody] Fask.WEBAPI.API_BusinessObjects.BO_FASK_FORMULARE FZ_data)
        {
            Init();

            int result;

            if (provider != null && provider is Fask.Server.Interfaces.Tisky.ITisk_TiskovaSablona)
            {
                try
                {

                    BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_FASK_FORMULARE, Fask.WEBAPI.API_BusinessObjects.BO_FASK_FORMULARE_row, Fask.Interfaces.DataSets.Vyroba.FASK_FORMULAREDataTable, Fask.Interfaces.DataSets.Vyroba.FASK_FORMULARERow> y = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_FASK_FORMULARE, Fask.WEBAPI.API_BusinessObjects.BO_FASK_FORMULARE_row, Fask.Interfaces.DataSets.Vyroba.FASK_FORMULAREDataTable, Fask.Interfaces.DataSets.Vyroba.FASK_FORMULARERow>();

                    var dt = y.GetDTFromBO(FZ_data);

                    Fask.Interfaces.DataSets.Vyroba.FASK_FORMULARERow row = dt.First();

                    //row.AcceptChanges();
                    //row.SetModified();

                    result = ((Fask.Server.Interfaces.Tisky.ITisk_TiskovaSablona)provider).TiskovaSablonaInsert_DB(row);
                    return Request.CreateResponse<int>(HttpStatusCode.OK, result);


                }
                catch (Exception ex)
                {
                    return Request.CreateResponse<Exception>(HttpStatusCode.InternalServerError, ex);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        [Route("api/Konzola_Tisk_TiskovaSablonaDelete_DB")]
        public HttpResponseMessage Konzola_Tisk_TiskovaSablonaDelete_DB([FromBody] Fask.WEBAPI.API_BusinessObjects.BO_FASK_FORMULARE FZ_data)
        {
            Init();

            int result;

            if (provider != null && provider is Fask.Server.Interfaces.Tisky.ITisk_TiskovaSablona)
            {
                try
                {

                    BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_FASK_FORMULARE, Fask.WEBAPI.API_BusinessObjects.BO_FASK_FORMULARE_row, Fask.Interfaces.DataSets.Vyroba.FASK_FORMULAREDataTable, Fask.Interfaces.DataSets.Vyroba.FASK_FORMULARERow> y = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_FASK_FORMULARE, Fask.WEBAPI.API_BusinessObjects.BO_FASK_FORMULARE_row, Fask.Interfaces.DataSets.Vyroba.FASK_FORMULAREDataTable, Fask.Interfaces.DataSets.Vyroba.FASK_FORMULARERow>();

                    var dt = y.GetDTFromBO(FZ_data);

                    Fask.Interfaces.DataSets.Vyroba.FASK_FORMULARERow row = dt.First();

                    //row.AcceptChanges();
                    //row.SetModified();

                    result = ((Fask.Server.Interfaces.Tisky.ITisk_TiskovaSablona)provider).TiskovaSablonaDelete_DB(row);
                    return Request.CreateResponse<int>(HttpStatusCode.OK, result);


                }
                catch (Exception ex)
                {
                    return Request.CreateResponse<Exception>(HttpStatusCode.InternalServerError, ex);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }


        #endregion


        #endregion

        #endregion

        #region Transakce

        #region Výrobní příkazy

        #region VPH

        [HttpGet]
        [Route("api/Konzola_GetDataByCountEntriesSOPNUMBE")]
        public HttpResponseMessage Konzola_GetDataByCountEntriesSOPNUMBE()
        {
            Init();

            try
            {
                Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHDataTable dt = new Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHDataTable();


                #region parametry z URL
                int CountEntries;
                string SOPNUMBE = null;

                // Zdrojove URL, resp. parametry z něho
                string URL_param = Request.RequestUri.Query;

                //list parametru
                var SeznamParametru = HttpUtility.ParseQueryString(URL_param);

                var CountEntriestmp = SeznamParametru.Get("CountEntries");
                CountEntries = Int32.Parse(CountEntriestmp);

                var SOPNUMBEtmp = SeznamParametru.Get("SOPNUMBE");
                SOPNUMBE = SOPNUMBEtmp.ToString();
                #endregion

                if (provider != null && provider is Fask.Interfaces.Vyroba.VPH.IVPH_GetDataByCountEntriesSOPNUMBE)
                {
                    try
                    {
                        dt = ((Fask.Interfaces.Vyroba.VPH.IVPH_GetDataByCountEntriesSOPNUMBE)provider).GetDataByCountEntriesSOPNUMBE(CountEntries, SOPNUMBE);

                        BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH, Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH_row, Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHDataTable, Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHRow> x = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH, Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH_row, Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHDataTable, Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHRow>();
                        var bo_data = x.GetBOFromDT(dt);

                        return Request.CreateResponse<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH>(HttpStatusCode.OK, bo_data);
                    }
                    catch (Exception ex)
                    {
                        return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
                    }

                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
            }

        }

        [HttpPost]
        [Route("api/Konzola_VPH_Update")]
        public HttpResponseMessage Konzola_VPH_Update([FromBody] Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH FZ_data)
        {
            Init();

            int result;

            if (provider != null && provider is Fask.Interfaces.Vyroba.VPH.IVPH_Update)
            {
                try
                {

                    BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH, Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH_row, Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHDataTable, Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHRow> y = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH, Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH_row, Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHDataTable, Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHRow>();
                    var dt = y.GetDTFromBO(FZ_data);


                    result = ((Fask.Interfaces.Vyroba.VPH.IVPH_Update)provider).Update(dt);
                    return Request.CreateResponse<int>(HttpStatusCode.OK, result);


                }
                catch (Exception ex)
                {
                    return Request.CreateResponse<Exception>(HttpStatusCode.InternalServerError, ex);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        [Route("api/Konzola_VPH_Fill")]
        public HttpResponseMessage Konzola_VPH_Fill()
        {
            Init();
            Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();

            if (provider != null && provider is Fask.Interfaces.Vyroba.VPH.IVPH_Fill)
            {
                try
                {

                    ((Fask.Interfaces.Vyroba.VPH.IVPH_Fill)provider).VPH_Fill(ds);

                    BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH, Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH_row, Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHDataTable, Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHRow> x = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH, Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH_row, Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHDataTable, Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHRow>();
                    var bo_data = x.GetBOFromDT(ds.CZPRO_VPH);

                    return Request.CreateResponse<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH>(HttpStatusCode.OK, bo_data);


                }
                catch (Exception ex)
                {
                    return Request.CreateResponse<Exception>(HttpStatusCode.InternalServerError, ex);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        [Route("api/Konzola_VPH_Insert_Row_Values")]
        public HttpResponseMessage Konzola_VPH_Insert_Row_Values()
        {

            Init();
            bool result = false;
            try
            {

                #region parametry z URL
                int CountEntries;
                string SOPNUMBE = null;
                string SOPTYPE = null;
                string SOPDESC = null;
                string VNDDOCNMH = null;
                string BarcodeH = null;
                string LOCNCODE = null;
                short DateProd;
                string Rez1 = null;
                string Rez2 = null;
                byte TermID;
                DateTime LSTMod;
                byte Active;


                // Zdrojove URL, resp. parametry z něho
                string URL_param = Request.RequestUri.Query;

                //list parametru
                var SeznamParametru = HttpUtility.ParseQueryString(URL_param);

                var CountEntriestmp = SeznamParametru.Get("CountEntries");
                CountEntries = Int32.Parse(CountEntriestmp);

                var SOPNUMBEtmp = SeznamParametru.Get("SOPNUMBE");
                SOPNUMBE = SOPNUMBEtmp.ToString();

                var SOPTYPEtmp = SeznamParametru.Get("SOPTYPE");
                SOPTYPE = SOPTYPEtmp.ToString();

                var SOPDESCtmp = SeznamParametru.Get("SOPDESC");
                SOPDESC = SOPDESCtmp.ToString();

                var VNDDOCNMHtmp = SeznamParametru.Get("VNDDOCNMH");
                VNDDOCNMH = VNDDOCNMHtmp.ToString();

                var BarcodeHtmp = SeznamParametru.Get("BarcodeH");
                BarcodeH = BarcodeHtmp.ToString();

                var LOCNCODEtmp = SeznamParametru.Get("LOCNCODE");
                LOCNCODE = LOCNCODEtmp.ToString();

                var DateProdtmp = SeznamParametru.Get("DateProd");
                DateProd = Int16.Parse(DateProdtmp);

                var Rez1tmp = SeznamParametru.Get("Rez1");
                Rez1 = Rez1tmp.ToString();

                var Rez2tmp = SeznamParametru.Get("Rez2");
                Rez2 = Rez2tmp.ToString();

                var TermIDtmp = SeznamParametru.Get("TermID");
                TermID = Byte.Parse(TermIDtmp);

                var LSTModtmp = SeznamParametru.Get("LSTMod");
                LSTMod = DateTime.Parse(LSTModtmp, System.Globalization.DateTimeFormatInfo.InvariantInfo);

                var Activetmp = SeznamParametru.Get("Active");
                Active = Byte.Parse(Activetmp);

                #endregion

                if (provider != null && provider is Fask.Interfaces.Vyroba.VPH.IVPH_Insert)
                {
                    try
                    {
                        Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHDataTable dt = new Interfaces.DataSets.Vyroba.CZPRO_VPHDataTable();

                        Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHRow row = dt.NewCZPRO_VPHRow();
                            row.CountEntries = CountEntries;
                            row.SOPNUMBE = SOPNUMBE;
                            row.SOPTYPE = SOPTYPE;
                            row.SOPDESC = SOPDESC;
                            row.VNDDOCNMH = VNDDOCNMH;
                            row.BarcodeH = BarcodeH;
                            row.LOCNCODE = LOCNCODE;
                            row.DateProd = DateProd;
                            row.Rez1 = Rez1;
                            row.Rez2 = Rez2;
                            row.TermID = TermID;
                            row.LSTMod = LSTMod;
                            row.Active = Active;


                        ((Fask.Interfaces.Vyroba.VPH.IVPH_Insert)provider).Insert(row);

                        result = true;

                        return Request.CreateResponse<bool>(HttpStatusCode.OK, result);
                    }
                    catch (Exception ex)
                    {
                        return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
                    }

                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
            }

        }

        [HttpPost]
        [Route("api/Konzola_VPH_Insert_Row")]
        public HttpResponseMessage Konzola_VPH_Insert_Row([FromBody] Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH FZ_data)
        {
            //TODO MaR zmenit metodu aby prijimala objekt!!
            //Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHRow row = null;

            Init();
            bool result = false;
            try
            {
                BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH, Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH_row, Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHDataTable, Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHRow> y = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH, Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH_row, Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHDataTable, Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHRow>();
                var dt = y.GetDTFromBO(FZ_data);

                if (provider != null && provider is Fask.Interfaces.Vyroba.VPH.IVPH_Insert)
                {
                    try
                    {
                        if (dt.Count == 1)
                        {
                            //dt[0].SetModified();
                            ((Fask.Interfaces.Vyroba.VPH.IVPH_Insert)provider).Insert(dt.First());
                            result = true;
                        }
                        else
                        {
                            result = false;
                        }

                        return Request.CreateResponse<bool>(HttpStatusCode.OK, result);
                    }
                    catch (Exception ex)
                    {
                        return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
                    }

                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
            }

        }

        [HttpPost]
        [Route("api/Konzola_VPH_Update_Row")]
        public HttpResponseMessage Konzola_VPH_Update_Row([FromBody] Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH FZ_data)
        {
            Init();
            int result;

            if (provider != null && provider is Fask.Interfaces.Vyroba.VPH.IVPH_Update_Row)
            {
                try
                {
                    BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH, Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH_row, Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHDataTable, Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHRow> y = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH, Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH_row, Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHDataTable, Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHRow>();
                    var dt = y.GetDTFromBO(FZ_data);


                    if (dt.Count == 1)
                    {
                       // dt[0].SetModified();
                        result = ((Fask.Interfaces.Vyroba.VPH.IVPH_Update_Row)provider).Update_Row(dt.First());
                    }
                    else
                    {
                        result = 0;
                    }

                    return Request.CreateResponse<int>(HttpStatusCode.OK, result);


                }
                catch (Exception ex)
                {
                    return Request.CreateResponse<Exception>(HttpStatusCode.InternalServerError, ex);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        [Route("api/Konzola_GetFiltrovanyVPHList")]
        public HttpResponseMessage Konzola_GetFiltrovanyVPHList([FromBody] Fask.Interfaces.Filtry.Vyroba_VPH_Filtr filtr)
        {
            Init();

            Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH bo = new WEBAPI.API_BusinessObjects.BO_CZPRO_VPH();
            Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();

            if (provider != null && provider is Fask.Interfaces.Vyroba.VPH.IVPH_GetFiltrovanyVPHList)
            {
                try
                {
                    ds = ((Fask.Interfaces.Vyroba.VPH.IVPH_GetFiltrovanyVPHList)provider).GetFiltrovanyVPHList(filtr);

                    BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH, Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH_row, Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHDataTable, Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHRow> x = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH, Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH_row, Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHDataTable, Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHRow>();
                    bo = x.GetBOFromDT(ds.CZPRO_VPH);

                    return Request.CreateResponse<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH>(HttpStatusCode.OK, bo);
                }
                catch (Exception ex)
                {
                    //return Request.CreateResponse<string>(HttpStatusCode.BadRequest, ex.Message + ex.StackTrace);
                    return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
                }

            }
            else
                return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        #endregion

        #region VPP

        [HttpGet]
        [Route("api/Konzola_GetDataByCountEntriesSOPNUMBEITEMNMBR")]
        public HttpResponseMessage Konzola_GetDataByCountEntriesSOPNUMBEITEMNMBR()
        {
            Init();
            try
            {
                Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP bo = new Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP();
                Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable dt = new Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable();

                #region parametry z URL
                int CountEntries;
                string SOPNUMBE = null;
                string ITEMNMBR = null;

                // Zdrojove URL, resp. parametry z něho
                string URL_param = Request.RequestUri.Query;

                //list parametru
                var SeznamParametru = HttpUtility.ParseQueryString(URL_param);

                var CountEntriestmp = SeznamParametru.Get("CountEntries");
                CountEntries = Int32.Parse(CountEntriestmp);

                var SOPNUMBEtmp = SeznamParametru.Get("SOPNUMBE");
                SOPNUMBE = SOPNUMBEtmp.ToString();

                var ITEMNMBRtmp = SeznamParametru.Get("ITEMNMBR");
                ITEMNMBR = ITEMNMBRtmp.ToString();

                #endregion

                if (provider != null && provider is Fask.Interfaces.Vyroba.VPP.IVPP_GetDataByCountEntriesSOPNUMBEITEMNMBR)
                {
                    try
                    {
                        dt = ((Fask.Interfaces.Vyroba.VPP.IVPP_GetDataByCountEntriesSOPNUMBEITEMNMBR)provider).GetDataByCountEntriesSOPNUMBEITEMNMBR(CountEntries, SOPNUMBE, ITEMNMBR);

                        BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP, Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP_row, Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable, Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPRow> x = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP, Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP_row, Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable, Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPRow>();
                        bo = x.GetBOFromDT(dt);


                        return Request.CreateResponse<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP>(HttpStatusCode.OK, bo);
                    }
                    catch (Exception ex)
                    {
                        //return Request.CreateResponse<string>(HttpStatusCode.BadRequest, ex.Message + ex.StackTrace);
                        return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
                    }

                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
            }

        }

        [HttpGet]
        [Route("api/Konzola_GetDataByCountEntriesSopnumbeItemnmbrBarcodeP")]
        public HttpResponseMessage Konzola_GetDataByCountEntriesSopnumbeItemnmbrBarcodeP()
        {
            Init();
            try
            {
                Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP bo = new Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP();
                Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable dt = new Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable();

                #region parametry z URL
                int CountEntries;
                string SOPNUMBE = null;
                string ITEMNMBR = null;
                string BarcodeP = null;

                // Zdrojove URL, resp. parametry z něho
                string URL_param = Request.RequestUri.Query;

                //list parametru
                var SeznamParametru = HttpUtility.ParseQueryString(URL_param);

                var CountEntriestmp = SeznamParametru.Get("CountEntries");
                CountEntries = Int32.Parse(CountEntriestmp);

                var SOPNUMBEtmp = SeznamParametru.Get("SOPNUMBE");
                SOPNUMBE = SOPNUMBEtmp.ToString();

                var ITEMNMBRtmp = SeznamParametru.Get("ITEMNMBR");
                ITEMNMBR = ITEMNMBRtmp.ToString();

                var BarcodePtmp = SeznamParametru.Get("BarcodeP");
                BarcodeP = BarcodePtmp.ToString();

                #endregion

                if (provider != null && provider is Fask.Interfaces.Vyroba.VPP.IVPP_GetDataByCountEntriesSopnumbeItemnmbrBarcodeP)
                {
                    try
                    {
                        dt = ((Fask.Interfaces.Vyroba.VPP.IVPP_GetDataByCountEntriesSopnumbeItemnmbrBarcodeP)provider).GetDataByCountEntriesSopnumbeItemnmbrBarcodeP(CountEntries, SOPNUMBE, ITEMNMBR, BarcodeP);

                        BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP, Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP_row, Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable, Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPRow> x = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP, Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP_row, Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable, Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPRow>();
                        bo = x.GetBOFromDT(dt);


                        return Request.CreateResponse<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP>(HttpStatusCode.OK, bo);
                    }
                    catch (Exception ex)
                    {
                        //return Request.CreateResponse<string>(HttpStatusCode.BadRequest, ex.Message + ex.StackTrace);
                        return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
                    }

                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
            }

        }

        [HttpGet]
        [Route("api/Konzola_FillByCountEntriesAndSOPNUMBE")]
        public HttpResponseMessage Konzola_FillByCountEntriesAndSOPNUMBE()
        {
            Init();
            try
            {
                Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP bo = new Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP();
                Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable dt = new Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable();
                Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();
                int result;

                #region parametry z URL
                int CountEntries;
                string SOPNUMBE = null;
                string TypeORDERBY = null;

                // Zdrojove URL, resp. parametry z něho
                string URL_param = Request.RequestUri.Query;

                //list parametru
                var SeznamParametru = HttpUtility.ParseQueryString(URL_param);

                var CountEntriestmp = SeznamParametru.Get("CountEntries");
                CountEntries = Int32.Parse(CountEntriestmp);

                var SOPNUMBEtmp = SeznamParametru.Get("SOPNUMBE");
                SOPNUMBE = SOPNUMBEtmp.ToString();

                var TypeORDERBYtmp = SeznamParametru.Get("TypeORDERBY");
                TypeORDERBY = TypeORDERBYtmp.ToString();


                #endregion

                if (provider != null && provider is Fask.Interfaces.Vyroba.VPP.IVPP_FillByCountEntriesAndSOPNUMBE)
                {
                    try
                    {
                        result = ((Fask.Interfaces.Vyroba.VPP.IVPP_FillByCountEntriesAndSOPNUMBE)provider).FillByCountEntriesAndSOPNUMBE(ds, TypeORDERBY, CountEntries, SOPNUMBE);

                        BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP, Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP_row, Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable, Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPRow> x = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP, Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP_row, Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable, Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPRow>();
                       
                        bo = x.GetBOFromDT(ds.CZPRO_VPP);


                        return Request.CreateResponse<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP>(HttpStatusCode.OK, bo);
                    }
                    catch (Exception ex)
                    {
                        //return Request.CreateResponse<string>(HttpStatusCode.BadRequest, ex.Message + ex.StackTrace);
                        return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
                    }

                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
            }

        }

        [HttpGet]
        [Route("api/Konzola_VPP_Fill")]
        public HttpResponseMessage Konzola_VPP_Fill()
        {
            Init();
            try
            {
                Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP bo = new Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP();
                Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable dt = new Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable();
                Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();

            

                if (provider != null && provider is Fask.Interfaces.Vyroba.VPP.IVPP_Fill)
                {
                    try
                    {
                       ((Fask.Interfaces.Vyroba.VPP.IVPP_Fill)provider).VPP_Fill(ds);

                        BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP, Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP_row, Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable, Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPRow> x = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP, Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP_row, Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable, Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPRow>();

                        bo = x.GetBOFromDT(ds.CZPRO_VPP);


                        return Request.CreateResponse<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP>(HttpStatusCode.OK, bo);
                    }
                    catch (Exception ex)
                    {
                        //return Request.CreateResponse<string>(HttpStatusCode.BadRequest, ex.Message + ex.StackTrace);
                        return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
                    }

                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
            }

        }

        [HttpDelete]
        [Route("api/Konzola_DeleteByCountEntriesSOPNUMBE")]
        public HttpResponseMessage Konzola_DeleteByCountEntriesSOPNUMBE()
        {
            Init();
            bool stav = false;

            if (provider != null && provider is Fask.Interfaces.Vyroba.VPP.IVPP_DeleteByCountEntriesSOPNUMBE)
            {
                try
                {
                    #region parametry z URL

                    int CountEntries;
                    string SOPNUMBE = null;

                    // Zdrojove URL, resp. parametry z něho
                    string URL_param = Request.RequestUri.Query;

                    //list parametru
                    var SeznamParametru = HttpUtility.ParseQueryString(URL_param);

                    var CountEntriestmp = SeznamParametru.Get("CountEntries");
                    CountEntries = int.Parse(CountEntriestmp);

                    var SOPNUMBEtmp = SeznamParametru.Get("SOPNUMBE");
                    SOPNUMBE = SOPNUMBEtmp.ToString();
                    #endregion


                    ((Fask.Interfaces.Vyroba.VPP.IVPP_DeleteByCountEntriesSOPNUMBE)provider).DeleteByCountEntriesSOPNUMBE(CountEntries,  SOPNUMBE);

                    stav = true;

                    return Request.CreateResponse<bool>(HttpStatusCode.OK, stav);

                }
                catch (Exception ex)
                {
                    return Request.CreateResponse<Exception>(HttpStatusCode.InternalServerError, ex);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        [Route("api/Konzola_VPP_Update")]
        public HttpResponseMessage Konzola_VPP_Update([FromBody] Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP FZ_data)
        {
            Init();

            bool result = false;
            Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable dt = new Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable();

            if (provider != null && provider is Fask.Interfaces.Vyroba.VPP.IVPP_Update)
            {
                try
                {
                    BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP, Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP_row, Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable, Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPRow> y = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP, Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP_row, Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable, Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPRow>();
                     dt = y.GetDTFromBO(FZ_data);

                    if (dt.Count == 1)
                    {

                        ((Fask.Interfaces.Vyroba.VPP.IVPP_Update)provider).VPP_Update(dt);
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }

                    return Request.CreateResponse<bool>(HttpStatusCode.OK, result);
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse<Exception>(HttpStatusCode.InternalServerError, ex);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        [Route("api/Konzola_VPP_Insert_Values")]
        public HttpResponseMessage Konzola_VPP_Insert_Values()
        {

            Init();
            //int result;
            try
            {

                #region parametry z URL
                    int CountEntries;
                    string SOPNUMBE = null;
                    string ITEMNMBR = null;
                    string ITEMTYPE = null;
                    string ITEMDESC = null;
                    string ITEMMJ = null;
                    string VNDDOCNMP = null;
                    string VNDITNUM = null;
                    int ORD;
                    string BarcodeP = null;
                    string LOCNCODE = null;
                    decimal QTYSHPPD;
                    decimal QTYDOKON;
                    decimal QTYPACK;
                    string QTYPACKMJ = null;
                    int TIMEMODE;
                    float TIMEPREP;
                    float TIMEUNIT;
                    byte DtProdT;
                    short DtProdL;
                    byte SerNumT;
                    short SerNumL;
                    byte VerT;
                    short VerL;
                    byte TermID;
                    DateTime LSTMod;
                    byte BarcodeT;
                    DateTime? Realization_Start;
                    DateTime? Realization_Stop;
                    byte CZ_REZ1_Track;
                    byte CZ_REZ2_Track;
                    byte CZ_REZ3_Track;
                    byte CZ_REZ4_Track;
                    byte CZ_REZ5_Track;
                    decimal? WEIGHT_TARA;
                    decimal? WEIGHT_NETTO;
                    decimal? WEIGHT_TOL_PLUS;
                    decimal? WEIGHT_TOL_MINUS;


                // Zdrojove URL, resp. parametry z něho
                string URL_param = Request.RequestUri.Query;

                //list parametru
                var SeznamParametru = HttpUtility.ParseQueryString(URL_param);

                var CountEntriestmp = SeznamParametru.Get("CountEntries");
                CountEntries = Int32.Parse(CountEntriestmp);

                var SOPNUMBEtmp = SeznamParametru.Get("SOPNUMBE");
                SOPNUMBE = SOPNUMBEtmp.ToString();

                var ITEMNMBRtmp = SeznamParametru.Get("ITEMNMBR");
                ITEMNMBR = ITEMNMBRtmp.ToString();

                var ITEMTYPEtmp = SeznamParametru.Get("ITEMTYPE");
                ITEMTYPE = ITEMTYPEtmp.ToString();

                var ITEMDESCtmp = SeznamParametru.Get("ITEMDESC");
                ITEMDESC = ITEMDESCtmp.ToString();

                var ITEMMJtmp = SeznamParametru.Get("ITEMMJ");
                ITEMMJ = ITEMMJtmp.ToString();

                var VNDDOCNMPtmp = SeznamParametru.Get("VNDDOCNMP");
                VNDDOCNMP = VNDDOCNMPtmp.ToString();

                var VNDITNUMtmp = SeznamParametru.Get("VNDITNUM");
                VNDITNUM = VNDITNUMtmp.ToString();

                var ORDtmp = SeznamParametru.Get("ORD");
                ORD = Int32.Parse( ORDtmp);

                var BarcodePtmp = SeznamParametru.Get("BarcodeP");
                BarcodeP = BarcodePtmp.ToString();

                var LOCNCODEtmp = SeznamParametru.Get("LOCNCODE");
                LOCNCODE = LOCNCODEtmp.ToString();

                var QTYSHPPDtmp = SeznamParametru.Get("QTYSHPPD");
                QTYSHPPD = Convert.ToDecimal(QTYSHPPDtmp);

                var QTYDOKONtmp = SeznamParametru.Get("QTYDOKON");
                QTYDOKON = Convert.ToDecimal(QTYDOKONtmp);

                var QTYPACKtmp = SeznamParametru.Get("QTYPACK");
                QTYPACK = Convert.ToDecimal(QTYPACKtmp);

                var QTYPACKMJtmp = SeznamParametru.Get("QTYPACKMJ");
                QTYPACKMJ = QTYPACKMJtmp.ToString();

                var TIMEMODEtmp = SeznamParametru.Get("TIMEMODE");
                TIMEMODE = Int32.Parse(TIMEMODEtmp);

                var TIMEPREPtmp = SeznamParametru.Get("TIMEPREP");
                TIMEPREP = float.Parse(TIMEPREPtmp);

                var TIMEUNITtmp = SeznamParametru.Get("TIMEUNIT");
                TIMEUNIT = float.Parse(TIMEUNITtmp);

                var DtProdTtmp = SeznamParametru.Get("DtProdT");
                DtProdT = Byte.Parse(DtProdTtmp);

                var DtProdLtmp = SeznamParametru.Get("DtProdL");
                DtProdL = short.Parse(DtProdLtmp);

                var SerNumTtmp = SeznamParametru.Get("SerNumT");
                SerNumT = Byte.Parse(SerNumTtmp);

                var SerNumLtmp = SeznamParametru.Get("SerNumL");
                SerNumL = short.Parse(SerNumLtmp);

                var VerTtmp = SeznamParametru.Get("VerT");
                VerT = Byte.Parse(VerTtmp);

                var VerLtmp = SeznamParametru.Get("VerL");
                VerL = short.Parse(VerLtmp);

                var TermIDtmp = SeznamParametru.Get("TermID");
                TermID = Byte.Parse(TermIDtmp);

                var LSTModtmp = SeznamParametru.Get("LSTMod");
                LSTMod = DateTime.Parse(LSTModtmp, System.Globalization.DateTimeFormatInfo.InvariantInfo);

                var BarcodeTtmp = SeznamParametru.Get("BarcodeT");
                BarcodeT = Byte.Parse(BarcodeTtmp);

                try
                {
                    var Realization_Starttmp = SeznamParametru.Get("Realization_Start");
                    Realization_Start = DateTime.Parse(Realization_Starttmp);
                }
                catch { Realization_Start = null; }

                try
                {
                    var Realization_Stoptmp = SeznamParametru.Get("Realization_Stop");
                    Realization_Stop = DateTime.Parse(Realization_Stoptmp);
                }
                catch { Realization_Stop = null; }

                var CZ_REZ1_Tracktmp = SeznamParametru.Get("CZ_REZ1_Track");
                CZ_REZ1_Track = Byte.Parse(CZ_REZ1_Tracktmp);

                var CZ_REZ2_Tracktmp = SeznamParametru.Get("CZ_REZ2_Track");
                CZ_REZ2_Track = Byte.Parse(CZ_REZ2_Tracktmp);

                var CZ_REZ3_Tracktmp = SeznamParametru.Get("CZ_REZ3_Track");
                CZ_REZ3_Track = Byte.Parse(CZ_REZ3_Tracktmp);

                var CZ_REZ4_Tracktmp = SeznamParametru.Get("CZ_REZ4_Track");
                CZ_REZ4_Track = Byte.Parse(CZ_REZ4_Tracktmp);

                var CZ_REZ5_Tracktmp = SeznamParametru.Get("CZ_REZ5_Track");
                CZ_REZ5_Track = Byte.Parse(CZ_REZ5_Tracktmp);

                try
                {
                    var WEIGHT_TARAtmp = SeznamParametru.Get("WEIGHT_TARA");
                    WEIGHT_TARA = decimal.Parse(WEIGHT_TARAtmp);

                }
                catch { WEIGHT_TARA = null; }

                try
                {
                    var WEIGHT_NETTOtmp = SeznamParametru.Get("WEIGHT_NETTO");
                    WEIGHT_NETTO = decimal.Parse(WEIGHT_NETTOtmp);
                }
                catch { WEIGHT_NETTO = null; }

                try
                {
                    var WEIGHT_TOL_PLUStmp = SeznamParametru.Get("WEIGHT_TOL_PLUS");
                    WEIGHT_TOL_PLUS = decimal.Parse(WEIGHT_TOL_PLUStmp);

                }
                catch { WEIGHT_TOL_PLUS = null; }

                try
                {
                    var WEIGHT_TOL_MINUStmp = SeznamParametru.Get("WEIGHT_TOL_MINUS");
                    WEIGHT_TOL_MINUS = decimal.Parse(WEIGHT_TOL_MINUStmp);
                }
                catch { WEIGHT_TOL_MINUS = null; }
               


                #endregion

                if (provider != null && provider is Fask.Interfaces.Vyroba.VPP.IVPP_Insert)
                {
                    try
                    {
                        Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable dt = new Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable();

                        Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPRow row = dt.NewCZPRO_VPPRow();

                        row.CountEntries = CountEntries;  
                        row.SOPNUMBE = SOPNUMBE;  
                        row.ITEMNMBR = ITEMNMBR;  
                        row.ITEMTYPE = ITEMTYPE;  
                        row.ITEMDESC = ITEMDESC;  
                        row.ITEMMJ = ITEMMJ;  
                        row.VNDDOCNMP = VNDDOCNMP;  
                        row.VNDITNUM = VNDITNUM;  
                        row.ORD = ORD;  
                        row.BarcodeP = BarcodeP;  
                        row.LOCNCODE = LOCNCODE;  
                        row.QTYSHPPD = QTYSHPPD;  
                        row.QTYDOKON = QTYDOKON;  
                        row.QTYPACK = QTYPACK;  
                        row.QTYPACKMJ = QTYPACKMJ;  
                        row.TIMEMODE = TIMEMODE;  
                        row.TIMEPREP = TIMEPREP;  
                        row.TIMEUNIT = TIMEUNIT;  
                        row.DtProdT = DtProdT; 
                        row.DtProdL = DtProdL;  
                        row.SerNumT = SerNumT;  
                        row.SerNumL = SerNumL;  
                        row.VerT = VerT;  
                        row.VerL = VerL;  
                        row.TermID = TermID;  
                        row.LSTMod = LSTMod;
                        row.BarcodeT = BarcodeT;

                        if (Realization_Start.HasValue)
                            row.Realization_Start = Realization_Start.Value;
                        else
                            row.SetRealization_StartNull();

                        if (Realization_Stop.HasValue)
                            row.Realization_Stop = Realization_Stop.Value;
                        else
                            row.SetRealization_StopNull();

                        row.CZ_REZ1_Track = CZ_REZ1_Track;
                        row.CZ_REZ2_Track = CZ_REZ2_Track;
                        row.CZ_REZ3_Track = CZ_REZ3_Track;
                        row.CZ_REZ4_Track = CZ_REZ4_Track;
                        row.CZ_REZ5_Track = CZ_REZ5_Track;

                        if (WEIGHT_TARA.HasValue)
                            row.WEIGHT_TARA = WEIGHT_TARA.Value;
                        else
                            row.SetWEIGHT_TARANull();

                        if (WEIGHT_NETTO.HasValue)
                            row.WEIGHT_NETTO = WEIGHT_NETTO.Value;
                        else
                            row.SetWEIGHT_NETTONull();

                        if (WEIGHT_TOL_PLUS.HasValue)
                            row.WEIGHT_TOL_PLUS = WEIGHT_TOL_PLUS.Value;
                        else
                            row.SetWEIGHT_TOL_PLUSNull();

                        if (WEIGHT_TOL_MINUS.HasValue)
                            row.WEIGHT_TOL_MINUS = WEIGHT_TOL_MINUS.Value;
                        else
                            row.SetWEIGHT_TOL_MINUSNull();


                        ((Fask.Interfaces.Vyroba.VPP.IVPP_Insert)provider).VPP_Insert_Row(row);


                        return Request.CreateResponse<int>(HttpStatusCode.OK, 0);
                    }
                    catch (Exception ex)
                    {
                        return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
                    }

                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
            }

        }

        [HttpPost]
        [Route("api/Konzola_VPP_Insert_Row")]
        public HttpResponseMessage Konzola_VPP_Insert_Row([FromBody] Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP FZ_data)
        {
            //TODO MaR zmenit metodu aby prijimala objekt!!
            //Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHRow row = null;

            Init();
            bool result = false;
            try
            {
                BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP, Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP_row, Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable, Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPRow> y = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP, Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP_row, Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable, Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPRow>();
                var dt = y.GetDTFromBO(FZ_data);

                if (provider != null && provider is Fask.Interfaces.Vyroba.VPP.IVPP_Insert)
                {
                    try
                    {
                        if (dt.Count == 1)
                        {
                            //dt[0].SetModified();
                            ((Fask.Interfaces.Vyroba.VPP.IVPP_Insert)provider).VPP_Insert_Row(dt.First());
                            result = true;
                        }
                        else
                        {
                            result = false;
                        }

                        return Request.CreateResponse<bool>(HttpStatusCode.OK, result);
                    }
                    catch (Exception ex)
                    {
                        return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
                    }

                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
            }

        }

        [HttpPost]
        [Route("api/Konzola_VPP_Update_Row")]
        public HttpResponseMessage Konzola_VPP_Update_Row([FromBody] Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP FZ_data)
        {
            Init();

            bool result = false;
            Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable dt = new Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable();

            if (provider != null && provider is Fask.Interfaces.Vyroba.VPP.IVPP_Update_Row)
            {
                try
                {
                    BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP, Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP_row, Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable, Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPRow> y = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP, Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP_row, Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable, Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPRow>();
                    dt = y.GetDTFromBO(FZ_data);

                    if (dt.Count == 1)
                    {

                        ((Fask.Interfaces.Vyroba.VPP.IVPP_Update_Row)provider).VPP_Update_Row(dt.First());
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }

                    return Request.CreateResponse<bool>(HttpStatusCode.OK, result);
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse<Exception>(HttpStatusCode.InternalServerError, ex);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        [Route("api/Konzola_GetFiltrovanyVPPList")]
        public HttpResponseMessage Konzola_GetFiltrovanyVPPList([FromBody] Fask.Interfaces.Filtry.Vyroba_VPP_Filtr filtr)
        {
            Init();

            Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP bo = new WEBAPI.API_BusinessObjects.BO_CZPRO_VPP();
            Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();

            if (provider != null && provider is Fask.Interfaces.Vyroba.VPP.IVPP_GetFiltrovanyVPPList)
            {
                try
                {
                    ds = ((Fask.Interfaces.Vyroba.VPP.IVPP_GetFiltrovanyVPPList)provider).GetFiltrovanyVPPList(filtr);

                    BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP, Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP_row, Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable, Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPRow> x = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP, Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP_row, Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable, Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPRow>();
                    bo = x.GetBOFromDT(ds.CZPRO_VPP);

                    return Request.CreateResponse<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP>(HttpStatusCode.OK, bo);
                }
                catch (Exception ex)
                {
                    //return Request.CreateResponse<string>(HttpStatusCode.BadRequest, ex.Message + ex.StackTrace);
                    return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
                }

            }
            else
                return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        #endregion

        #endregion

        #endregion
    }
}