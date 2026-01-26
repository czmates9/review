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

namespace Konzola.SkladLokace
{
    public partial class FormPohybyIS_List : Form
    {

        #region Parametry

        private string _itemnmbr;
        public string ITEMNMBR
        {
            get { return _itemnmbr; }
            set { _itemnmbr = value; }
        }

        private Fask.Interfaces.IMES provider = null;

        private Fask.Interfaces.Classes.ZOBRAZENI_TYP _zobrazeni = Fask.Interfaces.Classes.ZOBRAZENI_TYP.UNKNOWN;
        public Fask.Interfaces.Classes.ZOBRAZENI_TYP Zobrazeni
        {
            get
            {
                return _zobrazeni;
            }
            set
            {
                if (_zobrazeni != value)
                {
                    _zobrazeni = value;
                    switch (_zobrazeni)
                    {
                        case Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST:
                            panelButtonsZobrazeniVyber.Hide();
                            panelButtonsZobrazeniList.Show();
                            menuStrip2.Items.Remove(tsmiMenuVyber);
                            tsFiltry.Visible = true;
                            tsFiltry.Enabled = true;
                            break;
                        case Fask.Interfaces.Classes.ZOBRAZENI_TYP.VYBER:
                            panelButtonsZobrazeniList.Hide();
                            panelButtonsZobrazeniVyber.Show();
                            menuStrip2.Items.Remove(tsmiMenuList);
                            menuStrip2.Items.Remove(tsmiExporty);
                            tsFiltry.Visible = true; // Z jakeho duvodu to bylo false? snad na to nenarazim :D 
                            tsFiltry.Enabled = true; // Z jakeho duvodu to bylo false? snad na to nenarazim :D 
                            break;
                        case Fask.Interfaces.Classes.ZOBRAZENI_TYP.POHLED:
                            panelButtonsZobrazeniList.Hide();
                            panelButtonsZobrazeniVyber.Hide();
                            menuStrip2.Items.Remove(tsmiMenuList);
                            menuStrip2.Items.Remove(tsmiMenuVyber);
                            menuStrip2.Items.Remove(tsmiExporty);
                            tsFiltry.Visible = false; // Z jakeho duvodu to bylo false? snad na to nenarazim :D 
                            tsFiltry.Enabled = false; // Z jakeho duvodu to bylo false? snad na to nenarazim :D 
                            break;
                        default:
                            break;
                    }
                }
            }
        }


        /// <summary>
        /// Seznam vsech nactenych filtru.
        /// </summary>
        private List<Fask.Interfaces.Filtry.PohybyISListFiltr> filtry = new List<Fask.Interfaces.Filtry.PohybyISListFiltr>();

        /// <summary>
        /// Vybrany filtr.
        /// </summary>
        private Fask.Interfaces.Filtry.PohybyISListFiltr rowFiltr
        {
            get
            {
                try
                {
                    return tscbFiltry.SelectedItem as Fask.Interfaces.Filtry.PohybyISListFiltr;
                }
                catch
                {
                    return null;
                }
            }
        }

        //public Fask.Interfaces.DataSets.Konzola.FASK_LoginsRow loginrow { get; set; }

        /// <summary>
        /// Zvolené id před seřazením
        /// </summary>
        string selectedSortID = string.Empty;

        /// <summary>
        /// Radek, ktery se predvoli (pri editaci zaznamu)
        /// </summary>
        private Fask.Interfaces.DataSets.SkladLokace_CompareToIS.POHODA_PohybyRow selectRow { get; set; }

        /// <summary>
        /// Vybrana cinnost.
        /// </summary>
        public Fask.Interfaces.DataSets.SkladLokace_CompareToIS.POHODA_PohybyRow SelectedRow
        {
            get
            {
                try
                {
                    return ((DataRowView)(dg_PP.BindingContext[bs_PP].Current)).Row as Fask.Interfaces.DataSets.SkladLokace_CompareToIS.POHODA_PohybyRow;
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Kolekce veškerých zvolených sloupců.
        /// </summary>
        public DataGridViewSelectedRowCollection SelectedRows
        {
            get
            {
                try
                {
                    return dg_PP.SelectedRows;
                }
                catch
                {
                    return null;
                }
            }
        }

        #endregion

        #region Eventy formu

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="allowMultiSelect"></param>
        /// <param name="typZobrazeni"></param>
        public FormPohybyIS_List(bool allowMultiSelect, Fask.Interfaces.Classes.ZOBRAZENI_TYP typZobrazeni)
        {
            InitializeComponent();

            this.dg_PP.UpdateColumnHeaderCellsByDatasource();

            this.dg_PP.MultiSelect = allowMultiSelect;
            this.Zobrazeni = typZobrazeni;
            if (Zobrazeni == Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST)
            {
                this.WindowState = FormWindowState.Maximized;
                panelButtonsZobrazeniList.Menu = menuStrip2;
            }
        }


        private void FormPohybyIS_List_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                //this.WindowState = FormWindowState.Maximized;
                this.dg_PP.LoadConfiguration(this.GetType().ToString());


                if (Zobrazeni == Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST)
                {
                    panelButtonsZobrazeniList.LoadConfiguration(this.GetType().ToString());
                    panelButtonsZobrazeniList.Init();
                }

                advancedDataGridViewSearchToolBar1.SetColumns(dg_PP.Columns);

                //// načtení konfigurace vytvořených filtrů
                this.filtry = Extensions.ListExt.ReadFromXML<Fask.Interfaces.Filtry.PohybyISListFiltr>(this.GetType().ToString() + ".filtr");
                tscbFiltry.ComboBox.DataSource = this.filtry;
                tscbFiltry.SelectedItem = null;
                tscbFiltry.ComboBox.DropDownWitdhAutosize();


                // inicializace provider
                InitProvider();

                if (provider == null)
                    throw new Exception("Provider 'Pohyby IS' není inicializován");

                DateTime today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);

                dtp_Vytvoreno_OD.Value = today.AddDays(-1);
                dtp_Vytvoreno_DO.Value = today;

                dtp_Vytvoreno_OD.Checked = false;
                dtp_Vytvoreno_DO.Checked = false;

                dtp_Upraveno_OD.Value = today.AddDays(-1);
                dtp_Upraveno_DO.Value = today;


                dtp_Upraveno_OD.Checked = false;
                dtp_Upraveno_DO.Checked = false;

                cb_Upraveno_TimeVariant.Items.AddRange(Fask.Interfaces.Classes.TimeFilters.GetNumberNameRange());
                cb_Upraveno_TimeVariant.SelectedIndex = 0;

                cb_Vytvoreno_TimeVariant.Items.AddRange(Fask.Interfaces.Classes.TimeFilters.GetNumberNameRange());
                cb_Vytvoreno_TimeVariant.SelectedIndex = 0;


                btn_Vyhledat.Focus();


                if (!string.IsNullOrEmpty(_itemnmbr))
                {
                    tb_ITEMNMBR.Text = _itemnmbr;
                    buttonVyhledat_Click(null, null);
                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormPohybyIS_List_KeyDown(object sender, KeyEventArgs e)
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

        private void FormPohybyIS_List_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.dg_PP.SaveConfiguration(this.GetType().ToString());

                if (Zobrazeni == Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST)
                {
                    panelButtonsZobrazeniList.SaveConfiguration(this.GetType().ToString());
                }

                // ulozeni vsech filtru
                this.filtry.WriteXML(this.GetType().ToString() + ".filtr");

                // cekani na dobehnuti vlakna
                try
                {
                    if (bw_PP.IsBusy)
                    {
                        bw_PP.CancelAsync();
                        while (bw_PP.IsBusy)
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

        private void FormPohybyIS_List_Shown(object sender, EventArgs e)
        {
            try
            {
                this.progressIndicator1.Size = new Size(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ostatni[0].ProgressIndicatorSize, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ostatni[0].ProgressIndicatorSize);
                this.progressIndicator1.Location = new Point(this.dg_PP.Location.X + (this.dg_PP.Width / 2) - (progressIndicator1.Size.Width / 2), this.dg_PP.Location.Y + (this.dg_PP.Height / 2) - (progressIndicator1.Size.Height / 2));
            }
            catch { }
        }

        #endregion


        #region Casove filtry

        public Fask.Interfaces.Classes.TimeFilters.TimeVariants GetTimeVariantFromFilter_Upraveno()
        {
            Fask.Interfaces.Classes.TimeVariantName type = (Fask.Interfaces.Classes.TimeVariantName)cb_Upraveno_TimeVariant.SelectedItem;
            return type.GetTimeVarianta();
        }

        public Fask.Interfaces.Classes.TimeFilters.TimeVariants GetTimeVariantFromFilter_Vytvoreno()
        {
            Fask.Interfaces.Classes.TimeVariantName type = (Fask.Interfaces.Classes.TimeVariantName)cb_Vytvoreno_TimeVariant.SelectedItem;
            return type.GetTimeVarianta();
        }

        #endregion

        #region Inicializace Providera

        /// <summary>
        /// Inicializace providera
        /// </summary>
        private void InitProvider()
        {
            try
            {
                if (!String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                {
                    if (provider == null) //inicializace se provede pouze pokud nebyla provedena ... 
                    {
                        //Assembly providerAssemlby = Assembly.LoadFrom(Server.MapPath(providerAssemblyPath));
                        Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                        Type[] types = providerAssemlby.GetTypes();
                        foreach (Type t in types)
                        {
                            try
                            {
                                //t.IsAssignableFrom(typeof(Fask.Server.Interfaces.Vydej.IVydej);
                                if (typeof(Fask.Interfaces.SkladLokace.ISkladLokace2).IsAssignableFrom(t))
                                {
                                    provider = (Fask.Interfaces.SkladLokace.ISkladLokace2)providerAssemlby.CreateInstance(t.FullName);
                                    if (provider != null)
                                        break;
                                }
                            }
                            catch { }
                        }
                        //return config;
                    }

                    provider.InitProvider();

                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        #endregion

        #region Click Eventy

        private void buttonNovy_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        private void konecToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void obnovitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //UpdateForm(SelectedRow);
            PerformVyhledat();
        }

        private void buttonVyhledat_Click(object sender, EventArgs e)
        {
            PerformVyhledat();
        }

        private void tsmiImportovatZbozi_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtchanged = this.ds_PP.POHODA_Pohyby.GetChanges();
                if (dtchanged != null && dtchanged.Rows.Count > 0) //obsahuje zmeny ... 
                {
                    DialogResult dr = MessageBox.Show("Obsahuje změny, pokračovat?", "data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == System.Windows.Forms.DialogResult.No)
                        return;
                }

                if (bw_PP.IsBusy)
                {
                    bw_PP.CancelAsync();
                    while (bw_PP.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }

                ProgressIndicatorStart();

                int FirstDisplayedScrollingRowIndex = this.dg_PP.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

                bw_PP.RunWorkerAsync();

                if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dg_PP.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dg_PP.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index
            }
            catch (Exception ex)
            {
                ProgressIndicatorStop();
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Perform Metody

        private void PerformCancel()
        {
            try
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PerformOK()
        {
            try
            {
                if (Zobrazeni != Fask.Interfaces.Classes.ZOBRAZENI_TYP.VYBER)
                    return;

                if (SelectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam", this.Text, MessageBoxButtons.OK);
                    return;
                }

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //finally
            //{
            //}            
        }

        /// <summary>
        /// Vyhledani podle zadaneho filtru.
        /// </summary>
        private void PerformVyhledat()
        {
            try
            {
                DataTable dtchanged = this.ds_PP.POHODA_Pohyby.GetChanges();
                if (dtchanged != null && dtchanged.Rows.Count > 0) //obsahuje zmeny ... 
                {
                    DialogResult dr = MessageBox.Show("Obsahuje změny, pokračovat?", "data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == System.Windows.Forms.DialogResult.No)
                        return;
                }

                if (bw_PP.IsBusy)
                {
                    bw_PP.CancelAsync();
                    while (bw_PP.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }

                ProgressIndicatorStart();

                Fask.Interfaces.Filtry.PohybyISListFiltr filtr = new Fask.Interfaces.Filtry.PohybyISListFiltr();
                if (!CreateFilter(ref filtr))
                    return;

                int FirstDisplayedScrollingRowIndex = this.dg_PP.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

                bw_PP.RunWorkerAsync(filtr);

                if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dg_PP.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dg_PP.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index
            }
            catch (Exception ex)
            {
                ProgressIndicatorStop();
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region DataGrid Eventy

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                    PerformOK();
            }
            catch
            {
            }
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1)
                {
                    if (SelectedRow != null)
                        selectedSortID = SelectedRow.ITEMNMBR;
                }
            }
            catch
            {
            }
        }

        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            try
            {
                int pos = this.bs_PP.Find(ds_PP.POHODA_Pohyby.ITEMNMBRColumn.ColumnName, selectedSortID);
                this.bs_PP.Position = pos;
            }
            catch { }
        }

        private void advancedDataGridViewSearchToolBar1_Search(object sender, Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs e)
        {
            bool restartsearch = true;
            int startColumn = 0;
            int startRow = 0;
            if (!e.FromBegin)
            {
                bool endcol = dg_PP.CurrentCell.ColumnIndex + 1 >= dg_PP.ColumnCount;
                bool endrow = dg_PP.CurrentCell.RowIndex + 1 >= dg_PP.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dg_PP.CurrentCell.ColumnIndex;
                    startRow = dg_PP.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dg_PP.CurrentCell.ColumnIndex + 1;
                    startRow = dg_PP.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dg_PP.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dg_PP.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dg_PP.CurrentCell = c;
        }


        #endregion

        #region BackGround Worker bwLoadZbozi

        private void bw_PP_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Fask.Interfaces.Filtry.PohybyISListFiltr filtr = (Fask.Interfaces.Filtry.PohybyISListFiltr)e.Argument;
                Fask.Interfaces.DataSets.SkladLokace_CompareToIS ds = new Fask.Interfaces.DataSets.SkladLokace_CompareToIS();

                if (bw_PP.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                // nacteni dat v oddelenem vlakne
                if ((provider != null) && (provider is Fask.Interfaces.SkladLokace.ISkladLokace2_GetPohybyIS))
                {
                    ds = ((Fask.Interfaces.SkladLokace.ISkladLokace2_GetPohybyIS)provider).GetPohybyIS(filtr);
                }
                else
                {
                    throw new NotImplementedException("Provider neobsahuje implemetaci IZbozi2_GetFiltrovaneZbozi");
                }

                if (bw_PP.CancellationPending)
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

        private void bw_PP_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                // check error, check cancel, then use result
                if (e.Error != null)
                {
                    // handle the error
                    ds_PP = new Fask.Interfaces.DataSets.SkladLokace_CompareToIS();
                    bs_PP.DataSource = ds_PP;
                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, e.Error);
                    MessageBox.Show(e.Error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (e.Cancelled)
                {
                    // handle cancellation
                    // TODO: maji se vymazat data z datasetu?? asi ano ...
                    //if (dsSkladPohyb == null)
                    ds_PP = new Fask.Interfaces.DataSets.SkladLokace_CompareToIS();
                    bs_PP.DataSource = ds_PP;
                }
                else
                {
                    // uspesne dokonceno ...
                    // use it on the UI thread
                    ds_PP = (Fask.Interfaces.DataSets.SkladLokace_CompareToIS)e.Result;
                    if (ds_PP == null)
                        ds_PP = new Fask.Interfaces.DataSets.SkladLokace_CompareToIS();

                    bs_PP.DataSource = ds_PP;
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

        #endregion

        #region Filtry

        private void tsbNastavit_Click(object sender, EventArgs e)
        {
            PerformNastavitFiltr();
        }

        private void tsbVycistit_Click(object sender, EventArgs e)
        {
            PerformVycistitFiltr();
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

        /// <summary>
        /// Vytvoreni filtru, ktery se pouzije.
        /// </summary>
        /// <param name="filtr"></param>
        /// <returns></returns>
        private bool CreateFilter(ref Fask.Interfaces.Filtry.PohybyISListFiltr filtr)
        {
            if (filtr == null)
                filtr = new Fask.Interfaces.Filtry.PohybyISListFiltr();

            filtr.ITEMNMBR = tb_ITEMNMBR.Text.Trim();
            filtr.ITEMDESC = tb_ITEMDESC.Text.Trim();
            filtr.ITEMCODE = tb_ITEMCODE.Text.Trim();

            if (dtp_Upraveno_OD.Checked)
            {
                filtr.DatumUlozeni_OD = dtp_Upraveno_OD.Value;
            }
            else
                filtr.DatumUlozeni_OD = null;

            if (dtp_Upraveno_DO.Checked)
            {
                filtr.DatumUlozeni_DO = dtp_Upraveno_DO.Value;
            }
            else
                filtr.DatumUlozeni_DO = null;



            var v = GetTimeVariantFromFilter_Upraveno();

            if (v == Fask.Interfaces.Classes.TimeFilters.TimeVariants.unknow)
            {
                filtr.DatumUlozeni_TimeVariant = null;
            }
            else
            {
                filtr.DatumUlozeni_TimeVariant = v;
            }

            if (dtp_Vytvoreno_OD.Checked)
            {
                filtr.DatumVytvoreni_OD = dtp_Vytvoreno_OD.Value;
            }
            else
                filtr.DatumVytvoreni_OD = null;

            if (dtp_Vytvoreno_DO.Checked)
            {
                filtr.DatumVytvoreni_DO = dtp_Vytvoreno_DO.Value;
            }
            else
                filtr.DatumVytvoreni_DO = null;

            var vy = GetTimeVariantFromFilter_Vytvoreno();

            if (vy == Fask.Interfaces.Classes.TimeFilters.TimeVariants.unknow)
            {
                filtr.DatumVytvoreni_TimeVariant= null;
            }
            else
            {
                filtr.DatumVytvoreni_TimeVariant = vy;
            }

            return true;
        }

        /// <summary>
        /// Nastavi zvoleny filtr do comboboxu, ...
        /// </summary>
        /// <param name="filtr"></param>
        private void PerformNastavitFiltr()
        {
            try
            {
                if (rowFiltr == null)
                    return;

                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                PerformVycistitFiltr();

                tb_ITEMNMBR.Text = rowFiltr.ITEMNMBR;         // itemnmbr
                tb_ITEMCODE.Text = rowFiltr.ITEMCODE;   // itemcode
                tb_ITEMDESC.Text = rowFiltr.ITEMDESC;      // itemdesc

                if (rowFiltr.DatumUlozeni_OD != null)
                {
                    dtp_Vytvoreno_OD.Checked = true;
                    dtp_Vytvoreno_OD.Value = (DateTime)rowFiltr.DatumUlozeni_OD;
                }

                if (rowFiltr.DatumUlozeni_DO != null)
                {
                    dtp_Vytvoreno_DO.Checked = true;
                    dtp_Vytvoreno_DO.Value = (DateTime)rowFiltr.DatumUlozeni_DO;
                }


                if (rowFiltr.DatumVytvoreni_OD != null)
                {
                    dtp_Vytvoreno_OD.Checked = true;
                    dtp_Vytvoreno_OD.Value = (DateTime)rowFiltr.DatumVytvoreni_OD;
                }

                if (rowFiltr.DatumVytvoreni_DO != null)
                {
                    dtp_Vytvoreno_DO.Checked = true;
                    dtp_Vytvoreno_DO.Value = (DateTime)rowFiltr.DatumVytvoreni_DO;
                }

                if (rowFiltr.DatumVytvoreni_TimeVariant != null)
                {
                    cb_Vytvoreno_TimeVariant.SelectedItem = rowFiltr.DatumVytvoreni_TimeVariant;
                }

                if (rowFiltr.DatumUlozeni_TimeVariant != null)
                {
                    cb_Upraveno_TimeVariant.SelectedItem = rowFiltr.DatumUlozeni_TimeVariant;
                }

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
                tb_ITEMDESC.Text =
                tb_ITEMCODE.Text =
                tb_ITEMNMBR.Text = string.Empty;

                dtp_Upraveno_DO.Checked = false;
                dtp_Upraveno_OD.Checked = false;


                dtp_Vytvoreno_DO.Checked = false;
                dtp_Vytvoreno_OD.Checked = false;

                cb_Upraveno_TimeVariant.SelectedIndex = 0;
                cb_Vytvoreno_TimeVariant.SelectedIndex = 0;

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

                if (rowFiltr == null)
                    return;

                DialogResult dr = MessageBox.Show("Opravdu chcete změnit vybraný filtr '" + rowFiltr.NazevFiltru.Trim() + "'?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr != System.Windows.Forms.DialogResult.Yes)
                    return;

                Fask.Interfaces.Filtry.PohybyISListFiltr filtr = rowFiltr;

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
        /// Ulozeni filtru do souboru.
        /// </summary>
        private void PerformPridatFiltr()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                Fask.Interfaces.Filtry.PohybyISListFiltr filtr = new Fask.Interfaces.Filtry.PohybyISListFiltr();
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

                this.dg_PP.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                this.dg_PP.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
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

                this.dg_PP.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                dg_PP.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
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

                dg_PP.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                dg_PP.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region ProgressIndicator

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
                this.progressIndicator1.Location = new Point(this.dg_PP.Location.X + (this.dg_PP.Width / 2) - (progressIndicator1.Size.Width / 2), this.dg_PP.Location.Y + (this.dg_PP.Height / 2) - (progressIndicator1.Size.Height / 2));
            }
            catch { }
            progressIndicator1.Start();
            progressIndicator1.Visible = true;
        }


        #endregion


    }
}
