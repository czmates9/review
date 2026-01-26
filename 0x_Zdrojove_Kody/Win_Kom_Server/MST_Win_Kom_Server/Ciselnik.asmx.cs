using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Data.Common;
using System.Text;
using System.IO;
using System.Reflection;
using Fask.Server.Interfaces.Classes;
using Fask.Logging;

using System.Data.SqlClient;
using Fask.MST_W_Server.SQLite_Classes;

using Fask.MST_W_Server.Constants;
using Fask.MST_W_Server.Extensions;
using Fask.Tracing;
using Fask.Interfaces.DataSets;

namespace Fask.MST_W_Server
{
    /// <summary>
    /// Služba pøenosu dat èíselníkù
    /// </summary>
    [WebService(Namespace = "http://fask.cz/", Description = "Služba pøenosu dat èíselníkù", Name = "CiselnikService")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class Ciselnik : System.Web.Services.WebService
    {

        #region Lokalne promenne

        //Fask.Server.Interfaces.Ciselniky.ICiselniky provider = null;
        object provider = null;

        #endregion

        #region Konstruktor

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Ciselnik()
        {

            try
            {
                // Inicializuje objektove rozhrani ...
                Konfigurace.Classes.Globals_Konfig_WebConfig.LoadConfiguration();
                string providerAssemblyPath = Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Providers[0].Provider_Ciselniky;
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
                                if (typeof(Fask.Server.Interfaces.Ciselniky.ICiselniky).IsAssignableFrom(t))
                                {
                                    provider = (Fask.Server.Interfaces.Ciselniky.ICiselniky)providerAssemlby.CreateInstance(t.FullName);
                                    if (provider != null)
                                        break;
                                }
                                else if (typeof(Fask.Server.Interfaces.Ciselniky.IOdberatele).IsAssignableFrom(t))
                                {
                                    provider = (Fask.Server.Interfaces.Ciselniky.IOdberatele)providerAssemlby.CreateInstance(t.FullName);
                                    if (provider != null)
                                        break;
                                }
                                else if (typeof(Fask.Server.Interfaces.Ciselniky.IPracovnici).IsAssignableFrom(t))
                                {
                                    provider = (Fask.Server.Interfaces.Ciselniky.IPracovnici)providerAssemlby.CreateInstance(t.FullName);
                                    if (provider != null)
                                        break;
                                }
                                else if (typeof(Fask.Server.Interfaces.Ciselniky.IStrediska).IsAssignableFrom(t))
                                {
                                    provider = (Fask.Server.Interfaces.Ciselniky.IStrediska)providerAssemlby.CreateInstance(t.FullName);
                                    if (provider != null)
                                        break;
                                }
                                else if (typeof(Fask.Server.Interfaces.Ciselniky.ITypDokladu).IsAssignableFrom(t))
                                {
                                    provider = (Fask.Server.Interfaces.Ciselniky.ITypDokladu)providerAssemlby.CreateInstance(t.FullName);
                                    if (provider != null)
                                        break;
                                }
                                else if (typeof(Fask.Server.Interfaces.Ciselniky.IZbozi).IsAssignableFrom(t))
                                {
                                    provider = (Fask.Server.Interfaces.Ciselniky.IZbozi)providerAssemlby.CreateInstance(t.FullName);
                                    if (provider != null)
                                        break;
                                }
                                else if (typeof(Fask.Server.Interfaces.Ciselniky.ILokace).IsAssignableFrom(t))
                                {
                                    provider = (Fask.Server.Interfaces.Ciselniky.ILokace)providerAssemlby.CreateInstance(t.FullName);
                                    if (provider != null)
                                        break;
                                }
                                else if (typeof(Fask.Server.Interfaces.Ciselniky.ISklady).IsAssignableFrom(t))
                                {
                                    provider = (Fask.Server.Interfaces.Ciselniky.ISklady)providerAssemlby.CreateInstance(t.FullName);
                                    if (provider != null)
                                        break;
                                }
                                else if (typeof(Fask.Server.Interfaces.Ciselniky.IMeny).IsAssignableFrom(t))
                                {
                                    provider = (Fask.Server.Interfaces.Ciselniky.IMeny)providerAssemlby.CreateInstance(t.FullName);
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

        #endregion

        #region WebMetody

		#region Exporty 

		/// <summary>
		/// Metoda pro export Zásob z IS do SQL DB
		/// </summary>
		/// <param name="idterminal">ID Terminalu</param>
		/// <param name="prefixskladu">Prefix skladu</param>
		/// <returns>StatusObject - objekt který nese infomace o stavu</returns>
		[WebMethod(Description = "Metoda pro export Zásob z IS do SQL DB")]
		public StatusObject KatalogZboziExport(byte idterminal, string prefixskladu)
		{
			string srcFile = Path.Combine(Fask.MyPath.Path.SQLiteDBsDirectory, Common.All + Common.Zbozi + Common.PRDSO);
			string statusFile = srcFile;

			StatusObject so = new StatusObject(statusFile);
			if (so.Exists)
			{
				so.Read();
				so.StatusText = "Export jiz probiha\n" + so.StatusText;
				so.Exception = true;
				return so;
			}

			try
			{
				so.Write("Zaèátek exportu");

				if (provider != null && (provider is Fask.Server.Interfaces.Ciselniky.IZbozi)) //nove rozhrani objektove ...
				{
					Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();
					Fask.Server.Interfaces.Classes.Sklad sklad = new Fask.Server.Interfaces.Classes.Sklad();

					terminal.ID = idterminal;
					sklad.ID = prefixskladu;

					StatusInfo statusInfo = ((Fask.Server.Interfaces.Ciselniky.IZbozi)provider).KatalogZboziExport(terminal, sklad, ref so);

					so.Write("File succesfully prepared");

					so.SetOK();
					return so;
				}


			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, " (Terminál ID:" + idterminal.ToString() + ")");
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				so.Exception = true;
				so.Write(ex.Message);
				return so;
			}
			finally
			{
				so.Delete();
			}

			string msg = "Provider v Ciselnik.asmx > 'KatalogZboziExport(byte idterminal, string prefixskladu)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
			throw new Exception(msg);

		}

		/// <summary>
		/// Metoda pro export Odbìratele z IS do SQL DB
		/// </summary>
		/// <param name="idterminal">ID Terminalu</param>
		/// <returns>StatusObject - objekt který nese infomace o stavu</returns>
		[WebMethod(Description = "Metoda pro export Odbìratele z IS do SQL DB")]
		public StatusObject KatalogOdberateleExport(byte idterminal)
		{
			string srcFile = Path.Combine(Fask.MyPath.Path.SQLiteDBsDirectory, Common.All + Common.Odberatele + Common.PRDSO);
			string statusFile = srcFile;

			StatusObject so = new StatusObject(statusFile);
			if (so.Exists)
			{
				so.Read();
				so.StatusText = "Export jiz probiha\n" + so.StatusText;
				so.Exception = true;
				return so;
			}

			try
			{
				so.Write("Zaèátek exportu");

				if (provider != null && (provider is Fask.Server.Interfaces.Ciselniky.IOdberatele)) //nove rozhrani objektove ...
				{
					Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();

					terminal.ID = idterminal;

					StatusInfo statusInfo = ((Fask.Server.Interfaces.Ciselniky.IOdberatele)provider).KatalogOdberateleExport(terminal, ref so);

					so.Write("File succesfully prepared");

					so.SetOK();
					return so;
				}


			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, " (Terminál ID:" + idterminal.ToString() + ")");
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				so.Exception = true;
				so.Write(ex.Message);
				return so;
			}
			finally
			{
				so.Delete();
			}

			string msg = "Provider v Ciselnik.asmx > 'KatalogOdberateleExport(byte idterminal)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
			throw new Exception(msg);
		}

		/// <summary>
		/// Metoda pro export Støediska z IS do SQL DB
		/// </summary>
		/// <param name="idterminal">ID Terminalu</param>
		/// <returns>StatusObject - objekt který nese infomace o stavu</returns>
		[WebMethod(Description = "Metoda pro export Støediska z IS do SQL DB")]
		public StatusObject KatalogStrediskaExport(byte idterminal)
		{
			string srcFile = Path.Combine(Fask.MyPath.Path.SQLiteDBsDirectory, Common.All + Common.Strediska + Common.PRDSO);
			string statusFile = srcFile;

			StatusObject so = new StatusObject(statusFile);
			if (so.Exists)
			{
				so.Read();
				so.StatusText = "Export jiz probiha\n" + so.StatusText;
				so.Exception = true;
				return so;
			}

			try
			{
				so.Write("Zaèátek exportu");

				if (provider != null && (provider is Fask.Server.Interfaces.Ciselniky.IStrediska)) //nove rozhrani objektove ...
				{
					Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();

					terminal.ID = idterminal;

					StatusInfo statusInfo = ((Fask.Server.Interfaces.Ciselniky.IStrediska)provider).KatalogStrediskaExport(terminal, ref so);

					so.Write("File succesfully prepared");

					so.SetOK();
					return so;
				}


			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, " (Terminál ID:" + idterminal.ToString() + ")");
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				so.Exception = true;
				so.Write(ex.Message);
				//throw ex;
				return so;
			}
			finally
			{
				so.Delete();
			}

			string msg = "Provider v Ciselnik.asmx > 'KatalogStrediskaExport(byte idterminal)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
			throw new Exception(msg);
		}

		/// <summary>
		/// Metoda pro export Precovnici z IS do SQL DB
		/// </summary>
		/// <param name="idterminal">ID Terminalu</param>
		/// <returns>StatusObject - objekt který nese infomace o stavu</returns>
		[WebMethod(Description = "Metoda pro export Precovnici z IS do SQL DB")]
		public StatusObject KatalogPracovniciExport(byte idterminal)
		{
			string srcFile = Path.Combine(Fask.MyPath.Path.SQLiteDBsDirectory, Common.All + Common.Pracovnici + Common.PRDSO);
			string statusFile = srcFile;

			StatusObject so = new StatusObject(statusFile);
			if (so.Exists)
			{
				so.Read();
				so.StatusText = "Export jiz probiha\n" + so.StatusText;
				so.Exception = true;
				return so;
			}

			try
			{
				so.Write("Zaèátek exportu");

				if (provider != null && (provider is Fask.Server.Interfaces.Ciselniky.IPracovnici)) //nove rozhrani objektove ...
				{
					Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();

					terminal.ID = idterminal;

					StatusInfo statusInfo = ((Fask.Server.Interfaces.Ciselniky.IPracovnici)provider).KatalogPracovniciExport(terminal, ref so);

					so.Write("File succesfully prepared");

					so.SetOK();
					return so;
				}


			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, " (Terminál ID:" + idterminal.ToString() + ")");
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				so.Exception = true;
				so.Write(ex.Message);
				//throw ex;
				return so;
			}
			finally
			{
				so.Delete();
			}

			string msg = "Provider v Ciselnik.asmx > 'KatalogPracovniciExport(byte idterminal)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
			throw new Exception(msg);
		}

		/// <summary>
		/// Metoda pro export Mìny z IS do SQL DB
		/// </summary>
		/// <param name="idterminal">ID Terminalu</param>
		/// <returns>StatusObject - objekt který nese infomace o stavu</returns>
		[WebMethod(Description = "Metoda pro export Mìny z IS do SQL DB")]
		public StatusObject KatalogMenyExport(byte idterminal)
		{
			string srcFile = Path.Combine(Fask.MyPath.Path.SQLiteDBsDirectory, Common.All + Common.Meny + Common.PRDSO);
			string statusFile = srcFile;

			StatusObject so = new StatusObject(statusFile);
			if (so.Exists)
			{
				so.Read();
				so.StatusText = "Export jiz probiha\n" + so.StatusText;
				so.Exception = true;
				return so;
			}

			try
			{
				so.Write("Zaèátek exportu");

				if (provider != null && (provider is Fask.Server.Interfaces.Ciselniky.IMeny)) //nove rozhrani objektove ...
				{
					Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();

					terminal.ID = idterminal;

					StatusInfo statusInfo = ((Fask.Server.Interfaces.Ciselniky.IMeny)provider).KatalogMenExport(terminal, ref so);

					so.Write("File succesfully prepared");

					so.SetOK();
					return so;
				}


			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, " (Terminál ID:" + idterminal.ToString() + ")");
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				so.Exception = true;
				so.Write(ex.Message);
				return so;
			}
			finally
			{
				so.Delete();
			}

			string msg = "Provider v Ciselnik.asmx > 'KatalogMenyExport(byte idterminal)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
			throw new Exception(msg);
		}

		/// <summary>
		/// Metoda pro export Sklady z IS do SQL DB
		/// </summary>
		/// <param name="idterminal">ID Terminalu</param>
		/// <returns>StatusObject - objekt který nese infomace o stavu</returns>
		[WebMethod(Description = "Metoda pro export Sklady z IS do SQL DB")]
		public StatusObject KatalogSkladyExport(byte idterminal)
		{
			string srcFile = Path.Combine(Fask.MyPath.Path.SQLiteDBsDirectory, Common.All + Common.Sklady + Common.PRDSO);
			string statusFile = srcFile;

			StatusObject so = new StatusObject(statusFile);
			if (so.Exists)
			{
				so.Read();
				so.StatusText = "Export jiz probiha\n" + so.StatusText;
				so.Exception = true;
				return so;
			}

			try
			{
				so.Write("Zaèátek exportu");

				if (provider != null && (provider is Fask.Server.Interfaces.Ciselniky.ISklady)) //nove rozhrani objektove ...
				{
					Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();

					terminal.ID = idterminal;

					StatusInfo statusInfo = ((Fask.Server.Interfaces.Ciselniky.ISklady)provider).KatalogSkladyExport(terminal, ref so);

					so.Write("File succesfully prepared");

					so.SetOK();
					return so;
				}


			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, " (Terminál ID:" + idterminal.ToString() + ")");
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				so.Exception = true;
				so.Write(ex.Message);
				//throw ex;
				return so;
			}
			finally
			{
				so.Delete();
			}

			string msg = "Provider v Ciselnik.asmx > 'KatalogSkladyExport(byte idterminal)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
			throw new Exception(msg);
		}
		
		#endregion

        /// <summary>
        /// Metoda pro pøípravu Souboru Zboží pro terminal
        /// </summary>
        /// <param name="idterminal">ID Terminalu</param>
        /// <param name="prefixskladu">Prefix skladu</param>
        /// <returns>StatusObject - objekt který nese infomace o stavu</returns>
        [WebMethod(Description="Metoda pro pøípravu Souboru Zboží pro terminal")]
        public StatusObject KatalogZboziDBPrepare(byte idterminal, string prefixskladu)
        {
			//System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
			//sw.Start();

			TracId tracid = new TracId(null, idterminal, null, "KatalogZboziDBPrepare");

			#region trace
			Trac.Write("Start", tracid);
			#endregion

			string dstFile = Path.Combine(Fask.MyPath.Path.SQLiteDBsDirectory, idterminal.ToString() + Common.Backslash + Common.Zbozi + Common.PRD);
			string statusFile = dstFile + Common.SO;

			#region trace
			Trac.Write("InitParams", tracid);
			#endregion

			StatusObject so = new StatusObject(statusFile);
            if (so.Exists)
            {
                so.Read();
                so.StatusText = "File is still preparing\n" + so.StatusText;
                so.Exception = true;
                return so;
            }

            if (File.Exists(dstFile))
            {
                so.StatusText = "File already prepared\n" + "Ready to download";
                so.Exception = false;
                return so;
            }

			#region trace
			Trac.Write("StatusObjekt OK", tracid);
			#endregion

			try
			{
                so.Write("Preparing template");

				#region trace
				Trac.Write("Preparing template", tracid);
				#endregion

				if (!Directory.Exists(Path.GetDirectoryName(dstFile)))
                    Directory.CreateDirectory(Path.GetDirectoryName(dstFile));

				SQLite_Helper helper = new SQLite_Helper();
				if (!helper.SQLite_CreateFile(dstFile, Common.Zbozi))
				{
					throw new Exception("Nastala chyba pri tvorbe SQLite souboru pro " + Common.Zbozi);
				}

                so.Write("Downloading data");

				#region trace
				Trac.Write("Downloading data", tracid);
				#endregion

				Fask.SQLiteDBs.DataSets.Zbozi zbozi = null;
                if (provider != null && (provider is Fask.Server.Interfaces.Ciselniky.IZbozi)) //nove rozhrani objektove ...
                {
                    Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();
                    Fask.Server.Interfaces.Classes.Sklad sklad = new Fask.Server.Interfaces.Classes.Sklad();

                    terminal.ID = idterminal;
                    sklad.ID = prefixskladu;                    

                    Fask.Interfaces.DataSets.Zbozi zboziprovider = ((Fask.Server.Interfaces.Ciselniky.IZbozi)provider).KatalogZbozi(terminal, sklad, ref so);
					zbozi = new Fask.SQLiteDBs.DataSets.Zbozi();


					// \TODO : !!! zbavit se nejak tohoto cyklu???
                    zbozi.CZMST095.BeginLoadData();
                    foreach (Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBYRow row in zboziprovider.FASK_ZASOBY)
                    {
						row.TrimStringColumns();
                        zbozi.CZMST095.ImportRow(row);
                    }
                    zbozi.CZMST095.EndLoadData();

                    zbozi.CZMST095M.BeginLoadData();
                    foreach (Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_MENYRow rowM in zboziprovider.FASK_ZASOBY_MENY)
                    {
						rowM.TrimStringColumns();
                        zbozi.CZMST095M.ImportRow(rowM);
                    }
                    zbozi.CZMST095M.EndLoadData();
                }
                else  
                {
                    string msg = "Provider v Ciselnik.asmx > 'KatalogZboziDBPrepare(byte idterminal, string prefixskladu)' nenastaven.";
					Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
                    throw new Exception(msg);
                }

                so.Write("Filling data to database");
				#region trace
				Trac.Write("Filling data to database", tracid);
				#endregion

				//SQLiteDBs.DataSets.ZboziTableAdapters.CZMST095TableAdapter zta = new Fask.MST_W_Server.SQLiteDBs.DataSets.ZboziTableAdapters.CZMST095TableAdapter();
				//zta.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + dstFile);

				//try
				//{
				//zta.Connection.Open();
				//	var Ztransakce = zta.Connection.BeginTransaction();
				//zta.Update(zbozi.CZMST095);
				//	Ztransakce.Commit();
				//}
				//catch (Exception ex)
				//{
				//	Logging.ExceptionHandler2.Handle(ex);
				//}
				//finally
				//{
				//	if (zta != null)
				//	{
				//		if ((zta.Connection.State & ConnectionState.Open) == ConnectionState.Open)
				//			zta.Connection.Close();
				//		zta.Dispose();
				//	}
				//}

				#region trace
				Trac.Write("Update 095", tracid);
				#endregion
				using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Zbozi ConZbo = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Zbozi(dstFile))
				{
					ConZbo.Update_CZMST095(zbozi.CZMST095);
				}
				#region trace
				Trac.Write("Update 095 complete", tracid);
				#endregion



				//SQLiteDBs.DataSets.ZboziTableAdapters.CZMST095MTableAdapter ztaM = new Fask.MST_W_Server.SQLiteDBs.DataSets.ZboziTableAdapters.CZMST095MTableAdapter();
				//ztaM.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + dstFile);

				//try
				//{
				//	ztaM.Connection.Open();
				//	var ZMtransakce = ztaM.Connection.BeginTransaction();
				//	ztaM.Update(zbozi.CZMST095M);
				//	ZMtransakce.Commit();
				//}
				//catch (Exception ex)
				//{
				//	Logging.ExceptionHandler2.Handle(ex);
				//}
				//finally
				//{
				//	if (ztaM != null)
				//	{
				//		if ((ztaM.Connection.State & ConnectionState.Open) == ConnectionState.Open)
				//			ztaM.Connection.Close();
				//		ztaM.Dispose();
				//	}
				//}

				#region trace
				Trac.Write("Update 095M", tracid);
				#endregion

				using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Zbozi ConZbo = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Zbozi(dstFile))
				{
					ConZbo.Update_CZMST095M(zbozi.CZMST095M);

					so.Write("Shrinking database");
					ConZbo.Shrink();
				}

				#region trace
				Trac.Write("Update 095M complete", tracid);
				#endregion

				//so.Write("Shrinking database");
				//SQLite_Classes.SQLite_Helper.Shrink("Data source=" + dstFile);

				so.Write("Compressing database");
				#region trace
				Trac.Write("Compressing database", tracid);
				#endregion
				Fask.Compressing.Zip.Compress(dstFile);

                so.Write("File succesfully prepared");
				#region trace
				Trac.Write("File succesfully prepared", tracid);
				#endregion

				so.SetOK();
                return so;
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, " (Terminál ID:" + idterminal.ToString() + ")");
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                so.Exception = true;
                so.Write(ex.Message);
                //throw ex;
                return so;
            }
            finally
            {
                so.Delete();

				#region trace
				Trac.Write("Stop", tracid);
				#endregion
				//sw.Stop();
				//System.Diagnostics.Debug.WriteLine(string.Format("{0} : {1}", "prepare ciselniku zbozi trval", sw.Elapsed.ToString()));
			}
		}

        /// <summary>
        /// Metoda pro pøípravu Souboru Mìny pro terminal 
        /// </summary>
        /// <param name="idterminal">ID Terminal</param>
        /// <returns>True- OK, False- chyba</returns>
        [WebMethod(Description = "Metoda pro pøípravu Souboru Mìny pro terminal")]
		public StatusObject KatalogMenyDBPrepare(byte idterminal)
        {
			
			Fask.SQLiteDBs.DataSets.Meny meny = null;
				string dstFile = Path.Combine(Fask.MyPath.Path.SQLiteDBsDirectory, idterminal.ToString() + Common.Backslash + Common.Meny + Common.PRD);
				string statusFile = dstFile + Common.SO;

				StatusObject so = new StatusObject(statusFile);
				if (so.Exists)
				{
					so.Read();
					so.StatusText = "File is still preparing\n" + so.StatusText;
					so.Exception = true;
					return so;
				}

				if (File.Exists(dstFile))
				{
					so.StatusText = "File already prepared\n" + "Ready to download";
					so.Exception = false;
					return so;
				}

			try
			{
				so.Write("Preparing template");

				if (!Directory.Exists(Path.GetDirectoryName(dstFile)))
					Directory.CreateDirectory(Path.GetDirectoryName(dstFile));

				SQLite_Helper helper = new SQLite_Helper();
				if (!helper.SQLite_CreateFile(dstFile, Common.Meny))
				{
					throw new Exception("Nastala chyba pri tvorbe SQLite souboru pro " + Common.Meny);
				}
				so.Write("Downloading data");
				if (provider != null && (provider is Fask.Server.Interfaces.Ciselniky.IMeny)) //nove rozhrani objektove ...
				{
					Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();


					terminal.ID = idterminal; ;

					Fask.DataSets.Meny menyProvider = ((Fask.Server.Interfaces.Ciselniky.IMeny)provider).KatalogMen(terminal);
					meny = new Fask.SQLiteDBs.DataSets.Meny();
					foreach (Fask.DataSets.Meny.CZMST097Row srow in menyProvider.CZMST097)
					{
						srow.TrimStringColumns();
						meny.CZMST097.ImportRow(srow);
					}
				}
				else
				{
					string msg = "Provider v Ciselnik.asmx > 'KatalogMenyDBPrepare(byte idterminal)' nenastaven.";
					Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
					throw new Exception(msg);
				}

				//so.Write("Filling data to database");
				//SQLiteDBs.DataSets.MenyTableAdapters.CZMST097TableAdapter ota = new Fask.MST_W_Server.SQLiteDBs.DataSets.MenyTableAdapters.CZMST097TableAdapter();
				//ota.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + dstFile);
				//try
				//{
				//	if (meny != null)
				//	{
				//		ota.Connection.Open();
				//		var Ztransakce = ota.Connection.BeginTransaction();
				//		ota.Update(meny.CZMST097);
				//		Ztransakce.Commit();
				//	}
				//}
				//catch (Exception ex)
				//{
				//	Logging.ExceptionHandler2.Handle(ex);
				//}
				//finally
				//{
				//	if (ota != null)
				//	{
				//		if ((ota.Connection.State & ConnectionState.Open) == ConnectionState.Open)
				//			ota.Connection.Close();
				//		ota.Dispose();
				//	}
				//}


				//so.Write("Shrinking database");
				//SQLite_Classes.SQLite_Helper.Shrink("Data source=" + dstFile);


				try
				{
					using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Meny ConMeny = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Meny(dstFile))
					{
						so.Write("Filling data to database");
						ConMeny.Update(meny.CZMST097);

						so.Write("Shrinking database");
						ConMeny.Shrink();
					}
				}
				catch (System.Exception ex)
				{
					Fask.Logging.ExceptionHandler2.Handle(ex);
				}


				so.Write("Compressing database");
				Fask.Compressing.Zip.Compress(dstFile);

				so.Write("File succesfully prepared");

				so.SetOK();
				return so;
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, " (Terminál ID:" + idterminal.ToString() + ")");
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				so.Exception = true;
				so.Write(ex.Message);
				return so;
			}
			finally
			{
				so.Delete();
			}
        }

        /// <summary>
        /// Metoda pro pøípravu Souboru Odbìratele pro terminal
        /// </summary>
        /// <param name="idterminal">ID Terminalu</param>
        /// <param name="prefixskladu">Prefix skladu</param>
        /// <returns>True- OK, False- chyba</returns>
		[WebMethod(Description = "Metoda pro pøípravu Souboru Odbìratele pro terminal")]
		public StatusObject KatalogOdberateleDBPrepare(byte idterminal, string prefixskladu)
		{

			string dstFile = Path.Combine(Fask.MyPath.Path.SQLiteDBsDirectory, idterminal.ToString() + Common.Backslash + Common.Odberatele + Common.PRD);
			string statusFile = dstFile + Common.SO;

			StatusObject so = new StatusObject(statusFile);
			if (so.Exists)
			{
				so.Read();
				so.StatusText = "File is still preparing\n" + so.StatusText;
				so.Exception = true;
				return so;
			}

			if (File.Exists(dstFile))
			{
				so.StatusText = "File already prepared\n" + "Ready to download";
				so.Exception = false;
				return so;
			}

			try
			{
				so.Write("Preparing template");
				if (!Directory.Exists(Path.GetDirectoryName(dstFile)))
					Directory.CreateDirectory(Path.GetDirectoryName(dstFile));

				SQLite_Helper helper = new SQLite_Helper();
				if (!helper.SQLite_CreateFile(dstFile, Common.Odberatele))
				{
					throw new Exception("Nastala chyba pri tvorbe SQLite souboru pro " + Common.Odberatele);
				}
				so.Write("Downloading data");
				Fask.SQLiteDBs.DataSets.Odberatele odberatele = null;
				if (provider != null && (provider is Fask.Server.Interfaces.Ciselniky.IOdberatele)) //nove rozhrani objektove ...
				{
					Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();
					Fask.Server.Interfaces.Classes.Sklad sklad = new Fask.Server.Interfaces.Classes.Sklad();

					terminal.ID = idterminal;
					sklad.ID = prefixskladu;

					Odberatele odberateleprovider = ((Fask.Server.Interfaces.Ciselniky.IOdberatele)provider).KatalogOdberatele(terminal, sklad);
					odberatele = new Fask.SQLiteDBs.DataSets.Odberatele();
					foreach (Odberatele.CZMST090Row srow in odberateleprovider.CZMST090)
					{
						srow.TrimStringColumns();
						odberatele.CZMST090.ImportRow(srow);
					}
				}
				else
				{
					string msg = "Provider v Ciselnik.asmx > 'KatalogOdberateleDBPrepare(byte idterminal, string prefixskladu)' nenastaven.";
					Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
					throw new Exception(msg);
				}

				//so.Write("Filling data to database");
				//SQLiteDBs.DataSets.OdberateleTableAdapters.CZMST090TableAdapter ota = new Fask.MST_W_Server.SQLiteDBs.DataSets.OdberateleTableAdapters.CZMST090TableAdapter();
				//ota.Connection = new System.Data.SQLite.SQLiteConnection( "Data source=" + dstFile);
				//try
				//{
				//	if (odberatele != null)
				//	{
				//		ota.Connection.Open();
				//		var Ztransakce = ota.Connection.BeginTransaction();
				//		ota.Update(odberatele.CZMST090);
				//		Ztransakce.Commit();
				//	}
				//}
				//catch (Exception ex)
				//{
				//	Logging.ExceptionHandler2.Handle(ex);
				//}
				//finally
				//{
				//	if (ota != null)
				//	{
				//		if ((ota.Connection.State & ConnectionState.Open) == ConnectionState.Open)
				//			ota.Connection.Close();
				//		ota.Dispose();
				//	}
				//}

				//so.Write("Shrinking database");
    //            SQLite_Classes.SQLite_Helper.Shrink("Data source=" + dstFile);


				try
				{
					using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Odberatele ConOdb = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Odberatele(dstFile))
					{
						so.Write("Filling data to database");
						ConOdb.Update(odberatele.CZMST090);

						so.Write("Shrinking database");
						ConOdb.Shrink();
					}

				}
				catch (System.Exception ex)
				{
					Fask.Logging.ExceptionHandler2.Handle(ex);
				}

				so.Write("Compressing database");
				Fask.Compressing.Zip.Compress(dstFile);

				so.Write("File succesfully prepared");

				so.SetOK();
				return so;
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, " (Terminál ID:" + idterminal.ToString() + ")");
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				so.Exception = true;
				so.Write(ex.Message);
				return so;
			}
			finally
			{
				so.Delete();
			}
		}

        /// <summary>
        /// Metoda pro pøípravu Souboru Støediska pro terminal
        /// </summary>
        /// <param name="idterminal">ID Terminalu</param>
        /// <param name="prefixskladu">Prefix skladu</param>
        /// <returns>True- OK, False- chyba</returns>
		[WebMethod(Description = "Metoda pro pøípravu Souboru Støediska pro terminal")]
		public StatusObject KatalogStrediskaDBPrepare(byte idterminal, string prefixskladu)
		{

			string dstFile = Path.Combine(Fask.MyPath.Path.SQLiteDBsDirectory, idterminal.ToString() + Common.Backslash + Common.Strediska + Common.PRD);
			string statusFile = dstFile + Common.SO;

			StatusObject so = new StatusObject(statusFile);
			if (so.Exists)
			{
				so.Read();
				so.StatusText = "File is still preparing\n" + so.StatusText;
				so.Exception = true;
				return so;
			}

			if (File.Exists(dstFile))
			{
				so.StatusText = "File already prepared\n" + "Ready to download";
				so.Exception = false;
				return so;
			}

			try
			{
				so.Write("Preparing template");
				if (!Directory.Exists(Path.GetDirectoryName(dstFile)))
					Directory.CreateDirectory(Path.GetDirectoryName(dstFile));

				SQLite_Helper helper = new SQLite_Helper();
				if (!helper.SQLite_CreateFile(dstFile, Common.Strediska))
				{
					throw new Exception("Nastala chyba pri tvorbe SQLite souboru pro " + Common.Strediska);
				}
				so.Write("Downloading data");
				Fask.SQLiteDBs.DataSets.Strediska strediska = null;
				if (provider != null && (provider is Fask.Server.Interfaces.Ciselniky.IStrediska)) //nove rozhrani objektove ...
				{
					Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();
					Fask.Server.Interfaces.Classes.Sklad sklad = new Fask.Server.Interfaces.Classes.Sklad();

					terminal.ID = idterminal;
					sklad.ID = prefixskladu;

					Fask.Interfaces.DataSets.Strediska strediskaprovider = ((Fask.Server.Interfaces.Ciselniky.IStrediska)provider).KatalogStrediska(terminal, sklad);
					strediska = new Fask.SQLiteDBs.DataSets.Strediska();
					foreach (Fask.Interfaces.DataSets.Strediska.CZMST091Row srow in strediskaprovider.CZMST091)
					{
						srow.TrimStringColumns();
						strediska.CZMST091.ImportRow(srow);
					}
				}
				else
				{
					string msg = "Provider v Ciselnik.asmx > 'KatalogStrediskaDBPrepare(byte idterminal, string prefixskladu)' nenastaven.";
					Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
					throw new Exception(msg);
				}
				//so.Write("Filling data to database");
				//SQLiteDBs.DataSets.StrediskaTableAdapters.CZMST091TableAdapter sta = new Fask.MST_W_Server.SQLiteDBs.DataSets.StrediskaTableAdapters.CZMST091TableAdapter();
				//sta.Connection = new System.Data.SQLite.SQLiteConnection( "Data source=" + dstFile);
				//try
				//{
				//	if (strediska != null)
				//	{
				//		sta.Connection.Open();
				//		var Ztransakce = sta.Connection.BeginTransaction();
				//		sta.Update(strediska.CZMST091);
				//		Ztransakce.Commit();
				//	}
				//}
				//catch (Exception ex)
				//{
				//	Logging.ExceptionHandler2.Handle(ex);
				//}
				//finally
				//{
				//	if (sta != null)
				//	{
				//		if ((sta.Connection.State & ConnectionState.Open) == ConnectionState.Open)
				//			sta.Connection.Close();
				//		sta.Dispose();
				//	}
				//}

				//so.Write("Shrinking database");
    //            SQLite_Classes.SQLite_Helper.Shrink("Data source=" + dstFile);

				try
				{
					using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Strediska ConStr = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Strediska(dstFile))
					{

						so.Write("Filling data to database");
						ConStr.Update(strediska.CZMST091);

						so.Write("Shrinking database");
						ConStr.Shrink();
					}
				}
				catch (System.Exception ex)
				{
					Fask.Logging.ExceptionHandler2.Handle(ex);
				}

				so.Write("Compressing database");
				Fask.Compressing.Zip.Compress(dstFile);

				so.Write("File succesfully prepared");

				so.SetOK();
				return so;

			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, " (Terminál ID:" + idterminal.ToString() + ")");
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				so.Exception = true;
				so.Write(ex.Message);
				return so;
			}
			finally
			{
				so.Delete();
			}
		}

        /// <summary>
        /// Metoda pro pøípravu Souboru Precovnici pro terminal
        /// </summary>
        /// <param name="idterminal">ID Terminalu</param>
        /// <param name="prefixskladu">Prefix skladu</param>
        /// <returns>True- OK, False- chyba</returns>
		[WebMethod(Description = "Metoda pro pøípravu Souboru Precovnici pro terminal")]
		public StatusObject KatalogPracovniciDBPrepare(byte idterminal, string prefixskladu)
		{

			string dstFile = Path.Combine(Fask.MyPath.Path.SQLiteDBsDirectory, idterminal.ToString() + Common.Backslash + Common.Pracovnici + Common.PRD);
			string statusFile = dstFile + Common.SO;

			StatusObject so = new StatusObject(statusFile);
			if (so.Exists)
			{
				so.Read();
				so.StatusText = "File is still preparing\n" + so.StatusText;
				so.Exception = true;
				return so;
			}

			if (File.Exists(dstFile))
			{
				so.StatusText = "File already prepared\n" + "Ready to download";
				so.Exception = false;
				return so;
			}

			try
			{
				so.Write("Preparing template");
				if (!Directory.Exists(Path.GetDirectoryName(dstFile)))
					Directory.CreateDirectory(Path.GetDirectoryName(dstFile));

				SQLite_Helper helper = new SQLite_Helper();
				if (!helper.SQLite_CreateFile(dstFile, Common.Pracovnici))
				{
					throw new Exception("Nastala chyba pri tvorbe SQLite souboru pro " + Common.Pracovnici);
				}
				so.Write("Downloading data");
				Fask.SQLiteDBs.DataSets.Pracovnici pracovnici = null;
				if (provider != null && (provider is Fask.Server.Interfaces.Ciselniky.IPracovnici)) //nove rozhrani objektove ...
				{
					Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();
					Fask.Server.Interfaces.Classes.Sklad sklad = new Fask.Server.Interfaces.Classes.Sklad();

					terminal.ID = idterminal;
					sklad.ID = prefixskladu;

					Fask.DataSets.Pracovnici pracovniciprovider = ((Fask.Server.Interfaces.Ciselniky.IPracovnici)provider).KatalogPracovnici(terminal, sklad);
					pracovnici = new Fask.SQLiteDBs.DataSets.Pracovnici();
					foreach (Fask.DataSets.Pracovnici.CZMST096Row srow in pracovniciprovider.CZMST096)
					{
						srow.TrimStringColumns();
						pracovnici.CZMST096.ImportRow(srow);
					}
				}
				else
				{
					string msg = string.Empty;
					try
					{
						File.Delete(dstFile);
					}
					catch (Exception ex)
					{
						msg = "Chyba pøi mazani SQLite souboru Pracovnici." + Environment.NewLine;
						Logging.ExceptionHandler2.Handle(ex);
					}

					 msg += "Provider v Ciselnik.asmx > 'KatalogPracovniciDBPrepare(byte idterminal, string prefixskladu)' nenastaven.";
					Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
					throw new Exception(msg);
				}

				//so.Write("Filling data to database");
				//SQLiteDBs.DataSets.PracovniciTableAdapters.CZMST096TableAdapter pta = new Fask.MST_W_Server.SQLiteDBs.DataSets.PracovniciTableAdapters.CZMST096TableAdapter();
				//pta.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + dstFile);
				//try
				//{
				//	if (pracovnici != null)
				//	{
				//		pta.Connection.Open();
				//		var Ztransakce = pta.Connection.BeginTransaction();
				//		pta.Update(pracovnici.CZMST096);
				//		Ztransakce.Commit();
				//	}
				//}
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

				//so.Write("Shrinking database");
    //            SQLite_Classes.SQLite_Helper.Shrink("Data source=" + dstFile);


				try
				{
					using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Pracovnici ConPra = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Pracovnici(dstFile))
					{

						so.Write("Filling data to database");
						ConPra.Update(pracovnici.CZMST096);

						so.Write("Shrinking database");
						ConPra.Shrink();
					}
				}
				catch (System.Exception ex)
				{
					Fask.Logging.ExceptionHandler2.Handle(ex);
				}


				so.Write("Compressing database");
				Fask.Compressing.Zip.Compress(dstFile);

				so.Write("File succesfully prepared");

				so.SetOK();
				return so;
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, " (Terminál ID:" + idterminal.ToString() + ")");
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				so.Exception = true;
				so.Write(ex.Message);
				return so;
			}
			finally
			{
				so.Delete();
			}
		}

        /// <summary>
        /// Metoda pro pøípravu Souboru Typy Dokladu pro terminal
        /// </summary>
        /// <param name="idterminal">ID Terminalu</param>
        /// <param name="prefixskladu">Prefix skladu</param>
        /// <returns>True- OK, False- chyba</returns>
		[WebMethod(Description = "Metoda pro pøípravu Souboru Typy Dokladu pro terminal")]
		public StatusObject KatalogTypDokladuDBPrepare(byte idterminal, string prefixskladu)
		{
			Fask.SQLiteDBs.DataSets.TypDokladu typdokladu = null;

			string dstFile = Path.Combine(Fask.MyPath.Path.SQLiteDBsDirectory, idterminal.ToString() + Common.Backslash + Common.TypDokladu + Common.PRD);
			string statusFile = dstFile + Common.SO;

			StatusObject so = new StatusObject(statusFile);
			if (so.Exists)
			{
				so.Read();
				so.StatusText = "File is still preparing\n" + so.StatusText;
				so.Exception = true;
				return so;
			}

			if (File.Exists(dstFile))
			{
				so.StatusText = "File already prepared\n" + "Ready to download";
				so.Exception = false;
				return so;
			}
			try
			{
				so.Write("Preparing template");
				if (!Directory.Exists(Path.GetDirectoryName(dstFile)))
					Directory.CreateDirectory(Path.GetDirectoryName(dstFile));


				SQLite_Helper helper = new SQLite_Helper();
				if (!helper.SQLite_CreateFile(dstFile, Common.TypDokladu))
				{
					throw new Exception("Nastala chyba pri tvorbe SQLite souboru pro " + Common.TypDokladu);
				}
				so.Write("Downloading data");
				if (provider != null && (provider is Fask.Server.Interfaces.Ciselniky.ITypDokladu)) //nove rozhrani objektove ...
				{
					Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();
					Fask.Server.Interfaces.Classes.Sklad sklad = new Fask.Server.Interfaces.Classes.Sklad();

					terminal.ID = idterminal;
					sklad.ID = prefixskladu;

					Fask.DataSets.TypDokladu typdokladuprovider = ((Fask.Server.Interfaces.Ciselniky.ITypDokladu)provider).KatalogTypDokladu(terminal, sklad);
					typdokladu = new Fask.SQLiteDBs.DataSets.TypDokladu();
					foreach (Fask.DataSets.TypDokladu.CZMST092Row row in typdokladuprovider.CZMST092)
					{
						row.SetAdded();
						row.TrimStringColumns();
						typdokladu.CZMST092.ImportRow(row);
					}
				}
				else
				{
					string msg = "Provider v Ciselnik.asmx > 'KatalogTypDokladuDBPrepare(byte idterminal, string prefixskladu)' nenastaven.";
					//Log.writeErrorLog(msg);
					Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
					throw new Exception(msg);
				}

				//so.Write("Filling data to database");
				//SQLiteDBs.DataSets.TypDokladuTableAdapters.CZMST092TableAdapter tdta = new Fask.MST_W_Server.SQLiteDBs.DataSets.TypDokladuTableAdapters.CZMST092TableAdapter();
				//tdta.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + dstFile);
				//try
				//{
				//	if (typdokladu != null)
				//	{
				//		tdta.Connection.Open();
				//		var Ztransakce = tdta.Connection.BeginTransaction();
				//		tdta.Update(typdokladu.CZMST092);
				//		Ztransakce.Commit();
				//	}
				//}
				//catch (Exception ex)
				//{
				//	Logging.ExceptionHandler2.Handle(ex);
				//}
				//finally
				//{
				//	if (tdta != null)
				//	{
				//		if ((tdta.Connection.State & ConnectionState.Open) == ConnectionState.Open)
				//			tdta.Connection.Close();
				//		tdta.Dispose();
				//	}
				//}

				//so.Write("Shrinking database");
    //            SQLite_Classes.SQLite_Helper.Shrink("Data source=" + dstFile);

				try
				{
					using (Fask.SQLiteDBs.Controllers.SQLite_Controller_TypDokladu ConTD = new Fask.SQLiteDBs.Controllers.SQLite_Controller_TypDokladu(dstFile))
					{
						so.Write("Filling data to database");
						ConTD.Update(typdokladu.CZMST092);

						so.Write("Shrinking database");
						ConTD.Shrink();
					}

				}
				catch (System.Exception ex)
				{
					Fask.Logging.ExceptionHandler2.Handle(ex);
				}

				so.Write("Compressing database");
				Fask.Compressing.Zip.Compress(dstFile);

				so.Write("File succesfully prepared");

				so.SetOK();
				return so;
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, " (Terminál ID:" + idterminal.ToString() + ")");
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				so.Exception = true;
				so.Write(ex.Message);
				return so;
			}
			finally
			{
				so.Delete();
			}
		}

        /// <summary>
        /// Metoda pro pøípravu Souboru SKlady pro terminal
        /// </summary>
        /// <param name="idterminal">ID Terminalu</param>
        /// <returns>True- OK, False- chyba</returns>
        [WebMethod(Description = "Metoda pro pøípravu Souboru SKlady pro terminal")]
        public StatusObject KatalogSkladyDBPrepare(byte idterminal)
        {
 
				string dstFile = Path.Combine(Fask.MyPath.Path.SQLiteDBsDirectory, idterminal.ToString() + Common.Backslash + Common.Sklady + Common.PRD);
				string statusFile = dstFile + Common.SO;

				StatusObject so = new StatusObject(statusFile);
				if (so.Exists)
				{
					so.Read();
					so.StatusText = "File is still preparing\n" + so.StatusText;
					so.Exception = true;
					return so;
				}

				if (File.Exists(dstFile))
				{
					so.StatusText = "File already prepared\n" + "Ready to download";
					so.Exception = false;
					return so;
				}

			try
			{
				so.Write("Preparing template");
				if (!Directory.Exists(Path.GetDirectoryName(dstFile)))
					Directory.CreateDirectory(Path.GetDirectoryName(dstFile));


				SQLite_Helper helper = new SQLite_Helper();
				if (!helper.SQLite_CreateFile(dstFile, Common.Sklady))
				{
					throw new Exception("Nastala chyba pri tvorbe SQLite souboru pro " + Common.Sklady);
				}
				so.Write("Downloading data");
				Fask.SQLiteDBs.DataSets.Sklady sklady = null;
				if (provider != null && (provider is Fask.Server.Interfaces.Ciselniky.ISklady)) //nove rozhrani objektove ...
				{
					Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();

					terminal.ID = idterminal;

					Fask.Interfaces.DataSets.Sklady skladyprovider = ((Fask.Server.Interfaces.Ciselniky.ISklady)provider).KatalogSklady(terminal);
					sklady = new Fask.SQLiteDBs.DataSets.Sklady();
					foreach (Fask.Interfaces.DataSets.Sklady.CZMST093Row row in skladyprovider.CZMST093)
					{
						row.TrimStringColumns();
						sklady.CZMST093.ImportRow(row);
					}
				}
				else
				{
					string msg = "Provider v Ciselnik.asmx > 'KatalogSkladyDBPrepare(byte idterminal)' nenastaven.";
					Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
					throw new Exception(msg);
				}

				//so.Write("Filling data to database");
				//SQLiteDBs.DataSets.SkladyTableAdapters.CZMST093TableAdapter sta = new Fask.MST_W_Server.SQLiteDBs.DataSets.SkladyTableAdapters.CZMST093TableAdapter();
				//sta.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + dstFile);
				//try
				//{
				//	if (sklady != null)
				//	{
				//		sta.Connection.Open();
				//		var Ztransakce = sta.Connection.BeginTransaction();
				//		sta.Update(sklady.CZMST093);
				//		Ztransakce.Commit();
				//	}
				//}
				//catch (Exception ex)
				//{
				//	Logging.ExceptionHandler2.Handle(ex);
				//}
				//finally
				//{
				//	if (sta != null)
				//	{
				//		if ((sta.Connection.State & ConnectionState.Open) == ConnectionState.Open)
				//			sta.Connection.Close();
				//		sta.Dispose();
				//	}
				//}

				//so.Write("Shrinking database");
    //            SQLite_Classes.SQLite_Helper.Shrink("Data source=" + dstFile);

				try
				{
					using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Sklady ConSkl = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Sklady(dstFile))
					{

						so.Write("Filling data to database");
						ConSkl.Update(sklady.CZMST093);

						so.Write("Shrinking database");
						ConSkl.Shrink();
					}
				}
				catch (System.Exception ex)
				{
					Fask.Logging.ExceptionHandler2.Handle(ex);
				}


				so.Write("Compressing database");
				Fask.Compressing.Zip.Compress(dstFile);

				so.Write("File succesfully prepared");

				so.SetOK();
				return so;
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, " (Terminál ID:" + idterminal.ToString() + ")");
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				so.Exception = true;
				so.Write(ex.Message);
				return so;
			}
			finally
			{
				so.Delete();
			}


        }

		/// <summary>
		/// Metoda pro pøípravu Souboru Lokace pro terminal
		/// </summary>
		/// <param name="idterminal">ID Terminalu</param>
		/// <param name="prefixskladu">Prefix skladu</param>
		/// <returns>True- OK, False- chyba</returns>
		[WebMethod(Description = "Metoda pro pøípravu Souboru Lokace pro terminal")]
		public StatusObject KatalogLokaceDBPrepare(byte idterminal, string prefixskladu)
		{

			string dstFile = Path.Combine(Fask.MyPath.Path.SQLiteDBsDirectory, idterminal.ToString() + Common.Backslash + Common.Lokace + Common.PRD);
			string statusFile = dstFile + Common.SO;

			StatusObject so = new StatusObject(statusFile);
			if (so.Exists)
			{
				so.Read();
				so.StatusText = "File is still preparing\n" + so.StatusText;
				so.Exception = true;
				return so;
			}

			if (File.Exists(dstFile))
			{
				so.StatusText = "File already prepared\n" + "Ready to download";
				so.Exception = false;
				return so;
			}

			try
			{
				so.Write("Preparing template");
				if (!Directory.Exists(Path.GetDirectoryName(dstFile)))
					Directory.CreateDirectory(Path.GetDirectoryName(dstFile));

				SQLite_Helper helper = new SQLite_Helper();
				if (!helper.SQLite_CreateFile(dstFile, Common.Lokace))
				{
					throw new Exception("Nastala chyba pri tvorbe SQLite souboru pro " + Common.Lokace);
				}
				so.Write("Downloading data");
				Fask.SQLiteDBs.DataSets.Lokace lokace = null;
				if (provider != null && (provider is Fask.Server.Interfaces.Ciselniky.ILokace)) //nove rozhrani objektove ...
				{
					Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();
					Fask.Server.Interfaces.Classes.Sklad sklad = new Fask.Server.Interfaces.Classes.Sklad();

					terminal.ID = idterminal;
					sklad.ID = prefixskladu;

					Fask.DataSets.Lokace lokaceprovider = ((Fask.Server.Interfaces.Ciselniky.ILokace)provider).KatalogLokace(terminal, sklad);
					lokace = new Fask.SQLiteDBs.DataSets.Lokace();
					foreach (Fask.DataSets.Lokace.CZMST094Row row in lokaceprovider.CZMST094)
					{
						row.TrimStringColumns();
						lokace.CZMST094.ImportRow(row);
					}

					foreach (Fask.DataSets.Lokace.CZMST_SkladLokace_LokaceTypyRow row in lokaceprovider.CZMST_SkladLokace_LokaceTypy)
					{
						row.TrimStringColumns();
						lokace.CZMST_SkladLokace_LokaceTypy.ImportRow(row);
					}
				}
				else
				{
					string msg = "Provider v Ciselnik.asmx > 'KatalogLokaceDBPrepare(byte idterminal, string prefixskladu)' nenastaven.";
					Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
					throw new Exception(msg);
				}

				so.Write("Filling data to database");
				//SQLiteDBs.DataSets.LokaceTableAdapters.CZMST094TableAdapter sta = new Fask.MST_W_Server.SQLiteDBs.DataSets.LokaceTableAdapters.CZMST094TableAdapter();
				//sta.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + dstFile);
				//try
				//{
				//	if (lokace != null)
				//	{
				//		sta.Connection.Open();
				//		var LokTransakce = sta.Connection.BeginTransaction();
				//		sta.Update(lokace.CZMST094);
				//		LokTransakce.Commit();
				//	}
				//}
				//catch (Exception ex)
				//{
				//	Logging.ExceptionHandler2.Handle(ex);
				//}
				//finally
				//{
				//	if (sta != null)
				//	{
				//		if ((sta.Connection.State & ConnectionState.Open) == ConnectionState.Open)
				//			sta.Connection.Close();
				//		sta.Dispose();
				//	}
				//}

				//SQLiteDBs.DataSets.LokaceTableAdapters.CZMST_SkladLokace_LokaceTypyTableAdapter typyta = new Fask.MST_W_Server.SQLiteDBs.DataSets.LokaceTableAdapters.CZMST_SkladLokace_LokaceTypyTableAdapter();
				//typyta.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + dstFile);
				//try
				//{
				//	if (lokace != null)
				//	{
				//		typyta.Connection.Open();
				//		var Ztransakce = typyta.Connection.BeginTransaction();
				//		typyta.Update(lokace.CZMST_SkladLokace_LokaceTypy);
				//		Ztransakce.Commit();
				//	}
				//}
				//catch (Exception ex)
				//{
				//	Logging.ExceptionHandler2.Handle(ex);
				//}
				//finally
				//{
				//	if (typyta != null)
				//	{
				//		if ((typyta.Connection.State & ConnectionState.Open) == ConnectionState.Open)
				//			typyta.Connection.Close();
				//		typyta.Dispose();
				//	}
				//}

				//so.Write("Shrinking database");
               // SQLite_Classes.SQLite_Helper.Shrink("Data source=" + dstFile);


				try
				{
					using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Lokace ConLok = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Lokace(dstFile))
					{
						so.Write("Filling data to database");

						ConLok.Update_CZMST094(lokace.CZMST094);
						ConLok.Update_CZMST_SkladLokace_LokaceTypy(lokace.CZMST_SkladLokace_LokaceTypy);

						so.Write("Shrinking database");
						ConLok.Shrink();
					}
				}
				catch (System.Exception ex)
				{
					Fask.Logging.ExceptionHandler2.Handle(ex);
				}


				so.Write("Compressing database");
				Fask.Compressing.Zip.Compress(dstFile);

				so.Write("File succesfully prepared");

				so.SetOK();
				return so;
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, " (Terminál ID:" + idterminal.ToString() + ")");
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				so.Exception = true;
				so.Write(ex.Message);
				return so;
			}
			finally
			{
				so.Delete();
			}
		}

        /// <summary>
        /// Metoda pro pøípravu Souboru Typy udalosti pro terminal
        /// </summary>
        /// <param name="idterminal">ID Terminalu</param>
        /// <returns>True- OK, False- chyba</returns>
		[WebMethod(Description = "Metoda pro pøípravu Souboru Typy udalosti pro terminal")]
		public StatusObject KatalogEventTypesDBPrepare(byte idterminal)
		{
            Konfigurace.Classes.Globals_Konfig_WebConfig.LoadConfiguration();

            string dstFile = Path.Combine(Fask.MyPath.Path.SQLiteDBsDirectory, idterminal.ToString() + Common.Backslash + Common.EventTypes + Common.PRD);
			string statusFile = dstFile + Common.SO;

			StatusObject so = new StatusObject(statusFile);
			if (so.Exists)
			{
				so.Read();
				so.StatusText = "File is still preparing\n" + so.StatusText;
				so.Exception = true;
				return so;
			}

			if (File.Exists(dstFile))
			{
				so.StatusText = "File already prepared\n" + "Ready to download";
				so.Exception = false;
				return so;
			}
			
			try
			{
				so.Write("Preparing template");
				if (!Directory.Exists(Path.GetDirectoryName(dstFile)))
					Directory.CreateDirectory(Path.GetDirectoryName(dstFile));

				SQLite_Helper helper = new SQLite_Helper();
				if (!helper.SQLite_CreateFile(dstFile, Common.EventTypes))
				{
					throw new Exception("Nastala chyba pri tvorbe SQLite souboru pro " + Common.EventTypes);
				}
				so.Write("Downloading data");
				string select = "SELECT * FROM CZMST_EVENTSTYPES";

				SqlDataAdapter xda = new SqlDataAdapter(select, Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.ConnectionString[0].FASKDB);

				//Nacteni dat prijemky z databaze                    
				Fask.SQLiteDBs.DataSets.EventsTypes events = new Fask.SQLiteDBs.DataSets.EventsTypes();
				xda.Fill(events, events.CZMST_EventsTypes.TableName);

				//Ulozeni dat pro terminal
				foreach (Fask.SQLiteDBs.DataSets.EventsTypes.CZMST_EventsTypesRow erow in events.CZMST_EventsTypes)
				{
					erow.TrimStringColumns();
					erow.SetAdded();
				}

				
				//SQLiteDBs.DataSets.EventsTableAdapters.CZMST_EventsTypesTableAdapter eta = new Fask.MST_W_Server.SQLiteDBs.DataSets.EventsTableAdapters.CZMST_EventsTypesTableAdapter();
				//eta.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + dstFile);
				//try
				//{
				//	if (events != null)
				//	{
				//		eta.Connection.Open();
				//		var Ztransakce = eta.Connection.BeginTransaction();
				//		eta.Update(events.CZMST_EventsTypes);
				//		Ztransakce.Commit();
				//	}
				//}
				//catch (Exception ex)
				//{
				//	Logging.ExceptionHandler2.Handle(ex);
				//}
				//finally
				//{
				//	if (eta != null)
				//	{
				//		if ((eta.Connection.State & ConnectionState.Open) == ConnectionState.Open)
				//			eta.Connection.Close();
				//		eta.Dispose();
				//	}
				//}

				using(Fask.SQLiteDBs.Controllers.SQLite_Controller_EventsTypes ConEvTyp = new Fask.SQLiteDBs.Controllers.SQLite_Controller_EventsTypes(dstFile))
				{
					so.Write("Filling data to database");
					ConEvTyp.Update(events.CZMST_EventsTypes);

					so.Write("Shrinking database");
					ConEvTyp.Shrink();
				}

					//so.Write("Shrinking database");
                //SQLite_Classes.SQLite_Helper.Shrink("Data source=" + dstFile);

				so.Write("Compressing database");
				Fask.Compressing.Zip.Compress(dstFile);

				so.Write("File succesfully prepared");

				so.SetOK();
				return so;
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, " (Terminál ID:" + idterminal.ToString() + ")");
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				so.Exception = true;
				so.Write(ex.Message);
				return so;
			}
			finally
			{
				so.Delete();
			}
		}

        /// <summary>
        /// Metoda pro pøípravu Souboru Tiskarny pro terminal
        /// </summary>
        /// <param name="idterminal">ID Terminalu</param>
        /// <returns>True- OK, False- chyba</returns>
		[WebMethod(Description = "Metoda pro pøípravu Souboru Tiskarny pro terminal")]
		public StatusObject KatalogTiskarnyDBPrepare(byte idterminal)
		{
            Konfigurace.Classes.Globals_Konfig_WebConfig.LoadConfiguration();
            

			string dstFile = Path.Combine(Fask.MyPath.Path.SQLiteDBsDirectory, idterminal.ToString() + Common.Backslash + Common.Tiskarny + Common.PRD);
			string statusFile = dstFile + Common.SO;

			StatusObject so = new StatusObject(statusFile);
			if (so.Exists)
			{
				so.Read();
				so.StatusText = "File is still preparing\n" + so.StatusText;
				so.Exception = true;
				return so;
			}

			if (File.Exists(dstFile))
			{
				so.StatusText = "File already prepared\n" + "Ready to download";
				so.Exception = false;
				return so;
			}

			try
			{
				so.Write("Preparing template");
				if (!Directory.Exists(Path.GetDirectoryName(dstFile)))
					Directory.CreateDirectory(Path.GetDirectoryName(dstFile));


				SQLite_Helper helper = new SQLite_Helper();
				if (!helper.SQLite_CreateFile(dstFile, Common.Tiskarny))
				{
					throw new Exception("Nastala chyba pri tvorbe SQLite souboru pro " + Common.Tiskarny);
				}
				so.Write("Downloading data");
				Fask.SQLiteDBs.DataSets.Tiskarny tiskarna = null;

				string select = "SELECT * FROM CZMST_TISKARNA";
				SqlDataAdapter xda = new SqlDataAdapter(select, Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.ConnectionString[0].FASKDB);
				//Nacteni dat prijemky z databaze                    
				tiskarna = new Fask.SQLiteDBs.DataSets.Tiskarny();
				xda.Fill(tiskarna, tiskarna.CZMST_TISKARNA.TableName);
				//Ulozeni dat pro terminal
				foreach (Fask.SQLiteDBs.DataSets.Tiskarny.CZMST_TISKARNARow trow in tiskarna.CZMST_TISKARNA)
				{
					trow.TrimStringColumns();
					trow.SetAdded();
				}

				//so.Write("Filling data to database");
				//SQLiteDBs.DataSets.TiskarnaTableAdapters.CZMST_TISKARNATableAdapter tta = new Fask.MST_W_Server.SQLiteDBs.DataSets.TiskarnaTableAdapters.CZMST_TISKARNATableAdapter();
				//tta.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + dstFile);
				//try
				//{
				//	if (tiskarna != null)
				//	{
				//		tta.Connection.Open();
				//		var Ztransakce = tta.Connection.BeginTransaction();
				//		tta.Update(tiskarna.CZMST_TISKARNA);
				//		Ztransakce.Commit();
				//	}
				//}
				//catch (Exception ex)
				//{
				//	Logging.ExceptionHandler2.Handle(ex);
				//}
				//finally
				//{
				//	if (tta != null)
				//	{
				//		if ((tta.Connection.State & ConnectionState.Open) == ConnectionState.Open)
				//			tta.Connection.Close();
				//		tta.Dispose();
				//	}
				//}

				//so.Write("Shrinking database");
    //            SQLite_Classes.SQLite_Helper.Shrink("Data source=" + dstFile);


				try
				{
					using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Tiskarny ConTisk = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Tiskarny(dstFile))
					{
						so.Write("Filling data to database");
						ConTisk.Update(tiskarna.CZMST_TISKARNA);

						so.Write("Shrinking database");
						ConTisk.Shrink();
					}

				}
				catch (System.Exception ex)
				{
					Fask.Logging.ExceptionHandler2.Handle(ex);
				}


				so.Write("Compressing database");
				Fask.Compressing.Zip.Compress(dstFile);

				so.Write("File succesfully prepared");

				so.SetOK();
				return so;

			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, " (Terminál ID:" + idterminal.ToString() + ")");
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				so.Exception = true;
				so.Write(ex.Message);
				return so;
			}
			finally
			{
				so.Delete();
			}
		}

        #endregion

    }
}
