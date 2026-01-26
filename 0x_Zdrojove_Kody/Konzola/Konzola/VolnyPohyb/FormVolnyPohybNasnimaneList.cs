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
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Fask.Interfaces.Classes;
using Konzola.Vyroba.Rozbory;
using MST_Print_Server_ZPL_Printing;

namespace Konzola.VolnyPohyb
{
    public partial class FormVolnyPohybNasnimaneList: Form
    {

        #region Pomocna metoda pro Klavesovy vystup

        [DllImport("user32.dll")]
        public static extern int SetForegroundWindow(IntPtr hWnd);

        #endregion


        private Fask.Interfaces.IMES providerProdej = null;
        private Fask.BarCodeGraphics.IBarCodeGraphics providerTiskKod = null;
        private Fask.Interfaces.IMES providerTisk = null; //**DONE

        private List<Fask.Interfaces.Filtry.ProdejFiltr> filtry = new List<Fask.Interfaces.Filtry.ProdejFiltr>();
        
        /// <summary>
        /// Zvolené id před seřazením
        /// </summary>
        string sortedID = string.Empty;

        public Fask.Interfaces.DataSets.Prodej.CZMST_DIRow  Prodej_SelectedRow
        {
            get
            {
                try
                {
                    return ((DataRowView)(dgProdej.BindingContext[bsProdej].Current)).Row as Fask.Interfaces.DataSets.Prodej.CZMST_DIRow;
                }
                catch
                {
                    return null;
                }
            }
        }

        private List<Fask.Interfaces.DataSets.Prodej.CZMST_DIRow> Prodej_SelectedRows
        {
            //get
            //{
            //    List<Fask.Interfaces.DataSets.Prodej.CZMST_DIRow> rows = new List<Fask.Interfaces.DataSets.Prodej.CZMST_DIRow>();

            //    foreach (DataGridViewRow selectedRow in dgProdej.SelectedRows)
            //    {
            //        Fask.Interfaces.DataSets.Prodej.CZMST_DIRow row = ((DataRowView)selectedRow.DataBoundItem).Row as Fask.Interfaces.DataSets.Prodej.CZMST_DIRow;
            //        rows.Add(row);
            //    }

            //    return rows;
            //}

            get
            {
                return dgProdej.SelectedRows
                    .Cast<DataGridViewRow>()
                    .OrderBy(r => r.Index)               // zarovnání na vizuální pořadí v gridu
                    .Select(r => ((DataRowView)r.DataBoundItem).Row as Fask.Interfaces.DataSets.Prodej.CZMST_DIRow)
                    .Where(r => r != null)
                    .ToList();
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
                    return dgProdej.SelectedRows;
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
        private Fask.Interfaces.Filtry.ProdejFiltr rowFiltr
        {
            get
            {
                try
                {
                    return tscbFiltry.SelectedItem as Fask.Interfaces.Filtry.ProdejFiltr;
                }
                catch
                {
                    return null;
                }
            }
        }

        #region Eventy formu

        public FormVolnyPohybNasnimaneList()
        {
            InitializeComponent();

            this.dgProdej.UpdateColumnHeaderCellsByDatasource();
            panelButtons.Menu = menuStrip1;
        }

        private bool opravneni = false;

        public FormVolnyPohybNasnimaneList(bool opravneni) : this()
        {
            this.opravneni = opravneni;
            //this.dg_OdvodEvents.MultiSelect = allowMultiSelect;
            //this.Zobrazeni = typZobrazeni;

            //if (!opravneni)
            //{
            //    menuStrip1.Items.Remove(tsmiPolozka);
            //    tsmiAkce.DropDownItems.Remove(tsmiUpravit);
            //    tsmiAkce.DropDownItems.Remove(tsmiOdstranit);
            //}

            if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Import())
            {
                //MessageBox.Show("Uživatel nemá dostatečná oprávnění!", "Upozornení", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                //frmlist = new Vydej.FormVydejNasnimaneList(true);
            }
            else
            {
                menuStrip1.Items.Remove(tsmiAkce);
                tsmiAkce.DropDownItems.Remove(tsmiImportovatDavku);
            }

            //if (Zobrazeni == Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST)
            //{
            //    panelButtonsZobrazeniList.Menu = menuStrip2;
            //    this.WindowState = FormWindowState.Maximized;
            //}
        }


        private void FormVolnyPohybNasnimaneList_Load(object sender, EventArgs e)
        {
            try
            {


                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_P_ZP())
                {
                    tiskEtiketToolStripMenuItem.Enabled = true;



                }
                else
                {
                    tiskEtiketToolStripMenuItem.Enabled = false;
                    tsmiExport.DropDownItems.Remove(tiskEtiketToolStripMenuItem);
                }

                //zneviditelneni tlacitek
                //tsmiOdstranit.Visible = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_VolnyPohyb_Nasnimane_Odstranit;
                //tsmiUpravit.Visible = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_VolnyPohyb_Nasnimane_Upravit;
                //tsmi_KlavesovyVystup.Visible = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_VolnyPohyb_Nasnimane_KlavVystup;

                if (!Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_VolnyPohyb_Nasnimane_Odstranit)
                {
                    if (!Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_VolnyPohyb_Nasnimane_Upravit)
                        menuStrip1.Items.Remove(tsmiPolozka);

                    tsmiPolozka.DropDownItems.Remove(tsmiOdstranit);
                }
                if (!Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_VolnyPohyb_Nasnimane_Upravit)
                {
                    tsmiPolozka.DropDownItems.Remove(tsmiUpravit);
                }

                if (!Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_VolnyPohyb_Nasnimane_KlavVystup)
                {
                    if (!Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_VolnyPohyb_Nasnimane_ImpDavku)
                        menuStrip1.Items.Remove(tsmiAkce);

                    tsmiAkce.DropDownItems.Remove(tsmi_KlavesovyVystup);
                }
                if (!Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_VolnyPohyb_Nasnimane_ImpDavku)
                {
                    tsmiAkce.DropDownItems.Remove(tsmiImportovatDavku);
                }

                this.ShowIcon = false;
                this.WindowState = FormWindowState.Maximized;

                
                // načtení konfigurace datagridu z nastavení aplikace
                this.dgProdej.LoadConfiguration(this.GetType().ToString());

                //string hodnota = this.GetType().ToString();

                panelButtons.LoadConfiguration(this.GetType().ToString());
                panelButtons.Init();


                advancedDataGridViewSearchToolBar1.SetColumns(dgProdej.Columns);

                //// načtení konfigurace vytvořených filtrů
                this.filtry = Extensions.ListExt.ReadFromXML<Fask.Interfaces.Filtry.ProdejFiltr>(this.GetType().ToString() + ".filtr");
                tscbFiltry.ComboBox.DataSource = this.filtry;
                tscbFiltry.SelectedItem = null;
                tscbFiltry.ComboBox.DropDownWitdhAutosize();


                DateTime today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                dtp_porizeno_OD.Value = today.AddDays(-1);
                dtp_porizeno_DO.Value = today;


                dtp_porizeno_OD.Checked = false;
                dtp_porizeno_DO.Checked = false;

                cb_Porizeno_TimeVariant.Items.AddRange(Fask.Interfaces.Classes.TimeFilters.GetNumberNameRange());
                cb_Porizeno_TimeVariant.SelectedIndex = 0;

                //cb_Porizeno_TimeVariant.Items.AddRange(Fask.Interfaces.Classes.TimeFilters.GetNumberNameRange());
                //cb_Porizeno_TimeVariant.SelectedIndex = 0;


                panelButtons.Size = new Size(85, 700);

                // inicializace providera
                InitProvider();

                if (providerProdej == null)
                    throw new Exception("Provider 'Volny Pohyb' není inicializován");

        

                buttonVyhledat.Focus();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public string GetTimeVariantFromFilter_porizeno()
        {
            Fask.Interfaces.Classes.TimeVariantName type = (Fask.Interfaces.Classes.TimeVariantName)cb_Porizeno_TimeVariant.SelectedItem;
            return type.ToString_Filter();
        }

        private void FormVolnyPohybNasnimaneList_KeyDown(object sender, KeyEventArgs e)
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
            else if ((e.Control && e.KeyCode == Keys.T))
            {
                PrimyTiskZPL(); // Simuluje kliknutí na tlačítko
            }
            else if ((e.Control && e.KeyCode == Keys.R))
            {
                PrimyTiskRDLC(); // Simuluje kliknutí na tlačítko
            }
            else
                return;

            e.Handled = true;
        }

        private void FormVolnyPohybNasnimaneList_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.dgProdej.SaveConfiguration(this.GetType().ToString());
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

        private void FormVolnyPohybNasnimaneList_Shown(object sender, EventArgs e)
        {
            try
            {
                this.progressIndicator1.Size = new Size(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ostatni[0].ProgressIndicatorSize, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ostatni[0].ProgressIndicatorSize);
                this.progressIndicator1.Location = new Point(this.dgProdej.Location.X + (this.dgProdej.Width / 2) - (progressIndicator1.Size.Width / 2), this.dgProdej.Location.Y + (this.dgProdej.Height / 2) - (progressIndicator1.Size.Height / 2));
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

                if (providerProdej == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Prodej.IProdej2).IsAssignableFrom(t))
                            {
                                providerProdej = (Fask.Interfaces.Prodej.IProdej2)providerAssemlby.CreateInstance(t.FullName);
                                if (providerProdej != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                providerProdej.InitProvider();
                
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                if (!String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].Provider_Graphics))
                {
                    if (providerTiskKod == null) //inicializace se provede pouze pokud nebyla provedena ... 
                    {
                        Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].Provider_Graphics));
                        Type[] types = providerAssemlby.GetTypes();
                        foreach (Type t in types)
                        {
                            try
                            {
                                if (typeof(Fask.BarCodeGraphics.IBarCodeGraphics).IsAssignableFrom(t))
                                {
                                    providerTiskKod = (Fask.BarCodeGraphics.IBarCodeGraphics)providerAssemlby.CreateInstance(t.FullName);
                                    if (providerTiskKod != null)
                                        break;
                                }
                            }
                            catch { }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            #region Tisk
            try
            {
                if (String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                    return;

                if (providerTisk == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Tisky.ITisky2).IsAssignableFrom(t))
                            {
                                providerTisk = (Fask.Interfaces.Tisky.ITisky2)providerAssemlby.CreateInstance(t.FullName);
                                if (providerTisk != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                providerTisk.InitProvider();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion

        }

        private void buttonVyhledat_Click(object sender, EventArgs e)
        {
            #region prace s datagridem - nastaveni numeric hodnot na pocet desetinnych mist
            try
            {
                // Procházení sloupců v DataGridView
                foreach (DataGridViewColumn column in dgProdej.Columns)
                {
                    // Získání typu dat pro sloupec
                    Type dataType = null;

                    if (dgProdej.DataSource is DataTable dataTable)
                    {
                        // Pokud je datový zdroj DataTable
                        if (dataTable.Columns.Contains(column.DataPropertyName))
                        {
                            dataType = dataTable.Columns[column.DataPropertyName].DataType;
                        }
                    }
                    else if (dgProdej.DataSource is BindingSource bindingSource)
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
        private bool CreateFilter(ref Fask.Interfaces.Filtry.ProdejFiltr filtr)
        {
            if (filtr == null)
                filtr = new Fask.Interfaces.Filtry.ProdejFiltr();

            filtr.CountEntries = cbCountEntries.Text.Trim();
            filtr.ITEMNMBR = cbITEMNMBR.Text.Trim();

            filtr.DOC_ID = tB_Typ_pohybu.Text.Trim();
            filtr.DOC_ID2 = tB_Typ_pohybu2.Text.Trim();
            filtr.ITEMDESC = tB_Nazev_polozky.Text.Trim();
            filtr.ITEMCODE = tB_kod_polozky.Text.Trim();


            string input = tB_ID_uzivatele.Text.Trim();
            filtr.USERID = new List<int>();

            if (!string.IsNullOrWhiteSpace(input))
            {
                string[] parts = input.Split(',');
                foreach (var part in parts)
                {
                    int id;
                    if (int.TryParse(part.Trim(), out id))
                    {
                        filtr.USERID.Add(id);
                    }
                }

                // Pokud se nepodařilo žádné ID zparsovat, nastav na null
                if (filtr.USERID.Count == 0)
                    filtr.USERID = null;
            }
            else
            {
                filtr.USERID = null;
            }


            string terminalInput = tB_ID_terminalu.Text.Trim();
            filtr.ID_TERMINAL = new List<int>();

            if (!string.IsNullOrWhiteSpace(terminalInput))
            {
                string[] parts = terminalInput.Split(',');
                foreach (var part in parts)
                {
                    int id;
                    if (int.TryParse(part.Trim(), out id))
                        filtr.ID_TERMINAL.Add(id);
                }

                if (filtr.ID_TERMINAL.Count == 0)
                    filtr.ID_TERMINAL = null;
            }
            else
            {
                filtr.ID_TERMINAL = null;
            }



            #region DATEDONE

            if (dtp_porizeno_OD.Checked && dtp_porizeno_OD.Enabled)
            {
                filtr.DATEDONE_OD = dtp_porizeno_OD.Value;
            }
            else
                filtr.DATEDONE_OD = null;

            if (dtp_porizeno_DO.Checked && dtp_porizeno_DO.Enabled)
            {
                filtr.DATEDONE_DO = dtp_porizeno_DO.Value;
            }
            else
                filtr.DATEDONE_DO = null;

            #endregion

            #region Time variant dateeve

            var por = GetTimeVariantFromFilter_porizeno();

            if (por.Contains("unknow"))
            {
                filtr.Dateeve_TimeVariant = null;
            }
            else
            {
                filtr.Dateeve_TimeVariant = por;
            }

            #endregion



            return true;
        }

        //public string GetTimeVariantFromFilter_porizeno()
        //{
        //    Fask.Interfaces.Classes.TimeVariantName type = (Fask.Interfaces.Classes.TimeVariantName)cb_Porizeno_TimeVariant.SelectedItem;
        //    return type.ToString_Filter();
        //}

        int? Index = null;

        private void PerformOK()
        {
            try
            {
                //DataTable dtchanged = this.dsProdej.CZMST_DI.GetChanges();
                //if (dtchanged != null && dtchanged.Rows.Count > 0) //obsahuje zmeny ... 
                //{
                //    //MaR zakomentoval 11.3.2024
                //    //DialogResult dr = MessageBox.Show("Obsahuje změny, pokračovat?", "data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                //    //if (dr == System.Windows.Forms.DialogResult.No)
                //    //    return;
                //}

                if (bwLoadVolnyPohyb.IsBusy)
                {
                    bwLoadVolnyPohyb.CancelAsync();
                    while (bwLoadVolnyPohyb.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }

                ProgressIndicatorStart();

                Fask.Interfaces.Filtry.ProdejFiltr filtr = new Fask.Interfaces.Filtry.ProdejFiltr();
                if (!CreateFilter(ref filtr))
                    return;

                int FirstDisplayedScrollingRowIndex = this.dgProdej.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

                if(Prodej_SelectedRow != null)
                    Index = Prodej_SelectedRow.DEX_ROW_ID;


                bwLoadVolnyPohyb.RunWorkerAsync(filtr);

                if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dgProdej.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dgProdej.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index
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
                this.dgProdej.SelectAll();
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
                this.dgProdej.ClearSelection();
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
                Fask.Interfaces.Filtry.ProdejFiltr filtr = (Fask.Interfaces.Filtry.ProdejFiltr)e.Argument;
                Fask.Interfaces.DataSets.Prodej ds = new Fask.Interfaces.DataSets.Prodej();

                if (bwLoadVolnyPohyb.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                // nacteni dat v oddelenem vlakne
                //ds = ((Fask.Interfaces.Vydej.IVydej)providerVydej).GetFiltrovaneHlavicky(filtr);

                if ((providerProdej != null) && (providerProdej is Fask.Interfaces.Prodej.IProdej2_Prodej_GetFiltrovaneDavky ))
                    ds = ((Fask.Interfaces.Prodej.IProdej2_Prodej_GetFiltrovaneDavky)providerProdej).Prodej_GetFiltrovaneDavky(filtr);
                else
                    throw new NotImplementedException("Provider neimplementuje IProdej2_GetFiltrovaneDavky.");



                if (bwLoadVolnyPohyb.CancellationPending)
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
                    dsProdej = new Fask.Interfaces.DataSets.Prodej();
                    bsProdej.DataSource = dsProdej;
                }
                else if (e.Cancelled)
                {
                    // handle cancellation
                    // TODO: maji se vymazat data z datasetu?? asi ano ...
                    //if (dsSkladPohyb == null)
                    dsProdej = new Fask.Interfaces.DataSets.Prodej();
                    bsProdej.DataSource = dsProdej;
                }
                else
                {
                    // uspesne dokonceno ...
                    // use it on the UI thread
                    dsProdej = (Fask.Interfaces.DataSets.Prodej)e.Result;
                    if (dsProdej == null)
                        dsProdej = new Fask.Interfaces.DataSets.Prodej();

                    bsProdej.DataSource = dsProdej;



                    if (Index != null )
                    {
                        var polozky = dsProdej.CZMST_DI.Where(x => x.DEX_ROW_ID == Index);
                        if (polozky.Count() > 0)
                        {
                            var polozka = polozky.First();

                            int index = ((System.Data.DataView)bsProdej.List).Table.Rows.IndexOf(polozka);
                            bsProdej.Position = index;
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
                this.progressIndicator1.Location = new Point(this.dgProdej.Location.X + (this.dgProdej.Width / 2) - (progressIndicator1.Size.Width / 2), this.dgProdej.Location.Y + (this.dgProdej.Height / 2) - (progressIndicator1.Size.Height / 2));
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

                Fask.Interfaces.Filtry.ProdejFiltr filtr = rowFiltr;

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

                Fask.Interfaces.Filtry.ProdejFiltr filtr = new Fask.Interfaces.Filtry.ProdejFiltr();
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
        private void PerformNastavitFiltr(Fask.Interfaces.Filtry.ProdejFiltr filtr)
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
                cbITEMNMBR.Text = filtr.ITEMNMBR;



                // DOC_ID, DOC_ID2, ITEMDESC z filtru do TextBoxů
                tB_Typ_pohybu.Text = filtr.DOC_ID ?? string.Empty;
                tB_Typ_pohybu2.Text = filtr.DOC_ID2 ?? string.Empty;
                // Předpokládám, že ITEMDESC je stejný jako DOC_ID – pokud ne, uprav
                tB_Nazev_polozky.Text = filtr.ITEMDESC ?? string.Empty;

                tB_kod_polozky.Text = filtr.ITEMCODE ?? string.Empty;

                // USERID – převedení seznamu int na čárkami oddělený string
                if (filtr.USERID != null && filtr.USERID.Any())
                {
                    tB_ID_uzivatele.Text = string.Join(", ", filtr.USERID);
                }
                else
                {
                    tB_ID_uzivatele.Text = string.Empty;
                }

                // ID_TERMINAL – povinný int, ale pokud je null, nastavíme prázdný string
                if (filtr.ID_TERMINAL != null && filtr.ID_TERMINAL.Any())
                {
                    tB_ID_terminalu.Text = string.Join(", ", filtr.ID_TERMINAL);
                }
                else
                {
                    tB_ID_terminalu.Text = string.Empty;
                }
                // DOC_ID, DOC_ID2, ITEMDESC z filtru do TextBoxů
                tB_Typ_pohybu.Text = filtr.DOC_ID ?? string.Empty;
                tB_Typ_pohybu2.Text = filtr.DOC_ID2 ?? string.Empty;
                // Předpokládám, že ITEMDESC je stejný jako DOC_ID – pokud ne, uprav
                // tB_Typ_dokladu.Text = filtr.ITEMDESC ?? string.Empty;

                tB_kod_polozky.Text = filtr.ITEMCODE ?? string.Empty;

                // USERID – převedení seznamu int na čárkami oddělený string
                if (filtr.USERID != null && filtr.USERID.Any())
                {
                    tB_ID_uzivatele.Text = string.Join(", ", filtr.USERID);
                }
                else
                {
                    tB_ID_uzivatele.Text = string.Empty;
                }

                // ID_TERMINAL – povinný int, ale pokud je null, nastavíme prázdný string
                if (filtr.ID_TERMINAL != null && filtr.ID_TERMINAL.Any())
                {
                    tB_ID_terminalu.Text = string.Join(", ", filtr.ID_TERMINAL);
                }
                else
                {
                    tB_ID_terminalu.Text = string.Empty;
                }






                if (filtr.DATEDONE_DO != null)
                {
                    dtp_porizeno_DO.Checked = true;
                    dtp_porizeno_DO.Value = (DateTime)filtr.DATEDONE_DO;
                }

                if (filtr.DATEDONE_OD != null)
                {
                    dtp_porizeno_OD.Checked = true;
                    dtp_porizeno_OD.Value = (DateTime)filtr.DATEDONE_OD;
                }

                if (!string.IsNullOrEmpty(filtr.Dateeve_TimeVariant))
                {

                    var arr = filtr.Dateeve_TimeVariant.Split(';');
                    TimeFilters.TimeVariants TimeVar = (TimeFilters.TimeVariants)Enum.Parse(typeof(TimeFilters.TimeVariants), arr[0], true);

                    TimeVariantName time = new TimeVariantName(TimeVar, arr[1]);

                    cb_Porizeno_TimeVariant.SelectedItem = time;
                }



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
                    if (Prodej_SelectedRow != null)
                        sortedID = Prodej_SelectedRow.CountEntries.ToString();
                }
            }
            catch { }
        }

        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            try
            {
                int pos = this.bsProdej.Find(dsProdej.CZMST_DI.CountEntriesColumn.ColumnName, sortedID);
                this.bsProdej.Position = pos;
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


                tB_Nazev_polozky.Text = string.Empty;
                tB_Typ_pohybu.Text = string.Empty;
                tB_Typ_pohybu2.Text = string.Empty;
                tB_kod_polozky.Text = string.Empty;
                tB_ID_uzivatele.Text = string.Empty;
                tB_ID_terminalu.Text = string.Empty;


                cbCountEntries.Text = string.Empty;
                cbITEMNMBR.Text = string.Empty;

                dtp_porizeno_OD.Checked = false;
                dtp_porizeno_DO.Checked = false;
                cb_Porizeno_TimeVariant.SelectedIndex = 0;


            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Generovat data 



        private void ImportDavka(int? CountEntries, string SKL_ID)
        {
            try
            {
                // Zastaví případný předchozí běh BackgroundWorkeru
                if (bwExport.IsBusy)
                {
                    bwExport.CancelAsync();
                    while (bwExport.IsBusy)
                    {
                        Application.DoEvents(); // není ideální, ale často používané v legacy WinForms
                    }
                }

                ProgressIndicatorStart(); // Zobrazí indikátor postupu

                // Vytvoření objektu pro přenos dat
                Fask.Interfaces.Classes.DataDavky data = new Fask.Interfaces.Classes.DataDavky();

                // Naplnění dat o skladu
                data.sklad = new Fask.Interfaces.Classes.Sklad();
                data.sklad.ID = SKL_ID.Trim();

                // Pokud je k dispozici číslo dávky (počet položek), přidej objednávku
                if (CountEntries.HasValue)
                {
                    data.objednavka = new Fask.Interfaces.Classes.Objednavka();
                    data.objednavka.CisloDavky = CountEntries.Value.ToString();
                }

                // Spustí asynchronní operaci exportu
                bwExport.RunWorkerAsync(data);
            }
            catch (Exception ex)
            {
                ProgressIndicatorStop(); // Zastaví indikátor při chybě
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void bwExport_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                //dsVydej = (Fask.Interfaces.DataSets.Vydej)e.Result;
                Fask.Interfaces.Classes.StatusInfo si = new Fask.Interfaces.Classes.StatusInfo();
                try
                {

                    si = (Fask.Interfaces.Classes.StatusInfo)e.Result;

                }
                catch
                {}

                // check error, check cancel, then use result
                if (e.Error != null)
                {
                    // handle the error
                    MessageBox.Show(e.Error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                    PerformOK();
                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                if (ex.InnerException != null)
                {
                    Fask.Logging.ExceptionHandler2.Handle(ex.InnerException);
                    MessageBox.Show(ex.Message + ex.InnerException.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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

                Fask.Interfaces.Classes.StatusInfo si;

                if ((providerProdej != null) && (providerProdej is Fask.Interfaces.Prodej.IProdej2_ImportDavka))
                    si = ((Fask.Interfaces.Prodej.IProdej2_ImportDavka)providerProdej).ImportDavka(data.objednavka, data.sklad, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Ostatni[0].Prodej_ImportDokladuGrupuj);
                else
                    throw new NotImplementedException("Provider neimplementuje IProdej2_ImportDavka.");



                if (bwExport.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                e.Result = si;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
                
                //MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                this.dgProdej.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                this.dgProdej.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
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

                this.dgProdej.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                dgProdej.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
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

                dgProdej.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                dgProdej.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
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
                bool endcol = dgProdej.CurrentCell.ColumnIndex + 1 >= dgProdej.ColumnCount;
                bool endrow = dgProdej.CurrentCell.RowIndex + 1 >= dgProdej.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dgProdej.CurrentCell.ColumnIndex;
                    startRow = dgProdej.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dgProdej.CurrentCell.ColumnIndex + 1;
                    startRow = dgProdej.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dgProdej.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dgProdej.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dgProdej.CurrentCell = c;
        }

        private void tsmiImportovatDavku_Click(object sender, EventArgs e)
        {
           // throw new NotImplementedException();

            if (Prodej_SelectedRow == null)
            {
                MessageBox.Show("Není vybrán záznam pro úpravu", this.Text, MessageBoxButtons.OK);
                return;
            }


            if (dgProdej.SelectedRows.Count > 1)
            {
                MessageBox.Show("Je vybráno více záznamů než jeden.", this.Text, MessageBoxButtons.OK);
                return;
            }


            ImportDavka(Prodej_SelectedRow.CountEntries, Prodej_SelectedRow.SKL_ID);
        }

        private void tsmiOdstranit_Click(object sender, EventArgs e)
        {

            if (!opravneni)
            {
                MessageBox.Show("Nemáte dostatečné oprávnění pro tuto akci!", "Oprávnění", MessageBoxButtons.OK);
                return;
            }


            PerformDeleteRecord();
        }

        private void PerformDeleteRecord()
        {
            try
            {

                if (Prodej_SelectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro smazani", this.Text, MessageBoxButtons.OK);
                    return;
                }

                #region doimplementovat!! MaR 22.2. 2024
                if (dgProdej.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Je vybráno více záznamů než jeden.", this.Text, MessageBoxButtons.OK);
                    return;
                }


                //throw new NotImplementedException("Neimplementováno.");


                if ((providerProdej != null) && (providerProdej is Fask.Interfaces.Prodej.IProdej2_DeleteDI))
                    ((Fask.Interfaces.Prodej.IProdej2_DeleteDI)providerProdej).DeleteDI(Prodej_SelectedRow);
                else
                    throw new NotImplementedException("Provider neimplementuje IProdej2_DeleteDI.");
                #endregion

                PerformOK();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsmiUpravit_Click(object sender, EventArgs e)
        {
            if (!opravneni)
            {
                MessageBox.Show("Nemáte dostatečné oprávnění pro tuto akci!", "Oprávnění", MessageBoxButtons.OK);
                return;
            }
            PerformEditRecord();
        }

        private void PerformEditRecord()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (Prodej_SelectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro úpravu", this.Text, MessageBoxButtons.OK);
                    return;
                }


                if (dgProdej.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Je vybráno více záznamů než jeden.", this.Text, MessageBoxButtons.OK);
                    return;
                }

                #region doimplementovat!! MaR 22.2. 2024
                using (FormDavkyProdejDIEdit frmuziv = new FormDavkyProdejDIEdit())
                {
                    frmuziv.rowDavkaEdit = Prodej_SelectedRow;
                    frmuziv.Text = "Úprava Davky";
                    if (frmuziv.ShowDialog(this) != DialogResult.OK)
                        return;

                    //this.SelectedRow.CountEntries = frmuziv.returnrow.CountEntries;
                    //this.SelectedRow.SOPNUMBE = frmuziv.returnrow.SOPNUMBE;
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
                    ////this.SelectedRow.DEX_ROW_ID = frmuziv.returnrow.DEX_ROW_ID;

                    //this.dsVydej.CZMST_SE.AcceptChanges();
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
        #region Tisk

        private void tsmiTiskRadkuEtiketa_Click(object sender, EventArgs e)
        {
            try
            {

                if (Prodej_SelectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if (SelectedRows.Count == 0)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }


                if (SelectedRows.Count > 1)
                {

                    MessageBox.Show("Je vybráno vícero záznamů pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }


                Tisk(true);

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Tisk(bool MnozstvuAutoJedna)
        {


     


            try
            {
                PrintDialog printDialog1 = new PrintDialog();
                printDialog1.UseEXDialog = true;

                if (printDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.Cancel)
                    return;


                string pocetStr = string.Empty;
                int pocetInt = 1;

                if (!MnozstvuAutoJedna)
                {
                    while (true)
                    {
                        DialogResult drPocet = Konzola.Forms.InputBox.Show("Etiketa tisk", "Počet etiket k vytištění", pocetInt.ToString(), Forms.InputBox.TypeOfCode.NumericInt, true, 1, 99, false, out pocetStr);
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

                        break;
                    }
                }

                string strPath = System.IO.Path.Combine(MySystem.MyPath.PrintDirectory, "Etiketa_Prodej.zpl");


                System.IO.StreamReader sr = new System.IO.StreamReader(strPath);
                string strData = sr.ReadToEnd();
                sr.Close();

                StringBuilder sbData = new StringBuilder();
                sbData.Append(strData);





                string EXPIRACE_RRMMDD = string.Empty;
                string GS1_KOD = string.Empty;
                string VNDITNUM = Prodej_SelectedRow.VNDITNUM;
                string SERLTNUM = Prodej_SelectedRow.SERLTNUM;

                DateTime? EXPIRACE;
                if (!Prodej_SelectedRow.IsEXPIRACENull())
                {
                    EXPIRACE = Prodej_SelectedRow.EXPIRACE;
                }
                else
                {
                    EXPIRACE = null;
                }
                

                if (!string.IsNullOrEmpty(VNDITNUM))
                {
                    if (VNDITNUM.Length < 14)
                    {
                        VNDITNUM = VNDITNUM.PadLeft(14, '0');
                    }

                    if (VNDITNUM.Length != 14)
                    {
                        throw new Exception("GTIN nemá 14 znaků");
                    }

                    GS1_KOD += "01" + VNDITNUM;

                    if (EXPIRACE.HasValue)
                    {
                        string datumexpirace = EXPIRACE.Value.ToString("yyMMdd");
                        GS1_KOD += "17" + datumexpirace;
                    }

                    if (!string.IsNullOrEmpty(SERLTNUM))
                    {
                        GS1_KOD += "10" + SERLTNUM;
                    }

                }

                if (EXPIRACE.HasValue)
                {
                    EXPIRACE_RRMMDD += EXPIRACE.Value.ToString("dd.MM.yyyy");
                    EXPIRACE_RRMMDD += " (";
                    EXPIRACE_RRMMDD += EXPIRACE.Value.ToString("yyMMdd");
                    EXPIRACE_RRMMDD += ")";
                }

                sbData.Replace("$VNDITNUM$", Prodej_SelectedRow.IsVNDITNUMNull() ? string.Empty : Prodej_SelectedRow.VNDITNUM);
                sbData.Replace("$ITEMCODE$", Prodej_SelectedRow.IsITEMCODENull() ? string.Empty : Prodej_SelectedRow.ITEMCODE);
                sbData.Replace("$SERLTNUM$", Prodej_SelectedRow.SERLTNUM);
                sbData.Replace("$EXPIRACE_RRMMDD$", EXPIRACE_RRMMDD);
                sbData.Replace("$GS1_KOD$", GS1_KOD);
                

                sbData = ReplaceImage(sbData);
                ;
                if (sbData == null)
                    throw new Exception("Tisk etikety IBarCodeGraphics chyba.");

                sbData.Replace("$PocetVytisku$", pocetInt.ToString());

                string debugprintlabel = string.Empty;

                if (Fask.Logging.ExceptionHandler2.EnablePrintLogging)
                {

                    byte[] destinbytes = Encoding.GetEncoding(1250).GetBytes(sbData.ToString()); // Kodovani je ěpatně, musi to byt v konfiguraci
                    debugprintlabel = "PL_Label_" + Guid.NewGuid().ToString() + ".zpl";

                    FileStream sw = new FileStream(System.IO.Path.Combine(MySystem.MyPath.PrintLogDirectory, debugprintlabel), FileMode.Create);
                    sw.Write(destinbytes, 0, destinbytes.Length);
                    sw.Flush();
                    sw.Close();
                }


                //doplnit logy tisku
                try
                {
                    if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                    {



                        // Iterujeme přes jednotlivé řádky v StringBuilderu sbData
                        foreach (var line in sbData.ToString().Split(/*Environment.NewLine*/))
                        {
                            Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo," 88: " + line);
                        }




                        //potrebuji iterovat podle StringBuilder sbData = new StringBuilder();
                        foreach (DataColumn item in Prodej_SelectedRow.Table.Columns)
                        {
                            Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start transakce/volny pohyb/nasnimane TISK ???-------------------------------------");

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

                            Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END transakce/volny pohyb/nasnimane TISK ???-------------------------------------");
                        }

                    }

                }
                catch (Exception ex)
                {

                    Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, $"ERROR: " + ex.Message);
                }




                if (!Konzola._Support_.RawPrinterHelper.SendStringToPrinter(printDialog1.PrinterSettings.PrinterName, sbData.ToString()))
                {
                    throw new Exception("Tisk etikety Etiketa_LokMech_Stav  se nezdařil");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        protected StringBuilder ReplaceImage(StringBuilder sb)
        {
            try
            {

                if ((providerTiskKod != null) && (providerTiskKod is Fask.BarCodeGraphics.IBarCodeGraphics))
                    return ((Fask.BarCodeGraphics.IBarCodeGraphics)providerTiskKod).AddGraphics(sb);
                else
                    return sb;
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return null;
            }
        }


        #endregion

        private void tsmi_KlavesovyVystup_Click(object sender, EventArgs e)
        {
            try
            {
              
               
                    //1- Skontroloje zda je vybán aspon jeden záznam k smazaní
                    if (this.dgProdej.SelectedRows.Count == 0)
                    {
                        MessageBox.Show("Nejsou vybrány záznamy na klavesový výstup.", this.Text, MessageBoxButtons.OK);
                        return;
                    }

                    List<List<string>> All = new List<List<string>>();

                    Konfigurace.Globals_Konfig_Konzola.LoadConfiguration();
                    string nazevProcesu = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce_VolnyPohyb_Nasnimane[0].Klavesovy_Vystup_NazevProcesu;
                    string template = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce_VolnyPohyb_Nasnimane[0].Klavesovy_Vystup_TEMPLATE;
                    string separ = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce_VolnyPohyb_Nasnimane[0].Klavesovy_Vystup_TEMPLATE_Separator;
                    int CasCekani = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce_VolnyPohyb_Nasnimane[0].Klavesovy_Vystup_CasCekani;


                    // 0. Logina nepodporuje stlačení kombinac klaves alt  ctrl a shift
                    // 0.1. muselo by se pořadne zamyslet nad logikou, ted funguje nejprimitivnejší spusob, radoby tukani jednym prstem
                    // 1. Vztahnout template z konfigurace
                    // 1.1. Napr:   $SERLTNUM$;{TAB};$REZ_1$;{TAB}
                    // 2. Rozhodit template na jednotlive prvky

                    foreach (DataGridViewRow item in this.dgProdej.SelectedRows)
                    {
                        var row = GetSelectedRow(item);

                        if (string.IsNullOrEmpty(template))
                            throw new Exception("Template je prázdný!");

                        var x = template.Split(separ.ToCharArray());

                        string[] arr = new string[x.Length];

                        for (int i = 0; i < x.Length; i++)
                        {
                            var pol = x[i].Trim();

                            if (pol.StartsWith("$") && pol.EndsWith("$"))
                            {
                                var NazevStloupce = pol.Replace("$", "");
                                arr[i] = row[NazevStloupce.Trim()].ToString();
                            }
                            else if (pol.StartsWith("{") && pol.EndsWith("}"))
                            {
                                arr[i] = pol;
                            }
                            else
                            {
                                arr[i] = pol;
                            }
                        }

                        All.Add(new List<string>(arr));
                    }

                    Process[] processes = Process.GetProcessesByName(nazevProcesu);

                    if (processes.Length == 0)
                    {
                        MessageBox.Show(string.Format("Nenalezen spuštěn proces: {0}", nazevProcesu), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                        return;
                    }

                    foreach (Process proc in processes)
                    {
                        SetForegroundWindow(proc.MainWindowHandle);

                        foreach (List<string> item in All)
                        {
                            foreach (string klavesa in item)
                            {
                                SendKeys.SendWait(klavesa);
                                System.Threading.Thread.Sleep(CasCekani);
                            }
                        }
                    } 
             

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Pomocne metody

        /// <summary>
        /// Metoda ktera vratí typovy dataset z vybraného čidku GataGridView
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Fask.Interfaces.DataSets.Prodej.CZMST_DIRow GetSelectedRow(DataGridViewRow item)
        {
            try
            {
                return ((item.DataBoundItem as DataRowView).Row) as Fask.Interfaces.DataSets.Prodej.CZMST_DIRow;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return null;
            }
        }

        #endregion



        #region MaR 14.11.2024 Tisk ZPL a RDLC
        private void tiskEtiketToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Perform_Tisk();

            Perform_Tisk_Selected(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Tisk_Klic_ZPL
                , Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Tisk_ZPL_Vychozi_Tiskarna
                , Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Tisk_ZPL_Pocet_Vytisku);

        }

        private void tiskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PerformPrint(false
                , Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Tisk_Klic_RDLC
              , Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Tisk_RDLC_Vychozi_Tiskarna
              , Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Tisk_RDLC_Pocet_Vytisku);

            //Perform_Tisk_Selected(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Tisk_Klic_RDLC
            //   , Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Tisk_RDLC_Vychozi_Tiskarna
            //   , Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Tisk_RDLC_Pocet_Vytisku);
        }

        private void PrimyTiskZPL()
        {
            //MaR 13.11.2024 zde bude primy tisk ZPL sablon s ord 0 a loginId
            //tisk
            Perform_Tisk_Selected_Primy_Tisk(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Tisk_Klic_ZPL
                , Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Tisk_ZPL_Vychozi_Tiskarna
                , Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Tisk_ZPL_Pocet_Vytisku);


        }

        private void PrimyTiskRDLC()
        {
            //MaR 13.11.2024 zde bude primy tisk ZPL sablon s ord 0 a loginId
            //tisk
            PerformPrint(true
                , Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Tisk_Klic_RDLC
              , Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Tisk_RDLC_Vychozi_Tiskarna
              , Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Tisk_RDLC_Pocet_Vytisku);

            //Perform_Tisk_Selected_Primy_Tisk(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Tisk_Klic_RDLC
            //    , Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Tisk_RDLC_Vychozi_Tiskarna
            //    , Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Tisk_RDLC_Pocet_Vytisku);

        }

        private void Perform_Tisk_Selected_Primy_Tisk(string klicTyp, string NazevVychoziTiskarny, string pocetVytisku)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (Prodej_SelectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                #region MaR 6.11.2024 vypis zpl do souboru selected rows
                int index_zaznamu = 0;

                foreach (var item in Prodej_SelectedRows)
                {

                    if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start sklady,volny pohyb nasnimane TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");

                        foreach (DataColumn column in item.Table.Columns)
                        {
                            string columnName = string.Empty;
                            object columnValue = string.Empty;
                            //Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, $"Klíč: {kv[1]}, Hodnota: {kv[2]}");
                            try
                            {
                                columnName = column.ColumnName;
                                columnValue = item[columnName];
                            }
                            catch (Exception ex)
                            {

                                columnName = column.ColumnName;
                                columnValue = string.Empty;
                            }
                            //Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo,$"Název sloupce: {columnName}, Hodnota: {columnValue}");
                            Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, $"{columnName}: {columnValue}");
                        }

                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END sklady,volny pohyb nasnimane TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");


                    }

                    index_zaznamu++;
                }


                #endregion

                if (providerTisk is Fask.Interfaces.Tisky.ITisky2_EtiketaTisk)
                {

                    var PrinerName = prepareTiskParams();

                    if (PrinerName == null)
                        return;

                    int? MN_ToTisk = TiskMnozstvi(true);



                    foreach (var item in Prodej_SelectedRows)
                    {
                        Dictionary<string, string> dict = PrepareDataToTisk(item);

                        if (MN_ToTisk.HasValue)
                        {
                            ((Fask.Interfaces.Tisky.ITisky2_EtiketaTisk)providerTisk).EtiketaTisk(
                               (int)Konzola.Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].TerminalID,
                               Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba[0].NazevKSabloneTisku,
                               PrinerName,
                               dict,
                               MN_ToTisk.Value);
                        }
                        else
                            MessageBox.Show(this, "Chyba pri zadani množství", "Error", MessageBoxButtons.OK);
                    }


                    MessageBox.Show(this, "Tisk proběhl uspešně.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    //pocet vytisku do tiskarny
                    int MN_ToTisk = 1;

                    if (!string.IsNullOrEmpty(pocetVytisku))
                    {
                        MN_ToTisk = int.Parse(pocetVytisku);
                    }

                    //primy tisk cesta k sablone
                    FormOdvod_TiskoveSablony form = new FormOdvod_TiskoveSablony(this.Text, FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Admin(), klicTyp, false, FASK.Logins.Uzivatel.Instance.UserID, 0);
                    string path_sablona = form.Prime_Tisky_Path();

                    if (string.IsNullOrEmpty(path_sablona))
                    {
                        MessageBox.Show(this, "Chybí šablona pro tisk!", "Error", MessageBoxButtons.OK);
                        return;
                    }

                    string plr_Path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path_sablona);

                    //odeslani dat do tiskarny
                    Tisk_selected_rows_Primy_Tisk(plr_Path, true, MN_ToTisk, Prodej_SelectedRows, NazevVychoziTiskarny);

                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Tisk_selected_rows_Primy_Tisk(string plr_Path, bool MnozstvuAutoJedna, int MN_ToTisk, List<Fask.Interfaces.DataSets.Prodej.CZMST_DIRow> selected_Rows, string nazevVychTiskarny)
        {
            try
            {

                #region selected rows

                foreach (var item in selected_Rows)
                {
                    string data = PrepareDataToTisk(plr_Path, item, MN_ToTisk);


                    if (string.IsNullOrEmpty(data))
                    {
                        MessageBox.Show("Špatná šablona pro tisk!", "Cesta k šabloně", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;

                    }

                    for (int i = 0; i < MN_ToTisk; i++)
                    {
                        if (!Konzola._Support_.RawPrinterHelper.SendStringToPrinter(nazevVychTiskarny, data))
                        {
                            throw new Exception(string.Format("Tisk etikety '{0}' se nezdařil", Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba[0].NazevKSabloneTisku));
                        }
                    }


                }

                #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void PerformPrint(bool primyTisk, string klicTyp, string nazevVychTiskarny, string pocetVytisku)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (Prodej_SelectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                //if (Production_rowProducts.Count > 1)
                //{
                //    //MessageBox.Show("Je vybráno více záznamů než jeden.", this.Text, MessageBoxButtons.OK);
                //    //return;
                //}

                Fask.Interfaces.DataSets.Prodej.CZMST_DIDataTable dt = new Fask.Interfaces.DataSets.Prodej.CZMST_DIDataTable();

                foreach (Fask.Interfaces.DataSets.Prodej.CZMST_DIRow row in Prodej_SelectedRows)
                {
                    try
                    {
                        Fask.Interfaces.DataSets.Prodej.CZMST_DIRow newRow = dt.NewCZMST_DIRow();
                        // Ošetřit delší vstupní pole než počet sloupců tabulky
                        int copyLength = Math.Min(row.ItemArray.Length, dt.Columns.Count);
                        newRow.ItemArray = row.ItemArray.Take(copyLength).ToArray();
                        dt.AddCZMST_DIRow(newRow);
                    }
                    catch (Exception ex)
                    {
                        // Ošetření vyjimky
                        // Můžete zde provést logování chyby nebo jiné požadované akce
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                    }
                }

                #region new vybrani tiskove  sablony
                string path_sablona = string.Empty;
                //pocet vytisku do tiskarny
                int MN_ToTisk = 1;

                if (!string.IsNullOrEmpty(pocetVytisku))
                {
                    MN_ToTisk = int.Parse(pocetVytisku);
                }

                if (!primyTisk)
                {


                    FormOdvod_TiskoveSablony form = new FormOdvod_TiskoveSablony(this.Text, FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Admin(), klicTyp, false, FASK.Logins.Uzivatel.Instance.UserID, 0);



                    DialogResult result = form.ShowDialog();
                    // Zobrazení formuláře
                    if (result == DialogResult.OK)
                    {
                        //dataGridView = (DataGridViewRow)form.Tag;
                        path_sablona = form.ResultString;
                        // uživatel stiskl OK
                    }
                    else if (result == DialogResult.Cancel)
                    {
                        // uživatel stiskl Cancel
                        //MessageBox.Show("Není vybraná tisková šablona");
                        return;
                    }
                }
                else
                {
                    FormOdvod_TiskoveSablony form = new FormOdvod_TiskoveSablony(this.Text, FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Admin(), klicTyp, false, FASK.Logins.Uzivatel.Instance.UserID, 0);
                    path_sablona = form.Prime_Tisky_Path();

                    if (string.IsNullOrEmpty(path_sablona))
                    {
                        MessageBox.Show(this, "Chybí šablona pro tisk!", "Error", MessageBoxButtons.OK);
                        return;
                    }
                }
                #endregion

                PrintReport_RDLC(dt, primyTisk, path_sablona, nazevVychTiskarny, MN_ToTisk);

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        }

        private void PrintReport_RDLC(Fask.Interfaces.DataSets.Prodej.CZMST_DIDataTable dt, bool primyTisk, string path_sablona, string nazevVychTiskarny, int pocetVytisku)
        {
            try
            {

                #region logovani tisku
                if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                {
                    foreach (var item in dt)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start sklady,volny pohyb nasnimane TISK rdlc-------------------------------------");

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

                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END sklady,volny pohyb nasnimane TISK rdlc-------------------------------------");
                    }
                }

                #endregion

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

                    if (!primyTisk)
                    {
                        plr.ShowPreview = true;
                    }
                    else
                    {
                        plr.PrinterName = nazevVychTiskarny;
                    }

                    plr.Path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path_sablona);

                    PrintReportLibrary.MyPath.PrintTemplateDirectory = Path.Combine(MySystem.MyPath.PrintDirectory);

                    try
                    {
                        for (int i = 0; i < pocetVytisku; i++)
                        {
                            plr.Print(this);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Chyba při pokusu o tisk.");
                        // Ošetření výjimky při tisku
                        Fask.Logging.ExceptionHandler2.Handle(ex);

                        // Volání metody PrintReport znovu (rekurze)
                        //PrintReport(dt, primyTisk, path_sablona, nazevVychTiskarny, pocetVytisku);
                    }

                }
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }

        private void Perform_Tisk_Selected(string klicTyp, string NazevVychoziTiskarny, string pocetVytisku)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (Prodej_SelectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                #region MaR 6.11.2024 vypis zpl do souboru selected rows
                int index_zaznamu = 0;

                foreach (var item in Prodej_SelectedRows)
                {

                    if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start sklady,volny pohyb nasnimane TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");

                        foreach (DataColumn column in item.Table.Columns)
                        {
                            string columnName = string.Empty;
                            object columnValue = string.Empty;
                            //Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, $"Klíč: {kv[1]}, Hodnota: {kv[2]}");
                            try
                            {
                                columnName = column.ColumnName;
                                columnValue = item[columnName];
                            }
                            catch (Exception ex)
                            {

                                columnName = column.ColumnName;
                                columnValue = string.Empty;
                            }
                            //Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo,$"Název sloupce: {columnName}, Hodnota: {columnValue}");
                            Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, $"{columnName}: {columnValue}");
                        }

                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END sklady,volny pohyb nasnimane TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");


                    }

                    index_zaznamu++;
                }


                #endregion


                if (providerTisk is Fask.Interfaces.Tisky.ITisky2_EtiketaTisk)
                {

                    var PrinerName = prepareTiskParams();

                    if (PrinerName == null)
                        return;

                    int? MN_ToTisk = TiskMnozstvi(true);



                    foreach (var item in Prodej_SelectedRows)
                    {
                        Dictionary<string, string> dict = PrepareDataToTisk(item);

                        if (MN_ToTisk.HasValue)
                        {
                            ((Fask.Interfaces.Tisky.ITisky2_EtiketaTisk)providerTisk).EtiketaTisk(
                               (int)Konzola.Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].TerminalID,
                               Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba[0].NazevKSabloneTisku,
                               PrinerName,
                               dict,
                               MN_ToTisk.Value);
                        }
                        else
                            MessageBox.Show(this, "Chyba pri zadani množství", "Error", MessageBoxButtons.OK);
                    }


                    MessageBox.Show(this, "Tisk proběhl uspešně.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {

                    //pocet vytisku do tiskarny
                    int MN_ToTisk = 1;

                    if (!string.IsNullOrEmpty(pocetVytisku))
                    {
                        MN_ToTisk = int.Parse(pocetVytisku);
                    }

                    FormOdvod_TiskoveSablony form = new FormOdvod_TiskoveSablony(this.Text, FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Admin(), klicTyp, false, FASK.Logins.Uzivatel.Instance.UserID, 0);

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
                        // MessageBox.Show("Není vybraná tisková šablona");
                        return;
                    }

                    string plr_Path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
                    Tisk_selected_rows(plr_Path, true, MN_ToTisk, Prodej_SelectedRows);

                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Tisk_selected_rows(string plr_Path, bool MnozstvuAutoJedna, int MN_ToTisk, List<Fask.Interfaces.DataSets.Prodej.CZMST_DIRow> selected_Rows)
        {
            try
            {
                PrintDialog printDialog1 = new PrintDialog();
                printDialog1.UseEXDialog = true;

                if (printDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.Cancel)
                    return;



                #region selected rows

                foreach (var item in selected_Rows)
                {
                    string data = PrepareDataToTisk(plr_Path, item, MN_ToTisk);


                    if (string.IsNullOrEmpty(data))
                    {
                        MessageBox.Show("Špatná šablona pro tisk!", "Cesta k šabloně", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;

                    }

                    for (int i = 0; i < MN_ToTisk; i++)
                    {
                        if (!Konzola._Support_.RawPrinterHelper.SendStringToPrinter(printDialog1.PrinterSettings.PrinterName, data))
                        {
                            throw new Exception(string.Format("Tisk etikety '{0}' se nezdařil", Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba[0].NazevKSabloneTisku));
                        }
                    }


                }

                #endregion


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private TiskParams prepareTiskParams()
        {

            if (Konfigurace_Tisky.Globals_Konfig_Tisk.Konfigurace.Params.Count == 1)
            {
                TiskParams tiskParams = new TiskParams();
                tiskParams.CONFIG_NAME = Konfigurace_Tisky.Globals_Konfig_Tisk.Konfigurace.Params[0].PrinterName; // to je vse ???
                return tiskParams;
            }
            else
            {
                MessageBox.Show(this, "Pro možnost volby z vicero tiskaren je potřeba doimplementovat funkčnost!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
        }

        private int? TiskMnozstvi(bool MnozstvuAutoJedna)
        {
            try
            {
                string pocetStr = string.Empty;
                int pocetInt = 1;

                if (!MnozstvuAutoJedna)
                {
                    while (true)
                    {
                        DialogResult drPocet = Konzola.Forms.InputBox.Show("Etiketa tisk", "Počet etiket k vytištění", pocetInt.ToString(), Forms.InputBox.TypeOfCode.NumericInt, true, 1, 99, false, out pocetStr);
                        if (drPocet == System.Windows.Forms.DialogResult.Cancel)
                            return null;

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
                }

                return pocetInt;

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return null;
            }
        }

        private string PrepareDataToTisk(
            string strPath,
            Fask.Interfaces.DataSets.Prodej.CZMST_DIRow row,
            int? mnozstviDoTisku
            )
        {
            try
            {
                string strData = string.Empty;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(strPath))
                {
                    strData = sr.ReadToEnd();
                    sr.Close();
                }

                StringBuilder sbData = new StringBuilder();
                sbData.Append(strData);

                string GS1_KOD_1_1D = string.Empty;
                string GS1_KOD_1_TX = string.Empty;
                string GS1_KOD_2_1D = string.Empty;
                string GS1_KOD_2_TX = string.Empty;
                string SSCC = string.Empty;
                string SSCC_bez_nul = string.Empty;
                string WEIGHT = string.Empty;

                string BarcodeP = string.Empty;
                string Expiration = string.Empty;
                string Serltnum = string.Empty;
                string ExpirationRRMMDD = string.Empty;



                string Expiration_YYYY_MM_DD = string.Empty;




                sbData.Replace("$SOURCE$", "Konzola");

                Fask.Interfaces.DataSets.Prodej.CZMST_DIDataTable dt = new Fask.Interfaces.DataSets.Prodej.CZMST_DIDataTable();

                foreach (DataColumn dcol in dt.Columns)
                {
                    string key = dcol.ColumnName;
                    string value = row[dcol.ColumnName].ToString();

                    try
                    {
                        sbData.Replace("$" + key + "$", value.Trim());
                    }
                    catch
                    {
                    }
                }



                return sbData.ToString();
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return null;
            }
        }

        private Dictionary<string, string> PrepareDataToTisk(Fask.Interfaces.DataSets.Prodej.CZMST_DIRow row_data)
        {
            try
            {
                while (true)
                {
                    try
                    {
                        Dictionary<string, string> data = new Dictionary<string, string>();


                        data.Add("SOURCE", "Konzola");

                        Fask.Interfaces.DataSets.Prodej.CZMST_DIDataTable dt = new Fask.Interfaces.DataSets.Prodej.CZMST_DIDataTable();


                        foreach (DataColumn dcol in dt.Columns)
                        {
                            string key = dcol.ColumnName;
                            string value = row_data[dcol.ColumnName].ToString();
                            if (!data.ContainsKey(key))
                                data.Add(key, value);
                        }


                        return data;
                    }
                    catch (Exception ex)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                        return null;
                    }
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void Save_Etikety(string SubPath, string Zdroj, string Obsah, Guid G, string Pripona)
        {

            string FileName = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_fff_") + Zdroj + "_" + G.ToString() + Pripona;
            if (string.IsNullOrEmpty(Obsah))
            {
                string c = Path.Combine(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Stitky_Path, "ETIKETY" + @"\" + FileName);
                string msg = string.Format("Halo tady je prazdny soubor, proč?" + Environment.NewLine +
                    "SubPath: {0}" + Environment.NewLine +
                    "Zdroj: {1}" + Environment.NewLine +
                    "G: {2}" + Environment.NewLine +
                    "Pripona: {3}" + Environment.NewLine,
                    SubPath,
                    Zdroj,
                    G,
                    Pripona
                    );
                ExceptionHandler2.Handle("", c);
            }


            string cesta = Path.Combine(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Stitky_Path, SubPath + @"\" + FileName);
            ExceptionHandler2.Handle(Obsah, cesta);

        }
        #endregion



    }
}

