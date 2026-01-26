using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Fask.MST_W_Server.API_BusinessObjects;

namespace Fask.MST_W_Server.Controllers
{
    public class KonfigTerminalController : ApiController
    {
        public const string terminalPrefix = "T_";
        public const string terminalSuffix = "_TEXT";
        public const string UserPrefix = "U_";
        public const string priponaJSON = ".json";

        // GET: api/KonfigTerminal
        [HttpGet]
        [Route("api/KonfigTerminal")]
        public IEnumerable<Konfig_Terminal_List> GetKonfig()
        {
            List<Konfig_Terminal_List> konfig_Terminals = new List<Konfig_Terminal_List>();

            //V tomto bode dojde k vraceni seznamu všech konfigurací na serveru 
           // string dstFile = Path.Combine(Fask.MyPath.Path.KonfiguraceTerminalDirectory, "*.json");

            string[] vs = Directory.GetFiles(Fask.MyPath.Path.KonfiguraceTerminalDirectory, "*" + priponaJSON);

            foreach (string item in vs)
            {
                DateTime modification = File.GetLastWriteTime(item).ToUniversalTime();

                string TID = null;
                string USERID = null;

                string FileName = Path.GetFileNameWithoutExtension(item);

                if (FileName.StartsWith(terminalPrefix))
                {
                    TID = FileName.Substring(2);
                }
                else if (FileName.StartsWith(UserPrefix))
                {
                    USERID = FileName.Substring(2);
                }


                    konfig_Terminals.Add(new Konfig_Terminal_List() { 
                    DateLastChange = modification,
                    PathToFile_Relative = item,
                    ID_User = USERID,
                    ID_Terminal = TID
                });                
            }


            //for (int i = 0; i < 10; i++)
            //{
            //    string dstFile = Path.Combine(Fask.MyPath.Path.KonfiguraceTerminalDirectory, terminalPrefix + i.ToString() + priponaJSON);

            //    MES_Android.Konfigurace konfigurace = new MES_Android.Konfigurace();

            //    var JSON = Fask.MST_W_Server.API.JSON_Class.Serialize_JSON(konfigurace);

            //    if (!Directory.Exists(Fask.MyPath.Path.KonfiguraceTerminalDirectory))
            //        Directory.CreateDirectory(Fask.MyPath.Path.KonfiguraceTerminalDirectory);

            //    File.WriteAllText(dstFile, JSON);

            //}





            return konfig_Terminals;
        }

        // GET: api/KonfigTerminal
        [HttpGet]
        [Route("api/KonfigTerminal/TerminalID/{TerminalID}")]
        public HttpResponseMessage GetKonfig(int TerminalID)
        {
            //V tomto bode dojde k vraceni konkretní konfigurace 
           
            try
            {

                string dstFile = Path.Combine(Fask.MyPath.Path.KonfiguraceTerminalDirectory, terminalPrefix + TerminalID.ToString() + priponaJSON);
                return GetKonfigurace(dstFile);
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }


        // GET: api/KonfigTerminal
        [HttpGet]
        [Route("api/KonfigTerminalTexty/TerminalID/{TerminalID}")]
        public HttpResponseMessage GetKonfigTexty(int TerminalID)
        {
            //V tomto bode dojde k vraceni konkretní konfigurace 

            try
            {

                string dstFile = Path.Combine(Fask.MyPath.Path.KonfiguraceTerminalDirectory, terminalPrefix + TerminalID.ToString() + terminalSuffix + priponaJSON);
                return GetKonfiguraceTexty(dstFile);
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        // GET: api/KonfigTerminal
        [HttpGet]
        [Route("api/KonfigTerminal/UserID/{UserID}")]
        public HttpResponseMessage GetKonfig(string UserID)
        {
            //V tomto bode dojde k vraceni konkretní konfigurace 
            try
            {

                string dstFile = Path.Combine(Fask.MyPath.Path.KonfiguraceTerminalDirectory, UserPrefix + UserID + priponaJSON);
                return GetKonfigurace(dstFile);

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        // DELETE: api/KonfigTerminal
        [HttpDelete]
        [Route("api/KonfigTerminal/DeleteFileTerminalID/{TerminalID}")]
        public HttpResponseMessage DeleteFileTerminalID(int TerminalID)
        {
            //V tomto bode dojde k smazani souboru konfigurace
            try
            {

                //cesta k souboru konfigurace:
                string dstFile = Path.Combine(Fask.MyPath.Path.KonfiguraceTerminalDirectory, terminalPrefix + TerminalID.ToString() + priponaJSON);


                //overeni cesty k souboru
                if(dstFile != string.Empty)
                {
                    //logika mazani souboru konfigurace:
                    //TODO MaR dodelat metodu!! 15.7. 2022
                    if(File.Exists(dstFile))
                    {
                        File.Delete(dstFile);
                    }

                    if (!File.Exists(dstFile))
                    {
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.InternalServerError);
                    } 
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
               
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        #region Metody

        private MES_Android.Konfigurace CreateDefaultKonfig(string Cesta)
        {
            try
            {

                MES_Android.Konfigurace konfigurace = new MES_Android.Konfigurace();

                if (!Directory.Exists(Fask.MyPath.Path.KonfiguraceTerminalDirectory))
                    Directory.CreateDirectory(Fask.MyPath.Path.KonfiguraceTerminalDirectory);

                var JSON = Fask.MST_W_Server.API.JSON_Class.Serialize_JSON(konfigurace);

                File.WriteAllText(Cesta, JSON);

                return konfigurace;
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return null;
            }
        }

        private object CreateDefaultKonfigTexty(string Cesta)
        {
            try
            {

                // MES_Android.KonfiguraceTexty konfiguraceText = new MES_Android.KonfiguraceTexty();
                //object konfiguraceText = new json

                object konfiguraceText = new object();

                if (!Directory.Exists(Fask.MyPath.Path.KonfiguraceTerminalDirectory))
                    Directory.CreateDirectory(Fask.MyPath.Path.KonfiguraceTerminalDirectory);

                var JSON = Fask.MST_W_Server.API.JSON_Class.Serialize_JSON(konfiguraceText);

                File.WriteAllText(Cesta, JSON);

                return konfiguraceText;
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return null;
            }
        }

        private HttpResponseMessage GetKonfigurace(string Cesta)
        {
            try
            {

                if (Directory.Exists(Fask.MyPath.Path.KonfiguraceTerminalDirectory))
                {
                    if (!File.Exists(Cesta))
                        return Request.CreateResponse<MES_Android.Konfigurace>(HttpStatusCode.Created, CreateDefaultKonfig(Cesta));
                    else
                    {

                        string JSON = File.ReadAllText(Cesta);

                        MES_Android.Konfigurace konf = Newtonsoft.Json.JsonConvert.DeserializeObject<MES_Android.Konfigurace>(JSON);

                        if (konf != null)
                            return Request.CreateResponse<MES_Android.Konfigurace>(HttpStatusCode.OK, konf);
                        else
                            return Request.CreateResponse(HttpStatusCode.NotFound);

                    }
                }
                else
                {
                    return Request.CreateResponse<MES_Android.Konfigurace>(HttpStatusCode.Created, CreateDefaultKonfig(Cesta));
                }
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }


        private HttpResponseMessage GetKonfiguraceTexty(string Cesta)
        {
            try
            {

                if (Directory.Exists(Fask.MyPath.Path.KonfiguraceTerminalDirectory))
                {
                    if (!File.Exists(Cesta))
                        return Request.CreateResponse<object>(HttpStatusCode.Created, CreateDefaultKonfigTexty(Cesta));
                    else
                    {

                        string JSON = File.ReadAllText(Cesta);

                        object konf = Newtonsoft.Json.JsonConvert.DeserializeObject<object>(JSON);

                        if (konf != null)
                            return Request.CreateResponse<object>(HttpStatusCode.OK, konf);
                        else
                            return Request.CreateResponse(HttpStatusCode.NotFound);

                    }
                }
                else
                {
                    return Request.CreateResponse<object>(HttpStatusCode.Created, CreateDefaultKonfigTexty(Cesta));
                }
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        #endregion


    }

    public class GlobalKonfigurace : ApiController
    {

        public const string terminalPrefix = "T_";
        public const string UserPrefix = "U_";
        public const string priponaJSON = ".json";
        public const string terminalSuffix = "_TEXT";

        #region externi metoda

        public object VratKonfiguraci(int TerminalID)
        {
            try
            {

                string dstFile = Path.Combine(Fask.MyPath.Path.KonfiguraceTerminalDirectory, terminalPrefix + TerminalID.ToString() + priponaJSON);
                return GetKonfigurace(dstFile);
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return ex;
            }
        }

        public object VratKonfiguraciTexty(int TerminalID)
        {
            try
            {

                string dstFile = Path.Combine(Fask.MyPath.Path.KonfiguraceTerminalDirectory, terminalPrefix + TerminalID.ToString() + terminalSuffix + priponaJSON);
                return GetKonfiguraceTexty(dstFile);
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return ex;
            }
        }
        #endregion

        #region Metody

        private MES_Android.Konfigurace CreateDefaultKonfig(string Cesta)
        {
            try
            {

                MES_Android.Konfigurace konfigurace = new MES_Android.Konfigurace();

                if (!Directory.Exists(Fask.MyPath.Path.KonfiguraceTerminalDirectory))
                    Directory.CreateDirectory(Fask.MyPath.Path.KonfiguraceTerminalDirectory);

                var JSON = Fask.MST_W_Server.API.JSON_Class.Serialize_JSON(konfigurace);

                File.WriteAllText(Cesta, JSON);

                return konfigurace;
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return null;
            }
        }

        private object CreateDefaultKonfigTexty(string Cesta)
        {
            try
            {

                //MES_Android.KonfiguraceTexty konfigurace = new MES_Android.KonfiguraceTexty();
                object konfigurace = new object();

                if (!Directory.Exists(Fask.MyPath.Path.KonfiguraceTerminalDirectory))
                    Directory.CreateDirectory(Fask.MyPath.Path.KonfiguraceTerminalDirectory);

                var JSON = Fask.MST_W_Server.API.JSON_Class.Serialize_JSON(konfigurace);

                File.WriteAllText(Cesta, JSON);

                return konfigurace;
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return null;
            }
        }

        private object GetKonfigurace(string Cesta)
        {

           // HttpResponseMessage msg = new HttpResponseMessage();
            try
            {

                if (Directory.Exists(Fask.MyPath.Path.KonfiguraceTerminalDirectory))
                {
                    if (!File.Exists(Cesta))
                        return  CreateDefaultKonfig(Cesta);
                    //return Request.CreateResponse<MES_Android.Konfigurace>(HttpStatusCode.Created, CreateDefaultKonfig(Cesta));
                    else
                    {

                        string JSON = File.ReadAllText(Cesta);

                        MES_Android.Konfigurace konf = Newtonsoft.Json.JsonConvert.DeserializeObject<MES_Android.Konfigurace>(JSON);

                        if (konf != null)
                            return konf;
                        //return Request.CreateResponse<object>(HttpStatusCode.OK, konf);
                        //return Request.CreateResponse<MES_Android.Konfigurace>(HttpStatusCode.OK, konf);
                        else
                            return null;
                        //return Request.CreateResponse(HttpStatusCode.NotFound);

                    }
                }
                else
                {
                    return CreateDefaultKonfig(Cesta);
                    //return Request.CreateResponse<MES_Android.Konfigurace>(HttpStatusCode.Created, CreateDefaultKonfig(Cesta));
                }
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
               // return Request.CreateResponse(HttpStatusCode.NotFound);
                return null;
            }
        }

        private object GetKonfiguraceTexty(string Cesta)
        {

            // HttpResponseMessage msg = new HttpResponseMessage();
            try
            {

                if (Directory.Exists(Fask.MyPath.Path.KonfiguraceTerminalDirectory))
                {
                    if (!File.Exists(Cesta))
                        return CreateDefaultKonfigTexty(Cesta);
                    //return Request.CreateResponse<MES_Android.Konfigurace>(HttpStatusCode.Created, CreateDefaultKonfig(Cesta));
                    else
                    {

                        string JSON = File.ReadAllText(Cesta);

                        //MES_Android.KonfiguraceTexty konf = Newtonsoft.Json.JsonConvert.DeserializeObject<MES_Android.KonfiguraceTexty>(JSON);

                        object konf = Newtonsoft.Json.JsonConvert.DeserializeObject<object>(JSON);


                        if (konf != null)
                            return konf;
                        //return Request.CreateResponse<object>(HttpStatusCode.OK, konf);
                        //return Request.CreateResponse<MES_Android.Konfigurace>(HttpStatusCode.OK, konf);
                        else
                            return null;
                        //return Request.CreateResponse(HttpStatusCode.NotFound);

                    }
                }
                else
                {
                    return CreateDefaultKonfigTexty(Cesta);
                    //return Request.CreateResponse<MES_Android.Konfigurace>(HttpStatusCode.Created, CreateDefaultKonfig(Cesta));
                }
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                // return Request.CreateResponse(HttpStatusCode.NotFound);
                return null;
            }
        }

        #endregion

    }
}