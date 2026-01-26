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
using System.Threading;
using System.IO;

namespace Konzola.Servis
{
    /// <summary>
    /// Prace s uzivateli pomoci Interface, ...
    /// Tento form primarne pouzivat a rozsirovat
    /// // TODO: pridani, editace a mazani zaznamu
    /// </summary>
    public partial class FormZdrojeList : Form
    {
        private Fask.Interfaces.IMES providerServis = null;
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
                            menuStrip2.Items.Remove(tsmiPolozka);
                            menuStrip2.Items.Remove(tsmiMenuList);
                            menuStrip2.Items.Remove(tsmiTisk);
                            tsFiltry.Visible = false;
                            tsFiltry.Enabled = false;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Seznam vsech nactenych filtru.
        /// </summary>
        private List<Fask.Interfaces.Filtry.ZdrojeListFiltr> filtry = new List<Fask.Interfaces.Filtry.ZdrojeListFiltr>();

        /// <summary>
        /// Vybrany filtr.
        /// </summary>
        private Fask.Interfaces.Filtry.ZdrojeListFiltr rowFiltr
        {
            get
            {
                try
                {
                    return tscbFiltry.SelectedItem as Fask.Interfaces.Filtry.ZdrojeListFiltr;
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Zvolené id před seřazením
        /// </summary>
        string selectedSortID = string.Empty;

        /// <summary>
        /// Radek, ktery se predvoli (pri editaci zaznamu)
        /// </summary>
        private Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojRow selectRow { get; set; }

        /// <summary>
        /// Vybrany radek.
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
        /// Kolekce veškerých zvolených sloupců.
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

        #region Eventy formu

        public FormZdrojeList(bool allowMultiSelect, Fask.Interfaces.Classes.ZOBRAZENI_TYP typZobrazeni)
        {
            InitializeComponent();
            this.dgServis.UpdateColumnHeaderCellsByDatasource();
            this.dgServis.MultiSelect = allowMultiSelect;
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

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="allowMultiSelect">Povoleni oznaceni vice zaznamu.</param>
        /// <param name="selectRow">Radek, ktery se oznaci.</param>
        public FormZdrojeList(bool allowMultiSelect, Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojRow selectRow, Fask.Interfaces.Classes.ZOBRAZENI_TYP typZobrazeni)
            : this(allowMultiSelect, typZobrazeni)
        {
            this.selectRow = selectRow;
        }

        private void FormZdrojeList_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                //this.WindowState = FormWindowState.Maximized;
                this.dgServis.LoadConfiguration(this.GetType().ToString());

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

                advancedDataGridViewSearchToolBar1.SetColumns(dgServis.Columns);


                // načtení konfigurace vytvořených filtrů
                this.filtry = Extensions.ListExt.ReadFromXML<Fask.Interfaces.Filtry.ZdrojeListFiltr>(this.GetType().ToString() + ".filtr");
                tscbFiltry.ComboBox.DataSource = this.filtry;
                tscbFiltry.SelectedItem = null;
                tscbFiltry.ComboBox.DropDownWitdhAutosize();

                // inicializace providera
                InitProvider();

                if (providerServis == null)
                    throw new Exception("Provider 'Servis' není inicializován");

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

        private void FormZdrojeList_KeyDown(object sender, KeyEventArgs e)
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

        private void FormZdrojeList_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.dgServis.SaveConfiguration(this.GetType().ToString());

                if (Zobrazeni == Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST)
                {
                    this.WindowState = FormWindowState.Maximized;

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
                    if (bwLoadData.IsBusy)
                    {
                        bwLoadData.CancelAsync();
                        while (bwLoadData.IsBusy)
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
        /// <summary>
        /// Inicializace providera
        /// </summary>
        private void InitProvider()
        {
            try
            {
                if (String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                    return;
                
                if (providerServis == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Servis.IServis).IsAssignableFrom(t))
                            {
                                providerServis= (Fask.Interfaces.Servis.IServis)providerAssemlby.CreateInstance(t.FullName);
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

        private void tsmiVybrat_Click(object sender, EventArgs e)
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

        private void tsmiKonec_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void obnovitToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
                        selectedSortID = SelectedRow.ID.ToString();
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
                int pos = this.bsServis.Find(dsServis.CZMST_Servis_Zdroj.IDColumn.ColumnName, selectedSortID);
                this.bsServis.Position = pos;
            }
            catch { }
        }

        private void bwLoadZbozi_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Fask.Interfaces.Filtry.ZdrojeListFiltr filtr = (Fask.Interfaces.Filtry.ZdrojeListFiltr)e.Argument;
                Fask.Interfaces.DataSets.Servis ds = new Fask.Interfaces.DataSets.Servis();

                if (bwLoadData.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                // nacteni dat v oddelenem vlakne
                ds = ((Fask.Interfaces.Servis.IServis)providerServis).GetFiltrovaneZdroje(filtr);
                if (bwLoadData.CancellationPending)
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
                    dsServis = new Fask.Interfaces.DataSets.Servis();
                    bsServis.DataSource = dsServis;
                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, e.Error);
                    MessageBox.Show(e.Error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (e.Cancelled)
                {
                    // handle cancellation
                    // TODO: maji se vymazat data z datasetu?? asi ano ...
                    //if (dsSkladPohyb == null)
                    dsServis = new Fask.Interfaces.DataSets.Servis();
                    bsServis.DataSource = dsServis;
                }
                else
                {
                    // uspesne dokonceno ...
                    // use it on the UI thread
                    dsServis= (Fask.Interfaces.DataSets.Servis)e.Result;
                    if (dsServis == null)
                        dsServis = new Fask.Interfaces.DataSets.Servis();

                    bsServis.DataSource = dsServis;
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
                this.progressIndicator1.Location = new Point(this.dgServis.Location.X + (this.dgServis.Width / 2) - (progressIndicator1.Size.Width / 2), this.dgServis.Location.Y + (this.dgServis.Height / 2) - (progressIndicator1.Size.Height / 2));
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
                DataTable dtchanged = this.dsServis.CZMST_Servis_Zdroj.GetChanges();
                if (dtchanged != null && dtchanged.Rows.Count > 0) //obsahuje zmeny ... 
                {
                    DialogResult dr = MessageBox.Show("Obsahuje změny, pokračovat?", "data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == System.Windows.Forms.DialogResult.No)
                        return;
                }

                if (bwLoadData.IsBusy)
                {
                    bwLoadData.CancelAsync();
                    while (bwLoadData.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }

                ProgressIndicatorStart();

                Fask.Interfaces.Filtry.ZdrojeListFiltr filtr = new Fask.Interfaces.Filtry.ZdrojeListFiltr();
                if (!CreateFilter(ref filtr))
                    return;

                int FirstDisplayedScrollingRowIndex = this.dgServis.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

                bwLoadData.RunWorkerAsync(filtr);

                if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dgServis.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dgServis.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index
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
        private bool CreateFilter(ref Fask.Interfaces.Filtry.ZdrojeListFiltr filtr)
        {
            if (filtr == null)
                filtr = new Fask.Interfaces.Filtry.ZdrojeListFiltr();

            filtr.ZdrojID = cbZdrojID.Text.Trim();
            filtr.ZdrojOznaceni = cbZdrojOznaceni.Text.Trim();
            filtr.ZdrojBarcode = cbZdrojBarcode.Text.Trim();
            filtr.ZdrojType = cbZdrojType.Text.Trim();
            
            return true;
        }

        private void FormZboziSelect_Shown(object sender, EventArgs e)
        {
            try
            {
                this.progressIndicator1.Size = new Size(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ostatni[0].ProgressIndicatorSize, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ostatni[0].ProgressIndicatorSize);
                this.progressIndicator1.Location = new Point(this.dgServis.Location.X + (this.dgServis.Width / 2) - (progressIndicator1.Size.Width / 2), this.dgServis.Location.Y + (this.dgServis.Height / 2) - (progressIndicator1.Size.Height / 2));
            }
            catch { }
            try
            {
                PerformVyhledat();
            }
            catch { }
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                //dataGridView1.ExportAllRowsVisibleColumnsToExcel(string.Empty);
                this.dgServis.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbNastavit_Click(object sender, EventArgs e)
        {
            PerformNastavitFiltr(rowFiltr);
        }

        /// <summary>
        /// Nastavi zvoleny filtr do comboboxu, ...
        /// </summary>
        /// <param name="filtr"></param>
        private void PerformNastavitFiltr(Fask.Interfaces.Filtry.ZdrojeListFiltr filtr)
        {
            try
            {
                if (filtr == null)
                    return;

                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                PerformVycistitFiltr();

                cbZdrojID.Text = filtr.ZdrojID;
                cbZdrojOznaceni.Text = filtr.ZdrojOznaceni;
                cbZdrojBarcode.Text = filtr.ZdrojBarcode;
                cbZdrojType.Text = filtr.ZdrojType;
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
                cbZdrojID.SelectedItem =
                cbZdrojBarcode.SelectedItem =
                cbZdrojType.SelectedItem =
                cbZdrojOznaceni.SelectedItem = null;

                cbZdrojID.Text =
                cbZdrojBarcode.Text =
                cbZdrojType.Text =
                cbZdrojOznaceni.Text = string.Empty;
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

                Fask.Interfaces.Filtry.ZdrojeListFiltr filtr = rowFiltr;

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

                Fask.Interfaces.Filtry.ZdrojeListFiltr filtr = new Fask.Interfaces.Filtry.ZdrojeListFiltr();
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

        private void PerformCreateRecord()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis[0].TvorbaZdrojePriraditStavOkruhPovolit)
                {
                    using (Servis.FormZdrojStavOkruhEdit frmzdroje = new FormZdrojStavOkruhEdit())
                    {
                        frmzdroje.Text = "Nový zdroj";
                        if (frmzdroje.ShowDialog(this) != DialogResult.OK)
                            return;

                        UpdateForm(frmzdroje.returnrow);
                        // TODO: pouzit importrow misto vyhledani ...
                        //this.dsServis.CZMST_Servis_Zdroj.ImportRow(frmzdroje.returnrow);
                        
                        //this.dsServis.CZMST_Servis_Zdroj.AcceptChanges();
                        //try
                        //{
                        //    this.bsServis.Position = dsServis.CZMST_Servis_Zdroj.Count - 1;
                        //}
                        //catch { }
                    }
                }
                else
                {
                    using (Servis.FormZdrojeEdit frmzdroje = new FormZdrojeEdit())
                    {
                        frmzdroje.Text = "Nový zdroj";
                        if (frmzdroje.ShowDialog(this) != DialogResult.OK)
                            return;

                        UpdateForm(frmzdroje.returnrow);
                        // TODO: pouzit importrow misto vyhledani ...
                        //this.dsServis.CZMST_Servis_Zdroj.ImportRow(frmzdroje.returnrow);
                        
                        //this.dsServis.CZMST_Servis_Zdroj.AcceptChanges();
                        //try
                        //{
                        //    this.bsServis.Position = dsServis.CZMST_Servis_Zdroj.Count - 1;
                        //}
                        //catch { }
                    }
                }


            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
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

                // Najit zdroj 
                FindZdrojInDataView(id);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show("Nepodařilo se obnovit záznamy\n" + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FindZdrojInDataView(string id)
        {
            try
            {
                while (bwLoadData.IsBusy)
                {
                    Application.DoEvents();
                }

                if (!string.IsNullOrEmpty(id))
                {
                    int index = bsServis.Find(dsServis.CZMST_Servis_Zdroj.IDColumn.ColumnName, id);
                    if (index >= 0)
                        this.bsServis.Position = index;
                }
            }
            catch
            {
            }
            dgServis.Focus();
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

                using (Servis.FormZdrojeEdit frmzdroje = new FormZdrojeEdit())
                {
                    frmzdroje.zdrojrow = SelectedRow;
                    frmzdroje.Text = "Úprava zdroje";
                    if (frmzdroje.ShowDialog(this) != DialogResult.OK)
                        return;

                    UpdateForm(frmzdroje.returnrow);
                    //// aktualizace
                    //this.SelectedRow.ID = frmzdroje.returnrow.ID;
                    //this.SelectedRow.Oznaceni = frmzdroje.returnrow.Oznaceni;
                    //if (frmzdroje.returnrow.IsBarcodeNull())
                    //    this.SelectedRow.SetBarcodeNull();
                    //else
                    //    this.SelectedRow.Barcode = frmzdroje.returnrow.Barcode;
                    //if (frmzdroje.returnrow.IsTypeNull())
                    //    this.SelectedRow.SetTypeNull();
                    //else
                    //    this.SelectedRow.Type = frmzdroje.returnrow.Type;
                    //dsServis.CZMST_Servis_Zdroj.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PerformDuplikateRecord()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (SelectedRows.Count > 1)
                {
                    MessageBox.Show("Musí být vybrán pouze jeden záznam", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if (SelectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro duplikování", this.Text, MessageBoxButtons.OK);
                    return;
                }

                var zdroj = SelectedRow;
                string newid = string.Empty;

                while (true)
                {
                    DialogResult dres = Konzola.Forms.InputBox.Show("Zadejte Identifikator noveho Zdroje", String.IsNullOrEmpty(newid) ? zdroj.ID : newid, out newid);
                    if (dres == System.Windows.Forms.DialogResult.Cancel)
                        return;

                    // test na existenci zdroje s newid
                    var zdrojexist = ((Fask.Interfaces.Servis.IServis)providerServis).GetZdrojByID(newid);
                    if (zdrojexist != null)
                    {
                        DialogResult drExist = MessageBox.Show("Zdroj s ID '" + newid + "' již existuje", this.Text, MessageBoxButtons.RetryCancel);
                        if (drExist == System.Windows.Forms.DialogResult.Retry)
                            continue;
                        else
                            return;
                    }
                    else
                    {
                        if (!((Fask.Interfaces.Servis.IServis)providerServis).DuplicateZdroj(zdroj, newid))
                        {
                            DialogResult drOpakovat = MessageBox.Show("Duplikace zdroje se nezdařila", this.Text, MessageBoxButtons.RetryCancel);
                            if (drOpakovat == System.Windows.Forms.DialogResult.Retry)
                                continue;
                            else
                                return;
                        }
                        break;
                    }
                }

                // obnoveni zdroju a nalezeni
                PerformVyhledat();

                // najit zdroj s id ...
                FindZdrojInDataView(newid);

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

                if (SelectedRows.Count > 1)
                {
                    MessageBox.Show("Musí být vybrán pouze jeden záznam", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if (SelectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro odstranění", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if (MessageBox.Show("Opravdu chcete odstranit zdroj '" + SelectedRow.ToString() + "'?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

                bool result = ((Fask.Interfaces.Servis.IServis)providerServis).DeleteZdroj(SelectedRow.ID);

                this.dsServis.CZMST_Servis_Zdroj.RemoveCZMST_Servis_ZdrojRow(SelectedRow);
                this.dsServis.AcceptChanges();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripMenuItemDuplikovat_Click(object sender, EventArgs e)
        {
            PerformDuplikateRecord();
        }

        private void tsmiTiskReport_Click(object sender, EventArgs e)
        {

            //System.Drawing.Printing.PrintDocument pdocument = new System.Drawing.Printing.PrintDocument();
            //pdocument.DocumentName = "ReportZdroj.rdlc";

            //pdocument.prin

            //// vytiskne report ...
            //PrintDialog pd = new PrintDialog();
            //pd.ShowDialog();

            //PrintPreviewDialog ppd = new PrintPreviewDialog();
            //ppd.Document = pdocument;
            //ppd.ShowDialog();


            //this.bsServis.

            Print.FormPrintZdroj fpz = new Print.FormPrintZdroj();

            Fask.Interfaces.DataSets.Servis dsServisSelected = new Fask.Interfaces.DataSets.Servis();
            foreach (DataGridViewRow row in this.dgServis.SelectedRows)
            {
                DataRowView drv = this.bsServis[row.Index] as DataRowView;
                var zdroj = drv.Row as Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojRow;
                dsServisSelected.CZMST_Servis_Zdroj.ImportRow(zdroj);
            }

            fpz.CZMST_Servis_ZdrojDataTable = dsServisSelected.CZMST_Servis_Zdroj;
            fpz.ShowDialog(this);
        }

        private void tsmiTiskEtiketa_Click(object sender, EventArgs e)
        {
            try
            {
                PrintDialog printDialog1 = new PrintDialog();
                printDialog1.UseEXDialog = true;

                if (printDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.Cancel)
                    return;

                // 1) pripravit 
                Fask.Interfaces.DataSets.Servis dsServisSelected = new Fask.Interfaces.DataSets.Servis();
                foreach (DataGridViewRow row in this.dgServis.SelectedRows)
                {
                    DataRowView drv = this.bsServis[row.Index] as DataRowView;
                    var _zdrojRow = drv.Row as Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojRow;
                    dsServisSelected.CZMST_Servis_Zdroj.ImportRow(_zdrojRow);
                }

                string pocetStr = string.Empty;
                int pocetInt = 1;
                while (true)
                {
                    DialogResult drPocet = Konzola.Forms.InputBox.Show("Etiketa tisk", "Počet etiket Zdroje k vytištění", pocetInt.ToString(), Forms.InputBox.TypeOfCode.NumericInt, true, 1, 99, false, out pocetStr);
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
                    _Printers_.PrinterLeitz.Print_Zdroj(printDialog1.PrinterSettings.PrinterName, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Servis", "Print", "Templates", "Etiketa_Zdroj.LeitzLbl"), pocetInt, dsServisSelected);
                    return;
                }
                else
                { // je to neco jineho ...

                    string strPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Servis", "Print", "Templates", "Etiketa_Zdroj.txt");
                    System.IO.StreamReader sr = new System.IO.StreamReader(strPath);
                    string strData = sr.ReadToEnd();
                    sr.Close();

                    var columns = dsServisSelected.CZMST_Servis_Zdroj.Columns;
                    foreach (var row in dsServisSelected.CZMST_Servis_Zdroj)
                    {
                        StringBuilder sbData = new StringBuilder();
                        sbData.Append(strData);

                        foreach (DataColumn col in columns)
                        {
                            sbData.Replace(String.Format("${0}$", col.ColumnName), row[col].ToString());
                        }

                        sbData.Replace("$Pocet$", pocetInt.ToString());
                        if (!Konzola._Support_.RawPrinterHelper.SendStringToPrinter(printDialog1.PrinterSettings.PrinterName, sbData.ToString()))
                        {
                            throw new Exception("Tisk etikety '" + row.Oznaceni + " (" + row.ID + ")" + "' se nezdařil");
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

        private void tsmiDuplikovat_Click(object sender, EventArgs e)
        {
            PerformDuplikateRecord();
        }

        private void tsmiUpravit_Click(object sender, EventArgs e)
        {
            PerformEditRecord();
        }

        private void tsmiNovy_Click(object sender, EventArgs e)
        {
            PerformCreateRecord();
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
