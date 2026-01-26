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
    public class IT_castController : ApiController
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

        [HttpGet]
        [Route("api/Konzola_Fask_Events_ArchivaceProcedura")]
        public HttpResponseMessage CallProcedura()
        {
            Init();


            try
            {
                #region parametry z URL
                DateTime? OD = null;
                DateTime? DO = null;
                Fask.WEBAPI.API_BusinessObjects.BO_Fask_Events_Archivace bo = new Fask.WEBAPI.API_BusinessObjects.BO_Fask_Events_Archivace();


                // Zdrojove URL, resp. parametry z něho
                string URL_param = Request.RequestUri.Query;

                //list parametru
                var SeznamParametru = HttpUtility.ParseQueryString(URL_param);

                var ODtmp = SeznamParametru.Get("OD");
                OD = DateTime.Parse(ODtmp, System.Globalization.DateTimeFormatInfo.InvariantInfo);

                var DOtmp = SeznamParametru.Get("DO");
                DO = DateTime.Parse(DOtmp, System.Globalization.DateTimeFormatInfo.InvariantInfo);

                
                #endregion

                if (provider != null && provider is Fask.Interfaces.IT_cast.IIT_cast_Events_Archivace)
                {
                    try
                    {
                        bo.pocetZaznamu = ((Fask.Interfaces.IT_cast.IIT_cast_Events_Archivace)provider).ArchivaceProcedura(OD, DO);
                        return Request.CreateResponse<Fask.WEBAPI.API_BusinessObjects.BO_Fask_Events_Archivace>(HttpStatusCode.OK, bo);
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
        [Route("api/Konzola_Fask_Events_Archivace_id")]
        public HttpResponseMessage Fask_Events_CallProcedura_id()
        {
            Init();


            try
            {
                #region parametry z URL
                int pom_id = 0;
                Fask.WEBAPI.API_BusinessObjects.BO_Fask_Events_Archivace bo = new Fask.WEBAPI.API_BusinessObjects.BO_Fask_Events_Archivace();


                // Zdrojove URL, resp. parametry z něho
                string URL_param = Request.RequestUri.Query;

                //list parametru
                var SeznamParametru = HttpUtility.ParseQueryString(URL_param);

                var IDtmp = SeznamParametru.Get("pom_id");
                pom_id = int.Parse(IDtmp);

              

                #endregion

                if (provider != null && provider is Fask.Interfaces.IT_cast.IIT_cast_Events_Archivace)
                {
                    try
                    {
                        bo.pocetZaznamu = ((Fask.Interfaces.IT_cast.IIT_cast_Events_Archivace)provider).Archivace_Fask_Event_id(pom_id);
                        return Request.CreateResponse<Fask.WEBAPI.API_BusinessObjects.BO_Fask_Events_Archivace>(HttpStatusCode.OK, bo);
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
        [Route("api/Konzola_Fask_Production_ArchivaceProcedura")]
        public HttpResponseMessage Production_CallProcedura()
        {
            Init();


            try
            {
                #region parametry z URL
                DateTime? OD = null;
                DateTime? DO = null;
                Fask.WEBAPI.API_BusinessObjects.BO_Production_Archivace bo = new Fask.WEBAPI.API_BusinessObjects.BO_Production_Archivace();


                // Zdrojove URL, resp. parametry z něho
                string URL_param = Request.RequestUri.Query;

                //list parametru
                var SeznamParametru = HttpUtility.ParseQueryString(URL_param);

                var ODtmp = SeznamParametru.Get("OD");
                OD = DateTime.Parse(ODtmp, System.Globalization.DateTimeFormatInfo.InvariantInfo);

                var DOtmp = SeznamParametru.Get("DO");
                DO = DateTime.Parse(DOtmp, System.Globalization.DateTimeFormatInfo.InvariantInfo);


                #endregion

                if (provider != null && provider is Fask.Interfaces.IT_cast.IIT_cast_Production_Archivace)
                {
                    try
                    {
                        bo.pocetZaznamu = ((Fask.Interfaces.IT_cast.IIT_cast_Production_Archivace)provider).Production_ArchivaceProcedura(OD, DO);
                        return Request.CreateResponse<Fask.WEBAPI.API_BusinessObjects.BO_Production_Archivace>(HttpStatusCode.OK, bo);
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
        [Route("api/Konzola_Fask_Production_ArchivaceProcedura_guid")]
        public HttpResponseMessage Production_CallProcedura_guid()
        {
            Init();


            try
            {
                #region parametry z URL
                Guid pom_guid;
                Fask.WEBAPI.API_BusinessObjects.BO_Production_Archivace bo = new Fask.WEBAPI.API_BusinessObjects.BO_Production_Archivace();


                // Zdrojove URL, resp. parametry z něho
                string URL_param = Request.RequestUri.Query;

                //list parametru
                var SeznamParametru = HttpUtility.ParseQueryString(URL_param);

                var IDtmp = SeznamParametru.Get("pom_guid");
                pom_guid = Guid.Parse(IDtmp);


                #endregion

                if (provider != null && provider is Fask.Interfaces.IT_cast.IIT_cast_Production_Archivace)
                {
                    try
                    {
                        bo.pocetZaznamu = ((Fask.Interfaces.IT_cast.IIT_cast_Production_Archivace)provider).Production_ArchivaceProcedura_guid(pom_guid);
                        return Request.CreateResponse<Fask.WEBAPI.API_BusinessObjects.BO_Production_Archivace>(HttpStatusCode.OK, bo);
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

    }
}