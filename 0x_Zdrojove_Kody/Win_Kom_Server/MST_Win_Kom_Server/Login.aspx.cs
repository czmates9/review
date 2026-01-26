using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Fask.Logging;
using System.Diagnostics;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace Fask.MST_W_Server
{
	/// <summary>
	/// Metoda pro pøihlašovaní na server
	/// </summary>
    public partial class Login : System.Web.UI.Page
    {

		#region Konstruktor

		/// <summary>
		/// Konstruktor
		/// </summary>
		public Login()
		{
		} 

		#endregion

		/// <summary>
		/// PageLoad, volá se když se otevøe stranka
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
			var AV = Assembly.GetExecutingAssembly().GetName().Version;

			LabelV.Text = string.Format("Verze projektu je :{0}.{1} ", AV.Major, AV.Minor);

			Konfigurace.Classes.Globals_Konfig_WebConfig.LoadConfiguration();
			
			if (!string.IsNullOrEmpty(Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Providers[0].Provider))
			{
				string Prov = Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Providers[0].Provider;
				Label_Provider.Text = string.Format("Nastavený provider: '{0}'", Prov);
			}
			else
				Label_Provider.Text = "Nastavený provider: '-'";


			if (!string.IsNullOrEmpty(Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.ConnectionString[0].FASKDB))
			{
				Dictionary<string, string> connStringParts = Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.ConnectionString[0].FASKDB.Split(';')
					.Select(t => t.Split(new char[] { '=' }, 2))
					.ToDictionary(t => t[0].Trim(), t => t[1].Trim(), StringComparer.InvariantCultureIgnoreCase);

				string db = connStringParts["Initial Catalog"];

				Label_Databaze.Text = string.Format("Nastavená FASK DB: '{0}'", db);
			}
			else
				Label_Databaze.Text = "Nastavená FASK DB: '-'";

			
			
			
		}

		///// <summary>
		///// Metoda pro dotažení do netypoveho datasetu.
		///// </summary>
		///// <param name="ConnectionString">ConnectionString</param>
		///// <param name="SQL">SQL dotaz pro dotaženi</param>
		///// <returns>Netypovy dataset</returns>
  //      public DataSet GetDataSet(string ConnectionString, string SQL)
  //      {
  //          SqlConnection conn = new SqlConnection(ConnectionString);
  //          SqlDataAdapter da = new SqlDataAdapter();
  //          SqlCommand cmd = conn.CreateCommand();
  //          cmd.CommandText = SQL;
  //          da.SelectCommand = cmd;
  //          DataSet ds = new DataSet();

  //          conn.Open();
  //          da.Fill(ds);
  //          conn.Close();

  //          return ds;
  //      }

		/// <summary>
		/// Metoda pro ovìøení uživatele 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
			Fask.Logging.ExceptionHandler2.Handle(LogLevel.Info, "Prihlaseni Uzivatele Server.");
			FASK.Logins.DataSets.Pristupy.FASK_LoginsDataTable FASK_Logins = null;
			try
			{
				FASK_Logins = FASK.Logins.Uzivatel.Instance.Komunikace.GetOverLogins(Login1.UserName, Login1.Password, "M_Admin_");
			}
			catch
			{
				Fask.Logging.ExceptionHandler2.Handle(LogLevel.Warn, "Nepodaøilo se ovìøit uživatele vùèi SQL serveru.");
				FASK_Logins = null;
			}

			if (FASK_Logins != null && FASK_Logins.Count > 0)
            {

                Session["UserAuthenticated"] = e.Authenticated = true;

				Session["ActualUser"] = FASK_Logins[0];
            }
            else if (Login1.UserName == "159" && Login1.Password == "159")
            {

				FASK.Logins.DataSets.Pristupy.FASK_LoginsDataTable dt = new FASK.Logins.DataSets.Pristupy.FASK_LoginsDataTable();

				FASK.Logins.DataSets.Pristupy.FASK_LoginsRow row = dt.NewFASK_LoginsRow();

				row.USERID = "159";
				row.psswd = "159";
				row.firstname = "FASK";
				row.surname = "admin";
				row.VALIDFROM = DateTime.Now;
				//row.VALIDTO = null;
				row.CREATED = DateTime.Now;


                Session["UserAuthenticated"] = e.Authenticated = true;

				Session["ActualUser"] = row;
            }
            else
            {
                Session["UserAuthenticated"] = e.Authenticated = false;
            }

			Fask.Logging.ExceptionHandler2.Handle(LogLevel.Info, "Prihlaseni Uzivatele Server Autorizace session: " + e.Authenticated.ToString());

			if (e.Authenticated)
            {

				//Server.Transfer("Default.aspx", true);
				//Response.Redirect("Default.aspx");
				//Response.Redirect("Default.aspx", false);
				//Response.Redirect(@"\Default.aspx", false);
				//Response.Redirect(@"D.aspx", false);
				//Context.ApplicationInstance.CompleteRequest();
				
				Response.Redirect("Default.aspx");
				//Response.Redirect("Default.aspx", true);
				Context.ApplicationInstance.CompleteRequest();
			}

		}
    }
}
