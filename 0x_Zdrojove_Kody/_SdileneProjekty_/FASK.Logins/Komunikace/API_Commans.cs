using Fask.Logging;
using Fask.RestSharp.API;
using FASK.Logins.DataSets;
using FASK.Logins.WEBAPI;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FASK.Logins.Komunikace
{
    public class API_Commans : CommunicateClass, ICommans
    {
        //private string _FASK_TID = null;
        //public string FASK_TID
        //{
        //    get { return _FASK_TID; }
        //    set { _FASK_TID = value; }
        //}

        //private string _FASK_TYPE_KLIENT = null;
        //public string FASK_TYPE_KLIENT
        //{
        //    get { return _FASK_TYPE_KLIENT; }
        //    set { _FASK_TYPE_KLIENT = value; }
        //}



        public API_Commans()
        {

        }

        public API_Commans(
            string adresa, 
            string autorizace_DoAPI,
            string aliasDB,
            bool ishttps,
            int timeout,
            string fask_TID,
            string fask_TYPE_KLIENT
            ) : this()  
        {
            Adresa = adresa;
            Autorizace_DoAPI = autorizace_DoAPI;
            AliasDB = aliasDB;
            isHTTPS = ishttps;
            API_TimeOut = timeout;
            FASK_TID = fask_TID;
            FASK_TYPE_KLIENT = fask_TYPE_KLIENT;
        }

        public Pristupy GetAgednaID(string AgendaID)
        {
            Pristupy ds = null;
            try
            {
                IRestResponse restResponse;
                string param = "GetAgednaID" + "/" + AgendaID;
                string JSON = "";

                //JSON = JSON_Class.Serialize_JSON(USERID);

                if (!base.Communicate(REST_Type.GET, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    ds = Newtonsoft.Json.JsonConvert.DeserializeObject<Pristupy>(restResponse.Content);

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

        public Pristupy GetLikeAgednaID(string AgendaID)
        {
            Pristupy ds = null;
            try
            {
                IRestResponse restResponse;
                string param = "GetLikeAgednaID" + "/" + AgendaID;
                string JSON = "";

                //JSON = JSON_Class.Serialize_JSON(USERID);

                if (!base.Communicate(REST_Type.GET, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    ds = Newtonsoft.Json.JsonConvert.DeserializeObject<Pristupy>(restResponse.Content);

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

        public Pristupy.FASK_LoginsDataTable GetLogins()
        {

            Pristupy.FASK_LoginsDataTable dt = null;
            try
            {
                IRestResponse restResponse;
                string param = "GetLogins";
                string JSON = "";

                //JSON = JSON_Class.Serialize_JSON();

                if (!base.Communicate(REST_Type.GET, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    dt = Newtonsoft.Json.JsonConvert.DeserializeObject<Pristupy.FASK_LoginsDataTable>(restResponse.Content);

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

        public Pristupy.FASK_LoginsDataTable GetLoginsByID(string AgendaID)
        {
            Pristupy.FASK_LoginsDataTable ds = null;
            try
            {
                IRestResponse restResponse;
                string param = "GetLoginsByID" + "/" + AgendaID;
                string JSON = "";

                //JSON = JSON_Class.Serialize_JSON(USERID);

                if (!base.Communicate(REST_Type.GET, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    ds = Newtonsoft.Json.JsonConvert.DeserializeObject<Pristupy.FASK_LoginsDataTable>(restResponse.Content);

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

        public Pristupy.FASK_LoginsDataTable GetOverLogins(string USERID, string PWD, string AGENDA)
        {
            Pristupy.FASK_LoginsDataTable ds = null;
            try
            {
                IRestResponse restResponse;
                #region skladani URL
                HttpValueCollection URI_param_tmp = new HttpValueCollection();
                string tmp = null;
                
                if(!string.IsNullOrEmpty(USERID))
                URI_param_tmp.Add("USERID", USERID);

                if (!string.IsNullOrEmpty(PWD))
                    URI_param_tmp.Add("PWD", PWD);

                if (!string.IsNullOrEmpty(AGENDA))
                    URI_param_tmp.Add("AGENDA", AGENDA);

                tmp += "?";
                tmp += URI_param_tmp.ToString();
                #endregion

                string param = "GetOverLogins" + tmp;
                string JSON = "";

                //JSON = JSON_Class.Serialize_JSON(USERID);

                if (!base.Communicate(REST_Type.GET, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    ds = Newtonsoft.Json.JsonConvert.DeserializeObject<Pristupy.FASK_LoginsDataTable>(restResponse.Content);

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

        public Pristupy GetViewData(string USERID)
        {
            Pristupy ds = null;
            try
            {
                IRestResponse restResponse;
                string param = "GetViewData" + "/" + USERID ;
                string JSON = "";

                //JSON = JSON_Class.Serialize_JSON(USERID);

                if (!base.Communicate(REST_Type.GET, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    ds = Newtonsoft.Json.JsonConvert.DeserializeObject<Pristupy>(restResponse.Content);

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
    }
}
