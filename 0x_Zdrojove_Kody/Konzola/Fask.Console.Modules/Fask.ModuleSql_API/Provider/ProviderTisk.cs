using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Fask.Interfaces.Tisky;
using Fask.Logging;
using Fask.ModuleSql_API.Classes;
using MST_Print_Server_ZPL_Printing;
using RestSharp;
using static Fask.ModuleSql_API.Classes.Comunication;

namespace Fask.ModuleSql_API
{
    public partial class Provider :
        ITisky2,
        ITisky2_EtiketaTisk
    {
        public bool EtiketaTisk(int terminalID, string templateName, TiskParams printerParams, Dictionary<string, string> data, int pocetVytisku)
        {
            string result = string.Empty;
            try
            {
                Fask.WEBAPI.API_BusinessObjects.Tisk_Etiketa _Etiketa = new WEBAPI.API_BusinessObjects.Tisk_Etiketa()
                {
                    TerminalID = terminalID,
                    TemplateName = templateName,
                    PrinterParams = printerParams,
                    Data = data,
                    PocetVytisku = pocetVytisku
                };

                string param = "Tisk/Etiketa";
                string JSON = "";
                IRestResponse restResponse;




                JSON = JSON_Class.Serialize_JSON(_Etiketa);

                #region logovani JSON request
                Guid G = Guid.NewGuid(); // Parovaci GUID ked je zapnute logovani...
                if (Globals_V1.Konfigurace.Nastaveni[0].LogovatRequestyResponsy)
                {
                    SaveToFile.Save(JSON_Logs.URL, "EtiketaTisk()", new JsonFormatter(JSON).Format(), G, JSON_Logs.txt);
                }
                #endregion
                if (!Communicate(Terminal_ID, REST_Type.POST, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                #region logovani JSON response
                if (Globals_V1.Konfigurace.Nastaveni[0].LogovatRequestyResponsy)
                {
                    SaveToFile.Save(JSON_Logs.URL, "EtiketaTisk()", new JsonFormatter(restResponse.Content).Format(), G, JSON_Logs.txt);
                }
                #endregion

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(restResponse.Content);

                    if (result == "OK")
                        return true;
                    else
                        return false;
                }
                else if (restResponse.StatusCode == HttpStatusCode.BadRequest)
                {
                    Exception exx = Newtonsoft.Json.JsonConvert.DeserializeObject<Exception>(restResponse.Content);
                    throw exx;
                }
                else if (restResponse.StatusCode == HttpStatusCode.InternalServerError)
                {
                    throw new Exception("Tisk se nepovedl");
                }
                else
                    throw new Exception(string.Format("Neznámy StatusCode:{0}", restResponse.StatusCode)); 

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }
    }
}
