using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;
using Fask.Tracing;
using System.Data.Common;
using System.IO;
using System.Configuration;
using System.Web.Configuration;
using System.Security.Cryptography;
using System.Web.Services.Protocols;
using System.Globalization;
using System.Reflection;
using Fask.Server.Interfaces.Classes;
using Fask.Logging;

using System.Data.SqlClient;
using Fask.MST_W_Server.Constants;
using Fask.MST_W_Server.SQLite_Classes;

using System.Linq;

namespace Fask.MST_W_Server
{
	/// <summary>
	/// Služba pøenosu dat inventury1
	/// </summary>
	[WebService(Namespace = "http://Inventura1.fask.cz/", Description = "Služba pøenosu dat inventury1", Name = "Inventura1Service")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Inventura1 : System.Web.Services.WebService
    {
        public enum ProcessInventuraState
        {
            Uvolnit,
            Zpracovat
        }

		#region Lokalne parametry

		private Fask.Server.Interfaces.Inventura1.IInventura1 provider = null;

        const string Inventura1DBFileExtension = @".in1";
		private string TABLE_CZMST_I1 = "CZMST_I1";
		
		#endregion
 
		#region Konstruktor
 
		/// <summary>
		/// Konstruktor
		/// </summary>
		public Inventura1()
		{
			//CODEGEN: This call is required by the ASP.NET Web Services Designer
			InitializeComponent();

			// Inicializuje objektove rozhrani ...
			try
			{
                Konfigurace.Classes.Globals_Konfig_WebConfig.LoadConfiguration();
                string providerAssemblyPath = Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Providers[0].Provider_Inventura1;
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
								if (typeof(Fask.Server.Interfaces.Inventura1.IInventura1).IsAssignableFrom(t))
								{
									provider = (Fask.Server.Interfaces.Inventura1.IInventura1)providerAssemlby.CreateInstance(t.FullName);
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
			catch //(Exception exProvider)
			{
				//;
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
        /// Metoda která vrací hlavièky inventury
        /// </summary>
        /// <param name="terminalID">ID Terminalu</param>
        /// <param name="sklad_id">ID Skladu</param>
        /// <returns>Dataset Inventury1, dotažene inventury</returns>
        [WebMethod(Description = "Metoda která vrací inventury")]
        public Fask.DataSets.Inventury1 GetInventury(byte terminalID, string sklad_id)
        {

                try
                {
                    if (provider != null && provider is Fask.Server.Interfaces.Inventura1.IInventura1)
                    {
                        Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();
                        Fask.Server.Interfaces.Classes.Sklad sklad = new Fask.Server.Interfaces.Classes.Sklad();

                        terminal.ID = terminalID;
                        sklad.ID = sklad_id;

                        return provider.Inventura_GetInventury(
                            terminal,
                            sklad
                            );
                    }
                }
                catch (Exception ex)
                {
					Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                    throw ex;
                }


            string msg = "Provider v Inventura1.asmx > 'GetInventury(byte terminalID, string sklad_id)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
            throw new Exception(msg);
        }



        /// <summary>
        /// Metoda která pripravy soubor na serveru pro Terminal
        /// </summary>
        /// <param name="countentries">èíslo dávky</param>
        /// <param name="idterminal">ID Terminalu</param>
        /// <returns>True- OK, False- chyba</returns>
		[WebMethod(Description = "Metoda která pripravy soubor na serveru pro Terminal")]
		public bool PrepareInventuraDB(int countentries, byte idterminal)
		{
			try
			{


				Fask.DataSets.Inventura1 inventura = GetInventura(countentries, idterminal);


				string dstFile = Path.Combine(Fask.MyPath.Path.SQLiteDBsDirectory, idterminal.ToString() + @"\" + countentries.ToString() + Inventura1DBFileExtension);
				string dstFilezip = dstFile + Common.ZIP;

				if (!Directory.Exists(Path.GetDirectoryName(dstFile)))
					Directory.CreateDirectory(Path.GetDirectoryName(dstFile));


				SQLite_Helper helper = new SQLite_Helper();
				if (!helper.SQLite_CreateFile(dstFile, Common.Inventura1))
				{
					throw new Exception("Nastala chyba pri tvorbe SQLite souboru pro " + Common.Inventura1);
				}


				//Nacist a ulozit konfiguracni parametry
				Fask.SQLiteDBs.DataSets.Inventura1 inventura1sqlce = new Fask.SQLiteDBs.DataSets.Inventura1();
				try
				{
                    Konfigurace.Classes.Globals_Konfig_Agendy.LoadConfiguration();
                    inventura1sqlce.Parametry.ImportRow(Konfigurace.Classes.Globals_Konfig_Agendy.Konfigurace.Inventura1Parametry[0]);
                    //inventura1sqlce.ReadXml(Fask.MyPath.Path.InventuraParams);
					inventura1sqlce.AcceptChanges();

				}
				catch (Exception ex)
				{
					Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				}

				#region old kod

				//SQLiteDBs.DataSets.Inventura1TableAdapters.CZMST_I1TableAdapter i1eta = new Fask.MST_W_Server.SQLiteDBs.DataSets.Inventura1TableAdapters.CZMST_I1TableAdapter();
				//i1eta.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + dstFile);
				//try
				//{
				//	i1eta.Connection.Open();
				//	var tran = i1eta.Connection.BeginTransaction();

				//	foreach (Fask.DataSets.Inventura1.CZMST_I1Row pr in inventura.CZMST_I1)
				//	{
				//		int rowinserted = i1eta.Insert(
				//			CountEntries: pr.CountEntries,
				//			ITEMNMBR: pr.ITEMNMBR.Trim(),
				//			CZ_CarKod: pr.CZ_CarKod.Trim(),
				//			ITEMDESC: pr.ITEMDESC.Trim(),
				//			LOCNCODE: pr.LOCNCODE.Trim(),
				//			QUANTITY: pr.QUANTITY,
				//			DATEDONE: pr.DATEDONE,
				//			IntegerValue: pr.IntegerValue,
				//			TIMESPRT: pr.TIMESPRT,
				//			CZ_SerNum_Track: pr.CZ_SerNum_Track,
				//			CZ_SerNum_Find: pr.CZ_SerNum_Find,
				//			DEX_ROW_ID: pr.DEX_ROW_ID,
				//			TerminalID: pr.TerminalID,
				//			O_TID: pr.IsO_TIDNull() ? (byte?)null : (byte?)pr.O_TID,
				//			skl_id: pr.SKL_ID.Trim(),
				//			DMJ: pr.IsDMJNull() ? string.Empty : pr.DMJ.Trim(),
				//			REZ_1: pr.IsREZ_1Null() ? string.Empty : pr.REZ_1.Trim(),
				//			REZ_2: pr.IsREZ_2Null() ? string.Empty : pr.REZ_2.Trim(),
				//			ITEMCODE: pr.IsITEMCODENull() ? string.Empty : pr.ITEMCODE.Trim(),
				//			CZ_REZ_1_Track: pr.IsCZ_REZ1_TrackNull() ? (byte)0 : pr.CZ_REZ1_Track,
				//			CZ_REZ_2_Track: pr.IsCZ_REZ2_TrackNull() ? (byte)0 : pr.CZ_REZ2_Track
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
				//	if (i1eta != null)
				//	{
				//		if ((i1eta.Connection.State & ConnectionState.Open) == ConnectionState.Open)
				//			i1eta.Connection.Close();
				//		i1eta.Dispose();
				//	}
				//}

				//Fask.SQLiteDBs.DataSets.Inventura1TableAdapters.CZMST_I2TableAdapter i2ta = new Fask.SQLiteDBs.DataSets.Inventura1TableAdapters.CZMST_I2TableAdapter();
				//i2ta.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + dstFile);
				//try
				//{
				//	i2ta.Connection.Open();
				//	var tran = i2ta.Connection.BeginTransaction();
				//	foreach (Fask.DataSets.Inventura1.CZMST_I2Row pr in inventura.CZMST_I2)
				//	{
				//		int rowinserted = i2ta.Insert(
				//			CountEntries: pr.CountEntries,
				//			ITEMNMBR: pr.ITEMNMBR.Trim(),
				//			SERLNMBR: pr.SERLNMBR.Trim(),
				//			DEX_ROW_ID: pr.DEX_ROW_ID,
				//			QTY: pr.IsQTYNull() ? (decimal?)null : pr.QTY
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
				//	if (i2ta != null)
				//	{
				//		if ((i2ta.Connection.State & ConnectionState.Open) == ConnectionState.Open)
				//			i2ta.Connection.Close();
				//		i2ta.Dispose();
				//	}
				//}

				//SQLiteDBs.DataSets.Inventura1TableAdapters.CZMST_I3TableAdapter i3ta = new Fask.MST_W_Server.SQLiteDBs.DataSets.Inventura1TableAdapters.CZMST_I3TableAdapter();
				//i3ta.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + dstFile);
				//try
				//{
				//	i3ta.Connection.Open();
				//	var tran = i3ta.Connection.BeginTransaction();
				//	foreach (Fask.DataSets.Inventura1.CZMST_I3Row pr in inventura.CZMST_I3)
				//	{
				//		int rowinserted = i3ta.Insert(
				//			CountEntries: pr.CountEntries,
				//			ITEMNMBR: pr.ITEMNMBR.Trim(),
				//			CZ_CarKod: pr.CZ_CarKod.Trim(),
				//			QTYPACK: pr.IsQTYPACKNull() ? 0 : pr.QTYPACK,
				//			VENDORID: pr.VENDORID.Trim(),
				//			VNDITNUM: pr.VNDITNUM.Trim(),
				//			VENDNAME: pr.VENDNAME.Trim(),
				//			DEX_ROW_ID: pr.DEX_ROW_ID,
				//			MJ: pr.MJ.Trim(),
				//			WEIGHT: pr.IsWEIGHTNull() ? (decimal?)null : pr.WEIGHT
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
				//	if (i3ta != null)
				//	{
				//		if ((i3ta.Connection.State & ConnectionState.Open) == ConnectionState.Open)
				//			i3ta.Connection.Close();
				//		i3ta.Dispose();
				//	}
				//}

				//SQLiteDBs.DataSets.Inventura1TableAdapters.CZMST_I1HTableAdapter i1hta = new Fask.MST_W_Server.SQLiteDBs.DataSets.Inventura1TableAdapters.CZMST_I1HTableAdapter();
				//i1hta.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + dstFile);
				//try
				//{
				//	i1hta.Connection.Open();
				//	var tran = i1hta.Connection.BeginTransaction();
				//	foreach (Fask.DataSets.Inventura1.CZMST_I1HRow i1h in inventura.CZMST_I1H)
				//	{
				//		int rowinserted = i1hta.Insert(
				//			CountEntries: i1h.CountEntries,
				//			Description: i1h.IsDescriptionNull() ? null : i1h.Description.Trim(),
				//			Status: i1h.IsStateNull() ? (byte)0 : i1h.State
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
				//	if (i1hta != null)
				//	{
				//		if ((i1hta.Connection.State & ConnectionState.Open) == ConnectionState.Open)
				//			i1hta.Connection.Close();
				//		i1hta.Dispose();
				//	}
				//}


				//SQLiteDBs.DataSets.Inventura1TableAdapters.ParametryTableAdapter pta = new Fask.MST_W_Server.SQLiteDBs.DataSets.Inventura1TableAdapters.ParametryTableAdapter();
				//pta.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + dstFile);
				//try
				//{
				//	pta.Connection.Open();
				//	var tran = pta.Connection.BeginTransaction();

				//	DataRow drow0 = (DataRow)inventura1sqlce.Parametry[0];
				//	drow0.SetAdded();
				//	pta.Update(drow0);

				//	tran.Commit();
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

				//SQLite_Classes.SQLite_Helper.Shrink("Data source=" + dstFile);

				#endregion

				try
				{
					using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Inventura1 ConInv1 = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Inventura1(dstFile))
					{
						System.Data.SQLite.SQLiteTransaction tran = null;
						try
						{

							foreach (var item in inventura.CZMST_I1)
							{
								item.SetAdded();
							}

							foreach (var item in inventura.CZMST_I2)
							{
								item.SetAdded();
							}

							foreach (var item in inventura.CZMST_I3)
							{
								item.SetAdded();
							}


							foreach (var item in inventura.CZMST_I4)
							{
								item.SetAdded();
							}

							foreach (var item in inventura.CZMST_I1H)
							{
								item.SetAdded();
							}

							ConInv1.Connection_Open();
							tran = ConInv1.Connection.BeginTransaction();

							ConInv1.Update_I1(inventura.CZMST_I1, ConInv1.Connection, tran);
							ConInv1.Update_I2(inventura.CZMST_I2, ConInv1.Connection, tran);
							ConInv1.Update_I3(inventura.CZMST_I3, ConInv1.Connection, tran);
							ConInv1.Update_I4(inventura.CZMST_I3, ConInv1.Connection, tran);
							ConInv1.Update_I1H(inventura.CZMST_I1H, ConInv1.Connection, tran);

							DataRow drow0 = (DataRow)inventura1sqlce.Parametry[0];
							drow0.SetAdded();
							ConInv1.Update_Params(drow0, ConInv1.Connection, tran);

							tran.Commit();

						}
						catch (System.Exception ex)
						{
							try
							{
								if (tran != null)
									tran.Rollback();
							}
							catch (Exception exTransaction)
							{
								//Logging.Log.Write(exTransaction);
								Logging.ExceptionHandler2.Handle(exTransaction);
							}

							Fask.Logging.ExceptionHandler2.Handle(ex);
							throw ex;
						}
						finally
						{
							ConInv1.Connection_Close();
						}

						try
						{
							ConInv1.Shrink();

						}
						catch (System.Exception ex)
						{
							Fask.Logging.ExceptionHandler2.Handle(ex);
							throw ex;
						}

					}
				}
				catch (Exception ex)
				{
					Logging.ExceptionHandler2.Handle(ex);
					throw ex;
				}


				Fask.Compressing.Zip.Compress(dstFile);

				return true;

			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, " (I1 : Dávka: " + countentries.ToString() + ", Terminál ID:" + idterminal.ToString() + ")");
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				throw ex;
			}
		}

        /// <summary>
        /// Metoda pro oznaèení stažené inventury
        /// </summary>
        /// <param name="countentries">èíslo dávky</param>
        /// <param name="idterminal">ID Terminalu</param>
        /// <returns>True- OK, False- chyba</returns>
        [WebMethod(Description = "Metoda pro oznaèení stažené inventury")]
        public bool GetInventuraReceived(int countentries, byte idterminal)
        {


                try
                {
                    if (provider != null && provider is Fask.Server.Interfaces.Inventura1.IInventura1)
                    {
                        Fask.Server.Interfaces.Classes.Davka davka = new Fask.Server.Interfaces.Classes.Davka();
                        Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();

                        davka.ID = countentries;
                        terminal.ID = idterminal;

                        return provider.Inventura_GetInventuraReceived(
                            davka,
                            terminal
                            );
                    }
                }
                catch (Exception ex)
                {
					Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                    throw ex;
                }

                string msg = "Provider v Inventura1.asmx > 'GetInventuraReceived(int countentries, byte idterminal)' nenastaven.";
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
                throw new Exception(msg);
        }

        /// <summary>
        /// Metoda pro zpracovaní dat na serveru z prijateho souboru.
        /// </summary>
        /// <param name="countentries">èíslo dávky</param>
        /// <param name="idterminal">ID Terminalu</param>
        /// <param name="processState">pøíznak co se ma stat : Uvolnit, Zpracovat</param>
        /// <returns>StatusObject- nese informace o stavu</returns>
		[WebMethod(Description = "Metoda pro zpracovaní dat na serveru z prijateho souboru.")]
		public StatusObject ProcessInventura2(int countentries, byte idterminal, ProcessInventuraState processState)
		{
			StatusObject so = new StatusObject();

			if (!isLicenseValid())
			{
				so.StatusText = "Licence na serveru není validní!";
				so.Exception = true;
				return so;
			}

			TracId tracid = new TracId(null, idterminal, countentries);

			#region trace
			Trac.Write("ProcessInventura START, ProcessPrijemState: " + processState.ToString(), tracid);
			#endregion

			string dstFile = Path.Combine(Fask.MyPath.Path.SQLiteDBsDirectory, idterminal.ToString() + @"\" + countentries.ToString() + Inventura1DBFileExtension);
			string dstFileZip = dstFile + Common.ZIP;

			try
			{

				Fask.DataSets.Inventura1 inventuraData = new Fask.DataSets.Inventura1();
				Fask.SQLiteDBs.DataSets.Inventura1 inventuraDataCE = new Fask.SQLiteDBs.DataSets.Inventura1();

				Fask.Compressing.Zip.Decompress(dstFileZip);



				//SQLiteDBs.DataSets.Inventura1TableAdapters.CZMST_I1TableAdapter i1ta = new Fask.MST_W_Server.SQLiteDBs.DataSets.Inventura1TableAdapters.CZMST_I1TableAdapter();
				//SQLiteDBs.DataSets.Inventura1TableAdapters.CZMST_I2TableAdapter i2ta = new Fask.MST_W_Server.SQLiteDBs.DataSets.Inventura1TableAdapters.CZMST_I2TableAdapter();
				//SQLiteDBs.DataSets.Inventura1TableAdapters.CZMST_I3TableAdapter i3ta = new Fask.MST_W_Server.SQLiteDBs.DataSets.Inventura1TableAdapters.CZMST_I3TableAdapter();
				//SQLiteDBs.DataSets.Inventura1TableAdapters.CZMST_I4TableAdapter i4ta = new Fask.MST_W_Server.SQLiteDBs.DataSets.Inventura1TableAdapters.CZMST_I4TableAdapter();
				//SQLiteDBs.DataSets.Inventura1TableAdapters.CZMST_IHTableAdapter ihta = new Fask.MST_W_Server.SQLiteDBs.DataSets.Inventura1TableAdapters.CZMST_IHTableAdapter();



				//i1ta.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + dstFile);
				//i2ta.Connection = i1ta.Connection;
				//i3ta.Connection = i1ta.Connection;
				//i4ta.Connection = i1ta.Connection;
				//ihta.Connection = i1ta.Connection;

				try
				{
					//i1ta.Connection.Open();
					//var tran = i1ta.Connection.BeginTransaction();

					using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Inventura1 ConInv1 = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Inventura1(dstFile))
					{
						ConInv1.Fill_I1(inventuraDataCE.CZMST_I1);
						ConInv1.Fill_I2(inventuraDataCE.CZMST_I2);
						ConInv1.Fill_I3(inventuraDataCE.CZMST_I3);
						ConInv1.Fill_I4(inventuraDataCE.CZMST_I4);
						ConInv1.Fill_IH(inventuraDataCE.CZMST_IH); 
					}

					//tran.Commit();
				}
				catch (Exception ex)
				{
					Logging.ExceptionHandler2.Handle(ex);
				}
				finally
				{
					//if (i1ta != null)
					//{
					//	if ((i1ta.Connection.State & ConnectionState.Open) == ConnectionState.Open)
					//		i1ta.Connection.Close();
					//	i1ta.Dispose();
					//}

					//if (i2ta != null)
					//{
					//	if ((i2ta.Connection.State & ConnectionState.Open) == ConnectionState.Open)
					//		i2ta.Connection.Close();
					//	i2ta.Dispose();
					//}

					//if (i3ta != null)
					//{
					//	if ((i3ta.Connection.State & ConnectionState.Open) == ConnectionState.Open)
					//		i3ta.Connection.Close();
					//	i3ta.Dispose();
					//}

					//if (i4ta != null)
					//{
					//	if ((i4ta.Connection.State & ConnectionState.Open) == ConnectionState.Open)
					//		i4ta.Connection.Close();
					//	i4ta.Dispose();
					//}

					//if (ihta != null)
					//{
					//	if ((ihta.Connection.State & ConnectionState.Open) == ConnectionState.Open)
					//		ihta.Connection.Close();
					//	ihta.Dispose();
					//}
				}



				inventuraData.CZMST_I1.BeginLoadData();
				foreach (Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I1Row ir in inventuraDataCE.CZMST_I1)
				{
					Fask.DataSets.Inventura1.CZMST_I1Row i = inventuraData.CZMST_I1.NewCZMST_I1Row();
					//inventuraData.CZMST_I1.AddCZMST_I1Row(
					i.CountEntries = ir.CountEntries;
					i.ITEMNMBR = ir.ITEMNMBR;
					i.CZ_CarKod = ir.CZ_CarKod;
					i.ITEMDESC = ir.ITEMDESC;
					i.LOCNCODE = ir.LOCNCODE;
					i.QUANTITY = ir.QUANTITY;
					i.DATEDONE = ir.DATEDONE;
					i.IntegerValue = ir.IntegerValue;
					i.TIMESPRT = ir.TIMESPRT;
					i.CZ_SerNum_Track = ir.CZ_SerNum_Track;
					i.CZ_SerNum_Find = ir.CZ_SerNum_Find;
					i.DEX_ROW_ID = ir.DEX_ROW_ID;
					i.TerminalID = ir.TerminalID;
					if (ir.IsO_TIDNull())
						i.SetO_TIDNull();
					else
						i.O_TID = ir.O_TID;
					i.SKL_ID = ir.skl_id;
					i.DMJ = ir.DMJ;
					i.REZ_1 = ir.REZ_1;
					i.REZ_2 = ir.REZ_2;
					i.ITEMCODE = ir.ITEMCODE;
					i.CZ_REZ1_Track = ir.IsCZ_REZ1_TrackNull() ? (byte)0 : ir.CZ_REZ1_Track;
					i.CZ_REZ2_Track = ir.IsCZ_REZ2_TrackNull() ? (byte)0 : ir.CZ_REZ2_Track;
					i.CZ_Expirace_Track = ir.CZ_Expirace_Track;
					//);
					inventuraData.CZMST_I1.AddCZMST_I1Row(i);
				}
				inventuraData.CZMST_I1.EndLoadData();
				inventuraData.CZMST_I1.AcceptChanges();
				foreach (Fask.DataSets.Inventura1.CZMST_I1Row ir in inventuraData.CZMST_I1)
				{
					ir.SetModified();
				}

				inventuraData.CZMST_I2.BeginLoadData();
				foreach (Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I2Row ir in inventuraDataCE.CZMST_I2)
				{
					inventuraData.CZMST_I2.ImportRow(ir);

					//inventuraData.CZMST_I2.AddCZMST_I2Row(
					//	ir.CountEntries,
					//	ir.ITEMNMBR,
					//	ir.SERLNMBR,
					//	ir.QTY,
					//	ir.IsExpiraceNull() ? DateTime.Now : ir.Expirace
					//	);
				}
				inventuraData.CZMST_I2.EndLoadData();

				inventuraData.CZMST_I3.BeginLoadData();
				foreach (Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I3Row ir in inventuraDataCE.CZMST_I3)
				{
					inventuraData.CZMST_I3.AddCZMST_I3Row(
						ir.CountEntries,
						ir.ITEMNMBR,
						ir.CZ_CarKod,
						ir.IsQTYPACKNull() ? 0 : ir.QTYPACK,
						ir.VENDORID,
						ir.IsVNDITNUMNull() ? string.Empty : ir.VNDITNUM,
						ir.VENDNAME,
						ir.DEX_ROW_ID,
						ir.MJ,
						ir.IsWEIGHTNull() ? 0 : ir.WEIGHT
						);
				}
				inventuraData.CZMST_I3.EndLoadData();

				inventuraData.CZMST_I4.BeginLoadData();
				foreach (Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I4Row ir in inventuraDataCE.CZMST_I4)
				{

					var rowI4 = inventuraData.CZMST_I4.NewCZMST_I4Row();

					rowI4.CountEntries=ir.CountEntries;
					rowI4.ITEMNMBR=ir.ITEMNMBR;
					rowI4.CZ_CarKod=ir.CZ_CarKod;
					rowI4.LOCNCODE=ir.LOCNCODE;
					rowI4.SKL_ID = ir.skl_id;
					rowI4.VNDITNUM=ir.VNDITNUM;
					rowI4.MJ=ir.MJ;
					rowI4.QUANTITY=ir.QUANTITY;
					rowI4.QUANTITYMJ=ir.QUANTITYMJ;
					rowI4.QTYPACK=ir.QTYPACK;
					rowI4.SERLNMBR=ir.SERLNMBR;
					rowI4.DATEDONE=ir.DATEDONE;
					rowI4.TIMEDONE=ir.TIMEDONE;
					rowI4.USERID=ir.USERID;
					rowI4.GUID=ir.GUID;
					rowI4.O_Checked=ir.O_Checked;
					rowI4.INPUT_MODE=ir.INPUT_MODE;
					rowI4.ID_TERMINAL=ir.ID_TERMINAL;
					rowI4.ITEMCODE= ir.IsITEMCODENull() ? string.Empty : ir.ITEMCODE;
					rowI4.REZ_1= ir.IsREZ_1Null() ? string.Empty : ir.REZ_1;
					rowI4.REZ_2= ir.IsREZ_2Null() ? string.Empty : ir.REZ_2;
					rowI4.WEIGHT= ir.IsWEIGHTNull() ? 0 : ir.WEIGHT;
					
					rowI4.SetCE_OrigNull();

					if (ir.IsExpiraceNull())
						rowI4.SetExpiraceNull();
					else
						rowI4.Expirace = ir.Expirace;

					inventuraData.CZMST_I4.AddCZMST_I4Row(rowI4);

					//inventuraData.CZMST_I4.AddCZMST_I4Row(
					//	ir.CountEntries,
					//	ir.ITEMNMBR,
					//	ir.CZ_CarKod,
					//	ir.LOCNCODE, ir.skl_id, ir.VNDITNUM, ir.MJ, ir.QUANTITY, ir.QUANTITYMJ,
					//	ir.QTYPACK, ir.SERLNMBR, ir.DATEDONE, ir.TIMEDONE, ir.USERID, ir.GUID,
					//	ir.O_Checked, ir.INPUT_MODE, ir.ID_TERMINAL,
					//	ir.IsITEMCODENull() ? string.Empty : ir.ITEMCODE,
					//	ir.IsREZ_1Null() ? string.Empty : ir.REZ_1,
					//	ir.IsREZ_2Null() ? string.Empty : ir.REZ_2,
					//	ir.IsWEIGHTNull() ? 0 : ir.WEIGHT,
					//	0
					//	);

					// radeji takto ... ???				
					//var irN = inventuraData.CZMST_I4.NewCZMST_I4Row();

					//irN.CountEntries = ir.CountEntries;
					//irN.ITEMNMBR = ir.is

					//inventuraData.CZMST_I4.AddCZMST_I4Row(irN);
				}

				inventuraData.CZMST_I4.EndLoadData();



				inventuraData.CZMST_IH.BeginLoadData();
				foreach (Fask.SQLiteDBs.DataSets.Inventura1.CZMST_IHRow per in inventuraDataCE.CZMST_IH)
				{
					inventuraData.CZMST_IH.AddCZMST_IHRow(
						per.CountEntries, per.GUID);
				}
				inventuraData.CZMST_IH.EndLoadData();


				so = ProcessInventura(countentries, idterminal, inventuraData, processState);

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
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				Routines.ManageDataFiles.Move(dstFile, Path.Combine(Fask.MyPath.Path.ErrorDataFileDirectory, Path.GetFileName(dstFile)));
				so.StatusText = ex.Message;
				so.Exception = true;
				return so;

				throw Routines.Exceptions.CustomSoapException("Process", ex.Message, "Inventura");

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
				}
			}
		}

        #region Online checks ...

        /// <summary>
        /// Metoda pro online kontrolu položky a oznaèeni
        /// </summary>
        /// <param name="countentries">èíslo dávky</param>
        /// <param name="terminalid">ID Terminalu</param>
        /// <param name="itemnmbr">ID položky</param>
        /// <param name="o_terminalid">reference na O_TID</param>
        /// <returns>True-OK, False- chyba</returns>
        [WebMethod(Description = "Metoda pro online kontrolu položky a oznaèeni")]
        public bool OnlineCheckState(int countentries, byte terminalid, string itemnmbr, out byte o_terminalid)
        {

            SqlConnection xconnection = null;
            SqlCommand xcommand = null;
            try
            {
                Konfigurace.Classes.Globals_Konfig_WebConfig.LoadConfiguration();
                xconnection = new SqlConnection(Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.ConnectionString[0].FASKDB);
                xcommand = new SqlCommand();
                xcommand.Connection = xconnection;
                xcommand.CommandText = "select * from " + TABLE_CZMST_I1 + " where countentries=" + countentries + " and itemnmbr='" + itemnmbr + "'";

                xconnection.Open();
                xcommand.Transaction = xcommand.Connection.BeginTransaction(IsolationLevel.Serializable);

                Fask.DataSets.Inventura1 dsinv1 = new Fask.DataSets.Inventura1();

                IDataReader ireader = xcommand.ExecuteReader();
                try
                {
                    if (ireader.Read()) //existuje zaznam =
                    {
                        object o_tid = ireader["O_TID"];
                        if (o_tid == null || o_tid is System.DBNull)
                        { //neni nastaveno => nastavit
                            o_terminalid = terminalid;
                        }
                        else if (Convert.ToByte(o_tid) == 0) //nebylo nastaveno terminalem
                        {
                            //pokracuje dale nastavenim ...
                            o_terminalid = terminalid;
                        }
                        else
                        { //je nastaveno 
                            o_terminalid = Convert.ToByte(o_tid);
                            // je nastaveno timto terminalem => povolit zapis
                            // neni nastaveno timto terminalem => nepovolit zapis
                            if (Convert.ToByte(o_tid) == terminalid)
                                return true; //nemusim dale pokracovat, protoze jiz bylo nastaveno...
                            else
                                return false;
                        }
                    }
                    else //zaznam neexistuje => nelze overit
                    {
                        o_terminalid = 0;
                        return false;
                    }

                }
                finally
                {
                    if (ireader != null && !ireader.IsClosed)
                        ireader.Close();
                }
                xcommand.CommandText = "Update " + TABLE_CZMST_I1 + " set O_TID=" + o_terminalid + " where countentries=" + countentries + " and itemnmbr='" + itemnmbr + "'";
                int raff = xcommand.ExecuteNonQuery();

                xcommand.Transaction.Commit();

                return true;

            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                if (xcommand != null && xcommand.Transaction != null)
                {
                    xcommand.Transaction.Rollback();
                }
                o_terminalid = 0;
                return false;
            }
            finally
            {
                if (xconnection != null && xconnection.State == ConnectionState.Open)
                {
                    xconnection.Close();
                    xconnection.Dispose();
                    xconnection = null;
                }
            }
        }

        /// <summary>
        /// Metoda pro online kontrolu položky a odznaèeni
        /// </summary>
        /// <param name="countentries">èíslo dávky</param>
        /// <param name="terminalid">ID Terminalu</param>
        /// <param name="itemnmbr">ID položky</param>
        /// <param name="o_terminalid">reference na O_TID</param>
        /// <returns>True-OK, False- chyba</returns>
        [WebMethod(Description = "Metoda pro online kontrolu položky a odznaèeni")]
        public bool OnlineUnCheckState(int countentries, byte terminalid, string itemnmbr, out byte o_terminalid)
        {
            SqlConnection xconnection = null;
            SqlCommand xcommand = null;
            try
            {
                Konfigurace.Classes.Globals_Konfig_WebConfig.LoadConfiguration();
                xconnection = new SqlConnection(Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.ConnectionString[0].FASKDB);
                xcommand = new SqlCommand();
                xcommand.Connection = xconnection;
                xcommand.CommandText = "select * from " + TABLE_CZMST_I1 + " where countentries=" + countentries + " and itemnmbr='" + itemnmbr + "'";

                xconnection.Open();
                xcommand.Transaction = xcommand.Connection.BeginTransaction(IsolationLevel.Serializable);

                Fask.DataSets.Inventura1 dsinv1 = new Fask.DataSets.Inventura1();

                IDataReader ireader = xcommand.ExecuteReader();
                try
                {
                    if (ireader.Read()) //existuje zaznam =
                    {
                        object o_tid = ireader["O_TID"];
                        if (o_tid == null || o_tid is System.DBNull)
                        { //neni nastaveno => povolit odmaz
                            o_terminalid = terminalid;
                            return true; //nemusim dale pokracovat, protoze O_TID v db je NULL ...
                        }
                        else
                        { //je nastaveno 
                            o_terminalid = Convert.ToByte(o_tid);
                            // je nastaveno timto terminalem => povolit odmaz
                            // neni nastaveno timto terminalem => nepovolit odmaz
                            if (Convert.ToByte(o_tid) != terminalid)
                                return false;
                        }
                    }
                    else //zaznam neexistuje => nelze overit
                    {
                        o_terminalid = 0;
                        return false;
                    }

                }
                finally
                {
                    if (ireader != null && !ireader.IsClosed)
                        ireader.Close();
                }

                xcommand.CommandText = "Update " + TABLE_CZMST_I1 + " set O_TID=NULL where countentries=" + countentries + " and itemnmbr='" + itemnmbr + "'";
                int raff = xcommand.ExecuteNonQuery();

                xcommand.Transaction.Commit();

                return true;

            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                if (xcommand != null && xcommand.Transaction != null)
                {
                    xcommand.Transaction.Rollback();
                }
                o_terminalid = 0;
                return false;
            }
            finally
            {
                if (xconnection != null && xconnection.State == ConnectionState.Open)
                {
                    xconnection.Close();
                    xconnection.Dispose();
                    xconnection = null;
                }
            }
        }

        #endregion



        #endregion

        #region Privatne metody

        /// <summary>
        /// Metoda pro dotažení dat inventury pro Terminal
        /// </summary>
        /// <param name="countentries">èíslo dávky</param>
        /// <param name="idterminal">ID Terminalu</param>
		/// <returns>Dataset Inventura1 s naplnenima datama</returns>
        private Fask.DataSets.Inventura1 GetInventura(int countentries, byte idterminal)
        {
            Fask.DataSets.Inventura1 inventura = null;


                try
                {
                    if (provider != null && provider is Fask.Server.Interfaces.Inventura1.IInventura1)
                    {
                        Fask.Server.Interfaces.Classes.Davka davka = new Fask.Server.Interfaces.Classes.Davka();
                        Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();

                        davka.ID = countentries;
                        terminal.ID = idterminal;

                        inventura = provider.Inventura_GetInventura(davka,
                            terminal);

                        return inventura;
                    }

                }
                catch (Exception ex)
                {
					Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                    throw ex;
                }

            string msg = "Provider v Inventura1.asmx > 'GetInventura(int countentries, byte idterminal)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
            throw new Exception(msg);

        }

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
        /// Metoda pro zpracovaní dat na serveru do SQL a IS
        /// </summary>
        /// <param name="countentries">èíslo dávky</param>
        /// <param name="idterminal">ID Terminalu</param>
        /// <param name="inventuraData">Data pro zpracovaní</param>
        /// <param name="processState">pøíznak co se ma stat : Uvolnit, Zpracovat</param>
        /// <returns>StatusObject- nese informace o stavu</returns>
        private StatusObject ProcessInventura(int countentries, byte idterminal, Fask.DataSets.Inventura1 inventuraData, ProcessInventuraState processState)
        {
            try
            {
                if (provider != null && provider is Fask.Server.Interfaces.Inventura1.IInventura1)
                {

                    Fask.Server.Interfaces.Classes.Davka davka = new Fask.Server.Interfaces.Classes.Davka();
                    Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();
                    Fask.Server.Interfaces.Classes.Sklad sklad = new Fask.Server.Interfaces.Classes.Sklad();
                    Fask.Server.Interfaces.Classes.Item item = new Fask.Server.Interfaces.Classes.Item();

                    davka.ID = countentries;
                    terminal.ID = idterminal;

                    Fask.Server.Interfaces.Inventura1.ProcessState pState = (Fask.Server.Interfaces.Inventura1.ProcessState)processState;

                    return provider.Inventura_Process(
                        davka,
                        terminal,
                        inventuraData,
                        pState
                        );
                }

            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }

            string msg = "Provider v Inventura1.asmx > 'ProcessInventura(int countentries, byte idterminal, Fask.DataSets.Inventura1 inventuraData, ProcessInventuraState processState)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
            throw new Exception(msg);
        }

        #endregion

    }
}
