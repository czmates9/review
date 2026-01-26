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
    public partial class FormVazbyDynTabDefDynTabList : Form
    {
        private Fask.Interfaces.IMES providerServis = null;

        /// <summary>
        /// Zvolené id Stav před seřazením
        /// </summary>
        string selectedDynTabDefID = string.Empty;
        /// <summary>
        /// Zvolené id StavNext před seřazením
        /// </summary>
        string selectedDynTabID = string.Empty;
        /// <summary>
        /// Vybraná definice tabulky
        /// </summary>
        public Fask.Interfaces.DataSets.Servis.CZMST_Servis_Dynamic_Table_DefinitionRow rowDynTabDef
        {
            get
            {
                try
                {
                    return ((DataRowView)(dgStav.BindingContext[bsStav].Current)).Row as Fask.Interfaces.DataSets.Servis.CZMST_Servis_Dynamic_Table_DefinitionRow;
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Vybraný záznam dynamické tabulky
        /// </summary>
        public Fask.Interfaces.DataSets.Servis.CZMST_Servis_Dynamic_TableRow rowDynTab
        {
            get
            {
                try
                {
                    return ((DataRowView)(dgStavNext.BindingContext[bsStavNext].Current)).Row as Fask.Interfaces.DataSets.Servis.CZMST_Servis_Dynamic_TableRow;

                }
                catch
                {
                    return null;
                }
            }
        }

        public FormVazbyDynTabDefDynTabList()
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

        private void FormVazbyDynTabDefDynTabList_Load(object sender, EventArgs e)
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
        
        private void FormVazbyDynTabDefDynTabList_KeyDown(object sender, KeyEventArgs e)
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

        private void FormVazbyDynTabDefDynTabList_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.dgStav.SaveConfiguration(this.GetType().ToString());
                this.dgStavNext.SaveConfiguration(this.GetType().ToString() + "next");

                panelButtons.SaveConfiguration(this.GetType().ToString());


                Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis_Ostatni[0].FormVazbyDynTabDefDynTabListSplitterDistance = this.splitContainer1.SplitterDistance;
                Konfigurace.Globals_Konfig_Konzola.SaveConfiguration();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormVazbyDynTabDefDynTabList_Shown(object sender, EventArgs e)
        {
            try
            {
                this.splitContainer1.SplitterDistance = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis_Ostatni[0].FormVazbyDynTabDefDynTabListSplitterDistance;
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

                ds = ((Fask.Interfaces.Servis.IServis)providerServis).GetDynamicTableDefinition();

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
        private void UpdateDynTabDefForm(Fask.Interfaces.DataSets.Servis.CZMST_Servis_Dynamic_Table_DefinitionRow rowdynTabDef, Fask.Interfaces.DataSets.Servis.CZMST_Servis_Dynamic_TableRow rowdynTab)
        {
            try
            {
                string dynTabDefID = rowdynTabDef != null ? rowdynTabDef.FullName : string.Empty;
                string dynTabID = rowdynTab != null ? rowdynTab.ID : string.Empty;

                // aktualizace Stav
                dsStav.Clear();
                dsStav = ((Fask.Interfaces.Servis.IServis)providerServis).GetDynamicTableDefinition();
                bsStav.DataSource = dsStav;

                int index = -1;
                try
                {
                    if (!string.IsNullOrEmpty(dynTabDefID))
                    {
                        index = bsStav.Find(dsStavNext.CZMST_Servis_Dynamic_Table_Definition.FullNameColumn.ColumnName, dynTabDefID);
                        //this.bsDynTabDef.Position = index;
                    }
                    this.bsStav.Position = index;
                }
                catch
                {
                }

                // aktualizace StavNext
                dsStavNext.Clear();
                if (index != -1 && !string.IsNullOrEmpty(dynTabDefID))
                    dsStavNext = ((Fask.Interfaces.Servis.IServis)providerServis).GetDynamicTable(dynTabDefID);
                else if (rowDynTabDef != null)
                    dsStavNext = ((Fask.Interfaces.Servis.IServis)providerServis).GetDynamicTable(rowDynTabDef.FullName);
                else
                    dsStavNext.CZMST_Servis_Dynamic_Table.Clear();

                bsStavNext.DataSource = dsStavNext;

                try
                {
                    if (!string.IsNullOrEmpty(dynTabID))
                    {
                        index = bsStavNext.Find(dsStavNext.CZMST_Servis_Dynamic_Table.IDColumn.ColumnName, dynTabID);
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
        private void UpdateDynTabForm(Fask.Interfaces.DataSets.Servis.CZMST_Servis_Dynamic_Table_DefinitionRow rowdynTabDef, Fask.Interfaces.DataSets.Servis.CZMST_Servis_Dynamic_TableRow rowdynTab)
        {
            try
            {
                string dynTabDefID = rowdynTabDef != null ? rowdynTabDef.FullName : string.Empty;
                string dynTabID = rowdynTab != null ? rowdynTab.ID : string.Empty;

                dsStavNext.Clear();

                if (!string.IsNullOrEmpty(dynTabDefID))
                    dsStavNext = ((Fask.Interfaces.Servis.IServis)providerServis).GetDynamicTable(dynTabDefID);

                bsStavNext.DataSource = dsStavNext;

                try
                {
                    if (!string.IsNullOrEmpty(dynTabID))
                    {
                        int index = bsStavNext.Find(dsStavNext.CZMST_Servis_Dynamic_Table.IDColumn.ColumnName, dynTabID);
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
                if (rowDynTabDef == null)
                {
                    lblDynTabDefFullName.Text = string.Empty;
                    lblDynTabDefTypeName.Text = string.Empty;
                }
                else
                {
                    // něco je zvoleno
                    lblDynTabDefFullName.Text = rowDynTabDef.FullName.Trim();
                    lblDynTabDefTypeName.Text = rowDynTabDef.TypeName.Trim();
                }

                // StavNext
                if (rowDynTab == null)
                {
                    lblStavNextID.Text = string.Empty;
                    lblStavNextOznaceni.Text = string.Empty;
                    lblStavNextBarcode.Text = string.Empty;
                }
                else
                {
                    // něco je zvoleno
                    lblStavNextID.Text = rowDynTab.ID.Trim();
                    lblStavNextOznaceni.Text = rowDynTab.Oznaceni.Trim();
                    lblStavNextBarcode.Text = rowDynTab.IsBarcodeNull() ? string.Empty : rowDynTab.Barcode.Trim();
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
                this.dsStavNext.CZMST_Servis_Dynamic_Table.Clear();
                this.dsStavNext.CZMST_Servis_Dynamic_Table.AcceptChanges();

                // nic není zvoleno
                if (rowDynTabDef == null)
                {
                    FillLabels();
                    return;
                }

                // naplneni stavnext
                this.dsStavNext = ((Fask.Interfaces.Servis.IServis)providerServis).GetDynamicTable(rowDynTabDef.FullName);
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

        //private void PerformPriraditVazbu()
        //{
        //    try
        //    {
        //        if (!MySystem.LoginTest.UserLoginTest())
        //            return;

        //        if (rowStav == null)
        //        {
        //            MessageBox.Show("Není vybrán stav pro přiřazení", this.Text, MessageBoxButtons.OK);
        //            return;
        //        }

        //        Fask.Interfaces.DataSets.Servis dsSelectedRows = new Fask.Interfaces.DataSets.Servis();
        //        using (Servis.FormStavySelect frmstavy = new FormStavySelect(true))
        //        {
        //            frmstavy.Text = "Přiřadit stav";
        //            //frmstavy.rowsSelected = rowStav;
        //            if (frmstavy.ShowDialog(this) != DialogResult.OK)
        //                return;

        //            // naplneni datasetu vybranymy radky
                    
        //            foreach (DataGridViewRow dgview in frmstavy.SelectedRows)
        //            {
        //                Fask.Interfaces.DataSets.Servis.CZMST_Servis_StavRow item = ((DataRowView)dgview.DataBoundItem).Row as Fask.Interfaces.DataSets.Servis.CZMST_Servis_StavRow;
        //                dsSelectedRows.CZMST_Servis_Stav.ImportRow(item);
        //            }
        //        }
        //        providerServis.InsertStavNext(rowStav.ID, dsSelectedRows);
        //        UpdateStavNextForm(rowStav, dsSelectedRows.CZMST_Servis_Stav.Count > 0 ? dsSelectedRows.CZMST_Servis_Stav.First() : null);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Write(ex);
        //        MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        private void PerformOdstranitDefiniciTabulky()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (rowDynTabDef == null)
                {
                    MessageBox.Show("Není vybrána definice tabulky", this.Text, MessageBoxButtons.OK);
                    return;
                }

                // data nemusi existovat ...
                //if (rowDynTab == null)
                //{
                //    MessageBox.Show("Není vybrán záznam pro odstranění", this.Text, MessageBoxButtons.OK);
                //    return;
                //}

                if (MessageBox.Show("Opravdu chcete odstranit dynamickou tabulku '" + rowDynTabDef.TypeName.Trim() + "'?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

                ((Fask.Interfaces.Servis.IServis)providerServis).DeleteDynamicTableDefinitionByID(rowDynTabDef.FullName, rowDynTabDef.TypeName);

                //UpdateDynTabDefForm(rowDynTabDef, rowDynTab);
                UpdateDynTabDefForm(null, null);
                //UpdateStavNextForm(rowDynTabDef, rowDynTab);  // pouzit pri mazani zaznamu z prave tabulky !!
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
                int pos = this.bsStav.Find(dsStav.CZMST_Servis_Dynamic_Table_Definition.FullNameColumn.ColumnName, selectedDynTabDefID);
                this.bsStav.Position = pos;

                int pos2 = this.bsStavNext.Find(dsStavNext.CZMST_Servis_Dynamic_Table.IDColumn.ColumnName, selectedDynTabID);
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
                    if (rowDynTabDef != null)
                        selectedDynTabDefID = rowDynTabDef.FullName;

                    if (rowDynTab != null)
                        selectedDynTabID = rowDynTab.ID;
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
                int pos2 = this.bsStavNext.Find("ID", selectedDynTabID);
                this.bsStavNext.Position = pos2;
            }
            catch
            {
            }
        }

        private void dataGridView2_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1)
                {
                    if (rowDynTab != null)
                        selectedDynTabID = rowDynTab.ID;
                }
            }
            catch
            {
            }
        }

        private void PerformVytvoritDefinicyTabulky()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                using (Servis.FormDynTabDefEdit frmdyntab = new FormDynTabDefEdit())
                {
                    frmdyntab.Text = "Nová dynamická tabulka";
                    if (frmdyntab.ShowDialog(this) != DialogResult.OK)
                        return;

                    UpdateDynTabDefForm(frmdyntab.returnrow, null);
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void PerformPridatDataTabulky()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (rowDynTabDef == null)
                {
                    MessageBox.Show("Není vybrána definice tabulky", this.Text, MessageBoxButtons.OK);
                    return;
                }

                using (Servis.FormDynTabEdit frmdyntab = new FormDynTabEdit(rowDynTabDef))
                {
                    frmdyntab.Text = "Nový záznam pro '" + rowDynTabDef.TypeName.Trim() + "'";
                    if (frmdyntab.ShowDialog(this) != DialogResult.OK)
                        return;

                    //UpdateDynTabDefForm(frmdyntab.returnrow, null);
                    UpdateDynTabForm(rowDynTabDef, frmdyntab.returnrow);
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void PerformUpravitDataTabulky()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (rowDynTabDef == null)
                {
                    MessageBox.Show("Není vybrána definice tabulky", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if (rowDynTab == null)
                {
                    MessageBox.Show("Není vybrán záznam pro odstranění", this.Text, MessageBoxButtons.OK);
                    return;
                }

                using (Servis.FormDynTabEdit frmdyntab = new FormDynTabEdit(rowDynTabDef))
                {
                    frmdyntab.Text = "Upravit záznam pro '" + rowDynTabDef.TypeName.Trim() + "'";
                    frmdyntab.dyntabrow = rowDynTab;
                    if (frmdyntab.ShowDialog(this) != DialogResult.OK)
                        return;

                    //UpdateDynTabDefForm(frmdyntab.returnrow, null);
                    UpdateDynTabForm(rowDynTabDef, frmdyntab.returnrow);
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void PerformOdstranitDataTabulky()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (rowDynTabDef == null)
                {
                    MessageBox.Show("Není vybrána definice tabulky", this.Text, MessageBoxButtons.OK);
                    return;
                }

                // data nemusi existovat ...
                if (rowDynTab == null)
                {
                    MessageBox.Show("Není vybrán záznam pro odstranění", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if (MessageBox.Show("Opravdu chcete odstranit záznam '" + rowDynTab.Oznaceni.Trim() + "'?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

                ((Fask.Interfaces.Servis.IServis)providerServis).DeleteDynamicTableByID(rowDynTabDef, rowDynTab.ID);

                //UpdateDynTabDefForm(rowDynTabDef, rowDynTab);
                UpdateDynTabForm(rowDynTabDef, rowDynTab);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void tsmiAktualizovat_Click(object sender, EventArgs e)
        {
            UpdateDynTabDefForm(rowDynTabDef, rowDynTab);
        }

        private void tsmiKonec_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void tsmiPridatDataTabulky_Click(object sender, EventArgs e)
        {
            PerformPridatDataTabulky();
        }

        private void tsmiUpravitDataTabulky_Click(object sender, EventArgs e)
        {
            PerformUpravitDataTabulky();
        }

        private void tsmiOdstranitDataTabulky_Click(object sender, EventArgs e)
        {
            PerformOdstranitDataTabulky();
        }

        private void tsmiPridatDefiniciTabulky_Click(object sender, EventArgs e)
        {
            PerformVytvoritDefinicyTabulky();
        }

        private void tsmiOdstranitVazbu_Click(object sender, EventArgs e)
        {
            PerformOdstranitDefiniciTabulky();  
        }

        private void advancedDataGridViewSearchToolBar_StavNext_Search(object sender, Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs e)
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

        private void advancedDataGridViewSearchToolBar_Stav_Search(object sender, Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs e)
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
