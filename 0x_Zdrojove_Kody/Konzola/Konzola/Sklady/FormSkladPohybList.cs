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
using Konzola.Vyroba.Rozbory;
using MST_Print_Server_ZPL_Printing;

namespace Konzola.Sklady
{
    public partial class FormSkladPohybList : Form
    {
        private Fask.Interfaces.IMES providerSkladPohyb = null;
        //private Fask.Interfaces.IVyrobaKonzola providerUzivatele = null;
        private Fask.Interfaces.IMES providerSklady = null;
        private Fask.Interfaces.IMES providerTisk = null; //**DONE

        //private Fask.Interfaces.DataSets.Uzivatele dsUzivatele = new Fask.Interfaces.DataSets.Uzivatele();
        private FASK.Logins.DataSets.Pristupy.FASK_LoginsDataTable dtUzivatele = new FASK.Logins.DataSets.Pristupy.FASK_LoginsDataTable();
        private Fask.Interfaces.DataSets.Sklady dsSklady = new Fask.Interfaces.DataSets.Sklady();
        private Fask.Interfaces.DataSets.SkladPohyb.CZMST_Sklad_PohybDataTable dtPohyb = new Fask.Interfaces.DataSets.SkladPohyb.CZMST_Sklad_PohybDataTable();  // typy pohybu (momentalne distinct type na data)

        private List<Fask.Interfaces.Filtry.SkladPohybListFiltr> filtry = new List<Fask.Interfaces.Filtry.SkladPohybListFiltr>();

        /// <summary>
        /// Vybrany filtr ze seznamu filtru.
        /// </summary>
        private Fask.Interfaces.Filtry.SkladPohybListFiltr rowFiltr
        {
            get
            {
                try
                {
                    return tscbFiltry.SelectedItem as Fask.Interfaces.Filtry.SkladPohybListFiltr;
                }
                catch
                {
                    return null;
                }
            }
        }

        public Fask.Interfaces.DataSets.SkladPohyb.CZMST_Sklad_PohybRow CZMST_Sklad_Pohyb_selectedRow
        {
            get
            {
                try
                {
                    return ((DataRowView)(dgSkladPohyb.BindingContext[bsSkladPohyb].Current)).Row as Fask.Interfaces.DataSets.SkladPohyb.CZMST_Sklad_PohybRow;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        private List<Fask.Interfaces.DataSets.SkladPohyb.CZMST_Sklad_PohybRow> CZMST_Sklad_Pohyb_selectedRows
        {
            //get
            //{
            //    List<Fask.Interfaces.DataSets.SkladPohyb.CZMST_Sklad_PohybRow> rows = new List<Fask.Interfaces.DataSets.SkladPohyb.CZMST_Sklad_PohybRow>();

            //    foreach (DataGridViewRow selectedRow in dgSkladPohyb.SelectedRows)
            //    {
            //        Fask.Interfaces.DataSets.SkladPohyb.CZMST_Sklad_PohybRow row = ((DataRowView)selectedRow.DataBoundItem).Row as Fask.Interfaces.DataSets.SkladPohyb.CZMST_Sklad_PohybRow;
            //        rows.Add(row);
            //    }

            //    return rows;
            //}

            get
            {
                return dgSkladPohyb.SelectedRows
                    .Cast<DataGridViewRow>()
                    .OrderBy(r => r.Index)               // zarovnání na vizuální pořadí v gridu
                    .Select(r => ((DataRowView)r.DataBoundItem).Row as Fask.Interfaces.DataSets.SkladPohyb.CZMST_Sklad_PohybRow)
                    .Where(r => r != null)
                    .ToList();
            }
        }

        /// <summary>
        /// Vybrany checkbox ze seznamu.
        /// </summary>
        private Fask.Interfaces.Classes.ZOBRAZENI_DAT zobrazeniDat
        {
            get
            {
                try
                {
                    var choice = gbZobrazeniDat.Controls.OfType<RadioButton>().FirstOrDefault(x => x.Checked);
                    return (Fask.Interfaces.Classes.ZOBRAZENI_DAT)choice.Tag;
                }
                catch
                {
                    return Fask.Interfaces.Classes.ZOBRAZENI_DAT.Aktualni;
                }
            }
            set
            {
                try
                {
                    var choice = gbZobrazeniDat.Controls.OfType<RadioButton>().FirstOrDefault(x => ((Fask.Interfaces.Classes.ZOBRAZENI_DAT)x.Tag) == value);
                    if (choice != null)
                        choice.Checked = true;
                }
                catch
                {
                    rbZobrazeniDatAktualni.Checked = true;
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

        ///// <summary>
        ///// Zvoleny typ pohybu z comboboxu
        ///// </summary>
        //private Fask.Interfaces.DataSets.SkladPohyb.CZMST_Sklad_PohybRow rowType
        //{
        //    get
        //    {
        //        try
        //        {
        //            return comboBoxType.SelectedItem as Fask.Interfaces.DataSets.SkladPohyb.CZMST_Sklad_PohybRow;
        //        }
        //        catch
        //        {
        //            return null;
        //        }
        //    }
        //}

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

        public FormSkladPohybList()
        {
            InitializeComponent();

            this.dgSkladPohyb.UpdateColumnHeaderCellsByDatasource();
            panelButtons.Menu = menuStrip1;
        }

        private void FormSkladPohybList_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                this.WindowState = FormWindowState.Maximized;

                // načtení konfigurace datagridu z nastavení aplikace
                this.dgSkladPohyb.LoadConfiguration(this.GetType().ToString());

                panelButtons.LoadConfiguration(this.GetType().ToString());
                panelButtons.Init();
                panelButtons.Size = new Size(85, 700);

                advancedDataGridViewSearchToolBar1.SetColumns(dgSkladPohyb.Columns);

                this.rbZobrazeniDatAktualni.Tag = Fask.Interfaces.Classes.ZOBRAZENI_DAT.Aktualni;
                this.rbZobrazeniDatArchivni.Tag = Fask.Interfaces.Classes.ZOBRAZENI_DAT.Archivni;
                this.rbZobrazeniDatAktualniAArchivni.Tag = Fask.Interfaces.Classes.ZOBRAZENI_DAT.Aktualni_a_Archivni;
                //var choice = gbZobrazeniDat.Controls.OfType<RadioButton>().FirstOrDefault(x => ((Fask.Interfaces.Classes.ZOBRAZENI_DAT)x.Tag) == Settings.FormSkladPohybListZobrazeniDat);
                //if (choice != null)
                //    choice.Checked = true;
                zobrazeniDat = Konfigurace.Globals_Konfig_Konzola.FormSkladPohybListZobrazeniDat;

                if (!Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady[0].PodledyNaDataArchivniPovolit)
                {
                    zobrazeniDat = Fask.Interfaces.Classes.ZOBRAZENI_DAT.Aktualni;
                    gbZobrazeniDat.Enabled = false;
                    gbZobrazeniDat.Visible = false;
                }

                // nacteni filtru z konfigurace aplikace
                this.filtry = Extensions.ListExt.ReadFromXML<Fask.Interfaces.Filtry.SkladPohybListFiltr>(this.GetType().ToString() + ".filtr");
                tscbFiltry.ComboBox.DataSource = this.filtry;
                tscbFiltry.SelectedItem = null;
                tscbFiltry.ComboBox.DropDownWitdhAutosize();

                // inicializace providera
                InitProvider();

                if (providerSkladPohyb == null)
                    throw new Exception("Provider 'SkladPohyb' není inicializován");

                if (providerSklady == null)
                    throw new Exception("Provider 'Sklady' není inicializován");

                //if (providerUzivatele == null)
                //    throw new Exception("Provider 'Uživatelé' není inicializován");

                //System.Threading.Thread loadThread = new System.Threading.Thread(LoadDataAsync);
                //loadThread.IsBackground = true;
                //loadThread.Start();

                // nastavení času
                //dateTimePickerDatumDo.Value = dateTimePickerDatumOd.Value = DateTime.Now.AddSeconds(-DateTime.Now.Second);
                // nastavení času (zacatek a konec dne)
                dateTimePickerDatumOd.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                dateTimePickerDatumDo.Value = DateTime.Now.AddDays(1).Date.AddSeconds(-1);
                dateTimePickerDatumDo.Checked = false;
                dateTimePickerDatumOd.Checked = false;

                buttonVyhledat.Focus();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormSkladPohybList_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.dgSkladPohyb.SaveConfiguration(this.GetType().ToString());
                panelButtons.SaveConfiguration(this.GetType().ToString());

                // cekani na dobehnuti vlakna
                try
                {
                    if (bwLoadSkladPohyb.IsBusy)
                    {
                        bwLoadSkladPohyb.CancelAsync();
                        while (bwLoadSkladPohyb.IsBusy)
                        {
                            Application.DoEvents();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                }

                // ulozeni zvoleneho radiobuttonu
                Konfigurace.Globals_Konfig_Konzola.FormSkladPohybListZobrazeniDat = zobrazeniDat;
                this.filtry.WriteXML(this.GetType().ToString() + ".filtr");
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormSkladPohybList_KeyDown(object sender, KeyEventArgs e)
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

        private void FormSkladPohybList_Shown(object sender, EventArgs e)
        {
            try
            {
                this.progressIndicator1.Size = new Size(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ostatni[0].ProgressIndicatorSize, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ostatni[0].ProgressIndicatorSize);
                this.progressIndicator1.Location = new Point(this.dgSkladPohyb.Location.X + (this.dgSkladPohyb.Width / 2) - (progressIndicator1.Size.Width / 2), this.dgSkladPohyb.Location.Y + (this.dgSkladPohyb.Height / 2) - (progressIndicator1.Size.Height / 2));
            }
            catch { }
        }

        #endregion


        private void LoadDataAsync()
        {
            try
            {
                Fask.Interfaces.DataSets.Sklady dsSkladyData = new Fask.Interfaces.DataSets.Sklady();
                FASK.Logins.DataSets.Pristupy.FASK_LoginsDataTable dtUzivateleData = new FASK.Logins.DataSets.Pristupy.FASK_LoginsDataTable();
                Fask.Interfaces.DataSets.SkladPohyb.CZMST_Sklad_PohybDataTable dtPohybData = new Fask.Interfaces.DataSets.SkladPohyb.CZMST_Sklad_PohybDataTable();

                // naplneni ciselniku skladu
                //dsSkladyData = ((Fask.Interfaces.Ciselniky.ISklady)providerSklady).GetSklady();


                if ((providerSklady != null) && (providerSklady is Fask.Interfaces.Ciselniky.Sklady.ISklady2_GetSklady))
                    dsSkladyData = ((Fask.Interfaces.Ciselniky.Sklady.ISklady2_GetSklady)providerSklady).GetSklady();
                else
                    throw new NotImplementedException("Provider neimplementuje ISklady2_GetSklady.");

                // naplneni ciselniku uzivatelu
                //dsUzivateleData = ((Fask.Interfaces.Ciselniky.IUzivatele)providerUzivatele).GetUzivatele();



                //if ((providerUzivatele != null) && (providerUzivatele is Fask.Interfaces.Ciselniky.IUzivatele2_GetUzivatele))
                //    dsUzivateleData = ((Fask.Interfaces.Ciselniky.IUzivatele2_GetUzivatele)providerUzivatele).GetUzivatele();
                //else
                //    throw new NotImplementedException("Provider neimplementuje IUzivatele2_GetUzivatele.");

                dtUzivateleData = FASK.Logins.Uzivatel.Instance.Komunikace.GetLogins();


                // zjisteni nazvu tabulky/pohledu podle typu dat ...
                var choice = gbZobrazeniDat.Controls.OfType<RadioButton>().FirstOrDefault(x => x.Checked);
                Fask.Interfaces.Classes.ZOBRAZENI_DAT choiceAsEnum = (Fask.Interfaces.Classes.ZOBRAZENI_DAT)choice.Tag;
                string tablename = string.Empty;
                switch (choiceAsEnum)
                {
                    case Fask.Interfaces.Classes.ZOBRAZENI_DAT.Aktualni:
                        tablename = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady[0].PohledAktualni;
                        break;
                    case Fask.Interfaces.Classes.ZOBRAZENI_DAT.Archivni:
                        tablename = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady[0].PohledArchivni;
                        break;
                    case Fask.Interfaces.Classes.ZOBRAZENI_DAT.Aktualni_a_Archivni:
                        tablename = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady[0].PohledAktualniAArchivni;
                        break;
                    default:
                        break;
                }

                //dtPohybData = ((Fask.Interfaces.SkladPohyb.ISkladPohyb)providerSkladPohyb).GetTypPohybu(tablename);

                if ((providerSkladPohyb != null) && (providerSkladPohyb is Fask.Interfaces.SkladPohyb.ISkladPohyb2_GetTypPohybu))
                    dtPohybData = ((Fask.Interfaces.SkladPohyb.ISkladPohyb2_GetTypPohybu)providerSkladPohyb).GetTypPohybu(tablename);
                else
                    throw new NotImplementedException("Provider neimplementuje ISkladPohyb2_GetTypPohybu.");




                // navrat do hlavniho vlakna
                if (InvokeRequired)
                {
                    BeginInvoke(new Action(() =>
                    {
                        PopulateUI(dtUzivateleData, dsSkladyData, dtPohybData);
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

        private void PopulateUI(FASK.Logins.DataSets.Pristupy.FASK_LoginsDataTable dtUzivateleData, Fask.Interfaces.DataSets.Sklady dsSkladyData, Fask.Interfaces.DataSets.SkladPohyb.CZMST_Sklad_PohybDataTable dtPohybData)
        {
            try
            {
                // naplneni comboboxu uzivatelu
                this.dtUzivatele = dtUzivateleData;
                comboBoxUzivatel.Items.AddRange(dtUzivateleData.Select(null, "surname asc"));
                comboBoxUzivatel.SelectedItem = null;

                // naplneni comboboxu skladu
                this.dsSklady = dsSkladyData;
                comboBoxSKLID.Items.AddRange(dsSkladyData.CZMST093.Select(null, "skl_desc asc"));
                comboBoxSKLID.SelectedItem = null;

                this.dtPohyb = dtPohybData;
                //comboBoxType.Items.AddRange(dtPohybData.Select(null, "Type asc"));
                //comboBoxType.ValueMember = dtPohybData.TypeColumn.ColumnName;
                //comboBoxType.SelectedItem = null;
                // naplneni lokaci
                //var dtMaterialLOCNCODE = ds.get_os_ms.GroupBy(g => g.LOCNCODE).Select(s => s.First()).ToList(); //.ukolovaniDataset1.CZ_UKOL.GroupBy(g => g.Priority).Select(s => s.First()).ToList();
                //var dtMaterialLOCNCODE2 = ds.get_os_ms.GroupBy(g => g.LOCNCODE).Select(s => s.First()).ToList(); //.ukolovaniDataset1.CZ_UKOL.GroupBy(g => g.Priority).Select(s => s.First()).ToList();
                //foreach (var item in dtMaterialLOCNCODE)
                //{
                //    item.LOCNCODE = item.LOCNCODE.Trim();
                //}
                //comboBoxMaterialLOCNCODESRC.DataSource = dtMaterialLOCNCODE;
                //comboBoxMaterialLOCNCODESRC.ValueMember = "LOCNCODE";
                //comboBoxMaterialLOCNCODESRC.SelectedItem = null;
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
            try
            {
                if (!String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                {
                    if (providerSkladPohyb == null) //inicializace se provede pouze pokud nebyla provedena ... 
                    {
                        Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                        Type[] types = providerAssemlby.GetTypes();
                        foreach (Type t in types)
                        {
                            try
                            {
                                if (typeof(Fask.Interfaces.SkladPohyb.ISkladPohyb2).IsAssignableFrom(t))
                                {
                                    providerSkladPohyb = (Fask.Interfaces.SkladPohyb.ISkladPohyb2)providerAssemlby.CreateInstance(t.FullName);
                                    if (providerSkladPohyb != null)
                                        break;
                                }
                            }
                            catch { }
                        }
                        //return config;
                    }

                    providerSkladPohyb.InitProvider();

                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                if (!String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
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

        private void buttonVyhledat_Click(object sender, EventArgs e)
        {
            #region prace s datagridem - nastaveni numeric hodnot na pocet desetinnych mist
            try
            {
                // Procházení sloupců v DataGridView
                foreach (DataGridViewColumn column in dgSkladPohyb.Columns)
                {
                    // Získání typu dat pro sloupec
                    Type dataType = null;

                    if (dgSkladPohyb.DataSource is DataTable dataTable)
                    {
                        // Pokud je datový zdroj DataTable
                        if (dataTable.Columns.Contains(column.DataPropertyName))
                        {
                            dataType = dataTable.Columns[column.DataPropertyName].DataType;
                        }
                    }
                    else if (dgSkladPohyb.DataSource is BindingSource bindingSource)
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

        private bool CreateFilter(ref Fask.Interfaces.Filtry.SkladPohybListFiltr filtr)
        {
            if(filtr == null)
                filtr = new Fask.Interfaces.Filtry.SkladPohybListFiltr();

            filtr.rowUzivatel = rowUzivatel != null ? rowUzivatel.USERID : string.Empty;
            filtr.UzivatelID = comboBoxUzivatel.Text.Trim();
            filtr.MaterialID = comboBoxMaterialID.Text.Trim();
            filtr.MaterialITEMCODE = comboBoxMaterialITEMCODE.Text.Trim();
            filtr.MaterialSERLTNUM = comboBoxSerltnum.Text.Trim();
            filtr.DocumentNumber = comboBoxDocumentNumber.Text.Trim();

            //filtr.rowType = rowType != null ? rowType.Type : string.Empty;
            //filtr.PohybType = comboBoxType.Text.Trim();

            filtr.PohybType = tB_typ_pohybu.Text.Trim();
            filtr.TypPolozky = tB_typ_polozky.Text.Trim();

            filtr.cbSelectedMaterialSKLID = rowSKLID != null ? rowSKLID.skl_id : string.Empty;
            filtr.MaterialSKLID = comboBoxSKLID.Text.Trim();
            filtr.MaterialLocncode = comboBoxLOCNCODE.Text.Trim();
            filtr.DatumOd = dateTimePickerDatumOd.Checked ? dateTimePickerDatumOd.Value : (DateTime?)null;
            filtr.DatumDo = dateTimePickerDatumDo.Checked ? dateTimePickerDatumDo.Value : (DateTime?)null;
            filtr.ZobrazeniDat = zobrazeniDat;
            filtr.MaterialPracID = cbPracID.Text.Trim();
            filtr.Win_data = chB_Win_data.Checked;
            return true;
        }

        private void PerformOK()
        {
            try
            {
                DataTable dtchanged = this.dsSkladPohyb.CZMST_Sklad_Pohyb.GetChanges();
                if (dtchanged != null && dtchanged.Rows.Count > 0) //obsahuje zmeny ... 
                {
                    DialogResult dr = MessageBox.Show("Obsahuje změny, pokračovat?", "data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == System.Windows.Forms.DialogResult.No)
                        return;
                }

                if (bwLoadSkladPohyb.IsBusy)
                {
                    bwLoadSkladPohyb.CancelAsync();
                    while (bwLoadSkladPohyb.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }

                ProgressIndicatorStart();

                Fask.Interfaces.Filtry.SkladPohybListFiltr filtr = new Fask.Interfaces.Filtry.SkladPohybListFiltr();
                if (!CreateFilter(ref filtr))
                    return;
                
                int FirstDisplayedScrollingRowIndex = this.dgSkladPohyb.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

                bwLoadSkladPohyb.RunWorkerAsync(filtr);
                
                if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dgSkladPohyb.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dgSkladPohyb.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index
            }
            catch (Exception ex)
            {
                ProgressIndicatorStop();
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.dgSkladPohyb.Focus();
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
                this.dgSkladPohyb.SelectAll();
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
                this.dgSkladPohyb.ClearSelection();
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
                Fask.Interfaces.Filtry.SkladPohybListFiltr filtr = (Fask.Interfaces.Filtry.SkladPohybListFiltr)e.Argument;
                Fask.Interfaces.DataSets.SkladPohyb ds = new Fask.Interfaces.DataSets.SkladPohyb();

                if (bwLoadSkladPohyb.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                
                // zjisteni nazvu tabulky/pohledu podle typu dat ...
                string tablename = string.Empty;
                switch (filtr.ZobrazeniDat)
                {
                    case Fask.Interfaces.Classes.ZOBRAZENI_DAT.Aktualni:
                        tablename = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady[0].PohledAktualni;
                        break;
                    case Fask.Interfaces.Classes.ZOBRAZENI_DAT.Archivni:
                        tablename = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady[0].PohledArchivni;
                        break;
                    case Fask.Interfaces.Classes.ZOBRAZENI_DAT.Aktualni_a_Archivni:
                        tablename = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady[0].PohledAktualniAArchivni;
                        break;
                    default:
                        break;
                }

                // nacteni dat v oddelenem vlakne
                //ds = ((Fask.Interfaces.SkladPohyb.ISkladPohyb)providerSkladPohyb).GetFiltrovanySkladLokace(filtr, tablename);


                if ((providerSkladPohyb != null) && (providerSkladPohyb is Fask.Interfaces.SkladPohyb.ISkladPohyb2_GetFiltrovanySkladLokace))
                    ds = ((Fask.Interfaces.SkladPohyb.ISkladPohyb2_GetFiltrovanySkladLokace)providerSkladPohyb).GetFiltrovanySkladLokace(filtr, tablename);
                else
                    throw new NotImplementedException("Provider neimplementuje ISkladPohyb2_GetFiltrovanySkladLokace.");





                
                if (bwLoadSkladPohyb.CancellationPending)
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

        private void bwSkladPohyb_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                // check error, check cancel, then use result
                if (e.Error != null)
                {
                    // handle the error
                    dsSkladPohyb = new Fask.Interfaces.DataSets.SkladPohyb();

                    bsSkladPohyb.DataSource = dsSkladPohyb;
                }
                else if (e.Cancelled)
                {
                    // handle cancellation
                    dsSkladPohyb = new Fask.Interfaces.DataSets.SkladPohyb();

                    bsSkladPohyb.DataSource = dsSkladPohyb;
                }
                else
                {
                    // uspesne dokonceno ...
                    // use it on the UI thread
                    dsSkladPohyb = (Fask.Interfaces.DataSets.SkladPohyb)e.Result;
                    if (dsSkladPohyb == null)
                        dsSkladPohyb = new Fask.Interfaces.DataSets.SkladPohyb();

                    bsSkladPohyb.DataSource = dsSkladPohyb;
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
                this.progressIndicator1.Location = new Point(this.dgSkladPohyb.Location.X + (this.dgSkladPohyb.Width / 2) - (progressIndicator1.Size.Width / 2), this.dgSkladPohyb.Location.Y + (this.dgSkladPohyb.Height / 2) - (progressIndicator1.Size.Height / 2));
            }
            catch { }
            progressIndicator1.Start();
            progressIndicator1.Visible = true;
        }

        private void tsbNastavit_Click(object sender, EventArgs e)
        {
            PerformNastavitFiltr(rowFiltr);
        }

        /// <summary>
        /// Nastavi zvoleny filtr do comboboxu, ...
        /// </summary>
        /// <param name="filtr"></param>
        private void PerformNastavitFiltr(Fask.Interfaces.Filtry.SkladPohybListFiltr filtr)
        {
            try
            {
                if (filtr == null)
                    return;

                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                PerformVycistitFiltr();

                // uzivatel
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
                
                // itemnmbr
                comboBoxMaterialID.Text = filtr.MaterialID;

                // itemcode
                comboBoxMaterialITEMCODE.Text = filtr.MaterialITEMCODE;

                // serltnum
                comboBoxSerltnum.Text = filtr.MaterialSERLTNUM;

                //PracID
                cbPracID.Text = filtr.MaterialPracID;

                // document number
                comboBoxDocumentNumber.Text = filtr.DocumentNumber;

                //// type
                //comboBoxType.Text = filtr.rowType;

                // type
                tB_typ_pohybu.Text = filtr.PohybType;

                // typ polozky
                tB_typ_polozky.Text = filtr.TypPolozky;

                // sklad
                if (string.IsNullOrEmpty(filtr.cbSelectedMaterialSKLID))
                {
                    // uzivatel nevyplnen, doplnit pouze text ...
                    comboBoxSKLID.Text = filtr.MaterialSKLID;
                }
                else
                {
                    // uzivatel vyplnen, pokusit se dohledat
                    var sklady = dsSklady.CZMST093.Where(x => x.skl_id == filtr.cbSelectedMaterialSKLID);
                    if (sklady.Count() > 0)
                        comboBoxSKLID.SelectedItem = sklady.First();
                    else
                        comboBoxSKLID.Text = filtr.cbSelectedMaterialSKLID;
                }

                // locncode
                comboBoxLOCNCODE.Text = filtr.MaterialLocncode;

                // datum od
                if (filtr.DatumOd != null)
                {
                    dateTimePickerDatumOd.Value = filtr.DatumOd.Value;
                    dateTimePickerDatumOd.Checked= true;
                }
                else
                    dateTimePickerDatumOd.Checked = false;

                // datum do
                if (filtr.DatumDo != null)
                {
                    dateTimePickerDatumDo.Value = filtr.DatumDo.Value;
                    dateTimePickerDatumDo.Checked = true;
                }
                else
                    dateTimePickerDatumDo.Checked = false;

                this.zobrazeniDat = filtr.ZobrazeniDat;
                //var choice = gbVypocetStavu.Controls.OfType<RadioButton>().FirstOrDefault(x => ((Fask.Interfaces.Classes.VYPOCET_STAVU)x.Tag) == filtr.FiltrStav);
                //if (choice != null)
                //    choice.Checked = true;

                //Fask.Interfaces.Classes.VYPOCET_STAVU choiceAsEnum = (Fask.Interfaces.Classes.VYPOCET_STAVU)choice.Tag;
                //filtr.FiltrStav = choiceAsEnum;

                // lokace
                comboBoxLOCNCODE.Text = filtr.MaterialLocncode;

                chB_Win_data.Checked = filtr.Win_data;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show("Nepodařilo se načíst data filtru.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                Fask.Interfaces.Filtry.SkladPohybListFiltr filtr = new Fask.Interfaces.Filtry.SkladPohybListFiltr();
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

                Fask.Interfaces.Filtry.SkladPohybListFiltr filtr = rowFiltr;

                if (!CreateFilter(ref filtr))
                    return;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show("Nepodařilo se upravit filtr.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                comboBoxUzivatel.SelectedItem = null;
                comboBoxMaterialID.SelectedItem = null;
                comboBoxMaterialITEMCODE.SelectedItem = null;
                comboBoxSerltnum.SelectedItem = null;
                comboBoxDocumentNumber.SelectedItem = null;

                //comboBoxType.SelectedItem = null;

                comboBoxSKLID.SelectedItem = null;

                comboBoxLOCNCODE.SelectedItem = null;
                cbPracID.SelectedItem = null;

                comboBoxUzivatel.Text = string.Empty;
                comboBoxMaterialID.Text = string.Empty;
                comboBoxMaterialITEMCODE.Text = string.Empty;
                comboBoxSerltnum.Text = string.Empty;
                comboBoxDocumentNumber.Text = string.Empty;

                //comboBoxType.Text = string.Empty;

                comboBoxSKLID.Text = string.Empty;
                comboBoxLOCNCODE.Text = string.Empty;
                cbPracID.Text = string.Empty;

                dateTimePickerDatumOd.Checked = false;
                dateTimePickerDatumDo.Checked = false;
                chB_Win_data.Checked = false;

                tB_typ_pohybu.Text = string.Empty;
                tB_typ_polozky.Text = string.Empty;

                zobrazeniDat = Konfigurace.Globals_Konfig_Konzola.FormSkladPohybListZobrazeniDat;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #region Exporty



        private void exportDoCSVVseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                this.dgSkladPohyb.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                this.dgSkladPohyb.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
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

                this.dgSkladPohyb.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                dgSkladPohyb.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
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

                dgSkladPohyb.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                dgSkladPohyb.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
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
                bool endcol = dgSkladPohyb.CurrentCell.ColumnIndex + 1 >= dgSkladPohyb.ColumnCount;
                bool endrow = dgSkladPohyb.CurrentCell.RowIndex + 1 >= dgSkladPohyb.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dgSkladPohyb.CurrentCell.ColumnIndex;
                    startRow = dgSkladPohyb.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dgSkladPohyb.CurrentCell.ColumnIndex + 1;
                    startRow = dgSkladPohyb.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dgSkladPohyb.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dgSkladPohyb.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dgSkladPohyb.CurrentCell = c;






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

                if (CZMST_Sklad_Pohyb_selectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if (CZMST_Sklad_Pohyb_selectedRows.Count > 1)
                {
                    //MessageBox.Show("Je vybráno více záznamů než jeden.", this.Text, MessageBoxButtons.OK);
                    //return;
                }

                Fask.Interfaces.DataSets.SkladPohyb.CZMST_Sklad_PohybDataTable dt = new Fask.Interfaces.DataSets.SkladPohyb.CZMST_Sklad_PohybDataTable();

                foreach (Fask.Interfaces.DataSets.SkladPohyb.CZMST_Sklad_PohybRow row in CZMST_Sklad_Pohyb_selectedRows)
                {
                    try
                    {
                        Fask.Interfaces.DataSets.SkladPohyb.CZMST_Sklad_PohybRow newRow = dt.NewCZMST_Sklad_PohybRow();
                        // Ošetřit delší vstupní pole než počet sloupců tabulky
                        int copyLength = Math.Min(row.ItemArray.Length, dt.Columns.Count);
                        newRow.ItemArray = row.ItemArray.Take(copyLength).ToArray();
                        dt.AddCZMST_Sklad_PohybRow(newRow);
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


        private void PrintReport(Fask.Interfaces.DataSets.SkladPohyb.CZMST_Sklad_PohybDataTable dt)
        {
            try
            {

                if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                {
                    foreach (var item in dt)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start rozbory Sklad_Pohyb TISK rdlc-------------------------------------");

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

                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END rozbory Sklad_Pohyb prijmu TISK rdlc-------------------------------------");
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

                if (CZMST_Sklad_Pohyb_selectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                #region MaR 6.11.2024 vypis zpl do souboru selected rows
                int index_zaznamu = 0;

                foreach (var item in CZMST_Sklad_Pohyb_selectedRows)
                {

                    if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start sklady/rozbory/pohyby TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");

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

                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END sklady/rozbory/pohyby TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");


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



                    foreach (var item in CZMST_Sklad_Pohyb_selectedRows)
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
                    Tisk_selected_rows_Primy_Tisk(plr_Path, true, MN_ToTisk, CZMST_Sklad_Pohyb_selectedRows, NazevVychoziTiskarny);

                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Tisk_selected_rows_Primy_Tisk(string plr_Path, bool MnozstvuAutoJedna, int MN_ToTisk, List<Fask.Interfaces.DataSets.SkladPohyb.CZMST_Sklad_PohybRow> FASK_ZASOBY_selected_Rows, string nazevVychTiskarny)
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

                if (CZMST_Sklad_Pohyb_selectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                //if (CZMST_Sklad_Pohyb_selectedRows.Count > 1)
                //{
                //    //MessageBox.Show("Je vybráno více záznamů než jeden.", this.Text, MessageBoxButtons.OK);
                //    //return;
                //}

                Fask.Interfaces.DataSets.SkladPohyb.CZMST_Sklad_PohybDataTable dt = new Fask.Interfaces.DataSets.SkladPohyb.CZMST_Sklad_PohybDataTable();

                foreach (Fask.Interfaces.DataSets.SkladPohyb.CZMST_Sklad_PohybRow row in CZMST_Sklad_Pohyb_selectedRows)
                {
                    try
                    {
                        Fask.Interfaces.DataSets.SkladPohyb.CZMST_Sklad_PohybRow newRow = dt.NewCZMST_Sklad_PohybRow();
                        // Ošetřit delší vstupní pole než počet sloupců tabulky
                        int copyLength = Math.Min(row.ItemArray.Length, dt.Columns.Count);
                        newRow.ItemArray = row.ItemArray.Take(copyLength).ToArray();
                        dt.AddCZMST_Sklad_PohybRow(newRow);
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

        private void PrintReport_RDLC(Fask.Interfaces.DataSets.SkladPohyb.CZMST_Sklad_PohybDataTable dt, bool primyTisk, string path_sablona, string nazevVychTiskarny, int pocetVytisku)
        {
            try
            {

                #region logovani tisku
                if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                {
                    foreach (var item in dt)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start sklady/rozbory/pohyby TISK rdlc-------------------------------------");

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

                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END sklady/rozbory/pohyby TISK rdlc-------------------------------------");
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

                if (CZMST_Sklad_Pohyb_selectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                #region MaR 6.11.2024 vypis zpl do souboru selected rows
                int index_zaznamu = 0;

                foreach (var item in CZMST_Sklad_Pohyb_selectedRows)
                {

                    if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start sklady/rozbory/pohyby TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");

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

                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END sklady/rozbory/pohyby TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");


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



                    foreach (var item in CZMST_Sklad_Pohyb_selectedRows)
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
                    Tisk_selected_rows(plr_Path, true, MN_ToTisk, CZMST_Sklad_Pohyb_selectedRows);

                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Tisk_selected_rows(string plr_Path, bool MnozstvuAutoJedna, int MN_ToTisk, List<Fask.Interfaces.DataSets.SkladPohyb.CZMST_Sklad_PohybRow> FASK_ZASOBY_selected_Rows)
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
            Fask.Interfaces.DataSets.SkladPohyb.CZMST_Sklad_PohybRow row,
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

                Fask.Interfaces.DataSets.SkladPohyb.CZMST_Sklad_PohybDataTable dt = new Fask.Interfaces.DataSets.SkladPohyb.CZMST_Sklad_PohybDataTable();

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

        private Dictionary<string, string> PrepareDataToTisk(Fask.Interfaces.DataSets.SkladPohyb.CZMST_Sklad_PohybRow productionRow_data)
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

                        Fask.Interfaces.DataSets.SkladPohyb.CZMST_Sklad_PohybDataTable dt = new Fask.Interfaces.DataSets.SkladPohyb.CZMST_Sklad_PohybDataTable();


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
