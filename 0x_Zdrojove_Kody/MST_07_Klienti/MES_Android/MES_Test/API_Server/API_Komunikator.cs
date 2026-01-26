using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MES_Android.API_Server
{
    class API_Komunikator
    {

        public enum REST_Type
        {
            GET,
            POST,
            PUT,
            DELETE,
            unknow
        }

        public static bool Communicate(REST_Type _Type, out IRestResponse response, string Param, string JSON_Message = "")
        {
            try
            {


                if (_Type == REST_Type.POST || _Type == REST_Type.PUT)
                {
                    if (string.IsNullOrEmpty(JSON_Message))
                        throw new Exception("Obsah JSON je prázdy, ale pro " + _Type.ToString() + " musí byt vyplnení!");
                }

                response = null;

                switch (_Type)
                {
                    case REST_Type.GET:
                        response = Classes.DataInfo_Static.API_GO_Instance.REST_GET(Param);
                        break;
                    case REST_Type.POST:
                        response = Classes.DataInfo_Static.API_GO_Instance.REST_POST(Param, JSON_Message);
                        break;
                    case REST_Type.PUT:
                        response = Classes.DataInfo_Static.API_GO_Instance.REST_PUT(Param, JSON_Message);
                        break;
                    case REST_Type.DELETE:
                        response = Classes.DataInfo_Static.API_GO_Instance.REST_DELETE(Param);
                        break;
                    case REST_Type.unknow:
                    default:
                        Fask.Logging.ExceptionHandler2.Handle(new Exception("Neznamy typ requestu..."));
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