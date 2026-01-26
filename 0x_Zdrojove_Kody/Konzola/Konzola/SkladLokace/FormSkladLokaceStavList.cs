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
using System.IO;

namespace Konzola.SkladLokace
{
    /// <summary>
    /// Formular pro vyber/zobrazeni aktualniho stavu skladu lokacniho mechanismu
    /// </summary>
    public partial class FormSkladLokaceStavList : Form
    {

        #region Parametry

        private string _itemnmbr;
        public string ITEMNMBR
        {
            get { return _itemnmbr; }
            set { _itemnmbr = value; }
        }

        private Fask.Interfaces.IMES providerSkladLokace = null;
        private Fask.Interfaces.IMES providerInventura = null;
        private Fask.BarCodeGraphics.IBarCodeGraphics providerTiskKod = null;

        private Fask.Interfaces.IMES providerZbozi = null;

        private Fask.Interfaces.IMES providerTISK = null;


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
                            menuStrip2.Items.Remove(tsmiAkce);
                            menuStrip2.Items.Remove(tsmiVystup);
                            tsFiltry.Visible = false;
                            tsFiltry.Enabled = false;
                            break;
                        case Fask.Interfaces.Classes.ZOBRAZENI_TYP.POHLED:
                            panelButtonsZobrazeniList.Hide();
                            panelButtonsZobrazeniVyber.Hide();
                            menuStrip2.Items.Remove(tsmiMenuVyber);
                            menuStrip2.Items.Remove(tsmiMenuList);
                            menuStrip2.Items.Remove(tsmiAkce);
                            menuStrip2.Items.Remove(tsmiVystup);
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
        private List<Fask.Interfaces.Filtry.SkladLokaceStavListFiltr> filtry = new List<Fask.Interfaces.Filtry.SkladLokaceStavListFiltr>();

        /// <summary>
        /// Vybrany filtr.
        /// </summary>
        private Fask.Interfaces.Filtry.SkladLokaceStavListFiltr rowFiltr
        {
            get
            {
                try
                {
                    return tscbFiltry.SelectedItem as Fask.Interfaces.Filtry.SkladLokaceStavListFiltr;
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
        private Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_StavRow selectRow { get; set; }

        /// <summary>
        /// Vybrany radek.
        /// </summary>
        public Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_StavRow SelectedRow
        {
            get
            {
                try
                {
                    return ((DataRowView)(dgSkladLokace.BindingContext[bsSkladLokace].Current)).Row as Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_StavRow;
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
                    return dgSkladLokace.SelectedRows;
                }
                catch
                {
                    return null;
                }
            }
        }

        ContextMenu m = new ContextMenu();

        #endregion

        #region Eventy formu

        public FormSkladLokaceStavList(bool allowMultiSelect, Fask.Interfaces.Classes.ZOBRAZENI_TYP typZobrazeni)
        {
            InitializeComponent();

            this.Zobrazeni = typZobrazeni;

            this.dgSkladLokace.UpdateColumnHeaderCellsByDatasource();
            this.dgSkladLokace.MultiSelect = allowMultiSelect;


            if (Zobrazeni == Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST)
            {
                panelButtonsZobrazeniList.Menu = menuStrip2;
            }
                        
            if (Zobrazeni == Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST)
            {
                this.WindowState = FormWindowState.Maximized;

                m.MenuItems.Add(new MenuItem("Příjem", PrijemPohyb_Click));
                m.MenuItems.Add(new MenuItem("Výdej", VydejPohyb_Click));
                m.MenuItems.Add(new MenuItem("Převod", PrelokovaniPohyb_Click));
            }

            
        }

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="allowMultiSelect">Povoleni oznaceni vice zaznamu.</param>
        /// <param name="selectRow">Radek, ktery se oznaci.</param>
        public FormSkladLokaceStavList(bool allowMultiSelect, Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_StavRow selectRow, Fask.Interfaces.Classes.ZOBRAZENI_TYP typZobrazeni)
            : this(allowMultiSelect, typZobrazeni)
        {
            this.selectRow = selectRow;
        }

        private void FormSkladLokaceStavList_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                //this.WindowState = FormWindowState.Maximized;
                this.dgSkladLokace.LoadConfiguration(this.GetType().ToString());
                if (Zobrazeni == Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST)
                {
                    panelButtonsZobrazeniList.LoadConfiguration(this.GetType().ToString());
                    panelButtonsZobrazeniList.Init();
                    panelButtonsZobrazeniList.Size = new Size(85, 700);
                }

                advancedDataGridViewSearchToolBar1.SetColumns(dgSkladLokace.Columns);

                //// načtení konfigurace vytvořených filtrů
                this.filtry = Extensions.ListExt.ReadFromXML<Fask.Interfaces.Filtry.SkladLokaceStavListFiltr>(this.GetType().ToString() + ".filtr");
                tscbFiltry.ComboBox.DataSource = this.filtry;
                tscbFiltry.SelectedItem = null;
                tscbFiltry.ComboBox.DropDownWitdhAutosize();

                // inicializace providera
                InitProvider();

                if (providerSkladLokace == null)
                    throw new Exception("Provider 'SkladLokace' není inicializován");

                if (providerInventura == null)
                    throw new Exception("Provider 'Inventura' není inicializován");

                if (providerZbozi == null)
                    throw new Exception("Provider 'Zboží' není inicializován");

                // 13.7.2016 PeV: jiz se nepouziva, predelano na backgroundworker
                //System.Threading.Thread loadThread = new System.Threading.Thread(LoadDataAsync);
                //loadThread.IsBackground = true;
                //loadThread.Start();
                //PerformVyhledat();
                buttonVyhledat.Focus();

                if (!string.IsNullOrEmpty(_itemnmbr))
                {
                    cbMaterialITEMNMBR.Text = _itemnmbr;
                    buttonVyhledat_Click(null, null);
                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormSkladLokaceStavList_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.dgSkladLokace.SaveConfiguration(this.GetType().ToString());

                if (Zobrazeni == Fask.Interfaces.Classes.ZOBRAZENI_TYP.LIST)
                {
                    panelButtonsZobrazeniList.SaveConfiguration(this.GetType().ToString());
                }
                // ulozeni vsech filtru
                this.filtry.WriteXML(this.GetType().ToString() + ".filtr");

                // cekani na dobehnuti vlakna
                try
                {
                    if (bwLoadSkladLokaceStav.IsBusy)
                    {
                        bwLoadSkladLokaceStav.CancelAsync();
                        while (bwLoadSkladLokaceStav.IsBusy)
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

        private void FormSkladLokaceStavList_Shown(object sender, EventArgs e)
        {
            try
            {
                this.progressIndicator1.Size = new Size(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ostatni[0].ProgressIndicatorSize, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Ostatni[0].ProgressIndicatorSize);
                this.progressIndicator1.Location = new Point(this.dgSkladLokace.Location.X + (this.dgSkladLokace.Width / 2) - (progressIndicator1.Size.Width / 2), this.dgSkladLokace.Location.Y + (this.dgSkladLokace.Height / 2) - (progressIndicator1.Size.Height / 2));
            }
            catch { }
        }

        private void FormSkladLokaceStavList_KeyDown(object sender, KeyEventArgs e)
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

        #endregion

        #region Inicalizace Provideru

        /// <summary>
        /// Inicializace providera
        /// </summary>
        private void InitProvider()
        {
            #region Sklad Lokace

            try
            {
                if (String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                    return;

                if (providerSkladLokace == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.SkladLokace.ISkladLokace2).IsAssignableFrom(t))
                            {
                                providerSkladLokace = (Fask.Interfaces.SkladLokace.ISkladLokace2)providerAssemlby.CreateInstance(t.FullName);
                                if (providerSkladLokace != null)
                                    break;
                            }
                        }
                        catch { }
                    }

                }

                providerSkladLokace.InitProvider();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            #endregion

            #region providerInventura

           try
            {
                if (!String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                {
                    if (providerInventura == null) //inicializace se provede pouze pokud nebyla provedena ... 
                    {
                        Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                        Type[] types = providerAssemlby.GetTypes();
                        foreach (Type t in types)
                        {
                            try
                            {
                                if (typeof(Fask.Interfaces.Inventura.IInventura2).IsAssignableFrom(t))
                                {
                                    providerInventura = (Fask.Interfaces.Inventura.IInventura2)providerAssemlby.CreateInstance(t.FullName);
                                    if (providerInventura != null)
                                        break;
                                }
                            }
                            catch { }
                        }
                    }

                    providerInventura.InitProvider();
                  

                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion

            #region providerTiskKod   - Generovani 2D QR kodu do šablony jak obrazek
            try
            {
                if (!String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].Provider_Graphics))
                {
                    if (providerTiskKod == null) //inicializace se provede pouze pokud nebyla provedena ... 
                    {
                        Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].Provider_Graphics));
                        Type[] types = providerAssemlby.GetTypes();
                        foreach (Type t in types)
                        {
                            try
                            {
                                if (typeof(Fask.BarCodeGraphics.IBarCodeGraphics).IsAssignableFrom(t))
                                {
                                    providerTiskKod = (Fask.BarCodeGraphics.IBarCodeGraphics)providerAssemlby.CreateInstance(t.FullName);
                                    if (providerTiskKod != null)
                                        break;
                                }
                            }
                            catch { }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion

            #region providerZbozi

            try
            {
                if (!String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                {
                    if (providerZbozi == null) //inicializace se provede pouze pokud nebyla provedena ... 
                    {
                        //Assembly providerAssemlby = Assembly.LoadFrom(Server.MapPath(providerAssemblyPath));
                        Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                        Type[] types = providerAssemlby.GetTypes();
                        foreach (Type t in types)
                        {
                            try
                            {
                                //t.IsAssignableFrom(typeof(Fask.Server.Interfaces.Vydej.IVydej);
                                if (typeof(Fask.Interfaces.Ciselniky.Zbozi.IZbozi2).IsAssignableFrom(t))
                                {
                                    providerZbozi = (Fask.Interfaces.Ciselniky.Zbozi.IZbozi2)providerAssemlby.CreateInstance(t.FullName);
                                    if (providerZbozi != null)
                                        break;
                                }
                            }
                            catch { }
                        }
                        //return config;
                    }

                    providerZbozi.InitProvider();
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion

            #region providerTiskKod  - Generovani třeba GS1 kodu

            try
            {
                if (!String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderTisk))
                {
                    if (providerTISK == null) //inicializace se provede pouze pokud nebyla provedena ... 
                    {
                        Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderTisk));
                        Type[] types = providerAssemlby.GetTypes();
                        foreach (Type t in types)
                        {
                            try
                            {
                                if (typeof(Fask.Interfaces.Tisky.ITisky2).IsAssignableFrom(t))
                                {
                                    providerTISK = (Fask.Interfaces.Tisky.ITisky2)providerAssemlby.CreateInstance(t.FullName);
                                    if (providerTISK != null)
                                        break;
                                }
                            }
                            catch { }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            #endregion

        }

        #endregion

        #region Metody pravy click mouse

        private void PrijemPohyb_Click(object sender, EventArgs e)
        {
            try
            {
                PerformCreateRecord(Fask.Interfaces.SkladLokace.TypeOfRecord.P);
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }   
        }

        private void VydejPohyb_Click(object sender, EventArgs e)
        {
            try
            {
                PerformCreateRecord(Fask.Interfaces.SkladLokace.TypeOfRecord.V);
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        }


        private void PrelokovaniPohyb_Click(object sender, EventArgs e)
        {
            try
            {
                PerformCreateRecord(Fask.Interfaces.SkladLokace.TypeOfRecord.D);
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
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
                DataTable dtchanged = this.dsSkladLokace.CZMST_SkladLokace_Stav.GetChanges();
                if (dtchanged != null && dtchanged.Rows.Count > 0) //obsahuje zmeny ... 
                {
                    DialogResult dr = MessageBox.Show("Obsahuje změny, pokračovat?", "data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == System.Windows.Forms.DialogResult.No)
                        return;
                }

                if (bwLoadSkladLokaceStav.IsBusy)
                {
                    bwLoadSkladLokaceStav.CancelAsync();
                    while (bwLoadSkladLokaceStav.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }

                ProgressIndicatorStart();

                Fask.Interfaces.Filtry.SkladLokaceStavListFiltr filtr = new Fask.Interfaces.Filtry.SkladLokaceStavListFiltr();
                if (!CreateFilter(ref filtr))
                    return;

                int FirstDisplayedScrollingRowIndex = this.dgSkladLokace.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

                bwLoadSkladLokaceStav.RunWorkerAsync(filtr);

                if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dgSkladLokace.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dgSkladLokace.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index
            }
            catch (Exception ex)
            {
                ProgressIndicatorStop();
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

                using (SkladLokace.FormSkladLokaceStavEdit frm = new FormSkladLokaceStavEdit())
                {
                    frm.Text = "Nový pohyb";
                    if (frm.ShowDialog(this) != DialogResult.OK)
                        return;

                    // prozatim nove vyhledani ... jinak by se muselo pokusit vyhledat zaznam, jestli vubec existuje, pripadne ho pridat nebo odstranit ...
                    PerformVyhledat();
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PerformCreateRecord(Fask.Interfaces.SkladLokace.TypeOfRecord typeOfRecord)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;


                if ((SelectedRows == null) || (SelectedRows.Count == 0))
                {
                    MessageBox.Show("Není zvolen žádný řádek", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (SelectedRows.Count > 1)
                {
                    MessageBox.Show("Je zvoleno vícero řádků", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Fask.Interfaces.Filtry.ZboziListFiltr filtr = new Fask.Interfaces.Filtry.ZboziListFiltr()
                {
                    MaterialID = SelectedRow.ITEMNMBR,
                    SKL_ID = SelectedRow.SKL_ID
                };
                
                
                Fask.Interfaces.DataSets.Zbozi ds = new Fask.Interfaces.DataSets.Zbozi();



                if ((providerZbozi != null) && (providerZbozi is Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_GetFiltrovaneZbozi))
                {
                    ds = ((Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_GetFiltrovaneZbozi)providerZbozi).GetFiltrovaneZbozi(filtr);
                }
                else
                {
                    throw new NotImplementedException("Provider neobsahuje implemetaci IZbozi2_GetFiltrovaneZbozi");
                }


                Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_ALL_KONZOLARow row = null;
                if (ds.FASK_ZASOBY_ALL_KONZOLA.Count > 0)
                {
                    row = ds.FASK_ZASOBY_ALL_KONZOLA.First();
                }

                if (row == null)
                {
                    string msg = string.Format("Položka ID: '{0}' ze skladu ID: '{1}', nebyla nalezena v číselníku zásob FASK.", SelectedRow.ITEMNMBR, SelectedRow.SKL_ID);
                    MessageBox.Show(this, msg, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1 );
                    return;
                }

                using (SkladLokace.FormSkladLokaceStavEdit frm = new FormSkladLokaceStavEdit())
                {
                    frm.Text = "Nový pohyb";

                    frm.ITEMNMBR = SelectedRow.ITEMNMBR;
                    frm.ITEMDESC = (SelectedRow.IsITEMDESCNull() ? string.Empty : SelectedRow.ITEMDESC.Trim()) + (SelectedRow.IsITEMDESCNull() ? string.Empty : (" (" + SelectedRow.ITEMCODE.Trim() + ")")); ;
                    frm.SKL_ID = SelectedRow.SKL_ID;
                    frm.LOCNCODE = SelectedRow.LOCNCODE;
                    frm.SERLTNUM = SelectedRow.SERLTNUM;
                    frm.CZ_SerNum_Track = row.CZ_SerNum_Track;
                    frm.USER_ID = FASK.Logins.Uzivatel.Instance.UserID;


                    frm.TypPohyby = typeOfRecord;

                    if (frm.ShowDialog(this) != DialogResult.OK)
                        return;

                    // prozatim nove vyhledani ... jinak by se muselo pokusit vyhledat zaznam, jestli vubec existuje, pripadne ho pridat nebo odstranit ...
                    PerformVyhledat();
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PerformFillLokMechFromINV()
        {
            try
            {
                //1. Skontrolovat zda je neco v tabulce CZMST_SkladLokace_Stav
                //1.1 ANO, tak dotaz " V tabulce Lok. Mech. Bylo nalezeno XY řadku, Smazat?"
                //1.1.1 ANO, tak smazat tabulku CZMST_SkladLokace_Stav a CZMST_SkladLokace_StavPohyb
                //1.1.2 NE, RETURN, nic se nedeje
                //1.2 NE,  RETURN, nic se nedeje
                //2. Otevře se okno, ve kterem bude pohled na CZMST_I1H
                //nebude zde multirowselect, a vybrana davka bude dál pokračovat
                //3. Zde se zavolá BackGroudWorker
                //3.1 Zavolá v provideru proceduru, která přehrne z I4 do Lok Mech....

                Fask.Interfaces.Filtry.SkladLokaceStavListFiltr filtr = new Fask.Interfaces.Filtry.SkladLokaceStavListFiltr();
                Fask.Interfaces.DataSets.SkladLokace ds = new Fask.Interfaces.DataSets.SkladLokace();

                if ((providerSkladLokace != null) && (providerSkladLokace is Fask.Interfaces.SkladLokace.ISkladLokace2_GetFiltrovanySkladLokaceStav))
                    ds = ((Fask.Interfaces.SkladLokace.ISkladLokace2_GetFiltrovanySkladLokaceStav)providerSkladLokace).GetFiltrovanySkladLokaceStav(filtr);
                else
                    throw new NotImplementedException("Provider neimplementuje ISkladLokace2_GetFiltrovanySkladLokaceStav.");

                if (ds.CZMST_SkladLokace_Stav.Count != 0)
                {
                    string msg = string.Format("V lokačním mechanizmu bylo nalezeno {0} řádků. " + Environment.NewLine + "Přejete si je smazat?", ds.CZMST_SkladLokace_Stav.Count);

                    DialogResult dr = MessageBox.Show(this, msg, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    if (dr != DialogResult.Yes)
                    {
                        return;
                    }
                    else
                    {
                        if ((providerSkladLokace != null) && (providerSkladLokace is Fask.Interfaces.SkladLokace.ISkladLokace2_ClearLokMech))
                            ((Fask.Interfaces.SkladLokace.ISkladLokace2_ClearLokMech)providerSkladLokace).ClearLokMech();
                        else
                            throw new NotImplementedException("Provider neimplementuje ISkladLokace2_ClearLokMech.");
                    }
                }

                int? CountEntries = null;
                Fask.Interfaces.DataSets.Inventura dsI1H = new Fask.Interfaces.DataSets.Inventura();

                if ((providerInventura != null) && (providerInventura is Fask.Interfaces.Inventura.IInventura2_GetHlavicky))
                    dsI1H = ((Fask.Interfaces.Inventura.IInventura2_GetHlavicky)providerInventura).GetHlavicky();
                else
                    throw new NotImplementedException("Provider neimplementuje IInventura2_GetHlavicky.");

                using (Konzola.SkladLokace.FormSkladLokaceStavList_SelectCountEntries frm = new FormSkladLokaceStavList_SelectCountEntries())
                {

                    frm.AI1HDataSet = dsI1H;

                    if (frm.ShowDialog(this) != DialogResult.OK)
                        return;

                    if(frm.rowI1H != null)
                        CountEntries = frm.rowI1H.CountEntries;

                }

                if (!CountEntries.HasValue)
                {
                    DialogResult dr = MessageBox.Show(this, "Číslo davky nazadano!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    return;
                }

                int  stateImp = 0;

                if ((providerSkladLokace != null) && (providerSkladLokace is Fask.Interfaces.SkladLokace.ISkladLokace2_ImportLokMech_From_I4))
                    stateImp = ((Fask.Interfaces.SkladLokace.ISkladLokace2_ImportLokMech_From_I4)providerSkladLokace).ImportLokMech_From_I4(CountEntries.Value);
                else
                    throw new NotImplementedException("Provider neimplementuje ISkladLokace2_ImportLokMech_From_I4.");

                PerformVyhledat();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #endregion

        #region Button/Menu Event

        private void buttonNovy_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        private void konecToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void buttonVyhledat_Click(object sender, EventArgs e)
        {
            #region prace s datagridem - nastaveni numeric hodnot na pocet desetinnych mist
            try
            {
                // Procházení sloupců v DataGridView
                foreach (DataGridViewColumn column in dgSkladLokace.Columns)
                {
                    // Získání typu dat pro sloupec
                    Type dataType = null;

                    if (dgSkladLokace.DataSource is DataTable dataTable)
                    {
                        // Pokud je datový zdroj DataTable
                        if (dataTable.Columns.Contains(column.DataPropertyName))
                        {
                            dataType = dataTable.Columns[column.DataPropertyName].DataType;
                        }
                    }
                    else if (dgSkladLokace.DataSource is BindingSource bindingSource)
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

        private void tsmiProvestPohyb_Click(object sender, EventArgs e)
        {
            PerformCreateRecord();
        }

        private void tsmi_FillLokMechFromINV_Click(object sender, EventArgs e)
        {
            PerformFillLokMechFromINV();
        }

        #endregion

        #region DataGridView eventy


        private void dgSkladLokace_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                m.Show(dgSkladLokace, new Point(e.X, e.Y));
            }
        }

        private void dgSkladLokace_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                    PerformOK();
            }
            catch { }
        }

        private void dgSkladLokace_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                // TODO: dodelat ulozeni pozice pri zmene razeni
                //if (e.RowIndex == -1)
                //{
                //    if (SelectedRow != null)
                //        selectedCinnostID = SelectedRow.ITEMCODE;
                //}
            }
            catch { }
        }

        private void dgSkladLokace_Sorted(object sender, EventArgs e)
        {
            try
            {
                // TODO: dodelat ulozeni pozice pri zmene razeni
                //int pos = this.bsSkladLokace.Find(dsSkladLokace.CZMST_SkladLokace_Stav.ITEMCODEColumn.ColumnName, selectedCinnostID);
                //this.bsSkladLokace.Position = pos;
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
                bool endcol = dgSkladLokace.CurrentCell.ColumnIndex + 1 >= dgSkladLokace.ColumnCount;
                bool endrow = dgSkladLokace.CurrentCell.RowIndex + 1 >= dgSkladLokace.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dgSkladLokace.CurrentCell.ColumnIndex;
                    startRow = dgSkladLokace.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dgSkladLokace.CurrentCell.ColumnIndex + 1;
                    startRow = dgSkladLokace.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dgSkladLokace.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dgSkladLokace.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dgSkladLokace.CurrentCell = c;
        }


        #endregion

        #region BackGround Worker

        private void bwLoadZbozi_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Fask.Interfaces.Filtry.SkladLokaceStavListFiltr filtr = (Fask.Interfaces.Filtry.SkladLokaceStavListFiltr)e.Argument;
                Fask.Interfaces.DataSets.SkladLokace ds = new Fask.Interfaces.DataSets.SkladLokace();

                if (bwLoadSkladLokaceStav.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                // nacteni dat v oddelenem vlakne
                //ds = ((Fask.Interfaces.SkladLokace.ISkladLokace)providerSkladLokace).GetFiltrovanySkladLokaceStav(filtr);



                if ((providerSkladLokace != null) && (providerSkladLokace is Fask.Interfaces.SkladLokace.ISkladLokace2_GetFiltrovanySkladLokaceStav))
                    ds = ((Fask.Interfaces.SkladLokace.ISkladLokace2_GetFiltrovanySkladLokaceStav)providerSkladLokace).GetFiltrovanySkladLokaceStav(filtr);
                else
                    throw new NotImplementedException("Provider neimplementuje ISkladLokace2_GetFiltrovanySkladLokaceStav.");


                if (bwLoadSkladLokaceStav.CancellationPending)
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
                    dsSkladLokace = new Fask.Interfaces.DataSets.SkladLokace();
                    bsSkladLokace.DataSource = dsSkladLokace;
                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, e.Error);
                    MessageBox.Show(e.Error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (e.Cancelled)
                {
                    // handle cancellation
                    dsSkladLokace = new Fask.Interfaces.DataSets.SkladLokace();
                    bsSkladLokace.DataSource = dsSkladLokace;
                }
                else
                {
                    // uspesne dokonceno ...
                    // use it on the UI thread
                    dsSkladLokace = (Fask.Interfaces.DataSets.SkladLokace)e.Result;
                    if (dsSkladLokace == null)
                        dsSkladLokace = new Fask.Interfaces.DataSets.SkladLokace();

                    bsSkladLokace.DataSource = dsSkladLokace;
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

        #region Progress indikator

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
                this.progressIndicator1.Location = new Point(this.dgSkladLokace.Location.X + (this.dgSkladLokace.Width / 2) - (progressIndicator1.Size.Width / 2), this.dgSkladLokace.Location.Y + (this.dgSkladLokace.Height / 2) - (progressIndicator1.Size.Height / 2));
            }
            catch { }
            progressIndicator1.Start();
            progressIndicator1.Visible = true;
        }


        #endregion

        #region Filtry

        /// <summary>
        /// Vytvoreni filtru, ktery se pouzije.
        /// </summary>
        /// <param name="filtr"></param>
        /// <returns></returns>
        private bool CreateFilter(ref Fask.Interfaces.Filtry.SkladLokaceStavListFiltr filtr)
        {
            if (filtr == null)
                filtr = new Fask.Interfaces.Filtry.SkladLokaceStavListFiltr();

            filtr.MaterialITEMNMBR = cbMaterialITEMNMBR.Text.Trim();
            filtr.MaterialNazev = cbMaterialOznaceni.Text.Trim();
            filtr.MaterialItemcode = cbMaterialITEMCODE.Text.Trim();
            filtr.MaterialBarcode = cbMaterialBarcode.Text.Trim();
            filtr.MaterialPracID = cbMaterialPracID.Text.Trim();
            filtr.PouzeNenulovyStav= cb_NenulovyStav.Checked;

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
        private void PerformNastavitFiltr(Fask.Interfaces.Filtry.SkladLokaceStavListFiltr filtr)
        {
            try
            {
                if (filtr == null)
                    return;

                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                PerformVycistitFiltr();

                cbMaterialITEMNMBR.Text = filtr.MaterialITEMNMBR;         // itemnmbr
                cbMaterialITEMCODE.Text = filtr.MaterialItemcode;   // itemcode
                cbMaterialBarcode.Text = filtr.MaterialBarcode;     // barcode
                cbMaterialOznaceni.Text = filtr.MaterialNazev;      // itemdesc
                cbMaterialPracID.Text = filtr.MaterialPracID;
                cb_NenulovyStav.Checked = filtr.PouzeNenulovyStav ;
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
                cbMaterialBarcode.SelectedItem =
                cbMaterialITEMCODE.SelectedItem =
                cbMaterialITEMNMBR.SelectedItem =
                cbMaterialPracID.SelectedItem =
                cbMaterialOznaceni.SelectedItem = null;

                cbMaterialBarcode.Text =
                cbMaterialITEMCODE.Text =
                cbMaterialITEMNMBR.Text =
                cbMaterialPracID.Text =
                cbMaterialOznaceni.Text = string.Empty;

                cb_NenulovyStav.Checked = false;
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

                Fask.Interfaces.Filtry.SkladLokaceStavListFiltr filtr = rowFiltr;

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

                Fask.Interfaces.Filtry.SkladLokaceStavListFiltr filtr = new Fask.Interfaces.Filtry.SkladLokaceStavListFiltr();
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

                this.dgSkladLokace.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                this.dgSkladLokace.ExportToCSV(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
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

                this.dgSkladLokace.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                dgSkladLokace.ExportToExcel(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
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

                dgSkladLokace.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.VSE);
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

                dgSkladLokace.ExportToXML(Fask.Interfaces.Classes.EXPORT_DAT.OZNACENE);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        #endregion

        #region Tisk

        private void tsmiTiskRadkuEtiketa_Click(object sender, EventArgs e)
        {
            try
            {

                if (SelectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if (SelectedRows.Count == 0)
                {
                    MessageBox.Show("Není vybrán záznam pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }


                if (SelectedRows.Count > 1)
                {

                    MessageBox.Show("Je vybráno vícero záznamů pro tisk!", this.Text, MessageBoxButtons.OK);
                    return;
                }
                
                
                Tisk(true);
                
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Tisk(bool MnozstvuAutoJedna)
        {
            try
            {
                string PrinterName = string.Empty;

                if (string.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Rozbory[0].FormSkladLokaceStavList_Tiskarna))
                {
                    PrintDialog printDialog1 = new PrintDialog();
                    printDialog1.UseEXDialog = true;

                    if (printDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.Cancel)
                        return;

                    PrinterName = printDialog1.PrinterSettings.PrinterName;
                }
                else
                {
                    PrinterName = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Sklady_Rozbory[0].FormSkladLokaceStavList_Tiskarna.Trim();
                }


                string pocetStr = string.Empty;
                int pocetInt = 1;

                if (!MnozstvuAutoJedna)
                {
                    while (true)
                    {
                        DialogResult drPocet = Konzola.Forms.InputBox.Show("Etiketa tisk", "Počet etiket k vytištění", pocetInt.ToString(), Forms.InputBox.TypeOfCode.NumericInt, true, 1, 99, false, out pocetStr);
                        if (drPocet == System.Windows.Forms.DialogResult.Cancel)
                            return;

                        try
                        {
                            pocetInt = int.Parse(pocetStr);
                        }
                        catch (Exception exPocet)
                        {
                            MessageBox.Show(exPocet.Message);
                            continue;
                        }

                        break;                    
                    }
                }

                string strPath = System.IO.Path.Combine(MySystem.MyPath.PrintDirectory, "Etiketa_LokMech_Stav.zpl");


                Fask.Interfaces.DataSets.DSValues dsVal = new Fask.Interfaces.DataSets.DSValues();





                //MaR tisk logovani

                foreach (DataColumn item in SelectedRow.Table.Columns)
                {
                    dsVal.Values.AddValuesRow(
                        item.ColumnName.ToUpper(),
                        SelectedRow[item.ColumnName].ToString()
                        );
                }



                if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].Log_Tisky_hl)
                {
                    foreach (DataColumn item in SelectedRow.Table.Columns)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------Start rozbory lok.mech./stav skladu TISK ???-------------------------------------");

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

                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.PrintInfo, "---------------------------------------END rozbory lok.mech./stav skladu TISK ???-------------------------------------");
                    }

                }




                if ((providerTISK != null) && (providerTISK is Fask.Interfaces.Tisky.ITisky2_MetodaEtiketa))
                {
                   ((Fask.Interfaces.Tisky.ITisky2_MetodaEtiketa)providerTISK).TiskMetodaEtiketa(
                       Konzola.Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].TerminalID,
                       ref strPath,
                       ref dsVal,
                       1);
                }
                

                System.IO.StreamReader sr = new System.IO.StreamReader(strPath);
                string strData = sr.ReadToEnd();
                sr.Close();

                StringBuilder sbData = new StringBuilder();
                sbData.Append(strData);


                sbData = ReplaceTemplateKeys(dsVal.Values , sbData);

                //foreach (Fask.Interfaces.DataSets.DSValues.ValuesRow item in dsVal.Values)
                //{
                //    string Key = "$" + item.Key.Trim() + "$";
                //    string value = item.IsValueNull() ? string.Empty : item.Value.Trim();

                //    sbData.Replace(Key, value);
                //}

                //sbData.Replace("$VNDITNUM$", SelectedRow.IsVNDITNUMNull() ? string.Empty : SelectedRow.VNDITNUM);
                //sbData.Replace("$ITEMNMBR$", SelectedRow.ITEMNMBR);
                //sbData.Replace("$SERLTNUM$", SelectedRow.SERLTNUM);
                //sbData.Replace("$ITEMDESC$", SelectedRow.IsITEMDESCNull() ? string.Empty : SelectedRow.ITEMDESC);
                //sbData.Replace("$LOCNCODEDEST$", SelectedRow.IsLOCNCODENull() ? string.Empty : SelectedRow.LOCNCODE);


                sbData = ReplaceImage(sbData);
;
                if(sbData == null)
                    throw new Exception("Tisk etikety IBarCodeGraphics chyba.");

                sbData.Replace("$PocetVytisku$", pocetInt.ToString());

                string debugprintlabel = string.Empty;

                if (Fask.Logging.ExceptionHandler2.EnablePrintLogging)
                {

                    byte[] destinbytes = Encoding.GetEncoding(1250).GetBytes(sbData.ToString()); // Kodovani je špatně, musi to byt v konfiguraci
                    debugprintlabel = "PL_Label_" + Guid.NewGuid().ToString() + ".zpl";

                    FileStream sw = new FileStream(System.IO.Path.Combine(MySystem.MyPath.PrintLogDirectory, debugprintlabel), FileMode.Create);
                    sw.Write(destinbytes, 0, destinbytes.Length);
                    sw.Flush();
                    sw.Close();
                }


                if (!Konzola._Support_.RawPrinterHelper.SendStringToPrinter(PrinterName, sbData.ToString()))
                {
                throw new Exception("Tisk etikety Etiketa_LokMech_Stav  se nezdařil");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        protected StringBuilder ReplaceImage(StringBuilder sb)
        {
            try
            {

                if ((providerTiskKod != null) && (providerTiskKod is Fask.BarCodeGraphics.IBarCodeGraphics))
                    return ((Fask.BarCodeGraphics.IBarCodeGraphics)providerTiskKod).AddGraphics(sb);
                else
                    return sb;
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return null;
            }
        }

        /// <summary>
        /// Prepise nalezene klice pomoci regularniho vyrazu => \$(\w+)((,)(\d+))?\$
        /// $[id](,[delka])?$
        /// [id] = identifikator
        /// [delka] = maximalni delka retezce (nemusi byt definovano, pak vraci cely retezec)
        /// </summary>
        /// <param name="data">Data</param>
        /// <param name="template">Template</param>
        /// <returns>Novy objekt s nahrazenymi parametry</returns>
        private StringBuilder ReplaceTemplateKeys(Fask.Interfaces.DataSets.DSValues.ValuesDataTable data, StringBuilder template)
        {
            StringBuilder sbNew = new StringBuilder(template.ToString());

            // RegEx 
            // => \$(\w+|.+,\d+)\$
            // => \$\w+(,\d+)?\$
            // => \$(\w+)((,)(\d+))?\$ 
            //  Group[0] = cely match
            //  Group[1] = identifikator (\w+)
            //  Group[2] = postfix ((,)(\d+))?
            //  Group[3] = carka (,)
            //  Group[4] = delka (\d+)
            //  Group[5] = postfix ((,)(\d+))?
            //  Group[6] = carka (,)
            //  Group[7] = delka (\d+)

            // puvodni - Obsahuje odpovidajici matche
            //System.Text.RegularExpressions.MatchCollection matches =
            //    System.Text.RegularExpressions.Regex.Matches(sbNew.ToString(), @"\$(\w+)((,)(\d+))?((,)(\d+))?\$");
            // puvodni - Obsahuje odpovídající matche
            System.Text.RegularExpressions.MatchCollection matches =
                System.Text.RegularExpressions.Regex.Matches(sbNew.ToString(), @"\$([\w ]+)((,)(\d+))?((,)(\d+))?\$");

            foreach (System.Text.RegularExpressions.Match match in matches)
            {
                string tresult = string.Empty;
                string key = match.Groups[1].Value;
                if (!data.Any( x=> x.Key.Trim() == key.Trim()))
                { //klic v datech nenalezen, nahradim do sablony prazdnym retezcem ...
                    tresult = string.Empty;
                }
                else
                { //klic nalezen, tak se ho pokusim naformatovat ...

                    var xxx = data.Where(x => x.Key == key);
                    if(xxx.Count() == 1)
                    {
                        tresult = xxx.First().Value;
                    }

                    int? p1 = null;
                    try { p1 = int.Parse(match.Groups[4].Value); }
                    catch { }
                    int? p2 = null;
                    try { p2 = int.Parse(match.Groups[7].Value); }
                    catch { }
                    if (p2.HasValue)
                    {
                        if (tresult.Length < p1.Value) //index mimo rozsah 
                            tresult = string.Empty;
                        else if (p2.Value <= 0)
                        {
                            tresult = tresult.Substring(
                                p1.Value,
                                // test na preteceni maximalni delky
                                tresult.Length - p1.Value
                                );
                        }
                        else // index v rozsahu
                            tresult = tresult.Substring(
                                p1.Value,
                                // test na preteceni maximalni delky
                                tresult.Length < (p1.Value + p2.Value) ? tresult.Length - p1.Value : p2.Value
                                );
                    }
                    else if (p1.HasValue && p1.Value > 0)
                    {
                        tresult = tresult.Substring(0, tresult.Length < p1.Value ? tresult.Length : p1.Value);
                    }
                    else
                    { // ??? neni nutny ... $<key>$
                    }

                }

                // finalni nahrazeni matche vysledkem formatovani ...
                sbNew.Replace(match.Value, tresult);
            }

            return sbNew;
        }


        #endregion

        #region EasternEgg pro zobrazeni kodu
        private void label5_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (SelectedRow != null)
                {

                    if (!string.IsNullOrEmpty(SelectedRow.VNDITNUM))
                    {
                        ZXing.BarcodeWriter bw = new ZXing.BarcodeWriter();
                        bw.Options.Width = 400;
                        bw.Options.Height = 400;
                        bw.Format = ZXing.BarcodeFormat.QR_CODE;
                        Bitmap bmp = bw.Write(SelectedRow.VNDITNUM.Trim());

                        Forms.FormKod.Show(bmp, "Čár. kód položky");
                    }
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        } 
        #endregion
    }
}
