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
using FirebirdSql.Data.FirebirdClient;
using System.Reflection;
using Konzola.Vyroba.Rozbory;
using System.IO;
using Konzola.Prijem;

namespace Konzola.Prijem
{
    public partial class FormDavkyPrijmuList : Form
    {




        private Fask.Interfaces.IMES providerPrijem = null;
        //private Fask.Interfaces.IVyrobaKonzola providerUzivatele = null;

        private List<Fask.Interfaces.Filtry.PrijemDavkyFiltr> filtry = new List<Fask.Interfaces.Filtry.PrijemDavkyFiltr>();
        
        /// <summary>
        /// Zvolené id před seřazením
        /// </summary>
        string sortedID = string.Empty;

        public Fask.Interfaces.DataSets.Prijem.CZMST_PERow  SelectedRow
        {
            get
            {
                try
                {
                    return ((DataRowView)(dgPrijem.BindingContext[bsPrijem].Current)).Row as Fask.Interfaces.DataSets.Prijem.CZMST_PERow;
                }
                catch
                {
                    return null;
                }
            }
        }

        private List<Fask.Interfaces.DataSets.Prijem.CZMST_PERow> CZMST_PE_selectedRows
        {
            //get
            //{
            //    List<Fask.Interfaces.DataSets.Prijem.CZMST_PERow> rows = new List<Fask.Interfaces.DataSets.Prijem.CZMST_PERow>();

            //    foreach (DataGridViewRow selectedRow in dgPrijem.SelectedRows)
            //    {
            //        Fask.Interfaces.DataSets.Prijem.CZMST_PERow row = ((DataRowView)selectedRow.DataBoundItem).Row as Fask.Interfaces.DataSets.Prijem.CZMST_PERow;
            //        rows.Add(row);
            //    }

            //    return rows;
            //}

            get
            {
                return dgPrijem.SelectedRows
                    .Cast<DataGridViewRow>()
                    .OrderBy(r => r.Index)               // zarovnání na vizuální pořadí v gridu
                    .Select(r => ((DataRowView)r.DataBoundItem).Row as Fask.Interfaces.DataSets.Prijem.CZMST_PERow)
                    .Where(r => r != null)
                    .ToList();
            }
        }

        /// <summary>
        /// Vybrany filtr.
        /// </summary>
        private Fask.Interfaces.Filtry.PrijemDavkyFiltr rowFiltr
        {
            get
            {
                try
                {
                    return tscbFiltry.SelectedItem as Fask.Interfaces.Filtry.PrijemDavkyFiltr;
                }
                catch
                {
                    return null;
                }
            }
        }


        #region Eventy formu

        public FormDavkyPrijmuList()
        {
            InitializeComponent();
            this.dgPrijem.UpdateColumnHeaderCellsByDatasource();
            panelButtons.Menu = menuStrip1;
        }

        private bool opravneni = false;

        public FormDavkyPrijmuList(bool opravneni) : this()
        {

            //this.dg_OdvodEvents.MultiSelect = allowMultiSelect;
            //this.Zobrazeni = typZobrazeni;

            if (!opravneni)
            {
                menuStrip1.Items.Remove(tsmiPolozka);
                tsmiAkce.DropDownItems.Remove(tsmiUpravit);
                tsmiAkce.DropDownItems.Remove(tsmiOdstranit);
            }

            //if (Zobrazeni == Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST)
            //{
            //    panelButtonsZobrazeniList.Menu = menuStrip2;
            //    this.WindowState = FormWindowState.Maximized;
            //}

            this.opravneni = opravneni;



        }

        private void FormDavkyPrijmuList_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                this.WindowState = FormWindowState.Maximized;

                // načtení konfigurace datagridu z nastavení aplikace
                this.dgPrijem.LoadConfiguration(this.GetType().ToString());

                panelButtons.LoadConfiguration(this.GetType().ToString());
                panelButtons.Init();
                panelButtons.Size = new Size(85, 700);

                advancedDataGridViewSearchToolBar1.SetColumns(dgPrijem.Columns);

                //// načtení konfigurace vytvořených filtrů
                this.filtry = Extensions.ListExt.ReadFromXML<Fask.Interfaces.Filtry.PrijemDavkyFiltr>(this.GetType().ToString() + ".filtr");
                tscbFiltry.ComboBox.DataSource = this.filtry;
                tscbFiltry.SelectedItem = null;
                tscbFiltry.ComboBox.DropDownWitdhAutosize();

                // inicializace providera
                InitProvider();

                if (providerPrijem == null)
                    throw new Exception("Provider 'Prijem' není inicializován");


                chB_Stazene.Checked = true;
                chB_Spracovane.Checked = true;
                chB_Mrtve.Checked = true;

                //if (providerUzivatele == null)
                //    throw new Exception("Provider 'Uživatelé' není inicializován");

                //System.Threading.Thread loadThread = new System.Threading.Thread(LoadDataAsync);
                //loadThread.IsBackground = true;
                //loadThread.Start();
                buttonVyhledat.Focus();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormDavkyPrijmuList_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.dgPrijem.SaveConfiguration(this.GetType().ToString());
                panelButtons.SaveConfiguration(this.GetType().ToString());

                // ulozeni vsech filtru
                this.filtry.WriteXML(this.GetType().ToString() + ".filtr");
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormDavkyPrijmuList_KeyDown(object sender, KeyEventArgs e)
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

        private void FormDavkyPrijmuList_Shown(object sender, EventArgs e)
        {
            try
            {
                this.progressIndicator1.Size = new Size(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ostatni[0].ProgressIndicatorSize, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ostatni[0].ProgressIndicatorSize);
                this.progressIndicator1.Location = new Point(this.dgPrijem.Location.X + (this.dgPrijem.Width / 2) - (progressIndicator1.Size.Width / 2), this.dgPrijem.Location.Y + (this.dgPrijem.Height / 2) - (progressIndicator1.Size.Height / 2));
            }
            catch { }
            try
            {
                // 11.8.2016 PeV: zakomentovano automaticke vyhledavani pri otevreni formulare
                //PerformOK();
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
                
                if (providerPrijem == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Prijem.IPrijem2).IsAssignableFrom(t))
                            {
                                providerPrijem = (Fask.Interfaces.Prijem.IPrijem2)providerAssemlby.CreateInstance(t.FullName);
                                if (providerPrijem != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                providerPrijem.InitProvider();

            

                //if (providerUzivatele == null) //inicializace se provede pouze pokud nebyla provedena ... 
                //{
                //    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                //    Type[] types = providerAssemlby.GetTypes();
                //    foreach (Type t in types)
                //    {
                //        try
                //        {
                //            if (typeof(Fask.Interfaces.Ciselniky.IUzivatele2).IsAssignableFrom(t))
                //            {
                //                providerUzivatele = (Fask.Interfaces.Ciselniky.IUzivatele2)providerAssemlby.CreateInstance(t.FullName);
                //                if (providerUzivatele != null)
                //                    break;
                //            }
                //        }
                //        catch { }
                //    }
                //    //return config;
                //}

                // nastaveni connection stringu
                //if (providerUzivatele != null)
                //    ((Fask.Interfaces.Ciselniky.IUzivatele)providerUzivatele).ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;

                //if ((providerUzivatele != null) && (providerUzivatele is Fask.Interfaces.Parametry.IParametry2_ConnectionString))
                //    ((Fask.Interfaces.Parametry.IParametry2_ConnectionString)providerUzivatele).ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonVyhledat_Click(object sender, EventArgs e)
        {
            #region prace s datagridem - nastaveni numeric hodnot na pocet desetinnych mist
            try
            {
                // Procházení sloupců v DataGridView
                foreach (DataGridViewColumn column in dgPrijem.Columns)
                {
                    // Získání typu dat pro sloupec
                    Type dataType = null;

                    if (dgPrijem.DataSource is DataTable dataTable)
                    {
                        // Pokud je datový zdroj DataTable
                        if (dataTable.Columns.Contains(column.DataPropertyName))
                        {
                            dataType = dataTable.Columns[column.DataPropertyName].DataType;
                        }
                    }
                    else if (dgPrijem.DataSource is BindingSource bindingSource)
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

            PerformOK();
        }

        /// <summary>
        /// Vytvoreni filtru, ktery se pouzije.
        /// </summary>
        /// <param name="filtr"></param>
        /// <returns></returns>
        private bool CreateFilter(ref Fask.Interfaces.Filtry.PrijemDavkyFiltr filtr)
        {
            if (filtr == null)
                filtr = new Fask.Interfaces.Filtry.PrijemDavkyFiltr();

            filtr.CountEntries = cbCountEntries.Text.Trim();
            filtr.PONUMBER = cbPONUMBER.Text.Trim();
            filtr.ITEMNMBR = cbITEMNMBR.Text.Trim();

            filtr.UvolneneDavky = chB_Uvolnene.Checked;
            filtr.NEUvolneneDavky = chB_NEUvolnene.Checked;
            filtr.SpracovaneDavky = chB_Spracovane.Checked;
            filtr.StazeneDavky = chB_Stazene.Checked;
            filtr.MrtveDavky = chB_Mrtve.Checked;

            return true;
        }

        int? Index = null;

        private void PerformOK()
        {
            try
            {
                //DataTable dtchanged = this.dsPrijem.CZMST_PE.GetChanges();
                //if (dtchanged != null && dtchanged.Rows.Count > 0) //obsahuje zmeny ... 
                //{

                //    //MaR zakomentoval 11.3.2024
                //    //DialogResult dr = MessageBox.Show("Obsahuje změny, pokračovat?", "data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                //    //if (dr == System.Windows.Forms.DialogResult.No)
                //    //    return;
                //}

                if (bwLoadPrijem.IsBusy)
                {
                    bwLoadPrijem.CancelAsync();
                    while (bwLoadPrijem.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }

                ProgressIndicatorStart();

                Fask.Interfaces.Filtry.PrijemDavkyFiltr filtr = new Fask.Interfaces.Filtry.PrijemDavkyFiltr();
                if (!CreateFilter(ref filtr))
                    return;

                int FirstDisplayedScrollingRowIndex = this.dgPrijem.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

                if(SelectedRow != null)
                    Index = SelectedRow.DEX_ROW_ID;


                bwLoadPrijem.RunWorkerAsync(filtr);

                if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dgPrijem.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dgPrijem.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index
            }
            catch (Exception ex)
            {
                ProgressIndicatorStop();
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //finally
            //{
            //    this.dataGridView1.Focus();
            //}
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


        private void buttonOznacitVse_Click(object sender, EventArgs e)
        {
            try
            {
                this.dgPrijem.SelectAll();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonOdznacitVse_Click(object sender, EventArgs e)
        {
            try
            {
                this.dgPrijem.ClearSelection();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void konecToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void buttonKonec_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }


        private void bwSkladPohyb_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //bwLoadSkladPohyb.CancelAsync();
                Fask.Interfaces.Filtry.PrijemDavkyFiltr filtr = (Fask.Interfaces.Filtry.PrijemDavkyFiltr)e.Argument;
                Fask.Interfaces.DataSets.Prijem ds = new Fask.Interfaces.DataSets.Prijem();

                if (bwLoadPrijem.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                // nacteni dat v oddelenem vlakne
                //ds = ((Fask.Interfaces.Prijem.IPrijem)providerPrijem).GetFiltrovaneHlavicky(filtr);

                if ((providerPrijem != null) && (providerPrijem is Fask.Interfaces.Prijem.IPrijem2_GetFiltrovaneDavkyPE))
                     ds = ((Fask.Interfaces.Prijem.IPrijem2_GetFiltrovaneDavkyPE)providerPrijem).GetFiltrovaneDavkyPE(filtr);
                else
                    throw new NotImplementedException("Provider neimplementuje IPrijem2_GetFiltrovaneDavkyPI.");



                if (bwLoadPrijem.CancellationPending)
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

        private void bwSkladPohyb_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                // check error, check cancel, then use result
                if (e.Error != null)
                {
                    // handle the error
                    dsPrijem = new Fask.Interfaces.DataSets.Prijem();
                    bsPrijem.DataSource = dsPrijem;
                }
                else if (e.Cancelled)
                {
                    // handle cancellation
                    // TODO: maji se vymazat data z datasetu?? asi ano ...
                    //if (dsSkladPohyb == null)
                    dsPrijem = new Fask.Interfaces.DataSets.Prijem();
                    bsPrijem.DataSource = dsPrijem;
                }
                else
                {
                    // uspesne dokonceno ...
                    // use it on the UI thread
                    dsPrijem= (Fask.Interfaces.DataSets.Prijem)e.Result;
                    if (dsPrijem == null)
                        dsPrijem = new Fask.Interfaces.DataSets.Prijem();

                    bsPrijem.DataSource = dsPrijem;



                    if (Index != null )
                    {
                        var polozky = dsPrijem.CZMST_PE.Where(x => x.DEX_ROW_ID == Index);
                        if (polozky.Count() > 0)
                        {
                            var polozka = polozky.First();

                            int index = ((System.Data.DataView)bsPrijem.List).Table.Rows.IndexOf(polozka);
                            bsPrijem.Position = index;
                        }
                        Index = null;
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
                this.progressIndicator1.Location = new Point(this.dgPrijem.Location.X + (this.dgPrijem.Width / 2) - (progressIndicator1.Size.Width / 2), this.dgPrijem.Location.Y + (this.dgPrijem.Height / 2) - (progressIndicator1.Size.Height / 2));
            }
            catch { }
            progressIndicator1.Start();
            progressIndicator1.Visible = true;
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

                Fask.Interfaces.Filtry.PrijemDavkyFiltr filtr = rowFiltr;

                if (!CreateFilter(ref filtr))
                    return;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show("Nepodařilo se upravit filtr.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                Fask.Interfaces.Filtry.PrijemDavkyFiltr filtr = new Fask.Interfaces.Filtry.PrijemDavkyFiltr();
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

        private void tsbNastavit_Click(object sender, EventArgs e)
        {
            PerformNastavitFiltr(rowFiltr);
        }

        /// <summary>
        /// Nastavi zvoleny filtr do comboboxu, ...
        /// </summary>
        /// <param name="filtr"></param>
        private void PerformNastavitFiltr(Fask.Interfaces.Filtry.PrijemDavkyFiltr filtr)
        {
            try
            {
                if (filtr == null)
                    return;

                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                PerformVycistitFiltr();

                // countentries
                cbCountEntries.Text = filtr.CountEntries.ToString();

                // sopnumbe
                cbPONUMBER.Text = filtr.PONUMBER;
                cbITEMNMBR.Text = filtr.ITEMNMBR;
                chB_Uvolnene.Checked = filtr.UvolneneDavky ;
                chB_NEUvolnene.Checked = filtr.NEUvolneneDavky;

                chB_Spracovane.Checked = filtr.SpracovaneDavky;
                chB_Stazene.Checked = filtr.StazeneDavky;
                chB_Mrtve.Checked = filtr.MrtveDavky;

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show("Nepodařilo se načíst data filtru.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                int pos = this.bsPrijem.Find(dsPrijem.CZMST_PE.CountEntriesColumn.ColumnName, sortedID);
                this.bsPrijem.Position = pos;
            }
            catch { }
        }

        private enum Vyber
        {
            Storno,
            Odstranit_prirazeni,
            Zmenit_uzivatele
        }

        private void tsbVycistit_Click(object sender, EventArgs e)
        {
            PerformVycistitFiltr();
        }

        private void PerformVycistitFiltr()
        {
            try
            {
                cbCountEntries.Text = string.Empty;
                cbPONUMBER.Text = string.Empty;
                cbITEMNMBR.Text = string.Empty;
                chB_NEUvolnene.Checked = true;
                chB_Uvolnene.Checked = true;
                chB_Spracovane.Checked = true;
                chB_Stazene.Checked = true;
                chB_Mrtve.Checked = true;

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Generovat data 


        private void button_Generovat_Click(object sender, EventArgs e)
        {

                

        }

        private void GenerateDavka(int? CountEntries, string SKL_ID)
        {

            MessageBox.Show("Neimplementovano!!");
            //try
            //{

            //    if (bwExport.IsBusy)
            //    {
            //        bwExport.CancelAsync();
            //        while (bwExport.IsBusy)
            //        {
            //            Application.DoEvents();
            //        }
            //    }

            //    ProgressIndicatorStart();

            //    // Okno s zadanim čisla....


            //    DataDavkyGenerate data = new DataDavkyGenerate();
            //    string DescSklad = string.Empty;

            //    Fask.Interfaces.Classes.Sklad sklad = new Fask.Interfaces.Classes.Sklad();
            //    Fask.Interfaces.DataSets.Sklady.CZMST093Row RowSklad;

            //    if (CountEntries != null)
            //    {
            //        sklad.ID = SKL_ID.Trim();

            //        if ((providerPrijem != null) && (providerPrijem is Fask.Interfaces.Ciselniky.Sklady.ISklady2_GetSkladByID))
            //            RowSklad = ((Fask.Interfaces.Ciselniky.Sklady.ISklady2_GetSkladByID)providerPrijem).GetSkladByID(SKL_ID.Trim());
            //        else
            //            throw new NotImplementedException("Provider neimplementuje ISklady2_GetSkladByID.");

            //        DescSklad = RowSklad.skl_desc.Trim();


            //    }
            //    else
            //    {

            //        using (Ciselniky.FormSkladyList frmSklad = new Ciselniky.FormSkladyList(false, null, Fask.Interfaces.Classes.ZOBRAZENI_TYP.VYBER))
            //        {
            //            frmSklad.Text = "Výběr Sklad";

            //            if (frmSklad.ShowDialog(this) != DialogResult.OK)
            //            {
            //                ProgressIndicatorStop();
            //                return;
            //            }

            //            sklad.ID = frmSklad.SelectedRow.skl_id.Trim();
            //            DescSklad = frmSklad.SelectedRow.skl_desc.Trim();
            //        }
            //    }

            //    data.Davka = CountEntries;

            //    string TextOut = CountEntries == null ? string.Format("Zadejte číslo objednávky." + Environment.NewLine + "Pro sklad: '{0}'.", DescSklad) : string.Format("Zadejte číslo objednávky." + Environment.NewLine + "Pro sklad: '{0}'." + Environment.NewLine + "Sloučení do dávky: '{1}'", DescSklad, CountEntries);

            //    string sopnumber = string.Empty;
            //    DialogResult dr = Forms.InputBox.Show("Číslo objednávky", TextOut, string.Empty, false, out sopnumber);
            //    if (dr != System.Windows.Forms.DialogResult.OK)
            //    {
            //        ProgressIndicatorStop();
            //        return;
            //    }

            //    Fask.Interfaces.Classes.Objednavka objednavka = new Fask.Interfaces.Classes.Objednavka();

            //    objednavka.ID = sopnumber;
            //    objednavka.CisloDavky = string.Empty;

            //    data.sklad = sklad;
            //    data.objednavka = objednavka;

            //    bwExport.RunWorkerAsync(data);

            //}
            //catch (Exception ex)
            //{
            //    ProgressIndicatorStop();
            //    Log.Write(ex);
            //    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

    
        private void bwExport_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                //dsPrijem = (Fask.Interfaces.DataSets.Prijem)e.Result;
                 Fask.Interfaces.Classes.StatusInfo si = (Fask.Interfaces.Classes.StatusInfo)e.Result;

                // check error, check cancel, then use result
                if (e.Error != null)
                {
                    // handle the error

                }
                else if (e.Cancelled)
                {
                    //handle the cancelled
                }
                else
                {
                    // uspesne dokonceno ...

                    if (si.ID < 0)
                    {
                        MessageBox.Show(si.Description, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }


                    chB_Mrtve.Checked = false;
                    chB_NEUvolnene.Checked = true;
                    chB_Spracovane.Checked = false;
                    chB_Stazene.Checked = false;
                    chB_Uvolnene.Checked = false;


                    PerformOK();
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

        private void bwExport_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Fask.Interfaces.Classes.DataDavky data = (Fask.Interfaces.Classes.DataDavky)e.Argument;

                if (bwExport.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                // nacteni dat v oddelenem vlakne
                //ds = ((Fask.Interfaces.Prijem.IPrijem)providerPrijem).GetFiltrovaneHlavicky(filtr);

                //Fask.Interfaces.Classes.StatusInfo si;

                //if ((providerPrijem != null) && (providerPrijem is Fask.Interfaces.Prijem.IPrijem2_GenerateDavka))
                //    si = ((Fask.Interfaces.Prijem.IPrijem2_GenerateDavka)providerPrijem).Prijem_GenerateDavka(data.objednavka, data.sklad, data.Davka);
                //else
                //    throw new NotImplementedException("Provider neimplementuje IPrijem2_GenerateDavka.");



                if (bwExport.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                //e.Result = si;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion



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


                if (dgPrijem.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Je vybráno více záznamů než jeden.", this.Text, MessageBoxButtons.OK);
                    return;
                }

                #region doimplementovat!! MaR 22.2. 2024
                using (FormDavkyPrijemPEEdit frmuziv = new FormDavkyPrijemPEEdit())
                {
                    frmuziv.rowDavkaEdit = SelectedRow;
                    frmuziv.Text = "Úprava Davky";
                    if (frmuziv.ShowDialog(this) != DialogResult.OK)
                        return;

                    //this.SelectedRow.CountEntries = frmuziv.returnrow.CountEntries;
                    //this.SelectedRow.PONUMBER = frmuziv.returnrow.SOPNUMBE;
                    //this.SelectedRow.ITEMNMBR = frmuziv.returnrow.ITEMNMBR;
                    //this.SelectedRow.ITEMTYPE = frmuziv.returnrow.ITEMTYPE;
                    //this.SelectedRow.ITEMDESC = frmuziv.returnrow.ITEMDESC;
                    //this.SelectedRow.VNDDOCNM = frmuziv.returnrow.VNDDOCNM;
                    //this.SelectedRow.VNDITNUM = frmuziv.returnrow.VNDITNUM;
                    //this.SelectedRow.ORD = frmuziv.returnrow.ORD;
                    //this.SelectedRow.CZ_CarKod = frmuziv.returnrow.CZ_CarKod;
                    //this.SelectedRow.SKL_ID = frmuziv.returnrow.SKL_ID;
                    //this.SelectedRow.LOCNCODE = frmuziv.returnrow.LOCNCODE;
                    //this.SelectedRow.MJ = frmuziv.returnrow.MJ;
                    //this.SelectedRow.QTYSHPPD = frmuziv.returnrow.QTYSHPPD;
                    //this.SelectedRow.QTYPACK = frmuziv.returnrow.QTYPACK;
                    //this.SelectedRow.CZ_DatVyr_Track = frmuziv.returnrow.CZ_DatVyr_Track;
                    //this.SelectedRow.CZ_DatVyr_Delka = frmuziv.returnrow.CZ_DatVyr_Delka;
                    //this.SelectedRow.CZ_SerNum_Track = frmuziv.returnrow.CZ_SerNum_Track;
                    //this.SelectedRow.CZ_SerNum_Delka = frmuziv.returnrow.CZ_SerNum_Delka;
                    //this.SelectedRow.CZ_SW_Track = frmuziv.returnrow.CZ_SW_Track;
                    //this.SelectedRow.CZ_SW_Delka = frmuziv.returnrow.CZ_SW_Delka;
                    //this.SelectedRow.CZ_Doslo = frmuziv.returnrow.CZ_Doslo;
                    //this.SelectedRow.Note = frmuziv.returnrow.Note;
                    //this.SelectedRow.TYPEPAL = frmuziv.returnrow.TYPEPAL;
                    //this.SelectedRow.QTYPAL = frmuziv.returnrow.QTYPAL;
                    //this.SelectedRow.PRIORITY = frmuziv.returnrow.PRIORITY;
                    //this.SelectedRow.PRINTED = frmuziv.returnrow.PRINTED;
                    //this.SelectedRow.USERID = frmuziv.returnrow.USERID;
                    //this.SelectedRow.DEX_ROW_ID = frmuziv.returnrow.DEX_ROW_ID;

                    //this.dsPrijem.CZMST_PE.AcceptChanges();
                }
                #endregion

                PerformOK();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonKontrola_Click(object sender, EventArgs e)
        {
            PerformCheck();
        }

        private void PerformCheck()
        {
            MessageBox.Show("Neimplementovano!!");
        }

        private void PerformPrint()
        {

            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (SelectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if (CZMST_PE_selectedRows.Count > 1)
                {
                    //MessageBox.Show("Je vybráno více záznamů než jeden.", this.Text, MessageBoxButtons.OK);
                    //return;
                }

                //Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable dt = new Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable();

                //foreach (Fask.Interfaces.DataSets.Vyroba.Production_KonzolaRow row in Production_selectedRows)
                //{
                //    Fask.Interfaces.DataSets.Vyroba.Production_KonzolaRow newRow = dt.NewProduction_KonzolaRow();
                //    // Přenést data z původního řádku do nového řádku
                //    newRow.ItemArray = row.ItemArray.Clone() as object[];
                //    dt.AddProduction_KonzolaRow(newRow);
                //}

                Fask.Interfaces.DataSets.Prijem.CZMST_PEDataTable dt = new Fask.Interfaces.DataSets.Prijem.CZMST_PEDataTable();

                foreach (Fask.Interfaces.DataSets.Prijem.CZMST_PERow row in CZMST_PE_selectedRows)
                {
                    try
                    {
                        Fask.Interfaces.DataSets.Prijem.CZMST_PERow newRow = dt.NewCZMST_PERow();
                        // Ošetřit delší vstupní pole než počet sloupců tabulky
                        int copyLength = Math.Min(row.ItemArray.Length, dt.Columns.Count);
                        newRow.ItemArray = row.ItemArray.Take(copyLength).ToArray();
                        dt.AddCZMST_PERow(newRow);
                    }
                    catch (Exception ex)
                    {
                        // Ošetření vyjimky
                        // Můžete zde provést logování chyby nebo jiné požadované akce
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                    }
                }


                PrintReport(dt);

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        }

        private void PrintReport(Fask.Interfaces.DataSets.Prijem.CZMST_PEDataTable dt)
        {
            try
            {

                if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                {
                    foreach (var item in dt)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start sklady,davky prijmu TISK rdlc-------------------------------------");

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

                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END sklady,davky prijmu TISK rdlc-------------------------------------");
                    }

                }




                using (PrintReportLibrary.CoolPrintPreviewDialog plr = new PrintReportLibrary.CoolPrintPreviewDialog())
                {

                    plr.Params = new Microsoft.Reporting.WinForms.ReportParameter[]
                    {
                    new Microsoft.Reporting.WinForms.ReportParameter("Param_Firma", Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ostatni[0].Vyroba_Param_Firma),
                    new Microsoft.Reporting.WinForms.ReportParameter("Param_Adresa", Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ostatni[0].Vyroba_Param_Adresa),
                    new Microsoft.Reporting.WinForms.ReportParameter("Param_PSC", Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ostatni[0].Vyroba_Param_PSC),
                    new Microsoft.Reporting.WinForms.ReportParameter("Param_Obec", Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ostatni[0].Vyroba_Param_Obec),

                    new Microsoft.Reporting.WinForms.ReportParameter("Param_Datum", DateTime.Now.ToShortDateString()),
                   // new Microsoft.Reporting.WinForms.ReportParameter("Param_SOPDESC", SOPDESC)
                    };

                    plr.NazevDataTable = "DataSet";
                    plr.DataTable = dt;

                    List<PrintReportLibrary.TypeData> tmplist = new List<PrintReportLibrary.TypeData>();
                    tmplist.Add(PrintReportLibrary.TypeData.DataTable);
                    plr.typedata = new List<PrintReportLibrary.TypeData>(tmplist);
                    plr.Projekt = PrintReportLibrary.Projekt.MST;
                    plr.ShowPreview = true;

                    #region vybrani tiskove sablony
                    //formular na vybrani tiskove ulohy
                    DataGridViewRow dataGridView = new DataGridViewRow();



                    #region old vybrani tiskove  sablony
                    //// Vytvoření instance formuláře
                    //FormVyrobniPrikazList_Tisk form = new FormVyrobniPrikazList_Tisk();

                    //DialogResult result = form.ShowDialog();
                    //// Zobrazení formuláře
                    //if (result == DialogResult.OK)
                    //{
                    //    dataGridView = (DataGridViewRow)form.Tag;
                    //    // uživatel stiskl OK
                    //}
                    //else if (result == DialogResult.Cancel)
                    //{
                    //    // uživatel stiskl Cancel
                    //    MessageBox.Show("Není vybraná tisková šablona");
                    //    return;
                    //}
                    #endregion

                    #region new vybrani tiskove  sablony
                    // Vytvoření instance formuláře
                    //FormOdvod_TiskoveSablony form = new FormOdvod_TiskoveSablony(this.Text, FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Admin());

                    string klic = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Tisk_Klic_RDLC;
                    //FormOdvod_TiskoveSablony form = new FormOdvod_TiskoveSablony(this.Text, FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Admin(), klic);


                    FormOdvod_TiskoveSablony form = new FormOdvod_TiskoveSablony(this.Text, FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Admin(), klic, false);




                    string path = string.Empty;
                    DialogResult result = form.ShowDialog();
                    // Zobrazení formuláře
                    if (result == DialogResult.OK)
                    {
                        //dataGridView = (DataGridViewRow)form.Tag;
                        path = form.ResultString;
                        // uživatel stiskl OK
                    }
                    else if (result == DialogResult.Cancel)
                    {
                        // uživatel stiskl Cancel
                        MessageBox.Show("Není vybraná tisková šablona");
                        return;
                    }
                    #endregion

                    //plr.Path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PrintTemplates", dataGridView.Cells["Název tiskové šablony"].Value.ToString());


                    plr.Path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);

                    #endregion


                    //TODO ošetreny, zda existuje tiskova sestava
                    //plr.Path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Servis", "Print", "Templates", Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ostatni[0].VyrobnyPrikazTiskTemplate);

                    Fask.Logging.ExceptionHandler2.Handle(LogLevel.Info, "Path Tisk VPP:'" + plr.Path);
                    Fask.Logging.ExceptionHandler2.Handle(LogLevel.Info, "Path to PrinterDirectory:'" + MySystem.MyPath.PrintDirectory);

                    PrintReportLibrary.MyPath.PrintTemplateDirectory = Path.Combine(MySystem.MyPath.PrintDirectory);


                    //plr.CountEntries = CountEntries.ToString();
                    //plr.HlavickaKod = SOPNUMBE;
                    //plr.HlavickaKodIMG = SOPNUMBE.Trim();


                    plr.CountEntries = " ";
                    plr.HlavickaKod = "2";
                    plr.HlavickaKodIMG = "3";

                    try
                    {
                        plr.Print(this);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Tisková šablona je chybná, vyber jinou.");
                        // Ošetření výjimky při tisku
                        Fask.Logging.ExceptionHandler2.Handle(ex);

                        // Volání metody PrintReport znovu (rekurze)
                        PrintReport(dt);
                    }

                }
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }


        private void tiskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PerformPrint();
        }

        #region Exporty



        private void exportDoCSVVseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                this.dgPrijem.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                this.dgPrijem.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
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

                this.dgPrijem.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                dgPrijem.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
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

                dgPrijem.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                dgPrijem.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #endregion

        private void tsmiUpravit_Click(object sender, EventArgs e)
        {
            PerformEditRecord();
        }

        private void tsmiOdstranit_Click(object sender, EventArgs e)
        {
            PerformDeleteRecord();
        }

        private void PerformDeleteRecord()
        {
            try
            {

                if (SelectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro smazani", this.Text, MessageBoxButtons.OK);
                    return;
                }


                if (dgPrijem.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Je vybráno více záznamů než jeden.", this.Text, MessageBoxButtons.OK);
                    return;
                }

                //throw new NotImplementedException("Neimplementováno.");

                #region doimplementovat!! MaR 22.2. 2024


                if ((providerPrijem != null) && (providerPrijem is Fask.Interfaces.Prijem.IPrijem2_DeletePE))
                    ((Fask.Interfaces.Prijem.IPrijem2_DeletePE)providerPrijem).DeletePE(SelectedRow);
                else
                    throw new NotImplementedException("Provider neimplementuje IPrijem2_DeletePE.");
                #endregion

                PerformOK();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsmiGenerovatDavku_Click(object sender, EventArgs e)
        {
            GenerateDavka(null, string.Empty);
        }

        private void tsmiSloucitDavku_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Neimplementovano!!");
        }

        private void tsmiUvolnitDavku_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Neimplementovano!!");
        }

        private void tsmiKontrolaDavky_Click(object sender, EventArgs e)
        {
            PerformCheck();
        }

        private void advancedDataGridViewSearchToolBar1_Search(object sender, Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs e)
        {

            bool restartsearch = true;
            int startColumn = 0;
            int startRow = 0;
            if (!e.FromBegin)
            {
                bool endcol = dgPrijem.CurrentCell.ColumnIndex + 1 >= dgPrijem.ColumnCount;
                bool endrow = dgPrijem.CurrentCell.RowIndex + 1 >= dgPrijem.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dgPrijem.CurrentCell.ColumnIndex;
                    startRow = dgPrijem.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dgPrijem.CurrentCell.ColumnIndex + 1;
                    startRow = dgPrijem.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dgPrijem.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dgPrijem.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dgPrijem.CurrentCell = c;
        }

        private void stornovatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PerformStornoRecord();
        }

        private void PerformStornoRecord()
        {
            try
            {
                int pocet = 0;

                if (SelectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro storno", this.Text, MessageBoxButtons.OK);
                    return;
                }


                if (CZMST_PE_selectedRows.Count >= 1)
                {
                   // Fask.Interfaces.DataSets.Prijem.CZMST_PEDataTable dt = new Fask.Interfaces.DataSets.Prijem.CZMST_PEDataTable();

                    foreach (var row in CZMST_PE_selectedRows)
                    {

                        if(row.CZ_Doslo != 201)
                        {
                            row.CZ_Doslo = 201;


                            if ((providerPrijem != null) && (providerPrijem is Fask.Interfaces.Prijem.IPrijem2_UpdatePE))
                                pocet += ((Fask.Interfaces.Prijem.IPrijem2_UpdatePE)providerPrijem).UpdatePE(row) == true ? 1 : 0;
                            else
                                throw new NotImplementedException("Provider neimplementuje IPrijem2_UpdatePE.");
                           
                        }


                    }



                    //#region doimplementovat!! MaR 22.2. 2024


                    //if ((providerPrijem != null) && (providerPrijem is Fask.Interfaces.Prijem.IPrijem2_UpdatePE))
                    //    pocet = ((Fask.Interfaces.Prijem.IPrijem2_UpdatePE)providerPrijem).UpdatePE(dt);
                    //else
                    //    throw new NotImplementedException("Provider neimplementuje IPrijem2_UpdatePE.");
                    //#endregion
                }

                //throw new NotImplementedException("Neimplementováno.");
                string mess = string.Format("Stornováno {0} záznamů",pocet);
                MessageBox.Show(mess, this.Text, MessageBoxButtons.OK);


                PerformOK();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

