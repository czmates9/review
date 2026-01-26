using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using MST_Print_Server_ZPL_Printing;
using System.Data;
using System.Drawing.Printing;

namespace Fask.MST_W_Server
{
	/// <summary>
	/// Služba pro tisk Testy.
	/// </summary>
	[WebService(Namespace = "http://TiskTest.fask.cz/", Description = "Služba pro tisk.")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
	// [System.Web.Script.Services.ScriptService]
	public class TiskTest : System.Web.Services.WebService
	{

		#region Konstruktor
		/// <summary>
		/// Konstruktor
		/// </summary>
		public TiskTest()
		{
			//co tady? :D 
		}
		
		#endregion

		#region WebMetody

		/// <summary>
		/// Proběhne tisk etikety z CZMST_PI tabulky
		/// </summary>
		/// <param name="terminalID">ID Terminalu</param>
		/// <param name="templateName">Název šablony</param>
		/// <param name="pocetVytisku">počet vytisku</param>
		/// <param name="ip">IP tiskarny</param>
		/// <param name="ip_port">Port tiskarny</param>
		/// <param name="name">nazev tiskarny</param>
		/// <param name="com_port">COM port</param>
		/// <returns>True - tisk proběhl, False - chyba</returns>
		[WebMethod(Description = "Proběhne tisk etikety z CZMST_PI tabulky.")]
		public bool TEST_EtiketaTisk(int terminalID, string templateName, int pocetVytisku, string ip, string ip_port, string name, string com_port)
		{
			bool state;
			Fask.DataSets.Prijem dsPrijem = new Fask.DataSets.Prijem();
			Fask.Server.Interfaces.DataSets.DSValues data = new Fask.Server.Interfaces.DataSets.DSValues();
			Tisk T = new Tisk();

			try
			{
				#region Vstupni testovací data

				var rowPE = dsPrijem.CZMST_PE.NewCZMST_PERow();

				rowPE.CountEntries = 1;
				rowPE.PONUMBER = "184000014";
				rowPE.ITEMNMBR = "65019";
				rowPE.ITEMDESC = "Adventure Menu Rýžový nákyp se švestkami 250g";
				rowPE.ORD = 342639;
				rowPE.VNDDOCNM = "";
				rowPE.VNDITNUM = "7297210432407";
				rowPE.CZ_CarKod = "ADV012";
				rowPE.SKL_ID = "1";
				rowPE.LOCNCODE = "";
				rowPE.MJ = "ks";
				rowPE.QTYSHPPD = 100;
				rowPE.QTYPACK = 0;
				rowPE.CZ_DatVyr_Track = 0;
				rowPE.CZ_DatVyr_Delka = 0;
				rowPE.CZ_SerNum_Track = 0;
				rowPE.CZ_SerNum_Delka = 0;
				rowPE.CZ_SW_Track = 0;
				rowPE.CZ_SW_Delka = 0;
				rowPE.CZ_Doslo = 0;
				rowPE.DEX_ROW_ID = 1;
				//rowPE.WEIGHT= "";
				//rowPE.NMBRPAL= "";
				//rowPE.TYPEPAL= "";
				//rowPE.ITEMCODE= "";
				//rowPE.SERLTNUM= "";
				//rowPE.CZ_REZ1_Track= "";
				//rowPE.CZ_REZ2_Track= "";

				rowPE.CZ_Expirace_Track = 0;

				dsPrijem.CZMST_PE.AddCZMST_PERow(rowPE);


				var rowPI = dsPrijem.CZMST_PI.NewCZMST_PIRow();


				rowPI.CountEntries = 1;
				rowPI.PONUMBER = "184000014";
				rowPI.ORD = 342639;
				rowPI.ITEMNMBR = "65019";
				rowPI.VNDDOCNM = "";
				rowPI.VNDITNUM = "8594043790056";
				rowPI.SKL_ID = "1";
				rowPI.LOCNCODE = "0";
				rowPI.MJ = "ks";
				rowPI.QTYSHPPD = 100;
				rowPI.QTYSHPPDMJ = 100;
				rowPI.QTYPACK = 0;
				rowPI.SERLTNUM = "";
				rowPI.KOD_SW = "";
				rowPI.DAT_VYROBY = "";
				rowPI.DATEDONE = "20180516";
				rowPI.TIMEDONE = "180049";
				rowPI.CZ_CarKod = "ADV012";
				rowPI.REZ_1 = "";
				rowPI.REZ_2 = "";
				rowPI.USER_ID = 1;
				rowPI.DEX_ROW_ID = 1;
				rowPI.GUID = new Guid("4F2C7471-C638-4DD8-A592-1847380B97AE");
				rowPI.INPUT_MODE = 2;
				rowPI.ID_TERMINAL = 11;

				dsPrijem.CZMST_PI.AddCZMST_PIRow(rowPI); 
				#endregion
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				throw ex;
			}

			try
			{
				MST_Print_Server_ZPL_Printing.TiskParams printerParams = new TiskParams(ip, ip_port, name, com_port);

				data.Values.BeginLoadData();
				foreach (DataColumn item in dsPrijem.CZMST_PI.Columns)
				{
					data.Values.AddValuesRow(item.ColumnName.ToUpper(), dsPrijem.CZMST_PI[0][item.ColumnName].ToString());
				}
				data.Values.EndLoadData();
				data.AcceptChanges();

				

				state = T.Etiketa(terminalID, templateName, printerParams, data, pocetVytisku);
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				throw ex;
			}

			return state;
		}

		/// <summary>
		/// Metoda pro Testovani tisku soupisu
		/// </summary>
		/// <param name="terminalID">ID terminalu</param>
		/// <param name="template">nazev šablony</param>
		/// <param name="PrinterName">Nazev tiskarny</param>
		/// <param name="pocet">počet vytisku</param>
		/// <returns>True - Tisk proběhl, False- chyba</returns>
		[WebMethod(Description = "Metoda pro Testovani tisku soupisu")]
		public bool SoupisTisk_Test(
			int terminalID,
			string CountEntries,
			string SOPNUMBE,
			string template,
			string PrinterName,
			int pocet)
		{
			Tisk T = new Tisk();
			MST_Print_Server_ZPL_Printing.TiskParams printerParams = new TiskParams();
			Fask.Server.Interfaces.DataSets.DSValues dataHeader = new Fask.Server.Interfaces.DataSets.DSValues();
			List<Fask.Server.Interfaces.DataSets.DSValues> dataRowList = new List<Fask.Server.Interfaces.DataSets.DSValues>();
			Fask.Server.Interfaces.DataSets.DSValues dataFooter = new Fask.Server.Interfaces.DataSets.DSValues();

			printerParams.CONFIG_NAME = PrinterName; // "HP LaserJet 2430 PCL6 Class Driver";


            dataHeader.Values.AddValuesRow("NAME", "TestNAME");
            dataHeader.Values.AddValuesRow("STREET", "TestSTREET");
            dataHeader.Values.AddValuesRow("POSTCODE", "TestPOSTCODE");
            dataHeader.Values.AddValuesRow("COUNTRY", "TestCOUNTRY");
            dataHeader.Values.AddValuesRow("CITY", "TestCITY");
            dataHeader.Values.AddValuesRow("SOPNUMBE", SOPNUMBE);
			dataHeader.Values.AddValuesRow("CountEntries", CountEntries);
			dataHeader.Values.AddValuesRow("FIRSTNAME", "TestTest");
			dataHeader.Values.AddValuesRow("SECONDNAME", "SkuskaSkuska");



			//dataHeader.Values.AddValuesRow("CountEntries", "110055");
			//dataHeader.Values.AddValuesRow("CountEntries", "110055");
			//dataHeader.Values.AddValuesRow("CountEntries", "110055");

			decimal QTY = 2;
			List<Dictionary<string, string>> dataRowListDic = new List<Dictionary<string, string>>();

			for (int i = 0; i < 10; i++)
			{
				Dictionary<string, string> dataRadek = new Dictionary<string, string>();
				dataRadek.Add("ITEMNMBR", "123456" );
				dataRadek.Add("VNDITNUM", "8596011330333" );
				dataRadek.Add("CZ_CarKod", "8888888");
				dataRadek.Add("ITEMCODE", "555");
				dataRadek.Add("ITEMDESC", "POkusne zboži jedna");
				dataRadek.Add("QTYSHPPD", QTY.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
				dataRadek.Add("QTYSHPPD_DIF", QTY.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));

				dataRadek.Add("SOPNUMBE", SOPNUMBE);
				dataRadek.Add("NMBRPAL", "00005643210000000045");
				dataRadek.Add("DIMENSIONWIDTH", "100");
				dataRadek.Add("DIMENSIONHEIGHT", "200");
				dataRadek.Add("DIMENSIONDEPTH", "300");
				dataRadek.Add("GROSSWEIGHT", "400");
				dataRowListDic.Add(dataRadek);

			}

			QTY = 3;

			for (int i = 0; i < 2; i++)
			{
				Dictionary<string, string> dataRadek = new Dictionary<string, string>();
				dataRadek.Add("ITEMNMBR", "544684861");
				dataRadek.Add("VNDITNUM", "8596011330333");
				dataRadek.Add("CZ_CarKod", "2222");
				dataRadek.Add("ITEMCODE", "666");
				dataRadek.Add("ITEMDESC", "POkusne zboži dva");
				dataRadek.Add("QTYSHPPD", QTY.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
				dataRadek.Add("QTYSHPPD_DIF", QTY.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));

				dataRadek.Add("SOPNUMBE", SOPNUMBE);
				dataRadek.Add("NMBRPAL", "00005643210000000045");
				dataRadek.Add("DIMENSIONWIDTH", "100");
				dataRadek.Add("DIMENSIONHEIGHT", "200");
				dataRadek.Add("DIMENSIONDEPTH", "300");
				dataRadek.Add("GROSSWEIGHT", "400");
				dataRowListDic.Add(dataRadek);
			}

			QTY = 4;

			for (int i = 0; i < 5; i++)
			{
				Dictionary<string, string> dataRadek = new Dictionary<string, string>();
				dataRadek.Add("ITEMNMBR", "8789465456");
				dataRadek.Add("VNDITNUM", "8596011330333");
				dataRadek.Add("CZ_CarKod", "111");
				dataRadek.Add("ITEMCODE", "777");
				dataRadek.Add("ITEMDESC", "POkusne zboži tri");
				dataRadek.Add("QTYSHPPD", QTY.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
				dataRadek.Add("QTYSHPPD_DIF", QTY.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));

				dataRadek.Add("SOPNUMBE", SOPNUMBE);
				dataRadek.Add("NMBRPAL", "00005643210000000056");
				dataRadek.Add("DIMENSIONWIDTH", "900");
				dataRadek.Add("DIMENSIONHEIGHT", "800");
				dataRadek.Add("DIMENSIONDEPTH", "700");
				dataRadek.Add("GROSSWEIGHT", "600");
				dataRowListDic.Add(dataRadek);
			}

			foreach (var item in dataRowListDic)
			{
				dataRowList.Add(prepareTiskValues(item));
			}

			return T.Soupis(
				terminalID,
				printerParams,
				dataHeader,
				dataRowList,
				dataFooter,
				template,
				"r",
				"f",
				pocet);

		}

		/// <summary>
		/// Metoda pro Testovani tisku soupisu
		/// </summary>
		/// <param name="terminalID">ID terminalu</param>
		/// <param name="template">nazev šablony</param>
		/// <param name="PrinterName">Nazev tiskarny</param>
		/// <param name="pocet">počet vytisku</param>
		/// <returns>True - Tisk proběhl, False- chyba</returns>
		[WebMethod(Description = "Metoda pro Testovani tisku soupisu")]
		public bool SoupisTisk_TestVicObjednavek(
			int terminalID,
			string CountEntries,
			string template,
			string PrinterName,
			int pocet)
		{
			Tisk T = new Tisk();
			MST_Print_Server_ZPL_Printing.TiskParams printerParams = new TiskParams();
			Fask.Server.Interfaces.DataSets.DSValues dataHeader = new Fask.Server.Interfaces.DataSets.DSValues();
			List<Fask.Server.Interfaces.DataSets.DSValues> dataRowList = new List<Fask.Server.Interfaces.DataSets.DSValues>();
			Fask.Server.Interfaces.DataSets.DSValues dataFooter = new Fask.Server.Interfaces.DataSets.DSValues();

			printerParams.CONFIG_NAME = PrinterName; // "HP LaserJet 2430 PCL6 Class Driver";


			dataHeader.Values.AddValuesRow("NAME", "TestNAME");
			dataHeader.Values.AddValuesRow("STREET", "TestSTREET");
			dataHeader.Values.AddValuesRow("POSTCODE", "TestPOSTCODE");
			dataHeader.Values.AddValuesRow("COUNTRY", "TestCOUNTRY");
			dataHeader.Values.AddValuesRow("CITY", "TestCITY");
			dataHeader.Values.AddValuesRow("CountEntries", CountEntries);
			dataHeader.Values.AddValuesRow("FIRSTNAME", "TestTest");
			dataHeader.Values.AddValuesRow("SECONDNAME", "SkuskaSkuska");

			//dataHeader.Values.AddValuesRow("CountEntries", "110055");
			//dataHeader.Values.AddValuesRow("CountEntries", "110055");
			//dataHeader.Values.AddValuesRow("CountEntries", "110055");

			decimal QTY = 2;
			List<Dictionary<string, string>> dataRowListDic = new List<Dictionary<string, string>>();

			string SOPNUMBE = "123OBJ";
			int cntSOP = 0;

			for (int j = 0; j < 3; j++)
			{



				for (int i = 0; i < 2; i++)
				{
					Dictionary<string, string> dataRadek = new Dictionary<string, string>();
					dataRadek.Add("SOPNUMBE", SOPNUMBE + cntSOP.ToString("0000"));
					dataRadek.Add("ITEMNMBR", "123456" + i.ToString());
					dataRadek.Add("VNDITNUM", "8596011330333" + i.ToString());
					dataRadek.Add("CZ_CarKod", "8888888" + i.ToString());
					dataRadek.Add("ITEMCODE", "555" + i.ToString());
					dataRadek.Add("MJ", "ks");
					dataRadek.Add("ITEMDESC", "Bacha, Saluber A463 Thermo mají stejný čárák pro velikosti. Naskladni je dle faktury ručně" + i.ToString());
					dataRadek.Add("QTYSHPPD", QTY.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
					dataRadek.Add("QTYSHPPD_DIF", QTY.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
					dataRowListDic.Add(dataRadek);

				}

				QTY = 3;

				for (int i = 0; i < 2; i++)
				{
					Dictionary<string, string> dataRadek = new Dictionary<string, string>();
					dataRadek.Add("SOPNUMBE", SOPNUMBE + cntSOP.ToString("0000"));
					dataRadek.Add("ITEMNMBR", "544684861" + i.ToString());
					dataRadek.Add("VNDITNUM", "8596011330333" + i.ToString());
					dataRadek.Add("CZ_CarKod", "2222" + i.ToString());
					dataRadek.Add("ITEMCODE", "666" + i.ToString());
					dataRadek.Add("MJ", "ks");
					dataRadek.Add("ITEMDESC", "POkusne zboži dva" + i.ToString());
					dataRadek.Add("QTYSHPPD", QTY.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
					dataRadek.Add("QTYSHPPD_DIF", QTY.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
					dataRowListDic.Add(dataRadek);
				}

				QTY = 4;

				for (int i = 0; i < 3; i++)
				{
					Dictionary<string, string> dataRadek = new Dictionary<string, string>();
					dataRadek.Add("SOPNUMBE", SOPNUMBE + cntSOP.ToString("0000"));
					dataRadek.Add("ITEMNMBR", "8789465456" + i.ToString());
					dataRadek.Add("VNDITNUM", "8596011330333" + i.ToString());
					dataRadek.Add("CZ_CarKod", "111" + i.ToString());
					dataRadek.Add("ITEMCODE", "777" + i.ToString());
					dataRadek.Add("MJ", "ks");
					dataRadek.Add("ITEMDESC", "POkusne zboži tri" + i.ToString());
					dataRadek.Add("QTYSHPPD", QTY.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
					dataRadek.Add("QTYSHPPD_DIF", QTY.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
					dataRowListDic.Add(dataRadek);
				}

				cntSOP++;

			}

			foreach (var item in dataRowListDic)
			{
				dataRowList.Add(prepareTiskValues(item));
			}

			return T.Soupis(
				terminalID,
				printerParams,
				dataHeader,
				dataRowList,
				dataFooter,
				template,
				"r",
				"f",
				pocet);

		}


		/// <summary>
		/// Metoda která vrací seznam všech nainstalovanych tiskaren
		/// </summary>
		/// <returns>Netypov dataset všech tiskaren nainstalovanych</returns>
		[WebMethod(Description = "Metoda která vrací seznam všech nainstalovanych tiskaren")]
		public DataTable Printers()
		{
			DataTable dt = new DataTable();
			try
			{
				dt.TableName = "Printers";
				dt.Columns.Add("Name");
				dt.Columns.Add("Valid");

				foreach (string pname in PrinterSettings.InstalledPrinters)
				{
					PrinterSettings ps = new PrinterSettings();
					ps.PrinterName = pname;

					DataRow dr = dt.NewRow();
					dr["Name"] = pname;
					dr["Valid"] = ps.IsValid.ToString();

					dt.Rows.Add(dr);
				}
				dt.AcceptChanges();
			}
			catch (System.Exception ex)
			{
				Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
			}
			return dt;
		}

[WebMethod]
		public void TiskKonkretnyUlohy(string NamePrinter, 
		string PathToZPL_HEAD,
		string PathToZPL_ROW1,
		string PathToZPL_ROW2,
		string PathToZPL_ROW3,
		string PathToZPL_FOOR
		)
		{

			try
			{
                Konfigurace.Classes.Globals_Konfig_WebConfig.LoadConfiguration();


                if (string.IsNullOrEmpty(PathToZPL_HEAD))
					throw new Exception("Chyba!!! Zadej hlavičku");

				MST_Print_Server_ZPL_Printing.TiskParams printerParams = new TiskParams();

				printerParams.CONFIG_NAME = NamePrinter;


				List<string> lst = new List<string>();

				try
				{
					lst.Add(System.IO.File.ReadAllText(PathToZPL_HEAD));

					if (!string.IsNullOrEmpty(PathToZPL_ROW1))
					{
						lst.Add(System.IO.File.ReadAllText(PathToZPL_ROW1));
					}

					if (!string.IsNullOrEmpty(PathToZPL_ROW2))
					{
						lst.Add(System.IO.File.ReadAllText(PathToZPL_ROW2));
					}

					if (!string.IsNullOrEmpty(PathToZPL_ROW3))
					{
						lst.Add(System.IO.File.ReadAllText(PathToZPL_ROW3));
					}

					if (!string.IsNullOrEmpty(PathToZPL_FOOR))
					{
						lst.Add(System.IO.File.ReadAllText(PathToZPL_FOOR));
					}
				}
				catch (Exception ex)
				{
					throw ex;
				}
				

				MST_Print_Server_ZPL_Printing.StringPrinting sprint = new MST_Print_Server_ZPL_Printing.StringPrinting(printerParams);
				sprint.PrintEncodingPage = Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.PaletovyListek[0].PrintEncodingPage;
				sprint.user = new User(
                    Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.PaletovyListek[0].UserName,
                    Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.PaletovyListek[0].UserPassword,
                    Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.PaletovyListek[0].DomainName);
				sprint.Print(lst, "MST Print TEST");
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}


		[WebMethod(Description = "Metoda která vrací cestu k souboru ConfigPrinter.xml ")]
		public string GetPathTo_ConfigPrinter()
		{
			string rootpath = Server.MapPath("~");
			string filepath = System.IO.Path.Combine(rootpath, @"ConfigPrinter.xml");
			return filepath;
		}

		[WebMethod(Description = "Metoda která vrací cestu k souboru ConfigPrinter.xml ")]
		public string TiskEtiketa_SuperTestMetoda(string Tiskarna, string SablonaCesta_i_s_Nazvem ,int PocetVytisku, string ITEMNMBR, string ITEMCODE, string ITEMDESC, string LOCNCODE, string SERLTNUM, string VNDITNUM, string EXPIRACE)
		{
			Fask.Server.Interfaces.DataSets.DSValues data = new Fask.Server.Interfaces.DataSets.DSValues();
			Tisk T = new Tisk();

			try
			{
				TiskParams printerParams = new TiskParams() { CONFIG_NAME = Tiskarna };

				data.Values.BeginLoadData();

				DoplnHodnotu(data, "ITEMNMBR", ITEMNMBR);
				DoplnHodnotu(data, "ITEMCODE", ITEMCODE);
				DoplnHodnotu(data, "ITEMDESC", ITEMDESC);
				DoplnHodnotu(data, "LOCNCODE", LOCNCODE);
				DoplnHodnotu(data, "SERLTNUM", SERLTNUM);
				DoplnHodnotu(data, "VNDITNUM", VNDITNUM);
				DoplnHodnotu(data, "EXPIRACE", EXPIRACE);

				data.Values.EndLoadData();
				data.AcceptChanges();


				var state = T.Etiketa(99, SablonaCesta_i_s_Nazvem, printerParams, data, PocetVytisku);
				if (!state)
					throw new Exception("Neco je špatne...");
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				throw ex;
			}

			return "OK Uspešně vytisknuto :)";
		}



		#endregion

		#region Private metody

		/// <summary>
		/// Metoda ktera prekopiruje data z Dictionary do Datasetu DSValues
		/// </summary>
		/// <param name="data">Vstupne data v Dictionary</param>
		/// <returns>Vystup v Datasetu  DSValues</returns>
		private Fask.Server.Interfaces.DataSets.DSValues prepareTiskValues(Dictionary<string, string> data)
		{
			Fask.Server.Interfaces.DataSets.DSValues tiskValues = new Fask.Server.Interfaces.DataSets.DSValues();
			tiskValues.Values.BeginLoadData();
			foreach (var item in data)
			{
				tiskValues.Values.AddValuesRow(item.Key, item.Value);
			}
			tiskValues.Values.EndLoadData();
			tiskValues.AcceptChanges();
			return tiskValues;
		}

		private void DoplnHodnotu(Fask.Server.Interfaces.DataSets.DSValues data, string Key , string Value)
		{
			if (!string.IsNullOrEmpty(Value))
			{
				data.Values.AddValuesRow(Key.ToUpper(), Value);
			}
		}
		
		#endregion

	
	}
}
