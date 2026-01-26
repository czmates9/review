using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Fask.Interfaces.DataSets;
using RestSharp;
using Fask.Logging;
using System.Net;
using Fask.Extension;
using Fask.RestSharp.API;
using Fask.ModuleSql_API.Classes;
using static Fask.ModuleSql_API.Classes.Comunication;

namespace Fask.ModuleSql_API
{
    public partial class Provider :
        Fask.Interfaces.IT_cast.IIT_cast,
        Fask.Interfaces.IT_cast.IIT_cast_Events_Archivace,
        Fask.Interfaces.IT_cast.IIT_cast_Production_Archivace
    {
        #region IIT_cast

        #region IIT_cast_Events_Archivace
        public int ArchivaceProcedura(DateTime? OD, DateTime? DO)
        {
            int pocetZaznamu = 0;
            Fask.WEBAPI.API_BusinessObjects.BO_Fask_Events_Archivace bo = new Fask.WEBAPI.API_BusinessObjects.BO_Fask_Events_Archivace();
            try
            {
                #region skladani URL
                HttpValueCollection URI_param_tmp = new HttpValueCollection();
                string tmp = null;

                if (OD.HasValue)
                    URI_param_tmp.Add("OD", OD.Value.ToString(System.Globalization.DateTimeFormatInfo.InvariantInfo));

                if (DO.HasValue)
                    URI_param_tmp.Add("DO", DO.Value.ToString(System.Globalization.DateTimeFormatInfo.InvariantInfo));

                tmp += "?";
                tmp += URI_param_tmp.ToString();
                #endregion

                string param = "Konzola_Fask_Events_ArchivaceProcedura" + tmp;
                string JSON = "";
                IRestResponse restResponse;
                //JSON = JSON_Class.Serialize_JSON(filtr);

                if (!Communicate(Terminal_ID, REST_Type.GET, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    bo = Newtonsoft.Json.JsonConvert.DeserializeObject<Fask.WEBAPI.API_BusinessObjects.BO_Fask_Events_Archivace>(restResponse.Content);
                    pocetZaznamu = bo.pocetZaznamu;
                }

                return pocetZaznamu;
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                return 0;
            }
        }

        public int Archivace_Fask_Event_id(int id)
        {
            int pocetZaznamu = 0;
            Fask.WEBAPI.API_BusinessObjects.BO_Fask_Events_Archivace bo = new Fask.WEBAPI.API_BusinessObjects.BO_Fask_Events_Archivace();
            try
            {
                #region skladani URL
                HttpValueCollection URI_param_tmp = new HttpValueCollection();
                string tmp = null;

                    URI_param_tmp.Add("pom_id",id.ToString());

                tmp += "?";
                tmp += URI_param_tmp.ToString();
                #endregion

                // string param = "Konzola_Fask_Events_ArchivaceProcedura" + tmp;

                string param = "Konzola_Fask_Events_Archivace_id" + tmp;

                string JSON = "";
                IRestResponse restResponse;
                //JSON = JSON_Class.Serialize_JSON(filtr);

                if (!Communicate(Terminal_ID, REST_Type.GET, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    bo = Newtonsoft.Json.JsonConvert.DeserializeObject<Fask.WEBAPI.API_BusinessObjects.BO_Fask_Events_Archivace>(restResponse.Content);
                    pocetZaznamu = bo.pocetZaznamu;
                }

                return pocetZaznamu;
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                return 0;
            }
        }



        #endregion

        #region IIT_cast_Production_Archivace
        public int Production_ArchivaceProcedura(DateTime? OD, DateTime? DO)
        {
            int pocetZaznamu = 0;
            Fask.WEBAPI.API_BusinessObjects.BO_Production_Archivace bo = new Fask.WEBAPI.API_BusinessObjects.BO_Production_Archivace();
            try
            {
                #region skladani URL
                HttpValueCollection URI_param_tmp = new HttpValueCollection();
                string tmp = null;

                if (OD.HasValue)
                    URI_param_tmp.Add("OD", OD.Value.ToString(System.Globalization.DateTimeFormatInfo.InvariantInfo));

                if (DO.HasValue)
                    URI_param_tmp.Add("DO", DO.Value.ToString(System.Globalization.DateTimeFormatInfo.InvariantInfo));

                tmp += "?";
                tmp += URI_param_tmp.ToString();
                #endregion

                string param = "Konzola_Fask_Production_ArchivaceProcedura" + tmp;
                string JSON = "";
                IRestResponse restResponse;
                //JSON = JSON_Class.Serialize_JSON(filtr);

                if (!Communicate(Terminal_ID, REST_Type.GET, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    bo = Newtonsoft.Json.JsonConvert.DeserializeObject<Fask.WEBAPI.API_BusinessObjects.BO_Production_Archivace>(restResponse.Content);
                    pocetZaznamu = bo.pocetZaznamu;
                }

                return pocetZaznamu;
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                return 0;
            }
        }

        public int Production_ArchivaceProcedura_guid(Guid guid)
        {
            int pocetZaznamu = 0;
            Fask.WEBAPI.API_BusinessObjects.BO_Production_Archivace bo = new Fask.WEBAPI.API_BusinessObjects.BO_Production_Archivace();
            try
            {
                #region skladani URL
                HttpValueCollection URI_param_tmp = new HttpValueCollection();
                string tmp = null;

                URI_param_tmp.Add("pom_guid", guid.ToString());

                tmp += "?";
                tmp += URI_param_tmp.ToString();
                #endregion

                string param = "Konzola_Fask_Production_ArchivaceProcedura_guid" + tmp;
                string JSON = "";
                IRestResponse restResponse;
                //JSON = JSON_Class.Serialize_JSON(filtr);

                if (!Communicate(Terminal_ID, REST_Type.GET, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    bo = Newtonsoft.Json.JsonConvert.DeserializeObject<Fask.WEBAPI.API_BusinessObjects.BO_Production_Archivace>(restResponse.Content);
                    pocetZaznamu = bo.pocetZaznamu;
                }

                return pocetZaznamu;
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                return 0;
            }
        }
        #endregion

        #endregion
    }
}
