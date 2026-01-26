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

namespace Konzola.IT_cast
{
    public partial class FormIT_cast_EventsArchivace : Form
    {


        #region Parametry

        protected Fask.Interfaces.IMES provider = null;
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
                            menuStrip2.Items.Remove(tsmiAkce);
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

        public Fask.Interfaces.DataSets.Vyroba.FASK_EventsRow SelectedRow
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
            get
            {
                List<Fask.Interfaces.DataSets.Vyroba.FASK_EventsRow> rows = new List<Fask.Interfaces.DataSets.Vyroba.FASK_EventsRow>();

                foreach (DataGridViewRow selectedRow in dg_OdvodEvents.SelectedRows)
                {
                    Fask.Interfaces.DataSets.Vyroba.FASK_EventsRow row = ((DataRowView)selectedRow.DataBoundItem).Row as Fask.Interfaces.DataSets.Vyroba.FASK_EventsRow;
                    rows.Add(row);
                }

                return rows;
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

        public FormIT_cast_EventsArchivace() 
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

        public FormIT_cast_EventsArchivace(bool allowMultiSelect, Fask.Interfaces.Classes.ZOBRAZENI_TYP typZobrazeni) : this()
        {
            
            this.dg_OdvodEvents.MultiSelect = allowMultiSelect;
            this.Zobrazeni = typZobrazeni;
            if (Zobrazeni == Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST)
            {
                panelButtonsZobrazeniList.Menu = menuStrip2;
                this.WindowState = FormWindowState.Maximized;
            }
            
        }

        private void FormOdvod_EventsList_Load(object sender, EventArgs e)
        {
            try
            {
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

                                //if (typeof(Fask.Interfaces.IT_cast.IIT_cast).IsAssignableFrom(t))
                                //{
                                //    provider = (Fask.Interfaces.IT_cast.IIT_cast)providerAssemlby.CreateInstance(t.FullName);
                                //    if (provider != null)
                                //        break;
                                //}

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
                dtp_OD.Checked = false;
                dtp_DO.Checked = false;
                chb_Zpravovane.Checked = false;
                chb_Nezpracovane.Checked = false;

                dtp_porizeno_OD.Checked = false;
                dtp_porizeno_DO.Checked = false;
                cb_Porizeno_TimeVariant.SelectedIndex = 0;
                cb_TimeVariant.SelectedIndex = 0;


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

                PerformAkce_Archivace();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                return;
            }

            PerformVyhledat();
        }

        void PerformAkce_Archivace()
        {
            try
            {
                int pocetArchZaznamu = 0;
                DateTime? OD = null;
                DateTime? DO = null;


                using (Form_DateTime_From_To_archivace frm = new Form_DateTime_From_To_archivace())
                {
                    frm.Popis = tsmiArchivace.Text;

                    DialogResult dr = frm.ShowDialog();

                    if (dr == System.Windows.Forms.DialogResult.OK)
                    {
                        OD = frm.OD;
                        DO = frm.DO;
                    }
                    else
                    {
                        return;
                    }
                }


                if ((provider != null) && (provider is Fask.Interfaces.IT_cast.IIT_cast_Events_Archivace))
                {
                    pocetArchZaznamu = ((Fask.Interfaces.IT_cast.IIT_cast_Events_Archivace)provider).ArchivaceProcedura(OD, DO);
                }
                else
                {
                    throw new NotImplementedException("Provider neobsahuje implemetaci IIT_cast_Events_Archivace ArchivaceProcedura");
                }

                    MessageBox.Show(this, "Akce dokončena. Pocet zaarchivovaných záznamů:"+ pocetArchZaznamu.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                return;
            }
            //finally
            //{
            //    ProgressIndicatorStop();
            //}
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

        private void archivaceVybraneToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
}
