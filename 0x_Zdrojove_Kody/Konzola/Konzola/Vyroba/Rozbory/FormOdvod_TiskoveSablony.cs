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
using Fask.Interfaces.Classes;
using System.Text.RegularExpressions;
using Konzola.Tisk;

namespace Konzola.Vyroba.Rozbory
{
    public partial class FormOdvod_TiskoveSablony : Form
    {
        private bool opravneni = false;

        #region Parametry

        protected Fask.Interfaces.IMES provider = null;
        protected Fask.Interfaces.Classes.ZOBRAZENI_TYP _zobrazeni = Fask.Interfaces.Classes.ZOBRAZENI_TYP.UNKNOWN;
        public Fask.Interfaces.Classes.ZOBRAZENI_TYP Zobrazeni
        {
            get
            {
                return _zobrazeni;
            }
            set
            {
                if (_zobrazeni != value)
                {
                    _zobrazeni = value;
                    switch (_zobrazeni)
                    {
                        case Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST:
                            panelButtonsZobrazeniVyber.Hide();
                            tsFiltry.Visible = true;
                            tsFiltry.Enabled = true;
                            break;
                        case Fask.Interfaces.Classes.ZOBRAZENI_TYP.VYBER:
                            panelButtonsZobrazeniVyber.Show();
                            tsFiltry.Visible = false;
                            tsFiltry.Enabled = false;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Kolekce veškerých zvolených sloupců.
        /// </summary>
        public DataGridViewSelectedRowCollection SelectedRows
        {
            get
            {
                try
                {
                    return dg_OdvodTiskoveSablony.SelectedRows;
                }
                catch
                {
                    return null;
                }
            }
        }

        public Fask.Interfaces.DataSets.Vyroba.FASK_FORMULARERow SelectedRow
        {
            get
            {
                try
                {
                    return ((DataRowView)(dg_OdvodTiskoveSablony.BindingContext[bs_OdvodTiskoveSablony].Current)).Row as Fask.Interfaces.DataSets.Vyroba.FASK_FORMULARERow;
                }
                catch
                {
                    return null;
                }
            }
        }

        public string ResultString { get; private set; }

        #region Parametry Filtry


        private string nazev_okna;

        private string typ = string.Empty;
        private BindingList<string> typy = new BindingList<string>();

        /// <summary>
        /// Seznam vsech nactenych filtru.
        /// </summary>
        private List<Fask.Interfaces.Filtry.Odvod_TiskoveSablonyFiltr> filtry = new List<Fask.Interfaces.Filtry.Odvod_TiskoveSablonyFiltr>();

        /// <summary>
        /// Vybrany filtr.
        /// </summary>
        private Fask.Interfaces.Filtry.Odvod_TiskoveSablonyFiltr rowFiltr
        {
            get
            {
                try
                {
                    return tscbFiltry.SelectedItem as Fask.Interfaces.Filtry.Odvod_TiskoveSablonyFiltr;
                }
                catch
                {
                    return null;
                }
            }
        }


        #endregion


        #endregion

        #region eventy Formu

        public FormOdvod_TiskoveSablony() 
        {
            InitializeComponent();

            this.dg_OdvodTiskoveSablony.UpdateColumnHeaderCellsByDatasource();


        

        }

        public FormOdvod_TiskoveSablony(string par_vyhledavani) : this()
        {
            nazev_okna = par_vyhledavani;

        }

        public FormOdvod_TiskoveSablony(bool allowMultiSelect, Fask.Interfaces.Classes.ZOBRAZENI_TYP typZobrazeni) : this()
        {
            
            this.dg_OdvodTiskoveSablony.MultiSelect = allowMultiSelect;
            this.Zobrazeni = typZobrazeni;
            if (Zobrazeni == Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            
        }


        public FormOdvod_TiskoveSablony(string par_vyhledavani, bool opravneni) : this()
        {

            nazev_okna = par_vyhledavani;
            //this.typ = string.Empty;
            this.opravneni = opravneni;

        }

        public FormOdvod_TiskoveSablony(string par_vyhledavani, bool opravneni, string typ) : this()
        {

            nazev_okna = par_vyhledavani;
            this.typ = typ;
            this.opravneni = opravneni;

        }


        private bool typyBox = true;

        public FormOdvod_TiskoveSablony(string par_vyhledavani, bool opravneni, string typ, bool klicTyp) : this()
        {

            nazev_okna = par_vyhledavani;
            this.typ = typ;
            this.opravneni = opravneni;
            typyBox = klicTyp;
          

        }


        private int ord_par = 0;
        private string loginId = string.Empty;


        public FormOdvod_TiskoveSablony(string par_vyhledavani, bool opravneni, string typ, bool klicTyp, string loginId, int ord) : this()
        {

            this.nazev_okna = par_vyhledavani;
            this.typ = typ;
            this.opravneni = opravneni;
            this.typyBox = klicTyp;
            this.ord_par = ord;
            this.loginId = loginId;

    }

        public string Prime_Tisky_Path()
        {
            try
            {
                string path = string.Empty;

                InitProvider();

                Fask.Interfaces.Filtry.Odvod_TiskoveSablonyFiltr filtr = new Fask.Interfaces.Filtry.Odvod_TiskoveSablonyFiltr();
             
                filtr.nazev_okna = nazev_okna;
                filtr.typ = typ;
                filtr.ord = ord_par;
                filtr.loginid = loginId;

                // nacteni dat v oddelenem vlakne
                if ((provider != null) && (provider is Fask.Interfaces.Vyroba.Odvod_TiskoveSablony.IOdvod_TiskoveSablony_GetFiltrovanyTiskoveSablony))
                {
                    path = ((Fask.Interfaces.Vyroba.Odvod_TiskoveSablony.IOdvod_TiskoveSablony_GetFiltrovanyTiskoveSablony)provider).TiskoveSablony_GetFiltrovanyOdvodTiskoveSablony_Path(filtr);
                }
                else
                {
                    throw new NotImplementedException("Provider neobsahuje implemetaci IOdvod_TiskoveSablony_GetFiltrovanyTiskoveSablony");
                }


                return path;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }




        private void FormOdvod_TiskoveSablony_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                //this.WindowState = FormWindowState.Maximized;
                this.dg_OdvodTiskoveSablony.LoadConfiguration(this.GetType().ToString());


                if(opravneni)
                { 
                    btn_editace.Enabled = true;
                    btn_vytvorit.Enabled = true;
                    btn_delete.Enabled = true;
                }
                else
                {
                    btn_editace.Enabled = false;
                    btn_vytvorit.Enabled = false;
                    btn_delete.Enabled = false;
                }

                //if (Zobrazeni == Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST)
                //{
                //    panelButtonsZobrazeniList.LoadConfiguration(this.GetType().ToString());
                //    panelButtonsZobrazeniList.Init();
                //}

                //advancedDataGridViewSearchToolBar1.SetColumns(dg_OdvodMachineStateSet.Columns);

                //// načtení konfigurace vytvořených filtrů
                this.filtry = Extensions.ListExt.ReadFromXML<Fask.Interfaces.Filtry.Odvod_TiskoveSablonyFiltr>(this.GetType().ToString() + ".filtr");
                tscbFiltry.ComboBox.DataSource = this.filtry;
                tscbFiltry.SelectedItem = null;
                tscbFiltry.ComboBox.DropDownWitdhAutosize();

                DateTime today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);

              
                InitProvider();

               // tB_typ.Text = typ;

                //naplneni comboBoxu tap



                typRoletka();


                //cB_typ.SelectedItem = typ;
                if (cB_typ.Items.Contains(typ))
                {
                    cB_typ.SelectedItem = typ;
                }
                else
                {
                    cB_typ.SelectedItem = string.Empty;
                }
                //SetStatusLabelText_Events(-1);

                PerformVyhledat();
                //buttonVyhledat.Focus();

                this.cB_typ.Enabled = typyBox;





            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void typRoletka()
        {
            try
            {
                Fask.Interfaces.Filtry.Odvod_TiskoveSablonyFiltr filtr = new Fask.Interfaces.Filtry.Odvod_TiskoveSablonyFiltr();
                Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();
               

                typy.Clear();
                filtr.nazev_okna = nazev_okna;
                // nacteni dat v oddelenem vlakne
                if ((provider != null) && (provider is Fask.Interfaces.Vyroba.Odvod_TiskoveSablony.IOdvod_TiskoveSablony_GetFiltrovanyTiskoveSablony))
                {
                    ds = ((Fask.Interfaces.Vyroba.Odvod_TiskoveSablony.IOdvod_TiskoveSablony_GetFiltrovanyTiskoveSablony)provider).TiskoveSablony_GetFiltrovanyOdvodTiskoveSablony(filtr);
                }
                else
                {
                    throw new NotImplementedException("Provider neobsahuje implemetaci IOdvod_TiskoveSablony_GetFiltrovanyTiskoveSablony");
                }


                //zkouska

                //BindingList<string> typy = new BindingList<string>();

                //// Přidejte prázdný výběr jako první hodnotu.
                typy.Add(string.Empty);

                // 1. Načtěte všechny hodnoty ze sloupce typ do seznamu.
                var typyZeSloupce = ds.FASK_FORMULARE
                    .Where(row => row["typ"] != DBNull.Value)
                    .Select(row => row["typ"].ToString())
                    .Distinct()
                    .ToList();

                typyZeSloupce.Sort();
                typyZeSloupce.Remove(string.Empty);
                // Přidejte načtené typy do BindingList.
                foreach (var typ in typyZeSloupce)
                {
                    typy.Add(typ);
                }

                
                cB_typ.DataSource = typy;
               

                // Nastavení výchozího výběru na prázdný výběr.
                //cB_typ.SelectedItem = string.Empty;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // 2. Vytvoření enumu z jedinečných hodnot.
        public enum Typy
        {
            None, // Volitelná hodnota pro případ, kdy žádný typ neodpovídá
        }


        private void FormOdvod_TiskoveSablony_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Shift && !e.Control && !e.Alt)
            {
                if (e.KeyCode == Keys.Escape)
                {
                    PerformCancel();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    PerformVyhledat();
                }
                else
                    return;
            }
            else
                return;

            e.Handled = true;
        }


        private void FormOdvod_TiskoveSablony_Shown(object sender, EventArgs e)
        {
            try
            {
                this.progressIndicator1.Size = new Size(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ostatni[0].ProgressIndicatorSize, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ostatni[0].ProgressIndicatorSize);
                this.progressIndicator1.Location = new Point(this.dg_OdvodTiskoveSablony.Location.X + (this.dg_OdvodTiskoveSablony.Width / 2) - (progressIndicator1.Size.Width / 2), this.dg_OdvodTiskoveSablony.Location.Y + (this.dg_OdvodTiskoveSablony.Height / 2) - (progressIndicator1.Size.Height / 2));
            }
            catch { }
        }


        #endregion

        //public Fask.Interfaces.Classes.TimeFilters.TimeVariants GetTimeVariantFromFilter_porizeno()
        //{
        //    Fask.Interfaces.Classes.TimeVariantName type = (Fask.Interfaces.Classes.TimeVariantName)cb_Porizeno_TimeVariant.SelectedItem;
        //    return type.GetTimeVarianta();
        //}

        //public Fask.Interfaces.Classes.TimeFilters.TimeVariants GetTimeVariantFromFilter()
        //{
        //    Fask.Interfaces.Classes.TimeVariantName type = (Fask.Interfaces.Classes.TimeVariantName)cb_TimeVariant.SelectedItem;
        //    return type.GetTimeVarianta(); 
        //}

    

    

        #region Eventy

        private void buttonNovy_Click(object sender, EventArgs e)
        {
            PerformOK();
        }


        private void konecToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void FormUzivateleList_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
              
                this.dg_OdvodTiskoveSablony.SaveConfiguration(this.GetType().ToString());

                this.filtry.WriteXML(this.GetType().ToString() + ".filtr");

                WaithToEndThread();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                    PerformOK();
            }
            catch
            {
            }
        }

        private void buttonVyhledat_Click(object sender, EventArgs e)
        {
            PerformVyhledat();
        }

  

        #region Filtre




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

        #endregion

        #region Protected metody

        /// <summary>
        /// Inicializace providera
        /// </summary>
        public void InitProvider()
        {
            try
            {
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
                                
                                if (typeof(Fask.Interfaces.Vyroba.Odvod_TiskoveSablony.IOdvod_TiskoveSablony).IsAssignableFrom(t))
                                {
                                    provider = (Fask.Interfaces.Vyroba.Odvod_TiskoveSablony.IOdvod_TiskoveSablony)providerAssemlby.CreateInstance(t.FullName);
                                    if (provider != null)
                                        break;
                                }
                            }
                            catch { }
                        }
                        //return config;
                    }

                    provider.InitProvider();

                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// MEtoda pro Vybrat material
        /// </summary>
        public virtual void PerformOK() { }

        /// <summary>
        /// Metoda na čekani dobehnuti vlakna
        /// </summary>
        public virtual void WaithToEndThread()
        {
            //// cekani na dobehnuti vlakna
            try
            {
                if (bw_OdvodTiskoveSablony.IsBusy)
                {
                    bw_OdvodTiskoveSablony.CancelAsync();
                    while (bw_OdvodTiskoveSablony.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }



        /// <summary>
        /// Vyhledani podle zadaneho filtru.
        /// </summary>
        public void PerformVyhledatNew()
        {


            try
            {


                DataTable dtchanged = this.ds_OdvodTiskoveSablony.FASK_FORMULARE.GetChanges();
                if (dtchanged != null && dtchanged.Rows.Count > 0) //obsahuje zmeny ... 
                {
                    DialogResult dr = MessageBox.Show("Obsahuje změny, pokračovat?", "data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == System.Windows.Forms.DialogResult.No)
                        return;
                    typ = string.Empty;
                    typRoletka();
                    cB_typ.SelectedItem = string.Empty;
                    //typ = cB_typ.SelectedItem.ToString();
                }

                if (bw_OdvodTiskoveSablony.IsBusy)
                {
                    bw_OdvodTiskoveSablony.CancelAsync();
                    while (bw_OdvodTiskoveSablony.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }

                ProgressIndicatorStart();

                Fask.Interfaces.Filtry.Odvod_TiskoveSablonyFiltr filtr = new Fask.Interfaces.Filtry.Odvod_TiskoveSablonyFiltr();
                if (!CreateFilter(ref filtr))
                {
                    ProgressIndicatorStop();
                    return;
                }

                int FirstDisplayedScrollingRowIndex = this.dg_OdvodTiskoveSablony.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

                filtr.nazev_okna = nazev_okna;
                //filtr.typ = tB_typ.Text;

                try
                {

                    //MaR zmena 8.11.2024 pretipovani roletky
                    //filtr.typ = cB_typ.SelectedItem.ToString();

                    filtr.typ = typ;
                }
                catch (Exception)
                {

                    filtr.typ = string.Empty;
                    typ = string.Empty;
                    typRoletka();
                    cB_typ.SelectedItem = string.Empty;
                }
                bw_OdvodTiskoveSablony.RunWorkerAsync(filtr);

                if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dg_OdvodTiskoveSablony.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dg_OdvodTiskoveSablony.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index
            }
            catch (Exception ex)
            {
                ProgressIndicatorStop();
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }





        /// <summary>
        /// Vyhledani podle zadaneho filtru.
        /// </summary>
        public  void PerformVyhledat() {


            try
            {
                

                DataTable dtchanged = this.ds_OdvodTiskoveSablony.FASK_FORMULARE.GetChanges();
                if (dtchanged != null && dtchanged.Rows.Count > 0) //obsahuje zmeny ... 
                {
                    DialogResult dr = MessageBox.Show("Obsahuje změny, pokračovat?", "data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == System.Windows.Forms.DialogResult.No)
                        return;
                    typ = string.Empty;
                    typRoletka();
                    cB_typ.SelectedItem = string.Empty;
                    //typ = cB_typ.SelectedItem.ToString();
                }

                if (bw_OdvodTiskoveSablony.IsBusy)
                {
                    bw_OdvodTiskoveSablony.CancelAsync();
                    while (bw_OdvodTiskoveSablony.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }
                
                ProgressIndicatorStart();

                Fask.Interfaces.Filtry.Odvod_TiskoveSablonyFiltr filtr = new Fask.Interfaces.Filtry.Odvod_TiskoveSablonyFiltr();
                if (!CreateFilter(ref filtr))
                {
                    ProgressIndicatorStop();
                    return;
                }

                int FirstDisplayedScrollingRowIndex = this.dg_OdvodTiskoveSablony.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

                filtr.nazev_okna = nazev_okna;
                //filtr.typ = tB_typ.Text;

                try
                {

                    //MaR zmena 8.11.2024 pretipovani roletky
                    //filtr.typ = cB_typ.SelectedItem.ToString();

                    filtr.typ = typ;
                }
                catch (Exception)
                {

                    filtr.typ = string.Empty;
                    typ = string.Empty;
                    typRoletka();
                    cB_typ.SelectedItem = string.Empty;
                }
                bw_OdvodTiskoveSablony.RunWorkerAsync(filtr);

                if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dg_OdvodTiskoveSablony.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dg_OdvodTiskoveSablony.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index
            }
            catch (Exception ex)
            {
                ProgressIndicatorStop();
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #region Filtry
        /// <summary>
        /// Odstrani vybrany filtr
        /// </summary>
        public  void PerformOdebratFiltr() 
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

                Fask.Interfaces.Filtry.Odvod_TiskoveSablonyFiltr filtr = new Fask.Interfaces.Filtry.Odvod_TiskoveSablonyFiltr();
                string nazev = string.Empty;
                DialogResult dr = Forms.InputBox.Show("Název filtru", "Zadejte název filtru", string.Empty, false, out nazev);
                if (dr != System.Windows.Forms.DialogResult.OK)
                    return;

                string nameFile = Guid.NewGuid().ToString() + "_" + this.GetType().ToString();
                this.dg_OdvodTiskoveSablony.SaveConfiguration(nameFile);

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

                Fask.Interfaces.Filtry.Odvod_TiskoveSablonyFiltr filtr = rowFiltr;

                string nameFile = string.Empty;

                if (string.IsNullOrEmpty(filtr.NameFileDataGridView))
                {
                   nameFile = Guid.NewGuid().ToString() + "_" + this.GetType().ToString();
                }
                else
                    nameFile = filtr.NameFileDataGridView;

                this.dg_OdvodTiskoveSablony.SaveConfiguration(nameFile);

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
                tB_loginid.Text = string.Empty;
                tb_machineid.Text = string.Empty;

                this.dg_OdvodTiskoveSablony.LoadConfiguration(this.GetType().ToString());

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
        public void PerformNastavitFiltr(Fask.Interfaces.Filtry.Odvod_TiskoveSablonyFiltr filtr) 
        {
            try
            {
                if (filtr == null)
                    return;

                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                PerformVycistitFiltr();

                if (filtr.loginid != null)
                {
                    tB_loginid.Text = filtr.loginid;
                }

                if (filtr.machineid != null)
                {
                    tb_machineid.Text = filtr.machineid;
                }



                if (string.IsNullOrEmpty(filtr.NameFileDataGridView))
                    this.dg_OdvodTiskoveSablony.LoadConfiguration(this.GetType().ToString());
                else
                    this.dg_OdvodTiskoveSablony.LoadConfiguration(filtr.NameFileDataGridView);


            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show("Nepodařilo se načíst data filtru.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Vytvoreni filtru, ktery se pouzije.
        /// </summary>
        /// <param name="filtr"></param>
        /// <returns></returns>
        private bool CreateFilter(ref Fask.Interfaces.Filtry.Odvod_TiskoveSablonyFiltr filtr)
        {
            if (filtr == null)
                filtr = new Fask.Interfaces.Filtry.Odvod_TiskoveSablonyFiltr();

            //if (!string.IsNullOrEmpty(tB_IP_ADAM.Text))
            //{
            //    Regex rx = new Regex("^(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$");
            //    MatchCollection matches = rx.Matches(tB_IP_ADAM.Text);

            //    if (matches.Count > 0)
            //        filtr.nazev_okna = tB_IP_ADAM.Text.Trim();
            //    else
            //    {
            //        MessageBox.Show("Špatně zadaná IP adresa!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return false;
            //    }
            //}
             filtr.loginid = tB_loginid.Text;
            filtr.machineid = tb_machineid.Text;

            return true;
        }


        #endregion


        #endregion

        #region Ostatni metody

        protected void PerformCancel()
        {
            try
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        protected void ProgressIndicatorStop()
        {
            progressIndicator1.Stop();
            progressIndicator1.Visible = false;
        }

        protected void ProgressIndicatorStart()
        {
            try
            {
                // prepocet stredu datagridu
                this.progressIndicator1.Location = new Point(this.dg_OdvodTiskoveSablony.Location.X + (this.dg_OdvodTiskoveSablony.Width / 2) - (progressIndicator1.Size.Width / 2), this.dg_OdvodTiskoveSablony.Location.Y + (this.dg_OdvodTiskoveSablony.Height / 2) - (progressIndicator1.Size.Height / 2));
            }
            catch { }
            progressIndicator1.Start();
            progressIndicator1.Visible = true;
        }

        #endregion



        private void advancedDataGridViewSearchToolBar1_Search(object sender, Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs e)
        {
            bool restartsearch = true;
            int startColumn = 0;
            int startRow = 0;
            if (!e.FromBegin)
            {
                bool endcol = dg_OdvodTiskoveSablony.CurrentCell.ColumnIndex + 1 >= dg_OdvodTiskoveSablony.ColumnCount;
                bool endrow = dg_OdvodTiskoveSablony.CurrentCell.RowIndex + 1 >= dg_OdvodTiskoveSablony.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dg_OdvodTiskoveSablony.CurrentCell.ColumnIndex;
                    startRow = dg_OdvodTiskoveSablony.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dg_OdvodTiskoveSablony.CurrentCell.ColumnIndex + 1;
                    startRow = dg_OdvodTiskoveSablony.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dg_OdvodTiskoveSablony.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dg_OdvodTiskoveSablony.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dg_OdvodTiskoveSablony.CurrentCell = c;
        }

        private void bw_OdvodTiskoveSablony_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Fask.Interfaces.Filtry.Odvod_TiskoveSablonyFiltr filtr = (Fask.Interfaces.Filtry.Odvod_TiskoveSablonyFiltr)e.Argument;
                Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();

                if (bw_OdvodTiskoveSablony.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }


                filtr.nazev_okna = nazev_okna;


                filtr.typ = typ;
                filtr.ord = ord_par;
                filtr.loginid = loginId;

                // nacteni dat v oddelenem vlakne
                if ((provider != null) && (provider is Fask.Interfaces.Vyroba.Odvod_TiskoveSablony.IOdvod_TiskoveSablony_GetFiltrovanyTiskoveSablony))
                {
                    ds = ((Fask.Interfaces.Vyroba.Odvod_TiskoveSablony.IOdvod_TiskoveSablony_GetFiltrovanyTiskoveSablony)provider).TiskoveSablony_GetFiltrovanyOdvodTiskoveSablony(filtr);
                }
                else
                {
                    throw new NotImplementedException("Provider neobsahuje implemetaci IOdvod_TiskoveSablony_GetFiltrovanyTiskoveSablony");
                }

                if (bw_OdvodTiskoveSablony.CancellationPending)
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

        private void bw_OdvodTiskoveSablony_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                // check error, check cancel, then use result
                if (e.Error != null)
                {
                    // handle the error
                    ds_OdvodTiskoveSablony = new Fask.Interfaces.DataSets.Vyroba();
                    bs_OdvodTiskoveSablony.DataSource = ds_OdvodTiskoveSablony;
                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, e.Error);
                    MessageBox.Show(e.Error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    SetStatusLabelText_Events(-1);
                }
                else if (e.Cancelled)
                {
                    // handle cancellation
                    // TODO: maji se vymazat data z datasetu?? asi ano ...
                    //if (dsSkladPohyb == null)
                    ds_OdvodTiskoveSablony = new Fask.Interfaces.DataSets.Vyroba();
                    bs_OdvodTiskoveSablony.DataSource = ds_OdvodTiskoveSablony;

                    SetStatusLabelText_Events(-1);
                }
                else
                {
                    // uspesne dokonceno ...
                    // use it on the UI thread
                    ds_OdvodTiskoveSablony = (Fask.Interfaces.DataSets.Vyroba)e.Result;
                    if (ds_OdvodTiskoveSablony == null)
                        ds_OdvodTiskoveSablony = new Fask.Interfaces.DataSets.Vyroba();

                    bs_OdvodTiskoveSablony.DataSource = ds_OdvodTiskoveSablony;


                    //if (ds_OdvodTiskoveSablony.FASK_FORMULARE.Count == 0)
                    //    SetStatusLabelText_Events(-1);
                    //else
                    //{
                    //    foreach (DataGridViewRow row in dg_OdvodTiskoveSablony.SelectedRows)
                    //    {
                    //        SetStatusLabelText_Events(row.Index);
                    //    }
                    //}
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

      
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb != null)
            {
                if (string.IsNullOrEmpty(tb.Text))
                {
                    tb.BackColor = SystemColors.Window;
                    return;
                }

                try
                {
                   

                    Regex rx = new Regex("^(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$");
                    MatchCollection matches = rx.Matches(tb.Text);

                   if(matches.Count > 0)
                    tb.BackColor = Color.LightGreen;
                   else
                    tb.BackColor = Color.MistyRose;

                }
                catch
                {
                    tb.BackColor = Color.MistyRose;
                }
            }
        }

        private void cb_Porizeno_TimeVariant_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(sender is ComboBox)
            {
                Fask.Interfaces.Classes.TimeVariantName type = (Fask.Interfaces.Classes.TimeVariantName)((ComboBox)sender).SelectedItem;
                var v = type.GetTimeVarianta();

             
            }
        }



        private void chb_Razeni_CheckedChanged(object sender, EventArgs e)
        {
            var x = sender is CheckBox;

            if (x)
            {
                CheckBox ch = sender as CheckBox;

                if (ch.Text == "Vzestupně")
                {
                    ch.Text = "Sestupně";
                }
                else if (ch.Text == "Sestupně")
                {
                    ch.Text = "Vzestupně";
                }
            }
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

            tssl_Eventu_Count.Text = string.Format("{0}/{1}", index + 1, ds_OdvodTiskoveSablony.MachineStateSetHistory.Count);
        }

        private void dg_OdvodTiskoveSablony_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dg_OdvodTiskoveSablony.SelectedRows)
            {
                SetStatusLabelText_Events(row.Index);
            }
        }

        private void CloseForm(DialogResult dialog)
        {
            // Nastavení hodnoty DialogResult na OK
            this.DialogResult = dialog;

            // Uzavření formuláře
            this.Close();
        }

        private void btn_zrusit_Click(object sender, EventArgs e)
        {
            CloseForm(DialogResult.Cancel);
        }

        private void btn_vyber_Click(object sender, EventArgs e)
        {
            if(SelectedRow == null)
            {
                MessageBox.Show("Není vybrán záznam!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (SelectedRows.Count > 1)
            {
                MessageBox.Show("Je vybráno více než jeden záznam!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            if (SelectedRow.IsformularNull())
            {
                ResultString = string.Empty;
            }
            else
            {

                ResultString = SelectedRow.formular;
            }


            CloseForm(DialogResult.OK);


        }

        private void btn_editace_Click(object sender, EventArgs e)
        {
            try
            {
                //MessageBox.Show("Bude implementováno...", this.Text, MessageBoxButtons.OK);
                //return;

                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (this.dg_OdvodTiskoveSablony.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Je možné upravovat pouze po jednom záznamu", this.Text, MessageBoxButtons.OK);
                    return;
                }


                if (SelectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro úpravu", this.Text, MessageBoxButtons.OK);
                    return;
                }

                using (FormTiskovaSablonaEdit frmuziv = new FormTiskovaSablonaEdit())
                {
                    frmuziv.fASK_FORMULARERow = SelectedRow;
                    frmuziv.Text = "Úprava tisku: " + SelectedRow.nazev;
                    if (frmuziv.ShowDialog(this) != DialogResult.OK)
                        return;

                }

                PerformRoletkaInit();
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

        private void btn_vytvorit_Click(object sender, EventArgs e)
        {
            try
            {
                //MessageBox.Show("Bude implementováno...", this.Text, MessageBoxButtons.OK);
                //return;

                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();
                Fask.Interfaces.DataSets.Vyroba.FASK_FORMULARERow row = ds.FASK_FORMULARE.NewFASK_FORMULARERow();
                row.nazev_okna = nazev_okna;
                row.ord = 0;

                using (FormTiskovaSablonaEdit frmuziv = new FormTiskovaSablonaEdit(true))
                {
                    frmuziv.fASK_FORMULARERow = row;
                    frmuziv.Text = "Vytvoření tisku ";
                    if (frmuziv.ShowDialog(this) != DialogResult.OK)
                        return;

                }

                PerformRoletkaInit();
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

        private void PerformRoletkaInit()
        {
            typ = string.Empty;
            typRoletka();
            cB_typ.SelectedItem = string.Empty;
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            int pocetZaznamu = 0;
            try
            {
                //MessageBox.Show("Bude implementováno...", this.Text, MessageBoxButtons.OK);
                //return;
                //InitProvider();

                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (this.dg_OdvodTiskoveSablony.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Je možné odtranit pouze po jednom záznamu", this.Text, MessageBoxButtons.OK);
                    return;
                }


                if (SelectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro odtranění", this.Text, MessageBoxButtons.OK);
                    return;
                }

               
                  DialogResult dialog =  MessageBox.Show("Opravdu si přejete odstranit záznam?", this.Text, MessageBoxButtons.YesNo);
                
                if(dialog != DialogResult.Yes)
                {
                    return;
                }

               
                if ((provider != null) && (provider is Fask.Interfaces.Tisky.ITisk_TiskovaSablona))
                {
                        pocetZaznamu = ((Fask.Interfaces.Tisky.ITisk_TiskovaSablona)provider).TiskovaSablonaDelete_DB(SelectedRow);
                  
                }
                else
                    throw new Exception("TiskovaSablonaDelete_DB not implementet");


                if (pocetZaznamu == 0)
                {
                    MessageBox.Show("Záznam nebyl smazán.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (pocetZaznamu < 0)
                {
                    MessageBox.Show("Záznam nebyl smazán. Chyba na straně komunikačního serveru.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                PerformRoletkaInit();
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
    }
}
