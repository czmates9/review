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
using Fask.Interfaces.Classes;
using System.IO;
using Konzola.Forms;
using MST_Print_Server_ZPL_Printing;

namespace Konzola.Vyroba.Rozbory
{
    public partial class FormOdvod_EventsList : Form
    {

        private bool opravneni = false;

        #region Parametry

        protected Fask.Interfaces.IMES provider = null;
        private Fask.Interfaces.IMES providerTisk = null; //**DONE
        protected Fask.Interfaces.Classes.ZOBRAZENI_TYP _zobrazeni = Fask.Interfaces.Classes.ZOBRAZENI_TYP.UNKNOWN;
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
                            menuStrip2.Items.Remove(tsmiPolozka);
                            menuStrip2.Items.Remove(tsmiAkce);
                            menuStrip2.Items.Remove(tsmiExporty);
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
        /// Kolekce veškerých zvolených sloupců.
        /// </summary>
        public DataGridViewSelectedRowCollection SelectedRows
        {
            get
            {
                try
                {
                    return dg_OdvodEvents.SelectedRows;
                }
                catch
                {
                    return null;
                }
            }
        }

        public Fask.Interfaces.DataSets.Vyroba.FASK_EventsRow FASK_Events_SelectedRow
        {
            get
            {
                try
                {
                    return ((DataRowView)(dg_OdvodEvents.BindingContext[bs_OdvodEvents].Current)).Row as Fask.Interfaces.DataSets.Vyroba.FASK_EventsRow;
                }
                catch
                {
                    return null;
                }
            }
        }

        private List<Fask.Interfaces.DataSets.Vyroba.FASK_EventsRow> FASK_Events_selectedRows
        {
            //get
            //{
            //    List<Fask.Interfaces.DataSets.Vyroba.FASK_EventsRow> rows = new List<Fask.Interfaces.DataSets.Vyroba.FASK_EventsRow>();

            //    foreach (DataGridViewRow selectedRow in dg_OdvodEvents.SelectedRows)
            //    {
            //        Fask.Interfaces.DataSets.Vyroba.FASK_EventsRow row = ((DataRowView)selectedRow.DataBoundItem).Row as Fask.Interfaces.DataSets.Vyroba.FASK_EventsRow;
            //        rows.Add(row);
            //    }

            //    return rows;
            //}

            get
            {
                return dg_OdvodEvents.SelectedRows
                    .Cast<DataGridViewRow>()
                    .OrderBy(r => r.Index)               // zarovnání na vizuální pořadí v gridu
                    .Select(r => ((DataRowView)r.DataBoundItem).Row as Fask.Interfaces.DataSets.Vyroba.FASK_EventsRow)
                    .Where(r => r != null)
                    .ToList();
            }
        }

        #region Parametry Filtry


        /// <summary>
        /// Seznam vsech nactenych filtru.
        /// </summary>
        private List<Fask.Interfaces.Filtry.Odvod_EventsListFiltr> filtry = new List<Fask.Interfaces.Filtry.Odvod_EventsListFiltr>();

        /// <summary>
        /// Vybrany filtr.
        /// </summary>
        private Fask.Interfaces.Filtry.Odvod_EventsListFiltr rowFiltr
        {
            get
            {
                try
                {
                    return tscbFiltry.SelectedItem as Fask.Interfaces.Filtry.Odvod_EventsListFiltr;
                }
                catch
                {
                    return null;
                }
            }
        }


        #endregion


        #endregion

        #region eventy Formu

        public FormOdvod_EventsList() 
        {
            InitializeComponent();

            this.dg_OdvodEvents.UpdateColumnHeaderCellsByDatasource();

            dtp_OD.Format = DateTimePickerFormat.Custom;
            dtp_OD.CustomFormat = "dd/MM/yyyy HH:mm:ss";

            dtp_DO.Format = DateTimePickerFormat.Custom;
            dtp_DO.CustomFormat = "dd/MM/yyyy HH:mm:ss";

            dtp_porizeno_OD.Format = DateTimePickerFormat.Custom;
            dtp_porizeno_OD.CustomFormat = "dd/MM/yyyy HH:mm:ss";

            dtp_porizeno_DO.Format = DateTimePickerFormat.Custom;
            dtp_porizeno_DO.CustomFormat = "dd/MM/yyyy HH:mm:ss";

        }

        public FormOdvod_EventsList(bool allowMultiSelect, Fask.Interfaces.Classes.ZOBRAZENI_TYP typZobrazeni) : this()
        {
            
            this.dg_OdvodEvents.MultiSelect = allowMultiSelect;
            this.Zobrazeni = typZobrazeni;

            tsmiAkce.DropDownItems.Remove(tsmiArchivaceVybrane);

            if (Zobrazeni == Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST)
            {
                panelButtonsZobrazeniList.Menu = menuStrip2;
                this.WindowState = FormWindowState.Maximized;
            }
            
        }

        public FormOdvod_EventsList(bool allowMultiSelect, Fask.Interfaces.Classes.ZOBRAZENI_TYP typZobrazeni, bool opravneni) : this()
        {

            this.dg_OdvodEvents.MultiSelect = allowMultiSelect;
            this.Zobrazeni = typZobrazeni;

            if (!opravneni)
            {
                tsmiAkce.DropDownItems.Remove(tsmiArchivaceVybrane);
            }



            if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_P_ZP())
            {
                tiskEtiketToolStripMenuItem.Enabled = true;



            }
            else
            {
                tiskEtiketToolStripMenuItem.Enabled = false;
                tsmiExporty.DropDownItems.Remove(tiskEtiketToolStripMenuItem);
            }





            if (Zobrazeni == Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST)
            {
                panelButtonsZobrazeniList.Menu = menuStrip2;
                this.WindowState = FormWindowState.Maximized;
            }

            this.opravneni = opravneni;

        

        }

        //private bool opravneniArchivace = false;

        private bool opravneniEditace = false;
        private bool opravneniImport = false;
        private bool opravneniArchivace = false;

        private Opravneni opravneni_1;

        private void SetOpravneni()
        {

            if (opravneni_1.HasFlag(Opravneni.Editace) && opravneni_1.HasFlag(Opravneni.Archivace) && opravneni_1.HasFlag(Opravneni.Import))
            {
                opravneniEditace = true;
                opravneniImport = true;
                opravneniArchivace = true;
                // Povolit funkce pro oba případy
                //MessageBox.Show("Máte oprávnění k editaci i importu.");
            }

            // Nastavíte možnosti formuláře na základě oprávnění
            if (opravneni_1.HasFlag(Opravneni.Editace))
            {
                opravneniEditace = true;
                // Povolit funkce pro editaci
                // například povolit nějaké tlačítka nebo editační pole
            }

            if (opravneni_1.HasFlag(Opravneni.Import))
            {
                opravneniImport = true;
                // Povolit funkce pro import
                // například povolit tlačítko nebo nabídku pro import
            }

            if (opravneni_1.HasFlag(Opravneni.Archivace))
            {
                opravneniArchivace = true;
                // Povolit funkce pro import
                // například povolit tlačítko nebo nabídku pro import
            }

            // Zkontroluje, zda má uživatel obě oprávnění


            // Můžete přidat další logiku pro další oprávnění
        }

        public FormOdvod_EventsList(bool allowMultiSelect, Fask.Interfaces.Classes.ZOBRAZENI_TYP typZobrazeni, Opravneni opravneni) : this()
        {

            this.dg_OdvodEvents.MultiSelect = allowMultiSelect;
            this.Zobrazeni = typZobrazeni;

            //if (!opravneniArchivace)
            //{
            //    tsmiAkce.DropDownItems.Remove(tsmiArchivaceVybrane);
            //}

            if (Zobrazeni == Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST)
            {
                panelButtonsZobrazeniList.Menu = menuStrip2;
                this.WindowState = FormWindowState.Maximized;
            }

            this.opravneni_1 = opravneni;
            SetOpravneni();


        }

        private void FormOdvod_EventsList_Load(object sender, EventArgs e)
        {
            try
            {

                if (!opravneniArchivace)
                {
                    //if (!Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_VolnyPohyb_Nasnimane_ImpDavku)
                    //    menuStrip2.Items.Remove(tsmiAkce);

                    tsmiAkce.DropDownItems.Remove(tsmiArchivaceVybrane);
                }

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_P_ZP())
                {
                    tiskEtiketToolStripMenuItem.Enabled = true;



                }
                else
                {
                    tiskEtiketToolStripMenuItem.Enabled = false;
                    tsmiExporty.DropDownItems.Remove(tiskEtiketToolStripMenuItem);
                }


                this.ShowIcon = false;
                //this.WindowState = FormWindowState.Maximized;
                this.dg_OdvodEvents.LoadConfiguration(this.GetType().ToString());

                if (Zobrazeni == Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST)
                {
                    panelButtonsZobrazeniList.LoadConfiguration(this.GetType().ToString());
                    panelButtonsZobrazeniList.Init();
                }

                advancedDataGridViewSearchToolBar1.SetColumns(dg_OdvodEvents.Columns);

                //// načtení konfigurace vytvořených filtrů
                this.filtry = Extensions.ListExt.ReadFromXML<Fask.Interfaces.Filtry.Odvod_EventsListFiltr>(this.GetType().ToString() + ".filtr");
                tscbFiltry.ComboBox.DataSource = this.filtry;
                tscbFiltry.SelectedItem = null;
                tscbFiltry.ComboBox.DropDownWitdhAutosize();

                DateTime today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);

                dtp_OD.Value = today.AddDays(-1);
                dtp_DO.Value = today;

                dtp_OD.Checked = false;
                dtp_DO.Checked = false;

                dtp_porizeno_OD.Value = today.AddDays(-1);
                dtp_porizeno_DO.Value = today;


                dtp_porizeno_OD.Checked = false;
                dtp_porizeno_DO.Checked = false;

                cb_Porizeno_TimeVariant.Items.AddRange(Fask.Interfaces.Classes.TimeFilters.GetNumberNameRange());
                cb_Porizeno_TimeVariant.SelectedIndex = 0;

                cb_TimeVariant.Items.AddRange(Fask.Interfaces.Classes.TimeFilters.GetNumberNameRange());
                cb_TimeVariant.SelectedIndex = 0;

                InitProvider();





                SetStatusLabelText_Events(-1);





                //PerformVyhledat();
                buttonVyhledat.Focus();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormOdvod_EventsList_KeyDown(object sender, KeyEventArgs e)
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


        private void FormOdvod_EventsList_Shown(object sender, EventArgs e)
        {
            try
            {
                this.progressIndicator1.Size = new Size(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ostatni[0].ProgressIndicatorSize, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ostatni[0].ProgressIndicatorSize);
                this.progressIndicator1.Location = new Point(this.dg_OdvodEvents.Location.X + (this.dg_OdvodEvents.Width / 2) - (progressIndicator1.Size.Width / 2), this.dg_OdvodEvents.Location.Y + (this.dg_OdvodEvents.Height / 2) - (progressIndicator1.Size.Height / 2));
            }
            catch { }
        }


        #endregion

        //public Fask.Interfaces.Classes.TimeFilters.TimeVariants GetTimeVariantFromFilter_porizeno()
        //{
        //    Fask.Interfaces.Classes.TimeVariantName type = (Fask.Interfaces.Classes.TimeVariantName)cb_Porizeno_TimeVariant.SelectedItem;
        //    return type.GetTimeVarianta();
        //}

        //public Fask.Interfaces.Classes.TimeFilters.TimeVariants GetTimeVariantFromFilter()
        //{
        //    Fask.Interfaces.Classes.TimeVariantName type = (Fask.Interfaces.Classes.TimeVariantName)cb_TimeVariant.SelectedItem;
        //    return type.GetTimeVarianta(); 
        //}

        public string GetTimeVariantFromFilter_porizeno()
        {
            Fask.Interfaces.Classes.TimeVariantName type = (Fask.Interfaces.Classes.TimeVariantName)cb_Porizeno_TimeVariant.SelectedItem;
            return type.ToString_Filter();
        }

        public string GetTimeVariantFromFilter()
        {
            Fask.Interfaces.Classes.TimeVariantName type = (Fask.Interfaces.Classes.TimeVariantName)cb_TimeVariant.SelectedItem;
            return type.ToString_Filter();
        }

        #region Eventy

        private void buttonNovy_Click(object sender, EventArgs e)
        {
            PerformOK();
        }


        private void konecToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void FormUzivateleList_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (Zobrazeni == Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST)
                {
                    panelButtonsZobrazeniList.SaveConfiguration(this.GetType().ToString());
                }

                this.dg_OdvodEvents.SaveConfiguration(this.GetType().ToString());

                this.filtry.WriteXML(this.GetType().ToString() + ".filtr");

                WaithToEndThread();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void buttonVyhledat_Click(object sender, EventArgs e)
        {
            #region prace s datagridem - nastaveni numeric hodnot na pocet desetinnych mist
            try
            {
                // Procházení sloupců v DataGridView
                foreach (DataGridViewColumn column in dg_OdvodEvents.Columns)
                {
                    // Získání typu dat pro sloupec
                    Type dataType = null;

                    if (dg_OdvodEvents.DataSource is DataTable dataTable)
                    {
                        // Pokud je datový zdroj DataTable
                        if (dataTable.Columns.Contains(column.DataPropertyName))
                        {
                            dataType = dataTable.Columns[column.DataPropertyName].DataType;
                        }
                    }
                    else if (dg_OdvodEvents.DataSource is BindingSource bindingSource)
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
                    //else
                    //{
                    //    column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    //}

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

        private void tsmiStorno_Click(object sender, EventArgs e)
        {
            PerformStorno();
        }

        private void PerformStorno()
        {
            try
            {

                if (SelectedRows.Count > 1)
                {
                    MessageBox.Show("Je vybrán víc jak jeden záznam!", this.Text, MessageBoxButtons.OK);
                    return;
                }


                if (FASK_Events_SelectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if (!((FASK_Events_SelectedRow.status == 0 || FASK_Events_SelectedRow.status == 900) && FASK_Events_SelectedRow.description == "automaticke ulozeni paleta"))
                {
                    //MessageBox.Show(string.Format("Status je jiní jak \"0\" anebo \"900\"! Statuje je: {0}", SelectedRow.status), this.Text, MessageBoxButtons.OK);
                    MessageBox.Show("Stornovat lze pouze 'automatické uložení paleta' se statusem 0 nebo 900", this.Text, MessageBoxButtons.OK);
                    return;
                }
                

                DialogResult dr = DialogResult.Abort;

                using (FormOdvod_EventsList_Storno frm = new FormOdvod_EventsList_Storno())
                {
                    frm.Radek = FASK_Events_SelectedRow;
                    dr = frm.ShowDialog();
                }

                if(dr == DialogResult.Yes)
                    PerformVyhledat();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


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
            PerformNastavitFiltr(rowFiltr);
        }

        #endregion

        #endregion

        #region Protected metody

        /// <summary>
        /// Inicializace providera
        /// </summary>
        public void InitProvider()
        {
            try
            {
                if (!String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                {
                    if (provider == null) //inicializace se provede pouze pokud nebyla provedena ... 
                    {
                        //Assembly providerAssemlby = Assembly.LoadFrom(Server.MapPath(providerAssemblyPath));
                        Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                        Type[] types = providerAssemlby.GetTypes();
                        foreach (Type t in types)
                        {
                            try
                            {
                                if (typeof(Fask.Interfaces.Vyroba.Odvod_Events.IOdvod_Events).IsAssignableFrom(t))
                                {
                                    provider = (Fask.Interfaces.Vyroba.Odvod_Events.IOdvod_Events)providerAssemlby.CreateInstance(t.FullName);
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

        /// <summary>
        /// MEtoda pro Vybrat material
        /// </summary>
        public virtual void PerformOK() { }

        /// <summary>
        /// Metoda na čekani dobehnuti vlakna
        /// </summary>
        public virtual void WaithToEndThread()
        {
            //// cekani na dobehnuti vlakna
            try
            {
                if (bw_OdvodEvents.IsBusy)
                {
                    bw_OdvodEvents.CancelAsync();
                    while (bw_OdvodEvents.IsBusy)
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
        public  void PerformVyhledat() {


            try
            {
                DataTable dtchanged = this.ds_OdvodEvents.FASK_Events.GetChanges();
                if (dtchanged != null && dtchanged.Rows.Count > 0) //obsahuje zmeny ... 
                {
                    DialogResult dr = MessageBox.Show("Obsahuje změny, pokračovat?", "data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == System.Windows.Forms.DialogResult.No)
                        return;
                }

                if (bw_OdvodEvents.IsBusy)
                {
                    bw_OdvodEvents.CancelAsync();
                    while (bw_OdvodEvents.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }
                
                ProgressIndicatorStart();

                Fask.Interfaces.Filtry.Odvod_EventsListFiltr filtr = new Fask.Interfaces.Filtry.Odvod_EventsListFiltr();
                if (!CreateFilter(ref filtr))
                {
                    ProgressIndicatorStop();
                    return;
                }

                int FirstDisplayedScrollingRowIndex = this.dg_OdvodEvents.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

                bw_OdvodEvents.RunWorkerAsync(filtr);

                if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dg_OdvodEvents.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dg_OdvodEvents.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index
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
        public  void PerformOdebratFiltr() 
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

                Fask.Interfaces.Filtry.Odvod_EventsListFiltr filtr = new Fask.Interfaces.Filtry.Odvod_EventsListFiltr();
                string nazev = string.Empty;
                DialogResult dr = Forms.InputBox.Show("Název filtru", "Zadejte název filtru", string.Empty, false, out nazev);
                if (dr != System.Windows.Forms.DialogResult.OK)
                    return;

                string nameFile = Guid.NewGuid().ToString() + "_" + this.GetType().ToString();
                this.dg_OdvodEvents.SaveConfiguration(nameFile);

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

                Fask.Interfaces.Filtry.Odvod_EventsListFiltr filtr = rowFiltr;

                string nameFile = string.Empty;

                if (string.IsNullOrEmpty(filtr.NameFileDataGridView))
                {
                   nameFile = Guid.NewGuid().ToString() + "_" + this.GetType().ToString();
                }
                else
                    nameFile = filtr.NameFileDataGridView;

                this.dg_OdvodEvents.SaveConfiguration(nameFile);

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
                textBox1.Text = string.Empty;
                dtp_OD.Checked = false;
                dtp_DO.Checked = false;
                chb_Zpravovane.Checked = false;
                chb_Nezpracovane.Checked = false;

                dtp_porizeno_OD.Checked = false;
                dtp_porizeno_DO.Checked = false;
                cb_Porizeno_TimeVariant.SelectedIndex = 0;
                cb_TimeVariant.SelectedIndex = 0;

                tb_MachineID.Text = string.Empty;
                tb_description.Text = string.Empty;
                tb_status.Text = string.Empty;
                cb_PackType.Text = string.Empty;

                cb_Razeni.Text = string.Empty;

                this.dg_OdvodEvents.LoadConfiguration(this.GetType().ToString());

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
        public void PerformNastavitFiltr(Fask.Interfaces.Filtry.Odvod_EventsListFiltr filtr) 
        {
            try
            {
                if (filtr == null)
                    return;

                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                PerformVycistitFiltr();

                if (filtr.productionGUID != null)
                {
                    textBox1.Text = filtr.productionGUID.ToString();
                }

                if (filtr.IsProcessed_OD != null)
                {
                    dtp_OD.Checked = true;
                    dtp_OD.Value = (DateTime)filtr.IsProcessed_OD;
                }

                if (filtr.IsProcessed_DO != null)
                {
                    dtp_DO.Checked = true;
                    dtp_DO.Value = (DateTime)filtr.IsProcessed_DO;
                }

                chb_Nezpracovane.Checked = filtr.NEZpracovane  ;
                chb_Zpravovane.Checked = filtr.Zpracovane;

                if (!string.IsNullOrEmpty(filtr.IsProcessed_TimeVariant))
                {

                    var arr = filtr.IsProcessed_TimeVariant.Split(';');
                    TimeFilters.TimeVariants TimeVar = (TimeFilters.TimeVariants)Enum.Parse(typeof(TimeFilters.TimeVariants), arr[0], true);

                    TimeVariantName time = new TimeVariantName(TimeVar,arr[1]);

                    cb_TimeVariant.SelectedItem = time;
                }


                if (filtr.Dateeve_DO != null)
                {
                    dtp_porizeno_DO.Checked = true;
                    dtp_porizeno_DO.Value = (DateTime)filtr.Dateeve_DO;
                }

                if (filtr.Dateeve_OD != null)
                {
                    dtp_porizeno_OD.Checked = true;
                    dtp_porizeno_OD.Value = (DateTime)filtr.Dateeve_OD;
                }

                if (!string.IsNullOrEmpty(filtr.Dateeve_TimeVariant))
                {

                    var arr = filtr.Dateeve_TimeVariant.Split(';');
                    TimeFilters.TimeVariants TimeVar = (TimeFilters.TimeVariants)Enum.Parse(typeof(TimeFilters.TimeVariants), arr[0], true);

                    TimeVariantName time = new TimeVariantName(TimeVar, arr[1]);

                    cb_Porizeno_TimeVariant.SelectedItem = time;
                }

                if (!string.IsNullOrEmpty(filtr.MachineID))
                {
                    tb_MachineID.Text = filtr.MachineID;
                }

                if (!string.IsNullOrEmpty(filtr.Description))
                {
                    tb_description.Text = filtr.Description;
                }

                if (!string.IsNullOrEmpty(filtr.status))
                {
                    tb_status.Text = filtr.status;
                }

                if (!string.IsNullOrEmpty(filtr.PackType))
                {
                    cb_PackType.Text = filtr.PackType;
                }

                if (!string.IsNullOrEmpty(filtr.Razeni_Column))
                {

                    switch (filtr.Razeni_Column.Trim())
                    {
                        case "id":
                            cb_Razeni.Text = "ID";
                            break;
                        case "dateeve":
                            cb_Razeni.Text = "Pořízeno";
                            break;
                        case "barcodeSended":
                            cb_Razeni.Text = "Odeslaný kód";
                            break;
                        case "NMBRPAL":
                            cb_Razeni.Text = "SSCC";
                            break;
                        default:
                            break;
                    }

                    //cb_Razeni.Text = filtr.Razeni_Column;
                }

                if (!string.IsNullOrEmpty(filtr.asc_desc))
                {
                    if (filtr.asc_desc == "ASC")
                        chb_Razeni.Text = "Vzestupně";
                    else if (filtr.asc_desc == "DESC")
                        chb_Razeni.Text = "Sestupně";
                }

                if(string.IsNullOrEmpty(filtr.NameFileDataGridView))
                    this.dg_OdvodEvents.LoadConfiguration(this.GetType().ToString());
                else
                    this.dg_OdvodEvents.LoadConfiguration(filtr.NameFileDataGridView);


            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show("Nepodařilo se načíst data filtru.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Vytvoreni filtru, ktery se pouzije.
        /// </summary>
        /// <param name="filtr"></param>
        /// <returns></returns>
        private bool CreateFilter(ref Fask.Interfaces.Filtry.Odvod_EventsListFiltr filtr)
        {
            if (filtr == null)
                filtr = new Fask.Interfaces.Filtry.Odvod_EventsListFiltr();

            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                Guid G = Guid.Empty;
                if (Guid.TryParse(textBox1.Text, out G))
                {
                    filtr.productionGUID = G;
                }
                else
                {
                    filtr.productionGUID = null;
                    MessageBox.Show(this, "Zadaný GUID: '" + textBox1.Text + "' neni korekní." + Environment.NewLine + " Hledání nebude provedeno!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    return false;
                }
            }

            #region IsProcessed

            if (dtp_OD.Checked)
            {
                filtr.IsProcessed_OD = dtp_OD.Value;
            }
            else
                filtr.IsProcessed_OD = null;

            if (dtp_DO.Checked)
            {
                filtr.IsProcessed_DO = dtp_DO.Value;
            }
            else
                filtr.IsProcessed_DO = null;

            #endregion

            #region Zpracovane / nezpracovane

            filtr.NEZpracovane = chb_Nezpracovane.Checked;
            filtr.Zpracovane = chb_Zpravovane.Checked; 

            #endregion

            #region Time variant IsProcessed

            var v = GetTimeVariantFromFilter();

            if (v.Contains("unknow"))
            {
                filtr.IsProcessed_TimeVariant = null;
            }
            else
            {
                filtr.IsProcessed_TimeVariant = v;
            }

            #endregion

            #region Dateeve

            if (dtp_porizeno_OD.Checked && dtp_porizeno_OD.Enabled)
            {
                filtr.Dateeve_OD = dtp_porizeno_OD.Value;
            }
            else
                filtr.Dateeve_OD = null;

            if (dtp_porizeno_DO.Checked && dtp_porizeno_DO.Enabled)
            {
                filtr.Dateeve_DO = dtp_porizeno_DO.Value;
            }
            else
                filtr.Dateeve_DO = null; 

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

            #region řazeni

            if (chb_Razeni.Text == "Vzestupně")
            {
                filtr.asc_desc = "ASC";
            }
            else if (chb_Razeni.Text == "Sestupně")
            {
                filtr.asc_desc = "DESC";
            }


            switch (cb_Razeni.Text.Trim())
            {
                case "ID":
                    filtr.Razeni_Column = "id";
                    break;
                case "Pořízeno":
                    filtr.Razeni_Column = "dateeve";
                    break;
                case "Odeslaný kód":
                    filtr.Razeni_Column = "barcodeSended";
                    break;
                case "SSCC":
                    filtr.Razeni_Column = "NMBRPAL";
                    break;
                default:
                    break;
            }

            #endregion

            #region Filtry 

            filtr.MachineID = tb_MachineID.Text;
            filtr.Description = tb_description.Text;
            filtr.status = tb_status.Text;
            filtr.PackType = cb_PackType.Text;

            #endregion

            return true;
        }


        #endregion


        #endregion

        #region Ostatni metody

        protected void PerformCancel()
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


        protected void ProgressIndicatorStop()
        {
            progressIndicator1.Stop();
            progressIndicator1.Visible = false;
        }

        protected void ProgressIndicatorStart()
        {
            try
            {
                // prepocet stredu datagridu
                this.progressIndicator1.Location = new Point(this.dg_OdvodEvents.Location.X + (this.dg_OdvodEvents.Width / 2) - (progressIndicator1.Size.Width / 2), this.dg_OdvodEvents.Location.Y + (this.dg_OdvodEvents.Height / 2) - (progressIndicator1.Size.Height / 2));
            }
            catch { }
            progressIndicator1.Start();
            progressIndicator1.Visible = true;
        }

        #endregion

        #region Exporty



        private void exportDoCSVVseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                this.dg_OdvodEvents.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                this.dg_OdvodEvents.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
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

                this.dg_OdvodEvents.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                dg_OdvodEvents.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
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

                dg_OdvodEvents.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                dg_OdvodEvents.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
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
                bool endcol = dg_OdvodEvents.CurrentCell.ColumnIndex + 1 >= dg_OdvodEvents.ColumnCount;
                bool endrow = dg_OdvodEvents.CurrentCell.RowIndex + 1 >= dg_OdvodEvents.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dg_OdvodEvents.CurrentCell.ColumnIndex;
                    startRow = dg_OdvodEvents.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dg_OdvodEvents.CurrentCell.ColumnIndex + 1;
                    startRow = dg_OdvodEvents.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dg_OdvodEvents.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dg_OdvodEvents.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dg_OdvodEvents.CurrentCell = c;
        }

        private void bw_OdvodEvents_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Fask.Interfaces.Filtry.Odvod_EventsListFiltr filtr = (Fask.Interfaces.Filtry.Odvod_EventsListFiltr)e.Argument;
                Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();

                if (bw_OdvodEvents.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                // nacteni dat v oddelenem vlakne
                if ((provider != null) && (provider is Fask.Interfaces.Vyroba.Odvod_Events.IOdvod_Events_GetFiltrovanyOdvodEvents))
                {
                    ds = ((Fask.Interfaces.Vyroba.Odvod_Events.IOdvod_Events_GetFiltrovanyOdvodEvents)provider).GetFiltrovanyOdvodEvents(filtr);
                }
                else
                {
                    throw new NotImplementedException("Provider neobsahuje implemetaci IOdvod_Events_GetFiltrovanyOdvodEvents");
                }

                if (bw_OdvodEvents.CancellationPending)
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

        private void bw_OdvodEvents_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                // check error, check cancel, then use result
                if (e.Error != null)
                {
                    // handle the error
                    ds_OdvodEvents = new Fask.Interfaces.DataSets.Vyroba();
                    bs_OdvodEvents.DataSource = ds_OdvodEvents;
                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, e.Error);
                    MessageBox.Show(e.Error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    SetStatusLabelText_Events(-1);
                }
                else if (e.Cancelled)
                {
                    // handle cancellation
                    // TODO: maji se vymazat data z datasetu?? asi ano ...
                    //if (dsSkladPohyb == null)
                    ds_OdvodEvents = new Fask.Interfaces.DataSets.Vyroba();
                    bs_OdvodEvents.DataSource = ds_OdvodEvents;

                    SetStatusLabelText_Events(-1);
                }
                else
                {
                    // uspesne dokonceno ...
                    // use it on the UI thread
                    ds_OdvodEvents = (Fask.Interfaces.DataSets.Vyroba)e.Result;
                    if (ds_OdvodEvents == null)
                        ds_OdvodEvents = new Fask.Interfaces.DataSets.Vyroba();

                    bs_OdvodEvents.DataSource = ds_OdvodEvents;

                    if (ds_OdvodEvents.FASK_Events.Count == 0)
                        SetStatusLabelText_Events(-1);
                    else
                    {
                        foreach (DataGridViewRow row in dg_OdvodEvents.SelectedRows)
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
                ProgressIndicatorStop();
            }
        }

        private void tsmi_nacteniVyroby_Click(object sender, EventArgs e)
        {

            try
            {
                //var task = new System.Threading.Tasks.Task(() => {
                //    PerformAkce_NacteniVyroby(); 
                //});
                //task.Start();
                //task.Wait();

                PerformAkce_NacteniVyroby();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                return;
            }

            PerformVyhledat();
        }

        void PerformAkce_NacteniVyroby()
        {
            try
            {

                DateTime? OD = null;
                DateTime? DO = null;
                string Mat = string.Empty;
                DataTable dt = null;


                if ((provider != null) && (provider is Fask.Interfaces.Vyroba.Odvod_Events.IOdvod_Events_GetMaterials))
                {
                    dt = ((Fask.Interfaces.Vyroba.Odvod_Events.IOdvod_Events_GetMaterials)provider).GetMaterials();
                }
                else
                {
                    throw new NotImplementedException("Provider neobsahuje implemetaci IOdvod_Events_GetMaterials");
                }

                using (Forms.Form_DateTime_From_To frm = new Forms.Form_DateTime_From_To())
                {
                    frm.Popis = tsmiNacteniVyroby.Text;
                    frm.DataTableMaterialy = dt;

                    DialogResult dr = frm.ShowDialog();

                    if (dr == System.Windows.Forms.DialogResult.OK)
                    {
                        OD = frm.OD;
                        DO = frm.DO;
                        Mat = frm.Material.Trim();
                    }
                    else
                    {
                        return;
                    }
                }

                //System.Threading.Tasks.

                ProgressIndicatorStart();

                //throw new Exception("my test task exception");

                string msg = null;
                int? CountEntries = null;

                //var task2 = new System.Threading.Tasks.Task(() =>
                //    {
                //        if ((provider != null) && (provider is Fask.Interfaces.Vyroba.Odvod_Events.IOdvod_Events_CallProcedura))
                //        {
                //            msg = ((Fask.Interfaces.Vyroba.Odvod_Events.IOdvod_Events_CallProcedura)provider).CallProcedura(OD, DO, Mat, out CountEntries);
                //        }
                //        else
                //        {
                //            throw new NotImplementedException("Provider neobsahuje implemetaci IOdvod_Events_CallProcedura");
                //        }
                //    });
                //task2.Start();
                //task2.Wait();

                if ((provider != null) && (provider is Fask.Interfaces.Vyroba.Odvod_Events.IOdvod_Events_CallProcedura))
                {
                    msg = ((Fask.Interfaces.Vyroba.Odvod_Events.IOdvod_Events_CallProcedura)provider).CallProcedura(OD, DO, Mat, out CountEntries);
                }
                else
                {
                    throw new NotImplementedException("Provider neobsahuje implemetaci IOdvod_Events_CallProcedura");
                }

                if (!string.IsNullOrEmpty(msg))
                    MessageBox.Show(this, msg, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                else
                    MessageBox.Show(this, "Akce dokončena. Bez zpětné vazby!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                ProgressIndicatorStop();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb != null)
            {
                if (string.IsNullOrEmpty(tb.Text))
                {
                    tb.BackColor = SystemColors.Window;
                    return;
                }

                try
                {
                    Guid.Parse(tb.Text);
                    tb.BackColor = Color.LightGreen;

                }
                catch
                {
                    tb.BackColor = Color.MistyRose;
                }
            }
        }

        private void cb_Porizeno_TimeVariant_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(sender is ComboBox)
            {
                Fask.Interfaces.Classes.TimeVariantName type = (Fask.Interfaces.Classes.TimeVariantName)((ComboBox)sender).SelectedItem;
                var v = type.GetTimeVarianta();

                if(v == Fask.Interfaces.Classes.TimeFilters.TimeVariants.unknow)
                {
                    dtp_porizeno_DO.Enabled =
                    dtp_porizeno_OD.Enabled = true;
                }
                else
                {
                    dtp_porizeno_DO.Enabled =
                    dtp_porizeno_OD.Enabled = false;
                }
            }
        }

        private void cb_TimeVariant_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is ComboBox)
            {
                Fask.Interfaces.Classes.TimeVariantName type = (Fask.Interfaces.Classes.TimeVariantName)((ComboBox)sender).SelectedItem;
                var v = type.GetTimeVarianta();

                if (v == Fask.Interfaces.Classes.TimeFilters.TimeVariants.unknow)
                {
                    dtp_OD.Enabled =
                    dtp_DO.Enabled = true;
                }
                else
                {
                    dtp_OD.Enabled =
                    dtp_DO.Enabled = false;
                }
            }
        }

        private void chb_Razeni_CheckedChanged(object sender, EventArgs e)
        {
            var x = sender is CheckBox;

            if (x)
            {
                CheckBox ch = sender as CheckBox;

                if (ch.Text == "Vzestupně")
                {
                    ch.Text = "Sestupně";
                }
                else if (ch.Text == "Sestupně")
                {
                    ch.Text = "Vzestupně";
                }
            }
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

            tssl_Eventu_Count.Text = string.Format("{0}/{1}", index + 1, ds_OdvodEvents.FASK_Events.Count);
        }

        private void dg_OdvodEvents_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dg_OdvodEvents.SelectedRows)
            {
                SetStatusLabelText_Events(row.Index);
            }
        }

        //private void tiskToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    PerformPrint();
        //}

        private void PerformPrint()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (FASK_Events_SelectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if (FASK_Events_selectedRows.Count > 1)
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

                Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable dt = new Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable();

                foreach (Fask.Interfaces.DataSets.Vyroba.FASK_EventsRow row in FASK_Events_selectedRows)
                {
                    try
                    {
                        Fask.Interfaces.DataSets.Vyroba.FASK_EventsRow newRow = dt.NewFASK_EventsRow();
                        // Ošetřit delší vstupní pole než počet sloupců tabulky
                        int copyLength = Math.Min(row.ItemArray.Length, dt.Columns.Count);
                        newRow.ItemArray = row.ItemArray.Take(copyLength).ToArray();
                        dt.AddFASK_EventsRow(newRow);
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


        private void PrintReport(Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable dt)
        {
            try
            {
                if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                {
                    foreach (var item in dt)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start prehled odvadeni stroju TISK rdlc-------------------------------------");

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

                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END prehled odvadeni stroju TISK rdlc-------------------------------------");
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


                    #region new vybrani tiskove  sablony
                    // Vytvoření instance formuláře
                    //FormOdvod_TiskoveSablony form = new FormOdvod_TiskoveSablony(this.Text, FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Admin());

                    FormOdvod_TiskoveSablony form = new FormOdvod_TiskoveSablony(this.Text, FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Admin(), Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Tisk_Klic_RDLC,false);

                   



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

        private void tsmiArchivaceVybrane_Click(object sender, EventArgs e)
        {
            if (opravneniArchivace)
            {
                int pocet_zaznamu = FASK_Events_selectedRows.Count;
                string zprava = string.Format("Bude archivováno {0} záznamů. \n\nChceš provést?", pocet_zaznamu);
                //DialogResult result = MessageBox.Show(zprava, "Potvrzení", MessageBoxButtons.YesNo);
                DialogResult result = MessageBox.Show(zprava, "Potvrzení", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                if (result == DialogResult.Yes)
                {
                        // Zde provedete akci, pokud bylo vybráno "Ano"
                        //Console.WriteLine("Akce 'Ano' byla vybrána.");




                        try
                        {
                            if (FASK_Events_selectedRows.Count > 0)
                            {
                                int pocetArchZaznamu = 0;
                                Guid pom = FASK_Events_selectedRows[0].faskGUID;
                                //archivace vybraných řádků
                                foreach (var item in FASK_Events_selectedRows)
                                {
                                    int id = item.id;

                                    //archivuji podle id, poslu do procedury
                                    if ((provider != null) && (provider is Fask.Interfaces.IT_cast.IIT_cast_Events_Archivace))
                                    {
                                        pocetArchZaznamu = ((Fask.Interfaces.IT_cast.IIT_cast_Events_Archivace)provider).Archivace_Fask_Event_id(id);
                                    }
                                    else
                                    {
                                        throw new NotImplementedException("Provider neobsahuje implemetaci IIT_cast_Events_Archivace ArchivaceProcedura");
                                    }

                                    pom = item.faskGUID;
                                }

                                MessageBox.Show(this, "Akce dokončena. Pocet zaarchivovaných záznamů:" + FASK_Events_selectedRows.Count.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);



                            }

                            PerformVyhledat();
                        }
                        catch (Exception ex)
                        {

                            Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                            MessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                            // return;
                        }
                    }
            }
            else
            {
                    MessageBox.Show(this, "Nedostatečná práva k akci archivace záznamů.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

            }

         
        }



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

                if (FASK_Events_SelectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                #region MaR 6.11.2024 vypis zpl do souboru selected rows
                int index_zaznamu = 0;

                foreach (var item in FASK_Events_selectedRows)
                {

                    if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start vyroba/rozbory/PrehledOdvadeniStroju TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");

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

                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END vyroba/rozbory/PrehledOdvadeniStroju TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");


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



                    foreach (var item in FASK_Events_selectedRows)
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
                        MessageBox.Show(this, "Chybí šablona pro tisk!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string plr_Path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path_sablona);

                    //odeslani dat do tiskarny
                    Tisk_selected_rows_Primy_Tisk(plr_Path, true, MN_ToTisk, FASK_Events_selectedRows, NazevVychoziTiskarny);

                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Tisk_selected_rows_Primy_Tisk(string plr_Path, bool MnozstvuAutoJedna, int MN_ToTisk, List<Fask.Interfaces.DataSets.Vyroba.FASK_EventsRow> FASK_ZASOBY_selected_Rows, string nazevVychTiskarny)
        {
            try
            {

                #region selected rows

                foreach (var item in FASK_ZASOBY_selected_Rows)
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

                if (FASK_Events_SelectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                //if (FASK_Events_selectedRows.Count > 1)
                //{
                //    //MessageBox.Show("Je vybráno více záznamů než jeden.", this.Text, MessageBoxButtons.OK);
                //    //return;
                //}

                Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable dt = new Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable();

                foreach (Fask.Interfaces.DataSets.Vyroba.FASK_EventsRow row in FASK_Events_selectedRows)
                {
                    try
                    {
                        Fask.Interfaces.DataSets.Vyroba.FASK_EventsRow newRow = dt.NewFASK_EventsRow();
                        // Ošetřit delší vstupní pole než počet sloupců tabulky
                        int copyLength = Math.Min(row.ItemArray.Length, dt.Columns.Count);
                        newRow.ItemArray = row.ItemArray.Take(copyLength).ToArray();
                        dt.AddFASK_EventsRow(newRow);
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


                }
                #endregion
                if (string.IsNullOrEmpty(path_sablona))
                {
                    MessageBox.Show(this, "Chybí šablona pro tisk!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                PrintReport_RDLC(dt, primyTisk, path_sablona, nazevVychTiskarny, MN_ToTisk);

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        }

        private void PrintReport_RDLC(Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable dt, bool primyTisk, string path_sablona, string nazevVychTiskarny, int pocetVytisku)
        {
            try
            {

                #region logovani tisku
                if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                {
                    foreach (var item in dt)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start vyroba/rozbory/PrehledOdvadeniStroju TISK rdlc-------------------------------------");

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

                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END vyroba/rozbory/PrehledOdvadeniStroju TISK rdlc-------------------------------------");
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

                if (FASK_Events_SelectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                #region MaR 6.11.2024 vypis zpl do souboru selected rows
                int index_zaznamu = 0;

                foreach (var item in FASK_Events_selectedRows)
                {

                    if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start vyroba/rozbory/PrehledOdvadeniStroju TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");

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

                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END vyroba/rozbory/PrehledOdvadeniStroju TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");


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



                    foreach (var item in FASK_Events_selectedRows)
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
                    if (string.IsNullOrEmpty(path))
                    {
                        MessageBox.Show(this, "Chybí šablona pro tisk!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    string plr_Path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
                    Tisk_selected_rows(plr_Path, true, MN_ToTisk, FASK_Events_selectedRows);

                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Tisk_selected_rows(string plr_Path, bool MnozstvuAutoJedna, int MN_ToTisk, List<Fask.Interfaces.DataSets.Vyroba.FASK_EventsRow> FASK_ZASOBY_selected_Rows)
        {
            try
            {
                PrintDialog printDialog1 = new PrintDialog();
                printDialog1.UseEXDialog = true;

                if (printDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.Cancel)
                    return;



                #region selected rows

                foreach (var item in FASK_ZASOBY_selected_Rows)
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
            Fask.Interfaces.DataSets.Vyroba.FASK_EventsRow row,
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


                #region MaR 11.11.2024 nepotrebne
                //if (row != null)
                //{



                //    if (!row.IsSERLTNUMNull())
                //    {
                //        Serltnum = row.SERLTNUM;
                //    }
                //    else
                //    {
                //        Serltnum = " ";
                //    }

                //    sbData.Replace("$BarcodeP$", BarcodeP);
                //    sbData.Replace("$Expiration$", Expiration);
                //    sbData.Replace("$ExpirationRRMMDD$", ExpirationRRMMDD);
                //    sbData.Replace("$Expiration_YYYY_MM_DD$", Expiration_YYYY_MM_DD);
                //    sbData.Replace("$Serltnum$", Serltnum);
                //    //----------------------------------------------------------

                //    if (!row.IsWEIGHTNull())
                //    {
                //        WEIGHT = row.WEIGHT.ToString();
                //    }

                //    sbData.Replace("$WEIGHT$", WEIGHT);




                //    sbData.Replace("$SSCC$", SSCC_bez_nul);


                //}

                //sbData.Replace("$GS1_KOD_1_1D$", GS1_KOD_1_1D);
                //sbData.Replace("$GS1_KOD_1_TX$", GS1_KOD_1_TX);
                //sbData.Replace("$GS1_KOD_2_1D$", GS1_KOD_2_1D);
                //sbData.Replace("$GS1_KOD_2_TX$", GS1_KOD_2_TX); 
                #endregion

                sbData.Replace("$SOURCE$", "Konzola");

                Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable dt = new Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable();

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

        private Dictionary<string, string> PrepareDataToTisk(Fask.Interfaces.DataSets.Vyroba.FASK_EventsRow productionRow_data)
        {
            try
            {
                while (true)
                {
                    try
                    {
                        Dictionary<string, string> data = new Dictionary<string, string>();

                        #region MaR 11.11. 2024 nepotrebne
                        //string GS1_KOD_1_1D = string.Empty;
                        //string GS1_KOD_1_TX = string.Empty;
                        //string GS1_KOD_2_1D = string.Empty;
                        //string GS1_KOD_2_TX = string.Empty;
                        //string SSCC = string.Empty;
                        //string SSCC_bez_nul = string.Empty;
                        //string WEIGHT = string.Empty;

                        //string BarcodeP = string.Empty;
                        //string Expiration = string.Empty;
                        //string Serltnum = string.Empty;
                        //string ExpirationRRMMDD = string.Empty;


                        //string Expiration_YYYY_MM_DD = string.Empty;



                        //if (productionRow_data != null)
                        //{



                        //    if (!productionRow_data.IsSERLTNUMNull())
                        //    {
                        //        Serltnum = productionRow_data.SERLTNUM;
                        //    }
                        //    else
                        //    {
                        //        Serltnum = " ";
                        //    }


                        //    //------------START-DATA----------------
                        //    //dotahovat data SSCC a WEIGHT

                        //    if (!productionRow_data.IsWEIGHTNull())
                        //    {
                        //        WEIGHT = productionRow_data.WEIGHT.ToString();
                        //    }

                        //    if (!data.ContainsKey("WEIGHT"))
                        //        data.Add("WEIGHT", WEIGHT);



                        //    if (!data.ContainsKey("SSCC"))
                        //        data.Add("SSCC", SSCC_bez_nul);

                        //    //------------END-DATA----------------

                        //}


                        //if (!data.ContainsKey("GS1_KOD_1_1D"))
                        //    data.Add("GS1_KOD_1_1D", GS1_KOD_1_1D);

                        //if (!data.ContainsKey("GS1_KOD_1_TX"))
                        //    data.Add("GS1_KOD_1_TX", GS1_KOD_1_TX);

                        //if (!data.ContainsKey("GS1_KOD_2_1D"))
                        //    data.Add("GS1_KOD_2_1D", GS1_KOD_2_1D);

                        //if (!data.ContainsKey("GS1_KOD_2_TX"))
                        //    data.Add("GS1_KOD_2_TX", GS1_KOD_2_TX); 
                        #endregion

                        data.Add("SOURCE", "Konzola");

                        Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable dt = new Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable();


                        foreach (DataColumn dcol in dt.Columns)
                        {
                            string key = dcol.ColumnName;
                            string value = productionRow_data[dcol.ColumnName].ToString();
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
