using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
//using Fask.MST_W.ServerAccess;
using System.Net;
//using Fask.MST_W.Forms;

namespace FASK.MST_WINDOWS.Forms.FileTransfer
{
    public class Downloading
    {
        public static bool DownloadFileFromServer(string fileciselnik)
        {
            return DownloadFileFromServer(fileciselnik, false, true,null);
        }



        /// <summary>
        /// Stahnuti dat ze serveru.
        /// </summary>
        /// <param name="fileciselnik">cela cesta k souboru jake ma sa ulozit</param>
        /// <param name="silent">Zobrazeni progressu (true tak je ticho, false tak se zobrazuje progress)</param>
        /// <param name="overwrite">Prepsat soubor nove stazenym (True -> prepsat, False -> nechat puvodni i Tmp)</param>
        /// <returns></returns>
        //public static bool DownloadFileFromServer(string fileciselnik, bool silent, bool overwrite)
        //{
        //    string fileciselniktmp = fileciselnik + ".tmp";

        //    #region Downloading code
        //    FileStream fs = null;
        //    try
        //    {
        //        if (File.Exists(fileciselniktmp))
        //            File.Delete(fileciselniktmp);

        //        #region Chunked download code

        //        #region OLD
        //        /*
        //        //v kilobytech
        //        long bufferMaxLength = ftransferService.GetMaxRequestLength();
        //        //v bytech, prozatim napevno... 1024 * 64 = 65536 bytu
        //        int bufferSize = 1024 * 256;

        //        long filesize = ftransferService.GetFileSize(MST_Global.TerminalID, fileciselnik);

        //        long offset = 0;
        //        byte[] buffer = new byte[0];
        //        int errorRepeatCount = 0;
        //        int errorRepeatCountMax = 5;

        //        fs = new FileStream(fileciselniktmp, FileMode.OpenOrCreate, FileAccess.ReadWrite);

        //        DateTime start = DateTime.Now;

        //        //ftransferService.Timeout = MST_Global.Inventura2ChunkTimeout;

        //        do
        //        {
        //            int procento = (int)(offset * 100 / (float)filesize);
        //            int downloadspeed = (int)((offset / (float)1024) / (DateTime.Now - start).TotalSeconds);
        //            DateTime end = DateTime.Now;
        //            try { end = end.AddSeconds((filesize - offset) / (float)(downloadspeed * 1024)); }
        //            catch { }
        //            TimeSpan odhad = end - DateTime.Now;
        //            if (!silent)
        //                Program.mstw.mbw.Zprava = "Stahují se data číselníku\n" + procento + "%\nRychlost:" + downloadspeed + " KB/s\nOdhad:" + odhad.Minutes + ":" + odhad.Seconds.ToString("00") + "\nChyb:" + errorRepeatCount;

        //            do
        //            {
        //                try
        //                {
        //                    buffer = ftransferService.DownloadChunk(MST_Global.TerminalID, fileciselnik, offset, (int)bufferSize);
        //                    //MySystem.FileOperations.DBSave(tmpfiledavka, ref buffer, offset);
        //                    fs.Position = offset;
        //                    fs.Write(buffer, 0, buffer.Length);

        //                    errorRepeatCount = 0;
        //                }
        //                catch (WebException webEx)
        //                {
        //                    errorRepeatCount++;
        //                    if (errorRepeatCount >= errorRepeatCountMax)
        //                        throw new Exception("Chyba při stahování dat číselníku", webEx);
        //                }
        //                catch (Exception ex)
        //                {
        //                    errorRepeatCount++;
        //                    if (errorRepeatCount >= errorRepeatCountMax)
        //                        throw new Exception("Chyba při stahování dat číselníku", ex);
        //                }

        //            } while (0 < errorRepeatCount && errorRepeatCount < errorRepeatCountMax);

        //            offset += buffer.Length;
        //            if (!silent)
        //                Program.mstw.mbw.Zprava = "Stahují se data číselníku\n" + (int)(offset * 100 / (float)filesize) + "%";
        //        } while (buffer.Length == bufferSize || offset < filesize); //pokracuje, dokud nenacte vse

        //        fs.Flush();
        //        fs.Close();
        //        fs = null;
        //        */

        //        #endregion

        //        #region new Download
                
        //        FileTransferService.FileTransfer ftransferService = new FileTransferService.FileTransfer();
        //        ftransferService.Url = Properties.Settings.Default.ServerAddress + "FileTransfer.asmx";
        //        ftransferService.Timeout = 10000;
        //        //ftransferService.UpdateWebServiceCredentials();

        //        //long filesize = ftransferService.GetFileSize(MST_Global.TerminalID, fileciselnik);


        //        int prevTick = Environment.TickCount;
        //        int errorRepeatCount = 0;
        //        int errorRepeatCountMax = 5;
        //        string path = fileciselniktmp;
        //        byte[] buffer = new byte[4096];
        //        FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);

        //        decimal lastingSeconds;

        //        string url = Properties.Settings.Default.ServerAddress + @"SqlCEDBs/" + Logging.LogConfig.MachineID + "//" + Path.GetFileName(fileciselnik);
        //        //string FilePath = Path.Combine(UploadPath, FileName);
        //        System.Net.WebRequest wr = System.Net.WebRequest.Create(url);
        //        wr.Proxy = System.Net.GlobalProxySelection.Select;
        //        wr.Timeout = 1000;
        //        long celkovoKstazeni = ftransferService.GetFileSize(byte.Parse(Logging.LogConfig.MachineID), fileciselnik);
                
        //        try
        //        {
        //            using (System.Net.WebResponse response = wr.GetResponse())
        //            {
        //                using (Stream responseStream = response.GetResponseStream())
        //                {

        //                    int count = 0;
        //                    int dataRead = 0;

        //                    do
        //                    {

        //                        do
        //                        {
        //                            try
        //                            {
        //                                count = responseStream.Read(buffer, 0, buffer.Length);
        //                                fileStream.Write(buffer, 0, count);
        //                                //int procento = (int)(dataRead * 100 / (float)celkovoKstazeni);
        //                                decimal procento = (((decimal)dataRead / (decimal)celkovoKstazeni)) * 100;


        //                                //int downloadspeed = (int)((dataRead / (float)1024) / (DateTime.Now - start).TotalSeconds);
        //                                decimal kB = count / 1024;
        //                                decimal s = (decimal)(Environment.TickCount - prevTick) / (decimal)1000;
        //                                decimal downloadspeed = kB / s;

        //                                prevTick = Environment.TickCount;

        //                                decimal speed = ((downloadspeed == 0 ? 1 : downloadspeed) * 1024);
        //                                decimal offset = (decimal)celkovoKstazeni - (decimal)dataRead;
        //                                lastingSeconds = offset / speed;

        //                                if (!silent)
        //                                    Program.mstw.mbw.Zprava = String.Format("Percent: {0:###.00}% \nRychlost: {1:#####.00}kB/s\nOdhad: {2:###.00}s", procento, downloadspeed, lastingSeconds);
        //                                    //Program.mstw.mbw.Zprava = "Stahují se data\n" + procento + "%\nRychlost:" + downloadspeed + " KB/s\nOdhad:" + lastingSeconds + "s" + "\nChyb:" + errorRepeatCount;
                                        

        //                                errorRepeatCount = 0;

        //                            }
        //                            catch (WebException webEx)
        //                            {
        //                                errorRepeatCount++;
        //                                if (errorRepeatCount >= errorRepeatCountMax)
        //                                    throw new Exception("Chyba při stahování dat", webEx);
        //                            }
        //                            catch (Exception ex)
        //                            {
        //                                errorRepeatCount++;
        //                                if (errorRepeatCount >= errorRepeatCountMax)
        //                                    throw new Exception("Chyba při stahování dat", ex);
        //                            }

        //                        }
        //                        //while (count != 0 && errorRepeatCount < errorRepeatCountMax);
        //                        while (0 < errorRepeatCount && errorRepeatCount < errorRepeatCountMax);


        //                        dataRead += count;
                                

        //                    } while (count != 0); //pokracuje, dokud nenacte vse
        //                }
        //            }
        //        }
        //        catch (System.Net.WebException wex)
        //        {
        //            throw (wex);
        //        }
        //        finally
        //        {
        //            if (fileStream != null)
        //            {
        //                fileStream.Flush();
        //                fileStream.Close();
        //                fileStream = null;
        //            }
        //        }
        //        #endregion


        //        #region Hash check
        //        //Program.mstw.mbw.Zprava = "Kontrola stažených dat dávky inventury";
        //        //IAsyncResult ares = ftransferService.BeginCheckFileHash(MST_Global.TerminalID, davkafilename, null, null);
        //        //string LocalFileHash = MySystem.FileOperations.CheckFileHash(tmpfiledavka);
        //        //ares.AsyncWaitHandle.WaitOne();
        //        //string ServerFileHash = ftransferService.EndCheckFileHash(ares);
        //        //if (LocalFileHash != ServerFileHash)
        //        //    throw new Exception("MD5 hash check failed!");
        //        #endregion

        //        #endregion


        //        try
        //        {
        //            //MySystem.FileOperations.DBSave(Path.Combine(Main.DataDir, davka.ToString() + "." + Main.Inventura1I1), ref dataDavka);
        //            //potvrzeni prijeti davky se povedlo => odstranit priponu tmp souboru
        //            if (overwrite)
        //            {
        //                File.Delete(fileciselnik); //odmazani stareho
        //                File.Move(fileciselniktmp, fileciselnik); //nahrani noveho ...
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Nepodařilo se uložit číselník.\n" + ex.Message);
        //        }
                
        //        //reindexace se udela az v nadrazenem ...

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        if (fs != null)
        //        {
        //            try { fs.Close(); }
        //            catch { }
        //        }
        //        File.Delete(fileciselniktmp);
        //        Logging.Log.Write(ex);
        //        if (!silent)
        //            MessageBoxBig.Show(ex.Message, "FileTransfer", System.Windows.Forms.MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
        //        return false;
        //    }
        //    finally
        //    {
        //    }
        //    #endregion
        //}


        /// <summary>
        /// Stahnuti dat ze serveru.
        /// </summary>
        /// <param name="fileciselnik">cela cesta k souboru jake ma sa ulozit</param>
        /// <param name="silent">Zobrazeni progressu (true tak je ticho, false tak se zobrazuje progress)</param>
        /// <param name="overwrite">Prepsat soubor nove stazenym (True -> prepsat, False -> nechat puvodni i Tmp)</param>
        /// <returns></returns>
        //public static bool DownloadFileFromServer_Prodej(string fileciselnik, bool silent, bool overwrite, Fask.MST_W.Prijem_4.PrijemServiceOperations pso)
        public static bool DownloadFileFromServer(string fileciselnik, bool silent, bool overwrite, Object pso)
        {
            string fileciselniktmp = fileciselnik + ".tmp";

            #region Downloading code
            FileStream fs = null;
            try
            {
                if (File.Exists(fileciselniktmp))
                    File.Delete(fileciselniktmp);

                #region Chunked download code

                #region OLD
                /*
                //v kilobytech
                long bufferMaxLength = ftransferService.GetMaxRequestLength();
                //v bytech, prozatim napevno... 1024 * 64 = 65536 bytu
                int bufferSize = 1024 * 256;

                long filesize = ftransferService.GetFileSize(MST_Global.TerminalID, fileciselnik);

                long offset = 0;
                byte[] buffer = new byte[0];
                int errorRepeatCount = 0;
                int errorRepeatCountMax = 5;

                fs = new FileStream(fileciselniktmp, FileMode.OpenOrCreate, FileAccess.ReadWrite);

                DateTime start = DateTime.Now;

                //ftransferService.Timeout = MST_Global.Inventura2ChunkTimeout;

                do
                {
                    int procento = (int)(offset * 100 / (float)filesize);
                    int downloadspeed = (int)((offset / (float)1024) / (DateTime.Now - start).TotalSeconds);
                    DateTime end = DateTime.Now;
                    try { end = end.AddSeconds((filesize - offset) / (float)(downloadspeed * 1024)); }
                    catch { }
                    TimeSpan odhad = end - DateTime.Now;
                    if (!silent)
                        Program.mstw.mbw.Zprava = "Stahují se data číselníku\n" + procento + "%\nRychlost:" + downloadspeed + " KB/s\nOdhad:" + odhad.Minutes + ":" + odhad.Seconds.ToString("00") + "\nChyb:" + errorRepeatCount;

                    do
                    {
                        try
                        {
                            buffer = ftransferService.DownloadChunk(MST_Global.TerminalID, fileciselnik, offset, (int)bufferSize);
                            //MySystem.FileOperations.DBSave(tmpfiledavka, ref buffer, offset);
                            fs.Position = offset;
                            fs.Write(buffer, 0, buffer.Length);

                            errorRepeatCount = 0;
                        }
                        catch (WebException webEx)
                        {
                            errorRepeatCount++;
                            if (errorRepeatCount >= errorRepeatCountMax)
                                throw new Exception("Chyba při stahování dat číselníku", webEx);
                        }
                        catch (Exception ex)
                        {
                            errorRepeatCount++;
                            if (errorRepeatCount >= errorRepeatCountMax)
                                throw new Exception("Chyba při stahování dat číselníku", ex);
                        }

                    } while (0 < errorRepeatCount && errorRepeatCount < errorRepeatCountMax);

                    offset += buffer.Length;
                    if (!silent)
                        Program.mstw.mbw.Zprava = "Stahují se data číselníku\n" + (int)(offset * 100 / (float)filesize) + "%";
                } while (buffer.Length == bufferSize || offset < filesize); //pokracuje, dokud nenacte vse

                fs.Flush();
                fs.Close();
                fs = null;
                */

                #endregion

                #region new Download

                //FileTransferService.FileTransfer ftransferService = new Fask.MST_W.FileTransferService.FileTransfer();
                //ftransferService.Url = MST_Global.ServerAddress + "FileTransfer.asmx";
                //ftransferService.Timeout = 10000;
                //ftransferService.UpdateWebServiceCredentials();

                //long filesize = ftransferService.GetFileSize(MST_Global.TerminalID, fileciselnik);


                int prevTick = Environment.TickCount;
                int errorRepeatCount = 0;
                int errorRepeatCountMax = 5;
                string path = fileciselniktmp;
                byte[] buffer = new byte[4096];
                FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);

                decimal lastingSeconds;

                string url = Logging.LogConfig.KomServer + @"SqlCEDBs/" + Logging.LogConfig.TerminalID + "//" + Path.GetFileName(fileciselnik);
                //string FilePath = Path.Combine(UploadPath, FileName);
                System.Net.WebRequest wr = System.Net.WebRequest.Create(url);
                wr.Proxy = System.Net.GlobalProxySelection.Select;
                wr.Timeout = 1000;
                long filesize = 0;// ftransferService.GetFileSize(MST_Global.TerminalID, fileciselnik);

                try
                {
                    using (System.Net.WebResponse response = wr.GetResponse())
                    {
                        filesize = response.ContentLength;
                        using (Stream responseStream = response.GetResponseStream())
                        {

                            int count = 0;
                            int dataRead = 0;

                            do
                            {
                                do
                                {
                                    try
                                    {
                                        count = responseStream.Read(buffer, 0, buffer.Length);
                                        fileStream.Write(buffer, 0, count);
                                        //int procento = (int)(dataRead * 100 / (float)celkovoKstazeni);
                                        decimal procento = (((decimal)dataRead / (decimal)filesize)) * 100;


                                        //int downloadspeed = (int)((dataRead / (float)1024) / (DateTime.Now - start).TotalSeconds);
                                        decimal kB = count / 1024;
                                        decimal s = (decimal)(Environment.TickCount - prevTick) / (decimal)1000;
                                        decimal downloadspeed = kB / s;

                                        prevTick = Environment.TickCount;

                                        decimal speed = ((downloadspeed == 0 ? 1 : downloadspeed) * 1024);
                                        decimal offset = (decimal)filesize - (decimal)dataRead;
                                        lastingSeconds = offset / speed;

                                        if (!silent)
                                        {
                                            //if (pso != null && pso is PrijemServiceOperations)
                                            //{
                                            //    Fask.MST_W.Prijem_4.PrijemServiceOperations _pso = (Fask.MST_W.Prijem_4.PrijemServiceOperations)pso;
                                            //    _pso.WriteDesc(String.Format("Percent: {0:###.00}% \nRychlost: {1:#####.00}kB/s\nOdhad: {2:###.00}s", procento, downloadspeed, lastingSeconds));
                                            //}
                                            //if (pso != null && pso is Fask.MST_W.ServisModule.ServisServiceOperations)
                                            //{
                                            //    Fask.MST_W.ServisModule.ServisServiceOperations _pso = (Fask.MST_W.ServisModule.ServisServiceOperations)pso;
                                            //    _pso.WriteDesc(String.Format("Percent: {0:###.00}% \nRychlost: {1:#####.00}kB/s\nOdhad: {2:###.00}s", procento, downloadspeed, lastingSeconds));
                                            //}
                                            //else
                                            //{
                                            //    Program.mstw.mbw.Zprava = String.Format("Percent: {0:###.00}% \nRychlost: {1:#####.00}kB/s\nOdhad: {2:###.00}s", procento, downloadspeed, lastingSeconds);
                                            //}

                                            
                                        }
                                        errorRepeatCount = 0;

                                    }
                                    catch (WebException webEx)
                                    {
                                        errorRepeatCount++;
                                        if (errorRepeatCount >= errorRepeatCountMax)
                                            throw new Exception("Chyba při stahování dat", webEx);
                                    }
                                    catch (Exception ex)
                                    {
                                        errorRepeatCount++;
                                        if (errorRepeatCount >= errorRepeatCountMax)
                                            throw new Exception("Chyba při stahování dat", ex);
                                    }

                                }
                                //while (count != 0 && errorRepeatCount < errorRepeatCountMax);
                                while (0 < errorRepeatCount && errorRepeatCount < errorRepeatCountMax);


                                dataRead += count;


                            } while (count != 0); //pokracuje, dokud nenacte vse
                        }
                    }
                }
                catch (System.Net.WebException wex)
                {
                    throw (wex);
                }
                finally
                {
                    if (fileStream != null)
                    {
                        fileStream.Flush();
                        fileStream.Close();
                        fileStream = null;
                    }
                }
                #endregion


                #region Hash check
                //Program.mstw.mbw.Zprava = "Kontrola stažených dat dávky inventury";
                //IAsyncResult ares = ftransferService.BeginCheckFileHash(MST_Global.TerminalID, davkafilename, null, null);
                //string LocalFileHash = MySystem.FileOperations.CheckFileHash(tmpfiledavka);
                //ares.AsyncWaitHandle.WaitOne();
                //string ServerFileHash = ftransferService.EndCheckFileHash(ares);
                //if (LocalFileHash != ServerFileHash)
                //    throw new Exception("MD5 hash check failed!");
                #endregion

                #endregion


                try
                {
                    //MySystem.FileOperations.DBSave(Path.Combine(Main.DataDir, davka.ToString() + "." + Main.Inventura1I1), ref dataDavka);
                    //potvrzeni prijeti davky se povedlo => odstranit priponu tmp souboru
                    if (overwrite)
                    {
                        File.Delete(fileciselnik); //odmazani stareho
                        File.Move(fileciselniktmp, fileciselnik); //nahrani noveho ...
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Nepodařilo se uložit číselník.\n" + ex.Message);
                }

                //reindexace se udela az v nadrazenem ...

                return true;
            }
            catch (Exception ex)
            {
                if (fs != null)
                {
                    try { fs.Close(); }
                    catch { }
                }
                File.Delete(fileciselniktmp);
                ErrorLog.Log.Write(ex);
                if (!silent)
                    FlexibleMessageBox.Show(ex.Message, "FileTransfer", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return false;
            }
            finally
            {
            }
            #endregion
        }

        protected delegate void VoidDelegate();


    }
}
