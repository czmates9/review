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

namespace Fask.MST_W.Expedice
{
    public partial class ExpediceBaleniVyberHlavickyList : Form
    {
        private SejmiKodForm skf = null;

        // nactena data z online metody
        Fask.SQLiteDBs.DataSets.Sklady.CZMST093Row sklad = null;
        //private ExpediceService.ExpediceBaleniHlavicky dsExpediceBaleniHlavicky = new Fask.MST_W.ExpediceService.ExpediceBaleniHlavicky();
        private _WebRefernces_Globals.ExpediceSeviceSession  wsExpedice = null;
        
        public ExpediceBaleniVyberHlavickyList(Fask.SQLiteDBs.DataSets.Sklady.CZMST093Row sklad)
        {
            InitializeComponent();
            try
            {
                this.sklad = sklad;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message, "Expedice.ExpediceVyberHlavickyList, ExpediceVyberHlavickyList");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
        }

        /// <summary>
        /// Vybrana hlavicka
        /// </summary>
        public Fask.MST_W.ExpediceService.ExpediceBaleniHlavicky.CZMST_Expedice_Baleni_HlavickaRow _Hlavicka
        {
            get
            {
                try
                {
                    return ((DataRowView)(dataGrid1.BindingContext[bsHlavicky].Current)).Row as Fask.MST_W.ExpediceService.ExpediceBaleniHlavicky.CZMST_Expedice_Baleni_HlavickaRow;
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

                bsHlavicky.DataSource = dsExpediceBaleniHlavicky.CZMST_Expedice_Baleni_Hlavicka;
                dataGrid1.DataSource = bsHlavicky;

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
            ts.MappingName = dsExpediceBaleniHlavicky.CZMST_Expedice_Baleni_Hlavicka.TableName;

            DataGrid2TextBoxColumn dgtbc = new DataGrid2TextBoxColumn();
            dgtbc.HeaderText = "ID";
            dgtbc.MappingName = dsExpediceBaleniHlavicky.CZMST_Expedice_Baleni_Hlavicka.IDColumn.ColumnName;
            dgtbc.NullText = "-";
            dgtbc.Width = 50;
            ts.GridColumnStyles.Add(dgtbc);

            //dgtbc = new DataGrid2TextBoxColumn();
            //dgtbc.HeaderText = "Přepravce ID";
            //dgtbc.MappingName = hlavicky.CZMST_Expedice_Baleni_Hlavicka.PrepravceIDColumn.ColumnName; ;
            //dgtbc.NullText = "-";
            //dgtbc.Width = 50;
            //ts.GridColumnStyles.Add(dgtbc);

            //dgtbc = new DataGrid2TextBoxColumn();
            //dgtbc.HeaderText = "Přepravce SPZ";
            //dgtbc.MappingName = hlavicky.CZMST_Expedice_Hlavicka.PrepravceSPZColumn.ColumnName; ;
            //dgtbc.NullText = "-";
            //dgtbc.Width = 50;
            //ts.GridColumnStyles.Add(dgtbc);

            dgtbc = new DataGrid2TextBoxColumn();
            dgtbc.HeaderText = "Č.k.";
            dgtbc.MappingName = dsExpediceBaleniHlavicky.CZMST_Expedice_Baleni_Hlavicka.BarcodeColumn.ColumnName; ;
            dgtbc.NullText = "-";
            dgtbc.Width = 50;
            ts.GridColumnStyles.Add(dgtbc);

            dgtbc = new DataGrid2TextBoxColumn();
            dgtbc.HeaderText = "Načteno";
            dgtbc.MappingName = dsExpediceBaleniHlavicky.CZMST_Expedice_Baleni_Hlavicka.SumItemsColumn.ColumnName; ;
            dgtbc.NullText = "-";
            dgtbc.Width = 50;
            ts.GridColumnStyles.Add(dgtbc);

            dataGrid1.TableStyles.Add(ts);
        }

        //private void InitializeGridDynamicColumns(ExpediceService.ExpediceHlavicky hlavicky)
        //{
        //    foreach (DataColumn dcol in hlavicky.CZMST_Expedice_Hlavicka.Columns)
        //    {
        //        if (!this.dataGridTableStyle1.GridColumnStyles.Contains(dcol.ColumnName))
        //        {
        //            DataGrid2TextBoxColumn du = new DataGrid2TextBoxColumn();
        //            du.MappingName = dcol.ColumnName;
        //            du.HeaderText = dcol.ColumnName;
        //            du.NullText = "-";
        //            du.Width = 45;
        //            du.Grid = this.dataGrid1;
        //            this.dataGridTableStyle1.GridColumnStyles.Add(du);
        //        }
        //    }
        //}

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
            else if (e.KeyCode == Keys.Back)
            {
                PerformDeleteHlavicka();
            }
            else if (e.KeyCode == Keys.F1)
            {
                PerformCreateHlavicka();
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
                bsHlavicky.Filter = string.Empty;
                string selectcmd = "Barcode = '" + kod + "'";
                var tables = dsExpediceBaleniHlavicky.CZMST_Expedice_Baleni_Hlavicka.Select(selectcmd);
                
                if (tables.Count() == 0)
                {
                    MessageBoxBig.Show(string.Format("Expediční příkaz s '{0}' nebyl nalezen", kod), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return;
                }
                else if (tables.Count() > 1)
                {
                    bsHlavicky.Filter = "Barcode='" + kod + "'";
                    MessageBoxBig.Show(string.Format("Nalezeno více expedičních příkazů '{0}'", kod), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return;
                }
                else
                {
                    bsHlavicky.Filter = "Barcode='" + kod + "'";
                    PerformOK();
                }
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
            if (MessageBoxBig.Show("Opravdu chcete ukončit modul?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) 
                == DialogResult.No)
                return;

            PerformCancel();
        }

        private void PerformOK()
        {
            try
            {
                if (_Hlavicka == null)
                {
                    MessageBoxBig.Show("Není vybrán expediční příkaz", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
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
            if (MessageBoxBig.Show("Opravdu chcete ukončit modul?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) == DialogResult.No)
                return;

            PerformCancel();
        }

        private void panelButtons_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
            ok_but.Size = nsize;
        }

        private Fask.MST_W.ExpediceService.ExpediceBaleniHlavicky OnlineGetHlavicky(string skl_id)
        {
            try
            {
                return wsExpedice.Baleni_GetHlavicky(MST_Global.TerminalID, MST_Global.UserID, skl_id);
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
                
                dsExpediceBaleniHlavicky = OnlineGetHlavicky(sklad != null ? sklad.skl_id : string.Empty);
                //if (hlavicky != null)
                //{
                //    bsHlavicky = new BindingSource();
                //    bsHlavicky.DataSource = hlavicky.CZMST_Expedice_Hlavicka;
                //    dataGrid1.DataSource = bsHlavicky;
                //}
                if (dsExpediceBaleniHlavicky == null)
                    dsExpediceBaleniHlavicky = new Fask.MST_W.ExpediceService.ExpediceBaleniHlavicky();

                bsHlavicky = new BindingSource();
                bsHlavicky.DataSource = dsExpediceBaleniHlavicky.CZMST_Expedice_Baleni_Hlavicka;
                dataGrid1.DataSource = bsHlavicky;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Logging.Log.Write(ex);
                MessageBoxBig.Show("Načtení expedičních příkazů se nezdařilo.\n" + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
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

        private void miNajitLokaci_Click(object sender, EventArgs e)
        {
            try
            {
                ScannerStop();

                string kod = string.Empty;
                using (SejmiKodForm skf = new SejmiKodForm("Čárový kód", SejmiKodForm.TypeOfCode.AlphaNumeric, 0, false, false, string.Empty))
                {
                    skf.Text = "Zadejte čár. kód příkazu";
                    DialogResult dr = skf.ShowDialog();

                    if (dr != DialogResult.OK)
                    {
                        ScannerStart();
                        return;
                    }

                    kod = skf.Kod;
                }

                najdipolozku(kod);
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message, "Expedice.ExpediceVyberHlavickyList, miNajitLokaci_Click");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
            finally
            {
                ScannerStart();
            }
        }

        private void miNovyPrikaz_Click(object sender, EventArgs e)
        {
            PerformCreateHlavicka();
        }

        private void PerformCreateHlavicka()
        {
            string prepravceID = string.Empty;
            string prepravceSPZ = string.Empty;

            ExpediceService.ExpediceBaleniHlavicky hlavickyData = new Fask.MST_W.ExpediceService.ExpediceBaleniHlavicky();
            ExpediceService.ExpediceBaleniHlavicky.CZMST_Expedice_Baleni_HlavickaRow rowHlavicka = null;

            try
            {
                ScannerStop();
               
                //if (skf == null) 
                //    skf = new SejmiKodForm();
                //skf.CodeType = SejmiKodForm.TypeOfCode.AlphaNumeric;
                //skf.AllowEmpty = true;  // TODO: zmenit ...
                //skf.CheckLen = false;
                //skf.Kod = string.Empty;
                //skf.Len = 0;
                ////skf.MaxLength = REZ1_LEN;
                
                //// zadani prepravce
                //skf.Popis = "Přepravce";
                //skf.Text = "Zadání přepravce";
                //skf.Kod = string.Empty;
                //if (skf.ShowDialog() == DialogResult.Cancel)
                //    return;
                //prepravceID = skf.Kod;

                //// zadani SPZ
                //skf.Popis = "SPZ";
                //skf.Text = "Zadání SPZ";
                //skf.Kod = string.Empty;
                //if (skf.ShowDialog() == DialogResult.Cancel)
                //    return;
                //prepravceSPZ = skf.Kod;



                rowHlavicka = hlavickyData.CZMST_Expedice_Baleni_Hlavicka.NewCZMST_Expedice_Baleni_HlavickaRow();

                rowHlavicka.ID = Guid.NewGuid();
                //rowHlavicka.PrepravceID = prepravceID;
                //rowHlavicka.PrepravceSPZ = prepravceSPZ;
                rowHlavicka.SKL_ID = sklad == null ? string.Empty : sklad.skl_id;
                rowHlavicka.UserID = MST_Global.UserID;
                rowHlavicka.TermID = MST_Global.TerminalID;

                rowHlavicka.Barcode = string.Empty;
                rowHlavicka.DateCreated = DateTime.Now;

                hlavickyData.CZMST_Expedice_Baleni_Hlavicka.AddCZMST_Expedice_Baleni_HlavickaRow(rowHlavicka);

                bool state = true;
                while (state)
                {
                    try
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        int res = wsExpedice.Baleni_Hlavicka_Add(MST_Global.TerminalID, MST_Global.UserID, sklad == null ? string.Empty : sklad.skl_id, hlavickyData);

                        dsExpediceBaleniHlavicky.CZMST_Expedice_Baleni_Hlavicka.ImportRow(rowHlavicka);
                        dsExpediceBaleniHlavicky.AcceptChanges();
                        Cursor.Current = Cursors.Default;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Cursor.Current = Cursors.Default;
                        Logging.Log.Write(ex.Message, "Expedice.ExpediceVyberHlavickyList, PerformNovyPrikaz");
                        if (MessageBoxBig.Show(string.Format("Expediční příkaz se nepodařilo vytvořit\n{0}\nOpakovat?", ex.Message), this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Critical)
                            != DialogResult.Yes)
                            return;

                    }
                }
                int pos = bsHlavicky.Find(dsExpediceBaleniHlavicky.CZMST_Expedice_Baleni_Hlavicka.IDColumn.ColumnName, rowHlavicka.ID);

                dataGrid1.CurrentRowIndex = pos;

                // TODO: konfiguracne otevrit davku??
                if (_Hlavicka != null)
                {
                    finalize();
                    DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Logging.Log.Write(ex.Message, "Expedice.ExpediceVyberHlavickyList, PerformNovyPrikaz");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                ScannerStart();
            }
        }
        private void ExpediceVyberHlavickyList_Shown(object sender, EventArgs e)
        {
            timerLoad.Enabled = false;
            Cursor.Current = Cursors.WaitCursor; Application.DoEvents();

            try
            {
                // online nacteni dat
                PerformUpdate();
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
            PerformDeleteHlavicka();
        }

        /// <summary>
        /// metoda pro odstraneni hlavicky
        /// </summary>
        private void PerformDeleteHlavicka()
        {
            if (_Hlavicka == null)
                return;

            try
            {
                ScannerStop();

                //if (MessageBoxBig.Show("Opravdu chcete odstranit vybraný příkaz?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
                //== DialogResult.No)
                //    return;

                if (!_Hlavicka.IsSumItemsNull() && _Hlavicka.SumItems > 0)
                {
                    if (MessageBoxBig.Show("Expediční příkaz obsahuje načtené položky.\nOpravdu chcete odstranit vybraný příkaz?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
                    == DialogResult.No)
                        return;
                }
                else
                {
                    if (MessageBoxBig.Show("Opravdu chcete odstranit vybraný příkaz?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
                    == DialogResult.No)
                        return;
                }

                int res = wsExpedice.Baleni_Hlavicka_Del(MST_Global.TerminalID, MST_Global.UserID, sklad == null ? string.Empty : sklad.skl_id, _Hlavicka.ID);

                dsExpediceBaleniHlavicky.CZMST_Expedice_Baleni_Hlavicka.RemoveCZMST_Expedice_Baleni_HlavickaRow(_Hlavicka);
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message, "Expedice.ExpediceVyberHlavickyList, PerformNovyPrikaz");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
            finally
            {
                ScannerStart();
            }
        }
    }
}