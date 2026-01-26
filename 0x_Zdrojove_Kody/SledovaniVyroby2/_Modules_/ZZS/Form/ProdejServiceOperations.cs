using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//using Fask.MST_W.Forms;
using System.IO;
//using Fask.MST_W.ServerAccess;
using System.Net;
using System.Reflection;
using FASK.SledovaniVyroby.Module.ZZS.ProdejService;


namespace FASK.SledovaniVyroby.Module.ZZS.Forms
{
    public partial class ProdejServiceOperations : ServiceOperationsForm
    {
        public enum Operation
        {
            SendData
        }

        private Operation operation;
        private ProdejService.ProdejService pservice = null;
        private int davka = -1; //Davka

        private string configFilePath = string.Empty;
        public const string ProdejOExt = "di";

        private ProdejServiceOperations()
        {
            InitializeComponent();
            this.configFilePath = (new Uri(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase))).LocalPath;
            this.Text = "";
        }

        private ProdejServiceOperations(ProdejService.ProdejService pservice, Operation operation)
            : this()
        {
            this.pservice = pservice;
            this.operation = operation;

            this.pservice.ProcessProdejDB2Completed -= new ProdejService.ProcessProdejDB2CompletedEventHandler(ProcessProdejEnd);
            this.pservice.ProcessProdejDB2Completed += new ProdejService.ProcessProdejDB2CompletedEventHandler(ProcessProdejEnd);

        }

        public static bool SendData(ProdejService.ProdejService pservice, int davka)
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
                        return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void PrijemServiceOperations_Load(object sender, EventArgs e)
        {
            //this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            //this.Size = Forms.FormLocation.ScreenResolution;
            byte[] data = null;
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
                        filename = Path.Combine(configFilePath, @"SQLCEDB\" + davka.ToString() + "." + ProdejOExt);
                        try
                        {
                            filename = Path.Combine(configFilePath, @"SQLCEDB\"+ davka.ToString() + "." + ProdejOExt);
                            FileTransfer.CompressFile.CompressToZip(filename, filename + ".zip");
                            if (FileTransfer.Uploading.SendFile(Logging.LogConfig.KomServer + "Upload.aspx", filename + ".zip", Logging.LogConfig.MachineID + "\\" + Path.GetFileName(filename) + ".zip") != "OK")
                            { 
                                throw new Exception("davku se nepodarilo odeslat"); 
                            }
                            //aresult = pservice.BeginProcessPrijemDBFile2(this.davka, MST_Global.TerminalID, Fask.MST_W.PrijemService.ProcessPrijemState.Zpracovat, new AsyncCallback(ProcessPrijemEnd), (object)davka);
                            pservice.ProcessProdejDB2Async(this.davka, byte.Parse(Logging.LogConfig.MachineID), int.Parse(Logging.LogConfig.LoginID), Logging.LogConfig.LoginName);
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
                ErrorLog.Log.Write(ex);
                FlexibleMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void ProcessProdejEnd(object sender, ProcessProdejDB2CompletedEventArgs e)
        {

            string filename = Path.Combine(this.configFilePath, @"SQLCEDB\" + davka.ToString() + "." + ProdejOExt);

            try
            {
                //Status objekt vraceny s serveru
                //e.Result

                if (e.Result.StatusText == "OK")
                {
                    

                    try
                    {
                        if (pservice == null) //jestlize je pservice null, pak doslo k abortu, a znamena to ze nema dal nic resit
                        {
                            return;
                        }

                        //ProdejService.StatusObject so = pservice.EndProcessProdejDB(ares);
                        //if (so.StatusText == "OK" && !so.Exception)
                        //{
                        //string filename = Path.Combine(Main.StorageDir, davka.ToString() + ".prodej");
                        //File.Delete(Path.ChangeExtension(filename, Main.ProdejOExt));

                        File.Delete(filename);
                        File.Delete(filename + ".zip");
                        //}
                        //else
                        //{
                        //    //MessageBoxBig.Show("Nastala chyba pøi odesílání dat: " + so.StatusText);
                        //    //throw new Exception("Nepodaøilo se odeslat data");
                        //    MessageBoxBig.Show("Nastala chyba pøi odesílání dat: " + so.StatusText);
                        //    //string filename = Path.Combine(Main.StorageDir, davka.ToString() + "." + Main.ProdejOExt);
                        //    File.Delete(filename + ".zip");

                        //    throw new Exception("Nepodaøilo se odeslat data");
                        //}
                        this.BeginInvoke(new VoidDelegate(PerformOK));
                    }
                    catch (Exception ex)
                    {
                        FlexibleMessageBox.Show(ex.Message);

                        try
                        {
                            File.Delete(filename + ".zip");
                        }
                        catch
                        { }

                        this.BeginInvoke(new VoidDelegate(PerformCancel));
                    }
                }
                else
                {
                    FlexibleMessageBox.Show(e.Result.StatusText, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                if (File.Exists(filename + ".zip"))
                    File.Delete(filename + ".zip");
                throw ex;
            }
        }

        delegate void MethodInvoker();

        protected override void PerformCancel()
        {
            finalize();
            base.PerformCancel();
        }

        private void finalize()
        {
            pservice.ProcessProdejDB2Completed -= new ProcessProdejDB2CompletedEventHandler(ProcessProdejEnd);
            
            try
            {
                if (pservice != null)
                    pservice.Abort();

                pservice = null;
            }
            catch
            {
            }
        }

        protected override void PerformOK()
        {
            finalize();
            base.PerformOK();
        }

    }
}

