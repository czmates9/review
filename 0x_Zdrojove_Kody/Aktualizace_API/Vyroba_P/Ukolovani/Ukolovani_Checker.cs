using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Fask.Aktualizace_API.UkolovaniService;
using System.IO;
using System.Windows.Forms;
//using Fask.MST_W.SqlCEDBs.DataSets;

namespace Fask.Aktualizace_API.Ukolovani
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
                Fask.Aktualizace_API.Data.UkolyTableAdapters.CZ_UKOL_UZIVTableAdapter ta_ukoluziv = new Fask.Aktualizace_API.Data.UkolyTableAdapters.CZ_UKOL_UZIVTableAdapter();
                Fask.Aktualizace_API.Data.UkolyTableAdapters.CZ_UKOL_STATETableAdapter ta_ukolstate = new Fask.Aktualizace_API.Data.UkolyTableAdapters.CZ_UKOL_STATETableAdapter();

                ta_ukoluziv.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.Ukoly + Constants.PRD));
                ta_ukolstate.Connection = ta_ukoluziv.Connection;

                Fask.Aktualizace_API.Data.Ukoly.CZ_UKOL_STATEDataTable dt_ukolstate = ta_ukolstate.GetData();

                Fask.Aktualizace_API.Data.Ukoly.CZ_UKOL_UZIVDataTable dt_ukoluziv = ta_ukoluziv.GetDataByUserID(userID);
                StringBuilder sb = new StringBuilder();
                foreach (var item in dt_ukoluziv)
                {
                    Fask.Aktualizace_API.Data.Ukoly.CZ_UKOL_STATERow r_ukolstate = dt_ukolstate.FindByState(item.State);
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
                Fask.Logging.ExceptionHandler2.Handle("Ukolovani_Checker", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return "U:Err";
            }
        }


        private static Ukolovani_Checker ukolovaniChecker = null;

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
                Fask.Logging.ExceptionHandler2.Handle("Ukolovani_Checker", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
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
                Fask.Logging.ExceptionHandler2.Handle("Ukolovani_Checker", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        // Instance 



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

                    System.Threading.Thread.Sleep(Constants.TasksSynchronizationInterval);

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
                    string downloaded = "";
                    Fask.Aktualizace_API.Data.UkolyTableAdapters.CZ_UKOL_UZIVTableAdapter uota = new Fask.Aktualizace_API.Data.UkolyTableAdapters.CZ_UKOL_UZIVTableAdapter();
                    Fask.Aktualizace_API.Data.UkolyTableAdapters.CZ_UKOL_UZIVTableAdapter usta = new Fask.Aktualizace_API.Data.UkolyTableAdapters.CZ_UKOL_UZIVTableAdapter();
                    Fask.Aktualizace_API.Data.UkolyTableAdapters.CZ_UKOLTableAdapter uta = new Fask.Aktualizace_API.Data.UkolyTableAdapters.CZ_UKOLTableAdapter();

                    uota.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + Constants.CiselnikUkolyDB);
                    usta.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + Constants.CiselnikUkolyDBSynch);
                    uta.Connection = usta.Connection;

                    // started ... 
                   Fask.Aktualizace_API.Data.Ukoly.CZ_UKOL_UZIVDataTable uodt;
                   Fask.Aktualizace_API.Data.Ukoly.CZ_UKOL_UZIVDataTable usdt;

                    try
                    {
                        so = Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.ukolovaniService.PrepareDB(Settings.TerminalID, int.Parse(Settings.LastProductionUserID), Path.GetFileName(Constants.CiselnikUkolyDBSynch));

                        if (synchronizationInterrupt)
                            return;

                        if (so == null || so.StatusText != "OK")
                        {
                            Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "Ukolovani_Checker, Synchronize >> Exception:" + so.Exception.ToString() + "(" + so.StatusText + ")");
                        }
                        else
                        {
                            downloaded = FileTransfer.Downloading.DownloadFileFromServer(Constants.CiselnikUkolyDBSynch, true, true, null);
                        }

                    }
                    catch (Exception ex)
                    {
                        Fask.Logging.ExceptionHandler2.Handle("Ukolovani_Checker", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                    }

                    if (synchronizationInterrupt)
                        return;

                    //seznam novych id ukolu (CZ_UKOL)
                    List<UkolovaniService.Ukol> noveukoly = new List<UkolovaniService.Ukol>();
                    if (downloaded == "OK")
                    {
                        uodt = uota.GetDataByUserIDNew(int.Parse(Settings.LastProductionUserID));

                        if (synchronizationInterrupt)
                            return;

                        usdt = usta.GetDataByUserIDNew(int.Parse(Settings.LastProductionUserID));

                        if (synchronizationInterrupt)
                            return;

                        foreach (var sitem in usdt)
                        {
                           Fask.Aktualizace_API.Data.Ukoly.CZ_UKOL_UZIVRow oitem = uodt.FindByID(sitem.ID);
                            if (oitem == null) //neni ve stavajici databazi, takze je novy ...
                            {
                                //noveukoly.Add(sitem.UkolID);
                                UkolovaniService.Ukol nUkol = new UkolovaniService.Ukol();
                                nUkol.id = sitem.ID;
                                nUkol.nazev = uta.GetDataByID(sitem.UkolID)[0].Name;
                                noveukoly.Add(nUkol);
                            }
                        }

                        if (synchronizationInterrupt)
                            return;

                        //prepsani databaze ...                     
                        File.Copy(Constants.CiselnikUkolyDBSynch, Constants.CiselnikUkolyDB, true);
                        File.Delete(Constants.CiselnikUkolyDBSynch);

                        if (synchronizationInterrupt)
                            return;

                        if (noveukoly.Count > 0) //Jsou nove ukoly, tak nejak zobrazit ...
                        {
                            // Prehrat zvuk ze je novy ukol ...
                            //MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, "notify.wav")); //ok, vlozeno pro informaci
                            //Program.mstw.ShowAsynchMessageBoxBig("Počet nových úkolů: " + noveukoly.Count);
                            Fask.Aktualizace_API.Forms.FormMain main = new Fask.Aktualizace_API.Forms.FormMain();
                            main.ShowAsynchNewTasks(noveukoly);
                        }

                        if (synchronizationInterrupt)
                            return;

                        //Program.mstw.UpdateStatusBar();
                    }

                    //pripomenuti bude cekovat, jen pokud nejsou nove ukoly ... 
                    List<UkolovaniService.Ukol> pripomenutiukoly = new List<UkolovaniService.Ukol>();
                    if (noveukoly.Count <= 0)
                    {
                        DateTime checkTime = DateTime.Now;
                        // TODO : Kontrola pripomenuti a pripadne vyvolani dialogu pripomenuti ukolu ... 
                        // zobrazit timedialog ...
                        uodt = uota.GetDataByUserID(int.Parse(Settings.LastProductionUserID));
                        uta.Connection = uota.Connection;
                        foreach (var item in uodt)
                        {
                            if (synchronizationInterrupt)
                                return;

                            if (!item.IsDateNotifyNull() && (item.DateNotify <= checkTime))
                            {
                                Fask.Aktualizace_API.UkolovaniService.Ukol pUkol = new Fask.Aktualizace_API.UkolovaniService.Ukol();
                                pUkol.id = item.ID;
                                pUkol.nazev = uta.GetDataByID(item.UkolID)[0].Name;
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
                            //MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, "notify.wav")); //ok, vlozeno pro informaci
                            //Program.mstw.ShowAsynchMessageBoxBig("Počet nových úkolů: " + noveukoly.Count);
                            Fask.Aktualizace_API.Forms.FormMain main = new Fask.Aktualizace_API.Forms.FormMain();
                            main.ShowAsynchNotificationTasks(pripomenutiukoly);
                        }
                    }

                    if (synchronizationInterrupt)
                        return;

                }
            }
            catch (System.Threading.ThreadAbortException taex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Ukolovani_Checker", System.Reflection.MethodBase.GetCurrentMethod().Name, taex);
                // TODO : zde se killuje vlakno stopem ...
            }
            catch (Exception ex)
            {
                // TODO : logovani vyjimky odstranit ...
                Fask.Logging.ExceptionHandler2.Handle("Ukolovani_Checker", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
            finally
            {
                synchronizationInProgress = false;
            }
        }
    }
}
