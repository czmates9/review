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
    public partial class FormInventuraHlavickaList : Form
    {
        private Fask.Console.Interfaces.IVyrobaKonzola providerSklady = null;
        private Fask.Console.Interfaces.IVyrobaKonzola providerInventura = null;
        
        private List<Fask.Console.Interfaces.Classes.InventuraPredlohaListFiltr> filtry = new List<Fask.Console.Interfaces.Classes.InventuraPredlohaListFiltr>();
        private Fask.Console.Interfaces.DataSets.Sklady dsSklady = new Fask.Console.Interfaces.DataSets.Sklady();

        /// <summary>
        /// Zvolené id před seřazením
        /// </summary>
        string sortedID = string.Empty;

        public Fask.Console.Interfaces.DataSets.Inventura.CZMST_I1Row SelectedRow
        {
            get
            {
                try
                {
                    return ((DataRowView)(dataGridView1.BindingContext[bsInventuraPredloha].Current)).Row as Fask.Console.Interfaces.DataSets.Inventura.CZMST_I1Row;
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
        private Fask.Console.Interfaces.Classes.InventuraPredlohaListFiltr rowFiltr
        {
            get
            {
                try
                {
                    return tscbFiltry.SelectedItem as Fask.Console.Interfaces.Classes.InventuraPredlohaListFiltr;
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
        private Fask.Console.Interfaces.DataSets.Inventura.CZMST_I1HRow rowCountEntries
        {
            get
            {
                try
                {
                    return comboBoxCountEntries.SelectedItem as Fask.Console.Interfaces.DataSets.Inventura.CZMST_I1HRow;
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
        private Fask.Console.Interfaces.DataSets.Sklady.CZMST093Row rowSKLID
        {
            get
            {
                try
                {
                    return comboBoxSKLID.SelectedItem as Fask.Console.Interfaces.DataSets.Sklady.CZMST093Row;
                }
                catch
                {
                    return null;
                }
            }
        }

        public FormInventuraHlavickaList()
        {
            InitializeComponent();
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                //dataGridView1.ExportAllRowsVisibleColumnsToExcel(string.Empty);
                this.dataGridView1.ExportToExcel(Fask.Console.Interfaces.Classes.EXPORT_DAT.VSE);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonExportOznacene_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                //dataGridView1.ExportSelectedRowsVisibleColumnsToExcel(string.Empty);
                this.dataGridView1.ExportToExcel(Fask.Console.Interfaces.Classes.EXPORT_DAT.OZNACENE);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormPohybyList_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                this.WindowState = FormWindowState.Maximized;
                
                // načtení konfigurace datagridu z nastavení aplikace
                this.dataGridView1.LoadConfiguration(this.GetType().ToString());

                //// načtení konfigurace vytvořených filtrů
                this.filtry = Extensions.ListExt.ReadFromXML<Fask.Console.Interfaces.Classes.InventuraPredlohaListFiltr>(this.GetType().ToString() + ".filtr");
                tscbFiltry.ComboBox.DataSource = this.filtry;
                tscbFiltry.SelectedItem = null;
                tscbFiltry.ComboBox.DropDownWitdhAutosize();

                // inicializace providera
                InitProvider();

                if (providerInventura == null)
                    throw new Exception("Provider 'Inventura' není inicializován");
                
                if (providerSklady == null)
                    throw new Exception("Provider 'Sklady' není inicializován");

                System.Threading.Thread loadThread = new System.Threading.Thread(LoadDataAsync);
                loadThread.IsBackground = true;
                loadThread.Start();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDataAsync()
        {
            try
            {
                Fask.Console.Interfaces.DataSets.Sklady dsSkladyData = new Fask.Console.Interfaces.DataSets.Sklady();
                Fask.Console.Interfaces.DataSets.Inventura dsInventuraHlavickyData = new Fask.Console.Interfaces.DataSets.Inventura();
                
                // naplneni ciselniku skladu
                //dsSkladyData = ((Fask.Console.Interfaces.Ciselniky.ISklady)providerSklady).GetSklady();


                if ((providerSklady != null) && (providerSklady is Fask.Console.Interfaces.Ciselniky.ISklady2_GetSklady))
                    dsSkladyData = ((Fask.Console.Interfaces.Ciselniky.ISklady2_GetSklady)providerSklady).GetSklady();
                else
                    throw new NotImplementedException("Provider neimplementuje ISklady2_GetSklady.");

                // naplneni ciselniku hlavicek
                //dsInventuraHlavickyData = ((Fask.Console.Interfaces.Inventura.IInventura)providerInventura).GetHlavicky();

                if ((providerInventura != null) && (providerInventura is Fask.Console.Interfaces.Inventura.IInventura2_GetHlavicky))
                    dsInventuraHlavickyData = ((Fask.Console.Interfaces.Inventura.IInventura2_GetHlavicky)providerInventura).GetHlavicky();
                else
                    throw new NotImplementedException("Provider neimplementuje IInventura2_GetHlavicky.");


                // navrat do hlavniho vlakna
                //if (InvokeRequired)
                //{
                //    BeginInvoke(new Action(() =>
                //    {
                //        PopulateUI(dsSkladyData, dsInventuraHlavickyData);
                //    }));
                //}
                PopulateUI(dsSkladyData, dsInventuraHlavickyData);
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

        private void PopulateUI(Fask.Console.Interfaces.DataSets.Sklady dsSkladyData, Fask.Console.Interfaces.DataSets.Inventura dsInventuraPredlohaData)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    PopulateUI(dsSkladyData, dsInventuraPredlohaData);
                }));
                return;
            }

            try
            {
                // naplneni comboboxu uzivatelu
                //comboBoxCountEntries.Items.AddRange(dsUzivateleData.CZMSTPWD.Select(null, "SECONDNAME asc"));
                //comboBoxCountEntries.SelectedItem = null;

                // naplneni comboboxu skladu
                this.dsSklady = dsSkladyData;
                comboBoxSKLID.Items.Clear();
                comboBoxSKLID.Items.AddRange(dsSkladyData.CZMST093.Select(null, "skl_desc asc"));
                comboBoxSKLID.SelectedItem = null;

                comboBoxCountEntries.Items.Clear();
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
            try
            {
                if (!String.IsNullOrEmpty(Settings.ProviderKonzola))
                {
                    if (providerInventura == null) //inicializace se provede pouze pokud nebyla provedena ... 
                    {
                        Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Settings.ProviderKonzola));
                        Type[] types = providerAssemlby.GetTypes();
                        foreach (Type t in types)
                        {
                            try
                            {
                                if (typeof(Fask.Console.Interfaces.Inventura.IInventura2).IsAssignableFrom(t))
                                {
                                    providerInventura = (Fask.Console.Interfaces.Inventura.IInventura2)providerAssemlby.CreateInstance(t.FullName);
                                    if (providerInventura != null)
                                        break;
                                }
                            }
                            catch { }
                        }
                        //return config;
                    }

                    // nastaveni connection stringu
                    //if (providerInventura != null)
                    //    ((Fask.Console.Interfaces.Inventura.IInventura)providerInventura).ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;

                    if ((providerInventura != null) && (providerInventura is Fask.Console.Interfaces.Parametry.IParametry2_ConnectionString))
                        ((Fask.Console.Interfaces.Parametry.IParametry2_ConnectionString)providerInventura).ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
              

                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                if (!String.IsNullOrEmpty(Settings.ProviderKonzola))
                {
                    if (providerSklady == null) //inicializace se provede pouze pokud nebyla provedena ... 
                    {
                        Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Settings.ProviderKonzola));
                        Type[] types = providerAssemlby.GetTypes();
                        foreach (Type t in types)
                        {
                            try
                            {
                                if (typeof(Fask.Console.Interfaces.Ciselniky.ISklady2).IsAssignableFrom(t))
                                {
                                    providerSklady = (Fask.Console.Interfaces.Ciselniky.ISklady2)providerAssemlby.CreateInstance(t.FullName);
                                    if (providerSklady != null)
                                        break;
                                }
                            }
                            catch { }
                        }
                        //return config;
                    }

                    // nastaveni connection stringu
                    //if (providerSklady != null)
                    //    ((Fask.Console.Interfaces.Ciselniky.ISklady)providerSklady).ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
                    if ((providerSklady != null) && (providerSklady is Fask.Console.Interfaces.Parametry.IParametry2_ConnectionString))
                        ((Fask.Console.Interfaces.Parametry.IParametry2_ConnectionString)providerSklady).ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
              

                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonVyhledat_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        /// <summary>
        /// Vytvoreni filtru, ktery se pouzije.
        /// </summary>
        /// <param name="filtr"></param>
        /// <returns></returns>
        private bool CreateFilter(ref Fask.Console.Interfaces.Classes.InventuraPredlohaListFiltr filtr)
        {
            if(filtr == null)
                filtr = new Fask.Console.Interfaces.Classes.InventuraPredlohaListFiltr();

            filtr.CountEntries = rowCountEntries == null ? (string.IsNullOrEmpty(comboBoxCountEntries.Text) ? string.Empty : comboBoxCountEntries.Text) : rowCountEntries.CountEntries.ToString();
            filtr.MaterialID = comboBoxMaterialID.Text.Trim();
            filtr.rowMaterialSKLID = rowSKLID == null ? string.Empty : rowSKLID.skl_id;
            filtr.MaterialSKLID = comboBoxSKLID.Text.Trim();
            filtr.MaterialLocncode = comboBoxLOCNCODE.Text.Trim();
            filtr.ZobrazitPouzeNenasnimane = cbFiltrNenasnimanePolozky.Checked;

            return true;
        }

        private void PerformOK()
        {
            try
            {
                DataTable dtchanged = this.dsInventuraPredloha.CZMST_I1.GetChanges();
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

                //int countentries = 0;
                //if (rowCountEntries == null)
                //{
                //    if (string.IsNullOrEmpty(comboBoxCountEntries.Text.Trim()))
                //    {
                //        MessageBox.Show("Musí být vybráno číslo dávky", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        //ProgressIndicatorStop();
                //        comboBoxCountEntries.Focus();
                //        comboBoxCountEntries.SelectAll();
                //        return;
                //    }

                //    bool result = int.TryParse(comboBoxCountEntries.Text, out countentries);
                //    if (!result)
                //    {
                //        ProgressIndicatorStop();
                //        MessageBox.Show("Vybrané číslo dávky musí být číslo", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        comboBoxCountEntries.Focus();
                //        comboBoxCountEntries.SelectAll();
                //        return;
                //    }
                //    // 12.5.2016 PeV: konfiguracne kontrolovat cislo davky
                //    //else
                //    //{
                //    //    // zkusit vyhledat ...
                //    //    var res = providerInventura.GetHlavickaByID(Convert.ToInt32(comboBoxCountEntries.Text));
                //    //    if (res == null)
                //    //    {
                //    //        ProgressIndicatorStop();
                //    //        MessageBox.Show("Dávka s číslem '" + comboBoxCountEntries.Text.Trim() + "' nebyla nalezena", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    //        comboBoxCountEntries.Focus();
                //    //        comboBoxCountEntries.SelectAll();
                //    //        return;
                //    //    }
                //    //}
                //}

                ProgressIndicatorStart();

                //Fask.Console.Interfaces.Classes.InventuraPredlohaListFiltr filtr = new Fask.Console.Interfaces.Classes.InventuraPredlohaListFiltr();
                //if (!CreateFilter(ref filtr))
                //    return;

                int FirstDisplayedScrollingRowIndex = this.dataGridView1.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

                //bwLoadSkladPohyb.RunWorkerAsync(filtr);
                bwLoadSkladPohyb.RunWorkerAsync();

                if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dataGridView1.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dataGridView1.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index
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

        private void FormPohybyList_KeyDown(object sender, KeyEventArgs e)
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

        private void FormPohybyList_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.dataGridView1.SaveConfiguration(this.GetType().ToString());

                // ulozeni vsech filtru
                this.filtry.WriteXML(this.GetType().ToString() + ".filtr");
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
                this.dataGridView1.SelectAll();
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
                this.dataGridView1.ClearSelection();
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

        private void FormSkladPohybList_Shown(object sender, EventArgs e)
        {
            try
            {
                this.progressIndicator1.Size = new Size(Settings.ProgressIndicatorSize, Settings.ProgressIndicatorSize);
                this.progressIndicator1.Location = new Point(this.dataGridView1.Location.X + (this.dataGridView1.Width / 2) - (progressIndicator1.Size.Width / 2), this.dataGridView1.Location.Y + (this.dataGridView1.Height / 2) - (progressIndicator1.Size.Height / 2));
            }
            catch { }
        }

        private void bwSkladPohyb_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //bwLoadSkladPohyb.CancelAsync();
                //Fask.Console.Interfaces.Classes.InventuraPredlohaListFiltr filtr = (Fask.Console.Interfaces.Classes.InventuraPredlohaListFiltr)e.Argument;
                Fask.Console.Interfaces.DataSets.Inventura ds = new Fask.Console.Interfaces.DataSets.Inventura();

                if (bwLoadSkladPohyb.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                // nacteni dat v oddelenem vlakne
                //ds = ((Fask.Console.Interfaces.Inventura.IInventura)providerInventura).GetFiltrovanaPredloha(filtr);

                if ((providerInventura != null) && (providerInventura is Fask.Console.Interfaces.Inventura.IInventura2_GetHlavicky))
                    ds = ((Fask.Console.Interfaces.Inventura.IInventura2_GetHlavicky)providerInventura).GetHlavicky();
                else
                    throw new NotImplementedException("Provider neimplementuje IInventura2_GetHlavicky.");

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
                    dsInventuraPredloha = new Fask.Console.Interfaces.DataSets.Inventura();
                    bsInventuraPredloha.DataSource = dsInventuraPredloha;
                }
                else if (e.Cancelled)
                {
                    // handle cancellation
                    // TODO: maji se vymazat data z datasetu?? asi ano ...
                    //if (dsSkladPohyb == null)
                    dsInventuraPredloha = new Fask.Console.Interfaces.DataSets.Inventura();
                    bsInventuraPredloha.DataSource = dsInventuraPredloha;
                }
                else
                {
                    // uspesne dokonceno ...
                    // use it on the UI thread
                    dsInventuraPredloha = (Fask.Console.Interfaces.DataSets.Inventura)e.Result;
                    if (dsInventuraPredloha == null)
                        dsInventuraPredloha = new Fask.Console.Interfaces.DataSets.Inventura();

                    bsInventuraPredloha.DataSource = dsInventuraPredloha;
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
                this.progressIndicator1.Location = new Point(this.dataGridView1.Location.X + (this.dataGridView1.Width / 2) - (progressIndicator1.Size.Width / 2), this.dataGridView1.Location.Y + (this.dataGridView1.Height / 2) - (progressIndicator1.Size.Height / 2));
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

                Fask.Console.Interfaces.Classes.InventuraPredlohaListFiltr filtr = rowFiltr;

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

                Fask.Console.Interfaces.Classes.InventuraPredlohaListFiltr filtr = new Fask.Console.Interfaces.Classes.InventuraPredlohaListFiltr();
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
        private void PerformNastavitFiltr(Fask.Console.Interfaces.Classes.InventuraPredlohaListFiltr filtr)
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

                // sklad
                if (string.IsNullOrEmpty(filtr.rowMaterialSKLID))
                {
                    // uzivatel nevyplnen, doplnit pouze text ...
                    comboBoxSKLID.Text = filtr.MaterialSKLID;
                }
                else
                {
                    // uzivatel vyplnen, pokusit se dohledat
                    //var sklady = dsSklady.CZMST093.Where(x => x.skl_id == filtr.rowMaterialSKLID);
                    //if (sklady.Count() > 0)
                    //    comboBoxSKLID.SelectedItem = sklady.First();
                    //else
                    //    comboBoxSKLID.Text = filtr.rowMaterialSKLID;
                }

                // lokace
                comboBoxLOCNCODE.Text = filtr.MaterialLocncode;

                cbFiltrNenasnimanePolozky.Checked = filtr.ZobrazitPouzeNenasnimane;
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
                        sortedID = SelectedRow.DEX_ROW_ID.ToString();
                }
            }
            catch { }
        }

        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            try
            {
                int pos = this.bsInventuraPredloha.Find(dsInventuraPredloha.CZMST_I1.DEX_ROW_IDColumn.ColumnName, sortedID);
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
                comboBoxSKLID.SelectedItem = null;
                comboBoxLOCNCODE.SelectedItem = null;

                comboBoxCountEntries.Text = string.Empty;
                comboBoxMaterialID.Text = string.Empty;
                comboBoxSKLID.Text = string.Empty;
                comboBoxLOCNCODE.Text = string.Empty;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



    }

}
