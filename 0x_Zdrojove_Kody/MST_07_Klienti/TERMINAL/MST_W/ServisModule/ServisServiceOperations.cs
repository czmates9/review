using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Fask.MST_W.Forms;
using System.IO;
using Fask.Upgrade;

namespace Fask.MST_W.ServisModule
{
    public partial class ServisServiceOperations : Fask.MST_W.Forms.ServiceOperationsForm
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
        private _WebRefernces_Globals.ServisModuleWServiceSession sservice = null;
        private Fask.MST_W.ServisModuleWService.ServisDavky heads = null; //Hlavicky

        private ServisServiceOperations()
        {
            InitializeComponent();
        }

        private ServisServiceOperations(_WebRefernces_Globals.ServisModuleWServiceSession sservice, Operation operation)
            : this()
        {
            this.sservice = sservice;
            this.operation = operation;
        }

        public static ServisModuleWService.ServisDavky GetHeads(_WebRefernces_Globals.ServisModuleWServiceSession sservice)
        {
            using (ServisServiceOperations pso = new ServisServiceOperations(sservice, Operation.GetHeads))
            {
                pso.Description = "Stahuji seznam dávek";
                if (pso.ShowDialog() == DialogResult.Cancel)
                    return null;
                else
                    return pso.heads;
            }
        }

        public static bool GetData(_WebRefernces_Globals.ServisModuleWServiceSession sservis, int davka)
        {
            using (ServisServiceOperations pso = new ServisServiceOperations(sservis, Operation.GetData))
            {
                pso.Description = "Stahuji dávku č." + davka.ToString() + " ze serveru.";
                pso.davka = davka;
                if (pso.ShowDialog() == DialogResult.Cancel)
                    return false;
                return true;
            }
        }

        public static bool SendData(_WebRefernces_Globals.ServisModuleWServiceSession sservice, int davka, bool uvolnit)
        {
            using (ServisServiceOperations pso = new ServisServiceOperations(sservice, uvolnit ? Operation.SendDataRelease : Operation.SendDataSave))
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
						aresult = sservice.BeginGetServisky(
							MST_Global.TerminalID,
							//string.Empty, 
							//Prijem_4.PrijemMain.prijemI.sklad == null ? string.Empty : Prijem_4.PrijemMain.prijemI.sklad.skl_id.Trim(),
							new AsyncCallback(ServiskyEnd),
							null
							);
						break;
					case Operation.GetData:
						aresult = sservice.BeginGetServiskaDBFile(davka, MST_Global.TerminalID, new AsyncCallback(ServiskaGetDataEnd), (object)davka);
						break;
					case Operation.SendDataRelease:
						try
						{
							filename = Path.Combine(Main.StorageDir, davka.ToString() + "." + Main.Ext_ServisI);
							string filenameZip = filename + ".zip";
							string KamNaServer = MST_Global.TerminalID + "\\" + Path.GetFileName(filenameZip);

							Fask.MST_W.FileTransfer.CompressFile.CompressToZip(filename, filenameZip);

							if (Fask.MST_W.FileTransfer.Uploading.SendFile_API(filenameZip, KamNaServer, Fask.MST_W.FileTransfer.Uploading.Co.Servis) != "OK") { throw new Exception("davku se nepodarilo odeslat"); };


							//filename = Path.Combine(Main.StorageDir, davka.ToString() + "." + Main.Ext_ServisI);
							////filename = Path.Combine(Main.StorageDir, Globals.ServisZdrojePohybDB);
							//Fask.MST_W.FileTransfer.CompressFile.CompressToZip(filename, filename + ".zip");
							//if (Fask.MST_W.FileTransfer.Uploading.SendFileCalcTime(MST_Global.ServerAddress + "Upload.aspx", filename + ".zip", MST_Global.TerminalID + "\\" + Path.GetFileName(filename) + ".zip", Fask.MST_W.FileTransfer.Uploading.Co.Servis) != "OK") { throw new Exception("davku se nepodarilo odeslat"); };
							aresult = sservice.BeginProcessServisDBFile2(MST_Global.UserID, this.davka, MST_Global.TerminalID, Fask.MST_W.ServisModuleWService.ProcessServisState.Uvolnit, new AsyncCallback(ProcessServisEnd), (object)davka);

						}
						catch (Exception ex)
						{
							throw ex;
						}
						break;
					case Operation.SendDataSave:
						try
						{

							filename = Path.Combine(Main.StorageDir, davka + "." + Main.Ext_ServisI);
							string filenameZip = filename + ".zip";
							string KamNaServer = MST_Global.TerminalID + "\\" + Path.GetFileName(filenameZip);

							Fask.MST_W.FileTransfer.CompressFile.CompressToZip(filename, filename + ".zip");

							if (Fask.MST_W.FileTransfer.Uploading.SendFile_API(filenameZip, KamNaServer, Fask.MST_W.FileTransfer.Uploading.Co.Servis) != "OK") { throw new Exception("davku se nepodarilo odeslat"); };



							//filename = Path.Combine(Main.StorageDir, davka.ToString() + "." + Main.Ext_ServisI);
							////filename = Path.Combine(Main.StorageDir, Globals.ServisZdrojePohybDB);
							//Fask.MST_W.FileTransfer.CompressFile.CompressToZip(filename, filename + ".zip");
							//if (Fask.MST_W.FileTransfer.Uploading.SendFileCalcTime(MST_Global.ServerAddress + "Upload.aspx", filename + ".zip", MST_Global.TerminalID + "\\" + Path.GetFileName(filename) + ".zip", Fask.MST_W.FileTransfer.Uploading.Co.Servis) != "OK") { throw new Exception("davku se nepodarilo odeslat"); };
							aresult = sservice.BeginProcessServisDBFile2(MST_Global.UserID, this.davka, MST_Global.TerminalID, Fask.MST_W.ServisModuleWService.ProcessServisState.Zpracovat, new AsyncCallback(ProcessServisEnd), (object)davka);
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
			}
		}

        private void ServiskyEnd(IAsyncResult ares)
        {
            try
            {
                heads = sservice.EndGetServisky(ares);
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
                    Logging.Log.Write("Chyba: \n" + ex2.Message, "ServiskyEnd");
                    //throw new Exception("Data byly odeslány, ale nezdařilo se odstranit datové soubory",ex);
                }
            }
        }

        private void ServiskaGetDataEnd(IAsyncResult ares)
        {
            try
            {
                data = sservice.EndGetServiskaDBFile(ares);
                aresult = sservice.BeginGetServiskaReceived((int)ares.AsyncState, MST_Global.TerminalID, new AsyncCallback(ServiskaGetDataReceivedEnd), ares.AsyncState);
            }
            catch (Exception ex)
            {
                MessageBoxBig.Show(ex.Message);
                data = null;
                this.BeginInvoke(new VoidDelegate(PerformCancel));
            }
        }

        private void ServiskaGetDataReceivedEnd(IAsyncResult ares)
        {
            try
            {
                bool result = sservice.EndGetServiskaReceived(ares);
                if (result)
                {
                    int davka = (int)ares.AsyncState;
                    string filename = Path.Combine(Main.StorageDir, davka.ToString() + "." + Main.Ext_ServisI);
                    //MySystem.FileOperations.DBSave(filename, ref data);
                    if (!FileTransfer.Downloading.DownloadFileFromServer(filename,false,true,this))
                        throw new Exception("nepodařilo se stáhnout dávku");
                    this.BeginInvoke(new VoidDelegate(PerformOK));
                }
                else
                {
                    this.BeginInvoke(new VoidDelegate(PerformCancel));
                }
            }
            catch //(Exception ex)
            {
                //MessageBoxBig.Show(ex.Message); upraveno aby pri zruseni stahovani nehazelo hlasku objectdispose...
                try
                {
                    this.BeginInvoke(new VoidDelegate(PerformCancel));
                }
                catch (Exception ex2)
                {
                    Logging.Log.Write("Chyba: \n" + ex2.Message, "ServiskaGetDataReceivedEnd");
                    //throw new Exception("Data byly odeslány, ale nezdařilo se odstranit datové soubory",ex);
                }
                
            }
        }

        private void ProcessServisEnd(IAsyncResult ares)
        {
            try
            {

                ServisModuleWService.StatusObject so = sservice.EndProcessServisDBFile(ares);

                if (so.StatusText == "OK" && !so.Exception)
                {
                    try
                    {
                        // data pohybu uspesne odeslana, smazat davku ...
                        string filename = Path.Combine(Main.StorageDir, davka.ToString() + "." + Main.Ext_ServisI);
                        //string filename = Path.Combine(Main.StorageDir, Globals.ServisZdrojePohybDB);         
                        File.Delete(filename);
                        File.Delete(filename+ ".zip");
                    }
                    catch (Exception ex)
                    {
                        Logging.Log.Write("Data byla odeslána, ale nezdařilo se odstranit datové soubory: \n" + ex.Message, "ProcessServisEnd");
                        //throw new Exception("Data byly odeslány, ale nezdařilo se odstranit datové soubory",ex);
                    }
                }
                else
                {
                    MessageBoxBig.Show("Nastala chyba při odesílání dat: " + so.StatusText);
                    string filename = Path.Combine(Main.StorageDir, davka.ToString() + "." + Main.Ext_ServisI);
                    File.Delete(filename + ".zip");

                    throw new Exception("Nepodařilo se odeslat data");
                }
                this.BeginInvoke(new VoidDelegate(PerformOK));
            }
            catch (Exception ex)
            {
                //MessageBoxBig.Show(ex.Message, "Odeslání dat", MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                Exceptions.ExceptionVizualize.Show(ex);
                this.BeginInvoke(new VoidDelegate(PerformCancel));
            }
        }

        protected override void PerformCancel()
        {
            sservice.Abort();
            base.PerformCancel();
        }

        public void WriteDesc(string a)
        {
            if (this.InvokeRequired)
            {
                //sa vytvori nove vlakno v kterem je zavolana znovu tato metoda
                this.BeginInvoke((VoidDelegate)delegate() { this.WriteDesc(a); });
                return;
            }

            this.Description = a;
        }
    }
}

