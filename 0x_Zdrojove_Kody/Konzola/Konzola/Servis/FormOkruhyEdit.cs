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
using Konzola.Extensions;

namespace Konzola.Servis
{
    public partial class FormOkruhyEdit : Form
    {
        private Fask.Interfaces.IMES providerServis = null;
        private Fask.Interfaces.IMES providerOdberatel = null;

        /// <summary>
        /// vytvoreny/upraveny zaznam.
        /// </summary>
        public Fask.Interfaces.DataSets.Servis.CZMST_Servis_OkruhRow returnrow { get; set; }
        /// <summary>
        /// Okruh, který se má upravit.
        /// </summary>
        public Fask.Interfaces.DataSets.Servis.CZMST_Servis_OkruhRow okruhrow { get; set; }

        private Fask.Interfaces.DataSets.Odberatele.CZMST090Row selectedOdberatel { get; set; }

        public FormOkruhyEdit()
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
                    throw new Exception("Provider servis není inicializován");

                if (providerOdberatel == null)
                    throw new Exception("Provider odběratel není inicializován");

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

            try
            {
                if (!String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                {
                    if (providerOdberatel == null) //inicializace se provede pouze pokud nebyla provedena ... 
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
                                    providerOdberatel = (Fask.Interfaces.Ciselniky.Odberatele.IOdberatele2)providerAssemlby.CreateInstance(t.FullName);
                                    if (providerOdberatel != null)
                                        break;
                                }
                            }
                            catch { }
                        }
                        //return config;
                    }

                    providerOdberatel.InitProvider();
                                  
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
            tbIDOdberatel.Text = "Nenastaveno";
            
            // je úprava záznamu, dojde k načtení dat
            if (okruhrow != null)
            {
                tbID.Enabled = false;
                tbID.Text = okruhrow.ID.Trim();
                tbOznaceni.Text = okruhrow.Oznaceni.Trim();
                tbBarcode.Text = okruhrow.IsBarcodeNull() ? string.Empty : okruhrow.Barcode.Trim();
                tbZdrojSeznamID.Text = okruhrow.ZdrojSeznamID.Trim();

                // dotáhnutí odběratele
                if(!okruhrow.IsODB_IDNull())
                {
                    //Fask.Interfaces.DataSets.Odberatele.CZMST090Row row = ((Fask.Interfaces.Ciselniky.IOdberatele)providerOdberatel).GetOdberatelByID(okruhrow.ODB_ID);
                    Fask.Interfaces.DataSets.Odberatele.CZMST090Row row;

                    if ((providerOdberatel != null) && (providerOdberatel is Fask.Interfaces.Ciselniky.Odberatele.IOdberatele2_GetOdberatelByID))
                        row = ((Fask.Interfaces.Ciselniky.Odberatele.IOdberatele2_GetOdberatelByID)providerOdberatel).GetOdberatelByID(okruhrow.ODB_ID);
                    else
                        throw new NotImplementedException("Provider neimplementuje IOdberatele2_GetOdberatelByID.");

                    selectedOdberatel = row;
                    if(row != null)
                        tbIDOdberatel.Text = (selectedOdberatel.Isodb_descNull() ? string.Empty : selectedOdberatel.odb_desc.Trim()) + " (" + selectedOdberatel.odb_id.Trim() + ")";
                }
                else
                    selectedOdberatel = null;
            }
            else 
                selectedOdberatel = null;
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
                if (okruhrow != null)
                {
                    Fask.Interfaces.DataSets.Servis.CZMST_Servis_OkruhRow okruh_test = ((Fask.Interfaces.Servis.IServis)providerServis).GetOkruhByID(okruhrow.ID);

                    if (okruh_test == null)
                    {
                        MessageBox.Show("Záznam byl od posledního načtení odstraněn.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // kvuli refreshi
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        return;
                    }
                    else
                    {
                        Fask.Interfaces.DataSets.Servis.CZMST_Servis_Okruh_COMPAREDataTable dtCmp = new Fask.Interfaces.DataSets.Servis.CZMST_Servis_Okruh_COMPAREDataTable();
                        dtCmp.ImportRow(okruhrow);
                        dtCmp.ImportRow(okruh_test);
                        
                        //Fask.Interfaces.DataSets.Ukolovani.CZ_UKOL_COMPAREDataTable dtCmpUkol = new UkolovaniDataset.CZ_UKOL_COMPAREDataTable();
                        //dtCmpUkol.ImportRow(rowUkolEdit);
                        //dtCmpUkol.ImportRow(dtUkol.First());

                        IEqualityComparer<Fask.Interfaces.DataSets.Servis.CZMST_Servis_Okruh_COMPARERow> comparer = DataRowComparer.Default;
                        bool isMatch = comparer.Equals(dtCmp[0], dtCmp[1]);
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
                Fask.Interfaces.DataSets.Servis.CZMST_Servis_OkruhRow newOkruhRow = dsServis2.CZMST_Servis_Okruh.NewCZMST_Servis_OkruhRow();

                newOkruhRow.ID = tbID.Text.Trim();
                newOkruhRow.Oznaceni = tbOznaceni.Text.Trim();

                if (string.IsNullOrEmpty(tbBarcode.Text.Trim()))
                    newOkruhRow.SetBarcodeNull();
                else
                    newOkruhRow.Barcode = tbBarcode.Text.Trim();

                newOkruhRow.ZdrojSeznamID = tbZdrojSeznamID.Text;
                newOkruhRow.ODB_ID = selectedOdberatel.odb_id;

                //if (stavrow != null && !stavrow.IsIDCinnostNull())
                //    newStavRow.IDCinnost = stavrow.IDCinnost;
                //else
                //    newStavRow.SetIDCinnostNull();

                // je úprava záznamu
                if (okruhrow != null)
                {
                    ((Fask.Interfaces.Servis.IServis)providerServis).UpdateOkruh(newOkruhRow);
                }
                else   // nový záznam
                {
                    ((Fask.Interfaces.Servis.IServis)providerServis).InsertOkruh(newOkruhRow);
                }

                returnrow = newOkruhRow;
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
                    errorProvider1.SetError(tbID, "Musíte zadat id okruhu");
                
                if (okruhrow == null)
                {
                    // vytváří se nový záznam, kontrola existence id
                    Fask.Interfaces.DataSets.Servis.CZMST_Servis_OkruhRow stav = ((Fask.Interfaces.Servis.IServis)providerServis).GetOkruhByID(tbID.Text);
                    if (stav != null)
                    {
                        tbID.Focus();
                        //textBoxId.SelectAll();
                        errorProvider1.SetError(tbID, "Zadané id okruhu již existuje");
                        //throw new Exception("Zadané id uživatele již existuje");
                    }

                    // kontrola zdrojseznamid
                    // tbZdrojSeznamID
                    // 1) pokud je novy zaznam, nesmi existovat
                    // 2) pokud je editace, neni mozne vyplnit
                    Fask.Interfaces.DataSets.Servis.CZMST_Servis_OkruhRow okruh = ((Fask.Interfaces.Servis.IServis)providerServis).GetOkruhByZdrojSeznamID(tbZdrojSeznamID.Text);
                    if (okruh != null)
                    {
                        tbZdrojSeznamID.Focus();
                        errorProvider1.SetError(tbZdrojSeznamID, "Zadaný zdroj seznamu již je přiřazen okruhu '" + okruh.Oznaceni.Trim() + "' (" + okruh.ID.Trim() + ")");
                    }
                }

                if (string.IsNullOrEmpty(tbOznaceni.Text.Trim()))
                {
                    tbOznaceni.Focus();
                    errorProvider1.SetError(tbOznaceni, "Musíte zadat označení");
                    //throw new Exception("Musíte zadat jméno");
                }

                
                if (string.IsNullOrEmpty(tbZdrojSeznamID.Text.Trim()))
                {
                    tbZdrojSeznamID.Focus();
                    errorProvider1.SetError(tbZdrojSeznamID, "Musíte zadat název seznamu zdrojů");
                    //throw new Exception("Musíte zadat jméno");
                }

                if (selectedOdberatel == null)
                {
                    errorProvider1.SetError(tbIDOdberatel, "Musíte zadat odběratele");
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
                        text = "Jedinečný identifikátor okruhu.";
                    }
                    else if (tbOznaceni == tb)  // oznaceni
                    {
                        text = "Označení okruhu (text, který se bude zobrazovat).";
                    }
                    else if (tbBarcode == tb)  // oznaceni
                    {
                        text = "Čárový kód okruhu.";
                    }
                    else if (tbIDOdberatel == tb)
                    {
                        text = "Vybraný odběratel.";
                    }
                    else if (tbZdrojSeznamID == tb)
                    {
                        text = "Název seznamu zdrojů. Každý okruh musí mít jedinečný seznam zdrojů.";
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

            PerformPriraditOdberatele();
        }

        private void PerformPriraditOdberatele()
        {
            try
            {
                if (!MySystem.LoginTest.UserLoginTest())
                    return;

                Fask.Interfaces.DataSets.Servis dsSelectedRows = new Fask.Interfaces.DataSets.Servis();
                using (Ciselniky.FormOdberateleSelect frmodberatele = new Ciselniky.FormOdberateleSelect(false, selectedOdberatel))
                {
                    frmodberatele.Text = "Přiřadit odběratele";
                    //frmstavy.rowsSelected = rowStav;
                    if (frmodberatele.ShowDialog(this) != DialogResult.OK)
                        return;

                    selectedOdberatel = frmodberatele.SelectedRow;

                    tbIDOdberatel.Text = (selectedOdberatel.Isodb_descNull() ? string.Empty : selectedOdberatel.odb_desc.Trim()) + " (" + selectedOdberatel.odb_id.Trim() + ")";
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
