using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Configuration;

using System.Diagnostics;

using System.Data.Common;
using System.IO;
using System.Web.Configuration;
using System.Security.Cryptography;
using Fask.MST_W_Server.Routines;
using Fask.Server.Interfaces.Classes;
using System.Reflection;
using Fask.Logging;
using Fask.MST_W_Server.Constants;
using Fask.MST_W_Server.SQLite_Classes;

namespace Fask.MST_W_Server
{
    /// <summary>
    /// Služba přenosu dat inventury2
    /// </summary>
    [WebService(Namespace = "http://fask.cz/", Description = "Služba přenosu dat inventury2", Name = "Inventura2Service")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Inventura2 : System.Web.Services.WebService
    {
        public enum ProcessInventuraState
        {
            Uvolnit,
            Zpracovat
        }

        #region lokalne promenne

        private Fask.Server.Interfaces.Inventura2.IInventura2 provider = null;
        const string Inventura2DBFileExtension = @".in2";

        #endregion

        #region Konstruktor

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Inventura2()
        {


            try
            {
                Konfigurace.Classes.Globals_Konfig_WebConfig.LoadConfiguration();
                string providerAssemblyPath = Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Providers[0].Provider_Inventura2;
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
                                if (typeof(Fask.Server.Interfaces.Inventura2.IInventura2).IsAssignableFrom(t))
                                {
                                    provider = (Fask.Server.Interfaces.Inventura2.IInventura2)providerAssemlby.CreateInstance(t.FullName);
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
        #endregion

        #region WebMetody

        /// <summary>
        /// Metoda která vraci seznam dostupnych inventurnich predloh
        /// </summary>
        /// <param name="terminalID">Cislo terminalu</param>
        /// <returns>Dataset Inventury2</returns>
        [WebMethod(Description = "Metoda která vraci seznam dostupnych inventurnich predloh")]
        public Fask.DataSets.Inventury2 GetInventury(byte terminalID)
        {
            if (provider != null)
            {
                try
                {
                    Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();

                    terminal.ID = terminalID;

                    return provider.Inventura2_GetInventury(
                        terminal
                        );
                }
                catch (Exception ex)
                {
					Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                    throw ex;
                }
            }

            string msg = "Provider v Inventura.asmx > 'GetInventury(byte terminalID)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, msg);
            throw new Exception(msg);

        }

        /// <summary>
        /// Metoda která vytvori predlohu db
        /// </summary>
        /// <param name="cislodavky">číslo dávky</param>
        /// <param name="idterminal">ID Terminalu</param>
        /// <returns>True-OK, False- chyba</returns>
        [WebMethod(Description = "Metoda která vytvori predlohu db")]
        public bool PrepareDB(string cislodavky, byte idterminal)
        {


			Fask.SQLiteDBs.DataSets.Inventura2 inventura2sqlce = null;

            try
            {

                string dstFile = Path.Combine(Fask.MyPath.Path.SQLiteDBsDirectory, idterminal.ToString() + Common.Backslash + cislodavky.Trim() + Inventura2DBFileExtension);
				string dstFilezip = dstFile + Common.ZIP; 

                //vytvorim
                if (!Directory.Exists(Path.GetDirectoryName(dstFile)))
                    Directory.CreateDirectory(Path.GetDirectoryName(dstFile));

				SQLite_Helper helper = new SQLite_Helper();
				if (!helper.SQLite_CreateFile(dstFile, Common.Inventura2))
				{
					throw new Exception("Nastala chyba pri tvorbe SQLite souboru pro " + Common.Inventura2);
				}

                Fask.DataSets.Inventura2 inventura2 = GetInventura2(cislodavky, idterminal);
				inventura2sqlce = new Fask.SQLiteDBs.DataSets.Inventura2();


                try
                {


                    Konfigurace.Classes.Globals_Konfig_Agendy.LoadConfiguration();
                    inventura2sqlce.Parametry.ImportRow(Konfigurace.Classes.Globals_Konfig_Agendy.Konfigurace.Inventura2Parametry[0]);

                    inventura2sqlce.AcceptChanges();
                    for (int i = 0; i < inventura2sqlce.Parametry.Rows.Count; i++)
                    {
                        inventura2sqlce.Parametry[i].SetAdded();
                    }

                }
                catch (Exception ex)
                {
					Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                    //Exceptions.CustomSoapException("Inventura2 params read", ex.Message, "PrepareDB");
                }

                #region Presun do struktur pro sqlce

                for (int j = 0; j < inventura2.Tables.Count; j++)
                {
                    string tablename = inventura2.Tables[j].TableName;
                    inventura2sqlce.Tables[tablename].BeginLoadData();
                    for (int i = 0; i < inventura2.Tables[j].Rows.Count; i++)
                    {
                        //postupne pridavam radky
                        //inventura2.Tables[j].Rows[i].SetAdded();
                        DataRow dr = inventura2sqlce.Tables[tablename].LoadDataRow(inventura2.Tables[j].Rows[i].ItemArray, false);

                        //dr.SetAdded(); -- po load jiz je added ...
                    }
                    inventura2sqlce.Tables[tablename].EndLoadData();
                }
                #endregion


				//SQLiteDBs.DataSets.Inventura2TableAdapters.TableAdapterManager ta_sce_manager = new Fask.MST_W_Server.SQLiteDBs.DataSets.Inventura2TableAdapters.TableAdapterManager();
    //            //data sety, ty naplni db tabulkama co jsem naplnil a to majetek a parametry
				//SQLiteDBs.DataSets.Inventura2TableAdapters.MAJETEKTableAdapter ta_sce_majetek = new Fask.MST_W_Server.SQLiteDBs.DataSets.Inventura2TableAdapters.MAJETEKTableAdapter();
				//SQLiteDBs.DataSets.Inventura2TableAdapters.ParametryTableAdapter ta_sce_parametry = new Fask.MST_W_Server.SQLiteDBs.DataSets.Inventura2TableAdapters.ParametryTableAdapter();
				//SQLiteDBs.DataSets.Inventura2TableAdapters.KANCLTableAdapter ta_sce_kancl = new Fask.MST_W_Server.SQLiteDBs.DataSets.Inventura2TableAdapters.KANCLTableAdapter();
				//SQLiteDBs.DataSets.Inventura2TableAdapters.LOKACETableAdapter ta_sce_lokace = new Fask.MST_W_Server.SQLiteDBs.DataSets.Inventura2TableAdapters.LOKACETableAdapter();
				//SQLiteDBs.DataSets.Inventura2TableAdapters.OSOBYTableAdapter ta_sce_osoby = new Fask.MST_W_Server.SQLiteDBs.DataSets.Inventura2TableAdapters.OSOBYTableAdapter();
				//SQLiteDBs.DataSets.Inventura2TableAdapters.UCSTRTableAdapter ta_sce_strediska = new Fask.MST_W_Server.SQLiteDBs.DataSets.Inventura2TableAdapters.UCSTRTableAdapter();


                //try
                //{
                //ta_sce_majetek.Connection = new System.Data.SQLite.SQLiteConnection( "Data source=" + dstFile);
                //ta_sce_parametry.Connection = ta_sce_majetek.Connection;
                //ta_sce_kancl.Connection = ta_sce_majetek.Connection;
                //ta_sce_lokace.Connection = ta_sce_majetek.Connection;
                //ta_sce_osoby.Connection = ta_sce_majetek.Connection;
                //ta_sce_strediska.Connection = ta_sce_majetek.Connection;

                //ta_sce_manager.MAJETEKTableAdapter = ta_sce_majetek;
                //ta_sce_manager.ParametryTableAdapter = ta_sce_parametry;
                //ta_sce_manager.KANCLTableAdapter = ta_sce_kancl;
                //ta_sce_manager.LOKACETableAdapter = ta_sce_lokace;
                //ta_sce_manager.OSOBYTableAdapter = ta_sce_osoby;
                //ta_sce_manager.UCSTRTableAdapter = ta_sce_strediska;

                //ta_sce_majetek.Connection.Open();
                //var tran = ta_sce_majetek.Connection.BeginTransaction();

                //ta_sce_manager.BackupDataSetBeforeUpdate = false;
                //ta_sce_manager.UpdateOrder = Fask.MST_W_Server.SQLiteDBs.DataSets.Inventura2TableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
                //int x = ta_sce_manager.UpdateAll(inventura2sqlce);
                //tran.Commit();
                //}
				//catch (Exception ex)
				//{
				//	Logging.ExceptionHandler2.Handle(ex);
				//}
				//finally
				//{
				//	if (ta_sce_manager != null)
				//	{
				//		if ((ta_sce_manager.Connection.State & ConnectionState.Open) == ConnectionState.Open)
				//			ta_sce_manager.Connection.Close();
				//		ta_sce_manager.Dispose();
				//	}
				//} 
                
                //SQLite_Classes.SQLite_Helper.Shrink("Data source=" + dstFile);


                try
                {
                    try
                    {
                        using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Inventura2 ConInv2 = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Inventura2(dstFile))
                        {
                            ConInv2.Update_MAJETEK(inventura2sqlce.MAJETEK);    //OK
                            ConInv2.Update_Params(inventura2sqlce.Parametry);   //OK
                            ConInv2.Update_KANCL(inventura2sqlce.KANCL);        //OK
                            ConInv2.Update_LOKACE(inventura2sqlce.LOKACE);      //OK
                            ConInv2.Update_OSOBY(inventura2sqlce.OSOBY);        //OK
                            ConInv2.Update_UCSTR(inventura2sqlce.UCSTR);        //OK

                            ConInv2.Shrink();
                        }
                    }
                    catch (System.Exception ex)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                    }

                }
                catch (System.Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(ex);
                }


                Fask.Compressing.Zip.Compress(dstFile);

                return true;
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, " (Terminál ID:" + idterminal.ToString() + ")");
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw Routines.Exceptions.CustomSoapException(null, ex.Message, "PrepareDB");
            }
        }

        /// <summary>
        /// Metoda blokuje data po prenosu do terminalu, je poslednim krokem pri prenosu dat
        /// 1. PrepareDB
        /// 2. Pomoci Filetransfer.asmx se prenesou data vygenerovane davky
        /// 3. ReceivedDB data oznaci jako prenesena do databaze
        /// </summary>
        /// <param name="cislodavky">cislodavky inventury2</param>
        /// <param name="idterminal">cislo terminalu</param>
        /// <returns>True, pokud se podari data zablokovat pro terminal, False pokud se nepovede</returns>
        /// <exception>Nastane-li vyjimka, je mozne s pokusit o nove blokovani...</exception>
        [WebMethod(Description = "Metoda blokuje data po prenosu do terminalu, je poslednim krokem pri prenosu dat")]
        public bool ReceivedDB(string cislodavky, byte idterminal)
        {
            try
            {
                if (provider != null && provider is Fask.Server.Interfaces.Inventura1.IInventura1)
                {
                    Fask.Server.Interfaces.Classes.Davka davka = new Fask.Server.Interfaces.Classes.Davka();
                    Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();

                    davka.ID = int.Parse(cislodavky);
                    terminal.ID = idterminal;

                    return provider.Inventura2_GetInventuraReceived(
                        davka,
                        terminal
                        );
                }
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }


            string msg = "Provider v Inventura.asmx > 'ReceivedDB(string cislodavky, byte idterminal)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, msg);
            throw new Exception(msg);

        }

        /// <summary>
        /// Metoda která zpracuje prenesena data nebo uvolni davku...
        /// </summary>
        /// <param name="cislodavky">nazev(cislo) davky inventury2</param>
        /// <param name="idterminal">cislo terminalu, ktery data prenasi</param>
        /// <param name="processState">Zpracovat/Uvolnit</param>
        /// <returns>True pokud zapis uspeje, False pokud se nepovede</returns>
        [WebMethod(Description = "Metoda která zpracuje prenesena data nebo uvolni davku...")]
        public StatusObject ProcessDB2(string cislodavky, byte idterminal, ProcessInventuraState processState)
        {
            StatusObject so = new StatusObject();

            if (!isLicenseValid())
            {
                so.StatusText = "Licence na serveru není validní!";
                so.Exception = true;
                return so;
            }


            string dstFile = Path.Combine(Fask.MyPath.Path.SQLiteDBsDirectory, idterminal.ToString() + @"\" + cislodavky.Trim() + Inventura2DBFileExtension);
			string dstFileZip = dstFile + Common.ZIP;

            Fask.Compressing.Zip.Decompress(dstFileZip);

            try
            {
				Fask.SQLiteDBs.DataSets.Inventura2 in2ds = new Fask.SQLiteDBs.DataSets.Inventura2();

				//SQLiteDBs.DataSets.Inventura2TableAdapters.HLAVICKYTableAdapter i2taHlavick = new Fask.MST_W_Server.SQLiteDBs.DataSets.Inventura2TableAdapters.HLAVICKYTableAdapter();
				//SQLiteDBs.DataSets.Inventura2TableAdapters.INVENTURTableAdapter i2taInv = new Fask.MST_W_Server.SQLiteDBs.DataSets.Inventura2TableAdapters.INVENTURTableAdapter();


                //i2taInv.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + dstFile);
				//i2taHlavick.Connection = i2taInv.Connection;

				//try
				//{
				//	i2taInv.Connection.Open();
				//	var tran = i2taInv.Connection.BeginTransaction();

				//	i2taInv.Fill(in2ds.INVENTUR);
				//	i2taHlavick.Fill(in2ds.HLAVICKY);

				//	tran.Commit();

				//}
				//catch (Exception ex)
				//{
				//	Logging.ExceptionHandler2.Handle(ex);
				//}
				//finally
				//{
				//	if (i2taInv != null)
				//	{
				//		if ((i2taInv.Connection.State & ConnectionState.Open) == ConnectionState.Open)
				//			i2taInv.Connection.Close();
				//		i2taInv.Dispose();
				//	}

				//	if (i2taHlavick != null)
				//	{
				//		if ((i2taHlavick.Connection.State & ConnectionState.Open) == ConnectionState.Open)
				//			i2taHlavick.Connection.Close();
				//		i2taHlavick.Dispose();
				//	}
				//}


                try
                {
                    using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Inventura2 ConInv2 = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Inventura2(dstFile))
                    {

                        ConInv2.Fill_Inventur(in2ds.INVENTUR);
                        ConInv2.Fill_Hlavicky(in2ds.HLAVICKY);

                    }
                }
                catch (System.Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(ex);
                }



                Fask.DataSets.Inventura2 tablei2Inv = new Fask.DataSets.Inventura2();

                tablei2Inv.INVENTUR.BeginLoadData();
				foreach (Fask.SQLiteDBs.DataSets.Inventura2.INVENTURRow i2InvRow in in2ds.INVENTUR)
                {
                    tablei2Inv.INVENTUR.LoadDataRow(i2InvRow.ItemArray, false);
                }
                tablei2Inv.INVENTUR.EndLoadData();


                tablei2Inv.HLAVICKY.BeginLoadData();
				foreach (Fask.SQLiteDBs.DataSets.Inventura2.HLAVICKYRow i2InvRow in in2ds.HLAVICKY)
                {
                    tablei2Inv.HLAVICKY.LoadDataRow(i2InvRow.ItemArray, false);
                }
                tablei2Inv.HLAVICKY.EndLoadData();

                so = ProcessInventura(cislodavky, idterminal, tablei2Inv, processState);

                if (so.StatusText == "OK" && !so.Exception)
                {
                    Routines.ManageDataFiles.Move(dstFile, Path.Combine(Fask.MyPath.Path.ProcessedDataFileDirectory, Path.GetFileName(dstFile)));
                }
                else
                {
                    Routines.ManageDataFiles.Move(dstFile, Path.Combine(Fask.MyPath.Path.ErrorDataFileDirectory, Path.GetFileName(dstFile)));
                }

                return so;
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                Routines.ManageDataFiles.Move(dstFile, Path.Combine(Fask.MyPath.Path.ErrorDataFileDirectory, Path.GetFileName(dstFile)));
                so.StatusText = ex.Message;
                so.Exception = true;

                throw Routines.Exceptions.CustomSoapException("Process", ex.Message, "Inventura");

            }
            finally
            {
                try
                {
                    File.Delete(dstFile);
                }
                catch (Exception ex)
                {
					Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                }
            }
        }


        #endregion

        #region Privatne metody

        /// <summary>
        /// Metoda pro dotaženi inventury z DB pro pripravu souboru
        /// </summary>
        /// <param name="cislodavky">číslo dávky</param>
        /// <param name="idterminal">ID Terminalu</param>
        /// <returns>Dataset Inventura2, naplnen datama</returns>
        private Fask.DataSets.Inventura2 GetInventura2(string cislodavky, byte idterminal)
        {
            try
            {
                if (provider != null && provider is Fask.Server.Interfaces.Inventura1.IInventura1)
                {
                    Fask.Server.Interfaces.Classes.Davka davka = new Fask.Server.Interfaces.Classes.Davka();
                    Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();

                    davka.ID = int.Parse(cislodavky);
                    terminal.ID = idterminal;

                    return provider.Inventura2_GetInventura(davka, terminal);
                }
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }

            string msg = "Provider v Inventura.asmx > 'GetInventura2(string cislodavky, byte idterminal)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, msg);
            throw new Exception(msg);
        }

        /// <summary>
        /// Metoda pro kontrolu licence
        /// </summary>
        /// <returns>True - Licence je validni, False- Licence neni validni</returns>
        private bool isLicenseValid()
        {
			// \bug : resit nejakym lepsim zpusobem ... 

            Licensing.License lic = Application[Constants.Common.license] as Licensing.License;

            if (lic != null)
            {
                if (!lic.isValid || lic.isExpirated)
                    return false;

                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Metoda která zpracuje prenesena data nebo uvolni davku...
        /// </summary>
        /// <param name="cislodavky">číslo dávky</param>
        /// <param name="idterminal">ID Terminalu</param>
        /// <param name="tablei2Inv">Data pro zpracovaní</param>
        /// <param name="processState">Zpracovat/Uvolnit</param>
        /// <returns>StatusObject - Nese informace o stavu</returns>
        private StatusObject ProcessInventura(string cislodavky, byte idterminal, Fask.DataSets.Inventura2 tablei2Inv, ProcessInventuraState processState)
        {

            StatusObject processStatus = new StatusObject();

            if (provider != null)
            {

                try
                {
                    Fask.Server.Interfaces.Classes.Davka davka = new Fask.Server.Interfaces.Classes.Davka();
                    Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();
                    Fask.Server.Interfaces.Classes.Sklad sklad = new Fask.Server.Interfaces.Classes.Sklad();
                    Fask.Server.Interfaces.Classes.Item item = new Fask.Server.Interfaces.Classes.Item();

                    davka.ID = int.Parse(cislodavky);
                    terminal.ID = idterminal;

                    Fask.Server.Interfaces.Inventura2.ProcessState pState = (Fask.Server.Interfaces.Inventura2.ProcessState)processState;

                    processStatus = provider.Inventura2_Process(
                        davka,
                        terminal,
                        tablei2Inv,
                        pState
                        );

                    return processStatus;

                }
                catch (Exception ex)
                {
					Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                    throw ex;
                }
            }

            throw new Exception();
        }

        #endregion
 
    }
}
