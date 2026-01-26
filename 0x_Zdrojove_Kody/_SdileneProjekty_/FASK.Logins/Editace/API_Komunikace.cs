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

namespace FASK.Logins.Editace
{
    public class API_Komunikace : CommunicateClass, IKomunikace
    {


        public API_Komunikace(
            string adresa,
            string autorizace_DoAPI,
            string aliasDB,
            bool ishttps,
            int timeout,
            string tid,
            string typeklient
            ) : this()
        {
            Adresa = adresa;
            Autorizace_DoAPI = autorizace_DoAPI;
            AliasDB = aliasDB;
            isHTTPS = ishttps;
            API_TimeOut = timeout;
            TID = tid;
            TypKlient = typeklient;
        }

        public API_Komunikace()
        {
         
        }

        public bool Delete_Auth(int DEX_ROW_ID)
        {
            bool vysledek = false;
            try
            {
                IRestResponse restResponse;
                string param = "Delete_Auth" + "/" + DEX_ROW_ID;
                string JSON = "";

                //JSON = JSON_Class.Serialize_JSON(USERID);

                if (!base.Communicate(REST_Type.DELETE, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    vysledek = Newtonsoft.Json.JsonConvert.DeserializeObject<bool>(restResponse.Content);

                    return vysledek;
                }

                return false;
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                return false;
            }
        }

        public bool Delete_Login(string id)
        {
            bool vysledek = false;
            try
            {
                IRestResponse restResponse;
                string param = "Delete_Login" + "/" + id;
                string JSON = "";

                //JSON = JSON_Class.Serialize_JSON(USERID);

                if (!base.Communicate(REST_Type.DELETE, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    vysledek = Newtonsoft.Json.JsonConvert.DeserializeObject<bool>(restResponse.Content);

                    return vysledek;
                }

                return false;
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                return false;
            }
        }

        public Pristupy GetFASK_AGENDA_Filtrovana(Filtry_Agenda_A filtr)
        {
            Pristupy ds = null;
            try
            {
                IRestResponse restResponse;
                string param = "GetFASK_AGENDA_Filtrovana";
                string JSON = "";

                JSON = JSON_Class.Serialize_JSON(filtr);

                if (!base.Communicate(REST_Type.POST, out restResponse, param, JSON))
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

        public Pristupy GetFiltrovanyLogins(Filtry_Login_A Filtr)
        {
            Pristupy ds = null;
            try
            {
                IRestResponse restResponse;
                string param = "GetFiltrovanyLogins";
                string JSON = "";

                JSON = JSON_Class.Serialize_JSON(Filtr);

                if (!base.Communicate(REST_Type.POST, out restResponse, param, JSON))
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

        public Pristupy GetLogins_Auth(Filtry_Auth_A filtr)
        {
            Pristupy ds = null;
            try
            {
                IRestResponse restResponse;
                string param = "GetLogins_Auth";
                string JSON = "";

                JSON = JSON_Class.Serialize_JSON(filtr);

                if (!base.Communicate(REST_Type.POST, out restResponse, param, JSON))
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

        public bool Insert_Auth(string USERID, string AGENDAID)
        {
            bool vysledek = false;
            try
            {
                #region skladani URL
                HttpValueCollection URI_param_tmp = new HttpValueCollection();
                string tmp = null;

                if (!string.IsNullOrEmpty(USERID))
                    URI_param_tmp.Add("USERID", USERID);

                if (!string.IsNullOrEmpty(AGENDAID))
                    URI_param_tmp.Add("AGENDAID", AGENDAID);

                tmp += "?";
                tmp += URI_param_tmp.ToString();
                #endregion

                IRestResponse restResponse;
                string param = "Insert_Auth" + tmp;
                string JSON = "";

                JSON = JSON_Class.Serialize_JSON(USERID);

                if (!base.Communicate(REST_Type.POST, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    vysledek = Newtonsoft.Json.JsonConvert.DeserializeObject<bool>(restResponse.Content);

                    return vysledek;
                }

                return false;
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                return false;
            }
        }

        public bool Insert_Login(string USERID, string firstname, string surname, string psswd)
        {
            bool vysledek = false;
            try
            {
                IRestResponse restResponse;

                #region skladani URL
                HttpValueCollection URI_param_tmp = new HttpValueCollection();
                string tmp = null;

                if (!string.IsNullOrEmpty(USERID))
                    URI_param_tmp.Add("USERID", USERID);

                if (!string.IsNullOrEmpty(firstname))
                    URI_param_tmp.Add("firstname", firstname);

                if (!string.IsNullOrEmpty(surname))
                    URI_param_tmp.Add("surname", surname);

                if (!string.IsNullOrEmpty(psswd))
                    URI_param_tmp.Add("psswd", psswd);

                tmp += "?";
                tmp += URI_param_tmp.ToString();
                #endregion

                string param = "Insert_Login" + tmp;
                string JSON = "";

                JSON = JSON_Class.Serialize_JSON(USERID);

                if (!base.Communicate(REST_Type.POST, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    vysledek = Newtonsoft.Json.JsonConvert.DeserializeObject<bool>(restResponse.Content);

                    return vysledek;
                }

                return false;
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                return false;
            }
        }

        public bool isExist_Auth(string USERID, string AGENDAID)
        {
            bool vysledek = false;
            try
            {
                IRestResponse restResponse;
                #region skladani URL
                HttpValueCollection URI_param_tmp = new HttpValueCollection();
                string tmp = null;

                if (!string.IsNullOrEmpty(USERID))
                    URI_param_tmp.Add("USERID", USERID);

                if (!string.IsNullOrEmpty(AGENDAID))
                    URI_param_tmp.Add("AGENDAID", AGENDAID);

                tmp += "?";
                tmp += URI_param_tmp.ToString();
                #endregion
                string param = "isExist_Auth" + tmp;
                string JSON = "";

                //JSON = JSON_Class.Serialize_JSON(USERID);

                if (!base.Communicate(REST_Type.GET, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    vysledek = Newtonsoft.Json.JsonConvert.DeserializeObject<bool>(restResponse.Content);

                    return vysledek;
                }

                return false;
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                return false;
            }
        }

        public bool Update_Login_Row(Pristupy.FASK_LoginsRow loginsrow)
        {
            bool vysledek = false;
            try
            {
                #region plneny objektu pro JSON
                Fask.WEBAPI.API_BusinessObjects.FASK_Login login_data = new Fask.WEBAPI.API_BusinessObjects.FASK_Login();
                login_data.USERID = string.IsNullOrEmpty(loginsrow.USERID) ? throw new Exception("USERID is null!!") : loginsrow.USERID;
                login_data.firstname = string.IsNullOrEmpty(loginsrow.firstname) ? string.Empty : loginsrow.firstname;
                login_data.surname = string.IsNullOrEmpty(loginsrow.surname) ? string.Empty : loginsrow.surname;
                login_data.psswd = string.IsNullOrEmpty(loginsrow.psswd) ? string.Empty : loginsrow.psswd;

                if (loginsrow.IsCREATEDNull())
                    login_data.CREATED = null;
                else
                    login_data.CREATED = loginsrow.CREATED;

                if (loginsrow.IsVALIDFROMNull())
                    login_data.VALIDFROM = null;
                else
                    login_data.VALIDFROM = loginsrow.VALIDFROM;

                if (loginsrow.IsVALIDTONull())
                    login_data.VALIDTO = null;
                else
                    login_data.VALIDTO = loginsrow.VALIDTO;

                if (loginsrow.IsRFIDNull())
                    login_data.RFID = null;
                else
                    login_data.RFID = loginsrow.RFID;

                //login_data.RFID = string.IsNullOrEmpty(loginsrow.RFID) ? string.Empty : loginsrow.RFID;

                #endregion

                IRestResponse restResponse;
                string param = "Update_Login_Row";
                string JSON = "";

                JSON = JSON_Class.Serialize_JSON(login_data);

                if (!base.Communicate(REST_Type.POST, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    vysledek = Newtonsoft.Json.JsonConvert.DeserializeObject<bool>(restResponse.Content);

                    return vysledek;
                }

                return false;
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                return false;
            }
        }
    }
}
