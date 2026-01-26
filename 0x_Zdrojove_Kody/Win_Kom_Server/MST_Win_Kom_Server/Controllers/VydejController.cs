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
    public class VydejController : ApiController
    {


		[HttpPost]
		[Route("api/Vydej/Vydejky")]
		public HttpResponseMessage Vydejky([FromBody] Vydejky_row json_data)
		{
			BL.VydejBL vydejBL = new BL.VydejBL();
			vydejBL.Initialize(System.Web.Hosting.HostingEnvironment.MapPath);

			int userid = 0;
			byte idterminal = 0;
			string itemtype = string.Empty;
			string prefixskladu = string.Empty;
			Fask.DataSets.Vydejky volnevydejky = new Fask.DataSets.Vydejky();



			try
			{


				if (json_data == null)
				{
					var message = String.Format("predavany objekt JSON nenalezen");
					Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info, message);
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, message);
					//return Request.CreateResponse<string>(HttpStatusCode.NoContent, "predavany objekt JSON nenalezen");
				}

				userid = json_data.userid;
				prefixskladu = json_data.prefixskladu;
				idterminal = json_data.idterminal;
				itemtype = json_data.itemtype;

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
				volnevydejky = vydejBL.GetVydejky(idterminal, prefixskladu, itemtype,userid);
				return Request.CreateResponse<Fask.DataSets.Vydejky>(System.Net.HttpStatusCode.OK, volnevydejky);
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
		[Route("api/Vydej/PrepareVydejkaDB")]
		public HttpResponseMessage PrepareVydejkaDB([FromBody] Vydejka_row json_data)
		{
			BL.VydejBL vydejBL = new BL.VydejBL();
			vydejBL.Initialize(System.Web.Hosting.HostingEnvironment.MapPath);
			int countentries = 0;
			byte idterminal = 0;
			string itemtype = string.Empty;
			bool vysledek = false;

			try
			{


				if (json_data == null)
				{
					var message = String.Format("predavany objekt JSON nenalezen");
					Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info, message);
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, message);
					//return Request.CreateResponse<string>(HttpStatusCode.NoContent, "predavany objekt JSON nenalezen");
				}

				 countentries = json_data.countentries;
				 idterminal = json_data.idterminal;
				 itemtype = json_data.itemtype;

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
                vysledek = vydejBL.PrepareVydejkaDB(countentries, idterminal, itemtype);
                return Request.CreateResponse<bool>(System.Net.HttpStatusCode.OK, vysledek);
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
		[Route("api/Vydej/GetVydejkaReceived")]
		public HttpResponseMessage GetVydejkaReceived([FromBody] Vydejka_row json_data)
		{
			BL.VydejBL vydejBL = new BL.VydejBL();
			vydejBL.Initialize(System.Web.Hosting.HostingEnvironment.MapPath);
			int countentries = 0;
			byte idterminal = 0;
			string itemtype = string.Empty;
			bool vysledek = false;

			try
			{


				if (json_data == null)
				{
					var message = String.Format("predavany objekt JSON nenalezen");
					Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info, message);
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, message);
					//return Request.CreateResponse<string>(HttpStatusCode.NoContent, "predavany objekt JSON nenalezen");
				}

				countentries = json_data.countentries;
				idterminal = json_data.idterminal;
				itemtype = json_data.itemtype;

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
				vysledek = vydejBL.GetVydejkaReceived(countentries, idterminal, itemtype);
				return Request.CreateResponse<bool>(System.Net.HttpStatusCode.OK, vysledek);
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
		[Route("api/Vydej/ProcessVydejkaDBFile2")]
		public HttpResponseMessage ProcessVydejkaDBFile2([FromBody] Vydejka_row json_data)
		{
			BL.VydejBL vydejBL = new BL.VydejBL();
			vydejBL.Initialize(System.Web.Hosting.HostingEnvironment.MapPath);
			int countentries = 0;
			byte idterminal = 0;
			string state = string.Empty;
			string itemtype = string.Empty;
           StatusObject vysledek = new StatusObject();


			// Vytvoření instance Stopwatch
			Stopwatch stopwatch = new Stopwatch();

			// Start měření času
			stopwatch.Start();

			Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info, "0: " + stopwatch.ElapsedMilliseconds.ToString());

			try
			{


				if (json_data == null)
				{
					var message = String.Format("predavany objekt JSON nenalezen");
					Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info, message);
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, message);
					//return Request.CreateResponse<string>(HttpStatusCode.NoContent, "predavany objekt JSON nenalezen");
				}

				countentries = json_data.countentries;
				idterminal = json_data.idterminal;
				state = json_data.state;
				itemtype = json_data.itemtype;

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

				vysledek = vydejBL.ProcessVydejkaDBFile2(countentries, idterminal, BL.VydejBL.ParseStringToEnum(state), itemtype);


				Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info, "1: " + stopwatch.ElapsedMilliseconds.ToString());

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


		[HttpPost]
		[Route("api/Vydej/GenerateDavkaRequest_Vydej")]
		public HttpResponseMessage GenerateDavkaRequest_Vydej([FromBody] Vydejka_row json_data)
		{
			BL.VydejBL vydejBL = new BL.VydejBL();
			vydejBL.Initialize(System.Web.Hosting.HostingEnvironment.MapPath);
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
				so = vydejBL.GenerateDavkaRequest(sopnumbe, skl_id);
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
		[Route("api/Vydej/GenerateDavkaStatus_Vydej")]
		public HttpResponseMessage GenerateDavkaStatus_Vydej([FromBody] Vydejka_row json_data)
		{
			BL.VydejBL vydejBL = new BL.VydejBL();
			vydejBL.Initialize(System.Web.Hosting.HostingEnvironment.MapPath);
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
				so = vydejBL.GenerateDavkaStatus(sopnumbe, skl_id);
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
		[Route("api/Vydej/GenerateDavkaStatusDelete_Vydej")]
		public HttpResponseMessage GenerateDavkaStatusDelete_Vydej([FromBody] Vydejka_row json_data)
		{
			BL.VydejBL vydejBL = new BL.VydejBL();
			vydejBL.Initialize(System.Web.Hosting.HostingEnvironment.MapPath);
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
				so = vydejBL.GenerateDavkaStatusDelete(sopnumbe, skl_id);
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
		[Route("api/Vydej/Online_Expirace_Verify")]
		public HttpResponseMessage Online_Expirace_Verify([FromBody] Vydejka_row json_data)
		{
			BL.VydejBL vydejBL = new BL.VydejBL();
			vydejBL.Initialize(System.Web.Hosting.HostingEnvironment.MapPath);

			//string itemnmbr, string sklad_id, string serltnum, DateTime? expirace
			string itemnmbr = string.Empty;
			string sklad_id = string.Empty;
			string serltnum = string.Empty;
			DateTime? expirace = null;
			StatusOverExpirace so = new StatusOverExpirace();


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
				 sklad_id = json_data.skl_id;
				 serltnum = json_data.serltnum;

				if(json_data.expirace.HasValue)
                {
					expirace = json_data.expirace.Value;
				}

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
				so = vydejBL.Online_Expirace_Verify( itemnmbr, sklad_id, serltnum, expirace);
				return Request.CreateResponse<StatusOverExpirace>(System.Net.HttpStatusCode.OK, so);
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

		/// <summary>
		/// Získání dat pro lokace
		/// </summary>
		/// <param name="json_data"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("api/Vydej/GenerateDataRequest_CZMST094_Vydej")]
		public HttpResponseMessage GenerateDataRequest_CZMST094_Vydej([FromBody] Vydejka_row json_data)
		{
			BL.VydejBL vydejBL = new BL.VydejBL();
			vydejBL.Initialize(System.Web.Hosting.HostingEnvironment.MapPath);
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
				so = vydejBL.GenerateDataRequest_CZMST094(sopnumbe,skl_id);
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
		[Route("api/Vydej/GenerateDataStatus_CZMST094_Vydej")]
		public HttpResponseMessage GenerateDataStatus_CZMST094_Vydej([FromBody] Vydejka_row json_data)
		{
			BL.VydejBL vydejBL = new BL.VydejBL();
			vydejBL.Initialize(System.Web.Hosting.HostingEnvironment.MapPath);
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
				so = vydejBL.GenerateDataStatus_CZMST094(sopnumbe, skl_id);
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
		[Route("api/Vydej/GenerateDataStatusDelete_CZMST094_Vydej")]
		public HttpResponseMessage GenerateDataStatusDelete_CZMST094([FromBody] Vydejka_row json_data)
		{
			BL.VydejBL vydejBL = new BL.VydejBL();
			vydejBL.Initialize(System.Web.Hosting.HostingEnvironment.MapPath);
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
				so = vydejBL.GenerateDataStatusDelete_CZMST094(sopnumbe, skl_id);
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