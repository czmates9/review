using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Fask.Interfaces.DataSets;
using RestSharp;
using Fask.Logging;
using System.Net;
using Fask.Extension;
using Fask.RestSharp.API;
using static Fask.ModuleSql_API.Classes.Comunication;

namespace Fask.ModuleSql_API
{
    public partial class Provider :
        Fask.Interfaces.Ciselniky.Zbozi.IZbozi2,
        Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_DeleteZbozi,
        Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_GetFiltrovaneZbozi,
        Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_GetZbozi,
        Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_GetZboziByID,
        Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_InsertZbozi,
        Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_UpdateZbozi,
        Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_ImportZbozi,
        Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_UpdateZboziParams,
        Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_InsertZboziParams
    {

        #region IZbozi2

        /// <summary>
        /// Metoda pro smazani řadku z FASK_ZBOZI a FASK_ZBOZI_PARAMETRY 
        /// </summary>
        /// <param name="id_ZBOZI">ID řadku v FASK_ZBOZI</param>
        /// <param name="ID_Params">ID řadku v FASK_ZBOZI_PARAMETRY</param>
        /// <returns>True-OK, False- chyba</returns>
        public bool DeleteZbozi(int id_ZBOZI, int? ID_Params)
        {
  
            bool stav = false;
            try
            {
                #region skladani URL
                HttpValueCollection URI_param_tmp = new HttpValueCollection();
                string tmp = null;

                URI_param_tmp.Add("id_ZBOZI", id_ZBOZI.ToString());

                if (ID_Params.HasValue)
                    URI_param_tmp.Add("ID_Params", ID_Params.ToString());

                tmp += "?";
                tmp += URI_param_tmp.ToString(); 
                #endregion

                string param = "Konzola_DeleteZbozi" + tmp;
                string JSON = "";
                IRestResponse restResponse;
                //JSON = JSON_Class.Serialize_JSON(filtr);

                if (!Communicate(Terminal_ID, REST_Type.DELETE, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    stav = Newtonsoft.Json.JsonConvert.DeserializeObject<bool>(restResponse.Content);

                    return stav;
                }

                return false;
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                return false;
            }

        }

        /// <summary>
        /// Metoda která podle zadaných filtrú dotahne FASK_ZBOZI left join FASK_ZBOZI_PARAMETRY
        /// </summary>
        /// <param name="filtr">Filtr s podminkama</param>
        /// <returns>Dataset Zbozi naplnen zbožím</returns>
        public Fask.Interfaces.DataSets.Zbozi GetFiltrovaneZbozi(Fask.Interfaces.Filtry.ZboziListFiltr filtr)
        {
            Fask.Interfaces.DataSets.Zbozi ds = new Fask.Interfaces.DataSets.Zbozi();
            Fask.WEBAPI.API_BusinessObjects.FASK_ZASOBY_ALL FZ_data = new Fask.WEBAPI.API_BusinessObjects.FASK_ZASOBY_ALL();
            try
            {
                IRestResponse restResponse;
                string param = "Konzola_GetFiltrovaneZbozi";
                string JSON = "";

                JSON = Classes.JSON_Class.Serialize_JSON(filtr);

                if (!Communicate(Terminal_ID, REST_Type.POST, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    FZ_data = Newtonsoft.Json.JsonConvert.DeserializeObject<Fask.WEBAPI.API_BusinessObjects.FASK_ZASOBY_ALL>(restResponse.Content);

                    //FZ_data = ds.DataSetToBO();
                    //ds = FZ_data.BOToDataSet();
                    foreach (var item in FZ_data.BOToDataSet())
                    {
                        ds.FASK_ZASOBY_ALL_KONZOLA.ImportRow(item);
                    }

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

        /// <summary>
        /// Metoda která vratí všechno zboži z tabulky FASK_ZBOZI left join FASK_ZBOZI_PARAMETRY 
        /// </summary>
        /// <returns>Dataset Zbozi naplnen zbožím</returns>
        public Zbozi GetZbozi()
        {

            Zbozi ds = new Zbozi();
            Fask.WEBAPI.API_BusinessObjects.FASK_ZASOBY_ALL FZ_data = new Fask.WEBAPI.API_BusinessObjects.FASK_ZASOBY_ALL();
            try
            {
                IRestResponse restResponse;
                string param = "Konzola_GetZbozi";
                string JSON = "";

                //JSON = JSON_Class.Serialize_JSON(USERID);

                if (!Communicate(Terminal_ID, REST_Type.GET, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    FZ_data = Newtonsoft.Json.JsonConvert.DeserializeObject<Fask.WEBAPI.API_BusinessObjects.FASK_ZASOBY_ALL>(restResponse.Content);

                    //FZ_data = ds.DataSetToBO();

                    foreach (var item in FZ_data.BOToDataSet())
                    {
                        ds.FASK_ZASOBY_ALL_KONZOLA.ImportRow(item);
                    }

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

        /// <summary>
        /// Metoda která vratí jeden řadek zboži z tabulky FASK_ZBOZI left join FASK_ZBOZI_PARAMETRY
        /// </summary>
        /// <param name="id">ID Zboží</param>
        /// <returns>DataRow jeden řadek zboží</returns>
        public Zbozi.FASK_ZASOBY_ALL_KONZOLARow GetZboziByID(string id)
        {
            Zbozi ds = new Zbozi();
            Fask.WEBAPI.API_BusinessObjects.FASK_ZASOBY_ALL FZ_data = new Fask.WEBAPI.API_BusinessObjects.FASK_ZASOBY_ALL();
            try
            {

                IRestResponse restResponse;
                string param = "Konzola_GetZboziByID" + "/" + id;
                string JSON = "";

                //JSON = JSON_Class.Serialize_JSON(FZ_data);

                if (!Communicate(Terminal_ID, REST_Type.GET, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    FZ_data = Newtonsoft.Json.JsonConvert.DeserializeObject<Fask.WEBAPI.API_BusinessObjects.FASK_ZASOBY_ALL>(restResponse.Content);

                    //FZ_data = ds.DataSetToBO();
                    //ds = FZ_data.BOToDataSet();
                    foreach (var item in FZ_data.BOToDataSet())
                    {
                        ds.FASK_ZASOBY_ALL_KONZOLA.ImportRow(item);
                    }

                    return ds.FASK_ZASOBY_ALL_KONZOLA.First();
                }

                return null;
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                return null;
            }

        }

        /// <summary>
        /// Metoda pro Insert zboží do FASK_ZASOBY
        /// </summary>
        /// <param name="zboziRow">řadek co se vloží</param>
        /// <returns></returns>
        public bool InsertZbozi(Zbozi.FASK_ZASOBY_KONZOLARow zboziRow)
        {
            Zbozi ds = new Zbozi();
            Fask.WEBAPI.API_BusinessObjects.FASK_ZASOBY FZ_data = new Fask.WEBAPI.API_BusinessObjects.FASK_ZASOBY();
            bool result = false;
            try
            {
                zboziRow.AcceptChanges();
                ds.FASK_ZASOBY_KONZOLA.ImportRow(zboziRow);
                //ds.FASK_ZASOBY.AddFASK_ZASOBYRow(zboziRow);
                FZ_data = ds.FASK_ZASOBY_KONZOLA.DataSetToBO();


                IRestResponse restResponse;
                string param = "Konzola_InsertZbozi";
                string JSON = "";

                JSON = Classes.JSON_Class.Serialize_JSON(FZ_data);

                if (!Communicate(Terminal_ID, REST_Type.POST, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<bool>(restResponse.Content);

                    //FZ_data = ds.DataSetToBO();
                    //ds = FZ_data.BOToDataSet();

                    return result;
                }

                return false;
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                return false;
            }
        }

        /// <summary>
        /// Metoda pro update pouze FASK_ZBOZI
        /// </summary>
        /// <param name="zboziRow">Radek pro Update</param>
        /// <returns>True-OK, False- chyba</returns>
        public bool UpdateZbozi(Zbozi.FASK_ZASOBY_ALL_KONZOLARow zboziRow)
        {
            Zbozi ds = new Zbozi();
            Fask.WEBAPI.API_BusinessObjects.FASK_ZASOBY_ALL FZ_data = new Fask.WEBAPI.API_BusinessObjects.FASK_ZASOBY_ALL();
            bool result = false;
            try
            {
                //ds.FASK_ZASOBY_ALL.AddFASK_ZASOBY_ALLRow(zboziRow);

                zboziRow.AcceptChanges();
                ds.FASK_ZASOBY_ALL_KONZOLA.ImportRow(zboziRow);
                FZ_data = ds.FASK_ZASOBY_ALL_KONZOLA.DataSetToBO();


                IRestResponse restResponse;
                string param = "Konzola_UpdateZbozi";
                string JSON = "";

                JSON = Classes.JSON_Class.Serialize_JSON(FZ_data);

                if (!Communicate(Terminal_ID, REST_Type.POST, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<bool>(restResponse.Content);

                    //FZ_data = ds.DataSetToBO();
                    //ds = FZ_data.BOToDataSet();

                    return result;
                }

                return false;
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                return false;
            }

        }

        #region Parametry

        /// <summary>
        /// Metoda pro inser do FASK_ZASOBY_PARAMETRY
        /// </summary>
        /// <param name="zboziRow"></param>
        /// <returns></returns>
        public bool InsertZboziParams(Zbozi.FASK_ZASOBY_PARAMETRY_KONZOLARow zboziRow)
        {
            Zbozi ds = new Zbozi();
            Fask.WEBAPI.API_BusinessObjects.FASK_ZASOBY_PARAMETRY FZ_data = new Fask.WEBAPI.API_BusinessObjects.FASK_ZASOBY_PARAMETRY();
            bool result = false;
            try
            {
                //ds.FASK_ZASOBY_PARAMETRY.AddFASK_ZASOBY_PARAMETRYRow(zboziRow);

                zboziRow.AcceptChanges();
                ds.FASK_ZASOBY_PARAMETRY_KONZOLA.ImportRow(zboziRow);
                FZ_data = ds.FASK_ZASOBY_PARAMETRY_KONZOLA.DataSetToBO();


                IRestResponse restResponse;
                string param = "Konzola_InsertZboziParams";
                string JSON = "";

                JSON = Classes.JSON_Class.Serialize_JSON(FZ_data);

                if (!Communicate(Terminal_ID, REST_Type.POST, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<bool>(restResponse.Content);

                    //FZ_data = ds.DataSetToBO();
                    //ds = FZ_data.BOToDataSet();

                    return result;
                }

                return false;
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                return false;
            }
        }


        /// <summary>
        /// Metoda pro Inser/Update Parametru...
        /// </summary>
        /// <param name="zboziRow"></param>
        /// <returns></returns>
        public bool UpdateZboziParams(Zbozi.FASK_ZASOBY_ALL_KONZOLARow zboziRow)
        {
            Zbozi ds = new Zbozi();
            Fask.WEBAPI.API_BusinessObjects.FASK_ZASOBY_ALL FZ_data = new Fask.WEBAPI.API_BusinessObjects.FASK_ZASOBY_ALL();
            bool result = false;
            try
            {
                //ds.FASK_ZASOBY_ALL.AddFASK_ZASOBY_ALLRow(zboziRow);

                zboziRow.AcceptChanges();
                ds.FASK_ZASOBY_ALL_KONZOLA.ImportRow(zboziRow);
                FZ_data = ds.FASK_ZASOBY_ALL_KONZOLA.DataSetToBO();


                IRestResponse restResponse;
                string param = "Konzola_UpdateZboziParams";
                string JSON = "";

                JSON = Classes.JSON_Class.Serialize_JSON(FZ_data);

                if (!Communicate(Terminal_ID, REST_Type.POST, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<bool>(restResponse.Content);

                    //FZ_data = ds.DataSetToBO();
                    //ds = FZ_data.BOToDataSet();

                    return result;
                }

                return false;
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                return false;
            }

        }


        private bool UpdateParametry(Zbozi.FASK_ZASOBY_ALL_KONZOLARow zboziRow)
        {
            Zbozi ds = new Zbozi();
            Fask.WEBAPI.API_BusinessObjects.FASK_ZASOBY_ALL FZ_data = new Fask.WEBAPI.API_BusinessObjects.FASK_ZASOBY_ALL();
            bool result = false;
            try
            {
                //ds.FASK_ZASOBY_ALL.AddFASK_ZASOBY_ALLRow(zboziRow);

                zboziRow.AcceptChanges();
                ds.FASK_ZASOBY_ALL_KONZOLA.ImportRow(zboziRow);
                FZ_data = ds.FASK_ZASOBY_ALL_KONZOLA.DataSetToBO();


                IRestResponse restResponse;
                string param = "Konzola_UpdateParametry";
                string JSON = "";

                JSON = Classes.JSON_Class.Serialize_JSON(FZ_data);

                if (!Communicate(Terminal_ID, REST_Type.POST, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<bool>(restResponse.Content);

                    //FZ_data = ds.DataSetToBO();
                    //ds = FZ_data.BOToDataSet();

                    return result;
                }

                return false;
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                return false;
            }

        }

        private bool GetParametrybyID(string ITEMNMBR)
        {
            bool result = false;
            try
            {
                IRestResponse restResponse;
                string param = "Konzola_GetParametrybyID" + "/" + ITEMNMBR;
                string JSON = "";

                if (!Communicate(Terminal_ID, REST_Type.GET, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<bool>(restResponse.Content);

                    return result;
                }

                return false;
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                return false;
            }

        }


        #endregion

        #region Import Zbozi

        public string ImportZbozi()
        {
            string result = string.Empty;
            try
            {
                IRestResponse restResponse;
                string param = "Konzola_ImportZbozi";
                string JSON = "";

                if (!Communicate(Terminal_ID, REST_Type.GET, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(restResponse.Content);

                    return result;
                }

                return "Error";
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                return "Error";
            }

        }

        private string ExportKatalogZasoby_Procedura()
        {
            string result = string.Empty;
            try
            {
                IRestResponse restResponse;
                string param = "Konzola_ExportKatalogZasoby_Procedura";
                string JSON = "";

                if (!Communicate(Terminal_ID, REST_Type.GET, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(restResponse.Content);

                    return result;
                }

                return "Error";
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                return "Error";
            }

        }

        public List<Tuple<string, string, bool>> GetZasobyTableInfo()
        {
            throw new NotImplementedException();
        }



        #endregion

        #endregion
    }
}
