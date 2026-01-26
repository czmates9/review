using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Reflection;
using Fask.Logging;
using Fask.Server.Interfaces.Classes;

namespace Fask.MST_W_Server
{
    /// <summary>
	/// Služba přenosu dat lokačního mechanismu.
    /// </summary>
    [WebService(Namespace = "http://fask.cz/", Description = "Služba přenosu dat lokačního mechanismu", Name = "LokaceService")]
    public class Lokace : System.Web.Services.WebService
    {

		#region Lokalne promenne

		Fask.Server.Interfaces.Lokace.ILokace provider = null;
		
		#endregion

		#region Konstruktor

		/// <summary>
		/// Konstruktor
		/// </summary>
		public Lokace()
		{
			try
			{

                Konfigurace.Classes.Globals_Konfig_WebConfig.LoadConfiguration();
                string providerAssemblyPath = Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Providers[0].Provider_Lokace;
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
								if (typeof(Fask.Server.Interfaces.Lokace.ILokace).IsAssignableFrom(t))
								{
									provider = (Fask.Server.Interfaces.Lokace.ILokace)providerAssemlby.CreateInstance(t.FullName);
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

		#region WebMetody

		/// <summary>
		/// Metoda sloúžící pro pridani noveho zaznamu do lokacniho mechanismu.
		/// </summary>
		/// <param name="TableRow">Radek pro pridani</param>
		/// <returns>StatusLokace - objekt nese info o stavu</returns>
		[WebMethod(Description = "Metoda sloúžící pro pridani noveho zaznamu do lokacniho mechanismu.")]
		public StatusLokace AddRecord(Fask.Server.Interfaces.Lokace.LokacePohyb TableRow)
		{
			StatusLokace sl = new StatusLokace();
			try
			{
				Fask.Logging.ExceptionHandler2.Handle(LogLevel.Location, "Operation:add,Mode:online,Modul:" + TableRow.POHYB_TYPE.ToString() + ",Function:" + this.ToString() + ".AddRecord");

				if (provider != null && provider is Fask.Server.Interfaces.Lokace.ILokace)
				{
					return provider.Lokace_AddRecord(TableRow);
				}
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				sl.State = States.ERROR;
				sl.ErrorMessage = ex.Message;

				return sl;
			}

			string msg = "Provider v Lokace.asmx > 'AddRecord(Fask.Server.Interfaces.Lokace.LokacePohyb TableRow)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
			throw new Exception(msg);

		} 

        /// <summary>
		/// Metoda sloužící k pohybu v Lokacnim mechanizmu
        /// pokud je Record.POHYB_TYPE == TypeOfRecord.D, dojde k vydani materialu ze zdrojove lokace a prijmu materialu na cilovou lokaci.
        /// pokud je Record.POHYB_TYPE == TypeOfRecord.V, dojde k vydani materialu ze zdrojove lokace
        /// pokud je Record.POHYB_TYPE == TypeOfRecord.P, dojde k prijmu materialu na zdrojovou lokaci
        /// </summary>
		/// <param name="Data">Radek pro pridani</param>
		/// <returns>StatusLokace - objekt nese info o stavu</returns>
		[WebMethod(Description = "Metoda sloužící k pohybu v Lokacnim mechanizmu")]
        public StatusLokace MoveItem(Fask.Server.Interfaces.Lokace.LokacePohyb Data)
        {
            Fask.Logging.ExceptionHandler2.Handle(LogLevel.Location,"Operation:move,Mode:online,Modul:" + Data.POHYB_TYPE.ToString() + ",Function:" + this.ToString() + ".MoveItem");

            StatusLokace sl = new StatusLokace();
            try
            {
				if (provider != null && provider is Fask.Server.Interfaces.Lokace.ILokace)
                {
                    return provider.Lokace_MoveItem(Data);
                }
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                sl.State = States.ERROR;
                sl.ErrorMessage = ex.Message;

				return sl;
			}

			string msg = "Provider v Lokace.asmx > 'MoveItem(Fask.Server.Interfaces.Lokace.LokacePohyb Data)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
			throw new Exception(msg);
 
        }

		/// <summary>
		/// Metoda sloužící pro odstraneni (resp. odecteni opacnym pohybem) zaznamu podle Guid (pokud odecteni jiz neprobehlo)
		/// </summary>
		/// <param name="Guid">Guid, podle ktereho se urci zaznam</param>
		/// <param name="ModulName">Nazev modulu, ktery vyvolava operaci</param>
		/// <returns>StatusLocation</returns>
		[WebMethod(Description = "Metoda sloužící pro odstraneni (resp. odecteni opacnym pohybem) zaznamu podle Guid (pokud odecteni jiz neprobehlo)", MessageName = "DeleteRecordByGuid(ModulName)")]
		public StatusLokace DeleteRecordByGuid(Guid Guid, Fask.Server.Interfaces.Lokace.ModulName ModulName)
		{
			StatusLokace sl = new StatusLokace();
			try
			{
				Fask.Logging.ExceptionHandler2.Handle(LogLevel.Location, "Operation:del,Mode:online,Modul:" + ModulName.ToString() + ",Function:" + this.ToString() + ".DeleteRecordByGuid");

				if (provider != null && provider is Fask.Server.Interfaces.Lokace.ILokace)
				{
					return provider.Lokace_DeleteRecord(Guid, ModulName);
				}
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				sl.State = States.ERROR;
				sl.ErrorMessage = ex.Message;

				return sl;
			}

			string msg = "Provider v Lokace.asmx > 'DeleteRecordByGuid(Guid Guid, Fask.Server.Interfaces.Lokace.ModulName ModulName)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
			throw new Exception(msg);
		}

		/// <summary>
		/// Metoda sloužící pro odstraneni (resp. odecteni opacnym pohybem) zaznamu podle Guid (pokud odecteni jiz neprobehlo)
		/// </summary>
		/// <param name="Guid">Guid, podle ktereho se urci zaznam</param>
		/// <param name="recordType">Nazev modulu, ktery vyvolava operaci</param>
		/// <returns>StatusLocation - objekt nese informace o stavu</returns>
		[WebMethod(Description = "Metoda sloužící pro odstraneni (resp. odecteni opacnym pohybem) zaznamu podle Guid (pokud odecteni jiz neprobehlo)", MessageName = "DeleteRecordByGuid(TypeOfRecord)")]
        public StatusLokace DeleteRecordByGuid(Guid Guid, Fask.Server.Interfaces.Lokace.TypeOfRecord recordType)
        {
            StatusLokace sl = new StatusLokace();
            Fask.Server.Interfaces.Lokace.ModulName modulName;

            try
            {
                if (recordType == Fask.Server.Interfaces.Lokace.TypeOfRecord.D)
                    modulName = Fask.Server.Interfaces.Lokace.ModulName.DEFREGMENTACE;
                else if (recordType == Fask.Server.Interfaces.Lokace.TypeOfRecord.V)
                    modulName = Fask.Server.Interfaces.Lokace.ModulName.VYDEJ;
                else if (recordType == Fask.Server.Interfaces.Lokace.TypeOfRecord.P)
                    modulName = Fask.Server.Interfaces.Lokace.ModulName.PRIJEM;
                else
                    throw new Exception("Neznámá typ pohybu '" + recordType.ToString() + "', DeleteRecordByGuid()");

				Fask.Logging.ExceptionHandler2.Handle(LogLevel.Location, "Operation:del,Mode:online,Modul:" + modulName.ToString() + ",Function:" + this.ToString() + ".DeleteRecordByGuid");

				if (provider != null && provider is Fask.Server.Interfaces.Lokace.ILokace)
                {
                    sl = provider.Lokace_DeleteRecord(Guid, modulName);
                }
                else
                    throw new Exception("XDatabase neni implementovana.");
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                sl.State = States.ERROR;
                sl.ErrorMessage = ex.Message;

				return sl;
			}

			string msg = "Provider v Lokace.asmx > 'DeleteRecordByGuid(Guid Guid, Fask.Server.Interfaces.Lokace.ModulName ModulName)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
			throw new Exception(msg);
        }

		/// <summary>
		/// Metoda slouzi k zobrazeni prijmovych lokaci.
		/// </summary>
		/// <param name="skl_id">ID Skladu</param>
		/// <returns>Seznam prijmovych lokaci k danemu skladu</returns>
		[WebMethod(Description = "Metoda slouzi k zobrazeni prijmovych lokaci.")]
        public Fask.Server.Interfaces.DataSets.Location ShowReceiveLocations(string skl_id)
        {
            Fask.Server.Interfaces.DataSets.Location location = new Fask.Server.Interfaces.DataSets.Location();

            try
            {
				if (provider != null && provider is Fask.Server.Interfaces.Lokace.ILokace)
                {
                    return provider.Lokace_ShowReceiveLocations(skl_id);
                }
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }

			string msg = "Provider v Lokace.asmx > 'ShowReceiveLocations(string skl_id)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
			throw new Exception(msg);
        }

 
		[WebMethod(Description = "Metoda která vraci seznam materialu ve skladu.")]
		public Fask.Server.Interfaces.DataSets.Location ShowMaterial_TEST(string itemnmbr, string serltnum, string skl_id)
		{
			return ShowMaterial(itemnmbr, serltnum, skl_id,null, false );
		}
 
 
		/// <summary>
		/// Metoda která vraci seznam materialu ve skladu.
		/// </summary>
		/// <param name="itemnmbr">ID položky</param>
		/// <param name="serltnum">seriove čislo/ šarže</param>
		/// <param name="skl_id">ID Skladu</param>
		/// <param name="NumberOfRecords">číslo zaznamu</param>
		/// <param name="ShowEmpty">Dotaženi prazdnych?</param>
		/// <returns>Dataset Location - naplnen datama</returns>
		[WebMethod(Description = "Metoda která vraci seznam materialu ve skladu.")]
        public Fask.Server.Interfaces.DataSets.Location ShowMaterial(string itemnmbr, string serltnum, string skl_id, uint? NumberOfRecords, bool ShowEmpty)
        {
            Fask.Server.Interfaces.DataSets.Location location = new Fask.Server.Interfaces.DataSets.Location();

            try
            {
				if (provider != null && provider is Fask.Server.Interfaces.Lokace.ILokace)
                {
                    return provider.Lokace_ShowMaterial(itemnmbr, serltnum, skl_id, NumberOfRecords, ShowEmpty);
                }
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }

			string msg = "Provider v Lokace.asmx > 'ShowMaterial(string itemnmbr, string serltnum, string skl_id, uint? NumberOfRecords, bool ShowEmpty)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
			throw new Exception(msg);
        }

		/// <summary>
		/// Metoda pro zjisteni, zdali varianta lokace existuje.
		/// </summary>
		/// <param name="idterminal">ID Terminalu</param>
		/// <param name="userid">ID uživatele</param>
		/// <param name="itemnmbr">ID položky</param>
		/// <param name="skl_id">ID Skladu</param>
		/// <param name="locncode">Lokace</param>
		/// <returns>True - OK, False, chyba</returns>
		[WebMethod(Description = "Metoda pro zjisteni, zdali varianta lokace existuje.")]
        public bool VariantySortimentExists(byte idterminal, int userid, string itemnmbr, string skl_id, string locncode)
        {
            try
            {
				if (provider != null && provider is Fask.Server.Interfaces.Lokace.ILokace)
                {
                    return provider.Lokace_VariantySortimentExists(itemnmbr, skl_id, locncode);
                }
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }

			string msg = "Provider v Lokace.asmx > 'VariantySortimentExists(byte idterminal, int userid, string itemnmbr, string skl_id, string locncode)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
			throw new Exception(msg);
        }

		/// <summary>
		/// Metoda pro zjisteni, jestli existuje varianta lokace pro vychozi pozici (IS_DEFAULT=1)
		/// </summary>
		/// <param name="idterminal">ID Terminalu</param>
		/// <param name="userid">ID uživatele</param>
		/// <param name="itemnmbr">ID položky</param>
		/// <param name="skl_id">ID Skladu</param>
		/// <param name="locncode">Lokace</param>
		/// <returns>True- OK, False - chyba</returns>
		[WebMethod(Description = "Metoda pro zjisteni, jestli existuje varianta lokace pro vychozi pozici (IS_DEFAULT=1)")]
        public bool VariantySortimentExistsDefault(byte idterminal, int userid, string itemnmbr, string skl_id, string locncode)
        {
            try
            {
				if (provider != null && provider is Fask.Server.Interfaces.Lokace.ILokace)
                {
                    return provider.Lokace_VariantySortimentExistsDefault(itemnmbr, skl_id, locncode);
                }

            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }

			string msg = "Provider v Lokace.asmx > 'VariantySortimentExistsDefault(byte idterminal, int userid, string itemnmbr, string skl_id, string locncode)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
			throw new Exception(msg);
        }

		/// <summary>
		/// Metoda pro nastaveni varianty lokace.
		/// </summary>
		/// <param name="idterminal">ID Terminalu</param>
		/// <param name="userid">ID uzivatele</param>
		/// <param name="itemnmbr">ID polozky</param>
		/// <param name="skl_id">ID Skladu</param>
		/// <param name="locncode">Lokace</param>
		/// <param name="type">Typ</param>
		/// <returns>True - OK, False - chyba</returns>
		[WebMethod(Description = "Metoda pro nastaveni varianty lokace.")]
        public bool VariantySortimentNastav(byte idterminal, int userid, string itemnmbr, string skl_id, string locncode, string type)
        {
            try
            {
				if (provider != null && provider is Fask.Server.Interfaces.Lokace.ILokace)
                {
                    return provider.Lokace_VariantySortimentNastav(idterminal, userid, itemnmbr, skl_id, locncode, type);
                }

            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }

			string msg = "Provider v Lokace.asmx > 'VariantySortimentNastav(byte idterminal, int userid, string itemnmbr, string skl_id, string locncode, string type)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
			throw new Exception(msg);
		}

		#endregion
	}
}
