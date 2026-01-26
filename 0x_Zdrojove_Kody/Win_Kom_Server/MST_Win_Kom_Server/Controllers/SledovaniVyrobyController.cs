using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Fask.ModuleSql;
using Fask.Server.Interfaces.DataSets;
using Fask.WEBAPI.API_BusinessObjects;

namespace Fask.MST_W_Server.Controllers
{
	public class SledovaniVyrobyController : ApiController
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
						Assembly providerAssemlby = Assembly.LoadFrom( mappedPath);

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


		// POST: api/login
		[HttpPost]
		[Route("api/SledovaniVyroby_data")]
		public Fask.WEBAPI.API_BusinessObjects.FASK_Events_row GetEvents([FromBody] Fask.WEBAPI.API_BusinessObjects.Filtr_FASK_Events filtr)
		{
			if (filtr == null)
			{
				return null;
			}

			Init();

			if (provider == null || !(provider is Fask.Interfaces.Vyroba.API.IVyroba_PostFaskEventsRow faskEventsRowAPI))
			{
				return null;
			}

			try
			{
				return faskEventsRowAPI.GetFASKEventsRow(filtr);
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				return null;
			}
		}

		// POST: api/login
		[HttpPost]
		[Route("api/SledovaniVyroby_dataADAM")]
		public Fask.WEBAPI.API_BusinessObjects.MachineStateSet GetADAMEvents([FromBody] Fask.WEBAPI.API_BusinessObjects.Filtr_MachineStateSet filtr)
		{
			if (filtr != null)
			{

				Init();

				if (provider != null && provider is Fask.Interfaces.Vyroba.API.IVyroba_GetMachineStateSetEventsRow)
				{
					try
					{
						return ((Fask.Interfaces.Vyroba.API.IVyroba_GetMachineStateSetEventsRow)provider).GetMachineStateSetEventsRow(filtr);


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

		// POST: api/login
		[HttpPost]
		[Route("api/SledovaniVyroby_dataADAM_zapis")]
		public HttpResponseMessage PostADAMEvents([FromBody] Fask.WEBAPI.API_BusinessObjects.MachineStateSet MSS_objekt)
		{
			if (MSS_objekt != null)
			{
				Init();

				if (provider != null && provider is Fask.Interfaces.Vyroba.API.IVyroba_PostMSSEventRow)
				{
					try
					{
						//return ((Fask.Server.Interfaces.Vyroba.IVyroba_PostMSSEventRow)provider).PostMachineStateSetEventsRow(MSS_objekt);
						//var xx = ((Fask.Server.Interfaces.Vyroba.IVyroba_PostMSSEventRow)provider).PostMachineStateSetEventsRow(MSS_objekt);
						((Fask.Interfaces.Vyroba.API.IVyroba_PostMSSEventRow)provider).SetMachineStateSet(MSS_objekt);
						return Request.CreateResponse(HttpStatusCode.OK);
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
			else
				return Request.CreateResponse(HttpStatusCode.BadRequest);

		}

		[HttpPost]
		[Route("api/SledovaniVyroby_zapisStatus")]
		public HttpResponseMessage PostFaskEvents_status([FromBody] Fask.WEBAPI.API_BusinessObjects.FASK_Events_row FE_object)
		{
			if (FE_object != null)
			{
				Init();

				if (provider != null && provider is Fask.Interfaces.Vyroba.API.IVyroba_PostFaskEventsRow)
				{
					try
					{
						//return ((Fask.Server.Interfaces.Vyroba.IVyroba_PostMSSEventRow)provider).PostMachineStateSetEventsRow(MSS_objekt);
						//var xx = ((Fask.Server.Interfaces.Vyroba.IVyroba_PostMSSEventRow)provider).PostMachineStateSetEventsRow(MSS_objekt);
						((Fask.Interfaces.Vyroba.API.IVyroba_PostFaskEventsRow)provider).SetFaskEventsRow(FE_object);
						return Request.CreateResponse(HttpStatusCode.OK);
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
			else
				return Request.CreateResponse(HttpStatusCode.BadRequest);

		}

		[HttpPost]
		[Route("api/Fask_Events_Insert")]
		public HttpResponseMessage Fask_Events_Insert([FromBody] Fask.WEBAPI.API_BusinessObjects.FASK_Events_row_11_2023 FE_object)
		{

            #region lokalni promenne
            string outMessage = string.Empty;
			bool validace = true;
            #endregion

            #region validace prijatych dat JSON
            try
            {
				
                if (FE_object != null)
                {

					if (string.IsNullOrEmpty(FE_object.loginid))
					{
						validace = false;
					}
					if (string.IsNullOrEmpty(FE_object.machineid))
					{
						validace = false;
					}
					if (!FE_object.dateeve.HasValue)
					{
						validace = false;
					}
					if (!FE_object.qty.HasValue)
					{
						validace = false;
					}
					if (!FE_object.qtyReal.HasValue)
					{
						validace = false;
					}
					if (FE_object.barcodeReaded==null)
					{
						validace = false;
					}
					if (FE_object.barcodeSended==null)
					{
						validace = false;
					}
					if (!FE_object.faskGUID.HasValue)
					{
						validace = false;
					}
					if (string.IsNullOrEmpty(FE_object.reportType))
					{
						validace = false;
					}

					if(!validace)
                    {
						outMessage = "Hodnota v objektu JSON nesmi byt null!";
						return Request.CreateResponse<string>(HttpStatusCode.UnsupportedMediaType, outMessage);
					}

				}
                else
                {
					outMessage = "Prazdny objekt JSON!";
					return Request.CreateResponse<string>(HttpStatusCode.UnsupportedMediaType, outMessage);
				}
                   
            }
            catch (Exception ex)
            {
                return Request.CreateResponse<Exception>(HttpStatusCode.InternalServerError,ex);
            }
            #endregion

            #region zapis dat z JSON do DB
            try
            {
                Init();

                if (provider != null && provider is Fask.Interfaces.Vyroba.API.IVyroba)
                {
                    try
                    {
                       bool vysledek = ((Fask.Interfaces.Vyroba.API.IVyroba)provider).FASK_Events_row_11_2023_Insert(FE_object);

						if(vysledek)
                        {
							return Request.CreateResponse<bool>(HttpStatusCode.OK, vysledek);
						}
						else
                        {

							return Request.CreateResponse<bool>(HttpStatusCode.Conflict, vysledek);
						}


                    }
                    catch (Exception ex)
                    {
                        return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
                    }

                }
                else
                    return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
				return Request.CreateResponse<Exception>(HttpStatusCode.InternalServerError, ex);
			} 
            #endregion

        }

		public string Fask_Events_validace(FASK_Events_row_11_2023 FE_object) 
		{
			string outMessage = string.Empty;
			bool validace = true;

            try
            {
				if (FE_object != null)
				{

					if (string.IsNullOrEmpty(FE_object.loginid))
					{
						validace = false;
					}
					if (string.IsNullOrEmpty(FE_object.machineid))
					{
						validace = false;
					}
					if (FE_object.dateeve == null)
					{
						validace = false;
					}
					if (FE_object.qty == null)
					{
						validace = false;
					}
					if (FE_object.qtyReal == null)
					{
						validace = false;
					}
					if (string.IsNullOrEmpty(FE_object.barcodeReaded))
					{
						validace = false;
					}
					if (string.IsNullOrEmpty(FE_object.barcodeSended))
					{
						validace = false;
					}
					if (FE_object.faskGUID == null)
					{
						validace = false;
					}
					if (string.IsNullOrEmpty(FE_object.reportType))
					{
						validace = false;
					}

					if (FE_object.isProcessed == null)
					{
					}





					if (string.IsNullOrEmpty(FE_object.description))
					{
					}

					if (string.IsNullOrEmpty(FE_object.zakazka))
					{
					}

					if (string.IsNullOrEmpty(FE_object.popis))
					{
					}

					if (FE_object.productionGuid == null)
					{
					}

					if (string.IsNullOrEmpty(FE_object.VPH))
					{
					}

					if (FE_object.VPPol == null)
					{
					}

					if (string.IsNullOrEmpty(FE_object.EAN_IS))
					{
					}

					if (string.IsNullOrEmpty(FE_object.IS_ID))
					{
					}

					if (string.IsNullOrEmpty(FE_object.NMBRPAL))
					{
					}

					if (FE_object.status == null)
					{
					}

					if (string.IsNullOrEmpty(FE_object.ITEMDESC))
					{
					}

					if (FE_object.QTYPACK == null)
					{
					}

					if (string.IsNullOrEmpty(FE_object.PackType))
					{
					}

					if (FE_object.WEIGHT == null)
					{
					}

					if (FE_object.BarcodeT == null)
					{
					}

					if (string.IsNullOrEmpty(FE_object.REZ_1))
					{
					}

					if (string.IsNullOrEmpty(FE_object.REZ_2))
					{
					}

					if (string.IsNullOrEmpty(FE_object.REZ_3))
					{
					}

					if (string.IsNullOrEmpty(FE_object.REZ_4))
					{
					}

					if (string.IsNullOrEmpty(FE_object.REZ_5))
					{
					}


				}
				else
				{
					outMessage = "Prázdný objekt Fask_Events!";
				}




				return outMessage;
			}
            catch (Exception ex)
            {

				return ex.Message;
            }

		}


		[HttpPost]
		[Route("api/SledovaniVyroby_updateStatus")]
		public HttpResponseMessage PostFaskEvents_updateStatus([FromBody] Fask.WEBAPI.API_BusinessObjects.Filtr_UpdateStatus FE_object)
		{
			if (FE_object != null)
			{
				Init();

				if (provider != null && provider is Fask.Interfaces.Vyroba.API.IVyroba_PostFaskEventsRow)
				{
					try
					{
						//return ((Fask.Server.Interfaces.Vyroba.IVyroba_PostMSSEventRow)provider).PostMachineStateSetEventsRow(MSS_objekt);
						//var xx = ((Fask.Server.Interfaces.Vyroba.IVyroba_PostMSSEventRow)provider).PostMachineStateSetEventsRow(MSS_objekt);
						int res = ((Fask.Interfaces.Vyroba.API.IVyroba_PostFaskEventsRow)provider).Update_StatusByGuid(FE_object);
						if(res == -1 || res == 0)
                        {
							return Request.CreateResponse(HttpStatusCode.NotFound);
						}
						return Request.CreateResponse(HttpStatusCode.OK);
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
			else
				return Request.CreateResponse(HttpStatusCode.BadRequest);

		}



		// POST: api/login
		[HttpPost]
		[Route("api/SledovaniVyroby_archivaceZaznamu_data")]
		public HttpResponseMessage FE_archivace_Rows_data([FromBody] Fask.WEBAPI.API_BusinessObjects.Filtr_FASK_Events filtr)
		{
            try
            {
                if (filtr != null)
                {

                    Init();

                    if (provider != null && provider is Fask.Interfaces.Vyroba.API.IVyroba_PostFaskEventsRow)
                    {
                        try
                        {
                            var dt = ((Fask.Interfaces.Vyroba.API.IVyroba_PostFaskEventsRow)provider).FE_archivace_Rows_data(filtr);

                            return Request.CreateResponse<Fask.Interfaces.DataSets.Vyroba.FASK_Events_archivaceDataTable>(HttpStatusCode.OK, dt);


                        }
                        catch (Exception ex)
                        {
							return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
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
            catch (Exception ex)
            {

				return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
			}
		}





		// POST: api/login
		[HttpPost]
		[Route("api/SledovaniVyroby_archivaceZaznamu_deaktivace_nakladka")]
		public HttpResponseMessage FE_archivace_Rows_deaktivace_nakladka([FromBody] Fask.Interfaces.DataSets.Vyroba.FASK_Events_archivaceDataTable data_rows)
		{
			try
			{
				if (data_rows != null)
				{

					Init();

					if (provider != null && provider is Fask.Interfaces.Vyroba.API.IVyroba_PostFaskEventsRow)
					{
						try
						{
							int pocet = ((Fask.Interfaces.Vyroba.API.IVyroba_PostFaskEventsRow)provider).FE_archivace_Rows_deaktivace_nakladka(data_rows);

							return Request.CreateResponse<int>(HttpStatusCode.OK, pocet);


						}
						catch (Exception ex)
						{
							return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
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
			catch (Exception ex)
			{

				return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
			}
		}

		[HttpPost]
		[Route("api/SledovaniVyroby_archivaceZaznamu_deaktivace_vykladka")]
		public HttpResponseMessage FE_archivace_Rows_deaktivace_vykladka([FromBody] Fask.Interfaces.DataSets.Vyroba.FASK_Events_archivaceDataTable data_rows)
		{
			try
			{
				if (data_rows != null)
				{

					Init();

					if (provider != null && provider is Fask.Interfaces.Vyroba.API.IVyroba_PostFaskEventsRow)
					{
						try
						{
							int pocet = ((Fask.Interfaces.Vyroba.API.IVyroba_PostFaskEventsRow)provider).FE_archivace_Rows_deaktivace_vykladka(data_rows);

							return Request.CreateResponse<int>(HttpStatusCode.OK, pocet);


						}
						catch (Exception ex)
						{
							return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
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
			catch (Exception ex)
			{

				return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
			}
		}



		// POST: api/login
		[HttpPost]
		[Route("api/Konfigurace_ADAM_data")]
		public HttpResponseMessage Konfigurace_ADAM_data([FromBody] Fask.WEBAPI.API_BusinessObjects.MachinesDefinition_objekt o)
		{
			try
			{
				Fask.Interfaces.DataSets.Vyroba.MachinesDefinitionDataTable dt_out = new Fask.Interfaces.DataSets.Vyroba.MachinesDefinitionDataTable();

				int cisloSluzby = o.ID_group;

				Init();

				if (provider != null && provider is Fask.Interfaces.Vyroba.API.IVyroba_PostFaskEventsRow)
				{
					try
					{
						dt_out = ((Fask.Interfaces.Vyroba.API.IVyroba_PostFaskEventsRow)provider).Konfigurace_ADAM_data(cisloSluzby);

						//return Request.CreateResponse<Fask.Server.Interfaces.DataSets.Vyroba.FASK_Events_archivaceDataTable>(HttpStatusCode.OK, dt);

						if (dt_out != null)
						{
							return Request.CreateResponse<Fask.Interfaces.DataSets.Vyroba.MachinesDefinitionDataTable>(HttpStatusCode.OK, dt_out);
						}
						else
						{
							return Request.CreateResponse<Fask.Interfaces.DataSets.Vyroba.MachinesDefinitionDataTable>(HttpStatusCode.InternalServerError, dt_out);
						}




					}
					catch (Exception ex)
					{
						return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
					}


				}
				else
				{
					return null;
				}
				//return xx;

			}
			catch (Exception ex)
			{

				return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
			}
		}



		// POST: api/login
		[HttpPost]
		[Route("api/Konfigurace_Mericich_Zarizeni")]
		public HttpResponseMessage Konfigurace_Mericich_Zarizeni([FromBody] Fask.WEBAPI.API_BusinessObjects.MachinesDefinitionMeasurement_objekt o)
		{
			try
			{
				Fask.Interfaces.DataSets.Vyroba.MachinesDefinitionMeasurementDataTable dt_out = new Fask.Interfaces.DataSets.Vyroba.MachinesDefinitionMeasurementDataTable();



                if (o == null)
                {
					return Request.CreateResponse<string>(HttpStatusCode.BadRequest, "objekt JSON nema data");
				}
                else
                {
                    if (string.IsNullOrEmpty(o.IP))
                    {
						return Request.CreateResponse<string>(HttpStatusCode.BadRequest, "objekt JSON nema data IP pro nalezeni Papouch zarizeni");
					}
                }


				string IP_ADAM = o.IP;

                Init();


                #region 25.4.2025 MaR OLD
                if (provider != null && provider is Fask.Interfaces.Vyroba.API.IVyroba_PostFaskEventsRow)
                {
                    try
                    {
                        dt_out = ((Fask.Interfaces.Vyroba.API.IVyroba_PostFaskEventsRow)provider).Konfigurace_Mericich_Zarizeni_00(IP_ADAM);

                        //return Request.CreateResponse<Fask.Server.Interfaces.DataSets.Vyroba.FASK_Events_archivaceDataTable>(HttpStatusCode.OK, dt);

                        if (dt_out != null)
                        {
                            return Request.CreateResponse<Fask.Interfaces.DataSets.Vyroba.MachinesDefinitionMeasurementDataTable>(HttpStatusCode.OK, dt_out);
                        }
                        else
                        {
                            //return Request.CreateResponse<Fask.Interfaces.DataSets.Vyroba.MachinesDefinitionMeasurementDataTable>(HttpStatusCode.InternalServerError, dt_out);
							return Request.CreateResponse<string>(HttpStatusCode.BadRequest, "nedohledana data o Papouch zarizeni");
						}




                    }
                    catch (Exception ex)
                    {
                        return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
                    }


                }
                else
                {
                    return Request.CreateResponse<Fask.Interfaces.DataSets.Vyroba.MachinesDefinitionMeasurementDataTable>(HttpStatusCode.BadRequest, null);
                }

                #endregion



                #region 25.4.2025MaR new
                //try
                //{
                //    dt_out = Konfigurace_Mericich_Zarizeni_00(IP_ADAM);

                //    //return Request.CreateResponse<Fask.Server.Interfaces.DataSets.Vyroba.FASK_Events_archivaceDataTable>(HttpStatusCode.OK, dt);

                //    if (dt_out != null)
                //    {
                //        return Request.CreateResponse<Fask.Interfaces.DataSets.Vyroba.MachinesDefinitionMeasurementDataTable>(HttpStatusCode.OK, dt_out);
                //    }
                //    else
                //    {
                //        return Request.CreateResponse<Fask.Interfaces.DataSets.Vyroba.MachinesDefinitionMeasurementDataTable>(HttpStatusCode.InternalServerError, dt_out);
                //    }




                //}
                //catch (Exception ex)
                //{
                //    return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
                //}
                #endregion



                //return xx;

            }
			catch (Exception ex)
			{

				return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
			}

		}


		#region MaR 23.4.2025 zkouska Papouch data
		public Fask.Interfaces.DataSets.Vyroba.MachinesDefinitionMeasurementDataTable Konfigurace_Mericich_Zarizeni_00(string IP)
		{
			//-----------------------------------------------START-------------------------------------------------------

			try
			{
				Globals.LoadConfiguration();

				//Fask.Interfaces.DataSets.Vyroba.MachinesDefinitionMeasurementDataTable DT_S0 = new Fask.Interfaces.DataSets.Vyroba.MachinesDefinitionMeasurementDataTable();


				// Fask.Interfaces.DataSets.Vyroba.FASK_Events_archivaceDataTable DT_S0 = new Fask.Interfaces.DataSets.Vyroba.FASK_Events_archivaceDataTable();


				//DT_S0 = Get_ADAM_konfig_Rows((Globals.Konfigurace.ConnectionString[0].FASKDB), 1);

				//var x = DT_S0.Count;

				Fask.Interfaces.DataSets.Vyroba.MachinesDefinitionMeasurementDataTable DT = new Fask.Interfaces.DataSets.Vyroba.MachinesDefinitionMeasurementDataTable();
				//SqlCommand comm = null; //CommandBehavior do databaze
				SqlConnection conn = null; //connection do databaze
										   //int result = 0;
				try
				{
					using (conn = new SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB))
					{


						using (var com = conn.CreateCommand())
						{
							com.CommandType = CommandType.Text;

							//---------------SQL dotaz-----------------------------

							//SELECT [IP]
							//        ,[Description]
							//        ,[MType]
							//        ,[PORT]
							//FROM [MachinesDefinition]

							//---------------SQL dotaz-----------------------------


							//com.CommandText = "SELECT" +
							//    " IP" +
							//    " ,Description" +
							//    " ,MType" +
							//    " ,PORT" +
							//    " FROM [MachinesDefinition]" ;

							com.CommandText = "SELECT" +
							" * " +
							" FROM [MachinesDefinitionMeasurement]";

							com.CommandText += " WHERE 1=1 " +
						  " and IP = '" + IP + "' ;";


							conn.Open();

							using (var adapter = new SqlDataAdapter())
							{
								adapter.SelectCommand = com;
								int returnValue = adapter.Fill(DT);

							}
						}
					}

					var w = DT.Count;

					return DT;
				}
				catch (Exception ex)
				{

					var message_sent = String.Format("CATCH--Konfigurace_ADAM_data!!");
					Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, message_sent);
					Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, ex.Message);
					return null;
				}
				//----------------------------------------------END-----------------------------------------------------------
			}
			catch (Exception ex)
			{

				var message_sent = String.Format("CATCH--Konfigurace_ADAM_data!!");
				Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, message_sent);
				Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, ex.Message);
				return null;
			}


		}

		#endregion


		#region Status 61/62
		[HttpPost]
		[Route("api/SV_ZmenaStatusu")]
		public bool ZmenaStatusu([FromBody] int status)
		{
			bool vysledekProcedury = false;
			System.Data.SqlClient.SqlConnection sqlConn = null;
			System.Data.SqlClient.SqlCommand sqlComm = null;
			try
			{

				Globals.LoadConfiguration();
				sqlConn = new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
				sqlComm = new System.Data.SqlClient.SqlCommand();
				sqlComm.CommandTimeout = 1000;
				sqlComm.Connection = sqlConn;
				sqlComm.CommandType = System.Data.CommandType.StoredProcedure;
				sqlComm.CommandText = "FASKEvents_ZmenaStatusu";

				sqlComm.Parameters.AddWithValue("@status_IN", status);

				//sqlComm.Parameters.AddWithValue("@cisloLinky", cisloLinky);
				//------vraceni promenne START-----------------------
				// Return value as parameter
				SqlParameter returnVysledek = new SqlParameter("vysledek", DbType.Boolean);
				returnVysledek.Direction = ParameterDirection.ReturnValue;
				sqlComm.Parameters.Add(returnVysledek);

				// Execute the stored procedure
				sqlComm.Connection.Open();
				sqlComm.ExecuteNonQuery();
				//myConnection.Close();

				vysledekProcedury = Convert.ToBoolean(returnVysledek.Value);
				//------vraceni promenne END-----------------------

				//sqlComm.Connection.Open();


				return vysledekProcedury;
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, ex.Message);
				return false;
			}
			finally
			{
				if ((sqlConn != null) && ((sqlConn.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open))
				{
					sqlConn.Close();
				}
			}
		}

		#endregion

		#region zadani hesla pri ukonceni klientske aplikace
		// POST: api/login
		[HttpGet]
        [Route("api/UkonceniAplikace_odhlaseni/{pass}")]
        public HttpResponseMessage OvereniHesla(string pass)
        {
           
				try
				{
					string password = Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Vyroba[0].ApplicationEndPass;
					if (pass.Trim() == password.Trim())
						return Request.CreateResponse<bool>(HttpStatusCode.OK, false); // vracim false kvuli cyklu do while v klientu!!!
					else
					return Request.CreateResponse<bool>(HttpStatusCode.Conflict, true);



			}
				catch (Exception ex)
				{
					Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
					return Request.CreateResponse<bool>(HttpStatusCode.InternalServerError, true);
				}

            
        }
		#endregion

		#region GET puvodni pokus
#if (false)
		// GET: api/SledovaniVyroby_data
		[HttpGet]
		//[Route("api/SledovaniVyroby_data")]
		[Route("api/SledovaniVyroby_data/MachineID/{machineID}/Status/{status}/")]
		public FASK_Events GetData(string machineID, string status)
		//public FASK_Events GetData()
		{
			Init();
			FASK_Events fe = new FASK_Events();

			if (provider != null && provider is Fask.Server.Interfaces.Vyroba.IVyroba_GetFASKEventsRow)
			{
				try
				{

					var row = ((Fask.Server.Interfaces.Vyroba.IVyroba_GetFASKEventsRow)provider).GetFASKEventsRow(machineID, status);

					if (row != null)
					{
						// TODO  naplnit cely objekt
						fe.id = row.id;
						fe.loginid = row.loginid;
						fe.machineid = row.machineid;
						fe.dateeve = row.dateeve;
						fe.qty = row.qty;
						fe.qtyReal = row.qtyReal;
						fe.description = row.description;
						fe.barcodeReaded = row.barcodeReaded;
						fe.barcodeSended = row.barcodeSended;
						fe.zakazka = row.zakazka;
						fe.faskGUID = row.faskGUID;
						fe.reportType = row.reportType;
						//fe.isProcessed = row.isProcessed; //chyba
						fe.IDO = row.IDO;
						fe.scan1 = row.scan1;
						fe.scan2 = row.scan2;
						fe.scan3 = row.scan3;
						fe.sensor = row.sensor;
						fe.material = row.material;
						fe.VPH = row.VPH;
						fe.VPPol = row.VPPol;
						fe.EAN_IS = row.EAN_IS;
						fe.IS_ID = row.IS_ID;
						fe.NMBRPAL = row.NMBRPAL;
						fe.status = row.status;
						//fe.productionGuid = row.   // chybi v row

					}

					
				}
				catch (Exception ex)
				{
					return null;
				}

				return fe;

			}
            else
            {
				return null;
            }
		}
#endif
		#endregion

		#region Metody pro APIRemoteLib z projektu sledovani vyroby
	

		#region FASK_Machine

		[HttpGet]
		[Route("api/SV_Fill_FASK_Machine")]
		public Fask.SQLiteDBs.DataSets.Vyroba.MachinesDataTable SV_Fill_FASK_Machine()
		{
			Fask.SQLiteDBs.DataSets.Vyroba.MachinesDataTable dataTable = new Fask.SQLiteDBs.DataSets.Vyroba.MachinesDataTable();

			Fill_FASK_Machine(dataTable);

			return dataTable;
		}

		public bool Fill_FASK_Machine(Fask.SQLiteDBs.DataSets.Vyroba.MachinesDataTable dataTable, bool ClearBeforeFill = true)
		{

			string connectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;

			System.Data.SqlClient.SqlConnection conn = null;
			try
			{
				if ((ClearBeforeFill == true))
				{
					dataTable.Clear();
				}

				try
				{
					using (conn = new System.Data.SqlClient.SqlConnection(connectionString))
					{

						using (var com = conn.CreateCommand())
						{
							com.CommandType = CommandType.Text;
							com.CommandText = "SELECT * FROM " + Constants.Common.TableName_FASK_Machines;
							conn.Open();

							using (var adapter = new System.Data.SqlClient.SqlDataAdapter())
							{
								adapter.SelectCommand = com;
								int returnValue = adapter.Fill(dataTable);
							}
						}
					}

					return true;
				}
				catch (Exception ex)
				{
					Fask.Logging.ExceptionHandler2.Handle(ex);
					//Log.WriteException(ex);
					return false;
				}
				finally
				{
					if ((conn.State & ConnectionState.Open) == ConnectionState.Open)
					{
						conn.Close();
						conn.Dispose();
					}
				}

			}
			catch (Exception ex)
			{
				//todo zalogovat exception
				string LOG = ex.Message.ToString();
				return false;
			}
		}

		#endregion

		#region FASK_MachineType

		[HttpGet]
		[Route("api/SV_Fill_FASK_MachineType")]
		public Fask.SQLiteDBs.DataSets.Vyroba.FASK_MachineTypeDataTable SV_Fill_FASK_MachineType()
		{
			Fask.SQLiteDBs.DataSets.Vyroba.FASK_MachineTypeDataTable dataTable = new Fask.SQLiteDBs.DataSets.Vyroba.FASK_MachineTypeDataTable();

			Fill_FASK_MachineType(dataTable);

			return dataTable;
		}

		public bool Fill_FASK_MachineType(Fask.SQLiteDBs.DataSets.Vyroba.FASK_MachineTypeDataTable dataTable, bool ClearBeforeFill = true)
		{
			string connectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;

			System.Data.SqlClient.SqlConnection conn = null;
			try
			{
				if ((ClearBeforeFill == true))
				{
					dataTable.Clear();
				}

				try
				{
					using (conn = new System.Data.SqlClient.SqlConnection(connectionString))
					{

						using (var com = conn.CreateCommand())
						{
							com.CommandType = CommandType.Text;
							com.CommandText = "SELECT * FROM " + Constants.Common.TableName_FASK_MachinesType;
							conn.Open();

							using (var adapter = new System.Data.SqlClient.SqlDataAdapter())
							{
								adapter.SelectCommand = com;
								int returnValue = adapter.Fill(dataTable);
							}
						}
					}

					return true;
				}
				catch (Exception ex)
				{
					Fask.Logging.ExceptionHandler2.Handle(ex);
					//Log.WriteException(ex);
					return false;
				}
				finally
				{
					if ((conn.State & ConnectionState.Open) == ConnectionState.Open)
					{
						conn.Close();
						conn.Dispose();
					}
				}

			}
			catch (Exception ex)
			{
				//todo zalogovat exception
				string LOG = ex.Message.ToString();
				return false;
			}
		}

		#endregion

		#region CZPRO_VPP

		[HttpGet]
		[Route("api/SV_getVyroba_CZPRO_VPP")]
		public Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPDataTable getVyroba_CZPRO_VPP()
		{
			string connectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;

			SqlConnection conn = null;
			Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPDataTable dt = new Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPDataTable();
			try
			{
				using (conn = new SqlConnection(connectionString))
				{
					using (var comm = conn.CreateCommand())
					{
						comm.CommandType = CommandType.Text;
						comm.CommandText = "SELECT * FROM " + Constants.Common.TableName_CZPRO_VPP_View;

						conn.Open();

						using (var adapter = new SqlDataAdapter())
						{
							adapter.SelectCommand = comm;
							int returnValue = adapter.Fill(dt);
						}
					}
				}

				return dt;

			}
			catch (System.Exception ex)
			{
				throw ex;
			}
			finally
			{
				if ((conn.State & ConnectionState.Open) == ConnectionState.Open)
				{
					conn.Close();
					conn.Dispose();
				}
			}
		}


		#endregion

		#region CZPRO_VPH

		[HttpGet]
		[Route("api/SV_getVyroba_CZPRO_VPH")]
		public Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHDataTable getVyroba_CZPRO_VPH()
		{
			string connectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;

			SqlConnection conn = null;
			Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHDataTable dt = new Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHDataTable();
			try
			{
				using (conn = new SqlConnection(connectionString))
				{
					using (var comm = conn.CreateCommand())
					{
						comm.CommandType = CommandType.Text;
						comm.CommandText = "SELECT * FROM " + Constants.Common.TableName_CZPRO_VPH_View;

						conn.Open();

						using (var adapter = new SqlDataAdapter())
						{
							adapter.SelectCommand = comm;
							int returnValue = adapter.Fill(dt);
						}
					}
				}

				return dt;

			}
			catch (System.Exception ex)
			{
				throw ex;
			}
			finally
			{
				if ((conn.State & ConnectionState.Open) == ConnectionState.Open)
				{
					conn.Close();
					conn.Dispose();
				}
			}
		}

		#endregion


		#region FASK_Events

		[HttpPost]
		[Route("api/SV_GetFASK_Events_ByStatus")]
		public Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable SV_GetFASK_Events_ByStatus([FromBody] Fask.WEBAPI.API_BusinessObjects.Filtr_FASK_Events filtr)
		{
			if (filtr != null)
			{

				Init();

				if (provider != null && provider is Fask.Interfaces.Vyroba.API.IVyroba_PostFaskEventsRow)
				{
					try
					{
						return ((Fask.Interfaces.Vyroba.API.IVyroba_PostFaskEventsRow)provider).GetFASKEventsRows(filtr);


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

		[HttpPost]
		[Route("api/SV_FASK_EventsInsert")]
		public int FASK_EventsInsert
			([FromBody] Fask.WEBAPI.API_BusinessObjects.FASK_Events_row dataObject
	)
		{

			System.Data.SqlClient.SqlConnection conn = null;
			string connectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;



			try
			{
				#region inicializace parametrů

				string loginid = dataObject.loginid;
				string machineid = dataObject.machineid;
				System.DateTime dateeve = (System.DateTime)dataObject.dateeve;
				decimal qty = dataObject.qty;
				decimal qtyReal = dataObject.qtyReal;
				string description = dataObject.description;
				string barcodeReaded = dataObject.barcodeReaded;
				string barcodeSended = dataObject.barcodeSended;
				string zakazka = dataObject.zakazka;
				string popis = dataObject.popis;
				System.Guid faskGUID = (System.Guid)dataObject.faskGUID;
				string reportType = dataObject.reportType;
				string IDO = dataObject.IDO;
				string scan1 = dataObject.scan1;
				string scan2 = dataObject.scan2;
				string scan3 = dataObject.scan3;
				string sensor = dataObject.sensor;
				string material = dataObject.material;
				string VPH = dataObject.VPH;
				int? VPPol = dataObject.VPPol;
				string EAN_IS = dataObject.EAN_IS;
				string IS_ID = dataObject.IS_ID;
				int? status = dataObject.status;
				string NMBRPAL = dataObject.NMBRPAL;
				global::System.Guid? productionGuid = dataObject.productionGuid;
				decimal QTYPACK = dataObject.QTYPACK;
				string PackType = dataObject.PackType;
				decimal? WEIGHT = dataObject.WEIGHT;

				byte BarcodeT = dataObject.BarcodeT;
				string REZ_1 = dataObject.REZ_1;
				string REZ_2 = dataObject.REZ_2;
				string REZ_3 = dataObject.REZ_3;
				string REZ_4 = dataObject.REZ_4;
				string REZ_5 = dataObject.REZ_5;
				#endregion

				using (conn = new System.Data.SqlClient.SqlConnection(connectionString))
				{
					using (var cmd = conn.CreateCommand())
					{

						cmd.Connection = conn;
						cmd.CommandType = System.Data.CommandType.Text;
						//cmd.CommandText = @"INSERT INTO " + Constants.Common.TableName_FASK_Events + " (" +
						//	" loginid, machineid, dateeve, qty, qtyReal, " +
						//	" description, barcodeReaded, barcodeSended, zakazka, popis, " +
						//	" faskGUID, reportType, IDO, scan1, scan2, " +
						//	" scan3, sensor, material, VPH, VPPol, " +
						//	" EAN_IS, IS_ID, NMBRPAL, status, QTYPACK, " +
						//	" PackType, WEIGHT " +
						//	" ) VALUES ( " +
						//	" @loginid, @machineid, @dateeve, @qty, @qtyReal, " +
						//	" @description, @barcodeReaded, @barcodeSended, @zakazka, @popis, " +
						//	" @faskGUID, @reportType, @IDO, @scan1, @scan2, " +
						//	" @scan3, @sensor, @material, @VPH, @VPPol, " +
						//	" @EAN_IS, @IS_ID, @NMBRPAL, @status, @QTYPACK, " +
						//	" @PackType, @WEIGHT " +
						//	" )";

						cmd.CommandText = @"INSERT INTO " + Constants.Common.TableName_FASK_Events + " (" +
	" loginid, machineid, dateeve, qty, qtyReal, " +
	" description, barcodeReaded, barcodeSended, zakazka, popis, " +
	" faskGUID, reportType, IDO, scan1, scan2, " +
	" scan3, sensor, material, VPH, VPPol, " +
	" EAN_IS, IS_ID, NMBRPAL, status, QTYPACK, " +
	" PackType, WEIGHT, BarcodeT, REZ_1, REZ_2, REZ_3, REZ_4, REZ_5 " +
	" ) VALUES ( " +
	" @loginid, @machineid, @dateeve, @qty, @qtyReal, " +
	" @description, @barcodeReaded, @barcodeSended, @zakazka, @popis, " +
	" @faskGUID, @reportType, @IDO, @scan1, @scan2, " +
	" @scan3, @sensor, @material, @VPH, @VPPol, " +
	" @EAN_IS, @IS_ID, @NMBRPAL, @status, @QTYPACK, " +
	" @PackType, @WEIGHT, " +
	" @BarcodeT, @REZ_1, @REZ_2, @REZ_3, @REZ_4, @REZ_5 " +
	" )";


						cmd.Parameters.AddWithValue("@loginid", string.IsNullOrEmpty(loginid) ? string.Empty : (object)loginid.Trim());
						cmd.Parameters.AddWithValue("@machineid", string.IsNullOrEmpty(machineid) ? string.Empty : (object)machineid.Trim());
						cmd.Parameters.AddWithValue("@dateeve", dateeve);
						cmd.Parameters.AddWithValue("@qty", qty);
						cmd.Parameters.AddWithValue("@qtyReal", qtyReal);

						cmd.Parameters.AddWithValue("@description", string.IsNullOrEmpty(description) ? DBNull.Value : (object)description.Trim());
						cmd.Parameters.AddWithValue("@barcodeReaded", string.IsNullOrEmpty(barcodeReaded) ? string.Empty : (object)barcodeReaded.Trim());
						cmd.Parameters.AddWithValue("@barcodeSended", string.IsNullOrEmpty(barcodeSended) ? string.Empty : (object)barcodeSended.Trim());
						cmd.Parameters.AddWithValue("@zakazka", string.IsNullOrEmpty(zakazka) ? DBNull.Value : (object)zakazka.Trim());
						cmd.Parameters.AddWithValue("@popis", string.IsNullOrEmpty(popis) ? DBNull.Value : (object)popis.Trim());

						cmd.Parameters.AddWithValue("@faskGUID", faskGUID);
						cmd.Parameters.AddWithValue("@reportType", string.IsNullOrEmpty(reportType) ? string.Empty : (object)reportType.Trim());
						cmd.Parameters.AddWithValue("@IDO", string.IsNullOrEmpty(IDO) ? DBNull.Value : (object)IDO.Trim());
						cmd.Parameters.AddWithValue("@scan1", string.IsNullOrEmpty(scan1) ? DBNull.Value : (object)scan1.Trim());
						cmd.Parameters.AddWithValue("@scan2", string.IsNullOrEmpty(scan2) ? DBNull.Value : (object)scan2.Trim());

						cmd.Parameters.AddWithValue("@scan3", string.IsNullOrEmpty(scan3) ? DBNull.Value : (object)scan3.Trim());
						cmd.Parameters.AddWithValue("@sensor", string.IsNullOrEmpty(sensor) ? DBNull.Value : (object)sensor.Trim());
						cmd.Parameters.AddWithValue("@material", string.IsNullOrEmpty(material) ? DBNull.Value : (object)material.Trim());
						cmd.Parameters.AddWithValue("@VPH", string.IsNullOrEmpty(VPH) ? DBNull.Value : (object)VPH.Trim());
						cmd.Parameters.AddWithValue("@VPPol", VPPol.HasValue ? (object)VPPol.Value : DBNull.Value);

						cmd.Parameters.AddWithValue("@EAN_IS", string.IsNullOrEmpty(EAN_IS) ? DBNull.Value : (object)EAN_IS.Trim());
						cmd.Parameters.AddWithValue("@IS_ID", string.IsNullOrEmpty(IS_ID) ? DBNull.Value : (object)IS_ID.Trim());
						cmd.Parameters.AddWithValue("@NMBRPAL", string.IsNullOrEmpty(NMBRPAL) ? DBNull.Value : (object)NMBRPAL.Trim());
						cmd.Parameters.AddWithValue("@status", status.HasValue ? (object)status.Value : DBNull.Value);
						cmd.Parameters.AddWithValue("@QTYPACK", QTYPACK);

						cmd.Parameters.AddWithValue("@PackType", string.IsNullOrEmpty(PackType) ? DBNull.Value : (object)PackType.Trim());
						cmd.Parameters.AddWithValue("@WEIGHT", WEIGHT.HasValue ? (object)WEIGHT.Value : DBNull.Value);

						cmd.Parameters.AddWithValue("@BarcodeT", BarcodeT);
						cmd.Parameters.AddWithValue("@REZ_1", string.IsNullOrEmpty(REZ_1) ? DBNull.Value : (object)REZ_1.Trim());
						cmd.Parameters.AddWithValue("@REZ_2", string.IsNullOrEmpty(REZ_2) ? DBNull.Value : (object)REZ_2.Trim());
						cmd.Parameters.AddWithValue("@REZ_3", string.IsNullOrEmpty(REZ_3) ? DBNull.Value : (object)REZ_3.Trim());
						cmd.Parameters.AddWithValue("@REZ_4", string.IsNullOrEmpty(REZ_4) ? DBNull.Value : (object)REZ_4.Trim());
						cmd.Parameters.AddWithValue("@REZ_5", string.IsNullOrEmpty(REZ_5) ? DBNull.Value : (object)REZ_5.Trim());

						conn.Open();

						//return cmd.ExecuteNonQuery() > 0 ? true : false;
						return cmd.ExecuteNonQuery();
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if ((conn.State & ConnectionState.Open) == ConnectionState.Open)
				{
					conn.Close();
					conn.Dispose();
				}
			}
		}

		[HttpPost]
		[Route("api/SV_FASK_Events_CountGUID")]
		public int? FASK_Events_CountGUID([FromBody] Guid Guid)
		{
			SqlConnection conn = null;
			string connectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;

			try
			{
				using (conn = new SqlConnection(connectionString))
				{
					using (var comm = conn.CreateCommand())
					{
						comm.CommandType = CommandType.Text;
						comm.CommandText = "SELECT COUNT(*) FROM FASK_Events WHERE faskGUID = @Guid";

						comm.Parameters.Add(new SqlParameter() { ParameterName = "@Guid", DbType = System.Data.DbType.Guid, Value = Guid });


						conn.Open();

						object returnValue = comm.ExecuteScalar();

						if (((returnValue == null) || (returnValue.GetType() == typeof(global::System.DBNull))))
						{
							return null;
						}
						else
						{
							return (int)returnValue;
						}
					}
				}
			}
			catch (System.Exception ex)
			{
				throw ex;
			}
			finally
			{
				if ((conn.State & ConnectionState.Open) == ConnectionState.Open)
				{
					conn.Close();
					conn.Dispose();
				}
			}
		}

		#endregion

		#region FASK_UserEvents

		[HttpPost]
		[Route("api/SV_FASK_UserEvents_CountGUID")]
		public int? FASK_UserEvents_CountGUID([FromBody] Guid guid)
		{
			SqlConnection conn = null;
			string connectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;

			try
			{
				using (conn = new SqlConnection(connectionString))
				{
					using (var comm = conn.CreateCommand())
					{
						comm.CommandType = CommandType.Text;
						comm.CommandText = "SELECT COUNT(*) FROM " + Constants.Common.TableName_FASK_UserEvents + " WHERE faskGUID = @Guid";

						comm.Parameters.Add(new SqlParameter() { ParameterName = "@Guid", DbType = System.Data.DbType.Guid, Value = guid });


						conn.Open();

						object returnValue = comm.ExecuteScalar();

						if (((returnValue == null) || (returnValue.GetType() == typeof(global::System.DBNull))))
						{
							return null;
						}
						else
						{
							return (int)returnValue;
						}
					}
				}
			}
			catch (System.Exception ex)
			{
				throw ex;
			}
			finally
			{
				if ((conn.State & ConnectionState.Open) == ConnectionState.Open)
				{
					conn.Close();
					conn.Dispose();
				}
			}
		}

		[HttpPost]
		[Route("api/SV_FASK_UserEventsInsert")]
		public bool FASK_UserEventsInsert([FromBody] Fask.WEBAPI.API_BusinessObjects.FASK_UserEvents dataObject
			)
		{

			System.Data.SqlClient.SqlConnection conn = null;
			string connectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;

			try
			{
				#region inicializace parametrů

				string loginid = dataObject.loginid;
				string machineid = dataObject.machineid;
				System.DateTime dateeve = dataObject.dateeve;
			    string statusid = dataObject.statusid;
				System.Guid faskGUID = dataObject.faskGUID;
				string rez_1 = dataObject.rez_1;
				string rez_2 = dataObject.rez_2;
				#endregion

				using (conn = new System.Data.SqlClient.SqlConnection(connectionString))
				{
					using (var cmd = conn.CreateCommand())
					{

						cmd.Connection = conn;
						cmd.CommandType = System.Data.CommandType.Text;
						cmd.CommandText = " INSERT INTO " +
										Constants.Common.TableName_FASK_UserEvents +
										" (loginid, machineid, dateeve, statusid, faskGUID, rez_1, rez_2" +
										" ) VALUES( " +
										" @loginid, @machineid, @dateeve, @statusid, @faskGUID, @rez_1, @rez_2)";


						cmd.Parameters.AddWithValue("@loginid", string.IsNullOrEmpty(loginid) ? string.Empty : (object)loginid.Trim());
						cmd.Parameters.AddWithValue("@machineid", string.IsNullOrEmpty(machineid) ? DBNull.Value : (object)machineid.Trim());
						cmd.Parameters.AddWithValue("@dateeve", dateeve);
						cmd.Parameters.AddWithValue("@statusid", string.IsNullOrEmpty(statusid) ? DBNull.Value : (object)statusid.Trim());
						cmd.Parameters.AddWithValue("@faskGUID", faskGUID);
						cmd.Parameters.AddWithValue("@rez_1", string.IsNullOrEmpty(rez_1) ? DBNull.Value : (object)rez_1.Trim());
						cmd.Parameters.AddWithValue("@rez_2", string.IsNullOrEmpty(rez_2) ? DBNull.Value : (object)rez_2.Trim());

						conn.Open();

						return cmd.ExecuteNonQuery() > 0 ? true : false;
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if ((conn.State & ConnectionState.Open) == ConnectionState.Open)
				{
					conn.Close();
					conn.Dispose();
				}
			}
		}

		#endregion


		#region FASK_Operations

		[HttpGet]
		[Route("api/SV_Fill_FASK_Operations")]
		public Fask.SQLiteDBs.DataSets.Vyroba.FASK_OperationsDataTable SV_Fill_FASK_Operations()
		{
			Fask.SQLiteDBs.DataSets.Vyroba.FASK_OperationsDataTable dataTable = new Fask.SQLiteDBs.DataSets.Vyroba.FASK_OperationsDataTable();

			Fill_FASK_Operations(dataTable);

			return dataTable;
		}

		public bool Fill_FASK_Operations(Fask.SQLiteDBs.DataSets.Vyroba.FASK_OperationsDataTable dataTable, bool ClearBeforeFill = true)
		{
			string connectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;

			System.Data.SqlClient.SqlConnection conn = null;
			try
			{
				if ((ClearBeforeFill == true))
				{
					dataTable.Clear();
				}

				try
				{
					using (conn = new System.Data.SqlClient.SqlConnection(connectionString))
					{

						using (var com = conn.CreateCommand())
						{
							com.CommandType = CommandType.Text;
							com.CommandText = "SELECT * FROM " + Constants.Common.TableName_FASK_Operations;
							conn.Open();

							using (var adapter = new System.Data.SqlClient.SqlDataAdapter())
							{
								adapter.SelectCommand = com;
								int returnValue = adapter.Fill(dataTable);
							}
						}
					}

					return true;
				}
				catch (Exception ex)
				{
					Fask.Logging.ExceptionHandler2.Handle(ex);
					//Log.WriteException(ex);
					return false;
				}
				finally
				{
					if ((conn.State & ConnectionState.Open) == ConnectionState.Open)
					{
						conn.Close();
						conn.Dispose();
					}
				}

			}
			catch (Exception ex)
			{
				//todo zalogovat exception
				string LOG = ex.Message.ToString();
				return false;
			}
		}


		#endregion

		#region FASK_Operations_Next

		[HttpGet]
		[Route("api/SV_Fill_FASK_Operations_Next")]
		public Fask.SQLiteDBs.DataSets.Vyroba.FASK_Operations_NextDataTable SV_Fill_FASK_Operations_Next()
		{
			Fask.SQLiteDBs.DataSets.Vyroba.FASK_Operations_NextDataTable dataTable = new Fask.SQLiteDBs.DataSets.Vyroba.FASK_Operations_NextDataTable();

			Fill_FASK_Operations_Next(dataTable);

			return dataTable;
		}

		public bool Fill_FASK_Operations_Next(Fask.SQLiteDBs.DataSets.Vyroba.FASK_Operations_NextDataTable dataTable, bool ClearBeforeFill = true)
		{
			string connectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;

			System.Data.SqlClient.SqlConnection conn = null;
			try
			{
				if ((ClearBeforeFill == true))
				{
					dataTable.Clear();
				}

				try
				{
					using (conn = new System.Data.SqlClient.SqlConnection(connectionString))
					{

						using (var com = conn.CreateCommand())
						{
							com.CommandType = CommandType.Text;
							com.CommandText = "SELECT * FROM " + Constants.Common.TableName_FASK_Operations_Next;
							conn.Open();

							using (var adapter = new System.Data.SqlClient.SqlDataAdapter())
							{
								adapter.SelectCommand = com;
								int returnValue = adapter.Fill(dataTable);
							}
						}
					}

					return true;
				}
				catch (Exception ex)
				{
					Fask.Logging.ExceptionHandler2.Handle(ex);
					//Log.WriteException(ex);
					return false;
				}
				finally
				{
					if ((conn.State & ConnectionState.Open) == ConnectionState.Open)
					{
						conn.Close();
						conn.Dispose();
					}
				}

			}
			catch (Exception ex)
			{
				//todo zalogovat exception
				string LOG = ex.Message.ToString();
				return false;
			}
		}


		#endregion

		#region MyRegion

		[HttpGet]
		[Route("api/ReturnSarze/{smenaID}/{userID}/{linkaID}")]
		public HttpResponseMessage ReturnSarze(string smenaID, string userID, string linkaID)
		{

			Init();

			try
			{
				if (provider != null && provider is Fask.Interfaces.Vyroba.API.IVyroba_Sarze)
				{
					try
					{
						string txt = ((Fask.Interfaces.Vyroba.API.IVyroba_Sarze)provider).ReturnSarze(smenaID, userID, linkaID);
						return Request.CreateResponse<string>(HttpStatusCode.OK, txt);
					}
					catch (Exception ex)
					{
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


		[HttpGet]
		[Route("api/ReturnID/{inID}")]
		public HttpResponseMessage ReturnID(string inID)
		{

			Init();

			try
			{
				if (provider != null && provider is Fask.Interfaces.Vyroba.API.IVyroba_Sarze)
				{
					try
					{
						bool txt = ((Fask.Interfaces.Vyroba.API.IVyroba_Sarze)provider).ReturnID(inID);
						return Request.CreateResponse<bool>(HttpStatusCode.OK, txt);
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

		[HttpGet]
		[Route("api/ReturnHeslo/{inHESLO}/{inID}")]
		public HttpResponseMessage ReturnHeslo(string inHESLO, string inID)
		{

			Init();

			try
			{
				if (provider != null && provider is Fask.Interfaces.Vyroba.API.IVyroba_Sarze)
				{
					try
					{
						bool txt = ((Fask.Interfaces.Vyroba.API.IVyroba_Sarze)provider).ReturnHeslo(inHESLO, inID);
						return Request.CreateResponse<bool>(HttpStatusCode.OK, txt);
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


		#endregion

		#endregion

	}
}