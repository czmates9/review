using Fask.Logging;
using FASK.Logins.Komunikace;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FASK.Logins.WEBAPI
{
    public abstract class CommunicateClass
    {
        #region Komunikace s API

        #region Parameters

        public string Autorizace_DoAPI = @"";
        public string Adresa = @"";
        public string AliasDB = @"";
        public bool isHTTPS = false;
        public int API_TimeOut = 5000;
        public Fask.RestSharp.API.Communication_4_7 com;

        public string TID = @"";
        public string TypKlient = @"";

        private string _FASK_TID = null;
        public string FASK_TID
        {
            get { return _FASK_TID; }
            set { _FASK_TID = value; }
        }

        private string _FASK_TYPE_KLIENT = null;
        public string FASK_TYPE_KLIENT
        {
            get { return _FASK_TYPE_KLIENT; }
            set { _FASK_TYPE_KLIENT = value; }
        }

        #endregion

        public enum REST_Type
        {
            GET,
            POST,
            PUT,
            DELETE,
            unknow
        }

        public CommunicateClass()
        {
                    
        }

        public CommunicateClass(string adresa,
            string autorizace_DoAPI,
            string aliasDB,
            bool ishttps,
            int timeout,
            string fask_TID,
            string fask_TYPE_KLIENT
            ) : this()
        {
            com = new Fask.RestSharp.API.Communication_4_7(
                    adresa,
                    autorizace_DoAPI,
                    aliasDB,
                    ishttps,
                    timeout,
                    fask_TID,
                    fask_TYPE_KLIENT
                    );

            Adresa = adresa;
            Autorizace_DoAPI = autorizace_DoAPI;
            AliasDB = aliasDB;
            isHTTPS = ishttps;
            API_TimeOut = timeout;
            FASK_TID = fask_TID;
            FASK_TYPE_KLIENT = fask_TYPE_KLIENT;
        }

        //public CommunicateClass(
        //  string adresa,
        //  string autorizace_DoAPI,
        //  string aliasDB,
        //  bool ishttps,
        //  int timeout,
        //  string tid,
        //  string typeklient
        //  ) : this()
        //{
        //    Adresa = adresa;
        //    Autorizace_DoAPI = autorizace_DoAPI;
        //    AliasDB = aliasDB;
        //    isHTTPS = ishttps;
        //    API_TimeOut = timeout;
        //    TID = tid;
        //    TypKlient = typeklient;
        //}

        public bool Communicate(REST_Type _Type, out IRestResponse response, string Param, string JSON_Message = "")
        {
            try
            {
                if (_Type == REST_Type.POST || _Type == REST_Type.PUT)
                {
                    if (string.IsNullOrEmpty(JSON_Message))
                        throw new Exception("Obsah JSON je prázdy, ale pro " + _Type.ToString() + " musí byt vyplnení!");
                }

                //Fask.RestSharp.API.Communication com;


                com = new Fask.RestSharp.API.Communication_4_7(
                  Adresa,
                  Autorizace_DoAPI,
                  AliasDB,
                  isHTTPS,
                  API_TimeOut,
                  FASK_TID,
                  FASK_TYPE_KLIENT
                  );


                response = null;

                switch (_Type)
                {
                    case REST_Type.GET:
                        response = com.REST_GET(Param);
                        break;
                    case REST_Type.POST:
                        response = com.REST_POST(Param, JSON_Message);
                        break;
                    case REST_Type.PUT:
                        response = com.REST_PUT(Param, JSON_Message);
                        break;
                    case REST_Type.DELETE:
                        response = com.REST_DELETE(Param);
                        break;
                    case REST_Type.unknow:
                    default:
                        ExceptionHandler2.Handle(LogLevel.Error, "Communication -- Neznamy typ requestu...");
                        break;
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }
        #endregion


    }
    public class CommunicateClass_podtrida : Editace.API_Komunikace //*CommunicateClass*/
    {
       

        public CommunicateClass_podtrida(string adresa,
            string autorizace_DoAPI,
            string aliasDB,
            bool ishttps,
            int timeout,
            string fask_TID,
            string fask_TYPE_KLIENT
            ) 
        {
            base.Adresa = adresa;
            base.Autorizace_DoAPI = autorizace_DoAPI;
            base.AliasDB = aliasDB;
            base.isHTTPS = ishttps;
            base.API_TimeOut =  timeout;
            base.TID = fask_TID;
            base.TypKlient = fask_TYPE_KLIENT;

            // string autorizace_DoAPI,
            // string aliasDB,
            // bool ishttps,
            // int timeout,
            // string fask_TID,
            // string fask_TYPE_KLIENT
            // )



        }

       
    }
}
