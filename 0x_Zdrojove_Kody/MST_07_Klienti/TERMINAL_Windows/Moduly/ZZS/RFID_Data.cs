using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Windows.Forms;
using FASK.MST_WINDOWS.ModuleIfc;
using System.IO;
using System.Reflection;
using FASK.MST_WINDOWS.Logging;


namespace FASK.MST_WINDOWS.Module.ZZS
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

        private string _typPohybName = String.Empty;
        public string typPohybName
        {
            set { this._typPohybName = value; }
            get { return this._typPohybName; }
        }

        private string _cisloDavky = String.Empty;
        public string CisloDavky
        {
            set { this._cisloDavky = value; }
            get { return this._cisloDavky; }
        }

        private System.Windows.Forms.Orientation _orientacePohledu = Orientation.Vertical;
        public System.Windows.Forms.Orientation OrientacePohledu
        {
            set { this._orientacePohledu = value; }
            get { return this._orientacePohledu; }
        }

        #region Data
        private FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.Odberatele.CZMST090Row _Odberatel;
        public FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.Odberatele.CZMST090Row Odberatel
        {
            set { this._Odberatel = value; }
            get { return this._Odberatel; }
        }

        private FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.TypDokladu.CZMST092Row _typdokladu;
        public FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.TypDokladu.CZMST092Row Typdokladu
        {
            set { this._typdokladu = value; }
            get { return this._typdokladu; }
        }

        private FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.Sklady.CZMST093Row _skladZdroj;
        public FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.Sklady.CZMST093Row SkladZdroj
        {
            set { this._skladZdroj = value; }
            get { return this._skladZdroj; }
        }

        private FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.Sklady.CZMST093Row _skladCil;
        public FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.Sklady.CZMST093Row SkladCil
        {
            set { this._skladCil = value; }
            get { return this._skladCil; }
        }

        private FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.Prodej.CZMST_DIDataTable _nasnimanaData;
        public FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.Prodej.CZMST_DIDataTable NasnimanaData
        {
            set { this._nasnimanaData = value; }
            get { return this._nasnimanaData; }
        }
        #endregion

        private string _cislodavkysqlfilename = string.Empty;
        //private string _zbozifilename = string.Empty;
        //private string _Prodejfilename = string.Empty;

       // private string configFilePath = string.Empty;


        public const string ProdejOExt = "di";

        public FASK.MST_WINDOWS.IScannerProvider.IScannerProvider Scanner = null;
        private string scannerTypeName = string.Empty;

        private FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.Zbozi _katalogZbozi = null;

        private FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.ProdejTableAdapters.CZMST_DITableAdapter dita;

        private FASK.MST_WINDOWS.Main._WebRefernces_Globals.ProdejServiceSession prodejService;

        private FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.ZboziTableAdapters.CZMST095TableAdapter ta095;
        private FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.PracovniciTableAdapters.CZMST096TableAdapter ta096;

        private PrintReportLibrary.DataSets.DS_Soupis dt_ZmetkyPrint;

        #region RFID
        
        public IRFIDProvider.IRFIDProvider RFID = null;


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

        public FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.Prodej.CZMST_DIRow rowDI
        {
            get
            {
                try
                {
                    return ((DataRowView)(dataGridView2.BindingContext[bindingSource2].Current)).Row as FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.Prodej.CZMST_DIRow;
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

            RFID = RFIDFactory.RFIDFactory.Init();

            //this.configFilePath = 

            UpdateStatusStripLeft();
            UpdateStatusStripRight();
        }

        public RFID_Data(
            TypPohybu typ,
            string typName,
            string cisloDavky,
            System.Windows.Forms.Orientation Orientace,
            FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.Odberatele.CZMST090Row odberatel,
            FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.TypDokladu.CZMST092Row typdokladu,
            FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.Sklady.CZMST093Row skladZdroj,
            FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.Sklady.CZMST093Row skladCil,
            FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.Prodej.CZMST_DIDataTable NasnimanaData
        )
            : this()
        {
            this._typPohybu = typ;
            this._typPohybName = typName;
            this._cisloDavky = cisloDavky;
            this._orientacePohledu = Orientace;
            this._Odberatel = odberatel;
            this._typdokladu = typdokladu;
            this._skladZdroj = skladZdroj;
            this._skladCil = skladCil;
            this._nasnimanaData = NasnimanaData;
        }


        #region Scanner


        private void LoadAssembliesScanner()
        {
            Scanner = FASK.MST_WINDOWS.ScannerFactory.ScannerFactory.Init();
            scannerTypeName = FASK.MST_WINDOWS.ScannerFactory.ScannerFactory.GetScannerTypeName();
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
                    Scanner.DataReady -= new FASK.MST_WINDOWS.IScannerProvider.ScannerEventHandler(Scanner_DataReady);
                    Scanner.DataReady += new FASK.MST_WINDOWS.IScannerProvider.ScannerEventHandler(Scanner_DataReady);
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
                    Scanner.DataReady -= new FASK.MST_WINDOWS.IScannerProvider.ScannerEventHandler(Scanner_DataReady);
                    Scanner.Disable();
                }

            }
            catch (Exception ex)
            {
                ErrorLog.Log.WriteException(ex);
            }
        }

        delegate void BarcodeReadedDelegate(FASK.MST_WINDOWS.IScannerProvider.ScannerEventArgs e);


        void Scanner_DataReady(object sender, FASK.MST_WINDOWS.IScannerProvider.ScannerEventArgs e)
        {
            this.BeginInvoke(new BarcodeReadedDelegate(ScannerDataReceived), new object[] { e });
        }


        private void ScannerDataReceived(FASK.MST_WINDOWS.IScannerProvider.ScannerEventArgs e)
        {
            try
            {
                FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.ZboziTableAdapters.CZMST095TableAdapter ta_zbozi = new FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.ZboziTableAdapters.CZMST095TableAdapter();
                ta_zbozi.Connection.ConnectionString = "Data source=" + MyPath.ZboziDirectory;

                if (_katalogZbozi == null)
                    _katalogZbozi = new FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.Zbozi();

                ta_zbozi.FillByCarKod(_katalogZbozi.CZMST095, e.BarcodeData.Trim());


                if (_katalogZbozi.CZMST095 == null || _katalogZbozi.CZMST095.Count == 0)
                {
                    FlexibleMessageBox.Show(this, string.Format("Čárový kód:{0} nenalezen v číselníku zboží.", e.BarcodeData.Trim()), this.typPohybName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
                else if (_katalogZbozi.CZMST095.Count == 1)
                {
                    pridatPolozku(_katalogZbozi.CZMST095[0], e.BarcodeData.Trim());
                }
                else
                {
                    FlexibleMessageBox.Show(this, string.Format("Čárový kód:{0} nenalezen {1}-krát v číselníku zboží.", e.BarcodeData.Trim(), _katalogZbozi.CZMST095.Count), this.typPohybName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Log.WriteException(ex);
                FlexibleMessageBox.Show(this, ex.Message, this._typPohybName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return;

            //string barcode = e.BarcodeData;

            //nasnimaneKody.AddRange(data);
            //nasnimaneKody.Add(e.BarcodeData);
            //PridejNasnimanePolozky();

            //PridatJednuPolozkuDoSeznamu(barcode);

            //lblScannerCodeValue.Text = barcode;
            //if (e.ScannedCodeType == FASK.MST_WINDOWS.IScannerProvider.Code.NoData)
            //{
            //    lblScannerCode.Text = "X";
            //    lblScannerCode.ForeColor = Color.Black;
            //}
            //if (e.ScannedCodeType == FASK.MST_WINDOWS.IScannerProvider.Code.NoRead)
            //{
            //    lblScannerCode.Text = "N";
            //    lblScannerCode.ForeColor = Color.Red;
            //}
            //else if (e.ScannedCodeType == FASK.MST_WINDOWS.IScannerProvider.Code.Read)
            //{
            //    lblScannerCode.Text = "R";
            //    lblScannerCode.ForeColor = Color.Green;
            //}
            //else
            ////if (
            ////e.ScannedCodeType == FASK.MST_WINDOWS.IScannerProvider.Code.BadRead
            ////||
            ////e.ScannedCodeType == FASK.MST_WINDOWS.IScannerProvider.Code.TooLong
            ////)
            //{
            //    lblScannerCode.Text = "E";
            //    lblScannerCode.ForeColor = Color.DarkOrange;
            //}

            //dataVyroba.ScannerActivate(barcode, e.ScannedCodeType);

            //dataVyroba.inkrementScannerAcitvatedCount();

            //updateForm();
        }

        private void pridatPolozku(FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.Zbozi.CZMST095Row cZMST095Row, string barcode)
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
                    frmKod.ZboziRow = cZMST095Row;

                    if (frmKod.ShowDialog(this) == DialogResult.Cancel)
                    {
                        return;
                    }

                    kod = frmKod.Mnozstvi;

                }

                NasnimanaData.DataTable1Row row = nasnimanaData1.DataTable1.NewDataTable1Row();
                row.Code = barcode.Trim();
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

        private void PridatPolozkuSkenner_CarKod(FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.Zbozi.CZMST095Row _zbozi, string barcode, decimal qty)
        {

            try
            {
                FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.Prodej.CZMST_DIRow di = prodej1.CZMST_DI.NewCZMST_DIRow();

                di.CountEntries = int.Parse(_cisloDavky);
                di.VNDITNUM = _zbozi.IsVNDITNUMNull() ? string.Empty : _zbozi.VNDITNUM.Trim();
                di.CZ_CarKod = _zbozi.IsCZ_CarKodNull() ? string.Empty : _zbozi.CZ_CarKod.Trim();
                di.SetODB_IDNull();
                di.SetSTR_IDNull();
                di.DOC_ID = _typdokladu.doc_id; //item.IsITEMDESCNull() ? string.Empty : item.ITEMDESC.Trim();
                di.DOC_ID2 = _typdokladu.doc_id2;
                di.SKL_ID = _skladZdroj.skl_id.Trim();
                di.SetPRAC_IDNull();
                di.ITEMNMBR = string.Empty;
                di.ITEMDESC = _zbozi.IsITEMDESCNull() ? string.Empty : _zbozi.ITEMDESC.Trim();
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
                di.USER_ID = FASK.MST_WINDOWS.Main.Configuration.Config.LoginID ?? -1;
                di.DATEDONE = DateTime.Now.ToString("yyyyMMdd");
                di.TIMEDONE = DateTime.Now.ToString("HHmmss");
                //di.DEX_ROW_ID= "";
                di.guid = Guid.NewGuid();
                di.INPUT_MODE = 1;
                di.ID_TERMINAL = int.Parse(FASK.MST_WINDOWS.Main.Configuration.Config.Main_TerminalID);
                di.LOCNCODEDEST = "1";
                di.SKL_ID_DEST = _skladCil.skl_id.Trim();
                di.WEIGHT = 0;
                di.SetNMBRPALNull();
                di.SetTYPEPALNull();
                di.PRINTED = false;

                prodej1.CZMST_DI.AddCZMST_DIRow(di);
                DI_DirectInsert(dita, di);

                UpdateStatusStripLeft();
                UpdateStatusStripRight();
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
                NaplnLevuStranu(barcode);

                PridatPolozkuRFID_EPC(barcode);
            }
            catch (Exception ex)
            {
                ErrorLog.Log.WriteException(ex);

            }
        }

        private void NaplnLevuStranu(string barcode)
        {

            try
            {
                NasnimanaData.DataTable1Row row = null;



                FASK.MST_WINDOWS.Main.ProdejService.Location ds = OnlineGetMaterial(string.Empty, string.Empty, barcode);

                if (ds == null)
                {
                    row = nasnimanaData1.DataTable1.NewDataTable1Row();
                     
                    row.SKL_ID = null;
                    row.LOCNCODE = null;
                    row.Description = "";
                    row.ITEMDESC = "Nenalezen zaznam v lok. mechanizmu";
                    row.Prac_ID = "";
                    row.Prac_Desc = "";
                    row.Code = "-1";
                    row.Count = 0;
                    row.PocetNaSklade = 0;

                    //this.columnCode = base.Columns["Code"];
                    //this.columnCount = base.Columns["Count"];
                    //this.columnSKL_ID = base.Columns["SKL_ID"];
                    //this.columnLOCNCODE = base.Columns["LOCNCODE"];
                    //this.columnDescription = base.Columns["Description"];
                    //this.columnITEMDESC = base.Columns["ITEMDESC"];
                    //this.columnPrac_ID = base.Columns["Prac_ID"];
                    //this.columnPocetNaSklade = base.Columns["PocetNaSklade"];
                    //this.columnPrac_Desc = base.Columns["Prac_Desc"];
                   

                    Audio.PlaySound(Path.Combine(MyPath.SoundDirectory, Audio.SoundChyba));
                    //row.PocetNaSklade;
                }
                else
                {
                    if (ds.CZMST_SkladLokace_Stav.Count > 0)
                    {

                        //Nadefinovat stavy chybove...
                        //
                        //DOLE v else//1-nenalezeno v lokacnim mechanizmu >> cervena spatne...
                        //2- nalezeno v lokacnim mechanizmu, ale na spatnem sklade >> oranzove, upozorneni na jiny sklad 
                        //3-nalezeno v lokacnim mechanizmu, ale na spravnem sklade ale mnozstvi 0 >> tak je zbozi vyrazeno Modra
                        //4- nalezeno v lokacnim mechanizmu, ale na spravnem sklade i mnoztvi je 1 (pri EPC[seriove cislo]) tak je zelene


                        #region pokus podle napadu JiS

                        #if true

                        System.Data.EnumerableRowCollection<FASK.MST_WINDOWS.Main.ProdejService.Location.CZMST_SkladLokace_StavRow> res;

                        //if (isOnSKlad(ds, row,out res))
                        if (isOnSKlad(ds, this._skladZdroj, out res))
                        {
                            //Nalezeno na lokalnem sklade


                            //if (res.Count() == 1)
                            //{

                            //FASK.MST_WINDOWS.Module.ZZS.ProdejService.Location.CZMST_SkladLokace_StavDataTable ds_tmp = res.CopyToDataTable<FASK.MST_WINDOWS.Module.ZZS.ProdejService.Location.CZMST_SkladLokace_StavRow>();

                            FASK.MST_WINDOWS.Main.ProdejService.Location.CZMST_SkladLokace_StavDataTable dt_stav = CopyResToSkladLokace_Stav(res);

                            if (dt_stav.Count == 1)
                            {
                                //vracen pouze jeden radek to je spravne
                                if (dt_stav[0].QTYSHPPD == 1)
                                {
                                    FillDataTableNasnimane(dt_stav[0], out row, "OK");
                                    Audio.PlaySound(Path.Combine(MyPath.SoundDirectory, Audio.SoundInfo));
                                    //row.Description = "OK";
                                }
                                else if (dt_stav[0].QTYSHPPD == 0)
                                {
                                   var a = ds.CZMST_SkladLokace_Stav.Where(x => x.QTYSHPPD == 1);
                                   var dt = CopyResToSkladLokace_Stav(a);
                                   if (dt.Count == 1)
                                   {
                                       FillDataTableNasnimane(dt[0], out row, "jiny sklad");
                                       Audio.PlaySound(Path.Combine(MyPath.SoundDirectory, Audio.SoundDotaz));
                                   }
                                   else 
                                   {
                                       FillDataTableNasnimane(dt_stav[0], out row, "Vyradene");
                                       Audio.PlaySound(Path.Combine(MyPath.SoundDirectory, Audio.SoundChimes));
                                   }

                                }
                                else if (dt_stav[0].QTYSHPPD > 1)
                                {
                                    FillDataTableNasnimane(dt_stav[0], out row, "ERR moc mnozstvi");
                                }

                                else if (dt_stav[0].QTYSHPPD < 0)
                                {
                                    FillDataTableNasnimane(dt_stav[0], out row, "minus mnozstvi");
                                }



                            }
                            else 
                            {
                                //nalezeno jine mnozstvi radku nez jeden zle
                                throw new Exception("Nalezeno jine mnozstvi radku nez jeden zle");
                            }
                            #region MyRegion

                            //if (dt_stav[0].QTYSHPPD == 1)
                            //{
                            //    row.Description = "OK";
                            //}
                            //else if (dt_stav[0].QTYSHPPD == 0)
                            //{
                            //    row.Description = "vyradeno";
                            //}
                            //else if (dt_stav[0].QTYSHPPD > 1)
                            //{
                            //    row.Description = "ERR moc mnozstvi";
                            //}

                            //else if (dt_stav[0].QTYSHPPD < 0)
                            //{
                            //    row.Description = "minus mnozstvi";
                            //}

                            //var _zbozi = ta095.GetDataByITEMNMBR(dt_stav[0].ITEMNMBR.Trim());

                            //row.SKL_ID = dt_stav[0].SKL_ID.Trim();
                            //row.LOCNCODE = dt_stav[0].LOCNCODE.Trim();
                            //row.Prac_ID = dt_stav[0].IsPRAC_ID_OWNERNull() ? null : dt_stav[0].PRAC_ID_OWNER;
                            //row.PocetNaSklade = dt_stav[0].QTYSHPPD;

                            //if (_zbozi != null && _zbozi.Count > 0)
                            //{
                            //    row.ITEMDESC = _zbozi[0].ITEMDESC.Trim();
                            //}
                            //else
                            //{
                            //    row.ITEMDESC = string.Empty;
                            //}


                            //}
                            //else
                            //{
                            //    System.Data.EnumerableRowCollection<FASK.MST_WINDOWS.Module.ZZS.ProdejService.Location.CZMST_SkladLokace_StavRow> res2 = res.Where(x => x.QTYSHPPD == 1);

                            //    if (res2.Count() == 1)
                            //    {
                            //        FASK.MST_WINDOWS.Module.ZZS.ProdejService.Location.CZMST_SkladLokace_StavDataTable dt_skladAQty = new ProdejService.Location.CZMST_SkladLokace_StavDataTable();

                            //        DataTable dt = res2.CopyToDataTable();

                            //        foreach (DataRow radek in dt.Rows)
                            //        {
                            //            FASK.MST_WINDOWS.Module.ZZS.ProdejService.Location.CZMST_SkladLokace_StavRow radekTMP = dt_skladAQty.NewCZMST_SkladLokace_StavRow();
                            //            radekTMP.ITEMDESC = radek["ITEMDESC"].ToString();
                            //            radekTMP.ITEMNMBR = radek["ITEMNMBR"].ToString();
                            //            radekTMP.LOCNCODE = radek["LOCNCODE"].ToString();
                            //            radekTMP.PRAC_ID_OWNER = radek["PRAC_ID_OWNER"].ToString();
                            //            radekTMP.QTY_OWNER = decimal.Parse(radek["QTY_OWNER"].ToString());
                            //            radekTMP.QTYSHPPD = decimal.Parse(radek["QTYSHPPD"].ToString());
                            //            radekTMP.QTYSHPPD_DEF = decimal.Parse(radek["QTYSHPPD_DEF"].ToString());
                            //            radekTMP.SKL_ID = radek["SKL_ID"].ToString();

                            //            radekTMP.SERLTNUM = radek["SERLTNUM"].ToString();
                            //            radekTMP.DATECHANGE = DateTime.Parse(radek["DATECHANGE"].ToString());

                            //            if (string.IsNullOrEmpty(radek["EXPIRATION"].ToString()))
                            //            {
                            //                radekTMP.SetEXPIRATIONNull();
                            //            }
                            //            else
                            //            {
                            //                DateTime.Parse(radek["EXPIRATION"].ToString());
                            //            }



                            //            dt_skladAQty.AddCZMST_SkladLokace_StavRow(radekTMP);
                            //        }




                            //        row.Description = "OK";
                            //        row.SKL_ID = dt_skladAQty[0].SKL_ID.Trim();
                            //        row.LOCNCODE = dt_skladAQty[0].LOCNCODE.Trim();
                            //        row.Prac_ID = dt_skladAQty[0].IsPRAC_ID_OWNERNull() ? null : dt_skladAQty[0].PRAC_ID_OWNER;
                            //        row.PocetNaSklade = dt_skladAQty[0].QTYSHPPD;


                            //        var _zbozi = ta095.GetDataByITEMNMBR(dt_skladAQty[0].ITEMNMBR.Trim());

                            //        if (_zbozi != null && _zbozi.Count > 0)
                            //        {
                            //            row.ITEMDESC = _zbozi[0].ITEMDESC.Trim();
                            //        }
                            //        else
                            //        {
                            //            row.ITEMDESC = string.Empty;
                            //        }



                            //    }
                            //    else
                            //    {
                            //        throw new Exception("Nalezeno vic radku EPC na sklade a mnozstvi... asi lokace...");
                            //    }

                            //}


                            
                            #endregion


                        }
                        else
                        {
                            //res = ds.CZMST_SkladLokace_Stav.Where(x => x.QTYSHPPD == 1);

                            res = ds.CZMST_SkladLokace_Stav.Where(x => x.QTYSHPPD == 1);
                            var dt = CopyResToSkladLokace_Stav(res);
                            if (dt.Count == 1)
                            {
                                FillDataTableNasnimane(dt[0], out row, "jiny sklad");
                            }
                            else
                            {
                                FillDataTableNasnimane(null, out row, "CHYBA");
                                //throw new Exception("Chyba... ");
                                //FillDataTableNasnimane(dt_stav[0], out row, "Vyradene");
                            }
                        }

                        #endif

                        #endregion

                        #region puvodny pokus
                        ////this._nasnimanaData
                        //var res = ds.CZMST_SkladLokace_Stav.Where(x => x.SKL_ID == _skladZdroj.skl_id);

                        //if (res.Count() > 1)
                        //{
                        //    var res_qty = ds.CZMST_SkladLokace_Stav.Where(x => x.SKL_ID == _skladZdroj.skl_id && x.QTYSHPPD == 1);

                        //    if (res_qty.Count() == 1)
                        //    {
                        //        ProdejService.Location.CZMST_SkladLokace_StavDataTable dtt = (ProdejService.Location.CZMST_SkladLokace_StavDataTable)res_qty.CopyToDataTable();

                        //        row.Description = "Uspesne nalezeno";
                        //        row.SKL_ID = dtt[0].SKL_ID.Trim();
                        //        row.LOCNCODE = dtt[0].LOCNCODE.Trim();
                        //        row.Prac_ID = dtt[0].IsPRAC_ID_OWNERNull() ? null : dtt[0].PRAC_ID_OWNER;
                        //        row.PocetNaSklade = dtt[0].QTYSHPPD;
                        //    }



                        //}
                        //else
                        //{



                        //}


                        //var _zbozi = ta095.GetDataByITEMNMBR(item.ITEMNMBR.Trim());



                        ////ds.CZMST_SkladLokace_Stav.


                        //foreach (ProdejService.Location.CZMST_SkladLokace_StavRow item in ds.CZMST_SkladLokace_Stav)
                        //{




                        //    if (item.QTYSHPPD == 0)
                        //    {
                        //        row.Description = "Vyřazene zboží";
                        //    }
                        //    else if (item.QTYSHPPD > 0)
                        //    {

                        //    }
                        //    else
                        //    {
                        //        row.Description = "Nalezeno zaporné množství";
                        //    }


                        //    row.SKL_ID = item.SKL_ID.Trim();
                        //    row.LOCNCODE = item.LOCNCODE.Trim();
                        //    row.Prac_ID = item.IsPRAC_ID_OWNERNull() ? null : item.PRAC_ID_OWNER;
                        //    row.PocetNaSklade = item.QTYSHPPD;

                        //    if (_zbozi != null && _zbozi.Count > 0)
                        //    {
                        //        row.ITEMDESC = _zbozi[0].ITEMDESC.Trim();
                        //    }
                        //    else
                        //    {
                        //        row.ITEMDESC = string.Empty;
                        //    }
                        //}

                        #endregion
                    }
                    else
                    {
                        FillDataTableNasnimane(null, out row, "Nenalezen zaznam v lok. mechanizmu");
                        Audio.PlaySound(Path.Combine(MyPath.SoundDirectory, Audio.SoundChyba));
                        //row.Description = "Nenalezen zaznam v lok. mechanizmu";
                    }

                    row.Code = barcode;
                    row.Count = 0;
                    row.Count = row.Count + 1;


                    
                }

                nasnimanaData1.DataTable1.AddDataTable1Row(row);
            }
            catch (Exception ex)
            {
                FlexibleMessageBox.Show(this, ex.Message, "EXCEPTION");
            }
        }

        private void FillDataTableNasnimane(FASK.MST_WINDOWS.Main.ProdejService.Location.CZMST_SkladLokace_StavRow cZMST_SkladLokace_StavRow, out ZZS.NasnimanaData.DataTable1Row row, string p)
        {
            row = nasnimanaData1.DataTable1.NewDataTable1Row();

            row.Description = string.IsNullOrEmpty(p) ? string.Empty : p.Trim() ;

            if (cZMST_SkladLokace_StavRow != null)
            {

                FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.Zbozi.CZMST095DataTable _zbozi = ta095.GetDataByITEMNMBR(cZMST_SkladLokace_StavRow.ITEMNMBR.Trim());
                FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.Pracovnici.CZMST096DataTable _Pracovnici = ta096.GetDataByID(cZMST_SkladLokace_StavRow.PRAC_ID_OWNER.Trim());
                
                row.SKL_ID = cZMST_SkladLokace_StavRow.SKL_ID.Trim();
                row.LOCNCODE = cZMST_SkladLokace_StavRow.LOCNCODE.Trim();
                row.Prac_ID = cZMST_SkladLokace_StavRow.IsPRAC_ID_OWNERNull() ? null : cZMST_SkladLokace_StavRow.PRAC_ID_OWNER;
                row.PocetNaSklade = cZMST_SkladLokace_StavRow.QTYSHPPD;

                if (_zbozi != null && _zbozi.Count > 0)
                {
                    row.ITEMDESC = _zbozi[0].ITEMDESC.Trim();
                }
                else
                {
                    row.ITEMDESC = string.Empty;
                }

                if (_Pracovnici != null && _Pracovnici.Count > 0)
                {
                    row.Prac_Desc = _Pracovnici[0].prac_desc.Trim();
                }
                else
                {
                    row.Prac_Desc = string.Empty;
                }
            }
            else
            {
                row.SKL_ID = string.Empty;
                row.LOCNCODE = string.Empty;
                row.Prac_ID = string.Empty;
                row.PocetNaSklade = 0;
                row.ITEMDESC = string.Empty;
            }

        }


        private static FASK.MST_WINDOWS.Main.ProdejService.Location.CZMST_SkladLokace_StavDataTable CopyResToSkladLokace_Stav(System.Data.EnumerableRowCollection<FASK.MST_WINDOWS.Main.ProdejService.Location.CZMST_SkladLokace_StavRow> res)
        {
            FASK.MST_WINDOWS.Main.ProdejService.Location.CZMST_SkladLokace_StavDataTable dt_stav = new FASK.MST_WINDOWS.Main.ProdejService.Location.CZMST_SkladLokace_StavDataTable();
            if (res.Count() == 0)
            {
                return dt_stav;
            }

            DataTable dt = res.CopyToDataTable();

            foreach (DataRow radek in dt.Rows)
            {
                FASK.MST_WINDOWS.Main.ProdejService.Location.CZMST_SkladLokace_StavRow radekTMP = dt_stav.NewCZMST_SkladLokace_StavRow();
                radekTMP.ITEMDESC = radek["ITEMDESC"].ToString().Trim();
                radekTMP.ITEMNMBR = radek["ITEMNMBR"].ToString().Trim();
                radekTMP.LOCNCODE = radek["LOCNCODE"].ToString().Trim();
                radekTMP.PRAC_ID_OWNER = radek["PRAC_ID_OWNER"].ToString().Trim();
                radekTMP.QTY_OWNER = decimal.Parse(radek["QTY_OWNER"].ToString().Trim());
                radekTMP.QTYSHPPD = decimal.Parse(radek["QTYSHPPD"].ToString().Trim());
                radekTMP.QTYSHPPD_DEF = decimal.Parse(radek["QTYSHPPD_DEF"].ToString().Trim());
                radekTMP.SKL_ID = radek["SKL_ID"].ToString().Trim();

                radekTMP.SERLTNUM = radek["SERLTNUM"].ToString().Trim();
                radekTMP.DATECHANGE = DateTime.Parse(radek["DATECHANGE"].ToString().Trim());

                if (string.IsNullOrEmpty(radek["EXPIRATION"].ToString().Trim()))
                {
                    radekTMP.SetEXPIRATIONNull();
                }
                else
                {
                    DateTime.Parse(radek["EXPIRATION"].ToString().Trim());
                }



                dt_stav.AddCZMST_SkladLokace_StavRow(radekTMP);
            }

            return dt_stav;
        }

        //private bool isOnSKlad(ProdejService.Location ds, NasnimanaData.DataTable1Row row, out System.Data.EnumerableRowCollection<FASK.MST_WINDOWS.Module.ZZS.ProdejService.Location.CZMST_SkladLokace_StavRow> res)
        private bool isOnSKlad(FASK.MST_WINDOWS.Main.ProdejService.Location ds, FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.Sklady.CZMST093Row Sklad, out System.Data.EnumerableRowCollection<FASK.MST_WINDOWS.Main.ProdejService.Location.CZMST_SkladLokace_StavRow> res)
        {
            res = null;
            try
            {

                res = ds.CZMST_SkladLokace_Stav.Where(x => x.SKL_ID == Sklad.skl_id);

                if (res.Count() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                    //if (ds.CZMST_SkladLokace_Stav.Count == 1)
                    //{
                    //    var _zbozi = ta095.GetDataByITEMNMBR(ds.CZMST_SkladLokace_Stav[0].ITEMNMBR.Trim());

                    //    row.SKL_ID = ds.CZMST_SkladLokace_Stav[0].SKL_ID.Trim();
                    //    row.LOCNCODE = ds.CZMST_SkladLokace_Stav[0].LOCNCODE.Trim();
                    //    row.Prac_ID = ds.CZMST_SkladLokace_Stav[0].IsPRAC_ID_OWNERNull() ? null : ds.CZMST_SkladLokace_Stav[0].PRAC_ID_OWNER;
                    //    row.PocetNaSklade = ds.CZMST_SkladLokace_Stav[0].QTYSHPPD;
                    //    row.Description = "Nalezeno na jinem sklade";
                    //    if (_zbozi != null && _zbozi.Count > 0)
                    //    {
                    //        row.ITEMDESC = _zbozi[0].ITEMDESC.Trim();
                    //    }
                    //    else
                    //    {
                    //        row.ITEMDESC = string.Empty;
                    //    }

                    //}
                    //else 
                    //{
                    //    res = ds.CZMST_SkladLokace_Stav.Where(x => x.QTYSHPPD > 0);
                    //    return true;
                    //    //throw new Exception("Nalezeno na inem sklade ale víc kusu...");
                    //}

                    //return false;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Log.Write(ex);

                //row.Description = "ERR isOnSklad";

                return false;
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
            RFIDStop();
            //throw new NotImplementedException();
        }

        public bool IsReadyToClose(out string message)
        {
            message = string.Empty;
            try
            {


                if (prodej1.CZMST_DI.Count > 0)
                {
                    //FlexibleMessageBox.Show(this,"Je potřeba odeslat nebo zrušit dávku...", this.typPohybName, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    this.Close();
                    return true;
                }
                else
                {
                    File.Delete(this._cislodavkysqlfilename);
                    this.Close();

                    return true;
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }

            //if (dataVyroba.GetStavVyroba() != DataVyroba.VyrobaStavy.LogIDSmena)
            //{
            //    message = "Pro vypnutí aplikace je nutné provést odhlášení směny!";
            //    return false;
            //}
            //else
            //{
            //    dataVyroba.SaveActualDataLogScreen();
            //return true;
            //}
        }

        #endregion

        private void panelMAIN_Resize(object sender, EventArgs e)
        {
            panelLeft.Size = new Size(panelMAIN.Width / 2, panelMAIN.Height);
        }

        private void btn_Send_Click(object sender, EventArgs e)
        {

            try
            {
                if (FlexibleMessageBox.Show(this, string.Format("Odeslat dávku č.{0} ?", _cisloDavky), this._typPohybName, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.OK)
                {

                    if (RFID.isOpen())
                    {
                        RFIDStop(); // CloseRFID();

                    }
                    
                    string status = odeslatDavku(this._cisloDavky);
                    if (status == "OK")
                    {

                        btn_Print_Click(null, null);

                        this.Close();
                    }
                    else
                        throw new Exception(status);
                }
            }
            catch (Exception ex)
            {
                //FlexibleMessageBox.Show(this, ex.Message, "Chyba při odesílání dávky.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);           
                try
                {
                    if (ex.InnerException != null)
                    {
                        if (ex.InnerException.InnerException != null)
                        {
                            ErrorLog.Log.WriteException(String.Format("{0}\n{1}\n{2}", ex.Message, ex.InnerException.Message, ex.InnerException.InnerException.Message));
                            FlexibleMessageBox.Show(this, String.Format("{0}\n{1}\n{2}", ex.Message, ex.InnerException.Message, ex.InnerException.InnerException.Message), "Chyba při odesílání dávky.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        }
                        else
                        {
                            ErrorLog.Log.WriteException(String.Format("{0}\n{1}", ex.Message, ex.InnerException.Message));
                            FlexibleMessageBox.Show(this, String.Format("{0}\n{1}", ex.Message, ex.InnerException.Message), "Chyba při odesílání dávky.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        }
                    }
                    else
                    {
                        ErrorLog.Log.WriteException(ex.Message);
                        FlexibleMessageBox.Show(this, ex.Message, "Chyba při odesílání dávky.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);           
                    }

                }
                catch (Exception exx)
                {
                    FlexibleMessageBox.Show(this, exx.Message, "Chyba při odesílání dávky.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                }
            }
        }

        private string odeslatDavku(string cisloDavky)
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

            if (pocetpolozek <= 0)
            {
                //MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prodej3ProdejMainDavkaNeobsahujePolozky, cd.ToString()), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                return "Žádná data k Odeslání.";
            }

           

            try
            {
                //Fask.MST_W.Program.mstw.mbw.BeginPracujiForm(string.Format(Fask.Localization.Localization.Prodej3ProdejMainOdesilamDavku, cd.ToString()));

                // bool result = 
                //Program.mstw.mbw.EndPracujiForm();

                if (FASK.MST_WINDOWS.Forms.ProdejServiceOperations.SendData(prodejService, cd))
                {
                    //if (Prodej.Globals.ProdejDialogUspesnehoOdeslaniDavky)
                    //MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prodej3ProdejMainDavkaOdeslana, cd.ToString()), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    if (false)
                        FlexibleMessageBox.Show(this, string.Format("Dávka č.{0} uspešne odeslána.", this._cisloDavky), this._typPohybName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return "OK";
                }
                else
                {
                    //FlexibleMessageBox.Show(string.Format("Dávka č.{0} neodeslána.", this._cisloDavky), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return string.Format( "Dávka č.{0} neodeslána.", this._cisloDavky);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Log.WriteException(ex);
                //Program.mstw.mbw.EndPracujiForm();
                throw ex;
                //FlexibleMessageBox.Show(this,ex.Message, "Chyba při odesílání dávky", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                //Program.mstw.mbw.EndPracujiForm();
            }
        }



        private void UpdateTypDokladuNameStatus()
        {
            lbl_TypPohybu.Text = this._typPohybName;
        }

        private int CZMST_DI_NO_ITEMNMBR;
        private int LeftTableZnamo;
        private int LeftTableNeZnamo;

        private void UpdateStatusStripLeft()
        {
            tssl_Nasnimane_celkove.Text = string.Format("C:{0}", nasnimanaData1.DataTable1.Count);
            tssl_Nasnimane_znamo.Text = string.Format("(Z:{0}," ,LeftTableZnamo); ;
            tssl_Nasnimane_NEznamo.Text = string.Format("N:{0} )", LeftTableNeZnamo); ;
        }

        private void UpdateStatusStripRight()
        {
            CZMST_DI_NO_ITEMNMBR = 0;
            foreach (FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.Prodej.CZMST_DIRow item in prodej1.CZMST_DI)
            {
                if (string.IsNullOrEmpty(item.ITEMNMBR.Trim()))
                    CZMST_DI_NO_ITEMNMBR++;
                
            }

            tssl_DI.Text = string.Format("C:{0}", prodej1.CZMST_DI.Count);
            tssl_DI_noItemnmbr.Text = string.Format("(N:{0})", CZMST_DI_NO_ITEMNMBR);
        }


        private void RFID_Data_Load(object sender, EventArgs e)
        {

            this.splitContainer1.Orientation = this._orientacePohledu;

            // TODO: This line of code loads data into the 'prodej1.CZMST_DI' table. You can move, or remove it, as needed.

            prodejService = new FASK.MST_WINDOWS.Main._WebRefernces_Globals.ProdejServiceSession();
            prodejService.Url = FASK.MST_WINDOWS.Main.Configuration.Config.Main_KomServer + "Prodej.asmx";
            prodejService.Timeout = FASK.MST_WINDOWS.Module.ZZS.Properties.Settings.Default.TimeOut_DataProcess;


            this.prodejService.ProcessSoupisCompleted -= new FASK.MST_WINDOWS.Main.ProdejService.ProcessSoupisCompletedEventHandler(ProcessSoupisEnd);
            this.prodejService.ProcessSoupisCompleted += new FASK.MST_WINDOWS.Main.ProdejService.ProcessSoupisCompletedEventHandler(ProcessSoupisEnd);


            //string configFilePath = (new Uri(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase))).LocalPath;
            this._cislodavkysqlfilename = Path.Combine(MyPath.SQLCEDBSDirectory,  this._cisloDavky + "." + ProdejOExt);

            //this._zbozifilename = Path.Combine(MyPath.SQLCEDBDirectory, "Zbozi.sdf");
            //this._Prodejfilename = Path.Combine(MyPath.SQLCEDBDirectory, "Prodej.sdf");


            dita = new FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.ProdejTableAdapters.CZMST_DITableAdapter();
            dita.Connection.ConnectionString = "Data source=" + _cislodavkysqlfilename;


            if (this._nasnimanaData != null && this._nasnimanaData.Count() > 0) 
            {
                foreach (var item in this._nasnimanaData)
                {
                    prodej1.CZMST_DI.ImportRow(item);    
                }
            }

            ta095 = new FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.ZboziTableAdapters.CZMST095TableAdapter();
            ta095.Connection.ConnectionString = "Data source=" + MyPath.ZboziDirectory;

            ta096 = new FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.PracovniciTableAdapters.CZMST096TableAdapter();
            ta096.Connection.ConnectionString = "Data source=" + MyPath.PracovniciDirectory;


            UpdateTypDokladuNameStatus();
            UpdateStatusStripLeft();
            UpdateStatusStripRight();

            RFIDStart();



            
        }

        private void PridatPolozkuRFID_EPC(string EPC)
        {

            try
            {
                //TODO Dodelat nacitavani polozky z ciselniku zbozi
                FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.ProdejTableAdapters.CZMST_DITableAdapter dita = new FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.ProdejTableAdapters.CZMST_DITableAdapter();
                dita.Connection.ConnectionString = "Data source=" + _cislodavkysqlfilename;

                FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.ZboziTableAdapters.CZMST095TableAdapter ta095 = new FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.ZboziTableAdapters.CZMST095TableAdapter();
                ta095.Connection.ConnectionString = "Data source=" + MyPath.ZboziDirectory;


                FASK.MST_WINDOWS.Main.ProdejService.Location ds = OnlineGetMaterial(string.Empty, _skladZdroj.skl_id.Trim(), EPC);

                if (ds == null)
                {
                    #region Pokud nenavaze spojeni tak vlozi takto

                    FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.Prodej.CZMST_DIRow diRowNOonline = prodej1.CZMST_DI.NewCZMST_DIRow();

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
                    diRowNOonline.USER_ID = FASK.MST_WINDOWS.Main.Configuration.Config.LoginID ?? -1;
                    diRowNOonline.DATEDONE = DateTime.Now.ToString("yyyyMMdd");
                    diRowNOonline.TIMEDONE = DateTime.Now.ToString("HHmmss");
                    //diRowNOonline.DEX_ROW_ID= "";
                    diRowNOonline.guid = Guid.NewGuid();
                    diRowNOonline.INPUT_MODE = 1;
                    diRowNOonline.ID_TERMINAL = int.Parse(FASK.MST_WINDOWS.Main.Configuration.Config.Main_TerminalID);
                    diRowNOonline.LOCNCODEDEST = "1";
                    diRowNOonline.SetSKL_ID_DESTNull();
                    diRowNOonline.WEIGHT = 0;
                    diRowNOonline.SetNMBRPALNull();
                    diRowNOonline.SetTYPEPALNull();
                    diRowNOonline.PRINTED = false;

                    prodej1.CZMST_DI.AddCZMST_DIRow(diRowNOonline);
                    DI_DirectInsert(dita, diRowNOonline);

                    #endregion
                }
                else
                {
                    #region Pokud najde v Lokacnem mechanizne tak vlozi takto
                    if (ds.CZMST_SkladLokace_Stav.Count > 0)
                    {

                        foreach (FASK.MST_WINDOWS.Main.ProdejService.Location.CZMST_SkladLokace_StavRow item in ds.CZMST_SkladLokace_Stav)
                        {
                            if (item.QTYSHPPD > 0)
                            {
                                //SQLCEDB.DataSets.Prodej.CZMST_DIRow di = prodej1.CZMST_DI.NewCZMST_DIRow();
                                //var _zbozi = ta095.GetDataByPolozkacisloSkladEquals(item.ITEMNMBR.Trim(), item.SKL_ID.Trim());
                                var _zbozi = ta095.GetDataByITEMNMBR(item.ITEMNMBR.Trim());

                                #region Nove vkladani

                                if (_zbozi != null && _zbozi.Count > 0)
                                {
                                    FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.Prodej.CZMST_DIRow diRowonline = prodej1.CZMST_DI.NewCZMST_DIRow();

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
                                    diRowonline.USER_ID = FASK.MST_WINDOWS.Main.Configuration.Config.LoginID ?? -1;
                                    diRowonline.DATEDONE = DateTime.Now.ToString("yyyyMMdd");
                                    diRowonline.TIMEDONE = DateTime.Now.ToString("HHmmss");
                                    //diRowonline.DEX_ROW_ID= "";
                                    diRowonline.guid = Guid.NewGuid();
                                    diRowonline.INPUT_MODE = 1;
                                    diRowonline.ID_TERMINAL = int.Parse(FASK.MST_WINDOWS.Main.Configuration.Config.Main_TerminalID);
                                    diRowonline.LOCNCODEDEST = "1";
                                    diRowonline.SKL_ID_DEST = _skladCil.skl_id.Trim();
                                    diRowonline.WEIGHT = 0;
                                    diRowonline.SetNMBRPALNull();
                                    diRowonline.SetTYPEPALNull();
                                    diRowonline.PRINTED = false;

                                    prodej1.CZMST_DI.AddCZMST_DIRow(diRowonline);
                                    DI_DirectInsert(dita, diRowonline);
                                }
                                else
                                {
                                    FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.Prodej.CZMST_DIRow diRowNOonlineZbozi = prodej1.CZMST_DI.NewCZMST_DIRow();

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
                                    diRowNOonlineZbozi.USER_ID = FASK.MST_WINDOWS.Main.Configuration.Config.LoginID ?? -1;
                                    diRowNOonlineZbozi.DATEDONE = DateTime.Now.ToString("yyyyMMdd");
                                    diRowNOonlineZbozi.TIMEDONE = DateTime.Now.ToString("HHmmss");
                                    //diRowNOonlineZbozi.DEX_ROW_ID= "";
                                    diRowNOonlineZbozi.guid = Guid.NewGuid();
                                    diRowNOonlineZbozi.INPUT_MODE = 1;
                                    diRowNOonlineZbozi.ID_TERMINAL = int.Parse(FASK.MST_WINDOWS.Main.Configuration.Config.Main_TerminalID);
                                    diRowNOonlineZbozi.LOCNCODEDEST = "1";
                                    diRowNOonlineZbozi.SKL_ID_DEST = _skladCil.skl_id.Trim();
                                    diRowNOonlineZbozi.WEIGHT = 0;
                                    diRowNOonlineZbozi.SetNMBRPALNull();
                                    diRowNOonlineZbozi.SetTYPEPALNull();
                                    diRowNOonlineZbozi.PRINTED = false;

                                    prodej1.CZMST_DI.AddCZMST_DIRow(diRowNOonlineZbozi);
                                    DI_DirectInsert(dita, diRowNOonlineZbozi);
                                }
                                #endregion 
                            }

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

                UpdateStatusStripLeft();
                UpdateStatusStripRight();
            }
            catch (Exception ex)
            {
                ErrorLog.Log.WriteException(ex);
            }
        }


        void RFID_DataReady(object sender, IRFIDProvider.RFIDEventArgs e)
        {
            this.BeginInvoke((System.Threading.ThreadStart)delegate() {
            //this.Invoke((System.Threading.ThreadStart)delegate() {
                AddData(e.TagIDs);
            });
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



        private void DI_DirectInsert(FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.ProdejTableAdapters.CZMST_DITableAdapter dita, FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.Prodej.CZMST_DIRow di)
        {
            try
            {
                dita.Insert(
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
            di.PRINTED
        );

                UpdateStatusStripRight();
            }
            catch (Exception ex)
            {
                ErrorLog.Log.WriteException(ex);

            }
        }

        private FASK.MST_WINDOWS.Main.ProdejService.Location OnlineGetMaterial(string itemnmbr, string skl_id, string serltnum)
        {
            FASK.MST_WINDOWS.Main.ProdejService.Location ds = new FASK.MST_WINDOWS.Main.ProdejService.Location();

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                ds = prodejService.Online_GetMaterial(itemnmbr.Trim(), skl_id.Trim(), serltnum.Trim(), _typdokladu != null ? _typdokladu.doc_id : string.Empty, true);
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


        private void btn_RFID_Click(object sender, EventArgs e)
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
                    RFID.DataReady -= new IRFIDProvider.RFIDHandler(RFID_DataReady);
                    RFID.DataReady += new IRFIDProvider.RFIDHandler(RFID_DataReady);
                    RFID.Start();


                    if (RFID.isOpen())
                    {
                        btn_RFID.BackColor = Color.Green;
                        btn_RFID.Text = "Stop Read";
                    }
                    else 
                    {
                        RFID.DataReady -= new IRFIDProvider.RFIDHandler(RFID_DataReady);
                        btn_RFID.BackColor = Color.Red;
                        btn_RFID.Text = "Start Read";
                    
                    }
                }

            }
            catch (Exception ex)
            {
                btn_RFID.BackColor = Color.Red;
                ErrorLog.Log.WriteException(ex);
            }
        }

        private void RFIDStop()
        {
            try
            {
                if (RFID != null)
                {
                    RFID.DataReady -= new IRFIDProvider.RFIDHandler(RFID_DataReady);
                    RFID.Stop();
                    btn_RFID.BackColor = Color.Red;
                    btn_RFID.Text = "Start Read";
                }

            }
            catch (Exception ex)
            {
                btn_RFID.BackColor = Color.Red;
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
                FlexibleMessageBox.Show(this, ex.Message, "EXCEPTION");
            }
        }


        #endregion

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            LeftTableNeZnamo = 0;
            LeftTableZnamo = 0;

            if (dt_ZmetkyPrint == null)
                dt_ZmetkyPrint = new PrintReportLibrary.DataSets.DS_Soupis();
            else
                dt_ZmetkyPrint.Polozky.Clear();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                try
                {



                    //bool x1 = prodej1.CZMST_DI.Any(x => (x.SERLTNUM.Trim() == row.Cells[0].Value.ToString().Trim()));
                    //bool x2 = prodej1.CZMST_DI.Any(x => ((x.IsCZ_CarKodNull() ? string.Empty : x.CZ_CarKod.Trim()) == row.Cells[0].Value.ToString().Trim()));
                    //bool x3 = prodej1.CZMST_DI.Any(x => ((x.IsVNDITNUMNull() ? string.Empty : x.VNDITNUM.Trim()) == row.Cells[0].Value.ToString().Trim()));

                    if (prodej1.CZMST_DI.Any(x =>
                        (x.SERLTNUM.Trim() == row.Cells["codeDataGridViewTextBoxColumn"].Value.ToString().Trim())
                        ||
                        ((x.IsCZ_CarKodNull() ? string.Empty : x.CZ_CarKod.Trim()) == row.Cells["codeDataGridViewTextBoxColumn"].Value.ToString().Trim())
                        ||
                        ((x.IsVNDITNUMNull() ? string.Empty : x.VNDITNUM.Trim()) == row.Cells["codeDataGridViewTextBoxColumn"].Value.ToString().Trim())
                        ))
                    {
                        row.DefaultCellStyle.BackColor = Color.Green;

                        //row.Cells["Code"].Style.BackColor = Color.Green;
                        //row.Cells["Count"].Style.BackColor = Color.Green;
                        //row.Cells["SKL_ID"].Style.BackColor = Color.Green;
                        //row.Cells["LOCNCODE"].Style.BackColor = Color.Green;
                        LeftTableZnamo++;
                    }
                    else
                    {


                        PrintReportLibrary.DataSets.DS_Soupis.PolozkyRow RowPolozka = dt_ZmetkyPrint.Polozky.NewPolozkyRow();

                        string sklid = row.Cells["sKLIDDataGridViewTextBoxColumn"].Value.ToString();

                        if (!string.IsNullOrEmpty(sklid))
                        {
                            decimal pocet = decimal.Parse(row.Cells["pocetNaSkladeDataGridViewTextBoxColumn"].Value.ToString());
                            
                            if (pocet == 0)
                            {
                                row.DefaultCellStyle.BackColor = Color.Blue;
                            }
                            else 
                            {
                                row.DefaultCellStyle.BackColor = Color.Orange;
                            }
                            
                            //row.DefaultCellStyle.BackColor = Color.Orange;
                            //row.Cells["Code"].Style.BackColor = Color.Orange;
                            //row.Cells["Count"].Style.BackColor = Color.Orange;
                            //row.Cells["SKL_ID"].Style.BackColor = Color.Orange;
                            //row.Cells["LOCNCODE"].Style.BackColor = Color.Orange;
                        }
                        else 
                        {
                            row.DefaultCellStyle.BackColor = Color.Red;

                            

                            
                            //row.Cells["Code"].Style.BackColor = Color.Red;
                            //row.Cells["Count"].Style.BackColor = Color.Red;
                            //row.Cells["SKL_ID"].Style.BackColor = Color.Red;
                            //row.Cells["LOCNCODE"].Style.BackColor = Color.Red;
                        }

                        RowPolozka.SERLTNUM = row.Cells["codeDataGridViewTextBoxColumn"].Value.ToString().Trim();
                        RowPolozka.ITEMDESC = row.Cells["descriptionDataGridViewTextBoxColumn"].Value.ToString().Trim();

                        dt_ZmetkyPrint.Polozky.AddPolozkyRow(RowPolozka);                      


                        LeftTableNeZnamo++;
                    }
                }
                catch (Exception ex)
                {
                    ErrorLog.Log.WriteException(ex);
                    FlexibleMessageBox.Show(this, ex.Message, "EXCEPTION");
                }
            }

            UpdateStatusStripLeft();
            //UpdateStatusStripRight();
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
                this.prodejService.ProcessSoupisCompleted += new FASK.MST_WINDOWS.Main.ProdejService.ProcessSoupisCompletedEventHandler(ProcessSoupisEnd);
                this.prodejService.ProcessSoupisCompleted -= new FASK.MST_WINDOWS.Main.ProdejService.ProcessSoupisCompletedEventHandler(ProcessSoupisEnd);

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
                Guid guid = rowDI.guid;

                if (rowDI != null)
                    rowDI.Delete();

                prodej1.CZMST_DI.AcceptChanges();

                dita.Delete(guid);
                //dita.Update(prodej1);

                //UpdateStatusStripLeft();
                UpdateStatusStripRight();
            }
            catch (Exception ex)
            {
                ErrorLog.Log.WriteException(ex);
            }

        }

        private void btn_zrusit_Click(object sender, EventArgs e)
        {
            DialogResult dr = FlexibleMessageBox.Show(this, string.Format("Opravdu zrušit dávku č.{0}", this._cisloDavky), this._typPohybName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (dr == System.Windows.Forms.DialogResult.Yes)
            {
                if (RFID.isOpen())
                {
                    RFIDStop(); // CloseRFID();

                }

                File.Delete(this._cislodavkysqlfilename);
                this.Close();
            }
        }

        private void btn_Print_Click(object sender, EventArgs e)
        {
            try
            {
               
                if (this.prodej1.CZMST_DI.Count == 0)
                {
                    FlexibleMessageBox.Show(this, "Nenalezena žádna data k odeslaní.", this._typPohybName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return;
                }

                
                PrintReportLibrary.DataSets.DS_Soupis ds = new PrintReportLibrary.DataSets.DS_Soupis();


                foreach (FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.Prodej.CZMST_DIRow item in this.prodej1.CZMST_DI)
                {
                    PrintReportLibrary.DataSets.DS_Soupis.PolozkyRow row = ds.Polozky.NewPolozkyRow();

                    row.ITEMDESC = item.IsITEMDESCNull() ? string.Empty : item.ITEMDESC.Trim();
                    row.ITEMNMBR = string.IsNullOrEmpty(item.ITEMNMBR) ? string.Empty : item.ITEMNMBR.Trim();
                    row.QTYSHPPD = item.QTYSHPPD.ToString();
                    row.ITEMCODE = item.IsITEMCODENull() ? string.Empty : item.ITEMCODE.Trim();
                    row.VNDITNUM = item.IsVNDITNUMNull() ? string.Empty : item.VNDITNUM.Trim();
                    row.CZ_CarKod = item.IsCZ_CarKodNull() ? string.Empty : item.CZ_CarKod.Trim();
                    row.QTYPACK = item.QTYPACK.ToString();
                    row.QTYSHPPDMJ = item.QTYSHPPDMJ.ToString();
                    row.MJ = string.IsNullOrEmpty(item.MJ) ? string.Empty : item.MJ.Trim();
                    //row.QTY = item.QTY;
                    row.NMBRPAL = item.IsNMBRPALNull() ? string.Empty : item.NMBRPAL.Trim();
                    row.SERLTNUM = string.IsNullOrEmpty(item.SERLTNUM) ? string.Empty : item.SERLTNUM.Trim();

                    ds.Polozky.AddPolozkyRow(row);
                }




                PrintReportLibrary.CoolPrintPreviewDialog plr = new PrintReportLibrary.CoolPrintPreviewDialog();

                //PrintReportLibrary.PrintReport plr = new PrintReportLibrary.PrintReport();
                plr.CountEntries = this.prodej1.CZMST_DI[0].CountEntries.ToString();
                plr.DS_Soupis = ds;

                List<PrintReportLibrary.TypeData> tmplist = new List<PrintReportLibrary.TypeData>();
                tmplist.Add(PrintReportLibrary.TypeData.DS_Soupis_Polozky);

                plr.typedata = new List<PrintReportLibrary.TypeData>(tmplist);
                plr.PrinterName = Properties.Settings.Default.PrinterName;

                if (this.typPohyb == TypPohybu.Predani_dodavateli_sluzeb_prani)
                {
                    plr.Typereport = PrintReportLibrary.TypeReport.ZPradelny_OK;
                }
                else if (this.typPohyb == TypPohybu.Prevzeti_od_dodavatele)
                {
                    plr.Typereport = PrintReportLibrary.TypeReport.DoPradelny_OK;
                }

                plr.ShowPreview = false;
                plr.Print();


                if (this.dt_ZmetkyPrint.Polozky.Count == 0)
                {
                    //FlexibleMessageBox.Show(this, "Nenalezena žádna data Zmetku k odeslaní.", this._typPohybName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    ErrorLog.Log.Write("Nenalezena žádna data Zmetku k odeslaní.");
                    return;
                }

                PrintReportLibrary.CoolPrintPreviewDialog plr_zle = new PrintReportLibrary.CoolPrintPreviewDialog();

                //PrintReportLibrary.PrintReport plr = new PrintReportLibrary.PrintReport();
                plr_zle.CountEntries = this.prodej1.CZMST_DI[0].CountEntries.ToString();
                plr_zle.DS_Soupis = dt_ZmetkyPrint;


                plr_zle.typedata = new List<PrintReportLibrary.TypeData>(tmplist);
                plr_zle.PrinterName = Properties.Settings.Default.PrinterName;
                plr_zle.Typereport = PrintReportLibrary.TypeReport.DoPradelny_Zle;
                plr_zle.ShowPreview = false;
                plr_zle.Print();




                ////PrintLocalReport.PrintLocalReport plr = new PrintLocalReport.PrintLocalReport();
                //plr.CountEntries = this.prodej1.CZMST_DI[0].CountEntries.ToString();
                //plr.DS_Soupis = ds;
                //plr.PrintTemplateDirectory = MyPath.PrintTemplateDirectory;
                //plr.Typereport = PrintLocalReport.TypeReport.DoPradelny_OK;

                //plr.PreparePrint();


                //#region Zmetky report
                //PrintLocalReport.PrintLocalReport plrZmetky = new PrintLocalReport.PrintLocalReport();
                //plrZmetky.CountEntries = this.prodej1.CZMST_DI[0].CountEntries.ToString();
                //plrZmetky.DS_Soupis = dt_ZmetkyPrint;
                //plrZmetky.PrintTemplateDirectory = MyPath.PrintTemplateDirectory;
                //plrZmetky.Typereport = PrintLocalReport.TypeReport.DoPradelny_Zle;

                //plrZmetky.PreparePrint(); 
                //#endregion



                //ProdejService.ProdejData localProdejOut = new ProdejService.ProdejData();

                //foreach (var item in this.prodej1.CZMST_DI)
                //{
                //    localProdejOut.CZMST_DI.ImportRow(item);
                //}

                //OnlineTisk(localProdejOut);
            }
            catch (Exception ex)
            {
                FlexibleMessageBox.Show(this, ex.Message, this._typPohybName, MessageBoxButtons.OK);
                ErrorLog.Log.WriteException(ex);
            }
        }

        private void OnlineTisk(FASK.MST_WINDOWS.Main.ProdejService.ProdejData data)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                //ErrorLog.Log.WriteException("2.OnlineTisk");
                prodejService.ProcessSoupisAsync(data);// Online_GetMaterial(itemnmbr, skl_id, serltnum, _typdokladu != null ? _typdokladu.doc_id : string.Empty);               
                //ErrorLog.Log.WriteException("3.OnlineTiskEnd");
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                //ErrorLog.Log.WriteException("XXXXX.OnlineTiskEXC");
                Cursor.Current = Cursors.Default;
                //ErrorLog.Log.Write(ex.Message, "RFID.RFID_Data, OnlineTisk");
                FlexibleMessageBox.Show(this, ex.Message, this._typPohybName, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void ProcessSoupisEnd(object sender, FASK.MST_WINDOWS.Main.ProdejService.ProcessSoupisCompletedEventArgs e)
        {
            try
            {
                //ErrorLog.Log.WriteException("5.ProcessSoupisEnd START");
                if (e.Result.Status == FASK.MST_WINDOWS.Main.ProdejService.StatusResultEnum.OK)
                {
                    foreach (var row in prodej1.CZMST_DI)
                    {
                        if (row.PRINTED != true)
                        {
                            row.PRINTED = true;
                            dita.Update(row);
                        }
                    }

                    

                }
                else if (e.Result.Status == FASK.MST_WINDOWS.Main.ProdejService.StatusResultEnum.WARNING)
                {
                    ErrorLog.Log.WriteException("ProcessSoupisEnd warning:" + e.Result.Message);
                    FlexibleMessageBox.Show(this, e.Result.Message, this._typPohybName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (e.Result.Status == FASK.MST_WINDOWS.Main.ProdejService.StatusResultEnum.ERROR)
                {
                    ErrorLog.Log.WriteException("ProcessSoupisEnd error:" + e.Result.Message);
                    FlexibleMessageBox.Show(this, e.Result.Message, this._typPohybName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {

                    //...
                    ErrorLog.Log.WriteException("Nastala nespecifikovana chyba v ProcessSoupisEnd ");
                    FlexibleMessageBox.Show(this, "Nastala nespecifikovana chyba.", this._typPohybName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                FlexibleMessageBox.Show(this, ex.Message, this._typPohybName, MessageBoxButtons.OK);
                ErrorLog.Log.WriteException(ex);
            }

        }

        private void panelBTN2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string SN = "300ED89F3350007FC465637E"; // cepice

                NajdiPolozkuCarovyKod(SN);
           

            }
            catch (Exception ex)
            {
                FlexibleMessageBox.Show(this, ex.Message, this._typPohybName, MessageBoxButtons.OK);
                ErrorLog.Log.WriteException(ex);
            }
        }


    }
} 
