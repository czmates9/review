using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FASK.SledovaniVyroby.ModuleIfc;
using System.IO;
using System.Reflection;
using FASK.SledovaniVyroby.Logging;
using FASK.SledovaniVyroby.Module.ZZS.Constants;

namespace FASK.SledovaniVyroby.Module.ZZS
{
    public enum TypPohybu
    {
        Predani_dodavateli_sluzeb_prani,
        Prevzeti_od_dodavatele,
        Unknow
    }


    public partial class RFID_Data : Form, IModuleConnector
    {

        private TypPohybu _typPohybu = TypPohybu.Unknow;
        public TypPohybu typPohyb
        {
            set { this._typPohybu = value; }
            get { return this._typPohybu; }
        }

        private string _cisloDavky = String.Empty;
        public string CisloDavky
        {
            set { this._cisloDavky = value; }
            get { return this._cisloDavky; }
        }


        #region Data
        private Fask.SQLiteDBs.DataSets.Odberatele.CZMST090Row _Odberatel;
        public Fask.SQLiteDBs.DataSets.Odberatele.CZMST090Row Odberatel
        {
            set { this._Odberatel = value; }
            get { return this._Odberatel; }
        }

        private Fask.SQLiteDBs.DataSets.TypDokladu.CZMST092Row _typdokladu;
        public Fask.SQLiteDBs.DataSets.TypDokladu.CZMST092Row Typdokladu
        {
            set { this._typdokladu = value; }
            get { return this._typdokladu; }
        }

        private Fask.SQLiteDBs.DataSets.Sklady.CZMST093Row _skladZdroj;
        public Fask.SQLiteDBs.DataSets.Sklady.CZMST093Row SkladZdroj
        {
            set { this._skladZdroj = value; }
            get { return this._skladZdroj; }
        }

        private Fask.SQLiteDBs.DataSets.Sklady.CZMST093Row _skladCil;
        public Fask.SQLiteDBs.DataSets.Sklady.CZMST093Row SkladCil
        {
            set { this._skladCil = value; }
            get { return this._skladCil; }
        }

        private Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIDataTable _nasnimanaData;
        public Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIDataTable NasnimanaData
        {
            set { this._nasnimanaData = value; }
            get { return this._nasnimanaData; }
        }
        #endregion

        //private string _cislodavkysqlfilename = string.Empty;
        //private string _zbozifilename = string.Empty;
        //private string _Prodejfilename = string.Empty;

        //private string configFilePath = string.Empty;


        public const string ProdejOExt = "di";

        public FASK.SledovaniVyroby.IScannerProvider.IScannerProvider Scanner = null;
        private string scannerTypeName = string.Empty;

        private Fask.SQLiteDBs.DataSets.Zbozi _katalogZbozi = null;

        //private Fask.SQLiteDBs.DataSets.ProdejTableAdapters.CZMST_DITableAdapter dita;

        private ProdejService.ProdejService prodejService;

        #region RFID
        public FASK.SledovaniVyroby.IRFIDProvider.IRFIDProvider RFID = null;


        #endregion

        #region Vybrany row

        public NasnimanaData.DataTable1Row rowCode
        {
            get
            {
                try
                {
                    return ((DataRowView)(dataGridView1.BindingContext[bindingSource1].Current)).Row as NasnimanaData.DataTable1Row;
                }
                catch
                {
                    return null;
                }
            }
        }

        public Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIRow rowDI
        {
            get
            {
                try
                {
                    return ((DataRowView)(dataGridView2.BindingContext[bindingSource2].Current)).Row as Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIRow;
                }
                catch
                {
                    return null;
                }
            }
        }

        #endregion

        #region RFID

        private System.Collections.Generic.List<string> nasnimaneKody = new List<string>();

        #endregion


        public RFID_Data()
        {
            InitializeComponent();
            //this.WindowState = FormWindowState.Maximized;

            //init scanner
            LoadAssembliesScanner();
            ScannerStart();

            RFID = FASK.SledovaniVyroby.RFIDFactory.RFIDFactory.Init();

          //  this.configFilePath = (new Uri(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase))).LocalPath;
        }

        public RFID_Data(
            TypPohybu typ,
            string cisloDavky,
            Fask.SQLiteDBs.DataSets.Odberatele.CZMST090Row odberatel,
            Fask.SQLiteDBs.DataSets.TypDokladu.CZMST092Row typdokladu,
            Fask.SQLiteDBs.DataSets.Sklady.CZMST093Row skladZdroj,
            Fask.SQLiteDBs.DataSets.Sklady.CZMST093Row skladCil,
            Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIDataTable NasnimanaData
        )
            : this()
        {
            this._typPohybu = typ;
            this._cisloDavky = cisloDavky;
            this._Odberatel = odberatel;
            this._typdokladu = typdokladu;
            this._skladZdroj = skladZdroj;
            this._skladCil = skladCil;
            this._nasnimanaData = NasnimanaData;
        }


        #region Scanner


        private void LoadAssembliesScanner()
        {
            Scanner = FASK.SledovaniVyroby.ScannerFactory.ScannerFactory.Init();
            scannerTypeName = FASK.SledovaniVyroby.ScannerFactory.ScannerFactory.GetScannerTypeName();
        }

        private void CloseTerminateScanner()
        {
            if (Scanner != null)
            {
                ScannerStop();

                Scanner.Disable();
                Scanner.TerminateScanner();
            }
        }

        private void ScannerStart()
        {
            try
            {
                if (Scanner != null)
                {
                    Scanner.DataReady -= new FASK.SledovaniVyroby.IScannerProvider.ScannerEventHandler(Scanner_DataReady);
                    Scanner.DataReady += new FASK.SledovaniVyroby.IScannerProvider.ScannerEventHandler(Scanner_DataReady);
                    Scanner.Enable();
                }

            }
            catch (Exception ex)
            {
                ErrorLog.Log.WriteException(ex);
            }
        }

        private void ScannerStop()
        {
            try
            {
                if (Scanner != null)
                {
                    Scanner.DataReady -= new FASK.SledovaniVyroby.IScannerProvider.ScannerEventHandler(Scanner_DataReady);
                    Scanner.Disable();
                }

            }
            catch (Exception ex)
            {
                ErrorLog.Log.WriteException(ex);
            }
        }

        delegate void BarcodeReadedDelegate(FASK.SledovaniVyroby.IScannerProvider.ScannerEventArgs e);


        void Scanner_DataReady(object sender, FASK.SledovaniVyroby.IScannerProvider.ScannerEventArgs e)
        {
            this.BeginInvoke(new BarcodeReadedDelegate(ScannerDataReceived), new object[] { e });
        }


        private void ScannerDataReceived(FASK.SledovaniVyroby.IScannerProvider.ScannerEventArgs e)
        {
            try
            {
                //SQLCEDB.DataSets.ZboziTableAdapters.CZMST095TableAdapter ta_zbozi = new SQLCEDB.DataSets.ZboziTableAdapters.CZMST095TableAdapter();
                //ta_zbozi.Connection.ConnectionString = "Data source=" + _zbozifilename;

                if (_katalogZbozi == null)
                    _katalogZbozi = new Fask.SQLiteDBs.DataSets.Zbozi();

                
                //ta_zbozi.FillByCarKod(_katalogZbozi.CZMST095, e.BarcodeData.Trim());
                Module.ZZS.frmMainZZS.prodejInstance.globalObject.controller_zbozi.FillByCarKod(_katalogZbozi.CZMST095, e.BarcodeData.Trim());



                if (_katalogZbozi.CZMST095 == null || _katalogZbozi.CZMST095.Count == 0)
                {

                }
                else if (_katalogZbozi.CZMST095.Count == 1)
                {
                    pridatPolozku(_katalogZbozi.CZMST095[0], e.BarcodeData.Trim());
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                ErrorLog.Log.WriteException(ex);
                FlexibleMessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return;

            //string barcode = e.BarcodeData;

            //nasnimaneKody.AddRange(data);
            //nasnimaneKody.Add(e.BarcodeData);
            //PridejNasnimanePolozky();

            //PridatJednuPolozkuDoSeznamu(barcode);

            //lblScannerCodeValue.Text = barcode;
            //if (e.ScannedCodeType == FASK.SledovaniVyroby.IScannerProvider.Code.NoData)
            //{
            //    lblScannerCode.Text = "X";
            //    lblScannerCode.ForeColor = Color.Black;
            //}
            //if (e.ScannedCodeType == FASK.SledovaniVyroby.IScannerProvider.Code.NoRead)
            //{
            //    lblScannerCode.Text = "N";
            //    lblScannerCode.ForeColor = Color.Red;
            //}
            //else if (e.ScannedCodeType == FASK.SledovaniVyroby.IScannerProvider.Code.Read)
            //{
            //    lblScannerCode.Text = "R";
            //    lblScannerCode.ForeColor = Color.Green;
            //}
            //else
            ////if (
            ////e.ScannedCodeType == FASK.SledovaniVyroby.IScannerProvider.Code.BadRead
            ////||
            ////e.ScannedCodeType == FASK.SledovaniVyroby.IScannerProvider.Code.TooLong
            ////)
            //{
            //    lblScannerCode.Text = "E";
            //    lblScannerCode.ForeColor = Color.DarkOrange;
            //}

            //dataVyroba.ScannerActivate(barcode, e.ScannedCodeType);

            //dataVyroba.inkrementScannerAcitvatedCount();

            //updateForm();
        }

        private void pridatPolozku(Fask.SQLiteDBs.DataSets.Zbozi.CZMST095Row cZMST095Row, string barcode)
        {


            try
            {
                //TODO : Vyzadat zadani mnozstvi...

                //FormInputQuantity qtyData = new FormInputQuantity();
                //this.Hide();
                //qtyData.MdiParent = this.MdiParent;
                //qtyData.Show();
                //qtyData.WindowState = FormWindowState.Maximized;
                //qtyData.FormClosed += new FormClosedEventHandler(qtyData_FormClosed);
                decimal kod;
                using (FormInputQuantity frmKod = new FormInputQuantity())
                {
                    if (frmKod.ShowDialog(this) == DialogResult.Cancel)
                    {
                        return;
                    }

                    kod = frmKod.Mnozstvi;

                }

                NasnimanaData.DataTable1Row row = nasnimanaData1.DataTable1.NewDataTable1Row();
                row.Code = barcode;
                row.Count = 1;
                nasnimanaData1.DataTable1.AddDataTable1Row(row);

                PridatPolozkuSkenner_CarKod(cZMST095Row, barcode, kod);
            }
            catch (Exception ex)
            {
                ErrorLog.Log.WriteException(ex);

            }
        }


        void qtyData_FormClosed(object sender, FormClosedEventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
            this.Show();
            this.WindowState = FormWindowState.Maximized;

            //throw new NotImplementedException();
        }

        private void PridatPolozkuSkenner_CarKod(Fask.SQLiteDBs.DataSets.Zbozi.CZMST095Row _zbozi, string barcode, decimal qty)
        {

            try
            {
                Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIRow di = prodej1.CZMST_DI.NewCZMST_DIRow();

                di.CountEntries = int.Parse(_cisloDavky);
                di.VNDITNUM = _zbozi.IsVNDITNUMNull() ? string.Empty : _zbozi.VNDITNUM;
                di.CZ_CarKod = _zbozi.IsCZ_CarKodNull() ? string.Empty : _zbozi.CZ_CarKod;
                di.SetODB_IDNull();
                di.SetSTR_IDNull();
                di.DOC_ID = _typdokladu.doc_id; //item.IsITEMDESCNull() ? string.Empty : item.ITEMDESC.Trim();
                di.DOC_ID2 = _typdokladu.doc_id2;
                di.SKL_ID = _skladZdroj.skl_id.Trim();
                di.SetPRAC_IDNull();
                di.ITEMNMBR = string.Empty;
                di.ITEMDESC = _zbozi.IsITEMDESCNull() ? string.Empty : _zbozi.ITEMDESC;
                di.SetITEMCODENull();
                di.LOCNCODE = "1";
                di.MJ = string.Empty;
                di.QTYSHPPD = qty; // mnozstvi jeden
                di.QTYSHPPDMJ = qty; // mnoztvi jeden
                di.QTYPACK = 0;
                di.SERLTNUM = string.Empty; // EPC na seriove cislo
                di.SetTAXAMPIENull();
                di.SetAMOUNPIENull();
                di.SetWITHTAXNull();
                di.SetPRICEXNull();
                di.Setmena_IDNull();
                di.SetTAXAMPIEMNull();
                di.SetAMOUNPIEMNull();
                di.Setmena_IDMNull();
                di.SetREZ_1Null();
                di.SetREZ_2Null();
                di.SetREZ_3Null();
                di.SetREZ_4Null();
                di.USER_ID = int.Parse(LogConfig.LoginID);
                di.DATEDONE = DateTime.Now.ToString("yyyyMMdd");
                di.TIMEDONE = DateTime.Now.ToString("HHmmss");
                //di.DEX_ROW_ID= "";
                di.guid = Guid.NewGuid();
                di.INPUT_MODE = 1;
                di.ID_TERMINAL = int.Parse(LogConfig.MachineID);
                di.LOCNCODEDEST = "1";
                di.SKL_ID_DEST = _skladCil.skl_id.Trim();
                di.WEIGHT = 0;
                di.SetNMBRPALNull();
                di.SetTYPEPALNull();
                di.PRINTED = false;

                prodej1.CZMST_DI.AddCZMST_DIRow(di);
                //DI_DirectInsert(dita, di);
                DI_DirectInsert( di);
            }
            catch (Exception ex)
            {
                ErrorLog.Log.WriteException(ex);

            }
        }

        /// <summary>
        /// Pridat Jednu Novou Nasnimanou Polozku z RFID
        /// </summary>
        /// <param name="barcode">nasnimane EPC ktere jeste nebylo nasnimano</param>
        private void PridatJNNP(string barcode)
        {
            try
            {
                NasnimanaData.DataTable1Row row = nasnimanaData1.DataTable1.NewDataTable1Row();
                row.Code = barcode;
                row.Count = 0;
                row.Count = row.Count + 1;
                nasnimanaData1.DataTable1.AddDataTable1Row(row);

                PridatPolozkuRFID_EPC(barcode);
            }
            catch (Exception ex)
            {
                ErrorLog.Log.WriteException(ex);

            }
        }

        #endregion

        #region IModuleConnector Members

        //Ikona oznameni
        private NotifyIcon notifyIconState;
        public NotifyIcon NotifyIconState
        {
            get { return notifyIconState; }
            set { notifyIconState = value; }
        }

        //Status label modulu
        private ToolStripStatusLabel statusLabel = null;
        public ToolStripStatusLabel StatusLabel
        {
            set { statusLabel = value; }
        }


        public void ReturnPortsToPreviousState()
        {
            //throw new NotImplementedException();
        }

        public void ClosePorts()
        {
            //throw new NotImplementedException();
        }

        public bool IsReadyToClose(out string message)
        {
            message = string.Empty;

            //if (dataVyroba.GetStavVyroba() != DataVyroba.VyrobaStavy.LogIDSmena)
            //{
            //    message = "Pro vypnutí aplikace je nutné provést odhlášení směny!";
            //    return false;
            //}
            //else
            //{
            //    dataVyroba.SaveActualDataLogScreen();
            return true;
            //}
        }

        #endregion

        private void panelMAIN_Resize(object sender, EventArgs e)
        {
            panelLeft.Size = new Size(panelMAIN.Width / 2, panelMAIN.Height);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                if (FlexibleMessageBox.Show(string.Format("Odeslat dávku č.{0} ?", _cisloDavky), this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.OK)
                {
                    string status = odeslatDavku(this._cisloDavky);
                    if (status == "OK")
                        this.Close();
                    else
                        throw new Exception(status);
                }
            }
            catch (Exception ex)
            {
                FlexibleMessageBox.Show(ex.Message, "Chyba při odesílání dávky :" + ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                try
                {
                    ErrorLog.Log.WriteException(ex.InnerException.InnerException.Message);
                    FlexibleMessageBox.Show(ex.InnerException.InnerException.Message, "Chyba při odesílání dávky :" + ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                }
                catch (Exception exx)
                {
                    FlexibleMessageBox.Show(exx.Message, "Chyba při odesílání dávky :" + ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                }
            }
        }

        private string odeslatDavku(string cisloDavky)
        {
            if (true) // dat do kontroleru
            {
                int cd = int.Parse(cisloDavky);
                System.Data.SqlServerCe.SqlCeCommand scecommand = null;
                int pocetpolozek = 0;
                try
                {

                    scecommand = new System.Data.SqlServerCe.SqlCeCommand(
                        "Select count(*) from czmst_di",
                        new System.Data.SqlServerCe.SqlCeConnection("Data source=" + this._cislodavkysqlfilename)
                    );
                    scecommand.Connection.Open();

                    pocetpolozek = (int)scecommand.ExecuteScalar();
                }
                finally
                {
                    if (scecommand != null && scecommand.Connection.State == ConnectionState.Open)
                        scecommand.Connection.Close();
                } 
            }

            if (pocetpolozek <= 0)
            {
                //MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prodej3ProdejMainDavkaNeobsahujePolozky, cd.ToString()), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                return "NicKOdeslani";
            }

            try
            {
                //Fask.MST_W.Program.mstw.mbw.BeginPracujiForm(string.Format(Fask.Localization.Localization.Prodej3ProdejMainOdesilamDavku, cd.ToString()));

                // bool result = 
                //Program.mstw.mbw.EndPracujiForm();

                if (FASK.SledovaniVyroby.Module.ZZS.Forms.ProdejServiceOperations.SendData(prodejService, cd))
                {
                    //if (Prodej.Globals.ProdejDialogUspesnehoOdeslaniDavky)
                    //MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prodej3ProdejMainDavkaOdeslana, cd.ToString()), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    if (false)
                        FlexibleMessageBox.Show(string.Format("Dávka č.{0} uspešne odeslána.", this._cisloDavky), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return "OK";
                }
                else
                {
                    //FlexibleMessageBox.Show(string.Format("Dávka č.{0} neodeslána.", this._cisloDavky), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return string.Format("Dávka č.{0} neodeslána.", this._cisloDavky);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Log.WriteException(ex);
                //Program.mstw.mbw.EndPracujiForm();
                throw ex;
                //FlexibleMessageBox.Show(ex.Message, "Chyba při odesílání dávky", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                //Program.mstw.mbw.EndPracujiForm();
            }
        }



        private void UpdateStatus()
        {
            lbl_TypPohybu.Text = this._typPohybu.ToString();
        }

        private void RFID_Data_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'prodej1.CZMST_DI' table. You can move, or remove it, as needed.

            prodejService = new ProdejService.ProdejService();
            prodejService.Url = Logging.LogConfig.KomServer + "Prodej.asmx";
            prodejService.Timeout = Properties.Settings.Default.TimeOut_Ciselniky;

            this.prodejService.ProcessSoupisCompleted -= new ProdejService.ProcessSoupisCompletedEventHandler(ProcessSoupisEnd);
            this.prodejService.ProcessSoupisCompleted += new ProdejService.ProcessSoupisCompletedEventHandler(ProcessSoupisEnd);


            //string configFilePath = (new Uri(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase))).LocalPath;
            //this._cislodavkysqlfilename = Path.Combine(configFilePath, @"SQLCEDB\" + this._cisloDavky + "." + ProdejOExt);

            //this._zbozifilename = Path.Combine(configFilePath, @"SQLCEDB\Zbozi.sdf"); //CiselnikZboziDB
            ////this._zbozifilename = Common.CiselnikZboziDB; //CiselnikZboziDB

            //this._Prodejfilename = Path.Combine(configFilePath, @"SQLCEDB\Prodej.sdf");


            //dita = new SQLCEDB.DataSets.ProdejTableAdapters.CZMST_DITableAdapter();
            //dita.Connection.ConnectionString = "Data source=" + _cislodavkysqlfilename;


            if (this._nasnimanaData != null && this._nasnimanaData.Count() > 0) 
            {
                foreach (var item in this._nasnimanaData)
                {
                    prodej1.CZMST_DI.ImportRow(item);    
                }
            }


            UpdateStatus();

            RFIDStart();

            
        }

        private void PridatPolozkuRFID_EPC(string EPC)
        {

            try
            {
                //TODO Dodelat nacitavani polozky z ciselniku zbozi
                //SQLCEDB.DataSets.ProdejTableAdapters.CZMST_DITableAdapter dita = new SQLCEDB.DataSets.ProdejTableAdapters.CZMST_DITableAdapter();
                //dita.Connection.ConnectionString = "Data source=" + _cislodavkysqlfilename;

                //SQLCEDB.DataSets.ZboziTableAdapters.CZMST095TableAdapter ta095 = new SQLCEDB.DataSets.ZboziTableAdapters.CZMST095TableAdapter();
                //ta095.Connection.ConnectionString = "Data source=" + _zbozifilename;


                ProdejService.Location ds = OnlineGetMaterial(string.Empty, _skladZdroj.skl_id.Trim(), EPC);

                if (ds == null)
                {
                    #region Pokud nenavaze spojeni tak vlozi takto

                    Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIRow diRowNOonline = prodej1.CZMST_DI.NewCZMST_DIRow();

                    diRowNOonline.CountEntries = int.Parse(_cisloDavky);
                    diRowNOonline.SetVNDITNUMNull();
                    diRowNOonline.SetCZ_CarKodNull();
                    diRowNOonline.SetODB_IDNull();
                    diRowNOonline.SetSTR_IDNull();
                    diRowNOonline.DOC_ID = _typdokladu.doc_id; //item.IsITEMDESCNull() ? string.Empty : item.ITEMDESC.Trim();
                    diRowNOonline.DOC_ID2 = _typdokladu.doc_id2;
                    diRowNOonline.SetSKL_IDNull();
                    diRowNOonline.SetPRAC_IDNull();
                    diRowNOonline.ITEMNMBR = string.Empty;
                    diRowNOonline.SetITEMDESCNull(); 
                    diRowNOonline.SetITEMCODENull();
                    diRowNOonline.LOCNCODE = "1";
                    diRowNOonline.MJ = string.Empty;
                    diRowNOonline.QTYSHPPD = 1; // mnozstvi jeden
                    diRowNOonline.QTYSHPPDMJ = 1; // mnoztvi jeden
                    diRowNOonline.QTYPACK = 0; // pocet baleni 0
                    diRowNOonline.SERLTNUM = EPC; // EPC na seriove cislo
                    diRowNOonline.SetTAXAMPIENull();
                    diRowNOonline.SetAMOUNPIENull();
                    diRowNOonline.SetWITHTAXNull();
                    diRowNOonline.SetPRICEXNull();
                    diRowNOonline.Setmena_IDNull();
                    diRowNOonline.SetTAXAMPIEMNull();
                    diRowNOonline.SetAMOUNPIEMNull();
                    diRowNOonline.Setmena_IDMNull();
                    diRowNOonline.SetREZ_1Null();
                    diRowNOonline.SetREZ_2Null();
                    diRowNOonline.SetREZ_3Null();
                    diRowNOonline.SetREZ_4Null();
                    diRowNOonline.USER_ID = int.Parse(LogConfig.LoginID);
                    diRowNOonline.DATEDONE = DateTime.Now.ToString("yyyyMMdd");
                    diRowNOonline.TIMEDONE = DateTime.Now.ToString("HHmmss");
                    //diRowNOonline.DEX_ROW_ID= "";
                    diRowNOonline.guid = Guid.NewGuid();
                    diRowNOonline.INPUT_MODE = 1;
                    diRowNOonline.ID_TERMINAL = int.Parse(LogConfig.MachineID);
                    diRowNOonline.LOCNCODEDEST = "1";
                    diRowNOonline.SetSKL_ID_DESTNull();
                    diRowNOonline.WEIGHT = 0;
                    diRowNOonline.SetNMBRPALNull();
                    diRowNOonline.SetTYPEPALNull();
                    diRowNOonline.PRINTED = false;

                    prodej1.CZMST_DI.AddCZMST_DIRow(diRowNOonline);
                    DI_DirectInsert(diRowNOonline);

                    #endregion
                }
                else
                {
                    #region Pokud najde v Lokacnem mechanizne tak vlozi takto
                    if (ds.CZMST_SkladLokace_Stav.Count > 0)
                    {

                        foreach (ProdejService.Location.CZMST_SkladLokace_StavRow item in ds.CZMST_SkladLokace_Stav)
                        {
                            //SQLCEDB.DataSets.Prodej.CZMST_DIRow di = prodej1.CZMST_DI.NewCZMST_DIRow();
                            //var _zbozi = ta095.GetDataByPolozkacisloSkladEquals(item.ITEMNMBR.Trim(), item.SKL_ID.Trim());
                            var _zbozi = ta095.GetDataByITEMNMBR(item.ITEMNMBR.Trim());

                            #region Nove vkladani

                            if (_zbozi != null && _zbozi.Count > 0)
                            {
                                Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIRow diRowonline = prodej1.CZMST_DI.NewCZMST_DIRow();

                                diRowonline.CountEntries = int.Parse(_cisloDavky);
                                diRowonline.VNDITNUM = _zbozi[0].IsVNDITNUMNull() ? string.Empty : _zbozi[0].VNDITNUM.Trim();
                                diRowonline.CZ_CarKod = _zbozi[0].IsCZ_CarKodNull() ? string.Empty : _zbozi[0].CZ_CarKod.Trim();
                                diRowonline.SetODB_IDNull();
                                diRowonline.SetSTR_IDNull();
                                diRowonline.DOC_ID = _typdokladu.doc_id; //item.IsITEMDESCNull() ? string.Empty : item.ITEMDESC.Trim();
                                diRowonline.DOC_ID2 = _typdokladu.doc_id2;
                                diRowonline.SKL_ID = _skladZdroj.skl_id.Trim();
                                diRowonline.SetPRAC_IDNull();
                                diRowonline.ITEMNMBR = item.ITEMNMBR;
                                diRowonline.ITEMDESC = _zbozi[0].IsITEMDESCNull() ? string.Empty : _zbozi[0].ITEMDESC.Trim(); 
                                diRowonline.ITEMCODE = _zbozi[0].IsITEMCODENull() ? string.Empty : _zbozi[0].ITEMCODE.Trim();
                                diRowonline.LOCNCODE = "1";
                                diRowonline.MJ = string.Empty;
                                diRowonline.QTYSHPPD = 1; // mnozstvi jeden
                                diRowonline.QTYSHPPDMJ = 1; // mnoztvi jeden
                                diRowonline.QTYPACK = 0;
                                diRowonline.SERLTNUM = item.SERLTNUM; // EPC na seriove cislo
                                diRowonline.SetTAXAMPIENull();
                                diRowonline.SetAMOUNPIENull();
                                diRowonline.SetWITHTAXNull();
                                diRowonline.SetPRICEXNull();
                                diRowonline.Setmena_IDNull();
                                diRowonline.SetTAXAMPIEMNull();
                                diRowonline.SetAMOUNPIEMNull();
                                diRowonline.Setmena_IDMNull();
                                diRowonline.SetREZ_1Null();
                                diRowonline.SetREZ_2Null();
                                diRowonline.SetREZ_3Null();
                                diRowonline.SetREZ_4Null();
                                diRowonline.USER_ID = int.Parse(LogConfig.LoginID);
                                diRowonline.DATEDONE = DateTime.Now.ToString("yyyyMMdd");
                                diRowonline.TIMEDONE = DateTime.Now.ToString("HHmmss");
                                //diRowonline.DEX_ROW_ID= "";
                                diRowonline.guid = Guid.NewGuid();
                                diRowonline.INPUT_MODE = 1;
                                diRowonline.ID_TERMINAL = int.Parse(LogConfig.MachineID);
                                diRowonline.LOCNCODEDEST = "1";
                                diRowonline.SKL_ID_DEST = _skladCil.skl_id.Trim();
                                diRowonline.WEIGHT = 0;
                                diRowonline.SetNMBRPALNull();
                                diRowonline.SetTYPEPALNull();
                                diRowonline.PRINTED = false;

                                prodej1.CZMST_DI.AddCZMST_DIRow(diRowonline);
                                DI_DirectInsert(diRowonline);
                            }
                            else
                            {
                                Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIRow diRowNOonlineZbozi = prodej1.CZMST_DI.NewCZMST_DIRow();

                                diRowNOonlineZbozi.CountEntries = int.Parse(_cisloDavky);
                                diRowNOonlineZbozi.SetVNDITNUMNull();
                                diRowNOonlineZbozi.SetCZ_CarKodNull();
                                diRowNOonlineZbozi.SetODB_IDNull();
                                diRowNOonlineZbozi.SetSTR_IDNull();
                                diRowNOonlineZbozi.DOC_ID = _typdokladu.doc_id; //item.IsITEMDESCNull() ? string.Empty : item.ITEMDESC.Trim();
                                diRowNOonlineZbozi.DOC_ID2 = _typdokladu.doc_id2;
                                diRowNOonlineZbozi.SKL_ID = _skladZdroj.skl_id.Trim();
                                diRowNOonlineZbozi.SetPRAC_IDNull();
                                diRowNOonlineZbozi.ITEMNMBR = item.ITEMNMBR;
                                diRowNOonlineZbozi.SetITEMDESCNull();
                                diRowNOonlineZbozi.SetITEMCODENull();
                                diRowNOonlineZbozi.LOCNCODE = "1";
                                diRowNOonlineZbozi.MJ = string.Empty;
                                diRowNOonlineZbozi.QTYSHPPD = 1; // mnozstvi jeden
                                diRowNOonlineZbozi.QTYSHPPDMJ = 1; // mnoztvi jeden
                                diRowNOonlineZbozi.QTYPACK = 0;
                                diRowNOonlineZbozi.SERLTNUM = item.SERLTNUM; // EPC na seriove cislo
                                diRowNOonlineZbozi.SetTAXAMPIENull();
                                diRowNOonlineZbozi.SetAMOUNPIENull();
                                diRowNOonlineZbozi.SetWITHTAXNull();
                                diRowNOonlineZbozi.SetPRICEXNull();
                                diRowNOonlineZbozi.Setmena_IDNull();
                                diRowNOonlineZbozi.SetTAXAMPIEMNull();
                                diRowNOonlineZbozi.SetAMOUNPIEMNull();
                                diRowNOonlineZbozi.Setmena_IDMNull();
                                diRowNOonlineZbozi.SetREZ_1Null();
                                diRowNOonlineZbozi.SetREZ_2Null();
                                diRowNOonlineZbozi.SetREZ_3Null();
                                diRowNOonlineZbozi.SetREZ_4Null();
                                diRowNOonlineZbozi.USER_ID = int.Parse(LogConfig.LoginID);
                                diRowNOonlineZbozi.DATEDONE = DateTime.Now.ToString("yyyyMMdd");
                                diRowNOonlineZbozi.TIMEDONE = DateTime.Now.ToString("HHmmss");
                                //diRowNOonlineZbozi.DEX_ROW_ID= "";
                                diRowNOonlineZbozi.guid = Guid.NewGuid();
                                diRowNOonlineZbozi.INPUT_MODE = 1;
                                diRowNOonlineZbozi.ID_TERMINAL = int.Parse(LogConfig.MachineID);
                                diRowNOonlineZbozi.LOCNCODEDEST = "1";
                                diRowNOonlineZbozi.SKL_ID_DEST = _skladCil.skl_id.Trim();
                                diRowNOonlineZbozi.WEIGHT = 0;
                                diRowNOonlineZbozi.SetNMBRPALNull();
                                diRowNOonlineZbozi.SetTYPEPALNull();
                                diRowNOonlineZbozi.PRINTED = false;

                                prodej1.CZMST_DI.AddCZMST_DIRow(diRowNOonlineZbozi);
                                DI_DirectInsert(diRowNOonlineZbozi);
                            }
                            #endregion

                            #region Puvodne vkladani
                            //DateTime dtnow = DateTime.Now;
                            //Guid newGuid = Guid.NewGuid();
                            //di.CountEntries = int.Parse(this._cisloDavky);
                            ////di.ItemDescription = zbozi.ITEMDESC;
                            //di.ITEMNMBR = item.ITEMNMBR.Trim();
                            ////TaD resit i lokace? pri zmene skladu?
                            //di.LOCNCODE = "1";       //string.Empty // TODO : lokace?
                            //di.LOCNCODEDEST = "1";   //string.Empty; // TODO : lokace
                            //di.ODB_ID = string.Empty;//(_odberatel == null ? string.Empty : _odberatel.odb_id.Trim());
                            //di.STR_ID = string.Empty;//(_stredisko == null ? string.Empty : _stredisko.str_id);
                            //di.PRAC_ID = string.Empty;//(_pracovnik == null ? string.Empty : _pracovnik.prac_id);
                            //di.DOC_ID = (_typdokladu == null ? string.Empty : _typdokladu.doc_id);
                            //di.DOC_ID2 = (_typdokladu == null ? string.Empty : _typdokladu.doc_id2);
                            //di.QTYPACK = 0; // qtypack ... ???
                            //di.REZ_1 = string.Empty; //???
                            //di.REZ_2 = string.Empty; //???
                            //di.REZ_3 = string.Empty; //???
                            //di.REZ_4 = string.Empty; //???
                            //di.SERLTNUM = item.SERLTNUM.Trim();
                            //di.DATEDONE = dtnow.ToString("yyyyMMdd");
                            //di.TIMEDONE = dtnow.ToString("HHmmss");
                            //di.USER_ID = int.Parse(LogConfig.LoginID); //MST_Global.UserID;
                            //di.guid = newGuid;
                            //di.ITEMDESC = item.IsITEMDESCNull() ? string.Empty : item.ITEMDESC;
                            //di.VNDITNUM = string.Empty; // DBNull
                            //di.CZ_CarKod = string.Empty;// DBNull
                            //di.INPUT_MODE = (byte)0; //_input_mode;
                            //// TODO : dohledat???
                            //di.ITEMCODE = string.Empty; //zbozi.IsITEMCODENull() ? string.Empty : zbozi.ITEMCODE;   // 3.6.2016 PeV: Doplneno, neprobihalo nastaveni ITEMCODE
                            //di.ID_TERMINAL = int.Parse(LogConfig.MachineID);
                            //// TODO : weight dohledat ...???
                            ////if (zbozi.IsWEIGHTNull())
                            ////    di.SetWEIGHTNull();
                            ////else
                            ////    di.WEIGHT = zbozi.WEIGHT;
                            //di.WEIGHT = 0;

                            //// TODO : sklad dest?
                            ////di.SKL_ID_DEST = sklad_id_dest;

                            //di.PRINTED = false;
                            //di.SKL_ID = _skladZdroj.skl_id.Trim();

                            ////TaD 16.2.2018
                            //di.SKL_ID_DEST = _skladCil.skl_id.Trim();

                            //// TODO : merna jednotka ... 
                            ////di.MJ = zbozi.MJ;
                            //di.MJ = string.Empty;
                            //// TODO : v jednotce ...
                            //di.QTYSHPPDMJ = 0;
                            ////di.QTYSHPPD = qty * (zbozi.QTYPACK > 0 ? zbozi.QTYPACK : 1);

                            ////ZZS nepoziva palety
                            ////if (nmbrpal != null)
                            ////{
                            ////    if (nmbrpal.Code != null)
                            ////        di.NMBRPAL = nmbrpal.Code.Trim();

                            ////    di.TYPEPAL = string.Empty;  // TODO: dodelat ...
                            ////    // neni implementovano ...
                            ////    //if (nmbrpal.Code != null)
                            ////    //    di.TYPEPAL = nmbrpal.Type.Trim();
                            ////}

                            //// TODO : meny doplnit doplneni men a cen se deje jinde v GLobals.zjistcenu, Globals.nastavcenu ...
                            ////di.mena_ID = _zbozi.MENA_ID;
                            ////di.mena_IDM = _mena == null ? null : _mena.mena_ID;
                            ////di.TAXAMPIEM = null;
                            ////di.AMOUNPIEM = null;

                            //#region ZZS nepouziva
                            ////SqlCEDBs.DataSets.Zbozi.CZMST095Row zbozi = null;
                            ////Price price = new Price();
                            ////var zbozipolozky = ta_zbozi.GetDataByPolozkacisloSkladLike(di.ITEMNMBR.Trim(), _skladZdroj.skl_id.Trim());
                            ////if (zbozipolozky.Count() > 0)
                            ////    zbozi = zbozipolozky.First();
                            ////if (zbozi != null)
                            ////    Prodej.Globals.zjisti_cenu(zbozi, _odberatel, _mena, price); // price je objekt, tedy odkazem => meni se vlastnosti ...
                            ////else
                            ////    Logging.Log.Write("Zbozi nenalezeno, ceny nedohledany...");
                            ////Prodej.Globals.nastav_cenu(di, price);

                            //#endregion

                            //prodej1.CZMST_DI.AddCZMST_DIRow(di);
                            //DI_DirectInsert(dita, di);
                            #endregion
                        }

                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Log.WriteException(ex);
            }
        }


        void RFID_DataReady(object sender, IRFIDProvider.RFIDEventArgs e)
        {
            AddData(e.TagIDs);
        }

        public void AddData(List<string> list)
        {
            if (list == null)
                return;

            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate() { this.AddData(list); });
                return;
            }

            try
            {
                nasnimaneKody.AddRange(list);
                PridejNasnimanePolozky();
            }
            catch (Exception ex)
            {
                ErrorLog.Log.WriteException(ex);
            }
        }



        private static void DI_DirectInsert(Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIRow di)
        {
            try
            {
                Module.ZZS.frmMainZZS.prodejInstance.globalObject.controller_prodej.Insert_DI(
            di.CountEntries,
            di.IsODB_IDNull() ? null : di.ODB_ID,
            di.IsSTR_IDNull() ? null : di.STR_ID,
            di.IsDOC_IDNull() ? null : di.DOC_ID,
            di.IsDOC_ID2Null() ? null : di.DOC_ID2,
            di.ITEMNMBR,
            di.LOCNCODE,
            di.QTYSHPPD,
            di.IsQTYPACKNull() ? (decimal?)null : di.QTYPACK,
            di.SERLTNUM,
            di.IsTAXAMPIEMNull() ? (decimal?)null : di.TAXAMPIE,
            di.IsAMOUNPIENull() ? (decimal?)null : di.AMOUNPIE,
            di.IsWITHTAXNull() ? (byte?)null : di.WITHTAX,
            di.IsPRICEXNull() ? (byte?)null : di.PRICEX,
            di.IsREZ_1Null() ? null : di.REZ_1,
            di.IsREZ_2Null() ? null : di.REZ_2,
            di.IsREZ_3Null() ? null : di.REZ_3,
            di.IsREZ_4Null() ? null : di.REZ_4,
            di.IsUSER_IDNull() ? (int?)null : di.USER_ID,
            di.IsDATEDONENull() ? null : di.DATEDONE,
            di.IsTIMEDONENull() ? null : di.TIMEDONE,
            di.guid,
            di.IsVNDITNUMNull() ? null : di.VNDITNUM,
            di.IsCZ_CarKodNull() ? null : di.CZ_CarKod,
            di.IsSKL_IDNull() ? null : di.SKL_ID,
            di.MJ,
            di.QTYSHPPDMJ,
            di.IsPRAC_IDNull() ? null : di.PRAC_ID,
            di.INPUT_MODE,
            di.ID_TERMINAL,
            di.IsTYPEPALNull() ? null : di.TYPEPAL, //_paleta == null ? null : _paleta.Typ,
            di.IsNMBRPALNull() ? null : di.NMBRPAL,//_paleta == null ? null : _paleta.Cislo,
            di.IsITEMCODENull() ? null : di.ITEMCODE,
            di.Ismena_IDNull() ? null : di.mena_ID,
            di.IsTAXAMPIEMNull() ? (decimal?)null : di.TAXAMPIEM,
            di.IsAMOUNPIEMNull() ? (decimal?)null : di.AMOUNPIEM,
            di.Ismena_IDMNull() ? null : di.mena_IDM,
            di.IsITEMDESCNull() ? null : di.ITEMDESC,
            di.IsLOCNCODEDESTNull() ? null : di.LOCNCODEDEST,
            di.IsSKL_ID_DESTNull() ? null : di.SKL_ID_DEST,
            di.IsWEIGHTNull() ? (decimal?)null : di.WEIGHT,
            di.PRINTED,
            di.IsDEX_ROW_IDNull() ? 0 : di.DEX_ROW_ID,
            di.IsEXPIRACENull() ? (DateTime?)null : di.EXPIRACE,
            di.IsAttributeToSNNull() ? string.Empty : di.AttributeToSN

        );
            }
            catch (Exception ex)
            {
                ErrorLog.Log.WriteException(ex);

            }
        }

        private ProdejService.Location OnlineGetMaterial(string itemnmbr, string skl_id, string serltnum)
        {
            ProdejService.Location ds = new ProdejService.Location();

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                ds = prodejService.Online_GetMaterial(itemnmbr.Trim(), skl_id.Trim(), serltnum.Trim(), _typdokladu != null ? _typdokladu.doc_id : string.Empty);
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                ErrorLog.Log.Write(ex.Message, "RFID.RFID_Data, OnlineGetMaterial");
                //MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);

                return null;
            }
            return ds;
        }

        private void RFID_Data_Shown(object sender, EventArgs e)
        {

        }

        #region RFID


        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //Timer_Test_.Enabled = !Timer_Test_.Enabled;
                if (RFID.isOpen())
                {
                    RFIDStop(); // CloseRFID();
                    
                }
                else
                {
                    RFIDStart(); // OpenRFID();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Log.WriteException(ex);
            }
        }



        private void RFIDStart()
        {
            try
            {
                if (RFID != null)
                {
                    RFID.DataReady -= new FASK.SledovaniVyroby.IRFIDProvider.RFIDHandler(RFID_DataReady);
                    RFID.DataReady += new FASK.SledovaniVyroby.IRFIDProvider.RFIDHandler(RFID_DataReady);
                    RFID.Start();

                    buttonRFID.BackColor = Color.Green;
                }

            }
            catch (Exception ex)
            {
                buttonRFID.BackColor = Color.Red;
                ErrorLog.Log.WriteException(ex);
            }
        }

        private void RFIDStop()
        {
            try
            {
                if (RFID != null)
                {
                    RFID.DataReady -= new FASK.SledovaniVyroby.IRFIDProvider.RFIDHandler(RFID_DataReady);
                    RFID.Stop();
                    buttonRFID.BackColor = Color.Red;
                }

            }
            catch (Exception ex)
            {
                buttonRFID.BackColor = Color.Red;
                ErrorLog.Log.WriteException(ex);
            }
        }



        public bool PridejNasnimanePolozky()
        {
            while (nasnimaneKody.Count > 0)
            {
                NajdiPolozkuCarovyKod(nasnimaneKody[0]);
                try { nasnimaneKody.RemoveAt(0); }
                catch { }
            }
            //UpdateUI();

            return true;
        }

        /// <summary>
        /// Kontrola zda už byla pirdana polozka  a kolikrat
        /// </summary>
        /// <param name="ck"></param>
        private void NajdiPolozkuCarovyKod(string ck)
        {
            try
            {

                NasnimanaData.DataTable1Row r = null;

                var result = nasnimanaData1.DataTable1.Where(x => (x.Code.Equals(ck, StringComparison.CurrentCultureIgnoreCase)));

                if (result == null || result.Count() == 0)
                {
                    PridatJNNP(ck);
                }
                else
                {
                    r = result.First();
                    r.Count = r.Count + 1;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Log.WriteException(ex);
                FlexibleMessageBox.Show(ex.Message, "EXCEPTION");
            }
        }


        #endregion

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
                try
                {

                    //bool x1 = prodej1.CZMST_DI.Any(x => (x.SERLTNUM.Trim() == row.Cells[0].Value.ToString().Trim()));
                    //bool x2 = prodej1.CZMST_DI.Any(x => ((x.IsCZ_CarKodNull() ? string.Empty : x.CZ_CarKod.Trim()) == row.Cells[0].Value.ToString().Trim()));
                    //bool x3 = prodej1.CZMST_DI.Any(x => ((x.IsVNDITNUMNull() ? string.Empty : x.VNDITNUM.Trim()) == row.Cells[0].Value.ToString().Trim()));

                    if (prodej1.CZMST_DI.Any(x =>
                        (x.SERLTNUM.Trim() == row.Cells[0].Value.ToString().Trim())
                        || 
                        ((x.IsCZ_CarKodNull() ? string.Empty : x.CZ_CarKod.Trim()) == row.Cells[0].Value.ToString().Trim())
                        || 
                        ((x.IsVNDITNUMNull() ? string.Empty : x.VNDITNUM.Trim()) == row.Cells[0].Value.ToString().Trim())
                        ))
                    {
                        row.Cells[0].Style.BackColor = Color.Green;
                        row.Cells[1].Style.BackColor = Color.Green;
                    }
                    else
                    {
                        row.Cells[0].Style.BackColor = Color.Red;
                        row.Cells[1].Style.BackColor = Color.Red;
                    }
                }
                catch (Exception ex)
                {
                    ErrorLog.Log.WriteException(ex);
                    FlexibleMessageBox.Show(ex.Message, "EXCEPTION");
                }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            this.dataGridView1.ClearSelection();
            //tssl_Nasnimane.Text = "Vybrany kod : " + rowCode.Code;

            //this.dataGridView1.Rows[this.dataGridView1.RowCount - 1].Selected = true; //(this.DataGridView1.RowCount - 1).Selected = True
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            //this.dataGridView2.ClearSelection();
            //tssl_DI.Text = "Vybrany kod : " + rowDI.SERLTNUM;

        }

        private void RFID_Data_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
                                
            {
                this.prodejService.ProcessSoupisCompleted += new ProdejService.ProcessSoupisCompletedEventHandler(ProcessSoupisEnd);
                this.prodejService.ProcessSoupisCompleted -= new ProdejService.ProcessSoupisCompletedEventHandler(ProcessSoupisEnd);

                CloseTerminateScanner();
                RFIDStop();
            }
            catch (Exception ex)
            {
                ErrorLog.Log.WriteException(ex);
            }
        }

        private void btn_smazat_Click(object sender, EventArgs e)
        {
            try
            {
                if (rowDI != null)
                    rowDI.Delete();

                dita.Update(prodej1.CZMST_DI);
            }
            catch (Exception ex)
            {
                ErrorLog.Log.WriteException(ex);
            }

        }

        private void btn_zrusit_Click(object sender, EventArgs e)
        {
            if (FlexibleMessageBox.Show(string.Format("Opravdu zrušit dávku č.{0}", this._cisloDavky), this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
            {
                File.Delete(Path.Combine(Common.StorageDir, String.Format(@"SQLiteDBs\{0}.{1}", frmMainZZS.prodejInstance.globalObject.Davka, Common.Ext_Prodej)));
                this.Close();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            ProdejService.ProdejData localProdejOut = new ProdejService.ProdejData();


            //foreach (var item in this.prodej1.CZMST_DEH)
            //{
            //    localProdejOut.CZMST_DEH.ImportRow(item);
            //}

            foreach (var item in this.prodej1.CZMST_DI)
            {
                localProdejOut.CZMST_DI.ImportRow(item);
            }

            //foreach (var item in this.prodej1.CZMST_DI_RFID)
            //{
            //    localProdejOut.CZMST_DI_RFID.ImportRow(item);
            //}

            //foreach (var item in this.prodej1.CZMST_DIH)
            //{
            //    localProdejOut.CZMST_DIH.ImportRow(item);
            //}

            OnlineTisk(localProdejOut);
        }

        private void OnlineTisk(ProdejService.ProdejData data)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                prodejService.ProcessSoupisAsync(data);// Online_GetMaterial(itemnmbr, skl_id, serltnum, _typdokladu != null ? _typdokladu.doc_id : string.Empty);               
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                ErrorLog.Log.Write(ex.Message, "RFID.RFID_Data, OnlineTisk");
                FlexibleMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void ProcessSoupisEnd(object sender, FASK.SledovaniVyroby.Module.ZZS.ProdejService.ProcessSoupisCompletedEventArgs e)
        {
            if (e.Result.Status == ProdejService.StatusResultEnum.OK)
            {
                foreach (var row in prodej1.CZMST_DI)
                {
                    row.PRINTED = true;
                }

                dita.Update(prodej1.CZMST_DI);
                //Module.ZZS.frmMainZZS.prodejInstance.globalObject.controller_prodej.Update_CZMST095(prodej1.CZMST_DI);

            }
            else if (e.Result.Status == ProdejService.StatusResultEnum.WARNING)
            {
                FlexibleMessageBox.Show(e.Result.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (e.Result.Status == ProdejService.StatusResultEnum.ERROR)
            {
                FlexibleMessageBox.Show(e.Result.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else 
            {
                
            //...
                FlexibleMessageBox.Show("Nastala nespecifikovana chyba.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        public bool IsReadyToShow(out string message)
        {
            //throw new NotImplementedException();
            message = "ok";
            return true;
        }
    }
} 
