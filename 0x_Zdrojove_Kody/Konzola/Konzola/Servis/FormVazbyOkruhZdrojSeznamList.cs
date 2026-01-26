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
    public partial class FormVazbyOkruhZdrojSeznamList : Form
    {
        private Fask.Interfaces.IMES providerServis = null;

        /// <summary>
        /// Zvolené id Stav před seřazením
        /// </summary>
        string selectedOkruhID = string.Empty;
        /// <summary>
        /// Zvolené id StavNext před seřazením
        /// </summary>
        string selectedZdrojSeznamID = string.Empty;
        /// <summary>
        /// Vybraný ookruh
        /// </summary>
        public Fask.Interfaces.DataSets.Servis.CZMST_Servis_OkruhRow rowOkruh
        {
            get
            {
                try
                {
                    return ((DataRowView)(dgStav.BindingContext[bsStav].Current)).Row as Fask.Interfaces.DataSets.Servis.CZMST_Servis_OkruhRow;
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Vybraný ZdrojSeznam
        /// </summary>
        public Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojSeznamRow rowZdrojSeznam
        {
            get
            {
                try
                {
                    return ((DataRowView)(dgStavNext.BindingContext[bsStavNext].Current)).Row as Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojSeznamRow;
                }
                catch
                {
                    return null;
                }
            }
        }


        #region Eventy Formu

        public FormVazbyOkruhZdrojSeznamList()
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

        private void FormVazbyOkruhZdrojSeznamList_Load(object sender, EventArgs e)
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

        private void FormVazbyOkruhZdrojSeznamList_KeyDown(object sender, KeyEventArgs e)
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

        private void FormVazbyOkruhZdrojSeznamList_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.dgStav.SaveConfiguration(this.GetType().ToString());
                this.dgStavNext.SaveConfiguration(this.GetType().ToString() + "next");

                panelButtons.SaveConfiguration(this.GetType().ToString());

                Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis_Ostatni[0].FormVazbyOkruhZdrojSeznamListSplitterDistance = this.splitContainer1.SplitterDistance;
                Konfigurace.Globals_Konfig_Konzola.SaveConfiguration();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormVazbyOkruhZdrojSeznamList_Shown(object sender, EventArgs e)
        {
            try
            {
                this.splitContainer1.SplitterDistance = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis_Ostatni[0].FormVazbyOkruhZdrojSeznamListSplitterDistance;
            }
            catch { }
        } 

        #endregion


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

                ds = ((Fask.Interfaces.Servis.IServis)providerServis).GetOkruhy();

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
        private void UpdateStavForm(Fask.Interfaces.DataSets.Servis.CZMST_Servis_OkruhRow rowOkruh, Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojSeznamRow rowZdrojSeznam)
        {
            try
            {
                string okruhID = rowOkruh != null ? rowOkruh.ID : string.Empty;
                string zdrojSeznamID = rowOkruh != null ? rowOkruh.ZdrojSeznamID : string.Empty;
                string zdrojID = rowZdrojSeznam != null ? rowZdrojSeznam.ZdrojID : string.Empty;

                // aktualizace okruhu
                dsStav.Clear();
                dsStav = ((Fask.Interfaces.Servis.IServis)providerServis).GetOkruhy();
                bsStav.DataSource = dsStav;

                try
                {
                    if (!string.IsNullOrEmpty(okruhID))
                    {
                        int index = bsStav.Find(dsStav.CZMST_Servis_Okruh.IDColumn.ColumnName, okruhID);
                        this.bsStav.Position = index;
                    }
                }
                catch
                {
                }

                // aktualizace StavNext
                dsStavNext.Clear();
                if (!string.IsNullOrEmpty(zdrojSeznamID))
                    dsStavNext = ((Fask.Interfaces.Servis.IServis)providerServis).GetZdrojSeznamByID(zdrojSeznamID);

                bsStavNext.DataSource = dsStavNext;

                try
                {
                    if (!string.IsNullOrEmpty(zdrojID))
                    {
                        int index = bsStavNext.Find(dsStavNext.CZMST_Servis_ZdrojSeznam.ZdrojIDColumn.ColumnName, zdrojID);
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
        private void UpdateStavNextForm(Fask.Interfaces.DataSets.Servis.CZMST_Servis_OkruhRow rowOkruh, Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojSeznamRow rowSZdrojSeznamSelected)
        {
            try
            {
                string rowSeznamID = rowOkruh != null ? rowOkruh.ZdrojSeznamID : string.Empty;
                string selectedZdrojSeznam = rowSZdrojSeznamSelected != null ? rowSZdrojSeznamSelected.ZdrojID : string.Empty;

                dsStavNext.Clear();

                if (!string.IsNullOrEmpty(rowSeznamID))
                    dsStavNext = ((Fask.Interfaces.Servis.IServis)providerServis).GetZdrojSeznamByID(rowSeznamID);

                bsStavNext.DataSource = dsStavNext;

                try
                {
                    if (!string.IsNullOrEmpty(selectedZdrojSeznam))
                    {
                        int index = bsStavNext.Find(dsStavNext.CZMST_Servis_ZdrojSeznam.ZdrojIDColumn.ColumnName, selectedZdrojSeznam);
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

        private void tsmiKonec_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void tsmiAktualizovat_Click(object sender, EventArgs e)
        {
            UpdateStavForm(rowOkruh, rowZdrojSeznam);
        }


        private void FillLabels()
        {
            try
            {
                // Okruh
                if (rowOkruh == null)
                {
                    lblOkruhID.Text = string.Empty;
                    lblOkruhOznaceni.Text = string.Empty;
                    lblOkruhBarcode.Text = string.Empty;
                    lblOkruhOdberatel.Text = string.Empty;
                    lblOkruhIDSeznamuZdroju.Text = string.Empty;
                }
                else
                {
                    // něco je zvoleno
                    lblOkruhID.Text = rowOkruh.ID.Trim();
                    lblOkruhOznaceni.Text = rowOkruh.Oznaceni.Trim();
                    lblOkruhBarcode.Text = rowOkruh.IsBarcodeNull() ? string.Empty : rowOkruh.Barcode.Trim();
                    lblOkruhIDSeznamuZdroju.Text = rowOkruh.ZdrojSeznamID.Trim();
                    lblOkruhOdberatel.Text = rowOkruh.IsOdberatelOznaceniNull() ? string.Empty : rowOkruh.OdberatelOznaceni.Trim();
                }

                // StavNext
                if (rowZdrojSeznam == null)
                {
                    lblZdrojSeznamZdrojID.Text = string.Empty;
                    lblZdrojSeznamZdrojOznaceni.Text = string.Empty;
                    lblZdrojSeznamZdrojBarcode.Text = string.Empty;
                    lblZdrojSeznamStavID.Text = string.Empty;
                    lblZdrojSeznamCinnostID.Text = string.Empty;
                }
                else
                {
                    // něco je zvoleno
                    lblZdrojSeznamZdrojID.Text = rowZdrojSeznam.ZdrojID.Trim();
                    lblZdrojSeznamZdrojOznaceni.Text = rowZdrojSeznam.IsZdrojOznaceniNull() ? string.Empty : rowZdrojSeznam.ZdrojOznaceni.Trim();
                    lblZdrojSeznamZdrojBarcode.Text = rowZdrojSeznam.IsZdrojBarcodeNull() ? string.Empty : rowZdrojSeznam.ZdrojBarcode.Trim();
                    lblZdrojSeznamStavID.Text = rowZdrojSeznam.IsIDStavNull() ? string.Empty : rowZdrojSeznam.IDStav.Trim();
                    lblZdrojSeznamCinnostID.Text = rowZdrojSeznam.IsIDCinnostNull() ? string.Empty : rowZdrojSeznam.IDCinnost.Trim();
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
                this.dsStavNext.CZMST_Servis_ZdrojSeznam.Clear();
                this.dsStavNext.CZMST_Servis_ZdrojSeznam.AcceptChanges();

                // nic není zvoleno
                if (rowOkruh == null)
                {
                    FillLabels();
                    return;
                }

                // naplneni stavnext
                this.dsStavNext = ((Fask.Interfaces.Servis.IServis)providerServis).GetZdrojSeznamByID(rowOkruh.ZdrojSeznamID);
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

                if (rowOkruh == null)
                {
                    MessageBox.Show("Není vybrán stav pro přiřazení", this.Text, MessageBoxButtons.OK);
                    return;
                }
                
                Fask.Interfaces.DataSets.Servis dsSelectedRows = new Fask.Interfaces.DataSets.Servis();
                using (Servis.FormZdrojeSelect frmzdroje = new FormZdrojeSelect(true))
                {
                    frmzdroje.Text = "Přiřadit zdroj";
                    //frmstavy.rowsSelected = rowStav;
                    if (frmzdroje.ShowDialog(this) != DialogResult.OK)
                        return;

                    // naplneni datasetu vybranymy zdroji
                    foreach (DataGridViewRow dgview in frmzdroje.SelectedRows)
                    {
                        Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojRow item = ((DataRowView)dgview.DataBoundItem).Row as Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojRow;
                        //dsSelectedRows.CZMST_Servis_Zdroj.ImportRow(item);
                        Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojSeznamRow newrow = dsSelectedRows.CZMST_Servis_ZdrojSeznam.NewCZMST_Servis_ZdrojSeznamRow();
                        newrow.ID = rowOkruh.ID;
                        newrow.ZdrojID = item.ID;
                        newrow.SetPoradiNull();
                        newrow.SetIDStavNull();
                        newrow.SetIDCinnostNull();
                        dsSelectedRows.CZMST_Servis_ZdrojSeznam.AddCZMST_Servis_ZdrojSeznamRow(newrow);
                    }
                }
                ((Fask.Interfaces.Servis.IServis)providerServis).InsertZdrojSeznam(rowOkruh.ZdrojSeznamID, dsSelectedRows);
                //UpdateStavNextForm(rowOkruh, dsSelectedRows.CZMST_Servis_Zdroj.Count > 0 ? dsSelectedRows.CZMST_Servis_Zdroj.First() : null);
                UpdateStavNextForm(rowOkruh, dsSelectedRows.CZMST_Servis_ZdrojSeznam.Count > 0 ? dsSelectedRows.CZMST_Servis_ZdrojSeznam.First() : null);
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

                if (rowOkruh == null)
                {
                    MessageBox.Show("Není vybrán okruh", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if (rowZdrojSeznam == null)
                {
                    MessageBox.Show("Není vybrán záznam pro odstranění", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if (MessageBox.Show("Opravdu chcete odstranit přiřazený zdroj '" + (rowZdrojSeznam.IsZdrojOznaceniNull() ? rowZdrojSeznam.ZdrojID.Trim() : rowZdrojSeznam.ZdrojOznaceni.Trim()) + "'?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

                ((Fask.Interfaces.Servis.IServis)providerServis).DeleteZdrojSeznamByIDAndZdrojID(rowZdrojSeznam.ID, rowZdrojSeznam.ZdrojID);
                UpdateStavNextForm(rowOkruh, rowZdrojSeznam);
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
                int pos = this.bsStav.Find(dsStav.CZMST_Servis_Okruh.IDColumn.ColumnName, selectedOkruhID);
                this.bsStav.Position = pos;

                int pos2 = this.bsStavNext.Find(dsStavNext.CZMST_Servis_ZdrojSeznam.ZdrojIDColumn.ColumnName, selectedZdrojSeznamID);
                this.bsStavNext.Position = pos2;
            }
            catch
            {
            }            
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1)
                {
                    if (rowOkruh != null)
                        selectedOkruhID = rowOkruh.ID;

                    if (rowZdrojSeznam != null)
                        selectedZdrojSeznamID = rowZdrojSeznam.ZdrojID;
                }
            }
            catch
            {
            }
        }

        private void dataGridView2_Sorted(object sender, EventArgs e)
        {
            try
            {
                int pos2 = this.bsStavNext.Find(dsStavNext.CZMST_Servis_ZdrojSeznam.ZdrojIDColumn.ColumnName, selectedZdrojSeznamID);
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
                    if (rowZdrojSeznam != null)
                        selectedZdrojSeznamID = rowZdrojSeznam.ZdrojID;
                }
            }
            catch (Exception)
            {
            }
        }

        private void tsmiPridatVazbu_Click(object sender, EventArgs e)
        {
            PerformPriraditVazbu();
        }

        private void tsmiOdstranitVazbu_Click(object sender, EventArgs e)
        {
            PerformOdstranitVazbu(); 
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
