using Fask.Logging;
using Fask.MST_W_Server.Constants;
using Fask.MST_W_Server.SQLite_Classes;
using Fask.Server.Interfaces.Classes;
using Fask.Tracing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Reflection;
using System.Web.Services;
using System.Linq;

namespace Fask.MST_W_Server
{
	/// <summary>
	/// Služba pøenosu dat pøíjmu.
	/// </summary>
	[WebService(Namespace="http://fask.cz/", Description="Služba pøenosu dat pøíjmu", Name="PrijemService")]
	public class Prijem : System.Web.Services.WebService
	{
		public enum ProcessPrijemState
		{
			Uvolnit,
			Zpracovat
		}

		#region Lokalne promenne

		Fask.Server.Interfaces.Prijem.IPrijem provider = null;

		const string PrijemDBFileExtension = @".pp";

		#endregion

		#region Konstruktor

		/// <summary>
		/// Konstruktor
		/// </summary>
		public Prijem()
		{
			//CODEGEN: This call is required by the ASP.NET Web Services Designer
			InitializeComponent();

			// Inicializuje objektove rozhrani ...
			try
			{
                Konfigurace.Classes.Globals_Konfig_WebConfig.LoadConfiguration();
                string providerAssemblyPath = Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Providers[0].Provider_Prijem;
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
								if (typeof(Fask.Server.Interfaces.Prijem.IPrijem).IsAssignableFrom(t))
								{
									provider = (Fask.Server.Interfaces.Prijem.IPrijem)providerAssemlby.CreateInstance(t.FullName);
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
		protected override void Dispose( bool disposing )
		{
			if(disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);		
		}
		
		#endregion

		#region WebMetody

		/// <summary>
		/// Metoda která ukonèí pøijemku tym že na CZ_DOSLO natsaví ID TErminal + 100 
		/// </summary>
		/// <param name="idterminal">ID Terminalu</param>
		/// <param name="idCountEntries">èíslo dávky</param>
		/// <param name="password">Heslo</param>
		/// <returns>StatusObject - Objekt který nese informace o stavu</returns>
		[WebMethod(Description = "Metoda která ukonèí pøijemku tym že na CZ_DOSLO natsaví ID TErminal + 100 ")]
		public StatusObject FinishPrijemka(byte idterminal, string idCountEntries, string password)
		{
			StatusObject processStatus = new StatusObject();

			try
			{
				if (provider != null && provider is Fask.Server.Interfaces.Prijem.IPrijem)
				{
					try
					{
						Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();
						Fask.Server.Interfaces.Classes.Davka davka = new Davka();

						terminal.ID = idterminal;
						davka.ID = int.Parse(idCountEntries);
						//item.Type = itemtype;

						processStatus = provider.Prijem_Finish_Prijemka(
							davka,
							terminal,
							password
							);

						return processStatus;
					}
					catch (Exception ex)
					{
						Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
						throw ex;
					}
				}
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
			}

			string msg = "Provider v Prijem.asmx > 'FinishPrijemka(byte idterminal, string idCountEntries, string password)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
			throw new Exception(msg);

		}

		/// <summary>
		/// Metoda sloužíci pro vypoèet položky zda se ma dát na Sklad, Exedici aleno rozdelit
		/// </summary>
		/// <param name="ITEMNMBR">ID položky</param>
		/// <param name="MnozstviZadane">Mnozstvi Zadane</param>
		/// <param name="MnozstviNasnimane">Mnozstvi Nasnimane</param>
		/// <param name="MnozstviDodavatelePozadovano">Mnozstvi Dodavatele Pozadovano</param>
		/// <param name="MnozstviDodavateleDodano">Mnozstvi Dodavatele Dodano</param>
		/// <param name="MnozstviDodavateleDodat">Mnozstvi Dodavatele Dodat</param>
		/// <param name="MnozstviOdberateliPozadovano">Mnozstvi Odberateli Pozadovano</param>
		/// <param name="MnozstviOdberatelumDodano">Mnozstvi Odberatelum Dodano</param>
		/// <param name="MnozstviOdberatelumDodat">Mnozstvi Odberatelum Dodat</param>
		/// <param name="Vysledek">Vysledek</param>
		/// <returns>True - prošlo, False - chyba</returns>
		[WebMethod(Description = "Metoda sloužíci pro vypoèet položky zda se ma dát na Sklad, Exedici aleno rozdelit")]
		public bool Online_GetSkladExpedice(
		string ITEMNMBR,
		decimal MnozstviZadane,
		decimal MnozstviNasnimane,
		out decimal MnozstviDodavatelePozadovano,
		out decimal MnozstviDodavateleDodano,
		out decimal MnozstviDodavateleDodat,
		out decimal MnozstviOdberateliPozadovano,
		out decimal MnozstviOdberatelumDodano,
		out decimal MnozstviOdberatelumDodat,
		out decimal Vysledek
		)
		{

			try
			{
					MnozstviDodavatelePozadovano =
					MnozstviDodavateleDodano =
					MnozstviDodavateleDodat =
					MnozstviOdberateliPozadovano =
					MnozstviOdberatelumDodano =
					MnozstviOdberatelumDodat =
					Vysledek = 0;

				if ((provider != null) && (provider is Fask.Server.Interfaces.Prijem.IPrijem))
				{
					return provider.Prijem_GetSkladExpedice(
						ITEMNMBR,
						MnozstviZadane,
						MnozstviNasnimane,
						out  MnozstviDodavatelePozadovano,
						out  MnozstviDodavateleDodano,
						out  MnozstviDodavateleDodat,
						out  MnozstviOdberateliPozadovano,
						out  MnozstviOdberatelumDodano,
						out  MnozstviOdberatelumDodat,
						out  Vysledek
					);
				}
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
			}

			string msg = "Provider v Prijem.asmx > 'Online_GetSkladExpedice(string ITEMNMBR,decimal MnozstviZadane,	decimal MnozstviNasnimane,out decimal MnozstviDodavatelePozadovano,out decimal MnozstviDodavateleDodano,out decimal MnozstviDodavateleDodat,out decimal MnozstviOdberateliPozadovano,out decimal MnozstviOdberatelumDodano,out decimal MnozstviOdberatelumDodat,out decimal Vysledek)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
			throw new Exception(msg);

		}


		/// <summary>
		/// Metoda sloužíci pro Storno pøijemky (Nastaví CZ_Doslo na 100)
		/// </summary>
		/// <param name="idterminal">ID Terminalu</param>
		/// <param name="idCountEntries">èíslo dávky</param>
		/// <param name="password">Heslo</param>
		/// <returns>StatusObject - Objekt ktery nese informace o stavu</returns>
		[WebMethod(Description = "Metoda sloužíci pro Storno pøijemky (Nastaví CZ_Doslo na 100)")]
		public StatusObject StornoPrijemka(byte idterminal, string idCountEntries, string password)
		{
			StatusObject processStatus = new StatusObject();


			try
			{
				if (provider != null)
				{
					Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();
					Fask.Server.Interfaces.Classes.Davka davka = new Davka();

					terminal.ID = idterminal;
					davka.ID = int.Parse(idCountEntries);
					//item.Type = itemtype;

					processStatus = provider.Prijem_Storno_Prijemka(
						davka,
						terminal,
						password
						);

					return processStatus;
				}
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				throw ex;
			}

			string msg = "Provider v Prijem.asmx > 'StornoPrijemka(byte idterminal, string idCountEntries, string password)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
			throw new Exception(msg);

		}

		/// <summary>
		/// Metoda která vratí pøijemky z pøedlohy podle ID Terminalu a Prefixu ID Skladu
		/// </summary>
		/// <param name="idterminal">ID Terminalu v CZ_Doslo</param>
		/// <param name="prefixskladu">ID Skladu SKL_ID like 'xxx%'</param>
		/// <returns>Dataset PrijemDavky najdenych pøijemek</returns>
		[WebMethod(Description = "Metoda která vratí pøijemky z pøedlohy podle ID Terminalu a Prefixu ID Skladu")]
		public Fask.DataSets.PrijemDavky GetPrijemky(byte idterminal, string prefixskladu)
		{


				try
				{
					if (provider != null && provider is Fask.Server.Interfaces.Prijem.IPrijem)
					{
						Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();
						Fask.Server.Interfaces.Classes.Sklad sklad = new Fask.Server.Interfaces.Classes.Sklad();
						Fask.Server.Interfaces.Classes.Item item = new Fask.Server.Interfaces.Classes.Item();

						terminal.ID = idterminal;
						sklad.ID = prefixskladu;
						//item.Type = itemtype;

						return provider.Prijem_GetPrijemky(
							terminal,
							sklad,
							item
							);
					}
				}
				catch (Exception ex)
				{
					Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
					throw ex;
				}

			string msg = "Provider v Prijem.asmx > 'GetPrijemky(byte idterminal, string prefixskladu)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
			throw new Exception(msg);

		}

		/// <summary>
		/// Metoda slouží pro nachystaní souboru pro terminál
		/// </summary>
		/// <param name="countentries">èíslo dávky</param>
		/// <param name="idterminal">ID Terminalu</param>
		/// <returns>True - Uspešne, False- neuspešne</returns>
		[WebMethod(Description = "Metoda slouží pro nachystaní souboru pro terminál")]
		public bool PreparePrijemkaDB(int countentries, byte idterminal)
		{
			try
			{

				string dstFile = Path.Combine(Fask.MyPath.Path.SQLiteDBsDirectory, idterminal.ToString() + Common.Backslash  + countentries.ToString() + PrijemDBFileExtension);

				if (!Directory.Exists(Path.GetDirectoryName(dstFile)))
					Directory.CreateDirectory(Path.GetDirectoryName(dstFile));

				SQLite_Helper helper = new SQLite_Helper();
				if (!helper.SQLite_CreateFile(dstFile, Common.Prijem))
				{
					throw new Exception("Nastala chyba pri tvorbe SQLite souboru pro " + Common.Prijem);
				}

				Fask.DataSets.Prijem prijem = GetPrijemka(countentries, idterminal);

				#region Puvodne rozdeleni

				//Ulozeni dat prijemky pro terminal
				//Predloha prijmu ...
				#region Puvodni ukladani

				//Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PETableAdapter peta = new Fask.MST_W_Server.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PETableAdapter();
				//peta.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + dstFile);
				//try
				//{
				//	peta.Connection.Open();
				//	var tran = peta.Connection.BeginTransaction();
				//	foreach (Fask.DataSets.Prijem.CZMST_PERow pr in prijem.CZMST_PE)
				//	{
				//		int rowinserted = peta.Insert(
				//			CountEntries: pr.CountEntries,
				//			PONUMBER: pr.PONUMBER.Trim(),
				//			ITEMNMBR: pr.IsITEMNMBRNull() ? "" : pr.ITEMNMBR.Trim(),
				//			ITEMDESC: pr.IsITEMDESCNull() ? "" : pr.ITEMDESC.Trim(),
				//			ORD: pr.ORD,
				//			VNDDOCNM: pr.IsVNDDOCNMNull() ? "" : pr.VNDDOCNM.Trim(),
				//			VNDITNUM: pr.IsVNDITNUMNull() ? "" : pr.VNDITNUM.Trim(),
				//			CZ_CarKod: String.IsNullOrEmpty(pr.CZ_CarKod) ? "" : pr.CZ_CarKod.Trim(),
				//			LOCNCODE: pr.IsLOCNCODENull() ? "" : pr.LOCNCODE.Trim(),
				//			QTYSHPPD: pr.QTYSHPPD,
				//			QTYPACK: pr.QTYPACK,
				//			CZ_DatVyr_Track: pr.CZ_DatVyr_Track,
				//			CZ_DatVyr_Delka: pr.CZ_DatVyr_Delka,
				//			CZ_SerNum_Track: pr.CZ_SerNum_Track,
				//			CZ_SerNum_Delka: pr.CZ_SerNum_Delka,
				//			CZ_SW_Track: pr.CZ_SW_Track,
				//			CZ_SW_Delka: pr.CZ_SW_Delka,
				//			CZ_Doslo: pr.CZ_Doslo,
				//			DEX_ROW_ID: pr.DEX_ROW_ID,
				//			Nasnimano: 0, //nasnimano ...
				//			MJ: pr.IsMJNull() ? "" : pr.MJ.Trim(),
				//			SKL_ID: pr.IsSKL_IDNull() ? "" : pr.SKL_ID.Trim(),
				//			WEIGHT: pr.IsWEIGHTNull() ? (decimal?)null : pr.WEIGHT,
				//			NMBRPAL: pr.IsNMBRPALNull() ? string.Empty : pr.NMBRPAL.Trim(),
				//			TYPEPAL: pr.IsTYPEPALNull() ? string.Empty : pr.TYPEPAL.Trim(),
				//			ITEMCODE: pr.IsITEMCODENull() ? string.Empty : pr.ITEMCODE.Trim(),
				//			SERLTNUM: pr.SERLTNUM.Trim(),
				//			CZ_REZ1_TRACK: pr.IsCZ_REZ1_TrackNull() ? (byte)0 : pr.CZ_REZ1_Track,
				//			CZ_REZ2_TRACK: pr.IsCZ_REZ2_TrackNull() ? (byte)0 : pr.CZ_REZ2_Track
				//			);
				//	}

				//	//Update nasnimano ...
				//	foreach (Fask.DataSets.Prijem.CZMST_PIRow pr in prijem.CZMST_PI)
				//	{
				//		peta.UpdateNasnimano(pr.QTYSHPPD, pr.PONUMBER.Trim(), pr.ITEMNMBR.Trim(), pr.ORD);
				//	}

				//	tran.Commit();

				//}
				//catch (Exception ex)
				//{
				//	Logging.ExceptionHandler2.Handle(ex);
				//}
				//finally
				//{
				//	if (peta != null)
				//	{
				//		if ((peta.Connection.State & ConnectionState.Open) == ConnectionState.Open)
				//			peta.Connection.Close();
				//		peta.Dispose();
				//	}
				//}

				#endregion

				#region Nove ukladani

				//try
				//{
				//	using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Prijem ConPrijem = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Prijem(dstFile))
				//	{

				//		ConPrijem.Update_PE(prijem.CZMST_PE);

				//		//Update nasnimano ...
				//		foreach (Fask.DataSets.Prijem.CZMST_PIRow pr in prijem.CZMST_PI)
				//		{
				//			ConPrijem.UpdateNasnimano_PE(pr.QTYSHPPD, pr.PONUMBER.Trim(), pr.ITEMNMBR.Trim(), pr.ORD);
				//		}
				//	}
				//}
				//catch (Exception ex)
				//{
				//	Logging.ExceptionHandler2.Handle(ex);
				//}

				#endregion

				//Predloha seriovych cisel/sarzi ...
				#region Puvodne ukladani

				//SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PE_SNTableAdapter pesnta = new Fask.MST_W_Server.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PE_SNTableAdapter();
				//pesnta.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + dstFile);
				//try
				//{
				//	pesnta.Connection.Open();
				//	var trans = pesnta.Connection.BeginTransaction();
				//	foreach (Fask.DataSets.Prijem.CZMST_PE_SNRow prsn in prijem.CZMST_PE_SN)
				//	{
				//		int rowinserted = pesnta.Insert(
				//			CountEntries: prsn.CountEntries,
				//			ITEMNMBR: prsn.ITEMNMBR.Trim(),
				//			SERLNMBR: prsn.SERLNMBR.Trim(),
				//			DEX_ROW_ID: prsn.DEX_ROW_ID
				//			);
				//	}
				//	trans.Commit();
				//}
				//catch (Exception ex)
				//{
				//	Logging.ExceptionHandler2.Handle(ex);
				//}
				//finally
				//{
				//	if (pesnta != null)
				//	{
				//		if ((pesnta.Connection.State & ConnectionState.Open) == ConnectionState.Open)
				//			pesnta.Connection.Close();
				//		pesnta.Dispose();
				//	}
				//}

				#endregion

				#region Nove ukladani

				//try
				//{
				//	using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Prijem ConPrijem = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Prijem(dstFile))
				//	{
				//		ConPrijem.Update_PE_SN(prijem.CZMST_PE_SN);
				//	}
				//}
				//catch (Exception ex)
				//{
				//	Logging.ExceptionHandler2.Handle(ex);
				//}

				#endregion

				//parametry pro rizeni prijmu z globalniho nastaveni na serveru ...
				#region Puvodne ukladani

				//SQLiteDBs.DataSets.PrijemTableAdapters.ParametryTableAdapter pta = new Fask.MST_W_Server.SQLiteDBs.DataSets.PrijemTableAdapters.ParametryTableAdapter();
				//pta.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + dstFile);
				//try
				//{
				//	pta.Connection.Open();
				//	var trans = pta.Connection.BeginTransaction();
				//	DataRow drow0 = (DataRow)prijem.Parametry[0];
				//	drow0.SetAdded();
				//	pta.Update(drow0);
				//	trans.Commit();
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

				#region nove ukladani

				//try
				//{

				//	using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Prijem ConPrijem = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Prijem(dstFile))
				//	{
				//		DataRow drow0 = (DataRow)prijem.Parametry[0];
				//		drow0.SetAdded();
				//		ConPrijem.Update_Param(drow0);
				//	}
				//}
				//catch (Exception ex)
				//{
				//	Logging.ExceptionHandler2.Handle(ex);
				//}

				#endregion

				#endregion

				#region Pøehledne

				try
				{
					using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Prijem ConPrijem = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Prijem(dstFile))
					{

						prijem.CZMST_PE.ToList().ForEach(x => x.SetAdded());

						ConPrijem.Update_PE(prijem.CZMST_PE);

						//Update nasnimano ...
						foreach (Fask.DataSets.Prijem.CZMST_PIRow pr in prijem.CZMST_PI)
						{
							ConPrijem.UpdateNasnimano_PE(pr.QTYSHPPD, pr.PONUMBER.Trim(), pr.ITEMNMBR.Trim(), pr.ORD);
						}

						prijem.CZMST_PE_SN.ToList().ForEach(x => x.SetAdded());

						ConPrijem.Update_PE_SN(prijem.CZMST_PE_SN);

						DataRow drow0 = (DataRow)prijem.Parametry[0];
						drow0.SetAdded();
						ConPrijem.Update_Param(drow0);

						ConPrijem.Shrink();
					}
				}
				catch (Exception ex)
				{
					Logging.ExceptionHandler2.Handle(ex);
					throw ex;
				}


				#endregion


				Fask.Compressing.Zip.Compress(dstFile);

				return true;

			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, " (Dávka: " + countentries.ToString() + ", Terminál ID:" + idterminal.ToString() + ")");
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				throw ex;
			}
		}

		/// <summary>
		/// Metoda slouží pro oznaèení davku za uspešne staženou pro daný terminal
		/// </summary>
		/// <param name="countentries">èíslo dávky</param>
		/// <param name="idterminal">ID Terminalu</param>
		/// <returns>True- oznaèeno, False- chyba</returns>
		[WebMethod(Description = "Metoda slouží pro oznaèení davku za uspešne staženou pro daný terminal")]
		public bool GetPrijemkaReceived(int countentries, byte idterminal)
		{
			try
			{
				if (provider != null)
				{
					Fask.Server.Interfaces.Classes.Davka davka = new Fask.Server.Interfaces.Classes.Davka();
					Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();
					Fask.Server.Interfaces.Classes.Sklad sklad = new Fask.Server.Interfaces.Classes.Sklad();
					Fask.Server.Interfaces.Classes.Item item = new Fask.Server.Interfaces.Classes.Item();

					davka.ID = countentries;
					terminal.ID = idterminal;

					return provider.Prijem_GetPrijemkaReceived(
						davka,
						terminal,
						sklad,
						item
						);
				}
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				throw ex;
			}

			string msg = "Provider v Prijem.asmx > 'GetPrijemkaReceived(int countentries, byte idterminal)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
			throw new Exception(msg);

		}

		/// <summary>
		/// Metoda sloužící pro rozbalení ZIP souboru, a vytažení dat z souboru do pameti. Nasledne zavolá metodu ProcessPrijemka která zpracuje data. 
		/// </summary>
		/// <param name="countentries">CountEntries</param>
		/// <param name="idterminal">ID Terminalu</param>
		/// <param name="processState">pøiznam co se ma stat: Zpracovat, Uvolnit</param>
		/// <returns>StatusObject - Objektr který vraci informace o stavu</returns>
		[WebMethod(Description = "Metoda sloužící pro rozbalení ZIP souboru, a vytažení dat z souboru do pameti. Nasledne zavolá metodu ProcessPrijemka která zpracuje data. ")]
		public StatusObject ProcessPrijemDBFile2(int countentries, byte idterminal, ProcessPrijemState processState)
		{
			TracId tracid = new TracId(null, idterminal, countentries);
			StatusObject so = new StatusObject();

			#region trace
			Trac.Write("ProcessPrijemFile START, ProcessPrijemState: " + processState.ToString(), tracid);
			#endregion

			if (!isLicenseValid())
			{
				so.StatusText = "Licence na serveru není validní!";
				so.Exception = true;
				return so;
			}

			//Ulozit data prijemky
			string dstFile = Path.Combine(Fask.MyPath.Path.SQLiteDBsDirectory, idterminal.ToString() + @"\" + countentries.ToString() + PrijemDBFileExtension);
			string dstFileZip = dstFile + Common.ZIP; 

			#region trace
			Trac.Write("DB FileStream start", "ProcessPrijemDBFile", tracid);
			#endregion

			//odzipovat
			Fask.Compressing.Zip.Decompress(dstFileZip);



			#region trace
			Trac.Write("DB FileStream end", "ProcessPrijemDBFile", tracid);
			#endregion

			try
			{
				Fask.DataSets.Prijem prijemData = new Fask.DataSets.Prijem();
				Fask.SQLiteDBs.DataSets.Prijem prijemDataCE = new Fask.SQLiteDBs.DataSets.Prijem();

				//SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PETableAdapter peta = null;
				//SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PITableAdapter pita = null;
				//SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PEHTableAdapter pehta = null;
				//SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PI_FTableAdapter pifta = null;
				//SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PIHTableAdapter pihta = null;
				//SQLiteDBs.DataSets.PrijemTableAdapters.ParametryTableAdapter paramsta = null;

				//System.Data.Common.DbTransaction tran = null;
				//System.Data.SQLite.SQLiteConnection sQLiteConnection = null;

				try
				{
					//sQLiteConnection = new System.Data.SQLite.SQLiteConnection("Data source=" + dstFile);

					//peta = new Fask.MST_W_Server.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PETableAdapter();
					//pita = new Fask.MST_W_Server.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PITableAdapter();
					//pehta = new Fask.MST_W_Server.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PEHTableAdapter();
					//pifta = new Fask.MST_W_Server.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PI_FTableAdapter();
					//pihta = new Fask.MST_W_Server.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PIHTableAdapter();
					//paramsta = new Fask.MST_W_Server.SQLiteDBs.DataSets.PrijemTableAdapters.ParametryTableAdapter();

					//peta.Connection = sQLiteConnection;
					//pita.Connection = peta.Connection;
					//pehta.Connection = peta.Connection;
					//pifta.Connection = peta.Connection;
					//pihta.Connection = peta.Connection;
					//paramsta.Connection = peta.Connection;

					//sQLiteConnection.Open();
					//tran = peta.Connection.BeginTransaction();

					//peta.Fill(prijemDataCE.CZMST_PE);
					//pita.Fill(prijemDataCE.CZMST_PI);
					//pehta.Fill(prijemDataCE.CZMST_PEH);				
					//pifta.Fill(prijemDataCE.CZMST_PI_F);
					//pihta.Fill(prijemDataCE.CZMST_PIH);
					//paramsta.Fill(prijemDataCE.Parametry);

					using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Prijem ConPri = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Prijem(dstFile))
					{
						ConPri.Fill_PE(prijemDataCE.CZMST_PE);
						ConPri.Fill_PI(prijemDataCE.CZMST_PI);
						ConPri.Fill_PEH(prijemDataCE.CZMST_PEH);
						ConPri.Fill_PIF(prijemDataCE.CZMST_PI_F);
						ConPri.Fill_PIH(prijemDataCE.CZMST_PIH);
						ConPri.Fill_Param(prijemDataCE.Parametry);
					}

					//tran.Commit();

					//sQLiteConnection.Close();
					//sQLiteConnection = null;

				}
				catch (Exception ex)
				{
					//try
					//{
					//	tran?.Rollback();
					//}
					//catch (Exception exRollback)
					//{
					//	Logging.ExceptionHandler2.Handle(exRollback);
					//}

					Logging.ExceptionHandler2.Handle(ex);
					throw ex;
				}
				finally
				{
					//if (peta != null)
					//{
					//	if ((peta.Connection.State & ConnectionState.Open) == ConnectionState.Open)
					//		peta.Connection.Close();
					//	peta.Dispose();
					//}

					//if (pita != null)
					//{
					//	if ((pita.Connection.State & ConnectionState.Open) == ConnectionState.Open)
					//		pita.Connection.Close();
					//	pita.Dispose();
					//}

					//if (pehta != null)
					//{
					//	if ((pehta.Connection.State & ConnectionState.Open) == ConnectionState.Open)
					//		pehta.Connection.Close();
					//	pehta.Dispose();
					//}

					//if (pifta != null)
					//{
					//	if ((pifta.Connection.State & ConnectionState.Open) == ConnectionState.Open)
					//		pifta.Connection.Close();
					//	pifta.Dispose();
					//}

					//if (pihta != null)
					//{
					//	if ((pihta.Connection.State & ConnectionState.Open) == ConnectionState.Open)
					//		pihta.Connection.Close();
					//	pihta.Dispose();
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




				prijemData.CZMST_PE.BeginLoadData();
				foreach (Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow per in prijemDataCE.CZMST_PE)
				{
					prijemData.CZMST_PE.AddCZMST_PERow(
						per.CountEntries,
						per.PONUMBER,
						per.ITEMNMBR,
						per.ITEMDESC,
						per.ORD,
						per.VNDDOCNM,
						per.VNDITNUM,
						per.CZ_CarKod,
						per.IsSKL_IDNull() ? string.Empty : per.SKL_ID,
						per.LOCNCODE,
						per.MJ,
						per.QTYSHPPD,
						per.QTYPACK,
						per.CZ_DatVyr_Track,
						per.CZ_DatVyr_Delka,
						per.CZ_SerNum_Track,
						per.CZ_SerNum_Delka,
						per.CZ_SW_Track,
						per.CZ_SW_Delka,
						per.CZ_Doslo,
						per.IsWEIGHTNull() ? 0 : per.WEIGHT,
						per.IsNMBRPALNull() ? string.Empty : per.NMBRPAL,
						per.IsTYPEPALNull() ? string.Empty : per.TYPEPAL,
						per.IsITEMCODENull() ? string.Empty : per.ITEMCODE,
						per.SERLTNUM,
						per.CZ_REZ1_TRACK,
						per.CZ_REZ2_TRACK,
						per.CZ_Expirace_Track,
						per.Nasnimano
						);
				}
				prijemData.CZMST_PE.EndLoadData();				

				prijemData.CZMST_PI.BeginLoadData();
				foreach (Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIRow pir in prijemDataCE.CZMST_PI)
				{

					prijemData.CZMST_PI.ImportRow(pir);

					//prijemData.CZMST_PI.AddCZMST_PIRow(
					//	pir.CountEntries,
					//	pir.PONUMBER,
					//	pir.ORD,
					//	pir.IsITEMNMBRNull() ? string.Empty : pir.ITEMNMBR,
					//	pir.IsVNDDOCNMNull() ? string.Empty : pir.VNDDOCNM,
					//	pir.IsVNDITNUMNull() ? string.Empty : pir.VNDITNUM,
					//	pir.IsSKL_IDNull() ? string.Empty : pir.SKL_ID,
					//	pir.IsLOCNCODENull() ? string.Empty : pir.LOCNCODE,
					//	pir.IsMJNull() ? string.Empty : pir.MJ,
					//	pir.QTYSHPPD,
					//	pir.QTYSHPPDMJ,
					//	pir.QTYPACK,
					//	pir.SERLTNUM,
					//	pir.IsKOD_SWNull() ? string.Empty : pir.KOD_SW,
					//	pir.IsDAT_VYROBYNull() ? string.Empty : pir.DAT_VYROBY,
					//	pir.IsDATEDONENull() ? string.Empty : pir.DATEDONE,
					//	pir.IsTIMEDONENull() ? string.Empty : pir.TIMEDONE,
					//	pir.IsCZ_CarKodNull() ? string.Empty : pir.CZ_CarKod,
					//	pir.IsREZ_1Null() ? string.Empty : pir.REZ_1,
					//	pir.IsREZ_2Null() ? string.Empty : pir.REZ_2,
					//	pir.USER_ID,
					//	pir.guid,
					//	pir.INPUT_MODE,
					//	pir.ID_TERMINAL,
					//	pir.IsWEIGHTNull() ? 0 : pir.WEIGHT,
					//	pir.IsNMBRPALNull() ? string.Empty : pir.NMBRPAL,
					//	pir.IsTYPEPALNull() ? string.Empty : pir.TYPEPAL,
					//	pir.IsITEMCODENull() ? string.Empty : pir.ITEMCODE,
					//	//pir.IsExpiraceNull() ? DateTime.Now  : pir.Expirace
					//	);
				}
				prijemData.CZMST_PI.EndLoadData();


				prijemData.CZMST_PEH.BeginLoadData();
				foreach (Fask.SQLiteDBs.DataSets.Prijem.CZMST_PEHRow pehr in prijemDataCE.CZMST_PEH)
				{
					prijemData.CZMST_PEH.AddCZMST_PEHRow(pehr.CountEntries, pehr.GUID);
				}
				prijemData.CZMST_PEH.EndLoadData();

				prijemData.CZMST_PI_F.BeginLoadData();
				foreach (Fask.SQLiteDBs.DataSets.Prijem.CZMST_PI_FRow pifr in prijemDataCE.CZMST_PI_F)
				{
					prijemData.CZMST_PI_F.AddCZMST_PI_FRow(pifr.IMG_NAME, pifr.GUID);
				}
				prijemData.CZMST_PI_F.EndLoadData();

				prijemData.CZMST_PIH.BeginLoadData();
				foreach (var item in prijemDataCE.CZMST_PIH)
				{
					var nitem = prijemData.CZMST_PIH.NewCZMST_PIHRow();
					nitem.CountEntries = item.CountEntries;
					if (item.IsDATUMDOKLADUNull())
						nitem.SetDATUMDOKLADUNull();
					else
						nitem.DATUMDOKLADU = item.DATUMDOKLADU;
					prijemData.CZMST_PIH.AddCZMST_PIHRow(nitem);
				}
				prijemData.CZMST_PIH.EndLoadData();


				prijemData.Parametry.BeginLoadData();
				foreach (Fask.SQLiteDBs.DataSets.Prijem.ParametryRow pir in prijemDataCE.Parametry)
				{
					prijemData.Parametry.AddParametryRow(
						pir.CONFIG_DUPLIC_SN,
						pir.CONFIG_KONT_DELKA,
						pir.CONFIG_KONT_DOKONCENOSTI,
						pir.CONFIG_KONT_NUL_DELKA,
						pir.CONFIG_KONT_UPL_POL,
						pir.CONFIG_LISTSNIM,
						pir.CONFIG_NOVA_KARTA,
						pir.CONFIG_POKRDOHLED,
						pir.CONFIG_PRIM_KEY1,
						pir.CONFIG_PTATSE_NEANO,
						pir.CONFIG_SNIM_LOCNCODE,
						pir.CONFIG_SNIM_PONUMBER,
						pir.CONFIG_SNIM_REZ2,
						pir.CONFIG_SNIMAT_POL1,
						pir.CONFIG_SNIMAT_POL2,
						pir.CONFIG_SNIMAT_POL3,
						pir.CONFIG_SNIMATZADAT_SN,
						pir.CONFIG_ZADAT_MN_POKAZDE,
						pir.ENABLE_LISTSNIM,
						pir.ENABLE_SNIMATZADAT_SN,
						pir.CONFIG_MNOZSTVI_PREDVYPLNIT,
						pir.CONFIG_MNOZSTVI_PREDVYPLNIT_JEDNA,
						pir.CONFIG_MNOZSTVI_PREDVYPLNIT_ZBYVAJICI,
						pir.CONFIG_MNOZSTVI_ZADAVAT,
						pir.CONFIG_MNOZSTVI_SCANNEREM,
						pir.IsCONFIG_LOKACE_POVOLITNull() ? false : pir.CONFIG_LOKACE_POVOLIT,
						pir.IsCONFIG_LOKACE_TIMEOUTNull() ? 0 : pir.CONFIG_LOKACE_TIMEOUT,
						pir.IsCONFIG_LOKACE_PRIJMOVANull() ? false : pir.CONFIG_LOKACE_PRIJMOVA
						);
				}
				prijemData.Parametry.EndLoadData();


				prijemData.AcceptChanges();

				prijemData.CZMST_PE.ToList().ForEach(x => x.SetModified());			//toto je kvuli tomu, ze se chce provest modify pro sloupec cz_doslo na cislo terminalu
				//prijemData.CZMST_PEH.ToList().ForEach(x => x.SetModified());
				//prijemData.CZMST_PE_SN.ToList().ForEach(x => x.SetModified());
				prijemData.CZMST_PI.ToList().ForEach(x => x.SetAdded());			// toto kvuli insertu do cilove db
				prijemData.CZMST_PIH.ToList().ForEach(x => x.SetAdded());			// toto kvuli insertu do cilove db
				prijemData.CZMST_PI_F.ToList().ForEach(x => x.SetAdded());          // toto kvuli insertu do cilove db

				#region trace
				Trac.Write("ProcessPrijemka(countentries, idterminal, prijemData, processState) begin", "ProcessPrijemFile", tracid);
				#endregion

				so = ProcessPrijemka(countentries, idterminal, prijemData, processState);

				#region trace
				Trac.Write("ProcessPrijemka(countentries, idterminal, prijemData, processState) end, StatusText: " + so.StatusText, "ProcessPrijemFile", tracid);
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
				Trac.Write("ProcessPrijemFile END, ProcessPrijemState: " + processState.ToString(), tracid);
				#endregion

				return so;
			}
			catch (Exception ex)
			{
				#region trace
				Trac.Write(ex, "ProcessPrijemFile END, ProcessPrijemState: " + processState.ToString(), tracid);
				#endregion

				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

				Routines.ManageDataFiles.Move(dstFile, Path.Combine(Fask.MyPath.Path.ErrorDataFileDirectory, Path.GetFileName(dstFile)));


				so.StatusText = ex.Message;
				so.Exception = true;
				return so;

				throw Routines.Exceptions.CustomSoapException("Process", ex.Message, "Prijem");

			}
			finally
			{
				#region trace
				Trac.Write("ProcessPrijemFile END, finally", tracid);
				#endregion

				try
				{
					//Log.writeOKData(prijemDBFile, countentries.ToString() + "." + idterminal.ToString() + "." + "prijem");
					File.Delete(dstFile);
				}
				catch (Exception ex)
				{
					Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				}
			}



		}

		#region Detaily

		/// <summary>
		/// Metoda která vrací Netypovy dataset podle dotazu v konfiguraci
		/// </summary>
		/// <param name="ponumber">èíslo objednavky</param>
		/// <param name="skl_id">ID Skladu</param>
		/// <returns>Netypovy dataset podle dotazu v konfiguraci</returns>
		[WebMethod(Description = "Metoda která vrací Netypovy dataset podle dotazu v konfiguraci")]
		public DataSet Detail(string ponumber, string skl_id)
		{
			try
			{
				#region trace
				Trac.Write("Detail BEGIN");
				#endregion

				if (provider != null && provider is Fask.Server.Interfaces.Prijem.IPrijem)
				{
					Objednavka objednavka = new Objednavka();
					objednavka.ID = ponumber;
					Sklad sklad = new Sklad();
					sklad.ID = skl_id;

					#region trace
					Trac.Write("Detail END");
					#endregion

					return provider.Prijem_Detail(objednavka, sklad);
				}

			}
			catch (Exception ex)
			{
				#region trace
				Trac.Write("Detail END, exception");
				#endregion

				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, " (PrijemkaDetail Doklad:" + ponumber + ", Sklad:" + skl_id + ")");
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

				throw ex;
			}


			string msg = "Provider v Prijem.asmx > 'Detail(string ponumber, string skl_id)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
			throw new Exception(msg);
		}

		/// <summary>
		/// Metoda kter vraci netypovy dataset podle dotazu v konfiguraci 
		/// </summary>
		/// <param name="ponumber">èíslo objednavky</param>
		/// <param name="skl_id">ID Skladu</param>
		/// <param name="itemnumber">ID položky</param>
		/// <param name="orderid">ORD ID, ordinery, ID øadku??</param>
		/// <param name="serltnum">Seriove èislo/ šarže</param>
		/// <returns>Netypovy dataset podle dotazu v konfiguraci</returns>
		[WebMethod(Description = "Metoda kter vraci netypovy dataset podle dotazu v konfiguraci")]
		public DataSet DetailItem(string ponumber, string skl_id, string itemnumber, string orderid, string serltnum)
		{
			try
			{
				if (provider != null && provider is Fask.Server.Interfaces.Prijem.IPrijem)
				{
					Objednavka objednavka = new Objednavka();
					Sklad sklad = new Sklad();
					Item item = new Item();

					objednavka.ID = ponumber;
					sklad.ID = skl_id;
					item.ID = itemnumber;
					item.Order = orderid;
					item.Serltnum = serltnum;

					return provider.Prijem_Detail_Polozka(objednavka, sklad, item);
				}

			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, " (PrijemkaDetailItem Doklad:" + ponumber + ", Sklad:" + skl_id + ", Item:" + itemnumber + ", Ord:" + orderid + ", SN:" + serltnum + ")");
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

				throw ex;
			}

			string msg = "Provider v Prijem.asmx > 'DetailItem(string ponumber, string skl_id, string itemnumber, string orderid, string serltnum)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
			throw new Exception(msg);
		}
		
		#endregion

		#region Generovani davky z terminalu pro Pøijem

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
		/// <param name="ponumber">èíslo objednavky</param>
		/// <param name="skl_id">ID Skladu</param>
		/// <returns>StatusObjekt - informace o zpracování</returns>
		[WebMethod(Description = "Metoda která zapoène pøipravu generovaní pøedlohy IS do Našich struktur")]
		public StatusObject GenerateDavkaRequest(string ponumber, string skl_id)
		{
			StatusObject so = StatusObject.Create(MyPath.Path.StatusObjectDirectory, StatusObject.Operations.PrijemGenerateDavka, ponumber, skl_id);
			if (so.Exists)
			{
				return so; //vrati informaci o stavu provedeni. Pokud se chce generovat znovu, tak se musi nejprve provest smazani statusu ... 
			}
			// kdyz neexistuje, tak je to novy pozadavek ...

			so.Finished = so.Exception = false;
			so.StatusText = "Pøíprava dávky dokladu '" + ponumber + "'";
			so.Write();

			// Vyvolani noveho threadu ...
			new System.Threading.Thread(() => GenerateDavkaWrapper(ponumber, skl_id, ref so)).Start();

			return so;
		}

		/// <summary>
		/// Metoda která provadí samotne generování dokladu z IS do našich struktur
		/// </summary>
		/// <param name="ponumber">èíslo objednavky</param>
		/// <param name="skl_id">ID Skladu</param>
		/// <returns>ID chyby</returns>
		[WebMethod(Description = "Metoda která provadí samotne generování dokladu z IS do našich struktur")]
		public int GenerateDavka(string ponumber, string skl_id)
		{
			string statusinfo = string.Empty;

			try
			{
				if (provider != null)
				{
					Objednavka objednavka = new Objednavka();
					Sklad sklad = new Sklad();

					objednavka.ID = ponumber;
					objednavka.CisloDavky = string.Empty;

					sklad.ID = skl_id;

					StatusInfo si = provider.Prijem_GenerateDavka(objednavka, sklad);


					string S = string.Format("Prijem, GenerateDavka > DatumVzniku:'{0}'" + 
						Environment.NewLine +
						 "ID:'{1}'" +
						 Environment.NewLine +
						 "Popis:'{2}'"
						, si.Created, si.ID, si.Description);

					Logging.ExceptionHandler2.Handle(LogLevel.Info, S);

					return si.ID;
				}
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(LogLevel.Error, this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, "Ponumber:'" + ponumber + "'");
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				throw ex;
			}

			string msg = "Provider v Prijem.asmx > 'GenerateDavka(string ponumber, string skl_id)' nenastaven.";
			//Log.writeErrorLog(msg);
			Fask.Logging.ExceptionHandler2.Handle(LogLevel.NotImplemented, msg);
			throw new Exception(msg);
		}

		/// <summary>
		/// Metoda která vrací status o prubehu generování
		/// </summary>
		/// <param name="ponumber">èíslo objednavky</param>
		/// <param name="skl_id">ID Skladu</param>
		/// <returns>StatusObjekt - informace o zpracování</returns>
		[WebMethod(Description = "Metoda která vrací status o prubehu generování")]
		public StatusObject GenerateDavkaStatus(string ponumber, string skl_id)
		{
			StatusObject so = StatusObject.Create(MyPath.Path.StatusObjectDirectory, StatusObject.Operations.PrijemGenerateDavka, ponumber, skl_id);
			if (so.Exists)
				return so;
			else
				return null;
		}

		/// <summary>
		/// Metoda slouzi terminalu ke smazani statusu a znovu generovani prikazu(davky)
		/// </summary>
		/// <param name="ponumber">èíslo objednavky</param>
		/// <param name="skl_id">ID SKladu</param>
		/// <returns>StatusObjekt - informace o zpracování</returns>
		[WebMethod(Description = "Metoda slouzi terminalu ke smazani statusu a znovu generovani prikazu(davky)")]
		public StatusObject GenerateDavkaStatusDelete(string ponumber, string skl_id)
		{
			StatusObject so = StatusObject.Create(MyPath.Path.StatusObjectDirectory, StatusObject.Operations.PrijemGenerateDavka, ponumber, skl_id);
			so.Delete();
			return so;
		}

		#endregion

		#region Online NEIMPLEMENTOVANE ani v SQL ani v POHODE

		/// <summary>
		/// Metoda pravdepodobne pro praci s Lok. Mechanizmem, pøidávani položky
		/// </summary>
		/// <param name="countentries">èíslo dávky</param>
		/// <param name="idterminal">ID Terminalu</param>
		/// <param name="prijemRows">øádky pro pøidání</param>
		/// <returns>StatusObject - Objekt ktery nese informace</returns>
		[WebMethod(Description = "Metoda pravdepodobne pro praci s Lok. Mechanizmem, pøidávani položky")]
		public StatusObject Online_Add(int countentries, byte idterminal, Fask.DataSets.Prijem prijemRows)
		{
			StatusObject processStatus = new StatusObject();


				try
				{
					if (provider != null && provider is Fask.Server.Interfaces.Prijem.IPrijem)
					{
						Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();
						Fask.Server.Interfaces.Classes.Davka davka = new Davka();

						terminal.ID = idterminal;
						davka.ID = countentries;

						processStatus = provider.Prijem_Online_Add(
							davka,
							terminal,
							prijemRows
							);

						return processStatus; 
					}
				}
				catch (Exception ex)
				{
					Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
					throw ex;
				}

				string msg = "Provider v Prijem.asmx > 'Online_Add(int countentries, byte idterminal, Fask.DataSets.Prijem prijemRows)' nenastaven.";
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
				throw new Exception(msg);
			
		}

		/// <summary>
		/// Metoda pravdepodobne pro praci s Lok. Mechanizmem, mazani položky
		/// </summary>
		/// <param name="countentries">èíslo dávky</param>
		/// <param name="idterminal">ID Terminalu</param>
		/// <param name="prijemRows">øádky pro pøidání</param>
		/// <returns>StatusObject - Objekt ktery nese informace</returns>
		[WebMethod(Description = "Metoda pravdepodobne pro praci s Lok. Mechanizmem, mazani položky")]
		public StatusObject Online_Del(int countentries, byte idterminal, Fask.DataSets.Prijem prijemRows)
		{
			StatusObject processStatus = new StatusObject();


				try
				{
					if (provider != null && provider is Fask.Server.Interfaces.Prijem.IPrijem)
					{
						Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();
						Fask.Server.Interfaces.Classes.Davka davka = new Davka();

						terminal.ID = idterminal;
						davka.ID = countentries;

						processStatus = provider.Prijem_Online_Del(
							davka,
							terminal,
							prijemRows
							);

						return processStatus;
					}
				}
				catch (Exception ex)
				{
					Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
					throw ex;
				}

				string msg = "Provider v Prijem.asmx > 'Online_Del(int countentries, byte idterminal, Fask.DataSets.Prijem prijemRows)' nenastaven.";
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
				throw new Exception(msg);
		}


		/// <summary>
		/// Metoda pravdepodobne pro praci s Lok. Mechanizmem, neco s množstvím
		/// </summary>
		/// <param name="countentries">èíslo dávky</param>
		/// <param name="idterminal">ID Terminalu</param>
		/// <param name="prijemRows">reference na øádky pro pøidání</param>
		/// <returns>StatusObject - Objekt ktery nese informace</returns>
		[WebMethod(Description = "Metoda pravdepodobne pro praci s Lok. Mechanizmem, neco s množstvím")]
		public StatusObject Online_Quantity(int countentries, byte idterminal, ref Fask.DataSets.Prijem prijemRows)
		{
			StatusObject processStatus = new StatusObject();


				try
				{
					if (provider != null)
					{
						Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();
						Fask.Server.Interfaces.Classes.Davka davka = new Davka();

						terminal.ID = idterminal;
						davka.ID = countentries;

						processStatus = provider.Prijem_Online_Quantity(
							davka,
							terminal,
							ref prijemRows
							);

						return processStatus;
					}
				}
				catch (Exception ex)
				{
					Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
					throw ex;
				}
				string msg = "Provider v Prijem.asmx > 'Online_Quantity(int countentries, byte idterminal, ref Fask.DataSets.Prijem prijemRows)' nenastaven.";
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
				throw new Exception(msg);
		}

		/// <summary>
		/// Metoda pro zavolaní procedury Prijem_GenerateSerltnum_Action
		/// </summary>
		/// <param name="itemnmbr">ID položky</param>
		/// <param name="skl_id">ID Skladu</param>
		/// <param name="oldSerltnum">staré seriove èislo/šarže</param>
		/// <returns>Nove seriove èislo/šarže</returns>
		[WebMethod(Description = "Metoda pro zavolaní procedury Prijem_GenerateSerltnum_Action")]
		public string Online_GenerateSerltnum(string itemnmbr, string skl_id, string oldSerltnum)
		{
            try
            {
                if (provider != null && provider is Fask.Server.Interfaces.Prijem.IPrijem)
                {
                    return provider.Online_GenerateSerltnum(itemnmbr, skl_id, oldSerltnum);
                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            string msg = "Provider v Prijem.asmx > 'Online_GenerateSerltnum(string itemnmbr, string skl_id, string oldSerltnum)' nenastaven.";
            Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
            throw new Exception(msg);
        }

		#endregion

		/// <summary>
		/// Metoda pro Online overeni lokace
		/// </summary>
		/// <param name="serltnum">seriove èíslo/ šarže</param>
		/// <param name="itemnmbr">ID položky</param>
		/// <param name="locncode">lokace</param>
		/// <param name="qtyshppd">množstvi</param>
		/// <param name="skl_id">ID Skladu</param>
		/// <returns>StatusOverLokace - Objekt ktery nese stav dotazu o lokaci </returns>
		[WebMethod(Description = "Metoda pro Online overeni lokace")]
		public StatusOverLokace Online_OverLokace(string serltnum, string itemnmbr, string locncode, decimal qtyshppd, string skl_id)
		{

			try
			{
				if (provider != null && provider is Fask.Server.Interfaces.Prijem.IPrijem)
				{
					return provider.Prijem_Online_OverLokace(serltnum, itemnmbr, locncode, qtyshppd, skl_id);
				}

			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				throw ex;
			}
			string msg = "Provider v Prijem.asmx > 'Online_OverLokace(string serltnum, string itemnmbr, string locncode, decimal qtyshppd, string skl_id)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
			throw new Exception(msg);

		}

		/// <summary>
		/// Metoda která vraci nerealizovane pøijemky, dle èarovych kodu.
		/// </summary>
		/// <param name="terminalid">ID terminalu</param>
		/// <param name="skladid">ID Skladu</param>
		/// <param name="itemtype">Typ položky</param>
		/// <param name="ListCarKod">List èarovych kodu</param>
		/// <returns>Naplneni dataset Obecne</returns>
		[WebMethod(Description = "Metoda která vraci nerealizovane pøijemky, dle èarovych kodu.")]
		public Fask.Server.Interfaces.DataSets.Obecne Online_GetNezrealizovanePrijemky_Vyber(byte terminalid, string skladid, string itemtype, List<string> ListCarKod)
		{
			try
			{
				if ((provider != null) && (provider is Fask.Server.Interfaces.Prijem.IPrijem))
				{
					Terminal terminal = new Terminal();
					Sklad sklad = new Sklad();
					Item item = new Item();

					terminal.ID = terminalid;
					sklad.ID = skladid;
					item.Type = itemtype;

					return provider.Prijem_GetPrijemky_External(terminal, sklad, item, ListCarKod);
				}
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				throw ex;
			}

			string msg = "Provider v Prijem.asmx > 'Online_GetNezrealizovanePrijemky_Vyber(byte terminalid, string skladid, string itemtype, List<string> ListCarKod)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
			throw new Exception(msg);

		}

		/// <summary>
		/// Metoda která zavolá proceduru  Prijem_GetDoporuceneLokace_Action a vrati doporuèene lokace
		/// </summary>
		/// <param name="itemnmbr">ID položky</param>
		/// <param name="skl_id">ID Skladu</param>
		/// <param name="serltnum">seriove èislo/ šarže</param>
		/// <returns>Naplneni dataset Obecne</returns>
		[WebMethod(Description = "Metoda která zavolá proceduru Prijem_GetDoporuceneLokace_Action a vrati doporuèene lokace")]
		public Fask.Server.Interfaces.DataSets.Obecne Online_GetDoporuceneLokace(string itemnmbr, string skl_id, string serltnum)
		{
            try
            {
                if (provider != null && provider is Fask.Server.Interfaces.Prijem.IPrijem)
                {
                    return provider.Online_GetDoporuceneLokace( itemnmbr, skl_id, serltnum);
                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            string msg = "Provider v Prijem.asmx > 'Online_GetDoporuceneLokace(string itemnmbr, string skl_id, string serltnum)' nenastaven.";
            Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
            throw new Exception(msg);

        }

		/// <summary>
		/// Metoda která zavola proceduru Prijem_GetNezrealizovanePrijemky_Action a vrati nerealizovane prjemky
		/// </summary>
		/// <returns>Naplneni dataset Obecne</returns>
		[WebMethod(Description = "Metoda která zavola proceduru Prijem_GetNezrealizovanePrijemky_Action a vrati nerealizovane prjemky")]
		public Fask.Server.Interfaces.DataSets.Obecne Online_GetNezrealizovanePrijemky()
		{
            try
            {
                if (provider != null && provider is Fask.Server.Interfaces.Prijem.IPrijem)
                {
                    return provider.Online_GetNezrealizovanePrijemky();
                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            string msg = "Provider v Prijem.asmx > 'Online_GetNezrealizovanePrijemky()' nenastaven.";
            Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
            throw new Exception(msg);
        }
		
		#endregion

		#region Privatne metody

		/// <summary>
		/// Metoda pro kontrolu licence
		/// </summary>
		/// <returns>True - validni, False - nevalidni</returns>
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
		/// Metoda která vratí jednu pøijemku pro metodu která pøipravuje soubor pro terminal
		/// </summary>
		/// <param name="countentries">èíslo dávky</param>
		/// <param name="idterminal">ID Terminalu</param>
		/// <returns>Dataset Prijem naplnení data pøíjemky</returns>
		public Fask.DataSets.Prijem GetPrijemka(int countentries, byte idterminal)
		{
			Fask.DataSets.Prijem prijem = null;


				try
				{
                if (provider != null && provider is Fask.Server.Interfaces.Prijem.IPrijem)
                {
                    Fask.Server.Interfaces.Classes.Davka davka = new Fask.Server.Interfaces.Classes.Davka();
                    Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();
                    Fask.Server.Interfaces.Classes.Sklad sklad = new Fask.Server.Interfaces.Classes.Sklad();
                    Fask.Server.Interfaces.Classes.Item item = new Fask.Server.Interfaces.Classes.Item();

                    davka.ID = countentries;
                    terminal.ID = idterminal;

                    prijem = provider.Prijem_GetPrijemka(
                        davka,
                        terminal,
                        sklad,
                        item
                        );

                    // \bug je to nutne tady když se to dotahuje i v provider? 
                    //prijem.ReadXml(Fask.MyPath.Path.PrijemParams);

                    Konfigurace.Classes.Globals_Konfig_Agendy.LoadConfiguration();
                    prijem.Parametry.ImportRow(Konfigurace.Classes.Globals_Konfig_Agendy.Konfigurace.PrijemParametry[0]);
                    prijem.AcceptChanges();

                    return prijem;
                }

				}
				catch (Exception ex)
				{
					Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
					throw ex;
				}
			

			string msg = "Provider v Prijem.asmx > 'GetPrijemky(byte idterminal, string prefixskladu)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
			throw new Exception(msg);

		}

		/// <summary>
		/// Metoda sloužící pro zpracovaní, Uložení do SQL serveru a prace nad IS
		/// </summary>
		/// <param name="countentries">èíslo dávky</param>
		/// <param name="idterminal">ID Terminalu</param>
		/// <param name="prijemdata">Data na zpracování</param>
		/// <param name="processState">pøiznam co se ma stat: Zpracovat, Uvolnit</param>
		/// <returns>StatusObject - Objektr který vraci informace o stavu</returns>
		private StatusObject ProcessPrijemka(int countentries, byte idterminal, Fask.DataSets.Prijem prijemdata, ProcessPrijemState processState)
		{
			TracId tracid = new TracId(null, idterminal, countentries);

			StatusObject processStatus = new StatusObject();

			#region trace
			Trac.Write("provider is " + (provider != null ? "not null" : "null"), "ProcessPrijemka START", tracid);
			#endregion



			try
			{
				if (provider != null && provider is Fask.Server.Interfaces.Prijem.IPrijem)
				{
					Fask.Server.Interfaces.Classes.Davka davka = new Fask.Server.Interfaces.Classes.Davka();
					Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();
					Fask.Server.Interfaces.Classes.Sklad sklad = new Fask.Server.Interfaces.Classes.Sklad();
					Fask.Server.Interfaces.Classes.Item item = new Fask.Server.Interfaces.Classes.Item();

					davka.ID = countentries;
					terminal.ID = idterminal;

					Fask.Server.Interfaces.Prijem.ProcessState pState = (Fask.Server.Interfaces.Prijem.ProcessState)processState;

					#region trace
					Trac.Write("provider.Prijem_Process(davka, terminal, sklad, item, prijemdata, pState) begin", "ProcessPrijemka", tracid);
					#endregion

					processStatus = provider.Prijem_Process(
						davka,
						terminal,
						sklad,
						item,
						prijemdata,
						pState
						);

					#region trace
					Trac.Write("provider.Prijem_Process(davka, terminal, sklad, item, prijemdata, pState) end, StatusText : " + processStatus.StatusText, "ProcessPrijemka", tracid);
					#endregion

					#region trace
					Trac.Write("ProcessPrijemka END", tracid);
					#endregion

					return processStatus;
				}

			}
			catch (Exception ex)
			{
				#region trace
				Trac.Write(ex, "ProcessPrijemka END, exception", tracid);
				#endregion

				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				throw ex;
			}


			string msg = "Provider v Prijem.asmx > 'GetPrijemky(byte idterminal, string prefixskladu)' nenastaven.";
			Trac.Write(msg, tracid);
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
			throw new Exception(msg);

			
		}

		/// <summary>
		/// Metoda wraper pro odchyceni vyjimky a ulozeni vysledku volani ...
		/// </summary>
		/// <param name="ponumber">èíslo objednavky</param>
		/// <param name="skl_id">ID Skladu</param>
		/// <param name="so">reference na StatusObject</param>
		private void GenerateDavkaWrapper(string ponumber, string skl_id, ref StatusObject so)
		{
			try
			{
				so.StatusText = "Generování dávky dokladu '" + ponumber + "'";
				so.Write();

				int result = GenerateDavka(ponumber, skl_id);

				so.StatusText = result.ToString();
				so.Finished = true;
				so.Write();

				//return result;
			}
			catch (Exception ex)
			{
				so.Exception = true;
				so.Finished = true;
				so.StatusText = ex.Message;
				so.Write();

				//return 0; //davka 0 normalne neexituje ...
			}
		}


		
		#endregion
	}
}
