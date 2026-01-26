using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace FASK.MST_WINDOWS.Main.Configuration.Update
{
    public class TransferManager
    {
        public delegate void TransferProgress(int progress);

        private TransferProgress transferProgressDelegate;
        private volatile bool abortTransfer = false;

        public TransferManager()
        {
        }

        void Notification_AbortUpdateEvent()
        {
            this.abortTransfer = true;
        }

        public bool downloadFile(String url, out Stream s, String path, TransferProgress del)
        {
            this.transferProgressDelegate = del;
            byte[] buffer = new byte[4096];
            FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);

            WebRequest wr = WebRequest.Create(url);
            wr.Proxy = System.Net.GlobalProxySelection.Select;
            wr.Timeout = 1000;
            try
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();

                using (WebResponse response = wr.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {

                        int count = 0;
                        int dataRead = 0;
                        do
                        {
                            count = responseStream.Read(buffer, 0, buffer.Length);
                            fileStream.Write(buffer, 0, count);

                            float progress = ((float)dataRead / (float)response.ContentLength) * 100.0f;
                            OnProgress((int)progress);

                            dataRead += count;
                        } while (count != 0 && !abortTransfer);
                    }
                }
                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;
            }
            catch (WebException wex)
            {

                //Logging.Log.Write(wex, "No Connection to upgrade server...");
                fileStream.Close();
                s = null;
                throw (wex);
            }
            s = fileStream;
            return true;
        }

        protected void OnProgress(int progress)
        {
            if (transferProgressDelegate != null)
            {
                transferProgressDelegate(progress);
            }
        }
    }
}
