using Fask.Interfaces.DataSets;
using Konzola.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Konzola.SkladLokace
{
    public partial class FormSKladLokace_PorovnaniVuciIS : Form
    {
        #region Parametry

        private Fask.Interfaces.IMES provider = null;

        /// <summary>
        /// Seznam vsech nactenych filtru.
        /// </summary>
        private List<Fask.Interfaces.Filtry.SkladLokaceCompareToISFiltr> filtry = new List<Fask.Interfaces.Filtry.SkladLokaceCompareToISFiltr>();

        /// <summary>
        /// Vybrany filtr.
        /// </summary>
        private Fask.Interfaces.Filtry.SkladLokaceCompareToISFiltr rowFiltr
        {
            get
            {
                try
                {
                    return tscbFiltry.SelectedItem as Fask.Interfaces.Filtry.SkladLokaceCompareToISFiltr;
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Vybrany radek FASK.
        /// </summary>
        public Fask.Interfaces.DataSets.SkladLokace_CompareToIS.LokMech_StavRow SelectedRow_FASK
        {
            get
            {
                try
                {
                    return ((DataRowView)(dg_FASK.BindingContext[bs_FASK].Current)).Row as Fask.Interfaces.DataSets.SkladLokace_CompareToIS.LokMech_StavRow;
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Vybrany radek IS.
        /// </summary>
        public Fask.Interfaces.DataSets.SkladLokace_CompareToIS.SKz_StavRow SelectedRow_IS
        {
            get
            {
                try
                {
                    return ((DataRowView)(dg_IS.BindingContext[bs_IS].Current)).Row as Fask.Interfaces.DataSets.SkladLokace_CompareToIS.SKz_StavRow;
                }
                catch
                {
                    return null;
                }
            }
        }

        #endregion

        #region Eventy fromu

        public FormSKladLokace_PorovnaniVuciIS()
        {
            InitializeComponent();

            #if DEBUG
                        tb_SKL_ID.Text = "51";
            #endif

            this.WindowState = FormWindowState.Maximized;

            panelButtons.Menu = menuStrip1;
            this.dg_FASK.UpdateColumnHeaderCellsByDatasource();
            this.dg_IS.UpdateColumnHeaderCellsByDatasource();
        }

        private void FormSKladLokace_PorovnaniVuciIS_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;

                panelButtons.LoadConfiguration(this.GetType().ToString());
                panelButtons.Init();
                panelButtons.Size = new Size(85, 700);

                this.dg_FASK.LoadConfiguration("FASK_" + this.GetType().ToString());
                this.dg_IS.LoadConfiguration("IS_" + this.GetType().ToString());

                dg_SearchToolBar_IS.SetColumns(dg_IS.Columns);
                dg_SearchToolBar_FASK.SetColumns(dg_FASK.Columns);

                //// načtení konfigurace vytvořených filtrů
                this.filtry = Extensions.ListExt.ReadFromXML<Fask.Interfaces.Filtry.SkladLokaceCompareToISFiltr>(this.GetType().ToString() + ".filtr");
                tscbFiltry.ComboBox.DataSource = this.filtry;
                tscbFiltry.SelectedItem = null;
                tscbFiltry.ComboBox.DropDownWitdhAutosize();

                // inicializace providera
                InitProvider();

                if (provider == null)
                    throw new Exception("Provider 'SkladLokace' není inicializován");

                SetStratusLabelText_IS(-1);
                SetStratusLabelText_FASK(-1);

                buttonVyhledat.Focus();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void FormSKladLokace_PorovnaniVuciIS_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Shift && !e.Control && !e.Alt)
            {
                if (e.KeyCode == Keys.Escape)
                {
                    PerformCancel();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    PerformVyhledat();
                }
                else
                    return;
            }
            else
                return;

            e.Handled = true;
        }

        private void FormSKladLokace_PorovnaniVuciIS_Shown(object sender, EventArgs e)
        {
            try
            {
                this.progressIndicator_FASK.Size = new Size(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ostatni[0].ProgressIndicatorSize, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ostatni[0].ProgressIndicatorSize);
                this.progressIndicator_FASK.Location = new Point(this.dg_FASK.Location.X + (this.dg_FASK.Width / 2) - (progressIndicator_FASK.Size.Width / 2), this.dg_FASK.Location.Y + (this.dg_FASK.Height / 2) - (progressIndicator_FASK.Size.Height / 2));

                this.progressIndicator_IS.Size = new Size(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ostatni[0].ProgressIndicatorSize, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ostatni[0].ProgressIndicatorSize);
                this.progressIndicator_IS.Location = new Point(this.dg_IS.Location.X + (this.dg_IS.Width / 2) - (progressIndicator_FASK.Size.Width / 2), this.dg_IS.Location.Y + (this.dg_IS.Height / 2) - (progressIndicator_FASK.Size.Height / 2));


            }
            catch { }
        }


        private void FormSKladLokace_PorovnaniVuciIS_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                panelButtons.SaveConfiguration(this.GetType().ToString());

                this.dg_FASK.SaveConfiguration("FASK_" + this.GetType().ToString());
                this.dg_IS.SaveConfiguration("IS_" + this.GetType().ToString());

                //// ulozeni vsech filtru
                //this.filtry.WriteXML(this.GetType().ToString() + ".filtr");

                // cekani na dobehnuti vlakna
                try
                {
                    if (bw_LoadData.IsBusy)
                    {
                        bw_LoadData.CancelAsync();
                        while (bw_LoadData.IsBusy)
                        {
                            Application.DoEvents();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #endregion

        #region Inicalizace Provideru

        /// <summary>
        /// Inicializace providera
        /// </summary>
        private void InitProvider()
        {
            try
            {
                if (String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                    return;

                if (provider == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.SkladLokace.ISkladLokace2).IsAssignableFrom(t))
                            {
                                provider = (Fask.Interfaces.SkladLokace.ISkladLokace2)providerAssemlby.CreateInstance(t.FullName);
                                if (provider != null)
                                    break;
                            }
                        }
                        catch { }
                    }

                }

                provider.InitProvider();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        #endregion

        #region  Button/Menu Event

        private void tsmi_Konec_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void buttonVyhledat_Click(object sender, EventArgs e)
        {
            dataGridWork();

            PerformVyhledat();
        }

        private void dataGridWork()
        {
            #region prace s datagridem - nastaveni numeric hodnot na pocet desetinnych mist
            try
            {
                // Procházení sloupců v DataGridView
                foreach (DataGridViewColumn column in dg_FASK.Columns)
                {
                    // Získání typu dat pro sloupec
                    Type dataType = null;

                    if (dg_FASK.DataSource is DataTable dataTable)
                    {
                        // Pokud je datový zdroj DataTable
                        if (dataTable.Columns.Contains(column.DataPropertyName))
                        {
                            dataType = dataTable.Columns[column.DataPropertyName].DataType;
                        }
                    }
                    else if (dg_FASK.DataSource is BindingSource bindingSource)
                    {
                        // Pokud je datový zdroj BindingSource
                        if (bindingSource.DataSource is DataTable dataSourceTable)
                        {
                            if (dataSourceTable.Columns.Contains(column.DataPropertyName))
                            {
                                dataType = dataSourceTable.Columns[column.DataPropertyName].DataType;
                            }
                        }
                        else if (bindingSource.DataSource is DataSet dataSet)
                        {
                            // Pokud je datový zdroj DataSet
                            DataTable dataTable2 = dataSet.Tables[bindingSource.DataMember];
                            if (dataTable2.Columns.Contains(column.DataPropertyName))
                            {
                                dataType = dataTable2.Columns[column.DataPropertyName].DataType;
                            }
                        }
                    }

                    if (dataType.Name == "Int32" || dataType.Name == "Decimal")
                    {
                        //zarovnani cisel doprava na stred
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }

                    //nastaveni poctu desetinnych mist pokud je sloupec typu Decimal
                    if (dataType.Name == "Decimal")
                    {
                        //column.DefaultCellStyle.Format = "N2";

                        if (!string.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Rozbory[0].PocetDesetinnychMist))
                        {
                            column.DefaultCellStyle.Format = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Rozbory[0].PocetDesetinnychMist;


                        }
                        else
                        {
                            column.DefaultCellStyle.Format = "N5";
                        }
                    }

                }
            }
            catch (Exception ex)
            {

                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show("Chyba při nastavení desetinnych míst.");
            }
            #endregion

            #region prace s datagridem - nastaveni numeric hodnot na pocet desetinnych mist
            try
            {
                // Procházení sloupců v DataGridView
                foreach (DataGridViewColumn column in dg_IS.Columns)
                {
                    // Získání typu dat pro sloupec
                    Type dataType = null;

                    if (dg_IS.DataSource is DataTable dataTable)
                    {
                        // Pokud je datový zdroj DataTable
                        if (dataTable.Columns.Contains(column.DataPropertyName))
                        {
                            dataType = dataTable.Columns[column.DataPropertyName].DataType;
                        }
                    }
                    else if (dg_IS.DataSource is BindingSource bindingSource)
                    {
                        // Pokud je datový zdroj BindingSource
                        if (bindingSource.DataSource is DataTable dataSourceTable)
                        {
                            if (dataSourceTable.Columns.Contains(column.DataPropertyName))
                            {
                                dataType = dataSourceTable.Columns[column.DataPropertyName].DataType;
                            }
                        }
                        else if (bindingSource.DataSource is DataSet dataSet)
                        {
                            // Pokud je datový zdroj DataSet
                            DataTable dataTable2 = dataSet.Tables[bindingSource.DataMember];
                            if (dataTable2.Columns.Contains(column.DataPropertyName))
                            {
                                dataType = dataTable2.Columns[column.DataPropertyName].DataType;
                            }
                        }
                    }

                    if (dataType.Name == "Int32" || dataType.Name == "Decimal")
                    {
                        //zarovnani cisel doprava na stred
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }

                    //nastaveni poctu desetinnych mist pokud je sloupec typu Decimal
                    if (dataType.Name == "Decimal")
                    {
                        //column.DefaultCellStyle.Format = "N2";

                        if (!string.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Rozbory[0].PocetDesetinnychMist))
                        {
                            column.DefaultCellStyle.Format = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Rozbory[0].PocetDesetinnychMist;


                        }
                        else
                        {
                            column.DefaultCellStyle.Format = "N5";
                        }
                    }

                }
            }
            catch (Exception ex)
            {

                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show("Chyba při nastavení desetinnych míst.");
            }
            #endregion

        }

        private void tsmi_LokMechStav_Click(object sender, EventArgs e)
        {
            PerformLokacniMechanizmusStav();
        }

        private void tsmi_IS_Pohyby_Click(object sender, EventArgs e)
        {
            PerformPohybyIS();
        }

        private void tsmi_poloAutomat_Click(object sender, EventArgs e)
        {
            PoloAutomat_Pohoda_TO_LokMech();
        }

        #endregion

        #region Perfom metody

        private void PerformCancel()
        {
            try
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Vyhledani podle zadaneho filtru.
        /// </summary>
        private void PerformVyhledat()
        {
            try
            {


                if(string.IsNullOrEmpty(tb_SKL_ID.Text))
                {
                    MessageBox.Show(this,"ID Skladu je povinný parametr pro filtr!", "Zapomětlivko!", MessageBoxButtons.OK,MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1 );
                    return;
                }



                if (bw_LoadData.IsBusy)
                {
                    bw_LoadData.CancelAsync();
                    while (bw_LoadData.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }

                ProgressIndicatorStart();

                Fask.Interfaces.Filtry.SkladLokaceCompareToISFiltr filtr = new Fask.Interfaces.Filtry.SkladLokaceCompareToISFiltr();
                if (!CreateFilter(ref filtr))
                    return;

                int FirstDisplayedScrollingRowIndex_IS = this.dg_IS.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index
                int FirstDisplayedScrollingRowIndex_FASK = this.dg_FASK.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

                bw_LoadData.RunWorkerAsync(filtr);

                if ((FirstDisplayedScrollingRowIndex_FASK >= 0) && ((this.dg_FASK.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex_FASK)) this.dg_FASK.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex_FASK; //Restore Scroll Index

                if ((FirstDisplayedScrollingRowIndex_IS >= 0) && ((this.dg_IS.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex_IS)) this.dg_IS.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex_IS; //Restore Scroll Index
            }
            catch (Exception ex)
            {
                ProgressIndicatorStop();
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PerformLokacniMechanizmusStav()
        {
            try
            {
                if (SelectedRow_FASK == null)
                {
                    MessageBox.Show("Není vybrán záznam", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if (dg_FASK.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Je vybráno víc jak 1 záznam", this.Text, MessageBoxButtons.OK);
                    return;
                }

                using (FormSkladLokaceStavList frm = new FormSkladLokaceStavList(true,Fask.Interfaces.Classes.ZOBRAZENI_TYP.POHLED))
                {
                    frm.ITEMNMBR = SelectedRow_FASK.ITEMNMBR;

                    frm.ShowDialog();
                }


            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PerformPohybyIS()
        {
            try
            {
                if (SelectedRow_IS == null)
                {
                    MessageBox.Show("Není vybrán záznam", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if (dg_IS.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Je vybráno víc jak 1 záznam", this.Text, MessageBoxButtons.OK);
                    return;
                }

                using (FormPohybyIS_List frm = new FormPohybyIS_List(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.POHLED))
                {
                    frm.ITEMNMBR = SelectedRow_IS.ITEMNMBR;

                    frm.ShowDialog();
                }


            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PoloAutomat_Pohoda_TO_LokMech()
        {
            try
            {
                if (SelectedRow_IS == null || dg_IS.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Není vybrán záznam", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if (bw_PoloAutomat.IsBusy)
                {
                    bw_PoloAutomat.CancelAsync();

                    while (bw_PoloAutomat.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }

                ProgressIndicatorStart();

                bw_PoloAutomat.RunWorkerAsync();

            }
            catch (Exception ex)
            {
                throw ex; 
            }
        }

        #endregion

        #region DataGrid eventy

        private void dg_SearchToolBar_IS_Search(object sender, Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs e)
        {
            bool restartsearch = true;
            int startColumn = 0;
            int startRow = 0;
            if (!e.FromBegin)
            {
                bool endcol = dg_IS.CurrentCell.ColumnIndex + 1 >= dg_IS.ColumnCount;
                bool endrow = dg_IS.CurrentCell.RowIndex + 1 >= dg_IS.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dg_IS.CurrentCell.ColumnIndex;
                    startRow = dg_IS.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dg_IS.CurrentCell.ColumnIndex + 1;
                    startRow = dg_IS.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dg_IS.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dg_IS.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dg_IS.CurrentCell = c;
        }

        private void dg_SearchToolBar_FASK_Search(object sender, Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs e)
        {
            bool restartsearch = true;
            int startColumn = 0;
            int startRow = 0;
            if (!e.FromBegin)
            {
                bool endcol = dg_FASK.CurrentCell.ColumnIndex + 1 >= dg_FASK.ColumnCount;
                bool endrow = dg_FASK.CurrentCell.RowIndex + 1 >= dg_FASK.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dg_FASK.CurrentCell.ColumnIndex;
                    startRow = dg_FASK.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dg_FASK.CurrentCell.ColumnIndex + 1;
                    startRow = dg_FASK.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dg_FASK.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dg_FASK.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dg_FASK.CurrentCell = c;
        }


        private void dg_IS_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewRow row in dg_IS.Rows)
            {

                Fask.Interfaces.DataSets.SkladLokace_CompareToIS.SKz_StavRow radek = (Fask.Interfaces.DataSets.SkladLokace_CompareToIS.SKz_StavRow)((DataRowView)row.DataBoundItem).Row;
                GetColorToRow(row, radek.IsstatusNull() ? (int?)null : radek.status);
            }
        }

        private void dg_FASK_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewRow row in dg_FASK.Rows)
            {

                Fask.Interfaces.DataSets.SkladLokace_CompareToIS.LokMech_StavRow radek = (Fask.Interfaces.DataSets.SkladLokace_CompareToIS.LokMech_StavRow)((DataRowView)row.DataBoundItem).Row;
                GetColorToRow(row, radek.IsstatusNull() ? (int?)null : radek.status);
            }
        }

        private static void GetColorToRow(DataGridViewRow row, int? status)
        {
            if (!status.HasValue)
            {
                row.DefaultCellStyle.BackColor = Color.Empty;
            }
            else if (status == 0)
            {
                row.DefaultCellStyle.BackColor = Color.Orange;
            }
            else if (status == 1)
            {
                row.DefaultCellStyle.BackColor = Color.Tomato;
            }
            else if (status == 2)
            {
                row.DefaultCellStyle.BackColor = Color.Yellow;
            }
            else if (status == 3)
            {
                row.DefaultCellStyle.BackColor = Color.Lime;
            }
            else if (status == 4)
            {
                row.DefaultCellStyle.BackColor = Color.DeepPink;
            }
            else if (status == 5)
            {
                row.DefaultCellStyle.BackColor = Color.SaddleBrown;
            }
            else if (status == 6)
            {
                row.DefaultCellStyle.BackColor = Color.Cyan;
            }
        }

        private void dg_IS_SelectionChanged(object sender, EventArgs e)
        {
            if (dg_IS.Focused)
            {
                try
                {
                    if ((dg_IS.SelectedRows.Count > 1) || (dg_IS.SelectedRows.Count == 0))
                    {
                        dg_FASK.ClearSelection();
                        return;
                    }

                    dg_FASK.ClearSelection();

                    foreach (DataGridViewRow row in dg_IS.SelectedRows)
                    {
                        var radek = (Fask.Interfaces.DataSets.SkladLokace_CompareToIS.SKz_StavRow)((DataRowView)row.DataBoundItem).Row;

                        foreach (DataGridViewRow R in dg_FASK.Rows)
                        {
                            var radek1 = (Fask.Interfaces.DataSets.SkladLokace_CompareToIS.LokMech_StavRow)((DataRowView)R.DataBoundItem).Row;

                            if (radek1.ITEMNMBR == radek.ITEMNMBR & radek1.SERLTNUM == radek.SERLTNUM)
                            {
                                R.Selected = true;

                                dg_FASK.FirstDisplayedScrollingRowIndex = R.Index;
                                SetStratusLabelText_FASK(R.Index);
                                SetStratusLabelText_IS(row.Index);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                } 
            }
        }

        private void dg_FASK_SelectionChanged(object sender, EventArgs e)
        {
            if (dg_FASK.Focused)
            {
                try
                {
                    if ((dg_FASK.SelectedRows.Count > 1) || (dg_FASK.SelectedRows.Count == 0))
                    {
                        dg_IS.ClearSelection();
                        return;
                    }

                    dg_IS.ClearSelection();

                    foreach (DataGridViewRow row in dg_FASK.SelectedRows)
                    {
                        var radek = (Fask.Interfaces.DataSets.SkladLokace_CompareToIS.LokMech_StavRow)((DataRowView)row.DataBoundItem).Row;

                        foreach (DataGridViewRow R in dg_IS.Rows)
                        {
                            var radek1 = (Fask.Interfaces.DataSets.SkladLokace_CompareToIS.SKz_StavRow)((DataRowView)R.DataBoundItem).Row;

                            if (radek1.ITEMNMBR == radek.ITEMNMBR & radek1.SERLTNUM == radek.SERLTNUM)
                            {
                                R.Selected = true;

                                dg_IS.FirstDisplayedScrollingRowIndex = R.Index;
                                SetStratusLabelText_IS(R.Index);
                                SetStratusLabelText_FASK(row.Index);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                } 
            }
        }

        #endregion

        #region Progress indikator

        private void ProgressIndicatorStop()
        {
            progressIndicator_FASK.Stop();
            progressIndicator_FASK.Visible = false;

            progressIndicator_IS.Stop();
            progressIndicator_IS.Visible = false;
        }

        private void ProgressIndicatorStart()
        {
            try
            {
                // prepocet stredu datagridu
                this.progressIndicator_FASK.Location = new Point(this.dg_FASK.Location.X + (this.dg_FASK.Width / 2) - (progressIndicator_FASK.Size.Width / 2), this.dg_FASK.Location.Y + (this.dg_FASK.Height / 2) - (progressIndicator_FASK.Size.Height / 2));
            }
            catch { }
            progressIndicator_FASK.Start();
            progressIndicator_FASK.Visible = true;


            try
            {
                // prepocet stredu datagridu
                this.progressIndicator_IS.Location = new Point(this.dg_IS.Location.X + (this.dg_IS.Width / 2) - (progressIndicator_IS.Size.Width / 2), this.dg_IS.Location.Y + (this.dg_IS.Height / 2) - (progressIndicator_IS.Size.Height / 2));
            }
            catch { }
            progressIndicator_IS.Start();
            progressIndicator_IS.Visible = true;
        }


        #endregion

        #region Filtry

        /// <summary>
        /// Vytvoreni filtru, ktery se pouzije.
        /// </summary>
        /// <param name="filtr"></param>
        /// <returns></returns>
        private bool CreateFilter(ref Fask.Interfaces.Filtry.SkladLokaceCompareToISFiltr filtr)
        {
            if (filtr == null)
                filtr = new Fask.Interfaces.Filtry.SkladLokaceCompareToISFiltr();

            filtr.SklID = tb_SKL_ID.Text.Trim();

            filtr.ITEMCODE = tb_KodPolozky.Text.Trim();
            filtr.ITEMCODE_L = chb_KodPolozky_L.Checked;
            filtr.ITEMCODE_R = chb_KodPolozky_R.Checked;

            filtr.ITEMDESC = tb_NazevPolozky.Text.Trim();
            filtr.ITEMDESC_L = chb_NazevPolozky_L.Checked;
            filtr.ITEMDESC_R = chb_NazevPolozky_R.Checked;

            filtr.SERLTNUM = tb_SarzeSN.Text.Trim();
            filtr.SERLTNUM_L = chb_SarzeSN_L.Checked;
            filtr.SERLTNUM_L = chb_SarzeSN_L.Checked;

            filtr.V_0 = chb_V_0.Checked;
            filtr.V_1 = chb_V_1.Checked;
            filtr.V_2 = chb_V_2.Checked;
            filtr.V_3 = chb_V_3.Checked;
            filtr.V_4 = chb_V_4.Checked;
            filtr.V_5 = chb_V_5.Checked;
            filtr.V_6 = chb_V_6.Checked;

            return true;
        }

        private void tsbNastavit_Click(object sender, EventArgs e)
        {
            PerformNastavitFiltr(rowFiltr);
        }

        /// <summary>
        /// Nastavi zvoleny filtr do comboboxu, ...
        /// </summary>
        /// <param name="filtr"></param>
        private void PerformNastavitFiltr(Fask.Interfaces.Filtry.SkladLokaceCompareToISFiltr filtr)
        {
            try
            {
                if (filtr == null)
                    return;

                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                PerformVycistitFiltr();

                tb_SKL_ID.Text = filtr.SklID;

                tb_KodPolozky.Text = filtr.ITEMCODE;
                chb_KodPolozky_L.Checked = filtr.ITEMCODE_L;
                chb_KodPolozky_R.Checked = filtr.ITEMCODE_R;

                tb_NazevPolozky.Text = filtr.ITEMDESC;
                chb_NazevPolozky_L.Checked = filtr.ITEMDESC_L;
                chb_NazevPolozky_R.Checked = filtr.ITEMDESC_R;

                tb_SarzeSN.Text = filtr.SERLTNUM;
                chb_SarzeSN_L.Checked = filtr.SERLTNUM_L;
                chb_SarzeSN_L.Checked = filtr.SERLTNUM_L;

                chb_V_0.Checked = filtr.V_0;
                chb_V_1.Checked = filtr.V_1 ;
                chb_V_2.Checked = filtr.V_2 ;
                chb_V_3.Checked = filtr.V_3 ;
                chb_V_4.Checked = filtr.V_4 ;
                chb_V_5.Checked = filtr.V_5 ;
                chb_V_6.Checked = filtr.V_6 ;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show("Nepodařilo se načíst data filtru.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbVycistit_Click(object sender, EventArgs e)
        {
            PerformVycistitFiltr();
        }

        private void PerformVycistitFiltr()
        {
            try
            {
                tb_KodPolozky.Text=
                    tb_NazevPolozky.Text = 
                    tb_SarzeSN.Text = 
                    tb_SKL_ID.Text = string.Empty;

                chb_KodPolozky_L.Checked =
                    chb_KodPolozky_R.Checked =
                    chb_NazevPolozky_L.Checked =
                    chb_NazevPolozky_R.Checked =
                    chb_SarzeSN_L.Checked =
                    chb_SarzeSN_R.Checked = false;

                chb_V_0.Checked
                = chb_V_1.Checked
                = chb_V_2.Checked
                = chb_V_4.Checked
                = chb_V_5.Checked = true;

                chb_V_3.Checked
                = chb_V_6.Checked = false;


            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbZmena_Click(object sender, EventArgs e)
        {
            PerformZmenitFiltr();
        }

        /// <summary>
        /// Upravi zvoleny filtr.
        /// </summary>
        private void PerformZmenitFiltr()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (rowFiltr == null)
                    return;

                DialogResult dr = MessageBox.Show("Opravdu chcete změnit vybraný filtr '" + rowFiltr.NazevFiltru.Trim() + "'?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr != System.Windows.Forms.DialogResult.Yes)
                    return;

                Fask.Interfaces.Filtry.SkladLokaceCompareToISFiltr filtr = rowFiltr;

                if (!CreateFilter(ref filtr))
                    return;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show("Nepodařilo se upravit filtr.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbPridat_Click(object sender, EventArgs e)
        {
            PerformPridatFiltr();
        }

        /// <summary>
        /// Ulozeni filtru do souboru.
        /// </summary>
        private void PerformPridatFiltr()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                Fask.Interfaces.Filtry.SkladLokaceCompareToISFiltr filtr = new Fask.Interfaces.Filtry.SkladLokaceCompareToISFiltr();
                string nazev = string.Empty;
                DialogResult dr = Forms.InputBox.Show("Název filtru", "Zadejte název filtru", string.Empty, false, out nazev);
                if (dr != System.Windows.Forms.DialogResult.OK)
                    return;

                bool result = CreateFilter(ref filtr);
                if (result)
                {
                    filtr.NazevFiltru = nazev;
                    filtry.Add(filtr);
                    //this.tscbFiltry.Items.Clear();
                    this.tscbFiltry.ComboBox.DataSource = null;
                    this.tscbFiltry.ComboBox.DataSource = filtry;
                    this.tscbFiltry.SelectedItem = filtr;
                    tscbFiltry.ComboBox.DropDownWitdhAutosize();
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show("Nepodařilo se uložit data filtru.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbOdebrat_Click(object sender, EventArgs e)
        {
            PerformOdebratFiltr();
        }

        /// <summary>
        /// Odstrani vybrany filtr
        /// </summary>
        private void PerformOdebratFiltr()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (rowFiltr == null)
                    return;

                DialogResult dr = MessageBox.Show("Opravdu chcete odstranit filtr '" + (string.IsNullOrEmpty(rowFiltr.NazevFiltru) ? string.Empty : rowFiltr.NazevFiltru.Trim()) + "'?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr != System.Windows.Forms.DialogResult.Yes)
                    return;

                filtry.Remove(rowFiltr);
                this.tscbFiltry.ComboBox.DataSource = null;
                this.tscbFiltry.ComboBox.DataSource = filtry;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show("Nepodařilo se odstranit filtr.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region BackGround Worker

        private void bw_LoadData_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Fask.Interfaces.Filtry.SkladLokaceCompareToISFiltr filtr = (Fask.Interfaces.Filtry.SkladLokaceCompareToISFiltr)e.Argument;  
                DataSets_All dss = new DataSets_All();

                if (bw_LoadData.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                // nacteni dat v oddelenem vlakne
                //ds = ((Fask.Interfaces.SkladLokace.ISkladLokace)providerSkladLokace).GetFiltrovanySkladLokaceStav(filtr);


                if ((provider != null) && (provider is Fask.Interfaces.SkladLokace.ISkladLokace2_GetFilt_CopareToIS_IS))
                    dss.ds_IS = ((Fask.Interfaces.SkladLokace.ISkladLokace2_GetFilt_CopareToIS_IS)provider).GetFilt_CopareToIS_IS(filtr);
                else
                    throw new NotImplementedException("Provider neimplementuje ISkladLokace2_GetFilt_CopareToIS_IS.");


                if ((provider != null) && (provider is Fask.Interfaces.SkladLokace.ISkladLokace2_GetFilt_CopareToIS_FASK))
                    dss.ds_FASK = ((Fask.Interfaces.SkladLokace.ISkladLokace2_GetFilt_CopareToIS_FASK)provider).GetFilt_CopareToIS_FASK(filtr);
                else
                    throw new NotImplementedException("Provider neimplementuje ISkladLokace2_GetFilt_CopareToIS_FASK.");


                if (bw_LoadData.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                e.Result = dss;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bw_LoadData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                // check error, check cancel, then use result
                if (e.Error != null)
                {
                    // handle the error
                    ds_FASK = new Fask.Interfaces.DataSets.SkladLokace_CompareToIS();
                    bs_FASK.DataSource = ds_FASK;

                    ds_IS = new Fask.Interfaces.DataSets.SkladLokace_CompareToIS();
                    bs_IS.DataSource = ds_IS;

                    SetStratusLabelText_IS(-1);
                    SetStratusLabelText_FASK(-1);

                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, e.Error);
                    MessageBox.Show(e.Error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (e.Cancelled)
                {
                    // handle cancellation
                    bs_FASK.DataSource = new Fask.Interfaces.DataSets.SkladLokace();
                    bs_FASK.DataSource = ds_FASK;

                    bs_IS.DataSource = new Fask.Interfaces.DataSets.SkladLokace();
                    bs_IS.DataSource = ds_IS;

                    SetStratusLabelText_IS(-1);
                    SetStratusLabelText_FASK(-1);
                }
                else
                {
                    // uspesne dokonceno ...
                    // use it on the UI thread
                    var dds = (DataSets_All)e.Result;

                    ds_FASK = dds.ds_FASK;
                    ds_IS = dds.ds_IS;


                    if (ds_FASK == null)
                        ds_FASK = new Fask.Interfaces.DataSets.SkladLokace_CompareToIS();

                    if (ds_IS == null)
                        ds_IS = new Fask.Interfaces.DataSets.SkladLokace_CompareToIS();


                    bs_FASK.DataSource = ds_FASK;
                    bs_IS.DataSource = ds_IS;

                    if(ds_FASK.LokMech_Stav.Count == 0)
                        SetStratusLabelText_FASK(-1);
                    else
                        SetStratusLabelText_FASK(0);

                    if (ds_IS.SKz_Stav.Count == 0)
                        SetStratusLabelText_IS(-1);
                    else
                        SetStratusLabelText_IS(0);

                    
                    
                }



            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ProgressIndicatorStop();
            }
        }

        internal class DataSets_All
        {
            public Fask.Interfaces.DataSets.SkladLokace_CompareToIS ds_IS;
            public Fask.Interfaces.DataSets.SkladLokace_CompareToIS ds_FASK;
        }

        #endregion

        #region Status Label, pocty radku

        private void SetStratusLabelText_IS(int index)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    SetStratusLabelText_IS(index);
                }));

                return;
            }

            tssl_IS_Count.Text = string.Format("{0}/{1}", index + 1, ds_IS.SKz_Stav.Count);
        }


        private void SetStratusLabelText_FASK(int index)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    SetStratusLabelText_FASK(index);
                }));

                return;
            }

            tssl_FASK_Count.Text = string.Format("{0}/{1}", index + 1, ds_FASK.LokMech_Stav.Count);
        }





        #endregion

        #region Logika Lokačního mechanizmu, přesunu položek

        private void Insert_LokMech_Zaznam(
            Fask.Interfaces.DataSets.SkladLokace_CompareToIS.SKz_StavRow row,
            Fask.Interfaces.SkladLokace.TypeOfRecord POHYB_TYPE,
            decimal QTYSHPPD,
            string LOCNCODE
            )
        {
            try
            {

                Fask.Interfaces.SkladLokace.LokacePohyb record = new Fask.Interfaces.SkladLokace.LokacePohyb();


                record.ITEMNMBR = row.ITEMNMBR;
                record.DOCUMENT_NUMBER = "PoloAutoKorekce";
                record.POHYB_TYPE = POHYB_TYPE;
                record.POHYB_SRC = record.POHYB_TYPE.ToString();
                record.SOURCE = "K";    // Konzola
                record.QTYSHPPD_DEF = 0;
                record.QTYSHPPD = QTYSHPPD;
                record.SERLTNUM = row.IsSERLTNUMNull() ? string.Empty : row.SERLTNUM;
                record.SKL_ID_SRC = row.SKL_ID;
                record.LOCNCODE_SRC = LOCNCODE;
                record.SKL_ID_DST = string.Empty;
                record.LOCNCODE_DST = string.Empty;
                record.UserID = int.Parse(FASK.Logins.Uzivatel.Instance.UserID);
                record.TermID = Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].TerminalID;
                record.guid = Guid.NewGuid();
                DateTime dtnow = DateTime.Now;
                record.dateeveS = record.dateeveT = dtnow;
                record.Expiration = row.IsEXPIRACENull() ? (DateTime?)null : row.EXPIRACE;
                record.ITEMDESC = row.ITEMDESC;
                record.CountEntries = null;
                record.PRAC_ID_OWNER =  string.Empty ;
                record.QTY_OWNER = 0;


                if ((provider != null) && (provider is Fask.Interfaces.SkladLokace.ISkladLokace2_InsertStavPohyb))
                    ((Fask.Interfaces.SkladLokace.ISkladLokace2_InsertStavPohyb)provider).InsertStavPohyb(record);
                else
                    throw new NotImplementedException("Provider neimplementuje ISkladLokace2_InsertStavPohyb.");

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        }

        #endregion

        #region BackGroundWorker PoloAutomat

        private void bw_PoloAutomat_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {

                if (bw_PoloAutomat.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                foreach (DataGridViewRow item in dg_IS.SelectedRows)
                {
                    Fask.Interfaces.DataSets.SkladLokace_CompareToIS.SKz_StavRow row = (Fask.Interfaces.DataSets.SkladLokace_CompareToIS.SKz_StavRow)((DataRowView)item.DataBoundItem).Row;

                    if(row.status == 0)
                    {
                        Insert_LokMech_Zaznam(
                            row,
                            Fask.Interfaces.SkladLokace.TypeOfRecord.P,
                            row.QTYSHPPD_Pohoda,
                            "999"
                            );
                    }

                    if (row.status == 1)
                    {
                        Insert_LokMech_Zaznam(
                            row,
                            Fask.Interfaces.SkladLokace.TypeOfRecord.P,
                            -(row.QTYSHPPD_LokMech - row.QTYSHPPD_Pohoda),
                            "888"
                            );
                    }

                    if (row.status == 2)
                    {
                        Insert_LokMech_Zaznam(
                            row,
                            Fask.Interfaces.SkladLokace.TypeOfRecord.P,
                            row.QTYSHPPD_Pohoda - row.QTYSHPPD_LokMech,
                            "999"
                            );
                    }

                    if (row.status == 4)
                    {
                        Insert_LokMech_Zaznam(
                            row,
                            Fask.Interfaces.SkladLokace.TypeOfRecord.P,
                            -row.QTYSHPPD_LokMech,
                            "888"
                            );
                    }

                    if (row.status == 5)
                    {
                        Insert_LokMech_Zaznam(
                            row,
                            Fask.Interfaces.SkladLokace.TypeOfRecord.P,
                            row.QTYSHPPD_Pohoda ,
                            "999"
                            );
                    }
                }

                if (bw_PoloAutomat.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bw_PoloAutomat_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {

                PerformVyhledat();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ProgressIndicatorStop();
            }
        }

        #endregion

        #region Napoveda filtry

        private void chb_R_MouseHover(object sender, EventArgs e)
        {
            toolTip1.RemoveAll();

            if (sender is CheckBox)
            {
                CheckBox cb = sender as CheckBox;
                toolTip1.SetToolTip(cb, "Jedná se o kontextové hledaní z prava.");

            }


        }

        private void chb_R_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.RemoveAll();
        }

        private void chb_L_MouseHover(object sender, EventArgs e)
        {
            toolTip1.RemoveAll();

            if (sender is CheckBox)
            {
                CheckBox cb = sender as CheckBox;
                toolTip1.SetToolTip(cb, "Jedná se o kontextové hledaní z leva.");

            }


        }

        private void chb_L_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.RemoveAll();
        }

        #endregion

        #region Exporty IS

        private void exportDoCSVVseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                this.dg_IS.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exportDoCSVOznaceneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                this.dg_IS.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exportDoExcelVseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                this.dg_IS.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exportDoExceOznaceneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                dg_IS.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exportDoXMLVseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                dg_IS.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exportDoXMLOznaceneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                dg_IS.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Exporty FASK

        private void exportDoCSVVseToolStripMenuItem_FASK_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                this.dg_FASK.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exportDoCSVOznaceneToolStripMenuItem_FASK_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                this.dg_FASK.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exportDoExcelVseToolStripMenuItem_FASK_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                this.dg_FASK.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exportDoExceOznaceneToolStripMenuItem_FASK_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                dg_FASK.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exportDoXMLVseToolStripMenuItem_FASK_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                dg_FASK.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exportDoXMLOznaceneToolStripMenuItem_FASK_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                dg_FASK.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion


    }



}
