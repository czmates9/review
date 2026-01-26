using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MES_Android.Classes;
using MES_Android.ServerAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MES_Android.OdvadeniVyroby
{
	public class GlobalObject : IDisposable
	{
		//Controllery
		public Fask.SQLiteDBs.Controllers.SQLite_Controller_InternalState controller_InternalState;
		public Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba controller_Vyroba__Production_PRD;
		public Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba controller_Vyroba__Production_PRDTMP;
		public Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba controller_Vyroba__Vyroba_PRD;
		public Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba controller_Vyroba__Vyroba_PRDTMP;

		//Webobe sluzby
		public _WebReferences_Globals.VyrobaBaleniSession baleniService;
		public _WebReferences_Globals.VyrobaSession vyrobaServis;
		public _WebReferences_Globals.VydejServiceSession vydejServis;

		public GlobalObject()
		{

			//controller_InternalState = new Fask.SQLiteDBs.Controllers.SQLite_Controller_InternalState(DataInfo_Static.InternalStatePrdDB);
			//controller_Vyroba__Production_PRD = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba(DataInfo_Static.ProductionPrdDB);
			//controller_Vyroba__Production_PRDTMP = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba(DataInfo_Static.ProductionPrdDB);
			//controller_Vyroba__Vyroba_PRD = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba(DataInfo_Static.VyrobaPrdDB);
			//controller_Vyroba__Vyroba_PRDTMP = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba(DataInfo_Static.VyrobaPrdTmpDB);

			controller_InternalState = new Fask.SQLiteDBs.Controllers.SQLite_Controller_InternalState(DataInfo_Static.InternalStateDB);
			controller_Vyroba__Production_PRD = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba(DataInfo_Static.ProductionDB);
			controller_Vyroba__Production_PRDTMP = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba(DataInfo_Static.ProductionDBTMP);
			controller_Vyroba__Vyroba_PRD = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba(DataInfo_Static.OdvadeniVyrobyDB);
			controller_Vyroba__Vyroba_PRDTMP = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba(DataInfo_Static.OdvadeniVyrobyDBTMP);

			try
			{
				baleniService = new _WebReferences_Globals.VyrobaBaleniSession();
				baleniService.Url = Config.Settings.Adresa + "Baleni.asmx";
				baleniService.Timeout = Config.Settings.TimeOut;
				baleniService.UpdateWebServiceCredentials();

				vyrobaServis = new _WebReferences_Globals.VyrobaSession();
				vyrobaServis.Url = Config.Settings.Adresa + "Vyroba.asmx";
				vyrobaServis.Timeout = Config.Settings.TimeOut;
				vyrobaServis.UpdateWebServiceCredentials();

				vydejServis = new _WebReferences_Globals.VydejServiceSession();
				vydejServis.Url = Config.Settings.Adresa + "Vydej.asmx";
				vydejServis.Timeout = Config.Settings.TimeOut;
				vydejServis.UpdateWebServiceCredentials();
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
			}

		}



		#region IDisposable Members

		public void Dispose()
		{
			if (controller_InternalState != null) controller_InternalState.Dispose();

			if (controller_Vyroba__Production_PRD != null) controller_Vyroba__Production_PRD.Dispose();
			if (controller_Vyroba__Production_PRDTMP != null) controller_Vyroba__Production_PRDTMP.Dispose();
			if (controller_Vyroba__Vyroba_PRD != null) controller_Vyroba__Vyroba_PRD.Dispose();
			if (controller_Vyroba__Vyroba_PRDTMP != null) controller_Vyroba__Vyroba_PRDTMP.Dispose();

			controller_InternalState = null;
			controller_Vyroba__Production_PRD = null;
			controller_Vyroba__Production_PRDTMP = null;
			controller_Vyroba__Vyroba_PRD = null;
			controller_Vyroba__Vyroba_PRDTMP = null;

			if (baleniService != null) baleniService.Dispose();
			if (vyrobaServis != null) vyrobaServis.Dispose();

			baleniService = null;
			vyrobaServis = null;
		}

		#endregion



		/// <summary>
		/// Deletes data from sqlite production.prd database based on guid of row that was processed 
		/// <param name="produtionprdtmp_processed">full path to filename that was processed/sended</param>
		/// </summary>
		public void DeleteProductionByGuid(string produtionprdtmp_processed)
		{
			try
			{
				using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba ConVyr = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba(DataInfo_Static.ProductionDB))
				using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba ConVyrTMP = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba(produtionprdtmp_processed))
				{
					ConVyr.Connection_Open();
					ConVyrTMP.Connection_Open();

					try
					{
						using (var preader = ConVyrTMP.GetReaderGUID_Production())
						{

							while (preader.Read())
							{
								object GUIDobject = preader[0];

								if (GUIDobject != null && GUIDobject is Guid)
								{
									Guid guid = (Guid)GUIDobject;
									ConVyr.DeleteByGUID_Production(guid);
								}
							}
						}
					}
					catch (Exception ex)
					{
						Fask.Logging.ExceptionHandler2.Handle(ex);
					}
					finally
					{
						ConVyr.Connection_Close();
						ConVyrTMP.Connection_Close();
					}
				}
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
			}
		}

		/// <summary>
		/// Deletes user events rows from produtction database, by processed/sended data
		/// </summary>
		/// <param name="produtionprdtmp_processed">full filename to processed/sended file</param>
		public void DeleteUserEventsByGuid(string produtionprdtmp_processed)
		{
			try
			{
				using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba ConVyr = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba(DataInfo_Static.ProductionDB))
				using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba ConVyrTMP = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba(produtionprdtmp_processed))
				{
					ConVyr.Connection_Open();
					ConVyrTMP.Connection_Open();

					try
					{
						using (var preader = ConVyrTMP.GetReaderGUID_UserEvents())
						{

							while (preader.Read())
							{
								object GUIDobject = preader[0];

								if (GUIDobject != null && GUIDobject is Guid)
								{
									Guid guid = (Guid)GUIDobject;
									ConVyr.DeleteByGUID_UserEvents(guid);
								}
							}
						}
					}
					catch (Exception ex)
					{
						Fask.Logging.ExceptionHandler2.Handle(ex);
					}
					finally
					{
						ConVyr.Connection_Close();
						ConVyrTMP.Connection_Close();
					}
				}
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
			}
		}

		/// <summary>
		/// Deletes production_sources rows from produtction database, by processed/sended data
		/// </summary>
		/// <param name="produtionprdtmp_processed">full filename to processed/sended file</param>
		public void DeleteProduction_SourcesByGuid(string produtionprdtmp_processed)
		{
			try
			{
				// TODO : revidovat
				using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba ConVyr = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba(DataInfo_Static.ProductionDB))
				using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba ConVyrTMP = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba(produtionprdtmp_processed))
				{
					ConVyr.Connection_Open();
					ConVyrTMP.Connection_Open();

					try
					{
						using (var preader = ConVyrTMP.GetReaderGUID_Production_Sources())
						{

							while (preader.Read())
							{
								object GUIDobject = preader[0];

								if (GUIDobject != null && GUIDobject is Guid)
								{
									Guid guid = (Guid)GUIDobject;
									ConVyr.DeleteByGUID_Production_Sources(guid);
								}
							}
						}
					}
					catch (Exception ex)
					{
						Fask.Logging.ExceptionHandler2.Handle(ex);
					}
					finally
					{
						ConVyr.Connection_Close();
						ConVyrTMP.Connection_Close();
					}
				}
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
			}
		}


		/// <summary>
		/// Deletes production_SN rows from produtction database, by processed/sended data
		/// </summary>
		/// <param name="produtionprdtmp_processed">full filename to processed/sended file</param>
		public void DeleteProduction_SNByGuid(string produtionprdtmp_processed)
		{
			try
			{
				// TODO : revidovat
				using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba ConVyr = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba(DataInfo_Static.ProductionDB))
				using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba ConVyrTMP = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba(produtionprdtmp_processed))
				{
					ConVyr.Connection_Open();
					ConVyrTMP.Connection_Open();

					try
					{
						using (var preader = ConVyrTMP.GetReaderGUID_Production_SN())
						{

							while (preader.Read())
							{
								object GUIDobject = preader[0];

								if (GUIDobject != null && GUIDobject is Guid)
								{
									Guid guid = (Guid)GUIDobject;
									ConVyr.DeleteByGUID_Production_SN(guid);
								}
							}
						}
					}
					catch (Exception ex)
					{
						Fask.Logging.ExceptionHandler2.Handle(ex);
					}
					finally
					{
						ConVyr.Connection_Close();
						ConVyrTMP.Connection_Close();
					}
				}
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
			}
		}

	}
}