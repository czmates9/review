using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Fask.Server.Interfaces.DataSets;
using Fask.Server.Interfaces.Vyroba;
using Fask.WEBAPI;

namespace Fask.MST_W_Server.Controllers
{
		public class AGRO_Controller : ApiController
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

		/// <summary>
		/// Poslani spravy z clienta že vše proběhlo v pořádku
		/// </summary>
		/// <param name="filtr"></param>
		/// <param name="ID"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("api/Vyroba_data_faskevents/{ID}")]
		public Fask.WEBAPI.API_BusinessObjects.FASK_Events_row Vyroba_data_faskevents([FromBody] Fask.WEBAPI.API_BusinessObjects.Filtr_FASK_Events filtr, string ID)
		{
			if (filtr != null)
			{

				Init();

				if (provider != null && provider is Fask.Interfaces.Vyroba.API.IVyroba_PostFaskEventsRow)
				{
					try
					{
						return ((Fask.Interfaces.Vyroba.API.IVyroba_PostFaskEventsRow)provider).GetFASKEventsRow(filtr);


					}
					catch (Exception ex)
					{
						Fask.Logging.ExceptionHandler2.Handle(ex);
						return null;
					}


				}
				else
				{
					return null;
				}
				//return xx;
			}
			else
			{
				return null;
			}
		}

		//      #region stara metoda
  //      /// <summary>
  //      /// Dotaz z klienta na objekt
  //      /// </summary>
  //      /// <param name="ID"></param>
  //      /// <returns></returns>
  //      [HttpGet]
  //      [Route("api/Vyroba_data_faskevents/{ID}")]
  //      public HttpResponseMessage Vyroba_data_faskevents(string ID)
  //      {


  //          try
  //          {
  //              if (!string.IsNullOrEmpty(ID))
  //              {
  //                  Init();

  //                  if (provider != null && provider is Fask.Server.Interfaces.Vyroba.IVyroba_PostFaskEventsRow)
  //                  {
  //                      try
  //                      {
  //                          Filtr_FASK_Events filtr = new Filtr_FASK_Events();
  //                          filtr.separator = "vaha;tisk";

  //                          int p = int.Parse(ID);

  //                          filtr.status = p - 10;
  //                          filtr.statusNew = p;
  //                          filtr.machineid = -1;

  //                          var fe = ((Fask.Server.Interfaces.Vyroba.IVyroba_PostFaskEventsRow)provider).GetFASKEventsRow(filtr);
  //                          return Request.CreateResponse<FASK_Events>(HttpStatusCode.OK, fe);

  //                      }
  //                      catch (Exception ex)
  //                      {
  //                          Logging.ExceptionHandler2.Handle(ex);
  //                          return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message);
  //                      }
  //                  }
  //                  else
  //                  {
  //                      return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, "Provider nenastaven!!");
  //                  }
  //              }
  //              else
  //              {
  //                  return Request.CreateResponse(HttpStatusCode.NoContent);
  //              }
  //          }
  //          catch (Exception ex)
  //          {
  //              Logging.ExceptionHandler2.Handle(ex);
  //              return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message);
  //          }
  //      }
		//#endregion

		#region nova metoda
		/// <summary>
		/// Dotaz z klienta na objekt
		/// </summary>
		/// <param name="ID"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("api/Vyroba_data_faskevents")]
		public HttpResponseMessage Vyroba_data_faskevents([FromBody] Fask.WEBAPI.API_BusinessObjects.Filtr_Tisk filtr_tisk)
		{

			Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, "Volani metody Vyroba_data_faskevents od Leonardo ");
			Fask.WEBAPI.API_BusinessObjects.FASK_Events_row FE_objekt_data = new Fask.WEBAPI.API_BusinessObjects.FASK_Events_row();
			Fask.WEBAPI.API_BusinessObjects.Filtr_FASK_Events filtr_FE = new Fask.WEBAPI.API_BusinessObjects.Filtr_FASK_Events();
			var message_sent = string.Empty;
			Guid G_value;
			Fask.WEBAPI.API_BusinessObjects.Filtr_UpdateStatus filtr_UpdateStatus = new Fask.WEBAPI.API_BusinessObjects.Filtr_UpdateStatus();
			int interni_promenna_status = 0;
			int interni_promenna_status_presun_na_tisk = 0;
			string message_zapis = string.Empty;


			try
			{


				if (filtr_tisk == null)
				{
					var message = String.Format("predavany objekt JSON nenalezen");
					Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, message);

					return Request.CreateErrorResponse(HttpStatusCode.NotFound, message);
					//return Request.CreateResponse<string>(HttpStatusCode.NoContent, "predavany objekt JSON nenalezen");
				}
				if (string.IsNullOrEmpty(filtr_tisk.SSCC))
				{
					var message = String.Format("hodnota SSCC je empty nebo null!!");
					Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, message);

					return Request.CreateErrorResponse(HttpStatusCode.NotFound, message);

					//return Request.CreateResponse<string>(HttpStatusCode.NoContent, "hodnota ID je null!!");
				}
			}
			catch (Exception ex)
			{

				//Logging.ExceptionHandler2.Handle(ex);
				var message = String.Format("CATCH_1 -- TISK_{0}",ex);
				//
				Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, message);
				return Request.CreateErrorResponse(HttpStatusCode.NoContent, message);

				//return Request.CreateResponse<string>(HttpStatusCode.NoContent, "predavany objekt JSON nenalezen");
			}


			try
			{
				if (!string.IsNullOrEmpty(filtr_tisk.SSCC))
				{
					var message_print_SSCC = String.Format("TISK -JSON- SSCC: {0}", filtr_tisk.SSCC);
					Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, message_print_SSCC);
					var message_print_statue = String.Format("TISK -JSON- statue: {0}", filtr_tisk.statue);
					Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, message_print_statue);
					var message_print_message = String.Format("TISK -JSON- message: {0}", filtr_tisk.message);
					Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, message_print_message);

					Init();

					if (provider != null && provider is Fask.Interfaces.Vyroba.API.IVyroba_PostFaskEventsRow)
					{
						try
						{
							//int SSCC = -1;
							//SSCC = Int32.Parse(ID);
							//if (SSCC == -1)
							//	return Request.CreateResponse(HttpStatusCode.OK);

							Fask.WEBAPI.API_BusinessObjects.Filtr_FASK_Events filtr = new Fask.WEBAPI.API_BusinessObjects.Filtr_FASK_Events();
							//filtr.separator = "vaha;tisk"; //bude jen vaha
							filtr.separator = Fask.Constants.AGRO.description_PresunNaTisk; //bude jen vaha
							filtr.NMBRPAL = filtr_tisk.SSCC;
							filtr.pocetPruchodu = 20;
							filtr.timeSleep = 100;

							//filtr.status = p - 10; // status 31 nebo 32
							//filtr.statusNew = p; // status 41 nebo 42
							//filtr.machineid = -1;

							//vyhledat objekt podle SSCC -logika dopsat!!
							//FE_objekt_data = ((Fask.Server.Interfaces.Vyroba.IVyroba_PostFaskEventsRow)provider).GetFASK_Events_BySSCC(filtr);

							FE_objekt_data = ((Fask.Interfaces.Vyroba.API.IVyroba_PostFaskEventsRow)provider).Vrat_zaznam_By_SSCC(filtr);
							//Vrat_zaznam_By_SSCC
							//o = GetFASK_Events_BySSCC(filtr);

							//zapis status TISK -logika dopsat!!
							if (FE_objekt_data != null)
                            {
								G_value = (Guid)FE_objekt_data.faskGUID;

								var message_print = String.Format("TISK -- paleta dohledana!!");
								Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, message_print);

								interni_promenna_status = (int)FE_objekt_data.status;

								if(FE_objekt_data.status.HasValue)
                                {
									interni_promenna_status_presun_na_tisk = FE_objekt_data.status.Value;

								}
                                else
                                {
									var message_status = String.Format("TISK -- FE_objekt_data.status nema hodnotu!!");
									Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, message_status);
								}


								FE_objekt_data.dateeve = DateTime.Now;

								if(filtr_tisk.statue == 1)
                                {
									//FE_objekt_data.description = "vytisknuto";
									FE_objekt_data.description = Fask.Constants.AGRO.description_Tisk_ok;
									message_zapis = Fask.Constants.AGRO.description_Tisk_ok;
								}
								else if (filtr_tisk.statue == 2)
								{
									//FE_objekt_data.description = "tisk_neaplikovano_NP";
									FE_objekt_data.description = Fask.Constants.AGRO.description_Tisk_NP;
									message_zapis = Fask.Constants.AGRO.description_Tisk_NP;
								}
								else if (filtr_tisk.statue == 3)
								{
									//FE_objekt_data.description = "tisk_neaplikovano_FP";
									FE_objekt_data.description = Fask.Constants.AGRO.description_Tisk_FP;
									message_zapis = Fask.Constants.AGRO.description_Tisk_FP;
									
								}
								else
                                {
									//FE_objekt_data.description = "tisk_chyba";
									FE_objekt_data.description = Fask.Constants.AGRO.description_Tisk_error;
									message_zapis = Fask.Constants.AGRO.description_Tisk_error;
								}


								FE_objekt_data.status = interni_promenna_status + 10;
								FE_objekt_data.faskGUID = Guid.NewGuid();
								FE_objekt_data.isProcessed = null;

								Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, message_zapis);

								//poslat objekt FE_objekt_data na zapis status
								((Fask.Interfaces.Vyroba.API.IVyroba_PostFaskEventsRow)provider).SetFaskEventsRow(FE_objekt_data); // chyba??

								message_sent = String.Format("Zapisuji do DB, OK??");
								Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, message_sent);
							}
							else
							{
								message_sent = String.Format("Nedohledan zaznam palety!!");
								Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, message_sent);
								return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, message_sent);
							}

							//dopsat logiku archivace, dokonceno
							if (G_value != null)
							{
								filtr_UpdateStatus.G = G_value;
								if (interni_promenna_status_presun_na_tisk == 41)
								{
									filtr_UpdateStatus.Status = 241;
								}
								else if (interni_promenna_status_presun_na_tisk == 42)
								{
									filtr_UpdateStatus.Status = 242;
								}
								else if (interni_promenna_status_presun_na_tisk == 43)
								{
									filtr_UpdateStatus.Status = 243;
								}
								else if (interni_promenna_status_presun_na_tisk == 44)
								{
									filtr_UpdateStatus.Status = 244;
								}
								else
                                {
									//nastava k tehle situaci, je to chybova situace, osetri to!
									filtr_UpdateStatus.Status = 240;
									var message_sent_arch_1 = String.Format("ARCHIVACE TISK - presun na tisk - predchozi status zaznamu:({0}), archivace na status 240!", interni_promenna_status_presun_na_tisk);
									Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, message_sent_arch_1);
								}

								var x = ((Fask.Interfaces.Vyroba.API.IVyroba_PostFaskEventsRow)provider).Update_StatusByGuid(filtr_UpdateStatus);

								var message_sent_arch = String.Format("ARCHIVACE TISK CORRECT, GUID: {0}!!", G_value);
								Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, message_sent_arch);
							}
							else
							{
								var message_sent_arch = String.Format("ARCHIVACE TISK FALSE!!");
								Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, message_sent_arch);
							}


							var message_sent_end = String.Format("TISK END --OK--!!");
							Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, message_sent_end);
							return Request.CreateErrorResponse(HttpStatusCode.OK, message_sent_end);

						}
						catch (Exception ex)
						{
							//Logging.ExceptionHandler2.Handle(ex);
							var message = String.Format("CATCH_2 -- TISK_{0}", ex);
							//
							Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, message);
							return Request.CreateErrorResponse(HttpStatusCode.NoContent, message);
						}
					}
					else
					{
						var message = String.Format("Provider IVyroba_PostFaskEventsRow nenastaven!!");
						//
						Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, message);
						return Request.CreateResponse<string>(HttpStatusCode.NotFound, "Provider IVyroba_PostFaskEventsRow nenastaven!!");
					}
				}
				else
				{
					var message = String.Format("hodnota SSCC je empty nebo null!!");
					Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, message);

					return Request.CreateErrorResponse(HttpStatusCode.NotFound, message);
				}
			}
			catch (Exception ex)
			{
				var message = String.Format("CATCH_3 -- TISK_{0}", ex);
				//
				Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, message);
				return Request.CreateErrorResponse(HttpStatusCode.NoContent, message);
			}
		}
		#endregion

		/// <summary>
		/// Dotaz z klienta zda objekt existuje
		/// </summary>
		/// <param name="ID"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("api/Vyroba_data_faskevents_over/{ID}")]
		public HttpResponseMessage Vyroba_data_faskevents_over(string ID)
		{


			return Request.CreateResponse<string>(HttpStatusCode.OK, "OK");
			//if (filtr != null)
			//{

			//	Init();

			//	if (provider != null && provider is Fask.Server.Interfaces.Vyroba.IVyroba_PostFaskEventsRow)
			//	{
			//		try
			//		{
			//			return ((Fask.Server.Interfaces.Vyroba.IVyroba_PostFaskEventsRow)provider).GetFASKEventsRow(filtr);


			//		}
			//		catch (Exception ex)
			//		{
			//			return null;
			//		}


			//	}
			//	else
			//	{
			//		return null;
			//	}
			//	//return xx;
			//}
			//else
			//{
			//	return null;
			//}
		}


		#region nova metoda
		/// <summary>
		/// Dotaz z klienta na objekt
		/// </summary>
		/// <param name="ID"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("api/Vyroba_data_vaha")]
		public HttpResponseMessage Vyroba_data_vaha([FromBody] Fask.WEBAPI.API_BusinessObjects.Filtr_Vaha filtr_vaha)
		{
			Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, "Volani metody Vyroba_data_vaha od Tebis ");
			Fask.WEBAPI.API_BusinessObjects.FASK_Events_row FE_objekt_data = new Fask.WEBAPI.API_BusinessObjects.FASK_Events_row();
			Fask.WEBAPI.API_BusinessObjects.Filtr_FASK_Events filtr_FE = new Fask.WEBAPI.API_BusinessObjects.Filtr_FASK_Events();
			var message_sent = string.Empty;
			Guid G_value;
			Fask.WEBAPI.API_BusinessObjects.Filtr_UpdateStatus filtr_UpdateStatus = new Fask.WEBAPI.API_BusinessObjects.Filtr_UpdateStatus();
			try
			{


				if (filtr_vaha == null)
				{
					var message = String.Format("predavany objekt JSON nenalezen");
					Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, message);
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, message);
					//return Request.CreateResponse<string>(HttpStatusCode.NoContent, "predavany objekt JSON nenalezen");
				}
				if (!filtr_vaha.ID.HasValue)
				{
					var message = String.Format("hodnota ID je null!!");
					Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, message);
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, message);

					//return Request.CreateResponse<string>(HttpStatusCode.NoContent, "hodnota ID je null!!");
				}
				if (!filtr_vaha.weight.HasValue)
				{
					var message = String.Format("hodnota weight je null!!");
					Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, message);
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, message);
					//return Request.CreateResponse<string>(HttpStatusCode.NoContent, "hodnota weight je null!!");
				}
				if (!filtr_vaha.state.HasValue)
				{
					var message = String.Format("hodnota state je null!!");
					Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, message);
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, message);
					//return Request.CreateResponse<string>(HttpStatusCode.NoContent, "hodnota state je null!!");
				}

				var messageJSON = String.Format("VAHA -JSON: ID[{0}], WEIGHT[{1}], STATE[{2}], MESSAGE[{3}]",
					filtr_vaha.ID.ToString().Trim(), filtr_vaha.weight.ToString().Trim(), filtr_vaha.state.ToString().Trim(), filtr_vaha.message.ToString().Trim());
				Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, messageJSON);

			}
            catch (Exception ex)
            {

				Logging.ExceptionHandler2.Handle(ex);
				var message = String.Format("CATCH -- predavany objekt JSON nenalezen");
				//
				Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, message);
				return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);

				//return Request.CreateResponse<string>(HttpStatusCode.NoContent, "predavany objekt JSON nenalezen");
			}

			try
			{
				Fask.WEBAPI.API_BusinessObjects.FASK_Events FE_o = new Fask.WEBAPI.API_BusinessObjects.FASK_Events();

				if (filtr_vaha.ID == 31 || filtr_vaha.ID == 32 || filtr_vaha.ID == 33 || filtr_vaha.ID == 34)
				{

					Init();

					if (provider != null && provider is Fask.Interfaces.Vyroba.API.IVyroba_PostFaskEventsRow)
					{

						//int status_hledat = ((Fask.Interfaces.Vyroba.API.IVyroba_PostFaskEventsRow)provider).GetStatus(filtr_vaha.ID.ToString());


						try
						{
							//logika dohledani zaznamu pro ulozeni hodnoty weight
							//------dohledani zaznamu START----------------------
							filtr_FE.pocetPruchodu = 10;
							filtr_FE.timeSleep = 100;
							filtr_FE.separator = "presun na streckovacku;vaha;vaha_chyba";

							if (filtr_vaha.ID == 31)
							{
								filtr_FE.status = 21;
								filtr_FE.statusNew = 31;
							}
							else if (filtr_vaha.ID == 32)
							{
								filtr_FE.status = 22;
								filtr_FE.statusNew = 32;
							}
							else if(filtr_vaha.ID == 33) 
                            {
								filtr_FE.status = 21;
								filtr_FE.statusNew = 31;
								filtr_FE.machineid = 1033;
							}
							else if (filtr_vaha.ID == 34) //bylo 33, chyba 16.4.2024 MaR
							{
								filtr_FE.status = 24;
								filtr_FE.statusNew = 34;
								filtr_FE.machineid = 10;
							}

							//TODO doplneni logiky pro vahu na robotech

							FE_objekt_data = ((Fask.Interfaces.Vyroba.API.IVyroba_PostFaskEventsRow)provider).Get_FE_Row(filtr_FE);
							
							
							
							//------dohledani zaznamu END----------------------


						}
						catch (Exception ex)
						{
							Logging.ExceptionHandler2.Handle(ex);
						}
					}
					else
					{
						//
						var message = String.Format("Provider IVyroba_PostFaskEventsRow nenastaven!!");
						//
						Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, message);
						return Request.CreateResponse<string>(HttpStatusCode.NotFound, "Provider IVyroba_PostFaskEventsRow nenastaven!!");
					}




					if (provider != null && provider is Fask.Interfaces.Vyroba.API.IVyroba_Vaha)
					{
						try
						{
							////int SSCC = -1;
							////SSCC = Int32.Parse(ID);
							////if (SSCC == -1)
							////	return Request.CreateResponse(HttpStatusCode.OK);

							//Filtr_FASK_Events filtr = new Filtr_FASK_Events();
							////filtr.separator = "vaha;tisk"; //bude jen vaha
							//filtr.separator = "presun na streckovacku"; //bude jen vaha
							//filtr.NMBRPAL = filtr_tisk.SSCC;

							////filtr.status = p - 10; // status 31 nebo 32
							////filtr.statusNew = p; // status 41 nebo 42
							////filtr.machineid = -1;

							//vyhledat objekt podle SSCC -logika dopsat!!
							//FASK_Events o = new FASK_Events();
							//o = ((Fask.Server.Interfaces.Vyroba.IVyroba_PostFaskEventsRow)provider).GetFASK_Events_BySSCC(filtr);
							//o = GetFASK_Events_BySSCC(filtr);

							//zapis status TISK -logika dopsat!!
							if (FE_objekt_data != null)
							{

								var messageDATA = String.Format("VAHA -FE: SSCC[{0}], LINKA[{1}], ITEMDESC[{2}], WEIGHT[{3}]",
					FE_objekt_data.NMBRPAL.ToString().Trim(), FE_objekt_data.machineid.ToString().Trim(), FE_objekt_data.ITEMDESC.ToString().Trim(), FE_objekt_data.WEIGHT.ToString().Trim());
								Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, messageDATA);

								G_value = (Guid)FE_objekt_data.faskGUID;
								FE_objekt_data.dateeve = DateTime.Now;
								FE_objekt_data.status = filtr_vaha.ID;
								FE_objekt_data.faskGUID = Guid.NewGuid();
								FE_objekt_data.isProcessed = null;


								if (filtr_vaha.state == 1)
								{
									if (filtr_vaha.weight > 0)
									{
										FE_objekt_data.description = "vaha";
										FE_objekt_data.WEIGHT = filtr_vaha.weight;
										message_sent = String.Format("Zapis do DB, OK!!");
									}
									else
									{
										// pokud je weight <= 0
										//TODO osetreni hodnoty weight pokud je mensi nebo rovno 0
										FE_objekt_data.description = "vaha_chyba";
										FE_objekt_data.WEIGHT = 0;

										message_sent = String.Format("Vážení se nepovedlo, hodnota weight je mensi nebo rovna 0! zaznam propsan do DB!!");
										Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, message_sent);
										//return Request.CreateErrorResponse(HttpStatusCode.Conflict, message_1);

									}
								}
								else
								{
									//pokud state != 1
									//TODO chybove stavy vahy! 
									FE_objekt_data.description = "vaha_chyba";
									FE_objekt_data.WEIGHT = 0;

									message_sent = String.Format("Vážení se nepovedlo, state !=1 zaznam propsan do DB!!");
									Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, message_sent);
									//return Request.CreateErrorResponse(HttpStatusCode.Conflict, message_2);
								}

								//poslat objekt o na zapis status
								((Fask.Interfaces.Vyroba.API.IVyroba_PostFaskEventsRow)provider).SetFaskEventsRow(FE_objekt_data);

								if (FE_objekt_data.productionGuid.HasValue && FE_objekt_data.WEIGHT.HasValue)
								{
									((Fask.Interfaces.Vyroba.API.IVyroba_PostFaskEventsRow)provider).UpdateProductionRow(FE_objekt_data.productionGuid, FE_objekt_data.WEIGHT);
								}


							//poslat


							}
							else
                            {
								message_sent = String.Format("Nedohledan zaznam palety!!");
								Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, message_sent);
								
								//neodesilat
								return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, message_sent);
							}



                            //dopsat logiku archivace, dokonceno
                            if (G_value != null)
                            {
                                filtr_UpdateStatus.G = G_value;
                                if (filtr_vaha.ID == 31)
                                {
                                    filtr_UpdateStatus.Status = 221;
                                }
                                else if (filtr_vaha.ID == 32)
                                {
                                    filtr_UpdateStatus.Status = 222;
								}
								else if (filtr_vaha.ID == 33)
								{
									filtr_UpdateStatus.Status = 223;
								}
								else if (filtr_vaha.ID == 34)
								{
									filtr_UpdateStatus.Status = 224;
								}

								var x = ((Fask.Interfaces.Vyroba.API.IVyroba_PostFaskEventsRow)provider).Update_StatusByGuid(filtr_UpdateStatus);

								var message_sent_arch = String.Format("ARCHIVACE CORRECT, GUID: {0}!!", G_value);
								Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, message_sent_arch);
							}
                            else
                            {
								var message_sent_arch = String.Format("ARCHIVACE FALSE!!");
								Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, message_sent_arch);
								return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, message_sent);
							}
							


							Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, message_sent);
							return Request.CreateErrorResponse(HttpStatusCode.OK, message_sent);

						}
						catch (Exception ex)
						{
							Logging.ExceptionHandler2.Handle(ex);
							return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message);
						}
					}
					else
					{
						var message = String.Format("Provider nenastaven!!");
						Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, message);
						return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);

						//return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, "Provider nenastaven!!");
					}


				}
				else
				{
					//ID stroje neni 31/32
					var message = String.Format("Nerozpoznan stroj! je ID 31 nebo 32 ?");
					Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, message);
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, message);

					//return Request.CreateResponse(HttpStatusCode.NoContent);
				}
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message);
			}
		}
		#endregion

		#region Logs

		[HttpPost]
		[Route("api/SV_Logs_insert")]
		public HttpResponseMessage Konzola_VPP_Update_Row([FromBody] Fask.WEBAPI.API_BusinessObjects.BO_Logs FZ_data)
		{
			Init();

			bool result = false;
			Fask.Interfaces.DataSets.Vyroba.FASK_EventsErrDataTable dt = new Fask.Interfaces.DataSets.Vyroba.FASK_EventsErrDataTable();

			if (provider != null && provider is Fask.Interfaces.Vyroba.API.IVyroba_Logs_Insert)
			{
				try
				{
					BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Logs, Fask.WEBAPI.API_BusinessObjects.BO_Logs_row, Fask.Interfaces.DataSets.Vyroba.FASK_EventsErrDataTable, Fask.Interfaces.DataSets.Vyroba.FASK_EventsErrRow> y = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Logs, Fask.WEBAPI.API_BusinessObjects.BO_Logs_row, Fask.Interfaces.DataSets.Vyroba.FASK_EventsErrDataTable, Fask.Interfaces.DataSets.Vyroba.FASK_EventsErrRow>();
					dt = y.GetDTFromBO(FZ_data);

					if (dt.Count == 1)
					{
						result = ((Fask.Interfaces.Vyroba.API.IVyroba_Logs_Insert)provider).Logs_Insert(dt);
					}
					else
					{
						result = false;
					}

					return Request.CreateResponse<bool>(HttpStatusCode.OK, result);
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

		#endregion

		#region Vizualizace ziskani dat List_FE
		/// <summary>
		/// Dotaz z klienta na objekt
		/// </summary>
		/// <param name="ID"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("api/Vyroba_data_list_FE")]
		public Fask.WEBAPI.API_BusinessObjects.FASK_Events Vyroba_data_list_FE([FromBody] Fask.WEBAPI.API_BusinessObjects.Filtr_FASK_Events filtr_FE)
		{
			//Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, "Volani metody Vyroba_data_vaha od Tebis ");
			Fask.WEBAPI.API_BusinessObjects.FASK_Events list_FE = null;
			

			try
			{
                if(filtr_FE.status == 21 || filtr_FE.status == 22)
                {
                    Init();

                    if (provider != null && provider is Fask.Interfaces.Vyroba.API.IVyroba_PostFaskEventsRow)
                    {
                        try
                        {
							//------dohledani zaznamu START----------------------
							filtr_FE.pocetPruchodu = 10;
							filtr_FE.timeSleep = 100;

							if(filtr_FE.separator == null || filtr_FE.separator == string.Empty)
								filtr_FE.separator = "presun na streckovacku";

                            list_FE = ((Fask.Interfaces.Vyroba.API.IVyroba_PostFaskEventsRow)provider).Get_FE_List(filtr_FE);
                            //------dohledani zaznamu END----------------------


                        }
                        catch (Exception ex)
                        {
                            Logging.ExceptionHandler2.Handle(ex);
							Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, "Vyroba_data_list_FE --> ERROR exception!!");
						}
                    }
                    else
                    {
						//return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, "Provider IVyroba_PostFaskEventsRow nenastaven!!");
						Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, "Vyroba_data_list_FE --> Provider IVyroba_PostFaskEventsRow nenastaven!!");
					}
				}
                else
                {
					//neni status 21 nebo 22
					Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, "Vyroba_data_list_FE --> Status se nerovna 21 nebo 22!!");
				}

				return list_FE;

			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				//return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ex.Message);
				Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, "Vyroba_data_list_FE --> ERROR exception!!");
				return list_FE;
			}
		}
		#endregion
	}

}