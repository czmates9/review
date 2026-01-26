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
using System.Reflection;

namespace Konzola.Vyroba
{
    public partial class FormPlanovaniVyroby_Rozbor : Form
    {

        private Fask.Interfaces.IMES providerPV = null;

        public Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVHRow SelectedPVHRow
        {
            get
            {
                try
                {
                    return ((DataRowView)(dg_PVH.BindingContext[this.bs_PVH].Current)).Row as Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVHRow;

                }
                catch
                {
                    return null;
                }
            }
        }

        public Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVPRow SelectedPVPRow
        {
            get
            {
                try
                {
                    return ((DataRowView)(dg_PVP.BindingContext[this.bs_PVP].Current)).Row as Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVPRow;
                }
                catch
                {
                    return null;
                }
            }
        }

        #region Eventy formu

        public FormPlanovaniVyroby_Rozbor()
        {
            InitializeComponent();
            this.dg_PVH.UpdateColumnHeaderCellsByDatasource();
            this.dg_PVP.UpdateColumnHeaderCellsByDatasource();
            panelButtons.Menu = menuStrip2;
        }

        private void FormPlanovaniVyroby_Rozbor_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                this.WindowState = FormWindowState.Maximized;
                this.dg_PVH.LoadConfiguration(this.GetType().ToString() + "PVH");
                this.dg_PVP.LoadConfiguration(this.GetType().ToString() + "PVP");

                panelButtons.LoadConfiguration(this.GetType().ToString());
                panelButtons.Init();
                panelButtons.Size = new Size(85, 700);

                advancedDataGridViewSearchToolBar_PVH.SetColumns(dg_PVH.Columns);
                advancedDataGridViewSearchToolBar_PVP.SetColumns(dg_PVP.Columns);

                InitProvider();

                if (providerPV == null)
                    throw new Exception("Provider 'PV' není inicializován");


                buttonVyhledat_PVH.Focus();
                

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormPlanovaniVyroby_Rozbor_KeyDown(object sender, KeyEventArgs e)
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

        private void FormPlanovaniVyroby_Rozbor_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.dg_PVH.SaveConfiguration(this.GetType().ToString() + "PVH");
                this.dg_PVP.SaveConfiguration(this.GetType().ToString() + "PVP");

                panelButtons.SaveConfiguration(this.GetType().ToString());

                Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ostatni[0].FormPlanovaniVyroby_Rozbor_SplitPoloha = splitContainer1.SplitterDistance;

                Konfigurace.Globals_Konfig_Konzola.SaveConfiguration();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormPlanovaniVyroby_Rozbor_Shown(object sender, EventArgs e)
        {
            try
            {
                splitContainer1.SplitterDistance = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ostatni[0].FormPlanovaniVyroby_Rozbor_SplitPoloha;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                splitContainer1.SplitterDistance = 1000;
            }
        }
        
        #endregion


        private void UpdatePVH()
        {
            bs_PVH.DataSource = this.ds;
        }

        private void InitProvider()
        {
            #region providerPV

            try
            {
                if (String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                    return;

                if (providerPV == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Vyroba.PV.IPV).IsAssignableFrom(t))
                            {
                                providerPV = (Fask.Interfaces.Vyroba.PV.IPV)providerAssemlby.CreateInstance(t.FullName);
                                if (providerPV != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                providerPV.InitProvider();
            
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            #endregion

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

  
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            NastavDatagrid();

            try
            {
                if (SelectedPVHRow == null)
                    return;

                if (providerPV is Fask.Interfaces.Vyroba.PV.IPV_FillByRefPVH)
                    ((Fask.Interfaces.Vyroba.PV.IPV_FillByRefPVH)providerPV).FillByRefPVH(this.ds, SelectedPVHRow.DEX_ROW_ID);
                else
                    throw new Exception("IPV_FillByRefPVH not implementet");

                UpdatePVP();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void UpdatePVP()
        {
            bs_PVP.DataSource = this.ds;
        }


        private void konecToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }


        private void advancedDataGridViewSearchToolBar_PVH_Search(object sender, Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs e)
        {
            bool restartsearch = true;
            int startColumn = 0;
            int startRow = 0;
            if (!e.FromBegin)
            {
                bool endcol = dg_PVH.CurrentCell.ColumnIndex + 1 >= dg_PVH.ColumnCount;
                bool endrow = dg_PVH.CurrentCell.RowIndex + 1 >= dg_PVH.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dg_PVH.CurrentCell.ColumnIndex;
                    startRow = dg_PVH.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dg_PVH.CurrentCell.ColumnIndex + 1;
                    startRow = dg_PVH.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dg_PVH.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dg_PVH.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dg_PVH.CurrentCell = c;
        }

        private void advancedDataGridViewSearchToolBar_PVP_Search(object sender, Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs e)
        {
            bool restartsearch = true;
            int startColumn = 0;
            int startRow = 0;
            if (!e.FromBegin)
            {
                bool endcol = dg_PVP.CurrentCell.ColumnIndex + 1 >= dg_PVP.ColumnCount;
                bool endrow = dg_PVP.CurrentCell.RowIndex + 1 >= dg_PVP.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dg_PVP.CurrentCell.ColumnIndex;
                    startRow = dg_PVP.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dg_PVP.CurrentCell.ColumnIndex + 1;
                    startRow = dg_PVP.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dg_PVP.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dg_PVP.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dg_PVP.CurrentCell = c;
        }

        private void buttonVyhledat_PVH_Click(object sender, EventArgs e)
        {
            NastavDatagrid();
            //Load PVH
            if (providerPV is Fask.Interfaces.Vyroba.PV.IPV_FillPVH)
                ((Fask.Interfaces.Vyroba.PV.IPV_FillPVH)providerPV).FillPVH(this.ds);
            else
                throw new Exception("FillPVH not implementet");

            UpdatePVH();
        }

        private void buttonVyhledat_PVP_Click(object sender, EventArgs e)
        {
            NastavDatagrid();

            dataGridView1_SelectionChanged(null, null);
        }

        private void NastavDatagrid()
        {
            #region prace s datagridem - nastaveni numeric hodnot na pocet desetinnych mist
            try
            {
                // Procházení sloupců v DataGridView
                foreach (DataGridViewColumn column in dg_PVP.Columns)
                {
                    // Získání typu dat pro sloupec
                    Type dataType = null;

                    if (dg_PVP.DataSource is DataTable dataTable)
                    {
                        // Pokud je datový zdroj DataTable
                        if (dataTable.Columns.Contains(column.DataPropertyName))
                        {
                            dataType = dataTable.Columns[column.DataPropertyName].DataType;
                        }
                    }
                    else if (dg_PVP.DataSource is BindingSource bindingSource)
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

    }
}
