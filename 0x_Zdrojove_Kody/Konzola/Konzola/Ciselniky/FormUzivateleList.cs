using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fask.Logging;
using Vyroba_Konzola.Extensions;
using Vyroba_Konzola;
using System.Reflection;
using System.IO;

namespace Vyroba_Konzola.Ciselniky
{
    /// <summary>
    /// Prace s uzivateli pomoci Interface, ...
    /// Tento form primarne pouzivat a rozsirovat
    /// // TODO: pridani, editace a mazani zaznamu
    /// </summary>
    public partial class FormUzivateleList : Form
    {
        private Fask.Console.Interfaces.IVyrobaKonzola providerUzivatel = null;
        private Fask.Console.Interfaces.Classes.ZOBRAZENI_TYP _zobrazeni = Fask.Console.Interfaces.Classes.ZOBRAZENI_TYP.UNKNOWN;
        public Fask.Console.Interfaces.Classes.ZOBRAZENI_TYP Zobrazeni
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
                        case Fask.Console.Interfaces.Classes.ZOBRAZENI_TYP.LIST:
                            panelButtonsZobrazeniVyber.Hide();
                            panelButtonsZobrazeniList.Show();
                            menuStrip2.Items.Remove(tsmiMenuVyber);
                            tsFiltry.Visible = true;
                            tsFiltry.Enabled = true;
                            break;
                        case Fask.Console.Interfaces.Classes.ZOBRAZENI_TYP.VYBER:
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
        private List<Fask.Console.Interfaces.Classes.UzivateleListFiltr> filtry = new List<Fask.Console.Interfaces.Classes.UzivateleListFiltr>();

        /// <summary>
        /// Vybrany filtr.
        /// </summary>
        private Fask.Console.Interfaces.Classes.UzivateleListFiltr rowFiltr
        {
            get
            {
                try
                {
                    return tscbFiltry.SelectedItem as Fask.Console.Interfaces.Classes.UzivateleListFiltr;
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
        private Fask.Console.Interfaces.DataSets.Uzivatele.CZMSTPWDRow selectRow { get; set; }

        /// <summary>
        /// Vybrany radek.
        /// </summary>
        public Fask.Console.Interfaces.DataSets.Uzivatele.CZMSTPWDRow SelectedRow
        {
            get
            {
                try
                {
                    return ((DataRowView)(dataGridView1.BindingContext[bsUzivatel].Current)).Row as Fask.Console.Interfaces.DataSets.Uzivatele.CZMSTPWDRow;
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
                    return dataGridView1.SelectedRows;
                }
                catch
                {
                    return null;
                }
            }
        }

        public FormUzivateleList(bool allowMultiSelect, Fask.Console.Interfaces.Classes.ZOBRAZENI_TYP typZobrazeni)
        {
            InitializeComponent();
            this.dataGridView1.MultiSelect = allowMultiSelect;
            this.Zobrazeni = typZobrazeni;
            if(Zobrazeni == Fask.Console.Interfaces.Classes.ZOBRAZENI_TYP.LIST)
                this.WindowState = FormWindowState.Maximized;
        }

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="allowMultiSelect">Povoleni oznaceni vice zaznamu.</param>
        /// <param name="selectRow">Radek, ktery se oznaci.</param>
        public FormUzivateleList(bool allowMultiSelect, Fask.Console.Interfaces.DataSets.Uzivatele.CZMSTPWDRow selectRow, Fask.Console.Interfaces.Classes.ZOBRAZENI_TYP typZobrazeni)
            : this(allowMultiSelect, typZobrazeni)
        {
            this.selectRow = selectRow;
        }



        private void FormUzivateleList_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                //this.WindowState = FormWindowState.Maximized;
                this.dataGridView1.LoadConfiguration(this.GetType().ToString());

                // načtení konfigurace vytvořených filtrů
                this.filtry = Extensions.ListExt.ReadFromXML<Fask.Console.Interfaces.Classes.UzivateleListFiltr>(this.GetType().ToString() + ".filtr");
                tscbFiltry.ComboBox.DataSource = this.filtry;
                tscbFiltry.SelectedItem = null;
                tscbFiltry.ComboBox.DropDownWitdhAutosize();
                
                // inicializace providera
                InitProvider();

                if (providerUzivatel == null)
                    throw new Exception("Provider 'Uživatelé' není inicializován");

                // 13.7.2016 PeV: jiz se nepouziva, predelano na backgroundworker
                //System.Threading.Thread loadThread = new System.Threading.Thread(LoadDataAsync);
                //loadThread.IsBackground = true;
                //loadThread.Start();
                PerformVyhledat();
            }
            catch (Exception ex)
            {
                Log.Write(ex);
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
                if (String.IsNullOrEmpty(Settings.ProviderKonzola))
                    return;
                
                if (providerUzivatel == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Vyroba_Konzola.MySystem.MyPath.CurrentDirectory, Settings.ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Console.Interfaces.Ciselniky.IUzivatele2).IsAssignableFrom(t))
                            {
                                providerUzivatel = (Fask.Console.Interfaces.Ciselniky.IUzivatele2)providerAssemlby.CreateInstance(t.FullName);
                                if (providerUzivatel != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                // nastaveni connection stringu
                //if (providerUzivatel != null)
                //    ((Fask.Console.Interfaces.Ciselniky.IUzivatele)providerUzivatel).ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;

                if ((providerUzivatel != null) && (providerUzivatel is Fask.Console.Interfaces.Parametry.IParametry2_ConnectionString))
                    ((Fask.Console.Interfaces.Parametry.IParametry2_ConnectionString)providerUzivatel).ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
              

            }
            catch (Exception ex)
            {
                Fask.Logging.Log.Write(ex, "Load Provider.Uzivatele");
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormUzivateleList_KeyDown(object sender, KeyEventArgs e)
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

        private void PerformCancel()
        {
            try
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            catch (Exception ex)
            {
                Log.Write(ex);
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
                if (Zobrazeni != Fask.Console.Interfaces.Classes.ZOBRAZENI_TYP.VYBER)
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
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //finally
            //{
            //}            
        }

        private void FormUzivateleList_Resize(object sender, EventArgs e)
        {

        }

        private void konecToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void obnovitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PerformVyhledat();
        }

        private void FormUzivateleList_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.dataGridView1.SaveConfiguration(this.GetType().ToString());

                // ulozeni vsech filtru
                this.filtry.WriteXML(this.GetType().ToString() + ".filtr");

                // cekani na dobehnuti vlakna
                try
                {
                    if (bwLoadUzivatele.IsBusy)
                    {
                        bwLoadUzivatele.CancelAsync();
                        while (bwLoadUzivatele.IsBusy)
                        {
                            Application.DoEvents();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Write(ex);
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                        selectedSortID = SelectedRow.ID.ToString();
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
                int pos = this.bsUzivatel.Find(dsUzivatel.CZMSTPWD.IDColumn.ColumnName, selectedSortID);
                this.bsUzivatel.Position = pos;
            }
            catch { }
        }

        private void bwLoadZbozi_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Fask.Console.Interfaces.Classes.UzivateleListFiltr filtr = (Fask.Console.Interfaces.Classes.UzivateleListFiltr)e.Argument;
                Fask.Console.Interfaces.DataSets.Uzivatele ds = new Fask.Console.Interfaces.DataSets.Uzivatele();

                if (bwLoadUzivatele.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                // nacteni dat v oddelenem vlakne
                //ds = ((Fask.Console.Interfaces.Ciselniky.IUzivatele)providerUzivatel).GetFiltrovaneUzivatele(filtr);

                if ((providerUzivatel != null) && (providerUzivatel is Fask.Console.Interfaces.Ciselniky.IUzivatele2_GetFiltrovaneUzivatele))
                    ds = ((Fask.Console.Interfaces.Ciselniky.IUzivatele2_GetFiltrovaneUzivatele)providerUzivatel).GetFiltrovaneUzivatele(filtr);
                else
                    throw new NotImplementedException("Provider neimplementuje IUzivatele2_GetFiltrovaneUzivatele.");
                
                if (bwLoadUzivatele.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                e.Result = ds;
            }
            catch (Exception ex)
            {
                Log.Write(ex);
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
                    dsUzivatel = new Fask.Console.Interfaces.DataSets.Uzivatele();
                    bsUzivatel.DataSource = dsUzivatel;
                    Log.Write(e.Error);
                    MessageBox.Show(e.Error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (e.Cancelled)
                {
                    // handle cancellation
                    // TODO: maji se vymazat data z datasetu?? asi ano ...
                    //if (dsSkladPohyb == null)
                    dsUzivatel = new Fask.Console.Interfaces.DataSets.Uzivatele();
                    bsUzivatel.DataSource = dsUzivatel;
                }
                else
                {
                    // uspesne dokonceno ...
                    // use it on the UI thread
                    dsUzivatel = (Fask.Console.Interfaces.DataSets.Uzivatele)e.Result;
                    if (dsUzivatel == null)
                        dsUzivatel= new Fask.Console.Interfaces.DataSets.Uzivatele();

                    bsUzivatel.DataSource = dsUzivatel;
                }

            }
            catch (Exception ex)
            {
                Log.Write(ex);
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

        private void buttonVyhledat_Click(object sender, EventArgs e)
        {
            PerformVyhledat();
        }

        /// <summary>
        /// Vyhledani podle zadaneho filtru.
        /// </summary>
        private void PerformVyhledat()
        {
            try
            {
                DataTable dtchanged = this.dsUzivatel.CZMSTPWD.GetChanges();
                if (dtchanged != null && dtchanged.Rows.Count > 0) //obsahuje zmeny ... 
                {
                    DialogResult dr = MessageBox.Show("Obsahuje změny, pokračovat?", "data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == System.Windows.Forms.DialogResult.No)
                        return;
                }

                if (bwLoadUzivatele.IsBusy)
                {
                    bwLoadUzivatele.CancelAsync();
                    while (bwLoadUzivatele.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }

                ProgressIndicatorStart();

                Fask.Console.Interfaces.Classes.UzivateleListFiltr filtr = new Fask.Console.Interfaces.Classes.UzivateleListFiltr();
                if (!CreateFilter(ref filtr))
                    return;

                int FirstDisplayedScrollingRowIndex = this.dataGridView1.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

                bwLoadUzivatele.RunWorkerAsync(filtr);

                if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dataGridView1.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dataGridView1.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index
            }
            catch (Exception ex)
            {
                ProgressIndicatorStop();
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Vytvoreni filtru, ktery se pouzije.
        /// </summary>
        /// <param name="filtr"></param>
        /// <returns></returns>
        private bool CreateFilter(ref Fask.Console.Interfaces.Classes.UzivateleListFiltr filtr)
        {
            if (filtr == null)
                filtr = new Fask.Console.Interfaces.Classes.UzivateleListFiltr();

            filtr.UserID = cbUserID.Text.Trim();
            filtr.UserLogin = cbUserLogin.Text.Trim();
            
            return true;
        }

        private void FormZboziSelect_Shown(object sender, EventArgs e)
        {
            try
            {
                this.progressIndicator1.Size = new Size(Settings.ProgressIndicatorSize, Settings.ProgressIndicatorSize);
                this.progressIndicator1.Location = new Point(this.dataGridView1.Location.X + (this.dataGridView1.Width / 2) - (progressIndicator1.Size.Width / 2), this.dataGridView1.Location.Y + (this.dataGridView1.Height / 2) - (progressIndicator1.Size.Height / 2));
            }
            catch { }
        }



        private void tsbNastavit_Click(object sender, EventArgs e)
        {
            PerformNastavitFiltr(rowFiltr);
        }

        /// <summary>
        /// Nastavi zvoleny filtr do comboboxu, ...
        /// </summary>
        /// <param name="filtr"></param>
        private void PerformNastavitFiltr(Fask.Console.Interfaces.Classes.UzivateleListFiltr filtr)
        {
            try
            {
                if (filtr == null)
                    return;

                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                PerformVycistitFiltr();

                cbUserID.Text = filtr.UserID;         // itemnmbr
                cbUserLogin.Text = filtr.UserLogin;      // itemdesc
            }
            catch (Exception ex)
            {
                Log.Write(ex);
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
                cbUserID.SelectedItem =
                cbUserLogin.SelectedItem = null;

                cbUserID.Text =
                cbUserLogin.Text = string.Empty;
            }
            catch (Exception ex)
            {
                Log.Write(ex);
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

                Fask.Console.Interfaces.Classes.UzivateleListFiltr filtr = rowFiltr;

                if (!CreateFilter(ref filtr))
                    return;
            }
            catch (Exception ex)
            {
                Log.Write(ex);
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

                Fask.Console.Interfaces.Classes.UzivateleListFiltr filtr = new Fask.Console.Interfaces.Classes.UzivateleListFiltr();
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
                Log.Write(ex);
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
                Log.Write(ex);
                MessageBox.Show("Nepodařilo se odstranit filtr.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonNovy_Click_1(object sender, EventArgs e)
        {
            PerformCreateRecord();
        }

        private void PerformCreateRecord()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                using (Ciselniky.FormUzivateleEdit frmuziv = new FormUzivateleEdit())
                {
                    frmuziv.Text = "Nový uživatel";
                    if (frmuziv.ShowDialog(this) != DialogResult.OK)
                        return;

                    // pridat ...
                    this.dsUzivatel.CZMSTPWD.ImportRow(frmuziv.returnrow);
                    //this.dsUzivatel.CZMSTPWD.AddCZMSTPWDRow(frmuziv.returnrow); // patri do jine tabulky
                    this.dsUzivatel.CZMSTPWD.AcceptChanges();
                    try
                    {
                        this.bsUzivatel.Position = dsUzivatel.CZMSTPWD.Count - 1;
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonUpravit_Click(object sender, EventArgs e)
        {
            PerformEditRecord();
        }

        private void PerformEditRecord()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (SelectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro úpravu", this.Text, MessageBoxButtons.OK);
                    return;
                }

                using (Ciselniky.FormUzivateleEdit frmuziv = new FormUzivateleEdit())
                {
                    frmuziv.rowUzivatelEdit = SelectedRow;
                    frmuziv.Text = "Úprava uživatele";
                    if (frmuziv.ShowDialog(this) != DialogResult.OK)
                        return;

                    // aktualizace
                    this.SelectedRow.ID = frmuziv.returnrow.ID;
                    this.SelectedRow.LOGIN = frmuziv.returnrow.LOGIN;
                    this.SelectedRow.PASSWD = frmuziv.returnrow.PASSWD;
                    if (frmuziv.returnrow.IsADMNull())
                        this.SelectedRow.SetADMNull();
                    else this.SelectedRow.ADM = frmuziv.returnrow.ADM;
                    this.SelectedRow.FIRSTNAME= frmuziv.returnrow.FIRSTNAME;
                    this.SelectedRow.SECONDNAME= frmuziv.returnrow.SECONDNAME;
                    if (frmuziv.returnrow.IsHASHNull())
                        this.SelectedRow.SetHASHNull();
                    else this.SelectedRow.HASH = frmuziv.returnrow.HASH;
                    if (frmuziv.returnrow.IsEANNull())
                        this.SelectedRow.SetEANNull();
                    else this.SelectedRow.EAN = frmuziv.returnrow.EAN;
                    this.SelectedRow.CODE = frmuziv.returnrow.CODE;

                    this.dsUzivatel.CZMSTPWD.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonOdstranit_Click(object sender, EventArgs e)
        {
            PerformDeleteRecord();
        }

        private void PerformDeleteRecord()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (SelectedRows.Count > 1)
                {
                    MessageBox.Show("Musí být vybrán pouze jeden záznam", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if (SelectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam pro odstranění", this.Text, MessageBoxButtons.OK);
                    return;
                }

                if (MessageBox.Show("Opravdu chcete odstranit uživatele '" + SelectedRow.LOGIN.Trim() + "'?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

                //bool result = ((Fask.Console.Interfaces.Ciselniky.IUzivatele)providerUzivatel).DeleteUzivatel(SelectedRow.ID.ToString());

                bool result = false;

                if ((providerUzivatel != null) && (providerUzivatel is Fask.Console.Interfaces.Ciselniky.IUzivatele2_DeleteUzivatel))
                    result = ((Fask.Console.Interfaces.Ciselniky.IUzivatele2_DeleteUzivatel)providerUzivatel).DeleteUzivatel(SelectedRow.ID.ToString());
                else
                    throw new NotImplementedException("Provider neimplementuje IUzivatele2_DeleteUzivatel.");


                this.dsUzivatel.CZMSTPWD.RemoveCZMSTPWDRow(SelectedRow);
                this.dsUzivatel.AcceptChanges();
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripMenuItemTiskEtiketa_Click(object sender, EventArgs e)
        {
            try
            {
                PrintDialog printDialog1 = new PrintDialog();
                printDialog1.UseEXDialog = true;

                if (printDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.Cancel)
                    return;

                // 1) pripravit 
                Fask.Console.Interfaces.DataSets.Uzivatele dsUzivateleSelected = new Fask.Console.Interfaces.DataSets.Uzivatele();
                foreach (DataGridViewRow row in this.dataGridView1.SelectedRows)
                {
                    DataRowView drv = this.bsUzivatel[row.Index] as DataRowView;
                    var _uzivatelRow = drv.Row as Fask.Console.Interfaces.DataSets.Uzivatele.CZMSTPWDRow;
                    dsUzivateleSelected.CZMSTPWD.ImportRow(_uzivatelRow);
                }

                string pocetStr = string.Empty;
                int pocetInt = 1;
                while (true)
                {
                    DialogResult drPocet = Vyroba_Konzola.Forms.InputBox.Show("Etiketa tisk", "Počet etiket Uživatele k vytištění", pocetInt.ToString(), Forms.InputBox.TypeOfCode.NumericInt, true, 1, 99, false, out pocetStr);
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

                    break; // vse ok ... 
                }

                // Test, zda je to Leitz ...
                if (_Printers_.PrinterLeitz.IsLeitz(printDialog1.PrinterSettings.PrinterName))
                { // je to leitz ... 
                    // => tisknout pomoci SDK ...
                    _Printers_.PrinterLeitz.Print_Uzivatel(printDialog1.PrinterSettings.PrinterName, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Servis", "Print", "Templates", "Etiketa_Uzivatel.LeitzLbl"), pocetInt, dsUzivateleSelected);
                    return;
                }
                else
                { // je to neco jineho ...

                    string strPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Servis", "Print", "Templates", "Etiketa_Uzivatel.txt");
                    System.IO.StreamReader sr = new System.IO.StreamReader(strPath);
                    string strData = sr.ReadToEnd();
                    sr.Close();

                    var columns = dsUzivateleSelected.CZMSTPWD.Columns;
                    foreach (var row in dsUzivateleSelected.CZMSTPWD)
                    {
                        StringBuilder sbData = new StringBuilder();
                        sbData.Append(strData);

                        foreach (DataColumn col in columns)
                        {
                            sbData.Replace(String.Format("${0}$", col.ColumnName), row[col].ToString());
                        }

                        sbData.Replace("$Pocet$", pocetInt.ToString());
                        if (!Vyroba_Konzola._Support_.RawPrinterHelper.SendStringToPrinter(printDialog1.PrinterSettings.PrinterName, sbData.ToString()))
                        {
                            throw new Exception("Tisk etikety '" + row.LOGIN + "' se nezdařil");
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }



        #region Exporty



        private void exportDoCSVVseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                this.dataGridView1.ExportToCSV(Fask.Console.Interfaces.Classes.EXPORT_DAT.VSE);
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exportDoCSVOznaceneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                this.dataGridView1.ExportToCSV(Fask.Console.Interfaces.Classes.EXPORT_DAT.OZNACENE);
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exportDoExcelVseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                this.dataGridView1.ExportToExcel(Fask.Console.Interfaces.Classes.EXPORT_DAT.VSE);
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exportDoExceOznaceneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                dataGridView1.ExportToExcel(Fask.Console.Interfaces.Classes.EXPORT_DAT.OZNACENE);
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void exportDoXMLVseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                dataGridView1.ExportToXML(Fask.Console.Interfaces.Classes.EXPORT_DAT.VSE);
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exportDoXMLOznaceneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                dataGridView1.ExportToXML(Fask.Console.Interfaces.Classes.EXPORT_DAT.OZNACENE);
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #endregion

    }
}
