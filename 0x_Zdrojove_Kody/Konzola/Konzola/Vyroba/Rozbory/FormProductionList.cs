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
using Konzola.Vyroba.Transakce;
using Konzola.Vyroba.Rozbory;
using Fask.ModuleSql_API;
using Fask.ModuleSql_API.Classes;
using Konzola.Forms;

namespace Konzola.Vyroba
{
    public partial class FormProductionList : Form
    {

        private bool opravneni = false;
        #region Parametry

        /// <summary>
        /// Provider pro komunikaci
        /// </summary>
        private Fask.Interfaces.IMES providerProduction = null; //**DONE
        private Fask.Interfaces.IMES providerZbozi = null;
        private Fask.Interfaces.IMES providerVPH = null;  //**DONE

        private Fask.Interfaces.IMES providerGropus = null;

        private Fask.Interfaces.IMES providerMachines = null;
        private Fask.Interfaces.IMES providerOperations = null;
        //private Fask.Interfaces.IVyrobaKonzola providerLogins = null;
        private Fask.Interfaces.IMES providerTisk = null; //**DONE
        protected Fask.Interfaces.IMES provider = null;


        /// <summary>
        /// Zvolený VPH záznam.
        /// </summary>
        private Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHRow rowVPH
        {
            get
            {
                try
                {
                    //return ((DataRowView)(comboBoxVyrobniPrikaz.SelectedItem)).Row as Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHRow;
                    return comboBoxVyrobniPrikaz.SelectedItem as Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHRow;
                }
                catch
                {
                    return null;
                }
            }
            set
            {
                try
                {
                    comboBoxVyrobniPrikaz.SelectedItem = value;
                }
                catch
                {
                    comboBoxVyrobniPrikaz.SelectedIndex = 0;
                    comboBoxVyrobniPrikaz.Text = string.Empty;
                }
            }
        }

        /// <summary>
        /// Vybraná skupina
        /// </summary>
        private Fask.Interfaces.DataSets.Vyroba.GroupsRow rowGroups
        {
            get
            {
                try
                {
                    return comboBoxSkupina.SelectedItem as Fask.Interfaces.DataSets.Vyroba.GroupsRow;
                }
                catch
                {
                    return null;
                }
            }
            set
            {
                try
                {
                    comboBoxSkupina.SelectedItem = value;
                }
                catch
                {
                    comboBoxSkupina.SelectedIndex = 0;
                    comboBoxSkupina.Text = string.Empty;
                }
            }
        }

        /// <summary>
        /// Zvolený stroj
        /// </summary>
        private Fask.Interfaces.DataSets.Vyroba.MachinesRow rowMachine
        {
            get
            {
                try
                {
                    return comboBoxStroj.SelectedItem as Fask.Interfaces.DataSets.Vyroba.MachinesRow;
                }
                catch
                {
                    return null;
                }
            }
            set
            {
                try
                {
                    comboBoxStroj.SelectedItem = value;
                }
                catch
                {
                    comboBoxStroj.SelectedIndex = 0;
                    comboBoxStroj.Text = string.Empty;
                }
            }
        }

        /// <summary>
        /// Zvolená operace
        /// </summary>
        private Fask.Interfaces.DataSets.Vyroba.OperationsRow rowOperation
        {
            get
            {
                try
                {
                    return comboBoxOperace.SelectedItem as Fask.Interfaces.DataSets.Vyroba.OperationsRow;
                }
                catch
                {
                    return null;
                }
            }
            set
            {
                try
                {
                    comboBoxOperace.SelectedItem = value;
                }
                catch
                {
                    comboBoxOperace.SelectedIndex = 0;
                    comboBoxOperace.Text = string.Empty;
                }
            }
        }

        /// <summary>
        /// Zvolený Production záznam.
        /// </summary>
        private Fask.Interfaces.DataSets.Vyroba.Production_KonzolaRow rowProduct
        {
            get
            {
                try
                {
                    return ((DataRowView)(dgVyrobek.BindingContext[this.bsVyrobek].Current)).Row as Fask.Interfaces.DataSets.Vyroba.Production_KonzolaRow;
                }
                catch
                {
                    return null;
                }
            }
        }

        private List<Fask.Interfaces.DataSets.Vyroba.Production_KonzolaRow> Production_selectedRows
        {
            //get
            //{
            //    List<Fask.Interfaces.DataSets.Vyroba.Production_KonzolaRow> rows = new List<Fask.Interfaces.DataSets.Vyroba.Production_KonzolaRow>();

            //    foreach (DataGridViewRow selectedRow in dgVyrobek.SelectedRows)
            //    {
            //        Fask.Interfaces.DataSets.Vyroba.Production_KonzolaRow row = ((DataRowView)selectedRow.DataBoundItem).Row as Fask.Interfaces.DataSets.Vyroba.Production_KonzolaRow;
            //        rows.Add(row);
            //    }

            //    return rows;
            //}

            get
            {
                return dgVyrobek.SelectedRows
                    .Cast<DataGridViewRow>()
                    .OrderBy(r => r.Index)               // zarovnání na vizuální pořadí v gridu
                    .Select(r => ((DataRowView)r.DataBoundItem).Row as Fask.Interfaces.DataSets.Vyroba.Production_KonzolaRow)
                    .Where(r => r != null)
                    .ToList();
            }
        }

        private FASK.Logins.DataSets.Pristupy.FASK_LoginsRow rowUzivatel
        {
            get
            {
                try
                {
                    return comboBoxUzivatel.SelectedItem as FASK.Logins.DataSets.Pristupy.FASK_LoginsRow;
                }
                catch
                {
                    return null;
                }
            }
            set
            {
                try
                {
                    comboBoxUzivatel.SelectedItem = value;
                }
                catch
                {
                    comboBoxUzivatel.SelectedIndex = 0;
                    comboBoxUzivatel.Text = string.Empty;
                }
            }
        }

        private Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_KONZOLARow rowZbozi
        {
            get
            {
                try
                {
                    return comboBoxZbozi.SelectedItem as Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_KONZOLARow;
                }
                catch
                {
                    return null;
                }
            }
            set
            {
                try
                {
                    comboBoxZbozi.SelectedItem = value;
                }
                catch
                {
                    comboBoxZbozi.SelectedIndex = 0;
                    comboBoxZbozi.Text = string.Empty;
                }
            }
        }


        #region Parametry Filtry


        /// <summary>
        /// Seznam vsech nactenych filtru.
        /// </summary>
        private List<Fask.Interfaces.Filtry.ProductionListFiltr> filtry = new List<Fask.Interfaces.Filtry.ProductionListFiltr>();

        /// <summary>
        /// Vybrany filtr.
        /// </summary>
        private Fask.Interfaces.Filtry.ProductionListFiltr rowFiltr
        {
            get
            {
                try
                {
                    return tscbFiltry.SelectedItem as Fask.Interfaces.Filtry.ProductionListFiltr;
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
        public FormProductionList()
        {
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);

                

            InitializeComponent();
            this.dgVyrobek.UpdateColumnHeaderCellsByDatasource();

            menuStrip1.Items.Remove(tsmiAkce);
            tsmiAkce.DropDownItems.Remove(tsmiArchivaceVybrane);


            panelButtons.Menu = menuStrip1;
        }

        public FormProductionList(bool opravneni)
        {
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);

          

            InitializeComponent();
            this.dgVyrobek.UpdateColumnHeaderCellsByDatasource();


            this.opravneni = opravneni;

            if (!opravneni)
            {
                menuStrip1.Items.Remove(tsmiAkce);
                tsmiAkce.DropDownItems.Remove(tsmiArchivaceVybrane);
            }
                

            //menuStrip1.Items.
            panelButtons.Menu = menuStrip1;
        }

        private Opravneni opravneni_pravo;

        private bool opravneniEditace = false;
        private bool opravneniImport = false;
        private bool opravneniArchivace = false;

        public FormProductionList(Opravneni opravneni)
        {
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);



            InitializeComponent();
            this.dgVyrobek.UpdateColumnHeaderCellsByDatasource();


            this.opravneni_pravo = opravneni;

           
            SetOpravneni();

            //menuStrip1.Items.

            if (!opravneniArchivace)
            {
                menuStrip1.Items.Remove(tsmiAkce);
                tsmiAkce.DropDownItems.Remove(tsmiArchivaceVybrane);
            }

            panelButtons.Menu = menuStrip1;
        }

        private void SetOpravneni()
        {

            if (opravneni_pravo.HasFlag(Opravneni.Editace) && opravneni_pravo.HasFlag(Opravneni.Archivace) && opravneni_pravo.HasFlag(Opravneni.Import))
            {
                opravneniEditace = true;
                opravneniImport = true;
                opravneniArchivace = true;
                // Povolit funkce pro oba případy
                //MessageBox.Show("Máte oprávnění k editaci i importu.");
            }

            // Nastavíte možnosti formuláře na základě oprávnění
            if (opravneni_pravo.HasFlag(Opravneni.Editace))
            {
                opravneniEditace = true;
                // Povolit funkce pro editaci
                // například povolit nějaké tlačítka nebo editační pole
            }

            if (opravneni_pravo.HasFlag(Opravneni.Import))
            {
                opravneniImport = true;
                // Povolit funkce pro import
                // například povolit tlačítko nebo nabídku pro import
            }

            if (opravneni_pravo.HasFlag(Opravneni.Archivace))
            {
                opravneniArchivace = true;

            

              

                // Povolit funkce pro import
                // například povolit tlačítko nebo nabídku pro import
            }

            // Zkontroluje, zda má uživatel obě oprávnění


            // Můžete přidat další logiku pro další oprávnění
        }


        private void FormProductionList_Load(object sender, EventArgs e)
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
                this.WindowState = FormWindowState.Maximized;

                // načtení konfigurace datagridu z nastavení aplikace
                this.dgVyrobek.LoadConfiguration(this.GetType().ToString());

                panelButtons.LoadConfiguration(this.GetType().ToString());
                panelButtons.Init();
                panelButtons.Size = new Size(85, 700);

                advancedDataGridViewSearchToolBar1.SetColumns(dgVyrobek.Columns);

             

                InitProvider();

                if (providerProduction == null)
                    throw new Exception("Provider 'Production' není inicializován");

                if (providerZbozi == null)
                    throw new Exception("Provider 'Zbozi' není inicializován");

                if (providerVPH == null)
                    throw new Exception("Provider 'VPH' není inicializován");

                if (providerGropus == null)
                    throw new Exception("Provider 'Groups' není inicializován");

                if (providerMachines == null)
                    throw new Exception("Provider 'Machines' není inicializován");

                if (providerOperations == null)
                    throw new Exception("Provider 'Operations' není inicializován");

                //if (provider == null)
                //    throw new Exception("Provider 'Archivace' není inicializován");

                //if (providerLogins == null)
                //    throw new Exception("Provider 'Logins' není inicializován");

                // nastavení času
                // nastavení času (zacatek a konec dne)

                //DateTime dnes = DateTime.Now;
                //DateTime zitra = DateTime.Now.AddDays(1);

                //dateTimePickerDatumOd.Value = new DateTime(dnes.Year, dnes.Month, dnes.Day, 0, 0, 0);
                //dateTimePickerDatumDo.Value = new DateTime(zitra.Year, zitra.Month, zitra.Day, 0, 0, 0);

                DateTime today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);

                dateTimePickerDatumOd.Value = today.AddDays(-1);
                dateTimePickerDatumDo.Value = today;

                //dateTimePickerDatumDo.Value = DateTime.Now.AddDays(1).Date.AddSeconds(-1);
                dateTimePickerDatumDo.Checked = false;
                dateTimePickerDatumOd.Checked = false;

                if ((providerVPH != null) && providerVPH is Fask.Interfaces.Vyroba.VPH.IVPH_Fill)
                    ((Fask.Interfaces.Vyroba.VPH.IVPH_Fill)providerVPH).VPH_Fill(this.dsVyrobek);
                else
                    throw new Exception("IVPH_Fill not implementet");

                comboBoxVyrobniPrikaz.Items.AddRange(this.dsVyrobek.CZPRO_VPH.Select(null, "SOPNUMBE asc"));
                comboBoxVyrobniPrikaz.SelectedItem = null;

                if ((providerGropus != null) && providerGropus is Fask.Interfaces.Vyroba.Groups.IGroups_FillGroups)
                    ((Fask.Interfaces.Vyroba.Groups.IGroups_FillGroups)providerGropus).Groups_Fill(this.dsVyrobek);
                else
                    throw new Exception("IGroups_FillGroups not implementet");

                comboBoxSkupina.Items.AddRange(this.dsVyrobek.Groups.Select(null, "name asc"));
                comboBoxSkupina.SelectedItem = null;

                if ((providerMachines != null) && providerMachines is Fask.Interfaces.Vyroba.Machines.IMachines_Fill)
                    ((Fask.Interfaces.Vyroba.Machines.IMachines_Fill)providerMachines).Machines_Fill(this.dsVyrobek);
                else
                    throw new Exception("IMachines_Fill not implementet");

                comboBoxStroj.Items.AddRange(this.dsVyrobek.Machines.Select(null, "name asc"));
                comboBoxStroj.SelectedItem = null;

                if ((providerOperations != null) && providerOperations is Fask.Interfaces.Vyroba.Operations.IOperations_Fill)
                    ((Fask.Interfaces.Vyroba.Operations.IOperations_Fill)providerOperations).Operations_Fill(this.dsVyrobek);
                else
                    throw new Exception("IOperations_Fill not implementet");

                comboBoxOperace.Items.AddRange(this.dsVyrobek.Operations.Select(null, "name asc"));
                comboBoxOperace.SelectedItem = null;

                if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba[0].PouzivatTabulkuZbozi)
                {

                    Fask.Interfaces.DataSets.Zbozi ds = new Fask.Interfaces.DataSets.Zbozi();

                    if ((providerZbozi != null) && (providerZbozi is Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_GetZbozi))
                        ds = ((Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_GetZbozi)providerZbozi).GetZbozi();
                    else
                        throw new NotImplementedException("Neimplementovan provider pro IZbozi2_GetZbozi");


                    comboBoxZbozi.Items.AddRange(ds.FASK_ZASOBY_ALL_KONZOLA.Select(null, "ITEMDESC asc"));

                    comboBoxZbozi.SelectedItem = null;
                }
                else comboBoxZbozi.Enabled = false;


                cb_TimeVariant.Items.AddRange(Fask.Interfaces.Classes.TimeFilters.GetNumberNameRange());
                cb_TimeVariant.SelectedIndex = 0;

                //if ((providerLogins != null) && (providerLogins is Fask.Interfaces.Vyroba.Login.ILogin_FillLogin))
                //    ((Fask.Interfaces.Vyroba.Login.ILogin_FillLogin)providerLogins).FillLogin(this.vyrobaDataSet1);
                //else
                //    throw new NotImplementedException("Neimplementovan provider pro ILogin_FillLogin");

                var Logins = FASK.Logins.Uzivatel.Instance.Komunikace.GetLogins();

                comboBoxUzivatel.Items.AddRange(Logins.Select(null, "surname asc, firstname asc"));
                comboBoxUzivatel.SelectedItem = null;

                //// načtení konfigurace vytvořených filtrů
                this.filtry = Extensions.ListExt.ReadFromXML<Fask.Interfaces.Filtry.ProductionListFiltr>(this.GetType().ToString() + ".filtr");
                tscbFiltry.ComboBox.DataSource = this.filtry;
                tscbFiltry.SelectedItem = null;
                tscbFiltry.ComboBox.DropDownWitdhAutosize();

                SetStatusLabelText_Events(-1);

                if (!opravneniArchivace)
                {
                    //if (!Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].Povolit_VolnyPohyb_Nasnimane_ImpDavku)
                        menuStrip1.Items.Remove(tsmiAkce);
                    //tsmiAkce.
                    tsmiAkce.DropDownItems.Remove(tsmiArchivaceVybrane);
                }


     



                buttonVyhledat.Focus();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormProductionList_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.dgVyrobek.SaveConfiguration(this.GetType().ToString());
                panelButtons.SaveConfiguration(this.GetType().ToString());

                this.filtry.WriteXML(this.GetType().ToString() + ".filtr");
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormProductionList_KeyDown(object sender, KeyEventArgs e)
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


        #endregion

        #region inicaliyace provideru

        /// <summary>
        /// Inicializace providera Production
        /// </summary>
        private void InitProvider()
        {
            #region Productions
            try
            {
                if (String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                    return;

                if (providerProduction == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Vyroba.Production.IProduction).IsAssignableFrom(t))
                            {
                                providerProduction = (Fask.Interfaces.Vyroba.Production.IProduction)providerAssemlby.CreateInstance(t.FullName);
                                if (providerProduction != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                providerProduction.InitProvider();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion

            #region Zbozi
            try
            {

                if (providerZbozi == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
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
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion

            #region providerVPH
            try
            {

                if (providerVPH == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Vyroba.VPH.IVPH).IsAssignableFrom(t))
                            {
                                providerVPH = (Fask.Interfaces.Vyroba.VPH.IVPH)providerAssemlby.CreateInstance(t.FullName);
                                if (providerVPH != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                providerVPH.InitProvider();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion

            #region providerGroups
            try
            {

                if (providerGropus == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Vyroba.Groups.IGroups).IsAssignableFrom(t))
                            {
                                providerGropus = (Fask.Interfaces.Vyroba.Groups.IGroups)providerAssemlby.CreateInstance(t.FullName);
                                if (providerGropus != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                providerGropus.InitProvider();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion

            #region providerMachines
            try
            {

                if (providerMachines == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Vyroba.Machines.IMachines).IsAssignableFrom(t))
                            {
                                providerMachines = (Fask.Interfaces.Vyroba.Machines.IMachines)providerAssemlby.CreateInstance(t.FullName);
                                if (providerMachines != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                providerMachines.InitProvider();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion

            #region providerOperations
            try
            {

                if (providerOperations == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Vyroba.Operations.IOperations).IsAssignableFrom(t))
                            {
                                providerOperations = (Fask.Interfaces.Vyroba.Operations.IOperations)providerAssemlby.CreateInstance(t.FullName);
                                if (providerOperations != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                providerOperations.InitProvider();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion

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

            #region archivace

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
            #endregion

            //#region providerLogins
            //try
            //{

            //    if (providerLogins == null) //inicializace se provede pouze pokud nebyla provedena ... 
            //    {
            //        Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
            //        Type[] types = providerAssemlby.GetTypes();
            //        foreach (Type t in types)
            //        {
            //            try
            //            {
            //                if (typeof(Fask.Interfaces.Vyroba.Login.ILogin).IsAssignableFrom(t))
            //                {
            //                    providerLogins = (Fask.Interfaces.Vyroba.Login.ILogin)providerAssemlby.CreateInstance(t.FullName);
            //                    if (providerLogins != null)
            //                        break;
            //                }
            //            }
            //            catch { }
            //        }
            //        //return config;
            //    }

            //    // nastaveni connection stringu
            //    if ((providerLogins != null) && (providerLogins is Fask.Interfaces.Parametry.IParametry2_ConnectionString))
            //        ((Fask.Interfaces.Parametry.IParametry2_ConnectionString)providerLogins).ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
            //}
            //catch (Exception ex)
            //{
            //    Fask.Logging.ExceptionHandler2.Handle(ex, "Load Provider.Logins");
            //    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //#endregion
        }

        #endregion

        #region NEPOUZIVA SA

        private void comboBoxVyrobniPrikaz_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (rowVPH != null)
            //    label4.Text = rowVPH.SOPNUMBE.Trim();
            //else
            //    label4.Text = string.Empty;


            //try
            //{
            //    //if (comboBoxVyrobniPrikaz.SelectedIndex == -1)  // nic není zvoleno
            //    //    return;
            //    //if (rowVPH == null)      // nic není zvoleno
            //    //{
            //    //    textBoxVyrobniPrikaz.Text = string.Empty;
            //    //    textBoxZbozi.Text = string.Empty;
            //    //    comboBoxZbozi.SelectedItem = null;
            //    //    comboBoxZbozi.DataSource = null;
            //    //    return;
            //    //}
            //    if (comboBoxVyrobniPrikaz.SelectedIndex != -1)
            //    {
            //        textBoxVyrobniPrikaz.Text = rowVPH.SOPNUMBE.Trim();
            //        var adapter = new Production.DataServices.VyrobaDataSetTableAdapters.CZPRO_VPPTableAdapter();
            //        adapter.Connection.ConnectionString = Properties.Settings.Default.Production_ConncetionString;
            //        var dataVPP = adapter.GetDataByCountEntriesAndSOPNUMBE(rowVPH.CountEntries, rowVPH.SOPNUMBE);
            //        comboBoxZbozi.DataSource = dataVPP;
            //        comboBoxZbozi.SelectedIndex = -1;
            //        comboBoxZbozi.Text = string.Empty;
            //    }
            //    else
            //    {
            //        textBoxVyrobniPrikaz.Text = string.Empty;
            //        textBoxZbozi.Text = string.Empty;
            //        comboBoxZbozi.SelectedIndex = -1;
            //        comboBoxZbozi.DataSource = null;
            //    }


            //}
            //catch (Exception ex)
            //{
            //    Log.Write(ex);
            //    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}            
        }

        private void comboBoxZbozi_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (rowZbozi != null)
            //    label5.Text = rowZbozi.ToString();
            //else
            //    label5.Text = string.Empty;

            //try
            //{
            //    if (rowVPP == null) // není zvolené VPP  ... odstranit data z datagridviewu
            //    {
            //        textBoxZbozi.Text = string.Empty;
            //        this.bindingSourceProduct.DataSource = null;
            //        //this.dataGridView1.DataSource = null;
            //    }
            //    else
            //    {
            //        textBoxZbozi.Text = rowVPP.ITEMNMBR;

            //        var adapter = new Production.DataServices.VyrobaDataSetTableAdapters.ProductionTableAdapter();
            //        adapter.Connection.ConnectionString = Properties.Settings.Default.Production_ConncetionString;
            //        var dataVPP = adapter.GetDataByCountEntriesAndSOPNUMBEandITEMNMBR(rowVPH.CountEntries, rowVPH.SOPNUMBE, rowVPP.ITEMNMBR);
            //        this.bindingSourceProduct.DataSource = dataVPP;
            //        //this.dataGridView1.DataSource = dataVPP;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Log.Write(ex);
            //    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}            
        }

        private void comboBoxUzivatel_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (rowUzivatel != null)
            //    label6.Text = rowUzivatel.ToString();
            //else
            //    label6.Text = string.Empty;
        }

        private void textBoxZbozi_Leave(object sender, EventArgs e)
        {
            //try
            //{
            //    comboBoxZbozi.SelectedValue = textBoxZbozi.Text;
            //    //if(comboBoxZbozi.Items.Contains(textBoxVyrobniPrikaz.Text.Trim()))
            //    //{
            //    //    comboBoxZbozi.SelectedValue = textBoxVyrobniPrikaz.Text.Trim();
            //    //}
            //    //else
            //    //{
            //    //    comboBoxZbozi.SelectedItem = null;
            //    //}

            //}
            //catch (Exception ex)
            //{
            //    Log.Write(ex);
            //    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void PerformEditRecord()
        {
            try
            {
                if (this.dgVyrobek.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Je možné upravovat pouze po jednom záznamu", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if (rowProduct == null)
                {
                    MessageBox.Show("Není vybrán záznam pro úpravu", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if (rowProduct.IsTIMESTOPNull())
                {
                    MessageBox.Show("Zvolený záznam obsahuje pouze zahájení výroby", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if (!rowProduct.IsTIMECORSTARTNull())
                {
                    MessageBox.Show("Korekci nelze upravovat", this.Text, MessageBoxButtons.OK);
                    return;
                }

                using (FormProductionEdit frmuziv = new FormProductionEdit())
                {
                    frmuziv.rowProduct = rowProduct;
                    frmuziv.Text = "Úprava výroby";
                    if (frmuziv.ShowDialog(this) != DialogResult.OK)
                        return;

                    //var lta = new Production.DataServices.VyrobaDataSetTableAdapters.ProductionTableAdapter();
                    //lta.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
                    ////lta.Delete(SelectedRow.ITEMNMBR);
                    //lta.Update(frmuziv.rowProduct);
                    ////lta.Update(this.vyrobaDataSet1.Production);


                    if ((providerProduction != null) && providerProduction is Fask.Interfaces.Vyroba.Production.IProduction_Update_Row)
                        ((Fask.Interfaces.Vyroba.Production.IProduction_Update_Row)providerProduction).Production_Update_Row(frmuziv.rowProduct);
                    else
                        throw new Exception("IProduction_Update_Row not implementet");

                    this.dsVyrobek.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkBoxZakazky_CheckedChanged(object sender, EventArgs e)
        {
            // odškrtnutí filtru na zakázky
            //if (checkBoxZakazky.Checked)
            //    checkBoxNedokonceneKorekce.Checked = false;
        }

        private void checkBoxNedokonceneKorekce_CheckedChanged(object sender, EventArgs e)
        {
            //if (checkBoxNedokonceneKorekce.Checked)
            //    checkBoxZakazky.Checked = false;

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
                DataTable dtchanged = this.dsVyrobek.Production_Konzola.GetChanges();
                if (dtchanged != null && dtchanged.Rows.Count > 0) //obsahuje zmeny ... 
                {
                    DialogResult dr = MessageBox.Show("Obsahuje změny, pokračovat?", "data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == System.Windows.Forms.DialogResult.No)
                        return;
                }

                if (bw_Vyrobek.IsBusy)
                {
                    bw_Vyrobek.CancelAsync();
                    while (bw_Vyrobek.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }

                ProgressIndicatorVyrobekStart();

                Fask.Interfaces.Filtry.ProductionListFiltr filtr = new Fask.Interfaces.Filtry.ProductionListFiltr();
                if (!CreateFilter(ref filtr))
                    return;

                int FirstDisplayedScrollingRowIndex = this.dgVyrobek.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

                bw_Vyrobek.RunWorkerAsync(filtr);

                if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dgVyrobek.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dgVyrobek.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index
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
        private void PerformImport(Fask.Interfaces.DataSets.Vyroba.ProductionImportRow Row)
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

                ProgressIndicatorVyrobekStart();

                bw_import.RunWorkerAsync(Row);
            }
            catch (Exception ex)
            {
                ProgressIndicatorVyrobekStop();
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        #endregion

        #region Click eventy menu a button

        private void konecToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void buttonVyhledat_Click(object sender, EventArgs e)
        {

            #region prace s datagridem - nastaveni numeric hodnot na pocet desetinnych mist
            try
            {
                // Procházení sloupců v DataGridView
                foreach (DataGridViewColumn column in dgVyrobek.Columns)
                {
                    // Získání typu dat pro sloupec
                    Type dataType = null;

                    if (dgVyrobek.DataSource is DataTable dataTable)
                    {
                        // Pokud je datový zdroj DataTable
                        if (dataTable.Columns.Contains(column.DataPropertyName))
                        {
                            dataType = dataTable.Columns[column.DataPropertyName].DataType;
                        }
                    }
                    else if (dgVyrobek.DataSource is BindingSource bindingSource)
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

            //PerformOK();
            PerformVyhledat();
        }

        private void buttonOdznacitVse_Click(object sender, EventArgs e)
        {
            try
            {
                this.dgVyrobek.ClearSelection();
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
                this.dgVyrobek.SelectAll();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsmiUpravit_Click(object sender, EventArgs e)
        {

            if (!opravneniEditace)
            {
                MessageBox.Show("Nemáte dostatečné oprávnění pro tuto akci!", "Oprávnění", MessageBoxButtons.OK);
                return;
            }


            try
            {
                //MessageBox.Show("Bude implementováno...", this.Text, MessageBoxButtons.OK);
                //return;

                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (this.dgVyrobek.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Je možné upravovat pouze po jednom záznamu", this.Text, MessageBoxButtons.OK);
                    return;
                }


                if (rowProduct == null)
                {
                    MessageBox.Show("Není vybrán záznam pro úpravu", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if (!rowProduct.IsISOKNull())
                {
                    MessageBox.Show("Záznam importovaný do IS nelze upravovat!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //if (rowProduct.IsTIMESTOPNull())
                //{
                //    MessageBox.Show("Zvolený záznam obsahuje pouze zahájení výroby", this.Text, MessageBoxButtons.OK);
                //    return;
                //}
                //if (!rowProduct.IsTIMECORSTARTNull())
                //{
                //    MessageBox.Show("Korekci nelze upravovat", this.Text, MessageBoxButtons.OK);
                //    return;
                //}

                using (FormProductionEdit3 frmuziv = new FormProductionEdit3())
                {
                    frmuziv.rowProduct = rowProduct;
                    frmuziv.Text = "Úprava výroby";
                    if (frmuziv.ShowDialog(this) != DialogResult.OK)
                        return;

                    //var lta = new Production.DataServices.VyrobaDataSetTableAdapters.ProductionTableAdapter();
                    //lta.Connection.ConnectionString = Properties.Settings.Default.Production_ConncetionString;
                    //lta.Delete(SelectedRow.ITEMNMBR);
                    //lta.Update(frmuziv.rowProduct);
                    //lta.Update(this.vyrobaDataSet1.Production);
                    //this.vyrobaDataSet1.AcceptChanges();
                }
                // opetovne vyhledani zaznamu
                //PerformOK();
                PerformVyhledat();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsmiUkoncitZakazkuKorekci_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                Fask.Interfaces.DataSets.Vyroba dsOdvadeni = new Fask.Interfaces.DataSets.Vyroba();
                Fask.Interfaces.DataSets.Vyroba dsKorekce = new Fask.Interfaces.DataSets.Vyroba();
                //var taProduction = new Production.DataServices.VyrobaDataSetTableAdapters.ProductionTableAdapter();
                //taProduction.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;

                if (rowProduct == null)
                {
                    MessageBox.Show("Není vybrán záznam pro ukončení", this.Text, MessageBoxButtons.OK);
                    return;
                }

                //List<Fask.Interfaces.DataSets.Vyroba.ProductionRow> listProduction = new List<DataServices.VyrobaDataSet.ProductionRow>();

                //// výběr veškerých SOUBEHGUID a CORRGUID            
                //foreach (var item in this.dataGridView1.SelectedRows)
                //{
                //    listProduction.Add(item as Fask.Interfaces.DataSets.Vyroba.ProductionRow);                
                //}
                //// unikátní guid
                //var selectedSOUBEHGUID = listProduction.Select(x => x.SOUBEHGUID).Distinct();
                //var selectedCORRGUID = listProduction.Select(x => x.CORRGUID).Distinct();
                //// najití všech dat podle zvolených SOUBEHGUID
                //foreach (var item in selectedSOUBEHGUID)
                //{
                //    //taProduction.FillBySOUBEHGUID(dsOdvadeni.Production, item);
                //    dsOdvadeni.Production.Merge(taProduction.GetDataBySOUBEHGUID(item));
                //}
                //// najití všech dat podle zvolených CORRGUID
                //foreach (var item in selectedCORRGUID)
                //{
                //    //taProduction.FillByCORRGUID(dsKorekce.Production, item);
                //    dsKorekce.Production.Merge(taProduction.GetDataByCORRGUID(item));
                //}

                //var finishedSOUBEHGUID = dsOdvadeni.Production.Where(x => !x.IsSOUBEHGUIDNull() && !x.IsTIMESTOPNull());
                //var unfinishedSOUBEHGUID = dsOdvadeni.Production.Where(x => !x.IsSOUBEHGUIDNull() && !x.IsTIMESTOPNull());

                //var finishedCORRGUID = dsKorekce.Production.Where(x=> !x.IsCORRGUIDNull()
                DateTime dtNow = DateTime.Now;
                DateTime dtStop;    // zadane datum ukonceni
                Decimal dMnozstvi;  // zadane mnozstvi
                if (this.dgVyrobek.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Je možné ukončovat zakázky/korekce pouze po jednom záznamu", this.Text, MessageBoxButtons.OK);
                    return;
                }

                // konec korekce
                if (!rowProduct.IsTIMECORSTARTNull() && !rowProduct.IsTIMECORSTOPNull())
                {
                    MessageBox.Show("Korekce již byla ukončena", this.Text, MessageBoxButtons.OK);
                    return;
                }

                // ukonceni odvadeni
                if (!rowProduct.IsTIMESTARTNull() && (!rowProduct.IsTIMESTOPNull()))
                {
                    MessageBox.Show("Odvádění již bylo dokončeno", this.Text, MessageBoxButtons.OK);
                    return;
                }


                // zacatek odvadeni
                if (!rowProduct.IsTIMESTARTNull() && rowProduct.IsTIMESTOPNull() && !rowProduct.IsSOUBEHGUIDNull())
                {
                    // nacteni vsech zaznamu podle soubehguid
                    //dsOdvadeni.Production.Merge(taProduction.GetDataBySOUBEHGUID(rowProduct.SOUBEHGUID));
                    //var dataOdvedena = taProduction.GetDataBySOUBEHGUID(rowProduct.SOUBEHGUID);

                    Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable dataOdvedena;

                    if ((providerProduction != null) && providerProduction is Fask.Interfaces.Vyroba.Production.IProduction_GetDataBySOUBEHGUID)
                        dataOdvedena = ((Fask.Interfaces.Vyroba.Production.IProduction_GetDataBySOUBEHGUID)providerProduction).Production_GetDataBySOUBEHGUID(rowProduct.SOUBEHGUID);
                    else
                        throw new Exception("IProduction_GetDataBySOUBEHGUID not implementet");

                    // najiti dokonceneho odvadeni
                    var odvodUkoncen = dataOdvedena.Where(x => !x.IsTIMESTOPNull());
                    if (odvodUkoncen.Count() > 0)
                    {
                        MessageBox.Show("Odvádění již bylo dokončeno", this.Text, MessageBoxButtons.OK);
                        return;
                    }
                    else   // dokonceni odvadeni
                    {
                        using (FormUkoncitZakazkuOdvadeni frmUkoncit = new FormUkoncitZakazkuOdvadeni())
                        {
                            frmUkoncit.Text = "Ukončení zakázky";
                            frmUkoncit.rowProduction = rowProduct;
                            if (frmUkoncit.ShowDialog(this) != DialogResult.OK)
                                return;
                            dtStop = frmUkoncit.Datum;
                            dMnozstvi = frmUkoncit.Mnozstvi;
                        }

                        foreach (var item in dataOdvedena)
                        {
                            Fask.Interfaces.DataSets.Vyroba.Production_KonzolaRow nRow = dsOdvadeni.Production_Konzola.NewProduction_KonzolaRow();
                            if (!item.IsCountEntriesNull())
                                nRow.CountEntries = item.CountEntries;
                            if (!item.IsSOPNUMBENull())
                                nRow.SOPNUMBE = item.SOPNUMBE;
                            if (!item.IsITEMNMBRNull())
                                nRow.ITEMNMBR = item.ITEMNMBR;

                            if (!item.IsITEMDESCNull())
                                nRow.ITEMDESC = item.ITEMDESC;

                            if (!item.IsITEMTYPENull())
                                nRow.ITEMTYPE = item.ITEMTYPE;
                            if (!item.IsITEMMJNull())
                                nRow.ITEMMJ = item.ITEMMJ;
                            if (!item.IsORDNull())
                                nRow.ORD = item.ORD; ;
                            if (!item.IsTIMEMODENull())
                                nRow.TIMEMODE = item.TIMEMODE;
                            if (!item.IsTIMEPREPSTARTNull())
                                nRow.TIMEPREPSTART = item.TIMEPREPSTART;
                            if (!item.IsTIMEPREPSTOPNull())
                                nRow.TIMEPREPSTOP = item.TIMEPREPSTOP;
                            if (!item.IsTIMEPREPNull())
                                nRow.TIMEPREP = item.TIMEPREP;
                            if (!item.IsTIMEUNITNull())
                                nRow.TIMEUNIT = item.TIMEUNIT;
                            if (!item.IsTIMESTARTNull())
                                nRow.TIMESTART = item.TIMESTART;
                            nRow.TIMESTOP = dtStop;             // Upravuje se!!
                            //if (!item.IsTIMESTOPNull())
                            //    nRow.TIMESTOP = item.TIMESTOP;
                            if (!item.IsTIMECORSTARTNull())
                                nRow.TIMECORSTART = item.TIMECORSTART;
                            if (!item.IsTIMECORSTOPNull())
                                nRow.TIMECORSTOP = item.TIMECORSTOP;
                            if (!item.IsTIMECORNull())
                                nRow.TIMECOR = item.TIMECOR;
                            if (!item.IsTIMECRIDNull())
                                nRow.TIMECRID = item.TIMECRID;
                            // id neupravovat
                            nRow.loginid = item.loginid;
                            if (!item.IsmachineidNull())
                                nRow.machineid = item.machineid;
                            if (!item.IsoperationidNull())
                                nRow.operationid = item.operationid;
                            // dateeve neupravovat
                            nRow.dateeve = dtStop;      // nastavuje se datum ukončení a ne datum události!!
                            nRow.qtyReal = nRow.qty = dMnozstvi;                  // Upravuje se !!
                            //nRow.qtyReal = item.qtyReal;
                            if (!item.IsQTYPACKNull())
                                nRow.QTYPACK = item.QTYPACK;
                            if (!item.IsQTYPACKMJNull())
                                nRow.QTYPACKMJ = item.QTYPACKMJ;
                            if (!item.IsdescriptionNull())
                                nRow.description = item.description;
                            if (!item.IsBarcodePNull())
                                nRow.BarcodeP = item.BarcodeP;
                            nRow.UserID = item.UserID;
                            nRow.TermID = item.TermID;
                            if (!item.IsISOKNull())
                                nRow.ISOK = item.ISOK;
                            nRow.GUID = Guid.NewGuid();
                            nRow.SOUBEHGUID = item.SOUBEHGUID;
                            // SOUBEHGUID
                            // CORRGUID
                            // porovnani qtyOld oproti predchozimu stavu
                            //if ((_producttype == PRODUCTIONTYPE.ODVOD_STOP) && (rowProduct.IsqtyOldNull()))
                            //    item.qtyOld = Convert.ToDecimal(textBoxQty.Text);
                            //if (!item.IsTIMEPREPSTARTNull())
                            //    nRow.TIMEPREPSTART = dateTimePickerTIMEPREPSTART.Value;
                            //if (!item.IsTIMEPREPSTOPNull())
                            //    nRow.TIMEPREPSTOP = dateTimePickerTIMEPREPSTOP.Value;
                            //if (!item.IsTIMECORSTARTNull())
                            //    nRow.TIMECORSTART = dateTimePickerTIMECORSTART.Value;
                            //if (!item.IsTIMECORSTOPNull())
                            //    nRow.TIMECORSTOP = dateTimePickerTIMECORSTOP.Value;

                            nRow.idVS = FASK.Logins.Uzivatel.Instance.UserID != null ? FASK.Logins.Uzivatel.Instance.UserID : string.Empty;
                            nRow.dateedit = dtNow;
                            //nRow.TIMESTOP = DateTime.Now;   // TODO: pridat doplnovani
                            //nRow.dateedit = dtNow;
                            //nRow.TIMEPREP = 0;
                            //nRow.qty = item.qtyReal = 99;   // TODO: pridat doplnovani
                            //nRow.GUID = Guid.NewGuid();
                            //if (!item.IsSOUBEHGUIDNull())
                            //    nRow.SOUBEHGUID = item.SOUBEHGUID;
                            //nRow.idVS = Globals.Pracovnik != null ? Globals.Pracovnik.id : string.Empty;

                            //dsOdvadeni.Production.ImportRow(nRow);
                            dsOdvadeni.Production_Konzola.AddProduction_KonzolaRow(nRow);
                        }
                    }
                }

                // zacatek korekce
                if (!rowProduct.IsTIMECORSTARTNull() && rowProduct.IsTIMECORSTOPNull() && !rowProduct.IsCORRGUIDNull())
                {
                    //Fask.Interfaces.DataSets.Vyroba.ProductionDataTable dataOdvedena = taProduction.GetDataByCORRGUID(rowProduct.CORRGUID);
                    Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable dataOdvedena;

                    if ((providerProduction != null) && providerProduction is Fask.Interfaces.Vyroba.Production.IProduction_GetDataByCORRGUID)
                        dataOdvedena = ((Fask.Interfaces.Vyroba.Production.IProduction_GetDataByCORRGUID)providerProduction).Production_GetDataByCORRGUID(rowProduct.CORRGUID);
                    else
                        throw new Exception("IProduction_GetDataByCORRGUID not implementet");

                    // najiti dokoncenych korekci
                    var korekceUkoncena = dataOdvedena.Where(x => !x.IsTIMECORSTOPNull());
                    if (korekceUkoncena.Count() > 0)
                    {
                        MessageBox.Show("Korekce již byla dokončena", this.Text, MessageBoxButtons.OK);
                        return;
                    }
                    else   // dokonceni korekce
                    {
                        var item = dataOdvedena.First();
                        using (FormUkoncitZakazkuOdvadeni frmUkoncit = new FormUkoncitZakazkuOdvadeni())
                        {
                            frmUkoncit.Text = "Ukončení korekce";
                            frmUkoncit.rowProduction = item;
                            if (frmUkoncit.ShowDialog(this) != DialogResult.OK)
                                return;
                            dtStop = frmUkoncit.Datum;
                            //dMnozstvi = frmUkoncit.Mnozstvi;
                        }

                        Fask.Interfaces.DataSets.Vyroba.Production_KonzolaRow nRow = dsOdvadeni.Production_Konzola.NewProduction_KonzolaRow();
                        if (!item.IsCountEntriesNull())
                            nRow.CountEntries = item.CountEntries;
                        if (!item.IsSOPNUMBENull())
                            nRow.SOPNUMBE = item.SOPNUMBE;
                        if (!item.IsITEMNMBRNull())
                            nRow.ITEMNMBR = item.ITEMNMBR;
                        if (!item.IsITEMDESCNull())
                            nRow.ITEMDESC = item.ITEMDESC;
                        if (!item.IsITEMTYPENull())
                            nRow.ITEMTYPE = item.ITEMTYPE;
                        if (!item.IsITEMMJNull())
                            nRow.ITEMMJ = item.ITEMMJ;
                        if (!item.IsORDNull())
                            nRow.ORD = item.ORD; ;
                        if (!item.IsTIMEMODENull())
                            nRow.TIMEMODE = item.TIMEMODE;
                        if (!item.IsTIMEPREPSTARTNull())
                            nRow.TIMEPREPSTART = item.TIMEPREPSTART;
                        if (!item.IsTIMEPREPSTOPNull())
                            nRow.TIMEPREPSTOP = item.TIMEPREPSTOP;
                        if (!item.IsTIMEPREPNull())
                            nRow.TIMEPREP = item.TIMEPREP;
                        if (!item.IsTIMEUNITNull())
                            nRow.TIMEUNIT = item.TIMEUNIT;
                        if (!item.IsTIMESTARTNull())
                            nRow.TIMESTART = item.TIMESTART;
                        if (!item.IsTIMESTOPNull())
                            nRow.TIMESTOP = item.TIMESTOP;
                        if (!item.IsTIMECORSTARTNull())
                            nRow.TIMECORSTART = item.TIMECORSTART;
                        //if (!item.IsTIMECORSTOPNull())
                        nRow.TIMECORSTOP = dtStop;    // Upravuje se !!
                        item.TIMECOR = (float)(nRow.TIMECORSTOP - nRow.TIMECORSTART).TotalMinutes;
                        if (!item.IsTIMECORNull())
                            nRow.TIMECOR = item.TIMECOR;
                        if (!item.IsTIMECRIDNull())
                            nRow.TIMECRID = item.TIMECRID;
                        // id neupravovat
                        nRow.loginid = item.loginid;
                        if (!item.IsmachineidNull())
                            nRow.machineid = item.machineid;
                        if (!item.IsoperationidNull())
                            nRow.operationid = item.operationid;
                        // dateeve neupravovat
                        nRow.dateeve = dtStop;        // dateeve nastaveno na datum ukončení korekce!!                      
                        nRow.qty = item.qty;
                        nRow.qtyReal = item.qtyReal;
                        //nRow.qtyReal = item.qtyReal;
                        if (!item.IsQTYPACKNull())
                            nRow.QTYPACK = item.QTYPACK;
                        if (!item.IsQTYPACKMJNull())
                            nRow.QTYPACKMJ = item.QTYPACKMJ;
                        if (!item.IsdescriptionNull())
                            nRow.description = item.description;
                        if (!item.IsBarcodePNull())
                            nRow.BarcodeP = item.BarcodeP;
                        nRow.UserID = item.UserID;
                        nRow.TermID = item.TermID;
                        if (!item.IsISOKNull())
                            nRow.ISOK = item.ISOK;
                        nRow.GUID = Guid.NewGuid();
                        if (!item.IsSOUBEHGUIDNull())
                            nRow.SOUBEHGUID = item.SOUBEHGUID;
                        if (!item.IsCORRGUIDNull())
                            nRow.CORRGUID = item.CORRGUID;
                        // SOUBEHGUID
                        // CORRGUID
                        // porovnani qtyOld oproti predchozimu stavu
                        //if ((_producttype == PRODUCTIONTYPE.ODVOD_STOP) && (rowProduct.IsqtyOldNull()))
                        //    item.qtyOld = Convert.ToDecimal(textBoxQty.Text);
                        //if (!item.IsTIMEPREPSTARTNull())
                        //    nRow.TIMEPREPSTART = dateTimePickerTIMEPREPSTART.Value;
                        //if (!item.IsTIMEPREPSTOPNull())
                        //    nRow.TIMEPREPSTOP = dateTimePickerTIMEPREPSTOP.Value;
                        //if (!item.IsTIMECORSTARTNull())
                        //    nRow.TIMECORSTART = dateTimePickerTIMECORSTART.Value;
                        //if (!item.IsTIMECORSTOPNull())
                        //    nRow.TIMECORSTOP = dateTimePickerTIMECORSTOP.Value;

                        nRow.idVS = FASK.Logins.Uzivatel.Instance.UserID != null ? FASK.Logins.Uzivatel.Instance.UserID : string.Empty;
                        nRow.dateedit = dtNow;
                        //nRow.TIMESTOP = DateTime.Now;   // TODO: pridat doplnovani
                        //nRow.dateedit = dtNow;
                        //nRow.TIMEPREP = 0;
                        //nRow.qty = item.qtyReal = 99;   // TODO: pridat doplnovani
                        //nRow.GUID = Guid.NewGuid();
                        //if (!item.IsSOUBEHGUIDNull())
                        //    nRow.SOUBEHGUID = item.SOUBEHGUID;
                        //nRow.idVS = Globals.Pracovnik != null ? Globals.Pracovnik.id : string.Empty;

                        //dsOdvadeni.Production.ImportRow(nRow);
                        dsOdvadeni.Production_Konzola.AddProduction_KonzolaRow(nRow);
                    }
                }

                //taProduction.Update(dsOdvadeni.Production);

                if ((providerProduction != null) && providerProduction is Fask.Interfaces.Vyroba.Production.IProduction_Update)
                    ((Fask.Interfaces.Vyroba.Production.IProduction_Update)providerProduction).Production_Update(dsOdvadeni.Production_Konzola);
                else
                    throw new Exception("IProduction_Update not implementet");


                dsOdvadeni.AcceptChanges();

                // opetovne vyhledani zaznamu
                //PerformOK();
                PerformVyhledat();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsmiSchvalitVybrane_Click(object sender, EventArgs e)
        {
            try
            {
                if (!opravneniEditace)
                {
                    MessageBox.Show("Nemáte dostatečné oprávnění pro tuto akci!", "Oprávnění", MessageBoxButtons.OK);
                    return;
                }

                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (!FASK.Logins.Uzivatel.Instance.GetPravaKonzole_P_Approval())
                    return;

                if (this.dgVyrobek.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Nejsou vybrány záznamy na schválení", this.Text, MessageBoxButtons.OK);
                    return;
                }

                string msg = string.Format("Opravdu chcete schválit vybrané záznamy? Počet záznamů pro schvalení je: '{0}' ", this.dgVyrobek.SelectedRows.Count);

                if (MessageBox.Show(msg, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;


                if (bw_Schvaleni.IsBusy)
                {
                    bw_Schvaleni.CancelAsync();
                    while (bw_Schvaleni.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }

                ProgressIndicatorVyrobekStart();

                bw_Schvaleni.RunWorkerAsync(this.dgVyrobek.SelectedRows);

            }
            catch (Exception ex)
            {
                ProgressIndicatorVyrobekStop();
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsmiExportovatPrijemku_Click(object sender, EventArgs e)
        {
            Fask.Interfaces.DataSets.Vyroba.ProductionImportRow Row;
            //string SKL_ID;

            using (FormProductionList_SelectCountEntries frm = new FormProductionList_SelectCountEntries())
            {

                //dotahnout producion na zaklade ISOK a grupnut podle countentries a sopnumb

                Fask.Interfaces.DataSets.Vyroba ds;

                if (providerProduction is Fask.Interfaces.Vyroba.Production.IProduction_GetDataSelectListImport)
                    ds = ((Fask.Interfaces.Vyroba.Production.IProduction_GetDataSelectListImport)providerProduction).Production_GetDataSelectListImport();
                else
                    throw new Exception("IProduction_GetDataSelectListImport not implementet");


                frm.AVyrobaDataSet = ds;

                if (frm.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return;

                Row = frm.rowProduction;
                //SKL_ID = frm.rowProduction.SKL_ID;
                //pokracovat v nacitavani dal s vybranou davkou

            }

            PerformImport(Row);
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
        private bool CreateFilter(ref Fask.Interfaces.Filtry.ProductionListFiltr filtr)
        {
            if (filtr == null)
                filtr = new Fask.Interfaces.Filtry.ProductionListFiltr();


            if (dateTimePickerDatumOd.Checked)
            {
                filtr.DatumOdValue = dateTimePickerDatumOd.Value;
            }
            else
                filtr.DatumOdValue = null;

            if (dateTimePickerDatumDo.Checked)
            {
                filtr.DatumDoValue = dateTimePickerDatumDo.Value;
            }
            else
                filtr.DatumDoValue = null;

            ////filtr.DatumDo = dateTimePickerDatumDo.Checked;
            //filtr.DatumDoValue = dateTimePickerDatumDo.Value;
            ////filtr.DatumOd = dateTimePickerDatumOd.Checked;
            //filtr.DatumOdValue = dateTimePickerDatumOd.Value;
            filtr.KorekceNedokoncene = checkBoxKorekceNedokoncene.Checked;
            filtr.KorekceVse = checkBoxKorekceVse.Checked;
            filtr.OdvadeniNedokoncene = checkBoxOdvadeniNedokoncene.Checked;
            filtr.OdvadeniVse = checkBoxOdvadeniVse.Checked;
            filtr.Operace = comboBoxOperace.Text.Trim();

            filtr.PouzeNeschvalene = chb_PouzeNeschvalene.Checked;

            filtr.rowGroups = new Fask.Interfaces.DataSets.Vyroba.GroupsDataTable();
            filtr.rowGroups.ImportRow(rowGroups);

            filtr.rowMachine = new Fask.Interfaces.DataSets.Vyroba.MachinesDataTable();
            filtr.rowMachine.ImportRow(rowMachine);

            filtr.rowOperation = new Fask.Interfaces.DataSets.Vyroba.OperationsDataTable();
            filtr.rowOperation.ImportRow(rowOperation);

            filtr.rowUzivatel = new FASK.Logins.DataSets.Pristupy.FASK_LoginsDataTable();
            filtr.rowUzivatel.ImportRow(rowUzivatel);

            filtr.rowVPH = new Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHDataTable();
            filtr.rowVPH.ImportRow(rowVPH);

            filtr.rowZbozi = new Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_KONZOLADataTable();
            filtr.rowZbozi.ImportRow(rowZbozi);

            filtr.Skupina = comboBoxSkupina.Text.Trim();
            filtr.Stroj = comboBoxStroj.Text.Trim();
            filtr.Uzivatel = comboBoxUzivatel.Text.Trim();
            filtr.VyrobaPouzivatTabulkuZbozi = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba[0].PouzivatTabulkuZbozi;
            filtr.VyrobniPrikaz = comboBoxVyrobniPrikaz.Text.Trim();
            filtr.Zbozi_itemdesc = comboBoxZbozi.Text.Trim();

            filtr.TerminalID = tb_TerminalID.Text;

            #region Time variant IsProcessed

            var v = GetTimeVariantFromFilter();

            if (v.Contains("unknow"))
            {
                filtr.DATEEVE_TimeVariant = null;
            }
            else
            {
                filtr.DATEEVE_TimeVariant = v;
            }

            #endregion

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

                Fask.Interfaces.Filtry.ProductionListFiltr filtr = new Fask.Interfaces.Filtry.ProductionListFiltr();
                string nazev = string.Empty;
                DialogResult dr = Forms.InputBox.Show("Název filtru", "Zadejte název filtru", string.Empty, false, out nazev);
                if (dr != System.Windows.Forms.DialogResult.OK)
                    return;

                string nameFile = Guid.NewGuid().ToString() + "_" + this.GetType().ToString();
                this.dgVyrobek.SaveConfiguration(nameFile);

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

                this.dgVyrobek.SaveConfiguration(nameFile);

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

                comboBoxVyrobniPrikaz.Text =
                    comboBoxZbozi.Text =
                    comboBoxUzivatel.Text =
                    comboBoxSkupina.Text =
                    comboBoxStroj.Text =
                    cb_TimeVariant.Text =
                    comboBoxStroj.Text =
                    comboBoxOperace.Text =
                    string.Empty;


                try
                {

                    comboBoxVyrobniPrikaz.SelectedIndex = -1;
                    comboBoxZbozi.SelectedIndex = -1;
                    comboBoxUzivatel.SelectedIndex = -1;
                    comboBoxSkupina.SelectedIndex = -1;
                    comboBoxStroj.SelectedIndex = -1;
                    comboBoxStroj.SelectedIndex = -1;
                    comboBoxOperace.SelectedIndex = -1;

                    cb_TimeVariant.SelectedIndex = 0;
                }
                catch (System.Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(ex);
                }

                dateTimePickerDatumOd.Checked =
                    dateTimePickerDatumDo.Checked =
                    false;

                checkBoxOdvadeniVse.Checked =
                    checkBoxOdvadeniNedokoncene.Checked =
                    checkBoxKorekceVse.Checked =
                    checkBoxKorekceNedokoncene.Checked =
                    chb_PouzeNeschvalene.Checked =
                    false;

                tb_TerminalID.Text = string.Empty;

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
        public void PerformNastavitFiltr(Fask.Interfaces.Filtry.ProductionListFiltr filtr)
        {
            try
            {
                if (filtr == null)
                    return;

                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                PerformVycistitFiltr();


                if (filtr.rowVPH.Count > 0)
                {
                    rowVPH = filtr.rowVPH[0];
                }
                comboBoxVyrobniPrikaz.Text = filtr.VyrobniPrikaz;

                if (filtr.rowZbozi.Count > 0)
                {
                    rowZbozi = filtr.rowZbozi[0];
                }
                comboBoxZbozi.Text = filtr.Zbozi_itemdesc;

                if (filtr.rowUzivatel.Count > 0)
                {
                    rowUzivatel = filtr.rowUzivatel[0];
                }
                comboBoxUzivatel.Text = filtr.Uzivatel;


                if (filtr.rowGroups.Count > 0)
                {
                    rowGroups = filtr.rowGroups[0];
                }

                comboBoxSkupina.Text = filtr.Skupina;

                if (filtr.DatumOdValue.HasValue)
                {
                    dateTimePickerDatumOd.Checked = true;
                    dateTimePickerDatumOd.Value = filtr.DatumOdValue.Value;
                }

                if (filtr.DatumDoValue.HasValue)
                {
                    dateTimePickerDatumDo.Checked = true;
                    dateTimePickerDatumDo.Value = filtr.DatumDoValue.Value;
                }

                if (!string.IsNullOrEmpty(filtr.DATEEVE_TimeVariant))
                {

                    var arr = filtr.DATEEVE_TimeVariant.Split(';');
                    TimeFilters.TimeVariants TimeVar = (TimeFilters.TimeVariants)Enum.Parse(typeof(TimeFilters.TimeVariants), arr[0], true);

                    TimeVariantName time = new TimeVariantName(TimeVar, arr[1]);

                    cb_TimeVariant.SelectedItem = time;
                }

                if (filtr.rowMachine.Count > 0)
                {
                    rowMachine = filtr.rowMachine[0];
                }
                comboBoxStroj.Text = filtr.Stroj;

                if (filtr.rowOperation.Count > 0)
                {
                    rowOperation = filtr.rowOperation[0];
                }
                comboBoxOperace.Text = filtr.Operace;


                checkBoxOdvadeniVse.Checked = filtr.OdvadeniVse;
                checkBoxOdvadeniNedokoncene.Checked = filtr.OdvadeniNedokoncene;

                checkBoxKorekceVse.Checked = filtr.KorekceVse;
                checkBoxKorekceNedokoncene.Checked = filtr.KorekceNedokoncene;

                chb_PouzeNeschvalene.Checked = filtr.PouzeNeschvalene;

                if (string.IsNullOrEmpty(filtr.NameFileDataGridView))
                    this.dgVyrobek.LoadConfiguration(this.GetType().ToString());
                else
                    this.dgVyrobek.LoadConfiguration(filtr.NameFileDataGridView);

                tb_TerminalID.Text = filtr.TerminalID;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show("Nepodařilo se načíst data filtru.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region BACKGRUND workery pro dotahovani tabulky

        private void bw_Production_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Fask.Interfaces.Filtry.ProductionListFiltr filtr = (Fask.Interfaces.Filtry.ProductionListFiltr)e.Argument;
                Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();
                //Fask.Interfaces.DataSets.SkladLokace ds = new Fask.Interfaces.DataSets.SkladLokace();

                if (bw_Vyrobek.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                //providerVazby.ITEMNMBR_Materialy = rowVyrobek.ITEMNMBR;

                // nacteni dat v oddelenem vlakne
                if ((providerProduction != null) && providerProduction is Fask.Interfaces.Vyroba.Production.IProduction_GetFiltrovanyProductionList)
                    ds = ((Fask.Interfaces.Vyroba.Production.IProduction_GetFiltrovanyProductionList)providerProduction).Production_GetFiltrovanyProductionList(filtr);
                else
                    throw new Exception("IProduction_GetFiltrovanyProductionList not implementet");
                //providerVazby.ITEMNMBR_Materialy = String.Empty;

                if (bw_Vyrobek.CancellationPending)
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

        private void bw_Production_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                // check error, check cancel, then use result
                if (e.Error != null)
                {
                    // handle the error
                    this.dsVyrobek = new Fask.Interfaces.DataSets.Vyroba();
                    bsVyrobek.DataSource = this.dsVyrobek;
                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, e.Error);
                    MessageBox.Show(e.Error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    SetStatusLabelText_Events(-1);

                }
                else if (e.Cancelled)
                {
                    // handle cancellation
                    this.dsVyrobek = new Fask.Interfaces.DataSets.Vyroba();
                    bsVyrobek.DataSource = this.dsVyrobek;

                    SetStatusLabelText_Events(-1);
                }
                else
                {
                    // uspesne dokonceno ...
                    // use it on the UI thread
                    this.dsVyrobek = (Fask.Interfaces.DataSets.Vyroba)e.Result;
                    if (this.dsVyrobek == null)
                        this.dsVyrobek = new Fask.Interfaces.DataSets.Vyroba();

                    bsVyrobek.DataSource = this.dsVyrobek;

                    if (dsVyrobek.Production_Konzola.Count == 0)
                        SetStatusLabelText_Events(-1);
                    else
                    {
                        foreach (DataGridViewRow row in dgVyrobek.SelectedRows)
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
                this.progressIndicatorVyrobek.Location = new Point(this.dgVyrobek.Location.X + (this.dgVyrobek.Width / 2) - (progressIndicatorVyrobek.Size.Width / 2), this.dgVyrobek.Location.Y + (this.dgVyrobek.Height / 2) - (progressIndicatorVyrobek.Size.Height / 2));
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
                Fask.Interfaces.DataSets.Vyroba.ProductionImportRow Row = (Fask.Interfaces.DataSets.Vyroba.ProductionImportRow)e.Argument;

                if (bw_import.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                string status = string.Empty;
                //Zavolani metody s backgroungworkerem....

                if (providerProduction is Fask.Interfaces.Vyroba.Production.IProduction_ImportPrijemkaPohoda_Zdroj_P_PS)
                    status = ((Fask.Interfaces.Vyroba.Production.IProduction_ImportPrijemkaPohoda_Zdroj_P_PS)providerProduction).Production_ImportPrijemkaPohoda_Zdroj_P_PS(Row.CountEntries, Row.SKL_ID, FASK.Logins.Uzivatel.Instance.UserID, null);
                else
                    throw new Exception("IProduction_ImportPrijemkaPohoda not implementet");

                if (string.IsNullOrEmpty(status))
                {
                    e.Cancel = true;
                    e.Result = "Nenalezeno číslo davky";
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
                    Fask.Logging.ExceptionHandler2.Handle(LogLevel.Error, (string)e.Result);
                    MessageBox.Show((string)e.Result, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show(string.Format("Příjemka č.{0} exportována.", (string)e.Result), "Import", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
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

                this.dgVyrobek.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                this.dgVyrobek.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
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

                this.dgVyrobek.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                dgVyrobek.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
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

                dgVyrobek.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                dgVyrobek.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
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
                bool endcol = dgVyrobek.CurrentCell.ColumnIndex + 1 >= dgVyrobek.ColumnCount;
                bool endrow = dgVyrobek.CurrentCell.RowIndex + 1 >= dgVyrobek.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dgVyrobek.CurrentCell.ColumnIndex;
                    startRow = dgVyrobek.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dgVyrobek.CurrentCell.ColumnIndex + 1;
                    startRow = dgVyrobek.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dgVyrobek.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dgVyrobek.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dgVyrobek.CurrentCell = c;
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

            tssl_Production_Count.Text = string.Format("{0}/{1}", index + 1, dsVyrobek.Production_Konzola.Count);
        }

        private void dgVyrobek_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgVyrobek.SelectedRows)
            {
                SetStatusLabelText_Events(row.Index);
            }
        }

        public string GetTimeVariantFromFilter()
        {
            Fask.Interfaces.Classes.TimeVariantName type = (Fask.Interfaces.Classes.TimeVariantName)cb_TimeVariant.SelectedItem;
            return type.ToString_Filter();
        }

        private void cb_TimeVariant_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is ComboBox)
            {

                ComboBox com = (ComboBox)sender;

                if (com.SelectedItem != null)
                {
                    Fask.Interfaces.Classes.TimeVariantName type = (Fask.Interfaces.Classes.TimeVariantName)com.SelectedItem;
                    var v = type.GetTimeVarianta();

                    if (v == Fask.Interfaces.Classes.TimeFilters.TimeVariants.unknow)
                    {
                        dateTimePickerDatumOd.Enabled =
                        dateTimePickerDatumDo.Enabled = true;
                    }
                    else
                    {
                        dateTimePickerDatumOd.Enabled =
                        dateTimePickerDatumDo.Enabled = false;
                    }

                }
                else
                {
                    com.SelectedIndex = 0;
                    dateTimePickerDatumOd.Enabled =
                    dateTimePickerDatumDo.Enabled = true;
                }
            }
        }

        #endregion

        #region BACKGROUND workery pro import

        private void bw_Schvaleni_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                int cnt = 0;

                DataGridViewSelectedRowCollection Rows = (DataGridViewSelectedRowCollection)e.Argument;

                if (bw_import.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }


                #region MyRegion

                Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable dt = new Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable();

                foreach (DataGridViewRow item in Rows)
                {


                    Fask.Interfaces.DataSets.Vyroba.Production_KonzolaRow row = ((item.DataBoundItem as DataRowView).Row) as Fask.Interfaces.DataSets.Vyroba.Production_KonzolaRow;
                    // time stop je vyplnen, je potreba uprava ...
                    if (!row.IsTIMESTOPNull())
                    {
                        if (row.IsqtyOldNull())
                            row.qtyOld = row.qty;
                        row.idVS = FASK.Logins.Uzivatel.Instance.UserID;
                        row.dateedit = DateTime.Now;
                        cnt++;

                    }

                    dt.ImportRow(row);
                }

                dt.AcceptChanges();
                foreach (var item in dt)
                {
                    item.SetModified();
                }

                if ((providerProduction != null) && providerProduction is Fask.Interfaces.Vyroba.Production.IProduction_Update)
                    ((Fask.Interfaces.Vyroba.Production.IProduction_Update)providerProduction).Production_Update(dt);
                else
                    throw new Exception("IProduction_Update not implementet");

                this.dsVyrobek.AcceptChanges();

                #endregion


                if (bw_import.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                e.Result = cnt.ToString();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        private void bw_Schvaleni_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
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
                    MessageBox.Show(string.Format("Schváleno: {0} záznamů.", (string)e.Result), "Schvalení", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
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


        private void tsmiArchivaceVybrane_Click(object sender, EventArgs e)
        {

            if (!opravneniArchivace)
            {
                MessageBox.Show("Nemáte dostatečné oprávnění pro tuto akci!", "Oprávnění", MessageBoxButtons.OK);
                return;
            }
            else
            {
                int pocet_zaznamu = Production_selectedRows.Count;
                string zprava = string.Format("Bude archivováno {0} záznamů. \n\nChceš provést?", pocet_zaznamu);
                //DialogResult result = MessageBox.Show(zprava, "Potvrzení", MessageBoxButtons.YesNo);
                DialogResult result = MessageBox.Show(zprava, "Potvrzení", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        if (Production_selectedRows.Count > 0)
                        {
                            int pocetArchZaznamu = 0;
                            Guid pom = Production_selectedRows[0].GUID;
                            //archivace vybraných řádků
                            foreach (var item in Production_selectedRows)
                            {
                                int id = item.id;
                                pom = item.GUID;

                                //archivuji podle id, poslu do procedury
                                if ((provider != null) && (provider is Fask.Interfaces.IT_cast.IIT_cast_Production_Archivace))
                                {
                                    pocetArchZaznamu = ((Fask.Interfaces.IT_cast.IIT_cast_Production_Archivace)provider).Production_ArchivaceProcedura_guid(pom);
                                }
                                else
                                {
                                    throw new NotImplementedException("Provider neobsahuje implemetaci IIT_cast_Production_Archivace Production_ArchivaceProcedura");
                                }


                            }

                            MessageBox.Show(this, "Akce dokončena. Pocet zaarchivovaných záznamů:" + Production_selectedRows.Count.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);



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
            //else
            //{
            //    MessageBox.Show(this, "Nedostatečné oprávnění k provedení akce archivace záznamů", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

            //}
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

                if (rowProduct == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                #region MaR 6.11.2024 vypis zpl do souboru selected rows
                int index_zaznamu = 0;

                foreach (var item in Production_selectedRows)
                {

                    if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start vyroba/rozbory/prehled vyrobky TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");

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

                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END vyroba/rozbory/prehled vyrobky TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");


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



                    foreach (var item in Production_selectedRows)
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
                    Tisk_selected_rows_Primy_Tisk(plr_Path, true, MN_ToTisk, Production_selectedRows, NazevVychoziTiskarny);

                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Tisk_selected_rows_Primy_Tisk(string plr_Path, bool MnozstvuAutoJedna, int MN_ToTisk, List<Fask.Interfaces.DataSets.Vyroba.Production_KonzolaRow> selected_Rows, string nazevVychTiskarny)
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

                if (rowProduct == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                //if (Production_rowProducts.Count > 1)
                //{
                //    //MessageBox.Show("Je vybráno více záznamů než jeden.", this.Text, MessageBoxButtons.OK);
                //    //return;
                //}

                Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable dt = new Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable();

                foreach (Fask.Interfaces.DataSets.Vyroba.Production_KonzolaRow row in Production_selectedRows)
                {
                    try
                    {
                        Fask.Interfaces.DataSets.Vyroba.Production_KonzolaRow newRow = dt.NewProduction_KonzolaRow();
                        // Ošetřit delší vstupní pole než počet sloupců tabulky
                        int copyLength = Math.Min(row.ItemArray.Length, dt.Columns.Count);
                        newRow.ItemArray = row.ItemArray.Take(copyLength).ToArray();
                        dt.AddProduction_KonzolaRow(newRow);
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

        private void PrintReport_RDLC(Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable dt, bool primyTisk, string path_sablona, string nazevVychTiskarny, int pocetVytisku)
        {
            try
            {

                #region logovani tisku
                if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                {
                    foreach (var item in dt)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start vyroba/rozbory/prehled vyrobky TISK rdlc-------------------------------------");

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

                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END vyroba/rozbory/prehled vyrobky TISK rdlc-------------------------------------");
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

                if (rowProduct == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                #region MaR 6.11.2024 vypis zpl do souboru selected rows
                int index_zaznamu = 0;

                foreach (var item in Production_selectedRows)
                {

                    if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start vyroba/rozbory/prehled vyrobky TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");

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

                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END vyroba/rozbory/prehled vyrobky TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");


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



                    foreach (var item in Production_selectedRows)
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
                    Tisk_selected_rows(plr_Path, true, MN_ToTisk, Production_selectedRows);

                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Tisk_selected_rows(string plr_Path, bool MnozstvuAutoJedna, int MN_ToTisk, List<Fask.Interfaces.DataSets.Vyroba.Production_KonzolaRow> selected_Rows)
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
            Fask.Interfaces.DataSets.Vyroba.Production_KonzolaRow row,
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

                Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable dt = new Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable();

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

        private Dictionary<string, string> PrepareDataToTisk(Fask.Interfaces.DataSets.Vyroba.Production_KonzolaRow row_data)
        {
            try
            {
                while (true)
                {
                    try
                    {
                        Dictionary<string, string> data = new Dictionary<string, string>();


                        data.Add("SOURCE", "Konzola");

                        Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable dt = new Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable();


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
