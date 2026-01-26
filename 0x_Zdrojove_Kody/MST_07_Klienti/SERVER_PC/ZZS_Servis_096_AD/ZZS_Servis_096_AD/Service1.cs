using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace ZZS_Servis_096_AD
{
    [System.ComponentModel.DesignerCategory("")]
    public partial class Service1 : ServiceBase
    {
        private System.Threading.Timer timer = null;

        BackgroundWorker bw = null;

        public Service1()
        {
            InitializeComponent();
            bw = new BackgroundWorker();
            bw.WorkerReportsProgress = false;
            bw.WorkerSupportsCancellation = true;

            bw.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bw_DoWork);
            bw.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bw_RunWorkerCompleted);
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                Log.Write(this.ServiceName + " Starting");
                //data = new global::MST_Print_Server_Service.Print.Tisk();
                //data.Timeout = Properties.Settings.Default.MST_Print_Server_Timeout;
                timer = new Timer(new TimerCallback(WorkerFunction));
                timer.Change(2000, Properties.Settings.Default.Period);
                Log.Enable = true;
                Log.Write("Parameters:");
                //Log.Write("Print server address : " + Properties.Settings.Default.MST_Print_Server_Service_Print_Tisk);
                Log.Write("Period: " + TimeSpan.FromMilliseconds(Properties.Settings.Default.Period).ToString());
                Log.Write("Log: " + Properties.Settings.Default.Log);
                Log.Enable = Properties.Settings.Default.Log;
                Log.Write(this.ServiceName + " Started");
            }
            catch (Exception ex)
            {
                Log.Write(this.ServiceName + " Error : " + ex.Message);
                this.Stop();
            }
        }

        protected override void OnStop()
        {
            try
            {
                Log.Write(this.ServiceName + " Stopping");
                timer.Change(Timeout.Infinite, Timeout.Infinite);
                Log.Write(this.ServiceName + " Stoppped");
            }
            catch (Exception ex)
            {
                Log.Write(this.ServiceName + " Error : " + ex.Message);
            }

        }

        protected override void OnShutdown()
        {
            try
            {
                Log.Write(this.ServiceName + " System shutting down");
                this.Stop();
            }
            catch (Exception ex)
            {
                Log.Write(this.ServiceName + " Error : " + ex.Message);
            }
        }



        private void WorkerFunction(Object stateInfo)
        {
            try
            {
                Log.Write(this.ServiceName + " Working begin");

                timer.Change(Timeout.Infinite, Timeout.Infinite);

                DateTime now = DateTime.Now;

                //TODO co delat...

                if ((now.Minute == Properties.Settings.Default.TimeSyncMIN) && (now.Hour == Properties.Settings.Default.TimeSyncHO))
                {

                    if (bw.IsBusy)
                    {
                        bw.CancelAsync();
                        while (bw.IsBusy)
                        {
                            System.Threading.Thread.Sleep(100);
                            //Application.DoEvents();
                            //???co??? 
                        }
                    }

                    bw.RunWorkerAsync();
                }

            }
            catch (Exception ex)
            {
                Log.Write(this.ServiceName + " Error : " + ex.Message);
            }
            finally
            {
                timer.Change(Properties.Settings.Default.Period, Properties.Settings.Default.Period);
            }
        }


        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {

                if (bw.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }


                Log.Write(this.ServiceName + " Synchronization" );

                Pracovnici.SynchronizacePracovniciAD();

                if (bw.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }


            }
            catch (Exception ex)
            {
                Log.Write(ex.Message);
            }
            finally
            {

            }
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {

                if (e.Error != null)
                {
                   

                }
                else if (e.Cancelled)
                {
                   
                }
                else
                {

                }

            }
            catch (Exception ex)
            {
                Log.Write(ex.Message);

            }
            finally
            {
                
            }
        }

    }
}
