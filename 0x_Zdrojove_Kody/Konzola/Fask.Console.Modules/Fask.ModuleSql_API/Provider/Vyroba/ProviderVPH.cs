using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Fask.RestSharp.API;
using RestSharp;
using Fask.Logging;
using System.Net;
using Fask.Interfaces.DataSets;
using Fask.ModuleSql_API.Classes;
using static Fask.ModuleSql_API.Classes.Comunication;
using Fask.WEBAPI;

namespace Fask.ModuleSql_API
{
    public partial class Provider :
        Fask.Interfaces.Vyroba.VPH.IVPH,
        Fask.Interfaces.Vyroba.VPH.IVPH_Fill,
        Fask.Interfaces.Vyroba.VPH.IVPH_Update,
        Fask.Interfaces.Vyroba.VPH.IVPH_GetDataByCountEntriesSOPNUMBE,
        Fask.Interfaces.Vyroba.VPH.IVPH_Insert,
        Fask.Interfaces.Vyroba.VPH.IVPH_Update_Row,
        Fask.Interfaces.Vyroba.VPH.IVPH_GetFiltrovanyVPHList

    {
        #region IVPH_Fill Members

        //static object x = new object();

        public void VPH_Fill(Fask.Interfaces.DataSets.Vyroba ds)
        {
            Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH bo = new Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH();
            Vyroba.CZPRO_VPHDataTable dt = new Vyroba.CZPRO_VPHDataTable();

            try
            {
                //TODO MaR ds na bo

                string param = "Konzola_VPH_Fill";
                string JSON = "";
                IRestResponse restResponse;


                #region logovani JSON request
#if false
                Guid G = Guid.NewGuid(); // Parovaci GUID ked je zapnute logovani...
                if (Globals_V1.Konfigurace.Nastaveni[0].LogovatRequestyResponsy)
                {
                    SaveToFile.Save(JSON_Logs.URL, "Insert_Row(CZPRO_VPHRow)", new JsonFormatter(JSON).Format(), G, JSON_Logs.txt);
                }  
#endif
                #endregion


                if (!Communicate(Terminal_ID, REST_Type.GET, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                #region logovani JSON response
#if false
                if (Globals_V1.Konfigurace.Nastaveni[0].LogovatRequestyResponsy)
                {
                    SaveToFile.Save(JSON_Logs.URL, "Insert_Row(CZPRO_VPHRow)", new JsonFormatter(restResponse.Content).Format(), G, JSON_Logs.txt);
                } 
#endif 
                #endregion

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    bo = Newtonsoft.Json.JsonConvert.DeserializeObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH>(restResponse.Content);
                    //TODO MaR bo na ds a vratit objekt!!
                    //Fask.Interfaces.BusinessObject<BO_CZPRO_VPH> x = new Interfaces.BusinessObject<BO_CZPRO_VPH>(bo);
                    //x.GetBOFromDT(ds.CZPRO_VPH);

                    BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH, Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH_row, Vyroba.CZPRO_VPHDataTable, Vyroba.CZPRO_VPHRow> y = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH, Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH_row, Vyroba.CZPRO_VPHDataTable, Vyroba.CZPRO_VPHRow>();
                    dt = y.GetDTFromBO(bo);

         

                        ds.CZPRO_VPH.Clear();
                        ds.CZPRO_VPH.AcceptChanges();
                        ds.CZPRO_VPH.BeginLoadData();

                        foreach (Vyroba.CZPRO_VPHRow item in dt)
                        {
                            ds.CZPRO_VPH.AddCZPRO_VPHRow(
                                item.CountEntries,
                                item.SOPNUMBE,
                                item.SOPTYPE,
                                item.SOPDESC,
                                item.VNDDOCNMH,
                                item.BarcodeH,
                                item.LOCNCODE,
                                item.DateProd,
                                item.Rez1,
                                item.Rez2,
                                item.TermID,
                                item.LSTMod,
                                item.Active,
                                item.USERID
                                );
                        }

                        ds.CZPRO_VPH.EndLoadData();
              
                }

            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
            }

        }

        #endregion

        #region IVPH_Update Members

        public int Update(Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHDataTable dt)
        {
            Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH bo = new Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH();
            int result;

            try
            {
                //dt na bo
                BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH, Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH_row, Vyroba.CZPRO_VPHDataTable, Vyroba.CZPRO_VPHRow> x = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH, Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH_row, Vyroba.CZPRO_VPHDataTable, Vyroba.CZPRO_VPHRow>();
                bo = x.GetBOFromDT(dt);

                string param = "Konzola_VPH_Update";
                string JSON = "";
                IRestResponse restResponse;
                JSON = JSON_Class.Serialize_JSON(bo);

                if (!Communicate(Terminal_ID, REST_Type.POST, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<int>(restResponse.Content);


                    return result;
                }

                return -1;
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                return -1;
            }

            // return Database.Vyroba_CZPRO_VPH.Update(dt, ConnectionString);
            //return -1;
        }

        #endregion

        #region IVPH_GetDataByCountEntriesSOPNUMBE Members

        public Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHDataTable GetDataByCountEntriesSOPNUMBE(int CountEntries, string SOPNUMBE)
        {

            Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH bo = new Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH();
            Vyroba.CZPRO_VPHDataTable dt = new Vyroba.CZPRO_VPHDataTable();

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

                string param = "Konzola_GetDataByCountEntriesSOPNUMBE" + tmp;
                string JSON = "";
                IRestResponse restResponse;

                if (!Communicate(Terminal_ID, REST_Type.GET, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    bo = Newtonsoft.Json.JsonConvert.DeserializeObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH>(restResponse.Content);

                    //TODO MaR naplnit dt z bo
                    BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH, Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH_row, Vyroba.CZPRO_VPHDataTable, Vyroba.CZPRO_VPHRow> y = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH, Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH_row, Vyroba.CZPRO_VPHDataTable, Vyroba.CZPRO_VPHRow>();
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

        #region IVPH_Insert Members

        //public void Insert_Row(int CountEntries, string SOPNUMBE, string SOPTYPE, string SOPDESC, string VNDDOCNMH, string BarcodeH, string LOCNCODE, short DateProd, string Rez1, string Rez2, byte TermID, DateTime LSTMod, byte Active)
        //{

        //    Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH bo = new Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH();
        //    Vyroba.CZPRO_VPHDataTable dt = new Vyroba.CZPRO_VPHDataTable();
        //    bool result = false;

        //    try
        //    {
        //        #region skladani URL
        //        HttpValueCollection URI_param_tmp = new HttpValueCollection();
        //        string tmp = null;

        //        URI_param_tmp.Add("CountEntries", CountEntries.ToString());

        //        if (!string.IsNullOrEmpty(SOPNUMBE))
        //            URI_param_tmp.Add("SOPNUMBE", SOPNUMBE);

        //        if (!string.IsNullOrEmpty(SOPTYPE))
        //            URI_param_tmp.Add("SOPTYPE", SOPTYPE);

        //        if (!string.IsNullOrEmpty(SOPDESC))
        //            URI_param_tmp.Add("SOPDESC", SOPDESC);

        //        if (!string.IsNullOrEmpty(VNDDOCNMH))
        //            URI_param_tmp.Add("VNDDOCNMH", VNDDOCNMH);

        //        if (!string.IsNullOrEmpty(BarcodeH))
        //            URI_param_tmp.Add("BarcodeH", BarcodeH);

        //        if (!string.IsNullOrEmpty(LOCNCODE))
        //            URI_param_tmp.Add("LOCNCODE", LOCNCODE);

        //            URI_param_tmp.Add("DateProd", DateProd.ToString());

        //        if (!string.IsNullOrEmpty(Rez1))
        //            URI_param_tmp.Add("Rez1", Rez1);

        //        if (!string.IsNullOrEmpty(Rez2))
        //            URI_param_tmp.Add("Rez2", Rez2);

        //            URI_param_tmp.Add("TermID", TermID.ToString());

        //        if (LSTMod != null)
        //            URI_param_tmp.Add("LSTMod", LSTMod.ToString(System.Globalization.DateTimeFormatInfo.InvariantInfo));

        //            URI_param_tmp.Add("Active", Active.ToString());

        //        tmp += "?";
        //        tmp += URI_param_tmp.ToString();
        //        #endregion

        //        string param = "Konzola_VPH_Insert_Row_Values" + tmp;
        //        string JSON = "";
        //        IRestResponse restResponse;


        //        #region logovani JSON request
        //        Guid G = Guid.NewGuid(); // Parovaci GUID ked je zapnute logovani...
        //        if (Globals_V1.Konfigurace.Nastaveni[0].LogovatRequestyResponsy)
        //        {
        //            SaveToFile.Save(JSON_Logs.URL, "VPH_Insert_Row(parameters)", new JsonFormatter(JSON).Format(), G, JSON_Logs.txt);
        //        }
        //        #endregion
        //        if (!Communicate(Terminal_ID, REST_Type.GET, out restResponse, param, JSON))
        //        {
        //            ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
        //        }

        //        #region logovani JSON response
        //        if (Globals_V1.Konfigurace.Nastaveni[0].LogovatRequestyResponsy)
        //        {
        //            SaveToFile.Save(JSON_Logs.URL, "VPH_Insert_Row(parameters)", new JsonFormatter(restResponse.Content).Format(), G, JSON_Logs.txt);
        //        }
        //        #endregion

        //        if (restResponse.StatusCode == HttpStatusCode.OK)
        //        {
        //            result = Newtonsoft.Json.JsonConvert.DeserializeObject<bool>(restResponse.Content);

        //            //TODO MaR naplnit dt z bo

        //           // return dt;
        //        }

        //       // return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionHandler2.Handle(ex);
        //       // return null;
        //    }

        //    ////zmenit na:
        //    //Database.Vyroba_CZPRO_VPH.Insert_VPH(ConnectionString,
        //    //   CountEntries,
        //    //   SOPNUMBE,
        //    //   SOPTYPE,
        //    //   SOPDESC,
        //    //   VNDDOCNMH,
        //    //   BarcodeH,
        //    //   LOCNCODE,
        //    //   DateProd,
        //    //   Rez1,
        //    //   Rez2,
        //    //   TermID,
        //    //   LSTMod,
        //    //   Active
        //    //   );

        //}

        public void Insert(Vyroba.CZPRO_VPHRow row)
        {
            Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH bo = new Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH();
            Vyroba.CZPRO_VPHDataTable dt = new Vyroba.CZPRO_VPHDataTable();
            bool result;

            try
            {

                //dt.AddCZPRO_VPHRow(row);
                dt.AcceptChanges();
                dt.ImportRow(row);

                //dt na bo
                BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH, Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH_row, Vyroba.CZPRO_VPHDataTable, Vyroba.CZPRO_VPHRow> x = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH, Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH_row, Vyroba.CZPRO_VPHDataTable, Vyroba.CZPRO_VPHRow>();
                bo = x.GetBOFromDT(dt);

                string param = "Konzola_VPH_Insert_Row";
                string JSON = "";
                IRestResponse restResponse;
                JSON = JSON_Class.Serialize_JSON(bo);

          

                #region logovani JSON request
#if true
                Guid G = Guid.NewGuid(); // Parovaci GUID ked je zapnute logovani...
                if (Globals_V1.Konfigurace.Nastaveni[0].LogovatRequestyResponsy)
                {
                    SaveToFile.Save(JSON_Logs.URL, "VPH_Insert_Row(CZPRO_VPHRow)", new JsonFormatter(JSON).Format(), G, JSON_Logs.txt);
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
                    SaveToFile.Save(JSON_Logs.URL, "VPH_Insert_Row(CZPRO_VPHRow)", new JsonFormatter(restResponse.Content).Format(), G, JSON_Logs.txt);
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

        #region IVPH_Update_Row Members

        public int Update_Row(Vyroba.CZPRO_VPHRow Row)
        {

            Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH bo = new Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH();
            Vyroba.CZPRO_VPHDataTable dt = new Vyroba.CZPRO_VPHDataTable();
            int result;

            try
            {
                //dt.AddCZPRO_VPHRow(Row);
                dt.AcceptChanges();
                dt.ImportRow(Row);
               

                //dt na bo
                BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH, Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH_row, Vyroba.CZPRO_VPHDataTable, Vyroba.CZPRO_VPHRow> x = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH, Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH_row, Vyroba.CZPRO_VPHDataTable, Vyroba.CZPRO_VPHRow>();
                bo = x.GetBOFromDT(dt);

                string param = "Konzola_VPH_Update_Row";
                string JSON = "";
                IRestResponse restResponse;
                JSON = JSON_Class.Serialize_JSON(bo);


                #region logovani JSON request
                Guid G = Guid.NewGuid(); // Parovaci GUID ked je zapnute logovani...
                if (Globals_V1.Konfigurace.Nastaveni[0].LogovatRequestyResponsy)
                {
                    SaveToFile.Save(JSON_Logs.URL, "Update_Row(CZPRO_VPHRow)", new JsonFormatter(JSON).Format(), G, JSON_Logs.txt);
                }
                #endregion
                if (!Communicate(Terminal_ID, REST_Type.POST, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                #region logovani JSON response
                if (Globals_V1.Konfigurace.Nastaveni[0].LogovatRequestyResponsy)
                {
                    SaveToFile.Save(JSON_Logs.URL, "Update_Row(CZPRO_VPHRow)", new JsonFormatter(restResponse.Content).Format(), G, JSON_Logs.txt);
                }
                #endregion

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<int>(restResponse.Content);


                    return result;
                }

     

                return -1;
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                return -1;
            }


            //  return Database.Vyroba_CZPRO_VPH.Update(Row, ConnectionString);
            // return -1;
        }



        #endregion

        #region IVPH_GetFiltrovanyVPHList Members   

        public Fask.Interfaces.DataSets.Vyroba GetFiltrovanyVPHList(Fask.Interfaces.Filtry.Vyroba_VPH_Filtr filtr)
        {
            Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH bo = new Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH();
            Vyroba ds = new Vyroba();

            try
            {
                string param = "Konzola_GetFiltrovanyVPHList";
                string JSON = "";
                IRestResponse restResponse;
                JSON = JSON_Class.Serialize_JSON(filtr);

                if (!Communicate(Terminal_ID, REST_Type.POST, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s DB se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    bo = Newtonsoft.Json.JsonConvert.DeserializeObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH>(restResponse.Content);

                    //TODO MaR naplnit dt z bo
                    BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH, Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH_row, Vyroba.CZPRO_VPHDataTable, Vyroba.CZPRO_VPHRow> y = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH, Fask.WEBAPI.API_BusinessObjects.BO_CZPRO_VPH_row, Vyroba.CZPRO_VPHDataTable, Vyroba.CZPRO_VPHRow>();
                    var x  = y.GetDTFromBO(bo);

                    foreach (var item in x)
                    {
                        ds.CZPRO_VPH.ImportRow(item);
                    }

                    ds.CZPRO_VPH.AcceptChanges();

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
    }
}
