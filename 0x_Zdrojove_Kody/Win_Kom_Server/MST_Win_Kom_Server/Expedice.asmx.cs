using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Reflection;
using Fask.Server.Interfaces.Classes;
using Fask.Logging;


namespace Fask.MST_W_Server
{
    /// <summary>
	/// Služba přenosu dat expedice
    /// </summary>
	[WebService(Namespace = "http://Expedice.fask.cz/", Description = "Služba přenosu dat expedice", Name = "ExpediceService")]
    public class Expedice : System.Web.Services.WebService
    {
		#region Lokalna trida pro TiskSablony

		/// <summary>
		/// Trida pro uchovani informaci o tisku
		/// </summary>
		public class TiskSablona
		{
			/// <summary>
			/// Konstruktor
			/// </summary>
			public TiskSablona()
			{
				this.printerName = string.Empty;
				this.sablonaHlavicka = string.Empty;
				this.sablonaRadek = string.Empty;
				this.sablonaPaticka = string.Empty;
				this.pocetVytisku = 1;
			}

			public string printerName;
			public string sablonaHlavicka;
			public string sablonaRadek;
			public string sablonaPaticka;
			public int pocetVytisku;
		} 

		#endregion

		#region Lokalne promenne

		Fask.Server.Interfaces.Expedice.IExpedice provider = null;
		
		#endregion

		#region Konstruktor

		/// <summary>
		/// Konstruktor
		/// </summary>
		public Expedice()
		{
			// Inicializuje objektove rozhrani ...
			try
			{
                Konfigurace.Classes.Globals_Konfig_WebConfig.LoadConfiguration();
                string providerAssemblyPath = Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Providers[0].Provider_Expedice;
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
								if (typeof(Fask.Server.Interfaces.Expedice.IExpedice).IsAssignableFrom(t))
								{
									provider = (Fask.Server.Interfaces.Expedice.IExpedice)providerAssemlby.CreateInstance(t.FullName);
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

		#region Baleni

		/// <summary>
		/// Metoda pro přidání hlavičky do Baleni
		/// </summary>
		/// <param name="idterminal">ID Terminalu</param>
		/// <param name="uzivatel">Uživatel</param>
		/// <param name="skl_id">ID Skladu</param>
		/// <param name="hlavicka">POložky pro pridani</param>
		/// <returns>ID Chyby</returns>
		[WebMethod(Description = "Metoda pro přidání hlavičky do Baleni")]
		public int Baleni_Hlavicka_Add(byte idterminal, int uzivatel, string skl_id, Fask.Server.Interfaces.DataSets.ExpediceBaleniHlavicky hlavicka)
		{
			try
			{
				if (provider != null && provider is Fask.Server.Interfaces.Expedice.IExpedice)
				{
					Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();
					Fask.Server.Interfaces.Classes.Sklad sklad = new Fask.Server.Interfaces.Classes.Sklad();
					User user = new User();

					terminal.ID = idterminal;
					sklad.ID = skl_id;
					user.ID = uzivatel;

					StatusInfo si = provider.Expedice_Baleni_Hlavicka_Add(terminal,
						user,
						sklad,
						hlavicka);

					return si.ID;
				}
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				throw ex;
			}

			string msg = "Provider v Expedice.asmx > 'AddRecord(Fask.Server.Interfaces.Lokace.LokacePohyb TableRow)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, msg);
			throw new Exception(msg);
		}

		/// <summary>
		/// Metoda kterí vrací seznam položek z balení
		/// </summary>
		/// <param name="idterminal">ID Terminalu</param>
		/// <param name="uzivatel">ID uživatele</param>
		/// <param name="skl_id">ID Skladu</param>
		/// <param name="barcode">čarový kod</param>
		/// <param name="itemnmbr">ID položky</param>
		/// <param name="serltnum">Seriove čislo/ šarže</param>
		/// <returns>Dataset - ExpediceBaleni, naplneni položkama</returns>
		[WebMethod(Description = "Metoda kterí vrací seznam položek z balení")]
		public Fask.Server.Interfaces.DataSets.ExpediceBaleni Baleni_Polozka_Get(byte idterminal, int uzivatel, string skl_id, string barcode, string itemnmbr, string serltnum)
		{
			try
			{
				if (provider != null && provider is Fask.Server.Interfaces.Expedice.IExpedice)
				{
					Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();
					Fask.Server.Interfaces.Classes.Sklad sklad = new Fask.Server.Interfaces.Classes.Sklad();
					User user = new User();

					terminal.ID = idterminal;
					sklad.ID = skl_id;
					user.ID = uzivatel;

					return provider.Expedice_Baleni_Polozka_Get(
						terminal,
						user,
						skl_id,
						barcode,
						itemnmbr,
						serltnum
						);
				}
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				throw ex;
			}

			string msg = "Provider v Expedice.asmx > 'Baleni_Polozka_Get(byte idterminal, int uzivatel, string skl_id, string barcode, string itemnmbr, string serltnum)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, msg);
			throw new Exception(msg);
		}

		/// <summary>
		/// Metoda která smaže zaznamy balení
		/// </summary>
		/// <param name="idterminal">ID Terminalu</param>
		/// <param name="uzivatel">ID uživatele</param>
		/// <param name="skl_id">ID Skladu</param>
		/// <param name="hlavickaID">GUID Hlavičky</param>
		/// <returns>ID Chyby</returns>
		[WebMethod(Description = "Metoda která smaže zaznamy balení")]
		public int Baleni_Hlavicka_Del(byte idterminal, int uzivatel, string skl_id, Guid hlavickaID)
		{
			try
			{
				if (provider != null && provider is Fask.Server.Interfaces.Expedice.IExpedice)
				{
					Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();
					Fask.Server.Interfaces.Classes.Sklad sklad = new Fask.Server.Interfaces.Classes.Sklad();
					User user = new User();

					terminal.ID = idterminal;
					sklad.ID = skl_id;
					user.ID = uzivatel;

					StatusInfo si = provider.Expedice_Baleni_Hlavicka_Del(
						terminal,
						user,
						sklad,
						hlavickaID
						);

					return si.ID;
				}
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				throw ex;
			}

			string msg = "Provider v Expedice.asmx > 'Baleni_Hlavicka_Del(byte idterminal, int uzivatel, string skl_id, Guid hlavickaID)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, msg);
			throw new Exception(msg);
		}

		/// <summary>
		/// Metoda která vratí nalezene hlavčky pro Baleni
		/// </summary>
		/// <param name="idterminal">ID Terminalu</param>
		/// <param name="uzivatel">ID uživatele</param>
		/// <param name="prefixskladu">Prefix Skladu</param>
		/// <returns>Dataset ExpediceBaleniHlavicky, naplnen informacema</returns>
		[WebMethod(Description = "Metoda která vratí nalezene hlavčky pro Baleni")]
        public Fask.Server.Interfaces.DataSets.ExpediceBaleniHlavicky Baleni_GetHlavicky(byte idterminal, int uzivatel, string prefixskladu)
        {
            try
            {
                if (provider != null && provider is Fask.Server.Interfaces.Expedice.IExpedice)
                {
                    Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();
                    Fask.Server.Interfaces.Classes.Sklad sklad = new Fask.Server.Interfaces.Classes.Sklad();
                    User user = new User();

                    terminal.ID = idterminal;
                    sklad.ID = prefixskladu;
                    user.ID = uzivatel;

                    return provider.Expedice_Baleni_GetHlavicky(
                        terminal,
                        user,
                        sklad
                        );
                }

            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }

			string msg = "Provider v Expedice.asmx > 'Baleni_GetHlavicky(byte idterminal, int uzivatel, string prefixskladu)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, msg);
			throw new Exception(msg);
        }

		/// <summary>
		/// Metoda pro zpracovaní davky Baleni
		/// </summary>
		/// <param name="idterminal">ID Terminalu</param>
		/// <param name="uzivatel">ID uživatele</param>
		/// <param name="skl_id">ID Skladu</param>
		/// <param name="hlavicka">Guid Hlavičky</param>
		/// <param name="expedicedata">Data pro zpracovani</param>
		/// <param name="processExpediceState">Přiznak, co se ma udelat: Uvolnit, UvolnitAZpracovat, Zpracovat</param>
		/// <returns>StatusObjekt, nese informace o stavu</returns>
		[WebMethod(Description = "Metoda pro zpracovaní davky Baleni")]
		public Fask.Server.Interfaces.Classes.StatusObject Baleni_Process(byte idterminal, int uzivatel, string skl_id, Guid hlavicka, Fask.Server.Interfaces.DataSets.ExpediceBaleni expedicedata, Fask.Server.Interfaces.Expedice.ProcessState processExpediceState)
		{
			try
			{
				if (provider != null && provider is Fask.Server.Interfaces.Expedice.IExpedice)
				{
					Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();
					Fask.Server.Interfaces.Classes.Sklad sklad = new Fask.Server.Interfaces.Classes.Sklad();
					User user = new User();

					terminal.ID = idterminal;
					sklad.ID = skl_id;
					user.ID = uzivatel;

					StatusObject so = provider.Expedice_Baleni_Process(
						hlavicka,
						user,
						terminal,
						sklad,
						expedicedata,
						processExpediceState
						);

					return so;
				}

			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				throw ex;
			}

			string msg = "Provider v Expedice.asmx > 'Baleni_Process(byte idterminal, int uzivatel, string skl_id, Guid hlavicka, Fask.Server.Interfaces.DataSets.ExpediceBaleni expedicedata, Fask.Server.Interfaces.Expedice.ProcessState processExpediceState)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, msg);
			throw new Exception(msg);
		}

		/// <summary>
		/// Metoda na Storno davky Baleni
		/// </summary>
		/// <param name="idterminal">ID Terminalu</param>
		/// <param name="uzivatel">uživatel</param>
		/// <param name="skl_id">ID Skladu</param>
		/// <param name="hlavickaID">GUID Hlavičky</param>
		/// <param name="password">Heslo</param>
		/// <returns>ID Chyby</returns>
		[WebMethod(Description = "Metoda na Storno davky Baleni")]
		public int Baleni_Storno_Hlavicka(byte idterminal, int uzivatel, string skl_id, Guid hlavickaID, string password)
		{
			try
			{
				if (provider != null && provider is Fask.Server.Interfaces.Expedice.IExpedice)
				{
					Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();
					Fask.Server.Interfaces.Classes.Sklad sklad = new Fask.Server.Interfaces.Classes.Sklad();
					User user = new User();

					terminal.ID = idterminal;
					sklad.ID = skl_id;
					user.ID = uzivatel;

					StatusInfo si = provider.Expedice_Baleni_Storno_Hlavicka(
						hlavickaID,
						terminal,
						user,
						password
						);

					return si.ID;
				}
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				throw ex;
			}

			string msg = "Provider v Expedice.asmx > 'Baleni_Storno_Hlavicka(byte idterminal, int uzivatel, string skl_id, Guid hlavickaID, string password)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, msg);
			throw new Exception(msg);
		}

		/// <summary>
		/// Metoda pro pridani položky Baliku do davky
		/// </summary>
		/// <param name="idterminal">ID Terminalu</param>
		/// <param name="uzivatel">ID uživatele</param>
		/// <param name="skl_id">ID Skladu</param>
		/// <param name="hlavickaID">Guid Hlavičky</param>
		/// <param name="expediceRows">radky baliku co se maji přidat</param>
		/// <returns>ID Chyby</returns>
		[WebMethod(Description = "Metoda pro pridani položky Baliku do davky")]
		public int Baleni_Polozka_Add(byte idterminal, int uzivatel, string skl_id, Guid hlavickaID, Fask.Server.Interfaces.DataSets.ExpediceBaleni expediceRows)
		{
			try
			{
				if (provider != null && provider is Fask.Server.Interfaces.Expedice.IExpedice)
				{
					Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();
					Fask.Server.Interfaces.Classes.Sklad sklad = new Fask.Server.Interfaces.Classes.Sklad();
					User user = new User();

					terminal.ID = idterminal;
					sklad.ID = skl_id;
					user.ID = uzivatel;

					StatusInfo si = provider.Expedice_Baleni_Polozka_Add(
						hlavickaID,
						terminal,
						user,
						sklad,
						expediceRows
						);

					return si.ID;
				}

			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				throw ex;
			}

			string msg = "Provider v Expedice.asmx > 'Baleni_Polozka_Add(byte idterminal, int uzivatel, string skl_id, Guid hlavickaID, Fask.Server.Interfaces.DataSets.ExpediceBaleni expediceRows)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, msg);
			throw new Exception(msg);
		}

		/// <summary>
		/// Metoda pro smazani položky z davky baleni
		/// </summary>
		/// <param name="idterminal">ID Terminalu</param>
		/// <param name="uzivatel">ID uživatele</param>
		/// <param name="skl_id">ID Skladu</param>
		/// <param name="hlavickaID">Guid  hlavičky</param>
		/// <param name="polozkaID">Guid položky </param>
		/// <returns></returns>
		[WebMethod(Description = "Metoda pro smazani položky z davky baleni")]
		public int Baleni_Polozka_Del(byte idterminal, int uzivatel, string skl_id, Guid hlavickaID, Guid polozkaID)
		{
			try
			{
				if (provider != null && provider is Fask.Server.Interfaces.Expedice.IExpedice)
				{
					Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();
					Fask.Server.Interfaces.Classes.Sklad sklad = new Fask.Server.Interfaces.Classes.Sklad();
					User user = new User();

					terminal.ID = idterminal;
					sklad.ID = skl_id;
					user.ID = uzivatel;

					StatusInfo si = provider.Expedice_Baleni_Polozka_Del(
						hlavickaID,
						terminal,
						user,
						polozkaID
						);

					return si.ID;
				}

			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				throw ex;
			}

			string msg = "Provider v Expedice.asmx > 'Baleni_Polozka_Del(byte idterminal, int uzivatel, string skl_id, Guid hlavickaID, Guid polozkaID)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, msg);
			throw new Exception(msg);
		}

		/// <summary>
		/// Metoda která vráti seznam položek pro Baleni
		/// </summary>
		/// <param name="idterminal">ID Terminalu</param>
		/// <param name="uzivatel">uživatele</param>
		/// <param name="skl_id">ID Skladu</param>
		/// <param name="hlavickaID">Guid hlavičky</param>
		/// <returns>ExpediceBaleni - Dataset naplnen balikama</returns>
		[WebMethod(Description = "Metoda která vráti seznam položek pro Baleni")]
		public Fask.Server.Interfaces.DataSets.ExpediceBaleni Baleni_GetPolozky(byte idterminal, int uzivatel, string skl_id, Guid hlavickaID)
		{
			try
			{
				if (provider != null && provider is Fask.Server.Interfaces.Expedice.IExpedice)
				{
					Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();
					Fask.Server.Interfaces.Classes.Sklad sklad = new Fask.Server.Interfaces.Classes.Sklad();
					User user = new User();

					terminal.ID = idterminal;
					sklad.ID = skl_id;
					user.ID = uzivatel;

					return provider.Expedice_Baleni_GetPolozky(
						terminal,
						user,
						sklad,
						hlavickaID
						);
				}
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				throw ex;
			}

			string msg = "Provider v Expedice.asmx > 'Baleni_GetPolozky(byte idterminal, int uzivatel, string skl_id, Guid hlavickaID)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, msg);
			throw new Exception(msg);
		}

		/// <summary>
		/// Metoda pro Tisk z Baleni
		/// </summary>
		/// <param name="guidHlavickaBaleni">Guid hlavičky</param>
		/// <param name="NMBRPAL">číslo palety</param>
		/// <param name="tiskSablona">Objekt informaci o tisku</param>
		/// <returns>True- OK. False- chyba</returns>
		[WebMethod(Description = "Metoda pro Tisk z Baleni")]
		public bool Expedice_Baleni_Tisk(Guid guidHlavickaBaleni, string NMBRPAL, TiskSablona tiskSablona)
		{
			Fask.Server.Interfaces.DataSets.ExpediceBaleni.CZMST_Expedice_Baleni_Polozky_TISKDataTable dt = new Fask.Server.Interfaces.DataSets.ExpediceBaleni.CZMST_Expedice_Baleni_Polozky_TISKDataTable();
			try
			{

				if (provider != null && provider is Fask.Server.Interfaces.Expedice.IExpedice)
				{
					dt = provider.Expedice_Baleni_Tisk(
					guidHlavickaBaleni,
					NMBRPAL
					);
				}
				else
				{
					string msg = "Provider v Expedice.asmx > 'AddRecord(Fask.Server.Interfaces.Lokace.LokacePohyb TableRow)' nenastaven.";
					Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, msg);
					throw new Exception(msg);
				}
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				throw ex;
			}

			int terminalID;
			MST_Print_Server_ZPL_Printing.TiskParams printerParams = null;
			string templateHeader;
			string templateRow;
			string templateFooter;
			int pocet;

			if (tiskSablona == null)
			{
				terminalID = 11;
				printerParams = new MST_Print_Server_ZPL_Printing.TiskParams("", "", "ZDesigner ZT230-200dpi ZPL", "");
				templateHeader = "testHead1_PAL.zpl";
				templateRow = "testRow1_PAL.zpl";
				templateFooter = "testFoot1_PAL.zpl";
				pocet = 1;
			}
			else
			{
				terminalID = 11;
				printerParams = new MST_Print_Server_ZPL_Printing.TiskParams("", "", tiskSablona.printerName, "");
				templateHeader = tiskSablona.sablonaHlavicka;
				templateRow = tiskSablona.sablonaRadek;
				templateFooter = tiskSablona.sablonaPaticka;
				pocet = tiskSablona.pocetVytisku;
			}


			//Data
			Fask.Server.Interfaces.DataSets.DSValues dataHeader = new Fask.Server.Interfaces.DataSets.DSValues();
			List<Fask.Server.Interfaces.DataSets.DSValues> dataRowList = new List<Fask.Server.Interfaces.DataSets.DSValues>();
			Fask.Server.Interfaces.DataSets.DSValues dataFooter = new Fask.Server.Interfaces.DataSets.DSValues();

			// Hlavičky



			dataHeader.Values.AddValuesRow("NMBRPAL", dt[0].NMBRPAL);

			dataHeader.Values.AddValuesRow("Firma", dt[0].Firma);
			dataHeader.Values.AddValuesRow("Utvar", dt[0].Utvar);
			dataHeader.Values.AddValuesRow("Jmeno", dt[0].Jmeno);
			dataHeader.Values.AddValuesRow("Ulice", dt[0].Ulice);
			dataHeader.Values.AddValuesRow("PSC", dt[0].PSC);
			dataHeader.Values.AddValuesRow("Obec", dt[0].Obec);
			dataHeader.Values.AddValuesRow("ICO", dt[0].ICO);
			dataHeader.Values.AddValuesRow("DIC", dt[0].DIC);

			#region SOPNUMBE čisla objednavek v hlavičke

			List<IGrouping<string, Fask.Server.Interfaces.DataSets.ExpediceBaleni.CZMST_Expedice_Baleni_Polozky_TISKRow>> dokladgroup = dt.GroupBy(x => x.Doklad.Trim()).Select(x => x).ToList(); ;

			List<string> SOPNUMBESeznam = new List<string>();

			foreach (IGrouping<string, Fask.Server.Interfaces.DataSets.ExpediceBaleni.CZMST_Expedice_Baleni_Polozky_TISKRow> item in dokladgroup)
			{
				SOPNUMBESeznam.Add(item.Key.Trim());
			}

			dataHeader.Values.AddValuesRow("SOPNUMBEList", String.Join(", ", SOPNUMBESeznam.ToArray()));

			#endregion

			#region VNDDOCNM čisla objednavek v hlavičke

			//List<IGrouping<string, Fask.Server.Interfaces.DataSets.ExpediceBaleni.CZMST_Expedice_Baleni_Polozky_TISKRow>> dokladgroupVNDDOCNM = dt.GroupBy(x => x.VNDDOCNM.Trim()).Select(x => x).ToList(); ;

			//List<string> VNDDOCNMSeznam = new List<string>();

			//foreach (IGrouping<string, Fask.Server.Interfaces.DataSets.ExpediceBaleni.CZMST_Expedice_Baleni_Polozky_TISKRow> item in dokladgroupVNDDOCNM)
			//{
			//    VNDDOCNMSeznam.Add(item.Key.Trim());
			//}

			//dataHeader.Values.AddValuesRow("VNDDOCNMList", String.Join(", ", VNDDOCNMSeznam.ToArray()));
			dataHeader.Values.AddValuesRow("VNDDOCNMList", dt[0].VNDDOCNMList);

			#endregion

			#region Počet baliku na palete

			dataHeader.Values.AddValuesRow("POCETBALIKU", dt[0].PocetBaliku.ToString("0", System.Globalization.CultureInfo.InvariantCulture));


			//Složitejší dotahnout kedže se grupuje podle ITEMNMBR položky a dalsich info tak jeden typ položky muže byt ve vice Balikoch a tady se grupuje vramcu cele palety

			//List<IGrouping<string, Fask.Server.Interfaces.DataSets.ExpediceBaleni.CZMST_Expedice_Baleni_Polozky_TISKRow>> balikygroup = dt.GroupBy(x => x.NMBRBAL.Trim()).Select(x => x).ToList(); ;

			//List<string> NMBRBALSeznam = new List<string>();

			//foreach (IGrouping<string, Fask.Server.Interfaces.DataSets.ExpediceBaleni.CZMST_Expedice_Baleni_Polozky_TISKRow> item in balikygroup)
			//{
			//    NMBRBALSeznam.Add(item.Key.Trim());
			//}

			//dataHeader.Values.AddValuesRow("OBJ", String.Join(", ", NMBRBALSeznam.ToArray()));


			#endregion

			//Dotahovani odberatele s SI
			#region SQL dotaz
			//            --select
			//--* 
			//----ITEMNMBR, SUM(qty) as qty  
			//--from CZMST_Expedice_Baleni_Polozky 
			//--where 
			//----IDH = 'f39f8789-3afd-4174-84cf-e4482ca4e4fc' and 
			//--NMBRPAL = '00001234560000001152'
			//----group by ITEMNMBR


			//select * from CZMST_SI where NMBRPAL = '00001234560000001152'
			//select * from CZMST_Expedice_Baleni_Polozky where NMBRBAL = '00001234560000001152'




			//select s.NMBRPAL from  CZMST_SI s inner join CZMST_Expedice_Baleni_Polozky b ON s.NMBRPAL = b.NMBRBAL 
			//--group by s.NMBRPAL 
			#endregion

			//Data

			foreach (Fask.Server.Interfaces.DataSets.ExpediceBaleni.CZMST_Expedice_Baleni_Polozky_TISKRow item in dt)
			{
				Fask.Server.Interfaces.DataSets.DSValues dsval = new Fask.Server.Interfaces.DataSets.DSValues();

				foreach (System.Data.DataColumn CLM in dt.Columns)
				{
					string columnName = CLM.ColumnName;
					string Value;


					if (columnName.Trim() == "QTY")
					{
						string tmpstring = item[CLM.ColumnName].ToString();
						decimal tmpdecimal = decimal.Parse(tmpstring);
						Value = tmpdecimal.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
					}
					else
						Value = item[CLM.ColumnName].ToString().Trim();


					dsval.Values.AddValuesRow(CLM.ColumnName, Value);
				}


				dataRowList.Add(dsval);
			}

			//Patičky



			Tisk tisk = new Tisk();
			tisk.Soupis(terminalID, printerParams, dataHeader, dataRowList, dataFooter, templateHeader, templateRow, templateFooter, pocet);

			return true;
		}


		#endregion

		#region Expedice

		/// <summary>
		/// Metoda pro dotaženi položek z expedice
		/// </summary>
		/// <param name="idterminal">ID Terminalu</param>
		/// <param name="uzivatel">ID uživatele</param>
		/// <param name="skl_id">ID Skladu</param>
		/// <param name="hlavickaID">Guid hlavičky</param>
		/// <returns>Dataset Expedice naplnen položkama</returns>
		[WebMethod(Description="Metoda pro dotaženi položek z expedice")]
		public Fask.Server.Interfaces.DataSets.Expedice Expedice_GetPolozky(byte idterminal, int uzivatel, string skl_id, Guid hlavickaID)
		{
			try
			{
				if (provider != null && provider is Fask.Server.Interfaces.Expedice.IExpedice)
				{
					Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();
					Fask.Server.Interfaces.Classes.Sklad sklad = new Fask.Server.Interfaces.Classes.Sklad();
					User user = new User();

					terminal.ID = idterminal;
					sklad.ID = skl_id;
					user.ID = uzivatel;

					return provider.Expedice_GetPolozky(
						terminal,
						user,
						sklad,
						hlavickaID
						);
				}
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				throw ex;
			}

			string msg = "Provider v Expedice.asmx > 'Expedice_GetPolozky(byte idterminal, int uzivatel, string skl_id, Guid hlavickaID)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, msg);
			throw new Exception(msg);
		}

		/// <summary>
		/// Metoda pro pridany hlaviček do Expedice
		/// </summary>
		/// <param name="idterminal">ID Terminalu</param>
		/// <param name="uzivatel">ID uživatele</param>
		/// <param name="skl_id">ID Skladu</param>
		/// <param name="hlavicka">Data hlavičky</param>
		/// <returns>ID chyby</returns>
		[WebMethod(Description="Metoda pro pridany položek do Expedice")]
		public int Expedice_Hlavicka_Add(byte idterminal, int uzivatel, string skl_id, Fask.Server.Interfaces.DataSets.ExpediceHlavicky hlavicka)
		{
			try
			{
				if (provider != null && provider is Fask.Server.Interfaces.Expedice.IExpedice)
				{
					Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();
					Fask.Server.Interfaces.Classes.Sklad sklad = new Fask.Server.Interfaces.Classes.Sklad();
					User user = new User();

					terminal.ID = idterminal;
					sklad.ID = skl_id;
					user.ID = uzivatel;

					StatusInfo si = provider.Expedice_Hlavicka_Add(terminal,
						user,
						sklad,
						hlavicka);

					return si.ID;
				}
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				throw ex;
			}

			string msg = "Provider v Expedice.asmx > 'Expedice_Hlavicka_Add(byte idterminal, int uzivatel, string skl_id, Fask.Server.Interfaces.DataSets.ExpediceHlavicky hlavicka)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, msg);
			throw new Exception(msg);
		}

		/// <summary>
		/// Metoda pro smazani radku v hlavičke expedice
		/// </summary>
		/// <param name="idterminal">ID Terminalu</param>
		/// <param name="uzivatel">ID uživatele</param>
		/// <param name="skl_id">ID Skladu</param>
		/// <param name="hlavickaID">ID Hlavičky</param>
		/// <returns>ID chyby</returns>
		[WebMethod(Description = "Metoda pro smazani radku v hlavičke expedice")]
		public int Expedice_Hlavicka_Del(byte idterminal, int uzivatel, string skl_id, Guid hlavickaID)
		{
			try
			{
				if (provider != null && provider is Fask.Server.Interfaces.Expedice.IExpedice)
				{
					Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();
					Fask.Server.Interfaces.Classes.Sklad sklad = new Fask.Server.Interfaces.Classes.Sklad();
					User user = new User();

					terminal.ID = idterminal;
					sklad.ID = skl_id;
					user.ID = uzivatel;

					StatusInfo si = provider.Expedice_Hlavicka_Del(
						terminal,
						user,
						sklad,
						hlavickaID
						);

					return si.ID;
				}
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				throw ex;
			}

			string msg = "Provider v Expedice.asmx > 'Expedice_Hlavicka_Del(byte idterminal, int uzivatel, string skl_id, Guid hlavickaID)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, msg);
			throw new Exception(msg);
		}

		/// <summary>
		/// Metoda ktera vraci hlavičky z expedice
		/// </summary>
		/// <param name="idterminal">ID Terminalu</param>
		/// <param name="uzivatel">ID uživatele</param>
		/// <param name="prefixskladu">Prefix Skladu</param>
		/// <returns>Dataset Naplnen hlavičkama</returns>
		[WebMethod(Description = "Metoda ktera vraci hlavičky z expedice")]
		public Fask.Server.Interfaces.DataSets.ExpediceHlavicky Expedice_GetHlavicky(byte idterminal, int uzivatel, string prefixskladu)
		{
			try
			{
				if (provider != null && provider is Fask.Server.Interfaces.Expedice.IExpedice)
				{
					Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();
					Fask.Server.Interfaces.Classes.Sklad sklad = new Fask.Server.Interfaces.Classes.Sklad();
					User user = new User();

					terminal.ID = idterminal;
					sklad.ID = prefixskladu;
					user.ID = uzivatel;

					return provider.Expedice_GetHlavicky(
						terminal,
						user,
						sklad
						);
				}
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				throw ex;
			}

			string msg = "Provider v Expedice.asmx > 'Expedice_GetHlavicky(byte idterminal, int uzivatel, string prefixskladu)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, msg);
			throw new Exception(msg);
		}

		/// <summary>
		/// Metoda která vrací palety na expedici
		/// </summary>
		/// <param name="idterminal">ID Terminalu</param>
		/// <param name="uzivatel">ID Uživatele</param>
		/// <param name="skl_id">ID Skladu</param>
		/// <param name="hlavickaID">Guid hlavičky</param>
		/// <returns>Dataset Expedice naplnen paletama</returns>
		[WebMethod(Description = "Metoda která vrací palety na expedici")]
		public Fask.Server.Interfaces.DataSets.Expedice Expedice_GetPalety(byte idterminal, int uzivatel, string skl_id, Guid hlavickaID)
		{
			try
			{
				if (provider != null && provider is Fask.Server.Interfaces.Expedice.IExpedice)
				{
					Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();
					Fask.Server.Interfaces.Classes.Sklad sklad = new Fask.Server.Interfaces.Classes.Sklad();
					User user = new User();

					terminal.ID = idterminal;
					sklad.ID = skl_id;
					user.ID = uzivatel;

					return provider.Expedice_GetPalety(
						terminal,
						user,
						sklad,
						hlavickaID
						);
				}
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				throw ex;
			}

			string msg = "Provider v Expedice.asmx > 'Expedice_GetPalety(byte idterminal, int uzivatel, string skl_id, Guid hlavickaID)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, msg);
			throw new Exception(msg);
		}

		/// <summary>
		/// Metoda která vraci palety podle čísla palety
		/// </summary>
		/// <param name="idterminal">ID Terminalu</param>
		/// <param name="uzivatel">ID uživatele</param>
		/// <param name="skl_id">ID Skladu</param>
		/// <param name="nmbrpal">číslo palety</param>
		/// <returns>Expedice, naplnene položkama pro paletu</returns>
		[WebMethod(Description = "Metoda která vraci palety podle čísla palety")]
		public Fask.Server.Interfaces.DataSets.Expedice Expedice_Paleta_Get(byte idterminal, int uzivatel, string skl_id, string nmbrpal)
		{
			try
			{
				if (provider != null && provider is Fask.Server.Interfaces.Expedice.IExpedice)
				{
					Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();
					Fask.Server.Interfaces.Classes.Sklad sklad = new Fask.Server.Interfaces.Classes.Sklad();
					User user = new User();

					terminal.ID = idterminal;
					sklad.ID = skl_id;
					user.ID = uzivatel;

					return provider.Expedice_Paleta_Get(
						terminal,
						user,
						skl_id,
						nmbrpal
						);
				}
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				throw ex;
			}

			string msg = "Provider v Expedice.asmx > 'Expedice_Paleta_Get(byte idterminal, int uzivatel, string skl_id, string nmbrpal)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, msg);
			throw new Exception(msg);
		}

		/// <summary>
		/// Metoda pro pridany položky na paletu
		/// </summary>
		/// <param name="idterminal">ID Terminalu</param>
		/// <param name="uzivatel">ID uživatele</param>
		/// <param name="skl_id">ID  Skladu</param>
		/// <param name="hlavickaID">Guid hlavičky</param>
		/// <param name="nmbrpal">číslo paletz</param>
		/// <returns>ID Chyby</returns>
		[WebMethod(Description = "Metoda pro pridany položky na paletu")]
		public int Expedice_Polozka_Add(byte idterminal, int uzivatel, string skl_id, Guid hlavickaID, string nmbrpal)
		{
			try
			{
				if (provider != null && provider is Fask.Server.Interfaces.Expedice.IExpedice)
				{
					Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();
					Fask.Server.Interfaces.Classes.Sklad sklad = new Fask.Server.Interfaces.Classes.Sklad();
					User user = new User();

					terminal.ID = idterminal;
					sklad.ID = skl_id;
					user.ID = uzivatel;

					StatusInfo si = provider.Expedice_Polozka_Add(
						hlavickaID,
						terminal,
						user,
						sklad,
						nmbrpal
						);

					return si.ID;
				}

			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				throw ex;
			}

			string msg = "Provider v Expedice.asmx > 'Expedice_Polozka_Add(byte idterminal, int uzivatel, string skl_id, Guid hlavickaID, string nmbrpal)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, msg);
			throw new Exception(msg);
		}

		/// <summary>
		/// Metoda pro smazani položky z palety
		/// </summary>
		/// <param name="idterminal">ID Terminalu</param>
		/// <param name="uzivatel">ID uživatele</param>
		/// <param name="skl_id">ID Skladu</param>
		/// <param name="hlavickaID">Guid Hlavičky</param>
		/// <param name="nmbrpal">číslo palety</param>
		/// <returns>ID chyby</returns>
		[WebMethod(Description = "Metoda pro smazani položky z palety")]
		public int Expedice_Polozka_Del(byte idterminal, int uzivatel, string skl_id, Guid hlavickaID, string nmbrpal)
		{
			try
			{
				if (provider != null && provider is Fask.Server.Interfaces.Expedice.IExpedice)
				{
					Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();
					Fask.Server.Interfaces.Classes.Sklad sklad = new Fask.Server.Interfaces.Classes.Sklad();
					User user = new User();

					terminal.ID = idterminal;
					sklad.ID = skl_id;
					user.ID = uzivatel;

					StatusInfo si = provider.Expedice_Polozka_Del(
						hlavickaID,
						terminal,
						user,
						nmbrpal
						);

					return si.ID;
				}
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				throw ex;
			}

			string msg = "Provider v Expedice.asmx > 'Expedice_Polozka_Del(byte idterminal, int uzivatel, string skl_id, Guid hlavickaID, string nmbrpal)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, msg);
			throw new Exception(msg);
		}

		/// <summary>
		/// Metoda pro zpracovaní davky expedce na serveru
		/// </summary>
		/// <param name="idterminal">ID Terminalu</param>
		/// <param name="uzivatel">ID uživatele</param>
		/// <param name="skl_id">ID Skladu</param>
		/// <param name="hlavicka">Guid hlavičky</param>
		/// <param name="processExpediceState">příznak co se ma stat: Uvolnit, ZpracovatAPokracovat, Zpracovat </param>
		/// <returns></returns>
		[WebMethod(Description = "Metoda pro zpracovaní davky expedce na serveru")]
		public Fask.Server.Interfaces.Classes.StatusObject Expedice_Process(byte idterminal, int uzivatel, string skl_id, Guid hlavicka, Fask.Server.Interfaces.Expedice.ProcessState processExpediceState)
		{
			try
			{
				if (provider != null && provider is Fask.Server.Interfaces.Expedice.IExpedice)
				{
					Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();
					Fask.Server.Interfaces.Classes.Sklad sklad = new Fask.Server.Interfaces.Classes.Sklad();
					User user = new User();

					terminal.ID = idterminal;
					sklad.ID = skl_id;
					user.ID = uzivatel;

					StatusObject so = provider.Expedice_Process(
						hlavicka,
						user,
						terminal,
						sklad,
						processExpediceState
						);

					return so;
				}
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				throw ex;
			}

			string msg = "Provider v Expedice.asmx > 'Expedice_Process(byte idterminal, int uzivatel, string skl_id, Guid hlavicka, Fask.Server.Interfaces.Expedice.ProcessState processExpediceState)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, msg);
			throw new Exception(msg);
		}

		
		#endregion

		#region SSCC

		/// <summary>
		/// Metoda pro generovani SSCC kodu
		/// </summary>
		/// <param name="idterminal">ID Terminalu</param>
		/// <param name="uzivatel">ID uživatele</param>
		/// <param name="skl_id">ID Skladu</param>
		/// <returns>Objekt SSCC s vygenerovanim kodem</returns>
		[WebMethod(Description = "Metoda pro generovani SSCC kodu")]
		public Fask.Server.Interfaces.BarCodes.SSCC SSCC_Generovat(byte idterminal, int uzivatel, string skl_id)
		{
			try
			{
				if (provider != null && provider is Fask.Server.Interfaces.Expedice.IExpedice)
				{
					Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();
					Fask.Server.Interfaces.Classes.Sklad sklad = new Fask.Server.Interfaces.Classes.Sklad();
					User user = new User();

					terminal.ID = idterminal;
					sklad.ID = skl_id;
					user.ID = uzivatel;

					return provider.Expedice_SSCC_Generovat(
						terminal,
						user
						);
				}
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				throw ex;
			}

			string msg = "Provider v Expedice.asmx > 'SSCC_Generovat(byte idterminal, int uzivatel, string skl_id)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, msg);
			throw new Exception(msg);
		}

		/// <summary>
		/// Metoda pro najdeni konkretneho SSCC kodu u položek
		/// </summary>
		/// <param name="idterminal">ID Terminalu</param>
		/// <param name="uzivatel">uživatel</param>
		/// <param name="skl_id">ID Skladu</param>
		/// <param name="hlavickaID">Guid hlavičky</param>
		/// <param name="code">SSCC??</param>
		/// <returns>objekt SSCC naplnen</returns>
		[WebMethod(Description = "Metoda pro najdeni konkretneho SSCC kodu u položek")]
		public Fask.Server.Interfaces.BarCodes.SSCC SSCC_Get(byte idterminal, int uzivatel, string skl_id, Guid hlavickaID, string code)
		{
			try
			{
				if (provider != null && provider is Fask.Server.Interfaces.Expedice.IExpedice)
				{
					Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();
					Fask.Server.Interfaces.Classes.Sklad sklad = new Fask.Server.Interfaces.Classes.Sklad();
					User user = new User();

					terminal.ID = idterminal;
					sklad.ID = skl_id;
					user.ID = uzivatel;

					return provider.Expedice_SSCC_Get(
						terminal,
						user,
						skl_id,
						hlavickaID,
						code
						);
				}
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				throw ex;
			}

			string msg = "Provider v Expedice.asmx > 'SSCC_Get(byte idterminal, int uzivatel, string skl_id, Guid hlavickaID, string code)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, msg);
			throw new Exception(msg);
		}
		
		#endregion
		
		#endregion
	
	}
}
