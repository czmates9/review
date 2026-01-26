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

namespace Konzola.IT_cast
{
    public partial class FormIT_cast_ProductionArchivace : Form
    {
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
            get
            {
                List<Fask.Interfaces.DataSets.Vyroba.Production_KonzolaRow> rows = new List<Fask.Interfaces.DataSets.Vyroba.Production_KonzolaRow>();

                foreach (DataGridViewRow selectedRow in dgVyrobek.SelectedRows)
                {
                    Fask.Interfaces.DataSets.Vyroba.Production_KonzolaRow row = ((DataRowView)selectedRow.DataBoundItem).Row as Fask.Interfaces.DataSets.Vyroba.Production_KonzolaRow;
                    rows.Add(row);
                }

                return rows;
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
        public FormIT_cast_ProductionArchivace()
        {
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);

            InitializeComponent();
            this.dgVyrobek.UpdateColumnHeaderCellsByDatasource();





            panelButtons.Menu = menuStrip1;
        }

        private void FormProductionList_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                this.WindowState = FormWindowState.Maximized;

                // načtení konfigurace datagridu z nastavení aplikace
                this.dgVyrobek.LoadConfiguration(this.GetType().ToString());
                panelButtons.LoadConfiguration(this.GetType().ToString());
                panelButtons.Init();

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

     
        private void archivaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
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
                    frm.Popis = archivaceToolStripMenuItem.Text;

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


                if ((provider != null) && (provider is Fask.Interfaces.IT_cast.IIT_cast_Production_Archivace))
                {
                    pocetArchZaznamu = ((Fask.Interfaces.IT_cast.IIT_cast_Production_Archivace)provider).Production_ArchivaceProcedura(OD, DO);
                }
                else
                {
                    throw new NotImplementedException("Provider neobsahuje implemetaci IIT_cast_Production_Archivace Production_ArchivaceProcedura");
                }

                MessageBox.Show(this, "Akce dokončena. Počet zaarchivovaných záznamů:" + pocetArchZaznamu.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

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

        private void archivaceVybraneToolStripMenuItem_Click(object sender, EventArgs e)
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
}
