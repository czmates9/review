using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fask.Logging;
using System.Reflection;
using Konzola.Extensions;

namespace Konzola.Ciselniky
{
    public partial class FormLokaceVariantySortimentEdit : Form
    {
        private Fask.Interfaces.IMES providerMapa = null;
        private Fask.Interfaces.IMES providerTypy = null;
        private Fask.Interfaces.IMES providerSklady = null;
        private Fask.Interfaces.IMES providerVarianty = null;
        private Fask.Interfaces.IMES providerZbozi = null;


        /// <summary>
        /// vytvoreny/upraveny zaznam.
        /// </summary>
        public Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_LokaceVariantySortimentRow returnrow { get; set; }
        /// <summary>
        /// Varianta, která se má upravit.
        /// </summary>
        public Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_LokaceVariantySortimentRow variantarow { get; set; }

        /// <summary>
        /// Zvolený typ lokace
        /// </summary>
        private Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_LokaceTypyRow rowTyp
        {
            get
            {
                try
                {
                    return cbbTypLokace.SelectedItem as Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_LokaceTypyRow;
                }
                catch
                {
                    return null;
                }
            }
        }

        public FormLokaceVariantySortimentEdit()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormUzivateleEdit_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                FormUzivateleEdit_Resize(null, null);

                // inicializace providera
                InitProvider();

                if (providerMapa == null)
                    throw new Exception("Provider 'Mapa lokací' není inicializován");

                if (providerSklady == null)
                    throw new Exception("Provider 'Sklady' není inicializován");

                if (providerTypy == null)
                    throw new Exception("Provider 'Typy lokací' není inicializován");

                if (providerVarianty == null)
                    throw new Exception("Provider 'Varianty sortimentu' není inicializován");

                if (providerZbozi == null)
                    throw new Exception("Provider 'Zboží' není inicializován");

                LoadData();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Inicializace providera
        /// </summary>
        private void InitProvider()
        {
            // neni vyplnen provider
            if (String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                return;

            try
            {
                if (providerMapa == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    //Assembly providerAssemlby = Assembly.LoadFrom(Server.MapPath(providerAssemblyPath));
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Ciselniky.SkladLokace_Mapa.ISkladLokace_Mapa2).IsAssignableFrom(t))
                            {
                                providerMapa = (Fask.Interfaces.Ciselniky.SkladLokace_Mapa.ISkladLokace_Mapa2)providerAssemlby.CreateInstance(t.FullName);
                                if (providerMapa != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                providerMapa.InitProvider();
               

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                if (providerVarianty == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    //Assembly providerAssemlby = Assembly.LoadFrom(Server.MapPath(providerAssemblyPath));
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Ciselniky.SkladLokace_LVS.ISkladLokace_LVS2).IsAssignableFrom(t))
                            {
                                providerVarianty = (Fask.Interfaces.Ciselniky.SkladLokace_LVS.ISkladLokace_LVS2)providerAssemlby.CreateInstance(t.FullName);
                                if (providerVarianty != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                providerVarianty.InitProvider();
                
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                if (providerTypy == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    //Assembly providerAssemlby = Assembly.LoadFrom(Server.MapPath(providerAssemblyPath));
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Ciselniky.SkladLokace_LokaceTypy.ISkladLokace_LokaceTypy2).IsAssignableFrom(t))
                            {
                                providerTypy = (Fask.Interfaces.Ciselniky.SkladLokace_LokaceTypy.ISkladLokace_LokaceTypy2)providerAssemlby.CreateInstance(t.FullName);
                                if (providerTypy != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                providerTypy.InitProvider();
             

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
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
                            if (typeof(Fask.Interfaces.Ciselniky.Sklady.ISklady2).IsAssignableFrom(t))
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
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
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
        }

        private void LoadData()
        {
            try
            {
                // naplneni comboboxu typu lokaci
                //Fask.Interfaces.DataSets.SkladLokace ds = ((Fask.Interfaces.Ciselniky.ISkladLokace_LokaceTypy)providerTypy).GetSkladLokace_LokaceTypy();
                Fask.Interfaces.DataSets.SkladLokace ds;

                if ((providerTypy != null) && (providerTypy is Fask.Interfaces.Ciselniky.SkladLokace_LokaceTypy.ISkladLokace_LokaceTypy2_GetSkladLokace_LokaceTypy))
                    ds = ((Fask.Interfaces.Ciselniky.SkladLokace_LokaceTypy.ISkladLokace_LokaceTypy2_GetSkladLokace_LokaceTypy)providerTypy).GetSkladLokace_LokaceTypy();
                else
                    throw new NotImplementedException("Provider neimplementuje ISkladLokace_LokaceTypy2_GetSkladLokace_LokaceTypy.");

                //cbbTypLokace.DataSource = ds.CZMST_SkladLokace_LokaceTypy;
                cbbTypLokace.Items.AddRange(ds.CZMST_SkladLokace_LokaceTypy.Select(null, "TYPE asc"));
                cbbTypLokace.SelectedItem = null;

                // je úprava záznamu, dojde k načtení dat
                if (variantarow != null)
                {
                    tbITEMNMBR.Enabled = false;
                    tbITEMNMBR.Text = variantarow.ITEMNMBR.Trim();
                    tbSKLID.Enabled = false;
                    tbSKLID.Text = variantarow.SKL_ID.Trim();
                    tbLOCNCODE.Text = variantarow.LOCNCODE.Trim();
                    //tbLOCNCODE.Enabled = false;
                    //tbType.Text = variantarow.TYPE.Trim();
                    var nalezeno = ds.CZMST_SkladLokace_LokaceTypy.Where(x => !x.IsTYPENull() && x.TYPE.Trim() == variantarow.TYPE.Trim());
                    if (nalezeno.Count() > 0)
                    {
                        cbbTypLokace.SelectedItem = nalezeno.First();
                    }

                    BTN_SKL_ID_LOCNCODE.Size = new Size(BTN_SKL_ID_LOCNCODE.Width, tbLOCNCODE.Size.Height);
                    BTN_SKL_ID_LOCNCODE.Location = new Point(BTN_SKL_ID_LOCNCODE.Location.X, tbLOCNCODE.Location.Y);
                }
            }
            catch
            {
                throw;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.PerformOK();
        }

        private void buttonStorno_Click(object sender, EventArgs e)
        {
            this.PerformCancel();
        }

        private void FormUzivateleEdit_KeyDown(object sender, KeyEventArgs e)
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

        private void PerformCancel()
        {
            DialogResult = DialogResult.Cancel;
        }

        private void PerformOK()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (!ValidateData())
                    return;

                // porovnani s existujicim zaznamem
                // TODO: do transakce s editaci/pridanim??
                if (variantarow != null)
                {
                    //Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_LokaceVariantySortimentRow stav_test = providerVarianty.GetSkladLokace_LokaceVariantySortimentBySklidLocncodeItemnmbr(tbSKLID.Text.Trim(), tbLOCNCODE.Text.Trim(), tbITEMNMBR.Text.Trim());
                    //Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_LokaceVariantySortimentRow stav_test = ((Fask.Interfaces.Ciselniky.ISkladLokace_LokaceVariantySortiment)providerVarianty).GetSkladLokace_LokaceVariantySortimentBySklidLocncodeItemnmbr(tbSKLID.Text.Trim(), variantarow.LOCNCODE.Trim(), tbITEMNMBR.Text.Trim());
                    Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_LokaceVariantySortimentRow stav_test;

                    if ((providerVarianty != null) && (providerVarianty is Fask.Interfaces.Ciselniky.SkladLokace_LVS.ISkladLokace_LVS2_GetSkladLokace_LokaceVariantySortimentBySklidLocncodeItemnmbr))
                        stav_test = ((Fask.Interfaces.Ciselniky.SkladLokace_LVS.ISkladLokace_LVS2_GetSkladLokace_LokaceVariantySortimentBySklidLocncodeItemnmbr)providerVarianty).GetSkladLokace_LokaceVariantySortimentBySklidLocncodeItemnmbr(tbSKLID.Text.Trim(), variantarow.LOCNCODE.Trim(), tbITEMNMBR.Text.Trim());
                    else
                        throw new NotImplementedException("Provider neimplementuje ISkladLokace_LokaceVariantySortiment2_GetSkladLokace_LokaceVariantySortimentBySklidLocncodeItemnmbr.");


                    if (stav_test == null)
                    {
                        MessageBox.Show("Záznam byl od posledního načtení odstraněn.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // kvuli refreshi
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        return;
                    }
                    else
                    {
                        Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_LokaceVariantySortiment_COMPAREDataTable dtCmp = new Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_LokaceVariantySortiment_COMPAREDataTable();
                        dtCmp.ImportRow(variantarow);
                        dtCmp.ImportRow(stav_test);

                        //Fask.Interfaces.DataSets.Ukolovani.CZ_UKOL_COMPAREDataTable dtCmpUkol = new UkolovaniDataset.CZ_UKOL_COMPAREDataTable();
                        //dtCmpUkol.ImportRow(rowUkolEdit);
                        //dtCmpUkol.ImportRow(dtUkol.First());

                        IEqualityComparer<Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_LokaceVariantySortiment_COMPARERow> comparer = DataRowComparer.Default;
                        bool isMatch = comparer.Equals(dtCmp[0], dtCmp[1]);
                        if (!isMatch)
                        {
                            if (DialogResult.Yes != MessageBox.Show("Záznam byl od posledního načtení změněn. Přejete si ho přesto upravit?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                            {
                                // kvuli refreshi
                                this.DialogResult = DialogResult.OK;
                                return;
                            }
                        }
                    }
                }

                Fask.Interfaces.DataSets.SkladLokace ds2 = new Fask.Interfaces.DataSets.SkladLokace();
                Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_LokaceVariantySortimentRow newVariantaRow = ds2.CZMST_SkladLokace_LokaceVariantySortiment.NewCZMST_SkladLokace_LokaceVariantySortimentRow();

                newVariantaRow.ITEMNMBR = tbITEMNMBR.Text.Trim();
                newVariantaRow.SKL_ID = tbSKLID.Text.Trim();
                newVariantaRow.LOCNCODE = tbLOCNCODE.Text.Trim();
                //newVariantaRow.TYPE = tbType.Text.Trim();
                newVariantaRow.TYPE = rowTyp.TYPE.Trim();
                newVariantaRow.SetUserIDNull();
                newVariantaRow.TermID = Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].TerminalID;

                // je úprava záznamu
                if (variantarow != null)
                {
                    //((Fask.Interfaces.Ciselniky.ISkladLokace_LokaceVariantySortiment)providerVarianty).UpdateSkladLokace_LokaceVariantySortiment(newVariantaRow, variantarow.LOCNCODE);

                    if ((providerVarianty != null) && (providerVarianty is Fask.Interfaces.Ciselniky.SkladLokace_LVS.ISkladLokace_LVS2_UpdateSkladLokace_LokaceVariantySortiment))
                        ((Fask.Interfaces.Ciselniky.SkladLokace_LVS.ISkladLokace_LVS2_UpdateSkladLokace_LokaceVariantySortiment)providerVarianty).UpdateSkladLokace_LokaceVariantySortiment(newVariantaRow, variantarow.LOCNCODE);
                    else
                        throw new NotImplementedException("Provider neimplementuje ISkladLokace_LokaceVariantySortiment2_UpdateSkladLokace_LokaceVariantySortiment.");

                }
                else   // nový záznam
                {
                    //((Fask.Interfaces.Ciselniky.ISkladLokace_LokaceVariantySortiment)providerVarianty).InsertSkladLokace_LokaceVariantySortiment(newVariantaRow);

                    if ((providerVarianty != null) && (providerVarianty is Fask.Interfaces.Ciselniky.SkladLokace_LVS.ISkladLokace_LVS2_InsertSkladLokace_LokaceVariantySortiment_Row))
                        ((Fask.Interfaces.Ciselniky.SkladLokace_LVS.ISkladLokace_LVS2_InsertSkladLokace_LokaceVariantySortiment_Row)providerVarianty).InsertSkladLokace_LokaceVariantySortiment(newVariantaRow);
                    else
                        throw new NotImplementedException("Provider neimplementuje ISkladLokace_LokaceVariantySortiment2_InsertSkladLokace_LokaceVariantySortiment_Row.");

                }

                returnrow = newVariantaRow;
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Kontrola vyplněných dat.
        /// </summary>
        /// <returns>True, pokud je vše v pořádku, jinak False</returns>
        private bool ValidateData()
        {
            try
            {
                errorProvider1.Clear();


                if (string.IsNullOrEmpty(tbITEMNMBR.Text.Trim()))
                    errorProvider1.SetError(tbITEMNMBR, "Musíte zadat id materiálu");
                else
                {
                    // kontrola existence itemnmbr
                    Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_ALL_KONZOLARow zbozi = null;

                    if ((providerZbozi != null) && (providerZbozi is Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_GetZboziByID))
                    {
                        zbozi = ((Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_GetZboziByID)providerZbozi).GetZboziByID(tbITEMNMBR.Text.Trim());
                    }
                    else
                    {
                        throw new NotImplementedException("Provider neobsahuje implemetaci IZbozi2_GetZboziByID");
                    }

                    if (zbozi == null)
                        errorProvider1.SetError(tbITEMNMBR, "Materiál s ID '" + tbITEMNMBR.Text.Trim() + "' neexistuje v číselníku zboží");
                }

                // kontrola existence skladu v tabulce czmst093
                if (string.IsNullOrEmpty(tbSKLID.Text.Trim()))
                    errorProvider1.SetError(tbSKLID, "Musíte zadat id skladu");
                else
                {
                    //Fask.Interfaces.DataSets.Sklady.CZMST093Row sklad = ((Fask.Interfaces.Ciselniky.ISklady)providerSklady).GetSkladByID(tbSKLID.Text.Trim());
                    Fask.Interfaces.DataSets.Sklady.CZMST093Row sklad;

                    if ((providerSklady != null) && (providerSklady is Fask.Interfaces.Ciselniky.Sklady.ISklady2_GetSkladByID))
                        sklad = ((Fask.Interfaces.Ciselniky.Sklady.ISklady2_GetSkladByID)providerSklady).GetSkladByID(tbSKLID.Text.Trim());
                    else
                        throw new NotImplementedException("Provider neimplementuje ISklady2_GetSkladByID.");

                    if (sklad == null)
                        errorProvider1.SetError(tbSKLID, "Sklad s ID '" + tbSKLID.Text.Trim() + "' neexistuje v číselníku skladů");
                }

                // kontrola existence typu lokace v tabulce CZMST_SkladLokace_LokaceTypy
                if (rowTyp == null)
                {
                    errorProvider1.SetError(cbbTypLokace, "Musíte zadat typ lokace");
                }
                //if (string.IsNullOrEmpty(tbType.Text.Trim()))
                //    errorProvider1.SetError(tbType, "Musíte zadat typ lokace");
                //else
                //{
                //    Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_LokaceTypyRow sklad = providerTypy.GetSkladLokace_LokaceTypyByType(tbType.Text.Trim());
                //    if (sklad == null)
                //        errorProvider1.SetError(tbType, "Typ lokace s ID '" + tbType.Text.Trim() + "' neexistuje v číselníku typů lokací");
                //}

                // kontrola vyplneni lokace
                if (string.IsNullOrEmpty(tbLOCNCODE.Text.Trim()))
                {
                    errorProvider1.SetError(tbLOCNCODE, "Musí být vyplněna lokace.");
                }

                // kontrola existence lokace a skladu v mape lokaci
                if (!string.IsNullOrEmpty(tbSKLID.Text.Trim()) && !string.IsNullOrEmpty(tbLOCNCODE.Text.Trim()))
                {
                    //Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_MapaRow maparow = ((Fask.Interfaces.Ciselniky.ISkladLokace_Mapa)providerMapa).GetSkladLokace_MapaBySklIDAndLocncode(tbSKLID.Text.Trim(), tbLOCNCODE.Text.Trim());
                    Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_MapaRow maparow;


                    if ((providerMapa != null) && (providerMapa is Fask.Interfaces.Ciselniky.SkladLokace_Mapa.ISkladLokace_Mapa2_GetSkladLokace_MapaBySklIDAndLocncode))
                        maparow = ((Fask.Interfaces.Ciselniky.SkladLokace_Mapa.ISkladLokace_Mapa2_GetSkladLokace_MapaBySklIDAndLocncode)providerMapa).GetSkladLokace_MapaBySklIDAndLocncode(tbSKLID.Text.Trim(), tbLOCNCODE.Text.Trim());
                    else
                        throw new NotImplementedException("Provider neimplementuje ISkladLokace_Mapa2_GetSkladLokace_MapaBySklIDAndLocncode.");

                    if (maparow == null)
                        errorProvider1.SetError(tbSKLID, "Vybraná lokace a sklad neexistují v mapě skladu.");
                }
                else
                {
                    // neni vyplnen sklad nebo lokace, resi se driv ... zde neni potreba
                }

                if (variantarow == null)
                {
                    // vytváří se nový záznam, kontrola existence id
                    //Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_LokaceVariantySortimentRow stav = ((Fask.Interfaces.Ciselniky.ISkladLokace_LokaceVariantySortiment)providerVarianty).GetSkladLokace_LokaceVariantySortimentBySklidLocncodeItemnmbr(tbSKLID.Text.Trim(), tbLOCNCODE.Text.Trim(), tbITEMNMBR.Text.Trim());
                    Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_LokaceVariantySortimentRow stav;

                    if ((providerVarianty != null) && (providerVarianty is Fask.Interfaces.Ciselniky.SkladLokace_LVS.ISkladLokace_LVS2_GetSkladLokace_LokaceVariantySortimentBySklidLocncodeItemnmbr))
                        stav = ((Fask.Interfaces.Ciselniky.SkladLokace_LVS.ISkladLokace_LVS2_GetSkladLokace_LokaceVariantySortimentBySklidLocncodeItemnmbr)providerVarianty).GetSkladLokace_LokaceVariantySortimentBySklidLocncodeItemnmbr(tbSKLID.Text.Trim(), tbLOCNCODE.Text.Trim(), tbITEMNMBR.Text.Trim());
                    else
                        throw new NotImplementedException("Provider neimplementuje ISkladLokace_LokaceVariantySortiment2_GetSkladLokace_LokaceVariantySortimentBySklidLocncodeItemnmbr.");

                    if (stav != null)
                    {
                        tbSKLID.Focus();
                        errorProvider1.SetError(tbSKLID, "Varianta lokace k materiálu '" + tbITEMNMBR.Text.Trim() + "' již existuje");
                    }
                }
                else
                {
                    // editace zaznamu
                    // pokud se locncode lisi, je treba ho vyhledat
                    if (variantarow.LOCNCODE.Trim() != tbLOCNCODE.Text.Trim())
                    {
                        //Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_LokaceVariantySortimentRow stav = ((Fask.Interfaces.Ciselniky.ISkladLokace_LokaceVariantySortiment)providerVarianty).GetSkladLokace_LokaceVariantySortimentBySklidLocncodeItemnmbr(tbSKLID.Text.Trim(), tbLOCNCODE.Text.Trim(), tbITEMNMBR.Text.Trim());
                        Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_LokaceVariantySortimentRow stav;

                        if ((providerVarianty != null) && (providerVarianty is Fask.Interfaces.Ciselniky.SkladLokace_LVS.ISkladLokace_LVS2_GetSkladLokace_LokaceVariantySortimentBySklidLocncodeItemnmbr))
                            stav = ((Fask.Interfaces.Ciselniky.SkladLokace_LVS.ISkladLokace_LVS2_GetSkladLokace_LokaceVariantySortimentBySklidLocncodeItemnmbr)providerVarianty).GetSkladLokace_LokaceVariantySortimentBySklidLocncodeItemnmbr(tbSKLID.Text.Trim(), tbLOCNCODE.Text.Trim(), tbITEMNMBR.Text.Trim());
                        else
                            throw new NotImplementedException("Provider neimplementuje ISkladLokace_LokaceVariantySortiment2_GetSkladLokace_LokaceVariantySortimentBySklidLocncodeItemnmbr.");



                        if (stav != null)
                        {
                            tbSKLID.Focus();
                            errorProvider1.SetError(tbSKLID, "Záznam varianty materiálu pro materiál '" + tbITEMNMBR.Text.Trim() + "' s na lokaci '" + tbLOCNCODE.Text.Trim() + "' již existuje");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return IsAllValid();
        }

        private bool IsAllValid()
        {
            foreach (Control c in panel2.Controls)
                if (errorProvider1.GetError(c) != "")
                    return false;
            return true;
        }

        private void FormUzivateleEdit_Shown(object sender, EventArgs e)
        {
        }

        private void FormUzivateleEdit_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
            buttonStorno.Size = nsize;
        }

        private void buttonOK_Click_1(object sender, EventArgs e)
        {
            this.PerformOK();
        }

        private void formZdrojeEdit_Enter(object sender, EventArgs e)
        {
            tbNapoveda.Text = string.Empty;
            string text = string.Empty;

            try
            {
                if (sender is TextBox)
                {
                    TextBox tb = ((TextBox)sender);
                    if (tbITEMNMBR == tb)
                    {
                        text = "Identifikátor materiálu.";
                    }
                    else if (tbSKLID == tb)  // oznaceni
                    {
                        text = "Identifikátor skladu.";
                    }
                    else if (tbLOCNCODE == tb)  // oznaceni
                    {
                        text = "Označení lokace.";
                    }
                }
                else if (sender is ComboBox)
                {
                    ComboBox cb = ((ComboBox)sender);
                    if (cbbTypLokace == cb)  // oznaceni
                    {
                        text = "Typ lokace z číselníku typů lokací.";
                    }
                }
            }
            catch { }
            finally
            {
                tbNapoveda.Text = text;
            }
        }

        private void btnVybratItemnmbrRucne_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                using (Ciselniky.FormZboziList frmzbozi = new FormZboziList(false, null, Fask.Interfaces.Classes.ZOBRAZENI_TYP.VYBER))
                {
                    frmzbozi.Text = "Přiřadit materiál";

                    if (frmzbozi.ShowDialog(this) != DialogResult.OK)
                        return;

                    tbITEMNMBR.Text = frmzbozi.FASK_ZASOBY_selectedRow.ITEMNMBR;
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BTN_SKL_ID_LOCNCODE_Click(object sender, EventArgs e)
        {
            Hledat_SKL_ID_LOCNCODE();
        }

        private void Hledat_SKL_ID_LOCNCODE()
        {
            using (Konzola.Ciselniky.FormLokaceMapaList fpeck = new FormLokaceMapaList(false, Fask.Interfaces.Classes.ZOBRAZENI_TYP.VYBER))
            {
                fpeck.Text = "Výběr ID skladu a Lokace";
                if (fpeck.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                    return;
                if (variantarow != null)
                {
                    if (tbSKLID.Text.Trim() == fpeck.Lokace_Mapa_selectedRow.SKL_ID.Trim())
                    {
                        tbLOCNCODE.Text = fpeck.Lokace_Mapa_selectedRow.LOCNCODE.Trim();
                    }
                    else
                    {
                        MessageBox.Show(this,"Sklad nesouhlasí s předchozím!",this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1);
                    }
                }
                else
                {
                    tbLOCNCODE.Text = fpeck.Lokace_Mapa_selectedRow.LOCNCODE.Trim();
                    tbSKLID.Text = fpeck.Lokace_Mapa_selectedRow.SKL_ID.Trim();
                }
            }
        }
    }
}
