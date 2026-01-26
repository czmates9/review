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
using Konzola.Vyroba.Rozbory;
using System.IO;
using MST_Print_Server_ZPL_Printing;

namespace Konzola.Vydej
{
    public partial class FormDavkyVydejeList : Form
    {
        private Fask.Interfaces.IMES providerVydej = null;
        private Fask.Interfaces.IMES providerTisk = null; //**DONE
        //private Fask.Interfaces.IVyrobaKonzola providerUzivatele = null;

        private List<Fask.Interfaces.Filtry.VydejDavkyFiltr> filtry = new List<Fask.Interfaces.Filtry.VydejDavkyFiltr>();
        
        /// <summary>
        /// Zvolené id před seřazením
        /// </summary>
        string sortedID = string.Empty;

        public Fask.Interfaces.DataSets.Vydej.CZMST_SERow  CZMST_SE_SelectedRow
        {
            get
            {
                try
                {
                      return ((DataRowView)(dgVydej.BindingContext[bsVydej].Current)).Row as Fask.Interfaces.DataSets.Vydej.CZMST_SERow;
                }
                catch
                {
                    return null;
                }
            }
        }

        private List<Fask.Interfaces.DataSets.Vydej.CZMST_SERow> CZMST_SE_SelectedRows
        {
            //get
            //{
            //    List<Fask.Interfaces.DataSets.Vydej.CZMST_SERow> rows = new List<Fask.Interfaces.DataSets.Vydej.CZMST_SERow>();

            //    foreach (DataGridViewRow selectedRow in dgVydej.SelectedRows)
            //    {
            //        Fask.Interfaces.DataSets.Vydej.CZMST_SERow row = ((DataRowView)selectedRow.DataBoundItem).Row as Fask.Interfaces.DataSets.Vydej.CZMST_SERow;
            //        rows.Add(row);
            //    }

            //    return rows;
            //}

            get
            {
                return dgVydej.SelectedRows
                    .Cast<DataGridViewRow>()
                    .OrderBy(r => r.Index)               // zarovnání na vizuální pořadí v gridu
                    .Select(r => ((DataRowView)r.DataBoundItem).Row as Fask.Interfaces.DataSets.Vydej.CZMST_SERow)
                    .Where(r => r != null)
                    .ToList();
            }
        }


        //private List<Fask.Interfaces.DataSets.Vyroba.FASK_EventsRow> FASK_Events_selectedRows
        //{
        //    get
        //    {
        //        List<Fask.Interfaces.DataSets.Vyroba.FASK_EventsRow> rows = new List<Fask.Interfaces.DataSets.Vyroba.FASK_EventsRow>();

        //        foreach (DataGridViewRow selectedRow in dg_OdvodEvents.SelectedRows)
        //        {
        //            Fask.Interfaces.DataSets.Vyroba.FASK_EventsRow row = ((DataRowView)selectedRow.DataBoundItem).Row as Fask.Interfaces.DataSets.Vyroba.FASK_EventsRow;
        //            rows.Add(row);
        //        }

        //        return rows;
        //    }
        //}

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
        /// Vybrany filtr.
        /// </summary>
        private Fask.Interfaces.Filtry.VydejDavkyFiltr rowFiltr
        {
            get
            {
                try
                {
                    return tscbFiltry.SelectedItem as Fask.Interfaces.Filtry.VydejDavkyFiltr;
                }
                catch
                {
                    return null;
                }
            }
        }

        #region Eventy formu

        public FormDavkyVydejeList()
        {
            InitializeComponent();
            this.dgVydej.UpdateColumnHeaderCellsByDatasource();
            panelButtons.Menu = menuStrip1;
        }

        private bool opravneni = false;

        public FormDavkyVydejeList(bool opravneni) : this()
        {
            this.opravneni = opravneni;
            //this.dg_OdvodEvents.MultiSelect = allowMultiSelect;
            //this.Zobrazeni = typZobrazeni;

            if (!opravneni)
            {
                menuStrip1.Items.Remove(tsmiPolozka);
                tsmiAkce.DropDownItems.Remove(tsmiUpravit);
                tsmiAkce.DropDownItems.Remove(tsmiOdstranit);
            }

            //if (Zobrazeni == Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST)
            //{
            //    panelButtonsZobrazeniList.Menu = menuStrip2;
            //    this.WindowState = FormWindowState.Maximized;
            //}
        }

        private void FormDavkyVydejeList_Load(object sender, EventArgs e)
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

                //tsmiAkce.DropDownItems.Remove(iSPohodaToolStripMenuItem);
                iSPohodaToolStripMenuItem.Text = "Načtení";
                iSPohodaToolStripMenuItem.Enabled = false;
                //iSPohodaToolStripMenuItem.Visible = false;

                //tsmiAkce.DropDownItems.Remove(tsmiKontrolaDavky);

                tsmiKontrolaDavky.Enabled = false;


                this.ShowIcon = false;
                this.WindowState = FormWindowState.Maximized;


                // načtení konfigurace datagridu z nastavení aplikace
                this.dgVydej.LoadConfiguration(this.GetType().ToString());

                panelButtons.LoadConfiguration(this.GetType().ToString());
                panelButtons.Init();
                panelButtons.Size = new Size(85, 700);

                advancedDataGridViewSearchToolBar1.SetColumns(dgVydej.Columns);

                //// načtení konfigurace vytvořených filtrů
                this.filtry = Extensions.ListExt.ReadFromXML<Fask.Interfaces.Filtry.VydejDavkyFiltr>(this.GetType().ToString() + ".filtr");
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

                chB_Stazene.Checked = true;
                chB_Spracovane.Checked = true;
                chB_Mrtve.Checked = true;


               



                buttonVyhledat.Focus();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormDavkyVydejeList_Shown(object sender, EventArgs e)
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

        private void FormDavkyVydejeList_KeyDown(object sender, KeyEventArgs e)
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

        private void FormDavkyVydejeList_FormClosing(object sender, FormClosingEventArgs e)
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


        #endregion
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
        private bool CreateFilter(ref Fask.Interfaces.Filtry.VydejDavkyFiltr filtr)
        {
            if (filtr == null)
                filtr = new Fask.Interfaces.Filtry.VydejDavkyFiltr();

            filtr.CountEntries = cbCountEntries.Text.Trim();
            filtr.Sopnumbe = cbSopnumbe.Text.Trim();
            filtr.ITEMNMBR = cbITEMNMBR.Text.Trim();

            filtr.UvolneneDavky = chB_Uvolnene.Checked;
            filtr.NEUvolneneDavky = chB_NEUvolnene.Checked;
            filtr.SpracovaneDavky = chB_Spracovane.Checked;
            filtr.StazeneDavky = chB_Stazene.Checked;
            filtr.MrtveDavky = chB_Mrtve.Checked;

            filtr.ITEMTYPE_J = chB_vydej.Checked;
            filtr.ITEMTYPE_I = chB_inventura.Checked;
            filtr.ITEMTYPE_P = chB_prijem.Checked;
            filtr.ITEMTYPE_E = chB_expedice.Checked;
            filtr.ITEMTYPE_V = chB_vyroba.Checked;
            filtr.ITEMTYPE_O = chB_ostatni.Checked;




            filtr.PouzeKladneMnozstvy = false;
            return true;
        }

        int? Index = null;

        private void PerformOK()
        {
            try
            {
                //DataTable dtchanged = this.dsVydej.Hlavicky.GetChanges();
                //if (dtchanged != null && dtchanged.Rows.Count > 0) //obsahuje zmeny ... 
                //{
                //    //MaR zakomentoval 11.3.2024
                //    //DialogResult dr = MessageBox.Show("Obsahuje změny, pokračovat?", "data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                //    //if (dr == System.Windows.Forms.DialogResult.No)
                //    //    return;
                //}

                if (bwLoadVydejHlavicky.IsBusy)
                {
                    bwLoadVydejHlavicky.CancelAsync();
                    while (bwLoadVydejHlavicky.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }

                ProgressIndicatorStart();

                Fask.Interfaces.Filtry.VydejDavkyFiltr filtr = new Fask.Interfaces.Filtry.VydejDavkyFiltr();
                if (!CreateFilter(ref filtr))
                    return;

                int FirstDisplayedScrollingRowIndex = this.dgVydej.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

                if(CZMST_SE_SelectedRow != null)
                    Index = CZMST_SE_SelectedRow.DEX_ROW_ID;


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
                Fask.Interfaces.Filtry.VydejDavkyFiltr filtr = (Fask.Interfaces.Filtry.VydejDavkyFiltr)e.Argument;
                Fask.Interfaces.DataSets.Vydej ds = new Fask.Interfaces.DataSets.Vydej();

                if (bwLoadVydejHlavicky.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                // nacteni dat v oddelenem vlakne
                //ds = ((Fask.Interfaces.Vydej.IVydej)providerVydej).GetFiltrovaneHlavicky(filtr);

                if ((providerVydej != null) && (providerVydej is Fask.Interfaces.Vydej.IVydej2_GetFiltrovaneDavky))
                    ds = ((Fask.Interfaces.Vydej.IVydej2_GetFiltrovaneDavky)providerVydej).GetFiltrovaneDavky(filtr);
                else
                    throw new NotImplementedException("Provider neimplementuje IVydej2_GetFiltrovaneDavky.");



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



                    if (Index != null )
                    {
                        var polozky = dsVydej.CZMST_SE.Where(x => x.DEX_ROW_ID == Index);
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

                Fask.Interfaces.Filtry.VydejDavkyFiltr filtr = rowFiltr;

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

                Fask.Interfaces.Filtry.VydejDavkyFiltr filtr = new Fask.Interfaces.Filtry.VydejDavkyFiltr();
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
        private void PerformNastavitFiltr(Fask.Interfaces.Filtry.VydejDavkyFiltr filtr)
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
                cbITEMNMBR.Text = filtr.ITEMNMBR;
                chB_Uvolnene.Checked = filtr.UvolneneDavky ;
                chB_NEUvolnene.Checked = filtr.NEUvolneneDavky;

                chB_Spracovane.Checked = filtr.SpracovaneDavky;
                chB_Stazene.Checked = filtr.StazeneDavky;
                chB_Mrtve.Checked = filtr.MrtveDavky;

                chB_vydej.Checked = filtr.ITEMTYPE_J;
                chB_inventura.Checked = filtr.ITEMTYPE_I;
                chB_prijem.Checked = filtr.ITEMTYPE_P;
                chB_expedice.Checked = filtr.ITEMTYPE_E;
                chB_vyroba.Checked = filtr.ITEMTYPE_V;
                chB_ostatni.Checked = filtr.ITEMTYPE_O;

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
                    if (CZMST_SE_SelectedRow != null)
                        sortedID = CZMST_SE_SelectedRow.CountEntries.ToString();
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
                cbSopnumbe.Text = string.Empty;
                cbITEMNMBR.Text = string.Empty;
                chB_NEUvolnene.Checked = true;
                chB_Uvolnene.Checked = true;
                chB_Spracovane.Checked = true;
                chB_Stazene.Checked = true;
                chB_Mrtve.Checked = true;

                chB_vydej.Checked = true;
                chB_inventura.Checked = true;
                chB_prijem.Checked = true;
                chB_expedice.Checked = true;
                chB_vyroba.Checked = true;
                chB_ostatni.Checked = true;

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Generovat data 


       private void GenerateDavka(int? CountEntries, string SKL_ID)
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


                DataDavkyGenerate data = new DataDavkyGenerate();
                string DescSklad = string.Empty;

                Fask.Interfaces.Classes.Sklad sklad = new Fask.Interfaces.Classes.Sklad();
                Fask.Interfaces.DataSets.Sklady.CZMST093Row RowSklad;

                if (CountEntries != null)
                {
                    sklad.ID = SKL_ID.Trim();

                    if ((providerVydej != null) && (providerVydej is Fask.Interfaces.Ciselniky.Sklady.ISklady2_GetSkladByID))
                        RowSklad = ((Fask.Interfaces.Ciselniky.Sklady.ISklady2_GetSkladByID)providerVydej).GetSkladByID(SKL_ID.Trim());
                    else
                        throw new NotImplementedException("Provider neimplementuje ISklady2_GetSkladByID.");

                    DescSklad = RowSklad.skl_desc.Trim();


                }
                else
                {

                    using (Ciselniky.FormSkladyList frmSklad = new Ciselniky.FormSkladyList(false, null, Fask.Interfaces.Classes.ZOBRAZENI_TYP.VYBER))
                    {
                        frmSklad.Text = "Výběr Sklad";

                        if (frmSklad.ShowDialog(this) != DialogResult.OK)
                        {
                            ProgressIndicatorStop();
                            return;
                        }

                        sklad.ID = frmSklad.CZMST093_selectedRow.skl_id.Trim();
                        DescSklad = frmSklad.CZMST093_selectedRow.skl_desc.Trim();
                    }
                }

                data.Davka = CountEntries;

                string TextOut = CountEntries == null ? string.Format("Zadejte číslo dokladu." + Environment.NewLine + "Pro sklad: '{0}'.", DescSklad) : string.Format("Zadejte číslo dokladu." + Environment.NewLine + "Pro sklad: '{0}'." + Environment.NewLine + "Sloučení do dávky: '{1}'", DescSklad, CountEntries);

                string sopnumber = string.Empty;
                DialogResult dr = Forms.InputBox.Show("Číslo dokladu", TextOut, string.Empty, false, out sopnumber);
                if (dr != System.Windows.Forms.DialogResult.OK)
                {
                    ProgressIndicatorStop();
                    return;
                }

                //TaD 23.9.2020 Kontrola na existenci dávky




                Fask.Interfaces.Classes.Objednavka objednavka = new Fask.Interfaces.Classes.Objednavka();

                objednavka.ID = sopnumber;
                objednavka.CisloDavky = string.Empty;

                data.sklad = sklad;
                data.objednavka = objednavka;

                bwExport.RunWorkerAsync(data);

            }
            catch (Exception ex)
            {
                ProgressIndicatorStop();
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #region MaR 16.6.2026 zakomentovano
        //private void bwExport_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    try
        //    {
        //        //dsVydej = (Fask.Interfaces.DataSets.Vydej)e.Result;
        //         Fask.Interfaces.Classes.StatusInfo si = (Fask.Interfaces.Classes.StatusInfo)e.Result;

        //        // check error, check cancel, then use result
        //        if (e.Error != null)
        //        {
        //            // handle the error

        //        }
        //        else if (e.Cancelled)
        //        {
        //            //handle the cancelled
        //        }
        //        else
        //        {
        //            // uspesne dokonceno ...

        //            if (si.ID < 0)
        //            {
        //                MessageBox.Show(si.Description, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            }


        //            chB_Mrtve.Checked = false;
        //            chB_NEUvolnene.Checked = true;
        //            chB_Spracovane.Checked = false;
        //            chB_Stazene.Checked = false;
        //            chB_Uvolnene.Checked = false;


        //            PerformOK();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
        //        MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    finally
        //    {
        //        ProgressIndicatorStop();
        //    }
        //} 
        #endregion

        private void bwExport_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null)
                {
                    // Došlo k chybě během zpracování (např. výjimka v DoWork)
                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, e.Error);
                    MessageBox.Show("Při exportu došlo k chybě:\n" + e.Error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (e.Cancelled)
                {
                    // Uživatelské zrušení
                    MessageBox.Show("Operace byla zrušena uživatelem.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (e.Result == null)
                {
                    MessageBox.Show("Nebyl vrácen žádný výsledek.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var si = e.Result as Fask.Interfaces.Classes.StatusInfo;
                if (si == null)
                {
                    MessageBox.Show("Neočekávaný typ výsledku operace.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                // Ověření statusu
                if (si.ID < 0)
                {
                    MessageBox.Show(si.Description ?? "Neznámá chyba.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Úspěšné dokončení
                chB_Mrtve.Checked = false;
                chB_NEUvolnene.Checked = true;
                chB_Spracovane.Checked = false;
                chB_Stazene.Checked = false;
                chB_Uvolnene.Checked = false;

                chB_vydej.Checked = true;
                chB_inventura.Checked = true;
                chB_prijem.Checked = true;
                chB_expedice.Checked = true;
                chB_vyroba.Checked = true;
                chB_ostatni.Checked = true;


                PerformOK();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show("Neočekávaná chyba:\n" + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                DataDavkyGenerate data = (DataDavkyGenerate)e.Argument;

                if (bwExport.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                // nacteni dat v oddelenem vlakne
                //ds = ((Fask.Interfaces.Vydej.IVydej)providerVydej).GetFiltrovaneHlavicky(filtr);

                Fask.Interfaces.Classes.StatusInfo si;

                if ((providerVydej != null) && (providerVydej is Fask.Interfaces.Vydej.IVydej2_GenerateDavka))
                    si = ((Fask.Interfaces.Vydej.IVydej2_GenerateDavka)providerVydej).Vydej_GenerateDavka(data.objednavka, data.sklad, data.Davka);
                else
                    throw new NotImplementedException("Provider neimplementuje IVydej2_GenerateDavka.");



                if (bwLoadVydejHlavicky.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                e.Result = si;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion



        private void PerformEditRecord()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (CZMST_SE_SelectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro úpravu", this.Text, MessageBoxButtons.OK);
                    return;
                }


                if (dgVydej.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Je vybráno více záznamů než jeden.", this.Text, MessageBoxButtons.OK);
                    return;
                }

                using (FormDavkyVydejeEdit frmuziv = new FormDavkyVydejeEdit())
                {
                    frmuziv.rowDavkaEdit = CZMST_SE_SelectedRow;
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

                PerformOK();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void buttonKontrola_Click(object sender, EventArgs e)
        {
            PerformCheck();
        }

        private void PerformCheck()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (CZMST_SE_SelectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro úpravu", this.Text, MessageBoxButtons.OK);
                    return;
                }


                if (dgVydej.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Je vybráno více záznamů než jeden.", this.Text, MessageBoxButtons.OK);
                    return;
                }

                using (FormDavkyVydejeKontrola frmuziv = new FormDavkyVydejeKontrola())
                {
                    frmuziv.CountEntriesCurrent = CZMST_SE_SelectedRow.CountEntries;
                    frmuziv.Text = string.Format("Číslo dávky: {0} ", CZMST_SE_SelectedRow.CountEntries);
                    //if (frmuziv.ShowDialog(this) != DialogResult.OK)
                    //    return;
                    frmuziv.ShowDialog(this);
                }

                PerformOK();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

                if (CZMST_SE_SelectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro úpravu", this.Text, MessageBoxButtons.OK);
                    return;
                }


                if (dgVydej.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Je vybráno více záznamů než jeden.", this.Text, MessageBoxButtons.OK);
                    return;
                }

                Fask.Interfaces.DataSets.Vydej ds;
                Fask.Interfaces.Filtry.VydejDavkyFiltr filtr = new Fask.Interfaces.Filtry.VydejDavkyFiltr();

                filtr.CountEntries = CZMST_SE_SelectedRow.CountEntries.ToString();
                filtr.MrtveDavky = true;
                filtr.NEUvolneneDavky = true;
                filtr.SpracovaneDavky = true;
                filtr.StazeneDavky = true;
                filtr.UvolneneDavky = true;
                filtr.PouzeKladneMnozstvy = true;

                if ((providerVydej != null) && (providerVydej is Fask.Interfaces.Vydej.IVydej2_GetFiltrovaneDavky))
                    ds = ((Fask.Interfaces.Vydej.IVydej2_GetFiltrovaneDavky)providerVydej).GetFiltrovaneDavky(filtr);
                else
                    throw new NotImplementedException("Provider neimplementuje IVydej2_GetFiltrovaneDavky.");



                IEnumerable<IGrouping<string, Fask.Interfaces.DataSets.Vydej.CZMST_SERow>>  grup = ds.CZMST_SE.GroupBy(x => x.SOPNUMBE);
                //List<string> SOPNUMBERList = new List<string>();

                Fask.Interfaces.DataSets.Vydej.CZMST_SERow First = null;
                //bool first = true;
                string SOPNUMBE;
                string VNDDOCNM;

                var gruplist = grup.ToList();
                First = gruplist[0].ToList()[0];

                if(gruplist.Count > 1)
                {
                    SOPNUMBE = "-";
                    VNDDOCNM = "-";
                }
                else
                {
                    SOPNUMBE = First.SOPNUMBE.Trim();
                    VNDDOCNM = First.VNDDOCNM.Trim();
                }


                //First = gruplist[0].ToList()[0];

                //foreach (IGrouping<string, Fask.Interfaces.DataSets.Vydej.CZMST_SERow> item in grup)
                //{
                //    item.ToArray();
                //    if (!string.IsNullOrEmpty(item.Key))
                //    {
                //        if (first)
                //        {
                //            var a = item.GetEnumerator();
                //            First = a.Current;
                //            first = false;
                //        }
                //        SOPNUMBERList.Add(item.Key.Trim()); 
                //    }
                    
                //}

               

                //if (SOPNUMBERList.Count > 1)
                //    NUMOBJ = "-";
                //else
                //    NUMOBJ = SOPNUMBERList[0].Trim();

                Fask.Interfaces.DataSets.Vydej dsAdresa;

                if ((providerVydej != null) && (providerVydej is Fask.Interfaces.Vydej.IVydej2_GetAdresaOdberatel))
                    dsAdresa = ((Fask.Interfaces.Vydej.IVydej2_GetAdresaOdberatel)providerVydej).GetAdresaOdberatel(First.SOPNUMBE);
                else
                    throw new NotImplementedException("Provider neimplementuje IVydej2_GetAdresaOdberatel.");

                //Fask.Interfaces.DataSets.Vydej.Adresy_OdberatelRow AdresaRow = null;
                Fask.Interfaces.DataSets.Vydej.OBJ_AdresyRow OBJAdresaRow = null;


                string ICO = string.Empty;
                string DIC = string.Empty;
                string fax = string.Empty;


                string Firma = string.Empty;
                string Ulice = string.Empty;
                string PSC = string.Empty;
                string Obec = string.Empty;
                string tel = string.Empty;
                string email = string.Empty;

                if ((dsAdresa != null) && (dsAdresa.OBJ_Adresy.Count > 0))
                {
                    OBJAdresaRow = dsAdresa.OBJ_Adresy.First();

                    ICO = OBJAdresaRow.IsICONull() ? "-" : OBJAdresaRow.ICO.Trim();
                    DIC = OBJAdresaRow.IsDICNull() ? "-" : OBJAdresaRow.DIC.Trim();
                    fax = OBJAdresaRow.IsFaxNull() ? "-" : OBJAdresaRow.Fax.Trim();


                                        if (
                        !OBJAdresaRow.IsFirma2Null() ||
                        !OBJAdresaRow.IsUlice2Null() ||
                        !OBJAdresaRow.IsPSC2Null() ||
                        !OBJAdresaRow.IsObec2Null() ||
                        !OBJAdresaRow.IsTel2Null() ||
                        !OBJAdresaRow.IsEmail2Null()
                        )
                    {

                        Firma = OBJAdresaRow.IsFirma2Null() ? "-" : OBJAdresaRow.Firma2.Trim();
                        Ulice = OBJAdresaRow.IsUlice2Null() ? "-" : OBJAdresaRow.Ulice2.Trim();
                        PSC = OBJAdresaRow.IsPSC2Null() ? "-" : OBJAdresaRow.PSC2.Trim();
                        Obec = OBJAdresaRow.IsObec2Null() ? "-" : OBJAdresaRow.Obec2.Trim();
                        tel = OBJAdresaRow.IsTel2Null() ? "-" : OBJAdresaRow.Tel2.Trim();
                        email = OBJAdresaRow.IsEmail2Null() ? "-" : OBJAdresaRow.Email2.Trim();
                    }
                    else
                    {
                        Firma = OBJAdresaRow.IsFirmaNull() ? "-" : OBJAdresaRow.Firma.Trim();
                        Ulice = OBJAdresaRow.IsUliceNull() ? "-" : OBJAdresaRow.Ulice.Trim();
                        PSC = OBJAdresaRow.IsPSCNull() ? "-" : OBJAdresaRow.PSC.Trim();
                        Obec = OBJAdresaRow.IsObecNull() ? "-" : OBJAdresaRow.Obec.Trim();
                        tel = OBJAdresaRow.IsTelNull() ? "-" : OBJAdresaRow.Tel.Trim();
                        email = OBJAdresaRow.IsEmailNull() ? "-" : OBJAdresaRow.Email.Trim();
                    }



                }

                Fask.Interfaces.DataSets.Vydej dstmp = new Fask.Interfaces.DataSets.Vydej();
                dstmp.Adresy_Odberatel.AddAdresy_OdberatelRow(  Firma ,
                                                                Ulice ,
                                                                PSC ,
                                                                Obec,
                                                                ICO ,
                                                                DIC,
                                                                tel,
                                                                fax,
                                                                email);


                PrintReport(ds, CZMST_SE_SelectedRow.CountEntries.ToString(), First.SOPNUMBE, SOPNUMBE, VNDDOCNM, dstmp.Adresy_Odberatel.First());


            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintReport(Fask.Interfaces.DataSets.Vydej ds, string CountEntries, string SOPNUMBE_IMG, string SOPNUMBE, string VNDDOCNM, Fask.Interfaces.DataSets.Vydej.Adresy_OdberatelRow AdresaRow)
        {

            //logovat tisky MaR 8.11.2024
            // plr.DataTable = ds.CZMST_SE;
            if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
            {
                try
                {
                    foreach (var item in ds.CZMST_SE)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start vydej s predlohou/predloha TISK ???-------------------------------------");

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

                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END vydej s predlohou/predloha TISK ???-------------------------------------");
                    }

                }
                catch (Exception ex)
                {

                    throw;
                }
            }



            using (PrintReportLibrary.CoolPrintPreviewDialog plr = new PrintReportLibrary.CoolPrintPreviewDialog())
            {

                plr.Params = new Microsoft.Reporting.WinForms.ReportParameter[]
                {
                    new Microsoft.Reporting.WinForms.ReportParameter("Param_Dodavatel_Firma", Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Vydej[0].Vydej_Param_Dodavatel_Firma),
                    new Microsoft.Reporting.WinForms.ReportParameter("Param_Dodavatel_Adresa", Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Vydej[0].Vydej_Param_Dodavatel_Adresa),
                    new Microsoft.Reporting.WinForms.ReportParameter("Param_Dodavatel_PSC", Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Vydej[0].Vydej_Param_Dodavatel_PSC),
                    new Microsoft.Reporting.WinForms.ReportParameter("Param_Dodavatel_Obec", Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Vydej[0].Vydej_Param_Dodavatel_Obec),
                    //new Microsoft.Reporting.WinForms.ReportParameter("Param_Dodavatel_ICO", "64086551"),
                    //new Microsoft.Reporting.WinForms.ReportParameter("Param_Dodavatel_DIC", "DE259853840"),
                    //new Microsoft.Reporting.WinForms.ReportParameter("Param_Dodavatel_Telefon", "+42059663713,"),
                    //new Microsoft.Reporting.WinForms.ReportParameter("Param_Dodavatel_Fax", "+420596637130"),
                    //new Microsoft.Reporting.WinForms.ReportParameter("Param_Dodavatel_Email", "info@i-tec.cz"),
                    //new Microsoft.Reporting.WinForms.ReportParameter("Param_Dodavatel_WWW", "www.i-tec.cz"),

                    new Microsoft.Reporting.WinForms.ReportParameter("Param_Odberatel_Firma", (AdresaRow== null ? "-" :  (AdresaRow.IsParam_Odberatel_FirmaNull() ? "-" :  AdresaRow.Param_Odberatel_Firma))),
                    new Microsoft.Reporting.WinForms.ReportParameter("Param_Odberatel_Adresa", (AdresaRow== null ? "-" :  (AdresaRow.IsParam_Odberatel_AdresaNull() ? "-" :  AdresaRow.Param_Odberatel_Adresa))),
                    new Microsoft.Reporting.WinForms.ReportParameter("Param_Odberatel_PSC", (AdresaRow== null ? "-" :  (AdresaRow.IsParam_Odberatel_PSCNull() ? "-" :  AdresaRow.Param_Odberatel_PSC))),
                    new Microsoft.Reporting.WinForms.ReportParameter("Param_Odberatel_Obec", (AdresaRow== null ? "-" :  (AdresaRow.IsParam_Odberatel_ObecNull() ? "-" :  AdresaRow.Param_Odberatel_Obec))),
                    new Microsoft.Reporting.WinForms.ReportParameter("Param_Odberatel_ICO", (AdresaRow== null ? "-" :  (AdresaRow.IsParam_Odberatel_ICONull() ? "-" :  AdresaRow.Param_Odberatel_ICO))),
                    new Microsoft.Reporting.WinForms.ReportParameter("Param_Odberatel_DIC", (AdresaRow== null ? "-" :  (AdresaRow.IsParam_Odberatel_DICNull() ? "-" :  AdresaRow.Param_Odberatel_DIC))),
                    new Microsoft.Reporting.WinForms.ReportParameter("Param_Odberatel_Telefon", (AdresaRow== null ? "-" :  (AdresaRow.IsParam_Odberatel_TelefonNull() ? "-" :  AdresaRow.Param_Odberatel_Telefon))),
                    new Microsoft.Reporting.WinForms.ReportParameter("Param_Odberatel_Fax", (AdresaRow== null ? "-" :  (AdresaRow.IsParam_Odberatel_FaxNull()  ? "-" :  AdresaRow.Param_Odberatel_Fax))),
                    new Microsoft.Reporting.WinForms.ReportParameter("Param_Odberatel_Email", (AdresaRow== null ? "-" :  (AdresaRow.IsParam_Odberatel_EmailNull() ? "-" :  AdresaRow.Param_Odberatel_Email))),
                    //new Microsoft.Reporting.WinForms.ReportParameter("Param_Odberatel_WWW", "www.imgramko.cz"),

                    new Microsoft.Reporting.WinForms.ReportParameter("Param_PrijatyDoklad", VNDDOCNM),
                    new Microsoft.Reporting.WinForms.ReportParameter("Param_Datum", DateTime.Now.ToShortDateString())

                    
                    //new Microsoft.Reporting.WinForms.ReportParameter("DatumOd",date.First().Modified == null ? "-" : date.First().Modified.ToShortDateString() ),
                    //new Microsoft.Reporting.WinForms.ReportParameter("DatumDo",date.Last().Modified == null ? "-" : date.Last().Modified.ToShortDateString()),
                    //new Microsoft.Reporting.WinForms.ReportParameter("Status" ,status)


                   
                };

                plr.NazevDataTable = "DataSet";
                plr.DataTable = ds.CZMST_SE;

                List<PrintReportLibrary.TypeData> tmplist = new List<PrintReportLibrary.TypeData>();
                tmplist.Add(PrintReportLibrary.TypeData.DataTable);
                plr.typedata = new List<PrintReportLibrary.TypeData>(tmplist);
                plr.Projekt = PrintReportLibrary.Projekt.MST;
                plr.ShowPreview = true;
                plr.Path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Servis", "Print", "Templates", Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Vydej[0].DavkyTemplate);
                
                plr.CountEntries = CountEntries;
                plr.HlavickaKod = SOPNUMBE;
                plr.HlavickaKodIMG = SOPNUMBE_IMG.Trim();
                
                plr.Print(this);

            }
        }



        //private void PerformPrint2()
        //{
        //    try
        //    {
        //        if (!MySystem.LoginTest.UserLoginTest())
        //            return;

        //        if (SelectedRow == null)
        //        {
        //            MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
        //            return;
        //        }

        //        if (dgVydej.SelectedRows.Count > 1)
        //        {
        //            //MessageBox.Show("Je vybráno více záznamů než jeden.", this.Text, MessageBoxButtons.OK);
        //            //return;
        //        }

        //        //Fask.Interfaces.DataSets.Vydej.CZMST_SEDataTable dt = new Fask.Interfaces.DataSets.Vydej.CZMST_SEDataTable();

        //        //foreach (Fask.Interfaces.DataSets.Vydej.CZMST_SERow row in Production_selectedRows)
        //        //{
        //        //    Fask.Interfaces.DataSets.Vydej.CZMST_SERow newRow = dt.NewProduction_KonzolaRow();
        //        //    // Přenést data z původního řádku do nového řádku
        //        //    newRow.ItemArray = row.ItemArray.Clone() as object[];
        //        //    dt.AddProduction_KonzolaRow(newRow);
        //        //}

        //        Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable dt = new Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable();

        //        foreach (Fask.Interfaces.DataSets.Vyroba.FASK_EventsRow row in FASK_Events_selectedRows)
        //        {
        //            try
        //            {
        //                Fask.Interfaces.DataSets.Vyroba.FASK_EventsRow newRow = dt.NewFASK_EventsRow();
        //                // Ošetřit delší vstupní pole než počet sloupců tabulky
        //                int copyLength = Math.Min(row.ItemArray.Length, dt.Columns.Count);
        //                newRow.ItemArray = row.ItemArray.Take(copyLength).ToArray();
        //                dt.AddFASK_EventsRow(newRow);
        //            }
        //            catch (Exception ex)
        //            {
        //                // Ošetření vyjimky
        //                // Můžete zde provést logování chyby nebo jiné požadované akce
        //                Fask.Logging.ExceptionHandler2.Handle(ex);
        //            }
        //        }


        //        PrintReport2(dt);

        //    }
        //    catch (Exception ex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle(ex);
        //    }
        //}


        private void PrintReport2(Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable dt)
        {
            try
            {

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


                    #region new vybrani tiskove  sablony
                    // Vytvoření instance formuláře
                    FormOdvod_TiskoveSablony form = new FormOdvod_TiskoveSablony(this.Text, FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Admin());
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
                        PrintReport2(dt);
                    }

                }
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
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

        private void tsmiUpravit_Click(object sender, EventArgs e)
        {
            PerformEditRecord();
        }

        private void tsmiOdstranit_Click(object sender, EventArgs e)
        {
            PerformDeleteRecord();
        }

        private void PerformDeleteRecord()
        {
            try
            {

                if (CZMST_SE_SelectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro smazani", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if (dgVydej.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Je vybráno více záznamů než jeden.", this.Text, MessageBoxButtons.OK);
                    return;
                }





                if ((providerVydej != null) && (providerVydej is Fask.Interfaces.Vydej.IVydej2_DeleteSE))
                    ((Fask.Interfaces.Vydej.IVydej2_DeleteSE)providerVydej).DeleteSE(CZMST_SE_SelectedRow.DEX_ROW_ID);
                else
                    throw new NotImplementedException("Provider neimplementuje IVydej2_DeleteSE.");




                PerformOK();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsmiRekapitulace_Click(object sender, EventArgs e)
        {
            using (Form_Predloha_Rekapitulace frm = new Form_Predloha_Rekapitulace())
            {
                frm.ds_rekap = dsVydej;

                frm.ShowDialog();

            }
        }

        private void tsmiKontrolaDavky_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Neimplementováno." +
                "", this.Text, MessageBoxButtons.OK);
            //PerformCheck();
        }

        private void tsmiGenerovatDavku_Click(object sender, EventArgs e)
        {
            GenerateDavka(null, string.Empty);
        }

        private void tsmiSloucitDavku_Click(object sender, EventArgs e)
        {
            try
            {

                if (CZMST_SE_SelectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro sloučení", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if (dgVydej.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Je vybráno více záznamů než jeden.", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if (CZMST_SE_SelectedRow.CZ_Doslo == 0)
                {
                    MessageBox.Show("Nelze sloučit data do již uvolněné dávky!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if ((CZMST_SE_SelectedRow.CZ_Doslo > 0) && (CZMST_SE_SelectedRow.CZ_Doslo < 100))
                {
                    MessageBox.Show("Nelze sloučit data do již stažené dávky!", this.Text, MessageBoxButtons.OK);
                    return;
                }


                if ((CZMST_SE_SelectedRow.CZ_Doslo > 100) && (CZMST_SE_SelectedRow.CZ_Doslo < 200))
                {
                    MessageBox.Show("Nelze sloučit data do již spracované dávky!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if (CZMST_SE_SelectedRow.CZ_Doslo == 201)
                {
                    MessageBox.Show("Nelze sloučit data do již stornované dávky!", this.Text, MessageBoxButtons.OK);
                    return;
                }


                GenerateDavka(CZMST_SE_SelectedRow.CountEntries, CZMST_SE_SelectedRow.SKL_ID);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsmiUvolnitDavku_Click(object sender, EventArgs e)
        {
            //
            //Uvolnovat podle SOPNUMBER?
            //uvolnovat řadek po řadku?
            // vyskakovaci okno ktere vypiše seznam všech SOPNUMBER vyexportovanych s možnosti uvolnit?
            //....
            try
            {

                if (CZMST_SE_SelectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam uvolnení", this.Text, MessageBoxButtons.OK);
                    return;
                }


                if (dgVydej.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Je vybráno více záznamů než jeden.", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if ((providerVydej != null) && (providerVydej is Fask.Interfaces.Vydej.IVydej2_UvolnitDavku))
                    ((Fask.Interfaces.Vydej.IVydej2_UvolnitDavku)providerVydej).Vydej_UvolnitDavku(CZMST_SE_SelectedRow.CountEntries);
                else
                    throw new NotImplementedException("Provider neimplementuje IVydej2_UvolnitDavku.");


                PerformOK();

            }
            catch (Exception ex)
            {

                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        #region storno davky
        private void stornovatToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {

                if (bw_storno.IsBusy)
                {
                    bw_storno.CancelAsync();
                    while (bw_storno.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }

                ProgressIndicatorStart();

                // vybrat vybrane data a naplnit datatable
                Fask.Interfaces.DataSets.Vydej.CZMST_SEDataTable dt = new Fask.Interfaces.DataSets.Vydej.CZMST_SEDataTable();

                foreach (DataGridViewRow item in SelectedRows)
                {
                    Fask.Interfaces.DataSets.Vydej.CZMST_SERow row = ((DataRowView)item.DataBoundItem).Row as Fask.Interfaces.DataSets.Vydej.CZMST_SERow;

                    dt.ImportRow(row);
                }
                dt.AcceptChanges();

                bw_storno.RunWorkerAsync(dt);
                //bw_storno.RunWorkerAsync();

            }
            catch (Exception ex)
            {
                ProgressIndicatorStop();
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void bw_storno_DoWork(object sender, DoWorkEventArgs e)
        {

            try
            {
                //DataDavkyImport data = (DataDavkyImport)e.Argument;

                if (bw_storno.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }


                //pretypovat na datatable
                Fask.Interfaces.DataSets.Vydej.CZMST_SEDataTable dt = (Fask.Interfaces.DataSets.Vydej.CZMST_SEDataTable)e.Argument;

                //Fask.Interfaces.Classes.StatusInfo si;
                int pocet = 0;
                if ((providerVydej != null) && (providerVydej is Fask.Interfaces.Vydej.IVydej2_UpdateSE_storno))
                    pocet = ((Fask.Interfaces.Vydej.IVydej2_UpdateSE_storno)providerVydej).UpdateSE_storno(dt);
                else
                    throw new NotImplementedException("Provider neimplementuje IVydej2_UpdateSE_storno.");


               
                //metoda pro storno, vstup datatable

                e.Result = pocet;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;

                //MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bw_storno_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            try
            {
                //dsVydej = (Fask.Interfaces.DataSets.Vydej)e.Result;
                int si = 0;
                try
                {

                    si = (int)e.Result;

                }
                catch
                { }

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
                    MessageBox.Show(string.Format("Pocet aktualizovaných záznamů: {0}", si));
                   

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

        #endregion

        private void tiskNovyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //PerformPrint2();
            //MaR 18.12. 2024



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

                if (CZMST_SE_SelectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                #region MaR 6.11.2024 vypis zpl do souboru selected rows
                int index_zaznamu = 0;

                foreach (var item in CZMST_SE_SelectedRows)
                {

                    if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start sklady/transakce/VydejSPredlohou_Predloha TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");

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

                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END sklady/transakce/VydejSPredlohou_Predloha TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");


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



                    foreach (var item in CZMST_SE_SelectedRows)
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
                    Tisk_selected_rows_Primy_Tisk(plr_Path, true, MN_ToTisk, CZMST_SE_SelectedRows, NazevVychoziTiskarny);

                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Tisk_selected_rows_Primy_Tisk(string plr_Path, bool MnozstvuAutoJedna, int MN_ToTisk, List<Fask.Interfaces.DataSets.Vydej.CZMST_SERow> selected_Rows, string nazevVychTiskarny)
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

                if (CZMST_SE_SelectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                //if (Production_rowProducts.Count > 1)
                //{
                //    //MessageBox.Show("Je vybráno více záznamů než jeden.", this.Text, MessageBoxButtons.OK);
                //    //return;
                //}

                Fask.Interfaces.DataSets.Vydej.CZMST_SEDataTable dt = new Fask.Interfaces.DataSets.Vydej.CZMST_SEDataTable();

                foreach (Fask.Interfaces.DataSets.Vydej.CZMST_SERow row in CZMST_SE_SelectedRows)
                {
                    try
                    {
                        Fask.Interfaces.DataSets.Vydej.CZMST_SERow newRow = dt.NewCZMST_SERow();
                        // Ošetřit delší vstupní pole než počet sloupců tabulky
                        int copyLength = Math.Min(row.ItemArray.Length, dt.Columns.Count);
                        newRow.ItemArray = row.ItemArray.Take(copyLength).ToArray();
                        dt.AddCZMST_SERow(newRow);
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

        private void PrintReport_RDLC(Fask.Interfaces.DataSets.Vydej.CZMST_SEDataTable dt, bool primyTisk, string path_sablona, string nazevVychTiskarny, int pocetVytisku)
        {
            try
            {

                #region logovani tisku
                if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                {
                    foreach (var item in dt)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start sklady/transakce/VydejSPredlohou_Predloha TISK rdlc-------------------------------------");

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

                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END sklady/transakce/VydejSPredlohou_Predloha TISK rdlc-------------------------------------");
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

                if (CZMST_SE_SelectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                #region MaR 6.11.2024 vypis zpl do souboru selected rows
                int index_zaznamu = 0;

                foreach (var item in CZMST_SE_SelectedRows)
                {

                    if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start sklady/transakce/VydejSPredlohou_Predloha TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");

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

                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END sklady/transakce/VydejSPredlohou_Predloha TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");


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



                    foreach (var item in CZMST_SE_SelectedRows)
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
                    Tisk_selected_rows(plr_Path, true, MN_ToTisk, CZMST_SE_SelectedRows);

                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Tisk_selected_rows(string plr_Path, bool MnozstvuAutoJedna, int MN_ToTisk, List<Fask.Interfaces.DataSets.Vydej.CZMST_SERow> selected_Rows)
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
            Fask.Interfaces.DataSets.Vydej.CZMST_SERow row,
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

                Fask.Interfaces.DataSets.Vydej.CZMST_SEDataTable dt = new Fask.Interfaces.DataSets.Vydej.CZMST_SEDataTable();

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

        private Dictionary<string, string> PrepareDataToTisk(Fask.Interfaces.DataSets.Vydej.CZMST_SERow row_data)
        {
            try
            {
                while (true)
                {
                    try
                    {
                        Dictionary<string, string> data = new Dictionary<string, string>();


                        data.Add("SOURCE", "Konzola");

                        Fask.Interfaces.DataSets.Vydej.CZMST_SEDataTable dt = new Fask.Interfaces.DataSets.Vydej.CZMST_SEDataTable();


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

        private void iSPohodaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MaR 18.12.2024 odvedeni davky do IS POHODA

            //koncept:

            // 1)ziskani radku zaznamu
            // 2)validace radku?
            // 3)komunikace s POHODA
            // 4)zaslani dat do IS Pohoda
            // 5)zaverecny vysledek
            throw new NotImplementedException();


            OdvedDoISPohoda();



        }

        /// <summary>
        /// Metoda zabyvajici se odvedenim davky do IS pohoda
        /// </summary>
        private void OdvedDoISPohoda()
        {
            //overit vybrany zaznam

            try
            {

                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (CZMST_SE_SelectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro odeslání do IS Pohoda!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                KomunikaceISPohoda();




            }
            catch (Exception ex)
            {

                Fask.Logging.ExceptionHandler2.Handle(ex);
            }

            //throw new NotImplementedException();
        }

        /// <summary>
        /// Metoda pro otevreni a komunikace s IS POHODA
        /// </summary>
        private void KomunikaceISPohoda()
        {
            throw new NotImplementedException();
        }
    }

    public class DataDavkyGenerate 
    {
        public Fask.Interfaces.Classes.Sklad sklad;
        public Fask.Interfaces.Classes.Objednavka objednavka;
        public int? Davka;

    
    }
}

