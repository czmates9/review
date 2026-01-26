using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using Fask.Server.Interfaces.Classes;
using Fask.DataSets;
using Fask.Server.Interfaces.DataSets;

namespace Fask.SQL
{
    public partial class Provider : Fask.Server.Interfaces.Prodej.IProdej,
		Fask.Server.Interfaces.Prodej.IProdej_TiskPOHODA
    {

        private string TABLE_CZMST_DI = "CZMST_DI";


		#region Old kod

		// 4.5.2016 PeV: zakomentovano, pouziva se pouze lokacni mechanismus
		//Fask.Server.Interfaces.Classes.StatusObject Fask.Server.Interfaces.Prodej.IProdej.Prodej_Process(Fask.Server.Interfaces.Classes.Davka davka, Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.User uzivatel, Fask.DataSets.ProdejData data)
		//{
		//    Fask.Server.Interfaces.Classes.StatusObject so = new Fask.Server.Interfaces.Classes.StatusObject();
		//    so.StatusText = "Error";
		//    //throw new NotImplementedException();
		//    try
		//    {

		//        Datasets.VydejBezPredlohy prodejdatads = new Datasets.VydejBezPredlohy();
		//        foreach (Fask.DataSets.ProdejData.CZMST_DIRow dirow in data.CZMST_DI)
		//        {
		//            prodejdatads.CZMST_DI.ImportRow(dirow);
		//        }

		//        string dateFrom = string.Empty;
		//        string dateTill = string.Empty;
		//        string dateLasChange = string.Empty;
		//        string uzivFiltrPohoda = string.Empty;

		//        string pom = string.Empty;

		//        pom = Globals.LoadConfiguration();

		//        if (pom != "OK")
		//            return null;

		//        string idCizimena = string.Empty;

		//        if (data.CZMST_DIH != null && data.CZMST_DIH.Count > 0)
		//            idCizimena = data.CZMST_DIH[0].Ismena_IDNull() ? string.Empty : data.CZMST_DIH[0].mena_ID;

		//        //zjistime typ dokladu...
		//        string dokladID = prodejdatads.CZMST_DI[0].DOC_ID.Trim();                
		//        Doklad typDoklad = null;
		//        for (int i = 0; i < Globals.listDokladu.Count; i++)
		//        {
		//            if (dokladID == Globals.listDokladu[i].docid)
		//            {
		//                typDoklad = Globals.listDokladu[i];
		//                break;
		//            }
		//        }


		//        if (typDoklad.funkce == "objbezodb")
		//        { //snimano zbozi bez dodavatele/odberatele
		//            DataView view = new DataView(prodejdatads.CZMST_DI);
		//            DataTable distinctValues = view.ToTable(true, "odb_id");

		//            List<string> odbIds = new List<string>();
		//            foreach (DataRow row in distinctValues.Rows)
		//            {
		//                odbIds.Add(row.ItemArray[0].ToString());
		//            }

		//            for (int i = 0; i < odbIds.Count; i++)
		//            {
		//                try
		//                {
		//                    Datasets.VydejBezPredlohy.CZMST_DIDataTable di_dt = new Datasets.VydejBezPredlohy.CZMST_DIDataTable();

		//                    string dodavatelID = string.Empty;
		//                    string davkaID = davka.ID.ToString() + "_" + i;

		//                    string file = FilenameComposeTypDoklad(typDoklad);

		//                    if (String.IsNullOrEmpty(odbIds[i]))
		//                    { //dotahnout defaultniho odb
		//                        Datasets.DatabasePohodaTableAdapters.ADTableAdapter ad_ta = new Datasets.DatabasePohodaTableAdapters.ADTableAdapter();
		//                        ad_ta.Connection.ConnectionString = Globals.ConnectionStringPohodaDB;

		//                        Datasets.DatabasePohoda.ADDataTable ad_dt = ad_ta.GetDataByID(int.Parse(Globals.IDDefaultniDodavatel));

		//                        if (ad_ta.Connection != null && ad_ta.Connection.State == ConnectionState.Open)
		//                            ad_ta.Connection.Close();

		//                        dodavatelID = ad_dt[0].ID.ToString();

		//                        //polozky bez odberatele
		//                        Datasets.VydejBezPredlohy.CZMST_DIRow[] rows = (Datasets.VydejBezPredlohy.CZMST_DIRow[])prodejdatads.CZMST_DI.Select("odb_id is null or odb_id = ''");

		//                        for (int x = 0; x < rows.Length; x++)
		//                            di_dt.ImportRow(rows[x]);
		//                    }
		//                    else
		//                    {
		//                        dodavatelID = odbIds[i];

		//                        //polozky jen pro toho odberatele...
		//                        Datasets.VydejBezPredlohy.CZMST_DIRow[] rows = (Datasets.VydejBezPredlohy.CZMST_DIRow[])prodejdatads.CZMST_DI.Select("odb_id='" + dodavatelID + "'");

		//                        for (int x = 0; x < rows.Length; x++)
		//                            di_dt.ImportRow(rows[x]);
		//                    }

		//                    //zjistit na jakou je menu....
		//                    Datasets.DatabasePohodaTableAdapters.ADTableAdapter ad2_ta = new Datasets.DatabasePohodaTableAdapters.ADTableAdapter();
		//                    ad2_ta.Connection.ConnectionString = Globals.ConnectionStringPohodaDB;

		//                    Datasets.DatabasePohoda.ADDataTable ad2_dt = ad2_ta.GetDataByID(int.Parse(dodavatelID));

		//                    if (ad2_ta.Connection != null && ad2_ta.Connection.State == ConnectionState.Open)
		//                        ad2_ta.Connection.Close();

		//                    if (ad2_dt[0].P3)
		//                    {//cizi mena
		//                        idCizimena = Globals.KodCiziMena;
		//                    }
		//                    else
		//                    {//defaultni mena
		//                        idCizimena = string.Empty;
		//                    }

		//                    if (!XML.MST_Pohoda.CreateProdejImportXML(di_dt, file, davkaID, "", uzivatel, idCizimena, typDoklad, dodavatelID))
		//                        return null;

		//                    bool saveToDB = true;

		//                    pom = XML.MST_Pohoda.ZpracovaniUlozeniVsechDatDoDB(file, saveToDB, new Fask.Server.Interfaces.Classes.Objednavka(), uzivatel);

		//                    //MST_Pohoda.LoadProdejImportResponseXML(Path.GetFileName(file));

		//                    if (pom != "OK")
		//                        throw new Exception(pom);
		//                }
		//                catch (Exception ex)
		//                {
		//                    Log.writeErrorLog("objbezodb c." + i + ex.Message);
		//                }
		//            }

		//        }
		//        else
		//        { //defaultni funkcionalita....

		//            string filename = "";
		//            filename = FilenameComposeTypDoklad(typDoklad);

		//            if (!XML.MST_Pohoda.CreateProdejImportXML(prodejdatads.CZMST_DI, filename, davka.ID.ToString(), "", uzivatel, idCizimena, typDoklad, string.Empty))
		//                return null;

		//            bool saveToDB = true;

		//            pom = XML.MST_Pohoda.ZpracovaniUlozeniVsechDatDoDB(filename, saveToDB, new Fask.Server.Interfaces.Classes.Objednavka(), uzivatel);
		//            if (pom != "OK")
		//                throw new Exception(pom);
		//        }

		//        // \TODO : vratit nejaky rozumny vysledek ... !!!
		//        // PREDELAT !!!
		//        //MST_Pohoda.LoadProdejImportResponseXML(Path.GetFileName(filename), davka.ID ?? 0);

		//        so.Write("Data imported");
		//        so.SetOK();
		//    }
		//    catch (Exception ex)
		//    {
		//        so.Exception = true;
		//        so.Write(ex.Message);
		//        throw ex;
		//    }

		//    return so;
		//}

		#endregion

		#region IProdej Members Prodej_Process

		public StatusObject Prodej_Process(Fask.Server.Interfaces.Classes.Davka davka, Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.User uzivatel, Fask.DataSets.ProdejData data)
		{
            Globals_V1.LoadConfiguration();
			//string guidDavka = data.CZMST_DEH[0].GUID.ToString();
			string guidDavka = string.Empty;
			if (data.CZMST_DEH.Count > 0)
				guidDavka = data.CZMST_DEH[0].GUID.ToString();
			else
				guidDavka = Guid.NewGuid().ToString();
			//string filePath = Path.Combine(Properties.Settings.Default.StatusObjectsDirectory, guidDavka);
			string filePath = Path.Combine(Fask.MyPath.Path.RootPath, Path.Combine(Globals_V1.Konfigurace.Sdilene[0].StatusObjectsDirectory, guidDavka));

			StatusObject so = new StatusObject(filePath);

			//zjistit zda soubor s danym guid existuje
			if (File.Exists(filePath))
			{ //soubor jiz existuje
				so = StatusObject.Load(filePath);
				if (!so.Exception)
					return so;
			}

			/*FileStream fs = new FileStream(dstFile, FileMode.Open, FileAccess.Read, FileShare.Read);
			 byte[] dataProdej = new byte[(new FileInfo(dstFile)).Length];
			 fs.Read(dataProdej, 0, dataProdej.Length);
			 fs.Close();
			 fs = null;*/
			SqlConnection connect = null;
			SqlTransaction iTrans1 = null;

			Doklad typDoklad = null;
			string pom = string.Empty;
			string idCizimena = string.Empty;

			try
			{
				pom = Globals_V1.LoadConfiguration();

				if (pom != "OK")
					return null;


				#region Online validace dat vuči pohode

				#region TypDokladu

				Fask.Rady.DS_Rady.FASK_RADYRow row = null;

				string skladID = data.CZMST_DI[0].SKL_ID.Trim();
				string dokladID = data.CZMST_DI[0].DOC_ID.Trim();
				string dokladID2 = data.CZMST_DI[0].DOC_ID2.Trim();

				try
				{
					Fask.Rady.NumericalSeries NS = new Fask.Rady.NumericalSeries(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
					row = NS.GetRadaRow(Fask.Rady.TypDB.SQL, Fask.Rady.Modul.Pro, skladID, string.Empty, false, dokladID, dokladID2);
				}
				catch (Exception ex)
				{
					Fask.Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML", " Rada,dotaženi", ex);
				}

				typDoklad = new Doklad(row);


				#endregion

				if ((typDoklad.Kontrola_Disponability) && (typDoklad.Import_Doklad_IS))
				{

					Fask.POHODA.Disponibility.StatusInfo info = Database.Pohoda.Prodej_Validace(data);

					if (info.ID != 0)
					{
						//ve je OK a mužeme pokračovat
						throw new Exception(info.Description);

					}
				}

				#endregion

				so.Write("connection");
				connect = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
				bool allowInsertData = true;
				connect.Open();

				SqlCommand xselect = new SqlCommand("Select Count(*) as number from " + TABLE_CZMST_DI + " where countentries=" + davka.ID, connect);
				object result = xselect.ExecuteScalar();
				if (result != null && ((int)result) > 0)
				{
					allowInsertData = false;
				}

				if (allowInsertData)
				{
					//Probehne ulozeni dat
					so.Write("insert to db");
					iTrans1 = connect.BeginTransaction();


					#region ulozeni do DIH

					//Database.Prodej2.Update_CZMST_DIH(data.CZMST_DIH.Select(null, null, DataViewRowState.Added), connect, iTrans1);

					DataRow[] rowsToInsert;

					// běžná větev – jsou Added řádky
					if (data != null && data.CZMST_DIH != null)
					{
						rowsToInsert = data.CZMST_DIH.Select(null, null, DataViewRowState.Added);

						if (rowsToInsert.Length == 0 && davka.ID.HasValue)
						{
							var tmpTable = new Fask.DataSets.ProdejData.CZMST_DIHDataTable();
							var r = tmpTable.NewCZMST_DIHRow();
							r.CountEntries = davka.ID.Value;
							tmpTable.Rows.Add(r);

							rowsToInsert = tmpTable.Select(null, null, DataViewRowState.Added);
						}
					}
					else
					{
						// tabulka je null
						if (davka.ID.HasValue)
						{
							var tmpTable = new Fask.DataSets.ProdejData.CZMST_DIHDataTable();
							var r = tmpTable.NewCZMST_DIHRow();
							r.CountEntries = davka.ID.Value;
							tmpTable.Rows.Add(r);

							rowsToInsert = tmpTable.Select(null, null, DataViewRowState.Added);
						}
						else
						{
							rowsToInsert = new DataRow[0];
						}
					}

					// volání update jen když má smysl
					if (rowsToInsert.Length > 0)
					{
						Database.Prodej2.Update_CZMST_DIH(rowsToInsert, connect, iTrans1);
					}




					#endregion


					Database.Prodej2.Update_CZMST_DI(data.CZMST_DI.Select(null, null, DataViewRowState.Added), connect, iTrans1);

					#region Lokace
					// ulozeni do lokacniho mechanismu, pokud je zapnuty ...

					// v davce je jen jeden typ dokladu, urceni, zdali je lokacni mechanismus zapnuty podle tohoto typu dokladu
					if (data.CZMST_DI.Count > 0)
					{
						// nacteni dat typu dokladu
						string commandText092 = "select * from czmst092 where doc_id=@doc_id and doc_id2=@doc_id2";
						SqlCommand command092 = new SqlCommand(commandText092, connect, iTrans1);
						command092.Parameters.Add(new SqlParameter("@doc_id", data.CZMST_DI.First().DOC_ID));
						command092.Parameters.Add(new SqlParameter("@doc_id2", data.CZMST_DI.First().DOC_ID2));
						SqlDataAdapter adapter = new SqlDataAdapter();
						adapter.SelectCommand = command092;
						Fask.DataSets.TypDokladu dstypdokladu = new Fask.DataSets.TypDokladu();
						adapter.Fill(dstypdokladu, dstypdokladu.CZMST092.TableName);


						if (dstypdokladu.CZMST092.Count > 0 && !dstypdokladu.CZMST092.First().Iscfg_lok_mechNull() && dstypdokladu.CZMST092.First().cfg_lok_mech > 0)
						{
							Fask.DataSets.TypDokladu.CZMST092Row _typdokladu = dstypdokladu.CZMST092.First();
							foreach (Fask.DataSets.ProdejData.CZMST_DIRow dirow in data.CZMST_DI)
							{
								string pohyb_type = _typdokladu.Iscfg_lok_mech_pohyb_typeNull() ? string.Empty : _typdokladu.cfg_lok_mech_pohyb_type;
								Fask.Server.Interfaces.Lokace.TypeOfRecord recordType = (Fask.Server.Interfaces.Lokace.TypeOfRecord)Enum.Parse(typeof(Fask.Server.Interfaces.Lokace.TypeOfRecord), pohyb_type, true);

								string countCommandText = "select count(*) from " + TABLE_CZMST_SKLADLOKACE_STAVPOHYB + " where guid=@guid";
								SqlCommand countCommand = new SqlCommand(countCommandText, connect, iTrans1);
								countCommand.Parameters.Clear();
								countCommand.Parameters.AddWithValue("@guid", dirow.guid);

								int guidcount = (int)countCommand.ExecuteScalar();
								// \TODO: Co kdyz je jiny recordtype??
								if (
									((guidcount % 2 == 0) && recordType == Fask.Server.Interfaces.Lokace.TypeOfRecord.P) ||
									((guidcount % 2 == 0) && recordType == Fask.Server.Interfaces.Lokace.TypeOfRecord.V) ||
									((guidcount % 4 == 0) && recordType == Fask.Server.Interfaces.Lokace.TypeOfRecord.D)
									)
								{

									var nazev = Database.Spolecne.GET_ITEMDESC_by_ITEMCODE(dirow.ITEMNMBR);

									DateTime dtnow = DateTime.Now;
									Fask.Server.Interfaces.Lokace.LokacePohyb pohybrow = new Fask.Server.Interfaces.Lokace.LokacePohyb();
									pohybrow.ITEMNMBR = dirow.ITEMNMBR;
									pohybrow.DOCUMENT_NUMBER = _typdokladu.doc_id;      // pokud je prijem, vydej dle predlohy, bude obsahovat hodnotu SOPNUMBE(PONUMBE) (hodnoty cisla dokladu IS), pokud prodej, tak doc_id
									pohybrow.POHYB_TYPE = recordType;
									pohybrow.POHYB_SRC = "R";
									pohybrow.SOURCE = "S";      // doplneni ze serveru ...
									pohybrow.QTYSHPPD = dirow.QTYSHPPD;
									pohybrow.SERLTNUM = dirow.SERLTNUM;
									pohybrow.SKL_ID_SRC = dirow.IsSKL_IDNull() ? string.Empty : dirow.SKL_ID;
									pohybrow.SKL_ID_DST = dirow.IsSKL_ID_DESTNull() ? string.Empty : dirow.SKL_ID_DEST;
									pohybrow.LOCNCODE_SRC = dirow.IsLOCNCODENull() ? string.Empty : dirow.LOCNCODE;
									pohybrow.LOCNCODE_DST = dirow.IsLOCNCODEDESTNull() ? string.Empty : dirow.LOCNCODEDEST;
									pohybrow.UserID = dirow.USER_ID;
									pohybrow.TermID = terminal.ID;
									pohybrow.guid = dirow.guid;
									pohybrow.Expiration = dirow.IsEXPIRACENull() ? (DateTime?)null : dirow.EXPIRACE;
									pohybrow.ITEMDESC = nazev;   // dotahnout nazev??  5.5.2021 TaD konečne se dotahuje nazev
									pohybrow.CountEntries = dirow.CountEntries;
									pohybrow.dateeveS = dtnow;
									if (dirow.IsDATEDONENull() || dirow.IsTIMEDONENull())
										pohybrow.dateeveS = dtnow;
									else
										pohybrow.dateeveT = DateTime.ParseExact(dirow.DATEDONE + " " + dirow.TIMEDONE, "yyyyMMdd HHmmss", System.Globalization.CultureInfo.InvariantCulture);

									Lokace_MoveItem(pohybrow, new SqlCommand(), connect, iTrans1, new SqlDataAdapter());
								}
							}
						}
					}

					#endregion

					if (iTrans1 != null)
						iTrans1.Commit();
				}
			}
			catch (Exception ex)
			{
				if (iTrans1 != null)
					iTrans1.Rollback();

				so.Exception = true;
				so.Write(ex.Message);

				throw ex;
			}
			finally
			{
				try
				{
					if (connect != null && (connect.State == System.Data.ConnectionState.Open))
						connect.Close();
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}

		



			#region Reakce na typ dokladu after process

			#region Action after data processed
			bool aDP_Action_Asynch = Globals_V1.Konfigurace.Prodej[0].AfterDataProcessed_Action_Asynchronous;
			if (aDP_Action_Asynch)
			{
				System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(Prodej_AfterProcessedActionAsync));
				thread.Start(davka);
			}
			else
			{
				if (!Prodej_AfterProcessedAction(davka))
				{
					so.Exception = true;
					so.Write("chyba");
					return so;
				}
			}
			#endregion

			#region 22.11.2018 Novy spusob importu

			try
			{
				if (typDoklad.Import_Doklad_IS)
				{
					Fask.DataSets.ProdejData dt_di = null;

					if (Globals_V1.Konfigurace.Prodej[0].GrupujDataImport)
						dt_di = Database.Prodej2.GETDATA_CZMSTDI_DS_GroupBy_CountEntries(davka.ID);
					else
						dt_di = Database.Prodej2.GETDATA_CZMSTDI_DS(davka.ID);



					if (dt_di.CZMST_DI.Count > 0)
					{
						so.Write("Generovani Dokladu");
						string vysledek = this.Prodej_Import_to_IS(davka, dt_di, uzivatel);

						if (vysledek != "OK")
						{
							so.StatusText = vysledek;
							so.Exception = true;
							so.Write();
							return so;
						}
					}
				}
			}
			catch (Exception exImport)
			{
				so.Exception = true;
				so.Write(exImport.Message);
				return so;
			}

			#endregion

			#endregion Reakce na typ dokladu after process

			
			so.SetOK();
			so.Write();

			return so;
		}

		#endregion

		#region IProdej Members Prodej_AfterProcessedActionAsync

		public void Prodej_AfterProcessedActionAsync(object davka)
        {
            try
            {
                Fask.Server.Interfaces.Classes.Davka d = (Fask.Server.Interfaces.Classes.Davka)davka;
                Prodej_AfterProcessedAction(d);
            }
            catch (Exception ex)
            {
                throw ex;
            }
		}

		#endregion

		#region IProdej Members Prodej_AfterProcessedAction

		public bool Prodej_AfterProcessedAction(Fask.Server.Interfaces.Classes.Davka davka)
        {
            try
            {
                Globals_V1.LoadConfiguration();
                string aDP_Action = Globals_V1.Konfigurace.Prodej[0].AfterDataProcessed_Action;
                string aDP_Action_P1 = Globals_V1.Konfigurace.Prodej[0].AfterDataProcessed_Action_P1;
                string aDP_Action_P2 = Globals_V1.Konfigurace.Prodej[0].AfterDataProcessed_Action_P2;
                if (aDP_Action.Length != 0)
                {
                    Routines.AfterProcessAction.Execute(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB, TABLE_CZMST_DI, (int)davka.ID, aDP_Action, aDP_Action_P1, aDP_Action_P2, 120);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
		}

		#endregion

		#region IProdej Members Prodej_AfterProcessedAction

		bool Fask.Server.Interfaces.Prodej.IProdej.Prodej_AfterProcessedAction(Fask.Server.Interfaces.Classes.Davka davka)
        {           
            //throw new NotImplementedException();
            return true;
		}
		
		#endregion

		#region IProdej Members Over_Pohyb

		public Fask.Server.Interfaces.Classes.StatusOverPohyb Over_Pohyb(Fask.Server.Interfaces.Classes.ProdejPohyb ph)
        {
            throw new NotImplementedException();
		}
		
		#endregion

		#region IProdej Members Prodej_Online_OverLokace

		public Fask.Server.Interfaces.Classes.StatusOverLokace Prodej_Online_OverLokace(string itemnmbr, string serltnum, DateTime? Expirace, string locncode, string skl_id, decimal qtyshppd, string doc_id, Fask.Server.Interfaces.Classes.TYPLokace locationType, Fask.Server.Interfaces.Lokace.TypeOfRecord recordType)
        {
            return Lokace_OverLokace(itemnmbr, serltnum, locncode, skl_id, qtyshppd, doc_id, locationType, recordType);
		}
		
		#endregion

		#region IProdej Members Prodej_Online_GetMaterial

		public Fask.Server.Interfaces.DataSets.Location Prodej_Online_GetMaterial(string itemnmbr, string skl_id, string serltnum, string doc_id, bool ShowEmpty)
        {
            return Lokace_ShowMaterial(itemnmbr, serltnum, skl_id, null, ShowEmpty);
		}
		
		#endregion

		#region IProdej Members Prodej_ProcessTiskSoupis

		public StatusResult Prodej_ProcessTiskSoupis(Fask.DataSets.ProdejData data, out Fask.Server.Interfaces.DataSets.DSValues dataHeader, out List<Fask.Server.Interfaces.DataSets.DSValues> dataRowList, out Fask.Server.Interfaces.DataSets.DSValues dataFooter)
        {
            throw new NotImplementedException();
		}
		
		#endregion

		#region IProdej Members Prodej_Import_Pohoda_XML

		public string Prodej_Import_to_IS(Davka davka, Fask.DataSets.ProdejData data, Fask.Server.Interfaces.Classes.User uzivatel)
		{
			#region TaD XML soubor, 10.4.2019

			//Doklad typDoklad = null;
			//string dokladID = data.CZMST_DI[0].DOC_ID.Trim();
			//string dokladID2 = data.CZMST_DI[0].DOC_ID2.Trim();
			//string skladID = data.CZMST_DI[0].SKL_ID.Trim();

			//for (int i = 0; i < Globals.listDokladu.Count; i++)
			//{
			//    if ((dokladID == Globals.listDokladu[i].docid) && (dokladID2 == Globals.listDokladu[i].docid2) && (skladID == Globals.listDokladu[i].sklid))
			//    {
			//        typDoklad = Globals.listDokladu[i];
			//        break;
			//    }
			//}

			//Classes.TypDokladu td = (Classes.TypDokladu)Enum.Parse(typeof(Classes.TypDokladu), typDoklad.funkce, true);
			
			#endregion

			#region TaD SQL DB , 10.4.2019


			#region TypDokladu

			Classes.TypDokladu td = Classes.TypDokladu.Unknow;
			Fask.Rady.DS_Rady.FASK_RADYRow row = null;
			Doklad typDoklad = null;
			Fask.Rady.NumericalSeries NS = null;

			string skladID = data.CZMST_DI[0].SKL_ID.Trim();
			string dokladID = data.CZMST_DI[0].DOC_ID.Trim();
			string dokladID2 = data.CZMST_DI[0].DOC_ID2.Trim();

			try
			{
                Globals_V1.LoadConfiguration();
				NS = new Fask.Rady.NumericalSeries(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
				row = NS.GetRadaRow(Fask.Rady.TypDB.SQL, Fask.Rady.Modul.Pro, skladID, string.Empty, false, dokladID, dokladID2);

				td = (Classes.TypDokladu)Enum.Parse(typeof(Classes.TypDokladu), row.Modul_Funkce, true);
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML", " Rada,dotaženi pro DOC_ID:  " + dokladID, ex);
				
				if(NS == null)
					Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "Fask.ModulePohodaXML", " Rada,objekt NS", " Objekt NS is NULL");

				if (row == null)
					Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "Fask.ModulePohodaXML", " Rada,objekt row", " Objekt row is NULL");

				if (row.Modul_Funkce == null)
					Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "Fask.ModulePohodaXML", " Rada,objekt Modul_Funkce", " Objekt Modul_Funkce is NULL");

			}

			typDoklad = new Doklad(row);


			#endregion


			////string funkce;
			//Fask.Rady.DS_Rady.FASK_RADYRow row = null;

			//string skladID = data.CZMST_DI[0].SKL_ID.Trim();
			//string dokladID = data.CZMST_DI[0].DOC_ID.Trim();
			//string dokladID2 = data.CZMST_DI[0].DOC_ID2.Trim();

			//int? idsradadokladu = null;
			//try
			//{
			//    Fask.Rady.NumericalSeries NS = new Fask.Rady.NumericalSeries(Globals.ConnectionString);
			//    //idsradadokladu = NS.GetCiselnaRada_ID(Fask.Rady.TypDB.SQL, Fask.Rady.Modul.Pro, skladID, string.Empty, false, dokladID, dokladID2, null);
			//    //funkce = NS.GetFunkceRada(Fask.Rady.TypDB.SQL, Fask.Rady.Modul.Pro, skladID, string.Empty, false, dokladID, dokladID2);
			//    row = NS.GetRadaRow(Fask.Rady.TypDB.SQL, Fask.Rady.Modul.Pro, skladID, string.Empty, false, dokladID, dokladID2);


				
			//}
			//catch (Exception ex)
			//{
			//    Log.writeErrorLog("Fask.ModulePohodaXML", " Rada", ex.Message + "\n" + ex.StackTrace);
			//}

			// = new Doklad(row);

			
			#endregion


			string filename = Classes.Prodej2.FilenameComposeTypDoklad(td);

			string Status = "OK";

			switch (td)
			{
				case Classes.TypDokladu.vyd:
					Status = Prodej_Import_Vyd_Pohoda(filename, data, typDoklad, uzivatel);
					break;
				case Classes.TypDokladu.pri:
					Status = Prodej_Import_Pri_Pohoda(filename, data, typDoklad, uzivatel);
					Prodej_UpdateVC_Pohoda(data);
					break;
				case Classes.TypDokladu.pro:
					Status = Prodej_Import_Pro_Pohoda(filename, data, typDoklad, uzivatel);
					break;
				case Classes.TypDokladu.objv:
					Status = Prodej_Import_Objv_Pohoda(filename, data, typDoklad, uzivatel);
					break;
				case Classes.TypDokladu.objvcm:
					Status = Prodej_Import_Objvcm_Pohoda(filename, data, typDoklad, uzivatel);
					break;
				case Classes.TypDokladu.objvm:
					Status = Prodej_Import_Objvm_Pohoda(filename, data, typDoklad, uzivatel);
					break;
				case Classes.TypDokladu.objbezodb:
					Status = Prodej_Import_Objbezodb_Pohoda(filename, data, typDoklad, uzivatel);
					break;
				case Classes.TypDokladu.objp:
					Status = Prodej_Import_Objp_Pohoda(filename, data, typDoklad, uzivatel);
					break;
				case Classes.TypDokladu.objpcm:
					Status = Prodej_Import_Objpcm_Pohoda(filename, data, typDoklad, uzivatel);
					break;
				case Classes.TypDokladu.objpm:
					Status = Prodej_Import_Objpm_Pohoda(filename, data, typDoklad, uzivatel);
					break;
				case Classes.TypDokladu.expedice:
					Status = Prodej_Import_Expedice_Pohoda(filename, data, typDoklad, uzivatel);
					break;
				case Classes.TypDokladu.pre:
					Status = Prodej_Import_Pre_Pohoda(filename, data, typDoklad, uzivatel);
					break;
				case Classes.TypDokladu.fv:
					Status = Prodej_Import_FV_Pohoda(filename, data, typDoklad, uzivatel);
					break;
				case Classes.TypDokladu.vydvr:
					Status = Prodej_Import_Vyd_Vratka_Pohoda(filename, data, typDoklad, uzivatel);
					break;
				default:
					break;
			}

			return Status;
		}

		public string Prodej_Import_to_IS(Davka davka, Fask.DataSets.ProdejData data, Fask.Server.Interfaces.Classes.User uzivatel, Fask.DataSets.ProdejData.CZMST_DIHDataTable dt_DIH)
		{
			#region TaD XML soubor, 10.4.2019

			//Doklad typDoklad = null;
			//string dokladID = data.CZMST_DI[0].DOC_ID.Trim();
			//string dokladID2 = data.CZMST_DI[0].DOC_ID2.Trim();
			//string skladID = data.CZMST_DI[0].SKL_ID.Trim();

			//for (int i = 0; i < Globals.listDokladu.Count; i++)
			//{
			//    if ((dokladID == Globals.listDokladu[i].docid) && (dokladID2 == Globals.listDokladu[i].docid2) && (skladID == Globals.listDokladu[i].sklid))
			//    {
			//        typDoklad = Globals.listDokladu[i];
			//        break;
			//    }
			//}

			//Classes.TypDokladu td = (Classes.TypDokladu)Enum.Parse(typeof(Classes.TypDokladu), typDoklad.funkce, true);

			#endregion

			#region TaD SQL DB , 10.4.2019


			#region TypDokladu

			Classes.TypDokladu td = Classes.TypDokladu.Unknow;
			Fask.Rady.DS_Rady.FASK_RADYRow row = null;
			Doklad typDoklad = null;
			Fask.Rady.NumericalSeries NS = null;

			string skladID = data.CZMST_DI[0].SKL_ID.Trim();
			string dokladID = data.CZMST_DI[0].DOC_ID.Trim();
			string dokladID2 = data.CZMST_DI[0].DOC_ID2.Trim();

			try
			{
				Globals_V1.LoadConfiguration();
				NS = new Fask.Rady.NumericalSeries(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
				row = NS.GetRadaRow(Fask.Rady.TypDB.SQL, Fask.Rady.Modul.Pro, skladID, string.Empty, false, dokladID, dokladID2);

				//td = (Classes.TypDokladu)Enum.Parse(typeof(Classes.TypDokladu), row.Modul_Funkce, true);

				var funkceRaw = row?.Modul_Funkce;
				var funkce = funkceRaw?.Trim();

				Fask.Logging.ExceptionHandler2.Handle(
					Logging.LogLevel.Info,
					"Fask.ModulePohodaXML",
					"funkce debug",
					$"raw='{funkceRaw}', rawLen={funkceRaw?.Length}, trim='{funkce}', trimLen={funkce?.Length}, bytes={BitConverter.ToString(System.Text.Encoding.UTF8.GetBytes(funkceRaw ?? ""))}"
				);


				funkce = (row?.Modul_Funkce ?? "")
	.Replace("\0", "")
	.Replace("\uFEFF", "") // BOM
	.Replace("\u200B", "") // zero-width space
	.Trim();


				if (!Enum.TryParse(funkce, ignoreCase: true, out Classes.TypDokladu tdParsed))
				{
					tdParsed = Classes.TypDokladu.Unknow;
					Fask.Logging.ExceptionHandler2.Handle(
						Logging.LogLevel.Error,
						"Fask.ModulePohodaXML",
						"Enum parse failed",
						$"Modul_Funkce='{funkce}'"
					);
				}

				td = tdParsed;


				Fask.Logging.ExceptionHandler2.Handle(
	Logging.LogLevel.Info,
	"Fask.ModulePohodaXML",
	"TypDokladu resolved",
	$"Modul_Funkce='{row?.Modul_Funkce}', td='{td}', tdInt={(int)td}"
);


			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML",
					"Rada,dotaženi pro DOC_ID: " + dokladID, ex);

				if (NS == null)
					Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "Fask.ModulePohodaXML", "Rada,objekt NS", "Objekt NS is NULL");

				if (row == null)
					Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "Fask.ModulePohodaXML", "Rada,objekt row", "Objekt row is NULL");
				else if (row.Modul_Funkce == null)
					Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "Fask.ModulePohodaXML", "Rada,objekt Modul_Funkce", "Objekt Modul_Funkce is NULL");
			}


			typDoklad = new Doklad(row);


			#endregion


			////string funkce;
			//Fask.Rady.DS_Rady.FASK_RADYRow row = null;

			//string skladID = data.CZMST_DI[0].SKL_ID.Trim();
			//string dokladID = data.CZMST_DI[0].DOC_ID.Trim();
			//string dokladID2 = data.CZMST_DI[0].DOC_ID2.Trim();

			//int? idsradadokladu = null;
			//try
			//{
			//    Fask.Rady.NumericalSeries NS = new Fask.Rady.NumericalSeries(Globals.ConnectionString);
			//    //idsradadokladu = NS.GetCiselnaRada_ID(Fask.Rady.TypDB.SQL, Fask.Rady.Modul.Pro, skladID, string.Empty, false, dokladID, dokladID2, null);
			//    //funkce = NS.GetFunkceRada(Fask.Rady.TypDB.SQL, Fask.Rady.Modul.Pro, skladID, string.Empty, false, dokladID, dokladID2);
			//    row = NS.GetRadaRow(Fask.Rady.TypDB.SQL, Fask.Rady.Modul.Pro, skladID, string.Empty, false, dokladID, dokladID2);



			//}
			//catch (Exception ex)
			//{
			//    Log.writeErrorLog("Fask.ModulePohodaXML", " Rada", ex.Message + "\n" + ex.StackTrace);
			//}

			// = new Doklad(row);


			#endregion


			string filename = Classes.Prodej2.FilenameComposeTypDoklad(td);

			string Status = "OK";

			Fask.Logging.ExceptionHandler2.Handle(
	Logging.LogLevel.Info,
	"Fask.ModulePohodaXML",
	"TypDokladu resolved",
	$"DOC_ID='{dokladID}', DOC_ID2='{dokladID2}', SKL_ID='{skladID}', Modul_Funkce='{row?.Modul_Funkce}', td='{td}'"
);


			switch (td)
			{
				case Classes.TypDokladu.vyd:
					Status = Prodej_Import_Vyd_Pohoda(filename, data, typDoklad, uzivatel);
					break;
				case Classes.TypDokladu.pri:
					Status = Prodej_Import_Pri_Pohoda(filename, data, typDoklad, uzivatel);
					Prodej_UpdateVC_Pohoda(data);
					break;
				case Classes.TypDokladu.pro:
					Status = Prodej_Import_Pro_Pohoda(filename, data, typDoklad, uzivatel);
					break;
				case Classes.TypDokladu.objv:
					Status = Prodej_Import_Objv_Pohoda(filename, data, typDoklad, uzivatel);
					break;
				case Classes.TypDokladu.objvcm:
					Status = Prodej_Import_Objvcm_Pohoda(filename, data, typDoklad, uzivatel);
					break;
				case Classes.TypDokladu.objvm:
					Status = Prodej_Import_Objvm_Pohoda(filename, data, typDoklad, uzivatel);
					break;
				case Classes.TypDokladu.objbezodb:
					Status = Prodej_Import_Objbezodb_Pohoda(filename, data, typDoklad, uzivatel);
					break;
				case Classes.TypDokladu.objp:
					Status = Prodej_Import_Objp_Pohoda(filename, data, typDoklad, uzivatel);
					break;
				case Classes.TypDokladu.objpcm:
					Status = Prodej_Import_Objpcm_Pohoda(filename, data, typDoklad, uzivatel);
					break;
				case Classes.TypDokladu.objpm:
					Status = Prodej_Import_Objpm_Pohoda(filename, data, typDoklad, uzivatel);
					break;
				case Classes.TypDokladu.expedice:
					Status = Prodej_Import_Expedice_Pohoda(filename, data, typDoklad, uzivatel);
					break;
				case Classes.TypDokladu.pre:
					Status = Prodej_Import_Pre_Pohoda(filename, data, typDoklad, uzivatel);
					break;
				case Classes.TypDokladu.fv:
					Status = Prodej_Import_FV_Pohoda(filename, data, typDoklad, uzivatel);
					break;
				case Classes.TypDokladu.vydvr:
					Status = Prodej_Import_Vyd_Vratka_Pohoda(filename, data, typDoklad, uzivatel);
					break;
				default:
					break;
			}

			return Status;
		}

		private void Prodej_UpdateVC_Pohoda(ProdejData data)
        {
			if (data.CZMST_DI.Count > 0)
			{

				System.Data.OleDb.OleDbTransaction transPOH = null;
				System.Data.OleDb.OleDbConnection connPOH = null;

				try
				{
					using (connPOH = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB))
					{
						connPOH.Open();
						transPOH = connPOH.BeginTransaction();

						foreach (Fask.DataSets.ProdejData.CZMST_DIRow pol in data.CZMST_DI)
						{
							if (!pol.IsAttributeToSNNull() && !string.IsNullOrEmpty(pol.AttributeToSN))
							{
								Database.Pohoda.Update_SKzVC(connPOH, transPOH, pol.ITEMNMBR, pol.SERLTNUM, pol.AttributeToSN);
							}

							if (!pol.IsEXPIRACENull())
							{
								Database.Pohoda.Update_SKzVC(connPOH, transPOH, pol.ITEMNMBR, pol.SERLTNUM, pol.EXPIRACE);
							}
						}

						if (transPOH != null)
							transPOH.Commit();
					}

				}
				catch (Exception ex)
				{
					if (transPOH != null)
						transPOH.Rollback();

					throw ex;
				}
			}
		}

        #region Import Metody

        /// <summary>
        /// Metoda pro import Prevodka
        /// </summary>
        /// <param name="filename">Cesta k requestu</param>
        /// <param name="data">data z CZMST_DI</param>
        /// <param name="uzivatel">Uzivatel</param>
        /// <returns>bud OK anebo chybu</returns>
        private string Prodej_Import_Pre_Pohoda(string filename, Fask.DataSets.ProdejData data, Doklad typDoklad, User uzivatel)
		{
			try
			{

				string pom = string.Empty;

				pom = Globals_V1.LoadConfiguration();

				if (pom != "OK")
					return pom;

				if (!Classes.Prodej2.CreateRequest_Pre_XML(filename, "FASK Import - Pre XML", data.CZMST_DI, typDoklad))
					return "CHYBA";

				string responsefilename;
				if (!XML.PohodaComunication.Communicate(filename, out responsefilename))
				{
					throw new Exception("Komunikace s Pohodou se nezdařila");
				}

				pom = Classes.Prodej2.LoadResponse_Pre_XML(responsefilename, uzivatel);

				return pom;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}


		/// <summary>
		/// Metoda pro import Ëxpedice
		/// </summary>
		/// <param name="filename">Cesta k requestu</param>
		/// <param name="data">data z CZMST_DI</param>
		/// <param name="uzivatel">Uzivatel</param>
		/// <returns>bud OK anebo chybu</returns>
		private string Prodej_Import_Expedice_Pohoda(string filename, Fask.DataSets.ProdejData data, Doklad typDoklad, User uzivatel)
		{
			try
			{

				string pom = string.Empty;

				pom = Globals_V1.LoadConfiguration();

				if (pom != "OK")
					return pom;

				if (!Classes.Prodej2.CreateRequest_Expedice_XML(filename, "FASK Import - Expedice XML", data.CZMST_DI, typDoklad))
					return "CHYBA";

				string responsefilename;
				if (!XML.PohodaComunication.Communicate(filename, out responsefilename))
				{
					throw new Exception("Komunikace s Pohodou se nezdařila");
				}

				pom = Classes.Prodej2.LoadResponse_Expedice_XML(filename, uzivatel);

				return pom;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Metoda pro import Objpm (Objednavka prijata m???)
		/// </summary>
		/// <param name="filename">Cesta k requestu</param>
		/// <param name="data">data z CZMST_DI</param>
		/// <param name="uzivatel">Uzivatel</param>
		/// <returns>bud OK anebo chybu</returns>
		private string Prodej_Import_Objpm_Pohoda(string filename, Fask.DataSets.ProdejData data, Doklad typDoklad, User uzivatel)
		{
			try
			{

				string pom = string.Empty;

				pom = Globals_V1.LoadConfiguration();

				if (pom != "OK")
					return pom;

				if (!Classes.Prodej2.CreateRequest_Objpm_XML(filename, "FASK Import - Objpm XML", data.CZMST_DI, typDoklad))
					return "CHYBA";

				string responsefilename;
				if (!XML.PohodaComunication.Communicate(filename, out responsefilename))
				{
					throw new Exception("Komunikace s Pohodou se nezdařila");
				}

				pom = Classes.Prodej2.LoadResponse_Objpm_XML(filename, uzivatel);

				return pom;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Metoda pro import Objpcm (Objednavka prijata cizi mena)
		/// </summary>
		/// <param name="filename">Cesta k requestu</param>
		/// <param name="data">data z CZMST_DI</param>
		/// <param name="uzivatel">Uzivatel</param>
		/// <returns>bud OK anebo chybu</returns>
		private string Prodej_Import_Objpcm_Pohoda(string filename, Fask.DataSets.ProdejData data, Doklad typDoklad, User uzivatel)
		{
			try
			{

				string pom = string.Empty;

				pom = Globals_V1.LoadConfiguration();

				if (pom != "OK")
					return pom;

				if (!Classes.Prodej2.CreateRequest_Objpcm_XML(filename, "FASK Import - Objpcm XML", data.CZMST_DI, typDoklad))
					return "CHYBA";

				string responsefilename;
				if (!XML.PohodaComunication.Communicate(filename, out responsefilename))
				{
					throw new Exception("Komunikace s Pohodou se nezdařila");
				}

				pom = Classes.Prodej2.LoadResponse_Objpcm_XML(filename, uzivatel);

				return pom;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Metoda pro import Objp (Objednavka prijata)
		/// </summary>
		/// <param name="filename">Cesta k requestu</param>
		/// <param name="data">data z CZMST_DI</param>
		/// <param name="uzivatel">Uzivatel</param>
		/// <returns>bud OK anebo chybu</returns>
		private string Prodej_Import_Objp_Pohoda(string filename, Fask.DataSets.ProdejData data, Doklad typDoklad, User uzivatel)
		{
			try
			{

				string pom = string.Empty;

				pom = Globals_V1.LoadConfiguration();

				if (pom != "OK")
					return pom;

				if (!Classes.Prodej2.CreateRequest_Objp_XML(filename, "FASK Import - Objp XML", data.CZMST_DI, typDoklad))
					return "CHYBA";

				string responsefilename;
				if (!XML.PohodaComunication.Communicate(filename, out responsefilename))
				{
					throw new Exception("Komunikace s Pohodou se nezdařila");
				}

				pom = Classes.Prodej2.LoadResponse_Objp_XML(filename, uzivatel);

				return pom;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Metoda pro import Objbezodb (Objednavka vydana bez odberatele)
		/// </summary>
		/// <param name="filename">Cesta k requestu</param>
		/// <param name="data">data z CZMST_DI</param>
		/// <param name="uzivatel">Uzivatel</param>
		/// <returns>bud OK anebo chybu</returns>
		private string Prodej_Import_Objbezodb_Pohoda(string filename, Fask.DataSets.ProdejData data, Doklad typDoklad, User uzivatel)
		{
			try
			{

				string pom = string.Empty;

				pom = Globals_V1.LoadConfiguration();

				if (pom != "OK")
					return pom;

				if (!Classes.Prodej2.CreateRequest_Objbezodb_XML(filename, "FASK Import - Objbezodb XML", data.CZMST_DI, typDoklad))
					return "CHYBA";

				string responsefilename;
				if (!XML.PohodaComunication.Communicate(filename, out responsefilename))
				{
					throw new Exception("Komunikace s Pohodou se nezdařila");
				}

				pom = Classes.Prodej2.LoadResponse_Objbezodb_XML(filename, uzivatel);

				return pom;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Metoda pro import Objvm (Objednavka vydana m ???)
		/// </summary>
		/// <param name="filename">Cesta k requestu</param>
		/// <param name="data">data z CZMST_DI</param>
		/// <param name="uzivatel">Uzivatel</param>
		/// <returns>bud OK anebo chybu</returns>
		private string Prodej_Import_Objvm_Pohoda(string filename, Fask.DataSets.ProdejData data, Doklad typDoklad, User uzivatel)
		{
			try
			{

				string pom = string.Empty;

				pom = Globals_V1.LoadConfiguration();

				if (pom != "OK")
					return pom;

				if (!Classes.Prodej2.CreateRequest_Objvm_XML(filename, "FASK Import - Objvm XML", data.CZMST_DI, typDoklad))
					return "CHYBA";

				string responsefilename;
				if (!XML.PohodaComunication.Communicate(filename, out responsefilename))
				{
					throw new Exception("Komunikace s Pohodou se nezdařila");
				}

				pom = Classes.Prodej2.LoadResponse_Objvm_XML(filename, uzivatel);

				return pom;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Metoda pro import Objvcm (??Objednavka vydava cizi mena)
		/// </summary>
		/// <param name="filename">Cesta k requestu</param>
		/// <param name="data">data z CZMST_DI</param>
		/// <param name="uzivatel">Uzivatel</param>
		/// <returns>bud OK anebo chybu</returns>
		private string Prodej_Import_Objvcm_Pohoda(string filename, Fask.DataSets.ProdejData data, Doklad typDoklad, User uzivatel)
		{
			try
			{

				string pom = string.Empty;

				pom = Globals_V1.LoadConfiguration();

				if (pom != "OK")
					return pom;

				if (!Classes.Prodej2.CreateRequest_Objvcm_XML(filename, "FASK Import - Objvcm XML", data.CZMST_DI, typDoklad))
					return "CHYBA";

				string responsefilename;
				if (!XML.PohodaComunication.Communicate(filename, out responsefilename))
				{
					throw new Exception("Komunikace s Pohodou se nezdařila");
				}

				pom = Classes.Prodej2.LoadResponse_Objvcm_XML(filename, uzivatel);

				return pom;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Metoda pro import Objv (??Objednavka vydana)
		/// </summary>
		/// <param name="filename">Cesta k requestu</param>
		/// <param name="data">data z CZMST_DI</param>
		/// <param name="uzivatel">Uzivatel</param>
		/// <returns>bud OK anebo chybu</returns>
		private string Prodej_Import_Objv_Pohoda(string filename, Fask.DataSets.ProdejData data, Doklad typDoklad, User uzivatel)
		{
			try
			{

				string pom = string.Empty;

				pom = Globals_V1.LoadConfiguration();

				if (pom != "OK")
					return pom;

				if (!Classes.Prodej2.CreateRequest_Objv_XML(filename, "FASK Import - Objv XML", data.CZMST_DI,typDoklad))
					return "CHYBA";

				string responsefilename;
				if (!XML.PohodaComunication.Communicate(filename, out responsefilename))
				{
					throw new Exception("Komunikace s Pohodou se nezdařila");
				}

				pom = Classes.Prodej2.LoadResponse_Objv_XML(filename, uzivatel);

				return pom;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Metoda pro import Prodejka
		/// </summary>
		/// <param name="filename">Cesta k requestu</param>
		/// <param name="data">data z CZMST_DI</param>
		/// <param name="uzivatel">Uzivatel</param>
		/// <returns>bud OK anebo chybu</returns>
		private string Prodej_Import_Pro_Pohoda(string filename, Fask.DataSets.ProdejData data, Doklad typDoklad, User uzivatel)
		{
			try
			{

				string pom = string.Empty;

				pom = Globals_V1.LoadConfiguration();

				if (pom != "OK")
					return pom;

				if (!Classes.Prodej2.CreateRequest_Pro_XML(filename, "FASK Import - Pro XML", data.CZMST_DI, typDoklad))
					return "CHYBA";

				string responsefilename;
				if (!XML.PohodaComunication.Communicate(filename, out responsefilename))
				{
					throw new Exception("Komunikace s Pohodou se nezdařila");
				}

				pom = Classes.Prodej2.LoadResponse_Pro_XML(filename, uzivatel);

				return pom;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Metoda pro import Vydejky
		/// </summary>
		/// <param name="filename">Cesta k requestu</param>
		/// <param name="data">data z CZMST_DI</param>
		/// <param name="uzivatel">Uzivatel</param>
		/// <returns>bud OK anebo chybu</returns>
		private string Prodej_Import_Vyd_Pohoda(string filename, Fask.DataSets.ProdejData data,Doklad typDoklad, User uzivatel)
		{
			try
			{

				string pom = string.Empty;

				pom = Globals_V1.LoadConfiguration();

				if (pom != "OK")
					return pom;

				if (!Classes.Prodej2.CreateRequest_Vyd_XML(filename, "FASK Import - Vyd XML", data.CZMST_DI,typDoklad))
					return "CHYBA";

				string responsefilename;
				if (!XML.PohodaComunication.Communicate(filename, out responsefilename))
				{
					throw new Exception("Komunikace s Pohodou se nezdařila");
				}

				pom = Classes.Prodej2.LoadResponse_Vyd_XML(filename, uzivatel);

				return pom;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Metoda pro import Vydejky
		/// </summary>
		/// <param name="filename">Cesta k requestu</param>
		/// <param name="data">data z CZMST_DI</param>
		/// <param name="uzivatel">Uzivatel</param>
		/// <returns>bud OK anebo chybu</returns>
		private string Prodej_Import_Vyd_Vratka_Pohoda(string filename, Fask.DataSets.ProdejData data, Doklad typDoklad, User uzivatel)
		{
			try
			{

				string pom = string.Empty;

				pom = Globals_V1.LoadConfiguration();

				if (pom != "OK")
					return pom;

				if (!Classes.Prodej2.CreateRequest_Vydvr_XML(filename, "FASK Import - Vyd vratka XML", data.CZMST_DI, typDoklad))
					return "CHYBA";

				string responsefilename;
				if (!XML.PohodaComunication.Communicate(filename, out responsefilename))
				{
					throw new Exception("Komunikace s Pohodou se nezdařila");
				}

				pom = Classes.Prodej2.LoadResponse_Vyd_Vratka_XML(filename, uzivatel);

				return pom;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}


		/// <summary>
		/// Metoda pro import Prijemky
		/// </summary>
		/// <param name="filename">Cesta k requestu</param>
		/// <param name="data">data z CZMST_DI</param>
		/// <param name="uzivatel">Uzivatel</param>
		/// <returns>bud OK anebo chybu</returns>
		public string Prodej_Import_Pri_Pohoda(string filename, Fask.DataSets.ProdejData data, Doklad typDoklad, Fask.Server.Interfaces.Classes.User uzivatel)
		{
			try
			{

				string pom = string.Empty;

				pom = Globals_V1.LoadConfiguration();

				if (pom != "OK")
					return pom;

				if (!Classes.Prodej2.CreateRequest_Pri_XML(filename, "FASK Import - Pri XML", data.CZMST_DI, typDoklad))
					return "CHYBA";

				string responsefilename;
				if (!XML.PohodaComunication.Communicate(filename, out responsefilename))
				{
					throw new Exception("Komunikace s Pohodou se nezdařila");
				}

				pom = Classes.Prodej2.LoadResponse_Pri_XML(filename, uzivatel);

				return pom;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Metoda pro import Faktury vydane
		/// </summary>
		/// <param name="filename">Cesta k requestu</param>
		/// <param name="data">data z CZMST_DI</param>
		/// <param name="typDoklad">Typ dokladu</param>
		/// <param name="uzivatel">Uzivatel</param>
		/// <returns></returns>
		private string Prodej_Import_FV_Pohoda(string filename, ProdejData data, Doklad typDoklad, User uzivatel)
		{
			try
			{

				string pom = string.Empty;

				pom = Globals_V1.LoadConfiguration();

				if (pom != "OK")
					return pom;

				if (!Classes.Prodej2.CreateRequest_FV_XML(filename, "FASK Import - FV XML", data.CZMST_DI, typDoklad))
					return "CHYBA";

				string responsefilename;
				if (!XML.PohodaComunication.Communicate(filename, out responsefilename))
				{
					throw new Exception("Komunikace s Pohodou se nezdařila");
				}

				pom = Classes.Prodej2.LoadResponse_FV_XML(responsefilename, uzivatel);

				return pom;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		#endregion


		#endregion

		#region IProdej Members Prodej_GetDodavatele_External


		public Fask.Interfaces.DataSets.Odberatele Prodej_GetDodavatele_External(Terminal terminal, Sklad sklad, Item item, List<string> ListCarKod)
		{
			Fask.Interfaces.DataSets.Odberatele ds_o = new Fask.Interfaces.DataSets.Odberatele();
			try
			{

				//System.Data.OleDb.OleDbDataAdapter oledb_da = new System.Data.OleDb.OleDbDataAdapter(
				//"Select * from dbo.FASK_GetDodavatelByCarKody(?,?,?)",
				//Globals.ConnectionStringPohodaDB);

				//oledb_da.SelectCommand.Parameters.AddWithValue("?", terminal.ID);
				//oledb_da.SelectCommand.Parameters.AddWithValue("?", sklad.ID);
				//oledb_da.SelectCommand.Parameters.AddWithValue("?", String.Join(",", ListCarKod.ToArray()));

				//oledb_da.Fill(ds_o, ds_o.CZMST090.TableName);

				#region SQL  1.2.2019 HANIBAL

				System.Data.SqlClient.SqlDataAdapter SQL_da = new System.Data.SqlClient.SqlDataAdapter(
				"Select * from dbo.FASK_GetDodavatelByCarKody(@TerminalID,@SkladID,@CarKod)",
                Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

				SQL_da.SelectCommand.Parameters.AddWithValue("@TerminalID", terminal.ID);
				SQL_da.SelectCommand.Parameters.AddWithValue("@SkladID", sklad.ID);
				SQL_da.SelectCommand.Parameters.AddWithValue("@CarKod", String.Join(",", ListCarKod.ToArray()));

				SQL_da.Fill(ds_o, ds_o.CZMST090.TableName);
				
				#endregion


			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				Logging.ExceptionHandler2.Handle(ds_o);
			}

			return ds_o;

		}

		#endregion

		#region IProdej Members Prodej_GetDodavatele_ExternalByICO


		public Fask.Interfaces.DataSets.Odberatele Prodej_GetDodavatele_ExternalByICO(Terminal terminal, Sklad sklad, string ICO)
		{
			Fask.Interfaces.DataSets.Odberatele odb = new Fask.Interfaces.DataSets.Odberatele();
			Datasets.DatabasePohoda.ADDataTable dataTable = new Datasets.DatabasePohoda.ADDataTable();

			

			Database.Pohoda.AD_FillByICO(dataTable, ICO.Trim());


			int odb_id_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Odberatele.ColumnsInfo_CZMST090["odb_id"].MaxLength;
			int odb_desc_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Odberatele.ColumnsInfo_CZMST090["odb_desc"].MaxLength;
			int odb_carcode_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Odberatele.ColumnsInfo_CZMST090["odb_carcode"].MaxLength;
			int odb_ico_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Odberatele.ColumnsInfo_CZMST090["odb_ico"].MaxLength;
			int mena_ID_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Odberatele.ColumnsInfo_CZMST090["mena_ID"].MaxLength;


			 
			if ((dataTable != null) && (dataTable.Count == 1))
			{
				var item =  dataTable[0];

				string odb_ico = item.IsICONull() ? "" : item.ICO;

				string odb_barcode = item.IsCisloNull() ? "" : item.Cislo;
				string odb_desc = item.IsFirmaNull() ? "" : item.Firma;
				string odb_id = item.ID.ToString();
				string mena_id = "";

				if (odb_id.Length > odb_id_MaxLength)
					odb_id = odb_id.Remove(odb_id_MaxLength);

				if (odb_desc.Length > odb_desc_MaxLength)
					odb_desc = odb_desc.Remove(odb_desc_MaxLength);

				if (odb_barcode.Length > odb_carcode_MaxLength)
					odb_barcode = odb_barcode.Remove(odb_carcode_MaxLength);

				if (odb_ico.Length > odb_ico_MaxLength)
					odb_ico = odb_ico.Remove(odb_ico_MaxLength);

				if (mena_id.Length > mena_ID_MaxLength)
					mena_id = mena_id.Remove(mena_ID_MaxLength);

				odb.CZMST090.AddCZMST090Row(
					odb_id,
					odb_desc,
					"0",
					odb_barcode,
					0,
					odb_ico,
					mena_id, 
					null, null, null, null, null, false, true);

				return odb;

			}
			else
			{
				return null;
			}

		}

		#endregion

		#region IProdej Members Prodej_Disponibilita


		public StatusInfo Prodej_Disponibilita(Disponibilita disponibilita)
		{
			#region V1
			StatusInfo si = null;

			Fask.POHODA.Disponibility.ValidateData dsDisp = new POHODA.Disponibility.ValidateData();


			Fask.POHODA.Disponibility.ValidateData.DataDispRow Row = dsDisp.DataDisp.NewDataDispRow();

			Row.ITEMNMBR = disponibilita.ITEMNMBR;
			Row.SKL_ID = disponibilita.SKL_ID;
			Row.QTY = disponibilita.QTY;
			Row.SetSOPNUMBENull();
			Row.SetORDNull();
			Row.SetSKz_RezerNull();
			Row.SetSKz_StavZNull();
			Row.SetOBJ_RezerNull();

			dsDisp.DataDisp.AddDataDispRow(Row);
			Fask.POHODA.Disponibility.CheckDisp disp = new Fask.POHODA.Disponibility.CheckDisp();
			Fask.POHODA.Disponibility.StatusInfo info = disp.KontrolaDisponibility(dsDisp, Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
			si = new StatusInfo(info.ID, info.Description, info.InnerException, info.Created);

			return si;

			#endregion


		}

		#endregion

		#region IProdej_TiskPOHODA Members TiskPOHODA

		public StatusResult TiskPOHODA(string agenda, int ID_Dokladu, int ID_Sablony,int PocetKopii, string NazevTiskarny)
		{
			StatusResult sr = new StatusResult();
			//Tisk skrz pohodu
			try
			{
				Classes.printAgendaType agendaEnum = (Classes.printAgendaType)Enum.Parse(typeof(Classes.printAgendaType), agenda, true);

				var filename = XML.MST_Pohoda.FilenameCompose_FullPath("Tisk.xml");

				string pom = string.Empty;

				pom = Globals_V1.LoadConfiguration();

				if (pom != "OK")
				{
					sr.SetERROR(pom);
					return sr; ;
				}

				if (!Classes.Prodej2.CreateRequest_TISK_XML(filename, agendaEnum, ID_Dokladu, ID_Sablony, PocetKopii, NazevTiskarny))
				{
					sr.SetERROR("Create request");
					return sr;
				}

				string responsefilename;
				if (!XML.PohodaComunication.Communicate(filename, out responsefilename))
				{
					sr.SetERROR("Komunikace s Pohodou se nezdařila");
					return sr;
				}

				pom = Classes.Prodej2.LoadResponse_TISK_XML(responsefilename);

				if (pom != "OK")
				{
					sr.SetERROR(pom);
					return sr; ;
				}
				else
				{
					sr.SetOK(pom);
					return sr;
				}

			
			}
			catch (Exception ex)
			{
				throw ex;
			}


		}

		#endregion

		#region IProdej Members Online_UniverzalnyDotazNaCokoliv

		public Fask.Server.Interfaces.Classes_OnlineKomunikace.VystupniObjekt Online_UniverzalnyDotazNaCokoliv(Fask.Server.Interfaces.Classes_OnlineKomunikace.VstupniObjekt ObjektIN)
		{

			try
			{

                #region 10.10.2019 HANIBAL

                Globals_V1.LoadConfiguration();

				Fask.Server.Interfaces.Classes_OnlineKomunikace.VystupniObjekt ObjektOUT = new Fask.Server.Interfaces.Classes_OnlineKomunikace.VystupniObjekt();

				try
				{
					System.Data.SqlClient.SqlConnection SQLcon = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
					System.Data.SqlClient.SqlCommand dbCommand = new System.Data.SqlClient.SqlCommand();
					dbCommand.CommandType = CommandType.StoredProcedure;
					dbCommand.Connection = SQLcon;

					if (dbCommand.CommandType == CommandType.StoredProcedure)
					{
						dbCommand.CommandText = "FASK_PrijemGetSkladExpedice";

						var pItemnmbr = dbCommand.Parameters.AddWithValue("@Itemnmbr", ObjektIN.ITEMNMBR.Trim());
						//var pItemnmbr = dbCommand.Parameters.AddWithValue(prijemkadetail_paramname_IDPozlozky, ITEMNMBR);
						var pMnozstviZadane = dbCommand.Parameters.AddWithValue("@MnozstviZadane", ObjektIN.MnozstviZadane);
						var pMnozstviNasnimano = dbCommand.Parameters.AddWithValue("@MnozstviNasnimane", ObjektIN.MnozstviNasnimane);

						var pMnozstviDodavatelePozadovano = dbCommand.Parameters.AddWithValue("@MnozstviDodavatelePozadovano", ObjektOUT.MnozstviDodavatelePozadovano);
						var pMnozstviDodavateleDodano = dbCommand.Parameters.AddWithValue("@MnozstviDodavateleDodano", ObjektOUT.MnozstviDodavateleDodano);
						var pMnozstviDodavateleDodat = dbCommand.Parameters.AddWithValue("@MnozstviDodavateleDodat", ObjektOUT.MnozstviDodavateleDodat);
						var pMnozstviOdberateliPozadovano = dbCommand.Parameters.AddWithValue("@MnozstviOdberateliPozadovano", ObjektOUT.MnozstviOdberateliPozadovano);
						var pMnozstviOdberatelumDodano = dbCommand.Parameters.AddWithValue("@MnozstviOdberatelumDodano", ObjektOUT.MnozstviOdberatelumDodano);
						var pMnozstviOdberatelumDodat = dbCommand.Parameters.AddWithValue("@MnozstviOdberatelumDodat", ObjektOUT.MnozstviOdberatelumDodat);
						var pVysledek = dbCommand.Parameters.AddWithValue("@Vysledek", ObjektOUT.Vysledek);

						pMnozstviDodavatelePozadovano.Direction = ParameterDirection.Output;
						pMnozstviDodavateleDodano.Direction = ParameterDirection.Output;
						pMnozstviDodavateleDodat.Direction = ParameterDirection.Output;
						pMnozstviOdberateliPozadovano.Direction = ParameterDirection.Output;
						pMnozstviOdberatelumDodano.Direction = ParameterDirection.Output;
						pMnozstviOdberatelumDodat.Direction = ParameterDirection.Output;
						pVysledek.Direction = ParameterDirection.Output;

						pItemnmbr.SqlDbType = System.Data.SqlDbType.VarChar;
						pItemnmbr.Precision = 31;
						pItemnmbr.Scale = 0;

						pMnozstviZadane.SqlDbType = System.Data.SqlDbType.Decimal;
						pMnozstviZadane.Precision = 19;
						pMnozstviZadane.Scale = 5;
						pMnozstviZadane.Size = 4;

						pMnozstviNasnimano.SqlDbType = System.Data.SqlDbType.Decimal;
						pMnozstviNasnimano.Precision = 19;
						pMnozstviNasnimano.Scale = 5;
						pMnozstviNasnimano.Size = 4;

						pMnozstviDodavatelePozadovano.SqlDbType = System.Data.SqlDbType.Decimal;
						pMnozstviDodavatelePozadovano.Precision = 19;
						pMnozstviDodavatelePozadovano.Scale = 5;
						pMnozstviDodavatelePozadovano.Size = 4;

						pMnozstviDodavateleDodano.SqlDbType = System.Data.SqlDbType.Decimal;
						pMnozstviDodavateleDodano.Precision = 19;
						pMnozstviDodavateleDodano.Scale = 5;
						pMnozstviDodavateleDodano.Size = 4;

						pMnozstviDodavateleDodat.SqlDbType = System.Data.SqlDbType.Decimal;
						pMnozstviDodavateleDodat.Precision = 19;
						pMnozstviDodavateleDodat.Scale = 5;
						pMnozstviDodavateleDodat.Size = 4;

						pMnozstviOdberateliPozadovano.SqlDbType = System.Data.SqlDbType.Decimal;
						pMnozstviOdberateliPozadovano.Precision = 19;
						pMnozstviOdberateliPozadovano.Scale = 5;
						pMnozstviOdberateliPozadovano.Size = 4;

						pMnozstviOdberatelumDodano.SqlDbType = System.Data.SqlDbType.Decimal;
						pMnozstviOdberatelumDodano.Precision = 19;
						pMnozstviOdberatelumDodano.Scale = 5;
						pMnozstviOdberatelumDodano.Size = 4;

						pMnozstviOdberatelumDodat.SqlDbType = System.Data.SqlDbType.Decimal;
						pMnozstviOdberatelumDodat.Precision = 19;
						pMnozstviOdberatelumDodat.Scale = 5;
						pMnozstviOdberatelumDodat.Size = 4;

						pVysledek.SqlDbType = System.Data.SqlDbType.Decimal;
						pVysledek.Precision = 19;
						pVysledek.Scale = 5;
						pVysledek.Size = 4;

						dbCommand.Connection.Open();

						//dbCommand.Prepare();

						dbCommand.ExecuteNonQuery();
						dbCommand.Connection.Close();

						ObjektOUT.MnozstviDodavatelePozadovano = (decimal)pMnozstviDodavatelePozadovano.Value;
						ObjektOUT.MnozstviDodavateleDodano = (decimal)pMnozstviDodavateleDodano.Value;
						ObjektOUT.MnozstviDodavateleDodat = (decimal)pMnozstviDodavateleDodat.Value;
						ObjektOUT.MnozstviOdberateliPozadovano = (decimal)pMnozstviOdberateliPozadovano.Value;
						ObjektOUT.MnozstviOdberatelumDodano = (decimal)pMnozstviOdberatelumDodano.Value;
						ObjektOUT.MnozstviOdberatelumDodat = (decimal)pMnozstviOdberatelumDodat.Value;
						ObjektOUT.Vysledek = (decimal)pVysledek.Value;

						return ObjektOUT;
					}
					else if (dbCommand.CommandType == CommandType.Text)
					{

					}
					else
					{
						throw new Exception("Neznámý typ příkazu: " + dbCommand.CommandType.ToString());
					}


					return ObjektOUT;
				}
				catch (Exception ex)
				{

					throw ex;
				}


				#endregion


			}
			catch (Exception ex)
			{
				throw ex;
			}


		}

        #endregion

        #region IProdej Members GetSklad

        public STATUS GetSklad(byte idterminal, int userID, string doc_id, string itemnmbr, string serltnum, out string skl_id, out string skl_id_dest)
        {
            SqlConnection xConnection1 = null;

            try
            {
                Globals_V1.LoadConfiguration();

                xConnection1 = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                xConnection1.Open();
                SqlCommand xcommand = new SqlCommand(Globals_V1.Konfigurace.Prodej[0].GetSklad_procName, xConnection1);    // getSklad
                xcommand.CommandType = System.Data.CommandType.StoredProcedure;

                xcommand.Parameters.Add((new SqlParameter(Globals_V1.Konfigurace.Prodej[0].GetSklad_paramName1, // doc_id
                        doc_id)));
                xcommand.Parameters.Add((new SqlParameter(Globals_V1.Konfigurace.Prodej[0].GetSklad_paramName2, // matID
                    itemnmbr)));
                xcommand.Parameters.Add((new SqlParameter(Globals_V1.Konfigurace.Prodej[0].GetSklad_paramName3, // serialID
                    serltnum)));

                SqlParameter parameterSklID = xcommand.CreateParameter();
                parameterSklID.ParameterName = Globals_V1.Konfigurace.Prodej[0].GetSklad_paramName4;    // skladSrc
                parameterSklID.DbType = DbType.String;
                parameterSklID.Value = string.Empty;
                parameterSklID.Size = 20;
                parameterSklID.Direction = System.Data.ParameterDirection.Output;
                xcommand.Parameters.Add(parameterSklID);

                SqlParameter parameterSklIDDest = xcommand.CreateParameter();
                parameterSklIDDest.ParameterName = Globals_V1.Konfigurace.Prodej[0].GetSklad_paramName5;    // skladDest
                parameterSklIDDest.DbType = DbType.String;
                parameterSklIDDest.Size = 20;
                parameterSklIDDest.Value = string.Empty;
                parameterSklIDDest.Direction = System.Data.ParameterDirection.Output;
                xcommand.Parameters.Add(parameterSklIDDest);

                xcommand.ExecuteNonQuery();

                skl_id = (string)parameterSklID.Value;
                skl_id_dest = (string)parameterSklIDDest.Value;

                skl_id = null;
                skl_id_dest = null;

                return STATUS.OK;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                throw ex;
            }
            finally
            {
                try
                {
                    if ((xConnection1.State & ConnectionState.Open) == ConnectionState.Open)
                        xConnection1.Close();
                }
                catch (Exception ex2)
                {
                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex2);
                }
            }
        }

   

        #endregion
    }
}
