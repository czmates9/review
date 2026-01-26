using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fask.Logging;
using FASK.SledovaniVyroby.Main.Configuration;
using RestSharp;

namespace Vyroba.Classes.WEBAPI
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

        internal static bool Communicate(REST_Type _Type, out IRestResponse response, string Param, string JSON_Message = "",string TID = "", string volajici = "")
        {
            try
            {
                if (_Type == REST_Type.POST || _Type == REST_Type.PUT)
                {
                    if (string.IsNullOrEmpty(JSON_Message))
                        throw new Exception("Obsah JSON je prázdy, ale pro " + _Type.ToString() + " musí byt vyplnení!");
                }

                //Fask.RestSharp.API.Communication com = new Fask.RestSharp.API.Communication(
                //   AgroSledovaniVozikuConfig.config.WEBAPI[0].Adresa, //   "192.168.1.121:8080", // 192.168.1.69/MST_Win_Kom_Server_7_Dasenka 
                //   AgroSledovaniVozikuConfig.config.WEBAPI[0].Autorizace_DoAPI, //"MDox",
                //   AgroSledovaniVozikuConfig.config.WEBAPI[0].AliasDB, // "api",
                //   AgroSledovaniVozikuConfig.config.WEBAPI[0].isHTTPS, // false,
                //   AgroSledovaniVozikuConfig.config.WEBAPI[0].API_TimeOut //10000
                //   );

                Fask.RestSharp.API.Communication_4_7 com ;


                try
                {
                    com = new Fask.RestSharp.API.Communication_4_7(
                            Config.config.WEBAPI[0].Adresa, //   "192.168.1.121:8080", // 192.168.1.69/MST_Win_Kom_Server_7_Dasenka 
                            Config.config.WEBAPI[0].Autorizace_DoAPI, //"MDox",
                            Config.config.WEBAPI[0].AliasDB, // "api",
                            Config.config.WEBAPI[0].isHTTPS, // false,
                            Config.config.WEBAPI[0].API_TimeOut, //10000
                           TID,
                            volajici
                            );
                }
                catch (Exception ex)
                {

                    ExceptionHandler2.Handle("Nezname parametry pro komunikaci se serverem, vypln spravne WEBAPI v souboru vyrobaconfig.xml", "Log_Vyroba", "txt");
                    throw ex;
                }


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
                        //Logging.ExceptionHandler2.Handle(new Exception("Neznamy typ requestu..."));
                        //Log.Write(new Exception("Neznamy typ requestu..."));
                        ExceptionHandler2.Handle("Communication -- Neznamy typ requestu...", "Log_Vyroba", "txt");
                        break;
                }

                return true;
            }
            catch (System.Exception ex)
            {
                //Fask.Logging.ExceptionHandler2.Handle(ex);
                //Log.Write(ex);
                ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }
    }
}
