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
using Fask.Interfaces.Filtry;

namespace Fask.MST_W_Server.Controllers
{
    public class SkladyController : ApiController
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


        [HttpPost]
        [Route("api/Konzola_Prodej_GetFiltrovaneDavky")]
        public HttpResponseMessage Prodej_GetFiltrovaneDavky(ProdejFiltr filtr)
        {
            Fask.WEBAPI.API_BusinessObjects.BO_CZMST_DI bo = new Fask.WEBAPI.API_BusinessObjects.BO_CZMST_DI();
            Fask.Interfaces.DataSets.Prodej.CZMST_DIDataTable dt = new Fask.Interfaces.DataSets.Prodej.CZMST_DIDataTable();
            Fask.Interfaces.DataSets.Prodej ds = new Fask.Interfaces.DataSets.Prodej();

            Init();

            if (provider != null && provider is Fask.Interfaces.Prodej.IProdej2_Prodej_GetFiltrovaneDavky)
            {
                try
                {
                    ds = ((Fask.Interfaces.Prodej.IProdej2_Prodej_GetFiltrovaneDavky)provider).Prodej_GetFiltrovaneDavky(filtr);

                    BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZMST_DI, Fask.WEBAPI.API_BusinessObjects.BO_CZMST_DI_row, Fask.Interfaces.DataSets.Prodej.CZMST_DIDataTable, Fask.Interfaces.DataSets.Prodej.CZMST_DIRow> x = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZMST_DI, Fask.WEBAPI.API_BusinessObjects.BO_CZMST_DI_row, Fask.Interfaces.DataSets.Prodej.CZMST_DIDataTable, Fask.Interfaces.DataSets.Prodej.CZMST_DIRow>();

                    bo = x.GetBOFromDT(ds.CZMST_DI);


                    return Request.CreateResponse<Fask.WEBAPI.API_BusinessObjects.BO_CZMST_DI>(HttpStatusCode.OK, bo);


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
    }
}