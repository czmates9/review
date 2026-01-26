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
using System.Reflection;

namespace Konzola.Vyroba
{
    public partial class FormVazbyAddVyrobek : Form
    {


        /// <summary>
        /// Vybrany filtr Vyrobky
        /// </summary>
        private Fask.Interfaces.Filtry.VazbyMaterialyFiltr rowFiltr_Vyrobky
        {
            get
            {
                try
                {
                    return tscbFiltry_AddVyrobek.SelectedItem as Fask.Interfaces.Filtry.VazbyMaterialyFiltr;
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Seznam vsech nactenych filtru Vyrobky
        /// </summary>
        private List<Fask.Interfaces.Filtry.VazbyMaterialyFiltr> filtry_Vyrobky = new List<Fask.Interfaces.Filtry.VazbyMaterialyFiltr>();

        /// <summary>
        /// Provider pro vyhledavani filtrama
        /// </summary>
        private Fask.Interfaces.IMES providerVazby = null;

        public string ITEMNMBR_First = null;

        public string ITEMNMBR_Zdroj = null;
        public string IDL_Zdroj = null;
        private bool _duplikace = false;


        #region Eventy formu

        public FormVazbyAddVyrobek(bool Duplikace)
        {
            InitializeComponent();
            _duplikace = Duplikace;

            this.dgVyrobek.UpdateColumnHeaderCellsByDatasource();

        }

        private void FormVazbyAddVyrobek_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Shift && !e.Control && !e.Alt)
            {
                if (e.KeyCode == Keys.Escape)
                {
                    PerformCancel();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    buttonNovy_Click(null, null);
                }
                else
                    return;
            }
            else
                return;

            e.Handled = true;
        }

        private void FormVazbyAddVyrobek_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                this.WindowState = FormWindowState.Maximized;
                this.dgVyrobek.LoadConfiguration(this.GetType().ToString());


                //// načtení konfigurace vytvořených filtrů
                this.filtry_Vyrobky = Extensions.ListExt.ReadFromXML<Fask.Interfaces.Filtry.VazbyMaterialyFiltr>(this.GetType().ToString() + ".filtr");
                tscbFiltry_AddVyrobek.ComboBox.DataSource = this.filtry_Vyrobky;
                tscbFiltry_AddVyrobek.SelectedItem = null;
                tscbFiltry_AddVyrobek.ComboBox.DropDownWitdhAutosize();

                InitProvider();

                if (providerVazby == null)
                    throw new Exception("Provider 'Vazby' není inicializován");

                if (_duplikace == true)
                {
                    dgVyrobek.MultiSelect = false;
                }


                PerformVyhledat_Vyrobek();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormVazbyAddVyrobek_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.dgVyrobek.SaveConfiguration(this.GetType().ToString());

                this.filtry_Vyrobky.WriteXML(this.GetType().ToString()  + ".filtr");
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

        private void buttonNovy_Click(object sender, EventArgs e)
        {
            try
            {
                //Production.DataServices.VyrobaDataSetTableAdapters.FASK_Vyroba_TPTableAdapter tpta = new Production.DataServices.VyrobaDataSetTableAdapters.FASK_Vyroba_TPTableAdapter();
                //tpta.Connection = new System.Data.SqlClient.SqlConnection(Properties.Settings.Default.Konzola_ConnectionString);
                Fask.Interfaces.DataSets.Zbozi ds = new Fask.Interfaces.DataSets.Zbozi();


                if (this.dgVyrobek.SelectedRows.Count < 1)
                {
                    MessageBox.Show("Chybý vybrán výrobek.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    return;
                }



                foreach (DataGridViewRow row in this.dgVyrobek.SelectedRows)
                {

                    Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_KONZOLARow rowVyrobek = ds.FASK_ZASOBY_KONZOLA.NewFASK_ZASOBY_KONZOLARow();
                    rowVyrobek = ((DataRowView)row.DataBoundItem).Row as Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_KONZOLARow;


                    int? cnt = null; // tpta.Scalar_Vyrobky_Count(rowVyrobek.ITEMNMBR.Trim());



                    if (providerVazby is Fask.Interfaces.Vazby.IVazby2_Scalar_Vyrobky_Count)
                    {
                        cnt = ((Fask.Interfaces.Vazby.IVazby2_Scalar_Vyrobky_Count)providerVazby).Scalar_Vyrobky_Count(rowVyrobek.ITEMNMBR.Trim());
                    }
                    else
                        throw new Exception("IVazby2_Scalar_Vyrobky_Count not implementet");




                    if (cnt > 0 || cnt == null)
                    {
                        MessageBox.Show(string.Format("Výrobek :'{0}' v seznamu již existuje.", rowVyrobek.ITEMDESC.Trim()), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                        continue;
                    }


                    int? IDL = null;


                    if (providerVazby is Fask.Interfaces.Vazby.IVazby2_Scalar_MAX_IDL)
                        IDL = ((Fask.Interfaces.Vazby.IVazby2_Scalar_MAX_IDL)providerVazby).Scalar_MAX_IDL();
                    else
                        throw new Exception("IVazby2_Scalar_MAX_IDL not implementet");


                    int ID_L;

                    if (IDL.HasValue)
                        ID_L = (int)IDL + 1;
                    else
                        ID_L = 100;



                    if (string.IsNullOrEmpty(ITEMNMBR_First))
                        ITEMNMBR_First = rowVyrobek.ITEMNMBR.Trim();

                    if (providerVazby is Fask.Interfaces.Vazby.IVazby2_Insert_VazbyAddVyrobky)
                    {
                        int a = ((Fask.Interfaces.Vazby.IVazby2_Insert_VazbyAddVyrobky)providerVazby).Insert_VazbyAddVyrobky(
                          null,
                          ID_L.ToString().Trim(),
                          rowVyrobek.ITEMNMBR.Trim(),
                          rowVyrobek.ITEMDESC.Trim(),
                          rowVyrobek.MJ.Trim(),
                          null,
                          null,
                          null,
                          null,
                          FASK.Logins.Uzivatel.Instance.UserID.Trim(),
                          DateTime.Now,
                          null,
                          "1"
                          );
                    }
                    else
                        throw new Exception("IVazby2_Insert_VazbyAddVyrobky not implementet");


                    if (_duplikace)
                    {

                        //ITEMNMBR_Zdroj; // Zdrojovy ITEM
                        Fask.Interfaces.DataSets.Vyroba dsMat = new Fask.Interfaces.DataSets.Vyroba();
                        Fask.Interfaces.Filtry.VazbyMaterialyFiltr filtr = new Fask.Interfaces.Filtry.VazbyMaterialyFiltr();

                        ((Fask.Interfaces.Vazby.IVazby2)providerVazby).ITEMNMBR_Def = ITEMNMBR_Zdroj;
                        ((Fask.Interfaces.Vazby.IVazby2)providerVazby).rowvyrobek_ID_L = IDL_Zdroj.ToString();

                        if (providerVazby is Fask.Interfaces.Vazby.IVazby2_GetFiltrovanyVazbyMaterialy)
                            dsMat = ((Fask.Interfaces.Vazby.IVazby2_GetFiltrovanyVazbyMaterialy)providerVazby).GetFiltrovanyVazbyMaterialy(filtr);
                        else
                            throw new Exception("IVazby2_GetFiltrovanyVazbyMaterialy not implementet");


                        ((Fask.Interfaces.Vazby.IVazby2)providerVazby).ITEMNMBR_Def = String.Empty;
                        ((Fask.Interfaces.Vazby.IVazby2)providerVazby).rowvyrobek_ID_L = String.Empty;

                        if ((dsMat != null) && (dsMat.FASK_Vyroba_TP_Material.Count() > 0))
                        {
                            int ID_Lmat = ID_L;

                            foreach (var PolMat in dsMat.FASK_Vyroba_TP_Material)
                            {

                                if (providerVazby is Fask.Interfaces.Vazby.IVazby2_Insert_VazbyAddVyrobky)
                                {
                                    ID_Lmat++;

                                    int a = ((Fask.Interfaces.Vazby.IVazby2_Insert_VazbyAddVyrobky)providerVazby).Insert_VazbyAddVyrobky(
                             ID_L.ToString(),
                             ID_Lmat.ToString(),
                             rowVyrobek.ITEMNMBR,
                             rowVyrobek.IsITEMDESCNull() ? null : rowVyrobek.ITEMDESC.Trim(),
                             rowVyrobek.MJ,
                             PolMat.IsITEMNMBRNull() ? null : PolMat.ITEMNMBR.Trim(),
                             PolMat.IsITEMDESCNull() ? null : PolMat.ITEMDESC.Trim(),
                             PolMat.IsMJNull() ? null : PolMat.MJ.Trim(),
                             PolMat.IskoefNull() ? null : PolMat.koef.Trim(),
                             FASK.Logins.Uzivatel.Instance.UserID.Trim(),
                             DateTime.Now,
                             PolMat.IsalterNull() ? null : PolMat.alter.Trim(),
                             PolMat.IsPUONull() ? "0" : PolMat.PUO.Trim());
                                }
                                else
                                    throw new Exception("IVazby2_Insert_VazbyAddVyrobky not implementet");
                            }
                        }

                    }


                }

                PerformCancel();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonKonec_Click(object sender, EventArgs e)
        {
            PerformCancel();
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

        #region Filtry


        #region FILTRY
        /// <summary>
        /// Nastavi zvoleny filtr do comboboxu, ...
        /// </summary>
        /// <param name="filtr"></param>
        private void PerformNastavitFiltr_Vyrobek(Fask.Interfaces.Filtry.VazbyMaterialyFiltr filtr)
        {
            try
            {
                if (filtr == null)
                    return;

                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                PerformVycistitFiltr_Vyrobek();

                // TODO naplneni comboboxu


                comboBox_Vyrobek_ITEMNMBR.Text = filtr.MaterialITEMNMBR;
                comboBox_Vyrobek_LOCNCODE.Text = filtr.MaterialLocncode;
                comboBox_Vyrobek_SKL_ID.Text = filtr.MaterialSklID;
                comboBox_Vyrobek_ITEMDESC.Text = filtr.MaterialITEMDESC;
                comboBox_Vyrobek_VNDITNUM.Text = filtr.MaterialVNDITNUM;
                comboBox_Vyrobek_CZ_CarKod.Text = filtr.MaterialCarKod;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show("Nepodařilo se načíst data filtru.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PerformVycistitFiltr_Vyrobek()
        {
            try
            {
                // ToDo vycisteni Comboboxu 
                comboBox_Vyrobek_ITEMNMBR.SelectedItem =
                comboBox_Vyrobek_LOCNCODE.SelectedItem =
                comboBox_Vyrobek_SKL_ID.SelectedItem =
                comboBox_Vyrobek_ITEMDESC.SelectedItem =
                comboBox_Vyrobek_ITEMDESC.SelectedItem =
                comboBox_Vyrobek_CZ_CarKod.SelectedItem = null;


                comboBox_Vyrobek_ITEMNMBR.Text =
                comboBox_Vyrobek_LOCNCODE.Text =
                comboBox_Vyrobek_SKL_ID.Text =
                comboBox_Vyrobek_ITEMDESC.Text =
                comboBox_Vyrobek_VNDITNUM.Text =
                comboBox_Vyrobek_CZ_CarKod.Text = string.Empty;
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
        private void PerformZmenitFiltr_Vyrobek()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (rowFiltr_Vyrobky == null)
                    return;

                DialogResult dr = MessageBox.Show("Opravdu chcete změnit vybraný filtr '" + rowFiltr_Vyrobky.NazevFiltru.Trim() + "'?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr != System.Windows.Forms.DialogResult.Yes)
                    return;

                Fask.Interfaces.Filtry.VazbyMaterialyFiltr filtr = rowFiltr_Vyrobky;

                if (!CreateFilter_Vyrobek(ref filtr))
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
        private bool CreateFilter_Vyrobek(ref Fask.Interfaces.Filtry.VazbyMaterialyFiltr filtr)
        {
            if (filtr == null)
                filtr = new Fask.Interfaces.Filtry.VazbyMaterialyFiltr();


            // nacteni z ComboBoxu do Filtru
            filtr.MaterialITEMNMBR = comboBox_Vyrobek_ITEMNMBR.Text.Trim();
            filtr.MaterialLocncode = comboBox_Vyrobek_LOCNCODE.Text.Trim();
            filtr.MaterialSklID = comboBox_Vyrobek_SKL_ID.Text.Trim();
            filtr.MaterialITEMDESC = comboBox_Vyrobek_ITEMDESC.Text.Trim();
            filtr.MaterialVNDITNUM = comboBox_Vyrobek_VNDITNUM.Text.Trim();
            filtr.MaterialCarKod = comboBox_Vyrobek_CZ_CarKod.Text.Trim();


            return true;
        }

        /// <summary>
        /// Ulozeni filtru do souboru.
        /// </summary>
        private void PerformPridatFiltr_Vyrobek()
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

                bool result = CreateFilter_Vyrobek(ref filtr);
                if (result)
                {
                    filtr.NazevFiltru = nazev;
                    filtry_Vyrobky.Add(filtr);
                    //this.tscbFiltry.Items.Clear();
                    this.tscbFiltry_AddVyrobek.ComboBox.DataSource = null;
                    this.tscbFiltry_AddVyrobek.ComboBox.DataSource = filtry_Vyrobky;
                    this.tscbFiltry_AddVyrobek.SelectedItem = filtr;
                    tscbFiltry_AddVyrobek.ComboBox.DropDownWitdhAutosize();
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
        private void PerformOdebratFiltr_Vyrobek()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (rowFiltr_Vyrobky == null)
                    return;

                DialogResult dr = MessageBox.Show("Opravdu chcete odstranit filtr '" + (string.IsNullOrEmpty(rowFiltr_Vyrobky.NazevFiltru) ? string.Empty : rowFiltr_Vyrobky.NazevFiltru.Trim()) + "'?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr != System.Windows.Forms.DialogResult.Yes)
                    return;

                filtry_Vyrobky.Remove(rowFiltr_Vyrobky);
                this.tscbFiltry_AddVyrobek.ComboBox.DataSource = null;
                this.tscbFiltry_AddVyrobek.ComboBox.DataSource = filtry_Vyrobky;
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
        private void PerformVyhledat_Vyrobek()
        {
            try
            {
                DataTable dtchanged = this.dsVyrobek.FASK_ZASOBY_KONZOLA.GetChanges();
                if (dtchanged != null && dtchanged.Rows.Count > 0) //obsahuje zmeny ... 
                {
                    DialogResult dr = MessageBox.Show("Obsahuje změny, pokračovat?", "data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == System.Windows.Forms.DialogResult.No)
                        return;
                }

                if (bw_vyrobek.IsBusy)
                {
                    bw_vyrobek.CancelAsync();
                    while (bw_vyrobek.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }

                ProgressIndicatorVyrobekStart();

                Fask.Interfaces.Filtry.VazbyMaterialyFiltr filtr = new Fask.Interfaces.Filtry.VazbyMaterialyFiltr();
                if (!CreateFilter_Vyrobek(ref filtr))
                    return;

                int FirstDisplayedScrollingRowIndex = this.dgVyrobek.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

                bw_vyrobek.RunWorkerAsync(filtr);

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

        #region CLICKY

        private void button_Filtr_Vyrobky_Click(object sender, EventArgs e)
        {
            PerformVyhledat_Vyrobek();
        }

        private void tsbNastavit_AddVyrobek_Click(object sender, EventArgs e)
        {
            PerformNastavitFiltr_Vyrobek(rowFiltr_Vyrobky);
        }

        private void tsbZmena_AddVyrobek_Click(object sender, EventArgs e)
        {
            PerformZmenitFiltr_Vyrobek();
        }

        private void tsbPridat_AddVyrobek_Click(object sender, EventArgs e)
        {
            PerformPridatFiltr_Vyrobek();
        }

        private void tsbOdebrat_AddVyrobek_Click(object sender, EventArgs e)
        {
            PerformOdebratFiltr_Vyrobek();
        }

        private void tsbVycistit_AddVyrobek_Click(object sender, EventArgs e)
        {
            PerformVycistitFiltr_Vyrobek();
        }

        #endregion


        #region BACKGROUNWORKER

        private void bw_vyrobek_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Fask.Interfaces.Filtry.VazbyMaterialyFiltr filtr = (Fask.Interfaces.Filtry.VazbyMaterialyFiltr)e.Argument;
                Fask.Interfaces.DataSets.Zbozi ds = new Fask.Interfaces.DataSets.Zbozi();
                //Fask.Interfaces.DataSets.SkladLokace ds = new Fask.Interfaces.DataSets.SkladLokace();

                if (bw_vyrobek.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                //providerVazby.ITEMNMBR_Materialy = rowVyrobek.ITEMNMBR;

                // nacteni dat v oddelenem vlakne
                //ds = ((Fask.Interfaces.Vazby.IVazby2)providerVazby).GetFiltrovanyVazbyAddVyrobky(filtr);

                if (providerVazby is Fask.Interfaces.Vazby.IVazby2_GetFiltrovanyVazbyAddVyrobky)
                    ds = ((Fask.Interfaces.Vazby.IVazby2_GetFiltrovanyVazbyAddVyrobky)providerVazby).GetFiltrovanyVazbyAddVyrobky(filtr);
                else
                    throw new Exception("IVazby2_GetFiltrovanyVazbyAddVyrobky not implementet");

                //providerVazby.ITEMNMBR_Materialy = String.Empty;

                if (bw_vyrobek.CancellationPending)
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

        private void bw_vyrobek_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                // check error, check cancel, then use result
                if (e.Error != null)
                {
                    // handle the error
                    dsVyrobek = new Fask.Interfaces.DataSets.Zbozi();
                    bsVyrobek.DataSource = dsVyrobek;
                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, e.Error);
                    MessageBox.Show(e.Error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (e.Cancelled)
                {
                    // handle cancellation
                    dsVyrobek = new Fask.Interfaces.DataSets.Zbozi();
                    bsVyrobek.DataSource = dsVyrobek;
                }
                else
                {
                    // uspesne dokonceno ...
                    // use it on the UI thread
                    dsVyrobek = (Fask.Interfaces.DataSets.Zbozi)e.Result;
                    if (dsVyrobek == null)
                        dsVyrobek = new Fask.Interfaces.DataSets.Zbozi();

                    bsVyrobek.DataSource = dsVyrobek;
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

        #endregion

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




        #endregion



    }
}
