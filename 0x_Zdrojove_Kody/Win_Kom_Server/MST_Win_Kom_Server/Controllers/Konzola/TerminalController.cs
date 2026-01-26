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

using Fask.Interfaces.Terminal;
using Fask.WEBAPI;

namespace Fask.MST_W_Server.Controllers
{
    public class TerminalController : ApiController
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
								if (typeof(Fask.Interfaces.Terminal.ITerminal).IsAssignableFrom(t))
								{
									provider = (Fask.Interfaces.Terminal.ITerminal)providerAssemlby.CreateInstance(t.FullName);
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
        [Route("api/Konzola_GetTerminalAll")]
        public HttpResponseMessage Konzola_GetTerminalAll()
        {
            Init();
            try
            {
                Fask.WEBAPI.API_BusinessObjects.BO_CZMST_TERMINAL_ALL bo = new Fask.WEBAPI.API_BusinessObjects.BO_CZMST_TERMINAL_ALL();
                Fask.Interfaces.DataSets.Terminal.CZMST_TERMINAL_ALLDataTable dt = new Fask.Interfaces.DataSets.Terminal.CZMST_TERMINAL_ALLDataTable();
                Fask.Interfaces.DataSets.Terminal ds = new Fask.Interfaces.DataSets.Terminal();

               
                if (provider != null && provider is ITerminal_GetTerminalAll)
                {
                    try
                    {
                        ds = ((ITerminal_GetTerminalAll)provider).GetTerminalAll();

                        BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZMST_TERMINAL_ALL, Fask.WEBAPI.API_BusinessObjects.BO_CZMST_TERMINAL_ALL_row, Fask.Interfaces.DataSets.Terminal.CZMST_TERMINAL_ALLDataTable, Fask.Interfaces.DataSets.Terminal.CZMST_TERMINAL_ALLRow> x = new BusinessObject<
                            Fask.WEBAPI.API_BusinessObjects.BO_CZMST_TERMINAL_ALL, 
                            Fask.WEBAPI.API_BusinessObjects.BO_CZMST_TERMINAL_ALL_row, 
                            Fask.Interfaces.DataSets.Terminal.CZMST_TERMINAL_ALLDataTable, 
                            Fask.Interfaces.DataSets.Terminal.CZMST_TERMINAL_ALLRow>();

                        bo = x.GetBOFromDT(ds.CZMST_TERMINAL_ALL);


                        return Request.CreateResponse<Fask.WEBAPI.API_BusinessObjects.BO_CZMST_TERMINAL_ALL>(HttpStatusCode.OK, bo);
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
        [Route("api/Konfigurace_GetDataByID")]
        public HttpResponseMessage Konfigurace_GetDataByID([FromBody] object FZ_data)
        {


            Init();
            //int result;

            //request neni inicializovan!!!
            HttpResponseMessage msg = new HttpResponseMessage();
            //msg.RequestMessage.CreateResponse();

            try
            {
              

                #region parametry z URL
                int iD_TERMINAL;
               


                // Zdrojove URL, resp. parametry z něho
                string URL_param = Request.RequestUri.Query;

                //list parametru
                var SeznamParametru = HttpUtility.ParseQueryString(URL_param);

                var iD_TERMINALtmp = SeznamParametru.Get("iD_TERMINAL");
                iD_TERMINAL = Int32.Parse(iD_TERMINALtmp);

                
                #endregion
                //object konf_tmp = "konfigurace pro terminal";
                object konf_tmp = null;
                GlobalKonfigurace aa = new GlobalKonfigurace();

                konf_tmp = aa.VratKonfiguraci(iD_TERMINAL);

                if(konf_tmp != null)
                    return Request.CreateResponse<object>(HttpStatusCode.OK, konf_tmp);
                else
                    return Request.CreateResponse<object>(HttpStatusCode.NotFound, konf_tmp);
            }
            catch (Exception ex)
            {
                //return msg.RequestMessage.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
                return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
            }

        }

        [HttpPost]
        [Route("api/KonfiguraceTexty_GetDataByID")]
        public HttpResponseMessage KonfiguraceTexty_GetDataByID([FromBody] object FZ_data)
        {


            Init();
            //int result;

            //request neni inicializovan!!!
            HttpResponseMessage msg = new HttpResponseMessage();
            //msg.RequestMessage.CreateResponse();

            try
            {


                #region parametry z URL
                int iD_TERMINAL;



                // Zdrojove URL, resp. parametry z něho
                string URL_param = Request.RequestUri.Query;

                //list parametru
                var SeznamParametru = HttpUtility.ParseQueryString(URL_param);

                var iD_TERMINALtmp = SeznamParametru.Get("iD_TERMINAL");
                iD_TERMINAL = Int32.Parse(iD_TERMINALtmp);


                #endregion
                //object konf_tmp = "konfigurace pro terminal";
                object konf_tmp = null;
                GlobalKonfigurace aa = new GlobalKonfigurace();

                konf_tmp = aa.VratKonfiguraciTexty(iD_TERMINAL);

                if (konf_tmp != null)
                    return Request.CreateResponse<object>(HttpStatusCode.OK, konf_tmp);
                else
                    return Request.CreateResponse<object>(HttpStatusCode.NotFound, konf_tmp);
            }
            catch (Exception ex)
            {
                //return msg.RequestMessage.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
                return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
            }

        }
    }
}