using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml;
using System.IO;
using System.Data.SqlClient;

namespace Fask.SQL
{
	public static class Prijem
	{
		// 16.6.2016 PeV: nesmi byt static!! Jinak se pamatuji ostatni data a pridavaji se do dalsi davky
		//private static Datasets.Prijem PrijemDS = new Datasets.Prijem();


		private static void NastavPromenne(Fask.SQL.Datasets.Prijem.CZMST_PERow peRow)
		{
			peRow.VNDITNUM = string.Empty;
			peRow.VNDDOCNM = string.Empty;
			peRow.ORD = 0;
			peRow.CZ_DatVyr_Delka = 0;
			peRow.CZ_DatVyr_Track = 0;
			peRow.CZ_SerNum_Delka = 0;
			peRow.CZ_SerNum_Track = 0;
			peRow.CZ_SW_Delka = 0;
			peRow.CZ_SW_Track = 0;
			peRow.CZ_Doslo = 0;
			peRow.QTYPACK = 0;
			peRow.QTYSHPPD = 0;
			peRow.ITEMNMBR = string.Empty;
			peRow.LOCNCODE = string.Empty;
			peRow.ITEMDESC = string.Empty;
			peRow.CZ_CarKod = string.Empty;
			peRow.SKL_ID = string.Empty;

			peRow.SERLTNUM = string.Empty;
			peRow.SetWEIGHTNull();
			peRow.NMBRPAL = string.Empty;
			peRow.TYPEPAL = string.Empty;
			peRow.ITEMCODE = string.Empty;
			peRow.CZ_REZ1_Track = 0;
			peRow.CZ_REZ2_Track = 0;
			peRow.SetRealization_StartNull();
			peRow.SetRealization_StopNull();
			peRow.CZ_Expirace_Track = 0;


	}

		/// <summary>
		/// Nacte data z vydanych objednavek 
		/// </summary>
		/// <param name="filename">cesta ke xml souboru</param>
		public static string LoadResponse_ObjednavkaVydana_XML(string filename, bool save, Fask.Server.Interfaces.Classes.Objednavka objednavka)
		{
			XML.MST_Pohoda.CheckExistResponseFile(filename);

			string actualStrName = string.Empty;
			string previousStrName = string.Empty;
			int actualCountEntries = 0;
			bool InVydejkItem = false;
			int indexPriID = 0;
			int indexTypID = 0;
			int indexTypIDS = 0;
			decimal quantity = 0;
			decimal delivered = 0;
			string mj = string.Empty;
			string actualSonnumber = string.Empty;
			bool isExecuted = false;
			bool check = false;
			bool isDelivered = false;
			bool incCountEntr = true;

			// dotazeni informace o polozce z pohoda
			//Datasets.DatabasePohodaTableAdapters.SKzTableAdapter SKzTableAdapter = new Datasets.DatabasePohodaTableAdapters.SKzTableAdapter();
			//SKzTableAdapter.Connection.ConnectionString = Globals.ConnectionStringPohodaDB;

			// dotazeni parametru pro locncode (vychozi lokaci ..)
			//Datasets.DatabasePohodaTableAdapters.SKzParametryTableAdapter SKzParametryTableAdapter = new Datasets.DatabasePohodaTableAdapters.SKzParametryTableAdapter();
			//SKzParametryTableAdapter.Connection.ConnectionString = Globals.ConnectionStringPohodaDB;

			Datasets.Prijem PrijemDS = new Datasets.Prijem();

			Datasets.PrijemTableAdapters.CZMST_PETableAdapter CZMST_PETableAdapter = new Datasets.PrijemTableAdapters.CZMST_PETableAdapter();
			CZMST_PETableAdapter.Connection.ConnectionString = Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB;


			Datasets.Prijem.CZMST_PERow peRow = null;

			Datasets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter ParamTA = null;
			if (!Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1)
			{
				ParamTA = new Datasets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter();
				ParamTA.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
			}

			XmlTextReader reader = new XmlTextReader(filename);

			int PONUMBER_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Prijem.ColumnsInfo_CZMST_PE["PONUMBER"].MaxLength;
			int SKL_ID_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Prijem.ColumnsInfo_CZMST_PE["SKL_ID"].MaxLength;
			int VNDITNUM_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Prijem.ColumnsInfo_CZMST_PE["VNDITNUM"].MaxLength;
			int CZ_CarKod_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Prijem.ColumnsInfo_CZMST_PE["CZ_CarKod"].MaxLength;
			int ITEMDESC_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Prijem.ColumnsInfo_CZMST_PE["ITEMDESC"].MaxLength;
			int ITEMCODE_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Prijem.ColumnsInfo_CZMST_PE["ITEMCODE"].MaxLength;

			try
			{
				peRow = PrijemDS.CZMST_PE.NewCZMST_PERow();

				NastavPromenne(peRow);

				actualCountEntries = Database.Prijem.CZMSTPE_MAX_CountEntries();
				//objednavka.CisloDavky = actualCountEntries;

				while (reader.Read())
				{
					switch (reader.NodeType)
					{
						case XmlNodeType.Element:

							previousStrName = actualStrName;
							actualStrName = reader.Name;

							if (actualStrName == "ord:orderItem")
							{
								InVydejkItem = true;
								if (peRow.ITEMDESC != string.Empty)
								{ //jsme u dalsiho zbozi na prijemce...ulozime aktulani zbozi 

									peRow.CountEntries = actualCountEntries;
									peRow.PONUMBER = actualSonnumber;

									if (peRow.PONUMBER.Length > PONUMBER_MaxLength)
									{
										peRow.PONUMBER = peRow.PONUMBER.Remove(PONUMBER_MaxLength);
									}
									

									if (Database.Prijem.CZMSTPE_PONUMBER_EXIST(peRow.PONUMBER) && !check)
									{ // toto cislo davky se uz v db vyskytuje -> smazeme jej
										check = true;
										bool test = Database.Prijem.CZMSTPE_UPDATE_CZDOSLO(peRow.PONUMBER);
										if (test)
											Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info, "prijem - proveden update cz_doslo na 201 u objednavky=" + peRow.PONUMBER);
										else
											Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info, "prijem - nebyl proveden update cz_doslo na 201 u objednavky=" + peRow.PONUMBER);
									}
									else
										check = true;

									//TODO: revidovat, přepsat
									//if (!Database.Prijem.CZMSTPE_EXIST_PONNUMBER(peRow.PONUMBER, peRow.ITEMNMBR, peRow.ORD))
									//{
									if (save)
									{
										SaveData_toDB(ref quantity, delivered, ref incCountEntr, CZMST_PETableAdapter, peRow);
									}
									else if (Globals_V1.Konfigurace.Prijem[0].StatusObjednavky)
									{
										if ((Globals_V1.Konfigurace.Prijem[0].Executed == isExecuted) && (Globals_V1.Konfigurace.Prijem[0].Delivered == isDelivered))
										{
											SaveData_toDB(ref quantity, delivered, ref incCountEntr, CZMST_PETableAdapter, peRow);
										}
									}
									else
									{
										SaveData_toDB(ref quantity, delivered, ref incCountEntr, CZMST_PETableAdapter, peRow);
									}
									/*}
									else
									{
										incCountEntr = false;
										//pokud uz objednavka je v tabulce, zjistime jeji countentries
										int pom = Database.Prijem.CZMSTPE_CountEntries_SOPNUMBE(peRow.PONUMBER);

										if (pom >= 0)
											actualCountEntries = pom; 

									}*/

									peRow = PrijemDS.CZMST_PE.NewCZMST_PERow();

									NastavPromenne(peRow);
									InVydejkItem = true;
									indexTypID = 0;
									indexTypIDS = 0;
								}

							}
							else if (actualStrName == "ord:orderSummary")
							{
								if (peRow.ITEMDESC != string.Empty)
								{ //jsme u dalsiho zbozi na prijemce...ulozime aktulani zbozi 

									peRow.CountEntries = actualCountEntries;
									peRow.PONUMBER = actualSonnumber;

									if (peRow.PONUMBER.Length > PONUMBER_MaxLength)
									{
										peRow.PONUMBER = peRow.PONUMBER.Remove(PONUMBER_MaxLength);
									}

									if (Database.Prijem.CZMSTPE_PONUMBER_EXIST(peRow.PONUMBER) && !check)
									{ // toto cislo davky se uz v db vyskytuje -> smazeme jej
										check = true;
										bool test = Database.Prijem.CZMSTPE_UPDATE_CZDOSLO(peRow.PONUMBER);
										if (test)
											Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info, "prijem - proveden update cz_doslo na 201 u objednavky=" + peRow.PONUMBER);
										else
											Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info, "prijem - nebyl proveden update cz_doslo na 201 u objednavky=" + peRow.PONUMBER);
									}
									else
										check = true;

									//if (!Database.Prijem.CZMSTPE_EXIST_PONNUMBER(peRow.PONUMBER, peRow.ITEMNMBR, peRow.ORD))
									//{
									if (save)
									{
										SaveData_toDB(ref quantity, delivered, ref incCountEntr, CZMST_PETableAdapter, peRow);

									}
									else if (Globals_V1.Konfigurace.Prijem[0].StatusObjednavky)
									{
										if ((Globals_V1.Konfigurace.Prijem[0].Executed == isExecuted) && (Globals_V1.Konfigurace.Prijem[0].Delivered == isDelivered))
										{
											SaveData_toDB(ref quantity, delivered, ref incCountEntr, CZMST_PETableAdapter, peRow);
										}
									}
									else
									{
										SaveData_toDB(ref quantity, delivered, ref incCountEntr, CZMST_PETableAdapter, peRow);
									}
									/*}
									else
									{
										incCountEntr = false;
										int pom = Database.Prijem.CZMSTPE_CountEntries_SOPNUMBE(peRow.PONUMBER);

										if (pom >= 0)
											actualCountEntries = pom; 
									}*/

									peRow = PrijemDS.CZMST_PE.NewCZMST_PERow();

									NastavPromenne(peRow);
									InVydejkItem = false;
									indexTypID = 0;
									indexTypIDS = 0;
								}
							}
							else if (actualStrName == "lst:order" && peRow.ITEMDESC == string.Empty)
							{
								indexTypIDS = indexTypID = indexPriID = 0;

								if (incCountEntr)
								{
									actualCountEntries++;
									objednavka.CisloDavky = actualCountEntries.ToString();
								}

								isExecuted = false;
								isDelivered = false;
							}
							break;

						case XmlNodeType.Text:

							if (actualStrName == "typ:id")
							{
								if (InVydejkItem)
								{
									if (indexTypID == 0)
									{
										peRow.SKL_ID = reader.Value;
										if (peRow.SKL_ID.Length > SKL_ID_MaxLength)
										{
											peRow.SKL_ID = peRow.SKL_ID.Remove(SKL_ID_MaxLength);
										}

										//peRow.LOCNCODE = reader.Value;
										//if (peRow.LOCNCODE.Length > PrijemDS.CZMST_PE.LOCNCODEColumn.MaxLength)
										//{
										//    peRow.LOCNCODE = peRow.LOCNCODE.Remove(PrijemDS.CZMST_PE.LOCNCODEColumn.MaxLength);
										//} 
									}

									if (indexTypID == 1)
									{
										peRow.ITEMNMBR = reader.Value;

										// TOTO se nesmi delat, jinak muze dojit k problemum s identifikaci...
										// pokud presahne delku, tak je problem !!!
										//if (peRow.ITEMNMBR.Length > PrijemDS.CZMST_PE.ITEMNMBRColumn.MaxLength)
										//{
										//    peRow.ITEMNMBR = peRow.ITEMNMBR.Remove(PrijemDS.CZMST_PE.ITEMNMBRColumn.MaxLength);
										//}

										// dotazeni vychozi lokace pro zasobu
										string locncodedefault = string.Empty;
										if (!String.IsNullOrEmpty(Globals_V1.Konfigurace.Sdilene[0].LokaceVychoziParametrNazevPohoda))
										{
											//Datasets.DatabasePohoda.SKzParametryDataTable skzParametry_table = SKzParametryTableAdapter.GetDataByParamNameSKzID(Properties.Settings.Default.LokaceVychoziParametrNazevPohoda, int.Parse(peRow.ITEMNMBR));
											Datasets.DatabasePohoda.SKzParametryDataTable skzParametry_table = Database.Pohoda.SKzParametry_GetDataByParamNameSKzID(Globals_V1.Konfigurace.Sdilene[0].LokaceVychoziParametrNazevPohoda, int.Parse(peRow.ITEMNMBR));

											Datasets.DatabasePohoda.SKzParametryRow skzParametry_row = skzParametry_table.Count > 0 ? skzParametry_table[0] : null;
											if (skzParametry_row != null)
											{
												locncodedefault = skzParametry_row.IspValTextNull() ? string.Empty : skzParametry_row.pValText.Trim();
											}
										}
										peRow.LOCNCODE = locncodedefault;

										// 13.6.2016 PeV: presunuto do metody SaveData, az jsou znamy vsechna data
										//LocncodeFindAlgorithmVychozi(peRow);


										var skzdt = Database.Pohoda.SKz_GetDataByID(int.Parse(peRow.ITEMNMBR));

										if ((skzdt != null) && (skzdt.Count > 0))
										{
											//19.9.2018 TaD nova logika SerNumTrack
											var skz_row = skzdt[0];

											if (Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1)
											{
												int? tmp_RelSKzVC = skz_row.IsRelSKzVCNull() ? (int?)null : (int?)skz_row.RelSKzVC;

												peRow.CZ_SerNum_Track = Classes.Pohoda.GetPriznakSledovani(tmp_RelSKzVC, skz_row, Classes.Pohoda.TypAgendy.Prijem);



												if (tmp_RelSKzVC.HasValue)
												{
													if(tmp_RelSKzVC.Value == 2 && peRow.CZ_SerNum_Track == 2)
                                                    {
														bool? expTrack =  skz_row.IsVPrCZExpTrackISNull() ? (bool?)null : skz_row.VPrCZExpTrackIS;

														if(expTrack.HasValue && expTrack.Value)
                                                        {
															peRow.CZ_Expirace_Track = 1;
														}
													}

													if (tmp_RelSKzVC.Value == 1 && peRow.CZ_SerNum_Track == 1)
													{
														bool? expTrack = skz_row.IsVPrCZExpTrackISNull() ? (bool?)null : skz_row.VPrCZExpTrackIS;

														if (expTrack.HasValue && expTrack.Value)
														{
															peRow.CZ_Expirace_Track = 1;
														}
                                                        else
                                                        {
															peRow.CZ_Expirace_Track = 0;
														}

														bool? SerTrack = skz_row.IsVPrCZSerNumTrISNull() ? (bool?)null : skz_row.VPrCZSerNumTrIS;

														if (SerTrack.HasValue && SerTrack.Value)
														{
															peRow.CZ_SerNum_Track = 10;
														}

														bool? SerTrackIGN = skz_row.IsVPrCZSNumTrIGNNull() ? (bool?)null : skz_row.VPrCZSNumTrIGN;

														if (SerTrackIGN.HasValue && SerTrackIGN.Value)
														{
															peRow.CZ_SerNum_Track = 11;
														}
													}
												}
											}
											else
											{
												Datasets.Zbozi.FASK_ZASOBY_PARAMETRYRow dt_row_param = null;
												var dt_param = ParamTA.GetDataByITEMNMBR(peRow.ITEMNMBR.Trim());

												if ((dt_param != null) && (dt_param.Count > 0))
												{
													dt_row_param = dt_param.First();
												}

												peRow.CZ_SerNum_Track = Classes.Pohoda.GetPriznakSledovani(skz_row.IsRelSKzVCNull() ? (int?)null : (int?)skz_row.RelSKzVC, dt_row_param, Classes.Pohoda.TypAgendy.Prijem);
											}

										}
										else
										{
											Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Warn, "Nebylo nalezeno zbozi v SKz, hodnota CZ_SerNum_Track byla nastavena na 0");
											peRow.CZ_SerNum_Track = 0;
										}
										

									}

									indexTypID++;
								}
							}
							if (actualStrName == "ord:id")
							{
								if (InVydejkItem)
									peRow.ORD = Convert.ToInt32(reader.Value);

								indexPriID++;
							}
							else if (actualStrName == "typ:numberRequested")
							{
								check = false;
								actualSonnumber = reader.Value;
							}
							else if (actualStrName == "typ:EAN" /*|| actualStrName == "typ:PLU"*/)
							{
								if (peRow.VNDITNUM == string.Empty)
								{
									peRow.VNDITNUM = reader.Value;

									if (peRow.VNDITNUM.Length > VNDITNUM_MaxLength)
									{
										peRow.VNDITNUM = peRow.VNDITNUM.Remove(VNDITNUM_MaxLength);
									}
								}
								/*
								else
								{
									peRow.CZ_CarKod = reader.Value;

									if (peRow.CZ_CarKod.Length > PrijemDS.CZMST_PE.CZ_CarKodColumn.MaxLength)
									{
										peRow.CZ_CarKod = peRow.CZ_CarKod.Remove(PrijemDS.CZMST_PE.CZ_CarKodColumn.MaxLength);
									}
								}*/
							}
							else if (actualStrName == "typ:ids")
							{
								if (InVydejkItem)
								{
									if (indexTypIDS == 1)
									{
										peRow.CZ_CarKod = reader.Value;

										if (peRow.CZ_CarKod.Length > CZ_CarKod_MaxLength)
										{
											peRow.CZ_CarKod = peRow.CZ_CarKod.Remove(CZ_CarKod_MaxLength);
										}
									}
									indexTypIDS++;
								}
							}
							else if (actualStrName == "ord:isExecuted")
								isExecuted = Convert.ToBoolean(reader.Value);
							else if (actualStrName == "ord:isDelivered")
								isDelivered = Convert.ToBoolean(reader.Value);
							else if (actualStrName == "ord:text")
							{
								if (InVydejkItem)
									peRow.ITEMDESC = reader.Value;

								if (peRow.ITEMDESC.Length > ITEMDESC_MaxLength)
								{
									peRow.ITEMDESC = peRow.ITEMDESC.Remove(ITEMDESC_MaxLength);
								}

							}
							else if (actualStrName == "ord:code")
							{
								if (InVydejkItem)
									peRow.ITEMCODE = reader.Value;

								if (peRow.ITEMCODE.Length > ITEMCODE_MaxLength)
								{
									peRow.ITEMCODE = peRow.ITEMCODE.Remove(ITEMCODE_MaxLength);
								}

							}
							else if (actualStrName == "typ:PLU")
							{
								//peRow.ORD = Convert.ToInt32(reader.Value);
								//peRow.VNDDOCNM = reader.Value;
							}
							else if (actualStrName == "ord:unit")
								mj = reader.Value.Trim();
							else if (actualStrName == "ord:coefficient")
								peRow.QTYPACK = decimal.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo); //System.Globalization.CultureInfo.InvariantCulture);
							else if (actualStrName == "ord:quantity")
								quantity = decimal.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
							else if (actualStrName == "ord:delivered")
								delivered = decimal.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);

							break;

						default:
							break;
					}
				}

				//dotazeni alternativnich kodu k polozkam objednavky ... 
				// predpoklad, ze je pouze jedna objednavka/prijemka vygenerovana ...
				//Datasets.DatabasePohodaTableAdapters.SKzNCTableAdapter skznc_ta = new Datasets.DatabasePohodaTableAdapters.SKzNCTableAdapter();
				//skznc_ta.Connection.ConnectionString = Globals.ConnectionStringPohodaDB;

				Datasets.Prijem prijem_ds = new Datasets.Prijem();
				Datasets.Prijem prijem_ds_added = new Datasets.Prijem();
				CZMST_PETableAdapter.FillByCountEntries(prijem_ds.CZMST_PE, int.Parse(objednavka.CisloDavky));
				// \TODO : kolekce byla zmenena => musim udelat do noveho a ten pak updatnout ... 
				foreach (Datasets.Prijem.CZMST_PERow perow in prijem_ds.CZMST_PE)
				{
					//Datasets.DatabasePohoda.SKzNCDataTable skznc_dt = skznc_ta.GetDataSKzID(int.Parse(perow.ITEMNMBR));
					Datasets.DatabasePohoda.SKzNCDataTable skznc_dt = Database.Pohoda.SKzNC_GetDataSKzID(int.Parse(perow.ITEMNMBR));

					foreach (Datasets.DatabasePohoda.SKzNCRow skzncrow in skznc_dt)
					{
						if (!skzncrow.IsEANNull())
						{
							var Row = prijem_ds_added.CZMST_PE.NewCZMST_PERow();

							Row.CountEntries = perow.CountEntries;
							Row.PONUMBER = perow.PONUMBER;
							Row.ITEMNMBR = perow.ITEMNMBR;
							Row.ITEMDESC = perow.ITEMDESC;
							Row.ORD = perow.ORD;
							Row.VNDDOCNM = perow.VNDDOCNM;
							Row.VNDITNUM = skzncrow.IsEANNull() ? "" : skzncrow.EAN;
							Row.CZ_CarKod = perow.CZ_CarKod;
							Row.LOCNCODE = perow.LOCNCODE;
							Row.QTYSHPPD = perow.QTYSHPPD;
							Row.QTYPACK = perow.QTYPACK;
							Row.CZ_DatVyr_Track = perow.CZ_DatVyr_Track;
							Row.CZ_DatVyr_Delka = perow.CZ_DatVyr_Delka;
							Row.CZ_SerNum_Track = perow.CZ_SerNum_Track;
							Row.CZ_SerNum_Delka = perow.CZ_SerNum_Delka;
							Row.CZ_SW_Track = perow.CZ_SW_Track;
							Row.CZ_SW_Delka = perow.CZ_SW_Delka;
							Row.CZ_Doslo = perow.CZ_Doslo;
							Row.MJ = skzncrow.IsMJEANNull() ? "" : skzncrow.MJEAN;
							Row.SKL_ID = perow.IsSKL_IDNull() ? string.Empty : perow.SKL_ID;
							Row.SERLTNUM = perow.SERLTNUM;

							if (perow.IsWEIGHTNull())
								Row.SetWEIGHTNull();
							else
								Row.WEIGHT = perow.WEIGHT;

							Row.NMBRPAL = perow.NMBRPAL;
							Row.TYPEPAL = perow.TYPEPAL;
							Row.ITEMCODE = perow.ITEMCODE;

							Row.CZ_REZ1_Track = perow.CZ_REZ1_Track;
							Row.CZ_REZ2_Track = perow.CZ_REZ2_Track;

							Row.SetRealization_StartNull();
							Row.SetRealization_StopNull();

							Row.CZ_Expirace_Track = perow.CZ_Expirace_Track;

							prijem_ds_added.CZMST_PE.AddCZMST_PERow(Row);


							//	prijem_ds_added.CZMST_PE.AddCZMST_PERow(
							//perow.CountEntries,
							//perow.PONUMBER,
							//perow.ITEMNMBR,
							//perow.ITEMDESC,
							//perow.ORD,
							//perow.VNDDOCNM,
							//skzncrow.IsEANNull() ? "" : skzncrow.EAN,
							//perow.CZ_CarKod,
							//perow.LOCNCODE,
							//perow.QTYSHPPD,
							//perow.QTYPACK,
							//perow.CZ_DatVyr_Track,
							//perow.CZ_DatVyr_Delka,
							//perow.CZ_SerNum_Track,
							//perow.CZ_SerNum_Delka,
							//perow.CZ_SW_Track,
							//perow.CZ_SW_Delka,
							//perow.CZ_Doslo,
							//skzncrow.IsMJEANNull() ? "" : skzncrow.MJEAN,
							//perow.IsSKL_IDNull() ? string.Empty : perow.SKL_ID,
							//perow.SERLTNUM
							//);
						}
					}
				}

				CZMST_PETableAdapter.Update(prijem_ds_added.CZMST_PE);

				return "OK";
			}
			catch (XmlException e)
			{
				Fask.Logging.ExceptionHandler2.Handle(e);
				return e.Message;
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				return ex.Message;
			}
			finally
			{
				if (reader != null)
					reader.Close();
			}
		}

		private static void LocncodeFindAlgorithmVychozi(Datasets.Prijem.CZMST_PERow peRow)
		{
			// \TODO : dotazeni vychozi lokace pro polozku skladu ...
			// pro ruzne MJ se muze lisit??? => mozna do budoucna ...
			Provider provider = new Provider();
			Fask.Server.Interfaces.DataSets.Location locationDS = provider.Lokace_VariantySortimentGet(peRow.ITEMNMBR.Trim(), peRow.IsSKL_IDNull() ? string.Empty : peRow.SKL_ID.Trim());
			var loctypedefaultrows = locationDS.CZMST_SkladLokace_LokaceTypy.Where(x => x.IS_DEFAULT);
			if (loctypedefaultrows.Count() > 0)
			{
				var loctypedefault = loctypedefaultrows.First();
				var loctypedefaultforitemrows = locationDS.CZMST_SkladLokace_LokaceVariantySortiment.Where(x => x.TYPE == loctypedefault.TYPE);
				if (loctypedefaultforitemrows.Count() > 0)
				{
					var loctypedefaultforitem = loctypedefaultforitemrows.First();
					peRow.LOCNCODE = loctypedefaultforitem.LOCNCODE.Trim();
				}
			}
		}

		//private class PrevodkaData
		//{
		//    public string skl_id { get; set; }       // sSklad_ID = 0;   // id skladu
		//    public string itemnmbr { get; set; }     // SKz_ID = 0;      // id zbozi
		//    public decimal qtyshppd { get; set; }
		//    public string vnditnum { get; set; }
		//}

		//private class Prevodka
		//{
		//    public string ponumber = string.Empty;      // string SKMP_Cislo = string.Empty;       // PONUMBER
		//    public List<PrevodkaData> prevodkaData { get; set; }

		//    public Prevodka()
		//    {
		//        prevodkaData = new List<PrevodkaData>();
		//    }
		//}

		/// <summary>
		/// Nacte data z odpovedi prevodky
		/// </summary>
		/// <param name="filename">cesta ke xml souboru</param>
		public static string LoadResponse_Prevodka_XML(string filename, bool save, Fask.Server.Interfaces.Classes.Objednavka objednavka)
		{
			XML.MST_Pohoda.CheckExistResponseFile(filename);

			Datasets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter ParamTA = null;
			if (!Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1)
			{
				ParamTA = new Datasets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter();
				ParamTA.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
			}

			int CZ_CarKod_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Prijem.ColumnsInfo_CZMST_PE["CZ_CarKod"].MaxLength;
			int ITEMDESC_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Prijem.ColumnsInfo_CZMST_PE["ITEMDESC"].MaxLength;
			int MJ_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Prijem.ColumnsInfo_CZMST_PE["MJ"].MaxLength;


			//Prevodka pData = new Prevodka();

			// adapter pro dotahnuti informaci o polozce z pohody
			//Datasets.DatabasePohodaTableAdapters.SKzTableAdapter SKzTableAdapter = new Datasets.DatabasePohodaTableAdapters.SKzTableAdapter();
			//SKzTableAdapter.Connection.ConnectionString = Globals.ConnectionStringPohodaDB;

			// adapter pro dotahnuti informace o vychozi lokaci
			//Datasets.DatabasePohodaTableAdapters.SKzParametryTableAdapter SKzParametryTableAdapter = new Datasets.DatabasePohodaTableAdapters.SKzParametryTableAdapter();
			//SKzParametryTableAdapter.Connection.ConnectionString = Globals.ConnectionStringPohodaDB;

			//Datasets.DatabasePohodaTableAdapters.SKMPPolVazbaTableAdapter SKMPPolVazbaTableAdapter = new Datasets.DatabasePohodaTableAdapters.SKMPPolVazbaTableAdapter();
			//SKMPPolVazbaTableAdapter.Connection.ConnectionString = Globals.ConnectionStringPohodaDB;

			// czmst_pe adapter
			Datasets.PrijemTableAdapters.CZMST_PETableAdapter CZMST_PETableAdapter = new Datasets.PrijemTableAdapters.CZMST_PETableAdapter();
			CZMST_PETableAdapter.Connection.ConnectionString = Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB;

			//Pouzite namespacy
			XNamespace dat = "http://www.stormware.cz/schema/version_2/data.xsd";
			XNamespace rsp = "http://www.stormware.cz/schema/version_2/response.xsd";
			XNamespace lst = "http://www.stormware.cz/schema/version_2/list.xsd";
			XNamespace ftr = "http://www.stormware.cz/schema/version_2/filter.xsd";
			XNamespace typ = "http://www.stormware.cz/schema/version_2/type.xsd";
			XNamespace pre = "http://www.stormware.cz/schema/version_2/prevodka.xsd";

			// nacteni hodnot z odpovedi ...
			XDocument root = XDocument.Load(filename);

			XElement responsePack = root.Element(rsp + "responsePack");
			XAttribute state = responsePack.Attribute("state");
			if (state.Value != "ok")
			{
				XAttribute note = responsePack.Attribute("note");
				throw new Exception("Nepodařilo se získat data převodky.\nStatus:" + state.Value + "\nChyba:" + note.Value);
			}
			XElement responsePackItem = responsePack.Element(rsp + "responsePackItem");
			XAttribute responsePackItemState = responsePackItem.Attribute("state");
			if (responsePackItemState.Value.Trim() != "ok")
			{
				XAttribute responsePackItemNote = responsePackItem.Attribute("note");
				throw new Exception("Nepodařilo se získat data převodky.\nStatus:" + responsePackItemState.Value + "\nChyba:" + responsePackItemNote.Value);
			}
			XElement prevodka =
				root.Element(rsp + "responsePack").
					Element(rsp + "responsePackItem").
						Element(lst + "listPrevodka").
							Element(lst + "prevodka");

			// \TODO: prepsat hlasku
			if (prevodka == null)
				throw new Exception("Response byla uspesne vygenerovana, ale prevodka pravdepodoobne neexistuje!");

			XElement prevodkaHeader = prevodka.Element(pre + "prevodkaHeader");
			int prevodkaID = int.Parse(prevodkaHeader.Element(pre + "id").Value);

			// nacteni headeru prevodky
			XElement numberRequested = prevodka.
				Element(pre + "prevodkaHeader").
					Element(pre + "number").
						Element(typ + "numberRequested");
			string ponumber = numberRequested.Value;

			// nacteni samotnych dat
			var prevodkaItems = prevodka.
				Element(pre + "prevodkaDetail").
					Elements(pre + "prevodkaItem");

			// \TODO: Poresit, pustit, nepustit ...
			if (Database.Prijem.CZMSTPE_PONUMBER_EXIST(ponumber))
			{
				// toto cislo davky se uz v db vyskytuje -> smazeme jej
				bool test = Database.Prijem.CZMSTPE_UPDATE_CZDOSLO(ponumber);
				if (test)
					Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info, "prijem - proveden update cz_doslo na 201 u prevodky=" + ponumber);
				else
				{
					Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info, "prijem - nebyl proveden update cz_doslo na 201 u prevodky=" + ponumber);
				}
			}

			// 28.4.2016 JiS - cislo davky se musi generovat v transakci ...
			//// nacteni cisla davky a zvetseni o 1
			//int countEntries = Database.Prijem.CZMSTPE_MAX_CountEntries();
			//countEntries += 1;
			int countEntries = 0; //Bude pred insertem u zaznamu zmeneno ...

			Datasets.Prijem PrijemDS = new Datasets.Prijem();
			//Datasets.Prijem.CZMST_PEDataTable peTable = new Datasets.Prijem.CZMST_PEDataTable();
			Datasets.Prijem.CZMST_PERow peRow = null;

			int ord = 0;
			// nacteni vsech polozek
			foreach (var prevodkaItem in prevodkaItems)
			{
				// Pokud to neni skladova polozka, tak ji neresit ...
				XElement stockItem = prevodkaItem.Element(pre + "stockItem");
				if (stockItem == null)
					continue;

				peRow = PrijemDS.CZMST_PE.NewCZMST_PERow();
				NastavPromenne(peRow);

				// nastaveni promennych
				NastavPromenne(peRow);
				peRow.CountEntries = countEntries;  // countentries
				peRow.PONUMBER = ponumber;  // ponumber                
				//peRow.ITEMNMBR = prevodkaItem.
				//Element(pre + "stockItem").
				//    Element(typ + "stockItem").
				//        Element(typ + "id").Value;  // itemnmbr

				// Musi se dohledat odpovidajici polozka na sklade, na ktery prijimam
				int stockItemIDZdroj = int.Parse(
					prevodkaItem.Element(pre + "stockItem").Element(typ + "stockItem").Element(typ + "id").Value
					);  // itemnmbr polozky zdrojoveho skladu
				//int stockItemIDCil = SKMPPolVazbaTableAdapter.GetData_RefAg_RefSKz(prevodkaID, stockItemIDZdroj)[0].RefSKz1;
				int stockItemIDCil = Database.Pohoda.SKMPPolVazba_GetData_RefAg_RefSKz(prevodkaID, stockItemIDZdroj)[0].RefSKz1;
				peRow.ITEMNMBR = stockItemIDCil.ToString();

				peRow.ORD = --ord;

				//Datasets.DatabasePohoda.SKzDataTable SKzrows = SKzTableAdapter.GetDataByID(Convert.ToInt32(peRow.ITEMNMBR));
				Datasets.DatabasePohoda.SKzDataTable SKzrows = Database.Pohoda.SKz_GetDataByID(Convert.ToInt32(peRow.ITEMNMBR));
				// polozka nalezena, doplnit data ...
				if (SKzrows.Count > 0)
				{
					// nacteni prvniho zaznamu
					Datasets.DatabasePohoda.SKzRow SKzrow = SKzrows.First();

					// dotazeni vychozi lokace pro zasobu
					string locncodeDeafult = string.Empty;
					if (SKzrow != null)
					{
						if (!String.IsNullOrEmpty(Globals_V1.Konfigurace.Sdilene[0].LokaceVychoziParametrNazevPohoda))
						{
							//Datasets.DatabasePohoda.SKzParametryDataTable skzParametry_table = SKzParametryTableAdapter.GetDataByParamNameSKzID(Properties.Settings.Default.LokaceVychoziParametrNazevPohoda, SKzrow.ID);
							Datasets.DatabasePohoda.SKzParametryDataTable skzParametry_table = Database.Pohoda.SKzParametry_GetDataByParamNameSKzID(Globals_V1.Konfigurace.Sdilene[0].LokaceVychoziParametrNazevPohoda, SKzrow.ID);

							Datasets.DatabasePohoda.SKzParametryRow skzParametry_row = skzParametry_table.Count > 0 ? skzParametry_table[0] : null;
							if (skzParametry_row != null)
							{
								locncodeDeafult = skzParametry_row.IspValTextNull() ? string.Empty : skzParametry_row.pValText.Trim();
							}
						}
					}

					peRow.LOCNCODE = locncodeDeafult;
					// qtyshppd
					peRow.QTYSHPPD = Convert.ToDecimal(prevodkaItem.Element(pre + "quantity").Value, System.Globalization.NumberFormatInfo.InvariantInfo);

					// nastaveni CZ_SerNumTrack
					// Skz.RelSKzVC" -> CZ_SerNumTrack ("2" Sarze a "1" vyrobni cislo v Pohode)
					//if (SKzrow.IsRelSKzVCNull())
					//    peRow.CZ_SW_Track = 0; // TODO : pravit
					//else
					//    peRow.CZ_SW_Track = (byte)SKzrow.RelSKzVC;


					//peRow.CZ_SerNum_Track = Classes.Pohoda.GetSerNumTrack(SKzrow.IsRelSKzVCNull() ? (int?)null : (int?)SKzrow.RelSKzVC);


					//19.9.2018 TaD nova logika SerNumTrack
					//var skz_row = skzdt[0];

					//if (Properties.Settings.Default.pohodaXfaskPouzitVPrFXTS)
					//{
					//    if (SKzrow.IsVPrFXTSNull())
					//    {
					//        peRow.CZ_SerNum_Track = 0;
					//    }
					//    else
					//    {
					//        if (SKzrow.VPrFXTS)
					//        {
					//            peRow.CZ_SerNum_Track = Classes.Pohoda.GetSerNumTrack((SKzrow == null) || (SKzrow.IsRefVPrFXTSNull()) ? (int?)null : ((int?)SKzrow.RefVPrFXTS - 1), SKzrow.VPrFXTS);
					//        }
					//        else
					//        {
					//            peRow.CZ_SerNum_Track = Classes.Pohoda.GetSerNumTrack((SKzrow == null) || (SKzrow.IsRefVPrFPTSNull()) ? (int?)null : ((int?)SKzrow.RefVPrFPTS - 1), SKzrow.VPrFPTS);
					//        }
					//    }
					//}
					//else
					//{
					//    peRow.CZ_SerNum_Track = Classes.Pohoda.GetSerNumTrack((SKzrow == null) || (SKzrow.IsRelSKzVCNull()) ? (int?)null : (int?)SKzrow.RelSKzVC, true);
					//}

					if (Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1)
					{
						peRow.CZ_SerNum_Track = Classes.Pohoda.GetPriznakSledovani(SKzrow.IsRelSKzVCNull() ? (int?)null : (int?)SKzrow.RelSKzVC, SKzrow, Classes.Pohoda.TypAgendy.Prijem);
					}
					else
					{
						Datasets.Zbozi.FASK_ZASOBY_PARAMETRYRow dt_row_param = null;
						var dt_param = ParamTA.GetDataByITEMNMBR(peRow.ITEMNMBR.Trim());

						if ((dt_param != null) && (dt_param.Count > 0))
						{
							dt_row_param = dt_param.First();
						}

						peRow.CZ_SerNum_Track = Classes.Pohoda.GetPriznakSledovani(SKzrow.IsRelSKzVCNull() ? (int?)null : (int?)SKzrow.RelSKzVC, dt_row_param, Classes.Pohoda.TypAgendy.Prijem);
					}


					peRow.VNDITNUM = (string)prevodkaItem.
					Element(pre + "stockItem").
						Element(typ + "stockItem").
							Element(typ + "EAN") ?? string.Empty;

					// cz_carkod
					peRow.CZ_CarKod = SKzrow.IsIDSNull() ? string.Empty : SKzrow.IDS;
					if (peRow.CZ_CarKod.Length > CZ_CarKod_MaxLength)
					{
						peRow.CZ_CarKod = peRow.CZ_CarKod.Remove(CZ_CarKod_MaxLength);
					}

					// itemdesc
					peRow.ITEMDESC = SKzrow.Nazev;
					if (peRow.ITEMDESC.Length > ITEMDESC_MaxLength)
					{
						peRow.ITEMDESC = peRow.ITEMDESC.Remove(ITEMDESC_MaxLength);
					}

					// skl_id
					//peRow.SKL_ID = (string)prevodkaItem.
					//Element(pre + "stockItem").
					//    Element(typ + "store").
					//        Element(typ + "id") ?? string.Empty;
					peRow.SKL_ID = SKzrow.RefSklad.ToString(); // Muze byt null => co pak?

					// mj
					peRow.MJ = SKzrow.MJ;
					if (peRow.MJ.Length > MJ_MaxLength)
					{
						peRow.MJ = peRow.MJ.Remove(MJ_MaxLength);
					}

					LocncodeFindAlgorithmVychozi(peRow);

					PrijemDS.CZMST_PE.AddCZMST_PERow(peRow);

					if (Globals_V1.Konfigurace.Prijem[0].MerneJednotky_DotahovatDalsiVarianty)
					{
						// MJ2 a qtypack (pokud existuje)
						if (!SKzrow.IsMJ2Null() && !SKzrow.IsMJ2KoefNull())
						{
							// kopie zaznamu
							Datasets.Prijem.CZMST_PERow peRow2 = PrijemDS.CZMST_PE.NewCZMST_PERow();
							peRow2.ItemArray = peRow.ItemArray.Clone() as object[];
							peRow = peRow2;

							peRow.MJ = SKzrow.MJ2;
							peRow.QTYPACK = (decimal)SKzrow.MJ2Koef;
							if (peRow.MJ.Length > MJ_MaxLength)
							{
								peRow.MJ = peRow.MJ.Remove(MJ_MaxLength);
							}

							PrijemDS.CZMST_PE.Rows.Add(peRow);
						}

						// MJ2 a qtypack (pokud existuje)
						if (!SKzrow.IsMJ3Null() && !SKzrow.IsMJ3KoefNull())
						{
							// kopie zaznamu
							Datasets.Prijem.CZMST_PERow peRow2 = PrijemDS.CZMST_PE.NewCZMST_PERow();
							peRow2.ItemArray = peRow.ItemArray.Clone() as object[];
							peRow = peRow2;

							peRow.MJ = SKzrow.MJ3;
							peRow.QTYPACK = (decimal)SKzrow.MJ3Koef;
							if (peRow.MJ.Length > MJ_MaxLength)
							{
								peRow.MJ = peRow.MJ.Remove(MJ_MaxLength);
							}

							PrijemDS.CZMST_PE.Rows.Add(peRow);
						}
					}
				}
				else
					throw new Exception("LoadResponse_Prevodka_XML (Prevodka), Zaznam s ID '" + peRow.ITEMNMBR + "' nebyl nalezen v seznamu skladovych karet.");

				peRow = null;
			}



			try
			{
				// ulozeni do czmst_pe
				CZMST_PETableAdapter.Connection.Open();
				CZMST_PETableAdapter.Transaction = CZMST_PETableAdapter.Connection.BeginTransaction(System.Data.IsolationLevel.Serializable);
				countEntries = Database.Prijem.CZMSTPE_MAX_CountEntries(CZMST_PETableAdapter.Connection, CZMST_PETableAdapter.Transaction);
				countEntries += 1;
				foreach (var item in PrijemDS.CZMST_PE)
				{
					item.CountEntries = countEntries;
					// porad je pouze pridana, nikoli zmenena po zmene countentries...
					item.AcceptChanges();
					item.SetAdded();
				}
				int updatedRows = CZMST_PETableAdapter.Update(PrijemDS.CZMST_PE);
				CZMST_PETableAdapter.Transaction.Commit();
				objednavka.CisloDavky = countEntries.ToString();
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);

				try
				{
					CZMST_PETableAdapter.Transaction.Rollback();
				}
				catch { }
				throw new Exception("Uložení načtených položek se nezdařilo!");
			}
			finally
			{
				if ((CZMST_PETableAdapter.Connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
					CZMST_PETableAdapter.Connection.Close();
			}

			return "OK";
		}

		private static void SaveData_toDB(
			ref decimal quantity,
			decimal delivered,
			ref bool incCountEntr,
			Datasets.PrijemTableAdapters.CZMST_PETableAdapter CZMST_PETableAdapter,
			Datasets.Prijem.CZMST_PERow peRow)
		{
			if (Globals_V1.Konfigurace.Prijem[0].Zbyva)
				quantity -= delivered;
			// tady se uklada neco z xml exportu ... 
			// vzhledem k Mernym jednotkam, dotazeni variant ... 
			if (peRow.IsITEMNMBRNull() || string.IsNullOrEmpty(peRow.ITEMNMBR.Trim()))
				return;

			// Pred ulozenim nacte vychozi lokaci pro prijem ...
			LocncodeFindAlgorithmVychozi(peRow);

			//Datasets.DatabasePohoda.SKzDataTable dt_skz = SKzTableAdapter.GetDataByID(int.Parse(peRow.ITEMNMBR));
			Datasets.DatabasePohoda.SKzDataTable dt_skz = Database.Pohoda.SKz_GetDataByID(int.Parse(peRow.ITEMNMBR));
			if (dt_skz.Count > 0)
			{ // pro kazdou variantu MJ vlozit alternativni MJ ... 
				var item = dt_skz[0];
				//foreach (var item in dt_skz)
				{
					// pro MJ 
					CZMST_PETableAdapter.Insert(
						peRow.CountEntries,
						peRow.PONUMBER,
						peRow.ITEMNMBR,
						peRow.ITEMDESC,
						peRow.ORD,
						peRow.VNDDOCNM,
						peRow.VNDITNUM,
						peRow.CZ_CarKod,
						peRow.LOCNCODE,
						quantity,
						0, //peRow.QTYPACK,
						peRow.CZ_DatVyr_Track,
						peRow.CZ_DatVyr_Delka,
						peRow.CZ_SerNum_Track,
						peRow.CZ_SerNum_Delka,
						peRow.CZ_SW_Track,
						peRow.CZ_SW_Delka,
						peRow.CZ_Doslo,
						item.IsMJNull() ? string.Empty : item.MJ,
						peRow.IsSKL_IDNull() ? string.Empty : peRow.SKL_ID,
						peRow.IsWEIGHTNull() ? (decimal?)null : peRow.WEIGHT,
						peRow.NMBRPAL,
						peRow.TYPEPAL,
						peRow.ITEMCODE,
						peRow.SERLTNUM,
						peRow.CZ_REZ1_Track,
						peRow.CZ_REZ2_Track,
						peRow.IsRealization_StartNull() ? (DateTime?)null : peRow.Realization_Start,
						peRow.IsRealization_StopNull() ? (DateTime?)null : peRow.Realization_Stop,
						peRow.CZ_Expirace_Track
						);

					if (Globals_V1.Konfigurace.Prijem[0].MerneJednotky_DotahovatDalsiVarianty)
					{
						//pro MJ2
						if (!item.IsMJ2Null() && !item.IsMJ2KoefNull())
						{
							CZMST_PETableAdapter.Insert(
								peRow.CountEntries,
								peRow.PONUMBER,
								peRow.ITEMNMBR,
								peRow.ITEMDESC,
								peRow.ORD,
								peRow.VNDDOCNM,
								peRow.VNDITNUM,
								peRow.CZ_CarKod,
								peRow.LOCNCODE,
								quantity,
								(decimal)item.MJ2Koef, //peRow.QTYPACK,
								peRow.CZ_DatVyr_Track,
								peRow.CZ_DatVyr_Delka,
								peRow.CZ_SerNum_Track,
								peRow.CZ_SerNum_Delka,
								peRow.CZ_SW_Track,
								peRow.CZ_SW_Delka,
								peRow.CZ_Doslo,
								item.IsMJ2Null() ? string.Empty : item.MJ2,
								peRow.IsSKL_IDNull() ? string.Empty : peRow.SKL_ID,
								peRow.IsWEIGHTNull() ? (decimal?)null : peRow.WEIGHT,
								peRow.NMBRPAL,
								peRow.TYPEPAL,
								peRow.ITEMCODE,
								peRow.SERLTNUM,
								peRow.CZ_REZ1_Track,
								peRow.CZ_REZ2_Track,
								peRow.IsRealization_StartNull() ? (DateTime?)null : peRow.Realization_Start,
								peRow.IsRealization_StopNull() ? (DateTime?)null : peRow.Realization_Stop,
								peRow.CZ_Expirace_Track
								);
						}
						//pro MJ3
						if (!item.IsMJ3Null() && !item.IsMJ3KoefNull())
						{
							CZMST_PETableAdapter.Insert(
								peRow.CountEntries,
								peRow.PONUMBER,
								peRow.ITEMNMBR,
								peRow.ITEMDESC,
								peRow.ORD,
								peRow.VNDDOCNM,
								peRow.VNDITNUM,
								peRow.CZ_CarKod,
								peRow.LOCNCODE,
								quantity,
								(decimal)item.MJ3Koef, //peRow.QTYPACK,
								peRow.CZ_DatVyr_Track,
								peRow.CZ_DatVyr_Delka,
								peRow.CZ_SerNum_Track,
								peRow.CZ_SerNum_Delka,
								peRow.CZ_SW_Track,
								peRow.CZ_SW_Delka,
								peRow.CZ_Doslo,
								item.IsMJ3Null() ? string.Empty : item.MJ3,
								peRow.IsSKL_IDNull() ? string.Empty : peRow.SKL_ID,
								peRow.IsWEIGHTNull() ? (decimal?)null : peRow.WEIGHT,
								peRow.NMBRPAL,
								peRow.TYPEPAL,
								peRow.ITEMCODE,
								peRow.SERLTNUM,
								peRow.CZ_REZ1_Track,
								peRow.CZ_REZ2_Track,
								peRow.IsRealization_StartNull() ? (DateTime?)null : peRow.Realization_Start,
								peRow.IsRealization_StopNull() ? (DateTime?)null : peRow.Realization_Stop,
								peRow.CZ_Expirace_Track
								);
						}
					}
				}
			}
			else
			{
				string tmpMJ = string.Empty;

				if (peRow.MJ != (object)DBNull.Value)
					tmpMJ = peRow.MJ;

				CZMST_PETableAdapter.Insert(
					peRow.CountEntries,
					peRow.PONUMBER,
					peRow.ITEMNMBR,
					peRow.ITEMDESC,
					peRow.ORD,
					peRow.VNDDOCNM,
					peRow.VNDITNUM,
					peRow.CZ_CarKod,
					peRow.LOCNCODE,
					quantity,
					peRow.QTYPACK,
					peRow.CZ_DatVyr_Track,
					peRow.CZ_DatVyr_Delka,
					peRow.CZ_SerNum_Track,
					peRow.CZ_SerNum_Delka,
					peRow.CZ_SW_Track,
					peRow.CZ_SW_Delka,
					peRow.CZ_Doslo,
					tmpMJ,
					peRow.IsSKL_IDNull() ? string.Empty : peRow.SKL_ID,
					peRow.IsWEIGHTNull() ? (decimal?)null : peRow.WEIGHT,
					peRow.NMBRPAL,
					peRow.TYPEPAL,
					peRow.ITEMCODE,
					peRow.SERLTNUM,
					peRow.CZ_REZ1_Track,
					peRow.CZ_REZ2_Track,
					peRow.IsRealization_StartNull() ? (DateTime?)null : peRow.Realization_Start,
					peRow.IsRealization_StopNull() ? (DateTime?)null : peRow.Realization_Stop,
					peRow.CZ_Expirace_Track
					);
			}

			incCountEntr = true;
		}

		/// <summary>
		/// Vytvori xml soubor pro export prijemek podle zadanyc parametru
		/// </summary>
		/// <param name="filename">cesta kam se ma xml soubor ulozit (mel by byt shodny s adresarem nastavenym v ini souboru)</param>
		/// <param name="lastChanges">Program POHODA exportuje všechny záznamy, které mají datum "uloženo" novější(menší) než datum parametru lastchange (format: 2011-04-29T14:30:00)</param>
		/// <param name="dateFrom">filtr dle datumu vystavení dokladu (format: 2011-01-10)</param>
		/// <param name="dateTill">filtr dle datumu vystavení dokladu (format: 2011-01-10)</param>
		/// <param name="ico">IČ firmy, pro kterou je XML určeno. Hodnota musí souhlasit s IČ zadané firmy.</param>
		/// <param name="note">Textová poznámka, hodnota se zobrazí v záložce "Poznámky" v agendě XML Import.</param>
		/// <returns>TRUE - OK, FALSE - CHYBA</returns>
		public static bool CreateRequest_ObjednavkaVydana_XML(string filename, string lastChanges, string dateFrom, string dateTill, string note, List<string> companys, string userFilterName, List<string> icos, List<string> cislaDokladu)
		{
			try
			{
	
				List<object> filterList = new List<object>();
				List<object> mainfilterList = new List<object>();


				XNamespace dat = "http://www.stormware.cz/schema/version_2/data.xsd";
				XNamespace lst = "http://www.stormware.cz/schema/version_2/list.xsd";
				XNamespace ftr = "http://www.stormware.cz/schema/version_2/filter.xsd";
				XNamespace typ = "http://www.stormware.cz/schema/version_2/type.xsd";

				if (lastChanges != string.Empty)
					filterList.Add(new XElement(ftr + "lastChanges", lastChanges));

				if (dateFrom != string.Empty && dateTill != string.Empty)
				{
					filterList.Add(new XElement(ftr + "dateFrom", dateFrom));
					filterList.Add(new XElement(ftr + "dateTill", dateTill));
				}

				List<object> filterCompanyList = new List<object>();
				if (companys.Count > 0)
				{
					for (int i = 0; i < companys.Count; i++)
					{
						filterCompanyList.Add(new XElement(ftr + "company", companys[i]));
					}

					filterList.Add(new XElement(ftr + "selectedCompanys", filterCompanyList.ToArray()));
				}

				List<object> filterIcoList = new List<object>();
				if (icos.Count > 0)
				{
					for (int i = 0; i < icos.Count; i++)
					{
						filterIcoList.Add(new XElement(ftr + "ico", icos[i]));
					}

					filterList.Add(new XElement(ftr + "selectedIco", filterIcoList.ToArray()));
				}

				List<object> filterCisloDokladuList = new List<object>();
				if (cislaDokladu.Count > 0)
				{
					for (int i = 0; i < cislaDokladu.Count; i++)
					{
						filterCisloDokladuList.Add(new XElement(ftr + "number",
						   (new XElement(typ + "numberRequested", cislaDokladu[i]))));
					}

					filterList.Add(new XElement(ftr + "selectedNumbers", filterCisloDokladuList.ToArray()));
				}


				object[] filter = null;
				object mainFilter = null;

				filter = filterList.ToArray();

				if (userFilterName != string.Empty)
				{
					mainFilter = new XElement(ftr + "userFilterName", userFilterName);
				}
				else
					mainFilter = new XElement(ftr + "filter", filter);

				XElement root = new XElement(dat + "dataPack",
					new XAttribute(XNamespace.Xmlns + "dat", "http://www.stormware.cz/schema/version_2/data.xsd"),
					new XAttribute(XNamespace.Xmlns + "ftr", "http://www.stormware.cz/schema/version_2/filter.xsd"),
					new XAttribute(XNamespace.Xmlns + "lst", "http://www.stormware.cz/schema/version_2/list.xsd"),
					new XAttribute(XNamespace.Xmlns + "typ", "http://www.stormware.cz/schema/version_2/type.xsd"),
					new XAttribute("id", "001"),
					new XAttribute("ico", Globals_V1.Konfigurace.PohodaInfo[0].ICO),
					new XAttribute("application", Fask.SQL.Constants.Common.application_S),
					new XAttribute("version", "2.0"),
					new XAttribute("note", note),

						new XElement(dat + "dataPackItem",
							new XAttribute("id", "li1"),
							new XAttribute("version", "2.0"),

								new XElement(lst + "listOrderRequest",
									new XAttribute("version", "2.0"),
									new XAttribute("orderType", "issuedOrder"),
									new XAttribute("orderVersion", "2.0"),

										new XElement(lst + "requestOrder",
											   mainFilter
										)
								)
						)

				);

				root.Save(filename);

				return true;

			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				return false;
			}
		}

		/// <summary>
		/// Export dokladu prevodky
		/// </summary>
		/// <param name="file">nazev souboru kam se request ulozi</param>
		/// <param name="objednavka">id dokladu</param>
		/// <returns>True : ok; False(exception) : nepovedlo se sestaveni nebo ulozeni(False: bez vyjimky, duvod neznamy; Exception: vyjimka)</returns>
		public static bool CreateRequest_Prevodka_XML(string filename, Fask.Server.Interfaces.Classes.Objednavka objednavka)
		{

			// 
			// ***** VZOR ****
			// 
			//<?xml version="1.0" encoding="utf-8" ?>
			//<dat:dataPack
			//  xmlns:dat="http://www.stormware.cz/schema/version_2/data.xsd"
			//  xmlns:lst="http://www.stormware.cz/schema/version_2/list.xsd"
			//  xmlns:typ="http://www.stormware.cz/schema/version_2/type.xsd"
			//  xmlns:ftr="http://www.stormware.cz/schema/version_2/filter.xsd"
			//  version="2.0"
			//  id="PrevodkaRequest_01"
			//  ico="12345678"
			//  application="FASK_prevodka_Test"
			//  note="Fask prevodka_test_request_01"
			//>
			//  <dat:dataPackItem id="prevodka_test_request_01" version="2.0">
			//    <lst:listPrevodkaRequest version="2.0" prevodkaVersion="2.0">
			//      <lst:requestPrevodka>
			//        <ftr:filter>
			//          <ftr:selectedNumbers>
			//            <ftr:number>
			//              <typ:numberRequested>15Prv00001</typ:numberRequested>
			//            </ftr:number>
			//          </ftr:selectedNumbers>
			//        </ftr:filter>
			//      </lst:requestPrevodka>
			//    </lst:listPrevodkaRequest>
			//  </dat:dataPackItem>
			//</dat:dataPack>

			try
			{
		
				List<object> filterList = new List<object>();

				XNamespace dat = "http://www.stormware.cz/schema/version_2/data.xsd";
				XNamespace lst = "http://www.stormware.cz/schema/version_2/list.xsd";
				XNamespace ftr = "http://www.stormware.cz/schema/version_2/filter.xsd";
				XNamespace typ = "http://www.stormware.cz/schema/version_2/type.xsd";

				XElement ftrSelectedNumbers =
					new XElement(ftr + "selectedNumbers",
						new XElement(ftr + "number",
							new XElement(typ + "numberRequested", objednavka.ID.Trim())
						)
					);

				XElement mainFilter = new XElement(ftr + "filter", ftrSelectedNumbers);

				XElement root = new XElement(dat + "dataPack",
					new XAttribute(XNamespace.Xmlns + "dat", "http://www.stormware.cz/schema/version_2/data.xsd"),
					new XAttribute(XNamespace.Xmlns + "lst", "http://www.stormware.cz/schema/version_2/list.xsd"),
					new XAttribute(XNamespace.Xmlns + "ftr", "http://www.stormware.cz/schema/version_2/filter.xsd"),
					new XAttribute(XNamespace.Xmlns + "typ", "http://www.stormware.cz/schema/version_2/type.xsd"),
					new XAttribute("id", "prevodka"),
					new XAttribute("ico", Globals_V1.Konfigurace.PohodaInfo[0].ICO),
					new XAttribute("application", Fask.SQL.Constants.Common.application_S),
					new XAttribute("version", "2.0"),
					new XAttribute("note", "Fask export prevodka"),

						new XElement(dat + "dataPackItem",
							new XAttribute("id", "prevodkaRequest"),
							new XAttribute("version", "2.0"),

								new XElement(lst + "listPrevodkaRequest",
									new XAttribute("version", "2.0"),
					// new XAttribute("invoiceType", "issuedInvoice"),  // existuje prevodka type??
									new XAttribute("prevodkaVersion", "2.0"),
										new XElement(lst + "requestPrevodka", mainFilter)
										)));

				root.Save(filename);

				return true;

			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				throw ex;
				//return false;
			}

			//
			// ***** VZOR ****
			//
			//<?xml version="1.0" encoding="utf-8" ?>
			//<dat:dataPack 
			//  xmlns:dat="http://www.stormware.cz/schema/version_2/data.xsd"
			//  xmlns:lst="http://www.stormware.cz/schema/version_2/list.xsd"
			//  xmlns:ftr="http://www.stormware.cz/schema/version_2/filter.xsd"
			//  xmlns:typ="http://www.stormware.cz/schema/version_2/type.xsd"
			//  version="2.0"
			//  id="issuedInvoiceRequest_01"
			//  ico="111111"
			//  application="FASK_Test"
			//  note="Fask test issued invoice request 01"
			//  >
			//  <dat:dataPackItem id="issuedInvoiceRequest_01" version="2.0">
			//    <lst:listInvoiceRequest version="2.0" invoiceType="issuedInvoice" invoiceVersion="2.0">
			//      <lst:requestInvoice>
			//        <ftr:filter>
			//          <ftr:selectedNumbers>
			//            <ftr:number>
			//              <typ:numberRequested>XXX</typ:numberRequested>
			//            </ftr:number>
			//            <ftr:number>
			//              <typ:numberRequested>YYY</typ:numberRequested>
			//            </ftr:number>
			//          </ftr:selectedNumbers>
			//        </ftr:filter>
			//      </lst:requestInvoice>
			//    </lst:listInvoiceRequest>
			//  </dat:dataPackItem>
			//</dat:dataPack>

			//try
			//{
			//    string filename = Path.Combine(Globals.PathToInputDirectory, file);

			//    List<object> filterList = new List<object>();

			//    XNamespace dat = "http://www.stormware.cz/schema/version_2/data.xsd";
			//    XNamespace lst = "http://www.stormware.cz/schema/version_2/list.xsd";
			//    XNamespace ftr = "http://www.stormware.cz/schema/version_2/filter.xsd";
			//    XNamespace typ = "http://www.stormware.cz/schema/version_2/type.xsd";

			//    XElement ftrSelectedNumbers =
			//        new XElement(ftr + "selectedNumbers",
			//            new XElement(ftr + "number",
			//                new XElement(ftr + "numberRequested", objednavka.ID.Trim())
			//            )
			//        );

			//    XElement mainFilter = new XElement(ftr + "filter", ftrSelectedNumbers);

			//    XElement root = new XElement(dat + "dataPack",
			//        new XAttribute(XNamespace.Xmlns + "dat", "http://www.stormware.cz/schema/version_2/data.xsd"),
			//        new XAttribute(XNamespace.Xmlns + "lst", "http://www.stormware.cz/schema/version_2/list.xsd"),
			//        new XAttribute(XNamespace.Xmlns + "ftr", "http://www.stormware.cz/schema/version_2/filter.xsd"),
			//        new XAttribute(XNamespace.Xmlns + "typ", "http://www.stormware.cz/schema/version_2/type.xsd"),
			//        new XAttribute("id", "issuedInvoice"),
			//        new XAttribute("ico", Globals.ICO),
			//        new XAttribute("application", Fask.ModulePohodaXML.Constants.Common.application),
			//        new XAttribute("version", "2.0"),
			//        new XAttribute("note", "Fask export issued invoice"),

			//            new XElement(dat + "dataPackItem",
			//                new XAttribute("id", "issuedInvoiceRequest"),
			//                new XAttribute("version", "2.0"),

			//                    new XElement(lst + "listInvoiceRequest",
			//                        new XAttribute("version", "2.0"),
			//                        new XAttribute("invoiceType", "issuedInvoice"),
			//                        new XAttribute("invoiceVersion", "2.0"),
			//                            new XElement(lst + "requestInvoice", mainFilter)
			//                            )));

			//    root.Save(filename);

			//    return true;

			//}
			//catch (Exception ex)
			//{
			//    Log.writeErrorLog(ex.ToString());
			//    throw ex;
			//    //return false;
			//}
		}

		public static bool CreateImportXML_PrijemkaVazbaObjednavka_Z_Prijmu(string filename, int countEntries, string note)
		{

			//Format čisel pro ToString()
			System.Globalization.NumberFormatInfo nfi = System.Globalization.NumberFormatInfo.InvariantInfo;

			Datasets.Prijem ds_pi = null;
			Datasets.Prijem.CZMST_PIHDataTable dt_pih = null;





			try
			{
				Datasets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter ParamTA = null;
				if (!Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1)
				{
					ParamTA = new Datasets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter();
					ParamTA.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
				}

				//NameSpace pro XML soubor
				XNamespace dat = "http://www.stormware.cz/schema/version_2/data.xsd";
				XNamespace pri = "http://www.stormware.cz/schema/version_2/prijemka.xsd";
				XNamespace typ = "http://www.stormware.cz/schema/version_2/type.xsd";

				DateTime date = DateTime.Now;


				// Konfiguračne možnost Grupovat položky pred nahranim do Pohody
				if (Globals_V1.Konfigurace.Prijem[0].GrupujDataPrijemka)
					ds_pi = Database.Prijem.GETDATA_CZMSTPI_DS_GroupBy_CountEntries(countEntries);
				else
					ds_pi = Database.Prijem.GETDATA_CZMSTPI_DS_By_CountEntries(countEntries);

				// TODO : test na to, ze neco je v dt_pi !!!
				if (ds_pi == null || ds_pi.CZMST_PI.Count == 0)
				{
					throw new Exception("Nebyla nalezena data příjemky pro zpracování!");
				}

				var dt_pi = ds_pi.CZMST_PI;

				//Vytaženi hodnot do Hlavičny XML requestu, dotahuje se s prvniho, neočekava se Slučovaní!!!
				string SKL_ID = dt_pi[0].SKL_ID.Trim();

				//Dotaženi HLavičky pro Datum dohladu
				dt_pih = Database.Prijem.GETDATA_CZMSTPIH(countEntries);

				DateTime? datumdokladu = null;
				if (dt_pih != null && dt_pih.Count > 0)
					datumdokladu = dt_pih[0].DATUMDOKLADU;


				#region Dotazeni info z Hlavičky

				Datasets.DatabasePohoda.OBJDataTable pohoda_obj_dt = Database.Pohoda.OBJ_GetDataByCislo(dt_pi[0].PONUMBER.Trim());

				if ((pohoda_obj_dt == null) || (pohoda_obj_dt.Count == 0))
				{
					throw new Exception("Objednavka nenalezena");
				}

				Datasets.DatabasePohoda.OBJRow pohoda_obj = pohoda_obj_dt.First();

				string partnerID = pohoda_obj.IsRefADNull() ? string.Empty : pohoda_obj.RefAD.ToString();
				string cislozak = pohoda_obj.IsCisloZAKNull() ? string.Empty : pohoda_obj.CisloZAK;
				string cinnost = pohoda_obj.IsRefCinNull() ? string.Empty : pohoda_obj.RefCin.ToString();
				string stredisko = pohoda_obj.IsRefStrNull() ? string.Empty : pohoda_obj.RefStr.ToString();
				string text = pohoda_obj.IsSTextNull() ? string.Empty : pohoda_obj.SText;
				string poznamka = pohoda_obj.IsPoznNull() ? string.Empty : pohoda_obj.Pozn;
				string poznamka2 = pohoda_obj.IsPozn2Null() ? string.Empty : pohoda_obj.Pozn2;
				string cizimenaid = pohoda_obj.IsRefCMNull() ? string.Empty : pohoda_obj.RefCM.ToString();

				Datasets.DatabasePohoda.OBJpolDataTable pohoda_odbpol_dt = Database.Pohoda.OBJPol_GetDataByRefAg(pohoda_obj.ID);

				#endregion

				#region Dotažení řady z vazebni tabulky FASK_RADY

				int? idsradadokladu = null;
				try
				{
					Fask.Rady.NumericalSeries NS = new Fask.Rady.NumericalSeries(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
					idsradadokladu = NS.GetCiselnaRada_ID(Fask.Rady.TypDB.SQL, Fask.Rady.Modul.PrP, SKL_ID, string.Empty, false);

				}
				catch (Exception ex)
				{
					Fask.Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML", "CreateImportXML_PrijemkaVazbaObjednavka_Z_Prijmu", ex);
				}

				#endregion

				#region Hlavička


				List<XElement> listHeader = new List<XElement>();

				if (!string.IsNullOrEmpty(text))
					listHeader.Add(new XElement(pri + "text", text.Trim()));


				StringBuilder poznB = new StringBuilder();
				if (!string.IsNullOrEmpty(poznamka))
					poznB.AppendLine(poznamka);

				if (!string.IsNullOrEmpty(note))
					poznB.AppendLine(note);

				if (poznB.Length > 0)
					listHeader.Add(new XElement(pri + "note", poznB.ToString()));


				listHeader.Add(new XElement(pri + "intNote", (poznamka2 == null ? string.Empty : (poznamka2 + ".") + "\n") + "Nacteno z mobilniho terminalu z dávky č.:" + countEntries.ToString()));


				if (datumdokladu.HasValue)
				{
					listHeader.Add(new XElement(pri + "date", XmlConvert.ToString(datumdokladu.Value, "yyyy-MM-dd")));
				}

				if (idsradadokladu.HasValue)
				{
					listHeader.Add(new XElement(pri + "number", new XElement(typ + "id", idsradadokladu.Value.ToString())));
				}

				if (!string.IsNullOrEmpty(partnerID))
				{
					listHeader.Add(new XElement(pri + "partnerIdentity", new XElement(typ + "id", partnerID)));
				}

				if (!string.IsNullOrEmpty(cislozak))
				{
					listHeader.Add(new XElement(pri + "contract", new XElement(typ + "ids", cislozak)));
				}

				if (!string.IsNullOrEmpty(cinnost))
				{
					listHeader.Add(new XElement(pri + "activity", new XElement(typ + "id", cinnost)));
				}

				if (!string.IsNullOrEmpty(stredisko))
				{
					listHeader.Add(new XElement(pri + "centre", new XElement(typ + "id", stredisko)));
				}

				#endregion


				#region Položky

				List<XElement> listDetail = new List<XElement>();

				foreach (Datasets.Prijem.CZMST_PIRow row in dt_pi)
				{
					List<XElement> listPrijemkaItem = new List<XElement>();

					var objpols = pohoda_odbpol_dt.Where(x => (x.ID == row.ORD)); //jedinecna vazba ...
					Datasets.DatabasePohoda.OBJpolRow objpol = null;
					if (objpols.Count() > 0)
						objpol = objpols.First();


					if ((objpol != null) && (!objpol.IsRefStrNull()) && (objpol.RefStr != 0))
					{
						listPrijemkaItem.Add(new XElement(pri + "centre", new XElement(typ + "id", objpol.RefStr)));
					}

					if ((objpol != null) && (!objpol.IsRefCinNull()) && (objpol.RefCin != 0))
					{
						listPrijemkaItem.Add(new XElement(pri + "activity", new XElement(typ + "id", objpol.RefCin)));
					}
					if ((objpol != null) && (!objpol.IsCisloZAKNull()))
					{
						listPrijemkaItem.Add(new XElement(pri + "contract", new XElement(typ + "ids", objpol.RefStr)));
					}


					listPrijemkaItem.Add(new XElement(pri + "quantity", ((float)row.QTYSHPPD).ToString(nfi)));

					if ((objpol != null) && (!objpol.IsMJNull()))
					{
						listPrijemkaItem.Add(new XElement(pri + "unit", objpol.MJ.Trim()));
					}

					if ((objpol != null) && (!objpol.IsMJKoefNull()))
					{
						listPrijemkaItem.Add(new XElement(pri + "coefficient", objpol.MJKoef.ToString(nfi)));
					}

					if ((objpol != null) && (!objpol.IsSlevaNull()))
					{
						listPrijemkaItem.Add(new XElement(pri + "discountPercentage", objpol.Sleva.ToString(nfi)));
					}

					if ((objpol != null))
					{
						listPrijemkaItem.Add(new XElement(pri + "payVAT", objpol.SDph.ToString().ToLower()));
					}

					if ((objpol != null) && (!objpol.IsRelSzDPHNull()))
					{
						string dphSazba = Classes.Pohoda.Sazby.GetName(objpol.RelSzDPH);
						listPrijemkaItem.Add(new XElement(pri + "rateVAT", dphSazba));
					}

					if ((objpol != null) && (!objpol.IsKcJednNull()))
					{
						listPrijemkaItem.Add(new XElement(pri + "homeCurrency", new XElement(typ + "unitPrice", objpol.KcJedn.ToString(nfi))));
					}

					if ((objpol != null) && (!objpol.IsCmJednNull()))
					{
						listPrijemkaItem.Add(new XElement(pri + "foreignCurrency", new XElement(typ + "unitPrice", objpol.CmJedn.ToString(nfi))));
					}

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
							Logika_prijem_Sledovani_ImportPOHODA(
								pri, 
								row, 
								listPrijemkaItem, 
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

								Logika_prijem_Sledovani_ImportPOHODA(
										pri,
										row,
										listPrijemkaItem,
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
								Prijem_SetParams_serltnum_itemnmbr(pri, row, listPrijemkaItem, sern, itemnmbr);
							}
						}


						#endregion

					}
					else
					{
						Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Import Prijem. ale tohle by nemnelo nastat. Karta nenalezena:" + row.ITEMNMBR);
						Prijem_SetParams_serltnum_itemnmbr(pri, row, listPrijemkaItem, sern, itemnmbr);
					}

					if (!row.IsExpiraceNull())
					{
						listPrijemkaItem.Add(new XElement(pri + "expirationDate", XmlConvert.ToString(row.Expirace, "yyyy-MM-dd")));
					}

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

					

					if (!row.IsExpiraceNull())
					{
						List<XElement> listVPrEXP = new List<XElement>();
						var nameEXP = new XElement(typ + "name", "VPrExspiraceKSN");
						var textValueEXP = new XElement(typ + "datetimeValue", XmlConvert.ToString(row.Expirace, "yyyy-MM-dd"));
						listVPrEXP.Add(nameEXP);
						listVPrEXP.Add(textValueEXP);

						listVPrParams.Add(new XElement(typ + "parameter", listVPrEXP));

					}

					if (listVPrParams.Count > 0)
					{
						listPrijemkaItem.Add(new XElement(pri + "parameters", listVPrParams));
					}

					listDetail.Add(new XElement(pri + "prijemkaItem", listPrijemkaItem));

				}

				#endregion


				#region Summary

				List<XElement> listSummary = new List<XElement>();


				if (!string.IsNullOrEmpty(cizimenaid))
				{
					if (!pohoda_obj.IsCmKursNull())
					{
						listSummary.Add(new XElement(pri + "foreignCurrency", new XElement(typ + "currency", new XElement(typ + "id", cizimenaid)), new XElement(typ + "rate", pohoda_obj.CmKurs.ToString(nfi))));
					}
					else
					{
						listSummary.Add(new XElement(pri + "foreignCurrency", new XElement(typ + "currency", new XElement(typ + "id", cizimenaid))));
					}
				}

				#endregion


				XElement root = new XElement(dat + "dataPack",
		new XAttribute("id", "PRI" + countEntries.ToString()),
		new XAttribute("ico", Globals_V1.Konfigurace.PohodaInfo[0].ICO),
		new XAttribute("application", Fask.SQL.Constants.Common.application_S),
		new XAttribute("version", "2.0"),
		new XAttribute("note", "Import Prijemky"),
		new XAttribute(XNamespace.Xmlns + "dat", "http://www.stormware.cz/schema/version_2/data.xsd"),
		new XAttribute(XNamespace.Xmlns + "pri", "http://www.stormware.cz/schema/version_2/prijemka.xsd"),
		new XAttribute(XNamespace.Xmlns + "typ", "http://www.stormware.cz/schema/version_2/type.xsd"),

			new XElement(dat + "dataPackItem",
				new XAttribute("id", "PRI" + countEntries.ToString()),
				new XAttribute("version", "2.0"),
					new XElement(pri + "prijemka", new XAttribute("version", "2.0"),
						new XElement(pri + "prijemkaHeader", listHeader),
						new XElement(pri + "prijemkaDetail", listDetail),
						new XElement(pri + "prijemkaSummary", listSummary)
				)));

				root.Save(filename);

				return true;

			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				return false;
			}

		}

		public static bool CreateImportXML_PrijemkaVazbaObjednavka_Z_Prijmu_Z_SI(string filename, int countEntries, string note)
		{

			////Format čisel pro ToString()
			//System.Globalization.NumberFormatInfo nfi = System.Globalization.NumberFormatInfo.InvariantInfo;

			//Datasets.Prijem ds_pi = null;
			//Datasets.Prijem.CZMST_PIHDataTable dt_pih = null;


			// konstanta pro tostrin() cisel na invariantni format ...
			System.Globalization.NumberFormatInfo nfi = System.Globalization.NumberFormatInfo.InvariantInfo;
			Datasets.Vydej.CZMST_SIDataTable dt_si = null;


			try
			{
				Datasets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter ParamTA = null;
				if (!Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1)
				{
					ParamTA = new Datasets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter();
					ParamTA.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
				}

				//NameSpace pro XML soubor
				XNamespace dat = "http://www.stormware.cz/schema/version_2/data.xsd";
				XNamespace pri = "http://www.stormware.cz/schema/version_2/prijemka.xsd";
				XNamespace typ = "http://www.stormware.cz/schema/version_2/type.xsd";

				DateTime date = DateTime.Now;


				// Konfiguračne možnost Grupovat položky pred nahranim do Pohody
				if (Globals_V1.Konfigurace.Prijem[0].GrupujDataPrijemka)
					dt_si = Database.Vydej.GETDATA_CZMSTSI_DS_GroupBy_CountEntries(countEntries);
				else
					dt_si = Database.Vydej.GETDATA_CZMSTSI_By_CountEntries(countEntries);

				// TODO : test na to, ze neco je v dt_pi !!!
				if (dt_si == null || dt_si.Count == 0)
				{
					throw new Exception("Nebyla nalezena data příjemky pro zpracování!");
				}

				

				//Vytaženi hodnot do Hlavičny XML requestu, dotahuje se s prvniho, neočekava se Slučovaní!!!
				string SKL_ID = dt_si[0].SKL_ID.Trim();

				

				DateTime? datumdokladu = DateTime.Now;

                //24.7.2025 MaR zakomentovano
                //Dotaženi HLavičky pro Datum dohladu
                //dt_pih = Database.Prijem.GETDATA_CZMSTPIH(countEntries);
                //DateTime? datumdokladu = null;
                //if (dt_pih != null && dt_pih.Count > 0)
                //	datumdokladu = dt_pih[0].DATUMDOKLADU;


                #region Dotazeni info z Hlavičky

                Datasets.DatabasePohoda.OBJDataTable pohoda_obj_dt = Database.Pohoda.OBJ_GetDataByCislo((string)dt_si[0].SOPNUMBE.Trim());

				if ((pohoda_obj_dt == null) || (pohoda_obj_dt.Count == 0))
				{
					throw new Exception("Objednavka nenalezena");
				}

				Datasets.DatabasePohoda.OBJRow pohoda_obj = pohoda_obj_dt.First();

				string partnerID = pohoda_obj.IsRefADNull() ? string.Empty : pohoda_obj.RefAD.ToString();
				string cislozak = pohoda_obj.IsCisloZAKNull() ? string.Empty : pohoda_obj.CisloZAK;
				string cinnost = pohoda_obj.IsRefCinNull() ? string.Empty : pohoda_obj.RefCin.ToString();
				string stredisko = pohoda_obj.IsRefStrNull() ? string.Empty : pohoda_obj.RefStr.ToString();
				string text = pohoda_obj.IsSTextNull() ? string.Empty : pohoda_obj.SText;
				string poznamka = pohoda_obj.IsPoznNull() ? string.Empty : pohoda_obj.Pozn;
				string poznamka2 = pohoda_obj.IsPozn2Null() ? string.Empty : pohoda_obj.Pozn2;
				string cizimenaid = pohoda_obj.IsRefCMNull() ? string.Empty : pohoda_obj.RefCM.ToString();

				Datasets.DatabasePohoda.OBJpolDataTable pohoda_odbpol_dt = Database.Pohoda.OBJPol_GetDataByRefAg(pohoda_obj.ID);

				#endregion

				#region Dotažení řady z vazebni tabulky FASK_RADY

				int? idsradadokladu = null;
				try
				{
					Fask.Rady.NumericalSeries NS = new Fask.Rady.NumericalSeries(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
					idsradadokladu = NS.GetCiselnaRada_ID(Fask.Rady.TypDB.SQL, Fask.Rady.Modul.PrP, SKL_ID, string.Empty, false);

				}
				catch (Exception ex)
				{
					Fask.Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML", "CreateImportXML_PrijemkaVazbaObjednavka_Z_Prijmu", ex);
				}

				#endregion

				#region Hlavička


				List<XElement> listHeader = new List<XElement>();

				if (!string.IsNullOrEmpty(text))
					listHeader.Add(new XElement(pri + "text", text.Trim()));


				StringBuilder poznB = new StringBuilder();
				if (!string.IsNullOrEmpty(poznamka))
					poznB.AppendLine(poznamka);

				if (!string.IsNullOrEmpty(note))
					poznB.AppendLine(note);

				if (poznB.Length > 0)
					listHeader.Add(new XElement(pri + "note", poznB.ToString()));


				listHeader.Add(new XElement(pri + "intNote", (poznamka2 == null ? string.Empty : (poznamka2 + ".") + "\n") + "Nacteno z mobilniho terminalu z dávky č.:" + countEntries.ToString()));


				if (datumdokladu.HasValue)
				{
					listHeader.Add(new XElement(pri + "date", XmlConvert.ToString(datumdokladu.Value, "yyyy-MM-dd")));
				}

				if (idsradadokladu.HasValue)
				{
					listHeader.Add(new XElement(pri + "number", new XElement(typ + "id", idsradadokladu.Value.ToString())));
				}

				if (!string.IsNullOrEmpty(partnerID))
				{
					listHeader.Add(new XElement(pri + "partnerIdentity", new XElement(typ + "id", partnerID)));
				}

				if (!string.IsNullOrEmpty(cislozak))
				{
					listHeader.Add(new XElement(pri + "contract", new XElement(typ + "ids", cislozak)));
				}

				if (!string.IsNullOrEmpty(cinnost))
				{
					listHeader.Add(new XElement(pri + "activity", new XElement(typ + "id", cinnost)));
				}

				if (!string.IsNullOrEmpty(stredisko))
				{
					listHeader.Add(new XElement(pri + "centre", new XElement(typ + "id", stredisko)));
				}

				#endregion


				#region Položky

				List<XElement> listDetail = new List<XElement>();

				foreach (Datasets.Vydej.CZMST_SIRow row in dt_si)
				{
					List<XElement> listPrijemkaItem = new List<XElement>();

					var objpols = pohoda_odbpol_dt.Where(x => (x.ID == row.ORD)); //jedinecna vazba ...
					Datasets.DatabasePohoda.OBJpolRow objpol = null;
					if (objpols.Count() > 0)
						objpol = objpols.First();


					if ((objpol != null) && (!objpol.IsRefStrNull()) && (objpol.RefStr != 0))
					{
						listPrijemkaItem.Add(new XElement(pri + "centre", new XElement(typ + "id", objpol.RefStr)));
					}

					if ((objpol != null) && (!objpol.IsRefCinNull()) && (objpol.RefCin != 0))
					{
						listPrijemkaItem.Add(new XElement(pri + "activity", new XElement(typ + "id", objpol.RefCin)));
					}
					if ((objpol != null) && (!objpol.IsCisloZAKNull()))
					{
						listPrijemkaItem.Add(new XElement(pri + "contract", new XElement(typ + "ids", objpol.RefStr)));
					}


					listPrijemkaItem.Add(new XElement(pri + "quantity", ((float)row.QTYSHPPD).ToString(nfi)));

					if ((objpol != null) && (!objpol.IsMJNull()))
					{
						listPrijemkaItem.Add(new XElement(pri + "unit", objpol.MJ.Trim()));
					}

					if ((objpol != null) && (!objpol.IsMJKoefNull()))
					{
						listPrijemkaItem.Add(new XElement(pri + "coefficient", objpol.MJKoef.ToString(nfi)));
					}

					if ((objpol != null) && (!objpol.IsSlevaNull()))
					{
						listPrijemkaItem.Add(new XElement(pri + "discountPercentage", objpol.Sleva.ToString(nfi)));
					}

					if ((objpol != null))
					{
						listPrijemkaItem.Add(new XElement(pri + "payVAT", objpol.SDph.ToString().ToLower()));
					}

					if ((objpol != null) && (!objpol.IsRelSzDPHNull()))
					{
						string dphSazba = Classes.Pohoda.Sazby.GetName(objpol.RelSzDPH);
						listPrijemkaItem.Add(new XElement(pri + "rateVAT", dphSazba));
					}

					if ((objpol != null) && (!objpol.IsKcJednNull()))
					{
						listPrijemkaItem.Add(new XElement(pri + "homeCurrency", new XElement(typ + "unitPrice", objpol.KcJedn.ToString(nfi))));
					}

					if ((objpol != null) && (!objpol.IsCmJednNull()))
					{
						listPrijemkaItem.Add(new XElement(pri + "foreignCurrency", new XElement(typ + "unitPrice", objpol.CmJedn.ToString(nfi))));
					}

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
							Logika_prijem_Sledovani_ImportPOHODA_Z_SI(
								pri,
								row,
								listPrijemkaItem,
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

								Logika_prijem_Sledovani_ImportPOHODA_Z_SI(
										pri,
										row,
										listPrijemkaItem,
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
								Prijem_SetParams_serltnum_itemnmbr_Z_SI(pri, row, listPrijemkaItem, sern, itemnmbr);
							}
						}


						#endregion

					}
					else
					{
						Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Import Prijem. ale tohle by nemnelo nastat. Karta nenalezena:" + row.ITEMNMBR);
						Prijem_SetParams_serltnum_itemnmbr_Z_SI(pri, row, listPrijemkaItem, sern, itemnmbr);
					}

					if (!row.IsExpiraceNull())
					{
						listPrijemkaItem.Add(new XElement(pri + "expirationDate", XmlConvert.ToString(row.Expirace, "yyyy-MM-dd")));
					}

					List<XElement> listVPrParams = new List<XElement>();


					//24.7.2025MaR zakomentoval
					//if (!row.IsAttributeToSNNull() && !string.IsNullOrEmpty(row.AttributeToSN))
					//{


					//	List<XElement> listVPr = new List<XElement>();
					//	var name = new XElement(typ + "name", "VPrSarzeKSN");
					//	var textValue = new XElement(typ + "textValue", "");
					//	//24.7.2025 MaR zakomentovano
					//	//var textValue = new XElement(typ + "textValue", row.AttributeToSN);
					//	listVPr.Add(name);
					//	listVPr.Add(textValue);

					//	listVPrParams.Add(new XElement(typ + "parameter", listVPr));
					//}



					if (!row.IsExpiraceNull())
					{
						List<XElement> listVPrEXP = new List<XElement>();
						var nameEXP = new XElement(typ + "name", "VPrExspiraceKSN");
						var textValueEXP = new XElement(typ + "datetimeValue", XmlConvert.ToString(row.Expirace, "yyyy-MM-dd"));
						listVPrEXP.Add(nameEXP);
						listVPrEXP.Add(textValueEXP);

						listVPrParams.Add(new XElement(typ + "parameter", listVPrEXP));

					}

					if (listVPrParams.Count > 0)
					{
						listPrijemkaItem.Add(new XElement(pri + "parameters", listVPrParams));
					}

					listDetail.Add(new XElement(pri + "prijemkaItem", listPrijemkaItem));

				}

				#endregion


				#region Summary

				List<XElement> listSummary = new List<XElement>();


				if (!string.IsNullOrEmpty(cizimenaid))
				{
					if (!pohoda_obj.IsCmKursNull())
					{
						listSummary.Add(new XElement(pri + "foreignCurrency", new XElement(typ + "currency", new XElement(typ + "id", cizimenaid)), new XElement(typ + "rate", pohoda_obj.CmKurs.ToString(nfi))));
					}
					else
					{
						listSummary.Add(new XElement(pri + "foreignCurrency", new XElement(typ + "currency", new XElement(typ + "id", cizimenaid))));
					}
				}

				#endregion


				XElement root = new XElement(dat + "dataPack",
		new XAttribute("id", "PRI" + countEntries.ToString()),
		new XAttribute("ico", Globals_V1.Konfigurace.PohodaInfo[0].ICO),
		new XAttribute("application", Fask.SQL.Constants.Common.application_S),
		new XAttribute("version", "2.0"),
		new XAttribute("note", "Import Prijemky"),
		new XAttribute(XNamespace.Xmlns + "dat", "http://www.stormware.cz/schema/version_2/data.xsd"),
		new XAttribute(XNamespace.Xmlns + "pri", "http://www.stormware.cz/schema/version_2/prijemka.xsd"),
		new XAttribute(XNamespace.Xmlns + "typ", "http://www.stormware.cz/schema/version_2/type.xsd"),

			new XElement(dat + "dataPackItem",
				new XAttribute("id", "PRI" + countEntries.ToString()),
				new XAttribute("version", "2.0"),
					new XElement(pri + "prijemka", new XAttribute("version", "2.0"),
						new XElement(pri + "prijemkaHeader", listHeader),
						new XElement(pri + "prijemkaDetail", listDetail),
						new XElement(pri + "prijemkaSummary", listSummary)
				)));

				root.Save(filename);

				return true;

			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				return false;
			}

		}



		private static void Logika_prijem_Sledovani_ImportPOHODA(
			XNamespace pri, 
			Datasets.Prijem.CZMST_PIRow row, 
			List<XElement> listPrijemkaItem, 
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
                        listPrijemkaItem.Add(new XElement(pri + "stockItem", itemnmbr));
                    }
                    else if (RefVPrFXTS.Value == 1 || RefVPrFXTS.Value == 2)
                    {
                        Prijem_SetParams_serltnum_Poznamka_itemnmbr(pri, row, listPrijemkaItem, itemnmbr);
                    }

                }
                else
                {
                    if (stavVPrFPTS)
                    {

                        if (!RefVPrFPTS.HasValue || RefVPrFPTS.Value == 0)
                        {
                            listPrijemkaItem.Add(new XElement(pri + "stockItem", itemnmbr));
                        }
                        else if (RefVPrFPTS.Value == 1 || RefVPrFPTS.Value == 2)
                        {
                            Prijem_SetParams_serltnum_Poznamka_itemnmbr(pri, row, listPrijemkaItem, itemnmbr);
                        }

                    }
                    else
                    {
                        listPrijemkaItem.Add(new XElement(pri + "stockItem", itemnmbr));
                    }
                }
            }
            else if (RelSKzVC.Value == 1 || RelSKzVC.Value == 2)
            {
                Prijem_SetParams_serltnum_itemnmbr(pri, row, listPrijemkaItem, sern, itemnmbr);
            }
        }

		private static void Logika_prijem_Sledovani_ImportPOHODA_Z_SI(
	XNamespace pri,
	Datasets.Vydej.CZMST_SIRow row,
	List<XElement> listPrijemkaItem,
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
						listPrijemkaItem.Add(new XElement(pri + "stockItem", itemnmbr));
					}
					else if (RefVPrFXTS.Value == 1 || RefVPrFXTS.Value == 2)
					{
						Prijem_SetParams_serltnum_Poznamka_itemnmbr_Z_SI(pri, row, listPrijemkaItem, itemnmbr);
					}

				}
				else
				{
					if (stavVPrFPTS)
					{

						if (!RefVPrFPTS.HasValue || RefVPrFPTS.Value == 0)
						{
							listPrijemkaItem.Add(new XElement(pri + "stockItem", itemnmbr));
						}
						else if (RefVPrFPTS.Value == 1 || RefVPrFPTS.Value == 2)
						{
							Prijem_SetParams_serltnum_Poznamka_itemnmbr_Z_SI(pri, row, listPrijemkaItem, itemnmbr);
						}

					}
					else
					{
						listPrijemkaItem.Add(new XElement(pri + "stockItem", itemnmbr));
					}
				}
			}
			else if (RelSKzVC.Value == 1 || RelSKzVC.Value == 2)
			{
				Prijem_SetParams_serltnum_itemnmbr_Z_SI(pri, row, listPrijemkaItem, sern, itemnmbr);
			}
		}

		private static void Prijem_SetParams_serltnum_Poznamka_itemnmbr(XNamespace pri, Datasets.Prijem.CZMST_PIRow row, List<XElement> listPrijemkaItem, XElement itemnmbr)
        {
            listPrijemkaItem.Add(new XElement(pri + "stockItem", itemnmbr));

            if (row.SERLTNUM.Trim().Length > 0)
            {
                listPrijemkaItem.Add(new XElement(pri + "note", row.SERLTNUM.Trim()));
            }
        }



		private static void Prijem_SetParams_serltnum_Poznamka_itemnmbr_Z_SI(XNamespace pri, Datasets.Vydej.CZMST_SIRow row, List<XElement> listPrijemkaItem, XElement itemnmbr)
		{
			listPrijemkaItem.Add(new XElement(pri + "stockItem", itemnmbr));

			if (row.SERLTNUM.Trim().Length > 0)
			{
				listPrijemkaItem.Add(new XElement(pri + "note", row.SERLTNUM.Trim()));
			}
		}

		private static void Prijem_SetParams_serltnum_itemnmbr(XNamespace pri, Datasets.Prijem.CZMST_PIRow row, List<XElement> listPrijemkaItem, XElement sern, XElement itemnmbr)
        {
            if (row.SERLTNUM.Trim().Length > 0)
            {
                listPrijemkaItem.Add(new XElement(pri + "stockItem", itemnmbr, sern));
            }
            else
            {
                listPrijemkaItem.Add(new XElement(pri + "stockItem", itemnmbr));
            }
        }

		private static void Prijem_SetParams_serltnum_itemnmbr_Z_SI(XNamespace pri, Datasets.Vydej.CZMST_SIRow row, List<XElement> listPrijemkaItem, XElement sern, XElement itemnmbr)
		{
			if (row.SERLTNUM.Trim().Length > 0)
			{
				listPrijemkaItem.Add(new XElement(pri + "stockItem", itemnmbr, sern));
			}
			else
			{
				listPrijemkaItem.Add(new XElement(pri + "stockItem", itemnmbr));
			}
		}

	}
}