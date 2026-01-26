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
	public class ExpediceController : ApiController
	{

		[HttpPost]
		[Route("api/Expedice/GenerateDavkaRequest_Expedice")]
		public HttpResponseMessage GenerateDavkaRequest_Expedice([FromBody] Expedice_row json_data)
		{
			BL.ExpediceBL expediceBL = new BL.ExpediceBL();
			expediceBL.Initialize(System.Web.Hosting.HostingEnvironment.MapPath);
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
				so = expediceBL.GenerateDavkaRequest(sopnumbe, skl_id);
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
		[Route("api/Expedice/GenerateDavkaStatus_Expedice")]
		public HttpResponseMessage GenerateDavkaStatus_Expedice([FromBody] Expedice_row json_data)
		{
			BL.ExpediceBL expediceBL = new BL.ExpediceBL();
			expediceBL.Initialize(System.Web.Hosting.HostingEnvironment.MapPath);
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
				so = expediceBL.GenerateDavkaStatus(sopnumbe, skl_id);
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
		[Route("api/Expedice/GenerateDavkaStatusDelete_Expedice")]
		public HttpResponseMessage GenerateDavkaStatusDelete_Expedice([FromBody] Expedice_row json_data)
		{
			BL.ExpediceBL expediceBL = new BL.ExpediceBL();
			expediceBL.Initialize(System.Web.Hosting.HostingEnvironment.MapPath);
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
				so = expediceBL.GenerateDavkaStatusDelete(sopnumbe, skl_id);
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