using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Fask.MST_W_Server.API_BusinessObjects;
using Fask.Server.Interfaces.Classes;
using Fask.WEBAPI.API_BusinessObjects;

namespace Fask.MST_W_Server.Controllers
{
    public class UzivatelController : ApiController
    {


		[HttpPost]
		[Route("api/Uzivatel/GetKatalogUzivatele")]
		public HttpResponseMessage GetKatalogUzivatele([FromBody] UzivatelJSON_row json_data)
		{
			BL.UzivatelBL uzivatelBL = new BL.UzivatelBL();
			uzivatelBL.Initialize(System.Web.Hosting.HostingEnvironment.MapPath);

			string loginid = string.Empty;
			int countentries = 0;
			byte idterminal = 0;
			int userID = 0;
			StatusObject vysledek = new StatusObject();



			try
			{


				if (json_data == null)
				{
					var message = String.Format("predavany objekt JSON nenalezen");
					Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info, message);
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, message);
					//return Request.CreateResponse<string>(HttpStatusCode.NoContent, "predavany objekt JSON nenalezen");
				}


				//loginid = json_data.loginid;
				//countentries = json_data.countentries;
				idterminal = json_data.idterminal;
				//userID = json_data.userID;

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
				vysledek = uzivatelBL.GetKatalogUzivatele(idterminal);
				return Request.CreateResponse<StatusObject>(System.Net.HttpStatusCode.OK, vysledek);
			}
			catch (Exception ex)
			{

				Logging.ExceptionHandler2.Handle(ex);
				var message = ex.Message;
				//
				Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info, message);
				return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);

			}

		}









	}

}