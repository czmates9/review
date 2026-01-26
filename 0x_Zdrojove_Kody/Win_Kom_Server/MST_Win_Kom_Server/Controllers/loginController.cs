using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Fask.Server.Interfaces.DataSets;
using Fask.MST_W_Server.API_BusinessObjects;
using static Fask.Server.Interfaces.DataSets.Pristupy;
using System.Reflection;

namespace Fask.MST_W_Server.Controllers
{
    public class loginController : ApiController
    {

        Fask.Server.Interfaces.Login.ILogin provider = null;
        public Pristupy.FASK_LoginsRow loginsrowPom { get; set; }

        private void Init()
        {
            try
            {
                Konfigurace.Classes.Globals_Konfig_WebConfig.LoadConfiguration();
                string providerAssemblyPath = Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Providers[0].Provider_Login;
                string providerAssemblyPathGlobal = Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Providers[0].Provider;
                if (String.IsNullOrEmpty(providerAssemblyPath))
                    providerAssemblyPath = providerAssemblyPathGlobal;

                if (!String.IsNullOrEmpty(providerAssemblyPath))
                {
                    if (provider == null) //inicializace se provede pouze pokud nebyla provedena ... 
                    {
                        //System.Web.Services.WebService ws = new System.Web.Services.WebService();

                        //Assembly providerAssemlby = Assembly.LoadFrom(ws.Server.MapPath(providerAssemblyPath));
                        string s = @"~/" + providerAssemblyPath;
                        var mappedPath = System.Web.Hosting.HostingEnvironment.MapPath(s);
                        Assembly providerAssemlby = Assembly.LoadFrom(mappedPath);

                        Type[] types = providerAssemlby.GetTypes();
                        foreach (Type t in types)
                        {
                            try
                            {
                                if (typeof(Fask.Server.Interfaces.Login.ILogin).IsAssignableFrom(t))
                                {
                                    provider = (Fask.Server.Interfaces.Login.ILogin)providerAssemlby.CreateInstance(t.FullName);
                                    if (provider != null)
                                        break;
                                }
                            }
                            catch (Exception ex)
                            {
                                Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }
        #region login

        // GET: api/login
        [HttpGet]
        [Route("api/login")]
        public IEnumerable<Fask.WEBAPI.API_BusinessObjects.FASK_Login> GetUsers()
        {
            List<Fask.WEBAPI.API_BusinessObjects.FASK_Login> logins = new List<Fask.WEBAPI.API_BusinessObjects.FASK_Login>();

            FASK.Logins.DataSets.Pristupy pris = FASK.Logins.Uzivatel.Instance.GetUzivateleSOpravnenim(string.Empty);

            if (pris != null && pris.FASK_Logins.Count > 0)
            {
                foreach (var item in pris.FASK_Logins)
                {
                    logins.Add(new Fask.WEBAPI.API_BusinessObjects.FASK_Login()
                    {
                        CREATED = item.IsCREATEDNull() ? (DateTime?)null : item.CREATED,
                        firstname = item.firstname,
                        psswd = item.psswd,
                        surname = item.surname,
                        USERID = item.USERID,
                        VALIDFROM = item.IsVALIDFROMNull() ? (DateTime?)null : item.VALIDFROM,
                        VALIDTO = item.IsVALIDTONull() ? (DateTime?)null : item.VALIDTO
                    });
                }
            }


            return logins;
        }

        [HttpPost]
        [Route("api/LoginsByPermission")]
        public IEnumerable<Fask.WEBAPI.API_BusinessObjects.FASK_Login> GetUsersByPermission([FromBody] Fask.WEBAPI.API_BusinessObjects.FASK_Login_Permissions V)
        {
            List<Fask.WEBAPI.API_BusinessObjects.FASK_Login> logins = new List<Fask.WEBAPI.API_BusinessObjects.FASK_Login>();

            FASK.Logins.DataSets.Pristupy pris = FASK.Logins.Uzivatel.Instance.GetUzivateleSOpravnenim(V.Permissions);

            if (pris != null && pris.FASK_Logins.Count > 0)
            {
                foreach (var item in pris.FASK_Logins)
                {
                    logins.Add(new Fask.WEBAPI.API_BusinessObjects.FASK_Login()
                    {
                        CREATED = item.IsCREATEDNull() ? (DateTime?)null : item.CREATED,
                        firstname = item.firstname,
                        psswd = item.psswd,
                        surname = item.surname,
                        USERID = item.USERID,
                        VALIDFROM = item.IsVALIDFROMNull() ? (DateTime?)null : item.VALIDFROM,
                        VALIDTO = item.IsVALIDTONull() ? (DateTime?)null : item.VALIDTO
                    });
                }
            }


            return logins;
        }



        //// GET: api/login/5
        //[Route("api/login/TerminalID/{TerminalID}")]
        //public IEnumerable<BusinessObjects.FASK_Login> Get(int TerminalID)
        //{
        //    List<BusinessObjects.FASK_Login> logins = new List<BusinessObjects.FASK_Login>();

        //    FASK.Logins.DataSets.Pristupy pris = FASK.Logins.Uzivatel.Instance.GetUzivateleSOpravnenim(string.Empty);
        //    if (pris != null && pris.FASK_Logins.Count > 0)
        //    {
        //        foreach (var item in pris.FASK_Logins)
        //        {
        //            if (true)
        //            {
        //                logins.Add(new BusinessObjects.FASK_Login()
        //                {
        //                    CREATED = item.IsCREATEDNull() ? (DateTime?)null : item.CREATED,
        //                    firstname = item.firstname,
        //                    psswd = item.psswd,
        //                    surname = item.surname,
        //                    USERID = item.USERID,
        //                    VALIDFROM = item.IsVALIDFROMNull() ? (DateTime?)null : item.VALIDFROM,
        //                    VALIDTO = item.IsVALIDTONull() ? (DateTime?)null : item.VALIDTO
        //                }); 
        //            }
        //        }
        //    }

        //    return logins;
        //}

        //// GET: api/login/5/11
        //[Route("api/login/UserID_/_TerminalID/{userID}/{TerminalID}")]
        //public IEnumerable<BusinessObjects.FASK_Login> Get(string userID, int TerminalID)
        //{
        //    List<BusinessObjects.FASK_Login> logins = new List<BusinessObjects.FASK_Login>();

        //    FASK.Logins.DataSets.Pristupy pris = FASK.Logins.Uzivatel.Instance.GetUzivateleSOpravnenim(string.Empty);
        //    if (pris != null && pris.FASK_Logins.Count > 0)
        //    {
        //        foreach (var item in pris.FASK_Logins)
        //        {
        //            logins.Add(new BusinessObjects.FASK_Login()
        //            {
        //                CREATED = item.IsCREATEDNull() ? (DateTime?)null : item.CREATED,
        //                firstname = item.firstname,
        //                psswd = item.psswd,
        //                surname = item.surname,
        //                USERID = item.USERID,
        //                VALIDFROM = item.IsVALIDFROMNull() ? (DateTime?)null : item.VALIDFROM,
        //                VALIDTO = item.IsVALIDTONull() ? (DateTime?)null : item.VALIDTO
        //            });
        //        }
        //    }

        //    return logins;
        //}

        // GET: api/login/5

        // GET: api/login/5

        [HttpGet]
        [Route("api/login/{userID}")]
        public Fask.WEBAPI.API_BusinessObjects.FASK_Login GetUser(string userID)
        {
            List<Fask.WEBAPI.API_BusinessObjects.FASK_Login> logins = new List<Fask.WEBAPI.API_BusinessObjects.FASK_Login>();

            FASK.Logins.DataSets.Pristupy pris = FASK.Logins.Uzivatel.Instance.GetUzivateleSOpravnenim(string.Empty);
            if (pris != null && pris.FASK_Logins.Count > 0)
            {
                foreach (var item in pris.FASK_Logins)
                {
                    if (item.USERID.Trim() == userID.Trim())
                    {
                        logins.Add(new Fask.WEBAPI.API_BusinessObjects.FASK_Login()
                        {
                            CREATED = item.IsCREATEDNull() ? (DateTime?)null : item.CREATED,
                            firstname = item.firstname,
                            psswd = item.psswd,
                            surname = item.surname,
                            USERID = item.USERID,
                            VALIDFROM = item.IsVALIDFROMNull() ? (DateTime?)null : item.VALIDFROM,
                            VALIDTO = item.IsVALIDTONull() ? (DateTime?)null : item.VALIDTO
                        });
                    }
                }
            }

            if (logins.Count != 1)
            {
                //Tohle by nemnelo nidky nastat, USER ID je na urovni DB jak primarykey takže nemuže byt duplicita stejneho ID
                return null;
            }
            else
                return logins[0];
        }

        // POST: api/login
        [HttpPost]
        [Route("api/login")]
        public HttpResponseMessage SaveNewUser([FromBody] Fask.WEBAPI.API_BusinessObjects.FASK_Login V)
        {
            if (V != null)
            {
                string x = V.firstname;

                return Request.CreateResponse<Fask.WEBAPI.API_BusinessObjects.FASK_Login>(HttpStatusCode.OK, V);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }


        // PUT: api/login/5
        [HttpPut]
        [Route("api/login/{UserID}")]
        public void UpdateUser(int UserID, [FromBody] Fask.WEBAPI.API_BusinessObjects.FASK_Login value)
        {
            var x = value;
        }

        // DELETE: api/login/5
        [HttpDelete]
        [Route("api/login/{UserID}")]
        public void RemoveUser(int UserID)
        {
            var x = UserID;
        } 
        #endregion

        #region Logins commans
        [HttpGet]
        [Route("api/GetLogins")]
        public HttpResponseMessage GetLogins()
        {
            Init();

            if (provider != null && provider is Fask.Server.Interfaces.Login.ILogin_Commans)
            {
                try
                {

                    FASK_LoginsDataTable dt = new FASK_LoginsDataTable();

                    dt = ((Fask.Server.Interfaces.Login.ILogin_Commans)provider).GetLogins();
                    return Request.CreateResponse<FASK_LoginsDataTable>(HttpStatusCode.OK, dt);
                }
                catch (Exception ex)
                {
                    //return Request.CreateResponse<string>(HttpStatusCode.BadRequest, ex.Message + ex.StackTrace);
                    return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
                }

            }
            else
                return Request.CreateResponse(HttpStatusCode.NotFound);

        }

        [HttpGet]
        [Route("api/GetViewData/{userID}")]
        public HttpResponseMessage GetViewData(string userID)
        {
            Init();

            try
            {
                if (provider != null && provider is Fask.Server.Interfaces.Login.ILogin_Commans)
                {
                    try
                    {

                        Pristupy dt = new Pristupy();

                        dt = ((Fask.Server.Interfaces.Login.ILogin_Commans)provider).GetViewData(userID);
                        return Request.CreateResponse<Pristupy>(HttpStatusCode.OK, dt);
                    }
                    catch (Exception ex)
                    {
                        //return Request.CreateResponse<string>(HttpStatusCode.BadRequest, ex.Message + ex.StackTrace);
                        return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
                    }

                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
            }

        }

        [HttpGet]
        [Route("api/GetOverLogins")]
        public HttpResponseMessage GetOverLogins()
        {
            Init();
           

            try
            {
                #region parametry z URL
                string USERID = string.Empty;
                string PWD = string.Empty;
                string AGENDA = string.Empty;
                // Zdrojove URL, resp. parametry z něho
                string URL_param = Request.RequestUri.Query;

                //list parametru
                var SeznamParametru = HttpUtility.ParseQueryString(URL_param);

                var USERIDtmp = SeznamParametru.Get("USERID");
                USERID = USERIDtmp.ToString();

                var PWDtmp = SeznamParametru.Get("PWD");
                PWD = PWDtmp.ToString();

                var AGENDAtmp = SeznamParametru.Get("AGENDA");
                AGENDA = AGENDAtmp.ToString();
                #endregion

                if (provider != null && provider is Fask.Server.Interfaces.Login.ILogin_Commans)
                {
                    try
                    {

                        Pristupy.FASK_LoginsDataTable dt = new Pristupy.FASK_LoginsDataTable();

                        dt = ((Fask.Server.Interfaces.Login.ILogin_Commans)provider).GetOverLogins(USERID, PWD, AGENDA);
                        return Request.CreateResponse<Pristupy.FASK_LoginsDataTable>(HttpStatusCode.OK, dt);
                    }
                    catch (Exception ex)
                    {
                        //return Request.CreateResponse<string>(HttpStatusCode.BadRequest, ex.Message + ex.StackTrace);
                        return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
                    }

                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
            }

        }

        [HttpGet]
        [Route("api/GetLoginsByID/{AgendaID}")]
        public HttpResponseMessage GetLoginsByID(string AgendaID)
        {
            Init();

            try
            {
                if (provider != null && provider is Fask.Server.Interfaces.Login.ILogin_Commans)
                {
                    try
                    {

                        Pristupy.FASK_LoginsDataTable dt = new Pristupy.FASK_LoginsDataTable();

                        dt = ((Fask.Server.Interfaces.Login.ILogin_Commans)provider).GetLoginsByID(AgendaID);
                        return Request.CreateResponse<Pristupy.FASK_LoginsDataTable>(HttpStatusCode.OK, dt);
                    }
                    catch (Exception ex)
                    {
                        //return Request.CreateResponse<string>(HttpStatusCode.BadRequest, ex.Message + ex.StackTrace);
                        return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
                    }

                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
            }

        }

        [HttpGet]
        [Route("api/GetLikeAgednaID/{AgendaID}")]
        public HttpResponseMessage GetLikeAgednaID(string AgendaID)
        {
            Init();

            try
            {
                if (provider != null && provider is Fask.Server.Interfaces.Login.ILogin_Commans)
                {
                    try
                    {

                        Pristupy dt = new Pristupy();

                        dt = ((Fask.Server.Interfaces.Login.ILogin_Commans)provider).GetLikeAgednaID(AgendaID);
                        return Request.CreateResponse<Pristupy>(HttpStatusCode.OK, dt);
                    }
                    catch (Exception ex)
                    {
                        //return Request.CreateResponse<string>(HttpStatusCode.BadRequest, ex.Message + ex.StackTrace);
                        return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
                    }

                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
            }

        }

        [HttpGet]
        [Route("api/GetAgednaID/{AgendaID}")]
        public HttpResponseMessage GetAgednaID(string AgendaID)
        {
            Init();

            try
            {
                if (provider != null && provider is Fask.Server.Interfaces.Login.ILogin_Commans)
                {
                    try
                    {

                        Pristupy dt = new Pristupy();

                        dt = ((Fask.Server.Interfaces.Login.ILogin_Commans)provider).GetAgednaID(AgendaID);
                        return Request.CreateResponse<Pristupy>(HttpStatusCode.OK, dt);
                    }
                    catch (Exception ex)
                    {
                        //return Request.CreateResponse<string>(HttpStatusCode.BadRequest, ex.Message + ex.StackTrace);
                        return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
                    }

                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
            }

        }
        #endregion

        #region Logins komunikace
        [HttpPost]
        [Route("api/GetFASK_AGENDA_Filtrovana")]
        public HttpResponseMessage GetFASK_AGENDA_Filtrovana([FromBody] Fask.WEBAPI.API_BusinessObjects.Filtry_Agenda filtr)
        {
            Init();

            try
            {
                if (provider != null && provider is Fask.Server.Interfaces.Login.ILogin_Komunikace)
                {
                    try
                    {

                        Pristupy dt = new Pristupy();

                        dt = ((Fask.Server.Interfaces.Login.ILogin_Komunikace)provider).GetFASK_AGENDA_Filtrovana(filtr);
                        return Request.CreateResponse<Pristupy>(HttpStatusCode.OK, dt);
                    }
                    catch (Exception ex)
                    {
                        //return Request.CreateResponse<string>(HttpStatusCode.BadRequest, ex.Message + ex.StackTrace);
                        return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
                    }

                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
            }

        }

        [HttpPost]
        [Route("api/GetFiltrovanyLogins")]
        public HttpResponseMessage GetFiltrovanyLogins([FromBody] Fask.WEBAPI.API_BusinessObjects.Filtry_Login filtr)
        {
            Init();

            try
            {
                if (provider != null && provider is Fask.Server.Interfaces.Login.ILogin_Komunikace)
                {
                    try
                    {

                        Pristupy dt = new Pristupy();

                        dt = ((Fask.Server.Interfaces.Login.ILogin_Komunikace)provider).GetFiltrovanyLogins(filtr);
                        return Request.CreateResponse<Pristupy>(HttpStatusCode.OK, dt);
                    }
                    catch (Exception ex)
                    {
                        //return Request.CreateResponse<string>(HttpStatusCode.BadRequest, ex.Message + ex.StackTrace);
                        return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
                    }

                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
            }

        }

        [HttpPost]
        [Route("api/GetLogins_Auth")]
        public HttpResponseMessage GetLogins_Auth([FromBody] Fask.WEBAPI.API_BusinessObjects.Filtry_Auth filtr)
        {
            Init();

        
            try
            {
                if (provider != null && provider is Fask.Server.Interfaces.Login.ILogin_Komunikace)
                {
                    try
                    {

                        Pristupy dt = new Pristupy();

                        dt = ((Fask.Server.Interfaces.Login.ILogin_Komunikace)provider).GetLogins_Auth(filtr);
                        return Request.CreateResponse<Pristupy>(HttpStatusCode.OK, dt);
                    }
                    catch (Exception ex)
                    {
                        //return Request.CreateResponse<string>(HttpStatusCode.BadRequest, ex.Message + ex.StackTrace);
                        return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
                    }

                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
            }

        }

        [HttpGet]
        [Route("api/isExist_Auth")]
        public HttpResponseMessage isExist_Auth()
        {
            Init();

            if (provider != null && provider is Fask.Server.Interfaces.Login.ILogin_Komunikace)
            {
                try
                {
                    #region parametry z URL
                    string USERID = string.Empty;
                    string AGENDAID = string.Empty;
                    // Zdrojove URL, resp. parametry z něho
                    string URL_param = Request.RequestUri.Query;

                    //list parametru
                    var SeznamParametru = HttpUtility.ParseQueryString(URL_param);

                    var USERIDtmp = SeznamParametru.Get("USERID");
                    USERID = USERIDtmp.ToString();

                    var AGENDAIDtmp = SeznamParametru.Get("AGENDAID");
                    AGENDAID = AGENDAIDtmp.ToString();
                    #endregion


                    bool vysledek = false;

                    vysledek = ((Fask.Server.Interfaces.Login.ILogin_Komunikace)provider).isExist_Auth(USERID, AGENDAID);
                    return Request.CreateResponse<bool>(HttpStatusCode.OK, vysledek);
                }
                catch (Exception ex)
                {
                    //return Request.CreateResponse<string>(HttpStatusCode.BadRequest, ex.Message + ex.StackTrace);
                    return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
                }

            }
            else
                return Request.CreateResponse(HttpStatusCode.NotFound);

        }

        [HttpDelete]
        [Route("api/Delete_Login/{id}")]
        public HttpResponseMessage Delete_Login(string id)
        {
            Init();

            if (provider != null && provider is Fask.Server.Interfaces.Login.ILogin_Komunikace)
            {
                try
                {

                    bool vysledek = false;

                    vysledek = ((Fask.Server.Interfaces.Login.ILogin_Komunikace)provider).Delete_Login(id);
                    return Request.CreateResponse<bool>(HttpStatusCode.OK, vysledek);
                }
                catch (Exception ex)
                {
                    //return Request.CreateResponse<string>(HttpStatusCode.BadRequest, ex.Message + ex.StackTrace);
                    return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
                }

            }
            else
                return Request.CreateResponse(HttpStatusCode.NotFound);

        }

        [HttpDelete]
        [Route("api/Delete_Auth/{DEX_ROW_ID}")]
        public HttpResponseMessage Delete_Auth(int DEX_ROW_ID)
        {
            Init();

            if (provider != null && provider is Fask.Server.Interfaces.Login.ILogin_Komunikace)
            {
                try
                {

                    bool vysledek = false;

                    vysledek = ((Fask.Server.Interfaces.Login.ILogin_Komunikace)provider).Delete_Auth(DEX_ROW_ID);
                    return Request.CreateResponse<bool>(HttpStatusCode.OK, vysledek);
                }
                catch (Exception ex)
                {
                    //return Request.CreateResponse<string>(HttpStatusCode.BadRequest, ex.Message + ex.StackTrace);
                    return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
                }

            }
            else
                return Request.CreateResponse(HttpStatusCode.NotFound);

        }

        [HttpDelete]
        [Route("api/test_delete/{id}")]
        public void test(int id)
        {
            int pom = 22;
            Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, pom.ToString());
        }

        [HttpPost]
        [Route("api/Update_Login_Row")]
        public HttpResponseMessage Update_Login_Row([FromBody] Fask.WEBAPI.API_BusinessObjects.FASK_Login login_data)
        {
            Init();

            if (provider != null && provider is Fask.Server.Interfaces.Login.ILogin_Komunikace)
            {
                try
                {


                    #region plneny objektu pro JSON
                    FASK_LoginsDataTable dt = new FASK_LoginsDataTable();
                    FASK_LoginsRow loginsrow = dt.NewFASK_LoginsRow();

                    loginsrow.USERID = string.IsNullOrEmpty(login_data.USERID) ? throw new Exception("USERID is null!!") : login_data.USERID;
                    loginsrow.firstname = string.IsNullOrEmpty(login_data.firstname) ? string.Empty : login_data.firstname;
                    loginsrow.surname = string.IsNullOrEmpty(login_data.surname) ? string.Empty : login_data.surname;
                    loginsrow.psswd = string.IsNullOrEmpty(login_data.psswd) ? string.Empty : login_data.psswd;

                    if (login_data.CREATED.HasValue)
                        loginsrow.CREATED = login_data.CREATED.Value;
                    else
                        loginsrow.SetCREATEDNull();

                    if (login_data.VALIDFROM.HasValue)
                        loginsrow.VALIDFROM = login_data.VALIDFROM.Value;
                    else
                        loginsrow.SetVALIDFROMNull();

                    if (login_data.VALIDTO.HasValue)
                        loginsrow.VALIDTO = login_data.VALIDTO.Value;
                    else
                        loginsrow.SetVALIDTONull();

                    if (login_data.RFID == null)
                        loginsrow.RFID = null;
                    else
                        loginsrow.RFID = login_data.RFID;
                    #endregion


                    bool vysledek = false;

                    vysledek = ((Fask.Server.Interfaces.Login.ILogin_Komunikace)provider).Update_Login_Row(loginsrow);
                    return Request.CreateResponse<bool>(HttpStatusCode.OK, vysledek);
                }
                catch (Exception ex)
                {
                    //return Request.CreateResponse<string>(HttpStatusCode.BadRequest, ex.Message + ex.StackTrace);
                    return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
                }

            }
            else
                return Request.CreateResponse(HttpStatusCode.NotFound);

        }

        [HttpPost]
        [Route("api/Insert_Login")]
        public HttpResponseMessage Insert_Login()
        {
            Init();

            if (provider != null && provider is Fask.Server.Interfaces.Login.ILogin_Komunikace)
            {
                try
                {
                    #region parametry z URL
                    string USERID = string.Empty;
                    string firstname = string.Empty;
                    string surname = string.Empty;
                    string psswd = string.Empty;
                    // Zdrojove URL, resp. parametry z něho
                    string URL_param = Request.RequestUri.Query;

                    //list parametru
                    var SeznamParametru = HttpUtility.ParseQueryString(URL_param);

                    var USERIDtmp = SeznamParametru.Get("USERID");
                    USERID = USERIDtmp.ToString();

                    var firstnametmp = SeznamParametru.Get("firstname");
                    firstname = firstnametmp.ToString();

                    var surnametmp = SeznamParametru.Get("surname");
                    surname = surnametmp.ToString();

                    var psswdtmp = SeznamParametru.Get("psswd");
                    psswd = psswdtmp.ToString();
                    #endregion

                    bool vysledek = false;

                    vysledek = ((Fask.Server.Interfaces.Login.ILogin_Komunikace)provider).Insert_Login( USERID,  firstname,  surname,  psswd);
                    return Request.CreateResponse<bool>(HttpStatusCode.OK, vysledek);
                }
                catch (Exception ex)
                {
                    //return Request.CreateResponse<string>(HttpStatusCode.BadRequest, ex.Message + ex.StackTrace);
                    return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
                }

            }
            else
                return Request.CreateResponse(HttpStatusCode.NotFound);

        }

        [HttpPost]
        [Route("api/Insert_Auth")]
        public HttpResponseMessage Insert_Auth()
        {
            Init();

            if (provider != null && provider is Fask.Server.Interfaces.Login.ILogin_Komunikace)
            {
                try
                {
                    #region parametry z URL
                    string USERID = string.Empty;
                    string AGENDAID = string.Empty;
                    // Zdrojove URL, resp. parametry z něho
                    string URL_param = Request.RequestUri.Query;

                    //list parametru
                    var SeznamParametru = HttpUtility.ParseQueryString(URL_param);

                    var USERIDtmp = SeznamParametru.Get("USERID");
                    USERID = USERIDtmp.ToString();

                    var AGENDAIDtmp = SeznamParametru.Get("AGENDAID");
                    AGENDAID = AGENDAIDtmp.ToString();
                    #endregion

                    bool vysledek = false;

                    vysledek = ((Fask.Server.Interfaces.Login.ILogin_Komunikace)provider).Insert_Auth( USERID,  AGENDAID);
                    return Request.CreateResponse<bool>(HttpStatusCode.OK, vysledek);
                }
                catch (Exception ex)
                {
                    //return Request.CreateResponse<string>(HttpStatusCode.BadRequest, ex.Message + ex.StackTrace);
                    return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
                }

            }
            else
                return Request.CreateResponse(HttpStatusCode.NotFound);

        }
        #endregion

    }
}