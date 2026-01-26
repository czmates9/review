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
    public partial class FormZdrojeSelect : Form
    {
        private Fask.Interfaces.IMES providerServis = null;

        /// <summary>
        /// Zvolené id před seřazením
        /// </summary>
        string selectedZdrojID = string.Empty;

        /// <summary>
        /// Radek, ktery se predvoli (pri editaci zaznamu)
        /// </summary>
        private Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojRow selectRow { get; set; }

        /// <summary>
        /// Vybrany zdroj.
        /// </summary>
        public Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojRow SelectedRow
        {
            get
            {
                try
                {
                    return ((DataRowView)(dgServis.BindingContext[bsServis].Current)).Row as Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojRow;
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Seznam vsech vybranych/oznacenych stavu.
        /// </summary>
        public DataGridViewSelectedRowCollection SelectedRows
        {
            get
            {
                try
                {
                    return dgServis.SelectedRows;
                }
                catch
                {
                    return null;
                }
            }
        }

        public FormZdrojeSelect(bool allowMultiSelect)
        {
            InitializeComponent();
            this.dgServis.UpdateColumnHeaderCellsByDatasource();
            panelButtons.Menu = menuStrip2;
            this.dgServis.MultiSelect = allowMultiSelect;            
        }

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="allowMultiSelect">Povoleni oznaceni vice zaznamu.</param>
        /// <param name="selectRow">Radek, ktery se oznaci.</param>
        public FormZdrojeSelect(bool allowMultiSelect, Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojRow selectRow) :this(allowMultiSelect)
        {
            this.selectRow = selectRow;
        }

        private void FormZdrojeSelect_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                //this.WindowState = FormWindowState.Maximized;
                this.dgServis.LoadConfiguration(this.GetType().ToString());
                panelButtons.LoadConfiguration(this.GetType().ToString());
                panelButtons.Init();

                advancedDataGridViewSearchToolBar1.SetColumns(dgServis.Columns);


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

        private void FormZdrojeSelect_KeyDown(object sender, KeyEventArgs e)
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

        private void FormZdrojeSelect_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.dgServis.SaveConfiguration(this.GetType().ToString());
                panelButtons.SaveConfiguration(this.GetType().ToString());


            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
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

                ds = ((Fask.Interfaces.Servis.IServis)providerServis).GetZdroje();

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

        private void PopulateUI(Fask.Interfaces.DataSets.Servis ds)
        {
            try
            {
                dsServis.Clear();
                //dsServis = providerServis.GetZdroje();
                bsServis.DataSource = ds;

                try
                {
                    // oznaceni vybraneho radku 
                    if (selectRow != null)
                    {
                        int index = bsServis.Find(dsServis.CZMST_Servis_Stav.IDColumn.ColumnName, selectRow.ID);
                        this.bsServis.Position = index;
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
        /// Aktualizace dat po aktualizaci.
        /// </summary>
        private void UpdateForm(Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojRow row)
        {
            try
            {
                string id = row != null ? row.ID : string.Empty;

                dsServis.Clear();
                dsServis = ((Fask.Interfaces.Servis.IServis)providerServis).GetZdroje();
                bsServis.DataSource = dsServis;

                try
                {
                    if (!string.IsNullOrEmpty(id))
                    {
                        int index = bsServis.Find(dsServis.CZMST_Servis_Zdroj.IDColumn.ColumnName, id);
                        this.bsServis.Position = index;
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

        private void PerformOK()
        {
            try
            {
                if (SelectedRow == null)
                {
                    MessageBox.Show("Není vybrán stav", this.Text, MessageBoxButtons.OK);
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
                        selectedZdrojID = SelectedRow.ID;
                }
            }
            catch { }
        }

        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            try
            {
                int pos = this.bsServis.Find(dsServis.CZMST_Servis_Zdroj.IDColumn.ColumnName, selectedZdrojID);
                this.bsServis.Position = pos;
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
                bool endcol = dgServis.CurrentCell.ColumnIndex + 1 >= dgServis.ColumnCount;
                bool endrow = dgServis.CurrentCell.RowIndex + 1 >= dgServis.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dgServis.CurrentCell.ColumnIndex;
                    startRow = dgServis.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dgServis.CurrentCell.ColumnIndex + 1;
                    startRow = dgServis.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dgServis.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dgServis.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dgServis.CurrentCell = c;
        }

        private void tsmiKonec_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void tsmiAktualizovat_Click(object sender, EventArgs e)
        {
            UpdateForm(SelectedRow);
        }

        private void tsmiVybrat_Click(object sender, EventArgs e)
        {
            PerformOK();
        }
    }
}
