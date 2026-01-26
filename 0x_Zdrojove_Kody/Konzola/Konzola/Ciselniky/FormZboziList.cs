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
using Konzola.Vyroba.Rozbory;
using System.IO;
using JR.Utils.GUI.Forms;
using Konzola.Forms;
using MST_Print_Server_ZPL_Printing;

namespace Konzola.Ciselniky
{
    public partial class FormZboziList : Form
    {




        #region private promenne

        private bool opravneniEditace = false;
        private bool opravneniImport = false;
        private bool opravneniArchivace = false;

        #endregion

        #region Parametry

        private Fask.Interfaces.IMES providerZbozi = null;
        private Fask.Interfaces.Ciselniky.Sklady.ISklady2 providerSklady = null;
        private Fask.Interfaces.IMES providerTisk = null; //**DONE

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
                            menuStrip2.Items.Remove(tsmiExporty);
                            menuStrip2.Items.Remove(tsmiPolozka);
                            menuStrip2.Items.Remove(tsmiAkce);
                            tsFiltry.Visible = true; // Z jakeho duvodu to bylo false? snad na to nenarazim :D 
                            tsFiltry.Enabled = true; // Z jakeho duvodu to bylo false? snad na to nenarazim :D 
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
        private List<Fask.Interfaces.Filtry.ZboziListFiltr> filtry = new List<Fask.Interfaces.Filtry.ZboziListFiltr>();

        /// <summary>
        /// Vybrany filtr.
        /// </summary>
        private Fask.Interfaces.Filtry.ZboziListFiltr rowFiltr
        {
            get
            {
                try
                {
                    return tscbFiltry.SelectedItem as Fask.Interfaces.Filtry.ZboziListFiltr;
                }
                catch
                {
                    return null;
                }
            }
        }

        //public Fask.Interfaces.DataSets.Konzola.FASK_LoginsRow loginrow { get; set; }

        /// <summary>
        /// Zvolené id před seřazením
        /// </summary>
        string selectedSortID = string.Empty;

        /// <summary>
        /// Radek, ktery se predvoli (pri editaci zaznamu)
        /// </summary>
        private Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_ALL_KONZOLARow selectRow { get; set; }

        /// <summary>
        /// Vybrana cinnost.
        /// </summary>
        public Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_ALL_KONZOLARow FASK_ZASOBY_selectedRow
        {
            get
            {
                try
                {
                    return ((DataRowView)(dg_Zbozi.BindingContext[bsZbozi].Current)).Row as Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_ALL_KONZOLARow;
                }
                catch
                {
                    return null;
                }
            }
        }

        private List<Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_ALL_KONZOLARow> FASK_ZASOBY_selectedRows
        {
            //get
            //{
            //    List<Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_ALL_KONZOLARow> rows = new List<Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_ALL_KONZOLARow>();

            //    foreach (DataGridViewRow selectedRow in dg_Zbozi.SelectedRows)
            //    {
            //        Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_ALL_KONZOLARow row = ((DataRowView)selectedRow.DataBoundItem).Row as Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_ALL_KONZOLARow;
            //        rows.Add(row);
            //    }

            //    return rows;
            //}

            get
            {
                return dg_Zbozi.SelectedRows
                    .Cast<DataGridViewRow>()
                    .OrderBy(r => r.Index)               // zarovnání na vizuální pořadí v gridu
                    .Select(r => ((DataRowView)r.DataBoundItem).Row as Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_ALL_KONZOLARow)
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
                    return dg_Zbozi.SelectedRows;
                }
                catch
                {
                    return null;
                }
            }
        }

        private Fask.Interfaces.DataSets.Sklady.CZMST093Row rowSklad
        {
            get
            {
                try
                {
                    if (cbSklad.SelectedItem is Fask.Interfaces.DataSets.Sklady.CZMST093Row)
                        return cbSklad.SelectedItem as Fask.Interfaces.DataSets.Sklady.CZMST093Row;
                    else
                        return null;
                }
                catch
                {
                    return null;
                }
            }
        }


        #region Vlastnosti pro VPPEdit

        private bool _vyhledatVLoad = false;
        public bool VyhledatVLoad
        {
            set { _vyhledatVLoad = value; }
            get { return _vyhledatVLoad; }
        }


        public string Nastav_Filtr_ITEMCODE
        {
            set { cbMaterialITEMCODE.Text = value; }
        }

        public string Nastav_Filtr_ITEMDESC
        {
            set { cbMaterialOznaceni.Text = value; }
        }

        public string Nastav_Filtr_ITEMNMBR
        {
            set { cbMaterialITEMNMBR.Text = value; }
        }

        #endregion
        #endregion

        #region Eventy formu

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="allowMultiSelect"></param>
        /// <param name="typZobrazeni"></param>
        public FormZboziList(bool allowMultiSelect, Fask.Interfaces.Classes.ZOBRAZENI_TYP typZobrazeni)
        {
            InitializeComponent();

            this.dg_Zbozi.UpdateColumnHeaderCellsByDatasource();

            this.dg_Zbozi.MultiSelect = allowMultiSelect;
            this.Zobrazeni = typZobrazeni;
            if (Zobrazeni == Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST)
            {
                this.WindowState = FormWindowState.Maximized;
                panelButtonsZobrazeniList.Menu = menuStrip2;
            }
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="allowMultiSelect"></param>
        /// <param name="typZobrazeni"></param>
        public FormZboziList(bool allowMultiSelect, Fask.Interfaces.Classes.ZOBRAZENI_TYP typZobrazeni, bool opravneniEditace, bool opravneniImport)
        {
            InitializeComponent();

            this.opravneniEditace = opravneniEditace;
            this.opravneniImport = opravneniImport;
            this.dg_Zbozi.UpdateColumnHeaderCellsByDatasource();

            this.dg_Zbozi.MultiSelect = allowMultiSelect;
            this.Zobrazeni = typZobrazeni;
            if (Zobrazeni == Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST)
            {
                this.WindowState = FormWindowState.Maximized;
                panelButtonsZobrazeniList.Menu = menuStrip2;
            }
        }

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


        public FormZboziList(bool allowMultiSelect, Fask.Interfaces.Classes.ZOBRAZENI_TYP typZobrazeni, Opravneni opravneni)
        {
            InitializeComponent();

            //this.opravneniEditace = opravneniEditace;
            //this.opravneniImport = opravneniImport;

            this.dg_Zbozi.UpdateColumnHeaderCellsByDatasource();

            this.dg_Zbozi.MultiSelect = allowMultiSelect;
            this.Zobrazeni = typZobrazeni;
            if (Zobrazeni == Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST)
            {
                this.WindowState = FormWindowState.Maximized;
                panelButtonsZobrazeniList.Menu = menuStrip2;
            }

            // Uloží oprávnění pro další použití v programu
            this.opravneni = opravneni;

            // Nastavení podle oprávnění
            SetOpravneni();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="allowMultiSelect"></param>
        /// <param name="typZobrazeni"></param>
        public FormZboziList(bool allowMultiSelect, Fask.Interfaces.Classes.ZOBRAZENI_TYP typZobrazeni, bool opravneniEditace)
        {
            InitializeComponent();

            this.dg_Zbozi.UpdateColumnHeaderCellsByDatasource();
            this.opravneniEditace = opravneniEditace;

            this.dg_Zbozi.MultiSelect = allowMultiSelect;
            this.Zobrazeni = typZobrazeni;
            if (Zobrazeni == Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST)
            {
                this.WindowState = FormWindowState.Maximized;
                panelButtonsZobrazeniList.Menu = menuStrip2;
            }
        }
        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="allowMultiSelect">Povoleni oznaceni vice zaznamu.</param>
        /// <param name="selectRow">Radek, ktery se oznaci.</param>
        public FormZboziList(bool allowMultiSelect, Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_ALL_KONZOLARow selectRow, Fask.Interfaces.Classes.ZOBRAZENI_TYP typZobrazeni)
            : this(allowMultiSelect, typZobrazeni)
        {
            this.selectRow = selectRow;
        }


        private void FormZboziList_Load(object sender, EventArgs e)
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
                    tsmiExporty.DropDownItems.Remove(tiskEtiketToolStripMenuItem);
                }

                this.ShowIcon = false;
                //this.WindowState = FormWindowState.Maximized;
                this.dg_Zbozi.LoadConfiguration(this.GetType().ToString());


                if (Zobrazeni == Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST)
                {
                    panelButtonsZobrazeniList.LoadConfiguration(this.GetType().ToString());
                    panelButtonsZobrazeniList.Init();
                    panelButtonsZobrazeniList.Size = new Size(85, 700);
                }

                advancedDataGridViewSearchToolBar1.SetColumns(dg_Zbozi.Columns);

                //// načtení konfigurace vytvořených filtrů
                this.filtry = Extensions.ListExt.ReadFromXML<Fask.Interfaces.Filtry.ZboziListFiltr>(this.GetType().ToString() + ".filtr");
                tscbFiltry.ComboBox.DataSource = this.filtry;
                tscbFiltry.SelectedItem = null;
                tscbFiltry.ComboBox.DropDownWitdhAutosize();


                // inicializace provider
                InitProvider();

                if (providerZbozi == null)
                    throw new Exception("Provider 'Zboží' není inicializován");

                if (providerSklady == null)
                    throw new Exception("Provider 'Sklady' není inicializován");

                Fask.Interfaces.DataSets.Sklady dtSklady = null;

                if ((providerSklady != null) && (providerSklady is Fask.Interfaces.Ciselniky.Sklady.ISklady2_GetSklady))
                {
                    dtSklady = ((Fask.Interfaces.Ciselniky.Sklady.ISklady2_GetSklady)providerSklady).GetSklady();
                }
                else { throw new NotImplementedException("Provider neobsahuje implemetaci ISklady2_GetSklady"); }
                

                cbSklad.Items.AddRange(dtSklady.CZMST093.Select());
                cbSklad.SelectedItem = null;

                cbTypMaterialu.DataSource = Enum.GetValues(typeof(Fask.Interfaces.Classes.TypPolozky));

                SetStratusLabelText_Zbozi(-1);



                ////MaR 16.92024 zde zobrazovat tlacitka nebo jejich disable
                //if(opravneniEditace)
                //{
                //    //disable tlacitka editovat a odstranit
                //    tsmiNovy.Enabled = true;
                //    tsmiUpravit.Enabled = true;
                //    tsmiOdstranit.Enabled = true;
                //}
                //else
                //{
                //    //disable tlacitka editovat a odstranit
                //    tsmiNovy.Enabled = false;
                //    tsmiUpravit.Enabled = false;
                //    tsmiOdstranit.Enabled = false;

                //    menuStrip2.Items.Remove(tsmiNovy);
                //    menuStrip2.Items.Remove(tsmiUpravit);
                //    menuStrip2.Items.Remove(tsmiOdstranit);

                //    //tsmiNovy.Visible = false;
                //    //tsmiUpravit.Visible = false;
                //    //tsmiOdstranit.Visible = false;
                //}

                //if (opravneniImport)
                //{

                //    tsmiImportovatZbozi.Enabled = true;
                //    tsmiImportovatZbozi.Visible = true;
                //}
                //else
                //{
                //    //disable tlacitka importovat
                //    tsmiImportovatZbozi.Enabled = false;
                //    menuStrip2.Items.Remove(tsmiImportovatZbozi);

                //    // tsmiImportovatZbozi.Visible = false;
                //}


              


                // 13.7.2016 PeV: jiz se nepouziva, predelano na backgroundworker
                //System.Threading.Thread loadThread = new System.Threading.Thread(LoadDataAsync);
                //loadThread.IsBackground = true;
                //loadThread.Start();
                //PerformVyhledat();
                btn_Vyhledat.Focus();

                if (_vyhledatVLoad)
                {
                    PerformVyhledat();
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormZboziList_KeyDown(object sender, KeyEventArgs e)
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



        private void FormZboziList_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.dg_Zbozi.SaveConfiguration(this.GetType().ToString());

                if (Zobrazeni == Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST)
                {
                    panelButtonsZobrazeniList.SaveConfiguration(this.GetType().ToString());
                }

                // ulozeni vsech filtru
                this.filtry.WriteXML(this.GetType().ToString() + ".filtr");

                // cekani na dobehnuti vlakna
                try
                {
                    if (bwLoadZbozi.IsBusy)
                    {
                        bwLoadZbozi.CancelAsync();
                        while (bwLoadZbozi.IsBusy)
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

        private void FormZboziList_Shown(object sender, EventArgs e)
        {
            try
            {
                this.progressIndicator1.Size = new Size(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ostatni[0].ProgressIndicatorSize, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ostatni[0].ProgressIndicatorSize);
                this.progressIndicator1.Location = new Point(this.dg_Zbozi.Location.X + (this.dg_Zbozi.Width / 2) - (progressIndicator1.Size.Width / 2), this.dg_Zbozi.Location.Y + (this.dg_Zbozi.Height / 2) - (progressIndicator1.Size.Height / 2));
            }
            catch { }
        }

        #endregion

        #region Inicializace Providera
        
        /// <summary>
        /// Inicializace providera
        /// </summary>
        private void InitProvider()
        {
            try
            {
                if (!String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                {
                    if (providerZbozi == null) //inicializace se provede pouze pokud nebyla provedena ... 
                    {
                        //Assembly providerAssemlby = Assembly.LoadFrom(Server.MapPath(providerAssemblyPath));
                        Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                        Type[] types = providerAssemlby.GetTypes();
                        foreach (Type t in types)
                        {
                            try
                            {
                                //t.IsAssignableFrom(typeof(Fask.Server.Interfaces.Vydej.IVydej);
                                if (typeof(Fask.Interfaces.Ciselniky.Zbozi.IZbozi2).IsAssignableFrom(t))
                                {
                                    providerZbozi = (Fask.Interfaces.Ciselniky.Zbozi.IZbozi2)providerAssemlby.CreateInstance(t.FullName);
                                    if (providerZbozi != null)
                                        break;
                                }
                            }
                            catch { }
                        }
                        //return config;
                    }

                    providerZbozi.InitProvider();

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
                        //Assembly providerAssemlby = Assembly.LoadFrom(Server.MapPath(providerAssemblyPath));
                        Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                        Type[] types = providerAssemlby.GetTypes();
                        foreach (Type t in types)
                        {
                            try
                            {
                                //t.IsAssignableFrom(typeof(Fask.Server.Interfaces.Vydej.IVydej);
                                if (typeof(Fask.Interfaces.Ciselniky.Zbozi.IZbozi2).IsAssignableFrom(t))
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

        #endregion

        #region Click Eventy

        private void buttonNovy_Click(object sender, EventArgs e)
        {
            PerformOK();
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

        private void buttonVyhledat_Click(object sender, EventArgs e)
        {
            #region prace s datagridem - nastaveni numeric hodnot na pocet desetinnych mist
            try
            {
                // Procházení sloupců v DataGridView
                foreach (DataGridViewColumn column in dg_Zbozi.Columns)
                {
                    // Získání typu dat pro sloupec
                    Type dataType = null;

                    if (dg_Zbozi.DataSource is DataTable dataTable)
                    {
                        // Pokud je datový zdroj DataTable
                        if (dataTable.Columns.Contains(column.DataPropertyName))
                        {
                            dataType = dataTable.Columns[column.DataPropertyName].DataType;
                        }
                    }
                    else if (dg_Zbozi.DataSource is BindingSource bindingSource)
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

        private void tsmiImportovatZbozi_Click(object sender, EventArgs e)
        {

            if (!opravneniImport)
            {
                MessageBox.Show("Nemáte dostatečné oprávnění pro tuto akci!", "Oprávnění", MessageBoxButtons.OK);
                return;
            }


            bool uspech = false;
            try
            {

                // Create an instance of FormImport
                using (FormImport form = new FormImport("Import zboží", "Procedura", "Soubor CSV"))
                {
                    // Show the form as a dialog and check if the user clicked OK
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        // Retrieve the selected option from the form
                        string selectedOption = form.SelectedOption;

                        // Do something with the selected option
                       // MessageBox.Show("You selected: " + selectedOption);


                        if (selectedOption == "CSV")
                        {




                            List<Tuple<string, string, bool>> tableInfo = new List<Tuple<string, string, bool>>();


                            // nacteni dat v oddelenem vlakne
                            if ((providerZbozi != null) && (providerZbozi is Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_ImportZbozi))
                                tableInfo = ((Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_ImportZbozi)providerZbozi).GetZasobyTableInfo();
                            else
                                throw new NotImplementedException("Provider neimplementuje IZbozi2_ImportZbozi.");


                            //logika importu zbozi z CSV
                            string pathToFile = string.Empty;
                            uspech = ImportZboziZFileCSV(tableInfo);

                            //DialogResult dr = MessageBox.Show("Obsahuje změny, chcete refresh?", "data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            //if (dr == System.Windows.Forms.DialogResult.No)
                            //    return;


                            //PerformVyhledat();


                        }
                        else if (selectedOption == "SQL_Procedura")
                        {
                            DataTable dtchanged = this.dsZbozi.FASK_ZASOBY_ALL_KONZOLA.GetChanges();
                            if (dtchanged != null && dtchanged.Rows.Count > 0) //obsahuje zmeny ... 
                            {
                                DialogResult dr = MessageBox.Show("Obsahuje změny, pokračovat?", "data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                if (dr == System.Windows.Forms.DialogResult.No)
                                    return;
                            }

                            if (bwImportZbozi.IsBusy)
                            {
                                bwImportZbozi.CancelAsync();
                                while (bwImportZbozi.IsBusy)
                                {
                                    Application.DoEvents();
                                }
                            }

                            ProgressIndicatorStart();

                            int FirstDisplayedScrollingRowIndex = this.dg_Zbozi.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

                            bwImportZbozi.RunWorkerAsync();

                            if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dg_Zbozi.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dg_Zbozi.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index

                        }
                        else
                        {

                            //nic se neprovede
                            return;
                        }



                    }
                    else
                    {
                        //nevybrano nic
                        return;
                    }
                }



                #region MaR odkomentovan vyber importu 21.8.2024
                //DialogResult dr_import = MessageBox.Show("Importovat zboží ze souboru? Volba NE importuje zboží procedurou SQL!", "Import dat", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                //if (dr_import == System.Windows.Forms.DialogResult.Yes)
                //{




                //    List<Tuple<string, string, bool>> tableInfo = new List<Tuple<string, string, bool>>();


                //    // nacteni dat v oddelenem vlakne
                //    if ((providerZbozi != null) && (providerZbozi is Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_ImportZbozi))
                //        tableInfo = ((Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_ImportZbozi)providerZbozi).GetZasobyTableInfo();
                //    else
                //        throw new NotImplementedException("Provider neimplementuje IZbozi2_ImportZbozi.");


                //    //logika importu zbozi z CSV
                //    string pathToFile = string.Empty;
                //    uspech = ImportZboziZFileCSV(tableInfo);

                //    //DialogResult dr = MessageBox.Show("Obsahuje změny, chcete refresh?", "data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                //    //if (dr == System.Windows.Forms.DialogResult.No)
                //    //    return;


                //    //PerformVyhledat();


                //}
                //else if (dr_import == System.Windows.Forms.DialogResult.No)
                //{
                //    DataTable dtchanged = this.dsZbozi.FASK_ZASOBY_ALL_KONZOLA.GetChanges();
                //    if (dtchanged != null && dtchanged.Rows.Count > 0) //obsahuje zmeny ... 
                //    {
                //        DialogResult dr = MessageBox.Show("Obsahuje změny, pokračovat?", "data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                //        if (dr == System.Windows.Forms.DialogResult.No)
                //            return;
                //    }

                //    if (bwImportZbozi.IsBusy)
                //    {
                //        bwImportZbozi.CancelAsync();
                //        while (bwImportZbozi.IsBusy)
                //        {
                //            Application.DoEvents();
                //        }
                //    }

                //    ProgressIndicatorStart();

                //    int FirstDisplayedScrollingRowIndex = this.dg_Zbozi.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

                //    bwImportZbozi.RunWorkerAsync();

                //    if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dg_Zbozi.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dg_Zbozi.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index

                //}
                //else
                //{

                //    //nic se neprovede
                //}
                #endregion

                #region MaR zakomentovan stary zpusob 29.8.2024

                //DataTable dtchanged = this.dsZbozi.FASK_ZASOBY_ALL_KONZOLA.GetChanges();
                //if (dtchanged != null && dtchanged.Rows.Count > 0) //obsahuje zmeny ... 
                //{
                //    DialogResult dr = MessageBox.Show("Obsahuje změny, pokračovat?", "data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                //    if (dr == System.Windows.Forms.DialogResult.No)
                //        return;
                //}

                //if (bwImportZbozi.IsBusy)
                //{
                //    bwImportZbozi.CancelAsync();
                //    while (bwImportZbozi.IsBusy)
                //    {
                //        Application.DoEvents();
                //    }
                //}

                //ProgressIndicatorStart();

                //int FirstDisplayedScrollingRowIndex = this.dg_Zbozi.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

                //bwImportZbozi.RunWorkerAsync();

                //if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dg_Zbozi.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dg_Zbozi.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index 


                #endregion

                bool coSeStalo = true;

            }
            catch (Exception ex)
            {
                ProgressIndicatorStop();
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ImportZboziZFileCSV(List<Tuple<string, string, bool>> tableInfo)
        {
            bool uspech = false;
                try
                {
                    if (!MySystem.LoginTest.UserLoginTest())
                        return uspech;

                //this.dg_Zbozi.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
                uspech =  this.dg_Zbozi.ImportFromCSV(tableInfo);

                }
                catch (Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return uspech;
                }

            return uspech;
            
        }

        private void tsmiOdstranit_Click(object sender, EventArgs e)
        {

            if(!opravneniEditace)
            {
                MessageBox.Show("Nemáte dostatečné oprávnění pro tuto akci!", "Oprávnění", MessageBoxButtons.OK);
                    return;
            }

            DialogResult dialog = MessageBox.Show("Přejete si odstranit položku/y?", this.Text, MessageBoxButtons.YesNo);
            if (dialog != DialogResult.Yes)
            {
                return;
            }

            if (SelectedRows != null)
            {
                try
                {
                    {
                        if (this.SelectedRows.Count == 0)
                        {
                            MessageBox.Show("Neni vybráno žádné zboží.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            return;
                        }

                        foreach (DataGridViewRow material in this.SelectedRows)
                        {
                            Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_ALL_KONZOLARow row = ((DataRowView)material.DataBoundItem).Row as Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_ALL_KONZOLARow;

                            if ((providerZbozi != null) && (providerZbozi is Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_DeleteZbozi))
                            {
                                ((Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_DeleteZbozi)providerZbozi).DeleteZbozi(row.DEX_ROW_ID_Zbozi, row.IsDEX_ROW_ID_PARAMETRYNull() ? null : (int?)row.DEX_ROW_ID_PARAMETRY);
                            }
                            else
                            {
                                throw new NotImplementedException("Provider neobsahuje implemetaci IZbozi2_DeleteZbozi");
                            }
                        }

                        PerformVyhledat();
                    }
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

        #region Perform Metody

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

        private void PerformOK()
        {
            try
            {
                if (Zobrazeni != Fask.Interfaces.Classes.ZOBRAZENI_TYP.VYBER)
                    return;

                if (FASK_ZASOBY_selectedRow == null)
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

        /// <summary>
        /// Vyhledani podle zadaneho filtru.
        /// </summary>
        private void PerformVyhledat()
        {
            try
            {
                //DataTable dtchanged = this.dsZbozi.FASK_ZASOBY_ALL_KONZOLA.GetChanges();
                //if (dtchanged != null && dtchanged.Rows.Count > 0) //obsahuje zmeny ... 
                //{
                //    DialogResult dr = MessageBox.Show("Obsahuje změny, pokračovat?", "data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                //    if (dr == System.Windows.Forms.DialogResult.No)
                //        return;
                //}

                if (bwLoadZbozi.IsBusy)
                {
                    bwLoadZbozi.CancelAsync();
                    while (bwLoadZbozi.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }

                ProgressIndicatorStart();

                Fask.Interfaces.Filtry.ZboziListFiltr filtr = new Fask.Interfaces.Filtry.ZboziListFiltr();
                if (!CreateFilter(ref filtr))
                    return;

                int FirstDisplayedScrollingRowIndex = this.dg_Zbozi.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

                bwLoadZbozi.RunWorkerAsync(filtr);

                if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dg_Zbozi.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dg_Zbozi.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index
            }
            catch (Exception ex)
            {
                ProgressIndicatorStop();
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

                using (Ciselniky.FormZboziEdit frmzbozi = new FormZboziEdit())
                {
                    frmzbozi.Text = "Nové zboží";
                    if (frmzbozi.ShowDialog(this) != DialogResult.OK)
                        return;

                    // pridat ...
                    //this.dsZbozi.FASK_ZASOBY.ImportRow(frmzbozi.returnrow);
                    //this.dsZbozi.FASK_ZASOBY.AcceptChanges();
                    //try
                    //{
                    //    this.bindingSource1.Position = dsZbozi.FASK_ZASOBY.Count - 1;
                    //}
                    //catch { }
                    PerformVyhledat();
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

                if (FASK_ZASOBY_selectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro úpravu", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if(FASK_ZASOBY_selectedRow.MJ.Length > 5)
                {

                    // MessageBox.Show("Parametr Měrná jednotka překračuje rozsah znaků(5 znaků je max)!", this.Text, MessageBoxButtons.OK);
                    
                    // FlexibleMessageBox.Show("Parametr Měrná jednotka překračuje rozsah znaků(5 znaků je max)!", "Nelze upravit záznam!", MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    // Vytvoření nového dialogového okna MessageBox
                    FlexibleMessageBox.Show("Parametr Měrná jednotka překračuje rozsah znaků (maximálně 5 znaků)!", "Nelze upravit záznam!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;

                }

                using (Ciselniky.FormZboziEdit frmzbozi = new FormZboziEdit())
                {

                    frmzbozi.rowZboziEdit = FASK_ZASOBY_selectedRow;
                    frmzbozi.Text = "Úprava zásob";
                    if (frmzbozi.ShowDialog(this) != DialogResult.OK)
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


        #endregion

        #region DataGrid Eventy

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
                    if (FASK_ZASOBY_selectedRow != null)
                        selectedSortID = FASK_ZASOBY_selectedRow.ITEMNMBR;
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
                int pos = this.bsZbozi.Find(dsZbozi.FASK_ZASOBY_ALL_KONZOLA.ITEMNMBRColumn.ColumnName, selectedSortID);
                this.bsZbozi.Position = pos;
            }
            catch { }
        }

        private void advancedDataGridViewSearchToolBar1_Search(object sender, Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs e)
        {
            bool restartsearch = true;
            int startColumn = 0;
            int startRow = 0;
            if (!e.FromBegin)
            {
                bool endcol = dg_Zbozi.CurrentCell.ColumnIndex + 1 >= dg_Zbozi.ColumnCount;
                bool endrow = dg_Zbozi.CurrentCell.RowIndex + 1 >= dg_Zbozi.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dg_Zbozi.CurrentCell.ColumnIndex;
                    startRow = dg_Zbozi.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dg_Zbozi.CurrentCell.ColumnIndex + 1;
                    startRow = dg_Zbozi.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dg_Zbozi.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dg_Zbozi.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dg_Zbozi.CurrentCell = c;
        }


        #endregion

        #region BackGround Worker bwLoadZbozi

        private void bwLoadZbozi_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Fask.Interfaces.Filtry.ZboziListFiltr filtr = (Fask.Interfaces.Filtry.ZboziListFiltr)e.Argument;
                Fask.Interfaces.DataSets.Zbozi ds = new Fask.Interfaces.DataSets.Zbozi();

                if (bwLoadZbozi.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                // nacteni dat v oddelenem vlakne
                if ((providerZbozi != null) && (providerZbozi is Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_GetFiltrovaneZbozi))
                {
                    ds = ((Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_GetFiltrovaneZbozi)providerZbozi).GetFiltrovaneZbozi(filtr);
                }
                else
                {
                    throw new NotImplementedException("Provider neobsahuje implemetaci IZbozi2_GetFiltrovaneZbozi");
                }

                if (bwLoadZbozi.CancellationPending)
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
                    dsZbozi = new Fask.Interfaces.DataSets.Zbozi();
                    bsZbozi.DataSource = dsZbozi;
                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, e.Error);
                    MessageBox.Show(e.Error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    SetStratusLabelText_Zbozi(-1);
                }
                else if (e.Cancelled)
                {
                    // handle cancellation
                    // TODO: maji se vymazat data z datasetu?? asi ano ...
                    //if (dsSkladPohyb == null)
                    dsZbozi = new Fask.Interfaces.DataSets.Zbozi();
                    bsZbozi.DataSource = dsZbozi;

                    SetStratusLabelText_Zbozi(-1);
                }
                else
                {
                    // uspesne dokonceno ...
                    // use it on the UI thread
                    dsZbozi = (Fask.Interfaces.DataSets.Zbozi)e.Result;
                    if (dsZbozi == null)
                        dsZbozi = new Fask.Interfaces.DataSets.Zbozi();

                    bsZbozi.DataSource = dsZbozi;

                    if (dsZbozi.FASK_ZASOBY_ALL_KONZOLA.Count == 0)
                        SetStratusLabelText_Zbozi(-1);
                    else
                    {
                        foreach (DataGridViewRow row in dg_Zbozi.SelectedRows)
                        {
                            SetStratusLabelText_Zbozi(row.Index);
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

        #endregion

        #region Filtry

        private void tsbNastavit_Click(object sender, EventArgs e)
        {
            PerformNastavitFiltr();
        }

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

        /// <summary>
        /// Vytvoreni filtru, ktery se pouzije.
        /// </summary>
        /// <param name="filtr"></param>
        /// <returns></returns>
        private bool CreateFilter(ref Fask.Interfaces.Filtry.ZboziListFiltr filtr)
        {
            if (filtr == null)
                filtr = new Fask.Interfaces.Filtry.ZboziListFiltr();

            filtr.MaterialID = cbMaterialITEMNMBR.Text.Trim();
            filtr.MaterialNazev = cbMaterialOznaceni.Text.Trim();
            filtr.MaterialItemcode = cbMaterialITEMCODE.Text.Trim();
            filtr.MaterialBarcode = cbMaterialBarcode.Text.Trim();
            filtr.ZobrazitDuplicitniCaroveKody = cbZobrazitDuplicitniCaroveKody.Checked;
            filtr.SKL_ID = rowSklad == null ? string.Empty : rowSklad.skl_id;
            //filtr.TypMaterialu = cbTypMaterialu.Text.Trim();
            filtr.TypMaterialu = (Fask.Interfaces.Classes.TypPolozky)Enum.Parse(typeof(Fask.Interfaces.Classes.TypPolozky) ,cbTypMaterialu.SelectedValue.ToString());

            return true;
        }

        /// <summary>
        /// Nastavi zvoleny filtr do comboboxu, ...
        /// </summary>
        /// <param name="filtr"></param>
        private void PerformNastavitFiltr()
        {
            try
            {
                if (rowFiltr == null)
                    return;

                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                PerformVycistitFiltr();

                cbMaterialITEMNMBR.Text = rowFiltr.MaterialID;         // itemnmbr
                cbMaterialITEMCODE.Text = rowFiltr.MaterialItemcode;   // itemcode
                cbMaterialBarcode.Text = rowFiltr.MaterialBarcode;     // barcode
                cbMaterialOznaceni.Text = rowFiltr.MaterialNazev;      // itemdesc
                cbSklad.Text = rowFiltr.SKL_ID;      // SKL_ID
                cbTypMaterialu.SelectedItem = rowFiltr.TypMaterialu;      // ITEMTYPE
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show("Nepodařilo se načíst data filtru.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PerformVycistitFiltr()
        {
            try
            {
                cbTypMaterialu.SelectedItem =
                cbSklad.SelectedItem =
                cbMaterialBarcode.SelectedItem =
                cbMaterialITEMCODE.SelectedItem =
                cbMaterialITEMNMBR.SelectedItem =
                cbMaterialOznaceni.SelectedItem = null;

                cbTypMaterialu.Text =
                cbSklad.Text =
                cbMaterialBarcode.Text =
                cbMaterialITEMCODE.Text =
                cbMaterialITEMNMBR.Text =
                cbMaterialOznaceni.Text = string.Empty;

                cbZobrazitDuplicitniCaroveKody.Checked = false;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

                Fask.Interfaces.Filtry.ZboziListFiltr filtr = rowFiltr;

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
        /// Ulozeni filtru do souboru.
        /// </summary>
        private void PerformPridatFiltr()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                Fask.Interfaces.Filtry.ZboziListFiltr filtr = new Fask.Interfaces.Filtry.ZboziListFiltr();
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



        #endregion

        #region BackGround Worker bwImportZbozi

        private void bwImportZbozi_DoWork(object sender, DoWorkEventArgs e)
        {
            string status;

            try
            {

                if (bwImportZbozi.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }


                // nacteni dat v oddelenem vlakne
                if ((providerZbozi != null) && (providerZbozi is Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_ImportZbozi))
                    status = ((Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_ImportZbozi)providerZbozi).ImportZbozi();
                else
                    throw new NotImplementedException("Provider neimplementuje IZbozi2_ImportZbozi.");

                if (status != "OK")
                {
                    //error...

                    Fask.Logging.ExceptionHandler2.Handle(LogLevel.Error, "ImportZbozi se nepovedl!");
                    MessageBox.Show("Import Zbozi se nepovedl!", "Importování zboží", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (bwImportZbozi.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                //e.Result = ds;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void bwImportZbozi_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
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
                }
                else
                {
                    // use it on the UI thread
                    PerformVyhledat();
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

        #endregion

        #region Exporty 

        private void exportDoCSVVseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                this.dg_Zbozi.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                this.dg_Zbozi.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
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

                this.dg_Zbozi.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                dg_Zbozi.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
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

                dg_Zbozi.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                dg_Zbozi.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region ProgressIndicator

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
                this.progressIndicator1.Location = new Point(this.dg_Zbozi.Location.X + (this.dg_Zbozi.Width / 2) - (progressIndicator1.Size.Width / 2), this.dg_Zbozi.Location.Y + (this.dg_Zbozi.Height / 2) - (progressIndicator1.Size.Height / 2));
            }
            catch { }
            progressIndicator1.Start();
            progressIndicator1.Visible = true;
        }


        #endregion

        #region EasterEgg pro zobrazeni 2D kodu EANU

        private void label5_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (FASK_ZASOBY_selectedRow != null)
                {

                    if (!string.IsNullOrEmpty(FASK_ZASOBY_selectedRow.VNDITNUM))
                    {
                        ZXing.BarcodeWriter bw = new ZXing.BarcodeWriter();
                        bw.Options.Width = 400;
                        bw.Options.Height = 400;
                        bw.Options.PureBarcode = false;
                        bw.Format = ZXing.BarcodeFormat.QR_CODE;
                        Bitmap bmp = bw.Write(FASK_ZASOBY_selectedRow.VNDITNUM.Trim());

                        Forms.FormKod.Show(bmp, "Čár. kód položky");
                    }
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        }

        #endregion

        private void SetStratusLabelText_Zbozi(int index)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    SetStratusLabelText_Zbozi(index);
                }));

                return;
            }

            tssl_Zbozi_Count.Text = string.Format("{0}/{1}", index + 1, dsZbozi.FASK_ZASOBY_ALL_KONZOLA.Count);
        }

        private void dg_Zbozi_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dg_Zbozi.SelectedRows)
            {
                SetStratusLabelText_Zbozi(row.Index);
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

                if (FASK_ZASOBY_selectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                #region MaR 6.11.2024 vypis zpl do souboru selected rows
                int index_zaznamu = 0;

                foreach (var item in FASK_ZASOBY_selectedRows)
                {

                    if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start ciselniky/zasoby TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");

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

                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END ciselniky/zasoby TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");


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



                    foreach (var item in FASK_ZASOBY_selectedRows)
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
                    Tisk_selected_rows_Primy_Tisk(plr_Path, true, MN_ToTisk, FASK_ZASOBY_selectedRows, NazevVychoziTiskarny);

                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Tisk_selected_rows_Primy_Tisk(string plr_Path, bool MnozstvuAutoJedna, int MN_ToTisk, List<Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_ALL_KONZOLARow> FASK_ZASOBY_selected_Rows, string nazevVychTiskarny)
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

                if (FASK_ZASOBY_selectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                //if (FASK_ZASOBY_selectedRows.Count > 1)
                //{
                //    //MessageBox.Show("Je vybráno více záznamů než jeden.", this.Text, MessageBoxButtons.OK);
                //    //return;
                //}

                Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_ALL_KONZOLADataTable dt = new Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_ALL_KONZOLADataTable();

                dt.Columns.Add("VNDITNUM_IMG", typeof(string));
                dt.Columns.Add("CZ_CarKod_IMG", typeof(string));
                dt.Columns.Add("LOCNCODE_IMG", typeof(string));


                foreach (Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_ALL_KONZOLARow row in FASK_ZASOBY_selectedRows)
                {
                    try
                    {
                        Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_ALL_KONZOLARow newRow = dt.NewFASK_ZASOBY_ALL_KONZOLARow();
                        // Ošetřit delší vstupní pole než počet sloupců tabulky
                        int copyLength = Math.Min(row.ItemArray.Length, dt.Columns.Count);
                        newRow.ItemArray = row.ItemArray.Take(copyLength).ToArray();
                        dt.AddFASK_ZASOBY_ALL_KONZOLARow(newRow);
                    }
                    catch (Exception ex)
                    {
                        // Ošetření vyjimky
                        // Můžete zde provést logování chyby nebo jiné požadované akce
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                    }


                }

                foreach (Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_ALL_KONZOLARow row in dt)
                {
                   
                    try
                    {
                        if (!row.IsVNDITNUMNull() && !string.IsNullOrEmpty(row.VNDITNUM))
                        {
                            ZXing.BarcodeWriter zw = new ZXing.BarcodeWriter();
                            zw.Format = ZXing.BarcodeFormat.CODE_128;
                            zw.Options.Height = 50; //50
                            zw.Options.PureBarcode = true;
                            System.Drawing.Bitmap image1 = zw.Write(row.VNDITNUM);

                            System.IO.MemoryStream ms = new System.IO.MemoryStream();
                            image1.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                            byte[] imgReportBarcode = ms.ToArray();
                            ms.Close();

                            string Base64Imahe = Convert.ToBase64String(imgReportBarcode);

                            row["VNDITNUM_IMG"] = Base64Imahe;
                        }

                        if (!row.IsCZ_CarKodNull() && !string.IsNullOrEmpty(row.CZ_CarKod))
                        {
                            ZXing.BarcodeWriter zw = new ZXing.BarcodeWriter();
                            zw.Format = ZXing.BarcodeFormat.CODE_128;
                            zw.Options.Height = 50; //50
                            zw.Options.PureBarcode = true;
                            System.Drawing.Bitmap image1 = zw.Write(row.CZ_CarKod);

                            System.IO.MemoryStream ms = new System.IO.MemoryStream();
                            image1.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                            byte[] imgReportBarcode = ms.ToArray();
                            ms.Close();

                            string Base64Imahe = Convert.ToBase64String(imgReportBarcode);

                            row["CZ_CarKod_IMG"] = Base64Imahe;

                        }

                        if (!row.IsLOCNCODENull() && !string.IsNullOrEmpty(row.LOCNCODE))
                        {
                            ZXing.BarcodeWriter zw = new ZXing.BarcodeWriter();
                            zw.Format = ZXing.BarcodeFormat.CODE_128;
                            zw.Options.Height = 50; //50
                            zw.Options.PureBarcode = true;
                            System.Drawing.Bitmap image1 = zw.Write(row.LOCNCODE);

                            System.IO.MemoryStream ms = new System.IO.MemoryStream();
                            image1.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                            byte[] imgReportBarcode = ms.ToArray();
                            ms.Close();

                            string Base64Imahe = Convert.ToBase64String(imgReportBarcode);

                            row["LOCNCODE_IMG"] = Base64Imahe;

                        }

                    }
                    catch (Exception ex)
                    {

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

        private void PrintReport_RDLC(Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_ALL_KONZOLADataTable dt, bool primyTisk, string path_sablona, string nazevVychTiskarny, int pocetVytisku)
        {
            try
            {

                #region logovani tisku
                if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                {
                    foreach (var item in dt)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start ciselniky/zasoby TISK rdlc-------------------------------------");

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

                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END ciselniky/zasoby TISK rdlc-------------------------------------");
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

                if (FASK_ZASOBY_selectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                #region MaR 6.11.2024 vypis zpl do souboru selected rows
                int index_zaznamu = 0;

                foreach (var item in FASK_ZASOBY_selectedRows)
                {

                    if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start ciselniky/zasoby TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");

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

                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END ciselniky/zasoby TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");


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



                    foreach (var item in FASK_ZASOBY_selectedRows)
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
                    Tisk_selected_rows(plr_Path, true, MN_ToTisk, FASK_ZASOBY_selectedRows);

                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Tisk_selected_rows(string plr_Path, bool MnozstvuAutoJedna, int MN_ToTisk, List<Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_ALL_KONZOLARow> FASK_ZASOBY_selected_Rows)
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
            Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_ALL_KONZOLARow row,
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

                Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_ALL_KONZOLADataTable dt = new Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_ALL_KONZOLADataTable();

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

        private Dictionary<string, string> PrepareDataToTisk(Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_ALL_KONZOLARow productionRow_data)
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

                        Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_ALL_KONZOLADataTable dt = new Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_ALL_KONZOLADataTable();


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
