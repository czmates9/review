using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
//using Fask.MST_W.ServerAccess;
using System.Net;
using FASK.SledovaniVyroby.Module.ZZS.CiselnikService;
using FASK.SledovaniVyroby.Logging;

namespace FASK.SledovaniVyroby.Module.ZZS
{
    public partial class CiselnikServiceOperationsForm : FASK.SledovaniVyroby.Module.ZZS.Forms.ServiceOperationsForm
    {
        public enum Operation
        {
            Zbozi,            
            Odberatele,
            //Strediska,
            TypDokladu,
            Sklady,
            Pracovnici,
            //Lokace,
            //Uzivatele,
            //Meny,
            //ExportZbozi,
            //ExportOdberatele,
            //ExportSklady,
            //ExportMeny,
            //ExportStrediska,
            //ExportPracovnici
        }

        private Operation operation;
        private CiselnikService.CiselnikService cservice = null;
        //private LoginService.LoginService lservice = null;
        private string skladprefix = string.Empty;

        

        private CiselnikServiceOperationsForm()
        {
            InitializeComponent();


        }

        private CiselnikServiceOperationsForm(CiselnikService.CiselnikService cservice, Operation operation, string skladprefix)
            : this()
        {
            this.cservice = cservice;
            this.operation = operation;
            this.skladprefix = skladprefix;
            cservice.KatalogTypDokladuDBPrepareCompleted -= new CiselnikService.KatalogTypDokladuDBPrepareCompletedEventHandler(ProcessKatalogTypDokladuEnd);
            cservice.KatalogZboziDBPrepareCompleted -= new KatalogZboziDBPrepareCompletedEventHandler(ProcessKatalogZboziEnd);
            cservice.KatalogSkladyDBPrepareCompleted -= new KatalogSkladyDBPrepareCompletedEventHandler(ProcessKatalogSkladyEnd);
            cservice.KatalogOdberateleDBPrepareCompleted -= new KatalogOdberateleDBPrepareCompletedEventHandler(ProcessKatalogOdberateleEnd);
            cservice.KatalogPracovniciDBPrepareCompleted -= new KatalogPracovniciDBPrepareCompletedEventHandler(ProcessKatalogPracovniciEnd);
            

            cservice.KatalogTypDokladuDBPrepareCompleted += new CiselnikService.KatalogTypDokladuDBPrepareCompletedEventHandler(ProcessKatalogTypDokladuEnd);
            cservice.KatalogZboziDBPrepareCompleted += new KatalogZboziDBPrepareCompletedEventHandler(ProcessKatalogZboziEnd);
            cservice.KatalogSkladyDBPrepareCompleted += new KatalogSkladyDBPrepareCompletedEventHandler(ProcessKatalogSkladyEnd);
            cservice.KatalogOdberateleDBPrepareCompleted += new KatalogOdberateleDBPrepareCompletedEventHandler(ProcessKatalogOdberateleEnd);
            cservice.KatalogPracovniciDBPrepareCompleted += new KatalogPracovniciDBPrepareCompletedEventHandler(ProcessKatalogPracovniciEnd);
        }

        private CiselnikServiceOperationsForm(CiselnikService.CiselnikService cservice, Operation operation)
            : this()
        {
            this.cservice = cservice;
            this.operation = operation;
            this.skladprefix = "";
            cservice.KatalogTypDokladuDBPrepareCompleted -= new CiselnikService.KatalogTypDokladuDBPrepareCompletedEventHandler(ProcessKatalogTypDokladuEnd);
            cservice.KatalogZboziDBPrepareCompleted -= new KatalogZboziDBPrepareCompletedEventHandler(ProcessKatalogZboziEnd);
            cservice.KatalogSkladyDBPrepareCompleted -= new KatalogSkladyDBPrepareCompletedEventHandler(ProcessKatalogSkladyEnd);
            cservice.KatalogOdberateleDBPrepareCompleted -= new KatalogOdberateleDBPrepareCompletedEventHandler(ProcessKatalogOdberateleEnd);
            cservice.KatalogPracovniciDBPrepareCompleted -= new KatalogPracovniciDBPrepareCompletedEventHandler(ProcessKatalogPracovniciEnd);
            
            cservice.KatalogTypDokladuDBPrepareCompleted += new CiselnikService.KatalogTypDokladuDBPrepareCompletedEventHandler(ProcessKatalogTypDokladuEnd);
            cservice.KatalogZboziDBPrepareCompleted += new KatalogZboziDBPrepareCompletedEventHandler(ProcessKatalogZboziEnd);
            cservice.KatalogSkladyDBPrepareCompleted += new KatalogSkladyDBPrepareCompletedEventHandler(ProcessKatalogSkladyEnd);
            cservice.KatalogOdberateleDBPrepareCompleted += new KatalogOdberateleDBPrepareCompletedEventHandler(ProcessKatalogOdberateleEnd);
            cservice.KatalogPracovniciDBPrepareCompleted += new KatalogPracovniciDBPrepareCompletedEventHandler(ProcessKatalogPracovniciEnd);
        }

        //private CiselnikServiceOperationsForm(LoginService.LoginService loginservice, Operation operation)
        //    : this()
        //{
        //    this.cservice = null;
        //    this.lservice = loginservice;
        //    this.operation = operation;
        //    this.skladprefix = "";
        //}


        #region Entering methods

        //public static bool KatalogZboziExport(CiselnikServiceSession ciselnikS, string skladprefix)
        //{
        //    using (CiselnikServiceOperationsForm cso = new CiselnikServiceOperationsForm(ciselnikS, Operation.ExportZbozi, skladprefix))
        //    {
        //        cso.Description = "Probíhá export èíselníku zboží";
        //        if (cso.ShowDialog() == DialogResult.Cancel)
        //            return false;
        //        else
        //            return true;
        //    }
        //}

        //public static bool KatalogMenyExport(CiselnikServiceSession ciselnikS)
        //{
        //    using (CiselnikServiceOperationsForm cso = new CiselnikServiceOperationsForm(ciselnikS, Operation.ExportMeny))
        //    {
        //        cso.Description = "Probíhá export èíselníku mìn";
        //        if (cso.ShowDialog() == DialogResult.Cancel)
        //            return false;
        //        else
        //            return true;
        //    }
        //}

        //public static bool KatalogStrediskaExport(CiselnikServiceSession ciselnikS)
        //{
        //    using (CiselnikServiceOperationsForm cso = new CiselnikServiceOperationsForm(ciselnikS, Operation.ExportStrediska))
        //    {
        //        cso.Description = "Probíhá export èíselníku støedisek";
        //        if (cso.ShowDialog() == DialogResult.Cancel)
        //            return false;
        //        else
        //            return true;
        //    }
        //}

        //public static bool KatalogPracovniciExport(CiselnikServiceSession ciselnikS)
        //{
        //    using (CiselnikServiceOperationsForm cso = new CiselnikServiceOperationsForm(ciselnikS, Operation.ExportPracovnici))
        //    {
        //        cso.Description = "Probíhá export èíselníku pracovníkù";
        //        if (cso.ShowDialog() == DialogResult.Cancel)
        //            return false;
        //        else
        //            return true;
        //    }
        //}

        //public static bool KatalogOdberateleExport(CiselnikServiceSession ciselnikS)
        //{
        //    using (CiselnikServiceOperationsForm cso = new CiselnikServiceOperationsForm(ciselnikS, Operation.ExportOdberatele))
        //    {
        //        cso.Description = "Probíhá export èíselníku odbìratelù";
        //        if (cso.ShowDialog() == DialogResult.Cancel)
        //            return false;
        //        else
        //            return true;
        //    }
        //}

        //public static bool KatalogSkladyExport(CiselnikServiceSession ciselnikS)
        //{
        //    using (CiselnikServiceOperationsForm cso = new CiselnikServiceOperationsForm(ciselnikS, Operation.ExportSklady))
        //    {
        //        cso.Description = "Probíhá export èíselníku skladù";
        //        if (cso.ShowDialog() == DialogResult.Cancel)
        //            return false;
        //        else
        //            return true;
        //    }
        //}

        public static bool KatalogZbozi(CiselnikService.CiselnikService cservice, string skladprefix)
        {
            using (CiselnikServiceOperationsForm cso = new CiselnikServiceOperationsForm(cservice, Operation.Zbozi, skladprefix))
            {
                cso.Description = "Stahuje se číselník zboží";
                if (cso.ShowDialog() == DialogResult.Cancel)
                    return false;
                else
                    return true;
            }
        }

        //public static bool KatalogUzivatelu(LoginService.LoginService lservice)
        //{
        //    using (CiselnikServiceOperationsForm cso = new CiselnikServiceOperationsForm(lservice, Operation.Uzivatele))
        //    {
        //        cso.Description = "Stahuje se číselník uživatelú";
        //        if (cso.ShowDialog() == DialogResult.Cancel)
        //            return false;
        //        else
        //            return true;
        //    }
        //}




        public static bool KatalogSklady(CiselnikService.CiselnikService cservice)
        {
            using (CiselnikServiceOperationsForm cso = new CiselnikServiceOperationsForm(cservice, Operation.Sklady, string.Empty))
            {
                cso.Description = "Stahuje se číselník skladú";
                if (cso.ShowDialog() == DialogResult.Cancel)
                    return false;
                else
                    return true;
            }
        }


        public static bool KatalogOdberatele(CiselnikService.CiselnikService cservice, string skladprefix)
        {
            using (CiselnikServiceOperationsForm cso = new CiselnikServiceOperationsForm(cservice, Operation.Odberatele, skladprefix))
            {
                cso.Description = "Stahuje se číselník odbìratelú";
                if (cso.ShowDialog() == DialogResult.Cancel)
                    return false;
                else
                    return true;
            }
        }

        public static bool KatalogTypDokladu(CiselnikService.CiselnikService cservice, string skladprefix)
        {
            using (CiselnikServiceOperationsForm cso = new CiselnikServiceOperationsForm(cservice, Operation.TypDokladu, skladprefix))
            {
                cso.Description = "Stahuje se číselník typů dokladů";
                if (cso.ShowDialog() == DialogResult.Cancel)
                    return false;
                else
                    return true;
            }
        }

        //public static bool KatalogStrediska(CiselnikService.CiselnikService cservice, string skladprefix)
        //{
        //    using (CiselnikServiceOperationsForm cso = new CiselnikServiceOperationsForm(cservice, Operation.Strediska, skladprefix))
        //    {
        //        cso.Description = "Stahuje se èíselník støedisek";
        //        if (cso.ShowDialog() == DialogResult.Cancel)
        //            return false;
        //        else
        //            return true;
        //    }
        //}

        public static bool KatalogPracovnici(CiselnikService.CiselnikService cservice, string skladprefix)
        {
            using (CiselnikServiceOperationsForm cso = new CiselnikServiceOperationsForm(cservice, Operation.Pracovnici, skladprefix))
            {
                cso.Description = "Stahuje se èíselník pracovníkù";
                if (cso.ShowDialog() == DialogResult.Cancel)
                    return false;
                else
                    return true;
            }
        }

        //public static bool KatalogMen(CiselnikService.CiselnikService cservice)
        //{
        //    using (CiselnikServiceOperationsForm cso = new CiselnikServiceOperationsForm(cservice, Operation.Meny))
        //    {
        //        cso.Description = "Stahuje se èíselník mìn";
        //        if (cso.ShowDialog() == DialogResult.Cancel)
        //            return false;
        //        else
        //            return true;
        //    }
        //}

        //public static bool KatalogLokace(CiselnikService.CiselnikService cservice, string skladprefix)
        //{
        //    using (CiselnikServiceOperationsForm cso = new CiselnikServiceOperationsForm(cservice, Operation.Lokace, skladprefix))
        //    {
        //        cso.Description = "Stahuje se èíselník lokací";
        //        if (cso.ShowDialog() == DialogResult.Cancel)
        //            return false;
        //        else
        //            return true;
        //    }
        //}
        #endregion

        private void CiselnikServiceOperationsForm_Load(object sender, EventArgs e)
        {
            //this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            //this.Size = Forms.FormLocation.ScreenResolution;
            
            try
            {
                switch (operation)
                {
                    case Operation.Zbozi:
                        cservice.KatalogZboziDBPrepareAsync(byte.Parse(LogConfig.MachineID), skladprefix);
                        break;
                    case Operation.Odberatele:
                        cservice.KatalogOdberateleDBPrepareAsync(byte.Parse(LogConfig.MachineID), skladprefix);
                        break;
                    //case Operation.Strediska:
                    //    aresult = cservice.BeginKatalogStrediskaDBPrepare(MST_Global.TerminalID, skladprefix, new AsyncCallback(ProcessKatalogStrediskaEnd), null);
                    //    break;
                    case Operation.TypDokladu:
                        cservice.KatalogTypDokladuDBPrepareAsync(byte.Parse(LogConfig.MachineID), skladprefix);
                        break;
                    case Operation.Sklady:
                        cservice.KatalogSkladyDBPrepareAsync(byte.Parse(LogConfig.MachineID));
                        break;
                    case Operation.Pracovnici:
                        cservice.KatalogPracovniciDBPrepareAsync(byte.Parse(LogConfig.MachineID), skladprefix);
                        break;
                    //case Operation.Lokace:
                    //    aresult = cservice.BeginKatalogLokaceDBPrepare(MST_Global.TerminalID, skladprefix, new AsyncCallback(ProcessKatalogLokaceEnd), null);
                    //    break;
                    //case Operation.Uzivatele:
                    //    aresult = lservice.BeginGetKatalogUzivatele(MST_Global.TerminalID, new AsyncCallback(ProcessKatalogUzivateleEnd), null);
                    //    break;
                    //case Operation.Meny:
                    //    aresult = cservice.BeginKatalogMenyDBPrepare(MST_Global.TerminalID, new AsyncCallback(ProcessKatalogMenyEnd), null);
                    //    break;
                    //case Operation.ExportZbozi:
                    //    aresult = cservice.BeginKatalogZboziExport(MST_Global.TerminalID, skladprefix, new AsyncCallback(ProcessKatalogZboziExportEnd), null);
                    //    break;
                    //case Operation.ExportOdberatele:
                    //    aresult = cservice.BeginKatalogOdberateleExport(MST_Global.TerminalID, new AsyncCallback(ProcessKatalogOdberateleExportEnd), null);
                    //    break;
                    //case Operation.ExportSklady:
                    //    aresult = cservice.BeginKatalogSkladyExport(MST_Global.TerminalID, new AsyncCallback(ProcessKatalogSkladyExportEnd), null);
                    //    break;
                    //case Operation.ExportPracovnici:
                    //    aresult = cservice.BeginKatalogPracovniciExport(MST_Global.TerminalID, new AsyncCallback(ProcessKatalogPracovniciExportEnd), null);
                    //    break;
                    //case Operation.ExportMeny:
                    //    aresult = cservice.BeginKatalogMenyExport(MST_Global.TerminalID, new AsyncCallback(ProcessKatalogMenyExportEnd), null);
                    //    break;
                    //case Operation.ExportStrediska:
                    //    aresult = cservice.BeginKatalogStrediskaExport(MST_Global.TerminalID, new AsyncCallback(ProcessKatalogStrediskaExportEnd), null);
                    //    break;
                    default:
                        this.BeginInvoke(new VoidDelegate(PerformCancel));
                        break;
                }

            }
            catch (Exception ex)
            {
                ErrorLog.Log.Write(ex.Message);
                //Logging.Log.Write(ex);
                //MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
        }

        delegate void MethodInvoker();

        private void ProcessKatalogZboziEnd(object sender, KatalogZboziDBPrepareCompletedEventArgs e)
        {
            try
            {
                if (cservice == null) //Doslo k abrotu, tak ven
                {
                    return;
                }

                //this.data = cservice.EndKatalogZboziDB(ares);
                //MySystem.FileOperations.DBSave(Main.CiselnikKatalogZboziDB, ref this.data);

                //bool done = cservice.EndKatalogZboziDBPrepare(ares);
                //CiselnikService.StatusObject so = cservice.EndKatalogZboziDBPrepare(ares);
                //if (so.Exception)
                //    throw new Exception("Číselník zboží:\n" + so.StatusText);

                string configFilePath = (new Uri(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase))).LocalPath;

                string fullpath = Path.Combine(configFilePath , @"SQLCEDB\" + Properties.Settings.Default.CiselnikKatalogZboziDB);

                DownloadFileFromServer(fullpath);

                ReindexaceCiselnikZbozi(fullpath);

                this.BeginInvoke(new VoidDelegate(PerformOK));
            }
            catch (Exception ex)
            {
                //MessageBoxBig.Show(ex.Message);
                ErrorLog.Log.Write(ex.Message);
                this.BeginInvoke(new VoidDelegate(PerformCancel));
            }
        }

        private void ReindexaceCiselnikZbozi(string fileciselnik)
        {
            System.Data.SqlServerCe.SqlCeConnection sceconn = null;
            try
            {
                this.BeginInvoke((MethodInvoker)delegate()
                {
                    this.Description = "Kontrola dat";
                });

                sceconn = new System.Data.SqlServerCe.SqlCeConnection("Data source=" + fileciselnik);
                sceconn.Open();

                int pocetindexu = 13;
                int pocetindexucount = 1;

                this.BeginInvoke((MethodInvoker)delegate()
                {
                    this.Description = "Obnovení indexù " + (pocetindexucount++) + " / " + pocetindexu;
                });

                System.Data.SqlServerCe.SqlCeCommand scec = new System.Data.SqlServerCe.SqlCeCommand(
                    "Select * from czmst095 where itemdesc='zamsehf'",
                    sceconn);
                scec.ExecuteNonQuery();


                this.BeginInvoke((MethodInvoker)delegate()
                {
                    this.Description = "Obnovení indexù " + (pocetindexucount++) + " / " + pocetindexu;
                });
                scec.CommandText = "Select * from czmst095 where vnditnum='98765214'";
                scec.ExecuteNonQuery();

                this.BeginInvoke((MethodInvoker)delegate()
                {
                    this.Description = "Obnovení indexù " + (pocetindexucount++) + " / " + pocetindexu;
                });
                scec.CommandText = "Select * from czmst095 where cz_carkod='98765214'";
                scec.ExecuteNonQuery();

                this.BeginInvoke((MethodInvoker)delegate()
                {
                    this.Description = "Obnovení indexù " + (pocetindexucount++) + " / " + pocetindexu;
                });
                scec.CommandText = "Select * from czmst095 where skl_id='98765214'";
                scec.ExecuteNonQuery();

                this.BeginInvoke((MethodInvoker)delegate()
                {
                    this.Description = "Obnovení indexù " + (pocetindexucount++) + " / " + pocetindexu;
                });
                scec.CommandText = "Select * from czmst095 where itemnmbr='98765214'";
                scec.ExecuteNonQuery();

                this.BeginInvoke((MethodInvoker)delegate()
                {
                    this.Description = "Obnovení indexù " + (pocetindexucount++) + " / " + pocetindexu;
                });
                scec.CommandText = "Select * from czmst095 where itemcode='dasdcxyc'";
                scec.ExecuteNonQuery();


                this.BeginInvoke((MethodInvoker)delegate()
                {
                    this.Description = "Obnovení indexù " + (pocetindexucount++) + " / " + pocetindexu;
                });
                scec.CommandText = "Select * from czmst095 where odb_id='dasdcxyc'";
                scec.ExecuteNonQuery();

                this.BeginInvoke((MethodInvoker)delegate()
                {
                    this.Description = "Obnovení indexù " + (pocetindexucount++) + " / " + pocetindexu;
                });
                scec.CommandText = "Select * from czmst095 where serltnum='dasdcxyc'";
                scec.ExecuteNonQuery();

                this.BeginInvoke((MethodInvoker)delegate()
                {
                    this.Description = "Obnovení indexù " + (pocetindexucount++) + " / " + pocetindexu;
                });

                scec.CommandText = "select count(*) from czmst095";
                scec.CommandType = CommandType.Text;
                scec.ExecuteScalar();

                this.BeginInvoke((MethodInvoker)delegate()
                {
                    this.Description = "Obnovení indexù " + (pocetindexucount++) + " / " + pocetindexu;
                });

                scec.CommandText = "select * from czmst095M where ITEMNMBR='98765214'";
                scec.CommandType = CommandType.Text;
                scec.ExecuteNonQuery();

                this.BeginInvoke((MethodInvoker)delegate()
                {
                    this.Description = "Obnovení indexù " + (pocetindexucount++) + " / " + pocetindexu;
                });

                scec.CommandText = "select * from czmst095M where ITEMNMBR='98765214' and mena_ID='CZK'";
                scec.CommandType = CommandType.Text;
                scec.ExecuteNonQuery();

                this.BeginInvoke((MethodInvoker)delegate()
                {
                    this.Description = "Obnovení indexù " + (pocetindexucount++) + " / " + pocetindexu;
                });

                scec.CommandText = "select * from czmst095M where ITEMNMBR='98765214' and mena_ID='CZK' and PRICEX=0";
                scec.CommandType = CommandType.Text;
                scec.ExecuteNonQuery();

                this.BeginInvoke((MethodInvoker)delegate()
                {
                    this.Description = "Obnovení indexù " + (pocetindexucount++) + " / " + pocetindexu;
                });

                scec.CommandText = "select count(*) from czmst095M";
                scec.CommandType = CommandType.Text;
                scec.ExecuteNonQuery();

                //scec.CommandText = "Select count(*) from czmst095";
                //scec.ExecuteNonQuery();

                sceconn.Close();
                sceconn = null;
            }
            catch (Exception e)
            {
                ErrorLog.Log.Write(e.Message);

                if (sceconn != null && sceconn.State == ConnectionState.Open)
                    sceconn.Close();
            }
        }

        private void ProcessKatalogOdberateleEnd(object sender, KatalogOdberateleDBPrepareCompletedEventArgs e)
        {
            try
            {
                if (cservice == null) //Doslo k abrotu, tak ven
                {
                    return;
                }

                //this.data = cservice.EndKatalogOdberateleDB(ares);
                //MySystem.FileOperations.DBSave(Main.CiselnikOdberateleDB, ref this.data);

                //bool done = cservice.EndKatalogOdberateleDBPrepare(ares);
                //if (!done)
                //    throw new Exception("Nepodařilo se pøipravit èíselník odbìratelù");
                string configFilePath = (new Uri(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase))).LocalPath;

                string fullpath = Path.Combine(configFilePath, @"SQLCEDB\" + Properties.Settings.Default.CiselnikOdberateleDB);
                DownloadFileFromServer(fullpath);

                this.BeginInvoke(new VoidDelegate(PerformOK));
            }
            catch (Exception ex)
            {
                //MessageBoxBig.Show(ex.Message);
                this.BeginInvoke(new VoidDelegate(PerformCancel));
            }
        }

        //private void ProcessKatalogStrediskaEnd(IAsyncResult ares)
        //{
        //    try
        //    {
        //        if (cservice == null) //Doslo k abrotu, tak ven
        //        {
        //            return;
        //        }

        //        //this.data = cservice.EndKatalogStrediskaDB(ares);
        //        //MySystem.FileOperations.DBSave(Main.CiselnikStrediskaDB, ref this.data);

        //        bool done = cservice.EndKatalogStrediskaDBPrepare(ares);
        //        if (!done)
        //            throw new Exception("Nepodaøilo se pøipravit èíselník støedisek");
        //        DownloadFileFromServer(Main.CiselnikStrediskaDB);

        //        this.BeginInvoke(new VoidDelegate(PerformOK));
        //    }
        //    catch (Exception ex)
        //    {
        //       // MessageBoxBig.Show(ex.Message);
        //        this.BeginInvoke(new VoidDelegate(PerformCancel));
        //    }
        //}


        //private void ProcessKatalogMenyEnd(IAsyncResult ares)
        //{
        //    try
        //    {
        //        if (cservice == null) //Doslo k abrotu, tak ven
        //        {
        //            return;
        //        }

        //        //this.data = cservice.EndKatalogStrediskaDB(ares);
        //        //MySystem.FileOperations.DBSave(Main.CiselnikStrediskaDB, ref this.data);

        //        bool done = cservice.EndKatalogMenyDBPrepare(ares);
        //        if (!done)
        //            throw new Exception("Nepodaøilo se pøipravit èíselník mìn");
        //        DownloadFileFromServer(Main.CiselnikMenDB);

        //        this.BeginInvoke(new VoidDelegate(PerformOK));
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBoxBig.Show(ex.Message);
        //        this.BeginInvoke(new VoidDelegate(PerformCancel));
        //    }
        //}


        private void ProcessKatalogTypDokladuEnd(object sender, KatalogTypDokladuDBPrepareCompletedEventArgs e)
        {
            try
            {
                if (cservice == null) //Doslo k abrotu, tak ven
                {
                    return;
                }

                //this.data = cservice.EndKatalogTypDokladuDB(ares);
                //MySystem.FileOperations.DBSave(Main.CiselnikTypDokladuDB, ref this.data);

                //bool done = cservice.EndKatalogTypDokladuDBPrepare(ares);
                //if (!done)
                //    throw new Exception("Nepodaøilo se pøipravit èíselník typù dokladù");
                string configFilePath = (new Uri(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase))).LocalPath;

                string fullpath = Path.Combine(configFilePath, @"SQLCEDB\" + Properties.Settings.Default.CiselnikTypDokladuDB);

                DownloadFileFromServer(fullpath);
                
                this.BeginInvoke(new VoidDelegate(PerformOK));
            }
            catch (Exception ex)
            {
                //MessageBoxBig.Show(ex.Message);
                ErrorLog.Log.Write(ex.Message);
                this.BeginInvoke(new VoidDelegate(PerformCancel));
            }
        }


        //private void ProcessKatalogUzivateleEnd(IAsyncResult ares)
        //{
        //    try
        //    {
        //        if (lservice == null) //Doslo k abrotu, tak ven
        //        {
        //            return;
        //        }

        //        //this.data = cservice.EndKatalogTypDokladuDB(ares);
        //        //MySystem.FileOperations.DBSave(Main.CiselnikTypDokladuDB, ref this.data);

        //        LoginService.StatusObject done = lservice.EndGetKatalogUzivatele(ares);
        //        if (done.Exception)
        //            throw new Exception("Nepodařilo se připravit číselník uživatelú");

        //        DownloadFileFromServer(Main.CiselnikUzivateleDB);

        //        this.BeginInvoke(new VoidDelegate(PerformOK));
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBoxBig.Show(ex.Message);
        //        this.BeginInvoke(new VoidDelegate(PerformCancel));
        //    }
        //}

        private void ProcessKatalogSkladyEnd(object sender, KatalogSkladyDBPrepareCompletedEventArgs e)
        {
            try
            {
                if (cservice == null) //Doslo k abrotu, tak ven
                {
                    return;
                }

                //this.data = cservice.EndKatalogSkladyDB(ares);
                //MySystem.FileOperations.DBSave(Main.CiselnikSkladyDB, ref this.data);

                //bool done = cservice.EndKatalogSkladyDBPrepare(ares);
                //if (!done)
                //    throw new Exception("Nepodařilo se připravit číselník skladú");
                string configFilePath = (new Uri(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase))).LocalPath;

                string fullpath = Path.Combine(configFilePath, @"SQLCEDB\" + Properties.Settings.Default.CiselnikSkladyDB);

                DownloadFileFromServer(fullpath);

                this.BeginInvoke(new VoidDelegate(PerformOK));
            }
            catch (Exception ex)
            {
                //MessageBoxBig.Show(ex.Message);
                this.BeginInvoke(new VoidDelegate(PerformCancel));
            }
        }

        //private void ProcessKatalogZboziExportEnd(IAsyncResult ares)
        //{
        //    try
        //    {
        //        if (cservice == null) //Doslo k abrotu, tak ven
        //        {
        //            return;
        //        }

        //        //this.data = cservice.EndKatalogStrediskaDB(ares);
        //        //MySystem.FileOperations.DBSave(Main.CiselnikStrediskaDB, ref this.data);

        //        Fask.MST_W.CiselnikService.StatusObject done = cservice.EndKatalogZboziExport(ares);
        //        if (done.Exception)
        //            throw new Exception("Nepodaøilo se provést export zboží: " + done.StatusText);

        //        this.BeginInvoke(new VoidDelegate(PerformOK));
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBoxBig.Show(ex.Message);
        //        this.BeginInvoke(new VoidDelegate(PerformCancel));
        //    }
        //}


        //private void ProcessKatalogOdberateleExportEnd(IAsyncResult ares)
        //{
        //    try
        //    {
        //        if (cservice == null) //Doslo k abrotu, tak ven
        //        {
        //            return;
        //        }

        //        //this.data = cservice.EndKatalogStrediskaDB(ares);
        //        //MySystem.FileOperations.DBSave(Main.CiselnikStrediskaDB, ref this.data);

        //        Fask.MST_W.CiselnikService.StatusObject done = cservice.EndKatalogOdberateleExport(ares);
        //        if (done.Exception)
        //            throw new Exception("Nepodaøilo se provést export odbìratelù: " + done.StatusText);

        //        this.BeginInvoke(new VoidDelegate(PerformOK));
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBoxBig.Show(ex.Message);
        //        this.BeginInvoke(new VoidDelegate(PerformCancel));
        //    }
        //}

        //private void ProcessKatalogSkladyExportEnd(IAsyncResult ares)
        //{
        //    try
        //    {
        //        if (cservice == null) //Doslo k abrotu, tak ven
        //        {
        //            return;
        //        }

        //        //this.data = cservice.EndKatalogStrediskaDB(ares);
        //        //MySystem.FileOperations.DBSave(Main.CiselnikStrediskaDB, ref this.data);

        //        Fask.MST_W.CiselnikService.StatusObject done = cservice.EndKatalogSkladyExport(ares);
        //        if (done.Exception)
        //            throw new Exception("Nepodaøilo se provést export skladù: " + done.StatusText);

        //        this.BeginInvoke(new VoidDelegate(PerformOK));
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBoxBig.Show(ex.Message);
        //        this.BeginInvoke(new VoidDelegate(PerformCancel));
        //    }
        //}

        //private void ProcessKatalogStrediskaExportEnd(IAsyncResult ares)
        //{
        //    try
        //    {
        //        if (cservice == null) //Doslo k abrotu, tak ven
        //        {
        //            return;
        //        }

        //        //this.data = cservice.EndKatalogStrediskaDB(ares);
        //        //MySystem.FileOperations.DBSave(Main.CiselnikStrediskaDB, ref this.data);

        //        Fask.MST_W.CiselnikService.StatusObject done = cservice.EndKatalogSkladyExport(ares);
        //        if (done.Exception)
        //            throw new Exception("Nepodaøilo se provést export støedisek: " + done.StatusText);

        //        this.BeginInvoke(new VoidDelegate(PerformOK));
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBoxBig.Show(ex.Message);
        //        this.BeginInvoke(new VoidDelegate(PerformCancel));
        //    }
        //}

        //private void ProcessKatalogMenyExportEnd(IAsyncResult ares)
        //{
        //    try
        //    {
        //        if (cservice == null) //Doslo k abrotu, tak ven
        //        {
        //            return;
        //        }

        //        //this.data = cservice.EndKatalogStrediskaDB(ares);
        //        //MySystem.FileOperations.DBSave(Main.CiselnikStrediskaDB, ref this.data);

        //        Fask.MST_W.CiselnikService.StatusObject done = cservice.EndKatalogSkladyExport(ares);
        //        if (done.Exception)
        //            throw new Exception("Nepodaøilo se provést export mìn: " + done.StatusText);

        //        this.BeginInvoke(new VoidDelegate(PerformOK));
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBoxBig.Show(ex.Message);
        //        this.BeginInvoke(new VoidDelegate(PerformCancel));
        //    }
        //}

        //private void ProcessKatalogPracovniciExportEnd(IAsyncResult ares)
        //{
        //    try
        //    {
        //        if (cservice == null) //Doslo k abrotu, tak ven
        //        {
        //            return;
        //        }

        //        //this.data = cservice.EndKatalogStrediskaDB(ares);
        //        //MySystem.FileOperations.DBSave(Main.CiselnikStrediskaDB, ref this.data);

        //        Fask.MST_W.CiselnikService.StatusObject done = cservice.EndKatalogSkladyExport(ares);
        //        if (done.Exception)
        //            throw new Exception("Nepodaøilo se provést export pracovníkù: " + done.StatusText);

        //        this.BeginInvoke(new VoidDelegate(PerformOK));
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBoxBig.Show(ex.Message);
        //        this.BeginInvoke(new VoidDelegate(PerformCancel));
        //    }
        //}

        private void ProcessKatalogPracovniciEnd(object sender, KatalogPracovniciDBPrepareCompletedEventArgs e)
        {
            try
            {
                if (cservice == null) //Doslo k abrotu, tak ven
                {
                    return;
                }

                //this.data = cservice.EndKatalogStrediskaDB(ares);
                //MySystem.FileOperations.DBSave(Main.CiselnikStrediskaDB, ref this.data);

                //bool done = cservice.EndKatalogPracovniciDBPrepare(ares);
                //if (!done)
                //    throw new Exception("Nepodařilo se připravit číselník pracovníkú");

                string configFilePath = (new Uri(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase))).LocalPath;

                string fullpath = Path.Combine(configFilePath, @"SQLCEDB\" + Properties.Settings.Default.CiselnikPracovniciDB);


                DownloadFileFromServer(fullpath);

                this.BeginInvoke(new VoidDelegate(PerformOK));
            }
            catch (Exception ex)
            {
                FlexibleMessageBox.Show(ex.Message);
                this.BeginInvoke(new VoidDelegate(PerformCancel));
            }
        }

        //private void ProcessKatalogLokaceEnd(IAsyncResult ares)
        //{
        //    try
        //    {
        //        if (cservice == null) //Doslo k abrotu, tak ven
        //        {
        //            return;
        //        }

        //        bool done = cservice.EndKatalogLokaceDBPrepare(ares);
        //        if (!done)
        //            throw new Exception("Nepodaøilo se pøipravit èíselník lokací");

        //        DownloadFileFromServer(Main.CiselnikLokaceDB);

        //        ReindexaceCiselnikLokaci(Main.CiselnikLokaceDB);

        //        this.BeginInvoke(new VoidDelegate(PerformOK));
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBoxBig.Show(ex.Message);
        //        this.BeginInvoke(new VoidDelegate(PerformCancel));
        //    }
        //}

        //private void ReindexaceCiselnikLokaci(string fileciselnik)
        //{
        //    System.Data.SqlServerCe.SqlCeConnection sceconn = null;
        //    try
        //    {
        //        this.BeginInvoke((MethodInvoker)delegate()
        //        {
        //            this.Description = "Kontrola dat";
        //        });

        //        sceconn = new System.Data.SqlServerCe.SqlCeConnection("Data source=" + fileciselnik);
        //        sceconn.Open();

        //        int pocetindexu = 1;
        //        int pocetindexucount = 1;

        //        this.BeginInvoke((MethodInvoker)delegate()
        //        {
        //            this.Description = "Obnovení indexù " + (pocetindexucount++) + " / " + pocetindexu;
        //        });

        //        System.Data.SqlServerCe.SqlCeCommand scec = new System.Data.SqlServerCe.SqlCeCommand(
        //            "Select * from czmst094 where skl_id='zamsehf'",
        //            sceconn);
        //        scec.ExecuteNonQuery();

        //        //scec.CommandText = "Select count(*) from czmst095";
        //        //scec.ExecuteNonQuery();

        //        sceconn.Close();
        //        sceconn = null;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logging.Log.Write(ex);
        //        if (sceconn != null && sceconn.State == ConnectionState.Open)
        //            sceconn.Close();
        //    }
        //}

        protected override void PerformOK()
        {
            finalize();
            base.PerformOK();
        }

        private void finalize()
        {
            cservice.KatalogTypDokladuDBPrepareCompleted -= new CiselnikService.KatalogTypDokladuDBPrepareCompletedEventHandler(ProcessKatalogTypDokladuEnd);
            cservice.KatalogZboziDBPrepareCompleted -= new KatalogZboziDBPrepareCompletedEventHandler(ProcessKatalogZboziEnd);
            cservice.KatalogSkladyDBPrepareCompleted -= new KatalogSkladyDBPrepareCompletedEventHandler(ProcessKatalogSkladyEnd);
            cservice.KatalogOdberateleDBPrepareCompleted -= new KatalogOdberateleDBPrepareCompletedEventHandler(ProcessKatalogOdberateleEnd);
            cservice.KatalogPracovniciDBPrepareCompleted -= new KatalogPracovniciDBPrepareCompletedEventHandler(ProcessKatalogPracovniciEnd);
            try
            {
                if (cservice != null)
                    cservice.Abort();

                cservice = null;
            }
            catch
            {
            }
        }
        protected override void PerformCancel()
        {
            finalize();
            base.PerformCancel();
        }


        private void DownloadFileFromServer(string fileciselnik)
        {
            string fileciselniktmp = fileciselnik + ".tmp";

            #region Downloading code
            FileStream fs = null;
            try
            {
                #region Chunked download code
                /*
                FileTransferService.FileTransfer ftransferService = new Fask.MST_W.FileTransferService.FileTransfer();
                ftransferService.Url = MST_Global.ServerAddress + "FileTransfer.asmx";
                ftransferService.Timeout = 10000;
                ftransferService.UpdateWebServiceCredentials();

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
                    this.BeginInvoke((MethodInvoker)delegate()
                    {
                        this.Description = "Stahují se data èíselníku\n" + procento + "%\nRychlost:" + downloadspeed + " KB/s\nOdhad:" + odhad.Minutes + ":" + odhad.Seconds.ToString("00") + "\nChyb:" + errorRepeatCount;
                    });
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
                                throw new Exception("Chyba pøi stahování dat èíselníku", webEx);
                        }
                        catch (Exception ex)
                        {
                            errorRepeatCount++;
                            if (errorRepeatCount >= errorRepeatCountMax)
                                throw new Exception("Chyba pøi stahování dat èíselníku", ex);
                        }

                    } while (0 < errorRepeatCount && errorRepeatCount < errorRepeatCountMax);

                    offset += buffer.Length;
                    this.BeginInvoke((MethodInvoker)delegate()
                    {
                        this.Description = "Stahují se data èíselníku\n" + (int)(offset * 100 / (float)filesize) + "%";
                    });
                } while (buffer.Length == bufferSize || offset < filesize); //pokracuje, dokud nenacte vse

                fs.Flush();
                fs.Close();
                fs = null;


                #region Hash check
                //Program.mstw.mbw.Zprava = "Kontrola stažených dat dávky inventury";
                //IAsyncResult ares = ftransferService.BeginCheckFileHash(MST_Global.TerminalID, davkafilename, null, null);
                //string LocalFileHash = MySystem.FileOperations.CheckFileHash(tmpfiledavka);
                //ares.AsyncWaitHandle.WaitOne();
                //string ServerFileHash = ftransferService.EndCheckFileHash(ares);
                //if (LocalFileHash != ServerFileHash)
                //    throw new Exception("MD5 hash check failed!");
                #endregion

                ftransferService.Delete(MST_Global.TerminalID, fileciselnik);
                */
                #endregion

                #region new Download

                if (File.Exists(fileciselniktmp))
                    File.Delete(fileciselniktmp);

                int errorRepeatCount = 0;
                int errorRepeatCountMax = 5;
                byte[] buffer = new byte[4096];
                long filesize = 0;
                long offset = 0;

                // slouzi pro mereni casu stahovani a odhadu dokonceni stahovani ...
                //OpenNETCF.Diagnostics.Stopwatch stopwatch = new OpenNETCF.Diagnostics.Stopwatch();
                //stopwatch.Start();

                FileStream fileStream = new FileStream(fileciselniktmp, FileMode.Create, FileAccess.ReadWrite);

                string url = Logging.LogConfig.KomServer + @"SqlCEDBs/" + LogConfig.MachineID + @"/" + Path.GetFileName(fileciselnik);
                //string FilePath = Path.Combine(UploadPath, FileName);
                System.Net.WebRequest wr = System.Net.WebRequest.Create(url);
                wr.Proxy = System.Net.GlobalProxySelection.Select;
                wr.Timeout = 1000;
                //long newFilesize = ftransferService.GetFileSize(MST_Global.TerminalID, fileciselnik);
                try
                {
                    using (System.Net.WebResponse response = wr.GetResponse())
                    {
                        filesize = response.ContentLength;
                        using (Stream responseStream = response.GetResponseStream())
                        {
                            int dataRead = 0;
                            decimal procento;
                            decimal downloadspeed;
                            decimal lastingSeconds;
                            int prevTick = Environment.TickCount;

                            do
                            {
                                try
                                {
                                    if (responseStream.CanSeek)
                                        responseStream.Seek(offset, SeekOrigin.Begin);

                                    dataRead = responseStream.Read(buffer, 0, buffer.Length);

                                    fileStream.Write(buffer, 0, dataRead);

                                    offset += dataRead;
                                    errorRepeatCount = 0;

                                    #region zobrazeni informaci zbyvajicich
                                    //procento = (int)((offset / (float)filesize) * 100);
                                    procento = ((decimal)offset / (decimal)filesize) * 100;

                                    //downloadspeed = (double)((offset / (float)1024) / stopwatch.Elapsed.TotalSeconds);
                                    decimal kB = offset / 1024;
                                    decimal s = (decimal)(Environment.TickCount - prevTick) / (decimal)1000;
                                    downloadspeed = kB / s;
                                    prevTick = Environment.TickCount;

                                    decimal speed = ((downloadspeed == 0 ? 1 : downloadspeed) * 1024);
                                    decimal dev = (decimal)filesize - (decimal)dataRead;
                                    lastingSeconds = dev / speed;

                                    //lastingSeconds = (double)(filesize - offset) / (float)(downloadspeed * 1024);
                                    //TimeSpan odhad = TimeSpan.FromMinutes(lastingSeconds);
                                    
                                    this.BeginInvoke((MethodInvoker)delegate()
                                        {
                                            this.Description = String.Format("Stahují se data číselníku\nPercent: {0:###.00}% \nRychlost: {1:#####.00}kB/s\nOdhad: {2:###.00}s", procento, downloadspeed, lastingSeconds);
                                            //this.Description = "Stahují se data číselníku\n" + procento + "%\nRychlost:" + downloadspeed + " KB/s\nOdhad:" + odhad.Minutes + ":" + odhad.Seconds.ToString("00") + "\nChyb:" + errorRepeatCount;
                                        });
                                    #endregion

                                }
                                catch (Exception ex)
                                {
                                    errorRepeatCount++;
                                    if (errorRepeatCount >= errorRepeatCountMax)
                                        throw new Exception("Chyba pøi stahování dat císelníku", ex);
                                }

                            }
                            while (dataRead != 0 && errorRepeatCount < errorRepeatCountMax);
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

                try
                {
                    //MySystem.FileOperations.DBSave(Path.Combine(Main.DataDir, davka.ToString() + "." + Main.Inventura1I1), ref dataDavka);
                    //potvrzeni prijeti davky se povedlo => odstranit priponu tmp souboru
                    File.Delete(fileciselnik); //odmazani stareho
                    File.Move(fileciselniktmp, fileciselnik); //nahrani noveho ...
                }
                catch (Exception ex)
                {
                    throw new Exception("Nepodařilo se uložit číselník.\n" + ex.Message);
                }
            }
            catch (Exception ex)
            {
                if (fs != null)
                {
                    try { fs.Close(); }
                    catch { }
                }
                File.Delete(fileciselniktmp);
                throw new Exception(ex.Message);
            }
            finally
            {
            }
            #endregion
        }



    }
}

