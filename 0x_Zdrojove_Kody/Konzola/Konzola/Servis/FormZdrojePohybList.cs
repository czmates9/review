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

namespace Konzola.Servis
{
    public partial class FormZdrojePohybList : Form
    {
        private Fask.Interfaces.IMES providerServis = null;
        private Fask.Interfaces.IMES providerOdberatele = null;
        //private Fask.Interfaces.IVyrobaKonzola providerUzivatele = null;

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
        /// Zvolene ID zdroje v ComboBoxu
        /// </summary>
        private Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojRow rowZdrojID
        {
            get
            {
                try
                {
                    return comboBoxZdrojID.SelectedItem as Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojRow;
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Zvolene ID stavu v ComboBoxu
        /// </summary>
        private Fask.Interfaces.DataSets.Servis.CZMST_Servis_StavRow rowStavID
        {
            get
            {
                try
                {
                    return comboBoxStavID.SelectedItem as Fask.Interfaces.DataSets.Servis.CZMST_Servis_StavRow;
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Zvolene ID cinnosti v ComboBoxu
        /// </summary>
        private Fask.Interfaces.DataSets.Servis.CZMST_Servis_CinnostRow rowCinnostID
        {
            get
            {
                try
                {
                    return comboBoxCinnostID.SelectedItem as Fask.Interfaces.DataSets.Servis.CZMST_Servis_CinnostRow;
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Zvolene ID odberatel v ComboBoxu
        /// </summary>
        private Fask.Interfaces.DataSets.Odberatele.CZMST090Row rowOdberatelID
        {
            get
            {
                try
                {
                    return comboBoxODB_ID.SelectedItem as Fask.Interfaces.DataSets.Odberatele.CZMST090Row;
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Zvolený Production záznam.
        /// </summary>
        private Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojPohybRow rowZdrojPohyb
        {
            get
            {
                try
                {
                    return ((DataRowView)(dgServis.BindingContext[this.bsServis].Current)).Row as Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojPohybRow;
                }
                catch
                {
                    return null;
                }
            }
        }


        #region Eventy formu

        public FormZdrojePohybList()
        {
            InitializeComponent();

            this.dgServis.UpdateColumnHeaderCellsByDatasource();
            panelButtons.Menu = menuStrip1;

            dgServis.CellPainting += new DataGridViewCellPaintingEventHandler(dataGridView1_CellPainting);
            dgServis.CellMouseClick += new DataGridViewCellMouseEventHandler(dataGridView1_CellMouseClick);
        }

        private void FormZdrojePohybList_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                this.WindowState = FormWindowState.Maximized;

                // načtení konfigurace datagridu z nastavení aplikace
                this.dgServis.LoadConfiguration(this.GetType().ToString());
                
                panelButtons.LoadConfiguration(this.GetType().ToString());
                panelButtons.Init();

                advancedDataGridViewSearchToolBar1.SetColumns(dgServis.Columns);

                // inicializace providera
                InitProvider();

                if (providerServis == null)
                    throw new Exception("Provider Servis není inicializován");

                if (providerOdberatele == null)
                    throw new Exception("Provider Odběratelé není inicializován");

                //if (providerUzivatele == null)
                //    throw new Exception("Provider Uživatelé není inicializován");

                System.Threading.Thread loadThread = new System.Threading.Thread(LoadDataAsync);
                loadThread.IsBackground = true;
                loadThread.Start();

                // nastavení času (zacatek a konec dne)
                dateTimePickerDatumOd.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                dateTimePickerDatumDo.Value = DateTime.Now.AddDays(1).Date.AddSeconds(-1);
                dateTimePickerDatumDo.Checked = false;
                dateTimePickerDatumOd.Checked = false;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormZdrojePohybList_KeyDown(object sender, KeyEventArgs e)
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

        private void FormZdrojePohybList_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.dgServis.SaveConfiguration(this.GetType().ToString());
                panelButtons.SaveConfiguration(this.GetType().ToString());


                // cekani na dobehnuti vlakna
                try
                {
                    if (bwLoadData.IsBusy)
                    {
                        bwLoadData.CancelAsync();
                        while (bwLoadData.IsBusy)
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
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        #endregion


        void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0
                    && e.ColumnIndex >= 0
                    )
                {
                    if ((ModifierKeys & Keys.Control) == Keys.Control)
                    { // stisknuty pouze Control ... 
                        string sort = ((DataView)((BindingSource)dgServis.DataSource).List).Sort;
                        var sortcolumns = sort.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                        var column = sortcolumns.Where(x => x.Contains(this.dgServis.Columns[e.ColumnIndex].DataPropertyName));
                        string columnnew = null;

                        if (column.Count() > 0)
                        { // uz je pridelen v sortu
                            var firstcolumn = column.First();
                            // => otocit razeni ...
                            if (firstcolumn.Trim().EndsWith("desc", StringComparison.CurrentCultureIgnoreCase))
                                //this.dataGridView1.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                                columnnew = String.Format("{0} {1}", this.dgServis.Columns[e.ColumnIndex].DataPropertyName, "asc");
                            else
                                //this.dataGridView1.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                                columnnew = String.Format("{0} {1}", this.dgServis.Columns[e.ColumnIndex].DataPropertyName, "desc");

                            int indexofcolumn = sortcolumns.IndexOf(firstcolumn);
                            sortcolumns.RemoveAt(indexofcolumn);
                            sortcolumns.Insert(indexofcolumn, columnnew);
                        }
                        else
                        { // neni, tak ho pridam ... 
                            //this.dataGridView1.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                            //this.dataGridView1.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.Programmatic;
                            columnnew = String.Format("{0} {1}", this.dgServis.Columns[e.ColumnIndex].DataPropertyName, "asc");
                            sortcolumns.Add(columnnew);
                        }

                        //((DataView)((BindingSource)dataGridView1.DataSource).List).Sort = String.Join(", ", sortcolumns);
                        this.BeginInvoke((MethodInvoker)delegate
                        {
                            this.bsServis.Sort = String.Join(",", sortcolumns);
                        }
                        );
                    }
                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0
                    && e.ColumnIndex >= 0
                    && !String.IsNullOrEmpty(((DataView)((BindingSource)dgServis.DataSource).List).Sort)
                    )
                {
                    //.Contains(this.dataGridView1.Columns[e.ColumnIndex].DataPropertyName))
                    //e.Paint(e.ClipBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentBackground);
                    //e.Graphics.DrawEllipse(Pens.Red, e.CellBounds);
                    //e.Handled = true;

                    string sort = ((DataView)((BindingSource)dgServis.DataSource).List).Sort;
                    var sortcolumns = sort.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    var column = sortcolumns.Where(x => x.Contains(this.dgServis.Columns[e.ColumnIndex].DataPropertyName));
                    if (column.Count() > 0)
                    {
                        var firstcolumn = column.First();
                        if (firstcolumn.Trim().EndsWith("desc", StringComparison.CurrentCultureIgnoreCase))
                            this.dgServis.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                        else
                            this.dgServis.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                    }
                    else
                    {
                        this.dgServis.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.None;
                    }
                    //this.dataGridView1.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                }

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
                Fask.Interfaces.DataSets.Servis dsServisData = new Fask.Interfaces.DataSets.Servis();
                Fask.Interfaces.DataSets.Odberatele dsOdberateleData = new Fask.Interfaces.DataSets.Odberatele();
                FASK.Logins.DataSets.Pristupy.FASK_LoginsDataTable dtUzivateleData = new FASK.Logins.DataSets.Pristupy.FASK_LoginsDataTable();

                // naplneni ciselniku odberatelu
                //dsOdberateleData = ((Fask.Interfaces.Ciselniky.IOdberatele)providerOdberatele).GetOdberatele();

                if ((providerOdberatele != null) && (providerOdberatele is Fask.Interfaces.Ciselniky.Odberatele.IOdberatele2_GetOdberatele))
                    dsOdberateleData = ((Fask.Interfaces.Ciselniky.Odberatele.IOdberatele2_GetOdberatele)providerOdberatele).GetOdberatele();
                else
                    throw new NotImplementedException("Provider neimplementuje IOdberatele2_GetOdberatele.");


                // naplneni zdroju
                foreach (var item in ((Fask.Interfaces.Servis.IServis)providerServis).GetZdroje().CZMST_Servis_Zdroj)
                {
                    dsServisData.CZMST_Servis_Zdroj.ImportRow(item);
                }
                
                // naplneni stavu
                foreach (var item in ((Fask.Interfaces.Servis.IServis)providerServis).GetStavy().CZMST_Servis_Stav)
                {
                    dsServisData.CZMST_Servis_Stav.ImportRow(item);
                }

                // naplneni cinnosti
                foreach (var item in ((Fask.Interfaces.Servis.IServis)providerServis).GetCinnosti().CZMST_Servis_Cinnost)
                {
                    dsServisData.CZMST_Servis_Cinnost.ImportRow(item);
                }

                // naplneni comboboxu uzivatelu
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
                        PopulateUI(dsServisData, dsOdberateleData, dtUzivateleData);
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

        private void PopulateUI(Fask.Interfaces.DataSets.Servis dsServisData, Fask.Interfaces.DataSets.Odberatele dsOdberateleData, FASK.Logins.DataSets.Pristupy.FASK_LoginsDataTable dtUzivateleData)
        {
            try
            {
                // naplneni comboboxu uzivatelu
                comboBoxUzivatel.Items.AddRange(dtUzivateleData.Select(null, "SECONDNAME asc"));
                comboBoxUzivatel.SelectedItem = null;

                // naplneni comboboxu zdroju
                comboBoxZdrojID.Items.AddRange(dsServisData.CZMST_Servis_Zdroj.Select(null, "Oznaceni asc"));
                comboBoxZdrojID.SelectedItem = null;

                // naplneni comboboxu stavu
                comboBoxStavID.Items.AddRange(dsServisData.CZMST_Servis_Stav.Select(null, "Oznaceni asc"));
                comboBoxStavID.SelectedItem = null;

                // naplneni comboboxu cinnosti
                comboBoxCinnostID.Items.AddRange(dsServisData.CZMST_Servis_Cinnost.Select(null, "Oznaceni asc"));
                comboBoxCinnostID.SelectedItem = null;

                // naplneni comboboxu odberatelu
                comboBoxODB_ID.Items.AddRange(dsOdberateleData.CZMST090.Select(null, "odb_desc asc"));
                comboBoxODB_ID.SelectedItem = null;

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
                    if (providerServis == null) //inicializace se provede pouze pokud nebyla provedena ... 
                    {
                        //Assembly providerAssemlby = Assembly.LoadFrom(Server.MapPath(providerAssemblyPath));
                        Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                        Type[] types = providerAssemlby.GetTypes();
                        foreach (Type t in types)
                        {
                            try
                            {
                                //t.IsAssignableFrom(typeof(Fask.Server.Interfaces.Vydej.IVydej);
                                if (typeof(Fask.Interfaces.Servis.IServis).IsAssignableFrom(t))
                                {
                                    providerServis = (Fask.Interfaces.Servis.IServis)providerAssemlby.CreateInstance(t.FullName);
                                    if (providerServis != null)
                                        break;
                                }
                            }
                            catch { }
                        }
                        //return config;
                    }

                    // nastaveni connection stringu
                    if (providerServis != null)
                        ((Fask.Interfaces.Servis.IServis)providerServis).ConnectionString = Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].FASKDB_ConnesctionString;
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
                    if (providerOdberatele == null) //inicializace se provede pouze pokud nebyla provedena ... 
                    {
                        //Assembly providerAssemlby = Assembly.LoadFrom(Server.MapPath(providerAssemblyPath));
                        Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                        Type[] types = providerAssemlby.GetTypes();
                        foreach (Type t in types)
                        {
                            try
                            {
                                //t.IsAssignableFrom(typeof(Fask.Server.Interfaces.Vydej.IVydej);
                                if (typeof(Fask.Interfaces.Ciselniky.Odberatele.IOdberatele2).IsAssignableFrom(t))
                                {
                                    providerOdberatele = (Fask.Interfaces.Ciselniky.Odberatele.IOdberatele2)providerAssemlby.CreateInstance(t.FullName);
                                    if (providerOdberatele != null)
                                        break;
                                }
                            }
                            catch { }
                        }
                        //return config;
                    }

                    providerOdberatele.InitProvider();
                    

                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //try
            //{
            //    if (!String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
            //    {
            //        if (providerUzivatele == null) //inicializace se provede pouze pokud nebyla provedena ... 
            //        {
            //            //Assembly providerAssemlby = Assembly.LoadFrom(Server.MapPath(providerAssemblyPath));
            //            Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
            //            Type[] types = providerAssemlby.GetTypes();
            //            foreach (Type t in types)
            //            {
            //                try
            //                {
            //                    //t.IsAssignableFrom(typeof(Fask.Server.Interfaces.Vydej.IVydej);
            //                    if (typeof(Fask.Interfaces.Ciselniky.IUzivatele2).IsAssignableFrom(t))
            //                    {
            //                        providerUzivatele = (Fask.Interfaces.Ciselniky.IUzivatele2)providerAssemlby.CreateInstance(t.FullName);
            //                        if (providerUzivatele != null)
            //                            break;
            //                    }
            //                }
            //                catch { }
            //            }
            //            //return config;
            //        }

            //        // nastaveni connection stringu
            //       // if (providerUzivatele != null)
            //      //      ((Fask.Interfaces.Ciselniky.IUzivatele)providerUzivatele).ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
            //        if ((providerUzivatele != null) && (providerUzivatele is Fask.Interfaces.Parametry.IParametry2_ConnectionString))
            //            ((Fask.Interfaces.Parametry.IParametry2_ConnectionString)providerUzivatele).ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
              

            //    }
            //}
            //catch (Exception ex)
            //{
            //    Fask.Logging.ExceptionHandler2.Handle(ex, "Load Provider.StavSkladu");
            //    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void buttonVyhledat_Click(object sender, EventArgs e)
        {
            PerformVyhledat();
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
                this.dgServis.SelectAll();
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
                this.dgServis.ClearSelection();
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

        // zobrazeni podrovnejsich informaci, pokud to je mozne ...
        private void PerformZobrazitInformace()
        {
            try
            {
                if (rowZdrojPohyb == null || rowZdrojPohyb.IsCinnostTypeNull() || rowZdrojPohyb.IsCinnostValueNull())
                    return;

                if (!rowZdrojPohyb.IsIDCinnostNull() && rowZdrojPohyb.CinnostType == "D")
                {
                    // dynamicka tabulka
                    // 1) nacteni informaci o cinnosti
                    // 2) zjisteni nazvu tabulky
                    // 3) nacteni informaci o vybranem radku
                    Fask.Interfaces.DataSets.Servis.CZMST_Servis_CinnostRow cinnost = ((Fask.Interfaces.Servis.IServis)providerServis).GetCinnostByID(rowZdrojPohyb.IDCinnost);
                    
                    // test existence cinnosti
                    if (cinnost == null)
                        throw new Exception("Činnost s id '" + rowZdrojPohyb.IDCinnost + "' neexistuje");

                    Fask.Interfaces.DataSets.Servis.CZMST_Servis_Dynamic_Table_DefinitionRow dyntabdef = ((Fask.Interfaces.Servis.IServis)providerServis).GetDynamicTableDefinitionByTypeName(cinnost.TYPEVALUE);

                    // test existence definice dynamicke tabulky
                    if(dyntabdef == null)
                        throw new Exception("Činnost s id '" + rowZdrojPohyb.IDCinnost + "' neexistuje");

                    Fask.Interfaces.DataSets.Servis.CZMST_Servis_Dynamic_TableRow dyntabrow = ((Fask.Interfaces.Servis.IServis)providerServis).GetDynamicTableByID(dyntabdef.TypeName, rowZdrojPohyb.CinnostValue);

                    // test existence zaznamu v dynamicke tabulce
                    if (dyntabrow == null)
                        throw new Exception("Záznam s id '" + rowZdrojPohyb.CinnostValue + "' neexistuje v dynamické tabulce '" + dyntabdef.TypeName + "'");

                    MessageBox.Show(dyntabrow.Oznaceni.Trim());
                }
                else if (rowZdrojPohyb.CinnostType == "P" && !rowZdrojPohyb.IsCinnostValueNull())
                {
                    // fotka
                    // zobrazeni fotky v formulari
                    string path = System.IO.Path.Combine(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Servis[0].ImagesDataFileDirectory, rowZdrojPohyb.CinnostValue);
                    // test existence
                    if (!System.IO.File.Exists(path))
                        throw new Exception("Soubor '" + rowZdrojPohyb.CinnostValue + "' neexistuje");

                    Forms.FormImage.Show(path);


                }
                //providerServis.GetCinnostByID(rowZdrojPohyb.ci

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (rowZdrojPohyb == null || rowZdrojPohyb.IsCinnostTypeNull())
                {
                    this.tsmiZobrazitInformace.Enabled = false;
                    return;
                }

                if ((rowZdrojPohyb.CinnostType == "D") || (rowZdrojPohyb.CinnostType == "P"))
                {
                    this.tsmiZobrazitInformace.Enabled = true;
                }
                else
                    this.tsmiZobrazitInformace.Enabled = false;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PerformVyhledat()
        {
            try
            {
                DataTable dtchanged = this.dsServis.CZMST_Servis_ZdrojPohyb.GetChanges();
                if (dtchanged != null && dtchanged.Rows.Count > 0) //obsahuje zmeny ... 
                {
                    DialogResult dr = MessageBox.Show("Obsahuje změny, pokračovat?", "data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == System.Windows.Forms.DialogResult.No)
                        return;

                }

                if (bwLoadData.IsBusy)
                {
                    bwLoadData.CancelAsync();
                    while (bwLoadData.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }

                ProgressIndicatorStart();

                Fask.Interfaces.Filtry.ZdrojePohybListFiltr filtr = new Fask.Interfaces.Filtry.ZdrojePohybListFiltr();
                if (!CreateFilter(ref filtr))
                    return;

                //Save Current Scroll Index

                Point pScrollingIndexTmp = new Point(
                    this.dgServis.FirstDisplayedScrollingRowIndex,
                    this.dgServis.FirstDisplayedScrollingColumnIndex
                );

                bwLoadData.RunWorkerAsync(filtr);

                //if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dataGridView1.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) 
                //    this.dataGridView1.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index
                try
                {
                    this.dgServis.FirstDisplayedScrollingRowIndex = pScrollingIndexTmp.X;
                    this.dgServis.FirstDisplayedScrollingColumnIndex = pScrollingIndexTmp.Y;
                }
                catch { }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.dgServis.Focus();
            }
        }

        /// <summary>
        /// Vytvoreni filtru, ktery se pouzije.
        /// </summary>
        /// <param name="filtr"></param>
        /// <returns></returns>
        private bool CreateFilter(ref Fask.Interfaces.Filtry.ZdrojePohybListFiltr filtr)
        {
            if (filtr == null)
                filtr = new Fask.Interfaces.Filtry.ZdrojePohybListFiltr();

            filtr.rowUzivatel = rowUzivatel;
            filtr.UzivatelID = comboBoxUzivatel.Text;
            filtr.rowZdrojID = rowZdrojID;
            filtr.ZdrojID = comboBoxZdrojID.Text;
            filtr.rowStavID = rowStavID;
            filtr.StavID = comboBoxStavID.Text;
            filtr.rowCinnostID = rowCinnostID;
            filtr.CinnostID = comboBoxCinnostID.Text;
            filtr.rowOdberatelID = rowOdberatelID;
            filtr.OdberatelID = comboBoxODB_ID.Text;
            filtr.DatumOd = dateTimePickerDatumOd.Checked ? dateTimePickerDatumOd.Value : (DateTime?)null;
            filtr.DatumDo = dateTimePickerDatumDo.Checked ? dateTimePickerDatumDo.Value : (DateTime?)null;
            filtr.VyloucitNedefinovaneHodnoty = cbVyloucitNedefinovaneHodnoty.Checked;
            filtr.VsechnyZdrojeOkruhu = cbVsechnyZdrojeOkruhu.Checked;
            filtr.OkruhID = cbOkruh.Text;
            filtr.Misto = cbMisto.Text;
            filtr.Type = cbTyp.Text;
            try
            {
                int tmpDavka;
                if (int.TryParse(cbCountEntries.Text, out tmpDavka))
                    filtr.Davka = tmpDavka;
                else
                    filtr.Davka = null;

            }
            catch { }

            return true;
        }
        
        private void bwLoadData_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Fask.Interfaces.Filtry.ZdrojePohybListFiltr filtr = (Fask.Interfaces.Filtry.ZdrojePohybListFiltr)e.Argument;
                Fask.Interfaces.DataSets.Servis ds = new Fask.Interfaces.DataSets.Servis();

                if (bwLoadData.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                // nacteni dat v oddelenem vlakne
                ds = ((Fask.Interfaces.Servis.IServis)providerServis).GetFiltrovanyZdrojPohyb(filtr);
                if (bwLoadData.CancellationPending)
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

        private void bwLoadData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                // check error, check cancel, then use result
                if (e.Error != null)
                {
                    // handle the error
                    dsServis = new Fask.Interfaces.DataSets.Servis();
                    bsServis.DataSource = dsServis;
                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, e.Error);
                    MessageBox.Show(e.Error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (e.Cancelled)
                {
                    // handle cancellation
                    // TODO: maji se vymazat data z datasetu?? asi ano ...
                    //if (dsSkladPohyb == null)
                    dsServis= new Fask.Interfaces.DataSets.Servis();
                    bsServis.DataSource = dsServis;
                }
                else
                {
                    // uspesne dokonceno ...
                    // use it on the UI thread
                    dsServis= (Fask.Interfaces.DataSets.Servis)e.Result;
                    if (dsServis == null)
                        dsServis = new Fask.Interfaces.DataSets.Servis();

                    bsServis.DataSource = dsServis;
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
                this.progressIndicator1.Location = new Point(this.dgServis.Location.X + (this.dgServis.Width / 2) - (progressIndicator1.Size.Width / 2), this.dgServis.Location.Y + (this.dgServis.Height / 2) - (progressIndicator1.Size.Height / 2));
            }
            catch { }
            progressIndicator1.Start();
            progressIndicator1.Visible = true;
        }

        private void tsbNastavit_Click(object sender, EventArgs e)
        {
        }

        private void PerformVycistitFiltr()
        {
            try
            {
                comboBoxUzivatel.SelectedItem =
                comboBoxZdrojID.SelectedItem =
                comboBoxStavID.SelectedItem =
                comboBoxCinnostID.SelectedItem =
                comboBoxODB_ID.SelectedItem = 
                cbOkruh.SelectedItem = 
                cbCountEntries.SelectedItem = null;

                comboBoxUzivatel.Text =
                comboBoxZdrojID.Text =
                comboBoxStavID.Text =
                comboBoxCinnostID.Text =
                comboBoxODB_ID.Text =
                cbOkruh.Text = 
                cbCountEntries.Text = null;

                dateTimePickerDatumOd.Checked =
                dateTimePickerDatumDo.Checked = false;

                //cbVyloucitNedefinovaneHodnoty.Checked = false;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintReport( Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojPohybDataTable dt, string status)
        {
            using (PrintReportLibrary.CoolPrintPreviewDialog plr = new PrintReportLibrary.CoolPrintPreviewDialog())
            {
                OrderedEnumerableRowCollection<Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojPohybRow> date = dsServis.CZMST_Servis_ZdrojPohyb.OrderBy(x => x.Modified);

                var odberatele = dsServis.CZMST_Servis_ZdrojPohyb
                    .Where(x => !x.IsOdberatelOznaceniNull() && !x.IsODB_IDNull())
                    .GroupBy(x => new
                    {
                        x.OdberatelOznaceni //(string)(x.IsOdberatelOznaceniNull() ? string.Empty : x.OdberatelOznaceni)
                        ,
                        x.ODB_ID //, ( x.IsODB_IDNull() ? string.Empty : x.ODB_ID )
                    });
                string odber = "-";
                if (odberatele.Count() == 1)
                {
                    var odberatel = odberatele.First();
                    odber = String.Format("{0} ({1})", odberatel.Key.OdberatelOznaceni.Trim(), odberatel.Key.ODB_ID.Trim());
                }

                plr.Params = new Microsoft.Reporting.WinForms.ReportParameter[]
                {
                    new Microsoft.Reporting.WinForms.ReportParameter("Odberatel",odber),
                    new Microsoft.Reporting.WinForms.ReportParameter("DatumOd",date.First().Modified == null ? "-" : date.First().Modified.ToShortDateString() ),
                    new Microsoft.Reporting.WinForms.ReportParameter("DatumDo",date.Last().Modified == null ? "-" : date.Last().Modified.ToShortDateString()),
                    new Microsoft.Reporting.WinForms.ReportParameter("Status" ,status),


                    new Microsoft.Reporting.WinForms.ReportParameter("ImageURL", new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Servis", "Print", "Templates", "LOGO.jpg")).AbsoluteUri)

                };

                plr.NazevDataTable = "DataSet";
                plr.DataTable = dt;

                List<PrintReportLibrary.TypeData> tmplist = new List<PrintReportLibrary.TypeData>();
                tmplist.Add(PrintReportLibrary.TypeData.DataTable);
                plr.typedata = new List<PrintReportLibrary.TypeData>(tmplist);
                plr.Projekt = PrintReportLibrary.Projekt.DDD;
                plr.ShowPreview = true;
                plr.Path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Servis", "Print", "Templates", "Report_TEST.rdlc");
                plr.Print(this);

            }
        }


        private void tsmiZobrazitInformace_Click(object sender, EventArgs e)
        {
            try
            {
                PerformZobrazitInformace();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.dgServis.Focus();
            }
        }

        private void advancedDataGridViewSearchToolBar1_Search(object sender, Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs e)
        {
            bool restartsearch = true;
            int startColumn = 0;
            int startRow = 0;
            if (!e.FromBegin)
            {
                bool endcol = dgServis.CurrentCell.ColumnIndex + 1 >= dgServis.ColumnCount;
                bool endrow = dgServis.CurrentCell.RowIndex + 1 >= dgServis.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dgServis.CurrentCell.ColumnIndex;
                    startRow = dgServis.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dgServis.CurrentCell.ColumnIndex + 1;
                    startRow = dgServis.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dgServis.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dgServis.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dgServis.CurrentCell = c;
        }

        #region Exporty

        private void exportDoCSVVseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                this.dgServis.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                this.dgServis.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
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

                this.dgServis.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                dgServis.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
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

                dgServis.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                dgServis.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        private void tsmiReportTiskVse_Click(object sender, EventArgs e)
        {
            try
            {

                if (dsServis.CZMST_Servis_ZdrojPohyb == null || dsServis.CZMST_Servis_ZdrojPohyb.Count <= 0)
                {
                    MessageBox.Show("Neni zobrazen žádný záznam.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                #region Puvodny tisk
                //using (FormZdrojePohybReportViewer rep = new FormZdrojePohybReportViewer())
                //{

                //    OrderedEnumerableRowCollection<Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojPohybRow> date = dsServis.CZMST_Servis_ZdrojPohyb.OrderBy(x => x.Modified);

                //    var odberatele = dsServis.CZMST_Servis_ZdrojPohyb
                //        .Where(x => !x.IsOdberatelOznaceniNull() && !x.IsODB_IDNull())
                //        .GroupBy(x => new
                //        {
                //            x.OdberatelOznaceni //(string)(x.IsOdberatelOznaceniNull() ? string.Empty : x.OdberatelOznaceni)
                //            ,
                //            x.ODB_ID //, ( x.IsODB_IDNull() ? string.Empty : x.ODB_ID )
                //        });
                //    string odber = "-";
                //    if (odberatele.Count() == 1)
                //    {
                //        var odberatel = odberatele.First();
                //        odber = String.Format("{0} ({1})", odberatel.Key.OdberatelOznaceni.Trim(), odberatel.Key.ODB_ID.Trim());
                //    }

                //    rep.Params = new Microsoft.Reporting.WinForms.ReportParameter[]
                //{
                //    new Microsoft.Reporting.WinForms.ReportParameter("Odberatel",odber),
                //    new Microsoft.Reporting.WinForms.ReportParameter("DatumOd",date.First().Modified == null ? "-" : date.First().Modified.ToShortDateString() ),
                //    new Microsoft.Reporting.WinForms.ReportParameter("DatumDo",date.Last().Modified == null ? "-" : date.Last().Modified.ToShortDateString()),
                //    new Microsoft.Reporting.WinForms.ReportParameter("Status" ,"Vše"),


                //    new Microsoft.Reporting.WinForms.ReportParameter("ImageURL", new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Servis", "Print", "Templates", "LOGO.jpg")).AbsoluteUri)

                //};

                //    //Binding Source predavany
                //    //rep.bindingsource = this.bsServis;
                //    //rep.typedata = TypeData.BindingSource;

                //    rep.typedata = TypeData.DataTable;
                //    rep.DataTable = dsServis.CZMST_Servis_ZdrojPohyb;


                //    rep.NazevDataTable = "DataSet";
                //    rep.Path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Servis", "Print", "Templates", "Report_TEST.rdlc");
                //    // = @"D:\xxx\xxx_2017\xxx_2017_52_Reporty\template\Report_Params.rdlc";

                //    rep.ShowDialog();
                //} 
                #endregion

                #region 14.5.2018 TaD Upravena verze tisku, co nejvic veci ve vlastni rezii


                PrintReport(dsServis.CZMST_Servis_ZdrojPohyb, "Vše");

                //PrintReportLibrary.PrintReport plr = new PrintReportLibrary.PrintReport();
                //plr.CountEntries = "123456";
                //plr.DS_Soupis = ds;




                //plr.PrinterName = "HP LaserJet 2430 PCL6 Class Driver";
                //plr.Typereport = PrintReportLibrary.TypeReport.DoPradelny_OK;


                #endregion

                var l = this.bsServis.List;
                var t = (Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojPohybDataTable)((DataView)l).Table;
                DateTime dt = DateTime.Now;
                t.ToList().ForEach(x =>
                {
                    if (x.GUID != Guid.Empty)
                        x.dateExported = dt;
                });
                //providerServis.UpdateZdrojPohybExported(guidsExported, DateTime.Now);
                ((Fask.Interfaces.Servis.IServis)providerServis).UpdateZdrojPohyb((Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojPohybDataTable)((DataView)l).Table);

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                // throw;
            }
        }

        private void tsmiReportTiskOznacene_Click(object sender, EventArgs e)
        {
            try
            {

                if (dgServis.SelectedRows == null || dgServis.SelectedRows.Count <= 0)
                {
                    MessageBox.Show("Neni vybrán žádný záznam.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                //using (FormZdrojePohybReportViewer rep = new FormZdrojePohybReportViewer())
                //{

                //bsServis.ord
                //rep.bindingsource = this._bindingsource;
                //rep.typedata = TypeData.BindingSource;


                //rep.DataTable = (Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojPohybDataTable)dsServis.CZMST_Servis_ZdrojPohyb.Select().OrderBy(x => x["Modified"]);
                //rep.DataTable = dsServis.CZMST_Servis_ZdrojPohyb.OrderBy(x => x.Modified);
                //rep.DataTable = dt;
                //rep.typedata = TypeData.DataTable;


                Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojPohybDataTable dt = new Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojPohybDataTable();

                dt.Clear();

                foreach (DataGridViewRow row in dgServis.SelectedRows)
                {

                    Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojPohybRow Radek = dt.NewCZMST_Servis_ZdrojPohybRow();
                    Radek = ((DataRowView)row.DataBoundItem).Row as Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojPohybRow;

                    //Radek = row;
                    dt.ImportRow(Radek);

                }

                dt.AcceptChanges();

                PrintReport(dt, "Ruční výběr");


                #region Puvodny tisk
                //        OrderedEnumerableRowCollection<Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojPohybRow> date = dt.OrderBy(x => x.Modified);
                //        //int cnt = 0;

                //        var odberatele = dt
                //             .Where(x => !x.IsOdberatelOznaceniNull() && !x.IsODB_IDNull())
                //             .GroupBy(x => new
                //             {
                //                 x.OdberatelOznaceni //(string)(x.IsOdberatelOznaceniNull() ? string.Empty : x.OdberatelOznaceni)
                //                 ,
                //                 x.ODB_ID //, ( x.IsODB_IDNull() ? string.Empty : x.ODB_ID )
                //             });
                //        string odber = "-";
                //        if (odberatele.Count() == 1)
                //        {
                //            var odberatel = odberatele.First();
                //            odber = String.Format("{0} ({1})", odberatel.Key.OdberatelOznaceni.Trim(), odberatel.Key.ODB_ID.Trim());
                //        }
                //        rep.Params = new Microsoft.Reporting.WinForms.ReportParameter[]
                //{

                //    new Microsoft.Reporting.WinForms.ReportParameter("Odberatel",odber),
                //    //new Microsoft.Reporting.WinForms.ReportParameter("Odberatel",
                //    //    dt.Count(x => x.OdberatelOznaceni == dt.First().OdberatelOznaceni) == dt.Count ? 
                //    //    dt.First().OdberatelOznaceni : "-"),



                //    new Microsoft.Reporting.WinForms.ReportParameter("DatumOd",date.First().Modified == null ? "-" : date.First().Modified.ToShortDateString() ),
                //    new Microsoft.Reporting.WinForms.ReportParameter("DatumDo",date.Last().Modified == null ? "-" : date.Last().Modified.ToShortDateString()),


                //    new Microsoft.Reporting.WinForms.ReportParameter("Status" ,"Ruční výběr"),
                //    //new ReportParameter("Url",tb_url.Text),

                //    new Microsoft.Reporting.WinForms.ReportParameter("ImageURL", new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Servis", "Print", "Templates", "LOGO.jpg")).AbsoluteUri)

                //};


                //        //dt.AcceptChanges();

                //        rep.DataTable = dt;
                //        rep.typedata = TypeData.DataTable;

                //        //rep.ienumerable = _datagridview.SelectedRows;
                //        //rep.typedata = TypeData.IEnumerable;

                //        rep.NazevDataTable = "DataSet"; // zmatecny nazev v reportu, jedna se o datatable ale je pomenovany jak dataset... upravit

                //        rep.Path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Servis", "Print", "Templates", "Report_TEST.rdlc");
                //        // = @"D:\xxx\xxx_2017\xxx_2017_52_Reporty\template\Report_Params.rdlc";

                //        rep.ShowDialog();
                // }

                #endregion


                DateTime datum = DateTime.Now;

                foreach (DataGridViewRow r in this.dgServis.SelectedRows)
                {
                    var zp = (Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojPohybRow)(((DataRowView)r.DataBoundItem).Row);
                    if (zp.GUID != Guid.Empty)
                        zp.dateExported = datum;
                }
                var l = this.bsServis.List;

                //providerServis.UpdateZdrojPohybExported(guidsExported, DateTime.Now);
                ((Fask.Interfaces.Servis.IServis)providerServis).UpdateZdrojPohyb((Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojPohybDataTable)((DataView)l).Table);

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //throw;
            }
        }

        private void tsmiZrusitPriznakOznacene_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                foreach (DataGridViewRow r in this.dgServis.SelectedRows)
                {
                    var zp = (Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojPohybRow)(((DataRowView)r.DataBoundItem).Row);
                    if (zp.GUID != Guid.Empty)
                        zp.SetdateExportedNull();
                }
                var l = this.bsServis.List;

                //providerServis.UpdateZdrojPohybExported(guidsExported, DateTime.Now);
                ((Fask.Interfaces.Servis.IServis)providerServis).UpdateZdrojPohyb((Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojPohybDataTable)((DataView)l).Table);

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
