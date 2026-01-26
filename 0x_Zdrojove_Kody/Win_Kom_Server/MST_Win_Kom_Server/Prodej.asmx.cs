using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;
using System.Globalization;
using System.Text;
using System.Data.Common;
using System.IO;
using System.Reflection;
using Fask.Server.Interfaces.Classes;
using Fask.Tracing;
using Fask.Logging;
using System.Data.SqlClient;
using System.Collections.Generic;

using System.Linq;
using MST_Print_Server_ZPL_Printing;
using Fask.MST_W_Server.Constants;
using Fask.Interfaces.DataSets;

namespace Fask.MST_W_Server
{
	/// <summary>
	/// Služba pøenosu dat prodeje
	/// </summary>
	[WebService(Namespace = "http://Prodej.fask.cz/", Description = "Služba pøenosu dat prodeje", Name = "ProdejService")]
	public class Prodej : System.Web.Services.WebService
	{
		public struct DotazeniHodnotParams
		{
			public string itemcode;
			public string itemnmbr;
			public string serltnum;
			public string vnditnum;
			public string czcarkod;
		}

		#region Lokalne promenne

		Fask.Server.Interfaces.Prodej.IProdej provider = null;
		BL.ProdejBL prodejBL = null;

		const string ProdejDBFileExtension = @".di";
		private string TABLE_FASK_ZASOBY = "FASK_ZASOBY";

		#endregion

		#region Konstruktor

		/// <summary>
		/// Konstruktor
		/// </summary>
		public Prodej()
		{
			//CODEGEN: This call is required by the ASP.NET Web Services Designer
			InitializeComponent();
			prodejBL = new BL.ProdejBL();
			prodejBL.Initialize(Server.MapPath);
			provider = prodejBL.provider;
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
		/// Metoda pro zpracovaní dat na serveru
		/// </summary>
		/// <param name="countentries">èíslo dávky</param>
		/// <param name="idterminal">ID Terminalu</param>
		/// <param name="userID">ID uživatele</param>
		/// <param name="login">Login uživatele</param>
		/// <returns>StatusObjekt - informace o zpracování</returns>
		[WebMethod(Description = "Metoda pro zpracovaní dat na serveru")]
		public StatusObject ProcessProdejDB2(int countentries, byte idterminal, int userID, string login)
		{
            StatusObject so = new StatusObject();
			TracId tracid = new TracId(userID, idterminal, countentries);

			#region trace
			Trac.Write("ProcessProdejDB BEGIN", tracid);
			#endregion

			if (!isLicenseValid())
			{
				so.StatusText = "Licence na serveru není validní!";
				so.Exception = true;
				return so;
			}

			string dstFile = Path.Combine(Fask.MyPath.Path.SQLiteDBsDirectory, idterminal.ToString() + @"\" + countentries.ToString() + ProdejDBFileExtension);
			string dstFileZip = dstFile + Common.ZIP;

			if (!File.Exists(dstFileZip))
			{
				so.StatusText = "Nenalezen Soubor pro zpracovani";
				so.Exception = true;
				return so;
			}

			#region trace
			Trac.Write("DB FileStream start", "ProcessProdejDB", tracid);
			#endregion

			Fask.Compressing.Zip.Decompress(dstFileZip);

			#region trace
			Trac.Write("DB FileStream end", "ProcessProdejDB", tracid);
			#endregion

			try
			{
				so = ProcessProdejFile(countentries, idterminal, userID, login, dstFile);
			}
			catch (Exception ex)
			{
				if (File.Exists(dstFile))
					File.Delete(dstFile);

				so.Exception = true;
				so.StatusText = ex.Message;
			}

			if (so.StatusText == "OK" && !so.Exception)
			{
				if (File.Exists(dstFile))
					File.Delete(dstFile);

				if (File.Exists(dstFileZip))
					File.Delete(dstFileZip);
			}
			else
			{
				//so.Exception = true;
				//so.StatusText = "Nezpracovana data na serveru";
			}



			#region trace
			Trac.Write("ProcessProdejDB END", tracid);
			#endregion

			return so;
		}

		/// <summary>
		/// Vrací množství položky s interním èíslem na skladu. Závislé na uložené proceduøe v db servereru(parametr1:itemnumber[char], parametr2:location[char])
		/// </summary>
		/// <param name="itemnmbr">ID položky</param>
		/// <param name="SKL_ID">ID Skladu</param>
		/// <param name="LOCNCODE">Lokace</param>
		/// <param name="QTY">Zadane množství</param>
		/// <returns>StatusResult objekt s informacema</returns>
		[WebMethod(Description = "Vrací množství položky s interním èíslem na skladu. Závislé na uložené proceduøe v db servereru(parametr1:itemnumber[char], parametr2:location[char])")]
		public Fask.Server.Interfaces.Classes.StatusResult Disponibilita(Disponibilita disponibilita)
        {
            return prodejBL.Disponibilita(disponibilita);
        }



        /// <summary>
        /// Metoda pro zpracovaní souboru na serveru
        /// </summary>
        /// <param name="countentries">èíslo dávky</param>
        /// <param name="idterminal">ID Terminalu</param>
        /// <param name="userID">ID uživatele</param>
        /// <param name="login">Login uživatele</param>
        /// <param name="dstFile">Cesta s souboru ktery se sa zpracovat</param>
        /// <returns>StatusObjekt - informace o zpracování</returns>
        [WebMethod(Description = "Metoda pro zpracovaní souboru na serveru")]
		public StatusObject ProcessProdejFile(int countentries, byte idterminal, int userID, string login, string dstFile)
		{
			return prodejBL.ProcessProdejFile( countentries,  idterminal,  userID,  login,  dstFile);

			//// \TODO: try/catch od zacatku do konce, zalogovat chybu a presun datoveho souboru ...
			//StatusObject processStatus = new StatusObject();
			//TracId tracid = new TracId(userID, idterminal, countentries);

			//#region trace
			//Trac.Write("provider is " + (provider != null ? "not null" : "null"), "ProcessProdejFile START", tracid);
			//#endregion

			//if (provider != null) //Nova funkcnost objektova ...
			//{

			//	//1. nacist data z filu
			//	#region Nacteni dat z datoveho souboru do datasetu
			//	Fask.DataSets.ProdejData prodejData = new Fask.DataSets.ProdejData();
			//	Fask.SQLiteDBs.DataSets.Prodej prodejDataCE = new Fask.SQLiteDBs.DataSets.Prodej();

			//	//SQLiteDBs.DataSets.ProdejTableAdapters.CZMST_DITableAdapter dita = new Fask.MST_W_Server.SQLiteDBs.DataSets.ProdejTableAdapters.CZMST_DITableAdapter();
			//	//dita.Connection = new System.Data.SQLite.SQLiteConnection( "Data source=" + dstFile);

			//	//try
			//	//{
			//	//	dita.Fill(prodejDataCE.CZMST_DI);
			//	//}
			//	//catch (Exception ex)
			//	//{
			//	//	Logging.ExceptionHandler2.Handle(ex);
			//	//}
			//	//finally
			//	//{
			//	//	if (dita != null)
			//	//	{
			//	//		if ((dita.Connection.State & ConnectionState.Open) == ConnectionState.Open)
			//	//			dita.Connection.Close();
			//	//		dita.Dispose();
			//	//	}
			//	//} 

			//	using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Prodej ConPro = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Prodej(dstFile))
			//	{
			//		ConPro.Fill_DI(prodejDataCE.CZMST_DI);
			//	}


			//	#region trace
			//	Trac.Write(prodejDataCE.CZMST_DI, "ProcessProdejFile", tracid);
			//	#endregion


			//	prodejData.CZMST_DI.BeginLoadData();

			//	foreach (Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIRow dir in prodejDataCE.CZMST_DI)
			//	{
			//		string ITEMNMBR = string.Empty;
			//		string SERLTNUM = string.Empty;

			//		Fask.DataSets.ProdejData.CZMST_DIRow dirn = prodejData.CZMST_DI.NewCZMST_DIRow();
			//		DotazeniHodnotParams dhp = new DotazeniHodnotParams();
			//		dhp.itemnmbr = dir.ITEMNMBR.Trim();
			//		dhp.czcarkod = dir.IsCZ_CarKodNull() ? string.Empty : dir.CZ_CarKod.Trim();
			//		dhp.vnditnum = dir.IsVNDITNUMNull() ? string.Empty : dir.VNDITNUM.Trim();
			//		dhp.serltnum = dir.SERLTNUM.Trim();
			//		dhp.itemcode = dir.IsITEMCODENull() ? string.Empty : dir.ITEMCODE.Trim();

			//		DotazeniHodnotDo_DI(dhp, dirn);


			//		if (!dir.IsAMOUNPIENull())
			//			dirn.AMOUNPIE = dir.AMOUNPIE;
			//		if (!dir.IsAMOUNPIEMNull())
			//			dirn.AMOUNPIEM = dir.AMOUNPIEM;
			//		dirn.CountEntries = dir.CountEntries;
			//		if (!dir.IsCZ_CarKodNull())
			//			dirn.CZ_CarKod = dir.CZ_CarKod;
			//		if (!dir.IsDATEDONENull())
			//			dirn.DATEDONE = dir.DATEDONE;
			//		dirn.DEX_ROW_ID = dir.DEX_ROW_ID;
			//		if (!dir.IsDOC_IDNull())
			//			dirn.DOC_ID = dir.DOC_ID;
			//		if (!dir.IsDOC_ID2Null())
			//			dirn.DOC_ID2 = dir.DOC_ID2;
			//		dirn.guid = dir.guid;
			//		dirn.ID_TERMINAL = dir.ID_TERMINAL;
			//		dirn.INPUT_MODE = dir.INPUT_MODE;
   //                 //if (!dir.IsITEMCODENull())
   //                 //    dirn.ITEMCODE = dir.ITEMCODE;
   //                 //dirn.ITEMNMBR = dir.ITEMNMBR;
   //                 if (!dir.IsLOCNCODENull())
			//			dirn.LOCNCODE = dir.LOCNCODE;
			//		if (!dir.Ismena_IDNull())
			//			dirn.mena_ID = dir.mena_ID;
			//		if (!dir.Ismena_IDMNull())
			//			dirn.mena_IDM = dir.mena_IDM;
			//		dirn.MJ = dir.MJ;
			//		if (!dir.IsNMBRPALNull())
			//			dirn.NMBRPAL = dir.NMBRPAL;
			//		if (!dir.IsODB_IDNull())
			//			dirn.ODB_ID = dir.ODB_ID;
			//		if (!dir.IsPRAC_IDNull())
			//			dirn.PRAC_ID = dir.PRAC_ID;
			//		if (!dir.IsPRICEXNull())
			//			dirn.PRICEX = dir.PRICEX;
			//		if (!dir.IsQTYPACKNull())
			//			dirn.QTYPACK = dir.QTYPACK;
			//		dirn.QTYSHPPD = dir.QTYSHPPD;
			//		dirn.QTYSHPPDMJ = dir.QTYSHPPDMJ;
			//		if (!dir.IsREZ_1Null())
			//			dirn.REZ_1 = dir.REZ_1;
			//		if (!dir.IsREZ_2Null())
			//			dirn.REZ_2 = dir.REZ_2;
			//		if (!dir.IsREZ_3Null())
			//			dirn.REZ_3 = dir.REZ_3;
			//		if (!dir.IsREZ_4Null())
			//			dirn.REZ_4 = dir.REZ_4;
			//		//dirn.SERLTNUM = dir.SERLTNUM;
			//		if (!dir.IsSKL_IDNull())
			//			dirn.SKL_ID = dir.SKL_ID;
			//		if (!dir.IsSTR_IDNull())
			//			dirn.STR_ID = dir.STR_ID;
			//		if (!dir.IsTAXAMPIENull())
			//			dirn.TAXAMPIE = dir.TAXAMPIE;
			//		if (!dir.IsTAXAMPIEMNull())
			//			dirn.TAXAMPIEM = dir.TAXAMPIEM;
			//		if (!dir.IsTIMEDONENull())
			//			dirn.TIMEDONE = dir.TIMEDONE;
			//		if (!dir.IsTYPEPALNull())
			//			dirn.TYPEPAL = dir.TYPEPAL;
			//		if (!dir.IsUSER_IDNull())
			//			dirn.USER_ID = dir.USER_ID;
			//		if (!dir.IsVNDITNUMNull())
			//			dirn.VNDITNUM = dir.VNDITNUM;
			//		if (!dir.IsWITHTAXNull())
			//			dirn.WITHTAX = dir.WITHTAX;
			//		if (!dir.IsLOCNCODEDESTNull())
			//			dirn.LOCNCODEDEST = dir.LOCNCODEDEST;
			//		if (!dir.IsSKL_ID_DESTNull())
			//			dirn.SKL_ID_DEST = dir.SKL_ID_DEST;
			//		if (!dir.IsWEIGHTNull())
			//			dirn.WEIGHT = dir.WEIGHT;
			//		if (!dir.IsEXPIRACENull())
			//			dirn.EXPIRACE = dir.EXPIRACE;
			//		if (!dir.IsAttributeToSNNull())
			//			dirn.AttributeToSN = dir.AttributeToSN;


			//		prodejData.CZMST_DI.AddCZMST_DIRow(dirn);
			//	}
			//	prodejData.CZMST_DI.EndLoadData();

			//	//SQLiteDBs.DataSets.ProdejTableAdapters.CZMST_DI_RFIDTableAdapter dirfidta = new Fask.MST_W_Server.SQLiteDBs.DataSets.ProdejTableAdapters.CZMST_DI_RFIDTableAdapter();
			//	//dirfidta.Connection = new System.Data.SQLite.SQLiteConnection( "Data source=" + dstFile);

			//	//try
			//	//{
			//	//	dirfidta.Fill(prodejDataCE.CZMST_DI_RFID);
			//	//}
			//	//catch (Exception ex)
			//	//{
			//	//	Logging.ExceptionHandler2.Handle(ex);
			//	//}
			//	//finally
			//	//{
			//	//	if (dirfidta != null)
			//	//	{
			//	//		if ((dirfidta.Connection.State & ConnectionState.Open) == ConnectionState.Open)
			//	//			dirfidta.Connection.Close();
			//	//		dirfidta.Dispose();
			//	//	}
			//	//} 

			//	using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Prodej ConPro = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Prodej(dstFile))
			//	{
			//		ConPro.Fill_DI_RFID(prodejDataCE.CZMST_DI_RFID);
			//	}

			//	#region trace
			//	Trac.Write(prodejDataCE.CZMST_DI_RFID, "ProcessProdejFile", tracid);
			//	#endregion

			//	foreach (var i in prodejDataCE.CZMST_DI_RFID)
			//	{
			//		i.AcceptChanges();
			//		i.SetAdded();
			//		prodejData.CZMST_DI_RFID.ImportRow(i);
			//	}

			//	//SQLiteDBs.DataSets.ProdejTableAdapters.CZMST_DEHTableAdapter dehta = new Fask.MST_W_Server.SQLiteDBs.DataSets.ProdejTableAdapters.CZMST_DEHTableAdapter();
			//	//dehta.Connection = new System.Data.SQLite.SQLiteConnection( "Data source=" + dstFile);
			//	//try
			//	//{
			//	//	dehta.Fill(prodejDataCE.CZMST_DEH);
			//	//}
			//	//catch (Exception ex)
			//	//{
			//	//	Logging.ExceptionHandler2.Handle(ex);
			//	//}
			//	//finally
			//	//{
			//	//	if (dehta != null)
			//	//	{
			//	//		if ((dehta.Connection.State & ConnectionState.Open) == ConnectionState.Open)
			//	//			dehta.Connection.Close();
			//	//		dehta.Dispose();
			//	//	}
			//	//}

			//	using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Prodej ConPro = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Prodej(dstFile))
			//	{
			//		ConPro.Fill_DEH(prodejDataCE.CZMST_DEH);
			//	}

			//	#region trace
			//	Trac.Write(prodejDataCE.CZMST_DEH, "ProcessProdejFile", tracid);
			//	#endregion

			//	prodejData.CZMST_DEH.BeginLoadData();
			//	foreach (Fask.SQLiteDBs.DataSets.Prodej.CZMST_DEHRow dir in prodejDataCE.CZMST_DEH)
			//	{
			//		prodejData.CZMST_DEH.AddCZMST_DEHRow(
			//			dir.CountEntries,
			//			dir.GUID
			//			);
			//	}
			//	prodejData.CZMST_DEH.EndLoadData();

			//	//SQLiteDBs.DataSets.ProdejTableAdapters.CZMST_DIHTableAdapter dihta = new Fask.MST_W_Server.SQLiteDBs.DataSets.ProdejTableAdapters.CZMST_DIHTableAdapter();
			//	//dihta.Connection = new System.Data.SQLite.SQLiteConnection( "Data source=" + dstFile);
			//	//try
			//	//{
			//	//	dihta.Fill(prodejDataCE.CZMST_DIH);
			//	//}
			//	//catch (Exception ex)
			//	//{
			//	//	Logging.ExceptionHandler2.Handle(ex);
			//	//}
			//	//finally
			//	//{
			//	//	if (dihta != null)
			//	//	{
			//	//		if ((dihta.Connection.State & ConnectionState.Open) == ConnectionState.Open)
			//	//			dihta.Connection.Close();
			//	//		dihta.Dispose();
			//	//	}
			//	//}

			//	using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Prodej ConPro = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Prodej(dstFile))
			//	{
			//		ConPro.Fill_DIH(prodejDataCE.CZMST_DIH);
			//	}

			//	#region trace
			//	Trac.Write(prodejDataCE.CZMST_DIH, "ProcessProdejFile", tracid);
			//	#endregion

			//	prodejData.CZMST_DIH.BeginLoadData();
			//	foreach (Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIHRow dir in prodejDataCE.CZMST_DIH)
			//	{
			//		prodejData.CZMST_DIH.AddCZMST_DIHRow(
			//			dir.IsZakazka_IDNull() ? "" : dir.Zakazka_ID,
			//			dir.IsPaleta_IDNull() ? "" : dir.Paleta_ID,
			//			dir.Ismena_IDNull() ? "" : dir.mena_ID,
			//			dir.IsSKL_IDNull() ? "" : dir.SKL_ID,
			//			countentries
			//			);
			//	}
			//	prodejData.CZMST_DIH.EndLoadData();

			//	#endregion

			//	//2. zavolani process data rozhrani objektu
			//	#region Zpracovani dat

			//	Fask.Server.Interfaces.Classes.Davka davka = new Fask.Server.Interfaces.Classes.Davka();
			//	Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();
			//	Fask.Server.Interfaces.Classes.User uzivatel = new Fask.Server.Interfaces.Classes.User();

			//	davka.ID = countentries;
			//	terminal.ID = idterminal;
			//	uzivatel.ID = userID;
			//	uzivatel.Login = login;

			//	#region trace
			//	Trac.Write("provider.Prodej_Process(davka, terminal,uzivatel, prodejData) begin", "ProcessProdejFile", tracid);
			//	#endregion

			//	try
			//	{
			//		processStatus = provider.Prodej_Process(davka, terminal, uzivatel, prodejData);
			//	}
			//	catch (System.Exception ex)
			//	{
			//		processStatus.SetException(ex);
			//		return processStatus;
			//	}

			//	#region trace
			//	Trac.Write("provider.Prodej_Process(davka, terminal,uzivatel, prodejData) end, StatusText : " + processStatus.StatusText, "ProcessProdejFile", tracid);
			//	#endregion
			//	#endregion

			//	//3. Managment datoveho souboru
			//	#region Managment datoveho souboru
			//	if (processStatus.StatusText == "OK" && !processStatus.Exception) //uspelo => do processed
			//	{
			//		Routines.ManageDataFiles.Move(dstFile, Path.Combine(Fask.MyPath.Path.ProcessedDataFileDirectory, Path.GetFileName(dstFile)));
			//	}
			//	else //neuspelo => do erroru
			//	{
			//		Routines.ManageDataFiles.Move(dstFile, Path.Combine(Fask.MyPath.Path.ErrorDataFileDirectory, Path.GetFileName(dstFile)));
			//	}
			//	#endregion

			//	#region trace
			//	Trac.Write("ProcessProdejFile END", tracid);
			//	#endregion

			//	//4. navratova hodnota zpracovani ...
			//	return processStatus;
			//}

			//return null;

		}

		/// <summary>
		/// Metoda pomoci ktere se ovìøí pohyb v Lokaèním mechanizmu
		/// </summary>
		/// <param name="prodejPohyb">Informace o položke ktera se ma ovìøit</param>
		/// <param name="idterminal">ID terminalu</param>
		/// <param name="userID">ID uživatele</param>
		/// <returns>StatusOverPohyb - objekt ktery nese informace o pohybu</returns>
		[WebMethod(Description = "Metoda pomoci ktere se ovìøí pohyb v Lokaèním mechanizmu")]
		public StatusOverPohyb OverPohyb(Fask.Server.Interfaces.Classes.ProdejPohyb prodejPohyb, byte idterminal, int userID)
		{
			TracId tracid = new TracId(userID, idterminal, null);

			StatusOverPohyb statusOP = new StatusOverPohyb();
			try
			{
				#region trace
				Trac.Write("OverPohyb BEGIN", tracid);
				#endregion

				statusOP = new StatusOverPohyb();

				if (provider != null && provider is Fask.Server.Interfaces.Prodej.IProdej)
				{
					statusOP = provider.Over_Pohyb(prodejPohyb);
					return statusOP;
				}
			}
			catch (Exception ex)
			{
				#region trace
				Trac.Write(ex, "OverPohyb END, exception", tracid);
				#endregion
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				statusOP.Message = ex.Message;
			}
			finally
			{
				#region trace
				Trac.Write("OverPohyb END, finally", tracid);
				#endregion
			}

			string msg = "Provider v Prodej.asmx > 'OverPohyb(Fask.Server.Interfaces.Classes.ProdejPohyb prodejPohyb, byte idterminal, int userID)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
			throw new Exception(msg);
		}

		/// <summary>
		/// Metoda  pro ANC. GetSklad pro navrat skladu, se kterymi se pracuje.
		/// Vyuziva se v prodejnim modulu a na prijmu.
		/// </summary>
		/// <param name="idterminal">ID Terminalu</param>
		/// <param name="userID">ID uživatele</param>
		/// <param name="doc_id">ID Typi dokladu</param>
		/// <param name="itemnmbr">ID položky</param>
		/// <param name="serltnum">Seriove èislo/ šarže</param>
		/// <param name="skl_id">ID Skladu zdroj</param>
		/// <param name="skl_id_dest">ID Skladu cil</param>
		/// <returns>STATUS - Enum, bud OK anebo ERROR </returns>
		[WebMethod(Description = "Metoda  pro ANC. GetSklad pro navrat skladu, se kterymi se pracuje.Vyuziva se v prodejnim modulu a na prijmu.")]
		public Fask.Server.Interfaces.Classes.STATUS GetSklad(byte idterminal, int userID, string doc_id, string itemnmbr, string serltnum, out string skl_id, out string skl_id_dest)
		{
            try
            {
                if ((provider != null && provider is Fask.Server.Interfaces.Prodej.IProdej))
                {

                    return provider.GetSklad( idterminal,  userID,  doc_id,  itemnmbr,  serltnum, out  skl_id, out  skl_id_dest);
                }
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }

            string msg = "Provider v Prodej.asmx > 'GetSklad(byte idterminal, int userID, string doc_id, string itemnmbr, string serltnum, out string skl_id, out string skl_id_dest)' nenastaven.";
            Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
            throw new Exception(msg);
        }

		/// <summary>
		/// Metoda ktera Online pomoci ICO dotahne seznam odbìratelu/dodavatelu/partneru/gumitku...
		/// </summary>
		/// <param name="terminalid">ID terminalu</param>
		/// <param name="skladid">ID Skladu</param>
		/// <param name="ICO">ICO</param>
		/// <returns>Dataset Odberatele naplneni podle nalezenych hodnot</returns>
		[WebMethod(Description="Metoda ktera Online pomoci ICO dotahne seznam odbìratelu/dodavatelu/partneru/gumitku...")]
		public Odberatele Online_GetDodavateleByICO(byte terminalid, string skladid, string ICO)
		{
			try
			{
				if ((provider != null && provider is Fask.Server.Interfaces.Prodej.IProdej))
				{
					Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();
					Sklad sklad = new Sklad();

					terminal.ID = terminalid;
					sklad.ID = skladid;

					return provider.Prodej_GetDodavatele_ExternalByICO(terminal, sklad, ICO);
				}
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				throw ex;
			}

			string msg = "Provider v Prodej.asmx > 'Online_GetDodavateleByICO(byte terminalid, string skladid, string ICO)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
			throw new Exception(msg);

		}

		/// <summary>
		/// Metoda pro Online dotaženi mnozstvi materialu na jednotlivych lokaci
		/// </summary>
		/// <param name="itemnmbr">ID položky</param>
		/// <param name="skl_id">ID Skladu</param>
		/// <param name="serltnum">Seriove èíslo / šarže</param>
		/// <param name="doc_id">ID Typu dokladu</param>
		/// <param name="ShowEmpty">pøiznak zda dotahovat prazdne</param>
		/// <returns>Dataset Location - naplneni položkama</returns>
		[WebMethod(Description = "Metoda pro Online dotaženi mnozstvi materialu na jednotlivych lokaci")]
		public Fask.Server.Interfaces.DataSets.Location Online_GetMaterial(string itemnmbr, string skl_id, string serltnum, string doc_id, bool ShowEmpty)
		{
			try
			{
				if (provider != null && provider is Fask.Server.Interfaces.Prodej.IProdej)
				{
					return provider.Prodej_Online_GetMaterial(itemnmbr, skl_id, serltnum, doc_id, ShowEmpty);
				}
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				throw ex;
			}

			string msg = "Provider v Prodej.asmx > 'Online_GetMaterial(string itemnmbr, string skl_id, string serltnum, string doc_id, bool ShowEmpty)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
			throw new Exception(msg);
		}


		/// <summary>
		/// Metoda pro Online ovìøení lokace
		/// </summary>
		/// <param name="itemnmbr">ID položky</param>
		/// <param name="serltnum">Seriove èislo/ šarže</param>
		/// <param name="locncode">lokace</param>
		/// <param name="skl_id">ID Skladu</param>
		/// <param name="qtyshppd">Množství</param>
		/// <param name="doc_id">ID Typu dokladu</param>
		/// <param name="locationType">'S' - overovani zdrojove lokace, 'D' - overovani cilove lokace</param>
		/// <param name="recordType"></param>
		/// <returns>StatusOverLokace - objekt s overenou lokaci</returns>
		[WebMethod(Description = "Metoda pro Online ovìøení lokace")]
		public StatusOverLokace Online_OverLokace(string itemnmbr, string serltnum, DateTime? expirace, string locncode, string skl_id, decimal qtyshppd, string doc_id, TYPLokace locationType, Fask.Server.Interfaces.Lokace.TypeOfRecord recordType)
		{
			try
			{
				if (provider != null && provider is Fask.Server.Interfaces.Prodej.IProdej)
				{
					return provider.Prodej_Online_OverLokace(itemnmbr, serltnum,expirace, locncode, skl_id, qtyshppd, doc_id, locationType, recordType);
				}

			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				throw ex;
			}

			string msg = "Provider v Prodej.asmx > 'Online_OverLokace(string itemnmbr, string serltnum, string locncode, string skl_id, decimal qtyshppd, string doc_id, TYPLokace locationType, Fask.Server.Interfaces.Lokace.TypeOfRecord recordType)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
			throw new Exception(msg);
		}

		/// <summary>
		/// Metoda sloužící pro dotažení Dodavatele/odbìratele/patnera... podle èasoveho kodu položky
		/// </summary>
		/// <param name="terminalid">ID Terminalu</param>
		/// <param name="skladid">ID Skladu</param>
		/// <param name="itemtype">Typ položky</param>
		/// <param name="ListCarKod">List èarových kodu</param>
		/// <returns>Dataset Odberatele naplnen seznam odberatelu</returns>
		[WebMethod(Description = "Metoda sloužící pro dotažení Dodavatele/odbìratele/patnera... podle èasoveho kodu položky")]
		public Odberatele Online_GetSeznamDodavatele_Vyber(byte terminalid, string skladid, string itemtype, List<string> ListCarKod)
		{
			try
			{
				if ((provider != null) && (provider is Fask.Server.Interfaces.Prodej.IProdej))
				{
					Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();
					Sklad sklad = new Sklad();
					Item item = new Item();

					terminal.ID = terminalid;
					sklad.ID = skladid;
					item.Type = itemtype;

					return provider.Prodej_GetDodavatele_External(terminal, sklad, item, ListCarKod);
				}
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				throw ex;
			}

			string msg = "Provider v Prodej.asmx > 'Online_GetSeznamDodavatele_Vyber(byte terminalid, string skladid, string itemtype, List<string> ListCarKod)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
			throw new Exception(msg);

		}

		/// <summary>
		/// Metoda která prijima data, ktera se maji poslat, pripadne doplnit, pro tisk Paletového lístku
		/// </summary>
		/// <param name="data">Vstupni data pro tisk</param>
		/// <returns>StatusResult - Objekt o informacich o tisku</returns>
		[WebMethod(Description = "Metoda která prijima data, ktera se maji poslat, pripadne doplnit, pro tisk Paletového lístku")]
		public Fask.Server.Interfaces.Classes.StatusResult ProcessSoupis(Fask.DataSets.ProdejData data)
		{
			// \TODO : Vyvest vypocty do providera pro konkretni system/zakaznika ...
			// aktualni implementace pro Steinex vydej ...
			Fask.Server.Interfaces.Classes.StatusResult status = new StatusResult();
			status.Status = StatusResultEnum.OK;

			try
			{

				if (provider != null && provider is Fask.Server.Interfaces.Prodej.IProdej)
				{
                    Konfigurace.Classes.Globals_Konfig_WebConfig.LoadConfiguration();
                    // Dotazeni nastaveni templatu ...
                    string templateHeader = "";
					string templateRow = "";
					string templateFooter = "";
					int pocet = 1;

					Fask.Server.Interfaces.DataSets.DSValues dataHeader = null;
					List<Fask.Server.Interfaces.DataSets.DSValues> dataRowList = null;
					Fask.Server.Interfaces.DataSets.DSValues dataFooter = null;

					status = provider.Prodej_ProcessTiskSoupis(data, out dataHeader, out dataRowList, out dataFooter);

					//dataRowList.Add(ds);

					// Footer ???
					bool isHomogennous = false;
					// Druh etiket : toto je reseno konfiguraci dle typu pohybu, pripadne jinak
					// novy konfiguracni soubor : 
					ProdejPrintPLConfig ppplconfig = new ProdejPrintPLConfig();
					if (File.Exists(MyPath.Path.ProdejPLPrintConfig))
						ppplconfig.ReadXml(MyPath.Path.ProdejPLPrintConfig);
					else
						ppplconfig.WriteXml(MyPath.Path.ProdejPLPrintConfig, XmlWriteMode.WriteSchema);

					string doc_id = data.CZMST_DI[0].DOC_ID.Trim();
					string doc_id2 = data.CZMST_DI[0].IsDOC_ID2Null() ? null : data.CZMST_DI[0].DOC_ID2.Trim();
					var templates = ppplconfig.PL.Where(x => x.doc_id.Trim() == doc_id && x.homogennous == isHomogennous);
					if (templates.Count() == 0)
						throw new Exception("No template found");
					var templatePLRow = templates.First();

					templateHeader = templatePLRow.templateHeader;
					templateRow = templatePLRow.templateRow;
					templateFooter = templatePLRow.templateFooter;

					Tisk wsTisk = new Tisk();
					var tiskparams = new TiskParams();
					tiskparams.CONFIG_NAME = Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.PaletovyListek[0].PrinterName;

					wsTisk.Soupis(0, tiskparams, dataHeader, dataRowList, dataFooter, templateHeader, templateRow, templateFooter, pocet);



					return status;

				}
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				status.Status = StatusResultEnum.ERROR;
				status.Message = ex.Message;

				return status;
			}

			string msg = "Provider v Prodej.asmx > 'ProcessSoupis(Fask.DataSets.ProdejData data)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
			throw new Exception(msg);

		}

		[WebMethod(Description="Jedna se o online metodu, ktera ma byt univerzalna podle typu dokladu...")]
		public Fask.Server.Interfaces.Classes_OnlineKomunikace.VystupniObjekt Online_UniverzalnyDotazNaCokoliv(Fask.Server.Interfaces.Classes_OnlineKomunikace.VstupniObjekt ObjektIN)
		{
			Fask.Server.Interfaces.Classes_OnlineKomunikace.VystupniObjekt ObjektOUT = null;

			try
			{

				if (provider != null && provider is Fask.Server.Interfaces.Prodej.IProdej)
				{
					return provider.Online_UniverzalnyDotazNaCokoliv(ObjektIN);
				}

			}
			catch (System.Exception ex)
			{
				Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
			}

			return ObjektOUT;

		}

		[WebMethod(Description = "Metoda která vraci SQLite zakladaci script pro prodej")]
		public StatusObject Get_SQLlite_Script_DB()
		{
			StatusObject so = new StatusObject();
			try
			{

				string srcPath = System.IO.Path.Combine(Fask.MyPath.Path.SQLiteDBsDirectory, "Prodej" + @".sql");

				if (!File.Exists(srcPath))
				{
					so.SetException(new Exception("Soubor Prodej.sql nenalezen."));
					return so;
				}
				else
				{
					string content = File.ReadAllText(srcPath);

					if (string.IsNullOrEmpty(content))
					{
						so.SetException(new Exception("Soubor Prodej.sql je prázdný, anebo se podaøilo ho koretnì naèíst."));
						return so;
					}
					else
					{
						so.Exception = false;
						so.StatusText = content;
						return so;
					}
				}
			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				so.SetException(ex);
				return so;
			}
		}

		#endregion

		#region Privatne metody

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
		/// Metoda která dotahne hodnoty z FASK_ZASOBY do DI
		/// </summary>
		/// <param name="dhp">Podminky pro dotaženi</param>
		/// <param name="dirOUT">Dotaženy Row z CZMST_DI</param>
		private void DotazeniHodnotDo_DI(DotazeniHodnotParams dhp, Fask.DataSets.ProdejData.CZMST_DIRow dirOUT)
		{
            Konfigurace.Classes.Globals_Konfig_WebConfig.LoadConfiguration();

            SqlConnection connect = null;

			connect = new System.Data.SqlClient.SqlConnection(Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.ConnectionString[0].FASKDB);

			if (String.IsNullOrEmpty(dhp.itemnmbr))
			{

				if (string.IsNullOrEmpty(dhp.serltnum))
				{

					string commandText095 = "Select * from " + TABLE_FASK_ZASOBY + " where CZ_CarKod=@CZ_CarKod or VNDITNUM=@vnditnum";
					SqlCommand command095 = new SqlCommand(commandText095, connect);
					command095.Parameters.Add(new SqlParameter("@CZ_CarKod", dhp.czcarkod ?? string.Empty));
					command095.Parameters.Add(new SqlParameter("@vnditnum", dhp.vnditnum ?? string.Empty));
					SqlDataAdapter adapter095 = new SqlDataAdapter();
					adapter095.SelectCommand = command095;
					Fask.Interfaces.DataSets.Zbozi dsZbozi = new Fask.Interfaces.DataSets.Zbozi();
					adapter095.Fill(dsZbozi, dsZbozi.FASK_ZASOBY.TableName);

					dirOUT.ITEMNMBR = dsZbozi.FASK_ZASOBY[0].ITEMNMBR.Trim();
					dirOUT.ITEMCODE = dsZbozi.FASK_ZASOBY[0].ITEMCODE.Trim();


				}
				else
				{
					string commandTextSTAV2 = "SELECT * FROM CZMST_SkladLokace_Stav where SERLTNUM=@SERLTNUM";
					SqlCommand commandSTAV2 = new SqlCommand(commandTextSTAV2, connect);
					commandSTAV2.Parameters.Add(new SqlParameter("@SERLTNUM", dhp.serltnum));
					SqlDataAdapter adapterSTAV2 = new SqlDataAdapter();
					adapterSTAV2.SelectCommand = commandSTAV2;
					DataSet ds = new DataSet();
					adapterSTAV2.Fill(ds);

					string ITEMNMBRtmp = (string)ds.Tables[0].Rows[0]["ITEMNMBR"];
					dirOUT.ITEMNMBR = ITEMNMBRtmp.Trim();

					string ITEMCODEtmp = (string)ds.Tables[0].Rows[0]["ITEMCODE"];
					dirOUT.ITEMCODE = ITEMCODEtmp.Trim();

				}
			}
			else
			{
				dirOUT.ITEMNMBR = dhp.itemnmbr;
				dirOUT.SERLTNUM = dhp.serltnum ?? string.Empty;
				dirOUT.ITEMCODE = dhp.itemcode ?? string.Empty;

			}
		}

		/// <summary>
		/// Metoda která z tabulky FASK_ZASOBY dotahne ITEMDESC pro položku podle ITEMNMBR
		/// </summary>
		/// <param name="itemnmbr">ID položky</param>
		/// <returns></returns>
		private string DotazeniNazvu(string itemnmbr)
		{
            Konfigurace.Classes.Globals_Konfig_WebConfig.LoadConfiguration();

            SqlConnection connect = null;
			connect = new System.Data.SqlClient.SqlConnection(Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.ConnectionString[0].FASKDB);
			string commandText095 = "Select * from " + TABLE_FASK_ZASOBY + " where ITEMNMBR=@itemnmbr";
			SqlCommand command095 = new SqlCommand(commandText095, connect);
			command095.Parameters.Add(new SqlParameter("@itemnmbr", itemnmbr ?? string.Empty));
			SqlDataAdapter adapter095 = new SqlDataAdapter();
			adapter095.SelectCommand = command095;
			Fask.Interfaces.DataSets.Zbozi dsZbozi = new Fask.Interfaces.DataSets.Zbozi();
			adapter095.Fill(dsZbozi, dsZbozi.FASK_ZASOBY.TableName);

			if (dsZbozi.FASK_ZASOBY != null && dsZbozi.FASK_ZASOBY.Count > 0)
				return dsZbozi.FASK_ZASOBY[0].ITEMDESC.Trim();
			else
				return string.Empty;
		}

		/// <summary>
		/// Metoda pro praci s èarovym kodem
		/// </summary>
		/// <param name="data">Vstupni Prodejdata</param>
		/// <param name="dataValues">Vtupni všeobecna data</param>
		/// <param name="vnditnum">èar. kod</param>
		/// <param name="qty">množstvi</param>
		private void pl_calculate_barcode(Fask.DataSets.ProdejData data, Fask.Server.Interfaces.DataSets.DSValues dataValues, string vnditnum, decimal qty)
		{
			string aiPrefix = vnditnum.Substring(0, 2);
			if ((qty <= 99.999m) && (vnditnum.Trim().Length <= 7) && (aiPrefix == "28" || aiPrefix == "29")) //vahovy kod
			{
				decimal vaha_integral = Math.Truncate(qty);
				decimal vaha_fractional = Math.Truncate((qty - vaha_integral) * 1000);
				string vaha = vaha_integral.ToString("00") + vaha_fractional.ToString("000");
				string barcode = vnditnum.PadRight(7, '0') + vaha;
				barcode = barcode.PadRight(13, '0');
				dataValues.Values.AddValuesRow("Barcode", barcode);
				dataValues.Values.AddValuesRow("BarcodeGTIN14", barcode.PadLeft(14, '0'));
			}
			else
			{
				string barcode = vnditnum.PadRight(13, '0');
				dataValues.Values.AddValuesRow("Barcode", barcode);
				dataValues.Values.AddValuesRow("BarcodeGTIN14", vnditnum.PadLeft(14, '0'));
			}
		}

		#endregion

	}
}