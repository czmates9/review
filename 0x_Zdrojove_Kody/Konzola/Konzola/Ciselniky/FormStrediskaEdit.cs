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
    public partial class FormStrediskaEdit : Form
    {
        private Fask.Interfaces.IMES providerStrediska = null;
        /// <summary>
        /// vytvoreny/upraveny zaznam.
        /// </summary>
        public Fask.Interfaces.DataSets.Strediska.CZMST091Row returnrow { get; set; }
        /// <summary>
        /// Stredisko, který se má upravit.
        /// </summary>
        public Fask.Interfaces.DataSets.Strediska.CZMST091Row Strediskorow { get; set; }

        public FormStrediskaEdit()
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

                if (providerStrediska == null)
                    throw new Exception("Provider 'Stredisko' není inicializován");

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
                    if (providerStrediska == null) //inicializace se provede pouze pokud nebyla provedena ... 
                    {
                        //Assembly providerAssemlby = Assembly.LoadFrom(Server.MapPath(providerAssemblyPath));
                        Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                        Type[] types = providerAssemlby.GetTypes();
                        foreach (Type t in types)
                        {
                            try
                            {
                                if (typeof(Fask.Interfaces.Ciselniky.Strediska.IStrediska2).IsAssignableFrom(t))
                                {
                                    providerStrediska = (Fask.Interfaces.Ciselniky.Strediska.IStrediska2)providerAssemlby.CreateInstance(t.FullName);
                                    if (providerStrediska != null)
                                        break;
                                }
                            }
                            catch { }
                        }
                        //return config;
                    }

                    providerStrediska.InitProvider();

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
            if (Strediskorow != null)
            {
                tbSklID.Enabled = false;
                tbSklID.Text = Strediskorow.str_id.Trim();
                tbSklDesc.Text = Strediskorow.Isstr_descNull() ? string.Empty : Strediskorow.str_desc.Trim();
                tbSklTyp.Text = Strediskorow.Isstr_typNull() ? string.Empty : Strediskorow.str_typ.Trim();
                tbSklCarcode.Text = Strediskorow.Isstr_carcodeNull() ? string.Empty : Strediskorow.str_carcode.Trim();
                tb_SKL_ID.Text = Strediskorow.Isskl_idNull() ? string.Empty : Strediskorow.skl_id.Trim();
                tb_ODB_ID.Text = Strediskorow.Isodb_idNull() ? string.Empty : Strediskorow.odb_id.Trim();
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
                if (Strediskorow != null)
                {
                    //Fask.Interfaces.DataSets.Sklady.CZMST093Row sklad_test = ((Fask.Interfaces.Ciselniky.ISklady)providerStrediska).GetSkladByID(tbSklID.Text.Trim());
                    Fask.Interfaces.DataSets.Strediska.CZMST091Row sklad_test;

                    if ((providerStrediska != null) && (providerStrediska is Fask.Interfaces.Ciselniky.Strediska.IStrediska2_GetStrediskoByID))
                        sklad_test = ((Fask.Interfaces.Ciselniky.Strediska.IStrediska2_GetStrediskoByID)providerStrediska).GetStrediskoByID(tbSklID.Text.Trim());
                    else
                        throw new NotImplementedException("Provider neimplementuje IStrediska2_GetStrediskoByID.");


                    if (sklad_test == null)
                    {
                        MessageBox.Show("Záznam byl od posledního načtení odstraněn.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // kvuli refreshi
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        return;
                    }
                    else
                    {
                        IEqualityComparer<Fask.Interfaces.DataSets.Strediska.CZMST091Row> comparer = DataRowComparer.Default;
                        bool isMatch = comparer.Equals(Strediskorow, sklad_test);
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

                Fask.Interfaces.DataSets.Strediska ds2 = new Fask.Interfaces.DataSets.Strediska();
                Fask.Interfaces.DataSets.Strediska.CZMST091Row newSkladRow = ds2.CZMST091.NewCZMST091Row();

                newSkladRow.str_id = tbSklID.Text.Trim();

                if (string.IsNullOrEmpty(tbSklDesc.Text.Trim()))
                    newSkladRow.Setstr_descNull();
                else
                    newSkladRow.str_desc = tbSklDesc.Text.Trim();

                if (string.IsNullOrEmpty(tbSklTyp.Text.Trim()))
                    newSkladRow.Setstr_typNull();
                else
                    newSkladRow.str_typ = tbSklTyp.Text.Trim();

                if(string.IsNullOrEmpty(tbSklCarcode.Text.Trim()))
                    newSkladRow.Setstr_carcodeNull();
                else
                    newSkladRow.str_carcode = tbSklCarcode.Text.Trim();

                if (string.IsNullOrEmpty(tb_SKL_ID.Text.Trim()))
                    newSkladRow.Setskl_idNull();
                else
                    newSkladRow.skl_id = tb_SKL_ID.Text.Trim();

                if (string.IsNullOrEmpty(tb_ODB_ID.Text.Trim()))
                    newSkladRow.Setodb_idNull();
                else
                    newSkladRow.odb_id = tb_ODB_ID.Text.Trim();

                // je úprava záznamu
                if (Strediskorow != null)
                {
                    //((Fask.Interfaces.Ciselniky.ISklady)providerStrediska).UpdateSklad(newSkladRow);

                    if ((providerStrediska != null) && (providerStrediska is Fask.Interfaces.Ciselniky.Strediska.IStrediska2_UpdateStredisko))
                        ((Fask.Interfaces.Ciselniky.Strediska.IStrediska2_UpdateStredisko)providerStrediska).UpdateStredisko(newSkladRow);
                    else
                        throw new NotImplementedException("Provider neimplementuje IStrediska2_UpdateStredisko.");
                }
                else   // nový záznam
                {
                    //((Fask.Interfaces.Ciselniky.ISklady)providerStrediska).InsertSklad(newSkladRow);

                    if ((providerStrediska != null) && (providerStrediska is Fask.Interfaces.Ciselniky.Strediska.IStrediska2_InsertStredisko))
                        ((Fask.Interfaces.Ciselniky.Strediska.IStrediska2_InsertStredisko)providerStrediska).InsertStredisko(newSkladRow);
                    else
                        throw new NotImplementedException("Provider neimplementuje IStrediska2_InsertStredisko.");
                }

                // opetovne nacteni uzivatele
                //returnrow = ((Fask.Interfaces.Ciselniky.ISklady)providerStrediska).GetSkladByID(newSkladRow.skl_id);

                if ((providerStrediska != null) && (providerStrediska is Fask.Interfaces.Ciselniky.Strediska.IStrediska2_GetStrediskoByID))
                    returnrow = ((Fask.Interfaces.Ciselniky.Strediska.IStrediska2_GetStrediskoByID)providerStrediska).GetStrediskoByID(newSkladRow.str_id);
                else
                    throw new NotImplementedException("Provider neimplementuje IStrediska2_GetStrediskoByID.");

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
                    if (Strediskorow == null)
                    {
                        //Fask.Interfaces.DataSets.Sklady.CZMST093Row sklad = ((Fask.Interfaces.Ciselniky.ISklady)providerStrediska).GetSkladByID(tbSklID.Text.Trim());
                        Fask.Interfaces.DataSets.Strediska.CZMST091Row sklad;

                        if ((providerStrediska != null) && (providerStrediska is Fask.Interfaces.Ciselniky.Strediska.IStrediska2_GetStrediskoByID))
                            sklad = ((Fask.Interfaces.Ciselniky.Strediska.IStrediska2_GetStrediskoByID)providerStrediska).GetStrediskoByID(tbSklID.Text.Trim());
                        else
                            throw new NotImplementedException("Provider neimplementuje IStrediska2_GetStrediskoByID.");

                        if (sklad != null)
                            errorProvider1.SetError(tbSklID, "Zadané id strediska již existuje");
                    }
                }

                if (string.IsNullOrEmpty(tbSklDesc.Text.Trim()))
                    errorProvider1.SetError(tbSklDesc, "Musíte zadat název strediska");

                if (Strediskorow == null)
                {
                    // vytváří se nový záznam, kontrola existence id
                    //Fask.Interfaces.DataSets.Sklady.CZMST093Row stav = ((Fask.Interfaces.Ciselniky.ISklady)providerStrediska).GetSkladByID(tbSklID.Text.Trim());
                    Fask.Interfaces.DataSets.Strediska.CZMST091Row stav;

                    if ((providerStrediska != null) && (providerStrediska is Fask.Interfaces.Ciselniky.Strediska.IStrediska2_GetStrediskoByID))
                        stav = ((Fask.Interfaces.Ciselniky.Strediska.IStrediska2_GetStrediskoByID)providerStrediska).GetStrediskoByID(tbSklID.Text.Trim());
                    else
                        throw new NotImplementedException("Provider neimplementuje IStrediska2_GetStrediskoByID.");

                    if (stav != null)
                    {
                        tbSklID.Focus();
                        errorProvider1.SetError(tbSklDesc, "Stredisko s ID '" + tbSklID.Text.Trim() + "' již existuje");
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
                        text = "Identifikátor střediska.";
                    }
                    else if (tbSklDesc == tb)  // oznaceni
                    {
                        text = "Název střediska.";
                    }
                    else if (tbSklTyp == tb)  // oznaceni
                    {
                        text = "Typ střediska.";
                    }
                    else if (tbSklCarcode == tb)
                    {
                        text = "Čárový kód střediska.";
                    }
                    else if (tb_SKL_ID == tb)
                    {
                        text = "ID Skladu.";
                    }
                    else if (tb_ODB_ID == tb)
                    {
                        text = "ID Odběratele/Dodavatele/Partnera ...";
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
