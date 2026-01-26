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
using Konzola.Forms;
using Konzola.Vyroba.Rozbory;
using MST_Print_Server_ZPL_Printing;

namespace Konzola.Vyroba
{
    public partial class FormProductionSourecesList : Form
    {

        #region private promenne

        private bool opravneniEditace = false;
        private bool opravneniImport = false;
        private bool opravneniArchivace = false;

        #endregion
        /// <summary>
        /// Provider pro komunikaci
        /// </summary>
        private Fask.Interfaces.IMES providerPS = null;
        private Fask.Interfaces.IMES providerZbozi = null;

        private Fask.Interfaces.IMES providerVPH = null;
        private Fask.Interfaces.IMES provider093 = null;
        private Fask.Interfaces.IMES provider094 = null;
        private Fask.Interfaces.IMES providerTisk = null; //**DONE
                                                          //private Fask.Interfaces.IVyrobaKonzola providerLogins = null;





        /// <summary>
        /// Zvolený Production záznam.
        /// </summary>
        private Fask.Interfaces.DataSets.Vyroba.Production_SourcesRow Production_Sources_selectedRow
        {
            get
            {
                try
                {
                    return ((DataRowView)(dgMaterial.BindingContext[this.bsMaterial].Current)).Row as Fask.Interfaces.DataSets.Vyroba.Production_SourcesRow;
                }
                catch
                {
                    return null;
                }
            }
        }

    
        private List<Fask.Interfaces.DataSets.Vyroba.Production_SourcesRow> Production_Sources_selectedRows
        {
            //get
            //{
            //    List<Fask.Interfaces.DataSets.Vyroba.Production_SourcesRow> rows = new List<Fask.Interfaces.DataSets.Vyroba.Production_SourcesRow>();

            //    foreach (DataGridViewRow selectedRow in dgMaterial.SelectedRows)
            //    {
            //        Fask.Interfaces.DataSets.Vyroba.Production_SourcesRow row = ((DataRowView)selectedRow.DataBoundItem).Row as Fask.Interfaces.DataSets.Vyroba.Production_SourcesRow;
            //        rows.Add(row);
            //    }

            //    return rows;
            //}

            get
            {
                return dgMaterial.SelectedRows
                    .Cast<DataGridViewRow>()
                    .OrderBy(r => r.Index)               // zarovnání na vizuální pořadí v gridu
                    .Select(r => ((DataRowView)r.DataBoundItem).Row as Fask.Interfaces.DataSets.Vyroba.Production_SourcesRow)
                    .Where(r => r != null)
                    .ToList();
            }
        }

        /// <summary>
        /// Zvolený VPH záznam.
        /// </summary>
        private Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHRow rowVPH
        {
            get
            {
                try
                {
                    //return ((DataRowView)(comboBoxVyrobniPrikaz.SelectedItem)).Row as Fask.Interfaces.DataSets.Vyroba..CZPRO_VPHRow;
                    return comboBoxSOPNUMBE.SelectedItem as Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHRow;
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Zvolený  sklad
        /// </summary>
        private Fask.Interfaces.DataSets.Vyroba.CZMST093Row rowSKLAD
        {
            get
            {
                try
                {
                    //return ((DataRowView)(comboBoxVyrobniPrikaz.SelectedItem)).Row as Fask.Interfaces.DataSets.Vyroba..CZPRO_VPHRow;
                    return ComboBoxSKL_ID.SelectedItem as Fask.Interfaces.DataSets.Vyroba.CZMST093Row;
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Zvolený  lokace
        /// </summary>
        private Fask.Interfaces.DataSets.Vyroba.CZMST094Row rowLokace
        {
            get
            {
                try
                {
                    //return ((DataRowView)(comboBoxVyrobniPrikaz.SelectedItem)).Row as Fask.Interfaces.DataSets.Vyroba..CZPRO_VPHRow;
                    return ComboBoxLOCNCODE.SelectedItem as Fask.Interfaces.DataSets.Vyroba.CZMST094Row;
                }
                catch
                {
                    return null;
                }
            }
        }

        private Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_KONZOLARow rowZbozi
        {
            get
            {
                try
                {
                    return comboBoxzbozi.SelectedItem as Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_KONZOLARow;
                }
                catch
                {
                    return null;
                }
            }
        }

        private Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_KONZOLARow rowVyrobek
        {
            get
            {
                try
                {
                    return comboBox_Zbozi_Vyrobek.SelectedItem as Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_KONZOLARow;
                }
                catch
                {
                    return null;
                }
            }
        }

        private FASK.Logins.DataSets.Pristupy.FASK_LoginsRow rowUzivatel
        {
            get
            {
                try
                {
                    return comboBoxUSER_ID.SelectedItem as FASK.Logins.DataSets.Pristupy.FASK_LoginsRow;
                }
                catch
                {
                    return null;
                }
            }
        }


        #region Eventy formu

        public FormProductionSourecesList()
        {
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);

            InitializeComponent();
            this.dgMaterial.UpdateColumnHeaderCellsByDatasource();
            panelButtons.Menu = menuStrip1;
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
        public FormProductionSourecesList(Opravneni opravneni)
        {
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);

            InitializeComponent();
            this.dgMaterial.UpdateColumnHeaderCellsByDatasource();
            panelButtons.Menu = menuStrip1;



            // Uloží oprávnění pro další použití v programu
            this.opravneni = opravneni;

            // Nastavení podle oprávnění
            SetOpravneni();

        }


        private void FormProductionSourecesList_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                this.WindowState = FormWindowState.Maximized;

                // načtení konfigurace datagridu z nastavení aplikace
                this.dgMaterial.LoadConfiguration(this.GetType().ToString());

                panelButtons.LoadConfiguration(this.GetType().ToString());
                panelButtons.Init();
                panelButtons.Size = new Size(85, 700);


                advancedDataGridViewSearchToolBar1.SetColumns(dgMaterial.Columns);

                InitProvider();

                if (providerPS == null)
                    throw new Exception("Provider 'Production Sources' není inicializován");

                if (providerZbozi == null)
                    throw new Exception("Provider 'Zbozi' není inicializován");


                if (providerVPH == null)
                    throw new Exception("Provider 'VPH' není inicializován");

                if (provider093 == null)
                    throw new Exception("Provider 'Sklad' není inicializován");

                if (provider094 == null)
                    throw new Exception("Provider 'Lokace' není inicializován");

                //if (providerLogins == null)
                //    throw new Exception("Provider 'Logins' není inicializován");

    

                // nastavení času
                // nastavení času (zacatek a konec dne)
                dateTimePickerDatumOd.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                dateTimePickerDatumDo.Value = DateTime.Now.AddDays(1).Date.AddSeconds(-1);
                //dateTimePickerDatumDo.Value = dateTimePickerDatumOd.Value = DateTime.Now.AddSeconds(-DateTime.Now.Second);
                dateTimePickerDatumDo.Checked = false;
                dateTimePickerDatumOd.Checked = false;


                // naplnění comboboxu zakázek
                //var adapter = new Production.DataServices.VyrobaDataSetTableAdapters.CZPRO_VPHTableAdapter();
                //adapter.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
                //adapter.Fill(this.vyrobaDataSet1.CZPRO_VPH);

                if ((providerVPH != null) && (providerVPH is Fask.Interfaces.Vyroba.VPH.IVPH_Fill))
                    ((Fask.Interfaces.Vyroba.VPH.IVPH_Fill)providerVPH).VPH_Fill(this.dsMaterial);
                else
                    throw new NotImplementedException("IVPH_Fill not implementit");



                comboBoxSOPNUMBE.Items.AddRange(this.dsMaterial.CZPRO_VPH.Select(null, "SOPNUMBE asc"));
                comboBoxSOPNUMBE.SelectedItem = null;


                //var adapterSKL_ID = new Production.DataServices.VyrobaDataSetTableAdapters.CZMST093TableAdapter();
                //adapterSKL_ID.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
                //adapterSKL_ID.Fill(this.vyrobaDataSet1.CZMST093);

                if ((provider093 != null) && (provider093 is Fask.Interfaces.Ciselniky.Sklady.ISklady2_Vyroba_Fill))
                    ((Fask.Interfaces.Ciselniky.Sklady.ISklady2_Vyroba_Fill)provider093).Sklady_Vyroba_Fill(this.dsMaterial);
                else
                    throw new NotImplementedException("ISklady2_Vyroba_Fill not implementit");

                ComboBoxSKL_ID.Items.AddRange(this.dsMaterial.CZMST093.Select(null, "SKL_ID asc"));
                ComboBoxSKL_ID.SelectedItem = null;

                //var adapterLOCNCODE = new Production.DataServices.VyrobaDataSetTableAdapters.CZMST094TableAdapter();
                //adapterLOCNCODE.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
                //adapterLOCNCODE.Fill(this.vyrobaDataSet1.CZMST094);

                if ((provider094 != null) && (provider094 is Fask.Interfaces.Ciselniky.Lokace.ILokace2_Fill))
                    ((Fask.Interfaces.Ciselniky.Lokace.ILokace2_Fill)provider094).Fill(this.dsMaterial);
                else
                    throw new NotImplementedException("ILokace2_Fill not implementit");

                ComboBoxLOCNCODE.Items.AddRange(this.dsMaterial.CZMST094.Select(null, "LOCNCODE asc"));
                ComboBoxLOCNCODE.SelectedItem = null;

                //var adapterUzivatel = new Production.DataServices.VyrobaDataSetTableAdapters.LoginsTableAdapter();
                //adapterUzivatel.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
                //adapterUzivatel.Fill(this.vyrobaDataSet1.Logins);

                //if ((providerLogins != null) && (providerLogins is Fask.Interfaces.Vyroba.Login.ILogin_FillLogin))
                //    ((Fask.Interfaces.Vyroba.Login.ILogin_FillLogin)providerLogins).FillLogin(this.vyrobaDataSet1);
                //else
                //    throw new NotImplementedException("ILokace2_Fill not implementit");


                var dtUzivateleData = FASK.Logins.Uzivatel.Instance.Komunikace.GetLogins();

                //comboBoxUzivatel.DataSource = this.vyrobaDataSet1.Logins;
                comboBoxUSER_ID.Items.AddRange(dtUzivateleData.Select(null, "surname asc, firstname asc"));
                comboBoxUSER_ID.SelectedItem = null;

                if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba[0].PouzivatTabulkuZbozi)
                {
                    Fask.Interfaces.DataSets.Zbozi konzolaDataSet1 = new Fask.Interfaces.DataSets.Zbozi();

                    if ((providerZbozi != null) && (providerZbozi is Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_GetZbozi))
                        konzolaDataSet1 = ((Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_GetZbozi)providerZbozi).GetZbozi();
                    else
                        throw new NotImplementedException("Neimplementovan provider pro IZbozi2_GetZbozi");


                    comboBoxzbozi.Items.AddRange(konzolaDataSet1.FASK_ZASOBY_ALL_KONZOLA.Select(null, "ITEMDESC asc"));
                    comboBoxzbozi.SelectedItem = null;

                    comboBox_Zbozi_Vyrobek.Items.AddRange(konzolaDataSet1.FASK_ZASOBY_ALL_KONZOLA.Select(null, "ITEMDESC asc"));
                    comboBox_Zbozi_Vyrobek.SelectedItem = null;
                }
                else comboBoxzbozi.Enabled = false;


                btn_Vyhledat.Focus();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void FormProductionSourecesList_KeyDown(object sender, KeyEventArgs e)
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


        private void FormProductionSourecesList_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.dgMaterial.SaveConfiguration(this.GetType().ToString());
                panelButtons.SaveConfiguration(this.GetType().ToString());
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        #endregion




        /// <summary>
        /// Inicializace providera Production
        /// </summary>
        private void InitProvider()
        {
            #region Production Sources
            try
            {
                if (String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                    return;

                if (providerPS == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Vyroba.ProductionSources.IProductionSources).IsAssignableFrom(t))
                            {
                                providerPS = (Fask.Interfaces.Vyroba.ProductionSources.IProductionSources)providerAssemlby.CreateInstance(t.FullName);
                                if (providerPS != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                providerPS.InitProvider();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 
            #endregion

            #region ZBOZI
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

            #region VPH
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

            #region 093
            try
            {

                if (provider093 == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Ciselniky.Sklady.ISklady2).IsAssignableFrom(t))
                            {
                                provider093 = (Fask.Interfaces.Ciselniky.Sklady.ISklady2)providerAssemlby.CreateInstance(t.FullName);
                                if (provider093 != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                provider093.InitProvider();
          
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion

            #region 094
            try
            {

                if (provider094 == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Ciselniky.Lokace.ILokace2).IsAssignableFrom(t))
                            {
                                provider094 = (Fask.Interfaces.Ciselniky.Lokace.ILokace2)providerAssemlby.CreateInstance(t.FullName);
                                if (provider094 != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                provider094.InitProvider();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion

            //#region Logins
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


        private void textBoxVyrobniPrikaz_Leave(object sender, EventArgs e)
        {
            //try
            //{
            //    comboBoxVyrobniPrikaz.SelectedValue = textBoxVyrobniPrikaz.Text.Trim();
            //}
            //catch (Exception ex)
            //{
            //    Log.Write(ex);
            //    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
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

        private void upravitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //PerformEditRecord();
        }

        private void PerformEditRecord()
        {
            try
            {
                if (this.dgMaterial.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Je možné upravovat pouze po jednom záznamu", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if (Production_Sources_selectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro úpravu", this.Text, MessageBoxButtons.OK);
                    return;
                }

                using (FormProductionSourcesEdit2 frmuziv = new FormProductionSourcesEdit2())
                {
                    frmuziv.rowProductSources = Production_Sources_selectedRow;
                    frmuziv.Text = "Úprava výroby";
                    if (frmuziv.ShowDialog(this) != DialogResult.OK)
                        return;

                    //var lta = new Production.DataServices.VyrobaDataSetTableAdapters.ProductionTableAdapter();
                    //lta.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
                    ////lta.Delete(SelectedRow.ITEMNMBR);
                    //lta.Update(frmuziv.rowProductSources);

                    if ((providerPS != null) && (providerPS is Fask.Interfaces.Vyroba.ProductionSources.IProductionSources_Update_Row))
                        ((Fask.Interfaces.Vyroba.ProductionSources.IProductionSources_Update_Row)providerPS).Update_Row(frmuziv.rowProductSources);
                    else
                        throw new NotImplementedException("Neimplementovan provider pro IProductionSources_Update_Row");


                    //lta.Update(this.vyrobaDataSet1.Production);
                    this.dsMaterial.AcceptChanges();
                }
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

        private void buttonVyhledat_Click(object sender, EventArgs e)
        {
            NastavDatagrid();

            //PerformOK();
            PerformVyhledat();
        }


        private void NastavDatagrid()
        {
            #region prace s datagridem - nastaveni numeric hodnot na pocet desetinnych mist
            try
            {
                // Procházení sloupců v DataGridView
                foreach (DataGridViewColumn column in dgMaterial.Columns)
                {
                    // Získání typu dat pro sloupec
                    Type dataType = null;

                    if (dgMaterial.DataSource is DataTable dataTable)
                    {
                        // Pokud je datový zdroj DataTable
                        if (dataTable.Columns.Contains(column.DataPropertyName))
                        {
                            dataType = dataTable.Columns[column.DataPropertyName].DataType;
                        }
                    }
                    else if (dgMaterial.DataSource is BindingSource bindingSource)
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
        }

        private void buttonOdznacitVse_Click(object sender, EventArgs e)
        {
            try
            {
                this.dgMaterial.ClearSelection();
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
                this.dgMaterial.SelectAll();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void ComboBoxSKL_ID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // aktualizovat polozky lokaci a pokusit se nechat vybrat lokaci se stejnym kodem ... na nove ...
                var lokaceAkt = ComboBoxLOCNCODE.SelectedItem as Fask.Interfaces.DataSets.Vyroba.CZMST094Row;

                var sklad = ComboBoxSKL_ID.SelectedItem as Fask.Interfaces.DataSets.Vyroba.CZMST093Row;
                if (sklad == null)
                    return;

                ComboBoxLOCNCODE.Items.Clear();
                ComboBoxLOCNCODE.Items.AddRange(this.dsMaterial.CZMST094.Where(x => x.SKL_ID == sklad.skl_id).ToArray());

                var lokaceNove = this.dsMaterial.CZMST094.Where(x => x.SKL_ID == sklad.skl_id && x.Barcode == lokaceAkt.Barcode);
                if (lokaceNove.Count() > 0)
                    ComboBoxLOCNCODE.SelectedItem = lokaceNove.First();

            }
            catch
            {
            }
        }


        /// <summary>
        /// Metoda pro dotaženi dat podle filtru
        /// </summary>
        private void PerformVyhledat()
        {
            try
            {
                DataTable dtchanged = this.dsMaterial.Production_Sources.GetChanges();
                if (dtchanged != null && dtchanged.Rows.Count > 0) //obsahuje zmeny ... 
                {
                    DialogResult dr = MessageBox.Show("Obsahuje změny, pokračovat?", "data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == System.Windows.Forms.DialogResult.No)
                        return;
                }

                if (bwMaterial.IsBusy)
                {
                    bwMaterial.CancelAsync();
                    while (bwMaterial.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }

                ProgressIndicatorVyrobekStart();

                Fask.Interfaces.Filtry.ProductionSourcesListFiltr filtr = new Fask.Interfaces.Filtry.ProductionSourcesListFiltr();
                if (!CreateFilter(ref filtr))
                    return;

                int FirstDisplayedScrollingRowIndex = this.dgMaterial.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

                bwMaterial.RunWorkerAsync(filtr);

                if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dgMaterial.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dgMaterial.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index
            }
            catch (Exception ex)
            {
                ProgressIndicatorVyrobekStop();
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        /// <summary>
        /// Vytvořeni filtru pro dotazeni dat
        /// </summary>
        /// <param name="filtr"></param>
        /// <returns></returns>
        private bool CreateFilter(ref Fask.Interfaces.Filtry.ProductionSourcesListFiltr filtr)
        {
            if (filtr == null)
                filtr = new Fask.Interfaces.Filtry.ProductionSourcesListFiltr();


            filtr.CountEntries = textBoxCountEntries.Text;
        filtr.rowVPH= rowVPH;
        filtr.SOPNUMBE= comboBoxSOPNUMBE.Text;
        filtr.EAN = textBoxEAN.Text;
        filtr.rowSKLAD = rowSKLAD;
        filtr.SKLAD= ComboBoxSKL_ID.Text;
        filtr.rowLokace= rowLokace;
        filtr.LOCNCODE= ComboBoxLOCNCODE.Text;
        filtr.VyrobaPouzivatTabulkuZbozi = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba[0].PouzivatTabulkuZbozi;
        filtr.rowZASOBY = rowZbozi;
        filtr.ZBOZI = comboBoxzbozi.Text;
        filtr.rowVyrobek= rowVyrobek;
        filtr.VYROBEK= comboBox_Zbozi_Vyrobek.Text;
        filtr.ITEMTYPE = textBoxITEMTYPE.Text;
        filtr.ITEMCODE = textBoxITEMCODE.Text;
        filtr.MJ = textBoxMJ.Text;
        filtr.SERLTNUM = textBoxSERLTNUM.Text;
        filtr.DatumDo = dateTimePickerDatumDo.Checked;
        filtr.DatumOd = dateTimePickerDatumOd.Checked;
        filtr.DatumDoValue= dateTimePickerDatumDo.Value;
        filtr.DatumOdValue= dateTimePickerDatumOd.Value;
        filtr.Uzivatel= comboBoxUSER_ID.Text;
        filtr.rowUzivatel= rowUzivatel;
        filtr.TERMINAL_ID = textBoxTERMINAL_ID.Text;
        filtr.NMBRPAL = textBoxNMBRPAL.Text;
        filtr.TYPEPAL = textBoxTYPEPAL.Text;
        filtr.PRINTED = textBoxPRINTED.Text;



            return true;
        }


        private void bw_ProductionSources_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Fask.Interfaces.Filtry.ProductionSourcesListFiltr filtr = (Fask.Interfaces.Filtry.ProductionSourcesListFiltr)e.Argument;
                Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();
                //Fask.Interfaces.DataSets.SkladLokace ds = new Fask.Interfaces.DataSets.SkladLokace();

                if (bwMaterial.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                //providerVazby.ITEMNMBR_Materialy = rowVyrobek.ITEMNMBR;

                // nacteni dat v oddelenem vlakne
                if (providerPS is Fask.Interfaces.Vyroba.ProductionSources.IProductionSources_GetFiltrovanyProductionSourcesList)
                    ds = ((Fask.Interfaces.Vyroba.ProductionSources.IProductionSources_GetFiltrovanyProductionSourcesList)providerPS).GetFiltrovanyProductionSourcesList(filtr);
                else
                    throw new Exception("IProductionSources_GetFiltrovanyProductionSourcesList not implementet");
                //providerVazby.ITEMNMBR_Materialy = String.Empty;

                if (bwMaterial.CancellationPending)
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

        private void bw_ProductionSources_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                // check error, check cancel, then use result
                if (e.Error != null)
                {
                    // handle the error
                    this.dsMaterial = new Fask.Interfaces.DataSets.Vyroba();
                    bsMaterial.DataSource = this.dsMaterial;
                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, e.Error);
                    MessageBox.Show(e.Error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (e.Cancelled)
                {
                    // handle cancellation
                    this.dsMaterial = new Fask.Interfaces.DataSets.Vyroba();
                    bsMaterial.DataSource = this.dsMaterial;
                }
                else
                {
                    // uspesne dokonceno ...
                    // use it on the UI thread
                    this.dsMaterial = (Fask.Interfaces.DataSets.Vyroba)e.Result;
                    if (this.dsMaterial == null)
                        this.dsMaterial = new Fask.Interfaces.DataSets.Vyroba();

                    bsMaterial.DataSource = this.dsMaterial;
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
                this.progressIndicatorVyrobek.Location = new Point(this.dgMaterial.Location.X + (this.dgMaterial.Width / 2) - (progressIndicatorVyrobek.Size.Width / 2), this.dgMaterial.Location.Y + (this.dgMaterial.Height / 2) - (progressIndicatorVyrobek.Size.Height / 2));
            }
            catch { }
            progressIndicatorVyrobek.Start();
            progressIndicatorVyrobek.Visible = true;
        }


        /// <summary>
        /// Metoda pro import dat
        /// </summary>
        private void PerformImport(Fask.Interfaces.DataSets.Vyroba.Production_SourcesImportRow RowPS)
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

                bw_import.RunWorkerAsync(RowPS);
            }
            catch (Exception ex)
            {
                ProgressIndicatorVyrobekStop();
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void bw_import_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Fask.Interfaces.DataSets.Vyroba.Production_SourcesImportRow RowPS = (Fask.Interfaces.DataSets.Vyroba.Production_SourcesImportRow)e.Argument;

                if (bw_import.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                string status = string.Empty;
                //Zavolani metody s backgroungworkerem....

                Fask.Interfaces.Classes.StatusInfo_Dispo dd = null;

                if (providerPS is Fask.Interfaces.Vyroba.ProductionSources.IProductionSources_ImportVydejkaPohoda)
                    dd = ((Fask.Interfaces.Vyroba.ProductionSources.IProductionSources_ImportVydejkaPohoda)providerPS).ImportVydejkaPohoda(RowPS.CountEntries, RowPS.SKL_ID, FASK.Logins.Uzivatel.Instance.UserID, false, false);
                else
                    throw new Exception("IProduction_ImportVydejkaPohoda not implementet");

                status = dd.Description;

                if (string.IsNullOrEmpty(status))
                {
                    e.Cancel = true;
                    return;
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
                //string num = (string)e.Result;

                if (e.Error != null)
                {
                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, e.Error);
                    MessageBox.Show(e.Error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (e.Cancelled)
                {
                    //Log.Write((string)e.Result);
                    //MessageBox.Show((string)e.Result, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show(string.Format("Výdejka č.{0} exportována.", (string)e.Result), "Import", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                ProgressIndicatorVyrobekStop();
                PerformVyhledat();
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
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (this.dgMaterial.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Je možné upravovat pouze po jednom záznamu", this.Text, MessageBoxButtons.OK);
                    return;
                }


                if (Production_Sources_selectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro úpravu", this.Text, MessageBoxButtons.OK);
                    return;
                }

                using (FormProductionSourcesEdit2 frmuziv = new FormProductionSourcesEdit2())
                {
                    frmuziv.rowProductSources = Production_Sources_selectedRow;
                    frmuziv.Text = "Úprava materialu";
                    if (frmuziv.ShowDialog(this) != DialogResult.OK)
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

                if (this.dgMaterial.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Nejsou vybrány záznamy na schválení", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if (MessageBox.Show("Opravdu chcete schválit vybrané záznamy?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;

                //var lta = new Production.DataServices.VyrobaDataSetTableAdapters.Production_SourcesTableAdapter();
                //lta.Connection.ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;

                foreach (DataGridViewRow item in this.dgMaterial.SelectedRows)
                {
                    Fask.Interfaces.DataSets.Vyroba.Production_SourcesRow row = ((item.DataBoundItem as DataRowView).Row) as Fask.Interfaces.DataSets.Vyroba.Production_SourcesRow;
                    row.idVS = FASK.Logins.Uzivatel.Instance.UserID;
                    row.dateedit = DateTime.Now;

                }

                if ((providerPS != null) && (providerPS is Fask.Interfaces.Vyroba.ProductionSources.IProductionSources_Update))
                    ((Fask.Interfaces.Vyroba.ProductionSources.IProductionSources_Update)providerPS).Update(this.dsMaterial.Production_Sources);
                else
                    throw new NotImplementedException("Neimplementovan provider pro IProductionSources_Update");


                //lta.Update(this.vyrobaDataSet1.Production_Sources);
                this.dsMaterial.AcceptChanges();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsmiExportovatVydejku_Click(object sender, EventArgs e)
        {
            Fask.Interfaces.DataSets.Vyroba.Production_SourcesImportRow RowPS;

            //Zmenit na ProductionSources...
            using (FormProductionSourcesList_SelectCountEntries frm = new FormProductionSourcesList_SelectCountEntries())
            {
                Fask.Interfaces.DataSets.Vyroba ds;

                if (providerPS is Fask.Interfaces.Vyroba.ProductionSources.IProductionSources_GetDataSelectListImport)
                    ds = ((Fask.Interfaces.Vyroba.ProductionSources.IProductionSources_GetDataSelectListImport)providerPS).GetDataSelectListImport();
                else
                    throw new Exception("IProductionSources_GetDataSelectListImport not implementet");


                frm.AVyrobaDataSet = ds;


                if (frm.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return;

                RowPS = frm.rowPS;
                //pokracovat v nacitavani dal s vybranou davkou

            }

            PerformImport(RowPS);
        }

        #region Exporty



        private void exportDoCSVVseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                this.dgMaterial.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                this.dgMaterial.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
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

                this.dgMaterial.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                dgMaterial.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
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

                dgMaterial.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                dgMaterial.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
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
                bool endcol = dgMaterial.CurrentCell.ColumnIndex + 1 >= dgMaterial.ColumnCount;
                bool endrow = dgMaterial.CurrentCell.RowIndex + 1 >= dgMaterial.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dgMaterial.CurrentCell.ColumnIndex;
                    startRow = dgMaterial.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dgMaterial.CurrentCell.ColumnIndex + 1;
                    startRow = dgMaterial.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dgMaterial.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dgMaterial.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dgMaterial.CurrentCell = c;
        }

        #region MaR 26.9.2025 Tisk ZPL a RDLC
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

                if (Production_Sources_selectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                #region MaR 6.11.2024 vypis zpl do souboru selected rows
                int index_zaznamu = 0;

                foreach (var item in Production_Sources_selectedRows)
                {

                    if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start vyroba/rozbory/prehled_materialy TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");

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

                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END vyroba/rozbory/prehled_materialy TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");


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



                    foreach (var item in Production_Sources_selectedRows)
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
                    Tisk_selected_rows_Primy_Tisk(plr_Path, true, MN_ToTisk, Production_Sources_selectedRows, NazevVychoziTiskarny);

                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Tisk_selected_rows_Primy_Tisk(string plr_Path, bool MnozstvuAutoJedna, int MN_ToTisk, List<Fask.Interfaces.DataSets.Vyroba.Production_SourcesRow> Production_Sources_selected_Rows, string nazevVychTiskarny)
        {
            try
            {

                #region selected rows

                foreach (var item in Production_Sources_selected_Rows)
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

                if (Production_Sources_selectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                //if (FASK_ZASOBY_selectedRows.Count > 1)
                //{
                //    //MessageBox.Show("Je vybráno více záznamů než jeden.", this.Text, MessageBoxButtons.OK);
                //    //return;
                //}

                Fask.Interfaces.DataSets.Vyroba.Production_SourcesDataTable dt = new Fask.Interfaces.DataSets.Vyroba.Production_SourcesDataTable();

                //dt.Columns.Add("VNDITNUM_IMG", typeof(string));
                //dt.Columns.Add("BarcodeP_IMG", typeof(string));
                dt.Columns.Add("SOPNUMBE_IMG", typeof(string));
                dt.Columns.Add("ITEMNMBR_IMG", typeof(string));
                dt.Columns.Add("BarcodeP_IMG", typeof(string));
                dt.Columns.Add("VNDITNUM_IMG", typeof(string));

                dt.Columns.Add("A_2D_DataMatrix_IMG", typeof(string));
                dt.Columns.Add("A_2D_DataMatrix_kod", typeof(string));
            


                foreach (Fask.Interfaces.DataSets.Vyroba.Production_SourcesRow row in Production_Sources_selectedRows)
                {
                    try
                    {
                        Fask.Interfaces.DataSets.Vyroba.Production_SourcesRow newRow = dt.NewProduction_SourcesRow();
                        // Ošetřit delší vstupní pole než počet sloupců tabulky
                        int copyLength = Math.Min(row.ItemArray.Length, dt.Columns.Count);
                        newRow.ItemArray = row.ItemArray.Take(copyLength).ToArray();
                        dt.AddProduction_SourcesRow(newRow);
                    }
                    catch (Exception ex)
                    {
                        // Ošetření vyjimky
                        // Můžete zde provést logování chyby nebo jiné požadované akce
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                    }


                }

                #region 1.10.2025 MaR navrh CK pro promenne
                //foreach (Fask.Interfaces.DataSets.Vyroba.Production_SourcesRow row in dt)
                //{

                //    try
                //    {
                //        if (!row.IsVNDITNUMNull() && !string.IsNullOrEmpty(row.VNDITNUM))
                //        {
                //            ZXing.BarcodeWriter zw = new ZXing.BarcodeWriter();
                //            zw.Format = ZXing.BarcodeFormat.CODE_128;
                //            zw.Options.Height = 50; //50
                //            zw.Options.PureBarcode = true;
                //            System.Drawing.Bitmap image1 = zw.Write(row.VNDITNUM);

                //            System.IO.MemoryStream ms = new System.IO.MemoryStream();
                //            image1.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                //            byte[] imgReportBarcode = ms.ToArray();
                //            ms.Close();

                //            string Base64Imahe = Convert.ToBase64String(imgReportBarcode);

                //            row["VNDITNUM_IMG"] = Base64Imahe;
                //        }

                //        if (!row.IsCZ_CarKodNull() && !string.IsNullOrEmpty(row.CZ_CarKod))
                //        {
                //            ZXing.BarcodeWriter zw = new ZXing.BarcodeWriter();
                //            zw.Format = ZXing.BarcodeFormat.CODE_128;
                //            zw.Options.Height = 50; //50
                //            zw.Options.PureBarcode = true;
                //            System.Drawing.Bitmap image1 = zw.Write(row.CZ_CarKod);

                //            System.IO.MemoryStream ms = new System.IO.MemoryStream();
                //            image1.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                //            byte[] imgReportBarcode = ms.ToArray();
                //            ms.Close();

                //            string Base64Imahe = Convert.ToBase64String(imgReportBarcode);

                //            row["CZ_CarKod_IMG"] = Base64Imahe;

                //        }

                //        if (!row.IsLOCNCODENull() && !string.IsNullOrEmpty(row.LOCNCODE))
                //        {
                //            ZXing.BarcodeWriter zw = new ZXing.BarcodeWriter();
                //            zw.Format = ZXing.BarcodeFormat.CODE_128;
                //            zw.Options.Height = 50; //50
                //            zw.Options.PureBarcode = true;
                //            System.Drawing.Bitmap image1 = zw.Write(row.LOCNCODE);

                //            System.IO.MemoryStream ms = new System.IO.MemoryStream();
                //            image1.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                //            byte[] imgReportBarcode = ms.ToArray();
                //            ms.Close();

                //            string Base64Imahe = Convert.ToBase64String(imgReportBarcode);

                //            row["LOCNCODE_IMG"] = Base64Imahe;

                //        }

                //    }
                //    catch (Exception ex)
                //    {

                //        Fask.Logging.ExceptionHandler2.Handle(ex);
                //    }


                //} 
                #endregion



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



        private void PrintReport_RDLC(Fask.Interfaces.DataSets.Vyroba.Production_SourcesDataTable dt, bool primyTisk, string path_sablona, string nazevVychTiskarny, int pocetVytisku)
        {
            try
            {

                #region logovani tisku
                if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                {
                    foreach (var item in dt)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start vyroba/rozbory/prehled_materialy TISK rdlc-------------------------------------");

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

                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END vyroba/rozbory/prehled_materialy TISK rdlc-------------------------------------");
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

                if (Production_Sources_selectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                #region MaR 6.11.2024 vypis zpl do souboru selected rows
                int index_zaznamu = 0;

                foreach (var item in Production_Sources_selectedRows)
                {

                    if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start vyroba/rozbory/prehled_materialy TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");

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

                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END vyroba/rozbory/prehled_materialy TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");


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



                    foreach (var item in Production_Sources_selectedRows)
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
                    Tisk_selected_rows(plr_Path, true, MN_ToTisk, Production_Sources_selectedRows);

                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Tisk_selected_rows(string plr_Path, bool MnozstvuAutoJedna, int MN_ToTisk, List<Fask.Interfaces.DataSets.Vyroba.Production_SourcesRow> Production_Sources_selected_Rows)
        {
            try
            {
                PrintDialog printDialog1 = new PrintDialog();
                printDialog1.UseEXDialog = true;

                if (printDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.Cancel)
                    return;



                #region selected rows

                foreach (var item in Production_Sources_selected_Rows)
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

        private int? TiskMnozstvi(bool MnozstviAutoJedna)
        {
            try
            {
                string pocetStr = string.Empty;
                int pocetInt = 1;

                if (!MnozstviAutoJedna)
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
            Fask.Interfaces.DataSets.Vyroba.Production_SourcesRow row,
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

                Fask.Interfaces.DataSets.Vyroba.Production_SourcesDataTable dt = new Fask.Interfaces.DataSets.Vyroba.Production_SourcesDataTable();

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

        private Dictionary<string, string> PrepareDataToTisk(Fask.Interfaces.DataSets.Vyroba.Production_SourcesRow Production_SourcesRow_data)
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

                        Fask.Interfaces.DataSets.Vyroba.Production_SourcesDataTable dt = new Fask.Interfaces.DataSets.Vyroba.Production_SourcesDataTable();


                        foreach (DataColumn dcol in dt.Columns)
                        {
                            string key = dcol.ColumnName;
                            string value = Production_SourcesRow_data[dcol.ColumnName].ToString();
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
