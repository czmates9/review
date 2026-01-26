using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fask.Logging;
using System.Reflection;

namespace Konzola.Servis
{
    public partial class FormZdrojeStavyEdit : Form
    {
        private Fask.Interfaces.IMES providerServis = null;
        /// <summary>
        /// vytvoreny/upraveny zaznam.
        /// </summary>
        public Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojStavRow returnrow { get; set; }
        /// <summary>
        /// ZdrojStav, který se má upravit.
        /// </summary>
        public Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojStavRow zdrojStavRow { get; set; }

        private Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojRow selectedZdroj { get; set; }
        private Fask.Interfaces.DataSets.Servis.CZMST_Servis_CinnostRow selectedCinnost { get; set; }
        private Fask.Interfaces.DataSets.Servis.CZMST_Servis_StavRow selectedStav { get; set; }

        public FormZdrojeStavyEdit()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormUzivateleEdit_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                FormUzivateleEdit_Resize(null, null);

                // inicializace providera
                InitProvider();

                if (providerServis == null)
                    throw new Exception("Provider není inicializován");

                LoadData();
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
        }

        private void LoadData()
        {
            tbIDCinnost.Text = "Nenastaveno";
            // je úprava záznamu, dojde k načtení dat
            if (zdrojStavRow != null)
            {
                // nacteni zdroje
                selectedZdroj = ((Fask.Interfaces.Servis.IServis)providerServis).GetZdrojByID(zdrojStavRow.IDZdroj);

                // nacteni stavu
                selectedStav = ((Fask.Interfaces.Servis.IServis)providerServis).GetStavByID(zdrojStavRow.IDStav);

                // nacteni cinnosti
                if (!zdrojStavRow.IsIDCinnostNull())
                    selectedCinnost = ((Fask.Interfaces.Servis.IServis)providerServis).GetCinnostByID(zdrojStavRow.IDCinnost);
                else
                {
                    Fask.Interfaces.DataSets.Servis dsServis = new Fask.Interfaces.DataSets.Servis();
                    Fask.Interfaces.DataSets.Servis.CZMST_Servis_CinnostRow emptyrow = dsServis.CZMST_Servis_Cinnost.NewCZMST_Servis_CinnostRow();
                    emptyrow.SetIDNull();
                    emptyrow.Oznaceni = "Přechod do dalšího stavu";
                    emptyrow.SetBarcodeNull();
                    emptyrow.TYPE = string.Empty;
                    emptyrow.SetTYPEVALUENull();
                    emptyrow.Mandatory = 0;
                    emptyrow.SetRequiredLengthNull();
                    //dsServis.CZMST_Servis_Cinnost.AddCZMST_Servis_CinnostRow(emptyrow);
                    selectedCinnost = emptyrow;
                }

                tbIDZdroj.Text = selectedZdroj.Oznaceni.Trim() + " (" + selectedZdroj.ID.Trim() + ")";
                tbIDStav.Text = selectedStav.Oznaceni.Trim() + " (" + selectedStav.ID.Trim() + ")";
                tbIDCinnost.Text = selectedCinnost.Oznaceni.Trim() + (selectedCinnost.IsIDNull() ? string.Empty : (" (" + selectedCinnost.ID.Trim() + ")"));
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.PerformOK();
        }

        private void buttonStorno_Click(object sender, EventArgs e)
        {
            this.PerformCancel();
        }

        private void FormUzivateleEdit_KeyDown(object sender, KeyEventArgs e)
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
            DialogResult = DialogResult.Cancel;
        }

        private void PerformOK()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                if (!ValidateData())
                    return;

                // porovnani s existujicim zaznamem
                // TODO: do transakce s editaci/pridanim??
                if (zdrojStavRow != null)
                {
                    Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojStavRow stav_test = ((Fask.Interfaces.Servis.IServis)providerServis).GetZdrojStavByZdrojID(zdrojStavRow.IDZdroj);

                    if (stav_test == null)
                    {
                        MessageBox.Show("Záznam byl od posledního načtení odstraněn.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // kvuli refreshi
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        return;
                    }
                    else
                    {
                        Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojStav_COMPAREDataTable dtCompare = new Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojStav_COMPAREDataTable();
                        dtCompare.ImportRow(zdrojStavRow);
                        dtCompare.ImportRow(stav_test);

                        IEqualityComparer<Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojStav_COMPARERow> comparer = DataRowComparer.Default;
                        bool isMatch = comparer.Equals(dtCompare[0], dtCompare[1]);
                        if (!isMatch)
                        {
                            if (DialogResult.Yes != MessageBox.Show("Záznam byl od posledního načtení změněn. Přejete si ho přesto upravit?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                            {
                                // kvuli refreshi
                                this.DialogResult = DialogResult.OK;
                                return;
                            }
                        }
                    }
                }

                Fask.Interfaces.DataSets.Servis dsServis2 = new Fask.Interfaces.DataSets.Servis();
                Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojStavRow newStavRow = dsServis2.CZMST_Servis_ZdrojStav.NewCZMST_Servis_ZdrojStavRow();


                newStavRow.IDZdroj = selectedZdroj.ID;
                newStavRow.IDStav = selectedStav.ID;
              
                // pokud se edituje zaznam, id cinnosti se zachova
                if (selectedCinnost != null && !selectedCinnost.IsIDNull())
                {
                    // ... pri editaci prevzit cislo davky, id okruhu, ...
                    newStavRow.IDCinnost = selectedCinnost.ID;
                }
                else
                    newStavRow.SetIDCinnostNull();

                newStavRow.Modified = DateTime.Now;
                newStavRow.IDTerminal = Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].TerminalID;
                int iduser;                
                try
                {
                    // v konzole muze byt jako id uzivatele string ...
                    iduser = int.Parse(FASK.Logins.Uzivatel.Instance.UserID);
                    newStavRow.IDUser = iduser;
                }
                catch
                {
                    newStavRow.IDUser = 99;
                }

                newStavRow.SetCinnostOznaceniNull();
                newStavRow.SetGPS_XNull();
                newStavRow.SetGPS_YNull();
                newStavRow.SetGPS_ZNull();

                //dodelat !!!
                //newStavRow.ID = tbIDZdroj.Text.Trim();
                //newStavRow.Oznaceni = tbOznaceni.Text.Trim();

                //if (string.IsNullOrEmpty(tbIDStav.Text.Trim()))
                //    newStavRow.SetBarcodeNull();
                //else
                //    newStavRow.Barcode = tbIDStav.Text.Trim();

                
                //if (stavrow != null && !stavrow.IsIDCinnostNull())
                //    newStavRow.IDCinnost = stavrow.IDCinnost;
                //else
                //    newStavRow.SetIDCinnostNull();

                // TODO: aktualizovat take zdrojpohyb??
                // je úprava záznamu
                if (zdrojStavRow != null)
                {
                    // doplneni jiz existujicich dat
                    newStavRow.GUID = zdrojStavRow.IsGUIDNull() ? Guid.NewGuid() : zdrojStavRow.GUID;
                    if (zdrojStavRow.IsCinnostValueNull())
                        newStavRow.SetCinnostValueNull();
                    else
                        newStavRow.CinnostValue = zdrojStavRow.CinnostValue;

                    if (zdrojStavRow.IsCinnostTypeNull())
                        newStavRow.SetCinnostTypeNull();
                    else
                        newStavRow.CinnostType = zdrojStavRow.CinnostType;

                    if (zdrojStavRow.IsCinnostTypeNull())
                        newStavRow.SetCinnostTypeNull();
                    else
                        newStavRow.CinnostType = zdrojStavRow.CinnostType;

                    if (zdrojStavRow.IsCountEntriesNull())
                        newStavRow.SetCountEntriesNull();
                    else
                        newStavRow.CountEntries = zdrojStavRow.CountEntries;

                    if (zdrojStavRow.IsODB_IDNull())
                        newStavRow.SetODB_IDNull();
                    else
                        newStavRow.ODB_ID = zdrojStavRow.ODB_ID;

                    if (zdrojStavRow.IsOkruhIDNull())
                        newStavRow.SetOkruhIDNull();
                    else
                        newStavRow.OkruhID = zdrojStavRow.OkruhID;

                    ((Fask.Interfaces.Servis.IServis)providerServis).UpdateZdrojStav(newStavRow);
                }
                else   // nový záznam
                {
                    newStavRow.GUID = Guid.NewGuid();
                    newStavRow.SetCinnostValueNull();
                    newStavRow.SetCinnostTypeNull();
                    newStavRow.SetCountEntriesNull();
                    newStavRow.SetODB_IDNull();
                    newStavRow.SetOkruhIDNull();

                    ((Fask.Interfaces.Servis.IServis)providerServis).InsertZdrojStav(newStavRow);
                }

                returnrow = newStavRow;
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Kontrola vyplněných dat.
        /// </summary>
        /// <returns>True, pokud je vše v pořádku, jinak False</returns>
        private bool ValidateData()
        {
            try
            {
                errorProvider1.Clear();

                if (selectedZdroj == null)
                {
                    tbIDZdroj.Focus();
                    errorProvider1.SetError(tbIDZdroj, "Musíte vybrat zdroj");
                }

                if (selectedStav == null)
                {
                    tbIDZdroj.Focus();
                    errorProvider1.SetError(tbIDStav, "Musíte vybrat stav");
                }

                if (selectedCinnost == null)
                {
                    tbIDZdroj.Focus();
                    errorProvider1.SetError(tbIDCinnost, "Musíte vybrat činnost");
                }

                if (zdrojStavRow == null && selectedZdroj != null)
                {
                    // vytváří se nový záznam, kontrola existence id
                    Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojStavRow stav = ((Fask.Interfaces.Servis.IServis)providerServis).GetZdrojStavByZdrojID(selectedZdroj.ID);
                    if (stav != null)
                    {
                        tbIDZdroj.Focus();
                        //textBoxId.SelectAll();
                        errorProvider1.SetError(tbIDZdroj, "Zadané id zdroje již existuje");
                        //throw new Exception("Zadané id uživatele již existuje");
                    }
                }

                //if (string.IsNullOrEmpty(tbOznaceni.Text.Trim()))
                //{
                //    tbOznaceni.Focus();
                //    errorProvider1.SetError(tbOznaceni, "Musíte zadat označení");
                //    //throw new Exception("Musíte zadat jméno");
                //}
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return IsAllValid();
        }

        private bool IsAllValid()
        {
            //foreach (Control c in errorProvider1.ContainerControl.Controls)
            foreach (Control c in panel2.Controls)
                if (errorProvider1.GetError(c) != "")
                    return false;
            return true;
        }

        private void FormUzivateleEdit_Shown(object sender, EventArgs e)
        {         
        }

        private void FormUzivateleEdit_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
            buttonStorno.Size = nsize;
        }

        private void buttonOK_Click_1(object sender, EventArgs e)
        {
            this.PerformOK();
        }

        private void formZdrojeEdit_Enter(object sender, EventArgs e)
        {
            tbNapoveda.Text = string.Empty;
            string text = string.Empty;

            try
            {
                if (sender is TextBox)
                {
                    TextBox tb = ((TextBox)sender);
                    //string propertyName = tb.Name;
                    if (tbIDZdroj == tb)
                    {
                        text = "Vybraný zdroj.";
                    }
                    else if (tbIDStav == tb)  // oznaceni
                    {
                        text = "Vybraný stav. Při nastavení/změně stavu dojde k automatickému vyplnění výchozí činnosti.";
                    }
                    else if (tbIDCinnost == tb)
                    {
                        text = "Vybraná činnost.";
                    }
                }
                else if (sender is Button)
                {
                    Button btn = ((Button)sender);
                    //string propertyName = tb.Name;
                    if (btnZdrojID == btn)
                    {
                        text = "Vybraný zdroj.";
                    }
                    else if (btnStavID == btn)  // oznaceni
                    {
                        text = "Vybraný stav. Při nastavení/změně stavu dojde k automatickému vyplnění výchozí činnosti.";
                    }
                    else if (btnCinnostID == btn)
                    {
                        text = "Vybraná činnost.";
                    }
                }
            }
            catch { }
            finally
            {
                tbNapoveda.Text = text;
            }
        }

        private void PerformPriraditCinnost()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                
                Fask.Interfaces.DataSets.Servis dsSelectedRows = new Fask.Interfaces.DataSets.Servis();
                using (Servis.FormCinnostiSelect frmcinnosti = new FormCinnostiSelect(false, selectedCinnost))
                {
                    frmcinnosti.Text = "Přiřadit činnost";
                    //frmstavy.rowsSelected = rowStav;
                    if (frmcinnosti.ShowDialog(this) != DialogResult.OK)
                        return;

                    selectedCinnost = frmcinnosti.SelectedRow;

                    tbIDCinnost.Text = selectedCinnost.Oznaceni.Trim() + (selectedCinnost.IsIDNull() ? string.Empty : (" (" + selectedCinnost.ID.Trim() + ")"));
                }                
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnZdrojID_Click(object sender, EventArgs e)
        {
            // aktualizace napovedy ... po kliknuti se nezavola
            formZdrojeEdit_Enter(sender, null);

            PerformPriraditZdroj();
        }

        private void PerformPriraditZdroj()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                Fask.Interfaces.DataSets.Servis dsSelectedRows = new Fask.Interfaces.DataSets.Servis();
                using (Servis.FormZdrojeSelect frmcinnosti = new FormZdrojeSelect(false, selectedZdroj))
                {
                    frmcinnosti.Text = "Přiřadit zdroj";
                    //frmstavy.rowsSelected = rowStav;
                    if (frmcinnosti.ShowDialog(this) != DialogResult.OK)
                        return;

                    selectedZdroj = frmcinnosti.SelectedRow;

                    tbIDZdroj.Text = selectedZdroj.Oznaceni.Trim() + " (" + selectedZdroj.ID.Trim() + ")";
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCinnostID_Click(object sender, EventArgs e)
        {
            // aktualizace napovedy ... po kliknuti se nezavola
            formZdrojeEdit_Enter(sender, null);

            PerformPriraditCinnost();
        }

        private void btnStavID_Click(object sender, EventArgs e)
        {
            // aktualizace napovedy ... po kliknuti se nezavola
            formZdrojeEdit_Enter(sender, null);

            PerformPriraditStav();
        }

        private void PerformPriraditStav()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;


                using (Servis.FormStavySelect frmcinnosti = new FormStavySelect(false, selectedStav))
                {
                    frmcinnosti.Text = "Přiřadit stav";
                    //frmstavy.rowsSelected = rowStav;
                    if (frmcinnosti.ShowDialog(this) != DialogResult.OK)
                        return;

                    selectedStav = frmcinnosti.SelectedRow;

                    tbIDStav.Text = selectedStav.Oznaceni.Trim() + " (" + selectedStav.ID.Trim() + ")";
                }

                // null poresit ...
                if (selectedStav.IsIDCinnostNull())
                {
                    Fask.Interfaces.DataSets.Servis dsServis = new Fask.Interfaces.DataSets.Servis();
                    Fask.Interfaces.DataSets.Servis.CZMST_Servis_CinnostRow emptyrow = dsServis.CZMST_Servis_Cinnost.NewCZMST_Servis_CinnostRow();
                    emptyrow.SetIDNull();
                    emptyrow.Oznaceni = "Přechod do dalšího stavu";
                    emptyrow.SetBarcodeNull();
                    emptyrow.TYPE = string.Empty;
                    emptyrow.SetTYPEVALUENull();
                    emptyrow.Mandatory = 0;
                    emptyrow.SetRequiredLengthNull();
                    //dsServis.CZMST_Servis_Cinnost.AddCZMST_Servis_CinnostRow(emptyrow);
                    selectedCinnost = emptyrow;
                }
                else
                    selectedCinnost = ((Fask.Interfaces.Servis.IServis)providerServis).GetCinnostByID(selectedStav.IDCinnost);

                tbIDCinnost.Text = selectedCinnost.Oznaceni.Trim() + (selectedCinnost.IsIDNull() ? string.Empty : (" (" + selectedCinnost.ID.Trim() + ")"));
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
