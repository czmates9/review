using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Ingres.Client;
using System.Data;
using Fask.Server.Interfaces.Classes;
using Fask.DataSets;
using Fask.Server.Interfaces.Classes_OnlineKomunikace;
using Fask.Interfaces.DataSets;
using Fask.Interfaces.Filtry;
using Fask.Server.Interfaces.DataSets;

namespace Fask.Module.Ingres.SAD
{
    public partial class Provider : 
        Fask.Server.Interfaces.Prodej.IProdej,
        Fask.Interfaces.Prodej.IProdej2,
        Fask.Interfaces.Prodej.IProdej2_Prodej_GetFiltrovaneDavky
    {

        #region NEimplementovane

        public StatusOverPohyb Over_Pohyb(ProdejPohyb ph)
        {
            throw new NotImplementedException();
        }

        public string Prodej_Import_to_IS(Davka davka, ProdejData data, User uzivatel)
        {
            throw new NotImplementedException();
        }

        public Odberatele Prodej_GetDodavatele_External(Fask.Server.Interfaces.Classes.Terminal terminal, Sklad sklad, Item item, List<string> ListCarKod)
        {
            throw new NotImplementedException();
        }

        public Odberatele Prodej_GetDodavatele_ExternalByICO(Fask.Server.Interfaces.Classes.Terminal terminal, Sklad sklad, string ICO)
        {
            throw new NotImplementedException();
        }

        public StatusInfo Prodej_Disponibilita(Disponibilita disponibilita)
        {
            StatusInfo si = null;
            try
            {
                Globals.LoadConfiguration();

                Fask.Interfaces.DataSets.Zbozi zbozi = new Fask.Interfaces.DataSets.Zbozi();

                using (IngresConnection conn = new IngresConnection(Globals.Konfigurace.ConnectionString[0].FASKDB))
                {
                    string select = "SELECT KOD, KOD_LOK, STAV FROM " + TABLE_FASK_ZASOBY_STAV +
                                    " WHERE KOD_LOK = '" + disponibilita.SKL_ID + "'" +
                                    " AND KOD = '" + disponibilita.ITEMNMBR + "'";

                    using (IngresDataAdapter xda = new IngresDataAdapter(select, conn))
                    {
                        xda.Fill(zbozi, zbozi.FASK_ZASOBY_STAV.TableName);
                    }
                }

                string msg = string.Format(Environment.NewLine + "Zboží ID:\t'{0}' " + Environment.NewLine + "Na skladě:\t'{1}'" + Environment.NewLine, disponibilita.ITEMNMBR, disponibilita.SKL_ID);

                if (zbozi.FASK_ZASOBY_STAV.Count == 0)
                {
                    msg += " Nenalezeno";
                    si = new StatusInfo(2, msg);
                    return si;
                }
                else if (zbozi.FASK_ZASOBY_STAV.Count > 1)
                {
                    msg += " Nalezeno vic jak jednou.";
                    si = new StatusInfo(2, msg);
                    return si;
                }
                else if (zbozi.FASK_ZASOBY_STAV.Count == 1)
                {
                    var row = zbozi.FASK_ZASOBY_STAV[0];

                    if (row.STAV > disponibilita.QTY)
                    {
                        si = new StatusInfo(0, "OK");
                    }
                    else if (row.STAV < disponibilita.QTY)
                    {
                        msg += "Zadáno Mn.:\t" + disponibilita.QTY.ToString("#.00") + 
                                Environment.NewLine +
                               "Skladem Mn.:\t" + row.STAV.ToString("#.00");

                        si = new StatusInfo(2, msg);
                    }
                    else if (row.STAV == disponibilita.QTY)
                    {
                        si = new StatusInfo(0, "OK");
                    }


                    return si;
                }
                else
                {
                    si = new StatusInfo(2, "Neznámá chyba!");
                    return si;
                }


            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                si = new StatusInfo(2, ex.Message, ex, DateTime.Now);
                return si;
            }
        }

        public VystupniObjekt Online_UniverzalnyDotazNaCokoliv(VstupniObjekt ObjektIN)
        {
            throw new NotImplementedException();
        }

        #endregion


        #region IProdej Members

        public StatusObject Prodej_Process(Fask.Server.Interfaces.Classes.Davka davka, Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.User uzivatel, Fask.DataSets.ProdejData data)
        {
            Globals.LoadConfiguration();
            //string guidDavka = data.CZMST_DEH[0].GUID.ToString();
            string guidDavka = string.Empty;
            if (data.CZMST_DEH.Count > 0)
                guidDavka = data.CZMST_DEH[0].GUID.ToString();
            else
                guidDavka = Guid.NewGuid().ToString();

            //string filePath = Path.Combine(Properties.Settings.Default.PathStateDataFile, guidDavka);
            string filePath = Path.Combine(Fask.MyPath.Path.RootPath, Path.Combine(Globals.Konfigurace.Prodej[0].StatusObjectsDirectory, guidDavka));

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
            IngresConnection connect = null;
            IngresTransaction iTrans1 = null;

            try
            {

                so.Write("connection");
                connect = new IngresConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
                bool allowInsertData = true;

                connect.Open();

                IngresCommand xselect = new IngresCommand("Select Count(*) as number from " + TABLE_CZMST_DI + " where countentries=" + davka.ID, connect);
                object result = xselect.ExecuteScalar();
                if (result != null && ((int)result) > 0)
                {
                    allowInsertData = false;
                }

                if (allowInsertData)
                {
                    //Probehne ulozeni dat
                    so.Write("insert to db");
                    //Fask.DataSets.ProdejData prodejData = new Fask.DataSets.ProdejData();

                    iTrans1 = connect.BeginTransaction();

                    #region CZMST_DIH

                    #region ulozeni do DIH

                    Database.Prodej.Update_CZMST_DIH(data.CZMST_DIH.Select(null, null, DataViewRowState.Added), connect, iTrans1);

                    #endregion

                    #endregion

                    Database.Prodej.Update_CZMST_DI(add_guid_byte_type_and_values(data), connect, iTrans1);


                    //SQL_Datasets.ProdejTableAdapters.CZMST_DITableAdapter dita = new Fask.ModuleSql.SQL_Datasets.ProdejTableAdapters.CZMST_DITableAdapter();
                    //dita.Connection = connect;
                    //dita.Transaction = iTrans1;
                    //dita.Update(data.CZMST_DI.Select(null, null, DataViewRowState.Added));

                    #region Lokace
                    // ulozeni do lokacniho mechanismu, pokud je zapnuty ...

                    // v davce je jen jeden typ dokladu, urceni, zdali je lokacni mechanismus zapnuty podle tohoto typu dokladu
                    if (data.CZMST_DI.Count > 0)
                    {
                        // nacteni dat typu dokladu
                        string commandText092 = "select * from czmst092 where doc_id=@doc_id and doc_id2=@doc_id2";
                        IngresCommand command092 = new IngresCommand(commandText092, connect, iTrans1);
                        command092.Parameters.Add(new IngresParameter("@doc_id", data.CZMST_DI.First().DOC_ID));
                        string doc_id2 = data.CZMST_DI.First().IsDOC_ID2Null() ? null : data.CZMST_DI.First().DOC_ID2;
                        //command092.Parameters.Add(new IngresParameter("@doc_id2", doc_id2 ?? (object)System.DBNull.Value));
                        command092.Parameters.Add(new IngresParameter("@doc_id2", doc_id2 ?? string.Empty));
                        IngresDataAdapter adapter = new IngresDataAdapter();
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
                                IngresCommand countCommand = new IngresCommand(countCommandText, connect, iTrans1);
                                countCommand.Parameters.Clear();
                                countCommand.Parameters.AddWithValue("@guid", dirow.guid.ToByteArray());

                                int guidcount = (int)countCommand.ExecuteScalar();
                                // \TODO: Co kdyz je jiny recordtype??
                                if (
                                    ((guidcount % 2 == 0) && recordType == Fask.Server.Interfaces.Lokace.TypeOfRecord.P) ||
                                    ((guidcount % 2 == 0) && recordType == Fask.Server.Interfaces.Lokace.TypeOfRecord.V) ||
                                    ((guidcount % 4 == 0) && recordType == Fask.Server.Interfaces.Lokace.TypeOfRecord.D)
                                    )
                                {
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
                                    pohybrow.ITEMDESC = string.Empty;   // dotahnout nazev??
                                    pohybrow.CountEntries = dirow.CountEntries;
                                    pohybrow.dateeveS = dtnow;
                                    if (dirow.IsDATEDONENull() || dirow.IsTIMEDONENull())
                                        pohybrow.dateeveT = dtnow;
                                    else
                                        pohybrow.dateeveT = DateTime.ParseExact(dirow.DATEDONE + " " + dirow.TIMEDONE, "yyyyMMdd HHmmss", System.Globalization.CultureInfo.InvariantCulture);

                                    Lokace_MoveItem(pohybrow, new IngresCommand(), connect, iTrans1, new IngresDataAdapter());
                                }
                            }
                        }
                    }

                    #endregion
                    /*IngresDataAdapter xDataAdapterDI = new IngresDataAdapter();

                    IngresCommand xInsertCommand_DI = CreateDIInsertCom(connect);

                    IngresCommand xSelectCommand_DI = CreateDISelectCom(connect);

                    xDataAdapterDI.InsertCommand = xInsertCommand_DI;
                    xDataAdapterDI.SelectCommand = xSelectCommand_DI;
                    SetAdapterTableMap(xDataAdapterDI);

                    xDataAdapterDI.SelectCommand.Transaction = iTrans1;
                    xDataAdapterDI.InsertCommand.Transaction = iTrans1;

                    //xDataAdapterDI.DatabaseDataAdapter.InsertCommand.Prepare();
                    xDataAdapterDI.Update(data.CZMST_DI.Select(null, null, DataViewRowState.Added));*/

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

            #region Action after data processed
            bool aDP_Action_Asynch = Globals.Konfigurace.Prodej[0].AfterDataProcessed_Action_Asynchronous;
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


            so.SetOK();

            return so;
        }

        private static DataRow[] add_guid_byte_type_and_values(ProdejData data)
        {
            //this.columnGUID = new global::System.Data.DataColumn("GUID", typeof(byte[]), null, global::System.Data.MappingType.Element);
            DataColumn guid_byte_column = new DataColumn("GUID_byte", typeof(byte[]));
            data.CZMST_DI.Columns.Add(guid_byte_column);
            data.CZMST_DI.ToList().ForEach(r => r[guid_byte_column] = r.guid.ToByteArray());
            DataRow[] datarows = data.CZMST_DI.Select(null, null, DataViewRowState.Added);

            return datarows;
        }

        public bool Prodej_AfterProcessedAction(Fask.Server.Interfaces.Classes.Davka davka)
        {
            try
            {
                Globals.LoadConfiguration();

                switch (Globals.Konfigurace.Prodej[0].AfterDataProcessed_TYPE)
                {
                    case "PROC":
                        string aDP_Action = Globals.Konfigurace.Prodej[0].AfterDataProcessed_Action;
                        string aDP_Action_P1 = Globals.Konfigurace.Prodej[0].AfterDataProcessed_Action_P1;
                        string aDP_Action_P2 = Globals.Konfigurace.Prodej[0].AfterDataProcessed_Action_P2;
                        if (aDP_Action.Length != 0)
                        {
                            Routines.AfterProcessAction.Execute(Globals.Konfigurace.ConnectionString[0].FASKDB, TABLE_CZMST_DI, (int)davka.ID, aDP_Action, aDP_Action_P1, aDP_Action_P2, 120);
                        }
                        break;
                    case "EXE":

                        if (davka.ID.HasValue)
                        {
                            Communication.EXE_Comunication.Initialize();
                            
                            //string SB_Konstanta = "/AUTOMAT:MST_IMP";

                            List<string> vs = new List<string>();

                            if (!Globals.Konfigurace.Prodej[0].IsAfterDataProcessed_EXE_FirstParametrKonstantNull() || !string.IsNullOrEmpty(Globals.Konfigurace.Prodej[0].AfterDataProcessed_EXE_FirstParametrKonstant))
                            {
                                vs.Add(Globals.Konfigurace.Prodej[0].AfterDataProcessed_EXE_FirstParametrKonstant.Trim());
                            }

                            vs.Add(TABLE_CZMST_DI);
                            vs.Add(davka.ID.ToString());

                            Communication.EXE_Comunication.Communicate(vs);
                            Communication.EXE_Comunication.Terminate();
                        }
                        else
                        {
                            Logging.ExceptionHandler2.Handle(Logging.LogLevel.Warn, "Neni vyplnená dávka");
                        }
                        break;
                    default:
                        Logging.ExceptionHandler2.Handle(Logging.LogLevel.Warn, "Neznámy typ:" + Globals.Konfigurace.Prodej[0].AfterDataProcessed_TYPE);
                        break;
                }

                
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

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


        public StatusOverLokace Prodej_Online_OverLokace(string itemnmbr, string serltnum, DateTime? Expirace, string locncode, string skl_id, decimal qtyshppd, string doc_id, TYPLokace locationType, Fask.Server.Interfaces.Lokace.TypeOfRecord recordType)
        {
            return Lokace_OverLokace(itemnmbr, serltnum, locncode, skl_id, qtyshppd, doc_id, locationType, recordType);
        }

        public Fask.Server.Interfaces.DataSets.Location Prodej_Online_GetMaterial(string itemnmbr, string skl_id, string serltnum, string doc_id, bool ShowEmpty)
        {
            return Lokace_ShowMaterial(itemnmbr, serltnum, skl_id, null, ShowEmpty);
        }

        public StatusResult Prodej_ProcessTiskSoupis(Fask.DataSets.ProdejData data, out Fask.Server.Interfaces.DataSets.DSValues dataHeader, out List<Fask.Server.Interfaces.DataSets.DSValues> dataRowList, out Fask.Server.Interfaces.DataSets.DSValues dataFooter)
        {
            Fask.Server.Interfaces.Classes.StatusResult status = new StatusResult();
            status.Status = StatusResultEnum.OK;

             dataHeader = new Fask.Server.Interfaces.DataSets.DSValues();
             dataRowList = new List<Fask.Server.Interfaces.DataSets.DSValues>();
             dataFooter = new Fask.Server.Interfaces.DataSets.DSValues();

            if (data == null)
            {
                data = new Fask.DataSets.ProdejData();
                data.ReadXml(Path.Combine(MyPath.Path.ConfigDirectory, "plprocessdata.xml"));
            }

            dataHeader.Values.AddValuesRow(data.CZMST_DI.CountEntriesColumn.ColumnName.Trim(), data.CZMST_DI[0].CountEntries.ToString());

            


            //Fask.Server.Interfaces.DataSets.DSValues dataRowValues = new Fask.Server.Interfaces.DataSets.DSValues();

            foreach (var item in data.CZMST_DI)
            {
                Fask.Server.Interfaces.DataSets.DSValues dataRowValues = new Fask.Server.Interfaces.DataSets.DSValues();
                DotazeniHodnotParams dhp = new DotazeniHodnotParams();
                dhp.itemnmbr = item.ITEMNMBR.Trim();
                dhp.czcarkod = item.IsCZ_CarKodNull() ? string.Empty : item.CZ_CarKod.Trim();
                dhp.vnditnum = item.IsVNDITNUMNull() ? string.Empty : item.VNDITNUM.Trim();
                dhp.serltnum = item.SERLTNUM.Trim();
                DotazeniHodnotDo_DI(dhp, item);

                
                foreach (DataColumn col in data.CZMST_DI.Columns)
                {

                    if (!item.IsNull(col))
                        dataRowValues.Values.AddValuesRow(col.ColumnName.Trim(), item[col].ToString());
                }

                // dotazeni nazvu ...
                if (item.IsITEMDESCNull())
                {
                    string itemdesc = DotazeniNazvu(item.ITEMNMBR.Trim());
                    dataRowValues.Values.AddValuesRow("ITEMDESC", itemdesc);
                }
                dataRowList.Add(dataRowValues);
            }            


            return status;
        }

        public struct DotazeniHodnotParams
        {
            public string itemnmbr;
            public string itemdesc;
            public string serltnum;
            public string vnditnum;
            public string czcarkod;
        }

        private void DotazeniHodnotDo_DI(DotazeniHodnotParams dhp, Fask.DataSets.ProdejData.CZMST_DIRow dirOUT)
        {
            Globals.LoadConfiguration();
            IngresConnection connect = null;

            connect = new IngresConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);

            if (String.IsNullOrEmpty(dhp.itemnmbr))
            {

                if (string.IsNullOrEmpty(dhp.serltnum))
                {

					string commandText095 = "Select * from " + TABLE_FASK_ZASOBY + " where CZ_CarKod=@CZ_CarKod or VNDITNUM=@vnditnum";
                    IngresCommand command095 = new IngresCommand(commandText095, connect);
                    command095.Parameters.Add(new IngresParameter("@CZ_CarKod", dhp.czcarkod ?? string.Empty));
                    command095.Parameters.Add(new IngresParameter("@vnditnum", dhp.vnditnum ?? string.Empty));
                    IngresDataAdapter adapter095 = new IngresDataAdapter();
                    adapter095.SelectCommand = command095;
                    Fask.Interfaces.DataSets.Zbozi dsZbozi = new Fask.Interfaces.DataSets.Zbozi();
                    adapter095.Fill(dsZbozi, dsZbozi.FASK_ZASOBY.TableName);

					dirOUT.ITEMNMBR = dsZbozi.FASK_ZASOBY[0].ITEMNMBR.Trim();
					dirOUT.ITEMCODE = dsZbozi.FASK_ZASOBY[0].ITEMCODE.Trim();
                    //string commandTextSTAV = "SELECT * FROM CZMST_SkladLokace_Stav where ITEMNMBR=@ITEMNMBR";
                    //IngresCommand commandSTAV = new IngresCommand(commandTextSTAV, connect);
                    //commandSTAV.Parameters.Add(new IngresParameter("@ITEMNMBR", dirn.ITEMNMBR.Trim()));
                    //IngresDataAdapter adapterSTAV = new IngresDataAdapter();
                    //adapterSTAV.SelectCommand = commandSTAV;
                    //DataSet ds = new DataSet();
                    //adapterSTAV.Fill(ds);

                    //string SERLTNUMtmp = (string)ds.Tables[0].Rows[0]["SERLTNUM"];
                    dirOUT.SERLTNUM = string.Empty; //SERLTNUMtmp.Trim();

                }
                else //if (!string.IsNullOrEmpty(dhp.serltnum))
                {
                    string commandTextSTAV2 = "SELECT * FROM CZMST_SkladLokace_Stav where SERLTNUM=@SERLTNUM";
                    IngresCommand commandSTAV2 = new IngresCommand(commandTextSTAV2, connect);
                    commandSTAV2.Parameters.Add(new IngresParameter("@SERLTNUM", dhp.serltnum));
                    IngresDataAdapter adapterSTAV2 = new IngresDataAdapter();
                    adapterSTAV2.SelectCommand = commandSTAV2;
                    DataSet ds = new DataSet();
                    adapterSTAV2.Fill(ds);

                    string ITEMNMBRtmp = (string)ds.Tables[0].Rows[0]["ITEMNMBR"];
                    dirOUT.ITEMNMBR = ITEMNMBRtmp.Trim();

                    string ITEMCODEtmp = (string)ds.Tables[0].Rows[0]["ITEMCODE"];
                    dirOUT.ITEMCODE = ITEMCODEtmp.Trim();

                }
            }
            else
            {
                dirOUT.ITEMNMBR = dhp.itemnmbr;
                dirOUT.SERLTNUM = dhp.serltnum ?? string.Empty;

            }
        }

        private string DotazeniNazvu(string itemnmbr)
        {
            Globals.LoadConfiguration();
            IngresConnection connect = null;
            connect = new IngresConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
			string commandText095 = "Select * from " + TABLE_FASK_ZASOBY + " where ITEMNMBR=@itemnmbr";
            IngresCommand command095 = new IngresCommand(commandText095, connect);
            command095.Parameters.Add(new IngresParameter("@itemnmbr", itemnmbr ?? string.Empty));
            IngresDataAdapter adapter095 = new IngresDataAdapter();
            adapter095.SelectCommand = command095;
            Fask.Interfaces.DataSets.Zbozi dsZbozi = new Fask.Interfaces.DataSets.Zbozi();
            adapter095.Fill(dsZbozi, dsZbozi.FASK_ZASOBY.TableName);

            if (dsZbozi.FASK_ZASOBY != null && dsZbozi.FASK_ZASOBY.Count > 0)
                return dsZbozi.FASK_ZASOBY[0].ITEMDESC.Trim();
            else
                return string.Empty;
        }

        public STATUS GetSklad(byte idterminal, int userID, string doc_id, string itemnmbr, string serltnum, out string skl_id, out string skl_id_dest)
        {
            IngresConnection xConnection1 = null;

            try
            {
                Globals.LoadConfiguration();

                xConnection1 = new IngresConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
                xConnection1.Open();
                IngresCommand xcommand = new IngresCommand(Globals.Konfigurace.Prodej[0].GetSklad_procName, xConnection1);    // getSklad
                xcommand.CommandType = System.Data.CommandType.StoredProcedure;

                xcommand.Parameters.Add((new IngresParameter(Globals.Konfigurace.Prodej[0].GetSklad_paramName1, // doc_id
                        doc_id)));
                xcommand.Parameters.Add((new IngresParameter(Globals.Konfigurace.Prodej[0].GetSklad_paramName2, // matID
                    itemnmbr)));
                xcommand.Parameters.Add((new IngresParameter(Globals.Konfigurace.Prodej[0].GetSklad_paramName3, // serialID
                    serltnum)));

                IngresParameter parameterSklID = xcommand.CreateParameter();
                parameterSklID.ParameterName = Globals.Konfigurace.Prodej[0].GetSklad_paramName4;    // skladSrc
                parameterSklID.DbType = DbType.String;
                parameterSklID.Value = string.Empty;
                parameterSklID.Size = 20;
                parameterSklID.Direction = System.Data.ParameterDirection.Output;
                xcommand.Parameters.Add(parameterSklID);

                IngresParameter parameterSklIDDest = xcommand.CreateParameter();
                parameterSklIDDest.ParameterName = Globals.Konfigurace.Prodej[0].GetSklad_paramName5;    // skladDest
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


        #region IProdej2
        
        public Prodej Prodej_GetFiltrovaneDavky(ProdejFiltr filtr)
        {
            IngresConnection connection = null;
            IngresCommand command = null;
            IngresDataAdapter adapter = null;

            Fask.Interfaces.DataSets.Prodej dsProdej = new Fask.Interfaces.DataSets.Prodej();
            try
            {
                connection = new IngresConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
                command = new IngresCommand();
                adapter = new IngresDataAdapter();

                command.CommandText =
                    "SELECT * FROM " + Fask.SQL.Constants.Common.TABLE_CZMST_DI +
                    " WHERE" +
                    " 1=1 ";


                if (filtr.CountEntries != null && !string.IsNullOrEmpty(filtr.CountEntries.Trim()))
                {
                    command.CommandText += "AND CountEntries=@countentries ";
                    command.Parameters.AddWithValue("@countentries", filtr.CountEntries);
                }


                if (filtr.ITEMNMBR != null && !string.IsNullOrEmpty(filtr.ITEMNMBR.Trim()))
                {
                    command.CommandText += "AND ITEMNMBR=@ITEMNMBR ";
                    command.Parameters.AddWithValue("@ITEMNMBR", filtr.ITEMNMBR);
                }


                if (filtr.DATEDONE_OD != null && filtr.DATEDONE_DO != null)
                {
                    string datum_OD = string.Empty;
                    datum_OD = filtr.DATEDONE_OD.Value.ToString("yyyyMMdd");

                    string datum_DO = string.Empty;
                    datum_DO = filtr.DATEDONE_DO.Value.ToString("yyyyMMdd");


                    command.CommandText += "AND DATEDONE between @DATEDONE_OD and @DATEDONE_DO ";
                    command.Parameters.AddWithValue("@DATEDONE_OD", datum_OD);
                    command.Parameters.AddWithValue("@DATEDONE_DO", datum_DO);
                }

                if (filtr.DATEDONE_OD == null && filtr.DATEDONE_DO != null)
                {

                    string datum_DO = string.Empty;
                    datum_DO = filtr.DATEDONE_DO.Value.ToString("yyyyMMdd");

                    command.CommandText += "AND DATEDONE between '19990101' and @DATEDONE_DO ";
                    command.Parameters.AddWithValue("@DATEDONE_DO", datum_DO);
                }

                if (filtr.DATEDONE_OD != null && filtr.DATEDONE_DO == null)
                {
                    string datum_OD = string.Empty;
                    datum_OD = filtr.DATEDONE_OD.Value.ToString("yyyyMMdd");

                    command.CommandText += "AND DATEDONE between @DATEDONE_OD and 25000101 ";
                    command.Parameters.AddWithValue("@DATEDONE_OD", datum_OD);
                }

                command.CommandText += "order by";
                command.CommandText += " CountEntries";
                command.CommandText += " , DEX_ROW_ID";


                command.Connection = connection;
                adapter.SelectCommand = command;

                adapter.Fill(dsProdej, dsProdej.CZMST_DI.TableName);

                return dsProdej;
            }
            catch
            {
                throw;
            }
        }

        public Location Prodej_Online_GetMaterial(string itemnmbr, string skl_id, string serltnum, string doc_id, string locncode)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}


