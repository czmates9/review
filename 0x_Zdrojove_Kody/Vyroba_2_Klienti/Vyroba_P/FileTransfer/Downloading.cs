using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace Fask.Vyroba_P.FileTransfer
{
    public class Downloading
    {
        /// <summary>
        /// Stahnuti dat ze serveru po novem 18.6.2018
        /// </summary>
        /// <param name="fileciselnik">cela cesta k souboru jake ma sa ulozit</param>
        /// <param name="silent">Zobrazeni progressu (true tak je ticho, false tak se zobrazuje progress)</param>
        /// <param name="overwrite">Prepsat soubor nove stazenym (True -> prepsat, False -> nechat puvodni i Tmp)</param>
        /// <returns></returns>
        //public static bool DownloadFileFromServer_Prodej(string fileciselnik, bool silent, bool overwrite, Fask.MST_W.Prijem_4.PrijemServiceOperations pso)
        public static string DownloadFileFromServer(string fileciselnik, bool silent, bool overwrite, Fask.Vyroba_P.Forms.updateProgressBarDownloadDelegate callback )
        {

            string fileciselnik_TMP = fileciselnik + Constants.TMP;
            string fileciselnik_ZIP = fileciselnik_TMP + Constants.ZIP;

            #region Downloading code

            FileStream fs = null;
            try
            {
                if (File.Exists(fileciselnik_ZIP))
                    File.Delete(fileciselnik_ZIP);

                #region Chunked download code


                #region new Download

                int prevTick = Environment.TickCount;
                int errorRepeatCount = 0;
                int errorRepeatCountMax = 5;
                //string path = fileciselnik_ZIP;
                byte[] buffer = new byte[4096];
                FileStream fileStream = new FileStream(fileciselnik_ZIP, FileMode.Create, FileAccess.ReadWrite);

                decimal lastingSeconds;

                string url = Settings.WebServiceAddressVyroba + Constants.SQLiteDBs + Settings.TerminalID.ToString() + Constants.Slash + Path.GetFileName(fileciselnik_ZIP);
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

                        if (filesize == 0)
                            return "Velkost souboru je 0";

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

                                        decimal tmpperc = (decimal)dataRead / (decimal)filesize;
                                        int procento = (int)(tmpperc * 100);

                                        decimal kB = count / 1024;
                                        decimal s = (decimal)(Environment.TickCount - prevTick) / (decimal)1000;

                                        decimal downloadspeed = kB / (s == 0 ? 1 : s);

                                        prevTick = Environment.TickCount;

                                        decimal speed = ((downloadspeed == 0 ? 1 : downloadspeed) * 1024);
                                        decimal offset = (decimal)filesize - (decimal)dataRead;
                                        lastingSeconds = offset / (speed == 0 ? 1 : speed);

                                        if (!silent)
                                        {
                                            callback(procento, String.Format("Aktualizace:P:{0:000}%,R:{1:0000}[KB/s],O:{2:0000}s", procento, downloadspeed, lastingSeconds));
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
                    //potvrzeni prijeti davky se povedlo => odstranit priponu tmp souboru
                    if (overwrite)
                    {
                        File.Delete(fileciselnik_TMP); //odmazani stareho
                        FileTransfer.CompressFile.DeCompressFromZip(fileciselnik_ZIP, fileciselnik_TMP);

                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Nepodařilo se uložit číselník.\n" + ex.Message);
                }
                finally
                {
                    File.Delete(fileciselnik_ZIP);
                }

                //reindexace se udela az v nadrazenem ...

                return "OK";
            }
            catch (Exception ex)
            {
                if (fs != null)
                {
                    try { fs.Close(); }
                    catch { }
                }

                if (File.Exists(fileciselnik_ZIP))
                    File.Delete(fileciselnik_ZIP);

                if (File.Exists(fileciselnik_TMP))
                    File.Delete(fileciselnik_TMP);


          
                Fask.Logging.ExceptionHandler2.Handle("Downloading", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                return ex.Message;
            }
            finally
            {

            }
            #endregion
        }

    }
}
