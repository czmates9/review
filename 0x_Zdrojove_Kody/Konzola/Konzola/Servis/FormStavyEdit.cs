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
    public partial class FormStavyEdit : Form
    {
        private Fask.Interfaces.IMES providerServis = null;
        /// <summary>
        /// vytvoreny/upraveny zaznam.
        /// </summary>
        public Fask.Interfaces.DataSets.Servis.CZMST_Servis_StavRow returnrow { get; set; }
        /// <summary>
        /// Stav, který se má upravit.
        /// </summary>
        public Fask.Interfaces.DataSets.Servis.CZMST_Servis_StavRow stavrow { get; set; }

        private Fask.Interfaces.DataSets.Servis.CZMST_Servis_CinnostRow selectedCinnost { get; set; }

        public FormStavyEdit()
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
            if (stavrow != null)
            {
                tbID.Enabled = false;
                tbID.Text = stavrow.ID.Trim();
                tbOznaceni.Text = stavrow.Oznaceni.Trim();
                tbBarcode.Text = stavrow.IsBarcodeNull() ? string.Empty : stavrow.Barcode.Trim();

                if (!stavrow.IsIDCinnostNull())
                    tbIDCinnost.Text = stavrow.IDCinnost.Trim();

                if (!stavrow.IsIDCinnostNull())
                {
                    selectedCinnost = ((Fask.Interfaces.Servis.IServis)providerServis).GetCinnostByID(stavrow.IDCinnost);

                    tbIDCinnost.Text = selectedCinnost.Oznaceni.Trim() + (selectedCinnost.IsIDNull() ? string.Empty : (" (" + selectedCinnost.ID.Trim() + ")"));
                }
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
                if (stavrow != null)
                {
                    Fask.Interfaces.DataSets.Servis.CZMST_Servis_StavRow stav_test = ((Fask.Interfaces.Servis.IServis)providerServis).GetStavByID(stavrow.ID);

                    if (stav_test == null)
                    {
                        MessageBox.Show("Záznam byl od posledního načtení odstraněn.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // kvuli refreshi
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        return;
                    }
                    else
                    {
                        IEqualityComparer<Fask.Interfaces.DataSets.Servis.CZMST_Servis_StavRow> comparer = DataRowComparer.Default;
                        bool isMatch = comparer.Equals(stavrow, stav_test);
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
                Fask.Interfaces.DataSets.Servis.CZMST_Servis_StavRow newStavRow = dsServis2.CZMST_Servis_Stav.NewCZMST_Servis_StavRow();

                newStavRow.ID = tbID.Text.Trim();
                newStavRow.Oznaceni = tbOznaceni.Text.Trim();

                if (string.IsNullOrEmpty(tbBarcode.Text.Trim()))
                    newStavRow.SetBarcodeNull();
                else
                    newStavRow.Barcode = tbBarcode.Text.Trim();

                // pokud se edituje zaznam, id cinnosti se zachova
                if (selectedCinnost != null && !selectedCinnost.IsIDNull())
                {
                    newStavRow.IDCinnost = selectedCinnost.ID;
                }
                else
                    newStavRow.SetIDCinnostNull();
                //if (stavrow != null && !stavrow.IsIDCinnostNull())
                //    newStavRow.IDCinnost = stavrow.IDCinnost;
                //else
                //    newStavRow.SetIDCinnostNull();

                // je úprava záznamu
                if (stavrow != null)
                {
                    ((Fask.Interfaces.Servis.IServis)providerServis).UpdateStav(newStavRow);
                }
                else   // nový záznam
                {
                    ((Fask.Interfaces.Servis.IServis)providerServis).InsertStav(newStavRow);
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

                if(string.IsNullOrEmpty(tbID.Text.Trim()))
                    errorProvider1.SetError(tbID, "Musíte zadat id stavu");
                
                if (stavrow == null)
                {
                    // vytváří se nový záznam, kontrola existence id
                    Fask.Interfaces.DataSets.Servis.CZMST_Servis_StavRow stav = ((Fask.Interfaces.Servis.IServis)providerServis).GetStavByID(tbID.Text);
                    if (stav != null)
                    {
                        tbID.Focus();
                        //textBoxId.SelectAll();
                        errorProvider1.SetError(tbID, "Zadané id stavu již existuje");
                        //throw new Exception("Zadané id uživatele již existuje");
                    }
                }

                if (string.IsNullOrEmpty(tbOznaceni.Text.Trim()))
                {
                    tbOznaceni.Focus();
                    errorProvider1.SetError(tbOznaceni, "Musíte zadat označení");
                    //throw new Exception("Musíte zadat jméno");
                }
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
                    if (tbID == tb)
                    {
                        text = "Jedinečný identifikátor stavu.";
                    }
                    else if (tbOznaceni == tb)  // oznaceni
                    {
                        text = "Označení stavu (text, který se bude zobrazovat).";
                    }
                    else if (tbBarcode == tb)  // oznaceni
                    {
                        text = "Čárový kód stavu.";
                    }
                    else if (tbIDCinnost == tb)
                    {
                        text = "Počáteční činnost stavu.";
                    }
                }
                if (sender is Button)
                {
                    Button btn = ((Button)sender);
                    if (btnCinnostID == btn)
                    {
                        text = "Počáteční činnost stavu.";
                    }
                }
            }
            catch { }
            finally
            {
                tbNapoveda.Text = text;
            }
        }

        private void btnCinnostID_Click(object sender, EventArgs e)
        {
            // aktualizace napovedy ... po kliknuti se nezavola
            formZdrojeEdit_Enter(sender, null);

            PerformPriraditCinnost();
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
    }
}
