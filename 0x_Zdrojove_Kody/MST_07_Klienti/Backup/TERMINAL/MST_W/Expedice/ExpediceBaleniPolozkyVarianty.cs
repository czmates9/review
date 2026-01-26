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
using Fask.Parsing.Codes;

namespace Fask.MST_W.Expedice
{
    public partial class ExpediceBaleniPolozkyVarianty : Form
    {
        public enum Zobrazeni { VARIANTY, STAV_SKLADU}
        private Zobrazeni zobrazeni;

        private SejmiKodForm skf = null;

        // nactena data z online metody
        Fask.SQLiteDBs.DataSets.Sklady.CZMST093Row sklad = null;
        //private ExpediceService.ExpediceBaleni dsExpediceBaleni = new Fask.MST_W.ExpediceService.ExpediceBaleni();
        private _WebRefernces_Globals.ExpediceSeviceSession  wsExpedice = null;
        private string barcode = string.Empty;
        private string itemnmbr = string.Empty;
        private string serltnum = string.Empty;

        public ExpediceBaleniPolozkyVarianty(string barcode, string itemnmbr, string serltnum, ExpediceService.ExpediceBaleni polozky, Fask.SQLiteDBs.DataSets.Sklady.CZMST093Row sklad, Zobrazeni zobrazeni)
        {
            InitializeComponent();

            try
            {
                this.sklad = sklad;
                this.zobrazeni = zobrazeni;


                this.barcode = barcode;
                this.itemnmbr = itemnmbr;
                this.serltnum = serltnum;
                this.dsExpediceBaleni = polozky;

                if (zobrazeni == Zobrazeni.STAV_SKLADU)
                {
                    ok_but.Enabled = false;
                    ok_but.Visible = false;
                    zpet_but.Text = "Zpět";
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "ExpedicePolozkyVarianty");
            }
        }

        /// <summary>
        /// Vybrana hlavicka
        /// </summary>
        private Fask.MST_W.ExpediceService.ExpediceBaleniHlavicky.CZMST_Expedice_Baleni_HlavickaRow _Hlavicka = null;

        
        public Fask.MST_W.ExpediceService.ExpediceBaleni.Expedice_Baleni_PolozkaRow _Polozka
        {
            get
            {
                try
                {
                    return ((DataRowView)(dataGrid1.BindingContext[bsPolozky].Current)).Row as Fask.MST_W.ExpediceService.ExpediceBaleni.Expedice_Baleni_PolozkaRow;
                }
                catch
                {
                    return null;
                }
            }
        }

        private void PrijemVyberPrijmoveLokaceList_Load(object sender, EventArgs e)
        {
            try
            {
                // nacteni lokalizace ze souboru
                Fask.Localization.LocalizationExtensionForm.Localize(this);

                this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
                this.Size = Forms.FormLocation.ScreenResolution;

                wsExpedice = new _WebRefernces_Globals.ExpediceSeviceSession();
                wsExpedice.Url = MST_Global.ServerAddress + "Expedice.asmx";
                wsExpedice.Timeout = MST_Global.ServiceTimeOut;
                wsExpedice.UpdateWebServiceCredentials();

                //Cursor.Current = Cursors.WaitCursor;
                InitializeDataGridView();

                InitializeGrid();

                //bsPolozky.DataSource = dsExpediceBaleni.Expedice_Baleni_Polozka;
                //dataGrid1.DataSource = bsPolozky;

                panelButtons_Resize(null, null);
            }
            catch (Exception ex)
            {
                //Cursor.Current = Cursors.Default;
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
            finally
            {
                timerLoad.Enabled = true;
            }
        }

        private void InitializeDataGridView()
        {
            DataGridTableStyle ts = new DataGridTableStyle();
            ts.MappingName = dsExpediceBaleni.Expedice_Baleni_Polozka.TableName;

            DataGrid2TextBoxColumn dgtbc = new DataGrid2TextBoxColumn();
            dgtbc.HeaderText = "Položka č.";
            dgtbc.MappingName = dsExpediceBaleni.Expedice_Baleni_Polozka.ITEMNMBRColumn.ColumnName;
            dgtbc.NullText = "-";
            dgtbc.Width = 50;
            ts.GridColumnStyles.Add(dgtbc);

            dgtbc = new DataGrid2TextBoxColumn();
            dgtbc.HeaderText = "Název";
            dgtbc.MappingName = dsExpediceBaleni.Expedice_Baleni_Polozka.ITEMDESCColumn.ColumnName; ;
            dgtbc.NullText = "-";
            dgtbc.Width = 150;
            ts.GridColumnStyles.Add(dgtbc);

            dgtbc = new DataGrid2TextBoxColumn();
            dgtbc.HeaderText = "Množství";
            dgtbc.MappingName = dsExpediceBaleni.Expedice_Baleni_Polozka.QTYColumn.ColumnName; ;
            dgtbc.NullText = "-";
            dgtbc.Width = 50;
            ts.GridColumnStyles.Add(dgtbc);

            dgtbc = new DataGrid2TextBoxColumn();
            dgtbc.HeaderText = "SN";
            dgtbc.MappingName = dsExpediceBaleni.Expedice_Baleni_Polozka.SERLTNUMColumn.ColumnName; ;
            dgtbc.NullText = "-";
            dgtbc.Width = 50;
            ts.GridColumnStyles.Add(dgtbc);

            dgtbc = new DataGrid2TextBoxColumn();
            dgtbc.HeaderText = "Č.k.";
            dgtbc.MappingName = dsExpediceBaleni.Expedice_Baleni_Polozka.VNDITNUMColumn.ColumnName; ;
            dgtbc.NullText = "-";
            dgtbc.Width = 50;
            ts.GridColumnStyles.Add(dgtbc);

            dgtbc = new DataGrid2TextBoxColumn();
            dgtbc.HeaderText = "Č.k. vlastní";
            dgtbc.MappingName = dsExpediceBaleni.Expedice_Baleni_Polozka.CZ_CarKodColumn.ColumnName; ;
            dgtbc.NullText = "-";
            dgtbc.Width = 50;
            ts.GridColumnStyles.Add(dgtbc);

            dgtbc = new DataGrid2TextBoxColumn();
            dgtbc.HeaderText = "Lokace";
            dgtbc.MappingName = dsExpediceBaleni.Expedice_Baleni_Polozka.LOCNCODEColumn.ColumnName; ;
            dgtbc.NullText = "-";
            dgtbc.Width = 50;
            ts.GridColumnStyles.Add(dgtbc);

            dgtbc = new DataGrid2TextBoxColumn();
            dgtbc.HeaderText = "Sklad ID";
            dgtbc.MappingName = dsExpediceBaleni.Expedice_Baleni_Polozka.SKL_IDColumn.ColumnName; ;
            dgtbc.NullText = "-";
            dgtbc.Width = 50;
            ts.GridColumnStyles.Add(dgtbc);

            dataGrid1.TableStyles.Add(ts);
        }

        private void InitializeGrid()
        {
            this.dataGrid1.InitializeTableGridColumnStyles(Settings.UIFormatDesCisel);
            this.dataGrid1.Font = new Font(this.dataGrid1.Font.Name, Settings.UIGridFont, this.dataGrid1.Font.Style);
            this.dataGrid1.Load(Path.Combine(Main.ConfigDir, this.GetType().ToString()));
        }

        private void PrijemVyberPrijmoveLokaceList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PerformOK();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                PerformCancel();
            }
            else if (e.KeyCode == Keys.F2)
            {
                miAktualizovat_Click(null, null);
            }
            else
                return;

            e.Handled = true;
        }

        private void PerformCancel()
        {
            finalize(); 
            DialogResult = DialogResult.Cancel;
        }

        private void finalize()
        {
            ScannerFinalize();

            this.dataGrid1.Save(Path.Combine(Main.ConfigDir, this.GetType().ToString()));
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
        /// <param name="kod">barcode</param>
        private void najdipolozku(string kod)
        {
            try
            {
                //ScannerStop();

                //string ck = kod.Trim();

                //// parsovani vahoveho kodu
                //Fask.Parsing.Codes.BaseCode code = null;
                //if (Expedice.Globals.PovolitParsovaniCK)
                //{
                //    code = Parsing.ParsingFactory.ParseWeightCode(ck);
                //    if (code is WeightCode)
                //        ck = ((WeightCode)code).id;
                //}

                // wsExpedice.Polozka_Get(MST_Global.TerminalID, MST_Global.UserID, Expedice.Globals.SkladID, ck);
                 
                
                //ScannerStart();

                //// TODO: prepsat, vyhledat online ...
                //bsPolozky.Filter = string.Empty;
                //string selectcmd = "CZ_CarKod = '" + kod + "' OR VNDITNUM = '" + kod + "'";
                //var tables = polozky.CZMST_Expedice_Polozky.Select(selectcmd);
                
                //if (tables.Count() == 0)
                //{
                //    MessageBoxBig.Show(string.Format("Položka s čárovým kódem '{0}' nebyla nalezena", kod), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                //    return;
                //}
                //else if (tables.Count() > 1)
                //{
                //    bsPolozky.Filter = "CZ_CarKod = '" + kod + "' OR VNDITNUM = '" + kod + "'"; ;
                //    MessageBoxBig.Show(string.Format("Nalezeno více položek s čárovým kódem '{0}'", kod), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                //    return;
                //}
                //else
                //{
                //    bsPolozky.Filter = "CZ_CarKod = '" + kod + "' OR VNDITNUM = '" + kod + "'"; ;
                //    PerformOK();
                //}
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
            finally
            {
                if (MST_Global.OnScannerSound_Expedice)
                {
                    MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, "scannerready.wav"));
                }
            }
        }

        #endregion scanner

        private void miKonec_Click(object sender, EventArgs e)
        {
            if (MessageBoxBig.Show("Opravdu chcete ukončit zadávání?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) 
                == DialogResult.No)
                return;

            PerformCancel();
        }

        private void PerformOK()
        {
            try
            {
                if (_Polozka == null)
                {
                    MessageBoxBig.Show("Není vybrána položka", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
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
        }

        private void miDynamickaTabulka_Click(object sender, EventArgs e)
        {
        }

        private void dataGrid1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void zpet_but_Click_1(object sender, EventArgs e)
        {

        }

        private void ok_but_Click_1(object sender, EventArgs e)
        {

        }

        private void ok_but_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        private void zpet_but_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void panelButtons_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
            ok_but.Size = nsize;
        }

        private Fask.MST_W.ExpediceService.ExpediceBaleni OnlineGetPolozky(string skl_id, string barcode, string itemnmbr, string serltnum)
        {
            try
            {
                return wsExpedice.Baleni_Polozka_Get(MST_Global.TerminalID, MST_Global.UserID, skl_id, barcode, itemnmbr, serltnum);
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message, "Expedice.ExpediceVyberHlavickyList, OnlineGetHlavicky");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                return null;
            }
        }

        private void miAktualizovat_Click(object sender, EventArgs e)
        {
            try
            {
                PerformUpdate();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message, "Expedice.ExpediceVyberHlavickyList, Aktualizovat");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
        }

        private void PerformUpdate()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                
                dsExpediceBaleni = OnlineGetPolozky(sklad != null ? sklad.skl_id : string.Empty, barcode, itemnmbr, serltnum);
                //if (hlavicky != null)
                //{
                //    bsHlavicky = new BindingSource();
                //    bsHlavicky.DataSource = hlavicky.CZMST_Expedice_Hlavicka;
                //    dataGrid1.DataSource = bsHlavicky;
                //}
                if (dsExpediceBaleni == null)
                    dsExpediceBaleni = new Fask.MST_W.ExpediceService.ExpediceBaleni();

                bsPolozky = new BindingSource();
                bsPolozky.DataSource = dsExpediceBaleni.Expedice_Baleni_Polozka;
                dataGrid1.DataSource = bsPolozky;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Logging.Log.Write(ex);
                MessageBoxBig.Show("Načtení položek se nezdařilo.\n" + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
            
            try
            {
                dataGrid1.Focus();
                dataGrid1.CurrentRowIndex = dataGrid1.CurrentRowIndex;
            }
            catch
            {
            }
        }

        private void ExpediceVyberHlavickyList_Shown(object sender, EventArgs e)
        {
            timerLoad.Enabled = false;
            Cursor.Current = Cursors.WaitCursor; Application.DoEvents();

            try
            {
                bsPolozky.DataSource = this.dsExpediceBaleni;
                dataGrid1.DataSource = bsPolozky;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message, "Expedice.ExpediceVyberHlavickyList, ExpediceVyberHlavickyList_Shown");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }

            ScannerStart();

            Cursor.Current = Cursors.Default;
        }

        private void miOdstranitPrikaz_Click(object sender, EventArgs e)
        {
        }
    }
}