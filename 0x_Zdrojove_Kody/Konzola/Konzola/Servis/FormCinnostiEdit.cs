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
    public partial class FormCinnostiEdit : Form
    {
        private Fask.Interfaces.IMES providerServis = null;
        /// <summary>
        /// vytvoreny/upraveny zaznam.
        /// </summary>
        public Fask.Interfaces.DataSets.Servis.CZMST_Servis_CinnostRow returnrow { get; set; }
        /// <summary>
        /// Zdroj, který se má upravit.
        /// </summary>
        public Fask.Interfaces.DataSets.Servis.CZMST_Servis_CinnostRow cinnostrow { get; set; }

        public FormCinnostiEdit()
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

                // naplneni comboboxu typy
                Dictionary<string, string> test = new Dictionary<string, string>();
                test.Add("A", "Text");
                test.Add("N", "Číslo");
                test.Add("P", "Fotka");
                test.Add("D", "Databáze");
                test.Add("V", "Volba (ano/ne)");
                cbbType.DataSource = new BindingSource(test, null);
                cbbType.DisplayMember = "Value";
                cbbType.ValueMember = "Key";

                cbbType.SelectedItem = null;
                
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

            // je úprava záznamu, dojde k načtení dat
            if (cinnostrow != null)
            {
                tbID.Enabled = false;
                tbID.Text = cinnostrow.ID.Trim();
                tbOznaceni.Text = cinnostrow.Oznaceni.Trim();
                tbBarcode.Text = cinnostrow.IsBarcodeNull() ? string.Empty : cinnostrow.Barcode.Trim();
                //tbType.Text = cinnostrow.TYPE.Trim();
                cbbType.SelectedValue = cinnostrow.TYPE.Trim();
                tbTypeValue.Text = cinnostrow.IsTYPEVALUENull() ? string.Empty : cinnostrow.TYPEVALUE;
                tbRequiredLength.Text = cinnostrow.IsRequiredLengthNull() ? string.Empty : cinnostrow.RequiredLength.ToString();
                cbMandatory.Checked = Convert.ToBoolean(cinnostrow.Mandatory);
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
                if (cinnostrow != null)
                {
                    Fask.Interfaces.DataSets.Servis.CZMST_Servis_CinnostRow cinnost_test = ((Fask.Interfaces.Servis.IServis)providerServis).GetCinnostByID(cinnostrow.ID);

                    if (cinnost_test == null)
                    {
                        MessageBox.Show("Záznam byl od posledního načtení odstraněn.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // kvuli refreshi
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        return;
                    }
                    else
                    {
                        IEqualityComparer<Fask.Interfaces.DataSets.Servis.CZMST_Servis_CinnostRow> comparer = DataRowComparer.Default;
                        bool isMatch = comparer.Equals(cinnostrow, cinnost_test);
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

                Fask.Interfaces.DataSets.Servis dsServis = new Fask.Interfaces.DataSets.Servis();
                Fask.Interfaces.DataSets.Servis.CZMST_Servis_CinnostRow newCinnostRow = dsServis.CZMST_Servis_Cinnost.NewCZMST_Servis_CinnostRow();

                newCinnostRow.ID = tbID.Text.Trim();
                newCinnostRow.Oznaceni = tbOznaceni.Text.Trim();
                if (string.IsNullOrEmpty(tbBarcode.Text.Trim()))
                    newCinnostRow.SetBarcodeNull();
                else
                    newCinnostRow.Barcode = tbBarcode.Text.Trim();
                //newCinnostRow.TYPE = tbType.Text.Trim();
                //cbbType.SelectedValue = "A";
                //string value = cbbType.SelectedValue.ToString();
                newCinnostRow.TYPE = cbbType.SelectedValue.ToString();

                if (string.IsNullOrEmpty(tbTypeValue.Text.Trim()))
                    newCinnostRow.SetTYPEVALUENull();
                else
                    newCinnostRow.TYPEVALUE = tbTypeValue.Text.Trim();

                newCinnostRow.Mandatory = (byte)(cbMandatory.Checked ? 1 : 0);

                if (string.IsNullOrEmpty(tbRequiredLength.Text.Trim()))
                    newCinnostRow.SetRequiredLengthNull();
                else
                    newCinnostRow.RequiredLength = int.Parse(tbRequiredLength.Text);


                // je úprava záznamu
                if (cinnostrow != null)
                {
                    ((Fask.Interfaces.Servis.IServis)providerServis).UpdateCinnost(newCinnostRow);
                }
                else   // nový záznam
                {
                    ((Fask.Interfaces.Servis.IServis)providerServis).InsertCinnost(newCinnostRow);                    
                }
                returnrow = newCinnostRow;

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
                    errorProvider1.SetError(tbID, "Musíte zadat id činnosti");
                    //throw new Exception("Musíte zadat id uživatele");
                
                if (cinnostrow == null)
                {
                    // vytváří se nový záznam, kontrola existence id
                    Fask.Interfaces.DataSets.Servis.CZMST_Servis_CinnostRow cinnost = ((Fask.Interfaces.Servis.IServis)providerServis).GetCinnostByID(tbID.Text);
                    if (cinnost != null)
                    {
                        tbID.Focus();
                        errorProvider1.SetError(tbID, "Zadaná činnost již existuje");
                    }
                }

                // kontrola oznaceni
                if (string.IsNullOrEmpty(tbOznaceni.Text.Trim()))
                {
                    tbOznaceni.Focus();
                    errorProvider1.SetError(tbOznaceni, "Musíte zadat označení");
                }

                // kontrola type
                //if (string.IsNullOrEmpty(tbType.Text.Trim()))
                //{
                //    tbType.Focus();
                //    errorProvider1.SetError(tbType, "Musíte zadat typ");
                //}
                //else 
                //{
                //    List<string> types = new List<string>();
                //    types.AddRange(new string[] { "A", "N", "P", "D", "V" });
                //    bool contains = types.Contains(tbType.Text.Trim());
                //    if (!contains)
                //    {
                //        tbType.Focus();
                //        errorProvider1.SetError(tbType, "Zvolený typ činnosti neexistuje");
                //    }
                //}

                if (cbbType.SelectedValue == null)
                {
                    cbbType.Focus();
                    errorProvider1.SetError(cbbType, "Musíte zadat typ");
                }
                else if ("D" == cbbType.SelectedValue.ToString())
                {
                    // test vyplneni
                    if (string.IsNullOrEmpty(tbTypeValue.Text.Trim()))
                    {
                        tbTypeValue.Focus();
                        errorProvider1.SetError(tbTypeValue, "Musíte zadat název databázové tabulky");
                    }
                    else
                    {
                        // test existence zaznamu v seznamu dynamickych tabulek
                        Fask.Interfaces.DataSets.Servis.CZMST_Servis_Dynamic_Table_DefinitionRow rowDynTab = ((Fask.Interfaces.Servis.IServis)providerServis).GetDynamicTableDefinitionByTypeName(tbTypeValue.Text.Trim());
                        if (rowDynTab == null)
                        {
                            tbTypeValue.Focus();
                            errorProvider1.SetError(tbTypeValue, "Zkratka databázové tabulky není v číselníku dynamických tabulek");
                        }
                    }
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

        private void formCinnostiEdit_Enter(object sender, EventArgs e)
        {
            tbNapoveda.Text = string.Empty;
            string text = string.Empty;

            try
            {
                if (sender is TextBox)
                {
                    TextBox tb = ((TextBox)sender);
                    string propertyName = tb.Name;
                    if (tbID == tb)
                    {
                        text = "Jedinečný identifikátor činnosti.";
                    }
                    else if (tbOznaceni == tb)  // oznaceni
                    {
                        text = "Označení činnosti (text, který se bude zobrazovat).";
                    }
                    else if (tbBarcode == tb)  // oznaceni
                    {
                        text = "Čárový kód činnosti.";
                    }
                    //else if (tbType == tb)
                    //{
                    //    text = 
                    //        "Typ činnosti:\r\n" + 
                    //        "A = Text\r\n" + 
                    //        "N = Číslo\r\n" + 
                    //        "P = Fotka\r\n" + 
                    //        "D = Databáze (zobrazení dynamické tabulky)\r\n" + 
                    //        "V = Volba (ano/ne)"
                    //        ;
                    //}
                    else if (tbTypeValue == tb)
                    {
                        text = "Hodnota typu činnosti:\r\n" + 
                            "[A,N] = Výchozí/předvyplněná hodnota\r\n" + 
                            "[P,V] = Název dialogu pro popis okna\r\n" + 
                            "[D] = Název tabulky pro vyhledávání hodnot z tabulky [CZMST_Servis_Dynamic_Table_definition]"
                            ;
                    }
                    else if (tbRequiredLength == tb)
                    {
                        text = "Přesná délka textu(A)/čísla(N), která se musí zadat.";
                    }
                }
                else if (sender is CheckBox)
                {
                    CheckBox cb = (CheckBox)sender;
                    if (cb == cbMandatory)
                    {
                        text = "Povinné zadání textu(A)/čísla(N).";
                    }
                }
                else if (sender is ComboBox)
                {
                    ComboBox cb = (ComboBox)sender;
                    if (cb == cbbType)
                    {
                        text =
                            "Typ činnosti:\r\n" +
                            "A = Text\r\n" +
                            "N = Číslo\r\n" +
                            "P = Fotka\r\n" +
                            "D = Databáze (zobrazení dynamické tabulky)\r\n" +
                            "V = Volba (ano/ne)"
                            ;
                    }
                }
            }
            catch { }
            finally
            {
                tbNapoveda.Text = text;
            }
        }
    }
}
