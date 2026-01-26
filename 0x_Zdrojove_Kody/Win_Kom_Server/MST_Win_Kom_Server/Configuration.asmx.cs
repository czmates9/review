using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Data.Common;
using System.IO;
using System.Text;
using System.Xml;
using Fask.Logging;

using System.Data.SqlClient;

namespace Fask.MST_W_Server
{
    /// <summary>
	/// Služba pøenosu dat Configuration
    /// </summary>
	[WebService(Namespace = "http://Configuration.fask.cz/", Description = "Služba pøenosu dat Configuration")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class Configuration : System.Web.Services.WebService
    {

		#region Konstruktor

		/// <summary>
		/// Konstruktor
		/// </summary>
		public Configuration()
		{

		} 
		#endregion

		#region WebMetody

		/// <summary>
		/// Metoda pro dotaženi uživatelu a XML vystupem
		/// </summary>
		/// <returns>XML vystup dotaženy uživatele</returns>
		[WebMethod(Description = "Metoda pro dotaženi uživatelu a XML vystupem")]
		public string GetPasswords()
		{
            Konfigurace.Classes.Globals_Konfig_WebConfig.LoadConfiguration();

            string selectcommandtext = "SELECT login, passwd, id FROM CZMSTPWD";

			SqlDataAdapter xda = new SqlDataAdapter(selectcommandtext, new SqlConnection(Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.ConnectionString[0].FASKDB));

			Fask.DataSets.Passwd passwd = new Fask.DataSets.Passwd();
			xda.Fill(passwd, passwd.CZMSTPWD.TableName);

			StringBuilder sbxml = new StringBuilder();

			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;
			settings.OmitXmlDeclaration = true;
			XmlWriter writer = XmlWriter.Create(sbxml, settings);

			writer.WriteStartElement("passfile");
			foreach (Fask.DataSets.Passwd.CZMSTPWDRow pwdr in passwd.CZMSTPWD)
			{
				writer.WriteStartElement("user");

				writer.WriteStartElement("login");
				writer.WriteString(pwdr.login.Trim());
				writer.WriteEndElement();

				writer.WriteStartElement("password");
				writer.WriteString(pwdr.passwd.Trim());
				writer.WriteEndElement();

				writer.WriteStartElement("id");
				writer.WriteValue(pwdr.id);
				writer.WriteEndElement();

				writer.WriteEndElement();
			}
			writer.WriteEndElement();

			writer.Close();

			return sbxml.ToString();
		}

		/// <summary>
		/// Metoda pro dotažení palet z souboru, podle ID Terminalu
		/// </summary>
		/// <param name="idterminal">ID Termianlu</param>
		/// <returns>XML format Palety</returns>
		[WebMethod(Description = "Metoda pro dotažení palet z souboru, podle ID Terminalu")]
		public string GetTypyPalet(byte idterminal)
		{
			GetTypyPaletFile(idterminal);

			StreamReader sr = null;
			try
			{
				string typypaletpath = Path.Combine(Fask.MyPath.Path.ConfigDirectory, "TypyPalet" + idterminal.ToString() + ".xml");
				sr = new StreamReader(typypaletpath);
				string typypalet = sr.ReadToEnd();
				sr.Close();
				sr = null;
				return typypalet;
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				throw ex;
			}
			finally
			{
				if (sr != null)
				{
					sr.Close();
					sr = null;
				}
			}
		}

		/// <summary>
		/// Metoda pro dotažení konfigurací pro scanner
		/// </summary>
		/// <returns>XML format konfigurace pro scanner</returns>
		[WebMethod(Description = "Metoda pro dotažení konfigurací pro scanner")]
		public string GetScannerConfig()
		{
			StreamReader sr = null;
			try
			{
				string scannerconfigpath = Path.Combine(Fask.MyPath.Path.ConfigDirectory, "Scanner.xml");
				sr = new StreamReader(scannerconfigpath);
				string scannerconfig = sr.ReadToEnd();
				sr.Close();
				sr = null;
				return scannerconfig;
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				throw ex;
			}
			finally
			{
				if (sr != null)
				{
					sr.Close();
					sr = null;
				}
			}
		}

		/// <summary>
		/// Metoda pro uloženi scanner konfigurace na server
		/// </summary>
		/// <param name="scannerconfig">Konfigurace v XML formatu jak string</param>
		/// <returns></returns>
		[WebMethod(Description = "Metoda pro uloženi scanner konfigurace na server")]
		public bool SetScannerConfig(string scannerconfig)
		{
			StreamWriter sw = null;
			try
			{
				string scannerconfigpath = Path.Combine(Fask.MyPath.Path.ConfigDirectory, "Scanner.xml");
				sw = new StreamWriter(scannerconfigpath, false);
				sw.Write(scannerconfig);
				sw.Close();
				sw = null;
				return true;
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				throw ex;
			}
			finally
			{
				if (sw != null)
				{
					sw.Close();
					sw = null;
				}
			}
		}

		/// <summary>
		/// Metoda pro naètení konfigurací modulu na Terminalu
		/// </summary>
		/// <returns>Konfigurace terinalu v XML formatu</returns>
		[WebMethod(Description = "Metoda pro naètení konfigurací modulu na Terminalu")]
		public string GetModulesConfig()
		{
			StreamReader sr = null;
			try
			{
				string modulesconfigpath = Path.Combine(Fask.MyPath.Path.ConfigDirectory, "ConfigModules.xml");
				sr = new StreamReader(modulesconfigpath);
				string modulesconfig = sr.ReadToEnd();
				sr.Close();
				sr = null;
				return modulesconfig;
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				throw ex;
			}
			finally
			{
				if (sr != null)
				{
					sr.Close();
					sr = null;
				}
			}
		}

		/// <summary>
		/// Metoda pro vraceni licence z serveru
		/// </summary>
		/// <returns>Objekt License</returns>
		[WebMethod(Description = "Metoda pro vraceni licence z serveru")]
		public Licensing.License GetLicenceInfo()
		{
			Licensing.License lic = Application[Constants.Common.license] as Licensing.License;

            int? months = Application[Constants.Common.Show_Info_On_Terminal_Before_Expiration] as int?;

			if (!String.IsNullOrEmpty(lic.expiration))
			{
				DateTime vyprseniLicence = DateTime.ParseExact(lic.expiration, "dd.MM.yyyy", null);
				if (vyprseniLicence <= DateTime.Now.AddMonths((int)months))
					lic.showInfoExpirationInTerminal = true;
				else
					lic.showInfoExpirationInTerminal = false;
			}

			return lic;
		}

		/// <summary>
		/// Medota na dotaženi informaci z CZMSTCFG
		/// </summary>
		/// <returns>Dataset dsCZMSTCFG, naplnen hodnotama</returns>
		[WebMethod(Description = "Medota na dotaženi informaci z CZMSTCFG")]
		public Fask.DataSets.dsCZMSTCFG GetCZMSTCFG()
		{
			try
			{
                Konfigurace.Classes.Globals_Konfig_WebConfig.LoadConfiguration();
                SqlDataAdapter xda = new SqlDataAdapter("Select * from CZMSTCFG", new SqlConnection(Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.ConnectionString[0].FASKDB));

				Fask.DataSets.dsCZMSTCFG czmstcfg = new Fask.DataSets.dsCZMSTCFG();
				xda.Fill(czmstcfg, czmstcfg.CZMSTCFG.TableName);

				return czmstcfg;

			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				return null;
			}
		}

		#endregion


		#region Privatne metody

		/// <summary>
		/// Metoda pro dotaženi palet z SQL serveru a uloženi do souboru
		/// </summary>
		/// <param name="idterminal">ID Terminalu</param>
		/// <returns>True- OK, False-chyba</returns>
		private bool GetTypyPaletFile(byte idterminal)
		{
            Konfigurace.Classes.Globals_Konfig_WebConfig.LoadConfiguration();
            bool succed = false;
			string typypaletpath = Path.Combine(Fask.MyPath.Path.ConfigDirectory, "TypyPalet" + idterminal.ToString() + ".xml");

			SqlDataAdapter xda = new SqlDataAdapter("Select id, name from czmst_palety", new SqlConnection(Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.ConnectionString[0].FASKDB));

			Fask.MST_W_Server.DataSets.TypyPalet paleta = new Fask.MST_W_Server.DataSets.TypyPalet();
			xda.Fill(paleta, paleta.Palety.TableName);
			paleta.WriteXml(typypaletpath, XmlWriteMode.IgnoreSchema);

			StringBuilder sbxml = new StringBuilder();

			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;
			settings.OmitXmlDeclaration = true;
			XmlWriter writer = XmlWriter.Create(typypaletpath, settings);

			writer.WriteStartElement("TypyPalet", "http://Configuration.fask.cz/TypyPalet.xsd");
			foreach (Fask.MST_W_Server.DataSets.TypyPalet.PaletyRow plt in paleta.Palety)
			{
				writer.WriteStartElement("Palety");

				writer.WriteStartElement("ID");
				writer.WriteString(plt.ID.Trim());
				writer.WriteEndElement();

				writer.WriteStartElement("Name");
				writer.WriteValue(plt.Name.Trim());
				writer.WriteEndElement();

				writer.WriteEndElement();
			}
			writer.WriteEndElement();

			writer.Close();
			succed = true;
			return succed;
		}

		#endregion






    }
}
