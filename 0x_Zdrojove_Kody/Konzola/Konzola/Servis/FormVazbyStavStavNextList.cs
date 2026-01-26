using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fask.Logging;
using Konzola.Extensions;
using Konzola;
using System.Reflection;

namespace Konzola.Servis
{
    public partial class FormVazbyStavStavNextList : Form
    {
        private Fask.Interfaces.IMES providerServis = null;

        /// <summary>
        /// Zvolené id Stav před seřazením
        /// </summary>
        string selectedStavID = string.Empty;
        /// <summary>
        /// Zvolené id StavNext před seřazením
        /// </summary>
        string selectedStavNextID = string.Empty;
        /// <summary>
        /// Vybraný stav
        /// </summary>
        public Fask.Interfaces.DataSets.Servis.CZMST_Servis_StavRow rowStav
        {
            get
            {
                try
                {
                    return ((DataRowView)(dgStav.BindingContext[bsStav].Current)).Row as Fask.Interfaces.DataSets.Servis.CZMST_Servis_StavRow;
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Vybraný stavNext
        /// </summary>
        public Fask.Interfaces.DataSets.Servis.CZMST_Servis_StavRow rowStavNext
        {
            get
            {
                try
                {
                    return ((DataRowView)(dgStavNext.BindingContext[bsStavNext].Current)).Row as Fask.Interfaces.DataSets.Servis.CZMST_Servis_StavRow;

                }
                catch
                {
                    return null;
                }
            }
        }

        public FormVazbyStavStavNextList()
        {
            try
            {
                InitializeComponent();

                this.dgStav.UpdateColumnHeaderCellsByDatasource();
                this.dgStavNext.UpdateColumnHeaderCellsByDatasource();
                panelButtons.Menu = menuStrip2;

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        private void FormVazbyStavStavNextList_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                this.WindowState = FormWindowState.Maximized;
                this.dgStav.LoadConfiguration(this.GetType().ToString());
                this.dgStavNext.LoadConfiguration(this.GetType().ToString() + "next");

                panelButtons.LoadConfiguration(this.GetType().ToString());
                panelButtons.Init();

                advancedDataGridViewSearchToolBar_Stav.SetColumns(dgStav.Columns);
                advancedDataGridViewSearchToolBar_StavNext.SetColumns(dgStavNext.Columns);

                // inicializace providera
                InitProvider();

                if (providerServis == null)
                    throw new Exception("Provider není inicializován");

                System.Threading.Thread loadThread = new System.Threading.Thread(LoadDataAsync);
                loadThread.IsBackground = true;
                loadThread.Start();

                this.splitContainer1.SplitterDistance = (this.splitContainer1.Size.Width / 2) + (this.splitContainer1.SplitterWidth / 2);
                //FillLabels();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void FormVazbyStavStavNextList_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Shift && !e.Control && !e.Alt)
            {
                if (e.KeyCode == Keys.Escape)
                {
                    PerformCancel();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    //PerformOK();
                }
                else
                    return;
            }
            else
                return;

            e.Handled = true;
        }

        private void FormVazbyStavStavNextList_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.dgStav.SaveConfiguration(this.GetType().ToString());
                this.dgStavNext.SaveConfiguration(this.GetType().ToString() + "next");
                panelButtons.SaveConfiguration(this.GetType().ToString());


                Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis_Ostatni[0].FormVazbyStavStavNextListSplitterDistance = this.splitContainer1.SplitterDistance;
                Konfigurace.Globals_Konfig_Konzola.SaveConfiguration();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormVazbyStavStavNextList_Shown(object sender, EventArgs e)
        {
            try
            {
                this.splitContainer1.SplitterDistance = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis_Ostatni[0].FormVazbyStavStavNextListSplitterDistance;
            }
            catch { }
        }



        /// <summary>
        /// Inicializace providera
        /// </summary>
        private void InitProvider()
        {
            try
            {
                if (!String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                {
                    if (providerServis == null) //inicializace se provede pouze pokud nebyla provedena ... 
                    {
                        //Assembly providerAssemlby = Assembly.LoadFrom(Server.MapPath(providerAssemblyPath));
                        Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                        Type[] types = providerAssemlby.GetTypes();
                        foreach (Type t in types)
                        {
                            try
                            {
                                //t.IsAssignableFrom(typeof(Fask.Server.Interfaces.Vydej.IVydej);
                                if (typeof(Fask.Interfaces.Servis.IServis).IsAssignableFrom(t))
                                {
                                    providerServis = (Fask.Interfaces.Servis.IServis)providerAssemlby.CreateInstance(t.FullName);
                                    if (providerServis != null)
                                        break;
                                }
                            }
                            catch { }
                        }
                        //return config;
                    }

                    // nastaveni connection stringu
                    if (providerServis != null)
                        ((Fask.Interfaces.Servis.IServis)providerServis).ConnectionString = Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].FASKDB_ConnesctionString;
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// nacteni dat v jinem vlakne
        /// </summary>
        private void LoadDataAsync()
        {
            try
            {
                Fask.Interfaces.DataSets.Servis ds = new Fask.Interfaces.DataSets.Servis();

                ds = ((Fask.Interfaces.Servis.IServis)providerServis).GetStavy();

                // navrat do hlavniho vlakna
                if (InvokeRequired)
                {
                    BeginInvoke(new Action(() =>
                    {
                        PopulateUI(ds);
                    }));
                }
            }
            catch (Exception ex)
            {
                if (InvokeRequired)
                {
                    BeginInvoke(new Action(() =>
                    {
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                        MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }));
                }
            }
            //UpdateForm();
        }

        private void PopulateUI(Fask.Interfaces.DataSets.Servis ds)
        {
            try
            {
                dsStav.Clear();
                //dsServis = providerServis.GetZdroje();
                bsStav.DataSource = ds;
                FillLabels();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Aktualizace Stav a StavNext po aktualizaci.
        /// </summary>
        private void UpdateStavForm(Fask.Interfaces.DataSets.Servis.CZMST_Servis_StavRow rowStav, Fask.Interfaces.DataSets.Servis.CZMST_Servis_StavRow rowStavNext)
        {
            try
            {
                string stavID = rowStav != null ? rowStav.ID : string.Empty;
                string stavIDNext = rowStavNext != null ? rowStavNext.ID : string.Empty;

                // aktualizace Stav
                dsStav.Clear();
                dsStav = ((Fask.Interfaces.Servis.IServis)providerServis).GetStavy();
                bsStav.DataSource = dsStav;

                try
                {
                    if (!string.IsNullOrEmpty(stavID))
                    {
                        int index = bsStav.Find(dsStavNext.CZMST_Servis_Stav.IDColumn.ColumnName, stavID);
                        this.bsStav.Position = index;
                    }
                }
                catch
                {
                }

                // aktualizace StavNext
                dsStavNext.Clear();
                if (!string.IsNullOrEmpty(stavID))
                    dsStavNext = ((Fask.Interfaces.Servis.IServis)providerServis).GetStavyNextByStavID(stavID);

                bsStavNext.DataSource = dsStavNext;

                try
                {
                    if (!string.IsNullOrEmpty(stavIDNext))
                    {
                        int index = bsStavNext.Find(dsStavNext.CZMST_Servis_StavNext.IDColumn.ColumnName, stavIDNext);
                        this.bsStavNext.Position = index;
                    }
                }
                catch
                {
                }

                dgStavNext.Focus();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show("Nepodařilo se obnovit záznamy\n" + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Aktualizace StavNext po aktualizaci.
        /// </summary>
        private void UpdateStavNextForm(Fask.Interfaces.DataSets.Servis.CZMST_Servis_StavRow rowStav, Fask.Interfaces.DataSets.Servis.CZMST_Servis_StavRow rowStavNextSelected)
        {
            try
            {
                string rowStavID = rowStav != null ? rowStav.ID : string.Empty;
                string selectedStavNext = rowStavNextSelected != null ? rowStavNextSelected.ID : string.Empty;

                dsStavNext.Clear();

                if (!string.IsNullOrEmpty(rowStavID))
                    dsStavNext = ((Fask.Interfaces.Servis.IServis)providerServis).GetStavyNextByStavID(rowStavID);
                
                bsStavNext.DataSource = dsStavNext;

                try
                {
                    if (!string.IsNullOrEmpty(selectedStavNext))
                    {
                        int index = bsStavNext.Find(dsStavNext.CZMST_Servis_StavNext.IDColumn.ColumnName, selectedStavNext);
                        this.bsStavNext.Position = index;
                    }
                }
                catch
                {
                }
                dgStavNext.Focus();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show("Nepodařilo se obnovit záznamy\n" + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PerformCancel()
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FillLabels()
        {
            try
            {
                // Stav
                if (rowStav == null)
                {
                    lblStavID.Text = string.Empty;
                    lblStavOznaceni.Text = string.Empty;
                    lblStavBarcode.Text = string.Empty;
                    lblStavCinnostID.Text = string.Empty;
                }
                else
                {
                    // něco je zvoleno
                    lblStavID.Text = rowStav.ID.Trim();
                    lblStavOznaceni.Text = rowStav.Oznaceni.Trim();
                    lblStavBarcode.Text = rowStav.IsBarcodeNull() ? string.Empty : rowStav.Barcode.Trim();
                    lblStavCinnostID.Text = rowStav.IsIDCinnostNull() ? string.Empty : rowStav.IDCinnost.Trim();
                }

                // StavNext
                if (rowStavNext == null)
                {
                    lblStavNextID.Text = string.Empty;
                    lblStavNextOznaceni.Text = string.Empty;
                    lblStavNextBarcode.Text = string.Empty;
                    lblStavNextCinnostID.Text = string.Empty;
                }
                else
                {
                    // něco je zvoleno
                    lblStavNextID.Text = rowStavNext.ID.Trim();
                    lblStavNextOznaceni.Text = rowStavNext.Oznaceni.Trim();
                    lblStavNextBarcode.Text = rowStavNext.IsBarcodeNull() ? string.Empty : rowStavNext.Barcode.Trim();
                    lblStavNextCinnostID.Text = rowStavNext.IsIDCinnostNull() ? string.Empty : rowStavNext.IDCinnost.Trim();
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            // selectionchanged dgStav
            try
            {
                this.dsStavNext.CZMST_Servis_Stav.Clear();
                this.dsStavNext.CZMST_Servis_Stav.AcceptChanges();

                // nic není zvoleno
                if (rowStav == null)
                {
                    FillLabels();
                    return;
                }

                // naplneni stavnext
                this.dsStavNext = ((Fask.Interfaces.Servis.IServis)providerServis).GetStavyNextByStavID(rowStav.ID);
                bsStavNext.DataSource = this.dsStavNext;

                FillLabels();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                FillLabels();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        private void PerformPriraditVazbu()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (rowStav == null)
                {
                    MessageBox.Show("Není vybrán stav pro přiřazení", this.Text, MessageBoxButtons.OK);
                    return;
                }

                Fask.Interfaces.DataSets.Servis dsSelectedRows = new Fask.Interfaces.DataSets.Servis();
                using (Servis.FormStavySelect frmstavy = new FormStavySelect(true))
                {
                    frmstavy.Text = "Přiřadit stav";
                    //frmstavy.rowsSelected = rowStav;
                    if (frmstavy.ShowDialog(this) != DialogResult.OK)
                        return;

                    // naplneni datasetu vybranymy radky
                    
                    foreach (DataGridViewRow dgview in frmstavy.SelectedRows)
                    {
                        Fask.Interfaces.DataSets.Servis.CZMST_Servis_StavRow item = ((DataRowView)dgview.DataBoundItem).Row as Fask.Interfaces.DataSets.Servis.CZMST_Servis_StavRow;
                        dsSelectedRows.CZMST_Servis_Stav.ImportRow(item);
                    }
                }
                ((Fask.Interfaces.Servis.IServis)providerServis).InsertStavNext(rowStav.ID, dsSelectedRows);
                UpdateStavNextForm(rowStav, dsSelectedRows.CZMST_Servis_Stav.Count > 0 ? dsSelectedRows.CZMST_Servis_Stav.First() : null);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PerformOdstranitVazbu()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (rowStav == null)
                {
                    MessageBox.Show("Není vybrán záznam stav", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if (rowStavNext == null)
                {
                    MessageBox.Show("Není vybrán záznam pro odstranění", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if (MessageBox.Show("Opravdu chcete odstranit přiřazený stav '" + rowStavNext.Oznaceni.Trim() + "'?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

                ((Fask.Interfaces.Servis.IServis)providerServis).DeleteStavNext(rowStav.ID, rowStavNext.ID);
                UpdateStavNextForm(rowStav, rowStavNext);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            try
            {
                int pos = this.bsStav.Find(dsStav.CZMST_Servis_Stav.IDColumn.ColumnName, selectedStavID);
                this.bsStav.Position = pos;

                int pos2 = this.bsStavNext.Find(dsStavNext.CZMST_Servis_Stav.IDColumn.ColumnName, selectedStavNextID);
                this.bsStavNext.Position = pos2;
            }
            catch (Exception)
            {
            }            
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1)
                {
                    if (rowStav != null)
                        selectedStavID = rowStav.ID;

                    if (rowStavNext != null)
                        selectedStavNextID = rowStavNext.ID;
                }
            }
            catch (Exception)
            {
            }
        }

        private void dataGridView2_Sorted(object sender, EventArgs e)
        {
            try
            {
                int pos2 = this.bsStavNext.Find("ID", selectedStavNextID);
                this.bsStavNext.Position = pos2;
            }
            catch (Exception)
            {
            }
        }

        private void dataGridView2_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1)
                {
                    if (rowStavNext != null)
                        selectedStavNextID = rowStavNext.ID;
                }
            }
            catch (Exception)
            {
            }
        }

        private void tsmiKonec_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void tsmiAktualizovat_Click(object sender, EventArgs e)
        {
            UpdateStavForm(rowStav, rowStavNext);
        }

        private void tsmiOdstranitVazbu_Click(object sender, EventArgs e)
        {
            PerformOdstranitVazbu(); 
        }

        private void tsmiPridatVazbu_Click(object sender, EventArgs e)
        {
            PerformPriraditVazbu();
        }

        private void advancedDataGridViewSearchToolBar_Stav_Search(object sender, Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs e)
        {
            bool restartsearch = true;
            int startColumn = 0;
            int startRow = 0;
            if (!e.FromBegin)
            {
                bool endcol = dgStav.CurrentCell.ColumnIndex + 1 >= dgStav.ColumnCount;
                bool endrow = dgStav.CurrentCell.RowIndex + 1 >= dgStav.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dgStav.CurrentCell.ColumnIndex;
                    startRow = dgStav.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dgStav.CurrentCell.ColumnIndex + 1;
                    startRow = dgStav.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dgStav.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dgStav.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dgStav.CurrentCell = c;
        }

        private void advancedDataGridViewSearchToolBar_StavNext_Search(object sender, Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs e)
        {
            bool restartsearch = true;
            int startColumn = 0;
            int startRow = 0;
            if (!e.FromBegin)
            {
                bool endcol = dgStavNext.CurrentCell.ColumnIndex + 1 >= dgStavNext.ColumnCount;
                bool endrow = dgStavNext.CurrentCell.RowIndex + 1 >= dgStavNext.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dgStavNext.CurrentCell.ColumnIndex;
                    startRow = dgStavNext.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dgStavNext.CurrentCell.ColumnIndex + 1;
                    startRow = dgStavNext.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dgStavNext.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dgStavNext.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dgStavNext.CurrentCell = c;
        }
    }
}
