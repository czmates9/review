using Fask.MST_W_Server.Constants;
using Fask.Server.Interfaces.Classes;
using Fask.Tracing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Fask.MST_W_Server.BL
{
    public class ProdejBL
    {
        #region lokalni promenne

        public Fask.Server.Interfaces.Prodej.IProdej provider = null;
        const string ProdejDBFileExtension = @".di";
        private string TABLE_FASK_ZASOBY = "FASK_ZASOBY";

        public struct DotazeniHodnotParams
        {
            public string itemcode;
            public string itemnmbr;
            public string serltnum;
            public string vnditnum;
            public string czcarkod;
        } 

        #endregion

        #region konstruktor
        public ProdejBL()
        {

        }

        public ProdejBL(Fask.Server.Interfaces.Prodej.IProdej provider)
        {
            this.provider = provider;
        }
        #endregion

        #region inicializace
        public void Initialize(Func<string, string> map_path_function)
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
                        Assembly providerAssemlby = Assembly.LoadFrom(map_path_function(@"~/" + providerAssemblyPath));
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
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }
        #endregion

        #region privatni metody

        /// <summary>
        /// Metoda pro kontrolu licence
        /// </summary>
        /// <returns>True - Licence je validni, False- Licence neni validni</returns>
        private bool isLicenseValid()
        {
            Licensing.License lic = HttpContext.Current.Application[Constants.Common.license] as Licensing.License;

            // Zkontrolujte, zda lic není null
            if (lic != null)
            {
                // Zkontrolujte, zda licence je platná a nevypršela
                if (!lic.isValid || lic.isExpirated)
                    return false;

                return true;
            }

            // V případě, že lic je null, vrátíme false (neplatná licence)
            return false;
        }

        #endregion

        #region web Metody
        public StatusResult Disponibilita(Disponibilita disponibilita)
        {
            Fask.Server.Interfaces.Classes.StatusResult statusResult = new StatusResult();
            statusResult.Status = StatusResultEnum.ERROR;
            statusResult.Message = "Initialized status...";

            if ((provider != null) && (provider is Fask.Server.Interfaces.Prodej.IProdej))
            {
                var sidisp = provider.Prodej_Disponibilita(disponibilita);

                statusResult.Status = (StatusResultEnum)sidisp.ID;
                statusResult.Message = sidisp.Description;
                return statusResult;
            }

            string msg = "Provider v Prodej.asmx > 'Disponibilita(string itemnmbr, string SKL_ID, string LOCNCODE, decimal QTY)' nenastaven.";
            Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
            throw new Exception(msg);
        }

        public StatusObject ProcessProdejFile(int countentries, byte idterminal, int userID, string login, string dstFile)
        {
            // \TODO: try/catch od zacatku do konce, zalogovat chybu a presun datoveho souboru ...
            StatusObject processStatus = new StatusObject();
            TracId tracid = new TracId(userID, idterminal, countentries);

            #region trace
            Trac.Write("provider is " + (provider != null ? "not null" : "null"), "ProcessProdejFile START", tracid);
            #endregion

            if (provider != null) //Nova funkcnost objektova ...
            {

                //1. nacist data z filu
                #region Nacteni dat z datoveho souboru do datasetu
                Fask.DataSets.ProdejData prodejData = new Fask.DataSets.ProdejData();
                Fask.SQLiteDBs.DataSets.Prodej prodejDataCE = new Fask.SQLiteDBs.DataSets.Prodej();

                //SQLiteDBs.DataSets.ProdejTableAdapters.CZMST_DITableAdapter dita = new Fask.MST_W_Server.SQLiteDBs.DataSets.ProdejTableAdapters.CZMST_DITableAdapter();
                //dita.Connection = new System.Data.SQLite.SQLiteConnection( "Data source=" + dstFile);

                //try
                //{
                //	dita.Fill(prodejDataCE.CZMST_DI);
                //}
                //catch (Exception ex)
                //{
                //	Logging.ExceptionHandler2.Handle(ex);
                //}
                //finally
                //{
                //	if (dita != null)
                //	{
                //		if ((dita.Connection.State & ConnectionState.Open) == ConnectionState.Open)
                //			dita.Connection.Close();
                //		dita.Dispose();
                //	}
                //} 

                using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Prodej ConPro = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Prodej(dstFile))
                {
                    ConPro.Fill_DI(prodejDataCE.CZMST_DI);
                }


                #region trace
                Trac.Write(prodejDataCE.CZMST_DI, "ProcessProdejFile", tracid);
                #endregion


                prodejData.CZMST_DI.BeginLoadData();

                foreach (Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIRow dir in prodejDataCE.CZMST_DI)
                {
                    string ITEMNMBR = string.Empty;
                    string SERLTNUM = string.Empty;

                    Fask.DataSets.ProdejData.CZMST_DIRow dirn = prodejData.CZMST_DI.NewCZMST_DIRow();
                    DotazeniHodnotParams dhp = new DotazeniHodnotParams();
                    dhp.itemnmbr = dir.ITEMNMBR.Trim();
                    dhp.czcarkod = dir.IsCZ_CarKodNull() ? string.Empty : dir.CZ_CarKod.Trim();
                    dhp.vnditnum = dir.IsVNDITNUMNull() ? string.Empty : dir.VNDITNUM.Trim();
                    dhp.serltnum = dir.SERLTNUM.Trim();
                    dhp.itemcode = dir.IsITEMCODENull() ? string.Empty : dir.ITEMCODE.Trim();

                    DotazeniHodnotDo_DI(dhp, dirn);


                    if (!dir.IsAMOUNPIENull())
                        dirn.AMOUNPIE = dir.AMOUNPIE;
                    if (!dir.IsAMOUNPIEMNull())
                        dirn.AMOUNPIEM = dir.AMOUNPIEM;
                    dirn.CountEntries = dir.CountEntries;
                    if (!dir.IsCZ_CarKodNull())
                        dirn.CZ_CarKod = dir.CZ_CarKod;
                    if (!dir.IsDATEDONENull())
                        dirn.DATEDONE = dir.DATEDONE;
                    dirn.DEX_ROW_ID = dir.DEX_ROW_ID;
                    if (!dir.IsDOC_IDNull())
                        dirn.DOC_ID = dir.DOC_ID;
                    if (!dir.IsDOC_ID2Null())
                        dirn.DOC_ID2 = dir.DOC_ID2;
                    dirn.guid = dir.guid;
                    dirn.ID_TERMINAL = dir.ID_TERMINAL;
                    dirn.INPUT_MODE = dir.INPUT_MODE;
                    //if (!dir.IsITEMCODENull())
                    //    dirn.ITEMCODE = dir.ITEMCODE;
                    //dirn.ITEMNMBR = dir.ITEMNMBR;
                    if (!dir.IsLOCNCODENull())
                        dirn.LOCNCODE = dir.LOCNCODE;
                    if (!dir.Ismena_IDNull())
                        dirn.mena_ID = dir.mena_ID;
                    if (!dir.Ismena_IDMNull())
                        dirn.mena_IDM = dir.mena_IDM;
                    dirn.MJ = dir.MJ;
                    if (!dir.IsNMBRPALNull())
                        dirn.NMBRPAL = dir.NMBRPAL;
                    if (!dir.IsODB_IDNull())
                        dirn.ODB_ID = dir.ODB_ID;
                    if (!dir.IsPRAC_IDNull())
                        dirn.PRAC_ID = dir.PRAC_ID;
                    if (!dir.IsPRICEXNull())
                        dirn.PRICEX = dir.PRICEX;
                    if (!dir.IsQTYPACKNull())
                        dirn.QTYPACK = dir.QTYPACK;
                    dirn.QTYSHPPD = dir.QTYSHPPD;
                    dirn.QTYSHPPDMJ = dir.QTYSHPPDMJ;
                    if (!dir.IsREZ_1Null())
                        dirn.REZ_1 = dir.REZ_1;
                    if (!dir.IsREZ_2Null())
                        dirn.REZ_2 = dir.REZ_2;
                    if (!dir.IsREZ_3Null())
                        dirn.REZ_3 = dir.REZ_3;
                    if (!dir.IsREZ_4Null())
                        dirn.REZ_4 = dir.REZ_4;
                    //dirn.SERLTNUM = dir.SERLTNUM;
                    if (!dir.IsSKL_IDNull())
                        dirn.SKL_ID = dir.SKL_ID;
                    if (!dir.IsSTR_IDNull())
                        dirn.STR_ID = dir.STR_ID;
                    if (!dir.IsTAXAMPIENull())
                        dirn.TAXAMPIE = dir.TAXAMPIE;
                    if (!dir.IsTAXAMPIEMNull())
                        dirn.TAXAMPIEM = dir.TAXAMPIEM;
                    if (!dir.IsTIMEDONENull())
                        dirn.TIMEDONE = dir.TIMEDONE;
                    if (!dir.IsTYPEPALNull())
                        dirn.TYPEPAL = dir.TYPEPAL;
                    if (!dir.IsUSER_IDNull())
                        dirn.USER_ID = dir.USER_ID;
                    if (!dir.IsVNDITNUMNull())
                        dirn.VNDITNUM = dir.VNDITNUM;
                    if (!dir.IsWITHTAXNull())
                        dirn.WITHTAX = dir.WITHTAX;
                    if (!dir.IsLOCNCODEDESTNull())
                        dirn.LOCNCODEDEST = dir.LOCNCODEDEST;
                    if (!dir.IsSKL_ID_DESTNull())
                        dirn.SKL_ID_DEST = dir.SKL_ID_DEST;
                    if (!dir.IsWEIGHTNull())
                        dirn.WEIGHT = dir.WEIGHT;
                    if (!dir.IsEXPIRACENull())
                        dirn.EXPIRACE = dir.EXPIRACE;
                    if (!dir.IsAttributeToSNNull())
                        dirn.AttributeToSN = dir.AttributeToSN;


                    prodejData.CZMST_DI.AddCZMST_DIRow(dirn);
                }
                prodejData.CZMST_DI.EndLoadData();

                //SQLiteDBs.DataSets.ProdejTableAdapters.CZMST_DI_RFIDTableAdapter dirfidta = new Fask.MST_W_Server.SQLiteDBs.DataSets.ProdejTableAdapters.CZMST_DI_RFIDTableAdapter();
                //dirfidta.Connection = new System.Data.SQLite.SQLiteConnection( "Data source=" + dstFile);

                //try
                //{
                //	dirfidta.Fill(prodejDataCE.CZMST_DI_RFID);
                //}
                //catch (Exception ex)
                //{
                //	Logging.ExceptionHandler2.Handle(ex);
                //}
                //finally
                //{
                //	if (dirfidta != null)
                //	{
                //		if ((dirfidta.Connection.State & ConnectionState.Open) == ConnectionState.Open)
                //			dirfidta.Connection.Close();
                //		dirfidta.Dispose();
                //	}
                //} 

                using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Prodej ConPro = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Prodej(dstFile))
                {
                    ConPro.Fill_DI_RFID(prodejDataCE.CZMST_DI_RFID);
                }

                #region trace
                Trac.Write(prodejDataCE.CZMST_DI_RFID, "ProcessProdejFile", tracid);
                #endregion

                foreach (var i in prodejDataCE.CZMST_DI_RFID)
                {
                    i.AcceptChanges();
                    i.SetAdded();
                    prodejData.CZMST_DI_RFID.ImportRow(i);
                }

                //SQLiteDBs.DataSets.ProdejTableAdapters.CZMST_DEHTableAdapter dehta = new Fask.MST_W_Server.SQLiteDBs.DataSets.ProdejTableAdapters.CZMST_DEHTableAdapter();
                //dehta.Connection = new System.Data.SQLite.SQLiteConnection( "Data source=" + dstFile);
                //try
                //{
                //	dehta.Fill(prodejDataCE.CZMST_DEH);
                //}
                //catch (Exception ex)
                //{
                //	Logging.ExceptionHandler2.Handle(ex);
                //}
                //finally
                //{
                //	if (dehta != null)
                //	{
                //		if ((dehta.Connection.State & ConnectionState.Open) == ConnectionState.Open)
                //			dehta.Connection.Close();
                //		dehta.Dispose();
                //	}
                //}

                using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Prodej ConPro = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Prodej(dstFile))
                {
                    ConPro.Fill_DEH(prodejDataCE.CZMST_DEH);
                }

                #region trace
                Trac.Write(prodejDataCE.CZMST_DEH, "ProcessProdejFile", tracid);
                #endregion

                prodejData.CZMST_DEH.BeginLoadData();
                foreach (Fask.SQLiteDBs.DataSets.Prodej.CZMST_DEHRow dir in prodejDataCE.CZMST_DEH)
                {
                    prodejData.CZMST_DEH.AddCZMST_DEHRow(
                        dir.CountEntries,
                        dir.GUID
                        );
                }
                prodejData.CZMST_DEH.EndLoadData();

                //SQLiteDBs.DataSets.ProdejTableAdapters.CZMST_DIHTableAdapter dihta = new Fask.MST_W_Server.SQLiteDBs.DataSets.ProdejTableAdapters.CZMST_DIHTableAdapter();
                //dihta.Connection = new System.Data.SQLite.SQLiteConnection( "Data source=" + dstFile);
                //try
                //{
                //	dihta.Fill(prodejDataCE.CZMST_DIH);
                //}
                //catch (Exception ex)
                //{
                //	Logging.ExceptionHandler2.Handle(ex);
                //}
                //finally
                //{
                //	if (dihta != null)
                //	{
                //		if ((dihta.Connection.State & ConnectionState.Open) == ConnectionState.Open)
                //			dihta.Connection.Close();
                //		dihta.Dispose();
                //	}
                //}

                using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Prodej ConPro = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Prodej(dstFile))
                {
                    ConPro.Fill_DIH(prodejDataCE.CZMST_DIH);
                }

                #region trace
                Trac.Write(prodejDataCE.CZMST_DIH, "ProcessProdejFile", tracid);
                #endregion

                //prodejData.CZMST_DIH.BeginLoadData();

                //foreach (Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIHRow dir in prodejDataCE.CZMST_DIH)
                //{
                //    prodejData.CZMST_DIH.AddCZMST_DIHRow(
                //        dir.IsZakazka_IDNull() ? "" : dir.Zakazka_ID,
                //        dir.IsPaleta_IDNull() ? "" : dir.Paleta_ID,
                //        dir.Ismena_IDNull() ? "" : dir.mena_ID,
                //        dir.IsSKL_IDNull() ? "" : dir.SKL_ID,
                //        //dir.IsISOKNull() ? (DateTime?)null : dir.ISOK,
                //        //dir.IsstatusNull() ? (int?)null : dir.status,
                //    countentries
                //        );
                //}

                //prodejData.CZMST_DIH.EndLoadData();

                foreach (Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIHRow dir in prodejDataCE.CZMST_DIH)
                {
                    var r = prodejData.CZMST_DIH.NewCZMST_DIHRow();

                    r.Zakazka_ID = dir.IsZakazka_IDNull() ? "" : dir.Zakazka_ID;
                    r.Paleta_ID = dir.IsPaleta_IDNull() ? "" : dir.Paleta_ID;
                    r.mena_ID = dir.Ismena_IDNull() ? "" : dir.mena_ID;
                    r.SKL_ID = dir.IsSKL_IDNull() ? "" : dir.SKL_ID;
                    r.CountEntries = countentries;

                    if (dir.IsISOKNull()) r.SetISOKNull();
                    else r.ISOK = dir.ISOK;

                    if (dir.IsstatusNull()) r.SetstatusNull();
                    else r.status = dir.status;

                    prodejData.CZMST_DIH.AddCZMST_DIHRow(r);
                }



                #endregion

                //2. zavolani process data rozhrani objektu
                #region Zpracovani dat

                Fask.Server.Interfaces.Classes.Davka davka = new Fask.Server.Interfaces.Classes.Davka();
                Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();
                Fask.Server.Interfaces.Classes.User uzivatel = new Fask.Server.Interfaces.Classes.User();

                davka.ID = countentries;
                terminal.ID = idterminal;
                uzivatel.ID = userID;
                uzivatel.Login = login;

                #region trace
                Trac.Write("provider.Prodej_Process(davka, terminal,uzivatel, prodejData) begin", "ProcessProdejFile", tracid);
                #endregion

                try
                {
                    processStatus = provider.Prodej_Process(davka, terminal, uzivatel, prodejData);
                }
                catch (System.Exception ex)
                {
                    processStatus.SetException(ex);
                    return processStatus;
                }

                #region trace
                Trac.Write("provider.Prodej_Process(davka, terminal,uzivatel, prodejData) end, StatusText : " + processStatus.StatusText, "ProcessProdejFile", tracid);
                #endregion
                #endregion

                //3. Managment datoveho souboru
                #region Managment datoveho souboru
                if (processStatus.StatusText == "OK" && !processStatus.Exception) //uspelo => do processed
                {
                    Routines.ManageDataFiles.Move(dstFile, Path.Combine(Fask.MyPath.Path.ProcessedDataFileDirectory, Path.GetFileName(dstFile)));
                }
                else //neuspelo => do erroru
                {
                    Routines.ManageDataFiles.Move(dstFile, Path.Combine(Fask.MyPath.Path.ErrorDataFileDirectory, Path.GetFileName(dstFile)));
                }
                #endregion

                #region trace
                Trac.Write("ProcessProdejFile END", tracid);
                #endregion

                //4. navratova hodnota zpracovani ...
                return processStatus;
            }

            return null;

        }

        /// <summary>
        /// Metoda která dotahne hodnoty z FASK_ZASOBY do DI
        /// </summary>
        /// <param name="dhp">Podminky pro dotaženi</param>
        /// <param name="dirOUT">Dotaženy Row z CZMST_DI</param>
        private void DotazeniHodnotDo_DI(DotazeniHodnotParams dhp, Fask.DataSets.ProdejData.CZMST_DIRow dirOUT)
        {
            Konfigurace.Classes.Globals_Konfig_WebConfig.LoadConfiguration();

            SqlConnection connect = null;

            connect = new System.Data.SqlClient.SqlConnection(Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.ConnectionString[0].FASKDB);

            if (String.IsNullOrEmpty(dhp.itemnmbr))
            {

                if (string.IsNullOrEmpty(dhp.serltnum))
                {

                    string commandText095 = "Select * from " + TABLE_FASK_ZASOBY + " where CZ_CarKod=@CZ_CarKod or VNDITNUM=@vnditnum";
                    SqlCommand command095 = new SqlCommand(commandText095, connect);
                    command095.Parameters.Add(new SqlParameter("@CZ_CarKod", dhp.czcarkod ?? string.Empty));
                    command095.Parameters.Add(new SqlParameter("@vnditnum", dhp.vnditnum ?? string.Empty));
                    SqlDataAdapter adapter095 = new SqlDataAdapter();
                    adapter095.SelectCommand = command095;
                    Fask.Interfaces.DataSets.Zbozi dsZbozi = new Fask.Interfaces.DataSets.Zbozi();
                    adapter095.Fill(dsZbozi, dsZbozi.FASK_ZASOBY.TableName);

                    dirOUT.ITEMNMBR = dsZbozi.FASK_ZASOBY[0].ITEMNMBR.Trim();
                    dirOUT.ITEMCODE = dsZbozi.FASK_ZASOBY[0].ITEMCODE.Trim();


                }
                else
                {
                    string commandTextSTAV2 = "SELECT * FROM CZMST_SkladLokace_Stav where SERLTNUM=@SERLTNUM";
                    SqlCommand commandSTAV2 = new SqlCommand(commandTextSTAV2, connect);
                    commandSTAV2.Parameters.Add(new SqlParameter("@SERLTNUM", dhp.serltnum));
                    SqlDataAdapter adapterSTAV2 = new SqlDataAdapter();
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
                dirOUT.ITEMCODE = dhp.itemcode ?? string.Empty;

            }
        }

        /// <summary>
        /// Metoda pro zpracovaní dat na serveru
        /// </summary>
        /// <param name="countentries">číslo dávky</param>
        /// <param name="idterminal">ID Terminalu</param>
        /// <param name="userID">ID uživatele</param>
        /// <param name="login">Login uživatele</param>
        /// <returns>StatusObjekt - informace o zpracování</returns>
        public StatusObject ProcessProdejDB2(int countentries, byte idterminal, int userID, string login)
        {
            StatusObject so = new StatusObject();
            TracId tracid = new TracId(userID, idterminal, countentries);

            #region trace
            Trac.Write("ProcessProdejDB BEGIN", tracid);
            #endregion

            if (!isLicenseValid())
            {
                so.StatusText = "Licence na serveru není validní!";
                so.Exception = true;
                return so;
            }

            string dstFile = Path.Combine(Fask.MyPath.Path.SQLiteDBsDirectory, idterminal.ToString() + @"\" + countentries.ToString() + ProdejDBFileExtension);
            string dstFileZip = dstFile + Common.ZIP;

            if (!File.Exists(dstFileZip))
            {
                so.StatusText = "Nenalezen Soubor pro zpracovani";
                so.Exception = true;
                return so;
            }

            #region trace
            Trac.Write("DB FileStream start", "ProcessProdejDB", tracid);
            #endregion

            Fask.Compressing.Zip.Decompress(dstFileZip);

            #region trace
            Trac.Write("DB FileStream end", "ProcessProdejDB", tracid);
            #endregion

            try
            {
                so = ProcessProdejFile(countentries, idterminal, userID, login, dstFile);
            }
            catch (Exception ex)
            {
                if (File.Exists(dstFile))
                    File.Delete(dstFile);

                so.Exception = true;
                so.StatusText = ex.Message;
            }

            if (so.StatusText == "OK" && !so.Exception)
            {
                if (File.Exists(dstFile))
                    File.Delete(dstFile);

                if (File.Exists(dstFileZip))
                    File.Delete(dstFileZip);
            }
            else
            {
                //so.Exception = true;
                //so.StatusText = "Nezpracovana data na serveru";
            }



            #region trace
            Trac.Write("ProcessProdejDB END", tracid);
            #endregion

            return so;
        }



        #region online metody
        

        //public Fask.Server.Interfaces.DataSets.Location Online_GetMaterial(string itemnmbr, string skl_id, string serltnum, string doc_id, string locncode)
        //{
        //    Fask.Server.Interfaces.DataSets.Obecne ds = new Fask.Server.Interfaces.DataSets.Obecne();
        //    Fask.Server.Interfaces.DataSets.Location dsLocation = new Fask.Server.Interfaces.DataSets.Location();

        //    try
        //    {
        //        if (provider != null)
        //        {
        //            return provider.Prodej_Online_GetMaterial(itemnmbr ?? string.Empty, skl_id ?? string.Empty, serltnum ?? string.Empty, doc_id ?? string.Empty, locncode);
        //        }
        //        //1.8.2025 MaR zakomentovano, protoze je to docasne prebrano z MST_06 a zde nejsou prozatim dostupne komponenty
        //        #region docasne zakomentovano, prebrano z MST_06
        //        //else
        //        //{
                    

        //        //    string prijem_generateData = System.Configuration.ConfigurationManager.AppSettings["Prodej_GetLokaci_Action"].Trim();
        //        //    if (prijem_generateData.Length != 0)
        //        //    {
        //        //        xConnection1 = new XConnection(xdb, xconnstring);
        //        //        XCommand adpacommand = new XCommand(xdb, prijem_generateData);
        //        //        adpacommand.CommandType = CommandType.StoredProcedure;

        //        //        // parametry
        //        //        adpacommand.Parameters.Add((new XParameter(xdb, "@Itemnmbr", XDbType.NVarChar, 31)).DatabaseParameter);
        //        //        adpacommand.Parameters.Add((new XParameter(xdb, "@Skl_id", XDbType.NVarChar, 20)).DatabaseParameter);
        //        //        adpacommand.Parameters.Add((new XParameter(xdb, "@Serltnum", XDbType.NVarChar, 21)).DatabaseParameter);
        //        //        adpacommand.Parameters.Add((new XParameter(xdb, "@doc_id", XDbType.NVarChar, 12)).DatabaseParameter);

        //        //        // hodnoty vstupnich parametru
        //        //        ((IDataParameter)adpacommand.Parameters["@Itemnmbr"]).Value = itemnmbr;
        //        //        ((IDataParameter)adpacommand.Parameters["@Skl_id"]).Value = skl_id;
        //        //        ((IDataParameter)adpacommand.Parameters["@Serltnum"]).Value = serltnum;
        //        //        ((IDataParameter)adpacommand.Parameters["@doc_id"]).Value = doc_id;

        //        //        adpacommand.Connection = xConnection1.DatabaseConnection;
        //        //        xConnection1.Open();

        //        //        XDataAdapter xda = new XDataAdapter(xdb);
        //        //        xda.SelectCommand = adpacommand.DatabaseCommand;

        //        //        ((DbDataAdapter)xda.DatabaseDataAdapter).Fill(ds, ds.Palety.TableName);

        //        //        // import do noveho datasetu
        //        //        if (ds != null && ds.Palety.Count > 0)
        //        //        {
        //        //            DateTime dtnow = DateTime.Now;
        //        //            foreach (var paletyrow in ds.Palety)
        //        //            {
        //        //                Fask.Server.Interfaces.DataSets.Location.CZMST_SkladLokace_StavRow newrow = dsLocation.CZMST_SkladLokace_Stav.NewCZMST_SkladLokace_StavRow();
        //        //                newrow.ITEMNMBR = paletyrow.IsITEMNMBRNull() ? string.Empty : paletyrow.ITEMNMBR;
        //        //                newrow.ITEMDESC = string.Empty;
        //        //                newrow.QTYSHPPD_DEF = paletyrow.QTYSHPPD;
        //        //                newrow.QTYSHPPD = paletyrow.QTYSHPPD;
        //        //                newrow.SERLTNUM = paletyrow.IsSERLTNUMNull() ? string.Empty : paletyrow.SERLTNUM;
        //        //                newrow.SKL_ID = paletyrow.IsSKL_IDNull() ? string.Empty : paletyrow.SKL_ID;
        //        //                newrow.LOCNCODE = paletyrow.IsLOCNCODENull() ? string.Empty : paletyrow.LOCNCODE;
        //        //                newrow.DATECHANGE = dtnow;
        //        //                newrow.SetEXPIRATIONNull();

        //        //                dsLocation.CZMST_SkladLokace_Stav.AddCZMST_SkladLokace_StavRow(newrow);
        //        //            }
        //        //        }
        //        //    }
        //            #endregion

        //            return dsLocation;
                
        //    }
        //    catch (Exception ex)
        //    {
        //        //Log.writeErrorLog(ex.Message);
        //        Logging.ExceptionHandler2.Handle(ex);
        //        throw ex;
        //    }
        //    //1.8.2025 MaR zakomentovano, protoze je to docasne prebrano z MST_06 a zde nejsou prozatim dostupne komponenty
        //    //finally
        //    //{
        //    //    if (xConnection1 != null && (xConnection1.State & ConnectionState.Open) == ConnectionState.Open)
        //    //        xConnection1.Close();
        //    //}
        //}


        #endregion


        #endregion
    }
}