// JS  : 11.2.2016
// \TODO : - LOCNCODE LIKE zmeneno na SKL_ID LIKE => proverit ostatni providery a funkce a upravit take... 

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data.Common;
using System.IO;
using System.Reflection;
using Fask.Server.Interfaces.Classes;
using Fask.Tracing;
using Fask.Logging;
using Fask.MST_W_Server.Constants;
using Fask.MST_W_Server.SQLite_Classes;
using System.Linq;

namespace Fask.MST_W_Server
{
    /// <summary>
	/// Služba pøenosu dat výdeje. Tato služna slouží pro WebReferenci MSTW øešení pro WindowsCE, Windows a v budoucnu i Android øešení. Jedná se o SOAP komunikaci. V budoucnu muže pøibudnou i komprimace a šifrovani SOAP pøenosu
    /// </summary>
	[WebService(Namespace = "http://Vydej.fask.cz/", Description = "Služba pøenosu dat výdeje. Tato služna slouží pro WebReferenci MSTW øešení pro WindowsCE, Windows a v budoucnu i Android øešení. Jedná se o SOAP komunikaci. V budoucnu muže pøibudnou i komprimace a šifrovani SOAP pøenosu", Name = "VydejService")]
    public class Vydej : System.Web.Services.WebService
    {
		public enum ProcessVydejState
		{
			Uvolnit,
			Zpracovat,
			ZpracovatAPokracovat
		}

		#region Lokalne promenne

		private Fask.Server.Interfaces.IWebModule provider = null;
		const string VydejDBFileExtension = @".vi";

		#endregion

		#region Konstruktor
		/// <summary>
		/// Konstruktor
		/// </summary>
		public Vydej()
		{
			//CODEGEN: This call is required by the ASP.NET Web Services Designer
			InitializeComponent();

			// Inicializuje objektove rozhrani ...
			try
			{
                Konfigurace.Classes.Globals_Konfig_WebConfig.LoadConfiguration();
                string providerAssemblyPath = Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Providers[0].Provider_Vydej;
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
								if (typeof(Fask.Server.Interfaces.IWebModule).IsAssignableFrom(t))
								{
									provider = (Fask.Server.Interfaces.IWebModule)providerAssemlby.CreateInstance(t.FullName);
									if (provider != null)
									{
										if (provider is Fask.Server.Interfaces.Configuration.IConfiguration)
										{
											((Fask.Server.Interfaces.Configuration.IConfiguration)provider).LoadConfiguration();
										}
										break;
									}
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

        #region Component Designer generated code

        //Required by the Web Services Designer 
        private IContainer components = null;

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion

		#region WebMetody

		/// <summary>
		/// Metoda která vraci výdejky
		/// </summary>
		/// <param name="idterminal">ID Termínálu (pro kontrolu v CZ_DOSLO)</param>
		/// <param name="prefixskladu">Prefix Skladu ( SKL_ID LIKE 'xxx%' </param>
		/// <param name="itemtype">Typ položky (ITEMTYPE LIKE 'xxx%')</param>
		/// <param name="userid">ID uživatele (nepouživa se)</param>
		/// <returns>Dataset výdej, naplnení položkama</returns>
		[WebMethod(Description = "Metoda která vraci výdejky")]
		public Fask.DataSets.Vydejky GetVydejky(byte idterminal, string prefixskladu, string itemtype, int userid)
		{
			TracId tracid = new TracId(userid, idterminal, null, "GetVydejky");
			try
			{
				#region trace
				Trac.Write("Start", tracid);
				#endregion


				if ((provider != null) && (provider is Fask.Server.Interfaces.Vydej.IVydej))
				{


					#region trace
					Trac.Write("Provider start", tracid);
					#endregion

					try
					{
						Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();
						Fask.Server.Interfaces.Classes.Sklad sklad = new Fask.Server.Interfaces.Classes.Sklad();
						Fask.Server.Interfaces.Classes.Item item = new Fask.Server.Interfaces.Classes.Item();
						Fask.Server.Interfaces.Classes.User user = new Fask.Server.Interfaces.Classes.User();

						terminal.ID = idterminal;
						sklad.ID = prefixskladu;
						item.Type = itemtype;
						user.ID = userid;

						return (provider as Fask.Server.Interfaces.Vydej.IVydej).Vydej_GetVydejky(
							terminal,
							sklad,
							item,
							user
							);
					}
					catch (Exception ex)
					{
						Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
						#region trace
						Trac.Write(ex, tracid); 
						#endregion
						throw ex;
					}
					finally
					{
						#region trace
						Trac.Write("Provider end", tracid); 
						#endregion
					}
				}

				string msg = "Provider v Vydej.asmx > 'GetVydejky(byte idterminal, string prefixskladu, string itemtype, int userid)' nenastaven.";
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
				#region trace
				Trac.Write(msg, tracid); 
				#endregion
				throw new Exception(msg);

			}
			finally
			{
				#region trace
				Trac.Write("End", tracid); 
				#endregion
			}
		} 
        
        /// <summary>
        /// Metoda pro dotažení Výdejky podle èísla dávky.
        /// </summary>
        /// <param name="countentries">èíslo dávky</param>
        /// <param name="idterminal">ID terminálu</param>
        /// <param name="itemtype">typ položky</param>
        /// <returns>dataset výdej naplnení položkama</returns>
		[WebMethod(Description = "Metoda pro dotažení Výdejky podle èísla dávky.")]
        public Fask.DataSets.Vydej GetVydejka(int countentries, byte idterminal, string itemtype)
        {
			// \TODO: Pøidat do parametru metody id uživatele

			#region trace
			TracId tracid = new TracId(null, idterminal, countentries, "GetVydejka"); 
			#endregion

            try
            {
                #region trace
                Trac.Write("Start", tracid);
                #endregion

                if ((provider != null) && (provider is Fask.Server.Interfaces.Vydej.IVydej))
                {
                    #region trace
                    Trac.Write("Provider Start", tracid);
                    #endregion

                    try
                    {
                        Fask.Server.Interfaces.Classes.Davka davka = new Fask.Server.Interfaces.Classes.Davka();
                        Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();
                        Fask.Server.Interfaces.Classes.Sklad sklad = new Fask.Server.Interfaces.Classes.Sklad();
                        Fask.Server.Interfaces.Classes.Item item = new Fask.Server.Interfaces.Classes.Item();

                        davka.ID = countentries;
                        terminal.ID = idterminal;
                        item.Type = itemtype;

                        Fask.DataSets.Vydej vydej = (provider as Fask.Server.Interfaces.Vydej.IVydej).Vydej_GetVydejka(
                            davka,
                            terminal,
                            sklad,
                            item
                            );

                        // \bug ?? je to nutne?? dotahuje se v provideru
                        //vydej.ReadXml(Fask.MyPath.Path.VydejParams);

                        Konfigurace.Classes.Globals_Konfig_Agendy.LoadConfiguration();
                        vydej.Parametry.ImportRow(Konfigurace.Classes.Globals_Konfig_Agendy.Konfigurace.VydejParametry[0]);
                        vydej.AcceptChanges();

                        #region trace
                        Trac.Write(vydej, "GetVydejka, provider not null", tracid);
                        #endregion


                        return vydej;

                    }
                    catch (Exception ex)
                    {
                        Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                        #region trace
                        Trac.Write(ex, tracid);
                        #endregion
                        throw ex;
                    }
                    finally
                    {
                        #region trace
                        Trac.Write("Provider End", tracid);
                        #endregion
                    }
                }
            }
            finally
            {
				#region trace
				Trac.Write("End", tracid); 
				#endregion
            }

			string msg = "Provider v Vydej.asmx > 'GetVydejka(int countentries, byte idterminal, string itemtype)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
			#region trace
			Trac.Write(msg, tracid); 
			#endregion
			throw new Exception(msg);

		}

		/// <summary>
		/// Metoda sloužící pro vygenerovaní .prd souboru pro terminál s datama dávky podle èísla dávky
		/// </summary>
		/// <param name="countentries">èíslo dávky</param>
		/// <param name="idterminal">ID terminálu</param>
		/// <param name="itemtype">Typ položky</param>
		/// <returns>True - uspešne vygenerovane, False - nenastane, rovno to hodi exception</returns>
		[WebMethod(Description = "Metoda sloužící pro vygenerovaní .PRD souboru pro terminál s datama dávky podle èísla dávky")]
        public bool PrepareVydejkaDB(int countentries, byte idterminal, string itemtype)
        {
            TracId tracid = new TracId(null, idterminal, countentries, "GetVydejkaDBFile");
            try
            {
                #region trace
                Trac.Write("Start", tracid);
                #endregion

                string dstFile = Path.Combine(Fask.MyPath.Path.SQLiteDBsDirectory, idterminal.ToString() + @"\" + countentries.ToString() + VydejDBFileExtension); // cílový soubor

                if (!Directory.Exists(Path.GetDirectoryName(dstFile)))
					Directory.CreateDirectory(Path.GetDirectoryName(dstFile));


				SQLite_Helper helper = new SQLite_Helper();
				if (!helper.SQLite_CreateFile(dstFile, Common.Vydej))
				{
					throw new Exception("Nastala chyba pri tvorbe SQLite souboru pro " + Common.Vydej);
				}


                Fask.DataSets.Vydej vydejka = GetVydejka(countentries, idterminal, itemtype);

				#region Insert to SE

				//Fask.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SETableAdapter seta = new Fask.MST_W_Server.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SETableAdapter();
				//seta.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + dstFile);

				//try
				//{
				//	seta.Connection.Open();
				//	var seTransakce = seta.Connection.BeginTransaction();

				//	foreach (Fask.DataSets.Vydej.CZMST_SERow sr in vydejka.CZMST_SE)
				//	{
				//		int rowinserted = seta.Insert(
				//			CountEntries: sr.CountEntries,
				//			SOPNUMBE: sr.SOPNUMBE.Trim(),
				//			ITEMNMBR: sr.ITEMNMBR.Trim(),
				//			ITEMTYPE: sr.IsITEMTYPENull() ? string.Empty : sr.ITEMTYPE.Trim(),
				//			ITEMDESC: sr.IsITEMDESCNull() ? string.Empty : sr.ITEMDESC.Trim(),
				//			VNDDOCNM: sr.IsVNDDOCNMNull() ? string.Empty : sr.VNDDOCNM.Trim(),
				//			VNDITNUM: sr.IsVNDITNUMNull() ? string.Empty : sr.VNDITNUM.Trim(),
				//			ORD: sr.ORD,
				//			CZ_CarKod: sr.CZ_CarKod.Trim(),
				//			LOCNCODE: sr.IsLOCNCODENull() ? string.Empty : sr.LOCNCODE.Trim(),
				//			QTYSHPPD: sr.QTYSHPPD,
				//			QTYPACK: sr.QTYPACK,
				//			CZ_DatVyr_Track: sr.CZ_DatVyr_Track,
				//			CZ_DatVyr_Delka: sr.CZ_DatVyr_Delka,
				//			CZ_SerNum_Track: sr.CZ_SerNum_Track,
				//			CZ_SerNum_Delka: sr.CZ_SerNum_Delka,
				//			CZ_SW_Track: sr.CZ_SW_Track,
				//			CZ_SW_Delka: sr.CZ_SW_Delka,
				//			CZ_Doslo: sr.CZ_Doslo,
				//			DEX_ROW_ID: sr.DEX_ROW_ID,
				//			Note: sr.IsNoteNull() ? "" : sr.Note.Trim(),
				//			TYPEPAL: sr.IsTYPEPALNull() ? "" : sr.TYPEPAL.Trim(),
				//			QTYPAL: sr.IsQTYPALNull() ? (decimal?)null : sr.QTYPAL,
				//			PRINTED: 0,
				//			PRIORITY: (byte)(sr.IsPRIORITYNull() ? 3 : sr.PRIORITY),
				//			SKL_ID: sr.IsSKL_IDNull() ? "" : sr.SKL_ID.Trim(),
				//			MJ: sr.IsMJNull() ? string.Empty : sr.MJ.Trim(),
				//			CZ_REZ1_TRACK: sr.IsCZ_REZ1_TRACKNull() ? Convert.ToByte(0) : sr.CZ_REZ1_TRACK,
				//			CZ_REZ2_TRACK: sr.IsCZ_REZ2_TRACKNull() ? Convert.ToByte(0) : sr.CZ_REZ2_TRACK,
				//			ITEMCODE: sr.IsITEMCODENull() ? string.Empty : sr.ITEMCODE,
				//			WEIGHT: sr.IsWEIGHTNull() ? 0 : sr.WEIGHT
				//			);
				//	}

				//	seTransakce.Commit();
				//}
				//catch (Exception ex)
				//{
				//	Logging.ExceptionHandler2.Handle(ex);
				//}
				//finally
				//{
				//	if (seta != null)
				//	{
				//		if ((seta.Connection.State & ConnectionState.Open) == ConnectionState.Open)
				//			seta.Connection.Close();
				//		seta.Dispose();
				//	}
				//} 

				#endregion

				#region Insert to SI

				//SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SITableAdapter sita = new Fask.MST_W_Server.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SITableAdapter();
				//sita.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + dstFile);


				//try
				//{
				//	sita.Connection.Open();
				//	var siTransakce = sita.Connection.BeginTransaction();

				//	//Fask.MST_W_Server.SQLiteDBs.DataSets.Vydej.CZMST_SIDataTable sidt = new SQLiteDBs.DataSets.Vydej.CZMST_SIDataTable();
				//	//sidt.BeginLoadData();
				//	//foreach (Fask.DataSets.Vydej.CZMST_SIRow sr in vydejka.CZMST_SI)
				//	//{
				//	//	sr.AcceptChanges();
				//	//	sr.SetAdded();
				//	//	sidt.ImportRow(sr);
				//	//}
				//	//sidt.EndLoadData();
				//	//sita.Update(sidt);

				//	foreach (Fask.DataSets.Vydej.CZMST_SIRow sr in vydejka.CZMST_SI)
				//	{
				//		int rowinserted = sita.Insert(
				//					CountEntries: sr.CountEntries,
				//					SOPNUMBE: sr.SOPNUMBE.Trim(),
				//					ITEMNMBR: sr.IsITEMNMBRNull() ? string.Empty : sr.ITEMNMBR.Trim(),
				//					ORD: sr.ORD,
				//					VNDDOCNM: sr.IsVNDDOCNMNull() ? string.Empty : sr.VNDDOCNM.Trim(),
				//					VNDITNUM: sr.IsVNDITNUMNull() ? string.Empty : sr.VNDITNUM.Trim(),
				//					CZ_CarKod: sr.IsCZ_CarKodNull() ? string.Empty : sr.CZ_CarKod.Trim(),
				//					LOCNCODE: sr.IsLOCNCODENull() ? string.Empty : sr.LOCNCODE.Trim(),
				//					QTYSHPPD: sr.QTYSHPPD,
				//					QTYPACK: sr.QTYPACK,
				//					SERLTNUM: sr.SERLTNUM.Trim(),
				//					KOD_SW: sr.IsKOD_SWNull() ? string.Empty : sr.KOD_SW.Trim(),
				//					DAT_VYROBY: sr.IsDAT_VYROBYNull() ? string.Empty : sr.DAT_VYROBY.Trim(),
				//					REZ_1: sr.IsREZ_1Null() ? string.Empty : sr.REZ_1.Trim(),
				//					ODBER_ID: sr.IsODBER_IDNull() ? string.Empty : sr.ODBER_ID.Trim(),
				//					DATEDONE: sr.IsDATEDONENull() ? string.Empty : sr.DATEDONE.Trim(),
				//					TIMEDONE: sr.IsTIMEDONENull() ? string.Empty : sr.TIMEDONE.Trim(),
				//					USER_ID: sr.USER_ID,
				//					guid: sr.GUID,
				//					TYPEPAL: sr.IsTYPEPALNull() ? null : sr.TYPEPAL.Trim(),
				//					NMBRPAL: sr.IsNMBRPALNull() ? null : sr.NMBRPAL.Trim(),
				//					PRINTED: sr.IsPRINTEDNull() ? false : sr.PRINTED,
				//					REZ_2: sr.IsREZ_2Null() ? string.Empty : sr.REZ_2.Trim(),
				//					INPUT_MODE: sr.INPUT_MODE,
				//					ID_TERMINAL: sr.ID_TERMINAL,
				//					SKL_ID: sr.IsSKL_IDNull() ? string.Empty : sr.SKL_ID.Trim(),
				//					MJ: sr.IsMJNull() ? string.Empty : sr.MJ,
				//					QTYSHPPDMJ: sr.QTYSHPPDMJ,
				//					ITEMCODE : sr.IsITEMCODENull() ? string.Empty : sr.ITEMCODE,
				//					WEIGHT: sr.IsWEIGHTNull() ? 0 : sr.WEIGHT
				//					);
				//	}

				//	siTransakce.Commit();
				//}
				//catch (Exception ex)
				//{
				//	Logging.ExceptionHandler2.Handle(ex);
				//}
				//finally
				//{
				//	if (sita != null)
				//	{
				//		if ((sita.Connection.State & ConnectionState.Open) == ConnectionState.Open)
				//			sita.Connection.Close();
				//		sita.Dispose();
				//	}
				//} 
				#endregion

				#region Insert to SE SN

				//SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SE_SNTableAdapter sesnta = new Fask.MST_W_Server.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SE_SNTableAdapter();
				//sesnta.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + dstFile);

				//try
				//{
				//	sesnta.Connection.Open();
				//	var seSNTransakce = sesnta.Connection.BeginTransaction();


				//	foreach (Fask.DataSets.Vydej.CZMST_SE_SNRow sr in vydejka.CZMST_SE_SN)
				//	{
				//		int rowinserted = sesnta.Insert(
				//			CountEntries: sr.CountEntries,
				//			ITEMNMBR: sr.IsITEMNMBRNull() ? string.Empty : sr.ITEMNMBR.Trim(),
				//			SERLNMBR: sr.SERLNMBR.Trim(),
				//			QTY: sr.QTY,
				//			DEX_ROW_ID: sr.DEX_ROW_ID,
				//			SOPNUMBE: sr.IsSOPNUMBENull() ? string.Empty : sr.SOPNUMBE.Trim(),
				//			ORD: sr.ORD
				//			);
				//	}

				//	seSNTransakce.Commit();
				//}
				//catch (Exception ex)
				//{
				//	Logging.ExceptionHandler2.Handle(ex);
				//}
				//finally
				//{
				//	if (sesnta != null)
				//	{
				//		if ((sesnta.Connection.State & ConnectionState.Open) == ConnectionState.Open)
				//			sesnta.Connection.Close();
				//		sesnta.Dispose();
				//	}
				//} 
				#endregion

				#region Insert to Parametry

				//SQLiteDBs.DataSets.VydejTableAdapters.ParametryTableAdapter pta = new Fask.MST_W_Server.SQLiteDBs.DataSets.VydejTableAdapters.ParametryTableAdapter();
				//pta.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + dstFile);

				//try
				//{
				//	pta.Connection.Open();
				//	var PTransakce = pta.Connection.BeginTransaction();

				//	DataRow drow0 = (DataRow)vydejka.Parametry[0];
				//	drow0.SetAdded();
				//	pta.Update(drow0);

				//	PTransakce.Commit();
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

				#endregion

				try
				{

					using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Vydej ConVyd = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Vydej(dstFile))
					{

						foreach (var item in vydejka.CZMST_SE)
						{
							item.SetAdded();
						}

						foreach (var item in vydejka.CZMST_SI)
						{
							item.SetAdded();
						}

						foreach (var item in vydejka.CZMST_SE_SN)
						{
							item.SetAdded();
						}

						ConVyd.Update_SE(vydejka.CZMST_SE);
						ConVyd.Update_SI(vydejka.CZMST_SI);
						ConVyd.Update_SE_SN(vydejka.CZMST_SE_SN);

						DataRow drow0 = (DataRow)vydejka.Parametry[0];
						drow0.SetAdded();
						ConVyd.Update_Param(drow0);

					}

				}
				catch (System.Exception ex)
				{
					Fask.Logging.ExceptionHandler2.Handle(ex);
					throw ex;
				}

                Fask.Compressing.Zip.Compress(dstFile);

                return true;

            }
            catch (Exception ex)
            {
				Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				#region trace
				Trac.Write(ex, tracid);
				#endregion
                throw ex;
            }
            finally
            {
                #region trace
                Trac.Write("End", tracid);
                #endregion
            }
		}

		/// <summary>
		/// Metoda která potvrzuje prevzeti davky vydejky 
		/// </summary>
		/// <param name="countentries">èíslo dávky</param>
		/// <param name="idterminal">ID terminálu</param>
		/// <param name="itemtype">typ položky</param>
		/// <returns>True - uspešne provedeno, false - nenstave rovno hodí exception</returns>
		[WebMethod(Description = "Metoda která potvrzuje prevzeti davky vydejky")]
        public bool GetVydejkaReceived(int countentries, byte idterminal, string itemtype)
        {

			#region trace
			TracId tracid = new TracId(null, idterminal, countentries, "GetVydejkaReceived");
			#endregion

            try
            {
                #region trace
                Trac.Write("Start", tracid);
                #endregion

                if ((provider != null) && (provider is Fask.Server.Interfaces.Vydej.IVydej))
                {
                    #region trace
                    Trac.Write("Provider Start", tracid);
                    #endregion

                    try
                    {
                        Fask.Server.Interfaces.Classes.Davka davka = new Fask.Server.Interfaces.Classes.Davka();
                        Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();
                        Fask.Server.Interfaces.Classes.Sklad sklad = new Fask.Server.Interfaces.Classes.Sklad();
                        Fask.Server.Interfaces.Classes.Item item = new Fask.Server.Interfaces.Classes.Item();

                        davka.ID = countentries;
                        terminal.ID = idterminal;
                        item.Type = itemtype;

                        return (provider as Fask.Server.Interfaces.Vydej.IVydej).Vydej_GetVydejkaReceived(
                            davka,
                            terminal,
                            sklad,
                            item
                            );
                    }
                    catch (Exception ex)
                    {
						Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
						#region trace
						Trac.Write(ex, tracid); 
						#endregion
                        throw ex;
                    }
                    finally
                    {
                        #region trace
                        Trac.Write("Provider End", tracid);
                        #endregion
                    }
                }

            }
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				#region trace
				Trac.Write(ex, tracid); 
				#endregion
				throw ex;
			}
            finally
            {
                #region trace
                Trac.Write("End", tracid);
                #endregion
            }

			string msg = "Provider v Vydej.asmx > 'GetVydejkaReceived(int countentries, byte idterminal, string itemtype)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
			#region trace
			Trac.Write(msg, tracid); 
			#endregion
			throw new Exception(msg);

		}

		/// <summary>
		/// Metoda sloužící pro rozbalení ZIP souboru, a vytažení dat z souboru do pameti. Nasledne zavolá metodu ProcessVydejka která zpracuje data.
		/// </summary>
		/// <param name="countentries">èíslo dávky</param>
		/// <param name="idterminal">ID Terminálu</param>
		/// <param name="processVydejState">Enum, pøíznak jak se maji data zpracovat</param>
		/// <returns>StatusObjekt - informace o zpracování</returns>
		[WebMethod(Description = "Metoda sloužící pro rozbalení ZIP souboru, a vytažení dat z souboru do pameti. Nasledne zavolá metodu ProcessVydejka která zpracuje data.")]
		public StatusObject ProcessVydejkaDBFile2(int countentries, byte idterminal, ProcessVydejState processVydejState)
		{
			TracId tracid = new TracId(null, idterminal, countentries, "ProcessVydejkaDBFile2");
			StatusObject so = new StatusObject();

			try
			{
				#region trace
				Trac.Write("Start, ProcessVydejState: " + processVydejState.ToString(), tracid);
				#endregion

				if (!isLicenseValid())
				{
					so.StatusText = "Licence na serveru není validní!";
					so.Exception = true;
					return so;
				}

				string dstFile = Path.Combine(Fask.MyPath.Path.SQLiteDBsDirectory, idterminal.ToString() + @"\" + countentries.ToString() + VydejDBFileExtension);
				string dstFileZip = dstFile + Common.ZIP;

				//odzipovat
				Fask.Compressing.Zip.Decompress(dstFileZip);

				try
				{
					Fask.DataSets.Vydej vydejData = new Fask.DataSets.Vydej();
					Fask.SQLiteDBs.DataSets.Vydej vydejDataCE = new Fask.SQLiteDBs.DataSets.Vydej();

					//SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SETableAdapter seta = null;
					//SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SITableAdapter sita = null;
					//SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SIHTableAdapter sihta = null;
					//SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SEHTableAdapter sehta = null;
					//SQLiteDBs.DataSets.VydejTableAdapters.ParametryTableAdapter paramsta = null;
					
					//System.Data.SQLite.SQLiteConnection sQLiteConnection = null;
					try
					{
						//sQLiteConnection = new System.Data.SQLite.SQLiteConnection("Data source=" + dstFile);

						//seta = new Fask.MST_W_Server.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SETableAdapter();
						//sita = new Fask.MST_W_Server.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SITableAdapter();
						//sihta = new Fask.MST_W_Server.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SIHTableAdapter();
						//sehta = new Fask.MST_W_Server.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SEHTableAdapter();
						//paramsta = new Fask.MST_W_Server.SQLiteDBs.DataSets.VydejTableAdapters.ParametryTableAdapter();

						//seta.Connection = sQLiteConnection;
						//sita.Connection = seta.Connection;
						//sihta.Connection = seta.Connection;
						//sehta.Connection = seta.Connection;
						//paramsta.Connection = seta.Connection;

						//sQLiteConnection.Open();

						using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Vydej ConVyd = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Vydej(dstFile))
						{
							ConVyd.Fill_SE(vydejDataCE.CZMST_SE);
							ConVyd.Fill_SI(vydejDataCE.CZMST_SI);
							ConVyd.Fill_SI_BV(vydejDataCE.CZMST_SI_BV);
							ConVyd.Fill_SIH(vydejDataCE.CZMST_SIH);
							ConVyd.Fill_SEH(vydejDataCE.CZMST_SEH);
							ConVyd.Fill_Param(vydejDataCE.Parametry); 
						}

						//sQLiteConnection.Close();
						//sQLiteConnection = null;

					}
					catch (Exception ex)
					{
						Logging.ExceptionHandler2.Handle(ex);
						throw ex;
					}
					finally
					{
						//if (seta != null)
						//{
						//	if ((seta.Connection.State & ConnectionState.Open) == ConnectionState.Open)
						//		seta.Connection.Close();
						//	seta.Dispose();
						//}

						//if (sita != null)
						//{
						//	if ((sita.Connection.State & ConnectionState.Open) == ConnectionState.Open)
						//		sita.Connection.Close();
						//	sita.Dispose();
						//}

						//if (sihta != null)
						//{
						//	if ((sihta.Connection.State & ConnectionState.Open) == ConnectionState.Open)
						//		sihta.Connection.Close();
						//	sihta.Dispose();
						//}

						//if (sehta != null)
						//{
						//	if ((sehta.Connection.State & ConnectionState.Open) == ConnectionState.Open)
						//		sehta.Connection.Close();
						//	sehta.Dispose();
						//}

						//if (paramsta != null)
						//{
						//	if ((paramsta.Connection.State & ConnectionState.Open) == ConnectionState.Open)
						//		paramsta.Connection.Close();
						//	paramsta.Dispose();
						//}

						//if (sQLiteConnection != null)
						//{
						//	if ((sQLiteConnection.State & ConnectionState.Open) == ConnectionState.Open)
						//		sQLiteConnection.Close();
						//	sQLiteConnection.Dispose();
						//	sQLiteConnection = null;
						//}
					}


					#region trace
					Trac.Write(vydejDataCE, "ProcessVydejkaDBFile2, vydejDataCE, fill CZMST_SE, CZMST_SI, CZMST_SIH, CZMST_SEH - vydejDataCE", tracid);
					#endregion

					vydejData.CZMST_SE.BeginLoadData();
					foreach (Fask.SQLiteDBs.DataSets.Vydej.CZMST_SERow ser in vydejDataCE.CZMST_SE)
					{
						vydejData.CZMST_SE.AddCZMST_SERow(
							ser.CountEntries,
							ser.SOPNUMBE,
							ser.ITEMNMBR,
							ser.ITEMDESC,
							ser.VNDDOCNM,
							ser.IsVNDITNUMNull() ? null : ser.VNDITNUM,
							ser.ORD,
							ser.CZ_CarKod,
							ser.IsSKL_IDNull() ? string.Empty : ser.SKL_ID,
							ser.LOCNCODE,
							ser.QTYSHPPD,
							ser.QTYPACK,
							ser.CZ_DatVyr_Track,
							ser.CZ_DatVyr_Delka,
							ser.CZ_SerNum_Track,
							ser.CZ_SerNum_Delka,
							ser.CZ_SW_Track,
							ser.CZ_SW_Delka,
							ser.CZ_Doslo,
							ser.DEX_ROW_ID,
							0, 0,
							ser.ITEMTYPE,
							ser.IsTYPEPALNull() ? string.Empty : ser.TYPEPAL,
							ser.IsQTYPALNull() ? 0 : ser.QTYPAL,
							ser.PRIORITY, //ser.IsPRIORITYNull() ? 3 : ser.PRIORITY,
							ser.Note, //ser.IsNoteNull() ? string.Empty : ser.Note
							ser.IsMJNull() ? string.Empty : ser.MJ,
							ser.IsCZ_REZ1_TRACKNull() ? Convert.ToByte(0) : ser.CZ_REZ1_TRACK,
							ser.IsCZ_REZ2_TRACKNull() ? Convert.ToByte(0) : ser.CZ_REZ2_TRACK,
							ser.IsITEMCODENull() ? string.Empty : ser.ITEMCODE.Trim(),
							ser.IsWEIGHTNull() ? 0 : ser.WEIGHT,
							ser.CZ_Expirace_Track
							);
					}
					vydejData.CZMST_SE.EndLoadData();


					#region trace
					Trac.Write(vydejData.CZMST_SE, "ProcessVydejkaDBFile2, vydejData.setModified CZMST_SE", tracid);
					#endregion

					vydejData.CZMST_SI.BeginLoadData();
					foreach (Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIRow sir in vydejDataCE.CZMST_SI)
					{
						vydejData.CZMST_SI.ImportRow(sir);

						//vydejData.CZMST_SI.AddCZMST_SIRow(
						//	sir.CountEntries,
						//	sir.SOPNUMBE,
						//	sir.ITEMNMBR,
						//	sir.ORD,
						//	sir.VNDDOCNM,
						//	sir.VNDITNUM,
						//	sir.CZ_CarKod,
						//	sir.IsSKL_IDNull() ? string.Empty : sir.SKL_ID,
						//	sir.LOCNCODE,
						//	sir.QTYSHPPD,
						//	sir.QTYPACK, sir.SERLTNUM, sir.KOD_SW, sir.DAT_VYROBY, sir.REZ_1,
						//	sir.ODBER_ID, sir.DATEDONE, sir.TIMEDONE, sir.USER_ID, sir.DEX_ROW_ID,
						//	sir.IsTYPEPALNull() ? string.Empty : sir.TYPEPAL,
						//	sir.IsNMBRPALNull() ? string.Empty : sir.NMBRPAL,
						//	sir.PRINTED, //sir.IsPRINTEDNull() ? false : sir.PRINTED,
						//	sir.guid,
						//	sir.REZ_2, sir.INPUT_MODE, sir.ID_TERMINAL,
						//	sir.IsMJNull() ? string.Empty : sir.MJ,
						//	sir.QTYSHPPDMJ,
						//	sir.IsITEMCODENull() ? string.Empty : sir.ITEMCODE,
						//	sir.IsWEIGHTNull() ? 0 : sir.WEIGHT,
						//	sir.IsExpiraceNull() ? DateTime.Now : sir.Expirace
						//	);
					}
					vydejData.CZMST_SI.EndLoadData();

					#region trace
					Trac.Write(vydejData.CZMST_SI, "ProcessVydejkaDBFile2, vydejData.fill CZMST_SI", tracid);
					#endregion

					vydejData.CZMST_SI_BV.BeginLoadData();
					foreach (Fask.SQLiteDBs.DataSets.Vydej.CZMST_SI_BVRow sibv in vydejDataCE.CZMST_SI_BV)
					{
						vydejData.CZMST_SI_BV.ImportRow(sibv);
					}

					vydejData.CZMST_SI_BV.EndLoadData();

					#region trace
					Trac.Write(vydejData.CZMST_SI_BV, "ProcessVydejkaDBFile2, vydejData.fill CZMST_SI_BV", tracid);
					#endregion


					vydejData.CZMST_SIH.BeginLoadData();

					foreach (Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIHRow sihr in vydejDataCE.CZMST_SIH)
					{
						var newRow = vydejData.CZMST_SIH.NewCZMST_SIHRow();

						newRow.CountEntries = sihr.CountEntries;
						newRow.TISKARNA_NAME = sihr.IsTISKARNA_NAMENull() ? string.Empty : sihr.TISKARNA_NAME;
						newRow.PRAC_ID = sihr.IsPRAC_IDNull() ? string.Empty : sihr.PRAC_ID;

						if (!sihr.IsISOKNull())
							newRow.ISOK = sihr.ISOK;

						if (!sihr.IsstatusNull())
							newRow.status = sihr.status;

						vydejData.CZMST_SIH.AddCZMST_SIHRow(newRow);
					}

					vydejData.CZMST_SIH.EndLoadData();


					//MaR zakomentoval 2.7.2025
					//vydejData.CZMST_SIH.BeginLoadData();
					//foreach (Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIHRow sihr in vydejDataCE.CZMST_SIH)
					//{
					//	vydejData.CZMST_SIH.AddCZMST_SIHRow(
					//		sihr.CountEntries,
					//		sihr.IsTISKARNA_NAMENull() ? string.Empty : sihr.TISKARNA_NAME,
					//		sihr.IsPRAC_IDNull() ? string.Empty : sihr.PRAC_ID
					//		);
					//}
					//vydejData.CZMST_SIH.EndLoadData();

					#region trace
					Trac.Write(vydejData.CZMST_SIH, "ProcessVydejkaDBFile2, vydejData.fill CZMST_SIH", tracid);
					#endregion

					vydejData.CZMST_SEH.BeginLoadData();
					foreach (Fask.SQLiteDBs.DataSets.Vydej.CZMST_SEHRow sihr in vydejDataCE.CZMST_SEH)
					{
						vydejData.CZMST_SEH.AddCZMST_SEHRow(
							sihr.CountEntries,
							sihr.GUID
							);
					}
					vydejData.CZMST_SEH.EndLoadData();

					#region trace
					Trac.Write(vydejData.CZMST_SEH, "ProcessVydejkaDBFile2, vydejData.fill CZMST_SEH", tracid);
					#endregion

					vydejData.Parametry.BeginLoadData();
					foreach (Fask.SQLiteDBs.DataSets.Vydej.ParametryRow prow in vydejDataCE.Parametry)
					{
						vydejData.Parametry.AddParametryRow(
							prow.ENABLE_LISTSNIM,
							prow.CONFIG_LISTSNIM,
							prow.CONFIG_POKRDOHLED,
							prow.CONFIG_PTATSE_NEANO,
							prow.CONFIG_KONT_UPL_POL,
							prow.CONFIG_ZADAT_MN_POKAZDE,
							prow.ENABLE_DOHLED_ODB,
							prow.CONFIG_DOHLED_ODB,
							prow.CONFIG_KONT_DOKONCENOSTI,
							prow.CONFIG_KONT_DELKA,
							prow.CONFIG_KONT_SN_CARKOD,
							prow.CONFIG_DUPLIC_SN,
							prow.CONFIG_NOVA_KARTA,
							prow.CONFIG_SKRYT_MNOZSTVI,
							prow.CONFIG_SNIMEJ_PRI_LISTU,
							prow.CONFIG_KONT_NUL_DELKA,
							prow.CONFIG_SNIM_LOCNCODE,
							prow.CONFIG_MNOZSTVI_PREDVYPLNIT,
							prow.CONFIG_MNOZSTVI_PREDVYPLNIT_JEDNA,
							prow.CONFIG_MNOZSTVI_PREDVYPLNIT_ZBYVAJICI,
							prow.CONFIG_MNOZSTVI_ZADAVAT,
							prow.IsCONFIG_MNOZSTVI_SCANNEREMNull() ? false : prow.CONFIG_MNOZSTVI_SCANNEREM,
							prow.IsCONFIG_KONT_PREDLOHA_SNNull() ? false : prow.CONFIG_KONT_PREDLOHA_SN,
							prow.IsCONFIG_LOCNCODE_OVERIT_SCANEREMNull() ? false : prow.CONFIG_LOCNCODE_OVERIT_SCANEREM,
							prow.CONFIG_POUZIT_CISELNIK_ZBOZI.ToString(),
							prow.IsCONFIG_NAZEV_SLOUPCE_K_OVERENI_V_CISELNIKUNull() ? null : prow.CONFIG_NAZEV_SLOUPCE_K_OVERENI_V_CISELNIKU,
							prow.IsCONFIG_NAZEV_SLOUPCE_K_NAVRACENI_V_CISELNIKUNull() ? null : prow.CONFIG_NAZEV_SLOUPCE_K_NAVRACENI_V_CISELNIKU,
							prow.IsCONFIG_LOKACE_POVOLITNull() ? false : prow.CONFIG_LOKACE_POVOLIT,
							prow.IsCONFIG_LOKACE_TIMEOUTNull() ? 0 : prow.CONFIG_LOKACE_TIMEOUT
							);
					}
					vydejData.Parametry.EndLoadData();

					vydejData.AcceptChanges();

					//foreach (Fask.DataSets.Vydej.CZMST_SERow ser in vydejData.CZMST_SE)
					//{
					//	ser.SetModified();
					//}
					vydejData.CZMST_SE.ToList().ForEach(x => x.SetModified());
					//vydejData.CZMST_SEH.ToList().ForEach(x => x.set);
					//vydejData.CZMST_SE_SN.ToList().ForEach(x => x.SetModified());
					//vydejData.CZMST_SI.ToList().ForEach(x => x.SetModified());
					//vydejData.CZMST_SIH.ToList().ForEach(x => x.SetModified());
					//vydejData.CZMST_SI_RFID.ToList().ForEach(x => x.SetModified());

					so = ProcessVydejka(countentries, idterminal, vydejData, processVydejState);

					if (so.StatusText == "OK" && !so.Exception)
					{
						Routines.ManageDataFiles.Move(dstFile, Path.Combine(Fask.MyPath.Path.ProcessedDataFileDirectory, Path.GetFileName(dstFile)));
					}
					else
					{
						Routines.ManageDataFiles.Move(dstFile, Path.Combine(Fask.MyPath.Path.ErrorDataFileDirectory, Path.GetFileName(dstFile)));
					}

					return so;

				}
				catch (Exception ex)
				{
					Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
					#region trace
					Trac.Write(ex, tracid);
					#endregion
					Routines.ManageDataFiles.Move(dstFile, Path.Combine(Fask.MyPath.Path.ErrorDataFileDirectory, Path.GetFileName(dstFile)));
					throw ex;
				}
				finally
				{
					try
					{
						File.Delete(dstFile);
					}
					catch (Exception ex)
					{
						Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
						throw ex;
					}
				}
			}
			finally
			{
				#region trace
				Trac.Write("End", tracid);
				#endregion
			}
		}

		#region Detaily pouze cez SQL

		/// <summary>
		/// Metoda slouží pro custom volaní bud Procesury anebo select podle nastaveni 
		/// </summary>
		/// <param name="sopnumber">Parametr, predavany do dotazu, SOPNUMBE(èíslo objednavky)</param>
		/// <returns>Netypový dataset který vrací naètená data</returns>
		[WebMethod(Description = "Metoda slouží pro custom volaní bud Procesury anebo select podle nastaveni ")]
		public DataSet Detail(string sopnumber)
		{
            try
            {

                if ((provider != null) && (provider is Fask.Server.Interfaces.Vydej.IVydej))
                {
                    Fask.Server.Interfaces.Classes.Objednavka item = new Objednavka();
                    item.ID = sopnumber;

                    return (provider as Fask.Server.Interfaces.Vydej.IVydej).Vydej_Detail(item);
                }
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }

            string msg = "Provider v Vydej.asmx > 'Detail(string sopnumber)' nenastaven.";
            Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
            throw new Exception(msg);
        }

		/// <summary>
		/// Metoda slouží pro custom volaní bud Procesury anebo select podle nastaveni 
		/// </summary>
		/// <param name="countentries">Parametr, predavany do dotazu, CountEntries(èíslo dávky)</param>
		/// <returns>Netypový dataset který vrací naètená data</returns>
		[WebMethod(Description = "Metoda slouží pro custom volaní bud Procesury anebo select podle nastaveni ")]
		public DataSet DetailDavka(string countentries)
		{

            try
            {

                if ((provider != null) && (provider is Fask.Server.Interfaces.Vydej.IVydej))
                {
                    Fask.Server.Interfaces.Classes.Davka item = new Davka();
                    item.ID = int.Parse(countentries);

                    return (provider as Fask.Server.Interfaces.Vydej.IVydej).Vydej_DetailDavka(item);
                }
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }

            string msg = "Provider v Vydej.asmx > 'DetailDavka(string countentries)' nenastaven.";
            Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
            throw new Exception(msg);
        }

        /// <summary>
        /// Metoda slouží pro custom volaní bud Procesury anebo select podle nastaveni 
        /// </summary>
        /// <param name="itemnmbr">Parametr, predavany do dotazu, ITEMNMBR(èislo položky)</param>
        /// <returns>Netypový dataset který vrací naètená data</returns>
        [WebMethod(Description = "Metoda slouží pro custom volaní bud Procesury anebo select podle nastaveni ")]
        public DataSet DetailPolozka(string itemnmbr)
        {
            try
            {

                if ((provider != null) && (provider is Fask.Server.Interfaces.Vydej.IVydej))
                {
                    Fask.Server.Interfaces.Classes.Item item = new Item();
                    item.ID = itemnmbr;

                    return (provider as Fask.Server.Interfaces.Vydej.IVydej).Vydej_DetailPolozka(item);
                }
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }

            string msg = "Provider v Vydej.asmx > 'DetailPolozka(string itemnmbr)' nenastaven.";
            Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
            throw new Exception(msg);
        }

		#endregion

		#region Palety

		/// <summary>
		/// Metoda sloužící pro vložení palety do tabulky CZMST_Palety
		/// </summary>
		/// <param name="name">Jmeno palety</param>
		/// <param name="id">ID palety</param>
		/// <returns></returns>
		[WebMethod(Description = "Metoda sloužící pro vložení palety do tabulky CZMST_Palety")]
		public bool PridatPaletu(string name, string id)
		{
			bool succed = false;
			System.Data.SqlClient.SqlCommand xcomm = null;

			TracId tracid = new TracId(null, null, null, "Vydej.PridatPaletu: " + name + ":" + id);

			try
			{
                Konfigurace.Classes.Globals_Konfig_WebConfig.LoadConfiguration();

                #region trace
                Trac.Write("Start", tracid);
				#endregion

				xcomm = new System.Data.SqlClient.SqlCommand(
						"Insert into CZMST_Palety (ID, Name) " +
						"values ('" + id + "', '" + name + "')",
						new SqlConnection(Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.ConnectionString[0].FASKDB)
						);

				xcomm.Connection.Open();
				xcomm.ExecuteNonQuery();
				xcomm.Connection.Close();
				succed = true;
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, " (PridatPaletu:" + name + ")");
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				#region trace
				Trac.Write(ex, tracid);
				#endregion
				throw ex;
			}
			finally
			{
				#region trace
				Trac.Write("End", tracid);
				#endregion
			}

			return succed;
		}

		/// <summary>
		/// Metoda slouží pro smazaní palety podle Jmena
		/// </summary>
		/// <param name="name">Jmeno palety která bude smazana</param>
		/// <returns>True - usmešne smazano, False - nenastave, rovno hodi exception</returns>
		[WebMethod(Description = "Metoda slouží pro smazaní palety podle Jmena")]
		public bool SmazatPaletu(string name)
		{
			bool succed = false;
			SqlCommand xcomm = null;

			TracId tracid = new TracId(null, null, null, "Vydej.SmazatPaletu: " + name);

			try
			{
                Konfigurace.Classes.Globals_Konfig_WebConfig.LoadConfiguration();

                #region trace
                Trac.Write("Start", tracid);
				#endregion

				xcomm = new SqlCommand(
					"Delete from CZMST_Palety " +
					" where Name='" + name + "'",
					new SqlConnection(Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.ConnectionString[0].FASKDB)
					);

				xcomm.Connection.Open();
				xcomm.ExecuteNonQuery();
				xcomm.Connection.Close();
				succed = true;
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, " (SmazatPaletu:" + name + ")");
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				#region trace
				Trac.Write(ex, tracid);
				#endregion
				throw ex;
			}
			finally
			{
				#region trace
				Trac.Write("End", tracid);
				#endregion
			}

			return succed;
		}

		
		#endregion

		/// <summary>
		/// Metoda sloužící pro vraceni Vygenerovaneho SSCC Kodu
		/// </summary>
		/// <param name="seq_id">ID sequence</param>
		/// <param name="count">skok o kolik se zvedne Count</param>
		/// <param name="terminalID">ID Terminalu</param>
		/// <returns>Vygenerovany SSCC Kod</returns>
		[WebMethod(Description = "Metoda sloužící pro vraceni Vygenerovaneho SSCC Kodu")]
		public string Terminal_GetSSCCCode(int seq_id, int count, int terminalID)
		{
			try
			{
				return this.CZMST_SSCC_SEQUENCE_INC_proc(seq_id, count, terminalID);
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "Terminal_GetSSCCCode IDSequence:"+ seq_id.ToString());
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				throw ex;
			}
		}

        #region online funkce
        /// <summary>
        /// Metoda pro Storno výdejky
        /// </summary>
        /// <param name="idterminal">ID Terminálu</param>
        /// <param name="idCountEntries">èíslo dávky</param>
        /// <param name="password">Heslo pro storno dávky</param>
        /// <returns>StatusObjekt - informace o zpracování</returns>
        [WebMethod(Description = "Metoda pro Storno výdejky")]
        public StatusObject StornoVydejka(byte idterminal, string idCountEntries, string password)
        {
            TracId tracid = new TracId(null, idterminal, null, "Vydejka.StornoVydejka:" + (idCountEntries ?? string.Empty));
            StatusObject processStatus = new StatusObject();

            try
            {
                #region trace
                Trac.Write("Start", tracid);
                #endregion

                if ((provider != null) && (provider is Fask.Server.Interfaces.Vydej.IVydej))
                {
                    try
                    {
                        Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();
                        Fask.Server.Interfaces.Classes.Davka davka = new Davka();

                        terminal.ID = idterminal;
                        davka.ID = int.Parse(idCountEntries);

                        processStatus = (provider as Fask.Server.Interfaces.Vydej.IVydej).Vydej_Storno_Vydejka(
                            davka,
                            terminal,
                            password
                            );

                        return processStatus;
                    }
                    catch (Exception ex)
                    {
                        Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                        throw ex;
                    }
                }
            }
            finally
            {
                #region trace
                Trac.Write("End", tracid);
                #endregion
            }

            string msg = "Provider v Vydej.asmx > 'StornoVydejka(byte idterminal, string idCountEntries, string password)' nenastaven.";
            Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
            #region trace
            Trac.Write(msg, tracid);
            #endregion
            throw new Exception(msg);
        }


        /// <summary>
        /// Metoda, ktera vraci nalezene materialy a jejich lokace a dalsi atributy z lokacniho mechanismu
        /// </summary>
        /// <param name="itemnmbr">cislo polozky pro dohledani, pokud je zname, jinak prazdne nebo null</param>
        /// <param name="skl_id">cislo skladu, kde se ma hledat, nebo [null|empty] hledat ve vsech skladech</param>
        /// <param name="serltnum">sarze materialu, ktera se ma dohledat, pokud je [null|empty] hledat vsechny zaznamy pro itemnmbr, ktere ale nesmi byt prazdne</param>
        /// <returns>Fask.Server.Inerfaces.DataSets.Location.CZMST_SkladLokace_Lokace</returns>
        /// <remarks>Jedna se o kombinovanou metodu, ktera na zakladne vstupnich podminek vraci data dle techto podminek. Toto neni uplne vhodne</remarks>
        [WebMethod(Description = "Metoda, ktera vraci nalezene materialy a jejich lokace a dalsi atributy z lokacniho mechanismu")]
        public Fask.Server.Interfaces.DataSets.Vydej_Items_Online Online_GetMaterial(string itemnmbr, string skl_id, string serltnum)
        {
            try
            {
                if ((provider != null) && (provider is Fask.Server.Interfaces.Vydej.IVydej))
                {
                    return ((Fask.Server.Interfaces.Vydej.IVydej)provider).Vydej_Online_GetMaterial(itemnmbr, skl_id, serltnum);
                }
                else
                    throw new Exception("Provider není implementován");
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
        }

        /// <summary>
        /// Metoda, ktera vraci nalezene materialy a jejich lokace a dalsi atributy z lokacniho mechanismu
        /// </summary>
        /// <param name="itemnmbr">cislo polozky pro dohledani, pokud je zname, jinak prazdne nebo null</param>
        /// <param name="skl_id">cislo skladu, kde se ma hledat, nebo [null|empty] hledat ve vsech skladech</param>
        /// <param name="serltnum">sarze materialu, ktera se ma dohledat, pokud je [null|empty] hledat vsechny zaznamy pro itemnmbr, ktere ale nesmi byt prazdne</param>
        /// <param name="expirace">expirace materialu, ktera se ma dohledat, pokud je [null|empty] hledat vsechny zaznamy pro itemnmbr, ktere ale nesmi byt prazdne</param>
        /// <returns>Fask.Server.Inerfaces.Classes.StatusOverExpirace</returns>
        /// <remarks>Jedna se o kombinovanou metodu, ktera na zakladne vstupnich podminek vraci data dle techto podminek. Toto neni uplne vhodne</remarks>
        [WebMethod(Description = "Metoda, ktera vraci stav expirace, zda je mozne ji vydat")]
        public StatusOverExpirace Online_Expirace_Verify(string itemnmbr, string sklad_id, string serltnum, DateTime? expirace)
        {
            try
            {
                if ((provider != null) && (provider is Fask.Server.Interfaces.Vydej.IVydej))
                {
                    return ((Fask.Server.Interfaces.Vydej.IVydej)provider).Vydej_Online_Expirace_Verify(itemnmbr, sklad_id, serltnum, expirace);
                }
                else
                    throw new Exception("Provider není implementován");
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
        }

        [WebMethod(Description = "Metoda, ktera overuje heslo pro vydani expirace, ktera neni vhodna")]
        public bool Online_Expirace_Confirm(byte terminalID, string userID, string password)
        {
            try
            {
                if ((provider != null) && (provider is Fask.Server.Interfaces.Vydej.IVydej))
                {
                    return ((Fask.Server.Interfaces.Vydej.IVydej)provider).Vydej_Online_Expirace_Confirm(terminalID, userID, password);
                }
                else
                    throw new Exception("Provider není implementován");
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
        } 
        #endregion


        #region Generovani davky z terminalu pro Vydej


        // 19.4.2016 JiS
        // Uprava pro dlouhotrvajici operace.
        // 1) terminal vola GenerateDavkaRequest a vraci se mu StatusObject podle ktereho se dale ridi proces stahovani
        //      => tato metoda ridi stav stahovani pres StatusObject (rozsireno o prvek finished
        // 2) terminal nasledne v cyklu se dotazuje metodou GenerateDavkaStatus na StatuObject pro generovani teto davky
        //      => konci se bud vyjimkou SO.Exception = true ve SO.StatusText je text vyjimky
        //      => nebo SO.Finished = true ve SO.StatusText je ID nove generovane davky
        // Pozn: a) GenerateDavka je puvodni webova metoda, ktera je nyni neverejna (muze byt i privatni)
        //       b) GenerateDavkaWrapper je obalka pro volani puvodni metody GenerateDavka, 
        //          ktera zachycuje a zpracovava vyjimky puvodni metody pro StatusObject
        //       c) GenerateDavkaRequest vyvolava GenerateDavkaWrapper v novem vlakne
        //
        // Proces:
        //  terminal <-> GenerateDavkaRequest -> new Thread(GenerateDavkaWrapper) -> GenerateDavka
        //  terminal <-> GenerateDavkaStatus

        /// <summary>
        /// Metoda která zapoène pøipravu generovaní pøedlohy IS do Našich struktur
        /// </summary>
        /// <param name="sopnumbe">èíslo objednávky</param>
        /// <param name="skl_id">ID Skladu</param>
        /// <returns>StatusObjekt - informace o zpracování</returns>
        [WebMethod(Description = "Metoda která zapoène pøipravu generovaní pøedlohy IS do Našich struktur")]
		public StatusObject GenerateDavkaRequest(string sopnumbe, string skl_id)
		{
			StatusObject so = StatusObject.Create(MyPath.Path.StatusObjectDirectory, StatusObject.Operations.VydejGenerateDavka, sopnumbe, skl_id);
			if (so.Exists)
			{
				return so; //vrati informaci o stavu provedeni. Pokud se chce generovat znovu, tak se musi nejprve provest smazani statusu ... 
			}
			// kdyz neexistuje, tak je to novy pozadavek ...

			so.Finished = so.Exception = false;
			so.StatusText = "Pøíprava dávky dokladu '" + sopnumbe + "'";
			so.Write();

			// Vyvolani noveho threadu ...
			new System.Threading.Thread(() => GenerateDavkaWrapper(sopnumbe, skl_id, ref so)).Start();

			return so;
		}

		/// <summary>
		/// Metoda která provadí samotne generování dokladu z IS do našich struktur
		/// </summary>
		/// <param name="sopnumbe">èíslo objednávky</param>
		/// <param name="skl_id">ID Skladu</param>
		/// <returns> ID chyby </returns>
		[WebMethod(Description = "Metoda která provadí samotne generování dokladu z IS do našich struktur")]
		public int GenerateDavka(string sopnumbe, string skl_id)
		{
			TracId tracid = new TracId(null, null, null, "Vydej.GenerateDavka:" + (sopnumbe ?? string.Empty) + "," + (skl_id ?? string.Empty));
			try
			{
				#region trace
				Trac.Write("Start", tracid);
				#endregion

				#region sleep test
				//try
				//{
				//    int sleep = 1000; // 1s
				//    sleep *= 60; // 1min
				//    sleep *= 30; // 0,5h
				//    Debug.WriteLine("Sleep start:" + DateTime.Now.ToString());
				//    System.Threading.Thread.Sleep(sleep);
				//    Debug.WriteLine("Sleep stop:" + DateTime.Now.ToString());
				//}
				//catch (Exception ex)
				//{
				//    Log.writeErrorLog("Prijem generate davka sleep error:\n" + ex.Message);
				//}
				#endregion


				string statusinfo = string.Empty;

				try
				{
					if ((provider != null) && (provider is Fask.Server.Interfaces.Vydej.IVydej))
					{
						Objednavka objednavka = new Objednavka();
						Sklad sklad = new Sklad();

						objednavka.ID = sopnumbe;
						objednavka.CisloDavky = string.Empty;

						sklad.ID = skl_id;

						StatusInfo si = (provider as Fask.Server.Interfaces.Vydej.IVydej).Vydej_GenerateDavka(objednavka, sklad);
						return si.ID;
					}
				}
				catch (Exception ex)
				{
					Fask.Logging.ExceptionHandler2.Handle(LogLevel.Error, "Sopnumbe:'" + sopnumbe);
					Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

					throw ex;
				}

				string msg = "Provider v Vydej.asmx > 'GenerateDavka(string sopnumbe, string skl_id)' nenastaven.";
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
				#region trace
				Trac.Write(msg, tracid);
				#endregion
				throw new Exception(msg);

			}
			finally
			{
				#region trace
				Trac.Write("End", tracid);
				#endregion
			}
		}

		/// <summary>
		/// Metoda která vrací status o prubehu generování
		/// </summary>
		/// <param name="sopnumbe">èíslo objednávky</param>
		/// <param name="skl_id">ID Skladu</param>
		/// <returns>StatusObjekt - informace o zpracování</returns>
		[WebMethod(Description = "Metoda která vrací status o prubehu generování")]
		public StatusObject GenerateDavkaStatus(string sopnumbe, string skl_id)
		{
			StatusObject so = StatusObject.Create(MyPath.Path.StatusObjectDirectory, StatusObject.Operations.VydejGenerateDavka, sopnumbe, skl_id);
			if (so.Exists)
				return so;
			else
				return null;
		}

		/// <summary>
		/// Metoda která slouzi terminalu ke smazani statusu a znovu generovani prikazu(davky)
		/// </summary>
		/// <param name="sopnumbe">èíslo objednávky</param>
		/// <param name="skl_id">ID Skladu</param>
		/// <returns>StatusObjekt - informace o zpracování</returns>
		[WebMethod(Description = "Metoda která slouzi terminalu ke smazani statusu a znovu generovani prikazu(davky)")]
		public StatusObject GenerateDavkaStatusDelete(string sopnumbe, string skl_id)
		{
			StatusObject so = StatusObject.Create(MyPath.Path.StatusObjectDirectory, StatusObject.Operations.VydejGenerateDavka, sopnumbe, skl_id);
			so.Delete();
			return so;
		}

		#endregion

		#endregion

		#region Private metody

		/// <summary>
		/// Metoda pro kontrolu licence
		/// </summary>
		/// <returns>True - Licence je validni, False- Licence neni validni</returns>
		private bool isLicenseValid()
		{
			// \bug : resit nejakym lepsim zpusobem ... 
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

		/// <summary>
		/// Metoda sloužící pro zpracovaní, Uložení do SQL serveru a prace nad IS
		/// </summary>
		/// <param name="countentries">èíslo dávky</param>
		/// <param name="idterminal">ID Terminálu</param>
		/// <param name="vydejdata">Dataset Data ke zpracování</param>
		/// <param name="processVydejState">Enum, pøíznak jak se maji data zpracovat</param>
		/// <returns>StatusObjekt - informace o zpracování</returns>
		public StatusObject ProcessVydejka(int countentries, byte idterminal, Fask.DataSets.Vydej vydejdata, ProcessVydejState processVydejState)
		{
			TracId tracid = new TracId(null, idterminal, countentries, "ProcessVydejka");
			StatusObject processStatus = new StatusObject();

			try
			{
				#region trace
				Trac.Write("Start", tracid);
				#endregion

				string itemtype = string.Empty;

				if ((provider != null) && (provider is Fask.Server.Interfaces.Vydej.IVydej))
				{
					#region trace
					Trac.Write("Provider Start", tracid);
					#endregion

					Fask.Server.Interfaces.Classes.Davka davka = new Fask.Server.Interfaces.Classes.Davka();
					Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();
					Fask.Server.Interfaces.Classes.Sklad sklad = new Fask.Server.Interfaces.Classes.Sklad();
					Fask.Server.Interfaces.Classes.Item item = new Fask.Server.Interfaces.Classes.Item();

					davka.ID = vydejdata.CZMST_SE[0].CountEntries;
					terminal.ID = vydejdata.CZMST_SE[0].CZ_Doslo;
					item.Type = vydejdata.CZMST_SE[0].ITEMTYPE; //???

					Fask.Server.Interfaces.Vydej.ProcessState pState = (Fask.Server.Interfaces.Vydej.ProcessState)processVydejState;

					processStatus = (provider as Fask.Server.Interfaces.Vydej.IVydej).Vydej_Process(
						davka,
						terminal,
						sklad,
						item,
						vydejdata,
						pState,
						itemtype
						);

					return processStatus;
				}


			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				#region trace
				Trac.Write(ex, tracid);
				#endregion
				throw ex;
			}
			finally
			{
				#region trace
				Trac.Write("End", tracid);
				#endregion
			}

			string msg = "Provider v Vydej.asmx > 'ProcessVydejka(int countentries, byte idterminal, Fask.DataSets.Vydej vydejdata, ProcessVydejState processVydejState)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
			#region trace
			Trac.Write(msg, tracid);
			#endregion
			throw new Exception(msg);

		}



		/// <summary>
		/// Metoda slouží pro vracení sequence pro vygenerovaní SSCC kodu
		/// </summary>
		/// <param name="seq_id">ID Sequence z databaze</param>
		/// <param name="count">skok o kolik se zvedne Count</param>
		/// <param name="terminalID">ID Terminálu</param>
		/// <returns>Vracená sequence</returns>
		private string CZMST_SSCC_SEQUENCE_INC_proc(int seq_id, int count, int terminalID)
		{
            Konfigurace.Classes.Globals_Konfig_WebConfig.LoadConfiguration();
            // \bug Predelat nazev procedury a parametru do konfigurace??
            SqlConnection adpaconnection = new SqlConnection(Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.ConnectionString[0].FASKDB);
			SqlCommand adpacommand = new SqlCommand("CZMST_get_sscc_sequence_proc");
			adpacommand.CommandType = CommandType.StoredProcedure;

			adpacommand.Parameters.Add((new SqlParameter("@sequence", SqlDbType.Int)));
			adpacommand.Parameters.Add((new SqlParameter("@count", SqlDbType.Int)));
			adpacommand.Parameters.Add((new SqlParameter("@terminal", SqlDbType.Int)));
			adpacommand.Parameters.Add((new SqlParameter("@endSSCC", SqlDbType.NVarChar, 20)));

			((IDataParameter)adpacommand.Parameters["@sequence"]).Value = seq_id;
			((IDataParameter)adpacommand.Parameters["@count"]).Value = count;
			((IDataParameter)adpacommand.Parameters["@terminal"]).Value = terminalID;
			((IDataParameter)adpacommand.Parameters["@endSSCC"]).Direction = ParameterDirection.Output;

			adpacommand.Connection = adpaconnection;

			adpaconnection.Open();
			adpacommand.ExecuteNonQuery();

			int endSSCC = int.Parse(((IDataParameter)adpacommand.Parameters["@endSSCC"]).Value.ToString());

			adpacommand.CommandText = "SELECT dbo.CZMST_get_sscc_func(@seq_id,@sscc_count)";
			adpacommand.CommandType = CommandType.Text;
			adpacommand.Parameters.Clear();
			adpacommand.Parameters.Add((new SqlParameter("@seq_id", SqlDbType.Int)));
			adpacommand.Parameters.Add((new SqlParameter("@sscc_count", SqlDbType.Int)));
			((IDataParameter)adpacommand.Parameters["@seq_id"]).Value = seq_id;
			((IDataParameter)adpacommand.Parameters["@sscc_count"]).Value = endSSCC;

			object sscc = adpacommand.ExecuteScalar();

			adpaconnection.Close();

			return (string)sscc;
		}


		
		/// <summary>
		/// Metoda Wraper pro odchyceni vyjimky a ulozeni vysledku volani ...
		/// </summary>
		/// <param name="sopnumbe">èíslo objednávky</param>
		/// <param name="skl_id">ID Skladu</param>
		/// <param name="so">reference na StatusObject</param>
		private void GenerateDavkaWrapper(string sopnumbe, string skl_id, ref StatusObject so)
		{
			try
			{
				so.StatusText = "Generování dávky dokladu '" + sopnumbe + "'";
				so.Write();

				int result = GenerateDavka(sopnumbe, skl_id);

				so.StatusText = result.ToString();
				so.Finished = true;
				so.Write();

			}
			catch (Exception ex)
			{
				so.Exception = true;
				so.Finished = true;
				so.StatusText = ex.Message;
				so.Write();
			}
		}


		#endregion
    }
}
