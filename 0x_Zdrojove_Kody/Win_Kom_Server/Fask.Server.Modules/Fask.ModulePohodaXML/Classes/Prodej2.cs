using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml;
using System.IO;

namespace Fask.SQL.Classes
{

	#region Puvodni definice z XML konfiguračneho souboru

	//    <doklad docid="0" funkce="pri" docid2="" sklid="" idsradatext=""/>
	//<doklad docid="1" funkce="vyd" docid2="" sklid="" idsradatext=""/>
	//<doklad docid="2" funkce="pro" docid2="" sklid="" idsradatext=""/>
	//<doklad docid="3" funkce="objv" docid2="" sklid="" idsradatext=""/>
	//<doklad docid="4" funkce="objvcm" docid2="" sklid="" idsradatext=""/>
	//<doklad docid="5" funkce="objvm" docid2="" sklid="" idsradatext=""/>
	//<doklad docid="6" funkce="objbezodb" docid2="" sklid="" idsradatext=""/>
	//<doklad docid="7" funkce="objp" docid2="" sklid="" idsradatext="Objednávky EUROM"/>
	//<doklad docid="8" funkce="objpcm" docid2="" sklid="" idsradatext=""/>
	//<doklad docid="9" funkce="objpm" docid2="" sklid="" idsradatext=""/>
	//<doklad docid="10" funkce="expedice" docid2="" sklid="" idsradatext=""/>
	//<doklad docid="11" funkce="pre" docid2="" sklid="" idsradatext=""/>

	#endregion

	/// <summary>
	/// Enum ktere definuje typy podporovanych dokladu
	/// </summary>
	public enum TypDokladu
	{
		vyd,		//0
		pri,		//1
		pro,		//2
		objv,		//3
		objvcm,		//4
		objvm,		//5
		objbezodb,	//6
		objp,		//7
		objpcm,		//8
		objpm,		//9
		expedice,	//10
		pre,		//11
		fv,			//12
		vydvr,      //13
		Unknow
	}

	public static class Prodej2
	{
		#region Konstantny názvy souboru XML requestu ktere vznikaji

		public const string prodej_unknown = XML.MST_Pohoda._unknown + ".xml";
		public const string prodej_import_vydejka = XML.MST_Pohoda._import_vydejka + ".xml";
		public const string prodej_import_prijemka = XML.MST_Pohoda._import_prijemka + ".xml";
		public const string prodej_import_prodejka = XML.MST_Pohoda._import_prodejka + ".xml";
		public const string prodej_import_prevodka = XML.MST_Pohoda._import_prevodka + ".xml";
		public const string prodej_import_objp = XML.MST_Pohoda._import_objednavka_prijata + ".xml";
		public const string prodej_import_objv = XML.MST_Pohoda._import_objednavka_vydana + ".xml";
		public const string prodej_import_fv = XML.MST_Pohoda._import_faktura_vydana + ".xml";

		#endregion


		#region Pomocne metody

		/// <summary>
		/// Metoda sloužíci pro vraceni nazvu souboru podle typu dokladu
		/// </summary>
		/// <param name="typDoklad"> Enum Typ Dokladu</param>
		/// <returns></returns>
		public static string FilenameComposeTypDoklad(TypDokladu typDoklad)
		{
			try
			{
				switch (typDoklad)
				{
					case TypDokladu.vyd:
						return XML.MST_Pohoda.FilenameCompose_FullPath(prodej_import_vydejka);
					case TypDokladu.pri:
						return XML.MST_Pohoda.FilenameCompose_FullPath(prodej_import_prijemka);
					case TypDokladu.pro:
						return XML.MST_Pohoda.FilenameCompose_FullPath(prodej_import_prodejka);
					case TypDokladu.objv:
						return XML.MST_Pohoda.FilenameCompose_FullPath(prodej_import_objv);
					case TypDokladu.objvcm:
						return XML.MST_Pohoda.FilenameCompose_FullPath(prodej_import_objv);
					case TypDokladu.objvm:
						return XML.MST_Pohoda.FilenameCompose_FullPath(prodej_import_objv);
					case TypDokladu.objbezodb:
						return XML.MST_Pohoda.FilenameCompose_FullPath(prodej_import_objv);
					case TypDokladu.objp:
						return XML.MST_Pohoda.FilenameCompose_FullPath(prodej_import_objp);
					case TypDokladu.objpcm:
						return XML.MST_Pohoda.FilenameCompose_FullPath(prodej_import_objp);
					case TypDokladu.objpm:
						return XML.MST_Pohoda.FilenameCompose_FullPath(prodej_import_objp);
					case TypDokladu.expedice:
						return XML.MST_Pohoda.FilenameCompose_FullPath(prodej_import_vydejka);
					case TypDokladu.pre:
						return XML.MST_Pohoda.FilenameCompose_FullPath(prodej_import_prevodka);
					case TypDokladu.fv:
						return XML.MST_Pohoda.FilenameCompose_FullPath(prodej_import_fv);
					default:
						return XML.MST_Pohoda.FilenameCompose_FullPath(prodej_unknown);
				}
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML.Classes.Prodej2", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				return XML.MST_Pohoda.FilenameCompose_FullPath(prodej_unknown);
			}
		}


		private static XElement Vypln_AttributeToSN(XNamespace XName, XNamespace typ, DataSets.ProdejData.CZMST_DIRow row)
		{
			XElement ele = null;
			List<XElement> listVPrParams = new List<XElement>();

			var attr = Database.Pohoda.SKzVC_AttributeToSN(row.ITEMNMBR, row.SERLTNUM);

			if (attr.Count == 1)
			{
				var radek = attr.First();

				if (!radek.IsVPrSarzeKSNNull() && !string.IsNullOrEmpty(radek.VPrSarzeKSN))
				{


					List<XElement> listVPr = new List<XElement>();
					var name = new XElement(typ + "name", "VPrSarzeKSN");
					var textValue = new XElement(typ + "textValue", radek.VPrSarzeKSN);
					listVPr.Add(name);
					listVPr.Add(textValue);

					listVPrParams.Add(new XElement(typ + "parameter", listVPr));
				}


				if (!radek.IsVPrExspiraceKSNNull())
				{
					List<XElement> listVPrEXP = new List<XElement>();
					var nameEXP = new XElement(typ + "name", "VPrExspiraceKSN");
					var textValueEXP = new XElement(typ + "datetimeValue", XmlConvert.ToString(radek.VPrExspiraceKSN, "yyyy-MM-dd"));
					listVPrEXP.Add(nameEXP);
					listVPrEXP.Add(textValueEXP);

					listVPrParams.Add(new XElement(typ + "parameter", listVPrEXP));

				}

				ele = new XElement(XName + "parameters", listVPrParams);

			}

			return ele;
		}

		#endregion

		/***** Tescovaci režim requestu *****/


		#region Univerzalny tisk z Pohody

		/// <summary>
		/// request pomoci nehož ide univerzalne tisknout z IS POHODA
		/// </summary>
		/// <param name="filename">nazev souboru</param>
		/// <param name="agenda">Enum podporovane agendy</param>
		/// <param name="ID_Dokladu"> ID Dokladu</param>
		/// <param name="ID_Sablony">ID šablony</param>
		/// <param name="PocetKopii"> pořet výtisku</param>
		/// <param name="NazevTiskarny">Nazev tiskarny</param>
		/// <returns>true- OK false- chyba</returns>
		internal static bool CreateRequest_TISK_XML(string filename, Classes.printAgendaType agenda, int ID_Dokladu, int ID_Sablony, int PocetKopii, string NazevTiskarny)
		{

			#region Vzor XML

			//<?xml version="1.0" encoding="utf-8"?>
			//<dat:dataPack 
			//xmlns:dat="http://www.stormware.cz/schema/version_2/data.xsd" 
			//xmlns:flt="http://www.stormware.cz/schema/version_2/filter.xsd" 
			//xmlns:typ="http://www.stormware.cz/schema/version_2/type.xsd"
			//xmlns:prn="http://www.stormware.cz/schema/version_2/print.xsd" 
			//id="25aa9554-67f4-4b52-a4ec-e2fde29cf94e" ico="04535510" application="MST_Pohoda" version="2.0" note="TISK vydejky">
			//  <dat:dataPackItem id="25aa9554-67f4-4b52-a4ec-e2fde29cf94e" version="2.0">
			//<prn:print version="1.0">
			//  <prn:record agenda = "vydejky">
			//      <flt:filter>
			//      <flt:id>10855</flt:id>
			//    </flt:filter>
			//  </prn:record>
			//<prn:printerSettings>
			//  <prn:report>
			//    <prn:id>1169</prn:id>
			//  </prn:report>
			//  <prn:printer>HP LaserJet 2430 PCL6 Class Driver</prn:printer>
			//  <prn:parameters>
			//    <prn:copy>1</prn:copy>
			//  </prn:parameters>
			//</prn:printerSettings>
			//</prn:print>
			//  </dat:dataPackItem>
			//</dat:dataPack>

			#endregion

			Guid ID = Guid.NewGuid();

			System.Globalization.NumberFormatInfo nfi = System.Globalization.NumberFormatInfo.InvariantInfo;

			XNamespace dat = "http://www.stormware.cz/schema/version_2/data.xsd";
			XNamespace flt = "http://www.stormware.cz/schema/version_2/filter.xsd";
			XNamespace typ = "http://www.stormware.cz/schema/version_2/type.xsd";
			XNamespace prn = "http://www.stormware.cz/schema/version_2/print.xsd";

			XElement TiskElement = null;
			XElement recordElement = null;
			XElement printerSettingsElement = null;


			recordElement = new XElement(flt + "filter",
											new XElement(flt + "id", ID_Dokladu.ToString()));


			List<XElement> print = new List<XElement>();
			List<XElement> printSettings = new List<XElement>();

			//Jedná se o ID šablony, pokud neni vyplneno tak je to chyba
			printSettings.Add(new XElement(prn + "report", new XElement(prn + "id", ID_Sablony.ToString())));

			//pocet vytisku projistotu je napevno jeden...
			printSettings.Add(new XElement(prn + "parameters", new XElement(prn + "copy", PocetKopii)));

			//dotahuje se vždy, pokud je vyplnena tak se vytiskne na nu, pokud je to prazdne tak se tiskne na defaultní
			printSettings.Add(new XElement(prn + "printer", NazevTiskarny));

			printerSettingsElement = new XElement(prn + "printerSettings", printSettings);




			TiskElement = new XElement(prn + "print",
										new XAttribute("version", "1.0"),
										new XElement(prn + "record", new XAttribute("agenda", agenda.ToString()), recordElement),
										printerSettingsElement
										);



			XElement root = new XElement(dat + "dataPack",
			new XAttribute(XNamespace.Xmlns + "dat", "http://www.stormware.cz/schema/version_2/data.xsd"),
			new XAttribute(XNamespace.Xmlns + "flt", "http://www.stormware.cz/schema/version_2/filter.xsd"),
			new XAttribute(XNamespace.Xmlns + "typ", "http://www.stormware.cz/schema/version_2/type.xsd"),
			new XAttribute(XNamespace.Xmlns + "prn", "http://www.stormware.cz/schema/version_2/print.xsd"),
			new XAttribute("id", ID.ToString()),
			new XAttribute("ico", Globals_V1.Konfigurace.PohodaInfo[0].ICO),
			new XAttribute("application", Fask.SQL.Constants.Common.application_S),
			new XAttribute("version", "2.0"),
			new XAttribute("note", "Tisk " + agenda.ToString()),
			new XElement(dat + "dataPackItem",
			new XAttribute("id", ID.ToString()),
			new XAttribute("version", "2.0"),
			TiskElement
			));


			root.Save(filename);

			return true;

		}

		/// <summary>
		/// čteni responsu z pohody
		/// </summary>
		/// <param name="filename">cesta k souboru</param>
		/// <returns>vraci bud OK anebo chybu</returns>
		internal static string LoadResponse_TISK_XML(string filename)
		{
			XML.MST_Pohoda.CheckExistResponseFile(filename);

			return "OK";
		}
		
		#endregion


		/*****  Aktivne se použivaji ******/

		#region Přijemka

		/// <summary>
		/// Metoda pro Vytvořeni requestu Prijemky z volneho pohybu
		/// </summary>
		/// <param name="filename">Nazev souboru</param>
		/// <param name="note">poznamka</param>
		/// <param name="dt_DI"> Data z CZMST_DI</param>
		/// <returns>Zda je nalezeno nebo ne</returns>
		internal static bool CreateRequest_Pri_XML(string filename, string note, Fask.DataSets.ProdejData.CZMST_DIDataTable dt_DI, Doklad typDoklad)
		{
			System.Globalization.NumberFormatInfo nfi = System.Globalization.NumberFormatInfo.InvariantInfo;

			XNamespace dat = "http://www.stormware.cz/schema/version_2/data.xsd";
			XNamespace pri = "http://www.stormware.cz/schema/version_2/prijemka.xsd";
			XNamespace typ = "http://www.stormware.cz/schema/version_2/type.xsd";
			XNamespace prn = "http://www.stormware.cz/schema/version_2/print.xsd";

			try
			{
				//Fask.DataSets.ProdejData.CZMST_DIRow Row_DI;
				int objednavka = dt_DI[0].CountEntries;

				//var data = dt_DI.OrderBy(x => x.DEX_ROW_ID);
				//Row_DI = data.First();

				string partnerID = string.Empty;
				foreach (Fask.DataSets.ProdejData.CZMST_DIRow row in dt_DI)
				{
					if (!row.IsODB_IDNull() && row.ODB_ID.Trim() != string.Empty)
					{
						//Log.writeErrorLog("ODB_ID: " + row.ODB_ID.ToString());
						partnerID = row.ODB_ID.Trim();
						break;
					}
				}

				//7.1.2026 MaR pridano na zaklade JaS
				string zakazka = string.Empty;

				if ((dt_DI != null && !dt_DI[0].IsZakazka_IDNull()) && (!string.IsNullOrEmpty(dt_DI[0].Zakazka_ID)))
				{
					zakazka = dt_DI[0].Zakazka_ID;
				}

				int? CiziMena = null;

				if (!string.IsNullOrEmpty(partnerID))
				{
					try
					{
						var dt = Database.Pohoda.AD_GetDataByID(int.Parse(partnerID));

						if ((dt != null) && (dt.Count > 0))
						{
							CiziMena = dt.First().RefCM;
						}
					}
					catch
					{
						CiziMena = null;
					}
				}

				#region Hlavicka

				List<XElement> listHeader = new List<XElement>();

				listHeader.Add(new XElement(pri + "text", "MES-MST Vytvořeno z volného pohybu"));

				listHeader.Add(new XElement(pri + "note", objednavka));

				listHeader.Add(new XElement(pri + "date", XmlConvert.ToString(DateTime.Now, "yyyy-MM-dd")));

				if (!string.IsNullOrEmpty(partnerID))
				{
					XElement IDPartnerFakturacni;

					IDPartnerFakturacni = new XElement(typ + "id", partnerID.Trim());

					listHeader.Add(new XElement(pri + "partnerIdentity", IDPartnerFakturacni));
				}

				#region Ciselna Rada

				//if (!string.IsNullOrEmpty(typDoklad.idsradatext))
				//{
				//	listHeader.Add(new XElement(pri + "number", new XElement(typ + "id", typDoklad.idsradatext.Trim())));
				//}

				if (!string.IsNullOrEmpty(typDoklad.Rada_Prefix))
				{
					listHeader.Add(new XElement(pri + "number", new XElement(typ + "ids", typDoklad.Rada_Prefix.Trim())));
				}

				#endregion


				if ((typDoklad != null) && !string.IsNullOrEmpty(typDoklad.Zakazka))
				{
					listHeader.Add(new XElement(pri + "contract", new XElement(typ + "ids", typDoklad.Zakazka.Trim())));
				}
                else if (!string.IsNullOrEmpty(zakazka))
                {
					listHeader.Add(new XElement(pri + "contract", new XElement(typ + "ids", zakazka.Trim())));
				}

				if ((typDoklad != null) && !string.IsNullOrEmpty(typDoklad.Stredisko0))
				{
					listHeader.Add(new XElement(pri + "centre", new XElement(typ + "id", typDoklad.Stredisko0.Trim())));
				}



				if ((typDoklad != null) && !string.IsNullOrEmpty(typDoklad.Cinnost))
				{
					listHeader.Add(new XElement(pri + "activity", new XElement(typ + "id", typDoklad.Cinnost.Trim())));
				}

				#endregion

				#region Itemy

				List<XElement> listItem = new List<XElement>();

				foreach (var row in dt_DI)
				{
					List<XElement> prijemkaItem = new List<XElement>();

					prijemkaItem.Add(new XElement(pri + "quantity", ((float)row.QTYSHPPD).ToString(nfi)));
					prijemkaItem.Add(new XElement(pri + "unit", row.MJ.Trim()));
					prijemkaItem.Add(new XElement(pri + "coefficient", 1));
					prijemkaItem.Add(new XElement(pri + "discountPercentage", 0));

					if (CiziMena.HasValue)
					{

						prijemkaItem.Add(new XElement(pri + "payVAT", "false"));
						prijemkaItem.Add(new XElement(pri + "rateVAT", "none"));
						prijemkaItem.Add(new XElement(pri + "foreignCurrency", new XElement(typ + "unitPrice", "0")));
					}

					if (row.SERLTNUM.Trim().Length > 0)
						prijemkaItem.Add(new XElement(pri + "stockItem", new XElement(typ + "stockItem", new XElement(typ + "id", row.ITEMNMBR.Trim())), new XElement(typ + "serialNumber", row.SERLTNUM.Trim())));
					else
						prijemkaItem.Add(new XElement(pri + "stockItem", new XElement(typ + "stockItem", new XElement(typ + "id", row.ITEMNMBR.Trim()))));

					if (!row.IsEXPIRACENull())
					{
						prijemkaItem.Add(new XElement(pri + "expirationDate", XmlConvert.ToString(row.EXPIRACE, "yyyy-MM-dd")));
					}

					#region AttributeToSN


					List<XElement> listVPrParams = new List<XElement>();

						if (!row.IsAttributeToSNNull() && !string.IsNullOrEmpty(row.AttributeToSN))
						{


							List<XElement> listVPr = new List<XElement>();
							var name = new XElement(typ + "name", "VPrSarzeKSN");
							var textValue = new XElement(typ + "textValue", row.AttributeToSN);
							listVPr.Add(name);
							listVPr.Add(textValue);

							listVPrParams.Add(new XElement(typ + "parameter", listVPr));
						}


						if (!row.IsEXPIRACENull())
						{
							List<XElement> listVPrEXP = new List<XElement>();
							var nameEXP = new XElement(typ + "name", "VPrExspiraceKSN");
							var textValueEXP = new XElement(typ + "datetimeValue", XmlConvert.ToString(row.EXPIRACE, "yyyy-MM-dd"));
							listVPrEXP.Add(nameEXP);
							listVPrEXP.Add(textValueEXP);

							listVPrParams.Add(new XElement(typ + "parameter", listVPrEXP));

						}


                    if (listVPrParams.Count > 0)
                    {
                        prijemkaItem.Add(new XElement(pri + "parameters", listVPrParams)); 
                    }

					#endregion

					XElement polozka = new XElement(pri + "prijemkaItem", prijemkaItem);
					listItem.Add(polozka);
				}

				#endregion

				#region Summary

				List<XElement> prijemkaSummary = new List<XElement>();

				if (CiziMena.HasValue)
				{

					var Kurzdt = Database.Pohoda.sCKurspol_GetDataByKod(CiziMena.Value); ;
					double Kurz = 0;

					if ((Kurzdt != null) && (Kurzdt.Count > 0))
					{
						Kurz = Kurzdt[0].NBs;
						prijemkaSummary.Add(new XElement(pri + "foreignCurrency",
									new XElement(typ + "currency", new XElement(typ + "id", CiziMena.Value.ToString())),
									new XElement(typ + "rate", Kurz)));
					}
					else
					{
						prijemkaSummary.Add(new XElement(pri + "foreignCurrency",
															new XElement(typ + "currency", new XElement(typ + "id", CiziMena.Value.ToString()))));
					}
				}
				#endregion

				XElement PrijemkaElement = null;

				if (typDoklad.Prodej_Prijemka_Tisk_ID_sablona.HasValue && !string.IsNullOrEmpty(typDoklad.Prodej_Prijemka_Tisk_Tiskarna))
				{

					List<XElement> print = new List<XElement>();
					List<XElement> printSettings = new List<XElement>();

					//Jedná se o ID šablony, pokud neni vyplneno tak je to chyba
					printSettings.Add(new XElement(prn + "report", new XElement(prn + "id", typDoklad.Prodej_Prijemka_Tisk_ID_sablona.Value.ToString())));

					//pocet vytisku projistotu je napevno jeden...
					printSettings.Add(new XElement(prn + "parameters", new XElement(prn + "copy", 1)));

					//dotahuje se vždy, pokud je vyplnena tak se vytiskne na nu, pokud je to praydne tak se tiskne na defaultní
					printSettings.Add(new XElement(prn + "printer", typDoklad.Prodej_Prijemka_Tisk_Tiskarna.Trim()));

					print.Add(new XElement(prn + "printerSettings", printSettings));



					PrijemkaElement = new XElement(pri + "prijemka",
													new XAttribute("version", "2.0"),
													new XElement(pri + "prijemkaHeader", listHeader),
													new XElement(pri + "prijemkaDetail", listItem),
													new XElement(pri + "prijemkaSummary", prijemkaSummary),
													new XElement(pri + "print", print));

				}
				else
				{
					PrijemkaElement = new XElement(pri + "prijemka",
							new XAttribute("version", "2.0"),
							new XElement(pri + "prijemkaHeader", listHeader),
							new XElement(pri + "prijemkaDetail", listItem),
							new XElement(pri + "prijemkaSummary", prijemkaSummary)
							);
				}




				XElement root = new XElement(dat + "dataPack",
				new XAttribute(XNamespace.Xmlns + "dat", "http://www.stormware.cz/schema/version_2/data.xsd"),
				new XAttribute(XNamespace.Xmlns + "pri", "http://www.stormware.cz/schema/version_2/prijemka.xsd"),
				new XAttribute(XNamespace.Xmlns + "typ", "http://www.stormware.cz/schema/version_2/type.xsd"),
				new XAttribute("id", "PRI" + objednavka.ToString()),
				new XAttribute("ico", Globals_V1.Konfigurace.PohodaInfo[0].ICO),
				new XAttribute("application", Fask.SQL.Constants.Common.application_S),
				new XAttribute("version", "2.0"),
				new XAttribute("note", "Import prijemky"),

					new XElement(dat + "dataPackItem",
						new XAttribute("id", "PRI" + objednavka.ToString()),
						new XAttribute("version", "2.0"),
						PrijemkaElement));



				root.Save(filename);
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML.Classes.Prodej2", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				return false;
			}

			return true;
		}

		/// <summary>
		/// \TODO predelat na lepsi reakci
		/// </summary>
		/// <param name="filename">cesta k souboru</param>
		/// <param name="uzivatel"></param>
		/// <returns></returns>
		internal static string LoadResponse_Pri_XML(string filename, Fask.Server.Interfaces.Classes.User uzivatel)
		{
			//string file = Globals.PathToInputDirectory + "Response\\" + filename;

			XML.MST_Pohoda.CheckExistResponseFile(filename);

			// \TODO Predelat na lepe.... Kdyz se 

			XML.MST_Pohoda.UpdateCreatorSKPP(uzivatel, filename);


			return "OK";
			//throw new NotImplementedException();
		}

		#endregion

		#region Vydejka

		/// <summary>
		/// Metoda pro Vytvořeni requestu Vydejky z volneho pohybu
		/// </summary>
		/// <param name="filename">Nazev souboru</param>
		/// <param name="p">poznamka</param>
		/// <param name="cZMST_DIDataTable"> Data z CZMST_DI</param>
		/// <returns>Zda je nalezeno nebo ne</returns>
		internal static bool CreateRequest_Vyd_XML(string filename, string p, Fask.DataSets.ProdejData.CZMST_DIDataTable cZMST_DIDataTable, Doklad typDoklad)
		{

			System.Globalization.NumberFormatInfo nfi = System.Globalization.NumberFormatInfo.InvariantInfo;

			XNamespace dat = "http://www.stormware.cz/schema/version_2/data.xsd";
			XNamespace vyd = "http://www.stormware.cz/schema/version_2/vydejka.xsd";
			XNamespace typ = "http://www.stormware.cz/schema/version_2/type.xsd";
			XNamespace prn = "http://www.stormware.cz/schema/version_2/print.xsd";

			try
			{

				int objednavka = cZMST_DIDataTable[0].CountEntries;

				string partnerID = string.Empty;
				foreach (Fask.DataSets.ProdejData.CZMST_DIRow row in cZMST_DIDataTable)
				{
					if (!row.IsODB_IDNull() && row.ODB_ID.Trim() != string.Empty)
					{
						partnerID = row.ODB_ID.Trim();
						break;
					}


				}

				//20251107 MaR pridano na zaklade JaS
				string zakazka = string.Empty;
				//zakazka = "25Zak00001";
				string stredisko = string.Empty;

				if((!cZMST_DIDataTable[0].IsZakazka_IDNull()) && (!string.IsNullOrEmpty(cZMST_DIDataTable[0].Zakazka_ID)))
                {
					zakazka = cZMST_DIDataTable[0].Zakazka_ID;
				}

				if ((!cZMST_DIDataTable[0].IsSTR_IDNull()) && (!string.IsNullOrEmpty(cZMST_DIDataTable[0].STR_ID)))
				{
					stredisko = cZMST_DIDataTable[0].STR_ID;
				}

				#region Hlavicka

				List<XElement> listHeader = new List<XElement>();

				listHeader.Add(new XElement(vyd + "text", "MES-MST Vytvořeno z volného pohybu"));
				listHeader.Add(new XElement(vyd + "note", objednavka));

				listHeader.Add(new XElement(vyd + "date", XmlConvert.ToString(DateTime.Now, "yyyy-MM-dd")));

				if (!string.IsNullOrEmpty(partnerID))
				{
					XElement IDPartnerFakturacni;

					IDPartnerFakturacni = new XElement(typ + "id", partnerID.Trim());

					listHeader.Add(new XElement(vyd + "partnerIdentity", IDPartnerFakturacni));
				}

                //20251107 MaR pridano na zaklade JaS
    //            if (!string.IsNullOrEmpty(zakazka))
    //            {
				//	listHeader.Add(
				//	new XElement(vyd + "contract",
				//		new XElement(typ + "ids", zakazka.Trim())
				//));
				//}

				if ((typDoklad != null) && !string.IsNullOrEmpty(typDoklad.Zakazka))
				{
					listHeader.Add(new XElement(vyd + "contract", new XElement(typ + "ids", typDoklad.Zakazka.Trim())));
				}
				else if (!string.IsNullOrEmpty(zakazka))
				{
					listHeader.Add(new XElement(vyd + "contract", new XElement(typ + "ids", zakazka.Trim())));
				}

				if (!string.IsNullOrEmpty(stredisko))
				{
					listHeader.Add(
					new XElement(vyd + "centre",
						new XElement(typ + "ids", stredisko.Trim())
				));
				}

				#endregion

				#region Itemy

				List<XElement> listItem = new List<XElement>();

				foreach (var row in cZMST_DIDataTable)
				{
					List<XElement> VydejkaItem = new List<XElement>();

                    VydejkaItem.Add(new XElement(vyd + "quantity", ((float)row.QTYSHPPD).ToString(nfi)));

                    //					VydejkaItem.Add(
                    //	new XElement(
                    //		vyd + "quantity",
                    //		(-Math.Abs((float)row.QTYSHPPD)).ToString(nfi)
                    //	)
                    //);


                    VydejkaItem.Add(new XElement(vyd + "unit", row.MJ.Trim()));
					VydejkaItem.Add(new XElement(vyd + "coefficient", 1));
					VydejkaItem.Add(new XElement(vyd + "discountPercentage", 0));

					if (row.SERLTNUM.Trim().Length > 0)
						VydejkaItem.Add(new XElement(vyd + "stockItem", new XElement(typ + "stockItem", new XElement(typ + "id", row.ITEMNMBR.Trim())), new XElement(typ + "serialNumber", row.SERLTNUM.Trim())));
					else
						VydejkaItem.Add(new XElement(vyd + "stockItem", new XElement(typ + "stockItem", new XElement(typ + "id", row.ITEMNMBR.Trim()))));


					if (!row.IsEXPIRACENull())
					{
						VydejkaItem.Add(new XElement(vyd + "expirationDate", XmlConvert.ToString(row.EXPIRACE, "yyyy-MM-dd")));
					}

					#region AttributeToSN

					var ele = Vypln_AttributeToSN(vyd, typ, row);

					if (ele != null)
						VydejkaItem.Add(ele);

					#endregion

					XElement polozka = new XElement(vyd + "vydejkaItem", VydejkaItem);
					listItem.Add(polozka);
				}

				#endregion

				#region Summary

				List<XElement> Summary = new List<XElement>();


				#endregion

				XElement VydjkaElement = null;

				if (typDoklad.Prodej_Vydejka_Tisk_ID_sablona.HasValue && !string.IsNullOrEmpty(typDoklad.Prodej_Vydejka_Tisk_Tiskarna))
				{

					List<XElement> print = new List<XElement>();
					List<XElement> printSettings = new List<XElement>();

					//Jedná se o ID šablony, pokud neni vyplneno tak je to chyba
					printSettings.Add(new XElement(prn + "report", new XElement(prn + "id", typDoklad.Prodej_Vydejka_Tisk_ID_sablona.Value.ToString())));

					//pocet vytisku projistotu je napevno jeden...
					printSettings.Add(new XElement(prn + "parameters", new XElement(prn + "copy", 1)));

					//dotahuje se vždy, pokud je vyplnena tak se vytiskne na nu, pokud je to praydne tak se tiskne na defaultní
					printSettings.Add(new XElement(prn + "printer", typDoklad.Prodej_Vydejka_Tisk_Tiskarna.Trim()));

					print.Add(new XElement(prn + "printerSettings", printSettings));


					VydjkaElement = new XElement(vyd + "vydejka",
							   new XAttribute("version", "2.0"),
							   new XElement(vyd + "vydejkaHeader", listHeader),
							   new XElement(vyd + "vydejkaDetail", listItem),
							   new XElement(vyd + "vydejkaSummary", Summary),
							   new XElement(vyd + "print", print)
								   );
				}
				else
				{
					VydjkaElement = new XElement(vyd + "vydejka",
							   new XAttribute("version", "2.0"),
							   new XElement(vyd + "vydejkaHeader", listHeader),
							   new XElement(vyd + "vydejkaDetail", listItem),
							   new XElement(vyd + "vydejkaSummary", Summary)
								   );
				}

				XElement root = new XElement(dat + "dataPack",
											new XAttribute(XNamespace.Xmlns + "dat", "http://www.stormware.cz/schema/version_2/data.xsd"),
											new XAttribute(XNamespace.Xmlns + "vyd", "http://www.stormware.cz/schema/version_2/vydejka.xsd"),
											new XAttribute(XNamespace.Xmlns + "typ", "http://www.stormware.cz/schema/version_2/type.xsd"),
											new XAttribute("id", "VYD" + objednavka.ToString()),
											new XAttribute("ico", Globals_V1.Konfigurace.PohodaInfo[0].ICO),
											new XAttribute("application", Fask.SQL.Constants.Common.application_S),
											new XAttribute("version", "2.0"),
											new XAttribute("note", "Import vydejky"),
											new XElement(dat + "dataPackItem",
												new XAttribute("id", "VYD" + objednavka.ToString()),
												new XAttribute("version", "2.0"),
											VydjkaElement));

				root.Save(filename);
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML.Classes.Prodej2", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				return false;
			}

			return true;
		}

		/// <summary>
		/// Metoda pro Vytvořeni requestu Vydejky z volneho pohybu
		/// </summary>
		/// <param name="filename">Nazev souboru</param>
		/// <param name="p">poznamka</param>
		/// <param name="cZMST_DIDataTable"> Data z CZMST_DI</param>
		/// <returns>Zda je nalezeno nebo ne</returns>
		internal static bool CreateRequest_Vydvr_XML(string filename, string p, Fask.DataSets.ProdejData.CZMST_DIDataTable cZMST_DIDataTable, Doklad typDoklad)
		{

			System.Globalization.NumberFormatInfo nfi = System.Globalization.NumberFormatInfo.InvariantInfo;

			XNamespace dat = "http://www.stormware.cz/schema/version_2/data.xsd";
			XNamespace vyd = "http://www.stormware.cz/schema/version_2/vydejka.xsd";
			XNamespace typ = "http://www.stormware.cz/schema/version_2/type.xsd";
			XNamespace prn = "http://www.stormware.cz/schema/version_2/print.xsd";

			try
			{

				int objednavka = cZMST_DIDataTable[0].CountEntries;

				string partnerID = string.Empty;
				foreach (Fask.DataSets.ProdejData.CZMST_DIRow row in cZMST_DIDataTable)
				{
					if (!row.IsODB_IDNull() && row.ODB_ID.Trim() != string.Empty)
					{
						partnerID = row.ODB_ID.Trim();
						break;
					}


				}

				//20251107 MaR pridano na zaklade JaS
				string zakazka = string.Empty;
				//zakazka = "25Zak00001";
				string stredisko = string.Empty;

				if ((!cZMST_DIDataTable[0].IsZakazka_IDNull()) && (!string.IsNullOrEmpty(cZMST_DIDataTable[0].Zakazka_ID)))
				{
					zakazka = cZMST_DIDataTable[0].Zakazka_ID;
				}

				if ((!cZMST_DIDataTable[0].IsSTR_IDNull()) && (!string.IsNullOrEmpty(cZMST_DIDataTable[0].STR_ID)))
				{
					stredisko = cZMST_DIDataTable[0].STR_ID;
				}

				#region Hlavicka

				List<XElement> listHeader = new List<XElement>();

				listHeader.Add(new XElement(vyd + "text", "MES-MST Vytvořeno z volného pohybu"));
				listHeader.Add(new XElement(vyd + "note", objednavka));

				listHeader.Add(new XElement(vyd + "date", XmlConvert.ToString(DateTime.Now, "yyyy-MM-dd")));

				if (!string.IsNullOrEmpty(partnerID))
				{
					XElement IDPartnerFakturacni;

					IDPartnerFakturacni = new XElement(typ + "id", partnerID.Trim());

					listHeader.Add(new XElement(vyd + "partnerIdentity", IDPartnerFakturacni));
				}

				//20251107 MaR pridano na zaklade JaS
				//if (!string.IsNullOrEmpty(zakazka))
				//{
				//	listHeader.Add(
				//	new XElement(vyd + "contract",
				//		new XElement(typ + "ids", zakazka.Trim())
				//));
				//}

				if ((typDoklad != null) && !string.IsNullOrEmpty(typDoklad.Zakazka))
				{
					listHeader.Add(new XElement(vyd + "contract", new XElement(typ + "ids", typDoklad.Zakazka.Trim())));
				}
				else if (!string.IsNullOrEmpty(zakazka))
				{
					listHeader.Add(new XElement(vyd + "contract", new XElement(typ + "ids", zakazka.Trim())));
				}


				if (!string.IsNullOrEmpty(stredisko))
				{
					listHeader.Add(
					new XElement(vyd + "centre",
						new XElement(typ + "ids", stredisko.Trim())
				));
				}

				#endregion

				#region Itemy

				List<XElement> listItem = new List<XElement>();

				foreach (var row in cZMST_DIDataTable)
				{
					List<XElement> VydejkaItem = new List<XElement>();

					//VydejkaItem.Add(new XElement(vyd + "quantity", ((float)row.QTYSHPPD).ToString(nfi)));

                    VydejkaItem.Add(
    new XElement(
        vyd + "quantity",
        (-Math.Abs((float)row.QTYSHPPD)).ToString(nfi)
    )
);


                    VydejkaItem.Add(new XElement(vyd + "unit", row.MJ.Trim()));
					VydejkaItem.Add(new XElement(vyd + "coefficient", 1));
					VydejkaItem.Add(new XElement(vyd + "discountPercentage", 0));

					if (row.SERLTNUM.Trim().Length > 0)
						VydejkaItem.Add(new XElement(vyd + "stockItem", new XElement(typ + "stockItem", new XElement(typ + "id", row.ITEMNMBR.Trim())), new XElement(typ + "serialNumber", row.SERLTNUM.Trim())));
					else
						VydejkaItem.Add(new XElement(vyd + "stockItem", new XElement(typ + "stockItem", new XElement(typ + "id", row.ITEMNMBR.Trim()))));


					if (!row.IsEXPIRACENull())
					{
						VydejkaItem.Add(new XElement(vyd + "expirationDate", XmlConvert.ToString(row.EXPIRACE, "yyyy-MM-dd")));
					}

					#region AttributeToSN

					var ele = Vypln_AttributeToSN(vyd, typ, row);

					if (ele != null)
						VydejkaItem.Add(ele);

					#endregion

					XElement polozka = new XElement(vyd + "vydejkaItem", VydejkaItem);
					listItem.Add(polozka);
				}

				#endregion

				#region Summary

				List<XElement> Summary = new List<XElement>();


				#endregion

				XElement VydjkaElement = null;

				if (typDoklad.Prodej_Vydejka_Tisk_ID_sablona.HasValue && !string.IsNullOrEmpty(typDoklad.Prodej_Vydejka_Tisk_Tiskarna))
				{

					List<XElement> print = new List<XElement>();
					List<XElement> printSettings = new List<XElement>();

					//Jedná se o ID šablony, pokud neni vyplneno tak je to chyba
					printSettings.Add(new XElement(prn + "report", new XElement(prn + "id", typDoklad.Prodej_Vydejka_Tisk_ID_sablona.Value.ToString())));

					//pocet vytisku projistotu je napevno jeden...
					printSettings.Add(new XElement(prn + "parameters", new XElement(prn + "copy", 1)));

					//dotahuje se vždy, pokud je vyplnena tak se vytiskne na nu, pokud je to praydne tak se tiskne na defaultní
					printSettings.Add(new XElement(prn + "printer", typDoklad.Prodej_Vydejka_Tisk_Tiskarna.Trim()));

					print.Add(new XElement(prn + "printerSettings", printSettings));


					VydjkaElement = new XElement(vyd + "vydejka",
							   new XAttribute("version", "2.0"),
							   new XElement(vyd + "vydejkaHeader", listHeader),
							   new XElement(vyd + "vydejkaDetail", listItem),
							   new XElement(vyd + "vydejkaSummary", Summary),
							   new XElement(vyd + "print", print)
								   );
				}
				else
				{
					VydjkaElement = new XElement(vyd + "vydejka",
							   new XAttribute("version", "2.0"),
							   new XElement(vyd + "vydejkaHeader", listHeader),
							   new XElement(vyd + "vydejkaDetail", listItem),
							   new XElement(vyd + "vydejkaSummary", Summary)
								   );
				}

				XElement root = new XElement(dat + "dataPack",
											new XAttribute(XNamespace.Xmlns + "dat", "http://www.stormware.cz/schema/version_2/data.xsd"),
											new XAttribute(XNamespace.Xmlns + "vyd", "http://www.stormware.cz/schema/version_2/vydejka.xsd"),
											new XAttribute(XNamespace.Xmlns + "typ", "http://www.stormware.cz/schema/version_2/type.xsd"),
											new XAttribute("id", "VYD" + objednavka.ToString()),
											new XAttribute("ico", Globals_V1.Konfigurace.PohodaInfo[0].ICO),
											new XAttribute("application", Fask.SQL.Constants.Common.application_S),
											new XAttribute("version", "2.0"),
											new XAttribute("note", "Import vydejky"),
											new XElement(dat + "dataPackItem",
												new XAttribute("id", "VYD" + objednavka.ToString()),
												new XAttribute("version", "2.0"),
											VydjkaElement));

				root.Save(filename);
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML.Classes.Prodej2", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				return false;
			}

			return true;
		}


		/// <summary>
		/// Metoda pro Vytvořeni requestu Vydejky z volneho pohybu
		/// </summary>
		/// <param name="filename">Nazev souboru</param>
		/// <param name="p">poznamka</param>
		/// <param name="cZMST_DIDataTable"> Data z CZMST_DI</param>
		/// <returns>Zda je nalezeno nebo ne</returns>
		internal static bool CreateRequest_Vyd_XML_old_20251107(string filename, string p, Fask.DataSets.ProdejData.CZMST_DIDataTable cZMST_DIDataTable, Doklad typDoklad)
		{

			System.Globalization.NumberFormatInfo nfi = System.Globalization.NumberFormatInfo.InvariantInfo;

			XNamespace dat = "http://www.stormware.cz/schema/version_2/data.xsd";
			XNamespace vyd = "http://www.stormware.cz/schema/version_2/vydejka.xsd";
			XNamespace typ = "http://www.stormware.cz/schema/version_2/type.xsd";
			XNamespace prn = "http://www.stormware.cz/schema/version_2/print.xsd";

			try
			{

				int objednavka = cZMST_DIDataTable[0].CountEntries;

				string partnerID = string.Empty;
				foreach (Fask.DataSets.ProdejData.CZMST_DIRow row in cZMST_DIDataTable)
				{
					if (!row.IsODB_IDNull() && row.ODB_ID.Trim() != string.Empty)
					{
						partnerID = row.ODB_ID.Trim();
						break;
					}
				}

				#region Hlavicka

				List<XElement> listHeader = new List<XElement>();

				listHeader.Add(new XElement(vyd + "text", "MST Vytvořeno z volneho pohybu"));
				listHeader.Add(new XElement(vyd + "note", objednavka));

				listHeader.Add(new XElement(vyd + "date", XmlConvert.ToString(DateTime.Now, "yyyy-MM-dd")));

				if (!string.IsNullOrEmpty(partnerID))
				{
					XElement IDPartnerFakturacni;

					IDPartnerFakturacni = new XElement(typ + "id", partnerID.Trim());

					listHeader.Add(new XElement(vyd + "partnerIdentity", IDPartnerFakturacni));
				}


				#endregion

				#region Itemy

				List<XElement> listItem = new List<XElement>();

				foreach (var row in cZMST_DIDataTable)
				{
					List<XElement> VydejkaItem = new List<XElement>();

					VydejkaItem.Add(new XElement(vyd + "quantity", ((float)row.QTYSHPPD).ToString(nfi)));
					VydejkaItem.Add(new XElement(vyd + "unit", row.MJ.Trim()));
					VydejkaItem.Add(new XElement(vyd + "coefficient", 1));
					VydejkaItem.Add(new XElement(vyd + "discountPercentage", 0));

					if (row.SERLTNUM.Trim().Length > 0)
						VydejkaItem.Add(new XElement(vyd + "stockItem", new XElement(typ + "stockItem", new XElement(typ + "id", row.ITEMNMBR.Trim())), new XElement(typ + "serialNumber", row.SERLTNUM.Trim())));
					else
						VydejkaItem.Add(new XElement(vyd + "stockItem", new XElement(typ + "stockItem", new XElement(typ + "id", row.ITEMNMBR.Trim()))));


					if (!row.IsEXPIRACENull())
					{
						VydejkaItem.Add(new XElement(vyd + "expirationDate", XmlConvert.ToString(row.EXPIRACE, "yyyy-MM-dd")));
					}

					#region AttributeToSN

					var ele = Vypln_AttributeToSN(vyd, typ, row);

					if (ele != null)
						VydejkaItem.Add(ele);

					#endregion

					XElement polozka = new XElement(vyd + "vydejkaItem", VydejkaItem);
					listItem.Add(polozka);
				}

				#endregion

				#region Summary

				List<XElement> Summary = new List<XElement>();


				#endregion

				XElement VydjkaElement = null;

				if (typDoklad.Prodej_Vydejka_Tisk_ID_sablona.HasValue && !string.IsNullOrEmpty(typDoklad.Prodej_Vydejka_Tisk_Tiskarna))
				{

					List<XElement> print = new List<XElement>();
					List<XElement> printSettings = new List<XElement>();

					//Jedná se o ID šablony, pokud neni vyplneno tak je to chyba
					printSettings.Add(new XElement(prn + "report", new XElement(prn + "id", typDoklad.Prodej_Vydejka_Tisk_ID_sablona.Value.ToString())));

					//pocet vytisku projistotu je napevno jeden...
					printSettings.Add(new XElement(prn + "parameters", new XElement(prn + "copy", 1)));

					//dotahuje se vždy, pokud je vyplnena tak se vytiskne na nu, pokud je to praydne tak se tiskne na defaultní
					printSettings.Add(new XElement(prn + "printer", typDoklad.Prodej_Vydejka_Tisk_Tiskarna.Trim()));

					print.Add(new XElement(prn + "printerSettings", printSettings));


					VydjkaElement = new XElement(vyd + "vydejka",
							   new XAttribute("version", "2.0"),
							   new XElement(vyd + "vydejkaHeader", listHeader),
							   new XElement(vyd + "vydejkaDetail", listItem),
							   new XElement(vyd + "vydejkaSummary", Summary),
							   new XElement(vyd + "print", print)
								   );
				}
				else
				{
					VydjkaElement = new XElement(vyd + "vydejka",
							   new XAttribute("version", "2.0"),
							   new XElement(vyd + "vydejkaHeader", listHeader),
							   new XElement(vyd + "vydejkaDetail", listItem),
							   new XElement(vyd + "vydejkaSummary", Summary)
								   );
				}

				XElement root = new XElement(dat + "dataPack",
											new XAttribute(XNamespace.Xmlns + "dat", "http://www.stormware.cz/schema/version_2/data.xsd"),
											new XAttribute(XNamespace.Xmlns + "vyd", "http://www.stormware.cz/schema/version_2/vydejka.xsd"),
											new XAttribute(XNamespace.Xmlns + "typ", "http://www.stormware.cz/schema/version_2/type.xsd"),
											new XAttribute("id", "VYD" + objednavka.ToString()),
											new XAttribute("ico", Globals_V1.Konfigurace.PohodaInfo[0].ICO),
											new XAttribute("application", Fask.SQL.Constants.Common.application_S),
											new XAttribute("version", "2.0"),
											new XAttribute("note", "Import vydejky"),
											new XElement(dat + "dataPackItem",
												new XAttribute("id", "VYD" + objednavka.ToString()),
												new XAttribute("version", "2.0"),
											VydjkaElement));

				root.Save(filename);
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML.Classes.Prodej2", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				return false;
			}

			return true;
		}


		/// <summary>
		/// \TODO Predelat na lepsi
		/// </summary>
		/// <param name="filename"></param>
		/// <param name="uzivatel"></param>
		/// <returns></returns>
		internal static string LoadResponse_Vyd_XML(string filename, Fask.Server.Interfaces.Classes.User uzivatel)
		{

			XML.MST_Pohoda.CheckExistResponseFile(filename);

			// Zadna reakce...
			return "OK";
		}

		/// <summary>
		/// \TODO Predelat na lepsi
		/// </summary>
		/// <param name="filename"></param>
		/// <param name="uzivatel"></param>
		/// <returns></returns>
		internal static string LoadResponse_Vyd_Vratka_XML(string filename, Fask.Server.Interfaces.Classes.User uzivatel)
		{

			XML.MST_Pohoda.CheckExistResponseFile(filename);

			// Zadna reakce...
			return "OK";
		}


		#endregion

		#region Převodka

		/// <summary>
		/// Metoda pro Vytvořeni requestu Prevodky z volneho pohybu
		/// </summary>
		/// <param name="filename">Nazev souboru</param>
		/// <param name="p">poznamka</param>
		/// <param name="cZMST_DIDataTable"> Data z CZMST_DI</param>
		/// <returns>Zda je nalezeno nebo ne</returns>
		internal static bool CreateRequest_Pre_XML(string filename, string p, Fask.DataSets.ProdejData.CZMST_DIDataTable cZMST_DIDataTable, Doklad typDoklad)
		{

			Datasets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter ParamTA = null;
			if (!Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1)
			{
				ParamTA = new Datasets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter();
				ParamTA.Connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
			}

			string SKL_ID = cZMST_DIDataTable[0].SKL_ID_DEST;
			int objednavka = cZMST_DIDataTable[0].CountEntries;

			System.Globalization.NumberFormatInfo nfi = System.Globalization.NumberFormatInfo.InvariantInfo;

			XNamespace dat = "http://www.stormware.cz/schema/version_2/data.xsd";
			XNamespace pre = "http://www.stormware.cz/schema/version_2/prevodka.xsd";
			XNamespace typ = "http://www.stormware.cz/schema/version_2/type.xsd";
			XNamespace prn = "http://www.stormware.cz/schema/version_2/print.xsd";

			try
			{
				#region Hlavicka

				List<XElement> listHeader = new List<XElement>();

				listHeader.Add(new XElement(pre + "text", "MES-MST Vytvořeno z volného pohybu"));

				listHeader.Add(new XElement(pre + "note", objednavka));

				listHeader.Add(new XElement(pre + "date", XmlConvert.ToString(DateTime.Now, "yyyy-MM-dd")));
				listHeader.Add(new XElement(pre + "time", XmlConvert.ToString(DateTime.Now, "HH:mm:ss")));
				listHeader.Add(new XElement(pre + "store", new XElement(typ + "id", SKL_ID.Trim())));


				if ((typDoklad != null) && (typDoklad.Vyber_Typ_Prevodka != null))
				{
					switch (typDoklad.Vyber_Typ_Prevodka)
					{
						case 0:
							{
								// Tato varianta nastaví datum vydejky aj prijemky totožný
								//listHeader.Add(new XElement(pre + "dateOfReceipt", string.Empty));
								//listHeader.Add(new XElement(pre + "timeOfReceipt", string.Empty));
								break;
							}
						case 1:
							{
								//Tato varianta
								listHeader.Add(new XElement(pre + "dateOfReceipt", string.Empty));
								listHeader.Add(new XElement(pre + "timeOfReceipt", string.Empty));
								break;
							}
						case 2:
							{
								// Tato varianta nastaví konkretný čas a datum prijemky
								// odkud vzit?? 
								listHeader.Add(new XElement(pre + "dateOfReceipt", XmlConvert.ToString(DateTime.Now, "yyyy-MM-dd")));
								listHeader.Add(new XElement(pre + "timeOfReceipt", XmlConvert.ToString(DateTime.Now, "HH:mm:ss")));
								break;
							}
						default:
							break;
					}

				}

				if ((typDoklad != null) && !string.IsNullOrEmpty(typDoklad.Stredisko0))
				{
					listHeader.Add(new XElement(pre + "centreSource", new XElement(typ + "id", typDoklad.Stredisko0.Trim())));
				}


				Fask.Interfaces.DataSets.Strediska.CZMST091Row RowStredisko = Database.Prodej2.GETDATA_CZMST091(SKL_ID);

				if (RowStredisko != null)
				{

					if (!string.IsNullOrEmpty(RowStredisko.odb_id))
					{
						XElement IDPartnerFakturacni;

						IDPartnerFakturacni = new XElement(typ + "id", RowStredisko.odb_id.Trim());

						listHeader.Add(new XElement(pre + "partnerIdentity", IDPartnerFakturacni));
					}

					//centreSource // Zdrojove stredisko
					//centreDestination // cilove stredisko

					if (!string.IsNullOrEmpty(RowStredisko.str_id))
					{
						listHeader.Add(new XElement(pre + "centreDestination", new XElement(typ + "id", RowStredisko.str_id.Trim())));
					}
				}


				#endregion

				#region Itemy

				List<XElement> listItem = new List<XElement>();

				foreach (var row in cZMST_DIDataTable)
                {
                    List<XElement> PrevodkaItem = new List<XElement>();

                    PrevodkaItem.Add(new XElement(pre + "quantity", ((float)row.QTYSHPPD).ToString(nfi)));

                    #region Vkladani Sarze + ITEMNMBR

                    XElement sern = new XElement(typ + "serialNumber", row.SERLTNUM.Trim());
                    XElement itemnmbr = new XElement(typ + "stockItem", new XElement(typ + "id", row.ITEMNMBR.Trim()));

                    Datasets.DatabasePohoda.SKzDataTable dtSKz = null;

                    try
                    {
                        dtSKz = Database.Pohoda.SKz_GetDataByID(int.Parse(row.ITEMNMBR));
                    }
                    catch
                    {
                    }

                    if (dtSKz != null && dtSKz.Count > 0)
                    {

                        #region MyRegion

                        var skz_row = dtSKz.First();

                        if (Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1)
                        {
                            Logika_Prodej_Sledovani_ImportPOHODA(
                                pre,
                                row,
                                PrevodkaItem,
                                sern,
                                itemnmbr,
                                skz_row.IsRelSKzVCNull() ? (int?)null : (int?)skz_row.RelSKzVC,
                                !skz_row.IsVPrFXTSNull() && skz_row.VPrFXTS,
                                skz_row.IsRefVPrFXTSNull() ? (int?)null : (int?)skz_row.RefVPrFXTS - 1,
                                !skz_row.IsVPrFPTSNull() && skz_row.VPrFPTS,
                                skz_row.IsRefVPrFPTSNull() ? (int?)null : (int?)skz_row.RefVPrFPTS - 1);
                        }
                        else
                        {
                            Datasets.Zbozi.FASK_ZASOBY_PARAMETRYRow dt_row_param = null;
                            var dt_param = ParamTA.GetDataByITEMNMBR(row.ITEMNMBR.Trim());

                            if ((dt_param != null) && (dt_param.Count > 0))
                            {
                                dt_row_param = dt_param.First();

                                Logika_Prodej_Sledovani_ImportPOHODA(
                                        pre,
                                        row,
                                        PrevodkaItem,
                                        sern,
                                        itemnmbr,
                                        null,
                                        !dt_row_param.IsVPrFXTSNull() && dt_row_param.VPrFXTS,
                                        dt_row_param.IsRefVPrFXTSNull() ? (int?)null : (int?)dt_row_param.RefVPrFXTS - 1,
                                        !dt_row_param.IsVPrFPTSNull() && dt_row_param.VPrFPTS,
                                        dt_row_param.IsRefVPrFPTSNull() ? (int?)null : (int?)dt_row_param.RefVPrFPTS - 1);
                            }
                            else
                            {
                                Prijem_SetParams_serltnum_itemnmbr(pre, row, PrevodkaItem, sern, itemnmbr);
                            }
                        }


                        #endregion

                    }
                    else
                    {
                        Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Import Prodej. ale tohle by nemnelo nastat. Karta nenalezena:" + row.ITEMNMBR);
                        Prijem_SetParams_serltnum_itemnmbr(pre, row, PrevodkaItem, sern, itemnmbr);
                    }

                    #endregion

                    #region AttributeToSN

                    var ele = Vypln_AttributeToSN(pre, typ, row);

                    if (ele != null)
                        PrevodkaItem.Add(ele); 

                    #endregion

                    XElement polozka = new XElement(pre + "prevodkaItem", PrevodkaItem);
                    listItem.Add(polozka);
                }

                #endregion


                XElement PrevodkaElement = null;

				if (typDoklad.Prodej_Prevodka_Tisk_ID_sablona.HasValue && !string.IsNullOrEmpty(typDoklad.Prodej_Prevodka_Tisk_Tiskarna))
				{


					List<XElement> print = new List<XElement>();
					List<XElement> printSettings = new List<XElement>();

					//Jedná se o ID šablony, pokud neni vyplneno tak je to chyba
					printSettings.Add(new XElement(prn + "report", new XElement(prn + "id", typDoklad.Prodej_Prevodka_Tisk_ID_sablona.Value.ToString())));

					//pocet vytisku projistotu je napevno jeden...
					printSettings.Add(new XElement(prn + "parameters", new XElement(prn + "copy", 1)));

					//dotahuje se vždy, pokud je vyplnena tak se vytiskne na nu, pokud je to praydne tak se tiskne na defaultní
					printSettings.Add(new XElement(prn + "printer", typDoklad.Prodej_Prevodka_Tisk_Tiskarna.Trim()));

					print.Add(new XElement(prn + "printerSettings", printSettings));


					PrevodkaElement = new XElement(pre + "prevodka",
							new XAttribute("version", "2.0"),
							new XElement(pre + "prevodkaHeader", listHeader),
							new XElement(pre + "prevodkaDetail", listItem),
							new XElement(pre + "print", print)
							);
				}
				else
				{
					PrevodkaElement = new XElement(pre + "prevodka",
							new XAttribute("version", "2.0"),
							new XElement(pre + "prevodkaHeader", listHeader),
							new XElement(pre + "prevodkaDetail", listItem)
							);
				}

				XElement root = new XElement(dat + "dataPack",
						new XAttribute(XNamespace.Xmlns + "dat", "http://www.stormware.cz/schema/version_2/data.xsd"),
						new XAttribute(XNamespace.Xmlns + "pre", "http://www.stormware.cz/schema/version_2/prevodka.xsd"),
						new XAttribute(XNamespace.Xmlns + "typ", "http://www.stormware.cz/schema/version_2/type.xsd"),
						new XAttribute(XNamespace.Xmlns + "prn", "http://www.stormware.cz/schema/version_2/print.xsd"),
						new XAttribute("id", "PRE" + objednavka.ToString()),
						new XAttribute("ico", Globals_V1.Konfigurace.PohodaInfo[0].ICO),
						new XAttribute("application", Fask.SQL.Constants.Common.application_S),
						new XAttribute("version", "2.0"),
						new XAttribute("note", "Import prevodky"),
					new XElement(dat + "dataPackItem",
						new XAttribute("id", "PRE" + objednavka.ToString()),
						new XAttribute("version", "2.0"),
					PrevodkaElement
					));


				root.Save(filename);
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML.Classes.Prodej2", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				return false;
			}

			return true;

		}


        /// <summary>
        /// \TODO Predelat na lepsi reakci
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="uzivatel"></param>
        /// <returns></returns>
        internal static string LoadResponse_Pre_XML(string filename, Fask.Server.Interfaces.Classes.User uzivatel)
		{
			XML.MST_Pohoda.CheckExistResponseFile(filename);

			XML.Classes.Response2 response = new XML.Classes.Response2(filename);

			if (response != null)
			{
				if (response.Status == "OK")
				{
					var doc = response.DocumentNumber;

					if (!string.IsNullOrEmpty(doc))
					{
						Classes.Pohoda.Update_AttributeToSN_Prevod(doc);
					}
					else
					{
						throw new Exception("Dokument nenalezen");
					}
				}
				else
				{
					return response.Status;
				}

			}
			else
			{
				throw new Exception("Response nerozparsovan");
			}

			return "OK";
		}


		#endregion

		#region Faktura Vydana

		/// <summary>
		/// Metoda pro Vytvořeni requestu Prevodky z volneho pohybu
		/// </summary>
		/// <param name="filename">Nazev souboru</param>
		/// <param name="p">poznamka</param>
		/// <param name="cZMST_DIDataTable"> Data z CZMST_DI</param>
		/// <returns>Zda je nalezeno nebo ne</returns>
		internal static bool CreateRequest_FV_XML(string filename, string p, Fask.DataSets.ProdejData.CZMST_DIDataTable cZMST_DIDataTable, Doklad typDoklad)
		{

			Datasets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter ParamTA = null;
			if (!Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1)
			{
				ParamTA = new Datasets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter();
				ParamTA.Connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
			}

			string SKL_ID = cZMST_DIDataTable[0].SKL_ID_DEST;
			int objednavka = cZMST_DIDataTable[0].CountEntries;

			System.Globalization.NumberFormatInfo nfi = System.Globalization.NumberFormatInfo.InvariantInfo;

			XNamespace dat = "http://www.stormware.cz/schema/version_2/data.xsd";
			XNamespace inv = "http://www.stormware.cz/schema/version_2/invoice.xsd";
			XNamespace typ = "http://www.stormware.cz/schema/version_2/type.xsd";
			XNamespace prn = "http://www.stormware.cz/schema/version_2/print.xsd";

			try
			{
				#region Hlavicka

				List<XElement> listHeader = new List<XElement>();

				listHeader.Add(new XElement(inv + "invoiceType", "issuedInvoice"));

				listHeader.Add(new XElement(inv + "text", "MES-MST Vytvořeno z volného pohybu"));

				listHeader.Add(new XElement(inv + "note", objednavka));

				listHeader.Add(new XElement(inv + "date", XmlConvert.ToString(DateTime.Now, "yyyy-MM-dd")));

	
				#endregion

				#region Itemy

				List<XElement> listItem = new List<XElement>();

				foreach (var row in cZMST_DIDataTable)
				{
					List<XElement> FVItem = new List<XElement>();

					FVItem.Add(new XElement(inv + "quantity", ((float)row.QTYSHPPD).ToString(nfi)));

					#region Vkladani Sarze + ITEMNMBR

					XElement sern = new XElement(typ + "serialNumber", row.SERLTNUM.Trim());
					XElement itemnmbr = new XElement(typ + "stockItem", new XElement(typ + "id", row.ITEMNMBR.Trim()));

					Datasets.DatabasePohoda.SKzDataTable dtSKz = null;

					try
					{
						dtSKz = Database.Pohoda.SKz_GetDataByID(int.Parse(row.ITEMNMBR));
					}
					catch
					{
					}

					if (dtSKz != null && dtSKz.Count > 0)
					{

						#region MyRegion

						var skz_row = dtSKz.First();

						if (Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1)
						{
							Logika_Prodej_Sledovani_ImportPOHODA(
								inv,
								row,
								FVItem,
								sern,
								itemnmbr,
								skz_row.IsRelSKzVCNull() ? (int?)null : (int?)skz_row.RelSKzVC,
								!skz_row.IsVPrFXTSNull() && skz_row.VPrFXTS,
								skz_row.IsRefVPrFXTSNull() ? (int?)null : (int?)skz_row.RefVPrFXTS - 1,
								!skz_row.IsVPrFPTSNull() && skz_row.VPrFPTS,
								skz_row.IsRefVPrFPTSNull() ? (int?)null : (int?)skz_row.RefVPrFPTS - 1);
						}
						else
						{
							Datasets.Zbozi.FASK_ZASOBY_PARAMETRYRow dt_row_param = null;
							var dt_param = ParamTA.GetDataByITEMNMBR(row.ITEMNMBR.Trim());

							if ((dt_param != null) && (dt_param.Count > 0))
							{
								dt_row_param = dt_param.First();

								Logika_Prodej_Sledovani_ImportPOHODA(
										inv,
										row,
										FVItem,
										sern,
										itemnmbr,
										null,
										!dt_row_param.IsVPrFXTSNull() && dt_row_param.VPrFXTS,
										dt_row_param.IsRefVPrFXTSNull() ? (int?)null : (int?)dt_row_param.RefVPrFXTS - 1,
										!dt_row_param.IsVPrFPTSNull() && dt_row_param.VPrFPTS,
										dt_row_param.IsRefVPrFPTSNull() ? (int?)null : (int?)dt_row_param.RefVPrFPTS - 1);
							}
							else
							{
								Prijem_SetParams_serltnum_itemnmbr(inv, row, FVItem, sern, itemnmbr);
							}
						}


						#endregion

					}
					else
					{
						Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Import Prodej. ale tohle by nemnelo nastat. Karta nenalezena:" + row.ITEMNMBR);
						Prijem_SetParams_serltnum_itemnmbr(inv, row, FVItem, sern, itemnmbr);
					}

					#endregion

					if (!row.IsEXPIRACENull())
					{
						FVItem.Add(new XElement(inv + "expirationDate", XmlConvert.ToString(row.EXPIRACE, "yyyy-MM-dd")));
					}

					#region AttributeToSN

					var ele = Vypln_AttributeToSN(inv, typ, row);

					if (ele != null)
						FVItem.Add(ele);

					#endregion

					XElement polozka = new XElement(inv + "invoiceItem", FVItem);
					listItem.Add(polozka);
				}

				#endregion


				XElement PrevodkaElement = null;
				
				PrevodkaElement = new XElement(inv + "invoice",
							new XAttribute("version", "2.0"),
							new XElement(inv + "invoiceHeader", listHeader),
							new XElement(inv + "invoiceDetail", listItem)
							);
				

				XElement root = new XElement(dat + "dataPack",
						new XAttribute(XNamespace.Xmlns + "dat", "http://www.stormware.cz/schema/version_2/data.xsd"),
						new XAttribute(XNamespace.Xmlns + "inv", "http://www.stormware.cz/schema/version_2/invoice.xsd"),
						new XAttribute(XNamespace.Xmlns + "typ", "http://www.stormware.cz/schema/version_2/type.xsd"),
						new XAttribute(XNamespace.Xmlns + "prn", "http://www.stormware.cz/schema/version_2/print.xsd"),
						new XAttribute("id", "FV" + objednavka.ToString()),
						new XAttribute("ico", Globals_V1.Konfigurace.PohodaInfo[0].ICO),
						new XAttribute("application", Fask.SQL.Constants.Common.application_S),
						new XAttribute("version", "2.0"),
						new XAttribute("note", "Import faktura vydana"),
					new XElement(dat + "dataPackItem",
						new XAttribute("id", "FV" + objednavka.ToString()),
						new XAttribute("version", "2.0"),
					PrevodkaElement
					));


				root.Save(filename);
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML.Classes.Prodej2", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				return false;
			}

			return true;

		}

		/// <summary>
		/// \TODO Predelat na lepsi reakci
		/// </summary>
		/// <param name="filename"></param>
		/// <param name="uzivatel"></param>
		/// <returns></returns>
		internal static string LoadResponse_FV_XML(string filename, Fask.Server.Interfaces.Classes.User uzivatel)
		{
			XML.MST_Pohoda.CheckExistResponseFile(filename);

			return "OK";
		}


		#endregion

		#region Objednavka Vydana


		/// <summary>
		/// Metoda pro Vytvořeni requestu Objednavka vydana z volneho pohybu
		/// </summary>
		/// <param name="filename">Nazev souboru</param>
		/// <param name="p">poznamka</param>
		/// <param name="cZMST_DIDataTable"> Data z CZMST_DI</param>
		/// <returns>Zda je nalezeno nebo ne</returns>
		internal static bool CreateRequest_Objv_XML(string filename, string p, Fask.DataSets.ProdejData.CZMST_DIDataTable cZMST_DIDataTable, Doklad typDoklad)
		{
			System.Globalization.NumberFormatInfo nfi = System.Globalization.NumberFormatInfo.InvariantInfo;

			XNamespace dat = "http://www.stormware.cz/schema/version_2/data.xsd";
			XNamespace ord = "http://www.stormware.cz/schema/version_2/order.xsd";
			XNamespace typ = "http://www.stormware.cz/schema/version_2/type.xsd";

			try
			{
				int objednavka = cZMST_DIDataTable[0].CountEntries;

				string partnerID = "1";// string.Empty;
				foreach (Fask.DataSets.ProdejData.CZMST_DIRow row in cZMST_DIDataTable)
				{
					if (!row.IsODB_IDNull() && row.ODB_ID.Trim() != string.Empty)
					{
						partnerID = row.ODB_ID.Trim();
						break;
					}
				}


				#region Hlavicka

				List<XElement> listHeader = new List<XElement>();

				listHeader.Add(new XElement(ord + "orderType", "issuedOrder"));
				listHeader.Add(new XElement(ord + "text", "MES-MST Vytvořeno z volného pohybu"));
				listHeader.Add(new XElement(ord + "note", objednavka));

				listHeader.Add(new XElement(ord + "date", XmlConvert.ToString(DateTime.Now, "yyyy-MM-dd")));

				if (!string.IsNullOrEmpty(partnerID))
				{
					XElement IDPartnerFakturacni;

					IDPartnerFakturacni = new XElement(typ + "id", partnerID.Trim());

					listHeader.Add(new XElement(ord + "partnerIdentity", IDPartnerFakturacni));
				}
				else
				{
					XElement IDPartnerFakturacni;

					IDPartnerFakturacni = new XElement(typ + "id", "1");

					listHeader.Add(new XElement(ord + "partnerIdentity", IDPartnerFakturacni));
				}

				#endregion

				#region Itemy

				List<XElement> listItem = new List<XElement>();

				foreach (var row in cZMST_DIDataTable)
				{
					List<XElement> prodejkaItem = new List<XElement>();

					prodejkaItem.Add(new XElement(ord + "quantity", ((float)row.QTYSHPPD).ToString(nfi)));
					prodejkaItem.Add(new XElement(ord + "unit", row.MJ.Trim()));
					prodejkaItem.Add(new XElement(ord + "coefficient", 1));
					prodejkaItem.Add(new XElement(ord + "discountPercentage", 0));

					if (row.SERLTNUM.Trim().Length > 0)
						prodejkaItem.Add(new XElement(ord + "stockItem", new XElement(typ + "stockItem", new XElement(typ + "id", row.ITEMNMBR.Trim())), new XElement(typ + "serialNumber", row.SERLTNUM.Trim())));
					else
						prodejkaItem.Add(new XElement(ord + "stockItem", new XElement(typ + "stockItem", new XElement(typ + "id", row.ITEMNMBR.Trim()))));

					XElement polozka = new XElement(ord + "orderItem", prodejkaItem);
					listItem.Add(polozka);
				}

				#endregion


				#region Summary

				List<XElement> Summary = new List<XElement>();


				#endregion

				XElement root = new XElement(dat + "dataPack",
					new XAttribute(XNamespace.Xmlns + "dat", "http://www.stormware.cz/schema/version_2/data.xsd"),
					new XAttribute(XNamespace.Xmlns + "ord", "http://www.stormware.cz/schema/version_2/order.xsd"),
					new XAttribute(XNamespace.Xmlns + "typ", "http://www.stormware.cz/schema/version_2/type.xsd"),
					new XAttribute("id", "OBJV" + objednavka.ToString()),
					new XAttribute("ico", Globals_V1.Konfigurace.PohodaInfo[0].ICO),
					new XAttribute("application", Fask.SQL.Constants.Common.application_S),
					new XAttribute("version", "2.0"),
					new XAttribute("note", "Import objv"),

						new XElement(dat + "dataPackItem",
							new XAttribute("id", "OBJV" + objednavka.ToString()),
							new XAttribute("version", "2.0"),

								new XElement(ord + "order",
									new XAttribute("version", "2.0"),
									new XElement(ord + "orderHeader", listHeader),
									new XElement(ord + "orderDetail", listItem),
									new XElement(ord + "orderSummary", Summary)
										)));

				root.Save(filename);
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML.Classes.Prodej2", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				return false;
			}

			return true;


		}

		/// <summary>
		/// \TODO Predelat na lepsi reakci
		/// </summary>
		/// <param name="filename"></param>
		/// <param name="uzivatel"></param>
		/// <returns></returns>
		internal static string LoadResponse_Objv_XML(string filename, Fask.Server.Interfaces.Classes.User uzivatel)
		{
			XML.MST_Pohoda.CheckExistResponseFile(filename);

			XML.MST_Pohoda.UpdateCreatorObj(uzivatel, filename);
			return "OK";
		}

		#endregion

		/***********  Nepouživaji se u zakaznika *********/

		#region Objektovo skladane

		/// <summary>
		/// Metoda pro Vytvořeni requestu Objednavka vydana CM z volneho pohybu
		/// </summary>
		/// <param name="filename">Nazev souboru</param>
		/// <param name="p">poznamka</param>
		/// <param name="cZMST_DIDataTable"> Data z CZMST_DI</param>
		/// <returns>Zda je nalezeno nebo ne</returns>
		internal static bool CreateRequest_Objvcm_XML(string filename, string p, Fask.DataSets.ProdejData.CZMST_DIDataTable cZMST_DIDataTable, Doklad typDoklad)
		{


			System.Globalization.NumberFormatInfo nfi = System.Globalization.NumberFormatInfo.InvariantInfo;

			XNamespace dat = "http://www.stormware.cz/schema/version_2/data.xsd";
			XNamespace ord = "http://www.stormware.cz/schema/version_2/order.xsd";
			XNamespace typ = "http://www.stormware.cz/schema/version_2/type.xsd";

			try
			{
				Fask.DataSets.ProdejData.CZMST_DIRow Row_DI;


				var data = cZMST_DIDataTable.OrderBy(x => x.DEX_ROW_ID);
				Row_DI = data.First();

				string partnerID = string.Empty;
				foreach (Fask.DataSets.ProdejData.CZMST_DIRow row in cZMST_DIDataTable)
				{
					if (!row.IsODB_IDNull() && row.ODB_ID.Trim() != string.Empty)
					{
						//Log.writeErrorLog("ODB_ID: " + row.ODB_ID.ToString());
						partnerID = row.ODB_ID.Trim();
						break;
					}
				}


				#region Hlavicka

				List<XElement> listHeader = new List<XElement>();


				listHeader.Add(new XElement(ord + "orderType", "issuedOrder"));

				listHeader.Add(new XElement(ord + "paymentType", new XElement(typ + "ids", Globals_V1.Konfigurace.Prodej[0].KodFormaUhrady)));

				listHeader.Add(new XElement(ord + "text", "MES-MST Vytvořeno z volného pohybu"));

				if (!string.IsNullOrEmpty(p))
				{
					listHeader.Add(new XElement(ord + "note", p));
				}

				listHeader.Add(new XElement(ord + "date", XmlConvert.ToString(DateTime.Now, "yyyy-MM-dd")));

				if (!string.IsNullOrEmpty(partnerID))
				{
					XElement IDPartnerFakturacni;

					IDPartnerFakturacni = new XElement(typ + "id", partnerID.Trim());

					listHeader.Add(new XElement(ord + "partnerIdentity", IDPartnerFakturacni));
				}

				#endregion

				#region Itemy

				List<XElement> listItem = new List<XElement>();

				foreach (var row in cZMST_DIDataTable)
				{
					List<XElement> prodejkaItem = new List<XElement>();

					prodejkaItem.Add(new XElement(ord + "quantity", ((float)row.QTYSHPPD).ToString(nfi)));
					prodejkaItem.Add(new XElement(ord + "unit", row.MJ.Trim()));
					prodejkaItem.Add(new XElement(ord + "coefficient", 1));
					prodejkaItem.Add(new XElement(ord + "discountPercentage", 0));

					if (row.SERLTNUM.Trim().Length > 0)
						prodejkaItem.Add(new XElement(ord + "stockItem", new XElement(typ + "stockItem", new XElement(typ + "id", row.ITEMNMBR.Trim())), new XElement(typ + "serialNumber", row.SERLTNUM.Trim())));
					else
						prodejkaItem.Add(new XElement(ord + "stockItem", new XElement(typ + "stockItem", new XElement(typ + "id", row.ITEMNMBR.Trim()))));

					XElement polozka = new XElement(ord + "orderItem", prodejkaItem);
					listItem.Add(polozka);
				}

				#endregion


				#region Summary

				List<XElement> Summary = new List<XElement>();

				try
				{
					//Datasets.DatabasePohodaTableAdapters.sFormUhTableAdapter ta_sformuh = new Datasets.DatabasePohodaTableAdapters.sFormUhTableAdapter();
					//Datasets.DatabasePohodaTableAdapters.sCMenyTableAdapter ta_cmeny = new Datasets.DatabasePohodaTableAdapters.sCMenyTableAdapter();

					//ta_sformuh.Connection.ConnectionString = Globals.ConnectionStringPohodaDB;
					//ta_cmeny.Connection.ConnectionString = Globals.ConnectionStringPohodaDB;

					//Datasets.DatabasePohoda.sFormUhDataTable dt_sformuh = ta_sformuh.GetDataByIDS(Globals.KodCiziMena);
					int? cizimenaid = null;
					//cizimenaid ta_cmeny.GetDataByID(

					//Datasets.DatabasePohoda.sCMenyDataTable dt_cmeny = ta_cmeny.GetDataByKod(Globals.KodCiziMena);
					Datasets.DatabasePohoda.sCMenyDataTable dt_cmeny = Database.Pohoda.sCMeny_GetDataByKod(Globals_V1.Konfigurace.Prodej[0].KodCiziMena);
					//if (dt_sformuh.Count > 0 && !dt_sformuh[0].IsRefCMNull())
					//    cizimenaid = dt_sformuh[0].RefCM;

					if (dt_cmeny.Count > 0)
						cizimenaid = dt_cmeny[0].ID;

					if (cizimenaid.HasValue)
					{
						Summary.Add(new XElement(ord + "foreignCurrency", new XElement(typ + "currency", new XElement(typ + "id", cizimenaid))));
					}
				}
				catch (Exception ex)
				{
					Fask.Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML", "CreateRequest_Objvcm_XML", ex);
				}


				#endregion



				XElement root = new XElement(dat + "dataPack",
					new XAttribute(XNamespace.Xmlns + "dat", "http://www.stormware.cz/schema/version_2/data.xsd"),
					new XAttribute(XNamespace.Xmlns + "ord", "http://www.stormware.cz/schema/version_2/order.xsd"),
					new XAttribute(XNamespace.Xmlns + "typ", "http://www.stormware.cz/schema/version_2/type.xsd"),
					new XAttribute("id", "OBJVCM" + Row_DI.DEX_ROW_ID.ToString()),
					new XAttribute("ico", Globals_V1.Konfigurace.PohodaInfo[0].ICO),
					new XAttribute("application", Fask.SQL.Constants.Common.application_S),
					new XAttribute("version", "2.0"),
					new XAttribute("note", "Import objvcm"),

						new XElement(dat + "dataPackItem",
							new XAttribute("id", "OBJVCM" + Row_DI.DEX_ROW_ID.ToString()),
							new XAttribute("version", "2.0"),

								new XElement(ord + "order",
									new XAttribute("version", "2.0"),
									new XElement(ord + "orderHeader", listHeader),
									new XElement(ord + "orderDetail", listItem),
									new XElement(ord + "orderSummary", Summary)
										)));

				root.Save(filename);
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML.Classes.Prodej2", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				return false;
			}

			return true;



		}

		/// <summary>
		/// \TODO Predelat na lepsi rekaci
		/// </summary>
		/// <param name="filename"></param>
		/// <param name="uzivatel"></param>
		/// <returns></returns>
		internal static string LoadResponse_Objvcm_XML(string filename, Fask.Server.Interfaces.Classes.User uzivatel)
		{
			XML.MST_Pohoda.CheckExistResponseFile(filename);

			XML.MST_Pohoda.UpdateCreatorObj(uzivatel, filename);
			return "OK";
		}

		/// <summary>
		/// Metoda pro Vytvořeni requestu Prodejky z volneho pohybu
		/// </summary>
		/// <param name="filename">Nazev souboru</param>
		/// <param name="p">poznamka</param>
		/// <param name="cZMST_DIDataTable"> Data z CZMST_DI</param>
		/// <returns>Zda je nalezeno nebo ne</returns>
		internal static bool CreateRequest_Pro_XML(string filename, string p, Fask.DataSets.ProdejData.CZMST_DIDataTable cZMST_DIDataTable, Doklad typDoklad)
		{

			System.Globalization.NumberFormatInfo nfi = System.Globalization.NumberFormatInfo.InvariantInfo;

			XNamespace dat = "http://www.stormware.cz/schema/version_2/data.xsd";
			XNamespace pro = "http://www.stormware.cz/schema/version_2/prodejka.xsd";
			XNamespace typ = "http://www.stormware.cz/schema/version_2/type.xsd";

			try
			{

				#region Grupovani DI

				//if (Properties.Settings.Default.Prodej_GrupujDataPrijemka)
				//{


				//    //List<IGrouping<a, Fask.DataSets.ProdejData.CZMST_DIRow>>
				//    var DIList = cZMST_DIDataTable.GroupBy(x => new 
				//    { 
				//        x.CountEntries,
				//        x.VNDITNUM,
				//        x.CZ_CarKod,
				//        x.ODB_ID,
				//        x.SKL_ID,
				//        x.ITEMNMBR,
				//        x.

				//    }).ToList();

				//}


				#endregion



				Fask.DataSets.ProdejData.CZMST_DIRow Row_DI;


				var data = cZMST_DIDataTable.OrderBy(x => x.DEX_ROW_ID);
				Row_DI = data.First();

				string partnerID = string.Empty;
				foreach (Fask.DataSets.ProdejData.CZMST_DIRow row in cZMST_DIDataTable)
				{
					if (!row.IsODB_IDNull() && row.ODB_ID.Trim() != string.Empty)
					{
						//Log.writeErrorLog("ODB_ID: " + row.ODB_ID.ToString());
						partnerID = row.ODB_ID.Trim();
						break;
					}
				}


				#region Hlavicka

				List<XElement> listHeader = new List<XElement>();

				listHeader.Add(new XElement(pro + "text", "MES-MST Vytvořeno z volného pohybu"));

				if (!string.IsNullOrEmpty(p))
				{
					listHeader.Add(new XElement(pro + "note", p));
				}

				listHeader.Add(new XElement(pro + "date", XmlConvert.ToString(DateTime.Now, "yyyy-MM-dd")));

				if (!string.IsNullOrEmpty(partnerID))
				{
					XElement IDPartnerFakturacni;

					IDPartnerFakturacni = new XElement(typ + "id", partnerID.Trim());

					listHeader.Add(new XElement(pro + "partnerIdentity", IDPartnerFakturacni));
				}

				#endregion

				#region Itemy

				List<XElement> listItem = new List<XElement>();

				foreach (var row in cZMST_DIDataTable)
				{
					List<XElement> prodejkaItem = new List<XElement>();

					prodejkaItem.Add(new XElement(pro + "quantity", ((float)row.QTYSHPPD).ToString(nfi)));
					prodejkaItem.Add(new XElement(pro + "unit", row.MJ.Trim()));
					prodejkaItem.Add(new XElement(pro + "coefficient", 1));
					prodejkaItem.Add(new XElement(pro + "discountPercentage", 0));

					if (row.SERLTNUM.Trim().Length > 0)
						prodejkaItem.Add(new XElement(pro + "stockItem", new XElement(typ + "stockItem", new XElement(typ + "id", row.ITEMNMBR.Trim())), new XElement(typ + "serialNumber", row.SERLTNUM.Trim())));
					else
						prodejkaItem.Add(new XElement(pro + "stockItem", new XElement(typ + "stockItem", new XElement(typ + "id", row.ITEMNMBR.Trim()))));

					XElement polozka = new XElement(pro + "prodejkaItem", prodejkaItem);
					listItem.Add(polozka);
				}

				#endregion


				#region Summary

				List<XElement> Summary = new List<XElement>();


				#endregion



				XElement root = new XElement(dat + "dataPack",
					new XAttribute(XNamespace.Xmlns + "dat", "http://www.stormware.cz/schema/version_2/data.xsd"),
					new XAttribute(XNamespace.Xmlns + "pro", "http://www.stormware.cz/schema/version_2/prodejka.xsd"),
					new XAttribute(XNamespace.Xmlns + "typ", "http://www.stormware.cz/schema/version_2/type.xsd"),
					new XAttribute("id", "PRO" + Row_DI.DEX_ROW_ID.ToString()),
					new XAttribute("ico", Globals_V1.Konfigurace.PohodaInfo[0].ICO),
					new XAttribute("application", Fask.SQL.Constants.Common.application_S),
					new XAttribute("version", "2.0"),
					new XAttribute("note", "Import prodejky"),

						new XElement(dat + "dataPackItem",
							new XAttribute("id", "PRO" + Row_DI.DEX_ROW_ID.ToString()),
							new XAttribute("version", "2.0"),

								new XElement(pro + "prodejka",
									new XAttribute("version", "2.0"),
									new XElement(pro + "prodejkaHeader", listHeader),
									new XElement(pro + "prodejkaDetail", listItem),
									new XElement(pro + "prodejkaSummary", Summary)
										)));

				root.Save(filename);
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML.Classes.Prodej2", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				return false;
			}

			return true;


		}

		/// <summary>
		/// \TODO Predelat na lepsi reakci
		/// </summary>
		/// <param name="filename"></param>
		/// <param name="uzivatel"></param>
		/// <returns></returns>
		internal static string LoadResponse_Pro_XML(string filename, Fask.Server.Interfaces.Classes.User uzivatel)
		{
			XML.MST_Pohoda.CheckExistResponseFile(filename);

			return "OK";
		}



		
		#endregion

		#region předelat

		#region Expedice 29.11.2019 předelane na objektorve, potřeba otestovat, zda to k nečemu je....

		/// <summary>
		/// VELKE TODO Predelat do Objektoveho
		/// </summary>
		/// <param name="filename"></param>
		/// <param name="p"></param>
		/// <param name="cZMST_DIDataTable"></param>
		/// <returns></returns>
		internal static bool CreateRequest_Expedice_XML(string filename, string note, Fask.DataSets.ProdejData.CZMST_DIDataTable cZMST_DIDataTable, Doklad typDoklad)
		{

			#region OLD

			//string partnerID = string.Empty;
			//foreach (Fask.DataSets.ProdejData.CZMST_DIRow row in cZMST_DIDataTable)
			//{
			//    if (!row.IsODB_IDNull() && row.ODB_ID.Trim() != string.Empty)
			//    {
			//        //Log.writeErrorLog("ODB_ID: " + row.ODB_ID.ToString());
			//        partnerID = row.ODB_ID.Trim();
			//        break;
			//    }
			//}

			//TextWriter tw = new StreamWriter(filename);
			////XML.MST_Pohoda.WriteExpediceXML(p, cZMST_DIDataTable, partnerID, tw);

			//int davkacislo = cZMST_DIDataTable[0].CountEntries;

			////vytahnout zakazka a cinnost
			//// \TODO : dotahnout informace z vydane objednavky ...
			//string objednavka = cZMST_DIDataTable[0].STR_ID.Trim();

			////Datasets.DatabasePohodaTableAdapters.OBJTableAdapter pohoda_obj_ta = new Datasets.DatabasePohodaTableAdapters.OBJTableAdapter();
			////pohoda_obj_ta.Connection.ConnectionString = Globals.ConnectionStringPohodaDB;

			////Datasets.DatabasePohoda.OBJDataTable pohoda_obj_dt = pohoda_obj_ta.GetDataByCislo(objednavka);
			//Datasets.DatabasePohoda.OBJDataTable pohoda_obj_dt = Database.Pohoda.OBJ_GetDataByCislo(objednavka);

			//Datasets.DatabasePohoda.OBJRow pohoda_obj = null;
			//if (pohoda_obj_dt.Count > 0)
			//    pohoda_obj = pohoda_obj_dt[0];

			//string cislozak = null;
			//if (pohoda_obj != null && !pohoda_obj.IsCisloZAKNull())
			//    cislozak = pohoda_obj.CisloZAK;

			//int? refad = null;
			//if (pohoda_obj != null && !pohoda_obj.IsRefADNull())
			//    refad = pohoda_obj.RefAD;

			//// \TODO : cislo projektu = cinnost
			////pohoda_obj.
			//int? cinnost = null;
			//if (pohoda_obj != null && !pohoda_obj.IsRefCinNull())
			//    cinnost = pohoda_obj.RefCin;


			//tw.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
			//tw.WriteLine("<dat:dataPack id=\"exp" + davkacislo + "\" ico=\"" + Globals.ICO + "\" application=\"MST\" version = \"2.0\" note=\"Import Vydejky\"");
			//tw.WriteLine("xmlns:dat=\"http://www.stormware.cz/schema/version_2/data.xsd\"");
			//tw.WriteLine("xmlns:vyd=\"http://www.stormware.cz/schema/version_2/vydejka.xsd\"");
			//tw.WriteLine("xmlns:typ=\"http://www.stormware.cz/schema/version_2/type.xsd\">");

			//tw.WriteLine("<dat:dataPackItem id=\"EXP" + davkacislo + "\" version=\"2.0\">");
			//tw.WriteLine("  <vyd:vydejka version=\"2.0\">");

			//// hlavicka
			//tw.WriteLine("      <vyd:vydejkaHeader>");
			//tw.WriteLine("          <vyd:date>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString("00") + "-" + DateTime.Now.Day.ToString("00") + "</vyd:date>");
			//tw.WriteLine("          <vyd:text>" + p /*"Vydejka-volny vydej. Porizeno mobilnim terminalem"*/ + "</vyd:text>");

			////partner
			//if (partnerID != string.Empty /*&& dodavatel != null*/)
			//{
			//    tw.WriteLine("          <vyd:partnerIdentity>");
			//    tw.WriteLine("              <typ:id>" + partnerID + "</typ:id>");
			//    tw.WriteLine("          </vyd:partnerIdentity>");
			//}

			//if (!string.IsNullOrEmpty(objednavka))
			//{
			//    tw.WriteLine("          <vyd:numberOrder>" + objednavka + "</vyd:numberOrder>");
			//}

			////zakazka
			//if (cislozak != null)
			//{
			//    tw.WriteLine("          <vyd:contract>");
			//    tw.WriteLine("              <typ:ids>" + cislozak + "</typ:ids>");
			//    tw.WriteLine("          </vyd:contract>");
			//}
			////cinnost
			//if (cinnost.HasValue)
			//{
			//    tw.WriteLine("          <vyd:activity>");
			//    tw.WriteLine("              <typ:id>" + cinnost.Value + "</typ:id>");
			//    tw.WriteLine("          </vyd:activity>");
			//}

			////tw.WriteLine("          <vyd:note>" + "nacteno z xml (czmst_si)" + "</vyd:note>");
			//tw.WriteLine("          <vyd:intNote>" + "nacteno z mobilniho terminalu" + "</vyd:intNote>");
			//tw.WriteLine("      </vyd:vydejkaHeader>");

			////polozky 
			//tw.WriteLine("      <vyd:vydejkaDetail>");

			//foreach (Fask.DataSets.ProdejData.CZMST_DIRow row in cZMST_DIDataTable)
			//{
			//    tw.WriteLine("      <vyd:vydejkaItem>");
			//    tw.WriteLine("          <vyd:quantity>" + ((float)row.QTYSHPPD).ToString(System.Globalization.NumberFormatInfo.InvariantInfo) + "</vyd:quantity>");
			//    tw.WriteLine("          <vyd:unit>" + row.MJ.Trim() + "</vyd:unit>");
			//    tw.WriteLine("          <vyd:coefficient>1</vyd:coefficient>");
			//    tw.WriteLine("          <vyd:discountPercentage>0</vyd:discountPercentage>");
			//    tw.WriteLine("          <vyd:stockItem>");
			//    tw.WriteLine("              <typ:stockItem>");
			//    tw.WriteLine("                  <typ:id>" + row.ITEMNMBR.Trim() + "</typ:id>");
			//    tw.WriteLine("              </typ:stockItem>");

			//    if (row.SERLTNUM.Trim().Length > 0)
			//        tw.WriteLine("              <typ:serialNumber>" + row.SERLTNUM.Trim() + "</typ:serialNumber>");

			//    tw.WriteLine("          </vyd:stockItem>");
			//    tw.WriteLine("      </vyd:vydejkaItem>");
			//}

			//tw.WriteLine("      </vyd:vydejkaDetail>");
			//tw.WriteLine("  </vyd:vydejka>");
			//tw.WriteLine("</dat:dataPackItem>");
			//tw.WriteLine("</dat:dataPack>");

			//tw.Flush();
			//tw.Close();
			//return true;


			
			#endregion

			#region NEW

			System.Globalization.NumberFormatInfo nfi = System.Globalization.NumberFormatInfo.InvariantInfo;

			XNamespace dat = "http://www.stormware.cz/schema/version_2/data.xsd";
			XNamespace vyd = "http://www.stormware.cz/schema/version_2/vydejka.xsd";
			XNamespace typ = "http://www.stormware.cz/schema/version_2/type.xsd";

			try
			{

				#region Dotaženi dat

				string partnerID = string.Empty;
				foreach (Fask.DataSets.ProdejData.CZMST_DIRow row in cZMST_DIDataTable)
				{
					if (!row.IsODB_IDNull() && row.ODB_ID.Trim() != string.Empty)
					{
						partnerID = row.ODB_ID.Trim();
						break;
					}
				}

				int davkacislo = cZMST_DIDataTable[0].CountEntries;

				string objednavka = cZMST_DIDataTable[0].STR_ID.Trim();
				Datasets.DatabasePohoda.OBJDataTable pohoda_obj_dt = Database.Pohoda.OBJ_GetDataByCislo(objednavka);
				
				Datasets.DatabasePohoda.OBJRow pohoda_obj = null;
				if (pohoda_obj_dt.Count > 0)
					pohoda_obj = pohoda_obj_dt[0];

				string cislozak = null;
				if (pohoda_obj != null && !pohoda_obj.IsCisloZAKNull())
					cislozak = pohoda_obj.CisloZAK;

				int? refad = null;
				if (pohoda_obj != null && !pohoda_obj.IsRefADNull())
					refad = pohoda_obj.RefAD;

				int? cinnost = null;
				if (pohoda_obj != null && !pohoda_obj.IsRefCinNull())
					cinnost = pohoda_obj.RefCin;

				#endregion


				#region Header

				List<XElement> listHeader = new List<XElement>();

				listHeader.Add(new XElement(vyd + "date", XmlConvert.ToString(DateTime.Now, "yyyy-MM-dd")));

				listHeader.Add(new XElement(vyd + "text", note));

				listHeader.Add(new XElement(vyd + "intNote", objednavka));

	
				if (!string.IsNullOrEmpty(partnerID))
				{
					XElement IDPartnerFakturacni;

					IDPartnerFakturacni = new XElement(typ + "id", partnerID.Trim());

					listHeader.Add(new XElement(vyd + "partnerIdentity", IDPartnerFakturacni));
				}

				if (!string.IsNullOrEmpty(objednavka))
				{
					listHeader.Add(new XElement(vyd + "numberOrder", objednavka));
				}


				if (!string.IsNullOrEmpty(cislozak))
				{
					listHeader.Add(new XElement(vyd + "contract", new XElement(typ + "id", cislozak.Trim())));
				}

				if (cinnost.HasValue)
				{
					listHeader.Add(new XElement(vyd + "activity", new XElement(typ + "id", cinnost.Value.ToString())));
				}
				
				#endregion

				#region Items

				List<XElement> listItem = new List<XElement>();

				foreach (Fask.DataSets.ProdejData.CZMST_DIRow row in cZMST_DIDataTable)
				{
					List<XElement> vydejkaItem = new List<XElement>();

					vydejkaItem.Add(new XElement(vyd + "quantity", ((float)row.QTYSHPPD).ToString(nfi)));
					vydejkaItem.Add(new XElement(vyd + "unit", row.MJ.Trim()));
					vydejkaItem.Add(new XElement(vyd + "coefficient", 1));
					vydejkaItem.Add(new XElement(vyd + "discountPercentage", 0));


					if (row.SERLTNUM.Trim().Length > 0)
						vydejkaItem.Add(new XElement(vyd + "stockItem", new XElement(typ + "stockItem", new XElement(typ + "id", row.ITEMNMBR.Trim())), new XElement(typ + "serialNumber", row.SERLTNUM.Trim())));
					else
						vydejkaItem.Add(new XElement(vyd + "stockItem", new XElement(typ + "stockItem", new XElement(typ + "id", row.ITEMNMBR.Trim()))));

					XElement polozka = new XElement(vyd + "vydejkaItem", vydejkaItem);
					listItem.Add(polozka);
				}

				#endregion

				#region Summary

				List<XElement> vydejkaSummary = new List<XElement>();

				#endregion



				XElement ExpediceElement = null;

				ExpediceElement = new XElement(vyd + "vydejka",
					new XAttribute("version", "2.0"),
					new XElement(vyd + "vydejkaHeader", listHeader),
					new XElement(vyd + "vydejkaDetail", listItem),
					new XElement(vyd + "vydejkaSummary", vydejkaSummary)
					);


				XElement root = new XElement(dat + "dataPack",
					new XAttribute(XNamespace.Xmlns + "dat", "http://www.stormware.cz/schema/version_2/data.xsd"),
					new XAttribute(XNamespace.Xmlns + "vyd", "http://www.stormware.cz/schema/version_2/vydejka.xsd"),
					new XAttribute(XNamespace.Xmlns + "typ", "http://www.stormware.cz/schema/version_2/type.xsd"),
					new XAttribute("id", "EXP" + davkacislo.ToString()),
					new XAttribute("ico", Globals_V1.Konfigurace.PohodaInfo[0].ICO),
					new XAttribute("application", Fask.SQL.Constants.Common.application_S),
					new XAttribute("version", "2.0"),
					new XAttribute("note", "Import Vydejky"),

					new XElement(dat + "dataPackItem",
						new XAttribute("id", "EXP" + davkacislo.ToString()),
						new XAttribute("version", "2.0"),
						ExpediceElement));



				root.Save(filename);
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML.Classes.Prodej2", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				return false;
			}

			return true;

			
			#endregion



		}

		/// <summary>
		/// \TODO Predelat na lepsi reakci
		/// </summary>
		/// <param name="filename"></param>
		/// <param name="uzivatel"></param>
		/// <returns></returns>
		internal static string LoadResponse_Expedice_XML(string filename, Fask.Server.Interfaces.Classes.User uzivatel)
		{
			XML.MST_Pohoda.CheckExistResponseFile(filename);

			return "OK";
		}
		
		#endregion

		#region Objednavka přijata M 29.11.2019 předelane na objektorve, potřeba otestovat, zda to k nečemu je....

		/// <summary>
		/// VELKE TODO Predelat do Objektoveho
		/// </summary>
		/// <param name="filename"></param>
		/// <param name="p"></param>
		/// <param name="cZMST_DIDataTable"></param>
		/// <returns></returns>
		internal static bool CreateRequest_Objpm_XML(string filename, string note, Fask.DataSets.ProdejData.CZMST_DIDataTable cZMST_DIDataTable, Doklad typDoklad)
		{
			#region OLD


			////string partnerID = string.Empty;
			////foreach (Fask.DataSets.ProdejData.CZMST_DIRow row in cZMST_DIDataTable)
			////{
			////    if (!row.IsODB_IDNull() && row.ODB_ID.Trim() != string.Empty)
			////    {
			////        //Log.writeErrorLog("ODB_ID: " + row.ODB_ID.ToString());
			////        partnerID = row.ODB_ID.Trim();
			////        break;
			////    }
			////}

			////var idcizimena = cZMST_DIDataTable[0].mena_ID;

			//TextWriter tw = new StreamWriter(filename);

			////int davkacislo = cZMST_DIDataTable[0].CountEntries;

			//////rada dokladu cizi meny ...
			////string idsradadokladu = null;
			////try
			////{
			////    if (!String.IsNullOrEmpty(idcizimena)) //pouze pokud bude doklad v cizi mene ...
			////    {
			////        Datasets.DatabasePohoda.sCRadyDataTable dt_crady = Database.Pohoda.sCRady_GetDataBy_RokDokladObsahtextu(
			////        DateTime.Now.Year,
			////        21, //prijate objednavky
			////        "%" + Globals.RadaCiziMenaText + "%");

			////        if (dt_crady != null && dt_crady.Count > 0)
			////        {
			////            idsradadokladu = dt_crady[0].IDS;
			////        }
			////    }
			////}
			////catch (Exception ex)
			////{
			////    Fask.Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML", "WriteObjednavkaPrijataMXML", ex);
			////}

			//tw.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
			//tw.WriteLine("<dat:dataPack id=\"objpcm" + davkacislo + "\" ico=\"" + Globals.ICO + "\" application=\"MST\" version = \"2.0\" note=\"Import objednavky\"");
			//tw.WriteLine("xmlns:dat=\"http://www.stormware.cz/schema/version_2/data.xsd\"");
			//tw.WriteLine("xmlns:ord=\"http://www.stormware.cz/schema/version_2/order.xsd\"");
			//tw.WriteLine("xmlns:typ=\"http://www.stormware.cz/schema/version_2/type.xsd\">");

			//tw.WriteLine("<dat:dataPackItem id=\"OBJPCM" + davkacislo + "\" version=\"2.0\">");
			//tw.WriteLine("  <ord:order version=\"2.0\">");

			//// hlavicka
			//tw.WriteLine("      <ord:orderHeader>");
			//tw.WriteLine("          <ord:orderType>receivedOrder</ord:orderType>");

			////rada dokladu pro cizi menu ...
			//if (!string.IsNullOrEmpty(idsradadokladu))
			//{
			//    tw.WriteLine("          <ord:number>");
			//    tw.WriteLine("              <typ:ids>" + idsradadokladu + "</typ:ids>");
			//    tw.WriteLine("          </ord:number>");
			//}

			//tw.WriteLine("          <ord:paymentType>");
			//tw.WriteLine("              <typ:ids>" + Globals.KodFormaUhrady + "</typ:ids>");
			//tw.WriteLine("          </ord:paymentType>");

			//tw.WriteLine("          <ord:date>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString("00") + "-" + DateTime.Now.Day.ToString("00") + "</ord:date>");
			////tw.WriteLine("          <ord:numberOrder></pri:numberOrder>");
			//tw.WriteLine("          <ord:text>" + note + "</ord:text>");

			////partner
			//if (partnerID != string.Empty)
			//{
			//    tw.WriteLine("          <ord:partnerIdentity>");
			//    tw.WriteLine("              <typ:id>" + partnerID + "</typ:id>");
			//    tw.WriteLine("          </ord:partnerIdentity>");
			//}

			////tw.WriteLine("          <ord:note>" + "nacteno z xml - terminal" + "</ord:note>");
			//tw.WriteLine("          <ord:intNote>" + "nacteno z mobilniho terminalu" + "</ord:intNote>");
			//tw.WriteLine("      </ord:orderHeader>");

			////polozky 
			//tw.WriteLine("      <ord:orderDetail>");

			//foreach (Fask.DataSets.ProdejData.CZMST_DIRow row in cZMST_DIDataTable)
			//{
			//    tw.WriteLine("      <ord:orderItem>");
			//    tw.WriteLine("          <ord:quantity>" + ((float)row.QTYSHPPD).ToString(System.Globalization.NumberFormatInfo.InvariantInfo) + "</ord:quantity>");
			//    tw.WriteLine("          <ord:unit>" + row.MJ.Trim() + "</ord:unit>");
			//    tw.WriteLine("          <ord:coefficient>1</ord:coefficient>");
			//    tw.WriteLine("          <ord:discountPercentage>0</ord:discountPercentage>");
			//    tw.WriteLine("          <ord:stockItem>");
			//    tw.WriteLine("              <typ:stockItem>");
			//    tw.WriteLine("                  <typ:id>" + row.ITEMNMBR.Trim() + "</typ:id>");
			//    tw.WriteLine("              </typ:stockItem>");

			//    if (row.SERLTNUM.Trim().Length > 0)
			//        tw.WriteLine("              <typ:serialNumber>" + row.SERLTNUM.Trim() + "</typ:serialNumber>");

			//    tw.WriteLine("          </ord:stockItem>");
			//    tw.WriteLine("      </ord:orderItem>");
			//}

			//tw.WriteLine("      </ord:orderDetail>");

			////Cizi meny ... 
			//try
			//{

			//    if (!String.IsNullOrEmpty(idcizimena))
			//    {
			//        tw.WriteLine("      <ord:orderSummary>");
			//        tw.WriteLine("          <ord:foreignCurrency>");
			//        tw.WriteLine("              <typ:currency>");
			//        tw.WriteLine("                  <typ:id>" + idcizimena + "</typ:id>");
			//        tw.WriteLine("              </typ:currency>");
			//        tw.WriteLine("          </ord:foreignCurrency>");
			//        tw.WriteLine("      </ord:orderSummary>");
			//    }
			//}
			//catch (Exception ex)
			//{
			//    Fask.Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML", "WriteObjednavkaPrijataMXML", ex);
			//}

			//tw.WriteLine("  </ord:order>");
			//tw.WriteLine("</dat:dataPackItem>");
			//tw.WriteLine("</dat:dataPack>");

			//tw.Flush();
			//tw.Close();
			//return true; 

			#endregion

			#region NEW

			System.Globalization.NumberFormatInfo nfi = System.Globalization.NumberFormatInfo.InvariantInfo;

			XNamespace dat = "http://www.stormware.cz/schema/version_2/data.xsd";
			XNamespace ord = "http://www.stormware.cz/schema/version_2/order.xsd";
			XNamespace typ = "http://www.stormware.cz/schema/version_2/type.xsd";

			try
			{

				#region Dotaženi dat

				string partnerID = string.Empty;
				foreach (Fask.DataSets.ProdejData.CZMST_DIRow row in cZMST_DIDataTable)
				{
					if (!row.IsODB_IDNull() && row.ODB_ID.Trim() != string.Empty)
					{
						//Log.writeErrorLog("ODB_ID: " + row.ODB_ID.ToString());
						partnerID = row.ODB_ID.Trim();
						break;
					}
				}

				var idcizimena = cZMST_DIDataTable[0].mena_ID;

				int davkacislo = cZMST_DIDataTable[0].CountEntries;

				//rada dokladu cizi meny ...
				string idsradadokladu = null;
				try
				{
					if (!String.IsNullOrEmpty(idcizimena)) //pouze pokud bude doklad v cizi mene ...
					{
						Datasets.DatabasePohoda.sCRadyDataTable dt_crady = Database.Pohoda.sCRady_GetDataBy_RokDokladObsahtextu(
						DateTime.Now.Year,
						21, //prijate objednavky
						"%" + Globals_V1.Konfigurace.Prodej[0].RadaCiziMenaText + "%");

						if (dt_crady != null && dt_crady.Count > 0)
						{
							idsradadokladu = dt_crady[0].IDS;
						}
					}
				}
				catch (Exception ex)
				{
					Fask.Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML", "WriteObjednavkaPrijataMXML", ex);
				}

				#endregion


				#region Header

				List<XElement> listHeader = new List<XElement>();

				
				listHeader.Add(new XElement(ord + "orderType", "receivedOrder"));

				listHeader.Add(new XElement(ord + "date", XmlConvert.ToString(DateTime.Now, "yyyy-MM-dd")));

				listHeader.Add(new XElement(ord + "text", note));

				listHeader.Add(new XElement(ord + "intNote", note));


				if (!string.IsNullOrEmpty(partnerID))
				{
					XElement IDPartnerFakturacni;

					IDPartnerFakturacni = new XElement(typ + "id", partnerID.Trim());

					listHeader.Add(new XElement(ord + "partnerIdentity", IDPartnerFakturacni));
				}

				if (!string.IsNullOrEmpty(idsradadokladu))
				{
					XElement IDradadok;

					IDradadok = new XElement(typ + "ids", idsradadokladu.Trim());

					listHeader.Add(new XElement(ord + "number", IDradadok));
				}


				XElement IDPayTyp;
				IDPayTyp = new XElement(typ + "ids", Globals_V1.Konfigurace.Prodej[0].KodFormaUhrady);
				listHeader.Add(new XElement(ord + "paymentType", IDPayTyp));


				#endregion

				#region Items

				List<XElement> listItem = new List<XElement>();

				foreach (Fask.DataSets.ProdejData.CZMST_DIRow row in cZMST_DIDataTable)
				{
					List<XElement> vydejkaItem = new List<XElement>();

					vydejkaItem.Add(new XElement(ord + "quantity", ((float)row.QTYSHPPD).ToString(nfi)));
					vydejkaItem.Add(new XElement(ord + "unit", row.MJ.Trim()));
					vydejkaItem.Add(new XElement(ord + "coefficient", 1));
					vydejkaItem.Add(new XElement(ord + "discountPercentage", 0));


					if (row.SERLTNUM.Trim().Length > 0)
						vydejkaItem.Add(new XElement(ord + "stockItem", new XElement(typ + "stockItem", new XElement(typ + "id", row.ITEMNMBR.Trim())), new XElement(typ + "serialNumber", row.SERLTNUM.Trim())));
					else
						vydejkaItem.Add(new XElement(ord + "stockItem", new XElement(typ + "stockItem", new XElement(typ + "id", row.ITEMNMBR.Trim()))));

					XElement polozka = new XElement(ord + "orderItem", vydejkaItem);
					listItem.Add(polozka);
				}

				#endregion

				#region Summary

				List<XElement> orderSummary = new List<XElement>();

				if (!string.IsNullOrEmpty(idcizimena))
				{

					var Kurzdt = Database.Pohoda.sCKurspol_GetDataByKod(int.Parse(idcizimena)); ;
					double Kurz = 0;

					if ((Kurzdt != null) && (Kurzdt.Count > 0))
					{
						Kurz = Kurzdt[0].NBs;
						orderSummary.Add(new XElement(ord + "foreignCurrency",
									new XElement(typ + "currency", new XElement(typ + "id", idcizimena.Trim())),
									new XElement(typ + "rate", Kurz)));
					}
					else
					{
						orderSummary.Add(new XElement(ord + "foreignCurrency",
															new XElement(typ + "currency", new XElement(typ + "id", idcizimena.Trim()))));
					}
				}

				#endregion



				XElement ObjednavkaElement = null;

				ObjednavkaElement = new XElement(ord + "order",
					new XAttribute("version", "2.0"),
					new XElement(ord + "orderHeader", listHeader),
					new XElement(ord + "orderDetail", listItem),
					new XElement(ord + "orderSummary", orderSummary)
					);


				XElement root = new XElement(dat + "dataPack",
					new XAttribute(XNamespace.Xmlns + "dat", "http://www.stormware.cz/schema/version_2/data.xsd"),
					new XAttribute(XNamespace.Xmlns + "ord", "http://www.stormware.cz/schema/version_2/order.xsd"),
					new XAttribute(XNamespace.Xmlns + "typ", "http://www.stormware.cz/schema/version_2/type.xsd"),
					new XAttribute("id", "objpcm" + davkacislo.ToString()),
					new XAttribute("ico", Globals_V1.Konfigurace.PohodaInfo[0].ICO),
					new XAttribute("application", Fask.SQL.Constants.Common.application_S),
					new XAttribute("version", "2.0"),
					new XAttribute("note", "Import objednavky"),

					new XElement(dat + "dataPackItem",
						new XAttribute("id", "objpcm" + davkacislo.ToString()),
						new XAttribute("version", "2.0"),
						ObjednavkaElement));



				root.Save(filename);
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML.Classes.Prodej2", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				return false;
			}

			return true;


			#endregion
		}

		/// <summary>
		/// predelat na lepsi reakci
		/// </summary>
		/// <param name="filename"></param>
		/// <param name="uzivatel"></param>
		/// <returns></returns>
		internal static string LoadResponse_Objpm_XML(string filename, Fask.Server.Interfaces.Classes.User uzivatel)
		{
			XML.MST_Pohoda.CheckExistResponseFile(filename);

			XML.MST_Pohoda.UpdateCreatorObj(uzivatel, filename);
			return "OK";
		}
		
		#endregion

		#region Objednavka přijata CM 29.11.2019 předelane na objektorve, potřeba otestovat, zda to k nečemu je....

		/// <summary>
		/// \TODO Predelat na objektovo
		/// </summary>
		/// <param name="filename"></param>
		/// <param name="p"></param>
		/// <param name="cZMST_DIDataTable"></param>
		/// <returns></returns>
		internal static bool CreateRequest_Objpcm_XML(string filename, string note, Fask.DataSets.ProdejData.CZMST_DIDataTable cZMST_DIDataTable, Doklad typDoklad)
		{
			#region Old

			//string partnerID = string.Empty;
			//foreach (Fask.DataSets.ProdejData.CZMST_DIRow row in cZMST_DIDataTable)
			//{
			//    if (!row.IsODB_IDNull() && row.ODB_ID.Trim() != string.Empty)
			//    {
			//        //Log.writeErrorLog("ODB_ID: " + row.ODB_ID.ToString());
			//        partnerID = row.ODB_ID.Trim();
			//        break;
			//    }
			//}

			////var idcizimena = cZMST_DIDataTable[0].mena_ID;

			//TextWriter tw = new StreamWriter(filename);
			////XML.MST_Pohoda.WriteObjednavkaPrijataCMXML(p, cZMST_DIDataTable, partnerID, tw);


			//int davkacislo = cZMST_DIDataTable[0].CountEntries;

			//tw.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
			//tw.WriteLine("<dat:dataPack id=\"objpcm" + davkacislo + "\" ico=\"" + Globals.ICO + "\" application=\"MST\" version = \"2.0\" note=\"Import objednavky\"");
			//tw.WriteLine("xmlns:dat=\"http://www.stormware.cz/schema/version_2/data.xsd\"");
			//tw.WriteLine("xmlns:ord=\"http://www.stormware.cz/schema/version_2/order.xsd\"");
			//tw.WriteLine("xmlns:typ=\"http://www.stormware.cz/schema/version_2/type.xsd\">");

			//tw.WriteLine("<dat:dataPackItem id=\"OBJPCM" + davkacislo + "\" version=\"2.0\">");
			//tw.WriteLine("  <ord:order version=\"2.0\">");

			//// hlavicka
			//tw.WriteLine("      <ord:orderHeader>");
			//tw.WriteLine("          <ord:orderType>receivedOrder</ord:orderType>");

			//tw.WriteLine("          <ord:paymentType>");
			//// \TODO: Zmenit na potrebny typ
			//tw.WriteLine("              <typ:ids>" + Globals.KodFormaUhrady + "</typ:ids>");
			//tw.WriteLine("          </ord:paymentType>");

			//tw.WriteLine("          <ord:date>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString("00") + "-" + DateTime.Now.Day.ToString("00") + "</ord:date>");
			////tw.WriteLine("          <ord:numberOrder></pri:numberOrder>");
			//tw.WriteLine("          <ord:text>" + note + "</ord:text>");

			////partner
			//if (partnerID != string.Empty)
			//{
			//    tw.WriteLine("          <ord:partnerIdentity>");
			//    tw.WriteLine("              <typ:id>" + partnerID + "</typ:id>");
			//    tw.WriteLine("          </ord:partnerIdentity>");
			//}

			////tw.WriteLine("          <ord:note>" + "nacteno z xml - terminal" + "</ord:note>");
			//tw.WriteLine("          <ord:intNote>" + "nacteno z mobilniho terminalu" + "</ord:intNote>");
			//tw.WriteLine("      </ord:orderHeader>");

			////polozky 
			//tw.WriteLine("      <ord:orderDetail>");

			//foreach (Fask.DataSets.ProdejData.CZMST_DIRow row in cZMST_DIDataTable)
			//{
			//    tw.WriteLine("      <ord:orderItem>");
			//    tw.WriteLine("          <ord:quantity>" + ((float)row.QTYSHPPD).ToString(System.Globalization.NumberFormatInfo.InvariantInfo) + "</ord:quantity>");
			//    tw.WriteLine("          <ord:unit>" + row.MJ.Trim() + "</ord:unit>");
			//    tw.WriteLine("          <ord:coefficient>1</ord:coefficient>");
			//    tw.WriteLine("          <ord:discountPercentage>0</ord:discountPercentage>");
			//    tw.WriteLine("          <ord:stockItem>");
			//    tw.WriteLine("              <typ:stockItem>");
			//    tw.WriteLine("                  <typ:id>" + row.ITEMNMBR.Trim() + "</typ:id>");
			//    tw.WriteLine("              </typ:stockItem>");

			//    if (row.SERLTNUM.Trim().Length > 0)
			//        tw.WriteLine("              <typ:serialNumber>" + row.SERLTNUM.Trim() + "</typ:serialNumber>");

			//    tw.WriteLine("          </ord:stockItem>");
			//    tw.WriteLine("      </ord:orderItem>");
			//}

			//tw.WriteLine("      </ord:orderDetail>");

			////Cizi meny ... 
			//try
			//{
			//    int? cizimenaid = null;
			//    Datasets.DatabasePohoda.sCMenyDataTable dt_cmeny = Database.Pohoda.sCMeny_GetDataByKod(Globals.KodCiziMena);

			//    if (dt_cmeny.Count > 0)
			//        cizimenaid = dt_cmeny[0].ID;

			//    if (cizimenaid.HasValue)
			//    {
			//        tw.WriteLine("      <ord:orderSummary>");
			//        tw.WriteLine("          <ord:foreignCurrency>");
			//        tw.WriteLine("              <typ:currency>");
			//        tw.WriteLine("                  <typ:id>" + cizimenaid + "</typ:id>");
			//        tw.WriteLine("              </typ:currency>");
			//        tw.WriteLine("          </ord:foreignCurrency>");
			//        tw.WriteLine("      </ord:orderSummary>");
			//    }
			//}
			//catch (Exception ex)
			//{
			//    Fask.Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML", "WriteObjednavkaPrijataCMXML", ex);
			//}

			//tw.WriteLine("  </ord:order>");
			//tw.WriteLine("</dat:dataPackItem>");
			//tw.WriteLine("</dat:dataPack>");

			//tw.Flush();
			//tw.Close();
			//return true; 
			#endregion

			#region NEW

			System.Globalization.NumberFormatInfo nfi = System.Globalization.NumberFormatInfo.InvariantInfo;

			XNamespace dat = "http://www.stormware.cz/schema/version_2/data.xsd";
			XNamespace ord = "http://www.stormware.cz/schema/version_2/order.xsd";
			XNamespace typ = "http://www.stormware.cz/schema/version_2/type.xsd";

			try
			{

				#region Dotaženi dat

				string partnerID = string.Empty;
				foreach (Fask.DataSets.ProdejData.CZMST_DIRow row in cZMST_DIDataTable)
				{
					if (!row.IsODB_IDNull() && row.ODB_ID.Trim() != string.Empty)
					{
						partnerID = row.ODB_ID.Trim();
						break;
					}
				}

				int davkacislo = cZMST_DIDataTable[0].CountEntries;

				#endregion


				#region Header

				List<XElement> listHeader = new List<XElement>();


				listHeader.Add(new XElement(ord + "orderType", "receivedOrder"));

				listHeader.Add(new XElement(ord + "date", XmlConvert.ToString(DateTime.Now, "yyyy-MM-dd")));

				listHeader.Add(new XElement(ord + "text", note));

				listHeader.Add(new XElement(ord + "intNote", note));


				if (!string.IsNullOrEmpty(partnerID))
				{
					XElement IDPartnerFakturacni;

					IDPartnerFakturacni = new XElement(typ + "id", partnerID.Trim());

					listHeader.Add(new XElement(ord + "partnerIdentity", IDPartnerFakturacni));
				}

				XElement IDPayTyp;
				IDPayTyp = new XElement(typ + "ids", Globals_V1.Konfigurace.Prodej[0].KodFormaUhrady);
				listHeader.Add(new XElement(ord + "paymentType", IDPayTyp));


				#endregion

				#region Items

				List<XElement> listItem = new List<XElement>();

				foreach (Fask.DataSets.ProdejData.CZMST_DIRow row in cZMST_DIDataTable)
				{
					List<XElement> vydejkaItem = new List<XElement>();

					vydejkaItem.Add(new XElement(ord + "quantity", ((float)row.QTYSHPPD).ToString(nfi)));
					vydejkaItem.Add(new XElement(ord + "unit", row.MJ.Trim()));
					vydejkaItem.Add(new XElement(ord + "coefficient", 1));
					vydejkaItem.Add(new XElement(ord + "discountPercentage", 0));


					if (row.SERLTNUM.Trim().Length > 0)
						vydejkaItem.Add(new XElement(ord + "stockItem", new XElement(typ + "stockItem", new XElement(typ + "id", row.ITEMNMBR.Trim())), new XElement(typ + "serialNumber", row.SERLTNUM.Trim())));
					else
						vydejkaItem.Add(new XElement(ord + "stockItem", new XElement(typ + "stockItem", new XElement(typ + "id", row.ITEMNMBR.Trim()))));

					XElement polozka = new XElement(ord + "orderItem", vydejkaItem);
					listItem.Add(polozka);
				}

				#endregion

				#region Summary

				List<XElement> orderSummary = new List<XElement>();


				int? cizimenaid = null;
				Datasets.DatabasePohoda.sCMenyDataTable dt_cmeny = Database.Pohoda.sCMeny_GetDataByKod(Globals_V1.Konfigurace.Prodej[0].KodCiziMena);

				if (dt_cmeny.Count > 0)
					cizimenaid = dt_cmeny[0].ID;

				if (cizimenaid.HasValue)
				{
					orderSummary.Add(new XElement(ord + "foreignCurrency",
														new XElement(typ + "currency", new XElement(typ + "id", cizimenaid.Value.ToString()))));
				}

				#endregion



				XElement ObjednavkaElement = null;

				ObjednavkaElement = new XElement(ord + "order",
					new XAttribute("version", "2.0"),
					new XElement(ord + "orderHeader", listHeader),
					new XElement(ord + "orderDetail", listItem),
					new XElement(ord + "orderSummary", orderSummary)
					);


				XElement root = new XElement(dat + "dataPack",
					new XAttribute(XNamespace.Xmlns + "dat", "http://www.stormware.cz/schema/version_2/data.xsd"),
					new XAttribute(XNamespace.Xmlns + "ord", "http://www.stormware.cz/schema/version_2/order.xsd"),
					new XAttribute(XNamespace.Xmlns + "typ", "http://www.stormware.cz/schema/version_2/type.xsd"),
					new XAttribute("id", "objpcm" + davkacislo.ToString()),
					new XAttribute("ico", Globals_V1.Konfigurace.PohodaInfo[0].ICO),
					new XAttribute("application", Fask.SQL.Constants.Common.application_S),
					new XAttribute("version", "2.0"),
					new XAttribute("note", "Import objednavky"),

					new XElement(dat + "dataPackItem",
						new XAttribute("id", "objpcm" + davkacislo.ToString()),
						new XAttribute("version", "2.0"),
						ObjednavkaElement));



				root.Save(filename);
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML.Classes.Prodej2", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				return false;
			}

			return true;


			#endregion

		}

		/// <summary>
		/// \TODO PRedelat na lepsi reakci
		/// </summary>
		/// <param name="filename"></param>
		/// <param name="uzivatel"></param>
		/// <returns></returns>
		internal static string LoadResponse_Objpcm_XML(string filename, Fask.Server.Interfaces.Classes.User uzivatel)
		{
			XML.MST_Pohoda.CheckExistResponseFile(filename);

			XML.MST_Pohoda.UpdateCreatorObj(uzivatel, filename);
			return "OK";
		}
		
		#endregion

		#region Objednavka přijata 29.11.2019 předelane na objektorve, potřeba otestovat, zda to k nečemu je....

		/// <summary>
		/// VELKE TODO Predelat do Objektoveho
		/// </summary>
		/// <param name="filename"></param>
		/// <param name="p"></param>
		/// <param name="cZMST_DIDataTable"></param>
		/// <returns></returns>
		internal static bool CreateRequest_Objp_XML(string filename, string note, Fask.DataSets.ProdejData.CZMST_DIDataTable cZMST_DIDataTable, Doklad typDoklad)
		{
			#region OLD

			//string partnerID = string.Empty;
			//foreach (Fask.DataSets.ProdejData.CZMST_DIRow row in cZMST_DIDataTable)
			//{
			//    if (!row.IsODB_IDNull() && row.ODB_ID.Trim() != string.Empty)
			//    {
			//        partnerID = row.ODB_ID.Trim();
			//        break;
			//    }
			//}

			//TextWriter tw = new StreamWriter(filename);
			////XML.MST_Pohoda.WriteObjednavkaPrijataXML(p, cZMST_DIDataTable, partnerID, tw, typDoklad);

			//int davkacislo = cZMST_DIDataTable[0].CountEntries;

			//tw.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
			//tw.WriteLine("<dat:dataPack id=\"objp" + davkacislo + "\" ico=\"" + Globals.ICO + "\" application=\"MST\" version = \"2.0\" note=\"Import objednavky\"");
			//tw.WriteLine("xmlns:dat=\"http://www.stormware.cz/schema/version_2/data.xsd\"");
			//tw.WriteLine("xmlns:ord=\"http://www.stormware.cz/schema/version_2/order.xsd\"");
			//tw.WriteLine("xmlns:typ=\"http://www.stormware.cz/schema/version_2/type.xsd\">");

			//tw.WriteLine("<dat:dataPackItem id=\"OBJP" + davkacislo + "\" version=\"2.0\">");
			//tw.WriteLine("  <ord:order version=\"2.0\">");

			//// hlavicka
			//tw.WriteLine("      <ord:orderHeader>");
			//tw.WriteLine("          <ord:orderType>receivedOrder</ord:orderType>");
			//tw.WriteLine("          <ord:date>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString("00") + "-" + DateTime.Now.Day.ToString("00") + "</ord:date>");
			////tw.WriteLine("          <ord:numberOrder></pri:numberOrder>");
			//if (!string.IsNullOrEmpty(typDoklad.idsradatext))
			//{
			//    tw.WriteLine("          <ord:number>");
			//    tw.WriteLine("              <typ:id>" + typDoklad.idsradatext + "</typ:id>");
			//    tw.WriteLine("          </ord:number>");
			//}
			//tw.WriteLine("          <ord:text>" + note + "</ord:text>");

			////partner
			//if (partnerID != string.Empty)
			//{
			//    tw.WriteLine("          <ord:partnerIdentity>");
			//    tw.WriteLine("              <typ:id>" + partnerID + "</typ:id>");
			//    tw.WriteLine("          </ord:partnerIdentity>");
			//}

			////tw.WriteLine("          <ord:note>" + "nacteno z xml (terminal)" + "</ord:note>");
			//tw.WriteLine("          <ord:intNote>" + "nacteno z mobilniho terminalu" + "</ord:intNote>");
			//tw.WriteLine("      </ord:orderHeader>");

			////polozky 
			//tw.WriteLine("      <ord:orderDetail>");

			//foreach (Fask.DataSets.ProdejData.CZMST_DIRow row in cZMST_DIDataTable)
			//{
			//    tw.WriteLine("      <ord:orderItem>");
			//    tw.WriteLine("          <ord:quantity>" + ((float)row.QTYSHPPD).ToString(System.Globalization.NumberFormatInfo.InvariantInfo) + "</ord:quantity>");
			//    tw.WriteLine("          <ord:unit>" + row.MJ.Trim() + "</ord:unit>");
			//    tw.WriteLine("          <ord:coefficient>1</ord:coefficient>");
			//    tw.WriteLine("          <ord:discountPercentage>0</ord:discountPercentage>");
			//    tw.WriteLine("          <ord:stockItem>");
			//    tw.WriteLine("              <typ:stockItem>");
			//    tw.WriteLine("                  <typ:id>" + row.ITEMNMBR.Trim() + "</typ:id>");
			//    tw.WriteLine("              </typ:stockItem>");

			//    if (row.SERLTNUM.Trim().Length > 0)
			//        tw.WriteLine("              <typ:serialNumber>" + row.SERLTNUM.Trim() + "</typ:serialNumber>");

			//    tw.WriteLine("          </ord:stockItem>");
			//    tw.WriteLine("      </ord:orderItem>");
			//}

			//tw.WriteLine("      </ord:orderDetail>");
			//tw.WriteLine("  </ord:order>");
			//tw.WriteLine("</dat:dataPackItem>");
			//tw.WriteLine("</dat:dataPack>");


			//tw.Flush();
			//tw.Close();
			//return true; 
			#endregion

			#region NEW

			System.Globalization.NumberFormatInfo nfi = System.Globalization.NumberFormatInfo.InvariantInfo;

			XNamespace dat = "http://www.stormware.cz/schema/version_2/data.xsd";
			XNamespace ord = "http://www.stormware.cz/schema/version_2/order.xsd";
			XNamespace typ = "http://www.stormware.cz/schema/version_2/type.xsd";

			try
			{

				#region Dotaženi dat

				string partnerID = string.Empty;
				foreach (Fask.DataSets.ProdejData.CZMST_DIRow row in cZMST_DIDataTable)
				{
					if (!row.IsODB_IDNull() && row.ODB_ID.Trim() != string.Empty)
					{
						partnerID = row.ODB_ID.Trim();
						break;
					}
				}

				int davkacislo = cZMST_DIDataTable[0].CountEntries;

				#endregion


				#region Header

				List<XElement> listHeader = new List<XElement>();


				listHeader.Add(new XElement(ord + "orderType", "receivedOrder"));

				listHeader.Add(new XElement(ord + "date", XmlConvert.ToString(DateTime.Now, "yyyy-MM-dd")));

				listHeader.Add(new XElement(ord + "text", note));

				listHeader.Add(new XElement(ord + "intNote", note));


				if (!string.IsNullOrEmpty(partnerID))
				{
					XElement IDPartnerFakturacni;

					IDPartnerFakturacni = new XElement(typ + "id", partnerID.Trim());

					listHeader.Add(new XElement(ord + "partnerIdentity", IDPartnerFakturacni));
				}

				if (!string.IsNullOrEmpty(typDoklad.idsradatext))
				{
					XElement IDradadok;

					IDradadok = new XElement(typ + "ids", typDoklad.idsradatext.Trim());

					listHeader.Add(new XElement(ord + "number", IDradadok));
				}

				#endregion

				#region Items

				List<XElement> listItem = new List<XElement>();

				foreach (Fask.DataSets.ProdejData.CZMST_DIRow row in cZMST_DIDataTable)
				{
					List<XElement> vydejkaItem = new List<XElement>();

					vydejkaItem.Add(new XElement(ord + "quantity", ((float)row.QTYSHPPD).ToString(nfi)));
					vydejkaItem.Add(new XElement(ord + "unit", row.MJ.Trim()));
					vydejkaItem.Add(new XElement(ord + "coefficient", 1));
					vydejkaItem.Add(new XElement(ord + "discountPercentage", 0));


					if (row.SERLTNUM.Trim().Length > 0)
						vydejkaItem.Add(new XElement(ord + "stockItem", new XElement(typ + "stockItem", new XElement(typ + "id", row.ITEMNMBR.Trim())), new XElement(typ + "serialNumber", row.SERLTNUM.Trim())));
					else
						vydejkaItem.Add(new XElement(ord + "stockItem", new XElement(typ + "stockItem", new XElement(typ + "id", row.ITEMNMBR.Trim()))));

					XElement polozka = new XElement(ord + "orderItem", vydejkaItem);
					listItem.Add(polozka);
				}

				#endregion

				#region Summary

				List<XElement> orderSummary = new List<XElement>();

				#endregion



				XElement ObjednavkaElement = null;

				ObjednavkaElement = new XElement(ord + "order",
					new XAttribute("version", "2.0"),
					new XElement(ord + "orderHeader", listHeader),
					new XElement(ord + "orderDetail", listItem),
					new XElement(ord + "orderSummary", orderSummary)
					);


				XElement root = new XElement(dat + "dataPack",
					new XAttribute(XNamespace.Xmlns + "dat", "http://www.stormware.cz/schema/version_2/data.xsd"),
					new XAttribute(XNamespace.Xmlns + "ord", "http://www.stormware.cz/schema/version_2/order.xsd"),
					new XAttribute(XNamespace.Xmlns + "typ", "http://www.stormware.cz/schema/version_2/type.xsd"),
					new XAttribute("id", "objp" + davkacislo.ToString()),
					new XAttribute("ico", Globals_V1.Konfigurace.PohodaInfo[0].ICO),
					new XAttribute("application", Fask.SQL.Constants.Common.application_S),
					new XAttribute("version", "2.0"),
					new XAttribute("note", "Import objednavky"),

					new XElement(dat + "dataPackItem",
						new XAttribute("id", "objp" + davkacislo.ToString()),
						new XAttribute("version", "2.0"),
						ObjednavkaElement));



				root.Save(filename);
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML.Classes.Prodej2", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				return false;
			}

			return true;


			#endregion


		}

		/// <summary>
		/// \TODO predelat na lepsi reakci
		/// </summary>
		/// <param name="filename"></param>
		/// <param name="uzivatel"></param>
		/// <returns></returns>
		internal static string LoadResponse_Objp_XML(string filename, Fask.Server.Interfaces.Classes.User uzivatel)
		{
			XML.MST_Pohoda.CheckExistResponseFile(filename);

			XML.MST_Pohoda.UpdateCreatorObj(uzivatel, filename);
			return "OK";
		}

		#endregion

		#region Objednavka bez ODB 29.11.2019 předelane na objektorve, potřeba otestovat, zda to k nečemu je....

		/// <summary>
		/// VELKE TODO Predelat do Objektoveho
		/// </summary>
		/// <param name="filename"></param>
		/// <param name="p"></param>
		/// <param name="cZMST_DIDataTable"></param>
		/// <returns></returns>
		internal static bool CreateRequest_Objbezodb_XML(string filename, string note, Fask.DataSets.ProdejData.CZMST_DIDataTable cZMST_DIDataTable, Doklad typDoklad)
		{

			#region OLD 

			//string partnerID = string.Empty;
			//foreach (Fask.DataSets.ProdejData.CZMST_DIRow row in cZMST_DIDataTable)
			//{
			//    if (!row.IsODB_IDNull() && row.ODB_ID.Trim() != string.Empty)
			//    {
			//        //Log.writeErrorLog("ODB_ID: " + row.ODB_ID.ToString());
			//        partnerID = row.ODB_ID.Trim();
			//        break;
			//    }
			//}


			//var idCiziMena = cZMST_DIDataTable[0].mena_ID;
			//var davkaID = cZMST_DIDataTable[0].CountEntries.ToString();

			//TextWriter tw = new StreamWriter(filename);

			//if (string.IsNullOrEmpty(idCiziMena))
			//{//neni v cizi mene
				//XML.MST_Pohoda.WriteObjednavkaVydanaBezOdbXML(p, cZMST_DIDataTable, tw, partnerID, davkaID);

				//tw.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
				//tw.WriteLine("<dat:dataPack id=\"objvcm" + davkaID + "\" ico=\"" + Globals.ICO + "\" application=\"MST\" version = \"2.0\" note=\"Import objednavky\"");
				//tw.WriteLine("xmlns:dat=\"http://www.stormware.cz/schema/version_2/data.xsd\"");
				//tw.WriteLine("xmlns:ord=\"http://www.stormware.cz/schema/version_2/order.xsd\"");
				//tw.WriteLine("xmlns:typ=\"http://www.stormware.cz/schema/version_2/type.xsd\">");

				//tw.WriteLine("<dat:dataPackItem id=\"OBJVCM" + davkaID + "\" version=\"2.0\">");
				//tw.WriteLine("  <ord:order version=\"2.0\">");

				// hlavicka
				//tw.WriteLine("      <ord:orderHeader>");
				//tw.WriteLine("          <ord:orderType>issuedOrder</ord:orderType>");

				//forma uhrady
				//tw.WriteLine("          <ord:paymentType>");
				//tw.WriteLine("              <typ:ids>" + Globals.KodFormaUhrady + "</typ:ids>");
				//tw.WriteLine("          </ord:paymentType>");

				//tw.WriteLine("          <ord:date>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString("00") + "-" + DateTime.Now.Day.ToString("00") + "</ord:date>");
				//tw.WriteLine("          <ord:numberOrder></pri:numberOrder>");
				//tw.WriteLine("          <ord:text>" + note + "</ord:text>");

				//dodavatel
				//tw.WriteLine("          <ord:partnerIdentity>");
				//tw.WriteLine("              <typ:id>" + partnerID + "</typ:id>");
				//tw.WriteLine("          </ord:partnerIdentity>");

				//tw.WriteLine("          <ord:note>" + "nacteno z xml - terminal" + "</ord:note>");
				//tw.WriteLine("          <ord:intNote>" + "nacteno z xml - terminal" + "</ord:intNote>");
				//tw.WriteLine("      </ord:orderHeader>");

				//polozky 
				//tw.WriteLine("      <ord:orderDetail>");

				//foreach (Fask.DataSets.ProdejData.CZMST_DIRow row in cZMST_DIDataTable)
				//{
				//    tw.WriteLine("      <ord:orderItem>");
				//    tw.WriteLine("          <ord:quantity>" + ((float)row.QTYSHPPD).ToString(System.Globalization.NumberFormatInfo.InvariantInfo) + "</ord:quantity>");
				//    tw.WriteLine("          <ord:unit>" + row.MJ.Trim() + "</ord:unit>");
				//    tw.WriteLine("          <ord:coefficient>1</ord:coefficient>");
				//    tw.WriteLine("          <ord:discountPercentage>0</ord:discountPercentage>");
				//    tw.WriteLine("          <ord:stockItem>");
				//    tw.WriteLine("              <typ:stockItem>");
				//    tw.WriteLine("                  <typ:id>" + row.ITEMNMBR.Trim() + "</typ:id>");
				//    tw.WriteLine("              </typ:stockItem>");

				//    if (row.SERLTNUM.Trim().Length > 0)
				//        tw.WriteLine("              <typ:serialNumber>" + row.SERLTNUM.Trim() + "</typ:serialNumber>");

				//    tw.WriteLine("          </ord:stockItem>");
				//    tw.WriteLine("      </ord:orderItem>");
				//}

				//tw.WriteLine("      </ord:orderDetail>");

				//tw.WriteLine("  </ord:order>");
				//tw.WriteLine("</dat:dataPackItem>");
				//tw.WriteLine("</dat:dataPack>");

			//}
			//else
			//{
				//XML.MST_Pohoda.WriteObjednavkaVydanaBezOdbXMLCM(p, cZMST_DIDataTable, tw, idCiziMena, partnerID, davkaID);

				////Cizi meny ... 
				//int? cizimenaid = null;
				//try
				//{
				//    Datasets.DatabasePohoda.sCMenyDataTable dt_cmeny = Database.Pohoda.sCMeny_GetDataByKod(idCiziMena);

				//    if (dt_cmeny.Count > 0)
				//        cizimenaid = dt_cmeny[0].ID;

				//}
				//catch (Exception ex)
				//{
				//    Fask.Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML", "WriteObjednavkaBezOdbXMLCM", ex);
				//}

				////rada dokladu cizi meny ...
				//string idsradadokladu = null;
				//try
				//{
				//    if (cizimenaid.HasValue) //pouze pokud bude doklad v cizi mene ...
				//    {
				//        Datasets.DatabasePohoda.sCRadyDataTable dt_crady = Database.Pohoda.sCRady_GetDataBy_RokDokladObsahtextu(
				//        DateTime.Now.Year,
				//        10, //vydane objednavky
				//        "%" + Globals.RadaCiziMenaText + "%");

				//        if (dt_crady != null && dt_crady.Count > 0)
				//        {
				//            idsradadokladu = dt_crady[0].IDS;
				//        }
				//    }
				//}
				//catch (Exception ex)
				//{
				//    Fask.Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML", "WriteObjednavkaBezOdbXMLCM", ex);
				//}

				//tw.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
				//tw.WriteLine("<dat:dataPack id=\"objvcm" + davkaID + "\" ico=\"" + Globals.ICO + "\" application=\"MST\" version = \"2.0\" note=\"Import objednavky\"");
				//tw.WriteLine("xmlns:dat=\"http://www.stormware.cz/schema/version_2/data.xsd\"");
				//tw.WriteLine("xmlns:ord=\"http://www.stormware.cz/schema/version_2/order.xsd\"");
				//tw.WriteLine("xmlns:typ=\"http://www.stormware.cz/schema/version_2/type.xsd\">");

				//tw.WriteLine("<dat:dataPackItem id=\"OBJVCM" + davkaID + "\" version=\"2.0\">");
				//tw.WriteLine("  <ord:order version=\"2.0\">");

				// hlavicka
				//tw.WriteLine("      <ord:orderHeader>");
				//tw.WriteLine("          <ord:orderType>issuedOrder</ord:orderType>");

				//rada dokladu pro cizi menu ...
				//if (!string.IsNullOrEmpty(idsradadokladu))
				//{
				//    tw.WriteLine("          <ord:number>");
				//    tw.WriteLine("              <typ:ids>" + idsradadokladu + "</typ:ids>");
				//    tw.WriteLine("          </ord:number>");
				//}

				//Forma uhrady
				//tw.WriteLine("          <ord:paymentType>");
				//tw.WriteLine("              <typ:ids>" + Globals.KodFormaUhrady + "</typ:ids>");
				//tw.WriteLine("          </ord:paymentType>");

				//tw.WriteLine("          <ord:date>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString("00") + "-" + DateTime.Now.Day.ToString("00") + "</ord:date>");
				//tw.WriteLine("          <ord:numberOrder></pri:numberOrder>");
				//tw.WriteLine("          <ord:text>" + note + "</ord:text>");

				//dodavatel
				//tw.WriteLine("          <ord:partnerIdentity>");
				//tw.WriteLine("              <typ:id>" + partnerID + "</typ:id>");
				//tw.WriteLine("          </ord:partnerIdentity>");


				//tw.WriteLine("          <ord:note>" + "nacteno z xml - terminal" + "</ord:note>");
				//tw.WriteLine("          <ord:intNote>" + "nacteno z xml - terminal" + "</ord:intNote>");
				//tw.WriteLine("      </ord:orderHeader>");

				//polozky 
				//tw.WriteLine("      <ord:orderDetail>");

				//foreach (Fask.DataSets.ProdejData.CZMST_DIRow row in cZMST_DIDataTable)
				//{
				//    tw.WriteLine("      <ord:orderItem>");
				//    tw.WriteLine("          <ord:quantity>" + ((float)row.QTYSHPPD).ToString(System.Globalization.NumberFormatInfo.InvariantInfo) + "</ord:quantity>");
				//    tw.WriteLine("          <ord:unit>" + row.MJ.Trim() + "</ord:unit>");
				//    tw.WriteLine("          <ord:coefficient>1</ord:coefficient>");
				//    tw.WriteLine("          <ord:discountPercentage>0</ord:discountPercentage>");
				//    tw.WriteLine("          <ord:stockItem>");
				//    tw.WriteLine("              <typ:stockItem>");
				//    tw.WriteLine("                  <typ:id>" + row.ITEMNMBR.Trim() + "</typ:id>");
				//    tw.WriteLine("              </typ:stockItem>");

				//    if (row.SERLTNUM.Trim().Length > 0)
				//        tw.WriteLine("              <typ:serialNumber>" + row.SERLTNUM.Trim() + "</typ:serialNumber>");

				//    tw.WriteLine("          </ord:stockItem>");
				//    tw.WriteLine("      </ord:orderItem>");
				//}

				//tw.WriteLine("      </ord:orderDetail>");

				//if (cizimenaid.HasValue)
				//{
				//    tw.WriteLine("      <ord:orderSummary>");
				//    tw.WriteLine("          <ord:foreignCurrency>");
				//    tw.WriteLine("              <typ:currency>");
				//    tw.WriteLine("                  <typ:id>" + cizimenaid + "</typ:id>");
				//    tw.WriteLine("              </typ:currency>");
				//    tw.WriteLine("          </ord:foreignCurrency>");
				//    tw.WriteLine("      </ord:orderSummary>");
				//}


				//tw.WriteLine("  </ord:order>");
				//tw.WriteLine("</dat:dataPackItem>");
				//tw.WriteLine("</dat:dataPack>");


			//}

			//WriteObjednavkaPrijataXML(p, cZMST_DIDataTable, partnerID, tw, typDoklad);
			//tw.Flush();
			//tw.Close();
			//return true; 
			#endregion

			#region NEW

			System.Globalization.NumberFormatInfo nfi = System.Globalization.NumberFormatInfo.InvariantInfo;

			XNamespace dat = "http://www.stormware.cz/schema/version_2/data.xsd";
			XNamespace ord = "http://www.stormware.cz/schema/version_2/order.xsd";
			XNamespace typ = "http://www.stormware.cz/schema/version_2/type.xsd";

			try
			{

				#region Dotaženi dat
				//zmene na vychozi hodnotu MaR 10.7.2025
				string partnerID = "1"; // string.Empty;
				foreach (Fask.DataSets.ProdejData.CZMST_DIRow row in cZMST_DIDataTable)
				{
					if (!row.IsODB_IDNull() && row.ODB_ID.Trim() != string.Empty)
					{
						partnerID = row.ODB_ID.Trim();
						break;
					}
     //               else
     //               {
					//	partnerID = "1";
					//	break;
					//}
				}

             
                    int davkacislo = cZMST_DIDataTable[0].CountEntries;
				string idCiziMena = cZMST_DIDataTable[0].Ismena_IDNull() ? "CZK" : cZMST_DIDataTable[0].mena_ID;

				int? cizimenaid = null;
				if (!string.IsNullOrEmpty(idCiziMena))
				{
					try
					{
						var dt_cmeny = Database.Pohoda.sCMeny_GetDataByKod(idCiziMena);
						if (dt_cmeny.Count > 0)
							cizimenaid = dt_cmeny[0].ID;
					}
					catch (Exception ex)
					{
						Fask.Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML", "WriteObjednavkaBezOdbXMLCM", ex);
					}
				}


				//rada dokladu cizi meny ...
				string idsradadokladu = null;
				try
				{
					if (cizimenaid.HasValue) //pouze pokud bude doklad v cizi mene ...
					{
						Datasets.DatabasePohoda.sCRadyDataTable dt_crady = Database.Pohoda.sCRady_GetDataBy_RokDokladObsahtextu(
						DateTime.Now.Year,
						10, //vydane objednavky
						"%" + Globals_V1.Konfigurace.Prodej[0].RadaCiziMenaText + "%");

						if (dt_crady != null && dt_crady.Count > 0)
						{
							idsradadokladu = dt_crady[0].IDS;
						}
					}
				}
				catch (Exception ex)
				{
					Fask.Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML", "WriteObjednavkaBezOdbXMLCM", ex);
				}

				#endregion


				#region Header

				List<XElement> listHeader = new List<XElement>();


				listHeader.Add(new XElement(ord + "orderType", "issuedOrder"));

				listHeader.Add(new XElement(ord + "date", XmlConvert.ToString(DateTime.Now, "yyyy-MM-dd")));

				listHeader.Add(new XElement(ord + "text", note));

				listHeader.Add(new XElement(ord + "intNote", note));


				if (!string.IsNullOrWhiteSpace(partnerID))
				{
					XElement IDPartnerFakturacni;

					IDPartnerFakturacni = new XElement(typ + "id", partnerID.Trim());

					listHeader.Add(new XElement(ord + "partnerIdentity", IDPartnerFakturacni));
				}
				//doplneno MaR 10.7.2025
                else
                {
					XElement IDPartnerFakturacni;

					IDPartnerFakturacni = new XElement(typ + "id", "1");

					listHeader.Add(new XElement(ord + "partnerIdentity", IDPartnerFakturacni));
				}

				XElement IDPayTyp;
				IDPayTyp = new XElement(typ + "ids", Globals_V1.Konfigurace.Prodej[0].KodFormaUhrady);
				listHeader.Add(new XElement(ord + "paymentType", IDPayTyp));

				if (cizimenaid.HasValue)
				{
					if (!string.IsNullOrEmpty(idsradadokladu))
					{
						XElement IDNum;
						IDNum = new XElement(typ + "ids", idsradadokladu);
						listHeader.Add(new XElement(ord + "number", IDNum));
					}

				
				}

				#endregion

				#region Items

				List<XElement> listItem = new List<XElement>();

				foreach (Fask.DataSets.ProdejData.CZMST_DIRow row in cZMST_DIDataTable)
				{
					List<XElement> vydejkaItem = new List<XElement>();

					vydejkaItem.Add(new XElement(ord + "quantity", ((float)row.QTYSHPPD).ToString(nfi)));
					vydejkaItem.Add(new XElement(ord + "unit", row.MJ.Trim()));
					vydejkaItem.Add(new XElement(ord + "coefficient", 1));
					vydejkaItem.Add(new XElement(ord + "discountPercentage", 0));


					if (row.SERLTNUM.Trim().Length > 0)
						vydejkaItem.Add(new XElement(ord + "stockItem", new XElement(typ + "stockItem", new XElement(typ + "id", row.ITEMNMBR.Trim())), new XElement(typ + "serialNumber", row.SERLTNUM.Trim())));
					else
						vydejkaItem.Add(new XElement(ord + "stockItem", new XElement(typ + "stockItem", new XElement(typ + "id", row.ITEMNMBR.Trim()))));

					XElement polozka = new XElement(ord + "orderItem", vydejkaItem);
					listItem.Add(polozka);
				}

				#endregion

				#region Summary

				List<XElement> orderSummary = new List<XElement>();

				if (cizimenaid.HasValue)
				{
					orderSummary.Add(new XElement(ord + "foreignCurrency",
						new XElement(typ + "currency", new XElement(typ + "id", cizimenaid.Value))));
				}

				#endregion



				XElement ObjednavkaElement = null;

				ObjednavkaElement = new XElement(ord + "order",
					new XAttribute("version", "2.0"),
					new XElement(ord + "orderHeader", listHeader),
					new XElement(ord + "orderDetail", listItem),
					new XElement(ord + "orderSummary", orderSummary)
					);


				XElement root = new XElement(dat + "dataPack",
					new XAttribute(XNamespace.Xmlns + "dat", "http://www.stormware.cz/schema/version_2/data.xsd"),
					new XAttribute(XNamespace.Xmlns + "ord", "http://www.stormware.cz/schema/version_2/order.xsd"),
					new XAttribute(XNamespace.Xmlns + "typ", "http://www.stormware.cz/schema/version_2/type.xsd"),
					//new XAttribute("id", "objvcm" + davkacislo.ToString()),
					new XAttribute("id", "objbezodb" + davkacislo.ToString()),
					new XAttribute("ico", Globals_V1.Konfigurace.PohodaInfo[0].ICO),
					new XAttribute("application", Fask.SQL.Constants.Common.application_S),
					new XAttribute("version", "2.0"),
					new XAttribute("note", "Import objednavky"),

					new XElement(dat + "dataPackItem",
						//new XAttribute("id", "objvcm" + davkacislo.ToString()),
						new XAttribute("id", "objbezodb" + davkacislo.ToString()),
						new XAttribute("version", "2.0"),
						ObjednavkaElement));



				root.Save(filename);
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML.Classes.Prodej2", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				return false;
			}

			return true;


			#endregion

		}

		/// <summary>
		/// \TODO Predelat na lepsi reakci
		/// </summary>
		/// <param name="filename"></param>
		/// <param name="uzivatel"></param>
		/// <returns></returns>
		internal static string LoadResponse_Objbezodb_XML(string filename, Fask.Server.Interfaces.Classes.User uzivatel)
		{
			XML.MST_Pohoda.CheckExistResponseFile(filename);

			XML.MST_Pohoda.UpdateCreatorObj(uzivatel, filename);
			return "OK";
		}

		#endregion

		#region Objednavka vydana M 29.11.2019 předelane na objektorve, potřeba otestovat, zda to k nečemu je....

		/// <summary>
		/// VELKE TODO Predelat do Objektoveho
		/// </summary>
		/// <param name="filename"></param>
		/// <param name="p"></param>
		/// <param name="cZMST_DIDataTable"></param>
		/// <returns></returns>
		internal static bool CreateRequest_Objvm_XML(string filename, string note, Fask.DataSets.ProdejData.CZMST_DIDataTable cZMST_DIDataTable, Doklad typDoklad)
		{
			#region OLD 

			//string partnerID = string.Empty;
			//foreach (Fask.DataSets.ProdejData.CZMST_DIRow row in cZMST_DIDataTable)
			//{
			//    if (!row.IsODB_IDNull() && row.ODB_ID.Trim() != string.Empty)
			//    {
			//        partnerID = row.ODB_ID.Trim();
			//        break;
			//    }
			//}


			//var idcizimena = cZMST_DIDataTable[0].mena_ID;

			//TextWriter tw = new StreamWriter(filename);
			////XML.MST_Pohoda.WriteObjednavkaVydanaMXML(p, cZMST_DIDataTable, partnerID, tw, idcizimena);


			//int davkacislo = cZMST_DIDataTable[0].CountEntries;

			////rada dokladu cizi meny ...
			//string idsradadokladu = null;
			//try
			//{
			//    if (!String.IsNullOrEmpty(idcizimena)) //pouze pokud bude doklad v cizi mene ...
			//    {
			//        Datasets.DatabasePohoda.sCRadyDataTable dt_crady = Database.Pohoda.sCRady_GetDataBy_RokDokladObsahtextu(
			//            DateTime.Now.Year,
			//            10, //vydane objednavky
			//            "%" + Globals.RadaCiziMenaText + "%");

			//        if (dt_crady != null && dt_crady.Count > 0)
			//        {
			//            idsradadokladu = dt_crady[0].IDS;
			//        }
			//    }
			//}
			//catch (Exception ex)
			//{
			//    Fask.Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML", "WriteObjednavkaBezOdbXMLCM", ex);
			//}

			//tw.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
			//tw.WriteLine("<dat:dataPack id=\"objvcm" + davkacislo + "\" ico=\"" + Globals.ICO + "\" application=\"MST\" version = \"2.0\" note=\"Import objednavky\"");
			//tw.WriteLine("xmlns:dat=\"http://www.stormware.cz/schema/version_2/data.xsd\"");
			//tw.WriteLine("xmlns:ord=\"http://www.stormware.cz/schema/version_2/order.xsd\"");
			//tw.WriteLine("xmlns:typ=\"http://www.stormware.cz/schema/version_2/type.xsd\">");

			//tw.WriteLine("<dat:dataPackItem id=\"OBJVCM" + davkacislo + "\" version=\"2.0\">");
			//tw.WriteLine("  <ord:order version=\"2.0\">");

			//// hlavicka
			//tw.WriteLine("      <ord:orderHeader>");
			//tw.WriteLine("          <ord:orderType>issuedOrder</ord:orderType>");

			////rada dokladu pro cizi menu ...
			//if (!string.IsNullOrEmpty(idsradadokladu))
			//{
			//    tw.WriteLine("          <ord:number>");
			//    tw.WriteLine("              <typ:ids>" + idsradadokladu + "</typ:ids>");
			//    tw.WriteLine("          </ord:number>");
			//}

			//tw.WriteLine("          <ord:paymentType>");
			//tw.WriteLine("              <typ:ids>" + Globals.KodFormaUhrady + "</typ:ids>");
			//tw.WriteLine("          </ord:paymentType>");

			//tw.WriteLine("          <ord:date>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString("00") + "-" + DateTime.Now.Day.ToString("00") + "</ord:date>");
			////tw.WriteLine("          <ord:numberOrder></pri:numberOrder>");
			//tw.WriteLine("          <ord:text>" + note + "</ord:text>");

			////partner
			//if (partnerID != string.Empty)
			//{
			//    tw.WriteLine("          <ord:partnerIdentity>");
			//    tw.WriteLine("              <typ:id>" + partnerID + "</typ:id>");
			//    tw.WriteLine("          </ord:partnerIdentity>");


			//}

			////tw.WriteLine("          <ord:note>" + "nacteno z xml - terminal" + "</ord:note>");
			//tw.WriteLine("          <ord:intNote>" + "nacteno z mobilniho terminalu" + "</ord:intNote>");
			//tw.WriteLine("      </ord:orderHeader>");

			////polozky 
			//tw.WriteLine("      <ord:orderDetail>");

			//foreach (Fask.DataSets.ProdejData.CZMST_DIRow row in cZMST_DIDataTable)
			//{
			//    tw.WriteLine("      <ord:orderItem>");
			//    tw.WriteLine("          <ord:quantity>" + ((float)row.QTYSHPPD).ToString(System.Globalization.NumberFormatInfo.InvariantInfo) + "</ord:quantity>");
			//    tw.WriteLine("          <ord:unit>" + row.MJ.Trim() + "</ord:unit>");
			//    tw.WriteLine("          <ord:coefficient>1</ord:coefficient>");
			//    tw.WriteLine("          <ord:discountPercentage>0</ord:discountPercentage>");
			//    tw.WriteLine("          <ord:stockItem>");
			//    tw.WriteLine("              <typ:stockItem>");
			//    tw.WriteLine("                  <typ:id>" + row.ITEMNMBR.Trim() + "</typ:id>");
			//    tw.WriteLine("              </typ:stockItem>");

			//    if (row.SERLTNUM.Trim().Length > 0)
			//        tw.WriteLine("              <typ:serialNumber>" + row.SERLTNUM.Trim() + "</typ:serialNumber>");

			//    tw.WriteLine("          </ord:stockItem>");
			//    tw.WriteLine("      </ord:orderItem>");
			//}

			//tw.WriteLine("      </ord:orderDetail>");

			////Cizi meny ... 
			//try
			//{
			//    if (!String.IsNullOrEmpty(idcizimena))
			//    {
			//        tw.WriteLine("      <ord:orderSummary>");
			//        tw.WriteLine("          <ord:foreignCurrency>");
			//        tw.WriteLine("              <typ:currency>");
			//        tw.WriteLine("                  <typ:id>" + idcizimena + "</typ:id>");
			//        tw.WriteLine("              </typ:currency>");
			//        tw.WriteLine("          </ord:foreignCurrency>");
			//        tw.WriteLine("      </ord:orderSummary>");
			//    }
			//}
			//catch (Exception ex)
			//{
			//    Fask.Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML", "WriteObjednavkaCMXML", ex);
			//}

			//tw.WriteLine("  </ord:order>");
			//tw.WriteLine("</dat:dataPackItem>");
			//tw.WriteLine("</dat:dataPack>");



			//tw.Flush();
			//tw.Close();
			//return true;
 
			#endregion

			#region NEW

			System.Globalization.NumberFormatInfo nfi = System.Globalization.NumberFormatInfo.InvariantInfo;

			XNamespace dat = "http://www.stormware.cz/schema/version_2/data.xsd";
			XNamespace ord = "http://www.stormware.cz/schema/version_2/order.xsd";
			XNamespace typ = "http://www.stormware.cz/schema/version_2/type.xsd";

			try
			{

				#region Dotaženi dat

				string partnerID = string.Empty;
				foreach (Fask.DataSets.ProdejData.CZMST_DIRow row in cZMST_DIDataTable)
				{
					if (!row.IsODB_IDNull() && row.ODB_ID.Trim() != string.Empty)
					{
						partnerID = row.ODB_ID.Trim();
						break;
					}
				}


				var idcizimena = cZMST_DIDataTable[0].mena_ID;
				int davkacislo = cZMST_DIDataTable[0].CountEntries;

				//rada dokladu cizi meny ...
				string idsradadokladu = null;
				try
				{
					if (!String.IsNullOrEmpty(idcizimena)) //pouze pokud bude doklad v cizi mene ...
					{
						Datasets.DatabasePohoda.sCRadyDataTable dt_crady = Database.Pohoda.sCRady_GetDataBy_RokDokladObsahtextu(
							DateTime.Now.Year,
							10, //vydane objednavky
							"%" + Globals_V1.Konfigurace.Prodej[0].RadaCiziMenaText + "%");

						if (dt_crady != null && dt_crady.Count > 0)
						{
							idsradadokladu = dt_crady[0].IDS;
						}
					}
				}
				catch (Exception ex)
				{
					Fask.Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML", "WriteObjednavkaBezOdbXMLCM", ex);
				}

				#endregion


				#region Header

				List<XElement> listHeader = new List<XElement>();


				listHeader.Add(new XElement(ord + "orderType", "issuedOrder"));

				listHeader.Add(new XElement(ord + "date", XmlConvert.ToString(DateTime.Now, "yyyy-MM-dd")));

				listHeader.Add(new XElement(ord + "text", note));

				listHeader.Add(new XElement(ord + "intNote", note));


				if (!string.IsNullOrEmpty(partnerID))
				{
					XElement IDPartnerFakturacni;

					IDPartnerFakturacni = new XElement(typ + "id", partnerID.Trim());

					listHeader.Add(new XElement(ord + "partnerIdentity", IDPartnerFakturacni));
				}

				XElement IDPayTyp;
				IDPayTyp = new XElement(typ + "ids", Globals_V1.Konfigurace.Prodej[0].KodFormaUhrady);
				listHeader.Add(new XElement(ord + "paymentType", IDPayTyp));

				//if (!string.IsNullOrEmpty(typDoklad.idsradatext))
				//{
				//	XElement IDradadok;

				//	IDradadok = new XElement(typ + "ids", typDoklad.idsradatext.Trim());

				//	listHeader.Add(new XElement(ord + "number", IDradadok));
				//}

				if (!string.IsNullOrEmpty(typDoklad.Rada_Prefix))
				{
					XElement IDradadok;

					IDradadok = new XElement(typ + "ids", typDoklad.Rada_Prefix.Trim());

					listHeader.Add(new XElement(ord + "number", IDradadok));
				}

				#endregion

				#region Items

				List<XElement> listItem = new List<XElement>();

				foreach (Fask.DataSets.ProdejData.CZMST_DIRow row in cZMST_DIDataTable)
				{
					List<XElement> vydejkaItem = new List<XElement>();

					vydejkaItem.Add(new XElement(ord + "quantity", ((float)row.QTYSHPPD).ToString(nfi)));
					vydejkaItem.Add(new XElement(ord + "unit", row.MJ.Trim()));
					vydejkaItem.Add(new XElement(ord + "coefficient", 1));
					vydejkaItem.Add(new XElement(ord + "discountPercentage", 0));


					if (row.SERLTNUM.Trim().Length > 0)
						vydejkaItem.Add(new XElement(ord + "stockItem", new XElement(typ + "stockItem", new XElement(typ + "id", row.ITEMNMBR.Trim())), new XElement(typ + "serialNumber", row.SERLTNUM.Trim())));
					else
						vydejkaItem.Add(new XElement(ord + "stockItem", new XElement(typ + "stockItem", new XElement(typ + "id", row.ITEMNMBR.Trim()))));

					XElement polozka = new XElement(ord + "orderItem", vydejkaItem);
					listItem.Add(polozka);
				}

				#endregion

				#region Summary

				List<XElement> orderSummary = new List<XElement>();

				if (!string.IsNullOrEmpty(idcizimena))
				{
					orderSummary.Add(new XElement(ord + "foreignCurrency", new XElement(typ + "currency", new XElement(typ + "id", idcizimena.Trim()))));
				}

				#endregion



				XElement ObjednavkaElement = null;

				ObjednavkaElement = new XElement(ord + "order",
					new XAttribute("version", "2.0"),
					new XElement(ord + "orderHeader", listHeader),
					new XElement(ord + "orderDetail", listItem),
					new XElement(ord + "orderSummary", orderSummary)
					);


				XElement root = new XElement(dat + "dataPack",
					new XAttribute(XNamespace.Xmlns + "dat", "http://www.stormware.cz/schema/version_2/data.xsd"),
					new XAttribute(XNamespace.Xmlns + "ord", "http://www.stormware.cz/schema/version_2/order.xsd"),
					new XAttribute(XNamespace.Xmlns + "typ", "http://www.stormware.cz/schema/version_2/type.xsd"),
					new XAttribute("id", "objvcm" + davkacislo.ToString()),
					new XAttribute("ico", Globals_V1.Konfigurace.PohodaInfo[0].ICO),
					new XAttribute("application", Fask.SQL.Constants.Common.application_S),
					new XAttribute("version", "2.0"),
					new XAttribute("note", "Import objednavky"),

					new XElement(dat + "dataPackItem",
						new XAttribute("id", "objvcm" + davkacislo.ToString()),
						new XAttribute("version", "2.0"),
						ObjednavkaElement));



				root.Save(filename);
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML.Classes.Prodej2", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				return false;
			}

			return true;


			#endregion

		}

		/// <summary>
		/// \TODO Predelat na lepsi reakci
		/// </summary>
		/// <param name="filename"></param>
		/// <param name="uzivatel"></param>
		/// <returns></returns>
		internal static string LoadResponse_Objvm_XML(string filename, Fask.Server.Interfaces.Classes.User uzivatel)
		{
			XML.MST_Pohoda.CheckExistResponseFile(filename);

			XML.MST_Pohoda.UpdateCreatorObj(uzivatel, filename);
			return "OK";
		}

		#endregion

		#endregion

		#region Prevod pomocne metodz


		private static void Logika_Prodej_Sledovani_ImportPOHODA(
	XNamespace pre,
	Fask.DataSets.ProdejData.CZMST_DIRow row,
	List<XElement> PrevodkaItem,
	XElement sern,
	XElement itemnmbr,
	int? RelSKzVC,
	bool stavRelSKzVC,
	int? RefVPrFXTS,
	bool stavVPrFPTS,
	int? RefVPrFPTS)
		{
			//int? RelSKzVC = skz_row.IsRelSKzVCNull() ? (int?)null : (int?)skz_row.RelSKzVC;

			if (RelSKzVC == null || RelSKzVC.Value == 0)
			{
				if (stavRelSKzVC)
				{


					if (!RefVPrFXTS.HasValue || RefVPrFXTS.Value == 0)
					{
						PrevodkaItem.Add(new XElement(pre + "stockItem", itemnmbr));
					}
					else if (RefVPrFXTS.Value == 1 || RefVPrFXTS.Value == 2)
					{
						Prijem_SetParams_serltnum_Poznamka_itemnmbr(pre, row, PrevodkaItem, itemnmbr);
					}

				}
				else
				{
					if (stavVPrFPTS)
					{

						if (!RefVPrFPTS.HasValue || RefVPrFPTS.Value == 0)
						{
							PrevodkaItem.Add(new XElement(pre + "stockItem", itemnmbr));
						}
						else if (RefVPrFPTS.Value == 1 || RefVPrFPTS.Value == 2)
						{
							Prijem_SetParams_serltnum_Poznamka_itemnmbr(pre, row, PrevodkaItem, itemnmbr);
						}

					}
					else
					{
						PrevodkaItem.Add(new XElement(pre + "stockItem", itemnmbr));
					}
				}
			}
			else if (RelSKzVC.Value == 1 || RelSKzVC.Value == 2)
			{
				Prijem_SetParams_serltnum_itemnmbr(pre, row, PrevodkaItem, sern, itemnmbr);
			}
		}

		private static void Prijem_SetParams_serltnum_Poznamka_itemnmbr(XNamespace pre, Fask.DataSets.ProdejData.CZMST_DIRow row, List<XElement> PrevodkaItem, XElement itemnmbr)
		{
			PrevodkaItem.Add(new XElement(pre + "stockItem", itemnmbr));

			if (row.SERLTNUM.Trim().Length > 0)
			{
				PrevodkaItem.Add(new XElement(pre + "note", row.SERLTNUM.Trim()));
			}
		}

		private static void Prijem_SetParams_serltnum_itemnmbr(XNamespace pre, Fask.DataSets.ProdejData.CZMST_DIRow row, List<XElement> PrevodkaItem, XElement sern, XElement itemnmbr)
		{
			if (row.SERLTNUM.Trim().Length > 0)
			{
				PrevodkaItem.Add(new XElement(pre + "stockItem", itemnmbr, sern));
			}
			else
			{
				PrevodkaItem.Add(new XElement(pre + "stockItem", itemnmbr));
			}
		}

		#endregion

	}
}
