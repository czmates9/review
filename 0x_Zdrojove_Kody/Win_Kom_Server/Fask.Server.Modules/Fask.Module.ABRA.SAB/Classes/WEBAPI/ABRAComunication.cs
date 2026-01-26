using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Module.ABRA.SAB.Classes.WEBAPI
{
    public class ABRAComunication
    {
        public enum REST_Type
        {
            GET,
            POST,
            PUT,
            DELETE,
            unknow
        }

        internal static bool Communicate(REST_Type _Type, out IRestResponse response, string Param, string JSON_Message = "")
        {
            try
            {
                if (_Type == REST_Type.POST || _Type == REST_Type.PUT)
                {
                    if (string.IsNullOrEmpty(JSON_Message))
                        throw new Exception("Obsah JSON je prázdy, ale pro " + _Type.ToString() + " musí byt vyplnení!");
                }

                Globals_V1.LoadConfiguration();

                Fask.RestSharp.API.Communication_4_7 com = new RestSharp.API.Communication_4_7(
                    Globals_V1.Konfigurace.WEBAPI[0].Adresa,
                    Globals_V1.Konfigurace.WEBAPI[0].Autorizace_DoAPI,
                    Globals_V1.Konfigurace.WEBAPI[0].AliasDB,
                    Globals_V1.Konfigurace.WEBAPI[0].isHTTPS,
                    Globals_V1.Konfigurace.WEBAPI[0].API_TimeOut,
                    "0",
                    "server"
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
                        Logging.ExceptionHandler2.Handle(new Exception("Neznamy typ requestu..."));
                        break;
                }

                return true;
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }
    }
}
