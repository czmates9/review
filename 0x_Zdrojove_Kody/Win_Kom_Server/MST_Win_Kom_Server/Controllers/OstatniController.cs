using Fask.Server.Interfaces.API_BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Fask.Server.Interfaces.Classes;
using System.Diagnostics;

namespace Fask.MST_W_Server.Controllers
{
    public class OstatniController : ApiController
	{




		[HttpPost]
		[Route("api/Ostatni/GenerateDavkaRequest_Ostatni")]
		public HttpResponseMessage GenerateDavkaRequest_Ostatni([FromBody] Ostatni_row json_data)
		{
			BL.OstatniBL ostatniBL = new BL.OstatniBL();
			ostatniBL.Initialize(System.Web.Hosting.HostingEnvironment.MapPath);
			string sopnumbe = string.Empty;
			string skl_id = string.Empty;
			StatusObject so = new StatusObject();


			try
			{


				if (json_data == null)
				{
					var message = String.Format("predavany objekt JSON nenalezen");
					Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info, message);
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, message);
					//return Request.CreateResponse<string>(HttpStatusCode.NoContent, "predavany objekt JSON nenalezen");
				}

				sopnumbe = json_data.sopnumbe;
				skl_id = json_data.skl_id;

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
				so = ostatniBL.GenerateDavkaRequest(sopnumbe, skl_id);
				return Request.CreateResponse<StatusObject>(System.Net.HttpStatusCode.OK, so);
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

		[HttpPost]
		[Route("api/Ostatni/GenerateDavkaStatus_Ostatni")]
		public HttpResponseMessage GenerateDavkaStatus_Ostatni([FromBody] Ostatni_row json_data)
		{
			BL.OstatniBL ostatniBL = new BL.OstatniBL();
			ostatniBL.Initialize(System.Web.Hosting.HostingEnvironment.MapPath);
			string sopnumbe = string.Empty;
			string skl_id = string.Empty;
			StatusObject so = new StatusObject();


			try
			{


				if (json_data == null)
				{
					var message = String.Format("predavany objekt JSON nenalezen");
					Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info, message);
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, message);
					//return Request.CreateResponse<string>(HttpStatusCode.NoContent, "predavany objekt JSON nenalezen");
				}

				sopnumbe = json_data.sopnumbe;
				skl_id = json_data.skl_id;

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
				so = ostatniBL.GenerateDavkaStatus(sopnumbe, skl_id);
				return Request.CreateResponse<StatusObject>(System.Net.HttpStatusCode.OK, so);
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

		[HttpPost]
		[Route("api/Ostatni/GenerateDavkaStatusDelete_Ostatni")]
		public HttpResponseMessage GenerateDavkaStatusDelete_Ostatni([FromBody] Ostatni_row json_data)
		{
			BL.OstatniBL ostatniBL = new BL.OstatniBL();
			ostatniBL.Initialize(System.Web.Hosting.HostingEnvironment.MapPath);
			string sopnumbe = string.Empty;
			string skl_id = string.Empty;
			StatusObject so = new StatusObject();


			try
			{


				if (json_data == null)
				{
					var message = String.Format("predavany objekt JSON nenalezen");
					Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info, message);
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, message);
					//return Request.CreateResponse<string>(HttpStatusCode.NoContent, "predavany objekt JSON nenalezen");
				}

				sopnumbe = json_data.sopnumbe;
				skl_id = json_data.skl_id;

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
				so = ostatniBL.GenerateDavkaStatusDelete(sopnumbe, skl_id);
				return Request.CreateResponse<StatusObject>(System.Net.HttpStatusCode.OK, so);
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