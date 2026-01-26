using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Fask.MST_W.Forms;

namespace Fask.MST_W.Prijem_4
{
    public partial class PrijemNezrealizovanePrijemkyList : Form
    {
        // nactena data z online metody
        //private Fask.MST_W.PrijemService.Obecne ds;
        //private BindingSource bs;

        private List<string> ListCarKod;

        private DataView pohled = null;
        public string ponumber;

        public Fask.MST_W.PrijemService.Obecne.PrijemkyRow _prijemkaRow
        {
            get
            {
                try
                {
                    //return ((DataRowView)(dataGrid1.BindingContext[bsServis].Current)).Row as Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_Dynamic_TableRow;
                    return ((DataRowView)(dataGrid1.BindingContext[bindingSource1].Current)).Row as Fask.MST_W.PrijemService.Obecne.PrijemkyRow;
                    //return null;
                }
                catch (Exception ex)
                {
                    Logging.Log.Write(ex);
                    return null;
                }
            }
        }


        public PrijemNezrealizovanePrijemkyList()
        {
            InitializeComponent();


        }


        //public PrijemNezrealizovanePrijemkyList(Fask.MST_W.PrijemService.Obecne ds) : this()
        //{
        //    //InitializeComponent(); 
        //    try
        //    {
        //        this.ds = ds;
        //        //bs = new BindingSource();
        //        //bs.DataSource = ds.Prijemky;
        //        //dataGrid1.DataSource = bs;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logging.Log.Write(ex, "PrijemNezrealizovanePrijemkyList load");
        //    }
        //}

        private void ServisDynamickaTabulka_Load(object sender, EventArgs e)
        {
            // nacteni lokalizace ze souboru
            Fask.Localization.LocalizationExtensionForm.Localize(this);

            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            this.Size = Forms.FormLocation.ScreenResolution;

            ListCarKod = new List<string>();

            miMenuZobrazitSeznam.Checked = Settings.PrijemNerealizovanePrijekyDVNZ;

            if (Settings.PrijemNerealizovanePrijekyDVNZ)
            {
                this.ds = OnlineGetNezrealizovanePrijemky();
                updateForm();
             }

            UpdateStatusBar(ListCarKod.Count);
            

            //CreateGridStyles();

            InitializeGrid();

            ScannerStart();
            panelButtons_Resize(null, null);

            dataGrid1.Focus();
            try
            {
                dataGrid1.CurrentRowIndex = 0;
            }
            catch
            {
            }
        }

        private void UpdateStatusBar(int NasnimaneCarKody) 
        {
            statusBar1.Text = string.Format("C:{0}", NasnimaneCarKody);
        }

        //private void CreateGridStyles()
        //{
        //    //if (ds != null)
        //    //{
        //        DataGridTableStyle ts = new DataGridTableStyle();

        //        ts.MappingName = ds.Prijemky.TableName; // ds_servis.CZMST_Servis_Dynamic_Table.TableName;

        //        Fask.Graphic.DataGrid2TextBoxColumn dg = new Fask.Graphic.DataGrid2TextBoxColumn();
        //        dg.HeaderText = "Název";
        //        dg.MappingName = ds.Prijemky.NameColumn.ColumnName;
        //        dg.NullText = "-";
        //        //dg.SelectionShow = true;
        //        dg.Width = 50;
        //        ts.GridColumnStyles.Add(dg);

        //        dg = new Fask.Graphic.DataGrid2TextBoxColumn();
        //        dg.HeaderText = "Obj. č.";
        //        dg.MappingName = ds.Prijemky.PONUMBERColumn.ColumnName;
        //        dg.NullText = "-";
        //        //dg.SelectionShow = true;
        //        dg.Width = 50;
        //        ts.GridColumnStyles.Add(dg);

        //        dg = new Fask.Graphic.DataGrid2TextBoxColumn();
        //        dg.HeaderText = "Čár. k."; //ds.Palety.SERLTNUMColumn.Caption; //ds_servis.CZMST_Servis_Dynamic_Table.IDColumn.Caption;
        //        dg.MappingName = ds.Prijemky.CZ_CarKodColumn.ColumnName; // ds_servis.CZMST_Servis_Dynamic_Table.IDColumn.ColumnName;
        //        dg.NullText = "-";
        //        //dg.SelectionShow = true;
        //        dg.Width = 50;
        //        ts.GridColumnStyles.Add(dg);

        //        dg = new Fask.Graphic.DataGrid2TextBoxColumn();
        //        dg.HeaderText = "Dátum"; //ds.Palety.SERLTNUMColumn.Caption; //ds_servis.CZMST_Servis_Dynamic_Table.IDColumn.Caption;
        //        dg.MappingName = ds.Prijemky.DateTimeColumn.ColumnName; // ds_servis.CZMST_Servis_Dynamic_Table.IDColumn.ColumnName;
        //        dg.NullText = "-";
        //        //dg.SelectionShow = true;
        //        dg.Width = 50;
        //        ts.GridColumnStyles.Add(dg);


        //        dg = new Fask.Graphic.DataGrid2TextBoxColumn();
        //        dg.HeaderText = "Popis"; //ds.Palety.SERLTNUMColumn.Caption; //ds_servis.CZMST_Servis_Dynamic_Table.IDColumn.Caption;
        //        dg.MappingName = ds.Prijemky.DescColumn.ColumnName; // ds_servis.CZMST_Servis_Dynamic_Table.IDColumn.ColumnName;
        //        dg.NullText = "-";
        //        //dg.SelectionShow = true;
        //        dg.Width = 50;
        //        ts.GridColumnStyles.Add(dg);

        //        dataGrid1.TableStyles.Add(ts); 
        //    //}
        //}

        private void InitializeGrid()
        {
            this.dataGrid1.InitializeTableGridColumnStyles(Settings.UIFormatDesCisel);
            this.dataGrid1.Font = new Font(this.dataGrid1.Font.Name, Settings.UIGridFont, this.dataGrid1.Font.Style);
            this.dataGrid1.Load(Path.Combine(Main.ConfigDir, this.GetType().ToString()));
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
            if (e.KeyCode == Keys.F1)
            {
                NajdiPonumber();
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



                if (Prijem_4.Globals.NerealizovanePrijekyCarKody)
                {
                    this.BeginInvoke(new DelegateString(DohledejDavku), new object[] { kod });
                }
                else
                {
                    this.BeginInvoke(new DelegateString(najdipolozku), new object[] { kod });
                }
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
                bindingSource1.Filter = string.Empty;
                string selectcmd = "CZ_CarKod = '" + kod + "'";
                var tables = ds.Prijemky.Select(selectcmd);
                
                if (tables.Count() == 0)
                {
                    DialogResult dr = MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prijem4PrijemNezrealizovanePrijemkyListZaznamNenalezenPokracovatDotaz, kod), this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Information);
                    if (dr == DialogResult.No)
                        return;

                    ponumber = kod;

                    finalize();
                    DialogResult = DialogResult.OK;
                    return;
                }
                else if (tables.Count() > 1)
                {
                    bindingSource1.Filter = "CZ_CarKod='" + kod + "'";
                    MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prijem4PrijemNezrealizovanePrijemkyListNalezenoViceCK, kod), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return;
                }
                else
                {
                    bindingSource1.Filter = "CZ_CarKod='" + kod + "'";
                }

                ponumber = _prijemkaRow.PONUMBER;

                finalize();
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
            finally
            {
                if (MST_Global.OnScannerSound_Prijem_4)
                {
                    MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, "scannerready.wav"));
                }
            }
        }


        /// <summary>
        /// Dohledej položku podle čárového kódu
        /// </summary>
        /// <param name="kod"></param>
        private void DohledejDavku(string kod)
        {
            try
            {
                if (ListCarKod == null)
                    ListCarKod = new List<string>();

                if (ListCarKod.Contains(kod))
                    return;

                    ListCarKod.Add(kod);
                
                //Dohledani veci z serveru....
               this.ds = this.OnlineGetNezrealizovanePrijemky();



               //this.ds = ds;
               //bs = new BindingSource();
               //bs.DataSource = ds.Prijemky;
               //dataGrid1.DataSource = bs;

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
            finally
            {
                updateForm();
                UpdateStatusBar(ListCarKod.Count);

                if (MST_Global.OnScannerSound_Prijem_4)
                {
                    MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, "scannerready.wav"));
                }
            }
        }

        private void updateForm()
        {
            pohled = new DataView();
            if (ds.Prijemky != null)
                pohled.Table = ds.Prijemky;
            else
                pohled.Table = new Fask.MST_W.PrijemService.Obecne.PrijemkyDataTable();

            bindingSource1.DataSource = pohled;

            //hlavickyBindingSource.DataSource = davky;
            //hlavickyBindingSource.DataMember = davky.Hlavicky.TableName;
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
                if (_prijemkaRow == null)
                {
                    MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemNezrealizovanePrijemkyListNeniVybranaPaleta, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                    return;
                }

                ponumber = _prijemkaRow.PONUMBER;

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

        private void miNajitPonumber_Click(object sender, EventArgs e)
        {
            // najiti podle PONUMBER
            try
            {
                NajdiPonumber();
            }
            catch (Exception ex)
            {
                Fask.Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
            
        }

        private void NajdiPonumber()
        {
            try
            {
                string kod = string.Empty;
                filtrRemove();

                try
                {
                    ScannerStop();                    
                    
                    using (SejmiKodForm skf = new SejmiKodForm(Fask.Localization.Localization.Prijem4PrijemNezrealizovanePrijemkyListZadejteCisloObjednavky, SejmiKodForm.TypeOfCode.AlphaNumeric))
                    {
                        if (skf.ShowDialog() == DialogResult.OK)
                        {
                            kod = skf.Kod.Trim();
                        }
                        else
                            return;
                    }
                }
                catch
                {
                    throw;
                }
                finally
                {
                    ScannerStart();
                }

                // najiti podle PONUMBER
                bindingSource1.Filter = string.Empty;
                //string selectcmd = "PONUMBER = '" + kod + "'";
                string selectcmd = "PONUMBER like '%" + kod + "%'";
                var tables = ds.Prijemky.Select(selectcmd);

                if (tables.Count() == 0)
                {
                    DialogResult dr = MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prijem4PrijemNezrealizovanePrijemkyListObjNenalezenaPokracovatDotaz, kod), this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Information);
                    if (dr == DialogResult.No)
                        return;

                    ponumber = kod;

                    finalize();
                    DialogResult = DialogResult.OK;
                    return;
                }
                else if (tables.Count() > 1)
                {
                    //bs.Filter = "PONUMBER='" + kod + "'";
                    bindingSource1.Filter = "PONUMBER like '%" + kod + "%'";
                    MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prijem4PrijemNezrealizovanePrijemkyListNalezenoViceObj, kod), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return;
                }
                else
                {
                    //bs.Filter = "PONUMBER='" + kod + "'";
                    bindingSource1.Filter = "PONUMBER like '%" + kod + "%'";
                }

                ponumber = _prijemkaRow.PONUMBER;

                finalize();
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Fask.Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
        }

        private void filtrRemove()
        {
            this.bindingSource1.Filter = "";
        }

        private Fask.MST_W.PrijemService.Obecne OnlineGetNezrealizovanePrijemky()
        {
            Fask.MST_W.PrijemService.Obecne ds = new Fask.MST_W.PrijemService.Obecne();// PrijemService.Obecne();

            try
            {
                ds = Prijem_4.PrijemMain.prijemInstance.globalObject.service_prijem.Online_GetNezrealizovanePrijemky_Vyber(MST_Global.TerminalID, Globals.SkladID, string.Empty, ListCarKod.ToArray());
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message, "Prijem.PrijemNezrealizovanePrijemkyList, OnlineGetDoporuceneLokace");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);

                return null;
            }
            return ds;
        }



        private void menuItem3_Click(object sender, EventArgs e)
        {
            if (this.ListCarKod == null || this.ListCarKod.Count == 0)
                return;

            this.ListCarKod.Clear();
            MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, "info.wav"));
            UpdateStatusBar(ListCarKod.Count);
        }

        private void menuItem4_Click(object sender, EventArgs e)
        {
            //

            if (this.ListCarKod== null || this.ListCarKod.Count == 0)
                return;

            this.ListCarKod.RemoveAt(this.ListCarKod.Count - 1);

            MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, "notify.wav"));
            UpdateStatusBar(ListCarKod.Count);

        }

        private void miMenuZobrazitSeznam_Click(object sender, EventArgs e)
        {

            Settings.PrijemNerealizovanePrijekyDVNZ = !Settings.PrijemNerealizovanePrijekyDVNZ;
            miMenuZobrazitSeznam.Checked = Settings.PrijemNerealizovanePrijekyDVNZ;

        }
    }
}