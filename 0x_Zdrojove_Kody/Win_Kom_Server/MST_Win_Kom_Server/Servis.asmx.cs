using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Reflection;
using Fask.Server.Interfaces.Classes;
using System.Data;
using System.IO;
using System.Security.Principal;
using System.Runtime.InteropServices;
using Fask.Logging;
using Fask.Tracing;
using Fask.MST_W_Server.Constants;
using Fask.MST_W_Server.SQLite_Classes;

namespace Fask.MST_W_Server
{
    /// <summary>
	/// Služba přenosu dat servisu
    /// </summary>
    //[WebService(Namespace = "http://servis.fask.cz/")]
    [WebService(Namespace = "http://servis.fask.cz/", Description = "Služba přenosu dat servisu", Name = "ServisModule")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ServisModule : System.Web.Services.WebService
    {
		#region Lokalne promenne

		Fask.Server.Interfaces.Servis.IServis provider = null;
		const string ServisDBFileExtension = @".si";

		#endregion

		#region Konstruktor

		/// <summary>
		/// Konstruktor
		/// </summary>
		public ServisModule()
		{
			// Inicializuje objektove rozhrani ...
			try
			{
                Konfigurace.Classes.Globals_Konfig_WebConfig.LoadConfiguration();
                string providerAssemblyPath = Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Providers[0].Provider_Servis;
                string providerAssemblyPathGlobal = Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Providers[0].Provider;
                if (String.IsNullOrEmpty(providerAssemblyPath))
					providerAssemblyPath = providerAssemblyPathGlobal;

				if (!String.IsNullOrEmpty(providerAssemblyPath))
				{
					if (provider == null) //inicializace se provede pouze pokud nebyla provedena ... 
					{
						Assembly providerAssemlby = Assembly.LoadFrom(Server.MapPath(providerAssemblyPath));
						Type[] types = providerAssemlby.GetTypes();
						foreach (Type t in types)
						{
							try
							{
								//t.IsAssignableFrom(typeof(Fask.Server.Interfaces.Vydej.IVydej);
								if (typeof(Fask.Server.Interfaces.Servis.IServis).IsAssignableFrom(t))
								{
									provider = (Fask.Server.Interfaces.Servis.IServis)providerAssemlby.CreateInstance(t.FullName);
									if (provider != null)
										break;
								}
							}
							catch (Exception ex)
							{
								Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
								//throw ex;
							}
						}
						//return config;
					}
				}
			}
			catch { }

		}
		
		#endregion



        [WebMethod]
        public StatusObject ProcessState(byte terminalID, ref Fask.Server.Interfaces.Servis.Zdroj zdroj)
        {
            StatusObject so = new StatusObject();
            so.StatusText = "Updating ...";
            try
            {
                if (provider != null)
                {
                    StatusInfo siprov = provider.Servis_ProcessState(ref zdroj);
                }
                so.SetOK();
            }
            catch (Exception ex)
            {
				
                so.Exception = true;
                so.StatusText = ex.Message;
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "TID:"+ terminalID);
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
            return so;
        }

        [WebMethod]
        public StatusObject Prepare_Ciselniky(byte terminalID)
        {
            Fask.DataSets.Servis servisDS = null;

            StatusObject so = new StatusObject();
            so.StatusText = "Updating ...";
            try
            {
                if (provider != null)
                {
                    Terminal terminal = new Terminal();
                    terminal.ID = terminalID;
                    servisDS = provider.Servis_Prepare_Ciselniky(terminal, ref so);
                }

                string dstFile = Path.Combine(Fask.MyPath.Path.SQLiteDBsDirectory, terminalID.ToString() + Common.Backslash + Common.Servis_Ciselniky + Common.PRD);

                if (!Directory.Exists(Path.GetDirectoryName(dstFile)))
                    Directory.CreateDirectory(Path.GetDirectoryName(dstFile));

				SQLite_Helper helper = new SQLite_Helper();
				if (!helper.SQLite_CreateFile(dstFile, Common.Servis_Ciselniky))
				{
					throw new Exception("Nastala chyba pri tvorbe SQLite souboru pro " + Common.Servis_Ciselniky);
				}

				//SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_ZdrojTableAdapter ta_Zdroj = new Fask.MST_W_Server.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_ZdrojTableAdapter();
				//SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_StavTableAdapter ta_Stav = new Fask.MST_W_Server.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_StavTableAdapter();
				//SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_StavNextTableAdapter ta_StavNext = new Fask.MST_W_Server.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_StavNextTableAdapter();
				//SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_CinnostTableAdapter ta_Cinnost = new Fask.MST_W_Server.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_CinnostTableAdapter();
				//SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_CinnostNextTableAdapter ta_CinnostNext = new Fask.MST_W_Server.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_CinnostNextTableAdapter();
				//SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_Dynamic_Table_DefinitionTableAdapter ta_DynTabDef = new Fask.MST_W_Server.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_Dynamic_Table_DefinitionTableAdapter();
				//SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_OkruhTableAdapter ta_Okruh = new Fask.MST_W_Server.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_OkruhTableAdapter();
				//SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_ZdrojSeznamTableAdapter ta_ZdrojSeznam = new Fask.MST_W_Server.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_ZdrojSeznamTableAdapter();

				//ta_Zdroj.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + dstFile);
                //ta_StavNext.Connection = ta_Zdroj.Connection;
                //ta_Stav.Connection = ta_Zdroj.Connection;
                //ta_Cinnost.Connection = ta_Zdroj.Connection;
                //ta_CinnostNext.Connection = ta_Zdroj.Connection;
                //ta_DynTabDef.Connection = ta_Zdroj.Connection;
                //ta_Okruh.Connection = ta_Zdroj.Connection;
                //ta_ZdrojSeznam.Connection = ta_Zdroj.Connection;

				Fask.SQLiteDBs.DataSets.Servis dssqlservisciselniky = new Fask.SQLiteDBs.DataSets.Servis();

				//try
				//{
				//	var Tran = ta_Zdroj.Connection.BeginTransaction();

				//	foreach (var item in servisDS.CZMST_Servis_Zdroj)
				//	{
				//		ta_Zdroj.Insert(
				//			item.ID,
				//			item.Oznaceni,
				//			item.IsBarcodeNull() ? null : item.Barcode,
				//			item.IsTypeNull() ? null : item.Type,
				//			item.IsMistoNull() ? null : item.Misto
				//			);
				//	}

				//	foreach (var item in servisDS.CZMST_Servis_Stav)
				//	{
				//		ta_Stav.Insert(
				//			item.ID,
				//			item.Oznaceni,
				//			item.IsIDCinnostNull() ? null : item.IDCinnost,
				//			item.IsBarcodeNull() ? null : item.Barcode
				//			);
				//	}

				//	foreach (var item in servisDS.CZMST_Servis_StavNext)
				//	{
				//		ta_StavNext.Insert(
				//			item.ID,
				//			item.IDNext
				//			);
				//	}

				//	foreach (var item in servisDS.CZMST_Servis_Cinnost)
				//	{
				//		ta_Cinnost.Insert(
				//			item.ID,
				//			item.Oznaceni,
				//			item.IsBarcodeNull() ? null : item.Barcode,
				//			item.TYPE,
				//			item.IsTYPEVALUENull() ? null : item.TYPEVALUE,
				//			item.Mandatory,
				//			item.IsRequiredLengthNull() ? (int?)null : item.RequiredLength
				//			);
				//	}

				//	foreach (var item in servisDS.CZMST_Servis_CinnostNext)
				//	{
				//		ta_CinnostNext.Insert(
				//			item.ID,
				//			item.IsIDNextNull() ? null : item.IDNext,
				//			item.IsIDValueNull() ? null : item.IDValue
				//			);
				//	}

				//	foreach (var item in servisDS.CZMST_Servis_Dynamic_Table_Definition)
				//	{
				//		ta_DynTabDef.Insert(
				//			item.FullName,
				//			item.TypeName
				//			);
				//	}

				//	// okruh
				//	foreach (var item in servisDS.CZMST_Servis_Okruh)
				//	{
				//		ta_Okruh.Insert(
				//			item.ID,
				//			item.Oznaceni,
				//			item.IsODB_IDNull() ? null : item.ODB_ID,
				//			item.IsBarcodeNull() ? null : item.Barcode,
				//			item.ZdrojSeznamID
				//			);
				//	}

				//	// zdrojseznam
				//	foreach (var item in servisDS.CZMST_Servis_ZdrojSeznam)
				//	{
				//		ta_ZdrojSeznam.Insert(
				//			item.ID,
				//			item.ZdrojID,
				//			item.IsPoradiNull() ? (int?)null : item.Poradi,
				//			item.IsIDStavNull() ? null : item.IDStav,
				//			item.IsIDCinnostNull() ? null : item.IDCinnost
				//			);
				//	}

				//	Tran.Commit();
				//}
				//catch (Exception ex)
				//{
				//	Logging.ExceptionHandler2.Handle(ex);
				//}
				//finally
				//{

				//	if (ta_Zdroj != null)
				//	{
				//		if ((ta_Zdroj.Connection.State & ConnectionState.Open) == ConnectionState.Open)
				//			ta_Zdroj.Connection.Close();
				//		ta_Zdroj.Dispose();
				//	}

				//	if (ta_StavNext != null)
				//	{
				//		if ((ta_StavNext.Connection.State & ConnectionState.Open) == ConnectionState.Open)
				//			ta_StavNext.Connection.Close();
				//		ta_StavNext.Dispose();
				//	}

				//	if (ta_Stav != null)
				//	{
				//		if ((ta_Stav.Connection.State & ConnectionState.Open) == ConnectionState.Open)
				//			ta_Stav.Connection.Close();
				//		ta_Stav.Dispose();
				//	}

				//	if (ta_Cinnost != null)
				//	{
				//		if ((ta_Cinnost.Connection.State & ConnectionState.Open) == ConnectionState.Open)
				//			ta_Cinnost.Connection.Close();
				//		ta_Cinnost.Dispose();
				//	}

				//	if (ta_CinnostNext != null)
				//	{
				//		if ((ta_CinnostNext.Connection.State & ConnectionState.Open) == ConnectionState.Open)
				//			ta_CinnostNext.Connection.Close();
				//		ta_CinnostNext.Dispose();
				//	}

				//	if (ta_DynTabDef != null)
				//	{
				//		if ((ta_DynTabDef.Connection.State & ConnectionState.Open) == ConnectionState.Open)
				//			ta_DynTabDef.Connection.Close();
				//		ta_DynTabDef.Dispose();
				//	}

				//	if (ta_Okruh != null)
				//	{
				//		if ((ta_Okruh.Connection.State & ConnectionState.Open) == ConnectionState.Open)
				//			ta_Okruh.Connection.Close();
				//		ta_Okruh.Dispose();
				//	}

				//	if (ta_ZdrojSeznam != null)
				//	{
				//		if ((ta_ZdrojSeznam.Connection.State & ConnectionState.Open) == ConnectionState.Open)
				//			ta_ZdrojSeznam.Connection.Close();
				//		ta_ZdrojSeznam.Dispose();
				//	}
				//}

                try
                {
                    using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Servis_Ciselniky ConSerCislo = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Servis_Ciselniky(dstFile))
                    {
                        ConSerCislo.Update_CZMST_Servis_Zdroj(servisDS.CZMST_Servis_Zdroj);
                        ConSerCislo.Update_CZMST_Servis_Stav(servisDS.CZMST_Servis_Stav);
                        ConSerCislo.Update_CZMST_Servis_StavNext(servisDS.CZMST_Servis_StavNext);
                        ConSerCislo.Update_CZMST_Servis_Cinnost(servisDS.CZMST_Servis_Cinnost);
                        ConSerCislo.Update_CZMST_Servis_CinnostNext(servisDS.CZMST_Servis_CinnostNext);
                        ConSerCislo.Update_CZMST_Servis_Dynamic_Table_Definition(servisDS.CZMST_Servis_Dynamic_Table_Definition);
                        ConSerCislo.Update_CZMST_Servis_Okruh(servisDS.CZMST_Servis_Okruh);
                        ConSerCislo.Update_CZMST_Servis_ZdrojSeznam(servisDS.CZMST_Servis_ZdrojSeznam);

                        ConSerCislo.Shrink();
                    }

                }
                catch (System.Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(ex);
                }

                Fask.Compressing.Zip.Compress(dstFile);

                so.SetOK();
            }
            catch (Exception ex)
            {

				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error,"TID:" + terminalID );
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                so.Exception = true;
                so.StatusText = ex.Message;
			
            }
            
            return so;
        }

        private class ProcessZdrojPohybObject
        {
            public byte terminalID;
            public string dataPath;
            public ProcessZdrojPohybObject(byte terminalID, string dataPath)
            {
                this.terminalID = terminalID;
                this.dataPath = dataPath;
            }

        }
        [WebMethod]
        public bool ProcessZdrojPohybData(byte terminalID, byte[] data)
        {
            try
            {
                string dstFile = Path.Combine(Fask.MyPath.Path.SQLiteDBsDirectory, @terminalID.ToString() + Common.Backslash + Common.ZdrojPohyb_ + Guid.NewGuid().ToString("N") + Common.PRD);
                if (!Directory.Exists(Path.GetDirectoryName(dstFile)))
                    Directory.CreateDirectory(Path.GetDirectoryName(dstFile));

                FileStream fs = null;
                try
                {
                    fs = new FileStream(dstFile, FileMode.Create, FileAccess.Write, FileShare.Read);
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                    fs.Close();
                    fs = null;
                }
                finally
                {
                    if (fs != null)
                    {
                        fs.Flush();
                        fs.Close();
                        fs = null;
                    }
                }

                return ProcessZdrojPohybData(new ProcessZdrojPohybObject(terminalID, dstFile));
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "TID:" + terminalID + ", ProcessZdrojPohybData");
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                return false;
            }
        }

        [WebMethod]
        public bool ProcessZdrojPohyb(byte terminalID, string dstFile)
        {
            try
            {
                return ProcessZdrojPohybData(new ProcessZdrojPohybObject(terminalID, dstFile));
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "TID:" + terminalID + "ProcessZdrojPohyb");
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }

        private bool ProcessZdrojPohybData(object o)
        {
            Konfigurace.Classes.Globals_Konfig_WebConfig.LoadConfiguration();
            ProcessZdrojPohybObject ppo = o as ProcessZdrojPohybObject;
            if (ppo == null)
            {
				Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Error, "ProcessProductionDataThreaded(object o): parameter is null");
                return false;
            }

            List<Exception> exceptions = new List<Exception>();
            //IDbTransaction iTrans1 = null;
            try
            {
                // interface dataset
                Fask.DataSets.Servis vds = new Fask.DataSets.Servis();
                // CE dataset
				Fask.SQLiteDBs.DataSets.Servis vdsce = new Fask.SQLiteDBs.DataSets.Servis(); 

                #region Insert Production Data
                
                //VyrobaCEDataSetTableAdapters.ProductionTableAdapter ptace = new global::Vyroba.VyrobaCEDataSetTableAdapters.ProductionTableAdapter();
				//SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_ZdrojPohybTableAdapter ptace = new global::Fask.MST_W_Server.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_ZdrojPohybTableAdapter();

    //            ptace.Connection.ConnectionString = "Data source=" + ppo.dataPath;
    //            ptace.Fill(vdsce.CZMST_Servis_ZdrojPohyb);


                try
                {
                    using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Servis_ZdrojePohyb ConSerZdrojPohyb = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Servis_ZdrojePohyb(ppo.dataPath))
                    {
                        ConSerZdrojPohyb.Fill_ZdrojePohyb(vdsce.CZMST_Servis_ZdrojPohyb);
                    }
                }
                catch (System.Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(ex);
                }

				foreach (Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_ZdrojPohybRow prow in vdsce.CZMST_Servis_ZdrojPohyb)
                {
                    prow.SetAdded();
                    vds.CZMST_Servis_ZdrojPohyb.ImportRow(prow);
                }

                if(provider != null)
                    provider.UpdateZdrojPohyb(vds.CZMST_Servis_ZdrojPohyb);

                #endregion

                // 11.8.2010 JiS: zaloha korektne zpracovanych dat se delat nebude, protoze to zabira prilis mnoho prostoru,
                // mozna to nechat konfiguracne, ale prijde mi to zbytecne ...,
                // pokud se data ulozi korektne, tak neni duvod, je v podstate uchovavat. ...
                /*
                try
                {
                    //Presune zpracovana data do processed adresare
                    string dstDir = Path.Combine(rootpath, System.Configuration.ConfigurationManager.AppSettings["ProcessedDataFileDirectory"]);
                    if (!Directory.Exists(dstDir))
                        Directory.CreateDirectory(dstDir);

                    File.Move(ppo.dataPath, Path.Combine(dstDir, Path.GetFileName(ppo.dataPath)));
                }
                catch (Exception ex)
                {
                    Log.writeErrorLog("ProcessProductionData: " + ex.Message);
                    File.Delete(ppo.dataPath);
                    Log.writeErrorLog("ProcessProductionData: File deleted(" + ppo.dataPath + ")");
                }
                */

                File.Delete(ppo.dataPath);
                return true;

            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "TID:" + ppo.terminalID + "ProcessZdrojPohybData");
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                string dstDir = Path.Combine(Fask.MyPath.Path.RootPath, Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Directories[0].Error);
                try
                {
                    if (!Directory.Exists(dstDir))
                        Directory.CreateDirectory(dstDir);
                    File.Move(ppo.dataPath, Path.Combine(dstDir, Path.GetFileName(ppo.dataPath)));
                }
                catch (Exception ex2)
                {
					Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex2);
                }

                //exceptions.Insert(0, ex);
                //Informations.SendMailStatic(
                //    "\nProcessProductionData" +
                //    "\n           Datum: " + DateTime.Now.ToString() +
                //    "\n        Terminál: " + ppo.terminalID +
                //    "\n   Datový soubor: " + ppo.dataPath +
                //    "\n-----------------" +
                //    "\n      Exceptions: " +
                //    "\n" + Exceptions.ToString(exceptions)
                //);

                return false;

            }
            finally
            {
            }
        }

        [WebMethod]
        public StatusObject Prepare_Stavy(byte terminalID)
        {
            Fask.DataSets.Servis servisDS = null;

            StatusObject so = new StatusObject();
            so.StatusText = "Updating ...";
            try
            {
                if (provider != null)
                {
                    Terminal terminal = new Terminal();
                    terminal.ID = terminalID;
                    servisDS = provider.Servis_Prepare_Stavy(terminal, ref so);
                }

                string dstFile = Path.Combine(Fask.MyPath.Path.SQLiteDBsDirectory, terminalID.ToString() + Common.Backslash + Common.Servis_ZdrojeStav + Common.PRD);

                if (!Directory.Exists(Path.GetDirectoryName(dstFile)))
                    Directory.CreateDirectory(Path.GetDirectoryName(dstFile));


				SQLite_Helper helper = new SQLite_Helper();
				if (!helper.SQLite_CreateFile(dstFile, Common.Servis_ZdrojeStav))
				{
					throw new Exception("Nastala chyba pri tvorbe SQLite souboru pro " + Common.Servis_ZdrojeStav);
				}

				Fask.SQLiteDBs.DataSets.Servis dszdrojestav = new Fask.SQLiteDBs.DataSets.Servis();


				//SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_ZdrojStavTableAdapter ta_ZdrojeStav = new Fask.MST_W_Server.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_ZdrojStavTableAdapter();
				//ta_ZdrojeStav.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + dstFile);

				//try
				//{
				//	var tran = ta_ZdrojeStav.Connection.BeginTransaction();

				//	foreach (var item in servisDS.CZMST_Servis_ZdrojStav)
				//	{
				//		ta_ZdrojeStav.Insert(
				//			item.IDZdroj,
				//			item.IDStav,
				//			item.IsIDCinnostNull() ? null : item.IDCinnost,
				//			item.Modified,
				//			item.IsIDTerminalNull() ? (int?)null : item.IDTerminal,
				//			item.IsIDUserNull() ? (int?)null : item.IDUser,
				//			item.IsGUIDNull() ? (Guid?)null : item.GUID,
				//			item.IsCinnostValueNull() ? null : item.CinnostValue,
				//			item.IsCinnostTypeNull() ? null : item.CinnostType,
				//			item.IsCountEntriesNull() ? (int?)null : item.CountEntries,
				//			item.IsODB_IDNull() ? null : item.ODB_ID,
				//			item.IsOkruhIDNull() ? null : item.OkruhID,
				//			item.IsCinnostOznaceniNull() ? null : item.CinnostOznaceni,
				//			item.IsGPS_XNull() ? (double?)null : item.GPS_X,
				//			item.IsGPS_YNull() ? (double?)null : item.GPS_Y,
				//			item.IsGPS_ZNull() ? (int?)null : item.GPS_Z
				//			);
				//	}

				//	tran.Commit();
				//}
				//catch (Exception ex)
				//{
				//	Logging.ExceptionHandler2.Handle(ex);
				//}
				//finally
				//{
				//	if (ta_ZdrojeStav != null)
				//	{
				//		if ((ta_ZdrojeStav.Connection.State & ConnectionState.Open) == ConnectionState.Open)
				//			ta_ZdrojeStav.Connection.Close();
				//		ta_ZdrojeStav.Dispose();
				//	}
				//}

                try
                {
                    using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Servis_ZdrojeStav ConSerZdrojeStav = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Servis_ZdrojeStav(dstFile))
                    {
                        ConSerZdrojeStav.Update_ZdrojStav(servisDS.CZMST_Servis_ZdrojStav);
                    }
                }
                catch (System.Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(ex);
                }

                so.SetOK();
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "TID:" + terminalID);
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				so.Exception = true;
                so.StatusText = ex.Message;
            }
            return so;
        }

        [WebMethod]
        public StatusObject Prepare_Dynamic_Table(byte terminalID, string tableName)
        {
            Fask.DataSets.Servis servisDS = null;
            StatusObject so = new StatusObject();
            so.StatusText = "Updating ...";

            try
            {
                if (provider != null)
                {
                    Terminal terminal = new Terminal();
                    terminal.ID = terminalID;
                    servisDS = provider.Servis_Prepare_Dynamic_Table(terminal, ref so, tableName);
                }

                string dstFile = Path.Combine(Fask.MyPath.Path.SQLiteDBsDirectory, terminalID.ToString() + Common.Backslash + Common.Servis_Ciselniky + tableName + Common.PRD);

                if (!Directory.Exists(Path.GetDirectoryName(dstFile)))
                    Directory.CreateDirectory(Path.GetDirectoryName(dstFile));


				SQLite_Helper helper = new SQLite_Helper();
				if (!helper.SQLite_CreateFile(dstFile, Common.Servis_Ciselniky))
				{
					throw new Exception("Nastala chyba pri tvorbe SQLite souboru pro " + Common.Servis_Ciselniky);
				}

				Fask.SQLiteDBs.DataSets.Servis dssqlservisciselniky = new Fask.SQLiteDBs.DataSets.Servis();


				//SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_Dynamic_TableTableAdapter ta_DynTab = new Fask.MST_W_Server.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_Dynamic_TableTableAdapter();
				//ta_DynTab.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + dstFile);



				//try
				//{
				//	var tran = ta_DynTab.Connection.BeginTransaction();

				//	foreach (var item in servisDS.CZMST_Servis_Dynamic_Table)
				//	{
				//		ta_DynTab.Insert(
				//			item.ID,
				//			item.Oznaceni,
				//			item.IsBarCodeNull() ? null : item.BarCode
				//			);
				//	}

				//	tran.Commit();
				//}
				//finally
				//{
				//	if (ta_DynTab != null)
				//	{
				//		if ((ta_DynTab.Connection.State & ConnectionState.Open) == ConnectionState.Open)
				//			ta_DynTab.Connection.Close();
				//		ta_DynTab.Dispose();
				//	}
				//}

                try
                {
                    using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Servis_Ciselniky ConSerDyn = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Servis_Ciselniky(dstFile))
                    {
                        ConSerDyn.Update_CZMST_Servis_Dynamic_Table(servisDS.CZMST_Servis_Dynamic_Table);
                    }
                }
                catch (System.Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(ex);
                }

                so.SetOK();
            }
            catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "TID:" + terminalID);
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                so.Exception = true;
                so.StatusText = ex.Message;
            }
            return so;
        }

        [WebMethod]
        public StatusObject ZdrojHistory(byte terminalID, int userID, Fask.Server.Interfaces.Servis.Zdroj zdroj, int pocetZaznamu, out Fask.DataSets.Servis history)
        {
            history = null;

            StatusObject so = new StatusObject();
            so.StatusText = "Updating ...";
            try
            {
                if (provider != null)
                {
                    Terminal terminal = new Terminal();
                    User user = new User();

                    terminal.ID = terminalID;
                    user.ID = userID;

                    history = provider.Servis_ZdrojHistory(terminal, user, zdroj, pocetZaznamu);
                }
                so.SetOK();
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "TID:" + terminalID);
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                so.Exception = true;
                so.StatusText = ex.Message;
            }
            return so;
        }

        /// <summary>
        /// Vygenerovani davky
        /// </summary>
        /// <param name="document_number"></param>
        /// <param name="odb_id"></param>
        /// <param name="okruhid"></param>
        /// <returns></returns>
        [WebMethod]
        public int GenerateServiska(byte terminalID, int userID, string document_number, string odb_id, string okruhid)
        {
            try
            {
                if (provider != null)
                {
                    User user = new User();
                    user.ID = userID;
                    StatusInfo si = provider.Servis_GenerateDavka(user, document_number, odb_id, okruhid);
                    return si.ID;
                }
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(LogLevel.Error,"document_number:'" + document_number );
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                throw ex;
                //throw Routines.Exceptions.CustomSoapException("GenerateServiska", ex.Message, string.Empty);
            }

            return -10;
        }


        [WebMethod]
        public bool GetServiskaReceived(int countentries, byte idterminal)
        {

            if (provider != null)
            {
                try
                {
                    Fask.Server.Interfaces.Classes.Davka davka = new Fask.Server.Interfaces.Classes.Davka();
                    Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();

                    davka.ID = countentries;
                    terminal.ID = idterminal;

                    return provider.Servis_GetDavkaReceived(davka, terminal);
                }
                catch (Exception ex)
                {
					Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                    throw ex;
                }
            }

            return false; //mel byto vratit driv ... 
        }

        [WebMethod]
        public byte[] GetServiskaDBFile(int countentries, byte idterminal)
        {
            try
            {

                string dstFile = Path.Combine(Fask.MyPath.Path.SQLiteDBsDirectory, idterminal.ToString() + Common.Backslash + countentries.ToString() + ServisDBFileExtension);

                if (!Directory.Exists(Path.GetDirectoryName(dstFile)))
                    Directory.CreateDirectory(Path.GetDirectoryName(dstFile));

				SQLite_Helper helper = new SQLite_Helper();
				if (!helper.SQLite_CreateFile(dstFile, Common.Servis_ZdrojeStav))
				{
					throw new Exception("Nastala chyba pri tvorbe SQLite souboru pro " + Common.Servis_ZdrojeStav);
				}

                Fask.DataSets.Servis servis = GetServiska(countentries, idterminal);

                //Ulozeni dat pro terminal
                // naplneni predlohy
				//SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_PredlohaTableAdapter taPredloha = new Fask.MST_W_Server.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_PredlohaTableAdapter();
    //            taPredloha.Connection = new System.Data.SQLite.SQLiteConnection( "Data source=" + dstFile);
    //            try
    //            {
    //                taPredloha.Connection.Open();

				//	var tran = taPredloha.Connection.BeginTransaction();

    //                foreach (Fask.DataSets.Servis.CZMST_Servis_PredlohaRow item in servis.CZMST_Servis_Predloha)
    //                {

    //                    int rowinserted = taPredloha.Insert(
    //                        item.CountEntries,
    //                        item.IsDOCUMENT_NUMBERNull() ? string.Empty : item.DOCUMENT_NUMBER,
    //                        item.Rozpracovano,
    //                        item.OkruhID,
    //                        item.IsUserIDNull() ? (int?)null : item.UserID,
    //                        item.IsBarcodeNull() ? string.Empty : item.Barcode,
    //                        item.IsODB_IDNull() ? string.Empty : item.ODB_ID
    //                        );
    //                }

				//	tran.Commit();
    //            }
				//catch (Exception ex)
				//{
				//	Logging.ExceptionHandler2.Handle(ex);
				//}
				//finally
				//{
				//	if (taPredloha != null)
				//	{
				//		if ((taPredloha.Connection.State & ConnectionState.Open) == ConnectionState.Open)
				//			taPredloha.Connection.Close();
				//		taPredloha.Dispose();
				//	}
				//} 

                // naplneni dat davky
				//SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_ZdrojStavTableAdapter taZdrojStav = new Fask.MST_W_Server.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_ZdrojStavTableAdapter();
    //            taZdrojStav.Connection = new System.Data.SQLite.SQLiteConnection( "Data source=" + dstFile);
    //            try
    //            {
    //                taZdrojStav.Connection.Open();
				//	var tran = taZdrojStav.Connection.BeginTransaction();
    //                foreach (Fask.DataSets.Servis.CZMST_Servis_ZdrojStavRow item in servis.CZMST_Servis_ZdrojStav)
    //                {
    //                    int rowinserted = taZdrojStav.Insert(
    //                        item.IDZdroj,
    //                        item.IDStav,
    //                        item.IsIDCinnostNull() ? null : item.IDCinnost,
    //                        item.Modified,
    //                        item.IsIDTerminalNull() ? (int?)null : item.IDTerminal,
    //                        item.IsIDUserNull() ? (int?)null : item.IDUser,
    //                        item.IsGUIDNull() ? (Guid?)null : Guid.NewGuid(),
    //                        item.IsCinnostValueNull() ? null : item.CinnostValue,
    //                        item.IsCinnostTypeNull() ? null : item.CinnostType,
    //                        item.IsCountEntriesNull() ? (int?)null : item.CountEntries,
    //                        item.IsODB_IDNull() ? null : item.ODB_ID,
    //                        item.IsOkruhIDNull() ? null : item.OkruhID,
    //                        item.IsCinnostOznaceniNull() ? null : item.CinnostOznaceni,
    //                        item.IsGPS_XNull() ? (double?)null : item.GPS_X,
    //                        item.IsGPS_YNull() ? (double?)null : item.GPS_Y,
    //                        item.IsGPS_ZNull() ? (int?)null : item.GPS_Z
    //                        );
    //                }
				//	tran.Commit();
    //            }
				//catch (Exception ex)
				//{
				//	Logging.ExceptionHandler2.Handle(ex);
				//}
				//finally
				//{
				//	if (taZdrojStav != null)
				//	{
				//		if ((taZdrojStav.Connection.State & ConnectionState.Open) == ConnectionState.Open)
				//			taZdrojStav.Connection.Close();
				//		taZdrojStav.Dispose();
				//	}
				//} 


                //parametry pro rizeni prijmu z globalniho nastaveni na serveru ...
				//SQLiteDBs.DataSets.ServisTableAdapters.ParametryTableAdapter pta = new Fask.MST_W_Server.SQLiteDBs.DataSets.ServisTableAdapters.ParametryTableAdapter();
    //            pta.Connection = new System.Data.SQLite.SQLiteConnection( "Data source=" + dstFile);
    //            try
    //            {
				//	pta.Connection.Open();
				//	var tran = pta.Connection.BeginTransaction();
    //                DataRow drow0 = (DataRow)servis.Parametry[0];
    //                drow0.SetAdded();
    //                pta.Update(drow0);
				//	tran.Commit();
    //            }
				//catch (Exception ex)
				//{
				//	Logging.ExceptionHandler2.Handle(ex);
				//}
				//finally
				//{
				//	if (pta != null)
				//	{
				//		if ((pta.Connection.State & ConnectionState.Open) == ConnectionState.Open)
				//			pta.Connection.Close();
				//		pta.Dispose();
				//	}
				//}


                try
                {
                    using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Servis_ZdrojeStav ConSerStav = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Servis_ZdrojeStav(dstFile))
                    {

                        ConSerStav.Update_Servis_Predloha(servis.CZMST_Servis_Predloha);
                        ConSerStav.Update_ZdrojStav(servis.CZMST_Servis_ZdrojStav);

                        DataRow drow0 = (DataRow)servis.Parametry[0];
                        drow0.SetAdded();
                        ConSerStav.Update_Params(drow0);

                    }
                }
                catch (System.Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(ex);
                }


                //Prenos databazoveho souboru slqce prijemky pro temrinal
                long fileLength = (new FileInfo(dstFile)).Length;

                byte[] dbdata = new byte[fileLength];

                FileStream fs = null;
                try
                {
                    fs = new FileStream(dstFile, FileMode.Open, FileAccess.Read, FileShare.Read);
                    int bytesread = fs.Read(dbdata, 0, (int)fileLength);
                    fs.Close();
                    fs = null;
                    if (bytesread != (int)fileLength)
						Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Warn, "(Servis)GetServiskaDBFile: bytesread != (int)fileLength");

                }
                finally
                {
                    if (fs != null)
                    {
                        fs.Close();
                        fs = null;
                    }
                }

                return dbdata;

            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, " (Dávka: " + countentries.ToString() + ", Terminál ID:" + idterminal.ToString() + ")");
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                throw ex;
            }
        }

        [WebMethod]
        public Fask.Server.Interfaces.DataSets.ServisDavky GetServisky(byte idterminal)
        {

            if (provider != null)
            {
                try
                {
                    Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();
                    //Fask.Server.Interfaces.Classes.Sklad sklad = new Fask.Server.Interfaces.Classes.Sklad();
                    //Fask.Server.Interfaces.Classes.Item item = new Fask.Server.Interfaces.Classes.Item();

                    terminal.ID = idterminal;
                    //sklad.ID = prefixskladu;
                    //item.Type = itemtype;

                    return provider.Servis_GetDavky(terminal);
                }
                catch (Exception ex)
                {
					Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                    throw ex;
                }

                //return null; //mel byto vratit driv ... 
            }

            return null;
        }

        private Fask.DataSets.Servis GetServiska(int countentries, byte idterminal)
        {
            Fask.DataSets.Servis servis = null;

            if (provider != null)
            {
                try
                {
                    Fask.Server.Interfaces.Classes.Davka davka = new Fask.Server.Interfaces.Classes.Davka();
                    Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();
                    //Fask.Server.Interfaces.Classes.Sklad sklad = new Fask.Server.Interfaces.Classes.Sklad();
                    //Fask.Server.Interfaces.Classes.Item item = new Fask.Server.Interfaces.Classes.Item();

                    davka.ID = countentries;
                    terminal.ID = idterminal;

                    servis = provider.Servis_GetDavka(davka, terminal);

                    Konfigurace.Classes.Globals_Konfig_Agendy.LoadConfiguration();
                    servis.Parametry.ImportRow(Konfigurace.Classes.Globals_Konfig_Agendy.Konfigurace.ServisParametry[0]);
                    servis.AcceptChanges();
                    return servis;
                }
                catch (Exception ex)
                {
					Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                    throw ex;
                }                
            }

            return null; //mel byto vratit driv ... 
        }

        public enum ProcessServisState
        {
            Uvolnit,
            Zpracovat
        }

        private bool isLicenseValid()
        {
            Licensing.License lic = Application[Constants.Common.license] as Licensing.License;

            if (lic != null)
            {
                if (!lic.isValid || lic.isExpirated)
                    return false;

                return true;
            }
            else
                return false;
        }

        [Obsolete("Metoda ProcessServisDBFile se nepouziva, nove se pouziva ProcessServisDBFile2 ktera pracuje s zip souborem", true)]
        [WebMethod]
        public StatusObject ProcessServisDBFile(int userid, int countentries, byte idterminal, byte[] servisDBFile, ProcessServisState processState)
        {
            TracId tracid = new TracId(userid, idterminal, countentries);
            StatusObject so = new StatusObject();

            #region trace
            Trac.Write("ProcessServisDBFile START, ProcessServisState: " + processState.ToString(), tracid);
            #endregion

            if (!isLicenseValid())
            {
                so.StatusText = "Licence na serveru není validní!";
                so.Exception = true;
                return so;
            }

            //Ulozit data prijemky
            string dstFile = Path.Combine(Fask.MyPath.Path.SQLiteDBsDirectory, idterminal.ToString() + @"\" + countentries.ToString() + ServisDBFileExtension);

            if (!Directory.Exists(Path.GetDirectoryName(dstFile)))
                Directory.CreateDirectory(Path.GetDirectoryName(dstFile));

            FileStream fs = null;

            #region trace
            Trac.Write("DB FileStream start", "ProcessServisDBFile", tracid);
            #endregion

            try
            {
                fs = new FileStream(dstFile, FileMode.Create, FileAccess.Write, FileShare.Read);
                fs.Write(servisDBFile, 0, servisDBFile.Length);
                fs.Flush();
                fs.Close();
                fs = null;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Flush();
                    fs.Close();
                    fs = null;
                }
            }

            #region trace
            Trac.Write("DB FileStream end", "ProcessServisDBFile", tracid);
            #endregion

            try
            {
                Fask.DataSets.Servis servisData = new Fask.DataSets.Servis();
				Fask.SQLiteDBs.DataSets.Servis servisDataCE = new Fask.SQLiteDBs.DataSets.Servis();

                #region trace
                Trac.Write(servisDataCE.CZMST_Servis_ZdrojPohyb, "ProcessServisDBFile", tracid);
                #endregion
				//SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_ZdrojPohybTableAdapter ptace = new global::Fask.MST_W_Server.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_ZdrojPohybTableAdapter();
    //            ptace.Connection.ConnectionString = "Data source=" + dstFile;
    //            ptace.Fill(servisDataCE.CZMST_Servis_ZdrojPohyb);


                try
                {
                    using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Servis_ZdrojePohyb ConPohyb = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Servis_ZdrojePohyb(dstFile))
                    {
                        ConPohyb.Fill_ZdrojePohyb(servisDataCE.CZMST_Servis_ZdrojPohyb);
                    }
                }
                catch (System.Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(ex);
                }



				foreach (Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_ZdrojPohybRow prow in servisDataCE.CZMST_Servis_ZdrojPohyb)
                {
                    prow.SetAdded();
                    servisData.CZMST_Servis_ZdrojPohyb.ImportRow(prow);
                }

                #region trace
                Trac.Write(servisDataCE.CZMST_Servis_ZdrojPohyb, "ProcessServisDBFile", tracid);
                #endregion

                #region trace
                Trac.Write("ProcessServiska(countentries, idterminal, servisData, processState) begin", "ProcessServisFile", tracid);
                #endregion

                so = ProcessServiska(countentries, idterminal, servisData, processState);

                #region trace
                Trac.Write("ProcessServiska(countentries, idterminal, servisData, processState) end, StatusText: " + so.StatusText, "ProcessServisFile", tracid);
                #endregion

                if (so.StatusText == "OK" && !so.Exception)
                {
                    Routines.ManageDataFiles.Move(dstFile, Path.Combine(Fask.MyPath.Path.ProcessedDataFileDirectory, Path.GetFileName(dstFile)));
                }
                else
                {
                    Routines.ManageDataFiles.Move(dstFile, Path.Combine(Fask.MyPath.Path.ErrorDataFileDirectory, Path.GetFileName(dstFile)));                    
                }

                #region trace
                Trac.Write("ProcessServisFile END, ProcessServisState: " + processState.ToString(), tracid);
                #endregion

                return so;
            }
            catch (Exception ex)
            {
                #region trace
                Trac.Write(ex, "ProcessServisFile END, ProcessServisState: " + processState.ToString(), tracid);
                #endregion

				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                Routines.ManageDataFiles.Move(dstFile, Path.Combine(Fask.MyPath.Path.ErrorDataFileDirectory, Path.GetFileName(dstFile)));
                
                so.StatusText = ex.Message;
                so.Exception = true;
                return so;

                throw Routines.Exceptions.CustomSoapException("Process", ex.Message, "Servis");

            }
            finally
            {
                #region trace
                Trac.Write("ProcessServisFile END, finally", tracid);
                #endregion

                try
                {
                    File.Delete(dstFile);
                }
                catch (Exception ex)
                {
					Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                }
            }

        }


        [WebMethod]
        public StatusObject ProcessServisDBFile2(int userid, int countentries, byte idterminal, ProcessServisState processState)
        {
            TracId tracid = new TracId(userid, idterminal, countentries);
            StatusObject so = new StatusObject();

            #region trace
            Trac.Write("ProcessServisDBFile START, ProcessServisState: " + processState.ToString(), tracid);
            #endregion

            if (!isLicenseValid())
            {
                so.StatusText = "Licence na serveru není validní!";
                so.Exception = true;
                return so;
            }

            //Ulozit data prijemky
            string dstFile = Path.Combine(Fask.MyPath.Path.SQLiteDBsDirectory, idterminal.ToString() + Common.Backslash + countentries.ToString() + ServisDBFileExtension);
			string dstFileZip = dstFile + Common.ZIP;


            if (!Directory.Exists(Path.GetDirectoryName(dstFile)))
                Directory.CreateDirectory(Path.GetDirectoryName(dstFile));

            #region trace
            Trac.Write("DB FileStream start", "ProcessServisDBFile", tracid);
            #endregion

            Fask.Compressing.Zip.Decompress(dstFileZip);

            #region trace
            Trac.Write("DB FileStream end", "ProcessServisDBFile", tracid);
            #endregion

            try
            {
                Fask.DataSets.Servis servisData = new Fask.DataSets.Servis();
				Fask.SQLiteDBs.DataSets.Servis servisDataCE = new Fask.SQLiteDBs.DataSets.Servis();

                #region trace
                Trac.Write(servisDataCE.CZMST_Servis_ZdrojPohyb, "ProcessServisDBFile", tracid);
                #endregion

				//SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_ZdrojPohybTableAdapter ptace = new global::Fask.MST_W_Server.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_ZdrojPohybTableAdapter();
    //            ptace.Connection = new System.Data.SQLite.SQLiteConnection( "Data source=" + dstFile);

				//try
				//{
				//	ptace.Fill(servisDataCE.CZMST_Servis_ZdrojPohyb);
				//}
				//catch (Exception ex)
				//{
				//	Logging.ExceptionHandler2.Handle(ex);
				//}
				//finally
				//{
				//	if (ptace != null)
				//	{
				//		if ((ptace.Connection.State & ConnectionState.Open) == ConnectionState.Open)
				//			ptace.Connection.Close();
				//		ptace.Dispose();
				//	}
				//}

                try
                {
                    using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Servis_ZdrojePohyb ConSerPohyb = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Servis_ZdrojePohyb(dstFile))
                    {
                        ConSerPohyb.Fill_ZdrojePohyb(servisDataCE.CZMST_Servis_ZdrojPohyb);
                    }
                }
                catch (System.Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(ex);
                }


				foreach (Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_ZdrojPohybRow prow in servisDataCE.CZMST_Servis_ZdrojPohyb)
                {
                    prow.SetAdded();
                    servisData.CZMST_Servis_ZdrojPohyb.ImportRow(prow);
                }

                #region trace
                Trac.Write(servisDataCE.CZMST_Servis_ZdrojPohyb, "ProcessServisDBFile", tracid);
                #endregion

                #region trace
                Trac.Write("ProcessServiska(countentries, idterminal, servisData, processState) begin", "ProcessServisFile", tracid);
                #endregion

                so = ProcessServiska(countentries, idterminal, servisData, processState);

                #region trace
                Trac.Write("ProcessServiska(countentries, idterminal, servisData, processState) end, StatusText: " + so.StatusText, "ProcessServisFile", tracid);
                #endregion

                if (so.StatusText == "OK" && !so.Exception)
                {
                    Routines.ManageDataFiles.Move(dstFile, Path.Combine(Fask.MyPath.Path.ProcessedDataFileDirectory, Path.GetFileName(dstFile)));
                }
                else
                {
                    Routines.ManageDataFiles.Move(dstFile, Path.Combine(Fask.MyPath.Path.ErrorDataFileDirectory, Path.GetFileName(dstFile)));
                }

                #region trace
                Trac.Write("ProcessServisFile END, ProcessServisState: " + processState.ToString(), tracid);
                #endregion

                return so;
            }
            catch (Exception ex)
            {
                #region trace
                Trac.Write(ex, "ProcessServisFile END, ProcessServisState: " + processState.ToString(), tracid);
                #endregion

				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                Routines.ManageDataFiles.Move(dstFile, Path.Combine(Fask.MyPath.Path.ErrorDataFileDirectory, Path.GetFileName(dstFile)));

                so.StatusText = ex.Message;
                so.Exception = true;
                return so;

                throw Routines.Exceptions.CustomSoapException("Process", ex.Message, "Servis");

            }
            finally
            {
                #region trace
                Trac.Write("ProcessServisFile END, finally", tracid);
                #endregion

                try
                {
                    File.Delete(dstFile);
                }
                catch (Exception ex)
                {
					Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                }
            }

        }


        [WebMethod]
        public StatusObject StornoServiska(byte idterminal, int userid, string idCountEntries, string password)
        {
            StatusObject processStatus = new StatusObject();

            if (provider != null)
            {
                try
                {
                    Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();
                    Fask.Server.Interfaces.Classes.Davka davka = new Davka();
                    Fask.Server.Interfaces.Classes.User user = new User();

                    terminal.ID = idterminal;
                    user.ID = userid;                    
                    davka.ID = int.Parse(idCountEntries);
                    //item.Type = itemtype;

                    processStatus = provider.Servis_Storno_Davka(davka, user, terminal, password);

                    return processStatus;
                }
                catch (Exception ex)
                {
					Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                    throw ex;
                }
            }
            else
            {
                processStatus.StatusText = "Není nalinkovaná knihovna!";
                processStatus.Exception = true;
                return processStatus;
            }

        }

        public StatusObject ProcessServiska(int countentries, byte idterminal, Fask.DataSets.Servis servisdata, ProcessServisState processState)
        {
            TracId tracid = new TracId(null, idterminal, countentries);

            StatusObject processStatus = new StatusObject();

            #region trace
            Trac.Write("provider is " + (provider != null ? "not null" : "null"), "(Servis)ProcessDavka START", tracid);
            #endregion

            if (provider != null)
            {

                try
                {
                    Fask.Server.Interfaces.Classes.Davka davka = new Fask.Server.Interfaces.Classes.Davka();
                    Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();
                    
                    davka.ID = countentries;
                    terminal.ID = idterminal;
                    //davka.ID = prijemdata.CZMST_PE[0].CountEntries;
                    //terminal.ID = prijemdata.CZMST_PE[0].CZ_Doslo;
                    //sklad.ID = prefixskladu;
                    //item.Type = prijemdata.CZMST_PE[0].ITEMTYPE; //???

                    //Fask.Server.Interfaces.Prijem.ProcessState pState = (Fask.Server.Interfaces.Prijem.ProcessState)processState;
                    Fask.Server.Interfaces.Servis.ProcessState pState = (Fask.Server.Interfaces.Servis.ProcessState)processState;

                    #region trace
                    Trac.Write("provider.Servis_Process(davka, terminal, servisdata, pState) begin", "ProcessDavka", tracid);
                    #endregion

                    processStatus = provider.Servis_Process(davka, terminal, servisdata, pState);

                    #region trace
                    Trac.Write("provider.Servis_Process(davka, terminal, servisdata, pState) end, StatusText : " + processStatus.StatusText, "ProcessDavka", tracid);
                    #endregion

					// \TODO : management davek sjednotit s ostatnimi moduly ...
                    // toto je reseno v nadrazene metode ... ??? 
                    //4. Managment datoveho souboru
                    //bool moved = false;
                    //if (processStatus) //uspelo => do processed
                    //{
                    //    moved = Routines.ManageDataFiles.Move(
                    //        dstFile,
                    //        Path.Combine(
                    //            (string)Session[Constants.Common.Server_Directory_Processed],
                    //            Path.GetFileName(dstFile))
                    //        );
                    //}
                    //else //neuspelo => do erroru
                    //{
                    //    moved = Routines.ManageDataFiles.Move(
                    //        dstFile,
                    //        Path.Combine(
                    //            (string)Session[Constants.Common.Server_Directory_Error],
                    //            Path.GetFileName(dstFile))
                    //        );
                    //}

                    #region trace
                    Trac.Write("(Servis) ProcessDavka END", tracid);
                    #endregion

                    return processStatus;

                }
                catch (Exception ex)
                {
                    #region trace
                    Trac.Write(ex, "ProcessPrijemka END, exception", tracid);
                    #endregion

					Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                    throw ex;
                }
            }

            processStatus.StatusText = "Nastala chyba";

            return processStatus;
        }

        #region Foceni
        [WebMethod]
        public StatusObject ImageArchivate(byte terminalID, int userID, string imageFileName, byte[] imageData)
        {
            StatusObject so = new StatusObject();
            string username = string.Empty;
            try
            {
                Konfigurace.Classes.Globals_Konfig_WebConfig.LoadConfiguration();
                    
                string imagesDirectory = Fask.MyPath.Path.ImagesDataFileDirectory;

                username = Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Image[0].ImagesUserName;
                if (!string.IsNullOrEmpty(username))
                {
                    string password = Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Image[0].ImagesUserPassword;
                    string domainname = Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Image[0].ImagesDomainName;
                    impersonateValidUser(username, password, domainname);
                }
                
                if (!Directory.Exists(imagesDirectory))
                    Directory.CreateDirectory(imagesDirectory);

                if (File.Exists(Path.Combine(imagesDirectory, imageFileName)))
                {
					Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error,"Servis", "ImageArchivate", "File '" + imageFileName + "' already exists and was deleted");
                    File.Delete(Path.Combine(imagesDirectory, imageFileName));
                }
                FileStream fs = new FileStream(Path.Combine(imagesDirectory, imageFileName), FileMode.Create);
                fs.Write(imageData, 0, imageData.Length);
                fs.Flush();
                fs.Close();
                fs = null;

                if (provider != null)
                {
					// \TODO : dodelat rozhrani providera pro ulozeni fotek ... 
                }
                else
                {
					// \TODO : ulozit do nejakeho adresare ... 
                }
                so.SetOK();
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(
                    "Servis",
                    "ImageArchivate(" + terminalID + "," + userID + "," + imageFileName + "," + imageData.ToString() + ")",
                    ex
                    );
                so.Exception = true;
                so.StatusText = ex.Message;
            }
            finally
            {
                try
                {
                    if (!string.IsNullOrEmpty(username))
                        undoImpersonation();
                }
                catch {}
            }
            return so;
        }

        #region impersonifikace
        public const int LOGON32_LOGON_INTERACTIVE = 2;
        public const int LOGON32_PROVIDER_DEFAULT = 0;

        WindowsImpersonationContext impersonationContext;

        [DllImport("advapi32.dll")]
        public static extern int LogonUserA(String lpszUserName,
            String lpszDomain,
            String lpszPassword,
            int dwLogonType,
            int dwLogonProvider,
            ref IntPtr phToken);
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int DuplicateToken(IntPtr hToken,
            int impersonationLevel,
            ref IntPtr hNewToken);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool RevertToSelf();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool CloseHandle(IntPtr handle);

        private bool impersonateValidUser(string username, string password, string domainname)
        {
            WindowsIdentity tempWindowsIdentity;
            IntPtr token = IntPtr.Zero;
            IntPtr tokenDuplicate = IntPtr.Zero;

            if (RevertToSelf())
            {
                if (LogonUserA(username, domainname, password, LOGON32_LOGON_INTERACTIVE,
                    LOGON32_PROVIDER_DEFAULT, ref token) != 0)
                {
                    if (DuplicateToken(token, 2, ref tokenDuplicate) != 0)
                    {
                        tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
                        impersonationContext = tempWindowsIdentity.Impersonate();
                        if (impersonationContext != null)
                        {
                            CloseHandle(token);
                            CloseHandle(tokenDuplicate);
                            return true;
                        }
                    }
                }
            }
            if (token != IntPtr.Zero)
                CloseHandle(token);
            if (tokenDuplicate != IntPtr.Zero)
                CloseHandle(tokenDuplicate);
            return false;
        }

        private void undoImpersonation()
        {
            impersonationContext.Undo();
        }

        #endregion impersonifikace
        #endregion Foceni
    }
}
