using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Fask.Logging;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;
using Fask.Server.Interfaces.Classes;

namespace Fask.MST_W_Server
{
	/// <summary>
	/// Testovací Služba přenosu dat výdeje. 
	/// </summary>
	[WebService(Namespace = "http://VydejTest.fask.cz/", Description = "Testovací Služba přenosu dat výdeje.")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
	// [System.Web.Script.Services.ScriptService]
	public class VydejTest : System.Web.Services.WebService
	{

		private Fask.Server.Interfaces.IWebModule provider = null;

		/// <summary>
		/// Konstruktor
		/// </summary>
		public VydejTest()
		{
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


		/// <summary>
		/// Metoda pro ruční importu Vydejky z CZMST_SE
		/// </summary>
		/// <param name="countEntries">číslo dávky</param>
		/// <param name="userID">ID uživatele</param>
		/// <param name="SKL_ID">ID Skladu</param>
		/// <param name="note">Poznámka</param>
		/// <returns></returns>
		[WebMethod(Description = "Metoda pro ruční importu Vydejky z CZMST_SE")]
		public string TEST_ImportVydejka(int countEntries, int userID, string SKL_ID, string note)
		{
			try
			{
				if ((provider != null) && (provider is Fask.Server.Interfaces.Vydej.IVydej))
				{
					try
					{
						var processStatus = (provider as Fask.Server.Interfaces.Vydej.IVydej).TEST_ImportVydejka_Do_IS(countEntries, userID, SKL_ID, note);
						return processStatus.StatusText;
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
				return ex.Message;
			}

			return "Import výdejky proběhl korektně.";
		}

		/// <summary>
		/// Metoda pro ruční importu Faktury z CZMST_SE.
		/// </summary>
		/// <param name="countEntries">číslo dávky</param>
		/// <param name="userID">ID uživatele</param>
		/// <param name="SKL_ID">ID Skladu</param>
		/// <param name="note">Poznámka</param>
		/// <returns></returns>
		[WebMethod(Description = "Metoda pro ruční importu Faktury z CZMST_SE.")]
		public string TEST_ImportFaktura(int countEntries, int userID, string SKL_ID, string note)
		{
			try
			{
				if ((provider != null) && (provider is Fask.Server.Interfaces.Vydej.IVydej))
				{
					try
					{
						var processStatus = (provider as Fask.Server.Interfaces.Vydej.IVydej).TEST_ImportFaktura_Do_IS(countEntries, userID, SKL_ID, note);
						return processStatus.StatusText;
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
				return ex.Message;
			}

			return "Import faktúry proběhl korektně.";
		}



		//[Interval.SoapExtensions.ZipExtension()]
		//[Fask.MST_W_Server.ExtensionsSpy.SpyExtension(@"d:\_work_\_Vyvoj_Subversion_04_cipisek\MST_07\SERVER\MST_Win_Kom_Server\test.spy")] 

		/// <summary>
		/// Testovací metoda která z CZMST_SE vrací maximalní délku pro zadaný Sloupec
		/// dotahuje z XML souboru definice.
		/// </summary>
		/// <param name="clm"></param>
		/// <returns></returns>
		[WebMethod(Description = "Testovací metoda která z CZMST_SE vrací maximalní délku pro zadaný Sloupec")]
		public string TEST_Columns(string clm)
		{
			try
			{
				return Fask.Columns.Inicializace.InitInstance.Vydej.ColumnsInfo_CZMST_SE[clm].MaxLength.ToString();
			}
			catch (Exception ex)
			{
				string strex = string.Empty;
				strex = ex.Message + " | " + ex.StackTrace;
				if (ex.InnerException != null)
				{
					strex += ex.InnerException.Message + " | " + ex.InnerException.StackTrace;
				}
				return strex;
			}
		}

		/// <summary>
		/// Metoda pro zpracování davky na serveru z zip souboru
		/// </summary>
		/// <param name="countentries">číslo dávky</param>
		/// <param name="idterminal">ID Terminálu</param>
		/// <param name="processVydejState">ProcessVydejState enum typu zpracovani</param>
		/// <returns></returns>
		[WebMethod(Description = "Metoda pro zpracování davky na serveru z zip souboru. Varianty processVydejState : Uvolnit, Zpracovat, ZpracovatAPokracovat")]
		public string TEST_ProcessVydejkaDBFile2(int countentries, byte idterminal, string processVydejState)
		{
			Fask.Server.Interfaces.Classes.StatusObject so = null;
			Vydej v = new Vydej();
			try
			{

				if (processVydejState == "Uvolnit")
					so = v.ProcessVydejkaDBFile2(countentries, idterminal, Fask.MST_W_Server.Vydej.ProcessVydejState.Uvolnit);
				else if (processVydejState == "Zpracovat")
					so = v.ProcessVydejkaDBFile2(countentries, idterminal, Fask.MST_W_Server.Vydej.ProcessVydejState.Zpracovat);
				else if (processVydejState == "ZpracovatAPokracovat")
					so = v.ProcessVydejkaDBFile2(countentries, idterminal, Fask.MST_W_Server.Vydej.ProcessVydejState.ZpracovatAPokracovat);

				if (so.Exception)
				{
					return so.StatusText;
				}

			}
			catch (Exception ex)
			{
				return ex.Message;
			}

			return "OK";
		}



		[WebMethod(Description = "Metoda ktera prenese z SE rovnou do SI")]
		public string TEST_VykryDavku(int countentries)
		{
			Konfigurace.Classes.Globals_Konfig_WebConfig.LoadConfiguration();

			Fask.DataSets.Vydej.CZMST_SEDataTable dtSE = new Fask.DataSets.Vydej.CZMST_SEDataTable();

			using (var con = new System.Data.SqlClient.SqlConnection(Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.ConnectionString[0].FASKDB))
			{
				using (var ada = new System.Data.SqlClient.SqlDataAdapter())
				{
					using (var com = con.CreateCommand())
					{
						ada.SelectCommand = com;
						ada.SelectCommand.CommandText = "SELECT * FROM CZMST_SE WHERE CountEntries ='" + countentries + "'";
						ada.SelectCommand.Connection = con;
						ada.SelectCommand.CommandType = CommandType.Text;

						int returnValue;
						returnValue = ada.Fill(dtSE);
					}
				}
			}

			Fask.DataSets.Vydej.CZMST_SIDataTable dtSI = new Fask.DataSets.Vydej.CZMST_SIDataTable();

			foreach (Fask.DataSets.Vydej.CZMST_SERow item in dtSE)
			{
				dtSI.AddCZMST_SIRow(
					item.CountEntries,
					item.SOPNUMBE,
					item.ITEMNMBR,
					item.ORD,
					item.VNDDOCNM,
					item.VNDITNUM,
					item.CZ_CarKod,
					item.SKL_ID,
					item.LOCNCODE,
					item.QTYSHPPD,
					item.QTYPACK,
					"123",
					string.Empty,
					string.Empty,
					string.Empty,
					string.Empty,
					string.Empty,
					string.Empty,
					0,
					item.DEX_ROW_ID,
					item.TYPEPAL,
					string.Empty,
					false,
					Guid.NewGuid(),
					string.Empty,
					0,
					99,
					item.MJ,
					item.QTYSHPPD,
					item.ITEMCODE,
					item.IsWEIGHTNull() ? 0 : item.WEIGHT,
					DateTime.Now.AddDays(30)
					);
			}

			Update_CZMST_SI(dtSI);

			return "OK";
		}

		#region Update SI

		public static int Update_CZMST_SI(object data)
		{
			try
			{
				Konfigurace.Classes.Globals_Konfig_WebConfig.LoadConfiguration();
				int result = 0;

				using (var con = new System.Data.SqlClient.SqlConnection(Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.ConnectionString[0].FASKDB))
				{
					using (var commandInsert = con.CreateCommand())
					{
						InitializeCommandInsert_SI(commandInsert);

						using (var adapter = new SqlDataAdapter())
						{
							adapter.InsertCommand = commandInsert;

							var dataIsDataSet = data as System.Data.DataSet;
							var dataIsDataTable = data as System.Data.DataTable;
							var dataIsDataRow = data as System.Data.DataRow;
							var dataIsDataRowArray = data as System.Data.DataRow[];


							if (dataIsDataSet != null)
								result = adapter.Update(dataIsDataSet, dataIsDataSet.Tables[0].TableName);
							else if (dataIsDataTable != null)
								result = adapter.Update(dataIsDataTable);
							else if (dataIsDataRow != null)
								result = adapter.Update(new System.Data.DataRow[] { dataIsDataRow });
							else if (dataIsDataRowArray != null)
								result = adapter.Update(dataIsDataRowArray);
							else
								throw new Exception(String.Format("Neodpovídající datový typ: {0}", data.GetType().ToString()));
						}
					}
				}
				return result;
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}
		}

		#region Inicialize metody

		public static void InitializeCommandInsert_SI(SqlCommand command)
		{
			command.CommandText = "INSERT INTO CZMST_SI (CountEntries, SOPNUMBE, ITEMNMBR, ORD, VNDDOCNM, VNDITNUM, CZ_CarKod, SKL_ID, LOCNCODE, MJ, QTYSHPPD, QTYPACK, QTYSHPPDMJ, SERLTNUM, KOD_SW, DAT_VYROBY, REZ_1, REZ_2, ODBER_ID,DATEDONE, TIMEDONE, USER_ID, TYPEPAL, NMBRPAL, PRINTED, GUID, INPUT_MODE, ID_TERMINAL) " +
				" VALUES " +
				" (@CountEntries,@SOPNUMBE,@ITEMNMBR,@ORD,@VNDDOCNM,@VNDITNUM,@CZ_CarKod,@SKL_ID,@LOCNCODE,@MJ,@QTYSHPPD,@QTYPACK,@QTYSHPPDMJ,@SERLTNUM,@KOD_SW,@DAT_VYROBY,@REZ_1,@REZ_2,@ODBER_ID,@DATEDONE,@TIMEDONE,@USER_ID,@TYPEPAL,@NMBRPAL,@PRINTED,@GUID,@INPUT_MODE,@ID_TERMINAL)";

			command.Parameters.Add(new SqlParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@SOPNUMBE", DbType = System.Data.DbType.String, SourceColumn = "SOPNUMBE", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, SourceColumn = "ITEMNMBR", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@ORD", DbType = System.Data.DbType.Int32, SourceColumn = "ORD", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@VNDDOCNM", DbType = System.Data.DbType.String, SourceColumn = "VNDDOCNM", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@VNDITNUM", DbType = System.Data.DbType.String, SourceColumn = "VNDITNUM", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_CarKod", DbType = System.Data.DbType.String, SourceColumn = "CZ_CarKod", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, SourceColumn = "SKL_ID", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, SourceColumn = "LOCNCODE", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@MJ", DbType = System.Data.DbType.String, SourceColumn = "MJ", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@QTYSHPPD", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYSHPPD", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@QTYPACK", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYPACK", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@QTYSHPPDMJ", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYSHPPDMJ", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@SERLTNUM", DbType = System.Data.DbType.String, SourceColumn = "SERLTNUM", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@KOD_SW", DbType = System.Data.DbType.String, SourceColumn = "KOD_SW", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@DAT_VYROBY", DbType = System.Data.DbType.String, SourceColumn = "DAT_VYROBY", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@REZ_1", DbType = System.Data.DbType.String, SourceColumn = "REZ_1", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@REZ_2", DbType = System.Data.DbType.String, SourceColumn = "REZ_2", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@ODBER_ID", DbType = System.Data.DbType.String, SourceColumn = "ODBER_ID", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@DATEDONE", DbType = System.Data.DbType.String, SourceColumn = "DATEDONE", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@TIMEDONE", DbType = System.Data.DbType.String, SourceColumn = "TIMEDONE", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@USER_ID", DbType = System.Data.DbType.Int32, SourceColumn = "USER_ID", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@TYPEPAL", DbType = System.Data.DbType.String, SourceColumn = "TYPEPAL", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@NMBRPAL", DbType = System.Data.DbType.String, SourceColumn = "NMBRPAL", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@PRINTED", DbType = System.Data.DbType.Byte, SourceColumn = "PRINTED", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@GUID", DbType = System.Data.DbType.Guid, SourceColumn = "GUID", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@INPUT_MODE", DbType = System.Data.DbType.Byte, SourceColumn = "INPUT_MODE", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@ID_TERMINAL", DbType = System.Data.DbType.Int32, SourceColumn = "ID_TERMINAL", SourceVersion = System.Data.DataRowVersion.Current });

		}

		#endregion

		#endregion

		[WebMethod(Description = "Metoda ktera prenese z SE rovnou do SI a pak rovno zpracuje do IS")]
		public string TEST_ZpracujDataZ_SI(int countentries)
		{
			Fask.DataSets.Vydej ds = new Fask.DataSets.Vydej();

			using (var con = new System.Data.SqlClient.SqlConnection(Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.ConnectionString[0].FASKDB))
			{
				using (var ada = new System.Data.SqlClient.SqlDataAdapter())
				{
					using (var com = con.CreateCommand())
					{
						ada.SelectCommand = com;
						ada.SelectCommand.CommandText = "SELECT * FROM CZMST_SE WHERE CountEntries ='" + countentries + "'";
						ada.SelectCommand.CommandText += " AND QTYPACK = 0";
						ada.SelectCommand.Connection = con;
						ada.SelectCommand.CommandType = CommandType.Text;

						int returnValue;
						returnValue = ada.Fill(ds, ds.CZMST_SE.TableName);
					}
				}
			}

			foreach (Fask.DataSets.Vydej.CZMST_SERow item in ds.CZMST_SE)
			{
				//ds.CZMST_SI.AddCZMST_SIRow(
				//	item.CountEntries,
				//	item.SOPNUMBE,
				//	item.ITEMNMBR,
				//	item.ORD,
				//	item.VNDDOCNM,
				//	item.VNDITNUM,
				//	item.CZ_CarKod,
				//	item.SKL_ID,
				//	 "OBA001",//item.LOCNCODE,
				//	item.QTYSHPPD,
				//	item.QTYPACK,
				//	"220022",
				//	string.Empty,
				//	string.Empty,
				//	string.Empty,
				//	string.Empty,
				//	string.Empty,
				//	string.Empty,
				//	0,
				//	item.DEX_ROW_ID,
				//	item.TYPEPAL,
				//	string.Empty,
				//	false,
				//	Guid.Parse("15912170-8318-4d21-9797-cc74d2d927b7"),
				//	string.Empty,
				//	0,
				//	99,
				//	item.MJ,
				//	item.QTYSHPPD,
				//	item.ITEMCODE,
				//	item.IsWEIGHTNull() ? 0 : item.WEIGHT,
				//	DateTime.Now.AddDays(30)
				//	);

				var dt = new DateTime(2020, 5, 15);

				ds.CZMST_SI.AddCZMST_SIRow(
	item.CountEntries,
	item.SOPNUMBE,
	item.ITEMNMBR,
	item.ORD,
	item.VNDDOCNM,
	item.VNDITNUM,
	item.CZ_CarKod,
	item.SKL_ID,
	item.LOCNCODE,
	item.QTYSHPPD,
	item.QTYPACK,
	"123",
	string.Empty,
	string.Empty,
	string.Empty,
	string.Empty,
	string.Empty,
	string.Empty,
	0,
	item.DEX_ROW_ID,
	item.TYPEPAL,
	string.Empty,
	false,
	Guid.NewGuid(),
	//Guid.Parse("15912170-8318-4d21-9797-cc74d2d927b7"),
	string.Empty,
	0,
	99,
	item.MJ,
	item.QTYSHPPD,
	item.ITEMCODE,
	item.IsWEIGHTNull() ? 0 : item.WEIGHT,
	dt
	);
			}

			Konfigurace.Classes.Globals_Konfig_Agendy.LoadConfiguration();
			ds.Parametry.ImportRow(Konfigurace.Classes.Globals_Konfig_Agendy.Konfigurace.VydejParametry[0]);
			ds.AcceptChanges();



			Vydej v = new Vydej();

			var so = v.ProcessVydejka(countentries, 99, ds, Vydej.ProcessVydejState.Zpracovat);

			return so.StatusText;
		}


		[WebMethod(Description = "Metoda, ktera vraci stav expirace, zda je mozne ji vydat")]
		public StatusOverExpirace Online_Expirace_Verify(string itemnmbr, string sklad_id, string serltnum, string exp)
		{
			try
			{
				var expirace = DateTime.Parse(exp);

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



		//[WebMethod(Description = "Metoda ktera testuje sorting dat...")]
		//public string TEST_sort(int countentries = 5)
		//{

		//	Fask.DataSets.Vydej ds = new Fask.DataSets.Vydej();

		//	using (var con = new System.Data.SqlClient.SqlConnection(Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.ConnectionString[0].FASKDB))
		//	{
		//		using (var ada = new System.Data.SqlClient.SqlDataAdapter())
		//		{
		//			using (var com = con.CreateCommand())
		//			{
		//				ada.SelectCommand = com;
		//				ada.SelectCommand.CommandText = "SELECT * FROM CZMST_SE WHERE CountEntries ='" + countentries + "'";
		//				ada.SelectCommand.Connection = con;
		//				ada.SelectCommand.CommandType = CommandType.Text;

		//				int returnValue;
		//				returnValue = ada.Fill(ds, ds.CZMST_SE.TableName);
		//			}
		//		}
		//	}

		//	foreach (Fask.DataSets.Vydej.CZMST_SERow item in ds.CZMST_SE)
		//	{
		//		ds.CZMST_SI.AddCZMST_SIRow(
		//			item.CountEntries,
		//			item.SOPNUMBE,
		//			item.ITEMNMBR,
		//			item.ORD,
		//			item.VNDDOCNM,
		//			item.VNDITNUM,
		//			item.CZ_CarKod,
		//			item.SKL_ID,
		//			item.LOCNCODE,
		//			item.QTYSHPPD - 2,
		//			item.QTYPACK,
		//			"258",
		//			string.Empty,
		//			string.Empty,
		//			string.Empty,
		//			string.Empty,
		//			string.Empty,
		//			string.Empty,
		//			0,
		//			item.DEX_ROW_ID,
		//			item.TYPEPAL,
		//			string.Empty,
		//			false,
		//			Guid.NewGuid(),
		//			string.Empty,
		//			0,
		//			99,
		//			item.MJ,
		//			item.QTYSHPPD,
		//			item.ITEMCODE,
		//			item.IsWEIGHTNull() ? 0 : item.WEIGHT);

		//		ds.CZMST_SI.AddCZMST_SIRow(
		//			item.CountEntries,
		//			item.SOPNUMBE,
		//			item.ITEMNMBR,
		//			item.ORD,
		//			item.VNDDOCNM,
		//			item.VNDITNUM,
		//			item.CZ_CarKod,
		//			item.SKL_ID,
		//			item.LOCNCODE,
		//			 2,
		//			item.QTYPACK,
		//			"258",
		//			string.Empty,
		//			string.Empty,
		//			string.Empty,
		//			string.Empty,
		//			string.Empty,
		//			string.Empty,
		//			0,
		//			item.DEX_ROW_ID,
		//			item.TYPEPAL,
		//			string.Empty,
		//			false,
		//			Guid.NewGuid(),
		//			string.Empty,
		//			0,
		//			99,
		//			item.MJ,
		//			item.QTYSHPPD,
		//			item.ITEMCODE,
		//			item.IsWEIGHTNull() ? 0 : item.WEIGHT);
		//	}


		//	List<Polozky> polEdit = new List<Polozky>();
		//	List<Polozky> polDelete = new List<Polozky>();
		//	List<string> listOP = new List<string>();

		//	KontrolaVykriti_A_Smazani(ds,  out polEdit, out polDelete, out listOP);


		//	return "OK";
		//}

		//#region Private metoda

		//internal static void KontrolaVykriti_A_Smazani(Fask.DataSets.Vydej dsV,
		//out List<Polozky> polEdit,
		//out List<Polozky> polDelete,
		//out List<string> listOP)
		//{
		//	try
		//	{
		//		polEdit = new List<Polozky>();
		//		polDelete = new List<Polozky>();
		//		listOP = new List<string>();

		//		foreach (var rowSE in dsV.CZMST_SE)
		//		{
		//			//var rows = dsV.CZMST_SI.ToList().Where(x => x.ITEMNMBR == rowSE.ITEMNMBR && x.ORD == rowSE.ORD);
		//			var rows = dsV.CZMST_SI.ToList().Where(
		//				x =>
		//					x.ITEMNMBR == rowSE.ITEMNMBR &&
		//					x.SOPNUMBE == rowSE.SOPNUMBE &&
		//					x.SKL_ID == rowSE.SKL_ID
		//			);

		//			if (rows == null || rows.Count() == 0)
		//			{
		//				polDelete.Add(new Polozky() { ITEMNMBR = rowSE.ITEMNMBR, SKL_ID = rowSE.SKL_ID, SOPNUMBE = rowSE.SOPNUMBE });

		//				string ID_OP = "123";
		//				//string ID_OP = Database.ABRA.Get_ID_OP(new Polozky()
		//				//{
		//				//	ITEMNMBR = rowSE.ITEMNMBR.Trim(),
		//				//	SOPNUMBE = rowSE.SOPNUMBE.Trim(),
		//				//	SKL_ID = rowSE.SKL_ID.Trim()
		//				//});

		//				if (!string.IsNullOrEmpty(ID_OP))
		//				{
		//					if (!listOP.Contains(ID_OP.Trim()))
		//					{
		//						listOP.Add(ID_OP.Trim());
		//					}
		//				}
		//			}
		//			else
		//			{

		//				decimal qtySI_Sum = rows.GroupBy(
		//					x => new
		//					{
		//						x.ITEMNMBR,
		//						x.SOPNUMBE,
		//						x.SKL_ID,
		//						x.QTYSHPPD
		//					}).Sum(x => x.Key.QTYSHPPD);

		//				decimal qty = rowSE.QTYSHPPD - qtySI_Sum;

		//				if (qty == 0)
		//				{
		//					foreach (var item in rows)
		//					{
		//						if (rowSE.CZ_SerNum_Track == 2 || rowSE.CZ_SerNum_Track == 1)
		//						{
		//							if (!string.IsNullOrEmpty(item.SERLTNUM))
		//							{
		//								polEdit.Add(new Polozky()
		//								{
		//									QTY = null,
		//									ITEMNMBR = item.ITEMNMBR.Trim(),
		//									SKL_ID = item.SKL_ID.Trim(),
		//									SOPNUMBE = item.SOPNUMBE.Trim(),
		//									SELTNUM = item.SERLTNUM.Trim()
		//								});
		//							}
		//						}
		//						else
		//						{
		//							polEdit.Add(new Polozky()
		//							{
		//								QTY = null,
		//								ITEMNMBR = rows.ToArray()[0].ITEMNMBR.Trim(),
		//								SKL_ID = rows.ToArray()[0].SKL_ID.Trim(),
		//								SOPNUMBE = rows.ToArray()[0].SOPNUMBE.Trim(),
		//								SELTNUM = null
		//							});
		//						}
		//					}
		//				}
		//				else
		//				{


		//					polEdit.Add(new Polozky()
		//					{
		//						QTY = rows.ToArray()[0].QTYSHPPD,
		//						ITEMNMBR = rows.ToArray()[0].ITEMNMBR,
		//						SKL_ID = rows.ToArray()[0].SKL_ID,
		//						SOPNUMBE = rows.ToArray()[0].SOPNUMBE,
		//						SELTNUM = rows.ToArray()[0].SERLTNUM
		//					});
		//				}
		//			}
		//		}
		//	}
		//	catch (System.Exception ex)
		//	{
		//		Fask.Logging.ExceptionHandler2.Handle(ex);
		//		throw (ex);
		//	}

		//}

		//public class Polozky
		//{
		//	public decimal? QTY;
		//	public string ITEMNMBR;
		//	public string SKL_ID;
		//	public string SOPNUMBE;
		//	public string SELTNUM;
		//}

		//#endregion

	}
}
