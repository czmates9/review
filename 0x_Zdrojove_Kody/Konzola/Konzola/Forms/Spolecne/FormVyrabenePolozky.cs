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
using Konzola.Vyroba;

namespace Konzola.Forms.Spolecne
{
    public partial class FormVyrabenePolozky : Form
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
        protected Fask.Interfaces.IMES providerVyrabenePolozky = null;




        /// <summary>
        /// Zvolený Production záznam.
        /// </summary>
        private Fask.Interfaces.DataSets.Hlavni.VyrabenePolozkyRow VyrabenePolozky_selectedRow
        {
            get
            {
                try
                {
                    return ((DataRowView)(dgVyrabenePolozky.BindingContext[this.bsVyrabenePolozky].Current)).Row as Fask.Interfaces.DataSets.Hlavni.VyrabenePolozkyRow;
                }
                catch
                {
                    return null;
                }
            }
        }

        private List<Fask.Interfaces.DataSets.Hlavni.VyrabenePolozkyRow> VyrabenePolozky_selectedRows
        {
            //get
            //{
            //    List<Fask.Interfaces.DataSets.Hlavni.VyrabenePolozkyRow> rows = new List<Fask.Interfaces.DataSets.Hlavni.VyrabenePolozkyRow>();

            //    foreach (DataGridViewRow selectedRow in dgVyrabenePolozky.SelectedRows)
            //    {
            //        Fask.Interfaces.DataSets.Hlavni.VyrabenePolozkyRow row = ((DataRowView)selectedRow.DataBoundItem).Row as Fask.Interfaces.DataSets.Hlavni.VyrabenePolozkyRow;
            //        rows.Add(row);
            //    }

            //    return rows;
            //}

            get
            {
                return dgVyrabenePolozky.SelectedRows
                    .Cast<DataGridViewRow>()
                    .OrderBy(r => r.Index)               // zarovnání na vizuální pořadí v gridu
                    .Select(r => ((DataRowView)r.DataBoundItem).Row as Fask.Interfaces.DataSets.Hlavni.VyrabenePolozkyRow)
                    .Where(r => r != null)
                    .ToList();
            }
        }




        #region Parametry Filtry


        /// <summary>
        /// Seznam vsech nactenych filtru.
        /// </summary>
        private List<Fask.Interfaces.Filtry.VyrabenePolozkyFiltr> filtr = new List<Fask.Interfaces.Filtry.VyrabenePolozkyFiltr>();

        /// <summary>
        /// Vybrany filtr.
        /// </summary>
        private Fask.Interfaces.Filtry.VyrabenePolozkyFiltr rowFiltr
        {
            get
            {
                try
                {
                    return tscbFiltry.SelectedItem as Fask.Interfaces.Filtry.VyrabenePolozkyFiltr;
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
        public FormVyrabenePolozky()
        {
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);

                

            InitializeComponent();
            this.dgVyrabenePolozky.UpdateColumnHeaderCellsByDatasource();

            menuStrip1.Items.Remove(tsmiAkce);
            tsmiAkce.DropDownItems.Remove(tsmiArchivaceVybrane);


            panelButtons.Menu = menuStrip1;
        }

        public FormVyrabenePolozky(bool opravneni)
        {
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);

          

            InitializeComponent();
            this.dgVyrabenePolozky.UpdateColumnHeaderCellsByDatasource();


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

        public FormVyrabenePolozky(Opravneni opravneni)
        {
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);



            InitializeComponent();
            this.dgVyrabenePolozky.UpdateColumnHeaderCellsByDatasource();


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


                //DISABLE PRVKY MaR 20.12. 2024
                if (true)
                {
                    //tiskEtiketToolStripMenuItem.Enabled = true;
                    tsmiArchivaceVybrane.Enabled = false;
                    tsmiExportovatPrijemku.Enabled = false;
                    tsmiSchvalitVybrane.Enabled = false;
                    tsmiUkoncitZakazkuKorekci.Enabled = false;
                    tsmiUpravit.Enabled = false;
                    //tsmiExportDoCSVVse.Enabled = false;
                    //tsmiExportDoCSVOznacene.Enabled = false;
                    //tsmiExportDoExcelVse.Enabled = false;
                    //tsmiExportDoExceOznacene.Enabled = false;
                    //tsmiExportDoXMLVse.Enabled = false;
                    //tsmiExportDoXMLOznacene.Enabled = false;


                }





                this.ShowIcon = false;
                this.WindowState = FormWindowState.Maximized;

                // načtení konfigurace datagridu z nastavení aplikace
                this.dgVyrabenePolozky.LoadConfiguration(this.GetType().ToString());

                panelButtons.LoadConfiguration(this.GetType().ToString());
                panelButtons.Init();
                panelButtons.Size = new Size(85, 700);

                advancedDataGridViewSearchToolBar1.SetColumns(dgVyrabenePolozky.Columns);

             

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

                

                //if ((providerLogins != null) && (providerLogins is Fask.Interfaces.Vyroba.Login.ILogin_FillLogin))
                //    ((Fask.Interfaces.Vyroba.Login.ILogin_FillLogin)providerLogins).FillLogin(this.vyrobaDataSet1);
                //else
                //    throw new NotImplementedException("Neimplementovan provider pro ILogin_FillLogin");

                var Logins = FASK.Logins.Uzivatel.Instance.Komunikace.GetLogins();


                //// načtení konfigurace vytvořených filtrů
                this.filtr = Extensions.ListExt.ReadFromXML<Fask.Interfaces.Filtry.VyrabenePolozkyFiltr>(this.GetType().ToString() + ".filtr");
                tscbFiltry.ComboBox.DataSource = this.filtr;
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
                this.dgVyrabenePolozky.SaveConfiguration(this.GetType().ToString());
                panelButtons.SaveConfiguration(this.GetType().ToString());

                this.filtr.WriteXML(this.GetType().ToString() + ".filtr");
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

            #region Vyrabene polozky
            try
            {
                if (String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                    return;

                if (providerVyrabenePolozky == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Providers.IVyrabenePolozky2).IsAssignableFrom(t))
                            {
                                providerVyrabenePolozky = (Fask.Interfaces.Providers.IVyrabenePolozky2)providerAssemlby.CreateInstance(t.FullName);
                                if (providerProduction != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                providerVyrabenePolozky.InitProvider();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion


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
            //        //this.dgVyrabenePolozky.DataSource = null;
            //    }
            //    else
            //    {
            //        textBoxZbozi.Text = rowVPP.ITEMNMBR;

            //        var adapter = new Production.DataServices.VyrobaDataSetTableAdapters.ProductionTableAdapter();
            //        adapter.Connection.ConnectionString = Properties.Settings.Default.Production_ConncetionString;
            //        var dataVPP = adapter.GetDataByCountEntriesAndSOPNUMBEandITEMNMBR(rowVPH.CountEntries, rowVPH.SOPNUMBE, rowVPP.ITEMNMBR);
            //        this.bindingSourceProduct.DataSource = dataVPP;
            //        //this.dgVyrabenePolozky.DataSource = dataVPP;
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
                if (this.dgVyrabenePolozky.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Je možné upravovat pouze po jednom záznamu", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if (VyrabenePolozky_selectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro úpravu", this.Text, MessageBoxButtons.OK);
                    return;
                }

            

                using (FormProductionEdit frmuziv = new FormProductionEdit())
                {
                    //frmuziv.VyrabenePolozky_selectedRow = VyrabenePolozky_selectedRow;
                    //frmuziv.Text = "Úprava výroby";
                    //if (frmuziv.ShowDialog(this) != DialogResult.OK)
                    //    return;

                    ////var lta = new Production.DataServices.VyrobaDataSetTableAdapters.ProductionTableAdapter();
                    ////lta.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
                    //////lta.Delete(SelectedRow.ITEMNMBR);
                    ////lta.Update(frmuziv.VyrabenePolozky_selectedRow);
                    //////lta.Update(this.vyrobaDataSet1.Production);


                    //if ((providerProduction != null) && providerProduction is Fask.Interfaces.Vyroba.Production.IProduction_Update_Row)
                    //    ((Fask.Interfaces.Vyroba.Production.IProduction_Update_Row)providerProduction).Production_Update_Row(frmuziv.VyrabenePolozky_selectedRow);
                    //else
                    //    throw new Exception("IProduction_Update_Row not implementet");

                    this.dsVyrabenePolozky.AcceptChanges();
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

        #region old mar 17.1.2025
        ///// <summary>
        ///// Metoda pro dotaženi dat podle filtru
        ///// </summary>
        //private void PerformVyhledat()
        //{
        //    try
        //    {
        //        DataTable dtchanged = this.dsVyrabenePolozky.VyrabenePolozky.GetChanges();
        //        if (dtchanged != null && dtchanged.Rows.Count > 0) //obsahuje zmeny ... 
        //        {
        //            DialogResult dr = MessageBox.Show("Obsahuje změny, pokračovat?", "data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        //            if (dr == System.Windows.Forms.DialogResult.No)
        //                return;
        //        }

        //        if (bw_VyrabenePolozky.IsBusy)
        //        {
        //            bw_VyrabenePolozky.CancelAsync();
        //            while (bw_VyrabenePolozky.IsBusy)
        //            {
        //                Application.DoEvents();
        //            }
        //        }

        //        ProgressIndicatorVyrobekStart();

        //        Fask.Interfaces.Filtry.VyrabenePolozkyFiltr filtr = new Fask.Interfaces.Filtry.VyrabenePolozkyFiltr();
        //        if (!CreateFilter(ref filtr)) //rowFiltr
        //            return;




        //        int FirstDisplayedScrollingRowIndex = this.dgVyrabenePolozky.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

        //        bw_VyrabenePolozky.RunWorkerAsync(filtr);

        //        if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dgVyrabenePolozky.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dgVyrabenePolozky.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index
        //    }
        //    catch (Exception ex)
        //    {
        //        ProgressIndicatorVyrobekStop();
        //        Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
        //        MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }

        //} 

        #endregion
        /// <summary>
        /// Metoda pro dotažení dat podle filtru
        /// </summary>
        private void PerformVyhledat()
        {
            try
            {
                DataTable dtchanged = this.dsVyrabenePolozky.VyrabenePolozky.GetChanges();
                if (dtchanged != null && dtchanged.Rows.Count > 0) //obsahuje změny ...
                {
                    DialogResult dr = MessageBox.Show("Obsahuje změny, pokračovat?", "data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == System.Windows.Forms.DialogResult.No)
                        return;
                }

                if (bw_VyrabenePolozky.IsBusy)
                {
                    bw_VyrabenePolozky.CancelAsync();
                    while (bw_VyrabenePolozky.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }

                ProgressIndicatorVyrobekStart();

                // Inicializace filtru
                Fask.Interfaces.Filtry.VyrabenePolozkyFiltr filtr = null;

                // Vytvoření filtru
                if (!CreateFilter(ref filtr)) // rowFiltr
                    return;

                // Kontrola inicializace filtru
                if (filtr == null)
                {
                    throw new InvalidOperationException("Filtr nebyl správně inicializován.");
                }

                int FirstDisplayedScrollingRowIndex = this.dgVyrabenePolozky.FirstDisplayedScrollingRowIndex; // Save Current Scroll Index

                bw_VyrabenePolozky.RunWorkerAsync(filtr);

                if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dgVyrabenePolozky.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex))
                    this.dgVyrabenePolozky.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; // Restore Scroll Index
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
           

            #region Práce s DataGridView - nastavení formátování
            try
            {
                foreach (DataGridViewColumn column in dgVyrabenePolozky.Columns)
                {
                    Type dataType = null;

                    // Zjištění typu datového sloupce
                    if (dgVyrabenePolozky.DataSource is DataTable dataTable)
                    {
                        if (dataTable.Columns.Contains(column.DataPropertyName))
                        {
                            dataType = dataTable.Columns[column.DataPropertyName].DataType;
                        }
                    }
                    else if (dgVyrabenePolozky.DataSource is BindingSource bindingSource)
                    {
                        if (bindingSource.DataSource is DataTable bindingDataTable)
                        {
                            if (bindingDataTable.Columns.Contains(column.DataPropertyName))
                            {
                                dataType = bindingDataTable.Columns[column.DataPropertyName].DataType;
                            }
                        }
                        else if (bindingSource.DataSource is DataSet dataSet)
                        {
                            DataTable dataTableFromSet = dataSet.Tables[bindingSource.DataMember];
                            if (dataTableFromSet.Columns.Contains(column.DataPropertyName))
                            {
                                dataType = dataTableFromSet.Columns[column.DataPropertyName].DataType;
                            }
                        }
                    }

                    // Pokud není dataType nalezeno, použijeme výchozí nastavení nebo přeskočíme
                    if (dataType == null)
                    {
                        // Zalogujte nebo přeskočte sloupec, pokud typ dat není nalezen
                        Fask.Logging.ExceptionHandler2.Handle(new Exception($"Nebyl nalezen typ dat pro sloupec {column.Name}."));
                        continue;
                    }

                    // Nastavení zarovnání pro číselné typy
                    if (dataType.Name == "Int32" || dataType.Name == "Decimal")
                    {
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }

                    // Nastavení počtu desetinných míst pro typ Decimal
                    if (dataType.Name == "Decimal")
                    {
                        if (!string.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Rozbory[0].PocetDesetinnychMist) &&
                            int.TryParse(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Rozbory[0].PocetDesetinnychMist.Replace("N", ""), out int decimalPlaces))
                        {
                            column.DefaultCellStyle.Format = $"N{decimalPlaces}";
                        }
                        else
                        {
                            // Pokud není nastaveno, použijeme výchozí hodnotu
                            column.DefaultCellStyle.Format = "N5";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Logování výjimky s podrobnostmi
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show($"Chyba při nastavení formátování: {ex.Message}");
            }
            #endregion

            bool work = true;

            work =   ValidaceVstupUzivatel();

            if (work)
            {
                //PerformOK();
                PerformVyhledat();
            }
          







        }

        private bool ValidaceVstupUzivatel()
        {
            try
            {
                // Předpokládejme, že vstupní hodnoty jsou v proměnných tB_DateProd_OD.Text a tB_DateProd_DO.Text
                string vstup_OD = tB_DateProd_OD.Text;
                string vstup_DO = tB_DateProd_DO.Text;
                bool vysledek = true;

                // Validace vstupu OD
                if (!string.IsNullOrWhiteSpace(vstup_OD))
                {
                    if (short.TryParse(vstup_OD, out short result))
                    {
                        if (result < 0)
                        {
                            // Pokud je číslo záporné, zobrazí dialog a vrátí false
                            MessageBox.Show("Zadaná hodnota Očekávane datum výroby OD musí být kladné celé číslo do 32 767.", "Neplatná hodnota", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            vysledek = false;
                            return vysledek;
                        }
                    }
                    else
                    {
                        // musí být kladné celé číslo do 32 767.
                        // Pokud není platné celé číslo smallint, zobrazí dialog a vrátí false
                        MessageBox.Show("Zadaná hodnota Očekávane datum výroby OD musí být kladné celé číslo do 32 767.", "Neplatná hodnota", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        vysledek = false;
                        return vysledek;
                    }
                }

                // Validace vstupu DO
                if (!string.IsNullOrWhiteSpace(vstup_DO))
                {
                    if (short.TryParse(vstup_DO, out short result))
                    {
                        if (result < 0)
                        {
                            // Pokud je číslo záporné, zobrazí dialog a vrátí false
                            MessageBox.Show("Zadaná hodnota Očekávane datum výroby DO musí být kladné číslo nebo nula.", "Neplatná hodnota", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            vysledek = false;
                            return vysledek;
                        }
                    }
                    else
                    {
                        // Pokud není platné celé číslo smallint, zobrazí dialog a vrátí false
                        MessageBox.Show("Zadaná hodnota Očekávane datum výroby DO není platné celé číslo typu smallint.", "Neplatná hodnota", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        vysledek = false;
                        return vysledek;
                    }
                }

                return vysledek;
            }
            catch (Exception ex)
            {
                // Zpracování výjimek (volitelně zde přidat logování)
                MessageBox.Show($"Došlo k neočekávané chybě: {ex.Message}", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }



        private void buttonOdznacitVse_Click(object sender, EventArgs e)
        {
            try
            {
                this.dgVyrabenePolozky.ClearSelection();
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
                this.dgVyrabenePolozky.SelectAll();
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

                if (this.dgVyrabenePolozky.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Je možné upravovat pouze po jednom záznamu", this.Text, MessageBoxButtons.OK);
                    return;
                }


                if (VyrabenePolozky_selectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro úpravu", this.Text, MessageBoxButtons.OK);
                    return;
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

                if (VyrabenePolozky_selectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro ukončení", this.Text, MessageBoxButtons.OK);
                    return;
                }

                //List<Fask.Interfaces.DataSets.Vyroba.ProductionRow> listProduction = new List<DataServices.VyrobaDataSet.ProductionRow>();

                //// výběr veškerých SOUBEHGUID a CORRGUID            
                //foreach (var item in this.dgVyrabenePolozky.SelectedRows)
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
                if (this.dgVyrabenePolozky.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Je možné ukončovat zakázky/korekce pouze po jednom záznamu", this.Text, MessageBoxButtons.OK);
                    return;
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
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (!FASK.Logins.Uzivatel.Instance.GetPravaKonzole_P_Approval())
                    return;

                if (this.dgVyrabenePolozky.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Nejsou vybrány záznamy na schválení", this.Text, MessageBoxButtons.OK);
                    return;
                }

                string msg = string.Format("Opravdu chcete schválit vybrané záznamy? Počet záznamů pro schvalení je: '{0}' ", this.dgVyrabenePolozky.SelectedRows.Count);

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

                bw_Schvaleni.RunWorkerAsync(this.dgVyrabenePolozky.SelectedRows);

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
            //Fask.Interfaces.DataSets.Vyroba.ProductionImportRow Row;
            ////string SKL_ID;

            //using (FormProductionList_SelectCountEntries frm = new FormProductionList_SelectCountEntries())
            //{

            //    //dotahnout producion na zaklade ISOK a grupnut podle countentries a sopnumb

            //    Fask.Interfaces.DataSets.Vyroba ds;

            //    if (providerProduction is Fask.Interfaces.Vyroba.Production.IProduction_GetDataSelectListImport)
            //        ds = ((Fask.Interfaces.Vyroba.Production.IProduction_GetDataSelectListImport)providerProduction).Production_GetDataSelectListImport();
            //    else
            //        throw new Exception("IProduction_GetDataSelectListImport not implementet");


            //    frm.AVyrobaDataSet = ds;

            //    if (frm.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            //        return;

            //    Row = frm.VyrabenePolozky_selectedRowion;

            //    //SKL_ID = frm.VyrabenePolozky_selectedRowion.SKL_ID;
            //    //pokracovat v nacitavani dal s vybranou davkou

            //}

            //PerformImport(Row);
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
        /// Vytvoření filtru pro dotažení dat.
        /// </summary>
        /// <param name="filtr"></param>
        /// <returns></returns>
        private bool CreateFilter(ref Fask.Interfaces.Filtry.VyrabenePolozkyFiltr filtr)
        {
            // Zajištění inicializace filtru
            if (filtr == null)
                filtr = new Fask.Interfaces.Filtry.VyrabenePolozkyFiltr();

            // Použití InvokeRequired pro kontrolu vlákna
            if (InvokeRequired)
            {
                // Použití lokální proměnné místo ref parametru
                var tempFiltr = filtr;
                Invoke(new Action(() => {
                    SetFilterValues(tempFiltr);
                }));
                filtr = tempFiltr; // Aktualizace ref parametru
            }
            else
            {
                SetFilterValues(filtr);
            }

            return true;
        }

        /// <summary>
        /// Nastaví hodnoty do filtru z UI.
        /// </summary>
        /// <param name="filtr"></param>
        private void SetFilterValues(Fask.Interfaces.Filtry.VyrabenePolozkyFiltr filtr)
        {
            //filtr.DatumOdValue = dateTimePickerDatumOd.Checked ? dateTimePickerDatumOd.Value : (DateTime?)null;
            //filtr.DatumDoValue = dateTimePickerDatumDo.Checked ? dateTimePickerDatumDo.Value : (DateTime?)null;
          




            filtr.Aktivni = chB_Aktivni.Checked;
            filtr.Neaktivni = chB_Neaktivni.Checked;
            filtr.Ukonceno = chB_Ukonceno.Checked;

            //if (cB_Active.SelectedItem != null)
            //{
            //    filtr.Active = (byte)(int)cB_Active.SelectedItem;
            //}


            //if (cB_Active.SelectedItem != null)
            //{
            //    // Ověření, že SelectedItem je validní celé číslo a v rozsahu BYTE (0-255)
            //    if (int.TryParse(cB_Active.SelectedItem.ToString(), out int activeValue))
            //    {
            //        // Zajistíme, že hodnota je v rozsahu BYTE
            //        if (activeValue >= 0 && activeValue <= 255)
            //        {
            //            filtr.Active = (byte)activeValue; // Bezpečná konverze z int na byte
            //        }
            //        else
            //        {
            //            throw new ArgumentOutOfRangeException(nameof(cB_Active.SelectedItem), "Hodnota SelectedItem není v rozsahu BYTE (0-255).");
            //        }
            //    }
            //    else
            //    {
            //        throw new ArgumentException("Hodnota SelectedItem není platné celé číslo.", nameof(cB_Active.SelectedItem));
            //    }
            //}

            //if (cB_Active.SelectedItem != null)
            //{
            //    try
            //    {
            //        // Přímé ověření a konverze z enum na číslo
            //        var selectedEnum = (VyrobaStavPrikazu)cB_Active.SelectedItem;

            //        // Převedeme enum na číselnou hodnotu
            //        int activeValue = (int)selectedEnum;

            //        // Validace rozsahu hodnoty (BYTE: 0-255)
            //        if (activeValue >= 0 && activeValue <= 255)
            //        {
            //            filtr.Active = (byte)activeValue; // Bezpečná konverze na byte
            //        }
            //        else
            //        {
            //            throw new ArgumentOutOfRangeException(nameof(cB_Active.SelectedItem), "Hodnota enum není v rozsahu BYTE (0-255).");
            //        }
            //    }
            //    catch (InvalidCastException)
            //    {
            //        throw new ArgumentException("Hodnota SelectedItem není platná hodnota VyrobaStavPrikazu.", nameof(cB_Active.SelectedItem));
            //    }
            //}
            //else
            //{
            //    throw new ArgumentNullException(nameof(cB_Active.SelectedItem), "Hodnota SelectedItem je null.");
            //}



            filtr.JenNezrealizovane = chB_Nezrealizovane.Checked;




            filtr.CountEntries = string.IsNullOrEmpty(tB_CountEntries.Text) ? null : tB_CountEntries.Text.Trim();
            filtr.CountEntries_VPP = string.IsNullOrEmpty(tB_CountEntries_VPP.Text) ? null : tB_CountEntries_VPP.Text.Trim();


            filtr.DateProd_OD = string.IsNullOrEmpty(tB_DateProd_OD.Text) ? (short?)null : short.Parse(tB_DateProd_OD.Text.Trim());
            filtr.DateProd_DO = string.IsNullOrEmpty(tB_DateProd_DO.Text) ? (short?)null : short.Parse(tB_DateProd_DO.Text.Trim());



            filtr.ITEMDESC = string.IsNullOrEmpty(tB_ITEMDESC.Text) ? null : tB_ITEMDESC.Text.Trim();
            filtr.VNDITNUM = string.IsNullOrEmpty(tB_VNDITNUM.Text) ? null : tB_VNDITNUM.Text.Trim();
            filtr.SOPNUMBE = string.IsNullOrEmpty(tB_SOPNUMBE.Text) ? null : tB_SOPNUMBE.Text.Trim();

           
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

                filtr.Remove(rowFiltr);
                this.tscbFiltry.ComboBox.DataSource = null;
                this.tscbFiltry.ComboBox.DataSource = filtr;
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

                Fask.Interfaces.Filtry.VyrabenePolozkyFiltr filtr = new Fask.Interfaces.Filtry.VyrabenePolozkyFiltr();
                string nazev = string.Empty;
                DialogResult dr = Forms.InputBox.Show("Název filtru", "Zadejte název filtru", string.Empty, false, out nazev);
                if (dr != System.Windows.Forms.DialogResult.OK)
                    return;

                string nameFile = Guid.NewGuid().ToString() + "_" + this.GetType().ToString();
                this.dgVyrabenePolozky.SaveConfiguration(nameFile);

                bool result = CreateFilter(ref filtr);
                filtr.NameFileDataGridView = nameFile;

                if (result)
                {
                    filtr.NazevFiltru = nazev;
                    this.filtr.Add(filtr);
                    //this.tscbFiltry.Items.Clear();
                    this.tscbFiltry.ComboBox.DataSource = null;
                    this.tscbFiltry.ComboBox.DataSource = this.filtr;
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

                this.dgVyrabenePolozky.SaveConfiguration(nameFile);

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

               

            

                try
                {
                    tB_CountEntries.Text =
                   tB_CountEntries_VPP.Text =
                   tB_DateProd_OD.Text =
                   tB_ITEMDESC.Text =
                   tB_SOPNUMBE.Text =
                   tB_VNDITNUM.Text =
                   string.Empty;

                    //cB_Active.SelectedIndex = -1;

                    chB_Aktivni.Checked = false;
                    chB_Neaktivni.Checked = false;
                    chB_Ukonceno.Checked = false;
                    chB_Nezrealizovane.Checked = false;

                  
                }
                catch (System.Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(ex);
                }

            

                

                tB_VNDITNUM.Text = string.Empty;

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
        public void PerformNastavitFiltr(Fask.Interfaces.Filtry.VyrabenePolozkyFiltr filtr)
        {
            try
            {
                if (filtr == null)
                    return;

                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                PerformVycistitFiltr();

                //if (filtr.Active.HasValue)
                //{
                //    switch (filtr.Active.Value)
                //    {
                //        case 1:
                //            cB_Active.SelectedItem = Fask.Interfaces.Classes.VyrobaStavPrikazu.Aktivni;
                //            break;
                //        case 0:
                //            cB_Active.SelectedItem = Fask.Interfaces.Classes.VyrobaStavPrikazu.Neaktivni;
                //            break;
                //        case 200:
                //            cB_Active.SelectedItem = Fask.Interfaces.Classes.VyrobaStavPrikazu.Ukoncen;
                //            break;
                //        default:
                //            cB_Active.SelectedIndex = -1;
                //            break;
                //    }
                //}

                if (!string.IsNullOrEmpty( filtr.CountEntries)) 
                {
                    tB_CountEntries.Text = filtr.CountEntries;
                }

                if (!string.IsNullOrEmpty(filtr.CountEntries_VPP))
                {
                    tB_CountEntries_VPP.Text = filtr.CountEntries_VPP;
                }

                if (!string.IsNullOrEmpty(filtr.DateProd))
                {
                    tB_DateProd_OD.Text = filtr.DateProd;
                }

                if (!string.IsNullOrEmpty(filtr.SOPNUMBE))
                {
                    tB_SOPNUMBE.Text = filtr.SOPNUMBE;
                }

                if (!string.IsNullOrEmpty(filtr.ITEMDESC))
                {
                    tB_ITEMDESC.Text = filtr.ITEMDESC;
                }

                if (!string.IsNullOrEmpty(filtr.VNDITNUM))
                {
                    tB_VNDITNUM.Text = filtr.VNDITNUM;
                }



              

                if (!string.IsNullOrEmpty(filtr.DATEEVE_TimeVariant))
                {

                    var arr = filtr.DATEEVE_TimeVariant.Split(';');
                    TimeFilters.TimeVariants TimeVar = (TimeFilters.TimeVariants)Enum.Parse(typeof(TimeFilters.TimeVariants), arr[0], true);

                    TimeVariantName time = new TimeVariantName(TimeVar, arr[1]);

                    
                }


              
                    chB_Aktivni.Checked = filtr.Aktivni;

                    chB_Neaktivni.Checked = filtr.Neaktivni;
                
                    chB_Ukonceno.Checked = filtr.Ukonceno;

                chB_Nezrealizovane.Checked = filtr.JenNezrealizovane;




                if (string.IsNullOrEmpty(filtr.NameFileDataGridView))
                    this.dgVyrabenePolozky.LoadConfiguration(this.GetType().ToString());
                else
                    this.dgVyrabenePolozky.LoadConfiguration(filtr.NameFileDataGridView);

            
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show("Nepodařilo se načíst data filtru.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region BACKGRUND workery pro dotahovani tabulky

        #region new 17.1.2025 MaR

        private void bw_Production_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                // Inicializace datové sady
                Fask.Interfaces.DataSets.Hlavni ds = new Fask.Interfaces.DataSets.Hlavni();
                
                // Kontrola zrušení procesu
                if (bw_VyrabenePolozky.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                // Zajištění platného filtru
                if (rowFiltr == null)
                {
                    Fask.Interfaces.Filtry.VyrabenePolozkyFiltr rowFilter = new Fask.Interfaces.Filtry.VyrabenePolozkyFiltr();
                    CreateFilter(ref rowFilter); // Naplnění filtru hodnotami z UI

                    // Kontrola poskytovatele a načtení dat
                    if ((providerVyrabenePolozky != null) && providerVyrabenePolozky is Fask.Interfaces.Providers.IVyrabenePolozky2_Get)
                    {
                        ds = ((Fask.Interfaces.Providers.IVyrabenePolozky2_Get)providerVyrabenePolozky).GetVyrabenePolozkyFiltrovane(rowFilter);
                    }
                    else
                    {
                        throw new Exception("IVyrabenePolozky2_Get not implemented");
                    }

                }
                else
                {
                    // Kontrola poskytovatele a načtení dat
                    if ((providerVyrabenePolozky != null) && providerVyrabenePolozky is Fask.Interfaces.Providers.IVyrabenePolozky2_Get)
                    {
                        ds = ((Fask.Interfaces.Providers.IVyrabenePolozky2_Get)providerVyrabenePolozky).GetVyrabenePolozkyFiltrovane(rowFiltr);
                    }
                    else
                    {
                        throw new Exception("IVyrabenePolozky2_Get not implemented");
                    }
                }

               


                // Kontrola zrušení procesu
                if (bw_VyrabenePolozky.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                // Uložení výsledků
                e.Result = ds;

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion




        private void bw_Production_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                // check error, check cancel, then use result
                if (e.Error != null)
                {
                    // handle the error
                    this.dsVyrabenePolozky = new Fask.Interfaces.DataSets.Hlavni();// Fask.Interfaces.DataSets.Vyroba();
                    bsVyrabenePolozky.DataSource = this.dsVyrabenePolozky;
                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, e.Error);
                    MessageBox.Show(e.Error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    SetStatusLabelText_Events(-1);

                }
                else if (e.Cancelled)
                {
                    // handle cancellation
                    this.dsVyrabenePolozky = new Fask.Interfaces.DataSets.Hlavni();
                    bsVyrabenePolozky.DataSource = this.dsVyrabenePolozky;

                    SetStatusLabelText_Events(-1);
                }
                else
                {
                    // uspesne dokonceno ...
                    // use it on the UI thread
                    this.dsVyrabenePolozky = (Fask.Interfaces.DataSets.Hlavni)e.Result;
                    if (this.dsVyrabenePolozky == null)
                        this.dsVyrabenePolozky = new Fask.Interfaces.DataSets.Hlavni();

                    bsVyrabenePolozky.DataSource = this.dsVyrabenePolozky;

                    if (dsVyrabenePolozky.VyrabenePolozky.Count == 0)
                        SetStatusLabelText_Events(-1);
                    else
                    {
                        foreach (DataGridViewRow row in dgVyrabenePolozky.SelectedRows)
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
                this.progressIndicatorVyrobek.Location = new Point(this.dgVyrabenePolozky.Location.X + (this.dgVyrabenePolozky.Width / 2) - (progressIndicatorVyrobek.Size.Width / 2), this.dgVyrabenePolozky.Location.Y + (this.dgVyrabenePolozky.Height / 2) - (progressIndicatorVyrobek.Size.Height / 2));
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

                this.dgVyrabenePolozky.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                this.dgVyrabenePolozky.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
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

                this.dgVyrabenePolozky.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                dgVyrabenePolozky.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
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

                dgVyrabenePolozky.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                dgVyrabenePolozky.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
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
                bool endcol = dgVyrabenePolozky.CurrentCell.ColumnIndex + 1 >= dgVyrabenePolozky.ColumnCount;
                bool endrow = dgVyrabenePolozky.CurrentCell.RowIndex + 1 >= dgVyrabenePolozky.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dgVyrabenePolozky.CurrentCell.ColumnIndex;
                    startRow = dgVyrabenePolozky.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dgVyrabenePolozky.CurrentCell.ColumnIndex + 1;
                    startRow = dgVyrabenePolozky.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dgVyrabenePolozky.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dgVyrabenePolozky.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dgVyrabenePolozky.CurrentCell = c;
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

            tssl_Production_Count.Text = string.Format("{0}/{1}", index + 1, dsVyrabenePolozky.VyrabenePolozky.Count);
        }

        private void dgVyrobek_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgVyrabenePolozky.SelectedRows)
            {
                SetStatusLabelText_Events(row.Index);
            }
        }


        private void cb_TimeVariant_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is ComboBox)
            {

                ComboBox com = (ComboBox)sender;

              
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

                Fask.Interfaces.DataSets.Hlavni.VyrabenePolozkyDataTable dt = new Fask.Interfaces.DataSets.Hlavni.VyrabenePolozkyDataTable();

                foreach (DataGridViewRow item in Rows)
                {


                    Fask.Interfaces.DataSets.Hlavni.VyrabenePolozkyRow row = ((item.DataBoundItem as DataRowView).Row) as Fask.Interfaces.DataSets.Hlavni.VyrabenePolozkyRow;
                    // time stop je vyplnen, je potreba uprava ...
                    //if (!row.IsTIMESTOPNull())
                    //{
                    //    if (row.IsqtyOldNull())
                    //        row.qtyOld = row.qty;
                    //    row.idVS = FASK.Logins.Uzivatel.Instance.UserID;
                    //    row.dateedit = DateTime.Now;
                    //    cnt++;

                    //}

                    dt.ImportRow(row);
                }

                dt.AcceptChanges();
                foreach (var item in dt)
                {
                    item.SetModified();
                }

                //if ((providerProduction != null) && providerProduction is Fask.Interfaces.Vyroba.Production.IProduction_Update)
                //    ((Fask.Interfaces.Vyroba.Production.IProduction_Update)providerProduction).Production_Update(dt);
                //else
                //    throw new Exception("IProduction_Update not implementet");

                this.dsVyrabenePolozky.AcceptChanges();

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
                int pocet_zaznamu = VyrabenePolozky_selectedRows.Count;
                string zprava = string.Format("Bude archivováno {0} záznamů. \n\nChceš provést?", pocet_zaznamu);
                //DialogResult result = MessageBox.Show(zprava, "Potvrzení", MessageBoxButtons.YesNo);
                DialogResult result = MessageBox.Show(zprava, "Potvrzení", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        if (VyrabenePolozky_selectedRows.Count > 0)
                        {
                            int pocetArchZaznamu = 0;
                            //Guid pom = VyrabenePolozky_selectedRows[0].GUID;
                            ////archivace vybraných řádků
                            //foreach (var item in VyrabenePolozky_selectedRows)
                            //{
                            //    int id = item.id;
                            //    pom = item.GUID;

                            //    //archivuji podle id, poslu do procedury
                            //    if ((provider != null) && (provider is Fask.Interfaces.IT_cast.IIT_cast_Production_Archivace))
                            //    {
                            //        pocetArchZaznamu = ((Fask.Interfaces.IT_cast.IIT_cast_Production_Archivace)provider).Production_ArchivaceProcedura_guid(pom);
                            //    }
                            //    else
                            //    {
                            //        throw new NotImplementedException("Provider neobsahuje implemetaci IIT_cast_Production_Archivace Production_ArchivaceProcedura");
                            //    }


                            //}

                            MessageBox.Show(this, "Akce dokončena. Pocet zaarchivovaných záznamů:" + VyrabenePolozky_selectedRows.Count.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);



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

                if (VyrabenePolozky_selectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                #region MaR 6.11.2024 vypis zpl do souboru selected rows
                int index_zaznamu = 0;

                foreach (var item in VyrabenePolozky_selectedRows)
                {

                    if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start planovani/vyrabene polozky TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");

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

                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END planovani/vyrabene polozky TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");


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



                    foreach (var item in VyrabenePolozky_selectedRows)
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
                    Tisk_selected_rows_Primy_Tisk(plr_Path, true, MN_ToTisk, VyrabenePolozky_selectedRows, NazevVychoziTiskarny);

                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Tisk_selected_rows_Primy_Tisk(string plr_Path, bool MnozstvuAutoJedna, int MN_ToTisk, List<Fask.Interfaces.DataSets.Hlavni.VyrabenePolozkyRow> selected_Rows, string nazevVychTiskarny)
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

                if (VyrabenePolozky_selectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }



                //if (Production_VyrabenePolozky_selectedRows.Count > 1)
                //{
                //    //MessageBox.Show("Je vybráno více záznamů než jeden.", this.Text, MessageBoxButtons.OK);
                //    //return;
                //}

                Fask.Interfaces.DataSets.Hlavni.VyrabenePolozkyDataTable dt = new Fask.Interfaces.DataSets.Hlavni.VyrabenePolozkyDataTable();

                foreach (Fask.Interfaces.DataSets.Hlavni.VyrabenePolozkyRow row in VyrabenePolozky_selectedRows)
                {
                    try
                    {
                        Fask.Interfaces.DataSets.Hlavni.VyrabenePolozkyRow newRow = dt.NewVyrabenePolozkyRow();
                        // Ošetřit delší vstupní pole než počet sloupců tabulky
                        int copyLength = Math.Min(row.ItemArray.Length, dt.Columns.Count);
                        newRow.ItemArray = row.ItemArray.Take(copyLength).ToArray();
                        dt.AddVyrabenePolozkyRow(newRow);
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

        private void PrintReport_RDLC(Fask.Interfaces.DataSets.Hlavni.VyrabenePolozkyDataTable dt, bool primyTisk, string path_sablona, string nazevVychTiskarny, int pocetVytisku)
        {
            try
            {

                #region logovani tisku
                if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                {
                    foreach (var item in dt)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start planovani/vyrabene polozky TISK rdlc-------------------------------------");

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

                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END planovani/vyrabene polozky TISK rdlc-------------------------------------");
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

                if (VyrabenePolozky_selectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }

              


                if (providerTisk is Fask.Interfaces.Tisky.ITisky2_EtiketaTisk)
                {
                    #region MaR 6.11.2024 vypis zpl do souboru selected rows
                    int index_zaznamu = 0;

                    foreach (var item in VyrabenePolozky_selectedRows)
                    {

                        if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                        {
                            Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start planovani/vyrabene polozky TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");

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

                            Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END planovani/vyrabene polozky TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");


                        }

                        index_zaznamu++;
                    }


                    #endregion

                    var PrinerName = prepareTiskParams();

                    if (PrinerName == null)
                        return;

                    int? MN_ToTisk = TiskMnozstvi(true);



                    foreach (var item in VyrabenePolozky_selectedRows)
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
                    Tisk_selected_rows(plr_Path, true, MN_ToTisk, VyrabenePolozky_selectedRows);

                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Tisk_selected_rows(string plr_Path, bool MnozstvuAutoJedna, int MN_ToTisk, List<Fask.Interfaces.DataSets.Hlavni.VyrabenePolozkyRow> selected_Rows)
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

        #region old MaR 16.1.2025
        //private string PrepareDataToTisk(
        //  string strPath,
        //  Fask.Interfaces.DataSets.Hlavni.VyrabenePolozkyRow row,
        //  int? mnozstviDoTisku
        //  )
        //{
        //    try
        //    {
        //        string strData = string.Empty;
        //        using (System.IO.StreamReader sr = new System.IO.StreamReader(strPath))
        //        {
        //            strData = sr.ReadToEnd();
        //            sr.Close();
        //        }

        //        StringBuilder sbData = new StringBuilder();
        //        sbData.Append(strData);

        //        string GS1_KOD_1_1D = string.Empty;
        //        string GS1_KOD_1_TX = string.Empty;
        //        string GS1_KOD_2_1D = string.Empty;
        //        string GS1_KOD_2_TX = string.Empty;
        //        string SSCC = string.Empty;
        //        string SSCC_bez_nul = string.Empty;
        //        string WEIGHT = string.Empty;

        //        string BarcodeP = string.Empty;
        //        string Expiration = string.Empty;
        //        string Serltnum = string.Empty;
        //        string ExpirationRRMMDD = string.Empty;



        //        string Expiration_YYYY_MM_DD = string.Empty;




        //        sbData.Replace("$SOURCE$", "Konzola");

        //        Fask.Interfaces.DataSets.Hlavni.VyrabenePolozkyDataTable dt = new Fask.Interfaces.DataSets.Hlavni.VyrabenePolozkyDataTable();

        //        foreach (DataColumn dcol in dt.Columns)
        //        {
        //            string key = dcol.ColumnName;
        //            string value = row[dcol.ColumnName].ToString();

        //            try
        //            {
        //                sbData.Replace("$" + key + "$", value.Trim());
        //            }
        //            catch
        //            {
        //            }
        //        }



        //        return sbData.ToString();
        //    }
        //    catch (System.Exception ex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle(ex);
        //        return null;
        //    }
        //} 
        #endregion

        private string PrepareDataToTisk(
    string strPath,
    Fask.Interfaces.DataSets.Hlavni.VyrabenePolozkyRow VyrabenePolozkyRow,
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

                //dodatecne pridani a modifikace promenne pro tisk na ZPL
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
                string datumexpirace01 = string.Empty;
                string sn = string.Empty;
                string SERLTNUM_GTIN14 = string.Empty;
                string VNDITNUM = string.Empty;
                char prefixNumber_char = '0'; // Převod int na char


                if (VyrabenePolozkyRow != null)
                {
                    if (!string.IsNullOrEmpty(VyrabenePolozkyRow.BarcodeP))
                    {
                        BarcodeP = VyrabenePolozkyRow.BarcodeP;
                    }
                    else
                    {
                        BarcodeP = " ";
                    }

                    DateTime? EXPIRACE = DateTime.Now;

                    if (EXPIRACE != null && EXPIRACE.HasValue)
                    {

                        Expiration = EXPIRACE.Value.ToString("yyMMdd");

                        ExpirationRRMMDD = EXPIRACE.Value.ToString("yy-MM-dd");
                        Expiration_YYYY_MM_DD = EXPIRACE.Value.ToString("yyyy-MM-dd");

                        datumexpirace01 = EXPIRACE.Value.ToString("yyyy-MM-dd");
                        sn = EXPIRACE.Value.ToString("yy");

                    }
                    else
                    {
                        Expiration = " ";
                        ExpirationRRMMDD = " ";

                        Expiration_YYYY_MM_DD = " ";
                    }

                    if (!string.IsNullOrEmpty(VyrabenePolozkyRow.VNDITNUM))
                    {
                        Serltnum = VyrabenePolozkyRow.VNDITNUM;
                    }
                    else
                    {
                        Serltnum = " ";
                    }


                    if (VyrabenePolozkyRow["WEIGHT_NETTO"] != DBNull.Value)
                    {
                        WEIGHT = VyrabenePolozkyRow.WEIGHT_NETTO.ToString();
                    }
                    else
                    {
                        WEIGHT = string.Empty; // Nebo jakákoli jiná výchozí hodnota, kterou potřebuješ.
                    }

                    if (!string.IsNullOrEmpty(VyrabenePolozkyRow.ITEMNMBR))
                    {
                        SSCC = VyrabenePolozkyRow.ITEMNMBR;

                        int pocetZnaku = VyrabenePolozkyRow.ITEMNMBR.Length;

                        if (pocetZnaku >= 2)
                            SSCC_bez_nul = SSCC.Substring(2);
                        else
                            SSCC_bez_nul = VyrabenePolozkyRow.ITEMNMBR;



                        #region reseni BIOMAG_ 8.1.2025



                        if (string.IsNullOrEmpty(VyrabenePolozkyRow.QTYPACKMJ))
                        {
                            prefixNumber_char = '0';
                        }
                        else
                        {
                            prefixNumber_char = VyrabenePolozkyRow.QTYPACKMJ[0];
                        }


                        if (!string.IsNullOrEmpty(VyrabenePolozkyRow.VNDITNUM))
                        {

                            VNDITNUM = VyrabenePolozkyRow.VNDITNUM;
                            if (VNDITNUM.Length < 13)
                            {
                                VNDITNUM = VNDITNUM.PadLeft(12, '0');
                                VNDITNUM = VNDITNUM.PadLeft(13, prefixNumber_char);

                            }
                            else if (VNDITNUM.Length > 13)
                            {
                                VNDITNUM = VNDITNUM.Substring(0, 12);
                                VNDITNUM = VNDITNUM.PadLeft(13, prefixNumber_char);
                            }
                            else if (VNDITNUM.Length == 13)
                            {
                                VNDITNUM = VNDITNUM.Substring(0, 12);
                                VNDITNUM = VNDITNUM.PadLeft(13, prefixNumber_char);
                            }

                            SERLTNUM_GTIN14 = AddCheckDigit(VNDITNUM);
                        }

                        #endregion



                    }

                    GS1_KOD_1_1D = "02" + VyrabenePolozkyRow.BarcodeP.PadLeft(14, '0') + "37" + VyrabenePolozkyRow.QTYSHPPD.ToString("0000") + "";
                    GS1_KOD_1_TX = "(02)" + VyrabenePolozkyRow.BarcodeP.PadLeft(14, '0') + "(37)" + VyrabenePolozkyRow.QTYSHPPD.ToString("0000") + ""; //(02) (37)
                    GS1_KOD_2_1D = SSCC; //SSCC neni v production
                    GS1_KOD_2_TX = "(00)" + SSCC_bez_nul; // SSCC neni v production
                }

                //promenne na vystup
                sbData.Replace("$SERLTNUM_GTIN14$", SERLTNUM_GTIN14);
                sbData.Replace("$BarcodeP$", BarcodeP);
                sbData.Replace("$EXPIRATION$", Expiration);
                sbData.Replace("$ExpirationYYMMDD$", ExpirationRRMMDD);
                sbData.Replace("$Expiration_YYYY_MM_DD$", Expiration_YYYY_MM_DD);
                sbData.Replace("$SERLTNUM$", Serltnum);
                sbData.Replace("$WEIGHT$", WEIGHT);
                sbData.Replace("$GS1_KOD_1_1D$", GS1_KOD_1_1D);
                sbData.Replace("$GS1_KOD_1_TX$", GS1_KOD_1_TX);
                sbData.Replace("$GS1_KOD_2_1D$", GS1_KOD_2_1D);
                sbData.Replace("$GS1_KOD_2_TX$", GS1_KOD_2_TX);
                sbData.Replace("$SSCC$", SSCC_bez_nul);

                sbData.Replace("$SOURCE$", "Konzola");

                Fask.Interfaces.DataSets.Hlavni.VyrabenePolozkyDataTable dt = new Fask.Interfaces.DataSets.Hlavni.VyrabenePolozkyDataTable();

                //promenna pro logovani tisku ZPL
                int index = 0;


                foreach (DataColumn dcol in dt.Columns)
                {
                    string key = dcol.ColumnName;
                    string value = string.Empty;
                    // var value2 = VyrabenePolozkyRow["DEX_ROW_ID"];
                   
                     value = VyrabenePolozkyRow[dcol.ColumnName].ToString();

                    #region 11.12.2024 ZPL tisk logovani
                    if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                    {

                        if (index == 0)
                        {
                            Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start vyroba/transakce/vyrobni prikazy TISK zpl zaznam: " + "-------------------------------------");


                            //11.12.2024 pridani promennych na vypis
                            string key1 = "BarcodeP";
                            string key2 = "EXPIRATION";
                            string key3 = "ExpirationYYMMDD";
                            string key4 = "Expiration_YYYY_MM_DD";
                            string key5 = "SERLTNUM";
                            string key6 = "WEIGHT";
                            string key7 = "GS1_KOD_1_1D";
                            string key8 = "GS1_KOD_1_TX";
                            string key9 = "GS1_KOD_2_1D";
                            string key10 = "GS1_KOD_2_TX";
                            string key11 = "SSCC";
                            string key12 = "SERLTNUM_GTIN14";


                            Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, $"{key1}: {BarcodeP}");
                            Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, $"{key2}: {Expiration}");
                            Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, $"{key3}: {ExpirationRRMMDD}");
                            Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, $"{key4}: {Expiration_YYYY_MM_DD}");
                            Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, $"{key5}: {Serltnum}");
                            Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, $"{key6}: {WEIGHT}");
                            Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, $"{key7}: {GS1_KOD_1_1D}");
                            Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, $"{key8}: {GS1_KOD_1_TX}");
                            Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, $"{key9}: {GS1_KOD_2_1D}");
                            Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, $"{key10}: {GS1_KOD_2_TX}");
                            Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, $"{key11}: {SSCC_bez_nul}");
                            Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, $"{key12}: {SERLTNUM_GTIN14}");
                        }

                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, $"{key}: {value}");

                        index++;
                        if (index == dt.Columns.Count)
                        {
                            Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END vyroba/transakce/vyrobni prikazy TISK zpl zaznam: " + "-------------------------------------");
                        }

                    }

                    #endregion

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


        static string AddCheckDigit(string input)
        {
            // Výpočet check digitu
            int sumOdd = 0;
            int sumEven = 0;

            for (int i = 0; i < input.Length; i++)
            {
                int digit = int.Parse(input[i].ToString());

                if (i % 2 == 0)
                {
                    sumEven += digit;
                }
                else
                {
                    sumOdd += digit;
                }
            }

            int totalSum = sumOdd + sumEven * 3;
            int nearestTenMultiple = (int)Math.Ceiling((double)totalSum / 10) * 10;
            int checkDigit = nearestTenMultiple - totalSum;

            // Přidání check digitu na konec vstupního čísla
            return input + checkDigit.ToString();
        }



        private Dictionary<string, string> PrepareDataToTisk(Fask.Interfaces.DataSets.Hlavni.VyrabenePolozkyRow row_data)
        {
            try
            {
                while (true)
                {
                    try
                    {
                        Dictionary<string, string> data = new Dictionary<string, string>();


                        data.Add("SOURCE", "Konzola");

                        Fask.Interfaces.DataSets.Hlavni.VyrabenePolozkyDataTable dt = new Fask.Interfaces.DataSets.Hlavni.VyrabenePolozkyDataTable();


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
