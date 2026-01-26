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

namespace Konzola.Ciselniky
{
    public partial class FormOdberateleSelect : Form
    {
        private Fask.Interfaces.IMES providerOdberatele = null;

        /// <summary>
        /// Zvolené id před seřazením
        /// </summary>
        string selectedCinnostID = string.Empty;

        /// <summary>
        /// Radek, ktery se predvoli (pri editaci zaznamu)
        /// </summary>
        private Fask.Interfaces.DataSets.Odberatele.CZMST090Row selectRow { get; set; }

        /// <summary>
        /// Vybrana cinnost.
        /// </summary>
        public Fask.Interfaces.DataSets.Odberatele.CZMST090Row SelectedRow
        {
            get
            {
                try
                {
                    return ((DataRowView)(dgOdberatele.BindingContext[bsOdberatele].Current)).Row as Fask.Interfaces.DataSets.Odberatele.CZMST090Row;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Kolekce veškerých zvolených sloupců.
        /// </summary>
        public DataGridViewSelectedRowCollection SelectedRows
        {
            get
            {
                try
                {
                    return dgOdberatele.SelectedRows;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        #region Eventy formu

        public FormOdberateleSelect(bool allowMultiSelect)
        {
            InitializeComponent();
            this.dgOdberatele.UpdateColumnHeaderCellsByDatasource();
            panelButtons.Menu = menuStrip2;
            this.dgOdberatele.MultiSelect = allowMultiSelect;
        }

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="allowMultiSelect">Povoleni oznaceni vice zaznamu.</param>
        /// <param name="selectRow">Radek, ktery se oznaci.</param>
        public FormOdberateleSelect(bool allowMultiSelect, Fask.Interfaces.DataSets.Odberatele.CZMST090Row selectRow)
            : this(allowMultiSelect)
        {
            this.selectRow = selectRow;
        }

        private void FormOdberateleSelect_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                //this.WindowState = FormWindowState.Maximized;
                this.dgOdberatele.LoadConfiguration(this.GetType().ToString());
                panelButtons.LoadConfiguration(this.GetType().ToString());
                panelButtons.Init();

                advancedDataGridViewSearchToolBar1.SetColumns(dgOdberatele.Columns);

                // inicializace providera
                InitProvider();

                System.Threading.Thread loadThread = new System.Threading.Thread(LoadDataAsync);
                loadThread.IsBackground = true;
                loadThread.Start();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormOdberateleSelect_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Shift && !e.Control && !e.Alt)
            {
                if (e.KeyCode == Keys.Escape)
                {
                    PerformCancel();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    PerformOK();
                }
                else
                    return;
            }
            else
                return;

            e.Handled = true;
        }

        private void FormOdberateleSelect_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.dgOdberatele.SaveConfiguration(this.GetType().ToString());
                panelButtons.SaveConfiguration(this.GetType().ToString());

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        #endregion

        /// <summary>
        /// nacteni dat v jinem vlakne
        /// </summary>
        private void LoadDataAsync()
        {
            try
            {
                Fask.Interfaces.DataSets.Odberatele ds = new Fask.Interfaces.DataSets.Odberatele();

                //ds = ((Fask.Interfaces.Ciselniky.IOdberatele)providerOdberatele).GetOdberatele();

                if ((providerOdberatele != null) && (providerOdberatele is Fask.Interfaces.Ciselniky.Odberatele.IOdberatele2_GetOdberatele))
                    ds = ((Fask.Interfaces.Ciselniky.Odberatele.IOdberatele2_GetOdberatele)providerOdberatele).GetOdberatele();
                else
                    throw new NotImplementedException("Provider neimplementuje IOdberatele2_GetOdberatele.");

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
        }

        private void PopulateUI(Fask.Interfaces.DataSets.Odberatele ds)
        {
            try
            {
                dsOdberatele.Clear();
                //dsServis = providerServis.GetZdroje();
                bsOdberatele.DataSource = ds;

                try
                {
                    // oznaceni vybraneho radku 
                    if (selectRow != null)
                    {
                        int index = bsOdberatele.Find(dsOdberatele.CZMST090.odb_idColumn.ColumnName, selectRow.odb_id);
                        this.bsOdberatele.Position = index;
                    }
                }
                catch { }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                    if (providerOdberatele == null) //inicializace se provede pouze pokud nebyla provedena ... 
                    {
                        //Assembly providerAssemlby = Assembly.LoadFrom(Server.MapPath(providerAssemblyPath));
                        Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                        Type[] types = providerAssemlby.GetTypes();
                        foreach (Type t in types)
                        {
                            try
                            {
                                //t.IsAssignableFrom(typeof(Fask.Server.Interfaces.Vydej.IVydej);
                                if (typeof(Fask.Interfaces.Ciselniky.Odberatele.IOdberatele2).IsAssignableFrom(t))
                                {
                                    providerOdberatele = (Fask.Interfaces.Ciselniky.Odberatele.IOdberatele2)providerAssemlby.CreateInstance(t.FullName);
                                    if (providerOdberatele != null)
                                        break;
                                }
                            }
                            catch { }
                        }
                        //return config;
                    }

                    providerOdberatele.InitProvider();

                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Aktualizace dat po aktualizaci.
        /// </summary>
        private void UpdateForm(Fask.Interfaces.DataSets.Odberatele.CZMST090Row row)
        {
            try
            {
                string id = row != null ? row.odb_id : string.Empty;

                dsOdberatele.Clear();
                //dsOdberatele = ((Fask.Interfaces.Ciselniky.IOdberatele)providerOdberatele).GetOdberatele();

                if ((providerOdberatele != null) && (providerOdberatele is Fask.Interfaces.Ciselniky.Odberatele.IOdberatele2_GetOdberatele))
                    dsOdberatele = ((Fask.Interfaces.Ciselniky.Odberatele.IOdberatele2_GetOdberatele)providerOdberatele).GetOdberatele();
                else
                    throw new NotImplementedException("Provider neimplementuje IOdberatele2_GetOdberatele.");


                bsOdberatele.DataSource = dsOdberatele;

                try
                {
                    if (!string.IsNullOrEmpty(id))
                    {
                        int index = bsOdberatele.Find(dsOdberatele.CZMST090.odb_idColumn.ColumnName, id);
                        this.bsOdberatele.Position = index;
                    }
                }
                catch
                {
                }
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
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonNovy_Click(object sender, EventArgs e)
        {
            PerformOK();            
        }

        private void PerformOK()
        {
            try
            {
                if (SelectedRow == null)
                {
                    MessageBox.Show("Není vybrán odběratel", this.Text, MessageBoxButtons.OK);
                    return;
                }

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //finally
            //{
            //}            
        }

        private void konecToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void obnovitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateForm(SelectedRow);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                    PerformOK();
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
                    if (SelectedRow != null)
                        selectedCinnostID = SelectedRow.odb_id;
                }
            }
            catch (Exception)
            {
            }
        }

        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            try
            {
                int pos = this.bsOdberatele.Find(dsOdberatele.CZMST090.odb_idColumn.ColumnName, selectedCinnostID);
                this.bsOdberatele.Position = pos;
            }
            catch { }
        }

        private void advancedDataGridViewSearchToolBar1_Search(object sender, Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs e)
        {

            bool restartsearch = true;
            int startColumn = 0;
            int startRow = 0;
            if (!e.FromBegin)
            {
                bool endcol = dgOdberatele.CurrentCell.ColumnIndex + 1 >= dgOdberatele.ColumnCount;
                bool endrow = dgOdberatele.CurrentCell.RowIndex + 1 >= dgOdberatele.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dgOdberatele.CurrentCell.ColumnIndex;
                    startRow = dgOdberatele.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dgOdberatele.CurrentCell.ColumnIndex + 1;
                    startRow = dgOdberatele.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dgOdberatele.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dgOdberatele.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dgOdberatele.CurrentCell = c;

        }
    }
}
