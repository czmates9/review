using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fask.Logging;
using System.IO;
using Konzola.Extensions;
using System.Reflection;
using MST_Print_Server_ZPL_Printing;
using Konzola.Vyroba.Rozbory;
using ZXing.Datamatrix;
using ZXing.Common;
using ZXing;

namespace Konzola.Vyroba
{
    public partial class FormProduction_SN_List : Form
    {

        #region Parametry

        /// <summary>
        /// Provider pro komunikaci
        /// </summary>
        private Fask.Interfaces.IMES providerProduction = null;
        private Fask.Interfaces.IMES providerTisk = null; //**DONE

        private List<Fask.Interfaces.Filtry.Production_SNListFiltr> filtry = new List<Fask.Interfaces.Filtry.Production_SNListFiltr>();


        /// <summary>
        /// Vybrany filtr.
        /// </summary>
        private Fask.Interfaces.Filtry.Production_SNListFiltr rowFiltr
        {
            get
            {
                try
                {
                    return tscbFiltry.SelectedItem as Fask.Interfaces.Filtry.Production_SNListFiltr;
                }
                catch
                {
                    return null;
                }
            }
        }

        public Fask.Interfaces.DataSets.Vyroba.Production_SN_PohledRow Production_SN_Pohled_selectedRow
        {
            get
            {
                try
                {
                    return ((DataRowView)(dg_P_SN.BindingContext[bs_P_SN].Current)).Row as Fask.Interfaces.DataSets.Vyroba.Production_SN_PohledRow;
                }
                catch
                {
                    return null;
                }
            }
        }


        private List<Fask.Interfaces.DataSets.Vyroba.Production_SN_PohledRow> Production_SN_Pohled_selectedRows
        {
            //get
            //{
            //    List<Fask.Interfaces.DataSets.Vyroba.Production_SN_PohledRow> rows = new List<Fask.Interfaces.DataSets.Vyroba.Production_SN_PohledRow>();

            //    foreach (DataGridViewRow selectedRow in dg_P_SN.SelectedRows)
            //    {
            //        Fask.Interfaces.DataSets.Vyroba.Production_SN_PohledRow row = ((DataRowView)selectedRow.DataBoundItem).Row as Fask.Interfaces.DataSets.Vyroba.Production_SN_PohledRow;
            //        rows.Add(row);
            //    }

            //    return rows;
            //}

            get
            {
                return dg_P_SN.SelectedRows
                    .Cast<DataGridViewRow>()
                    .OrderBy(r => r.Index)               // zarovnání na vizuální pořadí v gridu
                    .Select(r => ((DataRowView)r.DataBoundItem).Row as Fask.Interfaces.DataSets.Vyroba.Production_SN_PohledRow)
                    .Where(r => r != null)
                    .ToList();
            }
        }


        #endregion

        #region Eventy formu


        public FormProduction_SN_List()
        {
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);

            InitializeComponent();
            this.dg_P_SN.UpdateColumnHeaderCellsByDatasource();
            panelButtons.Menu = menuStrip1;
        }

        private void FormProduction_SN_List_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                this.WindowState = FormWindowState.Maximized;

                // načtení konfigurace datagridu z nastavení aplikace
                this.dg_P_SN.LoadConfiguration(this.GetType().ToString());
                panelButtons.LoadConfiguration(this.GetType().ToString());
                panelButtons.Init();
                panelButtons.Size = new Size(85, 700);

                adgvstb_P_SN.SetColumns(dg_P_SN.Columns);

                InitProvider();

                if (providerProduction == null)
                    throw new Exception("Provider 'Production' není inicializován");


                // nastavení času
                // nastavení času (zacatek a konec dne)
                dtp_DatumOd.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                dtp_DatumDo.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1, 0, 0, 0);
                dtp_DatumDo.Checked = false;
                dtp_DatumOd.Checked = false;


                var Logins = FASK.Logins.Uzivatel.Instance.Komunikace.GetLogins();

                cb_USERID.Items.AddRange(Logins.Select(null, "surname asc, firstname asc"));
                cb_USERID.SelectedItem = null;

                buttonVyhledat.Focus();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormProduction_SN_List_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.dg_P_SN.SaveConfiguration(this.GetType().ToString());
                panelButtons.SaveConfiguration(this.GetType().ToString());
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormProduction_SN_List_KeyDown(object sender, KeyEventArgs e)
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


        #endregion

        #region Inicializace provideru

        /// <summary>
        /// Inicializace providera Production
        /// </summary>
        private void InitProvider()
        {
            #region Productions

            try
            {
                if (String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                    return;

                if (providerProduction == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Vyroba.Production.IProduction).IsAssignableFrom(t))
                            {
                                providerProduction = (Fask.Interfaces.Vyroba.Production.IProduction)providerAssemlby.CreateInstance(t.FullName);
                                if (providerProduction != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                }

                providerProduction.InitProvider();
               
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

        /// <summary>
        /// Metoda pro dotaženi dat podle filtru
        /// </summary>
        private void PerformVyhledat()
        {
            try
            {
                DataTable dtchanged = this.ds_P_SN.Production_SN_Pohled.GetChanges();
                if (dtchanged != null && dtchanged.Rows.Count > 0) //obsahuje zmeny ... 
                {
                    DialogResult dr = MessageBox.Show("Obsahuje změny, pokračovat?", "data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == System.Windows.Forms.DialogResult.No)
                        return;
                }

                if (bw__P_SN.IsBusy)
                {
                    bw__P_SN.CancelAsync();
                    while (bw__P_SN.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }

                ProgressIndicatorVyrobekStart();

                Fask.Interfaces.Filtry.Production_SNListFiltr filtr = new Fask.Interfaces.Filtry.Production_SNListFiltr();
                if (!CreateFilter(ref filtr))
                    return;

                int FirstDisplayedScrollingRowIndex = this.dg_P_SN.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

                bw__P_SN.RunWorkerAsync(filtr);

                if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dg_P_SN.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dg_P_SN.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index
            }
            catch (Exception ex)
            {
                ProgressIndicatorVyrobekStop();
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }



        #endregion

        #region Click eventy

        private void tsmiKonec_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }


        private void buttonVyhledat_Click(object sender, EventArgs e)
        {
            NastavDatagrid();

            PerformVyhledat();
        }

        private void buttonOdznacitVse_Click(object sender, EventArgs e)
        {
            try
            {
                this.dg_P_SN.ClearSelection();
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
                this.dg_P_SN.SelectAll();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region DataGrid
        private void NastavDatagrid()
        {
            #region prace s datagridem - nastaveni numeric hodnot na pocet desetinnych mist
            try
            {
                // Procházení sloupců v DataGridView
                foreach (DataGridViewColumn column in dg_P_SN.Columns)
                {
                    // Získání typu dat pro sloupec
                    Type dataType = null;

                    if (dg_P_SN.DataSource is DataTable dataTable)
                    {
                        // Pokud je datový zdroj DataTable
                        if (dataTable.Columns.Contains(column.DataPropertyName))
                        {
                            dataType = dataTable.Columns[column.DataPropertyName].DataType;
                        }
                    }
                    else if (dg_P_SN.DataSource is BindingSource bindingSource)
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
        private bool CreateFilter(ref Fask.Interfaces.Filtry.Production_SNListFiltr filtr)
        {
            if (filtr == null)
                filtr = new Fask.Interfaces.Filtry.Production_SNListFiltr();

            //TODO Tady filtry

            filtr.DatumDo = dtp_DatumDo.Checked;
            filtr.DatumDoValue = dtp_DatumDo.Value;

            filtr.DatumOd = dtp_DatumOd.Checked;
            filtr.DatumOdValue = dtp_DatumOd.Value;

            filtr.ITEMDESC = cb_ITEMDESC.Text;
            filtr.ITEMCODE = cb_ITEMCODE.Text;
            filtr.SOPNUMBE = cb_SOPNUMBE.Text;
            filtr.SERLNMBR = cb_SERLNMBR.Text;
            filtr.USERID = cb_USERID.Text;

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

                Fask.Interfaces.Filtry.Production_SNListFiltr filtr = rowFiltr;

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

                Fask.Interfaces.Filtry.Production_SNListFiltr filtr = new Fask.Interfaces.Filtry.Production_SNListFiltr();
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
        private void PerformNastavitFiltr(Fask.Interfaces.Filtry.Production_SNListFiltr filtr)
        {
            try
            {
                if (filtr == null)
                    return;

                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                PerformVycistitFiltr();

                //TODO  Doplnit filtry
                dtp_DatumDo.Checked = filtr.DatumDo;
                dtp_DatumDo.Value = filtr.DatumDoValue;

                dtp_DatumOd.Checked = filtr.DatumOd;
                dtp_DatumOd.Value = filtr.DatumOdValue;
                
                cb_ITEMDESC.Text = filtr.ITEMDESC;
                cb_ITEMCODE.Text = filtr.ITEMCODE;
                cb_SOPNUMBE.Text = filtr.SOPNUMBE;
                cb_SERLNMBR.Text = filtr.SERLNMBR;
                cb_USERID.Text = filtr.USERID;

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
                //TODO tady vyčistit filtry
                dtp_DatumDo.Checked = false;
                dtp_DatumDo.Value = DateTime.Now;

                dtp_DatumOd.Checked = false;
                dtp_DatumOd.Value = DateTime.Now;

                cb_ITEMDESC.Text = string.Empty;
                cb_ITEMCODE.Text = string.Empty;
                cb_SOPNUMBE.Text = string.Empty;
                cb_SERLNMBR.Text = string.Empty;
                cb_USERID.Text = string.Empty;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Exporty  CSV, XML, EXCEL

        private void exportDoCSVVseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                this.dg_P_SN.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                this.dg_P_SN.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
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

                this.dg_P_SN.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                dg_P_SN.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
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

                dg_P_SN.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                dg_P_SN.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region BACKGRUND workery pro dotahovani tabulky

        private void bw__P_SN_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
            Fask.Interfaces.Filtry.Production_SNListFiltr filtr = (Fask.Interfaces.Filtry.Production_SNListFiltr)e.Argument;
            Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();
   
            if (bw__P_SN.CancellationPending)
            {
                e.Cancel = true;
                return;
            }


            // nacteni dat v oddelenem vlakne
            if ((providerProduction != null) && providerProduction is Fask.Interfaces.Vyroba.Production_SN.IProduction_SN_GetFiltrovanyProduction_SNList)
                ds = ((Fask.Interfaces.Vyroba.Production_SN.IProduction_SN_GetFiltrovanyProduction_SNList)providerProduction).GetFiltrovanyProduction_SNList(filtr);
            else
                throw new Exception("IProduction_SN_GetFiltrovanyProduction_SNList not implementet");


            if (bw__P_SN.CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            e.Result = ds;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bw__P_SN_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                // check error, check cancel, then use result
                if (e.Error != null)
                {
                    // handle the error
                    this.ds_P_SN = new Fask.Interfaces.DataSets.Vyroba();
                    bs_P_SN.DataSource = this.ds_P_SN;
                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, e.Error);
                    MessageBox.Show(e.Error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (e.Cancelled)
                {
                    // handle cancellation
                    this.ds_P_SN = new Fask.Interfaces.DataSets.Vyroba();
                    bs_P_SN.DataSource = this.ds_P_SN;
                }
                else
                {
                    // uspesne dokonceno ...
                    // use it on the UI thread
                    this.ds_P_SN = (Fask.Interfaces.DataSets.Vyroba)e.Result;
                    if (this.ds_P_SN == null)
                        this.ds_P_SN = new Fask.Interfaces.DataSets.Vyroba();

                    bs_P_SN.DataSource = this.ds_P_SN;
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
                this.progressIndicatorVyrobek.Location = new Point(this.dg_P_SN.Location.X + (this.dg_P_SN.Width / 2) - (progressIndicatorVyrobek.Size.Width / 2), this.dg_P_SN.Location.Y + (this.dg_P_SN.Height / 2) - (progressIndicatorVyrobek.Size.Height / 2));
            }
            catch { }
            progressIndicatorVyrobek.Start();
            progressIndicatorVyrobek.Visible = true;
        }

        #endregion

        #region advancedDataGridViewSearchToolBar

        private void adgvstb_P_SN_Search(object sender, Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs e)
        {
            bool restartsearch = true;
            int startColumn = 0;
            int startRow = 0;
            if (!e.FromBegin)
            {
                bool endcol = dg_P_SN.CurrentCell.ColumnIndex + 1 >= dg_P_SN.ColumnCount;
                bool endrow = dg_P_SN.CurrentCell.RowIndex + 1 >= dg_P_SN.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dg_P_SN.CurrentCell.ColumnIndex;
                    startRow = dg_P_SN.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dg_P_SN.CurrentCell.ColumnIndex + 1;
                    startRow = dg_P_SN.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dg_P_SN.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dg_P_SN.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dg_P_SN.CurrentCell = c;
        }

        #endregion

        #region GS1 metody
        static string AddCheckDigit(string input)
        {
            // Výpočet check digitu
            int sumOdd = 0;
            int sumEven = 0;

            for (int i = 0; i < input.Length; i++)
            {
                int digit = int.Parse(input[i].ToString());

                if (i % 2 == 0)
                {
                    sumEven += digit;
                }
                else
                {
                    sumOdd += digit;
                }
            }

            int totalSum = sumOdd + sumEven * 3;
            int nearestTenMultiple = (int)Math.Ceiling((double)totalSum / 10) * 10;
            int checkDigit = nearestTenMultiple - totalSum;

            // Přidání check digitu na konec vstupního čísla
            return input + checkDigit.ToString();
        }

        static Bitmap GenerateGS1DataMatrix(string gs1Data)
        {
            // Nastavení parametrů pro GS1 formát
            DatamatrixEncodingOptions encodingOptions = new DatamatrixEncodingOptions
            {
                GS1Format = true,
                Height = 300,
                Width = 300
            };

            // Vytvoření instance BarcodeWriter pro DataMatrix
            BarcodeWriter barcodeWriter = new BarcodeWriter();
            barcodeWriter.Format = BarcodeFormat.DATA_MATRIX;
            barcodeWriter.Options = encodingOptions;

            // Generování kódu
            ZXing.Common.BitMatrix bitMatrix = barcodeWriter.Encode(gs1Data);

            // Převedení BitMatrix na Bitmap
            Bitmap bitmap = BitMatrixToBitmap(bitMatrix);

            return bitmap;
        }

        static Bitmap BitMatrixToBitmap(BitMatrix bitMatrix)
        {
            int width = bitMatrix.Width;
            int height = bitMatrix.Height;
            Bitmap bitmap = new Bitmap(width, height);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    bitmap.SetPixel(x, y, bitMatrix[x, y] ? Color.Black : Color.White);
                }
            }

            return bitmap;
        }
        #endregion



        #region MaR 26.9.2025 Tisk ZPL a RDLC
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

                if (Production_SN_Pohled_selectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                #region MaR 6.11.2024 vypis zpl do souboru selected rows
                int index_zaznamu = 0;

                foreach (var item in Production_SN_Pohled_selectedRows)
                {

                    if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start vyroba/rozbory/prehled_vyrobky_SN_sarze TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");

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

                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END vyroba/rozbory/prehled_vyrobky_SN_sarze TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");


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



                    foreach (var item in Production_SN_Pohled_selectedRows)
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
                    Tisk_selected_rows_Primy_Tisk(plr_Path, true, MN_ToTisk, Production_SN_Pohled_selectedRows, NazevVychoziTiskarny);

                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Tisk_selected_rows_Primy_Tisk(string plr_Path, bool MnozstvuAutoJedna, int MN_ToTisk, List<Fask.Interfaces.DataSets.Vyroba.Production_SN_PohledRow> Production_SN_Pohled_selected_Rows, string nazevVychTiskarny)
        {
            try
            {

                #region selected rows

                foreach (var item in Production_SN_Pohled_selected_Rows)
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

                if (Production_SN_Pohled_selectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                //if (FASK_ZASOBY_selectedRows.Count > 1)
                //{
                //    //MessageBox.Show("Je vybráno více záznamů než jeden.", this.Text, MessageBoxButtons.OK);
                //    //return;
                //}

                Fask.Interfaces.DataSets.Vyroba.Production_SN_PohledDataTable dt = new Fask.Interfaces.DataSets.Vyroba.Production_SN_PohledDataTable();

                //dt.Columns.Add("VNDITNUM_IMG", typeof(string));
                //dt.Columns.Add("BarcodeP_IMG", typeof(string));
                dt.Columns.Add("SOPNUMBE_IMG", typeof(string));
                dt.Columns.Add("ITEMNMBR_IMG", typeof(string));

                #region reseni BIOMAG_ 8.1.2025


                dt.Columns.Add("BarcodeP_IMG", typeof(string));
                dt.Columns.Add("VNDITNUM_IMG", typeof(string));

                dt.Columns.Add("A_2D_DataMatrix_IMG", typeof(string));
                dt.Columns.Add("A_2D_DataMatrix_kod", typeof(string));
                string A_2D_kod = string.Empty;


                string SERLTNUM = string.Empty;

                string VNDITNUM = string.Empty;
                DateTime? EXPIRACE = null;

                string datumexpirace01 = string.Empty;
                string sn = string.Empty;
                dt.Columns.Add("datumExpirace", typeof(string));
                dt.Columns.Add("SN", typeof(string));

                dt.Columns.Add("QTY_Paleta", typeof(decimal));
                dt.Columns.Add("MJ_Paleta", typeof(string));

                string A_2D_kod_GS1 = string.Empty;

                #region kontrola dat UDI MaR 7.2.2025
                if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba[0].kontrolaDatUDI)
                {
                    foreach (Fask.Interfaces.DataSets.Vyroba.Production_SN_PohledRow item in dt)
                    {

                        if (!item.IsSOPNUMBENull() && !string.IsNullOrEmpty(item.SOPNUMBE))
                        {
                            ZXing.BarcodeWriter zw = new ZXing.BarcodeWriter();
                            zw.Format = ZXing.BarcodeFormat.CODE_128;
                            zw.Options.Height = 50; //50
                            zw.Options.PureBarcode = true;
                            System.Drawing.Bitmap image1 = zw.Write(item.SOPNUMBE);

                            System.IO.MemoryStream ms = new System.IO.MemoryStream();
                            image1.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                            byte[] imgReportBarcode = ms.ToArray();
                            ms.Close();

                            string Base64Imahe = Convert.ToBase64String(imgReportBarcode);

                            item["SOPNUMBE_IMG"] = Base64Imahe;

                        }

                        if (!item.IsITEMNMBRNull() && !string.IsNullOrEmpty(item.ITEMNMBR))
                        {
                            ZXing.BarcodeWriter zw = new ZXing.BarcodeWriter();
                            zw.Format = ZXing.BarcodeFormat.CODE_128;
                            zw.Options.Height = 50; //50
                            zw.Options.PureBarcode = true;
                            System.Drawing.Bitmap image1 = zw.Write(item.ITEMNMBR);

                            System.IO.MemoryStream ms = new System.IO.MemoryStream();
                            image1.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                            byte[] imgReportBarcode = ms.ToArray();
                            ms.Close();

                            string Base64Imahe = Convert.ToBase64String(imgReportBarcode);

                            item["ITEMNMBR_IMG"] = Base64Imahe;

                        }

                        char prefixNumber_char = '0'; // Převod int na char



                        if (item.IsQTYPACKMJNull() || string.IsNullOrEmpty(item.QTYPACKMJ))
                        {
                            prefixNumber_char = '0';
                        }
                        else
                        {
                            prefixNumber_char = item.QTYPACKMJ[0];
                        }




                        if (!item.IsVNDITNUMNull() || !string.IsNullOrEmpty(item.VNDITNUM))
                        {
                            ZXing.BarcodeWriter zw = new ZXing.BarcodeWriter();
                            zw.Format = ZXing.BarcodeFormat.CODE_128;
                            zw.Options.Height = 50; //50
                            zw.Options.PureBarcode = true;
                            System.Drawing.Bitmap image1 = zw.Write(item.VNDITNUM);

                            System.IO.MemoryStream ms = new System.IO.MemoryStream();
                            image1.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                            byte[] imgReportBarcode = ms.ToArray();
                            ms.Close();

                            string Base64Imahe = Convert.ToBase64String(imgReportBarcode);

                            item["VNDITNUM_IMG"] = Base64Imahe;


                            EXPIRACE = DateTime.Now;
                            if (EXPIRACE.HasValue)
                            {

                                datumexpirace01 = EXPIRACE.Value.ToString("yyyy-MM-dd");
                                sn = EXPIRACE.Value.ToString("yy");
                            }


                            //2D kod

                            VNDITNUM = item.VNDITNUM;
                            if (VNDITNUM.Length < 13)
                            {
                                VNDITNUM = VNDITNUM.PadLeft(12, '0');
                                VNDITNUM = VNDITNUM.PadLeft(13, prefixNumber_char);

                            }
                            else if (VNDITNUM.Length > 13)
                            {
                                VNDITNUM = VNDITNUM.Substring(0, 12);
                                VNDITNUM = VNDITNUM.PadLeft(13, prefixNumber_char);
                            }
                            else if (VNDITNUM.Length == 13)
                            {
                                VNDITNUM = VNDITNUM.Substring(0, 12);
                                VNDITNUM = VNDITNUM.PadLeft(13, prefixNumber_char);
                            }

                            A_2D_kod += "(01)" + AddCheckDigit(VNDITNUM);
                            A_2D_kod_GS1 += "01" + AddCheckDigit(VNDITNUM);



                        }

                        EXPIRACE = DateTime.Now;
                        if (EXPIRACE.HasValue)
                        {

                            string datumexpirace = EXPIRACE.Value.ToString("yyMMdd");
                            A_2D_kod += "(11)" + datumexpirace;
                            A_2D_kod_GS1 += "11" + datumexpirace;
                        }

                        // A_2D_kod += "(11)" + DateTime.Now.ToString() + @"\n";

                        if (!string.IsNullOrEmpty(item.BarcodeP))
                        {
                            ZXing.BarcodeWriter zw = new ZXing.BarcodeWriter();
                            zw.Format = ZXing.BarcodeFormat.CODE_128;
                            zw.Options.Height = 50; //50
                            zw.Options.PureBarcode = true;
                            System.Drawing.Bitmap image1 = zw.Write(item.BarcodeP);

                            System.IO.MemoryStream ms = new System.IO.MemoryStream();
                            image1.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                            byte[] imgReportBarcode = ms.ToArray();
                            ms.Close();

                            string Base64Imahe = Convert.ToBase64String(imgReportBarcode);

                            item["BarcodeP_IMG"] = Base64Imahe;

                            //2D kod
                            A_2D_kod += "(21)" + item.BarcodeP;

                            if (item.BarcodeP.Length < 5)
                            {
                                string message_ex = string.Format("GTIN nemá správnou strukturu, prvek BarcodeP:{0} musí mít více než 4 znaky!", item.BarcodeP);
                                Fask.Logging.ExceptionHandler2.Handle(LogLevel.Error, message_ex);
                            }

                            A_2D_kod_GS1 += "21" + item.BarcodeP;
                            sn += "-" + item.BarcodeP;

                        }


                        #region 2D_Datamatrix GS1

                        // GS1 DataMatrix kód s FNC1 symbolem
                        string gs1Data = (char)29 + A_2D_kod_GS1;
                        // Generování GS1 DataMatrix
                        Bitmap gs1DataMatrixImage = GenerateGS1DataMatrix(gs1Data);
                        MemoryStream ms1 = new MemoryStream();
                        gs1DataMatrixImage.Save(ms1, System.Drawing.Imaging.ImageFormat.Bmp);

                        byte[] imgReportBarcode1 = ms1.ToArray();
                        ms1.Close();

                        string Base64Imahe1 = Convert.ToBase64String(imgReportBarcode1);




                        item["A_2D_DataMatrix_IMG"] = Base64Imahe1;
                        item["A_2D_DataMatrix_kod"] = A_2D_kod;

                        item["datumExpirace"] = datumexpirace01;
                        item["SN"] = sn;
                        A_2D_kod = string.Empty;
                        gs1Data = string.Empty;
                        A_2D_kod_GS1 = string.Empty;
                        #endregion



                        //if (item.QTYPACK > 0)
                        //    item["QTY_Paleta"] = item.QTYSHPPD / item.QTYPACK;
                        //else
                        //    item["QTY_Paleta"] = 0;

                        //item["MJ_Paleta"] = "Pal";

                        //if (string.IsNullOrEmpty(item.ITEMMJ))
                        //    item.ITEMMJ = "-";
                    }

                }
                else
                {
                    foreach (Fask.Interfaces.DataSets.Vyroba.Production_SN_PohledRow item in dt)
                    {

                        if (!item.IsSOPNUMBENull() && !string.IsNullOrEmpty(item.SOPNUMBE))
                        {
                            ZXing.BarcodeWriter zw = new ZXing.BarcodeWriter();
                            zw.Format = ZXing.BarcodeFormat.CODE_128;
                            zw.Options.Height = 50; //50
                            zw.Options.PureBarcode = true;
                            System.Drawing.Bitmap image1 = zw.Write(item.SOPNUMBE);

                            System.IO.MemoryStream ms = new System.IO.MemoryStream();
                            image1.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                            byte[] imgReportBarcode = ms.ToArray();
                            ms.Close();

                            string Base64Imahe = Convert.ToBase64String(imgReportBarcode);

                            item["SOPNUMBE_IMG"] = Base64Imahe;

                        }

                        if (!item.IsITEMNMBRNull() && !string.IsNullOrEmpty(item.ITEMNMBR))
                        {
                            ZXing.BarcodeWriter zw = new ZXing.BarcodeWriter();
                            zw.Format = ZXing.BarcodeFormat.CODE_128;
                            zw.Options.Height = 50; //50
                            zw.Options.PureBarcode = true;
                            System.Drawing.Bitmap image1 = zw.Write(item.ITEMNMBR);

                            System.IO.MemoryStream ms = new System.IO.MemoryStream();
                            image1.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                            byte[] imgReportBarcode = ms.ToArray();
                            ms.Close();

                            string Base64Imahe = Convert.ToBase64String(imgReportBarcode);

                            item["ITEMNMBR_IMG"] = Base64Imahe;

                        }


                        char prefixNumber_char = '0'; // Převod int na char



                        if (item.IsQTYPACKMJNull() || string.IsNullOrEmpty(item.QTYPACKMJ))
                        {
                            prefixNumber_char = '0';
                        }
                        else
                        {
                            prefixNumber_char = item.QTYPACKMJ[0];
                        }




                        if (!item.IsVNDITNUMNull() || !string.IsNullOrEmpty(item.VNDITNUM))
                        {
                            ZXing.BarcodeWriter zw = new ZXing.BarcodeWriter();
                            zw.Format = ZXing.BarcodeFormat.CODE_128;
                            zw.Options.Height = 50; //50
                            zw.Options.PureBarcode = true;
                            System.Drawing.Bitmap image1 = zw.Write(item.VNDITNUM);

                            System.IO.MemoryStream ms = new System.IO.MemoryStream();
                            image1.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                            byte[] imgReportBarcode = ms.ToArray();
                            ms.Close();

                            string Base64Imahe = Convert.ToBase64String(imgReportBarcode);

                            item["VNDITNUM_IMG"] = Base64Imahe;


                            EXPIRACE = DateTime.Now;
                            if (EXPIRACE.HasValue)
                            {

                                datumexpirace01 = EXPIRACE.Value.ToString("yyyy-MM-dd");
                                sn = EXPIRACE.Value.ToString("yy");
                            }


                            //2D kod

                            VNDITNUM = item.VNDITNUM;
                            if (VNDITNUM.Length < 13)
                            {
                                VNDITNUM = VNDITNUM.PadLeft(12, '0');
                                VNDITNUM = VNDITNUM.PadLeft(13, prefixNumber_char);

                            }
                            else if (VNDITNUM.Length > 13)
                            {
                                VNDITNUM = VNDITNUM.Substring(0, 12);
                                VNDITNUM = VNDITNUM.PadLeft(13, prefixNumber_char);
                            }
                            else if (VNDITNUM.Length == 13)
                            {
                                VNDITNUM = VNDITNUM.Substring(0, 12);
                                VNDITNUM = VNDITNUM.PadLeft(13, prefixNumber_char);
                            }

                            A_2D_kod += "(01)" + AddCheckDigit(VNDITNUM);
                            A_2D_kod_GS1 += "01" + AddCheckDigit(VNDITNUM);



                        }

                        EXPIRACE = DateTime.Now;
                        if (EXPIRACE.HasValue)
                        {

                            string datumexpirace = EXPIRACE.Value.ToString("yyMMdd");
                            A_2D_kod += "(11)" + datumexpirace;
                            A_2D_kod_GS1 += "11" + datumexpirace;
                        }

                        // A_2D_kod += "(11)" + DateTime.Now.ToString() + @"\n";

                        if (!string.IsNullOrEmpty(item.BarcodeP))
                        {
                            ZXing.BarcodeWriter zw = new ZXing.BarcodeWriter();
                            zw.Format = ZXing.BarcodeFormat.CODE_128;
                            zw.Options.Height = 50; //50
                            zw.Options.PureBarcode = true;
                            System.Drawing.Bitmap image1 = zw.Write(item.BarcodeP);

                            System.IO.MemoryStream ms = new System.IO.MemoryStream();
                            image1.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                            byte[] imgReportBarcode = ms.ToArray();
                            ms.Close();

                            string Base64Imahe = Convert.ToBase64String(imgReportBarcode);

                            item["BarcodeP_IMG"] = Base64Imahe;

                            //2D kod
                            A_2D_kod += "(21)" + item.BarcodeP;

                            if (item.BarcodeP.Length < 5)
                            {
                                string message_ex = string.Format("GTIN nemá správnou strukturu, prvek BarcodeP:{0} musí mít více než 4 znaky!", item.BarcodeP);
                                Fask.Logging.ExceptionHandler2.Handle(LogLevel.Error, message_ex);
                            }

                            A_2D_kod_GS1 += "21" + item.BarcodeP;
                            sn += "-" + item.BarcodeP;

                        }


                        #region 2D_Datamatrix GS1

                        // GS1 DataMatrix kód s FNC1 symbolem
                        string gs1Data = (char)29 + A_2D_kod_GS1;
                        // Generování GS1 DataMatrix
                        Bitmap gs1DataMatrixImage = GenerateGS1DataMatrix(gs1Data);
                        MemoryStream ms1 = new MemoryStream();
                        gs1DataMatrixImage.Save(ms1, System.Drawing.Imaging.ImageFormat.Bmp);

                        byte[] imgReportBarcode1 = ms1.ToArray();
                        ms1.Close();

                        string Base64Imahe1 = Convert.ToBase64String(imgReportBarcode1);




                        item["A_2D_DataMatrix_IMG"] = Base64Imahe1;
                        item["A_2D_DataMatrix_kod"] = A_2D_kod;

                        item["datumExpirace"] = datumexpirace01;
                        item["SN"] = sn;
                        A_2D_kod = string.Empty;
                        gs1Data = string.Empty;
                        A_2D_kod_GS1 = string.Empty;
                        #endregion



                        //if (item.QTYPACK > 0)
                        //    item["QTY_Paleta"] = item.QTYSHPPD / item.QTYPACK;
                        //else
                        //    item["QTY_Paleta"] = 0;

                        //item["MJ_Paleta"] = "Pal";

                        //if (string.IsNullOrEmpty(item.ITEMMJ))
                        //    item.ITEMMJ = "-";
                    }
                }
                #endregion

                //metoda na UDI code
                //ProviderTisk tisk = new ProviderTisk();
                //string temp = string.Empty;
                //tisk.TiskMetodaEtiketa_GS1(ref dt);

                //string sopdesc = SelectedVPHRow.IsSOPDESCNull() ? string.Empty : SelectedVPHRow.SOPDESC.Trim();




                #endregion


                foreach (Fask.Interfaces.DataSets.Vyroba.Production_SN_PohledRow row in Production_SN_Pohled_selectedRows)
                {
                    try
                    {
                        Fask.Interfaces.DataSets.Vyroba.Production_SN_PohledRow newRow = dt.NewProduction_SN_PohledRow();
                        // Ošetřit delší vstupní pole než počet sloupců tabulky
                        int copyLength = Math.Min(row.ItemArray.Length, dt.Columns.Count);
                        newRow.ItemArray = row.ItemArray.Take(copyLength).ToArray();
                        dt.AddProduction_SN_PohledRow(newRow);
                    }
                    catch (Exception ex)
                    {
                        // Ošetření vyjimky
                        // Můžete zde provést logování chyby nebo jiné požadované akce
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                    }


                }

                #region 1.10.2025 MaR navrh CK pro promenne
                //foreach (Fask.Interfaces.DataSets.Vyroba.Production_SN_PohledRow row in dt)
                //{

                //    try
                //    {
                //        if (!row.IsVNDITNUMNull() && !string.IsNullOrEmpty(row.VNDITNUM))
                //        {
                //            ZXing.BarcodeWriter zw = new ZXing.BarcodeWriter();
                //            zw.Format = ZXing.BarcodeFormat.CODE_128;
                //            zw.Options.Height = 50; //50
                //            zw.Options.PureBarcode = true;
                //            System.Drawing.Bitmap image1 = zw.Write(row.VNDITNUM);

                //            System.IO.MemoryStream ms = new System.IO.MemoryStream();
                //            image1.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                //            byte[] imgReportBarcode = ms.ToArray();
                //            ms.Close();

                //            string Base64Imahe = Convert.ToBase64String(imgReportBarcode);

                //            row["VNDITNUM_IMG"] = Base64Imahe;
                //        }

                //        if (!row.IsCZ_CarKodNull() && !string.IsNullOrEmpty(row.CZ_CarKod))
                //        {
                //            ZXing.BarcodeWriter zw = new ZXing.BarcodeWriter();
                //            zw.Format = ZXing.BarcodeFormat.CODE_128;
                //            zw.Options.Height = 50; //50
                //            zw.Options.PureBarcode = true;
                //            System.Drawing.Bitmap image1 = zw.Write(row.CZ_CarKod);

                //            System.IO.MemoryStream ms = new System.IO.MemoryStream();
                //            image1.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                //            byte[] imgReportBarcode = ms.ToArray();
                //            ms.Close();

                //            string Base64Imahe = Convert.ToBase64String(imgReportBarcode);

                //            row["CZ_CarKod_IMG"] = Base64Imahe;

                //        }

                //        if (!row.IsLOCNCODENull() && !string.IsNullOrEmpty(row.LOCNCODE))
                //        {
                //            ZXing.BarcodeWriter zw = new ZXing.BarcodeWriter();
                //            zw.Format = ZXing.BarcodeFormat.CODE_128;
                //            zw.Options.Height = 50; //50
                //            zw.Options.PureBarcode = true;
                //            System.Drawing.Bitmap image1 = zw.Write(row.LOCNCODE);

                //            System.IO.MemoryStream ms = new System.IO.MemoryStream();
                //            image1.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                //            byte[] imgReportBarcode = ms.ToArray();
                //            ms.Close();

                //            string Base64Imahe = Convert.ToBase64String(imgReportBarcode);

                //            row["LOCNCODE_IMG"] = Base64Imahe;

                //        }

                //    }
                //    catch (Exception ex)
                //    {

                //        Fask.Logging.ExceptionHandler2.Handle(ex);
                //    }


                //} 
                #endregion



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



        private void PrintReport_RDLC(Fask.Interfaces.DataSets.Vyroba.Production_SN_PohledDataTable dt, bool primyTisk, string path_sablona, string nazevVychTiskarny, int pocetVytisku)
        {
            try
            {

                #region logovani tisku
                if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                {
                    foreach (var item in dt)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start vyroba/rozbory/prehled_vyrobky_SN_sarze TISK rdlc-------------------------------------");

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

                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END vyroba/rozbory/prehled_vyrobky_SN_sarze TISK rdlc-------------------------------------");
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

                if (Production_SN_Pohled_selectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                #region MaR 6.11.2024 vypis zpl do souboru selected rows
                int index_zaznamu = 0;

                foreach (var item in Production_SN_Pohled_selectedRows)
                {

                    if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start vyroba/rozbory/prehled_vyrobky_SN_sarze TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");

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

                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END vyroba/rozbory/prehled_vyrobky_SN_sarze TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");


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



                    foreach (var item in Production_SN_Pohled_selectedRows)
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
                    Tisk_selected_rows(plr_Path, true, MN_ToTisk, Production_SN_Pohled_selectedRows);

                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Tisk_selected_rows(string plr_Path, bool MnozstvuAutoJedna, int MN_ToTisk, List<Fask.Interfaces.DataSets.Vyroba.Production_SN_PohledRow> Production_SN_Pohled_selected_Rows)
        {
            try
            {
                PrintDialog printDialog1 = new PrintDialog();
                printDialog1.UseEXDialog = true;

                if (printDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.Cancel)
                    return;



                #region selected rows

                foreach (var item in Production_SN_Pohled_selected_Rows)
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

        private int? TiskMnozstvi(bool MnozstviAutoJedna)
        {
            try
            {
                string pocetStr = string.Empty;
                int pocetInt = 1;

                if (!MnozstviAutoJedna)
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
            Fask.Interfaces.DataSets.Vyroba.Production_SN_PohledRow row,
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

                Fask.Interfaces.DataSets.Vyroba.Production_SN_PohledDataTable dt = new Fask.Interfaces.DataSets.Vyroba.Production_SN_PohledDataTable();

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

        private Dictionary<string, string> PrepareDataToTisk(Fask.Interfaces.DataSets.Vyroba.Production_SN_PohledRow Production_SN_PohledRow_data)
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

                        Fask.Interfaces.DataSets.Vyroba.Production_SN_PohledDataTable dt = new Fask.Interfaces.DataSets.Vyroba.Production_SN_PohledDataTable();


                        foreach (DataColumn dcol in dt.Columns)
                        {
                            string key = dcol.ColumnName;
                            string value = Production_SN_PohledRow_data[dcol.ColumnName].ToString();
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
