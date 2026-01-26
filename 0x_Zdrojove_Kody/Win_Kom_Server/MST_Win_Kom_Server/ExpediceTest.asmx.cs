using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Fask.Logging;
using System.Reflection;

namespace Fask.MST_W_Server
{
    /// <summary>
	/// Služba přenosu dat ExpediceTest
    /// </summary>
	[WebService(Namespace = "http://ExpediceTest.fask.cz/", Description = "Služba přenosu dat ExpediceTest")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ExpediceTest : System.Web.Services.WebService
    {

		#region Lokalne promenne

		private Fask.Server.Interfaces.IWebModule provider = null;
		
		#endregion

		#region Konstruktor

		/// <summary>
		/// Konstruktor
		/// </summary>
		public ExpediceTest()
		{
			// Inicializuje objektove rozhrani ...
			try
			{
                Konfigurace.Classes.Globals_Konfig_WebConfig.LoadConfiguration();
                string providerAssemblyPath = Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Providers[0].Provider_Expedice;
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
								if (typeof(Fask.Server.Interfaces.IWebModule).IsAssignableFrom(t))
								{
									provider = (Fask.Server.Interfaces.IWebModule)providerAssemlby.CreateInstance(t.FullName);
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

		#region webmetody

        /// <summary>
		/// Test tisk Expedice baleni
        /// </summary>
        /// <param name="GUIDstring">Guid ID Hlavičky</param>
        /// <param name="NMBRPAL"> číslo palety</param>
        /// <param name="printerName"> nazev tiskarny</param>
        /// <param name="sablonaHlavicka"> šablona Hlavička</param>
        /// <param name="sablonaRadek">šablona řadek</param>
        /// <param name="sablonaPaticka">šablona patička</param>
        /// <returns>True-OK, False- chyba</returns>
		[WebMethod(Description = "Test tisk Expedice baleni")]
        public bool Expedice_Baleni_Tisk_TEST_Full(
            string GUIDstring,
            string NMBRPAL,
            string printerName,
            string sablonaHlavicka,
            string sablonaRadek,
            string sablonaPaticka,
            string PocetVytisku)
        {
            try
            {

                //GUIDstring = "f39f8789-3afd-4174-84cf-e4482ca4e4fc";
                //printerName = "ZDesigner ZT230-200dpi ZPL";
                //sablonaHlavicka = "testHead1_PAL.zpl";
                //sablonaRadek = "testRow1_PAL.zpl";
                //sablonaPaticka = "testFoot1_PAL.zpl";

                //NMBRPAL = "00006543210000000180";

                Fask.MST_W_Server.Expedice.TiskSablona ts = new Fask.MST_W_Server.Expedice.TiskSablona();

                ts.printerName = printerName;
                ts.sablonaHlavicka = sablonaHlavicka;
                ts.sablonaRadek = sablonaRadek;
                ts.sablonaPaticka = sablonaPaticka;
                if (!string.IsNullOrEmpty(PocetVytisku))
                    ts.pocetVytisku = int.Parse(PocetVytisku);


                // možno v debugu nakopirovat GUID testovaci...
                Guid guidTest = new Guid(GUIDstring);

                Fask.MST_W_Server.Expedice exp = new Expedice();


                exp.Expedice_Baleni_Tisk(guidTest, NMBRPAL, ts);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }



        /// <summary>
        /// Test s predvyplnenima hodnotama v kodu(defaultne hodnoy pro FASK)
        /// </summary>
        /// <param name="GUIDstring"> jedna sa o GUID ID Hlavičky</param>
        /// <param name="NMBRPAL"> čislo palety</param>
		/// <returns>True-OK, False- chyba</returns>
		[WebMethod(Description = "Test s predvyplnenima hodnotama v kodu(defaultne hodnoy pro FASK)")]
        public bool Expedice_Baleni_Tisk_TEST(
            string GUIDstring,
            string NMBRPAL)
        {
            try
            {

                // možno v debugu nakopirovat GUID testovaci...
                Guid guidTest = new Guid(GUIDstring);
                Fask.MST_W_Server.Expedice exp = new Expedice();
                exp.Expedice_Baleni_Tisk(guidTest, NMBRPAL, null);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
		}

		#endregion


	}
}
