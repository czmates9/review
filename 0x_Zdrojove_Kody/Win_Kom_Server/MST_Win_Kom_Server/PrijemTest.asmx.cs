using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Reflection;
using Fask.Logging;

namespace Fask.MST_W_Server
{
	/// <summary>
	/// Služba přenosu dat PrijemTest
	/// </summary>
	[WebService(Namespace = "http://PrijemTest.fask.cz/", Description = "Služba přenosu dat PrijemTest")]
	//[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	//[System.ComponentModel.ToolboxItem(false)]
	// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
	// [System.Web.Script.Services.ScriptService]
	public class PrijemTest : System.Web.Services.WebService
	{

		#region lokalne promenne

		private Fask.Server.Interfaces.IWebModule provider = null;
		
		#endregion

		#region Konstruktor

		/// <summary>
		/// Konstruktor
		/// </summary>
		public PrijemTest()
		{
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
								//t.IsAssignableFrom(typeof(Fask.Server.Interfaces.Vydej.IVydej);
								//if (typeof(Fask.Server.Interfaces.Vydej.IVydej).IsAssignableFrom(t))
								if (typeof(Fask.Server.Interfaces.IWebModule).IsAssignableFrom(t))
								{
									//provider = (Fask.Server.Interfaces.Vydej.IVydej)providerAssemlby.CreateInstance(t.FullName);
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


		#region webmetody

		/// <summary>
		/// Metoda na vyskoušeni zavolani funkce Nerealizovane přijemky dle č. kodu
		/// </summary>
		/// <param name="IDTerm">ID Terminalu</param>
		/// <param name="SKLID">ID Skladu</param>
		/// <param name="ITEMTYPE">Typ položky</param>
		/// <param name="Kod1">č. kod 1</param>
		/// <param name="Kod2">č. kod 2</param>
		/// <param name="Kod3">č. kod 3</param>
		/// <returns>Dataset Obecne naplneni</returns>
		[Fask.MST_W_Server.ExtensionsSpy.SpyExtension(@"d:\_work_\_Vyvoj_Subversion_04_cipisek\MST_07\SERVER\MST_Win_Kom_Server\test.spy")]
		[Interval.SoapExtensions.ZipExtension()]
		[WebMethod(Description = "Metoda na vyskoušeni zavolani funkce Nerealizovane přijemky dle č. kodu")]
		public Fask.Server.Interfaces.DataSets.Obecne Test_Fun_FASK_GetDavkyByCarKody(byte IDTerm, string SKLID, string ITEMTYPE, string Kod1, string Kod2, string Kod3)
		{
			try
			{
				Prijem p = new Prijem();
				List<string> l = new List<string>();

				if (!string.IsNullOrEmpty(Kod1.Trim()))
					l.Add(Kod1);

				if (!string.IsNullOrEmpty(Kod2.Trim()))
					l.Add(Kod2);


				if (!string.IsNullOrEmpty(Kod3.Trim()))
					l.Add(Kod3);

				return p.Online_GetNezrealizovanePrijemky_Vyber(IDTerm, SKLID, ITEMTYPE, l);
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				throw ex;
			}

		}

		/// <summary>
		/// Metoda pro vyskoušeni zavolani procedury Sklad/Expedice
		/// </summary>
		/// <param name="ITEMNMBR">ID položky</param>
		/// <param name="MnozstviZadane">množstvi zadane</param>
		/// <param name="MnozstviNasnimane">množstvi nasnimane</param>
		/// <returns>řetezec s vypočitanima hodnotama</returns>
		[WebMethod(Description = "Metoda pro vyskoušeni zavolani procedury Sklad/Expedice")]
		public string Online_GetSkladExpedice(
			string ITEMNMBR,
			decimal MnozstviZadane,
			decimal MnozstviNasnimane)
		{
			try
			{
				Prijem p = new Prijem();

				decimal MnozstviDodavatelePozadovano = 0;
				decimal MnozstviDodavateleDodano = 0;
				decimal MnozstviDodavateleDodat = 0;
				decimal MnozstviOdberateliPozadovano = 0;
				decimal MnozstviOdberatelumDodano = 0;
				decimal MnozstviOdberatelumDodat = 0;
				decimal Vysledek = 0;


				p.Online_GetSkladExpedice(ITEMNMBR,
			 MnozstviZadane,
			 MnozstviNasnimane,
			 out  MnozstviDodavatelePozadovano,
			out  MnozstviDodavateleDodano,
			out  MnozstviDodavateleDodat,
			out  MnozstviOdberateliPozadovano,
			out  MnozstviOdberatelumDodano,
			out  MnozstviOdberatelumDodat,
			out  Vysledek);

				return string.Format(
					"ITEMNMBR:{0} " + Environment.NewLine +
					",MnozstviZadane:{1}" + Environment.NewLine +
					",MnozstviNasnimane:{2}" + Environment.NewLine +
					",MnozstviDodavatelePozadovano:{3}" + Environment.NewLine +
					",MnozstviDodavateleDodano:{4}" + Environment.NewLine +
					",MnozstviDodavateleDodat:{5}" + Environment.NewLine +
					",MnozstviOdberateliPozadovano:{6}" + Environment.NewLine +
					",MnozstviOdberatelumDodano:{7}" + Environment.NewLine +
					",MnozstviOdberatelumDodat:{8}" + Environment.NewLine +
					",Vysledek:{9},",
					ITEMNMBR,
					MnozstviZadane,
					MnozstviNasnimane,
					MnozstviDodavatelePozadovano,
					MnozstviDodavateleDodano,
					MnozstviDodavateleDodat,
					MnozstviOdberateliPozadovano,
					MnozstviOdberatelumDodano,
					MnozstviOdberatelumDodat,
					Vysledek);

			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				throw ex;
			}
		}


		/// <summary>
		/// Metoda která dotahne zboží z FASK_ZASOBY
		/// </summary>
		/// <returns></returns>
		[Interval.SoapExtensions.ZipExtension()]
		[Fask.MST_W_Server.ExtensionsSpy.SpyExtension(@"d:\_work_\_Vyvoj_Subversion_04_cipisek\MST_07\SERVER\MST_Win_Kom_Server\test.spy")]
		[WebMethod(Description = "Metoda která dotahne zboží z FASK_ZASOBY")]
		public Fask.SQLiteDBs.DataSets.Zbozi Test_GetZbozi()
		{
            Konfigurace.Classes.Globals_Konfig_WebConfig.LoadConfiguration();

			Fask.SQLiteDBs.DataSets.Zbozi zbozi = null;

			//Nacteni dat prijemky z databaze                    
			zbozi = new Fask.SQLiteDBs.DataSets.Zbozi();

			//Ciselnik zbozi
			string select = "SELECT * FROM FASK_ZASOBY ";

			System.Data.SqlClient.SqlDataAdapter xda = new System.Data.SqlClient.SqlDataAdapter( select, new System.Data.SqlClient.SqlConnection(Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.ConnectionString[0].FASKDB));
			
			xda.Fill(zbozi, zbozi.CZMST095.TableName);


			//Ulozeni dat prijemky pro terminal
			foreach (Fask.SQLiteDBs.DataSets.Zbozi.CZMST095Row zrow in zbozi.CZMST095)
			{
				zrow.SetAdded();
			}

			return zbozi;
		}

		/// <summary>
		/// Metoda která zpracuje soubor na serveru
		/// </summary>
		/// <param name="countentries">číslo dávky</param>
		/// <param name="idterminal">ID Terminalu</param>
		/// <param name="processVydejState">přiznak co se ma udelat : Uvolnit,Zpracovat </param>
		/// <returns>Status o provedeni</returns>
		[WebMethod(Description = "Metoda která zpracuje soubor na serveru")]
		public string ProcessPrijemDBFile2_Test(int countentries, byte idterminal, string processVydejState)
		{
			Fask.Server.Interfaces.Classes.StatusObject so = null;
			Prijem v = new Prijem();
			try
			{

				if (processVydejState == "Uvolnit")
					so = v.ProcessPrijemDBFile2(countentries, idterminal, Fask.MST_W_Server.Prijem.ProcessPrijemState.Uvolnit);
				else if (processVydejState == "Zpracovat")
					so = v.ProcessPrijemDBFile2(countentries, idterminal, Fask.MST_W_Server.Prijem.ProcessPrijemState.Zpracovat);

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

		[WebMethod(Description ="testovaci metoda pro nachystani prijemky, problem s uvolnenim souboru davky sqlitem")]
		public string PrepareDavkaTest(int cislodavky, byte cisloterminalu)
		{
			try
			{
				Prijem p = new Prijem();
				var result = p.PreparePrijemkaDB(cislodavky, cisloterminalu);
				return result.ToString();
			}
			catch (Exception ex)
			{
				return ex.Message + "</br>" + ex.StackTrace;
			}
		}


		#endregion


		[WebMethod(Description = "Testovaci metoda pro Obedeni davkz primo z DB do IS")]
		public string OdvedeniDavky_Do_IS(int countEntries, string SKL_ID, string PONUMBER, string note)
		{
			try
			{
				if (provider != null && provider is Fask.Server.Interfaces.Prijem.IPrijem)
				{
					string ret = ((Fask.Server.Interfaces.Prijem.IPrijem)provider).TEST_ImportPrijem_Do_IS(countEntries, SKL_ID, PONUMBER, note);
					return ret;
				}
				else
					return "Provider neni nastaven...";
			}
			catch (Exception ex)
			{
				return ex.Message + "</br>" + ex.StackTrace;
			}
		}





	}

}
