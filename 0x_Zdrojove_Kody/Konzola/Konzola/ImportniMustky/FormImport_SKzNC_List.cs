using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fask.Logging;
using System.IO;
using Konzola.Extensions;
using System.Reflection;
using Fask.Interfaces.Classes;
using MST_Print_Server_ZPL_Printing;

using Fask.Interfaces.DataSets_Import;
using Konzola.ImportniMustky.Extensions;
using Konzola.Forms;

namespace Konzola.ImportniMustky
{
    public partial class FormImport_SKzNC_List : Form
    {

        #region private promenne s opravnenim

        private bool opravneniEditace = false;
        private bool opravneniImport = false;
        private bool opravneniArchivace = false;
        private Opravneni opravneni;

        private void SetOpravneni()
        {

            if (opravneni.HasFlag(Opravneni.Editace) && opravneni.HasFlag(Opravneni.Archivace) && opravneni.HasFlag(Opravneni.Import))
            {
                opravneniEditace = true;
                opravneniImport = true;
                opravneniArchivace = true;
                // Povolit funkce pro oba případy
                //MessageBox.Show("Máte oprávnění k editaci i importu.");
            }

            // Nastavíte možnosti formuláře na základě oprávnění
            if (opravneni.HasFlag(Opravneni.Editace))
            {
                opravneniEditace = true;
                // Povolit funkce pro editaci
                // například povolit nějaké tlačítka nebo editační pole
            }

            if (opravneni.HasFlag(Opravneni.Import))
            {
                opravneniImport = true;
                // Povolit funkce pro import
                // například povolit tlačítko nebo nabídku pro import
            }

            if (opravneni.HasFlag(Opravneni.Archivace))
            {
                opravneniArchivace = true;
                // Povolit funkce pro import
                // například povolit tlačítko nebo nabídku pro import
            }

            // Zkontroluje, zda má uživatel obě oprávnění


            // Můžete přidat další logiku pro další oprávnění
        }


 

        public FormImport_SKzNC_List(Opravneni opravneni)
        {
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);

            InitializeComponent();
            this.dgImportPOHODA.UpdateColumnHeaderCellsByDatasource();
            panelButtons.Menu = menuStrip1;

            // Uloží oprávnění pro další použití v programu
            this.opravneni = opravneni;

            // Nastavení podle oprávnění
            SetOpravneni();
        }

        #endregion


        #region Parametry

        /// <summary>
        /// Provider pro komunikaci
        /// </summary>
        private Fask.Interfaces.IMES provider = null; 

        private ImportPOHODA_FromExcel.FASK_ZASOBY_IMPORT_POHODA_SKzNCRow rowDodavateleZasoby
        {
            get
            {
                try
                {
                    return ((DataRowView)(dgImportPOHODA.BindingContext[this.bsImportPOHODA].Current)).Row as ImportPOHODA_FromExcel.FASK_ZASOBY_IMPORT_POHODA_SKzNCRow;
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
                    return dgImportPOHODA.SelectedRows;
                }
                catch
                {
                    return null;
                }
            }
        }


        #region Parametry Filtry


        /// <summary>
        /// Seznam vsech nactenych filtru.
        /// </summary>
        private List<Fask.Interfaces.Filtry.Import_SKzNC_ListFiltr> filtry = new List<Fask.Interfaces.Filtry.Import_SKzNC_ListFiltr>();

        /// <summary>
        /// Vybrany filtr.
        /// </summary>
        private Fask.Interfaces.Filtry.Import_SKzNC_ListFiltr rowFiltr
        {
            get
            {
                try
                {
                    return tscbFiltry.SelectedItem as Fask.Interfaces.Filtry.Import_SKzNC_ListFiltr;
                }
                catch
                {
                    return null;
                }
            }
        }


        #endregion

        #endregion

        #region Eventy formu

        /// <summary>
        /// Kontruktor
        /// </summary>
        public FormImport_SKzNC_List()
        {
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);

            InitializeComponent();
            this.dgImportPOHODA.UpdateColumnHeaderCellsByDatasource();
            panelButtons.Menu = menuStrip1;
        }

        private void FormImport_SKzNC_List_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                this.WindowState = FormWindowState.Maximized;

                // načtení konfigurace datagridu z nastavení aplikace
                this.dgImportPOHODA.LoadConfiguration(this.GetType().ToString());
                panelButtons.LoadConfiguration(this.GetType().ToString());
                panelButtons.Init();
                panelButtons.Size = new Size(85, 700);

                advancedDataGridViewSearchToolBar1.SetColumns(dgImportPOHODA.Columns);

                InitProvider();

                if (provider == null)
                    throw new Exception("Provider není inicializován");


                DateTime today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);

                //// načtení konfigurace vytvořených filtrů
                this.filtry = ListExt.ReadFromXML<Fask.Interfaces.Filtry.Import_SKzNC_ListFiltr>(this.GetType().ToString() + ".filtr");
                tscbFiltry.ComboBox.DataSource = this.filtry;
                tscbFiltry.SelectedItem = null;
                tscbFiltry.ComboBox.DropDownWitdhAutosize();

                SetStatusLabelText_Events(-1);

                buttonVyhledat.Focus();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormImport_SKzNC_List_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.dgImportPOHODA.SaveConfiguration(this.GetType().ToString());
                panelButtons.SaveConfiguration(this.GetType().ToString());

                this.filtry.WriteXML(this.GetType().ToString() + ".filtr");
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormImport_SKzNC_List_KeyDown(object sender, KeyEventArgs e)
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
                    PerformVyhledat();
                }
                else
                    return;
            }
            else
                return;

            e.Handled = true;
        }


        #endregion

        #region inicaliyace provideru


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
                            if (typeof(Fask.Interfaces.ImportnyMustky.IImportnyMustky).IsAssignableFrom(t))
                            {
                                provider = (Fask.Interfaces.ImportnyMustky.IImportnyMustky)providerAssemlby.CreateInstance(t.FullName);
                                if (provider != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
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

        #region Perfom Metody

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

        /// <summary>
        /// Metoda pro dotaženi dat podle filtru
        /// </summary>
        private void PerformVyhledat()
        {
            try
            {
                DataTable dtchanged = this.dsImportPOHODA.FASK_ZASOBY_IMPORT_POHODA_SKzNC.GetChanges();
                if (dtchanged != null && dtchanged.Rows.Count > 0) //obsahuje zmeny ... 
                {
                    DialogResult dr = MessageBox.Show("Obsahuje změny, pokračovat?", "data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == System.Windows.Forms.DialogResult.No)
                        return;
                }

                if (bw_Vyhledat.IsBusy)
                {
                    bw_Vyhledat.CancelAsync();
                    while (bw_Vyhledat.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }

                ProgressIndicatorVyrobekStart();

                Fask.Interfaces.Filtry.Import_SKzNC_ListFiltr filtr = new Fask.Interfaces.Filtry.Import_SKzNC_ListFiltr();
                if (!CreateFilter(ref filtr))
                    return;

                int FirstDisplayedScrollingRowIndex = this.dgImportPOHODA.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

                bw_Vyhledat.RunWorkerAsync(filtr);

                if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dgImportPOHODA.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dgImportPOHODA.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index
            }
            catch (Exception ex)
            {
                ProgressIndicatorVyrobekStop();
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        /// <summary>
        /// Metoda pro import dat
        /// </summary>
        private void PerformImport()
        {
            try
            {
                if (bw_import.IsBusy)
                {
                    bw_import.CancelAsync();
                    while (bw_import.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }

                Fask.Interfaces.DataSets_Import.ImportPOHODA_FromExcel.FASK_ZASOBY_IMPORT_POHODA_SKzNCDataTable dt_tmp = new ImportPOHODA_FromExcel.FASK_ZASOBY_IMPORT_POHODA_SKzNCDataTable();

                if (SelectedRows != null)
                {
                    try
                    {
                        {
                            if (this.SelectedRows.Count == 0)
                            {
                                MessageBox.Show("Neni vybrán žádný řádek.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                                return;
                            }

                            foreach (DataGridViewRow material in this.SelectedRows)
                            {
                                ImportPOHODA_FromExcel.FASK_ZASOBY_IMPORT_POHODA_SKzNCRow row = ((DataRowView)material.DataBoundItem).Row as ImportPOHODA_FromExcel.FASK_ZASOBY_IMPORT_POHODA_SKzNCRow;

                                dt_tmp.ImportRow(row);
                            }

                            dt_tmp.AcceptChanges();


                        }
                    }
                    catch (Exception ex)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                        MessageBox.Show("Nepodařilo se import.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }


                if (dt_tmp.Count > 0)
                {
                    ProgressIndicatorVyrobekStart();
                    bw_import.RunWorkerAsync(dt_tmp);
                }
                else
                {
                    MessageBox.Show("Neni vybrán žádný řádek.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return;
                }
            }
            catch (Exception ex)
            {
                ProgressIndicatorVyrobekStop();
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        /// <summary>
        /// Metoda pro import dat
        /// </summary>
        private void PerformImportEXCEL()
        {
            try
            {
                if (bw_ImportEXCEL.IsBusy)
                {
                    bw_ImportEXCEL.CancelAsync();
                    while (bw_ImportEXCEL.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }

                ProgressIndicatorVyrobekStart();

                bool del = false;
                var dr =MessageBox.Show("Data před importem budou smazány!", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                if(dr == DialogResult.OK)
                {
                    del = true;
                }
                else
                {
                    return;
                }

                string cesta = string.Empty;

                using (System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog())
                {
                    openFileDialog1.Filter = "Soubory Microsoft Office Excel (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                    openFileDialog1.FilterIndex = 1;
                    openFileDialog1.RestoreDirectory = true;

                    string dir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                    openFileDialog1.InitialDirectory = dir;

                    if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        cesta = openFileDialog1.FileName;
                    }
                }

                ArgumentExportEXCEL argument = new ArgumentExportEXCEL() { 
                    Delete = del,
                    Path = cesta
                };

                bw_ImportEXCEL.RunWorkerAsync(argument);
            }
            catch (Exception ex)
            {
                ProgressIndicatorVyrobekStop();
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

                if (rowDodavateleZasoby == null)
                {
                    MessageBox.Show("Není vybrán záznam pro úpravu", this.Text, MessageBoxButtons.OK);
                    return;
                }

                using (var frm = new FormImport_SKzNC_Edit())
                {
                    frm.rowImportEdit = rowDodavateleZasoby;
                    frm.Text = "Úprava Import řádku";
                    if (frm.ShowDialog(this) != DialogResult.OK)
                        return;

                }

                PerformVyhledat();
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

                using (var frm = new FormImport_SKzNC_Edit())
                {
                    frm.Text = "Nové Import řádku";
                    if (frm.ShowDialog(this) != DialogResult.OK)
                        return;

                    PerformVyhledat();
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Click eventy menu a button

        private void tsmi_importZasobyDodavetele_Click(object sender, EventArgs e)
        {
            if (!opravneniImport)
            {
                MessageBox.Show("Nemáte dostatečné oprávnění pro tuto akci!", "Oprávnění", MessageBoxButtons.OK);
                return;
            }

            PerformImport();
        }

        private void tsmikonec_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void buttonVyhledat_Click(object sender, EventArgs e)
        {
            #region prace s datagridem - nastaveni numeric hodnot na pocet desetinnych mist
            try
            {
                // Procházení sloupců v DataGridView
                foreach (DataGridViewColumn column in dgImportPOHODA.Columns)
                {
                    // Získání typu dat pro sloupec
                    Type dataType = null;

                    if (dgImportPOHODA.DataSource is DataTable dataTable)
                    {
                        // Pokud je datový zdroj DataTable
                        if (dataTable.Columns.Contains(column.DataPropertyName))
                        {
                            dataType = dataTable.Columns[column.DataPropertyName].DataType;
                        }
                    }
                    else if (dgImportPOHODA.DataSource is BindingSource bindingSource)
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

        private void buttonOdznacitVse_Click(object sender, EventArgs e)
        {
            try
            {
                this.dgImportPOHODA.ClearSelection();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonOznacitVse_Click(object sender, EventArgs e)
        {
            try
            {
                this.dgImportPOHODA.SelectAll();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsmiimportDoFASKZEXCEL_Click(object sender, EventArgs e)
        {

            if (!opravneniImport)
            {
                MessageBox.Show("Nemáte dostatečné oprávnění pro tuto akci!", "Oprávnění", MessageBoxButtons.OK);
                return;
            }

            try
            {
                PerformImportEXCEL();
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        }

        private void tsmikontrolaKonzistenceDat_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Filtre click metody

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
            PerformNastavitFiltr(rowFiltr);
        }

        #endregion

        #region Filtry

        /// <summary>
        /// Vytvořeni filtru pro dotazeni dat
        /// </summary>
        /// <param name="filtr"></param>
        /// <returns></returns>
        private bool CreateFilter(ref Fask.Interfaces.Filtry.Import_SKzNC_ListFiltr filtr)
        {
            if (filtr == null)
                filtr = new Fask.Interfaces.Filtry.Import_SKzNC_ListFiltr();

            return true;
        }


        /// <summary>
        /// Odstrani vybrany filtr
        /// </summary>
        public void PerformOdebratFiltr()
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

        /// <summary>
        /// Ulozeni filtru do souboru.
        /// </summary>
        public void PerformPridatFiltr()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                Fask.Interfaces.Filtry.Import_SKzNC_ListFiltr filtr = new Fask.Interfaces.Filtry.Import_SKzNC_ListFiltr();
                string nazev = string.Empty;
                DialogResult dr = Forms.InputBox.Show("Název filtru", "Zadejte název filtru", string.Empty, false, out nazev);
                if (dr != System.Windows.Forms.DialogResult.OK)
                    return;

                string nameFile = Guid.NewGuid().ToString() + "_" + this.GetType().ToString();
                this.dgImportPOHODA.SaveConfiguration(nameFile);

                bool result = CreateFilter(ref filtr);
                filtr.NameFileDataGridView = nameFile;

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

        /// <summary>
        /// Upravi zvoleny filtr.
        /// </summary>
        public void PerformZmenitFiltr()
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

                var filtr = rowFiltr;

                string nameFile = string.Empty;

                if (string.IsNullOrEmpty(filtr.NameFileDataGridView))
                {
                    nameFile = Guid.NewGuid().ToString() + "_" + this.GetType().ToString();
                }
                else
                    nameFile = filtr.NameFileDataGridView;

                this.dgImportPOHODA.SaveConfiguration(nameFile);

                bool result = CreateFilter(ref filtr);
                filtr.NameFileDataGridView = nameFile;

                if (!result)
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
        public void PerformNastavitFiltr(Fask.Interfaces.Filtry.Import_SKzNC_ListFiltr filtr)
        {
            try
            {
                if (filtr == null)
                    return;

                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                PerformVycistitFiltr();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show("Nepodařilo se načíst data filtru.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region BACKGRUND workery pro dotahovani tabulky

        private void bw_Vyhledat_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Fask.Interfaces.Filtry.Import_SKzNC_ListFiltr filtr = (Fask.Interfaces.Filtry.Import_SKzNC_ListFiltr)e.Argument;
                Fask.Interfaces.DataSets_Import.ImportPOHODA_FromExcel ds = new Fask.Interfaces.DataSets_Import.ImportPOHODA_FromExcel();

                if (bw_Vyhledat.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                //providerVazby.ITEMNMBR_Materialy = rowVyrobek.ITEMNMBR;

                // nacteni dat v oddelenem vlakne
                if ((provider != null) && provider is Fask.Interfaces.ImportnyMustky.IImportnyMustky_GetImportPohoda_SKzNC)
                    ds = ((Fask.Interfaces.ImportnyMustky.IImportnyMustky_GetImportPohoda_SKzNC)provider).GetImportPohoda_SKzNC(filtr);
                else
                    throw new Exception("IImportnyMustky_GetImportPohoda_SKzNC not implementet");
                //providerVazby.ITEMNMBR_Materialy = String.Empty;

                if (bw_Vyhledat.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                e.Result = ds;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void bw_Vyhledat_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                // check error, check cancel, then use result
                if (e.Error != null)
                {
                    // handle the error
                    this.dsImportPOHODA = new ImportPOHODA_FromExcel();
                    bsImportPOHODA.DataSource = this.dsImportPOHODA;
                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, e.Error);
                    MessageBox.Show(e.Error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    SetStatusLabelText_Events(-1);

                }
                else if (e.Cancelled)
                {
                    // handle cancellation
                    this.dsImportPOHODA = new ImportPOHODA_FromExcel();
                    bsImportPOHODA.DataSource = this.dsImportPOHODA;

                    SetStatusLabelText_Events(-1);
                }
                else
                {
                    // uspesne dokonceno ...
                    // use it on the UI thread
                    this.dsImportPOHODA = (ImportPOHODA_FromExcel)e.Result;
                    if (this.dsImportPOHODA == null)
                        this.dsImportPOHODA = new ImportPOHODA_FromExcel();

                    bsImportPOHODA.DataSource = this.dsImportPOHODA;

                    if (dsImportPOHODA.FASK_ZASOBY_IMPORT_POHODA_SKzNC.Count == 0)
                        SetStatusLabelText_Events(-1);
                    else
                    {
                        foreach (DataGridViewRow row in dgImportPOHODA.SelectedRows)
                        {
                            SetStatusLabelText_Events(row.Index);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ProgressIndicatorVyrobekStop();
            }
        }

        private void ProgressIndicatorVyrobekStop()
        {
            progressIndicatorVyrobek.Stop();
            progressIndicatorVyrobek.Visible = false;
        }

        private void ProgressIndicatorVyrobekStart()
        {
            try
            {
                // prepocet stredu datagridu
                this.progressIndicatorVyrobek.Location = new Point(this.dgImportPOHODA.Location.X + (this.dgImportPOHODA.Width / 2) - (progressIndicatorVyrobek.Size.Width / 2), this.dgImportPOHODA.Location.Y + (this.dgImportPOHODA.Height / 2) - (progressIndicatorVyrobek.Size.Height / 2));
            }
            catch { }
            progressIndicatorVyrobek.Start();
            progressIndicatorVyrobek.Visible = true;
        }

        #endregion

        #region BACKGROUN workery pro import

        private void bw_import_DoWork(object sender, DoWorkEventArgs e)
        {

            try
            {
                Fask.Interfaces.DataSets_Import.ImportPOHODA_FromExcel.FASK_ZASOBY_IMPORT_POHODA_SKzNCDataTable dt = (ImportPOHODA_FromExcel.FASK_ZASOBY_IMPORT_POHODA_SKzNCDataTable)e.Argument;

                if (bw_import.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                bool status = true;
                //Zavolani metody s backgroungworkerem....

                if (provider is Fask.Interfaces.ImportnyMustky.IImportnyMustky_ImportPohoda_SKzNC)
                    status = ((Fask.Interfaces.ImportnyMustky.IImportnyMustky_ImportPohoda_SKzNC)provider).ImportPohoda_SKzNC(dt);
                else
                    throw new Exception("IImportnyMustky_ImportPohoda_SKzNC not implementet");

                if (!status)
                {
                    e.Cancel = true;
                    e.Result = "Neimportováno";
                    return;
                    //throw new Exception(status);
                }


                if (bw_import.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                e.Result = status;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bw_import_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            try
            {
                if (e.Error != null)
                {
                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, e.Error);
                    MessageBox.Show(e.Error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (e.Cancelled)
                {
                    Fask.Logging.ExceptionHandler2.Handle(LogLevel.Error, ((bool)e.Result).ToString());
                    MessageBox.Show(((bool)e.Result).ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if((bool)e.Result)
                    {
                        MessageBox.Show("Importováno :).", "Import", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    }
                    else
                    {
                        MessageBox.Show("NEImportováno :(.", "Import", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    }

                    
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ProgressIndicatorVyrobekStop();
                PerformVyhledat();
            }

        }

        #endregion

        #region BACKGROUN worker pro import z EXCELu


        private class ArgumentExportEXCEL {
            public string Path = string.Empty;
            public bool Delete = false;
        }

    private void bw_ImportEXCEL_DoWork(object sender, DoWorkEventArgs e)
        {
            ArgumentExportEXCEL arg = (ArgumentExportEXCEL)e.Argument;

            Fask.Interfaces.DataSets_Import.ImportPOHODA_FromExcel ds = new ImportPOHODA_FromExcel();

            if (bw_ImportEXCEL.CancellationPending)
            {
                e.Cancel = true;
                return;
            }


            if(arg.Delete)
            {
                foreach (var row in dsImportPOHODA.FASK_ZASOBY_IMPORT_POHODA_SKzNC)
                {
                    if ((provider != null) && (provider is Fask.Interfaces.ImportnyMustky.IImportnyMustky_Delete_ImportSKzNC))
                    {
                        ((Fask.Interfaces.ImportnyMustky.IImportnyMustky_Delete_ImportSKzNC)provider).Delete_ImportSKzNC(row.DEX_ROW_ID);
                    }
                    else
                    {
                        throw new NotImplementedException("Provider neobsahuje implemetaci IImportnyMustky_Delete_ImportSKzNC");
                    }
                }
            }


            ds.GetDataTableFromExcel(arg.Path, prov: this.provider);

            foreach (var item in ds.FASK_ZASOBY_IMPORT_POHODA_SKzNC)
            {
                int? refAg;

                if ((provider != null) && provider is Fask.Interfaces.ImportnyMustky.IImportnyMustky_ImportPohoda_SKzNC_AddRefAg)
                    refAg = ((Fask.Interfaces.ImportnyMustky.IImportnyMustky_ImportPohoda_SKzNC_AddRefAg)provider).AddRefAg(item);
                else
                    throw new Exception("IImportnyMustky_ImportPohoda_SKzNC_AddRefAg not implementet");

                if (refAg.HasValue)
                {
                    item.RefAg = refAg.Value;
                }
                else
                {
                    item.SetRefAgNull();
                    item.Status_Err += (int)MyErrorEnum.RefAg_NOT_EXIST_POHODA;
                } 
            }

            ds.AcceptChanges();

            foreach (var item in ds.FASK_ZASOBY_IMPORT_POHODA_SKzNC)
            {
                item.SetAdded();
            }
            
            int cnt = 0;
            //Inser to DB
            if ((provider != null) && provider is Fask.Interfaces.ImportnyMustky.IImportnyMustky_UpdateImportPohoda_SKzNC)
                cnt = ((Fask.Interfaces.ImportnyMustky.IImportnyMustky_UpdateImportPohoda_SKzNC)provider).UpdateImportPohoda_SKzNC(ds);
            else
                throw new Exception("IImportnyMustky_UpdateImportPohoda_SKzNC not implementet");

            if (bw_ImportEXCEL.CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            if (ds.FASK_ZASOBY_IMPORT_POHODA_SKzNC.Count == cnt)
                e.Result = ds.FASK_ZASOBY_IMPORT_POHODA_SKzNC.Count;
            else
                e.Result = -1;


        }

        private void bw_ImportEXCEL_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null)
                {
                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, e.Error);
                    MessageBox.Show(e.Error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (e.Cancelled)
                {
                    Fask.Logging.ExceptionHandler2.Handle(LogLevel.Error, (string)e.Result);
                    MessageBox.Show((string)e.Result, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if(((int)e.Result) == -1)
                        MessageBox.Show(string.Format("Importován špatný počet řádků :(", (int)e.Result), "Import", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    else
                        MessageBox.Show(string.Format("Importováno z EXCEL souboru:'{0}' řádků :)", (int)e.Result), "Import", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ProgressIndicatorVyrobekStop();
                PerformVyhledat();
            }
        }

        #endregion

        #region Exporty

        private void exportDoCSVVseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                this.dgImportPOHODA.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                this.dgImportPOHODA.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
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

                this.dgImportPOHODA.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                dgImportPOHODA.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
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

                dgImportPOHODA.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                dgImportPOHODA.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Pomocne metody + eventy objektu

        private void advancedDataGridViewSearchToolBar1_Search(object sender, Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs e)
        {
            bool restartsearch = true;
            int startColumn = 0;
            int startRow = 0;
            if (!e.FromBegin)
            {
                bool endcol = dgImportPOHODA.CurrentCell.ColumnIndex + 1 >= dgImportPOHODA.ColumnCount;
                bool endrow = dgImportPOHODA.CurrentCell.RowIndex + 1 >= dgImportPOHODA.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dgImportPOHODA.CurrentCell.ColumnIndex;
                    startRow = dgImportPOHODA.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dgImportPOHODA.CurrentCell.ColumnIndex + 1;
                    startRow = dgImportPOHODA.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dgImportPOHODA.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dgImportPOHODA.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dgImportPOHODA.CurrentCell = c;
        }

        private void SetStatusLabelText_Events(int index)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    SetStatusLabelText_Events(index);
                }));

                return;
            }

            tssl_ImportPOHODA_Count.Text = string.Format("{0}/{1}", index + 1, dsImportPOHODA.FASK_ZASOBY_IMPORT_POHODA_SKzNC.Count);
        }

        private void dgVyrobek_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgImportPOHODA.SelectedRows)
            {
                SetStatusLabelText_Events(row.Index);
            }
        }



        #endregion

        #region Create Delete Edit

        private void tsmiOdstranit_Click(object sender, EventArgs e)
        {

            if (!opravneniEditace)
            {
                MessageBox.Show("Nemáte dostatečné oprávnění pro tuto akci!", "Oprávnění", MessageBoxButtons.OK);
                return;
            }

            if (SelectedRows != null)
            {
                try
                {
                    
                        if (this.SelectedRows.Count == 0)
                        {
                            MessageBox.Show("Neni vybrán žádný řádek.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            return;
                        }

                        foreach (DataGridViewRow material in this.SelectedRows)
                        {
                            ImportPOHODA_FromExcel.FASK_ZASOBY_IMPORT_POHODA_SKzNCRow row = ((DataRowView)material.DataBoundItem).Row as ImportPOHODA_FromExcel.FASK_ZASOBY_IMPORT_POHODA_SKzNCRow;

                            if ((provider != null) && (provider is Fask.Interfaces.ImportnyMustky.IImportnyMustky_Delete_ImportSKzNC))
                            {
                                ((Fask.Interfaces.ImportnyMustky.IImportnyMustky_Delete_ImportSKzNC)provider).Delete_ImportSKzNC(row.DEX_ROW_ID);
                            }
                            else
                            {
                                throw new NotImplementedException("Provider neobsahuje implemetaci IImportnyMustky_Delete_ImportSKzNC");
                            }
                        }

                        PerformVyhledat();
                    
                }
                catch (Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                    MessageBox.Show("Nepodařilo se odstranit zboží.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void tsmiUpravit_Click(object sender, EventArgs e)
        {

            if (!opravneniEditace)
            {
                MessageBox.Show("Nemáte dostatečné oprávnění pro tuto akci!", "Oprávnění", MessageBoxButtons.OK);
                return;
            }

            PerformEditRecord();
            PerformVyhledat();
        }

        private void tsmiNovy_Click(object sender, EventArgs e)
        {

            if (!opravneniEditace)
            {
                MessageBox.Show("Nemáte dostatečné oprávnění pro tuto akci!", "Oprávnění", MessageBoxButtons.OK);
                return;
            }

            PerformCreateRecord();
            PerformVyhledat();
        }

        #endregion

        private void dgImportPOHODA_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewRow row in dgImportPOHODA.Rows)
            {
                int status = int.Parse(row.Cells["Status_ErrDataGridViewTextBoxColumn"].Value.ToString().Trim());

                

                if (status!= 0 )
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 99, 71); // Tomato

                }


            }
        }

        private void dgImportPOHODA_DoubleClick(object sender, EventArgs e)
        {
            if (SelectedRows != null)
            {
                try
                {

                    if (this.SelectedRows.Count != 1)
                    {
                        MessageBox.Show("Je vybráno víc jak 1 řádek.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        return;
                    }

                    ImportPOHODA_FromExcel.FASK_ZASOBY_IMPORT_POHODA_SKzNCRow row = ((DataRowView)this.SelectedRows[0].DataBoundItem).Row as ImportPOHODA_FromExcel.FASK_ZASOBY_IMPORT_POHODA_SKzNCRow;

                    List<MyErrorEnum> err = new List<MyErrorEnum>();

                    if (row.Status_Err > 0)
                    {
                        for (var flagIterator = 0; flagIterator < 32; flagIterator++)
                        {
                            int bitValue = 1 << flagIterator;

                            if ((row.Status_Err & bitValue) != 0)
                            {
                                if (Enum.IsDefined(typeof(MyErrorEnum), bitValue))
                                    err.Add((MyErrorEnum)bitValue);
                            }
                        }

                        using (var form = new FormImport_SKzNC_ErrList())
                        {
                            form.MyErrorEnums = err;

                            form.ShowDialog();
                        }
                    }

                }
                catch (Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                    MessageBox.Show("Nepodařilo se Otevřit detail.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
