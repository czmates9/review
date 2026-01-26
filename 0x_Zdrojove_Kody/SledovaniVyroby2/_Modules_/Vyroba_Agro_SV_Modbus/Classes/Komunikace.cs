using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using RestSharp;
using Fask.Logging;

using Fask.WEBAPI;
using Fask.WEBAPI.API_BusinessObjects;

namespace FASK.SledovaniVyroby.Module.Vyroba_Agro_SV_Modbus.Classes
{
    public class Komunikace
    {
        #region Logika_Voziky

        public static string Leonardo_TISK_API(FASK_Events_row o)
        {

            IRestResponse restResponse;
            string param = "agrocs";
            string JSON = "";

            ExceptionHandler2.Handle("TISK START:" + o.NMBRPAL, "Log_LV", "txt");

            JSON = Classes.WEBAPI.JSON_Class.Serialize_JSON(o);

            if (!Classes.WEBAPI.Comunication.Communicate(Classes.WEBAPI.Comunication.REST_Type.POST, out restResponse, param, JSON))
            {
                ExceptionHandler2.Handle(LogLevel.Error, "Komunikace s IS AGRO se nezdarila--" + restResponse.Content);
                return "Komunikace s IS AGRO se nezdarila";
            }

            if (restResponse.StatusCode == HttpStatusCode.OK)
            {
                ExceptionHandler2.Handle("TISK odeslana data OK", "Log_LV", "txt");
                return "OK";
            }
            else if (restResponse.StatusCode == 0)
            {
                //chyba pri odeslanych datech na tisk, kdy nejede sluzba od Leonarda
                ExceptionHandler2.Handle(string.Format("TISK - nefunkcni sluzba Leonardo! "), "Log_LV", "txt");

                string popisek = string.Format("TISK - nefunkcni sluzba!");
                return popisek;
            }
            else
            {
                var content = restResponse.Content;
                string txt = string.Format("TISK - StatusCode: {0}", restResponse.StatusCode.ToString());
                txt += string.Format("TISK - Content: {0}", restResponse.Content);
                ExceptionHandler2.Handle(txt, "Log_LV", "txt");
                return txt;
            }
        }

        #endregion

    }
}
