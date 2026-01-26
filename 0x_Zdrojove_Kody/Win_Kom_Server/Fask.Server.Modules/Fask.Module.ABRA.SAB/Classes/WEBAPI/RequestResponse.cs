using Fask.DataSets;
using Fask.Module.ABRA.SAB.Classes.ABRA_BO;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Module.ABRA.SAB.Classes.WEBAPI
{
    public class RequestResponse
    {

        #region Vydej

        #region Import_DL_to_FV

        //public static bool CreateRequest_Import_DL_to_FV(Fask.DataSets.Vydej dsV, Guid G, out string JSON)
        //{
        //    JSON = string.Empty;

        //    try
        //    {
        //        Globals_V1.LoadConfiguration();

        //        SelectedRows selectedRows = new SelectedRows();
        //        dsV.CZMST_SI.ToList().ForEach(x => selectedRows.sr.Add(x.ITEMNMBR.Trim()));

        //        Classes.WEBAPI.DL_to_FV dL_To_FV = new DL_to_FV();

        //        dL_To_FV._params = new Params()
        //        {
        //            DocQueue_ID = Globals_V1.Konfigurace.ID_Rady[0].RadaID_Vydej_FakturaVydana.Trim(),
        //            SelectedRows = selectedRows.GetSelectedRows()
        //        };

        //        JSON = Classes.JSON_Class.Serialize_JSON(dL_To_FV);

        //        if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
        //        {
        //            SAB.Constants.SaveToFile.Save(Constants.Common.REQ, "DL_to_FV", JSON, G, Constants.Common.json);
        //        }

        //        return true;
        //    }
        //    catch (System.Exception ex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle(ex);
        //        throw ex;
        //    }
        //}

        //public static Classes.ABRA_BO.Rootobject_FV LoadResponse_Import_DL_to_FV(IRestResponse restResponse, Guid G)
        //{
        //    if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
        //    {
        //        SAB.Constants.SaveToFile.Save(Constants.Common.RES, "DL_to_FV", new Classes.JsonFormatter(restResponse.Content).Format(), G, Constants.Common.json);
        //    }

        //    if (restResponse.StatusCode == HttpStatusCode.Created)
        //    {
        //        Classes.ABRA_BO.Rootobject_FV o = Newtonsoft.Json.JsonConvert.DeserializeObject<Classes.ABRA_BO.Rootobject_FV>(restResponse.Content);

        //        Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info, "Vytvořen doklad FV ID:'" + o.id + "'");
        //        return o;
        //    }
        //    else
        //    {
        //        throw new Exception("Chyba :'" + restResponse.StatusCode.ToString() + "'" + Environment.NewLine + restResponse.Content);
        //    }
        //}

        #endregion

        #region Edit_QTY_in_FV

        internal static bool CreateRequest_Edit_QTY_in_FV(Classes.ABRA_BO.Rootobject_FV rootobject, Fask.DataSets.Vydej dsV, Guid G, out string JSON)
        {
            JSON = string.Empty;

            try
            {
                Globals_V1.LoadConfiguration();

                Classes.ABRA_BO.Rootobject_FV ro = new Rootobject_FV();

                List<Classes.ABRA_BO.Row_FV> row = new List<Row_FV>();

                foreach (var radek in dsV.CZMST_SI)
                {
                    string SOPNUMBE = radek.SOPNUMBE;
                    string ITEMNMBR = radek.ITEMNMBR;
                    string SKL_ID = radek.SKL_ID;

                    string StoreID = Database.ABRA.Get_STORECARD_ID_z_DL(SOPNUMBE, ITEMNMBR, SKL_ID);

                    foreach (Classes.ABRA_BO.Row_FV item in rootobject.rows)
                    {
                        if (item.storecard_id.Trim() == StoreID.Trim())
                        {
                            float QTY = Convert.ToSingle(radek.QTYSHPPD);

                            row.Add(new Row_FV()
                            {
                                id = item.id,
                                quantity = QTY
                            });
                        }
                    }
                }

                ro.rows = row.ToArray();
                JSON = Classes.JSON_Class.Serialize_JSON(ro);

                if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
                {
                    SAB.Constants.SaveToFile.Save(Constants.Common.REQ, "Edit_QTY_in_FV", JSON, G, Constants.Common.json);
                }

                return true;
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }

        internal static string LoadResponse_Edit_QTY_in_FV(IRestResponse restResponse, Guid G)
        {
            if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
            {
                SAB.Constants.SaveToFile.Save(Constants.Common.RES, "Edit_QTY_in_FV", new Classes.JsonFormatter(restResponse.Content).Format(), G, Constants.Common.json);
            }

            if (restResponse.StatusCode == HttpStatusCode.OK)
            {
                return "OK";
            }
            else
            {
                throw new Exception("Chyba :'" + restResponse.StatusCode.ToString() + "'" + Environment.NewLine + restResponse.Content);
            }
        }

        #endregion

        #region Create_DL

        internal static bool CreateRequest_Create_DL(List<Polozky_Vydej> pol, Guid G, out string JSON)
        {
            JSON = string.Empty;

            try
            {
                Globals_V1.LoadConfiguration();

                SelectedRows selectedRows = new SelectedRows();

                foreach (Polozky_Vydej item in pol)
                {
                    string IDRow = Database.ABRA.Get_ID_Row_OP(item);
                    selectedRows.sr.Add(IDRow);
                }

                //dsV.CZMST_SI.ToList().ForEach(x => selectedRows.sr.Add(x.ITEMNMBR.Trim()));

                Classes.WEBAPI.OP_to_DL oP_To_DL = new OP_to_DL();

                oP_To_DL._params = new Params()
                {
                    DocQueue_ID = Globals_V1.Konfigurace.ID_Rady[0].RadaID_Vydej_DodaciList.Trim(),
                    SelectedRows = selectedRows.GetSelectedRows()
                };

                JSON = Classes.JSON_Class.Serialize_JSON(oP_To_DL);

                if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
                {
                    SAB.Constants.SaveToFile.Save(Constants.Common.REQ, "Create_DL", JSON, G, Constants.Common.json);
                }

                return true;
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }

        internal static string LoadResponse_Create_DL(IRestResponse restResponse, Guid G, out string ID_DL)
        {
            ID_DL = null;
            if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
            {
                SAB.Constants.SaveToFile.Save(Constants.Common.RES, "Create_DL", new Classes.JsonFormatter(restResponse.Content).Format(), G, Constants.Common.json);
            }

            if (restResponse.StatusCode == HttpStatusCode.Created)
            {

                Classes.ABRA_BO.Rootobject_DL o = Newtonsoft.Json.JsonConvert.DeserializeObject<Classes.ABRA_BO.Rootobject_DL>(restResponse.Content);

                ID_DL = o.id;

                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info, "Vytvořen doklad DL ID:'" + o.id + "'");
                return "OK";
            }
            else
            {
                throw new Exception("Chyba :'" + restResponse.StatusCode.ToString() + "'" + Environment.NewLine + restResponse.Content);
            }
        }

        #endregion

        #region Create_DL_all

        internal static bool CreateRequest_Create_DL_all(Guid G, out string JSON)
        {
            JSON = string.Empty;

            try
            {
                Globals_V1.LoadConfiguration();

                Classes.WEBAPI.OP_to_DL oP_To_DL = new OP_to_DL();

                oP_To_DL._params = new Params()
                {
                    DocQueue_ID = Globals_V1.Konfigurace.ID_Rady[0].RadaID_Vydej_DodaciList.Trim(),
                };

                JSON = Classes.JSON_Class.Serialize_JSON(oP_To_DL);

                if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
                {
                    SAB.Constants.SaveToFile.Save(Constants.Common.REQ, "Create_DL_all", JSON, G, Constants.Common.json);
                }

                return true;
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }

        internal static string LoadResponse_Create_DL_all(IRestResponse restResponse, Guid G, out string ID_DL)
        {
            ID_DL = null;
            if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
            {
                SAB.Constants.SaveToFile.Save(Constants.Common.RES, "Create_DL_all", new Classes.JsonFormatter(restResponse.Content).Format(), G, Constants.Common.json);
            }

            if (restResponse.StatusCode == HttpStatusCode.Created)
            {

                Classes.ABRA_BO.Rootobject_DL o = Newtonsoft.Json.JsonConvert.DeserializeObject<Classes.ABRA_BO.Rootobject_DL>(restResponse.Content);

                ID_DL = o.id;

                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info, "Vytvořen doklad DL ID:'" + o.id + "'");
                return "OK";
            }
            else
            {
                throw new Exception("Chyba :'" + restResponse.StatusCode.ToString() + "'" + Environment.NewLine + restResponse.Content);
            }
        }

        #endregion

        #region Edit_Stav_DL

        internal static string LoadResponse_Edit_Stav_DL(IRestResponse restResponse, Guid G)
        {
            if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
            {
                SAB.Constants.SaveToFile.Save(Constants.Common.RES, "Edit_Stav_DL", new Classes.JsonFormatter(restResponse.Content).Format(), G, Constants.Common.json);
            }

            if (restResponse.StatusCode == HttpStatusCode.OK)
            {
                return "OK";
            }
            else
            {
                throw new Exception("Chyba :'" + restResponse.StatusCode.ToString() + "'" + Environment.NewLine + restResponse.Content);
            }
        }

        internal static bool CreateRequest_Edit_Stav_DL(string stav, Guid G, out string JSON)
        {
            JSON = string.Empty;

            try
            {
                Globals_V1.LoadConfiguration();

                Classes.WEBAPI.PMState pMState = new PMState();

                //pMState.pmstate_id = Database.ABRA.Get_ID_PMState(stav);
                pMState.pmstate_id = stav.Trim();

                JSON = Classes.JSON_Class.Serialize_JSON(pMState);

                if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
                {
                    SAB.Constants.SaveToFile.Save(Constants.Common.REQ, "Edit_Stav_DL", JSON, G, Constants.Common.json);
                }

                return true;
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }

        #endregion

        #region Edit_QTY_in_DL

        internal static string LoadResponse_Edit_QTY_in_DL(IRestResponse restResponse, Guid G)
        {
            if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
            {
                SAB.Constants.SaveToFile.Save(Constants.Common.RES, "Edit_QTY_in_DL", new Classes.JsonFormatter(restResponse.Content).Format(), G, Constants.Common.json);
            }

            if (restResponse.StatusCode == HttpStatusCode.OK)
            {
                return "OK";
            }
            else
            {
                throw new Exception("Chyba :'" + restResponse.StatusCode.ToString() + "'" + Environment.NewLine + restResponse.Content);
            }
        }

        internal static bool CreateRequest_Edit_QTY_in_DL(List<Polozky_Vydej> polEdit, Guid G, out string JSON)
        {
            JSON = string.Empty;

            try
            {
                Globals_V1.LoadConfiguration();

                Classes.ABRA_BO.Rootobject_DL ro = new Rootobject_DL();

                List<Classes.ABRA_BO.Row_DL> row = new List<Row_DL>();

                foreach (Polozky_Vydej radek in polEdit)
                {
                    row.Add(new Row_DL()
                    {
                        id = radek.ITEMNMBR.Trim(),
                        quantity = Convert.ToSingle(radek.QTY)
                    });
                }

                ro.rows = row.ToArray();
                JSON = Classes.JSON_Class.Serialize_JSON(ro);

                if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
                {
                    SAB.Constants.SaveToFile.Save(Constants.Common.REQ, "Edit_QTY_in_DL", JSON, G, Constants.Common.json);
                }

                return true;
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }

        #endregion

        #region Delete_in_DL

        internal static string LoadResponse_Delete_in_DL(IRestResponse restResponse, Guid G)
        {
            if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
            {
                SAB.Constants.SaveToFile.Save(Constants.Common.RES, "Delete_in_DL", new Classes.JsonFormatter(restResponse.Content).Format(), G, Constants.Common.json);
            }

            if (restResponse.StatusCode == HttpStatusCode.NoContent)
            {
                return "OK";
            }
            else
            {
                throw new Exception("Chyba :'" + restResponse.StatusCode.ToString() + "'" + Environment.NewLine + restResponse.Content);
            }
        }

        #endregion

        #region Import_DL_to_FV_all

        internal static bool CreateRequest_Import_DL_to_FV_all(Guid G,string ID_OP, string DocQueueID_FV, out string JSON)
        {
            JSON = string.Empty;

            try
            {

                /******************************URL**********************************/

                //http://192.168.1.49:8085/demodata/issuedinvoices/import/billsofdelivery/F9R0000101

                /******************************BODY**********************************/

                //{
                //        "params": {
                //              "SelectedHeader": "35D0000101",   // ID Objednavky
                //              "DocQueue_ID": "5600000101"
                //    }
                //}

                /****************************************************************/

                Globals_V1.LoadConfiguration();

                Classes.WEBAPI.DL_to_FV dL_To_FV = new DL_to_FV();

                dL_To_FV._params = new Params()
                {
                    SelectedHeader = ID_OP,
                    DocQueue_ID = DocQueueID_FV
                };

                JSON = Classes.JSON_Class.Serialize_JSON(dL_To_FV);

                if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
                {
                    SAB.Constants.SaveToFile.Save(Constants.Common.REQ, "DL_to_FV_all", JSON, G, Constants.Common.json);
                }

                return true;
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }

        internal static Rootobject_FV LoadResponse_Import_DL_to_FV_all(IRestResponse restResponse, Guid G)
        {
            if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
            {
                SAB.Constants.SaveToFile.Save(Constants.Common.RES, "DL_to_FV_all", new Classes.JsonFormatter(restResponse.Content).Format(), G, Constants.Common.json);
            }

            if (restResponse.StatusCode == HttpStatusCode.Created)
            {
                Classes.ABRA_BO.Rootobject_FV o = Newtonsoft.Json.JsonConvert.DeserializeObject<Classes.ABRA_BO.Rootobject_FV>(restResponse.Content);

                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info, "Vytvořen doklad ID:'" + o.id + "'");
                return o;
            }
            else
            {
                throw new Exception("Chyba :'" + restResponse.StatusCode.ToString() + "'" + Environment.NewLine + restResponse.Content);
            }
        }

        #endregion

        #region Edit_SERLTNUM_QTY_in_DL

        internal static string LoadResponse_Edit_SERLTNUM_QTY_in_DL(IRestResponse restResponse, Guid G)
        {
            if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
            {
                SAB.Constants.SaveToFile.Save(Constants.Common.RES, "Edit_SERLTNUM_QTY_in_DL", new Classes.JsonFormatter(restResponse.Content).Format(), G, Constants.Common.json);
            }

            if (restResponse.StatusCode == HttpStatusCode.OK)
            {
                return "OK";
            }
            else
            {
                throw new Exception("Chyba :'" + restResponse.StatusCode.ToString() + "'" + Environment.NewLine + restResponse.Content);
            }
        }

        internal static bool CreateRequest_Edit_SERLTNUM_QTY_in_DL(List<Polozky_Vydej> polEdit, Guid G, out string JSON)
        {
            JSON = string.Empty;

            try
            {
                Globals_V1.LoadConfiguration();

                Classes.ABRA_BO.Rootobject_DL ro = new Rootobject_DL();

                List<Classes.ABRA_BO.Row_DL> row = new List<Row_DL>();

                //var tmpList = polEdit.GroupBy(x => new { x.ITEMNMBR, x.SOPNUMBE });

                //foreach (var item in tmpList)
                //{
                //    item.Key.ITEMNMBR
                //}

                foreach (Polozky_Vydej radek in polEdit)
                {
                    Row_DL row_DL = new Row_DL();
                    string idDL_Row = Database.ABRA.Get_ID_Polozka_DodaciList(radek);
                    row_DL.id = idDL_Row.Trim();

                    if (!string.IsNullOrEmpty(radek.SELTNUM))
                    {
                        // 15.5.2020 TaD Tady muže nastat chyba, že šarže neexistuje, DLE JaS je schvaleno na to nereagovat, tohle nastat nemuže...
                        string ID_Sarze = Database.ABRA.Get_ID_StoreBatch(radek.SELTNUM.Trim(), radek.Expirace, radek.ITEMNMBR.Trim());

                        if (string.IsNullOrEmpty(ID_Sarze))
                        {
                            throw new Exception("Tahle chyba dle JaS nenastane....");
                        }

                        var Sarze = new List<DocRowBatches_DL>();
                        float? qtyser  = null;
                        if (radek.QTY_SELTNUM.HasValue)
                        {
                            qtyser = Convert.ToSingle(radek.QTY_SELTNUM.Value);
                        }

                        Sarze.Add(new DocRowBatches_DL() { 
                            StoreBatch_ID = ID_Sarze,
                            quantity = qtyser
                        });

                        row_DL.DocRowBatches = Sarze.ToArray();
                    }

                    if (radek.QTY.HasValue)
                    {
                        row_DL.quantity = Convert.ToSingle(radek.QTY.Value);
                    }

                    row.Add(row_DL);
                }

                ro.rows = row.ToArray();
                JSON = Classes.JSON_Class.Serialize_JSON(ro);

                if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
                {
                    SAB.Constants.SaveToFile.Save(Constants.Common.REQ, "Edit_SERLTNUM_QTY_in_DL", JSON, G, Constants.Common.json);
                }

                return true;
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }

        #endregion

        #endregion

        #region Test Komunikace

        internal static string LoadResponse_TestKomunikace(IRestResponse restResponse, Guid G)
        {
            if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
            {
                SAB.Constants.SaveToFile.Save(Constants.Common.RES, "TestKomunikace", new Classes.JsonFormatter(restResponse.Content).Format(), G, Constants.Common.json);
            }

            if (restResponse.StatusCode == HttpStatusCode.OK)
            {
                return restResponse.Content;
            }
            else
            {
                throw new Exception("Chyba :'" + restResponse.StatusCode.ToString() + "'" + Environment.NewLine + restResponse.Content);
            }
        }



        #endregion

        #region Prijem

        #region Edit_Stav_PR

        internal static bool CreateRequest_Edit_Stav_PR(string stav, Guid G, out string JSON)
        {
            JSON = string.Empty;

            try
            {
                Globals_V1.LoadConfiguration();

                Classes.WEBAPI.PMState pMState = new PMState();

                //pMState.pmstate_id = Database.ABRA.Get_ID_PMState(stav);
                pMState.pmstate_id = stav.Trim();

                JSON = Classes.JSON_Class.Serialize_JSON(pMState);

                if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
                {
                    SAB.Constants.SaveToFile.Save(Constants.Common.REQ, "Edit_Stav_PR", JSON, G, Constants.Common.json);
                }

                return true;
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }

        internal static string LoadResponse_Edit_Stav_PR(IRestResponse restResponse, Guid G)
        {
            if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
            {
                SAB.Constants.SaveToFile.Save(Constants.Common.RES, "Edit_Stav_PR", new Classes.JsonFormatter(restResponse.Content).Format(), G, Constants.Common.json);
            }

            if (restResponse.StatusCode == HttpStatusCode.OK)
            {
                return "OK";
            }
            else
            {
                throw new Exception("Chyba :'" + restResponse.StatusCode.ToString() + "'" + Environment.NewLine + restResponse.Content);
            }
        }

        #endregion

        #region Delete_in_PR

        internal static string LoadResponse_Delete_in_PR(IRestResponse restResponse, Guid G)
        {
            if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
            {
                SAB.Constants.SaveToFile.Save(Constants.Common.RES, "Delete_in_PR", new Classes.JsonFormatter(restResponse.Content).Format(), G, Constants.Common.json);
            }

            if (restResponse.StatusCode == HttpStatusCode.NoContent)
            {
                return "OK";
            }
            else
            {
                throw new Exception("Chyba :'" + restResponse.StatusCode.ToString() + "'" + Environment.NewLine + restResponse.Content);
            }
        }

        #endregion

        #region Edit_SERLTNUM_QTY_in_PR

        internal static string LoadResponse_Edit_SERLTNUM_QTY_in_PR(IRestResponse restResponse, Guid G)
        {
            if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
            {
                SAB.Constants.SaveToFile.Save(Constants.Common.RES, "Edit_SERLTNUM_QTY_in_PR", new Classes.JsonFormatter(restResponse.Content).Format(), G, Constants.Common.json);
            }

            if (restResponse.StatusCode == HttpStatusCode.OK)
            {
                return "OK";
            }
            else
            {
                throw new Exception("Chyba :'" + restResponse.StatusCode.ToString() + "'" + Environment.NewLine + restResponse.Content);
            }
        }

        internal static bool CreateRequest_Edit_SERLTNUM_QTY_in_PR(List<Polozky_Prijem> polEdit, Guid G, out string JSON)
        {
            JSON = string.Empty;

            try
            {
                Globals_V1.LoadConfiguration();

                Classes.ABRA_BO.Rootobject_PR ro = new Rootobject_PR();

                List<Classes.ABRA_BO.Row_PR> rows = new List<Row_PR>();

                foreach (Polozky_Prijem radek in polEdit)
                {
                    Row_PR row_PR = new Row_PR();
                    string idDL_Row = Database.ABRA.Get_ID_Polozka_Prijemka(radek);
                    row_PR.id = idDL_Row.Trim();

                    if (!string.IsNullOrEmpty(radek.SELTNUM))
                    {
                        float? qtyser = null;
                        if (radek.QTY_SELTNUM.HasValue)
                        {
                            qtyser = Convert.ToSingle(radek.QTY_SELTNUM.Value);
                        }

                        string ID_Sarze = Database.ABRA.Get_ID_StoreBatch(radek.SELTNUM.Trim(), radek.Expirace, radek.ITEMNMBR.Trim());

                        if (string.IsNullOrEmpty(ID_Sarze))
                        {
                            bool? track = null;

                            if(radek.CZ_SerNumTrack.HasValue)
                            {
                                if (radek.CZ_SerNumTrack.Value == 1)
                                    track = true;
                                else if (radek.CZ_SerNumTrack.Value == 2)
                                    track = false;

                            }

                            Classes.ABRA.Create_Sarzi(radek.ITEMNMBR.Trim(), radek.SELTNUM.Trim(), radek.Expirace, track, out ID_Sarze);
                            
                        }

                        var Sarze = new List<DocRowBatches_PR>();

                        if (!string.IsNullOrEmpty(ID_Sarze))
                        {
                            Sarze.Add(new DocRowBatches_PR()
                            {
                                StoreBatch_ID = ID_Sarze,
                                quantity = qtyser
                            });
                        }
                        else
                        {
                            throw new Exception("ID šarže/SN nenalezeno");

                            //Sarze.Add(new DocRowBatches_PR()
                            //{
                            //    NewBatch = true,
                            //    NewBatchName = radek.SELTNUM.Trim(),
                            //    NewBatchExpirationDate = radek.Expirace, // Tohle je Expirace...
                            //    quantity = qtyser
                            //});
                        }

                        row_PR.DocRowBatches = Sarze.ToArray();
                    }

                    if (radek.QTY.HasValue)
                    {
                        row_PR.quantity = Convert.ToSingle(radek.QTY.Value);
                    }

                    rows.Add(row_PR);
                }

                ro.rows = rows.ToArray();
                JSON = Classes.JSON_Class.Serialize_JSON(ro);

                if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
                {
                    SAB.Constants.SaveToFile.Save(Constants.Common.REQ, "Edit_SERLTNUM_QTY_in_PR", JSON, G, Constants.Common.json);
                }

                return true;
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }

        #endregion

        #region Create_PR_all

        internal static bool CreateRequest_Create_PR_all(Guid G, out string JSON)
        {
            JSON = string.Empty;

            try
            {
                Globals_V1.LoadConfiguration();

                Classes.WEBAPI.OV_to_PR oV_To_PR = new OV_to_PR();

                oV_To_PR._params = new Params()
                {
                    DocQueue_ID = Globals_V1.Konfigurace.ID_Rady[0].RadaID_Prijem_Prijemka.Trim()
                };

                JSON = Classes.JSON_Class.Serialize_JSON(oV_To_PR);

                if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
                {
                    SAB.Constants.SaveToFile.Save(Constants.Common.REQ, "Create_PR_all", JSON, G, Constants.Common.json);
                }

                return true;
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }

        internal static string LoadResponse_Create_PR_all(IRestResponse restResponse, Guid G, out string ID_PR)
        {
            ID_PR = null;
            if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
            {
                SAB.Constants.SaveToFile.Save(Constants.Common.RES, "Create_PR_all", new Classes.JsonFormatter(restResponse.Content).Format(), G, Constants.Common.json);
            }

            if (restResponse.StatusCode == HttpStatusCode.Created)
            {

                Classes.ABRA_BO.Rootobject_PR o = Newtonsoft.Json.JsonConvert.DeserializeObject<Classes.ABRA_BO.Rootobject_PR>(restResponse.Content);

                ID_PR = o.id;

                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info, "Vytvořen doklad PR ID:'" + o.id + "'");
                return "OK";
            }
            else
            {
                throw new Exception("Chyba :'" + restResponse.StatusCode.ToString() + "'" + Environment.NewLine + restResponse.Content);
            }
        }

        #endregion

        #endregion

        #region Inventura

        #region Edit_DIP_Mnozstvi

        internal static bool CreateRequest_Edit_DIP_Mnozstvi( SQL_Datasets.Inventura.CZMST_I4_GrupaRow row, string DIP, Guid G, out string JSON)
        {
            JSON = string.Empty;

            try
            {
                Globals_V1.LoadConfiguration();

                string jsonEditDip = string.Empty;

                //asses.ABRA_BO.Rootobject_DIP rootobject_DIP = new Rootobject_DIP();

                Classes.ABRA_BO.Row_DIP row_DIPs = new Row_DIP()
                {
                    qunit = row.IsMJNull() ? null : row.MJ,
                    unitrealquantity = Convert.ToSingle(row.QUANTITY),
                    realquantitychanged = true
                };
                

                //foreach (SQL_Datasets.Inventura.CZMST_I4_GrupaRow row in rowsI4_2)
                //{

                //    string ID_DipRow = Database.ABRA.Get_ID_DIPRow(DIP, row.SKL_ID, row.ITEMNMBR);


                //    List<Row_Batch_DIP> rowsSarzi = new List<Row_Batch_DIP>();

                //    string ID_DipBatchRow = Database.ABRA.Get_ID_DipBatchRow(DIP, row.SKL_ID, row.ITEMNMBR, row.SERLNMBR);

                //    rowsSarzi.Add( new Row_Batch_DIP(){
                //        id = ID_DipBatchRow,
                //        realquantity = Convert.ToSingle(row.QUANTITY)
                //    });

                //    row_DIPs.Add(new Row_DIP()
                //    {
                //        id = ID_DipRow,
                //        rows = rowsSarzi.ToArray()
                //    });
                //}

                JSON = Classes.JSON_Class.Serialize_JSON(row_DIPs);

                if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
                {
                    SAB.Constants.SaveToFile.Save(Constants.Common.REQ, "Edit_DIP_Mnozstvi", JSON, G, Constants.Common.json);
                }

                return true;
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }

        internal static string LoadResponse_Edit_DIP_Mnozstvi(IRestResponse restResponse, Guid G)
        {
            if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
            {
                SAB.Constants.SaveToFile.Save(Constants.Common.RES, "Edit_DIP_Mnozstvi", new Classes.JsonFormatter(restResponse.Content).Format(), G, Constants.Common.json);
            }

            if (restResponse.StatusCode == HttpStatusCode.OK)
            {
                return "OK";
            }
            else
            {
                throw new Exception("Chyba :'" + restResponse.StatusCode.ToString() + "'" + Environment.NewLine + restResponse.Content);
            }
        }

        #endregion

        #region Edit_DIP_ZnameSarze

        internal static bool CreateRequest_Edit_DIP_ZnameSarze(SQL_Datasets.Inventura.CZMST_I4_GrupaRow row, string DIP, Guid G, out string JSON)
        {
            JSON = string.Empty;

            try
            {
                Globals_V1.LoadConfiguration();
                
                List<Row_Batch_DIP> rowsSarzi = new List<Row_Batch_DIP>();

                DateTime? exp = row.IsExpiraceNull() ? (DateTime?)null : row.Expirace;

                string ID_DipBatchRow = Database.ABRA.Get_ID_DipBatchRow(DIP, row.SKL_ID, row.ITEMNMBR, row.SERLNMBR, exp );

                rowsSarzi.Add(new Row_Batch_DIP()
                {
                    id = ID_DipBatchRow,
                    qunit = row.IsMJNull() ? null : row.MJ,
                    unitrealquantity = Convert.ToSingle(row.QUANTITY)
                });

                Row_DIP row_DIP = new Row_DIP()
                {
                    rows = rowsSarzi.ToArray()
                };

                JSON = Classes.JSON_Class.Serialize_JSON(row_DIP);

                if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
                {
                    SAB.Constants.SaveToFile.Save(Constants.Common.REQ, "Edit_DIP_ZnameSarze", JSON, G, Constants.Common.json);
                }

                return true;
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }

        internal static string LoadResponse_Edit_DIP_ZnameSarze(IRestResponse restResponse, Guid G)
        {
            if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
            {
                SAB.Constants.SaveToFile.Save(Constants.Common.RES, "Edit_DIP_ZnameSarze", new Classes.JsonFormatter(restResponse.Content).Format(), G, Constants.Common.json);
            }

            if (restResponse.StatusCode == HttpStatusCode.OK)
            {
                return "OK";
            }
            else
            {
                throw new Exception("Chyba :'" + restResponse.StatusCode.ToString() + "'" + Environment.NewLine + restResponse.Content);
            }
        }

        #endregion

        #region Create_Sarzi

        internal static bool CreateRequest_Create_Sarzi(string ID_StoreCard, string Name, DateTime? exp, bool? SN,  Guid G, out string JSON)
        {
            JSON = string.Empty;

            try
            {
                Globals_V1.LoadConfiguration();

                Classes.ABRA_BO.Rootobject_SB SB = new Classes.ABRA_BO.Rootobject_SB();

                SB.storecard_id = ID_StoreCard.Trim();
                SB.name = Name.Trim();
                SB.serialnumber = SN;
                SB.expirationdatedate = exp;

                JSON = Classes.JSON_Class.Serialize_JSON(SB);

                if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
                {
                    SAB.Constants.SaveToFile.Save(Constants.Common.REQ, "Create_Sarzi", JSON, G, Constants.Common.json);
                }

                return true;
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }

        internal static string LoadResponse_Create_Sarzi(IRestResponse restResponse, Guid G, out string ID_SB)
        {
            ID_SB = null;
            if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
            {
                SAB.Constants.SaveToFile.Save(Constants.Common.RES, "Create_Sarzi", new Classes.JsonFormatter(restResponse.Content).Format(), G, Constants.Common.json);
            }

            if (restResponse.StatusCode == HttpStatusCode.Created)
            {

                Classes.ABRA_BO.Rootobject_SB o = Newtonsoft.Json.JsonConvert.DeserializeObject<Classes.ABRA_BO.Rootobject_SB>(restResponse.Content);

                ID_SB = o.id;

                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info, "Vytvořena šarže s ID:'" + o.id + "'");
                return "OK";
            }
            else
            {
                throw new Exception("Chyba :'" + restResponse.StatusCode.ToString() + "'" + Environment.NewLine + restResponse.Content);
            }
        }

        #endregion

        #region Create_HIPBatch

        internal static bool CreateRequest_Create_HIPBatch(string ID_Sarze, string MJ, Guid G, out string JSON)
        {
            JSON = string.Empty;

            try
            {
                Globals_V1.LoadConfiguration();

                List<Classes.ABRA_BO.Row_HIP> row_HIPs = new List<Row_HIP>();

                row_HIPs.Add(new Row_HIP() { 
                    qunit = MJ,
                    storebatch_id = ID_Sarze
                });

                Classes.ABRA_BO.Rootobject_HIP rootobject_HIP = new Classes.ABRA_BO.Rootobject_HIP()
                {
                    rows = row_HIPs.ToArray()
                };


                JSON = Classes.JSON_Class.Serialize_JSON(rootobject_HIP);

                if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
                {
                    SAB.Constants.SaveToFile.Save(Constants.Common.REQ, "Create_HIPBatch", JSON, G, Constants.Common.json);
                }

                return true;
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }

        internal static string LoadResponse_Create_HIPBatch(IRestResponse restResponse, Guid G,string ID_Sarze,  out string ID_HIPBatch)
        {
            if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
            {
                SAB.Constants.SaveToFile.Save(Constants.Common.RES, "Create_HIPBatch", new Classes.JsonFormatter(restResponse.Content).Format(), G, Constants.Common.json);
            }

            ID_HIPBatch = null;

            if (restResponse.StatusCode == HttpStatusCode.OK)
            {

                Classes.ABRA_BO.Rootobject_HIP o = Newtonsoft.Json.JsonConvert.DeserializeObject<Classes.ABRA_BO.Rootobject_HIP>(restResponse.Content);

                foreach (Row_HIP item in o.rows)
                {
                    if(item.storebatch_id.Trim() == ID_Sarze.Trim())
                    {
                        ID_HIPBatch = item.id;
                        return "OK";
                    }
                }

                return "ERR ID radku na HIP nenalezen";

            }
            else
            {
                throw new Exception("Chyba :'" + restResponse.StatusCode.ToString() + "'" + Environment.NewLine + restResponse.Content);
            }
        }

        #endregion

        #region Create_DIPBatch

        internal static bool CreateRequest_Create_DIPBatch(string MIPBatch_ID, decimal QTY, string MJ, Guid G, out string JSON)
        {
            JSON = string.Empty;

            try
            {
                Globals_V1.LoadConfiguration();


                List<Row_Batch_DIP> row_Batch_DIPs = new List<Row_Batch_DIP>();

                row_Batch_DIPs.Add(new Row_Batch_DIP() { 

                    MIPBatch_ID = MIPBatch_ID,
                    qunit =  MJ,
                    unitrealquantity = Convert.ToSingle(QTY)
                });


                Row_DIP row_DIP = new Row_DIP() { 
                    rows = row_Batch_DIPs.ToArray()
                };


                JSON = Classes.JSON_Class.Serialize_JSON(row_DIP);

                if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
                {
                    SAB.Constants.SaveToFile.Save(Constants.Common.REQ, "Create_DIPBatch", JSON, G, Constants.Common.json);
                }

                return true;
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }

        internal static string LoadResponse_Create_DIPBatch(IRestResponse restResponse, Guid G)
        {
            if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
            {
                SAB.Constants.SaveToFile.Save(Constants.Common.RES, "Create_DIPBatch", new Classes.JsonFormatter(restResponse.Content).Format(), G, Constants.Common.json);
            }

            if (restResponse.StatusCode == HttpStatusCode.OK)
            {
                return "OK";

            }
            else
            {
                throw new Exception("Chyba :'" + restResponse.StatusCode.ToString() + "'" + Environment.NewLine + restResponse.Content);
            }
        }

        #endregion

        #region Edit_Expirace

        internal static bool CreateRequest_Edit_Expirace(DateTime exp, Guid G, out string JSON)
        {
            JSON = string.Empty;

            try
            {
                Globals_V1.LoadConfiguration();

                Classes.ABRA_BO.Rootobject_SB SB = new Classes.ABRA_BO.Rootobject_SB();

                SB.expirationdatedate = exp;

                JSON = Classes.JSON_Class.Serialize_JSON(SB);

                if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
                {
                    SAB.Constants.SaveToFile.Save(Constants.Common.REQ, "Edit_Expirace", JSON, G, Constants.Common.json);
                }

                return true;
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }

        internal static string LoadResponse_Edit_Expirace(IRestResponse restResponse, Guid G)
        {
            if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
            {
                SAB.Constants.SaveToFile.Save(Constants.Common.RES, "Edit_Expirace", new Classes.JsonFormatter(restResponse.Content).Format(), G, Constants.Common.json);
            }

            if (restResponse.StatusCode == HttpStatusCode.OK)
            {
                return "OK";
            }
            else
            {
                throw new Exception("Chyba :'" + restResponse.StatusCode.ToString() + "'" + Environment.NewLine + restResponse.Content);
            }
        }

        #endregion

        #endregion

        #region Prevodka vydej

        #region CreateRequest_Edit_Stav_PRV

        internal static bool CreateRequest_Edit_Stav_PRV(string stav, Guid G, out string JSON)
        {
            JSON = string.Empty;

            try
            {
                Globals_V1.LoadConfiguration();

                Classes.WEBAPI.PMState pMState = new PMState();

                //pMState.pmstate_id = Database.ABRA.Get_ID_PMState(stav);
                pMState.pmstate_id = stav.Trim();

                JSON = Classes.JSON_Class.Serialize_JSON(pMState);

                if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
                {
                    SAB.Constants.SaveToFile.Save(Constants.Common.REQ, "Edit_Stav_PRV", JSON, G, Constants.Common.json);
                }

                return true;
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }

        internal static string LoadResponse_Edit_Stav_PRV(IRestResponse restResponse, Guid G)
        {
            if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
            {
                SAB.Constants.SaveToFile.Save(Constants.Common.RES, "Edit_Stav_PRV", new Classes.JsonFormatter(restResponse.Content).Format(), G, Constants.Common.json);
            }

            if (restResponse.StatusCode == HttpStatusCode.OK)
            {
                return "OK";
            }
            else
            {
                throw new Exception("Chyba :'" + restResponse.StatusCode.ToString() + "'" + Environment.NewLine + restResponse.Content);
            }
        }
        #endregion


        #region Vydej prelokovani

        internal static string LoadResponse_PRV_prelokovani(IRestResponse restResponse, Guid G)
        {
            if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
            {
                SAB.Constants.SaveToFile.Save(Constants.Common.RES, "LoadResponse_PRV_prelokovani", new Classes.JsonFormatter(restResponse.Content).Format(), G, Constants.Common.json);
            }

            if (restResponse.StatusCode == HttpStatusCode.OK)
            {
                return "OK";
            }
            else
            {
                throw new Exception("Chyba :'" + restResponse.StatusCode.ToString() + "'" + Environment.NewLine + restResponse.Content);
            }
        }

        #endregion

        #region Edit_SERLTNUM_QTY_in_PRV

        internal static string LoadResponse_Edit_SERLTNUM_QTY_in_PRV(IRestResponse restResponse, Guid G)
        {
            if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
            {
                SAB.Constants.SaveToFile.Save(Constants.Common.RES, "Edit_SERLTNUM_QTY_in_PRV", new Classes.JsonFormatter(restResponse.Content).Format(), G, Constants.Common.json);
            }

            if (restResponse.StatusCode == HttpStatusCode.OK)
            {
                return "OK";
            }
            else
            {
                throw new Exception("Chyba :'" + restResponse.StatusCode.ToString() + "'" + Environment.NewLine + restResponse.Content);
            }
        }

        internal static bool CreateRequest_Edit_SERLTNUM_QTY_in_PRV(List<Polozky_Vydej> polEdit, Guid G, out string JSON)
        {
            JSON = string.Empty;

            try
            {
                Globals_V1.LoadConfiguration();

                Classes.ABRA_BO.Rootobject_PRV ro = new Rootobject_PRV();

                List<Classes.ABRA_BO.Row_PRV> row = new List<Row_PRV>();

                //var tmpList = polEdit.GroupBy(x => new { x.ITEMNMBR, x.SOPNUMBE });

                //foreach (var item in tmpList)
                //{
                //    item.Key.ITEMNMBR
                //}

                foreach (Polozky_Vydej radek in polEdit)
                {
                    Row_PRV row_PRV = new Row_PRV();
                    string idPRV_Row = Database.ABRA.Get_ID_Polozka_PrevodkyVydej(radek);
                    row_PRV.id = idPRV_Row.Trim();

                    if (!string.IsNullOrEmpty(radek.SELTNUM))
                    {
                        // 15.5.2020 TaD Tady muže nastat chyba, že šarže neexistuje, DLE JaS je schvaleno na to nereagovat, tohle nastat nemuže...
                        string ID_Sarze = Database.ABRA.Get_ID_StoreBatch(radek.SELTNUM.Trim(), radek.Expirace, radek.ITEMNMBR.Trim());

                        if (string.IsNullOrEmpty(ID_Sarze))
                        {
                            throw new Exception("Tahle chyba dle JaS nenastane....");
                        }

                        var Sarze = new List<DocRowBatches_PRV>();
                        float? qtyser = null;
                        if (radek.QTY_SELTNUM.HasValue)
                        {
                            qtyser = Convert.ToSingle(radek.QTY_SELTNUM.Value);
                        }

                        Sarze.Add(new DocRowBatches_PRV()
                        {
                            StoreBatch_ID = ID_Sarze,
                            quantity = qtyser
                        });

                        row_PRV.DocRowBatches = Sarze.ToArray();
                    }

                    if (radek.QTY.HasValue)
                    {
                        row_PRV.quantity = Convert.ToSingle(radek.QTY.Value);
                    }

                    row.Add(row_PRV);
                }

                ro.rows = row.ToArray();
                JSON = Classes.JSON_Class.Serialize_JSON(ro);

                if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
                {
                    SAB.Constants.SaveToFile.Save(Constants.Common.REQ, "Edit_SERLTNUM_QTY_in_PRV", JSON, G, Constants.Common.json);
                }

                return true;
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }

        #endregion

        #region MyRegion

        internal static bool CreateRequest_PRV_prelokovani(DataSets.Vydej dsV, Guid G, out string JSON)
        {
            JSON = string.Empty;

            try
            {
                Globals_V1.LoadConfiguration();

                #region 3.12.2025 MaR OLD
                //Classes.ABRA_BO.Rootobject_PRV ro = new Rootobject_PRV();

                //List<Classes.ABRA_BO.Row_PRV> row = new List<Row_PRV>();

                ////var tmpList = polEdit.GroupBy(x => new { x.ITEMNMBR, x.SOPNUMBE });

                ////foreach (var item in tmpList)
                ////{
                ////    item.Key.ITEMNMBR
                ////}

                //foreach (Polozky_Vydej radek in polEdit)
                //{
                //    Row_PRV row_PRV = new Row_PRV();
                //    string idPRV_Row = Database.ABRA.Get_ID_Polozka_PrevodkyVydej(radek);
                //    row_PRV.id = idPRV_Row.Trim();

                //    if (!string.IsNullOrEmpty(radek.SELTNUM))
                //    {
                //        // 15.5.2020 TaD Tady muže nastat chyba, že šarže neexistuje, DLE JaS je schvaleno na to nereagovat, tohle nastat nemuže...
                //        string ID_Sarze = Database.ABRA.Get_ID_StoreBatch(radek.SELTNUM.Trim(), radek.Expirace, radek.ITEMNMBR.Trim());

                //        if (string.IsNullOrEmpty(ID_Sarze))
                //        {
                //            throw new Exception("Tahle chyba dle JaS nenastane....");
                //        }

                //        var Sarze = new List<DocRowBatches_PRV>();
                //        float? qtyser = null;
                //        if (radek.QTY_SELTNUM.HasValue)
                //        {
                //            qtyser = Convert.ToSingle(radek.QTY_SELTNUM.Value);
                //        }

                //        Sarze.Add(new DocRowBatches_PRV()
                //        {
                //            StoreBatch_ID = ID_Sarze,
                //            quantity = qtyser
                //        });

                //        row_PRV.DocRowBatches = Sarze.ToArray();
                //    }

                //    if (radek.QTY.HasValue)
                //    {
                //        row_PRV.quantity = Convert.ToSingle(radek.QTY.Value);
                //    }

                //    row.Add(row_PRV);
                //}

                //ro.rows = row.ToArray();
                //JSON = Classes.JSON_Class.Serialize_JSON(ro);
                #endregion

                #region 3.12.2025 MaR new
                Classes.ABRA_BO.Rootobject_PRV ro = new Rootobject_PRV();

                List<Classes.ABRA_BO.Row_PRV> row = new List<Row_PRV>();

                //var tmpList = polEdit.GroupBy(x => new { x.ITEMNMBR, x.SOPNUMBE });

                //foreach (var item in tmpList)
                //{
                //    item.Key.ITEMNMBR
                //}

                foreach (var radek in dsV.CZMST_SI)
                {
                    //Row_PRV row_PRV = new Row_PRV();
                    //string idPRV_Row = Database.ABRA.Get_ID_Polozka_PrevodkyVydej_prelokovani(radek);
                    //row_PRV.id = radek.CountEntries.ToString();

                    Row_PRV row_PRV = new Row_PRV();
                    string idPRV_Row = Database.ABRA.Get_ID_Polozka_PrevodkyVydej_prelokovani(radek);
                    row_PRV.id = idPRV_Row.Trim();


                    //if (!string.IsNullOrEmpty(radek.SERLTNUM))
                    //{
                    //    // 15.5.2020 TaD Tady muže nastat chyba, že šarže neexistuje, DLE JaS je schvaleno na to nereagovat, tohle nastat nemuže...
                    //    string ID_Sarze = Database.ABRA.Get_ID_StoreBatch(radek.SERLTNUM.Trim(), radek.Expirace, radek.ITEMNMBR.Trim());

                    //    if (string.IsNullOrEmpty(ID_Sarze))
                    //    {
                    //        throw new Exception("Tahle chyba dle JaS nenastane....");
                    //    }

                    //    var Sarze = new List<DocRowBatches_PRV>();
                    //    float? qtyser = null;
                    //    if (radek.QTY_SELTNUM.HasValue)
                    //    {
                    //        qtyser = Convert.ToSingle(radek.QTY_SELTNUM.Value);
                    //    }

                    //    Sarze.Add(new DocRowBatches_PRV()
                    //    {
                    //        StoreBatch_ID = ID_Sarze,
                    //        quantity = qtyser
                    //    });

                    //    row_PRV.DocRowBatches = Sarze.ToArray();
                    //}

                    //if (radek.QTY.HasValue)
                    //{
                    //    row_PRV.quantity = Convert.ToSingle(radek.QTY.Value);
                    //}

                    row_PRV.StorePlace_ID = radek.LOCNCODE;

                    row.Add(row_PRV);
                }

                ro.rows = row.ToArray();
                JSON = Classes.JSON_Class.Serialize_JSON(ro);
                #endregion

                if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
                {
                    SAB.Constants.SaveToFile.Save(Constants.Common.REQ, "CreateRequest_PRV_prelokovani", JSON, G, Constants.Common.json);
                }

                return true;
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }


     


        #endregion


        #region Delete_in_PRV

        internal static string LoadResponse_Delete_in_PRV(IRestResponse restResponse, Guid G)
        {
            if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
            {
                SAB.Constants.SaveToFile.Save(Constants.Common.RES, "Delete_in_PRV", new Classes.JsonFormatter(restResponse.Content).Format(), G, Constants.Common.json);
            }

            if (restResponse.StatusCode == HttpStatusCode.NoContent)
            {
                return "OK";
            }
            else
            {
                throw new Exception("Chyba :'" + restResponse.StatusCode.ToString() + "'" + Environment.NewLine + restResponse.Content);
            }
        }

        #endregion

        #region Import_DL_to_FV_all

        internal static bool CreateRequest_Import_PRV_to_PRP_all(Guid G, string DocQueue_ID, string Store_ID, out string JSON)
        {
            JSON = string.Empty;

            try
            {

                Globals_V1.LoadConfiguration();

                Classes.WEBAPI.PRV_to_PRP pRV_to_PRP = new PRV_to_PRP();

                pRV_to_PRP._params = new Params_PRP()
                {
                    DocQueue_ID = DocQueue_ID,
                    Store_ID = Store_ID
                };

                JSON = Classes.JSON_Class.Serialize_JSON(pRV_to_PRP);

                if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
                {
                    SAB.Constants.SaveToFile.Save(Constants.Common.REQ, "PRV_to_PRP_all", JSON, G, Constants.Common.json);
                }

                return true;
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }

        internal static Rootobject_PRP LoadResponse_Import_PRV_to_PRP_all(IRestResponse restResponse, Guid G)
        {
            if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
            {
                SAB.Constants.SaveToFile.Save(Constants.Common.RES, "PRV_to_PRP_all", new Classes.JsonFormatter(restResponse.Content).Format(), G, Constants.Common.json);
            }

            if (restResponse.StatusCode == HttpStatusCode.Created)
            {
                Classes.ABRA_BO.Rootobject_PRP o = Newtonsoft.Json.JsonConvert.DeserializeObject<Classes.ABRA_BO.Rootobject_PRP>(restResponse.Content);

                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info, "Vytvořen doklad ID:'" + o.id + "'");
                return o;
            }
            else
            {
                throw new Exception("Chyba :'" + restResponse.StatusCode.ToString() + "'" + Environment.NewLine + restResponse.Content);
            }
        }

        #endregion
        #endregion

    }
}
