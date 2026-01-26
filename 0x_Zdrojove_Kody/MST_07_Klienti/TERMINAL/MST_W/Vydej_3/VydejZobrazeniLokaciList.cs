using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Fask.MST_W.Forms;

namespace Fask.MST_W.Vydej_3
{
    public partial class VydejZobrazeniLokaciList : Form
    {
        // nactena data z online metody
        //string skl_id = null;
        private Fask.SQLiteDBs.DataSets.Vydej.CZMST_SERow seRow = null;
        private Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIRow siRow = null;
        private Fask.MST_W.LokaceService.Location ds = new Fask.MST_W.LokaceService.Location();
        private string itemnmbr = string.Empty;
        private string serltnum = string.Empty;
        private string skl_id = string.Empty;

        private BindingSource bs;

        public VydejZobrazeniLokaciList(Fask.SQLiteDBs.DataSets.Vydej.CZMST_SERow seRow, Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIRow siRow)
        {
            InitializeComponent(); 
            try
            {
                //this.skl_id = sklad;
                this.seRow = seRow;
                this.siRow = siRow;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "PrijemVyberPrijmoveLokaceList load");
            }
        }

        public VydejZobrazeniLokaciList(string itemnmbr, string serltnum, string skl_id)
        {
            InitializeComponent();

            try
            {
                this.itemnmbr = itemnmbr;
                this.serltnum = serltnum;
                this.skl_id = skl_id;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "PrijemVyberPrijmoveLokaceList load");
            }
        }

        /// <summary>
        /// Uzivatelem vybrana lokace.
        /// </summary>
        public Fask.MST_W.LokaceService.Location.CZMST_SkladLokace_MapaRow PrijmovaLokace { get; set; }

        // vybrany radek v seznamu doporucenych lokaci
        private Fask.MST_W.LokaceService.Location.CZMST_SkladLokace_StavRow _vybranaLokace
        {
            get
            {
                try
                {
                    return ((DataRowView)(dataGrid1.BindingContext[bs].Current)).Row as Fask.MST_W.LokaceService.Location.CZMST_SkladLokace_StavRow;
                }
                catch (Exception ex)
                {
                    Logging.Log.Write(ex);
                    return null;
                }
            }
        }

        private void VydejZobrazeniLokaciList_Load(object sender, EventArgs e)
        {
            try
            {
                // nacteni lokalizace ze souboru
                Fask.Localization.LocalizationExtensionForm.Localize(this);

                this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
                this.Size = Forms.FormLocation.ScreenResolution;

                CreateGridStyles();

                InitializeGrid();

                ScannerStart();
                panelButtons_Resize(null, null);

                // online nacteni dat
                PerformUpdate();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
        }

        private void CreateGridStyles()
        {
            DataGridTableStyle ts = new DataGridTableStyle();
            ts.MappingName = ds.CZMST_SkladLokace_Stav.TableName;

            Fask.Graphic.DataGrid2TextBoxColumn dg = new Fask.Graphic.DataGrid2TextBoxColumn();
            dg.HeaderText = "Položka Č.";
            dg.MappingName = ds.CZMST_SkladLokace_Stav.ITEMNMBRColumn.ColumnName;
            dg.NullText = "-";
            dg.Width = 50;
            ts.GridColumnStyles.Add(dg);

            dg = new Fask.Graphic.DataGrid2TextBoxColumn();
            dg.HeaderText = "Lokace";
            dg.MappingName = ds.CZMST_SkladLokace_Stav.LOCNCODEColumn.ColumnName;
            dg.NullText = "-";
            //dg.SelectionShow = true;
            dg.Width = 50;
            ts.GridColumnStyles.Add(dg);

            dg = new Fask.Graphic.DataGrid2TextBoxColumn();
            dg.HeaderText = "Množství";
            dg.MappingName = ds.CZMST_SkladLokace_Stav.QTYSHPPDColumn.ColumnName;
            dg.NullText = "-";
            //dg.SelectionShow = true;
            dg.Width = 50;
            ts.GridColumnStyles.Add(dg);

			dg = new Fask.Graphic.DataGrid2TextBoxColumn();
			dg.HeaderText = "Šarže";
			dg.MappingName = ds.CZMST_SkladLokace_Stav.SERLTNUMColumn.ColumnName;
			dg.NullText = "-";
			//dg.SelectionShow = true;
			dg.Width = 50;
			ts.GridColumnStyles.Add(dg);

            dg = new Fask.Graphic.DataGrid2TextBoxColumn();
            dg.HeaderText = "Sklad ID";
            dg.MappingName = ds.CZMST_SkladLokace_Stav.SKL_IDColumn.ColumnName;
            dg.NullText = "-";
            //dg.SelectionShow = true;
            dg.Width = 50;
            ts.GridColumnStyles.Add(dg);

            dataGrid1.TableStyles.Add(ts);
        }

        private void InitializeGrid()
        {
            this.dataGrid1.InitializeTableGridColumnStyles(Settings.UIFormatDesCisel);
            this.dataGrid1.Font = new Font(this.dataGrid1.Font.Name, Settings.UIGridFont, this.dataGrid1.Font.Style);
            this.dataGrid1.Load(Path.Combine(Main.ConfigDir, this.GetType().ToString()));
        }

        private void VydejZobrazeniLokaciList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //PerformOK();
                PerformCancel();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                PerformCancel();
            }
            else if (e.KeyCode == Keys.F1)
            {
                miNajitLokaci_Click(null, null);
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
                bs.Filter = string.Empty;
                string selectcmd = "LOCNCODE = '" + kod + "'";
                var tables = ds.CZMST_SkladLokace_Stav.Select(selectcmd);
                
                if (tables.Count() == 0)
                {
                    MessageBoxBig.Show("Lokace '" + kod + "' nebyla nalezena v seznamu.", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return;
                }
                else if (tables.Count() > 1)
                {
                    bs.Filter = "LOCNCODE='" + kod + "'";
                    MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prijem4PrijemVyberLokaceListNalezenoViceLokaci, kod), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return;
                }
                else
                {
                    bs.Filter = "LOCNCODE='" + kod + "'";
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

        private void zpet_but_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void miPrerusit_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void panelButtons_Resize(object sender, EventArgs e)
        {
            //Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
            //ok_but.Size = nsize;
        }

        private Fask.MST_W.LokaceService.Location OnlineGetLokace(Fask.SQLiteDBs.DataSets.Vydej.CZMST_SERow seRow, Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIRow siRow)
        {
            Fask.MST_W.LokaceService.Location lokace;
            try
            {

                //lokace = Vydej.vydejInstance.globalObject.service_lokace.ShowMaterial(seRow.ITEMNMBR, siRow == null ? string.Empty : siRow.SERLTNUM, siRow.IsSKL_IDNull() ? string.Empty : siRow.SKL_ID, null, false);  // TODO: konfiguracne pocet zaznamu ...

				if (seRow == null)
				{
					lokace = Vydej.vydejInstance.globalObject.service_lokace.ShowMaterial(this.itemnmbr, string.IsNullOrEmpty(serltnum) ? string.Empty : serltnum, string.IsNullOrEmpty(skl_id) ? string.Empty : skl_id, null, false);  // TODO: konfiguracne pocet zaznamu ...
				}
				else
				{
					lokace = Vydej.vydejInstance.globalObject.service_lokace.ShowMaterial(seRow.ITEMNMBR, siRow == null ? string.Empty : siRow.SERLTNUM, siRow.IsSKL_IDNull() ? string.Empty : siRow.SKL_ID, null, false);  // TODO: konfiguracne pocet zaznamu ...
				}

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message, "Vydej.VydejZobrazeniLokaciList, OnlineGetPrijmoveLokace");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                return null;
            }
            
            return lokace;
        }

        private void miAktualizovat_Click(object sender, EventArgs e)
        {
            try
            {
                PerformUpdate();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message, "Vydej.VydejZobrazeniLokaciList, Aktualizovat");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
        }

        private void PerformUpdate()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                ds = OnlineGetLokace(seRow, siRow);
                if (ds != null)
                {
                    bs = new BindingSource();
                    bs.DataSource = ds.CZMST_SkladLokace_Stav;
                    dataGrid1.DataSource = bs;
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Logging.Log.Write(ex);
                MessageBoxBig.Show("Načtení lokací se nezdařilo.\n" + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
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
                using (SejmiKodForm skf = new SejmiKodForm("Lokace", SejmiKodForm.TypeOfCode.AlphaNumeric, 0, false, false, string.Empty))
                {
                    skf.Text = "Zadejte lokaci";
                    DialogResult dr = skf.ShowDialog();

                    if (dr != DialogResult.OK)
                    {
                        ScannerStart();
                        return;
                    }

                    kod = skf.Kod;
                }

                najdipolozku(kod);
                // kontrola, zdali je lokace v seznamu, pokud neni, zobrazi se dotaz
                //string selectcmd = "LOCNCODE = '" + kod + "'";
                //var tables = ds.CZMST_SkladLokace_Mapa.Select(selectcmd);
                //if (tables.Count() == 0)
                //{
                //    DialogResult dr = MessageBoxBig.Show("Lokace '" + kod + "' nebyla nalezena v seznamu.\nChcete ji přesto použít?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Information);
                //    if (dr != DialogResult.Yes)
                //    {
                //        ScannerStart();
                //        return;
                //    }
                //}

                //// vytvoreni noveho zaznamu ... vyhledat a dotaz??
                //Fask.MST_W.LokaceService.Location dsLokace = new Fask.MST_W.LokaceService.Location();
                //Fask.MST_W.LokaceService.Location.CZMST_SkladLokace_MapaRow lokaceRow = dsLokace.CZMST_SkladLokace_Mapa.NewCZMST_SkladLokace_MapaRow();
                //lokaceRow.SKL_ID = sklad != null ? sklad.skl_id : string.Empty;
                //lokaceRow.LOCNCODE = kod;
                //lokaceRow.IS_RECEIVE = 0;
                //lokaceRow.DEX_ROW_ID = 1;
                //lokaceRow.SetTYPENull();

                //PrijmovaLokace = lokaceRow;
                //finalize();
                //DialogResult = DialogResult.OK;
                
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message, "Vydej.VydejZobrazeniLokaciList, Najit lokaci");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
            finally
            {
                ScannerStart();
            }
        }
    }
}