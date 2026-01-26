using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using System.IO;

namespace Fask.MST_W_Server
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            //AppDomain.CurrentDomain.SetData("SQLServerCompactEdition
            //UnderWebHosting", true);

            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);

            string rootpath = Server.MapPath("~");
            Fask.MyPath.Path.RootPath = rootpath;

            //Logovani
            #region Nutne poradi inicializace ... Logovani, Konfigurace, Parametry
            Fask.Logging.ExceptionHandler2.SetEnablePrint(true);
            Fask.Logging.ExceptionHandler2.SetPath(rootpath);
            Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Info, "Nastavena cesta :'" + rootpath + "'");

            Konfigurace.Classes.Globals_Konfig_WebConfig.LoadConfiguration();
            Konfigurace.Classes.Globals_Konfig_Agendy.LoadConfiguration();

            SQL.Globals_V1.LoadConfiguration();
            ModuleSql.Globals.LoadConfiguration();
            Module.ABRA.CarpServise.Globals_V1.LoadConfiguration();
            Module.ABRA.SAB.Globals_V1.LoadConfiguration();

            Module.Print.Hanibal.Globals_V1.LoadConfiguration();
            Module.Pohoda.I_Tec.Globals_V1.LoadConfiguration();

            Fask.Logging.ExceptionHandler2.EnablePrintLogging = Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Logs[0].Prints;
            Fask.Logging.ExceptionHandler2.SendErrorEmail = Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Logs[0].ErrorsSendByMail;
            #endregion

            //Informace o DB
            Fask.Columns.Inicializace.InitInstance = new Fask.Columns.Inicializace(Path.Combine(rootpath, "DS_Information.xml"));

            //povolit Trace
            Fask.Tracing.Trac.Enable = Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Logs[0].Traces;

            //uzivatel
            FASK.Logins.Uzivatel.Instance = new FASK.Logins.Uzivatel(Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.ConnectionString[0].FASKDB);

            //Licence
            Licensing.License license = Licensing.Licensing.GetLicense();
            Application[Constants.Common.license] = license;

            Application[Constants.Common.Check_Application_BeginRequest] = Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Licenses[0].Check_Application_BeginRequest;
            Application[Constants.Common.Collecting_Stats] = Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Licenses[0].Collecting_Stats;
            Application[Constants.Common.Collecting_Stats_Time_Hours] = Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Licenses[0].Collecting_Stats_Time_Hours;
            Application[Constants.Common.Show_Info_On_Terminal_Before_Expiration] = Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Licenses[0].Show_Info_On_Terminal_Before_Expiration;
            
            //Directories
            Fask.MyPath.Path.StatusObjectDirectory = Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Directories[0].StatusObjects;
            Fask.MyPath.Path.ProcessedDataFileDirectory = Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Directories[0].Processed;
            Fask.MyPath.Path.ErrorDataFileDirectory = Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Directories[0].Error;
            Fask.MyPath.Path.PrintLogDirectory = Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Directories[0].Print;
            Fask.MyPath.Path.ImagesDataFileDirectory = Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Directories[0].Images;
            Fask.MyPath.Path.TracingDataFileDirectory = Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Directories[0].Tracing;
            
        }


        protected void Session_Start(object sender, EventArgs e)
        {
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (bool.Parse(Application[Constants.Common.Check_Application_BeginRequest].ToString()))
            {
                Licensing.License license = Application[Constants.Common.license] as Licensing.License;
                if (license == null || license.isExpirated || !license.isValid)
                    throw new HttpException("Licence na serveru není platná!");
            }

            if (bool.Parse(Application[Constants.Common.Collecting_Stats].ToString()))
            {
                Fask.MST_W_Server.Classes.Stats.setRequestTime(int.Parse(Application[Constants.Common.Collecting_Stats_Time_Hours].ToString()));
                Fask.MST_W_Server.Classes.Stats.addRequest();
            }

            // \TODO
            //ukladani IP do SQL

            //1. tabulka  CZMST_TERMINAL_DEFINITION v ktere sou pevne nadefinovane ID terminalu a typ(SQLCE, SQLite...)
            //2. tabulka  CZMST_TERMINAL_AKT v ktere je nadefinovane ID terminalu jak vazba na  CZMST_TERMINAL_DEFINITION a pan IP terminalu a DATETIME kdz byl zaznam aktualizovan
            //3. dojde request sem na server tak se koukne do tabulky CZMST_TERMINAL_AKT a podle IP zisti zda existuje
            // 3.1 pokud ano tak zisti dotazem ID terminalu + typ a uloží čas update kdy to zistil
            // 3.2 pokud ne tak vyžada ID terminalu + typ, zisti zda terminal existuje a uloží noví zaznam s IP, ID a časem


            // TaD : 18.6.2020 Tabulka CZMST_TERMINAL_DEFINITION je už k ničemu, ale i tam pořád je na SQL a tady v projektu v datasetu


            //vytahnout FASK_TID parametr z hlavicky requestu, je tam parametr? vytahnu hodnotu a doplnim misto 0
            //zjistit jestli obsahuje parametr, pokud ano vzit hodnotu jinak natvrdo 0
           // this.Request.Headers.
           // int TID_pom = this.Request.Headers.

            string IP = this.Request.UserHostAddress;

            string TID = "0";
            string TYP = "-";

            foreach (string item in this.Request.Headers.AllKeys)
            {
                if(item.Trim() == "FASK_TID")
                    TID = this.Request.Headers[item];

                if (item.Trim() == "FASK_TYPE_KLIENT")
                    TYP = this.Request.Headers[item];
            }
            

            Terminal_Komnikace.InsertTerminalsByIP2(IP.Trim(), TID, TYP);
			// \bug - Vytvořit mechanizmus pro synchronizaci IP s ID terminalu
        }


        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exc = Server.GetLastError();
            ;
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }




        //globalni metoda pro konfigurace:
        //TODO TaD doplnit 
        //TODO MaR 7.7.2022 --create method





    }
}