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

namespace Konzola
{
    public partial class FormPotrebaMaterialu : Form
    {

        #region Parametry

        private Fask.Interfaces.IMES provider_Mat = null;
        public Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHDataTable dt_VPH { get; set; }

        private List<Fask.Interfaces.Filtry.PotrebaMaterialu_Filtr> filtry_Materialy = new List<Fask.Interfaces.Filtry.PotrebaMaterialu_Filtr>();


        private Fask.Interfaces.Filtry.PotrebaMaterialu_Filtr rowFiltr_PotMat
        {
            get
            {
                try
                {
                    return tscbFiltry.SelectedItem as Fask.Interfaces.Filtry.PotrebaMaterialu_Filtr;
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
                    return dg_PotMat.SelectedRows;
                }
                catch
                {
                    return null;
                }
            }
        }
        #endregion

        #region c'tor + Load + eventy Formu

        public FormPotrebaMaterialu()
        {
            InitializeComponent();

            this.dg_PotMat.UpdateColumnHeaderCellsByDatasource();
            this.dg_PotMat_JenSoucty.UpdateColumnHeaderCellsByDatasource();

            panelButtonsZobrazeniList.Menu = menuStrip2;

        }

        private void FormUzivateleList_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                //this.WindowState = FormWindowState.Maximized;
                this.dg_PotMat.LoadConfiguration(this.GetType().ToString() + "_PotMat");
                this.dg_PotMat_JenSoucty.LoadConfiguration(this.GetType().ToString() + "_PotMat_JenSoucty");

                advancedDataGridViewSearchToolBar_PotMat.SetColumns(dg_PotMat.Columns);
                advancedDataGridViewSearchToolBar2.SetColumns(dg_PotMat_JenSoucty.Columns);

                panelButtonsZobrazeniList.LoadConfiguration(this.GetType().ToString());
                panelButtonsZobrazeniList.Init();


                InitProvider();

                if (provider_Mat == null)
                    throw new Exception("Provider 'Vyrobky' není inicializován");

                PerformVyhledat();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormUzivateleList_KeyDown(object sender, KeyEventArgs e)
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


        private void FormZboziSelect_Shown(object sender, EventArgs e)
        {
            try
            {
                this.progressIndicator1.Size = new Size(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ostatni[0].ProgressIndicatorSize, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ostatni[0].ProgressIndicatorSize);
                this.progressIndicator1.Location = new Point(this.dg_PotMat.Location.X + (this.dg_PotMat.Width / 2) - (progressIndicator1.Size.Width / 2), this.dg_PotMat.Location.Y + (this.dg_PotMat.Height / 2) - (progressIndicator1.Size.Height / 2));
            }
            catch { }
        }


        #endregion

        #region Eventy


        private void konecToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void FormUzivateleList_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.dg_PotMat.SaveConfiguration(this.GetType().ToString() + "_PotMat");
                this.dg_PotMat_JenSoucty.SaveConfiguration(this.GetType().ToString() + "_PotMat_JenSoucty");

                panelButtonsZobrazeniList.SaveConfiguration(this.GetType().ToString());

                WaithToEndThread();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void buttonVyhledat_Click(object sender, EventArgs e)
        {
            PerformVyhledat();
        }

        #region Filtre

        private void tsbVycistit_Click(object sender, EventArgs e)
        {
            PerformVycistitFiltr();
        }

        private void tsbZmena_Click(object sender, EventArgs e)
        {
            PerformZmenitFiltr();
        }

        private void tsbPridat_Click(object sender, EventArgs e)
        {
            PerformPridatFiltr();
        }

        private void tsbOdebrat_Click(object sender, EventArgs e)
        {
            PerformOdebratFiltr();
        }

        private void tsbNastavit_Click(object sender, EventArgs e)
        {
            PerformNastavitFiltr(rowFiltr_PotMat);
        }

        #endregion

        #endregion

        #region Protected metody

        /// <summary>
        /// Inicializace providera
        /// </summary>
        public void InitProvider()
        {
            #region Parametry
            try
            {
                if (String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                    return;

                if (provider_Mat == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Parametry.IParametry2).IsAssignableFrom(t))
                            {
                                provider_Mat = (Fask.Interfaces.Parametry.IParametry2)providerAssemlby.CreateInstance(t.FullName);
                                if (provider_Mat != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                provider_Mat.InitProvider();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion
        }

        private bool CreateFilter(ref Fask.Interfaces.Filtry.PotrebaMaterialu_Filtr filtr)
        {
            //implementace filtru

            //filtr.Jen_Soucty = cb_JenSoucty.Checked;

            return true;
        }

        /// <summary>
        /// Metoda na čekani dobehnuti vlakna
        /// </summary>
        public virtual void WaithToEndThread()
        {
            //// cekani na dobehnuti vlakna
            try
            {
                if (bw_PotMat.IsBusy)
                {
                    bw_PotMat.CancelAsync();
                    while (bw_PotMat.IsBusy)
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


        /// <summary>
        /// Vyhledani podle zadaneho filtru.
        /// </summary>
        public virtual void PerformVyhledat()
        {
            try
            {
                if (bw_PotMat.IsBusy)
                {
                    bw_PotMat.CancelAsync();
                    while (bw_PotMat.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }

                ProgressIndicatorStart();

                Fask.Interfaces.Filtry.PotrebaMaterialu_Filtr filtr = new Fask.Interfaces.Filtry.PotrebaMaterialu_Filtr();
                if (!CreateFilter(ref filtr))
                    return;

                int FirstDisplayedScrollingRowIndex = this.dg_PotMat.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

                bw_PotMat.RunWorkerAsync(filtr);

                if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dg_PotMat.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dg_PotMat.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index


            }
            catch (Exception ex)
            {
                ProgressIndicatorStop();
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Filtry

        /// <summary>
        /// Odstrani vybrany filtr
        /// </summary>
        public void PerformOdebratFiltr()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (rowFiltr_PotMat == null)
                    return;

                DialogResult dr = MessageBox.Show("Opravdu chcete odstranit filtr '" + (string.IsNullOrEmpty(rowFiltr_PotMat.NazevFiltru) ? string.Empty : rowFiltr_PotMat.NazevFiltru.Trim()) + "'?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr != System.Windows.Forms.DialogResult.Yes)
                    return;

                filtry_Materialy.Remove(rowFiltr_PotMat);
                this.tscbFiltry.ComboBox.DataSource = null;
                this.tscbFiltry.ComboBox.DataSource = filtry_Materialy;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show("Nepodařilo se odstranit filtr.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Ulozeni filtru do souboru.
        /// </summary>
        public void PerformPridatFiltr()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                Fask.Interfaces.Filtry.PotrebaMaterialu_Filtr filtr = new Fask.Interfaces.Filtry.PotrebaMaterialu_Filtr();
                string nazev = string.Empty;
                DialogResult dr = Forms.InputBox.Show("Název filtru", "Zadejte název filtru", string.Empty, false, out nazev);
                if (dr != System.Windows.Forms.DialogResult.OK)
                    return;

                bool result = CreateFilter(ref filtr);
                if (result)
                {
                    filtr.NazevFiltru = nazev;
                    filtry_Materialy.Add(filtr);
                    //this.tscbFiltry.Items.Clear();
                    this.tscbFiltry.ComboBox.DataSource = null;
                    this.tscbFiltry.ComboBox.DataSource = filtry_Materialy;
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

        /// <summary>
        /// Upravi zvoleny filtr.
        /// </summary>
        public void PerformZmenitFiltr()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (rowFiltr_PotMat == null)
                    return;

                DialogResult dr = MessageBox.Show("Opravdu chcete změnit vybraný filtr '" + rowFiltr_PotMat.NazevFiltru.Trim() + "'?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr != System.Windows.Forms.DialogResult.Yes)
                    return;

                Fask.Interfaces.Filtry.PotrebaMaterialu_Filtr filtr = rowFiltr_PotMat;

                if (!CreateFilter(ref filtr))
                    return;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show("Nepodařilo se upravit filtr.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Vyčistí filtr
        /// </summary>
        public void PerformVycistitFiltr()
        {
            try
            {
                //cb_JenSoucty.Checked = false;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Nastavit Filter
        /// </summary>
        public void PerformNastavitFiltr(Fask.Interfaces.Filtry.PotrebaMaterialu_Filtr filtr)
        {
            try
            {
                if (filtr == null)
                    return;

                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                PerformVycistitFiltr();

                // TODO naplneni comboboxu


                //cb_JenSoucty.Checked = filtr.Jen_Soucty;


            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show("Nepodařilo se načíst data filtru.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion


        #endregion

        #region Ostatni metody

        protected void PerformCancel()
        {
            try
            {
                this.DialogResult = DialogResult.Cancel;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected void ProgressIndicatorStop()
        {
            progressIndicator1.Stop();
            progressIndicator1.Visible = false;

            progressIndicator2.Stop();
            progressIndicator2.Visible = false;
        }

        protected void ProgressIndicatorStart()
        {
            try
            {
                // prepocet stredu datagridu
                this.progressIndicator1.Location = new Point(this.dg_PotMat.Location.X + (this.dg_PotMat.Width / 2) - (progressIndicator1.Size.Width / 2), this.dg_PotMat.Location.Y + (this.dg_PotMat.Height / 2) - (progressIndicator1.Size.Height / 2));

                this.progressIndicator2.Location = new Point(this.dg_PotMat_JenSoucty.Location.X + (this.dg_PotMat_JenSoucty.Width / 2) - (progressIndicator2.Size.Width / 2), this.dg_PotMat_JenSoucty.Location.Y + (this.dg_PotMat_JenSoucty.Height / 2) - (progressIndicator2.Size.Height / 2));
            }
            catch { }
            progressIndicator1.Start();
            progressIndicator1.Visible = true;

            progressIndicator2.Start();
            progressIndicator2.Visible = true;
        }

        #endregion

        #region Exporty

        private void exportDoCSVVseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (tabControl1.SelectedIndex == tabControl1.TabPages.IndexOf(tabPage_Prehled))
                {
                    this.dg_PotMat.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
                }
                else if (tabControl1.SelectedIndex == tabControl1.TabPages.IndexOf(tabPage_Soucty))
                {
                    this.dg_PotMat_JenSoucty.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
                }

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


                if (tabControl1.SelectedIndex == tabControl1.TabPages.IndexOf(tabPage_Prehled))
                {
                    this.dg_PotMat.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
                }
                else if (tabControl1.SelectedIndex == tabControl1.TabPages.IndexOf(tabPage_Soucty))
                {
                    this.dg_PotMat_JenSoucty.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
                }

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


                if (tabControl1.SelectedIndex == tabControl1.TabPages.IndexOf(tabPage_Prehled))
                {
                    this.dg_PotMat.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
                }
                else if (tabControl1.SelectedIndex == tabControl1.TabPages.IndexOf(tabPage_Soucty))
                {
                    this.dg_PotMat_JenSoucty.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
                }

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


                if (tabControl1.SelectedIndex == tabControl1.TabPages.IndexOf(tabPage_Prehled))
                {
                    this.dg_PotMat.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
                }
                else if (tabControl1.SelectedIndex == tabControl1.TabPages.IndexOf(tabPage_Soucty))
                {
                    this.dg_PotMat_JenSoucty.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
                }
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


                if (tabControl1.SelectedIndex == tabControl1.TabPages.IndexOf(tabPage_Prehled))
                {
                    this.dg_PotMat.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
                }
                else if (tabControl1.SelectedIndex == tabControl1.TabPages.IndexOf(tabPage_Soucty))
                {
                    this.dg_PotMat_JenSoucty.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
                }
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


                if (tabControl1.SelectedIndex == tabControl1.TabPages.IndexOf(tabPage_Prehled))
                {
                    this.dg_PotMat.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
                }
                else if (tabControl1.SelectedIndex == tabControl1.TabPages.IndexOf(tabPage_Soucty))
                {
                    this.dg_PotMat_JenSoucty.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #endregion

        private void advancedDataGridViewSearchToolBar1_Search(object sender, Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs e)
        {
            bool restartsearch = true;
            int startColumn = 0;
            int startRow = 0;
            if (!e.FromBegin)
            {
                bool endcol = dg_PotMat.CurrentCell.ColumnIndex + 1 >= dg_PotMat.ColumnCount;
                bool endrow = dg_PotMat.CurrentCell.RowIndex + 1 >= dg_PotMat.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dg_PotMat.CurrentCell.ColumnIndex;
                    startRow = dg_PotMat.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dg_PotMat.CurrentCell.ColumnIndex + 1;
                    startRow = dg_PotMat.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dg_PotMat.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dg_PotMat.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dg_PotMat.CurrentCell = c;
        }

        private void advancedDataGridViewSearchToolBar2_Search(object sender, Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs e)
        {
            bool restartsearch = true;
            int startColumn = 0;
            int startRow = 0;
            if (!e.FromBegin)
            {
                bool endcol = dg_PotMat_JenSoucty.CurrentCell.ColumnIndex + 1 >= dg_PotMat_JenSoucty.ColumnCount;
                bool endrow = dg_PotMat_JenSoucty.CurrentCell.RowIndex + 1 >= dg_PotMat_JenSoucty.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dg_PotMat_JenSoucty.CurrentCell.ColumnIndex;
                    startRow = dg_PotMat_JenSoucty.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dg_PotMat_JenSoucty.CurrentCell.ColumnIndex + 1;
                    startRow = dg_PotMat_JenSoucty.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dg_PotMat_JenSoucty.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dg_PotMat_JenSoucty.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dg_PotMat_JenSoucty.CurrentCell = c;
        }


        private void bw_PotMat_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {

                Fask.Interfaces.Filtry.PotrebaMaterialu_Filtr filtr = (Fask.Interfaces.Filtry.PotrebaMaterialu_Filtr)e.Argument;

                if (bw_PotMat.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                Fask.Interfaces.DataSets.Vyroba ds = null;

                if (provider_Mat is Fask.Interfaces.Vyroba.VPH.IVPH_GetPotrebaMaterialu)
                    ds = ((Fask.Interfaces.Vyroba.VPH.IVPH_GetPotrebaMaterialu)provider_Mat).GetPotrebaMaterialu(dt_VPH, filtr, FASK.Logins.Uzivatel.Instance.UserID);
                else
                    throw new Exception("IVPH_GetPotrebaMaterialu not implementet");



                if (bw_PotMat.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }


                e.Result = ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void bw_PotMat_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            try
            {
                // check error, check cancel, then use result
                if (e.Error != null)
                {
                    // handle the error
                    ds_Vyroba = new Fask.Interfaces.DataSets.Vyroba();
                    bs_PotMat.DataSource = ds_Vyroba;
                    bs_PotMat_JenSoucet.DataSource = ds_Vyroba;

                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, e.Error);
                    MessageBox.Show(e.Error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (e.Cancelled)
                {
                    // handle cancellation
                    ds_Vyroba = new Fask.Interfaces.DataSets.Vyroba();
                    bs_PotMat.DataSource = ds_Vyroba;
                    bs_PotMat_JenSoucet.DataSource = ds_Vyroba;
                }
                else
                {
                    // uspesne dokonceno ...
                    // use it on the UI thread
                    ds_Vyroba = (Fask.Interfaces.DataSets.Vyroba)e.Result;
                    if (ds_Vyroba == null)
                        ds_Vyroba = new Fask.Interfaces.DataSets.Vyroba();



                    //TaD 13.9.2019 dle JaS upravavene razeni podle ITEMNMBR_MAT

                    //var dt_tmp = ds_Vyroba.VPH_PotrebaMaterialu.OrderBy(x => x.ITEMNMBR_MAT);
                    //ds_Vyroba.VPH_PotrebaMaterialu.Clear();
                    //foreach (Fask.Interfaces.DataSets.Vyroba.VPH_PotrebaMaterialuRow item in dt_tmp)
                    //{
                    //    ds_Vyroba.VPH_PotrebaMaterialu.ImportRow(item);
                    //}

                    Fask.Interfaces.DataSets.Vyroba.VPH_PotrebaMaterialuDataTable ds_Vyroba_Copy = (Fask.Interfaces.DataSets.Vyroba.VPH_PotrebaMaterialuDataTable)ds_Vyroba.VPH_PotrebaMaterialu.Copy();
                    ds_Vyroba.VPH_PotrebaMaterialu.Clear();
                    foreach (Fask.Interfaces.DataSets.Vyroba.VPH_PotrebaMaterialuRow item in ds_Vyroba_Copy.OrderBy(i => i.ITEMNMBR_MAT))
                    {
                        ds_Vyroba.VPH_PotrebaMaterialu.ImportRow(item);
                    }


                    bs_PotMat.DataSource = ds_Vyroba;




                    ds_Vyroba.VPH_PotrebaMaterialu_JenSoucet.Clear();
                    //var GrupData = ds_Vyroba.VPH_PotrebaMaterialu.GroupBy(item => item.ITEMNMBR_MAT).Select(group => group.Sum(item => item.QTYSHPPD));

                    //ds_Vyroba.VPH_PotrebaMaterialu_JenSoucet
                    //var grupdata = ds_Vyroba.VPH_PotrebaMaterialu.GroupBy(i => i.ITEMNMBR_MAT).Select(p => new { ITEMNMBR_MAT = p.Key, QTYSHPPD = p.Sum( item => item.QTYSHPPD) });
                    //var grupdata = ds_Vyroba.VPH_PotrebaMaterialu.GroupBy(i => i.ITEMNMBR_MAT)
                    //    .Select(p =>
                    //        ds_Vyroba.VPH_PotrebaMaterialu_JenSoucet.AddVPH_PotrebaMaterialu_JenSoucetRow(p.Key, p.Sum( item => item.QTYSHPPD ))
                    //    );

                    var grupdata = ds_Vyroba.VPH_PotrebaMaterialu.GroupBy(i => new { i.ITEMNMBR_MAT, i.ITEMNAME_MAT, i.ITEMCODE_MAT, i.MJ_MAT, i.QTY_POHODA });
                    grupdata.ToList().ForEach(x =>
                    {
                        ds_Vyroba.VPH_PotrebaMaterialu_JenSoucet.AddVPH_PotrebaMaterialu_JenSoucetRow(
                            x.Key.ITEMNMBR_MAT,
                            x.Sum(item => item.QTYSHPPD),
                            x.Key.ITEMNAME_MAT,
                            x.Key.ITEMCODE_MAT,
                            x.Key.MJ_MAT,
                            x.Key.QTY_POHODA,
                            x.Key.QTY_POHODA - x.Sum(item => item.QTYSHPPD)
                            );
                    });

                    bs_PotMat_JenSoucet.DataSource = ds_Vyroba;

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

        private void dg_PotMat_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewRow row in dg_PotMat.Rows)
            {
                Fask.Interfaces.DataSets.Vyroba.VPH_PotrebaMaterialuRow radek = (Fask.Interfaces.DataSets.Vyroba.VPH_PotrebaMaterialuRow)((DataRowView)row.DataBoundItem).Row;

                if (radek.QTY_ROZDIL < 0)
                {
                    row.DefaultCellStyle.BackColor = Color.Crimson;
                }

            }
        }

        private void dg_PotMat_JenSoucty_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewRow row in dg_PotMat_JenSoucty.Rows)
            {
                Fask.Interfaces.DataSets.Vyroba.VPH_PotrebaMaterialu_JenSoucetRow radek = (Fask.Interfaces.DataSets.Vyroba.VPH_PotrebaMaterialu_JenSoucetRow)((DataRowView)row.DataBoundItem).Row;

                if (radek.QTY_ROZDIL < 0)
                {
                    row.DefaultCellStyle.BackColor = Color.Crimson;
                }
            }
        }

        private void dg_PotMat_SelectionChanged(object sender, EventArgs e)
        {
            this.dg_PotMat.ClearSelection();
        }

        private void dg_PotMat_JenSoucty_SelectionChanged(object sender, EventArgs e)
        {
            this.dg_PotMat_JenSoucty.ClearSelection();
        }
    }
}
