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
    public partial class FormSkladyEdit : Form
    {
        private Fask.Interfaces.IMES providerSklady = null;
        /// <summary>
        /// vytvoreny/upraveny zaznam.
        /// </summary>
        public Fask.Interfaces.DataSets.Sklady.CZMST093Row returnrow { get; set; }
        /// <summary>
        /// Sklad, který se má upravit.
        /// </summary>
        public Fask.Interfaces.DataSets.Sklady.CZMST093Row skladrow { get; set; }

        public FormSkladyEdit()
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

                if (providerSklady == null)
                    throw new Exception("Provider 'Sklady' není inicializován");

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
                    if (providerSklady == null) //inicializace se provede pouze pokud nebyla provedena ... 
                    {
                        //Assembly providerAssemlby = Assembly.LoadFrom(Server.MapPath(providerAssemblyPath));
                        Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                        Type[] types = providerAssemlby.GetTypes();
                        foreach (Type t in types)
                        {
                            try
                            {
                                if (typeof(Fask.Interfaces.Ciselniky.Sklady.ISklady2).IsAssignableFrom(t))
                                {
                                    providerSklady = (Fask.Interfaces.Ciselniky.Sklady.ISklady2)providerAssemlby.CreateInstance(t.FullName);
                                    if (providerSklady != null)
                                        break;
                                }
                            }
                            catch { }
                        }
                        //return config;
                    }

                    providerSklady.InitProvider();
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
            if (skladrow != null)
            {
                #region old MaR 23.10. 2024
                //tbSklID.Enabled = false;
                //tbSklID.Text = skladrow.skl_id.Trim();
                //tbSklDesc.Text = skladrow.Isskl_descNull() ? string.Empty : skladrow.skl_desc.Trim();
                //tbSklTyp.Text = skladrow.Isskl_typNull() ? string.Empty : skladrow.skl_typ.Trim();
                //tbSklCarcode.Text = skladrow.Isskl_carcodeNull() ? string.Empty : skladrow.skl_carcode.Trim(); 
                #endregion

                tbSklID.Enabled = false;
                tbSklID.Text = skladrow.skl_id;
                tbSklDesc.Text = skladrow.Isskl_descNull() ? string.Empty : skladrow.skl_desc;
                tbSklTyp.Text = skladrow.Isskl_typNull() ? string.Empty : skladrow.skl_typ;
                tbSklCarcode.Text = skladrow.Isskl_carcodeNull() ? string.Empty : skladrow.skl_carcode;
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
                if (skladrow != null)
                {
                    //Fask.Interfaces.DataSets.Sklady.CZMST093Row sklad_test = ((Fask.Interfaces.Ciselniky.ISklady)providerSklady).GetSkladByID(tbSklID.Text.Trim());
                    Fask.Interfaces.DataSets.Sklady.CZMST093Row sklad_test;

                    if ((providerSklady != null) && (providerSklady is Fask.Interfaces.Ciselniky.Sklady.ISklady2_GetSkladByID))
                        sklad_test = ((Fask.Interfaces.Ciselniky.Sklady.ISklady2_GetSkladByID)providerSklady).GetSkladByID(tbSklID.Text.Trim());
                    else
                        throw new NotImplementedException("Provider neimplementuje ISklady2_GetSkladByID.");


                    if (sklad_test == null)
                    {
                        MessageBox.Show("Záznam byl od posledního načtení odstraněn.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // kvuli refreshi
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        return;
                    }
                    else
                    {
                        IEqualityComparer<Fask.Interfaces.DataSets.Sklady.CZMST093Row> comparer = DataRowComparer.Default;
                        bool isMatch = comparer.Equals(skladrow, sklad_test);
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

                Fask.Interfaces.DataSets.Sklady ds2 = new Fask.Interfaces.DataSets.Sklady();
                Fask.Interfaces.DataSets.Sklady.CZMST093Row newSkladRow = ds2.CZMST093.NewCZMST093Row();

                newSkladRow.skl_id = tbSklID.Text.Trim();

                if (string.IsNullOrEmpty(tbSklDesc.Text.Trim()))
                    newSkladRow.Setskl_descNull();
                else
                    newSkladRow.skl_desc = tbSklDesc.Text.Trim();

                if (string.IsNullOrEmpty(tbSklTyp.Text.Trim()))
                    newSkladRow.Setskl_typNull();
                else
                    newSkladRow.skl_typ = tbSklTyp.Text.Trim();

                if(string.IsNullOrEmpty(tbSklCarcode.Text.Trim()))
                    newSkladRow.Setskl_carcodeNull();
                else
                    newSkladRow.skl_carcode = tbSklCarcode.Text.Trim();

                // je úprava záznamu
                if (skladrow != null)
                {
                    //((Fask.Interfaces.Ciselniky.ISklady)providerSklady).UpdateSklad(newSkladRow);

                    if ((providerSklady != null) && (providerSklady is Fask.Interfaces.Ciselniky.Sklady.ISklady2_UpdateSklad))
                        ((Fask.Interfaces.Ciselniky.Sklady.ISklady2_UpdateSklad)providerSklady).UpdateSklad(newSkladRow);
                    else
                        throw new NotImplementedException("Provider neimplementuje ISklady2_UpdateSklad.");
                }
                else   // nový záznam
                {
                    //((Fask.Interfaces.Ciselniky.ISklady)providerSklady).InsertSklad(newSkladRow);

                    if ((providerSklady != null) && (providerSklady is Fask.Interfaces.Ciselniky.Sklady.ISklady2_InsertSklad))
                        ((Fask.Interfaces.Ciselniky.Sklady.ISklady2_InsertSklad)providerSklady).InsertSklad(newSkladRow);
                    else
                        throw new NotImplementedException("Provider neimplementuje ISklady2_InsertSklad.");
                }

                // opetovne nacteni uzivatele
                //returnrow = ((Fask.Interfaces.Ciselniky.ISklady)providerSklady).GetSkladByID(newSkladRow.skl_id);

                if ((providerSklady != null) && (providerSklady is Fask.Interfaces.Ciselniky.Sklady.ISklady2_GetSkladByID))
                    returnrow = ((Fask.Interfaces.Ciselniky.Sklady.ISklady2_GetSkladByID)providerSklady).GetSkladByID(newSkladRow.skl_id);
                else
                    throw new NotImplementedException("Provider neimplementuje ISklady2_GetSkladByID.");

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

                if (string.IsNullOrEmpty(tbSklID.Text.Trim()))
                    errorProvider1.SetError(tbSklID, "Musíte zadat id skladu");
                else
                {
                    // kontrola existence skladu
                    if (skladrow == null)
                    {
                        //Fask.Interfaces.DataSets.Sklady.CZMST093Row sklad = ((Fask.Interfaces.Ciselniky.ISklady)providerSklady).GetSkladByID(tbSklID.Text.Trim());
                        Fask.Interfaces.DataSets.Sklady.CZMST093Row sklad;

                        if ((providerSklady != null) && (providerSklady is Fask.Interfaces.Ciselniky.Sklady.ISklady2_GetSkladByID))
                            sklad = ((Fask.Interfaces.Ciselniky.Sklady.ISklady2_GetSkladByID)providerSklady).GetSkladByID(tbSklID.Text.Trim());
                        else
                            throw new NotImplementedException("Provider neimplementuje ISklady2_GetSkladByID.");

                        if (sklad != null)
                            errorProvider1.SetError(tbSklID, "Zadané id skladu již existuje");
                    }
                }

                if (string.IsNullOrEmpty(tbSklDesc.Text.Trim()))
                    errorProvider1.SetError(tbSklDesc, "Musíte zadat název skladu");

                if (skladrow == null)
                {
                    // vytváří se nový záznam, kontrola existence id
                    //Fask.Interfaces.DataSets.Sklady.CZMST093Row stav = ((Fask.Interfaces.Ciselniky.ISklady)providerSklady).GetSkladByID(tbSklID.Text.Trim());
                    Fask.Interfaces.DataSets.Sklady.CZMST093Row stav;

                    if ((providerSklady != null) && (providerSklady is Fask.Interfaces.Ciselniky.Sklady.ISklady2_GetSkladByID))
                        stav = ((Fask.Interfaces.Ciselniky.Sklady.ISklady2_GetSkladByID)providerSklady).GetSkladByID(tbSklID.Text.Trim());
                    else
                        throw new NotImplementedException("Provider neimplementuje ISklady2_GetSkladByID.");

                    if (stav != null)
                    {
                        tbSklID.Focus();
                        errorProvider1.SetError(tbSklDesc, "Sklad s ID '" + tbSklID.Text.Trim() + "' již existuje");
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
                    if (tbSklID == tb)
                    {
                        text = "Identifikátor skladu.";
                    }
                    else if (tbSklDesc == tb)  // oznaceni
                    {
                        text = "Název skladu.";
                    }
                    else if (tbSklTyp == tb)  // oznaceni
                    {
                        text = "Typ skladu.";
                    }
                    else if (tbSklCarcode == tb)
                    {
                        text = "Čárový kód skladu.";
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
