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

namespace Konzola.Expedice
{
    public partial class FormBufferBaleniList : Form
    {
        private Fask.Interfaces.IMES providerExpedice = null;
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
                            tsFiltry.Visible = false;
                            tsFiltry.Enabled = false;
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
        private List<Fask.Interfaces.Filtry.ExpediceFormDodaciListyListFiltr> filtry = new List<Fask.Interfaces.Filtry.ExpediceFormDodaciListyListFiltr>();

        /// <summary>
        /// Vybrany filtr.
        /// </summary>
        private Fask.Interfaces.Filtry.ExpediceFormDodaciListyListFiltr rowFiltr
        {
            get
            {
                try
                {
                    return tscbFiltry.SelectedItem as Fask.Interfaces.Filtry.ExpediceFormDodaciListyListFiltr;
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
        string selectedSortID = string.Empty;

        /// <summary>
        /// Radek, ktery se predvoli (pri editaci zaznamu)
        /// </summary>
        private Fask.Interfaces.DataSets.Expedice.CZMST_Expedice_Baleni_BufferRow selectRow { get; set; }

        /// <summary>
        /// Vybrana cinnost.
        /// </summary>
        public Fask.Interfaces.DataSets.Expedice.CZMST_Expedice_Baleni_BufferRow SelectedRow
        {
            get
            {
                try
                {
                    return ((DataRowView)(dgExpedice.BindingContext[bsExpedice].Current)).Row as Fask.Interfaces.DataSets.Expedice.CZMST_Expedice_Baleni_BufferRow;
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
                    return dgExpedice.SelectedRows;
                }
                catch
                {
                    return null;
                }
            }
        }


        #region Inicilozace komponentu datagridview

        //private void InitializeComponent2()
        //{

        //    this.dataGridView1 = new Zuby.ADGV.AdvancedDataGridView();

        //    this.iTEMNMBRDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        //    this.iTEMDESCDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        //    this.qTYSHPPDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        //    this.sERLTNUMDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        //    this.nMBRBALDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        //    this.vNDITNUMDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        //    this.cZCarKodDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        //    this.dEXROWIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        //    this.gUIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();

        //    ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
        //    // 
        //    // iTEMNMBRDataGridViewTextBoxColumn
        //    // 
        //    this.iTEMNMBRDataGridViewTextBoxColumn.DataPropertyName = "ITEMNMBR";
        //    this.iTEMNMBRDataGridViewTextBoxColumn.HeaderText = "ID materálu";
        //    this.iTEMNMBRDataGridViewTextBoxColumn.MinimumWidth = 22;
        //    this.iTEMNMBRDataGridViewTextBoxColumn.Name = "iTEMNMBRDataGridViewTextBoxColumn";
        //    this.iTEMNMBRDataGridViewTextBoxColumn.ReadOnly = true;
        //    this.iTEMNMBRDataGridViewTextBoxColumn.ValueType = typeof(string);
        //    this.iTEMNMBRDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
        //    // 
        //    // iTEMDESCDataGridViewTextBoxColumn
        //    // 
        //    this.iTEMDESCDataGridViewTextBoxColumn.DataPropertyName = "ITEMDESC";
        //    this.iTEMDESCDataGridViewTextBoxColumn.HeaderText = "označení materálu";
        //    this.iTEMDESCDataGridViewTextBoxColumn.MinimumWidth = 22;
        //    this.iTEMDESCDataGridViewTextBoxColumn.Name = "iTEMDESCDataGridViewTextBoxColumn";
        //    this.iTEMDESCDataGridViewTextBoxColumn.ReadOnly = true;
        //    this.iTEMDESCDataGridViewTextBoxColumn.ValueType = typeof(string);
        //    this.iTEMDESCDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
        //    // 
        //    // qTYSHPPDDataGridViewTextBoxColumn
        //    // 
        //    this.qTYSHPPDDataGridViewTextBoxColumn.DataPropertyName = "QTYSHPPD";
        //    this.qTYSHPPDDataGridViewTextBoxColumn.HeaderText = "množství";
        //    this.qTYSHPPDDataGridViewTextBoxColumn.MinimumWidth = 22;
        //    this.qTYSHPPDDataGridViewTextBoxColumn.Name = "qTYSHPPDDataGridViewTextBoxColumn";
        //    this.qTYSHPPDDataGridViewTextBoxColumn.ReadOnly = true;
        //    this.qTYSHPPDDataGridViewTextBoxColumn.ValueType = typeof(decimal);
        //    this.qTYSHPPDDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
        //    // 
        //    // sERLTNUMDataGridViewTextBoxColumn
        //    // 
        //    this.sERLTNUMDataGridViewTextBoxColumn.DataPropertyName = "SERLTNUM";
        //    this.sERLTNUMDataGridViewTextBoxColumn.HeaderText = "SN";
        //    this.sERLTNUMDataGridViewTextBoxColumn.MinimumWidth = 22;
        //    this.sERLTNUMDataGridViewTextBoxColumn.Name = "sERLTNUMDataGridViewTextBoxColumn";
        //    this.sERLTNUMDataGridViewTextBoxColumn.ReadOnly = true;
        //    this.sERLTNUMDataGridViewTextBoxColumn.ValueType = typeof(string);
        //    this.sERLTNUMDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
        //    // 
        //    // nMBRBALDataGridViewTextBoxColumn
        //    // 
        //    this.nMBRBALDataGridViewTextBoxColumn.DataPropertyName = "NMBRBAL";
        //    this.nMBRBALDataGridViewTextBoxColumn.HeaderText = "číslo balení";
        //    this.nMBRBALDataGridViewTextBoxColumn.MinimumWidth = 22;
        //    this.nMBRBALDataGridViewTextBoxColumn.Name = "nMBRBALDataGridViewTextBoxColumn";
        //    this.nMBRBALDataGridViewTextBoxColumn.ReadOnly = true;
        //    this.nMBRBALDataGridViewTextBoxColumn.ValueType = typeof(string);
        //    this.nMBRBALDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
        //    // 
        //    // vNDITNUMDataGridViewTextBoxColumn
        //    // 
        //    this.vNDITNUMDataGridViewTextBoxColumn.DataPropertyName = "VNDITNUM";
        //    this.vNDITNUMDataGridViewTextBoxColumn.HeaderText = "čar. kód dodavatele";
        //    this.vNDITNUMDataGridViewTextBoxColumn.MinimumWidth = 22;
        //    this.vNDITNUMDataGridViewTextBoxColumn.Name = "vNDITNUMDataGridViewTextBoxColumn";
        //    this.vNDITNUMDataGridViewTextBoxColumn.ReadOnly = true;
        //    this.vNDITNUMDataGridViewTextBoxColumn.ValueType = typeof(string);
        //    this.vNDITNUMDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
        //    // 
        //    // cZCarKodDataGridViewTextBoxColumn
        //    // 
        //    this.cZCarKodDataGridViewTextBoxColumn.DataPropertyName = "CZ_CarKod";
        //    this.cZCarKodDataGridViewTextBoxColumn.HeaderText = "čar. kód";
        //    this.cZCarKodDataGridViewTextBoxColumn.MinimumWidth = 22;
        //    this.cZCarKodDataGridViewTextBoxColumn.Name = "cZCarKodDataGridViewTextBoxColumn";
        //    this.cZCarKodDataGridViewTextBoxColumn.ReadOnly = true;
        //    this.cZCarKodDataGridViewTextBoxColumn.ValueType = typeof(string);
        //    this.cZCarKodDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
        //    // 
        //    // dEXROWIDDataGridViewTextBoxColumn
        //    // 
        //    this.dEXROWIDDataGridViewTextBoxColumn.DataPropertyName = "DEX_ROW_ID";
        //    this.dEXROWIDDataGridViewTextBoxColumn.HeaderText = "index";
        //    this.dEXROWIDDataGridViewTextBoxColumn.MinimumWidth = 22;
        //    this.dEXROWIDDataGridViewTextBoxColumn.Name = "dEXROWIDDataGridViewTextBoxColumn";
        //    this.dEXROWIDDataGridViewTextBoxColumn.ReadOnly = true;
        //    this.dEXROWIDDataGridViewTextBoxColumn.ValueType = typeof(Int32);
        //    this.dEXROWIDDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
        //    // 
        //    // gUIDDataGridViewTextBoxColumn
        //    // 
        //    this.gUIDDataGridViewTextBoxColumn.DataPropertyName = "GUID";
        //    this.gUIDDataGridViewTextBoxColumn.HeaderText = "GUID";
        //    this.gUIDDataGridViewTextBoxColumn.MinimumWidth = 22;
        //    this.gUIDDataGridViewTextBoxColumn.Name = "gUIDDataGridViewTextBoxColumn";
        //    this.gUIDDataGridViewTextBoxColumn.ReadOnly = true;
        //    this.gUIDDataGridViewTextBoxColumn.ValueType = typeof(Guid);
        //    this.gUIDDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;

        //    // 
        //    // dataGridView1
        //    // 
        //    this.dataGridView1.AllowUserToAddRows = false;
        //    this.dataGridView1.AllowUserToDeleteRows = false;
        //    this.dataGridView1.AllowUserToOrderColumns = true;
        //    this.dataGridView1.AllowUserToResizeRows = false;
        //    this.dataGridView1.AutoGenerateColumns = false;
        //    this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        //    this.dataGridView1.DataSource = this.bsExpedice;
        //    this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
        //    this.dataGridView1.EnableHeadersVisualStyles = false;
        //    this.dataGridView1.FilterAndSortEnabled = true;
        //    this.dataGridView1.Location = new System.Drawing.Point(0, 162);
        //    this.dataGridView1.Name = "dataGridView1";
        //    this.dataGridView1.ReadOnly = true;
        //    this.dataGridView1.RowHeadersVisible = false;
        //    this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        //    this.dataGridView1.Size = new System.Drawing.Size(659, 306);
        //    this.dataGridView1.TabIndex = 1;
        //    this.dataGridView1.TabStop = false;
        //    this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
        //    this.dataGridView1.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDown);
        //    this.dataGridView1.Sorted += new System.EventHandler(this.dataGridView1_Sorted);




        //    ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();

        //}





        #endregion


        #region Eventy formu

        public FormBufferBaleniList(bool allowMultiSelect, Fask.Interfaces.Classes.ZOBRAZENI_TYP typZobrazeni)
        {
            //InitializeComponent2();
            InitializeComponent();

            this.dgExpedice.UpdateColumnHeaderCellsByDatasource();
            panelButtonsZobrazeniList.Menu = menuStrip2;

            this.dgExpedice.MultiSelect = allowMultiSelect;
            this.Zobrazeni = typZobrazeni;
            if (Zobrazeni == Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST)
                this.WindowState = FormWindowState.Maximized;
        }

        private bool opravneni = false;

        public FormBufferBaleniList(bool allowMultiSelect, Fask.Interfaces.Classes.ZOBRAZENI_TYP typZobrazeni, bool opravneni)
        {
            this.opravneni = opravneni;

            InitializeComponent();

            this.dgExpedice.UpdateColumnHeaderCellsByDatasource();
            panelButtonsZobrazeniList.Menu = menuStrip2;

            this.dgExpedice.MultiSelect = allowMultiSelect;
            this.Zobrazeni = typZobrazeni;
            if (Zobrazeni == Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST)
                this.WindowState = FormWindowState.Maximized;


            //this.dg_OdvodEvents.MultiSelect = allowMultiSelect;
            //this.Zobrazeni = typZobrazeni;

            if (!opravneni)
            {
                menuStrip2.Items.Remove(tsmiPolozka);
                tsmiPolozka.DropDownItems.Remove(tsmiUpravit);
                tsmiPolozka.DropDownItems.Remove(tsmiOdstranit);
            }

            //if (Zobrazeni == Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST)
            //{
            //    panelButtonsZobrazeniList.Menu = menuStrip2;
            //    this.WindowState = FormWindowState.Maximized;
            //}
        }



        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="allowMultiSelect">Povoleni oznaceni vice zaznamu.</param>
        /// <param name="selectRow">Radek, ktery se oznaci.</param>
        public FormBufferBaleniList(bool allowMultiSelect, Fask.Interfaces.DataSets.Expedice.CZMST_Expedice_Baleni_BufferRow selectRow, Fask.Interfaces.Classes.ZOBRAZENI_TYP typZobrazeni)
            : this(allowMultiSelect, typZobrazeni)
        {
            this.selectRow = selectRow;
        }


        private void FormBufferBaleniList_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                //this.WindowState = FormWindowState.Maximized;
                this.dgExpedice.LoadConfiguration(this.GetType().ToString());

                panelButtonsZobrazeniList.LoadConfiguration(this.GetType().ToString());
                panelButtonsZobrazeniList.Init();
                panelButtonsZobrazeniList.Size = new Size(85, 700);

                advancedDataGridViewSearchToolBar1.SetColumns(dgExpedice.Columns);

                //// načtení konfigurace vytvořených filtrů
                this.filtry = Extensions.ListExt.ReadFromXML<Fask.Interfaces.Filtry.ExpediceFormDodaciListyListFiltr>(this.GetType().ToString() + ".filtr");
                tscbFiltry.ComboBox.DataSource = this.filtry;
                tscbFiltry.SelectedItem = null;
                tscbFiltry.ComboBox.DropDownWitdhAutosize();
                // inicializace providera
                InitProvider();

                if (providerExpedice == null)
                    throw new Exception("Provider 'Expedice' není inicializován");


                // 13.7.2016 PeV: jiz se nepouziva, predelano na backgroundworker
                //System.Threading.Thread loadThread = new System.Threading.Thread(LoadDataAsync);
                //loadThread.IsBackground = true;
                //loadThread.Start();
                //PerformVyhledat();
                buttonVyhledat.Focus();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormBufferBaleniList_KeyDown(object sender, KeyEventArgs e)
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

        private void FormBufferBaleniList_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.dgExpedice.SaveConfiguration(this.GetType().ToString());
                panelButtonsZobrazeniList.SaveConfiguration(this.GetType().ToString());
                // ulozeni vsech filtru
                this.filtry.WriteXML(this.GetType().ToString() + ".filtr");

                // cekani na dobehnuti vlakna
                try
                {
                    if (bwLoadExpedice.IsBusy)
                    {
                        bwLoadExpedice.CancelAsync();
                        while (bwLoadExpedice.IsBusy)
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

        private void FormBufferBaleniList_Shown(object sender, EventArgs e)
        {
            try
            {
                this.progressIndicator1.Size = new Size(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ostatni[0].ProgressIndicatorSize, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ostatni[0].ProgressIndicatorSize);
                this.progressIndicator1.Location = new Point(this.dgExpedice.Location.X + (this.dgExpedice.Width / 2) - (progressIndicator1.Size.Width / 2), this.dgExpedice.Location.Y + (this.dgExpedice.Height / 2) - (progressIndicator1.Size.Height / 2));
            }
            catch { }
        }


        #endregion


        /// <summary>
        /// Inicializace providera
        /// </summary>
        private void InitProvider()
        {
            try
            {
                if (string.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                    return;
                
                if (providerExpedice == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Expedice.IExpedice2).IsAssignableFrom(t))
                            {
                                providerExpedice = (Fask.Interfaces.Expedice.IExpedice2)providerAssemlby.CreateInstance(t.FullName);
                                if (providerExpedice != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                // nastaveni connection stringu
                //if (providerExpedice != null)
                //    ((Fask.Interfaces.Expedice.IExpedice2)providerExpedice).ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;

                providerExpedice.InitProvider();

              
                
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


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

        private void buttonNovy_Click(object sender, EventArgs e)
        {
            PerformOK();
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


        private void konecToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

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
                        selectedSortID = SelectedRow.DEX_ROW_ID.ToString();
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
                int pos = this.bsExpedice.Find(dsExpedice.CZMST_Expedice_Polozky.IDColumn.ColumnName, selectedSortID);
                this.bsExpedice.Position = pos;
            }
            catch { }
        }

        private void bwLoadZbozi_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Fask.Interfaces.Filtry.ExpediceFormDodaciListyListFiltr filtr = (Fask.Interfaces.Filtry.ExpediceFormDodaciListyListFiltr)e.Argument;
                Fask.Interfaces.DataSets.Expedice ds = new Fask.Interfaces.DataSets.Expedice();

                if (bwLoadExpedice.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                // nacteni dat v oddelenem vlakne
                //ds = ((Fask.Interfaces.Expedice.IExpedice)providerExpedice).GetFiltrovaneExpedicePolozky(filtr);


                if ((providerExpedice != null) && (providerExpedice is Fask.Interfaces.Expedice.IExpedice2_GetBufferPolozky))
                    ds = ((Fask.Interfaces.Expedice.IExpedice2_GetBufferPolozky)providerExpedice).GetBufferPolozky();
                else
                    throw new NotImplementedException("Provider neimplementuje IExpedice2_GetBufferPolozky.");



                
                if (bwLoadExpedice.CancellationPending)
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

        private void bwLoadZbozi_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                // check error, check cancel, then use result
                if (e.Error != null)
                {
                    // handle the error
                    dsExpedice = new Fask.Interfaces.DataSets.Expedice();
                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, e.Error);
                    MessageBox.Show(e.Error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (e.Cancelled)
                {
                    // handle cancellation
                    // TODO: maji se vymazat data z datasetu?? asi ano ...
                    //if (dsSkladPohyb == null)
                    dsExpedice = new Fask.Interfaces.DataSets.Expedice();
                }
                else
                {
                    // uspesne dokonceno ...
                    // use it on the UI thread
                    dsExpedice = (Fask.Interfaces.DataSets.Expedice)e.Result;
                    if (dsExpedice == null)
                        dsExpedice = new Fask.Interfaces.DataSets.Expedice();
                }

                bsExpedice.DataSource = dsExpedice;
                //dataGridView1.SetDoubleBuffered();
                //dataGridView1.DataSource = bsExpedice;

                bsExpedice.DataMember = dsExpedice.CZMST_Expedice_Baleni_Buffer.TableName;


                //bindingSource_main.DataSource = _dataSet;

                //initialize datagridview
                //advancedDataGridView_main.SetDoubleBuffered();
                //advancedDataGridView_main.DataSource = bindingSource_main;

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
                this.progressIndicator1.Location = new Point(this.dgExpedice.Location.X + (this.dgExpedice.Width / 2) - (progressIndicator1.Size.Width / 2), this.dgExpedice.Location.Y + (this.dgExpedice.Height / 2) - (progressIndicator1.Size.Height / 2));
            }
            catch { }
            progressIndicator1.Start();
            progressIndicator1.Visible = true;
        }

        private void buttonVyhledat_Click(object sender, EventArgs e)
        {
            #region prace s datagridem - nastaveni numeric hodnot na pocet desetinnych mist
            try
            {
                // Procházení sloupců v DataGridView
                foreach (DataGridViewColumn column in dgExpedice.Columns)
                {
                    // Získání typu dat pro sloupec
                    Type dataType = null;

                    if (dgExpedice.DataSource is DataTable dataTable)
                    {
                        // Pokud je datový zdroj DataTable
                        if (dataTable.Columns.Contains(column.DataPropertyName))
                        {
                            dataType = dataTable.Columns[column.DataPropertyName].DataType;
                        }
                    }
                    else if (dgExpedice.DataSource is BindingSource bindingSource)
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

            PerformVyhledat();
        }

        /// <summary>
        /// Vyhledani podle zadaneho filtru.
        /// </summary>
        private void PerformVyhledat()
        {
            try
            {
                DataTable dtchanged = this.dsExpedice.CZMST_Expedice_Baleni_Buffer.GetChanges();
                if (dtchanged != null && dtchanged.Rows.Count > 0) //obsahuje zmeny ... 
                {
                    DialogResult dr = MessageBox.Show("Obsahuje změny, pokračovat?", "data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == System.Windows.Forms.DialogResult.No)
                        return;
                }

                if (bwLoadExpedice.IsBusy)
                {
                    bwLoadExpedice.CancelAsync();
                    while (bwLoadExpedice.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }

                ProgressIndicatorStart();

                Fask.Interfaces.Filtry.ExpediceFormDodaciListyListFiltr filtr = new Fask.Interfaces.Filtry.ExpediceFormDodaciListyListFiltr();
                if (!CreateFilter(ref filtr))
                    return;

                int FirstDisplayedScrollingRowIndex = this.dgExpedice.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

                bwLoadExpedice.RunWorkerAsync(filtr);

                if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dgExpedice.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dgExpedice.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index
            }
            catch (Exception ex)
            {
                ProgressIndicatorStop();
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Vytvoreni filtru, ktery se pouzije.
        /// </summary>
        /// <param name="filtr"></param>
        /// <returns></returns>
        private bool CreateFilter(ref Fask.Interfaces.Filtry.ExpediceFormDodaciListyListFiltr filtr)
        {
            if (filtr == null)
                filtr = new Fask.Interfaces.Filtry.ExpediceFormDodaciListyListFiltr();

            filtr.ITEMNMBR = cbMaterialITEMNMBR.Text.Trim();
            filtr.NMBRPAL = cbMaterialNMBRPAL.Text.Trim();
            filtr.Rozpracovano = cbMaterialRozpracovano.Text.Trim();

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
        private void PerformNastavitFiltr(Fask.Interfaces.Filtry.ExpediceFormDodaciListyListFiltr filtr)
        {
            try
            {
                if (filtr == null)
                    return;

                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                PerformVycistitFiltr();

                cbMaterialITEMNMBR.Text = filtr.ITEMNMBR;         // itemnmbr
                cbMaterialNMBRPAL.Text = filtr.NMBRPAL;      // itemdesc
                cbMaterialRozpracovano.Text = filtr.Rozpracovano;   // hlavicka.Rozpracovano
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
                cbMaterialITEMNMBR.SelectedItem =
                cbMaterialNMBRPAL.SelectedItem = 
                cbMaterialRozpracovano.SelectedItem = null;

                cbMaterialITEMNMBR.Text =
                cbMaterialNMBRPAL.Text = 
                cbMaterialRozpracovano.Text = string.Empty;
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

                Fask.Interfaces.Filtry.ExpediceFormDodaciListyListFiltr filtr = rowFiltr;

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

                Fask.Interfaces.Filtry.ExpediceFormDodaciListyListFiltr filtr = new Fask.Interfaces.Filtry.ExpediceFormDodaciListyListFiltr();
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


        private void advancedDataGridViewSearchToolBar1_Search(object sender, Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs e)
        {
            bool restartsearch = true;
            int startColumn = 0;
            int startRow = 0;
            if (!e.FromBegin)
            {
                bool endcol = dgExpedice.CurrentCell.ColumnIndex + 1 >= dgExpedice.ColumnCount;
                bool endrow = dgExpedice.CurrentCell.RowIndex + 1 >= dgExpedice.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dgExpedice.CurrentCell.ColumnIndex;
                    startRow = dgExpedice.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dgExpedice.CurrentCell.ColumnIndex + 1;
                    startRow = dgExpedice.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dgExpedice.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dgExpedice.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dgExpedice.CurrentCell = c;
        }



        #region Exporty



        private void exportDoCSVVseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                this.dgExpedice.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                this.dgExpedice.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
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

                this.dgExpedice.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                dgExpedice.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
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

                dgExpedice.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                dgExpedice.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #endregion

        private void tsmiOdstranit_Click(object sender, EventArgs e)
        {
            if (!opravneni)
            {
                MessageBox.Show("Nemáte dostatečné oprávnění pro tuto akci!", "Oprávnění", MessageBoxButtons.OK);
                return;
            }

            MessageBox.Show("Neimplementovano");
        }

        private void tsmiUpravit_Click(object sender, EventArgs e)
        {
            if (!opravneni)
            {
                MessageBox.Show("Nemáte dostatečné oprávnění pro tuto akci!", "Oprávnění", MessageBoxButtons.OK);
                return;
            }
            MessageBox.Show("Neimplementovano");
        }

        private void tsmiNovy_Click(object sender, EventArgs e)
        {
            if (!opravneni)
            {
                MessageBox.Show("Nemáte dostatečné oprávnění pro tuto akci!", "Oprávnění", MessageBoxButtons.OK);
                return;
            }
            MessageBox.Show("Neimplementovano");
        }

    }
}
