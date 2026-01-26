using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Fask.MST_W.Forms;
using System.IO;
using Fask.MST_W.ServerAccess;
using System.Net;
using System.Linq;

namespace Fask.MST_W.Prodej_3
{
    public partial class ProdejServiceOperations : Fask.MST_W.Forms.ServiceOperationsForm
    {
        public enum Operation
        {
            SendData
        }

        private Operation operation;
        private _WebRefernces_Globals.ProdejServiceSession pservice = null;
        private int davka = -1; //Davka

        private ProdejServiceOperations()
        {
            InitializeComponent();

            this.Text = MST_Global.ProdejName;
        }

        private ProdejServiceOperations(_WebRefernces_Globals.ProdejServiceSession pservice, Operation operation)
            : this()
        {
            this.pservice = pservice;
            this.operation = operation;
        }

        public static bool SendData(_WebRefernces_Globals.ProdejServiceSession pservice, int davka)
        {
  try
            {
            using (ProdejServiceOperations pso = new ProdejServiceOperations(pservice, Operation.SendData))
            {
                pso.Description = "Odesílají se data dávky è." + davka.ToString();
                pso.davka = davka;
				if (pso.ShowDialog() == DialogResult.Cancel)
					return false;
				else
				{

					return true;
				}
            }
          }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        private void PrijemServiceOperations_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            this.Size = Forms.FormLocation.ScreenResolution;

			// check : invoke nebo timer ?

			this.BeginInvoke((System.Threading.ThreadStart)delegate()
			{
				ProcessRequest();
			});

			// zde spustit timer (1x vyvolat)
			//System.Threading.Timer timer = new System.Threading.Timer(new System.Threading.TimerCallback(ProcessRequest), null, 1000, System.Threading.Timeout.Infinite);
        }

		//private void ProcessRequest(object o)
		//{
		//    this.BeginInvoke((MethodInvoker)delegate()
		//    {
		//        ProcessRequest();
		//    });
		//}

		private void ProcessRequest()
		{
			//System.Threading.Thread.Sleep(100);

			//byte[] data = null;
			string filename = string.Empty;

			try
			{
				switch (operation)
				{
					//case Operation.SendData:
					//    data = MySystem.FileOperations.DBLoad(Path.Combine(Main.StorageDir, davka.ToString() + "." + Main.ProdejOExt));
					//    aresult = pservice.BeginProcessProdejDB(this.davka, MST_Global.TerminalID, MST_Global.UserID, MST_Global.UserLoginName, data, new AsyncCallback(ProcessProdejEnd), (object)this.davka);
					//    break;
					case Operation.SendData:
						try
						{
							filename = Path.Combine(Main.StorageDir, davka.ToString() + "." + Main.Ext_Prodej);
							string filenameZip = filename + ".zip";
							string KamNaServer = MST_Global.TerminalID + "\\" + Path.GetFileName(filenameZip);

							Fask.MST_W.FileTransfer.CompressFile.CompressToZip(filename, filenameZip);

							if (Fask.MST_W.FileTransfer.Uploading.SendFile_API(filenameZip, KamNaServer, Fask.MST_W.FileTransfer.Uploading.Co.Prodej) != "OK") { throw new Exception("davku se nepodarilo odeslat"); };
							//if (Fask.MST_W.FileTransfer.Uploading.SendFileCalcTime(MST_Global.ServerAddress + "Upload.aspx", filename + ".zip", MST_Global.TerminalID + "\\" + Path.GetFileName(filename) + ".zip", Fask.MST_W.FileTransfer.Uploading.Co.Prodej) != "OK") { throw new Exception("davku se nepodarilo odeslat"); };

							

							//aresult = pservice.BeginProcessPrijemDBFile2(this.davka, MST_Global.TerminalID, Fask.MST_W.PrijemService.ProcessPrijemState.Zpracovat, new AsyncCallback(ProcessPrijemEnd), (object)davka);
							aresult = pservice.BeginProcessProdejDB2(this.davka, MST_Global.TerminalID, MST_Global.UserID, MST_Global.UserLoginName, new AsyncCallback(ProcessProdejEnd), (object)this.davka);

							//while (!aresult.IsCompleted )
							//{
							//    aresult.AsyncWaitHandle.WaitOne(pservice.Timeout);
							//    System.Threading.Thread.Sleep(1000);
							//}

							//aresult = pservice.BeginProcessProdejDB2(this.davka, MST_Global.TerminalID, MST_Global.UserID, MST_Global.UserLoginName, null, (object)this.davka);
							//aresult.AsyncWaitHandle.WaitOne(pservice.Timeout, false);
							//this.BeginInvoke((System.Threading.ThreadStart)delegate()
							//{
							//    ProcessProdejEnd(aresult);
							//});
						}
						catch (Exception ex)
						{
							throw ex;
						}
						break;
					default:
						this.BeginInvoke(new VoidDelegate(PerformCancel));
						break;
				}

			}
			catch (Exception ex)
			{
				Logging.Log.Write(ex);
				MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
			}
		}

        private void ProcessProdejEnd(IAsyncResult ares)
        {
            string filename = Path.Combine(Main.StorageDir, davka.ToString() + "." + Main.Ext_Prodej);

            try
            {
                if (pservice == null) //jestlize je pservice null, pak doslo k abortu, a znamena to ze nema dal nic resit
                {
                    return;
                }

                ProdejService.StatusObject so = pservice.EndProcessProdejDB2(ares);
                if (so.StatusText == "OK" && !so.Exception)
                {
                    //string filename = Path.Combine(Main.StorageDir, davka.ToString() + ".prodej");
                    //File.Delete(Path.ChangeExtension(filename, Main.ProdejOExt));
                    
                    File.Delete(filename);
                    File.Delete(filename + ".zip");
                }
                else
                {
                    //MessageBoxBig.Show("Nastala chyba pøi odesílání dat: " + so.StatusText);
                    //throw new Exception("Nepodaøilo se odeslat data");
                    MessageBoxBig.Show("Nastala chyba pøi odesílání dat: " + so.StatusText);
                    //string filename = Path.Combine(Main.StorageDir, davka.ToString() + "." + Main.ProdejOExt);
                    File.Delete(filename + ".zip");

                    throw new Exception("Nepodaøilo se odeslat data");
                }
                this.BeginInvoke(new VoidDelegate(PerformOK));
            }
            catch (Exception ex)
            {
                MessageBoxBig.Show(ex.Message);

                try
                {
                    File.Delete(filename + ".zip");
                }
                catch
                {}
                
                this.BeginInvoke(new VoidDelegate(PerformCancel));
            }
        }

      

        protected override void PerformCancel()
        {
            try
            {
                if (pservice != null)
                    pservice.Abort();


                pservice = null;
            }
            catch
            {
            }
            base.PerformCancel();
        }

    }
}

