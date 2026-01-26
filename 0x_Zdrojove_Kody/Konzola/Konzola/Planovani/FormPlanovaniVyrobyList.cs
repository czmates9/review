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
using System.IO;
using Fask.Interfaces.DataSets;
using Fask.Interfaces.Classes;
using MST_Print_Server_ZPL_Printing;
using Konzola.Vyroba.Rozbory;
using Konzola.Forms;

namespace Konzola.Planovani
{
    public partial class FormPlanovaniVyrobyList : Form
    {

        #region Parametry

        private Fask.Interfaces.IMES providerPV = null;
        private Fask.Interfaces.IMES providerVPP = null;
        private Fask.Interfaces.IMES providerZbozi = null;
        private Fask.Interfaces.IMES providerVydej = null;
        private Fask.Interfaces.IMES providerTisk = null; //**DONE


        private List<Fask.Interfaces.Filtry.Vyroba_PV_Filtr> filtry = new List<Fask.Interfaces.Filtry.Vyroba_PV_Filtr>();

        /// <summary>
        /// Zvolené id před seřazením
        /// </summary>
        string sortedID = string.Empty;

        public Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVPRow FASK_Vyroba_PVP_selectedRow
        {
            get
            {
                try
                {
                    return ((DataRowView)(dg_PV.BindingContext[bs_PV].Current)).Row as Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVPRow;
                }
                catch
                {
                    return null;
                }
            }
        }


        private List<Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVPRow> FASK_Vyroba_PVP_selectedRows
        {
            //get
            //{
            //    List<Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVPRow> rows = new List<Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVPRow>();

            //    foreach (DataGridViewRow selectedRow in dg_PV.SelectedRows)
            //    {
            //        Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVPRow row = ((DataRowView)selectedRow.DataBoundItem).Row as Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVPRow;
            //        rows.Add(row);
            //    }

            //    return rows;
            //}

            get
            {
                return dg_PV.SelectedRows
                    .Cast<DataGridViewRow>()
                    .OrderBy(r => r.Index)               // zarovnání na vizuální pořadí v gridu
                    .Select(r => ((DataRowView)r.DataBoundItem).Row as Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVPRow)
                    .Where(r => r != null)
                    .ToList();
            }
        }

        public Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVP_HlavickyRow SelectedRowHlavicky
        {
            get
            {
                try
                {
                    return ((DataRowView)(dg_PV.BindingContext[bs_PV_H].Current)).Row as Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVP_HlavickyRow;
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Vybrany filtr.
        /// </summary>
        private Fask.Interfaces.Filtry.Vyroba_PV_Filtr rowFiltr
        {
            get
            {
                try
                {
                    return tscbFiltry.SelectedItem as Fask.Interfaces.Filtry.Vyroba_PV_Filtr;
                }
                catch
                {
                    return null;
                }
            }
        }

        ContextMenu m = new ContextMenu();

        private string _ConnectionString_FASK = string.Empty;
        private string _ConnectionString_POHODA = string.Empty;

        #endregion

        #region Eventy Formu

        public FormPlanovaniVyrobyList()
        {
            InitializeComponent();

            this.dg_PV.UpdateColumnHeaderCellsByDatasource();
            this.dg_PV_H.UpdateColumnHeaderCellsByDatasource();
            
            m.MenuItems.Add(new MenuItem(tsmiOznacKZaplanovani.Text, tsmiOznacKZaplanovani_Click));
            m.MenuItems.Add(new MenuItem(tsmiOdznacKZaplanovani.Text, tsmiOdznacKZaplanovani_Click));
            m.MenuItems.Add(new MenuItem("Detail karta", dg_PV_DoubleClick));
            m.MenuItems.Add(new MenuItem("Označit navržené", OznacitNavrzene_Click));

            panelButtons.Menu = menuStrip1;

            
        }

    

        private Opravneni opravneni_pravo;

        private bool opravneniEditace = false;
        private bool opravneniImport = false;
        private bool opravneniArchivace = false;

        public FormPlanovaniVyrobyList(Opravneni opravneni)
        {
            InitializeComponent();

            this.dg_PV.UpdateColumnHeaderCellsByDatasource();
            this.dg_PV_H.UpdateColumnHeaderCellsByDatasource();

            m.MenuItems.Add(new MenuItem(tsmiOznacKZaplanovani.Text, tsmiOznacKZaplanovani_Click));
            m.MenuItems.Add(new MenuItem(tsmiOdznacKZaplanovani.Text, tsmiOdznacKZaplanovani_Click));
            m.MenuItems.Add(new MenuItem("Detail karta", dg_PV_DoubleClick));
            m.MenuItems.Add(new MenuItem("Označit navržené", OznacitNavrzene_Click));

            // panelButtons.Menu = menuStrip1;
            // Uloží oprávnění pro další použití v programu
            this.opravneni_pravo = opravneni;

            SetOpravneni();

            //menuStrip1.Items.

            //if (!opravneniArchivace)
            //{
            //    menuStrip1.Items.Remove(tsmiAkce);
            //    tsmiAkce.DropDownItems.Remove(tsmiArchivaceVybrane);
            //}

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

        private void FormPlanovaniVyrobyList_Load(object sender, EventArgs e)
        {
            try
            {
                Konfigurace.Globals_Konfig_Konzola.LoadConfiguration();

                this.ShowIcon = false;
                this.WindowState = FormWindowState.Maximized;


                // načtení konfigurace datagridu z nastavení aplikace
                this.dg_PV.LoadConfiguration(this.GetType().ToString() + "_P");
                this.dg_PV_H.LoadConfiguration(this.GetType().ToString() + "_H");

                panelButtons.LoadConfiguration(this.GetType().ToString());
                panelButtons.Init();
                panelButtons.Size = new Size(85, 700);

                advancedDataGridViewSearchToolBar1.SetColumns(dg_PV.Columns);
                advancedDataGridViewSearchToolBar2.SetColumns(dg_PV_H.Columns);

                //// načtení konfigurace vytvořených filtrů
                this.filtry = Extensions.ListExt.ReadFromXML<Fask.Interfaces.Filtry.Vyroba_PV_Filtr>(this.GetType().ToString() + ".filtr");
                tscbFiltry.ComboBox.DataSource = this.filtry;
                tscbFiltry.SelectedItem = null;
                tscbFiltry.ComboBox.DropDownWitdhAutosize();

                // inicializace providera
                InitProvider();

                if (providerPV == null) //neni v sql
                    throw new Exception("Provider 'PVyroba' není inicializován");

                if (providerVPP == null)
                    throw new Exception("Provider 'VPP' není inicializován");


                if (providerZbozi == null)
                    throw new Exception("Provider 'ZasobyParametry' není inicializován");

                if (providerVydej == null)
                    throw new Exception("Provider 'Vydej' není inicializován");

                if ((providerPV != null) && (providerPV is Fask.Interfaces.Parametry.IParametry2_Get_ConnectionStrings))
                    ((Fask.Interfaces.Parametry.IParametry2_Get_ConnectionStrings)providerPV).Get_ConnectionStrings(out _ConnectionString_FASK,out _ConnectionString_POHODA);


                tabControl1_SelectedIndexChanged(null,null);

                if (!string.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Planovani[0].FormPlanovaniVyrobyList_TabPage))
                {
                    string tb1 = tabPage1.Name.Trim();
                    string tb2 = tabPage2.Name.Trim();

                    if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.Planovani[0].FormPlanovaniVyrobyList_TabPage.Trim() == tb1)
                    {
                        tabControl1.SelectedTab = tabPage1;
                    }
                    else if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.Planovani[0].FormPlanovaniVyrobyList_TabPage.Trim() == tb2)
                    {
                        tabControl1.SelectedTab = tabPage2;
                    }
                    else
                    {
                        // Neznam...
                    }

                }

                dtp_Zaplanovano_OD.Checked = false;
                dtp_Zaplanovano_DO.Checked = false;

                cb_Zaplanovano_TimeVariant.Items.AddRange(Fask.Interfaces.Classes.TimeFilters.GetNumberNameRange());
                cb_Zaplanovano_TimeVariant.SelectedIndex = 0;

                buttonVyhledat.Focus();

           
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormPlanovaniVyrobyList_KeyDown(object sender, KeyEventArgs e)
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

        private void FormPlanovaniVyrobyList_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
           
                this.dg_PV.SaveConfiguration(this.GetType().ToString() + "_P");
                this.dg_PV_H.SaveConfiguration(this.GetType().ToString() + "_H");

                panelButtons.SaveConfiguration(this.GetType().ToString());

                // ulozeni vsech filtru
                this.filtry.WriteXML(this.GetType().ToString() + ".filtr");

                Konfigurace.Globals_Konfig_Konzola.Konfigurace.Planovani[0].FormPlanovaniVyrobyList_TabPage = tabControl1.SelectedTab.Name.Trim();
                Konfigurace.Globals_Konfig_Konzola.SaveConfiguration();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormPlanovaniVyrobyList_Shown(object sender, EventArgs e)
        {
            try
            {
                this.progressIndicator1.Size = new Size(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ostatni[0].ProgressIndicatorSize, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ostatni[0].ProgressIndicatorSize);
                this.progressIndicator1.Location = new Point(this.dg_PV.Location.X + (this.dg_PV.Width / 2) - (progressIndicator1.Size.Width / 2), this.dg_PV.Location.Y + (this.dg_PV.Height / 2) - (progressIndicator1.Size.Height / 2));
            }
            catch { }
            try
            {
                // 11.8.2016 PeV: zakomentovano automaticke vyhledavani pri otevreni formulare
                //PerformOK();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Inicializce provideru

        /// <summary>
        /// Inicializace providera
        /// </summary>
        private void InitProvider()
        {
            #region PV
            try
            {
                if (String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                    return;

                if (providerPV == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Vyroba.PV.IPV).IsAssignableFrom(t))
                            {
                                providerPV = (Fask.Interfaces.Vyroba.PV.IPV)providerAssemlby.CreateInstance(t.FullName);
                                if (providerPV != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                providerPV.InitProvider();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion

            #region VPP

            try
            {
                if (String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                    return;

                if (providerVPP == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Vyroba.VPP.IVPP).IsAssignableFrom(t))
                            {
                                providerVPP = (Fask.Interfaces.Vyroba.VPP.IVPP)providerAssemlby.CreateInstance(t.FullName);
                                if (providerVPP != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }


                providerVPP.InitProvider();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            #endregion

            #region Zasoby Parametry

            try
            {
                if (String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                    return;

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

            #region Vydej

            try
            {
                if (String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                    return;

                if (providerVydej == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Vydej.IVydej2).IsAssignableFrom(t))
                            {
                                providerVydej = (Fask.Interfaces.Vydej.IVydej2)providerAssemlby.CreateInstance(t.FullName);
                                if (providerVydej != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                providerVydej.InitProvider();

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

        #region Eventy DataGridView

        private void dg_PV_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1)
                {
                    if (FASK_Vyroba_PVP_selectedRow != null)
                        sortedID = FASK_Vyroba_PVP_selectedRow.OBJ_NMBR.ToString();
                }
            }
            catch { }
        }

        private void dg_PV_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                m.Show(dg_PV, new Point(e.X, e.Y));
            }
        }

        private void dg_PV_H_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                m.Show(dg_PV_H, new Point(e.X, e.Y));
            }
        }

        private void dg_PV_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewRow row in dg_PV.Rows)
            {

                Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVPRow radek = (Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVPRow)((DataRowView)row.DataBoundItem).Row;

                if (radek == null || radek.Is_TYPE_ROWNull())
                {
                    row.DefaultCellStyle.BackColor = Color.Empty;
                }
                else if (radek._TYPE_ROW == 0)
                {
                    row.DefaultCellStyle.BackColor = Color.Empty;
                }
                else if (radek._TYPE_ROW == 1)
                {
                    row.DefaultCellStyle.BackColor = Color.Empty;
                }
                else if (radek._TYPE_ROW == 2)
                {
                    row.DefaultCellStyle.BackColor = Color.Empty; 
                }
                else if (radek._TYPE_ROW == 3)
                {
                    row.DefaultCellStyle.BackColor = Color.MediumPurple;
                }
                else if (radek._TYPE_ROW == 4)
                {
                    row.DefaultCellStyle.BackColor = Color.SandyBrown;
                }
                else if (radek._TYPE_ROW == 5)
                {
                    row.DefaultCellStyle.BackColor = Color.LightGreen;
                }
                else if (radek._TYPE_ROW == 6)
                {
                    row.DefaultCellStyle.BackColor = Color.Tomato;
                }
            }
        }

        private void dg_PV_H_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewRow row in dg_PV_H.Rows)
            {

                Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVP_HlavickyRow radek = (Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVP_HlavickyRow)((DataRowView)row.DataBoundItem).Row;

                if (radek == null || radek.Is_TYPE_ROWNull())
                {
                    row.DefaultCellStyle.BackColor = Color.Empty;
                }
                else if (radek._TYPE_ROW == 0)
                {
                    row.DefaultCellStyle.BackColor = Color.Empty; 
                }
                else if (radek._TYPE_ROW == 1)
                {
                    row.DefaultCellStyle.BackColor = Color.MediumPurple; 
                }
                else if (radek._TYPE_ROW == 2)
                {
                    row.DefaultCellStyle.BackColor = Color.SandyBrown; 
                }
                else if (radek._TYPE_ROW == 3)
                {
                    row.DefaultCellStyle.BackColor = Color.LightGreen; 
                }
                else if (radek._TYPE_ROW == 4)
                {
                    row.DefaultCellStyle.BackColor = Color.Tomato; 
                }
            }
        }

        #endregion

        #region BW Vyhledavani

        private void bw_PV_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                RememberItem_PlanovaniVyroby Rem = (RememberItem_PlanovaniVyroby)e.Argument;
                Fask.Interfaces.Filtry.Vyroba_PV_Filtr filtr = Rem.filtr;

                //bwLoadSkladPohyb.CancelAsync();
                //Fask.Interfaces.Classes.Vyroba_PV_Filtr filtr = (Fask.Interfaces.Classes.Vyroba_PV_Filtr)e.Argument;
                Fask.Interfaces.DataSets.Vyroba_Planovani ds = new Fask.Interfaces.DataSets.Vyroba_Planovani();

                if (bw_PV.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                // nacteni dat v oddelenem vlakne

                if ((providerPV != null) && (providerPV is Fask.Interfaces.Vyroba.PV.IPV_GetFiltrovanyPVList))
                    ds = ((Fask.Interfaces.Vyroba.PV.IPV_GetFiltrovanyPVList)providerPV).GetFiltrovanyPVList(filtr);
                else
                    throw new NotImplementedException("Provider neimplementuje IPV_GetFiltrovanyPVList.");



                if (bw_PV.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                RememberItem_PlanovaniVyroby tmprem = new RememberItem_PlanovaniVyroby(null, Rem.DEX_ROW_ID, ds);

                e.Result = tmprem;

                //e.Result = ds;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void bw_PV_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                // check error, check cancel, then use result
                if (e.Error != null)
                {
                    // handle the error
                    ds_PV = new Fask.Interfaces.DataSets.Vyroba_Planovani();
                    bs_PV.DataSource = ds_PV;
                }
                else if (e.Cancelled)
                {
                    // handle cancellation
                    // TODO: maji se vymazat data z datasetu?? asi ano ...
                    //if (dsSkladPohyb == null)
                    ds_PV = new Fask.Interfaces.DataSets.Vyroba_Planovani();
                    bs_PV.DataSource = ds_PV;
                }
                else
                {
                    //// uspesne dokonceno ...
                    //// use it on the UI thread
                    //ds_PV = (Fask.Interfaces.DataSets.Vyroba)e.Result;
                    //if (ds_PV == null)
                    //    ds_PV = new Fask.Interfaces.DataSets.Vyroba();

                    //bs_PV.DataSource = ds_PV;



                    ////if (Index != null)
                    ////{
                    ////    var polozky = ds_PV.FASK_Vyroba_PVP.Where(x => x.DEX_ROW_ID == Index);
                    ////    if (polozky.Count() > 0)
                    ////    {
                    ////        var polozka = polozky.First();

                    ////        int index = ((System.Data.DataView)bs_PV.List).Table.Rows.IndexOf(polozka);
                    ////        bs_PV.Position = index;
                    ////    }
                    ////    Index = null;
                    ////}


                    RememberItem_PlanovaniVyroby Rem = (RememberItem_PlanovaniVyroby)e.Result;
                    ds_PV = Rem.ds;

                    if (ds_PV == null)
                        ds_PV = new Fask.Interfaces.DataSets.Vyroba_Planovani();

                    bs_PV.DataSource = ds_PV;

                    if (Rem.DEX_ROW_ID.HasValue)
                    {

                        var Rows = ds_PV.FASK_Vyroba_PVP.Select("DEX_ROW_ID = " + Rem.DEX_ROW_ID);

                        if (Rows.Count() == 1)
                        {

                            dg_PV.ClearSelection();
                            bs_PV.FindAndSelect(
                                new Key { PropertyName = ds_PV.FASK_Vyroba_PVP.DEX_ROW_IDColumn.ColumnName, Value = Rem.DEX_ROW_ID }
                                );
                        }
                    }


                    var dt_Grup = ds_PV.FASK_Vyroba_PVP.GroupBy(x => new
                    {
                        OBJ_NMBR = (x.IsOBJ_NMBRNull() ? string.Empty : x.OBJ_NMBR),
                        OBJ_DESC = (x.IsOBJ_DESCNull() ? string.Empty : x.OBJ_DESC),
                        OBJ_TYPE = (x.IsOBJ_TYPENull() ? string.Empty : x.OBJ_TYPE),
                        OBJ_COMPANY = (x.IsOBJ_COMPANYNull() ? string.Empty : x.OBJ_COMPANY),
                        OBJ_DATE_FROM = (x.IsOBJ_DATE_FROMNull() ? DateTime.MinValue : x.OBJ_DATE_FROM),
                        OBJ_DATE_TO = (x.IsOBJ_DATE_TONull() ? DateTime.MinValue : x.OBJ_DATE_TO),
                        OBJ_ORD = (x.IsOBJ_ORDNull() ? -1 : x.OBJ_ORD),
                        USERID = (x.IsUSERIDNull() ? -1 : x.USERID)
                    });

                    foreach (var item in dt_Grup)
                    {

                        var row = ds_PV.FASK_Vyroba_PVP_Hlavicky.NewFASK_Vyroba_PVP_HlavickyRow();

                        row.OBJ_NMBR = item.Key.OBJ_NMBR;
                        row.OBJ_DESC = item.Key.OBJ_DESC;
                        row.OBJ_TYPE = item.Key.OBJ_TYPE;
                        row.OBJ_COMPANY = item.Key.OBJ_COMPANY;

                        if (item.Key.OBJ_DATE_FROM == DateTime.MinValue)
                        {
                            row.SetOBJ_DATE_FROMNull();
                        }
                        else
                        {
                            row.OBJ_DATE_FROM = item.Key.OBJ_DATE_FROM;
                        }

                        if (item.Key.OBJ_DATE_TO == DateTime.MinValue)
                        {
                            row.SetOBJ_DATE_TONull();
                        }
                        else
                        {
                            row.OBJ_DATE_TO = item.Key.OBJ_DATE_TO;
                        }


                        row.OBJ_ORD = item.Key.OBJ_ORD;
                        row.QTY = item.Sum(x => x.QTY);

                        if (item.Key.USERID == -1)
                        {
                            row.SetUSERIDNull();
                        }
                        else
                        {
                            row.USERID = item.Key.USERID;
                        }


                        #region Stav

                        var RowInGup = item.ToList();

                        List<int> stav0 = new List<int>();
                        List<int> stav1 = new List<int>();
                        List<int> stav2 = new List<int>();
                        List<int> stav3 = new List<int>();
                        List<int> stav4 = new List<int>();
                        List<int> stav5 = new List<int>();
                        List<int> stav6 = new List<int>();


                        foreach (var Radek in RowInGup)
                        {
                            switch (Radek._TYPE_ROW)
                            {
                                case 0:
                                    stav0.Add(0);
                                    break;
                                case 1:
                                    stav1.Add(1);
                                    break;
                                case 2:
                                    stav2.Add(2);
                                    break;
                                case 3:
                                    stav3.Add(3);
                                    break;
                                case 4:
                                    stav4.Add(4);
                                    break;
                                case 5:
                                    stav5.Add(5);
                                    break;
                                case 6:
                                    stav6.Add(6);
                                    break;
                                default:
                                    break;
                            }
                        }

                        //Varianta NIC
                        if ((stav0.Count > 0 ||
                            stav1.Count > 0 ||
                            stav2.Count > 0) &&
                            stav3.Count == 0 &&
                            stav4.Count == 0 &&
                            stav5.Count == 0 &&
                            stav6.Count == 0
                            )
                        {
                            row._TYPE_ROW = 0;
                        }
                        //Varianta Označeno
                        else if (stav0.Count == 0 &&
                            stav1.Count == 0 &&
                            stav2.Count == 0 &&
                            (stav3.Count > 0 ||
                            stav4.Count > 0) &&
                            stav5.Count == 0 &&
                            stav6.Count == 0
                            )
                        {
                            row._TYPE_ROW = 1;
                        }

                        //Varianta zaplanovano
                        else if ((stav0.Count > 0 ||
                            stav1.Count > 0 ||
                            stav2.Count > 0 ||
                            stav3.Count > 0 ||
                            stav4.Count > 0) &&
                            stav5.Count == 0 &&
                            stav6.Count == 0
                            )
                        {
                            row._TYPE_ROW = 2;
                        }

                        //Varianta Zaplanovano VSE
                        else if (stav0.Count == 0 &&
                            stav1.Count == 0 &&
                            stav2.Count == 0 &&
                            stav3.Count == 0 &&
                            stav4.Count == 0 &&
                            stav5.Count > 0 &&
                            stav6.Count == 0
                            )
                        {
                            row._TYPE_ROW = 3;
                        }

                        //Varianta Zaplanovano castecne
                        else if (stav0.Count > 0 ||
                            stav1.Count > 0 ||
                            stav2.Count > 0 ||
                            stav3.Count > 0 ||
                            stav4.Count > 0 ||
                            stav5.Count > 0 ||
                            stav6.Count > 0
                            )
                        {
                            row._TYPE_ROW = 4;
                        }
                        else
                        {
                            string msg = string.Format("{0},{1},{2},{3},{4},{5},{6}", stav0.Count, stav1.Count, stav2.Count, stav3.Count, stav4.Count, stav5.Count, stav6.Count);
                            MessageBox.Show(msg);
                        }

                        #endregion


                        ds_PV.FASK_Vyroba_PVP_Hlavicky.AddFASK_Vyroba_PVP_HlavickyRow(row);
                    }

                    bs_PV_H.DataSource = ds_PV;
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
                this.progressIndicator1.Location = new Point(this.dg_PV.Location.X + (this.dg_PV.Width / 2) - (progressIndicator1.Size.Width / 2), this.dg_PV.Location.Y + (this.dg_PV.Height / 2) - (progressIndicator1.Size.Height / 2));
            }
            catch { }
            progressIndicator1.Start();
            progressIndicator1.Visible = true;
        }

        #endregion

        #region FILTRY


        private void tsbZmena_Click(object sender, EventArgs e)
        {
            PerformZmenitFiltr();
        }

        /// <summary>
        /// Vytvoreni filtru, ktery se pouzije.
        /// </summary>
        /// <param name="filtr"></param>
        /// <returns></returns>
        private bool CreateFilter(ref Fask.Interfaces.Filtry.Vyroba_PV_Filtr filtr)
        {
            if (filtr == null)
                filtr = new Fask.Interfaces.Filtry.Vyroba_PV_Filtr();

            filtr.OBJ = tb_SOPNUMBE.Text.Trim();
            filtr.ITEMNMBR = tb_ITEMNMBR.Text.Trim();

            filtr.DatumDO = dtp_DatumDo.Value.Date;
            filtr.DatumOD = dtp_DatumOd.Value.Date;

            filtr.DatumDO_Check = dtp_DatumDo.Checked;
            filtr.DatumOD_Check = dtp_DatumOd.Checked;

            filtr.Firma = tb_Firma.Text.Trim();
            filtr.Kod = tb_Kod.Text.Trim();
            filtr.NEzaplanovane = chb_NEzap.Checked;
            filtr.SOPNUMBE = tb_VyrZak.Text.Trim();
            filtr.zaplanovane = chb_Zap.Checked;

            #region Dateeve

            if (dtp_Zaplanovano_OD.Checked && dtp_Zaplanovano_OD.Enabled)
            {
                filtr.Zaplanovano_OD = dtp_Zaplanovano_OD.Value;
            }
            else
                filtr.Zaplanovano_OD = null;

            if (dtp_Zaplanovano_DO.Checked && dtp_Zaplanovano_DO.Enabled)
            {
                filtr.Zaplanovano_DO = dtp_Zaplanovano_DO.Value;
            }
            else
                filtr.Zaplanovano_DO = null;

            #endregion

            #region Time variant dateeve

            var por = GetTimeVariantFromFilter_Zaplanovano();

            if (por.Contains("unknow"))
            {
                filtr.Zaplanovano_TimeVariant = null;
            }
            else
            {
                filtr.Zaplanovano_TimeVariant = por;
            }

            #endregion

            return true;
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

                Fask.Interfaces.Filtry.Vyroba_PV_Filtr filtr = rowFiltr;

                if (!CreateFilter(ref filtr))
                    return;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show("Nepodařilo se upravit filtr.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbOdebrat_Click(object sender, EventArgs e)
        {
            PerformOdebratFiltr();
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

        private void tsbPridat_Click(object sender, EventArgs e)
        {
            PerformPridatFiltr();
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

                Fask.Interfaces.Filtry.Vyroba_PV_Filtr filtr = new Fask.Interfaces.Filtry.Vyroba_PV_Filtr();
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

        private void tsbNastavit_Click(object sender, EventArgs e)
        {
            PerformNastavitFiltr(rowFiltr);
        }

        /// <summary>
        /// Nastavi zvoleny filtr do comboboxu, ...
        /// </summary>
        /// <param name="filtr"></param>
        private void PerformNastavitFiltr(Fask.Interfaces.Filtry.Vyroba_PV_Filtr filtr)
        {
            try
            {
                if (filtr == null)
                    return;

                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                PerformVycistitFiltr();

                // sopnumbe
                tb_SOPNUMBE.Text = filtr.OBJ;
                tb_ITEMNMBR.Text = filtr.ITEMNMBR;
                dtp_DatumDo.Value = filtr.DatumDO;
                dtp_DatumOd.Value = filtr.DatumOD;

                dtp_DatumDo.Checked = filtr.DatumDO_Check;
                dtp_DatumOd.Checked = filtr.DatumOD_Check;

                tb_Firma.Text = filtr.Firma;
                tb_Kod.Text = filtr.Kod;
                chb_NEzap.Checked = filtr.NEzaplanovane;
                tb_VyrZak.Text = filtr.SOPNUMBE;
                chb_Zap.Checked = filtr.zaplanovane;

                if (filtr.Zaplanovano_DO != null)
                {
                    dtp_Zaplanovano_DO.Checked = true;
                    dtp_Zaplanovano_DO.Value = (DateTime)filtr.Zaplanovano_DO;
                }

                if (filtr.Zaplanovano_OD != null)
                {
                    dtp_Zaplanovano_OD.Checked = true;
                    dtp_Zaplanovano_OD.Value = (DateTime)filtr.Zaplanovano_OD;
                }

                if (!string.IsNullOrEmpty(filtr.Zaplanovano_TimeVariant))
                {

                    var arr = filtr.Zaplanovano_TimeVariant.Split(';');
                    Fask.Interfaces.Classes.TimeFilters.TimeVariants TimeVar = (Fask.Interfaces.Classes.TimeFilters.TimeVariants)Enum.Parse(typeof(TimeFilters.TimeVariants), arr[0], true);

                    TimeVariantName time = new TimeVariantName(TimeVar, arr[1]);

                    cb_Zaplanovano_TimeVariant.SelectedItem = time;
                }


            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show("Nepodařilo se načíst data filtru.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbVycistit_Click(object sender, EventArgs e)
        {
            PerformVycistitFiltr();
        }

        private void PerformVycistitFiltr()
        {
            try
            {
                tb_SOPNUMBE.Text = string.Empty;
                tb_ITEMNMBR.Text = string.Empty;
                dtp_DatumDo.Value = DateTime.Now;
                dtp_DatumOd.Value = DateTime.Now;
                tb_Firma.Text = string.Empty;
                tb_Kod.Text = string.Empty;
                chb_NEzap.Checked = false;
                tb_VyrZak.Text = string.Empty;
                chb_Zap.Checked = false;

                dtp_DatumOd.Checked = false;
                dtp_DatumDo.Checked = false;

                dtp_Zaplanovano_OD.Checked = false;
                dtp_Zaplanovano_DO.Checked = false;
                cb_Zaplanovano_TimeVariant.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                TabPage tab = tabControl1.SelectedTab;

                if (tab.Name == tabPage1.Name) // přehled
                {
                    this.dg_PV.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
                }
                else if (tab.Name == tabPage2.Name) // jen součty
                {
                    this.dg_PV_H.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
                }
                else
                {
                    //Neznamy...
                }
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

                TabPage tab = tabControl1.SelectedTab;

                if (tab.Name == tabPage1.Name) // přehled
                {
                    this.dg_PV.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
                }
                else if (tab.Name == tabPage2.Name) // jen součty
                {
                    this.dg_PV_H.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
                }
                else
                {
                    //Neznamy...
                }
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

                TabPage tab = tabControl1.SelectedTab;

                if (tab.Name == tabPage1.Name) // přehled
                {
                    this.dg_PV.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
                }
                else if (tab.Name == tabPage2.Name) // jen součty
                {
                    this.dg_PV_H.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
                }
                else
                {
                    //Neznamy...
                }
                this.dg_PV.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                TabPage tab = tabControl1.SelectedTab;

                if (tab.Name == tabPage1.Name) // přehled
                {
                    dg_PV.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
                }
                else if (tab.Name == tabPage2.Name) // jen součty
                {
                    dg_PV_H.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
                }
                else
                {
                    //Neznamy...
                }
                
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

                TabPage tab = tabControl1.SelectedTab;

                if (tab.Name == tabPage1.Name) // přehled
                {
                    dg_PV.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
                }
                else if (tab.Name == tabPage2.Name) // jen součty
                {
                    dg_PV_H.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
                }
                else
                {
                    //Neznamy...
                }
                
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

                TabPage tab = tabControl1.SelectedTab;

                if (tab.Name == tabPage1.Name) // přehled
                {
                    dg_PV.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
                }
                else if (tab.Name == tabPage2.Name) // jen součty
                {
                    dg_PV_H.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
                }
                else
                {
                    //Neznamy...
                }
                
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #endregion

        #region Tisk

        private void tsmiTisk_Click(object sender, EventArgs e)
        {
            Perform_Tisk();
        }

        #endregion

        #region Pomocne metody

        /// <summary>
        /// Metoda ktera vratí typovy dataset z vybraného čidku GataGridView
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVPRow GetSelectedRow(DataGridViewRow item)
        {
            try
            {
                return ((item.DataBoundItem as DataRowView).Row) as Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVPRow;
            }
            catch (Exception ex)
            {

                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return null;
            }
        }

        /// <summary>
        /// Metoda ktera vratí typovy dataset z vybraného čidku GataGridView
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVP_HlavickyRow GetSelectedRowHlavicky(DataGridViewRow item)
        {
            try
            {
                return ((item.DataBoundItem as DataRowView).Row) as Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVP_HlavickyRow;
            }
            catch (Exception ex)
            {

                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return null;
            }
        }



        #endregion

        #region Click Eventy

        private void buttonVyhledat_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        private void buttonOznacitVse_Click(object sender, EventArgs e)
        {
            try
            {
                TabPage tab = tabControl1.SelectedTab;

                if (tab.Name == tabPage1.Name) // přehled
                {
                    this.dg_PV.SelectAll();
                }
                else if (tab.Name == tabPage2.Name) // jen součty
                {
                    this.dg_PV_H.SelectAll();
                }
                else
                {
                    //Neznamy...
                }
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
                TabPage tab = tabControl1.SelectedTab;

                if (tab.Name == tabPage1.Name) // přehled
                {
                    this.dg_PV.ClearSelection();
                }
                else if (tab.Name == tabPage2.Name) // jen součty
                {
                    this.dg_PV_H.ClearSelection();
                }
                else
                {
                    //Neznamy...
                }
                
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsmiKonec_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void tsmiNacteniObchodnihoPozadavku_Click(object sender, EventArgs e)
        {
            //24.11.2025 MaR issue #167 zakomentovano a predelano na import
            //stejny okno

            Perform_Zaplanovani();

            //24.11.2025 MaR issue #167 predelano na import
           // Importovat();
            PerformOK();
        }

        private void tsmiOznacKZaplanovani_Click(object sender, EventArgs e)
        {
            TabPage tab = tabControl1.SelectedTab;

            if (tab.Name == tabPage1.Name) // přehled
            {
                Perform_Oznac();
            }
            else if (tab.Name == tabPage2.Name) // jen součty
            {
                Perform_Oznac_Hlavicka();
            }
            else
            {
                //Neznamy...
            }
            
            PerformOK();
        }

        private void tsmiOdznacKZaplanovani_Click(object sender, EventArgs e)
        {
            TabPage tab = tabControl1.SelectedTab;

            if (tab.Name == tabPage1.Name) // přehled
            {
                Perform_ODznac();
            }
            else if (tab.Name == tabPage2.Name) // jen součty
            {
                Perform_ODznac_Hlavicka();
            }
            else
            {
                //Neznamy...
            }
            
            PerformOK();
        }

        private void tsmiNavrh_Click(object sender, EventArgs e)
        {
            if (!MySystem.LoginTest.UserLoginTest())
                return;

            TabPage tab = tabControl1.SelectedTab;

            if (tab.Name == tabPage1.Name) // přehled
            {
                Perform_Navrh();
            }
            else if (tab.Name == tabPage2.Name) // jen součty
            {
                //Perform_Navrh_Hlavicky();
            }
            else
            {
                //Neznamy...
            }

            PerformOK();
        }

        private void tsmiZaplanovani_Vyroby_Click(object sender, EventArgs e)
        {
            if (!MySystem.LoginTest.UserLoginTest())
                return;

            TabPage tab = tabControl1.SelectedTab;

            if (tab.Name == tabPage1.Name) // přehled
            {
                Perform_Zaplanuj_DoVyroby();
            }
            else if (tab.Name == tabPage2.Name) // jen součty
            {
                Perform_Zaplanuj_DoVyroby_Hlavicky();
            }
            else
            {
                //Neznamy...
            }
            
            PerformOK();
        }

        private void tsmiZaplanovani_Vydeje_Click(object sender, EventArgs e)
        {
            if (!MySystem.LoginTest.UserLoginTest())
                return;

            TabPage tab = tabControl1.SelectedTab;

            if (tab.Name == tabPage1.Name) // přehled
            {
                Perform_Zaplanuj_DoVydeje();
            }
            else if (tab.Name == tabPage2.Name) // jen součty
            {
                Perform_Zaplanuj_DoVydeje_Hlavicky();
            }
            else
            {
                //Neznamy...
            }

            PerformOK();
        }

        private void tsmiOdstranit_Click(object sender, EventArgs e)
        {
            TabPage tab = tabControl1.SelectedTab;

            if (tab.Name == tabPage1.Name) // přehled
            {
                Perform_Delete();
            }
            else if (tab.Name == tabPage2.Name) // jen součty
            {
                Perform_Delete_Hlavicka();
            }
            else
            {
                //Neznamy...
            }
            
            PerformOK();
        }

        private void tsmiUpravit_Click(object sender, EventArgs e)
        {
            Perorm_Edit();
            PerformOK();
        }

        private void tsmiNovy_Click(object sender, EventArgs e)
        {
            Perform_New();
            PerformOK();
        }

        private void OznacitNavrzene_Click(object sender, EventArgs e)
        {
            try
            {
                TabPage tab = tabControl1.SelectedTab;

                if (tab.Name == tabPage1.Name) // přehled
                {
                    Perform_OznacitNavrzene();
                }
                else if (tab.Name == tabPage2.Name) // jen součty
                {
                    Perform_OznacitNavrzene_Hlavicka();
                }
                else
                {
                    //Neznamy...
                }

                //PerformOK();

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        }

        private void Perform_OznacitNavrzene()
        {

            if ((this.dg_PV.Rows == null) || (this.dg_PV.Rows.Count == 0))
            {
                MessageBox.Show(this, "Neni vybrán záznam označení.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                return;
            }

            this.dg_PV.ClearSelection();


            foreach (DataGridViewRow item in this.dg_PV.Rows)
            {
                var radek = ((item.DataBoundItem as DataRowView).Row) as Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVPRow;

                if ((radek != null) && radek.VP_PRPS)
                {
                    item.Selected = true;
                }
            }
        }

        private void Perform_OznacitNavrzene_Hlavicka()
        {

        }


            private void dg_PV_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if ((this.dg_PV.SelectedRows == null) || (this.dg_PV.SelectedRows.Count == 0))
                {
                    MessageBox.Show(this, "Neni vybrán záznam na detail.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    return;
                }

                if (this.dg_PV.SelectedRows.Count > 1)
                {
                    MessageBox.Show(this, "Je vybrán víc jak jeden záznam.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    return;
                }


                using (var frm = new Konzola.Planovani.Planovani_Detail.Form_Planovani_Detail())
                {
                    frm.ITEMCODE = FASK_Vyroba_PVP_selectedRow.ITEMCODE;
                    frm.ITEMNMBR = FASK_Vyroba_PVP_selectedRow.ITEMNMBR;

                    var dr = frm.ShowDialog();

                    if (dr == DialogResult.OK)
                    {
                        
                    }
                }
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        }

        #endregion

        #region Delete

        /// <summary>
        /// Metoda slouží k smazaní záznamu
        /// </summary>
        private void Perform_Delete()
        {
            try
            {

                //1- Skontroloje zda je vybán aspon jeden záznam k smazaní
                if (this.dg_PV.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Nejsou vybrány záznamy na odstranění", this.Text, MessageBoxButtons.OK);
                    return;
                }

                //Projde všechvy vybrané záznamy
                foreach (DataGridViewRow item in this.dg_PV.SelectedRows)
                {
                    var row = GetSelectedRow(item);
                    DELETE_Logika(row);
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        /// <summary>
        /// Metoda slouží k smazaní záznamu
        /// </summary>
        private void Perform_Delete_Hlavicka()
        {
            try
            {

                //1- Skontroloje zda je vybán aspon jeden záznam k smazaní
                if (this.dg_PV_H.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Nejsou vybrány záznamy na odstranění", this.Text, MessageBoxButtons.OK);
                    return;
                }

                //Projde všechvy vybrané záznamy
                foreach (DataGridViewRow item in this.dg_PV_H.SelectedRows)
                {
                    var row = GetSelectedRowHlavicky(item);

                    var whereDT = ds_PV.FASK_Vyroba_PVP.Where(x =>
                    x.OBJ_NMBR == row.OBJ_NMBR
                    && x.OBJ_ORD == row.OBJ_ORD
                    );

                    foreach (var radek in whereDT)
                    {
                        DELETE_Logika(radek);
                    }
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void DELETE_Logika(Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVPRow row)
        {
            try
            {
                var zapWhere = ds_PV.FASK_Vyroba_PVP.Where(x => x.OBJ_NMBR.Trim() == row.OBJ_NMBR.Trim() && x.ITEMNMBR.Trim() == row.ITEMNMBR.Trim() && !x.IsVP_PRDCT_QTYNull());
                decimal sum = zapWhere.Sum(x => x.VP_PRDCT_QTY);

                ///Pokud je Ref_PVH null, tak je řádek NEzaplánován, tak je možne ho smazat
                if (row.IsRef_PVHNull())
                {
                    bool state;

                    if ((providerPV != null) && (providerPV is Fask.Interfaces.Vyroba.PV.IPV_Delete_PV))
                        state = ((Fask.Interfaces.Vyroba.PV.IPV_Delete_PV)providerPV).Delete(row, sum);
                    else
                        throw new NotImplementedException("Provider neimplementuje IPV_Delete_PV.");


                    if (!state)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Error, string.Format("řádek '{0}'. Neodstranen...", row.DEX_ROW_ID));
                    }
                }
                else
                {
                    MessageBox.Show(this, "Nelze ostranit již zaplánovanou položku.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        #endregion

        #region Označ/Odznač metody

        #region OZNAC

        /// <summary>
        /// Metoda slouží k označení vybraneho řádku
        /// </summary>
        private void Perform_Oznac()
        {
            try
            {

                //1-či je vybran řádek
                //2-Pro každý řádek který neni Zaplánován a Zaroven neni označen tak ho označi a skopiruje množství do VP_PRPS_QTY

                if (this.dg_PV.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Nejsou vybrány záznamy na označení", this.Text, MessageBoxButtons.OK);
                    return;
                }

                foreach (DataGridViewRow item in this.dg_PV.SelectedRows)
                {
                    var row = GetSelectedRow(item);
                    OZNAC_Logika(row);
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        /// <summary>
        /// Metoda slouží k označení vybraneho řádku pomoci Hlavičky
        /// </summary>
        private void Perform_Oznac_Hlavicka()
        {
            try
            {

                //1-či je vybran řádek
                //2-Pro každý řádek který neni Zaplánován a Zaroven neni označen tak ho označi a skopiruje množství do VP_PRPS_QTY

                if (this.dg_PV_H.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Nejsou vybrány záznamy na označení", this.Text, MessageBoxButtons.OK);
                    return;
                }

                foreach (DataGridViewRow item in this.dg_PV_H.SelectedRows)
                {
                    var row = GetSelectedRowHlavicky(item);


                    var whereDT = ds_PV.FASK_Vyroba_PVP.Where(x =>
                    x.OBJ_NMBR == row.OBJ_NMBR
                    && x.OBJ_ORD == row.OBJ_ORD
                    );

                    foreach (var radek in whereDT)
                    {
                        OZNAC_Logika(radek);
                    }

                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void OZNAC_Logika(Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVPRow row)
        {
            try
            {
                if (!row.IsRef_PVHNull())
                    return;


                if (row.VP_PRPS)
                    return;


                //nahovno, nefunguje...
                //var zaplanovano = ds_PV.FASK_Vyroba_PVP.Select("OBJ_NMBR = '" + row.OBJ_NMBR.Trim() + "' AND ITEMNMBR = '" + row.ITEMNMBR.Trim() + "' AND VP_PRDCT_QTY is not null ");
                //var sum = zaplanovano.Sum(x => (decimal)x["VP_PRDCT_QTY"]);


                var zapWhere = ds_PV.FASK_Vyroba_PVP.Where(x => x.OBJ_NMBR.Trim() == row.OBJ_NMBR.Trim() && x.ITEMNMBR.Trim() == row.ITEMNMBR.Trim() && !x.IsVP_PRDCT_QTYNull());
                //decimal sum = zapWhere.Sum(x => x.IsVP_PRDCT_QTYNull() ? 0 : x.VP_PRDCT_QTY);
                decimal sum = zapWhere.Sum(x => x.VP_PRDCT_QTY);


                bool state;
                decimal vysledek = row.QTY - sum;

                if (!row.IsVP_PRPS_QTYNull())
                {
                    vysledek = row.VP_PRPS_QTY;
                }

                if ((providerPV != null) && (providerPV is Fask.Interfaces.Vyroba.PV.IPV_Edit_QTY_PV))
                    state = ((Fask.Interfaces.Vyroba.PV.IPV_Edit_QTY_PV)providerPV).Edit_QTY(row, vysledek, true, string.Empty);
                else
                    throw new NotImplementedException("Provider neimplementuje IPV_Edit_QTY_PV.");


                if (!state)
                {
                    Fask.Logging.ExceptionHandler2.Handle(LogLevel.Error, string.Format("řádek '{0}'. Nezeditovan...", row.DEX_ROW_ID));
                }

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }

        }

        #endregion

        #region ODZNAC

        /// <summary>
        /// Metoda slouží k ODznačení vybraneho řádku
        /// </summary>
        private void Perform_ODznac()
        {
            try
            {
                if (this.dg_PV.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Nejsou vybrány záznamy na odznačení", this.Text, MessageBoxButtons.OK);
                    return;
                }

                foreach (DataGridViewRow item in this.dg_PV.SelectedRows)
                {
                    var row = GetSelectedRow(item);
                    ODZNAC_Logika(row);
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        /// <summary>
        /// Metoda slouží k ODznačení vybraneho řádku, pomoci hlavičky
        /// </summary>
        private void Perform_ODznac_Hlavicka()
        {
            try
            {

                if (this.dg_PV_H.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Nejsou vybrány záznamy na odznačení", this.Text, MessageBoxButtons.OK);
                    return;
                }


                foreach (DataGridViewRow item in this.dg_PV_H.SelectedRows)
                {
                    var row = GetSelectedRowHlavicky(item);


                    var whereDT = ds_PV.FASK_Vyroba_PVP.Where(x =>
                    x.OBJ_NMBR == row.OBJ_NMBR
                    && x.OBJ_ORD == row.OBJ_ORD
                    );

                    foreach (var radek in whereDT)
                    {
                        ODZNAC_Logika(radek);

                    }
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void ODZNAC_Logika(Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVPRow row)
        {
            try
            {
                if (!row.IsRef_PVHNull())
                    return;

                
                if (!row.VP_PRPS)
                    return;


                bool state;

                if ((providerPV != null) && (providerPV is Fask.Interfaces.Vyroba.PV.IPV_Edit_QTY_PV))
                    state = ((Fask.Interfaces.Vyroba.PV.IPV_Edit_QTY_PV)providerPV).Edit_QTY(row, null, false, string.Empty);
                else
                    throw new NotImplementedException("Provider neimplementuje IPV_Edit_QTY.");


                if (!state)
                {
                    Fask.Logging.ExceptionHandler2.Handle(LogLevel.Error, string.Format("řádek '{0}'. Nezeditovan...", row.DEX_ROW_ID));
                }

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        #endregion

        #endregion

        #region Zaplanuj VYDEJ

        /// <summary>
        /// Plnanovaní do výdeje
        /// </summary>
        private void Perform_Zaplanuj_DoVydeje()
        {
            try
            {
                int count = 0;

                Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVPDataTable tmp_dtPVP = new Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVPDataTable();

                foreach (DataGridViewRow item in this.dg_PV.SelectedRows)
                {
                    var row = GetSelectedRow(item);

                    var state = VYDEJ_TRIDENI_Logika(row, count);

                    if (state < 0)
                    {
                        count++;
                        continue;
                    }

                    tmp_dtPVP.ImportRow(row);
                    count++;
                }

                VYDEJ_ZAPLANUJ_Logika(tmp_dtPVP);

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Plnanovaní do výdeje
        /// </summary>
        private void Perform_Zaplanuj_DoVydeje_Hlavicky()
        {
            try
            {
                int count = 0;

                Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVPDataTable tmp_dtPVP = new Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVPDataTable();

                foreach (DataGridViewRow item in this.dg_PV_H.SelectedRows)
                {
                    var row = GetSelectedRowHlavicky(item);

                    var whereDT = ds_PV.FASK_Vyroba_PVP.Where(x => x.OBJ_NMBR == row.OBJ_NMBR && x.OBJ_ORD == row.OBJ_ORD );

                    foreach (var radek in whereDT)
                    {
                        var state = VYDEJ_TRIDENI_Logika(radek, count);

                        if (state < 0)
                            continue;

                        tmp_dtPVP.ImportRow(radek);
                        count++;
                    }
                }

                VYDEJ_ZAPLANUJ_Logika(tmp_dtPVP);

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int VYDEJ_TRIDENI_Logika(Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVPRow row, int count)
        {
            try
            {
                if (!row.IsVP_PRDCT_QTYNull()) // pokud už je zaplanovana tak nelze zaplanovat
                {
                    this.dg_PV.SelectedRows[count].Selected = false;
                    return -1;
                }
                else if (row.IsVP_PRPS_QTYNull() || row.VP_PRPS_QTY == 0)
                {
                    this.dg_PV.SelectedRows[count].Selected = false;

                    MessageBox.Show(string.Format("Položka {0}({1}). Neobsahuje množství pro zavedení do výdeje." + Environment.NewLine + " Položka bude přeskočena!", row.ITEMDESC, row.ITEMNMBR), this.Text, MessageBoxButtons.OK);
                    return -1;

                }
                else if (!row.VP_PRPS)
                {
                    this.dg_PV.SelectedRows[count].Selected = false;

                    MessageBox.Show(string.Format("Položka {0}({1}). Neni označena pro zavedení do výdeje." + Environment.NewLine + " Položka bude přeskočena!", row.ITEMDESC, row.ITEMNMBR), this.Text, MessageBoxButtons.OK);
                    return -1;
                }

                #region Kontrola Polozky

                var Filtr = new Fask.Interfaces.Filtry.ZboziListFiltr()
                {
                    MaterialID = row.ITEMNMBR
                };

                Fask.Interfaces.DataSets.Zbozi dsZbozitmp = null;

                if ((providerZbozi != null) && (providerZbozi is Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_GetFiltrovaneZbozi))
                    dsZbozitmp = ((Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_GetFiltrovaneZbozi)providerZbozi).GetFiltrovaneZbozi(Filtr);
                else
                    throw new NotImplementedException("Provider IZbozi2_GetFiltrovaneZbozi.");

                if (dsZbozitmp == null)
                {
                    MessageBox.Show("Nenalezen čísleník zásob. " + Environment.NewLine + " Položka bude přeskočena!", this.Text, MessageBoxButtons.OK);
                    this.dg_PV.SelectedRows[count].Selected = false;
                    return -1;
                }
                else if (dsZbozitmp.FASK_ZASOBY_ALL_KONZOLA.Count == 0)
                {
                    MessageBox.Show(string.Format("Položka {0}({1}). Nenalezena v číselniku zásob." + Environment.NewLine + " Položka bude přeskočena!", row.ITEMDESC, row.ITEMNMBR), this.Text, MessageBoxButtons.OK);
                    this.dg_PV.SelectedRows[count].Selected = false;
                    return -1;
                }
                else if (dsZbozitmp.FASK_ZASOBY_ALL_KONZOLA.Count > 1)
                {
                    MessageBox.Show(string.Format("Položka {0}({1}). Nalezena v číselniku zásob víc jak jednou." + Environment.NewLine + " Položka bude přeskočena!", row.ITEMDESC, row.ITEMNMBR), this.Text, MessageBoxButtons.OK);
                    this.dg_PV.SelectedRows[count].Selected = false;
                    return -1;
                }

                #endregion

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }

            return 0;
        }

        private void VYDEJ_ZAPLANUJ_Logika(Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVPDataTable dt)
        {
            try
            {

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Nejsou vybrány záznamy na zavedení", this.Text, MessageBoxButtons.OK);
                    return;
                }

                var GrupData_PVP = dt.GroupBy(x => x.IsVP_PRPS_SOPNUMBENull() ? null : x.VP_PRPS_SOPNUMBE );

                int CountEntries = 0;

                if ((providerVydej != null) && (providerVydej is Fask.Interfaces.Vydej.IVydej2_Get_SE_MaxCountEntries))
                    CountEntries = ((Fask.Interfaces.Vydej.IVydej2_Get_SE_MaxCountEntries)providerVydej).Get_SE_MaxCountEntries();
                else
                    throw new NotImplementedException("Provider IVydej2_Get_SE_MaxCountEntries.");

               

                foreach (var item in GrupData_PVP)
                {
                    CountEntries++;
                    //string SOPNUMBE = "Plan" + CountEntries.ToString("0000000");
                    string SOPNUMBE = CountEntries.ToString();

                    var tmpRow = item.First();


                    bool state;

                    if ((providerPV != null) && (providerPV is Fask.Interfaces.Vyroba.PV.IPV_Insert_PVH))
                        state = ((Fask.Interfaces.Vyroba.PV.IPV_Insert_PVH)providerPV).Insert_PVH(SOPNUMBE, string.Empty, tmpRow.OBJ_DESC, SOPNUMBE, 0, Fask.Interfaces.Classes.ZaplanovanyDoklad.Vydej);
                    else
                        throw new NotImplementedException("Provider IPV_Insert_PVH.");


                    if (!state)
                    {
                        throw new Exception("Příkaz nevložen...");
                    }

                    int? DEXROWID = null;

                    if ((providerPV != null) && (providerPV is Fask.Interfaces.Vyroba.PV.IPV_GetDEX_ROW_ID_from_PVH))
                        DEXROWID = ((Fask.Interfaces.Vyroba.PV.IPV_GetDEX_ROW_ID_from_PVH)providerPV).GetDEX_ROW_ID_from_PVH(SOPNUMBE.Trim(), Fask.Interfaces.Classes.ZaplanovanyDoklad.Vydej);
                    else
                        throw new NotImplementedException("Provider IPV_GetDEX_ROW_ID_from_PVH.");


                    if (DEXROWID == null)
                        throw new Exception("PVH nevytvořena...");



                    foreach (Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVPRow pVPRow in item)
                    {
                        var zapWhere = ds_PV.FASK_Vyroba_PVP.Where(x => x.OBJ_NMBR.Trim() == pVPRow.OBJ_NMBR.Trim() && x.ITEMNMBR.Trim() == pVPRow.ITEMNMBR.Trim() && !x.IsVP_PRDCT_QTYNull());
                        decimal sum = zapWhere.Sum(x => x.VP_PRDCT_QTY);

                        if ((providerPV != null) && (providerPV is Fask.Interfaces.Vyroba.PV.IPV_Zaplanovani_PV))
                            state = ((Fask.Interfaces.Vyroba.PV.IPV_Zaplanovani_PV)providerPV).Zaplanovani_PV(pVPRow, (int)DEXROWID, sum);
                        else
                            throw new NotImplementedException("Provider IPV_Zaplanovani_PV.");


                        if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.Planovani[0].PriZaplanovaniVydej)
                        {

                            var F = new Fask.Interfaces.Filtry.ZboziListFiltr()
                            {
                                MaterialID = pVPRow.ITEMNMBR
                            };

                            Fask.Interfaces.DataSets.Zbozi dsZbozi = null;

                            if ((providerZbozi != null) && (providerZbozi is Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_GetFiltrovaneZbozi))
                                dsZbozi = ((Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_GetFiltrovaneZbozi)providerZbozi).GetFiltrovaneZbozi(F);
                            else
                                throw new NotImplementedException("Provider IZbozi2_GetFiltrovaneZbozi.");

                            if (dsZbozi == null)
                            {
                                throw new Exception("Tabulka Zbozi nevyplnena");
                            }

                            if (dsZbozi.FASK_ZASOBY_ALL_KONZOLA.Count == 0)
                            {
                                throw new Exception("Zboží nenalezeno...");
                            }


                            if (dsZbozi.FASK_ZASOBY_ALL_KONZOLA.Count > 1)
                            {
                                throw new Exception("Nalezeno vic jak jedno zboží");
                            }

                            var ZboziRow = dsZbozi.FASK_ZASOBY_ALL_KONZOLA.First();


                            Fask.Interfaces.DataSets.Vydej.CZMST_SEDataTable dtSE = new Fask.Interfaces.DataSets.Vydej.CZMST_SEDataTable();

                            Fask.Interfaces.DataSets.Vydej.CZMST_SERow SE_rowCreated = dtSE.NewCZMST_SERow();



                            string VNDITNUM = ZboziRow.IsVNDITNUMNull() ? null : ZboziRow.VNDITNUM;
                            string CZ_CarKod = ZboziRow.IsCZ_CarKodNull() ? string.Empty : ZboziRow.CZ_CarKod;

                            string SKL_ID = ZboziRow.IsSKL_IDNull() ? string.Empty : ZboziRow.SKL_ID ;
                            string MJ = ZboziRow.MJ;
                            byte CZ_SerNum_Track = ZboziRow.CZ_SerNum_Track;
                            short CZ_SerNum_Delka = ZboziRow.CZ_SerNum_Delka;

                            string ITEMCODE = ZboziRow.IsITEMCODENull() ? string.Empty : ZboziRow.ITEMCODE;


                            SE_rowCreated.CountEntries = CountEntries;
                            SE_rowCreated.SOPNUMBE = pVPRow.OBJ_NMBR;
                            SE_rowCreated.ITEMNMBR = pVPRow.ITEMNMBR;
                            SE_rowCreated.ITEMTYPE = string.Empty;
                            SE_rowCreated.ITEMDESC = pVPRow.ITEMDESC;
                            SE_rowCreated.VNDDOCNM = string.Empty;

                            if (string.IsNullOrEmpty(VNDITNUM))
                                SE_rowCreated.SetVNDITNUMNull();
                            else
                                SE_rowCreated.VNDITNUM = VNDITNUM; //TaD: Tady je to špatne, pokud je VNDITNUM NULL tak by mnel byi i nadale NULL a ne string.empty

                            SE_rowCreated.ORD = pVPRow.OBJ_ITEM_ORD;
                            SE_rowCreated.CZ_CarKod = CZ_CarKod;
                            SE_rowCreated.SKL_ID = SKL_ID;
                            SE_rowCreated.LOCNCODE = string.Empty;
                            SE_rowCreated.MJ = MJ;
                            SE_rowCreated.QTYSHPPD = pVPRow.VP_PRPS_QTY; //množství ktere se chce zaplanovat do vydeje
                            SE_rowCreated.QTYPACK = 0;
                            SE_rowCreated.CZ_DatVyr_Track = 0;
                            SE_rowCreated.CZ_DatVyr_Delka = 0;
                            SE_rowCreated.CZ_SerNum_Track = CZ_SerNum_Track;
                            SE_rowCreated.CZ_SerNum_Delka = CZ_SerNum_Delka;
                            SE_rowCreated.CZ_SW_Track = 0;
                            SE_rowCreated.CZ_SW_Delka = 0;
                            SE_rowCreated.CZ_Doslo = 0;
                            SE_rowCreated.Note = string.Empty;
                            SE_rowCreated.TYPEPAL = string.Empty;
                            SE_rowCreated.QTYPAL = 0;
                            SE_rowCreated.PRIORITY = 3;
                            SE_rowCreated.PRINTED = 0;

                            SE_rowCreated.USERID = int.Parse(FASK.Logins.Uzivatel.Instance.UserID.Trim());

                            SE_rowCreated.CZ_REZ1_Track = 0;
                            SE_rowCreated.CZ_REZ2_Track = 0;
                            SE_rowCreated.ITEMCODE = ITEMCODE;
                            SE_rowCreated.SetWEIGHTNull();


                            if ((providerVydej != null) && providerVydej is Fask.Interfaces.Vydej.IVydej2_InsertSE)
                            {

                                ((Fask.Interfaces.Vydej.IVydej2_InsertSE)providerVydej).InsertSE(SE_rowCreated);
                            }
                            else
                                throw new Exception("IVydej2_InsertSE not implementet");

                            if (pVPRow.Import_To_SI)
                            {
                                //TODO Vložit Zaznam do SI

                                var dtSI = new Fask.Interfaces.DataSets.Vydej.CZMST_SIDataTable();

                                var SI_rowCreated = dtSI.NewCZMST_SIRow();

                                SI_rowCreated.CountEntries= CountEntries;
                                SI_rowCreated.SOPNUMBE= pVPRow.OBJ_NMBR;
                                SI_rowCreated.ITEMNMBR= pVPRow.ITEMNMBR;
                                SI_rowCreated.ORD= pVPRow.OBJ_ITEM_ORD;
                                SI_rowCreated.VNDDOCNM= string.Empty;

                                if (string.IsNullOrEmpty(VNDITNUM))
                                    SI_rowCreated.SetVNDITNUMNull();
                                else
                                    SI_rowCreated.VNDITNUM = VNDITNUM; //TaD: Tady je to špatne, pokud je VNDITNUM NULL tak by mnel byi i nadale NULL a ne string.empty

                                SI_rowCreated.CZ_CarKod= CZ_CarKod;
                                SI_rowCreated.SKL_ID= SKL_ID;
                                SI_rowCreated.LOCNCODE= string.Empty;
                                SI_rowCreated.MJ= MJ;
                                SI_rowCreated.QTYSHPPD= pVPRow.VP_PRPS_QTY;
                                SI_rowCreated.QTYPACK= 0;
                                SI_rowCreated.QTYSHPPDMJ= pVPRow.VP_PRPS_QTY;
                                SI_rowCreated.SERLTNUM= string.Empty;
                                SI_rowCreated.KOD_SW= string.Empty;
                                SI_rowCreated.DAT_VYROBY= string.Empty;
                                SI_rowCreated.REZ_1= string.Empty;
                                SI_rowCreated.REZ_2= string.Empty;
                                SI_rowCreated.ODBER_ID= string.Empty;
                                SI_rowCreated.DATEDONE= DateTime.Now.ToString("yyyyMMdd");
                                SI_rowCreated.TIMEDONE= DateTime.Now.ToString("HHmmss");
                                SI_rowCreated.USER_ID= int.Parse(FASK.Logins.Uzivatel.Instance.UserID.Trim());
                                SI_rowCreated.TYPEPAL= string.Empty;
                                SI_rowCreated.NMBRPAL= string.Empty;
                                SI_rowCreated.PRINTED= 0;
                                SI_rowCreated.GUID= Guid.NewGuid();
                                SI_rowCreated.INPUT_MODE= 0;
                                SI_rowCreated.ID_TERMINAL= 99; // Dle JiS je tohle pouze informační hodnota
                                SI_rowCreated.ITEMCODE= ITEMCODE;
                                SI_rowCreated.SetWEIGHTNull();
                                SI_rowCreated.SetExpiraceNull();


                                if ((providerVydej != null) && providerVydej is Fask.Interfaces.Vydej.IVydej2_InsertSI)
                                {
                                    ((Fask.Interfaces.Vydej.IVydej2_InsertSI)providerVydej).InsertSI(SI_rowCreated);
                                }
                                else
                                    throw new Exception("IVydej2_InsertSE not implementet");

                            }

                        }
                    }
                }

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex,true);

            }
        }

        #endregion

        #region Zaplanuj VYROBA

        /// <summary>
        /// Metoda sloužící pro Zaplanovani do Výroby
        /// -Üdelat graficky alforitmus
        /// -Upravit lepe.... BW?? lepe cey providery???
        /// </summary>
        private void Perform_Zaplanuj_DoVyroby()
        {
            try
            {

                int count = 0;
                List<bool> Upozorneni = new List<bool>();
                Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVPDataTable tmp_dtPVP = new Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVPDataTable();


                foreach (DataGridViewRow item in this.dg_PV.SelectedRows)
                {
                    var row = GetSelectedRow(item);

                    bool upo;

                    var s = VYROBA_TRIDENI_Logika(row, count, out upo);

                    if (s == -1)
                    {
                        count++;
                        continue;
                    }

                    tmp_dtPVP.ImportRow(row);
                    Upozorneni.Add(upo);
                    
                }

                if (VYROBA_STATE_Logika(tmp_dtPVP, Upozorneni))
                    return;


                Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHRow VPHrowCreated;
                int? DEXROWID;

                if (VYROBA_PVH_Logika(out VPHrowCreated, out DEXROWID) < -0)
                    return;

                VYROBA_ZAPLANUJ_Logika(tmp_dtPVP, VPHrowCreated.CountEntries, VPHrowCreated.SOPNUMBE.Trim(), (int)DEXROWID);

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Metoda sloužící pro Zaplanovani do Výroby
        /// -Üdelat graficky alforitmus
        /// -Upravit lepe.... BW?? lepe cey providery???
        /// </summary>
        private void Perform_Zaplanuj_DoVyroby_Hlavicky()
        {
            try
            {

                int count = 0;
                List<bool> Upozorneni = new List<bool>();
                Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVPDataTable tmp_dtPVP = new Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVPDataTable();

                foreach (DataGridViewRow item in this.dg_PV_H.SelectedRows)
                {
                    var row = GetSelectedRowHlavicky(item);

                    var whereDT = ds_PV.FASK_Vyroba_PVP.Where(x => x.OBJ_NMBR == row.OBJ_NMBR && x.OBJ_ORD == row.OBJ_ORD);

                    foreach (var radek in whereDT)
                    {
                        bool upo;
                        var s = VYROBA_TRIDENI_Logika(radek, count, out upo);

                        if (s == -1)
                        {
                            count++;
                            continue;
                        }

                        tmp_dtPVP.ImportRow(radek);
                        Upozorneni.Add(upo);
                    }
                }


                if (VYROBA_STATE_Logika(tmp_dtPVP, Upozorneni))
                    return;


                Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHRow VPHrowCreated;
                int? DEXROWID;

                if (VYROBA_PVH_Logika(out VPHrowCreated, out DEXROWID) < -0)
                    return;

                VYROBA_ZAPLANUJ_Logika(tmp_dtPVP, VPHrowCreated.CountEntries, VPHrowCreated.SOPNUMBE.Trim(), (int)DEXROWID);

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int VYROBA_TRIDENI_Logika(Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVPRow row, int count, out bool upo)
        {
            upo = false;

            try
            {
               
                if (!row.IsVP_PRDCT_QTYNull()) // pokud už je zaplanovana tak nelze zaplanovat
                {
                    this.dg_PV.SelectedRows[count].Selected = false;
                    return -1; 

                }
                else if (row.IsVP_PRPS_QTYNull() || row.VP_PRPS_QTY == 0)
                {
                    this.dg_PV.SelectedRows[count].Selected = false;

                    MessageBox.Show(string.Format("Položka {0}({1}). Neobsahuje množství pro zavedení do výroby." + Environment.NewLine + " Položka bude přeskočena!", row.ITEMDESC, row.ITEMNMBR), this.Text, MessageBoxButtons.OK);
                    return -1;

                }
                else if (!row.VP_PRPS)
                {
                    this.dg_PV.SelectedRows[count].Selected = false;

                    MessageBox.Show(string.Format("Položka {0}({1}). Neni označena pro zavedení do výroby." + Environment.NewLine + " Položka bude přeskočena!", row.ITEMDESC, row.ITEMNMBR), this.Text, MessageBoxButtons.OK);

                    return -1;
                }
                else
                {
                    Fask.Interfaces.DataSets.Vyroba.Pohoda_SKz_VPPRow a = null;

                    if ((providerPV != null) && (providerPV is Fask.Interfaces.Vyroba.PV.IPV_GetSKzInfo_PV))
                        ((Fask.Interfaces.Vyroba.PV.IPV_GetSKzInfo_PV)providerPV).GetSKzInfo_PV(row.ITEMNMBR, out a);
                    else
                        throw new NotImplementedException("Provider IPV_GetSKzInfo_PV.");

                    if (a.IsTIMEMODENull() || a.IsTIMEPREPNull() || a.IsTIMEUNITNull())
                    {
                        upo = true;
                    }
                }

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }

            return 0;
        }

        private bool VYROBA_STATE_Logika(Vyroba_Planovani.FASK_Vyroba_PVPDataTable tmp_dtPVP, List<bool> upozorneni)
        {
            try
            {
                if (this.dg_PV.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Nejsou vybrány záznamy na zavedení", this.Text, MessageBoxButtons.OK);
                    return true;
                }


                if (upozorneni.Contains(true))
                {
                    if (MessageBox.Show("Některé položky nemají nastavený výrobní čas." + Environment.NewLine + "Pokračovat?", this.Text, MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes)
                        return true;
                }

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }

            return false;
        }

        private int VYROBA_PVH_Logika(out Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHRow VPHrowCreated, out int? DEXROWID)
        {
            DEXROWID = null;
            VPHrowCreated = null;
            try
            {
                using (Vyroba.FormVPHEdit frmzb = new Vyroba.FormVPHEdit())
                {
                    frmzb.Text = "Nový výrobní příkaz";
                    frmzb.InsertNewRow = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Planovani[0].PriZaplanovaniVyrobnyPrikaz;
                    frmzb.SlucovatPrikaze = true;
                    if (frmzb.ShowDialog(this) != DialogResult.OK)
                        return -1;

                    VPHrowCreated = frmzb.VPHrowCreated;
                }


                bool state;

                if ((providerPV != null) && (providerPV is Fask.Interfaces.Vyroba.PV.IPV_Insert_PVH))
                    state = ((Fask.Interfaces.Vyroba.PV.IPV_Insert_PVH)providerPV).Insert_PVH(VPHrowCreated.SOPNUMBE, string.Empty, VPHrowCreated.SOPDESC, VPHrowCreated.BarcodeH, VPHrowCreated.Active, Fask.Interfaces.Classes.ZaplanovanyDoklad.Vyroba);
                else
                    throw new NotImplementedException("Provider IPV_Insert_PVH.");



                if (!state)
                {
                    throw new Exception("Příkaz nevložen...");
                }


                if ((providerPV != null) && (providerPV is Fask.Interfaces.Vyroba.PV.IPV_GetDEX_ROW_ID_from_PVH))
                    DEXROWID = ((Fask.Interfaces.Vyroba.PV.IPV_GetDEX_ROW_ID_from_PVH)providerPV).GetDEX_ROW_ID_from_PVH(VPHrowCreated.SOPNUMBE.Trim(), Fask.Interfaces.Classes.ZaplanovanyDoklad.Vyroba);
                else
                    throw new NotImplementedException("Provider IPV_GetDEX_ROW_ID_from_PVH.");

                if (!DEXROWID.HasValue)
                    throw new Exception("PVH nevytvořen!");

            }
            catch (System.Exception ex)
            {
                throw ex;
            }

            return 0;
        }

        private void VYROBA_ZAPLANUJ_Logika(Vyroba_Planovani.FASK_Vyroba_PVPDataTable tmp_dtPVP, int CountEntries, string SOPNUMBE, int DEXROWID)
        {
            try
            {
                foreach (var row in tmp_dtPVP)
                {
                    var zapWhere = ds_PV.FASK_Vyroba_PVP.Where(x => x.OBJ_NMBR.Trim() == row.OBJ_NMBR.Trim() && x.ITEMNMBR.Trim() == row.ITEMNMBR.Trim() && !x.IsVP_PRDCT_QTYNull());
                    decimal sum = zapWhere.Sum(x => x.VP_PRDCT_QTY);

                    bool state;

                    if ((providerPV != null) && (providerPV is Fask.Interfaces.Vyroba.PV.IPV_Zaplanovani_PV))
                        state = ((Fask.Interfaces.Vyroba.PV.IPV_Zaplanovani_PV)providerPV).Zaplanovani_PV(row, DEXROWID, sum);
                    else
                        throw new NotImplementedException("Provider IPV_Zaplanovani_PV.");

                    if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.Planovani[0].PriZaplanovaniVyrobnyPrikaz)
                    {

                        #region Insert do VPP s vazbou na VPH

                        #region Dotaženi z pohody s SKz

                        Fask.Interfaces.DataSets.Vyroba.Pohoda_SKz_VPPRow a = null;


                        if ((providerPV != null) && (providerPV is Fask.Interfaces.Vyroba.PV.IPV_GetSKzInfo_PV))
                            ((Fask.Interfaces.Vyroba.PV.IPV_GetSKzInfo_PV)providerPV).GetSKzInfo_PV(row.ITEMNMBR, out a);
                        else
                            throw new NotImplementedException("Provider IPV_GetSKzInfo_PV.");

                        #endregion

                        Fask.Interfaces.DataSets.Vyroba dataset = new Fask.Interfaces.DataSets.Vyroba();

                        Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPRow newRow = dataset.CZPRO_VPP.NewCZPRO_VPPRow();
                        newRow.CountEntries = CountEntries;
                        newRow.SOPNUMBE = SOPNUMBE;
                        newRow.ITEMNMBR = row.ITEMNMBR.Trim();      // id zboží
                        newRow.ITEMTYPE = string.Empty;
                        newRow.ITEMDESC = row.ITEMDESC.Trim();      // popis zboží
                        newRow.ITEMMJ = a.IsMJNull() ? string.Empty : a.MJ;
                        newRow.VNDDOCNMP = "NULL";
                        newRow.VNDITNUM = a.VNDITNUM;
                        newRow.ORD = 1;
                        newRow.BarcodeP = a.IsBarcodePNull() ? string.Empty : a.BarcodeP;        // čár. kód zboží
                        newRow.LOCNCODE = string.Empty;
                        newRow.QTYSHPPD = row.VP_PRPS_QTY;      // požadované množství
                        newRow.QTYDOKON = 0;
                        newRow.QTYPACK = 0;
                        newRow.QTYPACKMJ = string.Empty;
                        newRow.TIMEMODE = a.IsTIMEMODENull() ? 0 : (a.TIMEMODE - 1);
                        newRow.TIMEPREP = a.IsTIMEPREPNull() ? 0 : (float)a.TIMEPREP;
                        newRow.TIMEUNIT = a.IsTIMEUNITNull() ? 0 : (float)a.TIMEUNIT;
                        newRow.DtProdT = 0;
                        newRow.DtProdL = 0;
                        newRow.SerNumT = 0;
                        newRow.SerNumL = 0;
                        newRow.VerT = 0;
                        newRow.VerL = 0;
                        newRow.TermID = 0;
                        newRow.LSTMod = DateTime.Now;

                        newRow.SetRealization_StartNull();
                        newRow.SetRealization_StopNull();

                        dataset.CZPRO_VPP.AddCZPRO_VPPRow(newRow);

                        if ((providerVPP != null) && providerVPP is Fask.Interfaces.Vyroba.VPP.IVPP_Insert)
                        {
                            ((Fask.Interfaces.Vyroba.VPP.IVPP_Insert)providerVPP).VPP_Insert_Row(newRow);
                        }
                        else
                            throw new Exception("IVPP_Update_Row not implementet");


                        #endregion

                    }

                }
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        }


        #endregion

        #region Navrhar

        private void Perform_Navrh()
        {
            try
            {

                if ((this.dg_PV.SelectedRows == null) || (this.dg_PV.SelectedRows.Count == 0))
                {
                    MessageBox.Show(this, "Nejsou vybrány záznamy na návrh.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    return;
                }


                ProgressIndicatorStart();

                Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_PLANOVANI_PARAMSRow Radek = null;

                using (Planovani_Navrhar.Form_PlanovaniVarianty form = new Planovani_Navrhar.Form_PlanovaniVarianty())
                {
                    DialogResult dr = form.ShowDialog();

                    if (dr != DialogResult.OK)
                    {
                        return;
                    }

                    Radek = form.Radek_Parametry;

                }


                //TODO ... tady bude unejaka logika navrhaře, podle parametru...

                Vyroba_Planovani.FASK_Vyroba_PVPDataTable tmp_loc = new Vyroba_Planovani.FASK_Vyroba_PVPDataTable();
                Vyroba_Planovani.FASK_Vyroba_PVPDataTable tmp_dtPVP = new Vyroba_Planovani.FASK_Vyroba_PVPDataTable();

                try
                {
                    foreach (DataGridViewRow item in this.dg_PV.SelectedRows)
                    {
                        var row = GetSelectedRow(item);
                        tmp_loc.ImportRow(row);
                    }

                    var xxx = tmp_loc.OrderBy(x => x.OBJ_NMBR);

                    foreach (var row in xxx)
                    {
                        tmp_dtPVP.ImportRow(row);
                    }

                }
                catch (Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                #region 17.12.2020 TaD Puvodni logika

                //if (Radek.Disponibilita)
                //{
                //    //Disponibilota...

                //    Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVP_DispoDataTable dtDispo = null;

                //    if ((providerPV != null) && (providerPV is Fask.Interfaces.Vyroba.PV.IPV_Navrh_PARAMS_Disponibilita))
                //        dtDispo = ((Fask.Interfaces.Vyroba.PV.IPV_Navrh_PARAMS_Disponibilita)providerPV).PARAMS_Disponibilita(tmp_dtPVP);
                //    else
                //        throw new NotImplementedException("Provider neimplementuje IPV_Navrh_PARAMS_Disponibilita.");


                //    foreach (Vyroba_Planovani.FASK_Vyroba_PVP_DispoRow pol in dtDispo)
                //    {
                //        if (pol.Flag_Dispo)
                //        {
                //            foreach (DataGridViewRow item in this.dg_PV.SelectedRows)
                //            {

                //                var row = GetSelectedRow(item);

                //                if (row.ITEMNMBR == pol.ITEMNMBR && row.OBJ_NMBR == pol.OBJ_NMBR && row.OBJ_ORD == pol.OBJ_ORD)
                //                {
                //                    if (Radek.Disponibilita_JenPlneVykriteObj)
                //                    {
                //                        bool state = dtDispo.Any(x => x.OBJ_NMBR == row.OBJ_NMBR && x.Flag_Dispo == true);

                //                        if (state)
                //                        {
                //                            OZNAC_Logika(row);
                //                            break;
                //                        }
                //                    }
                //                    else
                //                    {
                //                        OZNAC_Logika(row);
                //                        break;
                //                    }
                //                }
                //            }
                //        }
                //    }

                //} 

                #endregion


                if (Radek.Disponibilita)
                {
                    //Disponibilota...

                    Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVP_DispoDataTable dtDispo = null;

                    if ((providerPV != null) && (providerPV is Fask.Interfaces.Vyroba.PV.IPV_Navrh_PARAMS_Disponibilita))
                        dtDispo = ((Fask.Interfaces.Vyroba.PV.IPV_Navrh_PARAMS_Disponibilita)providerPV).PARAMS_Disponibilita(tmp_dtPVP);
                    else
                        throw new NotImplementedException("Provider neimplementuje IPV_Navrh_PARAMS_Disponibilita.");


                    foreach (Vyroba_Planovani.FASK_Vyroba_PVP_DispoRow pol in dtDispo)
                    {
                        if (pol.Flag_Dispo)
                        {
                            foreach (var row in tmp_dtPVP)
                            {

                                if (row.ITEMNMBR == pol.ITEMNMBR && row.OBJ_NMBR == pol.OBJ_NMBR && row.OBJ_ORD == pol.OBJ_ORD)
                                {
                                    if (Radek.Disponibilita_JenPlneVykriteObj)
                                    {
                                        bool state = dtDispo.Any(x => x.OBJ_NMBR == row.OBJ_NMBR && x.Flag_Dispo == false);

                                        if (!state)
                                        {
                                            OZNAC_Logika_Navrhar(row, false, null);
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        OZNAC_Logika_Navrhar(row, false, null);
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            foreach (var row in tmp_dtPVP)
                            {

                                if (row.ITEMNMBR == pol.ITEMNMBR && row.OBJ_NMBR == pol.OBJ_NMBR && row.OBJ_ORD == pol.OBJ_ORD)
                                {
                                    if (Radek.Disponibilita_JenPlneVykriteObj)
                                    {
                                        bool state = dtDispo.Any(x => x.OBJ_NMBR == row.OBJ_NMBR && x.Flag_Dispo == false);

                                        if (!state)
                                        {
                                            if (pol.STAV_SKLAD_PRED > 0)
                                            {
                                                OZNAC_Logika_Navrhar(row, true, pol.STAV_SKLAD_PRED);
                                            }
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (pol.STAV_SKLAD_PRED > 0)
                                        {
                                            OZNAC_Logika_Navrhar(row, true, pol.STAV_SKLAD_PRED);
                                        }
                                        break;
                                    }
                                }
                            }

                        }
                    }
                }
                else
                {
                    foreach (var row in tmp_dtPVP)
                    {
                        OZNAC_Logika_Navrhar(row, false, null);
                    }
                }

                if (Radek.OnLine_FIFO_OBJ)
                {
                    Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVP_DispoDataTable dtFIFO = null;

                    if ((providerPV != null) && (providerPV is Fask.Interfaces.Vyroba.PV.IPV_Navrh_PARAMS_OnLine_FIFO_OBJ))
                        dtFIFO = ((Fask.Interfaces.Vyroba.PV.IPV_Navrh_PARAMS_OnLine_FIFO_OBJ)providerPV).PARAMS_OnLine_FIFO_OBJ(tmp_dtPVP);
                    else
                        throw new NotImplementedException("Provider neimplementuje IPV_Navrh_PARAMS_OnLine_FIFO_OBJ.");

                    foreach (Vyroba_Planovani.FASK_Vyroba_PVP_DispoRow pol in dtFIFO)
                    {
                        foreach (var row in tmp_dtPVP)
                        {

                            if (row.ITEMNMBR == pol.ITEMNMBR && row.OBJ_NMBR == pol.OBJ_NMBR && row.OBJ_ORD == pol.OBJ_ORD)
                            {
                                bool state = dtFIFO.Any(x => x.OBJ_NMBR == row.OBJ_NMBR && x.Flag_Dispo == false);

                                if (!state)
                                {
                                    OZNAC_Logika_Navrhar(row, false, null);
                                    break;
                                }
                                else
                                {
                                    row.SetVP_PRPS_QTYNull();
                                    row.VP_PRPS = false;
                                    row.SetVP_PRPS_SOPNUMBENull();
                                }
                            }
                        }
                    }
                }

                tmp_dtPVP.ToList().ForEach(x => 
                { 
                    if (!x.VP_PRPS)
                        x.Delete(); 
                });

                tmp_dtPVP.AcceptChanges();


                if (Radek.Slucovani)
                {
                    if (Radek.Slucovani_PoOdberately)
                    {
                        //Sice krasne, ale nefunguje....
                        //tmp_dtPVP.GroupBy(x => x.OBJ_COMPANY)
                        //    .ToList()
                        //    .ForEach(x=> {
                        //        int cnt = 0;
                        //        x.ToList()
                        //        .ForEach(y => y.VP_PRPS_SOPNUMBE = cnt.ToString() );
                        //        cnt++;
                        //    });

                        var GrupComp = tmp_dtPVP.GroupBy(x => x.OBJ_COMPANY);

                        int cnt = 0;
                        foreach (var item in GrupComp)
                        {
                            foreach (var row in item)
                            {
                                row.VP_PRPS_SOPNUMBE = cnt.ToString();
                            }

                            cnt++;
                        }

                    }
                    else if (Radek.Slucovani_PoMnozstvi)
                    {

                        int cnt = 0;
                        foreach (var row in tmp_dtPVP.OrderBy(x => x.DEX_ROW_ID))
                        {

                            decimal Suma = tmp_dtPVP.Where(x => x.VP_PRPS_SOPNUMBE == cnt.ToString()).Sum(x => x.QTY);


                            if (Radek.Slucovani_PoMnozstvi_10)
                            {
                                if (Suma >= 10)
                                    cnt++;

                            }
                            else if (Radek.Slucovani_PoMnozstvi_20)
                            {
                                if (Suma >= 20)
                                    cnt++;
                            }
                            else if (Radek.Slucovani_PoMnozstvi_30)
                            {
                                if (Suma >= 30)
                                    cnt++;
                            }
                            else if (Radek.Slucovani_PoMnozstvi_40)
                            {
                                if (Suma >= 40)
                                    cnt++;
                            }
                            else if (Radek.Slucovani_PoMnozstvi_50)
                            {
                                if (Suma >= 50)
                                    cnt++;
                            }

                            row.VP_PRPS_SOPNUMBE = cnt.ToString();

                        }
                    }
                    else if (Radek.Slucovani_PoMnozstvi_Objednavka)
                    {

                        int cnt = 0;
                        bool first = true;

                        //Setridit podle DEX_ROW_ID nech jsou v spravnem pořadi
                        // Grupnut podle objednavek
                        
                        // Prochazet cyklem objednavky jednu po druhej
                        // zistit 

                        var grup = tmp_dtPVP.OrderBy(x => x.DEX_ROW_ID).GroupBy(x => x.OBJ_NMBR);

                        
                        foreach (System.Linq.IGrouping<string, Vyroba_Planovani.FASK_Vyroba_PVPRow> item in grup)
                        {

                            if(first)
                            {
                                foreach (var pol in tmp_dtPVP)
                                {
                                    if (pol.OBJ_NMBR.Trim() == item.Key)
                                    {
                                        pol.VP_PRPS_SOPNUMBE = cnt.ToString();
                                    }
                                }
                                first = false;
                            }
                            else
                            {
                                decimal SumaDavka = 0;
                                decimal sumObj = 0;

                                var ListMJ = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Planovani_VV_Params[0].Navrhar_MJ_Text.Split(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Planovani_VV_Params[0].Navrhar_MJ_Separator[0]).ToList();


                                foreach (var row in item)
                                {
                                    if (!row.Import_To_SI)
                                    {

                                        if (ListMJ.Contains(row.MJ.Trim()))
                                            sumObj += row.QTY;
                                        else
                                            sumObj += 1;

                                        //if (row.MJ.Trim() != "ks" && row.MJ.Trim() != "pár")
                                        //    sumObj += 1;
                                        //else
                                        //    sumObj += row.QTY;
                                    }
                                }

                                var dtOnlyZaplanovane = tmp_dtPVP.Where(x => x.VP_PRPS_SOPNUMBE == cnt.ToString());

                                foreach (var row in dtOnlyZaplanovane)
                                {

                                    if (!row.Import_To_SI)
                                    {

                                        if (ListMJ.Contains(row.MJ.Trim()))
                                            sumObj += row.QTY;
                                        else
                                            sumObj += 1;


                                        //if (row.MJ.Trim() != "ks" && row.MJ.Trim() != "pár")
                                        //    SumaDavka += 1;
                                        //else
                                        //    SumaDavka += row.QTY;
                                    }

                                }

                                decimal Suma = SumaDavka + sumObj;

                                if (Radek.Slucovani_PoMnozstvi_10)
                                {
                                    if (Suma >= 10)
                                        cnt++;

                                }
                                else if (Radek.Slucovani_PoMnozstvi_20)
                                {
                                    if (Suma >= 20)
                                        cnt++;
                                }
                                else if (Radek.Slucovani_PoMnozstvi_30)
                                {
                                    if (Suma >= 30)
                                        cnt++;
                                }
                                else if (Radek.Slucovani_PoMnozstvi_40)
                                {
                                    if (Suma >= 40)
                                        cnt++;
                                }
                                else if (Radek.Slucovani_PoMnozstvi_50)
                                {
                                    if (Suma >= 50)
                                        cnt++;
                                }

                                foreach (var pol in tmp_dtPVP)
                                {
                                    if (pol.OBJ_NMBR.Trim() == item.Key)
                                    {
                                        pol.VP_PRPS_SOPNUMBE = cnt.ToString();
                                    }
                                }

                            }

                        }

                        #region OLD

                        //foreach (var row in tmp_dtPVP.OrderBy(x => x.DEX_ROW_ID))
                        //{

                        //    decimal Suma = tmp_dtPVP.Where(x => x.VP_PRPS_SOPNUMBE == cnt.ToString()).Sum(x => x.QTY);

                        //    if (Radek.Slucovani_PoMnozstvi_10)
                        //    {
                        //        if (Suma >= 10)
                        //            cnt++;

                        //    }
                        //    else if (Radek.Slucovani_PoMnozstvi_20)
                        //    {
                        //        if (Suma >= 20)
                        //            cnt++;
                        //    }
                        //    else if (Radek.Slucovani_PoMnozstvi_30)
                        //    {
                        //        if (Suma >= 30)
                        //            cnt++;
                        //    }
                        //    else if (Radek.Slucovani_PoMnozstvi_40)
                        //    {
                        //        if (Suma >= 40)
                        //            cnt++;
                        //    }
                        //    else if (Radek.Slucovani_PoMnozstvi_50)
                        //    {
                        //        if (Suma >= 50)
                        //            cnt++;
                        //    }

                        //    row.VP_PRPS_SOPNUMBE = cnt.ToString();

                        //} 

                        #endregion
                    }
                    else
                    {
                        // ?? co se ma dit??
                        // 6.1.2021 Zadani do JaS, defaultně podle objednavek, co objednavka, to jedna dávka
                        var GrupComp = tmp_dtPVP.GroupBy(x => x.OBJ_NMBR);

                        int cnt = 0;
                        foreach (var item in GrupComp)
                        {
                            foreach (var row in item)
                            {
                                row.VP_PRPS_SOPNUMBE = cnt.ToString();
                            }

                            cnt++;
                        }
                    }
                }

                tmp_dtPVP.AcceptChanges();


                if ((providerPV != null) && (providerPV is Fask.Interfaces.Vyroba.PV.IPV_Navrh_UpdateNaplanovane))
                    ((Fask.Interfaces.Vyroba.PV.IPV_Navrh_UpdateNaplanovane)providerPV).UpdateNaplanovane(tmp_dtPVP);
                else
                    throw new NotImplementedException("Provider neimplementuje IPV_Navrh_UpdateNaplanovane.");

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
            finally
            {
                ProgressIndicatorStop();
            }
        }

        private void OZNAC_Logika_Navrhar(
            Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVPRow row, 
            bool dispo,
            decimal? stavsklad)
        {
            try
            {
                if ((row.IsRef_PVHNull()) && (row.VP_PRPS))
                    return;

                var zapWhere = ds_PV.FASK_Vyroba_PVP.Where(x => x.OBJ_NMBR.Trim() == row.OBJ_NMBR.Trim() && x.ITEMNMBR.Trim() == row.ITEMNMBR.Trim() && !x.IsVP_PRDCT_QTYNull());
                decimal sum = zapWhere.Sum(x => x.VP_PRDCT_QTY);


                //bool state;
                decimal vysledek = row.QTY - sum;

                if (!row.IsVP_PRPS_QTYNull())
                {
                    vysledek = row.VP_PRPS_QTY;
                }

                if (dispo && stavsklad.HasValue)
                {
                    vysledek = stavsklad.Value;
                }

                row.VP_PRPS_QTY = vysledek;
                row.VP_PRPS = true;
                row.VP_PRPS_SOPNUMBE = string.Empty;

                //" SET VP_PRPS_QTY = @VP_PRPS_QTY," +
                //" VP_PRPS_SOPNUMBE = @VP_PRPS_SOPNUMBE, " +
                //" VP_PRPS = @VP_PRPS " +
                //" WHERE (DEX_ROW_ID = @ID)";

                //if ((providerPV != null) && (providerPV is Fask.Interfaces.Vyroba.PV.IPV_Edit_QTY_PV))
                //    state = ((Fask.Interfaces.Vyroba.PV.IPV_Edit_QTY_PV)providerPV).Edit_QTY(row, vysledek, true, string.Empty);
                //else
                //    throw new NotImplementedException("Provider neimplementuje IPV_Edit_QTY_PV.");

                //if (!state)
                //{
                //    Fask.Logging.ExceptionHandler2.Handle(LogLevel.Error, string.Format("řádek '{0}'. Nezeditovan...", row.DEX_ROW_ID));
                //}

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }

        }


        private void Perform_Navrh_Hlavicky()
        {
            try
            {
                ProgressIndicatorStart();

                Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_PLANOVANI_PARAMSRow Radek = null;

                using (Planovani_Navrhar.Form_PlanovaniVarianty form = new Planovani_Navrhar.Form_PlanovaniVarianty())
                {
                    DialogResult dr = form.ShowDialog();

                    if (dr != DialogResult.OK)
                    {
                        return;
                    }

                    Radek = form.Radek_Parametry;

                }


                //TODO ... tady bude unejaka logika navrhaře, podle parametru...



                if (Radek.Disponibilita)
                {
                    //Disponibilota...

                    if (Radek.Disponibilita_JenPlneVykriteObj)
                    {


                    }
                }

                //if (xxx)
                //{
                //    //atd/...
                //}

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
            finally
            {
                ProgressIndicatorStop();
            }
        }


        #endregion

        #region ostatné Metody

        /// <summary>
        /// Metoda pro zavřeni Formu
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

        private void Importovat()
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

                            throw new NotImplementedException("Načtení z CSV neimplementováno.");


                            //List<Tuple<string, string, bool>> tableInfo = new List<Tuple<string, string, bool>>();


                            //// nacteni dat v oddelenem vlakne
                            //if ((providerZbozi != null) && (providerZbozi is Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_ImportZbozi))
                            //    tableInfo = ((Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_ImportZbozi)providerZbozi).GetZasobyTableInfo();
                            //else
                            //    throw new NotImplementedException("Provider neimplementuje IZbozi2_ImportZbozi.");


                            ////logika importu zbozi z CSV
                            //string pathToFile = string.Empty;
                            //uspech = ImportZboziZFileCSV(tableInfo);

                            ////DialogResult dr = MessageBox.Show("Obsahuje změny, chcete refresh?", "data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            ////if (dr == System.Windows.Forms.DialogResult.No)
                            ////    return;


                            ////PerformVyhledat();


                        }
                        else if (selectedOption == "SQL_Procedura")
                        {
                            //DataTable dtchanged = this.dsZbozi.FASK_ZASOBY_ALL_KONZOLA.GetChanges();
                            DataTable dtchanged = this.ds_PV.FASK_Vyroba_PVP.GetChanges();
                            if (dtchanged != null && dtchanged.Rows.Count > 0) //obsahuje zmeny ... 
                            {
                                DialogResult dr = MessageBox.Show("Obsahuje změny, pokračovat?", "data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                if (dr == System.Windows.Forms.DialogResult.No)
                                    return;
                            }

                            if (bwImportPlanovaniVyroby.IsBusy)
                            {
                                bwImportPlanovaniVyroby.CancelAsync();
                                while (bwImportPlanovaniVyroby.IsBusy)
                                {
                                    Application.DoEvents();
                                }
                            }

                            ProgressIndicatorStart();

                            int FirstDisplayedScrollingRowIndex = this.dg_PV.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

                            bwImportPlanovaniVyroby.RunWorkerAsync();

                            if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dg_PV.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dg_PV.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index

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

                bool coSeStalo = true;

            }
            catch (Exception ex)
            {
                ProgressIndicatorStop();
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        /// <summary>
        /// Metoda slouži k načtení z objednávek IS do Bufferu FASK_Vyroba_PVP
        /// </summary>
        private void Perform_Zaplanovani()
        {

            try
            {

                ////25.11.2025 MaR zakomentovano na zaklade pozadavku JaS, issue #167 ...
                //if ((providerZbozi != null) && (providerZbozi is Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_GetParametry))
                //{

                //    Fask.Interfaces.Classes.Parametry param = ((Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_GetParametry)providerZbozi).GetParametry();

                //    if (!param.POHODA_E1)
                //    {
                //        MessageBox.Show("'Načtení obchodního požadavku', je možné pouze v POHODE verze E1 která obsahuje volitené parametry.");
                //        return;
                //    }

                //}
                //else
                //    throw new Exception("IParametry2_GetPovolZaporneZasoby not implementet");



                using (FormPlanovaniVyroby_Zaplanovani frm = new FormPlanovaniVyroby_Zaplanovani())
                {
                    frm.ShowDialog(this);
                }


            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        /// <summary>
        /// Metoda slouží k vložení nového záznamu
        /// </summary>
        private void Perform_New()
        {
            try
            {
                using (FormPlanovaniVyrobyEdit frm = new FormPlanovaniVyrobyEdit())
                {
                    //frm.PV_Row = null;
                    frm.ShowDialog();
                }

            }
            catch (Exception ex)
            {

                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }

        }

        /// <summary>
        /// Metoda slouží pro editaci vybraného řádku
        /// </summary>
        private void Perorm_Edit()
        {
            try
            {

                //1- Kontrola zda je vybrán aspon jeden řádek
                //2- Kontrola zda je vybráno víc jak jeden řádek
                //3- Kontrola zda je řádek již zaplánován

                //4-Pokud neni zaplánováno tak umožní editovat


                if (this.dg_PV.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Nejsou vybrány záznamy na editaci", this.Text, MessageBoxButtons.OK);
                    return;
                }


                if (this.dg_PV.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Je vybrán víc jak jeden záznam.", this.Text, MessageBoxButtons.OK);
                    return;
                }


                var row = GetSelectedRow(this.dg_PV.SelectedRows[0]);

                if (row.IsRef_PVHNull())
                {
                    string VP_PRPS_QTY;
                    bool VP_PRPS;
                    string VP_PRPS_SOPNUMBE_local;

                    if (
                        Konzola.Forms.InputBox_Text_And_Checkbox.Show(
                        "Edit",
                        string.Format("Uprav hodnotu : 'Množstí navrh VP'" + Environment.NewLine + "Položka :{0}" + Environment.NewLine + "Kód:{1}" + Environment.NewLine + "Množství:{2}", row.ITEMDESC, row.ITEMCODE, row.QTY),
                        row.IsVP_PRPS_QTYNull() ? string.Empty : row.VP_PRPS_QTY.ToString(),
                        Forms.InputBox_Text_And_Checkbox.TypeOfCode.NumericDecimal,
                        true,
                        0,
                        decimal.MaxValue,
                        true,
                        true,
                        "Návrh VP",
                        out VP_PRPS_QTY,
                        out VP_PRPS,
                        true,
                        "Návrh číslo dávky",
                        row.IsVP_PRPS_SOPNUMBENull() ? string.Empty : row.VP_PRPS_SOPNUMBE,
                        out VP_PRPS_SOPNUMBE_local
                        ) != System.Windows.Forms.DialogResult.OK)
                    {
                        return;
                    }

                    bool state;
                    decimal? dec = null;

                    if (string.IsNullOrEmpty(VP_PRPS_QTY))
                    {
                        VP_PRPS = false;
                    }
                    else 
                    {
                        dec = decimal.Parse(VP_PRPS_QTY);
                    }


                    if ((providerPV != null) && (providerPV is Fask.Interfaces.Vyroba.PV.IPV_Edit_QTY_PV))
                        state = ((Fask.Interfaces.Vyroba.PV.IPV_Edit_QTY_PV)providerPV).Edit_QTY(row, dec, VP_PRPS, VP_PRPS_SOPNUMBE_local);
                    else
                        throw new NotImplementedException("Provider neimplementuje IPV_Edit_QTY_PV.");


                    if (!state)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.Error, string.Format("řádek '{0}'. Nezeditovan...", row.DEX_ROW_ID));
                    }




                }
                else
                {
                    MessageBox.Show(this, "Nelze upravit již zaplánovanou položku.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        /// <summary>
        /// Metoda slouží pro vyhledavani záznamu v DB
        /// </summary>
        private void PerformOK()
        {
            try
            {
                DataTable dtchanged = this.ds_PV.FASK_Vyroba_PVP.GetChanges();
                if (dtchanged != null && dtchanged.Rows.Count > 0) //obsahuje zmeny ... 
                {
                    DialogResult dr = MessageBox.Show("Obsahuje změny, pokračovat?", "data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == System.Windows.Forms.DialogResult.No)
                        return;
                }

                if (bw_PV.IsBusy)
                {
                    bw_PV.CancelAsync();
                    while (bw_PV.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }

                ProgressIndicatorStart();

                Fask.Interfaces.Filtry.Vyroba_PV_Filtr filtr = new Fask.Interfaces.Filtry.Vyroba_PV_Filtr();
                if (!CreateFilter(ref filtr))
                    return;

                int FirstDisplayedScrollingRowIndex = this.dg_PV.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

                //if (SelectedRow != null)
                //    Index = SelectedRow.DEX_ROW_ID;

                RememberItem_PlanovaniVyroby rem = null;

                if (FASK_Vyroba_PVP_selectedRow == null)
                {
                    rem = new RememberItem_PlanovaniVyroby(filtr, null, null);
                }
                else
                {
                    rem = new RememberItem_PlanovaniVyroby(filtr, FASK_Vyroba_PVP_selectedRow.DEX_ROW_ID, null);
                }

                bw_PV.RunWorkerAsync(rem);

                if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dg_PV.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dg_PV.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index
            }
            catch (Exception ex)
            {
                ProgressIndicatorStop();
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //finally
            //{
            //    this.dataGridView1.Focus();
            //}
        }

        private void Perform_Tisk()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (FASK_Vyroba_PVP_selectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                //if (dg_PV.SelectedRows.Count > 1)
                //{
                //    MessageBox.Show("Je vybráno více záznamů než jeden.", this.Text, MessageBoxButtons.OK);
                //    return;
                //}

                Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVPDataTable dt = (Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVPDataTable)ds_PV.FASK_Vyroba_PVP.Copy();

                //dt.Columns.Add("VNDITNUM_IMG", typeof(string));


                //foreach (Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_PVPRow item in dt)
                //{

                //    if (!string.IsNullOrEmpty(item.IsVNDITNUMNull() ? string.Empty : item.VNDITNUM))
                //    {
                //        ZXing.BarcodeWriter zw = new ZXing.BarcodeWriter();
                //        zw.Format = ZXing.BarcodeFormat.CODE_128;
                //        zw.Options.Height = 50;
                //        zw.Options.PureBarcode = true;
                //        System.Drawing.Bitmap image1 = zw.Write(item.VNDITNUM);

                //        System.IO.MemoryStream ms = new System.IO.MemoryStream();
                //        image1.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                //        byte[] imgReportBarcode = ms.ToArray();
                //        ms.Close();

                //        string Base64Imahe = Convert.ToBase64String(imgReportBarcode);

                //       item["VNDITNUM_IMG"] = string.Empty;

                //    }

                //}


                PrintReport(dt);

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void PrintReport(Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVPDataTable dt)
        {
            using (PrintReportLibrary.CoolPrintPreviewDialog plr = new PrintReportLibrary.CoolPrintPreviewDialog())
            {

                plr.Params = new Microsoft.Reporting.WinForms.ReportParameter[]
                {
                    new Microsoft.Reporting.WinForms.ReportParameter("Param_Firma", Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ostatni[0].Vyroba_Param_Firma),
                    new Microsoft.Reporting.WinForms.ReportParameter("Param_Adresa", Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ostatni[0].Vyroba_Param_Adresa),
                    new Microsoft.Reporting.WinForms.ReportParameter("Param_PSC", Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ostatni[0].Vyroba_Param_PSC),
                    new Microsoft.Reporting.WinForms.ReportParameter("Param_Obec", Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ostatni[0].Vyroba_Param_Obec),
    
                    new Microsoft.Reporting.WinForms.ReportParameter("Param_Datum", DateTime.Now.ToShortDateString())
                };

                plr.NazevDataTable = "DataSet";
                plr.DataTable = dt;

                List<PrintReportLibrary.TypeData> tmplist = new List<PrintReportLibrary.TypeData>();
                tmplist.Add(PrintReportLibrary.TypeData.DataTable);
                plr.typedata = new List<PrintReportLibrary.TypeData>(tmplist);
                plr.Projekt = PrintReportLibrary.Projekt.MST;
                plr.ShowPreview = true;

                //TODO ošetreny, zda existuje tiskova sestava
                plr.Path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Servis", "Print", "Templates", Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ostatni[0].PlanovaniVyrobyTiskTemplate);

                PrintReportLibrary.MyPath.PrintTemplateDirectory = Path.Combine(MySystem.MyPath.PrintDirectory);


                plr.CountEntries = "";
                plr.HlavickaKod = "";
                plr.HlavickaKodIMG = "";

                plr.Print(this);

            }
        }

        #endregion

        #region AdvanceDataGridView searchToolBar

        private void advancedDataGridViewSearchToolBar1_Search(object sender, Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs e)
        {
            bool restartsearch = true;
            int startColumn = 0;
            int startRow = 0;
            if (!e.FromBegin)
            {
                bool endcol = dg_PV.CurrentCell.ColumnIndex + 1 >= dg_PV.ColumnCount;
                bool endrow = dg_PV.CurrentCell.RowIndex + 1 >= dg_PV.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dg_PV.CurrentCell.ColumnIndex;
                    startRow = dg_PV.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dg_PV.CurrentCell.ColumnIndex + 1;
                    startRow = dg_PV.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dg_PV.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dg_PV.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dg_PV.CurrentCell = c;
        }

        private void advancedDataGridViewSearchToolBar2_Search(object sender, Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs e)
        {
            bool restartsearch = true;
            int startColumn = 0;
            int startRow = 0;
            if (!e.FromBegin)
            {
                bool endcol = dg_PV_H.CurrentCell.ColumnIndex + 1 >= dg_PV_H.ColumnCount;
                bool endrow = dg_PV_H.CurrentCell.RowIndex + 1 >= dg_PV_H.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dg_PV_H.CurrentCell.ColumnIndex;
                    startRow = dg_PV_H.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dg_PV_H.CurrentCell.ColumnIndex + 1;
                    startRow = dg_PV_H.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dg_PV_H.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dg_PV_H.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dg_PV_H.CurrentCell = c;
        }


        #endregion

        #region Zmena TabControl indexu, a podle toho uprava povoleni/zakazani tlačitek

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                TabPage tab = tabControl1.SelectedTab;

                if (tab.Name == tabPage1.Name) // přehled
                {
                    //panelButtons.Controls[tsmiTisk.Name].Enabled = true;
                    //panelButtons.Controls[tsmiExportDoCSVVse.Name].Enabled = true;
                    //panelButtons.Controls[tsmiExportDoCSVOznacene.Name].Enabled = true;
                    //panelButtons.Controls[tsmiExportDoExcelVse.Name].Enabled = true;
                    //panelButtons.Controls[tsmiExportDoExcelOznacene.Name].Enabled = true;
                    //panelButtons.Controls[tsmiExportDoXMLVse.Name].Enabled = true;
                    //panelButtons.Controls[tsmiExportDoXMLOznacene.Name].Enabled = true;

                    //panelButtons.Controls[tsmiZaplanovani_Vyroby.Name].Enabled = true;
                    //panelButtons.Controls[tsmiZaplanovani_Vydeje.Name].Enabled = true;

                    //panelButtons.Controls[tsmiOdznacKZaplanovani.Name].Enabled = true;
                    //panelButtons.Controls[tsmiOznacKZaplanovani.Name].Enabled = true;

                    panelButtons.Controls[tsmiNavrh.Name].Enabled = true;
                    tsmiNavrh.Enabled = true;

                    //panelButtons.Controls[tsmiNacteniObchodnihoPozadavku.Name].Enabled = true;


                    //panelButtons.Controls[tsmiOdstranit.Name].Enabled = true;
                    //tsmiOdstranit.Enabled = true;

                    panelButtons.Controls[tsmiUpravit.Name].Enabled = true;
                    tsmiUpravit.Enabled = true;

                    panelButtons.Controls[tsmiNovy.Name].Enabled = true;
                    tsmiNovy.Enabled = true;

                }
                else if (tab.Name == tabPage2.Name) // jen součty
                {
                    //panelButtons.Controls[tsmiTisk.Name].Enabled = false;
                    //panelButtons.Controls[tsmiExportDoCSVVse.Name].Enabled = false;
                    //panelButtons.Controls[tsmiExportDoCSVOznacene.Name].Enabled = false;
                    //panelButtons.Controls[tsmiExportDoExcelVse.Name].Enabled = false;
                    //panelButtons.Controls[tsmiExportDoExcelOznacene.Name].Enabled = false;
                    //panelButtons.Controls[tsmiExportDoXMLVse.Name].Enabled = false;
                    //panelButtons.Controls[tsmiExportDoXMLOznacene.Name].Enabled = false;

                    //panelButtons.Controls[tsmiZaplanovani_Vyroby.Name].Enabled = false;
                    //panelButtons.Controls[tsmiZaplanovani_Vydeje.Name].Enabled = false;

                    //panelButtons.Controls[tsmiOdznacKZaplanovani.Name].Enabled = false;
                    //panelButtons.Controls[tsmiOznacKZaplanovani.Name].Enabled = false;

                    panelButtons.Controls[tsmiNavrh.Name].Enabled = false;
                    tsmiNavrh.Enabled = false;

                    //panelButtons.Controls[tsmiNacteniObchodnihoPozadavku.Name].Enabled = false;


                    //panelButtons.Controls[tsmiOdstranit.Name].Enabled = false;
                    //tsmiOdstranit.Enabled = false;

                    panelButtons.Controls[tsmiUpravit.Name].Enabled = false;
                    tsmiUpravit.Enabled = false;

                    panelButtons.Controls[tsmiNovy.Name].Enabled = false;
                    tsmiNovy.Enabled = false;

                }
                else
                {
                    //Neznamy...
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        #endregion

        private void cb_Zaplanovano_TimeVariant_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is ComboBox)
            {
                Fask.Interfaces.Classes.TimeVariantName type = (Fask.Interfaces.Classes.TimeVariantName)((ComboBox)sender).SelectedItem;
                var v = type.GetTimeVarianta();

                if (v == Fask.Interfaces.Classes.TimeFilters.TimeVariants.unknow)
                {
                    dtp_Zaplanovano_DO.Enabled =
                    dtp_Zaplanovano_OD.Enabled = true;
                }
                else
                {
                    dtp_Zaplanovano_DO.Enabled =
                    dtp_Zaplanovano_OD.Enabled = false;
                }
            }
        }

        public string GetTimeVariantFromFilter_Zaplanovano()
        {
            Fask.Interfaces.Classes.TimeVariantName type = (Fask.Interfaces.Classes.TimeVariantName)cb_Zaplanovano_TimeVariant.SelectedItem;
            return type.ToString_Filter();
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

                if (FASK_Vyroba_PVP_selectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                #region MaR 6.11.2024 vypis zpl do souboru selected rows
                int index_zaznamu = 0;

                foreach (var item in FASK_Vyroba_PVP_selectedRows)
                {

                    if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start planovani/planovani vyroby TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");

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

                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END planovani/planovani vyroby TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");


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



                    foreach (var item in FASK_Vyroba_PVP_selectedRows)
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
                    Tisk_selected_rows_Primy_Tisk(plr_Path, true, MN_ToTisk, FASK_Vyroba_PVP_selectedRows, NazevVychoziTiskarny);

                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Tisk_selected_rows_Primy_Tisk(string plr_Path, bool MnozstvuAutoJedna, int MN_ToTisk, List<Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVPRow> FASK_ZASOBY_selected_Rows, string nazevVychTiskarny)
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

                if (FASK_Vyroba_PVP_selectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                //if (FASK_Vyroba_PVP_selectedRows.Count > 1)
                //{
                //    //MessageBox.Show("Je vybráno více záznamů než jeden.", this.Text, MessageBoxButtons.OK);
                //    //return;
                //}

                Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVPDataTable dt = new Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVPDataTable();

                foreach (Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVPRow row in FASK_Vyroba_PVP_selectedRows)
                {
                    try
                    {
                        Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVPRow newRow = dt.NewFASK_Vyroba_PVPRow();
                        // Ošetřit delší vstupní pole než počet sloupců tabulky
                        int copyLength = Math.Min(row.ItemArray.Length, dt.Columns.Count);
                        newRow.ItemArray = row.ItemArray.Take(copyLength).ToArray();
                        dt.AddFASK_Vyroba_PVPRow(newRow);
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

        private void PrintReport_RDLC(Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVPDataTable dt, bool primyTisk, string path_sablona, string nazevVychTiskarny, int pocetVytisku)
        {
            try
            {

                #region logovani tisku
                if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                {
                    foreach (var item in dt)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start planovani/planovani vyroby TISK rdlc-------------------------------------");

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

                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END planovani/planovani vyroby TISK rdlc-------------------------------------");
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

                if (FASK_Vyroba_PVP_selectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                #region MaR 6.11.2024 vypis zpl do souboru selected rows
                int index_zaznamu = 0;

                foreach (var item in FASK_Vyroba_PVP_selectedRows)
                {

                    if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start planovani/planovani vyroby TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");

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

                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END planovani/planovani vyroby TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");


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



                    foreach (var item in FASK_Vyroba_PVP_selectedRows)
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
                    Tisk_selected_rows(plr_Path, true, MN_ToTisk, FASK_Vyroba_PVP_selectedRows);

                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Tisk_selected_rows(string plr_Path, bool MnozstvuAutoJedna, int MN_ToTisk, List<Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVPRow> FASK_ZASOBY_selected_Rows)
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
            Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVPRow row,
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

                Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVPDataTable dt = new Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVPDataTable();

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

        private Dictionary<string, string> PrepareDataToTisk(Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVPRow productionRow_data)
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

                        Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVPDataTable dt = new Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVPDataTable();


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

       

        private void bwImportPlanovaniVyroby_DoWork(object sender, DoWorkEventArgs e)
        {
            string status;

            try
            {

                if (bwImportPlanovaniVyroby.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }


                // nacteni dat v oddelenem vlakne
                if ((providerPV != null) && (providerPV is Fask.Interfaces.Vyroba.PV.IPV_ImportPV))
                    status = ((Fask.Interfaces.Vyroba.PV.IPV_ImportPV)providerPV).ImportPV();
                else
                    throw new NotImplementedException("Provider neimplementuje IPV_ImportPV.");

                if (status != "OK")
                {
                    //error...

                    Fask.Logging.ExceptionHandler2.Handle(LogLevel.Error, "ImportPV se nepovedl!");
                    MessageBox.Show("Import Plánování výroby se nepovedl!", "Importování Plánování výroby", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (bwImportPlanovaniVyroby.CancellationPending)
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

        private void bwImportPlanovaniVyroby_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
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

        private void importovatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //24.11.2025 MaR issue #167 predelano na import
             Importovat();
            PerformOK();
        }
    }

    /// <summary>
    /// Pomocná třída pro pamatovaní si hodnot
    /// </summary>
    public class RememberItem_PlanovaniVyroby
    {
        public Fask.Interfaces.Filtry.Vyroba_PV_Filtr filtr;
        public int? DEX_ROW_ID;
        public Fask.Interfaces.DataSets.Vyroba_Planovani ds;

        public RememberItem_PlanovaniVyroby(Fask.Interfaces.Filtry.Vyroba_PV_Filtr _filtr, int? _DEX_ROW_ID, Fask.Interfaces.DataSets.Vyroba_Planovani _ds)
        {
            filtr = _filtr;
            DEX_ROW_ID = _DEX_ROW_ID;
            ds = _ds;
        }

    }

}

