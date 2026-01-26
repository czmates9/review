using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Fask.RestSharp.API;
using Fask.Logging;
using System.Net;
using RestSharp;
using Fask.Interfaces.DataSets;
using Fask.ModuleSql_API.Classes;
using static Fask.ModuleSql_API.Classes.Comunication;
using Fask.WEBAPI;

namespace Fask.ModuleSql_API
{
    public partial class Provider   :
        Fask.Interfaces.Vyroba.VPP.IVPP,
        Fask.Interfaces.Vyroba.VPP.IVPP_GetDataByCountEntriesSOPNUMBEITEMNMBR,
        Fask.Interfaces.Vyroba.VPP.IVPP_GetDataByCountEntriesSopnumbeItemnmbrBarcodeP,
        Fask.Interfaces.Vyroba.VPP.IVPP_FillByCountEntriesAndSOPNUMBE,
        Fask.Interfaces.Vyroba.VPP.IVPP_DeleteByCountEntriesSOPNUMBE,
        Fask.Interfaces.Vyroba.VPP.IVPP_Insert,
        Fask.Interfaces.Vyroba.VPP.IVPP_Update,
        Fask.Interfaces.Vyroba.VPP.IVPP_Fill,
        Fask.Interfaces.Vyroba.VPP.IVPP_Update_Row,
        Fask.Interfaces.Vyroba.VPP.IVPP_GetFiltrovanyVPPList
    {

        #region IVPP_GetDataByCountEntriesSOPNUMBEITEMNMBR Members

        public Vyroba.CZPRO_VPPDataTable GetDataByCountEntriesSOPNUMBEITEMNMBR(int CountEntries, string SOPNUMBE, string ITEMNMBR)
        {

            Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP bo = new Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP();
            Vyroba.CZPRO_VPPDataTable dt = new Vyroba.CZPRO_VPPDataTable();
            //bool result = false;

            try
            {
                #region skladani URL
                HttpValueCollection URI_param_tmp = new HttpValueCollection();
                string tmp = null;

                URI_param_tmp.Add("CountEntries", CountEntries.ToString());

                if (!string.IsNullOrEmpty(SOPNUMBE))
                    URI_param_tmp.Add("SOPNUMBE", SOPNUMBE);

                if (!string.IsNullOrEmpty(ITEMNMBR))
                    URI_param_tmp.Add("ITEMNMBR", ITEMNMBR);

                tmp += "?";
                tmp += URI_param_tmp.ToString();
                #endregion

                string param = "Konzola_GetDataByCountEntriesSOPNUMBEITEMNMBR" + tmp;
                string JSON = "";
                IRestResponse restResponse;

                if (!Communicate(Terminal_ID, REST_Type.GET, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    bo = Newtonsoft.Json.JsonConvert.DeserializeObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP>(restResponse.Content);

                    //TODO MaR naplnit dt z bo
                    BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP, Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP_row, Vyroba.CZPRO_VPPDataTable, Vyroba.CZPRO_VPPRow> y = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP, Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP_row, Vyroba.CZPRO_VPPDataTable, Vyroba.CZPRO_VPPRow>();
                     dt = y.GetDTFromBO(bo);
                     return dt;
                }

                 return null;
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                 return null;
            }
        }

        #endregion

        #region IVPP_GetDataByCountEntriesSopnumbeItemnmbrBarcodeP Members

        public Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable GetDataByCountEntriesSopnumbeItemnmbrBarcodeP(int CountEntries, string SOPNUMBE, string ITEMNMBR, string BarcodeP)
        {

            Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP bo = new Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP();
            Vyroba.CZPRO_VPPDataTable dt = new Vyroba.CZPRO_VPPDataTable();
            //bool result = false;

            try
            {
                #region skladani URL
                HttpValueCollection URI_param_tmp = new HttpValueCollection();
                string tmp = null;

                URI_param_tmp.Add("CountEntries", CountEntries.ToString());

                if (!string.IsNullOrEmpty(SOPNUMBE))
                    URI_param_tmp.Add("SOPNUMBE", SOPNUMBE);

                if (!string.IsNullOrEmpty(ITEMNMBR))
                    URI_param_tmp.Add("ITEMNMBR", ITEMNMBR);

                if (!string.IsNullOrEmpty(BarcodeP))
                    URI_param_tmp.Add("BarcodeP", BarcodeP);

                tmp += "?";
                tmp += URI_param_tmp.ToString();
                #endregion

                string param = "Konzola_GetDataByCountEntriesSopnumbeItemnmbrBarcodeP" + tmp;
                string JSON = "";
                IRestResponse restResponse;

                if (!Communicate(Terminal_ID, REST_Type.GET, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    bo = Newtonsoft.Json.JsonConvert.DeserializeObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP>(restResponse.Content);

                    //TODO MaR naplnit dt z bo
                    BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP, Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP_row, Vyroba.CZPRO_VPPDataTable, Vyroba.CZPRO_VPPRow> y = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP, Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP_row, Vyroba.CZPRO_VPPDataTable, Vyroba.CZPRO_VPPRow>();
                    dt = y.GetDTFromBO(bo);
                    return dt;
                }

                return null;
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                return null;
            }
        }

        #endregion

        #region IVPP_FillByCountEntriesAndSOPNUMBE Members

        /// <summary>
        /// Dohledani VPP
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="TypeORDERBY"></param>
        /// <param name="CountEntries"></param>
        /// <param name="SOPNUMBE"></param>
        /// <returns>metoda vraci vzdy hodnotu -1, jde spise o vyplneny dataset</returns>
        public int FillByCountEntriesAndSOPNUMBE(Fask.Interfaces.DataSets.Vyroba ds, string TypeORDERBY, int CountEntries, string SOPNUMBE)
        {
            Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP bo = new Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP();
            Vyroba.CZPRO_VPPDataTable dt = new Vyroba.CZPRO_VPPDataTable();
            //int result = -1;
            ds.CZPRO_VPP.Clear();

            try
            {
                #region skladani URL
                HttpValueCollection URI_param_tmp = new HttpValueCollection();
                string tmp = null;

                URI_param_tmp.Add("CountEntries", CountEntries.ToString());

                if (!string.IsNullOrEmpty(SOPNUMBE))
                    URI_param_tmp.Add("SOPNUMBE", SOPNUMBE);

                if (!string.IsNullOrEmpty(TypeORDERBY))
                    URI_param_tmp.Add("TypeORDERBY", TypeORDERBY);


                tmp += "?";
                tmp += URI_param_tmp.ToString();
                #endregion

                string param = "Konzola_FillByCountEntriesAndSOPNUMBE" + tmp;
                string JSON = "";
                IRestResponse restResponse;

                if (!Communicate(Terminal_ID, REST_Type.GET, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    bo = Newtonsoft.Json.JsonConvert.DeserializeObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP>(restResponse.Content);

                    //TODO MaR naplnit dt z bo
                    BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP, Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP_row, Vyroba.CZPRO_VPPDataTable, Vyroba.CZPRO_VPPRow> y = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP, Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP_row, Vyroba.CZPRO_VPPDataTable, Vyroba.CZPRO_VPPRow>();
                    dt = y.GetDTFromBO(bo);

                    foreach (var item in dt)
                    {
                        ds.CZPRO_VPP.ImportRow(item);
                    }

                    ds.CZPRO_VPP.AcceptChanges();

                    return -1;
                }

                return -1;
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                return -1;
            }

        }

        #endregion

        #region IVPP_DeleteByCountEntriesSOPNUMBE Members

        public void DeleteByCountEntriesSOPNUMBE(int CountEntries, string SOPNUMBE)
        {
            Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP bo = new Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP();
            Vyroba.CZPRO_VPPDataTable dt = new Vyroba.CZPRO_VPPDataTable();
            bool result = false;
            try
            {
                #region skladani URL
                HttpValueCollection URI_param_tmp = new HttpValueCollection();
                string tmp = null;

                URI_param_tmp.Add("CountEntries", CountEntries.ToString());

                if (!string.IsNullOrEmpty(SOPNUMBE))
                    URI_param_tmp.Add("SOPNUMBE", SOPNUMBE);


                tmp += "?";
                tmp += URI_param_tmp.ToString();
                #endregion

                string param = "Konzola_DeleteByCountEntriesSOPNUMBE" + tmp;
                string JSON = "";
                IRestResponse restResponse;

                if (!Communicate(Terminal_ID, REST_Type.DELETE, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                   result = Newtonsoft.Json.JsonConvert.DeserializeObject<bool>(restResponse.Content);


                    //overeni ze to probehlo v poradku??

                }

            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
            }

        }

        #endregion

        #region IVPP_Insert Members

        //public int VPP_Insert(int CountEntries, string SOPNUMBE, string ITEMNMBR, string ITEMTYPE, string ITEMDESC, string ITEMMJ, string VNDDOCNMP, string VNDITNUM, int ORD, string BarcodeP, string LOCNCODE, decimal QTYSHPPD, decimal QTYDOKON, decimal QTYPACK, string QTYPACKMJ, int TIMEMODE, float TIMEPREP, float TIMEUNIT, byte DtProdT, short DtProdL, byte SerNumT, short SerNumL, byte VerT, short VerL, byte TermID, DateTime LSTMod, byte BarcodeT)
        //{

           
        //    int result;

        //    try
        //    {
        //        #region skladani URL
        //        HttpValueCollection URI_param_tmp = new HttpValueCollection();
        //        string tmp = null;

        //        URI_param_tmp.Add("CountEntries", CountEntries.ToString());

                

        //        if (SOPNUMBE != null)
        //            URI_param_tmp.Add("SOPNUMBE", SOPNUMBE);

        //        if (ITEMNMBR != null)
        //            URI_param_tmp.Add("ITEMNMBR", ITEMNMBR);

        //        if (ITEMTYPE != null)
        //            URI_param_tmp.Add("ITEMTYPE", ITEMTYPE);

        //        if (ITEMDESC != null)
        //            URI_param_tmp.Add("ITEMDESC", ITEMDESC);

        //        if (ITEMMJ != null)
        //            URI_param_tmp.Add("ITEMMJ", ITEMMJ);

        //        if (VNDDOCNMP != null)
        //            URI_param_tmp.Add("VNDDOCNMP", VNDDOCNMP);

        //        if (VNDITNUM != null)
        //            URI_param_tmp.Add("VNDITNUM", VNDITNUM);

        //        URI_param_tmp.Add("ORD", ORD.ToString());

        //        if (BarcodeP != null)
        //            URI_param_tmp.Add("BarcodeP", BarcodeP);

        //        URI_param_tmp.Add("LOCNCODE", LOCNCODE);

        //        URI_param_tmp.Add("QTYSHPPD", QTYSHPPD.ToString());

        //        URI_param_tmp.Add("QTYDOKON", QTYDOKON.ToString());

        //        URI_param_tmp.Add("QTYPACK", QTYPACK.ToString());

        //        if (QTYPACKMJ != null)
        //            URI_param_tmp.Add("QTYPACKMJ", QTYPACKMJ); 

        //        URI_param_tmp.Add("TIMEMODE", TIMEMODE.ToString());

        //        URI_param_tmp.Add("TIMEPREP", TIMEPREP.ToString());

        //        URI_param_tmp.Add("TIMEUNIT", TIMEUNIT.ToString());

        //        URI_param_tmp.Add("DtProdT", DtProdT.ToString());

        //        URI_param_tmp.Add("DtProdL", DtProdL.ToString());

        //        URI_param_tmp.Add("SerNumT", SerNumT.ToString());

        //        URI_param_tmp.Add("SerNumL", SerNumL.ToString());

        //        URI_param_tmp.Add("VerT", VerT.ToString());

        //        URI_param_tmp.Add("VerL", VerL.ToString());

        //        URI_param_tmp.Add("TermID", TermID.ToString());

        //        if (LSTMod != null)
        //            URI_param_tmp.Add("LSTMod", LSTMod.ToString(System.Globalization.DateTimeFormatInfo.InvariantInfo));

        //        URI_param_tmp.Add("BarcodeT", BarcodeT.ToString());

        //        tmp += "?";
        //        tmp += URI_param_tmp.ToString();
        //        #endregion

        //        string param = "Konzola_VPP_Insert_Values" + tmp;
        //        string JSON = "";
        //        IRestResponse restResponse;

        //        #region logovani JSON request
        //        Guid G = Guid.NewGuid(); // Parovaci GUID ked je zapnute logovani...
        //        if (Globals_V1.Konfigurace.Nastaveni[0].LogovatRequestyResponsy)
        //        {
        //            SaveToFile.Save(JSON_Logs.URL, "VPP_Insert(parameters)", param, G, JSON_Logs.txt);
        //        }
        //        #endregion
        //        if (!Communicate(Terminal_ID, REST_Type.GET, out restResponse, param, JSON))
        //        {
        //            ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
        //        }

        //        #region logovani JSON response
        //        if (Globals_V1.Konfigurace.Nastaveni[0].LogovatRequestyResponsy)
        //        {
        //            SaveToFile.Save(JSON_Logs.URL, "VPP_Insert(parameters)", new JsonFormatter(restResponse.Content).Format(), G, JSON_Logs.txt);
        //        }
        //        #endregion

        //        if (restResponse.StatusCode == HttpStatusCode.OK)
        //        {
        //            result = Newtonsoft.Json.JsonConvert.DeserializeObject<int>(restResponse.Content);

        //            //TODO MaR naplnit dt z bo

        //             return result;
        //        }

        //        return -1;
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionHandler2.Handle(ex);
        //         return -1;
        //    }
        //}

        public void VPP_Insert_Row(Vyroba.CZPRO_VPPRow row)
        {
            Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP bo = new Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP();
            Vyroba.CZPRO_VPPDataTable dt = new Vyroba.CZPRO_VPPDataTable();
            bool result;

            try
            {

                //dt.AddCZPRO_VPHRow(row);
                dt.AcceptChanges();
                dt.ImportRow(row);

                //dt na bo
                BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP, Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP_row, Vyroba.CZPRO_VPPDataTable, Vyroba.CZPRO_VPPRow> x = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP, Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP_row, Vyroba.CZPRO_VPPDataTable, Vyroba.CZPRO_VPPRow>();
                bo = x.GetBOFromDT(dt);

                string param = "Konzola_VPP_Insert_Row";
                string JSON = "";
                IRestResponse restResponse;
                JSON = JSON_Class.Serialize_JSON(bo);



                #region logovani JSON request
#if true
                Guid G = Guid.NewGuid(); // Parovaci GUID ked je zapnute logovani...
                if (Globals_V1.Konfigurace.Nastaveni[0].LogovatRequestyResponsy)
                {
                    SaveToFile.Save(JSON_Logs.URL, "VPP_Insert_Row(CZPRO_VPPRow)-request", new JsonFormatter(JSON).Format(), G, JSON_Logs.txt);
                }
#endif
                #endregion

                if (!Communicate(Terminal_ID, REST_Type.POST, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }


                #region logovani JSON response
#if true
                if (Globals_V1.Konfigurace.Nastaveni[0].LogovatRequestyResponsy)
                {
                    SaveToFile.Save(JSON_Logs.URL, "VPP_Insert_Row(CZPRO_VPHRow)-response", new JsonFormatter(restResponse.Content).Format(), G, JSON_Logs.txt);
                }
#endif
                #endregion

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<bool>(restResponse.Content);
                }




            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
            }

            // return Database.Vyroba_CZPRO_VPH.Update(dt, ConnectionString);
            //return -1;
        }


        #endregion

        #region IVPP_Update Members

        public void VPP_Update(Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable dt)
        {

            Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP bo = new Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP();
           // Vyroba.CZPRO_VPPDataTable dt = new Vyroba.CZPRO_VPPDataTable();
            bool result = false;

            try
            {

                //dt na bo
                BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP, Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP_row, Vyroba.CZPRO_VPPDataTable, Vyroba.CZPRO_VPPRow> x = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP, Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP_row, Vyroba.CZPRO_VPPDataTable, Vyroba.CZPRO_VPPRow>();
                bo = x.GetBOFromDT(dt);

                string param = "Konzola_VPP_Update";
                string JSON = "";
                IRestResponse restResponse;
                JSON = JSON_Class.Serialize_JSON(bo);

               

                #region logovani JSON request
                Guid G = Guid.NewGuid(); // Parovaci GUID ked je zapnute logovani...
                if (Globals_V1.Konfigurace.Nastaveni[0].LogovatRequestyResponsy)
                {
                    SaveToFile.Save(JSON_Logs.URL, "VPP_Update(CZPRO_VPPDataTable)", new JsonFormatter(JSON).Format(), G, JSON_Logs.txt);
                }
                #endregion
                if (!Communicate(Terminal_ID, REST_Type.POST, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                #region logovani JSON response
                if (Globals_V1.Konfigurace.Nastaveni[0].LogovatRequestyResponsy)
                {
                    SaveToFile.Save(JSON_Logs.URL, "VPP_Update(CZPRO_VPPDataTable)", new JsonFormatter(restResponse.Content).Format(), G, JSON_Logs.txt);
                }
                #endregion

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<bool>(restResponse.Content);
                }

            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
            }
        }

        #endregion

        #region IVPP_Fill Members

        public void VPP_Fill(Fask.Interfaces.DataSets.Vyroba ds)
        {
            Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP bo = new Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP();
            Vyroba.CZPRO_VPPDataTable dt = new Vyroba.CZPRO_VPPDataTable();
            //int result = -1;
            ds.CZPRO_VPP.Clear();

            try
            {
                string param = "Konzola_VPP_Fill";
                string JSON = "";
                IRestResponse restResponse;

                if (!Communicate(Terminal_ID, REST_Type.GET, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    bo = Newtonsoft.Json.JsonConvert.DeserializeObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP>(restResponse.Content);

                    //TODO MaR naplnit dt z bo
                    BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP, Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP_row, Vyroba.CZPRO_VPPDataTable, Vyroba.CZPRO_VPPRow> y = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP, Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP_row, Vyroba.CZPRO_VPPDataTable, Vyroba.CZPRO_VPPRow>();
                    dt = y.GetDTFromBO(bo);

                    foreach (var item in dt)
                    {
                        ds.CZPRO_VPP.ImportRow(item);
                    }

                    ds.CZPRO_VPP.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
            }
        }

        #endregion

        #region IVPP_Update_Row Members

        public void VPP_Update_Row(Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPRow row)
        {
            Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP bo = new Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP();
            Vyroba.CZPRO_VPPDataTable dt = new Vyroba.CZPRO_VPPDataTable();
            bool result = false;

            try
            {

                //dt.AddCZPRO_VPHRow(row);
                dt.AcceptChanges();
                dt.ImportRow(row);


                //dt na bo
                BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP, Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP_row, Vyroba.CZPRO_VPPDataTable, Vyroba.CZPRO_VPPRow> x = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP, Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP_row, Vyroba.CZPRO_VPPDataTable, Vyroba.CZPRO_VPPRow>();
                bo = x.GetBOFromDT(dt);

                string param = "Konzola_VPP_Update_Row";
                string JSON = "";
                IRestResponse restResponse;
                JSON = JSON_Class.Serialize_JSON(bo);

                #region logovani JSON request
                Guid G = Guid.NewGuid(); // Parovaci GUID ked je zapnute logovani...
                if (Globals_V1.Konfigurace.Nastaveni[0].LogovatRequestyResponsy)
                {
                    SaveToFile.Save(JSON_Logs.URL, "VPP_Update_Row(CZPRO_VPPRow)", new JsonFormatter(JSON).Format(), G, JSON_Logs.txt);
                }
                #endregion
                if (!Communicate(Terminal_ID, REST_Type.POST, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                #region logovani JSON response
                if (Globals_V1.Konfigurace.Nastaveni[0].LogovatRequestyResponsy)
                {
                    SaveToFile.Save(JSON_Logs.URL, "VPP_Update_Row(CZPRO_VPPRow)", new JsonFormatter(restResponse.Content).Format(), G, JSON_Logs.txt);
                }
                #endregion

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<bool>(restResponse.Content);
                }

            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
            }
        }

        #endregion

        #region IVPP_GetFiltrovanyVPPList Members   

        #region IVPP_GetFiltrovanyVPPList Members   

        public Fask.Interfaces.DataSets.Vyroba GetFiltrovanyVPPList(Fask.Interfaces.Filtry.Vyroba_VPP_Filtr filtr)
        {
            Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP bo = new Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP();
            Vyroba ds = new Vyroba();

            try
            {
                string param = "Konzola_GetFiltrovanyVPPList";
                string JSON = "";
                IRestResponse restResponse;
                JSON = JSON_Class.Serialize_JSON(filtr);

                if (!Communicate(Terminal_ID, REST_Type.POST, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s DB se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    bo = Newtonsoft.Json.JsonConvert.DeserializeObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP>(restResponse.Content);

                    //TODO MaR naplnit dt z bo
                    BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP, Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP_row, Vyroba.CZPRO_VPPDataTable, Vyroba.CZPRO_VPPRow> y = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP, Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPP_row, Vyroba.CZPRO_VPPDataTable, Vyroba.CZPRO_VPPRow>();
                    var x = y.GetDTFromBO(bo);

                    foreach (var item in x)
                    {
                        ds.CZPRO_VPP.ImportRow(item);
                    }

                    ds.CZPRO_VPP.AcceptChanges();

                    return ds;
                }

                return null;
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                return null;
            }

        }

        #endregion

        #endregion

        #region Pomocne metody

        #region CZPRO_VPP OK

        internal int Update_CZPRO_VPP(object data)
        {
            System.Data.SqlClient.SqlTransaction transaction = null;
            SqlConnection connection = null;
            try
            {
                int result = 0;
                using (connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    transaction = connection.BeginTransaction();

                    using (var commandInsert = connection.CreateCommand())
                    using (var commandUpdate = connection.CreateCommand())
                    using (var commandDelete = connection.CreateCommand())
                    using (var commandSelect = connection.CreateCommand())
                    {

                        commandInsert.Transaction = transaction;
                        commandUpdate.Transaction = transaction;
                        commandDelete.Transaction = transaction;
                        commandSelect.Transaction = transaction;

                        InitializeCommandInsert_CZPRO_VPP(commandInsert);
                        InitializeCommandUpdate_CZPRO_VPP(commandUpdate);
                        InitializeCommandDelete_CZPRO_VPP(commandDelete);
                        InitializeCommandSelect_CZPRO_VPP(commandSelect);

                        using (var adapter = new System.Data.SqlClient.SqlDataAdapter())
                        {
                            adapter.DeleteCommand = commandDelete;
                            adapter.InsertCommand = commandInsert;
                            adapter.UpdateCommand = commandUpdate;
                            adapter.SelectCommand = commandSelect;

                            var dataIsDataSet = data as System.Data.DataSet;
                            var dataIsDataTable = data as System.Data.DataTable;
                            var dataIsDataRow = data as System.Data.DataRow;
                            var dataIsDataRowArray = data as System.Data.DataRow[];

                            //if (data is System.Data.DataSet)
                            if (dataIsDataSet != null)
                                result = adapter.Update(dataIsDataSet, dataIsDataSet.Tables[0].TableName);
                            else if (dataIsDataTable != null)
                                result = adapter.Update(dataIsDataTable);
                            else if (dataIsDataRow != null)
                                result = adapter.Update(new System.Data.DataRow[] { dataIsDataRow });
                            else if (dataIsDataRowArray != null)
                                result = adapter.Update(dataIsDataRowArray);
                            else
                                throw new Exception(String.Format("Neodpovídající datový typ: {0}", data.GetType().ToString()));
                        }
                    }

                    transaction.Commit();

                }
                return result;
            }
            catch (Exception ex)
            {
                //Logging.Log.Write(ex);
                Logging.ExceptionHandler2.Handle(ex);

                try
                {
                    if (transaction != null)
                        transaction.Rollback();
                }
                catch (Exception exTransaction)
                {
                    //Logging.Log.Write(exTransaction);
                    Logging.ExceptionHandler2.Handle(exTransaction);
                }

                throw ex;
            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }

        #region Inicialize metody

        public void InitializeCommandInsert_CZPRO_VPP(SqlCommand command)
        {
            command.CommandText = @"INSERT INTO [CZPRO_VPP] (" + 
                " [CountEntries], [SOPNUMBE], [ITEMNMBR], [ITEMTYPE], [ITEMDESC], " + 
                " [ITEMMJ], [VNDDOCNMP], [VNDITNUM], [ORD], [BarcodeP], " + 
                " [LOCNCODE], [QTYSHPPD], [QTYDOKON], [QTYPACK], [QTYPACKMJ], " + 
                " [TIMEMODE], [TIMEPREP], [TIMEUNIT], [DtProdT], [DtProdL], " + 
                " [SerNumT], [SerNumL], [VerT], [VerL], [TermID], " +
                " [LSTMod], [BarcodeT]" + 
                " ) VALUES (" + 
                " @CountEntries, @SOPNUMBE, @ITEMNMBR, @ITEMTYPE, @ITEMDESC, " + 
                " @ITEMMJ, @VNDDOCNMP, @VNDITNUM, @ORD, @BarcodeP, " + 
                " @LOCNCODE, @QTYSHPPD, @QTYDOKON, @QTYPACK, @QTYPACKMJ, " + 
                " @TIMEMODE, @TIMEPREP, @TIMEUNIT, @DtProdT, @DtProdL, " + 
                " @SerNumT, @SerNumL, @VerT, @VerL, @TermID, " +
                " @LSTMod, @BarcodeT" + 
                " ) ";

            command.Parameters.Add(new SqlParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@SOPNUMBE", DbType = System.Data.DbType.String, SourceColumn = "SOPNUMBE", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, SourceColumn = "ITEMNMBR", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMTYPE", DbType = System.Data.DbType.String, SourceColumn = "ITEMTYPE", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMDESC", DbType = System.Data.DbType.String, SourceColumn = "ITEMDESC", SourceVersion = DataRowVersion.Current });

            command.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMMJ", DbType = System.Data.DbType.String, SourceColumn = "ITEMMJ", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@VNDDOCNMP", DbType = System.Data.DbType.String, SourceColumn = "VNDDOCNMP", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@VNDITNUM", DbType = System.Data.DbType.String, SourceColumn = "VNDITNUM", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@ORD", DbType = System.Data.DbType.Int32, SourceColumn = "ORD", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@BarcodeP", DbType = System.Data.DbType.String, SourceColumn = "BarcodeP", SourceVersion = DataRowVersion.Current });

            command.Parameters.Add(new SqlParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, SourceColumn = "LOCNCODE", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@QTYSHPPD", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYSHPPD", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@QTYDOKON", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYDOKON", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@QTYPACK", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYPACK", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@QTYPACKMJ", DbType = System.Data.DbType.String, SourceColumn = "QTYPACKMJ", SourceVersion = DataRowVersion.Current });

            command.Parameters.Add(new SqlParameter() { ParameterName = "@TIMEMODE", DbType = System.Data.DbType.Int32, SourceColumn = "TIMEMODE", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@TIMEPREP", DbType = System.Data.DbType.Single, SourceColumn = "TIMEPREP", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@TIMEUNIT", DbType = System.Data.DbType.Single, SourceColumn = "TIMEUNIT", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@DtProdT", DbType = System.Data.DbType.Byte, SourceColumn = "DtProdT", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@DtProdL", DbType = System.Data.DbType.Int16, SourceColumn = "DtProdL", SourceVersion = DataRowVersion.Current });

            command.Parameters.Add(new SqlParameter() { ParameterName = "@SerNumT", DbType = System.Data.DbType.Byte, SourceColumn = "SerNumT", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@SerNumL", DbType = System.Data.DbType.Int16, SourceColumn = "SerNumL", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@VerT", DbType = System.Data.DbType.Byte, SourceColumn = "VerT", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@VerL", DbType = System.Data.DbType.Int16, SourceColumn = "VerL", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@TermID", DbType = System.Data.DbType.Int16, SourceColumn = "TermID", SourceVersion = DataRowVersion.Current });

            command.Parameters.Add(new SqlParameter() { ParameterName = "@LSTMod", DbType = System.Data.DbType.DateTime, SourceColumn = "LSTMod", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@BarcodeT", DbType = System.Data.DbType.Byte, SourceColumn = "BarcodeT", SourceVersion = DataRowVersion.Current });
            
            

        }

        public void InitializeCommandUpdate_CZPRO_VPP(SqlCommand command)
        {
            command.CommandText = "UPDATE [CZPRO_VPP] SET " +
                " [CountEntries] = @CountEntries, " +
                " [SOPNUMBE] = @SOPNUMBE, " +
                " [ITEMNMBR] = @ITEMNMBR, " +
                " [ITEMTYPE] = @ITEMTYPE, " +
                " [ITEMDESC] = @ITEMDESC, " +
                " [ITEMMJ] = @ITEMMJ, " +
                " [VNDDOCNMP] = @VNDDOCNMP," +
                " [VNDITNUM] = @VNDITNUM, " +
                " [ORD] = @ORD, " +
                " [BarcodeP] = @BarcodeP, " +
                " [LOCNCODE] = @LOCNCODE, " +
                " [QTYSHPPD] = @QTYSHPPD, " +
                " [QTYDOKON] = @QTYDOKON, " +
                " [QTYPACK] = @QTYPACK, " +
                " [QTYPACKMJ] = @QTYPACKMJ, " +
                " [TIMEMODE] = @TIMEMODE, " +
                " [TIMEPREP] = @TIMEPREP, " +
                " [TIMEUNIT] = @TIMEUNIT, " +
                " [DtProdT] = @DtProdT, " +
                " [DtProdL] = @DtProdL, " +
                " [SerNumT] = @SerNumT, " +
                " [SerNumL] = @SerNumL, " +
                " [VerT] = @VerT, " +
                " [VerL] = @VerL, " +
                " [TermID] = @TermID, " +
                " [LSTMod] = @LSTMod, " +
                " [BarcodeT] = @BarcodeT " +
                " WHERE " +
                " ( " +

                " ([CountEntries] = @Original_CountEntries) " +
                " AND ([SOPNUMBE] = @Original_SOPNUMBE) " +
                " AND ([ITEMNMBR] = @Original_ITEMNMBR) " +
                " AND ([ITEMTYPE] = @Original_ITEMTYPE) " +

                " AND (" +
                " (@IsNull_ITEMDESC = 1 AND [ITEMDESC] IS NULL) " +
                " OR ([ITEMDESC] = @Original_ITEMDESC) " +
                " ) " +

                " AND (" +
                " (@IsNull_ITEMMJ = 1 AND [ITEMMJ] IS NULL) " +
                " OR ([ITEMMJ] = @Original_ITEMMJ)" +
                " ) " +

                " AND (" +
                "(@IsNull_VNDDOCNMP = 1 AND [VNDDOCNMP] IS NULL) " +
                " OR ([VNDDOCNMP] = @Original_VNDDOCNMP)" +
                ") " +

                " AND (" +
                " (@IsNull_VNDITNUM = 1 AND [VNDITNUM] IS NULL) " +
                " OR ([VNDITNUM] = @Original_VNDITNUM)" +
                ") " +

                " AND ([ORD] = @Original_ORD) " +
                " AND ([BarcodeP] = @Original_BarcodeP) " +

                " AND (" +
                " (@IsNull_LOCNCODE = 1 AND [LOCNCODE] IS NULL) " +
                " OR ([LOCNCODE] = @Original_LOCNCODE)" +
                ") " +

                " AND ([QTYSHPPD] = @Original_QTYSHPPD) " +
                " AND ([QTYDOKON] = @Original_QTYDOKON) " +
                " AND ([QTYPACK] = @Original_QTYPACK) " +

                " AND ( " +
                " (@IsNull_QTYPACKMJ = 1 AND [QTYPACKMJ] IS NULL) " +
                " OR ([QTYPACKMJ] = @Original_QTYPACKMJ)" +
                " ) " +

                " AND ([TIMEMODE] = @Original_TIMEMODE) " +
                " AND ([TIMEPREP] = @Original_TIMEPREP) " +
                " AND ([TIMEUNIT] = @Original_TIMEUNIT) " +
                " AND ([DtProdT] = @Original_DtProdT) " +
                " AND ([DtProdL] = @Original_DtProdL) " +
                " AND ([SerNumT] = @Original_SerNumT) " +
                " AND ([SerNumL] = @Original_SerNumL) " +
                " AND ([VerT] = @Original_VerT) " +
                " AND ([VerL] = @Original_VerL) " +
                " AND ([TermID] = @Original_TermID) " +
                " AND ([LSTMod] = @Original_LSTMod) " +
                " AND ([DEX_ROW_ID] = @Original_DEX_ROW_ID)" +
                " AND ([BarcodeT] = @Original_BarcodeT)" +
                " )";


            command.Parameters.Add(new SqlParameter() { ParameterName = "@CountEntries", DbType = DbType.Int32, SourceColumn = "CountEntries", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@SOPNUMBE", DbType = DbType.String, SourceColumn = "SOPNUMBE", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMNMBR", DbType = DbType.String, SourceColumn = "ITEMNMBR", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMTYPE", DbType = DbType.String, SourceColumn = "ITEMTYPE", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMDESC", DbType = DbType.String, SourceColumn = "ITEMDESC", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMMJ", DbType = DbType.String, SourceColumn = "ITEMMJ", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@VNDDOCNMP", DbType = DbType.String, SourceColumn = "VNDDOCNMP", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@VNDITNUM", DbType = DbType.String, SourceColumn = "VNDITNUM", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@ORD", DbType = DbType.Int32, SourceColumn = "ORD", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@BarcodeP", DbType = DbType.String, SourceColumn = "BarcodeP", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@LOCNCODE", DbType = DbType.String, SourceColumn = "LOCNCODE", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@QTYSHPPD", DbType = DbType.Decimal, SourceColumn = "QTYSHPPD", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@QTYDOKON", DbType = DbType.Decimal, SourceColumn = "QTYDOKON", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@QTYPACK", DbType = DbType.Decimal, SourceColumn = "QTYPACK", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@QTYPACKMJ", DbType = DbType.String, SourceColumn = "QTYPACKMJ", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@TIMEMODE", DbType = DbType.Int32, SourceColumn = "TIMEMODE", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@TIMEPREP", DbType = DbType.Single, SourceColumn = "TIMEPREP", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@TIMEUNIT", DbType = DbType.Single, SourceColumn = "TIMEUNIT", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@DtProdT", DbType = DbType.Byte, SourceColumn = "DtProdT", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@DtProdL", DbType = DbType.Int16, SourceColumn = "DtProdL", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@SerNumT", DbType = DbType.Byte, SourceColumn = "SerNumT", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@SerNumL", DbType = DbType.Int16, SourceColumn = "SerNumL", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@VerT", DbType = DbType.Byte, SourceColumn = "VerT", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@VerL", DbType = DbType.Int16, SourceColumn = "VerL", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@TermID", DbType = DbType.Byte, SourceColumn = "TermID", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@LSTMod", DbType = DbType.DateTime, SourceColumn = "LSTMod", SourceVersion = DataRowVersion.Current });

            command.Parameters.Add(new SqlParameter() { ParameterName = "@BarcodeT", DbType = DbType.Byte, SourceColumn = "BarcodeT", SourceVersion = DataRowVersion.Current });

            command.Parameters.Add(new SqlParameter() { ParameterName = "@Original_CountEntries", DbType = DbType.Int32, SourceColumn = "CountEntries", SourceVersion = DataRowVersion.Original });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@Original_SOPNUMBE", DbType = DbType.String, SourceColumn = "SOPNUMBE", SourceVersion = DataRowVersion.Original });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@Original_ITEMNMBR", DbType = DbType.String, SourceColumn = "ITEMNMBR", SourceVersion = DataRowVersion.Original });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@Original_ITEMTYPE", DbType = DbType.String, SourceColumn = "ITEMTYPE", SourceVersion = DataRowVersion.Original });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@IsNull_ITEMDESC", DbType = DbType.Int32, SourceColumn = "ITEMDESC", SourceVersion = DataRowVersion.Original });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@Original_ITEMDESC", DbType = DbType.String, SourceColumn = "ITEMDESC", SourceVersion = DataRowVersion.Original });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@IsNull_ITEMMJ", DbType = DbType.Int32, SourceColumn = "ITEMMJ", SourceVersion = DataRowVersion.Original });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@Original_ITEMMJ", DbType = DbType.String, SourceColumn = "ITEMMJ", SourceVersion = DataRowVersion.Original });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@IsNull_VNDDOCNMP", DbType = DbType.Int32, SourceColumn = "VNDDOCNMP", SourceVersion = DataRowVersion.Original });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@Original_VNDDOCNMP", DbType = DbType.String, SourceColumn = "VNDDOCNMP", SourceVersion = DataRowVersion.Original });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@IsNull_VNDITNUM", DbType = DbType.Int32, SourceColumn = "VNDITNUM", SourceVersion = DataRowVersion.Original });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@Original_VNDITNUM", DbType = DbType.String, SourceColumn = "VNDITNUM", SourceVersion = DataRowVersion.Original });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@Original_ORD", DbType = DbType.Int32, SourceColumn = "ORD", SourceVersion = DataRowVersion.Original });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@Original_BarcodeP", DbType = DbType.String, SourceColumn = "BarcodeP", SourceVersion = DataRowVersion.Original });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@IsNull_LOCNCODE", DbType = DbType.Int32, SourceColumn = "LOCNCODE", SourceVersion = DataRowVersion.Original });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@Original_LOCNCODE", DbType = DbType.String, SourceColumn = "LOCNCODE", SourceVersion = DataRowVersion.Original });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@Original_QTYSHPPD", DbType = DbType.Decimal, SourceColumn = "QTYSHPPD", SourceVersion = DataRowVersion.Original });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@Original_QTYDOKON", DbType = DbType.Decimal, SourceColumn = "QTYDOKON", SourceVersion = DataRowVersion.Original });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@Original_QTYPACK", DbType = DbType.Decimal, SourceColumn = "QTYPACK", SourceVersion = DataRowVersion.Original });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@IsNull_QTYPACKMJ", DbType = DbType.Int32, SourceColumn = "QTYPACKMJ", SourceVersion = DataRowVersion.Original });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@Original_QTYPACKMJ", DbType = DbType.String, SourceColumn = "QTYPACKMJ", SourceVersion = DataRowVersion.Original });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@Original_TIMEMODE", DbType = DbType.Int32, SourceColumn = "TIMEMODE", SourceVersion = DataRowVersion.Original });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@Original_TIMEPREP", DbType = DbType.Single, SourceColumn = "TIMEPREP", SourceVersion = DataRowVersion.Original });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@Original_TIMEUNIT", DbType = DbType.Single, SourceColumn = "TIMEUNIT", SourceVersion = DataRowVersion.Original });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@Original_DtProdT", DbType = DbType.Byte, SourceColumn = "DtProdT", SourceVersion = DataRowVersion.Original });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@Original_DtProdL", DbType = DbType.Int16, SourceColumn = "DtProdL", SourceVersion = DataRowVersion.Original });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@Original_SerNumT", DbType = DbType.Byte, SourceColumn = "SerNumT", SourceVersion = DataRowVersion.Original });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@Original_SerNumL", DbType = DbType.Int16, SourceColumn = "SerNumL", SourceVersion = DataRowVersion.Original });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@Original_VerT", DbType = DbType.Byte, SourceColumn = "VerT", SourceVersion = DataRowVersion.Original });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@Original_VerL", DbType = DbType.Int16, SourceColumn = "VerL", SourceVersion = DataRowVersion.Original });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@Original_TermID", DbType = DbType.Byte, SourceColumn = "TermID", SourceVersion = DataRowVersion.Original });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@Original_LSTMod", DbType = DbType.DateTime, SourceColumn = "LSTMod", SourceVersion = DataRowVersion.Original });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@Original_DEX_ROW_ID", DbType = DbType.Int32, SourceColumn = "DEX_ROW_ID", SourceVersion = DataRowVersion.Original });

            command.Parameters.Add(new SqlParameter() { ParameterName = "@Original_BarcodeT", DbType = System.Data.DbType.Byte, SourceColumn = "BarcodeT", SourceVersion = DataRowVersion.Original });

        }

        public void InitializeCommandDelete_CZPRO_VPP(SqlCommand command)
        {
            command.CommandText = "DELETE FROM CZPRO_VPP WHERE (CountEntries = @Original_CountEntries) " + 
                " AND (SOPNUMBE = @Original_SOPNUMBE) AND (ITEMNMBR = @Original_ITEMNMBR)";

            command.Parameters.Add(new SqlParameter()
            {
                ParameterName = "@Original_CountEntries",
                DbType = System.Data.DbType.Int32,
                SourceColumn = "CountEntries",
                SourceVersion = System.Data.DataRowVersion.Original
            });

            command.Parameters.Add(new SqlParameter()
            {
                ParameterName = "@Original_SOPNUMBE",
                DbType = System.Data.DbType.String,
                SourceColumn = "SOPNUMBE",
                SourceVersion = System.Data.DataRowVersion.Original
            });

            command.Parameters.Add(new SqlParameter()
            {
                ParameterName = "@Original_ITEMNMBR",
                DbType = System.Data.DbType.String,
                SourceColumn = "ITEMNMBR",
                SourceVersion = System.Data.DataRowVersion.Original
            });

        }

        public void InitializeCommandSelect_CZPRO_VPP(SqlCommand command)
        {
            command.CommandText = "Select * from CZPRO_VPP";
        }


        #endregion

        #endregion

        #endregion
    }
}
