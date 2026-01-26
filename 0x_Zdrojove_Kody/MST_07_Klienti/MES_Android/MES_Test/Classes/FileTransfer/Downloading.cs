using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;


//using System.IO;
using System.Net;
using System.Data;
using System.Threading.Tasks;



using System.Diagnostics;
using Android.Util;
//using Java.IO;
using Java.Util.Zip;
using System.IO;

namespace MES_Android
{
    public class Downloading
    {

        public static bool DownloadFileFromServer(string fileciselnik)
        {
            return DownloadFileFromServer(null, fileciselnik, false, false);
        }

        public static bool DownloadFileFromServer(string fileciselnik, bool unZip)
        {
            return DownloadFileFromServer(null, fileciselnik, false, unZip);
        }

        public static bool DownloadFileFromServer(Android.Support.V7.App.AppCompatActivity activity, string fileciselnik, bool silent = false, bool unZip = false)
        {

            if (System.IO.File.Exists(fileciselnik))
                System.IO.File.Delete(fileciselnik);


            System.IO.FileStream fileStream = new System.IO.FileStream(fileciselnik, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite);

            try
            {

                byte[] buffer = new byte[4096];

                string url = Config.Settings.Adresa + @"SQLiteDBs/" + Config.Settings.TerminalID.ToString() + @"/" + Path.GetFileName(fileciselnik);

                WebRequest wr = WebRequest.Create(url);

                wr.Proxy = WebRequest.DefaultWebProxy;

                //wr.Proxy = GlobalProxySelection.Select;

                wr.Timeout = Config.Settings.TimeOut;

                long filesize = 0;
                int prevTick = System.Environment.TickCount;
                decimal lastingSeconds;
                //Stopwatch stopWatch = new Stopwatch();
                //stopWatch.Start();

                using (WebResponse response = wr.GetResponse())
                {
                    filesize = response.ContentLength;
                    using (System.IO.Stream responseStream = response.GetResponseStream())
                    {

                        int count = 0;
                        int dataRead = 0;
                        do
                        {
                            count = responseStream.Read(buffer, 0, buffer.Length);
                            fileStream.Write(buffer, 0, count);
                            //if (p != null)
                            //    p.Progress = (int)(((float)dataRead / (float)response.ContentLength) * 100.0f);
                            #region pocitani rychlosti

                            decimal procento = (((decimal)dataRead / (decimal)filesize)) * 100;

                            decimal kB = count / 1024;
                            decimal s = (decimal)(System.Environment.TickCount - prevTick) / (decimal)1000;

                            if (s == 0)
                                s = 1;

                            decimal downloadspeed = kB / s;

                            prevTick = System.Environment.TickCount;

                            decimal speed = ((downloadspeed == 0 ? 1 : downloadspeed) * 1024);
                            decimal offset = (decimal)filesize - (decimal)dataRead;

                            if (speed == 0)
                                speed = 1;

                            lastingSeconds = offset / speed;

                            if (activity != null)
                            {
                                if(activity is SplashActivity)
                                {
                                    SplashActivity x = activity as SplashActivity;
                                    x.SetText(String.Format("Percent: {0:000.00}% \nRychlost: {1:####0.00}kB/s\nOdhad: {2:###0.00}s", procento, downloadspeed, lastingSeconds));
                                }
                            }
                            else
                            Classes.ProgressDialog_Infinity.Message = String.Format("Percent: {0:000.00}% \nRychlost: {1:####0.00}kB/s\nOdhad: {2:###0.00}s", procento, downloadspeed, lastingSeconds);

                         
                            #endregion


                            dataRead += count;
                        } while (count != 0);
                    }
                }

                if (activity != null)
                {
                    if (activity is SplashActivity)
                    {
                        SplashActivity x = activity as SplashActivity;
                        x.SetText("Hotovo");
                    }
                }
                else
                    Classes.ProgressDialog_Infinity.Message = "Hotovo";
                //stopWatch.Stop();
                //TimeSpan ts = stopWatch.Elapsed;
            }
            catch (WebException wex)
            {
                if (fileStream != null)
                    fileStream.Close();
                //Logging.Log.Write((Exception)wex);
                if (System.IO.File.Exists(fileciselnik))
                    System.IO.File.Delete(fileciselnik);
                throw (wex);
            }
            finally
            {

                try
                {
                    if (fileStream != null)
                        fileStream.Close();
                }
                catch (Exception ex)
                {
                    string err = ex.Message;
                }

            }



            return true;
        }


    }
}