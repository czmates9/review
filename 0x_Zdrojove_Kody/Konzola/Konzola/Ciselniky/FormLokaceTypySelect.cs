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
    public partial class FormLokaceTypySelect : Form
    {
        private Fask.Interfaces.IMES providerTypy = null;

        /// <summary>
        /// Zvolené id před seřazením
        /// </summary>
        string selectedCinnostID = string.Empty;

        /// <summary>
        /// Radek, ktery se predvoli (pri editaci zaznamu)
        /// </summary>
        private Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_LokaceTypyRow selectRow { get; set; }

        /// <summary>
        /// Vybrana cinnost.
        /// </summary>
        public Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_LokaceTypyRow SelectedRow
        {
            get
            {
                try
                {
                    return ((DataRowView)(dgSkladLokace.BindingContext[bsSkladLokace].Current)).Row as Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_LokaceTypyRow;
                }
                catch
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
                    return dgSkladLokace.SelectedRows;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        #region Eventy formu

        public FormLokaceTypySelect(bool allowMultiSelect)
        {
            InitializeComponent();

            this.dgSkladLokace.UpdateColumnHeaderCellsByDatasource();
            panelButtons.Menu = menuStrip2;
            this.dgSkladLokace.MultiSelect = allowMultiSelect;
        }

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="allowMultiSelect">Povoleni oznaceni vice zaznamu.</param>
        /// <param name="selectRow">Radek, ktery se oznaci.</param>
        public FormLokaceTypySelect(bool allowMultiSelect, Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_LokaceTypyRow selectRow)
            : this(allowMultiSelect)
        {
            this.selectRow = selectRow;
        }

        private void FormLokaceTypySelect_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                //this.WindowState = FormWindowState.Maximized;
                this.dgSkladLokace.LoadConfiguration(this.GetType().ToString());
                panelButtons.LoadConfiguration(this.GetType().ToString());
                panelButtons.Init();

                advancedDataGridViewSearchToolBar1.SetColumns(dgSkladLokace.Columns);

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

        private void FormLokaceTypySelect_KeyDown(object sender, KeyEventArgs e)
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

        private void FormLokaceTypySelect_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.dgSkladLokace.SaveConfiguration(this.GetType().ToString());
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
                Fask.Interfaces.DataSets.SkladLokace ds = new Fask.Interfaces.DataSets.SkladLokace();

                //ds = ((Fask.Interfaces.Ciselniky.ISkladLokace_LokaceTypy)providertypLokace).GetSkladLokace_LokaceTypy();

                if ((providerTypy != null) && (providerTypy is Fask.Interfaces.Ciselniky.SkladLokace_LokaceTypy.ISkladLokace_LokaceTypy2_GetSkladLokace_LokaceTypy))
                    ds = ((Fask.Interfaces.Ciselniky.SkladLokace_LokaceTypy.ISkladLokace_LokaceTypy2_GetSkladLokace_LokaceTypy)providerTypy).GetSkladLokace_LokaceTypy();
                else
                    throw new NotImplementedException("Provider neimplementuje ISkladLokace_LokaceTypy2_GetSkladLokace_LokaceTypy.");


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

        private void PopulateUI(Fask.Interfaces.DataSets.SkladLokace ds)
        {
            try
            {
                dsSkladLokace.Clear();
                //dsServis = providerServis.GetZdroje();
                bsSkladLokace.DataSource = ds;

                try
                {
                    // oznaceni vybraneho radku 
                    if (selectRow != null)
                    {
                        int index = bsSkladLokace.Find(dsSkladLokace.CZMST_SkladLokace_LokaceTypy.TYPEColumn.ColumnName, selectRow.TYPE);
                        this.bsSkladLokace.Position = index;
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
                    if (providerTypy == null) //inicializace se provede pouze pokud nebyla provedena ... 
                    {
                        //Assembly providerAssemlby = Assembly.LoadFrom(Server.MapPath(providerAssemblyPath));
                        Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                        Type[] types = providerAssemlby.GetTypes();
                        foreach (Type t in types)
                        {
                            try
                            {
                                //t.IsAssignableFrom(typeof(Fask.Server.Interfaces.Vydej.IVydej);
                                if (typeof(Fask.Interfaces.Ciselniky.SkladLokace_LokaceTypy.ISkladLokace_LokaceTypy2).IsAssignableFrom(t))
                                {
                                    providerTypy = (Fask.Interfaces.Ciselniky.SkladLokace_LokaceTypy.ISkladLokace_LokaceTypy2)providerAssemlby.CreateInstance(t.FullName);
                                    if (providerTypy != null)
                                        break;
                                }
                            }
                            catch { }
                        }
                        //return config;
                    }

                    providerTypy.InitProvider();

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
        private void UpdateForm(Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_LokaceTypyRow row)
        {
            try
            {
                string id = (row != null && !row.IsTYPENull() )? row.TYPE : string.Empty;

                dsSkladLokace.Clear();
                //dsSkladLokace = ((Fask.Interfaces.Ciselniky.ISkladLokace_LokaceTypy)providertypLokace).GetSkladLokace_LokaceTypy();

                if ((providerTypy != null) && (providerTypy is Fask.Interfaces.Ciselniky.SkladLokace_LokaceTypy.ISkladLokace_LokaceTypy2_GetSkladLokace_LokaceTypy))
                    dsSkladLokace = ((Fask.Interfaces.Ciselniky.SkladLokace_LokaceTypy.ISkladLokace_LokaceTypy2_GetSkladLokace_LokaceTypy)providerTypy).GetSkladLokace_LokaceTypy();
                else
                    throw new NotImplementedException("Provider neimplementuje ISkladLokace_LokaceTypy2_GetSkladLokace_LokaceTypy.");

                bsSkladLokace.DataSource = dsSkladLokace;

                try
                {
                    if (!string.IsNullOrEmpty(id))
                    {
                        int index = bsSkladLokace.Find(dsSkladLokace.CZMST_SkladLokace_LokaceTypy.TYPEColumn.ColumnName, id);
                        this.bsSkladLokace.Position = index;
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
                        selectedCinnostID = SelectedRow.TYPE;
                }
            }
            catch
            {
            }
        }

        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            try
            {
                int pos = this.bsSkladLokace.Find(dsSkladLokace.CZMST_SkladLokace_LokaceTypy.TYPEColumn.ColumnName, selectedCinnostID);
                this.bsSkladLokace.Position = pos;
            }
            catch { }
        }

        private void vybratToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        private void advancedDataGridViewSearchToolBar1_Search(object sender, Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs e)
        {

            bool restartsearch = true;
            int startColumn = 0;
            int startRow = 0;
            if (!e.FromBegin)
            {
                bool endcol = dgSkladLokace.CurrentCell.ColumnIndex + 1 >= dgSkladLokace.ColumnCount;
                bool endrow = dgSkladLokace.CurrentCell.RowIndex + 1 >= dgSkladLokace.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dgSkladLokace.CurrentCell.ColumnIndex;
                    startRow = dgSkladLokace.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dgSkladLokace.CurrentCell.ColumnIndex + 1;
                    startRow = dgSkladLokace.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dgSkladLokace.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dgSkladLokace.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dgSkladLokace.CurrentCell = c;

        }

    }
}
