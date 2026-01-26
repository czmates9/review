using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Net.Mail;
using System.Reflection;
using System.Web;
using System.Web.Services;
using Fask.Logging;
using Fask.Tracing;

namespace Fask.MST_W_Server
{
	/// <summary>
	/// Služba informací.
	/// </summary>
	[WebService(Namespace = "http://Informations.fask.cz/", Description = "Služba informací", Name = "InformationsService")]
    public class Informations : System.Web.Services.WebService
	{

        #region Lokalne promenne

        private Fask.Server.Interfaces.IMST provider = null;

		#endregion

		#region Konstruktor
		/// <summary>
		/// Konstruktor
		/// </summary>
		public Informations()
		{
			//CODEGEN: This call is required by the ASP.NET Web Services Designer
			InitializeComponent();

            try
            {
                Konfigurace.Classes.Globals_Konfig_WebConfig.LoadConfiguration();
                string providerAssemblyPath = Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Providers[0].Provider_Informations;
                string providerAssemblyPathGlobal = Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Providers[0].Provider;
                if (String.IsNullOrEmpty(providerAssemblyPath))
                    providerAssemblyPath = providerAssemblyPathGlobal;

                if (!String.IsNullOrEmpty(providerAssemblyPath))
                {
                    if (provider == null) //inicializace se provede pouze pokud nebyla provedena ... 
                    {
                        Assembly providerAssemlby = Assembly.LoadFrom(Server.MapPath(providerAssemblyPath));
                        Type[] types = providerAssemlby.GetTypes();
                        foreach (Type t in types)
                        {
                            try
                            {
                                if (typeof(Fask.Server.Interfaces.IMST).IsAssignableFrom(t))
                                {
                                    provider = (Fask.Server.Interfaces.IMST)providerAssemlby.CreateInstance(t.FullName);
                                    if (provider != null)
                                    {
                                        if (provider is Fask.Server.Interfaces.Configuration.IConfiguration)
                                        {
                                            ((Fask.Server.Interfaces.Configuration.IConfiguration)provider).LoadConfiguration();
                                        }
                                        break;
                                    }
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
		#endregion

		#region Component Designer generated code
		
		//Required by the Web Services Designer 
		private IContainer components = null;
				
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if(disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);		
		}
		
		#endregion

		#region WebMetody

		/// <summary>
		/// Metoda která vrací dataset z uložené procedury definované v konfiguraci pod hodnotou pøijatou v parametru pøíkazu. Uložená procedura na serveru SQL vyžaduje 2 parametry øetìzce a vrací výsledek tabulky SQL. 
		/// </summary>
		/// <param name="param1">Parametr 1</param>
		/// <param name="param2">Parametr 2</param>
		/// <returns></returns>
		[WebMethod(Description = "Metoda která vrací dataset z uložené procedury definované v konfiguraci pod hodnotou pøijatou v parametru pøíkazu. Uložená procedura na serveru SQL vyžaduje 2 parametry øetìzce a vrací výsledek tabulky SQL.")]
		public DataSet Command1(string param1, string param2) //object[] parameters)
		{
            TracId tracid = new TracId(null, null, null, "Command1");
            try
            {
                #region trace
                Trac.Write("Start", tracid);
                #endregion


                if ((provider != null) && (provider is Fask.Server.Interfaces.Informations.IInformations2_Command1))
                {


                    #region trace
                    Trac.Write("Provider start", tracid);
                    #endregion

                    try
                    {
                        return (provider as Fask.Server.Interfaces.Informations.IInformations2_Command1).Command1(
                            param1,
                            param2
                            );
                    }
                    catch (Exception ex)
                    {
                        Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                        #region trace
                        Trac.Write(ex, tracid);
                        #endregion
                        throw ex;
                    }
                    finally
                    {
                        #region trace
                        Trac.Write("Provider end", tracid);
                        #endregion
                    }
                }

                string msg = "Provider v Informations.asmx > 'Command1(string param1, string param2)' nenastaven.";
                Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
                #region trace
                Trac.Write(msg, tracid);
                #endregion
                throw new Exception(msg);

            }
            finally
            {
                #region trace
                Trac.Write("End", tracid);
                #endregion
            }
        }

		/// <summary>
		/// Metoda která vrací množství zboží na skladì. Je závislé na uložené proceduøe s jedním parametrem typu char, který obsahuje interní èíslo zboží.
		/// </summary>
		/// <param name="ItemNumber">ID položky</param>
		/// <returns></returns>
        [WebMethod(Description="Metoda která vrací množství zboží na skladì. Je závislé na uložené proceduøe s jedním parametrem typu char, který obsahuje interní èíslo zboží.")]
        public float MnozstviNaSklade(string ItemNumber)
        {
            TracId tracid = new TracId(null, null, null, "MnozstviNaSklade");
            try
            {
                #region trace
                Trac.Write("Start", tracid);
                #endregion


                if ((provider != null) && (provider is Fask.Server.Interfaces.Informations.IInformations2_MnozstviNaSklade))
                {


                    #region trace
                    Trac.Write("Provider start", tracid);
                    #endregion

                    try
                    {
                        return (provider as Fask.Server.Interfaces.Informations.IInformations2_MnozstviNaSklade).MnozstviNaSklade(ItemNumber);
                    }
                    catch (Exception ex)
                    {
                        Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                        #region trace
                        Trac.Write(ex, tracid);
                        #endregion
                        throw ex;
                    }
                    finally
                    {
                        #region trace
                        Trac.Write("Provider end", tracid);
                        #endregion
                    }
                }

                string msg = "Provider v Informations.asmx > 'MnozstviNaSklade(string ItemNumber)' nenastaven.";
                Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
                #region trace
                Trac.Write(msg, tracid);
                #endregion
                throw new Exception(msg);

            }
            finally
            {
                #region trace
                Trac.Write("End", tracid);
                #endregion
            }
        }

		/// <summary>
		/// Metoda která vrací množství položky s interním èíslem na skladu. Závislé na uložené proceduøe v db servereru(parametr1:itemnumber[char], parametr2:location[char])
		/// </summary>
		/// <param name="ItemNumber">ID položky</param>
		/// <param name="Location">Lokace</param>
		/// <returns>množstvi na sklade</returns>
        [WebMethod(Description="Metoda která vrací množství položky s interním èíslem na skladu. Závislé na uložené proceduøe v db servereru(parametr1:itemnumber[char], parametr2:location[char])")]
        public float? MnozstviNaSklade_Itemnumber_Location(string ItemNumber, string Location)
        {
            TracId tracid = new TracId(null, null, null, "MnozstviNaSklade_Itemnumber_Location");
            try
            {
                #region trace
                Trac.Write("Start", tracid);
                #endregion


                if ((provider != null) && (provider is Fask.Server.Interfaces.Informations.IInformations2_MnozstviNaSklade))
                {


                    #region trace
                    Trac.Write("Provider start", tracid);
                    #endregion

                    try
                    {
                        return (provider as Fask.Server.Interfaces.Informations.IInformations2_MnozstviNaSklade_Itemnumber_Location).MnozstviNaSklade_Itemnumber_Location(ItemNumber, Location);
                    }
                    catch (Exception ex)
                    {
                        Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                        #region trace
                        Trac.Write(ex, tracid);
                        #endregion
                        throw ex;
                    }
                    finally
                    {
                        #region trace
                        Trac.Write("Provider end", tracid);
                        #endregion
                    }
                }

                string msg = "Provider v Informations.asmx > 'MnozstviNaSklade_Itemnumber_Location(string ItemNumber, string Location)' nenastaven.";
                Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
                #region trace
                Trac.Write(msg, tracid);
                #endregion
                throw new Exception(msg);

            }
            finally
            {
                #region trace
                Trac.Write("End", tracid);
                #endregion
            }
        }

		/// <summary>
		/// Metoda pro naèteni deailu polozky dle ID, podle konfigurace webConfig DetailItemnumber + parametry
		/// </summary>
		/// <param name="itemnumber">ID položky</param>
		/// <param name="doklad">èíslo dokladu</param>
		/// <returns>Netypovy dataset</returns>
		[WebMethod(Description = "Metoda pro naèteni deailu polozky dle ID, podle konfigurace webConfig DetailItemnumber + parametry")]
        public DataSet DetailItemnumber(string itemnumber, string doklad)
        {
            TracId tracid = new TracId(null, null, null, "DetailItemnumber");
            try
            {
                #region trace
                Trac.Write("Start", tracid);
                #endregion


                if ((provider != null) && (provider is Fask.Server.Interfaces.Informations.IInformations2_MnozstviNaSklade))
                {


                    #region trace
                    Trac.Write("Provider start", tracid);
                    #endregion

                    try
                    {
                        return (provider as Fask.Server.Interfaces.Informations.IInformations2_DetailItemnumber).DetailItemnumber(itemnumber, doklad);
                    }
                    catch (Exception ex)
                    {
                        Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                        #region trace
                        Trac.Write(ex, tracid);
                        #endregion
                        throw ex;
                    }
                    finally
                    {
                        #region trace
                        Trac.Write("Provider end", tracid);
                        #endregion
                    }
                }

                string msg = "Provider v Informations.asmx > 'DetailItemnumber(string itemnumber, string doklad)' nenastaven.";
                Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
                #region trace
                Trac.Write(msg, tracid);
                #endregion
                throw new Exception(msg);

            }
            finally
            {
                #region trace
                Trac.Write("End", tracid);
                #endregion
            }
        }

        #endregion

        #region Z Vyroby

        public static bool SendMailStatic(string body)
        {
            return SendMailStatic(System.Configuration.ConfigurationManager.AppSettings["DohledSMTPHlavicka"], body);
        }

        public static bool SendMailStatic(string subject, string body)
        {
            try
            {
                // nacteni cest
                string ErrorDataFileDirectory = System.Configuration.ConfigurationManager.AppSettings["ErrorDataFileDirectory"];
                string ProcessedDataFileDirectory = System.Configuration.ConfigurationManager.AppSettings["ProcessedDataFileDirectory"];
                string ErrorLogFile = System.Configuration.ConfigurationManager.AppSettings["ErrorLogFile"];

                bool dohledsmtp = true;
                try { dohledsmtp = bool.Parse(ConfigurationManager.AppSettings["DohledSMTP"]); }
                catch { }

                if (!dohledsmtp)
                    return true;

                MailMessage mail = new MailMessage(
                    ConfigurationManager.AppSettings["DohledSMTPFromAddress"],
                    ConfigurationManager.AppSettings["DohledSMTPToAddress"]
                    );
                mail.Subject = subject;
                mail.Body = body;
                System.Net.Mail.SmtpClient emailclient = new SmtpClient(ConfigurationManager.AppSettings["DohledSMTPServer"]);
                emailclient.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle("Informations.asmx", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                //Log.writeErrorLog("SendMail: " + ex.Message);
                return false;
            }
        }

        [WebMethod]
        public bool SendMail(string subject, string body)
        {
            return Informations.SendMailStatic(subject, body);
        }

        #endregion
    }
}
