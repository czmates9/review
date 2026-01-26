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
using System.Reflection;
using System.IO;
using Konzola.Vyroba.Rozbory;
using Konzola.Forms;
using MST_Print_Server_ZPL_Printing;

namespace Konzola.Ciselniky
{
    public partial class FormStrediskaList : Form
    {

        #region private promenne s opravnenim

        private bool opravneniEditace = false;
        private bool opravneniImport = false;
        private bool opravneniArchivace = false;
        private Opravneni opravneni;
        private Fask.Interfaces.IMES providerTisk = null; //**DONE

        private void SetOpravneni()
        {

            if (opravneni.HasFlag(Opravneni.Editace) && opravneni.HasFlag(Opravneni.Archivace) && opravneni.HasFlag(Opravneni.Import))
            {
                opravneniEditace = true;
                opravneniImport = true;
                opravneniArchivace = true;
                // Povolit funkce pro oba případy
                //MessageBox.Show("Máte oprávnění k editaci i importu.");
            }

            // Nastavíte možnosti formuláře na základě oprávnění
            if (opravneni.HasFlag(Opravneni.Editace))
            {
                opravneniEditace = true;
                // Povolit funkce pro editaci
                // například povolit nějaké tlačítka nebo editační pole
            }

            if (opravneni.HasFlag(Opravneni.Import))
            {
                opravneniImport = true;
                // Povolit funkce pro import
                // například povolit tlačítko nebo nabídku pro import
            }

            if (opravneni.HasFlag(Opravneni.Archivace))
            {
                opravneniArchivace = true;
                // Povolit funkce pro import
                // například povolit tlačítko nebo nabídku pro import
            }

            // Zkontroluje, zda má uživatel obě oprávnění


            // Můžete přidat další logiku pro další oprávnění
        }


        public FormStrediskaList( Opravneni opravneni)
        {
            InitializeComponent();
            this.dgStrediska.UpdateColumnHeaderCellsByDatasource();
            panelButtons.Menu = menuStrip2;

            // Uloží oprávnění pro další použití v programu
            this.opravneni = opravneni;

            // Nastavení podle oprávnění
            SetOpravneni();
        }



        #endregion


        /// <summary>
        /// Zvolené id před seřazením
        /// </summary>
        string sortedID = string.Empty;

        private Fask.Interfaces.IMES providerStrediska = null;
        /// <summary>
        /// Uživatel, který data upravuje
        /// </summary>
        //public Fask.Interfaces.DataSets.Konzola.FASK_LoginsRow loginrow { get; set; }
        public Fask.Interfaces.DataSets.Strediska.CZMST091Row CZMST091_selectedRow
        {
            get
            {
                try
                {
                    return ((DataRowView)(dgStrediska.BindingContext[bsStrediska].Current)).Row as Fask.Interfaces.DataSets.Strediska.CZMST091Row;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        private List<Fask.Interfaces.DataSets.Strediska.CZMST091Row> CZMST091_selectedRows
        {
            //get
            //{
            //    List<Fask.Interfaces.DataSets.Strediska.CZMST091Row> rows = new List<Fask.Interfaces.DataSets.Strediska.CZMST091Row>();

            //    foreach (DataGridViewRow selectedRow in dgStrediska.SelectedRows)
            //    {
            //        Fask.Interfaces.DataSets.Strediska.CZMST091Row row = ((DataRowView)selectedRow.DataBoundItem).Row as Fask.Interfaces.DataSets.Strediska.CZMST091Row;
            //        rows.Add(row);
            //    }

            //    return rows;
            //}

            get
            {
                return dgStrediska.SelectedRows
                    .Cast<DataGridViewRow>()
                    .OrderBy(r => r.Index)               // zarovnání na vizuální pořadí v gridu
                    .Select(r => ((DataRowView)r.DataBoundItem).Row as Fask.Interfaces.DataSets.Strediska.CZMST091Row)
                    .Where(r => r != null)
                    .ToList();
            }
        }

        private List<Fask.Interfaces.Filtry.StrediskaListFiltr> filtry = new List<Fask.Interfaces.Filtry.StrediskaListFiltr>();

        private Fask.Interfaces.Filtry.StrediskaListFiltr rowFiltr
        {
            get
            {
                try
                {
                    return tscbFiltry.SelectedItem as Fask.Interfaces.Filtry.StrediskaListFiltr;
                }
                catch
                {
                    return null;
                }
            }
        }

        #region Eventy formu

        public FormStrediskaList()
        {
            InitializeComponent();
            this.dgStrediska.UpdateColumnHeaderCellsByDatasource();
            panelButtons.Menu = menuStrip2;
        }

        private void FormStrediskaList_Load(object sender, EventArgs e)
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


                this.ShowIcon = false;
                this.WindowState = FormWindowState.Maximized;
                this.dgStrediska.LoadConfiguration(this.GetType().ToString());

                panelButtons.LoadConfiguration(this.GetType().ToString());
                panelButtons.Init();
                panelButtons.Size = new Size(85, 700);

                advancedDataGridViewSearchToolBar1.SetColumns(dgStrediska.Columns);

                //// načtení konfigurace vytvořených filtrů
                this.filtry = Extensions.ListExt.ReadFromXML<Fask.Interfaces.Filtry.StrediskaListFiltr>(this.GetType().ToString() + ".filtr");
                tscbFiltry.ComboBox.DataSource = this.filtry;
                tscbFiltry.SelectedItem = null;
                tscbFiltry.ComboBox.DropDownWitdhAutosize();

                // inicializace providera
                InitProvider();

                if (providerStrediska == null)
                    throw new Exception("Provider není inicializován");







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

        private void FormStrediskaList_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Shift && !e.Control && !e.Alt)
            {
                if (e.KeyCode == Keys.Escape)
                {
                    PerformCancel();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    //PerformOK();
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

        private void FormStrediskaList_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.dgStrediska.SaveConfiguration(this.GetType().ToString());
                panelButtons.SaveConfiguration(this.GetType().ToString());
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #endregion

        /// <summary>
        /// nacteni dat v jinem vlakne
        /// </summary>
        private void LoadDataAsync()
        {
            try
            {
                Fask.Interfaces.DataSets.Strediska ds = new Fask.Interfaces.DataSets.Strediska();

                //ds = ((Fask.Interfaces.Ciselniky.IStrediska)provider).GetStrediska();

                if ((providerStrediska != null) && (providerStrediska is Fask.Interfaces.Ciselniky.Strediska.IStrediska2_GetStrediska))
                    ds = ((Fask.Interfaces.Ciselniky.Strediska.IStrediska2_GetStrediska)providerStrediska).GetStrediska();
                else
                    throw new NotImplementedException("Provider neimplementuje IStrediska2_GetStrediska.");

                
                // navrat do hlavniho vlakna
                if (InvokeRequired)
                {
                    BeginInvoke(new Action(() =>
                    {
                        PopulateUI(ds);
                    }));
                }
            }
            catch (Exception ex)
            {
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
            //UpdateForm();
        }

        private void PopulateUI(Fask.Interfaces.DataSets.Strediska ds)
        {
            try
            {
                dsStrediska.Clear();
                //dsServis = providerServis.GetZdroje();
                bsStrediska.DataSource = ds;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ProgressIndicatorStop();
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
                    if (providerStrediska == null) //inicializace se provede pouze pokud nebyla provedena ... 
                    {
                        //Assembly providerAssemlby = Assembly.LoadFrom(Server.MapPath(providerAssemblyPath));
                        Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                        Type[] types = providerAssemlby.GetTypes();
                        foreach (Type t in types)
                        {
                            try
                            {
                                //t.IsAssignableFrom(typeof(Fask.Server.Interfaces.Vydej.IVydej);
                                if (typeof(Fask.Interfaces.Ciselniky.Strediska.IStrediska2).IsAssignableFrom(t))
                                {
                                    providerStrediska = (Fask.Interfaces.Ciselniky.Strediska.IStrediska2)providerAssemlby.CreateInstance(t.FullName);
                                    if (providerStrediska != null)
                                        break;
                                }
                            }
                            catch { }
                        }
                        //return config;
                    }

                    providerStrediska.InitProvider();

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

        /// <summary>
        /// Aktualizace dat po aktualizaci.
        /// </summary>
        private void PerformVyhledej(Fask.Interfaces.DataSets.Strediska.CZMST091Row row)
        {
            try
            {
                string id = row != null ? row.str_id : string.Empty;

                dsStrediska.Clear();
                //dsStrediska = ((Fask.Interfaces.Ciselniky.IStrediska)provider).GetStrediska();


                Fask.Interfaces.Filtry.StrediskaListFiltr filtr = new Fask.Interfaces.Filtry.StrediskaListFiltr();
                if (!CreateFilter(ref filtr))
                    return;


                if ((providerStrediska != null) && (providerStrediska is Fask.Interfaces.Ciselniky.Strediska.IStrediska2_GetFiltrovaneStrediska))
                    dsStrediska = ((Fask.Interfaces.Ciselniky.Strediska.IStrediska2_GetFiltrovaneStrediska)providerStrediska).GetFiltrovaneStrediska(filtr);
                else
                    throw new NotImplementedException("Provider neimplementuje IStrediska2_GetFiltrovaneStrediska.");


                bsStrediska.DataSource = dsStrediska;

                try
                {
                    if (!string.IsNullOrEmpty(id))
                    {
                        int index = bsStrediska.Find(dsStrediska.CZMST091.str_idColumn.ColumnName, id);
                        this.bsStrediska.Position = index;
                    }
                }
                catch
                {
                }
                dgStrediska.Focus();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show("Nepodařilo se obnovit záznamy\n" + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void PerformCreateRecord()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                using (FormStrediskaEdit frmcinnosti = new FormStrediskaEdit())
                {
                    //frmcinnosti.Text = "Nová činnost";
                    if (frmcinnosti.ShowDialog(this) != DialogResult.OK)
                        return;
                    PerformVyhledej(frmcinnosti.returnrow);
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PerformEditRecord()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (CZMST091_selectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro úpravu", this.Text, MessageBoxButtons.OK);
                    return;
                }

                using (FormStrediskaEdit frm = new FormStrediskaEdit())
                {
                    frm. Strediskorow = CZMST091_selectedRow;
                    //frm.Text = "Úprava činnosti";
                    if (frm.ShowDialog(this) != DialogResult.OK)
                        return;

                    PerformVyhledej(frm.returnrow);
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PerformDeleteRecord()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (CZMST091_selectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro odstranění", this.Text, MessageBoxButtons.OK);
                    return;
                }

                //if (MessageBox.Show("Opravdu chcete odstranit činnost '" + SelectedRow.Oznaceni.Trim() + "'?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                //    return;

                //bool odstranitNavaznosti = false;
                //DialogResult dr = MessageBox.Show("Chcete odstranit také návaznosti na vybranou činnost '" + SelectedRow.Oznaceni.Trim() + "'?", this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                //if (dr == System.Windows.Forms.DialogResult.Cancel)
                //    return;
                //else if (dr == System.Windows.Forms.DialogResult.Yes)
                //    odstranitNavaznosti = true;

                bool result;

                if ((providerStrediska != null) && (providerStrediska is Fask.Interfaces.Ciselniky.Strediska.IStrediska2_DeleteStredisko))
                    result = ((Fask.Interfaces.Ciselniky.Strediska.IStrediska2_DeleteStredisko)providerStrediska).DeleteStredisko(CZMST091_selectedRow.str_id);
                else
                    throw new NotImplementedException("Provider neimplementuje IStrediska2_GetStrediska.");



                //bool result = providerServis.DeleteZdroj(SelectedRow.ID);

                PerformVyhledej(CZMST091_selectedRow);
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

        private void obnovitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PerformVyhledej(CZMST091_selectedRow);
        }

        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            try
            {
                int pos = this.bsStrediska.Find(dsStrediska.CZMST091.str_idColumn.ColumnName, sortedID);
                this.bsStrediska.Position = pos;
            }
            catch { }
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1)
                {
                    if (CZMST091_selectedRow != null)
                        sortedID = CZMST091_selectedRow.str_id;
                }
            }
            catch { }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex != -1)
            //    PerformEditRecord();
        }

        private void ProgressIndicatorStart()
        {
            try
            {
                // prepocet stredu datagridu
                this.progressIndicator1.Location = new Point(this.dgStrediska.Location.X + (this.dgStrediska.Width / 2) - (progressIndicator1.Size.Width / 2), this.dgStrediska.Location.Y + (this.dgStrediska.Height / 2) - (progressIndicator1.Size.Height / 2));
            }
            catch { }
            progressIndicator1.Start();
            progressIndicator1.Visible = true;
        }

        private void ProgressIndicatorStop()
        {
            progressIndicator1.Stop();
            progressIndicator1.Visible = false;
        }

        private void bwImport_DoWork(object sender, DoWorkEventArgs e)
        {
            string status;

            try
            {

                if (bwImport.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }


                // nacteni dat v oddelenem vlakne
                if ((providerStrediska != null) && (providerStrediska is Fask.Interfaces.Ciselniky.Strediska.IStrediska2_ImportStrediska))
                    status = ((Fask.Interfaces.Ciselniky.Strediska.IStrediska2_ImportStrediska)providerStrediska).ImportStrediska();
                else
                    throw new NotImplementedException("Provider neimplementuje IStrediska2_ImportStrediska.");

                if (status != "OK")
                {
                    //error...
                    Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Error,status);
                }

                if (bwImport.CancellationPending)
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

        private void bwImport_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
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
                    PerformVyhledej(null);
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

        private void buttonVyhledat_Click(object sender, EventArgs e)
        {
            #region prace s datagridem - nastaveni numeric hodnot na pocet desetinnych mist
            try
            {
                // Procházení sloupců v DataGridView
                foreach (DataGridViewColumn column in dgStrediska.Columns)
                {
                    // Získání typu dat pro sloupec
                    Type dataType = null;

                    if (dgStrediska.DataSource is DataTable dataTable)
                    {
                        // Pokud je datový zdroj DataTable
                        if (dataTable.Columns.Contains(column.DataPropertyName))
                        {
                            dataType = dataTable.Columns[column.DataPropertyName].DataType;
                        }
                    }
                    else if (dgStrediska.DataSource is BindingSource bindingSource)
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

            PerformVyhledej(null);
        }

        #region FILTRE

        /// <summary>
        /// Vytvoreni filtru, ktery se pouzije.
        /// </summary>
        /// <param name="filtr"></param>
        /// <returns></returns>
        private bool CreateFilter(ref Fask.Interfaces.Filtry.StrediskaListFiltr filtr)
        {
            if (filtr == null)
                filtr = new Fask.Interfaces.Filtry.StrediskaListFiltr();

            filtr.str_id = tb_ID.Text.Trim();
            filtr.str_desc = tb_Popis.Text.Trim();
            filtr.str_typ = tb_Typ.Text.Trim();
            filtr.str_carcode = tb_CarKod.Text.Trim();


            return true;
        }


        private void tsbNastavit_Click(object sender, EventArgs e)
        {
            PerformNastavitFiltr(rowFiltr);
        }

        /// <summary>
        /// Nastavi zvoleny filtr do comboboxu, ...
        /// </summary>
        /// <param name="filtr"></param>
        private void PerformNastavitFiltr(Fask.Interfaces.Filtry.StrediskaListFiltr filtr)
        {
            try
            {
                if (filtr == null)
                    return;

                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                PerformVycistitFiltr();

                tb_ID.Text = filtr.str_id;
                tb_Popis.Text = filtr.str_desc;
                tb_Typ.Text = filtr.str_typ;
                tb_CarKod.Text = filtr.str_carcode;
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
                tb_ID.Text =
                tb_Typ.Text =
                tb_CarKod.Text =
                tb_Popis.Text = string.Empty;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                Fask.Interfaces.Filtry.StrediskaListFiltr filtr = rowFiltr;

                if (!CreateFilter(ref filtr))
                    return;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show("Nepodařilo se upravit filtr.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                Fask.Interfaces.Filtry.StrediskaListFiltr filtr = new Fask.Interfaces.Filtry.StrediskaListFiltr();
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

        #endregion

        #region Exporty



        private void exportDoCSVVseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                this.dgStrediska.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                this.dgStrediska.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
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

                this.dgStrediska.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                dgStrediska.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
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

                dgStrediska.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                dgStrediska.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #endregion



        private bool ImportDatZFileCSV(List<Tuple<string, string, bool>> tableInfo)
        {
            bool uspech = false;
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return uspech;

                //this.dg_Zbozi.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
                uspech = this.dgStrediska.ImportzCSV_strediska(tableInfo);

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return uspech;
            }

            return uspech;

        }


        private void tsmiImportovat_Click(object sender, EventArgs e)
        {
            bool uspech = false;

            try
            {
                if (!opravneniImport)
                {
                    MessageBox.Show("Nemáte dostatečné oprávnění pro tuto akci!", "Oprávnění", MessageBoxButtons.OK);
                    return;
                }


                using (FormImport form = new FormImport("Importovat", "Procedura", "Soubor CSV"))
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

                            //throw new NotImplementedException();


                            List<Tuple<string, string, bool>> tableInfo = new List<Tuple<string, string, bool>>();


                            // nacteni dat v oddelenem vlakne
                            if ((providerStrediska != null) && (providerStrediska is Fask.Interfaces.Ciselniky.Strediska.IStrediska2_ImportStrediska))
                                tableInfo = ((Fask.Interfaces.Ciselniky.Strediska.IStrediska2_ImportStrediska)providerStrediska).GetStrediskaTableInfo();
                            else
                                throw new NotImplementedException("Provider neimplementuje IZbozi2_ImportZbozi.");


                            //logika importu zbozi z CSV
                            string pathToFile = string.Empty;
                            uspech = ImportDatZFileCSV(tableInfo);


                            //PerformVyhledat();


                        }
                        else if (selectedOption == "SQL_Procedura")
                        {
                            DataTable dtchanged = this.dsStrediska.CZMST091.GetChanges();
                            if (dtchanged != null && dtchanged.Rows.Count > 0) //obsahuje zmeny ... 
                            {
                                DialogResult dr = MessageBox.Show("Obsahuje změny, pokračovat?", "data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                if (dr == System.Windows.Forms.DialogResult.No)
                                    return;
                            }

                            if (bwImport.IsBusy)
                            {
                                bwImport.CancelAsync();
                                while (bwImport.IsBusy)
                                {
                                    Application.DoEvents();
                                }
                            }

                            ProgressIndicatorStart();

                            int FirstDisplayedScrollingRowIndex = this.dgStrediska.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

                            bwImport.RunWorkerAsync();

                            if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dgStrediska.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dgStrediska.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index
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

            }
            catch (Exception ex)
            {

                ProgressIndicatorStop();
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            //---------------------------------------------------------------------


            #region old
            //if (!opravneniImport)
            //{
            //    MessageBox.Show("Nemáte dostatečné oprávnění pro tuto akci!", "Oprávnění", MessageBoxButtons.OK);
            //    return;
            //}

            //try
            //{
            //    DataTable dtchanged = this.dsStrediska.CZMST091.GetChanges();
            //    if (dtchanged != null && dtchanged.Rows.Count > 0) //obsahuje zmeny ... 
            //    {
            //        DialogResult dr = MessageBox.Show("Obsahuje změny, pokračovat?", "data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            //        if (dr == System.Windows.Forms.DialogResult.No)
            //            return;
            //    }

            //    if (bwImport.IsBusy)
            //    {
            //        bwImport.CancelAsync();
            //        while (bwImport.IsBusy)
            //        {
            //            Application.DoEvents();
            //        }
            //    }

            //    ProgressIndicatorStart();

            //    int FirstDisplayedScrollingRowIndex = this.dgStrediska.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

            //    bwImport.RunWorkerAsync();

            //    if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dgStrediska.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dgStrediska.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index
            //}
            //catch (Exception ex)
            //{
            //    ProgressIndicatorStop();
            //    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            //    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //} 
            #endregion
        }

        private void tsmiOdstranit_Click(object sender, EventArgs e)
        {
            if (!opravneniEditace)
            {
                MessageBox.Show("Nemáte dostatečné oprávnění pro tuto akci!", "Oprávnění", MessageBoxButtons.OK);
                return;
            }


            DialogResult dialog = MessageBox.Show("Přejete si odstranit položku/y?", this.Text, MessageBoxButtons.YesNo);
            if (dialog != DialogResult.Yes)
            {
                return;
            }

            PerformDeleteRecord();
        }

        private void tsmiUpravit_Click(object sender, EventArgs e)
        {
            if (!opravneniEditace)
            {
                MessageBox.Show("Nemáte dostatečné oprávnění pro tuto akci!", "Oprávnění", MessageBoxButtons.OK);
                return;
            }

            PerformEditRecord();
        }

        private void tsmiNovy_Click(object sender, EventArgs e)
        {
            if (!opravneniEditace)
            {
                MessageBox.Show("Nemáte dostatečné oprávnění pro tuto akci!", "Oprávnění", MessageBoxButtons.OK);
                return;
            }

            PerformCreateRecord();
        }

        private void advancedDataGridViewSearchToolBar1_Search(object sender, Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs e)
        {
            bool restartsearch = true;
            int startColumn = 0;
            int startRow = 0;
            if (!e.FromBegin)
            {
                bool endcol = dgStrediska.CurrentCell.ColumnIndex + 1 >= dgStrediska.ColumnCount;
                bool endrow = dgStrediska.CurrentCell.RowIndex + 1 >= dgStrediska.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dgStrediska.CurrentCell.ColumnIndex;
                    startRow = dgStrediska.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dgStrediska.CurrentCell.ColumnIndex + 1;
                    startRow = dgStrediska.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dgStrediska.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dgStrediska.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dgStrediska.CurrentCell = c;
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

                if (CZMST091_selectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if (CZMST091_selectedRows.Count > 1)
                {
                    //MessageBox.Show("Je vybráno více záznamů než jeden.", this.Text, MessageBoxButtons.OK);
                    //return;
                }

                Fask.Interfaces.DataSets.Strediska.CZMST091DataTable dt = new Fask.Interfaces.DataSets.Strediska.CZMST091DataTable();

                foreach (Fask.Interfaces.DataSets.Strediska.CZMST091Row row in CZMST091_selectedRows)
                {
                    try
                    {
                        Fask.Interfaces.DataSets.Strediska.CZMST091Row newRow = dt.NewCZMST091Row();
                        // Ošetřit delší vstupní pole než počet sloupců tabulky
                        int copyLength = Math.Min(row.ItemArray.Length, dt.Columns.Count);
                        newRow.ItemArray = row.ItemArray.Take(copyLength).ToArray();
                        dt.AddCZMST091Row(newRow);
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


        private void PrintReport(Fask.Interfaces.DataSets.Strediska.CZMST091DataTable dt)
        {
            try
            {


                if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                {
                    foreach (var item in dt)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start sklady,strediska TISK rdlc-------------------------------------");

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

                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END sklady,strediska TISK rdlc-------------------------------------");
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

                if (CZMST091_selectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                #region MaR 6.11.2024 vypis zpl do souboru selected rows
                int index_zaznamu = 0;

                foreach (var item in CZMST091_selectedRows)
                {

                    if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start sklady/ciselniky/strediska TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");

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

                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END sklady/ciselniky/strediska TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");


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



                    foreach (var item in CZMST091_selectedRows)
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
                    Tisk_selected_rows_Primy_Tisk(plr_Path, true, MN_ToTisk, CZMST091_selectedRows, NazevVychoziTiskarny);

                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Tisk_selected_rows_Primy_Tisk(string plr_Path, bool MnozstvuAutoJedna, int MN_ToTisk, List<Fask.Interfaces.DataSets.Strediska.CZMST091Row> FASK_ZASOBY_selected_Rows, string nazevVychTiskarny)
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

                if (CZMST091_selectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                //if (CZMST091_selectedRows.Count > 1)
                //{
                //    //MessageBox.Show("Je vybráno více záznamů než jeden.", this.Text, MessageBoxButtons.OK);
                //    //return;
                //}

                Fask.Interfaces.DataSets.Strediska.CZMST091DataTable dt = new Fask.Interfaces.DataSets.Strediska.CZMST091DataTable();

                foreach (Fask.Interfaces.DataSets.Strediska.CZMST091Row row in CZMST091_selectedRows)
                {
                    try
                    {
                        Fask.Interfaces.DataSets.Strediska.CZMST091Row newRow = dt.NewCZMST091Row();
                        // Ošetřit delší vstupní pole než počet sloupců tabulky
                        int copyLength = Math.Min(row.ItemArray.Length, dt.Columns.Count);
                        newRow.ItemArray = row.ItemArray.Take(copyLength).ToArray();
                        dt.AddCZMST091Row(newRow);
                    }
                    catch (Exception ex)
                    {
                        // Ošetření vyjimky
                        // Můžete zde provést logování chyby nebo jiné požadované akce
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                    }
                }

                dt.Columns.Add("str_carcode_IMG", typeof(string));
                foreach (Fask.Interfaces.DataSets.Strediska.CZMST091Row row in dt)
                {


                    try
                    {
                        if (!row.Isstr_carcodeNull() && !string.IsNullOrEmpty(row.str_carcode))
                        {
                            ZXing.BarcodeWriter zw = new ZXing.BarcodeWriter();
                            zw.Format = ZXing.BarcodeFormat.CODE_128;
                            zw.Options.Height = 50; //50
                            zw.Options.PureBarcode = true;
                            System.Drawing.Bitmap image1 = zw.Write(row.str_carcode);

                            System.IO.MemoryStream ms = new System.IO.MemoryStream();
                            image1.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                            byte[] imgReportBarcode = ms.ToArray();
                            ms.Close();

                            string Base64Imahe = Convert.ToBase64String(imgReportBarcode);

                            row["str_carcode_IMG"] = Base64Imahe;
                        }


                    }
                    catch (Exception ex)
                    {

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

        private void PrintReport_RDLC(Fask.Interfaces.DataSets.Strediska.CZMST091DataTable dt, bool primyTisk, string path_sablona, string nazevVychTiskarny, int pocetVytisku)
        {
            try
            {

                #region logovani tisku
                if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                {
                    foreach (var item in dt)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start sklady/ciselniky/strediska TISK rdlc-------------------------------------");

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

                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END sklady/ciselniky/strediska TISK rdlc-------------------------------------");
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

                if (CZMST091_selectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                #region MaR 6.11.2024 vypis zpl do souboru selected rows
                int index_zaznamu = 0;

                foreach (var item in CZMST091_selectedRows)
                {

                    if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start sklady/ciselniky/strediska TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");

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

                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END sklady/ciselniky/strediska TISK zpl zaznam: " + index_zaznamu.ToString() + "-------------------------------------");


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



                    foreach (var item in CZMST091_selectedRows)
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
                    Tisk_selected_rows(plr_Path, true, MN_ToTisk, CZMST091_selectedRows);

                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Tisk_selected_rows(string plr_Path, bool MnozstvuAutoJedna, int MN_ToTisk, List<Fask.Interfaces.DataSets.Strediska.CZMST091Row> FASK_ZASOBY_selected_Rows)
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
            Fask.Interfaces.DataSets.Strediska.CZMST091Row row,
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

                Fask.Interfaces.DataSets.Strediska.CZMST091DataTable dt = new Fask.Interfaces.DataSets.Strediska.CZMST091DataTable();

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

        private Dictionary<string, string> PrepareDataToTisk(Fask.Interfaces.DataSets.Strediska.CZMST091Row productionRow_data)
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

                        Fask.Interfaces.DataSets.Strediska.CZMST091DataTable dt = new Fask.Interfaces.DataSets.Strediska.CZMST091DataTable();


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
