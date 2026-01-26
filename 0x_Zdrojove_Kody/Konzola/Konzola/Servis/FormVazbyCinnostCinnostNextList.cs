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
    public partial class FormVazbyCinnostCinnostNextList : Form
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
        public Fask.Interfaces.DataSets.Servis.CZMST_Servis_CinnostRow rowCinnost
        {
            get
            {
                try
                {
                    return ((DataRowView)(dgCinnost.BindingContext[bsCinnost].Current)).Row as Fask.Interfaces.DataSets.Servis.CZMST_Servis_CinnostRow;
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
        public Fask.Interfaces.DataSets.Servis.CZMST_Servis_CinnostRow rowCinnostNext
        {
            get
            {
                try
                {
                    return ((DataRowView)(dgCinnostNext.BindingContext[bsCinnostNext].Current)).Row as Fask.Interfaces.DataSets.Servis.CZMST_Servis_CinnostRow;

                }
                catch
                {
                    return null;
                }
            }
        }
        
        #region Eventy formu

        public FormVazbyCinnostCinnostNextList()
        {
            try
            {
                InitializeComponent();
                this.dgCinnost.UpdateColumnHeaderCellsByDatasource();
                this.dgCinnostNext.UpdateColumnHeaderCellsByDatasource();
                panelButtons.Menu = menuStrip2;

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormVazbyCinnostCinnostNextList_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                this.WindowState = FormWindowState.Maximized;
                this.dgCinnost.LoadConfiguration(this.GetType().ToString());
                this.dgCinnostNext.LoadConfiguration(this.GetType().ToString() + "next");

                panelButtons.LoadConfiguration(this.GetType().ToString());
                panelButtons.Init();

                advancedDataGridViewSearchToolBar_Cinnost.SetColumns(dgCinnost.Columns);
                advancedDataGridViewSearchToolBar_CinnostNext.SetColumns(dgCinnostNext.Columns);


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

        private void FormVazbyCinnostCinnostNextList_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.dgCinnost.SaveConfiguration(this.GetType().ToString());
                this.dgCinnostNext.SaveConfiguration(this.GetType().ToString() + "next");
                panelButtons.SaveConfiguration(this.GetType().ToString());


                Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis_Ostatni[0].FormVazbyCinnostCinnostNextListSplitterDistance = this.splitContainer1.SplitterDistance;
                Konfigurace.Globals_Konfig_Konzola.SaveConfiguration();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormVazbyCinnostCinnostNextList_KeyDown(object sender, KeyEventArgs e)
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

        private void FormVazbyCinnostCinnostNextList_Shown(object sender, EventArgs e)
        {
            try
            {
                this.splitContainer1.SplitterDistance = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis_Ostatni[0].FormVazbyCinnostCinnostNextListSplitterDistance;

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

                ds = ((Fask.Interfaces.Servis.IServis)providerServis).GetCinnosti();

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
                dsServisCinnost.Clear();
                //dsServis = providerServis.GetZdroje();
                bsCinnost.DataSource = ds;
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
        private void UpdateStavForm(Fask.Interfaces.DataSets.Servis.CZMST_Servis_CinnostRow rowCinnost, Fask.Interfaces.DataSets.Servis.CZMST_Servis_CinnostRow rowCinnostNext)
        {
            try
            {
                string cinnostID = rowCinnost != null ? rowCinnost.ID : string.Empty;
                string cinnostIDNext = (rowCinnostNext != null && !rowCinnostNext.IsIDNull()) ? rowCinnostNext.ID : string.Empty;

                // aktualizace Stav
                dsServisCinnost.Clear();
                dsServisCinnost = ((Fask.Interfaces.Servis.IServis)providerServis).GetCinnosti();
                bsCinnost.DataSource = dsServisCinnost;

                try
                {
                    if (!string.IsNullOrEmpty(cinnostID))
                    {
                        int index = bsCinnost.Find(dsServisCinnostNext.CZMST_Servis_Cinnost.IDColumn.ColumnName, cinnostID);
                        this.bsCinnost.Position = index;
                    }
                }
                catch
                {
                }

                // aktualizace StavNext
                dsServisCinnostNext.Clear();
                if (!string.IsNullOrEmpty(cinnostID))
                    dsServisCinnostNext = ((Fask.Interfaces.Servis.IServis)providerServis).GetCinnostiNextByCinnostID(cinnostID);

                bsCinnostNext.DataSource = dsServisCinnostNext;

                try
                {
                    if (!string.IsNullOrEmpty(cinnostIDNext))
                    {
                        int index = bsCinnostNext.Find(dsServisCinnostNext.CZMST_Servis_CinnostNext.IDColumn.ColumnName, cinnostIDNext);
                        this.bsCinnostNext.Position = index;
                    }
                }
                catch
                {
                }

                dgCinnostNext.Focus();
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
        private void UpdateStavNextForm(Fask.Interfaces.DataSets.Servis.CZMST_Servis_CinnostRow rowCinnost, Fask.Interfaces.DataSets.Servis.CZMST_Servis_CinnostRow rowCinnostNextSelected)
        {
            try
            {
                string rowCinnostID = rowCinnost != null ? rowCinnost.ID : string.Empty;
                string selectedCinnostNext = (rowCinnostNextSelected != null && !rowCinnostNextSelected.IsIDNull()) ? rowCinnostNextSelected.ID : string.Empty;

                dsServisCinnostNext.Clear();

                if (!string.IsNullOrEmpty(rowCinnostID))
                    dsServisCinnostNext = ((Fask.Interfaces.Servis.IServis)providerServis).GetCinnostiNextByCinnostID(rowCinnostID);
                
                bsCinnostNext.DataSource = dsServisCinnostNext;

                try
                {
                    if (!string.IsNullOrEmpty(selectedCinnostNext))
                    {
                        int index = bsCinnostNext.Find(dsServisCinnostNext.CZMST_Servis_CinnostNext.IDColumn.ColumnName, selectedCinnostNext);
                        this.bsCinnostNext.Position = index;
                    }
                }
                catch
                {
                }
                dgCinnostNext.Focus();
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
            UpdateStavForm(rowCinnost, rowCinnostNext);
        }

        private void FillLabels()
        {
            try
            {
                // Stav
                if (rowCinnost == null)
                {
                    lblCinnostID.Text = string.Empty;
                    lblCinnostOznaceni.Text = string.Empty;
                    lblCinnostBarcode.Text = string.Empty;
                    lblCinnostType.Text = string.Empty;
                    lblCinnostTypeValue.Text = string.Empty;
                    lblCinnostMandatory.Text = string.Empty;
                    lblCinnostRequiredLength.Text = string.Empty;
                }
                else
                {
                    // něco je zvoleno
                    lblCinnostID.Text = rowCinnost.IsIDNull() ? string.Empty : rowCinnost.ID.Trim();
                    lblCinnostOznaceni.Text = rowCinnost.Oznaceni.Trim();
                    lblCinnostBarcode.Text = rowCinnost.IsBarcodeNull() ? string.Empty : rowCinnost.Barcode.Trim();
                    lblCinnostType.Text = rowCinnost.TYPE.Trim();
                    lblCinnostTypeValue.Text = rowCinnost.IsTYPEVALUENull() ? string.Empty : rowCinnost.TYPEVALUE.Trim();
                    lblCinnostMandatory.Text = rowCinnost.Mandatory > 0 ? "Ano" : "Ne";
                    lblCinnostRequiredLength.Text = rowCinnost.IsRequiredLengthNull() ? string.Empty : rowCinnost.RequiredLength.ToString();
                }

                // StavNext
                if (rowCinnostNext == null)
                {
                    lblCinnostNextID.Text = string.Empty;
                    lblCinnostNextOznaceni.Text = string.Empty;
                    lblCinnostNextBarcode.Text = string.Empty;
                    lblCinnostNextType.Text = string.Empty;
                    lblCinnostNextTypeValue.Text = string.Empty;
                    lblCinnostNextMandatory.Text = string.Empty;
                    lblCinnostNextRequiredLength.Text = string.Empty;
                    lblCinnostNextIDValue.Text = string.Empty;
                }
                else
                {
                    // něco je zvoleno
                    //lblStavNextID.Text = rowCinnostNext.ID.Trim();
                    // něco je zvoleno
                    lblCinnostNextID.Text = rowCinnostNext.IsIDNull() ? string.Empty : rowCinnostNext.ID.Trim();
                    lblCinnostNextOznaceni.Text = rowCinnostNext.Oznaceni.Trim();
                    lblCinnostNextBarcode.Text = rowCinnostNext.IsBarcodeNull() ? string.Empty : rowCinnostNext.Barcode.Trim();
                    lblCinnostNextType.Text = rowCinnostNext.TYPE.Trim();
                    lblCinnostNextTypeValue.Text = rowCinnostNext.IsTYPEVALUENull() ? string.Empty : rowCinnostNext.TYPEVALUE.Trim();
                    lblCinnostNextMandatory.Text = rowCinnostNext.Mandatory > 0 ? "Ano" : "Ne";
                    lblCinnostNextRequiredLength.Text = rowCinnostNext.IsRequiredLengthNull() ? string.Empty : rowCinnostNext.RequiredLength.ToString();
                    Fask.Interfaces.DataSets.Servis.CZMST_Servis_CinnostNextRow cinnostnext = null;
                    if (rowCinnost != null && !rowCinnost.IsIDNull() && !rowCinnostNext.IsIDNull())
                        cinnostnext = ((Fask.Interfaces.Servis.IServis)providerServis).GetCinnostNextByID(rowCinnost.ID, rowCinnostNext.ID);

                    lblCinnostNextIDValue.Text = (cinnostnext != null && !cinnostnext.IsIDValueNull()) ? cinnostnext.IDValue.Trim() : string.Empty;
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
                this.dsServisCinnostNext.CZMST_Servis_Stav.Clear();
                this.dsServisCinnostNext.CZMST_Servis_Stav.AcceptChanges();

                // nic není zvoleno
                if (rowCinnost == null)
                {
                    FillLabels();
                    return;
                }

                // naplneni stavnext
                this.dsServisCinnostNext = ((Fask.Interfaces.Servis.IServis)providerServis).GetCinnostiNextByCinnostID(rowCinnost.ID);
                bsCinnostNext.DataSource = this.dsServisCinnostNext;

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

                if (rowCinnost == null)
                {
                    MessageBox.Show("Není vybrána činnost pro přiřazení", this.Text, MessageBoxButtons.OK);
                    return;
                }

                Fask.Interfaces.DataSets.Servis dsSelectedRows = new Fask.Interfaces.DataSets.Servis();
                using (Servis.FormCinnostiSelect frmcinnosti = new FormCinnostiSelect(true))
                {
                    frmcinnosti.Text = "Přiřadit činnost";
                    //frmstavy.rowsSelected = rowStav;
                    if (frmcinnosti.ShowDialog(this) != DialogResult.OK)
                        return;

                    // naplneni datasetu vybranymy radky                    
                    foreach (DataGridViewRow dgview in frmcinnosti.SelectedRows)
                    {
                        Fask.Interfaces.DataSets.Servis.CZMST_Servis_CinnostRow item = ((DataRowView)dgview.DataBoundItem).Row as Fask.Interfaces.DataSets.Servis.CZMST_Servis_CinnostRow;
                        dsSelectedRows.CZMST_Servis_Cinnost.ImportRow(item);
                    }
                }

                // TODO: prejmenovat nejak normalne IDValue
                // moznost zmeny cinnosti podle predchoziho vysledku cinnosti
                string idvalue = string.Empty;
                if (Konzola.Forms.InputBox.Show(this.Text, "Zadejte IDValue", string.Empty, true, out idvalue) != System.Windows.Forms.DialogResult.OK)
                    return;

                ((Fask.Interfaces.Servis.IServis)providerServis).InsertCinnostNext(rowCinnost.ID, idvalue.Trim(), dsSelectedRows);
                UpdateStavNextForm(rowCinnost, dsSelectedRows.CZMST_Servis_Cinnost.Count > 0 ? dsSelectedRows.CZMST_Servis_Cinnost.First() : null);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonOdstranitVazbu_Click(object sender, EventArgs e)
        {
            PerformOdstranitVazbu();
        }

        private void PerformOdstranitVazbu()
        {
            try
            {                
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (rowCinnost == null)
                {
                    MessageBox.Show("Není vybrán záznam činnosti", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if (rowCinnostNext == null)
                {
                    MessageBox.Show("Není vybrán záznam pro odstranění", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if (MessageBox.Show("Opravdu chcete odstranit přiřazenou činnost '" + rowCinnostNext.Oznaceni.Trim() + "'?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

                ((Fask.Interfaces.Servis.IServis)providerServis).DeleteCinnostNext(rowCinnost.ID, rowCinnostNext.IsIDNull() ? null : rowCinnostNext.ID);
                UpdateStavNextForm(rowCinnost, rowCinnostNext);
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
                int pos = this.bsCinnost.Find(dsServisCinnost.CZMST_Servis_Stav.IDColumn.ColumnName, selectedStavID);
                this.bsCinnost.Position = pos;

                int pos2 = this.bsCinnostNext.Find(dsServisCinnostNext.CZMST_Servis_Stav.IDColumn.ColumnName, selectedStavNextID);
                this.bsCinnostNext.Position = pos2;
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
                    if (rowCinnost != null)
                        selectedStavID = rowCinnost.ID;

                    if (rowCinnostNext != null)
                        selectedStavNextID = rowCinnostNext.ID;
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
                int pos2 = this.bsCinnostNext.Find("ID", selectedStavNextID);
                this.bsCinnostNext.Position = pos2;
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
                    if (rowCinnostNext != null)
                        selectedStavNextID = rowCinnostNext.ID;
                }
            }
            catch
            {
            }
        }

        private void buttonPriraditVazbu_Click(object sender, EventArgs e)
        {
            PerformPriraditVazbu();
        }

        private void advancedDataGridViewSearchToolBar_Cinnost_Search(object sender, Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs e)
        {
            bool restartsearch = true;
            int startColumn = 0;
            int startRow = 0;
            if (!e.FromBegin)
            {
                bool endcol = dgCinnost.CurrentCell.ColumnIndex + 1 >= dgCinnost.ColumnCount;
                bool endrow = dgCinnost.CurrentCell.RowIndex + 1 >= dgCinnost.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dgCinnost.CurrentCell.ColumnIndex;
                    startRow = dgCinnost.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dgCinnost.CurrentCell.ColumnIndex + 1;
                    startRow = dgCinnost.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dgCinnost.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dgCinnost.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dgCinnost.CurrentCell = c;


        }

        private void advancedDataGridViewSearchToolBar_CinnostNext_Search(object sender, Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs e)
        {
            bool restartsearch = true;
            int startColumn = 0;
            int startRow = 0;
            if (!e.FromBegin)
            {
                bool endcol = dgCinnostNext.CurrentCell.ColumnIndex + 1 >= dgCinnostNext.ColumnCount;
                bool endrow = dgCinnostNext.CurrentCell.RowIndex + 1 >= dgCinnostNext.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dgCinnostNext.CurrentCell.ColumnIndex;
                    startRow = dgCinnostNext.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dgCinnostNext.CurrentCell.ColumnIndex + 1;
                    startRow = dgCinnostNext.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dgCinnostNext.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dgCinnostNext.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dgCinnostNext.CurrentCell = c;


        }
    }
}
