using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using Fask.Logging;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using Fask.MST_W_Server.Constants;
using Fask.MST_W_Server.SQLite_Classes;
using System.Reflection;

//using Interval.SoapExtensions;

namespace Fask.MST_W_Server
{
	/// <summary>
	/// Testovací webová služba s šikovnima utilitama.
	/// </summary>
	[WebService(Namespace = "http://Test.fask.cz/", Description = "Testovací webová služba s šikovnima utilitama.")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[ToolboxItem(false)]
	public class Test : System.Web.Services.WebService
	{

		private Fask.Server.Interfaces.IMST providerTest = null;

		#region Konstruktor

		//Konstruktor
		public Test()
		{

			// Inicializuje objektove rozhrani ...
			try
			{
				Konfigurace.Classes.Globals_Konfig_WebConfig.LoadConfiguration();
				string providerAssemblyPath = Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Providers[0].Provider;
				string providerAssemblyPathGlobal = Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Providers[0].Provider;
				if (String.IsNullOrEmpty(providerAssemblyPath))
					providerAssemblyPath = providerAssemblyPathGlobal;

				if (!String.IsNullOrEmpty(providerAssemblyPath))
				{
					if (providerTest == null) //inicializace se provede pouze pokud nebyla provedena ... 
					{
						Assembly providerAssemlby = Assembly.LoadFrom(Server.MapPath(providerAssemblyPath));
						Type[] types = providerAssemlby.GetTypes();
						foreach (Type t in types)
						{
							try
							{
								if (typeof(Fask.Server.Interfaces.IMST).IsAssignableFrom(t))
								{
									//provider = (Fask.Server.Interfaces.Vydej.IVydej)providerAssemlby.CreateInstance(t.FullName);
									providerTest = (Fask.Server.Interfaces.IMST)providerAssemlby.CreateInstance(t.FullName);
									if (providerTest != null)
									{
										if (providerTest is Fask.Server.Interfaces.Configuration.IConfiguration)
										{
											((Fask.Server.Interfaces.Configuration.IConfiguration)providerTest).LoadConfiguration();
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
						//return config;
					}
				}
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
			}
		}

		#endregion

		/// <summary>
		/// Testovací Ahoj od FASK!
		/// </summary>
		/// <returns>Text : Ahoj od FASK!</returns>
		[WebMethod(Description = "Testovací Ahoj od FASK!")]
		public string HelloWorld()
		{
			return "Ahoj od FASK! :)";
		}

		/// <summary>
		/// Testovací metoda která hodí exception
		/// </summary>
		/// <returns></returns>
		[WebMethod(Description = "Testovací která hodí chybu!")]
		public string Error()
		{
			throw new Exception("No fuj");
		}

		[WebMethod(Description = "Metoda která zapiše zadaný text do Log_Server_Info.txt souboru")]
		public bool WriteTo_Log_Server_Info_File(string Text)
		{
			return Fask.Logging.ExceptionHandler2.Handle(LogLevel.Info, Text);
		}

		[WebMethod(Description = "Metoda která zapiše zadaný text do Log_Server_Info.txt souboru")]
		public bool WriteTo_Trace_Server_Info_File(string Text)
		{

			try
			{

				Tracing.Trac.Write(Text);

			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				return false;
			}

			return true;
		}


		/// <summary>
		/// Vrací uživatele pod kterím bìží server na IIS
		/// </summary>
		/// <returns>Jmeno uživatele</returns>
		[WebMethod(Description = "Vrací uživatele pod kterím bìží server na IIS")]
		public string WindowsIdentity()
		{
			string res = string.Empty;
			res += System.Security.Principal.WindowsIdentity.GetCurrent().Name + "\n";
			return res;
		}

		/// <summary>
		/// Testovací metoda která pošle email na nastavenu adresu sledování
		/// </summary>
		/// <param name="data">Správa ktera se pošle do emailu</param>
		/// <returns>True - odeslano, False - chyba</returns>
		[WebMethod(Description = "Testovací metoda která pošle email na nastavenu adresu sledování")]
		public bool TestMail(string data)
		{
			try
			{
				Fask.Logging.ExceptionHandler2.Handle_Email(data);
				return true;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// Metoda která vratí seznam všech kodovaní na konkretním stroji
		/// </summary>
		/// <returns>List kodování</returns>
		[WebMethod(Description = "Metoda která vratí seznam všech kodovaní na konkretním stroji")]
		public List<string> Encodings()
		{
			List<string> encodings = new List<string>();

			try
			{
				foreach (var encinfo in System.Text.Encoding.GetEncodings())
				{
					encodings.Add(String.Format("{0}, {1}, {2}", encinfo.Name, encinfo.DisplayName, encinfo.CodePage));
				}
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
			}

			return encodings;
		}


		/// <summary>
		/// Metoda sloužící pro naèteni binarne uložene StatusObject souboru
		/// </summary>
		/// <param name="fullpath">Cesta k souboru</param>
		/// <returns>naètený soubor v èitelnej podobe</returns>
		[WebMethod(Description = "Metoda sloužící pro naèteni binarne uložene StatusObject souboru")]
		public Fask.Server.Interfaces.Classes.StatusObject StatusObjectReturn(string fullpath)
		{
			Fask.Server.Interfaces.Classes.StatusObject so = new Fask.Server.Interfaces.Classes.StatusObject(fullpath);
			so.Read();
			return so;
		}

		/// <summary>
		/// Metoda která vraci seznam všech šablon na serveru
		/// </summary>
		/// <returns>Netypov dataset všech šablon na serveru</returns>
		[WebMethod(Description = "Metoda která vraci seznam všech šablon na serveru")]
		public DataTable Labels()
		{
			DataTable dt = new DataTable();
			try
			{
				Konfigurace.Classes.Globals_Konfig_WebConfig.LoadConfiguration();
				dt.TableName = "Labels";
				dt.Columns.Add("Name");

				string templatedirpath = Server.MapPath(Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Directories[0].TemplateDirectory);

				foreach (string fname in Directory.GetFiles(templatedirpath))
				{
					DataRow dr = dt.NewRow();
					dr["Name"] = Path.GetFileName(fname);

					dt.Rows.Add(dr);
				}
				dt.AcceptChanges();
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				throw ex;
			}
			return dt;
		}

		/// <summary>
		/// Metoda slouží pro dotažení ID èíselnej øady 
		/// </summary>
		/// <param name="Modul">Typ modulu(Agendy) pro ktery se dotahuje </param>
		/// <param name="SKL_ID">ID Skladu</param>
		/// <returns>vrací ID øady</returns>
		[WebMethod(Description = "Metoda slouží pro dotažení ID èíselnej øady")]
		public string Test_Get_Rada(string Modul, string SKL_ID)
		{
			int? ID = 0;
			try
			{
				Konfigurace.Classes.Globals_Konfig_WebConfig.LoadConfiguration();


				Fask.Rady.Modul MyM = (Fask.Rady.Modul)Enum.Parse(typeof(Fask.Rady.Modul), Modul, true);
				Fask.Rady.NumericalSeries NS = new Fask.Rady.NumericalSeries(Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.ConnectionString[0].FASKDB);
				ID = NS.GetCiselnaRada_ID(Fask.Rady.TypDB.SQL, MyM, SKL_ID, string.Empty, false);

			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
			}

			if (ID.HasValue)
				return ID.Value.ToString();
			else
				return "ID Nenalezeno!!";


		}

		[WebMethod(Description = "Metoda pro tvorbu SQLite soborù")]
		public string CreateSQLiteFile()
		{

			CreateSQLFile(Common.EventTypes);
			CreateSQLFile(Common.EventsUser);
			CreateSQLFile(Common.InternalState);
			CreateSQLFile(Common.Inventura1);
			CreateSQLFile(Common.Inventura2);
			CreateSQLFile(Common.Lokace);
			CreateSQLFile(Common.Meny);
			CreateSQLFile(Common.Odberatele);
			CreateSQLFile(Common.Pracovnici);
			CreateSQLFile(Common.Prijem);
			CreateSQLFile(Common.Prodej);
			CreateSQLFile(Common.Production);
			CreateSQLFile(Common.Servis_Ciselniky);
			CreateSQLFile(Common.Servis_ZdrojePohyb);
			CreateSQLFile(Common.Servis_ZdrojeStav);
			CreateSQLFile(Common.Sklady);
			CreateSQLFile(Common.Strediska);
			CreateSQLFile(Common.Tiskarny);
			CreateSQLFile(Common.TypDokladu);
			CreateSQLFile(Common.Ukoly);
			CreateSQLFile(Common.Uzivatele);
			CreateSQLFile(Common.Vydej);
			CreateSQLFile(Common.Vyroba);
			CreateSQLFile(Common.Zbozi);

			return "OK";

		}

		private void CreateSQLFile(string NameFile)
		{
			string dstFile = Path.Combine(Fask.MyPath.Path.SQLiteDBsDirectory, "Soubory_SQLite" + Common.Backslash + NameFile + Common.PRD);

			string path = Path.GetDirectoryName(dstFile);

			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);

			if (File.Exists(dstFile))
				return;

			SQLite_Helper helper = new SQLite_Helper();
			if (!helper.SQLite_CreateFile(dstFile, NameFile))
			{
				//throw new Exception("Nastala chyba pri tvorbe SQLite souboru pro " + Common.Zbozi);
				Logging.ExceptionHandler2.Handle(LogLevel.Error, "Nastala chyba pri tvorbe SQLite souboru do Soubory_SQLite pro " + NameFile);
			}
		}

		//[WebMethod(Description = "Metoda pro test komunikace s IS, at už je jakakoliv")]
		//public string Test_Provider_Komunikace()
		//{
		//	if ((providerTest != null) && (providerTest is Fask.Server.Interfaces.Test.ITest))
		//	{
		//		return (providerTest as Fask.Server.Interfaces.Test.ITest_Komunikace).Komunikace();
		//	}
		//	else
		//	{
		//		return "Provider nenastaven...";
		//	}

		//}

		[WebMethod(Description = "Metoda pro test komunikace s IS, at už je jakakoliv")]
		public string Test_Provider_Komunikace()
		{
			if (providerTest == null)
				return "Provider nenastaven (providerTest == null).";

			if (providerTest is Fask.Server.Interfaces.Test.ITest_Komunikace testKom)
				return testKom.Komunikace();

			return "Provider je nastaven, ale neimplementuje ITest_Komunikace.";
		}

		//[WebMethod(Description = "Metoda pro test tisku GS1")]
		//public string Test_TiskGS1(string templateName)
		//{
		//	string X = string.Empty;
		//	Fask.Server.Interfaces.DataSets.DSValues ds = new Fask.Server.Interfaces.DataSets.DSValues();

		//	Fask.Server.Interfaces.DataSets.c


		//	DoplnHodnotu(ds, "VNDITNUM", ITEMNMBR);
		//	DoplnHodnotu(ds, "SERLTNUM", ITEMNMBR);
		//	DoplnHodnotu(ds, "EXPIRACE", ITEMNMBR);

		//	if ((providerTest != null) && (providerTest is Fask.Server.Interfaces.Tisky.ITisky2_MetodaEtiketa))
		//	{
		//		(providerTest as Fask.Server.Interfaces.Tisky.ITisky2_MetodaEtiketa).TiskMetodaEtiketa(99, ref X, ref ds, 0);
		//	}
		//	else
		//	{
		//		return "Provider nenastaven...";
		//	}

		//	return "OK";
		//}

		private void DoplnHodnotu(Fask.Server.Interfaces.DataSets.DSValues data, string Key, string Value)
		{
			if (!string.IsNullOrEmpty(Value))
			{
				data.Values.AddValuesRow(Key.ToUpper(), Value);
			}
		}


	}
}
