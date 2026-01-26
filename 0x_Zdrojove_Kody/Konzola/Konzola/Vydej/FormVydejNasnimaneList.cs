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
using Fask.Interfaces.Classes;
using System.IO;
using Konzola.Vyroba.Rozbory;
using MST_Print_Server_ZPL_Printing;

namespace Konzola.Vydej
{
    public partial class FormVydejNasnimaneList: Form
    {
        private Fask.Interfaces.IMES providerVydej = null;
        private Fask.Interfaces.IMES providerTisk = null; //**DONE

        private List<Fask.Interfaces.Filtry.VydejNasnimaneFiltr> filtry = new List<Fask.Interfaces.Filtry.VydejNasnimaneFiltr>();

        /// <summary>
        /// Kolekce veškerých zvolených sloupců.
        /// </summary>
        public DataGridViewSelectedRowCollection SelectedRows
        {
            get
            {
                try
                {
                    return dgVydej.SelectedRows;
                }
                catch
                {
                    return null;
                }
            }
        }

 

        /// <summary>
        /// Zvolené id před seřazením
        /// </summary>
        string sortedID = string.Empty;

        public Fask.Interfaces.DataSets.Vydej.CZMST_SIRow  CZMST_SI_SelectedRow
        {
            get
            {
                try
                {
                    return ((DataRowView)(dgVydej.BindingContext[bsVydej].Current)).Row as Fask.Interfaces.DataSets.Vydej.CZMST_SIRow;
                }
                catch
                {
                    return null;
                }
            }
        }

        private List<Fask.Interfaces.DataSets.Vydej.CZMST_SIRow> CZMST_SI_SelectedRows
        {
            //get
            //{
            //    List<Fask.Interfaces.DataSets.Vydej.CZMST_SIRow> rows = new List<Fask.Interfaces.DataSets.Vydej.CZMST_SIRow>();

            //    foreach (DataGridViewRow selectedRow in dgVydej.SelectedRows)
            //    {
            //        Fask.Interfaces.DataSets.Vydej.CZMST_SIRow row = ((DataRowView)selectedRow.DataBoundItem).Row as Fask.Interfaces.DataSets.Vydej.CZMST_SIRow;
            //        rows.Add(row);
            //    }

            //    return rows;
            //}

            get
            {
                return dgVydej.SelectedRows
                    .Cast<DataGridViewRow>()
                    .OrderBy(r => r.Index)               // zarovnání na vizuální pořadí v gridu
                    .Select(r => ((DataRowView)r.DataBoundItem).Row as Fask.Interfaces.DataSets.Vydej.CZMST_SIRow)
                    .Where(r => r != null)
                    .ToList();
            }
        }


        /// <summary>
        /// Vybrany filtr.
        /// </summary>
        private Fask.Interfaces.Filtry.VydejNasnimaneFiltr rowFiltr
        {
            get
            {
                try
                {
                    return tscbFiltry.SelectedItem as Fask.Interfaces.Filtry.VydejNasnimaneFiltr;
                }
                catch
                {
                    return null;
                }
            }
        }

        #region  Eventy formu

        public FormVydejNasnimaneList()
        {
            InitializeComponent();

            this.dgVydej.UpdateColumnHeaderCellsByDatasource();

            panelButtons.Menu = menuStrip1;
        }

        private bool opravneni = false;

        public FormVydejNasnimaneList(bool opravneni) : this()
        {
            this.opravneni = opravneni;
            //this.dg_OdvodEvents.MultiSelect = allowMultiSelect;
            //this.Zobrazeni = typZobrazeni;

            //if (!opravneni)
            //{
            //    menuStrip1.Items.Remove(tsmiPolozka);
            //    tsmiAkce.DropDownItems.Remove(tsmiUpravit);
            //    tsmiAkce.DropDownItems.Remove(tsmiOdstranit);
            //}



            if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Import())
            {
                //MessageBox.Show("Uživatel nemá dostatečná oprávnění!", "Upozornení", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                //frmlist = new Vydej.FormVydejNasnimaneList(true);
            }
            else
            {
                menuStrip1.Items.Remove(tsmiAkce);
                tsmiAkce.DropDownItems.Remove(tsmiImportovatDoklad);
            }

            //if (Zobrazeni == Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST)
            //{
            //    panelButtonsZobrazeniList.Menu = menuStrip2;
            //    this.WindowState = FormWindowState.Maximized;
            //}
        }


        private void FormVydejNasnimaneList_Load(object sender, EventArgs e)
        {
            try
            {

                if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_P_ZP())
                {
                    tiskEtiketToolStripMenuItem.Enabled = true;



                }
                else
                {
                    tiskEtiketToolStripMenuItem.Enabled = false;
                    tsmiExport.DropDownItems.Remove(tiskEtiketToolStripMenuItem);
                }

                tsmiImportovatDoklad.Enabled = false;

                this.ShowIcon = false;
                this.WindowState = FormWindowState.Maximized;


                // načtení konfigurace datagridu z nastavení aplikace
                this.dgVydej.LoadConfiguration(this.GetType().ToString());

                panelButtons.LoadConfiguration(this.GetType().ToString());
                panelButtons.Init();
                panelButtons.Size = new Size(85, 700);

                advancedDataGridViewSearchToolBar1.SetColumns(dgVydej.Columns);

                //// načtení konfigurace vytvořených filtrů
                this.filtry = Extensions.ListExt.ReadFromXML<Fask.Interfaces.Filtry.VydejNasnimaneFiltr>(this.GetType().ToString() + ".filtr");
                tscbFiltry.ComboBox.DataSource = this.filtry;
                tscbFiltry.SelectedItem = null;
                tscbFiltry.ComboBox.DropDownWitdhAutosize();

                DateTime today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                dtp_porizeno_OD.Value = today.AddDays(-1);
                dtp_porizeno_DO.Value = today;


                dtp_porizeno_OD.Checked = false;
                dtp_porizeno_DO.Checked = false;

                cb_Porizeno_TimeVariant.Items.AddRange(Fask.Interfaces.Classes.TimeFilters.GetNumberNameRange());
                cb_Porizeno_TimeVariant.SelectedIndex = 0;


                //tB_Typ_dokladu.ReadOnly = true;
                //tB_Typ_dokladu.Enabled = false;


                // inicializace providera
                InitProvider();

                if (providerVydej == null)
                    throw new Exception("Provider 'Vydej' není inicializován");



                tsmiImportovatDoklad.Text = "Odeslat doklad";
                tsmiImportovatDoklad.Enabled = false;


                buttonVyhledat.Focus();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public string GetTimeVariantFromFilter_porizeno()
        {
            Fask.Interfaces.Classes.TimeVariantName type = (Fask.Interfaces.Classes.TimeVariantName)cb_Porizeno_TimeVariant.SelectedItem;
            return type.ToString_Filter();
        }

        private void FormVydejNasnimaneList_FormClosing(object sender, FormClosingEventArgs e)
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

        private void FormVydejNasnimaneList_KeyDown(object sender, KeyEventArgs e)
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

        private void FormVydejNasnimaneList_Shown(object sender, EventArgs e)
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


        #endregion

        private void buttonExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                //dataGridView1.ExportAllRowsVisibleColumnsToExcel(string.Empty);
                this.dgVydej.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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
                this.dgVydej.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
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
        private bool CreateFilter(ref Fask.Interfaces.Filtry.VydejNasnimaneFiltr filtr)
        {
            if (filtr == null)
                filtr = new Fask.Interfaces.Filtry.VydejNasnimaneFiltr();

            filtr.CountEntries = cbCountEntries.Text.Trim();
            filtr.ITEMNMBR = cbITEMNMBR.Text.Trim();
            filtr.SOPNUMBE = cbSOPNUMBE.Text.Trim();

            filtr.ITEMTYPE_J = chB_vydej.Checked;
            filtr.ITEMTYPE_I = chB_inventura.Checked;
            filtr.ITEMTYPE_P = chB_prijem.Checked;
            filtr.ITEMTYPE_E = chB_expedice.Checked;
            filtr.ITEMTYPE_V = chB_vyroba.Checked;
            filtr.ITEMTYPE_O = chB_ostatni.Checked;

            filtr.ITEMDESC = tB_Nazev_polozky.Text.Trim();
            //filtr.ITEMTYPE = tB_Typ_dokladu.Text.Trim();
            filtr.ITEMCODE = tB_kod_polozky.Text.Trim();

            string input = tB_ID_uzivatele.Text.Trim();
            filtr.USERID = new List<int>();

            if (!string.IsNullOrWhiteSpace(input))
            {
                string[] parts = input.Split(',');
                foreach (var part in parts)
                {
                    int id;
                    if (int.TryParse(part.Trim(), out id))
                    {
                        filtr.USERID.Add(id);
                    }
                }

                // Pokud se nepodařilo žádné ID zparsovat, nastav na null
                if (filtr.USERID.Count == 0)
                    filtr.USERID = null;
            }
            else
            {
                filtr.USERID = null;
            }


            string terminalInput = tB_ID_terminalu.Text.Trim();
            filtr.ID_TERMINAL = new List<int>();

            if (!string.IsNullOrWhiteSpace(terminalInput))
            {
                string[] parts = terminalInput.Split(',');
                foreach (var part in parts)
                {
                    int id;
                    if (int.TryParse(part.Trim(), out id))
                        filtr.ID_TERMINAL.Add(id);
                }

                if (filtr.ID_TERMINAL.Count == 0)
                    filtr.ID_TERMINAL = null;
            }
            else
            {
                filtr.ID_TERMINAL = null;
            }




            #region DATEDONE

            if (dtp_porizeno_OD.Checked && dtp_porizeno_OD.Enabled)
            {
                filtr.DATEDONE_OD = dtp_porizeno_OD.Value;
            }
            else
                filtr.DATEDONE_OD = null;

            if (dtp_porizeno_DO.Checked && dtp_porizeno_DO.Enabled)
            {
                filtr.DATEDONE_DO = dtp_porizeno_DO.Value;
            }
            else
                filtr.DATEDONE_DO = null;

            #endregion

            #region Time variant dateeve

            var por = GetTimeVariantFromFilter_porizeno();

            if (por.Contains("unknow"))
            {
                filtr.Dateeve_TimeVariant = null;
            }
            else
            {
                filtr.Dateeve_TimeVariant = por;
            }

            #endregion

            return true;
        }

        int? Index = null;

        private void PerformOK()
        {
            try
            {
                //DataTable dtchanged = this.dsVydej.CZMST_SI.GetChanges();
                //if (dtchanged != null && dtchanged.Rows.Count > 0) //obsahuje zmeny ... 
                //{
                //    //MaR zakomentoval 11.3.2024
                //    //DialogResult dr = MessageBox.Show("Obsahuje změny, pokračovat?", "data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                //    //if (dr == System.Windows.Forms.DialogResult.No)
                //    //    return;
                //}

                if (bwLoadVolnyPohyb.IsBusy)
                {
                    bwLoadVolnyPohyb.CancelAsync();
                    while (bwLoadVolnyPohyb.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }

                ProgressIndicatorStart();

                Fask.Interfaces.Filtry.VydejNasnimaneFiltr filtr = new Fask.Interfaces.Filtry.VydejNasnimaneFiltr();
                if (!CreateFilter(ref filtr))
                    return;

                int FirstDisplayedScrollingRowIndex = this.dgVydej.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

                if(CZMST_SI_SelectedRow != null)
                    Index = CZMST_SI_SelectedRow.DEX_ROW_ID;


                bwLoadVolnyPohyb.RunWorkerAsync(filtr);

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
                Fask.Interfaces.Filtry.VydejNasnimaneFiltr filtr = (Fask.Interfaces.Filtry.VydejNasnimaneFiltr)e.Argument;
                Fask.Interfaces.DataSets.Vydej ds = new Fask.Interfaces.DataSets.Vydej();

                if (bwLoadVolnyPohyb.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                // nacteni dat v oddelenem vlakne
                //ds = ((Fask.Interfaces.Vydej.IVydej)providerVydej).GetFiltrovaneHlavicky(filtr);

                if ((providerVydej != null) && (providerVydej is Fask.Interfaces.Vydej.IVydej2_GetFiltrovaneDavkySI))
                    ds = ((Fask.Interfaces.Vydej.IVydej2_GetFiltrovaneDavkySI)providerVydej).GetFiltrovaneDavkySI(filtr);
                else
                    throw new NotImplementedException("Provider neimplementuje IVydej2_GetFiltrovaneDavkySI.");



                if (bwLoadVolnyPohyb.CancellationPending)
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
                    dsVydej = (Fask.Interfaces.DataSets.Vydej)e.Result;
                    if (dsVydej == null)
                        dsVydej = new Fask.Interfaces.DataSets.Vydej();

                    bsVydej.DataSource = dsVydej;



                    if (Index != null )
                    {
                        var polozky = dsVydej.CZMST_SI.Where(x => x.DEX_ROW_ID == Index);
                        if (polozky.Count() > 0)
                        {
                            var polozka = polozky.First();

                            int index = ((System.Data.DataView)bsVydej.List).Table.Rows.IndexOf(polozka);
                            bsVydej.Position = index;
                        }
                        Index = null;
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

                Fask.Interfaces.Filtry.VydejNasnimaneFiltr filtr = rowFiltr;

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

                Fask.Interfaces.Filtry.VydejNasnimaneFiltr filtr = new Fask.Interfaces.Filtry.VydejNasnimaneFiltr();
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
        private void PerformNastavitFiltr(Fask.Interfaces.Filtry.VydejNasnimaneFiltr filtr)
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
                cbITEMNMBR.Text = filtr.ITEMNMBR;
                cbSOPNUMBE.Text = filtr.SOPNUMBE;

                // ITEMDESC a ITEMTYPE zpět do TextBoxů
                tB_Nazev_polozky.Text = filtr.ITEMDESC ?? string.Empty;
                //tB_Typ_dokladu.Text = filtr.ITEMTYPE ?? string.Empty;
                tB_kod_polozky.Text = filtr.ITEMCODE ?? string.Empty;


                // USERID: List<int>? → TextBox jako čárkami oddělený string
                if (filtr.USERID != null && filtr.USERID.Any())
                {
                    tB_ID_uzivatele.Text = string.Join(", ", filtr.USERID);
                }
                else
                {
                    tB_ID_uzivatele.Text = string.Empty;
                }

                // ID_TERMINAL: List<int>? → TextBox jako čárkami oddělený string
                if (filtr.ID_TERMINAL != null && filtr.ID_TERMINAL.Any())
                {
                    tB_ID_terminalu.Text = string.Join(", ", filtr.ID_TERMINAL);
                }
                else
                {
                    tB_ID_terminalu.Text = string.Empty;
                }


                chB_vydej.Checked = filtr.ITEMTYPE_J;
                chB_inventura.Checked = filtr.ITEMTYPE_I;
                chB_prijem.Checked = filtr.ITEMTYPE_P;
                chB_expedice.Checked = filtr.ITEMTYPE_E;
                chB_vyroba.Checked = filtr.ITEMTYPE_V;
                chB_ostatni.Checked = filtr.ITEMTYPE_O;





                if (filtr.DATEDONE_DO != null)
                {
                    dtp_porizeno_DO.Checked = true;
                    dtp_porizeno_DO.Value = (DateTime)filtr.DATEDONE_DO;
                }

                if (filtr.DATEDONE_OD != null)
                {
                    dtp_porizeno_OD.Checked = true;
                    dtp_porizeno_OD.Value = (DateTime)filtr.DATEDONE_OD;
                }

                if (!string.IsNullOrEmpty(filtr.Dateeve_TimeVariant))
                {

                    var arr = filtr.Dateeve_TimeVariant.Split(';');
                    TimeFilters.TimeVariants TimeVar = (TimeFilters.TimeVariants)Enum.Parse(typeof(TimeFilters.TimeVariants), arr[0], true);

                    TimeVariantName time = new TimeVariantName(TimeVar, arr[1]);

                    cb_Porizeno_TimeVariant.SelectedItem = time;
                }

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
                    if (CZMST_SI_SelectedRow != null)
                        sortedID = CZMST_SI_SelectedRow.CountEntries.ToString();
                }
            }
            catch { }
        }

        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            try
            {
                int pos = this.bsVydej.Find(dsVydej.CZMST_SI.CountEntriesColumn.ColumnName, sortedID);
                this.bsVydej.Position = pos;
            }
            catch { }
        }

        private enum Vyber
        {
            Storno,
            Odstranit_prirazeni,
            Zmenit_uzivatele
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
                cbITEMNMBR.Text = string.Empty;
                cbSOPNUMBE.Text = string.Empty;


                tB_Nazev_polozky.Text = string.Empty;
                //tB_Typ_dokladu.Text = string.Empty;
                tB_ID_uzivatele.Text = string.Empty;
                tB_ID_terminalu.Text = string.Empty;
                tB_kod_polozky.Text = string.Empty;

                chB_vydej.Checked = true;
                chB_inventura.Checked = true;
                chB_prijem.Checked = true;
                chB_expedice.Checked = true;
                chB_vyroba.Checked = true;
                chB_ostatni.Checked = true;

                dtp_porizeno_OD.Checked = false;
                dtp_porizeno_DO.Checked = false;
                cb_Porizeno_TimeVariant.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Generovat data 


        private void ImportDavka(int? CountEntries, string SKL_ID)
        {
            try
            {

                if (bwExport.IsBusy)
                {
                    bwExport.CancelAsync();
                    while (bwExport.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }

                ProgressIndicatorStart();

                // Okno s zadanim čisla....


                //DataDavkyImport data = new DataDavkyImport();


                //data.sklad = new Fask.Interfaces.Classes.Sklad();
                //data.sklad.ID = SKL_ID.Trim();


                //if (CountEntries.HasValue)
                //{
                //    data.objednavka = new Fask.Interfaces.Classes.Objednavka();
                //    data.objednavka.CisloDavky = CountEntries.Value.ToString();
                //}


                //bwExport.RunWorkerAsync(data);
                bwExport.RunWorkerAsync();

            }
            catch (Exception ex)
            {
                ProgressIndicatorStop();
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    
        private void bwExport_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                //dsVydej = (Fask.Interfaces.DataSets.Vydej)e.Result;
                Fask.Interfaces.Classes.StatusInfo si = new Fask.Interfaces.Classes.StatusInfo();
                try
                {

                    si = (Fask.Interfaces.Classes.StatusInfo)e.Result;

                }
                catch
                {}

                // check error, check cancel, then use result
                if (e.Error != null)
                {
                    // handle the error
                    MessageBox.Show(e.Error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (e.Cancelled)
                {
                    //handle the cancelled
                }
                else
                {
                    // uspesne dokonceno ...

                    if (si.ID < 0)
                    {
                        MessageBox.Show(si.Description, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    PerformOK();
                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                if (ex.InnerException != null)
                {
                    Fask.Logging.ExceptionHandler2.Handle(ex.InnerException);
                    MessageBox.Show(ex.Message + ex.InnerException.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                ProgressIndicatorStop();
            }
        }

        private void bwExport_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
               //DataDavkyImport data = (DataDavkyImport)e.Argument;

                if (bwExport.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                //Fask.Interfaces.Classes.StatusInfo si;

                //if ((providerProdej != null) && (providerProdej is Fask.Interfaces.Prodej.IProdej2_ImportDavka))
                //    si = ((Fask.Interfaces.Prodej.IProdej2_ImportDavka)providerProdej).ImportDavka(data.objednavka, data.sklad, Settings.Prodej_ImportDokladuGrupuj);
                //else
                //    throw new NotImplementedException("Provider neimplementuje IProdej2_ImportDavka.");



                if (bwLoadVolnyPohyb.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                //e.Result = si;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
                
                //MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void tsmiUpravit_Click(object sender, EventArgs e)
        {
            if (!opravneni)
            {
                MessageBox.Show("Nemáte dostatečné oprávnění pro tuto akci!", "Oprávnění", MessageBoxButtons.OK);
                return;
            }


            PerformEditRecord();
        }

        private void PerformEditRecord()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (CZMST_SI_SelectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro úpravu", this.Text, MessageBoxButtons.OK);
                    return;
                }


                if (dgVydej.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Je vybráno více záznamů než jeden.", this.Text, MessageBoxButtons.OK);
                    return;
                }

                #region doimplementovat!! MaR 22.2. 2024
                using (FormDavkyVydejSIEdit frmuziv = new FormDavkyVydejSIEdit())
                {
                    frmuziv.rowDavkaEdit = CZMST_SI_SelectedRow;
                    frmuziv.Text = "Úprava Davky";
                    if (frmuziv.ShowDialog(this) != DialogResult.OK)
                        return;

                    //this.SelectedRow.CountEntries = frmuziv.returnrow.CountEntries;
                    //this.SelectedRow.SOPNUMBE = frmuziv.returnrow.SOPNUMBE;
                    //this.SelectedRow.ITEMNMBR = frmuziv.returnrow.ITEMNMBR;
                    //this.SelectedRow.ITEMTYPE = frmuziv.returnrow.ITEMTYPE;
                    //this.SelectedRow.ITEMDESC = frmuziv.returnrow.ITEMDESC;
                    //this.SelectedRow.VNDDOCNM = frmuziv.returnrow.VNDDOCNM;
                    //this.SelectedRow.VNDITNUM = frmuziv.returnrow.VNDITNUM;
                    //this.SelectedRow.ORD = frmuziv.returnrow.ORD;
                    //this.SelectedRow.CZ_CarKod = frmuziv.returnrow.CZ_CarKod;
                    //this.SelectedRow.SKL_ID = frmuziv.returnrow.SKL_ID;
                    //this.SelectedRow.LOCNCODE = frmuziv.returnrow.LOCNCODE;
                    //this.SelectedRow.MJ = frmuziv.returnrow.MJ;
                    //this.SelectedRow.QTYSHPPD = frmuziv.returnrow.QTYSHPPD;
                    //this.SelectedRow.QTYPACK = frmuziv.returnrow.QTYPACK;
                    //this.SelectedRow.CZ_DatVyr_Track = frmuziv.returnrow.CZ_DatVyr_Track;
                    //this.SelectedRow.CZ_DatVyr_Delka = frmuziv.returnrow.CZ_DatVyr_Delka;
                    //this.SelectedRow.CZ_SerNum_Track = frmuziv.returnrow.CZ_SerNum_Track;
                    //this.SelectedRow.CZ_SerNum_Delka = frmuziv.returnrow.CZ_SerNum_Delka;
                    //this.SelectedRow.CZ_SW_Track = frmuziv.returnrow.CZ_SW_Track;
                    //this.SelectedRow.CZ_SW_Delka = frmuziv.returnrow.CZ_SW_Delka;
                    //this.SelectedRow.CZ_Doslo = frmuziv.returnrow.CZ_Doslo;
                    //this.SelectedRow.Note = frmuziv.returnrow.Note;
                    //this.SelectedRow.TYPEPAL = frmuziv.returnrow.TYPEPAL;
                    //this.SelectedRow.QTYPAL = frmuziv.returnrow.QTYPAL;
                    //this.SelectedRow.PRIORITY = frmuziv.returnrow.PRIORITY;
                    //this.SelectedRow.PRINTED = frmuziv.returnrow.PRINTED;
                    //this.SelectedRow.USERID = frmuziv.returnrow.USERID;
                    ////this.SelectedRow.DEX_ROW_ID = frmuziv.returnrow.DEX_ROW_ID;

                    //this.dsVydej.CZMST_SE.AcceptChanges();
                }
                #endregion

                PerformOK();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsmiOdstranit_Click(object sender, EventArgs e)
        {
            if (!opravneni)
            {
                MessageBox.Show("Nemáte dostatečné oprávnění pro tuto akci!", "Oprávnění", MessageBoxButtons.OK);
                return;
            }

            PerformDeleteRecord();
        }

        private void PerformDeleteRecord()
        {
            try
            {

                if (CZMST_SI_SelectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro smazani", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if (dgVydej.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Je vybráno více záznamů než jeden.", this.Text, MessageBoxButtons.OK);
                    return;
                }





                //if ((providerVydej != null) && (providerVydej is Fask.Interfaces.Vydej.IVydej2_DeleteSI))
                //    ((Fask.Interfaces.Vydej.IVydej2_DeleteSI)providerVydej).DeleteSI(SelectedRow.DEX_ROW_ID);
                //else
                //    throw new NotImplementedException("Provider neimplementuje IVydej2_DeleteSI.");

                bool result;
                // je úprava záznamu
                if (CZMST_SI_SelectedRow != null)
                {
                    //((Fask.Interfaces.Ciselniky.IUzivatele)providerUzivatele).UpdateUzivatel(newUzivatelRow);

                    if ((providerVydej != null) && (providerVydej is Fask.Interfaces.Vydej.IVydej2_DeleteSI))
                        result = ((Fask.Interfaces.Vydej.IVydej2_DeleteSI)providerVydej).DeleteSI(CZMST_SI_SelectedRow);
                    else
                        throw new NotImplementedException("Provider neimplementuje IVydej2_DeleteSI.");
                }

                PerformOK();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsmiImportovatDoklad_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();


            if (CZMST_SI_SelectedRow == null)
            {
                MessageBox.Show("Není vybrán záznam pro úpravu", this.Text, MessageBoxButtons.OK);
                return;
            }


            if (dgVydej.SelectedRows.Count > 1)
            {
                MessageBox.Show("Je vybráno více záznamů než jeden.", this.Text, MessageBoxButtons.OK);
                return;
            }


            ImportDavka(CZMST_SI_SelectedRow.CountEntries, CZMST_SI_SelectedRow.SKL_ID);
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

                if (CZMST_SI_SelectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                #region MaR 6.11.2024 vypis zpl do souboru selected rows
                int index_zaznamu = 0;

                foreach (var item in CZMST_SI_SelectedRows)
                {

                    if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start sklady/transakce/vydej/nasnimane TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");

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

                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END sklady/transakce/vydej/nasnimane TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");


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



                    foreach (var item in CZMST_SI_SelectedRows)
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
                    Tisk_selected_rows_Primy_Tisk(plr_Path, true, MN_ToTisk, CZMST_SI_SelectedRows, NazevVychoziTiskarny);

                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Tisk_selected_rows_Primy_Tisk(string plr_Path, bool MnozstvuAutoJedna, int MN_ToTisk, List<Fask.Interfaces.DataSets.Vydej.CZMST_SIRow> selected_Rows, string nazevVychTiskarny)
        {
            try
            {

                #region selected rows

                foreach (var item in selected_Rows)
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

                if (CZMST_SI_SelectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                //if (Production_rowProducts.Count > 1)
                //{
                //    //MessageBox.Show("Je vybráno více záznamů než jeden.", this.Text, MessageBoxButtons.OK);
                //    //return;
                //}

                Fask.Interfaces.DataSets.Vydej.CZMST_SIDataTable dt = new Fask.Interfaces.DataSets.Vydej.CZMST_SIDataTable();

                foreach (Fask.Interfaces.DataSets.Vydej.CZMST_SIRow row in CZMST_SI_SelectedRows)
                {
                    try
                    {
                        Fask.Interfaces.DataSets.Vydej.CZMST_SIRow newRow = dt.NewCZMST_SIRow();
                        // Ošetřit delší vstupní pole než počet sloupců tabulky
                        int copyLength = Math.Min(row.ItemArray.Length, dt.Columns.Count);
                        newRow.ItemArray = row.ItemArray.Take(copyLength).ToArray();
                        dt.AddCZMST_SIRow(newRow);
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

        private void PrintReport_RDLC(Fask.Interfaces.DataSets.Vydej.CZMST_SIDataTable dt, bool primyTisk, string path_sablona, string nazevVychTiskarny, int pocetVytisku)
        {
            try
            {

                #region logovani tisku
                if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                {
                    foreach (var item in dt)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start sklady/transakce/vydej/nasnimane TISK rdlc-------------------------------------");

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

                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END sklady/transakce/vydej/nasnimane TISK rdlc-------------------------------------");
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

                if (CZMST_SI_SelectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                #region MaR 6.11.2024 vypis zpl do souboru selected rows
                int index_zaznamu = 0;

                foreach (var item in CZMST_SI_SelectedRows)
                {

                    if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start sklady/transakce/vydej/nasnimane TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");

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

                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END sklady/transakce/vydej/nasnimane TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");


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



                    foreach (var item in CZMST_SI_SelectedRows)
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
                    Tisk_selected_rows(plr_Path, true, MN_ToTisk, CZMST_SI_SelectedRows);

                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Tisk_selected_rows(string plr_Path, bool MnozstvuAutoJedna, int MN_ToTisk, List<Fask.Interfaces.DataSets.Vydej.CZMST_SIRow> selected_Rows)
        {
            try
            {
                PrintDialog printDialog1 = new PrintDialog();
                printDialog1.UseEXDialog = true;

                if (printDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.Cancel)
                    return;



                #region selected rows

                foreach (var item in selected_Rows)
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
            Fask.Interfaces.DataSets.Vydej.CZMST_SIRow row,
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




                sbData.Replace("$SOURCE$", "Konzola");

                Fask.Interfaces.DataSets.Vydej.CZMST_SIDataTable dt = new Fask.Interfaces.DataSets.Vydej.CZMST_SIDataTable();

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

        private Dictionary<string, string> PrepareDataToTisk(Fask.Interfaces.DataSets.Vydej.CZMST_SIRow row_data)
        {
            try
            {
                while (true)
                {
                    try
                    {
                        Dictionary<string, string> data = new Dictionary<string, string>();


                        data.Add("SOURCE", "Konzola");

                        Fask.Interfaces.DataSets.Vydej.CZMST_SIDataTable dt = new Fask.Interfaces.DataSets.Vydej.CZMST_SIDataTable();


                        foreach (DataColumn dcol in dt.Columns)
                        {
                            string key = dcol.ColumnName;
                            string value = row_data[dcol.ColumnName].ToString();
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

        private void cbSOPNUMBE_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

