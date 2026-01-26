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
using FirebirdSql.Data.FirebirdClient;
using System.Reflection;
using System.Xml.Linq;
using System.IO;
using Konzola.Vyroba.Rozbory;
using MST_Print_Server_ZPL_Printing;

namespace Konzola.Inventura
{
    public partial class FormInv_PredlohaList : Form
    {

        #region Parametry

        //private Fask.Interfaces.IVyrobaKonzola providerSklady = null;
        private Fask.Interfaces.IMES providerInventura = null;

        private Fask.Interfaces.IMES providerZbozi = null;
        private Fask.Interfaces.IMES providerTisk = null; //**DONE

        private List<Fask.Interfaces.Filtry.Inv_PredlohaListFiltr> filtry = new List<Fask.Interfaces.Filtry.Inv_PredlohaListFiltr>();
        private Fask.Interfaces.DataSets.Sklady dsSklady = new Fask.Interfaces.DataSets.Sklady();

        /// <summary>
        /// Zvolené id před seřazením
        /// </summary>
        string sortedID = string.Empty;

        public Fask.Interfaces.DataSets.Inventura.CZMST_I1_PredlohaRow CZMST_I1_Predloha_selectedRow
        {
            get
            {
                try
                {
                    return ((DataRowView)(dg_INV.BindingContext[bs_INV].Current)).Row as Fask.Interfaces.DataSets.Inventura.CZMST_I1_PredlohaRow;
                }
                catch
                {
                    return null;
                }
            }




        }

 

        private List<Fask.Interfaces.DataSets.Inventura.CZMST_I1_PredlohaRow> CZMST_I1_Predloha_selectedRows
        {
            //get
            //{
            //    List<Fask.Interfaces.DataSets.Inventura.CZMST_I1_PredlohaRow> rows = new List<Fask.Interfaces.DataSets.Inventura.CZMST_I1_PredlohaRow>();

            //    foreach (DataGridViewRow selectedRow in dg_INV.SelectedRows)
            //    {
            //        Fask.Interfaces.DataSets.Inventura.CZMST_I1_PredlohaRow row = ((DataRowView)selectedRow.DataBoundItem).Row as Fask.Interfaces.DataSets.Inventura.CZMST_I1_PredlohaRow;
            //        rows.Add(row);
            //    }

            //    return rows;
            //}

            get
            {
                return dg_INV.SelectedRows
                    .Cast<DataGridViewRow>()
                    .OrderBy(r => r.Index)               // zarovnání na vizuální pořadí v gridu
                    .Select(r => ((DataRowView)r.DataBoundItem).Row as Fask.Interfaces.DataSets.Inventura.CZMST_I1_PredlohaRow)
                    .Where(r => r != null)
                    .ToList();
            }
        }

        /// <summary>
        /// Vybrany filtr.
        /// </summary>
        private Fask.Interfaces.Filtry.Inv_PredlohaListFiltr rowFiltr
        {
            get
            {
                try
                {
                    return tscbFiltry.SelectedItem as Fask.Interfaces.Filtry.Inv_PredlohaListFiltr;
                }
                catch
                {
                    return null;
                }
            }
        }

         #endregion

        #region Form eventy

        public FormInv_PredlohaList()
        {
            InitializeComponent();
            this.dg_INV.UpdateColumnHeaderCellsByDatasource();
            panelButtons.Menu = menuStrip1;
        }

        private void FormInv_PredlohaList_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                this.WindowState = FormWindowState.Maximized;

                // načtení konfigurace datagridu z nastavení aplikace
                this.dg_INV.LoadConfiguration(this.GetType().ToString());
                panelButtons.LoadConfiguration(this.GetType().ToString());
                panelButtons.Init();
                panelButtons.Size = new Size(85, 700);

                advancedDataGridViewSearchToolBar2.SetColumns(dg_INV.Columns);

                //// načtení konfigurace vytvořených filtrů
                this.filtry = Extensions.ListExt.ReadFromXML<Fask.Interfaces.Filtry.Inv_PredlohaListFiltr>(this.GetType().ToString() + ".filtr");
                tscbFiltry.ComboBox.DataSource = this.filtry;
                tscbFiltry.SelectedItem = null;
                tscbFiltry.ComboBox.DropDownWitdhAutosize();

                // inicializace providera
                InitProvider();

                if (providerInventura == null)
                    throw new Exception("Provider 'Inventura' není inicializován");

                //if (providerSklady == null)
                //    throw new Exception("Provider 'Sklady' není inicializován");

                if (providerZbozi == null)
                    throw new Exception("Provider 'Zasoby' není inicializován");

                //System.Threading.Thread loadThread = new System.Threading.Thread(LoadDataAsync);
                //loadThread.IsBackground = true;
                //loadThread.Start();

                buttonVyhledat.Focus();


                //rb_Zakladni.Checked = true;

                if (!string.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Inventura[0].INVPredloha_Filter))
                {
                    if (rb_alterKody.Name == Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Inventura[0].INVPredloha_Filter)
                    {
                        rb_alterKody.Checked = true;
                        rb_Sarze.Checked = false;
                        rb_Zakladni.Checked = false;

                        AlternativaRazeni_TEXT(3,1);

                    }
                    else if (rb_Sarze.Name == Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Inventura[0].INVPredloha_Filter)
                    {
                        rb_alterKody.Checked = false;
                        rb_Sarze.Checked = true;
                        rb_Zakladni.Checked = false;

                        AlternativaRazeni_TEXT(2,1);

                    }
                    else if (rb_Zakladni.Name == Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Inventura[0].INVPredloha_Filter)
                    {
                        rb_alterKody.Checked = false;
                        rb_Sarze.Checked = false;
                        rb_Zakladni.Checked = true;

                        AlternativaRazeni_TEXT(1,1);

                    }

                }
                else
                {
                    AlternativaRazeni_TEXT(1,1);

                }



            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        /// <summary>
        /// Vytvoreni filtru, ktery se pouzije.
        /// </summary>
        /// <param name="filtr"></param>
        /// <returns></returns>
        private void AlternativaRazeni(ref Fask.Interfaces.Filtry.Inv_PredlohaListFiltr filtr)
        {
            if (filtr == null)
                filtr = new Fask.Interfaces.Filtry.Inv_PredlohaListFiltr();

          
            if (rB_raz_1.Checked)
            {
                filtr.alternativaRazeni = 1;
            }
            else if (rB_raz_2.Checked)
            {
                filtr.alternativaRazeni = 2;
            }
            else if (rB_raz_2.Checked)
            {
                filtr.alternativaRazeni = 3;
            }
            else
            {
                filtr.alternativaRazeni = 0;
            }

        }

        /// <summary>
        /// Vytvoreni filtru, ktery se pouzije.
        /// </summary>
        /// <param name="filtr"></param>
        /// <returns></returns>
        private void AlternativaRazeni_TEXT(int popis, int varianta = 0)
        {
            switch (popis)
            {
                case 0:
                    rB_raz_1.Text = string.Empty;
                    rB_raz_2.Text = string.Empty;
                    rB_raz_3.Text = string.Empty;

                    rB_raz_1.Enabled = false;
                    rB_raz_2.Enabled = false;
                    rB_raz_3.Enabled = false;

                    break;
                case 1:
                    rB_raz_1.Text = "dávka + položka";
                    rB_raz_2.Text = string.Empty;
                    rB_raz_3.Text = string.Empty;

                    rB_raz_1.Enabled = true;
                    rB_raz_2.Enabled = false;
                    rB_raz_3.Enabled = false;
                    break;
                case 2:
                    rB_raz_1.Text = "dávka + položka + šarže";
                    rB_raz_2.Text = "dávka + položka + expirace";
                    rB_raz_3.Text = string.Empty;

                    rB_raz_1.Enabled = true;
                    rB_raz_2.Enabled = true;
                    rB_raz_3.Enabled = false;
                    break;
                case 3:
                    rB_raz_1.Text = "Bez řazení";
                    rB_raz_2.Text = string.Empty;
                    rB_raz_3.Text = string.Empty;

                    rB_raz_1.Enabled = true;
                    rB_raz_2.Enabled = false;
                    rB_raz_3.Enabled = false;
                    break;
                default:
                    rB_raz_1.Text = string.Empty;
                    rB_raz_2.Text = string.Empty;
                    rB_raz_3.Text = string.Empty;

                    rB_raz_1.Enabled = false;
                    rB_raz_2.Enabled = false;
                    rB_raz_3.Enabled = false;
                    break;
            }

            rB_raz_1.Checked = false;
            rB_raz_2.Checked = false;
            rB_raz_3.Checked = false;

            switch (varianta)
            {
                
                case 0:
                    rB_raz_1.Checked = true;
                    break;
                case 1:
                    rB_raz_1.Checked = true;
                    break;
                case 2:
                    rB_raz_2.Checked = true;
                    break;
                case 3:
                    rB_raz_3.Checked = true;
                    break;
                default:
                    rB_raz_1.Checked = true;
                    break;
            }


        }

        public void NastavDefault()
        {
            rb_Zakladni.Checked = true;
            rb_Sarze.Checked = false;
            rb_alterKody.Checked = false;
            AlternativaRazeni_TEXT(1, 1);
        }




        private void FormInv_PredlohaList_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.dg_INV.SaveConfiguration(this.GetType().ToString());
                panelButtons.SaveConfiguration(this.GetType().ToString());

                // ulozeni vsech filtru
                this.filtry.WriteXML(this.GetType().ToString() + ".filtr");

                if(rb_Sarze.Checked)
                    Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Inventura[0].INVPredloha_Filter = rb_Sarze.Name;
                else if (rb_alterKody.Checked)
                    Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Inventura[0].INVPredloha_Filter = rb_alterKody.Name;
                else if (rb_Zakladni.Checked)
                    Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Inventura[0].INVPredloha_Filter = rb_Zakladni.Name;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormInv_PredlohaList_KeyDown(object sender, KeyEventArgs e)
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

        private void FormInv_PredlohaList_Shown(object sender, EventArgs e)
        {
            try
            {
                this.progressIndicator1.Size = new Size(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ostatni[0].ProgressIndicatorSize, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ostatni[0].ProgressIndicatorSize);
                this.progressIndicator1.Location = new Point(this.dg_INV.Location.X + (this.dg_INV.Width / 2) - (progressIndicator1.Size.Width / 2), this.dg_INV.Location.Y + (this.dg_INV.Height / 2) - (progressIndicator1.Size.Height / 2));
            }
            catch { }
        }


        #endregion

        #region Init Provider

        /// <summary>
        /// Inicializace providera
        /// </summary>
        private void InitProvider()
        {
            #region Inventura
            try
            {
                if (!String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                {
                    if (providerInventura == null) //inicializace se provede pouze pokud nebyla provedena ... 
                    {
                        Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                        Type[] types = providerAssemlby.GetTypes();
                        foreach (Type t in types)
                        {
                            try
                            {
                                if (typeof(Fask.Interfaces.Inventura.IInventura2).IsAssignableFrom(t))
                                {
                                    providerInventura = (Fask.Interfaces.Inventura.IInventura2)providerAssemlby.CreateInstance(t.FullName);
                                    if (providerInventura != null)
                                        break;
                                }
                            }
                            catch { }
                        }
                        //return config;
                    }
                    providerInventura.InitProvider();

                   
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            #endregion


            #region Sklady
            //try
            //{
            //    if (!String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
            //    {
            //        if (providerSklady == null) //inicializace se provede pouze pokud nebyla provedena ... 
            //        {
            //            Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
            //            Type[] types = providerAssemlby.GetTypes();
            //            foreach (Type t in types)
            //            {
            //                try
            //                {
            //                    if (typeof(Fask.Interfaces.Ciselniky.Sklady.ISklady2).IsAssignableFrom(t))
            //                    {
            //                        providerSklady = (Fask.Interfaces.Ciselniky.Sklady.ISklady2)providerAssemlby.CreateInstance(t.FullName);
            //                        if (providerSklady != null)
            //                            break;
            //                    }
            //                }
            //                catch { }
            //            }
            //            //return config;
            //        }

            //        // nastaveni connection stringu
            //        //if (providerSklady != null)
            //        //    ((Fask.Interfaces.Ciselniky.ISklady)providerSklady).ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
            //        if ((providerSklady != null) && (providerSklady is Fask.Interfaces.Parametry.IParametry2_ConnectionString))
            //            ((Fask.Interfaces.Parametry.IParametry2_ConnectionString)providerSklady).ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;


            //    }
            //}
            //catch (Exception ex)
            //{
            //    Fask.Logging.ExceptionHandler2.Handle(ex);
            //    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            #endregion

            #region Zasoby

            try
            {
                if (!String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
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
        }


        #endregion

        #region Označ a Odznač vše

        private void buttonOznacitVse_Click(object sender, EventArgs e)
        {
            try
            {
                this.dg_INV.SelectAll();
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
                this.dg_INV.ClearSelection();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #endregion

        #region BackGrounWorker Load Data

        private void bw_INV_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //bwLoadSkladPohyb.CancelAsync();
                Fask.Interfaces.Filtry.Inv_PredlohaListFiltr filtr = (Fask.Interfaces.Filtry.Inv_PredlohaListFiltr)e.Argument;
                Fask.Interfaces.DataSets.Inventura ds = new Fask.Interfaces.DataSets.Inventura();

                if (bw_INV.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                // nacteni dat v oddelenem vlakne
                //ds = ((Fask.Interfaces.Inventura.IInventura)providerInventura).GetFiltrovanaPredloha(filtr);

                if ((providerInventura != null) && (providerInventura is Fask.Interfaces.Inventura.IInventura2_GetFiltrovanaINVPredloha))
                    ds = ((Fask.Interfaces.Inventura.IInventura2_GetFiltrovanaINVPredloha)providerInventura).GetFiltrovanaINVPredloha(filtr);
                else
                    throw new NotImplementedException("Provider neimplementuje IInventura2_GetFiltrovanaINVPredloha.");

                if (bw_INV.CancellationPending)
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

        private void bw_INV_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                // check error, check cancel, then use result
                if (e.Error != null)
                {
                    // handle the error
                    ds_INV = new Fask.Interfaces.DataSets.Inventura();
                    bs_INV.DataSource = ds_INV;
                }
                else if (e.Cancelled)
                {
                    // handle cancellation
                    // TODO: maji se vymazat data z datasetu?? asi ano ...
                    //if (dsSkladPohyb == null)
                    ds_INV = new Fask.Interfaces.DataSets.Inventura();
                    bs_INV.DataSource = ds_INV;
                }
                else
                {
                    // uspesne dokonceno ...
                    // use it on the UI thread
                    ds_INV = (Fask.Interfaces.DataSets.Inventura)e.Result;
                    if (ds_INV == null)
                        ds_INV = new Fask.Interfaces.DataSets.Inventura();

                    bs_INV.DataSource = ds_INV;
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
                this.progressIndicator1.Location = new Point(this.dg_INV.Location.X + (this.dg_INV.Width / 2) - (progressIndicator1.Size.Width / 2), this.dg_INV.Location.Y + (this.dg_INV.Height / 2) - (progressIndicator1.Size.Height / 2));
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

                this.dg_INV.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                this.dg_INV.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
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

                this.dg_INV.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                dg_INV.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
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

                dg_INV.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                dg_INV.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #endregion

        #region Filtry

        #region Click event

        private void tsbZmena_Click(object sender, EventArgs e)
        {
            PerformZmenitFiltr();
        }

        private void tsbOdebrat_Click(object sender, EventArgs e)
        {
            PerformOdebratFiltr();
        }

        private void tsbPridat_Click(object sender, EventArgs e)
        {
            PerformPridatFiltr();
        }

        private void tsbNastavit_Click(object sender, EventArgs e)
        {
            PerformNastavitFiltr(rowFiltr);
        }

        private void tsbVycistit_Click(object sender, EventArgs e)
        {
            PerformVycistitFiltr();
        }


        #endregion

        #region Perform

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

                Fask.Interfaces.Filtry.Inv_PredlohaListFiltr filtr = rowFiltr;

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

        /// <summary>
        /// Ulozeni filtru do souboru.
        /// </summary>
        private void PerformPridatFiltr()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                Fask.Interfaces.Filtry.Inv_PredlohaListFiltr filtr = new Fask.Interfaces.Filtry.Inv_PredlohaListFiltr();
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
        /// Nastavi zvoleny filtr do comboboxu, ...
        /// </summary>
        /// <param name="filtr"></param>
        private void PerformNastavitFiltr(Fask.Interfaces.Filtry.Inv_PredlohaListFiltr filtr)
        {
            try
            {
                if (filtr == null)
                    return;

                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                PerformVycistitFiltr();

                comboBoxCountEntries.Text = filtr.CountEntries.ToString();
                comboBoxITEMNMBR.Text = filtr.ITEMNMBR;
                comboBoxSKLID.Text = filtr.SKL_ID;
                comboBoxLOCNCODE.Text = filtr.LOCNCODE;

                rb_alterKody.Checked = filtr.ZobrazitAlternativnyCaroveKody;
                rb_Sarze.Checked = filtr.ZobrazitSarze;
                rb_Zakladni.Checked = filtr.ZobrazitZakladni;


                int cislo_Popis = 0;
                if(rb_Zakladni.Checked)
                {
                    cislo_Popis = 1;
                }
                else if(rb_Sarze.Checked)
                {
                    cislo_Popis = 2;
                }
                else if (rb_alterKody.Checked)
                {
                    cislo_Popis = 3;
                }
                else 
                {
                    cislo_Popis = 0;
                }


                AlternativaRazeni_TEXT(cislo_Popis,filtr.alternativaRazeni) ;

              



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
                comboBoxCountEntries.SelectedItem = null;
                comboBoxITEMNMBR.SelectedItem = null;
                comboBoxSKLID.SelectedItem = null;
                comboBoxLOCNCODE.SelectedItem = null;

                comboBoxCountEntries.Text = string.Empty;
                comboBoxITEMNMBR.Text = string.Empty;
                comboBoxSKLID.Text = string.Empty;
                comboBoxLOCNCODE.Text = string.Empty;
                NastavDefault();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Vytvoreni filtru, ktery se pouzije.
        /// </summary>
        /// <param name="filtr"></param>
        /// <returns></returns>
        private bool CreateFilter(ref Fask.Interfaces.Filtry.Inv_PredlohaListFiltr filtr)
        {
            if (filtr == null)
                filtr = new Fask.Interfaces.Filtry.Inv_PredlohaListFiltr();

            filtr.CountEntries = comboBoxCountEntries.Text.Trim();
            filtr.ITEMNMBR = comboBoxITEMNMBR.Text.Trim();
            filtr.SKL_ID = comboBoxSKLID.Text.Trim();
            filtr.LOCNCODE = comboBoxLOCNCODE.Text.Trim();
            filtr.ZobrazitAlternativnyCaroveKody = rb_alterKody.Checked;
            filtr.ZobrazitSarze = rb_Sarze.Checked;
            filtr.ZobrazitZakladni = rb_Zakladni.Checked;

            AlternativaRazeni(ref filtr);


            return true;
        }


        #endregion

        #endregion

        #region Click Eventy

        private void buttonVyhledat_Click(object sender, EventArgs e)
        {
            #region prace s datagridem - nastaveni numeric hodnot na pocet desetinnych mist
            try
            {
                // Procházení sloupců v DataGridView
                foreach (DataGridViewColumn column in dg_INV.Columns)
                {
                    // Získání typu dat pro sloupec
                    Type dataType = null;

                    if (dg_INV.DataSource is DataTable dataTable)
                    {
                        // Pokud je datový zdroj DataTable
                        if (dataTable.Columns.Contains(column.DataPropertyName))
                        {
                            dataType = dataTable.Columns[column.DataPropertyName].DataType;
                        }
                    }
                    else if (dg_INV.DataSource is BindingSource bindingSource)
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

            PerformOK();
        }


        private void tsmiKonec_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void tsmiExportPOHODA_Click(object sender, EventArgs e)
        {
            try
            {

                if (bw_Export_Pohoda.IsBusy)
                {
                    bw_Export_Pohoda.CancelAsync();
                    while (bw_Export_Pohoda.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }

                ProgressIndicatorStart();


                bw_Export_Pohoda.RunWorkerAsync();

            }
            catch (Exception ex)
            {
                ProgressIndicatorStop();
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsmiExportXML_Click(object sender, EventArgs e)
        {
            try
            {
                IDictionary<string, string> info = new Dictionary<string, string>();

                ProgressIndicatorStart();

                string CountEntries = string.Empty;

                DialogResult dr1;
                do
                {

                    dr1 = Forms.InputBox.Show("Číslo dávky", "Zadejte číslo dávky.", CountEntries, false, out CountEntries);

                    if (dr1 != System.Windows.Forms.DialogResult.OK)
                    {
                        break;
                    }

                    try
                    {
                        int parse = int.Parse(CountEntries);
                        break;
                    }
                    catch { }


                } while (true);

                if (dr1 != System.Windows.Forms.DialogResult.OK)
                {
                    ProgressIndicatorStop();
                    return;
                }

                info.Add("CountEntries", CountEntries);


                string Desc = string.Empty;
                DialogResult dr2 = Forms.InputBox.Show("Nazev dávky", "Zadejte název dávky.", string.Empty, false, out Desc);

                if (dr2 != System.Windows.Forms.DialogResult.OK)
                {
                    ProgressIndicatorStop();
                    return;
                }

                info.Add("Desc", Desc);

                string filepath = string.Empty;
                DialogResult dr3 = showPathDialog(out filepath);
                if (dr3 != System.Windows.Forms.DialogResult.OK)
                {
                    ProgressIndicatorStop();
                    return;
                }

                info.Add("filepath", filepath);


                if (bw_Export_XML.IsBusy)
                {
                    bw_Export_XML.CancelAsync();
                    while (bw_Export_XML.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }



                bw_Export_XML.RunWorkerAsync(info);

            }
            catch (Exception ex)
            {
                ProgressIndicatorStop();
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsmiExportDBF_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Perform


        private void PerformOK()
        {
            try
            {
                //DataTable dtchanged = this.ds_INV.CZMST_I1.GetChanges();
                //if (dtchanged != null && dtchanged.Rows.Count > 0) //obsahuje zmeny ... 
                //{
                //    //MaR zakomentoval 11.3.2024
                //    //DialogResult dr = MessageBox.Show("Obsahuje změny, pokračovat?", "data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                //    //if (dr == System.Windows.Forms.DialogResult.No)
                //    //    return;
                //}

                if (bw_INV.IsBusy)
                {
                    bw_INV.CancelAsync();
                    while (bw_INV.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }


                ProgressIndicatorStart();

                Fask.Interfaces.Filtry.Inv_PredlohaListFiltr filtr = new Fask.Interfaces.Filtry.Inv_PredlohaListFiltr();
                if (!CreateFilter(ref filtr))
                    return;

                int FirstDisplayedScrollingRowIndex = this.dg_INV.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

                bw_INV.RunWorkerAsync(filtr);

                if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dg_INV.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dg_INV.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index
            }
            catch (Exception ex)
            {
                ProgressIndicatorStop();
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
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

        private DialogResult showPathDialog(out string filepath)
        {
            filepath = string.Empty;
            DialogResult dr;

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
                ofd.InitialDirectory = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Inventura[0].ImportPath;
                ofd.FileName = "stavskladu.xml";
                ofd.FilterIndex = 2;
                ofd.RestoreDirectory = true;
                var x = System.Threading.Thread.CurrentThread.GetApartmentState();
                System.Diagnostics.Debug.WriteLine(x.ToString());
                dr = ofd.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    //Get the path of specified file
                    filepath = ofd.FileName;
                }

            }

            return dr;
        }

        #endregion

        #region DataGridView


        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1)
                {
                    if (CZMST_I1_Predloha_selectedRow != null)
                        sortedID = CZMST_I1_Predloha_selectedRow.DEX_ROW_ID.ToString();
                }
            }
            catch { }
        }

        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            try
            {
                int pos = this.bs_INV.Find(ds_INV.CZMST_I1.DEX_ROW_IDColumn.ColumnName, sortedID);
                this.bs_INV.Position = pos;
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
                bool endcol = dg_INV.CurrentCell.ColumnIndex + 1 >= dg_INV.ColumnCount;
                bool endrow = dg_INV.CurrentCell.RowIndex + 1 >= dg_INV.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dg_INV.CurrentCell.ColumnIndex;
                    startRow = dg_INV.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dg_INV.CurrentCell.ColumnIndex + 1;
                    startRow = dg_INV.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dg_INV.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dg_INV.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dg_INV.CurrentCell = c;

        }




        #endregion

        #region Zmeny barev stloupcu

        private void rb_Zakladni_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                if (!rb_Zakladni.Checked)
                    return;

                AlternativaRazeni_TEXT(1,1);

                Set_SarzeColorColumns(Color.LightGray);
                Set_AlterKodyColorColumns(Color.LightGray);
                PerformOK();
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        }

        private void rb_Sarze_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                if (!rb_Sarze.Checked)
                    return;

                AlternativaRazeni_TEXT(2, 1);

                Set_SarzeColorColumns(Color.Empty);
                Set_AlterKodyColorColumns(Color.LightGray);
                PerformOK();
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        }

        private void rb_alterKody_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                if (!rb_alterKody.Checked)
                    return;


                AlternativaRazeni_TEXT(3, 1);

                Set_SarzeColorColumns(Color.LightGray);
                Set_AlterKodyColorColumns(Color.Empty);
                PerformOK();
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        }

        private void Set_SarzeColorColumns(Color color)
        {
            try
            {

                dg_INV.Columns[I2_CE_Orig.Name].DefaultCellStyle.BackColor = color;
                dg_INV.Columns[I2_DEX_ROW_ID.Name].DefaultCellStyle.BackColor = color;
                dg_INV.Columns[I2_Expirace.Name].DefaultCellStyle.BackColor = color;
                dg_INV.Columns[I2_QTY.Name].DefaultCellStyle.BackColor = color;
                dg_INV.Columns[I2_SERLNMBR.Name].DefaultCellStyle.BackColor = color;
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        }

        private void Set_AlterKodyColorColumns(Color color)
        {
            try
            {

                dg_INV.Columns[I3_CE_Orig.Name].DefaultCellStyle.BackColor = color;
                dg_INV.Columns[I3_CZ_CarKod.Name].DefaultCellStyle.BackColor = color;
                dg_INV.Columns[I3_DEX_ROW_ID.Name].DefaultCellStyle.BackColor = color;
                dg_INV.Columns[I3_MJ.Name].DefaultCellStyle.BackColor = color;
                dg_INV.Columns[I3_QTYPACK.Name].DefaultCellStyle.BackColor = color;
                dg_INV.Columns[I3_VENDNAME.Name].DefaultCellStyle.BackColor = color;
                dg_INV.Columns[I3_VENDORID.Name].DefaultCellStyle.BackColor = color;
                dg_INV.Columns[I3_VNDITNUM.Name].DefaultCellStyle.BackColor = color;
                dg_INV.Columns[I3_WEIGHT.Name].DefaultCellStyle.BackColor = color;
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        }

        private void dg_INV_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //try
            //{
            //    foreach (DataGridViewRow row in dg_INV.Rows)
            //    {

            //        Fask.Interfaces.DataSets.Inventura.CZMST_I1_PredlohaRow radek = (Fask.Interfaces.DataSets.Inventura.CZMST_I1_PredlohaRow)((DataRowView)row.DataBoundItem).Row;

            //        if (radek.CZ_SerNum_Track == 0)
            //        {
            //            row.Cells[I2_CE_Orig.Name].Style.BackColor = Color.Orange;
            //            row.Cells[I2_DEX_ROW_ID.Name].Style.BackColor = Color.Orange;
            //            row.Cells[I2_Expirace.Name].Style.BackColor = Color.Orange;
            //            row.Cells[I2_QTY.Name].Style.BackColor = Color.Orange;
            //            row.Cells[I2_SERLNMBR.Name].Style.BackColor = Color.Orange;
            //        }
            //    }

            //}
            //catch (System.Exception ex)
            //{
            //    Fask.Logging.ExceptionHandler2.Handle(ex);
            //}
        }

        #endregion

        #region Exporty 

        #region BackGroundWorker POHODA

        private void bw_Export_Pohoda_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {

                if (bw_Export_Pohoda.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                Fask.Interfaces.Classes.StatusInfo status;

                if ((providerInventura != null) && (providerInventura is Fask.Interfaces.Inventura.IInventura2_GenerateInventura))
                    status = ((Fask.Interfaces.Inventura.IInventura2_GenerateInventura)providerInventura).GenerateInventura();
                else
                    throw new NotImplementedException("Provider neimplementuje IInventura2_GenerateInventura.");



                if (bw_Export_Pohoda.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                e.Result = status;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void bw_Export_Pohoda_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                //dsVydej = (Fask.Interfaces.DataSets.Vydej)e.Result;
                Fask.Interfaces.Classes.StatusInfo si = (Fask.Interfaces.Classes.StatusInfo)e.Result;

                // check error, check cancel, then use result
                if (e.Error != null)
                {
                    // handle the error

                }
                else if (e.Cancelled)
                {
                    //handle the cancelled
                }
                else
                {
                    // uspesne dokonceno ...


                    if (si.ID == 0)
                    {
                        MessageBox.Show(si.Description, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    else if (si.ID < 0)
                    {
                        MessageBox.Show(si.Description, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    PerformOK();
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

        #region BackGroundWorker XML

        private void bw_Export_XML_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                IDictionary<string, string> info = e.Argument as Dictionary<string, string>;

                if (bw_Export_XML.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                Fask.Interfaces.Classes.StatusInfo status;


                //string filepath = string.Empty;

                //if (showPathDialog(out filepath) != System.Windows.Forms.DialogResult.OK)
                //    return;


                if ((providerInventura != null) && (providerInventura is Fask.Interfaces.Inventura.IInventura2_Generate_FromFile_Inventura))
                    status = ((Fask.Interfaces.Inventura.IInventura2_Generate_FromFile_Inventura)providerInventura).Genetare_FromFile_Inventura(Fask.Interfaces.Inventura.TypeFile.XML, info["CountEntries"], info["Desc"], info["filepath"]);
                else
                    throw new NotImplementedException("Provider neimplementuje IInventura2_Import_FromFile_Inventura.");



                if (bw_Export_XML.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                e.Result = status;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                Fask.Interfaces.Classes.StatusInfo status = new Fask.Interfaces.Classes.StatusInfo(-1, ex.Message);
                e.Result = status;
            }
        }

        private void bw_Export_XML_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                Fask.Interfaces.Classes.StatusInfo si = (Fask.Interfaces.Classes.StatusInfo)e.Result;

                // check error, check cancel, then use result
                if (e.Error != null)
                {
                    // handle the error

                }
                else if (e.Cancelled)
                {
                    //handle the cancelled
                }
                else
                {
                    if (si.ID == 0)
                    {
                        MessageBox.Show(si.Description, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    else if (si.ID < 0)
                    {
                        MessageBox.Show(si.Description, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    PerformOK();
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

        #region DBF

        private const string inventura_dbf = "inventura.dbf";
        private void PrepareDavku()
        {
            try
            {
                // funkce pripravy pro steinex ... 
                string nazevInventury = string.Empty;
                if (DialogResult.Cancel == Konzola.Forms.InputBox.Show("Název inventury", "Inventura " + DateTime.Now.ToString(), out nazevInventury))
                    return;

                // dalsi cislo davky inventury = max + 1;
                int davka = 1;
                System.Data.SqlClient.SqlDataAdapter sqlda = new System.Data.SqlClient.SqlDataAdapter("Select Max(Countentries) from czmst_i1h", Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].FASKDB_ConnesctionString);
                sqlda.SelectCommand.Connection.Open();
                object o = sqlda.SelectCommand.ExecuteScalar();
                sqlda.SelectCommand.Connection.Close();

                if (o != System.DBNull.Value)
                    davka = (int)o + 1;

                OpenFileDialog ofd = new OpenFileDialog();
                if (DialogResult.Cancel == ofd.ShowDialog(this))
                    return;

                //// zalozeni dat inventury ... 
                dataset.dsSBKompletInventura dsinvSB = new dataset.dsSBKompletInventura();

                // vytvori soubor "inventura.dbf" z pozadovaneho
                System.IO.File.Copy(ofd.FileName, System.IO.Path.Combine(System.IO.Path.GetDirectoryName(ofd.FileName), inventura_dbf), true);

                dataset.dsSBKompletInventuraTableAdapters.inventuraTableAdapter tainvSB = new dataset.dsSBKompletInventuraTableAdapters.inventuraTableAdapter();
                tainvSB.Connection = new System.Data.OleDb.OleDbConnection(String.Format("Provider=VFPOLEDB.1;Data Source={0}", ofd.FileName));

                tainvSB.Fill(dsinvSB.inventura);

                //// naplneni dat inventurni predlohy ...
                dataset.dsFaskInventura dsinvFask = new dataset.dsFaskInventura();

                dataset.dsFaskInventuraTableAdapters.CZMST_I1HTableAdapter taI1Hfask = new dataset.dsFaskInventuraTableAdapters.CZMST_I1HTableAdapter();
                dataset.dsFaskInventuraTableAdapters.CZMST_I1TableAdapter taI1fask = new dataset.dsFaskInventuraTableAdapters.CZMST_I1TableAdapter();
                dataset.dsFaskInventuraTableAdapters.CZMST_I3TableAdapter taI3fask = new dataset.dsFaskInventuraTableAdapters.CZMST_I3TableAdapter();

                taI1Hfask.Connection = new System.Data.SqlClient.SqlConnection(Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].FASKDB_ConnesctionString);
                taI1fask.Connection = taI1Hfask.Connection;
                taI3fask.Connection = taI1Hfask.Connection;

                var i1hRow = dsinvFask.CZMST_I1H.NewCZMST_I1HRow();
                i1hRow.CountEntries = davka;
                i1hRow.Description = nazevInventury;
                i1hRow.State = 0;
                dsinvFask.CZMST_I1H.AddCZMST_I1HRow(i1hRow);

                foreach (var i in dsinvSB.inventura)
                {

                    var i1 = dsinvFask.CZMST_I1.NewCZMST_I1Row();
                    var i3 = dsinvFask.CZMST_I3.NewCZMST_I3Row();

                    i1.CountEntries = i3.CountEntries = davka;
                    i1.ITEMNMBR = i3.ITEMNMBR = i.kod.Trim();
                    i1.CZ_CarKod = i3.VNDITNUM = i.kod2.Trim();

                    i1.CZ_SerNum_Find = 0;
                    i1.CZ_SerNum_Track = 0;
                    i1.DATEDONE = DateTime.Now;
                    i1.DMJ = string.Empty;
                    i1.IntegerValue = 0;

                    i1.ITEMDESC = i.nazev.Trim();
                    i1.LOCNCODE = string.Empty;
                    i1.O_TID = 0;
                    i1.QUANTITY = i.stav;
                    i1.REZ_1 = string.Empty;
                    i1.REZ_2 = string.Empty;
                    i1.SKL_ID = string.Empty;
                    i1.TerminalID = 0;
                    i1.TIMESPRT = 0;
                    i1.ITEMCODE = i.kod.Trim();


                    i3.MJ = i.IsjednotkaNull() ? string.Empty : i.jednotka.PadRight(3).Substring(0, 3).Trim();
                    i3.QTYPACK = 0;
                    i3.VENDNAME = i.dodav.Trim();
                    i3.VENDORID = string.Empty;
                    i3.CZ_CarKod = i.kod.Trim();
                    i3.WEIGHT = i.hmotnost;

                    dsinvFask.CZMST_I1.AddCZMST_I1Row(i1);
                    dsinvFask.CZMST_I3.AddCZMST_I3Row(i3);

                    if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Inventura[0].PredlohaAlternaceCarKody)
                    {
                        Fask.Interfaces.DataSets.Zbozi ds = null;

                        Fask.Interfaces.Filtry.ZboziListFiltr filtr = new Fask.Interfaces.Filtry.ZboziListFiltr();
                        filtr.MaterialID = i.kod.Trim();

                        if ((providerZbozi != null) && (providerZbozi is Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_GetFiltrovaneZbozi))
                        {

                            ds = ((Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_GetFiltrovaneZbozi)providerZbozi).GetFiltrovaneZbozi(filtr);
                        }
                        else
                            throw new NotImplementedException("Provider neimplementuje IZbozi2_GetFiltrovaneZbozi.");


                        if ((ds != null) && (ds.FASK_ZASOBY_ALL_KONZOLA != null) && (ds.FASK_ZASOBY_ALL_KONZOLA.Count > 0))
                        {

                            foreach (Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_ALL_KONZOLARow Row in ds.FASK_ZASOBY_ALL_KONZOLA)
                            {

                                var i3_alter = dsinvFask.CZMST_I3.NewCZMST_I3Row();

                                if (Row.VNDITNUM.Trim() == i.kod2.Trim())
                                    continue;

                                i3_alter.CountEntries = davka;
                                i3_alter.ITEMNMBR = Row.ITEMNMBR.Trim();
                                i3_alter.VNDITNUM = Row.IsVNDITNUMNull() ? string.Empty : Row.VNDITNUM.Trim();

                                i3_alter.MJ = Row.MJ.Trim();
                                i3_alter.QTYPACK = Row.IsQTYPACKNull() ? 0 : Row.QTYPACK;
                                i3_alter.VENDNAME = i.dodav.Trim();
                                i3_alter.VENDORID = string.Empty;
                                i3_alter.CZ_CarKod = Row.IsCZ_CarKodNull() ? string.Empty : Row.CZ_CarKod.Trim();
                                i3_alter.WEIGHT = Row.IsWEIGHTNull() ? 0 : Row.WEIGHT;


                                dsinvFask.CZMST_I3.AddCZMST_I3Row(i3_alter);
                            }
                        }
                    }
                }


                dataset.dsFaskInventuraTableAdapters.TableAdapterManager tamanager = new dataset.dsFaskInventuraTableAdapters.TableAdapterManager();
                tamanager.Connection = taI1Hfask.Connection;
                tamanager.CZMST_I1HTableAdapter = taI1Hfask;
                tamanager.CZMST_I1TableAdapter = taI1fask;
                tamanager.CZMST_I3TableAdapter = taI3fask;

                tamanager.UpdateAll(dsinvFask);

                MessageBox.Show(
                    "Data dávky vytvořena...\n" +
                    "Celkem " + dsinvFask.CZMST_I1.Count + " položek.",
                    nazevInventury,
                     MessageBoxButtons.OK,
                      MessageBoxIcon.Information
                    );

            }
            catch (Exception exPlneni)
            {
                MessageBox.Show(exPlneni.Message, "priprava inventuryni davky", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        #endregion

        #endregion

        private void stornovatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PerformStornoRecord();
        }

        private void PerformStornoRecord()
        {
            try
            {
                int pocet = 0;

                if (CZMST_I1_Predloha_selectedRow == null)
                    //if (CZMST_I1_selectedRows.Count <= 0)
                    {
                    MessageBox.Show("Není vybrán záznam pro storno", this.Text, MessageBoxButtons.OK);
                    return;
                }


                if (CZMST_I1_Predloha_selectedRows.Count >= 1)
                {
                    // Fask.Interfaces.DataSets.Prijem.CZMST_PEDataTable dt = new Fask.Interfaces.DataSets.Prijem.CZMST_PEDataTable();

                    foreach (var row in CZMST_I1_Predloha_selectedRows)
                    {

                        if (row.TerminalID != 201)
                        {
                            row.TerminalID = 201;


                            if ((providerInventura != null) && (providerInventura is Fask.Interfaces.Inventura.IInventura2_UpdateI1))
                                pocet += ((Fask.Interfaces.Inventura.IInventura2_UpdateI1)providerInventura).UpdateI1(row) == true ? 1 : 0;
                            else
                                throw new NotImplementedException("Provider neimplementuje IInventura2_UpdateI1.");

                        }


                    }



                    //#region doimplementovat!! MaR 22.2. 2024


                    //if ((providerPrijem != null) && (providerPrijem is Fask.Interfaces.Prijem.IPrijem2_UpdatePE))
                    //    pocet = ((Fask.Interfaces.Prijem.IPrijem2_UpdatePE)providerPrijem).UpdatePE(dt);
                    //else
                    //    throw new NotImplementedException("Provider neimplementuje IPrijem2_UpdatePE.");
                    //#endregion
                }

                //throw new NotImplementedException("Neimplementováno.");
                string mess = string.Format("Stornováno {0} záznamů", pocet);
                MessageBox.Show(mess, this.Text, MessageBoxButtons.OK);


                PerformOK();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        #region Tisk

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

                if (CZMST_I1_Predloha_selectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if (CZMST_I1_Predloha_selectedRows.Count > 1)
                {
                    //MessageBox.Show("Je vybráno více záznamů než jeden.", this.Text, MessageBoxButtons.OK);
                    //return;
                }


                Fask.Interfaces.DataSets.Inventura.CZMST_I1_PredlohaDataTable dt = new Fask.Interfaces.DataSets.Inventura.CZMST_I1_PredlohaDataTable();

                foreach (Fask.Interfaces.DataSets.Inventura.CZMST_I1_PredlohaRow row in CZMST_I1_Predloha_selectedRows)
                {
                    try
                    {
                        Fask.Interfaces.DataSets.Inventura.CZMST_I1_PredlohaRow newRow = dt.NewCZMST_I1_PredlohaRow();
                        // Ošetřit delší vstupní pole než počet sloupců tabulky
                        int copyLength = Math.Min(row.ItemArray.Length, dt.Columns.Count);
                        newRow.ItemArray = row.ItemArray.Take(copyLength).ToArray();
                        dt.AddCZMST_I1_PredlohaRow(newRow);
                    }
                    catch (Exception ex)
                    {
                        // Ošetření vyjimky
                        // Můžete zde provést logování chyby nebo jiné požadované akce
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                    }
                }


                PrintReport(dt);

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        }


        private void PrintReport(Fask.Interfaces.DataSets.Inventura.CZMST_I1_PredlohaDataTable dt)
        {
            try
            {

                if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                {
                    foreach (var item in dt)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start inventura predloha TISK rdlc-------------------------------------");

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

                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END inventura predloha TISK rdlc-------------------------------------");
                    }


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


                    #region new vybrani tiskove  sablony
                    // Vytvoření instance formuláře
                    //FormOdvod_TiskoveSablony form = new FormOdvod_TiskoveSablony(this.Text, FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Admin());


                    string klic = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Tisk_Klic_RDLC;
                    //FormOdvod_TiskoveSablony form = new FormOdvod_TiskoveSablony(this.Text, FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Admin(), klic);


                    FormOdvod_TiskoveSablony form = new FormOdvod_TiskoveSablony(this.Text, FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Admin(), klic, false);


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

        #endregion




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

                if (CZMST_I1_Predloha_selectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                #region MaR 6.11.2024 vypis zpl do souboru selected rows
                int index_zaznamu = 0;

                foreach (var item in CZMST_I1_Predloha_selectedRows)
                {

                    if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start sklady/transakce/inventura/predloha TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");

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

                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END sklady/transakce/inventura/predloha TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");


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



                    foreach (var item in CZMST_I1_Predloha_selectedRows)
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
                    Tisk_selected_rows_Primy_Tisk(plr_Path, true, MN_ToTisk, CZMST_I1_Predloha_selectedRows, NazevVychoziTiskarny);

                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Tisk_selected_rows_Primy_Tisk(string plr_Path, bool MnozstvuAutoJedna, int MN_ToTisk, List<Fask.Interfaces.DataSets.Inventura.CZMST_I1_PredlohaRow> FASK_ZASOBY_selected_Rows, string nazevVychTiskarny)
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

                if (CZMST_I1_Predloha_selectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                //if (CZMST_I1_Predloha_selectedRows.Count > 1)
                //{
                //    //MessageBox.Show("Je vybráno více záznamů než jeden.", this.Text, MessageBoxButtons.OK);
                //    //return;
                //}

                Fask.Interfaces.DataSets.Inventura.CZMST_I1_PredlohaDataTable dt = new Fask.Interfaces.DataSets.Inventura.CZMST_I1_PredlohaDataTable();

                foreach (Fask.Interfaces.DataSets.Inventura.CZMST_I1_PredlohaRow row in CZMST_I1_Predloha_selectedRows)
                {
                    try
                    {
                        Fask.Interfaces.DataSets.Inventura.CZMST_I1_PredlohaRow newRow = dt.NewCZMST_I1_PredlohaRow();
                        // Ošetřit delší vstupní pole než počet sloupců tabulky
                        int copyLength = Math.Min(row.ItemArray.Length, dt.Columns.Count);
                        newRow.ItemArray = row.ItemArray.Take(copyLength).ToArray();
                        dt.AddCZMST_I1_PredlohaRow(newRow);
                    }
                    catch (Exception ex)
                    {
                        // Ošetření vyjimky
                        // Můžete zde provést logování chyby nebo jiné požadované akce
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                    }
                }


                dt.Columns.Add("ITEMNMBR_IMG", typeof(string));
                dt.Columns.Add("CZ_CarKod_IMG", typeof(string));
                dt.Columns.Add("LOCNCODE_IMG", typeof(string));
                dt.Columns.Add("ITEMCODE_IMG", typeof(string));
                dt.Columns.Add("I2_SERLNMBR_IMG", typeof(string));
                dt.Columns.Add("I3_VNDITNUM_IMG", typeof(string));
                dt.Columns.Add("I3_CZ_CarKod_IMG", typeof(string));

                dt.Columns.Add("A_2D_DataMatrix_IMG", typeof(string));
                dt.Columns.Add("A_2D_DataMatrix_kod", typeof(string));
                foreach (Fask.Interfaces.DataSets.Inventura.CZMST_I1_PredlohaRow row in dt)
                {


                    try
                    {
                        if (!string.IsNullOrEmpty(row.ITEMNMBR))
                        {
                            ZXing.BarcodeWriter zw = new ZXing.BarcodeWriter();
                            zw.Format = ZXing.BarcodeFormat.CODE_128;
                            zw.Options.Height = 50; //50
                            zw.Options.PureBarcode = true;
                            System.Drawing.Bitmap image1 = zw.Write(row.ITEMNMBR);

                            System.IO.MemoryStream ms = new System.IO.MemoryStream();
                            image1.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                            byte[] imgReportBarcode = ms.ToArray();
                            ms.Close();

                            string Base64Imahe = Convert.ToBase64String(imgReportBarcode);

                            row["ITEMNMBR_IMG"] = Base64Imahe;

                        }

                        if (!string.IsNullOrEmpty(row.CZ_CarKod))
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

                        if (!string.IsNullOrEmpty(row.LOCNCODE))
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

                        if (!row.IsITEMCODENull() && !string.IsNullOrEmpty(row.ITEMCODE))
                        {
                            ZXing.BarcodeWriter zw = new ZXing.BarcodeWriter();
                            zw.Format = ZXing.BarcodeFormat.CODE_128;
                            zw.Options.Height = 50; //50
                            zw.Options.PureBarcode = true;
                            System.Drawing.Bitmap image1 = zw.Write(row.ITEMCODE);

                            System.IO.MemoryStream ms = new System.IO.MemoryStream();
                            image1.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                            byte[] imgReportBarcode = ms.ToArray();
                            ms.Close();

                            string Base64Imahe = Convert.ToBase64String(imgReportBarcode);

                            row["ITEMCODE_IMG"] = Base64Imahe;

                        }
                      
                        if (!row.IsI2_SERLNMBRNull() && !string.IsNullOrEmpty(row.I2_SERLNMBR))
                        {
                            ZXing.BarcodeWriter zw = new ZXing.BarcodeWriter();
                            zw.Format = ZXing.BarcodeFormat.CODE_128;
                            zw.Options.Height = 50; //50
                            zw.Options.PureBarcode = true;
                            System.Drawing.Bitmap image1 = zw.Write(row.I2_SERLNMBR);

                            System.IO.MemoryStream ms = new System.IO.MemoryStream();
                            image1.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                            byte[] imgReportBarcode = ms.ToArray();
                            ms.Close();

                            string Base64Imahe = Convert.ToBase64String(imgReportBarcode);

                            row["I2_SERLNMBR_IMG"] = Base64Imahe;

                        }

                        if (!row.IsI3_VNDITNUMNull() && !string.IsNullOrEmpty(row.I3_VNDITNUM))
                        {
                            ZXing.BarcodeWriter zw = new ZXing.BarcodeWriter();
                            zw.Format = ZXing.BarcodeFormat.CODE_128;
                            zw.Options.Height = 50; //50
                            zw.Options.PureBarcode = true;
                            System.Drawing.Bitmap image1 = zw.Write(row.I3_VNDITNUM);

                            System.IO.MemoryStream ms = new System.IO.MemoryStream();
                            image1.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                            byte[] imgReportBarcode = ms.ToArray();
                            ms.Close();

                            string Base64Imahe = Convert.ToBase64String(imgReportBarcode);

                            row["I3_VNDITNUM_IMG"] = Base64Imahe;

                        }

                        if (!row.IsI3_CZ_CarKodNull() && !string.IsNullOrEmpty(row.I3_CZ_CarKod))
                        {
                            ZXing.BarcodeWriter zw = new ZXing.BarcodeWriter();
                            zw.Format = ZXing.BarcodeFormat.CODE_128;
                            zw.Options.Height = 50; //50
                            zw.Options.PureBarcode = true;
                            System.Drawing.Bitmap image1 = zw.Write(row.I3_CZ_CarKod);

                            System.IO.MemoryStream ms = new System.IO.MemoryStream();
                            image1.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                            byte[] imgReportBarcode = ms.ToArray();
                            ms.Close();

                            string Base64Imahe = Convert.ToBase64String(imgReportBarcode);

                            row["I3_CZ_CarKod_IMG"] = Base64Imahe;

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
                PrintReport_RDLC(CZMST_I1_Predloha_selectedRow.CountEntries, dt, primyTisk, path_sablona, nazevVychTiskarny, MN_ToTisk);

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        }

        private void PrintReport_RDLC(int CountEntries, Fask.Interfaces.DataSets.Inventura.CZMST_I1_PredlohaDataTable dt, bool primyTisk, string path_sablona, string nazevVychTiskarny, int pocetVytisku)
        {
            try
            {

                #region logovani tisku
                if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                {
                    foreach (var item in dt)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start sklady/transakce/inventura/predloha TISK rdlc-------------------------------------");

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

                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END sklady/transakce/inventura/predloha TISK rdlc-------------------------------------");
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

                    plr.CountEntries = CountEntries.ToString();
                    //plr.HlavickaKod = SOPNUMBE;
                    //plr.HlavickaKodIMG = SOPNUMBE.Trim();


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

                if (CZMST_I1_Predloha_selectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                #region MaR 6.11.2024 vypis zpl do souboru selected rows
                int index_zaznamu = 0;

                foreach (var item in CZMST_I1_Predloha_selectedRows)
                {

                    if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start sklady/transakce/inventura/predloha TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");

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

                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END sklady/transakce/inventura/predloha TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");


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



                    foreach (var item in CZMST_I1_Predloha_selectedRows)
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
                    Tisk_selected_rows(plr_Path, true, MN_ToTisk, CZMST_I1_Predloha_selectedRows);

                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Tisk_selected_rows(string plr_Path, bool MnozstvuAutoJedna, int MN_ToTisk, List<Fask.Interfaces.DataSets.Inventura.CZMST_I1_PredlohaRow> FASK_ZASOBY_selected_Rows)
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
            Fask.Interfaces.DataSets.Inventura.CZMST_I1_PredlohaRow row,
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

                Fask.Interfaces.DataSets.Inventura.CZMST_I1_PredlohaDataTable dt = new Fask.Interfaces.DataSets.Inventura.CZMST_I1_PredlohaDataTable();

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

        private Dictionary<string, string> PrepareDataToTisk(Fask.Interfaces.DataSets.Inventura.CZMST_I1_PredlohaRow productionRow_data)
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

                        Fask.Interfaces.DataSets.Inventura.CZMST_I1_PredlohaDataTable dt = new Fask.Interfaces.DataSets.Inventura.CZMST_I1_PredlohaDataTable();


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
