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
using System.IO;
using Konzola.Vyroba.Rozbory;
using Konzola.Forms;
using MST_Print_Server_ZPL_Printing;

namespace Konzola.Vyroba
{
    public partial class FormVazbyMaterialy : Form
    {
        #region private promenne

        private bool opravneniEditace = false;
        private bool opravneniImport = false;
        private bool opravneniArchivace = false;

        #endregion

        private Opravneni opravneni;
        private Fask.Interfaces.IMES providerTisk = null; //**DONE

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
        
        /// <summary>
        /// Konstruktor
        /// </summary>
        public FormVazbyMaterialy(Opravneni opravneni)
        {
            try
            {
                InitializeComponent();

                this.dgMaterialy.UpdateColumnHeaderCellsByDatasource();
                this.dgVyrobky.UpdateColumnHeaderCellsByDatasource();

                panelButtons.Menu = menuStrip2;
                this.opravneni = opravneni;
                SetOpravneni();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        ///Vybrany řadek Vyrobku
        /// </summary>
        public Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TP_VyrobekRow FASK_Vyroba_TP_Vyrobek_selectedRow
        {
            get
            {
                try
                {
                    return ((DataRowView)(this.dgVyrobky.BindingContext[bsVyrobky].Current)).Row as Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TP_VyrobekRow;
                }
                catch
                {
                    return null;
                }
            }
        }

        private List<Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TP_VyrobekRow> FASK_Vyroba_TP_Vyrobek_selectedRows
        {
            //get
            //{
            //    List<Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TP_VyrobekRow> rows = new List<Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TP_VyrobekRow>();

            //    foreach (DataGridViewRow selectedRow in dgVyrobky.SelectedRows)
            //    {
            //        Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TP_VyrobekRow row = ((DataRowView)selectedRow.DataBoundItem).Row as Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TP_VyrobekRow;
            //        rows.Add(row);
            //    }

            //    return rows;
            //}

            get
            {
                return dgVyrobky.SelectedRows
                    .Cast<DataGridViewRow>()
                    .OrderBy(r => r.Index)               // zarovnání na vizuální pořadí v gridu
                    .Select(r => ((DataRowView)r.DataBoundItem).Row as Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TP_VyrobekRow)
                    .Where(r => r != null)
                    .ToList();
            }
        }

        private List<Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TP_MaterialRow> Material_selectedRows
        {
            //get
            //{
            //    List<Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TP_MaterialRow> rows = new List<Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TP_MaterialRow>();

            //    foreach (DataGridViewRow selectedRow in dgMaterialy.SelectedRows)
            //    {
            //        Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TP_MaterialRow row = ((DataRowView)selectedRow.DataBoundItem).Row as Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TP_MaterialRow;
            //        rows.Add(row);
            //    }

            //    return rows;
            //}

            get
            {
                return dgMaterialy.SelectedRows
                    .Cast<DataGridViewRow>()
                    .OrderBy(r => r.Index)               // zarovnání na vizuální pořadí v gridu
                    .Select(r => ((DataRowView)r.DataBoundItem).Row as Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TP_MaterialRow)
                    .Where(r => r != null)
                    .ToList();
            }
        }

        private List<Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TP_MaterialRow> Material_allRows
        {
            //get
            //{
            //    List<Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TP_MaterialRow> rows = new List<Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TP_MaterialRow>();

            //    foreach (DataGridViewRow gridRow in dgMaterialy.Rows)
            //    {
            //        if (gridRow.DataBoundItem is DataRowView dataRowView)
            //        {
            //            if (dataRowView.Row is Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TP_MaterialRow materialRow)
            //            {
            //                rows.Add(materialRow);
            //            }
            //        }
            //    }

            //    return rows;
            //}

            get
            {
                return dgMaterialy.Rows
     .Cast<DataGridViewRow>()
     .OrderBy(r => r.Index)
     .Select(r => r.DataBoundItem as DataRowView)
     .Where(v => v != null)
     .Select(v => v.Row as Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TP_MaterialRow)
     .Where(row => row != null)
     .ToList();
            }
        }



        /// <summary>
        /// Seznam vsech nactenych filtru Materialy
        /// </summary>
        private List<Fask.Interfaces.Filtry.VazbyMaterialyFiltr> filtry_Materialy = new List<Fask.Interfaces.Filtry.VazbyMaterialyFiltr>();

        /// <summary>
        /// Seznam vsech nactenych filtru Vyrobky
        /// </summary>
        private List<Fask.Interfaces.Filtry.VazbyMaterialyFiltr> filtry_Vyrobky = new List<Fask.Interfaces.Filtry.VazbyMaterialyFiltr>();



        /// <summary>
        /// Provider pro vyhledavani filtrama
        /// </summary>
        private Fask.Interfaces.IMES providerVazby = null;


        /// <summary>
        /// Vybrany filtr Materialy
        /// </summary>
        private Fask.Interfaces.Filtry.VazbyMaterialyFiltr rowFiltr_Materialy
        {
            get
            {
                try
                {
                    return tscbFiltry_Material.SelectedItem as Fask.Interfaces.Filtry.VazbyMaterialyFiltr;
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
        private Fask.Interfaces.Filtry.VazbyMaterialyFiltr rowFiltr_Vyrobky
        {
            get
            {
                try
                {
                    return tscbFiltry_Vyrobek.SelectedItem as Fask.Interfaces.Filtry.VazbyMaterialyFiltr;
                }
                catch
                {
                    return null;
                }
            }
        }

        #region Eventy formu

        /// <summary>
        /// Konstruktor
        /// </summary>
        public FormVazbyMaterialy()
        {
            try
            {
                InitializeComponent();

                this.dgMaterialy.UpdateColumnHeaderCellsByDatasource();
                this.dgVyrobky.UpdateColumnHeaderCellsByDatasource();

                panelButtons.Menu = menuStrip2;

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Load event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormVazbyMaterialy_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                this.WindowState = FormWindowState.Maximized;

                this.dgVyrobky.LoadConfiguration(this.GetType().ToString() + "Vyrobky");
                this.dgMaterialy.LoadConfiguration(this.GetType().ToString() + "Materialy");

                panelButtons.LoadConfiguration(this.GetType().ToString());
                panelButtons.Init();
                panelButtons.Size = new Size(85, 700);

                advancedDataGridViewSearchToolBarMaterialy.SetColumns(dgMaterialy.Columns);
                advancedDataGridViewSearchToolBarVyrobky.SetColumns(dgVyrobky.Columns);

                //// načtení konfigurace vytvořených filtrů
                this.filtry_Materialy = Extensions.ListExt.ReadFromXML<Fask.Interfaces.Filtry.VazbyMaterialyFiltr>(this.GetType().ToString() + "_Material" + ".filtr");
                tscbFiltry_Material.ComboBox.DataSource = this.filtry_Materialy;
                tscbFiltry_Material.SelectedItem = null;
                tscbFiltry_Material.ComboBox.DropDownWitdhAutosize();

                //// načtení konfigurace vytvořených filtrů
                this.filtry_Vyrobky = Extensions.ListExt.ReadFromXML<Fask.Interfaces.Filtry.VazbyMaterialyFiltr>(this.GetType().ToString() + "_Vyrobky" + ".filtr");
                tscbFiltry_Vyrobek.ComboBox.DataSource = this.filtry_Vyrobky;
                tscbFiltry_Vyrobek.SelectedItem = null;
                tscbFiltry_Vyrobek.ComboBox.DropDownWitdhAutosize();



                InitProvider();

                if (providerVazby == null)
                    throw new Exception("Provider 'Vazby' není inicializován");

                //UpdateVazbyMaterialyForm();
                //PerformVyhledat_Vyrobek(null);
                button_Filtr_Vyrobky.Focus();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Event odchytavajici klavesnice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormVazbyMaterialy_KeyDown(object sender, KeyEventArgs e)
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
                }
                //else if (e.KeyCode == Keys.F5)
                //{
                //    obnovitToolStripMenuItem_Click(null,null);
                //}
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

        /// <summary>
        /// Event po zavřeni okna
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormVazbyMaterialy_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.dgVyrobky.SaveConfiguration(this.GetType().ToString() + "Vyrobky");
                this.dgMaterialy.SaveConfiguration(this.GetType().ToString() + "Materialy");

                panelButtons.SaveConfiguration(this.GetType().ToString());

                // ulozeni vsech filtru
                this.filtry_Materialy.WriteXML(this.GetType().ToString() + "_Material" + ".filtr");
                this.filtry_Vyrobky.WriteXML(this.GetType().ToString() + "_Vyrobky" + ".filtr");

                Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ostatni[0].VazbyMaterialySplitPoloha = splitContainer1.SplitterDistance;
                Konfigurace.Globals_Konfig_Konzola.SaveConfiguration();


            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormVazbyMaterialy_Shown(object sender, EventArgs e)
        {
            try
            {
                this.progressIndicatorMaterialy.Size = new Size(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ostatni[0].ProgressIndicatorSize, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ostatni[0].ProgressIndicatorSize);
                this.progressIndicatorMaterialy.Location = new Point(this.dgMaterialy.Location.X + (this.dgMaterialy.Width / 2) - (progressIndicatorMaterialy.Size.Width / 2), this.dgMaterialy.Location.Y + (this.dgMaterialy.Height / 2) - (progressIndicatorMaterialy.Size.Height / 2));

                this.progressIndicatorVyrobky.Size = new Size(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ostatni[0].ProgressIndicatorSize, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ostatni[0].ProgressIndicatorSize);
                this.progressIndicatorVyrobky.Location = new Point(this.dgVyrobky.Location.X + (this.dgVyrobky.Width / 2) - (progressIndicatorVyrobky.Size.Width / 2), this.dgVyrobky.Location.Y + (this.dgVyrobky.Height / 2) - (progressIndicatorVyrobky.Size.Height / 2));

                splitContainer1.SplitterDistance = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ostatni[0].VazbyMaterialySplitPoloha;
            }
            catch { }
        }


        #endregion

   
        /// <summary>
        /// Event pro DatagridView , pri zmene vyrobku se dotahnou materialy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            NastavDatagrid();

            try
            {
                if (FASK_Vyroba_TP_Vyrobek_selectedRow != null)
                {
                    //NajdiMaterialy();
                    PerformVyhledat();
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        

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


        #region Progress indikatory

        private void ProgressIndicatorVyrobekStop()
        {
            progressIndicatorVyrobky.Stop();
            progressIndicatorVyrobky.Visible = false;
        }

        private void ProgressIndicatorMaterialStop()
        {
            progressIndicatorMaterialy.Stop();
            progressIndicatorMaterialy.Visible = false;
        }


        private void ProgressIndicatorVyrobekStart()
        {
            try
            {
                // prepocet stredu datagridu
                this.progressIndicatorVyrobky.Location = new Point(this.dgVyrobky.Location.X + (this.dgVyrobky.Width / 2) - (progressIndicatorVyrobky.Size.Width / 2), this.dgVyrobky.Location.Y + (this.dgVyrobky.Height / 2) - (progressIndicatorVyrobky.Size.Height / 2));
            }
            catch { }
            progressIndicatorVyrobky.Start();
            progressIndicatorVyrobky.Visible = true;
        }

        private void ProgressIndicatorMaterialStart()
        {
            try
            {
                // prepocet stredu datagridu
                this.progressIndicatorMaterialy.Location = new Point(this.dgMaterialy.Location.X + (this.dgMaterialy.Width / 2) - (progressIndicatorMaterialy.Size.Width / 2), this.dgMaterialy.Location.Y + (this.dgMaterialy.Height / 2) - (progressIndicatorMaterialy.Size.Height / 2));
            }
            catch { }
            progressIndicatorMaterialy.Start();
            progressIndicatorMaterialy.Visible = true;
        }


        #endregion

        #region Filtry Material

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


                 comboBox_ITEMNMBR.Text = filtr.MaterialITEMNMBR;
                 comboBox_ITEMDESC.Text = filtr.MaterialITEMDESC;
                 comboBox_MJ.Text = filtr.MaterialVNDITNUM;

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
                comboBox_ITEMNMBR.SelectedItem =
                comboBox_ITEMDESC.SelectedItem =
                comboBox_MJ.SelectedItem = null;


                comboBox_ITEMNMBR.Text =
                comboBox_ITEMDESC.Text =
                comboBox_MJ.Text = string.Empty;
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

                if (rowFiltr_Materialy == null)
                    return;

                DialogResult dr = MessageBox.Show("Opravdu chcete změnit vybraný filtr '" + rowFiltr_Materialy.NazevFiltru.Trim() + "'?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr != System.Windows.Forms.DialogResult.Yes)
                    return;

                Fask.Interfaces.Filtry.VazbyMaterialyFiltr filtr = rowFiltr_Materialy;

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
            filtr.MaterialITEMNMBR = comboBox_ITEMNMBR.Text.Trim();
            filtr.MaterialITEMDESC = comboBox_ITEMDESC.Text.Trim();
            filtr.MaterialVNDITNUM = comboBox_MJ.Text.Trim();
           


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
                    filtry_Materialy.Add(filtr);
                    //this.tscbFiltry.Items.Clear();
                    this.tscbFiltry_Material.ComboBox.DataSource = null;
                    this.tscbFiltry_Material.ComboBox.DataSource = filtry_Materialy;
                    this.tscbFiltry_Material.SelectedItem = filtr;
                    tscbFiltry_Material.ComboBox.DropDownWitdhAutosize();
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

                if (rowFiltr_Materialy == null)
                    return;

                DialogResult dr = MessageBox.Show("Opravdu chcete odstranit filtr '" + (string.IsNullOrEmpty(rowFiltr_Materialy.NazevFiltru) ? string.Empty : rowFiltr_Materialy.NazevFiltru.Trim()) + "'?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr != System.Windows.Forms.DialogResult.Yes)
                    return;

                filtry_Materialy.Remove(rowFiltr_Materialy);
                this.tscbFiltry_Material.ComboBox.DataSource = null;
                this.tscbFiltry_Material.ComboBox.DataSource = filtry_Materialy;
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
                DataTable dtchanged = this.dsMaterialy.FASK_Vyroba_TP_Material.GetChanges();
                if (dtchanged != null && dtchanged.Rows.Count > 0) //obsahuje zmeny ... 
                {
                    DialogResult dr = MessageBox.Show("Obsahuje změny, pokračovat?", "data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == System.Windows.Forms.DialogResult.No)
                        return;
                }

                if (bw_Materialy_stav.IsBusy)
                {
                    bw_Materialy_stav.CancelAsync();
                    while (bw_Materialy_stav.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }

                ProgressIndicatorMaterialStart();

                Fask.Interfaces.Filtry.VazbyMaterialyFiltr filtr = new Fask.Interfaces.Filtry.VazbyMaterialyFiltr();
                if (!CreateFilter(ref filtr))
                    return;

                int FirstDisplayedScrollingRowIndex = this.dgMaterialy.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

                bw_Materialy_stav.RunWorkerAsync(filtr);

                if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dgMaterialy.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dgMaterialy.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index
            }
            catch (Exception ex)
            {
                ProgressIndicatorMaterialStop();
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region CLICK

        private void tsbNastavit_Click(object sender, EventArgs e)
        {
            PerformNastavitFiltr(rowFiltr_Materialy);
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

        private void tsbVycistit_Click(object sender, EventArgs e)
        {
            PerformVycistitFiltr();
        }

        private void button_filtr_Material_Click(object sender, EventArgs e)
        {
            NastavDatagrid();

            //Todo filtrovani 
            PerformVyhledat();
        }

        #endregion

        #region BACKGRUND workery

        private void bw_Materialy_stav_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Fask.Interfaces.Filtry.VazbyMaterialyFiltr filtr = (Fask.Interfaces.Filtry.VazbyMaterialyFiltr)e.Argument;
                Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();
                //Fask.Interfaces.DataSets.SkladLokace ds = new Fask.Interfaces.DataSets.SkladLokace();

                if (bw_Materialy_stav.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                if (FASK_Vyroba_TP_Vyrobek_selectedRow != null)
                {
                    ((Fask.Interfaces.Vazby.IVazby2)providerVazby).ITEMNMBR_Def = FASK_Vyroba_TP_Vyrobek_selectedRow.ITEMNMBR;
                    ((Fask.Interfaces.Vazby.IVazby2)providerVazby).rowvyrobek_ID_L = FASK_Vyroba_TP_Vyrobek_selectedRow.ID_L;

                    // nacteni dat v oddelenem vlakne
                    //ds = ((Fask.Interfaces.Vazby.IVazby2)providerVazby).GetFiltrovanyVazbyMaterialy(filtr);

                    if (providerVazby is Fask.Interfaces.Vazby.IVazby2_GetFiltrovanyVazbyMaterialy)
                        ds = ((Fask.Interfaces.Vazby.IVazby2_GetFiltrovanyVazbyMaterialy)providerVazby).GetFiltrovanyVazbyMaterialy(filtr);
                    else
                        throw new Exception("IVazby2_GetFiltrovanyVazbyMaterialy not implementet");


                    ((Fask.Interfaces.Vazby.IVazby2)providerVazby).ITEMNMBR_Def = String.Empty;
                    ((Fask.Interfaces.Vazby.IVazby2)providerVazby).rowvyrobek_ID_L = String.Empty;
                }
                if (bw_Materialy_stav.CancellationPending)
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

        private void bw_Materialy_stav_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                // check error, check cancel, then use result
                if (e.Error != null)
                {
                    // handle the error
                    dsMaterialy = new Fask.Interfaces.DataSets.Vyroba();
                    bsMaterialy.DataSource = dsMaterialy;
                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, e.Error);
                    MessageBox.Show(e.Error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (e.Cancelled)
                {
                    // handle cancellation
                    dsMaterialy = new Fask.Interfaces.DataSets.Vyroba();
                    bsMaterialy.DataSource = dsMaterialy;
                }
                else
                {
                    // uspesne dokonceno ...
                    // use it on the UI thread
                    dsMaterialy = (Fask.Interfaces.DataSets.Vyroba)e.Result;
                    if (dsMaterialy == null)
                        dsMaterialy = new Fask.Interfaces.DataSets.Vyroba();

                    bsMaterialy.DataSource = dsMaterialy;
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ProgressIndicatorMaterialStop();
            }
        }

        #endregion



        #endregion

        #region Filtry Vyrobek


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
                comboBox_Vyrobek_ITEMDESC.Text = filtr.MaterialITEMDESC;
                comboBox_Vyrobek_MJ.Text = filtr.MaterialMJ;
                

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
                comboBox_Vyrobek_ITEMDESC.SelectedItem =
                comboBox_Vyrobek_MJ.SelectedItem = null;


                comboBox_Vyrobek_ITEMNMBR.Text =
                comboBox_Vyrobek_ITEMDESC.Text =
                comboBox_Vyrobek_MJ.Text = string.Empty;
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
            filtr.MaterialITEMDESC = comboBox_Vyrobek_ITEMDESC.Text.Trim();
            filtr.MaterialVNDITNUM = comboBox_Vyrobek_MJ.Text.Trim();



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
                    this.tscbFiltry_Vyrobek.ComboBox.DataSource = null;
                    this.tscbFiltry_Vyrobek.ComboBox.DataSource = filtry_Vyrobky;
                    this.tscbFiltry_Vyrobek.SelectedItem = filtr;
                    tscbFiltry_Vyrobek.ComboBox.DropDownWitdhAutosize();
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
                this.tscbFiltry_Vyrobek.ComboBox.DataSource = null;
                this.tscbFiltry_Vyrobek.ComboBox.DataSource = filtry_Vyrobky;
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
        private void PerformVyhledat_Vyrobek(string ITEMNMBR)
        {
            try
            {
                //DataTable dtchanged = this.dsVyrobky.FASK_Vyroba_TP_Vyrobek.GetChanges();
                //if (dtchanged != null && dtchanged.Rows.Count > 0) //obsahuje zmeny ... 
                //{
                //    DialogResult dr = MessageBox.Show("Obsahuje změny, pokračovat?", "data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                //    if (dr == System.Windows.Forms.DialogResult.No)
                //        return;
                //}

                if (bw_Vyrobky_stav.IsBusy)
                {
                    bw_Vyrobky_stav.CancelAsync();
                    while (bw_Vyrobky_stav.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }

                ProgressIndicatorVyrobekStart();

                Fask.Interfaces.Filtry.VazbyMaterialyFiltr filtr = new Fask.Interfaces.Filtry.VazbyMaterialyFiltr();
                if (!CreateFilter_Vyrobek(ref filtr))
                    return;

                RememberItem_VazbyMaterialy rem = new RememberItem_VazbyMaterialy(filtr, ITEMNMBR, null);

                int FirstDisplayedScrollingRowIndex = this.dgVyrobky.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

                bw_Vyrobky_stav.RunWorkerAsync(rem);

                if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dgVyrobky.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dgVyrobky.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index
            }
            catch (Exception ex)
            {
                ProgressIndicatorVyrobekStop();
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #endregion

        #region CLICK

        private void tsbNastavit_Material_Click(object sender, EventArgs e)
        {
            PerformNastavitFiltr_Vyrobek(rowFiltr_Vyrobky);
        }

        private void tsbZmena_Vyrobek_Click(object sender, EventArgs e)
        {
            PerformZmenitFiltr_Vyrobek();
        }

        private void tsbPridat_Vyrobek_Click(object sender, EventArgs e)
        {
            PerformPridatFiltr_Vyrobek();
        }

        private void tsbOdebrat_Vyrobek_Click(object sender, EventArgs e)
        {
            PerformOdebratFiltr_Vyrobek();
        }

        private void tsbVycistit_Vyrobek_Click(object sender, EventArgs e)
        {
            PerformVycistitFiltr_Vyrobek();
        }

        private void button_Filtr_Vyrobky_Click(object sender, EventArgs e)
        {


            NastavDatagrid();

            PerformVyhledat_Vyrobek(null);
        }

#endregion

        #region BACKGRUND workery

        private void bw_Vyrobky_stav_DoWork(object sender, DoWorkEventArgs e)
        {
            //try
            //{
                RememberItem_VazbyMaterialy Rem = (RememberItem_VazbyMaterialy)e.Argument;
                Fask.Interfaces.Filtry.VazbyMaterialyFiltr filtr = Rem.filtr;
                Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();
                //Fask.Interfaces.DataSets.SkladLokace ds = new Fask.Interfaces.DataSets.SkladLokace();

                if (bw_Vyrobky_stav.CancellationPending)
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

                if (bw_Vyrobky_stav.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                RememberItem_VazbyMaterialy tmprem = new RememberItem_VazbyMaterialy(null, Rem.ITEMBMBR, ds);

                e.Result = tmprem;
            //}
            //catch (Exception ex)
            //{
            //    Log.Write(ex);
            //    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        #region 4.9.2024 MaR zakomentoval old
        //private void bw_Vyrobky_stav_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    try
        //    {
        //        // check error, check cancel, then use result
        //        if (e.Error != null)
        //        {
        //            // handle the error
        //            dsVyrobky = new Fask.Interfaces.DataSets.Vyroba();
        //            bsVyrobky.DataSource = dsVyrobky;
        //            Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, e.Error);
        //            MessageBox.Show(e.Error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //        else if (e.Cancelled)
        //        {
        //            // handle cancellation
        //            dsVyrobky = new Fask.Interfaces.DataSets.Vyroba();
        //            bsVyrobky.DataSource = dsVyrobky;
        //        }
        //        else
        //        {
        //            // uspesne dokonceno ...
        //            // use it on the UI thread
        //            RememberItem_VazbyMaterialy Rem = (RememberItem_VazbyMaterialy)e.Result;
        //            dsVyrobky = Rem.ds;

        //            if (dsVyrobky == null)
        //                dsVyrobky = new Fask.Interfaces.DataSets.Vyroba();

        //            bsVyrobky.DataSource = dsVyrobky;

        //            if (!string.IsNullOrEmpty(Rem.ITEMBMBR))
        //            {

        //                var Rows = dsVyrobky.FASK_Vyroba_TP_Vyrobek.Select("ITEMNMBR = " + Rem.ITEMBMBR); //---- zde vyskakuje vyjímka {"Operaci = nelze provést na typech System.String a System.Int32."} oprav cely kod refaktoring upravy a rychlost

        //                if (Rows.Count() == 1)
        //                {

        //                    dgVyrobky.ClearSelection();
        //                    bsVyrobky.FindAndSelect(
        //                        new Key { PropertyName = dsVyrobky.FASK_Vyroba_TP_Vyrobek.ITEMNMBRColumn.ColumnName, Value = Rem.ITEMBMBR }
        //                        );
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
        //        MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    finally
        //    {
        //        ProgressIndicatorVyrobekStop();
        //    }
        //}

        #endregion

        #region 4.9.2024 New
        private void bw_Vyrobky_stav_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null)
                {
                    // Zpracování chyby
                    dsVyrobky = new Fask.Interfaces.DataSets.Vyroba();
                    bsVyrobky.DataSource = dsVyrobky;
                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, e.Error);
                    MessageBox.Show(e.Error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (e.Cancelled)
                {
                    // Zpracování zrušení
                    dsVyrobky = new Fask.Interfaces.DataSets.Vyroba();
                    bsVyrobky.DataSource = dsVyrobky;
                }
                else
                {
                    // Úspěšné dokončení
                    RememberItem_VazbyMaterialy Rem = (RememberItem_VazbyMaterialy)e.Result;
                    dsVyrobky = Rem.ds ?? new Fask.Interfaces.DataSets.Vyroba();
                    bsVyrobky.DataSource = dsVyrobky;

                    if (!string.IsNullOrEmpty(Rem.ITEMBMBR))
                    {
                        // Oprava problému s typy dat
                        string filterExpression = $"{dsVyrobky.FASK_Vyroba_TP_Vyrobek.ITEMNMBRColumn.ColumnName} = '{Rem.ITEMBMBR}'";

                        DataRow[] Rows = dsVyrobky.FASK_Vyroba_TP_Vyrobek.Select(filterExpression);

                        if (Rows.Length == 1)
                        {
                            dgVyrobky.ClearSelection();
                            bsVyrobky.FindAndSelect(
                                new Key { PropertyName = dsVyrobky.FASK_Vyroba_TP_Vyrobek.ITEMNMBRColumn.ColumnName, Value = Rem.ITEMBMBR }
                            );
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

        #endregion



        #endregion



        #endregion

        #region ClickEvents

        #region Click Eventy 

        /// <summary>
        /// Event metoda pro konec
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PerformCancel_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }
        
        /// <summary>
        /// Event metoda pro pridaní materálu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PerformPridatMaterial_Click(object sender, EventArgs e)
        {

            if (!opravneniEditace)
            {
                MessageBox.Show("Nemáte dostatečné oprávnění pro tuto akci!", "Oprávnění", MessageBoxButtons.OK);
                return;
            }

            try
            {
                if (FASK_Vyroba_TP_Vyrobek_selectedRow == null)
                {
                    MessageBox.Show("Neni vybrán výrobek.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return;
                }

                PerformPridatMaterial();
                PerformVyhledat();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        /// <summary>
        /// Event metoda pro editaci materálu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PerformEditMaterial_Click(object sender, EventArgs e)
        {
            if (!opravneniEditace)
            {
                MessageBox.Show("Nemáte dostatečné oprávnění pro tuto akci!", "Oprávnění", MessageBoxButtons.OK);
                return;
            }


            if (this.dgMaterialy.SelectedRows.Count == 0)
            {
                MessageBox.Show("Není vybrán záznam pro úpravu", this.Text, MessageBoxButtons.OK);
                return;
            }

            try
            {
                PerformEditMaterial();
                PerformVyhledat();
                //UpdateVazbyMaterialyForm();
            }
            catch (Exception ex)
            {
                
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
            
        }

        /// <summary>
        /// Event metoda pro delete materálu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PerformOdstranitMaterial_Click(object sender, EventArgs e)
        {
            if (!opravneniEditace)
            {
                MessageBox.Show("Nemáte dostatečné oprávnění pro tuto akci!", "Oprávnění", MessageBoxButtons.OK);
                return;
            }



            DialogResult dialog = MessageBox.Show("Přejete si odstranit materiál/y?", this.Text, MessageBoxButtons.YesNo);
            if (dialog != DialogResult.Yes)
            {
                return;
            }

            if (FASK_Vyroba_TP_Vyrobek_selectedRow == null)
            {
                MessageBox.Show("Neni vybrán výrobek.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

            if (this.dgMaterialy.SelectedRows.Count == 0)
            {
                MessageBox.Show("Neni vybrán žádný materál.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

            try
            {
                PerformOdstranitMaterial();
                PerformVyhledat();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }

        }

        /// <summary>
        /// Event metoda pro pridani polotovaru
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PerformAddPolotovar_Click(object sender, EventArgs e)
        {
            if (!opravneniEditace)
            {
                MessageBox.Show("Nemáte dostatečné oprávnění pro tuto akci!", "Oprávnění", MessageBoxButtons.OK);
                return;
            }


            if (FASK_Vyroba_TP_Vyrobek_selectedRow == null)
            {
                MessageBox.Show("Neni vybrán výrobek.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

            try
            {
                PerformAddPolotovar();
                PerformVyhledat();
            }
            catch (Exception ex)
            {
                
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        /// <summary>
        /// Event metoda pro pridani Vyrobku
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PerformPridatVyrobek_Click(object sender, EventArgs e)
        {

            if (!opravneniEditace)
            {
                MessageBox.Show("Nemáte dostatečné oprávnění pro tuto akci!", "Oprávnění", MessageBoxButtons.OK);
                return;
            }

            try
            {
                string TMP_ITEMNMBR_First;

                TMP_ITEMNMBR_First = PerformPridatVyrobek();
                PerformVyhledat_Vyrobek(TMP_ITEMNMBR_First);
            }
            catch (Exception ex)
            {
                
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        /// <summary>
        /// Event metoda pro odtraneni Vyrobku
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PerformOdstranitVyrobek_Click(object sender, EventArgs e)
        {

            if (!opravneniEditace)
            {
                MessageBox.Show("Nemáte dostatečné oprávnění pro tuto akci!", "Oprávnění", MessageBoxButtons.OK);
                return;
            }

            DialogResult dialog = MessageBox.Show("Přejete si odstranit výrobek/výrobky?", this.Text, MessageBoxButtons.YesNo);
            if (dialog != DialogResult.Yes)
            {
                return;
            }

            try
            {
                if (FASK_Vyroba_TP_Vyrobek_selectedRow == null)
                {
                    MessageBox.Show("Neni vybrán výrobek.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return;
                }
  
                    PerformOdstranitVyrobek();
                    PerformVyhledat_Vyrobek(null);
                
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        /// <summary>
        /// Event metoda pro duplikaci
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PerformDuplikace_Click(object sender, EventArgs e)
        {

            if (!opravneniEditace)
            {
                MessageBox.Show("Nemáte dostatečné oprávnění pro tuto akci!", "Oprávnění", MessageBoxButtons.OK);
                return;
            }

            try
            {
                string TMP_ITEMNMBR_First;

                TMP_ITEMNMBR_First = PerformDuplikace();
                //UpdateVazbyMaterialyForm();
                PerformVyhledat_Vyrobek(TMP_ITEMNMBR_First);
            }
            catch (Exception ex)
            {
                
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        /// <summary>
        /// Event metoda pro PrehodeniSkladu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PerformModifikace_TP_Click(object sender, EventArgs e)
        {
            PerformModifikace_TP();
        }

        #endregion

        #region Metody Perform volane

        /// <summary>
        /// Metoda pro konec
        /// </summary>
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
        /// Metoda pro pridaní materálu
        /// </summary>
        private void PerformPridatMaterial()
        {
            using (FormVazbyAddMaterialy formAddMaterial = new FormVazbyAddMaterialy())
            {
                formAddMaterial.rowVyrobek = FASK_Vyroba_TP_Vyrobek_selectedRow;
                formAddMaterial.ShowDialog();
            }
        }

        /// <summary>
        /// Metoda pro editaci materálu
        /// </summary>
        public void PerformEditMaterial()
        {

            Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();

            //Fask.Interfaces.DataSets.VyrobaTableAdapters.FASK_Vyroba_TPTableAdapter tpta = new Fask.Interfaces.DataSets.VyrobaTableAdapters.FASK_Vyroba_TPTableAdapter();
            //tpta.Connection = new System.Data.SqlClient.SqlConnection(Properties.Settings.Default.Konzola_ConnectionString);

            bool flagVSE = false;

            string koef = string.Empty;
            string alter = string.Empty;

            int pocet = 0;

            int fullpocet = this.dgMaterialy.SelectedRows.Count;

            foreach (DataGridViewRow row in this.dgMaterialy.SelectedRows)
            {

                Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TP_MaterialRow rowMaterial = ds.FASK_Vyroba_TP_Material.NewFASK_Vyroba_TP_MaterialRow();
                rowMaterial = ((DataRowView)row.DataBoundItem).Row as Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TP_MaterialRow;

                if (!flagVSE)
                {
                    using (FormVazbyAddMaterialyEdit VazbyAddMaterialyEdit = new FormVazbyAddMaterialyEdit())
                    {
                        VazbyAddMaterialyEdit.Text = "Editace materálu/polotovaru";
                        VazbyAddMaterialyEdit.edittype = EditType.Edit;
                        VazbyAddMaterialyEdit.rowVyrobek = FASK_Vyroba_TP_Vyrobek_selectedRow;
                        VazbyAddMaterialyEdit.rowMaterial = rowMaterial;
                        VazbyAddMaterialyEdit.ProgressOD = pocet.ToString();
                        VazbyAddMaterialyEdit.ProgressDO = fullpocet.ToString();

                        VazbyAddMaterialyEdit.ShowDialog();

                        if (VazbyAddMaterialyEdit.VseUlozit)
                        {
                            if (MessageBox.Show(string.Format("Opravdu provést změnu {0} položek na hodnotu K=<{1}>, A=<{2}>.", fullpocet - pocet, VazbyAddMaterialyEdit.Koeficient, (VazbyAddMaterialyEdit.Alter == null ? "" : VazbyAddMaterialyEdit.Alter)), "Info", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.OK)
                            {
                                flagVSE = true;
                                koef = VazbyAddMaterialyEdit.Koeficient;
                                alter = VazbyAddMaterialyEdit.Alter;

                                if (string.IsNullOrEmpty(alter))
                                {

                                    if (providerVazby is Fask.Interfaces.Vazby.IVazby2_UpdateEdit)
                                    {
                                        ((Fask.Interfaces.Vazby.IVazby2_UpdateEdit)providerVazby).UpdateEdit(
                                           koef.Trim(),
                                           FASK.Logins.Uzivatel.Instance.UserID.Trim(),
                                           DateTime.Now,
                                           null,
                                           FASK_Vyroba_TP_Vyrobek_selectedRow.ITEMNMBR.Trim(),
                                           rowMaterial.ITEMNMBR.Trim());
                                    }
                                    else
                                        throw new Exception("IVazby2_UpdateEdit not implementet");
                                }
                                else
                                {

                                    if (providerVazby is Fask.Interfaces.Vazby.IVazby2_UpdateEdit)
                                    {
                                        ((Fask.Interfaces.Vazby.IVazby2_UpdateEdit)providerVazby).UpdateEdit(
                                           koef.Trim(),
                                           FASK.Logins.Uzivatel.Instance.UserID.Trim(),
                                           DateTime.Now,
                                           alter.Trim(),
                                           FASK_Vyroba_TP_Vyrobek_selectedRow.ITEMNMBR.Trim(),
                                           rowMaterial.ITEMNMBR.Trim());
                                    }
                                    else
                                        throw new Exception("IVazby2_UpdateEdit not implementet");
                                }
                            }
                            else
                            {
                                flagVSE = false;
                            }
                        }
                        pocet++;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(alter))
                    {
                        if (providerVazby is Fask.Interfaces.Vazby.IVazby2_UpdateEdit)
                        {
                            ((Fask.Interfaces.Vazby.IVazby2_UpdateEdit)providerVazby).UpdateEdit(
                               koef.Trim(),
                               FASK.Logins.Uzivatel.Instance.UserID.Trim(),
                               DateTime.Now,
                               null,
                               FASK_Vyroba_TP_Vyrobek_selectedRow.ITEMNMBR.Trim(),
                               rowMaterial.ITEMNMBR.Trim());
                        }
                        else
                            throw new Exception("IVazby2_UpdateEdit not implementet");
                    }
                    else
                    {
                        if (providerVazby is Fask.Interfaces.Vazby.IVazby2_UpdateEdit)
                        {
                            ((Fask.Interfaces.Vazby.IVazby2_UpdateEdit)providerVazby).UpdateEdit(
                               koef.Trim(),
                               FASK.Logins.Uzivatel.Instance.UserID.Trim(),
                               DateTime.Now,
                               alter.Trim(),
                               FASK_Vyroba_TP_Vyrobek_selectedRow.ITEMNMBR.Trim(),
                               rowMaterial.ITEMNMBR.Trim());
                        }
                        else
                            throw new Exception("IVazby2_UpdateEdit not implementet");
                    }
                }
            }
        }

        /// <summary>
        /// Metoda pro delete materálu
        /// </summary>
        private void PerformOdstranitMaterial()
        {
            //Production.DataServices.VyrobaDataSetTableAdapters.FASK_Vyroba_TPTableAdapter ta = new Production.DataServices.VyrobaDataSetTableAdapters.FASK_Vyroba_TPTableAdapter();
            //ta.Connection = new System.Data.SqlClient.SqlConnection(Properties.Settings.Default.Konzola_ConnectionString);
            //Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TP_MaterialDataTable dt = GetMaterialy();

            //foreach (Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TP_MaterialRow row in dt)
            //{
            //    ta.DeleteMaterial_By_ITEMNMBR_def_and_fol(rowVyrobek.ITEMNMBR, row.ITEMNMBR);
            //}

            //UpdateVazbyMaterialyForm();

            foreach (DataGridViewRow material in dgMaterialy.SelectedRows)
            {
                //(((DataRowView)material.DataBoundItem).Row as Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TP_MaterialRow).Delete();

                if (providerVazby is Fask.Interfaces.Vazby.IVazby2_DeleteMaterial)
                    ((Fask.Interfaces.Vazby.IVazby2_DeleteMaterial)providerVazby).DeleteMaterial((((DataRowView)material.DataBoundItem).Row as Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TP_MaterialRow).ID);
                else
                    throw new Exception("IVazby2_DeleteMaterial not implementet");

            }



            //Production.DataServices.VyrobaDataSetTableAdapters.FASK_Vyroba_TP_MaterialTableAdapter taMaterial = new Production.DataServices.VyrobaDataSetTableAdapters.FASK_Vyroba_TP_MaterialTableAdapter();
            //taMaterial.Connection = new System.Data.SqlClient.SqlConnection(Properties.Settings.Default.Konzola_ConnectionString);

            //taMaterial.Update(DataSet_Materialy);
        }

        /// <summary>
        /// Metoda pro pridani polotovaru
        /// </summary>
        private void PerformAddPolotovar()
        {
            using (FormVazbyAddPolotovar formAddMaterial = new FormVazbyAddPolotovar())
            {
                formAddMaterial.rowVyrobek = FASK_Vyroba_TP_Vyrobek_selectedRow;
                formAddMaterial.ShowDialog();
            }
        }

        /// <summary>
        /// Metoda pro pridani Vyrobku
        /// </summary>
        /// <returns></returns>
        private static string PerformPridatVyrobek()
        {
            string TMP_ITEMNMBR_First;
            using (FormVazbyAddVyrobek VazbyAddVyrobek = new FormVazbyAddVyrobek(false))
            {
                VazbyAddVyrobek.ShowDialog();

                TMP_ITEMNMBR_First = VazbyAddVyrobek.ITEMNMBR_First;
            }
            return TMP_ITEMNMBR_First;
        }

        /// <summary>
        /// Metoda pro odstraneni Vyrobku
        /// </summary>
        private void PerformOdstranitVyrobek()
        {
            //int FirstDisplayedScrollingRowIndex = this.dgVyrobky.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index              
            //Production.DataServices.VyrobaDataSetTableAdapters.FASK_Vyroba_TPTableAdapter taTP = new Production.DataServices.VyrobaDataSetTableAdapters.FASK_Vyroba_TPTableAdapter();
            //taTP.Connection = new System.Data.SqlClient.SqlConnection(Properties.Settings.Default.Konzola_ConnectionString);
            //taTP.DeleteVyrobek_By_ITEMNMBR(rowVyrobek.ITEMNMBR);
            //UpdateVazbyMaterialyForm();
            //if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dgVyrobky.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dgVyrobky.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index


            if (providerVazby is Fask.Interfaces.Vazby.IVazby2_DeleteVyrobek)
                ((Fask.Interfaces.Vazby.IVazby2_DeleteVyrobek)providerVazby).DeleteVyrobek(FASK_Vyroba_TP_Vyrobek_selectedRow.ITEMNMBR.Trim());
            else
                throw new Exception("IVazby2_DeleteVyrobek not implementet");


            //rowVyrobek.Delete();

            //Production.DataServices.VyrobaDataSetTableAdapters.FASK_Vyroba_TP_VyrobekTableAdapter taTPvyrobek = new Production.DataServices.VyrobaDataSetTableAdapters.FASK_Vyroba_TP_VyrobekTableAdapter();
            //taTPvyrobek.Connection = new System.Data.SqlClient.SqlConnection(Properties.Settings.Default.Konzola_ConnectionString);
            //taTPvyrobek.Update(DataSet_Vyrobky);
        }

        /// <summary>
        /// Metoda pro duplikaci
        /// </summary>
        /// <returns></returns>
        private string PerformDuplikace()
        {
            string TMP_ITEMNMBR_First;
            using (FormVazbyAddVyrobek VazbyAddVyrobek = new FormVazbyAddVyrobek(true))
            {
                VazbyAddVyrobek.ITEMNMBR_Zdroj = FASK_Vyroba_TP_Vyrobek_selectedRow.ITEMNMBR;
                VazbyAddVyrobek.IDL_Zdroj = FASK_Vyroba_TP_Vyrobek_selectedRow.ID_L;


                VazbyAddVyrobek.ShowDialog();

                TMP_ITEMNMBR_First = VazbyAddVyrobek.ITEMNMBR_First;
            }
            return TMP_ITEMNMBR_First;
        }

        /// <summary>
        /// Metoda pro možnost modifikace TP
        /// </summary>
        private void PerformModifikace_TP()
        {

            using (Konzola.Vyroba.Ciselnik.FormModifikace_TP frm = new Konzola.Vyroba.Ciselnik.FormModifikace_TP(true, Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST))
            {
                frm.ShowDialog();
            }

 
        }

        #endregion

        private void NastavDatagrid()
        {
            #region prace s datagridem Vyrobky - nastaveni numeric hodnot na pocet desetinnych mist
            try
            {
                // Procházení sloupců v DataGridView
                foreach (DataGridViewColumn column in dgVyrobky.Columns)
                {
                    // Získání typu dat pro sloupec
                    Type dataType = null;

                    if (dgVyrobky.DataSource is DataTable dataTable)
                    {
                        // Pokud je datový zdroj DataTable
                        if (dataTable.Columns.Contains(column.DataPropertyName))
                        {
                            dataType = dataTable.Columns[column.DataPropertyName].DataType;
                        }
                    }
                    else if (dgVyrobky.DataSource is BindingSource bindingSource)
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

            #region prace s datagridem Materialy - nastaveni numeric hodnot na pocet desetinnych mist
            try
            {
                // Procházení sloupců v DataGridView
                foreach (DataGridViewColumn column in dgMaterialy.Columns)
                {
                    // Získání typu dat pro sloupec
                    Type dataType = null;

                    if (dgMaterialy.DataSource is DataTable dataTable)
                    {
                        // Pokud je datový zdroj DataTable
                        if (dataTable.Columns.Contains(column.DataPropertyName))
                        {
                            dataType = dataTable.Columns[column.DataPropertyName].DataType;
                        }
                    }
                    else if (dgMaterialy.DataSource is BindingSource bindingSource)
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
        }

        private void dgVyrobky_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            Fask.Logging.ExceptionHandler2.Handle(LogLevel.Error, "Jedá se o chybu ktera vznika nahodnym jezdenim nahoru a dolu v Datagridview...");
            Fask.Logging.ExceptionHandler2.Handle(e.Exception);
        }

        #endregion

        private void dgMaterialy_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            Fask.Logging.ExceptionHandler2.Handle(LogLevel.Error, "Jedá se o chybu ktera vznika nahodnym jezdenim nahoru a dolu v Datagridview...");
            Fask.Logging.ExceptionHandler2.Handle(e.Exception);
        }

        private void advancedDataGridViewSearchToolBarVyrobky_Search(object sender, Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs e)
        {
            bool restartsearch = true;
            int startColumn = 0;
            int startRow = 0;
            if (!e.FromBegin)
            {
                bool endcol = dgVyrobky.CurrentCell.ColumnIndex + 1 >= dgVyrobky.ColumnCount;
                bool endrow = dgVyrobky.CurrentCell.RowIndex + 1 >= dgVyrobky.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dgVyrobky.CurrentCell.ColumnIndex;
                    startRow = dgVyrobky.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dgVyrobky.CurrentCell.ColumnIndex + 1;
                    startRow = dgVyrobky.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dgVyrobky.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dgVyrobky.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dgVyrobky.CurrentCell = c;
        }

        private void advancedDataGridViewSearchToolBarMaterialy_Search(object sender, Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs e)
        {
            bool restartsearch = true;
            int startColumn = 0;
            int startRow = 0;
            if (!e.FromBegin)
            {
                bool endcol = dgMaterialy.CurrentCell.ColumnIndex + 1 >= dgMaterialy.ColumnCount;
                bool endrow = dgMaterialy.CurrentCell.RowIndex + 1 >= dgMaterialy.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dgMaterialy.CurrentCell.ColumnIndex;
                    startRow = dgMaterialy.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dgMaterialy.CurrentCell.ColumnIndex + 1;
                    startRow = dgMaterialy.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dgMaterialy.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dgMaterialy.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dgMaterialy.CurrentCell = c;
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

                if (FASK_Vyroba_TP_Vyrobek_selectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if (FASK_Vyroba_TP_Vyrobek_selectedRows.Count > 1)
                {
                    MessageBox.Show("Je vybráno více záznamů než jeden.", this.Text, MessageBoxButtons.OK);
                    return;
                }

                Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TP_VyrobekDataTable dt = new Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TP_VyrobekDataTable();

                foreach (Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TP_VyrobekRow row in FASK_Vyroba_TP_Vyrobek_selectedRows)
                {
                    try
                    {
                        Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TP_VyrobekRow newRow = dt.NewFASK_Vyroba_TP_VyrobekRow();
                        // Ošetřit delší vstupní pole než počet sloupců tabulky
                        int copyLength = Math.Min(row.ItemArray.Length, dt.Columns.Count);
                        newRow.ItemArray = row.ItemArray.Take(copyLength).ToArray();
                        dt.AddFASK_Vyroba_TP_VyrobekRow(newRow);
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

        private void PrintReport(Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TP_VyrobekDataTable dt)
        {
            try
            {

                if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                {
                    foreach (var item in dt)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start vyroba,vazba materialy TISK rdlc-------------------------------------");

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

                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END vyroba,vazba materialy TISK rdlc-------------------------------------");
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
                    FormOdvod_TiskoveSablony form = new FormOdvod_TiskoveSablony(this.Text, FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Admin(), Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Tisk_Klic_RDLC,false);

                    


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

                if (FASK_Vyroba_TP_Vyrobek_selectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                #region MaR 6.11.2024 vypis zpl do souboru selected rows
                int index_zaznamu = 0;

                foreach (var item in FASK_Vyroba_TP_Vyrobek_selectedRows)
                {

                    if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start vyroba/ciselniky/vazba materialy TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");

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

                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END vyroba/ciselniky/vazba materialy TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");


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



                    foreach (var item in FASK_Vyroba_TP_Vyrobek_selectedRows)
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
                    Tisk_selected_rows_Primy_Tisk(plr_Path, true, MN_ToTisk, FASK_Vyroba_TP_Vyrobek_selectedRows, NazevVychoziTiskarny);

                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Tisk_selected_rows_Primy_Tisk(string plr_Path, bool MnozstvuAutoJedna, int MN_ToTisk, List<Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TP_VyrobekRow> FASK_ZASOBY_selected_Rows, string nazevVychTiskarny)
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

                if (FASK_Vyroba_TP_Vyrobek_selectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                //if (Material_selectedRows == null)
                //{
                //    MessageBox.Show("Není vybrán záznam materiálu pro tisk!", this.Text, MessageBoxButtons.OK);
                //    return;
                //}

                //if (FASK_Vyroba_TP_Vyrobek_selectedRow.Count > 1)
                //{
                //    //MessageBox.Show("Je vybráno více záznamů než jeden.", this.Text, MessageBoxButtons.OK);
                //    //return;
                //}

                Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TP_VyrobekDataTable dtVyrobky = new Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TP_VyrobekDataTable();

                foreach (Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TP_VyrobekRow row in FASK_Vyroba_TP_Vyrobek_selectedRows)
                {
                    try
                    {
                        Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TP_VyrobekRow newRow = dtVyrobky.NewFASK_Vyroba_TP_VyrobekRow();
                        // Ošetřit delší vstupní pole než počet sloupců tabulky
                        int copyLength = Math.Min(row.ItemArray.Length, dtVyrobky.Columns.Count);
                        newRow.ItemArray = row.ItemArray.Take(copyLength).ToArray();
                        dtVyrobky.AddFASK_Vyroba_TP_VyrobekRow(newRow);
                    }
                    catch (Exception ex)
                    {
                        // Ošetření vyjimky
                        // Můžete zde provést logování chyby nebo jiné požadované akce
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                    }
                }

                //5.9.2025 MaR CK
                dtVyrobky.Columns.Add("VNDITNUM_Vyrobky_IMG", typeof(string));
                //dtVyrobky.Columns.Add("CZ_CarKod_IMG", typeof(string));
                //dtVyrobky.Columns.Add("LOCNCODE_IMG", typeof(string));
                foreach (Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TP_VyrobekRow row in dtVyrobky)
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

                            row["VNDITNUM_Vyrobky_IMG"] = Base64Imahe;
                        }

                     
                    }
                    catch (Exception ex)
                    {

                        Fask.Logging.ExceptionHandler2.Handle(ex);
                    }


                }


                Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TP_MaterialDataTable dtMaterialy = new Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TP_MaterialDataTable();

                foreach (Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TP_MaterialRow row in Material_allRows)
                {
                    try
                    {
                        Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TP_MaterialRow newRow = dtMaterialy.NewFASK_Vyroba_TP_MaterialRow();
                        // Ošetřit delší vstupní pole než počet sloupců tabulky
                        int copyLength = Math.Min(row.ItemArray.Length, dtMaterialy.Columns.Count);
                        newRow.ItemArray = row.ItemArray.Take(copyLength).ToArray();
                        dtMaterialy.AddFASK_Vyroba_TP_MaterialRow(newRow);
                    }
                    catch (Exception ex)
                    {
                        // Ošetření vyjimky
                        // Můžete zde provést logování chyby nebo jiné požadované akce
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                    }
                }

                //5.9.2025 MaR CK
                dtMaterialy.Columns.Add("VNDITNUM_Materialy_IMG", typeof(string));
                //dtVyrobky.Columns.Add("CZ_CarKod_IMG", typeof(string));
                //dtVyrobky.Columns.Add("LOCNCODE_IMG", typeof(string));
                foreach (Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TP_MaterialRow row in dtMaterialy)
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

                            row["VNDITNUM_Materialy_IMG"] = Base64Imahe;
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
                PrintReport_RDLC(dtVyrobky, dtMaterialy, primyTisk, path_sablona, nazevVychTiskarny, MN_ToTisk);

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        }

        private void PrintReport_RDLC(Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TP_VyrobekDataTable dtVyrobky, Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TP_MaterialDataTable dtMaterialy, bool primyTisk, string path_sablona, string nazevVychTiskarny, int pocetVytisku)
        {
            try
            {

                #region logovani tisku
                if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                {
                    foreach (var item in dtVyrobky)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start vyroba/ciselniky/vazba materialy TISK rdlc-------------------------------------");

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

                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END vyroba/ciselniky/vazba materialy TISK rdlc-------------------------------------");
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

                    plr.DataSources = new Dictionary<string, object>
{
    { "VyrobkyDataSet", dtVyrobky },
    { "MaterialyDataSet", dtMaterialy }
};


                    //plr.NazevDataTable = "VyrobkyDataSet";
                    //plr.DataTable = dt;

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

                if (FASK_Vyroba_TP_Vyrobek_selectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                #region MaR 6.11.2024 vypis zpl do souboru selected rows
                int index_zaznamu = 0;

                foreach (var item in FASK_Vyroba_TP_Vyrobek_selectedRows)
                {

                    if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start vyroba/ciselniky/vazba materialy TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");

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

                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END vyroba/ciselniky/vazba materialy TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");


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



                    foreach (var item in FASK_Vyroba_TP_Vyrobek_selectedRows)
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
                    Tisk_selected_rows(plr_Path, true, MN_ToTisk, FASK_Vyroba_TP_Vyrobek_selectedRows);

                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Tisk_selected_rows(string plr_Path, bool MnozstvuAutoJedna, int MN_ToTisk, List<Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TP_VyrobekRow> FASK_ZASOBY_selected_Rows)
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
            Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TP_VyrobekRow row,
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

                Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TP_VyrobekDataTable dt = new Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TP_VyrobekDataTable();

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

        private Dictionary<string, string> PrepareDataToTisk(Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TP_VyrobekRow productionRow_data)
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

                        Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TP_VyrobekDataTable dt = new Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TP_VyrobekDataTable();


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


        #region Import

        private void tsmiImport_Vyrobky_Click(object sender, EventArgs e)
        {
            bool uspech = false;

            try
            {
                if (!opravneniImport)
                {
                    MessageBox.Show("Nemáte dostatečné oprávnění pro tuto akci!", "Oprávnění", MessageBoxButtons.OK);
                    return;
                }


                using (FormImport form = new FormImport("Importovat", "Procedura", "Soubor CSV"))
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

                            throw new NotImplementedException();


                            //List<Tuple<string, string, bool>> tableInfo = new List<Tuple<string, string, bool>>();


                            //// nacteni dat v oddelenem vlakne
                            //if ((providerOdberatele != null) && (providerOdberatele is Fask.Interfaces.Ciselniky.Odberatele.IOdberatele2_ImportOdberatel))
                            //    tableInfo = ((Fask.Interfaces.Ciselniky.Odberatele.IOdberatele2_ImportOdberatel)providerOdberatele).GetOdberateleTableInfo();
                            //else
                            //    throw new NotImplementedException("Provider neimplementuje IZbozi2_ImportZbozi.");


                            ////logika importu zbozi z CSV
                            //string pathToFile = string.Empty;
                            //uspech = ImportDatZFileCSV(tableInfo);

                            ////DialogResult dr = MessageBox.Show("Obsahuje změny, chcete refresh?", "data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            ////if (dr == System.Windows.Forms.DialogResult.No)
                            ////    return;


                            ////PerformVyhledat();


                        }
                        else if (selectedOption == "SQL_Procedura")
                        {
                            DataTable dtchanged = this.dsVyrobky.FASK_Vyroba_TP_Vyrobek.GetChanges();
                           // DataTable dtchanged = this.dsZbozi.FASK_ZASOBY_ALL_KONZOLA.GetChanges();
                            if (dtchanged != null && dtchanged.Rows.Count > 0) //obsahuje zmeny ... 
                            {
                                DialogResult dr = MessageBox.Show("Obsahuje změny, pokračovat?", "data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                if (dr == System.Windows.Forms.DialogResult.No)
                                    return;
                            }

                            if (bwImportVyrobky.IsBusy)
                            {
                                bwImportVyrobky.CancelAsync();
                                while (bwImportVyrobky.IsBusy)
                                {
                                    Application.DoEvents();
                                }
                            }

                            ProgressIndicatorVyrobekStart();

                            int FirstDisplayedScrollingRowIndex = this.dgVyrobky.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

                            bwImportVyrobky.RunWorkerAsync();

                            if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dgVyrobky.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dgVyrobky.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index
                        }
                        else
                        {

                            //nic se neprovede
                            return;
                        }

                        //PerformVyhledat_Vyrobek(null);

                    }
                    else
                    {
                        //nevybrano nic
                        return;
                    }
                }

            }
            catch (Exception ex)
            {

                ProgressIndicatorVyrobekStop();
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

           
        }




        //private bool ImportDatZFileCSV(List<Tuple<string, string, bool>> tableInfo)
        //{
        //    bool uspech = false;
        //    try
        //    {
        //        if (!MySystem.LoginTest.UserLoginTest())
        //            return uspech;

        //        //this.dg_Zbozi.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
        //        uspech = this.dgMaterialy.ImportzCSV_odberatele(tableInfo);

        //    }
        //    catch (Exception ex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
        //        MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return uspech;
        //    }

        //    return uspech;

        //}


        private void bwImportVyrobky_DoWork(object sender, DoWorkEventArgs e)
        {
            string status;

            try
            {

                if (bwImportVyrobky.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }


                // nacteni dat v oddelenem vlakne
                if ((providerVazby != null) && (providerVazby is Fask.Interfaces.Vazby.IVazby2_ImportVyrobky))
                    status = ((Fask.Interfaces.Vazby.IVazby2_ImportVyrobky)providerVazby).ImportVyrobky();
                else
                    throw new NotImplementedException("Provider neimplementuje IVazby2_ImportVyrobky.");

                if (status != "OK")
                {
                    //error...
                }

                if (bwImportVyrobky.CancellationPending)
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

        private void bwImportVyrobky_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
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
                    PerformVyhledat_Vyrobek(null);
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


    }

    public class RememberItem_VazbyMaterialy
    {
        public Fask.Interfaces.Filtry.VazbyMaterialyFiltr filtr;
        public string ITEMBMBR;
        public Fask.Interfaces.DataSets.Vyroba ds;

        public RememberItem_VazbyMaterialy(Fask.Interfaces.Filtry.VazbyMaterialyFiltr _filtr, string _ITEMBMBR, Fask.Interfaces.DataSets.Vyroba _ds)
        {
            filtr = _filtr;
            ITEMBMBR = _ITEMBMBR;
            ds = _ds;
        }
 
    }

}
