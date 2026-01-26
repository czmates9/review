using Fask.Server.Interfaces.API_BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Fask.Server.Interfaces.Classes;

namespace Fask.MST_W_Server.Controllers
{
    public class CiselnikyController : ApiController
    {

		[HttpPost]
		[Route("api/Ciselniky/ZboziDBPrepare")]
		public HttpResponseMessage KatalogZboziDBPrepare([FromBody] Ciselniky_row json_data)
		{
			BL.CiselnikyBL ciselnikyBL = new BL.CiselnikyBL();
			ciselnikyBL.Initialize(System.Web.Hosting.HostingEnvironment.MapPath);

			byte idterminal = 0;
			string prefixskladu = string.Empty;
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

				prefixskladu = json_data.prefixskladu;
				idterminal = json_data.idterminal;

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
				vysledek = ciselnikyBL.KatalogZboziDBPrepare(idterminal, prefixskladu);
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
		[Route("api/Ciselniky/StrediskaDBPrepare")]
		public HttpResponseMessage KatalogStrediskaDBPrepare([FromBody] Ciselniky_row json_data)
		{
			BL.CiselnikyBL ciselnikyBL = new BL.CiselnikyBL();
			ciselnikyBL.Initialize(System.Web.Hosting.HostingEnvironment.MapPath);

			byte idterminal = 0;
			string prefixskladu = string.Empty;
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

				prefixskladu = json_data.prefixskladu;
				idterminal = json_data.idterminal;

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
				vysledek = ciselnikyBL.KatalogStrediskaDBPrepare(idterminal, prefixskladu);
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
		[Route("api/Ciselniky/OdberateleDBPrepare")]
		public HttpResponseMessage KatalogOdberateleDBPrepare([FromBody] Ciselniky_row json_data)
		{
			BL.CiselnikyBL ciselnikyBL = new BL.CiselnikyBL();
			ciselnikyBL.Initialize(System.Web.Hosting.HostingEnvironment.MapPath);

			byte idterminal = 0;
			string prefixskladu = string.Empty;
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

				prefixskladu = json_data.prefixskladu;
				idterminal = json_data.idterminal;

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
				vysledek = ciselnikyBL.KatalogOdberateleDBPrepare(idterminal, prefixskladu);
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
		[Route("api/Ciselniky/TypDokladuDBPrepare")]
		public HttpResponseMessage KatalogTypDokladuDBPrepare([FromBody] Ciselniky_row json_data)
		{
			BL.CiselnikyBL ciselnikyBL = new BL.CiselnikyBL();
			ciselnikyBL.Initialize(System.Web.Hosting.HostingEnvironment.MapPath);

			byte idterminal = 0;
			string prefixskladu = string.Empty;
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

				prefixskladu = json_data.prefixskladu;
				idterminal = json_data.idterminal;

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
				vysledek = ciselnikyBL.KatalogTypDokladuDBPrepare(idterminal, prefixskladu);
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
		[Route("api/Ciselniky/PracovniciDBPrepare")]
		public HttpResponseMessage KatalogPracovniciDBPrepare([FromBody] Ciselniky_row json_data)
		{
			BL.CiselnikyBL ciselnikyBL = new BL.CiselnikyBL();
			ciselnikyBL.Initialize(System.Web.Hosting.HostingEnvironment.MapPath);

			byte idterminal = 0;
			string prefixskladu = string.Empty;
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

				prefixskladu = json_data.prefixskladu;
				idterminal = json_data.idterminal;

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
				vysledek = ciselnikyBL.KatalogPracovniciDBPrepare(idterminal, prefixskladu);
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
		[Route("api/Ciselniky/MenyDBPrepare")]
		public HttpResponseMessage KatalogMenyDBPrepare([FromBody] Ciselniky_row json_data)
		{
			BL.CiselnikyBL ciselnikyBL = new BL.CiselnikyBL();
			ciselnikyBL.Initialize(System.Web.Hosting.HostingEnvironment.MapPath);

			byte idterminal = 0;
			string prefixskladu = string.Empty;
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

				prefixskladu = json_data.prefixskladu;
				idterminal = json_data.idterminal;

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
				vysledek = ciselnikyBL.KatalogMenyDBPrepare(idterminal);
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
		[Route("api/Ciselniky/SkladyDBPrepare")]
		public HttpResponseMessage KatalogSkladyDBPrepare([FromBody] Ciselniky_row json_data)
		{
			BL.CiselnikyBL ciselnikyBL = new BL.CiselnikyBL();
			ciselnikyBL.Initialize(System.Web.Hosting.HostingEnvironment.MapPath);

			byte idterminal = 0;
			string prefixskladu = string.Empty;
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

				prefixskladu = json_data.prefixskladu;
				idterminal = json_data.idterminal;

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
				vysledek = ciselnikyBL.KatalogSkladyDBPrepare(idterminal);
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
		[Route("api/Ciselniky/LokaceDBPrepare")]
		public HttpResponseMessage KatalogLokaceDBPrepare([FromBody] Ciselniky_row json_data)
		{
			BL.CiselnikyBL ciselnikyBL = new BL.CiselnikyBL();
			ciselnikyBL.Initialize(System.Web.Hosting.HostingEnvironment.MapPath);

			byte idterminal = 0;
			string prefixskladu = string.Empty;
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

				prefixskladu = json_data.prefixskladu;
				idterminal = json_data.idterminal;

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
				vysledek = ciselnikyBL.KatalogLokaceDBPrepare(idterminal, prefixskladu);
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
		[Route("api/Ciselniky/EventTypesDBPrepare")]
		public HttpResponseMessage KatalogEventTypesDBPrepare([FromBody] Ciselniky_row json_data)
		{
			BL.CiselnikyBL ciselnikyBL = new BL.CiselnikyBL();
			ciselnikyBL.Initialize(System.Web.Hosting.HostingEnvironment.MapPath);

			byte idterminal = 0;
			string prefixskladu = string.Empty;
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

				prefixskladu = json_data.prefixskladu;
				idterminal = json_data.idterminal;

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
				vysledek = ciselnikyBL.KatalogEventTypesDBPrepare(idterminal);
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
		[Route("api/Ciselniky/TiskarnyDBPrepare")]
		public HttpResponseMessage KatalogTiskarnyDBPrepare([FromBody] Ciselniky_row json_data)
		{
			BL.CiselnikyBL ciselnikyBL = new BL.CiselnikyBL();
			ciselnikyBL.Initialize(System.Web.Hosting.HostingEnvironment.MapPath);

			byte idterminal = 0;
			string prefixskladu = string.Empty;
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

				prefixskladu = json_data.prefixskladu;
				idterminal = json_data.idterminal;

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
				vysledek = ciselnikyBL.KatalogTiskarnyDBPrepare(idterminal);
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


        #region exporty

        [HttpPost]
        [Route("api/Ciselniky/KatalogZboziExport")]
        public HttpResponseMessage KatalogZboziExport([FromBody] Ciselniky_row json_data)
        {
            BL.CiselnikyBL ciselnikyBL = new BL.CiselnikyBL();
            ciselnikyBL.Initialize(System.Web.Hosting.HostingEnvironment.MapPath);

            byte idterminal = 0;
            string prefixskladu = string.Empty;
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

                prefixskladu = json_data.prefixskladu;
                idterminal = json_data.idterminal;

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
                vysledek = ciselnikyBL.KatalogZboziExport(idterminal, prefixskladu);
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
        [Route("api/Ciselniky/KatalogSkladyExport")]
        public HttpResponseMessage KatalogSkladyExport([FromBody] Ciselniky_row json_data)
        {
            BL.CiselnikyBL ciselnikyBL = new BL.CiselnikyBL();
            ciselnikyBL.Initialize(System.Web.Hosting.HostingEnvironment.MapPath);

            byte idterminal = 0;
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

                idterminal = json_data.idterminal;

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
                vysledek = ciselnikyBL.KatalogSkladyExport(idterminal);
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
        [Route("api/Ciselniky/KatalogMenyExport")]
        public HttpResponseMessage KatalogMenyExport([FromBody] Ciselniky_row json_data)
        {
            BL.CiselnikyBL ciselnikyBL = new BL.CiselnikyBL();
            ciselnikyBL.Initialize(System.Web.Hosting.HostingEnvironment.MapPath);

            byte idterminal = 0;
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

                idterminal = json_data.idterminal;

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
                vysledek = ciselnikyBL.KatalogMenyExport(idterminal);
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
        [Route("api/Ciselniky/KatalogPracovniciExport")]
        public HttpResponseMessage KatalogPracovniciExport([FromBody] Ciselniky_row json_data)
        {
            BL.CiselnikyBL ciselnikyBL = new BL.CiselnikyBL();
            ciselnikyBL.Initialize(System.Web.Hosting.HostingEnvironment.MapPath);

            byte idterminal = 0;
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

                idterminal = json_data.idterminal;

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
                vysledek = ciselnikyBL.KatalogPracovniciExport(idterminal);
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
        [Route("api/Ciselniky/KatalogStrediskaExport")]
        public HttpResponseMessage KatalogStrediskaExport([FromBody] Ciselniky_row json_data)
        {
            BL.CiselnikyBL ciselnikyBL = new BL.CiselnikyBL();
            ciselnikyBL.Initialize(System.Web.Hosting.HostingEnvironment.MapPath);

            byte idterminal = 0;
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

                idterminal = json_data.idterminal;

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
                vysledek = ciselnikyBL.KatalogStrediskaExport(idterminal);
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
        [Route("api/Ciselniky/KatalogOdberateleExport")]
        public HttpResponseMessage KatalogOdberateleExport([FromBody] Ciselniky_row json_data)
        {
            BL.CiselnikyBL ciselnikyBL = new BL.CiselnikyBL();
            ciselnikyBL.Initialize(System.Web.Hosting.HostingEnvironment.MapPath);

            byte idterminal = 0;
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

                idterminal = json_data.idterminal;

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
                vysledek = ciselnikyBL.KatalogOdberateleExport(idterminal);
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
		[Route("api/Ciselniky/KatalogLokaceExport")]
		public HttpResponseMessage KatalogLokaceExport([FromBody] Ciselniky_row json_data)
		{
			BL.CiselnikyBL ciselnikyBL = new BL.CiselnikyBL();
			ciselnikyBL.Initialize(System.Web.Hosting.HostingEnvironment.MapPath);

			byte idterminal = 0;
			string prefixskladu = string.Empty;
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

				prefixskladu = json_data.prefixskladu;
				idterminal = json_data.idterminal;

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
				vysledek = ciselnikyBL.KatalogLokaceExport(idterminal, prefixskladu);
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
		[Route("api/Ciselniky/KatalogTypDokladuExport")]
		public HttpResponseMessage KatalogTypDokladuExport([FromBody] Ciselniky_row json_data)
		{
			BL.CiselnikyBL ciselnikyBL = new BL.CiselnikyBL();
			ciselnikyBL.Initialize(System.Web.Hosting.HostingEnvironment.MapPath);

			byte idterminal = 0;
			string prefixskladu = string.Empty;
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

				prefixskladu = json_data.prefixskladu;
				idterminal = json_data.idterminal;

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
				vysledek = ciselnikyBL.KatalogTypDokladuExport(idterminal, prefixskladu);
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



		#endregion

	}
}