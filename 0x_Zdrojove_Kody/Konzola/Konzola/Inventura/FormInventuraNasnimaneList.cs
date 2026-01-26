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

namespace Konzola.Inventura
{
    public partial class FormInventuraNasnimaneList : Form
    {
        private Fask.Interfaces.IMES providerSklady = null;
        private Fask.Interfaces.IMES providerInventura = null;
        //private Fask.Interfaces.IVyrobaKonzola providerUzivatele = null;
        private Fask.Interfaces.IMES providerVariantySortiment = null;
        private Fask.Interfaces.IMES providerLokaceTypy = null;


        private List<Fask.Interfaces.Filtry.InventuraNasnimaneListFiltr> filtry = new List<Fask.Interfaces.Filtry.InventuraNasnimaneListFiltr>();
        private Fask.Interfaces.DataSets.Sklady dsSklady = new Fask.Interfaces.DataSets.Sklady();
        private FASK.Logins.DataSets.Pristupy.FASK_LoginsDataTable dtUzivatele = new FASK.Logins.DataSets.Pristupy.FASK_LoginsDataTable();
        private Fask.Interfaces.Filtry.InventuraNasnimaneListFiltr lastSelectedFiltr = null;

        /// <summary>
        /// Zvolené id před seřazením
        /// </summary>
        string sortedID = string.Empty;

        public Fask.Interfaces.DataSets.Inventura.CZMST_I4Row SelectedRow
        {
            get
            {
                try
                {
                    return ((DataRowView)(dgInventuraPredloha.BindingContext[bsInventuraPredloha].Current)).Row as Fask.Interfaces.DataSets.Inventura.CZMST_I4Row;
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
        private Fask.Interfaces.Filtry.InventuraNasnimaneListFiltr rowFiltr
        {
            get
            {
                try
                {
                    return tscbFiltry.SelectedItem as Fask.Interfaces.Filtry.InventuraNasnimaneListFiltr;
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Vybrany checkbox ze seznamu.
        /// </summary>
        private Fask.Interfaces.Classes.VYPOCET_STAVU vypocetStavu
        {
            get
            {
                try
                {
                    var choice = gbVypocetStavu.Controls.OfType<RadioButton>().FirstOrDefault(x => x.Checked);
                    return (Fask.Interfaces.Classes.VYPOCET_STAVU)choice.Tag;
                }
                catch
                {
                    return Fask.Interfaces.Classes.VYPOCET_STAVU.Polozky;
                }
            }
            set
            {
                try
                {
                    var choice = gbVypocetStavu.Controls.OfType<RadioButton>().FirstOrDefault(x => ((Fask.Interfaces.Classes.VYPOCET_STAVU)x.Tag) == value);
                    if (choice != null)
                        choice.Checked = true;
                }
                catch
                {
                    rbVypocetDlePolozky.Checked = true;
                }
            }
        }

        /// <summary>
        /// Zvoleny uzivatel v ComboBoxu
        /// </summary>
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
        }

        /// <summary>
        /// Zvolene cislo davky v ComboBoxu
        /// </summary>
        private Fask.Interfaces.DataSets.Inventura.CZMST_I1HRow rowCountEntries
        {
            get
            {
                try
                {
                    return comboBoxCountEntries.SelectedItem as Fask.Interfaces.DataSets.Inventura.CZMST_I1HRow;
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Zvolene ID skladu v ComboBoxu
        /// </summary>
        private Fask.Interfaces.DataSets.Sklady.CZMST093Row rowSKLID
        {
            get
            {
                try
                {
                    return comboBoxSKLID.SelectedItem as Fask.Interfaces.DataSets.Sklady.CZMST093Row;
                }
                catch
                {
                    return null;
                }
            }
        }


        #region Eventy formu

        public FormInventuraNasnimaneList()
        {
            InitializeComponent();

            this.dgInventuraPredloha.UpdateColumnHeaderCellsByDatasource();
            panelButtons.Menu = menuStrip1;
        }

        private void FormInventuraNasnimaneList_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                this.WindowState = FormWindowState.Maximized;

                rbVypocetDlePolozky.Tag = Fask.Interfaces.Classes.VYPOCET_STAVU.Polozky;
                rbVypocetDlePolozkyALokace.Tag = Fask.Interfaces.Classes.VYPOCET_STAVU.Polozky_a_Lokace;

                // načtení konfigurace datagridu z nastavení aplikace
                this.dgInventuraPredloha.LoadConfiguration(this.GetType().ToString());

                panelButtons.LoadConfiguration(this.GetType().ToString());
                panelButtons.Init();
                panelButtons.Size = new Size(85, 700);

                advancedDataGridViewSearchToolBar1.SetColumns(dgInventuraPredloha.Columns);


                //// načtení konfigurace vytvořených filtrů
                this.filtry = Extensions.ListExt.ReadFromXML<Fask.Interfaces.Filtry.InventuraNasnimaneListFiltr>(this.GetType().ToString() + ".filtr");
                tscbFiltry.ComboBox.DataSource = this.filtry;
                tscbFiltry.SelectedItem = null;
                tscbFiltry.ComboBox.DropDownWitdhAutosize();


                rbVypocetDlePolozky.Checked = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Inventura[0].PredlohaListVypocetStavuDlePolozky;
                rbVypocetDlePolozkyALokace.Checked = !Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Inventura[0].PredlohaListVypocetStavuDlePolozky;

                gbVariantyLokaciMaterialu.Enabled = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].InventuraPohybyVariantyLokaciPovolit;
                gbVariantyLokaciMaterialu.Visible = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].InventuraPohybyVariantyLokaciPovolit;

                // inicializace providera
                InitProvider();

                if (providerInventura == null)
                    throw new Exception("Provider 'Inventura' není inicializován");

                if (providerSklady == null)
                    throw new Exception("Provider 'Sklady' není inicializován");

                //if (providerUzivatele == null)
                //    throw new Exception("Provider 'Uživatelé' není inicializován");

                if (providerLokaceTypy == null)
                    throw new Exception("Provider 'Typy lokací' není inicializován");

                if (providerVariantySortiment == null)
                    throw new Exception("Provider 'Varianty sortimentu' není inicializován");

                //System.Threading.Thread loadThread = new System.Threading.Thread(LoadDataAsync);
                //loadThread.IsBackground = true;
                //loadThread.Start();

                buttonVyhledat.Focus();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormInventuraNasnimaneList_KeyDown(object sender, KeyEventArgs e)
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

        private void FormInventuraNasnimaneList_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                

                this.dgInventuraPredloha.SaveConfiguration(this.GetType().ToString());
                panelButtons.SaveConfiguration(this.GetType().ToString());

                // ulozeni vsech filtru
                this.filtry.WriteXML(this.GetType().ToString() + ".filtr");

                Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Inventura[0].PredlohaListVypocetStavuDlePolozky = rbVypocetDlePolozky.Checked;
                Konfigurace.Globals_Konfig_Konzola.SaveConfiguration();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormInventuraNasnimaneList_Shown(object sender, EventArgs e)
        {
            try
            {
                this.progressIndicator1.Size = new Size(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ostatni[0].ProgressIndicatorSize, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ostatni[0].ProgressIndicatorSize);
                this.progressIndicator1.Location = new Point(this.dgInventuraPredloha.Location.X + (this.dgInventuraPredloha.Width / 2) - (progressIndicator1.Size.Width / 2), this.dgInventuraPredloha.Location.Y + (this.dgInventuraPredloha.Height / 2) - (progressIndicator1.Size.Height / 2));
            }
            catch { }
        }


        #endregion


        private void LoadDataAsync()
        {
            try
            {
                Fask.Interfaces.DataSets.Sklady dsSkladyData = new Fask.Interfaces.DataSets.Sklady();
                Fask.Interfaces.DataSets.Inventura dsInventuraHlavickyData = new Fask.Interfaces.DataSets.Inventura();
                FASK.Logins.DataSets.Pristupy.FASK_LoginsDataTable dtUzivateleData = new FASK.Logins.DataSets.Pristupy.FASK_LoginsDataTable();

                // naplneni ciselniku skladu
                //dsSkladyData = ((Fask.Interfaces.Ciselniky.ISklady)providerSklady).GetSklady();

                if ((providerSklady != null) && (providerSklady is Fask.Interfaces.Ciselniky.Sklady.ISklady2_GetSklady))
                    dsSkladyData = ((Fask.Interfaces.Ciselniky.Sklady.ISklady2_GetSklady)providerSklady).GetSklady();
                else
                    throw new NotImplementedException("Provider neimplementuje ISklady2_GetSklady.");


                // naplneni ciselniku hlavicek
                //dsInventuraHlavickyData = ((Fask.Interfaces.Inventura.IInventura)providerInventura).GetHlavicky();

                if ((providerInventura != null) && (providerInventura is Fask.Interfaces.Inventura.IInventura2_GetHlavicky))
                    dsInventuraHlavickyData = ((Fask.Interfaces.Inventura.IInventura2_GetHlavicky)providerInventura).GetHlavicky();
                else
                    throw new NotImplementedException("Provider neimplementuje IInventura2_GetHlavicky.");

                // naplneni ciselniku uzivatelu
                //dsUzivateleData = ((Fask.Interfaces.Ciselniky.IUzivatele)providerUzivatele).GetUzivatele();


                //if ((providerUzivatele != null) && (providerUzivatele is Fask.Interfaces.Ciselniky.IUzivatele2_GetUzivatele))
                //    dsUzivateleData = ((Fask.Interfaces.Ciselniky.IUzivatele2_GetUzivatele)providerUzivatele).GetUzivatele();
                //else
                //    throw new NotImplementedException("Provider neimplementuje IUzivatele2_GetUzivatele.");

                dtUzivateleData = FASK.Logins.Uzivatel.Instance.Komunikace.GetLogins();

                // navrat do hlavniho vlakna
                if (InvokeRequired)
                {
                    BeginInvoke(new Action(() =>
                    {
                        PopulateUI(dsSkladyData, dsInventuraHlavickyData, dtUzivateleData);
                    }));
                }
            }
            catch (Exception ex)
            {
                if (InvokeRequired)
                {
                    BeginInvoke(new Action(() =>
                    {
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                        MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }));
                }
            }
        }

        private void PopulateUI(Fask.Interfaces.DataSets.Sklady dsSkladyData, Fask.Interfaces.DataSets.Inventura dsInventuraPredlohaData, FASK.Logins.DataSets.Pristupy.FASK_LoginsDataTable dtUzivateleData)
        {
            try
            {
                // naplneni comboboxu uzivatelu
                dtUzivatele = dtUzivateleData;
                comboBoxUzivatel.Items.AddRange(dtUzivateleData.Select(null, "surname asc"));
                comboBoxUzivatel.SelectedItem = null;

                // naplneni comboboxu skladu
                dsSklady = dsSkladyData;
                comboBoxSKLID.Items.AddRange(dsSkladyData.CZMST093.Select(null, "skl_desc asc"));
                comboBoxSKLID.SelectedItem = null;

                comboBoxCountEntries.Items.AddRange(dsInventuraPredlohaData.CZMST_I1H.Select(null, "CountEntries desc"));
                //comboBoxType.ValueMember = dtPohybData.TypeColumn.ColumnName;
                comboBoxCountEntries.SelectedItem = null;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Inicializace providera
        /// </summary>
        private void InitProvider()
        {
            if (String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                return;

            try
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
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                if (providerSklady == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
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

            //try
            //{
            //    if (providerUzivatele == null) //inicializace se provede pouze pokud nebyla provedena ... 
            //    {
            //        Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
            //        Type[] types = providerAssemlby.GetTypes();
            //        foreach (Type t in types)
            //        {
            //            try
            //            {
            //                if (typeof(Fask.Interfaces.Ciselniky.IUzivatele2).IsAssignableFrom(t))
            //                {
            //                    providerUzivatele = (Fask.Interfaces.Ciselniky.IUzivatele2)providerAssemlby.CreateInstance(t.FullName);
            //                    if (providerUzivatele != null)
            //                        break;
            //                }
            //            }
            //            catch { }
            //        }
            //        //return config;
            //    }

            //    // nastaveni connection stringu
            //    //if (providerUzivatele != null)
            //    //    ((Fask.Interfaces.Ciselniky.IUzivatele)providerUzivatele).ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;

            //    if ((providerUzivatele != null) && (providerUzivatele is Fask.Interfaces.Parametry.IParametry2_ConnectionString))
            //        ((Fask.Interfaces.Parametry.IParametry2_ConnectionString)providerUzivatele).ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
              


            //}
            //catch (Exception ex)
            //{
            //    Fask.Logging.ExceptionHandler2.Handle(ex, "Load Provider.Uzivatel");
            //    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

            try
            {
                if (providerLokaceTypy == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Ciselniky.SkladLokace_LokaceTypy.ISkladLokace_LokaceTypy2).IsAssignableFrom(t))
                            {
                                providerLokaceTypy = (Fask.Interfaces.Ciselniky.SkladLokace_LokaceTypy.ISkladLokace_LokaceTypy2)providerAssemlby.CreateInstance(t.FullName);
                                if (providerLokaceTypy != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                providerLokaceTypy.InitProvider();
       
            
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                if (providerVariantySortiment == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Ciselniky.SkladLokace_LVS.ISkladLokace_LVS2).IsAssignableFrom(t))
                            {
                                providerVariantySortiment = (Fask.Interfaces.Ciselniky.SkladLokace_LVS.ISkladLokace_LVS2)providerAssemlby.CreateInstance(t.FullName);
                                if (providerVariantySortiment != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                providerVariantySortiment.InitProvider();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonVyhledat_Click(object sender, EventArgs e)
        {
            #region prace s datagridem - nastaveni numeric hodnot na pocet desetinnych mist
            try
            {
                // Procházení sloupců v DataGridView
                foreach (DataGridViewColumn column in dgInventuraPredloha.Columns)
                {
                    // Získání typu dat pro sloupec
                    Type dataType = null;

                    if (dgInventuraPredloha.DataSource is DataTable dataTable)
                    {
                        // Pokud je datový zdroj DataTable
                        if (dataTable.Columns.Contains(column.DataPropertyName))
                        {
                            dataType = dataTable.Columns[column.DataPropertyName].DataType;
                        }
                    }
                    else if (dgInventuraPredloha.DataSource is BindingSource bindingSource)
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

        private void PerformOK()
        {
            try
            {
                //DataTable dtchanged = this.dsInventuraPredloha.CZMST_I4.GetChanges();
                //if (dtchanged != null && dtchanged.Rows.Count > 0) //obsahuje zmeny ... 
                //{
                //    DialogResult dr = MessageBox.Show("Obsahuje změny, pokračovat?", "data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                //    if (dr == System.Windows.Forms.DialogResult.No)
                //        return;
                //}

                if (bwLoadSkladPohyb.IsBusy)
                {
                    bwLoadSkladPohyb.CancelAsync();
                    while (bwLoadSkladPohyb.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }

                if (rowCountEntries == null)
                {
                    if (string.IsNullOrEmpty(comboBoxCountEntries.Text.Trim()))
                    {
                        MessageBox.Show("Musí být vybráno číslo dávky", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ProgressIndicatorStop();
                        return;
                    }
                    int countentries = 0;
                    bool result = int.TryParse(comboBoxCountEntries.Text, out countentries);
                    if (!result)
                    {
                        ProgressIndicatorStop();
                        MessageBox.Show("Vybrané číslo dávky musí být číslo", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    // 12.5.2016 PeV: konfiguracne kontrolovat cislo davky
                    //else
                    //{
                    //    // zkusit vyhledat ...
                    //    var res = providerInventura.GetHlavickaByID(Convert.ToInt32(comboBoxCountEntries.Text));
                    //    if (res == null)
                    //    {
                    //        ProgressIndicatorStop();
                    //        MessageBox.Show("Dávka s číslem '" + comboBoxCountEntries.Text.Trim() + "' nebyla nalezena", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //        return;
                    //    }
                    //}
                }

                ProgressIndicatorStart();

                Fask.Interfaces.Filtry.InventuraNasnimaneListFiltr filtr = new Fask.Interfaces.Filtry.InventuraNasnimaneListFiltr();
                if (!CreateFilter(ref filtr))
                    return;

                lastSelectedFiltr = filtr;

                int FirstDisplayedScrollingRowIndex = this.dgInventuraPredloha.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

                bwLoadSkladPohyb.RunWorkerAsync(filtr);
                
                if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dgInventuraPredloha.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dgInventuraPredloha.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index
            }
            catch (Exception ex)
            {
                ProgressIndicatorStop();
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.dgInventuraPredloha.Focus();
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


        private void buttonOznacitVse_Click(object sender, EventArgs e)
        {
            try
            {
                this.dgInventuraPredloha.SelectAll();
                SelectAllRows(true);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SelectAllRows(bool selected)
        {
            try
            {
                if (dsInventuraPredloha == null)
                    return;

                foreach (var item in dsInventuraPredloha.CZMST_I4)
                {
                    item.Vybrano = selected;
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
                this.dgInventuraPredloha.ClearSelection();
                SelectAllRows(false);
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

        private void buttonKonec_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }


        private void bwSkladPohyb_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //bwLoadSkladPohyb.CancelAsync();
                Fask.Interfaces.Filtry.InventuraNasnimaneListFiltr filtr = (Fask.Interfaces.Filtry.InventuraNasnimaneListFiltr)e.Argument;
                Fask.Interfaces.DataSets.Inventura ds = new Fask.Interfaces.DataSets.Inventura();

                if (bwLoadSkladPohyb.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                // nacteni dat v oddelenem vlakne
                //ds = ((Fask.Interfaces.Inventura.IInventura)providerInventura).GetFiltrovaneNasnimane(filtr);

                if ((providerInventura != null) && (providerInventura is Fask.Interfaces.Inventura.IInventura2_GetFiltrovaneNasnimane))
                    ds = ((Fask.Interfaces.Inventura.IInventura2_GetFiltrovaneNasnimane)providerInventura).GetFiltrovaneNasnimane(filtr);
                else
                    throw new NotImplementedException("Provider neimplementuje IInventura2_GetFiltrovaneNasnimane.");


                if (bwLoadSkladPohyb.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                else  // dopocitani stavu
                    PerformVypocitejStav(ref ds);

                e.Result = ds;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void bwSkladPohyb_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                // check error, check cancel, then use result
                if (e.Error != null)
                {
                    // handle the error
                    dsInventuraPredloha = new Fask.Interfaces.DataSets.Inventura();
                    bsInventuraPredloha.DataSource = dsInventuraPredloha;
                }
                else if (e.Cancelled)
                {
                    dsInventuraPredloha = new Fask.Interfaces.DataSets.Inventura();
                    bsInventuraPredloha.DataSource = dsInventuraPredloha.CZMST_I4;
                }
                else
                {
                    // uspesne dokonceno ...
                    dsInventuraPredloha = (Fask.Interfaces.DataSets.Inventura)e.Result;
                    if (dsInventuraPredloha == null)
                        dsInventuraPredloha = new Fask.Interfaces.DataSets.Inventura();

                    bsInventuraPredloha.DataSource = dsInventuraPredloha.CZMST_I4;
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
                this.progressIndicator1.Location = new Point(this.dgInventuraPredloha.Location.X + (this.dgInventuraPredloha.Width / 2) - (progressIndicator1.Size.Width / 2), this.dgInventuraPredloha.Location.Y + (this.dgInventuraPredloha.Height / 2) - (progressIndicator1.Size.Height / 2));
            }
            catch { }
            progressIndicator1.Start();
            progressIndicator1.Visible = true;
        }

        private void PerformVypocitejStav(ref Fask.Interfaces.DataSets.Inventura ds)
        {
            try
            {
                if (!Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady[0].StavZobrazitAktualniMnozstvi)
                    return;
                // TODO: pocitat v oddelenem vlakne
                // dopocitani mnozstvi od zacatku ...

                var test = ds.CZMST_I4.Select(null, "DEX_ROW_ID asc");
                // TODO: zohlednit sklad??? ... podle JiS ne, inventura se ma generovat na sklad ...
                // TODO: pouzit tridu jako klic kvuli pripadnemu pridani filtru
                // vypocet dle polozky a lokace
                if (rbVypocetDlePolozkyALokace.Checked)
                {
                    Dictionary<string, Dictionary<string, decimal>> data = new Dictionary<string, Dictionary<string, decimal>>();

                    for (int i = test.Count(); i > 0; i--)
                    {
                        Fask.Interfaces.DataSets.Inventura.CZMST_I4Row row = ds.CZMST_I4[i - 1];
                        
                        // pokud neobsahuje itemnmbr, tak ho prida a soucasne prida sarzi ...
                        if (!data.ContainsKey(row.ITEMNMBR.Trim()))
                        {
                            // neobsahuje klic, pridat a nastavit puvodni mnozstvi
                            Dictionary<string, decimal> dict = new Dictionary<string, decimal>();
                            dict.Add((row.LOCNCODE.Trim()), row.QUANTITY);
                            data.Add(row.ITEMNMBR.Trim(), dict);

                            row.Stav = row.QUANTITY;
                        }
                        else
                        {
                            // nalezeno, kontrola, zdali je sarze v seznamu
                            if (!data[row.ITEMNMBR.Trim()].ContainsKey(row.LOCNCODE.Trim()))
                            {
                                // neobsahuje sarzi ... pridat
                                Dictionary<string, decimal> dict = new Dictionary<string, decimal>();
                                data[row.ITEMNMBR.Trim()].Add((row.LOCNCODE.Trim()), row.QUANTITY);
                            }
                            else
                            {
                                // obsahuje sarzi ... zmenit mnozstvi
                                // zaznam jiz existuje z drivejska ... pricte/odecte mnozstvi
                                data[row.ITEMNMBR.Trim()][row.LOCNCODE.Trim()] += row.QUANTITY;
                            }

                            row.Stav = data[row.ITEMNMBR.Trim()][row.LOCNCODE.Trim()];
                            //data[row.ITEMNMBR.Trim()] += row.QTYSHPPD;
                            //row.Stav = data[row.ITEMNMBR.Trim()];
                        }
                        
                    }
                }
                else if (rbVypocetDlePolozky.Checked)
                {
                    Dictionary<string, decimal> data = new Dictionary<string, decimal>();

                    for (int i = test.Count(); i > 0; i--)
                    {
                        Fask.Interfaces.DataSets.Inventura.CZMST_I4Row row = ds.CZMST_I4[i - 1];
                        if (!data.ContainsKey(row.ITEMNMBR.Trim()))
                        {
                            // neobsahuje klic, pridat a nastavit puvodni mnozstvi
                            //data.Add(row.ITEMNMBR.Trim(), row.IsQTYSHPPDNull() ? (decimal?)null : row.QTYSHPPD);
                            data.Add(row.ITEMNMBR.Trim(), row.QUANTITY);
                            row.Stav = row.QUANTITY;
                        }
                        else
                        {
                            // zaznam jiz existuje z drivejska ... pricte/odecte mnozstvi
                            data[row.ITEMNMBR.Trim()] += row.QUANTITY;
                            row.Stav = data[row.ITEMNMBR.Trim()];
                        }
                    }
                }

                ds.AcceptChanges();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rbVypocetDlePolozky_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                // probiha vyhledavani, ukoncit ...
                if (bwLoadSkladPohyb.IsBusy)
                    return;

                if (bwVypocetStavu.IsBusy)
                {
                    bwVypocetStavu.CancelAsync();
                    while (bwVypocetStavu.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }

                ProgressIndicatorStart();
                // udelat kopii datasetu a nasledne az vypocitat stav?
                //PerformVypocitejStav(ref dsSkladPohyb);

                Fask.Interfaces.DataSets.Inventura ds = new Fask.Interfaces.DataSets.Inventura();
                foreach (var item in dsInventuraPredloha.CZMST_I4)
                {
                    ds.CZMST_I4.ImportRow(item);
                }

                ds.CZMST_I4.AcceptChanges();
                bwVypocetStavu.RunWorkerAsync(ds);

            }
            catch { }
            //finally
            //{
            //    ProgressIndicatorPohybStop();
            //}
        }

        private void bwVypocetStavu_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Fask.Interfaces.DataSets.Inventura ds = (Fask.Interfaces.DataSets.Inventura)e.Argument;

                if (bwVypocetStavu.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                //if (bwLoadSkladPohyb.CancellationPending)
                //{
                //    e.Cancel = true;
                //    return;
                //}
                //else
                PerformVypocitejStav(ref ds);
                if (bwVypocetStavu.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                // dopocitani
                e.Result = ds;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                if (InvokeRequired)
                {
                    BeginInvoke(new Action(() =>
                    {
                        ProgressIndicatorStop();
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                        MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }));
                }
            }
        }

        private void bwVypocetStavu_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                // check error, check cancel, then use result
                if (e.Error != null)
                {
                    // handle the error
                    dsInventuraPredloha = new Fask.Interfaces.DataSets.Inventura();

                    bsInventuraPredloha.DataSource = dsInventuraPredloha;
                }
                else if (e.Cancelled)
                {
                    // zdruseno, nic nedelat ...
                }
                else
                {
                    // uspesne dokonceno ...
                    // use it on the UI thread
                    dsInventuraPredloha = (Fask.Interfaces.DataSets.Inventura)e.Result;
                    if (dsInventuraPredloha == null)
                        dsInventuraPredloha = new Fask.Interfaces.DataSets.Inventura();

                    bsInventuraPredloha.DataSource = null;
                    bsInventuraPredloha.DataSource = dsInventuraPredloha.CZMST_I4;
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

        /// <summary>
        /// Vytvoření filteru.
        /// </summary>
        /// <param name="filtr"></param>
        /// <returns></returns>
        private bool CreateFilter(ref Fask.Interfaces.Filtry.InventuraNasnimaneListFiltr filtr)
        {
            if(filtr == null)
                filtr = new Fask.Interfaces.Filtry.InventuraNasnimaneListFiltr();
            
            filtr.UzivatelID = comboBoxUzivatel.Text.Trim();
            //filtr.rowUzivatel = rowUzivatel;
            filtr.rowUzivatel = rowUzivatel == null ? string.Empty : rowUzivatel.USERID;
            filtr.CountEntries = rowCountEntries == null ? Convert.ToInt32(comboBoxCountEntries.Text) : rowCountEntries.CountEntries;
            filtr.MaterialID = comboBoxMaterialID.Text.Trim();
            //filtr.rowMaterialSKLID = rowSKLID;
            filtr.rowMaterialSKLID = rowSKLID == null ? string.Empty : rowSKLID.skl_id;
            filtr.MaterialSKLID = comboBoxSKLID.Text.Trim();
            filtr.MaterialLocncode = comboBoxLOCNCODE.Text.Trim();
            //filtr.FiltrDlePolozky = rbVypocetDlePolozky.Checked;
            //filtr.FiltrDlePolozkyLokace = rbVypocetDlePolozkyALokace.Checked;
            // zjisteni, co je oznaceno ...
            //var choice = gbVypocetStavu.Controls.OfType<RadioButton>().FirstOrDefault(x => x.Checked);
            //Fask.Interfaces.Classes.VYPOCET_STAVU choiceAsEnum = (Fask.Interfaces.Classes.VYPOCET_STAVU)choice.Tag;
            //filtr.FiltrStav = choiceAsEnum;
            filtr.FiltrStav = vypocetStavu;

            return true;
        }

        /// <summary>
        /// Nastavi zvoleny filtr do comboboxu, ...
        /// </summary>
        /// <param name="filtr"></param>
        private void PerformNastavitFiltr(Fask.Interfaces.Filtry.InventuraNasnimaneListFiltr filtr)
        {
            try
            {
                if (filtr == null)
                    return;

                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                PerformVycistitFiltr();
                
                // cislo davky
                comboBoxCountEntries.Text = filtr.CountEntries.ToString();

                // itemnmbr
                comboBoxMaterialID.Text = filtr.MaterialID;

                // TODO: uzivatel
                if (string.IsNullOrEmpty(filtr.rowUzivatel))
                {
                    // uzivatel nevyplnen, doplnit pouze text ...
                    comboBoxUzivatel.Text = filtr.UzivatelID;
                }
                else
                {
                    // uzivatel vyplnen, pokusit se dohledat
                    var uzivatele = dtUzivatele.Where(x => x.USERID == filtr.rowUzivatel);
                    if (uzivatele.Count() > 0)
                        comboBoxUzivatel.SelectedItem = uzivatele.First();
                    else
                        comboBoxUzivatel.Text = filtr.rowUzivatel;
                }

                // TODO: sklad
                if (string.IsNullOrEmpty(filtr.rowMaterialSKLID))
                {
                    // uzivatel nevyplnen, doplnit pouze text ...
                    comboBoxSKLID.Text = filtr.MaterialSKLID;
                }
                else
                {
                    // uzivatel vyplnen, pokusit se dohledat
                    var sklady = dsSklady.CZMST093.Where(x => x.skl_id == filtr.rowMaterialSKLID);
                    if (sklady.Count() > 0)
                        comboBoxSKLID.SelectedItem = sklady.First();
                    else
                        comboBoxSKLID.Text = filtr.rowMaterialSKLID;
                }

                //rbVypocetDlePolozky.Checked = filtr.FiltrDlePolozky;
                //rbVypocetDlePolozkyALokace.Checked = filtr.FiltrDlePolozkyLokace;

                var choice = gbVypocetStavu.Controls.OfType<RadioButton>().FirstOrDefault(x => ((Fask.Interfaces.Classes.VYPOCET_STAVU)x.Tag) == filtr.FiltrStav);
                if (choice != null)
                    choice.Checked = true;

                //Fask.Interfaces.Classes.VYPOCET_STAVU choiceAsEnum = (Fask.Interfaces.Classes.VYPOCET_STAVU)choice.Tag;
                //filtr.FiltrStav = choiceAsEnum;

                // lokace
                comboBoxLOCNCODE.Text = filtr.MaterialLocncode;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show("Nepodařilo se načíst data filtru.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUlozitFiltr_Click(object sender, EventArgs e)
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

                Fask.Interfaces.Filtry.InventuraNasnimaneListFiltr filtr = new Fask.Interfaces.Filtry.InventuraNasnimaneListFiltr();
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

        private void btnNacistFiltr_Click(object sender, EventArgs e)
        {
            PerformNastavitFiltr(rowFiltr);
        }

        private void btnFiltrOdstranit_Click(object sender, EventArgs e)
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

        private void tsbZmena_Click(object sender, EventArgs e)
        {
            PerformZmenitFiltr();
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

                Fask.Interfaces.Filtry.InventuraNasnimaneListFiltr filtr = rowFiltr;

                if (!CreateFilter(ref filtr))
                    return;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show("Nepodařilo se upravit filtr.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAutomatickyNavrh_Click(object sender, EventArgs e)
        {
            try
            {
                PerformAutomatickyNavrhVariantLokaci();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PerformAutomatickyNavrhVariantLokaci()
        {
            try
            {
                if (!Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].InventuraPohybyVariantyLokaciPovolit)
                    return;

                if (dsInventuraPredloha == null)
                    return;

                if (bwLoadSkladPohyb.IsBusy)
                {
                    MessageBox.Show("Probíhá načítání dat, není možné pokračovat.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // TODO: zakazat navrh lokace v pripade, ze je aktivni filtr na lokace?? ... pri insertu do variant lokaci dochazi ke kontrole s DB, takze by to pripadne neproslo
                //if(lastSelectedFiltr != null && !string.IsNullOrEmpty(lastSelectedFiltr.MaterialLocncode.Trim()))
                //{
                //    MessageBox.Show("Není možné automaticky navrhnout lokace, pokud je aktivní filtr na lokace.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}

                // projiti vsech polozek
                foreach (var item in dsInventuraPredloha.CZMST_I4)
                {
                    var data = dsInventuraPredloha.CZMST_I4.Where(
                        x => x.ITEMNMBR.Trim() == item.ITEMNMBR.Trim() &&
                            x.SKL_ID.Trim() == item.SKL_ID.Trim() &&
                            x.LOCNCODE.Trim() != item.LOCNCODE.Trim()
                            );

                    // pokud neni, automaticky se vybere, jinak ne
                    if (data.Count() == 0)
                        item.Vybrano = true;
                    else
                        item.Vybrano = false;
                }

                dsInventuraPredloha.CZMST_I4.AcceptChanges();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPriradVariantyLokaci_Click(object sender, EventArgs e)
        {            
            try
            {
                PerformPriraditVariantyLokace();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PerformPriraditVariantyLokace()
        {
            try
            {
                if (!Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Transakce[0].InventuraPohybyVariantyLokaciPovolit)
                    return;

                // nic neni vybrano
                if (dsInventuraPredloha.CZMST_I4.Where(x => x.Vybrano).Count() == 0)
                    return;

                // najiti defaultniho typu lokace (resp. typ lokace bez typu ...)
                Fask.Interfaces.Filtry.LokaceTypyListFiltr filtr = new Fask.Interfaces.Filtry.LokaceTypyListFiltr();
                filtr.is_default = false;
                filtr.is_normal = false;
                filtr.is_receive = false;
                //var dsTypyLokaci = ((Fask.Interfaces.Ciselniky.ISkladLokace_LokaceTypy)providerLokaceTypy).GetFiltrovaneSkladLokace_LokaceTypy(filtr);
                Fask.Interfaces.DataSets.SkladLokace dsTypyLokaci; 
               
                if ((providerLokaceTypy != null) && (providerLokaceTypy is Fask.Interfaces.Ciselniky.SkladLokace_LokaceTypy.ISkladLokace_LokaceTypy2_GetFiltrovaneSkladLokace_LokaceTypy))
                    dsTypyLokaci = ((Fask.Interfaces.Ciselniky.SkladLokace_LokaceTypy.ISkladLokace_LokaceTypy2_GetFiltrovaneSkladLokace_LokaceTypy)providerLokaceTypy).GetFiltrovaneSkladLokace_LokaceTypy(filtr);
                else
                    throw new NotImplementedException("Provider neimplementuje ISkladLokace_LokaceTypy2_GetFiltrovaneSkladLokace_LokaceTypy.");

                
                Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_LokaceTypyRow rowTypLokace = dsTypyLokaci.CZMST_SkladLokace_LokaceTypy.Count > 0 ? dsTypyLokaci.CZMST_SkladLokace_LokaceTypy.First() : null;

                string locationType = string.Empty;
                // vyber typu lokace
                using (Ciselniky.FormLokaceTypySelect frmTypLokace = new Ciselniky.FormLokaceTypySelect(false, rowTypLokace))
                {
                    frmTypLokace.Text = "Výběr typu lokace";
                    //frmstavy.rowsSelected = rowStav;
                    if (frmTypLokace.ShowDialog(this) != DialogResult.OK)
                        return;

                    locationType = frmTypLokace.SelectedRow.TYPE;
                }

                int pocetZaznamu = 0;
                bool preskocitVse = false;
                foreach (var item in dsInventuraPredloha.CZMST_I4)
                {
                    // preskoceni nezaskrnutych
                    if(!item.Vybrano)
                        continue;

                    // najiti stejne polozky s jinym locncode (a soucasne vybranym)
                    var data = dsInventuraPredloha.CZMST_I4.Where(
                        x => x.ITEMNMBR.Trim() == item.ITEMNMBR.Trim() &&
                            x.SKL_ID.Trim() == item.SKL_ID.Trim() &&
                            x.LOCNCODE.Trim() != item.LOCNCODE.Trim() &&
                            x.Vybrano
                            );

                    if (data.Count() != 0)
                    {
                        if (preskocitVse)
                            continue;

                        try
                        {
                            // vyber zaznamu, ktery se zrovna upravuje
                            int index = this.bsInventuraPredloha.Find(dsInventuraPredloha.CZMST_I4.DEX_ROW_IDColumn.ColumnName, item.DEX_ROW_ID);
                            bsInventuraPredloha.Position = index;
                        }
                        catch { }

                        // zobrazeni dialogu pro preruseni, preskoceni, preskoceni vsech
                        // 0 -> Přeskočit
                        // 1 -> Přeskočit vše
                        // 2 -> Přerušit
                        int pos = Cliver.Message.Show(null,
                            "Materiál '" + item.ITEMNMBR.Trim() + "' je umístěn ve skladu '" + item.SKL_ID.Trim() + "' na více lokacích.",
                            2,
                            new string[] { "Přeskočit", "Přeskočit vše", "Přerušit" }
                            );

                        if (pos == 2)
                            return;
                        else if (pos == 0)
                            continue;
                        else if (pos == 1)
                        {
                            preskocitVse = true;
                            continue;
                        }
                    }
                    
                    //var result = ((Fask.Interfaces.Ciselniky.ISkladLokace_LokaceVariantySortiment)providerVariantySortiment).InsertSkladLokace_LokaceVariantySortiment(Settings.TerminalID, item, locationType);
                    Fask.Interfaces.Classes.INVENTURA_PLNENI_VARIANT_STATUS result;

                    if ((providerVariantySortiment != null) && (providerVariantySortiment is Fask.Interfaces.Ciselniky.SkladLokace_LVS.ISkladLokace_LVS2_InsertSkladLokace_LokaceVariantySortiment))
                        result = ((Fask.Interfaces.Ciselniky.SkladLokace_LVS.ISkladLokace_LVS2_InsertSkladLokace_LokaceVariantySortiment)providerVariantySortiment).InsertSkladLokace_LokaceVariantySortiment(Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].TerminalID, item, locationType);
                    else
                        throw new NotImplementedException("Provider neimplementuje ISkladLokace_LokaceVariantySortiment2_InsertSkladLokace_LokaceVariantySortiment.");


                    
                    // odskrtnuto
                    item.Vybrano = false;
                    pocetZaznamu++;
                    // 16.5.2016 PeV: predelana funkcionalita na overovani pouze nad datasetem
                    //if (result == Fask.Interfaces.Classes.INVENTURA_PLNENI_VARIANT_STATUS.ERROR_VICE_LOKACI)
                    //{
                    //    if (preskocitVse)
                    //        continue;

                    //    try
                    //    {
                    //        // vyber zaznamu, ktery se zrovna upravuje
                    //        int index = this.bsInventuraPredloha.Find(dsInventuraPredloha.CZMST_I4.DEX_ROW_IDColumn.ColumnName, item.DEX_ROW_ID);
                    //        bsInventuraPredloha.Position = index;
                    //    }
                    //    catch { }

                        

                    //    // zobrazeni dialogu pro preruseni, preskoceni, preskoceni vsech
                    //    // 0 -> Přeskočit
                    //    // 1 -> Přeskočit vše
                    //    // 2 -> Přerušit
                    //    int pos = Cliver.Message.Show(null,
                    //        "Materiál '" + item.ITEMNMBR.Trim() + "' je umístěn ve skladu '" + item.SKL_ID.Trim() + "' na více lokacích.",
                    //        2,
                    //        new string[] { "Přeskočit", "Přeskočit vše", "Přerušit" }
                    //        );

                    //    if (pos == 2)
                    //        return;
                    //    else if (pos == 0)
                    //        continue;
                    //    else if (pos == 1)
                    //    {
                    //        preskocitVse = true;
                    //        continue;
                    //    }
                    //}
                    //else  // odskrtnuti
                    //    item.Vybrano = false;
                }

                MessageBox.Show(string.Format("Bylo aktualizováno '{0}' záznamů", pocetZaznamu));
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1)
                {
                    if (SelectedRow != null)
                        sortedID = SelectedRow.DEX_ROW_ID.ToString();
                }
            }
            catch { }
        }

        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            try
            {
                int pos = this.bsInventuraPredloha.Find(dsInventuraPredloha.CZMST_I4.DEX_ROW_IDColumn.ColumnName, sortedID);
                this.bsInventuraPredloha.Position = pos;
            }
            catch { }
        }

        private void tsbVycistit_Click(object sender, EventArgs e)
        {
            PerformVycistitFiltr();
        }

        private void PerformVycistitFiltr()
        {
            try
            {
                comboBoxCountEntries.SelectedItem = null;
                comboBoxMaterialID.SelectedItem = null;
                comboBoxUzivatel.SelectedItem = null;
                comboBoxSKLID.SelectedItem = null;
                comboBoxLOCNCODE.SelectedItem = null;

                comboBoxCountEntries.Text = string.Empty;
                comboBoxMaterialID.Text = string.Empty;
                comboBoxUzivatel.Text = string.Empty;
                comboBoxSKLID.Text = string.Empty;
                comboBoxLOCNCODE.Text = string.Empty;

                // TODO: pamatovat si naposled zvoleny vypocet stavu??
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                IDictionary<string, string> info = new Dictionary<string, string>();

                if (bw_Import.IsBusy)
                {
                    bw_Import.CancelAsync();
                    while (bw_Import.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }

                ProgressIndicatorStart();


                string countentries = string.Empty;

                using (FormInventuraHlavickaList2 frmI = new FormInventuraHlavickaList2(false, Fask.Interfaces.Classes.ZOBRAZENI_TYP.VYBER))
                {
                    frmI.Text = "Výběr Davky";

                    if (frmI.ShowDialog(this) != DialogResult.OK)
                    {
                        ProgressIndicatorStop();
                        return;
                    }

                    countentries = frmI.SelectedRow.CountEntries.ToString().Trim();
                }

                info.Add("countentries", countentries);


                info.Add("Typ", "Pohoda");


                bw_Import.RunWorkerAsync(info);

            }
            catch (Exception ex)
            {
                ProgressIndicatorStop();
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void xMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                IDictionary<string, string> info = new Dictionary<string, string>();

                if (bw_Import.IsBusy)
                {
                    bw_Import.CancelAsync();
                    while (bw_Import.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }

                ProgressIndicatorStart();


                string countentries = string.Empty;

                using (FormInventuraHlavickaList2 frmI = new FormInventuraHlavickaList2(false, Fask.Interfaces.Classes.ZOBRAZENI_TYP.VYBER))
                {
                    frmI.Text = "Výběr Davky";

                    if (frmI.ShowDialog(this) != DialogResult.OK)
                    {
                        ProgressIndicatorStop();
                        return;
                    }

                    countentries = frmI.SelectedRow.CountEntries.ToString().Trim();
                }

                info.Add("countentries", countentries);


                string filepath = string.Empty;
                DialogResult dr3 = showPathDialog(out filepath);
                if (dr3 != System.Windows.Forms.DialogResult.OK)
                {
                    ProgressIndicatorStop();
                    return;
                }

                info.Add("filepath", filepath);


                info.Add("Typ", "XML");


                bw_Import.RunWorkerAsync(info);

            }
            catch (Exception ex)
            {
                ProgressIndicatorStop();
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DialogResult showPathDialog(out string filepath)
        {
            filepath = string.Empty;
            DialogResult dr;

            using (SaveFileDialog ofd = new SaveFileDialog())
            {
                ofd.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
                ofd.InitialDirectory = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Inventura[0].ImportPath;
                ofd.FileName = "Export.xml";
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

        private void bw_Import_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {

                IDictionary<string, string> info = e.Argument as Dictionary<string, string>;



                if (bw_Import.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }


                Fask.Interfaces.Classes.StatusInfo status = new Fask.Interfaces.Classes.StatusInfo();


                if (info["Typ"].Trim() == "Pohoda")
                {
                    if ((providerInventura != null) && (providerInventura is Fask.Interfaces.Inventura.IInventura2_ImportInventura))
                        status = ((Fask.Interfaces.Inventura.IInventura2_ImportInventura)providerInventura).ImportInventura(info["countentries"]);
                    else
                        throw new NotImplementedException("Provider neimplementuje IInventura2_ImportInventura.");
                }
                else if (info["Typ"].Trim() == "XML")
                {
                    if ((providerInventura != null) && (providerInventura is Fask.Interfaces.Inventura.IInventura2_Import_ToXML_Inventura))
                        status = ((Fask.Interfaces.Inventura.IInventura2_Import_ToXML_Inventura)providerInventura).ImportToXMLInventura(info["countentries"], info["filepath"]);
                    else
                        throw new NotImplementedException("Provider neimplementuje IInventura2_Import_ToXML_Inventura.");
                }


                if (bw_Import.CancellationPending)
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

        private void bw_Import_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
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

        #region Exporty



        private void exportDoCSVVseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                this.dgInventuraPredloha.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                this.dgInventuraPredloha.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
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

                this.dgInventuraPredloha.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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


                // TODO: prepsat ...                
                if (dgInventuraPredloha.Columns["Vybrano"].Visible)
                    this.dgInventuraPredloha.ExportToExcelInventura(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
                //dataGridView1.ExportSelectedRowsVisibleColumnsToExcelInventura(string.Empty);
                else
                    this.dgInventuraPredloha.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
                //dataGridView1.ExportSelectedRowsVisibleColumnsToExcel(string.Empty);
               
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

                dgInventuraPredloha.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                dgInventuraPredloha.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
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
                bool endcol = dgInventuraPredloha.CurrentCell.ColumnIndex + 1 >= dgInventuraPredloha.ColumnCount;
                bool endrow = dgInventuraPredloha.CurrentCell.RowIndex + 1 >= dgInventuraPredloha.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dgInventuraPredloha.CurrentCell.ColumnIndex;
                    startRow = dgInventuraPredloha.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dgInventuraPredloha.CurrentCell.ColumnIndex + 1;
                    startRow = dgInventuraPredloha.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dgInventuraPredloha.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dgInventuraPredloha.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dgInventuraPredloha.CurrentCell = c;




        }

        private void tsmiImportDBF_Click(object sender, EventArgs e)
        {
            SaveDavku();
        }

        private string rowCountEntriesText()
        {
            return rowCountEntries == null ? (string.IsNullOrEmpty(comboBoxCountEntries.Text) ? string.Empty : comboBoxCountEntries.Text) : rowCountEntries.CountEntries.ToString();
        }

        private const string inventura_dbf = "inventura.dbf";
        private void SaveDavku()
        {
            // 1. uzavrit ... s dotazem ... 

            // 2. natahnout data

            // 3. projit a modifikovat data 

            // 4. aktualizovat data 

            try
            {

                //if (rowCountEntries == null) JiS neni uplne v poradku ... oprava na rowcountetriestext()
                var strCountentries = rowCountEntriesText();
                if (String.IsNullOrEmpty(strCountentries))
                {
                    MessageBox.Show("Není vybrána dávka inventury ke zpracovnání", "Inventura", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                int intCountentries = 0;
                try
                {
                    intCountentries = int.Parse(strCountentries);
                }
                catch (Exception exIntCountentries)
                {
                    Fask.Logging.ExceptionHandler2.Handle(exIntCountentries);
                    throw new Exception("Dávka musí být číslo");
                }

                progressIndicator1.Show();
                progressIndicator1.Start();

                dataset.dsFaskInventura dsfask = new dataset.dsFaskInventura();
                dataset.dsSBKompletInventura dssb = new dataset.dsSBKompletInventura();

                OpenFileDialog ofd = new OpenFileDialog();
                if (DialogResult.Cancel == ofd.ShowDialog())
                    return;

                // vytvori soubor "inventura.dbf" z pozadovaneho
                System.IO.File.Copy(ofd.FileName, System.IO.Path.Combine(System.IO.Path.GetDirectoryName(ofd.FileName), inventura_dbf), true);

                dataset.dsFaskInventuraTableAdapters.CZMST_I1HTableAdapter tai1h = new dataset.dsFaskInventuraTableAdapters.CZMST_I1HTableAdapter();
                dataset.dsFaskInventuraTableAdapters.CZMST_I1TableAdapter tai1 = new dataset.dsFaskInventuraTableAdapters.CZMST_I1TableAdapter();
                dataset.dsFaskInventuraTableAdapters.CZMST_I4TableAdapter tai4 = new dataset.dsFaskInventuraTableAdapters.CZMST_I4TableAdapter();

                tai1h.Connection = new System.Data.SqlClient.SqlConnection(Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].FASKDB_ConnesctionString);
                tai1.Connection = tai1h.Connection;
                tai4.Connection = tai1h.Connection;

                // jis rowcountries zmena na rowcountriestext
                //tai1h.FillByCountEntries(dsfask.CZMST_I1H, rowCountEntries.CountEntries);
                //tai1.FillByCountentries(dsfask.CZMST_I1, rowCountEntries.CountEntries);
                //tai4.FillByCountentries(dsfask.CZMST_I4, rowCountEntries.CountEntries);

                tai1h.FillByCountEntries(dsfask.CZMST_I1H, intCountentries);
                tai1.FillByCountentries(dsfask.CZMST_I1, intCountentries);
                tai4.FillByCountentries(dsfask.CZMST_I4, intCountentries);

                var uzavrenaDavka = dsfask.CZMST_I1.Where(x => x.TerminalID < 100);
                if (uzavrenaDavka.Count() > 0)
                { // neni uzavrena

                    if (DialogResult.No == MessageBox.Show("Dávka dosud není uzavřena!\nUzavřít a pokračovat importem dat?", "Inventura", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                        return;

                    uzavrenaDavka.ToList().ForEach((Action<dataset.dsFaskInventura.CZMST_I1Row>)delegate (dataset.dsFaskInventura.CZMST_I1Row i1row)
                    {
                        i1row.TerminalID = 200;
                    });
                }


                dsfask.CZMST_I1H[0].State = 2; // uzavreno
                tai1h.Update(dsfask.CZMST_I1H); // uzavrit ... 
                tai1.Update(dsfask.CZMST_I1);

                dataset.dsSBKompletInventura dsinvSB = new dataset.dsSBKompletInventura();
                dataset.dsSBKompletInventuraTableAdapters.inventuraTableAdapter tainvSB = new dataset.dsSBKompletInventuraTableAdapters.inventuraTableAdapter();
                tainvSB.Connection = new System.Data.OleDb.OleDbConnection(String.Format("Provider=VFPOLEDB.1;Data Source={0}", ofd.FileName));

                tainvSB.Fill(dsinvSB.inventura);

                foreach (var i in dsinvSB.inventura)
                {
                    var zaznamyPolozky = dsfask.CZMST_I4.Where(x => x.ITEMNMBR.Trim() == i.kod.Trim());
                    if (zaznamyPolozky.Count() > 0)
                    {
                        i.tmp_inv = zaznamyPolozky.Sum(x => x.QUANTITY);
                        i.tmp_dif = i.tmp_inv - i.stav;
                    }
                }

                int updated = tainvSB.Update(dsinvSB.inventura);

                //if (System.IO.File.Exists(ofd.FileName) && DialogResult.No == MessageBox.Show("Přepsat soubor '" + ofd.FileName + "' souborem '" + inventura_dbf + "' ?", "Uložení inventurní dávky", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                //    return;
                // zpet prepise puvodni soubor z inventura.dbf
                System.IO.File.Copy(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(ofd.FileName), inventura_dbf), ofd.FileName, true);

                MessageBox.Show("Aktualizovano: " + updated.ToString() + " z " + dsinvSB.inventura.Count.ToString(), "Inventura", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "inventura", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                progressIndicator1.Stop();
                progressIndicator1.Hide();
            }
        }

    }

}
