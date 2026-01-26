using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Fask.Logging;
using System.Reflection;
using Fask.Interfaces.DataSets;

namespace Fask.MST_W_Server
{
	/// <summary>
	/// Služba přenosu dat ProdejeTest
	/// </summary>
	[WebService(Namespace = "http://ProdejTest.fask.cz/", Description = "Služba přenosu dat ProdejeTest")]
	//[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	//[System.ComponentModel.ToolboxItem(false)]
	// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
	// [System.Web.Script.Services.ScriptService]
	public class ProdejTest : System.Web.Services.WebService
	{

		#region Lokalne promenne

		private Fask.Server.Interfaces.Prodej.IProdej provider = null; 
		
		#endregion

		#region Konstruktor

		/// <summary>
		/// Konstruktor
		/// </summary>
		public ProdejTest()
		{
			// Inicializuje objektove rozhrani ...
			try
			{
                Konfigurace.Classes.Globals_Konfig_WebConfig.LoadConfiguration();
                string providerAssemblyPath = Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Providers[0].Provider_Prodej;
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
								if (typeof(Fask.Server.Interfaces.Prodej.IProdej).IsAssignableFrom(t))
								{
									provider = (Fask.Server.Interfaces.Prodej.IProdej)providerAssemlby.CreateInstance(t.FullName);
									if (provider != null)
										break;
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

		#region WebMetody

		/// <summary>
		/// Metoda která ma pevne nadefinovane dve položky a zavolá Prodej_Process
		/// </summary>
		/// <returns>Status OK anebo chybu</returns>
		[WebMethod(Description = "Metoda která ma pevne nadefinovane dve položky a zavolá Prodej_Process")]
		public string TEST_ImportDokladFull()
		{
			try
			{
				Fask.Server.Interfaces.Classes.Davka d = new Fask.Server.Interfaces.Classes.Davka();
				d.Description = "";
				d.ID = 110008;


				Fask.DataSets.ProdejData ds = new Fask.DataSets.ProdejData();
				Fask.DataSets.ProdejData.CZMST_DIRow row = ds.CZMST_DI.NewCZMST_DIRow();

				row.CountEntries = 110008;		//OK
				row.ODB_ID = "36349";			//OK
				row.STR_ID = string.Empty;		//OK          
				row.DOC_ID = "11";				//OK
				row.ITEMNMBR = "59185";			//OK
				row.LOCNCODE = string.Empty;    //OK
				row.QTYSHPPD = 1;				//OK
				row.QTYPACK = 0;				//OK
				row.SERLTNUM = string.Empty;	//OK
				row.TAXAMPIE = 0;				//OK
				row.AMOUNPIE = 0;				//OK
				row.WITHTAX = 0;				//OK
				row.PRICEX = 0;					//OK
				row.REZ_1 = string.Empty;		//OK
				row.REZ_2 = string.Empty;		//OK
				row.REZ_3 = string.Empty;		//OK
				row.REZ_4 = string.Empty;		//OK
				row.USER_ID = 1;				//OK
				row.DATEDONE = "20181126";		//OK
				row.TIMEDONE = "112300";		//OK         
				//row.DEX_ROW_ID = "";            
				row.guid = Guid.NewGuid();		//OK
				row.VNDITNUM = "1094633886014"; //OK
				row.CZ_CarKod = "";             //OK
				row.SKL_ID = "";				//OK           
				row.MJ = "ks";					//OK
				row.QTYSHPPDMJ = 1;				//OK
				row.PRAC_ID = string.Empty;		//OK    
				row.INPUT_MODE = 1;				//OK
				row.ID_TERMINAL = 99;			//OK
				row.TYPEPAL = "";				//??
				row.NMBRPAL = "";				//??
				row.SetITEMCODENull();			//OK 
				row.DOC_ID2 = string.Empty;		//OK           
				row.Setmena_IDNull();			//OK
				row.SetTAXAMPIEMNull();			//OK 
				row.SetAMOUNPIEMNull();			//OK
				row.Setmena_IDMNull();			//OK   
				row.LOCNCODEDEST = "0";			//OK        
				row.SKL_ID_DEST = "2";			//OK
				row.WEIGHT = 0;					//??
				row.PRINTED = 0;				//??
				row.ITEMDESC = "-";				//??

				ds.CZMST_DI.AddCZMST_DIRow(row);


				Fask.DataSets.ProdejData.CZMST_DIRow row2 = ds.CZMST_DI.NewCZMST_DIRow();

				row2.CountEntries = 110008;		//OK
				row2.ODB_ID = "36349";			//OK
				row2.STR_ID = string.Empty;		//OK          
				row2.DOC_ID = "11";				//OK
				row2.ITEMNMBR = "59185";			//OK
				row2.LOCNCODE = string.Empty;    //OK
				row2.QTYSHPPD = 1;				//OK
				row2.QTYPACK = 0;				//OK
				row2.SERLTNUM = string.Empty;	//OK
				row2.TAXAMPIE = 0;				//OK
				row2.AMOUNPIE = 0;				//OK
				row2.WITHTAX = 0;				//OK
				row2.PRICEX = 0;					//OK
				row2.REZ_1 = string.Empty;		//OK
				row2.REZ_2 = string.Empty;		//OK
				row2.REZ_3 = string.Empty;		//OK
				row2.REZ_4 = string.Empty;		//OK
				row2.USER_ID = 1;				//OK
				row2.DATEDONE = "20181126";		//OK
				row2.TIMEDONE = "112301";		//OK         
				//row.DEX_ROW_ID = "";            
				row2.guid = Guid.NewGuid();		//OK
				row2.VNDITNUM = "1094633886014"; //OK
				row2.CZ_CarKod = "";             //OK
				row2.SKL_ID = "";				//OK           
				row2.MJ = "ks";					//OK
				row2.QTYSHPPDMJ = 1;				//OK
				row2.PRAC_ID = string.Empty;		//OK    
				row2.INPUT_MODE = 1;				//OK
				row2.ID_TERMINAL = 99;			//OK
				row2.TYPEPAL = "";				//??
				row2.NMBRPAL = "";				//??
				row2.SetITEMCODENull();			//OK 
				row2.DOC_ID2 = string.Empty;		//OK           
				row2.Setmena_IDNull();			//OK
				row2.SetTAXAMPIEMNull();			//OK 
				row2.SetAMOUNPIEMNull();			//OK
				row2.Setmena_IDMNull();			//OK   
				row2.LOCNCODEDEST = "0";			//OK        
				row2.SKL_ID_DEST = "2";			//OK
				row2.WEIGHT = 0;					//??
				row2.PRINTED = 0;				//??
				row2.ITEMDESC = "-";				//??

				ds.CZMST_DI.AddCZMST_DIRow(row2);


				Fask.Server.Interfaces.Classes.User u = new Fask.Server.Interfaces.Classes.User();
				u.Login = "1";
				u.firstname = "a";
				u.secondname = "b";
				u.barcode = "123";

				Fask.Server.Interfaces.Classes.Terminal t = new Fask.Server.Interfaces.Classes.Terminal();
				t.ID = 99;


				var processStatus = ((Fask.Server.Interfaces.Prodej.IProdej)provider).Prodej_Process(d, t, u, ds);

				return "OK";

			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				throw ex;
			}


		}

		/// <summary>
		/// Metoda která ma pevne nadefinovane dve položky ale je možno zadat ID dokladu ktere rozhoduje jaky doklad vznikne v IS pohoda. Zavolá Prodej_Process
		/// </summary>
		/// <param name="DokladID">ID Dokladu</param>
		[WebMethod(Description = "Metoda která ma pevne nadefinovane dve položky ale je možno zadat ID dokladu ktere rozhoduje jaky doklad vznikne v IS pohoda. Zavolá Prodej_Process")]
		public void TEST_ImportDoklad(string DokladID)
		{
			try
			{

				Fask.Server.Interfaces.Classes.Davka d = new Fask.Server.Interfaces.Classes.Davka();
				d.Description = "";
				d.ID = 0;


				Fask.DataSets.ProdejData ds = new Fask.DataSets.ProdejData();
				Fask.DataSets.ProdejData.CZMST_DIRow row = ds.CZMST_DI.NewCZMST_DIRow();

				row.CountEntries = 110008;		//OK
				row.ODB_ID = "40999";			//OK
				row.STR_ID = string.Empty;		//OK          
				row.DOC_ID = DokladID;				//OK
				row.ITEMNMBR = "59185";			//OK
				row.LOCNCODE = string.Empty;    //OK
				row.QTYSHPPD = 1;				//OK
				row.QTYPACK = 0;				//OK
				row.SERLTNUM = string.Empty;	//OK
				row.TAXAMPIE = 0;				//OK
				row.AMOUNPIE = 0;				//OK
				row.WITHTAX = 0;				//OK
				row.PRICEX = 0;					//OK
				row.REZ_1 = string.Empty;		//OK
				row.REZ_2 = string.Empty;		//OK
				row.REZ_3 = string.Empty;		//OK
				row.REZ_4 = string.Empty;		//OK
				row.USER_ID = 1;				//OK
				row.DATEDONE = "20181126";		//OK
				row.TIMEDONE = "112300";		//OK         
				//row.DEX_ROW_ID = "";            
				row.guid = Guid.NewGuid();		//OK
				row.VNDITNUM = "1094633886014"; //OK
				row.CZ_CarKod = "";             //OK
				row.SKL_ID = "";				//OK           
				row.MJ = "ks";					//OK
				row.QTYSHPPDMJ = 1;				//OK
				row.PRAC_ID = string.Empty;		//OK    
				row.INPUT_MODE = 1;				//OK
				row.ID_TERMINAL = 99;			//OK
				row.TYPEPAL = "";				//??
				row.NMBRPAL = "";				//??
				row.SetITEMCODENull();			//OK 
				row.DOC_ID2 = string.Empty;		//OK           
				row.Setmena_IDNull();			//OK
				row.SetTAXAMPIEMNull();			//OK 
				row.SetAMOUNPIEMNull();			//OK
				row.Setmena_IDMNull();			//OK   
				row.LOCNCODEDEST = "0";			//OK        
				row.SKL_ID_DEST = "2";			//OK
				row.WEIGHT = 0;					//??
				row.PRINTED = 0;				//??
				row.ITEMDESC = "-";				//??

				ds.CZMST_DI.AddCZMST_DIRow(row);


				Fask.DataSets.ProdejData.CZMST_DIRow row2 = ds.CZMST_DI.NewCZMST_DIRow();

				row2.CountEntries = 110008;		//OK
				row2.ODB_ID = "40999";			//OK
				row2.STR_ID = string.Empty;		//OK          
				row2.DOC_ID = DokladID;				//OK
				row2.ITEMNMBR = "59185";			//OK
				row2.LOCNCODE = string.Empty;    //OK
				row2.QTYSHPPD = 1;				//OK
				row2.QTYPACK = 0;				//OK
				row2.SERLTNUM = string.Empty;	//OK
				row2.TAXAMPIE = 0;				//OK
				row2.AMOUNPIE = 0;				//OK
				row2.WITHTAX = 0;				//OK
				row2.PRICEX = 0;					//OK
				row2.REZ_1 = string.Empty;		//OK
				row2.REZ_2 = string.Empty;		//OK
				row2.REZ_3 = string.Empty;		//OK
				row2.REZ_4 = string.Empty;		//OK
				row2.USER_ID = 1;				//OK
				row2.DATEDONE = "20181126";		//OK
				row2.TIMEDONE = "112301";		//OK         
				//row.DEX_ROW_ID = "";            
				row2.guid = Guid.NewGuid();		//OK
				row2.VNDITNUM = "1094633886014"; //OK
				row2.CZ_CarKod = "";             //OK
				row2.SKL_ID = "";				//OK           
				row2.MJ = "ks";					//OK
				row2.QTYSHPPDMJ = 1;				//OK
				row2.PRAC_ID = string.Empty;		//OK    
				row2.INPUT_MODE = 1;				//OK
				row2.ID_TERMINAL = 99;			//OK
				row2.TYPEPAL = "";				//??
				row2.NMBRPAL = "";				//??
				row2.SetITEMCODENull();			//OK 
				row2.DOC_ID2 = string.Empty;		//OK           
				row2.Setmena_IDNull();			//OK
				row2.SetTAXAMPIEMNull();			//OK 
				row2.SetAMOUNPIEMNull();			//OK
				row2.Setmena_IDMNull();			//OK   
				row2.LOCNCODEDEST = "0";			//OK        
				row2.SKL_ID_DEST = "2";			//OK
				row2.WEIGHT = 0;					//??
				row2.PRINTED = 0;				//??
				row2.ITEMDESC = "-";				//??

				ds.CZMST_DI.AddCZMST_DIRow(row2);


				Fask.Server.Interfaces.Classes.User u = new Fask.Server.Interfaces.Classes.User();
				u.Login = "1";
				u.firstname = "a";
				u.secondname = "b";
				u.barcode = "123";

				TEST_ImportDoklad(d, ds, u);

			}
			catch (Exception ex)
			{
				//return ex.Message;
				throw ex;
			}
		}

		/// <summary>
		/// Metoda která ma pevne nadefinovane dve položky ale je možno zadat ID dokladu ktere rozhoduje jaky doklad vznikne v IS pohoda a 49slo davky, ktere určuje ID Requestu. Zavolá Prodej_Process
		/// </summary>
		/// <param name="DocID">ID Dokladu</param>
		/// <param name="countentries">číslo dávky</param>
		[WebMethod(Description = "Metoda která ma pevne nadefinovane dve položky ale je možno zadat ID dokladu ktere rozhoduje jaky doklad vznikne v IS pohoda a 49slo davky, ktere určuje ID Requestu. Zavolá Prodej_Process")]
		public void TEST_ImportDoklad_DocID(string DocID, int countentries)
		{
			try
			{

				Fask.Server.Interfaces.Classes.Davka d = new Fask.Server.Interfaces.Classes.Davka();
				d.Description = "";
				d.ID = 0;


				Fask.DataSets.ProdejData ds = new Fask.DataSets.ProdejData();
				Fask.DataSets.ProdejData.CZMST_DIRow row = ds.CZMST_DI.NewCZMST_DIRow();

				row.CountEntries = countentries;		//OK
				row.ODB_ID = "36349";			//OK
				row.STR_ID = string.Empty;		//OK          
				row.DOC_ID = DocID;				//OK
				row.ITEMNMBR = "59185";			//OK
				row.LOCNCODE = string.Empty;    //OK
				row.QTYSHPPD = 1;				//OK
				row.QTYPACK = 0;				//OK
				row.SERLTNUM = string.Empty;	//OK
				row.TAXAMPIE = 0;				//OK
				row.AMOUNPIE = 0;				//OK
				row.WITHTAX = 0;				//OK
				row.PRICEX = 0;					//OK
				row.REZ_1 = string.Empty;		//OK
				row.REZ_2 = string.Empty;		//OK
				row.REZ_3 = string.Empty;		//OK
				row.REZ_4 = string.Empty;		//OK
				row.USER_ID = 1;				//OK
				row.DATEDONE = "20181126";		//OK
				row.TIMEDONE = "112300";		//OK         
				//row.DEX_ROW_ID = "";            
				row.guid = Guid.NewGuid();		//OK
				row.VNDITNUM = "1094633886014"; //OK
				row.CZ_CarKod = "";             //OK
				row.SKL_ID = "";				//OK           
				row.MJ = "ks";					//OK
				row.QTYSHPPDMJ = 1;				//OK
				row.PRAC_ID = string.Empty;		//OK    
				row.INPUT_MODE = 1;				//OK
				row.ID_TERMINAL = 99;			//OK
				row.TYPEPAL = "";				//??
				row.NMBRPAL = "";				//??
				row.SetITEMCODENull();			//OK 
				row.DOC_ID2 = string.Empty;		//OK           
				row.Setmena_IDNull();			//OK
				row.SetTAXAMPIEMNull();			//OK 
				row.SetAMOUNPIEMNull();			//OK
				row.Setmena_IDMNull();			//OK   
				row.LOCNCODEDEST = "0";			//OK        
				row.SKL_ID_DEST = "2";			//OK
				row.WEIGHT = 0;					//??
				row.PRINTED = 0;				//??
				row.ITEMDESC = "-";				//??

				ds.CZMST_DI.AddCZMST_DIRow(row);


				Fask.DataSets.ProdejData.CZMST_DIRow row2 = ds.CZMST_DI.NewCZMST_DIRow();

				row2.CountEntries = countentries;		//OK
				row2.ODB_ID = "36349";			//OK
				row2.STR_ID = string.Empty;		//OK          
				row2.DOC_ID = DocID;				//OK
				row2.ITEMNMBR = "59185";			//OK
				row2.LOCNCODE = string.Empty;    //OK
				row2.QTYSHPPD = 1;				//OK
				row2.QTYPACK = 0;				//OK
				row2.SERLTNUM = string.Empty;	//OK
				row2.TAXAMPIE = 0;				//OK
				row2.AMOUNPIE = 0;				//OK
				row2.WITHTAX = 0;				//OK
				row2.PRICEX = 0;					//OK
				row2.REZ_1 = string.Empty;		//OK
				row2.REZ_2 = string.Empty;		//OK
				row2.REZ_3 = string.Empty;		//OK
				row2.REZ_4 = string.Empty;		//OK
				row2.USER_ID = 1;				//OK
				row2.DATEDONE = "20181126";		//OK
				row2.TIMEDONE = "112301";		//OK         
				//row.DEX_ROW_ID = "";            
				row2.guid = Guid.NewGuid();		//OK
				row2.VNDITNUM = "1094633886014"; //OK
				row2.CZ_CarKod = "";             //OK
				row2.SKL_ID = "";				//OK           
				row2.MJ = "ks";					//OK
				row2.QTYSHPPDMJ = 1;				//OK
				row2.PRAC_ID = string.Empty;		//OK    
				row2.INPUT_MODE = 1;				//OK
				row2.ID_TERMINAL = 99;			//OK
				row2.TYPEPAL = "";				//??
				row2.NMBRPAL = "";				//??
				row2.SetITEMCODENull();			//OK 
				row2.DOC_ID2 = string.Empty;		//OK           
				row2.Setmena_IDNull();			//OK
				row2.SetTAXAMPIEMNull();			//OK 
				row2.SetAMOUNPIEMNull();			//OK
				row2.Setmena_IDMNull();			//OK   
				row2.LOCNCODEDEST = "0";			//OK        
				row2.SKL_ID_DEST = "2";			//OK
				row2.WEIGHT = 0;					//??
				row2.PRINTED = 0;				//??
				row2.ITEMDESC = "-";				//??

				ds.CZMST_DI.AddCZMST_DIRow(row2);


				Fask.Server.Interfaces.Classes.User u = new Fask.Server.Interfaces.Classes.User();
				u.Login = "1";
				u.firstname = "a";
				u.secondname = "b";
				u.barcode = "123";

				TEST_ImportDoklad(d, ds, u);

			}
			catch (Exception ex)
			{
				//return ex.Message;
				throw ex;
			}
		}

		/// <summary>
		/// Metoda která provede tisk zadaneho dokladu na zadanou šablonu přímo z pohody
		/// </summary>
		/// <param name="agenda">Agenda z ktere se ma tisknout doklad</param>
		/// <param name="ID_Dokladu">ID Dokladu(zistit na SQL)</param>
		/// <param name="ID_Sablony">ID šablony(zistit v POHODE)</param>
		/// <param name="PocetKopii">počet vytisku</param>
		/// <param name="NazevTiskarny">Nazev tiskarny</param>
		/// <returns>StatusResult - Objekt ktery nese informace o prubehu</returns>
		[WebMethod(Description = "Metoda která provede tisk zadaneho dokladu na zadanou šablonu přímo z pohody. Seznam Agend : adresar, interni_doklady, vydane_nabidky, prijate_nabidky, ostatni_pohledavky,ostatni_zavazky, pokladna, vydane_poptavky, prijate_poptavky, prevod, prijemky, prijate_faktury, prijate_objednavky, prijate_zalohove_faktury, prodejky, vydane_faktury, vydane_objednavky, vydane_zalohove_faktury, vydejky, vyroba, zakazky, zasoby,")]
		public Fask.Server.Interfaces.Classes.StatusResult TiskPOHODA(string agenda, int ID_Dokladu, int ID_Sablony, int PocetKopii, string NazevTiskarny)
		{
			Fask.Server.Interfaces.Classes.StatusResult sr = new Fask.Server.Interfaces.Classes.StatusResult();


			try
			{

				if ((provider != null) && (provider is Fask.Server.Interfaces.Prodej.IProdej_TiskPOHODA))
					sr  = ((Fask.Server.Interfaces.Prodej.IProdej_TiskPOHODA)provider).TiskPOHODA(agenda, ID_Dokladu, ID_Sablony, PocetKopii, NazevTiskarny);
				else
					throw new NotImplementedException("Neimplementovan provider pro IProdej_TiskPOHODA");

			}
			catch (Exception ex)
			{
				sr.Message = ex.Message;
				sr.Status = Fask.Server.Interfaces.Classes.StatusResultEnum.ERROR;
				return sr;
			}

			return sr;

		}


		/// <summary>
		/// Metoda pro otestovaní zavolaní Fuknce pro vraceni dodavatele podle seznamu čarovych kodu
		/// </summary>
		/// <param name="IDTerm">ID Terminalu</param>
		/// <param name="SKLID">ID Skladu</param>
		/// <param name="ITEMTYPE">Typ položky</param>
		/// <param name="Kod1">č. Kod 1</param>
		/// <param name="Kod2">č. Kod 2</param>
		/// <param name="Kod3">č. Kod 3</param>
		/// <returns>Dataset Odberatele naplnen nalezenyma odberatelama</returns>
		[WebMethod(Description = "Metoda pro otestovaní zavolaní Fuknce pro vraceni dodavatele podle seznamu čarovych kodu")]
		public Odberatele Test_Fun_FASK_GetDodavatelByCarKody(byte IDTerm, string SKLID, string ITEMTYPE, string Kod1, string Kod2, string Kod3)
		{
			try
			{
				Prodej p = new Prodej();
				List<string> l = new List<string>();

				if (!string.IsNullOrEmpty(Kod1.Trim()))
					l.Add(Kod1);

				if (!string.IsNullOrEmpty(Kod2.Trim()))
					l.Add(Kod2);


				if (!string.IsNullOrEmpty(Kod3.Trim()))
					l.Add(Kod3);

				return p.Online_GetSeznamDodavatele_Vyber(IDTerm, SKLID, ITEMTYPE, l);
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				throw ex;
			}

		}

		/// <summary>
		/// Metoda pro otestovani Vraceni skladu
		/// </summary>
		/// <param name="doc_id">ID Typu dokladu</param>
		/// <param name="itemnmbr">ID položky</param>
		/// <param name="serltnum">seriove čislo/ šarže</param>
		/// <returns>Status se skladama</returns>
		[WebMethod(Description = "Metoda pro otestovani Vraceni skladu")]
		public string GetSkladTest(string doc_id, string itemnmbr, string serltnum)
		{
			string result = string.Empty;
			try
			{
				Prodej P = new Prodej();
				Fask.Server.Interfaces.Classes.STATUS status = Fask.Server.Interfaces.Classes.STATUS.ERROR;
				string skl_id = string.Empty;
				string skl_id_dest = string.Empty;
				status = P.GetSklad(1, 2, doc_id, itemnmbr, serltnum, out skl_id, out skl_id_dest);

				result = "Status: " + status.ToString() + ", skl_id: " + skl_id + ", skl_id_dest:" + skl_id_dest;

				return result;
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				throw;
			}
		}

		[WebMethod(Description = "Jedna se o online metodu, ktera ma byt univerzalna podle typu dokladu...")]
		public Fask.Server.Interfaces.Classes_OnlineKomunikace.VystupniObjekt Online_UniverzalnyDotazNaCokoliv_TEST(string ITEMNMBR, string MnozstviNasnimane, string MnozstviZadane)
		{

			Fask.Server.Interfaces.Classes_OnlineKomunikace.VstupniObjekt ObjektIN = new Fask.Server.Interfaces.Classes_OnlineKomunikace.VstupniObjekt();
			Prodej P = new Prodej();

			ObjektIN.ITEMNMBR = ITEMNMBR;
			ObjektIN.MnozstviNasnimane = decimal.Parse(MnozstviNasnimane, System.Globalization.CultureInfo.InvariantCulture);
			ObjektIN.MnozstviZadane = decimal.Parse(MnozstviZadane, System.Globalization.CultureInfo.InvariantCulture);

			return P.Online_UniverzalnyDotazNaCokoliv(ObjektIN);

		}

		[WebMethod(Description = "Testovací metoda která vola after akci po odeslani davky")]
		public bool AfterProcessedAction_TEST(int CisloDavky)
		{
			bool sr = true;

			try
			{
				Fask.Server.Interfaces.Classes.Davka davka = new Fask.Server.Interfaces.Classes.Davka();
				davka.ID = CisloDavky;
	
				if ((provider != null) && (provider is Fask.Server.Interfaces.Prodej.IProdej))
					sr = ((Fask.Server.Interfaces.Prodej.IProdej)provider).Prodej_AfterProcessedAction(davka);
				else
					throw new NotImplementedException("Neimplementovan provider pro IProdej");

			}
			catch (Exception ex)
			{
				throw ex;
			}

			return sr;
		}

		
		#endregion

		#region Privatne metody

		/// <summary>
		/// Metoda která zavolá Import do pohody.
		/// </summary>
		/// <param name="d">Davka</param>
		/// <param name="ds">Data pro import</param>
		/// <param name="u">uživatel</param>
		/// <returns>Status o provedeni</returns>
		private string TEST_ImportDoklad(Fask.Server.Interfaces.Classes.Davka d, Fask.DataSets.ProdejData ds, Fask.Server.Interfaces.Classes.User u)
		{
			try
			{
				if ((provider != null) && (provider is Fask.Server.Interfaces.Prodej.IProdej))
				{
					return (provider as Fask.Server.Interfaces.Prodej.IProdej).Prodej_Import_to_IS(d, ds, u);
				}
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				throw ex;
			}

			throw new Exception("Chyby nadefinovany provider...");
		}

		#endregion

	}
}
