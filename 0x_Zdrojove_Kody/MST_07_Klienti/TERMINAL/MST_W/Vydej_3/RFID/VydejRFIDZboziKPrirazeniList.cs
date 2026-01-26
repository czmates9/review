using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;
using Fask.MST_W.Forms;
using Fask.MST_W.ServerAccess;
using Fask.Graphic;

namespace Fask.MST_W.Vydej_3.RFID
{
    public partial class VydejRFIDZboziKPrirazeniList : Form
    {
        // nactena data z online metody
        //private Fask.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SETableAdapter _ta_se = new Fask.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SETableAdapter();
        //private Fask.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SITableAdapter _ta_si = new Fask.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SITableAdapter();
        //private Fask.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SI_RFIDTableAdapter _ta_si_rfid = new Fask.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SI_RFIDTableAdapter();
        private DsRFIDTableAdapters.PredlohaTableAdapter _ta_rfid_predloha = new Fask.MST_W.Vydej_3.RFID.DsRFIDTableAdapters.PredlohaTableAdapter();
        private DsRFIDTableAdapters.RfidTableAdapter _ta_rfid_rfid = new Fask.MST_W.Vydej_3.RFID.DsRFIDTableAdapters.RfidTableAdapter();

        private System.Data.SQLite.SQLiteConnection _davkasqlceconnection = null;
        private string _davka = string.Empty;
        //private bool filtrZobrazitVse = true;

        public VydejRFIDZboziKPrirazeniList(string davka)
        {
            InitializeComponent(); 
            try
            {
                this._davka = davka;
                _davkasqlceconnection = new System.Data.SQLite.SQLiteConnection( SQLiteDBs.Controllers.SQLite_Static.SQLiteConnectionStringFormat(_davka + "." + Main.Ext_Vydej));
                //_ta_se.Connection = _davkasqlceconnection;
                //_ta_si.Connection = _davkasqlceconnection;
                //_ta_si_rfid.Connection = _davkasqlceconnection;
                _ta_rfid_predloha.Connection = _davkasqlceconnection;
                _ta_rfid_rfid.Connection = _davkasqlceconnection;

                RefreshData();

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "VydejRFIDZboziKPrirazeniList.const.");
            }
        }

        private void RefreshData()
        {
            this._ean = string.Empty; // vynulovani hledaneho ean ...
            try
            {
                _ta_rfid_predloha.ClearBeforeFill = true;
                _ta_rfid_predloha.Fill(dsRFID.Predloha);
                _ta_rfid_rfid.ClearBeforeFill = true;
                _ta_rfid_rfid.Fill(dsRFID.Rfid);
                
                foreach (var pVseRow in dsRFID.Predloha)
                {
                    var rfidItems = dsRFID.Rfid.Where( 
                        x => 
                        (x.CountEntries == pVseRow.CountEntries)
                        && String.Equals(x.SOPNUMBE.Trim(), pVseRow.SOPNUMBE.Trim())
                        && String.Equals(x.ITEMNMBR.Trim(), pVseRow.ITEMNMBR.Trim())
                        && (x.ORD == pVseRow.ORD)
                        );

                    if (rfidItems.Count() == 0)
                        continue;

                    pVseRow.mnozstviRFID = rfidItems.First().mnozstvi;
                }

                dataGrid1.Focus();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "VydejRFIDZboziKPrirazeniList.RefreshData");
            }
        }

        private string _ean = string.Empty;
        public string EAN
        {
            get
            {
                return this._ean;
            }
        }

        public DsRFID.PredlohaRow SelectedRow
        {
            get
            {
                try
                {
                    return (bsRFID.Current as DataRowView).Row as DsRFID.PredlohaRow;
                }
                catch
                {
                    return null;
                }
            }
        }

        private void VydejRFIDZboziKPrirazeniList_Load(object sender, EventArgs e)
        {
            try
            {
                // nacteni lokalizace ze souboru
                Fask.Localization.LocalizationExtensionForm.Localize(this);

                this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
                this.Size = Forms.FormLocation.ScreenResolution;

                MyGridInitialize();

                ScannerStart();

                dataGrid1.Focus();
                try
                {
                    dataGrid1.CurrentRowIndex = 0;
                }
                catch
                {
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
        }

        private void MyGridInitialize()
        {
            this.dataGrid1.InitializeTableGridColumnStyles(Settings.UIFormatDesCisel);
            this.dataGrid1.Font = new Font(this.dataGrid1.Font.Name, Settings.UIGridFont, this.dataGrid1.Font.Style);
            this.dataGrid1.Load(Path.Combine(Main.ConfigDir, this.GetType().ToString()));
        }

        private void MyGridSave()
        {
            this.dataGrid1.Save(Path.Combine(Main.ConfigDir, this.GetType().ToString()));
        }


        private void ServisDynamickaTabulka_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PerformOK();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                PerformCancel();
            }
            else
                return;

            e.Handled = true;
        }

        private void PerformCancel()
        {
            DialogResult dr =  MessageBoxBig.Show("Opravdu chcete ukončit přiřazování tagů položkám?", "Dotaz", MessageBoxButtons.YesNo, MessageBoxBigIcon.Question);
            if (dr == DialogResult.No)
                return;

            finalize(); 
            DialogResult = DialogResult.Cancel;
        }

        private void finalize()
        {
            ScannerFinalize();
            //Settings.PrijemZalokovaniListFiltrVse = filtrZobrazitVse;
            MyGridSave();
        }

        #region scanner
        private bool scannserstart = true;
        private void ScannerFinalize()
        {
            this.ScannerStop();
            this.scannserstart = false;
        }

        private void ScannerStart()
        {
            if (!scannserstart)
                return;

            Program.mstw.ScannerEventAdd(new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady));
            Program.mstw.EnableScanner();
        }

        private void ScannerStop()
        {
            Program.mstw.ScannerEventRemove(new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady));
            Program.mstw.DisableScanner();
        }

        delegate void DelegateString(string kod);
        void Scanner_DataReady(object sender, Fask.ScannerProvider.ScannerEventArgs e)
        {
            try
            {
                string kod = e.BarcodeData.Trim();
                if (kod.Length <= 0)
                    return;

                this.BeginInvoke(new DelegateString(najdipolozku), new object[] { kod });

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
        }

        /// <summary>
        /// Najití položky podle čárového kódu
        /// </summary>
        /// <param name="kod"></param>
        private void najdipolozku(string kod)
        {
            try
            {
                //SqlCEDBs.DataSets.Vydej.CZMST_SEDataTable dt_se = _ta_se.GetDataByEan(kod);
                Fask.SQLiteDBs.DataSets.Vydej.CZMST_SEDataTable dt_se = Vydej.vydejInstance.globalObject.controller_vydej.GetDataByEan_SE(kod);
                // pokud je prazdny, tak info ...
                // pokud neni, tak se pokusit najit odpovidajici zaznam ... a pouzit ho ...
                // a) pokud je jeden (sopnumber, itemnmbr, ord) => najit a pouzit ...
                // b) pokud je vice, tak co ? filtr => zkusim 
                // ? pokud to udelam normalne filtrem, tak to bude obecne i pro jeden ???

                if (dt_se.Count == 0) // nenalezeno ... 
                {
                    MessageBoxBig.Show("Položka s č.k. '" + kod.Trim() + "' nenalezena v předloze", this.Text);
                    return;
                }

                this._ean = kod;

                List<DsRFID.PredlohaRow> rowsstays = new List<DsRFID.PredlohaRow>();

                foreach (var ise in dt_se)
                {
                    var foundsStays = dsRFID.Predloha.Where(x =>
                        x.SOPNUMBE.Trim().Equals(ise.SOPNUMBE.Trim())
                        && x.ITEMNMBR.Trim().Equals(ise.ITEMNMBR.Trim())
                        && x.ORD == ise.ORD
                        );
                    rowsstays.AddRange(foundsStays);
                }

                for (int i = dsRFID.Predloha.Count - 1; i >= 0; i--)
                {
                    Fask.MST_W.Vydej_3.RFID.DsRFID.PredlohaRow pRow = dsRFID.Predloha[i];
                    if (!rowsstays.Contains(pRow))
                        dsRFID.Predloha.RemovePredlohaRow(pRow);
                }                

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
            finally
            {
                if (MST_Global.OnScannerSound_Vydej_3)
                {
                    MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, "scannerready.wav"));
                }
            }
        }

        #endregion scanner

        private void miKonec_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void PerformOK()
        {
            try
            {
                if (this.SelectedRow == null)
                {
                    MessageBoxBig.Show("Není vybrána položka");
                    return;
                }

                if (this.SelectedRow.zbyva <= 0)
                {
                    MessageBoxBig.Show("Vše již bylo přiřazeno");
                    return;
                }

                finalize();
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Fask.Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
            finally
            {
                ScannerStart();
            }
        }

        private void menuItemObnovitPrehled_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

    }
}