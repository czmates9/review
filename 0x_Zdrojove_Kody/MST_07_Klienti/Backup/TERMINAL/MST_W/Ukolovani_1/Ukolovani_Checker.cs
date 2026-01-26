using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Fask.MST_W.ServerAccess;
using System.IO;
using System.Windows.Forms;

namespace Fask.MST_W.Ukolovani_1
{
    public class Ukolovani_Checker
    {
        // Trida pro synchronizace ukolu na pozadi ... 
        // pri otevreni hlavniho menu ukolu, se synchronizace zastavi i kdyz probiha...
        // synchronizace bezi v smostatnem vlakne
        // pri ukonceni aplikace se musi ukoncit ... 
        
        // proces:
        // 1) stahnout ciselnik do docasneho souboru
        // 2) zjistit nove ukoly z cz_ukol_uziv
        // 3) porovnat, zda pribyli nove ukoly porovnanim s aktualni db
        // 4) pokud ano, tak zaznamenat pocet novych ukolu pro zobrazeni a zpravu pro hlavni okno
        // 5) prepsat akutalni verzi Ukolovaci databaze
        // 6) Pokud nastane novy ukol/tak zobrazit poslat do hlavniho vlakna zpravu

        /// <summary>
        /// Slouzi pro informaci, kdy byl proveden posledni check ukolu a s tim souvisejicich zobrazeni pripomenuti.
        /// </summary>
        public static DateTime? LastCheck = null;

        // class 
        public static string UkolyInfo(int userID)
        {
            int pocetstart = 0;
            int pocetwork = 0;
            int pocetend = 0;
            return UkolyInfo(userID, out pocetstart, out pocetend, out pocetwork);
        }

        public static string UkolyInfo(int userID, out int pocetstart, out int pocetend, out int pocetwork)
        {
            pocetstart = 0;
            pocetwork = 0;
            pocetend = 0;

            try
            {
				//SqlCEDBs.DataSets.UkolyTableAdapters.CZ_UKOL_UZIVTableAdapter ta_ukoluziv = new Fask.SQLiteDBs.DataSets.UkolyTableAdapters.CZ_UKOL_UZIVTableAdapter();
				//SqlCEDBs.DataSets.UkolyTableAdapters.CZ_UKOL_STATETableAdapter ta_ukolstate = new Fask.SQLiteDBs.DataSets.UkolyTableAdapters.CZ_UKOL_STATETableAdapter();

				//ta_ukoluziv.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + Main.CiselnikUkolyDB);
				//ta_ukolstate.Connection = ta_ukoluziv.Connection;

				//SqlCEDBs.DataSets.Ukoly.CZ_UKOL_STATEDataTable dt_ukolstate = ta_ukolstate.GetData();

				//SqlCEDBs.DataSets.Ukoly.CZ_UKOL_UZIVDataTable dt_ukoluziv = ta_ukoluziv.GetDataByUserID(userID);


				Fask.SQLiteDBs.DataSets.Ukoly.CZ_UKOL_STATEDataTable dt_ukolstate = null;
				Fask.SQLiteDBs.DataSets.Ukoly.CZ_UKOL_UZIVDataTable dt_ukoluziv = null;

				using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Ukoly ConUkoly = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Ukoly(Main.CiselnikUkolyDB))
				{
					dt_ukolstate = ConUkoly.CZ_UKOL_STATE_GetData();
					dt_ukoluziv = ConUkoly.CZ_UKOL_UZIV_GetDataByUserID(userID);
				}


                StringBuilder sb = new StringBuilder();
                foreach (var item in dt_ukoluziv)
                {
                    Fask.SQLiteDBs.DataSets.Ukoly.CZ_UKOL_STATERow r_ukolstate = dt_ukolstate.FindByState(item.State);
                    if (r_ukolstate == null)
                        continue;
                    else if (r_ukolstate.IsStart)
                        pocetstart++;
                    else if (r_ukolstate.IsEnd)
                        pocetend++;
                    else
                        pocetwork++;
                }
                //sb.AppendFormat("U:{0}N,{1}A,{2}E", new object[] { pocetstart, pocetwork, pocetend });
                sb.AppendFormat("U{0}:{1}N,{2}A", new object[] { SynchronizationInProgress ? "*" : "-", pocetstart, pocetwork });
                return sb.ToString();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                return "U:Err";
            }
        }


        private static Ukolovani_Checker ukolovaniChecker = null;
		public GlobalObject globalObject = new GlobalObject(); 


        public static bool SynchronizationInProgress
        {
            get { return ukolovaniChecker != null ? ukolovaniChecker.synchronizationInProgress : false; }
        }

        public static void SynchronizationStop()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (ukolovaniChecker == null)
                    return;

                ukolovaniChecker.Stop();
                if (ukolovaniChecker.synchronizationThread != null)
                    ukolovaniChecker.synchronizationThread.Join(1000);

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        public static void SynchronizationStart()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (ukolovaniChecker == null)
                    ukolovaniChecker = new Ukolovani_Checker();
                if (!ukolovaniChecker.synchronizationInProgress)
                    ukolovaniChecker.Start();

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        // Instance 

        //Ukolovani_Helper ukolovaniHelper = new Ukolovani_Helper();

        private Ukolovani_Checker()
        {
        }

        public bool synchronizationInProgress = false;
        private System.Threading.Thread synchronizationThread = null;

        protected void Start()
        {
            synchronizationInProgress = true;
            synchronizationThread = new System.Threading.Thread(new System.Threading.ThreadStart(Synchronize));
            synchronizationThread.IsBackground = true;
            synchronizationThread.Name = "UkolovaniSynchronizationThread";
            synchronizationThread.Start();
        }

        protected void Stop()
        {
            if (synchronizationThread != null)
            {
                synchronizationInterrupt = true; // korektni ukonceni ...
                if (!synchronizationThread.Join(1000))
                {
                    synchronizationThread.Abort();
                    synchronizationThread.Join(1000);
                }
            }
            synchronizationInProgress = false;
        }

        private bool synchronizationInterrupt = false;
        private void Synchronize()
        {
            try
            {
                synchronizationInterrupt = false;
                // cyklus synchronizace ... 
                while (true)
                {
                    // Test na korektni ukonceni synchronizace ... 
                    if (synchronizationInterrupt)
                        return;

                    System.Threading.Thread.Sleep(MST_Global.TasksSynchronizationInterval);

                    // Synchronizace ... 
                    // proces:
                    // 1) stahnout ciselnik do docasneho souboru
                    // 2) zjistit nove ukoly z cz_ukol_uziv
                    // 3) porovnat, zda pribyli nove ukoly porovnanim s aktualni db
                    // 4) pokud ano, tak zaznamenat pocet novych ukolu pro zobrazeni a zpravu pro hlavni okno
                    // 5) prepsat akutalni verzi Ukolovaci databaze
                    // 6) Pokud nastane novy ukol/tak zobrazit poslat do hlavniho vlakna zpravu

                    // Implementace ...
                    // 1) stazeni souboru
                    UkolovaniService.StatusObject so = null;
                    bool downloaded = false;

                    //SqlCEDBs.DataSets.UkolyTableAdapters.CZ_UKOL_UZIVTableAdapter uota = new Fask.SQLiteDBs.DataSets.UkolyTableAdapters.CZ_UKOL_UZIVTableAdapter();
                    //SqlCEDBs.DataSets.UkolyTableAdapters.CZ_UKOL_UZIVTableAdapter usta = new Fask.SQLiteDBs.DataSets.UkolyTableAdapters.CZ_UKOL_UZIVTableAdapter();
                    //SqlCEDBs.DataSets.UkolyTableAdapters.CZ_UKOLTableAdapter uta = new Fask.SQLiteDBs.DataSets.UkolyTableAdapters.CZ_UKOLTableAdapter();

                    //uota.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + Main.CiselnikUkolyDB);
                    //usta.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + Main.CiselnikUkolyDBSynch);
                    //uta.Connection = usta.Connection;

                    // started ... 
                    Fask.SQLiteDBs.DataSets.Ukoly.CZ_UKOL_UZIVDataTable uodt;
                    Fask.SQLiteDBs.DataSets.Ukoly.CZ_UKOL_UZIVDataTable usdt;

                    try
                    {
						so = Ukolovani_Checker.ukolovaniChecker.globalObject._ukolovaniService.PrepareDB(MST_Global.TerminalID, MST_Global.UserID, Path.GetFileName(Main.CiselnikUkolyDBSynch));
                        //so = ukolovaniHelper.ukolovaniService.PrepareDB(MST_Global.TerminalID, MST_Global.UserID, Path.GetFileName(Main.CiselnikUkolyDBSynch));

                        if (synchronizationInterrupt)
                            return;

                        if (so == null || so.StatusText != "OK")
                        {
                            Logging.Log.Write("Exception:" + so.Exception.ToString() + "(" + so.StatusText + ")", "Ukolovani_Checker");
                        }
                        else
                        {
                            FileTransfer.Routines.DownloadDecompressDelete(Main.CiselnikUkolyDBSynch);
                            downloaded = true;
                        }

                    }
                    catch (Exception ex)
                    {
                        Logging.Log.Write(ex);
                    }

                    if (synchronizationInterrupt)
                        return;

                    //seznam novych id ukolu (CZ_UKOL)
                    List<UkolovaniService.Ukol> noveukoly = new List<Fask.MST_W.UkolovaniService.Ukol>();
                    if (downloaded)
                    {
						uodt = Ukolovani_Checker.ukolovaniChecker.globalObject.controller_ukoly.CZ_UKOL_UZIV_GetDataByUserIDNew(MST_Global.UserID);

                        if (synchronizationInterrupt)
                            return;

						usdt = Ukolovani_Checker.ukolovaniChecker.globalObject.controller_ukolySync.CZ_UKOL_UZIV_GetDataByUserIDNew(MST_Global.UserID);

                        if (synchronizationInterrupt)
                            return;

                        foreach (var sitem in usdt)
                        {
                            Fask.SQLiteDBs.DataSets.Ukoly.CZ_UKOL_UZIVRow oitem = uodt.FindByID(sitem.ID);
                            if (oitem == null) //neni ve stavajici databazi, takze je novy ...
                            {
                                //noveukoly.Add(sitem.UkolID);
                                Fask.MST_W.UkolovaniService.Ukol nUkol = new Fask.MST_W.UkolovaniService.Ukol();
                                nUkol.id = sitem.ID;
								nUkol.nazev = Ukolovani_Checker.ukolovaniChecker.globalObject.controller_ukolySync.CZ_UKOL_GetDataByID(sitem.UkolID)[0].Name;
                                noveukoly.Add(nUkol);
                            }
                        }

                        if (synchronizationInterrupt)
                            return;

                        //prepsani databaze ...                     
                        File.Copy(Main.CiselnikUkolyDBSynch, Main.CiselnikUkolyDB, true);
                        File.Delete(Main.CiselnikUkolyDBSynch);

                        if (synchronizationInterrupt)
                            return;

                        if (noveukoly.Count > 0) //Jsou nove ukoly, tak nejak zobrazit ...
                        {
                            // Prehrat zvuk ze je novy ukol ...
                            MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, "notify.wav")); //ok, vlozeno pro informaci
                            //Program.mstw.ShowAsynchMessageBoxBig("Počet nových úkolů: " + noveukoly.Count);
                            Program.mstw.ShowAsynchNewTasks(noveukoly);
                        }

                        if (synchronizationInterrupt)
                            return;

                        Program.mstw.UpdateStatusBar();
                    }

                    //pripomenuti bude cekovat, jen pokud nejsou nove ukoly ... 
                    List<UkolovaniService.Ukol> pripomenutiukoly = new List<Fask.MST_W.UkolovaniService.Ukol>();
                    if (noveukoly.Count <= 0)
                    {
                        DateTime checkTime = DateTime.Now;
                        // TODO : Kontrola pripomenuti a pripadne vyvolani dialogu pripomenuti ukolu ... 
                        // zobrazit timedialog ...
						uodt = Ukolovani_Checker.ukolovaniChecker.globalObject.controller_ukoly.CZ_UKOL_UZIV_GetDataByUserID(MST_Global.UserID);
                        //uta.Connection = uota.Connection;
                        foreach (var item in uodt)
                        {
                            if (synchronizationInterrupt)
                                return;

                            if (!item.IsDateNotifyNull() && (item.DateNotify <= checkTime))
                            {
                                Fask.MST_W.UkolovaniService.Ukol pUkol = new Fask.MST_W.UkolovaniService.Ukol();
                                pUkol.id = item.ID;
								pUkol.nazev = Ukolovani_Checker.ukolovaniChecker.globalObject.controller_ukoly.CZ_UKOL_GetDataByID(item.UkolID)[0].Name;
                                pripomenutiukoly.Add(pUkol);

                                // TODO : Pripomenuti pouze jednoho ukolu ... ???
                                //break;
                            }
                        }

                        if (synchronizationInterrupt)
                            return;

                        if (pripomenutiukoly.Count > 0)
                        {
                            // Prehrat zvuk pripomenuti ukolu ...
                            MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, "notify.wav")); //ok, vlozeno pro informaci
                            //Program.mstw.ShowAsynchMessageBoxBig("Počet nových úkolů: " + noveukoly.Count);
                            Program.mstw.ShowAsynchNotificationTasks(pripomenutiukoly);
                        }
                    }

                    if (synchronizationInterrupt)
                        return;

                }
            }
            catch (System.Threading.ThreadAbortException taex)
            {
                Logging.Log.Write(taex);
                // TODO : zde se killuje vlakno stopem ...
            }
            catch (Exception ex)
            {
                // TODO : logovani vyjimky odstranit ...
                Logging.Log.Write(ex);
            }
            finally
            {
                synchronizationInProgress = false;
            }
        }
    }
}
