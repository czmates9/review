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
using System.Text.RegularExpressions;
using System.IO;
using System.Data.SqlClient;
using MST_Print_Server_ZPL_Printing;

//using System;
//using System.Collections.Generic;
//using System.Data.SqlClient; // Závislost na SQL klientovi pro přístup k databázi
//using System.ComponentModel; // Nutné pro vytvoření vlastního enumu

namespace Konzola.Vyroba.Rozbory
{
    public partial class Form_MachineStateSetList_RozborOdvoduSSS : Form
    {

        #region FILTR dohledani proces ADAM
        // Enum pro popis hodnot
        public enum DescriptionEnum
        {
            // Přidejte požadované hodnoty z databáze
            Nevybráno
        }

        // Metoda pro načtení hodnot ze sloupce description v databázi a jejich přidání do ComboBoxu
        public static void LoadEnumFromDatabase(ComboBox comboBox)
        {

            try
            {


                // Připojení k databázi
                string connectionString = @"Data Source=192.168.1.121\SQLEXPRESS;Initial Catalog=Agro_fask;User ID=fask_dbowner;Password=pro147fask";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Otevření spojení s databází
                    connection.Open();

                    // Dotaz na získání unikátních hodnot ze sloupce description
                    string query = "SELECT DISTINCT description FROM MachinesDefinition";
                    SqlCommand command = new SqlCommand(query, connection);

                    // Vytvoření čteče pro zpracování výsledků dotazu
                    SqlDataReader reader = command.ExecuteReader();

                    // Přidání hodnot z databáze do ComboBoxu
                    while (reader.Read())
                    {
                        string description = reader.GetString(0); // Index 0 odpovídá sloupci description
                        comboBox.Items.Add(description);
                    }

                    // Uzavření čteče a spojení s databází
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                //MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }




        }



        public List<string> LoadDescriptionsFromDatabase()
        {
            List<string> descriptions = new List<string>();

            try
            {
                // Připojení k databázi
                string connectionString = @"Data Source=192.168.1.121\SQLEXPRESS;Initial Catalog=Agro_fask;User ID=fask_dbowner;Password=pro147fask";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Otevření spojení s databází
                    connection.Open();

                    // Dotaz na získání unikátních hodnot ze sloupce description
                    string query = "SELECT DISTINCT description FROM " + Fask.SQL.Constants.Common.TABLE_MachinesDefinition;
                    SqlCommand command = new SqlCommand(query, connection);

                    // Vytvoření čteče pro zpracování výsledků dotazu
                    SqlDataReader reader = command.ExecuteReader();

                    // Přidání hodnot z databáze do seznamu
                    while (reader.Read())
                    {
                        string description = reader.GetString(0); // Index 0 odpovídá sloupci description
                        descriptions.Add(description);
                    }

                    // Uzavření čteče a spojení s databází
                    reader.Close();
                }
            }
            catch (Exception ex)
            {

                Fask.Logging.ExceptionHandler2.Handle(ex);
            }

            return descriptions;
        }

        public void FillComboBoxFromDatabase(ComboBox comboBox)
        {

            comboBox.Items.Add("Nevybráno");
            comboBox.SelectedIndex = 0;

            // Zavolání metody pro načtení popisů z databáze
            List<string> descriptions = null;
            //List<string> descriptions = LoadDescriptionsFromDatabase();

            // nacteni dat v oddelenem vlakne
            if ((provider != null) && (provider is Fask.Interfaces.Vyroba.Odvod_MachineStateSet.IOdvod_MachineStateSet_GetEnum_description))
            {
                descriptions = ((Fask.Interfaces.Vyroba.Odvod_MachineStateSet.IOdvod_MachineStateSet_GetEnum_description)provider).LoadStrojNameFromDatabase();

                // Pokud seznam není null a není prázdný, přidáme jeho obsah do ComboBoxu
                if (descriptions != null && descriptions.Count > 0)
                {
                    // Přidání popisů do ComboBoxu
                    foreach (string description in descriptions)
                    {
                        comboBox.Items.Add(description);
                    }
                }

            }
            else
            {
                throw new NotImplementedException("Provider neobsahuje implemetaci IOdvod_MachineStateSet_GetEnum_description");
            }


        }



        #endregion


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
                    return dg_OdvodMachineStateSet.SelectedRows;
                }
                catch
                {
                    return null;
                }
            }
        }

        private List<Fask.Interfaces.DataSets.Vyroba.MachineStateSetHistory_Analyza_OdvoduRow> MachineStateSetHistory_Analyza_Odvodu_selectedRows
        {
            //get
            //{
            //    List<Fask.Interfaces.DataSets.Vyroba.MachineStateSetHistoryRow> rows = new List<Fask.Interfaces.DataSets.Vyroba.MachineStateSetHistoryRow>();

            //    foreach (DataGridViewRow selectedRow in dg_OdvodMachineStateSet.SelectedRows)
            //    {
            //        Fask.Interfaces.DataSets.Vyroba.MachineStateSetHistoryRow row = ((DataRowView)selectedRow.DataBoundItem).Row as Fask.Interfaces.DataSets.Vyroba.MachineStateSetHistoryRow;
            //        rows.Add(row);
            //    }

            //    return rows;
            //}

            get
            {
                return dg_OdvodMachineStateSet.SelectedRows
                    .Cast<DataGridViewRow>()
                    .OrderBy(r => r.Index)               // zarovnání na vizuální pořadí v gridu
                    .Select(r => ((DataRowView)r.DataBoundItem).Row as Fask.Interfaces.DataSets.Vyroba.MachineStateSetHistory_Analyza_OdvoduRow)
                    .Where(r => r != null)
                    .ToList();
            }
        }

        public Fask.Interfaces.DataSets.Vyroba.MachineStateSetHistory_Analyza_OdvoduRow MachineStateSetHistory_Analyza_Odvodu_selectedRow
        {
            get
            {
                try
                {
                    return ((DataRowView)(dg_OdvodMachineStateSet.BindingContext[bs_OdvodMachineStateSet].Current)).Row as Fask.Interfaces.DataSets.Vyroba.MachineStateSetHistory_Analyza_OdvoduRow;
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
        private List<Fask.Interfaces.Filtry.Odvod_MachineStateSetListFiltr> filtry = new List<Fask.Interfaces.Filtry.Odvod_MachineStateSetListFiltr>();

        /// <summary>
        /// Vybrany filtr.
        /// </summary>
        private Fask.Interfaces.Filtry.Odvod_MachineStateSetListFiltr rowFiltr
        {
            get
            {
                try
                {
                    return tscbFiltry.SelectedItem as Fask.Interfaces.Filtry.Odvod_MachineStateSetListFiltr;
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

        public Form_MachineStateSetList_RozborOdvoduSSS() 
        {
            InitializeComponent();

            this.dg_OdvodMachineStateSet.UpdateColumnHeaderCellsByDatasource();

            dtp_OD.Format = DateTimePickerFormat.Custom;
            dtp_OD.CustomFormat = "dd/MM/yyyy HH:mm:ss";

            dtp_DO.Format = DateTimePickerFormat.Custom;
            dtp_DO.CustomFormat = "dd/MM/yyyy HH:mm:ss";

        

        }

        public Form_MachineStateSetList_RozborOdvoduSSS(bool allowMultiSelect, Fask.Interfaces.Classes.ZOBRAZENI_TYP typZobrazeni) : this()
        {
            
            this.dg_OdvodMachineStateSet.MultiSelect = allowMultiSelect;
            this.Zobrazeni = typZobrazeni;
            if (Zobrazeni == Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST)
            {
                panelButtonsZobrazeniList.Menu = menuStrip2;
                this.WindowState = FormWindowState.Maximized;
            }
            
        }

        private void FormOdvod_MachineStateSetList_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                //this.WindowState = FormWindowState.Maximized;
                this.dg_OdvodMachineStateSet.LoadConfiguration(this.GetType().ToString());

                if (Zobrazeni == Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST)
                {
                    panelButtonsZobrazeniList.LoadConfiguration(this.GetType().ToString());
                    panelButtonsZobrazeniList.Init();
                    panelButtonsZobrazeniList.Size = new Size(85, 700); 
                }

                advancedDataGridViewSearchToolBar1.SetColumns(dg_OdvodMachineStateSet.Columns);

                //// načtení konfigurace vytvořených filtrů
                this.filtry = Extensions.ListExt.ReadFromXML<Fask.Interfaces.Filtry.Odvod_MachineStateSetListFiltr>(this.GetType().ToString() + ".filtr");
                tscbFiltry.ComboBox.DataSource = this.filtry;
                tscbFiltry.SelectedItem = null;
                tscbFiltry.ComboBox.DropDownWitdhAutosize();

                DateTime today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);

                dtp_OD.Value = today.AddDays(-1);
                dtp_DO.Value = today;

                dtp_OD.Checked = false;
                dtp_DO.Checked = false;

             
                cb_TimeVariant.Items.AddRange(Fask.Interfaces.Classes.TimeFilters.GetNumberNameRange());
                cb_TimeVariant.SelectedIndex = 0;




                //rB_aktualni.Checked = true;
                //rB_historie.Checked = false;


                InitProvider();


                #region FILTR dohledani proces ADAM

               // cB_description.Items.Add("Nevybráno");

                FillComboBoxFromDatabase(cB_description);

               // cB_description.SelectedIndex = 0;
                #endregion

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

        private void FormOdvod_MachineStateSetList_KeyDown(object sender, KeyEventArgs e)
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
            //else if ((e.Control && e.KeyCode == Keys.T))
            //{
            //    PrimyTiskZPL(); // Simuluje kliknutí na tlačítko
            //}
            else if ((e.Control && e.KeyCode == Keys.R))
            {
                PrimyTiskRDLC(); // Simuluje kliknutí na tlačítko
            }
            else
                return;

            e.Handled = true;
        }


        private void FormOdvod_MachineStateSetList_Shown(object sender, EventArgs e)
        {
            try
            {
                this.progressIndicator1.Size = new Size(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ostatni[0].ProgressIndicatorSize, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ostatni[0].ProgressIndicatorSize);
                this.progressIndicator1.Location = new Point(this.dg_OdvodMachineStateSet.Location.X + (this.dg_OdvodMachineStateSet.Width / 2) - (progressIndicator1.Size.Width / 2), this.dg_OdvodMachineStateSet.Location.Y + (this.dg_OdvodMachineStateSet.Height / 2) - (progressIndicator1.Size.Height / 2));
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

        public string GetDescriptionFromFilter()
        {

            string description = null;

            if (cB_description.SelectedIndex == 0)
            {
                description = null;
            }
            else
            {
                description = cB_description.SelectedItem as string;
            }

            return description;
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

                this.dg_OdvodMachineStateSet.SaveConfiguration(this.GetType().ToString());

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
                foreach (DataGridViewColumn column in dg_OdvodMachineStateSet.Columns)
                {
                    // Získání typu dat pro sloupec
                    Type dataType = null;

                    if (dg_OdvodMachineStateSet.DataSource is DataTable dataTable)
                    {
                        // Pokud je datový zdroj DataTable
                        if (dataTable.Columns.Contains(column.DataPropertyName))
                        {
                            dataType = dataTable.Columns[column.DataPropertyName].DataType;
                        }
                    }
                    else if (dg_OdvodMachineStateSet.DataSource is BindingSource bindingSource)
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
                                if (typeof(Fask.Interfaces.Vyroba.Odvod_MachineStateSet.IOdvod_MachineStateSet).IsAssignableFrom(t))
                                {
                                    provider = (Fask.Interfaces.Vyroba.Odvod_MachineStateSet.IOdvod_MachineStateSet)providerAssemlby.CreateInstance(t.FullName);
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
                if (bw_OdvodMachineStateSet.IsBusy)
                {
                    bw_OdvodMachineStateSet.CancelAsync();
                    while (bw_OdvodMachineStateSet.IsBusy)
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
                DataTable dtchanged = this.ds_OdvodMachineStateSet.MachineStateSetHistory_Analyza_Odvodu.GetChanges();
                if (dtchanged != null && dtchanged.Rows.Count > 0) //obsahuje zmeny ... 
                {
                    DialogResult dr = MessageBox.Show("Obsahuje změny, pokračovat?", "data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == System.Windows.Forms.DialogResult.No)
                        return;
                }

                if (bw_OdvodMachineStateSet.IsBusy)
                {
                    bw_OdvodMachineStateSet.CancelAsync();
                    while (bw_OdvodMachineStateSet.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }
                
                ProgressIndicatorStart();

                Fask.Interfaces.Filtry.Odvod_MachineStateSetListFiltr filtr = new Fask.Interfaces.Filtry.Odvod_MachineStateSetListFiltr();
                if (!CreateFilter(ref filtr))
                {
                    ProgressIndicatorStop();
                    return;
                }

                int FirstDisplayedScrollingRowIndex = this.dg_OdvodMachineStateSet.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

                bw_OdvodMachineStateSet.RunWorkerAsync(filtr);

                if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dg_OdvodMachineStateSet.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dg_OdvodMachineStateSet.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index
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

                Fask.Interfaces.Filtry.Odvod_MachineStateSetListFiltr filtr = new Fask.Interfaces.Filtry.Odvod_MachineStateSetListFiltr();
                string nazev = string.Empty;
                DialogResult dr = Forms.InputBox.Show("Název filtru", "Zadejte název filtru", string.Empty, false, out nazev);
                if (dr != System.Windows.Forms.DialogResult.OK)
                    return;

                string nameFile = Guid.NewGuid().ToString() + "_" + this.GetType().ToString();
                this.dg_OdvodMachineStateSet.SaveConfiguration(nameFile);

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

                Fask.Interfaces.Filtry.Odvod_MachineStateSetListFiltr filtr = rowFiltr;

                string nameFile = string.Empty;

                if (string.IsNullOrEmpty(filtr.NameFileDataGridView))
                {
                   nameFile = Guid.NewGuid().ToString() + "_" + this.GetType().ToString();
                }
                else
                    nameFile = filtr.NameFileDataGridView;

                this.dg_OdvodMachineStateSet.SaveConfiguration(nameFile);

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
                tb_CisloSluzby.Text = string.Empty;
                //tB_IP_ADAM.Text = string.Empty;
                dtp_OD.Checked = false;
                dtp_DO.Checked = false;

                cb_TimeVariant.SelectedIndex = 0;
                cB_description.SelectedIndex = 0;

                //rB_aktualni.Checked = true;
                //rB_historie.Checked = false;

                tB_S0.Text = string.Empty;
                tB_S1.Text = string.Empty;
                tB_S2.Text = string.Empty;
                tB_S3.Text = string.Empty;
                tB_S4.Text = string.Empty;
                tB_S5.Text = string.Empty;
                tB_S6.Text = string.Empty;
                tB_S7.Text = string.Empty;
                tB_S8.Text = string.Empty;
                tB_S9.Text = string.Empty;
                tB_S10.Text = string.Empty;
                tB_S11.Text = string.Empty;

                tB_Zdroj.Text = string.Empty;
                tB_StrojSklad.Text = string.Empty;
                tB_StrojLokace.Text = string.Empty;



                this.dg_OdvodMachineStateSet.LoadConfiguration(this.GetType().ToString());

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
        public void PerformNastavitFiltr(Fask.Interfaces.Filtry.Odvod_MachineStateSetListFiltr filtr) 
        {
            try
            {
                if (filtr == null)
                    return;

                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                PerformVycistitFiltr();

                //if (filtr.AdamIP != null)
                //{
                //    tB_IP_ADAM.Text = filtr.AdamIP;
                //}

                if (filtr.DateModified_OD != null)
                {
                    dtp_OD.Checked = true;
                    dtp_OD.Value = (DateTime)filtr.DateModified_OD;
                }

                if (filtr.DateModified_DO != null)
                {
                    dtp_DO.Checked = true;
                    dtp_DO.Value = (DateTime)filtr.DateModified_DO;
                }

                if (filtr.CisloSluzby != null)
                {
                    tb_CisloSluzby.Text = filtr.CisloSluzby;
                }

                //if(filtr.zaznam)
                //{
                //    rB_aktualni.Checked = filtr.zaznam;
                //}
                //else
                //{
                //    rB_historie.Checked = true;
                //}


                if (!string.IsNullOrEmpty(filtr.TimeVariant))
                {

                    var arr = filtr.TimeVariant.Split(';');
                    TimeFilters.TimeVariants TimeVar = (TimeFilters.TimeVariants)Enum.Parse(typeof(TimeFilters.TimeVariants), arr[0], true);

                    TimeVariantName time = new TimeVariantName(TimeVar, arr[1]);

                    cb_TimeVariant.SelectedItem = time;
                }

                if (!string.IsNullOrEmpty(filtr.Description))
                {


                    cB_description.SelectedItem = filtr.Description;
                }

                if (filtr.S0.HasValue)
                {
                    tB_S0.Text = filtr.S0.Value.ToString();
                }
                if (filtr.S1.HasValue)
                {
                    tB_S1.Text = filtr.S1.Value.ToString();
                }
                if (filtr.S2.HasValue)
                {
                    tB_S2.Text = filtr.S2.Value.ToString();
                }
                if (filtr.S3.HasValue)
                {
                    tB_S3.Text = filtr.S3.Value.ToString();
                }
                if (filtr.S4.HasValue)
                {
                    tB_S4.Text = filtr.S4.Value.ToString();
                }
                if (filtr.S5.HasValue)
                {
                    tB_S5.Text = filtr.S5.Value.ToString();
                }
                if (filtr.S6.HasValue)
                {
                    tB_S6.Text = filtr.S6.Value.ToString();
                }
                if (filtr.S7.HasValue)
                {
                    tB_S7.Text = filtr.S7.Value.ToString();
                }
                if (filtr.S8.HasValue)
                {
                    tB_S8.Text = filtr.S8.Value.ToString();
                }
                if (filtr.S9.HasValue)
                {
                    tB_S9.Text = filtr.S9.Value.ToString();
                }
                if (filtr.S10.HasValue)
                {
                    tB_S10.Text = filtr.S10.Value.ToString();
                }
                if (filtr.S11.HasValue)
                {
                    tB_S11.Text = filtr.S11.Value.ToString();
                }

                if (!string.IsNullOrEmpty(filtr.Zdroj))
                {


                    tB_Zdroj.Text = filtr.Zdroj;
                }
                if (!string.IsNullOrEmpty(filtr.StrojSklad))
                {


                    tB_StrojSklad.Text = filtr.StrojSklad;
                }
                if (!string.IsNullOrEmpty(filtr.StrojLokace))
                {


                    tB_StrojLokace.Text = filtr.StrojLokace;
                }

                if (string.IsNullOrEmpty(filtr.NameFileDataGridView))
                    this.dg_OdvodMachineStateSet.LoadConfiguration(this.GetType().ToString());
                else
                    this.dg_OdvodMachineStateSet.LoadConfiguration(filtr.NameFileDataGridView);


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
        private bool CreateFilter(ref Fask.Interfaces.Filtry.Odvod_MachineStateSetListFiltr filtr)
        {
            if (filtr == null)
                filtr = new Fask.Interfaces.Filtry.Odvod_MachineStateSetListFiltr();

            //if (!string.IsNullOrEmpty(tB_IP_ADAM.Text))
            //{
            //    Regex rx = new Regex("^(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$");
            //    MatchCollection matches = rx.Matches(tB_IP_ADAM.Text);

            //    if (matches.Count > 0)
            //        filtr.AdamIP = tB_IP_ADAM.Text.Trim();
            //    else
            //    {
            //        MessageBox.Show("Špatně zadaná IP adresa!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return false;
            //    }
            //}

            filtr.CisloSluzby = tb_CisloSluzby.Text;

            #region Time variant 

            var v = GetTimeVariantFromFilter();

            if (v.Contains("unknow"))
            {
                filtr.TimeVariant = null;
            }
            else
            {
                filtr.TimeVariant = v;
            }

            #endregion


            #region description

            string description_F = GetDescriptionFromFilter();

            if (string.IsNullOrEmpty(description_F))
            {
                filtr.Description = null;
            }
            else
            {
                filtr.Description = description_F;
            }

            #endregion



            //if (rB_aktualni.Checked)
            //{
            //    filtr.zaznam = true;
            //}
            //else
            //{
            //    filtr.zaznam = false;
            //}

            #region DateModified

            if (dtp_OD.Checked && dtp_OD.Enabled)
            {
                filtr.DateModified_OD = dtp_OD.Value;
            }
            else
                filtr.DateModified_OD = null;

            if (dtp_DO.Checked && dtp_DO.Enabled)
            {
                filtr.DateModified_DO = dtp_DO.Value;
            }
            else
                filtr.DateModified_DO = null;

            #endregion

            #region vstupy S0-S11
            for (int i = 0; i <= 11; i++)
            {
                TextBox tB = this.Controls.Find($"tB_S{i}", true).FirstOrDefault() as TextBox;
                if (tB != null && !string.IsNullOrEmpty(tB.Text))
                {
                    if (tB.Text == "0" || tB.Text == "1")
                    {
                        typeof(Fask.Interfaces.Filtry.Odvod_MachineStateSetListFiltr).GetProperty($"S{i}")?.SetValue(filtr, int.Parse(tB.Text));
                    }
                    else
                    {
                        MessageBox.Show($"Hodnota S{i} musí být (0 nebo 1)!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }

            #endregion


            filtr.Zdroj = tB_Zdroj.Text;
            filtr.StrojSklad = tB_StrojSklad.Text;
            filtr.StrojLokace = tB_StrojLokace.Text;

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
                this.progressIndicator1.Location = new Point(this.dg_OdvodMachineStateSet.Location.X + (this.dg_OdvodMachineStateSet.Width / 2) - (progressIndicator1.Size.Width / 2), this.dg_OdvodMachineStateSet.Location.Y + (this.dg_OdvodMachineStateSet.Height / 2) - (progressIndicator1.Size.Height / 2));
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

                this.dg_OdvodMachineStateSet.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                this.dg_OdvodMachineStateSet.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
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

                this.dg_OdvodMachineStateSet.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                dg_OdvodMachineStateSet.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
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

                dg_OdvodMachineStateSet.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                dg_OdvodMachineStateSet.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
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
                bool endcol = dg_OdvodMachineStateSet.CurrentCell.ColumnIndex + 1 >= dg_OdvodMachineStateSet.ColumnCount;
                bool endrow = dg_OdvodMachineStateSet.CurrentCell.RowIndex + 1 >= dg_OdvodMachineStateSet.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dg_OdvodMachineStateSet.CurrentCell.ColumnIndex;
                    startRow = dg_OdvodMachineStateSet.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dg_OdvodMachineStateSet.CurrentCell.ColumnIndex + 1;
                    startRow = dg_OdvodMachineStateSet.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dg_OdvodMachineStateSet.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dg_OdvodMachineStateSet.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dg_OdvodMachineStateSet.CurrentCell = c;
        }

        private void bw_OdvodEvents_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Fask.Interfaces.Filtry.Odvod_MachineStateSetListFiltr filtr = (Fask.Interfaces.Filtry.Odvod_MachineStateSetListFiltr)e.Argument;
                Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();

                if (bw_OdvodMachineStateSet.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                // nacteni dat v oddelenem vlakne
                if ((provider != null) && (provider is Fask.Interfaces.Vyroba.Odvod_MachineStateSet.IOdvod_MachineStateSet_GetFiltrovanyMachineStateSets))
                {
                    ds = ((Fask.Interfaces.Vyroba.Odvod_MachineStateSet.IOdvod_MachineStateSet_GetFiltrovanyMachineStateSets)provider).MachineStateSet_GetFiltrovanyOdvodMachineStateSet_Analyza_Odvodu(filtr);
                }
                else
                {
                    throw new NotImplementedException("Provider neobsahuje implemetaci IOdvod_MachineStateSet_GetFiltrovanyMachineStateSets");
                }

                if (bw_OdvodMachineStateSet.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                e.Result = ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
    ex.Message,
    "Chyba",
    MessageBoxButtons.OK,
    MessageBoxIcon.Error
);

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
                    ds_OdvodMachineStateSet = new Fask.Interfaces.DataSets.Vyroba();
                    bs_OdvodMachineStateSet.DataSource = ds_OdvodMachineStateSet;
                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, e.Error);
                    MessageBox.Show(e.Error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    SetStatusLabelText_Events(-1);
                }
                else if (e.Cancelled)
                {
                    // handle cancellation
                    // TODO: maji se vymazat data z datasetu?? asi ano ...
                    //if (dsSkladPohyb == null)
                    ds_OdvodMachineStateSet = new Fask.Interfaces.DataSets.Vyroba();
                    bs_OdvodMachineStateSet.DataSource = ds_OdvodMachineStateSet;

                    SetStatusLabelText_Events(-1);
                }
                else
                {
                    // uspesne dokonceno ...
                    // use it on the UI thread
                    ds_OdvodMachineStateSet = (Fask.Interfaces.DataSets.Vyroba)e.Result;
                    if (ds_OdvodMachineStateSet == null)
                        ds_OdvodMachineStateSet = new Fask.Interfaces.DataSets.Vyroba();

                    bs_OdvodMachineStateSet.DataSource = ds_OdvodMachineStateSet;

                    if (ds_OdvodMachineStateSet.MachineStateSetHistory_Analyza_Odvodu.Count == 0)
                        SetStatusLabelText_Events(-1);
                    else
                    {
                        foreach (DataGridViewRow row in dg_OdvodMachineStateSet.SelectedRows)
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
                   

                    Regex rx = new Regex("^(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$");
                    MatchCollection matches = rx.Matches(tb.Text);

                   if(matches.Count > 0)
                    tb.BackColor = Color.LightGreen;
                   else
                    tb.BackColor = Color.MistyRose;

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

            tssl_Eventu_Count.Text = string.Format("{0}/{1}", index + 1, ds_OdvodMachineStateSet.MachineStateSetHistory_Analyza_Odvodu.Count);
        }

        private void dg_OdvodMachineStateSet_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dg_OdvodMachineStateSet.SelectedRows)
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

                if (MachineStateSetHistory_Analyza_Odvodu_selectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if (MachineStateSetHistory_Analyza_Odvodu_selectedRows.Count > 1)
                {
                    //MessageBox.Show("Je vybráno více záznamů než jeden.", this.Text, MessageBoxButtons.OK);
                    //return;
                }


                Fask.Interfaces.DataSets.Vyroba.MachineStateSetHistory_Analyza_OdvoduDataTable dt = new Fask.Interfaces.DataSets.Vyroba.MachineStateSetHistory_Analyza_OdvoduDataTable();

                foreach (Fask.Interfaces.DataSets.Vyroba.MachineStateSetHistory_Analyza_OdvoduRow row in MachineStateSetHistory_Analyza_Odvodu_selectedRows)
                {
                    try
                    {
                        Fask.Interfaces.DataSets.Vyroba.MachineStateSetHistory_Analyza_OdvoduRow newRow = dt.NewMachineStateSetHistory_Analyza_OdvoduRow();
                        // Ošetřit delší vstupní pole než počet sloupců tabulky
                        int copyLength = Math.Min(row.ItemArray.Length, dt.Columns.Count);
                        newRow.ItemArray = row.ItemArray.Take(copyLength).ToArray();
                        dt.AddMachineStateSetHistory_Analyza_OdvoduRow(newRow);
                    }
                    catch (Exception ex)
                    {
                        // Ošetření vyjimky
                        // Můžete zde provést logování chyby nebo jiné požadované akce
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                    }
                }

                // Použití datové tabulky 'dt' dále dle potřeby

                //Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable dt = (Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable)ds.CZPRO_VPP.Copy();



                foreach (Fask.Interfaces.DataSets.Vyroba.MachineStateSetHistory_Analyza_OdvoduRow item in dt)
                {

                    //neco
                   


                }

                PrintReport(dt);

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        }


        private void PrintReport(Fask.Interfaces.DataSets.Vyroba.MachineStateSetHistory_Analyza_OdvoduDataTable dt)
        {
            try
            {


                foreach (var item in dt)
                {
                    Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start prehled stavy stroju TISK rdlc-------------------------------------");

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

                    Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END prehled stavy stroju TISK rdlc-------------------------------------");
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
                    FormOdvod_TiskoveSablony form = new FormOdvod_TiskoveSablony(this.Text, FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Admin(), Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Tisk_Klic_RDLC, false);


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

                if (MachineStateSetHistory_Analyza_Odvodu_selectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                #region MaR 6.11.2024 vypis zpl do souboru selected rows
                int index_zaznamu = 0;

                foreach (var item in MachineStateSetHistory_Analyza_Odvodu_selectedRows)
                {

                    if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start vyroba/rozbory/Analyza_SSS TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");

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

                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END vyroba/rozbory/Analyza_SSS TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");


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



                    foreach (var item in MachineStateSetHistory_Analyza_Odvodu_selectedRows)
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
                    Tisk_selected_rows_Primy_Tisk(plr_Path, true, MN_ToTisk, MachineStateSetHistory_Analyza_Odvodu_selectedRows, NazevVychoziTiskarny);

                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Tisk_selected_rows_Primy_Tisk(string plr_Path, bool MnozstvuAutoJedna, int MN_ToTisk, List<Fask.Interfaces.DataSets.Vyroba.MachineStateSetHistory_Analyza_OdvoduRow> selected_Rows, string nazevVychTiskarny)
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

                if (MachineStateSetHistory_Analyza_Odvodu_selectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                //if (Production_rowProducts.Count > 1)
                //{
                //    //MessageBox.Show("Je vybráno více záznamů než jeden.", this.Text, MessageBoxButtons.OK);
                //    //return;
                //}

                Fask.Interfaces.DataSets.Vyroba.MachineStateSetHistory_Analyza_OdvoduDataTable dt = new Fask.Interfaces.DataSets.Vyroba.MachineStateSetHistory_Analyza_OdvoduDataTable();

                foreach (Fask.Interfaces.DataSets.Vyroba.MachineStateSetHistory_Analyza_OdvoduRow row in MachineStateSetHistory_Analyza_Odvodu_selectedRows)
                {
                    try
                    {
                        Fask.Interfaces.DataSets.Vyroba.MachineStateSetHistory_Analyza_OdvoduRow newRow = dt.NewMachineStateSetHistory_Analyza_OdvoduRow();
                        // Ošetřit delší vstupní pole než počet sloupců tabulky
                        int copyLength = Math.Min(row.ItemArray.Length, dt.Columns.Count);
                        newRow.ItemArray = row.ItemArray.Take(copyLength).ToArray();
                        dt.AddMachineStateSetHistory_Analyza_OdvoduRow(newRow);
                    }
                    catch (Exception ex)
                    {
                        // Ošetření vyjimky
                        // Můžete zde provést logování chyby nebo jiné požadované akce
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                    }
                }


                //dt.Columns.Add("Barcode_IMG", typeof(string));
                //foreach (Fask.Interfaces.DataSets.Vyroba.MachineStateSetHistory_Analyza_OdvoduRow row in dt)
                //{

                //    try
                //    {
                //        if (!row.IsBarcodeNull() && !string.IsNullOrEmpty(row.Barcode))
                //        {
                //            ZXing.BarcodeWriter zw = new ZXing.BarcodeWriter();
                //            zw.Format = ZXing.BarcodeFormat.CODE_128;
                //            zw.Options.Height = 50; //50
                //            zw.Options.PureBarcode = true;
                //            System.Drawing.Bitmap image1 = zw.Write(row.Barcode);

                //            System.IO.MemoryStream ms = new System.IO.MemoryStream();
                //            image1.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                //            byte[] imgReportBarcode = ms.ToArray();
                //            ms.Close();

                //            string Base64Imahe = Convert.ToBase64String(imgReportBarcode);

                //            row["Barcode_IMG"] = Base64Imahe;
                //        }


                //    }
                //    catch (Exception ex)
                //    {

                //        Fask.Logging.ExceptionHandler2.Handle(ex);
                //    }


                //}

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

        private void PrintReport_RDLC(Fask.Interfaces.DataSets.Vyroba.MachineStateSetHistory_Analyza_OdvoduDataTable dt, bool primyTisk, string path_sablona, string nazevVychTiskarny, int pocetVytisku)
        {
            try
            {

                #region logovani tisku
                if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                {
                    foreach (var item in dt)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start vyroba/rozbory/Analyza_SSS TISK rdlc-------------------------------------");

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

                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END vyroba/rozbory/Analyza_SSS TISK rdlc-------------------------------------");
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

                    //try
                    //{
                    //    for (int i = 0; i < pocetVytisku; i++)
                    //    {
                    //        plr.Print(this);
                    //    }
                    //}
                    //catch (Exception ex)
                    //{

                    //    //--tady chci logovat uplne vse!!
                    //    MessageBox.Show("Chyba při pokusu o tisk.");
                    //    // Ošetření výjimky při tisku
                    //    Fask.Logging.ExceptionHandler2.Handle(ex);

                    //    // Volání metody PrintReport znovu (rekurze)
                    //    //PrintReport(dt, primyTisk, path_sablona, nazevVychTiskarny, pocetVytisku);
                    //}

                    if (!File.Exists(plr.Path))
                    {
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.Error,
                            $"Analyza_SSS - RDLC soubor NEEXISTUJE na cestě: {plr.Path}");
                    }
                    else
                    {
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo,
                            $"Analyza_SSS - RDLC soubor nalezen na cestě: {plr.Path}");
                    }


                    try
                    {
                        for (int i = 0; i < pocetVytisku; i++)
                        {
                            plr.Print(this);
                        }
                    }
                    catch (Exception ex)
                    {
                        // tady loguju ÚPLNĚ VŠE, co dává smysl
                        var sb = new StringBuilder();

                        sb.AppendLine("==================================================");
                        sb.AppendLine("CHYBA PŘI TISKU RDLC");
                        sb.AppendLine("Metoda: PrintReport_RDLC");
                        sb.AppendLine($"Čas: {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}");
                        sb.AppendLine("--------------------------------------------------");
                        sb.AppendLine("Parametry volání:");
                        sb.AppendLine($"primyTisk: {primyTisk}");
                        sb.AppendLine($"path_sablona: {path_sablona}");
                        sb.AppendLine($"nazevVychTiskarny: {nazevVychTiskarny}");
                        sb.AppendLine($"pocetVytisku: {pocetVytisku}");
                        sb.AppendLine("--------------------------------------------------");
                        sb.AppendLine("Stav objektu plr (CoolPrintPreviewDialog):");
                        sb.AppendLine($"plr.PrinterName: {plr.PrinterName}");
                        sb.AppendLine($"plr.ShowPreview: {plr.ShowPreview}");
                        sb.AppendLine($"plr.Path: {plr.Path}");
                        sb.AppendLine($"plr.NazevDataTable: {plr.NazevDataTable}");
                        sb.AppendLine($"plr.Projekt: {plr.Projekt}");
                        sb.AppendLine("--------------------------------------------------");
                        sb.AppendLine("Parametry reportu (ReportParameter[] Params):");

                        if (plr.Params != null)
                        {
                            foreach (var p in plr.Params)
                            {
                                try
                                {
                                    // Values může být pole – beru první hodnotu
                                    var value = (p.Values != null && p.Values.Count > 0)
                                        ? p.Values[0]
                                        : string.Empty;

                                    sb.AppendLine($"  {p.Name}: {value}");
                                }
                                catch (Exception exParam)
                                {
                                    sb.AppendLine($"  {p?.Name ?? "<null>"}: CHYBA ČTENÍ PARAMETRU – {exParam.Message}");

                                }
                            }
                        }
                        else
                        {
                            sb.AppendLine("  <Params == null>");
                        }

                        sb.AppendLine("--------------------------------------------------");
                        sb.AppendLine("Informace o DataTable (dt):");
                        try
                        {
                            sb.AppendLine($"dt.Rows.Count: {dt?.Count ?? 0}");

                            // možnost lognout první řádek – opatrně, aby log nebyl gigantický
                            if (dt != null && dt.Count > 0)
                            {
                                var firstRow = dt[0];
                                sb.AppendLine("První řádek dt:");

                                foreach (var property in firstRow.GetType().GetProperties())
                                {
                                    object propertyValue = null;
                                    try
                                    {
                                        propertyValue = property.GetValue(firstRow);
                                    }
                                    catch (Exception exProp)
                                    {
                                        propertyValue = $"<CHYBA ČTENÍ: {exProp.Message}>";
                                    }

                                    sb.AppendLine($"  {property.Name}: {propertyValue}");
                                }
                            }
                        }
                        catch (Exception exDt)
                        {
                            sb.AppendLine($"CHYBA PŘI LOGOVÁNÍ dt: {exDt.Message}");
                        }

                        sb.AppendLine("--------------------------------------------------");
                        sb.AppendLine("Detail výjimky:");
                        sb.AppendLine($"Typ: {ex.GetType().FullName}");
                        sb.AppendLine($"Message: {ex.Message}");
                        sb.AppendLine($"HResult: {ex.HResult}");
                        sb.AppendLine($"TargetSite: {ex.TargetSite}");
                        sb.AppendLine("StackTrace:");
                        sb.AppendLine(ex.StackTrace);

                        if (ex.InnerException != null)
                        {
                            sb.AppendLine("--------------- INNER EXCEPTION ------------------");
                            sb.AppendLine($"Typ: {ex.InnerException.GetType().FullName}");
                            sb.AppendLine($"Message: {ex.InnerException.Message}");
                            sb.AppendLine("StackTrace:");
                            sb.AppendLine(ex.InnerException.StackTrace);
                        }

                        sb.AppendLine("==================================================");

                        // 1) Log textový dump všech informací
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.Error, sb.ToString());

                        // 2) Log původní výjimku (pokud máš takový overload)
                        Fask.Logging.ExceptionHandler2.Handle(ex);

                        // Informace pro uživatele
                        MessageBox.Show("Chyba při pokusu o tisk. Podrobnosti byly zalogovány.",
                                        "Chyba tisku",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);

                        // případně NEvolat rekurzi, aby se nevytvořila smyčka
                        // PrintReport_RDLC(dt, primyTisk, path_sablona, nazevVychTiskarny, pocetVytisku);
                    }

                }
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw;
            }
        }

        private void Perform_Tisk_Selected(string klicTyp, string NazevVychoziTiskarny, string pocetVytisku)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (MachineStateSetHistory_Analyza_Odvodu_selectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                #region MaR 6.11.2024 vypis zpl do souboru selected rows
                int index_zaznamu = 0;

                foreach (var item in MachineStateSetHistory_Analyza_Odvodu_selectedRows)
                {

                    if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start vyroba/rozbory/Analyza_SSS TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");

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

                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END vyroba/rozbory/Analyza_SSS TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");


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



                    foreach (var item in MachineStateSetHistory_Analyza_Odvodu_selectedRows)
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
                    Tisk_selected_rows(plr_Path, true, MN_ToTisk, MachineStateSetHistory_Analyza_Odvodu_selectedRows);

                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Tisk_selected_rows(string plr_Path, bool MnozstvuAutoJedna, int MN_ToTisk, List<Fask.Interfaces.DataSets.Vyroba.MachineStateSetHistory_Analyza_OdvoduRow> selected_Rows)
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
            Fask.Interfaces.DataSets.Vyroba.MachineStateSetHistory_Analyza_OdvoduRow row,
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

                Fask.Interfaces.DataSets.SkladLokace.CZMST094DataTable dt = new Fask.Interfaces.DataSets.SkladLokace.CZMST094DataTable();

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

        private Dictionary<string, string> PrepareDataToTisk(Fask.Interfaces.DataSets.Vyroba.MachineStateSetHistory_Analyza_OdvoduRow row_data)
        {
            try
            {
                while (true)
                {
                    try
                    {
                        Dictionary<string, string> data = new Dictionary<string, string>();


                        data.Add("SOURCE", "Konzola");

                        Fask.Interfaces.DataSets.Vyroba.MachineStateSetHistory_Analyza_OdvoduDataTable dt = new Fask.Interfaces.DataSets.Vyroba.MachineStateSetHistory_Analyza_OdvoduDataTable();


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
