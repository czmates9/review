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

namespace Vyroba_Konzola.Konzola
{
    public partial class FormUzivateleEdit : Form
    {
        private Fask.Console.Interfaces.IVyrobaKonzola providerKonzola = null;
        /// <summary>
        /// Uživatel, který se má upravit.
        /// </summary>
        public Fask.Console.Interfaces.DataSets.Konzola.FASK_LoginsRow loginsrow { get; set; }

        /// <summary>
        /// Oprávnění uživatele.
        /// </summary>
        public Fask.Console.Interfaces.DataSets.Konzola.FASK_Logins_AuthRow loginsAuthrow { get; set; }


        public FormUzivateleEdit()
        {
            InitializeComponent();            
        }

        private void FormUzivateleEdit_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                FormUzivateleEdit_Resize(null, null);

                // inicializace providera
                InitProvider();

                if (providerKonzola == null)
                    throw new Exception("Provider není inicializován");

                LoadData();
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
                if (!String.IsNullOrEmpty(Settings.ProviderKonzola))
                {
                    if (providerKonzola == null) //inicializace se provede pouze pokud nebyla provedena ... 
                    {
                        //Assembly providerAssemlby = Assembly.LoadFrom(Server.MapPath(providerAssemblyPath));
                        Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Vyroba_Konzola.MySystem.MyPath.CurrentDirectory, Settings.ProviderKonzola));
                        Type[] types = providerAssemlby.GetTypes();
                        foreach (Type t in types)
                        {
                            try
                            {
                                //t.IsAssignableFrom(typeof(Fask.Server.Interfaces.Vydej.IVydej);
                                if (typeof(Fask.Console.Interfaces.Konzola.IKonzola2).IsAssignableFrom(t))
                                {
                                    providerKonzola = (Fask.Console.Interfaces.Konzola.IKonzola2)providerAssemlby.CreateInstance(t.FullName);
                                    if (providerKonzola != null)
                                        break;
                                }
                            }
                            catch { }
                        }
                        //return config;
                    }

                    // nastaveni connection stringu
                    if ((providerKonzola != null) && (providerKonzola is Fask.Console.Interfaces.Parametry.IParametry2_ConnectionString))
                        ((Fask.Console.Interfaces.Parametry.IParametry2_ConnectionString)providerKonzola).ConnectionString = Properties.Settings.Default.Konzola_ConnectionString;
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.Log.Write(ex, "Load Provider.Konzola");
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadData()
        {

            // je úprava záznamu, dojde k načtení dat
            if (loginsrow != null)
            {
                textBoxId.Enabled = false;
                textBoxId.Text = loginsrow.id.Trim();
                textBoxJmeno.Text = loginsrow.firstname.Trim();
                textBoxPrijmeni.Text = loginsrow.surname.Trim();
                textBoxHeslo.Text = loginsrow.psswd.Trim();
                if(loginsAuthrow != null)
                    checkBoxAdmin.Checked = Convert.ToBoolean(loginsAuthrow.ADM);
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
                if (!Vyroba_Konzola.MySystem.LoginTest.UserLoginAdminTest())
                    return;

                if (!ValidateData())
                    return;
                
                // je úprava záznamu
                if (loginsrow != null)
                {                    
                    loginsrow.firstname = textBoxJmeno.Text.Trim(); ;
                    loginsrow.surname = textBoxPrijmeni.Text.Trim();
                    loginsrow.psswd = textBoxHeslo.Text.Trim();
                    loginsAuthrow.ADM = checkBoxAdmin.Checked ? (byte)1 : (byte)0;

                    if ((providerKonzola != null) && (providerKonzola is Fask.Console.Interfaces.Konzola.IKonzola2_UpdateUzivatelAOpravneni))
                        ((Fask.Console.Interfaces.Konzola.IKonzola2_UpdateUzivatelAOpravneni)providerKonzola).UpdateUzivatelAOpravneni(loginsrow, loginsAuthrow);
                    else
                        throw new NotImplementedException("Provider neimplementuje IKonzola2_UpdateUzivatelAOpravneni.");

                    
                }
                else   // nový záznam
                {
                    Fask.Console.Interfaces.DataSets.Konzola dsKonzola = new Fask.Console.Interfaces.DataSets.Konzola();
                    Fask.Console.Interfaces.DataSets.Konzola.FASK_LoginsRow newLoginRow = dsKonzola.FASK_Logins.NewFASK_LoginsRow();
                    Fask.Console.Interfaces.DataSets.Konzola.FASK_Logins_AuthRow newAuthRow = dsKonzola.FASK_Logins_Auth.NewFASK_Logins_AuthRow();

                    newLoginRow.id = textBoxId.Text.Trim();
                    newLoginRow.firstname = textBoxJmeno.Text.Trim();
                    newLoginRow.surname = textBoxPrijmeni.Text.Trim();
                    newLoginRow.psswd = textBoxHeslo.Text.Trim();

                    newAuthRow.id = textBoxId.Text.Trim();
                    newAuthRow.Setopr_deleteNull();
                    newAuthRow.Setopr_editNull();
                    newAuthRow.Setopr_insertNull();
                    newAuthRow.Setopr_selectNull();
                    newAuthRow.ADM = checkBoxAdmin.Checked ? (byte)1 : (byte)0;

                    if ((providerKonzola != null) && (providerKonzola is Fask.Console.Interfaces.Konzola.IKonzola2_InsertUzivatelAOpravneni))
                        ((Fask.Console.Interfaces.Konzola.IKonzola2_InsertUzivatelAOpravneni)providerKonzola).InsertUzivatelAOpravneni(newLoginRow, newAuthRow);
                    else
                        throw new NotImplementedException("Provider neimplementuje IKonzola2_InsertUzivatelAOpravneni.");


                }

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Log.Write(ex);
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

                if(string.IsNullOrEmpty(textBoxId.Text.Trim()))
                    errorProvider1.SetError(textBoxId, "Musíte zadat id uživatele");
                    //throw new Exception("Musíte zadat id uživatele");
                
                if (loginsrow == null)
                {
                    Fask.Console.Interfaces.DataSets.Konzola.FASK_LoginsRow user;
                    // vytváří se nový záznam, kontrola existence id
                    if ((providerKonzola != null) && (providerKonzola is Fask.Console.Interfaces.Konzola.IKonzola2_GetUzivatel))
                    {
                        user = ((Fask.Console.Interfaces.Konzola.IKonzola2_GetUzivatel)providerKonzola).GetUzivatel(textBoxId.Text);

                    }
                    else
                    {
                        throw new NotImplementedException("Provider neimplementuje IKonzola2_GetUzivatel.");
                    }

                    if (user != null)
                    {
                        textBoxId.Focus();
                        //textBoxId.SelectAll();
                        errorProvider1.SetError(textBoxId, "Zadané id uživatele již existuje");
                        //throw new Exception("Zadané id uživatele již existuje");
                    }
                }

                //if (string.IsNullOrEmpty(textBoxJmeno.Text.Trim()))
                //{
                //    textBoxJmeno.Focus();
                //    errorProvider1.SetError(textBoxJmeno, "Musíte zadat jméno");
                //    //throw new Exception("Musíte zadat jméno");
                //}

                if (string.IsNullOrEmpty(textBoxPrijmeni.Text.Trim()))
                {
                    textBoxPrijmeni.Focus();
                    errorProvider1.SetError(textBoxPrijmeni, "Musíte zadat příjmení");
                    //throw new Exception("Musíte zadat příjmení");
                }
                if (string.IsNullOrEmpty(textBoxHeslo.Text.Trim()))
                {
                    textBoxHeslo.Focus();
                    errorProvider1.SetError(textBoxHeslo, "Musíte zadat heslo");
                    //throw new Exception("Musíte zadat heslo");
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
    }
}
