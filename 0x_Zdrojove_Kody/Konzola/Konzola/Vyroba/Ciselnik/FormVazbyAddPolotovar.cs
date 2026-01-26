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

namespace Konzola.Vyroba
{
    public partial class FormVazbyAddPolotovar : Form
    {

        public Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TP_VyrobekRow rowVyrobek;


        /// <summary>
        /// Seznam vsech nactenych filtru Materialy
        /// </summary>
        private List<Fask.Interfaces.Filtry.VazbyMaterialyFiltr> filtry_AddMaterialy = new List<Fask.Interfaces.Filtry.VazbyMaterialyFiltr>();

        /// <summary>
        /// Seznam vsech nactenych filtru Vyrobky
        /// </summary>
        private List<Fask.Interfaces.Filtry.VazbyMaterialyFiltr> filtry_PMaterialy = new List<Fask.Interfaces.Filtry.VazbyMaterialyFiltr>();



        /// <summary>
        /// Provider pro vyhledavani filtrama
        /// </summary>
        private Fask.Interfaces.IMES providerVazby = null;


        /// <summary>
        /// Vybrany filtr Materialy
        /// </summary>
        private Fask.Interfaces.Filtry.VazbyMaterialyFiltr rowFiltr_AddMaterialy
        {
            get
            {
                try
                {
                    return tscbFiltry_AddMaterial.SelectedItem as Fask.Interfaces.Filtry.VazbyMaterialyFiltr;
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Vybrany filtr Vyrobky
        /// </summary>
        private Fask.Interfaces.Filtry.VazbyMaterialyFiltr rowFiltr_PMaterialy
        {
            get
            {
                try
                {
                    return tscbFiltry_pridane_Materialy.SelectedItem as Fask.Interfaces.Filtry.VazbyMaterialyFiltr;
                }
                catch
                {
                    return null;
                }
            }
        }


        #region Eventy formu

        public FormVazbyAddPolotovar()
        {
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);

            InitializeComponent();

            this.dgPridane.UpdateColumnHeaderCellsByDatasource();
            this.dgProduct.UpdateColumnHeaderCellsByDatasource();
        }

        private void FormVazbyAddPolotovar_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                this.WindowState = FormWindowState.Maximized;

                // načtení konfigurace datagridu z nastavení aplikace
                this.dgProduct.LoadConfiguration(this.GetType().ToString() + "AddMaterial");
                this.dgPridane.LoadConfiguration(this.GetType().ToString() + "AddMaterialPridane");

                advancedDataGridViewSearchToolBar_Pridane.SetColumns(dgPridane.Columns);
                advancedDataGridViewSearchToolBar_Product.SetColumns(dgProduct.Columns);

                //// načtení konfigurace vytvořených filtrů
                this.filtry_AddMaterialy = Extensions.ListExt.ReadFromXML<Fask.Interfaces.Filtry.VazbyMaterialyFiltr>(this.GetType().ToString() + "_AddMaterial" + ".filtr");
                tscbFiltry_AddMaterial.ComboBox.DataSource = this.filtry_AddMaterialy;
                tscbFiltry_AddMaterial.SelectedItem = null;
                tscbFiltry_AddMaterial.ComboBox.DropDownWitdhAutosize();

                //// načtení konfigurace vytvořených filtrů
                this.filtry_PMaterialy = Extensions.ListExt.ReadFromXML<Fask.Interfaces.Filtry.VazbyMaterialyFiltr>(this.GetType().ToString() + "_PridaneMaterialy" + ".filtr");
                tscbFiltry_pridane_Materialy.ComboBox.DataSource = this.filtry_PMaterialy;
                tscbFiltry_pridane_Materialy.SelectedItem = null;
                tscbFiltry_pridane_Materialy.ComboBox.DropDownWitdhAutosize();


                InitProvider();

                if (providerVazby == null)
                    throw new Exception("Provider 'Vazby' není inicializován");


                PerformVyhledat();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormVazbyAddPolotovar_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Shift && !e.Control && !e.Alt)
            {
                if (e.KeyCode == Keys.Escape)
                {
                    PerformCancel();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    buttonPridat_Click(null, null);
                }
                else
                    return;
            }
            else
                return;

            e.Handled = true;
        }

        private void FormVazbyAddPolotovar_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.dgProduct.SaveConfiguration(this.GetType().ToString() + "AddMaterial");
                this.dgPridane.SaveConfiguration(this.GetType().ToString() + "AddMaterialPridane");

                this.filtry_AddMaterialy.WriteXML(this.GetType().ToString() + "_AddMaterial" + ".filtr");
                this.filtry_PMaterialy.WriteXML(this.GetType().ToString() + "_PridaneMaterialy" + ".filtr");

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #endregion



        /// <summary>
        /// Inicializace providera Materialy
        /// </summary>
        private void InitProvider()
        {
            try
            {
                if (String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                    return;

                if (providerVazby == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Vazby.IVazby2).IsAssignableFrom(t))
                            {
                                providerVazby = (Fask.Interfaces.Vazby.IVazby2)providerAssemlby.CreateInstance(t.FullName);
                                if (providerVazby != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                providerVazby.InitProvider();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void buttonOdznacitVse_Click(object sender, EventArgs e)
        {
            try
            {
                this.dgProduct.ClearSelection();
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
                this.dgProduct.SelectAll();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ComboBoxSKL_ID_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void buttonKonec_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void buttonPridat_Click(object sender, EventArgs e)
        {
            if (this.dgProduct.SelectedRows.Count == 0)
            {
                MessageBox.Show("Není vybrán záznam pro přidání", this.Text, MessageBoxButtons.OK);
                return;
            }

            InsertPolotovar();

        }

        public void InsertPolotovar()
        {
            
            Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();

            int cnt = this.dgProduct.SelectedRows.Count;
            int pocet = 1;

            foreach (DataGridViewRow row in this.dgProduct.SelectedRows)
            {

                Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TP_VyrobekRow rowPolotovar ;
                //= ds.FASK_Vyroba_TP_Material.NewFASK_Vyroba_TP_MaterialRow();
                rowPolotovar = ((DataRowView)row.DataBoundItem).Row as Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TP_VyrobekRow;


                if (rowPolotovar.ITEMNMBR == rowVyrobek.ITEMNMBR)
                {
                    pocet++;
                    MessageBox.Show("Nalezen polotovar se shodnim identifikačnym čislem!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    continue;
                }

                using (Konzola.Vyroba.FormVazbyAddMaterialyEdit VazbyAddMaterialyEdit = new Konzola.Vyroba.FormVazbyAddMaterialyEdit())
                {
                    VazbyAddMaterialyEdit.Text = "Přidání polotovaru";
                    VazbyAddMaterialyEdit.edittype = EditType.AddPolotovar;
                    VazbyAddMaterialyEdit.rowVyrobek = rowVyrobek;
                    VazbyAddMaterialyEdit.rowPolotovar = rowPolotovar;

                    VazbyAddMaterialyEdit.ProgressOD = pocet++.ToString("0000");
                    VazbyAddMaterialyEdit.ProgressDO = cnt.ToString("0000");

                    if (VazbyAddMaterialyEdit.ShowDialog() == DialogResult.Cancel)
                        break;
                    

                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                PerformVyhledat_P();

                //Production.DataServices.VyrobaDataSetTableAdapters.FASK_Vyroba_TP_MaterialTableAdapter tacons = new Production.DataServices.VyrobaDataSetTableAdapters.FASK_Vyroba_TP_MaterialTableAdapter();
                //tacons.Connection = new System.Data.SqlClient.SqlConnection(Properties.Settings.Default.Konzola_ConnectionString);
                //tacons.FillByItemnmbr(DataSet_Pridane.FASK_Vyroba_TP_Material, rowVyrobek.ITEMNMBR);
            }
            catch (Exception ex)
            {
                
                                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Progress indikatory

        private void ProgressIndicatorADDStop()
        {
            progressIndicatorAdd.Stop();
            progressIndicatorAdd.Visible = false;
        }

        private void ProgressIndicator_P_Stop()
        {
            progressIndicatorPridane.Stop();
            progressIndicatorPridane.Visible = false;
        }


        private void ProgressIndicatorADDStart()
        {
            try
            {
                // prepocet stredu datagridu
                this.progressIndicatorAdd.Location = new Point(this.dgProduct.Location.X + (this.dgProduct.Width / 2) - (progressIndicatorAdd.Size.Width / 2), this.dgProduct.Location.Y + (this.dgProduct.Height / 2) - (progressIndicatorAdd.Size.Height / 2));
            }
            catch { }
            progressIndicatorAdd.Start();
            progressIndicatorAdd.Visible = true;
        }

        private void ProgressIndicator_P_Start()
        {
            try
            {
                // prepocet stredu datagridu
                this.progressIndicatorPridane.Location = new Point(this.dgPridane.Location.X + (this.dgPridane.Width / 2) - (progressIndicatorPridane.Size.Width / 2), this.dgPridane.Location.Y + (this.dgPridane.Height / 2) - (progressIndicatorPridane.Size.Height / 2));
            }
            catch { }
            progressIndicatorPridane.Start();
            progressIndicatorPridane.Visible = true;
        }


        #endregion


        #region Filtry ADD

        #region FILTRY

        /// <summary>
        /// Nastavi zvoleny filtr do comboboxu, ...
        /// </summary>
        /// <param name="filtr"></param>
        private void PerformNastavitFiltr(Fask.Interfaces.Filtry.VazbyMaterialyFiltr filtr)
        {
            try
            {
                if (filtr == null)
                    return;

                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                PerformVycistitFiltr();

                // TODO naplneni comboboxu


                comboBox_AddMaterial_ITEMNMBR.Text = filtr.MaterialITEMNMBR;
                comboBox_AddMaterial_ITEMDESC.Text = filtr.MaterialITEMDESC;
                comboBox_AddMaterial_MJ.Text = filtr.MaterialVNDITNUM;

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
                // ToDo vycisteni Comboboxu 
                comboBox_AddMaterial_ITEMNMBR.SelectedItem  =
                comboBox_AddMaterial_ITEMDESC.SelectedItem  =
                comboBox_AddMaterial_MJ.SelectedItem  = null;


                comboBox_AddMaterial_ITEMNMBR.Text  =
                comboBox_AddMaterial_ITEMDESC.Text  =
                comboBox_AddMaterial_MJ.Text  =  string.Empty;
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

                if (rowFiltr_AddMaterialy == null)
                    return;

                DialogResult dr = MessageBox.Show("Opravdu chcete změnit vybraný filtr '" + rowFiltr_AddMaterialy.NazevFiltru.Trim() + "'?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr != System.Windows.Forms.DialogResult.Yes)
                    return;

                Fask.Interfaces.Filtry.VazbyMaterialyFiltr filtr = rowFiltr_AddMaterialy;

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
        /// Vytvoreni filtru, ktery se pouzije.
        /// </summary>
        /// <param name="filtr"></param>
        /// <returns></returns>
        private bool CreateFilter(ref Fask.Interfaces.Filtry.VazbyMaterialyFiltr filtr)
        {
            if (filtr == null)
                filtr = new Fask.Interfaces.Filtry.VazbyMaterialyFiltr();


            // nacteni z ComboBoxu do Filtru
            filtr.MaterialITEMNMBR = comboBox_AddMaterial_ITEMNMBR.Text.Trim();


            filtr.MaterialITEMDESC = comboBox_AddMaterial_ITEMDESC.Text.Trim();
            filtr.MaterialVNDITNUM = comboBox_AddMaterial_MJ.Text.Trim();
        


            return true;
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

                Fask.Interfaces.Filtry.VazbyMaterialyFiltr filtr = new Fask.Interfaces.Filtry.VazbyMaterialyFiltr();
                string nazev = string.Empty;
                DialogResult dr = Forms.InputBox.Show("Název filtru", "Zadejte název filtru", string.Empty, false, out nazev);
                if (dr != System.Windows.Forms.DialogResult.OK)
                    return;

                bool result = CreateFilter(ref filtr);
                if (result)
                {
                    filtr.NazevFiltru = nazev;
                    filtry_AddMaterialy.Add(filtr);
                    //this.tscbFiltry.Items.Clear();
                    this.tscbFiltry_AddMaterial.ComboBox.DataSource = null;
                    this.tscbFiltry_AddMaterial.ComboBox.DataSource = filtry_AddMaterialy;
                    this.tscbFiltry_AddMaterial.SelectedItem = filtr;
                    tscbFiltry_AddMaterial.ComboBox.DropDownWitdhAutosize();
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

                if (rowFiltr_AddMaterialy == null)
                    return;

                DialogResult dr = MessageBox.Show("Opravdu chcete odstranit filtr '" + (string.IsNullOrEmpty(rowFiltr_AddMaterialy.NazevFiltru) ? string.Empty : rowFiltr_AddMaterialy.NazevFiltru.Trim()) + "'?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr != System.Windows.Forms.DialogResult.Yes)
                    return;

                filtry_AddMaterialy.Remove(rowFiltr_AddMaterialy);
                this.tscbFiltry_AddMaterial.ComboBox.DataSource = null;
                this.tscbFiltry_AddMaterial.ComboBox.DataSource = filtry_AddMaterialy;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show("Nepodařilo se odstranit filtr.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Vyhledani podle zadaneho filtru.
        /// </summary>
        private void PerformVyhledat()
        {
            try
            {
                DataTable dtchanged = this.dsProduct.FASK_Vyroba_TP_Vyrobek.GetChanges();
                if (dtchanged != null && dtchanged.Rows.Count > 0) //obsahuje zmeny ... 
                {
                    DialogResult dr = MessageBox.Show("Obsahuje změny, pokračovat?", "data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == System.Windows.Forms.DialogResult.No)
                        return;
                }

                if (bw_Add_Material.IsBusy)
                {
                    bw_Add_Material.CancelAsync();
                    while (bw_Add_Material.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }

                ProgressIndicatorADDStart();

                Fask.Interfaces.Filtry.VazbyMaterialyFiltr filtr = new Fask.Interfaces.Filtry.VazbyMaterialyFiltr();
                if (!CreateFilter(ref filtr))
                    return;

                int FirstDisplayedScrollingRowIndex = this.dgProduct.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

                bw_Add_Material.RunWorkerAsync(filtr);

                if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dgProduct.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dgProduct.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index
            }
            catch (Exception ex)
            {
                ProgressIndicatorADDStop();
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region CLICK

        private void tsbNastavit_ADD_Click(object sender, EventArgs e)
        {
            PerformNastavitFiltr(rowFiltr_AddMaterialy);
        }

        private void tsbZmena_ADD_Click(object sender, EventArgs e)
        {
            PerformZmenitFiltr();
        }

        private void tsbPridat_ADD_Click(object sender, EventArgs e)
        {
            PerformPridatFiltr();
        }

        private void tsbOdebrat_ADD_Click(object sender, EventArgs e)
        {
            PerformOdebratFiltr();
        }

        private void tsbVycistit_ADD_Click(object sender, EventArgs e)
        {
            PerformVycistitFiltr();
        }

        private void button_filtr_ADD_Material_Click(object sender, EventArgs e)
        {
            //Todo filtrovani 
            PerformVyhledat();
        }

        #endregion

        #region BACKGRUND workery

        private void bw_MaterialyAdd_stav_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Fask.Interfaces.Filtry.VazbyMaterialyFiltr filtr = (Fask.Interfaces.Filtry.VazbyMaterialyFiltr)e.Argument;
                Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();
                //Fask.Interfaces.DataSets.SkladLokace ds = new Fask.Interfaces.DataSets.SkladLokace();

                if (bw_Add_Material.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                //providerVazby.ITEMNMBR_Materialy = rowVyrobek.ITEMNMBR;

                // nacteni dat v oddelenem vlakne
                //ds = ((Fask.Interfaces.Vazby.IVazby)providerVazby).GetFiltrovanyVazbyVyrobky(filtr);

                if (providerVazby is Fask.Interfaces.Vazby.IVazby2_GetFiltrovanyVazbyVyrobky)
                    ds = ((Fask.Interfaces.Vazby.IVazby2_GetFiltrovanyVazbyVyrobky)providerVazby).GetFiltrovanyVazbyVyrobky(filtr);
                else
                    throw new Exception("IVazby2_GetFiltrovanyVazbyVyrobky not implementet");

                //providerVazby.ITEMNMBR_Materialy = String.Empty;

                if (bw_Add_Material.CancellationPending)
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

        private void bw_MaterialyAdd_stav_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                // check error, check cancel, then use result
                if (e.Error != null)
                {
                    // handle the error
                    dsProduct = new Fask.Interfaces.DataSets.Vyroba();
                    bsProduct.DataSource = dsProduct;
                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, e.Error);
                    MessageBox.Show(e.Error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (e.Cancelled)
                {
                    // handle cancellation
                    dsProduct = new Fask.Interfaces.DataSets.Vyroba();
                    bsProduct.DataSource = dsProduct;
                }
                else
                {
                    // uspesne dokonceno ...
                    // use it on the UI thread
                    dsProduct = (Fask.Interfaces.DataSets.Vyroba)e.Result;
                    if (dsProduct == null)
                        dsProduct = new Fask.Interfaces.DataSets.Vyroba();

                    bsProduct.DataSource = dsProduct;
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ProgressIndicatorADDStop();
            }
        }

        #endregion



        #endregion


        #region Filtry Pridane


        #region FILTRY
        /// <summary>
        /// Nastavi zvoleny filtr do comboboxu, ...
        /// </summary>
        /// <param name="filtr"></param>
        private void PerformNastavitFiltr_P(Fask.Interfaces.Filtry.VazbyMaterialyFiltr filtr)
        {
            try
            {
                if (filtr == null)
                    return;

                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                PerformVycistitFiltr_P();

                // TODO naplneni comboboxu


                comboBox_P_Material_ITEMNMBR.Text = filtr.MaterialITEMNMBR;
                comboBox_P_Material_ITEMDESC.Text = filtr.MaterialITEMDESC;
                comboBox_P_Material_MJ.Text = filtr.MaterialVNDITNUM;
     
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show("Nepodařilo se načíst data filtru.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PerformVycistitFiltr_P()
        {
            try
            {
                // ToDo vycisteni Comboboxu 
                comboBox_P_Material_ITEMNMBR.SelectedItem =
                comboBox_P_Material_ITEMDESC.SelectedItem =
                comboBox_P_Material_MJ.SelectedItem = null;


                comboBox_P_Material_ITEMNMBR.Text =
                comboBox_P_Material_ITEMDESC.Text =
                comboBox_P_Material_MJ.Text = string.Empty;
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
        private void PerformZmenitFiltr_P()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (rowFiltr_PMaterialy == null)
                    return;

                DialogResult dr = MessageBox.Show("Opravdu chcete změnit vybraný filtr '" + rowFiltr_PMaterialy.NazevFiltru.Trim() + "'?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr != System.Windows.Forms.DialogResult.Yes)
                    return;

                Fask.Interfaces.Filtry.VazbyMaterialyFiltr filtr = rowFiltr_PMaterialy;

                if (!CreateFilter_P(ref filtr))
                    return;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show("Nepodařilo se upravit filtr.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Vytvoreni filtru, ktery se pouzije.
        /// </summary>
        /// <param name="filtr"></param>
        /// <returns></returns>
        private bool CreateFilter_P(ref Fask.Interfaces.Filtry.VazbyMaterialyFiltr filtr)
        {
            if (filtr == null)
                filtr = new Fask.Interfaces.Filtry.VazbyMaterialyFiltr();


            // nacteni z ComboBoxu do Filtru
            filtr.MaterialITEMNMBR = comboBox_P_Material_ITEMNMBR.Text.Trim();

            filtr.MaterialITEMDESC = comboBox_P_Material_ITEMDESC.Text.Trim();
            filtr.MaterialVNDITNUM = comboBox_P_Material_MJ.Text.Trim();


            return true;
        }

        /// <summary>
        /// Ulozeni filtru do souboru.
        /// </summary>
        private void PerformPridatFiltr_P()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                Fask.Interfaces.Filtry.VazbyMaterialyFiltr filtr = new Fask.Interfaces.Filtry.VazbyMaterialyFiltr();
                string nazev = string.Empty;
                DialogResult dr = Forms.InputBox.Show("Název filtru", "Zadejte název filtru", string.Empty, false, out nazev);
                if (dr != System.Windows.Forms.DialogResult.OK)
                    return;

                bool result = CreateFilter_P(ref filtr);
                if (result)
                {
                    filtr.NazevFiltru = nazev;
                    filtry_PMaterialy.Add(filtr);
                    //this.tscbFiltry.Items.Clear();
                    this.tscbFiltry_pridane_Materialy.ComboBox.DataSource = null;
                    this.tscbFiltry_pridane_Materialy.ComboBox.DataSource = filtry_PMaterialy;
                    this.tscbFiltry_pridane_Materialy.SelectedItem = filtr;
                    tscbFiltry_pridane_Materialy.ComboBox.DropDownWitdhAutosize();
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
        private void PerformOdebratFiltr_P()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (filtry_PMaterialy == null)
                    return;

                DialogResult dr = MessageBox.Show("Opravdu chcete odstranit filtr '" + (string.IsNullOrEmpty(rowFiltr_PMaterialy.NazevFiltru) ? string.Empty : rowFiltr_PMaterialy.NazevFiltru.Trim()) + "'?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr != System.Windows.Forms.DialogResult.Yes)
                    return;

                filtry_PMaterialy.Remove(rowFiltr_PMaterialy);
                this.tscbFiltry_pridane_Materialy.ComboBox.DataSource = null;
                this.tscbFiltry_pridane_Materialy.ComboBox.DataSource = filtry_PMaterialy;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show("Nepodařilo se odstranit filtr.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Vyhledani podle zadaneho filtru.
        /// </summary>
        private void PerformVyhledat_P()
        {
            try
            {
                DataTable dtchanged = this.dsPridane.FASK_Vyroba_TP_Material.GetChanges();
                if (dtchanged != null && dtchanged.Rows.Count > 0) //obsahuje zmeny ... 
                {
                    DialogResult dr = MessageBox.Show("Obsahuje změny, pokračovat?", "data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == System.Windows.Forms.DialogResult.No)
                        return;
                }

                if (bw_P_Material.IsBusy)
                {
                    bw_P_Material.CancelAsync();
                    while (bw_P_Material.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }

                ProgressIndicator_P_Start();

                Fask.Interfaces.Filtry.VazbyMaterialyFiltr filtr = new Fask.Interfaces.Filtry.VazbyMaterialyFiltr();
                if (!CreateFilter_P(ref filtr))
                    return;

                int FirstDisplayedScrollingRowIndex = this.dgPridane.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

                bw_P_Material.RunWorkerAsync(filtr);

                if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dgPridane.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dgPridane.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index
            }
            catch (Exception ex)
            {
                ProgressIndicator_P_Stop();
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #endregion

        #region CLICK

        private void tsbNastavit_P_Click(object sender, EventArgs e)
        {
            PerformNastavitFiltr_P(rowFiltr_PMaterialy);
        }

        private void tsbZmena_P_Click(object sender, EventArgs e)
        {
            PerformZmenitFiltr_P();
        }

        private void tsbPridat_P_Click(object sender, EventArgs e)
        {
            PerformPridatFiltr_P();
        }

        private void tsbOdebrat_P_Click(object sender, EventArgs e)
        {
            PerformOdebratFiltr_P();
        }

        private void tsbVycistit_P_Click(object sender, EventArgs e)
        {
            PerformVycistitFiltr_P();
        }

        private void button_Filtr_P_Click(object sender, EventArgs e)
        {
            PerformVyhledat_P();
        }

        #endregion

        #region BACKGRUND workery

        private void bw_Vyrobky_stav_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Fask.Interfaces.Filtry.VazbyMaterialyFiltr filtr = (Fask.Interfaces.Filtry.VazbyMaterialyFiltr)e.Argument;
                Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();
                //Fask.Interfaces.DataSets.SkladLokace ds = new Fask.Interfaces.DataSets.SkladLokace();

                if (bw_P_Material.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                ((Fask.Interfaces.Vazby.IVazby2)providerVazby).ITEMNMBR_Def = rowVyrobek.ITEMNMBR;
                ((Fask.Interfaces.Vazby.IVazby2)providerVazby).rowvyrobek_ID_L = rowVyrobek.ID_L;


                // nacteni dat v oddelenem vlakne
                //ds = ((Fask.Interfaces.Vazby.IVazby2)providerVazby).GetFiltrovanyVazbyMaterialy(filtr);

                if (providerVazby is Fask.Interfaces.Vazby.IVazby2_GetFiltrovanyVazbyMaterialy)
                    ds = ((Fask.Interfaces.Vazby.IVazby2_GetFiltrovanyVazbyMaterialy)providerVazby).GetFiltrovanyVazbyMaterialy(filtr);
                else
                    throw new Exception("IVazby2_GetFiltrovanyVazbyMaterialy not implementet");

                ((Fask.Interfaces.Vazby.IVazby2)providerVazby).ITEMNMBR_Def = String.Empty;
                ((Fask.Interfaces.Vazby.IVazby2)providerVazby).rowvyrobek_ID_L = String.Empty;

                if (bw_P_Material.CancellationPending)
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

        private void bw_Vyrobky_stav_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                // check error, check cancel, then use result
                if (e.Error != null)
                {
                    // handle the error
                    dsPridane = new Fask.Interfaces.DataSets.Vyroba();
                    bsPridane.DataSource = dsPridane;
                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, e.Error);
                    MessageBox.Show(e.Error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (e.Cancelled)
                {
                    // handle cancellation
                    dsPridane = new Fask.Interfaces.DataSets.Vyroba();
                    bsPridane.DataSource = dsPridane;
                }
                else
                {
                    // uspesne dokonceno ...
                    // use it on the UI thread
                    dsPridane = (Fask.Interfaces.DataSets.Vyroba)e.Result;
                    if (dsPridane == null)
                        dsPridane = new Fask.Interfaces.DataSets.Vyroba();

                    bsPridane.DataSource = dsPridane;
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ProgressIndicator_P_Stop();
            }
        }

        #endregion



        #endregion


        private void advancedDataGridViewSearchToolBar_Pridane_Search(object sender, Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs e)
        {


            bool restartsearch = true;
            int startColumn = 0;
            int startRow = 0;
            if (!e.FromBegin)
            {
                bool endcol = dgPridane.CurrentCell.ColumnIndex + 1 >= dgPridane.ColumnCount;
                bool endrow = dgPridane.CurrentCell.RowIndex + 1 >= dgPridane.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dgPridane.CurrentCell.ColumnIndex;
                    startRow = dgPridane.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dgPridane.CurrentCell.ColumnIndex + 1;
                    startRow = dgPridane.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dgPridane.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dgPridane.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dgPridane.CurrentCell = c;



        }


        private void advancedDataGridViewSearchToolBar_Product_Search(object sender, Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs e)
        {


            bool restartsearch = true;
            int startColumn = 0;
            int startRow = 0;
            if (!e.FromBegin)
            {
                bool endcol = dgProduct.CurrentCell.ColumnIndex + 1 >= dgProduct.ColumnCount;
                bool endrow = dgProduct.CurrentCell.RowIndex + 1 >= dgProduct.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dgProduct.CurrentCell.ColumnIndex;
                    startRow = dgProduct.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dgProduct.CurrentCell.ColumnIndex + 1;
                    startRow = dgProduct.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dgProduct.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dgProduct.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dgProduct.CurrentCell = c;



        }

    }
}
