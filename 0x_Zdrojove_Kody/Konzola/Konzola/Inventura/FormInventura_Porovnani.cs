using Fask.Interfaces.DataSets;
using Fask.Logging;
using Konzola.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Konzola.Inventura
{
    public partial class FormInventura_Porovnani : Form
    {
        #region Parametry

        private Fask.Interfaces.IMES provider = null;

        /// <summary>
        /// Seznam vsech nactenych filtru.
        /// </summary>
        private List<Fask.Interfaces.Filtry.InventuraCompare> filtry = new List<Fask.Interfaces.Filtry.InventuraCompare>();

        /// <summary>
        /// Vybrany filtr.
        /// </summary>
        private Fask.Interfaces.Filtry.InventuraCompare rowFiltr
        {
            get
            {
                try
                {
                    return tscbFiltry.SelectedItem as Fask.Interfaces.Filtry.InventuraCompare;
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Vybrany radek CZMST_I123
        /// </summary>
        public Fask.Interfaces.DataSets.Inventura_Compare.CZMST_I123Row SelectedRow_I123
        {
            get
            {
                try
                {
                    return ((DataRowView)(dg_I123.BindingContext[bs_I123].Current)).Row as Fask.Interfaces.DataSets.Inventura_Compare.CZMST_I123Row;
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Vybrany radek CZMST_I4
        /// </summary>
        public Fask.Interfaces.DataSets.Inventura_Compare.CZMST_I4Row SelectedRow_I4
        {
            get
            {
                try
                {
                    return ((DataRowView)(dg_I4.BindingContext[bs_I4].Current)).Row as Fask.Interfaces.DataSets.Inventura_Compare.CZMST_I4Row;
                }
                catch
                {
                    return null;
                }
            }
        }

        #endregion

        #region Eventy fromu

        public FormInventura_Porovnani()
        {
            InitializeComponent();

            #if DEBUG
                        tb_CountEntries.Text = "23";
            #endif

            this.WindowState = FormWindowState.Maximized;

            panelButtons.Menu = menuStrip1;
            this.dg_I123.UpdateColumnHeaderCellsByDatasource();
            this.dg_I4.UpdateColumnHeaderCellsByDatasource();
        }

        private void FormInventura_Porovnani_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;

                panelButtons.LoadConfiguration(this.GetType().ToString());
                panelButtons.Init();
                panelButtons.Size = new Size(85, 700);

                this.dg_I123.LoadConfiguration("I123_" + this.GetType().ToString());
                this.dg_I4.LoadConfiguration("I4_" + this.GetType().ToString());

                dg_SearchToolBar_I4.SetColumns(dg_I4.Columns);
                dg_SearchToolBar_I123.SetColumns(dg_I123.Columns);

                //// načtení konfigurace vytvořených filtrů
                this.filtry = Extensions.ListExt.ReadFromXML<Fask.Interfaces.Filtry.InventuraCompare>(this.GetType().ToString() + ".filtr");
                tscbFiltry.ComboBox.DataSource = this.filtry;
                tscbFiltry.SelectedItem = null;
                tscbFiltry.ComboBox.DropDownWitdhAutosize();

                // inicializace providera
                InitProvider();

                if (provider == null)
                    throw new Exception("Provider 'InventuraCompare' není inicializován");

                SetStratusLabelText_I4(-1);
                SetStratusLabelText_I123(-1);

                buttonVyhledat.Focus();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void FormInventura_Porovnani_KeyDown(object sender, KeyEventArgs e)
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

        private void FormInventura_Porovnani_Shown(object sender, EventArgs e)
        {
            try
            {
                this.progressIndicator_I123.Size = new Size(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ostatni[0].ProgressIndicatorSize, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ostatni[0].ProgressIndicatorSize);
                this.progressIndicator_I123.Location = new Point(this.dg_I123.Location.X + (this.dg_I123.Width / 2) - (progressIndicator_I123.Size.Width / 2), this.dg_I123.Location.Y + (this.dg_I123.Height / 2) - (progressIndicator_I123.Size.Height / 2));

                this.progressIndicator_I4.Size = new Size(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ostatni[0].ProgressIndicatorSize, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ostatni[0].ProgressIndicatorSize);
                this.progressIndicator_I4.Location = new Point(this.dg_I4.Location.X + (this.dg_I4.Width / 2) - (progressIndicator_I123.Size.Width / 2), this.dg_I4.Location.Y + (this.dg_I4.Height / 2) - (progressIndicator_I123.Size.Height / 2));


            }
            catch { }
        }


        private void FormInventura_Porovnani_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                panelButtons.SaveConfiguration(this.GetType().ToString());

                this.dg_I123.SaveConfiguration("I123_" + this.GetType().ToString());
                this.dg_I4.SaveConfiguration("I4_" + this.GetType().ToString());

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
                            if (typeof(Fask.Interfaces.Inventura.IInventura2).IsAssignableFrom(t))
                            {
                                provider = (Fask.Interfaces.Inventura.IInventura2)providerAssemlby.CreateInstance(t.FullName);
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
            #region prace s datagridem - nastaveni numeric hodnot na pocet desetinnych mist
            try
            {
                // Procházení sloupců v DataGridView
                foreach (DataGridViewColumn column in dg_I123.Columns)
                {
                    // Získání typu dat pro sloupec
                    Type dataType = null;

                    if (dg_I123.DataSource is DataTable dataTable)
                    {
                        // Pokud je datový zdroj DataTable
                        if (dataTable.Columns.Contains(column.DataPropertyName))
                        {
                            dataType = dataTable.Columns[column.DataPropertyName].DataType;
                        }
                    }
                    else if (dg_I123.DataSource is BindingSource bindingSource)
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

            PerformVyhledat();
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


                if(string.IsNullOrEmpty(tb_CountEntries.Text))
                {
                    MessageBox.Show(this,"Číslo dávky je povinný parametr pro filtr!", "Zapomětlivko!", MessageBoxButtons.OK,MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1 );
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

                Fask.Interfaces.Filtry.InventuraCompare filtr = new Fask.Interfaces.Filtry.InventuraCompare();
                if (!CreateFilter(ref filtr))
                    return;

                int FirstDisplayedScrollingRowIndex_I4 = this.dg_I4.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index
                int FirstDisplayedScrollingRowIndex_I123 = this.dg_I123.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

                bw_LoadData.RunWorkerAsync(filtr);

                if ((FirstDisplayedScrollingRowIndex_I123 >= 0) && ((this.dg_I123.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex_I123)) this.dg_I123.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex_I123; //Restore Scroll Index

                if ((FirstDisplayedScrollingRowIndex_I4 >= 0) && ((this.dg_I4.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex_I4)) this.dg_I4.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex_I4; //Restore Scroll Index
            }
            catch (Exception ex)
            {
                ProgressIndicatorStop();
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region DataGrid eventy

        private void dg_SearchToolBar_I4_Search(object sender, Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs e)
        {
            bool restartsearch = true;
            int startColumn = 0;
            int startRow = 0;
            if (!e.FromBegin)
            {
                bool endcol = dg_I4.CurrentCell.ColumnIndex + 1 >= dg_I4.ColumnCount;
                bool endrow = dg_I4.CurrentCell.RowIndex + 1 >= dg_I4.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dg_I4.CurrentCell.ColumnIndex;
                    startRow = dg_I4.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dg_I4.CurrentCell.ColumnIndex + 1;
                    startRow = dg_I4.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dg_I4.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dg_I4.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dg_I4.CurrentCell = c;
        }

        private void dg_SearchToolBar_I123_Search(object sender, Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs e)
        {
            bool restartsearch = true;
            int startColumn = 0;
            int startRow = 0;
            if (!e.FromBegin)
            {
                bool endcol = dg_I123.CurrentCell.ColumnIndex + 1 >= dg_I123.ColumnCount;
                bool endrow = dg_I123.CurrentCell.RowIndex + 1 >= dg_I123.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dg_I123.CurrentCell.ColumnIndex;
                    startRow = dg_I123.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dg_I123.CurrentCell.ColumnIndex + 1;
                    startRow = dg_I123.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dg_I123.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dg_I123.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dg_I123.CurrentCell = c;
        }


        private void dg_I4_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewRow row in dg_I4.Rows)
            {

                Inventura_Compare.CZMST_I4Row radek = (Inventura_Compare.CZMST_I4Row)((DataRowView)row.DataBoundItem).Row;
                GetColorToRow(row, radek.IsstatusNull() ? (int?)null : radek.status);
            }
        }

        private void dg_I123_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewRow row in dg_I123.Rows)
            {

                Inventura_Compare.CZMST_I123Row radek = (Inventura_Compare.CZMST_I123Row)((DataRowView)row.DataBoundItem).Row;
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
                row.DefaultCellStyle.BackColor = Color.Lime;
            }
            else if (status == 2)
            {
                row.DefaultCellStyle.BackColor = Color.Yellow;
            }
            else if (status == 3)
            {
                row.DefaultCellStyle.BackColor = Color.Tomato;
            }
            else if (status == 4)
            {
                row.DefaultCellStyle.BackColor = Color.Orange;
            }
        }

        private void dg_I4_SelectionChanged(object sender, EventArgs e)
        {
            if (dg_I4.Focused)
            {
                try
                {
                    if ((dg_I4.SelectedRows.Count > 1) || (dg_I4.SelectedRows.Count == 0))
                    {
                        dg_I123.ClearSelection();
                        return;
                    }

                    dg_I123.ClearSelection();

                    foreach (DataGridViewRow row in dg_I4.SelectedRows)
                    {
                        var radek = (Inventura_Compare.CZMST_I4Row)((DataRowView)row.DataBoundItem).Row;

                        foreach (DataGridViewRow R in dg_I123.Rows)
                        {
                            var radek1 = (Inventura_Compare.CZMST_I123Row)((DataRowView)R.DataBoundItem).Row;

                            if (
                                (radek1.IsITEMNMBRNull() ? "" : radek1.ITEMNMBR) == (radek.IsITEMNMBRNull() ? "" : radek.ITEMNMBR)
                                & (radek1.IsSERLNMBRNull() ? "" : radek1.SERLNMBR) == (radek.IsSERLNMBRNull() ? "" : radek.SERLNMBR)
                                & (radek1.IsExpiraceNull() ? DateTime.MinValue : radek1.Expirace) == (radek.IsExpiraceNull() ? DateTime.MinValue : radek.Expirace)
                                & (radek1.IsVNDITNUMNull() ? "" : radek1.VNDITNUM) == (radek.IsVNDITNUMNull() ? "" : radek.VNDITNUM)
                                & (radek1.IsMJNull() ? "" : radek1.MJ) == (radek.IsMJNull() ? "" : radek.MJ)
                                )
                            {
                                R.Selected = true;

                                dg_I123.FirstDisplayedScrollingRowIndex = R.Index;
                                SetStratusLabelText_I123(R.Index);
                                SetStratusLabelText_I4(row.Index);
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

        private void dg_I123_SelectionChanged(object sender, EventArgs e)
        {
            if (dg_I123.Focused)
            {
                try
                {
                    if ((dg_I123.SelectedRows.Count > 1) || (dg_I123.SelectedRows.Count == 0))
                    {
                        dg_I4.ClearSelection();
                        return;
                    }

                    dg_I4.ClearSelection();

                    foreach (DataGridViewRow row in dg_I123.SelectedRows)
                    {
                        var radek = (Inventura_Compare.CZMST_I123Row)((DataRowView)row.DataBoundItem).Row;

                        foreach (DataGridViewRow R in dg_I4.Rows)
                        {
                            var radek1 = (Inventura_Compare.CZMST_I4Row)((DataRowView)R.DataBoundItem).Row;

                            if (
                                (radek1.IsITEMNMBRNull() ? "" : radek1.ITEMNMBR) == (radek.IsITEMNMBRNull() ? "" : radek.ITEMNMBR)
                                & (radek1.IsSERLNMBRNull() ? "" : radek1.SERLNMBR) == (radek.IsSERLNMBRNull() ? "" : radek.SERLNMBR)
                                & (radek1.IsExpiraceNull() ? DateTime.MinValue : radek1.Expirace) == (radek.IsExpiraceNull() ? DateTime.MinValue : radek.Expirace)
                                & (radek1.IsVNDITNUMNull() ? "" : radek1.VNDITNUM) == (radek.IsVNDITNUMNull() ? "" : radek.VNDITNUM)
                                & (radek1.IsMJNull() ? "" : radek1.MJ) == (radek.IsMJNull() ? "" : radek.MJ)
                                )
                            {
                                R.Selected = true;

                                dg_I4.FirstDisplayedScrollingRowIndex = R.Index;
                                SetStratusLabelText_I4(R.Index);
                                SetStratusLabelText_I123(row.Index);
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
            progressIndicator_I123.Stop();
            progressIndicator_I123.Visible = false;

            progressIndicator_I4.Stop();
            progressIndicator_I4.Visible = false;
        }

        private void ProgressIndicatorStart()
        {
            try
            {
                // prepocet stredu datagridu
                this.progressIndicator_I123.Location = new Point(this.dg_I123.Location.X + (this.dg_I123.Width / 2) - (progressIndicator_I123.Size.Width / 2), this.dg_I123.Location.Y + (this.dg_I123.Height / 2) - (progressIndicator_I123.Size.Height / 2));
            }
            catch { }
            progressIndicator_I123.Start();
            progressIndicator_I123.Visible = true;


            try
            {
                // prepocet stredu datagridu
                this.progressIndicator_I4.Location = new Point(this.dg_I4.Location.X + (this.dg_I4.Width / 2) - (progressIndicator_I4.Size.Width / 2), this.dg_I4.Location.Y + (this.dg_I4.Height / 2) - (progressIndicator_I4.Size.Height / 2));
            }
            catch { }
            progressIndicator_I4.Start();
            progressIndicator_I4.Visible = true;
        }


        #endregion

        #region Filtry

        /// <summary>
        /// Vytvoreni filtru, ktery se pouzije.
        /// </summary>
        /// <param name="filtr"></param>
        /// <returns></returns>
        private bool CreateFilter(ref Fask.Interfaces.Filtry.InventuraCompare filtr)
        {
            if (filtr == null)
                filtr = new Fask.Interfaces.Filtry.InventuraCompare();

            filtr.SklID = tb_SKL_ID.Text.Trim();

            filtr.CountEntries = tb_CountEntries.Text.Trim();

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
        private void PerformNastavitFiltr(Fask.Interfaces.Filtry.InventuraCompare filtr)
        {
            try
            {
                if (filtr == null)
                    return;

                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                PerformVycistitFiltr();

                tb_SKL_ID.Text = filtr.SklID;

                 tb_CountEntries.Text = filtr.CountEntries;

                tb_KodPolozky.Text = filtr.ITEMCODE;
                chb_KodPolozky_L.Checked = filtr.ITEMCODE_L;
                chb_KodPolozky_R.Checked = filtr.ITEMCODE_R;

                tb_NazevPolozky.Text = filtr.ITEMDESC ;
                chb_NazevPolozky_L.Checked = filtr.ITEMDESC_L;
                chb_NazevPolozky_R.Checked = filtr.ITEMDESC_R;

                tb_SarzeSN.Text = filtr.SERLTNUM;
                chb_SarzeSN_L.Checked = filtr.SERLTNUM_L;
                chb_SarzeSN_L.Checked = filtr.SERLTNUM_L;

                chb_V_0.Checked = filtr.V_0;
                chb_V_1.Checked = filtr.V_1;
                chb_V_2.Checked = filtr.V_2;
                chb_V_3.Checked = filtr.V_3;
                chb_V_4.Checked = filtr.V_4;

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
                tb_CountEntries.Text = 
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
                = true;

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

                Fask.Interfaces.Filtry.InventuraCompare filtr = rowFiltr;

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

                Fask.Interfaces.Filtry.InventuraCompare filtr = new Fask.Interfaces.Filtry.InventuraCompare();
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
                Fask.Interfaces.Filtry.InventuraCompare filtr = (Fask.Interfaces.Filtry.InventuraCompare)e.Argument;  
                DataSets_All dss = new DataSets_All();

                if (bw_LoadData.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

 
                if ((provider != null) && (provider is Fask.Interfaces.Inventura.IInventura2_GetCompare_I4))
                    dss.ds_I4 = ((Fask.Interfaces.Inventura.IInventura2_GetCompare_I4)provider).GetCompare_I4(filtr);
                else
                    throw new NotImplementedException("Provider neimplementuje IInventura2_GetCompare_I4.");


                if ((provider != null) && (provider is Fask.Interfaces.Inventura.IInventura2_GetCompare_I123))
                    dss.ds_I123 = ((Fask.Interfaces.Inventura.IInventura2_GetCompare_I123)provider).GetCompare_I123(filtr);
                else
                    throw new NotImplementedException("Provider neimplementuje IInventura2_GetCompare_I123.");


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
                    ds_I123 = new Fask.Interfaces.DataSets.Inventura_Compare();
                    bs_I123.DataSource = ds_I123;

                    ds_I4 = new Fask.Interfaces.DataSets.Inventura_Compare();
                    bs_I4.DataSource = ds_I4;

                    SetStratusLabelText_I4(-1);
                    SetStratusLabelText_I123(-1);

                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, e.Error);
                    MessageBox.Show(e.Error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (e.Cancelled)
                {
                    // handle cancellation
                    bs_I123.DataSource = new Fask.Interfaces.DataSets.Inventura_Compare();
                    bs_I123.DataSource = ds_I123;

                    bs_I4.DataSource = new Fask.Interfaces.DataSets.Inventura_Compare();
                    bs_I4.DataSource = ds_I4;

                    SetStratusLabelText_I4(-1);
                    SetStratusLabelText_I123(-1);
                }
                else
                {
                    // uspesne dokonceno ...
                    // use it on the UI thread
                    var dds = (DataSets_All)e.Result;

                    ds_I123 = dds.ds_I123;
                    ds_I4 = dds.ds_I4;


                    if (ds_I123 == null)
                        ds_I123 = new Fask.Interfaces.DataSets.Inventura_Compare();

                    if (ds_I4 == null)
                        ds_I4 = new Fask.Interfaces.DataSets.Inventura_Compare();


                    bs_I123.DataSource = ds_I123;
                    bs_I4.DataSource = ds_I4;

                    if(ds_I123.CZMST_I123.Count == 0)
                        SetStratusLabelText_I123(-1);
                    else
                        SetStratusLabelText_I123(0);

                    if (ds_I4.CZMST_I123.Count == 0)
                        SetStratusLabelText_I4(-1);
                    else
                        SetStratusLabelText_I4(0);
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
            public Fask.Interfaces.DataSets.Inventura_Compare ds_I4;
            public Fask.Interfaces.DataSets.Inventura_Compare ds_I123;
        }

        #endregion

        #region Status Label, pocty radku

        private void SetStratusLabelText_I4(int index)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    SetStratusLabelText_I4(index);
                }));

                return;
            }

            tssl_I4_Count.Text = string.Format("{0}/{1}", index + 1, ds_I4.CZMST_I4.Count);
        }


        private void SetStratusLabelText_I123(int index)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    SetStratusLabelText_I123(index);
                }));

                return;
            }

            tssl_I123_Count.Text = string.Format("{0}/{1}", index + 1, ds_I123.CZMST_I123.Count);
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

                this.dg_I4.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                this.dg_I4.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
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

                this.dg_I4.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                dg_I4.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
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

                dg_I4.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                dg_I4.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Exporty FASK

        private void exportDoCSVVseToolStripMenuItem_I123_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                this.dg_I123.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exportDoCSVOznaceneToolStripMenuItem_I123_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                this.dg_I123.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exportDoExcelVseToolStripMenuItem_I123_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                this.dg_I123.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exportDoExceOznaceneToolStripMenuItem_I123_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                dg_I123.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exportDoXMLVseToolStripMenuItem_I123_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                dg_I123.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exportDoXMLOznaceneToolStripMenuItem_I123_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                dg_I123.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #endregion

        private void tiskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PerformPrint();
        }

        #region Tisky

        private void PerformPrint()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if(ds_I4.CZMST_I4.Count < 0)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                Fask.Interfaces.DataSets.Inventura_Compare.CZMST_TiskDataTable dt = new Inventura_Compare.CZMST_TiskDataTable();


                foreach (var item in ds_I4.CZMST_I4)
                {
                    string predloha = "-";
                    string rozdil = "-";

                    var tmpI123 = ds_I123.CZMST_I123.Where(x =>
                    (x.IsITEMNMBRNull() ? "" : x.ITEMNMBR) == (item.IsITEMNMBRNull() ? "" : item.ITEMNMBR)
                    & (x.IsSERLNMBRNull() ? "" : x.SERLNMBR) == (item.IsSERLNMBRNull() ? "" : item.SERLNMBR)
                    & (x.IsVNDITNUMNull() ? "" : x.VNDITNUM) == (item.IsVNDITNUMNull() ? "" : item.VNDITNUM)
                    & (x.IsMJNull() ? "" : x.MJ) == (item.IsMJNull() ? "" : item.MJ)
                    );


                    if(tmpI123 != null && tmpI123.Count() == 1)
                    {
                        var row = tmpI123.First();
                        predloha = row.QUANTITY.ToString("#,##0.0");
                        var q = row.QUANTITY - item.QUANTITY;

                        if (row.QUANTITY > item.QUANTITY)
                        {
                            rozdil = (-q).ToString("#,##0.0");
                        }
                        else if (row.QUANTITY < item.QUANTITY)
                        {
                            rozdil = (-q).ToString("#,##0.0");
                        }
                        else if (row.QUANTITY == item.QUANTITY)
                        {
                            continue;
                        }
                    }
                    else
                    {
                        predloha = "0";
                        rozdil = item.QUANTITY.ToString("#,###.0");
                    }

                    dt.AddCZMST_TiskRow(item.CountEntries,
                        item.ITEMNMBR,
                        item.ITEMDESC,
                        item.ITEMCODE,
                        item.VNDITNUM,
                        item.SKL_ID,
                        item.SKL_DESC,
                        item.MJ,
                        item.IsSERLNMBRNull() ? string.Empty : item.SERLNMBR.Trim(),
                        item.IsExpiraceNull() ? DateTime.MinValue : item.Expirace,
                        predloha,
                        item.QUANTITY.ToString("#,###.0"),
                        rozdil
                        );
                }
 
                PrintReport(dt, tb_CountEntries.Text);

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void PrintReport(Fask.Interfaces.DataSets.Inventura_Compare.CZMST_TiskDataTable dt, string CountEntries)
        {
            try
            {

                if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                {
                    foreach (var item in dt)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start inventura porovnani TISK rdlc-------------------------------------");

                        foreach (var property in item.GetType().GetProperties())
                        {
                            try
                            {
                                var propertyName = property.Name;
                                object propertyValue = null;

                                try
                                {
                                    propertyValue = property.GetValue(item);
                                }
                                catch (Exception ex)
                                {

                                    propertyValue = string.Empty;
                                }

                                Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, $"{propertyName}: {propertyValue}");
                            }
                            catch (Exception ex)
                            {
                                // Log the exception for the specific property
                                Fask.Logging.ExceptionHandler2.Handle(LogLevel.Error, $"Error accessing property: {property.Name}. Exception: {ex.Message}");
                            }
                        }

                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END inventura porovnani TISK rdlc-------------------------------------");
                    } 
                }




                using (PrintReportLibrary.CoolPrintPreviewDialog plr = new PrintReportLibrary.CoolPrintPreviewDialog())
                {

                    plr.Params = new Microsoft.Reporting.WinForms.ReportParameter[]
                    {
                    new Microsoft.Reporting.WinForms.ReportParameter("Param_Firma", Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Inventura_Tisk[0].Vyroba_Param_Firma),
                    new Microsoft.Reporting.WinForms.ReportParameter("Param_Adresa", Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Inventura_Tisk[0].Vyroba_Param_Adresa),
                    new Microsoft.Reporting.WinForms.ReportParameter("Param_PSC", Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Inventura_Tisk[0].Vyroba_Param_PSC),
                    new Microsoft.Reporting.WinForms.ReportParameter("Param_Obec", Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Inventura_Tisk[0].Vyroba_Param_Obec),

                    new Microsoft.Reporting.WinForms.ReportParameter("Param_Datum", DateTime.Now.ToShortDateString())
                    };

                    plr.NazevDataTable = "DataSet";
                    plr.DataTable = dt;

                    List<PrintReportLibrary.TypeData> tmplist = new List<PrintReportLibrary.TypeData>();
                    tmplist.Add(PrintReportLibrary.TypeData.DataTable);
                    plr.typedata = new List<PrintReportLibrary.TypeData>(tmplist);
                    plr.Projekt = PrintReportLibrary.Projekt.MST;
                    plr.ShowPreview = true;

                    //TODO ošetreny, zda existuje tiskova sestava
                    plr.Path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Servis", "Print", "Templates", Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Inventura_Tisk[0].InventuraCompareTiskTemplate);

                    Fask.Logging.ExceptionHandler2.Handle(LogLevel.Info, "Path Tisk InventuraCompare:'" + plr.Path);
                    Fask.Logging.ExceptionHandler2.Handle(LogLevel.Info, "Path to PrinterDirectory:'" + MySystem.MyPath.PrintDirectory);

                    PrintReportLibrary.MyPath.PrintTemplateDirectory = Path.Combine(MySystem.MyPath.PrintDirectory);


                    plr.CountEntries = CountEntries.ToString();


                    plr.Print(this);

                }
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }



        #endregion
    }



}
