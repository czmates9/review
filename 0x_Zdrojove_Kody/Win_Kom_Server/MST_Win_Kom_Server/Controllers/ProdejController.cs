using Fask.Server.Interfaces.API_BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Fask.Server.Interfaces.Classes;
using Fask.WEBAPI.API_BusinessObjects;

namespace Fask.MST_W_Server.Controllers
{
	public class ProdejController : ApiController
	{
		[HttpGet]
		[Route("api/Prodej/Disponibilita")]
		public HttpResponseMessage Disponibilita()
		{
			return Request.CreateResponse<Disponibilita>(System.Net.HttpStatusCode.OK, new Fask.Server.Interfaces.Classes.Disponibilita());
		}

		[HttpPost]
		[Route("api/Prodej/Disponibilita")]
		public HttpResponseMessage Disponibilita([FromBody] ProdejJSON_row prodejFile)
		{
			//BL.ProdejBL prodejBL = new BL.ProdejBL();
			//prodejBL.Initialize(System.Web.Hosting.HostingEnvironment.MapPath);
   //         StatusResult statusResult = prodejBL.ProcessProdejFile(prodejFile.countentries, prodejFile.idterminal, prodejFile.userID, prodejFile.login, prodejFile.dstFile);
   //         return Request.CreateResponse<StatusResult>(System.Net.HttpStatusCode.OK, statusResult);



			return null;
			//provider = prodejBL.provider;

			//// ? provider 
			//var statusresult = new BL.ProdejBL(provider).KLAJSDLKJFASD(disponibilita);

			//Fask.Server.Interfaces.Classes.StatusResult statusResult = new StatusResult();
			//statusResult.Status = StatusResultEnum.ERROR;
			//statusResult.Message = "Initialized status...";

			//return Request.CreateResponse<StatusResult>(System.Net.HttpStatusCode.OK, statusResult, System.Net.Http.Formatting.JsonMediaTypeFormatter.DefaultMediaType);

   //         if ((provider != null) && (provider is Fask.Server.Interfaces.Prodej.IProdej))
   //         {
   //             var sidisp = provider.Prodej_Disponibilita(disponibilita);

   //             statusResult.Status = (StatusResultEnum)sidisp.ID;
   //             statusResult.Message = sidisp.Description;
   //             return statusResult;
   //         }


   //         string msg = "Provider v Prodej.asmx > 'Disponibilita(string itemnmbr, string SKL_ID, string LOCNCODE, decimal QTY)' nenastaven.";
			//Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
			//throw new Exception(msg);


		}

		[HttpPost]
		[Route("api/Prodej/ProcessProdejFile")]
		public HttpResponseMessage ProcessProdejFile([FromBody] Disponibilita disponibilita)
		{
			BL.ProdejBL prodejBL = new BL.ProdejBL();
			prodejBL.Initialize(System.Web.Hosting.HostingEnvironment.MapPath);
			StatusResult statusResult = prodejBL.Disponibilita(disponibilita);
			return Request.CreateResponse<StatusResult>(System.Net.HttpStatusCode.OK, statusResult);
			//provider = prodejBL.provider;

			//// ? provider 
			//var statusresult = new BL.ProdejBL(provider).KLAJSDLKJFASD(disponibilita);

			//Fask.Server.Interfaces.Classes.StatusResult statusResult = new StatusResult();
			//statusResult.Status = StatusResultEnum.ERROR;
			//statusResult.Message = "Initialized status...";

			//return Request.CreateResponse<StatusResult>(System.Net.HttpStatusCode.OK, statusResult, System.Net.Http.Formatting.JsonMediaTypeFormatter.DefaultMediaType);

			//         if ((provider != null) && (provider is Fask.Server.Interfaces.Prodej.IProdej))
			//         {
			//             var sidisp = provider.Prodej_Disponibilita(disponibilita);

			//             statusResult.Status = (StatusResultEnum)sidisp.ID;
			//             statusResult.Message = sidisp.Description;
			//             return statusResult;
			//         }


			//         string msg = "Provider v Prodej.asmx > 'Disponibilita(string itemnmbr, string SKL_ID, string LOCNCODE, decimal QTY)' nenastaven.";
			//Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
			//throw new Exception(msg);


		}

		[HttpPost]
		[Route("api/Prodej/ProcessProdejDB2")]
		public HttpResponseMessage ProcessProdejDB2([FromBody] ProdejJSON_row json_data)
		{
			BL.ProdejBL prodejBL = new BL.ProdejBL();
			prodejBL.Initialize(System.Web.Hosting.HostingEnvironment.MapPath);

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

				loginid = json_data.loginid;
				countentries = json_data.countentries;
				idterminal = json_data.idterminal;
				userID = json_data.userID;

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
				vysledek = prodejBL.ProcessProdejDB2(countentries, idterminal, userID, loginid);
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