using Fask.Server.Interfaces.API_BO;
using Fask.Tracing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Fask.MST_W_Server.Controllers
{
    public class InformationsController : ApiController
    {


        /// <summary>
        /// pro ID zbozi a ID skladu vrati mnozstvi zasoby
        /// </summary>
        /// <param name="ItemNumber">mnozstvi zasoby</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Informations/MnozstviNaSklade")]
        public HttpResponseMessage MnozstviNaSklade([FromBody] Polozka_row json_data)
        {
            BL.InformationsBL informationsBL = new BL.InformationsBL();
            informationsBL.Initialize(System.Web.Hosting.HostingEnvironment.MapPath);
            string idZbozi = string.Empty;
            string idSklad = string.Empty;
            float? vysledek = null;

            try
            {


                if (json_data == null)
                {
                    var message = String.Format("predavany objekt JSON nenalezen");
                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info, message);
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, message);
                    //return Request.CreateResponse<string>(HttpStatusCode.NoContent, "predavany objekt JSON nenalezen");
                }

                idZbozi = json_data.idZbozi;
                idSklad = json_data.idSklad;

            }
            catch (Exception ex)
            {

                Logging.ExceptionHandler2.Handle(ex);
                var message = String.Format("CATCH -- predavany objekt JSON nenalezen");
                //
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info, message);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);

                //return Request.CreateResponse<string>(HttpStatusCode.NoContent, "predavany objekt JSON nenalezen");
            }


            try
            {
                vysledek = informationsBL.MnozstviNaSklade_Itemnumber_Location(idZbozi, idSklad);

                if (vysledek == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent);
                }
                else
                {
                    return Request.CreateResponse<float?>(HttpStatusCode.OK, vysledek);
                }
                
            }
            catch (Exception ex)
            {

                Logging.ExceptionHandler2.Handle(ex);
                var message = ex.Message;
                //
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info, message);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);

            }

            // todo : inplement
            // return -100.22F;
        }



        [HttpPost]
        [Route("api/Informations/OnlineGetFEFOFIFO")]
        public HttpResponseMessage OnlineGetFEFOFIFO([FromBody] Polozka_row json_data)
        {
            BL.InformationsBL informationsBL = new BL.InformationsBL();
            informationsBL.Initialize(System.Web.Hosting.HostingEnvironment.MapPath);
            string itemnmbr = string.Empty;
            string skl_id = string.Empty;
            string serltnum = string.Empty;
            string doc_id = string.Empty;
            string locncode = string.Empty;

            Fask.Server.Interfaces.DataSets.Location vysledek = new Fask.Server.Interfaces.DataSets.Location();
            //float vysledek = 0;
            //string itemnmbr, string skl_id, string serltnum, string doc_id, string locncode

            try
            {


                if (json_data == null)
                {
                    var message = String.Format("predavany objekt JSON nenalezen");
                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info, message);
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, message);
                    //return Request.CreateResponse<string>(HttpStatusCode.NoContent, "predavany objekt JSON nenalezen");
                }

                 itemnmbr = json_data.itemnmbr;
                 skl_id = json_data.idSklad;
                serltnum = json_data.serltnum;
                doc_id = json_data.doc_id;
                locncode = json_data.locncode;

                var messageData = String.Format(
     "-- Předávaný objekt JSON data: itemnmbr={0}; idSklad={1}; serltnum={2}; doc_id={3}; locncode={4};",
     itemnmbr, skl_id, serltnum, doc_id, locncode
 );

                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info, messageData);


            }
            catch (Exception ex)
            {

                Logging.ExceptionHandler2.Handle(ex);
                var message = String.Format("CATCH -- predavany objekt JSON nenalezen");
                //
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info, message);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);

                //return Request.CreateResponse<string>(HttpStatusCode.NoContent, "predavany objekt JSON nenalezen");
            }


            try
            {
                vysledek = informationsBL.Online_GetMaterial(itemnmbr, skl_id, serltnum, doc_id, locncode);

                

                return Request.CreateResponse<Fask.Server.Interfaces.DataSets.Location>(HttpStatusCode.OK, vysledek);
            }
            catch (Exception ex)
            {

                Logging.ExceptionHandler2.Handle(ex);
                var message = ex.Message;
                //
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info, message);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);

            }

            // todo : inplement
            // return -100.22F;
        }




    }
}