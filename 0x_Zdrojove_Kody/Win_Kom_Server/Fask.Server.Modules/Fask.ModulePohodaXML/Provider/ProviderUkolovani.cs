using Fask.DataSets;
using Fask.Server.Interfaces.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Fask.SQL
{
	public partial class Provider : Fask.Server.Interfaces.Ukolovani.IUkolovani
	{

		private string TABLE_CZ_UKOL = "CZ_UKOL";
		private string TABLE_CZ_UKOL_UZIV = "CZ_UKOL_UZIV";
		private string TABLE_CZ_UKOL_STATE = "CZ_UKOL_STATE";
		private string TABLE_CZ_UKOL_ServiceMan = "CZ_UKOL_ServiceMan";

		public Ukoly GetServiceMan()
		{
			try
			{
				Ukoly ds = new Ukoly();

				using (var con = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
				{
					using (var com = con.CreateCommand())
					{
						com.CommandText = "SELECT * FROM " + TABLE_CZ_UKOL_ServiceMan;

						using (var ada = new System.Data.SqlClient.SqlDataAdapter(com))
						{
							ada.Fill(ds.CZ_UKOL_ServiceMan);
						}
					}
				}

				return ds;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public Ukoly GetUkoly(Terminal terminal, User user)
		{
			try
			{
				Ukoly ds = new Ukoly();

				using (var con = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
				{
					using (var com = con.CreateCommand())
					{
						com.CommandText = "SELECT * FROM " + TABLE_CZ_UKOL + " WITH (NOLOCK)";

						using (var ada = new System.Data.SqlClient.SqlDataAdapter(com))
						{
							ada.Fill(ds.CZ_UKOL);
						}
					}
				}

				using (var con = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
				{
					using (var com = con.CreateCommand())
					{
						com.CommandText = "SELECT * FROM " + TABLE_CZ_UKOL_UZIV + " WITH (NOLOCK)";

						using (var ada = new System.Data.SqlClient.SqlDataAdapter(com))
						{
							ada.Fill(ds.CZ_UKOL_UZIV);
						}
					}
				}

				using (var con = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
				{
					using (var com = con.CreateCommand())
					{
						com.CommandText = "SELECT * FROM " + TABLE_CZ_UKOL_STATE + " WITH (NOLOCK)";

						using (var ada = new System.Data.SqlClient.SqlDataAdapter(com))
						{
							ada.Fill(ds.CZ_UKOL_STATE);
						}
					}
				}

				foreach (Fask.DataSets.Ukoly.CZ_UKOLRow urow in ds.CZ_UKOL)
				{
					urow.SetAdded();
				}
				foreach (Fask.DataSets.Ukoly.CZ_UKOL_UZIVRow uurow in ds.CZ_UKOL_UZIV)
				{
					uurow.SetAdded();
				}
				foreach (Fask.DataSets.Ukoly.CZ_UKOL_STATERow srow in ds.CZ_UKOL_STATE)
				{
					srow.SetAdded();
				}

				return ds;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public bool SendEmailToServiceMan(User User, byte TerminalID, string MachineID, string To, string Subject, string poznamka)
		{
			Globals_V1.LoadConfiguration();

			// přihlášení se k smtp od google gmail
			var client = new SmtpClient(Globals_V1.Konfigurace.Ukolovani[0].ServiceMan_Email_SMTP, Globals_V1.Konfigurace.Ukolovani[0].ServiceMan_Email_port)
			{
				Credentials = new NetworkCredential(
					Globals_V1.Konfigurace.Ukolovani[0].ServiceMan_Email_UserName,
					Globals_V1.Konfigurace.Ukolovani[0].ServiceMan_Email_Heslo),

				EnableSsl = Globals_V1.Konfigurace.Ukolovani[0].ServiceMan_Email_SSL
			};

			try
			{
				Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info, "ServiceMan : Priprava emailu na odeslani.");


				MailMessage message = new MailMessage(Globals_V1.Konfigurace.Ukolovani[0].ServiceMan_Email_From, To);
				message.Subject = Subject;
				message.IsBodyHtml = Globals_V1.Konfigurace.Ukolovani[0].ServiceMan_Email_IsBodyHtml;

				string msg_body = string.Empty;

				if (User != null)
				{
					msg_body = string.Format("U:{0} {1},T:{2},M:{3},O:{4}", User.firstname, User.secondname, TerminalID, MachineID, Globals_V1.Konfigurace.Ukolovani[0].ServiceMan_Email_Text);
				}

				if (!string.IsNullOrEmpty(poznamka))
					msg_body += ",P:" + poznamka;

				message.Body = msg_body;
				// odeslání emailu (od koho, komu, předmět, zpráva)

				if (!Globals_V1.Konfigurace.Ukolovani[0].ServiceMan_Email_Skip_Certifikaty)
				{
					ServicePointManager.ServerCertificateValidationCallback =
			delegate (object s, X509Certificate certificate,
					 X509Chain chain, SslPolicyErrors sslPolicyErrors)
			{ return true; };
				}

				client.Timeout = Globals_V1.Konfigurace.Ukolovani[0].ServiceMan_Email_Timeout;
				client.Send(message);

				Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info, "ServiceMan :" + "Odeslan email:" + msg_body);

				return true;


			}
			catch (System.Net.Mail.SmtpException exSMTP)
			{
				Logging.ExceptionHandler2.Handle(exSMTP);
				return false;
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				return false;
			}

			//return true;
		}
	}
}
