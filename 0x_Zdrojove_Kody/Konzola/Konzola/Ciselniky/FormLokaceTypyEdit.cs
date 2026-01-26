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
    public partial class FormLokaceTypyEdit : Form
    {
        private Fask.Interfaces.IMES providerTypy = null;

        /// <summary>
        /// vytvoreny/upraveny zaznam.
        /// </summary>
        public Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_LokaceTypyRow returnrow { get; set; }
        /// <summary>
        /// Typ lokace, který se má upravit.
        /// </summary>
        public Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_LokaceTypyRow typlokacerow { get; set; }

        public FormLokaceTypyEdit()
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

                if(providerTypy == null)
                    throw new Exception("Provider 'Typy lokací' není inicializován");

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
                    if (providerTypy == null) //inicializace se provede pouze pokud nebyla provedena ... 
                    {
                        //Assembly providerAssemlby = Assembly.LoadFrom(Server.MapPath(providerAssemblyPath));
                        Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                        Type[] types = providerAssemlby.GetTypes();
                        foreach (Type t in types)
                        {
                            try
                            {
                                if (typeof(Fask.Interfaces.Ciselniky.SkladLokace_LokaceTypy.ISkladLokace_LokaceTypy2).IsAssignableFrom(t))
                                {
                                    providerTypy = (Fask.Interfaces.Ciselniky.SkladLokace_LokaceTypy.ISkladLokace_LokaceTypy2)providerAssemlby.CreateInstance(t.FullName);
                                    if (providerTypy != null)
                                        break;
                                }
                            }
                            catch { }
                        }
                        //return config;
                    }

                    providerTypy.InitProvider();
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
            if (typlokacerow != null)
            {
                tbType.Enabled = false;
                tbType.Text = typlokacerow.IsTYPENull() ? string.Empty : typlokacerow.TYPE.Trim();
                tbType.Enabled = false;
                tbDescription.Text = typlokacerow.IsDescriptionNull() ? string.Empty : typlokacerow.Description.Trim();
                cbLokaceBezna.Checked = typlokacerow.IS_NORMAL;
                cbLokacePrijmova.Checked = typlokacerow.IS_RECEIVE;
                cbLokaceVychozi.Checked = typlokacerow.IS_DEFAULT;
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
                if (typlokacerow != null)
                {
                    //Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_LokaceTypyRow stav_test = ((Fask.Interfaces.Ciselniky.ISkladLokace_LokaceTypy)providertypy).GetSkladLokace_LokaceTypyByType(tbType.Text.Trim());
                    Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_LokaceTypyRow stav_test;

                    if ((providerTypy != null) && (providerTypy is Fask.Interfaces.Ciselniky.SkladLokace_LokaceTypy.ISkladLokace_LokaceTypy2_GetSkladLokace_LokaceTypyByType))
                        stav_test = ((Fask.Interfaces.Ciselniky.SkladLokace_LokaceTypy.ISkladLokace_LokaceTypy2_GetSkladLokace_LokaceTypyByType)providerTypy).GetSkladLokace_LokaceTypyByType(tbType.Text.Trim());
                    else
                        throw new NotImplementedException("Provider neimplementuje ISkladLokace_LokaceTypy2_GetSkladLokace_LokaceTypyByType.");


                    if (stav_test == null)
                    {
                        MessageBox.Show("Záznam byl od posledního načtení odstraněn.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // kvuli refreshi
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        return;
                    }
                    else
                    {
                        IEqualityComparer<Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_LokaceTypyRow> comparer = DataRowComparer.Default;
                        bool isMatch = comparer.Equals(typlokacerow, stav_test);
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

                Fask.Interfaces.DataSets.SkladLokace ds2 = new Fask.Interfaces.DataSets.SkladLokace();
                Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_LokaceTypyRow newTypRow = ds2.CZMST_SkladLokace_LokaceTypy.NewCZMST_SkladLokace_LokaceTypyRow();

                newTypRow.TYPE = tbType.Text.Trim();
                newTypRow.Description = tbDescription.Text.Trim();
                newTypRow.IS_RECEIVE = cbLokacePrijmova.Checked;
                newTypRow.IS_NORMAL = cbLokaceBezna.Checked;
                newTypRow.IS_DEFAULT = cbLokaceVychozi.Checked;

                // je úprava záznamu
                if (typlokacerow != null)
                {
                    //((Fask.Interfaces.Ciselniky.ISkladLokace_LokaceTypy)providertypy).UpdateSkladLokace_LokaceTypy(newTypRow);


                    if ((providerTypy != null) && (providerTypy is Fask.Interfaces.Ciselniky.SkladLokace_LokaceTypy.ISkladLokace_LokaceTypy2_UpdateSkladLokace_LokaceTypy))
                        ((Fask.Interfaces.Ciselniky.SkladLokace_LokaceTypy.ISkladLokace_LokaceTypy2_UpdateSkladLokace_LokaceTypy)providerTypy).UpdateSkladLokace_LokaceTypy(newTypRow);
                    else
                        throw new NotImplementedException("Provider neimplementuje ISkladLokace_LokaceTypy2_UpdateSkladLokace_LokaceTypy.");
                }
                else   // nový záznam
                {
                    //((Fask.Interfaces.Ciselniky.ISkladLokace_LokaceTypy)providertypy).InsertSkladLokace_LokaceTypy(newTypRow);


                    if ((providerTypy != null) && (providerTypy is Fask.Interfaces.Ciselniky.SkladLokace_LokaceTypy.ISkladLokace_LokaceTypy2_InsertSkladLokace_LokaceTypy))
                        ((Fask.Interfaces.Ciselniky.SkladLokace_LokaceTypy.ISkladLokace_LokaceTypy2_InsertSkladLokace_LokaceTypy)providerTypy).InsertSkladLokace_LokaceTypy(newTypRow);
                    else
                        throw new NotImplementedException("Provider neimplementuje ISkladLokace_LokaceTypy2_InsertSkladLokace_LokaceTypy.");
                }

                returnrow = newTypRow;
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

                if (string.IsNullOrEmpty(tbType.Text.Trim()))
                    errorProvider1.SetError(tbType, "Musíte zadat typ lokace");
                
                // kontrola delky
                if (tbType.Text.Length > 2)
                {
                    tbType.Focus();
                    errorProvider1.SetError(tbType, "Typ lokace '" + tbType.Text.Trim() + "' může obsahovat maximálné 2 znaky");
                }

                if (typlokacerow == null)
                {
                    // vytváří se nový záznam, kontrola existence id
                    //Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_LokaceTypyRow stav = ((Fask.Interfaces.Ciselniky.ISkladLokace_LokaceTypy)providertypy).GetSkladLokace_LokaceTypyByType(tbType.Text.Trim());
                    Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_LokaceTypyRow stav;

                    if ((providerTypy != null) && (providerTypy is Fask.Interfaces.Ciselniky.SkladLokace_LokaceTypy.ISkladLokace_LokaceTypy2_GetSkladLokace_LokaceTypyByType))
                        stav = ((Fask.Interfaces.Ciselniky.SkladLokace_LokaceTypy.ISkladLokace_LokaceTypy2_GetSkladLokace_LokaceTypyByType)providerTypy).GetSkladLokace_LokaceTypyByType(tbType.Text.Trim());
                    else
                        throw new NotImplementedException("Provider neimplementuje ISkladLokace_LokaceTypy2_GetSkladLokace_LokaceTypyByType.");
                    
                    if (stav != null)
                    {
                        tbType.Focus();
                        errorProvider1.SetError(tbType, "Typ lokace '" + tbType.Text.Trim() + "' již existuje");
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

        private void formZdrojeEdit_Enter(object sender, EventArgs e)
        {
            tbNapoveda.Text = string.Empty;
            string text = string.Empty;

            try
            {
                if (sender is TextBox)
                {
                    TextBox tb = ((TextBox)sender);
                    if (tbType == tb)
                    {
                        text = "Identifikátor typu lokace. Může obsahovat maximálné 2 znaky.";
                    }
                    else if (tbDescription == tb)  // oznaceni
                    {
                        text = "Označení typu lokace lokace.";
                    }
                }
                else if (sender is CheckBox)
                {
                    CheckBox tb = ((CheckBox)sender);
                    if (cbLokaceBezna == tb)
                    {
                        text = "Parametr určující, zdali se jedná o běžnou lokaci.";
                    }
                    else if (cbLokacePrijmova == tb)  // oznaceni
                    {
                        text = "Parametr určující, zdali se jedná o příjmovou lokaci.";
                    }
                    else if (cbLokaceVychozi == tb)  // oznaceni
                    {
                        text = "Parametr určující, zdali se jedná o výchozí lokaci.";
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
