using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Reflection;
using Fask.Logging;

namespace Fask.MST_W_Server
{
    /// <summary>
	/// Služba přenosu dat RFID
    /// </summary>
	[WebService(Namespace = "http://RFID.fask.cz/", Description = "Služba přenosu dat RFID")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class RFID : System.Web.Services.WebService
    {

		#region loalne promenne

		// obsahuje metody pro praci s daty pro rfid ... 
		// 1) generovani serioveho cisla pro itemnmbr, skl_id
		// 2) ulozeni zaznamu pro dane prirazeni RFID (guid je jedinecny zaznam)
		// 3) vraceni informace o zaznamu

		Fask.Server.Interfaces.RFID.IRFID provider = null; 

		#endregion

		#region Konstruktor

		/// <summary>
		/// Konstruktor
		/// </summary>
		public RFID()
		{
			try
			{
                Konfigurace.Classes.Globals_Konfig_WebConfig.LoadConfiguration();
                string providerAssemblyPath = Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Providers[0].Provider_RFID;
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
								//t.IsAssignableFrom(typeof(Fask.Server.Interfaces.Vydej.IVydej);
								if (typeof(Fask.Server.Interfaces.RFID.IRFID).IsAssignableFrom(t))
								{
									provider = (Fask.Server.Interfaces.RFID.IRFID)providerAssemlby.CreateInstance(t.FullName);
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
						//return config;
					}


				}
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
			}
		} 

		#endregion

		#region WebMetody

		/// <summary>
		/// Metoda která vraci dalsi seriove(poradove cislo) pro polozku
		/// </summary>
		/// <param name="itemnmbr">cislo polozky</param>
		/// <param name="skl_id">cislo skladu</param>
		/// <param name="countSerials">pocet seriovych cisel</param>
		/// <returns>List poradovych čisel</returns>
		[WebMethod(Description = "Metoda která vraci dalsi seriove(poradove cislo) pro polozku")]
		public List<int> GetNextSerial(string itemnmbr, string skl_id, int countSerials)
		{
			try
			{
				List<int> serialslist = null;

				if (provider != null)
				{
					serialslist = provider.RFID_GetNextSerial(itemnmbr, skl_id, countSerials);
				}
				else
				{
					throw new NotImplementedException("RFID");
				}

				return serialslist;
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				throw ex;
			}
		}

		#endregion

    }
}
