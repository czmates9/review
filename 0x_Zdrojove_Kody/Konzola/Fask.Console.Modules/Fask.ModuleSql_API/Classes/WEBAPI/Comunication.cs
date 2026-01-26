using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fask.Logging;
using Fask.ModuleSql_API;
using RestSharp;

namespace Fask.ModuleSql_API.Classes
{
   public class Comunication
    {

        public enum REST_Type
        {
            GET,
            POST,
            PUT,
            DELETE,
            unknow
        }

        public static bool Communicate(string tid, REST_Type _Type, out IRestResponse response, string Param, string JSON_Message = "")
        {
            try
            {
                Globals_V1.LoadConfiguration();

                if (_Type == REST_Type.POST || _Type == REST_Type.PUT)
                {
                    if (string.IsNullOrEmpty(JSON_Message))
                        throw new Exception("Obsah JSON je prázdy, ale pro " + _Type.ToString() + " musí byt vyplnení!");
                }

                Fask.RestSharp.API.Communication_4_7 com ;

                    com = new Fask.RestSharp.API.Communication_4_7(
                    Globals_V1.Konfigurace.Nastaveni[0].Adresa, //   "192.168.1.121:8080", // 192.168.1.69/MST_Win_Kom_Server_7_Dasenka 
                    Globals_V1.Konfigurace.Nastaveni[0].Autorizace_DoAPI, //"MDox",
                    Globals_V1.Konfigurace.Nastaveni[0].AliasDB, // "api",
                    Globals_V1.Konfigurace.Nastaveni[0].isHTTPS, // false,
                    Globals_V1.Konfigurace.Nastaveni[0].API_TimeOut //10000
                    , tid
                    , "Konzola"
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
                        ExceptionHandler2.Handle(new Exception("Neznamy typ requestu..."));
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
    }
}
