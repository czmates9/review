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
using System.IO;

namespace Konzola.Servis
{
    public partial class FormZdrojeStavyList : Form
    {
        /// <summary>
        /// Zvolené id před seřazením
        /// </summary>
        string selectedZdrojStavID = string.Empty;

        private Fask.Interfaces.IMES providerServis = null;
        //private Fask.Interfaces.DataSets.Konzola dsKonzola = new Fask.Interfaces.DataSets.Konzola();
        /// <summary>
        /// Uživatel, který data upravuje
        /// </summary>
        //public Fask.Interfaces.DataSets.Konzola.FASK_LoginsRow loginrow { get; set; }
        public Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojStavRow SelectedRow
        {
            get
            {
                try
                {
                    return ((DataRowView)(dgServis.BindingContext[bsServis].Current)).Row as Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojStavRow;

                }
                catch
                {
                    return null;
                }
            }
        }

        #region Eventy formu

        public FormZdrojeStavyList()
        {
            InitializeComponent();

            this.dgServis.UpdateColumnHeaderCellsByDatasource();
            panelButtons.Menu = menuStrip2;
        }

        private void FormZdrojeStavyList_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                this.WindowState = FormWindowState.Maximized;
                this.dgServis.LoadConfiguration(this.GetType().ToString());

                panelButtons.LoadConfiguration(this.GetType().ToString());
                panelButtons.Init();

                advancedDataGridViewSearchToolBar1.SetColumns(dgServis.Columns);

                // inicializace providera
                InitProvider();

                if (providerServis == null)
                    throw new Exception("Provider není inicializován");

                System.Threading.Thread loadThread = new System.Threading.Thread(LoadDataAsync);
                loadThread.IsBackground = true;
                loadThread.Start();
                //UpdateForm();
                //bindingSource1.DataSource = dsKonzola.FASK_Logins;

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormZdrojeStavyList_KeyDown(object sender, KeyEventArgs e)
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

        private void FormZdrojeStavyList_FormClosing(object sender, FormClosingEventArgs e)
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
        
        #endregion

        /// <summary>
        /// nacteni dat v jinem vlakne
        /// </summary>
        private void LoadDataAsync()
        {
            try
            {
                Fask.Interfaces.DataSets.Servis ds = new Fask.Interfaces.DataSets.Servis();

                ds = ((Fask.Interfaces.Servis.IServis)providerServis).GetZdrojeStavy();
                
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
        private void UpdateForm(Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojStavRow row)
        {
            try
            {
                string id = row != null ? row.IDZdroj : string.Empty;

                dsServis.Clear();
                dsServis = ((Fask.Interfaces.Servis.IServis)providerServis).GetZdrojeStavy();
                bsServis.DataSource = dsServis;

                try
                {
                    if (!string.IsNullOrEmpty(id))
                    {
                        int index = bsServis.Find(dsServis.CZMST_Servis_ZdrojStav.IDZdrojColumn.ColumnName, id);
                        this.bsServis.Position = index;
                    }
                }
                catch
                {
                }
                dgServis.Focus();
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

        private void PerformCreateRecord()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                using (Servis.FormZdrojeStavyEdit frmzdroje = new FormZdrojeStavyEdit())
                {
                    frmzdroje.Text = "Nový stav zdroje";
                    if (frmzdroje.ShowDialog(this) != DialogResult.OK)
                        return;

                    UpdateForm(frmzdroje.returnrow);
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PerformEditRecord()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (SelectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro úpravu", this.Text, MessageBoxButtons.OK);
                    return;
                }

                using (Servis.FormZdrojeStavyEdit frmzdroje = new FormZdrojeStavyEdit())
                {
                    frmzdroje.zdrojStavRow = SelectedRow;
                    frmzdroje.Text = "Úprava stavu zdroje";
                    if (frmzdroje.ShowDialog(this) != DialogResult.OK)
                        return;

                    UpdateForm(frmzdroje.returnrow);
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PerformDeleteRecord()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;
                
                if (SelectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro odstranění", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if (MessageBox.Show("Opravdu chcete odstranit zdroj '" + SelectedRow.IDZdroj.Trim()  + "'?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

                //bool odstranitNavaznosti = false;
                //if (MessageBox.Show("Chcete také odstranit návaznosti na vybraný zdroj '" + SelectedRow.Oznaceni.Trim() + "'?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                //    odstranitNavaznosti = true;

                //providerServis.DeleteZdroj(SelectedRow.ID, odstranitNavaznosti);
                bool result = ((Fask.Interfaces.Servis.IServis)providerServis).DeleteZdrojStavByZdrojID(SelectedRow.IDZdroj);

                UpdateForm(SelectedRow);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }                     
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1)
                {
                    if (SelectedRow != null)
                        selectedZdrojStavID = SelectedRow.IDZdroj;
                }
            }
            catch { }
        }

        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            try
            {
                int pos = this.bsServis.Find(dsServis.CZMST_Servis_ZdrojStav.IDZdrojColumn.ColumnName, selectedZdrojStavID);
                this.bsServis.Position = pos;
            }
            catch { }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
                PerformEditRecord();
        }

        private void tsmiReport_Click(object sender, EventArgs e)
        {
            // tisk report ...
            if (SelectedRow == null)
            {
                MessageBox.Show("Není vybrán záznam pro tisk", this.Text, MessageBoxButtons.OK);
                return;
            }

            var srow = SelectedRow;

            var zdrojrow = ((Fask.Interfaces.Servis.IServis)providerServis).GetZdrojByID(srow.IDZdroj);

            using (Print.FormPrintZdrojStav_Report fpzs = new Print.FormPrintZdrojStav_Report())
            {
                fpzs._ZdrojStavRow = srow;
                fpzs._ZdrojRow = zdrojrow;
                fpzs.ShowDialog(this);
            }
        }

        private void tsmiEtiketa_Click(object sender, EventArgs e)
        {
            //// tisk report ...
            //if (SelectedRow == null)
            //{
            //    MessageBox.Show("Není vybrán záznam pro tisk", this.Text, MessageBoxButtons.OK);
            //    return;
            //}

            //var srow = SelectedRow;

            //var zdrojrow = providerServis.GetZdrojByID(srow.IDZdroj);

            //using (Print.FormPrintZdrojStav_Etiketa fpzs = new Print.FormPrintZdrojStav_Etiketa())
            //{
            //    fpzs._ZdrojStavRow = srow;
            //    fpzs._ZdrojRow = zdrojrow;
            //    fpzs.ShowDialog(this);
            //}

            try
            {
                PrintDialog printDialog1 = new PrintDialog();
                printDialog1.UseEXDialog = true;

                if (printDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.Cancel)
                    return;

                // 1) pripravit zdroje
                Fask.Interfaces.DataSets.Servis dsServisSelected = new Fask.Interfaces.DataSets.Servis();
                foreach (DataGridViewRow row in this.dgServis.SelectedRows)
                {
                    DataRowView drv = this.bsServis[row.Index] as DataRowView;
                    var _ZdrojStavRow = drv.Row as Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojStavRow;
                    dsServisSelected.CZMST_Servis_ZdrojStav.ImportRow(_ZdrojStavRow);
                    var _ZdrojRow = ((Fask.Interfaces.Servis.IServis)providerServis).GetZdrojByID(_ZdrojStavRow.IDZdroj);
                    dsServisSelected.CZMST_Servis_Zdroj.ImportRow(_ZdrojRow);
                }

                string pocetStr = string.Empty;
                int pocetInt = 1;
                while (true)
                {
                    DialogResult drPocet = Konzola.Forms.InputBox.Show("Etiketa tisk", "Počet etiket zdroje k vytištění", pocetInt.ToString(), Forms.InputBox.TypeOfCode.NumericInt, true, 1, 99, false, out pocetStr);
                    if (drPocet == System.Windows.Forms.DialogResult.Cancel)
                        return;

                    try
                    {
                        pocetInt = int.Parse(pocetStr);
                    }
                    catch (Exception exPocet)
                    {
                        MessageBox.Show(exPocet.Message);
                        continue;
                    }

                    break; // vse ok ... 
                }

                // Test, zda je to Leitz ...
                if (_Printers_.PrinterLeitz.IsLeitz(printDialog1.PrinterSettings.PrinterName))
                { // je to leitz ... 
                    // => tisknout pomoci SDK ...
                    _Printers_.PrinterLeitz.Print_ZdrojStav(printDialog1.PrinterSettings.PrinterName, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Servis", "Print", "Templates", "Etiketa_ZdrojStav.LeitzLbl"), pocetInt, dsServisSelected);
                    return;
                }
                else
                { // je to neco jineho ...

                    string strPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Servis", "Print", "Templates", "Etiketa_ZdrojStav.txt");
                    System.IO.StreamReader sr = new System.IO.StreamReader(strPath);
                    string strData = sr.ReadToEnd();
                    sr.Close();

                    foreach (var _ZdrojStavRow in dsServisSelected.CZMST_Servis_ZdrojStav)
                    {
                        StringBuilder sbData = new StringBuilder();
                        sbData.Append(strData);

                        var _ZdrojRow = dsServisSelected.CZMST_Servis_Zdroj.FindByID(_ZdrojStavRow.IDZdroj);

                        sbData.Replace("$OkruhOznaceni$", _ZdrojStavRow.OkruhOznaceni);
                        sbData.Replace("$OdberatelOznaceni$", _ZdrojStavRow.OdberatelOznaceni);
                        sbData.Replace("$ZdrojOznaceni$", _ZdrojStavRow.ZdrojOznaceni);
                        sbData.Replace("$ZdrojBarcode$", _ZdrojRow.Barcode);

                        sbData.Replace("$Pocet$", pocetInt.ToString());
                        if (!Konzola._Support_.RawPrinterHelper.SendStringToPrinter(printDialog1.PrinterSettings.PrinterName, sbData.ToString()))
                        {
                            throw new Exception("Tisk etikety '" + _ZdrojStavRow.ZdrojOznaceni + "' se nezdařil");
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void tsmiOdstranit_Click(object sender, EventArgs e)
        {
            PerformDeleteRecord();
        }

        private void tsmiUpravit_Click(object sender, EventArgs e)
        {
            PerformEditRecord();
        }

        private void tsmiNovy_Click(object sender, EventArgs e)
        {
            PerformCreateRecord();
        }

        private void tsmiKonec_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void buttonVyhledat_Click(object sender, EventArgs e)
        {
            UpdateForm(SelectedRow);
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

        #region Exporty

        private void exportDoCSVVseToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exportDoCSVOznaceneToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exportDoExcelVseToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exportDoExceOznaceneToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exportDoXMLVseToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exportDoXMLOznaceneToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        #endregion

    }
}
