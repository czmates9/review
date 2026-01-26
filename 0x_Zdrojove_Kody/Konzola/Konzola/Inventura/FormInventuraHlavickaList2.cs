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

namespace Konzola.Inventura
{
    public partial class FormInventuraHlavickaList2 : Form
    {
        private Fask.Interfaces.IMES providerSklady = null;
        private Fask.Interfaces.IMES providerInventura = null;

        private Fask.Interfaces.Classes.ZOBRAZENI_TYP _zobrazeni = Fask.Interfaces.Classes.ZOBRAZENI_TYP.UNKNOWN;
        public Fask.Interfaces.Classes.ZOBRAZENI_TYP Zobrazeni
        {
            get
            {
                return _zobrazeni;
            }
            set
            {
                if (_zobrazeni != value)
                {
                    _zobrazeni = value;
                    switch (_zobrazeni)
                    {
                        case Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST:
                            panelButtonsZobrazeniVyber.Hide();
                            panelButtonsZobrazeniList.Show();
                            menuStrip2.Items.Remove(tsmiMenuVyber);
                            tsFiltry.Visible = true;
                            tsFiltry.Enabled = true;
                            break;
                        case Fask.Interfaces.Classes.ZOBRAZENI_TYP.VYBER:
                            panelButtonsZobrazeniList.Hide();
                            panelButtonsZobrazeniVyber.Show();
                            menuStrip2.Items.Remove(tsmiMenuList);
                            menuStrip2.Items.Remove(tsmiAkce);
                            menuStrip2.Items.Remove(tsmiPolozka);
                            tsFiltry.Visible = false;
                            tsFiltry.Enabled = false;
                            break;
                        default:
                            break;
                    }
                }
            }
        }


        private List<Fask.Interfaces.Filtry.InventuraPredlohaListFiltr> filtry = new List<Fask.Interfaces.Filtry.InventuraPredlohaListFiltr>();
        private Fask.Interfaces.DataSets.Sklady dsSklady = new Fask.Interfaces.DataSets.Sklady();

        /// <summary>
        /// Zvolené id před seřazením
        /// </summary>
        string sortedID = string.Empty;

        public Fask.Interfaces.DataSets.Inventura.CZMST_I1HRow SelectedRow
        {
            get
            {
                try
                {
                    return ((DataRowView)(dg_IH.BindingContext[bs_IH].Current)).Row as Fask.Interfaces.DataSets.Inventura.CZMST_I1HRow;
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Vybrany filtr.
        /// </summary>
        private Fask.Interfaces.Filtry.InventuraPredlohaListFiltr rowFiltr
        {
            get
            {
                try
                {
                    return tscbFiltry.SelectedItem as Fask.Interfaces.Filtry.InventuraPredlohaListFiltr;
                }
                catch
                {
                    return null;
                }
            }
        }



        public FormInventuraHlavickaList2(bool allowMultiSelect, Fask.Interfaces.Classes.ZOBRAZENI_TYP typZobrazeni)
        {
            InitializeComponent();
            this.dg_IH.UpdateColumnHeaderCellsByDatasource();
            this.dg_IH.MultiSelect = allowMultiSelect;
            this.Zobrazeni = typZobrazeni;
            if (Zobrazeni == Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST)
            {
                this.WindowState = FormWindowState.Maximized;
                panelButtonsZobrazeniList.Menu = menuStrip2;
            }
            else if (Zobrazeni == Fask.Interfaces.Classes.ZOBRAZENI_TYP.VYBER)
            {
                panelButtonsZobrazeniVyber.Menu = menuStrip2;
            }
        }

        private void FormInventuraHlavickaList2_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                //this.WindowState = FormWindowState.Maximized;
                this.dg_IH.LoadConfiguration(this.GetType().ToString());

                if (Zobrazeni == Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST)
                {
                    panelButtonsZobrazeniList.LoadConfiguration(this.GetType().ToString());
                    panelButtonsZobrazeniList.Init();
                }
                else if (Zobrazeni == Fask.Interfaces.Classes.ZOBRAZENI_TYP.VYBER)
                {
                    panelButtonsZobrazeniVyber.LoadConfiguration(this.GetType().ToString());
                    panelButtonsZobrazeniVyber.Init();
                }

                advancedDataGridViewSearchToolBar1.SetColumns(dg_IH.Columns);


                //// načtení konfigurace vytvořených filtrů
                //this.filtry = Extensions.ListExt.ReadFromXML<Fask.Interfaces.Filtry.ExpediceFormDodaciListyListFiltr>(this.GetType().ToString() + ".filtr");
                tscbFiltry.ComboBox.DataSource = this.filtry;
                tscbFiltry.SelectedItem = null;
                tscbFiltry.ComboBox.DropDownWitdhAutosize();
                // inicializace providera
                InitProvider();

                if (providerInventura == null)
                    throw new Exception("Provider 'Inventura' není inicializován");

                if (providerSklady == null)
                    throw new Exception("Provider 'Sklady' není inicializován");

                // 13.7.2016 PeV: jiz se nepouziva, predelano na backgroundworker
                //System.Threading.Thread loadThread = new System.Threading.Thread(LoadDataAsync);
                //loadThread.IsBackground = true;
                //loadThread.Start();
                PerformVyhledat();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormInventuraHlavickaList2_KeyDown(object sender, KeyEventArgs e)
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

        private void FormInventuraHlavickaList2_Shown(object sender, EventArgs e)
        {
            try
            {
                this.progressIndicator1.Size = new Size(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ostatni[0].ProgressIndicatorSize, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ostatni[0].ProgressIndicatorSize);
                this.progressIndicator1.Location = new Point(this.dg_IH.Location.X + (this.dg_IH.Width / 2) - (progressIndicator1.Size.Width / 2), this.dg_IH.Location.Y + (this.dg_IH.Height / 2) - (progressIndicator1.Size.Height / 2));
            }
            catch { }
        }

        private void FormInventuraHlavickaList2_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.dg_IH.SaveConfiguration(this.GetType().ToString());

                if (Zobrazeni == Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST)
                { 
                    panelButtonsZobrazeniList.SaveConfiguration(this.GetType().ToString()); 
                }
                else if (Zobrazeni == Fask.Interfaces.Classes.ZOBRAZENI_TYP.VYBER)
                { 
                    panelButtonsZobrazeniVyber.SaveConfiguration(this.GetType().ToString()); 
                }

                // ulozeni vsech filtru
                this.filtry.WriteXML(this.GetType().ToString() + ".filtr");

                // cekani na dobehnuti vlakna
                try
                {
                    if (bwIH.IsBusy)
                    {
                        bwIH.CancelAsync();
                        while (bwIH.IsBusy)
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

        /// <summary>
        /// Inicializace providera
        /// </summary>
        private void InitProvider()
        {
            try
            {
                if (!String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                {
                    if (providerInventura == null) //inicializace se provede pouze pokud nebyla provedena ... 
                    {
                        Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                        Type[] types = providerAssemlby.GetTypes();
                        foreach (Type t in types)
                        {
                            try
                            {
                                if (typeof(Fask.Interfaces.Inventura.IInventura2).IsAssignableFrom(t))
                                {
                                    providerInventura = (Fask.Interfaces.Inventura.IInventura2)providerAssemlby.CreateInstance(t.FullName);
                                    if (providerInventura != null)
                                        break;
                                }
                            }
                            catch { }
                        }
                        //return config;
                    }

                    providerInventura.InitProvider();

                 
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                if (!String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                {
                    if (providerSklady == null) //inicializace se provede pouze pokud nebyla provedena ... 
                    {
                        Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                        Type[] types = providerAssemlby.GetTypes();
                        foreach (Type t in types)
                        {
                            try
                            {
                                if (typeof(Fask.Interfaces.Ciselniky.Sklady.ISklady2).IsAssignableFrom(t))
                                {
                                    providerSklady = (Fask.Interfaces.Ciselniky.Sklady.ISklady2)providerAssemlby.CreateInstance(t.FullName);
                                    if (providerSklady != null)
                                        break;
                                }
                            }
                            catch { }
                        }
                        //return config;
                    }

                    providerSklady.InitProvider();
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (Zobrazeni != Fask.Interfaces.Classes.ZOBRAZENI_TYP.VYBER)
                    return;

                if (SelectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam", this.Text, MessageBoxButtons.OK);
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
            //UpdateForm(SelectedRow);
            PerformVyhledat();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                    PerformOK();
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
                    if (SelectedRow != null)
                        sortedID = SelectedRow.CountEntries.ToString();
                }
            }
            catch { }
        }

        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            try
            {
                int pos = this.bs_IH.Find(ds_IH.CZMST_I1H.CountEntriesColumn.ColumnName, sortedID);
                this.bs_IH.Position = pos;
            }
            catch { }
        }

        private void bwLoadZbozi_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //bwLoadSkladPohyb.CancelAsync();
                //Fask.Interfaces.Filtry.InventuraPredlohaListFiltr filtr = (Fask.Interfaces.Filtry.InventuraPredlohaListFiltr)e.Argument;
                Fask.Interfaces.DataSets.Inventura ds = new Fask.Interfaces.DataSets.Inventura();

                if (bwIH.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                // nacteni dat v oddelenem vlakne
                //ds = ((Fask.Interfaces.Inventura.IInventura)providerInventura).GetFiltrovanaPredloha(filtr);

                if ((providerInventura != null) && (providerInventura is Fask.Interfaces.Inventura.IInventura2_GetHlavicky))
                    ds = ((Fask.Interfaces.Inventura.IInventura2_GetHlavicky)providerInventura).GetHlavicky();
                else
                    throw new NotImplementedException("Provider neimplementuje IInventura2_GetHlavicky.");

                if (bwIH.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                e.Result = ds;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void bwLoadZbozi_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                // check error, check cancel, then use result
                if (e.Error != null)
                {
                    // handle the error
                    ds_IH = new Fask.Interfaces.DataSets.Inventura();
                    bs_IH.DataSource = ds_IH;
                }
                else if (e.Cancelled)
                {
                    // handle cancellation
                    // TODO: maji se vymazat data z datasetu?? asi ano ...
                    //if (dsSkladPohyb == null)
                    ds_IH = new Fask.Interfaces.DataSets.Inventura();
                    bs_IH.DataSource = ds_IH;
                }
                else
                {
                    // uspesne dokonceno ...
                    // use it on the UI thread
                    ds_IH = (Fask.Interfaces.DataSets.Inventura)e.Result;
                    if (ds_IH == null)
                        ds_IH = new Fask.Interfaces.DataSets.Inventura();

                    bs_IH.DataSource = ds_IH;
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

        private void ProgressIndicatorStop()
        {
            progressIndicator1.Stop();
            progressIndicator1.Visible = false;
        }

        private void ProgressIndicatorStart()
        {
            try
            {
                // prepocet stredu datagridu
                this.progressIndicator1.Location = new Point(this.dg_IH.Location.X + (this.dg_IH.Width / 2) - (progressIndicator1.Size.Width / 2), this.dg_IH.Location.Y + (this.dg_IH.Height / 2) - (progressIndicator1.Size.Height / 2));
            }
            catch { }
            progressIndicator1.Start();
            progressIndicator1.Visible = true;
        }

        private void buttonVyhledat_Click(object sender, EventArgs e)
        {
            PerformVyhledat();
        }

        /// <summary>
        /// Vyhledani podle zadaneho filtru.
        /// </summary>
        private void PerformVyhledat()
        {
            try
            {
                DataTable dtchanged = this.ds_IH.CZMST_I1H.GetChanges();
                if (dtchanged != null && dtchanged.Rows.Count > 0) //obsahuje zmeny ... 
                {
                    DialogResult dr = MessageBox.Show("Obsahuje změny, pokračovat?", "data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == System.Windows.Forms.DialogResult.No)
                        return;
                }

                if (bwIH.IsBusy)
                {
                    bwIH.CancelAsync();
                    while (bwIH.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }

                ProgressIndicatorStart();

                //Fask.Interfaces.Filtry.ExpediceFormDodaciListyListFiltr filtr = new Fask.Interfaces.Filtry.ExpediceFormDodaciListyListFiltr();
                //if (!CreateFilter(ref filtr))
                //    return;

                int FirstDisplayedScrollingRowIndex = this.dg_IH.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

                bwIH.RunWorkerAsync();

                if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dg_IH.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dg_IH.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index
            }
            catch (Exception ex)
            {
                ProgressIndicatorStop();
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Vytvoreni filtru, ktery se pouzije.
        /// </summary>
        /// <param name="filtr"></param>
        /// <returns></returns>
        //private bool CreateFilter(ref Fask.Interfaces.Filtry.ExpediceFormDodaciListyListFiltr filtr)
        //{
        //    if (filtr == null)
        //        filtr = new Fask.Interfaces.Filtry.ExpediceFormDodaciListyListFiltr();

        //    filtr.ITEMNMBR = cbMaterialITEMNMBR.Text.Trim();
        //    filtr.NMBRPAL = cbMaterialNMBRPAL.Text.Trim();
        //    filtr.Rozpracovano = cbMaterialRozpracovano.Text.Trim();

        //    return true;
        //}

        private void buttonExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                //dataGridView1.ExportAllRowsVisibleColumnsToExcel(string.Empty);
                this.dg_IH.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Slouceni vybranych inventurnich davek ... 
        /// </summary> 
        private void PerformSloucit()
        {
            try
            {
                var selectedRows = dg_IH.SelectedRows;
                // test vybranych
                if (selectedRows.Count <= 1)
                {
                    MessageBox.Show("Nejsou vybrané inventury ke sloučení", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                //selectedRows
                List<string> seznamDavekI = new List<string>();
                foreach (DataGridViewRow r in this.dg_IH.SelectedRows)
                {
                    var i1hrow = (Fask.Interfaces.DataSets.Inventura.CZMST_I1HRow)(((DataRowView)r.DataBoundItem).Row);
                    seznamDavekI.Add(i1hrow.CountEntries.ToString());
                }

                // vygenerovat cislo nove inventury
                string sloucenaI = string.Empty;
                Konzola.Forms.InputBox.Show("Zadejte číslo sloučené inventury", string.Empty, out sloucenaI);

                // overit, zda zadana neexistuje

                // prevest existujici zaznamy do nove a oznacit puvodni ...
                if (providerInventura is Fask.Interfaces.Inventura.IInventura2_SloucitInventury)
                {
                    Fask.Interfaces.Classes.StatusInfo si = ((Fask.Interfaces.Inventura.IInventura2_SloucitInventury)providerInventura).SloucitInventury(seznamDavekI, sloucenaI);
                    MessageBox.Show(si.Description, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Provider nepodporuje sloučení ... ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PerformRozdelit()
        {
            try
            {
                var selectedRows = dg_IH.SelectedRows;
                // test vybranych
                if (selectedRows.Count == 0)
                {
                    MessageBox.Show("Není vybrána inventura k rozdělení", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                //selectedRows
                List<string> seznamDavekI = new List<string>();
                foreach (DataGridViewRow r in this.dg_IH.SelectedRows)
                {
                    var i1hrow = (Fask.Interfaces.DataSets.Inventura.CZMST_I1HRow)(((DataRowView)r.DataBoundItem).Row);
                    seznamDavekI.Add(i1hrow.CountEntries.ToString());
                }

                // testy ...

                // prevest existujici zaznamy do puvodni ...
                if (providerInventura is Fask.Interfaces.Inventura.IInventura2_RozdelitInventury)
                {
                    Fask.Interfaces.Classes.StatusInfo si = ((Fask.Interfaces.Inventura.IInventura2_RozdelitInventury)providerInventura).RozdelitInventury(seznamDavekI);
                    MessageBox.Show(si.Description, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Provider nepodporuje sloučení ... ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsmiOdstranit_Click(object sender, EventArgs e)
        {

        }

        private void tsmiUpravit_Click(object sender, EventArgs e)
        {

        }

        private void tsmiNovy_Click(object sender, EventArgs e)
        {

        }

        private void tsmiSloucit_Click(object sender, EventArgs e)
        {
            PerformSloucit();
        }

        private void tsmiRozdelit_Click(object sender, EventArgs e)
        {
            PerformRozdelit();
        }

        private void advancedDataGridViewSearchToolBar1_Search(object sender, Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs e)
        {
            bool restartsearch = true;
            int startColumn = 0;
            int startRow = 0;
            if (!e.FromBegin)
            {
                bool endcol = dg_IH.CurrentCell.ColumnIndex + 1 >= dg_IH.ColumnCount;
                bool endrow = dg_IH.CurrentCell.RowIndex + 1 >= dg_IH.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dg_IH.CurrentCell.ColumnIndex;
                    startRow = dg_IH.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dg_IH.CurrentCell.ColumnIndex + 1;
                    startRow = dg_IH.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dg_IH.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dg_IH.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dg_IH.CurrentCell = c;
        }



        #region Historie
        //private void tsbNastavit_Click(object sender, EventArgs e)
        //{
        //    PerformNastavitFiltr(rowFiltr);
        //}

        /// <summary>
        /// Nastavi zvoleny filtr do comboboxu, ...
        /// </summary>
        /// <param name="filtr"></param>
        //private void PerformNastavitFiltr(Fask.Interfaces.Filtry.ExpediceFormDodaciListyListFiltr filtr)
        //{
        //    try
        //    {
        //        if (filtr == null)
        //            return;

        //        if (!MySystem.LoginTest.UserLoginTest())
        //            return;

        //        PerformVycistitFiltr();

        //        cbMaterialITEMNMBR.Text = filtr.ITEMNMBR;         // itemnmbr
        //        cbMaterialNMBRPAL.Text = filtr.NMBRPAL;      // itemdesc
        //        cbMaterialRozpracovano.Text = filtr.Rozpracovano;   // hlavicka.Rozpracovano
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Write(ex);
        //        MessageBox.Show("Nepodařilo se načíst data filtru.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        //private void tsbVycistit_Click(object sender, EventArgs e)
        //{
        //    PerformVycistitFiltr();
        //}

        //private void PerformVycistitFiltr()
        //{
        //    try
        //    {
        //        cbMaterialITEMNMBR.SelectedItem =
        //        cbMaterialNMBRPAL.SelectedItem = 
        //        cbMaterialRozpracovano.SelectedItem = null;

        //        cbMaterialITEMNMBR.Text =
        //        cbMaterialNMBRPAL.Text = 
        //        cbMaterialRozpracovano.Text = string.Empty;
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Write(ex);
        //        MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        //private void tsbZmena_Click(object sender, EventArgs e)
        //{
        //    PerformZmenitFiltr();
        //}

        /// <summary>
        /// Upravi zvoleny filtr.
        /// </summary>
        //private void PerformZmenitFiltr()
        //{
        //    try
        //    {
        //        if (!MySystem.LoginTest.UserLoginTest())
        //            return;

        //        if (rowFiltr == null)
        //            return;

        //        DialogResult dr = MessageBox.Show("Opravdu chcete změnit vybraný filtr '" + rowFiltr.NazevFiltru.Trim() + "'?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        //        if (dr != System.Windows.Forms.DialogResult.Yes)
        //            return;

        //        Fask.Interfaces.Filtry.ExpediceFormDodaciListyListFiltr filtr = rowFiltr;

        //        if (!CreateFilter(ref filtr))
        //            return;
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Write(ex);
        //        MessageBox.Show("Nepodařilo se upravit filtr.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        //private void tsbPridat_Click(object sender, EventArgs e)
        //{
        //    PerformPridatFiltr();
        //}

        ///// <summary>
        ///// Ulozeni filtru do souboru.
        ///// </summary>
        //private void PerformPridatFiltr()
        //{
        //    try
        //    {
        //        if (!MySystem.LoginTest.UserLoginTest())
        //            return;

        //        Fask.Interfaces.Filtry.ExpediceFormDodaciListyListFiltr filtr = new Fask.Interfaces.Filtry.ExpediceFormDodaciListyListFiltr();
        //        string nazev = string.Empty;
        //        DialogResult dr = Forms.InputBox.Show("Název filtru", "Zadejte název filtru", string.Empty, false, out nazev);
        //        if (dr != System.Windows.Forms.DialogResult.OK)
        //            return;

        //        bool result = CreateFilter(ref filtr);
        //        if (result)
        //        {
        //            filtr.NazevFiltru = nazev;
        //            filtry.Add(filtr);
        //            //this.tscbFiltry.Items.Clear();
        //            this.tscbFiltry.ComboBox.DataSource = null;
        //            this.tscbFiltry.ComboBox.DataSource = filtry;
        //            this.tscbFiltry.SelectedItem = filtr;
        //            tscbFiltry.ComboBox.DropDownWitdhAutosize();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Write(ex);
        //        MessageBox.Show("Nepodařilo se uložit data filtru.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        //private void tsbOdebrat_Click(object sender, EventArgs e)
        //{
        //    PerformOdebratFiltr();
        //}

        ///// <summary>
        ///// Odstrani vybrany filtr
        ///// </summary>
        //private void PerformOdebratFiltr()
        //{
        //    try
        //    {
        //        if (!MySystem.LoginTest.UserLoginTest())
        //            return;

        //        if (rowFiltr == null)
        //            return;

        //        DialogResult dr = MessageBox.Show("Opravdu chcete odstranit filtr '" + (string.IsNullOrEmpty(rowFiltr.NazevFiltru) ? string.Empty : rowFiltr.NazevFiltru.Trim()) + "'?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        //        if (dr != System.Windows.Forms.DialogResult.Yes)
        //            return;

        //        filtry.Remove(rowFiltr);
        //        this.tscbFiltry.ComboBox.DataSource = null;
        //        this.tscbFiltry.ComboBox.DataSource = filtry;
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Write(ex);
        //        MessageBox.Show("Nepodařilo se odstranit filtr.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        #endregion    }
    }
}
