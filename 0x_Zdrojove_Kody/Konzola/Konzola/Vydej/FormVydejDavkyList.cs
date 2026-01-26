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

namespace Konzola.Vydej
{
    public partial class FormVydejDavkyList : Form
    {
        private Fask.Interfaces.IMES providerVydej = null;
        //private Fask.Interfaces.IVyrobaKonzola providerUzivatele = null;

        private List<Fask.Interfaces.Filtry.VydejHlavickyFiltr> filtry = new List<Fask.Interfaces.Filtry.VydejHlavickyFiltr>();
        
        /// <summary>
        /// Zvolené id před seřazením
        /// </summary>
        string sortedID = string.Empty;

        public Fask.Interfaces.DataSets.Vydej.HlavickyRow SelectedRow
        {
            get
            {
                try
                {
                    return ((DataRowView)(dgVydej.BindingContext[bsVydej].Current)).Row as Fask.Interfaces.DataSets.Vydej.HlavickyRow;
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
        private Fask.Interfaces.Filtry.VydejHlavickyFiltr rowFiltr
        {
            get
            {
                try
                {
                    return tscbFiltry.SelectedItem as Fask.Interfaces.Filtry.VydejHlavickyFiltr;
                }
                catch
                {
                    return null;
                }
            }
        }

        #region Eventy formu

        public FormVydejDavkyList()
        {
            InitializeComponent();
            this.dgVydej.UpdateColumnHeaderCellsByDatasource();
            panelButtons.Menu = menuStrip1;
        }

        private void FormVydejDavkyList_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                this.WindowState = FormWindowState.Maximized;

                // načtení konfigurace datagridu z nastavení aplikace
                this.dgVydej.LoadConfiguration(this.GetType().ToString());

                panelButtons.LoadConfiguration(this.GetType().ToString());
                panelButtons.Init();
                panelButtons.Size = new Size(85, 700);

                advancedDataGridViewSearchToolBar1.SetColumns(dgVydej.Columns);

                //// načtení konfigurace vytvořených filtrů
                this.filtry = Extensions.ListExt.ReadFromXML<Fask.Interfaces.Filtry.VydejHlavickyFiltr>(this.GetType().ToString() + ".filtr");
                tscbFiltry.ComboBox.DataSource = this.filtry;
                tscbFiltry.SelectedItem = null;
                tscbFiltry.ComboBox.DropDownWitdhAutosize();

                // inicializace providera
                InitProvider();

                if (providerVydej == null)
                    throw new Exception("Provider 'Vydej' není inicializován");

                //if (providerUzivatele == null)
                //    throw new Exception("Provider 'Uživatelé' není inicializován");

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

        private void FormVydejDavkyList_Shown(object sender, EventArgs e)
        {
            try
            {
                this.progressIndicator1.Size = new Size(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ostatni[0].ProgressIndicatorSize, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ostatni[0].ProgressIndicatorSize);
                this.progressIndicator1.Location = new Point(this.dgVydej.Location.X + (this.dgVydej.Width / 2) - (progressIndicator1.Size.Width / 2), this.dgVydej.Location.Y + (this.dgVydej.Height / 2) - (progressIndicator1.Size.Height / 2));
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

        private void FormVydejDavkyList_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.dgVydej.SaveConfiguration(this.GetType().ToString());
                panelButtons.SaveConfiguration(this.GetType().ToString());

                // ulozeni vsech filtru
                this.filtry.WriteXML(this.GetType().ToString() + ".filtr");
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormVydejDavkyList_KeyDown(object sender, KeyEventArgs e)
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

        #endregion


        //private void LoadDataAsync()
        //{
        //    try
        //    {
        //        Fask.Interfaces.DataSets.Vydej dsVydejData = new Fask.Interfaces.DataSets.Vydej();
                
        //        // naplneni hlavicek
        //        dsVydejData = providerVydej.GetHlavicky(); ;

        //        // navrat do hlavniho vlakna
        //        if (InvokeRequired)
        //        {
        //            BeginInvoke(new Action(() =>
        //            {
        //                PopulateUI(dsVydejData);
        //            }));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (InvokeRequired)
        //        {
        //            BeginInvoke(new Action(() =>
        //            {
        //                ProgressIndicatorStop();
        //                Fask.Logging.ExceptionHandler2.Handle(ex, "LoadDataAsync");
        //                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            }));
        //        }
        //    }
        //}

        //private void PopulateUI(Fask.Interfaces.DataSets.Vydej dsVydejData)
        //{
        //    try
        //    {
        //        // naplneni hlavicek
        //        this.dsVydej = dsVydejData;
        //        this.bsVydej.DataSource = this.dsVydej;
        //    }
        //    catch (Exception ex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle(ex, "PopulateUI");
        //        MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    finally
        //    {
        //        ProgressIndicatorStop();
        //    }
        //}

        /// <summary>
        /// Inicializace providera
        /// </summary>
        private void InitProvider()
        {
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

                //if (providerUzivatele == null) //inicializace se provede pouze pokud nebyla provedena ... 
                //{
                //    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                //    Type[] types = providerAssemlby.GetTypes();
                //    foreach (Type t in types)
                //    {
                //        try
                //        {
                //            if (typeof(Fask.Interfaces.Ciselniky.IUzivatele2).IsAssignableFrom(t))
                //            {
                //                providerUzivatele = (Fask.Interfaces.Ciselniky.IUzivatele2)providerAssemlby.CreateInstance(t.FullName);
                //                if (providerUzivatele != null)
                //                    break;
                //            }
                //        }
                //        catch { }
                //    }
                //    //return config;
                //}

                // nastaveni connection stringu
                //if (providerUzivatele != null)
                //    ((Fask.Interfaces.Ciselniky.IUzivatele)providerUzivatele).ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;

                //if ((providerUzivatele != null) && (providerUzivatele is Fask.Interfaces.Parametry.IParametry2_ConnectionString))
                //    ((Fask.Interfaces.Parametry.IParametry2_ConnectionString)providerUzivatele).ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;


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
                foreach (DataGridViewColumn column in dgVydej.Columns)
                {
                    // Získání typu dat pro sloupec
                    Type dataType = null;

                    if (dgVydej.DataSource is DataTable dataTable)
                    {
                        // Pokud je datový zdroj DataTable
                        if (dataTable.Columns.Contains(column.DataPropertyName))
                        {
                            dataType = dataTable.Columns[column.DataPropertyName].DataType;
                        }
                    }
                    else if (dgVydej.DataSource is BindingSource bindingSource)
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

        /// <summary>
        /// Vytvoreni filtru, ktery se pouzije.
        /// </summary>
        /// <param name="filtr"></param>
        /// <returns></returns>
        private bool CreateFilter(ref Fask.Interfaces.Filtry.VydejHlavickyFiltr filtr)
        {
            if (filtr == null)
                filtr = new Fask.Interfaces.Filtry.VydejHlavickyFiltr();

            filtr.CountEntries = cbCountEntries.Text.Trim();
            filtr.Sopnumbe = cbSopnumbe.Text.Trim();
            filtr.Priority = cbPriority.Text.Trim();
            filtr.ZobrazitPouzeNestazeneDavky = cbFiltrNestazeneDavky.Checked;

            return true;
        }

        private void PerformOK()
        {
            try
            {
                DataTable dtchanged = this.dsVydej.Hlavicky.GetChanges();
                if (dtchanged != null && dtchanged.Rows.Count > 0) //obsahuje zmeny ... 
                {
                    DialogResult dr = MessageBox.Show("Obsahuje změny, pokračovat?", "data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == System.Windows.Forms.DialogResult.No)
                        return;
                }

                if (bwLoadVydejHlavicky.IsBusy)
                {
                    bwLoadVydejHlavicky.CancelAsync();
                    while (bwLoadVydejHlavicky.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }

                ProgressIndicatorStart();

                Fask.Interfaces.Filtry.VydejHlavickyFiltr filtr = new Fask.Interfaces.Filtry.VydejHlavickyFiltr();
                if (!CreateFilter(ref filtr))
                    return;

                int FirstDisplayedScrollingRowIndex = this.dgVydej.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

                bwLoadVydejHlavicky.RunWorkerAsync(filtr);

                if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dgVydej.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dgVydej.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index
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
                this.dgVydej.SelectAll();
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
                this.dgVydej.ClearSelection();
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
                Fask.Interfaces.Filtry.VydejHlavickyFiltr filtr = (Fask.Interfaces.Filtry.VydejHlavickyFiltr)e.Argument;
                Fask.Interfaces.DataSets.Vydej ds = new Fask.Interfaces.DataSets.Vydej();

                if (bwLoadVydejHlavicky.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                if ((providerVydej != null) && (providerVydej is Fask.Interfaces.Vydej.IVydej2_GetFiltrovaneHlavicky))
                    ds = ((Fask.Interfaces.Vydej.IVydej2_GetFiltrovaneHlavicky)providerVydej).GetFiltrovaneHlavicky(filtr);
                else
                    throw new NotImplementedException("Provider neobsahuje implemetaci IVydej2_GetFiltrovaneHlavicky");


                // nacteni dat v oddelenem vlakne
               //ds = ((Fask.Interfaces.Vydej.IVydej)providerVydej).GetFiltrovaneHlavicky(filtr);
                if (bwLoadVydejHlavicky.CancellationPending)
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
                    dsVydej = new Fask.Interfaces.DataSets.Vydej();
                    bsVydej.DataSource = dsVydej;
                }
                else if (e.Cancelled)
                {
                    // handle cancellation
                    // TODO: maji se vymazat data z datasetu?? asi ano ...
                    //if (dsSkladPohyb == null)
                    dsVydej = new Fask.Interfaces.DataSets.Vydej();
                    bsVydej.DataSource = dsVydej;
                }
                else
                {
                    // uspesne dokonceno ...
                    // use it on the UI thread
                    dsVydej= (Fask.Interfaces.DataSets.Vydej)e.Result;
                    if (dsVydej == null)
                        dsVydej = new Fask.Interfaces.DataSets.Vydej();

                    bsVydej.DataSource = dsVydej;
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
                this.progressIndicator1.Location = new Point(this.dgVydej.Location.X + (this.dgVydej.Width / 2) - (progressIndicator1.Size.Width / 2), this.dgVydej.Location.Y + (this.dgVydej.Height / 2) - (progressIndicator1.Size.Height / 2));
            }
            catch { }
            progressIndicator1.Start();
            progressIndicator1.Visible = true;
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

                Fask.Interfaces.Filtry.VydejHlavickyFiltr filtr = rowFiltr;

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

                Fask.Interfaces.Filtry.VydejHlavickyFiltr filtr = new Fask.Interfaces.Filtry.VydejHlavickyFiltr();
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
        private void PerformNastavitFiltr(Fask.Interfaces.Filtry.VydejHlavickyFiltr filtr)
        {
            try
            {
                if (filtr == null)
                    return;

                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                PerformVycistitFiltr();

                // countentries
                cbCountEntries.Text = filtr.CountEntries.ToString();

                // sopnumbe
                cbSopnumbe.Text = filtr.Sopnumbe;


                // lokace
                cbPriority.Text = filtr.Priority;

                cbFiltrNestazeneDavky.Checked = filtr.ZobrazitPouzeNestazeneDavky;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show("Nepodařilo se načíst data filtru.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1)
                {
                    if (SelectedRow != null)
                        sortedID = SelectedRow.CountEntries.ToString();
                }
            }
            catch { }
        }

        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            try
            {
                int pos = this.bsVydej.Find(dsVydej.Hlavicky.CountEntriesColumn.ColumnName, sortedID);
                this.bsVydej.Position = pos;
            }
            catch { }
        }

        private void PerformEditPriorityDavka()
        {
            try
            {
                if (SelectedRow == null)
                    return;

                if (!SelectedRow.IsCZ_DosloNull() && SelectedRow.CZ_Doslo > 0)
                {
                    MessageBox.Show("Prioritu je možné změnit pouze u nestažených dávek.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string val = string.Empty;
                
                DialogResult dr = Forms.InputBox.Show("Změna priority dávky", "Zadejte prioritu", SelectedRow.IsPRIORITYNull() ? string.Empty : SelectedRow.PRIORITY.ToString(), Forms.InputBox.TypeOfCode.NumericInt, true, 0, 5, out val);
                if (dr != System.Windows.Forms.DialogResult.OK)
                    return;

                // validace probiha v InputBoxu
                int priority = Convert.ToInt32(val);
                bool res = false;

                if ((providerVydej != null) && (providerVydej is Fask.Interfaces.Vydej.IVydej2_UpdatePriorityDavka_byPriority))
                    res = ((Fask.Interfaces.Vydej.IVydej2_UpdatePriorityDavka_byPriority)providerVydej).UpdatePriorityDavka(SelectedRow.CountEntries, (byte)priority);
                else
                    throw new NotImplementedException("Provider neobsahuje implemetaci IVydej2_UpdatePriorityDavka_byPriority");



                // aktualizace priority
                //bool res = ((Fask.Interfaces.Vydej.IVydej)providerVydej).UpdatePriorityDavka(SelectedRow.CountEntries, (byte)priority);
                if (res)
                {
                    SelectedRow.PRIORITY = priority;
                    this.dsVydej.Hlavicky.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private enum Vyber
        {
            Storno,
            Odstranit_prirazeni,
            Zmenit_uzivatele
        }

        private void PerformEditPriorityUser()
        {
            try
            {
                if (SelectedRow == null)
                    return;

                if (!SelectedRow.IsCZ_DosloNull() && SelectedRow.CZ_Doslo > 0)
                {
                    MessageBox.Show("Prioritu je možné změnit pouze u nestažených dávek.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int? userid = null;
                Vyber stav = Vyber.Zmenit_uzivatele;
                
                // 1) kontrola, zdali jiz je prirazena priorita
                // ANO -> dotaz, Priorita je jiz nastavena. Co dal? ... Storno, Zrusit prirazeni, zmena uzivatele
                // NE -> zobrazeni seznamu uzivatelu
                if (!SelectedRow.IsUSERIDNull())
                {
                    // uzivatel je nastaven ... dotaz
                    // zobrazeni dialogu pro preruseni, preskoceni, preskoceni vsech
                    // 0 -> Storno
                    // 1 -> Odstranit přiřazení
                    // 2 -> Změnit uživatele
                    int pos = Cliver.Message.Show(null,
                        "Priorita dávky uživateli je již přiřazena.",
                        2,
                        new string[] { "Storno", "Odstranit přiřazení", "Změnit uživatele" }
                        );
                    stav = (Vyber)pos;
                }

                switch (stav)
                {
                    case Vyber.Zmenit_uzivatele:
                     //TODO Zmena uživatele?
                    //using (Ciselniky.FormUzivateleList frmzbozi = new Ciselniky.FormUzivateleList(false, null, Fask.Interfaces.Classes.ZOBRAZENI_TYP.VYBER))
                        //{
                        //    frmzbozi.Text = "Výběr uživatele";

                        //    if (frmzbozi.ShowDialog(this) != DialogResult.OK)
                        //        return;

                        //    userid = frmzbozi.SelectedRow.ID;
                        //}
                        break;
                    case Vyber.Odstranit_prirazeni:
                        DialogResult dr = MessageBox.Show("Opravdu chcete odstranit přiřazení dávky uživateli?", "Dotaz", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr != System.Windows.Forms.DialogResult.Yes)
                            return;

                        userid = null;
                        break;                    
                    case Vyber.Storno:    
                    default: // vyber uzivatele
                        return;
                }
                bool res = false;

                if ((providerVydej != null) && (providerVydej is Fask.Interfaces.Vydej.IVydej2_UpdatePriorityDavka_byUserID))
                    res = ((Fask.Interfaces.Vydej.IVydej2_UpdatePriorityDavka_byUserID)providerVydej).UpdatePriorityDavka(SelectedRow.CountEntries, userid);
                else
                    throw new NotImplementedException("Provider neobsahuje implemetaci IVydej2_UpdatePriorityDavka_byUserID");

                
                // aktualizace priority
               // bool res = ((Fask.Interfaces.Vydej.IVydej)providerVydej).UpdatePriorityDavka(SelectedRow.CountEntries, userid);
                if (res)
                {
                    if (userid.HasValue)
                    {
                        SelectedRow.USERID = userid.Value;
                        
                        // dotahnuti loginu, jmena, prijmeni ... neni nutne, aby proslo
                        try
                        {
                            //Fask.Interfaces.DataSets.Uzivatele.CZMSTPWDRow uzivatel;

                            //if ((providerUzivatele != null) && (providerUzivatele is Fask.Interfaces.Ciselniky.IUzivatele2_GetUzivatelByID))
                            //    uzivatel = ((Fask.Interfaces.Ciselniky.IUzivatele2_GetUzivatelByID)providerUzivatele).GetUzivatelByID(userid.Value);
                            //else
                            //    throw new NotImplementedException("Provider neimplementuje IUzivatele2_GetUzivatelByID.");


                            var _validUserid = FASK.Logins.Uzivatel.Instance.Komunikace.GetLoginsByID(userid.Value.ToString());

                            //Fask.Interfaces.DataSets.Uzivatele.CZMSTPWDRow uzivatel = ((Fask.Interfaces.Ciselniky.IUzivatele)providerUzivatele).GetUzivatelByID(userid.Value);
                            if ((_validUserid != null) && (_validUserid.Count > 0))
                            {
                                SelectedRow.LOGIN = _validUserid.First().USERID.Trim();
                                SelectedRow.FIRSTNAME = _validUserid.First().firstname.Trim();
                                SelectedRow.SECONDNAME = _validUserid.First().surname.Trim(); 
                            }
                        }
                        catch
                        {
                            SelectedRow.SetLOGINNull();
                            SelectedRow.SetFIRSTNAMENull();
                            SelectedRow.SetSECONDNAMENull();
                        }
                    }
                    else
                    {
                        SelectedRow.SetUSERIDNull();
                        SelectedRow.SetLOGINNull();
                        SelectedRow.SetFIRSTNAMENull();
                        SelectedRow.SetSECONDNAMENull();
                    }

                    this.dsVydej.Hlavicky.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            PerformEditPriorityDavka();
        }

        private void tsbVycistit_Click(object sender, EventArgs e)
        {
            PerformVycistitFiltr();
        }

        private void PerformVycistitFiltr()
        {
            try
            {
                cbCountEntries.Text = string.Empty;
                cbSopnumbe.Text = string.Empty;
                cbPriority.Text = string.Empty;
                cbFiltrNestazeneDavky.Checked = false;
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

                this.dgVydej.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                this.dgVydej.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
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

                this.dgVydej.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                dgVydej.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
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

                dgVydej.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                dgVydej.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #endregion

        private void tsmiZmenitPriorituDavky_Click(object sender, EventArgs e)
        {
            PerformEditPriorityDavka();
        }

        private void tsmiZmenitPriorituDavkyNaUzivatele_Click(object sender, EventArgs e)
        {
            PerformEditPriorityUser();
        }

        private void advancedDataGridViewSearchToolBar1_Search(object sender, Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs e)
        {

            bool restartsearch = true;
            int startColumn = 0;
            int startRow = 0;
            if (!e.FromBegin)
            {
                bool endcol = dgVydej.CurrentCell.ColumnIndex + 1 >= dgVydej.ColumnCount;
                bool endrow = dgVydej.CurrentCell.RowIndex + 1 >= dgVydej.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dgVydej.CurrentCell.ColumnIndex;
                    startRow = dgVydej.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dgVydej.CurrentCell.ColumnIndex + 1;
                    startRow = dgVydej.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dgVydej.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dgVydej.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dgVydej.CurrentCell = c;


        }
    }
}
