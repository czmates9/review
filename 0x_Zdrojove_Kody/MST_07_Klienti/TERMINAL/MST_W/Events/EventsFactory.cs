using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Fask.MST_W.ServerAccess;
using System.IO;
using Fask.MST_W;
using Fask.MST_W.Forms;

namespace Fask.Events
{
    public class EventsFactory : IEvents
    {
        Fask.MST_W._WebRefernces_Globals.EventsServiceSession eventsService = new Fask.MST_W._WebRefernces_Globals.EventsServiceSession();

        private static EventsFactory sharedInstance = null;
        public static EventsFactory SharedInstance
        {
            get
            {
                if (sharedInstance == null)
                    sharedInstance = new EventsFactory();
                    
                return sharedInstance;
            }
        }

        //pristupuje se pouze pres statickou promennou sharedInstance ...
        private EventsFactory()
        {
            eventsService.Url = MST_W.MST_Global.ServerAddress + "EventsService.asmx";
            //eventsService.Timeout = MST_W.MST_Global.ServiceTimeOut;
            eventsService.Timeout = MST_W.MST_Global.EventsOnlineTimeout;
            eventsService.UpdateWebServiceCredentials();
        }

        #region IEventsUser Members

        public bool synchronize()
        {
            if (!MST_W.MST_Global.EventsEnable)
                return false;
            //throw new NotImplementedException();

            try
            {
                //1) presunout data pro presun
                //2) zkopirovat z predlohy do data
                //3) odeslat file
                //  i) overit, ze tam neco je
                //  - pokud neni prazdne, tak odeslat
                //  - pokud je prazdny, tak smazat
                //4) provest nahrani
                //5) ostatni soubory

                string eventsdestinationfile = MST_W.Main.EventsUserDBData + "_" + DateTime.Now.ToFileTimeUtc() + "." + MST_W.Main.Ext_EventsToTransfer;

                lock (this)
                {
                    if (File.Exists(Main.EventsUserDBData))
                    {
                        File.Move(
                            MST_W.Main.EventsUserDBData,
                            eventsdestinationfile
                            );
                    }
                    File.Copy(MST_W.Main.EventsUserDBSQLCeDBs, MST_W.Main.EventsUserDBData);
                }

                try
                {
                    int? pocetudalosti = null;
                    using (var controller_eventsuser = new Fask.SQLiteDBs.Controllers.SQLite_Controller_EventsUser(eventsdestinationfile))
                    {
                        //pocetudalosti = Convert.ToInt32(controller_eventsuser.CZMST_EventsUser_PocetUdalosti());
                        pocetudalosti = controller_eventsuser.PocetUdalosti();
                    }
                    if (pocetudalosti.HasValue && pocetudalosti.Value <= 0)
                    { //smazat soubor a hotovo ...
                        File.Delete(eventsdestinationfile);
                    }
                }
                catch (Exception ex)
                {
                    Logging.Log.Write(ex, "Events synchronize - empty test ... ");
                }
                //pokracuji dal odesilanim ...

                //MST_W.FileTransferService.FileTransfer ftransfer = new Fask.MST_W.FileTransferService.FileTransfer();
                //ftransfer.Url = MST_Global.ServerAddress + "FileTransfer.asmx";
                //ftransfer.Timeout = 10000;
                //ftransfer.UpdateWebServiceCredentials();

                foreach (string evtfile in Directory.GetFiles(MST_W.Main.DataDir, "*." + MST_W.Main.Ext_EventsToTransfer))
                {
                    #region New sending code
                    string filename = evtfile;
                    string davkafilename = Path.GetFileName(filename);
                    long offset = 0;
                    //long bufferMaxLength = ftransfer.GetMaxRequestLength();
                    int bufferLength = 1024 * 256;
                    byte[] buffer = new byte[bufferLength];
                    int bytesRead = 0;
                    long fileSize = (new FileInfo(filename)).Length;
                    int errorCount = 0;
                    int errorCountMax = 5;
                    using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
                    {
                        //ftransfer.Timeout = 3000;

                        fs.Seek(offset, SeekOrigin.Begin);
                        do
                        {
                            bytesRead = fs.Read(buffer, 0, buffer.Length);
                            if (bytesRead == 0)
                                break;

                            if (bytesRead != bufferLength)
                            {
                                bufferLength = bytesRead;
                                byte[] bufferTrimmed = new byte[bytesRead];
                                Array.Copy(buffer, bufferTrimmed, bytesRead);
                                buffer = bufferTrimmed;
                            }

                            //Odeslat na server
                            try
                            {
                                // TODO : opravit toto odesilani ... !!!
                                // => potreba moznosti odeslani na pozadi ... ???

                                //Program.mstw.mbw.Zprava = "Odesílají se data inventury\n" + (int)(offset * 100 / (float)fileSize) + "%";
                                //ftransfer.AppendChunk(MST_Global.TerminalID, davkafilename, buffer, offset);

								string KamNaServer = MST_Global.TerminalID + "\\" + Path.GetFileName(filename);
								if (Fask.MST_W.FileTransfer.Uploading.SendFile_API(filename, KamNaServer, Fask.MST_W.FileTransfer.Uploading.Co.Events) != "OK") { throw new Exception("davku se nepodarilo odeslat"); };


                                //MST_W.FileTransfer.Uploading.SendFileCalcTime(MST_Global.ServerAddress + "Upload.aspx", filename, MST_Global.TerminalID + "\\" + Path.GetFileName(filename), Fask.MST_W.FileTransfer.Uploading.Co.Events); 
                                offset += bytesRead;
                                errorCount = 0;
                            }
                            catch (Exception ex)
                            {
                                fs.Position -= bytesRead;
                                if (errorCount++ >= errorCountMax)
                                    throw new Exception("Při odesílání nastaly potíže: " + ex.Message);

                            }
                        } while (bytesRead > 0);
                    }

                    #endregion

                    if (eventsService.ProcessEventFileName(MST_W.MST_Global.TerminalID, davkafilename))
                    {
                        // pokud ok, tak smazat file
                        File.Delete(filename);
                    }
                }                

                return true;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                return false;
            }
        }

        public bool add(Event euser)
        {
            if (!MST_W.MST_Global.EventsEnable)
                return false;

            //throw new NotImplementedException();
            /** toto neni dobre, protoze to je blokujici operace a v samostatnem vlakne taky nevhodne ... 
             * 
             * */
            try
            {
                if (MST_W.MST_Global.EventsOnline)
                {
                    Fask.MST_W.EventsService.Events serverEvent = new Fask.MST_W.EventsService.Events();
                    Fask.MST_W.EventsService.Events.CZMST_EventsUserRow ner = serverEvent.CZMST_EventsUser.NewCZMST_EventsUserRow();
                    ner.eguid = euser.eguid;
                    ner.eid = euser.eid;
                    ner.etype = euser.etype;
                    ner.etime = euser.etime;
                    ner.termid = euser.termid;
                    ner.userid = euser.userid;
                    if (euser.loginid != null)
                        ner.loginid = euser.loginid;
                    if (euser.machineid != null)
                        ner.machineid = euser.machineid;
                    if (euser.modul != null)
                        ner.modul = euser.modul;
                    if (euser.countentries.HasValue)
                        ner.countentries = euser.countentries.Value;
                    if (euser.docnmbr != null)
                        ner.docnmbr = euser.docnmbr;
                    if (euser.itemnmbr != null)
                        ner.itemnmbr = euser.itemnmbr;
                    if (euser.REZ1 != null)
                        ner.REZ1 = euser.REZ1;
                    if (euser.REZ2 != null)
                        ner.REZ2 = euser.REZ2;
                    serverEvent.CZMST_EventsUser.AddCZMST_EventsUserRow(ner);

                    while (true)
                    {
                        bool result = false;

                        try
                        {
                            result = eventsService.ProcessEventData(MST_W.MST_Global.TerminalID, serverEvent);
                            if (result)
                                return true;
                        }
                        catch (Exception e)
                        {
                            Logging.Log.Write(e);
                            //MessageBoxBig.Show(e.Message, "Událost online", System.Windows.Forms.MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                            result = false;
                        }

                        if (!result && MST_Global.EventsOnlineConfirm)
                        {
                            if (System.Windows.Forms.DialogResult.No == 
                                MessageBoxBig.Show("Odeslání události se nezdařilo.\nOpakovat?\n\nAno = opakovat odeslání\nNe  = odeslat později", "Událost online", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning, System.Drawing.Color.Red))
                            {
                                break;
                            }
                            else //v cyklu se odesle znovu ...
                            {
                            }
                        }
                        else
                        {
                            break; //resul je ok nebo se event online nepotvrzuje
                            // => ulozi se k pozdejsimu odeslani ...
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
            /* 
            *
            * */

            //pokud vyjimka, nebo se nepodari na server, tak do lokalu, k dalsi synchronizaci ...
            try
            {
                //lock je tady kvuli tomu, ze muze probihat kopirovani/presun samotneho souboru udalosti (druhu lock je v synchronize pri presunech)
                lock (this)
                {
                    if (!File.Exists(Main.EventsUserDBData)) //Pokud neni, tak ho tam supnu ...
                        File.Copy(Main.EventsUserDBSQLCeDBs, Main.EventsUserDBData);

                    //using (Fask.SQLiteDBs.DataSets.EventsUserTableAdapters.CZMST_EventsUserTableAdapter eta = new Fask.SQLiteDBs.DataSets.EventsUserTableAdapters.CZMST_EventsUserTableAdapter())
                    //{
                    //    //eta.Connection = new System.Data.SqlServerCe.SqlCeConnection("Data source=" + MST_W.Main.EventsUserDBData);
                    //    eta.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + MST_W.Main.EventsUserDBData);
                    //    eta.Insert(euser.eguid, euser.eid, euser.etype, euser.etime, euser.termid, euser.userid,
                    //        euser.loginid, euser.machineid, euser.modul, euser.countentries, euser.docnmbr, euser.itemnmbr, euser.REZ1, euser.REZ2);
                    //}

					using (var controller_eventsuser = new Fask.SQLiteDBs.Controllers.SQLite_Controller_EventsUser(Main.EventsUserDBData))
                    {
						var dt_ue = new Fask.SQLiteDBs.DataSets.EventsUser.CZMST_EventsUserDataTable();
                        
                        var row_ue = dt_ue.NewCZMST_EventsUserRow();
                        if (euser.countentries.HasValue)
                            row_ue.countentries = euser.countentries.Value;
                        if (euser.docnmbr != null)
                            row_ue.docnmbr = euser.docnmbr;
                        if (euser.eguid != null)
                            row_ue.eguid = euser.eguid;
                        if (euser.eid != null)
                            row_ue.eguid = euser.eguid;
                        if (euser.eid != null)
                            row_ue.eid = euser.eid;
                        if (euser.etime != null)
                            row_ue.etime = euser.etime;
                        if (euser.etype != null)
                            row_ue.etype = euser.etype;
                        if (euser.itemnmbr != null)
                            row_ue.itemnmbr = euser.itemnmbr;
                        if (euser.loginid != null)
                            row_ue.loginid = euser.loginid;
                        if (euser.machineid != null)
                            row_ue.machineid = euser.machineid;
                        if (euser.modul != null)
                            row_ue.modul = euser.modul;
                        if (euser.REZ1 != null)
                            row_ue.REZ1 = euser.REZ1;
                        if (euser.REZ2 != null)
                            row_ue.REZ2 = euser.REZ2;
                        row_ue.termid = euser.termid;
                        row_ue.userid = euser.userid;

                        System.Diagnostics.Debug.Assert(row_ue.RowState == System.Data.DataRowState.Added);

                        dt_ue.AddCZMST_EventsUserRow(row_ue);
                        
                        //controller_eventsuser.Update(row_ue); // row_ue je novy sloupec -> insert...
                        controller_eventsuser.Update(row_ue);
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }

            return false;
        }

        #endregion
    }
}
