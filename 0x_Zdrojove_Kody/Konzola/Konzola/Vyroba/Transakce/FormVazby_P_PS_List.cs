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

namespace Konzola.Vyroba
{
    public enum TypZpravovaniVyroby
    {
        Vazba_P_PS, // vazba kde se zpracováva ProductionSources jak materialy(vydejka), a Production jak vyrobky(Prijemka)
        Vazba_P_TP,  // vazba kde se zpracováva FASK_Vyroba_TP jak definice pro materialy(vydejka), a Production jak vyrobky(Prijemka)
        Vazba_P_Vyroba,  // vazba kde se zpracováva v IS pohoda vezmnou definice pro materialy(vydejka), a Production jak vyrobky(Vyroba v IS POHODA)
        Vazba_P // Import pouze přijemek z tabulky Production
    }

    public partial class FormVazby_P_PS_List : Form
    {

        public static Konzola.Vyroba.TypZpravovaniVyroby VyrobaOdvodPOHODATyp
        {
            get 
            { 


                return (Konzola.Vyroba.TypZpravovaniVyroby)Enum.Parse(typeof(Konzola.Vyroba.TypZpravovaniVyroby), Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba[0].OdvodPOHODATyp, true); 
            }
            set 
            {
                Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba[0].OdvodPOHODATyp = value.ToString();
                Konfigurace.Globals_Konfig_Konzola.SaveConfiguration();
            }
        }

        #region Private promenne
        /// <summary>
        ///Vybrany řadek Vyrobku
        /// </summary>
        public Fask.Interfaces.DataSets.Vyroba.Production_OdvodRow rowVyrobek
        {
            get
            {
                try
                {
                    return ((DataRowView)(this.dgVyrobky.BindingContext[bs_vyrobky].Current)).Row as Fask.Interfaces.DataSets.Vyroba.Production_OdvodRow;
                }
                catch
                {
                    return null;
                }
            }
        }


        /// <summary>
        /// Seznam vsech nactenych filtru Materialy
        /// </summary>
        private List<Fask.Interfaces.Filtry.Vazby_P_PS_Filtr> filtry_Materialy = new List<Fask.Interfaces.Filtry.Vazby_P_PS_Filtr>();

        /// <summary>
        /// Seznam vsech nactenych filtru Vyrobky
        /// </summary>
        private List<Fask.Interfaces.Filtry.Vazby_P_PS_Filtr> filtry_Vyrobky = new List<Fask.Interfaces.Filtry.Vazby_P_PS_Filtr>();



        /// <summary>
        /// Provider pro vyhledavani filtrama
        /// </summary>
        private Fask.Interfaces.IMES providerP = null;
        private Fask.Interfaces.IMES providerPS = null;
        private Fask.Interfaces.IMES providerVazby = null;


        /// <summary>
        /// Vybrany filtr Materialy
        /// </summary>
        private Fask.Interfaces.Filtry.Vazby_P_PS_Filtr rowFiltr_Materialy
        {
            get
            {
                try
                {
                    return tscbFiltry_Material.SelectedItem as Fask.Interfaces.Filtry.Vazby_P_PS_Filtr;
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
        private Fask.Interfaces.Filtry.Vazby_P_PS_Filtr rowFiltr_Vyrobky
        {
            get
            {
                try
                {
                    return tscbFiltry_Vyrobek.SelectedItem as Fask.Interfaces.Filtry.Vazby_P_PS_Filtr;
                }
                catch
                {
                    return null;
                }
            }
        } 
        #endregion


        #region Eventy formu

        public FormVazby_P_PS_List()
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

        private void FormVazby_P_PS_List_Load(object sender, EventArgs e)
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

                advancedDataGridViewSearchToolBar_Materialy.SetColumns(dgMaterialy.Columns);
                advancedDataGridViewSearchToolBar_Vyrobky.SetColumns(dgVyrobky.Columns);


                //// načtení konfigurace vytvořených filtrů
                this.filtry_Materialy = Extensions.ListExt.ReadFromXML<Fask.Interfaces.Filtry.Vazby_P_PS_Filtr>(this.GetType().ToString() + "_Material" + ".filtr");
                tscbFiltry_Material.ComboBox.DataSource = this.filtry_Materialy;
                tscbFiltry_Material.SelectedItem = null;
                tscbFiltry_Material.ComboBox.DropDownWitdhAutosize();

                //// načtení konfigurace vytvořených filtrů
                this.filtry_Vyrobky = Extensions.ListExt.ReadFromXML<Fask.Interfaces.Filtry.Vazby_P_PS_Filtr>(this.GetType().ToString() + "_Vyrobky" + ".filtr");
                tscbFiltry_Vyrobek.ComboBox.DataSource = this.filtry_Vyrobky;
                tscbFiltry_Vyrobek.SelectedItem = null;
                tscbFiltry_Vyrobek.ComboBox.DropDownWitdhAutosize();



                InitProvider();

                if (providerP == null)
                    throw new Exception("Provider 'Vyrobky' není inicializován");

                if (providerPS == null)
                    throw new Exception("Provider 'Materialy' není inicializován");

                if (providerVazby == null)
                    throw new Exception("Provider 'Vazby' není inicializován");


                //UpdateVazbyMaterialyForm();
                //PerformVyhledat_Vyrobek();
                button_Filtr_Vyrobky.Focus();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormVazby_P_PS_List_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Shift && !e.Control && !e.Alt)
            {
                if (e.KeyCode == Keys.Escape)
                {
                    PerformCancel();
                }
                else
                    return;
            }
            else
                return;

            e.Handled = true;
        }

        private void FormVazby_P_PS_List_Shown(object sender, EventArgs e)
        {
            try
            {
                this.progressIndicatorMaterial.Size = new Size(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ostatni[0].ProgressIndicatorSize, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ostatni[0].ProgressIndicatorSize);
                this.progressIndicatorMaterial.Location = new Point(this.dgMaterialy.Location.X + (this.dgMaterialy.Width / 2) - (progressIndicatorMaterial.Size.Width / 2), this.dgMaterialy.Location.Y + (this.dgMaterialy.Height / 2) - (progressIndicatorMaterial.Size.Height / 2));

                this.progressIndicatorVyrobek.Size = new Size(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ostatni[0].ProgressIndicatorSize, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ostatni[0].ProgressIndicatorSize);
                this.progressIndicatorVyrobek.Location = new Point(this.dgVyrobky.Location.X + (this.dgVyrobky.Width / 2) - (progressIndicatorVyrobek.Size.Width / 2), this.dgVyrobky.Location.Y + (this.dgVyrobky.Height / 2) - (progressIndicatorVyrobek.Size.Height / 2));

                splitContainer1.SplitterDistance = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ostatni[0].Vazby_P_PS_SplitPoloha;
            }
            catch { }
        }

        private void FormVazby_P_PS_List_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.dgVyrobky.SaveConfiguration(this.GetType().ToString() + "Vyrobky");
                this.dgMaterialy.SaveConfiguration(this.GetType().ToString() + "Materialy");

                panelButtons.SaveConfiguration(this.GetType().ToString());

                // ulozeni vsech filtru
                this.filtry_Materialy.WriteXML(this.GetType().ToString() + "_Material" + ".filtr");
                this.filtry_Vyrobky.WriteXML(this.GetType().ToString() + "_Vyrobky" + ".filtr");

                Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ostatni[0].Vazby_P_PS_SplitPoloha = splitContainer1.SplitterDistance;
                Konfigurace.Globals_Konfig_Konzola.SaveConfiguration();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #endregion

        #region Init provider

        /// <summary>
        /// Inicializace providera Materialy
        /// </summary>
        private void InitProvider()
        {
            #region Prodiction
            try
            {
                if (String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                    return;

                if (providerP == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Vyroba.Production.IProduction).IsAssignableFrom(t))
                            {
                                providerP = (Fask.Interfaces.Vyroba.Production.IProduction)providerAssemlby.CreateInstance(t.FullName);
                                if (providerP != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                providerP.InitProvider();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion

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

            #region Vazby
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
            #endregion

        }


        #endregion

        #region Menu click

        private void konecToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        /// <summary>
        /// Obsluha události kliknutí na položku menu "Zpracovat výrobu".
        /// Tato metoda otevře dialog pro výběr výrobního záznamu a provede import dat.
        /// </summary>
        /// <param name="sender">Odesílatel události.</param>
        /// <param name="e">Argumenty události.</param>
        private void tsmiZpracovatVyrobu_Click(object sender, EventArgs e)
        {
            // Vybraný řádek z tabulky výroby
            Fask.Interfaces.DataSets.Vyroba.ProductionImportRow Row;

            // Zobrazení formuláře pro výběr počtu záznamů k importu
            using (FormProductionList_SelectCountEntries frm = new FormProductionList_SelectCountEntries())
            {
                // Dataset pro výrobní data
                Fask.Interfaces.DataSets.Vyroba ds;

                // Získání výrobních dat pomocí poskytovatele
                if ((providerP != null) && (providerP is Fask.Interfaces.Vyroba.Production.IProduction_GetDataSelectListImport))
                    ds = ((Fask.Interfaces.Vyroba.Production.IProduction_GetDataSelectListImport)providerP).Production_GetDataSelectListImport();
                else
                    throw new Exception("IProduction_GetDataSelectListImport not implementet");

                // Předání datasetu do formuláře
                frm.AVyrobaDataSet = ds;

                // Pokud uživatel zruší výběr, metoda končí
                if (frm.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return;

                // Uložení vybraného řádku
                Row = frm.rowProduction;
            }

            //21.11.2025 MaR novy formular na schvalovani
          bool importPokracuj =  Potvrzeni(Row);



            // Zavolání metody pro import vybraného výrobního řádku
            if (importPokracuj)
            {
                PerformImport(Row, false);
            }
         
        }

        private bool Potvrzeni(Fask.Interfaces.DataSets.Vyroba.ProductionImportRow row)
        {
            bool vysledek = false;

            //Vyrobky
            Fask.Interfaces.Filtry.Vazby_P_PS_Filtr filtr = new Fask.Interfaces.Filtry.Vazby_P_PS_Filtr();
            Fask.Interfaces.DataSets.Vyroba ds_vyrobky = new Fask.Interfaces.DataSets.Vyroba();
            Fask.Interfaces.DataSets.Vyroba ds_materialy = new Fask.Interfaces.DataSets.Vyroba();

            filtr.PotvrzeniPriznak = true;
            filtr.CountEntries = row.IsCountEntriesNull() ? "-1" : row.CountEntries.ToString();
            filtr.SOPNUMBE = row.IsSOPNUMBENull() ? string.Empty : row.SOPNUMBE;
            filtr.SKL_ID = row.IsSKL_IDNull() ? string.Empty : row.SKL_ID;
            

            if (providerP is Fask.Interfaces.Vyroba.Production.IProduction_GetFiltrovanyProductionVazby)
                ds_vyrobky = ((Fask.Interfaces.Vyroba.Production.IProduction_GetFiltrovanyProductionVazby)providerP).Production_GetFiltrovanyProductionVazby(filtr);
            else
                throw new Exception("IProduction_GetFiltrovanyProductionVazby not implementet");

            //Materialy
            //Fask.Interfaces.Filtry.Vazby_P_PS_Filtr filtr = (Fask.Interfaces.Filtry.Vazby_P_PS_Filtr)e.Argument;
            //Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();

            if (providerPS is Fask.Interfaces.Vyroba.ProductionSources.IProductionSources_GetFiltrovanyVazby)
                ds_materialy = ((Fask.Interfaces.Vyroba.ProductionSources.IProductionSources_GetFiltrovanyVazby)providerPS).GetFiltrovanyVazby(filtr);
            else
                throw new Exception("IProductionSources_GetFiltrovanyVazby not implementet");

            //spocitat zaznamy a vyhodnotit
            Fask.Interfaces.DataSets.Vyroba ds_vysledek = new Fask.Interfaces.DataSets.Vyroba();
            if (ds_vyrobky != null)
            {
                foreach (var item in ds_vyrobky.Production_Odvod)
                {
                    ds_vysledek.Production_Odvod.ImportRow(item);
                }
            }

            if (ds_materialy != null)
            {
                foreach (var item in ds_materialy.Production_Sources_Odvod)
                {
                    ds_vysledek.Production_Sources_Odvod.ImportRow(item);
                }
            }

            if (ds_vysledek.Production_Odvod.Count > 0 || ds_vysledek.Production_Sources_Odvod.Count > 0)
            {
                //zobrazeni formulare a odpoved ANO/NE
                // Zobrazení formuláře pro výběr počtu záznamů k importu
                using (FormVazby_P_PS_List_Zpracovat frm = new FormVazby_P_PS_List_Zpracovat())
                {

                    // Předání datasetu do formuláře
                    frm.bs_vyrobky.DataSource = ds_vysledek.Production_Odvod;
                    frm.bs_materialy.DataSource = ds_vysledek.Production_Sources_Odvod;

                    // Pokud uživatel zruší výběr, metoda končí
                    if (frm.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
                    {
                        vysledek = true;
                    }
                    else
                    {
                        vysledek = false;
                    }

                }

            }
            else
            {
                vysledek = true;
            }


            return vysledek;

        }

        private void tsmiupravitVyrobek_Click(object sender, EventArgs e)
        {
            try
            {
                //MessageBox.Show("Bude implementováno...", this.Text, MessageBoxButtons.OK);
                //return;

                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (this.dgVyrobky.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Je možné upravovat pouze po jednom záznamu", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if (rowVyrobek == null)
                {
                    MessageBox.Show("Není vybrán záznam pro úpravu", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if (!rowVyrobek.IsISOKNull())
                {
                    MessageBox.Show("Není možno editovat odvedenou položku do IS.", this.Text, MessageBoxButtons.OK);
                    return;
                }

                switch (FormVazby_P_PS_List.VyrobaOdvodPOHODATyp)
                {
                    case TypZpravovaniVyroby.Vazba_P_PS:
                        {
                            MessageBox.Show("Není možno editovat položku při typu 'Vazba_P_PS'", this.Text, MessageBoxButtons.OK);
                            return;
                        }
                    case TypZpravovaniVyroby.Vazba_P_TP:
                    case TypZpravovaniVyroby.Vazba_P_Vyroba:
                    case TypZpravovaniVyroby.Vazba_P:
                        break;
                }

                #region Transformace celeho Production

                Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable dt = new Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable();
                Fask.Interfaces.DataSets.Vyroba.Production_KonzolaRow productionRow = dt.NewProduction_KonzolaRow();

                #region Tohle je pouze PRODUCTION

                if (rowVyrobek.IsCountEntriesNull())
                    productionRow.SetCountEntriesNull();
                else
                    productionRow.CountEntries = rowVyrobek.CountEntries;


                productionRow.SOPNUMBE = rowVyrobek.IsSOPNUMBENull() ? null : rowVyrobek.SOPNUMBE;
                productionRow.ITEMNMBR = rowVyrobek.IsITEMNMBRNull() ? null : rowVyrobek.ITEMNMBR;
                productionRow.ITEMTYPE = rowVyrobek.IsITEMTYPENull() ? null : rowVyrobek.ITEMTYPE;
                productionRow.ITEMMJ = rowVyrobek.IsITEMMJNull() ? null : rowVyrobek.ITEMMJ;

                if (rowVyrobek.IsTIMEMODENull())
                    productionRow.SetTIMEMODENull();
                else
                    productionRow.TIMEMODE = rowVyrobek.TIMEMODE;

                if (rowVyrobek.IsTIMEPREPSTARTNull())
                    productionRow.SetTIMEPREPSTARTNull();
                else
                    productionRow.TIMEPREPSTART = rowVyrobek.TIMEPREPSTART;

                if (rowVyrobek.IsTIMEPREPSTOPNull())
                    productionRow.SetTIMEPREPSTOPNull();
                else
                    productionRow.TIMEPREPSTOP = rowVyrobek.TIMEPREPSTOP;

                if (rowVyrobek.IsTIMEPREPNull())
                    productionRow.SetTIMEPREPNull();
                else
                    productionRow.TIMEPREP = rowVyrobek.TIMEPREP;

                if (rowVyrobek.IsTIMEUNITNull())
                    productionRow.SetTIMEUNITNull();
                else
                    productionRow.TIMEUNIT = rowVyrobek.TIMEUNIT;

                if (rowVyrobek.IsTIMESTARTNull())
                    productionRow.SetTIMESTARTNull();
                else
                    productionRow.TIMESTART = rowVyrobek.TIMESTART;

                if (rowVyrobek.IsTIMESTOPNull())
                    productionRow.SetTIMESTOPNull();
                else
                    productionRow.TIMESTOP = rowVyrobek.TIMESTOP;

                if (rowVyrobek.IsTIMECORSTARTNull())
                    productionRow.SetTIMECORSTARTNull();
                else

                    productionRow.TIMECORSTART = rowVyrobek.TIMECORSTART;

                if (rowVyrobek.IsTIMECORSTOPNull())
                    productionRow.SetTIMECORSTOPNull();
                else
                    productionRow.TIMECORSTOP = rowVyrobek.TIMECORSTOP;

                if (rowVyrobek.IsTIMECORNull())
                    productionRow.SetTIMECORNull();
                else
                    productionRow.TIMECOR = rowVyrobek.TIMECOR;

                if (rowVyrobek.IsTIMECRIDNull())
                    productionRow.SetTIMECRIDNull();
                else
                    productionRow.TIMECRID = rowVyrobek.TIMECRID;

                if (rowVyrobek.IsTIMECRIDTYPENull())
                    productionRow.SetTIMECRIDTYPENull();
                else
                    productionRow.TIMECRIDTYPE = rowVyrobek.TIMECRIDTYPE;


                productionRow.id = rowVyrobek.id;
                productionRow.loginid = rowVyrobek.loginid;
                productionRow.machineid = rowVyrobek.IsmachineidNull() ? null : rowVyrobek.machineid;
                productionRow.operationid = rowVyrobek.IsoperationidNull() ? null : rowVyrobek.operationid;
                productionRow.dateeve = rowVyrobek.dateeve;
                productionRow.qty = rowVyrobek.qty;
                productionRow.qtyReal = rowVyrobek.qtyReal;

                if (rowVyrobek.IsQTYPACKNull())
                    productionRow.IsQTYPACKNull();
                else
                    productionRow.QTYPACK = rowVyrobek.QTYPACK;

                productionRow.QTYPACKMJ = rowVyrobek.IsQTYPACKMJNull() ? null : rowVyrobek.QTYPACKMJ;
                productionRow.description = rowVyrobek.IsdescriptionNull() ? null : rowVyrobek.description;
                productionRow.BarcodeP = rowVyrobek.IsBarcodePNull() ? null : rowVyrobek.BarcodeP;
                productionRow.UserID = rowVyrobek.UserID;
                productionRow.TermID = rowVyrobek.TermID;

                if (rowVyrobek.IsISOKNull())
                    productionRow.SetISOKNull();
                else
                    productionRow.ISOK = rowVyrobek.ISOK;

                productionRow.GUID = rowVyrobek.GUID;

                if (rowVyrobek.IsSOUBEHGUIDNull())
                    productionRow.SetSOUBEHGUIDNull();
                else
                    productionRow.SOUBEHGUID = rowVyrobek.SOUBEHGUID;

                if (rowVyrobek.IsCORRGUIDNull())
                    productionRow.SetCORRGUIDNull();
                else
                    productionRow.CORRGUID = rowVyrobek.CORRGUID;

                if (rowVyrobek.IsqtyOldNull())
                    productionRow.SetqtyOldNull();
                else
                    productionRow.qtyOld = rowVyrobek.qtyOld;

                productionRow.idVS = rowVyrobek.IsidVSNull() ? null : rowVyrobek.idVS;

                if (rowVyrobek.IsdateeditNull())
                    productionRow.SetdateeditNull();
                else
                    productionRow.dateedit = rowVyrobek.dateedit;

                productionRow.SKL_ID = rowVyrobek.IsSKL_IDNull() ? null : rowVyrobek.SKL_ID;
                productionRow.LOCNCODE = rowVyrobek.IsLOCNCODENull() ? null : rowVyrobek.LOCNCODE;
                productionRow.SERLTNUM = rowVyrobek.IsSERLTNUMNull() ? null : rowVyrobek.SERLTNUM;
                productionRow.EXPIRATION = rowVyrobek.IsEXPIRATIONNull() ? null : rowVyrobek.EXPIRATION;

                #endregion

                productionRow.ITEMDESC = rowVyrobek.IsITEMDESCNull() ? null : rowVyrobek.ITEMDESC;

                #endregion

                using (FormProductionEdit3 frmuziv = new FormProductionEdit3())
                {
                    frmuziv.rowProduct = productionRow;
                    frmuziv.Text = "Úprava výroby";
                    if (frmuziv.ShowDialog(this) != DialogResult.OK)
                        return;

                }
                // opetovne vyhledani zaznamu
                PerformVyhledat_Vyrobek();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void tsmi_ZruseniPriznakuISOK_Click(object sender, EventArgs e)
        {
            try
            {
                //4.8.2020 TaD.
                //Zle zadani od JaS, udelany další kočkopec, kde je možno po odevedeni do IS POHODA zrušít příznak, a proved odvedeni znovu
                //Nekoretní ale takove je zadaní....

                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (this.dgVyrobky.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Je možné zrušit pouze po jednom záznamu", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (rowVyrobek == null)
                {
                    MessageBox.Show("Není vybrán záznam pro zrušení", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (rowVyrobek.IsISOKNull())
                {
                    MessageBox.Show("Není možno zrušit neodvedenou položku do IS.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                switch (FormVazby_P_PS_List.VyrobaOdvodPOHODATyp)
                {
                    case TypZpravovaniVyroby.Vazba_P_TP:
                        {
                            MessageBox.Show("Není možno zrušit položku při typu 'Vazba_P_TP'", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    case TypZpravovaniVyroby.Vazba_P_PS:
                        {
                            MessageBox.Show("Není možno zrušit položku při typu 'Vazba_P_PS'", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    case TypZpravovaniVyroby.Vazba_P_Vyroba:
                    case TypZpravovaniVyroby.Vazba_P:
                        break;
                }

                var dr = MessageBox.Show("Opravdu chcete zrušit příznak ISOK, odvedené položky?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if(dr != DialogResult.Yes)
                    return;


                if ((providerP != null) && (providerP is Fask.Interfaces.Vyroba.Production.IProduction_Delete_ISOK))
                    ((Fask.Interfaces.Vyroba.Production.IProduction_Delete_ISOK)providerP).Production_Delete_ISOK(rowVyrobek);
                else
                    throw new Exception("IProduction_Delete_ISOK not implementet");

                PerformVyhledat_Vyrobek();
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        #endregion

        #region Perform Metody

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

        #endregion

        #region Progress indikatory

        private void ProgressIndicatorVyrobekStop()
        {
            if (this.InvokeRequired)
            {
                //sa vytvori nove vlakno v kterem je zavolana znovu zazo metoda
                this.BeginInvoke((System.Threading.ThreadStart)delegate() { this.ProgressIndicatorVyrobekStop(); });
                return;
            }

            progressIndicatorVyrobek.Stop();
            progressIndicatorVyrobek.Visible = false;
        }

        private void ProgressIndicatorMaterialStop()
        {
            if (this.InvokeRequired)
            {
                //sa vytvori nove vlakno v kterem je zavolana znovu zazo metoda
                this.BeginInvoke((System.Threading.ThreadStart)delegate() { this.ProgressIndicatorMaterialStop(); });
                return;
            }

            progressIndicatorMaterial.Stop();
            progressIndicatorMaterial.Visible = false;
        }

        private void ProgressIndicatorVyrobekStart()
        {
            if (this.InvokeRequired)
            {
                //sa vytvori nove vlakno v kterem je zavolana znovu zazo metoda
                this.BeginInvoke((System.Threading.ThreadStart)delegate() { this.ProgressIndicatorVyrobekStart(); });
                return;
            }

            try
            {
                // prepocet stredu datagridu
                this.progressIndicatorVyrobek.Location = new Point(this.dgVyrobky.Location.X + (this.dgVyrobky.Width / 2) - (progressIndicatorVyrobek.Size.Width / 2), this.dgVyrobky.Location.Y + (this.dgVyrobky.Height / 2) - (progressIndicatorVyrobek.Size.Height / 2));
            }
            catch { }
            progressIndicatorVyrobek.Start();
            progressIndicatorVyrobek.Visible = true;
        }

        private void ProgressIndicatorMaterialStart()
        {

            if (this.InvokeRequired)
            {
                //sa vytvori nove vlakno v kterem je zavolana znovu zazo metoda
                this.BeginInvoke((System.Threading.ThreadStart)delegate() { this.ProgressIndicatorMaterialStart(); });
                return;
            }


            try
            {
                // prepocet stredu datagridu
                this.progressIndicatorMaterial.Location = new Point(this.dgMaterialy.Location.X + (this.dgMaterialy.Width / 2) - (progressIndicatorMaterial.Size.Width / 2), this.dgMaterialy.Location.Y + (this.dgMaterialy.Height / 2) - (progressIndicatorMaterial.Size.Height / 2));
            }
            catch { }
            progressIndicatorMaterial.Start();
            progressIndicatorMaterial.Visible = true;
        }

        #endregion

        #region Filtry Material

        #region FILTRY

        /// <summary>
        /// Nastavi zvoleny filtr do comboboxu, ...
        /// </summary>
        /// <param name="filtr"></param>
        private void PerformNastavitFiltr(Fask.Interfaces.Filtry.Vazby_P_PS_Filtr filtr)
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
                 comboBox_MJ.Text = filtr.MaterialMJ;

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

                Fask.Interfaces.Filtry.Vazby_P_PS_Filtr filtr = rowFiltr_Materialy;

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
        private bool CreateFilter(ref Fask.Interfaces.Filtry.Vazby_P_PS_Filtr filtr)
        {
            if (filtr == null)
                filtr = new Fask.Interfaces.Filtry.Vazby_P_PS_Filtr();


            // nacteni z ComboBoxu do Filtru
            filtr.MaterialITEMNMBR = comboBox_ITEMNMBR.Text.Trim();
            filtr.MaterialITEMDESC = comboBox_ITEMDESC.Text.Trim();
            filtr.MaterialMJ = comboBox_MJ.Text.Trim();
           


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

                Fask.Interfaces.Filtry.Vazby_P_PS_Filtr filtr = new Fask.Interfaces.Filtry.Vazby_P_PS_Filtr();
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
                DataTable dtchanged = this.DataSet_Materialy.Production_Sources_Odvod.GetChanges();
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

                Fask.Interfaces.Filtry.Vazby_P_PS_Filtr filtr = new Fask.Interfaces.Filtry.Vazby_P_PS_Filtr();
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
                Fask.Interfaces.Filtry.Vazby_P_PS_Filtr filtr = (Fask.Interfaces.Filtry.Vazby_P_PS_Filtr)e.Argument;
                Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();
                //Fask.Interfaces.DataSets.SkladLokace ds = new Fask.Interfaces.DataSets.SkladLokace();

                if (bw_Materialy_stav.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                if (rowVyrobek != null)
                {
                    ((Fask.Interfaces.Vyroba.ProductionSources.IProductionSources)providerPS).GUID_Production = rowVyrobek.GUID;

                    if (providerPS is Fask.Interfaces.Vyroba.ProductionSources.IProductionSources_GetFiltrovanyVazby)
                        ds = ((Fask.Interfaces.Vyroba.ProductionSources.IProductionSources_GetFiltrovanyVazby)providerPS).GetFiltrovanyVazby(filtr);
                    else
                        throw new Exception("IProductionSources_GetFiltrovanyVazby not implementet");


                    ((Fask.Interfaces.Vyroba.ProductionSources.IProductionSources)providerPS).GUID_Production = null;

                   
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
                    DataSet_Materialy = new Fask.Interfaces.DataSets.Vyroba();
                    bs_materialy.DataSource = DataSet_Materialy;
                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, e.Error);
                    MessageBox.Show(e.Error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (e.Cancelled)
                {
                    // handle cancellation
                    DataSet_Materialy = new Fask.Interfaces.DataSets.Vyroba();
                    bs_materialy.DataSource = DataSet_Materialy;
                }
                else
                {
                    // uspesne dokonceno ...
                    // use it on the UI thread
                    DataSet_Materialy = (Fask.Interfaces.DataSets.Vyroba)e.Result;
                    if (DataSet_Materialy == null)
                        DataSet_Materialy = new Fask.Interfaces.DataSets.Vyroba();

                    bs_materialy.DataSource = DataSet_Materialy;
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
        private void PerformNastavitFiltr_Vyrobek(Fask.Interfaces.Filtry.Vazby_P_PS_Filtr filtr)
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

                Fask.Interfaces.Filtry.Vazby_P_PS_Filtr filtr = rowFiltr_Vyrobky;

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
        private bool CreateFilter_Vyrobek(ref Fask.Interfaces.Filtry.Vazby_P_PS_Filtr filtr)
        {
            if (filtr == null)
                filtr = new Fask.Interfaces.Filtry.Vazby_P_PS_Filtr();


            // nacteni z ComboBoxu do Filtru
            filtr.MaterialITEMNMBR = comboBox_Vyrobek_ITEMNMBR.Text.Trim();
            filtr.MaterialITEMDESC = comboBox_Vyrobek_ITEMDESC.Text.Trim();
            filtr.MaterialMJ = comboBox_Vyrobek_MJ.Text.Trim();



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

                Fask.Interfaces.Filtry.Vazby_P_PS_Filtr filtr = new Fask.Interfaces.Filtry.Vazby_P_PS_Filtr();
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
        private void PerformVyhledat_Vyrobek()
        {
            try
            {
                DataTable dtchanged = this.DataSet_Vyrobky.Production_Odvod.GetChanges();
                if (dtchanged != null && dtchanged.Rows.Count > 0) //obsahuje zmeny ... 
                {
                    DialogResult dr = MessageBox.Show("Obsahuje změny, pokračovat?", "data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == System.Windows.Forms.DialogResult.No)
                        return;
                }

                if (bw_Vyrobky_stav.IsBusy)
                {
                    bw_Vyrobky_stav.CancelAsync();
                    while (bw_Vyrobky_stav.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }

                ProgressIndicatorVyrobekStart();

                Fask.Interfaces.Filtry.Vazby_P_PS_Filtr filtr = new Fask.Interfaces.Filtry.Vazby_P_PS_Filtr();
                if (!CreateFilter_Vyrobek(ref filtr))
                    return;

                int FirstDisplayedScrollingRowIndex = this.dgVyrobky.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

                bw_Vyrobky_stav.RunWorkerAsync(filtr);

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

            PerformVyhledat_Vyrobek();
        }

#endregion

        #region BACKGRUND workery

        private void bw_Vyrobky_stav_DoWork(object sender, DoWorkEventArgs e)
        {
            //try
            //{
                Fask.Interfaces.Filtry.Vazby_P_PS_Filtr filtr = (Fask.Interfaces.Filtry.Vazby_P_PS_Filtr)e.Argument;
                Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();
         
                if (bw_Vyrobky_stav.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                if (providerP is Fask.Interfaces.Vyroba.Production.IProduction_GetFiltrovanyProductionVazby)
                    ds = ((Fask.Interfaces.Vyroba.Production.IProduction_GetFiltrovanyProductionVazby)providerP).Production_GetFiltrovanyProductionVazby(filtr);
                else
                    throw new Exception("IProduction_GetFiltrovanyProductionVazby not implementet");



                if (bw_Vyrobky_stav.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                e.Result = ds;

        }

        private void bw_Vyrobky_stav_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                // check error, check cancel, then use result
                if (e.Error != null)
                {
                    // handle the error
                    DataSet_Vyrobky = new Fask.Interfaces.DataSets.Vyroba();
                    bs_vyrobky.DataSource = DataSet_Vyrobky;
                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, e.Error);
                    MessageBox.Show(e.Error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (e.Cancelled)
                {
                    // handle cancellation
                    DataSet_Vyrobky = new Fask.Interfaces.DataSets.Vyroba();
                    bs_vyrobky.DataSource = DataSet_Vyrobky;
                }
                else
                {
                    // uspesne dokonceno ...
                    // use it on the UI thread
                    DataSet_Vyrobky = (Fask.Interfaces.DataSets.Vyroba)e.Result;
                    if (DataSet_Vyrobky == null)
                        DataSet_Vyrobky = new Fask.Interfaces.DataSets.Vyroba();

                    bs_vyrobky.DataSource = DataSet_Vyrobky;
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

        #region Import to IS

       private void PerformImport(Fask.Interfaces.DataSets.Vyroba.ProductionImportRow Row, bool PreskocDisp)
        {
            try
            {
                if (bw_ImportToIS.IsBusy)
                {
                    bw_ImportToIS.CancelAsync();
                    while (bw_ImportToIS.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }

                switch (FormVazby_P_PS_List.VyrobaOdvodPOHODATyp)
                {
                case TypZpravovaniVyroby.Vazba_P_PS:
                    {
                        ProgressIndicatorMaterialStart();
                        break;
                    }
                case TypZpravovaniVyroby.Vazba_P_TP:
                    {
                        ProgressIndicatorMaterialStart();
                        break;
                    }
                case TypZpravovaniVyroby.Vazba_P_Vyroba:
                    {
                        ProgressIndicatorVyrobekStart();
                        break;
                    }
                case TypZpravovaniVyroby.Vazba_P:
                    {
                        ProgressIndicatorVyrobekStart();
                        break;
                    }
                }

                RepeateCreateVydej rep = new RepeateCreateVydej();

                rep.Row = Row;
                rep.PreskocDisp = PreskocDisp;

                bw_ImportToIS.RunWorkerAsync(rep);
            }
            catch (Exception ex)
            {
                ProgressIndicatorMaterialStop();
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bw_ImportToIS_DoWork(object sender, DoWorkEventArgs e)
        {

            // 1. dojde mi jak predavany parameter číslo dávky a Sklad
            // 2. pošlu do IS Vydejku na material, pokud je OK 
            // 3. pošlu do ID Prijemku na Vyrobek


            try
            {



                RepeateCreateVydej rep = (RepeateCreateVydej)e.Argument;

               
                if (bw_ImportToIS.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                int CountEntries = rep.Row.CountEntries;
                string SKL_ID = rep.Row.IsSKL_IDNull() ? string.Empty : rep.Row.SKL_ID;


                Fask.Interfaces.Classes.StatusInfo_Dispo status = null;
                //Zavolani metody s backgroungworkerem....

                #region Rozhodovani varianty

                switch (FormVazby_P_PS_List.VyrobaOdvodPOHODATyp)
                {
                    case TypZpravovaniVyroby.Vazba_P_PS:
                        {
                            Vazba_P_PS(CountEntries, SKL_ID, out  status, false, rep.PreskocDisp);
                            break;
                        }
                    case TypZpravovaniVyroby.Vazba_P_TP:
                        {
                            Vazba_P_TP(CountEntries, SKL_ID);
                            Vazba_P_PS(CountEntries, SKL_ID, out  status, true, rep.PreskocDisp);
                            break;
                        }
                    case TypZpravovaniVyroby.Vazba_P_Vyroba:
                        {
                            Vazba_P_Vyroba(CountEntries, SKL_ID, out status);
                            break;
                        }
                    case TypZpravovaniVyroby.Vazba_P:
                        {
                            Vazba_P(CountEntries, SKL_ID, out status);
                            break;
                        }
                    default:
                        break;
                }

                #endregion

                if (bw_ImportToIS.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                rep.status = status;

                e.Result = rep;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void bw_ImportToIS_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
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
                    Fask.Logging.ExceptionHandler2.Handle(LogLevel.Error, "Operace byla zrušena.");
                    MessageBox.Show("Operace byla zrušena.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    //if (e.Result is Fask.Interfaces.Classes.StatusInfo_Dispo)
                    //{
                    //    Fask.Interfaces.Classes.StatusInfo_Dispo si = (Fask.Interfaces.Classes.StatusInfo_Dispo)e.Result;

                    //    if (si.Description == "ERROR" && si.dt != null)
                    //    {
                    //        ShowDataDisponibility(si.dt);
                    //    }
                    //    else
                    //    {
                    //        if (!string.IsNullOrEmpty(si.Description))
                    //            MessageBox.Show(string.Format("Příjemka č.{0} exportována.", si.Description), "Import", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    //    }
                    //}

                    if (e.Result is RepeateCreateVydej)
                    {
                        RepeateCreateVydej si = (RepeateCreateVydej)e.Result;


                        switch (FormVazby_P_PS_List.VyrobaOdvodPOHODATyp)
                        {
                            case TypZpravovaniVyroby.Vazba_P_Vyroba:
                                {
                                    if (!string.IsNullOrEmpty(si.status.Description))
                                        MessageBox.Show(string.Format("Výroba exportována  č.{0}", si.status.Description), "Import", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                                    break;
                                }
                            default:
                                {
                                    if (si.status.Description == "ERROR" && si.status.dt != null)
                                    {
                                        ShowDataDisponibility(si);
                                    }
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(si.status.Description))
                                            MessageBox.Show(string.Format("Příjemka exportována č.{0}", si.status.Description), "Import", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                                    }

                                    break;
                                }
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
                PerformVyhledat_Vyrobek();
            }
        }


        /// <summary>
        /// Zpracovává import Příjemky pro Výrobu.
        /// Metoda kontroluje, zda je poskytovatel implementován, a volá metodu Production_ImportPohoda_Zdroj_P_Vyroba.
        /// Pokud implementace chybí, vyhodí výjimku.
        /// </summary>
        /// <param name="CountEntries">Počet záznamů k importu.</param>
        /// <param name="SKL_ID">Identifikátor skladu.</param>
        /// <param name="status">Výstupní parametr s informací o výsledku operace.</param>
        private void Vazba_P_Vyroba(int CountEntries, string SKL_ID, out Fask.Interfaces.Classes.StatusInfo_Dispo status)
        {
            #region Vyroba / Výrobky / Production

            status = new Fask.Interfaces.Classes.StatusInfo_Dispo();

            if (providerVazby is Fask.Interfaces.Vyroba.Production.IProduction_ImportPohoda_Zdroj_P_Vyroba)
                status.Description = ((Fask.Interfaces.Vyroba.Production.IProduction_ImportPohoda_Zdroj_P_Vyroba)providerVazby)
                    .Production_ImportPohoda_Zdroj_P_Vyroba(CountEntries, SKL_ID, FASK.Logins.Uzivatel.Instance.UserID);
            else
                throw new Exception("IProduction_ImportPohoda_Zdroj_P_Vyroba not implementet");

            #endregion
        }

        /// <summary>
        /// Zpracovává proces výdeje materiálu a následného příjmu výrobků v rámci typu zpracování P_PS.
        /// Nejprve odešle výdejku materiálu do systému Pohoda a následně zpracuje příjemku výrobků.
        /// </summary>
        /// <param name="CountEntries">číslo dávky</param>
        /// <param name="SKL_ID">ID Skladu</param>
        /// <param name="e">event agument</param>
        /// <param name="status">navratový status</param>
        /// <param name="smazat_PS">príznak či smazat PS po chybe disponibility</param>
        private void Vazba_P_PS(int CountEntries, string SKL_ID, out Fask.Interfaces.Classes.StatusInfo_Dispo status, bool smazat_PS, bool PreskocDisp)
        {
            
            #region Vydejka / Materialy / Production Sources

            //Fask.Interfaces.Classes.StatusInfo_Dispo dd = null;

            status = new Fask.Interfaces.Classes.StatusInfo_Dispo();

            if (providerPS is Fask.Interfaces.Vyroba.ProductionSources.IProductionSources_ImportVydejkaPohoda)
                status = ((Fask.Interfaces.Vyroba.ProductionSources.IProductionSources_ImportVydejkaPohoda)providerPS).ImportVydejkaPohoda(CountEntries, SKL_ID, FASK.Logins.Uzivatel.Instance.UserID, smazat_PS, PreskocDisp);
            else
                throw new Exception("IProduction_ImportVydejkaPohoda not implementet");


            //if (string.IsNullOrEmpty(status.Description))
            //{
            //    e.Cancel = true;
            //    //e.Result = "Nenalezeno číslo Výdejky.";
            //    e.Result = status;
            //    return;
            //}

            if (status.Description == "ERROR" && status.dt != null)
            {
                //e.Cancel = true;
                //e.Result = dd;
                return;
            }


            ProgressIndicatorMaterialStop();
            ProgressIndicatorVyrobekStart();


            #endregion

            #region Prijemka / Vyrobky / Production



            if ((providerP != null) && (providerP is Fask.Interfaces.Vyroba.Production.IProduction_ImportPrijemkaPohoda_Zdroj_P_PS))
                status.Description = ((Fask.Interfaces.Vyroba.Production.IProduction_ImportPrijemkaPohoda_Zdroj_P_PS)providerP).Production_ImportPrijemkaPohoda_Zdroj_P_PS(CountEntries, SKL_ID, FASK.Logins.Uzivatel.Instance.UserID , status.Description);
            else
                throw new Exception("IProduction_ImportPrijemkaPohoda not implementet");

            if (string.IsNullOrEmpty(status.Description))
            {
                //e.Cancel = true;
                status.Description = "Nenalezeno číslo Příjemky";
                return;
                //throw new Exception(status);
            }

            #endregion

            //return status;

        }


        private void Vazba_P_TP(int CountEntries, string SKL_ID)
        {
            #region Vydejka / Materialy / FASK_Vyroba_TP

            //Jedná se o dotažení Materialu do tabulky ProductionSources z tabulky FASK_Vyroba_TP


            if (providerVazby is Fask.Interfaces.Vazby.IVazby2_Import_TP2PS)
                ((Fask.Interfaces.Vazby.IVazby2_Import_TP2PS)providerVazby).Import_TP2PS(CountEntries, SKL_ID, FASK.Logins.Uzivatel.Instance.UserID);
            else
                throw new Exception("IVazby2_Import_TP2PS not implemented");


            #endregion
        }

        private void Vazba_P(int CountEntries, string SKL_ID, out Fask.Interfaces.Classes.StatusInfo_Dispo status)
        {
            status = new Fask.Interfaces.Classes.StatusInfo_Dispo();

            if ((providerP != null) && (providerP is Fask.Interfaces.Vyroba.Production.IProduction_ImportPrijemkaPohoda_Zdroj_P))
                status.Description = ((Fask.Interfaces.Vyroba.Production.IProduction_ImportPrijemkaPohoda_Zdroj_P)providerP).Production_ImportPrijemkaPohoda_Zdroj_P(CountEntries, FASK.Logins.Uzivatel.Instance.UserID, SKL_ID);
            else
                throw new Exception("IProduction_ImportPrijemkaPohoda_Zdroj_P not implementet");

            if (string.IsNullOrEmpty(status.Description))
            {
                status.Description = "Nenalezeno číslo Příjemky";
                return;
            }

        }


        private void ShowDataDisponibility(RepeateCreateVydej si)
        {
            if (this.InvokeRequired)
            {
                //sa vytvori nove vlakno v kterem je zavolana znovu zazo metoda
                this.BeginInvoke((System.Threading.ThreadStart)delegate() { this.ShowDataDisponibility(si); });
                return;
            }


            using (Konzola.Vyroba.Transakce.Form_DisponibilityPrehled disp = new Transakce.Form_DisponibilityPrehled())
            {
                disp.TableDisp = (Fask.POHODA.Disponibility.ValidateData.VydejKontrolaDataTable)si.status.dt;

                if (disp.ShowDialog() == System.Windows.Forms.DialogResult.Retry)
                {
                    PerformImport(si.Row,true);
                } 
            }
        }


        #endregion

        #region DataGrid
        
        private void dgVyrobky_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Error, "Jedá se o chybu ktera vznika nahodnym jezdenim nahoru a dolu v Datagridview...");
            Fask.Logging.ExceptionHandler2.Handle(e.Exception);
        }

        private void dgMaterialy_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Error, "Jedá se o chybu ktera vznika nahodnym jezdenim nahoru a dolu v Datagridview...");
            Fask.Logging.ExceptionHandler2.Handle(e.Exception);
        }

        private void advancedDataGridViewSearchToolBar_Vyrobky_Search(object sender, Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs e)
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

        private void advancedDataGridViewSearchToolBar_Materialy_Search(object sender, Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs e)
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

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            NastavDatagrid();

            try
            {
                if (rowVyrobek != null)
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

        private void NastavDatagrid()
        {
            #region prace s datagridem - nastaveni numeric hodnot na pocet desetinnych mist
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

            #region prace s datagridem - nastaveni numeric hodnot na pocet desetinnych mist
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


        #endregion

        private void tsmiSchvalitVybrane_Click(object sender, EventArgs e)
        {

        }
    }

    public class RepeateCreateVydej
    {
        public Fask.Interfaces.DataSets.Vyroba.ProductionImportRow Row { get; set; }
        public Fask.Interfaces.Classes.StatusInfo_Dispo status { get; set; }
        public bool PreskocDisp { get; set; }
    }
}
