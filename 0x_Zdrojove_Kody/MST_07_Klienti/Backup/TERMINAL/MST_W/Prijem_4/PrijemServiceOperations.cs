using System;
using System.IO;
using System.Windows.Forms;
using Fask.MST_W.Forms;

namespace Fask.MST_W.Prijem_4
{
    public partial class PrijemServiceOperations : Fask.MST_W.Forms.ServiceOperationsForm
    {
        public enum Operation
        {
            GetHeads,
            GetData,
            SendDataSave,
            SendDataRelease
        }

        private int davka = -1; //Davka

        /// <summary>
        /// Pole, ktere obsahuje stazena data
        /// </summary>
        private byte[] data = null;
        /// <summary>
        /// Pole se stazenymi daty databaze pro ulozeni
        /// </summary>
        //public byte[] Data
        //{
        //    get { return this.data; }
        //}

        private Operation operation;
        private _WebRefernces_Globals.PrijemServiceSession pservice = null;
        private PrijemService.PrijemDavky heads = null; //Hlavicky

        private PrijemServiceOperations()
        {
            InitializeComponent();
        }

        private PrijemServiceOperations(_WebRefernces_Globals.PrijemServiceSession pservice, Operation operation)
            : this()
        {
            this.pservice = pservice;
            this.operation = operation;
        }

        public static PrijemService.PrijemDavky GetHeads(_WebRefernces_Globals.PrijemServiceSession pservice)
        {
            using (PrijemServiceOperations pso = new PrijemServiceOperations(pservice, Operation.GetHeads))
            {
                pso.Description = "Stahuji seznam dávek příjemek";
                if (pso.ShowDialog() == DialogResult.Cancel)
                    return null;
                else
                    return pso.heads;
            }
        }

        public static bool GetData(_WebRefernces_Globals.PrijemServiceSession pservis, int davka)
        {
            using (PrijemServiceOperations pso = new PrijemServiceOperations(pservis, Operation.GetData))
            {
                pso.Description = "Stahuji dávku č." + davka.ToString() + " ze serveru.";
                pso.davka = davka;
                if (pso.ShowDialog() == DialogResult.Cancel)
                    return false;
                return true;
            }
        }

        public static bool SendData(_WebRefernces_Globals.PrijemServiceSession pservice, int davka, bool uvolnit)
        {
            using (PrijemServiceOperations pso = new PrijemServiceOperations(pservice, uvolnit ? Operation.SendDataRelease : Operation.SendDataSave))
            {
                pso.Description = "Odesílají se data";
                pso.davka = davka;
                if (pso.ShowDialog() == DialogResult.Cancel)
                    return false;
                else
                    return true;
            }
        }


        private void PrijemServiceOperations_Load(object sender, EventArgs e)
        {
           

            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            this.Size = Forms.FormLocation.ScreenResolution;

			this.BeginInvoke((System.Threading.ThreadStart)delegate()
			{
				ProcessRequest();
			});

        }

		private void ProcessRequest()
		{
			string filename = string.Empty;

			try
			{
				switch (operation)
				{
					case Operation.GetHeads:
						aresult = pservice.BeginGetPrijemky(
							MST_Global.TerminalID,
							//string.Empty, 
							Prijem_4.PrijemMain.prijemInstance.globalObject.sklad == null ? string.Empty : Prijem_4.PrijemMain.prijemInstance.globalObject.sklad.skl_id.Trim(),
							new AsyncCallback(PrijemkyEnd),
							null
							);
						break;
					case Operation.GetData:
						{
							//aresult = pservice.BeginGetPrijemkaDBFile(davka, MST_Global.TerminalID, new AsyncCallback(PrijemkaGetDataEnd), (object)davka);
							aresult = pservice.BeginPreparePrijemkaDB(davka, MST_Global.TerminalID, new AsyncCallback(PrijemkaGetDataEnd), (object)davka);
						}
						break;
					case Operation.SendDataRelease:
						try
						{
							filename = Path.Combine(Main.StorageDir, davka + "." + Main.Ext_Prijem);
							string filenameZip = filename + ".zip";
							string KamNaServer = MST_Global.TerminalID + "\\" + Path.GetFileName(filenameZip);

							Fask.MST_W.FileTransfer.CompressFile.CompressToZip(filename, filenameZip);

							if (Fask.MST_W.FileTransfer.Uploading.SendFile_API(filenameZip, KamNaServer, Fask.MST_W.FileTransfer.Uploading.Co.Prijem) != "OK") { throw new Exception("davku se nepodarilo odeslat"); };


							////filename = Path.Combine(Main.StorageDir, davka + "." + Main.Ext_Prijem);
							//Fask.MST_W.FileTransfer.CompressFile.CompressToZip(filename, filename + ".zip");
							//if (Fask.MST_W.FileTransfer.Uploading.SendFileCalcTime(MST_Global.ServerAddress + "Upload.aspx", filename + ".zip", MST_Global.TerminalID + "\\" + Path.GetFileName(filename) + ".zip", Fask.MST_W.FileTransfer.Uploading.Co.Prijem) != "OK") { throw new Exception("davku se nepodarilo odeslat"); };
							aresult = pservice.BeginProcessPrijemDBFile2(this.davka, MST_Global.TerminalID, Fask.MST_W.PrijemService.ProcessPrijemState.Uvolnit, new AsyncCallback(ProcessPrijemEnd), (object)davka);

						}
						catch (Exception ex)
						{
							throw ex;
						}
						break;
					case Operation.SendDataSave:
						try
						{
							filename = Path.Combine(Main.StorageDir, davka + "." + Main.Ext_Prijem);
							string filenameZip = filename + ".zip";
							string KamNaServer = MST_Global.TerminalID + "\\" + Path.GetFileName(filenameZip);

							Fask.MST_W.FileTransfer.CompressFile.CompressToZip(filename, filename + ".zip");

							if (Fask.MST_W.FileTransfer.Uploading.SendFile_API(filenameZip, KamNaServer, Fask.MST_W.FileTransfer.Uploading.Co.Prijem) != "OK") { throw new Exception("davku se nepodarilo odeslat"); };
							//if (Fask.MST_W.FileTransfer.Uploading.SendFileCalcTime(MST_Global.ServerAddress + "Upload.aspx", filename + ".zip", MST_Global.TerminalID + "\\" + Path.GetFileName(filename) + ".zip", Fask.MST_W.FileTransfer.Uploading.Co.Prijem) != "OK") { throw new Exception("davku se nepodarilo odeslat"); };
							aresult = pservice.BeginProcessPrijemDBFile2(this.davka, MST_Global.TerminalID, Fask.MST_W.PrijemService.ProcessPrijemState.Zpracovat, new AsyncCallback(ProcessPrijemEnd), (object)davka);

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
				MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
				try
				{
					this.BeginInvoke(new VoidDelegate(PerformCancel));
				}
				catch (Exception exInvoke)
				{
					Logging.Log.Write(exInvoke);
				}
			}
		}

        private void PrijemkyEnd(IAsyncResult ares)
        {
            try
            {
                heads = pservice.EndGetPrijemky(ares);
                this.BeginInvoke(new VoidDelegate(PerformOK));
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                //MessageBoxBig.Show(ex.Message); upraveno aby pri zruseni stahovani nehazelo hlasku objectdispose...
                try
                {
                    heads = null;
                    this.BeginInvoke(new VoidDelegate(PerformCancel));
                }
                catch (Exception ex2)
                {
                    Logging.Log.Write("Chyba: \n" + ex2.Message, "PrijemkyEnd");
                    //throw new Exception("Data byly odeslány, ale nezdařilo se odstranit datové soubory",ex);
                }
            }
        }

        private void PrijemkaGetDataEnd(IAsyncResult ares)
        {
            try
            {
                if(!pservice.EndPreparePrijemkaDB(ares))  // PreparePrijemkaDB(davka, MST_Global.TerminalID))
                {
                    throw new Exception("davku se nepodarilo pripravit");
                }

                FileTransfer.Routines.DownloadDecompressDelete(Path.Combine(Main.StorageDir, davka + "." + Main.Ext_Prijem));

                aresult = pservice.BeginGetPrijemkaReceived((int)ares.AsyncState, MST_Global.TerminalID, new AsyncCallback(PrijemkaGetDataReceivedEnd), ares.AsyncState);

				//9.4.2021 TaD, myslím si že tohle tady nemá byt. Protože tohle ukončí tenhle objekt pomoci DialogResault, a potom v CallBack ked se nsaží ho znovu ukončit tak to da ObjectDisposedException
                //this.BeginInvoke(new VoidDelegate(PerformOK));

            }
            catch (Exception ex)
            {
                MessageBoxBig.Show(ex.Message);
                data = null;
                try
                {
                    this.BeginInvoke(new VoidDelegate(PerformCancel));
                }
                catch (Exception exInvoke)
                {
                    Logging.Log.Write(exInvoke);
                }
            }
        }

        //public delegate void delegat_write(string a);


        public void WriteDesc(string a)
        {
            if (this.InvokeRequired)
            {
                //sa vytvori nove vlakno v kterem je zavolana znovu tato metoda
                this.BeginInvoke((VoidDelegate)delegate(){ this.WriteDesc(a); });
                return;
            }

            this.Description = a;
        }



        private void PrijemkaGetDataReceivedEnd(IAsyncResult ares)
        {
            try
            {
                bool result = pservice.EndGetPrijemkaReceived(ares);
                if (result)
                {
                    //int davka = (int)ares.AsyncState;
                    //string filename = Path.Combine(Main.StorageDir, davka.ToString() + "." + Main.PrijemIExtData);
                    //MySystem.FileOperations.DBSave(filename, ref data);
                    this.BeginInvoke(new VoidDelegate(PerformOK));
                }
                else
                {
                    this.BeginInvoke(new VoidDelegate(PerformCancel));
                }
            }
            catch(Exception ex)
            {
                //MessageBoxBig.Show(ex.Message); upraveno aby pri zruseni stahovani nehazelo hlasku objectdispose...
                try
                {
                    this.BeginInvoke(new VoidDelegate(PerformCancel));
                }
                catch (Exception ex2)
                {
                    Logging.Log.Write("Chyba: \n" + ex2.Message, "PrijemkaGetDataReceivedEnd");
                    //throw new Exception("Data byly odeslány, ale nezdařilo se odstranit datové soubory",ex);
                }
                
            }
        }

        private void ProcessPrijemEnd(IAsyncResult ares)
        {
            try
            {

                PrijemService.StatusObject so = pservice.EndProcessPrijemDBFile2(ares);

                if (so.StatusText == "OK" && !so.Exception)
                {
                    try
                    {
                        string filename = Path.Combine(Main.StorageDir, davka + "." + Main.Ext_Prijem);
                        File.Delete(filename);
                        File.Delete(filename + ".zip");

                    }
                    catch (Exception ex)
                    {
                        // TODO : Localizovat !!!
                        Logging.Log.Write("Data byla odeslány, ale nezdařilo se odstranit datové soubory: \n" + ex.Message, "ProcessPrijemEnd");
                        //throw new Exception("Data byly odeslány, ale nezdařilo se odstranit datové soubory",ex);
                    }
                }
                else
                {
                    MessageBoxBig.Show("Nastala chyba při odesílání dat: " + so.StatusText);
                    string filename = Path.Combine(Main.StorageDir, davka + "." + Main.Ext_Prijem);
                    File.Delete(filename + ".zip");

                    throw new Exception("Nepodařilo se odeslat data");
                }
                this.BeginInvoke(new VoidDelegate(PerformOK));
            }
            catch (Exception ex)
            {
                //MessageBoxBig.Show(ex.Message, "Odeslání dat", MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                Exceptions.ExceptionVizualize.Show(ex);
                try
                {
                    this.BeginInvoke(new VoidDelegate(PerformCancel));
                }
                catch (Exception exInvoke)
                {
                    Logging.Log.Write(exInvoke);
                }
            }
        }

        protected override void PerformCancel()
        {
            pservice.Abort();
            base.PerformCancel();
        }
    }
}

