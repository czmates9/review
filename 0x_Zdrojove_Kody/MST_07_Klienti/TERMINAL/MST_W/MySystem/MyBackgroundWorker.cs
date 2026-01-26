#define ENABLED_INFO

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;


namespace Fask.MST_W.MySystem
{
    public class MyBackgroundWorker
    {
        public static Object processingLocker = new object();

        private Thread backgroundWorkerThread = null;
        public static bool processing = false;
        private Forms.PracujiForm2 pracujiForm2 = null;


        //OpenNETCF.ComponentModel.BackgroundWorker bw;
        //public delegate void StartWait();
        //public event StartWait StartWaitEvent;


        //public static bool Processing
        //{
        //    get
        //    {
        //        lock (processingLocker)
        //        {
        //            return processing;
        //        }
        //    }
        //}

        private string zprava;
        public string Zprava
        {
            get { return zprava; }
            set
            {
                zprava = value;
                if (pracujiForm2 != null && !pracujiForm2.IsDisposed)
                    pracujiForm2.Zprava = zprava;
            }
        }



        //private void startsait()
        //{
        //ThreadPool.QueueUserWorkItem(new WaitCallback(AsyncShowForm));
        //}


        public void BeginPracujiForm()
        {
            this.zprava = string.Empty;
#if ENABLED_INFO

            lock (processingLocker)
            {
                processing = true;
            }

            //bw = new OpenNETCF.ComponentModel.BackgroundWorker();

            //bw.DoWork += new OpenNETCF.ComponentModel.DoWorkEventHandler(bw_DoWork);
            //bw.RunWorkerAsync();

            //StartWaitEvent += new StartWait(startsait);
            //if (StartWaitEvent != null)
            //{
            //    StartWaitEvent();
            //}

            //ThreadPool.QueueUserWorkItem(new WaitCallback(AsyncShowForm));
            //ThreadPool.QueueUserWorkItem(AsyncShowForm);

            backgroundWorkerThread = new Thread(new ThreadStart(AsyncShowForm));
            backgroundWorkerThread.Start();
#endif
        }
        public void BeginPracujiForm(string zprava)
        {   
            this.zprava = zprava;
#if ENABLED_INFO
            lock (processingLocker)
            {
                processing = true;
            }

            //bw = new OpenNETCF.ComponentModel.BackgroundWorker();

            //bw.DoWork += new OpenNETCF.ComponentModel.DoWorkEventHandler(bw_DoWork);

            //bw.RunWorkerAsync();
            //ThreadPool.QueueUserWorkItem(new WaitCallback(AsyncShowForm));
            //ThreadPool.QueueUserWorkItem(AsyncShowForm);
            //StartWaitEvent += new StartWait(startsait);
            //if (StartWaitEvent != null)
            //{
            //    StartWaitEvent();
            //}
            backgroundWorkerThread = new Thread(new ThreadStart(AsyncShowForm));
            backgroundWorkerThread.Start();

            
#endif
        }

        //private void bw_DoWork(object sender, OpenNETCF.ComponentModel.DoWorkEventArgs e) 
        //{

        //    pracujiForm2 = new Forms.PracujiForm2();
        //    System.Windows.Forms.Application.Run(pracujiForm2);
        //}

        private void AsyncShowForm()
        {
            try
            {
                    //Thread.Sleep(5000);
                if ((pracujiForm2 == null) || ((pracujiForm2 != null) && (pracujiForm2.IsDisposed)))
                {
                    pracujiForm2 = new Forms.PracujiForm2();
                    System.Windows.Forms.Application.Run(pracujiForm2);
                }

                //using (pracujiForm2 = new Forms.PracujiForm2())
                //{
                //    if (this.zprava != string.Empty)
                //        pracujiForm2.Zprava = zprava;
                //    if (pracujiForm2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                //    {
                //    }
                //}
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
        }

        public void EndPracujiForm()
        {
            lock (processingLocker)
            {
                processing = false;                
            }
            //if ((pracujiForm2 != null) && (!pracujiForm2.IsDisposed))
            //    pracujiForm2.CloseForm();

            //bw.CancelAsync();

        }

    }
}
