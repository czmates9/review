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

namespace Konzola.Ciselniky
{
    public partial class FormOdberateleEdit : Form
    {
        private Fask.Interfaces.IMES providerOdberatel = null;
        /// <summary>
        /// vytvoreny/upraveny zaznam.
        /// </summary>
        public Fask.Interfaces.DataSets.Odberatele.CZMST090Row returnrow { get; set; }
        /// <summary>
        /// Odberatel, který se má upravit.
        /// </summary>
        public Fask.Interfaces.DataSets.Odberatele.CZMST090Row odberatelrow { get; set; }

        public FormOdberateleEdit()
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

                if (providerOdberatel == null)
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

            // je úprava záznamu, dojde k načtení dat
            if (odberatelrow != null)
            {
                tbOdb_id.Enabled = false;
                tbOdb_id.Text = odberatelrow.odb_id.Trim();
                tbOdb_desc.Text = odberatelrow.Isodb_descNull() ? string.Empty : odberatelrow.odb_desc.Trim();                
                tbOdb_typ.Text = odberatelrow.Isodb_typNull() ?string.Empty : odberatelrow.odb_typ.Trim();
                tbOdb_carcode.Text = odberatelrow.Isodb_carcodeNull() ? string.Empty : odberatelrow.odb_carcode.Trim();
                tbOdb_ico.Text = odberatelrow.Isodb_icoNull() ? string.Empty : odberatelrow.odb_ico.Trim();
                tbMena_id.Text = odberatelrow.Ismena_idNull() ? string.Empty : odberatelrow.mena_id.Trim();
                tbOdb_misto.Text = odberatelrow.Isodb_mistoNull() ? string.Empty : odberatelrow.odb_misto.Trim();
                tbOdb_ulice.Text = odberatelrow.Isodb_uliceNull() ? string.Empty : odberatelrow.odb_ulice.Trim();
                tbOdb_cisloOr.Text = odberatelrow.Isodb_cisloOrNull() ? string.Empty : odberatelrow.odb_cisloOr.Trim();
                tbOdb_psc.Text = odberatelrow.Isodb_pscNull() ? string.Empty : odberatelrow.odb_psc.Trim();
                tbOdb_dic.Text = odberatelrow.Isodb_dicNull() ? string.Empty : odberatelrow.odb_dic.Trim();
                cbOdb_Odberatel.Checked = odberatelrow.Isodb_OdberatelNull() ? false : odberatelrow.odb_Odberatel;
                cbOdb_Dodavatel.Checked = odberatelrow.Isodb_DodavatelNull() ? false : odberatelrow.odb_Dodavatel;
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
                if (odberatelrow != null)
                {
                    //Fask.Interfaces.DataSets.Odberatele.CZMST090Row odb_test = ((Fask.Interfaces.Ciselniky.IOdberatele)providerOdberatel).GetOdberatelByID(odberatelrow.odb_id);
                    Fask.Interfaces.DataSets.Odberatele.CZMST090Row odb_test;


                    if ((providerOdberatel != null) && (providerOdberatel is Fask.Interfaces.Ciselniky.Odberatele.IOdberatele2_GetOdberatelByID))
                        odb_test = ((Fask.Interfaces.Ciselniky.Odberatele.IOdberatele2_GetOdberatelByID)providerOdberatel).GetOdberatelByID(odberatelrow.odb_id);
                    else
                        throw new NotImplementedException("Provider neimplementuje IOdberatele2_GetOdberatelByID.");

                    if (odb_test == null)
                    {
                        MessageBox.Show("Záznam byl od posledního načtení odstraněn.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // kvuli refreshi
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        return;
                    }
                    else
                    {
                        IEqualityComparer<Fask.Interfaces.DataSets.Odberatele.CZMST090Row> comparer = DataRowComparer.Default;
                        bool isMatch = comparer.Equals(odberatelrow, odb_test);
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

                Fask.Interfaces.DataSets.Odberatele dsOdberatele2 = new Fask.Interfaces.DataSets.Odberatele();
                Fask.Interfaces.DataSets.Odberatele.CZMST090Row newOdberatelRow = dsOdberatele2.CZMST090.NewCZMST090Row();

                newOdberatelRow.odb_id = tbOdb_id.Text.Trim();
                newOdberatelRow.odb_desc = tbOdb_desc.Text.Trim();

                // odb_typ
                if (string.IsNullOrEmpty(tbOdb_typ.Text.Trim()))
                    newOdberatelRow.Setodb_typNull();
                else
                    newOdberatelRow.odb_typ = tbOdb_typ.Text.Trim();
                
                // odb_carcode
                if (string.IsNullOrEmpty(tbOdb_carcode.Text.Trim()))
                    newOdberatelRow.Setodb_carcodeNull();
                else
                    newOdberatelRow.odb_carcode = tbOdb_carcode.Text.Trim();

                // odb_ico
                if (string.IsNullOrEmpty(tbOdb_ico.Text.Trim()))
                    newOdberatelRow.Setodb_icoNull();
                else
                    newOdberatelRow.odb_ico = tbOdb_ico.Text.Trim();

                // mena_id
                if (string.IsNullOrEmpty(tbMena_id.Text.Trim()))
                    newOdberatelRow.Setmena_idNull();
                else
                    newOdberatelRow.mena_id= tbMena_id.Text.Trim();

                // odb_misto
                newOdberatelRow.odb_misto = tbOdb_misto.Text.Trim();

                // odb_ulice
                newOdberatelRow.odb_ulice = tbOdb_ulice.Text.Trim();

                // odb_cisloOr
                newOdberatelRow.odb_cisloOr = tbOdb_cisloOr.Text.Trim();

                // odb_psc
                newOdberatelRow.odb_psc = tbOdb_psc.Text.Trim();
                
                // odb_dic
                newOdberatelRow.odb_dic = tbOdb_dic.Text.Trim();

                // odb_Odberatel
                newOdberatelRow.odb_Odberatel = cbOdb_Odberatel.Checked;

                // odb_Dodavatel
                newOdberatelRow.odb_Dodavatel = cbOdb_Dodavatel.Checked;

                // je úprava záznamu
                if (odberatelrow != null)
                {
                    //((Fask.Interfaces.Ciselniky.IOdberatele)providerOdberatel).UpdateOdberatel(newOdberatelRow);


                    if ((providerOdberatel != null) && (providerOdberatel is Fask.Interfaces.Ciselniky.Odberatele.IOdberatele2_UpdateOdberatel))
                        ((Fask.Interfaces.Ciselniky.Odberatele.IOdberatele2_UpdateOdberatel)providerOdberatel).UpdateOdberatel(newOdberatelRow);
                    else
                        throw new NotImplementedException("Provider neimplementuje IOdberatele2_UpdateOdberatel.");
                
                }
                else   // nový záznam
                {
                    //((Fask.Interfaces.Ciselniky.IOdberatele)providerOdberatel).InsertOdberatel(newOdberatelRow);

                    if ((providerOdberatel != null) && (providerOdberatel is Fask.Interfaces.Ciselniky.Odberatele.IOdberatele2_InsertOdberatel))
                        ((Fask.Interfaces.Ciselniky.Odberatele.IOdberatele2_InsertOdberatel)providerOdberatel).InsertOdberatel(newOdberatelRow);
                    else
                        throw new NotImplementedException("Provider neimplementuje IOdberatele2_InsertOdberatel.");
                
                }

                returnrow = newOdberatelRow;
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

                if(string.IsNullOrEmpty(tbOdb_id.Text.Trim()))
                    errorProvider1.SetError(tbOdb_id, "Musíte zadat id odběratele");
                
                if (odberatelrow == null)
                {
                    // vytváří se nový záznam, kontrola existence id
                    //Fask.Interfaces.DataSets.Odberatele.CZMST090Row odb = ((Fask.Interfaces.Ciselniky.IOdberatele)providerOdberatel).GetOdberatelByID(tbOdb_id.Text);
                    Fask.Interfaces.DataSets.Odberatele.CZMST090Row odb;

                    if ((providerOdberatel != null) && (providerOdberatel is Fask.Interfaces.Ciselniky.Odberatele.IOdberatele2_GetOdberatelByID))
                        odb = ((Fask.Interfaces.Ciselniky.Odberatele.IOdberatele2_GetOdberatelByID)providerOdberatel).GetOdberatelByID(tbOdb_id.Text);
                    else
                        throw new NotImplementedException("Provider neimplementuje IOdberatele2_GetOdberatelByID.");
                    
                    if (odb != null)
                    {
                        tbOdb_id.Focus();
                        errorProvider1.SetError(tbOdb_id, "Zadané id odběratele již existuje");
                    }
                }

                if (string.IsNullOrEmpty(tbOdb_desc.Text.Trim()))
                {
                    tbOdb_desc.Focus();
                    errorProvider1.SetError(tbOdb_desc, "Musíte zadat označení");
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
                    string propertyName = tb.Name;
                    if (tbOdb_id == tb)
                    {
                        text = "Jedinečný identifikátor odběratele.";
                    }
                    else if (tbOdb_desc == tb)  // oznaceni
                    {
                        text = "Označení odběratele (text, který se bude zobrazovat).";
                    }
                    else if (tbOdb_carcode == tb)  // oznaceni
                    {
                        text = "Čárový kód odběratele.";
                    }
                    else if (tbOdb_typ == tb)
                    {
                        text = "Typ odběratele (0 – 5 udává cenovou hladinu pro výběr ceny  z číselníku zboží FASK_ZASOBY).";
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
